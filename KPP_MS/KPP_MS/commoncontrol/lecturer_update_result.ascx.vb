Imports System.Data.SqlClient
Imports System.Globalization

Public Class lecturer_update_result
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

                ddlExamClass.Enabled = False
                ddlExamCourse.Enabled = False

                ddlYear()
                ddlProgram()
                ddlExam()
                ddlLevel()

            End If
        Catch ex As Exception

        End Try
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

            If gvTable.Rows.Count = 0 Then
                Btnsimpan.Visible = False
            Else
                Btnsimpan.Visible = True
            End If

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by student_info.student_Name ASC"

        tmpSQL = "select distinct exam_result.ID, exam_result.course_ID, student_info.student_ID, student_info.student_Name, class_info.class_Name, exam_Info.exam_Name, subject_info.subject_Name, exam_result.marks, exam_result.grade
                  From exam_result 
                  Left Join course On exam_result.course_ID = course.course_ID
                  Left Join exam_info On exam_result.exam_ID = exam_Info.exam_ID
                  Left Join student_info On course.std_ID = student_info.std_ID
                  Left Join class_info On course.class_ID = class_info.class_ID
                  Left Join subject_info On course.subject_ID = subject_info.subject_ID 
                  left Join student_Png On student_info.std_ID=student_Png.std_ID
                  Where exam_result.ID Is Not null and class_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and student_info.student_Campus = '" & Session("SchoolCampus") & "'
                  and subject_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "'"

        If ddlExamYear.SelectedIndex > 0 Then
            strWhere += " And exam_Info.exam_Year = '" & ddlExamYear.SelectedValue & "' and (exam_Info.exam_Institutions = '" & Session("SchoolCampus") & "' or exam_Info.exam_Institutions = 'ALL')"
        Else
            strWhere += " And exam_Info.exam_Year = '" & Now.Year & "'"
        End If

        If ddlExamName.SelectedIndex > 0 Then
            strWhere += " And exam_Info.exam_Name = '" & ddlExamName.SelectedValue & "'"
        End If

        If ddlExamClass.SelectedIndex > 0 Then
            strWhere += " And course.class_ID = '" & ddlExamClass.SelectedValue & "'"
        End If

        strWhere += " And subject_info.subject_ID = '" & ddlExamCourse.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby
        '--debug

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

    Protected Sub ddlExamLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamLevel.SelectedIndexChanged

        Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        Dim get_ExamSem As String = ""

        If ddlExamLevel.SelectedIndex > 0 Then

            If ddlExamName.SelectedValue = "Exam 1" Or ddlExamName.SelectedValue = "Exam 2" Or ddlExamName.SelectedValue = "Exam 5" Or ddlExamName.SelectedValue = "Exam 6" Or ddlExamName.SelectedValue = "Exam 9" Or ddlExamName.SelectedValue = "Exam 10" Then
                get_ExamSem = "Sem 1"
            Else
                get_ExamSem = "Sem 2"
            End If

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim STDLEVEL As New SqlDataAdapter()

            strSQL = "  select distinct lecturer.class_ID,class_info.class_Name from class_info
                        left join lecturer on class_info.class_ID = lecturer.class_ID
                        where class_Level ='" & ddlExamLevel.SelectedValue & "' And class_year = '" & ddlExamYear.SelectedValue & "' And lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "ClassTable")

            ddlExamClass.DataSource = ds
            ddlExamClass.DataTextField = "class_Name"
            ddlExamClass.DataValueField = "class_ID"
            ddlExamClass.DataBind()
            ddlExamClass.Items.Insert(0, New ListItem("Select Class", String.Empty))

            strSQL = "select distinct subject_info.subject_Name, subject_info.subject_ID from subject_info
                      left join lecturer on subject_info.subject_ID = lecturer.subject_ID
                      where subject_StudentYear ='" & ddlExamLevel.SelectedValue & "' and subject_info.subject_sem = '" & get_ExamSem & "'
                      and subject_info.subject_year = '" & ddlExamYear.SelectedValue & "' and lecturer.stf_ID = '" & DATA_STAFFID & "'and subject_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "'"
            Dim sqlSub As New SqlDataAdapter(strSQL, objConn)

            Dim subds As DataSet = New DataSet
            sqlSub.Fill(subds, "SubjectTable")

            ddlExamCourse.DataSource = subds
            ddlExamCourse.DataTextField = "subject_Name"
            ddlExamCourse.DataValueField = "subject_ID"
            ddlExamCourse.DataBind()
            ddlExamCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))

            ddlExamClass.Enabled = True
            ddlExamCourse.Enabled = True

        Else
            ddlExamClass.Items.Clear()
            ddlExamCourse.Items.Clear()
            ddlExamClass.Enabled = False
            ddlExamCourse.Enabled = False

        End If

    End Sub

    Protected Sub ddlYear()
        Try
            Dim stryear As String = "select distinct lecturer_year from lecturer where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddlExamYear.DataSource = yrds
            ddlExamYear.DataValueField = "lecturer_year"
            ddlExamYear.DataTextField = "lecturer_year"
            ddlExamYear.DataBind()
            ddlExamYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlProgram()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "select Parameter, Value from setting where type = 'Stream' and Value = 'PS'"
        Else
            strSQL = "select Parameter, Value from setting where type = 'Stream' and Value <> 'Temp'"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamProgram.DataSource = ds
            ddlExamProgram.DataTextField = "Parameter"
            ddlExamProgram.DataValueField = "Value"
            ddlExamProgram.DataBind()
            ddlExamProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddlExamProgram.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlExam()

        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Exam'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "ExamTable")

            ddlExamName.DataSource = levds
            ddlExamName.DataValueField = "Parameter"
            ddlExamName.DataTextField = "Parameter"
            ddlExamName.DataBind()
            ddlExamName.Items.Insert(0, New ListItem("Select Examination", String.Empty))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlLevel()
        Try
            Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

            Dim strLevelSql As String = ""

            If ddlExamName.SelectedValue = "Exam 1" Or ddlExamName.SelectedValue = "Exam 2" Then

                strLevelSql = " select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and (class_info.class_Level = 'Foundation 1' or  class_info.class_Level = 'Level 1') and class_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'"

            ElseIf ddlExamName.SelectedValue = "Exam 3" Or ddlExamName.SelectedValue = "Exam 4" Then
                strLevelSql = " select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and (class_info.class_Level = 'Foundation 1' or  class_info.class_Level = 'Level 1') and class_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'"

            ElseIf ddlExamName.SelectedValue = "Exam 5" Or ddlExamName.SelectedValue = "Exam 6" Then
                strLevelSql = "select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and (class_info.class_Level = 'Foundation 2' or  class_info.class_Level = 'Level 2') and class_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'"

            ElseIf ddlExamName.SelectedValue = "Exam 7" Or ddlExamName.SelectedValue = "Exam 8" Then
                strLevelSql = " select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and (class_info.class_Level = 'Foundation 2' or  class_info.class_Level = 'Level 2') and class_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'"

            ElseIf ddlExamName.SelectedValue = "Exam 9" Or ddlExamName.SelectedValue = "Exam 10" Then
                strLevelSql = " select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.class_Level = 'Foundation 3' and class_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'"

            ElseIf ddlExamName.SelectedValue = "Exam 11" Or ddlExamName.SelectedValue = "Exam 12" Then
                strLevelSql = " select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.class_Level = 'Foundation 3' and class_info.course_Program = '" & ddlExamProgram.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "'"

            End If

            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlExamLevel.DataSource = levds
            ddlExamLevel.DataValueField = "class_Level"
            ddlExamLevel.DataTextField = "class_Level"
            ddlExamLevel.DataBind()
            ddlExamLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlExamCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamCourse.SelectedIndexChanged
        Try

            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        Dim find_examEndDate As String = "Select exam_EndDate from exam_info where exam_Year = '" & ddlExamYear.SelectedValue & "' and exam_Name = '" & ddlExamName.SelectedValue & "' and exam_Institutions = '" & Session("SchoolCampus") & "'"
        Dim get_examEndDate As String = oCommon.getFieldValue(find_examEndDate)

        Dim convertToDate_examEndDate As DateTime = DateTime.ParseExact(get_examEndDate, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
        Dim formatted_examEndDate As String = convertToDate_examEndDate.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo)

        Dim get_currentDate As String = DateTime.Now.ToString("yyyyMMdd")

        If get_currentDate < formatted_examEndDate Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim marks As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtmarks"), TextBox)
                Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

                ''update marks
                strSQL = "UPDATE exam_result SET marks='" & marks.Text & "' WHERE ID ='" & strKeyID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Dim ResultGrades As String = ""

                If marks.Text = "100" Or marks.Text = "100.00" Or marks.Text = "100.0" Then
                    ''select grades based on marks 

                    ResultGrades = "select grade_Name from grade_info where grade_min_range <= '" & marks.Text & "' and grade_max_range >= '" & marks.Text & "'"

                Else
                    ''select grades based on marks 

                    ResultGrades = "select grade_Name from grade_info where grade_min_range <= '" & marks.Text & "' and grade_max_range >= '" & marks.Text & "'"

                End If

                Dim grades As String = getFieldValue(ResultGrades, strConn)

                ''update grades and gpa
                strSQL = "UPDATE exam_result SET grade='" & grades & "' WHERE ID ='" & strKeyID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    errorCount = 0
                Else
                    errorCount = 1
                End If

            Next

        Else
            ShowMessage(" Unable To Update Student Result After " & get_examEndDate, MessageType.Error)
        End If

        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlExamName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamName.SelectedIndexChanged
        ddlLevel()
    End Sub

    Protected Sub ddlExamProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamProgram.SelectedIndexChanged
        Try
            ddlLevel()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlExamClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamClass.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class