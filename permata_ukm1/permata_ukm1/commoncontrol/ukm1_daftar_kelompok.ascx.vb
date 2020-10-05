
Public Class ukm1_daftar_kelompok
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblUKM1EmailDate.Text = oCommon.getAppsettings("UKM1EmailDate")
                lblDaftarEmail.Text = oCommon.getAppsettings("UKM1DaftarKelompokEmail")
            End If

        Catch ex As Exception
            Response.Write("Err:" & ex.Message)

        End Try
    End Sub

End Class