Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class tempat_create1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "INSERT INTO ppcs_tempat(Tempat) VALUES ('" & oCommon.FixSingleQuotes(txtTempat.Text.ToUpper) & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                ClearScreen()
                lblMsgTop.Text = "Penambahan tempat telah berjaya"
            Else
                lblMsgTop.Text = "system error:" & strRet
            End If
            '--refresh page
            Response.Redirect(Request.RawUrl)

        Catch ex As Exception
            lblMsgTop.Text = ex.Message

        End Try
    End Sub

    Private Sub ClearScreen()
        lblMsgTop.Text = ""
        txtTempat.Text = ""

    End Sub

    Private Function ValidateForm() As Boolean
        If txtTempat.Text.Length = 0 Then
            lblMsgTop.Text = "Nama Tempat tidak boleh kosong."
            txtTempat.Focus()
            Return False
        End If

        Return True
    End Function


End Class