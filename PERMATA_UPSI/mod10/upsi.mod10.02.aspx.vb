
Public Class upsi_mod10_02
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "10"
    Private QuestionNo As String = "2"
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestion As String = "~/mod10/upsi.mod10.03.aspx"
    Private NextModule As String = "~/upsi.outro.aspx"
    Private NextQuestionInterval As Integer = 90
    Private NextModuleInterval As Integer = 0
    Private Row As Integer = 2
    Private Col As Integer = 3
    Private HorizontalAnswerPair As List(Of CorrectPair) = New List(Of CorrectPair) From {New CorrectPair("mod10.02.02", "mod10.02.01")}
    Private VerticalAnswerPair As List(Of CorrectPair) = New List(Of CorrectPair) From {}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")
            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")

            time_left.InnerText = DirectCast(Session("TimeLeft"), Integer).ToString + "s"

        End If

    End Sub

    Protected Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Timer1.Enabled = True
        btnStart.Visible = False
        ScriptManager.RegisterStartupScript(Me, GetType(String), "x", "$(document).ready(function () {start();});", True)
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim timerleft As Integer = DirectCast(Session("TimeLeft"), Integer)

        If timerleft <= 0 Then 'timeout
            Timer1.Enabled = False
        Else

            timerleft = timerleft - 1
            Session("TimeLeft") = timerleft
            time_left.InnerText = timerleft.ToString + "s"

        End If

    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Try

            Dim UserMark As Single

            'If DirectCast(Session("TimeLeft"), Integer) <= 0 Then
            '    UserMark = 0
            'Else
            '    UserMark = mod01GetMark(Answer, user_answer.Value, NoOfBlokUsed)
            'End If

            UserMark = mod10GetMark(HorizontalAnswerPair, VerticalAnswerPair, user_answer.Value, Row, Col, 1)

            AddAnswer(Session("UserId").ToString, Session("ExamId").ToString, Session("AssistantName").ToString, Session("AssistantPhoneNo").ToString, ModuleNo, QuestionNo, user_answer.Value, UserMark)

            If UserMark = 0 Then
                Dim UserZeroMark As Integer = DirectCast(Session("ZeroMark"), Integer) + 1
                Session("ZeroMark") = UserZeroMark
                If UserZeroMark >= ZeroMarkLimit Then
                    NextQuestion = NextModule
                    NextQuestionInterval = NextModuleInterval
                End If

            Else
                Session("ZeroMark") = 0
            End If

            modFunction.UpdateLastState(Session("UserId").ToString, Session("ExamId").ToString, NextQuestion, NextQuestionInterval, CInt(Session("ZeroMark")))
            Session("UserPage") = NextQuestion
            Session("TimeLeft") = NextQuestionInterval

            If NextQuestion = NextModule Then modFunction.UpdateEndTime(Session("UserId").ToString, Session("ExamId").ToString)

        Catch ex As Exception
            LogError("upsi.mod10.02.aspx", ex.Message & "-" & ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)

    End Sub

End Class