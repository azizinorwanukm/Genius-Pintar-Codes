Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class takwim_update
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

                koko_kategori_list()

                koko_takwim_load()
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

    Private Sub koko_kategori_list()
        strSQL = "SELECT Kategori FROM koko_kategori ORDER BY KategoriID ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKategori.DataSource = ds
            ddlKategori.DataTextField = "Kategori"
            ddlKategori.DataValueField = "Kategori"
            ddlKategori.DataBind()

            'ddlKategori.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
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
                    ddlTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlTahun.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kategori")) Then
                    ddlKategori.Text = ds.Tables(0).Rows(0).Item("Kategori")
                Else
                    ddlKategori.Text = ""
                End If

                Dim strTarikh As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tarikh")) Then
                    strTarikh = ds.Tables(0).Rows(0).Item("Tarikh")
                Else
                    strTarikh = ""
                End If
                txtTarikh.Text = oCommon.DateFormat(strTarikh, "dddd dd-MM-yyyy")
                lblTarikh.Text = oCommon.DateFormat(strTarikh, "yyyy-MM-dd")

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Title")) Then
                    txtTitle.Text = ds.Tables(0).Rows(0).Item("Title")
                Else
                    txtTitle.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Catatan")) Then
                    txtCatatan.Text = ds.Tables(0).Rows(0).Item("Catatan")
                Else
                    txtCatatan.Text = ""
                End If

            End If

        Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.takwim.list.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'check form validation. if failed exit
        If ValidateForm() = False Then
            Exit Sub
        End If

        'UPDATE
        strSQL = "UPDATE koko_takwim SET Tahun='" & ddlTahun.Text & "',Kategori='" & ddlKategori.Text & "',Tarikh='" & lblTarikh.Text & "',Masa='" & oCommon.FixSingleQuotes(txtMasa.Text) & "',Tempat='" & oCommon.FixSingleQuotes(txtTempat.Text) & "',Title='" & oCommon.FixSingleQuotes(txtTitle.Text.ToUpper) & "',Catatan='" & oCommon.FixSingleQuotes(txtCatatan.Text) & "' WHERE TakwimID=" & Request.QueryString("takwimid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Kemaskini berjaya!"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub


    '--CHECK form validation.
    Private Function ValidateForm() As Boolean
        If txtTarikh.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtTarikh.Focus()
            Return False
        End If

        If txtMasa.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtMasa.Focus()
            Return False
        End If

        If txtTempat.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtTempat.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub calTarikh_SelectionChanged(sender As Object, e As EventArgs) Handles calTarikh.SelectionChanged
        txtTarikh.Text = calTarikh.SelectedDate.ToString("dddd dd-MM-yyyy")
        lblTarikh.Text = calTarikh.SelectedDate.ToString("yyyy-MM-dd")

        calTarikh.Visible = False
    End Sub

    Private Sub btnDate_Click(sender As Object, e As ImageClickEventArgs) Handles btnDate.Click
        Dim [date] As New DateTime()
        'Flip the visibility attribute
        calTarikh.Visible = Not (calTarikh.Visible)
        'If the calendar is visible try assigning the date from the textbox
        If calTarikh.Visible Then
            'If the Conversion was successfull assign the textbox's date
            If DateTime.TryParse(txtTarikh.Text, [date]) Then
                calTarikh.SelectedDate = [date]
            End If
            calTarikh.Attributes.Add("style", "POSITION: absolute")
        End If

    End Sub
End Class