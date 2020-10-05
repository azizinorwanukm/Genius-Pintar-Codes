Public Class upsi_mod06_24
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "6"
    Private QuestionNo As String = "24"
    Private Answer As String = ""
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestion As String = "~/mod06/upsi.mod06.25.aspx"
    Private NextModule As String = "~/mod07/upsi.mod07.01.aspx"
    Private NextQuestionInterval As Integer = 0
    Private NextModuleInterval As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")
            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")

            'Session("ZeroMark") = 0
            time_left.InnerText = DirectCast(Session("TimeLeft"), Integer).ToString + "s"

        End If

    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click


        Try

            Dim UserMark As Single

            'If DirectCast(Session("TimeLeft"), Integer) <= 0 Then
            '    UserMark = 0
            'Else
            '    UserMark = mod01GetMark(Answer, user_answer.Value, NoOfBlokUsed)
            'End If

            UserMark = mod06GetMark(QuestionNo, Session("Language").ToString, txtAnswer.Text)

            AddAnswer(Session("UserId").ToString, Session("ExamId").ToString, Session("AssistantName").ToString, Session("AssistantPhoneNo").ToString, ModuleNo, QuestionNo, txtAnswer.Text, UserMark)

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
            LogError("upsi.mod06.24.aspx", ex.Message & "-" & ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)


    End Sub

End Class