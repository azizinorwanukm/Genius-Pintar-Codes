Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class admin
    Inherits System.Web.UI.MasterPage

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Request.Cookies("ppcs_loginid") Is Nothing Then
                lblUsername.Text = Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value)
            Else
                Response.Redirect("../default.aspx")
            End If
            ''lblActiveSession.Text = Application("ActiveSession")

            lblDate.Text = Now.Date.ToString("dddd dd-MM-yyyy")
        Catch ex As Exception
            lblFooterMsg.Text = ex.Message
        End Try


    End Sub

End Class