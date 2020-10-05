Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class takwim_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_takwim_load()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub koko_takwim_load()
        strSQL = "SELECT * FROM koko_takwim WHERE TakwimID=" & Request.QueryString("takwimid")
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kategori")) Then
                    lblKategori.Text = ds.Tables(0).Rows(0).Item("Kategori")
                Else
                    lblKategori.Text = ""
                End If

                Dim strTarikh As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tarikh")) Then
                    strTarikh = ds.Tables(0).Rows(0).Item("Tarikh")
                Else
                    strTarikh = ""
                End If
                lblTarikh.Text = oCommon.DateFormat(strTarikh, "dddd dd-MM-yyyy")

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Masa")) Then
                    lblMasa.Text = ds.Tables(0).Rows(0).Item("Masa")
                Else
                    lblMasa.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tempat")) Then
                    lblTempat.Text = ds.Tables(0).Rows(0).Item("Tempat")
                Else
                    lblTempat.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Title")) Then
                    lblTitle.Text = ds.Tables(0).Rows(0).Item("Title")
                Else
                    lblTitle.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Catatan")) Then
                    ltCatatan.Text = ds.Tables(0).Rows(0).Item("Catatan").ToString.Replace(Environment.NewLine, "<br />")
                Else
                    ltCatatan.Text = ""
                End If

            End If

        Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub


End Class