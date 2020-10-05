Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class ppcs_class_update1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti hendak menghapuskan kelas tersebut?');")

        Try
            If Not IsPostBack Then
                '--load course detail
                ppcs_course_load()

                ppcs_tempat_list()

                '--class
                ppcs_class_load()
            End If
        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Sub ppcs_tempat_list()
        strSQL = "SELECT Tempat FROM ppcs_tempat ORDER BY Tempat"

        '--debug
        'Response.Write("examyear_list:" & strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTempat.DataSource = ds
            ddlTempat.DataTextField = "Tempat"
            ddlTempat.DataValueField = "Tempat"
            ddlTempat.DataBind()

            'ddlTempat.Items.Add(New ListItem("ALL", "ALL"))
        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ppcs_class_load()
        strSQL = "SELECT * FROM ppcs_class WHERE classid=" & Request.QueryString("classid")
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassCode")) Then
                    txtClassCode.Text = ds.Tables(0).Rows(0).Item("ClassCode")
                Else
                    txtClassCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassNameBM")) Then
                    txtClassNameBM.Text = ds.Tables(0).Rows(0).Item("ClassNameBM")
                Else
                    txtClassNameBM.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tempat")) Then
                    ddlTempat.Text = ds.Tables(0).Rows(0).Item("Tempat")
                Else
                    ddlTempat.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcs_course_load()
        strSQL = "SELECT * FROM ppcs_course WHERE CourseID=" & Request.QueryString("courseid")
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseCode")) Then
                    txtCourseCode.Text = ds.Tables(0).Rows(0).Item("CourseCode")
                Else
                    txtCourseCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseNameBM")) Then
                    txtCourseNameBM.Text = ds.Tables(0).Rows(0).Item("CourseNameBM")
                Else
                    txtCourseNameBM.Text = ""
                End If

                ''If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseNameBI")) Then
                ''    txtCourseNameBI.Text = ds.Tables(0).Rows(0).Item("CourseNameBI")
                ''Else
                ''    txtCourseNameBI.Text = ""
                ''End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSDate")) Then
                    txtPPCSDate.Text = ds.Tables(0).Rows(0).Item("PPCSDate")
                Else
                    txtPPCSDate.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "UPDATE ppcs_class SET ClassCode='" & oCommon.FixSingleQuotes(txtClassCode.Text) & "',ClassNameBM='" & oCommon.FixSingleQuotes(txtClassNameBM.Text) & "',Tempat='" & ddlTempat.Text & "' WHERE ClassID=" & Request.QueryString("classid")
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                ClearScreen()
                lblMsg.Text = "Kemaskini tempat telah berjaya"
                '--refresh page
                Response.Redirect(Request.RawUrl)
            Else
                lblMsg.Text = "system error:" & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try
    End Sub

    Private Sub ClearScreen()
        txtClassCode.Text = ""
        txtClassNameBM.Text = ""
        txtCourseCode.Text = ""
        txtCourseNameBM.Text = ""
    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtClassCode.Text.Length = 0 Then
            lblMsg.Text = "Kod kelas perlu di isi."
            txtClassCode.Focus()
            Return False
        End If

        If txtClassNameBM.Text.Length = 0 Then
            lblMsg.Text = "Nama kelas perlu di isi."
            txtCourseCode.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click

        strSQL = "DELETE ppcs_class WHERE classid=" & Request.QueryString("classid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            ClearScreen()
            lblMsg.Text = "Hapuskan kelas telah berjaya"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

        '--refresh page
        Response.Redirect(Request.RawUrl)

    End Sub

End Class