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

                Dim id As String = Request.QueryString("stf_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    year_list()

                    load_page()
                    student_class()
                    strRet = BindData(datRespondent)

                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter from setting where Parameter ='" & Now.Year & "' and Type = 'Year'"

        '--debug
        ''Response.Write(strSQLstd) 

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
                ddlYear.SelectedValue = ""
            End If
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

    Private Sub student_class()
        Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        strSQL = "select class_Name, class_ID from class_info 
                  where class_info.class_year = '" & ddlYear.SelectedValue & "'
                  and class_info.stf_ID = '" & DATA_STAFFID & "'"

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

            strSQL = "SELECT exam_Name, exam_ID FROM exam_Info WHERE exam_Year = '" & ddlYear.SelectedValue & "'"

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
            lblExamination.Text = oCommon.getFieldValue(strSQL)

            Dim get_ExamLevel As String = ""

            If exam_data = "Exam 1" Or exam_data = "Exam 2" Or exam_data = "Exam 5" Or exam_data = "Exam 6" Or exam_data = "Exam 9" Or exam_data = "Exam 10" Then
                get_ExamLevel = "Sem 1"

            ElseIf exam_data = "Exam 3" Or exam_data = "Exam 4" Or exam_data = "Exam 7" Or exam_data = "Exam 8" Or exam_data = "Exam 11" Or exam_data = "Exam 12" Then
                get_ExamLevel = "Sem 2"
            End If

            ''get class level
            strSQL = "Select class_Level from class_info where class_ID = '" & ddlClass.SelectedValue & "'"
            Dim class_data As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT subject_ID, subject_Name FROM subject_info WHERE subject_StudentYear = '" & class_data & "' 
                      and subject_year = '" & ddlYear.SelectedValue & "' and subject_sem = '" & get_ExamLevel & "'"
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

        Dim get_SubjectName As String = "Select subject_Name from subject_info where subject_ID = '" & ddlSubject.SelectedValue & "'"
        lblCourses.Text = oCommon.getFieldValue(get_SubjectName)

        strSelect = "SELECT
			        course.std_ID,
			        student_info.student_Name,
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
			            exam_result.exam_ID = '" & ddlExam.SelectedValue & "'
                        AND course.year = '" & ddlYear.SelectedValue & "'
			            AND class_info.subject_ID = subject_info.subject_ID
                        AND subject_info.subject_ID = '" & ddlSubject.SelectedValue & "'"

        ElseIf data_SubjectType = "Compulsory" Then

            strWhere = " WHERE
			            exam_result.exam_ID = '" & ddlExam.SelectedValue & "'
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
End Class