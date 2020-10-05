Public Partial Class admin_default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '--debug
            'Response.Write("admin_default:" & CType(Session.Item("permata_admin"), String))
            Session("pageid") = "admin.studentprofile.view.aspx"

        Catch ex As Exception

        End Try
    End Sub

End Class