Public Class logged
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("User") = "" Then
            Response.Redirect("login.aspx")
        End If
    End Sub

End Class