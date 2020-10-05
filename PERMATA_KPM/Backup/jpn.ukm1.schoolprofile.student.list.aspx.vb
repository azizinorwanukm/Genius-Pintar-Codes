Public Partial Class jpn_ukm1_schoolprofile_student_list
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnStudentSchoolUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStudentSchoolUpdate.Click
        Response.Redirect("jpn.studentschool.schoolprofile.select.aspx?oldschoolid=" & Request.QueryString("schoolid"))
    End Sub
End Class