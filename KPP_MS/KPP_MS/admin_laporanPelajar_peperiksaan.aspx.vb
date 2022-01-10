Imports System.Data.SqlClient

Public Class admin_laporanPelajar_peperikssan
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim MyConnection As SqlConnection = New SqlConnection(strConn)
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    year_list()
                    campus_list()
                    program_list()
                    exam_list()
                    student_level()
                    subject_list()
                    class_list()

                    load_page()
                    strRet = BindData(datRespondent)

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT year from student_Level where year ='" & Now.Year & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
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

    Private Sub year_list()
        Try
            strSQL = "SELECT Parameter FROM setting WHERE Type = 'Year'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub campus_list()
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

            ddlCampus.DataSource = ds
            ddlCampus.DataTextField = "Parameter"
            ddlCampus.DataValueField = "Value"
            ddlCampus.DataBind()
            ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub program_list()
        If ddlCampus.SelectedValue = "APP" Then
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

            ddlProgram.DataSource = ds
            ddlProgram.DataTextField = "Parameter"
            ddlProgram.DataValueField = "Value"
            ddlProgram.DataBind()
            ddlProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_level()

        strSQL = "select * from setting where Type = 'Level' "

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Value"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_list()

        strSQL = "select * from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlLevel.SelectedValue & "' and course_Program = '" & ddlProgram.SelectedValue & "' and class_Campus = '" & ddlCampus.SelectedValue & "'
                  order by class_Name ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClass.DataSource = ds
            ddlClass.DataTextField = "class_Name"
            ddlClass.DataValueField = "class_ID"
            ddlClass.DataBind()
            ddlClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlClass.Items.Insert(1, New ListItem("All", "All"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub exam_list()
        Try

            strSQL = "SELECT exam_Name, exam_ID FROM exam_Info WHERE exam_Year = '" & ddlYear.SelectedValue & "' order by exam_Name asc"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExam.DataSource = ds
            ddlExam.DataTextField = "exam_Name"
            ddlExam.DataValueField = "exam_ID"
            ddlExam.DataBind()
            ddlExam.Items.Insert(0, New ListItem("Select Exam", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub subject_list()
        Try
            ''get exam name
            strSQL = "SELECT exam_Name FROM exam_Info WHERE exam_ID = '" & ddlExam.SelectedValue & "'"
            Dim exam_data As String = oCommon.getFieldValue(strSQL)

            Dim get_ExamLevel As String = ""

            If exam_data = "Exam 1" Or exam_data = "Exam 2" Or exam_data = "Exam 5" Or exam_data = "Exam 6" Or exam_data = "Exam 9" Or exam_data = "Exam 10" Then
                get_ExamLevel = "Sem 1"
            ElseIf exam_data = "Exam 3" Or exam_data = "Exam 4" Or exam_data = "Exam 7" Or exam_data = "Exam 8" Or exam_data = "Exam 11" Or exam_data = "Exam 12" Then
                get_ExamLevel = "Sem 2"
            End If

            strSQL = "SELECT subject_ID, subject_Name FROM subject_info WHERE subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_year = '" & ddlYear.SelectedValue & "' and subject_sem = '" & get_ExamLevel & "' and course_Program = '" & ddlProgram.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourse.DataSource = ds
            ddlCourse.DataTextField = "subject_Name"
            ddlCourse.DataValueField = "subject_ID"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourse.Items.Insert(1, New ListItem("All", "All"))


        Catch ex As Exception
        End Try
    End Sub

    Private Function getSQL() As String

        Dim strSelect As String = ""
        Dim strWhere As String = ""
        Dim strGroupby As String = ""
        Dim strOrderby As String = ""

        If ddlCourse.SelectedValue = "All" Then

            strSelect = "   SELECT course.std_ID, UPPER(student_info.student_Name) student_Name, student_info.student_ID, subject_info.subject_Name, class_info.class_Name,
			                exam_result.marks, exam_result.grade
			                FROM exam_result
			                LEFT JOIN course ON exam_result.course_ID = course.course_ID
			                LEFT JOIN student_info ON course.std_ID = student_info.std_ID
			                LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
			                LEFT JOIN grade_info ON exam_result.grade = grade_info.grade_Name
			                LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

            strWhere = "    WHERE
                            student_info.student_Status = 'Access' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')
			                AND exam_result.exam_ID = '" & ddlExam.SelectedValue & "'
                            AND course.year = '" & ddlYear.SelectedValue & "'
                            and subject_info.subject_year = '" & ddlYear.SelectedValue & "'
                            and class_info.class_year = '" & ddlYear.SelectedValue & "'
                            AND subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.course_Program = '" & ddlProgram.SelectedValue & "' and subject_info.subject_Campus = '" & ddlCampus.SelectedValue & "'
                            AND class_info.class_Level = '" & ddlLevel.SelectedValue & "' and class_info.course_Program = '" & ddlProgram.SelectedValue & "' and class_info.class_Campus = '" & ddlCampus.SelectedValue & "'"

            strOrderby = " ORDER BY course_Name ASC, subject_info.subject_Name ASC, marks DESC, student_Name ASC"

        Else
            Dim get_SubjectType As String = "Select subject_type from subject_info where subject_ID = '" & ddlCourse.SelectedValue & "'"
            Dim data_SubjectType As String = oCommon.getFieldValue(get_SubjectType)

            strSelect = "SELECT course.std_ID, UPPER(student_info.student_Name) student_Name, student_info.student_ID, subject_info.subject_Name, class_info.class_Name,
			        exam_result.marks, exam_result.grade
			        FROM exam_result
			        LEFT JOIN course ON exam_result.course_ID = course.course_ID
			        LEFT JOIN student_info ON course.std_ID = student_info.std_ID
			        LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
			        LEFT JOIN grade_info ON exam_result.grade = grade_info.grade_Name
			        LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

            If data_SubjectType <> "Compulsory" Then
                strWhere = " WHERE
			                student_info.student_Status = 'Access' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')
			                AND exam_result.exam_ID = '" & ddlExam.SelectedValue & "'
                            AND course.year = '" & ddlYear.SelectedValue & "'
                            and subject_info.subject_year = '" & ddlYear.SelectedValue & "'
                            and class_info.class_year = '" & ddlYear.SelectedValue & "'
			                AND class_info.subject_ID = '" & ddlCourse.SelectedValue & "'
                            AND subject_info.subject_ID = '" & ddlCourse.SelectedValue & "'"

            ElseIf data_SubjectType = "Compulsory" Then
                strWhere = " WHERE
			                student_info.student_Status = 'Access' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')
			                AND exam_result.exam_ID = '" & ddlExam.SelectedValue & "'
                            AND course.year = '" & ddlYear.SelectedValue & "'
                            and subject_info.subject_year = '" & ddlYear.SelectedValue & "'
                            and class_info.class_year = '" & ddlYear.SelectedValue & "'
                            AND subject_info.subject_ID = '" & ddlCourse.SelectedValue & "'"
            End If

            If ddlClass.SelectedIndex > 1 Then
                strWhere += " AND class_info.class_ID = '" & ddlClass.SelectedValue & "'"
            Else
                strWhere += " AND class_info.class_Level = '" & ddlLevel.SelectedValue & "'"
            End If

            strOrderby = " ORDER BY course_Name ASC, subject_info.subject_Name ASC, marks DESC, student_Name ASC"
        End If

        getSQL = strSelect & strWhere & strOrderby

        Return getSQL

    End Function

    Private Sub graphFunction()

        Dim count40 As Integer = 0
        Dim countmore39 As Integer = 0
        Dim countmore38 As Integer = 0
        Dim countmore37 As Integer = 0
        Dim countmore36 As Integer = 0
        Dim countmore35 As Integer = 0
        Dim countmore34 As Integer = 0
        Dim countmore33 As Integer = 0
        Dim countmore32 As Integer = 0
        Dim countmore31 As Integer = 0
        Dim countmore30 As Integer = 0
        Dim countmore29 As Integer = 0
        Dim countmore28 As Integer = 0
        Dim countmore27 As Integer = 0
        Dim countmore26 As Integer = 0
        Dim countmore25 As Integer = 0
        Dim countless25 As Integer = 0

        Dim i As Integer

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim gpaCount As Label = datRespondent.Rows(i).FindControl("grade")

            If gpaCount.Text = "A+" Then

                count40 = count40 + 1

            ElseIf gpaCount.Text = "A" Then

                countmore39 = countmore39 + 1

            ElseIf gpaCount.Text = "A-" Then

                countmore38 = countmore38 + 1

            ElseIf gpaCount.Text = "B+" Then

                countmore37 = countmore37 + 1

            ElseIf gpaCount.Text = "B" Then

                countmore36 = countmore36 + 1

            ElseIf gpaCount.Text = "B-" Then

                countmore35 = countmore35 + 1

            ElseIf gpaCount.Text = "C+" Then

                countmore34 = countmore34 + 1

            ElseIf gpaCount.Text = "C" Then

                countmore33 = countmore33 + 1

            ElseIf gpaCount.Text = "D" Then

                countmore32 = countmore32 + 1

            ElseIf gpaCount.Text = "E" Then

                countmore31 = countmore31 + 1

            ElseIf gpaCount.Text = "G" Then

                countmore30 = countmore30 + 1

            End If

        Next

        count400.Value = count40
        countmore390.Value = countmore39
        countmore380.Value = countmore38
        countmore370.Value = countmore37
        countmore360.Value = countmore36
        countmore350.Value = countmore35
        countmore340.Value = countmore34
        countmore330.Value = countmore33
        countmore320.Value = countmore32
        countmore310.Value = countmore31
        countmore300.Value = countmore30

    End Sub

    Protected Sub ddlExam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExam.SelectedIndexChanged
        student_level()
        subject_list()
        class_list()
    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass.SelectedIndexChanged
        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        class_list()

        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        exam_list()
        student_level()
        subject_list()
        class_list()
    End Sub

    Protected Sub ddlCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampus.SelectedIndexChanged
        program_list()
        exam_list()
        student_level()
        subject_list()
        class_list()

        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgram.SelectedIndexChanged
        exam_list()
        student_level()
        subject_list()
        class_list()

        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        subject_list()
        class_list()

        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Private Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        If getSQL() <> "" Then
            ExportToCSV(getSQL)
        End If
    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    'export 
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & Now.Year & "_StudentResult.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

End Class