Imports System.Data.SqlClient

Public Class Import_Export_student_marks
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                exam_info()
                year_list()
                subject_info()
                load_page()
                'strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub exam_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Exam' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlexam_Name.DataSource = ds
            ddlexam_Name.DataTextField = "Parameter"
            ddlexam_Name.DataValueField = "Parameter"
            ddlexam_Name.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT distinct year from student_Level where year ='" & Now.Year & "'"

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

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        strSQL = "SELECT exam_Name from exam_Info where exam_Name = 'Exam 1' and exam_Year = '" & Now.Year & "'"

        Dim strConnExam As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConnExam As SqlConnection = New SqlConnection(strConnExam)
        Dim sqlDAExam As New SqlDataAdapter(strSQL, objConnExam)

        Dim dsExam As DataSet = New DataSet
        sqlDAExam.Fill(dsExam, "AnyTable")

        Dim nRowsExam As Integer = 0
        Dim nCountExam As Integer = 1
        Dim MyTableExam As DataTable = New DataTable
        MyTableExam = dsExam.Tables(0)
        If MyTableExam.Rows.Count > 0 Then
            If Not IsDBNull(dsExam.Tables(0).Rows(0).Item("exam_Name")) Then
                ddlexam_Name.SelectedValue = dsExam.Tables(0).Rows(0).Item("exam_Name")
            Else
                ddlexam_Name.SelectedValue = ""
            End If
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'strSQL = "SELECT student_Level from student_level where student_Level ='Foundation 1'"

        'Dim strConnLevel As String = ConfigurationManager.AppSettings("ConnectionString")
        'Dim objConnLevel As SqlConnection = New SqlConnection(strConnLevel)
        'Dim sqlDALevel As New SqlDataAdapter(strSQL, objConnLevel)

        'Dim dsLevel As DataSet = New DataSet
        'sqlDALevel.Fill(dsLevel, "AnyTable")

        'Dim nRowsLevel As Integer = 0
        'Dim nCountLevel As Integer = 1
        'Dim MyTableLevel As DataTable = New DataTable
        'MyTableLevel = dsLevel.Tables(0)
        'If MyTableLevel.Rows.Count > 0 Then
        '    If Not IsDBNull(dsLevel.Tables(0).Rows(0).Item("student_Level")) Then
        '        ddlstudent_Year.SelectedValue = dsLevel.Tables(0).Rows(0).Item("student_Level")
        '    Else
        '        ddlstudent_Year.SelectedValue = ""
        '    End If
        'End If

    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub subject_info()
        strSQL = "SELECT distinct course_Name FROM subject_info WHERE subject_year = '" & Now.Year & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsubject_Name.DataSource = ds
            ddlsubject_Name.DataTextField = "course_Name"
            ddlsubject_Name.DataValueField = "course_Name"
            ddlsubject_Name.DataBind()
            ddlsubject_Name.Items.Insert(0, New ListItem("Select Courses", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnExport_ServerClick(sender As Object, e As EventArgs) Handles BtnExport.ServerClick
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
        Response.AddHeader("content-disposition", "attachment;filename=studentMarks.csv")
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by class_info.class_Name, student_info.student_Name ASC"

        tmpSQL = "select distinct course.course_ID, student_info.student_Name, student_info.student_Mykad, exam_Info.exam_Name, subject_info.subject_Name,class_info.class_Name,exam_result.marks
                  From exam_result 
                  Left Join course On exam_result.course_ID = course.course_ID
                  Left Join exam_info On exam_result.exam_ID = exam_Info.exam_ID
                  Left Join student_info On course.std_ID = student_info.std_ID
                  Left Join class_info On course.class_ID = class_info.class_ID
                  Left Join subject_info On course.subject_ID = subject_info.subject_ID
                  Where exam_result.ID Is Not null"

        strWhere += " And exam_Info.exam_Year = '" & ddlyear.SelectedValue & "'"
        strWhere += " And exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "'"
        strWhere += " And subject_info.course_Name = '" & ddlsubject_Name.SelectedValue & "'"


        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

End Class