Imports System.Data.SqlClient

Public Class lecturer_attendanceData
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strRet2 As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ddlYearAttendance_List()
                ddlMonthAttendance_List()
                ddlDayAttendance_List()
                ddlProgramAttendance_List()
                ddlLevelAttendance_List()
                ddlSemesterAttendance_List()
                ddlClassAttendance_List()
                ddlCourseAttendace_List()
                ddlStatus_list()

                strRet2 = BindData2(addRespondent)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlYearAttendance_List()

        strSQL = "Select distinct lecturer_year from lecturer where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' order by lecturer_year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYearAttendance.DataSource = ds
            ddlYearAttendance.DataTextField = "lecturer_year"
            ddlYearAttendance.DataValueField = "lecturer_year"
            ddlYearAttendance.DataBind()
            ddlYearAttendance.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYearAttendance.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlMonthAttendance_List()

        strSQL = "select Parameter, Value from setting where type = 'month'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMonthAttendance.DataSource = ds
            ddlMonthAttendance.DataTextField = "Parameter"
            ddlMonthAttendance.DataValueField = "Value"
            ddlMonthAttendance.DataBind()
            ddlMonthAttendance.Items.Insert(0, New ListItem("Select Month", String.Empty))
            ddlMonthAttendance.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlDayAttendance_List()

        strSQL = "select Parameter, Value from setting where type = 'day'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDayAttendance.DataSource = ds
            ddlDayAttendance.DataTextField = "Parameter"
            ddlDayAttendance.DataValueField = "Value"
            ddlDayAttendance.DataBind()
            ddlDayAttendance.Items.Insert(0, New ListItem("Select Day", String.Empty))
            ddlDayAttendance.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlProgramAttendance_List()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "select Parameter, Value from setting where type = 'Stream' and Value = 'PS'"
        Else
            strSQL = "select Parameter, Value from setting where type = 'Stream'"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlProgramAttendance.DataSource = ds
            ddlProgramAttendance.DataTextField = "Parameter"
            ddlProgramAttendance.DataValueField = "Value"
            ddlProgramAttendance.DataBind()
            ddlProgramAttendance.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddlProgramAttendance.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlLevelAttendance_List()

        strSQL = "select Parameter, Value from setting where type = 'level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevelAttendance.DataSource = ds
            ddlLevelAttendance.DataTextField = "Parameter"
            ddlLevelAttendance.DataValueField = "Value"
            ddlLevelAttendance.DataBind()
            ddlLevelAttendance.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlLevelAttendance.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlSemesterAttendance_List()

        strSQL = "select Parameter, Value from setting where type = 'sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemesterAttendance.DataSource = ds
            ddlSemesterAttendance.DataTextField = "Parameter"
            ddlSemesterAttendance.DataValueField = "Value"
            ddlSemesterAttendance.DataBind()
            ddlSemesterAttendance.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlSemesterAttendance.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlClassAttendance_List()

        Dim checkCourseType As String = "Select subject_type from subject_info where subject_ID = '" & ddlCourseAttendace.SelectedValue & "'"
        Dim getData As String = oCommon.getFieldValue(checkCourseType)

        If getData = "Compulsory" Then
            strSQL = "  select distinct class_info.class_ID, class_info.class_Name from lecturer
                        left join class_info on lecturer.class_ID = class_info.class_ID
                        where class_info.class_year = '" & ddlYearAttendance.SelectedValue & "'
                        and class_info.class_Level = '" & ddlLevelAttendance.SelectedValue & "'
                        and class_info.class_type = 'Compulsory' and class_info.class_Campus = '" & Session("SchoolCampus") & "'
                        and class_info.course_Program = '" & ddlProgramAttendance.SelectedValue & "'
                        and lecturer.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'
                        order by class_info.class_Name"
        Else
            strSQL = "  select distinct class_info.class_ID, class_info.class_Name from lecturer
                        left join class_info on lecturer.class_ID = class_info.class_ID
                        where class_info.class_year = '" & ddlYearAttendance.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'
                        and class_info.class_Level = '" & ddlLevelAttendance.SelectedValue & "'
                        and class_info.subject_ID = '" & ddlCourseAttendace.SelectedValue & "'
                        and class_info.course_Program = '" & ddlProgramAttendance.SelectedValue & "'
                        and lecturer.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'
                        order by class_info.class_Name"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassAttendance.DataSource = ds
            ddlClassAttendance.DataTextField = "class_Name"
            ddlClassAttendance.DataValueField = "class_ID"
            ddlClassAttendance.DataBind()
            ddlClassAttendance.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlClassAttendance.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlCourseAttendace_List()

        strSQL = "  select distinct subject_info.subject_ID, subject_info.subject_Name from lecturer
                    left join subject_info on lecturer.subject_ID = subject_info.subject_ID
                    where lecturer.lecturer_year = '" & ddlYearAttendance.SelectedValue & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "'
                    and subject_info.subject_sem = '" & ddlSemesterAttendance.SelectedValue & "'
                    and lecturer.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'
                    and subject_info.course_Program = '" & ddlProgramAttendance.SelectedValue & "'
                    order by subject_info.subject_Name"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseAttendace.DataSource = ds
            ddlCourseAttendace.DataTextField = "subject_Name"
            ddlCourseAttendace.DataValueField = "subject_ID"
            ddlCourseAttendace.DataBind()
            ddlCourseAttendace.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourseAttendace.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlStatus_list()
        Try
            Dim strLevelSql As String = "SELECT Parameter FROM setting WHERE Type = '999999'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            'attendance_Status.DataSource = levds
            'attendance_Status.DataValueField = "Parameter"
            'attendance_Status.DataTextField = "Parameter"
            'attendance_Status.DataBind()
            'attendance_Status.Items.Insert(0, New ListItem("Select Status", String.Empty))
            'attendance_Status.Items.Insert(1, New ListItem("Absent", 0))
            'attendance_Status.Items.Insert(2, New ListItem("Attend", 1))
            'ddlSattendance_Statustatus.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData2(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL2, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            Dim row As Integer = 0
            Dim col As Integer = 4
            Dim lblDay As Label

            Dim CountPost As Integer = 0
            Dim CountNega As Integer = 0

            CountPost = 0
            CountNega = 0

            For row = 0 To addRespondent.Rows.Count - 1 Step row + 1
                lblDay = addRespondent.Rows(row).Cells(4).FindControl("lblday")
                If lblDay.Text <> "0" And lblDay.Text <> "1" Then
                    CountNega += 1
                Else
                    CountPost += 1
                End If
            Next

            If CountPost > 0 Then
                gvTable.Columns(4).Visible = True
            Else
                gvTable.Columns(4).Visible = False
            End If

            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        run_color()

        Return True

    End Function

    Private Function getSQL2() As String

        Dim get_Staff As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        strSQL = "  Select course.course_ID from attendance
                    left join course on attendance.course_ID = course.course_ID
                    where date_year = '" & ddlYearAttendance.SelectedValue & "' and attendance.staff_ID = '" & get_Staff & "' and course.subject_ID = '" & ddlCourseAttendace.SelectedValue & "' and course.class_ID = '" & ddlClassAttendance.SelectedValue & "'"

        strSQL += " And attendance.date_month = '" & ddlMonthAttendance.SelectedValue & "'"

        strSQL += " And attendance.date_day = '" & ddlDayAttendance.SelectedValue & "'"

        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length > 0 Then

            tmpSQL = "  SELECT course.course_ID, student_info.student_Name, student_info.student_ID, subject_info.subject_Name, class_info.class_Name, attendance.attendance_Status as StatusColor, attendance.attendance_Status as Status, attendance.attendance_Remarks from student_info
                        LEFT JOIN course on student_info.std_ID = course.std_ID
                        LEFT JOIN subject_info on course.subject_ID = subject_info.subject_ID
                        LEFT JOIN class_info on course.class_ID = class_info.class_ID
				        LEFT JOIN lecturer ON class_info.class_ID = lecturer.class_ID
				        LEFT JOIN staff_Info ON lecturer.stf_ID = staff_Info.stf_ID
                        LEFT JOIN attendance on course.course_ID = attendance.course_ID"

            strWhere = "    WHERE staff_Info.stf_ID = '" & get_Staff & "' and staff_Info.staff_Status = 'Access' and lecturer.lecturer_year = '" & ddlYearAttendance.SelectedValue & "' 
                            And student_info.student_status = 'Access' and course.year = '" & ddlYearAttendance.SelectedValue & "'
                            And subject_info.subject_sem = '" & ddlSemesterAttendance.SelectedValue & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'
                            And course.class_ID = '" & ddlClassAttendance.SelectedValue & "' and lecturer.class_ID = '" & ddlClassAttendance.SelectedValue & "'
                            And course.subject_ID = '" & ddlCourseAttendace.SelectedValue & "' and lecturer.subject_ID = '" & ddlCourseAttendace.SelectedValue & "'
                            And class_info.course_Program = '" & ddlProgramAttendance.SelectedValue & "' And subject_info.course_Program = '" & ddlProgramAttendance.SelectedValue & "'
                            And student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%') and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate')"

            strWhere += " And attendance.date_month = '" & ddlMonthAttendance.SelectedValue & "'"

            strWhere += " And attendance.date_day = '" & ddlDayAttendance.SelectedValue & "'"

        Else

            tmpSQL = "  SELECT course.course_ID, student_info.student_Name, student_info.student_ID, subject_info.subject_Name, class_info.class_Name, '' as StatusColor, '1' as Status, '' as attendance_Remarks from student_info
                        LEFT JOIN course on student_info.std_ID = course.std_ID
                        LEFT JOIN subject_info on course.subject_ID = subject_info.subject_ID
                        LEFT JOIN class_info on course.class_ID = class_info.class_ID
				        LEFT JOIN lecturer ON class_info.class_ID = lecturer.class_ID
				        LEFT JOIN staff_Info ON lecturer.stf_ID = staff_Info.stf_ID"

            strWhere = "    WHERE staff_Info.stf_ID = '" & get_Staff & "' and staff_Info.staff_Status = 'Access' and lecturer.lecturer_year = '" & ddlYearAttendance.SelectedValue & "' 
                            And student_info.student_status = 'Access' and course.year = '" & ddlYearAttendance.SelectedValue & "'
                            And subject_info.subject_sem = '" & ddlSemesterAttendance.SelectedValue & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'
                            And course.class_ID = '" & ddlClassAttendance.SelectedValue & "' and lecturer.class_ID = '" & ddlClassAttendance.SelectedValue & "'
                            And course.subject_ID = '" & ddlCourseAttendace.SelectedValue & "' and lecturer.subject_ID = '" & ddlCourseAttendace.SelectedValue & "'
                            And class_info.course_Program = '" & ddlProgramAttendance.SelectedValue & "' And subject_info.course_Program = '" & ddlProgramAttendance.SelectedValue & "'
                            And student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%') and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate')"

        End If

        getSQL2 = tmpSQL & strWhere & strOrderby

        Return getSQL2

    End Function

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

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim execQuery As Integer = 0
        Dim errorCount As Integer = 0
        Dim i As Integer
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim get_Staff As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        If ddlSemesterAttendance.SelectedIndex = 0 Then
            ShowMessage(" Please Select Semester ", MessageType.Error)
            execQuery = 1
        End If
        If ddlClassAttendance.SelectedIndex = 0 Then
            ShowMessage(" Please Select Class ", MessageType.Error)
            execQuery = 1
        End If
        If ddlCourseAttendace.SelectedIndex = 0 Then
            ShowMessage(" Please Select Course ", MessageType.Error)
            execQuery = 1
        End If
        If ddlMonthAttendance.SelectedIndex = 0 Then
            ShowMessage(" Please Select Month ", MessageType.Error)
            execQuery = 1
        End If
        If ddlDayAttendance.SelectedIndex = 0 Then
            ShowMessage(" Please Select Day ", MessageType.Error)
            execQuery = 1
        End If

        If execQuery = 0 Then

            For i = 0 To addRespondent.Rows.Count - 1 Step i + 1

                Dim textRemarks As TextBox = addRespondent.Rows(i).FindControl("attendance_Remarks")
                Dim ddlAttendance As DropDownList = addRespondent.Rows(i).FindControl("attendance_Status")
                ' Get the values of textboxes using findControl
                Dim strKey As String = addRespondent.DataKeys(i).Value.ToString
                Dim checkAttendance As String = "SELECT course_ID FROM attendance WHERE course_ID = '" & strKey & "' AND date_day = '" & ddlDayAttendance.Text & "' AND date_month = '" & ddlMonthAttendance.Text & "' AND date_year = '" & ddlYearAttendance.Text & "'"
                Dim dataCheckAttendance As String = getFieldValue(checkAttendance, strConn)

                'if data exist
                If dataCheckAttendance.Length > 0 And dataCheckAttendance <> "" Then

                    'UPDATE
                    strSQL = "UPDATE attendance SET attendance_Status = '" & ddlAttendance.SelectedValue & "', attendance_Remarks = UPPER('" & textRemarks.Text & "'), staff_ID = '" & get_Staff & "' WHERE course_ID = '" & strKey & "' AND date_day = '" & ddlDayAttendance.Text & "' AND date_month = '" & ddlMonthAttendance.Text & "' AND date_year = '" & ddlYearAttendance.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    errorCount = 0
                Else

                    Try
                        ''insert to attendance database
                        Using STDDATA As New SqlCommand("INSERT INTO attendance(course_ID, attendance_Status, attendance_Remarks, date_day, date_month, date_year,staff_ID) VALUES('" & strKey & "','" & ddlAttendance.SelectedValue & "', UPPER('" & textRemarks.Text & "'),'" & ddlDayAttendance.Text & "','" & ddlMonthAttendance.Text & "','" & ddlYearAttendance.Text & "','" & get_Staff & "')", objConn)
                            objConn.Open()
                            Dim j = STDDATA.ExecuteNonQuery()
                            objConn.Close()
                            If j <> 0 Then
                                errorCount = 0
                            Else
                                errorCount = 1
                            End If
                        End Using

                    Catch ex As Exception
                    End Try

                End If
            Next
        End If

        If errorCount = 0 Then
            ShowMessage(" Update Student Attendance ", MessageType.Success)
        ElseIf errorCount = 1 Then
            ShowMessage(" Unsuccessfull Update Student Attendance ", MessageType.Error)
        End If

        strRet2 = BindData2(addRespondent)

    End Sub

    Private Sub ddlYearAttendance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYearAttendance.SelectedIndexChanged
        Try
            ddlLevelAttendance_List()

            ddlSemesterAttendance_List()

            ddlClassAttendance_List()

            ddlCourseAttendace_List()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlProgramAttendance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgramAttendance.SelectedIndexChanged
        Try

            ddlCourseAttendace_List()
            ddlClassAttendance_List()

            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlLevelAttendance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelAttendance.SelectedIndexChanged
        Try
            ddlCourseAttendace_List()
            ddlClassAttendance_List()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlSemesterAttendance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemesterAttendance.SelectedIndexChanged
        Try
            ddlCourseAttendace_List()
            ddlClassAttendance_List()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlClassAttendance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassAttendance.SelectedIndexChanged
        Try
            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCourseAttendace_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseAttendace.SelectedIndexChanged
        Try
            ddlClassAttendance_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlMonthAttendance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonthAttendance.SelectedIndexChanged
        Try
            ddlDayAttendance_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlDayAttendance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDayAttendance.SelectedIndexChanged
        Try
            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Private Sub run_color()
        Dim col As Integer = 4
        Dim row As Integer = 0
        Dim lblDay As Label

        For row = 0 To addRespondent.Rows.Count - 1 Step row + 1
            lblDay = addRespondent.Rows(row).Cells(col).FindControl("lblday")

            If lblDay.Text = "1" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Green
                lblDay.ForeColor = Drawing.Color.Green
                lblDay.CssClass = "lblAttend"
            End If

            If lblDay.Text = "0" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Red
                lblDay.ForeColor = Drawing.Color.Red
                lblDay.CssClass = "lblAbsent"
            End If
        Next

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

End Class