Imports System.Data.SqlClient

Public Class pengarah_laporan_kehadiran_table

    Inherits System.Web.UI.UserControl

    Dim i As Integer
    Dim strSQL As String = ""
    Dim strSQL_ExamDate As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")

    Dim MyConnection As SqlConnection = New SqlConnection(strConn)

    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim sqlQueryGraph As String = ""

    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim arraysubjectName(ddlSelect_Class.Items.Count.ToString) As String

        Try
            If Not IsPostBack Then

                ddlYear_list()
                ddlMonth_list()
                ddlSem_list()
                ddlClass_list()
                ddlSubject_list()
                subject_list()

                sublist()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub sublist()

        ddlSubject_list()

        If ddlSelect_Class.SelectedIndex = 0 Then

            graph_january()
            graph_february()
            graph_march()
            graph_april()
            graph_april()
            graph_may()
            graph_june()
            graph_july()
            graph_august()
            graph_september()
            graph_october()
            graph_november()
            graph_december()

            For i = 1 To ddlSelect_Class.Items.Count.ToString Step i + 1
                If i = 1 Then
                    graph_class1(i)
                End If
                If i = 2 Then
                    graph_class2(i)
                End If
                If i = 3 Then
                    graph_class3(i)
                End If
                If i = 4 Then
                    graph_class4(i)
                End If
                If i = 5 Then
                    graph_class5(i)
                End If
                If i = 6 Then
                    graph_class6(i)
                End If
                If i = 7 Then
                    graph_class7(i)
                End If
                If i = 8 Then
                    graph_class8(i)
                End If
                If i = 9 Then
                    graph_class9(i)
                End If
                If i = 10 Then
                    graph_class10(i)
                End If
                If i = 11 Then
                    graph_class11(i)
                End If
                If i = 12 Then
                    graph_class12(i)
                End If
                If i = 13 Then
                    graph_class13(i)
                End If
                If i = 14 Then
                    graph_class14(i)
                End If
                If i = 15 Then
                    graph_class15(i)
                End If
                If i = 16 Then
                    graph_class16(i)
                End If
                If i = 17 Then
                    graph_class17(i)
                End If
                If i = 18 Then
                    graph_class18(i)
                End If
                If i = 19 Then
                    graph_class19(i)
                End If
                If i = 20 Then
                    graph_class20(i)
                End If
                If i = 21 Then
                    graph_class21(i)
                End If
                If i = 22 Then
                    graph_class22(i)
                End If
                If i = 23 Then
                    graph_class23(i)
                End If
                If i = 24 Then
                    graph_class24(i)
                End If
                If i = 25 Then
                    graph_class25(i)
                End If
                If i = 26 Then
                    graph_class26(i)
                End If
                If i = 27 Then
                    graph_class27(i)
                End If
                If i = 28 Then
                    graph_class28(i)
                End If
                If i = 29 Then
                    graph_class29(i)
                End If
                If i = 30 Then
                    graph_class30(i)
                End If
                If i = 31 Then
                    graph_class31(i)
                End If
                If i = 32 Then
                    graph_class32(i)
                End If
                If i = 33 Then
                    graph_class33(i)
                End If
                If i = 34 Then
                    graph_class34(i)
                End If
                If i = 35 Then
                    graph_class35(i)
                End If
                If i = 36 Then
                    graph_class36(i)
                End If
                If i = 37 Then
                    graph_class37(i)
                End If
                If i = 38 Then
                    graph_class38(i)
                End If
                If i = 39 Then
                    graph_class39(i)
                End If
                If i = 40 Then
                    graph_class40(i)
                End If
                If i = 41 Then
                    graph_class41(i)
                End If
                If i = 42 Then
                    graph_class42(i)
                End If
                If i = 43 Then
                    graph_class43(i)
                End If
                If i = 44 Then
                    graph_class44(i)
                End If
                If i = 45 Then
                    graph_class45(i)
                End If
                If i = 46 Then
                    graph_class46(i)
                End If
                If i = 47 Then
                    graph_class47(i)
                End If
                If i = 48 Then
                    graph_class48(i)
                End If
                If i = 49 Then
                    graph_class49(i)
                End If
                If i = 50 Then
                    graph_class50(i)
                End If
            Next
        Else

            graphClass_january()
            graphClass_february()
            graphClass_march()
            graphClass_april()
            graphClass_april()
            graphClass_may()
            graphClass_june()
            graphClass_july()
            graphClass_august()
            graphClass_september()
            graphClass_october()
            graphClass_november()
            graphClass_december()

            For i = 1 To ddlSubjectID.Items.Count.ToString Step i + 1
                If i = 1 Then
                    graph_subject1(i)
                End If
                If i = 2 Then
                    graph_subject2(i)
                End If
                If i = 3 Then
                    graph_subject3(i)
                End If
                If i = 4 Then
                    graph_subject4(i)
                End If
                If i = 5 Then
                    graph_subject5(i)
                End If
                If i = 6 Then
                    graph_subject6(i)
                End If
                If i = 7 Then
                    graph_subject7(i)
                End If
                If i = 8 Then
                    graph_subject8(i)
                End If
                If i = 9 Then
                    graph_subject9(i)
                End If
                If i = 10 Then
                    graph_subject10(i)
                End If
                If i = 11 Then
                    graph_subject11(i)
                End If
                If i = 12 Then
                    graph_subject12(i)
                End If
                If i = 13 Then
                    graph_subject13(i)
                End If
                If i = 14 Then
                    graph_subject14(i)
                End If
                If i = 15 Then
                    graph_subject15(i)
                End If
                If i = 16 Then
                    graph_subject16(i)
                End If
                If i = 17 Then
                    graph_subject17(i)
                End If
                If i = 18 Then
                    graph_subject18(i)
                End If
                If i = 19 Then
                    graph_subject19(i)
                End If
                If i = 20 Then
                    graph_subject20(i)
                End If
                If i = 21 Then
                    graph_subject21(i)
                End If
                If i = 22 Then
                    graph_subject22(i)
                End If
                If i = 23 Then
                    graph_subject23(i)
                End If
                If i = 24 Then
                    graph_subject24(i)
                End If
                If i = 25 Then
                    graph_subject25(i)
                End If
                If i = 26 Then
                    graph_subject26(i)
                End If
                If i = 27 Then
                    graph_subject27(i)
                End If
                If i = 28 Then
                    graph_subject28(i)
                End If
                If i = 29 Then
                    graph_subject29(i)
                End If
                If i = 30 Then
                    graph_subject30(i)
                End If
                If i = 31 Then
                    graph_subject31(i)
                End If
                If i = 32 Then
                    graph_subject32(i)
                End If
                If i = 33 Then
                    graph_subject33(i)
                End If
                If i = 34 Then
                    graph_subject34(i)
                End If
                If i = 35 Then
                    graph_subject35(i)
                End If
                If i = 36 Then
                    graph_subject36(i)
                End If
                If i = 37 Then
                    graph_subject37(i)
                End If
                If i = 38 Then
                    graph_subject38(i)
                End If
                If i = 39 Then
                    graph_subject39(i)
                End If
                If i = 40 Then
                    graph_subject40(i)
                End If
                If i = 41 Then
                    graph_subject41(i)
                End If
                If i = 42 Then
                    graph_subject42(i)
                End If
                If i = 43 Then
                    graph_subject43(i)
                End If
                If i = 44 Then
                    graph_subject44(i)
                End If
                If i = 45 Then
                    graph_subject45(i)
                End If
                If i = 46 Then
                    graph_subject46(i)
                End If
                If i = 47 Then
                    graph_subject47(i)
                End If
                If i = 48 Then
                    graph_subject48(i)
                End If
                If i = 49 Then
                    graph_subject49(i)
                End If
                If i = 50 Then
                    graph_subject50(i)
                End If
            Next
        End If

    End Sub

    Private Sub ddlYear_list()
        strSQL = "select Parameter from setting WHERE Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlSelect_Year.DataSource = dsReport
            ddlSelect_Year.DataTextField = "Parameter"
            ddlSelect_Year.DataValueField = "Parameter"
            ddlSelect_Year.DataBind()
            ddlSelect_Year.SelectedValue = Now.Year

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlMonth_list()
        strSQL = "select Parameter, Value from setting WHERE Type = 'month'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlSelect_Month.DataSource = dsReport
            ddlSelect_Month.DataTextField = "Parameter"
            ddlSelect_Month.DataValueField = "Value"
            ddlSelect_Month.DataBind()
            ddlSelect_Month.SelectedValue = Now.Month

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlSem_list()

        strSQL = "SELECT Parameter from setting WHERE Type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlSelect_Sem.DataSource = dsReport
            ddlSelect_Sem.DataTextField = "Parameter"
            ddlSelect_Sem.DataValueField = "Parameter"
            ddlSelect_Sem.DataBind()
            ddlSelect_Sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlSelect_Sem.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlClass_list()
        strSQL = "select class_Name from class_info WHERE class_type = 'Compulsory' AND class_year = '" & ddlSelect_Year.SelectedValue & "' ORDER BY class_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlSelect_Class.DataSource = dsReport
            ddlSelect_Class.DataTextField = "class_Name"
            ddlSelect_Class.DataValueField = "class_Name"
            ddlSelect_Class.DataBind()
            ddlSelect_Class.Items.Insert(0, New ListItem("All Class", String.Empty))
            ddlSelect_Class.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlSubject_list()
        Try
            Dim subjectLevel As String = ""
            subjectLevel = "select class_Level from class_info where class_Name ='" & ddlSelect_Class.SelectedValue & "' AND class_year = '" & ddlSelect_Year.SelectedValue & "'"
            Dim dataSubjectLevel As String = getFieldValue(subjectLevel, strConn)

            strSQL = "select subject_info.subject_Name from subject_info"
            strSQL += " where subject_info.subject_year ='" & ddlSelect_Year.SelectedValue & "'"
            strSQL += " and  subject_info.subject_StudentYear ='" & dataSubjectLevel & "'"
            strSQL += " and  subject_info.subject_sem ='" & ddlSelect_Sem.SelectedValue & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubjectID.DataSource = ds
            ddlSubjectID.DataTextField = "subject_Name"
            ddlSubjectID.DataValueField = "subject_Name"
            ddlSubjectID.DataBind()
            ddlSubjectID.Items.Insert(0, New ListItem(ddlSubjectID.Items.Count.ToString & " Courses", String.Empty))

        Catch ex As Exception

        End Try
    End Sub

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Sub subject_list()

        Try
            Dim subjectLevel As String = ""
            subjectLevel = "select class_Level from class_info where class_Name ='" & ddlSelect_Class.SelectedValue & "' AND class_year = '" & ddlSelect_Year.SelectedValue & "'"
            Dim dataSubjectLevel As String = getFieldValue(subjectLevel, strConn)

            strSQL = "select subject_info.subject_Name from subject_info"
            strSQL += " where subject_info.subject_year ='" & ddlSelect_Year.SelectedValue & "'"
            strSQL += " and  subject_info.subject_StudentYear ='" & dataSubjectLevel & "'"
            strSQL += " and  subject_info.subject_sem ='" & ddlSelect_Sem.SelectedValue & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSelect_Subject.DataSource = ds
            ddlSelect_Subject.DataTextField = "subject_Name"
            ddlSelect_Subject.DataValueField = "subject_Name"
            ddlSelect_Subject.DataBind()
            ddlSelect_Subject.Items.Insert(0, New ListItem("Select Courses", String.Empty))

        Catch ex As Exception

        End Try

    End Sub

    Private Function getSQL() As String

        Dim strSelect As String = ""
        Dim strWhere As String = ""
        Dim strGroupby As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        strSelect = "SELECT 
                    attendance.course_ID,
                    student_info.student_ID,      
                    student_info.student_Name,     
                    SUM(attendance_Status) AS 'JumlahHadir',
                    COUNT(attendance_Status)-SUM(attendance_Status) AS 'JumlahTidakHadir',
                    CONCAT(SUM(attendance_Status)*100/COUNT(attendance_Status),'%') AS 'Peratus'	
                    FROM attendance
                    LEFT JOIN course ON attendance.course_ID = course.course_ID
                    LEFT JOIN student_info ON course.std_ID = student_info.std_ID
                    LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
                    LEFT JOIN student_level ON course.std_ID = student_level.std_ID
                    LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

        strWhere = " WHERE
                    attendance.date_year = '" & ddlSelect_Year.SelectedValue & "'
                    AND attendance.date_month = '" & ddlSelect_Month.SelectedValue & "'                     
                    AND subject_info.subject_sem = '" & ddlSelect_Sem.SelectedValue & "'
                    AND class_info.class_Name = '" & ddlSelect_Class.SelectedValue & "'"

        If ddlSelect_Subject.SelectedValue IsNot "Select Courses" Then
            strWhere += " AND subject_info.subject_Name = '" & ddlSelect_Subject.SelectedValue & "'"
        End If

        strGroupby = " GROUP BY 
                    attendance.course_ID,
                    student_info.student_ID,      
                    student_info.student_Name"

        getSQL = strSelect & strWhere & strGroupby & strOrderby

        Return getSQL

    End Function

    Protected Sub ddlSelect_Year_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelect_Year.SelectedIndexChanged
        Try
            ddlMonth_list()
            ddlSem_list()
            ddlClass_list()
            ddlSubject_list()
            subject_list()
            sublist()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSelect_Month_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelect_Month.SelectedIndexChanged
        Try
            ddlSem_list()
            ddlClass_list()
            ddlSubject_list()
            subject_list()
            sublist()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSelect_Sem_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelect_Sem.SelectedIndexChanged
        Try
            ddlClass_list()
            ddlSubject_list()
            subject_list()
            sublist()
        Catch ex As Exception

        End Try
    End Sub

    'SELECT CLASS
    Protected Sub ddlSelect_Class_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelect_Class.SelectedIndexChanged
        Try
            ddlSubject_list()
            subject_list()
            sublist()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSelect_Subject_SelectedIndexChanged(sender As Object, e As EventArgs)
        strRet = BindData(datRespondent)
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


























    Private Sub graph_january()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '1'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceJan.Value = sql_month

    End Sub
    Private Sub graph_february()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '2'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceFeb.Value = sql_month

    End Sub
    Private Sub graph_march()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '3'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceMac.Value = sql_month

    End Sub
    Private Sub graph_april()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '4'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceApr.Value = sql_month

    End Sub
    Private Sub graph_may()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '5'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceMay.Value = sql_month

    End Sub

    Private Sub graph_june()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '6'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceJun.Value = sql_month

    End Sub
    Private Sub graph_july()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '7'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceJul.Value = sql_month

    End Sub
    Private Sub graph_august()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '8'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceAug.Value = sql_month

    End Sub
    Private Sub graph_september()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '9'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceSep.Value = sql_month

    End Sub
    Private Sub graph_october()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '10'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceOct.Value = sql_month

    End Sub
    Private Sub graph_november()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '11'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceNov.Value = sql_month

    End Sub
    Private Sub graph_december()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '12'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceDec.Value = sql_month

    End Sub

    ''IF CLASS SELECTED
    Private Sub graphClass_january()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '1' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceJan.Value = sql_month

    End Sub
    Private Sub graphClass_february()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '2' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceFeb.Value = sql_month

    End Sub
    Private Sub graphClass_march()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '3' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceMac.Value = sql_month

    End Sub
    Private Sub graphClass_april()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '4' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceApr.Value = sql_month

    End Sub
    Private Sub graphClass_may()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '5' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceMay.Value = sql_month

    End Sub

    Private Sub graphClass_june()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '6' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceJun.Value = sql_month

    End Sub
    Private Sub graphClass_july()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '7' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceJul.Value = sql_month

    End Sub
    Private Sub graphClass_august()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '8' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceAug.Value = sql_month

    End Sub
    Private Sub graphClass_september()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '9' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceSep.Value = sql_month

    End Sub
    Private Sub graphClass_october()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '10' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceOct.Value = sql_month

    End Sub
    Private Sub graphClass_november()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '11' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceNov.Value = sql_month

    End Sub
    Private Sub graphClass_december()

        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '12' AND class_Name = '" & ddlSelect_Class.SelectedValue & "'"
        Dim sql_month As String = getFieldValue(sqlQueryGraph, strConn)
        attendanceDec.Value = sql_month

    End Sub





    Private Sub graph_subject1(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance1.Value = sql_subject
        subjectName1.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject2(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance2.Value = sql_subject
        subjectName2.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject3(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance3.Value = sql_subject
        subjectName3.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject4(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance4.Value = sql_subject
        subjectName4.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject5(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance5.Value = sql_subject
        subjectName5.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject6(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance6.Value = sql_subject
        subjectName6.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject7(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance7.Value = sql_subject
        subjectName7.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject8(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance8.Value = sql_subject
        subjectName8.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject9(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance9.Value = sql_subject
        subjectName9.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject10(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance10.Value = sql_subject
        subjectName10.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject11(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance11.Value = sql_subject
        subjectName11.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject12(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance12.Value = sql_subject
        subjectName12.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject13(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance13.Value = sql_subject
        subjectName13.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject14(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance14.Value = sql_subject
        subjectName14.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject15(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance15.Value = sql_subject
        subjectName15.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject16(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance16.Value = sql_subject
        subjectName16.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject17(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance17.Value = sql_subject
        subjectName17.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject18(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance18.Value = sql_subject
        subjectName18.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject19(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance19.Value = sql_subject
        subjectName19.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject20(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance20.Value = sql_subject
        subjectName20.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject21(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance21.Value = sql_subject
        subjectName21.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject22(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance22.Value = sql_subject
        subjectName22.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject23(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance23.Value = sql_subject
        subjectName23.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject24(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance24.Value = sql_subject
        subjectName24.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject25(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance25.Value = sql_subject
        subjectName25.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject26(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance26.Value = sql_subject
        subjectName26.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject27(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance27.Value = sql_subject
        subjectName27.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject28(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance28.Value = sql_subject
        subjectName28.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject29(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance29.Value = sql_subject
        subjectName29.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject30(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance30.Value = sql_subject
        subjectName30.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject31(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance31.Value = sql_subject
        subjectName31.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject32(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance32.Value = sql_subject
        subjectName32.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject33(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance33.Value = sql_subject
        subjectName33.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject34(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance34.Value = sql_subject
        subjectName34.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject35(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance35.Value = sql_subject
        subjectName35.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject36(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance36.Value = sql_subject
        subjectName36.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject37(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance37.Value = sql_subject
        subjectName37.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject38(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance38.Value = sql_subject
        subjectName38.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject39(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance39.Value = sql_subject
        subjectName39.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject40(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance40.Value = sql_subject
        subjectName40.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject41(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance41.Value = sql_subject
        subjectName41.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject42(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance42.Value = sql_subject
        subjectName42.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject43(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance43.Value = sql_subject
        subjectName43.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject44(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance44.Value = sql_subject
        subjectName44.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject45(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance45.Value = sql_subject
        subjectName45.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject46(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance46.Value = sql_subject
        subjectName46.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject47(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance47.Value = sql_subject
        subjectName47.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject48(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance48.Value = sql_subject
        subjectName48.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject49(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance49.Value = sql_subject
        subjectName49.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub
    Private Sub graph_subject50(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.SelectedValue & "' AND subject_info.subject_Name = '" & ddlSubjectID.Items(i).Value.ToString & "'"
        Dim sql_subject As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance50.Value = sql_subject
        subjectName50.Value = ddlSubjectID.Items(i).Value.ToString
    End Sub



    Private Sub graph_class1(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance1.Value = sql_class
        subjectName1.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class2(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance2.Value = sql_class
        subjectName2.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class3(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance3.Value = sql_class
        subjectName3.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class4(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance4.Value = sql_class
        subjectName4.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class5(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance5.Value = sql_class
        subjectName5.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class6(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance6.Value = sql_class
        subjectName6.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class7(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance7.Value = sql_class
        subjectName7.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class8(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance8.Value = sql_class
        subjectName8.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class9(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance9.Value = sql_class
        subjectName9.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class10(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance10.Value = sql_class
        subjectName10.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class11(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance11.Value = sql_class
        subjectName11.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class12(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance12.Value = sql_class
        subjectName12.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class13(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance13.Value = sql_class
        subjectName13.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class14(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance14.Value = sql_class
        subjectName14.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class15(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance15.Value = sql_class
        subjectName15.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class16(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance16.Value = sql_class
        subjectName16.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class17(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance17.Value = sql_class
        subjectName17.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class18(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance18.Value = sql_class
        subjectName18.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class19(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance19.Value = sql_class
        subjectName19.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class20(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance20.Value = sql_class
        subjectName20.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class21(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance21.Value = sql_class
        subjectName21.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class22(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance22.Value = sql_class
        subjectName22.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class23(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance23.Value = sql_class
        subjectName23.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class24(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance24.Value = sql_class
        subjectName24.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class25(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance25.Value = sql_class
        subjectName25.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class26(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance26.Value = sql_class
        subjectName26.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class27(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance27.Value = sql_class
        subjectName27.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class28(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance28.Value = sql_class
        subjectName28.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class29(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance29.Value = sql_class
        subjectName29.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class30(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance30.Value = sql_class
        subjectName30.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class31(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance31.Value = sql_class
        subjectName31.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class32(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance32.Value = sql_class
        subjectName32.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class33(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance33.Value = sql_class
        subjectName33.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class34(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance34.Value = sql_class
        subjectName34.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class35(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance35.Value = sql_class
        subjectName35.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class36(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance36.Value = sql_class
        subjectName36.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class37(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance37.Value = sql_class
        subjectName37.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class38(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance38.Value = sql_class
        subjectName38.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class39(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance39.Value = sql_class
        subjectName39.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class40(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance40.Value = sql_class
        subjectName40.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class41(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance41.Value = sql_class
        subjectName41.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class42(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance42.Value = sql_class
        subjectName42.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class43(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance43.Value = sql_class
        subjectName43.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class44(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance44.Value = sql_class
        subjectName44.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class45(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance45.Value = sql_class
        subjectName45.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class46(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance46.Value = sql_class
        subjectName46.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class47(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance47.Value = sql_class
        subjectName47.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class48(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance48.Value = sql_class
        subjectName48.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class49(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance49.Value = sql_class
        subjectName49.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub
    Private Sub graph_class50(i)
        sqlQueryGraph = "SELECT CAST(ROUND((SUM(attendance_Status)*100.00/COUNT(attendance_Status)),2) AS decimal(5,2)) AS 'Percentage' FROM attendance LEFT JOIN course ON attendance.course_ID = course.course_ID LEFT JOIN class_info ON course.class_ID = class_info.class_ID WHERE date_year = '" & ddlSelect_Year.SelectedValue & "' AND date_month = '" & ddlSelect_Month.SelectedValue & "' AND class_Name = '" & ddlSelect_Class.Items(i).Value.ToString & "'"
        Dim sql_class As String = getFieldValue(sqlQueryGraph, strConn)
        subjectAttendance50.Value = sql_class
        subjectName50.Value = ddlSelect_Class.Items(i).Value.ToString
    End Sub




End Class