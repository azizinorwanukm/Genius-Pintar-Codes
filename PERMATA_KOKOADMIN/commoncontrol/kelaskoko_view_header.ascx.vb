Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class kelaskoko_view_header
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                getKOKODetail()
                koko_sukan_load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    '--getKOKODetail
    Private Sub getKOKODetail()
        strSQL = "SELECT KokoID FROM koko_kelaskoko WHERE kelaskokoid=" & Request.QueryString("kelaskokoid")
        lblKokoID.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Nama FROM koko_kolejpermata WHERE KokoID=" & lblKokoID.Text
        lblKOKOName.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Tahun FROM koko_kolejpermata WHERE KokoID=" & lblKokoID.Text
        lblTahun.Text = oCommon.getFieldValue(strSQL)
    End Sub

    Private Sub koko_sukan_load()
        strSQL = "SELECT * FROM koko_kelaskoko WHERE KelasKokoID=" & Request.QueryString("kelaskokoid")
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kelas")) Then
                    lblKelas.Text = ds.Tables(0).Rows(0).Item("Kelas")
                Else
                    lblKelas.Text = ""
                End If

            End If

        Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class