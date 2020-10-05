Public Class studentprofile_update_mykad1
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblIPAddress.Text = GetIPAddress()
            'oCommon.TransactionLog("studentprofile_update_load", "IP Address:" & lblIPAddress.Text, Request.UserHostAddress, CType(Session.Item("permata_mykad"), String))

        Catch ex As Exception

        End Try
    End Sub

    Private Function GetIPAddress() As String
        Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
        Dim strRet As String = "REMOTE_ADDR:" & context.Request.ServerVariables("REMOTE_ADDR")
        strRet += " REMOTE_HOST:" & context.Request.ServerVariables("REMOTE_HOST")
        strRet += " SERVER_NAME:" & context.Request.ServerVariables("SERVER_NAME")
        strRet += " HTTP_USER_AGENT:" & context.Request.ServerVariables("HTTP_USER_AGENT")
        strRet += " TIME:" & Now.ToLongTimeString
        strRet += " STUDENTID:" & CType(Session.Item("permata_studentid"), String)

        Return strRet
    End Function

End Class