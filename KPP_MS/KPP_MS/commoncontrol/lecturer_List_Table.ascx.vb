Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb

Public Class lecturer_List_Table
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strBil As String = ""
    Dim strStaffID As String = ""
    Dim strStaffName As String = ""
    Dim strStaffMykad As String = ""
    Dim strStaffGender As String = ""
    Dim strStaffEmail As String = ""
    Dim strStaffMobileNo As String = ""
    Dim strStaffTelNo As String = ""
    Dim strStaffLogin As String = ""
    Dim strStaffPasswordP1 As String = ""
    Dim strStaffPasswordP2 As String = ""
    Dim strStaffPasswordP3 As String = ""
    Dim strStaffYear As String = ""
    Dim strStaffPositionP1 As String = ""
    Dim strStaffPositionP2 As String = ""
    Dim strStaffPositionP3 As String = ""
    Dim strStaffAddress As String = ""
    Dim strStaffCity As String = ""
    Dim strStaffPostalCode As String = ""
    Dim strStaffState As String = ""
    Dim strStaffCampus As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "RS" Then ''Register Staff
                    txtbreadcrum1.Text = "Register Staff"

                    RegisterStaff.Visible = True
                    ViewStaff.Visible = False
                    ImportStaff.Visible = False

                    btnRegisterStaff.Attributes("class") = "btn btn-info"
                    btnViewStaff.Attributes("class") = "btn btn-default font"
                    btnImportStaff.Attributes("class") = "btn btn-default font"

                    fillDDL("State", staff_State)
                    accessLevel_List()
                    positionLevel_List()
                    adminLevel_List()
                    campus_List()

                    staff_Photo.ImageUrl = "~/staff_Image/user.png"

                ElseIf Session("getStatus") = "VS" Then ''View Staff
                    txtbreadcrum1.Text = "View Staff"

                    RegisterStaff.Visible = False
                    ViewStaff.Visible = True
                    ImportStaff.Visible = False

                    btnRegisterStaff.Attributes("class") = "btn btn-default font"
                    btnViewStaff.Attributes("class") = "btn btn-info"
                    btnImportStaff.Attributes("class") = "btn btn-default font"

                    txtstaff_data.Text = ""
                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "IS" Then ''Import Staff
                    txtbreadcrum1.Text = "Import Staff"

                    RegisterStaff.Visible = False
                    ViewStaff.Visible = False
                    ImportStaff.Visible = True

                    btnRegisterStaff.Attributes("class") = "btn btn-default font"
                    btnViewStaff.Attributes("class") = "btn btn-default font"
                    btnImportStaff.Attributes("class") = "btn btn-info"

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewStaff.Visible = False
        btnRegisterStaff.Visible = False
        BtnImport.Visible = False
        ViewStaff.Visible = False
        RegisterStaff.Visible = False
        ImportStaff.Visible = False

        Btnsimpan.Visible = False
        BtnImport.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewStaff As String = ""
        Dim Get_RegisterStaff As String = ""
        Dim Get_ImportStaff As String = ""

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

            ''Get Function Button 1 Import Data
            strSQL = "  Select B.F1_Import From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Import As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "View Staff" And find_Data_SubMenu2.Length > 0 Then
                btnViewStaff.Visible = True
                ViewStaff.Visible = True

                Get_ViewStaff = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Staff" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterStaff.Visible = True
                RegisterStaff.Visible = True

                Get_RegisterStaff = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Import Staff" And find_Data_SubMenu2.Length > 0 Then
                btnImportStaff.Visible = True
                ImportStaff.Visible = True

                Get_ImportStaff = "TRUE"

                If find_Data_F1Import.Length > 0 And find_Data_F1Import = "TRUE" Then
                    BtnImport.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewStaff.Visible = True
                btnRegisterStaff.Visible = True
                btnImportStaff.Visible = True
                ViewStaff.Visible = True
                RegisterStaff.Visible = True
                ImportStaff.Visible = True

                Btnsimpan.Visible = True
                BtnImport.Visible = True

                Get_ViewStaff = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RS" Or Session("getStatus") = "VS" Or Session("getStatus") = "IS" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RS" And Session("getStatus") <> "VS" And Session("getStatus") <> "IS" Then
            If Get_ViewStaff = "TRUE" Then
                Data_If_Not_Group_Status = "VS"
            ElseIf Get_RegisterStaff = "TRUE" Then
                Data_If_Not_Group_Status = "RS"
            ElseIf Get_ImportStaff = "TRUE" Then
                Data_If_Not_Group_Status = "IS"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewStaff = "TRUE" And Data_If_Not_Group_Status = "VS" Then
                Session("getStatus") = "VS"
            ElseIf Get_RegisterStaff = "TRUE" And Data_If_Not_Group_Status = "RS" Then
                Session("getStatus") = "RS"
            ElseIf Get_ImportStaff = "TRUE" And Data_If_Not_Group_Status = "IS" Then
                Session("getStatus") = "IS"
            End If
        End If

    End Sub

    Private Sub btnRegisterStaff_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterStaff.ServerClick
        Session("getStatus") = "RS"
        Response.Redirect("admin_carian_pengajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewStaff_ServerClick(sender As Object, e As EventArgs) Handles btnViewStaff.ServerClick
        Session("getStatus") = "VS"
        Response.Redirect("admin_carian_pengajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnImportStaff_ServerClick(sender As Object, e As EventArgs) Handles btnImportStaff.ServerClick
        Session("getStatus") = "IS"
        Response.Redirect("admin_carian_pengajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''data is not delete.. instead just block from view.. incase permata want to recover the data
        strSQL = "Update staff_Info set satff_Status = 'Block' where stf_ID = '" & strKeyName & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY staff_Name ASC"

        tmpSQL = "Select distinct staff_info.stf_ID, UPPER(staff_Info.staff_Name) staff_Name, staff_Mykad, UPPER(staff_Info.staff_MobileNo) staff_MobileNo, UPPER(staff_Info.staff_Email) staff_Email, UPPER(setting.Parameter) staff_Campus From staff_Info"
        strWhere += "   LEFT JOIN setting on staff_Info.staff_Campus = setting.Value
                        WHERE stf_ID IS NOT NULL and staff_info.staff_ID is not null"
        strWhere += "   AND staff_Status = 'Access' and staff_Name NOT LIKE '%araken%' "

        If Session("SchoolCampus") = "APP" Then
            strWhere += " AND staff_info.staff_Campus = 'APP'"
        End If

        If Not txtstaff_data.Text.Length = 0 Then
            strWhere += " AND (staff_Name LIKE '%" & txtstaff_data.Text & "%'"
        End If

        If Not txtstaff_data.Text.Length = 0 Then
            strWhere += " OR staff_Mykad = '" & txtstaff_data.Text & "' OR staff_info.staff_ID = '" & txtstaff_data.Text & "')"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_pengajar_data.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + strKeyID)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim id As Object = Request.QueryString("admin_ID")
        Dim MyConnection As SqlConnection = New SqlConnection(strConn)

        Dim P1 As String = "False"
        Dim P2 As String = "False"
        Dim P3 As String = "Fasle"

        If staff_P1_Position.SelectedIndex > 0 Then
            P1 = "True"
        End If

        If staff_P2_Position.SelectedIndex > 0 Then
            P2 = "True"
        End If

        If staff_P3_Position.SelectedIndex > 0 Then
            P3 = "True"
        End If

        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        Dim checking_data As String = "SELECT stf_ID from staff_Info where staff_Status = 'Access' and staff_Mykad= '" & staff_Mykad.Text & "'"
        Dim collecting_data As String = oCommon.getFieldValue(checking_data)

        If collecting_data = "" Then

            If Not IsNumeric(staff_Name.Text) And staff_Name.Text <> "" Then

                If IsNumeric(staff_Mykad.Text) And staff_Mykad.Text <> "" And staff_Mykad.Text.Length < 14 Then

                    If staff_ID.Text <> "" Then

                        If staff_Email.Text <> "" Then

                            If staff_MobileNo.Text = "" Or IsNumeric(staff_MobileNo.Text) Then

                                If txtCity.Text = "" Or txtCity.Text.Length > 0 Then

                                    If staff_State.SelectedIndex > 0 Then

                                        If staff_Posscode.Text = "" Or IsNumeric(staff_Posscode.Text) And Regex.IsMatch(staff_Posscode.Text, "^[0-9]+$") Then

                                            If staff_Campus.SelectedIndex > 0 Then

                                                Dim imgPath As String = "~/staff_Image/user.png"

                                                If uploadPhoto.PostedFile.FileName <> "" Then

                                                    Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                                    ''sets the image path
                                                    imgPath = "~/staff_Image/" + filename

                                                    ''then save it to the Folder
                                                    uploadPhoto.SaveAs(Server.MapPath(imgPath))
                                                End If

                                                If imgPath <> "~/staff_Image/user.png" Then

                                                    If P1 = "True" And P2 = "True" And P3 = "True" Then

                                                        If staff_P1_Position.SelectedValue <> staff_P2_Position.SelectedValue And staff_P1_Position.SelectedValue <> staff_P3_Position.SelectedValue And staff_P2_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                            ' register staff login id and staff password save into database staff_Login for Admin
                                                            Using STDDATA As New SqlCommand("   INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Sex,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3,staff_Campus) 
                                                                                                values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "','" & strgender & "',
                                                                                                '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "','" & staff_Campus.SelectedValue & "')", MyConnection)
                                                                MyConnection.Open()
                                                                Dim i = STDDATA.ExecuteNonQuery()
                                                                MyConnection.Close()
                                                                If i <> 0 Then
                                                                    ShowMessage(" Add New Staff ", MessageType.Success)

                                                                    capture_user_position()
                                                                Else
                                                                    ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                                End If

                                                            End Using
                                                        Else
                                                            ShowMessage(" Please Select Different Staff Position ", MessageType.Error)
                                                        End If

                                                    ElseIf P1 = "True" And P2 = "True" Then

                                                        If staff_P1_Position.SelectedValue <> staff_P2_Position.SelectedValue Then

                                                            ' register staff login id and staff password save into database staff_Login for Admin
                                                            Using STDDATA As New SqlCommand("   INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Sex,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3,staff_Campus) 
                                                                                                values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "','" & strgender & "',
                                                                                                '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "','" & staff_Campus.SelectedValue & "')", MyConnection)
                                                                MyConnection.Open()
                                                                Dim i = STDDATA.ExecuteNonQuery()
                                                                MyConnection.Close()
                                                                If i <> 0 Then
                                                                    ShowMessage(" Add New Staff ", MessageType.Success)

                                                                    capture_user_position()
                                                                Else
                                                                    ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                                End If

                                                            End Using
                                                        Else
                                                            ShowMessage(" Please Select Different Staff Position ", MessageType.Error)
                                                        End If

                                                    ElseIf P1 = "True" And P3 = "True" Then

                                                        If staff_P1_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                            ' register staff login id and staff password save into database staff_Login for Admin
                                                            Using STDDATA As New SqlCommand("   INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Sex,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3,staff_Campus) 
                                                                                                values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "','" & strgender & "',
                                                                                                '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "','" & staff_Campus.SelectedValue & "')", MyConnection)
                                                                MyConnection.Open()
                                                                Dim i = STDDATA.ExecuteNonQuery()
                                                                MyConnection.Close()
                                                                If i <> 0 Then
                                                                    ShowMessage(" Add New Staff ", MessageType.Success)

                                                                    capture_user_position()
                                                                Else
                                                                    ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                                End If

                                                            End Using
                                                        Else
                                                            ShowMessage(" Please Select Different Staff Position ", MessageType.Error)
                                                        End If

                                                    ElseIf P3 = "True" And P2 = "True" Then

                                                        If staff_P2_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                            ' register staff login id and staff password save into database staff_Login for Admin
                                                            Using STDDATA As New SqlCommand("   INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Sex,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3,staff_Campus) 
                                                                                                values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "','" & strgender & "',
                                                                                                '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "','" & staff_Campus.SelectedValue & "')", MyConnection)
                                                                MyConnection.Open()
                                                                Dim i = STDDATA.ExecuteNonQuery()
                                                                MyConnection.Close()
                                                                If i <> 0 Then
                                                                    ShowMessage(" Add New Staff ", MessageType.Success)

                                                                    capture_user_position()
                                                                Else
                                                                    ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                                End If

                                                            End Using
                                                        Else
                                                            ShowMessage(" Please Select Different Staff Position ", MessageType.Error)
                                                        End If

                                                    Else

                                                        '' register staff login id and staff password save into database staff_Login for Admin
                                                        Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Sex,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3,staff_Campus) 
                                                                                         values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "','" & strgender & "',
                                                                                         '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "','" & staff_Campus.SelectedValue & "')", MyConnection)
                                                            MyConnection.Open()
                                                            Dim i = STDDATA.ExecuteNonQuery()
                                                            MyConnection.Close()
                                                            If i <> 0 Then
                                                                ShowMessage(" Add New Staff ", MessageType.Success)

                                                                capture_user_position()
                                                            Else
                                                                ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                            End If
                                                        End Using

                                                    End If

                                                Else
                                                    ShowMessage(" Please Upload Profile Photo ", MessageType.Error)
                                                End If
                                            Else
                                                ShowMessage(" Please Select Institutions ", MessageType.Error)
                                            End If
                                        Else
                                            ShowMessage(" Please Enter The Valid Zip Code ", MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage(" Please Enter The Valid State ", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage(" Please Select City ", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Please Enter The Valid Phone No ", MessageType.Error)
                            End If

                        Else
                            ShowMessage(" Please Enter The Valid Email Address ", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Enter The Valid Staff ID ", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Enter The Valid Staff NRIC / MYKAD ", MessageType.Error)
                End If
            Else
                ShowMessage(" Please Enter The Valid STaff Name ", MessageType.Error)
            End If

        Else
            ShowMessage(" MYkad Had Existed In Database ", MessageType.Error)
        End If
    End Sub

    Private Sub capture_user_position()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim staffLogin As String = ""

        If staff_Campus.SelectedValue = "PGPN" Then
            staffLogin = Split(staff_Name.Text, " ")(0) & "@UKM"

        ElseIf staff_Campus.SelectedValue = "APP" Then
            staffLogin = Split(staff_Name.Text, " ")(0) & "@APP"

        End If

        Dim staff_Password_P1 As String = ""
        Dim staff_Password_P2 As String = ""
        Dim staff_Password_P3 As String = ""

        Dim find_value As String = ""
        Dim get_value As String = ""

        Dim find_stf_ID As String = "select stf_ID from staff_Info where staff_Mykad = '" & staff_Mykad.Text & "' and staff_Status = 'Access'"
        Dim get_stf_ID = oCommon.getFieldValue(find_stf_ID)

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If staff_P1_Position.SelectedIndex > 0 Then
            staff_Password_P1 = oCommon.pswrd_random()

            find_value = "select Value from setting where Value = '" & staff_P1_Position.SelectedValue & "' and Type = 'Level Access'"
            get_value = oCommon.getFieldValue(find_value)

            ''insert staff for position 1
            Using STDDATA As New SqlCommand("INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status) 
                                         values ('" & get_stf_ID & "','" & staffLogin.ToUpper & "','" & staff_Password_P1 & "','" & get_value & "','Position 1','Access')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using
        End If

        If staff_P2_Position.SelectedIndex > 0 Then
            staff_Password_P2 = oCommon.pswrd_random()

            find_value = "select Value from setting where Value = '" & staff_P2_Position.SelectedValue & "' and Type = 'Level Access'"
            get_value = oCommon.getFieldValue(find_value)

            ''insert staff for position 2
            Using STDDATA As New SqlCommand("INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status) 
                                         values ('" & get_stf_ID & "','" & staffLogin.ToUpper & "','" & staff_Password_P2 & "'," & get_value & "',Position 2,'Access')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using
        End If

        If staff_P3_Position.SelectedIndex > 0 Then
            staff_Password_P3 = oCommon.pswrd_random()

            find_value = "select Value from setting where Value = '" & staff_P3_Position.SelectedValue & "' and Type = 'Level Access'"
            get_value = oCommon.getFieldValue(find_value)

            ''insert staff for position 3
            Using STDDATA As New SqlCommand("INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status) 
                                         values ('" & get_stf_ID & "','" & staffLogin.ToUpper & "','" & staff_Password_P3 & "','" & get_value & "',Position 3,'Access')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using
        End If

        If staff_P1_Position.SelectedIndex = 0 And staff_P2_Position.SelectedIndex = 0 And staff_P3_Position.SelectedIndex = 0 Then
            staff_Password_P1 = oCommon.pswrd_random()

            Using STDDATA As New SqlCommand("   INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_Status) 
                                                values ('" & get_stf_ID & "','" & staffLogin.ToUpper & "','" & staff_Password_P1 & "','Access')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using
        End If

    End Sub

    Private Sub accessLevel_List()
        strSQL = "select Parameter, Value from setting where Type = 'Level Access' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_P2_Position.DataSource = ds
            staff_P2_Position.DataTextField = "Parameter"
            staff_P2_Position.DataValueField = "Value"
            staff_P2_Position.DataBind()
            staff_P2_Position.Items.Insert(0, New ListItem("None", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub positionLevel_List()
        strSQL = "select Parameter, Value from setting where Type = 'Level Access' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_P3_Position.DataSource = ds
            staff_P3_Position.DataTextField = "Parameter"
            staff_P3_Position.DataValueField = "Value"
            staff_P3_Position.DataBind()
            staff_P3_Position.Items.Insert(0, New ListItem("None", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub adminLevel_List()
        strSQL = "select Parameter, Value from setting where Type = 'Level Access' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_P1_Position.DataSource = ds
            staff_P1_Position.DataTextField = "Parameter"
            staff_P1_Position.DataValueField = "Value"
            staff_P1_Position.DataBind()
            staff_P1_Position.Items.Insert(0, New ListItem("None", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub fillDDL(type As String, ddl As DropDownList)
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = String.Format("SELECT Parameter FROM setting Where Type = '{0}' AND Parameter IS NOT NULL order by Parameter ASC", type)
            Dim sqlDA As New SqlDataAdapter(query, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl.DataSource = ds.Tables(0)
            ddl.DataTextField = "Parameter"
            ddl.DataValueField = "Parameter"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("Select " & type, String.Empty))

        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub campus_List()
        strSQL = "select Parameter, Value from setting where Type = 'Pusat Campus' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_Campus.DataSource = ds
            staff_Campus.DataTextField = "Parameter"
            staff_Campus.DataValueField = "Value"
            staff_Campus.DataBind()
            staff_Campus.Items.Insert(0, New ListItem("Select Campus", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/staff_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [staff$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVar()
                strMsg = ""

                'Staff_ID
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_ID")) Then
                    strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_ID")
                Else
                    strMsg += " Please Enter Staff_ID |"
                End If

                'Staff_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Name")) Then
                    strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_Name")
                Else
                    strMsg += " Please Enter Staff_Name |"
                End If

                'Staff_Mykad
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Mykad")) Then
                    strStaffMykad = SiteData.Tables(0).Rows(i).Item("Staff_Mykad")
                Else
                    strMsg += " Please Enter Staff_Mykad |"
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message & "Here 2"
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0

        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")
            strStaffID = SiteData.Tables(0).Rows(i).Item("Staff_ID")
            strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_Name")
            strStaffMykad = SiteData.Tables(0).Rows(i).Item("Staff_Mykad")
            strStaffGender = SiteData.Tables(0).Rows(i).Item("Staff_Gender")
            strStaffEmail = SiteData.Tables(0).Rows(i).Item("Staff_Email")
            strStaffMobileNo = SiteData.Tables(0).Rows(i).Item("Staff_MobileNo")

            strStaffLogin = Split(strStaffName, " ")(0) & "@UKM"

            strStaffPositionP1 = SiteData.Tables(0).Rows(i).Item("Staff_Position1")
            strStaffPositionP2 = SiteData.Tables(0).Rows(i).Item("Staff_Position2")
            strStaffPositionP3 = SiteData.Tables(0).Rows(i).Item("Staff_Position3")

            strStaffAddress = SiteData.Tables(0).Rows(i).Item("Staff_Address")
            strStaffCity = SiteData.Tables(0).Rows(i).Item("Staff_City")
            strStaffState = SiteData.Tables(0).Rows(i).Item("Staff_State")
            strStaffPostalCode = SiteData.Tables(0).Rows(i).Item("Staff_PostalCode")
            strStaffCampus = SiteData.Tables(0).Rows(i).Item("Staff_Campus")

            Dim staffLoginAccess_data_pos1 As String = ""
            Dim staffLoginAccess_data_pos2 As String = ""
            Dim staffLoginAccess_data_pos3 As String = ""

            If strStaffPositionP1.Length > 0 Then
                strStaffPasswordP1 = oCommon.pswrd_random()

                strSQL = "select Value from setting where Value = '" & strStaffPositionP1 & "' and Type = 'Level Access'"
                staffLoginAccess_data_pos1 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP1 = ""
            End If

            If strStaffPositionP2.Length > 0 Then
                strStaffPasswordP2 = oCommon.pswrd_random()

                strSQL = "select Value from setting where Value = '" & strStaffPositionP2 & "' and Type = 'Level Access'"
                staffLoginAccess_data_pos2 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP2 = ""
            End If

            If strStaffPositionP3.Length > 0 Then
                strStaffPasswordP3 = oCommon.pswrd_random()

                strSQL = "select Value from setting where Value = '" & strStaffPositionP3 & "' and Type = 'Level Access'"
                staffLoginAccess_data_pos3 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP3 = ""
            End If

            strSQL = "SELECT stf_ID FROM staff_Info WHERE staff_Mykad = '" & strStaffMykad & "' AND staff_Status = 'Access'"
            If oCommon.isExist(strSQL) = True Then
                'UPDATE STAFF INFO
                strSQL = "SELECT stf_ID FROM staff_Info WHERE staff_Mykad = '" & strStaffMykad & "' AND staff_Status = 'Access'"
                Dim id As String = oCommon.getFieldValue(strSQL)

                If id.Length > 0 Then

                    strSQL = "  UPDATE staff_Info SET
                                staff_ID = UPPER('" & strStaffID & "'),
                                staff_Name = UPPER('" & strStaffName & "'),
                                staff_Email = UPPER('" & strStaffEmail & "'),
                                staff_Sex = UPPER('" & strStaffGender & "'),
                                staff_MobileNo = '" & oCommon.FixSingleQuotes(strStaffMobileNo) & "',
                                staff_Address = UPPER('" & oCommon.FixSingleQuotes(strStaffAddress) & "'),
                                staff_City = UPPER('" & strStaffCity & "'),
                                staff_Posscode = '" & strStaffPostalCode & "',
                                staff_Position1 = UPPER('" & staffLoginAccess_data_pos1 & "'),
                                staff_Position2 = UPPER('" & staffLoginAccess_data_pos2 & "'),
                                staff_Position3 = UPPER('" & staffLoginAccess_data_pos3 & "'),
                                staff_State = UPPER('" & strStaffState & "')"

                    strSQL += " WHERE stf_ID = '" & id & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)


                    If strRet = "0" Then
                        errorData = 0
                        countUpdate = countUpdate + 1
                    Else
                        errorData = 1
                    End If

                End If

            Else
                'INSERT NEW STAFF
                strSQL = "  INSERT INTO staff_Info(staff_ID, staff_Name, staff_Mykad, staff_Email, staff_Sex, staff_MobileNo, staff_TelNo,
                            staff_Address, staff_City, staff_Posscode, staff_State, staff_Photo, staff_Status, staff_LoginAttempt,staff_Position1,staff_Position2,staff_Postion3)"

                strSQL += " VALUES (
                            UPPER('" & strStaffID & "'), 
                            UPPER('" & oCommon.FixSingleQuotes(strStaffName) & "'), 
                            '" & strStaffMykad & "', 
                            UPPER('" & oCommon.FixSingleQuotes(strStaffEmail) & "'),
                            UPPER('" & strStaffGender & "'),
                            '" & oCommon.FixSingleQuotes(strStaffMobileNo) & "', 
                            '" & oCommon.FixSingleQuotes(strStaffTelNo) & "',
                            UPPER('" & oCommon.FixSingleQuotes(strStaffAddress) & "'), 
                            UPPER('" & strStaffCity & "'), 
                            UPPER('" & strStaffPostalCode & "'), 
                            UPPER('" & strStaffState & "'), 
                            '~/staff_Image/user.png', 
                            'Access',  
                            '0',UPPER('" & staffLoginAccess_data_pos1 & "'),UPPER('" & staffLoginAccess_data_pos2 & "'),UPPER('" & staffLoginAccess_data_pos3 & "'))"

                strRet = oCommon.ExecuteSQL(strSQL)

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''INSERT STAFF LOGIN

                Dim find_stfID As String = "select stf_ID from staff_info where staff_Mykad = '" & strStaffMykad & "' and staff_Status = 'Access'"
                Dim get_sftfID As String = oCommon.getFieldValue(find_stfID)

                If strStaffPositionP1 <> "" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP1 & "','" & staffLoginAccess_data_pos1 & "','Position 1','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                If strStaffPositionP2 <> "" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP2 & "','" & staffLoginAccess_data_pos2 & "','Position 2','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                If strStaffPositionP3 <> "" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP3 & "','" & staffLoginAccess_data_pos3 & "','Position 3','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                If strRet = "0" Then
                    errorData = 0
                    countInsert = countInsert + 1
                Else
                    errorData = 1
                End If

            End If
        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Error)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        strBil = ""
        strStaffID = ""
        strStaffName = ""
        strStaffMykad = ""
        strStaffGender = ""
        strStaffEmail = ""
        strStaffMobileNo = ""
        strStaffTelNo = ""
        strStaffLogin = ""
        strStaffPasswordP1 = ""
        strStaffPasswordP2 = ""
        strStaffPasswordP3 = ""
        strStaffYear = ""
        strStaffPositionP1 = ""
        strStaffPositionP2 = ""
        strStaffPositionP3 = ""
        strStaffAddress = ""
        strStaffCity = ""
        strStaffPostalCode = ""
        strStaffState = ""
        strStaffCampus = ""

    End Sub

    Private Sub BtnUploadedStaff_ServerClick(sender As Object, e As EventArgs) Handles BtnUploadedStaff.ServerClick
        Try
            If ImportExcel() = True Then
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/staff_info.xlsx")
    End Sub

End Class