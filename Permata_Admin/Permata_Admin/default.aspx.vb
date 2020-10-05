Public Partial Class _default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("permata_admin") = ""
            Session("pageid") = ""
        Catch ex As Exception
            Session("permata_admin") = ""
        End Try

    End Sub

End Class