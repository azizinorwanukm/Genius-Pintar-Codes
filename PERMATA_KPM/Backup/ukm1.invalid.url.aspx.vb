Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class ukm1_invalid_url
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LoadStrings()

        Catch ex As Exception
            lblMsg.Text = "system error:" & ex.Message
        End Try

    End Sub

    Private Sub LoadStrings()
        lblPerhatian.Text = "PERHATIAN!"
        lblInvalidURL.Text = "Sistem mendapati URL yang digunakan adalah tidak betul atau cubaan untuk menukarnya. Jangan masukkan URL tersebut secara manual."

    End Sub

End Class