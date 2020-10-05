Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class home_candidate
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ci As CultureInfo
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("culture"))
            'get the culture info to set the language
            rm = New ResourceManager("Resources.eqtest_2014", System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

        Catch ex As Exception
            '--debug
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblHomeOverview.Text = rm.GetString("lblHomeOverview", ci)
        lblHomeCandidate.Text = rm.GetString("lblHomeCandidate", ci)
        lblHomeBenefit.Text = rm.GetString("lblHomeBenefit", ci)
        lblHomeBenefitContent.Text = rm.GetString("lblHomeBenefitContent", ci)

        lnkNext.Text = rm.GetString("lnkNext", ci)

    End Sub

    Private Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Dim strNextpage As String = "esurvey.page01.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Dim strDonepage As String = "esurvey.page.done.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        ''--can retake the survey
        If isComplete() = "Y" Then
            Response.Redirect(strDonepage)
        Else
            Response.Redirect(strNextpage)
        End If

    End Sub

    Private Function isComplete() As String
        strSQL = "SELECT IsCompleted FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class