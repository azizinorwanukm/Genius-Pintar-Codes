Imports System.Data.SqlClient
Imports System.IO

Public Class tambah_maklumat_dokumen
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("admin.kemaskini.maklumat.pengumuman.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

        ''sets the image path
        Dim imgPath As String = "~/dokumen/" + filename

        ''then save it to the Folder
        uploadPhoto.SaveAs(Server.MapPath(imgPath))

        Using STDDATA As New SqlCommand("INSERT INTO koko_content(DokumenName) values('" & filename & "')", objConn)
            objConn.Open()
            Dim i = STDDATA.ExecuteNonQuery()
            objConn.Close()

            If i <> 0 Then
                lblMsg.Text = "Berjaya Tambah Pengummuman Baru "
            Else
                lblMsg.Text = "Error!! Tidak Berjaya Tambah Pengummuman Baru "
            End If
        End Using
    End Sub

End Class