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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strHour As String = DateTime.Now.ToString("HH")
        Dim strHourLimit As String = ConfigurationManager.AppSettings("HourLimit")

        Try
            If Not strHourLimit = "0" Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Disebabkan server mengalami peningkatan penggunaan oleh pelajar secara mendadak, pihak kami terpaksa menghadkan penggunaan Portal KPM selepas jam [" & strHourLimit & "] sahaja.<br/>Kesulitan amat-amat dikesali. Harap Maklum. Jam sekarang [" & strHour & "]"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim strUserType As String = ""
        Dim strHour As String = DateTime.Now.ToString("HH")
        Dim strHourLimit As String = ConfigurationManager.AppSettings("HourLimit")

        '--limit penggunaan KPM
        If Not strHourLimit = "0" Then
            ''dibenarkan selepas 7malam sahaja
            If isAlllow() = False Then
                lblMsg.Text = "Disebabkan server mengalami peningkatan penggunaan oleh pelajar secara mendadak, pihak kami terpaksa menghadkan penggunaan Portal KPM selepas jam [" & strHourLimit & "] sahaja.<br/>Kesulitan amat-amat dikesali. Harap Maklum. Jam sekarang [" & strHour & "]"
                Exit Sub
            End If
        End If

        ''--sqlinjection
        If oCommon.CheckSqlInjection(txtLoginID.Text) = True Then
            Response.Redirect("ukm1.invalid.url.aspx?lang=" & Request.QueryString("lang"), False)
        End If
        If oCommon.CheckSqlInjection(txtPwd.Text) = True Then
            Response.Redirect("ukm1.invalid.url.aspx?lang=" & Request.QueryString("lang"), False)
        End If

        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "'"
        If oCommon.isExist(strSQL) = True Then
            ''keep loginid of current user
            Response.Cookies("ukmkpm_loginid").Value = txtLoginID.Text
            Response.Cookies("ukmkpm_loginid").Expires = DateTime.Now.AddDays(1)

            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

            strUserType = getUserProfile_UserType()
            Select Case strUserType
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
                    Response.Redirect("system.error.aspx?msg=Invalid user type: " & strUserType)
            End Select

        End If

        divMsg.Attributes("class") = "error"
        lblMsg.Text = "Invalid Login ID or Password! Please re-try."
    End Sub

    Private Function isAlllow() As Boolean
        Dim strHour As String = DateTime.Now.ToString("HH") 'return 24 hours format.
        Dim strHourLimit As String = ConfigurationManager.AppSettings("HourLimit")

        If Not strHourLimit = "0" Then
            If CInt(strHour) < CInt(strHourLimit) Then
                Return False
            End If
        End If

        Return True
    End Function

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

End Class