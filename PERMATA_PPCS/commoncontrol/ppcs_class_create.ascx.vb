Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class ppcs_class_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--refresh
                ClearScreen()

                '--load course detail
                ppcs_course_load()

                '--load tempat
                ppcs_tempat_list()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

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

    Private Sub ClearScreen()
        txtClassCode.Text = ""
        txtClassNameBM.Text = ""
        txtCourseCode.Text = ""
        txtCourseNameBM.Text = ""
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

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            ''--cjeck if code already exist
            strSQL = "SELECT ClassCode FROM ppcs_class WHERE ClassCode='" & txtClassCode.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Kod Kelas telah digunakan! Sila guna Kod Kelas yang baru."
                Exit Sub
            End If

            'insert into course list
            strSQL = "INSERT INTO ppcs_class(CourseID,ClassCode,ClassNameBM,ClassNameBI,PPCSDate,Tempat) VALUES ('" & Request.QueryString("courseid") & "','" & oCommon.FixSingleQuotes(txtClassCode.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtClassNameBM.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtClassNameBM.Text.ToUpper) & "','" & txtPPCSDate.Text & "','" & ddlTempat.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            lblMsg.Text = "Penambahan kelas berjaya!"

            '--refresh page
            Response.Redirect(Request.RawUrl)

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try

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

End Class