Public Class upsi_mod08_03
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "8"
    Private QuestionNo As String = "3"
    Private Answer As String = "mod08.03.03,mod08.03.07,mod08.03.10,mod08.03.14,mod08.03.17,mod08.03.24,mod08.03.27,mod08.03.31,mod08.03.34,mod08.03.36"
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestion As String = "~/mod08/upsi.mod08.04.aspx"
    Private NextModule As String = "~/mod09/upsi.mod09.01.aspx"
    Private NextQuestionInterval As Integer = 20
    Private NextModuleInterval As Integer = 5

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")
            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")

            Session("ZeroMark") = 0
            time_left.InnerText = DirectCast(Session("TimeLeft"), Integer).ToString + "s"

        End If

    End Sub

    Protected Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Timer1.Enabled = True
        btnStart.Visible = False
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim timerleft As Integer = DirectCast(Session("TimeLeft"), Integer)

        If timerleft <= 0 Then 'timeout
            Timer1.Enabled = False
            btnNext_Click(Nothing, Nothing)
        Else

            timerleft = timerleft - 1
            Session("TimeLeft") = timerleft
            time_left.InnerText = timerleft.ToString + "s"

        End If

    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Timer1.Enabled = False

        Try

            Dim UserMark As Single

            'If DirectCast(Session("TimeLeft"), Integer) <= 0 Then
            '    UserMark = 0
            'Else
            '    UserMark = mod01GetMark(Answer, user_answer.Value, NoOfBlokUsed)
            'End If

            UserMark = mod08GetMark(Answer, user_answer.Value)

            AddAnswer(Session("UserId").ToString, Session("ExamId").ToString, Session("AssistantName").ToString, Session("AssistantPhoneNo").ToString, ModuleNo, QuestionNo, user_answer.Value, UserMark)

            If UserMark = 0 Then
                Dim UserZeroMark As Integer = DirectCast(Session("ZeroMark"), Integer) + 1
                Session("ZeroMark") = UserZeroMark
                'If UserZeroMark >= ZeroMarkLimit Then
                '    NextQuestion = NextModule
                '    NextQuestionInterval = NextModuleInterval
                'End If

            Else
                Session("ZeroMark") = 0
            End If

            'NextQuestion = NextModule
            'NextQuestionInterval = NextModuleInterval

            modFunction.UpdateLastState(Session("UserId").ToString, Session("ExamId").ToString, NextQuestion, NextQuestionInterval, CInt(Session("ZeroMark")))
            Session("UserPage") = NextQuestion
            Session("TimeLeft") = NextQuestionInterval

        Catch ex As Exception
            LogError("upsi.mod08.03.aspx", ex.Message & "-" & ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)


    End Sub

End Class