Imports System.Resources
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net

Public Class instruktor_forgot
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_tahun_list()
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub koko_tahun_list()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

            'ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If isUserExist() = True Then
            SendEmail()
        Else
            lblMsg.Text = "Login ID tidak ditemui. Sila hubungi pihak pengurusan."
        End If

    End Sub

    Private Function isUserExist() As Boolean
        strSQL = "SELECT InstruktorID FROM koko_instruktor WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Tahun='" & ddlTahun.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            Return True
        End If

        Return False
    End Function

    Private Sub SendEmail()
        Dim strPwdDecrypt As String = ""
        Dim strbody As String = ""
        Dim strPwd As String = ""
        strSQL = "SELECT Pwd FROM koko_instruktor WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' AND Tahun='" & ddlTahun.Text & "'"
        strPwd = oCommon.getFieldValue(strSQL)
        strPwdDecrypt = oDes.DecryptData(strPwd)

        Try
            Dim message As New MailMessage(ConfigurationManager.AppSettings("EmailFrom"), oCommon.FixSingleQuotes(txtLoginID.Text))
            Dim SmtpClient As New SmtpClient(ConfigurationManager.AppSettings("SmtpClient"), ConfigurationManager.AppSettings("SmtpClientPort"))
            message.Subject = "KOKO:PERMATApintar LUPA KATA LALUAN."
            message.Bcc.Add(ConfigurationManager.AppSettings("EmailBCC"))   '--send a copy to admin--debug

            strbody += "<html><body><table border=0 width=100%>"
            strbody += "<tr>"
            strbody += "<td>System:</td><td>KOKO PERMATApintar</td>"
            strbody += "</tr>"
            strbody += "<tr>"
            strbody += "<td>URL:</td><td>" & ConfigurationManager.AppSettings("AppURL") & "</td>"
            strbody += "</tr>"
            strbody += "<tr>"
            strbody += "<td>Login ID:</td><td>" & oCommon.FixSingleQuotes(txtLoginID.Text) & "</td>"
            strbody += "</tr>"
            strbody += "<tr>"
            strbody += "<td>Kata Laluan:</td><td>" & strPwdDecrypt & "</td>"
            strbody += "</tr>"
            strbody += "</table></body></html>"
            message.Body = strbody

            '--debug
            'Response.Write(strbody)

            message.IsBodyHtml = True
            SmtpClient.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("EmailFrom"), ConfigurationManager.AppSettings("EmailFromPwd"))
            SmtpClient.Send(message)

            lblMsg.Text = "BERJAYA hantar kata laluan anda ke:" & txtLoginID.Text
        Catch ex As Exception
            lblMsg.Text = "GAGAL hantar kata laluan anda ke:" & txtLoginID.Text & ". Err:" & ex.Message
        End Try

    End Sub
End Class