Public Partial Class admin_ppcs_merge_main_confirm_msg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '-success/fail
            lblMsg.Text = Request.QueryString("msg")

        Catch ex As Exception

        End Try
    End Sub

End Class