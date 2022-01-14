Imports System.Data.SqlClient
Imports System.Globalization

Public Class student_marks1
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
                ddlLevel()
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

            ''check cand exam end date
            Dim examDate As String = "Select exam_EndDate from exam_Info where exam_Name = '" & ddl_exam.SelectedValue & "'"
            Dim data_ExamDate As DateTime = oCommon.getFieldValue(examDate)
            Dim reformatted As Integer = data_ExamDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)

            ''get current date
            Dim NOW_DATE As Integer = DateTime.Now.ToString("yyyyMMdd")

            If NOW_DATE > reformatted Then
                ''disabled
                Dim data_txt As TextBox = DirectCast(datRespondent.FindControl("txtmarks"), TextBox)
                data_txt.Enabled = False
            Else
                ''enabled
                Dim txtNumValues As TextBox = DirectCast(datRespondent.FindControl("txtmarks"), TextBox)
                txtNumValues.Enabled = True
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
            strWhere += " And course.subject_ID = '" & ddl_subject.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

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

        If Not ddl_level.SelectedValue = "-1" Then
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim STDLEVEL As New SqlDataAdapter()

            strSQL = "select class_ID,class_Name from class_info where class_Level ='" & ddl_level.SelectedValue & "' And class_year = '" & ddl_year.SelectedValue & "' and class_type = 'Compulsory'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "ClassTable")

            ddl_class.DataSource = ds
            ddl_class.DataTextField = "class_Name"
            ddl_class.DataValueField = "class_ID"
            ddl_class.DataBind()
            ddl_class.Items.Insert(0, New ListItem("-Select Class-", "-1"))

            strSQL = "select subject_ID, subject_code, subject_Name from subject_info where subject_StudentYear ='" & ddl_level.SelectedValue & "'"
            Dim sqlSub As New SqlDataAdapter(strSQL, objConn)

            Dim subds As DataSet = New DataSet
            sqlSub.Fill(subds, "SubjectTable")

            ddl_subject.DataSource = subds
            ddl_subject.DataTextField = "subject_Name"
            ddl_subject.DataValueField = "subject_ID"
            ddl_subject.DataBind()
            ddl_subject.Items.Insert(0, New ListItem("-Select subject-", "-1"))

            ddl_class.Enabled = True
            ddl_subject.Enabled = True

            strRet = BindData(datRespondent)

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
            ddl_year.Items.Insert(0, New ListItem(Year(Now), Year(Now)))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevel()

        Try
            Dim strLevelSql As String = "Select Parameter,idx from setting where Type = 'Level' order by idx ASC"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_level.DataSource = levds
            ddl_level.DataValueField = "Parameter"
            ddl_level.DataTextField = "Parameter"
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
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlExam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_exam.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_class.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

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

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim marks As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtmarks"), TextBox)
            Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

            ''update marks
            strSQL = "UPDATE exam_result SET marks='" & marks.Text & "' WHERE ID ='" & strKeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            ''select grades based on marks 
            Dim ResultGrades As String = ""
            ResultGrades = "select grade_Name from grade_info where grade_min_range <= '" & marks.Text & "' and grade_max_range >= '" & marks.Text & "'"
            Dim grades As String = getFieldValue(ResultGrades, strConn)

            ''update grades and gpa
            strSQL = "UPDATE exam_result SET grade='" & grades & "' WHERE ID ='" & strKeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        Next

        strRet = BindData(datRespondent)

    End Sub

End Class