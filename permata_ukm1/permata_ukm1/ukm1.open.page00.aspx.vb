Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class ukm1_open_page00
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim strSystemAns As String = ""

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
                Response.Redirect("ukm1.open.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
            End If

            '--get counter
            strQ = "Q" & Request.QueryString("q")
            strA = "A" & Request.QueryString("q")

            ''question counter
            nCurrQ = CInt(Request.QueryString("q"))

            ''-open ended 61-90. user change the q number
            If nCurrQ < 61 Then
                Response.Redirect("ukm1.session.end.aspx?lblmsg=URL tidak sah!")
            End If
            If nCurrQ > 90 Then
                Response.Redirect("ukm1.session.end.aspx?lblmsg=URL tidak sah!")
            End If

            '--set next question
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
        lblOpen.Text = rm.GetString("lblOpen", ci)
        lblQ1.Text = rm.GetString(strQ, ci)
        btnNext.Text = rm.GetString("btnNext", ci)

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ''--check if user answer before.
        If oCommon.isAnswered(strQ, Request.QueryString("studentid"), oCommon.getAppsettings("UKM1ExamYear")) = False Then
            SavePage()
        End If

        If strQ = "Q090" Then
            Response.Redirect("ukm1.open.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
        Else
            Response.Redirect("ukm1.open.page00.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid") & "&q=" & strNextQ)
        End If

    End Sub

    Private Sub SavePage()
        Dim nMark As Integer = 0

        ''--set lastpage
        Dim strLastpage As String = ""
        If strQ = "Q090" Then
            strLastpage = "ukm1.open.end.aspx?"
        Else
            '--loop thru questions
            strLastpage = "ukm1.open.page00.aspx?q=" & strNextQ & "&"
        End If

        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture

            '--calculate the mark
            nMark = getAnswerTextMark(ci, oCommon.FixSingleQuotes(txtQ1.Text), strA)

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

            strSQL += " UPDATE UKM1 SET " & strQ & "=" & nMark & ",LastPage='" & strLastpage & "',ExamEnd='" & oCommon.getNow & "',WrongCounter=" & nWrongCounter.ToString & " WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"

            'Debug.WriteLine(strSQL)
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = strRet
            End If

            'update ANSWER
            strSQL = "UPDATE UKM1_Answer WITH (ROWLOCK) SET " & strQ & "='" & oCommon.FixSingleQuotes(txtQ1.Text) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = strRet
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function getAnswerTextMark(ByVal ci As CultureInfo, ByVal strFullword As String, ByVal strAnswerID As String) As Integer
        Dim strKeyword As String = "|"
        Dim nMark As Integer = 0
        Dim nCount As Integer

        Dim arstrKeyword
        Dim strFind As String = ""

        Try
            '--get the answer from resource string
            strKeyword = rm.GetString(strAnswerID, ci)

            '--calculate the mark which MAX is 1 mark
            If strKeyword.Length > 1 Then
                arstrKeyword = strKeyword.Split("|")

                ''loop for all the string
                For nCount = 0 To UBound(arstrKeyword)
                    strFind = arstrKeyword(nCount)
                    '--debug
                    'Response.Write(":" & strFind & ":" & strFullword & ":")
                    If strFind.Length > 0 Then
                        If oCommon.Compare(strFind, strFullword) = True Then
                            nMark = 1
                            Exit For
                        Else
                            nMark = 0
                        End If
                    End If
                Next
            End If
            ''debug
            'Response.Write(nMark)
            Return nMark

        Catch ex As Exception
            'Response.Write("error message:" & ex.Message)
            Return 0

        End Try

    End Function

End Class