Public Partial Class logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("islogin").Value = "N"
        Response.Redirect("default.aspx")

    End Sub

End Class