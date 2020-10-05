
Imports System.Web.Configuration

Public Class upsi_maintenance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Session.Abandon()

            Dim IsUnderMaintenance As String = WebConfigurationManager.AppSettings("IsUnderMaintenance").ToString()

            If IsUnderMaintenance <> "1" Then Response.Redirect("default.aspx")

        End If

    End Sub

End Class