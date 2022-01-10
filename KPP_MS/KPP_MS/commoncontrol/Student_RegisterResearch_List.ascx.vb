Imports System.Data.SqlClient

Public Class Student_RegisterResearch_List
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "RS" Then ''Register Student Group & Supervisor
                    txtbreadcrum1.Text = "Register Student Group & Supervisor"

                    RegisterResearchStudent.Visible = True
                    ViewResearchStundet.Visible = False

                    btnRegisterResearchStudent.Attributes("class") = "btn btn-info"
                    btnViewResearchStundet.Attributes("class") = "btn btn-default font"

                    year_list_info()
                    level_list_info()
                    class_list_info()
                    staff_list_info()

                    load_page()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "VS" Then ''View Student Group & Supervisor
                    txtbreadcrum1.Text = "View Student Group & Supervisor"

                    RegisterResearchStudent.Visible = False
                    ViewResearchStundet.Visible = True

                    btnRegisterResearchStudent.Attributes("class") = "btn btn-default font"
                    btnViewResearchStundet.Attributes("class") = "btn btn-info"

                    year_view_info()
                    group_view_info()
                    staff_view_info()

                    load_page()

                    strRet = BindDataView(viewRespondent)
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewResearchStundet.Visible = False
        btnRegisterResearchStudent.Visible = False
        ViewResearchStundet.Visible = False
        RegisterResearchStudent.Visible = False

        btnAddStudent.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewStudent As String = ""
        Dim Get_RegisterStudent As String = ""

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

            If find_Data_SubMenu2 = "View Student Group & Supervisor" And find_Data_SubMenu2.Length > 0 Then
                btnViewResearchStundet.Visible = True
                ViewResearchStundet.Visible = True

                Get_ViewStudent = "TRUE"

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Student Group & Supervisor" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterResearchStudent.Visible = True
                RegisterResearchStudent.Visible = True

                Get_RegisterStudent = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnAddStudent.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewResearchStundet.Visible = True
                btnRegisterResearchStudent.Visible = True
                ViewResearchStundet.Visible = True
                RegisterResearchStudent.Visible = True

                btnAddStudent.Visible = True

                Get_ViewStudent = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RS" Or Session("getStatus") = "VS" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RS" And Session("getStatus") <> "VS" Then
            If Get_ViewStudent = "TRUE" Then
                Data_If_Not_Group_Status = "VS"
            ElseIf Get_RegisterStudent = "TRUE" Then
                Data_If_Not_Group_Status = "RS"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewStudent = "TRUE" And Data_If_Not_Group_Status = "VS" Then
                Session("getStatus") = "VS"
            ElseIf Get_RegisterStudent = "TRUE" And Data_If_Not_Group_Status = "RS" Then
                Session("getStatus") = "RS"
            End If
        End If

    End Sub

    Private Sub btnRegisterResearchStudent_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterResearchStudent.ServerClick
        Session("getStatus") = "RS"
        Response.Redirect("admin_daftarPenyelidikanPelajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewResearchStundet_ServerClick(sender As Object, e As EventArgs) Handles btnViewResearchStundet.ServerClick
        Session("getStatus") = "VS"
        Response.Redirect("admin_daftarPenyelidikanPelajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub year_list_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_year.DataSource = ds
            ddl_year.DataTextField = "Parameter"
            ddl_year.DataValueField = "Parameter"
            ddl_year.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_view_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_ViewYear.DataSource = ds
            ddl_ViewYear.DataTextField = "Parameter"
            ddl_ViewYear.DataValueField = "Parameter"
            ddl_ViewYear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub level_list_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_level.DataSource = ds
            ddl_level.DataTextField = "Parameter"
            ddl_level.DataValueField = "Parameter"
            ddl_level.DataBind()
            ddl_level.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub group_view_info()
        strSQL = "SELECT distinct ri_groupname from research_info where ri_year = '" & ddl_ViewYear.SelectedValue & "' order by ri_groupname asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_ViewGroup.DataSource = ds
            ddl_ViewGroup.DataTextField = "ri_groupname"
            ddl_ViewGroup.DataValueField = "ri_groupname"
            ddl_ViewGroup.DataBind()
            ddl_ViewGroup.Items.Insert(0, New ListItem("Select Group", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_list_info()
        strSQL = "select * from class_info where class_year = '" & ddl_year.SelectedValue & "' and class_Level = '" & ddl_level.SelectedValue & "' and class_type = 'Compulsory' order by class_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_class.DataSource = ds
            ddl_class.DataTextField = "class_Name"
            ddl_class.DataValueField = "class_ID"
            ddl_class.DataBind()
            ddl_class.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staff_view_info()
        strSQL = "  select distinct A.stf_ID, A.staff_Name from staff_info A
                    left join research_info B on B.stf_ID = A.stf_ID
                    where B.ri_year = '" & ddl_ViewYear.SelectedValue & "' and A.staff_Status = 'Access'
                    order by A.staff_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_ViewStaff.DataSource = ds
            ddl_ViewStaff.DataTextField = "staff_Name"
            ddl_ViewStaff.DataValueField = "stf_ID"
            ddl_ViewStaff.DataBind()
            ddl_ViewStaff.Items.Insert(0, New ListItem("Select Staff", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staff_list_info()
        strSQL = "select stf_ID,staff_Name from staff_info where staff_Status = 'Access' and staff_Name not like '%araken%' and staff_ID is not null order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_staff.DataSource = ds
            ddl_staff.DataTextField = "staff_Name"
            ddl_staff.DataValueField = "stf_ID"
            ddl_staff.DataBind()
            ddl_staff.Items.Insert(0, New ListItem("Select Staff", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter from setting where Type = 'Year' and Parameter ='" & Now.Year & "'"

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
                ddl_year.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
                ddl_ViewYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_year.SelectedIndex = 0
                ddl_ViewYear.SelectedIndex = 0
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
        Dim strOrderby As String = " order by class_info.class_Level, class_info.class_Name, student_info.student_Name asc"

        tmpSQL = "  select distinct student_info.std_ID, student_info.student_ID, student_info.student_Name, student_info.student_Mykad, class_info.class_level, class_info.class_Name from student_info
                    left join course on student_info.std_ID = course.std_Id
                    left join class_info on course.class_Id = class_info.class_ID"
        strWhere = " where (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and class_info.class_type = 'Compulsory'"
        strWhere += " and course.year = '" & ddl_year.SelectedValue & "' and class_info.class_year = '" & ddl_year.SelectedValue & "'"

        If ddl_class.SelectedIndex > 0 Then
            strWhere += " and class_info.class_ID = '" & ddl_class.SelectedValue & "'"
        End If

        If ddl_level.SelectedIndex > 0 Then
            strWhere += " and class_info.class_Level = '" & ddl_level.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Function BindDataView(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLView, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            If Session("getDeleteButton") = "TRUE" Then
                gvTable.Columns(6).Visible = True
            Else
                gvTable.Columns(6).Visible = False
            End If

            objConn.Close()

        Catch ex As Exception

            Return False
        End Try
        Return True
    End Function

    Private Function getSQLView() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by B.class_Name, A.student_Name asc"

        tmpSQL = "  select distinct C.ri_id, A.student_Name, A.student_ID, B.class_Name, C.ri_groupname, D.staff_Name from student_info A
                    left join course E on E.std_ID = A.std_ID
                    left join class_info B on B.class_ID = E.class_ID
                    left join research_info C on A.std_ID = C.std_ID
                    left join staff_info D on D.stf_ID = C.stf_id
                    where E.year = '" & ddl_ViewYear.SelectedValue & "' and B.class_year = '" & ddl_ViewYear.SelectedValue & "' and C.ri_year = '" & ddl_ViewYear.SelectedValue & "'
                    and (A.student_Status = 'Access' or A.student_Status = 'Graduate')  and A.student_ID is not null and A.student_ID <> '' and A.student_ID like '%M%'
                    and D.staff_Status = 'Access' and D.staff_Name not like '%araken%'
                    and B.class_type = 'Compulsory'"

        If ddl_ViewGroup.SelectedIndex > 0 Then
            strWhere += " and C.ri_groupname = '" & ddl_ViewGroup.SelectedValue & "'"
        End If

        If ddl_ViewStaff.SelectedIndex > 0 Then
            strWhere += " and C.stf_ID = '" & ddl_ViewStaff.SelectedValue & "'"
        End If

        getSQLView = tmpSQL & strWhere & strOrderby

        Return getSQLView
    End Function

    Private Sub viewRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles viewRespondent.RowDeleting
        Try
            Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

            strSQL = "Delete research_info where ri_id = '" & strKeyName & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage("Successful Delete Data", MessageType.Success)
            Else
                ShowMessage("Unsuccessful Delete Data", MessageType.Error)
            End If

            strRet = BindDataView(viewRespondent)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            class_list_info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged
        Try
            class_list_info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_class.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_ViewYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_ViewYear.SelectedIndexChanged
        Try
            group_view_info()
            staff_view_info()
            strRet = BindDataView(viewRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_ViewGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_ViewGroup.SelectedIndexChanged
        Try
            strRet = BindDataView(viewRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_ViewStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_ViewStaff.SelectedIndexChanged
        Try
            strRet = BindDataView(viewRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAddStudent_ServerClick(sender As Object, e As EventArgs) Handles btnAddStudent.ServerClick
        Dim i As Integer = 0
        Dim value As String = ""

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim checkResearchStudentGroup As String = "select ri_id from research_info where std_ID = '" & strKey & "' and ri_year = '" & ddl_year.SelectedValue & "' "
                    strRet = oCommon.getFieldValue(checkResearchStudentGroup)

                    If strRet.Length > 0 Then

                        strSQL = "Update research_info set std_ID = '" & strKey & "', ri_year = '" & ddl_year.SelectedValue & "', ri_groupname = '" & txt_group.Text & "', stf_id = '" & ddl_staff.SelectedValue & "' where ri_id = '" & strRet & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        strSQL = "insert into research_info(std_id,ri_year,ri_groupname,stf_id) values('" & strKey & "','" & ddl_year.SelectedValue & "','" & txt_group.Text & "','" & ddl_staff.SelectedValue & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                End If
            End If
        Next

        If strRet = "0" Then
            ShowMessage("Register student group", MessageType.Success)
        Else
            ShowMessage("Register student group", MessageType.Error)
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