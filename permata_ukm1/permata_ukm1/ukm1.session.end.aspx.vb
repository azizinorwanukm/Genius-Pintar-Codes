Public Partial Class ukm1_session_end
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = Request.QueryString("lblmsg")

    End Sub

End Class