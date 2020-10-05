''Imports System.Data
''Imports System.Data.OleDb
Imports System.Data.SqlClient
''Imports System.IO
''Imports System.Globalization

Public Class config_examStudentQuestionSetup1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ddlExamyear.Items.Insert(0, 2017)
        ddlExamyear.Items.Insert(1, 2018)
        ddlExamyear.Items.Insert(2, 2019)
    End Sub

    Private Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add.Click

        Dim totalQuests As Integer = CType(txt_jumlahSoalan.Text, Integer)
        strSQL = "INSERT INTO EXAMS (exam_name, examyear, quantity) OUTPUT INSERTED.id VALUES ('" & exam_Name.Text & "','" & ddlExamyear.SelectedValue & "','" & totalQuests & "')"
        Dim exam_id As String = oCommon.getFieldValue(strSQL)

        strSQL = "INSERT INTO QUESTIONS (quest_no, exam_id, question, answer) VALUES (1,'" & exam_id & "','',1)"

        For idx = 2 To totalQuests
            strSQL = strSQL & ",(" & idx & ",'" & exam_id & "','',1)"
        Next

        oCommon.ExecuteSQL(strSQL)

        Response.Redirect("config_examStudentQuestionUpload.aspx?question=1&exam_id=" & exam_id)
    End Sub

    Private Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        Response.Redirect("ukm3_examQuestionConfig.aspx")
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
End Class