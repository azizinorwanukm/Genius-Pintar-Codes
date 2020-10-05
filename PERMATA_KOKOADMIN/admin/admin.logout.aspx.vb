Public Class admin_logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Redirect("../default.aspx")

        Catch ex As Exception

        End Try
    End Sub

End Class