Public Partial Class _default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("kpmadmin_loginid") = ""

        Catch ex As Exception
            Session("kpmadmin_loginid") = ""

        End Try

    End Sub

End Class