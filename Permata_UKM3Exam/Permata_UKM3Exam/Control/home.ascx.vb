Public Class home1
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSTEM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSTEM.Click

        Response.Redirect("login.aspx")

    End Sub

    Private Sub btnEQ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEQ.Click

        Response.Redirect("http://eqtest.permatapintar.edu.my/")

    End Sub

    Private Sub btnKPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKPP.Click

        Response.Redirect("http://ukm3taksir.permatapintar.edu.my/default.aspx?userType=instruktor_kpp")

    End Sub

    Private Sub btnPPCS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPPCS.Click

        Response.Redirect("http://ukm3taksir.permatapintar.edu.my/default.aspx?userType=instruktor_ppcs")

    End Sub

    Private Sub btnRAPPCS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRAPPCS.Click

        Response.Redirect("http://ukm3taksir.permatapintar.edu.my/default.aspx?userType=rappcs")

    End Sub

    Private Sub btnTAPPCS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTAPPCS.Click

        Response.Redirect("http://ukm3taksir.permatapintar.edu.my/default.aspx?userType=tappcs")

    End Sub

End Class