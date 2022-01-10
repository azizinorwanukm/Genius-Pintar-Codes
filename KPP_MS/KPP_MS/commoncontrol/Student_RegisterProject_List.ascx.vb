Imports System.Data.SqlClient

Public Class Student_RegisterProject_List
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

                If Session("getStatus") = "RP" Then ''Register Student Project
                    txtbreadcrum1.Text = "Register Student Project"

                    RegisterStudentProject.Visible = True
                    ViewStudentProject.Visible = False

                    btnRegisterStudentProject.Attributes("class") = "btn btn-info"
                    btnViewStudentProject.Attributes("class") = "btn btn-default font"

                    year_list_info()

                    load_page()

                    group_list_info()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "VP" Then ''View Student Project
                    txtbreadcrum1.Text = "View Student Project"

                    RegisterStudentProject.Visible = False
                    ViewStudentProject.Visible = True

                    btnRegisterStudentProject.Attributes("class") = "btn btn-default font"
                    btnViewStudentProject.Attributes("class") = "btn btn-info"

                    year_list_info()

                    load_page()

                    group_list_info()

                    strRet = ViewBindData(viewRespondent)

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewStudentProject.Visible = False
        btnRegisterStudentProject.Visible = False
        ViewStudentProject.Visible = False
        RegisterStudentProject.Visible = False

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

            If find_Data_SubMenu2 = "View Student Project" And find_Data_SubMenu2.Length > 0 Then
                btnViewStudentProject.Visible = True
                ViewStudentProject.Visible = True

                Get_ViewStudent = "TRUE"

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Student Project" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterStudentProject.Visible = True
                RegisterStudentProject.Visible = True

                Get_RegisterStudent = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnAddStudent.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewStudentProject.Visible = True
                btnRegisterStudentProject.Visible = True
                ViewStudentProject.Visible = True
                RegisterStudentProject.Visible = True

                btnAddStudent.Visible = True

                Get_ViewStudent = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RP" Or Session("getStatus") = "VP" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RP" And Session("getStatus") <> "VP" Then
            If Get_ViewStudent = "TRUE" Then
                Data_If_Not_Group_Status = "VP"
            ElseIf Get_RegisterStudent = "TRUE" Then
                Data_If_Not_Group_Status = "RP"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewStudent = "TRUE" And Data_If_Not_Group_Status = "VP" Then
                Session("getStatus") = "VP"
            ElseIf Get_RegisterStudent = "TRUE" And Data_If_Not_Group_Status = "RP" Then
                Session("getStatus") = "RP"
            End If
        End If

    End Sub

    Private Sub btnRegisterStudentProject_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterStudentProject.ServerClick
        Session("getStatus") = "RP"
        Response.Redirect("admin_daftarPenyelidikanProjek.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewStudentProjectt_ServerClick(sender As Object, e As EventArgs) Handles btnViewStudentProject.ServerClick
        Session("getStatus") = "VP"
        Response.Redirect("admin_daftarPenyelidikanProjek.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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

            ddlViewYear.DataSource = ds
            ddlViewYear.DataTextField = "Parameter"
            ddlViewYear.DataValueField = "Parameter"
            ddlViewYear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub group_list_info()
        strSQL = "SELECT distinct ri_groupname from research_info where (ri_year = '" & ddl_year.SelectedValue & "' or ri_year = '" & ddlViewYear.SelectedValue & "')"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_group.DataSource = ds
            ddl_group.DataTextField = "ri_groupname"
            ddl_group.DataValueField = "ri_groupname"
            ddl_group.DataBind()
            ddl_group.Items.Insert(0, New ListItem("Select Group", String.Empty))
            ddl_group.SelectedIndex = 0

            ddlViewGroup.DataSource = ds
            ddlViewGroup.DataTextField = "ri_groupname"
            ddlViewGroup.DataValueField = "ri_groupname"
            ddlViewGroup.DataBind()
            ddlViewGroup.Items.Insert(0, New ListItem("Select Group", String.Empty))
            ddlViewGroup.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT * from setting where Type = 'Year' and Parameter ='" & Now.Year & "'"

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
                ddlViewYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_year.SelectedValue = Now.Year
                ddlViewYear.SelectedValue = Now.Year
            End If
        End If
    End Sub

    Protected Sub ddl_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            group_list_info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlViewYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewYear.SelectedIndexChanged
        Try
            group_list_info()
            strRet = ViewBindData(viewRespondent)
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
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrderby As String = " order by B.class_Name, A.student_Name asc"

        tmpSQL = "  select distinct C.ri_id, A.student_Name, A.student_ID, B.class_Name, C.ri_groupname, D.staff_Name from student_info A
                    left join course E on E.std_ID = A.std_ID
                    left join class_info B on B.class_ID = E.class_ID
                    left join research_info C on A.std_ID = C.std_ID
                    left join staff_info D on D.stf_ID = C.stf_id
                    where E.year = '" & ddl_year.SelectedValue & "' and B.class_year = '" & ddl_year.SelectedValue & "' and C.ri_year = '" & ddl_year.SelectedValue & "'
                    and (A.student_Status = 'Access' or A.student_Status = 'Graduate') and A.student_ID is not null and A.student_ID <> '' and A.student_ID like '%M%'
                    and D.staff_Status = 'Access' and D.staff_Name not like '%araken%'
                    and B.class_type = 'Compulsory'"

        If ddl_group.SelectedIndex > 0 Then
            strWhere += " and C.ri_groupname = '" & ddl_group.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Protected Sub ddl_group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_group.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlViewGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewGroup.SelectedIndexChanged
        Try
            strRet = ViewBindData(viewRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAddStudent_ServerClick(sender As Object, e As EventArgs) Handles btnAddStudent.ServerClick

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

            If txt_project.Text.Length > 0 Or txt_field.Text.Length > 0 Then

                Dim researchname As String = oCommon.FixSingleQuotes(txt_project.Text)
                Dim researchfiled As String = oCommon.FixSingleQuotes(txt_field.Text)

                ''update grades and gpa
                strSQL = "UPDATE research_info set ri_researchname = '" & txt_project.Text & "', ri_researchfiled = '" & txt_field.Text & "' where ri_id = '" & strKeyID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
        Next

        If strRet = "0" Then
            ShowMessage("Register student project", MessageType.Success)
        Else
            ShowMessage("Register student project", MessageType.Error)
        End If

        strRet = BindData(datRespondent)

    End Sub

    Private Function ViewBindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLView, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            If Session("getDeleteButton") = "TRUE" Then
                gvTable.Columns(5).Visible = True
            Else
                gvTable.Columns(5).Visible = False
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
        Dim strOrderby As String = "  order by research_info.ri_groupname, student_info.student_Name ASC"

        tmpSQL = "  select research_info.ri_id, research_info.ri_year, student_info.student_name, research_info.ri_groupname, research_info.ri_researchname, 
                    research_info.ri_researchfiled
                    from research_info
                    left join student_info on research_info.std_id = student_info.std_ID"

        strWhere = " where (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and research_info.ri_year = '" & ddlViewYear.SelectedValue & "'"

        If ddlViewGroup.SelectedIndex > 0 Then
            strWhere += " and research_info.ri_groupname = '" & ddlViewGroup.SelectedValue & "'"
        End If

        getSQLView = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQLView
    End Function

    Private Sub viewRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles viewRespondent.RowDeleting
        Try
            Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

            strSQL = "Update research_info set ri_researchname = '', ri_researchfiled = '' where ri_id = '" & strKeyName & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage("Successful Delete Data", MessageType.Success)
            Else
                ShowMessage("Unsuccessful Delete Data", MessageType.Error)
            End If

            strRet = ViewBindData(viewRespondent)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class