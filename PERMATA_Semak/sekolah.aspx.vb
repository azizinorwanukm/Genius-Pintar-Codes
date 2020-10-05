Public Partial Class sekolah
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '--not yet open
        Response.Redirect("contactus.aspx")
        Exit Sub

    End Sub

    Protected Sub btnSemak_Click(sender As Object, e As EventArgs) Handles btnSemak.Click

    End Sub
End Class