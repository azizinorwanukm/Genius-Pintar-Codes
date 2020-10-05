Public Partial Class ukm1_intro_page02
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strURL As String = "ukm1.modA.page01.aspx"
        Response.Redirect(strURL)

    End Sub

End Class