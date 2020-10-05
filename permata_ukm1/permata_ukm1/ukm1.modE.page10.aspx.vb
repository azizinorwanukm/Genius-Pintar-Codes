Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class ukm1_modE_page10
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
            ''go to last page. user try to keyin manually
            strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            If oCommon.getFieldValue(strSQL) = "DONE" Then
                Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
            End If

            LoadImages(oCommon.getQuestionYear(Request.QueryString("studentid")))

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            '--check if user answer before.
            If oCommon.isAnswered("Q057", Request.QueryString("studentid"), oCommon.getAppsettings("UKM1ExamYear")) = False Then
                SavePage()
            End If

            ''check how many wrong. if more than 2 go to next module
            strSQL = "SELECT WrongCounter FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            If oCommon.getFieldValueInt(strSQL) > 2 Then
                Response.Redirect("ukm1.mod.end.aspx?ans=0&lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"), False)
            End If


        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try

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
        strSQL = "UPDATE UKM1 WITH (ROWLOCK) SET Q057=" & nMark & ",LastPage='ukm1.modE.page10.aspx?',ExamEnd='" & oCommon.getNow & "',WrongCounter=" & nWrongCounter.ToString & " WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = strRet
        End If

        'update ANSWER
        strSQL = "UPDATE UKM1_Answer WITH (ROWLOCK) SET Q057='" & strAns & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = strRet
        End If

        '--debug
        'lblMsg.Text = "Random:" & Session("QuestionSet").ToString


    End Sub

    Private Sub LoadImages(ByVal strSet As String)
        Image0.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.0.gif"
        Image1.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.1.gif"
        Image2.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.2.gif"
        Image3.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.3.gif"
        Image4.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.4.gif"
        Image5.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.5.gif"
        Image6.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.6.gif"
        Image7.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.7.gif"
        Image8.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/E10.8.gif"

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblModuleA.Text = rm.GetString("lblModuleA", ci)
        lblInstructionModuleA.Text = rm.GetString("lblInstructionModuleA", ci)
        strSystemAns = rm.GetString("A057", ci)

    End Sub


End Class