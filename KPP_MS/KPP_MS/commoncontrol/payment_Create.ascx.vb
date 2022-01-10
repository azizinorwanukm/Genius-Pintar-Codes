Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class payment_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Dim reload As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "RPI" Then ''Register Payment
                    txtbreadcrum1.Text = "Register Payment Items"

                    RegisterPayment.Visible = True
                    ViewPayment.Visible = False

                    btnRegisterPayment.Attributes("class") = "btn btn-info"
                    btnViewPayment.Attributes("class") = "btn btn-default font"

                    Year()
                    RP_Level()
                    Fee_Type()
                    load_page()

                ElseIf Session("getStatus") = "VPI" Then ''View Payment
                    txtbreadcrum1.Text = "View Payment Items"
                    RegisterPayment.Visible = False
                    ViewPayment.Visible = True

                    btnRegisterPayment.Attributes("class") = "btn btn-default font"
                    btnViewPayment.Attributes("class") = "btn btn-info"

                    Year()
                    Fee_Type()
                    RP_Level()
                    load_page()
                    strRet = BindData(datRespondent)

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewPayment.Visible = False
        btnRegisterPayment.Visible = False

        ViewPayment.Visible = False
        RegisterPayment.Visible = False

        btnAddPayment.Visible = False
        Add_Student.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewPayment As String = ""
        Dim Get_RegisterPayment As String = ""

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

            If find_Data_SubMenu2 = "Register Payment Items" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterPayment.Visible = True
                RegisterPayment.Visible = True

                Get_RegisterPayment = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnAddPayment.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "View Payment Items" And find_Data_SubMenu2.Length > 0 Then
                btnViewPayment.Visible = True
                ViewPayment.Visible = True

                Get_ViewPayment = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Add_Student.Visible = True
                End If

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewPayment.Visible = True
                btnRegisterPayment.Visible = True
                ViewPayment.Visible = True
                RegisterPayment.Visible = True

                btnAddPayment.Visible = True
                Add_Student.Visible = True

                Get_RegisterPayment = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RPI" Or Session("getStatus") = "VPI" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RPI" And Session("getStatus") <> "VPI" Then
            If Get_ViewPayment = "TRUE" Then
                Data_If_Not_Group_Status = "VPI"
            ElseIf Get_RegisterPayment = "TRUE" Then
                Data_If_Not_Group_Status = "RPI"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewPayment = "TRUE" And Data_If_Not_Group_Status = "VPI" Then
                Session("getStatus") = "VPI"
            ElseIf Get_RegisterPayment = "TRUE" And Data_If_Not_Group_Status = "RPI" Then
                Session("getStatus") = "RPI"
            End If
        End If

    End Sub

    Private Sub btnRegisterPayment_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterPayment.ServerClick
        Session("getStatus") = "RPI"
        Response.Redirect("admin_daftar_yuran_baru.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewPayment_ServerClick(sender As Object, e As EventArgs) Handles btnViewPayment.ServerClick
        Session("getStatus") = "VPI"
        Response.Redirect("admin_daftar_yuran_baru.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub load_page()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        '' get year
        strSQL = "select Parameter from setting where Parameter = '" & Now.Year & "' and Type = 'Year'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
                ddlYear_List.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlYear.SelectedValue = ""
                ddlYear_List.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub Year()
        strSQL = "select Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear.SelectedIndex = 0

            ddlYear_List.DataSource = ds
            ddlYear_List.DataTextField = "Parameter"
            ddlYear_List.DataValueField = "Parameter"
            ddlYear_List.DataBind()
            ddlYear_List.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear_List.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub RP_Level()
        strSQL = "Select Parameter from setting where Type = 'Level' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel_List.DataSource = ds
            ddlLevel_List.DataTextField = "Parameter"
            ddlLevel_List.DataValueField = "Parameter"
            ddlLevel_List.DataBind()
            ddlLevel_List.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlLevel_List.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Fee_Type()
        strSQL = "select * from setting where Type = 'Payment_Type' and idx = 'Payment'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlType.DataSource = ds
            ddlType.DataTextField = "Parameter"
            ddlType.DataValueField = "Value"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Select Fee Type", String.Empty))
            ddlType.SelectedIndex = 0

            ddlType_List.DataSource = ds
            ddlType_List.DataTextField = "Parameter"
            ddlType_List.DataValueField = "Value"
            ddlType_List.DataBind()
            ddlType_List.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            If ddlYear_List.SelectedValue <> Now.Year Then

                ShowMessage("Unable to delete data year " & ddlYear_List.SelectedValue, MessageType.Error)

            Else

                Dim MyConnection As SqlConnection = New SqlConnection(strConn)
                Dim Dlt_ClassData As New SqlDataAdapter()

                Dim dlt_Class As String

                Dlt_ClassData.SelectCommand = New SqlCommand()
                Dlt_ClassData.SelectCommand.Connection = MyConnection
                Dlt_ClassData.SelectCommand.CommandText = "delete invoice_detail where ID_ID ='" & strKeyName & "'"
                MyConnection.Open()
                dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
                MyConnection.Close()

                ShowMessage("Remove data ", MessageType.Success)

            End If

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
        Dim strOrderby As String = " ORDER BY FIM_ID ASC"

        tmpSQL = "select * from fee_item_master"
        strWhere = " where FIM_ID is not null "
        strWhere += " AND FIM_Year = '" & ddlYear_List.SelectedValue & "' and FIM_Type = '" & ddlType_List.SelectedValue & "' and FIM_Level = '" & ddlLevel_List.SelectedValue & "'"


        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String
        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_yuran.aspx?FIM_ID=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddPayment_ServerClick(sender As Object, e As EventArgs) Handles btnAddPayment.ServerClick

        Session("Invoice_Level") = ""
        Session("Invoice_Gender") = ""
        Session("Invoice_Religion") = ""

        If rbtn_Male.Checked = True Then
            Session("Invoice_Gender") = "Male"
        End If
        If rbtn_Female.Checked = True Then
            Session("Invoice_Gender") = "Female"
        End If
        If rbtn_Both.Checked = True Then
            Session("Invoice_Gender") = "All"
        End If

        If rbtn_Muslim.Checked = True Then
            Session("Invoice_Religion") = "Muslim"
        End If
        If rbtn_NonMuslim.Checked = True Then
            Session("Invoice_Religion") = "Non-Muslim"
        End If
        If rbtn_All.Checked = True Then
            Session("Invoice_Religion") = "All"
        End If

        If ddlYear.SelectedValue = Now.Year Then

            If Inv_Name.Text.Length > 0 Then

                If CB_F1.Checked = True Or CB_F2.Checked = True Or CB_F3.Checked = True Or CB_L1.Checked = True Or CB_L2.Checked = True Then

                    If Std_Price.Text.Length > 0 Then

                        If Session("Invoice_Gender").Length > 0 Then

                            If Session("Invoice_Religion").Length > 0 Then

                                If ddlType.SelectedIndex > 0 Then

                                    If CB_F1.Checked = True Then
                                        Session("Invoice_Level") = "Foundation 1"
                                        RegisterInvoice()
                                    End If

                                    If CB_F2.Checked = True Then
                                        Session("Invoice_Level") = "Foundation 2"
                                        RegisterInvoice()
                                    End If

                                    If CB_F3.Checked = True Then
                                        Session("Invoice_Level") = "Foundation 3"
                                        RegisterInvoice()
                                    End If

                                    If CB_L1.Checked = True Then
                                        Session("Invoice_Level") = "Level 1"
                                        RegisterInvoice()
                                    End If

                                    If CB_L2.Checked = True Then
                                        Session("Invoice_Level") = "Level 2"
                                        RegisterInvoice()
                                    End If

                                    If strRet = 0 Then
                                        ShowMessage("Add New Payment Items", MessageType.Success)
                                    Else
                                        ShowMessage("Unsuccessful Add New Payment", MessageType.Error)
                                    End If

                                Else
                                    ShowMessage("Please select type of fee", MessageType.Error)
                                End If
                            Else
                                ShowMessage("Please select religion", MessageType.Error)
                            End If
                        Else
                            ShowMessage("Please select gender", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please enter price", MessageType.Error)
                    End If

                Else
                    ShowMessage("Please select level", MessageType.Error)
                End If
            Else
                ShowMessage("Please enter a valid invoice name", MessageType.Error)
            End If
        Else
            ShowMessage("Unable to select year " & ddlYear.SelectedValue, MessageType.Error)
        End If

    End Sub

    Private Sub RegisterInvoice()

        strSQL = "  Select FIM_ID from fee_item_master
                    Where FIM_Year = '" & ddlYear.SelectedValue & "' and FIM_Item = '" & Inv_Name.Text & "' and FIM_Quantity = '" & Inv_Quantity.Text & "' and FIM_Price = '" & Std_Price.Text & "' and FIM_Gender = '" & Session("Invoice_Gender") & "' and FIM_Type = '" & ddlType.SelectedValue & "' and FIM_Remark = '" & Inv_Remark.Text & "' and FIM_Religion = '" & Session("Invoice_Religion") & "' and FIM_Level = '" & Session("Invoice_Level") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length = 0 Then

            strSQL = "  INSERT INTO fee_item_master(FIM_Year, FIM_Item, FIM_Quantity, FIM_Price, FIM_Gender, FIM_Type, FIM_Remark, FIM_Religion, FIM_Level)
                        VALUES ('" & ddlYear.SelectedValue & "', '" & Inv_Name.Text & "', '" & Inv_Quantity.Text & "', '" & Std_Price.Text & "', '" & Session("Invoice_Gender") & "', '" & ddlType.SelectedValue & "', '" & Inv_Remark.Text & "', '" & Session("Invoice_Religion") & "', '" & Session("Invoice_Level") & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

        Else
            ShowMessage("The data had been registered for Year " & ddlYear.SelectedValue, MessageType.Error)
        End If

    End Sub

    Private Sub Add_Student_ServerClick(sender As Object, e As EventArgs) Handles Add_Student.ServerClick
        Try
            Response.Redirect("admin_daftar_invois.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Protected Sub ddlYear_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_List.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlType_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType_List.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlLevel_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel_List.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class