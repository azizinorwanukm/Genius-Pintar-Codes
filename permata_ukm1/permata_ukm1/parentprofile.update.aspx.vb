Partial Public Class parentprofile_update1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim studentID As String = Request.QueryString("studentid")

        If Common.isUKM1Done(studentID) Then
            Response.Redirect("default.main.aspx?lang=ms-MY&studentid=" & studentID)
        End If

    End Sub

End Class