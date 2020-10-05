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

    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click


        Dim Active As String = "Select Active from pcis_admin where LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        strRet = oCommon.getFieldValue(Active)

        If strRet = "Y" Then
            '' User Profile Active

            strSQL = "SELECT UserType FROM pcis_admin WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                ''keep loginid of current user
                Response.Cookies("pcis_admin").Value = txtLoginID.Text
                Response.Cookies("pcis_admin").Expires = DateTime.Now.AddDays(30)

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case get_pcis_admin_UserType()
                    Case "ADMIN"
                        Response.Redirect("admin.default.aspx")
                    Case "SUBADMIN"
                        Response.Redirect("subadmin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & get_pcis_admin_UserType()
                End Select
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Invalid Login ID or Password! Please re-try."
            End If

        ElseIf strRet = "N" Then
            '' User Profile Not Active

            lblMsg.Text = "Error Login ID.. Login ID is not active"

        End If

    End Sub

    Private Function get_pcis_admin_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM pcis_admin WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

End Class