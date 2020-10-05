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

                ddlSem_List()
                ddlYear_List()
                ddlMonth_list()
                ddlDay_list()
                ddlStatus_list()

                load_page()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter FROM setting WHERE Type = 'Year' and Parameter  = '" & Now.Year & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlYear.SelectedValue = Now.Year
            End If
        End If

        lblYear.Text = ddlYear.SelectedValue
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
                    lblDay.BackColor = Drawing.Color.LightGreen
                    lblDay.ForeColor = Drawing.Color.LightGreen
                    lblDay.CssClass = "lblAttend"

                End If
            Next
        Next
    End Sub

    Private Sub viewRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles viewRespondent.PageIndexChanging
        viewRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(viewRespondent)
        strRet2 = BindData2(addRespondent)
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

        If ddlStudent_Sem.SelectedIndex = 0 Or ddlClass_Name.SelectedIndex = 0 Or ddlSubject_Name.SelectedIndex = 0 Then
            strWhere += " WHERE  attendance.date_year = '9999999'" ''impossible
        Else
            strWhere = " where attendance.date_year = '" & ddlYear.SelectedValue & "'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " And (student_info.student_ID = '" & txtstudent.Text & "'"
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
            strWhere += " OR student_info.student_Mykad = '" & txtstudent.Text & "')"
        End If

        If ddlSubject_Name.SelectedIndex > 0 Then
            strWhere += " And course.subject_ID = '" & ddlSubject_Name.SelectedValue & "'"
        End If

        If ddlClass_Name.SelectedIndex > 0 Then
            strWhere += " And course.class_ID = '" & ddlClass_Name.SelectedValue & "'"
        End If

        If ddlMonth.SelectedIndex > 0 Then
            strWhere += " And attendance.date_month = '" & ddlMonth.SelectedValue & "'"
        End If

        If ddlStudent_Sem.SelectedIndex > 0 Then
            strWhere += " And subject_info.subject_sem = '" & ddlStudent_Sem.SelectedValue & "'"
        End If

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

        If ddlStudent_Sem.SelectedIndex = 0 Or ddlClass_Name.SelectedIndex = 0 Or ddlSubject_Name.SelectedIndex = 0 Then
            strWhere += " WHERE course.year = '9999999'" ''impossible
        Else
            strWhere += " WHERE course.year = '" & ddlYear.SelectedValue & "'"
        End If

        If ddlMonth.SelectedIndex > 0 Then
            strWhere += " AND attendance.date_month = '" & ddlMonth.SelectedValue & "'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " And (student_info.student_ID = '" & txtstudent.Text & "'"
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
            strWhere += " OR student_info.student_Mykad = '" & txtstudent.Text & "')"
        End If

        If ddlStudent_Sem.SelectedIndex > 0 Then
            strWhere += " And subject_info.subject_sem = '" & ddlStudent_Sem.SelectedValue & "'"
        End If

        If ddlClass_Name.SelectedIndex > 0 Then
            strWhere += " And course.class_ID = '" & ddlClass_Name.SelectedValue & "'"
        End If

        If ddlSubject_Name.SelectedIndex > 0 Then
            strWhere += " And subject_info.subject_ID = '" & ddlSubject_Name.SelectedValue & "'"
        End If

        If ddlDay.SelectedIndex > 0 Then
            strWhere += " AND attendance.date_day = '" & ddlDay.SelectedValue & "'"
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

    Protected Sub ddlSubjectName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject_Name.SelectedIndexChanged
        Try
            strRet = BindData(viewRespondent)
            strRet2 = BindData2(addRespondent)

        Catch ex As Exception
        End Try

        run_color()

    End Sub

    Protected Sub ddlClassName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass_Name.SelectedIndexChanged
        Try
            Dim subjectLevel As String = ""
            subjectLevel = "select class_Level from class_info where class_ID ='" & ddlClass_Name.SelectedValue & "'"
            Dim dataSubjectLevel As String = getFieldValue(subjectLevel, strConn)

            strSQL = "select subject_info.subject_Name, subject_info.subject_ID from subject_info"
            strSQL += " where subject_info.subject_year ='" & ddlYear.SelectedValue & "'"
            strSQL += " and  subject_info.subject_StudentYear ='" & dataSubjectLevel & "'"
            strSQL += " and  subject_info.subject_sem ='" & ddlStudent_Sem.SelectedValue & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubject_Name.DataSource = ds
            ddlSubject_Name.DataTextField = "subject_Name"
            ddlSubject_Name.DataValueField = "subject_ID"
            ddlSubject_Name.DataBind()
            ddlSubject_Name.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlSubject_Name.SelectedIndex = 0

            run_color()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlStudentSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudent_Sem.SelectedIndexChanged
        Try
            ddlClass_List()

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonth.SelectedIndexChanged
        Try
            If ddlStudent_Sem.SelectedIndex > 0 And ddlClass_Name.SelectedIndex > 0 And ddlSubject_Name.SelectedIndex > 0 Then
                strRet = BindData(viewRespondent)
                strRet = BindData2(addRespondent)

                run_color()
            End If

        Catch ex As Exception
        End Try

        Dim get_month As String = "Select Parameter from Setting where Type = 'MOnth' and Value = '" & ddlMonth.SelectedValue & "'"
        lblMonth.Text = oCommon.getFieldValue(get_month)
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            If ddlStudent_Sem.SelectedIndex > 0 And ddlClass_Name.SelectedIndex > 0 And ddlSubject_Name.SelectedIndex > 0 Then

                ddlStudent_Sem.SelectedIndex = 0
                ddlClass_Name.SelectedIndex = 0
                ddlSubject_Name.SelectedIndex = 0

                strRet = BindData(viewRespondent)
                strRet = BindData2(addRespondent)

                run_color()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(viewRespondent)
            strRet2 = BindData2(addRespondent)

        Catch ex As Exception
        End Try

        run_color()
    End Sub

    Protected Sub ddlDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDay.SelectedIndexChanged
        Try
            strRet2 = BindData2(addRespondent)
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
            Dim strLevelSql As String = "SELECT * FROM class_info where class_year = '" & ddlYear.SelectedValue & "' and (class_sem <> '" & sem & "' or class_type = 'Compulsory')  order by class_Name ASC"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

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
            Dim strLevelSql As String = "SELECT Parameter FROM setting WHERE Type = 'Year'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlYear.DataSource = levds
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataBind()

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

            ddlDay.DataSource = ds
            ddlDay.DataTextField = "Parameter"
            ddlDay.DataValueField = "Value"
            ddlDay.DataBind()
            ddlDay.Items.Insert(0, New ListItem("Select Day", String.Empty))
            ddlDay.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim execQuery As Integer = 0
        Dim errorCount As Integer = 0
        Dim i As Integer
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        If ddlStudent_Sem.SelectedIndex = 0 Then
            execQuery = 1
        End If

        If ddlClass_Name.SelectedIndex = 0 Then
            execQuery = 1
        End If

        If ddlSubject_Name.SelectedIndex = 0 Then
            execQuery = 1
        End If

        If execQuery = 0 Then

            For i = 0 To addRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(addRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                Dim textRemarks As TextBox = addRespondent.Rows(i).FindControl("attendance_Remarks")
                ' Get the values of textboxes using findControl
                Dim strKey As String = addRespondent.DataKeys(i).Value.ToString

                If chkUpdate.Checked = True Then

                    strSQL = "UPDATE attendance SET attendance_Status = '" & ddlStatus.SelectedValue & "', attendance_Remarks = UPPER('" & textRemarks.Text & "') WHERE course_ID = '" & strKey & "' AND date_day = '" & ddlDay.SelectedValue & "' AND date_month = '" & ddlMonth.SelectedValue & "' AND date_year = '" & ddlYear.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then
                        ShowMessage("Update Attendance", MessageType.Success)

                        strRet = BindData(viewRespondent)
                        strRet2 = BindData2(addRespondent)
                    Else
                        ShowMessage("Error Update Attendance", MessageType.Success)
                    End If
                End If
            Next
        End If

        run_color()

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class