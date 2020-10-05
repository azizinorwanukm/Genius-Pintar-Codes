Public Partial Class _default
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("kpmadmin_loginid") = ""
        Catch ex As Exception

        End Try

    End Sub

End Class