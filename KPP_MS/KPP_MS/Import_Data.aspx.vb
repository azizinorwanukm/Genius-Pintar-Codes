Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Import_Data
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strBil As String = ""
    Dim strNAMA As String = ""
    Dim strNOKP As String = ""
    Dim strLevel As String = ""
    Dim strKELAS As String = ""
    Dim strKursus As String = ""
    Dim strSubjek As String = ""
    Dim strPEPERIKSAAN As String = ""
    Dim strTahun As String = ""
    Dim strMARKAH As String = ""
    Dim strtGRADE As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/class_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [Sheet1$]", connection)
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
                    divMsg.Attributes("arab") = "error"
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Kelas:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error: HERE 1 : " & ex.Message
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
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")
                refreshVar()
                strMsg = ""

                'Bil
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("BIL")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("BIL")
                End If

                'Student Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("NAMA")) Then
                    strNAMA = SiteData.Tables(0).Rows(i).Item("NAMA")
                End If

                'Student Mykad
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("NO KP")) Then
                    strNOKP = SiteData.Tables(0).Rows(i).Item("NO KP")
                End If

                'Student Level
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("ASAS/TAHAP")) Then
                    strLevel = SiteData.Tables(0).Rows(i).Item("ASAS/TAHAP")
                End If

                'Class Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KELAS")) Then
                    strKELAS = SiteData.Tables(0).Rows(i).Item("KELAS")
                End If

                'Course Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KURSUS")) Then
                    strKursus = SiteData.Tables(0).Rows(i).Item("KURSUS")
                End If

                'Subject Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("SUBJEK")) Then
                    strSubjek = SiteData.Tables(0).Rows(i).Item("SUBJEK")
                End If

                'Peperiksaan 
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("PEPERIKSAAN")) Then
                    strPEPERIKSAAN = SiteData.Tables(0).Rows(i).Item("PEPERIKSAAN")
                End If

                'Tahun 
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("TAHUN")) Then
                    strTahun = SiteData.Tables(0).Rows(i).Item("TAHUN")
                End If

                'MARKAH 
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MARKAH")) Then
                    strTahun = SiteData.Tables(0).Rows(i).Item("MARKAH")
                End If

                'GRADE 
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("GRADE")) Then
                    strTahun = SiteData.Tables(0).Rows(i).Item("GRADE")
                End If

                If strMsg.Length = 0 Then

                Else
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

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0

        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")

            strBil = SiteData.Tables(0).Rows(i).Item("BIL")
            strNAMA = SiteData.Tables(0).Rows(i).Item("NAMA")
            strNOKP = SiteData.Tables(0).Rows(i).Item("NO KP")
            strLevel = SiteData.Tables(0).Rows(i).Item("ASAS/TAHAP")
            strKELAS = SiteData.Tables(0).Rows(i).Item("KELAS")
            strKursus = SiteData.Tables(0).Rows(i).Item("KURSUS")
            strSubjek = SiteData.Tables(0).Rows(i).Item("SUBJEK")
            strPEPERIKSAAN = SiteData.Tables(0).Rows(i).Item("PEPERIKSAAN")
            strTahun = SiteData.Tables(0).Rows(i).Item("TAHUN")
            strMARKAH = SiteData.Tables(0).Rows(i).Item("MARKAH")
            strtGRADE = SiteData.Tables(0).Rows(i).Item("GRADE")

            ''get student ID 
            Dim get_stdID As String = "Select std_ID from student_info where student_Mykad = '" & strNOKP & "' and student_Status = 'Access'"
            Dim data_stdID As String = oCommon.getFieldValue(get_stdID)

            ''get class_ID
            Dim get_classID As String = "select class_ID from class_info where class_year = '" & strTahun & "' and class_Name = '" & strKELAS & "'"
            Dim data_classID As String = oCommon.getFieldValue(get_classID)

            ''get student semester
            Dim get_Semester As String = ""

            If strPEPERIKSAAN = "EXAM 1" Or strPEPERIKSAAN = "EXAM 2" Or strPEPERIKSAAN = "EXAM 5" Or strPEPERIKSAAN = "EXAM 6" Or strPEPERIKSAAN = "EXAM 9" Or strPEPERIKSAAN = "EXAM 10" Then
                get_Semester = "Sem 1"

            ElseIf strPEPERIKSAAN = "EXAM 3" Or strPEPERIKSAAN = "EXAM 4" Or strPEPERIKSAAN = "EXAM 7" Or strPEPERIKSAAN = "EXAM 8" Or strPEPERIKSAAN = "EXAM 11" Or strPEPERIKSAAN = "EXAM 12" Then
                get_Semester = "Sem 2"

            End If

            ''get subject ID
            Dim get_subject_ID As String = "select subject_ID from subject_info where course_Name = '" & strKursus & "' and subject_NameBM = '" & strSubjek & "' and subject_year = '" & strTahun & "' and subject_StudentYear = '" & strLevel & "' and subject_Sem = '" & get_Semester & "'"
            Dim data_subject_ID As String = oCommon.getFieldValue(get_subject_ID)

            ''check if student level is exist
            Dim get_student_level As String = "select ID from student_level where std_ID = '" & data_stdID & "' and year = '" & strTahun & "' and student_Sem = '" & get_Semester & "' and student_Level = '" & strLevel & "' "
            Dim data_student_level As String = oCommon.getFieldValue(get_student_level)

            ''insert into table student_level
            If data_student_level = "" Then

                Using CLASSDATA As New SqlCommand("INSERT INTO student_level(std_ID, student_Sem , student_Level, year, month, day) VALUES ('" & data_stdID & "', '" & get_Semester & "' , '" & strLevel & "', '" & strTahun & "' , '1', '15')", objConn)
                    objConn.Open()
                    Dim j = CLASSDATA.ExecuteNonQuery()
                    objConn.Close()
                End Using

            End If

            ''check if the student, subject , class had been register in tahble course
            Dim chechk_CourseID As String = "select course_ID from std_ID = '" & data_stdID & "' and class_ID = '" & data_classID & "' and subject_ID = '" & data_subject_ID & "' and year = '" & strTahun & "'"
            Dim confirm_CourseID As String = oCommon.getFieldValue(chechk_CourseID)

            ''insert into table course
            If confirm_CourseID = "" Then

                Using CLASSDATA As New SqlCommand("INSERT INTO course(std_ID, subject_ID , class_ID, year) VALUES ('" & data_stdID & "', '" & data_subject_ID & "' , '" & data_classID & "', '" & strTahun & "' , '1', '15')", objConn)
                    objConn.Open()
                    Dim j = CLASSDATA.ExecuteNonQuery()
                    objConn.Close()
                End Using

                ''get the course ID
                chechk_CourseID = "select course_ID from std_ID = '" & data_stdID & "' and class_ID = '" & data_classID & "' and subject_ID = '" & data_subject_ID & "' and year = '" & strTahun & "'"
                confirm_CourseID = oCommon.getFieldValue(chechk_CourseID)
            End If

            ''get exam_ID 
            Dim get_ExamID As String = "Select exam_ID from exam_info where exam_Year = '" & strTahun & "' and exam_Name = '" & strPEPERIKSAAN & "'"
            Dim data_ExamID As String = oCommon.getFieldValue(get_ExamID)

            ''check if the course_ID , exam_ID have register into tanle exam_result
            Dim get_ExamResultID As String = "select ID from exam_result where course_ID = '" & confirm_CourseID & "' and exam_ID = '" & data_ExamID & "' "
            Dim data_ExamResultID As String = oCommon.getFieldValue(get_ExamResultID)

            If data_ExamResultID = "" Then

                ''insert data into table exam_result
                Using CLASSDATA As New SqlCommand("INSERT INTO exam_result(exam_ID, course_ID , marks, grade) VALUES ('" & data_ExamID & "', '" & confirm_CourseID & "' , '" & strMARKAH & "', '" & strtGRADE & "')", objConn)
                    objConn.Open()
                    Dim j = CLASSDATA.ExecuteNonQuery()
                    objConn.Close()
                End Using

            Else

                'update data into table exam_result
                strSQL = "UPDATE exam_result SET marks ='" & strMARKAH & "', grade ='" & strtGRADE & "' WHERE ID ='" & data_ExamResultID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

            countInsert = countInsert + 1
        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database ", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        strBil = ""
        strNAMA = ""
        strNOKP = ""
        strLevel = ""
        strKELAS = ""
        strKursus = ""
        strSubjek = ""
        strPEPERIKSAAN = ""
        strTahun = ""
        strMARKAH = ""
        strtGRADE = ""

    End Sub

    Private Sub BtnUploaded_ServerClick(sender As Object, e As EventArgs) Handles BtnUploaded.ServerClick
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("course") = "info"
            Else
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/student_past_exam_data.xlsx")
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class