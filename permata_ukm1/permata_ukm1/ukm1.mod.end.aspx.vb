Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class ukm1_mod_end
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim strSystemAns As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            '--check if user answer before.
            If oCommon.isAnswered("Q060", Request.QueryString("studentid"), oCommon.getAppsettings("UKM1ExamYear")) = False Then
                SavePage()
            End If

            ''--set wrong counter. Reset counter for each mod start
            Dim nWrongCounter As Integer = 0

            'Dim ukm1Table As String = Common.getUKM1Table(oCommon.getAppsettings("UKM1ExamYear"))

            'strSQL = ""

            'If Not ukm1Table = "UKM1" Then
            '    strSQL = "UPDATE " & ukm1Table & " SET WrongCounter=" & nWrongCounter.ToString & ",LastPage='ukm1.mod.end.aspx?' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            '    'oCommon.ExecuteSQL(strSQL)
            'End If

            strSQL = " UPDATE UKM1 SET WrongCounter=" & nWrongCounter.ToString & ",LastPage='ukm1.mod.end.aspx?' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"

            'Debug.WriteLine(strSQL)

            strRet = oCommon.ExecuteSQL(strSQL)

        Catch ex As Exception
            lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblModEnd.Text = rm.GetString("lblModEnd", ci)
        lblModEndMsg.Text = rm.GetString("lblModEndMsg", ci)

        btnNext.Text = rm.GetString("btnNext", ci)
        strSystemAns = rm.GetString("A060", ci)

    End Sub

    Private Sub SavePage()
        '--check prev page answer and give mark
        Dim strAns As String = Request.QueryString("ans")
        If strAns = strSystemAns Then
            nMark = 1
        Else
            nMark = 0
        End If

        ''--set wrong counter
        Dim nWrongCounter As Integer = 0
        If nMark = 0 Then
            strSQL = "SELECT WrongCounter FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            nWrongCounter = oCommon.getFieldValueInt(strSQL) + 1
        Else
            nWrongCounter = 0
        End If

        'update MARK

        'Dim ukm1Table As String = Common.getUKM1Table(oCommon.getAppsettings("UKM1ExamYear"))

        'strSQL = ""

        'If Not ukm1Table = "UKM1" Then
        '    strSQL = "UPDATE " & ukm1Table & " SET Q060=" & nMark & ",LastPage='ukm1.open.page00.aspx?q=061&',ExamEnd='" & oCommon.getNow & "',WrongCounter=" & nWrongCounter.ToString & " WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        '    'oCommon.ExecuteSQL(strSQL)
        'End If

        strSQL = " UPDATE UKM1 SET Q060=" & nMark & ",LastPage='ukm1.open.page00.aspx?q=061&',ExamEnd='" & oCommon.getNow & "',WrongCounter=" & nWrongCounter.ToString & " WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        'Debug.WriteLine(strSQL)

        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error:" & strRet
        End If

        'update ANSWER
        strSQL = "UPDATE UKM1_Answer WITH (ROWLOCK) SET Q060='" & strAns & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error:" & strRet
        End If

        '--debug
        'lblMsg.Text = "Random:" & Session("QuestionSet").ToString
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Response.Redirect("ukm1.open.page00.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid") & "&q=061")

    End Sub
End Class