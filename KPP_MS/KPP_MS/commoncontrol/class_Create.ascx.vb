Imports System.Data.SqlClient

Public Class class_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim data As String = oCommon.securityLogin(Request.QueryString("admin_ID"))

                previousPage.NavigateUrl = String.Format("~/admin_pengurusan_am_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    Dim getStatus As String = Request.QueryString("status")

                    If getStatus = "CC" Then ''create course
                        txtbreadcrum1.Text = "Create Class"
                        ClassCreate.Visible = True
                        ClassTransfer.Visible = False

                        btnClassCreate.Attributes("class") = "btn btn-info"
                        btnClassTransfer.Attributes("class") = "btn btn-default font"

                        displaystatusSemester.Visible = False
                        displaystatusCourseName.Visible = False

                        year_list()
                        class_type_load()

                        level_list()
                        sem_list()



                    ElseIf getStatus = "TC" Then ''transfer course
                        txtbreadcrum1.Text = "Transfer Class"
                        ClassCreate.Visible = False
                        ClassTransfer.Visible = True

                        btnClassCreate.Attributes("class") = "btn btn-default font"
                        btnClassTransfer.Attributes("class") = "btn btn-info"

                        year_load()
                        student_year_load()
                        student_sem_load()
                        subject_type_load()

                        Page_Load()


                    End If

                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClassCreate_ServerClick(sender As Object, e As EventArgs) Handles btnClassCreate.ServerClick
        Response.Redirect("admin_daftar_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=CC")
    End Sub

    Private Sub btnClassTransfer_ServerClick(sender As Object, e As EventArgs) Handles btnClassTransfer.ServerClick
        Response.Redirect("admin_daftar_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=TC")
    End Sub

    Private Sub class_type_load()
        strSQL = "SELECT * from class_info where class_ID is NULL"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            class_Type.DataSource = ds
            class_Type.DataTextField = "class_Type"
            class_Type.DataValueField = "class_Type"
            class_Type.DataBind()
            class_Type.Items.Insert(0, New ListItem("Select Class Type", String.Empty))
            class_Type.Items.Insert(1, New ListItem("Compulsory", "Compulsory"))
            class_Type.Items.Insert(2, New ListItem("Electives", "Electives"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub sem_list()
        strSQL = "SELECT * from setting where Type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            class_Sem.DataSource = ds
            class_Sem.DataTextField = "Parameter"
            class_Sem.DataValueField = "Value"
            class_Sem.DataBind()
            class_Sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub level_list()
        strSQL = "SELECT Parameter from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            class_Level.DataSource = ds
            class_Level.DataTextField = "Parameter"
            class_Level.DataValueField = "Parameter"
            class_Level.DataBind()
            class_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter from setting where Type = 'Year' Order by Parameter Desc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staff_info_list()
        strSQL = "SELECT stf_ID,staff_Name FROM staff_Info Where staff_Status = 'Access' order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_ID.DataSource = ds
            staff_ID.DataTextField = "staff_Name"
            staff_ID.DataValueField = "stf_ID"
            staff_ID.DataBind()
            staff_ID.Items.Insert(0, New ListItem("Select Staff", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick

        Dim errorCount As Integer = 0

        If class_Name.Text <> "" And Regex.IsMatch(class_Name.Text, "^[A-Za-z0-9]+$") Then

            If class_Level.SelectedValue <> "" Then

                If class_Type.SelectedValue = "Compulsory" Then

                    If staff_ID.SelectedValue <> "" Then

                        'Insert into class_info in kolejadmin database
                        Using CLASSDATA As New SqlCommand("INSERT into class_info(class_Name,class_year,class_Level,stf_ID,class_Type,subject_ID) 
                                                               values ('" & class_Name.Text & "','" & ddlYear.SelectedValue & "','" & class_Level.Text & "','" & staff_ID.SelectedValue & "',
                                                               '" & class_Type.SelectedValue & "','" & course_Name.SelectedValue & "')", objConn)
                            objConn.Open()
                            Dim i = CLASSDATA.ExecuteNonQuery()
                            objConn.Close()

                            If i <> 0 Then
                                ShowMessage("Successful Add New Class", MessageType.Success)

                                If class_Type.SelectedValue = "Compulsory" Then
                                    ''Insert into koko_kelas in permatapintar database
                                    strSQL = "insert into koko_kelas(Kelas,Tahun) values('" & class_Name.Text & "','" & ddlYear.SelectedValue & "')"
                                    strRet = oCommon.ExecuteSQLPermata(strSQL)
                                End If
                            Else
                                ShowMessage("Unsuccessful Add New Class", MessageType.Error)
                            End If
                        End Using

                    Else
                        ShowMessage("Please select homeroom", MessageType.Error)
                    End If

                Else

                    If staff_ID.SelectedValue = "" Or staff_ID.SelectedValue <> "" Then

                        If course_Name.SelectedValue <> "" Then

                            'Insert into class_info in kolejadmin database
                            Using CLASSDATA As New SqlCommand("INSERT into class_info(class_Name,class_year,class_Level,stf_ID,class_Type,subject_ID,class_sem) 
                                                               values ('" & class_Name.Text & "','" & ddlYear.SelectedValue & "','" & class_Level.Text & "','" & staff_ID.SelectedValue & "',
                                                               '" & class_Type.SelectedValue & "','" & course_Name.SelectedValue & "', '" & class_Sem.SelectedValue & "')", objConn)
                                objConn.Open()
                                Dim i = CLASSDATA.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    ShowMessage("Successful Add New Class", MessageType.Success)
                                Else
                                    ShowMessage("Unsuccessful Add New Class", MessageType.Error)
                                End If
                            End Using
                        End If
                    Else
                        ShowMessage("Please select homeroom", MessageType.Error)
                    End If

                End If
            Else
                ShowMessage("Please select class type", MessageType.Error)
            End If

        Else
            ShowMessage("Please fill in class name", MessageType.Error)
        End If

    End Sub

    Protected Sub class_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles class_Type.SelectedIndexChanged
        Try

            If class_Type.SelectedValue = "Electives" Then
                displaystatusCourseName.Visible = True
                displaystatusSemester.Visible = True

                courseName_Load()
                staff_info_list()

            Else
                displaystatusSemester.Visible = False
                displaystatusCourseName.Visible = False

                staff_info_list()

                strSQL = "SELECT subject_Name ,subject_ID FROM subject_info Where subject_year = '" & Now.Year & "' and subject_ID is null order by subject_Name ASC"
                Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
                Dim objConn As SqlConnection = New SqlConnection(strConn)
                Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "AnyTable")

                course_Name.DataSource = ds
                course_Name.DataTextField = ""
                course_Name.DataValueField = ""
                course_Name.DataBind()
                course_Name.Items.Insert(0, New ListItem("Select Course", String.Empty))

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub courseName_Load()
        strSQL = "  SELECT subject_Name ,subject_ID FROM subject_info 
                    Where subject_year = '" & ddlYear.SelectedValue & "' 
                    and subject_type != 'Compulsory' 
                    and subject_StudentYear = '" & class_Level.SelectedValue & "' 
                    and subject_sem = '" & class_Sem.SelectedValue & "' order by subject_Name ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            course_Name.DataSource = ds
            course_Name.DataTextField = "subject_Name"
            course_Name.DataValueField = "subject_ID"
            course_Name.DataBind()
            course_Name.Items.Insert(0, New ListItem("Select Course", "NULL"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles class_Level.SelectedIndexChanged
        Try
            courseName_Load()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub class_Sem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles class_Sem.SelectedIndexChanged
        Try
            courseName_Load()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Page_Load()
        ''student_info
        strSQL = "SELECT MAX(Parameter) FROM setting WHERE Type = 'Year' "

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

            If ddl_type.SelectedValue = "Compulsory" Then
                datRespondent.Columns(6).Visible = False
            Else
                datRespondent.Columns(6).Visible = True
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function


    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY class_Name ASC"

        tmpSQL = "Select * From class_info"
        strWhere += " WHERE class_ID IS NOT NULL "

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " AND class_level = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_type.SelectedIndex > 0 Then
            strWhere += " AND class_type = '" & ddl_type.SelectedValue & "'"

            If ddl_type.SelectedValue = "Electives" And ddl_Sem.SelectedIndex > 0 Then
                strWhere += " AND class_sem = '" & ddl_Sem.SelectedValue & "'"
            End If
        End If

        If ddl_Year.SelectedIndex > 0 Then
            strWhere += " AND class_year = '" & ddl_Year.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Private Sub year_load()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Year'"
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

    Private Sub student_year_load()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Level'"
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

    Private Sub student_sem_load()
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

    Private Sub subject_type_load()
        Try
            Dim strLevelSql As String = "select Parameter from setting where Type = 'Class Type'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_type.DataSource = levds
            ddl_type.DataValueField = "Parameter"
            ddl_type.DataTextField = "Parameter"
            ddl_type.DataBind()
            ddl_type.Items.Insert(0, New ListItem("Select Class Type", String.Empty))
            ddl_type.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Sem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_type.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnTransferClass_ServerClick(sender As Object, e As EventArgs) Handles btnTransferClass.ServerClick

        Dim i As Integer
        Dim errorCount As Integer = 0

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then

                ''get class_ID
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim find_old_subjectCode As String = ""
                    Dim get_old_subjectCode As String = ""
                    Dim find_new_subjectID As String = ""
                    Dim get_new_subjectID As String = ""
                    Dim find_old_className As String = ""
                    Dim get_old_className As String = ""
                    Dim find_new_className As String = ""
                    Dim get_new_className As String = ""

                    Dim set_classLevel As String = ""
                    Dim set_classSem As String = ""
                    Dim set_className As String = ""

                    Dim answer As Char
                    Dim answerInt As Integer = 0

                    If ddl_type.SelectedValue = "Electives" Then

                        find_old_subjectCode = "select subject_code from subject_info left join class_info on subject_info.subject_ID = class_info.subject_ID
                                                where class_info.class_year = '" & ddl_Year.SelectedValue & "' and class_info.class_type = '" & ddl_type.SelectedValue & "' and class_info.class_ID = '" & strKey & "'"
                        get_old_subjectCode = oCommon.getFieldValue(find_old_subjectCode)

                        ''get subject ID
                        find_new_subjectID = "select subject_ID from subject_info where subject_year = '" & ddlyear_Transfer.SelectedValue & "' and subject_code = '" & get_old_subjectCode & "'"
                        get_new_subjectID = oCommon.getFieldValue(find_new_subjectID)

                        ''set a class sem
                        set_classSem = ddl_Sem.SelectedValue

                        find_old_className = "select class_Name from class_info where class_ID = '" & strKey & "'"
                        get_old_className = oCommon.getFieldValue(find_old_className)

                    Else

                        find_old_className = "select class_Name from class_info where class_ID = '" & strKey & "'"
                        get_old_className = oCommon.getFieldValue(find_old_className)

                    End If

                    ''get first character of string & convert to integer format
                    answer = get_old_className.Chars(0)
                    answerInt = Integer.Parse(answer)

                    ''convert the answer to = answerInt + 1
                    If answerInt = 1 Then
                        answerInt += 1
                    ElseIf answerInt = 2 Then
                        answerInt += 1
                    ElseIf answerInt = 3 Then
                        answerInt += 1
                    ElseIf answerInt = 4 Then
                        answerInt += 1
                    End If

                    ''combine character
                    get_new_className = answerInt & get_old_className.Remove(0, 1)

                    ''change old class level to new class level
                    If ddl_Level.SelectedValue = "Foundation 1" Then
                        set_classLevel = "Foundation 2"
                    ElseIf ddl_Level.SelectedValue = "Foundation 2" Then
                        set_classLevel = "Foundation 3"
                    ElseIf ddl_Level.SelectedValue = "Foundation 3" Then
                        set_classLevel = "Level 1"
                    ElseIf ddl_Level.SelectedValue = "Level 1" Then
                        set_classLevel = "Level 2"
                    End If

                    ''check if the new class name is exist in database
                    Dim go_checking As String = "select class_ID from class_info where class_year = '" & ddlyear_Transfer.SelectedValue & "' and class_Name = '" & get_new_className & "'"
                    Dim go_confirm As String = oCommon.getFieldValue(go_checking)

                    If go_confirm.Length = 0 Then

                        Using PJGDATA As New SqlCommand("INSERT INTO class_info(class_Name, class_Level, class_sem, class_Year, class_type, subject_ID) 
                                                         VALUES('" & get_new_className & "','" & set_classLevel & "','" & set_classSem & "','" & ddlyear_Transfer.SelectedValue & "','" & ddl_type.SelectedValue & "','" & get_new_subjectID & "')", objConn)
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

                    If ddl_type.SelectedValue = "Compulsory" Then

                        Dim go_checking_permata As String = "select Kelas from koko_kelas where Tahun = '" & ddlyear_Transfer.SelectedValue & "' and Kelas = '" & get_new_className & "'"
                        Dim go_confirm_permata As String = oCommon.getFieldValue_Permata(go_checking_permata)

                        If go_confirm.Length = 0 Then
                            ''Insert into koko_kelas in permatapintar database
                            strSQL = "insert into koko_kelas(Kelas,Tahun) values('" & get_new_className & "','" & ddlyear_Transfer.SelectedValue & "')"
                            strRet = oCommon.ExecuteSQLPermata(strSQL)
                        End If

                    End If
                End If
            End If
        Next

        If errorCount = 1 Then
            ShowMessage("Successful Transfer Class", MessageType.Success)
        Else
            ShowMessage("Unsuccessful Transfer Class", MessageType.Error)
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