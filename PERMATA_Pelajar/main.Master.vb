Public Partial Class main
    Inherits System.Web.UI.MasterPage
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim strpermata_studentid As String = CType(Session.Item("permata_studentid"), String)
                If strpermata_studentid = "" Then
                    Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again.")
                End If

                Dim strpermata_mykad As String = CType(Session.Item("permata_mykad"), String)
                If strpermata_mykad = "" Then
                    Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again.")
                End If

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