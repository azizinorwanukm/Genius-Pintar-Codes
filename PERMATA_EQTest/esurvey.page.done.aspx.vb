Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net


Partial Public Class esurvey_page_done
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim dScorePercentage As Double = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strType As String = "C"
        Dim ci As CultureInfo

        '--send email or not
        If ConfigurationManager.AppSettings("SendEmail") = "N" Then
            btnEmail.Visible = False
        Else
            btnEmail.Visible = True
        End If

        Try
            If Not IsPostBack Then
                Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("culture"))
                'get the culture info to set the language
                rm = New ResourceManager("Resources.eqtest_2014", System.Reflection.Assembly.Load("App_GlobalResources"))
                ci = Thread.CurrentThread.CurrentCulture
                LoadStrings(ci, strType)

                ''--complete test. isCompleted='Y'
                setCompleted()
                SumScore()
            End If

        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo, ByVal strType As String)
        If strType = "C" Then
            lblThankheader.Text = rm.GetString("lblThankheaderC", ci)
        Else
            lblThankheader.Text = rm.GetString("lblThankheaderR", ci)
        End If

    End Sub

    Private Sub setCompleted()
        '--update record
        strSQL = "UPDATE EQTest SET IsCompleted='Y' WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        ''--debug
        ''Response.Write(strSQL)
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub

    Private Sub SumScore()
        Dim strSelfAwareness As String = "0"
        Dim strSelfRegulation As String = "0"
        Dim strSelfMotivation As String = "0"
        Dim strEmpathy As String = "0"
        Dim strSocialSkill As String = "0"
        Dim strSpirituality As String = "0"
        Dim strMaturity As String = "0"

        Dim nSelfAwareness As Integer = 0
        Dim nSelfRegulation As Integer = 0
        Dim nSelfMotivation As Integer = 0
        Dim nEmpathy As Integer = 0
        Dim nSocialSkill As Integer = 0
        Dim nSpirituality As Integer = 0
        Dim nMaturity As Integer = 0

        Try
            ''1--strSelfAwareness
            strSQL = "SELECT SUM(Q001+Q004+Q154+Q002+Q003+Q005+Q011+Q012+Q155+Q156+Q157+Q006+Q007+Q009+Q010+Q015+Q018+Q158+Q159+Q008+Q013+Q014+Q016+Q017+Q019+Q020+Q160+Q161) as DomainSum FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strSelfAwareness = oCommon.getFieldValue(strSQL)

            ''2--strSelfRegulation 
            strSQL = "SELECT SUM(Q026+Q028+Q031+Q033+Q035+Q038+Q039+Q054+Q075+Q076+Q077+Q078+Q079+Q080+Q027+Q030+Q034+Q036+Q037+Q032+Q040+Q042+Q044+Q045+Q047+Q041+Q043+Q046+Q048+Q050+Q052+Q029+Q049+Q051+Q053+Q055+Q056) as DomainSum FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strSelfRegulation = oCommon.getFieldValue(strSQL)

            ''3--strSelfMotivation 
            strSQL = "SELECT SUM(Q058+Q060+Q062+Q064+Q067+Q177+Q178+Q179+Q180+Q181+Q182+Q183+Q057+Q059+Q061+Q063+Q065+Q066+Q070+Q068+Q069+Q072+Q073+Q071+Q074) as DomainSum FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strSelfMotivation = oCommon.getFieldValue(strSQL)

            ''4--strEmpathy
            strSQL = "SELECT SUM(Q021+Q022+Q023+Q024+Q025+Q081+Q083+Q085+Q088+Q090+Q092+Q082+Q084+Q087+Q089+Q091+Q086+Q093+Q095+Q099+Q101+Q094+Q096+Q097+Q098+Q100+Q104+Q106+Q108+Q110+Q115+Q103+Q105+Q109+Q111+Q113+Q102+Q107+Q112+Q114+Q116) as DomainSum FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strEmpathy = oCommon.getFieldValue(strSQL)

            ''5--strSocialSkill
            strSQL = "SELECT SUM(Q118+Q120+Q122+Q124+Q126+Q117+Q119+Q123+Q125+Q121+Q127+Q128+Q129+Q131+Q133+Q134+Q136+Q130+Q132+Q135+Q137+Q138+Q151+Q141+Q144+Q145+Q147+Q149+Q140+Q142+Q146+Q139+Q143+Q148+Q150+Q152+Q153) as DomainSum FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strSocialSkill = oCommon.getFieldValue(strSQL)

            ''6--strSpirituality
            strSQL = "SELECT SUM(Q162+Q164+Q165+Q167+Q168+Q170+Q172+Q175+Q176) as DomainSum FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strSpirituality = oCommon.getFieldValue(strSQL)

            ''7--strMaturity
            strSQL = "SELECT SUM(Q163+Q166+Q169+Q171+Q173+Q174) as DomainSum FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strMaturity = oCommon.getFieldValue(strSQL)

            ''--domainperc
            'SelfAwareness,SelfRegulation,SelfMotivation,Empathy,SocialSkill,Spirituality,Maturity
            Dim dDomain01 As Double = (CInt(strSelfAwareness) / 116) * 100
            Dim dDomain02 As Double = (CInt(strSelfRegulation) / 197) * 100
            Dim dDomain03 As Double = (CInt(strSelfMotivation) / 125) * 100
            Dim dDomain04 As Double = (CInt(strEmpathy) / 205) * 100
            Dim dDomain05 As Double = (CInt(strSocialSkill) / 185) * 100
            Dim dDomain06 As Double = (CInt(strSpirituality) / 45) * 100
            Dim dDomain07 As Double = (CInt(strMaturity) / 30) * 100

            lblDomain01.Text = dDomain01.ToString("0")
            lblDomain02.Text = dDomain02.ToString("0")
            lblDomain03.Text = dDomain03.ToString("0")
            lblDomain04.Text = dDomain04.ToString("0")
            lblDomain05.Text = dDomain05.ToString("0")
            lblDomain06.Text = dDomain06.ToString("0")
            lblDomain07.Text = dDomain07.ToString("0")

            Dim dTotalDomain As Double = (dDomain01 + dDomain02 + dDomain03 + dDomain04 + dDomain05 + dDomain06 + dDomain07) / 7
            lblScorePercentage.Text = dTotalDomain.ToString("0")

            Dim strScore As String = CInt(strSelfAwareness) + CInt(strSelfRegulation) + CInt(strSelfMotivation) + CInt(strEmpathy) + CInt(strSocialSkill) + CInt(strSpirituality) + CInt(strMaturity)
            Dim strTotalMark As String = "903"

            '---EmailAdd
            strSQL = "SELECT EmailAdd FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            lblEmailAdd.Text = oCommon.getFieldValue(strSQL)

            '--update record
            'SelfAwareness,SelfRegulation,SelfMotivation,Empathy,SocialSkill,Spirituality,Maturity
            strSQL = "UPDATE EQTest SET SelfAwareness=" & lblDomain01.Text & ",SelfRegulation=" & lblDomain02.Text & ",SelfMotivation=" & lblDomain03.Text & ",Empathy=" & lblDomain04.Text & ",SocialSkill=" & lblDomain05.Text & ",Spirituality=" & lblDomain06.Text & ",Maturity=" & lblDomain07.Text & ",Score=" & strScore & ",TotalMark=" & strTotalMark & ",ScorePercentage=" & lblScorePercentage.Text & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            ''--debug
            ''Response.Write(strDomain01 & ":" & strDomain02 & ":" & strDomain03)
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Successfully update database!"
            Else
                lblMsg.Text = "system error:" & strRet
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnEmail_Click(sender As Object, e As EventArgs) Handles btnEmail.Click
        Dim strbody As String = ""

        '--remove the button
        btnEmail.Visible = False

        Try

            Dim message As New MailMessage(ConfigurationManager.AppSettings("EmailFrom"), lblEmailAdd.Text)
            Dim SmtpClient As New SmtpClient(ConfigurationManager.AppSettings("SmtpClient"), ConfigurationManager.AppSettings("SmtpClientPort"))
            message.Subject = "RESULT:PERMATApintar Emotional Quotient Test"
            message.Bcc.Add(ConfigurationManager.AppSettings("EmailBCC"))   '--send a copy to admin--debug

            strbody += "<html><body><table border=0 width=100%>"
            strbody += "<tr>"
            ''SelfAwareness,SelfRegulation,SelfMotivation,Empathy,SocialSkill,Spirituality,Maturity
            strbody += "<td>SelfAwareness</td><td>SelfRegulation</td><td>SelfMotivation</td><td>Empathy</td><td>SocialSkill</td><td>Spirituality</td><td>Maturity</td>"
            strbody += "</tr>"
            strbody += "<tr>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;'>" & lblDomain01.Text & "%</td>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;'>" & lblDomain02.Text & "%</td>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;'>" & lblDomain03.Text & "%</td>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;'>" & lblDomain04.Text & "%</td>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;'>" & lblDomain05.Text & "%</td>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;'>" & lblDomain06.Text & "%</td>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;'>" & lblDomain07.Text & "%</td>"
            strbody += "</tr>"
            strbody += "<tr>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;' colspan='1'>Your Index.</td>"
            strbody += "<td style='font-size: 20px; font-weight: bolder; vertical-align: top;' colspan='6'>" & lblScorePercentage.Text & "%</td>"
            strbody += "</tr>"
            strbody += "</table></body></html>"
            message.Body = strbody

            '--debug
            'Response.Write(strbody)

            message.IsBodyHtml = True
            SmtpClient.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("EmailFrom"), ConfigurationManager.AppSettings("EmailFromPwd"))
            SmtpClient.Send(message)

            lblMsg.Text = "Successfully send PERMATApintar Emotional Quotient Test Result to:" & lblEmailAdd.Text
        Catch ex As Exception
            lblMsg.Text = "Fail to sent PERMATApintar Emotional Quotient Test Result to:" & lblEmailAdd.Text & ". Err:" & ex.Message
        End Try


    End Sub

    Private Sub lnkChart_Click(sender As Object, e As EventArgs) Handles lnkChart.Click
        Response.Redirect("esurvey.chart.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=ms-MY")

    End Sub
End Class