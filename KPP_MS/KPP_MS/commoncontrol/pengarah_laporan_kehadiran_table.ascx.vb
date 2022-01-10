Imports System.Data.SqlClient

Public Class pengarah_laporan_kehadiran_table

    Inherits System.Web.UI.UserControl

    Dim i As Integer
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objconn As SqlConnection = New SqlConnection(strConn)

    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                ddlAttendanceYear_List()
                ddlAttendanceMonth_list()
                ddlAttendanceCampus_list()
                ddlAttendanceProgram_list()
                ddlAttendanceLevel_List()
                ddlAttendanceSubject_List()
                ddlAttendanceClass_List()

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlAttendanceYear_List()
        strSQL = "select Parameter from setting WHERE Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlAttendanceYear.DataSource = dsReport
            ddlAttendanceYear.DataTextField = "Parameter"
            ddlAttendanceYear.DataValueField = "Parameter"
            ddlAttendanceYear.DataBind()
            ddlAttendanceYear.SelectedIndex = 0
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlAttendanceMonth_list()
        strSQL = "select Parameter, Value from setting WHERE Type = 'month'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlAttendanceMonth.DataSource = dsReport
            ddlAttendanceMonth.DataTextField = "Parameter"
            ddlAttendanceMonth.DataValueField = "Value"
            ddlAttendanceMonth.DataBind()
            ddlAttendanceMonth.Items.Insert(0, New ListItem("Select Month", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlAttendanceCampus_list()
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
            ddlAttendanceCampus.DataSource = ds
            ddlAttendanceCampus.DataTextField = "Parameter"
            ddlAttendanceCampus.DataValueField = "Value"
            ddlAttendanceCampus.DataBind()
            ddlAttendanceCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlAttendanceProgram_list()
        If ddlAttendanceCampus.SelectedValue = "APP" Then
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

            ddlAttendanceProgram.DataSource = ds
            ddlAttendanceProgram.DataTextField = "Parameter"
            ddlAttendanceProgram.DataValueField = "Value"
            ddlAttendanceProgram.DataBind()
            ddlAttendanceProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlAttendanceLevel_List()
        strSQL = "select Parameter from setting WHERE Type = 'level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlAttendanceLevel.DataSource = dsReport
            ddlAttendanceLevel.DataTextField = "Parameter"
            ddlAttendanceLevel.DataValueField = "Parameter"
            ddlAttendanceLevel.DataBind()
            ddlAttendanceLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlAttendanceSubject_List()

        Dim semesterList As String = ""

        If ddlAttendanceMonth.SelectedValue < 7 Then
            semesterList = "Sem 1"
        ElseIf ddlAttendanceMonth.SelectedValue > 6 Then
            semesterList = "Sem 2"
        End If

        strSQL = " select subject_ID, subject_Name from subject_info where subject_year = '" & ddlAttendanceYear.SelectedValue & "' and subject_sem = '" & semesterList & "' and subject_StudentYear = '" & ddlAttendanceLevel.SelectedValue & "' and course_Program = '" & ddlAttendanceProgram.SelectedValue & "' and subject_Campus = '" & ddlAttendanceCampus.SelectedValue & "' order by subject_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlAttendaceCourse.DataSource = dsReport
            ddlAttendaceCourse.DataTextField = "subject_Name"
            ddlAttendaceCourse.DataValueField = "subject_ID"
            ddlAttendaceCourse.DataBind()
            ddlAttendaceCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlAttendanceClass_List()

        strSQL = "Select subject_type from subject_info where subject_ID = '" & ddlAttendaceCourse.SelectedValue & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet = "Compulsory" Then
            strSQL = "Select class_ID, class_name from class_info where class_year = '" & ddlAttendanceYear.SelectedValue & "' and class_Level = '" & ddlAttendanceLevel.SelectedValue & "' and class_type = 'Compulsory' and course_Program = '" & ddlAttendanceProgram.SelectedValue & "' and class_Campus = '" & ddlAttendanceCampus.SelectedValue & "' order by class_Name asc"
        Else
            strSQL = "Select class_ID, class_name from class_info where class_year = '" & ddlAttendanceYear.SelectedValue & "' and class_Level = '" & ddlAttendanceLevel.SelectedValue & "' and class_type <> 'Compulsory' and subject_ID = '" & ddlAttendaceCourse.SelectedValue & "'and course_Program = '" & ddlAttendanceProgram.SelectedValue & "' and class_Campus = '" & ddlAttendanceCampus.SelectedValue & "' order by class_Name asc"
        End If


        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim dsReport As DataSet = New DataSet
            sqlDA.Fill(dsReport, "AnyTable")

            ddlAttendanceClass.DataSource = dsReport
            ddlAttendanceClass.DataTextField = "class_Name"
            ddlAttendanceClass.DataValueField = "class_ID"
            ddlAttendanceClass.DataBind()
            ddlAttendanceClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
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

            objconn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQL() As String

        Dim strSelect As String = ""
        Dim strWhere As String = ""
        Dim strGroupby As String = ""
        Dim strOrderby As String = "    ORDER BY student_info.student_Name ASC"

        strSelect = "   SELECT 
                        attendance.course_ID,
                        student_info.student_ID,      
                        student_info.student_Name,
                        class_info.class_Name,
                        subject_info.subject_Name,  
                        SUM(attendance_Status) AS 'TotalPresent',
                        COUNT(attendance_Status)-SUM(attendance_Status) AS 'TotalAbsent',
                        CONCAT(SUM(attendance_Status)*100/COUNT(attendance_Status),'%') AS 'Percentage'
                        FROM attendance
                        LEFT JOIN course ON attendance.course_ID = course.course_ID
                        LEFT JOIN student_info ON course.std_ID = student_info.std_ID
                        LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
                        LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

        strWhere = "    WHERE
                        attendance.date_year = '" & ddlAttendanceYear.SelectedValue & "' and subject_info.course_Program = '" & ddlAttendanceProgram.SelectedValue & "' and subject_info.subject_Campus = '" & ddlAttendanceCampus.SelectedValue & "'
                        AND attendance.date_month = '" & ddlAttendanceMonth.SelectedValue & "'
                        AND subject_info.subject_StudentYear = '" & ddlAttendanceLevel.SelectedValue & "'
                        AND subject_info.subject_ID = '" & ddlAttendaceCourse.SelectedValue & "'"

        If ddlAttendanceClass.SelectedIndex > 0 Then
            strWhere += "   AND class_info.class_ID = '" & ddlAttendanceClass.SelectedValue & "'"
        End If


        strGroupby = "  GROUP BY 
                        attendance.course_ID,
                        student_info.student_ID,      
                        student_info.student_Name,
                        class_info.class_Name,
                        subject_info.subject_Name"

        getSQL = strSelect & strWhere & strGroupby & strOrderby

        Return getSQL

    End Function

    Protected Sub ddlAttendanceYear_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAttendanceYear.SelectedIndexChanged
        Try
            ddlAttendanceMonth_list()
            ddlAttendanceLevel_List()
            ddlAttendanceSubject_List()
            ddlAttendanceClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAttendanceMonth_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAttendanceMonth.SelectedIndexChanged
        Try
            ddlAttendanceLevel_List()
            ddlAttendanceSubject_List()
            ddlAttendanceClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAttendanceCampus_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAttendanceCampus.SelectedIndexChanged
        Try
            ddlAttendanceSubject_List()
            ddlAttendanceClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAttendanceProgram_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAttendanceProgram.SelectedIndexChanged
        Try
            ddlAttendanceSubject_List()
            ddlAttendanceClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAttendanceLevel_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAttendanceLevel.SelectedIndexChanged
        Try
            ddlAttendanceSubject_List()
            ddlAttendanceClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAttendaceCourse_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAttendaceCourse.SelectedIndexChanged
        Try
            ddlAttendanceClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAttendanceClass_OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAttendanceClass.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

End Class