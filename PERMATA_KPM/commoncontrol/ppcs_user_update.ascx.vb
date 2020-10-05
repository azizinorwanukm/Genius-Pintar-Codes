Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class ppcs_user_update
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                Load_Details()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            ' oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub

    Private Sub Load_Details()
        strSQL = "SELECT * FROM ppcs_users WHERE myGUID='" & Request.QueryString("myguid") & "'"
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
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    txtLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    txtLoginID.Text = ""
                End If
                ''--to compare changes
                txtOldLoginID.Text = txtLoginID.Text

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    txtPwd.Text = ds.Tables(0).Rows(0).Item("Pwd")
                    txtPwdVerify.Text = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    txtPwd.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    txtFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    txtFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContactNo")) Then
                    txtContactNo.Text = ds.Tables(0).Rows(0).Item("ContactNo")
                Else
                    txtContactNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ICNo")) Then
                    txtICNo.Text = ds.Tables(0).Rows(0).Item("ICNo")
                Else
                    txtICNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Address")) Then
                    txtAddress.Text = ds.Tables(0).Rows(0).Item("Address")
                Else
                    txtAddress.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Postcode")) Then
                    txtPostcode.Text = ds.Tables(0).Rows(0).Item("Postcode")
                Else
                    txtPostcode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("City")) Then
                    txtCity.Text = ds.Tables(0).Rows(0).Item("City")
                Else
                    txtCity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("State")) Then
                    txtState.Text = ds.Tables(0).Rows(0).Item("State")
                Else
                    txtState.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert in to user, not active
            strSQL = "UPDATE PPCS_Users WITH (UPDLOCK) SET LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "',Pwd='" & oDes.EncryptData(txtPwd.Text) & "',Fullname='" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "',ICNo='" & oCommon.FixSingleQuotes(txtICNo.Text) & "',ContactNo='" & oCommon.FixSingleQuotes(txtContactNo.Text) & "',Address='" & oCommon.FixSingleQuotes(txtAddress.Text.ToUpper) & "',Postcode='" & oCommon.FixSingleQuotes(txtPostcode.Text) & "',City='" & oCommon.FixSingleQuotes(txtCity.Text) & "',State='" & oCommon.FixSingleQuotes(txtState.Text) & "' WHERE myGUID='" & Request.QueryString("myguid") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "error:" & strRet
            Else
                lblMsg.Text = "Berjaya dikemaskini."
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

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "LoginID/Email perlu di isi."
            txtLoginID.Focus()
            Return False
        End If

        If Not txtLoginID.Text = txtOldLoginID.Text Then
            strSQL = "SELECT LoginID FROM ppcs_users WHERE LoginID='" & txtLoginID.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Login ID sudah ujud. Masukkan Login ID yang lain."
                txtLoginID.Focus()
                Return False
            End If
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

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nama."
            txtFullname.Focus()
            Return False
        End If

        If txtICNo.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor IC."
            txtICNo.Focus()
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

    Protected Sub lnkppcsuserlist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkppcsuserlist.Click
        Response.Redirect("admin.ppcs.user.list.aspx")

    End Sub

End Class