Imports System.Data.SqlClient

Public Class parent_attendanceData
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
                ddlYear_List()
                ddlMonth_list()

                strRet = BindData(datRespondent)

            End If
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

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging

        datRespondent.PageIndex = e.NewPageIndex
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim data_ID As String = Request.QueryString("std_ID")

        Dim tmpSQL As String
        Dim tmpSQL2 As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_info.subject_Name ASC"

        tmpSQL = "SELECT attendance.course_ID, subject_info.subject_Name, subject_info.subject_code, 
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
                    LEFT JOIN student_level ON course.std_ID = student_level.std_ID
                    LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

        tmpSQL2 = " GROUP BY attendance.course_ID, subject_info.subject_Name, subject_info.subject_code"

        strWhere = " WHERE student_info.std_ID = '" & data_ID & "' AND attendance.date_month = '" & ddlMonth.SelectedValue & "'"

        If ddlYear.SelectedIndex > 0 Then
            strWhere += " AND attendance.date_year = '" & ddlYear.SelectedValue & "'"
        End If

        If ddlSemester.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_sem = '" & ddlSemester.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & tmpSQL2 & strOrderby

        Return getSQL

    End Function

    Protected Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
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

    Private Sub ddlYear_List()

        Try

            Dim strLevelSql As String = "select distinct year from student_Level where std_ID = '" & Request.QueryString("std_ID") & "' order by year asc "
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlYear.DataSource = levds
            ddlYear.DataValueField = "year"
            ddlYear.DataTextField = "year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlSem_List()

        Dim strLevelSql As String = "Select Parameter from setting where Type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

        Try

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlSemester.DataSource = levds
            ddlSemester.DataValueField = "Parameter"
            ddlSemester.DataTextField = "Parameter"
            ddlSemester.DataBind()
            ddlSemester.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlSemester.SelectedIndex = 0

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

        Finally
            objConn.Dispose()
        End Try
    End Sub
End Class