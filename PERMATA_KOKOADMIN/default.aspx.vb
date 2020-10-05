Public Class _default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--reset cookies
                Session("koko_loginid") = ""
                Response.Cookies("kokoadmin_usertype").Value = ""

            End If
        Catch ex As Exception

        End Try
    End Sub

End Class