Public Class upsi_mod07_03
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "7"
    Private QuestionNo As String = "3"
    Private Answer() As String = {"mod07.03.01", "mod07.03.04"}
    Private NextQuestion As String = "~/mod07/upsi.mod07.04.aspx"
    Private NextModule As String = "~/mod08/upsi.mod08.01.aspx"
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestionInterval As Integer = 0
    Private NextModuleInterval As Integer = 20

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")

            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")

        End If

    End Sub


    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try

            Dim UserMark As Single = modFunction.mod07GetMark(Answer, user_answer.Value)

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

            modFunction.UpdateLastState(Session("UserId").ToString, Session("ExamId").ToString, NextQuestion, CInt(Session("TimeLeft")), CInt(Session("ZeroMark")))
            Session("UserPage") = NextQuestion
            Session("TimeLeft") = NextQuestionInterval

        Catch ex As Exception
            LogError("upsi.mod07.03.aspx", ex.Message + " " + ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)

    End Sub

End Class