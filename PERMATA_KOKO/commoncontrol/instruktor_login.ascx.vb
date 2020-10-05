Imports System.Globalization
Imports System.Resources

Public Class instruktor_login
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim strUserType As String
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        '--validate page form
        If ValidatePage() = False Then
            Exit Sub
        End If

        strSQL = "SELECT InstruktorID FROM koko_instruktor WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oDes.EncryptData(txtPwd.Text) & "' AND Tahun='" & lblTahun.Text & "'"
        '--debug
        'Response.Write(strSQL)
        If oCommon.isExist(strSQL) = True Then
            ''keep loginid of current user
            Session("koko_loginid") = txtLoginID.Text
            Session("koko_year") = lblTahun.Text

            Response.Cookies("koko_usertype").Value = "INSTRUKTOR"
            Response.Cookies("koko_culture").Value = "ms-MY"

            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "KOKO-LOGIN", "NA")
            Response.Redirect("instruktor/instruktor.login.succcess.aspx")
        Else
            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "KOKO_LOGIN_FAILED", "NA")
            lblMsg.Text = "Kesilapan Login ID atau Kata Laluan. Sila cuba lagi..."
        End If


    End Sub

    Private Function ValidatePage() As Boolean

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan data ke dalam medan ini!"
            txtLoginID.Focus()
            Return False
        End If

        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan data ke dalam medan ini!"
            txtPwd.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub lnkForgot_Click(sender As Object, e As EventArgs) Handles lnkForgot.Click
        Response.Redirect("instruktor.forgot.aspx")

    End Sub
End Class