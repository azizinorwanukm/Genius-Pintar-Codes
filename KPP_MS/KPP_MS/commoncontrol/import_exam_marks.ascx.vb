Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb

Public Class import_exam_marks
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strBil As String = ""
    Dim strStudentName As String = ""
    Dim strStudentMykad As String = ""
    Dim strStudentCampus As String = ""
    Dim strStudentProgram As String = ""
    Dim strClassName As String = ""
    Dim strSubjectName As String = ""
    Dim strExamName As String = ""
    Dim strExamYear As String = ""
    Dim strMarks As String = ""
    Dim strGrade As String = ""

    Dim strBil_CGPA As String = ""
    Dim strID_CGPA As String = ""
    Dim strIC_CGPA As String = ""
    Dim strPNGS_CGPA As String = ""
    Dim strPNGK_CGPA As String = ""
    Dim strEXAM_CGPA As String = ""
    Dim strYEAR_CGPA As String = ""
    Dim strTYPE_CGPA As String = ""
    Dim strKOMP_AKADEMIK As String = ""
    Dim strKOMP_KOKURIKULUM As String = ""
    Dim strKOMP_PORTFOLIO As String = ""
    Dim strKOMP_PENYELIDIKAN As String = ""
    Dim strKOMP_KENDIRI As String = ""
    Dim strSTAT_AKADEMIK As String = ""
    Dim strSTAT_KOKURIKULUM As String = ""
    Dim strSTAT_PORTFOLIO As String = ""
    Dim strSTAT_PENYELIDIKAN As String = ""
    Dim strSTAT_KENDIRI As String = ""

    Dim ImportExaminationResult_Year As String = ""
    Dim ImportExaminationResult_Exam As String = ""
    Dim ImportExaminationResult_Class As String = ""
    Dim ImportExaminationResult_Subject As String = ""
    Dim ImportExaminationResult_Campus As String = ""
    Dim ImportExaminationResult_Program As String = ""

    Dim ImportGPACGPA_Year As String = ""
    Dim ImportGPACGPA_Exam As String = ""
    Dim ImportGPACGPA_Type As String = ""

    Dim ImportKOKO_KP As String = ""
    Dim ImportKOKO_MARK As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "IER" Then ''Import Examination Result
                    txtbreadcrum1.Text = "Import Examination Result"

                    ImportExaminationResult.Visible = True
                    ImportGPACGPA.Visible = False
                    ViewImportExaminationResult.Visible = False
                    ImportKOKO.Visible = False

                    btnImportExaminationResult.Attributes("class") = "btn btn-info"
                    btnImportGPACGPA.Attributes("class") = "btn btn-default font"
                    btnImportKOKO.Attributes("class") = "btn btn-default font"


                ElseIf Session("getStatus") = "IGC" Then ''Import GPA & CGPA
                    txtbreadcrum1.Text = "Import GPA & CGPA"

                    ImportExaminationResult.Visible = False
                    ImportGPACGPA.Visible = True
                    ImportKOKO.Visible = False

                    btnImportExaminationResult.Attributes("class") = "btn btn-default font"
                    btnImportGPACGPA.Attributes("class") = "btn btn-info"
                    btnImportKOKO.Attributes("class") = "btn btn-default font"

                ElseIf Session("getStatus") = "IKOKO" Then ''Import KOKO
                    txtbreadcrum1.Text = "Import Kokurrikulum"

                    ImportExaminationResult.Visible = False
                    ImportGPACGPA.Visible = False
                    ImportKOKO.Visible = True

                    btnImportExaminationResult.Attributes("class") = "btn btn-default font"
                    btnImportGPACGPA.Attributes("class") = "btn btn-default font"
                    btnImportKOKO.Attributes("class") = "btn btn-info"

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnImportExaminationResult.Visible = False
        btnImportGPACGPA.Visible = False
        ImportExaminationResult.Visible = False
        ImportGPACGPA.Visible = False
        ImportKOKO.Visible = False

        BtnUploadedImportExaminationResult.Visible = False
        BtnUploadedImportGPACGPA.Visible = False
        BtnUploadedImportKOKO.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ImportExaminationResult As String = ""
        Dim Get_ImportGPACGPA As String = ""
        Dim Get_ImportKOKO As String = ""

        ''Loop The Count_No
        For num As Integer = 0 To find_CountNo_LoginID - 1 Step 1

            ''Get Main Menu Data
            strSQL = "  Select A.Menu From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_Menu_Data As String = oCommon.getFieldValue(strSQL)

            ''Get Sub Menu 2 Data
            strSQL = "  Select A.Menu_Sub2 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_SubMenu2 As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Import Data 
            strSQL = "  Select B.F1_Import From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Import As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "Import Examination Result" And find_Data_SubMenu2.Length > 0 Then
                btnImportExaminationResult.Visible = True
                ImportExaminationResult.Visible = True

                Get_ImportExaminationResult = "TRUE"

                If find_Data_F1Import.Length > 0 And find_Data_F1Import = "TRUE" Then
                    BtnUploadedImportExaminationResult.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Import GPA & CGPA" And find_Data_SubMenu2.Length > 0 Then
                btnImportGPACGPA.Visible = True
                ImportGPACGPA.Visible = True

                Get_ImportGPACGPA = "TRUE"

                If find_Data_F1Import.Length > 0 And find_Data_F1Import = "TRUE" Then
                    BtnUploadedImportGPACGPA.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnImportExaminationResult.Visible = True
                btnImportGPACGPA.Visible = True
                btnImportKOKO.Visible = True
                ImportExaminationResult.Visible = True
                ImportGPACGPA.Visible = True
                ImportKOKO.Visible = True

                BtnUploadedImportExaminationResult.Visible = True
                BtnUploadedImportGPACGPA.Visible = True
                BtnUploadedImportKOKO.Visible = True

                Get_ImportExaminationResult = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "IER" Or Session("getStatus") = "IGC" Or Session("getStatus") = "IKOKO" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "IER" And Session("getStatus") <> "IGC" And Session("getStatus") <> "IKOKO" Then
            If Get_ImportExaminationResult = "TRUE" Then
                Data_If_Not_Group_Status = "IER"
            ElseIf Get_ImportGPACGPA = "TRUE" Then
                Data_If_Not_Group_Status = "IGC"
            ElseIf Get_ImportKOKO = "TRUE" Then
                Data_If_Not_Group_Status = "IKOKO"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ImportExaminationResult = "TRUE" And Data_If_Not_Group_Status = "IER" Then
                Session("getStatus") = "IER"
            ElseIf Get_ImportGPACGPA = "TRUE" And Data_If_Not_Group_Status = "IGC" Then
                Session("getStatus") = "IGC"
            ElseIf Get_ImportKOKO = "TRUE" And Data_If_Not_Group_Status = "IKOKO" Then
                Session("getStatus") = "IKOKO"
            End If
        End If

    End Sub

    Private Sub btnImportExaminationResult_ServerClick(sender As Object, e As EventArgs) Handles btnImportExaminationResult.ServerClick
        Session("getStatus") = "IER"
        Response.Redirect("admin_importexport_peperiksaan_markah.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnImportGPACGPA_ServerClick(sender As Object, e As EventArgs) Handles btnImportGPACGPA.ServerClick
        Session("getStatus") = "IGC"
        Response.Redirect("admin_importexport_peperiksaan_markah.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnImportKOKO_ServerClick(sender As Object, e As EventArgs) Handles btnImportKOKO.ServerClick
        Session("getStatus") = "IKOKO"
        Response.Redirect("admin_importexport_peperiksaan_markah.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Function ImportExcelImportExaminationResult() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/exam_import"))

        If FileUploadImportExaminationResult.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FileUploadImportExaminationResult.FileName
            FileUploadImportExaminationResult.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [marks$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteDataImportExaminationResult(ds)
                If validationMessage = "" Then

                    Dim resultData As String = SaveSiteDataImportExaminationResult(ds)

                    If resultData = "True" Then
                        ViewImportExaminationResult.Visible = True
                        strRet = BindData(datRespondent)
                    End If

                Else
                    ShowMessage(" Please Fill In Examination Result Correctly ", MessageType.Error)
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            ShowMessage(" Please Select File To Upload ", MessageType.Error)
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteDataImportExaminationResult(ByVal SiteData As DataSet) As String
        Try

            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVarImportExaminationResult()
                strMsg = ""

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

    Private Function SaveSiteDataImportExaminationResult(ByVal SiteData As DataSet) As String

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim updateCount As Integer = 0
        Dim insertCount As Integer = 0

        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")
            strStudentMykad = SiteData.Tables(0).Rows(i).Item("Student_Mykad")
            strStudentCampus = SiteData.Tables(0).Rows(i).Item("Student_Campus")
            strStudentProgram = SiteData.Tables(0).Rows(i).Item("Student_Program")
            strClassName = SiteData.Tables(0).Rows(i).Item("Class_Name")
            strSubjectName = SiteData.Tables(0).Rows(i).Item("Subject_Name")
            strExamName = SiteData.Tables(0).Rows(i).Item("Exam_Name")
            strExamYear = SiteData.Tables(0).Rows(i).Item("Exam_Year")
            strMarks = SiteData.Tables(0).Rows(i).Item("Marks")
            strGrade = SiteData.Tables(0).Rows(i).Item("Grade")
            ImportExaminationResult_Year = strExamYear
            ImportExaminationResult_Campus = strStudentCampus
            ImportExaminationResult_Program = strStudentProgram

            ''GET STD ID
            strSQL = "  SELECT student_info.std_ID FROM student_info
		                LEFT JOIN student_level ON student_info.std_ID = student_level.std_ID
		                WHERE student_info.student_Mykad = '" & strStudentMykad & "'
		                AND student_level.year = '" & strExamYear & "'"
            Dim stdID As String = oCommon.getFieldValue(strSQL)

            ''GET CLASS ID
            strSQL = "  SELECT class_ID FROM class_info
		                WHERE class_Name = '" & strClassName & "' and course_Program = '" & strStudentProgram & "' and class_Campus = '" & strStudentCampus & "'
		                AND class_year = '" & strExamYear & "'"
            Dim classID As String = oCommon.getFieldValue(strSQL)

            ''GET CLASS LEVEL FOR SUBJECT ID
            strSQL = "  SELECT class_Level FROM class_info WHERE class_Name = '" & strClassName & "' and course_Program = '" & strStudentProgram & "' and class_Campus = '" & strStudentCampus & "'
		                AND class_year = '" & strExamYear & "'"
            Dim classLevel As String = oCommon.getFieldValue(strSQL)
            ImportExaminationResult_Class = classLevel

            ''GET STUDENT SEMESTER FOR SUBJECT ID
            Dim studentSem As String
            If strExamName = "Exam 1" Or strExamName = "Exam 2" Or strExamName = "Exam 5" Or strExamName = "Exam 6" Or strExamName = "Exam 9" Or strExamName = "Exam 10" Then
                studentSem = "Sem 1"
            Else
                studentSem = "Sem 2"
            End If

            ''GET SUBJECT ID
            strSQL = "  SELECT subject_ID FROM subject_info
		                WHERE subject_Name = '" & strSubjectName & "' and course_Program = '" & strStudentProgram & "' and subject_Campus = '" & strStudentCampus & "'
		                AND subject_year = '" & strExamYear & "'
		                AND subject_StudentYear = '" & classLevel & "'
		                AND subject_sem = '" & studentSem & "'
		                GROUP BY subject_ID"
            Dim subjectID As String = oCommon.getFieldValue(strSQL)
            ImportExaminationResult_Subject = subjectID

            ''GET EXAM ID
            strSQL = "  SELECT exam_ID FROM exam_Info
		                WHERE exam_Name = '" & strExamName & "'
		                AND exam_Year = '" & strExamYear & "'"
            Dim examID As String = oCommon.getFieldValue(strSQL)
            ImportExaminationResult_Exam = examID

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
                strSQL = "  INSERT INTO exam_result( exam_ID,course_ID, marks,grade)"
                strSQL += " VALUES  ('" & examID & "','" & courseID & "','" & strMarks & "',UPPER('" & strGrade & "'))"
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
            value = "True"

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Error)
            value = "False"

        End If

        Return value

    End Function

    Private Sub refreshVarImportExaminationResult()
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

    Private Sub BtnUploadedImportExaminationResult_ServerClick(sender As Object, e As EventArgs) Handles BtnUploadedImportExaminationResult.ServerClick
        Try
            '--upload excel
            If ImportExcelImportExaminationResult() = True Then
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnDownloadImportExaminationResult_ServerClick(sender As Object, e As EventArgs) Handles BtnDownloadImportExaminationResult.ServerClick
        Response.Redirect("download/student_marks.xlsx")
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = "order by class_info.class_Name, student_info.student_Name, exam_result.marks ASC"

        tmpSQL = "select distinct exam_result.course_ID, exam_result.ID, student_info.student_ID, student_info.student_Name, class_info.class_Name, exam_Info.exam_Name, subject_info.subject_Name, exam_result.marks, exam_result.grade
                  From exam_result Join course On exam_result.course_ID = course.course_ID
                  Left Join exam_info On exam_result.exam_ID = exam_Info.exam_ID
                  Left Join student_info On course.std_ID = student_info.std_ID
                  Left Join class_info On course.class_ID = class_info.class_ID
                  Left Join subject_info On course.subject_ID = subject_info.subject_ID left Join student_Png On student_info.std_ID=student_Png.std_ID
                  Where exam_result.ID Is Not null and (student_info.student_status = 'Access' or student_info.student_Status = 'Graduate') and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"
        strWhere += " And exam_Info.exam_Year = '" & ImportExaminationResult_Year & "'"
        strWhere += " And exam_Info.exam_ID = '" & ImportExaminationResult_Exam & "'"
        strWhere += " And class_info.class_Level = '" & ImportExaminationResult_Class & "'"
        strWhere += " And course.subject_ID = '" & ImportExaminationResult_Subject & "'"
        strWhere += " And student_info.student_Stream = '" & ImportExaminationResult_Campus & "'"
        strWhere += " And student_info.student_Campus = '" & ImportExaminationResult_Program & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Function ImportExcelImportGPACGPA() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/student_gpa_cgpa"))

        If FileUploadImportGPACGPA.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FileUploadImportGPACGPA.FileName
            FileUploadImportGPACGPA.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [exam$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteDataImportGPACGPA(ds)
                If validationMessage = "" Then

                    Dim resultData As String = SaveSiteDataImportGPACGPA(ds)

                    If resultData = "True" Then
                        ViewImportGPACGPA.Visible = True
                        strRet = BindDataGPACGPA(datRespondentGPACGPA)
                    End If

                Else
                    ShowMessage(" Please Fill In Information Correctly ", MessageType.Error)
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            ShowMessage(" Please Select File To Upload ", MessageType.Error)
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteDataImportGPACGPA(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVarImportGPACGPA()
                strMsg = ""

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Bil")) Then
                    strBil_CGPA = SiteData.Tables(0).Rows(i).Item("Bil")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MYKAD")) Then
                    strIC_CGPA = SiteData.Tables(0).Rows(i).Item("MYKAD")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("PNGS")) Then
                    strPNGS_CGPA = SiteData.Tables(0).Rows(i).Item("PNGS")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("PNGK")) Then
                    strPNGK_CGPA = SiteData.Tables(0).Rows(i).Item("PNGK")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("EXAM")) Then
                    strEXAM_CGPA = SiteData.Tables(0).Rows(i).Item("EXAM")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("YEAR")) Then
                    strYEAR_CGPA = SiteData.Tables(0).Rows(i).Item("YEAR")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("TYPE")) Then
                    strTYPE_CGPA = SiteData.Tables(0).Rows(i).Item("TYPE")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KOMP_AKADEMIK")) Then
                    strKOMP_AKADEMIK = SiteData.Tables(0).Rows(i).Item("KOMP_AKADEMIK")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KOMP_KOKURIKULUM")) Then
                    strKOMP_KOKURIKULUM = SiteData.Tables(0).Rows(i).Item("KOMP_KOKURIKULUM")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KOMP_PORTFOLIO")) Then
                    strKOMP_PORTFOLIO = SiteData.Tables(0).Rows(i).Item("KOMP_PORTFOLIO")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KOMP_PENYELIDIKAN")) Then
                    strKOMP_PENYELIDIKAN = SiteData.Tables(0).Rows(i).Item("KOMP_PENYELIDIKAN")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KOMP_KENDIRI")) Then
                    strKOMP_KENDIRI = SiteData.Tables(0).Rows(i).Item("KOMP_KENDIRI")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("STAT_AKADEMIK")) Then
                    strSTAT_AKADEMIK = SiteData.Tables(0).Rows(i).Item("STAT_AKADEMIK")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("STAT_KOKURIKULUM")) Then
                    strSTAT_KOKURIKULUM = SiteData.Tables(0).Rows(i).Item("STAT_KOKURIKULUM")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("STAT_PORTFOLIO")) Then
                    strSTAT_PORTFOLIO = SiteData.Tables(0).Rows(i).Item("STAT_PORTFOLIO")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("STAT_PENYELIDIKAN")) Then
                    strSTAT_PENYELIDIKAN = SiteData.Tables(0).Rows(i).Item("STAT_PENYELIDIKAN")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("STAT_KENDIRI")) Then
                    strSTAT_KENDIRI = SiteData.Tables(0).Rows(i).Item("STAT_KENDIRI")
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

    Private Function SaveSiteDataImportGPACGPA(ByVal SiteData As DataSet) As String

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0


        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil_CGPA = SiteData.Tables(0).Rows(i).Item("Bil")
            strIC_CGPA = SiteData.Tables(0).Rows(i).Item("MYKAD")
            strPNGS_CGPA = SiteData.Tables(0).Rows(i).Item("PNGS")
            strPNGK_CGPA = SiteData.Tables(0).Rows(i).Item("PNGK")
            strEXAM_CGPA = SiteData.Tables(0).Rows(i).Item("EXAM")
            strYEAR_CGPA = SiteData.Tables(0).Rows(i).Item("YEAR")
            strTYPE_CGPA = SiteData.Tables(0).Rows(i).Item("TYPE")
            strKOMP_AKADEMIK = SiteData.Tables(0).Rows(i).Item("KOMP_AKADEMIK")
            strKOMP_KOKURIKULUM = SiteData.Tables(0).Rows(i).Item("KOMP_KOKURIKULUM")
            strKOMP_PORTFOLIO = SiteData.Tables(0).Rows(i).Item("KOMP_PORTFOLIO")
            strKOMP_PENYELIDIKAN = SiteData.Tables(0).Rows(i).Item("KOMP_PENYELIDIKAN")
            strKOMP_KENDIRI = SiteData.Tables(0).Rows(i).Item("KOMP_KENDIRI")
            strSTAT_AKADEMIK = SiteData.Tables(0).Rows(i).Item("STAT_AKADEMIK")
            strSTAT_KOKURIKULUM = SiteData.Tables(0).Rows(i).Item("STAT_KOKURIKULUM")
            strSTAT_PORTFOLIO = SiteData.Tables(0).Rows(i).Item("STAT_PORTFOLIO")
            strSTAT_PENYELIDIKAN = SiteData.Tables(0).Rows(i).Item("STAT_PENYELIDIKAN")
            strSTAT_KENDIRI = SiteData.Tables(0).Rows(i).Item("STAT_KENDIRI")

            ImportGPACGPA_Year = strYEAR_CGPA
            ImportGPACGPA_Exam = strEXAM_CGPA
            ImportGPACGPA_Type = strTYPE_CGPA

            Dim get_exist As String = "select student_Png.std_Id from student_info left join student_Png where student_Mykad = '" & strIC_CGPA & "' and exam_Name = '" & strEXAM_CGPA & "' and year = '" & strYEAR_CGPA & "'"
            Dim data_exist As String = oCommon.getFieldValue(get_exist)

            If data_exist.Length > 0 Then
                ''update table
                strSQL = "  update student_Png set png = '" & strPNGS_CGPA & "', pngs = '" & strPNGK_CGPA & "', exam_Name = '" & strEXAM_CGPA & "', year = '" & strYEAR_CGPA & "', student_type = '" & strTYPE_CGPA & "'
                            , stat_akademik = '" & strSTAT_AKADEMIK & "', stat_kokurikulum = '" & strSTAT_KOKURIKULUM & "', stat_portfolio = '" & strSTAT_PORTFOLIO & "', stat_penyelidikan = '" & strSTAT_PENYELIDIKAN & "', stat_kendiri = '" & strSTAT_KENDIRI & "'
                            , komp_akademik = '" & strKOMP_AKADEMIK & "', komp_kokurikulum = '" & strKOMP_KOKURIKULUM & "', komp_portfolio = '" & strKOMP_PORTFOLIO & "', komp_penyelidikan = '" & strKOMP_PENYELIDIKAN & "', komp_kendiri = '" & strKOMP_KENDIRI & "'
                            where std_ID = '" & data_exist & "' and year = '" & strYEAR_CGPA & "' and exam_Name = '" & strEXAM_CGPA & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            Else
                ''insert new

                Dim get_stdID As String = "select std_Id from student_info where student_Mykad = '" & strIC_CGPA & "' and student_info.student_status = 'Access' and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"
                Dim data_stdID As String = oCommon.getFieldValue(get_stdID)

                strSQL = "  insert into student_Png(std_ID,png,pngs,exam_Name,year,student_type,komp_akademik,komp_kokurikulum,komp_portfolio_komp_penyelidikan,komp_kendiri,stat_akademik,stat_kokurikulum,stat_portfolio,stat_penyelidikan,stat_kendiri) 
                            values('" & data_stdID & "', '" & strPNGS_CGPA & "', '" & strPNGK_CGPA & "', '" & strEXAM_CGPA & "', '" & strYEAR_CGPA & "', '" & strTYPE_CGPA & "'
                            , '" & strKOMP_AKADEMIK & "', '" & strKOMP_KOKURIKULUM & "', '" & strKOMP_PORTFOLIO & "', '" & strKOMP_PENYELIDIKAN & "', '" & strKOMP_KENDIRI & "'
                            , '" & strSTAT_AKADEMIK & "', '" & strSTAT_KOKURIKULUM & "', '" & strSTAT_PORTFOLIO & "', '" & strSTAT_PENYELIDIKAN & "', '" & strSTAT_KENDIRI & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

            If strRet = 0 Then
                errorData = 0
                countInsert = countInsert + 1
            Else
                errorData = 1
            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database ", MessageType.Success)
            value = "True"

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = "False"

        End If

        Return value

    End Function

    Private Sub refreshVarImportGPACGPA()
        strBil_CGPA = ""
        strID_CGPA = ""
        strIC_CGPA = ""
        strPNGS_CGPA = ""
        strPNGK_CGPA = ""
        strEXAM_CGPA = ""
        strYEAR_CGPA = ""
        strTYPE_CGPA = ""
        strKOMP_AKADEMIK = ""
        strKOMP_KOKURIKULUM = ""
        strKOMP_PORTFOLIO = ""
        strKOMP_PENYELIDIKAN = ""
        strKOMP_KENDIRI = ""
        strSTAT_AKADEMIK = ""
        strSTAT_KOKURIKULUM = ""
        strSTAT_PORTFOLIO = ""
        strSTAT_PENYELIDIKAN = ""
        strSTAT_KENDIRI = ""
    End Sub

    Private Sub BtnUploadedImportGPACGPAt_ServerClick(sender As Object, e As EventArgs) Handles BtnUploadedImportGPACGPA.ServerClick
        Try
            '--upload excel
            If ImportExcelImportGPACGPA() = True Then
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnDownloadImportGPACGPA_ServerClick(sender As Object, e As EventArgs) Handles BtnDownloadImportGPACGPA.ServerClick
        Response.Redirect("download/student_pngs_pngk.xlsx")
    End Sub

    Private Function BindDataGPACGPA(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLGPACGPA, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLGPACGPA() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by C.pngs DESC, C.png DESC, A.student_Name ASC"

        tmpSQL = "select distinct A.std_ID, A.student_Name, A.student_Mykad, B.student_Level, E.class_Name, C.exam_Name, C.year, C.png, C.pngs from student_info A
                  left join student_level B on A.std_ID = B.std_ID
                  left join student_Png C on A.std_ID = C.std_ID
                  left join course D on A.std_ID = D.std_ID
                  left join class_info E on D.class_ID = E.class_ID
                  where A.std_ID is not null
                  and (A.student_Status = 'Access' or A.student_Status = 'Graduate') and A.student_ID is not null and A.student_ID <> '' and A.student_ID like '%M%'
                  and E.class_type = 'Compulsory'
                  and D.year = '" & ImportGPACGPA_Year & "' and E.class_year = '" & ImportGPACGPA_Year & "'
                  and B.year = '" & ImportGPACGPA_Year & "' and C.year = '" & ImportGPACGPA_Year & "'"

        strWhere += " and C.exam_Name = '" & ImportGPACGPA_Exam & "'"
        strWhere += " and C.student_type = '" & ImportGPACGPA_Type & "'"

        getSQLGPACGPA = tmpSQL & strWhere & strOrderby

        Return getSQLGPACGPA
    End Function

    Private Function ImportExcelImportKOKO() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/student_gpa_cgpa"))

        If FileUploadImportKOKO.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FileUploadImportKOKO.FileName
            FileUploadImportKOKO.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [Foundation1$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)

                Dim resultData As String = SaveSiteDataImportKOKO(ds)

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

            ''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''

            '--required ms access engine
            Dim excelConnectionStringF2 As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connectionF2 As OleDbConnection = New OleDbConnection(excelConnectionStringF2)
            Dim commandF2 As OleDbCommand = New OleDbCommand("SELECT * FROM [Foundation2$]", connectionF2)
            Dim daF2 As OleDbDataAdapter = New OleDbDataAdapter(commandF2)
            Dim dsF2 As DataSet = New DataSet

            Try
                connectionF2.Open()
                daF2.Fill(dsF2)

                Dim resultData As String = SaveSiteDataImportKOKO(dsF2)

                daF2.Dispose()
                connectionF2.Close()
                commandF2.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connectionF2.State = ConnectionState.Open Then
                    connectionF2.Close()
                End If
            End Try

            ''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''

            '--required ms access engine
            Dim excelConnectionStringF3 As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connectionF3 As OleDbConnection = New OleDbConnection(excelConnectionStringF3)
            Dim commandF3 As OleDbCommand = New OleDbCommand("SELECT * FROM [Foundation3$]", connectionF3)
            Dim daF3 As OleDbDataAdapter = New OleDbDataAdapter(commandF3)
            Dim dsF3 As DataSet = New DataSet

            Try
                connectionF3.Open()
                daF3.Fill(dsF3)

                Dim resultData As String = SaveSiteDataImportKOKO(dsF3)

                daF3.Dispose()
                connectionF3.Close()
                commandF3.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connectionF3.State = ConnectionState.Open Then
                    connectionF3.Close()
                End If
            End Try

            ''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''

            '--required ms access engine
            Dim excelConnectionStringL1 As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connectionL1 As OleDbConnection = New OleDbConnection(excelConnectionStringL1)
            Dim commandL1 As OleDbCommand = New OleDbCommand("SELECT * FROM [Level1$]", connectionL1)
            Dim daL1 As OleDbDataAdapter = New OleDbDataAdapter(commandL1)
            Dim dsL1 As DataSet = New DataSet

            Try
                connectionL1.Open()
                daL1.Fill(dsL1)

                Dim resultData As String = SaveSiteDataImportKOKO(dsL1)

                daL1.Dispose()
                connectionL1.Close()
                commandL1.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connectionL1.State = ConnectionState.Open Then
                    connectionL1.Close()
                End If
            End Try

        Else
            ShowMessage(" Please Select File To Upload ", MessageType.Error)
            Return False
        End If

        Return True

    End Function

    Private Function SaveSiteDataImportKOKO(ByVal SiteData As DataSet) As String

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0


        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")

            ImportKOKO_KP = SiteData.Tables(0).Rows(i).Item("NO KP")
            ImportKOKO_MARK = SiteData.Tables(0).Rows(i).Item("KOKU")

            strSQL = "select StudentID from StudentProfile where MYKAD = '" & ImportKOKO_KP & "'"
            strRet = oCommon.getFieldValue_Permata(strSQL)

            If ImportKOKO_MARK = "75" Then
                strSQL = "  update koko_pelajar set MarkahP1 = '75', GredP1 = 'A-', PNGP1 = '3.45', KOKOP1 = '12.94',
                            MarkahP2 = '75', GredP2 = 'A-', PNGP2 = '3.45', KOKOP2 = '12.94' where StudentID = '" & strRet & "' and Tahun = '2020'"
            ElseIf ImportKOKO_MARK = "85" Then
                strSQL = "  update koko_pelajar set MarkahP1 = '85', GredP1 = 'A', PNGP1 = '3.75', KOKOP1 = '14.0625',
                            MarkahP2 = '85', GredP2 = 'A', PNGP2 = '3.75', KOKOP2 = '14.0625' where  StudentID = '" & strRet & "' and Tahun = '2020'"
            ElseIf ImportKOKO_MARK = "95" Then
                strSQL = "  update koko_pelajar set MarkahP1 = '95', GredP1 = 'A+', PNGP1 = '4.00', KOKOP1 = '15',
                            MarkahP2 = '95', GredP2 = 'A+', PNGP2 = '4.00', KOKOP2 = '15' where StudentID = '" & strRet & "' and Tahun = '2020'"
            End If

            strRet = oCommon.ExecuteSQLPermata(strSQL)

            If strRet = 0 Then
                errorData = 0
                countInsert = countInsert + 1
            Else
                errorData = 1
            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database ", MessageType.Success)
            value = "True"

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = "False"

        End If

        Return value

    End Function

    Private Sub refreshVarImportKOKO()
        ImportKOKO_KP = ""
        ImportKOKO_MARK = ""
    End Sub

    Private Sub BtnUploadedImportKOKO_ServerClick(sender As Object, e As EventArgs) Handles BtnUploadedImportKOKO.ServerClick
        Try
            '--upload excel
            If ImportExcelImportKOKO() = True Then
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class