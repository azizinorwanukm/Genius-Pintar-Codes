Public Class instruktor_parent_page
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btnCity.Attributes.Add("onclick", "javascript:return OpenPopup('instruktor.child.page.aspx','Senarai Bandar',400,600)")
        End If

    End Sub

End Class