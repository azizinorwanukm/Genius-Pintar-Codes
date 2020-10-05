Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class admin_pensyarah_view
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Response.Redirect("admin.pensyarah.update.aspx?pensyarahid=" & Request.QueryString("pensyarahid"))

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        strSQL = "DELETE koko_pensyarah WHERE PensyarahID='" & Request.QueryString("pensyarahid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Rekod berjaya dihapuskan."
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

End Class