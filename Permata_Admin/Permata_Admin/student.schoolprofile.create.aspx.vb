Public Partial Class student_schoolprofile_create
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("admin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub
End Class