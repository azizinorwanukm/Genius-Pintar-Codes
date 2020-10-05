Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class config_update
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
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

                koko_config_load()
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

    Private Sub koko_config_load()
        strSQL = "SELECT * FROM koko_config WHERE ConfigID=" & Request.QueryString("configid")
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ConfigCODE")) Then
                    txtConfigCODE.Text = ds.Tables(0).Rows(0).Item("ConfigCODE")
                Else
                    txtConfigCODE.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ConfigString")) Then
                    txtConfigString.Text = ds.Tables(0).Rows(0).Item("ConfigString")
                Else
                    txtConfigString.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ConfigDesc")) Then
                    txtConfigDesc.Text = ds.Tables(0).Rows(0).Item("ConfigDesc")
                Else
                    txtConfigDesc.Text = ""
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
        strSQL = "UPDATE koko_config SET Tahun='" & ddlTahun.Text & "',ConfigCODE='" & oCommon.FixSingleQuotes(txtConfigCODE.Text.ToUpper) & "',ConfigString='" & oCommon.FixSingleQuotes(txtConfigString.Text.ToUpper) & "',ConfigDesc='" & oCommon.FixSingleQuotes(txtConfigDesc.Text.ToUpper) & "' WHERE ConfigID=" & Request.QueryString("configid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Kemaskini berjaya!"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtConfigCODE.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtConfigCODE.Focus()
            Return False
        End If
        If txtConfigString.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtConfigString.Focus()
            Return False
        End If
        Return True
    End Function

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.config.list.aspx?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub


End Class