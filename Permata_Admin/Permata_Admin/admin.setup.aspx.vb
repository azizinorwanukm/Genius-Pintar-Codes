Public Class admin_setup
    Inherits System.Web.UI.Page

    'p@ssw0rd1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnDecrypt_Click(sender As Object, e As EventArgs) Handles btnDecrypt.Click
        Try
            Dim oDes As New Simple3Des(txtKey.Text)
            txtPwdDecrypt.Text = oDes.DecryptData(txtPwdEncrypt.Text)

        Catch ex As Exception
            lblMsg.Text = "Error: " & ex.Message
        End Try

    End Sub

End Class