Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class ukm1_select_page00
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo
    ''--year 2011. Dr Siti instruct to reduce number of questions. until page15 only!

    Dim strQ As String = "Q"
    Dim strA As String = "A"
    Dim nCurrQ As Integer
    Dim nNextQ As Integer
    Dim strNextQ As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            ''test DONE
            strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            If oCommon.getFieldValue(strSQL) = "DONE" Then
                Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
            End If

            ''check how many wrong. if more than 2 go to next module
            strSQL = "SELECT WrongCounter FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            If oCommon.getFieldValueInt(strSQL) > 2 Then
                Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
            End If

            strQ = "Q" & Request.QueryString("q")
            strA = "A" & Request.QueryString("q")

            ''question counter
            nCurrQ = CInt(Request.QueryString("q"))

            ''-select section 105-134. user change the q number
            If nCurrQ < 105 Then
                Response.Redirect("ukm1.session.end.aspx?lblmsg=URL tidak sah!")
            End If
            If nCurrQ > 134 Then
                Response.Redirect("ukm1.session.end.aspx?lblmsg=URL tidak sah!")
            End If

            nNextQ = nCurrQ + 1
            strNextQ = oCommon.DoPadZeroLeft(nNextQ, 3)
            ''Response.Write(strNextQ)

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblSelect.Text = rm.GetString("lblSelect", ci)

        lblQ01.Text = " " & rm.GetString(strQ, ci)
        rbQ01.Items(0).Text = " " & rm.GetString(strQ & "_1", ci)
        rbQ01.Items(1).Text = " " & rm.GetString(strQ & "_2", ci)
        rbQ01.Items(2).Text = " " & rm.GetString(strQ & "_3", ci)
        rbQ01.Items(3).Text = " " & rm.GetString(strQ & "_4", ci)
        rbQ01.Items(4).Text = " " & rm.GetString(strQ & "_5", ci)
        rbQ01.Items(5).Text = " " & rm.GetString(strQ & "_6", ci)

        btnNext.Text = rm.GetString("btnNext", ci)

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ''--check if user answer before.
        If oCommon.isAnswered(strQ, Request.QueryString("studentid"), oCommon.getAppsettings("UKM1ExamYear")) = False Then
            SavePage()
        End If

        ''maximum number of questions
        If strQ = "Q134" Then
            Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
        Else
            Response.Redirect("ukm1.select.page00.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid") & "&q=" & strNextQ)
        End If

    End Sub

    Private Sub SavePage()
        Dim nMark As Integer = 0

        ''--set lastpage
        Dim strLastpage As String = ""
        If strQ = "Q134" Then
            strLastpage = "ukm1.permata.end.aspx?"
        Else
            strLastpage = "ukm1.select.page00.aspx?q=" & strNextQ & "&"
        End If

        '--load questions base on language selected
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture

            nMark = getSystemMark(ci, rbQ01.SelectedItem.Value, strA)
            ''lblMsg.Text = rbQ01.SelectedItem.Value

            ''--set wrong counter. If right reset back to 0
            Dim nWrongCounter As Integer = 0
            If nMark = 0 Then
                strSQL = "SELECT WrongCounter FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
                nWrongCounter = oCommon.getFieldValueInt(strSQL) + 1
            Else
                nWrongCounter = 0
            End If

            'update MARK

            Dim ukm1Table As String = Common.getUKM1Table(oCommon.getAppsettings("UKM1ExamYear"))

            strSQL = ""

            If Not ukm1Table = "UKM1" Then
                strSQL = "UPDATE " & ukm1Table & " SET " & strQ & "=" & nMark & ",LastPage='" & strLastpage & "',ExamEnd='" & oCommon.getNow & "',WrongCounter=" & nWrongCounter.ToString & " WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
                'oCommon.ExecuteSQL(strSQL)
            End If

            strSQL = " UPDATE UKM1 SET " & strQ & "=" & nMark & ",LastPage='" & strLastpage & "',ExamEnd='" & oCommon.getNow & "',WrongCounter=" & nWrongCounter.ToString & " WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"

            'Debug.WriteLine(strSQL)
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = strRet
            End If

            'update ANSWER
            strSQL = "UPDATE UKM1_Answer WITH (ROWLOCK) SET " & strQ & "='" & rbQ01.SelectedItem.Value & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = strRet
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function getSystemMark(ByVal ci As CultureInfo, ByVal strUserAnswer As String, ByVal strSystemAnswer As String) As Integer
        Dim strRet As String
        strRet = rm.GetString(strSystemAnswer, ci)
        ''debug
        'Response.Write("strRet:" & strRet)

        If strUserAnswer = strRet Then
            Return 1
        Else
            Return 0
        End If

    End Function

End Class