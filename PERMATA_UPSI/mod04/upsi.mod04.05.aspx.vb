Public Class upsi_mod04_05
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "4"
    Private QuestionNo As String = "5"
    Private Answer As String() = {"mod04.05.04", "mod04.05.11", "mod04.05.15"}
    Private NextQuestion As String = "~/mod04/upsi.mod04.06.aspx"
    Private NextModule As String = "~/mod05/upsi.mod05.01.aspx"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")

            time_left.InnerText = DirectCast(Session("TimeLeft"), Integer).ToString + "s"

            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")

        End If

    End Sub


    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim timeleft As Integer = DirectCast(Session("TimeLeft"), Integer)
        timeleft = timeleft - 1
        Session("TimeLeft") = timeleft
        time_left.InnerText = timeleft.ToString + "s"

        If timeleft <= 0 Then

            Timer1.Enabled = False

            NextQuestion = NextModule

            btnNext_Click(Nothing, Nothing)

        End If

    End Sub

    Protected Sub btnMula_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Timer1.Enabled = True
        btnStart.Visible = False
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try

            Dim UserMark As Single = modFunction.mod04GetMark(Answer, user_answer.Value)

            AddAnswer(Session("UserId").ToString, Session("ExamId").ToString, Session("AssistantName").ToString, Session("AssistantPhoneNo").ToString, ModuleNo, QuestionNo, user_answer.Value, UserMark)

            modFunction.UpdateLastState(Session("UserId").ToString, Session("ExamId").ToString, NextQuestion, CInt(Session("TimeLeft")), CInt(Session("ZeroMark")))
            Session("UserPage") = NextQuestion

        Catch ex As Exception
            LogError("upsi.mod04.05.aspx", ex.Message + " " + ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)

    End Sub


End Class