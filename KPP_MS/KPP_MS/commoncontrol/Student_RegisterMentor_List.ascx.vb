Imports System.Data.SqlClient

Public Class Student_RegisterMentor_List
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim hide As Integer = 0

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "RM" Then ''Register Student Mentor
                    txtbreadcrum1.Text = "Register Student Mentor"

                    RegisterStudentMentor.Visible = True
                    ViewStudentMentor.Visible = False

                    btnRegisterStudentMentor.Attributes("class") = "btn btn-info"
                    btnViewStudentMentor.Attributes("class") = "btn btn-default font"

                    year_list_info()
                    load_page()
                    group_list_info()

                ElseIf Session("getStatus") = "VM" Then ''View Student Mentor
                    txtbreadcrum1.Text = "View Student Mentor"

                    RegisterStudentMentor.Visible = False
                    ViewStudentMentor.Visible = True

                    btnRegisterStudentMentor.Attributes("class") = "btn btn-default font"
                    btnViewStudentMentor.Attributes("class") = "btn btn-info"

                    year_list_info()
                    load_page()
                    group_list_info()

                    strRet = BindData(datRespondent)
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewStudentMentor.Visible = False
        btnRegisterStudentMentor.Visible = False
        ViewStudentMentor.Visible = False
        RegisterStudentMentor.Visible = False

        btnAdd.Visible = False

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

            If find_Data_SubMenu2 = "View Student Mentor" And find_Data_SubMenu2.Length > 0 Then
                btnViewStudentMentor.Visible = True
                ViewStudentMentor.Visible = True

                Get_ViewStudent = "TRUE"

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Student Mentor" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterStudentMentor.Visible = True
                RegisterStudentMentor.Visible = True

                Get_RegisterStudent = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnAdd.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewStudentMentor.Visible = True
                btnRegisterStudentMentor.Visible = True
                ViewStudentMentor.Visible = True
                RegisterStudentMentor.Visible = True

                btnAdd.Visible = True

                Get_ViewStudent = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RM" Or Session("getStatus") = "VM" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RM" And Session("getStatus") <> "VM" Then
            If Get_ViewStudent = "TRUE" Then
                Data_If_Not_Group_Status = "VM"
            ElseIf Get_RegisterStudent = "TRUE" Then
                Data_If_Not_Group_Status = "RM"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewStudent = "TRUE" And Data_If_Not_Group_Status = "VM" Then
                Session("getStatus") = "VM"
            ElseIf Get_RegisterStudent = "TRUE" And Data_If_Not_Group_Status = "RM" Then
                Session("getStatus") = "RM"
            End If
        End If

    End Sub

    Private Sub btnRegisterStudentMentor_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterStudentMentor.ServerClick
        Session("getStatus") = "RM"
        Response.Redirect("admin_daftarPenyelidikanMentor.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewStudenPMentor_ServerClick(sender As Object, e As EventArgs) Handles btnViewStudentMentor.ServerClick
        Session("getStatus") = "VM"
        Response.Redirect("admin_daftarPenyelidikanMentor.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlViewYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewYear.SelectedIndexChanged
        Try
            group_list_info()
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

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = "  order by ri_mentor, ri_comentor ASC"

        tmpSQL = " select distinct ri_id, ri_mentor, ri_mentorfaculty, ri_comentor, ri_comentorfaculty from research_info"

        strWhere = "    where ri_year = '" & ddlViewYear.SelectedValue & "'
                        and ri_mentor is not null and ri_mentorfaculty is not null"

        If ddlViewGroup.SelectedIndex > 0 Then
            strWhere += " and ri_groupname = '" & ddlViewGroup.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Private Sub btnAdd_ServerClick(sender As Object, e As EventArgs) Handles btnAdd.ServerClick

        ''update grades and gpa
        strSQL = "UPDATE research_info set ri_mentor = '" & txtmentor.Text & "', ri_mentorfaculty = '" & txtmentor_faculty.Text & "', ri_comentor = '" & txtcomentor.Text & "', ri_comentorfaculty = '" & txtcomentor_faculty.Text & "' where ri_groupname = '" & ddl_group.SelectedValue & "' and ri_year = '" & ddl_year.SelectedValue & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage("Register student project", MessageType.Success)
        Else
            ShowMessage("Register student project", MessageType.Error)
        End If

    End Sub

    Protected Sub ddl_group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_group.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlViewGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewGroup.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Try
            Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

            strSQL = "UPDATE research_info set ri_mentor = '', ri_mentorfaculty = '', ri_comentor = '', ri_comentorfaculty = '' where ri_id = '" & strKeyName & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage("Successful Delete Data", MessageType.Success)
            Else
                ShowMessage("Unsuccessful Delete Data", MessageType.Error)
            End If

            strRet = BindData(datRespondent)

        Catch ex As Exception
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class