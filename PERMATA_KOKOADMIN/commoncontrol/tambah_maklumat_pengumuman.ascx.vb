Imports System.Data.SqlClient

Public Class tambah_maklumat_pengumuman
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                list_koko()
                txtPengumuman.InnerText = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If ddlKokurikulum.SelectedValue = "Semua" Then
            Using STDDATA As New SqlCommand("INSERT INTO master_pengumuman(Pengumuman, Jenis_Kokurikulum) values ('" & txtPengumuman.InnerText & "', 'Kelab Dan Persatuan')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()

                If i <> 0 Then
                    lblMsg.Text = "Berjaya Tambah Pengummuman Baru "
                Else
                    lblMsg.Text = "Error!! Tidak Berjaya Tambah Pengummuman Baru "
                End If
            End Using

            Using STDDATA As New SqlCommand("INSERT INTO master_pengumuman(Pengumuman, Jenis_Kokurikulum) values ('" & txtPengumuman.InnerText & "', 'Badan Beruniform')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()

                If i <> 0 Then
                    lblMsg.Text = "Berjaya Tambah Pengummuman Baru "
                Else
                    lblMsg.Text = "Error!! Tidak Berjaya Tambah Pengummuman Baru "
                End If
            End Using

            Using STDDATA As New SqlCommand("INSERT INTO master_pengumuman(Pengumuman, Jenis_Kokurikulum) values ('" & txtPengumuman.InnerText & "', 'Sukan Dan Permainan')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()

                If i <> 0 Then
                    lblMsg.Text = "Berjaya Tambah Pengummuman Baru "
                Else
                    lblMsg.Text = "Error!! Tidak Berjaya Tambah Pengummuman Baru "
                End If
            End Using
        Else
            Using STDDATA As New SqlCommand("INSERT INTO master_pengumuman(Pengumuman, Jenis_Kokurikulum) values ('" & txtPengumuman.InnerText & "', '" & ddlKokurikulum.SelectedValue & "')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()

                If i <> 0 Then
                    lblMsg.Text = "Berjaya Tambah Pengummuman Baru "
                Else
                    lblMsg.Text = "Error!! Tidak Berjaya Tambah Pengummuman Baru "
                End If
            End Using
        End If

    End Sub

    Private Sub list_koko()
        strSQL = "SELECT * FROM koko_tahun where Tahun = 'God_Error'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKokurikulum.DataSource = ds
            ddlKokurikulum.DataTextField = "Tahun"
            ddlKokurikulum.DataValueField = "Tahun"
            ddlKokurikulum.DataBind()
            ddlKokurikulum.Items.Insert(0, New ListItem("Pilih Kokurikulum", String.Empty))
            ddlKokurikulum.Items.Insert(1, New ListItem("Kelab Dan Persatuan", "Kelab Dan Persatuan"))
            ddlKokurikulum.Items.Insert(2, New ListItem("Badan Beruniform", "Badan Beruniform"))
            ddlKokurikulum.Items.Insert(3, New ListItem("Sukan Dan Permainan", "Sukan Dan Permainan"))
            ddlKokurikulum.Items.Insert(4, New ListItem("Semua", "Semua"))
            ddlKokurikulum.SelectedIndex = 0

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("admin.kemaskini.maklumat.pengumuman.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub
End Class