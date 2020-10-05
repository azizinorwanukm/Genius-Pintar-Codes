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

                ''Generate_Table()
                ddl_class.Enabled = False
                ddl_subject.Enabled = False

                ddlYear()

                ddlExam()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter from setting where Type ='Exam' and Parameter = 'Exam 1'"

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
                ddl_exam.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_exam.SelectedValue = ""
            End If
        End If
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
                nodatamessage.Visible = True
            Else
                nodatamessage.Visible = False
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
        Dim strOrderby As String = "order by student_info.student_Name ASC"

        tmpSQL = "select distinct exam_result.ID, exam_result.course_ID, student_info.student_ID, student_info.student_Name, class_info.class_Name, exam_Info.exam_Name, subject_info.subject_Name, exam_result.marks, exam_result.grade
                  From exam_result Join course On exam_result.course_ID = course.course_ID
                  Left Join exam_info On exam_result.exam_ID = exam_Info.exam_ID
                  Left Join student_info On course.std_ID = student_info.std_ID
                  Left Join class_info On course.class_ID = class_info.class_ID
                  Left Join subject_info On course.subject_ID = subject_info.subject_ID left Join student_Png On student_info.std_ID=student_Png.std_ID
                  Where exam_result.ID Is Not null"

        If ddl_year.SelectedIndex > 0 Then
            strWhere += " And exam_Info.exam_Year = '" & ddl_year.SelectedValue & "'"
        Else
            strWhere += " And exam_Info.exam_Year = '" & Now.Year & "'"
        End If

        If ddl_exam.SelectedIndex > 0 Then
            strWhere += " And exam_Info.exam_Name = '" & ddl_exam.SelectedValue & "'"
        End If

        If ddl_class.SelectedIndex > 0 Then
            strWhere += " And course.class_ID = '" & ddl_class.SelectedValue & "'"
        End If

        If ddl_subject.SelectedIndex > 0 Then
            strWhere += " And subject_info.subject_Name = '" & ddl_subject.SelectedValue & "'"
        End If

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

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged

        Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        If Not ddl_level.SelectedValue = "-1" Then
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim STDLEVEL As New SqlDataAdapter()

            strSQL = "select distinct lecturer.class_ID,class_info.class_Name from class_info
                      left join lecturer on class_info.class_ID = lecturer.class_ID
                      where class_Level ='" & ddl_level.SelectedValue & "' And class_year = '" & ddl_year.SelectedValue & "' And lecturer.stf_ID = '" & DATA_STAFFID & "'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "ClassTable")

            ddl_class.DataSource = ds
            ddl_class.DataTextField = "class_Name"
            ddl_class.DataValueField = "class_ID"
            ddl_class.DataBind()
            ddl_class.Items.Insert(0, New ListItem("-Select Class-", "-1"))

            strSQL = "select distinct subject_info.subject_Name from subject_info
                      left join lecturer on subject_info.subject_ID = lecturer.subject_ID
                      where subject_StudentYear ='" & ddl_level.SelectedValue & "'
                      and subject_info.subject_year = '" & ddl_year.SelectedValue & "' and lecturer.stf_ID = '" & DATA_STAFFID & "'"
            Dim sqlSub As New SqlDataAdapter(strSQL, objConn)

            Dim subds As DataSet = New DataSet
            sqlSub.Fill(subds, "SubjectTable")

            ddl_subject.DataSource = subds
            ddl_subject.DataTextField = "subject_Name"
            ddl_subject.DataValueField = "subject_Name"
            ddl_subject.DataBind()
            ddl_subject.Items.Insert(0, New ListItem("-Select subject-", "-1"))

            ddl_class.Enabled = True
            ddl_subject.Enabled = True

        Else
            ddl_class.Items.Clear()
            ddl_subject.Items.Clear()
            ddl_class.Enabled = False
            ddl_subject.Enabled = False

        End If

    End Sub

    Protected Sub ddlYear()
        Try
            Dim stryear As String = "Select distinct exam_Year from exam_Info"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddl_year.DataSource = yrds
            ddl_year.DataValueField = "exam_Year"
            ddl_year.DataTextField = "exam_Year"
            ddl_year.DataBind()
            ddl_year.Items.Insert(0, New ListItem("Select Year", 0))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevel()
        Try
            Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

            Dim strLevelSql As String = ""

            If ddl_exam.SelectedValue = "Exam 1" Or ddl_exam.SelectedValue = "Exam 2" Then

                strLevelSql = "select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.class_Level = 'Foundation 1' or  class_info.class_Level = 'Level 1' "

            ElseIf ddl_exam.SelectedValue = "Exam 3" Or ddl_exam.SelectedValue = "Exam 4" Then
                strLevelSql = "select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.class_Level = 'Foundation 1' or  class_info.class_Level = 'Level 1' "

            ElseIf ddl_exam.SelectedValue = "Exam 5" Or ddl_exam.SelectedValue = "Exam 6" Then
                strLevelSql = "select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.class_Level = 'Foundation 2' or  class_info.class_Level = 'Level 2' "

            ElseIf ddl_exam.SelectedValue = "Exam 7" Or ddl_exam.SelectedValue = "Exam 8" Then
                strLevelSql = "select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.class_Level = 'Foundation 2' or  class_info.class_Level = 'Level 2' "

            ElseIf ddl_exam.SelectedValue = "Exam 9" Or ddl_exam.SelectedValue = "Exam 10" Then
                strLevelSql = "select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.class_Level = 'Foundation 3'"

            ElseIf ddl_exam.SelectedValue = "Exam 11" Or ddl_exam.SelectedValue = "Exam 12" Then
                strLevelSql = "select distinct class_info.class_Level from class_info
                                left join lecturer on class_info.class_ID = lecturer.class_ID
                                where lecturer.stf_ID = '" & DATA_STAFFID & "' and class_info.class_Level = 'Foundation 3'"


            End If


            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_level.DataSource = levds
            ddl_level.DataValueField = "class_Level"
            ddl_level.DataTextField = "class_Level"
            ddl_level.DataBind()
            ddl_level.Items.Insert(0, New ListItem("-Select Foundation/Level-", "-1"))
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlExam()

        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Exam'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "ExamTable")

            ddl_exam.DataSource = levds
            ddl_exam.DataValueField = "Parameter"
            ddl_exam.DataTextField = "Parameter"
            ddl_exam.DataBind()
            ddl_exam.DataBind()
            ddl_exam.Items.Insert(0, New ListItem("Select Exam", 0))
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
        Dim errorCount As Integer = 0

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim marks As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtmarks"), TextBox)
            Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

            ''update marks
            strSQL = "UPDATE exam_result SET marks='" & marks.Text & "' WHERE ID ='" & strKeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Dim ResultGrades As String = ""

            If marks.Text = "100" Or marks.Text = "100.00" Or marks.Text = "100.0" Then
                ''select grades based on marks 

                ResultGrades = "select grade_Name from grade_info where grade_min_range <= " & marks.Text & " and grade_max_range >= " & marks.Text & ""

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

        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddl_exam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_exam.SelectedIndexChanged
        ddlLevel()
    End Sub
End Class