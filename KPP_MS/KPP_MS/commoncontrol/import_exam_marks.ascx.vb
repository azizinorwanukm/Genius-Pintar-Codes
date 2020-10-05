Imports System.Data.OleDb

Public Class import_exam_marks
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strBil As String = ""
    Dim strStudentName As String = ""
    Dim strStudentMykad As String = ""
    Dim strClassName As String = ""
    Dim strSubjectName As String = ""
    Dim strExamName As String = ""
    Dim strExamYear As String = ""
    Dim strMarks As String = ""
    Dim strGrade As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/exam_import"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [marks$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Markah:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVar()
                strMsg = ""

                'Student_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Name")) Then
                    strStudentMykad = SiteData.Tables(0).Rows(i).Item("Student_Name")
                Else
                    strMsg += " Please Enter Student_Name |"
                End If

                'Student_Mykad
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Mykad")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Student_Mykad")
                Else
                    strMsg += " Please Enter Student_Mykad |"
                End If

                'Class_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Class_Name")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Class_Name")
                Else
                    strMsg += " Please Enter Class_Name |"
                End If
                'Subject_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Name")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Subject_Name")
                Else
                    strMsg += " Please Enter Subject_Name |"
                End If

                'Exam_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Exam_Name")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Exam_Name")
                Else
                    strMsg += " Please Enter Exam_Name |"
                End If

                'Exam_Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Exam_Year")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Exam_Year")
                Else
                    strMsg += " Please Enter Exam_Year |"
                End If

                'Marks
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Marks")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Marks")
                Else
                    strMsg += " Please Enter Marks |"
                End If

                'Grade
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Grade")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Grade")
                Else
                    strMsg += " Please Enter Grade |"
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg = "BIL# :" & strBil & " Name " & strStudentName & ":" & strStudentMykad & strMsg
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim updateCount As Integer = 0
        Dim insertCount As Integer = 0

        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")
            strStudentName = SiteData.Tables(0).Rows(i).Item("Student_Name")
            strStudentMykad = SiteData.Tables(0).Rows(i).Item("Student_Mykad")
            strClassName = SiteData.Tables(0).Rows(i).Item("Class_Name")
            strSubjectName = SiteData.Tables(0).Rows(i).Item("Subject_Name")
            strExamName = SiteData.Tables(0).Rows(i).Item("Exam_Name")
            strExamYear = SiteData.Tables(0).Rows(i).Item("Exam_Year")
            strMarks = SiteData.Tables(0).Rows(i).Item("Marks")
            strGrade = SiteData.Tables(0).Rows(i).Item("Grade")

            ''GET STD ID
            strSQL = "  SELECT student_info.std_ID FROM student_info
		                    LEFT JOIN student_level ON student_info.std_ID = student_level.std_ID
		                    WHERE 
		                    student_info.student_Name = '" & strStudentName & "'
		                    AND student_info.student_Mykad = '" & strStudentMykad & "'
		                    AND student_level.year = '" & strExamYear & "'"
            Dim stdID As String = oCommon.getFieldValue(strSQL)

            ''GET CLASS ID
            strSQL = "  SELECT class_ID FROM class_info
		                    WHERE class_Name = '" & strClassName & "'
		                    AND class_year = '" & strExamYear & "'"
            Dim classID As String = oCommon.getFieldValue(strSQL)

            ''GET CLASS LEVEL FOR SUBJECT ID
            strSQL = "SELECT class_Level FROM class_info WHERE class_Name = '" & strClassName & "'"
            Dim classLevel As String = oCommon.getFieldValue(strSQL)

            ''GET STUDENT SEMESTER FOR SUBJECT ID
            Dim studentSem As String
            If strExamName = "Exam 1" Or strExamName = "Exam 2" Or strExamName = "Exam 5" Or strExamName = "Exam 6" Then
                studentSem = "Sem 1"
            Else
                studentSem = "Sem 2"
            End If

            ''GET SUBJECT ID
            strSQL = "  SELECT subject_ID FROM subject_info
		                    WHERE subject_Name = '" & strSubjectName & "'
		                    AND subject_year = '" & strExamYear & "'
		                    AND subject_StudentYear = '" & classLevel & "'
		                    AND subject_sem = '" & studentSem & "'
		                    GROUP BY subject_ID"
            Dim subjectID As String = oCommon.getFieldValue(strSQL)

            ''GET EXAM ID
            strSQL = "  SELECT exam_ID FROM exam_Info
		                    WHERE exam_Name = '" & strExamName & "'
		                    AND exam_Year = '" & strExamYear & "'"
            Dim examID As String = oCommon.getFieldValue(strSQL)

            ''GET COURSE ID
            strSQL = "  SELECT course_ID FROM course
		                    WHERE std_ID = '" & stdID & "'
		                    AND class_ID = '" & classID & "'
		                    AND subject_ID = '" & subjectID & "'
		                    AND year = '" & strExamYear & "'"
            Dim courseID As String = oCommon.getFieldValue(strSQL)

            ''CHECK IF EXIST
            strSQL = "  SELECT ID FROM exam_result
                            WHERE course_ID = '" & courseID & "'
                            AND exam_ID = '" & examID & "'"
            Dim id As String = oCommon.getFieldValue(strSQL)
            If oCommon.isExist(strSQL) = True Then
                ''UPDATE
                strSQL = "  UPDATE exam_result SET
                                marks = '" & strMarks & "',
                                grade = UPPER('" & strGrade & "')
                                WHERE
                                ID = '" & id & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ''COUNT UPDATE
                If strRet = 0 Then
                    updateCount = updateCount + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

            Else
                ''INSERT
                strSQL = "  INSERT INTO exam_result
                                (
                                exam_ID,
                                course_ID,
                                marks,
                                grade
                                )"
                strSQL += " VALUES
                                (
                                '" & examID & "',
                                '" & courseID & "',
                                '" & strMarks & "',
                                UPPER('" & strGrade & "')
                                )"
                strRet = oCommon.ExecuteSQL(strSQL)

                ''COUNT INSERT
                If strRet = 0 Then
                    insertCount = insertCount + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(insertCount & " rows inserted and " & updateCount & " rows updated in database", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        strBil = ""
        strStudentName = ""
        strStudentMykad = ""
        strClassName = ""
        strSubjectName = ""
        strExamName = ""
        strExamYear = ""
        strMarks = ""
        strGrade = ""

    End Sub

    Private Sub BtnUploaded_ServerClick(sender As Object, e As EventArgs) Handles BtnUploaded.ServerClick
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"
            Else
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try
    End Sub

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/student_marks.xlsx")
    End Sub
    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class