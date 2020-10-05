Public Partial Class _default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("permata_studentid") = ""
        Session("permata_mykad") = ""

        Response.Cookies("permata_schoolid").Value = ""
        Response.Cookies("permata_schoolid").Expires = DateTime.Now.AddDays(1)

    End Sub

End Class