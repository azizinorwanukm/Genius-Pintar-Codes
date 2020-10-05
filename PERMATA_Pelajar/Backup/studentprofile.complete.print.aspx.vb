Public Partial Class studentprofile_complete_print
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnPrint.Attributes.Add("onClick", "javascript:window.print(); return false;")

    End Sub

End Class