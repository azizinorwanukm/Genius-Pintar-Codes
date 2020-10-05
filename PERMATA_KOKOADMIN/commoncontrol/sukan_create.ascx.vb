Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class sukan_create
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
                ddlTahun.Text = ConfigurationManager.AppSettings("DefaultYear")
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtSukan.Text = ""

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

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            strSQL = "INSERT INTO koko_sukan(Tahun,Sukan,Hari,Masa,Tempat,IsMandatory) VALUES ('" & ddlTahun.Text & "','" & oCommon.FixSingleQuotes(txtSukan.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtHari.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtMasa.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtTempat.Text.ToUpper) & "','" & selIsMandatory.Value & "')"
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

        If txtSukan.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtSukan.Focus()
            Return False
        End If

        ''--check if already exist
        strSQL = "SELECT Sukan FROM koko_sukan WHERE Tahun='" & ddlTahun.Text & "' AND Sukan='" & oCommon.FixSingleQuotes(txtSukan.Text) & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Telah digunakan."
            Return False
        End If

        Return True
    End Function


    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.sukan.list.aspx")

    End Sub

End Class