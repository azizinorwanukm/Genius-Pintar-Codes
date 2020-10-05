Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class tempat_update1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti hendak menghapuskan tempat tersebut?');")

        Try
            If Not IsPostBack Then
                ppcs_tempat_load()
            End If
        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Sub ppcs_tempat_load()
        strSQL = "SELECT * FROM ppcs_tempat WHERE TempatID=" & Request.QueryString("tempatid")

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tempat")) Then
                    txtTempat.Text = ds.Tables(0).Rows(0).Item("Tempat")
                Else
                    txtTempat.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "UPDATE ppcs_tempat SET Tempat='" & oCommon.FixSingleQuotes(txtTempat.Text.ToUpper) & "' WHERE TempatID=" & Request.QueryString("tempatid")
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                ClearScreen()
                lblMsg.Text = "Kemaskini tempat telah berjaya"
            Else
                lblMsg.Text = "system error:" & strRet
            End If
            '--refresh page
            Response.Redirect(Request.RawUrl)

        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try
    End Sub

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtTempat.Text = ""

    End Sub

    Private Function ValidateForm() As Boolean
        If txtTempat.Text.Length = 0 Then
            lblMsg.Text = "Nama Tempat tidak boleh kosong."
            txtTempat.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        strSQL = "DELETE ppcs_tempat WHERE TempatID=" & Request.QueryString("tempatid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            ClearScreen()
            lblMsg.Text = "Hapuskan tempat telah berjaya"
        Else
            lblMsg.Text = "system error:" & strRet
        End If
        '--refresh page
        Response.Redirect(Request.RawUrl)

    End Sub
End Class