Public Class default_timeout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("koko_loginid") = ""

        Catch ex As Exception

        End Try
    End Sub

End Class