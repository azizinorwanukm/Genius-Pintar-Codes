Public Class admin_kelaskoko_list
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Select Case Request.QueryString("set")
                Case "instruktor"
                    lblTitle.Text = "Penetapan Instruktor"
                Case "pelajar"
                    lblTitle.Text = "Penetapan Pelajar"
                Case "kehadiran"
                    lblTitle.Text = "Kehadiran Pelajar"
            End Select
        Catch ex As Exception

        End Try
    End Sub

End Class