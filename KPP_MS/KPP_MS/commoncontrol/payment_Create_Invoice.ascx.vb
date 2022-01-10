Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Drawing
Imports System.Globalization

Public Class payment_Create_Invoice
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

                previousPage.NavigateUrl = String.Format("~/admin_daftar_yuran_baru.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                Checking_MenuAccess_Load()

                If Session("getStatus") = "GI" Then ''Generate Invoice
                    txtbreadcrum1.Text = "Generate Invoice"

                    GenerateInvoice.Visible = True
                    ViewInvoice.Visible = False

                    btnGenerateInvoice.Attributes("class") = "btn btn-info"
                    btnViewInvoice.Attributes("class") = "btn btn-default font"

                    Year()
                    student_Level()
                    'student_Invoice_Type()

                    strRet = BindData(datRespondent)
                    strRet = BindDataStudent(datStdRespondent)

                ElseIf Session("getStatus") = "VI" Then ''View Invoice
                    txtbreadcrum1.Text = "View Invoice"

                    GenerateInvoice.Visible = False
                    ViewInvoice.Visible = True

                    btnGenerateInvoice.Attributes("class") = "btn btn-default font"
                    btnViewInvoice.Attributes("class") = "btn btn-info"

                    Year()
                    student_Level()

                    strRet = BindDataStudentFee(datStdFeeRespondent)

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnGenerateInvoice.Visible = False
        btnViewInvoice.Visible = False

        GenerateInvoice.Visible = False
        ViewInvoice.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewInvoice As String = ""
        Dim Get_GenerateInvoice As String = ""

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
            Dim find_Data_SubMenu3 As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Register Data 
            strSQL = "  Select B.F1_Register From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Register As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 View Data 
            strSQL = "  Select B.F1_View From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1View As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu3 = "Generate Invoice" And find_Data_SubMenu3.Length > 0 Then
                btnGenerateInvoice.Visible = True
                GenerateInvoice.Visible = True

                Get_GenerateInvoice = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu3 = "View Invoice" And find_Data_SubMenu3.Length > 0 Then
                btnViewInvoice.Visible = True
                ViewInvoice.Visible = True

                Get_ViewInvoice = "TRUE"

                If find_Data_F1View.Length > 0 And find_Data_F1View = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu3.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewInvoice.Visible = True
                btnGenerateInvoice.Visible = True
                ViewInvoice.Visible = True
                GenerateInvoice.Visible = True

                Btnsimpan.Visible = True

                Get_GenerateInvoice = "TRUE"
                Session("getEditButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "GI" Or Session("getStatus") = "VI" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "GI" And Session("getStatus") <> "VI" Then
            If Get_ViewInvoice = "TRUE" Then
                Data_If_Not_Group_Status = "VI"
            ElseIf Get_GenerateInvoice = "TRUE" Then
                Data_If_Not_Group_Status = "GI"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewInvoice = "TRUE" And Data_If_Not_Group_Status = "VI" Then
                Session("getStatus") = "VI"
            ElseIf Get_GenerateInvoice = "TRUE" And Data_If_Not_Group_Status = "GI" Then
                Session("getStatus") = "GI"
            End If
        End If

    End Sub

    Private Sub btnGenerateInvoice_ServerClick(sender As Object, e As EventArgs) Handles btnGenerateInvoice.ServerClick
        Session("getStatus") = "GI"
        Response.Redirect("admin_daftar_invois.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewInvoice_ServerClick(sender As Object, e As EventArgs) Handles btnViewInvoice.ServerClick
        Session("getStatus") = "VI"
        Response.Redirect("admin_daftar_invois.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub Year()
        strSQL = "select Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear_List.DataSource = ds
            ddlYear_List.DataTextField = "Parameter"
            ddlYear_List.DataValueField = "Parameter"
            ddlYear_List.DataBind()
            ddlYear_List.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear_List.SelectedIndex = 0

            ddl_VI_year.DataSource = ds
            ddl_VI_year.DataTextField = "Parameter"
            ddl_VI_year.DataValueField = "Parameter"
            ddl_VI_year.DataBind()
            ddl_VI_year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_VI_year.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
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

            ddl_VI_level.DataSource = ds
            ddl_VI_level.DataTextField = "Parameter"
            ddl_VI_level.DataValueField = "Parameter"
            ddl_VI_level.DataBind()
            ddl_VI_level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_VI_level.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    'Private Sub student_Invoice_Type()
    '    strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
    '    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    '    Dim objConn As SqlConnection = New SqlConnection(strConn)
    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Try
    '        Dim ds As DataSet = New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        ddlInv_Type.DataSource = ds
    '        ddlInv_Type.DataTextField = "Parameter"
    '        ddlInv_Type.DataValueField = "Parameter"
    '        ddlInv_Type.DataBind()
    '        ddlInv_Type.Items.Insert(0, New ListItem("Select Student Type", String.Empty))
    '        ddlInv_Type.SelectedIndex = 0
    '    Catch ex As Exception

    '    Finally
    '        objConn.Dispose()
    '    End Try
    'End Sub

    Protected Sub ddlYear_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_List.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            strRet = BindDataStudent(datStdRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel_List.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            strRet = BindDataStudent(datStdRespondent)
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
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY FIM_ID ASC"

        tmpSQL = "select * from fee_item_master"
        strWhere = " where FIM_ID is not null "
        strWhere += " AND FIM_Year = '" & ddlYear_List.SelectedValue & "' and FIM_Level = '" & ddlLevel_List.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
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
        Dim strOrderby As String = " order by A.student_Name asc"

        tmpSQL = "  Select distinct A.std_ID, A.student_Name, A.student_ID, B.student_Level, D.class_Name, E.Parameter from student_info A
                    Left Join student_Level B on A.std_ID = B.std_ID
                    Left Join course C on A.std_ID = C.std_ID
                    Left join class_info D on C.class_ID = D.class_ID
                    Left Join setting E on A.student_Campus = E.Value"
        strWhere = "    where B.year = '" & ddlYear_List.SelectedValue & "' and C.year = '" & ddlYear_List.SelectedValue & "' and D.class_year = '" & ddlYear_List.SelectedValue & "'
                        and B.student_Level = '" & ddlLevel_List.SelectedValue & "' and D.class_Level = '" & ddlLevel_List.SelectedValue & "'
                        and A.student_Status = 'Access' and A.student_ID like '%M%' and B.Registered = 'Yes' and D.class_type = 'Compulsory'
                        and E.Type = 'Pusat Campus'"

        getSQLStudent = tmpSQL & strWhere & strOrderby

        Return getSQLStudent
    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim i As Integer
        Dim countFeeTable As Integer = 0

        Dim A As Integer
        ''Loop Fee Table For Counting
        For A = 0 To datRespondent.Rows.Count - 1 Step A + 1
            countFeeTable = countFeeTable + 1
        Next

        ''Loop Student Table
        For i = 0 To datStdRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdateStudent As CheckBox = CType(datStdRespondent.Rows(i).Cells(5).FindControl("CheckStudent"), CheckBox)
            If Not chkUpdateStudent Is Nothing Then

                ''Get Student ID (PK)
                Dim strKeyStudent As String = datStdRespondent.DataKeys(i).Value.ToString
                If chkUpdateStudent.Checked = True Then

                    ''Check Invoice Existed
                    strSQL = "Select II_ID from invoice_info where Std_ID = '" & strKeyStudent & "' and II_Year = '" & ddlYear_List.SelectedValue & "'"
                    Dim checkInvoice As String = oCommon.getFieldValue(strSQL)

                    ''If Invoice Not Exist
                    If checkInvoice.Length = 0 Then

                        Dim dateDay As String = Now.Day
                        Dim dateMonth As String = Now.Month

                        If dateDay.Length = 1 Then
                            dateDay = "0" & dateDay
                        End If

                        If dateMonth.Length = 1 Then
                            dateMonth = "0" & dateMonth
                        End If

                        ''Create Date
                        Dim dateReverse As String = Now.Year & dateMonth & dateDay

                        ''Get Fee Level
                        Dim getLevel As String = ""
                        If ddlLevel_List.SelectedValue = "Foundation 1" Then
                            getLevel = "F1"
                        ElseIf ddlLevel_List.SelectedValue = "Foundation 2" Then
                            getLevel = "F2"
                        ElseIf ddlLevel_List.SelectedValue = "Foundation 3" Then
                            getLevel = "F3"
                        ElseIf ddlLevel_List.SelectedValue = "Level 1" Then
                            getLevel = "L1"
                        ElseIf ddlLevel_List.SelectedValue = "Level 2" Then
                            getLevel = "L2"
                        End If

                        ''Create Invoice No
                        strSQL = "Select II_InvNo from invoice_info where II_Year = '" & ddlYear_List.SelectedValue & "' and II_InvNo like '%" & getLevel & "%'  order by II_InvNo Desc"
                        Dim checkExistInvNo As String = oCommon.getFieldValue(strSQL)

                        ''Get Invoice No
                        Dim InvoiceNo As String = ""

                        If checkExistInvNo.Length > 0 Then
                            Dim ID_InvNo As Integer = checkExistInvNo.Substring(checkExistInvNo.Length - 3, 3)
                            ID_InvNo = ID_InvNo + 1

                            ''Convert integer to string and add number "0" at infront
                            Dim ID_InvNO_String As String = ID_InvNo.ToString()
                            If ID_InvNO_String.Length = 1 Then
                                ID_InvNO_String = "00" & ID_InvNO_String
                            ElseIf ID_InvNO_String.Length = 2 Then
                                ID_InvNO_String = "0" & ID_InvNO_String
                            End If

                            InvoiceNo = ddlYear_List.SelectedValue & getLevel & ID_InvNO_String
                        Else
                            InvoiceNo = ddlYear_List.SelectedValue & getLevel & "001"
                        End If

                        ''Insert Student Invoice Info
                        strSQL = "Insert into invoice_info(II_Date,II_Year,II_InvNo,II_Status,Std_ID,II_PaidSetting) values('" & dateReverse & "','" & ddlYear_List.SelectedValue & "','" & InvoiceNo & "','Pending','" & strKeyStudent & "','3')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        ''Success insert data into Invoice Info
                        If strRet = "0" Then

                            Dim errorOccur As String = "1"

                            ''Get Invoice Info PK ID
                            strSQL = "Select II_ID from invoice_info where II_Year = '" & ddlYear_List.SelectedValue & "' and II_InvNo like '%" & getLevel & "%' and std_ID = '" & strKeyStudent & "' order by II_InvNo"
                            Dim get_IIID As String = oCommon.getFieldValue(strSQL)

                            Dim j As Integer = 0
                            ''Loop Fee Table
                            For j = 0 To countFeeTable - 1 Step j + 1
                                Dim chkUpdateFee As CheckBox = CType(datRespondent.Rows(j).Cells(5).FindControl("CheckAll"), CheckBox)
                                If Not chkUpdateFee Is Nothing Then

                                    ''Get Fee Item Master ID (PK)
                                    Dim strKeyFee As String = datRespondent.DataKeys(j).Value.ToString
                                    If chkUpdateFee.Checked = True Then

                                        ''Call Fee Item Data from Fee_Item_Master
                                        strSQL = "  Select * from fee_item_master where FIM_ID = '" & strKeyFee & "'"

                                        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
                                        Dim objConn As SqlConnection = New SqlConnection(strConn)
                                        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
                                        Dim ds As DataSet = New DataSet
                                        sqlDA.Fill(ds, "AnyTable")
                                        Dim MyTable As DataTable = New DataTable
                                        MyTable = ds.Tables(0)

                                        Dim FIM_Item As String = ""
                                        Dim FIM_Quantity As String = ""
                                        Dim FIM_Price As String = ""
                                        Dim FIM_Remark As String = ""

                                        If MyTable.Rows.Count > 0 Then
                                            FIM_Item = ds.Tables(0).Rows(0).Item("FIM_Item")
                                            FIM_Quantity = ds.Tables(0).Rows(0).Item("FIM_Quantity")
                                            FIM_Price = ds.Tables(0).Rows(0).Item("FIM_Price")
                                            FIM_Remark = ds.Tables(0).Rows(0).Item("FIM_Remark")
                                        End If

                                        ''Insert Student Invoice Info
                                        strSQL = "  Insert into invoice_item(II_ID,FIM_ID,IT_Year,IT_Item,IT_Quantity,IT_Price,IT_Status,IT_Desc) 
                                                    values('" & get_IIID & "','" & strKeyFee & "','" & ddlYear_List.SelectedValue & "','" & FIM_Item & "','" & FIM_Quantity & "','" & FIM_Price & "','Pending','" & FIM_Remark & "')"
                                        strRet = oCommon.ExecuteSQL(strSQL)

                                        If strRet <> "0" Then
                                            errorOccur = "0"
                                            strSQL = "Select student_Name from student_info where std_ID = '" & strKeyStudent & "'"
                                            ShowMessage("Unsuccessful generate invoice item for student " & oCommon.getFieldValue(strSQL), MessageType.Error)
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next

                            ''Update Fee Amount and Outstanding
                            Dim find_sum As String = "Select Sum(IT_Price) from invoice_item where II_ID = '" & get_IIID & "'"
                            Dim get_sum As String = oCommon.getFieldValue(find_sum)

                            strSQL = " Update invoice_info set II_FullAmount = '" & get_sum & "', II_Outstanding = '" & get_sum & "' where II_ID = '" & get_IIID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If errorOccur = "0" Then
                                Exit For
                            Else
                                ShowMessage("Successful generate invoice for student ", MessageType.Success)
                            End If
                        Else
                            ''Error insert data into Invoice Info
                            strSQL = "Select student_Name from student_info where std_ID = '" & strKeyStudent & "'"
                            ShowMessage("Unsuccessful generate invoice number for student " & oCommon.getFieldValue(strSQL), MessageType.Error)
                            Exit For
                        End If
                    Else
                        ''Invoice is Existed
                        strSQL = "Select student_Name from student_info where std_ID = '" & strKeyStudent & "'"
                        ShowMessage("Invoice had been registered for student " & oCommon.getFieldValue(strSQL), MessageType.Error)
                        Exit For
                    End If
                End If
            End If
        Next
    End Sub

    Private Function BindDataStudentFee(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLStudentFee, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            If Session("getEditButton") = "TRUE" Then
                gvTable.Columns(9).Visible = True
            Else
                gvTable.Columns(9).Visible = False
            End If

            objConn.Close()
            run_color()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQLStudentFee() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by A.student_Name asc"

        tmpSQL = "  Select distinct B.II_ID, A.student_Name, A.student_ID, F.class_Name, B.II_InvNo, B.II_Year, B.II_FullAmount, B.II_Outstanding, B.II_Status from student_info A
                    Left Join invoice_info B on A.std_ID = B.Std_ID
                    Left Join invoice_item C on B.II_ID = C.II_ID
                    Left Join fee_item_master D on C.FIM_ID = D.FIM_ID
                    Left Join course E on A.std_ID = E.std_ID
                    Left Join class_info F on E.class_ID = F.class_ID"
        strWhere = "    where C.IT_Year = '" & ddl_VI_year.SelectedValue & "' and D.FIM_Level = '" & ddl_VI_level.SelectedValue & "' and F.class_type = 'Compulsory' and F.class_year = '" & ddl_VI_year.SelectedValue & "'"

        getSQLStudentFee = tmpSQL & strWhere & strOrderby

        Return getSQLStudentFee
    End Function

    Protected Sub ddl_VI_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_VI_year.SelectedIndexChanged
        Try
            strRet = BindDataStudentFee(datStdFeeRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_VI_level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_VI_level.SelectedIndexChanged
        Try
            strRet = BindDataStudentFee(datStdFeeRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub datStdFeeRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datStdFeeRespondent.RowEditing
        Dim strKeyName As String = datStdFeeRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Dim adminID As String = Request.QueryString("admin_ID")
        Try
            Session("getStudentInvoice") = strKeyName
            Response.Redirect("admin_view_yuran_pelajar.aspx?admin_ID=" + adminID)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        For row = 0 To datStdFeeRespondent.Rows.Count - 1 Step row + 1
            lblDay = datStdFeeRespondent.Rows(row).Cells(8).FindControl("II_Status")
            If lblDay.Text = "Pending" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Red
                lblDay.ForeColor = Drawing.Color.Red
                lblDay.CssClass = "lblAbsent"

            End If

            If lblDay.Text = "Paid" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Green
                lblDay.ForeColor = Drawing.Color.Green
                lblDay.CssClass = "lblAttend"

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

End Class