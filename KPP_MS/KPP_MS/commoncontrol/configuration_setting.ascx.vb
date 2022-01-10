Imports System.Data.SqlClient

Public Class configuration_setting
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

                If Session("getStatus") = "VSC" Then ''View System Configuration
                    txtbreadcrum1.Text = "View System Configuration"

                    ViewSystemConfiguration.Visible = True
                    RegisterSystemConfiguration.Visible = False

                    btnViewSystemConfiguration.Attributes("class") = "btn btn-info"
                    btnRegisterSystemConfiguration.Attributes("class") = "btn btn-default font"

                    Genre_List()
                    Type_List()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "RSC" Then ''Register System Configuration
                    txtbreadcrum1.Text = "Register System Configuration"

                    ViewSystemConfiguration.Visible = False
                    RegisterSystemConfiguration.Visible = True

                    btnViewSystemConfiguration.Attributes("class") = "btn btn-default font"
                    btnRegisterSystemConfiguration.Attributes("class") = "btn btn-info"

                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewSystemConfiguration.Visible = False
        btnRegisterSystemConfiguration.Visible = False
        ViewSystemConfiguration.Visible = False
        RegisterSystemConfiguration.Visible = False
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

        Dim Get_ViewSystemConfiguration As String = ""
        Dim Get_RegistersystemConfiguration As String = ""

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

            If find_Data_SubMenu2 = "View System Configuration" And find_Data_SubMenu2.Length > 0 Then
                btnViewSystemConfiguration.Visible = True
                ViewSystemConfiguration.Visible = True

                Get_ViewSystemConfiguration = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register System Configuration" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterSystemConfiguration.Visible = True
                RegisterSystemConfiguration.Visible = True

                Get_RegistersystemConfiguration = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewSystemConfiguration.Visible = True
                btnRegisterSystemConfiguration.Visible = True
                ViewSystemConfiguration.Visible = True
                RegisterSystemConfiguration.Visible = True
                Btnsimpan.Visible = True

                Get_ViewSystemConfiguration = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RSC" Or Session("getStatus") = "VSC" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RSC" And Session("getStatus") <> "VSC" Then
            If Get_ViewSystemConfiguration = "TRUE" Then
                Data_If_Not_Group_Status = "VSC"
            ElseIf Get_RegistersystemConfiguration = "TRUE" Then
                Data_If_Not_Group_Status = "RSC"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewSystemConfiguration = "TRUE" And Data_If_Not_Group_Status = "VSC" Then
                Session("getStatus") = "VSC"
            ElseIf Get_RegistersystemConfiguration = "TRUE" And Data_If_Not_Group_Status = "RSC" Then
                Session("getStatus") = "RSC"
            End If
        End If

    End Sub

    Private Sub btnViewSystemConfiguration_ServerClick(sender As Object, e As EventArgs) Handles btnViewSystemConfiguration.ServerClick
        Session("getStatus") = "VSC"
        Response.Redirect("admin_konfigurasi.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnRegisterSystemConfiguration_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterSystemConfiguration.ServerClick
        Session("getStatus") = "RSC"
        Response.Redirect("admin_konfigurasi.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        If txtParameter.Text <> "" Then

            If txtValue.Text <> "" Then

                If txtType.Text <> "" And Regex.IsMatch(txtType.Text, "^[A-Za-z0-9 ]+$") Then

                    If txtGenre.Text <> "" And Regex.IsMatch(txtGenre.Text, "^[A-Za-z0-9 ]+$") Then

                        strSQL = "Select ID from setting where Parameter = '" & txtParameter.Text & "' and Value = '" & txtValue.Text & "' and Type = '" & txtType.Text & "' and idx = '" & txtGenre.Text & "'"
                        strRet = oCommon.getFieldValue(strSQL)

                        If strRet.Length > 0 Then
                            ShowMessage(" The Data Has Been Registered ", MessageType.Error)

                        Else
                            strSQL = "Insert into setting (Parameter,Value,Type,idx) values ('" & txtParameter.Text & "','" & txtValue.Text & "','" & txtType.Text & "','" & txtGenre.Text & "')"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                ShowMessage(" Register System Configuration ", MessageType.Success)
                            Else
                                ShowMessage(" Unsuccessful Register System Configuration ", MessageType.Error)
                            End If

                        End If
                    Else
                        ShowMessage("  Please Fill In Genre Correctly ", MessageType.Error)
                    End If
                Else
                    ShowMessage("  Please Fill In Type Correctly ", MessageType.Error)
                End If
            Else
                ShowMessage("  Please Fill In Value Correctly ", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Fill In Paremeter Correctly ", MessageType.Error)
        End If

    End Sub

    Private Sub Genre_List()

        strSQL = "select distinct idx from setting where  idx is not null order by idx asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlGenre.DataSource = ds
            ddlGenre.DataTextField = "idx"
            ddlGenre.DataValueField = "idx"
            ddlGenre.DataBind()
            ddlGenre.Items.Insert(0, New ListItem("Select Genre", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Type_List()

        strSQL = "Select distinct Type from setting where idx = '" & ddlGenre.SelectedValue & "' order by TYpe asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlType.DataSource = ds
            ddlType.DataTextField = "Type"
            ddlType.DataValueField = "Type"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Select Type", String.Empty))

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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY idx, Type ASC"

        tmpSQL = "Select * from setting "
        strWhere = " Where ID is not null and idx = '" & ddlGenre.SelectedValue & "'"

        If ddlType.SelectedIndex > 0 Then
            strWhere += " and type = '" & ddlType.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing

        datRespondent.EditIndex = e.NewEditIndex
        Me.BindData(datRespondent)

    End Sub

    Protected Sub OnRowCancelingEdit(sender As Object, e As EventArgs)
        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim parametertxtbox As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtParameter_Data"), TextBox)
        Dim valuetxtbox As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtValue_Data"), TextBox)

        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''update marks
        strSQL = "UPDATE setting SET Parameter='" & parametertxtbox.Text & "',Value ='" & valuetxtbox.Text & "' WHERE ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub ddlGenre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGenre.SelectedIndexChanged
        Try
            Type_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete setting where ID='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

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