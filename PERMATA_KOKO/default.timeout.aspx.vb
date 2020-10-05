Public Class default_timeout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("koko_loginid") = ""
                Response.Cookies("koko_usertype").Value = ""

            End If

        Catch ex As Exception

        End Try
    End Sub

End Class