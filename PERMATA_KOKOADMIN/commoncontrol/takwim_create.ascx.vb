Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class takwim_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--refresh
                ClearScreen()

                koko_tahun_list()
                ddlTahun.Text = Request.QueryString("tahun")

                koko_kategori_list()

                '--set default date
                calTarikh.SelectedDate = Now.Date
                txtTarikh.Text = calTarikh.SelectedDate.ToString("dddd dd-MM-yyyy")
                lblTarikh.Text = calTarikh.SelectedDate.ToString("yyyy-MM-dd")
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtTarikh.Text = ""
        txtMasa.Text = ""
        txtTempat.Text = ""
        txtTitle.Text = ""
        txtCatatan.Text = ""

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

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "INSERT INTO koko_takwim(Tahun,Kategori,Tarikh,Masa,Tempat,Title,Catatan) VALUES ('" & ddlTahun.Text & "','" & ddlKategori.Text & "','" & lblTarikh.Text & "','" & oCommon.FixSingleQuotes(txtMasa.Text) & "','" & oCommon.FixSingleQuotes(txtTempat.Text) & "','" & oCommon.FixSingleQuotes(txtTitle.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtCatatan.Text) & "')"
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

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.takwim.list.aspx?tahun=" & Request.QueryString("tahun") & "&admin_ID=" & Request.QueryString("admin_ID"))
            Case "INSTRUKTOR"
            Case "PENSYARAH"
            Case "PENGARAH"
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

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

    Private Sub calTarikh_SelectionChanged(sender As Object, e As EventArgs) Handles calTarikh.SelectionChanged
        txtTarikh.Text = calTarikh.SelectedDate.ToString("dddd dd-MM-yyyy")
        lblTarikh.Text = calTarikh.SelectedDate.ToString("yyyy-MM-dd")

        calTarikh.Visible = False
    End Sub

End Class