Public Class logout
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userType As String = Session("userType")

        Try
            '--insert into security audit trail table
            Session.Abandon()
            Session.RemoveAll()
            Session.Clear()

            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1))
            Response.Cache.SetNoStore()
            HttpContext.Current.Session.Abandon()

            oCommon.LoginTrail(CType(Session.Item("permata_admin"), String), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGOUT", "NA")
            Response.Redirect("default.aspx?userType=" & userType)

        Catch ex As Exception
            Response.Redirect("default.aspx?userType=" & userType)

        End Try

    End Sub


End Class