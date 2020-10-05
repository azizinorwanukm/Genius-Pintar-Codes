Public Class admin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblLoginID.Text = Request.Cookies("pcis_admin").Value
            If lblLoginID.Text.Length = 0 Then
                Response.Redirect("default.aspx")
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class