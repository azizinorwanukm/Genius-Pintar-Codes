Imports System.Data.SqlClient

Public Class student_ExamList
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
                Year()
                load_page()
                Exam()

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "select MAX(Parameter) as year from setting where type = 'year'"

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
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlyear.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub Year()

        Dim data_ID As String = Request.QueryString("std_ID")

        strSQL = "select distinct student_Level.year from student_Level
                  left join student_info on student_Level.std_ID=student_info.std_ID
                  where student_info.std_ID = '" + data_ID + "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "year"
            ddlyear.DataValueField = "year"
            ddlyear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Exam()

        Dim get_Check As String = "select Value from setting where Type  = 'Exam Result'"
        Dim data_Check As String = oCommon.getFieldValue(get_Check)

        Dim data_Exam As String = ""

        If data_Check = "off" Then

            ''show all student exam result except the current exam
            data_Exam = "  select max(exam_result.exam_ID) from exam_result
                        left join exam_info on exam_result.exam_ID = exam_Info.exam_ID
                        left join course on exam_result.course_ID = course.course_ID
                        where course.year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            strRet = oCommon.getFieldValue(data_Exam)

            strSQL = "select distinct exam_info.exam_Name from exam_result left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                  left join course on exam_result.course_ID = course.course_ID left join student_info on course.std_ID = student_info.std_ID
                  where student_info.student_Status = 'Access' and course.year = '" & ddlyear.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "' and exam_info.exam_ID < '" & strRet & "'
                  and student_info.std_ID = '" & Request.QueryString("std_ID") & "'"
        Else

            ''show all student exam result
            strSQL = "select distinct exam_info.exam_Name from exam_result left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                  left join course on exam_result.course_ID = course.course_ID left join student_info on course.std_ID = student_info.std_ID
                  where student_info.student_Status = 'Access' and course.year = '" & ddlyear.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                  and student_info.std_ID = '" & Request.QueryString("std_ID") & "'"
        End If



        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlexam.DataSource = ds
            ddlexam.DataTextField = "exam_Name"
            ddlexam.DataValueField = "exam_Name"
            ddlexam.DataBind()
            ddlexam.Items.Insert(0, New ListItem("Select Exam", String.Empty))
            ddlexam.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            Exam()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlexam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlexam.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
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

        Dim data_ID As String = Request.QueryString("std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_Name ASC"

        tmpSQL = "select exam_result.ID,subject_Name,subject_code,marks,grade,gpa from course
                      left join subject_info on course.subject_ID=subject_info.subject_ID
                      left join exam_result on course.course_ID=exam_result.course_ID
                      left join exam_Info on exam_result.exam_Id=exam_Info.exam_ID
                      left join grade_info on exam_result.grade=grade_info.grade_Name"
        strWhere = " where course.std_ID = '" + data_ID + "'"
        strWhere += " AND exam_Info.exam_Year = '" & ddlyear.SelectedValue & "'"
        strWhere += " AND exam_Info.exam_Name = '" & ddlexam.SelectedValue & "'"

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
End Class