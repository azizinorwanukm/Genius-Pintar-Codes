Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class hostel_list_managae
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

                If Session("getStatus") = "VH" Then ''View Hostel
                    txtbreadcrum1.Text = "View Hostel"

                    ViewHostel.Visible = True
                    RegisterHostel.Visible = False

                    btnViewHostel.Attributes("class") = "btn btn-info"
                    btnRegisterHostel.Attributes("class") = "btn btn-default font"

                    campus_name_list()
                    block_name_list()
                    block_level_list()
                    year_list()
                    sem_list()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "RH" Then ''Register Hostel
                    txtbreadcrum1.Text = "Register Hostel"

                    ViewHostel.Visible = False
                    RegisterHostel.Visible = True

                    btnViewHostel.Attributes("class") = "btn btn-default font"
                    btnRegisterHostel.Attributes("class") = "btn btn-info"

                    campus_name_list()
                    block_name_list()
                    block_level_list()
                    year_list()
                    sem_list()

                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewHostel.Visible = False
        btnRegisterHostel.Visible = False
        ViewHostel.Visible = False
        RegisterHostel.Visible = False

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

        Dim Get_ViewHostel As String = ""
        Dim Get_RegisterHostel As String = ""

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

            If find_Data_SubMenu2 = "View Hostel" And find_Data_SubMenu2.Length > 0 Then
                btnViewHostel.Visible = True
                ViewHostel.Visible = True

                Get_ViewHostel = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Hostel" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterHostel.Visible = True
                RegisterHostel.Visible = True

                Get_RegisterHostel = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewHostel.Visible = True
                btnRegisterHostel.Visible = True
                ViewHostel.Visible = True
                RegisterHostel.Visible = True

                Btnsimpan.Visible = True

                Get_ViewHostel = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RH" Or Session("getStatus") = "VH" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RH" And Session("getStatus") <> "VH" Then
            If Get_ViewHostel = "TRUE" Then
                Data_If_Not_Group_Status = "VH"
            ElseIf Get_RegisterHostel = "TRUE" Then
                Data_If_Not_Group_Status = "RH"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewHostel = "TRUE" And Data_If_Not_Group_Status = "VH" Then
                Session("getStatus") = "VH"
            ElseIf Get_RegisterHostel = "TRUE" And Data_If_Not_Group_Status = "RH" Then
                Session("getStatus") = "RH"
            End If
        End If

    End Sub

    Private Sub btnViewHostel_ServerClick(sender As Object, e As EventArgs) Handles btnViewHostel.ServerClick
        Session("getStatus") = "VH"
        Response.Redirect("admin_pengurusan_am_hostel.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnRegisterHostel_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterHostel.ServerClick
        Session("getStatus") = "RH"
        Response.Redirect("admin_pengurusan_am_hostel.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Protected Sub ddlBlockName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockName.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlBlockLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockLevel.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYearList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYearList.SelectedIndexChanged
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
        Dim strOrderby As String = " ORDER BY year, hostel_Sem, hostel_CampusNames, hostel_BlockNames, hostel_BlockLevels, hostel_RoomNumbers  ASC"

        tmpSQL = "Select hostel_ID, hostel_CampusNames, hostel_BlockNames, hostel_BlockLevels, hostel_RoomNumbers, year, hostel_Sem from hostel_info"

        strWhere = " WHERE hostel_info.year = '" & ddlYearList.SelectedValue & "'"

        If ddlBlockName.SelectedIndex > 0 Then
            strWhere += " AND hostel_info.hostel_BlockNames = '" & ddlBlockName.SelectedValue & "'"
        End If

        If ddlBlockLevel.SelectedIndex > 0 Then
            strWhere += " AND hostel_info.hostel_BlockLevels = '" & ddlBlockLevel.SelectedValue & "'"
        End If

        If ddlSemList.SelectedIndex > 0 Then
            strWhere += " AND hostel_info.hostel_Sem = '" & ddlSemList.SelectedValue & "'"
        End If

        If ddlCampusList.SelectedIndex > 0 Then
            strWhere += " AND hostel_info.hostel_CampusNames = '" & ddlCampusList.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Debug.WriteLine(getSQL)
        ''--debug
        Return getSQL
    End Function

    Private Sub campus_name_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Hostel_Name' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCampusList.DataSource = ds
            ddlCampusList.DataTextField = "Parameter"
            ddlCampusList.DataValueField = "Parameter"
            ddlCampusList.DataBind()
            ddlCampusList.Items.Insert(0, New ListItem("Select Campus", String.Empty))

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
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Block_Name' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlockName.DataSource = ds
            ddlBlockName.DataTextField = "Parameter"
            ddlBlockName.DataValueField = "Value"
            ddlBlockName.DataBind()
            ddlBlockName.Items.Insert(0, New ListItem("Select Block", String.Empty))

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
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Block_Level' "
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

            ddlYearList.DataSource = ds
            ddlYearList.DataTextField = "Parameter"
            ddlYearList.DataValueField = "Value"
            ddlYearList.DataBind()
            ddlYearList.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYearList.SelectedValue = Date.Today.Year

            ddlHostel_Year.DataSource = ds
            ddlHostel_Year.DataTextField = "Parameter"
            ddlHostel_Year.DataValueField = "Value"
            ddlHostel_Year.DataBind()
            ddlHostel_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlHostel_Year.SelectedValue = Date.Today.Year
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

            ddlSemList.DataSource = ds
            ddlSemList.DataTextField = "Parameter"
            ddlSemList.DataValueField = "Value"
            ddlSemList.DataBind()
            ddlSemList.Items.Insert(0, New ListItem("Select Semester", String.Empty))

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

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_asrama_data.aspx?hostelID=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
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
            Dlt_ClassData.SelectCommand.CommandText = "DELETE FROM hostel_info WHERE hostel_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub ddlSemList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemList.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCampusList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampusList.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Try

            If ddlHostel_Year.SelectedIndex > 0 Then

                If ddlHostel_Semester.SelectedIndex > 0 Then

                    If ddlHostel_Campus.SelectedIndex > 0 Then

                        If ddlHostel_Block.SelectedIndex > 0 Then

                            If ddlHostel_BlockLevel.SelectedIndex > 0 Then

                                If txtHostel_RoomQuantity.Text.Length > 0  Then

                                    If HostelChecking() = True Then

                                        strSQL = "  Insert into hostel_info(year,hostel_Sem,hostel_CampusNames,hostel_BlockNames,hostel_BlockLevels,hostel_RoomNumbers)
                                                    Values('" & ddlHostel_Year.SelectedValue & "', '" & ddlHostel_Semester.SelectedValue & "', '" & ddlHostel_Campus.SelectedValue & "', '" & ddlHostel_Block.SelectedValue & "', '" & ddlHostel_BlockLevel.SelectedValue & "', '" & txtHostel_RoomQuantity.Text & "')"
                                        strRet = oCommon.ExecuteSQL(strSQL)

                                        If strRet = "0" Then

                                            strSQL = "Select hostel_ID from hostel_info where year = '" & ddlHostel_Year.SelectedValue & "' and hostel_Sem = '" & ddlHostel_Semester.SelectedValue & "' and hostel_CampusNames = '" & ddlHostel_Campus.SelectedValue & "' and hostel_BlockNames = '" & ddlHostel_Block.SelectedValue & "' and hostel_BlockLevels = '" & ddlHostel_BlockLevel.SelectedValue & "'"
                                            Dim get_HostelID As String = oCommon.getFieldValue(strSQL)

                                            For i As Integer = 0 To Integer.Parse(txtHostel_RoomQuantity.Text.Trim) - 1

                                                strSQL = "  Insert into room_info(hostel_ID,room_Name,room_Capacity,year,room_Sem,stf_ID,created_date)
                                                            Values('" & get_HostelID & "','" & ddlHostel_Block.SelectedValue & "-" & ddlHostel_BlockLevel.SelectedValue & "-" & i + 1 & " ','2','" & ddlHostel_Year.SelectedValue & "','" & ddlHostel_Semester.SelectedValue & "','" & oCommon.Staff_securityLogin(Request.QueryString("admin_ID")) & "','" & Date.Now & "')"
                                                strRet = oCommon.ExecuteSQL(strSQL)
                                            Next

                                            If strRet = "0" Then
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

        If strRet.Length = 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class