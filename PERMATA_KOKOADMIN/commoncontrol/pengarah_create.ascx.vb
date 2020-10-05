Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class pengarah_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--refresh
                ClearScreen()

                koko_state_list()
                ddlState.SelectedValue = ""

                '--selected year
                txtTahun.Text = Request.QueryString("tahun")
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

    Private Sub koko_state_list()
        strSQL = "SELECT State FROM koko_state ORDER BY State"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "State"
            ddlState.DataValueField = "State"
            ddlState.DataBind()

            ddlState.Items.Add(New ListItem("PILIH", ""))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtFullname.Text = ""
        txtMYKAD.Text = ""
        txtContactNo.Text = ""
        txtEmail.Text = ""

        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtPostcode.Text = ""
        txtCity.Text = ""

        txtLoginID.Text = ""
        txtPwd.Text = ""

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtFullname.Focus()
            Return False
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtMYKAD.Focus()
            Return False
        End If

        ''--check if already exist
        strSQL = "SELECT MYKAD FROM koko_pengarah WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "MYKAD Telah digunakan."
            Return False
        End If

        Return True
    End Function

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Try
            '--new GUID
            Dim strInstruktorID As String = oCommon.getGUID

            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "INSERT INTO koko_pengarah(PengarahID,Tahun,Fullname,ContactNo,Email,MYKAD,BankName,AcctNo,Address1,Address2,PostCode,City,State,LoginID,Pwd) VALUES ('" & strInstruktorID & "','" & txtTahun.Text & "','" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtContactNo.Text) & "','" & oCommon.FixSingleQuotes(txtEmail.Text) & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtBankName.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtAcctNo.Text) & "','" & oCommon.FixSingleQuotes(txtAddress1.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtAddress2.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtCity.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(ddlState.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & oDes.EncryptData(txtPwd.Text) & "')"
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

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.pengarah.list.aspx?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

End Class