Imports System.Data.SqlClient

Public Class course_List_Table
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

                Checking_MenuAccess_Load()

                If Session("getStatus") = "RC" Then ''Register Course
                    txtbreadcrum1.Text = "Register Course"

                    RegisterCourse.Visible = True
                    ViewCourse.Visible = False
                    TransferCourse.Visible = False

                    btnRegisterCourse.Attributes("class") = "btn btn-info"
                    btnViewCourse.Attributes("class") = "btn btn-default font"
                    btnTransferCourse.Attributes("class") = "btn btn-default font"

                    year_load()
                    subject_type_load()
                    subject_sem_Load()
                    subject_StudentYear_Load()
                    subject_religions_Load()
                    subject_stream_Load()
                    subject_group_Load()
                    student_campus_load()

                    Load_Page()

                ElseIf Session("getStatus") = "VC" Then ''View Course
                    txtbreadcrum1.Text = "View Course"

                    RegisterCourse.Visible = False
                    ViewCourse.Visible = True
                    TransferCourse.Visible = False

                    btnRegisterCourse.Attributes("class") = "btn btn-default font"
                    btnViewCourse.Attributes("class") = "btn btn-info"
                    btnTransferCourse.Attributes("class") = "btn btn-default font"

                    year_load()
                    student_year_load()
                    student_sem_load()
                    subject_campus_load()
                    course_program_load()

                    Page_Load()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "TC" Then ''Transfer Course
                    txtbreadcrum1.Text = "Transfer Course"

                    RegisterCourse.Visible = False
                    ViewCourse.Visible = False
                    TransferCourse.Visible = True

                    btnRegisterCourse.Attributes("class") = "btn btn-default font"
                    btnViewCourse.Attributes("class") = "btn btn-default font"
                    btnTransferCourse.Attributes("class") = "btn btn-info"

                    yearLoad_List()
                    student_yearload_List()
                    student_semload_List()
                    subject_campusLoad_List()
                    subject_stream_list()

                    Page_Load_Transfer()

                    strRet = BindDataTransfer(TransferRespondent)

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewCourse.Visible = False
        btnRegisterCourse.Visible = False
        btnTransferCourse.Visible = False
        ViewCourse.Visible = False
        RegisterCourse.Visible = False
        TransferCourse.Visible = False
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

        Dim Get_ViewCourse As String = ""
        Dim Get_RegisterCourse As String = ""
        Dim Get_TransferCourse As String = ""

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
                btnViewCourse.Visible = True
                ViewCourse.Visible = True

                Get_ViewCourse = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Course" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterCourse.Visible = True
                RegisterCourse.Visible = True

                Get_RegisterCourse = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnUpdate.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Transfer Course" And find_Data_SubMenu2.Length > 0 Then
                btnTransferCourse.Visible = True
                TransferCourse.Visible = True

                Get_TransferCourse = "TRUE"

                If find_Data_F1Transfer.Length > 0 And find_Data_F1Transfer = "TRUE" Then
                    btnButtonTransfer.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewCourse.Visible = True
                btnRegisterCourse.Visible = True
                btnTransferCourse.Visible = True
                ViewCourse.Visible = True
                RegisterCourse.Visible = True
                TransferCourse.Visible = True
                btnUpdate.Visible = True
                btnButtonTransfer.Visible = True

                Get_ViewCourse = "TRUE"
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
            If Get_ViewCourse = "TRUE" Then
                Data_If_Not_Group_Status = "VC"
            ElseIf Get_RegisterCourse = "TRUE" Then
                Data_If_Not_Group_Status = "RC"
            ElseIf Get_TransferCourse = "TRUE" Then
                Data_If_Not_Group_Status = "TC"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewCourse = "TRUE" And Data_If_Not_Group_Status = "VC" Then
                Session("getStatus") = "VC"
            ElseIf Get_RegisterCourse = "TRUE" And Data_If_Not_Group_Status = "RC" Then
                Session("getStatus") = "RC"
            ElseIf Get_TransferCourse = "TRUE" And Data_If_Not_Group_Status = "TC" Then
                Session("getStatus") = "TC"
            End If
        End If

    End Sub

    Private Sub btnRegisterCourse_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterCourse.ServerClick
        Session("getStatus") = "RC"
        Response.Redirect("admin_pengurusan_am_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewCourse_ServerClick(sender As Object, e As EventArgs) Handles btnViewCourse.ServerClick
        Session("getStatus") = "VC"
        Response.Redirect("admin_pengurusan_am_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnTransferCourse_ServerClick(sender As Object, e As EventArgs) Handles btnTransferCourse.ServerClick
        Session("getStatus") = "TC"
        Response.Redirect("admin_pengurusan_am_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub Page_Load()
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
                filterYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
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
                gvTable.Columns(7).Visible = True
            Else
                gvTable.Columns(7).Visible = False
            End If

            If Session("getDeleteButton") = "TRUE" Then
                gvTable.Columns(8).Visible = True
            Else
                gvTable.Columns(8).Visible = False
            End If

            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Sub year_load()
        Try

            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Year'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterYear.DataSource = levds
            filterYear.DataValueField = "Parameter"
            filterYear.DataTextField = "Parameter"
            filterYear.DataBind()
            filterYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            filterYear.SelectedIndex = 0

            ddlsubject_year.DataSource = levds
            ddlsubject_year.DataTextField = "Parameter"
            ddlsubject_year.DataValueField = "Parameter"
            ddlsubject_year.DataBind()
            ddlsubject_year.Items.Insert(0, New ListItem("Select Year", "NULL"))
            ddlsubject_year.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_year_load()
        Try

            Dim strLevelSql As String = "Select * from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterFoundationLvl.DataSource = levds
            filterFoundationLvl.DataValueField = "Parameter"
            filterFoundationLvl.DataTextField = "Parameter"
            filterFoundationLvl.DataBind()
            filterFoundationLvl.Items.Insert(0, New ListItem("Select Level", String.Empty))
            filterFoundationLvl.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_sem_load()
        Try

            Dim strLevelSql As String = "Select * from setting where Type = 'Sem'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterSems.DataSource = levds
            filterSems.DataValueField = "Value"
            filterSems.DataTextField = "Parameter"
            filterSems.DataBind()
            filterSems.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            filterSems.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subject_type_load()
        Try
            Dim strLevelSql As String = "select Parameter from setting where Type = 'Subject Type'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            subject_type.DataSource = levds
            subject_type.DataTextField = "Parameter"
            subject_type.DataValueField = "Parameter"
            subject_type.DataBind()
            subject_type.Items.Insert(0, New ListItem("Select Course Type", String.Empty))
            subject_type.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subject_campus_load()
        Try
            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterCampus.DataSource = levds
            filterCampus.DataValueField = "Value"
            filterCampus.DataTextField = "Parameter"
            filterCampus.DataBind()
            filterCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
            filterCampus.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub course_program_load()
        Try
            If filterCampus.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterStream.DataSource = levds
            filterStream.DataValueField = "Value"
            filterStream.DataTextField = "Parameter"
            filterStream.DataBind()
            filterStream.Items.Insert(0, New ListItem("Select Program", String.Empty))
            filterStream.SelectedIndex = 0

        Catch ex As Exception

        End Try
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
            Dlt_ClassData.SelectCommand.CommandText = "delete subject_info where subject_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_StudentYear, subject_type, subject_Name, subject_sem ASC"

        tmpSQL = "Select * From subject_info"
        strWhere = " WHERE subject_ID IS NOT NULL AND subject_Name is not null and course_Program = '" & filterStream.SelectedValue & "' and subject_Campus = '" & filterCampus.SelectedValue & "'"

        ''--debug
        If filterSems.SelectedIndex > 0 Then
            strWhere += " AND subject_sem = '" & filterSems.SelectedValue & "'"
        End If

        If filterFoundationLvl.SelectedIndex > 0 Then
            strWhere += " AND subject_StudentYear = '" & filterFoundationLvl.SelectedValue & "'"
        End If

        If filterYear.SelectedIndex > 0 Then
            strWhere += " AND subject_year = '" & filterYear.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyCode As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_kursus_data.aspx?subject_ID=" + strKeyCode + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub filterSems_Changed(sender As Object, e As EventArgs) Handles filterSems.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterFoundationLvl_Changed(sender As Object, e As EventArgs) Handles filterFoundationLvl.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterCampus_Changed(sender As Object, e As EventArgs) Handles filterCampus.SelectedIndexChanged
        Try
            course_program_load()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterYear_Changed(sender As Object, e As EventArgs) Handles filterYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterStream_Changed(sender As Object, e As EventArgs) Handles filterStream.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
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

    Private Sub subject_StudentYear_Load()
        strSQL = "select Parameter from setting where Type = 'Level'"
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

    Private Sub student_campus_load()
        strSQL = "select Parameter, Value from setting where idx = 'Pusat Campus'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        ddlCampus.DataSource = ds
        ddlCampus.DataTextField = "Parameter"
        ddlCampus.DataValueField = "Value"
        ddlCampus.DataBind()
        ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
        ddlCampus.SelectedIndex = 0
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

    Private Sub subject_stream_Load()
        strSQL = "select * from setting where Type = 'Stream'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        ddlStream.DataSource = ds
        ddlStream.DataTextField = "Parameter"
        ddlStream.DataValueField = "Value"
        ddlStream.DataBind()
        ddlStream.Items.Insert(0, New ListItem("Select Course Program", "NULL"))
        ddlStream.SelectedIndex = 0
    End Sub

    Private Sub Load_Page()
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

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Dim errorCount As Integer = 0

        If Checking_RegisterCourse() = False Then
            Exit Sub
        End If

        Using STDDATA As New SqlCommand("   INSERT INTO subject_info(subject_Name,subject_NameBM,subject_code,subject_year,subject_type,subject_StudentYear,subject_sem,subject_religions,course_Name,subject_CreditHour,course_Program,subject_Campus) 
                                            values ('" & subject_Name.Text & "','" & subject_NameBM.Text & "','" & subject_code.Text & "','" & ddlsubject_year.SelectedValue & "','" & subject_type.SelectedValue & "',
                                            '" & subject_StudentYear.SelectedValue & "','" & subject_sem.SelectedValue & "','" & ddl_subjectreligions.SelectedValue & "','" & ddlCourse_group.SelectedValue & "','" & subject_CreditHour.Text & "','" & ddlStream.SelectedValue & "','" & ddlCampus.SelectedValue & "')", objConn)
            objConn.Open()
            Dim i = STDDATA.ExecuteNonQuery()
            objConn.Close()

            If i <> 0 Then
                ShowMessage("Successful Add New Course", MessageType.Success)
            Else
                ShowMessage("Unsuccessful Add New Course", MessageType.Error)
            End If
        End Using
    End Sub

    Private Function Checking_RegisterCourse() As Boolean

        If subject_Name.Text.Length = 0 And Regex.IsMatch(subject_Name.Text, "^[A-Za-z0-9 ]+$") Then
            ShowMessage("Please Fill In Course Name (English)", MessageType.Error)
            Return False
        End If

        If subject_NameBM.Text.Length = 0 And Regex.IsMatch(subject_NameBM.Text, "^[A-Za-z0-9 ]+$") Then
            ShowMessage("Please Fill In Course Name (Malay)", MessageType.Error)
            Return False
        End If

        If subject_code.Text.Length = 0 Then
            ShowMessage("Please Fill In Course Code)", MessageType.Error)
            Return False
        End If

        If subject_CreditHour.Text.Length = 0 Then
            ShowMessage("Please Fill In Course Credit Hour)", MessageType.Error)
            Return False
        End If

        If ddlsubject_year.SelectedIndex = 0 Then
            ShowMessage("Please Select Course Year", MessageType.Error)
            Return False
        End If

        If subject_StudentYear.SelectedIndex = 0 Then
            ShowMessage("Please Select Course Level", MessageType.Error)
            Return False
        End If

        If subject_sem.SelectedIndex = 0 Then
            ShowMessage("Please Select Course Semester", MessageType.Error)
            Return False
        End If

        If ddl_subjectreligions.SelectedIndex = 0 Then
            ShowMessage("Please Select Course Religion", MessageType.Error)
            Return False
        End If

        If subject_type.SelectedIndex = 0 Then
            ShowMessage("Please Select Course Type", MessageType.Error)
            Return False
        End If

        If ddlCourse_group.SelectedIndex = 0 Then
            ShowMessage("Please Select Course Group", MessageType.Error)
            Return False
        End If

        If ddlStream.SelectedIndex = 0 Then
            ShowMessage("Please Select Course Program", MessageType.Error)
            Return False
        End If

        If ddlCampus.SelectedIndex = 0 Then
            ShowMessage("Please Select Institutions", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

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

    Private Sub subject_campusLoad_List()
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

    Private Sub subject_stream_list()
        Try
            If ddl_Campus.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Stream.DataSource = levds
            ddl_Stream.DataValueField = "Value"
            ddl_Stream.DataTextField = "Parameter"
            ddl_Stream.DataBind()
            ddl_Stream.Items.Insert(0, New ListItem("Select Course Program", String.Empty))
            ddl_Stream.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Load_Transfer()
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
        Dim strOrderby As String = " ORDER BY subject_StudentYear, subject_type, subject_Name, subject_sem ASC"

        tmpSQL = "Select * From subject_info"
        strWhere = " WHERE subject_ID IS NOT NULL AND subject_Name is not null and course_Program = '" & ddl_Stream.SelectedValue & "' and subject_Campus = '" & ddl_Campus.SelectedValue & "'"

        If ddl_Sem.SelectedIndex > 0 Then
            strWhere += " AND subject_sem = '" & ddl_Sem.SelectedValue & "'"
        End If

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " AND subject_StudentYear = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_Year.SelectedIndex > 0 Then
            strWhere += " AND subject_year = '" & ddl_Year.SelectedValue & "'"
        End If

        getSQLTransfer = tmpSQL & strWhere & strOrderby
        Return getSQLTransfer
    End Function

    Private Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            strRet = BindDataTransfer(TransferRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddl_Sem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Sem.SelectedIndexChanged
        Try
            strRet = BindDataTransfer(TransferRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddl_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Campus.SelectedIndexChanged
        Try
            subject_stream_list()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindDataTransfer(TransferRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddl_Stream_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Stream.SelectedIndexChanged
        Try
            strRet = BindDataTransfer(TransferRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnButtonTransfer_ServerClick(sender As Object, e As EventArgs) Handles btnButtonTransfer.ServerClick

        Dim i As Integer
        Dim errorCount As Integer = 0

        If Checking_TransferCourse() = False Then
            Exit Sub
        End If

        For i = 0 To TransferRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(TransferRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = TransferRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    ''Select The Current Year Subject Code
                    Dim get_code As String = "select subject_code from subject_info where subject_ID = '" & strKey & "'"
                    Dim check_code As String = oCommon.getFieldValue(get_code)

                    ''Select The Next Year Subject Id
                    Dim find_subjectID As String = "select subject_ID from subject_info where subject_code = '" & check_code & "' and subject_year = '" & ddlyear_Transfer.SelectedValue & "'"
                    Dim check_subjectID As String = oCommon.getFieldValue(find_subjectID)

                    ''Get Current Year Subject Information
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
                    Dim find_courseProgram As String = "select course_Program from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_courseProgram As String = oCommon.getFieldValue(find_courseProgram)
                    Dim find_courseCampus As String = "select subject_Campus from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_courseCampus As String = oCommon.getFieldValue(find_courseCampus)

                    If check_subjectID.Length = 0 Then

                        If get_subjectType <> "Choose" Then

                            Using PJGDATA As New SqlCommand("   INSERT INTO subject_info(subject_Name, subject_NameBM, subject_code, subject_year, subject_type, subject_religions, subject_StudentYear, subject_CreditHour, subject_sem, course_Name, course_Program, subject_Campus)
                                                                Values('" & get_subjectName & "','" & get_subjectNameBM & "','" & check_code & "','" & ddlyear_Transfer.SelectedValue & "','" & get_subjectType & "','" & get_subjectReligions & "',
                                                                '" & get_subjectLevel & "','" & Integer.Parse(get_subjectHour) & "','" & get_subjectSem & "','" & get_subjectCourseName & "','" & get_courseProgram & "','" & get_courseCampus & "')", objConn)
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

                            If get_subjectLevel = "Foundation 1" Then
                                get_subjectLevel = "Foundation 2"
                            ElseIf get_subjectLevel = "Foundation 2" Then
                                get_subjectLevel = "Foundation 3"
                            ElseIf get_subjectLevel = "Foundation 3" Then
                                get_subjectLevel = "Level 1"
                            ElseIf get_subjectLevel = "Level 1" Then
                                get_subjectLevel = "Level 2"
                            End If

                            Using PJGDATA As New SqlCommand("   INSERT INTO subject_info(subject_Name, subject_NameBM, subject_code, subject_year, subject_type, subject_religions, subject_StudentYear, subject_CreditHour, subject_sem, course_Name, course_Program)
                                                                Values('" & get_subjectName & "','" & get_subjectNameBM & "','" & check_code & "','" & ddlyear_Transfer.SelectedValue & "','" & get_subjectType & "','" & get_subjectReligions & "',
                                                                '" & get_subjectLevel & "','" & Integer.Parse(get_subjectHour) & "','" & get_subjectSem & "','" & get_subjectCourseName & "','" & get_courseProgram & "')", objConn)
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

    Private Function Checking_TransferCourse() As Boolean

        If ddl_Year.SelectedIndex = 0 Then
            ShowMessage("Please Select Year", MessageType.Error)
            Return False
        End If

        If ddl_Campus.SelectedIndex = 0 Then
            ShowMessage("Please Select Institutions", MessageType.Error)
            Return False
        End If

        If ddl_Stream.SelectedIndex = 0 Then
            ShowMessage("Please Select Program", MessageType.Error)
            Return False
        End If

        If ddl_Level.SelectedIndex = 0 Then
            ShowMessage("Please Select Level", MessageType.Error)
            Return False
        End If

        If ddl_Sem.SelectedIndex = 0 Then
            ShowMessage("Please Select Semester", MessageType.Error)
            Return False
        End If

        If ddlyear_Transfer.SelectedIndex = 0 Then
            ShowMessage("Please Select Transfer Year", MessageType.Error)
            Return False
        End If

        Return True
    End Function

End Class