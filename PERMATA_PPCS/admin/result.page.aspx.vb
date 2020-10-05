Public Partial Class result_page
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = Request.QueryString("msg")
        divMsg.Attributes("class") = "info"

    End Sub

End Class