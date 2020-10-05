Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class coordinator_assessment_remark
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ddlyear_info()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlyear_info()
        Try
            Dim year As String = "SELECT Parameter FROM setting where Type = 'Year'"
            Dim yearDA As New SqlDataAdapter(year, objConn)

            Dim years_DS As DataSet = New DataSet
            yearDA.Fill(years_DS, "YearTable")

            ddlyear.DataSource = years_DS
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataBind()

            ddlexam_info()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlLevel_info()
        Try
            Dim level_asas As String = ""
            Dim level_tahap As String = ""

            If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 4" Then
                level_asas = "Foundation 1"
                level_tahap = "Level 1"

            ElseIf ddlexam_Name.SelectedValue = "Exam 5" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 7" Then
                level_asas = "Foundation 2"
                level_tahap = "Level 2"

            ElseIf ddlexam_Name.SelectedValue = "Exam 8" Then
                level_asas = "Foundation 2"

            ElseIf ddlexam_Name.SelectedValue = "Exam 9" Or ddlexam_Name.SelectedValue = "Exam 10" Or ddlexam_Name.SelectedValue = "Exam 11" Or ddlexam_Name.SelectedValue = "Exam 12" Then
                level_asas = "Foundation 3"

            End If

            Dim exam As String = "select * from setting where Type = 'Level' and ( Parameter = '" & level_asas & "' or Parameter = '" & level_tahap & "' )"
            Dim examDA As New SqlDataAdapter(exam, objConn)

            Dim exam_DS As DataSet = New DataSet
            examDA.Fill(exam_DS, "ExamTable")

            ddlLevel.DataSource = exam_DS
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataTextField = "Value"
            ddlLevel.DataBind()

            ddlsubject_info()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlsubject_info()
        Try
            Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))
            Dim level_sem As String = ""

            If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 5" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 9" Or ddlexam_Name.SelectedValue = "Exam 10" Then
                level_sem = "Sem 1"

            ElseIf ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 11" Or ddlexam_Name.SelectedValue = "Exam 12" Then
                level_sem = "Sem 2"

            End If

            Dim exam As String = "select distinct subject_info.course_Name from subject_info
                                  left join coordinator on subject_info.course_Name = coordinator.course_Name
                                  where subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                  and coordinator.year = '" & ddlyear.SelectedValue & "'
                                  and coordinator.coordinator_Level = '" & ddlLevel.SelectedValue & "'
                                  and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "'
                                  and coordinator.stf_ID = '" & DATA_STAFFID & "'
                                  and subject_info.subject_sem = '" & level_sem & "'"
            Dim examDA As New SqlDataAdapter(exam, objConn)

            Dim exam_DS As DataSet = New DataSet
            examDA.Fill(exam_DS, "ExamTable")

            ddlSubject.DataSource = exam_DS
            ddlSubject.DataValueField = "course_Name"
            ddlSubject.DataTextField = "course_Name"
            ddlSubject.DataBind()

            strRet = BindData(datRespondent)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlexam_info()
        Try
            Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

            Dim find_exam As String = "select distinct class_Level from class_info where class_year = '" & ddlyear.SelectedValue & "' and stf_ID = '" & DATA_STAFFID & "'"
            Dim get_exam As String = oCommon.getFieldValue(find_exam)

            Dim exam As String = "select distinct exam_info.exam_ID, exam_info.exam_Name from exam_info
                                  left join exam_result on exam_info.exam_ID = exam_result.exam_ID
                                  left join course on exam_result.course_Id = course.course_ID
                                  left join class_info on course.class_ID = class_info.class_ID
                                  where class_info.class_year = '" & ddlyear.SelectedValue & "'
                                  and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                  and course.year = '" & ddlyear.SelectedValue & "'
                                  and class_info.class_level = '" & get_exam & "'
                                  order by exam_Name ASC"
            Dim examDA As New SqlDataAdapter(exam, objConn)

            Dim exam_DS As DataSet = New DataSet
            examDA.Fill(exam_DS, "ExamTable")

            ddlexam_Name.DataSource = exam_DS
            ddlexam_Name.DataValueField = "exam_Name"
            ddlexam_Name.DataTextField = "exam_Name"
            ddlexam_Name.DataBind()
            'ddlexam_Name.Items.Insert(0, New ListItem("Select Exam", String.Empty))
            ddlexam_Name.SelectedIndex = 0

            ddlLevel_info()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            ddlexam_info()
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim get_exam As String = "select exam_ID from exam_info where exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_year = '" & ddlyear.SelectedValue & "'"
        Dim data_exam As String = oCommon.getFieldValue(get_exam)

        Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by subject_info.subject_Name, student_info.student_Name ASC"

        tmpSQL = "select student_info.std_ID, student_info.student_Name, student_info.student_Mykad, exam_info.exam_Name, subject_info.subject_Name from student_info
                  left join course on student_info.std_ID = course.std_ID
                  left join subject_info on course.subject_ID = subject_info.subject_ID
                  left join exam_result on course.course_ID = exam_result.course_ID
                  left join exam_info on exam_result.exam_ID = exam_info.exam_ID"

        strWhere = " where course.year = '" & ddlyear.SelectedValue & "'"
        strWhere += " And exam_info.exam_Year = '" & ddlyear.SelectedValue & "'"
        strWhere += " And subject_info.subject_year = '" & ddlyear.SelectedValue & "'"
        strWhere += " And subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "'"
        strWhere += " And exam_info.exam_ID = '" & data_exam & "'"
        strWhere += " And subject_info.course_Name = '" & ddlSubject.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL

    End Function

    Protected Sub ddlexam_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlexam_Name.SelectedIndexChanged
        Try
            ddlLevel_info()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        Try
            ddlsubject_info()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Answer_Test.Style.Add("display", "block")

        Try
            Dim level_sem As String = ""

            If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 5" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 9" Or ddlexam_Name.SelectedValue = "Exam 10" Then
                level_sem = "Sem 1"

            ElseIf ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 11" Or ddlexam_Name.SelectedValue = "Exam 12" Then
                level_sem = "Sem 2"

            End If

            Dim get_student_Subject As String = "select subject_info.subject_Name from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                                 where course.year = '" & ddlyear.SelectedValue & "' and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                                 and course.std_ID = '" & strKeyName & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                                 and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
            Dim data_student_Subject As String = oCommon.getFieldValue(get_student_Subject)

            Dim get_student_Name As String = "select student_Name from student_info where std_ID = '" & strKeyName & "'"
            Dim data_student_Name As String = oCommon.getFieldValue(get_student_Name)

            Dim get_student_Mark As String = "select exam_result.marks from exam_result left join course on exam_result.course_ID = course.course_ID 
                                              left join subject_info on course.subject_ID = subject_info.subject_ID
                                              where course.year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKeyName & "'
                                              and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                              and subject_info.subject_Name = '" & data_student_Subject & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                              and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
            Dim data_student_Mark As String = oCommon.getFieldValue(get_student_Mark)

            Dim get_student_Grade As String = "select exam_result.grade from exam_result left join course on exam_result.course_ID = course.course_ID 
                                               left join subject_info on course.subject_ID = subject_info.subject_ID
                                               where course.year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKeyName & "' 
                                               and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                               And subject_info.subject_Name = '" & data_student_Subject & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                               and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
            Dim data_student_Grade As String = oCommon.getFieldValue(get_student_Grade)

            Dim get_student_Class As String = "select class_info.class_Name from class_info left join course on class_info.class_ID = course.class_ID
                                               where course.year = '" & ddlyear.SelectedValue & "' and class_info.class_year = '" & ddlyear.SelectedValue & "'
                                               and course.std_ID = '" & strKeyName & "' and class_info.class_type = 'Compulsory'"
            Dim data_student_Class As String = oCommon.getFieldValue(get_student_Class)

            Dim get_student_Mykad As String = "select student_Mykad from student_info where std_ID = '" & strKeyName & "' and student_Status = 'Access'"
            Dim data_student_Mykad As String = oCommon.getFieldValue(get_student_Mykad)


            std_Name.Text = data_student_Name
            std_Mark.Text = data_student_Mark
            std_Exam.Text = ddlexam_Name.SelectedValue
            std_Subject.Text = data_student_Subject
            std_Class.Text = data_student_Class
            std_Grade.Text = data_student_Grade
            std_Mykad.Text = data_student_Mykad

            remark_load_page()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub remark_load_page()
        Try

            Dim level_sem As String = ""

            If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 5" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 9" Or ddlexam_Name.SelectedValue = "Exam 10" Then
                level_sem = "Sem 1"

            ElseIf ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 11" Or ddlexam_Name.SelectedValue = "Exam 12" Then
                level_sem = "Sem 2"

            End If

            Dim get_std_ID As String = "select std_ID from student_info where student_Mykad = '" & std_Mykad.Text & "' and student_Status = 'Access'"
            Dim data_std_ID As String = oCommon.getFieldValue(get_std_ID)

            Dim get_exam_ID As String = "select exam_ID from exam_info where exam_year = '" & ddlyear.SelectedValue & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "'"
            Dim data_exam_ID As String = oCommon.getFieldValue(get_exam_ID)

            Dim get_subjectID As String = "select subject_info.subject_ID from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                           where course.year = '" & ddlyear.SelectedValue & "' and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                           And course.std_ID = '" & data_std_ID & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                           and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
            Dim data_subjectID As String = oCommon.getFieldValue(get_subjectID)

            strSQL = "select * from coordinator_remark where std_ID = '" & data_std_ID & "' and year = '" & ddlyear.SelectedValue & "' and exam_ID = '" & data_exam_ID & "' 
                      and subject_ID = '" & data_subjectID & "'"

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim dmonth As DataSet = New DataSet
            sqlDA.Fill(dmonth, "AnyTable")

            Dim nRowsMonth As Integer = 0
            Dim nCountMonth As Integer = 1
            Dim MyTableMonth As DataTable = New DataTable
            MyTableMonth = dmonth.Tables(0)
            If MyTableMonth.Rows.Count > 0 Then
                If Not IsDBNull(dmonth.Tables(0).Rows(0).Item("report_remark")) Then
                    txtLaporan.Value = dmonth.Tables(0).Rows(0).Item("report_remark")
                Else
                    txtLaporan.Value = ""
                End If

                If Not IsDBNull(dmonth.Tables(0).Rows(0).Item("intervensi_remark")) Then
                    txtIntervensi.Value = dmonth.Tables(0).Rows(0).Item("intervensi_remark")
                Else
                    txtIntervensi.Value = ""
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim level_sem As String = ""

        If std_Exam.Text = "Exam 1" Or std_Exam.Text = "Exam 2" Or std_Exam.Text = "Exam 5" Or std_Exam.Text = "Exam 6" Or std_Exam.Text = "Exam 9" Or std_Exam.Text = "Exam 10" Then
            level_sem = "Sem 1"

        ElseIf std_Exam.Text = "Exam 3" Or std_Exam.Text = "Exam 4" Or std_Exam.Text = "Exam 7" Or std_Exam.Text = "Exam 8" Or std_Exam.Text = "Exam 11" Or std_Exam.Text = "Exam 12" Then
            level_sem = "Sem 2"

        End If

        ''get student_ID
        Dim get_ID As String = "select std_ID from student_info where student_Mykad = '" & std_Mykad.Text & "' and student_Status = 'Access'"
        Dim data_ID As String = oCommon.getFieldValue(get_ID)

        ''get subject_ID
        Dim get_subjectID As String = "select subject_info.subject_ID from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                       where course.year = '" & ddlyear.SelectedValue & "' and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                       And course.std_ID = '" & data_ID & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                       and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
        Dim data_subjectID As String = oCommon.getFieldValue(get_subjectID)

        ''get exam_ID
        Dim get_examID As String = "select exam_ID from exam_info where exam_Name = '" & std_Exam.Text & "' and exam_year = '" & ddlyear.SelectedValue & "'"
        Dim data_exam_ID As String = oCommon.getFieldValue(get_examID)

        ''update remark
        strSQL = "UPDATE coordinator_remark SET report_remark ='" & txtLaporan.Value & "', intervensi_remark = '" & txtIntervensi.Value & "'
                  WHERE std_ID ='" & data_ID & "' and year = '" & ddlyear.SelectedValue & "' and subject_ID = '" & data_subjectID & "' and exam_ID = '" & data_exam_ID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub

    Private Sub BtnPrint_ServerClick(sender As Object, e As EventArgs) Handles BtnPrint.ServerClick

        Dim myDocument As New Document(PageSize.A4)

        Dim i As Integer = 0

        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=UlasanPelajarPermataPintar.pdf")
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

        myDocument.Open()

        Dim level_sem As String = ""

        If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 5" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 9" Or ddlexam_Name.SelectedValue = "Exam 10" Then
            level_sem = "Sem 1"

        ElseIf ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 11" Or ddlexam_Name.SelectedValue = "Exam 12" Then
            level_sem = "Sem 2"

        End If


        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then

                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    ''GET EXAM ID
                    Dim get_exam_ID As String = "select exam_ID from exam_info where exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_year = '" & ddlyear.SelectedValue & "'"
                    Dim data_exam_ID As String = oCommon.getFieldValue(get_exam_ID)

                    ''GET STUDENT NAME
                    Dim get_student_name As String = "select student_Name from student_info where std_ID ='" & strKey & "' and student_Status = 'Access'"
                    Dim data_student_name As String = oCommon.getFieldValue(get_student_name)

                    ''GET STUDENT CLASS
                    Dim get_student_Class As String = "select class_info.class_Name from class_info left join course on class_info.class_ID = course.class_ID
                                                       where course.year = '" & ddlyear.SelectedValue & "' and class_info.class_year = '" & ddlyear.SelectedValue & "'
                                                       and course.std_ID = '" & strKey & "' and class_info.class_type = 'Compulsory'"
                    Dim data_student_Class As String = oCommon.getFieldValue(get_student_Class)

                    ''GET STUDENT SUBJECT NAME
                    Dim get_student_Subject As String = "select subject_info.subject_Name from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                                         where course.year = '" & ddlyear.SelectedValue & "' and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                                         and course.std_ID = '" & strKey & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                                         and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
                    Dim data_student_Subject As String = oCommon.getFieldValue(get_student_Subject)

                    'GET STUDENT SUBJECT ID
                    Dim get_student_Subject_ID As String = "select subject_info.subject_ID from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                                         where course.year = '" & ddlyear.SelectedValue & "' and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                                         and course.std_ID = '" & strKey & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                                         and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
                    Dim data_student_Subject_ID As String = oCommon.getFieldValue(get_student_Subject_ID)

                    ''GET STUDENT MARK
                    Dim get_student_Mark As String = "select exam_result.marks from exam_result left join course on exam_result.course_ID = course.course_ID 
                                                      left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where course.year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "' 
                                                      and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                                      and subject_info.subject_Name = '" & data_student_Subject & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                                      and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
                    Dim data_student_Mark As String = oCommon.getFieldValue(get_student_Mark)

                    ''GET STUDENT GRADE
                    Dim get_student_Grade As String = "select exam_result.grade from exam_result left join course on exam_result.course_ID = course.course_ID 
                                                       left join subject_info on course.subject_ID = subject_info.subject_ID
                                                       where course.year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "' 
                                                       and subject_info.subject_year = '" & ddlyear.SelectedValue & "'
                                                       And subject_info.subject_Name = '" & data_student_Subject & "' and subject_info.course_Name = '" & ddlSubject.SelectedValue & "' 
                                                       and subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_info.subject_Sem = '" & level_sem & "'"
                    Dim data_student_Grade As String = oCommon.getFieldValue(get_student_Grade)


                    ''GET REPORT REMARK
                    Dim tmpsql_report_remark As String = "select report_remark from coordinator_remark
                                                          where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' and exam_ID = '" & data_exam_ID & "' 
                                                          and subject_ID = '" & data_student_Subject_ID & "'"
                    Dim sql_report_remark As String = oCommon.getFieldValue(tmpsql_report_remark)

                    ''GET INTERVENSI REMARK
                    Dim tmpsql_intervensi_remark As String = "select intervensi_remark from coordinator_remark
                                                              where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' and exam_ID = '" & data_exam_ID & "' 
                                                              and subject_ID = '" & data_student_Subject_ID & "'"
                    Dim sql_intervensi_remark As String = oCommon.getFieldValue(tmpsql_intervensi_remark)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    '' draw spacing
                    Dim imgdrawSpacing As String = Server.MapPath("~/img/empty_space.png")
                    Dim imgSpacing As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgdrawSpacing)
                    imgSpacing.Alignment = iTextSharp.text.Image.LEFT_ALIGN  'left
                    imgSpacing.Border = 0

                    ' drawa permata pintar image
                    Dim get_imgPP As String = Server.MapPath("~/img/permata_logo.png")
                    Dim data_imgPP As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(get_imgPP)
                    data_imgPP.ScalePercent(30)
                    data_imgPP.SetAbsolutePosition(212, 770)
                    myDocument.Add(data_imgPP)

                    '' drawa permata pintar image
                    Dim get_imgUKM As String = Server.MapPath("~/img/ukm.jpg")
                    Dim data_imgUKM As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(get_imgUKM)
                    data_imgUKM.ScalePercent(12.5)
                    data_imgUKM.SetAbsolutePosition(312, 770)
                    myDocument.Add(data_imgUKM)

                    '' spacing
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    '' BORANG PENILAIN PELAJAR Text
                    Dim myPara001 As New Paragraph("LAPORAN PRESTASI PEPERIKSAAN", FontFactory.GetFont("Arial", 10, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    myDocument.Add(myPara001)

                    '' KOLEJ PERMATApintar Text
                    Dim myPara002 As New Paragraph("KOLEJ PERMATApintar®", FontFactory.GetFont("Arial", 10, Font.BOLD))
                    myPara002.Alignment = Element.ALIGN_CENTER
                    myDocument.Add(myPara002)

                    '' spacing
                    myDocument.Add(imgSpacing)

                    '' create a table
                    Dim table As New PdfPTable(6)
                    table.WidthPercentage = 100
                    table.SetWidths({5, 20, 30, 20, 20, 5})

                    Dim cetak = Environment.NewLine & ""
                    Dim cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" NAMA PELAJAR", FontFactory.GetFont("Arial", 9, Font.BOLD)))
                    table.AddCell(cell)

                    Dim cell_collspan = New PdfPCell(New Paragraph(" " & data_student_name, FontFactory.GetFont("Arial", 9)))
                    cell_collspan.Colspan = 3
                    table.AddCell(cell_collspan)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" KELAS", FontFactory.GetFont("Arial", 9, Font.BOLD)))
                    table.AddCell(cell)

                    cell_collspan = New PdfPCell(New Paragraph(" " & data_student_Class, FontFactory.GetFont("Arial", 9)))
                    cell_collspan.Colspan = 3
                    table.AddCell(cell_collspan)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" SUBJEK", FontFactory.GetFont("Arial", 9, Font.BOLD)))
                    table.AddCell(cell)

                    cell_collspan = New PdfPCell(New Paragraph(" " & data_student_Subject, FontFactory.GetFont("Arial", 9)))
                    cell_collspan.Colspan = 3
                    table.AddCell(cell_collspan)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" MARKAH", FontFactory.GetFont("Arial", 9, Font.BOLD)))
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" " & data_student_Mark, FontFactory.GetFont("Arial", 9)))
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" GRED", FontFactory.GetFont("Arial", 9, Font.BOLD)))
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" " & data_student_Grade, FontFactory.GetFont("Arial", 9)))
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    table.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" PEPERIKSAAN", FontFactory.GetFont("Arial", 9, Font.BOLD)))
                    table.AddCell(cell)

                    cell_collspan = New PdfPCell(New Paragraph(" " & ddlexam_Name.SelectedValue, FontFactory.GetFont("Arial", 9)))
                    cell_collspan.Colspan = 3
                    table.AddCell(cell_collspan)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    '' spacing
                    myDocument.Add(imgSpacing)

                    '' create a table
                    Dim table1 As New PdfPTable(3)
                    table1.WidthPercentage = 100
                    table1.SetWidths({5, 90, 5})

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table1.AddCell(cell)

                    Dim myPara003 As New Paragraph("LAPORAN", FontFactory.GetFont("Arial", 9))
                    myPara003.Alignment = Element.ALIGN_CENTER

                    cell.AddElement(myPara003)
                    table1.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table1.AddCell(cell)
                    table1.AddCell(cell)

                    Dim cell_rowspan = New PdfPCell(New Paragraph(" " & sql_report_remark, FontFactory.GetFont("Arial", 9)))
                    cell_rowspan.Rowspan = 10
                    table1.AddCell(cell_rowspan)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)

                    Dim myPara004 As New Paragraph("Intervensi", FontFactory.GetFont("Arial", 9))
                    myPara004.Alignment = Element.ALIGN_CENTER

                    cell.AddElement(myPara004)
                    table1.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table1.AddCell(cell)
                    table1.AddCell(cell)

                    cell_rowspan = New PdfPCell(New Paragraph(" " & sql_intervensi_remark, FontFactory.GetFont("Arial", 9)))
                    cell_rowspan.Rowspan = 10
                    table1.AddCell(cell_rowspan)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)
                    table1.AddCell(cell)

                    myDocument.Add(table1)

                    '' spacing
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    '' create a table
                    Dim table7 As New PdfPTable(3)
                    table7.WidthPercentage = 100
                    table7.SetWidths({25, 15, 60})

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table7.AddCell(cell)

                    cell = New PdfPCell(New Paragraph("(Tandatangan)", FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table7.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table7.AddCell(cell)

                    myDocument.Add(table7)

                    '' spacing
                    myDocument.Add(imgSpacing)

                    '' create a table
                    Dim table8 As New PdfPTable(4)
                    table8.WidthPercentage = 100
                    table8.SetWidths({5, 60, 20, 5})

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" Nama : ", FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    Dim cell_rowspan_cop = New PdfPCell(New Paragraph("         Cop Rasmi Kolej ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
                    cell_rowspan_cop.Rowspan = 4
                    table8.AddCell(cell_rowspan_cop)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(" Tarikh : ", FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table8.AddCell(cell)

                    myDocument.Add(table8)

                    '' spacing
                    myDocument.NewPage()

                End If
            End If
        Next

        myDocument.Close()

        HttpContext.Current.Response.Write(myDocument)
        HttpContext.Current.Response.End()

    End Sub
End Class