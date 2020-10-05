Public Partial Class _default
    Inherits System.Web.UI.MasterPage
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If isMaintenance() = True Then
                    Response.Redirect("under.maintenance.aspx")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function isMaintenance() As Boolean
        If oCommon.getAppsettings("maintenance_pelajar") = "Y" Then
            Return True
        End If

        Return False
    End Function

End Class