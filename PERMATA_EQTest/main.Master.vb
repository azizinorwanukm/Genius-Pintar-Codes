Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class main
    Inherits System.Web.UI.MasterPage

    Private rm As ResourceManager
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If isClosed() = True Then
                Response.Redirect("esurvey.closed.aspx")
            End If

            If Request.QueryString("culture") = "en-US" Then
                lnkLanguage.Text = "Bahasa Melayu"
            Else
                lnkLanguage.Text = "English"
            End If

            If Not IsPostBack Then
                If isValidLink() = False Then
                    Response.Redirect("error.invalid.link.aspx")
                    Exit Sub
                End If
                lblCandidatename.Text = getCandidatename()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function isClosed() As Boolean
        Try
            Dim strEndDate As String = oCommon.getAppsettings("EQTest_EndDate")
            strRet = oCommon.getToday()
            If CInt(strRet) > CInt(strEndDate) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub lnkLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLanguage.Click
        Dim strLang As String = ""
        Try
            strLang = Request.QueryString("culture")
            If strLang = "en-US" Then
                strLang = "ms-MY"
            Else
                strLang = "en-US"
            End If

            Dim strRedirectpage As String = Request.FilePath & "?loginid=" & Request.QueryString("loginid") & "&SurveyID=" & Request.QueryString("surveyid") & "&culture=" & strLang
            Response.Redirect(strRedirectpage)
            ''Response.Write(strRedirectpage)
        Catch ex As Exception
            strLang = "en-US"
        End Try

    End Sub

    Private Function isValidLink() As Boolean
        strSQL = "SELECT LoginID FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND SurveyID='" & Request.QueryString("surveyid") & "'"
        '--debug
        ' Response.Write(strSQL)
        If oCommon.isExist(strSQL) = False Then
            Return False
        End If

        Return True
    End Function

    Private Function getCandidatename() As String
        strSQL = "SELECT Fullname FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND SurveyID='" & Request.QueryString("surveyid") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class