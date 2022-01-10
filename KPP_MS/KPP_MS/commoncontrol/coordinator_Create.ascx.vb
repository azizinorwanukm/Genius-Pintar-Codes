Imports System.Data.SqlClient

Public Class coordinator_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction
    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "VC" Then ''View Coordinator
                    txtbreadcrum1.Text = "View Coordinator"
                    ViewCoordinator.Visible = True
                    RegisterCoordinator.Visible = False

                    btnViewCoordinator.Attributes("class") = "btn btn-info"
                    btnRegisterCoordinator.Attributes("class") = "btn btn-default font"

                    yearListCoordinator()
                    campusListCoordinator()
                    programListCoordinator()
                    courseListCoordinator()
                    staffListCoordinator()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "RC" Then ''Register Coordinator
                    txtbreadcrum1.Text = "Register Coordinator"
                    ViewCoordinator.Visible = False
                    RegisterCoordinator.Visible = True

                    btnViewCoordinator.Attributes("class") = "btn btn-default font"
                    btnRegisterCoordinator.Attributes("class") = "btn btn-info"

                    ddlsubject.Enabled = False

                    yearList()
                    courseList()
                    staffList()
                    studentlevel()
                    campusList()
                    programList()

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewCoordinator.Visible = False
        btnRegisterCoordinator.Visible = False
        ViewCoordinator.Visible = False
        RegisterCoordinator.Visible = False

        Btnsimpan.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewCoordinator As String = ""
        Dim Get_RegisterCoordinator As String = ""

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

            If find_Data_SubMenu2 = "View Coordinator" And find_Data_SubMenu2.Length > 0 Then
                btnViewCoordinator.Visible = True
                ViewCoordinator.Visible = True

                Get_ViewCoordinator = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Coordinator" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterCoordinator.Visible = True
                RegisterCoordinator.Visible = True

                Get_RegisterCoordinator = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewCoordinator.Visible = True
                btnRegisterCoordinator.Visible = True
                ViewCoordinator.Visible = True
                RegisterCoordinator.Visible = True

                Btnsimpan.Visible = True

                Get_ViewCoordinator = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RC" Or Session("getStatus") = "VC" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RC" And Session("getStatus") <> "VC" Then
            If Get_ViewCoordinator = "TRUE" Then
                Data_If_Not_Group_Status = "VC"
            ElseIf Get_RegisterCoordinator = "TRUE" Then
                Data_If_Not_Group_Status = "RC"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewCoordinator = "TRUE" And Data_If_Not_Group_Status = "VC" Then
                Session("getStatus") = "VC"
            ElseIf Get_RegisterCoordinator = "TRUE" And Data_If_Not_Group_Status = "RC" Then
                Session("getStatus") = "RC"
            End If
        End If

    End Sub

    Private Sub btnViewCoordinator_ServerClick(sender As Object, e As EventArgs) Handles btnViewCoordinator.ServerClick
        Session("getStatus") = "VC"
        Response.Redirect("admin_daftar_koordinator.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnRegisterCoordinator_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterCoordinator.ServerClick
        Session("getStatus") = "RC"
        Response.Redirect("admin_daftar_koordinator.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub yearList()
        strSQL = "SELECT Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub campusList()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlCampus.DataSource = levds
            ddlCampus.DataValueField = "Value"
            ddlCampus.DataTextField = "Parameter"
            ddlCampus.DataBind()
            ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub programList()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If ddlCampus.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlStream.DataSource = levds
            ddlStream.DataValueField = "Value"
            ddlStream.DataTextField = "Parameter"
            ddlStream.DataBind()
            ddlStream.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub studentlevel()
        strSQL = "SELECT Parameter from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllevel.DataSource = ds
            ddllevel.DataTextField = "Parameter"
            ddllevel.DataValueField = "Parameter"
            ddllevel.DataBind()
            ddllevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddllevel.Items.Insert(1, New ListItem("All", "ALL"))
            ddllevel.Items.Insert(2, New ListItem("Foundation (1-3)", "F1"))
            ddllevel.Items.Insert(3, New ListItem("Level (1-2)", "L1"))
            ddllevel.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staffList()
        strSQL = "Select staff_Name, stf_ID from staff_Info where staff_Status = 'Access' and staff_Name NOT LIKE '%araken%' and staff_Campus = '" & ddlCampus.SelectedValue & "' order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaff.DataSource = ds
            ddlstaff.DataTextField = "staff_Name"
            ddlstaff.DataValueField = "stf_ID"
            ddlstaff.DataBind()
            ddlstaff.Items.Insert(0, New ListItem("Select Staff", String.Empty))
            ddlstaff.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub courseList()
        strSQL = "SELECT distinct course_Name from subject_info where subject_year = '" & ddlyear.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "' order by course_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcourse.DataSource = ds
            ddlcourse.DataTextField = "course_Name"
            ddlcourse.DataValueField = "course_Name"
            ddlcourse.DataBind()
            ddlcourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlcourse.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub subjectList()
        strSQL = "SELECT distinct subject_Name from subject_info where course_Name = '" & ddlcourse.SelectedValue & "' and subject_year = '" & ddlyear.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "' order by subject_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsubject.DataSource = ds
            ddlsubject.DataTextField = "subject_Name"
            ddlsubject.DataValueField = "subject_Name"
            ddlsubject.DataBind()
            ddlsubject.Items.Insert(0, New ListItem("Select Subject", String.Empty))
            ddlsubject.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampus.SelectedIndexChanged
        Try
            courseList()
            staffList()
            programList()
            subjectList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlStream_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStream.SelectedIndexChanged
        Try
            courseList()
            subjectList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlcourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcourse.SelectedIndexChanged

        If ddlcourse.SelectedValue = "Bahasa Antarabangsa" Or ddlcourse.SelectedValue = "AP Courses" Then
            ddlsubject.Enabled = True
            subjectList()

        Else
            ddlsubject.Enabled = False
        End If

    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        If ddlyear.SelectedIndex > 0 Then

            If ddlstaff.SelectedValue > 0 And ddlstaff.SelectedValue <> "" Then

                If ddllevel.SelectedIndex > 0 And ddllevel.SelectedValue <> "" Then

                    If ddlcourse.SelectedValue <> "" And ddlcourse.SelectedValue <> "Bahasa Antarabangsa" And ddlcourse.SelectedValue <> "AP Courses" Then

                        If ddllevel.SelectedValue = "ALL" Then

                            ''insert Foundation
                            For value As Integer = 1 To 3
                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) 
                                                                 values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','Foundation " & value & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        ShowMessage(" Add New Coordinator", MessageType.Success)
                                    Else
                                        ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                    End If
                                End Using
                            Next

                            ''insert Level
                            For value As Integer = 1 To 2
                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) 
                                                                 values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','Level " & value & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        ShowMessage(" Add New Coordinator", MessageType.Success)
                                    Else
                                        ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                    End If
                                End Using
                            Next

                        ElseIf ddllevel.SelectedValue = "F1" Then

                            ''insert Foundation
                            For value As Integer = 1 To 3
                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','Foundation " & value & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        ShowMessage(" Add New Coordinator", MessageType.Success)
                                    Else
                                        ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                    End If
                                End Using
                            Next

                        ElseIf ddllevel.SelectedValue = "L1" Then

                            ''insert Level
                            For value As Integer = 1 To 2
                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','Level " & value & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        ShowMessage(" Add New Coordinator", MessageType.Success)
                                    Else
                                        ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                    End If
                                End Using
                            Next

                        Else

                            Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','" & ddllevel.SelectedValue & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                objConn.Open()
                                Dim i = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    ShowMessage(" Add New Coordinator", MessageType.Success)
                                Else
                                    ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                End If
                            End Using

                        End If

                    ElseIf ddlcourse.SelectedValue <> "" And (ddlcourse.SelectedValue = "Bahasa Antarabangsa" Or ddlcourse.SelectedValue = "AP Courses") Then

                        If ddlsubject.SelectedValue <> "" Then

                            Dim get_subject_name As String = "select distinct subject_Name from subject_info where subject_Name = '" & ddlsubject.SelectedValue & "' and subject_year = '" & ddlyear.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "'"
                            Dim data_subject_name As String = oCommon.getFieldValue(get_subject_name)

                            If ddllevel.SelectedValue = "ALL" Then

                                ''insert Foundation
                                For value As Integer = 1 To 3
                                    Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','Level " & value & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()

                                        If i <> 0 Then
                                            ShowMessage(" Add New Coordinator", MessageType.Success)
                                        Else
                                            ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                        End If
                                    End Using
                                Next

                                ''insert Level
                                For value As Integer = 1 To 2
                                    Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','Level " & value & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()

                                        If i <> 0 Then
                                            ShowMessage(" Add New Coordinator", MessageType.Success)
                                        Else
                                            ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                        End If
                                    End Using
                                Next

                            ElseIf ddllevel.SelectedValue = "F1" Then

                                ''insert Foundation
                                For value As Integer = 1 To 3
                                    Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','Level " & value & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()

                                        If i <> 0 Then
                                            ShowMessage(" Add New Coordinator", MessageType.Success)
                                        Else
                                            ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                        End If
                                    End Using
                                Next

                            ElseIf ddllevel.SelectedValue = "L1" Then

                                ''insert Level
                                For value As Integer = 1 To 2
                                    Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','Level " & value & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()

                                        If i <> 0 Then
                                            ShowMessage(" Add New Coordinator", MessageType.Success)
                                        Else
                                            ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                        End If
                                    End Using
                                Next

                            Else

                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level, program, campus) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','" & ddllevel.SelectedValue & "', '" & ddlStream.SelectedValue & "', '" & ddlCampus.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        ShowMessage(" Add New Coordinator", MessageType.Success)
                                    Else
                                        ShowMessage("Unsuccessful Add New Coordinator", MessageType.Error)
                                    End If
                                End Using

                            End If

                        Else
                            ShowMessage("Please Select Subject Name", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please Select Course Name", MessageType.Error)
                    End If

                Else
                    ShowMessage("Please Select Level", MessageType.Error)
                End If
            Else
                ShowMessage("Please Select Staff Name", MessageType.Error)
            End If
        Else
            ShowMessage("Please Select Year", MessageType.Error)
        End If

    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            courseList()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub yearListCoordinator()
        strSQL = "SELECT Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyearCoordinator.DataSource = ds
            ddlyearCoordinator.DataTextField = "Parameter"
            ddlyearCoordinator.DataValueField = "Parameter"
            ddlyearCoordinator.DataBind()
            ddlyearCoordinator.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub campusListCoordinator()
        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcampusCoordinator.DataSource = ds
            ddlcampusCoordinator.DataTextField = "Parameter"
            ddlcampusCoordinator.DataValueField = "Value"
            ddlcampusCoordinator.DataBind()
            ddlcampusCoordinator.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub programListCoordinator()
        If ddlcampusCoordinator.SelectedValue = "APP" Then
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

            ddlprogramCoordinator.DataSource = ds
            ddlprogramCoordinator.DataTextField = "Parameter"
            ddlprogramCoordinator.DataValueField = "Value"
            ddlprogramCoordinator.DataBind()
            ddlprogramCoordinator.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staffListCoordinator()
        strSQL = "  select distinct A.stf_ID, A.staff_Name from staff_Info A left join coordinator B on A.stf_ID = B.stf_ID
                    where B.year = '" & ddlyearCoordinator.SelectedValue & "' and A.staff_Campus = '" & ddlcampusCoordinator.SelectedValue & "' order by A.staff_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaffCoordinator.DataSource = ds
            ddlstaffCoordinator.DataTextField = "staff_Name"
            ddlstaffCoordinator.DataValueField = "stf_ID"
            ddlstaffCoordinator.DataBind()
            ddlstaffCoordinator.Items.Insert(0, New ListItem("Select Staff", String.Empty))
            ddlstaffCoordinator.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub courseListCoordinator()
        strSQL = "  select distinct A.course_Name from subject_info A left join coordinator B on A.course_Name = B.course_Name left join staff_Info C on B.stf_ID = C.stf_ID
                    where A.subject_year = '" & ddlyearCoordinator.SelectedValue & "' and B.year = '" & ddlyearCoordinator.SelectedValue & "' and B.stf_ID = '" & ddlstaffCoordinator.SelectedValue & "' and C.stf_ID = '" & ddlstaffCoordinator.SelectedValue & "'
                    and A.subject_Campus = '" & ddlcampusCoordinator.SelectedValue & "' and course_Program =  '" & ddlprogramCoordinator.SelectedValue & "' order by A.course_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcourseCoordinator.DataSource = ds
            ddlcourseCoordinator.DataTextField = "course_Name"
            ddlcourseCoordinator.DataValueField = "course_Name"
            ddlcourseCoordinator.DataBind()
            ddlcourseCoordinator.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlcourseCoordinator.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlcourseCoordinator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcourseCoordinator.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlcampusCoordinator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcampusCoordinator.SelectedIndexChanged
        Try
            programListCoordinator()
            courseListCoordinator()
            staffListCoordinator()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlprogramCoordinator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlprogramCoordinator.SelectedIndexChanged
        Try
            courseListCoordinator()
            staffListCoordinator()
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
                gvTable.Columns(5).Visible = True
            Else
                gvTable.Columns(5).Visible = False
            End If

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
        Dim strOrderby As String = " ORDER BY coordinator_Level, staff_Info.staff_Name, subject_Name ASC"

        tmpSQL = "select distinct coordinator_ID,staff_Name, coordinator_Level, subject_Name, year, program, campus from coordinator left join staff_Info on coordinator.stf_ID = staff_Info.stf_ID"
        strWhere = " where year = '" & ddlyearCoordinator.SelectedValue & "' and staff_info.staff_Campus = '" & ddlcampusCoordinator.SelectedValue & "' and coordinator.program = '" & ddlprogramCoordinator.SelectedValue & "' and coordinator.campus = '" & ddlcampusCoordinator.SelectedValue & "'"

        If ddlstaffCoordinator.SelectedValue <> "" Then
            strWhere += " and coordinator.stf_ID = '" & ddlstaffCoordinator.SelectedValue & "'"
        End If

        If ddlcourseCoordinator.SelectedValue <> "" Then
            strWhere += " and course_Name = '" & ddlcourseCoordinator.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Protected Sub ddlyearCoordinator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyearCoordinator.SelectedIndexChanged
        Try
            staffListCoordinator()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlstaffCoordinator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlstaffCoordinator.SelectedIndexChanged
        courseListCoordinator()
        strRet = BindData(datRespondent)
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
            Dlt_ClassData.SelectCommand.CommandText = "delete coordinator where coordinator_ID ='" & strKeyName & "'"
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
            Response.Redirect("admin_edit_coordinator.aspx?coordinator_ID=" + strKeyName + "&admin_ID=" + adminID)
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