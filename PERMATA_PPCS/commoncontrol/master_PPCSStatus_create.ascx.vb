Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization


Public Class master_PPCSStatus_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub lnkBrowse_Click(sender As Object, e As EventArgs) Handles lnkBrowse.Click
        Response.Redirect("ppcs.ppcsstatus.list.aspx")

    End Sub

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        strSQL = "INSERT INTO master_PPCSStatus (PPCSStatus) VALUES ('" & oCommon.FixSingleQuotes(PPCSStatus.Text.ToUpper) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya menambah Status PPCS baru."
        Else
            lblMsg.Text = "Gagal menambah Status PPCS baru."
        End If

    End Sub

End Class