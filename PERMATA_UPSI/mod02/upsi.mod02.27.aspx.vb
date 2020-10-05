Public Class upsi_mod02_27
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "2"
    Private QuestionNo As String = "27"
    Private Mark As Integer = 1
    Private Answer As String = "mod02.27.02,mod02.27.04,mod02.27.03,mod02.27.01"
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestion As String = "~/mod03/upsi.mod03.01.aspx"
    Private NextModule As String = "~/mod03/upsi.mod03.01.aspx"
    Private NextQuestionInterval As Integer = 30
    Private NextModuleInterval As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")

        If Not Page.IsPostBack Then
            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")
        End If

    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try

            Dim UserAnswer As String = user_answer.Value
            Dim UserMark As Single = modFunction.mod02GetMark(Answer, UserAnswer)

            AddAnswer(Session("UserId").ToString, Session("ExamId").ToString, Session("AssistantName").ToString, Session("AssistantPhoneNo").ToString, ModuleNo, QuestionNo, UserAnswer, UserMark)

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

        Catch ex As Exception
            LogError("upsi.mod02.27.aspx", ex.Message & " - " & ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)

    End Sub


End Class