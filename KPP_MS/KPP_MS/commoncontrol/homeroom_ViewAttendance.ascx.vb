Imports System.Data.SqlClient

Public Class homeroom_ViewAttendance
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ddlYear_List()
                ddlProgram_List()
                ddlMonth_list()
                ddlLevel_List()
                ddlSem_List()

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        For col = 3 To datRespondent.Columns.Count - 3 Step col + 1
            row = 0

            For row = 0 To datRespondent.Rows.Count - 1 Step row + 1
                lblDay = datRespondent.Rows(row).Cells(col).FindControl("lblday" & col - 2)
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

            For col = 3 To datRespondent.Columns.Count Step col + 1
                row = 0

                CountPost = 0
                CountNega = 0

                For row = 0 To datRespondent.Rows.Count - 1 Step row + 1
                    lblDay = datRespondent.Rows(row).Cells(col).FindControl("lblday" & col - 2)
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String = ""
        Dim tmpSQL2 As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_Name ASC"

        tmpSQL = "SELECT student_info.student_ID, UPPER(student_info.student_Name) student_Name, attendance.course_ID, class_info.class_Name,
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
            strWhere = " where attendance.date_year = '" & ddlYear.SelectedValue & "' and student_info.student_Stream = '" & ddlProgram.SelectedValue & "' and student_info.student_Campus = '" & Session("SchoolCampus") & "'"
        End If

        strWhere += " And (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"

        strWhere += " And course.subject_ID = '" & ddlSubject_Name.SelectedValue & "'"

        strWhere += " And course.class_ID = '" & ddlClass_Name.SelectedValue & "'"

        strWhere += " And attendance.date_month = '" & ddlMonth.SelectedValue & "'"

        strWhere += " And subject_info.subject_sem = '" & ddlStudent_Sem.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & tmpSQL2

        Return getSQL

    End Function

    Protected Sub ddlProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgram.SelectedIndexChanged
        Try
            ddlCourse_List()
            ddlClass_List()
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlSubjectName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject_Name.SelectedIndexChanged
        Try
            ddlClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlClassName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass_Name.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlStudentSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudent_Sem.SelectedIndexChanged
        Try
            ddlCourse_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlStudent_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudent_Level.SelectedIndexChanged
        Try
            ddlCourse_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
        run_color()
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonth.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            run_color()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            ddlLevel_List()
            strRet = BindData(datRespondent)
            run_color()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlClass_List()

        Dim sem As String = ""

        If ddlStudent_Sem.SelectedValue = "Sem 1" Then
            sem = "Sem 2"
        ElseIf ddlStudent_Sem.SelectedValue = "Sem 2" Then
            sem = "Sem 1"
        End If

        Try
            Dim checkLevel As String = "select Subject_type from subject_info where subject_ID = '" & ddlSubject_Name.SelectedValue & "'"
            Dim getLevel As String = oCommon.getFieldValue(checkLevel)

            If getLevel = "Compulsory" Then
                strSQL = "select class_ID, class_Name from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlStudent_Level.SelectedValue & "' and stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and class_Campus = '" & Session("SchoolCampus") & "' and course_Program = '" & ddlProgram.SelectedValue & "' order by class_Name ASC "
            Else
                strSQL = "select class_ID, class_Name from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_type <> 'Compulsory' and class_Level = '" & ddlStudent_Level.SelectedValue & "' and subject_ID = '" & ddlSubject_Name.SelectedValue & "' and class_Campus = '" & Session("SchoolCampus") & "' and course_Program = '" & ddlProgram.SelectedValue & "' order by class_Name ASC "
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
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlYear_List()
        Try
            Dim strLevelSql As String = "select class_year from class_info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' order by class_Year asc "
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlYear.DataSource = levds
            ddlYear.DataValueField = "class_year"
            ddlYear.DataTextField = "class_year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlProgram_List()
        Try
            If Session("SchoolCampus") = "APP" Then
                strSQL = "select Parameter, Value from setting where type = 'Stream' and Value = 'PS'"
            Else
                strSQL = "select Parameter, Value from setting where type = 'Stream'"
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlProgram.DataSource = levds
            ddlProgram.DataValueField = "Value"
            ddlProgram.DataTextField = "Parameter"
            ddlProgram.DataBind()
            ddlProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

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
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlLevel_List()
        Try
            Dim strLevelSql As String = "select class_Level from class_info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and class_year = '" & ddlYear.SelectedValue & "' order by class_Level asc "
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlStudent_Level.DataSource = levds
            ddlStudent_Level.DataValueField = "class_Level"
            ddlStudent_Level.DataTextField = "class_Level"
            ddlStudent_Level.DataBind()
            ddlStudent_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlStudent_Level.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCourse_List()
        Try
            strSQL = "select subject_info.subject_Name, subject_info.subject_ID from subject_info"
            strSQL += " where subject_info.subject_year ='" & ddlYear.SelectedValue & "' and subject_Campus = '" & Session("SchoolCampus") & "' and course_Program = '" & ddlProgram.SelectedValue & "'"
            strSQL += " and subject_info.subject_StudentYear ='" & ddlStudent_Level.SelectedValue & "'"
            strSQL += " and subject_info.subject_sem ='" & ddlStudent_Sem.SelectedValue & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubject_Name.DataSource = ds
            ddlSubject_Name.DataTextField = "subject_Name"
            ddlSubject_Name.DataValueField = "subject_ID"
            ddlSubject_Name.DataBind()
            ddlSubject_Name.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlSubject_Name.SelectedIndex = 0
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

End Class