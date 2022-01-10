Imports System.Data.SqlClient

Public Class course_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim data As String = oCommon.securityLogin(Request.QueryString("admin_ID"))

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    Dim getStatus As String = Request.QueryString("status")

                    If getStatus = "CC" Then ''create course
                        txtbreadcrum1.Text = "Create Course"
                        CourseCreate.Visible = True
                        CourseTransfer.Visible = False

                        btnCourseCreate.Attributes("class") = "btn btn-info"
                        btnCourseTransfer.Attributes("class") = "btn btn-default font"

                        year_Load()
                        subject_type_Load()
                        subject_sem_Load()
                        subject_StudentYear_Load()
                        subject_religions_Load()
                        subject_group_Load()

                        Load_Page()

                    ElseIf getStatus = "TC" Then ''transfer course
                        txtbreadcrum1.Text = "Transfer Course"
                        CourseCreate.Visible = False
                        CourseTransfer.Visible = True

                        btnCourseCreate.Attributes("class") = "btn btn-default font"
                        btnCourseTransfer.Attributes("class") = "btn btn-info"

                        yearLoad_List()
                        student_yearload_List()
                        student_semload_List()
                        subject_typeLoad_List()

                        Page_Load()

                        strRet = BindData(datRespondent)

                    End If

                    previousPage.NavigateUrl = String.Format("~/admin_pengurusan_am_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCourseCreate_ServerClick(sender As Object, e As EventArgs) Handles btnCourseCreate.ServerClick
        Response.Redirect("admin_daftar_kursus_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=CC")
    End Sub

    Private Sub btnCourseTransfer_ServerClick(sender As Object, e As EventArgs) Handles btnCourseTransfer.ServerClick
        Response.Redirect("admin_daftar_kursus_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=TC")
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Dim errorCount As Integer = 0

        If subject_Name.Text <> "" And Regex.IsMatch(subject_Name.Text, "^[A-Za-z0-9 ]+$") Then

            If subject_code.Text <> "" And Regex.IsMatch(subject_code.Text, "^[A-Za-z0-9]+$") Then

                If ddlsubject_year.SelectedValue <> "" Then

                    If subject_type.SelectedValue <> "" And subject_type.SelectedValue = "Compulsory" Or subject_type.SelectedValue = "Electives" Or subject_type.SelectedValue = "Choose" Then

                        If subject_StudentYear.SelectedValue <> "" And subject_StudentYear.SelectedValue = "Foundation 1" Or subject_StudentYear.SelectedValue = "Foundation 2" Or subject_StudentYear.SelectedValue = "Foundation 3" Or subject_StudentYear.SelectedValue = "Level 1" Or subject_StudentYear.SelectedValue = "Level 2" Then

                            If subject_sem.SelectedValue <> "" Then

                                If ddl_subjectreligions.SelectedValue <> "" Then

                                    If ddlCourse_group.SelectedValue <> "" Then

                                        Using STDDATA As New SqlCommand("INSERT INTO subject_info(subject_Name,subject_NameBM,subject_code,subject_year,subject_type,subject_StudentYear,subject_sem,subject_religions,course_Name,subject_CreditHour) 
                                                                        values ('" & subject_Name.Text & "','" & subject_NameBM.Text & "','" & subject_code.Text & "','" & ddlsubject_year.SelectedValue & "','" & subject_type.SelectedValue & "',
                                                                        '" & subject_StudentYear.SelectedValue & "','" & subject_sem.SelectedValue & "','" & ddl_subjectreligions.SelectedValue & "','" & ddlCourse_group.SelectedValue & "','" & subject_CreditHour.Text & "')", objConn)
                                            objConn.Open()
                                            Dim i = STDDATA.ExecuteNonQuery()
                                            objConn.Close()

                                            If i <> 0 Then
                                                ShowMessage("Successful Add New Course", MessageType.Success)
                                            Else
                                                ShowMessage("Unsuccessful Add New Course", MessageType.Error)
                                            End If
                                        End Using

                                    Else
                                        ''Error Course Group (please select course group)
                                        ShowMessage("Please select course group", MessageType.Error)
                                    End If

                                Else
                                    ''Error Religions (please select religions)
                                    ShowMessage("Please select religions", MessageType.Error)
                                End If

                            Else
                                ''Error Semester (please select semester)
                                ShowMessage("Please select semester", MessageType.Error)
                            End If
                        Else
                            ShowMessage("Please select level", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please select subject typep", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please select year", MessageType.Error)
                End If
            Else
                ShowMessage("Please fill in subject name in (BM) correctly", MessageType.Error)
            End If
        Else
            ShowMessage("Please fill in subject name in (BI) correctly", MessageType.Error)
        End If

    End Sub

    Private Sub Load_Page()
        ''student_info
        strSQL = "select * from setting where Type = 'Year' and Value = '" & Now.Year & "'"

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
                ddlsubject_year.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlsubject_year.SelectedValue = ""
            End If
        End If

    End Sub

    Private Sub subject_religions_Load()
        Try

            Dim strLevelSql As String = "Select subject_id from subject_info where subject_id is null"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_subjectreligions.DataSource = levds
            ddl_subjectreligions.DataValueField = "subject_id"
            ddl_subjectreligions.DataTextField = "subject_id"
            ddl_subjectreligions.DataBind()
            ddl_subjectreligions.Items.Insert(0, New ListItem("Select Course Religion", String.Empty))
            ddl_subjectreligions.Items.Insert(1, New ListItem("ALL", "ALL"))
            ddl_subjectreligions.Items.Insert(2, New ListItem("ISLAM", "ISLAM"))
            ddl_subjectreligions.Items.Insert(3, New ListItem("OTHERS", "OTHERS"))
            ddl_subjectreligions.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subject_type_Load()
        strSQL = "select * from setting where Type = 'Subject Type'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        subject_type.DataSource = ds
        subject_type.DataTextField = "Parameter"
        subject_type.DataValueField = "Parameter"
        subject_type.DataBind()
        subject_type.Items.Insert(0, New ListItem("Select Course Type", String.Empty))
        subject_type.SelectedIndex = 0
    End Sub

    Private Sub subject_StudentYear_Load()
        strSQL = "select * from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        subject_StudentYear.DataSource = ds
        subject_StudentYear.DataTextField = "Parameter"
        subject_StudentYear.DataValueField = "Parameter"
        subject_StudentYear.DataBind()
        subject_StudentYear.Items.Insert(0, New ListItem("Select Level", String.Empty))
        subject_StudentYear.SelectedIndex = 0

    End Sub

    Private Sub subject_sem_Load()
        strSQL = "select * from setting where Type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        subject_sem.DataSource = ds
        subject_sem.DataTextField = "Parameter"
        subject_sem.DataValueField = "Value"
        subject_sem.DataBind()
        subject_sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        subject_sem.SelectedIndex = 0

    End Sub

    Private Sub subject_group_Load()

        strSQL = "select * from setting where idx = 'Courses Group'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        ddlCourse_group.DataSource = ds
        ddlCourse_group.DataTextField = "Parameter"
        ddlCourse_group.DataValueField = "Parameter"
        ddlCourse_group.DataBind()
        ddlCourse_group.Items.Insert(0, New ListItem("Select Course Group", "NULL"))
        ddlCourse_group.SelectedIndex = 0

    End Sub

    Private Sub year_Load()

        strSQL = "select * from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        ddlsubject_year.DataSource = ds
        ddlsubject_year.DataTextField = "Parameter"
        ddlsubject_year.DataValueField = "Parameter"
        ddlsubject_year.DataBind()
        ddlsubject_year.Items.Insert(0, New ListItem("Select Year", "NULL"))
        ddlsubject_year.SelectedIndex = 0

    End Sub

    Private Sub yearLoad_List()
        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Year'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Year.DataSource = levds
            ddl_Year.DataValueField = "Parameter"
            ddl_Year.DataTextField = "Parameter"
            ddl_Year.DataBind()
            ddl_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_Year.SelectedIndex = 0

            ddlyear_Transfer.DataSource = levds
            ddlyear_Transfer.DataValueField = "Parameter"
            ddlyear_Transfer.DataTextField = "Parameter"
            ddlyear_Transfer.DataBind()
            ddlyear_Transfer.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlyear_Transfer.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_yearload_List()
        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Level.DataSource = levds
            ddl_Level.DataValueField = "Parameter"
            ddl_Level.DataTextField = "Parameter"
            ddl_Level.DataBind()
            ddl_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_Level.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_semload_List()
        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Sem'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Sem.DataSource = levds
            ddl_Sem.DataValueField = "Value"
            ddl_Sem.DataTextField = "Parameter"
            ddl_Sem.DataBind()
            ddl_Sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddl_Sem.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subject_typeLoad_List()
        Try
            Dim strLevelSql As String = "select * from setting where Type = 'Subject Type'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_type.DataSource = levds
            ddl_type.DataValueField = "Parameter"
            ddl_type.DataTextField = "Parameter"
            ddl_type.DataBind()
            ddl_type.Items.Insert(0, New ListItem("Select Course Type", String.Empty))
            ddl_type.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Load()
        strSQL = "select * from setting where Type = 'Year' and Value = '" & Now.Year & "'"

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
                ddl_Year.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_Year.SelectedValue = ""
            End If
        End If
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_StudentYear ASC"

        tmpSQL = "Select * From subject_info"
        strWhere += " WHERE subject_ID IS NOT NULL AND subject_Name is not null"

        If ddl_Sem.SelectedIndex > 0 Then
            strWhere += " AND subject_sem = '" & ddl_Sem.SelectedValue & "'"
        End If

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " AND subject_StudentYear = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_type.SelectedIndex > 0 Then
            strWhere += " AND subject_type = '" & ddl_type.SelectedValue & "'"
        End If

        If ddl_Year.SelectedIndex > 0 Then
            strWhere += " AND subject_year = '" & ddl_Year.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Private Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddl_Sem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddl_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_type.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnTransferCourse_ServerClick(sender As Object, e As EventArgs) Handles btnTransferCourse.ServerClick

        Dim i As Integer
        Dim errorCount As Integer = 0

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim get_code As String = "select subject_code from subject_info where subject_ID = '" & strKey & "'"
                    Dim check_code As String = oCommon.getFieldValue(get_code)

                    Dim find_subjectID As String = "select subject_ID from subject_info where subject_code = '" & check_code & "' and subject_year = '" & ddlyear_Transfer.SelectedValue & "'"
                    Dim check_subjectID As String = oCommon.getFieldValue(find_subjectID)

                    Dim find_subjectName As String = "select subject_Name from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectName As String = oCommon.getFieldValue(find_subjectName)
                    Dim find_subjectNameBM As String = "select subject_NameBM from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectNameBM As String = oCommon.getFieldValue(find_subjectNameBM)

                    Dim find_subjectReligions As String = "select subject_religions from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectReligions As String = oCommon.getFieldValue(find_subjectReligions)
                    Dim find_subjectLevel As String = "select subject_StudentYear from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectLevel As String = oCommon.getFieldValue(find_subjectLevel)
                    Dim find_subjectHour As String = "select subject_CreditHour from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectHour As String = oCommon.getFieldValue(find_subjectHour)
                    Dim find_subjectSem As String = "select subject_sem from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectSem As String = oCommon.getFieldValue(find_subjectSem)
                    Dim find_subjectCourseName As String = "select course_Name from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectCourseName As String = oCommon.getFieldValue(find_subjectCourseName)
                    Dim find_subjectType As String = "select subject_type from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectType As String = oCommon.getFieldValue(find_subjectType)

                    If check_subjectID.Length = 0 Then

                        If get_subjectType <> "Choose" Then

                            Using PJGDATA As New SqlCommand("   INSERT INTO subject_info(subject_Name, subject_NameBM, subject_code, subject_year, subject_type, subject_religions, subject_StudentYear, subject_CreditHour, subject_sem, course_Name)
                                                                Values('" & get_subjectName & "','" & get_subjectNameBM & "','" & check_code & "','" & ddlyear_Transfer.SelectedValue & "','" & get_subjectType & "','" & get_subjectReligions & "',
                                                                '" & get_subjectLevel & "','" & Integer.Parse(get_subjectHour) & "','" & get_subjectSem & "','" & get_subjectCourseName & "')", objConn)
                                objConn.Open()
                                Dim j = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                                If j <> 0 Then
                                    errorCount = 1
                                Else
                                    errorCount = 2
                                End If
                            End Using

                        ElseIf get_subjectType = "Choose" Then

                            Dim find_old_subjectLevel As String = "select subject_StudentYear from subject_info where subject_ID = '" & strKey & "'"
                            Dim get_old_subjectLevel As String = oCommon.getFieldValue(find_old_subjectLevel)

                            If get_old_subjectLevel = "Foundation 1" Then
                                get_old_subjectLevel = "Foundation 2"
                            ElseIf get_old_subjectLevel = "Foundation 2" Then
                                get_old_subjectLevel = "Foundation 3"
                            ElseIf get_old_subjectLevel = "Foundation 3" Then
                                get_old_subjectLevel = "Level 1"
                            ElseIf get_old_subjectLevel = "Level 1" Then
                                get_old_subjectLevel = "Level 2"
                            End If

                            Using PJGDATA As New SqlCommand("   INSERT INTO subject_info(subject_Name, subject_NameBM, subject_code, subject_year, subject_type, subject_religions, subject_StudentYear, subject_CreditHour, subject_sem, course_Name)
                                                                Values('" & get_subjectName & "','" & get_subjectNameBM & "','" & check_code & "','" & ddlyear_Transfer.SelectedValue & "','" & get_subjectType & "','" & get_subjectReligions & "',
                                                                '" & get_old_subjectLevel & "','" & Integer.Parse(get_subjectHour) & "','" & get_subjectSem & "','" & get_subjectCourseName & "')", objConn)
                                objConn.Open()
                                Dim j = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                                If j <> 0 Then
                                    errorCount = 1
                                Else
                                    errorCount = 2
                                End If
                            End Using

                        End If

                    End If

                End If
            End If
        Next

        If errorCount = 1 Then
            ShowMessage("Successful Transfer Course", MessageType.Success)
        Else
            ShowMessage("Unsuccessful Transfer Course", MessageType.Error)
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