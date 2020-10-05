Public Partial Class _default
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblUKM1DisplayEnd.Text = oCommon.getAppsettings("UKM1DisplayEnd")
            lblUKM1DisplayResult.Text = oCommon.getAppsettings("UKM1DisplayResult")

        Catch ex As Exception

        End Try
    End Sub

    
End Class