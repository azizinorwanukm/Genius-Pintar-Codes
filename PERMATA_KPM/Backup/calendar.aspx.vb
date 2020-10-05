Public Partial Class calendar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub calDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calDate.SelectionChanged
        txtDate.Text = calDate.SelectedDate.ToString("d")

    End Sub
End Class