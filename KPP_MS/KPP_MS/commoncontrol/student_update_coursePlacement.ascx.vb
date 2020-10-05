Imports System.Data.SqlClient

Public Class student_update_coursePlacement
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
                Dim id As String = ""
                ''id = Session("pelajar")
                id = Request.QueryString("std_ID")

                courseLoad(id)

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String
        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
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

    Private Sub courseLoad(ByVal access As String)

        strSQL = "select subject_info.subject_Name,subject_info.subject_ID
                  from subject_info
                  left join student_level on subject_info.subject_StudentYear = student_level.student_level
                  where student_level.std_ID = '" & access & "' and subject_info.subject_type != 'Compulsory' and subject_info.subject_sem = student_level.student_Sem and subject_info.subject_year = '" & Now.Year & "'"

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_courseName.DataSource = ds
            ddl_courseName.DataTextField = "subject_Name"
            ddl_courseName.DataValueField = "subject_ID"
            ddl_courseName.DataBind()
            ddl_courseName.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddl_courseName.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
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

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_ID ASC"

        tmpSQL = "Select * From course 
                  left join subject_info on course.subject_ID=subject_info.subject_ID 
                  left join class_info on course.class_ID=class_info.class_ID "
        strWhere = " WHERE course.std_ID = '" & Request.QueryString("std_ID") & "'"

        If ddl_courseName.SelectedIndex > 0 Then
            strWhere += " AND course.year = '" & ddl_courseName.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere
        ''--debug
        Return getSQL
    End Function

    Private Sub btnAdd_ServerClick(sender As Object, e As EventArgs) Handles btnAdd.ServerClick

        Dim errorCount As Integer = 0
        Dim id As String = ""
        ''id = Session("pelajar")
        id = Request.QueryString("std_ID")

        ''get student id
        Dim userID As String = ""
        userID = "select std_ID from student_info where std_ID = '" & id & "'"
        Dim stdID As String = getFieldValue(userID, strConn)

        ''count number of student who take that course
        Dim countID As String = ""
        countID = "select count(course.subject_ID) from course 
                    left join subject_info on course.subject_ID = subject_info.subject_ID 
                    where subject_info.subject_ID = '" & ddl_courseName.SelectedValue & "' and course.year = '" & Now.Year & "'"
        Dim countStdID As String = getFieldValue(countID, strConn)

        ''get max number student can take that course
        Dim maxID As String = ""
        maxID = "select std_number from subject_info where subject_ID = '" & ddl_courseName.SelectedValue & "'"
        Dim stdMaxID As String = getFieldValue(maxID, strConn)


        If countStdID <> stdMaxID Then

            ''get subject type
            Dim subjectType As String = ""
            subjectType = "select subject_type from subject_info where subject_ID = '" & ddl_courseName.SelectedValue & "'"
            Dim getSubjectType As String = getFieldValue(subjectType, strConn)

            ''check subject exist in course
            Dim subjectExist As String = ""
            subjectExist = "select subject_info.subject_type from course
                            left join subject_info on course.subject_ID=subject_info.subject_ID
                            where subject_info.subject_type = '" & getSubjectType & "' and course.year = '" & Now.Year & "' and course.std_ID = '" & stdID & "'"
            Dim getSubjectExist As String = getFieldValue(subjectExist, strConn)

            If getSubjectExist = "" Then

                ''insert to database
                Using STDDATA As New SqlCommand("INSERT INTO course(std_ID,subject_ID,class_ID,year) values 
                                                           ('" & id & "','" & ddl_courseName.SelectedValue & "','" & ddl_className.SelectedValue & "',
                                                            '" & Now.Year & "')", objConn)
                    objConn.Open()
                    Dim i = STDDATA.ExecuteNonQuery()
                    objConn.Close()
                    If i <> 0 Then
                        ''success execute
                        errorCount = 0
                    Else
                        ''error execute
                        errorCount = 1
                    End If
                End Using
            Else
                ''error (course already registered) 
                errorCount = 2
            End If
        Else
            ''error (course already full)
            errorCount = 3
        End If


        If errorCount = 0 Then
            Response.Redirect("pelajar_pilih_kursus.aspx?result=1&std_ID=" + id)
        ElseIf errorCount = 1 Then
            Response.Redirect("pelajar_pilih_kursus.aspx?result=-1&std_ID=" + id)
        ElseIf errorCount = 2 Then
            Response.Redirect("pelajar_pilih_kursus.aspx?result=2&std_ID=" + id)
        ElseIf errorCount = 3 Then
            Response.Redirect("pelajar_pilih_kursus.aspx?result=3&std_ID=" + id)
        End If

    End Sub


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete course where course_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub ddl_className_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_className.SelectedIndexChanged
        Try

            ''count number of student who take that course
            Dim countID As String = ""
            countID = "select count(course.subject_ID) from course
                       left join class_info on course.class_ID = class_info.class_ID
                       left join subject_info on class_info.subject_ID = subject_info.subject_ID
                       where subject_info.subject_ID = '" & ddl_courseName.SelectedValue & "' and course.year = '" & Now.Year & "' and course.class_ID = '" & ddl_className.SelectedValue & "'"
            Dim countStdID As String = getFieldValue(countID, strConn)

            ''get max number student can take that course
            Dim maxID As String = ""
            maxID = "select std_number from subject_info
                     where subject_ID = '" & ddl_courseName.SelectedValue & "'"
            Dim stdMaxID As String = getFieldValue(maxID, strConn)

            Dim answer As String = countStdID & "/" & stdMaxID

            count_student.Text = answer

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_courseName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_courseName.SelectedIndexChanged
        strSQL = "select class_Name,class_ID from class_info
                  where class_Type = 'Electives' and class_year = '" & Now.Year & "' and subject_ID = '" & ddl_courseName.SelectedValue & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_className.DataSource = ds
            ddl_className.DataTextField = "class_Name"
            ddl_className.DataValueField = "class_ID"
            ddl_className.DataBind()
            ddl_className.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddl_className.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub
End Class