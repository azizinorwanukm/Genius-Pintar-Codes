Imports System.Data.SqlClient

Public Class student_attendance
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

                Checking_MenuAccess_Load()

                If Session("getStatus") = "VA" Then ''View Attendance
                    txtbreadcrum1.Text = "View Attendance"

                    ViewAttendance.Visible = True
                    UpdateAttendance.Visible = False

                    btnViewAttendance.Attributes("class") = "btn btn-info"
                    btnUpdateAttendance.Attributes("class") = "btn btn-default font"

                    ddlYear_List()
                    ddlMonth_list()
                    ddlLevel_List()
                    ddlSem_List()
                    ddlProgram_List()

                    strRet = BindData(viewRespondent)

                    checkHide.Visible = False

                ElseIf Session("getStatus") = "UA" Then ''Update Attendance
                    txtbreadcrum1.Text = "Update Attendance"

                    ViewAttendance.Visible = False
                    UpdateAttendance.Visible = True

                    btnViewAttendance.Attributes("class") = "btn btn-default font"
                    btnUpdateAttendance.Attributes("class") = "btn btn-info"

                    ddlYear_List()
                    ddlMonth_list()
                    ddlLevel_List()
                    ddlSem_List()
                    ddlProgram_List()

                    ddlDay_list()
                    ddlStatus_list()

                    checkHide.Visible = True

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewAttendance.Visible = False
        btnUpdateAttendance.Visible = False
        ViewAttendance.Visible = False
        UpdateAttendance.Visible = False

        btnUpdateStudentAttendance.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewAttendance As String = ""
        Dim Get_UpdateAttendance As String = ""

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

            ''Get Function Button 1 Update Data 
            strSQL = "  Select B.F1_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Update As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "View Attendance" And find_Data_SubMenu2.Length > 0 Then
                btnViewAttendance.Visible = True
                ViewAttendance.Visible = True

                Get_ViewAttendance = "TRUE"
            End If

            If find_Data_SubMenu2 = "Update Attendance" And find_Data_SubMenu2.Length > 0 Then
                btnUpdateAttendance.Visible = True
                UpdateAttendance.Visible = True

                Get_UpdateAttendance = "TRUE"

                If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                    btnUpdateAttendance.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewAttendance.Visible = True
                btnUpdateAttendance.Visible = True
                ViewAttendance.Visible = True
                UpdateAttendance.Visible = True

                btnUpdateStudentAttendance.Visible = True

                Get_ViewAttendance = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "VA" Or Session("getStatus") = "UA" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "VA" And Session("getStatus") <> "UA" Then
            If Get_ViewAttendance = "TRUE" Then
                Data_If_Not_Group_Status = "VA"
            ElseIf Get_UpdateAttendance = "TRUE" Then
                Data_If_Not_Group_Status = "UA"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewAttendance = "TRUE" And Data_If_Not_Group_Status = "VA" Then
                Session("getStatus") = "VA"
            ElseIf Get_UpdateAttendance = "TRUE" And Data_If_Not_Group_Status = "UA" Then
                Session("getStatus") = "UA"
            End If
        End If

    End Sub

    Private Sub btnViewAttendance_ServerClick(sender As Object, e As EventArgs) Handles btnViewAttendance.ServerClick
        Session("getStatus") = "VA"
        Response.Redirect("admin_pelajar_kehadiran.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnUpdateAttendance_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateAttendance.ServerClick
        Session("getStatus") = "UA"
        Response.Redirect("admin_pelajar_kehadiran.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        For col = 3 To viewRespondent.Columns.Count - 3 Step col + 1
            row = 0

            For row = 0 To viewRespondent.Rows.Count - 1 Step row + 1
                lblDay = viewRespondent.Rows(row).Cells(col).FindControl("lblday" & col - 2)
                If lblDay.Text = "0" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Red
                    lblDay.ForeColor = Drawing.Color.Red
                    lblDay.CssClass = "lblAbsent"

                End If

                If lblDay.Text = "1" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Green
                    lblDay.ForeColor = Drawing.Color.Green
                    lblDay.CssClass = "lblAttend"

                End If

                If lblDay.Text = "2" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Yellow
                    lblDay.ForeColor = Drawing.Color.Yellow
                    lblDay.CssClass = "lblOthers"

                End If
            Next
        Next
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            Dim col As Integer = 0
            Dim row As Integer = 0
            Dim lblDay As Label

            Dim CountPost As Integer = 0
            Dim CountNega As Integer = 0

            For col = 3 To viewRespondent.Columns.Count Step col + 1
                row = 0

                CountPost = 0
                CountNega = 0

                For row = 0 To viewRespondent.Rows.Count - 1 Step row + 1
                    lblDay = viewRespondent.Rows(row).Cells(col).FindControl("lblday" & col - 2)
                    If lblDay.Text <> "0" And lblDay.Text <> "1" Then
                        CountNega += 1
                    Else
                        CountPost += 1
                    End If
                Next

                If CountPost > 0 Then
                    gvTable.Columns(col).Visible = True
                Else
                    gvTable.Columns(col).Visible = False
                End If
            Next

            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function BindData2(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL2, strConn)
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

        Dim tmpSQL As String = ""
        Dim tmpSQL2 As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "SELECT student_info.student_ID, student_info.student_Name, attendance.course_ID, class_info.class_Name,
                    MAX(CASE WHEN date_day = 1 THEN attendance_Status ELSE NULL END) AS '1',
                    MAX(CASE WHEN date_day = 2 THEN attendance_Status ELSE NULL END) AS '2',
                    MAX(CASE WHEN date_day = 3 THEN attendance_Status ELSE NULL END) AS '3',
                    MAX(CASE WHEN date_day = 4 THEN attendance_Status ELSE NULL END) AS '4',
                    MAX(CASE WHEN date_day = 5 THEN attendance_Status ELSE NULL END) AS '5',
                    MAX(CASE WHEN date_day = 6 THEN attendance_Status ELSE NULL END) AS '6',
                    MAX(CASE WHEN date_day = 7 THEN attendance_Status ELSE NULL END) AS '7',
                    MAX(CASE WHEN date_day = 8 THEN attendance_Status ELSE NULL END) AS '8',
                    MAX(CASE WHEN date_day = 9 THEN attendance_Status ELSE NULL END) AS '9',
                    MAX(CASE WHEN date_day = 10 THEN attendance_Status ELSE NULL END) AS '10',
		            MAX(CASE WHEN date_day = 11 THEN attendance_Status ELSE NULL END) AS '11',
                    MAX(CASE WHEN date_day = 12 THEN attendance_Status ELSE NULL END) AS '12',
                    MAX(CASE WHEN date_day = 13 THEN attendance_Status ELSE NULL END) AS '13',
                    MAX(CASE WHEN date_day = 14 THEN attendance_Status ELSE NULL END) AS '14',
                    MAX(CASE WHEN date_day = 15 THEN attendance_Status ELSE NULL END) AS '15',
                    MAX(CASE WHEN date_day = 16 THEN attendance_Status ELSE NULL END) AS '16',
                    MAX(CASE WHEN date_day = 17 THEN attendance_Status ELSE NULL END) AS '17',
                    MAX(CASE WHEN date_day = 18 THEN attendance_Status ELSE NULL END) AS '18',
                    MAX(CASE WHEN date_day = 19 THEN attendance_Status ELSE NULL END) AS '19',
                    MAX(CASE WHEN date_day = 20 THEN attendance_Status ELSE NULL END) AS '20',
		            MAX(CASE WHEN date_day = 21 THEN attendance_Status ELSE NULL END) AS '21',
                    MAX(CASE WHEN date_day = 22 THEN attendance_Status ELSE NULL END) AS '22',
                    MAX(CASE WHEN date_day = 23 THEN attendance_Status ELSE NULL END) AS '23',
                    MAX(CASE WHEN date_day = 24 THEN attendance_Status ELSE NULL END) AS '24',
                    MAX(CASE WHEN date_day = 25 THEN attendance_Status ELSE NULL END) AS '25',
                    MAX(CASE WHEN date_day = 26 THEN attendance_Status ELSE NULL END) AS '26',
                    MAX(CASE WHEN date_day = 27 THEN attendance_Status ELSE NULL END) AS '27',
                    MAX(CASE WHEN date_day = 28 THEN attendance_Status ELSE NULL END) AS '28',
                    MAX(CASE WHEN date_day = 29 THEN attendance_Status ELSE NULL END) AS '29',
                    MAX(CASE WHEN date_day = 30 THEN attendance_Status ELSE NULL END) AS '30',
		            MAX(CASE WHEN date_day = 31 THEN attendance_Status ELSE NULL END) AS '31',
                    MAX(CASE WHEN date_day = 1 THEN attendance_Remarks ELSE NULL END) AS 'R1',
                    MAX(CASE WHEN date_day = 2 THEN attendance_Remarks ELSE NULL END) AS 'R2',
                    MAX(CASE WHEN date_day = 3 THEN attendance_Remarks ELSE NULL END) AS 'R3',
                    MAX(CASE WHEN date_day = 4 THEN attendance_Remarks ELSE NULL END) AS 'R4',
                    MAX(CASE WHEN date_day = 5 THEN attendance_Remarks ELSE NULL END) AS 'R5',
                    MAX(CASE WHEN date_day = 6 THEN attendance_Remarks ELSE NULL END) AS 'R6',
                    MAX(CASE WHEN date_day = 7 THEN attendance_Remarks ELSE NULL END) AS 'R7',
                    MAX(CASE WHEN date_day = 8 THEN attendance_Remarks ELSE NULL END) AS 'R8',
                    MAX(CASE WHEN date_day = 9 THEN attendance_Remarks ELSE NULL END) AS 'R9',
                    MAX(CASE WHEN date_day = 10 THEN attendance_Remarks ELSE NULL END) AS 'R10',
		            MAX(CASE WHEN date_day = 11 THEN attendance_Remarks ELSE NULL END) AS 'R11',
                    MAX(CASE WHEN date_day = 12 THEN attendance_Remarks ELSE NULL END) AS 'R12',
                    MAX(CASE WHEN date_day = 13 THEN attendance_Remarks ELSE NULL END) AS 'R13',
                    MAX(CASE WHEN date_day = 14 THEN attendance_Remarks ELSE NULL END) AS 'R14',
                    MAX(CASE WHEN date_day = 15 THEN attendance_Remarks ELSE NULL END) AS 'R15',
                    MAX(CASE WHEN date_day = 16 THEN attendance_Remarks ELSE NULL END) AS 'R16',
                    MAX(CASE WHEN date_day = 17 THEN attendance_Remarks ELSE NULL END) AS 'R17',
                    MAX(CASE WHEN date_day = 18 THEN attendance_Remarks ELSE NULL END) AS 'R18',
                    MAX(CASE WHEN date_day = 19 THEN attendance_Remarks ELSE NULL END) AS 'R19',
                    MAX(CASE WHEN date_day = 20 THEN attendance_Remarks ELSE NULL END) AS 'R20',
		            MAX(CASE WHEN date_day = 21 THEN attendance_Remarks ELSE NULL END) AS 'R21',
                    MAX(CASE WHEN date_day = 22 THEN attendance_Remarks ELSE NULL END) AS 'R22',
                    MAX(CASE WHEN date_day = 23 THEN attendance_Remarks ELSE NULL END) AS 'R23',
                    MAX(CASE WHEN date_day = 24 THEN attendance_Remarks ELSE NULL END) AS 'R24',
                    MAX(CASE WHEN date_day = 25 THEN attendance_Remarks ELSE NULL END) AS 'R25',
                    MAX(CASE WHEN date_day = 26 THEN attendance_Remarks ELSE NULL END) AS 'R26',
                    MAX(CASE WHEN date_day = 27 THEN attendance_Remarks ELSE NULL END) AS 'R27',
                    MAX(CASE WHEN date_day = 28 THEN attendance_Remarks ELSE NULL END) AS 'R28',
                    MAX(CASE WHEN date_day = 29 THEN attendance_Remarks ELSE NULL END) AS 'R29',
                    MAX(CASE WHEN date_day = 30 THEN attendance_Remarks ELSE NULL END) AS 'R30',
		            MAX(CASE WHEN date_day = 31 THEN attendance_Remarks ELSE NULL END) AS 'R31'
                    FROM attendance
	                LEFT JOIN course ON attendance.course_ID = course.course_ID
                    LEFT JOIN student_info ON course.std_ID = student_info.std_ID
                    LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
                    LEFT JOIN student_level ON course.std_ID = student_level.std_ID
                    LEFT JOIN class_info ON course.class_ID = class_info.class_ID
                    LEFT JOIN lecturer ON class_info.class_ID = lecturer.class_ID
                    LEFT JOIN staff_Info ON lecturer.stf_ID = staff_Info.stf_ID"

        tmpSQL2 = " GROUP BY attendance.course_ID, student_info.student_ID, student_info.student_Name, class_info.class_Name"

        If ddlStudent_Sem.SelectedIndex = 0 And ddlClass_Name.SelectedIndex = 0 And ddlSubject_Name.SelectedIndex = 0 Then
            strWhere += " WHERE  attendance.date_year = '9999999'" ''impossible
        Else
            strWhere = " where attendance.date_year = '" & ddlYear.SelectedValue & "'"
        End If

        strWhere += " And (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%') and student_info.student_Campus = 'PGPN'"

        strWhere += " And course.subject_ID = '" & ddlSubject_Name.SelectedValue & "'"

        strWhere += " And course.class_ID = '" & ddlClass_Name.SelectedValue & "'"

        strWhere += " And attendance.date_month = '" & ddlMonth.SelectedValue & "'"

        strWhere += " And subject_info.subject_sem = '" & ddlStudent_Sem.SelectedValue & "'"

        strWhere += " And subject_info.course_Program = '" & ddlStudent_Program.SelectedValue & "' and class_info.course_Program = '" & ddlStudent_Program.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & tmpSQL2

        Return getSQL

    End Function

    Private Function getSQL2() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name, attendance.date_day ASC"

        tmpSQL = "  SELECT course.course_ID, student_info.student_Name, class_info.class_Name, subject_info.subject_Name,
                    attendance.date_day, (CASE when attendance.attendance_Status = 1 Then 'Attend' Else 'Absent' END) as status, attendance.attendance_Remarks from student_info
                    LEFT JOIN course on student_info.std_ID = course.std_ID
                    LEFT JOIN subject_info on course.subject_ID = subject_info.subject_ID
                    LEFT JOIN class_info on course.class_ID = class_info.class_ID
                    LEFT JOIN attendance on course.course_ID = attendance.course_ID "

        strWhere += " WHERE course.year = '" & ddlYear.SelectedValue & "' and ( student_info.student_status = 'Access' or student_info.student_Status = 'Graduate' ) and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%') and student_info.student_Campus = 'PGPN' "

        strWhere += " AND attendance.date_month = '" & ddlMonth.SelectedValue & "'"

        strWhere += " And subject_info.subject_sem = '" & ddlAddAttendanceSem.SelectedValue & "'"

        strWhere += " And course.class_ID = '" & ddlAddAttendanceClass.SelectedValue & "'"

        strWhere += " And subject_info.subject_ID = '" & ddlAddAttendanceCourse.SelectedValue & "'"

        strWhere += " And subject_info.course_Program = '" & ddlAddAttendanceProgram.SelectedValue & "' and class_info.course_Program = '" & ddlAddAttendanceProgram.SelectedValue & "'"

        If ddlAddAttendanceDay.SelectedIndex > 0 Then
            strWhere += " AND attendance.date_day = '" & ddlAddAttendanceDay.SelectedValue & "'"
        End If

        getSQL2 = tmpSQL & strWhere & strOrderby

        Return getSQL2

    End Function

    Protected Sub ddlStudent_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudent_Program.SelectedIndexChanged
        Try
            strRet = BindData(viewRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlAddAttendanceProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAddAttendanceProgram.SelectedIndexChanged
        Try
            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSubjectName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject_Name.SelectedIndexChanged
        Try
            ddlClass_List()
            strRet = BindData(viewRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlAddAttendanceCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAddAttendanceCourse.SelectedIndexChanged
        Try
            ddlClass_List()
            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlClassName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass_Name.SelectedIndexChanged
        Try
            strRet = BindData(viewRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlAddAttendanceClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAddAttendanceClass.SelectedIndexChanged
        Try
            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlStudentSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudent_Sem.SelectedIndexChanged
        Try
            ddlCourse_List()
            strRet = BindData(viewRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlAddAttendanceSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAddAttendanceSem.SelectedIndexChanged
        Try
            ddlCourse_List()
            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlStudent_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudent_Level.SelectedIndexChanged
        Try
            ddlCourse_List()
            strRet = BindData(viewRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlAddAttendanceLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAddAttendanceLevel.SelectedIndexChanged
        Try
            ddlCourse_List()
            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonth.SelectedIndexChanged
        Try
            strRet = BindData(viewRespondent)
            strRet = BindData2(addRespondent)
            run_color()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            ddlStudent_Sem.SelectedIndex = 0
            ddlClass_Name.SelectedIndex = 0
            ddlSubject_Name.SelectedIndex = 0

            strRet = BindData(viewRespondent)
            strRet = BindData2(addRespondent)
            run_color()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAddAttendanceDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAddAttendanceDay.SelectedIndexChanged
        Try
            strRet2 = BindData2(addRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlClass_List()

        Dim sem As String = ""

        If (ddlStudent_Sem.SelectedValue = "Sem 1" Or ddlAddAttendanceSem.SelectedValue = "Sem 1") Then
            sem = "Sem 2"
        ElseIf (ddlStudent_Sem.SelectedValue = "Sem 2" Or ddlAddAttendanceSem.SelectedValue = "Sem 2") Then
            sem = "Sem 1"
        End If

        Try
            Dim checkLevel As String = "select Subject_type from subject_info where (subject_ID = '" & ddlSubject_Name.SelectedValue & "' or subject_ID = '" & ddlAddAttendanceCourse.SelectedValue & "')"
            Dim getLevel As String = oCommon.getFieldValue(checkLevel)

            If getLevel = "Compulsory" Then
                strSQL = "select class_ID, class_Name from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_type = 'Compulsory' and (class_Level = '" & ddlStudent_Level.SelectedValue & "' or class_Level = '" & ddlAddAttendanceLevel.SelectedValue & "') and class_Campus = 'PGPN'  and (class_info.course_Program = '" & ddlStudent_Program.SelectedValue & "' or class_info.course_Program = '" & ddlAddAttendanceProgram.SelectedValue & "') order by class_Name ASC "
            Else
                strSQL = "select class_ID, class_Name from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_type <> 'Compulsory' and (class_Level = '" & ddlStudent_Level.SelectedValue & "' or class_Level = '" & ddlAddAttendanceLevel.SelectedValue & "') and (subject_ID = '" & ddlSubject_Name.SelectedValue & "' or subject_ID = '" & ddlAddAttendanceCourse.SelectedValue & "') and class_Campus = 'PGPN'  and (class_info.course_Program = '" & ddlStudent_Program.SelectedValue & "' or class_info.course_Program = '" & ddlAddAttendanceProgram.SelectedValue & "') order by class_Name ASC "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlClass_Name.DataSource = levds
            ddlClass_Name.DataValueField = "class_ID"
            ddlClass_Name.DataTextField = "class_Name"
            ddlClass_Name.DataBind()
            ddlClass_Name.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlClass_Name.SelectedIndex = 0

            ddlAddAttendanceClass.DataSource = levds
            ddlAddAttendanceClass.DataValueField = "class_ID"
            ddlAddAttendanceClass.DataTextField = "class_Name"
            ddlAddAttendanceClass.DataBind()
            ddlAddAttendanceClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlAddAttendanceClass.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlYear_List()
        Try
            Dim strLevelSql As String = "SELECT Parameter FROM setting WHERE Type = 'Year'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlYear.DataSource = levds
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlStatus_list()
        Try
            Dim strLevelSql As String = "SELECT Parameter FROM setting WHERE Type = '999999'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlStatus.DataSource = levds
            ddlStatus.DataValueField = "Parameter"
            ddlStatus.DataTextField = "Parameter"
            ddlStatus.DataBind()
            ddlStatus.Items.Insert(0, New ListItem("Select Status", String.Empty))
            ddlStatus.Items.Insert(1, New ListItem("Absent", 0))
            ddlStatus.Items.Insert(2, New ListItem("Attend", 1))
            ddlStatus.SelectedIndex = 0


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlSem_List()
        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Sem'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlStudent_Sem.DataSource = levds
            ddlStudent_Sem.DataValueField = "Value"
            ddlStudent_Sem.DataTextField = "Parameter"
            ddlStudent_Sem.DataBind()
            ddlStudent_Sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlStudent_Sem.SelectedIndex = 0

            ddlAddAttendanceSem.DataSource = levds
            ddlAddAttendanceSem.DataValueField = "Value"
            ddlAddAttendanceSem.DataTextField = "Parameter"
            ddlAddAttendanceSem.DataBind()
            ddlAddAttendanceSem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlAddAttendanceSem.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlProgram_list()
        Try
            Dim strLevelSql As String = "SELECT * FROM setting WHERE Type = 'Stream'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlStudent_Program.DataSource = levds
            ddlStudent_Program.DataValueField = "Value"
            ddlStudent_Program.DataTextField = "Parameter"
            ddlStudent_Program.DataBind()
            ddlStudent_Program.Items.Insert(0, New ListItem("Select Course Program", String.Empty))
            ddlStudent_Program.SelectedIndex = 0

            ddlAddAttendanceProgram.DataSource = levds
            ddlAddAttendanceProgram.DataValueField = "Value"
            ddlAddAttendanceProgram.DataTextField = "Parameter"
            ddlAddAttendanceProgram.DataBind()
            ddlAddAttendanceProgram.Items.Insert(0, New ListItem("Select Course Program", String.Empty))
            ddlAddAttendanceProgram.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlLevel_List()
        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlStudent_Level.DataSource = levds
            ddlStudent_Level.DataValueField = "Parameter"
            ddlStudent_Level.DataTextField = "Parameter"
            ddlStudent_Level.DataBind()
            ddlStudent_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlStudent_Level.SelectedIndex = 0

            ddlAddAttendanceLevel.DataSource = levds
            ddlAddAttendanceLevel.DataValueField = "Parameter"
            ddlAddAttendanceLevel.DataTextField = "Parameter"
            ddlAddAttendanceLevel.DataBind()
            ddlAddAttendanceLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlAddAttendanceLevel.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCourse_List()
        Try
            strSQL = "select subject_info.subject_Name, subject_info.subject_ID from subject_info"
            strSQL += " where subject_info.subject_year ='" & ddlYear.SelectedValue & "' and (subject_info.course_Program = '" & ddlStudent_Program.SelectedValue & "' or subject_info.course_Program = '" & ddlAddAttendanceProgram.SelectedValue & "')"
            strSQL += " and (subject_info.subject_StudentYear ='" & ddlStudent_Level.SelectedValue & "' or subject_info.subject_StudentYear ='" & ddlAddAttendanceLevel.SelectedValue & "')"
            strSQL += " and (subject_info.subject_sem ='" & ddlStudent_Sem.SelectedValue & "' or subject_info.subject_sem ='" & ddlAddAttendanceSem.SelectedValue & "')"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubject_Name.DataSource = ds
            ddlSubject_Name.DataTextField = "subject_Name"
            ddlSubject_Name.DataValueField = "subject_ID"
            ddlSubject_Name.DataBind()
            ddlSubject_Name.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlSubject_Name.SelectedIndex = 0

            ddlAddAttendanceCourse.DataSource = ds
            ddlAddAttendanceCourse.DataTextField = "subject_Name"
            ddlAddAttendanceCourse.DataValueField = "subject_ID"
            ddlAddAttendanceCourse.DataBind()
            ddlAddAttendanceCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlAddAttendanceCourse.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlMonth_list()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type = 'month'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMonth.DataSource = ds
            ddlMonth.DataTextField = "Parameter"
            ddlMonth.DataValueField = "Value"
            ddlMonth.DataBind()
            ddlMonth.Items.Insert(0, New ListItem("Select Month", String.Empty))
            ddlMonth.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlDay_list()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type = 'day'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAddAttendanceDay.DataSource = ds
            ddlAddAttendanceDay.DataTextField = "Parameter"
            ddlAddAttendanceDay.DataValueField = "Value"
            ddlAddAttendanceDay.DataBind()
            ddlAddAttendanceDay.Items.Insert(0, New ListItem("Select Day", String.Empty))
            ddlAddAttendanceDay.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdateStudentAttendance_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStudentAttendance.ServerClick

        Dim execQuery As Integer = 0
        Dim errorCount As Integer = 0
        Dim i As Integer
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        If ddlAddAttendanceSem.SelectedIndex = 0 Then
            execQuery = 1
        End If

        If ddlAddAttendanceClass.SelectedIndex = 0 Then
            execQuery = 1
        End If

        If ddlAddAttendanceCourse.SelectedIndex = 0 Then
            execQuery = 1
        End If

        If execQuery = 0 Then

            For i = 0 To addRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(addRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                Dim textRemarks As TextBox = addRespondent.Rows(i).FindControl("attendance_Remarks")
                ' Get the values of textboxes using findControl
                Dim strKey As String = addRespondent.DataKeys(i).Value.ToString

                If chkUpdate.Checked = True Then

                    strSQL = "UPDATE attendance SET attendance_Status = '" & ddlStatus.SelectedValue & "', attendance_Remarks = UPPER('" & textRemarks.Text & "') WHERE course_ID = '" & strKey & "' AND date_day = '" & ddlAddAttendanceDay.SelectedValue & "' AND date_month = '" & ddlMonth.SelectedValue & "' AND date_year = '" & ddlYear.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then
                        ShowMessage("Update Attendance", MessageType.Success)

                        strRet2 = BindData2(addRespondent)
                    Else
                        ShowMessage("Error Update Attendance", MessageType.Success)
                    End If
                End If
            Next
        End If

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class