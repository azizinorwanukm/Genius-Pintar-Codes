Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_select
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

                uniform_list()
                ddlUniform.SelectedValue = "PILIH"

                persatuan_list()
                ddlPersatuan.SelectedValue = "PILIH"

                sukan_list()
                ddlSukan.SelectedValue = "PILIH"

                '--load default value if any
                koko_pelajar_koko_load()

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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

    Private Sub uniform_list()
        strSQL = "SELECT Uniform FROM koko_uniform WHERE Tahun='" & ddlTahun.Text & "' ORDER BY Uniform ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniform.DataSource = ds
            ddlUniform.DataTextField = "Uniform"
            ddlUniform.DataValueField = "Uniform"
            ddlUniform.DataBind()

            ddlUniform.Items.Add(New ListItem("--PILIH--", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub persatuan_list()
        strSQL = "SELECT Persatuan FROM koko_persatuan WHERE Tahun='" & ddlTahun.Text & "' ORDER BY Persatuan ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPersatuan.DataSource = ds
            ddlPersatuan.DataTextField = "Persatuan"
            ddlPersatuan.DataValueField = "Persatuan"
            ddlPersatuan.DataBind()

            ddlPersatuan.Items.Add(New ListItem("--PILIH--", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub sukan_list()
        strSQL = "SELECT Sukan FROM koko_sukan WHERE Tahun='" & ddlTahun.Text & "' ORDER BY Sukan ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSukan.DataSource = ds
            ddlSukan.DataTextField = "Sukan"
            ddlSukan.DataValueField = "Sukan"
            ddlSukan.DataBind()

            ddlSukan.Items.Add(New ListItem("--PILIH--", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_pelajar_koko_load()
        strSQL = "SELECT * FROM koko_pelajar WHERE StudentID='" & Request.QueryString("studentid") & "' AND Tahun='" & ddlTahun.Text & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Uniform")) Then
                    ddlUniform.Text = ds.Tables(0).Rows(0).Item("Uniform")
                Else
                    ddlUniform.SelectedValue = "PILIH"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Persatuan")) Then
                    ddlPersatuan.Text = ds.Tables(0).Rows(0).Item("Persatuan")
                Else
                    ddlPersatuan.SelectedValue = "PILIH"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sukan")) Then
                    ddlSukan.Text = ds.Tables(0).Rows(0).Item("Sukan")
                Else
                    ddlSukan.SelectedValue = "PILIH"
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '--must select
        If ValidateForm() = False Then
            Exit Sub
        End If

        strSQL = "UPDATE koko_pelajar SET Uniform='" & ddlUniform.Text & "',Persatuan='" & ddlPersatuan.Text & "',Sukan='" & ddlSukan.Text & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND Tahun='" & ddlTahun.Text & "'"
        '--debug
        Response.Write(strSQL)
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini rekod pelajar."
        Else
            lblMsg.Text = "Gagal mengemaskini rekod pleajar. " & strRet
        End If

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If ddlUniform.SelectedValue = "PILIH" Then
            lblMsg.Text = "Medan ini mesti dipilih."
            ddlUniform.Focus()
            Return False
        End If

        If ddlPersatuan.SelectedValue = "PILIH" Then
            lblMsg.Text = "Medan ini mesti dipilih."
            ddlUniform.Focus()
            Return False
        End If

        If ddlSukan.SelectedValue = "PILIH" Then
            lblMsg.Text = "Medan ini mesti dipilih."
            ddlUniform.Focus()
            Return False
        End If

        Return True
    End Function

End Class