Public Class popup
    Inherits System.Web.UI.MasterPage
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblExamYear.Text = oCommon.getAppsettings("UKM1ExamYear")

            End If
        Catch ex As Exception

        End Try
    End Sub

End Class