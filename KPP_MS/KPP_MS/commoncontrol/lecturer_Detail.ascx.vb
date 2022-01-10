Imports System.Data.SqlClient
Imports System.IO

Public Class lecturer_Detail
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                previousPage.NavigateUrl = String.Format("~/admin_carian_pengajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                Checking_MenuAccess_Load()

                If Session("getStatus") = "SI" Then ''Staff Information
                    txtbreadcrum1.Text = "Staff Information"

                    StaffInformation.Visible = True
                    CourseInformation.Visible = False

                    btnStaffInformation.Attributes("class") = "btn btn-info"
                    btnCourseInformation.Attributes("class") = "btn btn-default font"

                    fillStateDDL()
                    staff_State.SelectedIndex = 0
                    campus_List()
                    fillPositionP1DDL()
                    fillPositionP2DDL()
                    fillPositionP3DDL()

                    LoadPage()

                ElseIf Session("getStatus") = "CI" Then ''Course Information
                    txtbreadcrum1.Text = "Course Information"

                    StaffInformation.Visible = False
                    CourseInformation.Visible = True

                    btnStaffInformation.Attributes("class") = "btn btn-default font"
                    btnCourseInformation.Attributes("class") = "btn btn-info"

                    sem_list()
                    year_list()

                    strRet = BindData(datRespondent)

                End If

                If staff_Campus.SelectedValue = "APP" Then
                    txtLoginID_Status.Text = "@APP"
                ElseIf staff_Campus.SelectedValue = "PGPN" Then
                    txtLoginID_Status.Text = "@UKM"
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnStaffInformation.Visible = False
        btnCourseInformation.Visible = False
        StaffInformation.Visible = False
        CourseInformation.Visible = False

        btnLecturerUpdate.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_StaffInformation As String = ""
        Dim Get_CourseInformation As String = ""

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

            ''Get Function Button 1 Update Data 
            strSQL = "  Select B.F1_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Update As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "Staff Information" And find_Data_SubMenu2.Length > 0 Then
                btnStaffInformation.Visible = True
                StaffInformation.Visible = True

                Get_StaffInformation = "TRUE"

                If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                    btnLecturerUpdate.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Course Information" And find_Data_SubMenu2.Length > 0 Then
                btnCourseInformation.Visible = True
                CourseInformation.Visible = True

                Get_CourseInformation = "TRUE"

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnStaffInformation.Visible = True
                btnCourseInformation.Visible = True
                StaffInformation.Visible = True
                CourseInformation.Visible = True

                btnLecturerUpdate.Visible = True

                Get_StaffInformation = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "SI" Or Session("getStatus") = "CI" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "SI" And Session("getStatus") <> "CI" Then
            If Get_StaffInformation = "TRUE" Then
                Data_If_Not_Group_Status = "SI"
            ElseIf Get_CourseInformation = "TRUE" Then
                Data_If_Not_Group_Status = "CI"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_StaffInformation = "TRUE" And Data_If_Not_Group_Status = "SI" Then
                Session("getStatus") = "SI"
            ElseIf Get_CourseInformation = "TRUE" And Data_If_Not_Group_Status = "CI" Then
                Session("getStatus") = "CI"
            End If
        End If

    End Sub


    Private Sub btnStaffInformation_ServerClick(sender As Object, e As EventArgs) Handles btnStaffInformation.ServerClick
        Session("getStatus") = "SI"
        Response.Redirect("admin_edit_pengajar_data.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID"))
    End Sub

    Private Sub btnCourseInformation_ServerClick(sender As Object, e As EventArgs) Handles btnCourseInformation.ServerClick
        Session("getStatus") = "CI"
        Response.Redirect("admin_edit_pengajar_data.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID"))
    End Sub

    Private Sub LoadPage()
        strSQL = "  select distinct UPPER(staff_Name) staff_Name, UPPER(staff_info.staff_ID) staff_ID, UPPER(staff_Mykad) staff_Mykad, UPPER(staff_Email) staff_Email, UPPER(staff_Sex) staff_Sex, staff_MobileNo, UPPER(staff_Address) staff_Address, UPPER(staff_City) staff_City, 
                    staff_State, Value, staff_Position1, staff_Position2, staff_Position3, staff_Posscode, staff_Photo
                    from staff_info left join setting on staff_info.staff_Campus = setting.Value where staff_info.stf_ID = '" & Request.QueryString("stf_ID") & "'"
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim STAFFPHOTO As New SqlDataAdapter()

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Name")) Then
                staff_Name.Text = ds.Tables(0).Rows(0).Item("staff_Name")
            Else
                staff_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_ID")) Then
                staff_ID.Text = ds.Tables(0).Rows(0).Item("staff_ID")
            Else
                staff_ID.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Mykad")) Then
                staff_MyKad.Text = ds.Tables(0).Rows(0).Item("staff_Mykad")
            Else
                staff_MyKad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Email")) Then
                staff_Email.Text = ds.Tables(0).Rows(0).Item("staff_Email")
            Else
                staff_Email.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Sex")) Then
                Dim staff_gender As String = ds.Tables(0).Rows(0).Item("staff_Sex")

                If staff_gender = "Male" Or staff_gender = "MALE" Then
                    rbtn_Male.Checked = True
                ElseIf staff_gender = "Female" Or staff_gender = "FEMALE" Then
                    rbtn_Female.Checked = True
                End If

            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_MobileNo")) Then
                staff_MobileNo.Text = ds.Tables(0).Rows(0).Item("staff_MobileNo")
            Else
                staff_MobileNo.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Address")) Then
                staff_Address.Text = ds.Tables(0).Rows(0).Item("staff_Address")
            Else
                staff_Address.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_City")) Then
                txtCity.Text = ds.Tables(0).Rows(0).Item("staff_City")
            Else
                txtCity.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_State")) Then
                staff_State.SelectedValue = ds.Tables(0).Rows(0).Item("staff_State")
            Else
                staff_State.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Value")) Then
                staff_Campus.SelectedValue = ds.Tables(0).Rows(0).Item("Value")
            Else
                staff_Campus.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position1")) Then
                staff_Position_P1.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position1")
            Else
                staff_Position_P1.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position2")) Then
                staff_Position_P2.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position2")
            Else
                staff_Position_P2.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position3")) Then
                staff_Position_P3.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position3")
            Else
                staff_Position_P3.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Posscode")) Then
                staff_Posscode.Text = ds.Tables(0).Rows(0).Item("staff_Posscode")
            Else
                staff_Posscode.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Photo")) Then
                staff_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("staff_Photo")

            Else
                staff_Photo.ImageUrl = "~/staff_Image/user.png"
            End If
        End If

        strSQL = "Select SUBSTRING (staff_Login,0,PATINDEX('%@%',staff_Login)) from staff_Login where stf_ID = '" & Request.QueryString("stf_ID") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        txtLoginID.Text = strRet
    End Sub

    Private Sub btnLecturerUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnLecturerUpdate.ServerClick
        Dim errorCount As Integer = 0

        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        Dim host As String = Net.Dns.GetHostName()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objNewConn As SqlConnection = New SqlConnection(strConn)

        Dim pos1_list As String = staff_Position_P1.SelectedValue
        Dim pos2_list As String = staff_Position_P2.SelectedValue
        Dim pos3_list As String = staff_Position_P3.SelectedValue

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        If Not IsNumeric(staff_Name.Text) And staff_Name.Text <> "" Then

            If staff_MyKad.Text <> "" And staff_MyKad.Text.Length < 20 Then

                If staff_ID.Text <> "" And Regex.IsMatch(staff_ID.Text, "^[A-Za-z0-9 ]+$") Then

                    If staff_Email.Text <> "" Then

                        If strgender <> "" Then

                            If staff_MobileNo.Text <> "" And Regex.IsMatch(staff_MobileNo.Text, "^[0-9]+$") Then

                                If txtCity.Text = "" Or txtCity.Text.Length > 0 Then

                                    If staff_State.Text = "" Or Not IsNumeric(staff_State.SelectedValue) And Regex.IsMatch(staff_State.SelectedValue, "^[A-Za-z ]+$") Then

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

                                                If txtLoginID.Text <> "" And Regex.IsMatch(txtLoginID.Text, "^[A-Za-z0-9]+$") Then

                                                    'UPDATE STAFF INFO DATA
                                                    strSQL = "  UPDATE staff_Info set staff_Name = UPPER('" & staff_Name.Text & "'), staff_Mykad = '" & staff_MyKad.Text & "',
                                                                staff_ID = UPPER('" & staff_ID.Text & "'), staff_Sex = UPPER('" & strgender & "'), staff_Email = UPPER('" & staff_Email.Text & "'), staff_MobileNo = '" & staff_MobileNo.Text & "', staff_Address = UPPER('" & staff_Address.Text & "'),
                                                                staff_City = UPPER('" & txtCity.Text & "'), staff_State = '" & staff_State.SelectedValue & "', staff_Posscode = '" & staff_Posscode.Text & "', staff_Photo = '" & imgPath & "',
                                                                staff_Position1 = '" & pos1_list & "', staff_Position2 = '" & pos2_list & "', staff_Position3 = '" & pos3_list & "', staff_Campus = '" & staff_Campus.SelectedValue & "' 
                                                                WHERE stf_ID ='" & Request.QueryString("stf_ID") & "'"
                                                    strRet = oCommon.ExecuteSQL(strSQL)

                                                    Dim find_value As String = ""
                                                    Dim get_value As String = ""

                                                    Dim strStaffLogin As String = ""
                                                    If staff_Campus.SelectedValue = "PGPN" Then
                                                        strStaffLogin = Split(staff_Name.Text, " ")(0) & "@UKM"

                                                    ElseIf staff_Campus.SelectedValue = "APP" Then
                                                        strStaffLogin = Split(staff_Name.Text, " ")(0) & "@APP"

                                                    End If

                                                    Dim insertFunction As String = ""
                                                    Dim updateFunction As String = ""

                                                    Dim strStaffPasswordP1 As String = oCommon.pswrd_random()

                                                    strSQL = "select login_ID from staff_Login where stf_ID = '" & Request.QueryString("stf_ID") & "' and staff_PositionNo = 'Position 1'"
                                                    strRet = oCommon.getFieldValue(strSQL)

                                                    find_value = "select Value from setting where Value = '" & staff_Position_P1.SelectedValue & "' and Type = 'Level Access'"
                                                    get_value = oCommon.getFieldValue(find_value)

                                                    If strRet = "" Then
                                                        insertFunction = InsertPosition(strStaffLogin, strStaffPasswordP1, "Position 1", get_value)
                                                    Else
                                                        updateFunction = UpdatePosition("Position 1", get_value, strRet, strStaffLogin)
                                                    End If


                                                    Dim strStaffPasswordP2 As String = oCommon.pswrd_random()

                                                    strSQL = "select login_ID from staff_Login where stf_ID = '" & Request.QueryString("stf_ID") & "' and staff_PositionNo = 'Position 2'"
                                                    strRet = oCommon.getFieldValue(strSQL)

                                                    find_value = "select Value from setting where Value = '" & staff_Position_P2.SelectedValue & "' and Type = 'Level Access'"
                                                    get_value = oCommon.getFieldValue(find_value)

                                                    If strRet = "" Then
                                                        insertFunction = InsertPosition(strStaffLogin, strStaffPasswordP2, "Position 2", get_value)
                                                    Else
                                                        updateFunction = UpdatePosition("Position 2", get_value, strRet, strStaffLogin)
                                                    End If

                                                    Dim strStaffPasswordP3 As String = oCommon.pswrd_random()

                                                    strSQL = "select login_ID from staff_Login where stf_ID = '" & Request.QueryString("stf_ID") & "' and staff_PositionNo = 'Position 3'"
                                                    strRet = oCommon.getFieldValue(strSQL)

                                                    find_value = "select Value from setting where Value = '" & staff_Position_P3.SelectedValue & "' and Type = 'Level Access'"
                                                    get_value = oCommon.getFieldValue(find_value)

                                                    If strRet = "" Then
                                                        insertFunction = InsertPosition(strStaffLogin, strStaffPasswordP3, "Position 3", get_value)
                                                    Else
                                                        updateFunction = UpdatePosition("Position 3", get_value, strRet, strStaffLogin)
                                                    End If

                                                    If strRet = "0" Then
                                                        ShowMessage(" Update Staff Information ", MessageType.Success)

                                                        Using ActivityTrail As New SqlCommand("INSERT INTO ActivityTrail_Upperlvl(log_Date,Activity,Login_ID,User_HostAddress,Name_Matters,page) values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','UPDATE STAFF','" & Request.QueryString("stf_ID") & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "','" & staff_Name.Text & "','pengajar_kemaskini_profile.aspx')", objConn)
                                                            objConn.Open()
                                                            Dim j = ActivityTrail.ExecuteNonQuery()
                                                            objConn.Close()
                                                        End Using
                                                    Else
                                                        ShowMessage(" Unsuccessful Update Staff Information ", MessageType.Error)
                                                    End If

                                                Else
                                                    ShowMessage(" Please Fill In Login ID Correctly [A-Z,a-z,0-9] ", MessageType.Error)
                                                End If
                                            Else
                                                ShowMessage(" Please Select Institutions ", MessageType.Error)
                                            End If
                                        Else
                                            ShowMessage(" Please Fill In Zip Code ", MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage(" Please Select State ", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage(" Please Fill In City ", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Please Fill In Phone Mobile ", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Please Select Gender ", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Fill In Email Address ", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Fill In Staff ID ", MessageType.Error)
                End If
            Else
                ShowMessage(" Please FIll In Staff NRIC / MYKAD ", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Fill In Staff Name ", MessageType.Error)
        End If

    End Sub

    Private Function InsertPosition(ByVal STFLOGIN As String, ByVal STFPSWRD As String, ByVal STFPOS As String, ByVal STFACCESS As String) As String

        strSQL = "INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_PositionNo,staff_Access,staff_Status,staff_LoginAttempt)
                  VALUES('" & Request.QueryString("stf_ID") & "','" & STFLOGIN & "','" & STFPSWRD & "','" & STFPOS & "','" & STFACCESS & "','Access','0')"
        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function

    Private Function UpdatePosition(ByVal STFPOS As String, ByVal STFACCESS As String, ByVal id_login As String, ByVal username As String) As String

        Dim checkPassword As String = "select staff_Password from staff_Login where login_ID = '" & id_login & "'"
        Dim getPassword As String = oCommon.getFieldValue(checkPassword)

        If getPassword.Length = 0 Then
            strSQL = "UPDATE staff_Login set staff_Access = '" & STFACCESS & "', staff_PositionNo = '" & STFPOS & "', staff_Login = '" & username & "', staff_Password = '" & oCommon.pswrd_random() & "' where login_ID = '" & id_login & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else
            strSQL = "UPDATE staff_Login set staff_Access = '" & STFACCESS & "', staff_PositionNo = '" & STFPOS & "', staff_Login = '" & username & "' where login_ID = '" & id_login & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        Return strRet
    End Function

    Private Sub campus_List()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "select UPPER(Parameter) Parameter, Value from setting where Type = 'Pusat Campus' and Value  = 'APP' order by Parameter ASC"
        Else
            strSQL = "select UPPER(Parameter) Parameter, Value from setting where Type = 'Pusat Campus' order by Parameter ASC"
        End If

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

    Protected Sub fillRadioList(parameter As String, rb As RadioButtonList)
        Dim Query As String = "SELECT Parameter FROM setting WHERE Type = '" & parameter & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(Query, objConn)

        Dim ds As New DataSet
        sqlDA.Fill(ds)
        rb.DataSource = ds
        rb.DataTextField = "Parameter"
        rb.DataValueField = "Parameter"
        rb.DataBind()

        For Each item As ListItem In rb.Items
            item.Attributes.Add("class", "radio-inline")
            item.Attributes.Add("Style", "display:inline-block; margin: 0px 25px 0px 25px;")
        Next

    End Sub

    Protected Sub fillStateDDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT UPPER(Parameter) Parameter, Value FROM setting Where Type = 'State' AND Parameter IS NOT NULL order by Parameter ASC"
            Dim sqlDA As New SqlDataAdapter(query, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            staff_State.DataSource = ds.Tables(0)
            staff_State.DataTextField = "Parameter"
            staff_State.DataValueField = "Value"
            staff_State.DataBind()
            staff_State.Items.Insert(0, New ListItem("Select State...", String.Empty))

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub fillPositionP2DDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT UPPER(Parameter) Parameter, Value FROM setting Where Type = 'Level Access' AND Parameter IS NOT NULL order by Parameter ASC"
            Dim sqlDA As New SqlDataAdapter(query, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)
            staff_Position_P2.DataSource = ds.Tables(0)
            staff_Position_P2.DataTextField = "Parameter"
            staff_Position_P2.DataValueField = "Value"
            staff_Position_P2.DataBind()
            staff_Position_P2.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub fillPositionP1DDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT UPPER(Parameter) Parameter, Value FROM setting Where Type = 'Level Access' AND Parameter IS NOT NULL order by Parameter ASC"
            Dim sqlDA As New SqlDataAdapter(query, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)
            staff_Position_P1.DataSource = ds.Tables(0)
            staff_Position_P1.DataTextField = "Parameter"
            staff_Position_P1.DataValueField = "Value"
            staff_Position_P1.DataBind()
            staff_Position_P1.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub fillPositionP3DDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT UPPER(Parameter) Parameter, Value FROM setting Where Type = 'Level Access' AND Parameter IS NOT NULL order by Parameter ASC"
            Dim sqlDA As New SqlDataAdapter(query, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)
            staff_Position_P3.DataSource = ds.Tables(0)
            staff_Position_P3.DataTextField = "Parameter"
            staff_Position_P3.DataValueField = "Value"
            staff_Position_P3.DataBind()
            staff_Position_P3.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub sem_list()
        strSQL = "SELECT * FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemn.DataSource = ds
            ddlSemn.DataTextField = "Parameter"
            ddlSemn.DataValueField = "Value"
            ddlSemn.DataBind()
            ddlSemn.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "Select distinct lecturer_year from lecturer where stf_ID = '" & Request.QueryString("stf_ID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "lecturer_year"
            ddlYear.DataValueField = "lecturer_year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
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
            Dlt_ClassData.SelectCommand.CommandText = "delete lecturer where ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
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

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        tmpSQL = "Select distinct lecturer.ID, subject_name, subject_code, subject_sem, subject_StudentYear, class_Name From lecturer 
                  left join subject_info on lecturer.subject_ID=subject_info.subject_ID 
                  left join class_info on lecturer.class_ID=class_info.class_ID 
                  left join staff_Info on lecturer.stf_ID=staff_Info.stf_ID"
        strWhere = " WHERE staff_Info.stf_ID = '" & Request.QueryString("stf_ID") & "'"
        strWhere += " and lecturer.lecturer_year = '" & ddlYear.SelectedValue & "'  and staff_Info.staff_Status = 'Access'"

        If ddlSemn.SelectedIndex > 0 Then
            strWhere += " and subject_info.subject_sem = '" & ddlSemn.SelectedValue & "' "
        End If

        getSQL = tmpSQL & strWhere

        Return getSQL
    End Function

    Private Sub ddlSemn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemn.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
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