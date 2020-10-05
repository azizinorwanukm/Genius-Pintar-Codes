Imports System.Globalization
Imports System.Threading
Imports System.Resources

Public Class pelajar_login1
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

        strSQL = "SELECT StudentProfileID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        '--debug
        'Response.Write(strSQL)
        If oCommon.isExist(strSQL) = True Then
            ''keep loginid of current user
            Session("koko_loginid") = txtMYKAD.Text

            Response.Cookies("kokoadmin_usertype").Value = "PELAJAR"

            '--set default language BM
            Response.Cookies("koko_culture").Value = "ms-MY"

            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtMYKAD.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "KOKO-LOGIN", "NA")

            ''get usertype
            Response.Redirect("pelajar/pelajar.login.succcess.aspx")
        End If

        
        lblMsg.Text = "Kesilapan Login ID atau Kata Laluan. Sila cuba lagi..."

    End Sub

    Private Function ValidatePage() As Boolean
        

        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan data ke dalam medan ini!"
            
            txtMYKAD.Focus()
            Return False
        End If

        '--check if exist as pelajar koko for the defaultyear
        Dim strStudentID As String = ""
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        strStudentID = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT StudentID FROM koko_pelajar WHERE StudentID='" & strStudentID & "' AND Tahun='" & lblTahun.Text & "' AND StatusTawaran='TERIMA'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Rekod anda tidak terdapat di dalam senarai KOKO Tahun:" & lblTahun.Text
            Return False
        End If

        
        Return True
    End Function


End Class