Public Class admin_kelas_select
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Select Case Request.QueryString("set")
                Case "pelajar"
                    lblType.Text = "Penetapan Pelajar"
                Case "instruktor"
                    lblType.Text = "Penetapan Instruktor"
                Case "senaraipelajar"
                    lblType.Text = "Senarai Pelajar"
            End Select

        Catch ex As Exception

        End Try
    End Sub

End Class