Partial Public Class studentprofile_create2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ''--Me.Button1.Attributes.Add("onclick", "javascript:return OpenPopup()")
            ''--Me.Button1.Attributes.Add("onclick", "window.showModalDialog('schoolprofile.select.aspx?schoolname=" & txtPopupValue.Text & "', 'List', 'scrollbars=no,resizable=no,width=800,height=600');return false;")
        End If


    End Sub


   

End Class