Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class popup2
    Inherits System.Web.UI.MasterPage

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strCourseCode As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Request.Cookies("ppcs_loginid") Is Nothing Then
                lblUsername.Text = Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value)
            End If
            If Not Request.Cookies("ppcs_usertype") Is Nothing Then
                lblUserType.Text = Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
            End If

        Catch ex As Exception
            lblFooterMsg.Text = ex.Message
        End Try
    End Sub

End Class