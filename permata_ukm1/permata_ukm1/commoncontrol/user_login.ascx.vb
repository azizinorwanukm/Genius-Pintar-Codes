Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class user_login
    Inherits System.Web.UI.UserControl

    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("culture") = "ms-MY"

    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        ''--validate page
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''--get password
        Dim strPwd As String = ""
        strSQL = "Select Pwd FROM StudentProfile WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        strPwd = oCommon.getFieldValue(strSQL)

        If strPwd.Length = 0 Then
            ''--first time user
            strSQL = "Select LoginID FROM StudentProfile WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        Else
            ''--existing user and change password done
            strSQL = "Select LoginID FROM StudentProfile WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "'"
        End If

        If oCommon.isExist(strSQL) = True Then
            ''assign studentid cookies here. TODO
            Response.Redirect("default.main.aspx")
        Else
            lblMsg.Text = "MYKAD/MYKID/Surat Beranak tidak terdapat di dalam pengkalan data kami atau kata laluan salah."
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Login ID!"
            txtLoginID.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Response.Redirect("studentprofile.create.aspx")

    End Sub

End Class