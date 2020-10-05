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

                ddlSem_List()
                ddlClass_List()
                ddlYear_List()
                ddlMonth_list()

                load_page()
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        ''ddl month date
        strSQL = "SELECT Parameter from setting where value ='" & Now.Month & "' and Type = 'month'"

        Dim dmonth As DataSet = New DataSet
        sqlDA.Fill(dmonth, "AnyTable")

        Dim nRowsMonth As Integer = 0
        Dim nCountMonth As Integer = 1
        Dim MyTableMonth As DataTable = New DataTable
        MyTableMonth = dmonth.Tables(0)
        If MyTableMonth.Rows.Count > 0 Then
            If Not IsDBNull(dmonth.Tables(0).Rows(0).Item("Parameter")) Then
                ddlMonth.SelectedValue = dmonth.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlMonth.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete attendance where ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim tmpSQL2 As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY attendance.ID ASC"

        tmpSQL = "SELECT student_info.student_ID, student_info.student_Name, attendance.course_ID, 
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
		            MAX(CASE WHEN date_day = 31 THEN attendance_Remarks ELSE NULL END) AS 'R31',
                    COUNT(attendance_Status)-SUM(attendance_Status) AS 'Total Absence',
					CONCAT(SUM(attendance_Status)*100/COUNT(attendance_Status),'%') AS 'Percentage'	
                    FROM attendance
	                LEFT JOIN course ON attendance.course_ID = course.course_ID
	                LEFT JOIN student_info ON course.std_ID = student_info.std_ID
	                LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
	                LEFT JOIN student_level ON course.student_ID = student_level.student_ID
	                LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

        tmpSQL2 = " GROUP BY attendance.course_ID, student_info.student_ID, student_info.student_Name"

        strWhere = " WHERE subject_info.subject_sem = '" & ddlStudent_Sem.SelectedValue & "'"

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " And (student_info.student_ID Like '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent.Text & "%')"
        End If

        If ddlSubject_Name.SelectedIndex > 0 Then
            strWhere += " And class_info.class_Name = '" & ddlClass_Name.SelectedValue & "'"
        End If

        If ddlClass_Name.SelectedIndex > 0 Then
            strWhere += " And subject_info.subject_ID = '" & ddlSubject_Name.SelectedValue & "'"
        End If

        If ddlMonth.SelectedValue > 0 Then
            strWhere += " And attendance.date_month = '" & ddlMonth.SelectedValue & "'"
        End If

        If ddlYear.SelectedIndex > 0 Then
            strWhere += " And attendance.date_year = '" & ddlYear.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & tmpSQL2

        Return getSQL

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
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

        'edit UI

        Dim col As Integer
        Dim row As Integer
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
                    lblDay.BackColor = Drawing.Color.LightGreen
                    lblDay.ForeColor = Drawing.Color.LightGreen
                    lblDay.CssClass = "lblAttend"

                End If
            Next
        Next

    End Sub

    Protected Sub ddlClassName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass_Name.SelectedIndexChanged
        Try
            Dim subjectLevel As String = ""
            subjectLevel = "select class_Level from class_info where class_Name ='" & ddlClass_Name.SelectedValue & "'"
            Dim dataSubjectLevel As String = getFieldValue(subjectLevel, strConn)

            strSQL = "select subject_Name, subject_ID from subject_info"
            strSQL += " where subject_year ='" & Now.Year & "'"
            strSQL += " and  subject_StudentYear ='" & dataSubjectLevel & "'"
            strSQL += " and  subject_sem ='" & ddlStudent_Sem.SelectedValue & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubject_Name.DataSource = ds
            ddlSubject_Name.DataTextField = "subject_Name"
            ddlSubject_Name.DataValueField = "subject_ID"
            ddlSubject_Name.DataBind()
            ddlSubject_Name.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlSubject_Name.SelectedIndex = 0

            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try

        'edit UI

        Dim col As Integer
        Dim row As Integer
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
                    lblDay.BackColor = Drawing.Color.LightGreen
                    lblDay.ForeColor = Drawing.Color.LightGreen
                    lblDay.CssClass = "lblAttend"

                End If
            Next
        Next

    End Sub

    Protected Sub ddlStudentSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudent_Sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

        'edit UI

        Dim col As Integer
        Dim row As Integer
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
                    lblDay.BackColor = Drawing.Color.LightGreen
                    lblDay.ForeColor = Drawing.Color.LightGreen
                    lblDay.CssClass = "lblAttend"

                End If
            Next
        Next

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged

        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

        'edit UI

        Dim col As Integer
        Dim row As Integer
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
                    lblDay.BackColor = Drawing.Color.LightGreen
                    lblDay.ForeColor = Drawing.Color.LightGreen
                    lblDay.CssClass = "lblAttend"

                End If
            Next
        Next
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonth.SelectedIndexChanged

        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

        'edit UI

        Dim col As Integer
        Dim row As Integer
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
                    lblDay.BackColor = Drawing.Color.LightGreen
                    lblDay.ForeColor = Drawing.Color.LightGreen
                    lblDay.CssClass = "lblAttend"

                End If
            Next
        Next
    End Sub

    Private Sub ddlClass_List()
        Try

            Dim data As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

            Dim strLevelSql As String = "SELECT class_Name FROM class_info where class_year = '" & Now.Year & "' AND stf_ID = '" & data & "'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlClass_Name.DataSource = levds
            ddlClass_Name.DataValueField = "class_Name"
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
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear.SelectedValue = Now.Year
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlSem_List()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Sem'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlStudent_Sem.DataSource = levds
            ddlStudent_Sem.DataValueField = "Parameter"
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

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class