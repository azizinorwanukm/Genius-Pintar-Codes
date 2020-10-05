Imports System.Data.SqlClient

Public Class question2
    Inherits System.Web.UI.UserControl

    Dim quantity As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        checkOnline()

        Try
            If Not IsPostBack Then
                lblName.Text = Session("StudentName")
                lblMykad.Text = Session("mykad")
                questNo.Text = Request.QueryString("question")
                getQuestion()
                quest_list()
                radio_answers()
                btnConfirm.Enabled = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub getQuestion()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT TOP 1 question FROM QUESTIONS WHERE exam_id = @exam_id AND quest_no = @quest_no "

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@quest_no", Request.QueryString("question")))
                mCmd.Parameters.Add(New SqlParameter("@exam_id", Session("examId")))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        lblQuest.Text = attachmentsTable.Rows(0).Item(0).ToString

        imgControl.ImageUrl = "../ImageVB.aspx?questNo=" & Request.QueryString("question")

    End Sub

    Private Sub quest_list()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT TOP 1 quantity FROM Exams WHERE id = " & Session("examId")

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@ukm3id", Session("ukm3id")))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        quantity = CType(attachmentsTable.Rows(0).Item(0).ToString, Integer)

        If quantity = CType(Request.QueryString("question"), Integer) Then
            btnConfirm.Text = "Confirm"
        End If

    End Sub

    Private Sub studentAnswer(ByVal sender As Object, ByVal e As EventArgs) Handles radioAnswers.SelectedIndexChanged
        lblStudentAnswer.Text = "You have chosen " & radioAnswers.SelectedItem.Text & ". Click button below to confirm"
        btnConfirm.Enabled = True
        btnDisable.Visible = False
        btnConfirm.Visible = True
    End Sub

    Private Sub confirm(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirm.Click

        Dim studentChoice As Integer = CType(radioAnswers.SelectedValue, Integer)

        Dim queryAnswer As String = "SELECT TOP 1 answer FROM QUESTIONS WHERE exam_id = " & Session("examId") & " AND quest_no =" & Request.QueryString("question")

        Dim correctAnswer As Integer = CType(CommonMethod.getSingleCellValue(queryAnswer), Integer)

        Dim isCorrect As Integer = 0

        If studentChoice = correctAnswer Then
            isCorrect = 1
        Else
            isCorrect = 0
        End If

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "UPDATE StudentAnswers SET choose = @choose, correct = @correct WHERE ukm3id = @ukm3id AND quest_no = @quest_no"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@choose", radioAnswers.SelectedValue))
                mCmd.Parameters.Add(New SqlParameter("@correct", isCorrect))
                mCmd.Parameters.Add(New SqlParameter("@ukm3id", Session("ukm3id")))
                mCmd.Parameters.Add(New SqlParameter("@quest_no", Request.QueryString("question")))
                mConn.Open()
                ''mAdapter.SelectCommand = mCmd
                mCmd.ExecuteNonQuery()
            End Using
        End Using

        query = "SELECT TOP 1 quantity FROM Exams WHERE id = @exam_id"
        Dim attachmentsTable = New DataTable

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@exam_id", Session("examId")))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        quantity = CType(attachmentsTable.Rows(0).Item(0).ToString, Integer)

        Dim nextquestion As Integer = CType(Request.QueryString("question"), Integer) + 1

        If nextquestion > quantity Then

            Dim mark80 As Integer = CType(CommonMethod.getSingleCellValue("SELECT COUNT(*) FROM StudentAnswers WHERE ukm3id = " & Session("ukm3id") & " and correct = 1"), Integer)
            Dim mark100 As String = Math.Round(mark80, 1, MidpointRounding.AwayFromZero) * 5 / 4

            query = "UPDATE UKM3 SET marks_80 = @marks_80, marks_100 = @marks_100 WHERE id = @std_id"

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@marks_80", mark80))
                    mCmd.Parameters.Add(New SqlParameter("@marks_100", mark100))
                    mCmd.Parameters.Add(New SqlParameter("@std_id", Session("ukm3id")))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using

            Session.Clear()
            Response.Redirect("login.aspx?note=done")
        Else
            Response.Redirect("question.aspx?question=" & nextquestion)
        End If

    End Sub

    Private Sub radio_answers()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT TOP 1 choose FROM StudentAnswers WHERE ukm3id = @ukm3id AND quest_no = @quest_no"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@ukm3id", Session("ukm3id")))
                mCmd.Parameters.Add(New SqlParameter("@quest_no", Request.QueryString("question")))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim ans As String = attachmentsTable.Rows(0).Item(0).ToString

        If ans = "0" Then
            lblStudentAnswer.Text = "You have not answered this question"
        Else
            radioAnswers.SelectedValue = attachmentsTable.Rows(0).Item(0).ToString
            lblStudentAnswer.Text = "You answered " & radioAnswers.SelectedItem.Text
        End If

    End Sub

    Private Sub logout(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
        Session.Clear()
        Response.Redirect("login.aspx?note=logoff")
    End Sub

    Private Sub checkOnline()
        Dim status As String = CommonMethod.getSingleCellValue("SELECT parameter FROM general_config WHERE config = 'Stem Test open'")

        If Not status = "1" Then
            Session.Clear()
            Response.Redirect("login.aspx")
        End If

    End Sub

End Class