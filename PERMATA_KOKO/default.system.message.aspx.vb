Public Class default_system_message
    Inherits System.Web.UI.Page
    Dim oCommon As New Commonfunction
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblUsername.Text = CType(Session.Item("koko_loginid"), String)
                lblUserType.Text = Server.HtmlEncode(Request.Cookies("koko_usertype").Value)

                strRet = oCommon.koko_txnlog("SECURITY", lblUsername.Text & ":" & lblUserType.Text & ":" & " attempt to change usertype.")
                If Not strRet = "0" Then
                    displayDebug(strRet)
                End If

                '--resset cookies
                Session("koko_loginid") = ""
                Response.Cookies("koko_usertype").Value = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub


End Class