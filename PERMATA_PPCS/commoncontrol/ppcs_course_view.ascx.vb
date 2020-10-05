Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class ppcs_course_view
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strCourseID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strCourseID = Request.QueryString("courseid")
            If Not IsPostBack Then
                Load_courseDetails(strCourseID)
            End If

        Catch ex As Exception
            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        End Try

    End Sub

    Private Sub Load_courseDetails(ByVal strValue As String)

        strSQL = "SELECT CourseCode FROM ppcs_course WHERE CourseID=" & strCourseID
        txtCourseCode.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT CourseNameBM FROM ppcs_course WHERE CourseID=" & strCourseID
        txtCourseNameBM.Text = oCommon.getFieldValue(strSQL)

    End Sub

End Class