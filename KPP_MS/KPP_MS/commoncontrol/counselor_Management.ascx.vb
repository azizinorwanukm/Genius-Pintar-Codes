Imports System.Data.SqlClient

Public Class counselor_Management
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

                If Session("getStatus") = "VM" Then ''View Management
                    txtbreadcrum1.Text = "View Management"

                    ViewManagement.Visible = True
                    SelfDevelopmentManagement.Visible = False
                    PersonalityDevelopmentManagement.Visible = False

                    btnViewManagement.Attributes("class") = "btn btn-info"
                    btnSelfDevelopmentManagement.Attributes("class") = "btn btn-default font"
                    btnPersonalityDevelopmentManagement.Attributes("class") = "btn btn-default font"

                    VM_year_list()
                    VM_program_list()
                    VM_level_list()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "SDM" Then ''Self Development Management
                    txtbreadcrum1.Text = "Self Development Management"

                    ViewManagement.Visible = False
                    SelfDevelopmentManagement.Visible = True
                    PersonalityDevelopmentManagement.Visible = False

                    btnViewManagement.Attributes("class") = "btn btn-default font"
                    btnSelfDevelopmentManagement.Attributes("class") = "btn btn-info"
                    btnPersonalityDevelopmentManagement.Attributes("class") = "btn btn-default font"

                    year_list()
                    program_list()
                    level_list()
                    name_list()

                ElseIf Session("getStatus") = "PDM" Then ''Personality Development Management
                    txtbreadcrum1.Text = "Personality Development Management"

                    ViewManagement.Visible = False
                    SelfDevelopmentManagement.Visible = False
                    PersonalityDevelopmentManagement.Visible = True

                    btnViewManagement.Attributes("class") = "btn btn-default font"
                    btnSelfDevelopmentManagement.Attributes("class") = "btn btn-default font"
                    btnPersonalityDevelopmentManagement.Attributes("class") = "btn btn-info"

                    year_list()
                    program_list()
                    level_list()
                    name_list()

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewManagement.Visible = False
        btnSelfDevelopmentManagement.Visible = False
        btnPersonalityDevelopmentManagement.Visible = False
        ViewManagement.Visible = False
        SelfDevelopmentManagement.Visible = False
        PersonalityDevelopmentManagement.Visible = False
        btnSDM_Update.Visible = False
        btnPDM_Update.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewManagement As String = ""
        Dim Get_SelfDevelopmentManagement As String = ""
        Dim Get_PersonalityManagement As String = ""

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

            If find_Data_SubMenu2 = "View Management" And find_Data_SubMenu2.Length > 0 Then
                btnViewManagement.Visible = True
                ViewManagement.Visible = True

                Get_ViewManagement = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Self Development Management" And find_Data_SubMenu2.Length > 0 Then
                btnSelfDevelopmentManagement.Visible = True
                SelfDevelopmentManagement.Visible = True

                Get_SelfDevelopmentManagement = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnSDM_Update.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Personality Development Management" And find_Data_SubMenu2.Length > 0 Then
                btnPersonalityDevelopmentManagement.Visible = True
                PersonalityDevelopmentManagement.Visible = True

                Get_PersonalityManagement = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnPDM_Update.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewManagement.Visible = True
                btnSelfDevelopmentManagement.Visible = True
                btnPersonalityDevelopmentManagement.Visible = True
                ViewManagement.Visible = True
                SelfDevelopmentManagement.Visible = True
                PersonalityDevelopmentManagement.Visible = True
                btnSDM_Update.Visible = True
                btnPDM_Update.Visible = True

                Get_ViewManagement = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "VM" Or Session("getStatus") = "SDM" Or Session("getStatus") = "PDM" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "VM" And Session("getStatus") <> "SDM" And Session("getStatus") <> "PDM" Then
            If Get_ViewManagement = "TRUE" Then
                Data_If_Not_Group_Status = "VM"
            ElseIf Get_SelfDevelopmentManagement = "TRUE" Then
                Data_If_Not_Group_Status = "SDM"
            ElseIf Get_PersonalityManagement = "TRUE" Then
                Data_If_Not_Group_Status = "PDM"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewManagement = "TRUE" And Data_If_Not_Group_Status = "VM" Then
                Session("getStatus") = "VM"
            ElseIf Get_SelfDevelopmentManagement = "TRUE" And Data_If_Not_Group_Status = "SDM" Then
                Session("getStatus") = "SDM"
            ElseIf Get_PersonalityManagement = "TRUE" And Data_If_Not_Group_Status = "PDM" Then
                Session("getStatus") = "PDM"
            End If
        End If

    End Sub

    Private Sub btnViewManagement_ServerClick(sender As Object, e As EventArgs) Handles btnViewManagement.ServerClick
        Session("getStatus") = "VM"
        Response.Redirect("admin_kaunselor_pengurusanKaunselor.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnSelfDevelopmentManagement_ServerClick(sender As Object, e As EventArgs) Handles btnSelfDevelopmentManagement.ServerClick
        Session("getStatus") = "SDM"
        Response.Redirect("admin_kaunselor_pengurusanKaunselor.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnPersonalityDevelopmentManagement_ServerClick(sender As Object, e As EventArgs) Handles btnPersonalityDevelopmentManagement.ServerClick
        Session("getStatus") = "PDM"
        Response.Redirect("admin_kaunselor_pengurusanKaunselor.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub VM_year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' order by parameter asc "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Year.DataSource = ds
            ddl_Year.DataTextField = "Parameter"
            ddl_Year.DataValueField = "Parameter"
            ddl_Year.DataBind()
            ddl_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_Year.SelectedIndex = 0
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub VM_program_list()
        Try
            Dim strLevelSql As String = "select Parameter, Value from setting where Type = 'Stream'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Program.DataSource = levds
            ddl_Program.DataValueField = "Value"
            ddl_Program.DataTextField = "Parameter"
            ddl_Program.DataBind()
            ddl_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddl_Program.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VM_level_list()
        Try
            Dim strLevelSql As String = "select Parameter from setting where Type = 'Level'"
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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            If Session("getEditButton") = "TRUE" Then
                gvTable.Columns(6).Visible = True
            Else
                gvTable.Columns(6).Visible = False
            End If

            If Session("getDeleteButton") = "TRUE" Then
                gvTable.Columns(7).Visible = True
            Else
                gvTable.Columns(7).Visible = False
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
        Dim strOrderby As String = " ORDER BY CM_ID ASC"

        tmpSQL = "Select * From counselor_management_rubrick"
        strWhere += " WHERE CM_Year = '" & ddl_Year.SelectedValue & "'"

        If ddl_Program.SelectedIndex > 0 Then
            strWhere += " AND CM_Program = '" & ddl_Program.SelectedValue & "'"
        End If

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " AND CM_Level = '" & ddl_Level.SelectedValue & "'"
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
            Dlt_ClassData.SelectCommand.CommandText = "delete counselor_management_rubrick where CM_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        datRespondent.EditIndex = e.NewEditIndex
        Me.BindData(datRespondent)
    End Sub

    Protected Sub OnRowCancelingEdit(sender As Object, e As EventArgs)
        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim CM_Name As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtCM_Name"), TextBox)
        Dim CM_Percentage As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtCM_Percentage"), TextBox)
        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''update grades
        strSQL = "UPDATE counselor_management_rubrick SET CM_Name='" & CM_Name.Text & "',CM_Percentage='" & CM_Percentage.Text & "' WHERE CM_ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage(" Edit Information ", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Edit Information", MessageType.Error)
        End If

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Program.SelectedIndexChanged
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

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' order by parameter asc "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSDM_Year.DataSource = ds
            ddlSDM_Year.DataTextField = "Parameter"
            ddlSDM_Year.DataValueField = "Parameter"
            ddlSDM_Year.DataBind()
            ddlSDM_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlSDM_Year.SelectedIndex = 0

            ddlPDM_Year.DataSource = ds
            ddlPDM_Year.DataTextField = "Parameter"
            ddlPDM_Year.DataValueField = "Parameter"
            ddlPDM_Year.DataBind()
            ddlPDM_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlPDM_Year.SelectedIndex = 0
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub program_list()
        Try
            Dim strLevelSql As String = "select Parameter, Value from setting where Type = 'Stream'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlSDM_Program.DataSource = levds
            ddlSDM_Program.DataValueField = "Value"
            ddlSDM_Program.DataTextField = "Parameter"
            ddlSDM_Program.DataBind()
            ddlSDM_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddlSDM_Program.SelectedIndex = 0

            ddlPDM_Program.DataSource = levds
            ddlPDM_Program.DataValueField = "Value"
            ddlPDM_Program.DataTextField = "Parameter"
            ddlPDM_Program.DataBind()
            ddlPDM_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddlPDM_Program.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub level_list()
        Try
            Dim strLevelSql As String = "select Parameter from setting where Type = 'Level' and Parameter <> 'level 1' and Parameter <> 'level 2'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlSDM_Level.DataSource = levds
            ddlSDM_Level.DataValueField = "Parameter"
            ddlSDM_Level.DataTextField = "Parameter"
            ddlSDM_Level.DataBind()
            ddlSDM_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlSDM_Level.SelectedIndex = 0

            Dim strLevelSqls As String = "select Parameter from setting where Type = 'Level' and Parameter <> 'Foundation 1' and Parameter <> 'Foundation 2' and Parameter <> 'Foundation 3'"
            Dim sqlLevelDB As New SqlDataAdapter(strLevelSqls, objConn)

            Dim levdt As DataSet = New DataSet
            sqlLevelDB.Fill(levdt, "LevTable")

            ddlPDM_Level.DataSource = levdt
            ddlPDM_Level.DataValueField = "Parameter"
            ddlPDM_Level.DataTextField = "Parameter"
            ddlPDM_Level.DataBind()
            ddlPDM_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlPDM_Level.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Private Sub name_list()
        Try
            Dim strLevelSql As String = "select Parameter from setting where idx = 'Self Development' and Type = '" & ddlSDM_Level.SelectedValue & "'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlSDM_Name.DataSource = levds
            ddlSDM_Name.DataValueField = "Parameter"
            ddlSDM_Name.DataTextField = "Parameter"
            ddlSDM_Name.DataBind()
            ddlSDM_Name.Items.Insert(0, New ListItem("Select Assessed Name", String.Empty))
            ddlSDM_Name.SelectedIndex = 0

            Dim strLevelSqls As String = "select Parameter from setting where idx = 'Personality Development' and Type = '" & ddlPDM_Level.SelectedValue & "'"
            Dim sqlLevelDB As New SqlDataAdapter(strLevelSqls, objConn)

            Dim levdt As DataSet = New DataSet
            sqlLevelDB.Fill(levdt, "LevTable")

            ddlPDM_Name.DataSource = levdt
            ddlPDM_Name.DataValueField = "Parameter"
            ddlPDM_Name.DataTextField = "Parameter"
            ddlPDM_Name.DataBind()
            ddlPDM_Name.Items.Insert(0, New ListItem("Select Assessed Name", String.Empty))
            ddlPDM_Name.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSDM_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSDM_Level.SelectedIndexChanged
        Try
            name_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlPDM_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPDM_Level.SelectedIndexChanged
        Try
            name_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSDM_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSDM_Name.SelectedIndexChanged
        Try
            strSQL = "Select Value from setting where idx = 'Self Development' and Type = '" & ddlSDM_Level.SelectedValue & "' and Parameter = '" & ddlSDM_Name.SelectedValue & "'"
            txtSDMPercentage.Text = oCommon.getFieldValue(strSQL)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlPDM_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPDM_Name.SelectedIndexChanged
        Try
            strSQL = "Select Value from setting where idx = 'Personality Development' and Type = '" & ddlPDM_Level.SelectedValue & "' and Parameter = '" & ddlPDM_Name.SelectedValue & "'"
            txtSDMPercentage.Text = oCommon.getFieldValue(strSQL)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSDM_Update_ServerClick(sender As Object, e As EventArgs) Handles btnSDM_Update.ServerClick

        If ddlSDM_Year.SelectedIndex > 0 Then
            If ddlSDM_Program.SelectedIndex > 0 Then
                If ddlSDM_Level.SelectedIndex > 0 Then
                    If ddlSDM_Name.SelectedIndex > 0 Then
                        If txtSDMPercentage.Text.Length > 0 Then

                            strSQL = "  Insert Into counselor_management_rubrick(CM_Year,CM_Program,CM_Level,CM_Name,CM_Percentage,CM_ManagementType)
                                        Values('" & ddlSDM_Year.SelectedValue & "','" & ddlSDM_Program.SelectedValue & "','" & ddlSDM_Level.SelectedValue & "','" & ddlSDM_Name.SelectedValue & "','" & txtSDMPercentage.Text & "','SDM')"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                ShowMessage(" Add New Self Development Management", MessageType.Success)
                            Else
                                ShowMessage(" Unsuccessful Add New Self Development Management", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Please Enter Percentage Value", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Enter Assessed Name", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Select Level", MessageType.Error)
                End If
            Else
                ShowMessage(" Please Select Program", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Select Year", MessageType.Error)
        End If

    End Sub

    Private Sub btnPDM_Update_ServerClick(sender As Object, e As EventArgs) Handles btnPDM_Update.ServerClick

        If ddlPDM_Year.SelectedIndex > 0 Then
            If ddlPDM_Program.SelectedIndex > 0 Then
                If ddlPDM_Level.SelectedIndex > 0 Then
                    If ddlPDM_Name.SelectedIndex > 0 Then
                        If txtPDMPercentage.Text.Length > 0 Then

                            strSQL = "  Insert Into counselor_management_rubrick(CM_Year,CM_Program,CM_Level,CM_Name,CM_Percentage,CM_ManagementType)
                                        Values('" & ddlPDM_Year.SelectedValue & "','" & ddlPDM_Program.SelectedValue & "','" & ddlPDM_Level.SelectedValue & "','" & ddlPDM_Name.SelectedValue & "','" & txtPDMPercentage.Text & "','PDM')"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                ShowMessage(" Add New Personality Development Management", MessageType.Success)
                            Else
                                ShowMessage(" Unsuccessful Add New Personality Development Management", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Please Enter Percentage Value", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Enter Assessed Name", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Select Level", MessageType.Error)
                End If
            Else
                ShowMessage(" Please Select Program", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Select Year", MessageType.Error)
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