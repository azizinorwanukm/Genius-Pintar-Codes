Imports System.Globalization
Imports System.Threading
Imports System.Resources

Public Class pensyarah_login1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim strUserType As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        '--validate page form
        If ValidatePage() = False Then
            Exit Sub
        End If

        strSQL = "SELECT PensyarahID FROM koko_pensyarah WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "'"
        '--debug
        'Response.Write(strSQL)
        If oCommon.isExist(strSQL) = True Then
            ''keep loginid of current user
            Session("koko_loginid") = txtLoginID.Text

            Response.Cookies("kokoadmin_usertype").Value = "PENSYARAH"

            '--set default language BM
            Response.Cookies("koko_culture").Value = "ms-MY"

            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "KOKO-LOGIN", "NA")

            ''get usertype
            Response.Redirect("pensyarah/pensyarah.login.succcess.aspx")
        End If

        
        lblMsg.Text = "Kesilapan Login ID atau Kata Laluan. Sila cuba lagi..."

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

End Class