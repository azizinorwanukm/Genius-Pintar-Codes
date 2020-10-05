''Imports System.Data
''Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
''Imports System.Globalization

Public Class config_examStudentQuestionUpload
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                txtQuestNo.Text = Request.QueryString("question")
                lblQuestNo.Text = Request.QueryString("question")
                imgControl.ImageUrl = "../ImageVB.aspx?exam_id=" & Request.QueryString("exam_id") & "&question=" & Request.QueryString("question")
                displayTotalQuestion()
                displaySubject()
                displayDifficulty()
                ddlAns()
                populateData()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        Response.Redirect("config_examStudentQuestionList.aspx?exam_id=" & Request.QueryString("exam_id"))
    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getDAtaStaff() As String
        strSQL = "SELECT top 1 stf_id from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub populateData()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT TOP 1 question, answer, subject_id, difficulty  FROM QUESTIONS WHERE exam_id = @exam_id AND quest_no = @question"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@exam_id", Request.QueryString("exam_id")))
                mCmd.Parameters.Add(New SqlParameter("@question", Request.QueryString("question")))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        txtQuest.Text = attachmentsTable.Rows(0).Item(0).ToString

        ddlAnswer.SelectedValue = attachmentsTable.Rows(0).Item(1).ToString

        ddlSuject.SelectedValue = attachmentsTable.Rows(0).Item(2).ToString

        ddlDifficulty.SelectedValue = attachmentsTable.Rows(0).Item(3).ToString

    End Sub

    Private Sub btn_update_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim quest_no As String = Request.QueryString("question")
        Dim exam_id As String = Request.QueryString("exam_id")

        Dim folderPath As String = ConfigurationManager.AppSettings("FolderPath") & "\module" & exam_id & "\"

        Dim newImg As String = "module" & exam_id & "-" & quest_no & ".jpg"
        Dim imgurl As String = "module" & exam_id & "\" & newImg

        'Check whether Directory (Folder) exists.
        If Not Directory.Exists(folderPath) Then
            'If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath)
        End If

        'Save the File to the Directory (Folder).

        If imgUpload.HasFile = True Then
            imgUpload.SaveAs(folderPath & newImg)
        End If

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "UPDATE QUESTIONS SET question=@question,answer=@answer,imgurl=@imgurl, subject_id=@subject_id, difficulty=@difficulty WHERE exam_id=@exam_id AND quest_no=@quest_no"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@question", txtQuest.Text))
                mCmd.Parameters.Add(New SqlParameter("@answer", ddlAnswer.SelectedValue))
                mCmd.Parameters.Add(New SqlParameter("@imgurl", imgurl))
                mCmd.Parameters.Add(New SqlParameter("@subject_id", ddlSuject.SelectedValue))
                mCmd.Parameters.Add(New SqlParameter("@difficulty", ddlDifficulty.SelectedValue))
                mCmd.Parameters.Add(New SqlParameter("@exam_id", exam_id))
                mCmd.Parameters.Add(New SqlParameter("@quest_no", quest_no))
                mConn.Open()
                ''mAdapter.SelectCommand = mCmd
                mCmd.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("config_examStudentQuestionUpload.aspx?question=" & quest_no & "&exam_id=" & exam_id)

    End Sub

    Private Sub btn_Go_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Response.Redirect("config_examStudentQuestionUpload.aspx?question=" & txtQuestNo.Text & "&exam_id=" & Request.QueryString("exam_id"))
    End Sub

    Private Sub ddlAns()
        ddlAnswer.Items.Add(New ListItem("A", 1))
        ddlAnswer.Items.Add(New ListItem("B", 2))
        ddlAnswer.Items.Add(New ListItem("C", 3))
        ddlAnswer.Items.Add(New ListItem("D", 4))
    End Sub

    Private Sub displayTotalQuestion()
        Dim totalQuests As String = oCommon.getFieldValue("SELECT TOP 1 quantity FROM EXAMS WHERE id = " & Request.QueryString("exam_id"))
        lblQuestTotal.Text = " / " & totalQuests
    End Sub

    Private Sub displaySubject()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id,subjectName FROM StemSubject"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                ''mCmd.Parameters.Add(New SqlParameter("@exam_id", Request.QueryString("exam_id")))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim rows As Integer = attachmentsTable.Rows.Count

        ddlSuject.Items.Add(New ListItem("-- Select subject --", 0))

        For k = 0 To rows - 1
            ddlSuject.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

    End Sub

    Private Sub displayDifficulty()
        ddlDifficulty.Items.Add(New ListItem(1, 1))
        ddlDifficulty.Items.Add(New ListItem(2, 2))
        ddlDifficulty.Items.Add(New ListItem(3, 3))
        ddlDifficulty.Items.Add(New ListItem(4, 4))
    End Sub

End Class