Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class config_create
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
                ddlTahun.Text = Request.QueryString("tahun")

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

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "INSERT INTO koko_config(ConfigCODE,ConfigString,ConfigDesc,Tahun) VALUES ('" & oCommon.FixSingleQuotes(txtConfigCODE.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtConfigString.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtConfigDesc.Text.ToUpper) & "','" & ddlTahun.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Penambahan berjaya!"
            Else
                lblMsg.Text = "Gagal. " & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

End Class