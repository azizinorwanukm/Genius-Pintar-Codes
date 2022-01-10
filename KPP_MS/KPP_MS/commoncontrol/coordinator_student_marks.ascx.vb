Imports System.Data.SqlClient
Imports System.Globalization

Public Class coordinator_student_marks
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

                ddlYear()
                ddlProgram()

                strRet = BindData(datRespondent)
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

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by class_info.class_Name, student_Name ASC"

        tmpSQL = "select distinct exam_result.ID, subject_info.subject_StudentYear, student_info.student_ID, 
                  UPPER(student_info.student_Name) student_Name, class_info.class_Name, exam_Info.exam_Name, subject_info.subject_Name, exam_result.marks, exam_result.grade
                  From exam_result Join course On exam_result.course_ID = course.course_ID
                  Left Join exam_info On exam_result.exam_ID = exam_Info.exam_ID
                  Left Join student_info On course.std_ID = student_info.std_ID
                  Left Join class_info On course.class_ID = class_info.class_ID
                  Left Join subject_info On course.subject_ID = subject_info.subject_ID left Join student_Png On student_info.std_ID=student_Png.std_ID
                  Where exam_result.ID Is Not null and student_info.student_Campus = '" & Session("SchoolCampus") & "' and student_info.student_Stream = '" & ddl_Program.SelectedValue & "' and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate')"
        strWhere += " And exam_Info.exam_Year = '" & ddl_year.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "'"
        strWhere += " And exam_Info.exam_Name = '" & ddl_exam.SelectedValue & "' and (exam_Info.exam_Institutions = '" & Session("SchoolCampus") & "' or exam_Info.exam_Institutions = 'ALL')"
        strWhere += " And course.subject_ID = '" & ddl_subject.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Protected Sub ddlYear()
        Try
            Dim stryear As String = "select distinct year from coordinator where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' order by year asc"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddl_year.DataSource = yrds
            ddl_year.DataValueField = "year"
            ddl_year.DataTextField = "year"
            ddl_year.DataBind()
            ddl_year.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlProgram()
        Try
            If Session("SchoolCampus") = "APP" Then
                strSQL = "select Parameter, Value from setting where type = 'Stream' and Value = 'PS'"
            Else
                strSQL = "select Parameter, Value from setting where type = 'Stream' and Value <> 'Temp'"
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Program.DataSource = levds
            ddl_Program.DataValueField = "Value"
            ddl_Program.DataTextField = "Parameter"
            ddl_Program.DataBind()
            ddl_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_info()
        Try
            Dim stryear As String = "select distinct coordinator_Level from coordinator where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and year = '" & ddl_year.SelectedValue & "' order by coordinator_Level asc"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddlLevel.DataSource = yrds
            ddlLevel.DataValueField = "coordinator_Level"
            ddlLevel.DataTextField = "coordinator_Level"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlExam()

        Try
            If ddlLevel.SelectedValue = "Foundation 1" Or ddlLevel.SelectedValue = "Level 1" Then
                strSQL = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 1' or Parameter = 'Exam 2' or Parameter = 'Exam 3' or Parameter = 'Exam 4')"
            ElseIf ddlLevel.SelectedValue = "Foundation 2" Or ddlLevel.SelectedValue = "Level 2" Then
                strSQL = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 5' or Parameter = 'Exam 6' or Parameter = 'Exam 7' or Parameter = 'Exam 8')"
            ElseIf ddlLevel.SelectedValue = "Foundation 3" Then
                strSQL = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 9' or Parameter = 'Exam 10' or Parameter = 'Exam 11' or Parameter = 'Exam 12')"
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "ExamTable")

            ddl_exam.DataSource = levds
            ddl_exam.DataValueField = "Parameter"
            ddl_exam.DataTextField = "Parameter"
            ddl_exam.DataBind()
            ddl_exam.Items.Insert(0, New ListItem("Select Examination", String.Empty))
            ddl_exam.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlExam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_exam.SelectedIndexChanged
        Try
            Dim semester As String = ""

            If ddl_exam.SelectedValue = "Exam 1" Or ddl_exam.SelectedValue = "Exam 2" Or ddl_exam.SelectedValue = "Exam 5" Or ddl_exam.SelectedValue = "Exam 6" Or ddl_exam.SelectedValue = "Exam 9" Or ddl_exam.SelectedValue = "Exam 10" Then
                semester = "Sem 1"

            ElseIf ddl_exam.SelectedValue = "Exam 3" Or ddl_exam.SelectedValue = "Exam 4" Or ddl_exam.SelectedValue = "Exam 7" Or ddl_exam.SelectedValue = "Exam 8" Or ddl_exam.SelectedValue = "Exam 11" Or ddl_exam.SelectedValue = "Exam 12" Then
                semester = "Sem 2"
            End If

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim STDLEVEL As New SqlDataAdapter()

            Dim data_ID = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

            strSQL = "Select course_Name from coordinator where year = '" & ddl_year.SelectedValue & "' and stf_ID = '" & data_ID & "' and coordinator_Level = '" & ddlLevel.SelectedValue & "' and program = '" & ddl_Program.SelectedValue & "' and campus = '" & Session("SchoolCampus") & "'"
            strRet = oCommon.getFieldValue(strSQL)

            If strRet = "Bahasa Antarabangsa" Then
                strSQL = "  select subject_ID, subject_info.subject_Name from subject_info left join coordinator on subject_info.course_Name = coordinator.course_Name 
                            where subject_sem ='" & semester & "' and subject_year = '" & ddl_year.SelectedValue & "' and coordinator.stf_ID = '" & data_ID & "'  and subject_Campus = '" & Session("SchoolCampus") & "' and course_Program = '" & ddl_Program.SelectedValue & "'
                            and subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Name = coordinator.subject_Name
                            and year = '" & ddl_year.SelectedValue & "' and coordinator_Level = '" & ddlLevel.SelectedValue & "' and program = '" & ddl_Program.SelectedValue & "' and campus = '" & Session("SchoolCampus") & "'"
            Else
                strSQL = "  select subject_ID, subject_info.subject_Name from subject_info left join coordinator on subject_info.course_Name = coordinator.course_Name 
                            where subject_sem ='" & semester & "' and subject_year = '" & ddl_year.SelectedValue & "' and coordinator.stf_ID = '" & data_ID & "'  and subject_Campus = '" & Session("SchoolCampus") & "' and course_Program = '" & ddl_Program.SelectedValue & "'
                            and subject_StudentYear = '" & ddlLevel.SelectedValue & "' and year = '" & ddl_year.SelectedValue & "' and coordinator_Level = '" & ddlLevel.SelectedValue & "' and program = '" & ddl_Program.SelectedValue & "' and campus = '" & Session("SchoolCampus") & "'"
            End If

            Dim sqlSub As New SqlDataAdapter(strSQL, objConn)

            Dim subds As DataSet = New DataSet
            sqlSub.Fill(subds, "SubjectTable")

            ddl_subject.DataSource = subds
            ddl_subject.DataTextField = "subject_Name"
            ddl_subject.DataValueField = "subject_ID"
            ddl_subject.DataBind()
            ddl_subject.Items.Insert(0, New ListItem("Select subject", String.Empty))

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_subject.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        If ddl_year.SelectedIndex > 0 Then

            If ddl_Program.SelectedIndex > 0 Then

                If ddl_exam.SelectedIndex > 0 Then

                    If ddlLevel.SelectedIndex > 0 Then

                        If ddl_subject.SelectedIndex > 0 Then

                            Dim find_examEndDate As String = "Select exam_EndDate from exam_info where exam_Year = '" & ddl_year.SelectedValue & "' and exam_Name = '" & ddl_exam.SelectedValue & "' and exam_Institutions = '" & Session("SchoolCampus") & "'"
                            Dim get_examEndDate As String = oCommon.getFieldValue(find_examEndDate)

                            Dim convertToDate_examEndDate As DateTime = DateTime.ParseExact(get_examEndDate, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
                            Dim formatted_examEndDate As String = convertToDate_examEndDate.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo)

                            Dim get_currentDate As String = DateTime.Now.ToString("yyyyMMdd")

                            If get_currentDate < formatted_examEndDate Then

                                For i As Integer = 0 To datRespondent.Rows.Count - 1

                                    Dim marks As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtmarks"), TextBox)
                                    Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

                                    If marks.Text.Length > 0 Then
                                        ''update marks
                                        strSQL = "UPDATE exam_result SET marks='" & marks.Text & "' WHERE ID ='" & strKeyID & "'"
                                        strRet = oCommon.ExecuteSQL(strSQL)

                                        ''select grades based on marks 
                                        Dim ResultGrades As String = ""
                                        ResultGrades = "select grade_Name from grade_info where grade_min_range <= '" & marks.Text & "' and grade_max_range >= '" & marks.Text & "'"
                                        Dim grades As String = oCommon.getFieldValue(ResultGrades)

                                        ''update grades and gpa
                                        strSQL = "UPDATE exam_result SET grade='" & grades & "' WHERE ID ='" & strKeyID & "'"
                                        strRet = oCommon.ExecuteSQL(strSQL)
                                    End If

                                Next

                                If strRet = 0 Then
                                    ShowMessage(" Update Student Result", MessageType.Success)
                                Else
                                    ShowMessage(" Unsuccessful Update Student Result", MessageType.Error)
                                End If

                            Else
                                ShowMessage(" Unable To Update Student Result After " & get_examEndDate, MessageType.Error)
                            End If

                        Else
                            ShowMessage(" Please Select Course", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Select Level", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Select Examination", MessageType.Error)
                End If
            Else
                ShowMessage(" Please Select Program", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Select Year", MessageType.Error)
        End If

        strRet = BindData(datRespondent)

    End Sub

    Protected Sub ddl_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            ddlLevel_info()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Program.SelectedIndexChanged
        Try
            ddlExam()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        Try
            ddlExam()
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