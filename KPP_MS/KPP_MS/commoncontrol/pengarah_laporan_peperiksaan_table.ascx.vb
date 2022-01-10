Imports System.Data.SqlClient

Public Class pengarah_laporan_peperiksaan_table
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim MyConnection As SqlConnection = New SqlConnection(strConn)
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                year_list()
                campus_list()
                program_list()
                exam_list()
                student_level()
                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try

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
            ddlYear.SelectedValue = Now.Year

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
        Try

            strSQL = "Select exam_Name from exam_info where exam_ID = '" & ddlExam.SelectedValue & "'"
            Dim get_ExamName As String = oCommon.getFieldValue(strSQL)

            If get_ExamName = "Exam 1" Or get_ExamName = "Exam 2" Or get_ExamName = "Exam 3" Or get_ExamName = "Exam 4" Then
                strSQL = "SELECT Parameter FROM setting WHERE Type = 'Level' and (Parameter = 'Foundation 1' or Parameter = 'Level 1')"
            ElseIf get_ExamName = "Exam 5" Or get_ExamName = "Exam 6" Or get_ExamName = "Exam 7" Or get_ExamName = "Exam 8" Then
                strSQL = "SELECT Parameter FROM setting WHERE Type = 'Level' and (Parameter = 'Foundation 2' or Parameter = 'Level 2')"
            ElseIf get_ExamName = "Exam 9" Or get_ExamName = "Exam 10" Or get_ExamName = "Exam 11" Or get_ExamName = "Exam 12" Then
                strSQL = "SELECT Parameter FROM setting WHERE Type = 'Level' and Parameter = 'Foundation 3'"
            End If

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception

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

    Private Function getSQL() As String

        Dim strSelect As String = ""
        Dim strWhere As String = ""
        Dim strGroupby As String = ""
        Dim strOrderby As String = " ORDER BY 'CGPA' DESC"

        Dim examData As String = "select exam_Name from exam_info where exam_ID = '" & ddlExam.SelectedValue & "'"
        Dim getExam As String = oCommon.getFieldValue(examData)

        strSelect = "SELECT
                    course.std_ID,
                    UPPER(student_info.student_Name) student_Name,
                    student_info.student_ID,
                    class_info.class_Name,
                    SUM(grade_info.gpa * subject_info.subject_CreditHour) AS 'gpa X credithour',
                    student_Png.png as 'GPA',
                    student_Png.pngs as 'CGPA'
                    FROM
                    exam_result
                    LEFT JOIN course ON exam_result.course_ID = course.course_ID
                    LEFT JOIN student_info ON course.std_ID = student_info.std_ID
                    LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
                    LEFT JOIN grade_info ON exam_result.grade = grade_info.grade_Name
                    LEFT JOIN class_info ON course.class_ID = class_info.class_ID
                    Left Join student_Png on student_info.std_ID = student_Png.std_ID"

        strWhere = " WHERE exam_result.exam_ID = '" & ddlExam.SelectedValue & "' and student_info.student_Stream = '" & ddlProgram.SelectedValue & "' and student_info.student_Campus = '" & ddlCampus.SelectedValue & "'
                    and student_Png.exam_Name = '" & getExam & "' and class_info.course_Program = '" & ddlProgram.SelectedValue & "' and class_info.class_Campus = '" & ddlCampus.SelectedValue & "'
                    and class_info.class_year = '" & ddlYear.SelectedValue & "'
                    and student_png.year = '" & ddlYear.SelectedValue & "'
                    and course.year = '" & ddlYear.SelectedValue & "'
                    and subject_info.subject_year = '" & ddlYear.SelectedValue & "'
                    and class_info.class_type = 'Compulsory' and (student_info.student_status = 'Access' or student_info.student_status = 'Graduate') and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%') "

        If ddlLevel.SelectedValue <> "" Then
            strWhere += " and class_info.class_level = '" & ddlLevel.SelectedValue & "' "
        End If

        strGroupby = " GROUP BY
                        course.std_ID,
                        student_Name,
                        student_info.student_ID,
                        class_info.class_Name,
                        student_Png.png,
                        student_Png.pngs"

        getSQL = strSelect & strWhere & strGroupby & strOrderby

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

            Dim gpaCount As Label = datRespondent.Rows(i).FindControl("CGPA")

            If gpaCount.Text = 4.0 Then

                count40 = count40 + 1

            ElseIf gpaCount.Text >= 3.9 Then

                countmore39 = countmore39 + 1

            ElseIf gpaCount.Text >= 3.8 Then

                countmore38 = countmore38 + 1

            ElseIf gpaCount.Text >= 3.7 Then

                countmore37 = countmore37 + 1

            ElseIf gpaCount.Text >= 3.6 Then

                countmore36 = countmore36 + 1

            ElseIf gpaCount.Text >= 3.5 Then

                countmore35 = countmore35 + 1

            ElseIf gpaCount.Text >= 3.4 Then

                countmore34 = countmore34 + 1

            ElseIf gpaCount.Text >= 3.3 Then

                countmore33 = countmore33 + 1

            ElseIf gpaCount.Text >= 3.2 Then

                countmore32 = countmore32 + 1

            ElseIf gpaCount.Text >= 3.1 Then

                countmore31 = countmore31 + 1

            ElseIf gpaCount.Text >= 3.0 Then

                countmore30 = countmore30 + 1

            ElseIf gpaCount.Text >= 2.9 Then

                countmore29 = countmore29 + 1

            ElseIf gpaCount.Text >= 2.8 Then

                countmore28 = countmore28 + 1

            ElseIf gpaCount.Text >= 2.7 Then

                countmore27 = countmore27 + 1

            ElseIf gpaCount.Text >= 2.6 Then

                countmore26 = countmore26 + 1

            ElseIf gpaCount.Text >= 2.5 Then

                countmore25 = countmore25 + 1

            ElseIf gpaCount.Text < 2.5 Then

                countless25 = countless25 + 1

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
        countmore290.Value = countmore29
        countmore280.Value = countmore28
        countmore270.Value = countmore27
        countmore260.Value = countmore26
        countmore250.Value = countmore25
        countless250.Value = countless25

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        exam_list()
        student_level()
        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlCampus_SelectedIndexChanged(sender As Object, e As EventArgs)
        program_list()
        exam_list()
        student_level()
        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlProgram_SelectedIndexChanged(sender As Object, e As EventArgs)
        exam_list()
        student_level()
        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlExam_SelectedIndexChanged(sender As Object, e As EventArgs)
        student_level()
        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Private Sub BtnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        ExportToCSV(getSQL)
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
        Response.AddHeader("content-disposition", "attachment;filename=Student Ranking List.csv")
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