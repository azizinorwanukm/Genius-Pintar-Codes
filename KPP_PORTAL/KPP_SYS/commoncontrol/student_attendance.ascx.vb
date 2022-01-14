Imports System.Data.SqlClient

Public Class student_attendance
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
                ddlDay_List()
                ddlMonth_List()
                ddlAttendance_List()

                load_page()
                strRet = BindData(datRespondent)
                ''Generate_Table()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        ''ddl day date
        strSQL = "SELECT Parameter from setting where Value ='" & Date.Today.Day & "' and Type = 'day'"

        Dim dday As DataSet = New DataSet
        sqlDA.Fill(dday, "AnyTable")

        Dim nRowsDay As Integer = 0
        Dim nCountDay As Integer = 1
        Dim MyTableDay As DataTable = New DataTable
        MyTableDay = dday.Tables(0)
        If MyTableDay.Rows.Count > 0 Then
            If Not IsDBNull(dday.Tables(0).Rows(0).Item("Parameter")) Then
                ddlDay.SelectedValue = dday.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlDay.SelectedValue = ""
            End If
        End If

        ''ddl month date
        strSQL = "SELECT Parameter from setting where value ='" & Date.Today.Month & "' and Type = 'month'"

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

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
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
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select course.course_ID,student_info.student_Name,student_info.student_ID,student_info.student_Mykad,subject_info.subject_Name,class_info.class_Name from student_info
                  left join student_level on student_info.std_ID = student_level.std_ID
                  Left Join course on student_info.std_ID = course.std_ID
                  Left Join subject_info on course.subject_ID = subject_info.subject_ID
                  Left join class_info on course.class_ID = class_info.class_ID"
        strWhere = " WHERE student_level.year = '" & Now.Year & "'"

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " And student_info.student_ID Like '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent.Text & "%'"
        End If

        If ddlStudent_Sem.SelectedIndex > 0 Then
            strWhere += " And subject_info.subject_sem = '" & ddlStudent_Sem.SelectedValue & "'"
        End If

        If ddlClass_Name.SelectedIndex > 0 Then
            strWhere += " And class_info.class_Name = '" & ddlClass_Name.SelectedValue & "'"
        End If

        If ddlSubject_Name.SelectedIndex > 0 Then
            strWhere += " And subject_info.subject_ID = '" & ddlSubject_Name.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL

    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then
                    Dim courseID As String = "select course_ID from course where course_ID ='" & strKey & "'"
                    Dim dataCourseID As String = getFieldValue(courseID, strConn)

                    Try
                        ''insert to attendance database
                        Using STDDATA As New SqlCommand("insert into attendance(course_ID,attendance_Status,date_day,date_month,date_year) values 
                                                        ('" & dataCourseID & "','" & ddlAttendance.SelectedValue & "','" & ddlDay.SelectedValue & "',
                                                        '" & ddlMonth.SelectedValue & "','" & Now.Year & "')", objConn)
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

                    errorCount = 0
                Else
                    errorCount = 1
                End If
            End If

        Next

        If errorCount > 0 Then
            Response.Redirect("admin_pelajar_kehadiran.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        Else
            Response.Redirect("admin_pelajar_kehadiran.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        End If
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

    Protected Sub ddlSubjectName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject_Name.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlClassName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass_Name.SelectedIndexChanged
        Try
            Dim subjectLevel As String = ""
            subjectLevel = "select class_Level from class_info where class_Name ='" & ddlClass_Name.SelectedValue & "'"
            Dim dataSubjectLevel As String = getFieldValue(subjectLevel, strConn)

            strSQL = "select subject_Name,subject_ID from subject_info "
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

    End Sub

    Protected Sub ddlStudentSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudent_Sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ddlClass_List()
        Try
            Dim strLevelSql As String = "Select class_Name,class_Level from class_info where class_year = '" & Now.Year & "'"
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

    Private Sub ddlDay_List()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'day'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlDay.DataSource = levds
            ddlDay.DataValueField = "Parameter"
            ddlDay.DataTextField = "Parameter"
            ddlDay.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlMonth_List()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'month'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlMonth.DataSource = levds
            ddlMonth.DataValueField = "Parameter"
            ddlMonth.DataTextField = "Parameter"
            ddlMonth.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlAttendance_List()
        Try
            Dim strLevelSql As String = "Select Parameter,Value from setting where Type = 'Attendance'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlAttendance.DataSource = levds
            ddlAttendance.DataValueField = "Value"
            ddlAttendance.DataTextField = "Parameter"
            ddlAttendance.DataBind()
            ddlAttendance.Items.Insert(0, New ListItem("Select Attendance", String.Empty))
            ddlAttendance.SelectedIndex = 0
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


End Class