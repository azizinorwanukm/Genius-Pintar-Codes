Public Class upsi_mod1_06
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "1"
    Private QuestionNo As String = "6"
    Private Mark As Integer = 1
    Private Answer As String = ",mod01.06.01,,,,mod01.06.01,mod01.06.01,"
    Private NoOfBlokUsed As Integer = 3
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestion As String = "~/mod01/upsi.mod01.07.aspx"
    Private NextModule As String = "~/upsi.outro.aspx"
    Private NextQuestionInterval As Integer = 30
    Private NextModuleInterval As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")
            time_left.InnerText = DirectCast(Session("TimeLeft"), Integer).ToString + "s"
            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")
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
            '    UserMark = GetMark(user_answer.Value)
            'End If

            UserMark = GetMark(user_answer.Value)

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
            LogError("upsi.mod01.06.aspx", ex.Message & "-" & ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)

    End Sub

    Private Function GetMark(UserAnswer As String) As Integer

        Dim AnswerArray As String() = UserAnswer.Split(",")
        Dim AnswerArray2D(2, 4) As String
        Dim count As Integer = 0
        Dim NonEmptyBlok As Integer

        For i As Integer = 0 To 2
            For j As Integer = 0 To 4
                AnswerArray2D(i, j) = AnswerArray(count)
                If AnswerArray(count) <> "" Then NonEmptyBlok = NonEmptyBlok + 1
                count = count + 1
            Next
        Next

        'get no of pattern use in answer. is not same fail
        If NonEmptyBlok <> NoOfBlokUsed Then Return 0

        If UserAnswer.Contains(Answer) Then Return Mark

        Return 0

    End Function

End Class