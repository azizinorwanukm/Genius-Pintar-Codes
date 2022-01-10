Imports System.Data.SqlClient

Public Class class_List_Table
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

                Checking_MenuAccess_Load()

                If Session("getStatus") = "RC" Then ''Register Class
                    txtbreadcrum1.Text = "Register Class"

                    RegisterClass.Visible = True
                    ViewClass.Visible = False
                    TransferClass.Visible = False

                    btnRegisterClass.Attributes("class") = "btn btn-info"
                    btnViewClass.Attributes("class") = "btn btn-default font"
                    btnTransferClass.Attributes("class") = "btn btn-default font"

                    year_list()
                    class_type_load()
                    level_list()
                    sem_list()
                    staff_info_list()
                    course_program_load()
                    campus_List()

                ElseIf Session("getStatus") = "VC" Then ''View Class
                    txtbreadcrum1.Text = "View Class"

                    RegisterClass.Visible = False
                    ViewClass.Visible = True
                    TransferClass.Visible = False

                    btnRegisterClass.Attributes("class") = "btn btn-default font"
                    btnViewClass.Attributes("class") = "btn btn-info"
                    btnTransferClass.Attributes("class") = "btn btn-default font"

                    year_list()
                    load_page()
                    program_list()
                    viewlevel_list()
                    viewCampus_list()
                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "TC" Then ''Transfer Class
                    txtbreadcrum1.Text = "Transfer Class"

                    RegisterClass.Visible = False
                    ViewClass.Visible = False
                    TransferClass.Visible = True

                    btnRegisterClass.Attributes("class") = "btn btn-default font"
                    btnViewClass.Attributes("class") = "btn btn-default font"
                    btnTransferClass.Attributes("class") = "btn btn-info"

                    year_load()
                    student_year_load()
                    student_program_load()
                    campus_load()

                    strRet = BindDataTransfer(TransferRespondent)

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewClass.Visible = False
        btnRegisterClass.Visible = False
        btnTransferClass.Visible = False
        ViewClass.Visible = False
        RegisterClass.Visible = False
        TransferClass.Visible = False
        btnUpdate.Visible = False
        btnButtonTransfer.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewClass As String = ""
        Dim Get_RegisterClass As String = ""
        Dim Get_TransferClass As String = ""

        ''Loop The Count_No
        For num As Integer = 0 To find_CountNo_LoginID - 1 Step 1

            ''Get Main Menu Data
            strSQL = "  Select A.Menu From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_Menu_Data As String = oCommon.getFieldValue(strSQL)

            ''Get Sub Menu 2 Data
            strSQL = "  Select A.Menu_Sub2 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_SubMenu2 As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Edit Data 
            strSQL = "  Select B.F1_Edit From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Edit As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Delete Data 
            strSQL = "  Select B.F1_Delete From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Delete As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Register Data 
            strSQL = "  Select B.F1_Register From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Register As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Transfer Data
            strSQL = "  Select B.F1_Transfer From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Transfer As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "View Course" And find_Data_SubMenu2.Length > 0 Then
                btnViewClass.Visible = True
                ViewClass.Visible = True

                Get_ViewClass = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Course" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterClass.Visible = True
                RegisterClass.Visible = True

                Get_RegisterClass = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnUpdate.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Transfer Course" And find_Data_SubMenu2.Length > 0 Then
                btnTransferClass.Visible = True
                TransferClass.Visible = True

                Get_TransferClass = "TRUE"

                If find_Data_F1Transfer.Length > 0 And find_Data_F1Transfer = "TRUE" Then
                    btnButtonTransfer.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewClass.Visible = True
                btnRegisterClass.Visible = True
                btnTransferClass.Visible = True
                ViewClass.Visible = True
                RegisterClass.Visible = True
                TransferClass.Visible = True
                btnUpdate.Visible = True
                btnButtonTransfer.Visible = True

                Get_ViewClass = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RC" Or Session("getStatus") = "VC" Or Session("getStatus") = "TC" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RC" And Session("getStatus") <> "VC" And Session("getStatus") <> "TC" Then
            If Get_ViewClass = "TRUE" Then
                Data_If_Not_Group_Status = "VC"
            ElseIf Get_RegisterClass = "TRUE" Then
                Data_If_Not_Group_Status = "RC"
            ElseIf Get_TransferClass = "TRUE" Then
                Data_If_Not_Group_Status = "TC"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewClass = "TRUE" And Data_If_Not_Group_Status = "VC" Then
                Session("getStatus") = "VC"
            ElseIf Get_RegisterClass = "TRUE" And Data_If_Not_Group_Status = "RC" Then
                Session("getStatus") = "RC"
            ElseIf Get_TransferClass = "TRUE" And Data_If_Not_Group_Status = "TC" Then
                Session("getStatus") = "TC"
            End If
        End If

    End Sub

    Private Sub btnRegisterClass_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterClass.ServerClick
        Session("getStatus") = "RC"
        Response.Redirect("admin_pengurusan_am_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewClass_ServerClick(sender As Object, e As EventArgs) Handles btnViewClass.ServerClick
        Session("getStatus") = "VC"
        Response.Redirect("admin_pengurusan_am_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnTransferClass_ServerClick(sender As Object, e As EventArgs) Handles btnTransferClass.ServerClick
        Session("getStatus") = "TC"
        Response.Redirect("admin_pengurusan_am_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub program_list()
        Try
            If ddl_Campus.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterProgram.DataSource = levds
            filterProgram.DataValueField = "Value"
            filterProgram.DataTextField = "Parameter"
            filterProgram.DataBind()
            filterProgram.Items.Insert(0, New ListItem("Select Course Program", String.Empty))
            filterProgram.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub viewlevel_list()
        Try
            Dim strLevelSql As String = "select * from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterLevel.DataSource = levds
            filterLevel.DataValueField = "Value"
            filterLevel.DataTextField = "Parameter"
            filterLevel.DataBind()
            filterLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            filterLevel.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub viewCampus_list()
        Try
            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If
            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Campus.DataSource = levds
            ddl_Campus.DataValueField = "Value"
            ddl_Campus.DataTextField = "Parameter"
            ddl_Campus.DataBind()
            ddl_Campus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
            ddl_Campus.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            filterYear.DataSource = ds
            filterYear.DataTextField = "Parameter"
            filterYear.DataValueField = "Parameter"
            filterYear.DataBind()
            filterYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT class_year from class_info where class_year ='" & Now.Year & "'"

        '--debug
        ''Response.Write(strSQLstd)

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_year")) Then
                filterYear.SelectedValue = ds.Tables(0).Rows(0).Item("class_year")
            Else
                filterYear.SelectedValue = ""
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

            If Session("getEditButton") = "TRUE" Then
                gvTable.Columns(8).Visible = True
            Else
                gvTable.Columns(8).Visible = False
            End If

            If Session("getDeleteButton") = "TRUE" Then
                gvTable.Columns(9).Visible = True
            Else
                gvTable.Columns(9).Visible = False
            End If

            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY class_type, class_Name ASC"

        tmpSQL = "Select class_info.class_ID, staff_info.staff_ID, UPPER(staff_Name) staff_Name, class_Name, class_year, class_Level, UPPER(course_Program) course_Program, class_Campus From class_info"
        tmpSQL += " Left Join staff_Info on class_info.stf_ID=staff_Info.stf_ID"
        strWhere += " WHERE class_ID IS NOT NULL"
        strWhere += " and class_info.class_year = '" & filterYear.SelectedValue & "'"

        strWhere += " and class_Campus = '" & ddl_Campus.SelectedValue & "' and staff_Campus = '" & ddl_Campus.SelectedValue & "'"

        If filterProgram.SelectedIndex > 0 Then
            strWhere += " AND class_info.course_Program = '" & filterProgram.SelectedValue & "'"
        End If

        If filterLevel.SelectedIndex > 0 Then
            strWhere += " AND class_info.class_Level = '" & filterLevel.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete class_info where class_id ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyName As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Dim adminID As String = Request.QueryString("admin_ID")
        Try
            Response.Redirect("admin_edit_kelas_data.aspx?class_ID=" + strKeyName + "&admin_ID=" + adminID)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub filterProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles filterProgram.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Campus.SelectedIndexChanged
        Try
            program_list()
            'lect_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles filterLevel.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles filterYear.SelectedIndexChanged
        Try
            'lect_list()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
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

    Private Sub staff_info_list()
        strSQL = "SELECT stf_ID,staff_Name FROM staff_Info Where staff_Status = 'Access' and staff_Campus = '" & ddlCampus.SelectedValue & "' order by staff_Name ASC"
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

    Private Sub course_program_load()

        If Session("SchoolCampus") = "APP" Then
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

            ddlStream.DataSource = ds
            ddlStream.DataTextField = "Parameter"
            ddlStream.DataValueField = "Value"
            ddlStream.DataBind()
            ddlStream.Items.Insert(0, New ListItem("Select Course Program", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub campus_List()
        strSQL = "Select Parameter, Value from setting where type = 'Pusat Campus'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCampus.DataSource = ds
            ddlCampus.DataTextField = "Parameter"
            ddlCampus.DataValueField = "Value"
            ddlCampus.DataBind()
            ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub class_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles class_Type.SelectedIndexChanged
        Try
            courseName_Load()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub courseName_Load()

        If class_Type.SelectedValue = "Electives" Then
            displaystatusCourseName.Visible = True
            displaystatusSemester.Visible = True

            staff_info_list()

            strSQL = "  SELECT subject_Name ,subject_ID FROM subject_info 
                        Where subject_year = '" & ddlYear.SelectedValue & "' 
                        and subject_type != 'Compulsory' 
                        and subject_StudentYear = '" & class_Level.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "'
                        and subject_sem = '" & class_Sem.SelectedValue & "' order by subject_Name ASC"

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            course_Name.DataSource = ds
            course_Name.DataTextField = "subject_Name"
            course_Name.DataValueField = "subject_ID"
            course_Name.DataBind()
            course_Name.Items.Insert(0, New ListItem("Select Course", "NULL"))

        Else
            displaystatusSemester.Visible = False
            displaystatusCourseName.Visible = False

            staff_info_list()

            strSQL = "  SELECT subject_Name ,subject_ID FROM subject_info Where subject_year = '" & Now.Year & "'
                        and subject_StudentYear = '" & class_Level.SelectedValue & "' and subject_sem = '" & class_Sem.SelectedValue & "' and subject_type <> 'Compulsory'
                        and course_Program = '" & ddlStream.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "' order by subject_Name ASC"
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
    End Sub

    Private Sub class_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles class_Level.SelectedIndexChanged
        Try
            courseName_Load()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampus.SelectedIndexChanged
        Try
            courseName_Load()
            staff_info_list()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dddlStream_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStream.SelectedIndexChanged
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

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick

        Dim errorCount As Integer = 0

        If class_Name.Text <> "" And Regex.IsMatch(class_Name.Text, "^[A-Za-z0-9 ]+$") Then

            If ddlYear.SelectedIndex > 0 Then

                If class_Level.SelectedIndex > 0 Then

                    If ddlStream.SelectedIndex > 0 Then

                        If ddlCampus.SelectedIndex > 0 Then

                            If course_Name.SelectedIndex > 0 Then

                                If class_Type.SelectedValue = "Compulsory" Then

                                    If staff_ID.SelectedValue <> "" Then

                                        strSQL = "Select class_ID from class_info where class_Name = '" & class_Name.Text & "' and class_Level = '" & class_Level.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and class_year = '" & ddlYear.SelectedValue & "' and class_Campus = '" & ddlCampus.SelectedValue & "'"
                                        Dim checkingExisting As String = oCommon.getFieldValue(strSQL)

                                        If checkingExisting.Length = 0 Then
                                            'Insert into class_info in kolejadmin database
                                            Using CLASSDATA As New SqlCommand(" INSERT into class_info(class_Name,class_year,class_Level,stf_ID,class_Type,subject_ID, course_Program, class_Campus) 
                                                                                values ('" & class_Name.Text & "','" & ddlYear.SelectedValue & "','" & class_Level.SelectedValue & "','" & staff_ID.SelectedValue & "',
                                                                                '" & class_Type.SelectedValue & "','" & course_Name.SelectedValue & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                                objConn.Open()
                                                Dim i = CLASSDATA.ExecuteNonQuery()
                                                objConn.Close()

                                                If i <> 0 Then
                                                    ShowMessage("Successful Add New Class", MessageType.Success)

                                                    strSQL = "Select KelasID from koko_kelas where Kelas = '" & class_Name.Text & "' and Tahun = '" & ddlYear.SelectedValue & "' and Kampus = '" & ddlCampus.SelectedValue & "'"
                                                    strRet = oCommon.getFieldValue_Permata(strSQL)

                                                    If class_Type.SelectedValue = "Compulsory" And strRet.Length = 0 Then
                                                        ''Insert into koko_kelas in permatapintar database
                                                        strSQL = "insert into koko_kelas(Kelas,Tahun,Kampus) values('" & class_Name.Text & "','" & ddlYear.SelectedValue & "','" & ddlCampus.SelectedValue & "')"
                                                        strRet = oCommon.ExecuteSQLPermata(strSQL)
                                                    End If
                                                Else
                                                    ShowMessage("Unsuccessful Add New Class", MessageType.Error)
                                                End If
                                            End Using
                                        Else
                                            ShowMessage(class_Name.Text & " Had Been Registerd For Year " & ddlYear.SelectedValue, MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage("Please select homeroom", MessageType.Error)
                                    End If
                                Else

                                    If staff_ID.SelectedValue = "" Or staff_ID.SelectedValue <> "" Then

                                        If course_Name.SelectedValue <> "" Then

                                            strSQL = "Select class_ID from class_info where class_Name = '" & class_Name.Text & "' and class_Level = '" & class_Level.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and class_year = '" & ddlYear.SelectedValue & "' and class_Campus = '" & ddlCampus.SelectedValue & "'"
                                            Dim checkingExisting As String = oCommon.getFieldValue(strSQL)

                                            If checkingExisting.Length = 0 Then
                                                'Insert into class_info in kolejadmin database
                                                Using CLASSDATA As New SqlCommand(" INSERT into class_info(class_Name,class_year,class_Level,stf_ID,class_Type,subject_ID,class_sem, course_Program, class_Campus) 
                                                                                    values ('" & class_Name.Text & "','" & ddlYear.SelectedValue & "','" & class_Level.SelectedValue & "','" & staff_ID.SelectedValue & "',
                                                                                    '" & class_Type.SelectedValue & "','" & course_Name.SelectedValue & "', '" & class_Sem.SelectedValue & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                                    objConn.Open()
                                                    Dim i = CLASSDATA.ExecuteNonQuery()
                                                    objConn.Close()

                                                    If i <> 0 Then
                                                        ShowMessage("Successful Add New Class", MessageType.Success)
                                                    Else
                                                        ShowMessage("Unsuccessful Add New Class", MessageType.Error)
                                                    End If
                                                End Using
                                            Else
                                                ShowMessage(class_Name.Text & " Had Been Registerd For Year " & ddlYear.SelectedValue, MessageType.Error)
                                            End If
                                        Else
                                            ShowMessage("Please select class course ", MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage("Please select homeroom", MessageType.Error)
                                    End If
                                End If

                            Else
                                ShowMessage("Please Select Course Name", MessageType.Error)
                            End If
                        Else
                            ShowMessage("Please Select Institutions", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please Select Program", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please Select Class Level", MessageType.Error)
                End If
            Else
                ShowMessage("Please Select Class Year", MessageType.Error)
            End If
        Else
            ShowMessage("Please Fill In Class Name", MessageType.Error)
        End If

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Function BindDataTransfer(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLTransfer, strConn)
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

    Private Function getSQLTransfer() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY class_Type, class_Name, class_Sem ASC"

        tmpSQL = "Select * From class_info"
        strWhere += " WHERE class_ID IS NOT NULL AND class_year = '" & ddl_Year.SelectedValue & "' and class_Campus = '" & ddl_CampusTransfer.SelectedValue & "'"

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " AND class_level = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_Program.SelectedIndex > 0 Then
            strWhere += " AND course_Program = '" & ddl_Program.SelectedValue & "'"
        End If

        getSQLTransfer = tmpSQL & strWhere & strOrderby
        Return getSQLTransfer
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

    Private Sub student_program_load()
        Try
            If ddl_CampusTransfer.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Program.DataSource = levds
            ddl_Program.DataValueField = "Value"
            ddl_Program.DataTextField = "Parameter"
            ddl_Program.DataBind()
            ddl_Program.Items.Insert(0, New ListItem("Select Course Program", String.Empty))
            ddl_Program.SelectedIndex = 0

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

    Private Sub campus_load()
        Try
            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If
            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_CampusTransfer.DataSource = levds
            ddl_CampusTransfer.DataValueField = "Value"
            ddl_CampusTransfer.DataTextField = "Parameter"
            ddl_CampusTransfer.DataBind()
            ddl_CampusTransfer.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
            ddl_CampusTransfer.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindDataTransfer(TransferRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            strRet = BindDataTransfer(TransferRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Program.SelectedIndexChanged
        Try
            strRet = BindDataTransfer(TransferRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_CampusTransfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_CampusTransfer.SelectedIndexChanged
        Try
            student_program_load()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnButtonTransfer_ServerClick(sender As Object, e As EventArgs) Handles btnButtonTransfer.ServerClick

        If Checking_Data() = False Then
            Exit Sub
        End If

        Dim i As Integer
        Dim errorCount As Integer = 0

        For i = 0 To TransferRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(TransferRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then

                ''get class_ID
                Dim strKey As String = TransferRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim find_old_subjectName As String = ""
                    Dim get_old_subjectName As String = ""
                    Dim find_old_courseName As String = ""
                    Dim get_old_courseName As String = ""
                    Dim find_new_subjectID As String = ""
                    Dim get_new_subjectID As String = ""
                    Dim find_old_className As String = ""
                    Dim get_old_className As String = ""
                    Dim find_new_className As String = ""
                    Dim get_new_className As String = ""
                    Dim find_new_courseProgram As String = ""
                    Dim get_old_courseProgram As String = ""
                    Dim get_old_classType As String = ""

                    Dim set_classLevel As String = ""
                    Dim set_classSem As String = ""
                    Dim set_className As String = ""

                    Dim answer As Char
                    Dim answerInt As Integer = 0

                    ''get class stype
                    strSQL = "Select class_type from class_info where class_ID = '" & strKey & "'"
                    get_old_classType = oCommon.getFieldValue(strSQL)

                    If get_old_classType = "Electives" Then

                        ''get old subject_Name 
                        find_old_subjectName = "select subject_Name from subject_info left join class_info on subject_info.subject_ID = class_info.subject_ID where class_info.class_ID = '" & strKey & "'"
                        get_old_subjectName = oCommon.getFieldValue(find_old_subjectName)

                        ''get old course_Name
                        find_old_courseName = "select course_Name from subject_info left join class_info on subject_info.subject_ID = class_info.subject_ID where class_info.class_ID = '" & strKey & "'"
                        get_old_courseName = oCommon.getFieldValue(find_old_courseName)

                        ''get class Sem
                        strSQL = "Select class_sem from class_info where class_ID = '" & strKey & "'"
                        set_classSem = oCommon.getFieldValue(strSQL)

                    End If

                    ''get old class name
                    find_old_className = "select class_Name from class_info where class_ID = '" & strKey & "'"
                    get_old_className = oCommon.getFieldValue(find_old_className)

                    ''get new course program
                    Dim find_old_courseProgram = "Select course_Program from class_info where class_ID = '" & strKey & "'"
                    Dim get_new_courseProgram As String = oCommon.getFieldValue(find_old_courseProgram)

                    ''get new class campus
                    Dim find_old_classCampus = "Select class_Campus from class_info where class_ID = '" & strKey & "'"
                    Dim get_new_classCampus As String = oCommon.getFieldValue(find_old_classCampus)

                    If ddl_Year.SelectedValue = ddlyear_Transfer.SelectedValue Then

                        ''set new class level
                        set_classLevel = ddl_Level.SelectedValue

                        ''set new class semester
                        If set_classSem = "Sem 1" Then
                            set_classSem = "Sem 2"
                        End If

                        ''set new class name
                        get_new_className = get_old_className

                    ElseIf ddl_Year.SelectedValue > ddlyear_Transfer.SelectedValue Then

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

                        ''combine character & set new class name
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

                        ''error occur
                    ElseIf ddl_Year.SelectedValue < ddlyear_Transfer.SelectedValue Then
                        ShowMessage("Transfer Year Should Be Bigger Than Selected Year", MessageType.Error)
                        Exit Sub
                    End If

                    If get_old_classType = "Electives" Then
                        strSQL = "and class_sem = '" & set_classSem & "'"
                    Else
                        strSQL = ""
                    End If

                    If get_old_classType = "Electives" Then

                        ''get subject ID
                        find_new_subjectID = "select subject_ID from subject_info where subject_year = '" & ddlyear_Transfer.SelectedValue & "' and subject_Name = '" & get_old_subjectName & "' and course_Name = '" & get_old_courseName & "' and subject_Sem = '" & set_classSem & "' and subject_StudentYear = '" & set_classLevel & "'"
                        get_new_subjectID = oCommon.getFieldValue(find_new_subjectID)

                    End If

                    ''check if the new class name is exist in database
                    Dim go_checking As String = "select class_ID from class_info where class_year = '" & ddlyear_Transfer.SelectedValue & "' and class_Name = '" & get_new_className & "' and class_type = '" & get_old_classType & "' and course_Program = '" & get_new_courseProgram & "' and class_Campus = '" & get_new_classCampus & "' " & strSQL & ""
                    Dim go_confirm As String = oCommon.getFieldValue(go_checking)

                    If go_confirm.Length = 0 Then

                        Using PJGDATA As New SqlCommand("INSERT INTO class_info(class_Name, class_Level, class_sem, class_Year, class_type, subject_ID, course_Program, class_Campus) 
                                                         VALUES('" & get_new_className & "','" & set_classLevel & "','" & set_classSem & "','" & ddlyear_Transfer.SelectedValue & "','" & get_old_classType & "','" & get_new_subjectID & "', '" & get_new_courseProgram & "','" & get_new_classCampus & "')", objConn)
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

                    If get_old_classType = "Compulsory" Then

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
            ShowMessage("Successful transfer class", MessageType.Success)
        Else
            ShowMessage("Class had been transfered", MessageType.Error)
        End If

    End Sub

    Private Function Checking_Data() As Boolean

        If ddl_Year.SelectedIndex = 0 Then
            ShowMessage("Please Select Year", MessageType.Error)
            Return False
        End If

        If ddl_CampusTransfer.SelectedIndex = 0 Then
            ShowMessage("Please Select Institutions", MessageType.Error)
            Return False
        End If

        If ddl_Program.SelectedIndex = 0 Then
            ShowMessage("Please Select Program", MessageType.Error)
            Return False
        End If

        If ddl_Level.SelectedIndex = 0 Then
            ShowMessage("Please Select Level", MessageType.Error)
            Return False
        End If

        If ddlyear_Transfer.SelectedIndex = 0 Then
            ShowMessage("Please Select Transfer Year", MessageType.Error)
            Return False
        End If

        Return True
    End Function

End Class