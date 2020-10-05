Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net

Partial Public Class ukm1_requestid
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strLang As String
    Dim strTokenID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''--YYMMDD
        Dim strDOB As String = RespDOBYear.Value & RespDOBmonth.Value & RespDOBday.Value

        ''--generate mykadtemp
        Dim mykadtemp As String = ""

        Dim nrunningno As Integer
        strSQL = "SELECT runningno FROM ukm1_mykadtemp order by mykadid DESC"
        strRet = oCommon.getFieldValueInt(strSQL)
        nrunningno = CInt(strRet) + 1

        ''kodnegeri
        Dim nkodnegeri As Integer
        strSQL = "SELECT kodnegeri FROM ukm1_mykadtemp order by mykadid DESC"
        strRet = oCommon.getFieldValueInt(strSQL)
        nkodnegeri = CInt(strRet)

        If nrunningno > 9998 Then
            ''--add kod negeri
            nkodnegeri += 1
            ''--reset running no
            nrunningno = 1
        End If

        ''--temporary mykad
        mykadtemp = strDOB & nkodnegeri.ToString & oCommon.DoPadZeroLeft(nrunningno.ToString, 4)

        ''--save to database
        strSQL = "INSERT INTO ukm1_mykadtemp (nosuratberanak,namapenuh,tarikhlahir,email,kodnegeri,runningno,mykadtemp) VALUES('" & oCommon.FixSingleQuotes(nosuratberanak.Text) & "','" & oCommon.FixSingleQuotes(namapenuh.Text) & "','" & strDOB & "','" & oCommon.FixSingleQuotes(email.Text) & "','" & nkodnegeri.ToString & "','" & nrunningno.ToString & "','" & mykadtemp & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Database Berjaya."
        Else
            lblMsg.Text = "Database Gagal. Sila email permatapintar@ukm.my message ini. " & strRet
            Exit Sub
        End If

        ''--send email
        If mykadtemp_sendwebmail(mykadtemp) = True Then
            lblMsg.Text = "Email Berjaya. Untuk pengesahan, sila semak email anda untuk nombor sementara menggantikan MYKAD/MYKID."

            ''clear last la
            clearScreen()
        End If


    End Sub

    Private Function ValidatePage() As Boolean
        If nosuratberanak.Text.Length = 0 Then
            lblMsg.Text = "Sila isi No Sijil Kelahiran Pelajar"
            nosuratberanak.Focus()
            Return False
        End If
        If namapenuh.Text.Length = 0 Then
            lblMsg.Text = "Sila isi Nama Penuh Pelajar"
            namapenuh.Focus()
            Return False
        End If

        If RespDOBday.Value.Length = 0 Then
            lblMsg.Text = "Sila pilih Hari Tarikh Lahir"
            Return False
        End If
        If RespDOBmonth.Value.Length = 0 Then
            lblMsg.Text = "Sila pilih Bulan Tarikh Lahir"
            Return False
        End If
        If RespDOBYear.Value.Length = 0 Then
            lblMsg.Text = "Sila pilih Tahun Tarikh Lahir"
            Return False
        End If

        If email.Text.Length = 0 Then
            lblMsg.Text = "Sila isi email anda."
            Return False
        End If

        If oCommon.isEmail(email.Text) = False Then
            lblMsg.Text = "Email di dalam format yang tidak sah. Contoh email: myemail@yahoo.com"
            Return False
        End If

        If Not email.Text = emailverify.Text Then
            lblMsg.Text = "Email yang dimasukkan tidak sama. Sila masukkan sekali lagi."
            email.Focus()
            Return False
        End If

        If email.Text = "permatapintar@ukm.my" Then
            lblMsg.Text = "Masukkan email anda sendiri samada yahoo atau gmail. Bukan email permatapintar."
            email.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub clearScreen()
        nosuratberanak.Text = ""
        namapenuh.Text = ""
        RespDOBday.SelectedIndex = 0
        RespDOBmonth.SelectedIndex = 0
        RespDOBYear.SelectedIndex = 0
        email.Text = ""
        emailverify.Text = ""

    End Sub

    Private Function mykadtemp_sendwebmail(ByVal strmykadtemp As String) As Boolean
        Dim strbody As String = ""

        Try
            Dim message As New MailMessage("support@permatapintar.edu.my", email.Text)
            Dim SmtpClient As New SmtpClient("mail.permatapintar.edu.my", 587)
            message.Subject = "PERMATApintar UKM1 2011 MYKAD/MYKID# Sementara: " & strmykadtemp
            message.Bcc.Add("support@permatapintar.edu.my")   '--send a copy to admin

            strbody += "<table>"
            strbody += "<tr><td style='vertical-align:top;' colspan='2'>No Sijil Kelahiran: " & nosuratberanak.Text & "</td>"
            strbody += "<tr><td style='vertical-align:top;' colspan='2'>Nama Penuh: " & namapenuh.Text & "</td>"
            strbody += "<tr><td style='vertical-align:top;' colspan='2'>Email: " & email.Text & "</td>"
            strbody += "<tr><td style='vertical-align:top;' colspan='2'>PERMATApintar UKM1 2011 MYKAD/MYKID# Sementara: " & strmykadtemp & "</td>"


            strbody += "<tr><td colspan='4' style='border-bottom:solid 1px #000000;'></td></tr>"
            strbody += "<tr><td colspan='4'><b>Gunakan nombor di atas untuk mengisi ruangan *MYKAD/MYKID#.</b></td></tr>"

            strbody += "<tr><td colspan='4'><b>Nombor ini bukan dikeluarkan oleh Jabatan Pendaftaran Negara. Ia hanyalah nombor sementara untuk kegunaan Ujian UKM1 PERMATApintar 2011. http://ukm1.permatapintar.edu.my/</b></td></tr>"
            strbody += "<tr><td colspan='4'>&nbsp;</td></tr>"
            strbody += "<tr><td colspan='4'><b>Please retain for your records</b></td></tr>"
            strbody += "<tr><td colspan='4'>&nbsp;</td></tr>"
            strbody += "<tr><td colspan='4'>This is computer generated, no signature required.</td></tr>"

            message.Body = strbody

            message.IsBodyHtml = True
            SmtpClient.Credentials = New NetworkCredential("support@permatapintar.edu.my", "p@ssw0rd1")
            SmtpClient.Send(message)

            Return True
        Catch ex As Exception
            lblMsg.Text = "Email Gagal. Sila email permatapintar@ukm.my No Sijil Kelahiran dan Nama Penuh." & ex.Message
            Return False
        End Try

    End Function


    
End Class