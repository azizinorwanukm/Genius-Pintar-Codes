Imports System.Data.SqlClient

Public Class master_config_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.master.config.list.aspx")

    End Sub

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        strSQL = "INSERT INTO master_Config (configCode,configString,configDesc) VALUES ('" & oCommon.FixSingleQuotes(txtconfigCode.Text) & "','" & oCommon.FixSingleQuotes(txtconfigString.Text) & "','" & oCommon.FixSingleQuotes(txtconfigDesc.Text) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya MENAMBAH Sistem Konfigurasi."
        Else
            lblMsg.Text = "GAGAL MENAMBAH Sistem Konfigurasi." & strRet
        End If

    End Sub
End Class