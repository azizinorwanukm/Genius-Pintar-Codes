Public Class pengajar_koko_kehadiranPelajar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("getStatus") = "" Then
            Session("getStatus") = "VA"
        End If

    End Sub

End Class