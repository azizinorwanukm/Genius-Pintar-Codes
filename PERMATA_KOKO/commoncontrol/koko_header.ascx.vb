Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_header
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_load()
        Dim strField As String = Request.QueryString("field")
        Dim strValue As String = Request.QueryString("value")
        Dim strFieldName As String = ""

        Select Case strField
            Case "UniformID"
                strSQL = "SELECT * FROM koko_uniform WHERE UniformID=" & Request.QueryString("value")
                strFieldName = "Uniform"
            Case "PersatuanID"
                strSQL = "SELECT * FROM koko_persatuan WHERE PersatuanID=" & Request.QueryString("value")
                strFieldName = "Persatuan"
            Case "SukanID"
                strSQL = "SELECT * FROM koko_sukan WHERE SukanID=" & Request.QueryString("value")
                strFieldName = "Sukan"
            Case "RumahsukanID"
                strSQL = "SELECT * FROM koko_rumahsukan WHERE RumahSukanID=" & Request.QueryString("value")
                strFieldName = "Rumahsukan"
        End Select
        '--debug
        'Response.Write(strSQL)

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    lblTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    lblTahun.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item(strFieldName)) Then
                    lblNama.Text = ds.Tables(0).Rows(0).Item(strFieldName)
                Else
                    lblNama.Text = ""
                End If

            End If

        Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class