Imports System.Data.SqlClient

Public Class student_AddHostel
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                year_list()
                level_list()
                sem_list()

                LoadPage()

                class_name_list()
                block_campus_list()
                block_name_list()
                block_level_list()
                roomInfo_name_list()
                gender_list()

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnUpdateStudentHostel.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_EdithostelInformation As String = ""
        Dim Get_EditRoomInformation As String = ""

        ''Loop The Count_No
        For num As Integer = 0 To find_CountNo_LoginID - 1 Step 1

            ''Get Main Menu Data
            strSQL = "  Select A.Menu From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_Menu_Data As String = oCommon.getFieldValue(strSQL)

            ''Get Sub Menu 1 Data
            strSQL = "  Select A.Menu_Sub1 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_SubMenu1 As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Update Data 
            strSQL = "  Select B.F1_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Update As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu1 = "Student Placement" And find_Data_SubMenu1.Length > 0 Then

                If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                    btnUpdateStudentHostel.Visible = True
                End If
            End If

            If find_Data_SubMenu1.Length = 0 And find_Data_Menu_Data = "All" Then
                btnUpdateStudentHostel.Visible = True
            End If

        Next
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Sub year_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostelYear.DataSource = ds
            ddlHostelYear.DataTextField = "Parameter"
            ddlHostelYear.DataValueField = "Value"
            ddlHostelYear.DataBind()

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub gender_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Gender' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlGenderName.DataSource = ds
            ddlGenderName.DataTextField = "Parameter"
            ddlGenderName.DataValueField = "Value"
            ddlGenderName.DataBind()
            ddlGenderName.DataBind()
            ddlGenderName.Items.Insert(0, New ListItem("Select Gender", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub level_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentLevel.DataSource = ds
            ddlStudentLevel.DataTextField = "Parameter"
            ddlStudentLevel.DataValueField = "Value"
            ddlStudentLevel.DataBind()
            ddlStudentLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub sem_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostelSem.DataSource = ds
            ddlHostelSem.DataTextField = "Parameter"
            ddlHostelSem.DataValueField = "Value"
            ddlHostelSem.DataBind()
            ddlHostelSem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_name_list()
        strSQL = "select class_ID, class_Name from class_info where class_year = '" & ddlHostelYear.SelectedValue & "' and class_Level = '" & ddlStudentLevel.SelectedValue & "' and class_type = 'Compulsory' and class_Campus = 'PGPN' order by class_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassName.DataSource = ds
            ddlClassName.DataTextField = "class_Name"
            ddlClassName.DataValueField = "class_ID"
            ddlClassName.DataBind()
            ddlClassName.Items.Insert(0, New ListItem("Select Class", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LoadPage()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' and Parameter = '" & Now.Year & "' "

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                    ddlHostelYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
                Else
                    ddlHostelYear.SelectedIndex = 0
                End If

            Else
                Debug.WriteLine("Table hostel_info return no data...")
            End If

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_campus_list()
        strSQL = "SELECT Parameter,Value from setting where Type = 'Hostel_Name'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCampusNames.DataSource = ds
            ddlCampusNames.DataTextField = "Parameter"
            ddlCampusNames.DataValueField = "Value"
            ddlCampusNames.DataBind()
            ddlCampusNames.Items.Insert(0, New ListItem("Select Campus", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_name_list()
        strSQL = "SELECT Parameter,Value from setting where Type = 'Block_Name'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlockNames.DataSource = ds
            ddlBlockNames.DataTextField = "Parameter"
            ddlBlockNames.DataValueField = "Value"
            ddlBlockNames.DataBind()
            ddlBlockNames.Items.Insert(0, New ListItem("Select Block", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_level_list()
        strSQL = "SELECT Parameter,Value from setting where Type = 'Block_Level' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlockLevel.DataSource = ds
            ddlBlockLevel.DataTextField = "Parameter"
            ddlBlockLevel.DataValueField = "Value"
            ddlBlockLevel.DataBind()
            ddlBlockLevel.Items.Insert(0, New ListItem("Select Block Level", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub roomInfo_name_list()
        strSQL = "  Select A.room_Name, A.room_ID from room_info A 
                    left join hostel_info B on A.hostel_ID = B.hostel_ID
                    where  A.year = '" & ddlHostelYear.SelectedValue & "' and B.year = '" & ddlHostelYear.SelectedValue & "'
                    and A.room_Sem = '" & ddlHostelSem.SelectedValue & "' and B.hostel_Sem = '" & ddlHostelSem.SelectedValue & "'
                    and b.hostel_CampusNames = '" & ddlCampusNames.SelectedValue & "' and B.hostel_BlockNames = '" & ddlBlockNames.SelectedValue & "' and B.hostel_BlockLevels = '" & ddlBlockLevel.SelectedValue & "'
                    order by A.room_ID asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRoomNames.DataSource = ds
            ddlRoomNames.DataTextField = "room_Name"
            ddlRoomNames.DataValueField = "room_ID"
            ddlRoomNames.DataBind()
            ddlRoomNames.Items.Insert(0, New ListItem("Select Room", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
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
        Dim strOrderby As String = " order by E.student_Level, G.class_Name, A.student_Name Asc"

        tmpSQL = "  Select distinct A.std_ID, A.student_Name, E.student_Level, E.student_Sem, G.class_Name, B.hostel_CampusNames, C.room_Name from student_info A
                    left join student_room D on A.std_ID = D.std_ID
                    left join room_info C on D.room_ID = C.room_ID
                    left join hostel_info B on C.hostel_ID = B.hostel_ID
                    left join student_Level E on A.std_ID = E.std_ID
                    left join course F on A.std_ID = F.std_ID
                    left join class_info G on F.class_ID = G.class_ID"

        strWhere = "    where (A.student_Status = 'Access' or A.student_Status = 'Graduate') and A.student_ID is not null and A.student_ID like '%M%' and G.class_type = 'Compulsory'
                        and E.year = '" & ddlHostelYear.SelectedValue & "' and F.year = '" & ddlHostelYear.SelectedValue & "' and G.class_year = '" & ddlHostelYear.SelectedValue & "'"
        strWhere += "   and E.student_Sem = '" & ddlHostelSem.SelectedValue & "' "
        strWhere += "   and E.student_Level = '" & ddlStudentLevel.SelectedValue & "'"

        If ddlClassName.SelectedIndex > 0 Then
            strWhere += "   and G.class_ID = '" & ddlClassName.SelectedValue & "'"
        End If

        If ddlGenderName.SelectedIndex > 0 Then

            Dim txtGender As String = ""

            If ddlGenderName.SelectedValue = "Male" Then
                txtGender = "LELAKI"
            ElseIf ddlGenderName.SelectedValue = "Female" Then
                txtGender = "PEREMPUAN"
            End If
            strWhere += "   and A.student_Sex = '" & txtGender & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub ddlHostelYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelYear.SelectedIndexChanged
        Try
            roomInfo_name_list()
            class_name_list()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlStudentLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStudentLevel.SelectedIndexChanged
        Try
            class_name_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlHostelSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelSem.SelectedIndexChanged
        Try
            roomInfo_name_list()
            class_name_list()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCampusNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampusNames.SelectedIndexChanged
        Try
            roomInfo_name_list()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlBlockNames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockNames.SelectedIndexChanged
        Try
            roomInfo_name_list()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlBlockLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockLevel.SelectedIndexChanged
        Try
            roomInfo_name_list()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlClassName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassName.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlGenderName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGenderName.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdateStudentHostel_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStudentHostel.ServerClick
        Try
            Dim i As Integer

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        Dim checkExistData As String = "Select id from student_room where std_ID = '" & strKey & "' and year = '" & ddlHostelYear.SelectedValue & "' and sem = '" & ddlHostelSem.SelectedValue & "'"
                        strRet = oCommon.getFieldValue(checkExistData)

                        If strRet.Length > 0 Then

                            strSQL = "update student_room set room_ID = '" & ddlRoomNames.SelectedValue & "', year = '" & ddlHostelYear.SelectedValue & "', sem = '" & ddlHostelSem.SelectedValue & "' where id = '" & strRet & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                ShowMessage(" Add Student Hostel", MessageType.Success)
                            Else
                                ShowMessage(" Unsucccessful Add Student Hostel", MessageType.Error)
                            End If
                        Else

                            strSQL = "Insert into student_room(std_ID,room_ID,year,sem) values('" & strKey & "','" & ddlRoomNames.SelectedValue & "','" & ddlHostelYear.SelectedValue & "','" & ddlHostelSem.SelectedValue & "')"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                ShowMessage(" Add Student Hostel", MessageType.Success)
                            Else
                                ShowMessage(" Unsucccessful Add Student Hostel", MessageType.Error)
                            End If
                        End If
                    End If
                End If
            Next

            strRet = BindData(datRespondent)

        Catch ex As Exception
        End Try
    End Sub

End Class