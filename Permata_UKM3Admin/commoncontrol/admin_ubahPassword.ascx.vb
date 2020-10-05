Imports System.Data.SqlClient

Public Class admin_ubahPassword
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUKM")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                userprofile_load()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub userprofile_load()
        strSQL = "SELECT A.staff_name,A.staff_login,A.staff_Password FROM ukm3.dbo.staff_info A Where A.stf_id='" & getUserID() & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_name")) Then
                    txtFullname.Text = ds.Tables(0).Rows(0).Item("staff_name")
                Else
                    txtFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_login")) Then
                    txtLoginID.Text = ds.Tables(0).Rows(0).Item("staff_login")
                Else
                    txtLoginID.Text = ""
                End If

                Dim strPwd As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Password")) Then
                    strPwd = ds.Tables(0).Rows(0).Item("staff_Password")
                Else
                    strPwd = ""
                End If
                '--display actual password
                If strPwd.Length > 0 Then
                    txtPwd.Text = strPwd
                End If


            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":userprofile_load:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Function getUserID() As String

        strSQL = "select stf_id from ukm3.dbo.staff_info where staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet

    End Function

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''--UPDATE UKM1 also
        strSQL = "UPDATE staff_info SET 
                staff_name='" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "',staff_login='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'
                ,staff_password='" & txtPwd.Text & "' WHERE stf_id='" & getUserID() & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = " Succesfully update userprofile!"
        Else
            lblMsg.Text += " Update SchoolProfile fail:" & strRet
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Nama Pengguna"
            txtFullname.Focus()
            Return False
        End If

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Login ID"
            txtLoginID.Focus()
            Return False
        End If

        ''changes made on the login id. its unique
        If Not txtLoginID.Text = lblLoginID.Text Then
            strSQL = "SELECT * FROM UserProfile WHERE LoginID='" & txtLoginID.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Login ID sudah digunakan. Sila gunakan Login ID yang lain."
                Return False
            End If
        End If

        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Password"
            txtPwd.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("admin.default.aspx")

    End Sub

End Class