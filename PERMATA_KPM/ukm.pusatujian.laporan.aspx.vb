Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class ukm_pusatujian_laporan
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                PusatUjian_Load()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PusatUjian_Load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''--display PusatUjian profile
        strSQL = "SELECT * FROM PusatUjian WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("Komen")) Then
                    txtKomen.Text = MyTable.Rows(nRows).Item("Komen").ToString
                Else
                    txtKomen.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        strSQL = "UPDATE PusatUjian SET Komen='" & oCommon.FixSingleQuotes(txtKomen.Text) & "' WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini komen Pusat Ujian."
        Else
            lblMsg.Text = "Berjaya mengemaskini komen Pusat Ujian."
        End If

    End Sub


End Class