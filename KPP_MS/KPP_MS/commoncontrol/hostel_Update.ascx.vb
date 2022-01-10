Imports System.Data.SqlClient

Public Class hostel_Update
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

                previousPage.NavigateUrl = String.Format("~/admin_pengurusan_am_hostel.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                Checking_MenuAccess_Load()

                If Session("getStatus") = "EHI" Then ''Edit Hostel Information
                    txtbreadcrum1.Text = "Edit Hostel Information"

                    EditHostelInformation.Visible = True
                    EditRoomInformation.Visible = False

                    btnEditHostelInformation.Attributes("class") = "btn btn-info"
                    btnEditRoomInformation.Attributes("class") = "btn btn-default font"

                    block_campus_list()
                    block_name_list()
                    block_level_list()
                    year_list()
                    sem_list()

                    LoadHostelInfo(Request.QueryString("hostelID"))

                ElseIf Session("getStatus") = "ERI" Then ''Edit Room Information
                    txtbreadcrum1.Text = "Edit Room Information"

                    EditHostelInformation.Visible = False
                    EditRoomInformation.Visible = True

                    btnEditHostelInformation.Attributes("class") = "btn btn-default font"
                    btnEditRoomInformation.Attributes("class") = "btn btn-info"

                    strRet = BindData(datRespondent)
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnEditHostelInformation.Visible = False
        btnEditRoomInformation.Visible = False
        EditHostelInformation.Visible = False
        EditRoomInformation.Visible = False

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

        Dim Get_EdithostelInformation As String = ""
        Dim Get_EditRoomInformation As String = ""

        ''Loop The Count_No
        For num As Integer = 0 To find_CountNo_LoginID - 1 Step 1

            ''Get Main Menu Data
            strSQL = "  Select A.Menu From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_Menu_Data As String = oCommon.getFieldValue(strSQL)

            ''Get Sub Menu 3 Data
            strSQL = "  Select A.Menu_Sub3 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_SubMenu2 As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Edit Data 
            strSQL = "  Select B.F2_Edit From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Edit As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Delete Data 
            strSQL = "  Select B.F2_Delete From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Delete As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Update Data 
            strSQL = "  Select B.F2_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Update As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "Edit Hostel Information" And find_Data_SubMenu2.Length > 0 Then
                btnEditHostelInformation.Visible = True
                EditHostelInformation.Visible = True

                Get_EdithostelInformation = "TRUE"

                If find_Data_F2Update.Length > 0 And find_Data_F2Update = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Edit Room Information" And find_Data_SubMenu2.Length > 0 Then
                btnEditRoomInformation.Visible = True
                EditRoomInformation.Visible = True

                Get_EditRoomInformation = "TRUE"

                If find_Data_F2Edit.Length > 0 And find_Data_F2Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F2Delete.Length > 0 And find_Data_F2Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnEditRoomInformation.Visible = True
                btnEditHostelInformation.Visible = True
                EditHostelInformation.Visible = True
                EditRoomInformation.Visible = True

                Btnsimpan.Visible = True

                Get_EdithostelInformation = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "EHI" Or Session("getStatus") = "ERI" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "EHI" And Session("getStatus") <> "ERI" Then
            If Get_EdithostelInformation = "TRUE" Then
                Data_If_Not_Group_Status = "EHI"
            ElseIf Get_EditRoomInformation = "TRUE" Then
                Data_If_Not_Group_Status = "ERI"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_EdithostelInformation = "TRUE" And Data_If_Not_Group_Status = "EHI" Then
                Session("getStatus") = "EHI"
            ElseIf Get_EditRoomInformation = "TRUE" And Data_If_Not_Group_Status = "ERI" Then
                Session("getStatus") = "ERI"
            End If
        End If

    End Sub

    Private Sub btnEditHostelInformation_ServerClick(sender As Object, e As EventArgs) Handles btnEditHostelInformation.ServerClick
        Session("getStatus") = "EHI"
        Response.Redirect("admin_edit_asrama_data.aspx?hostelID=" + Request.QueryString("hostelID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnEditRoomInformation_ServerClick(sender As Object, e As EventArgs) Handles btnEditRoomInformation.ServerClick
        Session("getStatus") = "ERI"
        Response.Redirect("admin_edit_asrama_data.aspx?hostelID=" + Request.QueryString("hostelID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub block_campus_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Hostel_Name' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostel_Campus.DataSource = ds
            ddlHostel_Campus.DataTextField = "Parameter"
            ddlHostel_Campus.DataValueField = "Parameter"
            ddlHostel_Campus.DataBind()
            ddlHostel_Campus.Items.Insert(0, New ListItem("Select Campus", String.Empty))
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

            ddlHostel_Block.DataSource = ds
            ddlHostel_Block.DataTextField = "Parameter"
            ddlHostel_Block.DataValueField = "Value"
            ddlHostel_Block.DataBind()
            ddlHostel_Block.Items.Insert(0, New ListItem("Select Block", String.Empty))
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

            ddlHostel_BlockLevel.DataSource = ds
            ddlHostel_BlockLevel.DataTextField = "Parameter"
            ddlHostel_BlockLevel.DataValueField = "Value"
            ddlHostel_BlockLevel.DataBind()
            ddlHostel_BlockLevel.Items.Insert(0, New ListItem("Select Block Level", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostel_Year.DataSource = ds
            ddlHostel_Year.DataTextField = "Parameter"
            ddlHostel_Year.DataValueField = "Value"
            ddlHostel_Year.DataBind()
            ddlHostel_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
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

            ddlHostel_Semester.DataSource = ds
            ddlHostel_Semester.DataTextField = "Parameter"
            ddlHostel_Semester.DataValueField = "Value"
            ddlHostel_Semester.DataBind()
            ddlHostel_Semester.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LoadHostelInfo(ByVal hostelID As String)
        strSQL = "SELECT * FROM hostel_info WHERE hostel_ID = '" & hostelID & "'"

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_CampusNames")) Then
                    ddlHostel_Campus.SelectedValue = ds.Tables(0).Rows(0).Item("hostel_CampusNames")
                Else
                    ddlHostel_Campus.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_BlockNames")) Then
                    ddlHostel_Block.SelectedValue = ds.Tables(0).Rows(0).Item("hostel_BlockNames")
                Else
                    ddlHostel_Block.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_BlockLevels")) Then
                    ddlHostel_BlockLevel.SelectedValue = ds.Tables(0).Rows(0).Item("hostel_BlockLevels")
                Else
                    ddlHostel_BlockLevel.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                    ddlHostel_Year.SelectedValue = ds.Tables(0).Rows(0).Item("year")
                Else
                    ddlHostel_Year.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_Sem")) Then
                    ddlHostel_Semester.SelectedValue = ds.Tables(0).Rows(0).Item("hostel_Sem")
                Else
                    ddlHostel_Semester.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_RoomNumbers")) Then
                    txtHostel_RoomQuantity.Text = ds.Tables(0).Rows(0).Item("hostel_RoomNumbers")
                Else
                    txtHostel_RoomQuantity.Text = ""
                End If

            Else
                Debug.WriteLine("Table hostel_info return no data...")
            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Try

            If ddlHostel_Year.SelectedIndex > 0 Then

                If ddlHostel_Semester.SelectedIndex > 0 Then

                    If ddlHostel_Campus.SelectedIndex > 0 Then

                        If ddlHostel_Block.SelectedIndex > 0 Then

                            If ddlHostel_BlockLevel.SelectedIndex > 0 Then

                                If txtHostel_RoomQuantity.Text.Length > 0 Then

                                    If HostelChecking() = True Then

                                        strSQL = "  Update hostel_info
                                                    Set year ='" & ddlHostel_Year.SelectedValue & "', hostel_Sem = '" & ddlHostel_Semester.SelectedValue & "', hostel_CampusNames = '" & ddlHostel_Campus.SelectedValue & "',
                                                    hostel_BlockNames = '" & ddlHostel_Block.SelectedValue & "', hostel_BlockLevels = '" & ddlHostel_BlockLevel.SelectedValue & "', hostel_RoomNumbers = '" & txtHostel_RoomQuantity.Text & "'
                                                    Where hostel_ID = '" & Request.QueryString("hostelID") & "'"
                                        strRet = oCommon.ExecuteSQL(strSQL)

                                        If strRet = 0 Then

                                            strSQL = "Delete room_info where hostel_ID = '" & Request.QueryString("hostelID") & "'"
                                            strRet = oCommon.ExecuteSQL(strSQL)

                                            For i As Integer = 0 To Integer.Parse(txtHostel_RoomQuantity.Text.Trim) - 1

                                                strSQL = "  Insert into room_info(hostel_ID,room_Name,room_Capacity,year,room_Sem,stf_ID,created_date)
                                                            Values('" & Request.QueryString("hostelID") & "','" & ddlHostel_Block.SelectedValue & "-" & ddlHostel_BlockLevel.SelectedValue & "-" & i + 1 & " ','2','" & ddlHostel_Year.SelectedValue & "','" & ddlHostel_Semester.SelectedValue & "','" & oCommon.Staff_securityLogin(Request.QueryString("admin_ID")) & "','" & Date.Now & "')"
                                                strRet = oCommon.ExecuteSQL(strSQL)
                                            Next

                                            If strRet = 0 Then
                                                ShowMessage("Successful Create Hostel & Room", MessageType.Success)
                                            Else
                                                ShowMessage("Unsuccessful Create Room", MessageType.Error)
                                            End If

                                        Else
                                            ShowMessage("Unsuccessful Create Hostel", MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage("The Hostel Information Had Been Registered", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage("Please Enter Room Quantity", MessageType.Error)
                                End If
                            Else
                                ShowMessage("Please Select Block Level", MessageType.Error)
                            End If
                        Else
                            ShowMessage("Please Select Block", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please Select Campus", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please Select Semester", MessageType.Error)
                End If
            Else
                ShowMessage("Please Select Year", MessageType.Error)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function HostelChecking() As Boolean

        strSQL = "Select hostel_ID from hostel_info where year = '" & ddlHostel_Year.SelectedValue & "' and hostel_Sem = '" & ddlHostel_Semester.SelectedValue & "' and hostel_CampusNames = '" & ddlHostel_Campus.SelectedValue & "' and hostel_BlockNames = '" & ddlHostel_Block.SelectedValue & "' and hostel_BlockLevels = '" & ddlHostel_BlockLevel.SelectedValue & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            If Session("getEditButton") = "TRUE" Then
                gvTable.Columns(8).Visible = True
            Else
                gvTable.Columns(8).Visible = False
            End If

            If Session("getDeleteButton") = "TRUE" Then
                gvTable.Columns(9).Visible = True
            Else
                gvTable.Columns(9).Visible = False
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
        Dim strOrderby As String = " ORDER BY room_ID ASC"

        tmpSQL = "Select * from room_info left join hostel_info on room_info.hostel_ID = hostel_info.hostel_ID"
        strWhere = " WHERE hostel_info.hostel_ID = '" & Request.QueryString("hostelID") & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyCode As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_NewCourse As New SqlDataAdapter()

            Dim dlt_Course As String

            Dlt_NewCourse.SelectCommand = New SqlCommand()
            Dlt_NewCourse.SelectCommand.Connection = MyConnection
            Dlt_NewCourse.SelectCommand.CommandText = "delete room_info where room_ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Course = Dlt_NewCourse.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
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
        Dim room_name As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtroom_Name"), TextBox)
        Dim room_capacity As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtroom_Capacity"), TextBox)
        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        strSQL = "UPDATE room_info SET room_Name ='" & room_name.Text & "',room_Capacity ='" & room_capacity.Text & "' WHERE room_ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = 0 Then
            ShowMessage("Update Room Info", MessageType.Success)
        Else
            ShowMessage("Unsuccessful Update Room Info", MessageType.Error)
        End If

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class