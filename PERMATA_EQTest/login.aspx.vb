Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class login
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strLoginID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblSurveyID.Text = oCommon.getAppsettings("EQTest_SurveyID")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim strLink As String = ""

        strSQL = "SELECT StudentID FROM StudentProfile WHERE AlumniID='" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "'"
        strLoginID = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT LoginID FROM EQTest WHERE LoginID='" & strLoginID & "' AND SurveyID='" & oCommon.getAppsettings("EQTest_SurveyID") & "'"
        '--debug
        'Response.Write(strSQL)
        If oCommon.isExist(strSQL) = True Then
            Response.Cookies("islogin").Value = "Y"
            strLink = "home.candidate.aspx?loginid=" & strLoginID & "&surveyid=" & oCommon.getAppsettings("EQTest_SurveyID") & "&culture=ms-MY"
            Response.Redirect(strLink)
        Else
            Response.Cookies("islogin").Value = "N"
            lblLoginMsg.Text = "Alumni ID tidak ditemui. Sila cuba sekali lagi."
        End If

    End Sub
End Class