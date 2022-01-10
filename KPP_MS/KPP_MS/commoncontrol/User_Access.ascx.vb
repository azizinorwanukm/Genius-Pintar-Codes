Imports System.Data.SqlClient

Public Class User_Access
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

                StatusPosition.Visible = False
                StudentDisplayData.Visible = False
                StaffDisplayData.Visible = False

                data_user_list()
                data_akses_list()
                data_position_list()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnUpdateData.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewSystemConfiguration As String = ""
        Dim Get_RegistersystemConfiguration As String = ""

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

            If find_Data_SubMenu1 = "User Access" And find_Data_SubMenu1.Length > 0 Then
                btnUpdateData.Visible = True
            End If

            If find_Data_SubMenu1.Length = 0 And find_Data_Menu_Data = "All" Then
                btnUpdateData.Visible = True
            End If

        Next

    End Sub

    Private Sub data_position_list()
        strSQL = "select Parameter,Value from setting where (Type = 'Level Access' or Type = 'Position' or Type = 'Admin Access') and idx = 'Admin'  order by Parameter ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPosition.DataSource = ds
            ddlPosition.DataTextField = "Parameter"
            ddlPosition.DataValueField = "Value"
            ddlPosition.DataBind()
            ddlPosition.Items.Insert(0, New ListItem("Select Position", String.Empty))
            ddlPosition.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub data_user_list()
        strSQL = "select Parameter from setting where Type = 'TEST GOD'" ''must be error or null

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUser.DataSource = ds
            ddlUser.DataTextField = "Parameter"
            ddlUser.DataValueField = "Parameter"
            ddlUser.DataBind()
            ddlUser.Items.Insert(0, New ListItem("Select User Type", String.Empty))
            ddlUser.Items.Insert(1, New ListItem("Staff", "Staff"))
            ddlUser.Items.Insert(2, New ListItem("Student", "Student"))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub data_akses_list()
        strSQL = "select Parameter from setting where Type = 'Status'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAccess.DataSource = ds
            ddlAccess.DataTextField = "Parameter"
            ddlAccess.DataValueField = "Parameter"
            ddlAccess.DataBind()
            ddlAccess.Items.Insert(0, New ListItem("Select Access", String.Empty))

            ddlUpdateStatus.DataSource = ds
            ddlUpdateStatus.DataTextField = "Parameter"
            ddlUpdateStatus.DataValueField = "Parameter"
            ddlUpdateStatus.DataBind()
            ddlUpdateStatus.Items.Insert(0, New ListItem("Select Status", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataStaff(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLStaff, strConn)
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

    Private Function getSQLStaff() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY staff_Name ASC"

        tmpSQL = "select staff_Login.login_ID, staff_Info.staff_Name, staff_Login.staff_Login, staff_Login.staff_Password, staff_Login.staff_Status, staff_Login.staff_Access from staff_Info
                  left join staff_Login on staff_Info.stf_ID = staff_Login.stf_ID"

        strWhere = " where staff_Login.staff_Status = '" & ddlAccess.SelectedValue & "'
                     and staff_Login.staff_Access <> '' and staff_Name not like '%araken%'"

        If ddlPosition.SelectedValue <> "" And ddlPosition.SelectedValue <> "All" Then
            strWhere += " And staff_Login.staff_Access = '" & ddlPosition.SelectedValue & "'"
        End If

        getSQLStaff = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQLStaff
    End Function

    Private Function BindDataStudent(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLStudent, strConn)
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

    Private Function getSQLStudent() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select * from student_info where student_ID Is Not null And student_ID <> '' and student_ID like '%M%'"

        strWhere = " and student_Status = '" & ddlAccess.SelectedValue & "' "

        getSQLStudent = tmpSQL & strWhere & strOrderby

        Return getSQLStudent

    End Function

    Protected Sub ddlAccess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccess.SelectedIndexChanged
        Try
            If ddlUser.SelectedValue = "Student" Then
                strRet = BindDataStudent(datRespondentStudent)
            Else
                strRet = BindDataStaff(datRespondentStaff)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUser.SelectedIndexChanged
        Try
            data_akses_list()
            data_position_list()

            If ddlUser.SelectedValue = "Student" Then
                StatusPosition.Visible = False
                StudentDisplayData.Visible = True
                StaffDisplayData.Visible = False

                strRet = BindDataStudent(datRespondentStudent)
            Else
                StatusPosition.Visible = True
                StaffDisplayData.Visible = True
                StudentDisplayData.Visible = False

                strRet = BindDataStaff(datRespondentStaff)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlPosition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPosition.SelectedIndexChanged
        Try
            data_akses_list()

            If ddlUser.SelectedValue = "Student" Then
                strRet = BindDataStudent(datRespondentStudent)
            Else
                strRet = BindDataStaff(datRespondentStaff)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdateData_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateData.ServerClick

        Dim i As Integer

        If ddlUser.SelectedValue = "Student" Then

            For i = 0 To datRespondentStudent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondentStudent.Rows(i).Cells(5).FindControl("chkSelectStudent"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondentStudent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        strSQL = "Update student_info set student_Status = '" & ddlUpdateStatus.SelectedValue & "', student_LoginAttempt = '0' where std_ID = '" & strKey & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                End If
            Next

            If strRet = "0" Then
                ShowMessage(" Update Student Status", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Student Status", MessageType.Error)
            End If

        Else

            For i = 0 To datRespondentStaff.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondentStaff.Rows(i).Cells(5).FindControl("chkSelectStaff"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondentStaff.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        strSQL = "Update staff_Login set staff_Status = '" & ddlUpdateStatus.SelectedValue & "', staff_LoginAttempt = '0' where login_ID = '" & strKey & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                End If

            Next

            If strRet = "0" Then
                ShowMessage(" Update Staff Status", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Staff Status", MessageType.Error)
            End If

        End If

        If ddlUser.SelectedValue = "Student" Then
            strRet = BindDataStudent(datRespondentStudent)
        Else
            strRet = BindDataStaff(datRespondentStaff)
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