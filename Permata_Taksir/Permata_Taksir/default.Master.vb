Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim user As String = Request.QueryString("userType")
        If user = "rappcs" Then
            lbl_user.Text = "RA PPCS"
        ElseIf user = "instruktor_ppcs" Then
            lbl_user.Text = "Instruktor PPCS"
        ElseIf user = "instruktor_kpp" Then
            lbl_user.Text = "Instruktor KPP"
        ElseIf user = "tappcs" Then
            lbl_user.Text = "TA PPCS"
        Else
            Response.Redirect("default.aspx?userType=instruktor_kpp")
        End If
    End Sub

End Class