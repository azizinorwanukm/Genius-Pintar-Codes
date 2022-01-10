Imports System.Data.SqlClient

Public Class student_update_course
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

                previousPage.NavigateUrl = String.Format("~/admin_pelajar_kepastian_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=VCourse")

                ddlYear()
                ddlLevel()
                ddlSem()
                student_Program_list()
                student_Campus_list()

                strRet = BindData(datRespondent)

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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_Name ASC"

        tmpSQL = "  Select distinct student_info.student_ID, student_info.std_ID,
                    student_info.student_Mykad,
                    student_info.student_Name,
                    student_Level.student_Level,
                    student_Level.student_Sem,
                    student_info.student_Stream,
                    student_info.student_Campus
                    From student_info 
                    left join student_Level on student_info.std_ID=student_Level.std_ID
                    left join course on student_info.std_ID= course.std_ID "
        strWhere = " WHERE student_info.std_ID Is Not NULL and student_info.student_status = 'Access' and student_info.student_Stream = '" & ddl_Program.SelectedValue & "' and student_info.student_Campus = '" & ddl_Campus.SelectedValue & "'"
        strWhere += " And course.year = '" & ddl_year.SelectedValue & "'"
        strWhere += " And student_level.year = '" & ddl_year.SelectedValue & "'"
        strWhere += " And student_Level.student_Level = '" & ddl_level.SelectedValue & "'"
        strWhere += " And student_Level.student_Sem = '" & ddl_sem.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL

    End Function

    Protected Sub ddl_sem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            subject_list()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddl_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Campus.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            student_Program_list()
            subject_list()

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ddl_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Program.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            subject_list()

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ddlSem()
        Try
            strSQL = "Select * from setting where Type = 'Sem'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_sem.DataSource = ds
            ddl_sem.DataTextField = "Parameter"
            ddl_sem.DataValueField = "Value"
            ddl_sem.DataBind()
            ddl_sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddl_sem.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlYear()
        Try
            Dim stryear As String = "Select Parameter from setting where Type = 'Year'"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddl_year.DataSource = yrds
            ddl_year.DataValueField = "Parameter"
            ddl_year.DataTextField = "Parameter"
            ddl_year.DataBind()
            ddl_year.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_Program_list()

        If ddl_Campus.SelectedValue = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Program.DataSource = ds
            ddl_Program.DataTextField = "Parameter"
            ddl_Program.DataValueField = "Value"
            ddl_Program.DataBind()
            ddl_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Campus_list()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Campus.DataSource = ds
            ddl_Campus.DataTextField = "Parameter"
            ddl_Campus.DataValueField = "Value"
            ddl_Campus.DataBind()
            ddl_Campus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub subject_list()
        Try
            Dim stryear As String = "Select subject_ID, subject_Name from subject_info where subject_StudentYear = '" & ddl_level.SelectedValue & "' and subject_sem = '" & ddl_sem.SelectedValue & "' and subject_year = '" & ddl_year.SelectedValue & "' and subject_Campus = '" & ddl_Campus.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "'"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddlCourseChoose.DataSource = yrds
            ddlCourseChoose.DataValueField = "subject_ID"
            ddlCourseChoose.DataTextField = "subject_Name"
            ddlCourseChoose.DataBind()
            ddlCourseChoose.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourseChoose.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub class_list_data()
        Try
            Dim check As String = "select subject_ID from class_info where subject_ID = '" & ddlCourseChoose.SelectedValue & "'"
            Dim data_check As String = oCommon.getFieldValue(check)

            If data_check = "" Then

                Dim stryear As String = "Select class_ID, class_Name from class_info where class_level = '" & ddl_level.SelectedValue & "' and class_type = 'Compulsory' and class_year = '" & ddl_year.SelectedValue & "' and class_Campus = '" & ddl_Campus.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "'"
                Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

                Dim yrds As DataSet = New DataSet
                sqlYearDA.Fill(yrds, "YrTable")

                ddlClassChose.DataSource = yrds
                ddlClassChose.DataValueField = "class_ID"
                ddlClassChose.DataTextField = "class_Name"
                ddlClassChose.DataBind()
                ddlClassChose.Items.Insert(0, New ListItem("Select Class", String.Empty))
                ddlClassChose.SelectedIndex = 0

            Else

                Dim stryear As String = "Select class_ID, class_Name from class_info where class_level = '" & ddl_level.SelectedValue & "' and subject_ID = '" & ddlCourseChoose.SelectedValue & "'  and class_year = '" & ddl_year.SelectedValue & "' and class_Campus = '" & ddl_Campus.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "'"
                Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

                Dim yrds As DataSet = New DataSet
                sqlYearDA.Fill(yrds, "YrTable")

                ddlClassChose.DataSource = yrds
                ddlClassChose.DataValueField = "class_ID"
                ddlClassChose.DataTextField = "class_Name"
                ddlClassChose.DataBind()
                ddlClassChose.Items.Insert(0, New ListItem("Select Class", String.Empty))
                ddlClassChose.SelectedIndex = 0

            End If

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
            ddl_level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_level.SelectedIndex = 0

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub ddl_level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged
        Try
            ddlSem()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlCourseChoose_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseChoose.SelectedIndexChanged
        Try
            class_list_data()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdateStudentCourse_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStudentCourse.ServerClick
        Dim i As Integer
        Dim errorCount As Integer = 0

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    'Insert
                    Using CLASSDATA As New SqlCommand("INSERT into course(std_ID,class_ID,subject_ID,year) 
                                                       values ('" & strKey & "','" & ddlClassChose.SelectedValue & "','" & ddlCourseChoose.SelectedValue & "','" & ddl_year.SelectedValue & "')", objConn)
                        objConn.Open()
                        Dim j = CLASSDATA.ExecuteNonQuery()
                        objConn.Close()

                        If j <> 0 Then
                            errorCount = 0
                        Else
                            errorCount = 1
                        End If
                    End Using

                End If
            End If
        Next

        If errorCount = 1 Then
            ShowMessage("Add Student course", MessageType.Success)
        ElseIf errorCount = 0 Then
            ShowMessage("Unsuccessful Add Student Course", MessageType.Error)
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class