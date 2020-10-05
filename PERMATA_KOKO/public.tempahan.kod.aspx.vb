Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient


Public Class public_tempahan_kod
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        'check form validation. if failed exit
        If ValidateForm() = False Then
            Exit Sub
        End If

        Response.Redirect("public.tempahan.cancel.aspx?kodtempahan=" & oCommon.FixSingleQuotes(txtKodTempahan.Text))

    End Sub

    Private Function ValidateForm() As Boolean
        If txtKodTempahan.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtKodTempahan.Focus()
            Return False
        End If

        strSQL = "SELECT KodTempahan FROM koko_tempahandetail WHERE kodTempahan='" & oCommon.FixSingleQuotes(txtKodTempahan.Text) & "'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Kod Tempahan tidak ditemui. Sila masukkan semula."
            Return False
        End If

        Return True
    End Function
End Class