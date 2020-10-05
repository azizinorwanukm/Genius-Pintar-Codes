Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class _default2
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value) = "" Then
                Response.Redirect("../default.aspx")
            Else
                lblMsg.Text = "LoginID:" & Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value)
            End If
        Catch ex As Exception
            lblMsg.Text = "errormessage:" & ex.Message
        End Try

    End Sub

End Class