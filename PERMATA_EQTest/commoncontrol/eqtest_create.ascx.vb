Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net

Public Class eqtest_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                lblCompanyname.Text = oCommon.getAppsettings("EQTestName")
                lblSurveyID.Text = oCommon.getAppsettings("EQTest_SurveyID_Pub")

            End If

        Catch ex As Exception
            lblMsgNOK.Text = ex.Message
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim strRedirect As String = ""

        Try
            lblMsgOK.Text = ""
            lblMsgNOK.Text = ""

            If ValidatePage() = False Then
                Exit Sub
            End If

            strRedirect = EQTest_insert()
            '--debug
            'Response.Write(strRedirect)
            If Not strRedirect.Length = 0 Then
                Response.Redirect(strRedirect)
            Else
                lblMsgNOK.Text = "Fail. " & strRet
            End If

        Catch ex As Exception
            lblMsgNOK.Text = "error:" & ex.Message
        End Try
    End Sub

    Private Function EQTest_insert() As String
        Dim strDomainName As String = oCommon.getAppsettings("DomainEQTest")
        Dim strLink As String = ""
        Dim strCandidateLink As String = strDomainName & "home.candidate.aspx?culture=ms-MY"

        Dim strLoginID As String = oCommon.getGUID
        Dim strStudentID As String = strLoginID
        Dim strPPCSDate As String = oCommon.getAppsettings("DefaultPPCSDate")
        Dim strSurveyID As String = oCommon.getAppsettings("EQTest_SurveyID_Pub")
        Dim strUserType As String = "SELF"

        strLink = strCandidateLink & "&loginid=" & strLoginID & "&surveyid=" & strSurveyID

        ''--EQTest
        strSQL = "INSERT INTO EQTest (LoginID,StudentID,PPCSDate,SurveyID,Fullname,EmailAdd,Position,Division,Department,Companyname,Age) VALUES ('" & strLoginID & "','" & strStudentID & "','" & strPPCSDate & "','" & strSurveyID & "','" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtEmailAdd.Text) & "','" & oCommon.FixSingleQuotes(txtPosition.Text.ToUpper) & "','NA','NA','" & oCommon.FixSingleQuotes(lblCompanyname.Text) & "','" & oCommon.FixSingleQuotes(txtAge.Text) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            '--sendemail
            sendemail_html(strLink)
            Return strLink
        Else
            Return ""
        End If

    End Function

    Private Sub RefreshScreen()
        lblMsgNOK.Text = ""
        lblMsgOK.Text = ""

        txtFullname.Text = ""
        txtAge.Text = ""
        txtEmailAdd.Text = ""
        txtPosition.Text = ""

    End Sub

    Private Function ValidatePage() As Boolean

        If txtFullname.Text.Length = 0 Then
            lblMsgNOK.Text = "Tidak boleh dibiarkan kosong."
            txtFullname.Focus()
            Return False
        End If

        If txtEmailAdd.Text.Length = 0 Then
            lblMsgNOK.Text = "Tidak boleh dibiarkan kosong."
            txtEmailAdd.Focus()
            Return False
        End If

        If oCommon.isEmail(txtEmailAdd.Text) = False Then
            lblMsgNOK.Text = "Bukan format EMail."
            txtEmailAdd.Focus()
            Return False
        End If

        strSQL = "SELECT EmailAdd FROM EQTest WHERE IsDeleted='N' AND EmailAdd='" & oCommon.FixSingleQuotes(txtEmailAdd.Text) & "' AND SurveyID='" & lblSurveyID.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsgNOK.Text = "EMail sudah digunakan untuk EQTEST ini. Sila guna EMail lain."
            txtEmailAdd.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub sendemail_html(ByVal strLink As String)
        Try
            Dim strbody As String = ""
            Dim message As New MailMessage(ConfigurationManager.AppSettings("EmailFrom"), oCommon.FixSingleQuotes(txtEmailAdd.Text))
            Dim SmtpClient As New SmtpClient(ConfigurationManager.AppSettings("SmtpClient"), ConfigurationManager.AppSettings("SmtpClientPort"))
            message.Subject = "PERMATApintar Emotional Quotient Test"
            message.Bcc.Add(ConfigurationManager.AppSettings("EmailBCC"))   '--send a copy to admin--debug


            strbody += "<html xmlns='http://www.w3.org/1999/xhtml'></body>"
            strbody += "Terima kasih kerana mengambil ujian ini. Sila klik link untuk meneruskan ujian ini -->" & strLink
            strbody += "</body></html>"
            message.Body = strbody

            message.IsBodyHtml = True
            SmtpClient.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("EmailFrom"), ConfigurationManager.AppSettings("EmailFromPwd"))
            SmtpClient.Send(message)

        Catch ex As Exception
            Response.Write("sendemail_html" & ex.Message)

        End Try


    End Sub

End Class