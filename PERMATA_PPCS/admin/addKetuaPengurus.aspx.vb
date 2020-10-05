Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class addKetuaPengurus
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strUserType As String = "KETUA PENGURUS AKADEMIK"
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                ClearScreen()
            End If


        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    Private Sub ClearScreen()
        lblMsg.Text = ""

        lblTotal.Text = ""

        strSQL = "SELECT * from ppcs_users WHERE usertype='" & strUserType & "' ORDER BY loginid desc"
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Record not found!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        ClearScreen()

        nPageno = e.NewPageIndex + 1


    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        '--Response.Write(strKeyID)
        Response.Redirect("viewDetails.aspx?ppcsuserid=" & strKeyID)

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try

            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert in to user, not active
            strSQL = "INSERT INTO ppcs_users(Fullname,LoginID,Email,ICNo,Pwd,ContactNo,Address,UserType) VALUES ('" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & oCommon.FixSingleQuotes(txtICNo.Text) & "','" & oDes.EncryptData(oCommon.FixSingleQuotes(txtPwd.Text)) & "','" & oCommon.FixSingleQuotes(txtContactNo.Text) & "','" & oCommon.FixSingleQuotes(txtAddress.Text) & "','" & strUserType & "')"
            ''debug
            ''Response.Write(strSQL)
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "Gagal:" & strRet
            Else
                lblMsg.Text = "Berjaya menambah " & strUserType
                ClearScreen()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean
        strSQL = "SELECT LoginID FROM ppcs_users WHERE LoginID='" & txtLoginID.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Login ID sudah ujud. Masukkan Login ID yang lain."
            txtLoginID.Focus()
            Return False
        End If

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nama."
            txtFullname.Focus()
            Return False
        End If

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "Email perlu di isi."
            txtLoginID.Focus()
            Return False
        End If

        If txtICNo.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor IC."
            txtICNo.Focus()
            Return False
        End If


        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "Masukkan password."
            txtPwd.Focus()
            Return False
        End If

        If Not txtPwd.Text = txtPwdVerify.Text Then
            lblMsg.Text = "Password dimasukkan tidak sama."
            txtPwd.Focus()
            Return False

        End If

        If txtContactNo.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor telefon."
            txtContactNo.Focus()
            Return False
        End If

        If txtAddress.Text.Length = 0 Then
            lblMsg.Text = "Masukkan alamat terkini."
            txtAddress.Focus()
            Return False
        End If

        Return True
    End Function

End Class