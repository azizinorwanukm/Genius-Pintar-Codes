Public Partial Class system_error
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblMsg.Text = Request.QueryString("msg")

        Catch ex As Exception

        End Try

    End Sub

End Class