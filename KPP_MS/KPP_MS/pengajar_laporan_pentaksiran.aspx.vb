Imports System.Data.SqlClient

Public Class pengajar_laporan_pentaksiran
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

                Dim data As String = oCommon.securityLogin(Request.QueryString("stf_ID"))

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    year_list()
                    program_list()
                    student_class()
                    exam_list()

                    strRet = BindData(datRespondent)

                End If

            End If
        Catch ex As Exception

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

    Private Sub year_list()
        Try
            strSQL = "select distinct class_year from class_info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and class_Campus = '" & Session("SchoolCampus") & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "class_year"
            ddlYear.DataValueField = "class_year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub program_list()
        Try
            If Session("SchoolCampus") = "APP" Then
                strSQL = "select Parameter, Value from setting where Type = 'Stream' and Value = 'PS'"
            Else
                strSQL = "select Parameter, Value from setting where Type = 'Stream' and Value <> 'Temp'"
            End If

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlProgram.DataSource = ds
            ddlProgram.DataTextField = "Parameter"
            ddlProgram.DataValueField = "Value"
            ddlProgram.DataBind()
            ddlProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_class()

        strSQL = "select class_Name, class_ID from class_info 
                  where class_info.class_year = '" & ddlYear.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "' and class_info.course_Program = '" & ddlProgram.SelectedValue & "'
                  and class_info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"

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

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub exam_list()
        Try
            ''get class level
            strSQL = "Select class_Level from class_info where class_ID = '" & ddlClass.SelectedValue & "' and class_year = '" & ddlYear.SelectedValue & "' and class_Campus = '" & Session("SchoolCampus") & "'"
            Dim class_data As String = oCommon.getFieldValue(strSQL)

            If class_data = "Foundation 1" Then
                strSQL = "SELECT exam_Name, exam_ID FROM exam_Info WHERE exam_Year = '" & ddlYear.SelectedValue & "' and (Exam_Name = 'Exam 1' or Exam_Name = 'Exam 2' or Exam_Name = 'Exam 3' or Exam_Name = 'Exam 4') order by exam_Name ASC"
            ElseIf class_data = "Foundation 2" Then
                strSQL = "SELECT exam_Name, exam_ID FROM exam_Info WHERE exam_Year = '" & ddlYear.SelectedValue & "' and (Exam_Name = 'Exam 5' or Exam_Name = 'Exam 6' or Exam_Name = 'Exam 7' or Exam_Name = 'Exam 8') order by exam_Name ASC"
            ElseIf class_data = "Foundation 3" Then
                strSQL = "SELECT exam_Name, exam_ID FROM exam_Info WHERE exam_Year = '" & ddlYear.SelectedValue & "' and (Exam_Name = 'Exam 9' or Exam_Name = 'Exam 10' or Exam_Name = 'Exam 11' or Exam_Name = 'Exam 12') order by exam_Name ASC"
            ElseIf class_data = "Level 1" Then
                strSQL = "SELECT exam_Name, exam_ID FROM exam_Info WHERE exam_Year = '" & ddlYear.SelectedValue & "' and (Exam_Name = 'Exam 1' or Exam_Name = 'Exam 2' or Exam_Name = 'Exam 3' or Exam_Name = 'Exam 4') order by exam_Name ASC"
            ElseIf class_data = "Level 2" Then
                strSQL = "SELECT exam_Name, exam_ID FROM exam_Info WHERE exam_Year = '" & ddlYear.SelectedValue & "' and (Exam_Name = 'Exam 5' or Exam_Name = 'Exam 6' or Exam_Name = 'Exam 7' or Exam_Name = 'Exam 8') order by exam_Name ASC"
            End If

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
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

    Private Sub subject_data()
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

            ''get class level
            strSQL = "Select class_Level from class_info where class_ID = '" & ddlClass.SelectedValue & "' and class_year = '" & ddlYear.SelectedValue & "' and class_Campus = '" & Session("SchoolCampus") & "'"
            Dim class_data As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT subject_ID, subject_Name FROM subject_info WHERE subject_StudentYear = '" & class_data & "' 
                      and subject_year = '" & ddlYear.SelectedValue & "' and subject_sem = '" & get_ExamLevel & "' and subject_Campus = '" & Session("SchoolCampus") & "' and course_Program = '" & ddlProgram.SelectedValue & "'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubject.DataSource = ds
            ddlSubject.DataTextField = "subject_Name"
            ddlSubject.DataValueField = "subject_ID"
            ddlSubject.DataBind()
            ddlSubject.Items.Insert(0, New ListItem("Select Course", String.Empty))


        Catch ex As Exception
        End Try
    End Sub

    Private Function getSQL() As String

        Dim strSelect As String = ""
        Dim strWhere As String = ""
        Dim strGroupby As String = ""
        Dim strOrderby As String = " ORDER BY marks DESC"

        Dim get_SubjectType As String = "Select subject_type from subject_info where subject_ID = '" & ddlSubject.SelectedValue & "'"
        Dim data_SubjectType As String = oCommon.getFieldValue(get_SubjectType)

        strSelect = "SELECT
			        course.std_ID,
			        UPPER(student_info.student_Name) student_Name,
                    student_info.student_ID,
			        student_info.student_Mykad,
			        class_info.class_Name,
			        exam_result.marks,
			        exam_result.grade
			        FROM
			        exam_result
			        LEFT JOIN course ON exam_result.course_ID = course.course_ID
			        LEFT JOIN student_info ON course.std_ID = student_info.std_ID
			        LEFT JOIN subject_info ON course.subject_ID = subject_info.subject_ID
			        LEFT JOIN grade_info ON exam_result.grade = grade_info.grade_Name
			        LEFT JOIN class_info ON course.class_ID = class_info.class_ID"

        If data_SubjectType <> "Compulsory" Then
            strWhere = " WHERE
			            exam_result.exam_ID = '" & ddlExam.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "' and student_info.student_Campus = '" & Session("SchoolCampus") & "'
                        AND course.year = '" & ddlYear.SelectedValue & "' and class_info.course_Program = '" & ddlProgram.SelectedValue & "' and subject_info.course_Program = '" & ddlProgram.SelectedValue & "'
			            AND class_info.subject_ID = '" & ddlSubject.SelectedValue & "'
                        AND subject_info.subject_ID = '" & ddlSubject.SelectedValue & "'"

        ElseIf data_SubjectType = "Compulsory" Then

            strWhere = " WHERE
			            exam_result.exam_ID = '" & ddlExam.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "' and student_info.student_Campus = '" & Session("SchoolCampus") & "'
                        AND course.year = '" & ddlYear.SelectedValue & "'
			            AND class_info.class_ID = '" & ddlClass.SelectedValue & "'
                        AND subject_info.subject_ID = '" & ddlSubject.SelectedValue & "'"

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
        subject_data()
    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass.SelectedIndexChanged
        exam_list()
    End Sub

    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubject.SelectedIndexChanged
        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        student_class()
    End Sub

    Protected Sub ddlProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgram.SelectedIndexChanged
        subject_data()
        student_class()
    End Sub
End Class