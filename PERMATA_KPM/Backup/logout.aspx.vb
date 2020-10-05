Public Partial Class logout
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '--insert into security audit trail table
            oCommon.LoginTrail(Request.Cookies("ukmkpm_loginid").Value, oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGOUT", "NA")
            Response.Redirect("default.aspx", False)
        Catch ex As Exception
            Response.Redirect("default.aspx", False)
        End Try

    End Sub

End Class