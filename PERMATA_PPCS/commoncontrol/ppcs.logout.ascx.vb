Imports System.Data
Imports System.Data.OleDb

Partial Public Class ppcs_logout
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strLoginID As String

    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.Cookies("ppcs_loginid") Is Nothing Then
            strLoginID = Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value)

            getPPCsUserType()
        End If

    End Sub

    Private Sub getPPCsUserType()
        lblUserType.Text = Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
        lblPPCSDate.Text = Server.HtmlEncode(Request.Cookies("ppcs_ppcsdate").Value)

    End Sub

    Private Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click
        '--insert into security audit trail table
        oCommon.LogTrail(oCommon.FixSingleQuotes(strLoginID), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "PPCS_LOGOUT", "NA")
        Response.Redirect("../default.aspx", True)

    End Sub
End Class
