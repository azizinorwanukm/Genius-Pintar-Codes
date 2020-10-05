Public Class upsi_mod5_28
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "5"
    Private QuestionNo As String = "28"
    Private Answer As String = "mod05.28.01,mod05.28.02,mod05.28.05,mod05.28.06,mod05.28.07"
    Private NextQuestion As String = "~/mod05/upsi.mod05.29.aspx"
    Private NextModule As String = "~/mod01/upsi.mod01.01.aspx"
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestionInterval As Integer = 5
    Private NextModuleInterval As Integer = 30

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")
            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")

            Dim timerleft As Integer = DirectCast(Session("TimeLeft"), Integer)

            time_left.InnerText = timerleft.ToString + "s"

            If timerleft <= 0 Then
                btnStart.Visible = False
                ScriptManager.RegisterStartupScript(Me, GetType(String), "x", "$(document).ready(function () {showAnswer();});", True)
            End If

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
            ScriptManager.RegisterStartupScript(Me, GetType(String), "x", "$(document).ready(function () {showAnswer();});", True)

        Else

            timerleft = timerleft - 1
            Session("TimeLeft") = timerleft
            time_left.InnerText = timerleft.ToString + "s"

        End If

    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try

            Dim UserMark As Single = modFunction.mod05GetMark(Answer, user_answer.Value)

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

        Catch ex As Exception
            LogError("upsi.mod05.28.aspx", ex.Message + " " + ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)

    End Sub

End Class