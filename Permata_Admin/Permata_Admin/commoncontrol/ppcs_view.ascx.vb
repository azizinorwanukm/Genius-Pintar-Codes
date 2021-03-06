Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization


Partial Public Class ppcs_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strSchoolID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ''--admin and subadmin only able to edit
                setAccessRight()
                ppcs_view()

            End If

        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Sub setAccessRight()

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                lnkEdit.Enabled = True
            Case "SUBADMIN"
                lnkEdit.Enabled = True
            Case "ADMINOP"
                lnkEdit.Enabled = False
            Case "PENGURUS PEJABAT"
                lnkEdit.Enabled = True
            Case Else
                lnkEdit.Visible = False
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM PPCS_Users WHERE loginid='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub ppcs_view()
        Dim strClassID As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM PPCS WHERE PPCSID=" & Request.QueryString("PPCSID")
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSID")) Then
                    lblPPCSID.Text = ds.Tables(0).Rows(0).Item("PPCSID")
                Else
                    lblPPCSID.Text = ""
                End If

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseID")) Then
                    lblCourseID.Text = ds.Tables(0).Rows(0).Item("CourseID")
                Else
                    lblCourseID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSClass")) Then
                    lblPPCSClass.Text = ds.Tables(0).Rows(0).Item("PPCSClass")
                Else
                    lblPPCSClass.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassID")) Then
                    lblClassID.Text = ds.Tables(0).Rows(0).Item("ClassID")
                Else
                    lblClassID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSStatus")) Then
                    lblPPCSStatus.Text = ds.Tables(0).Rows(0).Item("PPCSStatus")
                Else
                    lblPPCSStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaAsrama")) Then
                    lblNamaAsrama.Text = ds.Tables(0).Rows(0).Item("NamaAsrama")
                Else
                    lblNamaAsrama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NoBilik")) Then
                    lblNoBilik.Text = ds.Tables(0).Rows(0).Item("NoBilik")
                Else
                    lblNoBilik.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SaizBaju")) Then
                    lblSaizBaju.Text = ds.Tables(0).Rows(0).Item("SaizBaju")
                Else
                    lblSaizBaju.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SakitAlahan")) Then
                    lblSakitAlahan.Text = ds.Tables(0).Rows(0).Item("SakitAlahan")
                Else
                    lblSakitAlahan.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassID")) Then
                    strClassID = ds.Tables(0).Rows(0).Item("ClassID")
                Else
                    strClassID = ""
                End If

                '--get Tempat from PPCS_Class
                If Not strClassID.Length = 0 Then
                    lblTempat.Text = getTempat(strClassID)
                Else
                    lblTempat.Text = "NA"
                End If

                '--record exist. baru boleh edit
                lblMsg.Text = "Pelajar LAYAK pada Sessi PPCS tersebut!"
                lnkEdit.Enabled = True
            Else
                '--record tak ada. tak boleh edit
                lblMsg.Text = "Pelajar TIDAK LAYAK pada Sessi PPCS tersebut!"
                lnkEdit.Enabled = False
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

    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        Response.Redirect("ppcs.update.aspx?PPCSID=" & lblPPCSID.Text & "&studentid=" & Request.QueryString("studentid"))

    End Sub

    Private Sub ClearScreen()
        lblNamaAsrama.Text = ""
        lblNoBilik.Text = ""
        lblPPCSClass.Text = ""
        lblPPCSCourse.Text = ""
        lblPPCSStatus.Text = ""
        lblSaizBaju.Text = ""
        lblSakitAlahan.Text = ""

    End Sub

    Protected Sub lnkHarian_Click(sender As Object, e As EventArgs) Handles lnkHarian.Click
        Response.Redirect("ppcs.eval.daily.view.aspx?studentid=" & Request.QueryString("studentid") & "&ppcsid=" & Request.QueryString("PPCSID"))

    End Sub

    Protected Sub lnkMingguan_Click(sender As Object, e As EventArgs) Handles lnkMingguan.Click
        Response.Redirect("ppcs.eval.weekly.view.aspx?studentid=" & Request.QueryString("studentid") & "&ppcsdate=" & lblPPCSDate.Text & "&classid=" & lblClassID.Text)

    End Sub

    Private Sub lnkAkhir_Click(sender As Object, e As EventArgs) Handles lnkAkhir.Click
        Response.Redirect("ppcs.eval.end.view.aspx?studentid=" & Request.QueryString("studentid") & "&ppcsdate=" & lblPPCSDate.Text & "&classid=" & lblClassID.Text)

    End Sub
End Class