Public Class ppcs_alumni_studentprofile1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("pageid") = "ppcs.alumni.studentprofile.aspx"
            lblMsg.Text = "Pageid:" & CType(Session.Item("pageid"), String)


        Catch ex As Exception

        End Try
    End Sub

End Class