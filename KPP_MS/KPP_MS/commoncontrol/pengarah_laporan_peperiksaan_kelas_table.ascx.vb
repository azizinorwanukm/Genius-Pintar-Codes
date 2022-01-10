﻿Imports System.Data.SqlClient

Public Class pengarah_laporan_peperiksaan_kelas_table
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim MyConnection As SqlConnection = New SqlConnection(strConn)
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                year_list()
                campus_list()
                program_list
                exam_list()
                class_list()

            End If
        Catch ex As Exception
        End Try
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

    Private Function BindStudentList(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getStudentList, strConn)
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

    Private Sub year_list()

        Try
            strSQL = "SELECT Parameter FROM setting WHERE Type = 'Year'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYearExamination.DataSource = ds
            ddlYearExamination.DataTextField = "Parameter"
            ddlYearExamination.DataValueField = "Parameter"
            ddlYearExamination.DataBind()
            ddlYearExamination.SelectedValue = Now.Year

        Catch ex As Exception

        End Try

    End Sub

    Private Sub campus_list()
        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCampus.DataSource = ds
            ddlCampus.DataTextField = "Parameter"
            ddlCampus.DataValueField = "Value"
            ddlCampus.DataBind()
            ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub program_list()
        If ddlCampus.SelectedValue = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlProgram.DataSource = ds
            ddlProgram.DataTextField = "Parameter"
            ddlProgram.DataValueField = "Value"
            ddlProgram.DataBind()
            ddlProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub exam_list()

        Try
            strSQL = "SELECT exam_Name, exam_ID FROM exam_Info WHERE exam_Year = '" & ddlYearExamination.SelectedValue & "' order by exam_Name asc"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamination.DataSource = ds
            ddlExamination.DataTextField = "exam_Name"
            ddlExamination.DataValueField = "exam_ID"
            ddlExamination.DataBind()
            ddlExamination.Items.Insert(0, New ListItem("Select Exam", String.Empty))

        Catch ex As Exception

        End Try

    End Sub

    Private Sub class_list()

        strSQL = "SELECT exam_Name FROM exam_info WHERE exam_ID = '" & ddlExamination.SelectedValue & "'"
        Dim examName As String = oCommon.getFieldValue(strSQL)

        Try
            strSQL = "  SELECT class_Name, class_ID FROM class_info"
            strSQL += " WHERE class_year = '" & ddlYearExamination.SelectedValue & "' and class_Campus = '" & ddlCampus.SelectedValue & "' and course_Program = '" & ddlProgram.SelectedValue & "'"
            strSQL += " AND class_type = 'Compulsory'"

            If examName = "Exam 1" Or examName = "Exam 2" Or examName = "Exam 3" Or examName = "Exam 4" Then

                strSQL += " AND (class_Level = 'Foundation 1' or class_Level = 'Level 1')"

            ElseIf examName = "Exam 5" Or examName = "Exam 6" Or examName = "Exam 7" Or examName = "Exam 8" Then

                strSQL += " AND (class_Level = 'Foundation 2' or class_Level = 'Level 2')"

            ElseIf examName = "Exam 9" Or examName = "Exam 10" Or examName = "Exam 11" Or examName = "Exam 12" Then

                strSQL += " AND (class_Level = 'Foundation 3')"

            End If

            strSQL += " ORDER BY class_Name"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassExamination.DataSource = ds
            ddlClassExamination.DataTextField = "class_Name"
            ddlClassExamination.DataValueField = "class_ID"
            ddlClassExamination.DataBind()
            ddlClassExamination.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception

        End Try

    End Sub

    Private Function getSQL() As String

        Dim get_Exam As String = "Select exam_Name from exam_Info where exam_ID = '" & ddlExamination.SelectedValue & "'"
        Dim data_exam As String = oCommon.getFieldValue(get_Exam)
        Dim Exam As String = ""

        ''Get Semester
        If data_exam = "Exam 1" Or data_exam = "Exam 2" Or data_exam = "Exam 5" Or data_exam = "Exam 6" Or data_exam = "Exam 9" Or data_exam = "Exam 10" Then
            Exam = "Sem 1"
        ElseIf data_exam = "Exam 3" Or data_exam = "Exam 4" Or data_exam = "Exam 7" Or data_exam = "Exam 8" Or data_exam = "Exam 11" Or data_exam = "Exam 12" Then
            Exam = "Sem 2"
        End If

        ''Get Class Level
        Dim get_class As String = "Select distinct class_level from class_info where class_ID = '" & ddlClassExamination.SelectedValue & "' and class_year = '" & ddlYearExamination.SelectedValue & "'"
        Dim data_class As String = oCommon.getFieldValue(get_class)

        Dim strSelect As String = ""
        Dim strOrderby As String = " ORDER BY subject_info.course_Name ASC"

        strSelect = "   SELECT
                        exam_Info.exam_ID,
                        UPPER(subject_info.subject_Name) subject_Name, subject_info.subject_Code,

                        (select count(*) from course where subject_info.subject_ID = course.subject_ID and subject_year = '" & ddlYearExamination.SelectedValue & "' and subject_StudentYear = '" & data_class & "' 
                        and subject_type = 'Compulsory' and subject_sem = '" & Exam & "' and course.class_ID = '" & ddlClassExamination.SelectedValue & "') as 'Student_Number',
                        
                        COUNT(CASE WHEN exam_result.grade = 'A+' THEN 1 ELSE NULL END) AS 'A+',
                        COUNT(CASE WHEN exam_result.grade = 'A' THEN 1 ELSE NULL END) AS 'A',
                        COUNT(CASE WHEN exam_result.grade = 'A-' THEN 1 ELSE NULL END) AS 'A-',
                        COUNT(CASE WHEN exam_result.grade = 'B+' THEN 1 ELSE NULL END) AS 'B+',
                        COUNT(CASE WHEN exam_result.grade = 'B' THEN 1 ELSE NULL END) AS 'B',
                        COUNT(CASE WHEN exam_result.grade = 'B-' THEN 1 ELSE NULL END) AS 'B-',
                        COUNT(CASE WHEN exam_result.grade = 'C+' THEN 1 ELSE NULL END) AS 'C+',
                        COUNT(CASE WHEN exam_result.grade = 'C' THEN 1 ELSE NULL END) AS 'C',
                        COUNT(CASE WHEN exam_result.grade = 'D' THEN 1 ELSE NULL END) AS 'D',
                        COUNT(CASE WHEN exam_result.grade = 'E' THEN 1 ELSE NULL END) AS 'E',
                        COUNT(CASE WHEN exam_result.grade = 'G' THEN 1 ELSE NULL END) AS 'G'

                        FROM 
                        subject_info
                        LEFT JOIN course ON subject_info.subject_ID = course.subject_ID
                        LEFT JOIN class_info ON course.class_ID = class_info.class_ID
                        LEFT JOIN exam_result ON course.course_ID = exam_result.course_ID
                        LEFT JOIN exam_Info ON exam_result.exam_ID = exam_Info.exam_ID
                                                
                        WHERE 
                        exam_Info.exam_ID = '" & ddlExamination.SelectedValue & "'
                        and exam_Info.exam_year = '" & ddlYearExamination.SelectedValue & "'
                        and subject_info.subject_year = '" & ddlYearExamination.SelectedValue & "' and subject_info.course_Program = '" & ddlProgram.SelectedValue & "' and subject_info.subject_Campus = '" & ddlCampus.SelectedValue & "'
                        and subject_info.subject_StudentYear = '" & data_class & "' and class_info.course_Program = '" & ddlProgram.SelectedValue & "' and class_info.class_Campus = '" & ddlCampus.SelectedValue & "'
                        and subject_info.subject_type = 'Compulsory' 
                        and subject_info.subject_sem = '" & Exam & "'
                        and course.class_ID = '" & ddlClassExamination.SelectedValue & "'

                        GROUP BY
                        exam_Info.exam_ID,
                        subject_info.subject_ID,
                        subject_Name,
                        subject_info.subject_Code,
                        subject_info.subject_StudentYear,
                        subject_info.subject_sem,
                        subject_info.subject_type,
                        subject_info.subject_year

                        Union 

                        SELECT
                        exam_Info.exam_ID,
                        UPPER(subject_info.subject_Name) subject_Name, subject_info.subject_Code,

                        (select count(*) from course left join class_info on course.class_ID = class_info.class_ID where subject_info.subject_ID = course.subject_ID and subject_year = '" & ddlYearExamination.SelectedValue & "' 
                        and subject_StudentYear = '" & data_class & "' and subject_type <> 'Compulsory' and subject_sem = '" & Exam & "' 
                        and course.std_ID in (select distinct std_ID from course where course.class_ID = '" & ddlClassExamination.SelectedValue & "' and course.year = '" & ddlYearExamination.SelectedValue & "' )) as 'Student_Number',
                        
                        COUNT(CASE WHEN exam_result.grade = 'A+' THEN 1 ELSE NULL END) AS 'A+',
                        COUNT(CASE WHEN exam_result.grade = 'A' THEN 1 ELSE NULL END) AS 'A',
                        COUNT(CASE WHEN exam_result.grade = 'A-' THEN 1 ELSE NULL END) AS 'A-',
                        COUNT(CASE WHEN exam_result.grade = 'B+' THEN 1 ELSE NULL END) AS 'B+',
                        COUNT(CASE WHEN exam_result.grade = 'B' THEN 1 ELSE NULL END) AS 'B',
                        COUNT(CASE WHEN exam_result.grade = 'B-' THEN 1 ELSE NULL END) AS 'B-',
                        COUNT(CASE WHEN exam_result.grade = 'C+' THEN 1 ELSE NULL END) AS 'C+',
                        COUNT(CASE WHEN exam_result.grade = 'C' THEN 1 ELSE NULL END) AS 'C',
                        COUNT(CASE WHEN exam_result.grade = 'D' THEN 1 ELSE NULL END) AS 'D',
                        COUNT(CASE WHEN exam_result.grade = 'E' THEN 1 ELSE NULL END) AS 'E',
                        COUNT(CASE WHEN exam_result.grade = 'G' THEN 1 ELSE NULL END) AS 'G'

                        FROM 
                        subject_info
                        LEFT JOIN course ON subject_info.subject_ID = course.subject_ID
                        LEFT JOIN class_info ON course.class_ID = class_info.class_ID
                        LEFT JOIN exam_result ON course.course_ID = exam_result.course_ID
                        LEFT JOIN exam_Info ON exam_result.exam_ID = exam_Info.exam_ID
                                                
                        WHERE 
                        exam_Info.exam_ID = '" & ddlExamination.SelectedValue & "'
                        and exam_Info.exam_year = '" & ddlYearExamination.SelectedValue & "'
                        and subject_info.subject_year = '" & ddlYearExamination.SelectedValue & "' and subject_info.course_Program = '" & ddlProgram.SelectedValue & "' and subject_info.subject_Campus = '" & ddlCampus.SelectedValue & "'
                        and subject_info.subject_StudentYear = '" & data_class & "' and class_info.course_Program = '" & ddlProgram.SelectedValue & "' and class_info.class_Campus = '" & ddlCampus.SelectedValue & "'
                        and subject_info.subject_type <> 'Compulsory' 
                        and subject_info.subject_sem = '" & Exam & "'
                        and course.std_ID in (select distinct std_ID from course where course.class_ID = '" & ddlClassExamination.SelectedValue & "' and course.year = '" & ddlYearExamination.SelectedValue & "')

                        GROUP BY
                        exam_Info.exam_ID,
                        subject_info.subject_ID,
                        subject_Name,
                        subject_info.subject_Code,
                        subject_info.subject_StudentYear,
                        subject_info.subject_sem,
                        subject_info.subject_type,
                        subject_info.subject_year"

        getSQL = strSelect

        Return getSQL

    End Function

    Private Function getStudentList() As String

        Dim strSelect As String = ""
        Dim strWhere As String = ""
        Dim strGroupby As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name"



        strSelect = "SELECT
			        course.std_ID,
			        student_info.student_Name,
			        student_info.student_Mykad,
			        class_info.class_Name,
			        SUM(grade_info.gpa * subject_info.subject_CreditHour) AS 'gpa X credithour',
			        SUM(grade_info.gpa * subject_info.subject_CreditHour) / SUM(subject_info.subject_CreditHour) AS 'Total GPA'
			        FROM
			        exam_result
			        LEFT JOIN course ON exam_result.course_ID = course.course_ID
			        LEFT JOIN student_info ON course.std_ID = student_info.std_ID
			        LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
			        LEFT JOIN grade_info ON exam_result.grade = grade_info.grade_Name
			        LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

        strWhere = " WHERE (student_info.student_status = 'Access' or student_info.student_status = 'Graduate') and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%') and
			        exam_result.exam_ID = '" & ddlExamination.SelectedValue & "' and subject_info.course_Program = '" & ddlProgram.SelectedValue & "' and subject_info.subject_Campus = '" & ddlCampus.SelectedValue & "' and class_info.course_Program = '" & ddlProgram.SelectedValue & "' and class_info.class_Campus = '" & ddlCampus.SelectedValue & "'
			        AND class_info.class_ID = '" & ddlClassExamination.SelectedValue & "'"

        strGroupby = " GROUP BY
			        course.std_ID,
			        student_info.student_Name,
			        student_info.student_Mykad,
			        class_info.class_Name"

        getStudentList = strSelect & strWhere & strGroupby & strOrderby

        Return getStudentList

    End Function

    Protected Sub ddlYearExamination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYearExamination.SelectedIndexChanged
        exam_list()
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ddlExamination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamination.SelectedIndexChanged
        class_list()
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ddlProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgram.SelectedIndexChanged
        exam_list()
        class_list()
    End Sub

    Protected Sub ddlCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampus.SelectedIndexChanged
        program_list()
        exam_list()
        class_list()
    End Sub

    Protected Sub ddlClassExamination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassExamination.SelectedIndexChanged
        strRet = BindData(datRespondent)
        'strRet = BindStudentList(GridViewStudentList)
    End Sub
End Class