Public Class upsi_mod02_02
    Inherits System.Web.UI.Page

    Private ModuleNo As String = "2"
    Private QuestionNo As String = "2"
    Private Mark As Integer = 1
    Private Answer As String = "mod02.02.03"
    Private ZeroMarkLimit As Integer = 3
    Private NextQuestion As String = "~/mod02/upsi.mod02.03.aspx"
    Private NextModule As String = "~/mod03/upsi.mod03.01.aspx"
    Private NextQuestionInterval As Integer = 30
    Private NextModuleInterval As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNothing(Session("UserId")) Then Response.Redirect("~/default.aspx")

        If Not Page.IsPostBack Then
            If Not modFunction.IsCurrentPage(Request.Url.OriginalString, Session("UserPage").ToString, Session("RoleId").ToString) Then Response.Redirect("~/default.aspx")
        End If

    End Sub

    Protected Sub Image_Clicked(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click, ImageButton2.Click, ImageButton3.Click, ImageButton4.Click

        Try

            Dim img As ImageButton = DirectCast(sender, ImageButton)

            Dim UserAnswer As String = img.CommandArgument
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
            LogError("upsi.mod02.02.aspx", ex.Message & " - " & ex.StackTrace)
        End Try

        Response.Redirect(NextQuestion)

    End Sub


End Class