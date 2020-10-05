Imports System.Data.SqlClient
Imports System.Globalization
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
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Function isExistL(ByVal strSQL As String) As String
        If strSQL.Length = 0 Then
            Return False
        End If
        ''If isBlockText(strSQL) = True Then
        ''    Return False
        ''End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            If ds.Tables(0).Rows.Count > 0 Then
                lblDebug.Text = "OK:" & strConn
                Return True
            Else
                lblDebug.Text = "NOK:" & strConn
                Return False
            End If

        Catch ex As Exception
            lblDebug.Text = "Err:" & ex.Message
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oDes.EncryptData(txtPwd.Text) & "'"
        'isExistL(strSQL)
        'Exit Sub
        displayDebug(strSQL)
        If oCommon.isExist(strSQL) = True Then
            ''keep loginid of current user
            'Response.Cookies("permata_admin").Value = txtLoginID.Text
            'Response.Cookies("permata_admin").Expires = DateTime.Now.AddDays(30)

            Session("permata_admin") = txtLoginID.Text

            '--set default language BM
            Response.Cookies("ppcs_culture").Value = "ms-MY"

            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

            ''get usertype
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.default.aspx")
                Case "SUBADMIN"
                    Response.Redirect("subadmin.default.aspx")
                Case "ADMINOP"
                    Response.Redirect("adminop/default.aspx")
                Case "KPM"
                    lblMsg.Text = "Login via KPM site. http://kpm.permatapintar.edu.my"
                Case "JPN"
                    lblMsg.Text = "Login via KPM site. http://kpm.permatapintar.edu.my"
                Case "MRSM"
                    lblMsg.Text = "Login via KPM site. http://kpm.permatapintar.edu.my"
                Case "KPT"
                    lblMsg.Text = "Login via KPM site. http://kpm.permatapintar.edu.my"
                Case "ASASI"
                    lblMsg.Text = "Login via KPM site. http://kpm.permatapintar.edu.my"
                Case "UKM"
                    lblMsg.Text = "Login via KPM site. http://kpm.permatapintar.edu.my"
                Case Else
                    lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
            End Select
        Else
            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Invalid Login ID or Password! Please re-try."
        End If

    End Sub

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

End Class