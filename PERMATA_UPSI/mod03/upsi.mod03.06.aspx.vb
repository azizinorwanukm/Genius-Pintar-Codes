Public Class upsi_mod03_06
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "3"
    Private QuestionNo As String = "6"
    Private Mark As Integer = 1
    Private Answer As String = "mod03.06.02"
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestion As String = "~/mod03/upsi.mod03.07.aspx"
    Private NextModule As String = "~/mod04/upsi.mod04.01.aspx"
    Private NextQuestionInterval As Integer = 0
    Private NextModuleInterval As Integer = 120

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")
            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")

        End If

    End Sub

    Protected Sub Image_Clicked(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click, ImageButton2.Click, ImageButton3.Click, ImageButton4.Click

        Try

            Dim img As ImageButton = DirectCast(sender, ImageButton)

            Dim UserAnswer As String = img.CommandArgument
            Dim UserMark As Single = modFunction.mod03GetMark(Answer, UserAnswer)

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

            modFunction.UpdateLastState(Session("UserId").ToString, Session("ExamId").ToString, NextQuestion, NextQuestionInterval, CInt(Session("ZeroMark")))
            Session("UserPage") = NextQuestion
            Session("TimeLeft") = NextQuestionInterval

        Catch ex As Exception
            LogError("upsi.mod03.06.aspx", ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)

    End Sub

End Class