Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class user_login
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oDes.EncryptData(txtPwd.Text) & "'"
        If oCommon.isExist(strSQL) = True Then
            Session("kpmadmin_loginid") = txtLoginID.Text

            '--set default language BM
            Response.Cookies("ppcs_culture").Value = "ms-MY"

            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

            ''get usertype
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                Case "SUBADMIN"
                    lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                Case "KPM"
                    Response.Redirect("kpm.default.aspx")
                Case "JPN"
                    Response.Redirect("jpn.default.aspx")
                Case "MRSM"
                    Response.Redirect("mara.default.aspx")
                Case "KPT"
                    Response.Redirect("kpt.default.aspx")
                Case "ASASI"
                    Response.Redirect("asasi.default.aspx")
                Case "UKM"
                    Response.Redirect("ukm.default.aspx")
                Case Else
                    lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
            End Select
            divMsg.Attributes("class") = "error"
        Else
            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
            lblMsg.Text = "Invalid Login ID or Password! Please re-try."
        End If

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

End Class