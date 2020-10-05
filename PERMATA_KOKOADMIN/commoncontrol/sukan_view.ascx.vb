Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class sukan_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                koko_sukan_load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_sukan_load()
        strSQL = "SELECT * FROM koko_sukan WHERE SukanID=" & Request.QueryString("sukanid")
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sukan")) Then
                    lblSukan.Text = ds.Tables(0).Rows(0).Item("Sukan")
                Else
                    lblSukan.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Hari")) Then
                    lblHari.Text = ds.Tables(0).Rows(0).Item("Hari")
                Else
                    lblHari.Text = ""
                End If
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("IsMandatory")) Then
                    lblIsMandatory.Text = ds.Tables(0).Rows(0).Item("IsMandatory")
                Else
                    lblIsMandatory.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Response.Redirect("admin.sukan.update.aspx?sukanid=" & Request.QueryString("sukanid"))

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        strSQL = "DELETE koko_sukan WHERE SukanID=" & Request.QueryString("sukanid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Rekod berjaya dihapuskan."
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.sukan.list.aspx")

    End Sub
End Class