Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class rumahsukan_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_tahun_list()

                koko_rumahsukan_load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_tahun_list()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

            'ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_rumahsukan_load()
        strSQL = "SELECT * FROM koko_rumahsukan WHERE RumahsukanID=" & Request.QueryString("rumahsukanid")
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
                    ddlTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlTahun.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("RumahSukan")) Then
                    txtRumahsukan.Text = ds.Tables(0).Rows(0).Item("RumahSukan")
                Else
                    txtRumahsukan.Text = ""
                End If
                lblRumahsukanOrg.Text = txtRumahsukan.Text

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Hari")) Then
                    txtHari.Text = ds.Tables(0).Rows(0).Item("Hari")
                Else
                    txtHari.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Masa")) Then
                    txtMasa.Text = ds.Tables(0).Rows(0).Item("Masa")
                Else
                    txtMasa.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tempat")) Then
                    txtTempat.Text = ds.Tables(0).Rows(0).Item("Tempat")
                Else
                    txtTempat.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'check form validation. if failed exit
        If ValidateForm() = False Then
            Exit Sub
        End If

        'UPDATE
        strSQL = "UPDATE koko_rumahsukan SET Tahun='" & ddlTahun.Text & "',Rumahsukan='" & oCommon.FixSingleQuotes(txtRumahsukan.Text.ToUpper) & "',Hari='" & oCommon.FixSingleQuotes(txtHari.Text.ToUpper) & "',Masa='" & oCommon.FixSingleQuotes(txtMasa.Text.ToUpper) & "',Tempat='" & oCommon.FixSingleQuotes(txtTempat.Text.ToUpper) & "' WHERE RumahsukanID=" & Request.QueryString("rumahsukanid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Kemaskini berjaya!"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtRumahsukan.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtRumahsukan.Focus()
            Return False
        End If

        '--change made
        If Not lblRumahsukanOrg.Text = txtRumahsukan.Text Then
            ''--check if already exist
            strSQL = "SELECT Rumahsukan FROM koko_rumahsukan WHERE Tahun='" & ddlTahun.Text & "' AND Rumahsukan='" & oCommon.FixSingleQuotes(txtRumahsukan.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Telah digunakan."
                Return False
            End If
        Else
            lblMsg.Text = "Tiada Perubahan."
        End If

        Return True
    End Function

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.rumahsukan.list.aspx")

    End Sub

End Class