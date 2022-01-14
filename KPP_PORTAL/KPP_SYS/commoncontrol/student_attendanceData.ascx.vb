Imports System.Data.SqlClient

Public Class student_attendanceData
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
                ddlMonth_list()
                ddlLevel_List()
                ddlSem_List()
                ddlCourse_List()

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception
        End Try

        'edit UI
        Dim row As Integer
        Dim attendance_Status_Color As Label
        Dim attendance_Status_Text As Label

        For row = 0 To datRespondent.Rows.Count - 1 Step row + 1
            attendance_Status_Color = datRespondent.Rows(row).Cells(6).FindControl("attendance_Status_Color")
            If attendance_Status_Color.Text = "0" Then
                attendance_Status_Color.Text = "OO"
                attendance_Status_Color.BackColor = Drawing.Color.Red
                attendance_Status_Color.ForeColor = Drawing.Color.Red
                attendance_Status_Color.CssClass = "lblAbsent"
            End If

            If attendance_Status_Color.Text = "1" Then
                attendance_Status_Color.Text = "OO"
                attendance_Status_Color.BackColor = Drawing.Color.LightGreen
                attendance_Status_Color.ForeColor = Drawing.Color.LightGreen
                attendance_Status_Color.CssClass = "lblAttend"
            End If

            attendance_Status_Text = datRespondent.Rows(row).Cells(6).FindControl("attendance_Status_Text")
            If attendance_Status_Text.Text = "0" Then
                attendance_Status_Text.Text = "Absent"
            End If

            If attendance_Status_Text.Text = "1" Then
                attendance_Status_Text.Text = "Present"
            End If
        Next
    End Sub

    Private Sub DatRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
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

        'edit UI
        Dim row As Integer
        Dim attendance_Status_Color As Label
        Dim attendance_Status_Text As Label

        For row = 0 To datRespondent.Rows.Count - 1 Step row + 1
            attendance_Status_Color = datRespondent.Rows(row).Cells(6).FindControl("attendance_Status_Color")
            If attendance_Status_Color.Text = "0" Then
                attendance_Status_Color.Text = "OO"
                attendance_Status_Color.BackColor = Drawing.Color.Red
                attendance_Status_Color.ForeColor = Drawing.Color.Red
                attendance_Status_Color.CssClass = "lblAbsent"
            End If

            If attendance_Status_Color.Text = "1" Then
                attendance_Status_Color.Text = "OO"
                attendance_Status_Color.BackColor = Drawing.Color.LightGreen
                attendance_Status_Color.ForeColor = Drawing.Color.LightGreen
                attendance_Status_Color.CssClass = "lblAttend"
            End If

            attendance_Status_Text = datRespondent.Rows(row).Cells(6).FindControl("attendance_Status_Text")
            If attendance_Status_Text.Text = "0" Then
                attendance_Status_Text.Text = "Absent"
            End If

            If attendance_Status_Text.Text = "1" Then
                attendance_Status_Text.Text = "Present"
            End If
        Next

        Return True

    End Function

    Private Function getSQL() As String

        Dim strSelect As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY Date_Convert ASC"

        strSelect = "   SELECT 
                        attendance.course_ID,
                        concat(attendance.date_day, ' / ', attendance.date_month, ' / ', attendance.date_year) as Date,
                        convert(INT, concat(attendance.date_year, attendance.date_month, attendance.date_day)) as Date_Convert,
                        subject_info.subject_Name,
                        subject_info.subject_StudentYear,
                        subject_info.subject_sem,
                        class_info.class_Name,
                        attendance.attendance_Status,
                        attendance.attendance_Status
                        FROM attendance
                        LEFT JOIN course ON attendance.course_ID = course.course_ID
                        LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
                        LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

        strWhere = "    WHERE
                        attendance.date_year = '" & ddlYear.SelectedValue & "'
                        AND subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "'
                        AND subject_info.subject_sem = '" & ddlSemester.SelectedValue & "' AND course.subject_ID = '" & ddlCourse.SelectedValue & "'
                        AND course.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"

        If ddlMonth.SelectedIndex > 0 Then
            strWhere += "  AND attendance.date_month = '" & ddlMonth.SelectedValue & "'"
        End If

        getSQL = strSelect & strWhere & strOrderby

        Return getSQL

    End Function

    Protected Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        Try
            ddlCourse_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            ddlLevel_List()
            ddlCourse_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonth.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCourse_List()

        Dim strLevelSql As String = "   Select A.subject_ID, A.subject_Name from subject_info A left join course B on B.subject_ID = A.subject_ID
                                        Where A.subject_year = '" & ddlYear.SelectedValue & "' And B.year = '" & ddlYear.SelectedValue & "'
                                        And A.subject_StudentYear = '" & ddlLevel.SelectedValue & "' And A.subject_sem = '" & ddlSemester.SelectedValue & "'
                                        And B.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

        Try

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlCourse.DataSource = levds
            ddlCourse.DataValueField = "subject_ID"
            ddlCourse.DataTextField = "subject_Name"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourse.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlLevel_List()

        Dim strLevelSql As String = "Select distinct student_level from student_Level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

        Try

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlLevel.DataSource = levds
            ddlLevel.DataValueField = "student_level"
            ddlLevel.DataTextField = "student_level"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select level", String.Empty))
            ddlLevel.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlSem_List()

        Dim strLevelSql As String = "Select Parameter, Value from setting where Type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

        Try

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlSemester.DataSource = levds
            ddlSemester.DataValueField = "Value"
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

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlYear_List()

        Dim strLevelSql As String = "select distinct year from student_Level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' order by year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

        Try

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

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        Try

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

End Class