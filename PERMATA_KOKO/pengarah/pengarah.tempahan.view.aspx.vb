Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class pengarah_tempahan_view
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_tempahandetail_load()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("pengarah.tempahan.search.aspx?penarah_ID=" & Request.QueryString("pengarah_ID"))

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        strSQL = "UPDATE koko_tempahandetail SET StatusTempahan='" & selStatusTempahan.Value & "',CatatanPengarah='" & oCommon.FixSingleQuotes(txtCatatanPengarah.Text.ToUpper) & "' WHERE TempahanID=" & Request.QueryString("tempahanid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA mengemaskini status tempahan."
        Else
            lblMsg.Text = "GAGAL mengemaskini status tempahan. Err:" & strRet
        End If

        '--refresh page
        'Response.Redirect(Request.RawUrl)

    End Sub

    Private Sub koko_tempahandetail_load()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderBy As String = ""

        tmpSQL = "SELECT * FROM koko_tempahandetail"
        strWhere = " WHERE TempahanID=" & Request.QueryString("tempahanid")
        strSQL = tmpSQL & strWhere & strOrderBy
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
                '--koko_kemudahandetail
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StatusTempahan")) Then
                    selStatusTempahan.Value = ds.Tables(0).Rows(0).Item("StatusTempahan")
                Else
                    selStatusTempahan.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CatatanPengarah")) Then
                    txtCatatanPengarah.Text = ds.Tables(0).Rows(0).Item("CatatanPengarah")
                Else
                    txtCatatanPengarah.Text = ""
                End If
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub
End Class