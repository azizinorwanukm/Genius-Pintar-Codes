Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class main2
    Inherits System.Web.UI.MasterPage

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strCourseCode As String = ""


    Dim strLoginID As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Request.Cookies("ppcs_loginid") Is Nothing Then
                strLoginID = Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value)
            Else
                Response.Redirect("../default.aspx")
            End If
            lblUsername.Text = strLoginID

            ''--display course and class
            ''--ketua modul able to view all class
            lblDate.Text = Now.Date.ToString("dddd dd-MM-yyyy")
        Catch ex As Exception
            lblFooterMsg.Text = ex.Message
        End Try

    End Sub

End Class