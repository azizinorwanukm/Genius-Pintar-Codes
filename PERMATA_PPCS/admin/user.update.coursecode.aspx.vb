Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class user_update_coursecode
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strppcsuserid As String = ""
    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ''--get CourseNameBM
                strSQL = "SELECT CourseNameBM FROM PPCS_Course WHERE courseid=" & Request.QueryString("courseid")
                lblCourseNameBM.Text = oCommon.getFieldValue(strSQL)

                ''--get PPCSDate
                strSQL = "SELECT PPCSDate FROM PPCS_Course WHERE courseid=" & Request.QueryString("courseid")
                lblPPCSDate.Text = oCommon.getFieldValue(strSQL)

                ''--usertype
                lblUserType.Text = Request.QueryString("usertype")
                lblType.Text = Request.QueryString("type")

            End If

        Catch ex As Exception
            'lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            ' oCommon.WriteLogFile(strPath, strMsg)

        End Try

    End Sub

    Private Sub Update_ppc_users()
        Try
            '--PPCS_Users Fullname
            strSQL = "SELECT Fullname FROM PPCS_Users WHERE myGUID='" & Request.QueryString("myguid") & "'"
            Dim strFullname As String = oCommon.getFieldValue(strSQL)

            ''--update PPCS_Course
            Select Case Request.QueryString("type")
                Case "BM"
                    strSQL = "UPDATE PPCS_Course SET KetuaModul='" & Request.QueryString("myguid") & "',NamaKetuaModul='" & strFullname & "' WHERE CourseID=" & Request.QueryString("courseid")
                Case "BI"
                    strSQL = "UPDATE PPCS_Course SET KetuaModulBI='" & Request.QueryString("myguid") & "',NamaKetuaModulBI='" & strFullname & "' WHERE CourseID=" & Request.QueryString("courseid")
                Case Else
                    strSQL = "UPDATE PPCS_Course SET KetuaModul='" & Request.QueryString("myguid") & "',NamaKetuaModul='" & strFullname & "' WHERE CourseID=" & Request.QueryString("courseid")
            End Select
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                lblMsg.Text = "Berjaya menentukan " & lblUserType.Text & " untuk kursus " & lblCourseNameBM.Text
            Else
                lblMsg.Text = "System Error:" & strRet
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

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("course.list.ketuamodul.aspx?usertype=" & lblUserType.Text)

        ''http://localhost/ppcs/admin/class.list.aspx?usertype=PENGAJAR

    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Update_ppc_users()

    End Sub

End Class