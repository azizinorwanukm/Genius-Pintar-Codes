Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ppcs_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strSchoolID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ppcs_view()

                PPCS_Course_list()
                ddlPPCSCourse.SelectedValue = lblCourseID.Text

                If Not lblCourseID.Text.Length = 0 Then
                    PPCS_Class_list()
                    ddlPPCSClass.SelectedValue = lblClassID.Text
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Sub PPCS_Course_list()
        strSQL = "SELECT CourseID,CourseCode FROM PPCS_Course WHERE PPCSDate='" & lblPPCSDate.Text & "' ORDER BY CourseCode"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSCourse.DataSource = ds
            ddlPPCSCourse.DataValueField = "CourseID"
            ddlPPCSCourse.DataTextField = "CourseCode"
            ddlPPCSCourse.DataBind()

            '--ddlPPCSCourse.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub PPCS_Class_list()
        strSQL = "SELECT ClassID,ClassCode FROM PPCS_Class WHERE CourseID=" & ddlPPCSCourse.SelectedValue & " ORDER BY ClassCode"
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSClass.DataSource = ds
            ddlPPCSClass.DataValueField = "ClassID"
            ddlPPCSClass.DataTextField = "ClassCode"
            ddlPPCSClass.DataBind()

            '--ddlPPCSClass.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub PPCSStatus_list()
        strSQL = "SELECT PPCSStatus FROM master_PPCSStatus ORDER BY PPCSStatus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSStatus.DataSource = ds
            ddlPPCSStatus.DataTextField = "PPCSStatus"
            ddlPPCSStatus.DataValueField = "PPCSStatus"
            ddlPPCSStatus.DataBind()

            ddlPPCSStatus.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub ppcs_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM PPCS WHERE PPCSID='" & Request.QueryString("PPCSID") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSDate")) Then
                    lblPPCSDate.Text = ds.Tables(0).Rows(0).Item("PPCSDate")
                Else
                    lblPPCSDate.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSCourse")) Then
                    lblPPCSCourse.Text = ds.Tables(0).Rows(0).Item("PPCSCourse")
                Else
                    lblPPCSCourse.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSClass")) Then
                    lblPPCSClass.Text = ds.Tables(0).Rows(0).Item("PPCSClass")
                Else
                    lblPPCSClass.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSStatus")) Then
                    lblPPCSStatus.Text = ds.Tables(0).Rows(0).Item("PPCSStatus")
                Else
                    lblPPCSStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaAsrama")) Then
                    txtNamaAsrama.Text = ds.Tables(0).Rows(0).Item("NamaAsrama")
                Else
                    txtNamaAsrama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NoBilik")) Then
                    txtNoBilik.Text = ds.Tables(0).Rows(0).Item("NoBilik")
                Else
                    txtNoBilik.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SaizBaju")) Then
                    txtSaizBaju.Text = ds.Tables(0).Rows(0).Item("SaizBaju")
                Else
                    txtSaizBaju.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SakitAlahan")) Then
                    txtSakitAlahan.Text = ds.Tables(0).Rows(0).Item("SakitAlahan")
                Else
                    txtSakitAlahan.Text = ""
                End If

                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tempat")) Then
                '    lblTempat.Text = ds.Tables(0).Rows(0).Item("Tempat")
                'Else
                '    lblTempat.Text = ""
                'End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseID")) Then
                    lblCourseID.Text = ds.Tables(0).Rows(0).Item("CourseID")
                Else
                    lblCourseID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassID")) Then
                    lblClassID.Text = ds.Tables(0).Rows(0).Item("ClassID")
                Else
                    lblClassID.Text = ""
                End If

                '--get Tempat from PPCS_Class
                If Not lblClassID.Text.Length = 0 Then
                    lblTempat.Text = getTempat(lblClassID.Text)
                Else
                    lblTempat.Text = "NA"
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getTempat(ByVal strClassID As String) As String
        strSQL = "SELECT Tempat FROM PPCS_Class WHERE ClassID=" & strClassID
        Return oCommon.getFieldValue(strSQL)

    End Function

    Private Function ppcs_update() As Boolean
        strSQL = "UPDATE PPCS SET CourseID=" & ddlPPCSCourse.SelectedValue & ",PPCSCourse='" & ddlPPCSCourse.SelectedItem.Text & "',ClassID=" & ddlPPCSClass.SelectedValue & ",PPCSClass='" & ddlPPCSClass.SelectedItem.Text & "',Tempat='" & oCommon.FixSingleQuotes(lblTempat.Text.ToUpper) & "',NamaAsrama='" & oCommon.FixSingleQuotes(txtNamaAsrama.Text) & "',NoBilik='" & oCommon.FixSingleQuotes(txtNoBilik.Text) & "',SaizBaju='" & oCommon.FixSingleQuotes(txtSaizBaju.Text) & "',SakitAlahan='" & oCommon.FixSingleQuotes(txtSakitAlahan.Text) & "' WHERE PPCSID='" & Request.QueryString("PPCSID") & "'"
        '--debug
        'Response.Write(ddlPPCSCourse.SelectedValue & "|" & ddlPPCSCourse.SelectedItem.Text & "|" & ddlPPCSCourse.Text)

        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "system error:" & strRet
            Return False
        End If

    End Function

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If lblCourseID.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih Kursus terlebih dahulu. Jika tiada, masukkan terlebih dahulu melalui Sistem PPCS."
            Exit Sub
        End If

        ''update PPCS
        If ppcs_update() = True Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini maklumat pelajar!"
        Else
            divMsg.Attributes("class") = "error"
        End If

    End Sub

    Private Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("ppcs.alumni.studentprofile.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub

    Private Sub ddlPPCSCourse_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPPCSCourse.TextChanged
        lblCourseID.Text = ddlPPCSCourse.SelectedValue
        PPCS_Class_list()

    End Sub

    Protected Sub lnkHarian_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkHarian.Click
        If lblClassID.Text.Length = 0 Then
            lblMsg.Text = "Sila tentukan Kelas pelajar terlebih dahulu."
            Exit Sub
        End If
        Response.Redirect("PPCS.Eval.Daily.create.aspx?studentid=" & Request.QueryString("studentid") & "&ppcsdate=" & lblPPCSDate.Text & "&classid=" & lblClassID.Text)

    End Sub

    Protected Sub lnkMingguan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkMingguan.Click
        If lblClassID.Text.Length = 0 Then
            lblMsg.Text = "Sila tentukan Kelas pelajar terlebih dahulu."
            Exit Sub
        End If
        Response.Redirect("PPCS.Eval.Weekly.create.aspx?studentid=" & Request.QueryString("studentid") & "&ppcsdate=" & lblPPCSDate.Text & "&classid=" & lblClassID.Text)

    End Sub

    Protected Sub lnkAkhir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkAkhir.Click
        If lblClassID.Text.Length = 0 Then
            lblMsg.Text = "Sila tentukan Kelas pelajar terlebih dahulu."
            Exit Sub
        End If

        Response.Redirect("PPCS.Eval.End.create.aspx?studentid=" & Request.QueryString("studentid") & "&ppcsdate=" & lblPPCSDate.Text & "&classid=" & lblClassID.Text)

    End Sub

    Private Sub ddlPPCSClass_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPPCSClass.TextChanged
        lblClassID.Text = ddlPPCSClass.SelectedValue
        lblTempat.Text = getTempat(lblClassID.Text)

    End Sub
End Class