Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class tor_instruktor
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                LoadPage()
                LoadPageDokumen()
                LoadBadan()
                LoadSukan()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadPage()
        Dim Test As New StringBuilder()
        Dim i As Integer = 1

        strSQL = "SELECT Pengumuman from master_pengumuman where Jenis_Kokurikulum = 'Kelab Dan Persatuan'"

        Dim Pengumuman As New DataTable
        Dim SQL_Pengumuman As New SqlDataAdapter(strSQL, strConn)

        Try
            SQL_Pengumuman.Fill(Pengumuman)
        Catch ex As Exception
        End Try

        Test.Append("<table>")

        For Each row As DataRow In Pengumuman.Rows
            Test.Append("<tr>")
            For Each column As DataColumn In Pengumuman.Columns
                Test.Append("<td>")
                Test.Append(i & ".")
                Test.Append("<td>")

                Test.Append("<td>")
                Test.Append(row(column.ColumnName))
                Test.Append("</td>")
            Next
            i = i + 1
            Test.Append("</tr>")
        Next

        Test.Append("</table>")

        run_table.InnerHtml = Test.ToString()
    End Sub

    Private Sub LoadBadan()
        Dim Test As New StringBuilder()
        Dim i As Integer = 1

        strSQL = "SELECT Pengumuman from master_pengumuman where Jenis_Kokurikulum = 'Badan Beruniform'"

        Dim Pengumuman As New DataTable
        Dim SQL_Pengumuman As New SqlDataAdapter(strSQL, strConn)

        Try
            SQL_Pengumuman.Fill(Pengumuman)
        Catch ex As Exception
        End Try

        Test.Append("<table>")

        For Each row As DataRow In Pengumuman.Rows
            Test.Append("<tr>")
            For Each column As DataColumn In Pengumuman.Columns
                Test.Append("<td>")
                Test.Append(i & ".")
                Test.Append("<td>")

                Test.Append("<td>")
                Test.Append(row(column.ColumnName))
                Test.Append("</td>")
            Next
            i = i + 1
            Test.Append("</tr>")
        Next

        Test.Append("</table>")

        run_badan.InnerHtml = Test.ToString()
    End Sub

    Private Sub LoadSukan()
        Dim Test As New StringBuilder()
        Dim i As Integer = 1

        strSQL = "SELECT Pengumuman from master_pengumuman where Jenis_Kokurikulum = 'Sukan Dan Permainan'"

        Dim Pengumuman As New DataTable
        Dim SQL_Pengumuman As New SqlDataAdapter(strSQL, strConn)

        Try
            SQL_Pengumuman.Fill(Pengumuman)
        Catch ex As Exception
        End Try

        Test.Append("<table>")

        For Each row As DataRow In Pengumuman.Rows
            Test.Append("<tr>")
            For Each column As DataColumn In Pengumuman.Columns
                Test.Append("<td>")
                Test.Append(i & ".")
                Test.Append("<td>")

                Test.Append("<td>")
                Test.Append(row(column.ColumnName))
                Test.Append("</td>")
            Next
            i = i + 1
            Test.Append("</tr>")
        Next

        Test.Append("</table>")

        run_sukan.InnerHtml = Test.ToString()
    End Sub

    Private Sub LoadPageDokumen()
        Dim Test As New StringBuilder()
        Dim i As Integer = 1

        strSQL = "SELECT DokumenName from koko_content"

        Dim Pengumuman As New DataTable
        Dim SQL_Pengumuman As New SqlDataAdapter(strSQL, strConn)

        Try
            SQL_Pengumuman.Fill(Pengumuman)
        Catch ex As Exception
        End Try

        Test.Append("<table>")

        For Each row As DataRow In Pengumuman.Rows
            Test.Append("<tr>")
            For Each column As DataColumn In Pengumuman.Columns
                Test.Append("<td>")
                Test.Append(i & ".")
                Test.Append("<td>")

                Test.Append("<td>")
                Test.Append("<a href='http://kokoadmin.permatapintar.edu.my/dokumen/" & row(column.ColumnName) & "' target='_blank'>" & row(column.ColumnName) & "</a>")
                Test.Append("</td>")
            Next
            i = i + 1
            Test.Append("</tr>")
        Next

        Test.Append("</table>")

        run_dokumen.InnerHtml = Test.ToString()
    End Sub

End Class