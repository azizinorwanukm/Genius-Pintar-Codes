Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class payment_Transaction
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "ILO" Then ''Invoice Lock
                    txtbreadcrum1.Text = "Invoice Lock"

                    InvoiceLock.Visible = True
                    InvoiceLists.Visible = False

                    BtnInvoiceLock.Attributes("class") = "btn btn-info"
                    BtnInvoiceLists.Attributes("class") = "btn btn-default font"

                    ILO_Year()
                    ILO_Level()
                    ILO_Class()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "ILI" Then ''Invoice List
                    txtbreadcrum1.Text = "Invoice Lists"

                    InvoiceLock.Visible = False
                    InvoiceLists.Visible = True

                    BtnInvoiceLock.Attributes("class") = "btn btn-default font"
                    BtnInvoiceLists.Attributes("class") = "btn btn-info"

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        BtnInvoiceLock.Visible = False
        BtnInvoiceLists.Visible = False

        InvoiceLock.Visible = False
        InvoiceLists.Visible = False

        BtnPublish.Visible = False
        BtnPrint.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_InvoiceLock As String = ""
        Dim Get_InvoiceLists As String = ""

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

            ''Get Function Button 1 View Data 
            strSQL = "  Select B.F1_View From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1View As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Print Data 
            strSQL = "  Select B.F1_PrintInBI From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1PrintInBi As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Register Data 
            strSQL = "  Select B.F1_Register From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Register As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "Invoice Lock" And find_Data_SubMenu2.Length > 0 Then
                BtnInvoiceLock.Visible = True
                InvoiceLock.Visible = True

                Get_InvoiceLock = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    BtnPublish.Visible = True
                End If

                If find_Data_F1PrintInBi.Length > 0 And find_Data_F1PrintInBi = "TRUE" Then
                    BtnPrint.Visible = True
                End If

                If find_Data_F1View.Length > 0 And find_Data_F1View = "TRUE" Then
                    Session("getViewButton") = "TRUE"
                End If

            End If

            If find_Data_SubMenu2 = "Invoice Lists" And find_Data_SubMenu2.Length > 0 Then
                BtnInvoiceLists.Visible = True
                InvoiceLists.Visible = True

                Get_InvoiceLists = "TRUE"

            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                BtnInvoiceLock.Visible = True
                BtnInvoiceLists.Visible = True
                InvoiceLock.Visible = True
                InvoiceLists.Visible = True

                BtnPublish.Visible = True
                BtnPrint.Visible = True

                Get_InvoiceLock = "TRUE"
                Session("getViewButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "ILO" Or Session("getStatus") = "ILI" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "ILO" And Session("getStatus") <> "ILI" Then
            If Get_InvoiceLists = "TRUE" Then
                Data_If_Not_Group_Status = "ILI"
            ElseIf Get_InvoiceLock = "TRUE" Then
                Data_If_Not_Group_Status = "ILO"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_InvoiceLists = "TRUE" And Data_If_Not_Group_Status = "ILI" Then
                Session("getStatus") = "ILI"
            ElseIf Get_InvoiceLock = "TRUE" And Data_If_Not_Group_Status = "ILO" Then
                Session("getStatus") = "ILO"
            End If
        End If

    End Sub

    Private Sub BtnInvoiceLock_ServerClick(sender As Object, e As EventArgs) Handles BtnInvoiceLock.ServerClick
        Session("getStatus") = "ILO"
        Response.Redirect("admin_transaksi_yuran.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub BtnInvoiceLists_ServerClick(sender As Object, e As EventArgs) Handles BtnInvoiceLists.ServerClick
        Session("getStatus") = "ILI"
        Response.Redirect("admin_transaksi_yuran.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub ILO_Year()
        strSQL = "select Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear_InvoiceLock.DataSource = ds
            ddlYear_InvoiceLock.DataTextField = "Parameter"
            ddlYear_InvoiceLock.DataValueField = "Parameter"
            ddlYear_InvoiceLock.DataBind()
            ddlYear_InvoiceLock.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear_InvoiceLock.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ILO_Level()
        strSQL = "Select Parameter from setting where Type = 'Level' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel_InvoiceLock.DataSource = ds
            ddlLevel_InvoiceLock.DataTextField = "Parameter"
            ddlLevel_InvoiceLock.DataValueField = "Parameter"
            ddlLevel_InvoiceLock.DataBind()
            ddlLevel_InvoiceLock.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlLevel_InvoiceLock.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ILO_Class()
        strSQL = "Select class_ID, class_Name from class_info where class_year = '" & ddlYear_InvoiceLock.SelectedValue & "' and class_Level = '" & ddlLevel_InvoiceLock.SelectedValue & "' and class_type = 'Compulsory' and class_Campus = 'PGPN' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClass_InvoiceLock.DataSource = ds
            ddlClass_InvoiceLock.DataTextField = "class_Name"
            ddlClass_InvoiceLock.DataValueField = "class_ID"
            ddlClass_InvoiceLock.DataBind()
            ddlClass_InvoiceLock.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlClass_InvoiceLock.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlYear_InvoiceLock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_InvoiceLock.SelectedIndexChanged
        Try
            ILO_Class()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevel_InvoiceLock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel_InvoiceLock.SelectedIndexChanged
        Try
            ILO_Class()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlClass_InvoiceLock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass_InvoiceLock.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSearch_InvoiceLock_ServerClick(sender As Object, e As EventArgs) Handles btnSearch_InvoiceLock.ServerClick
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

            If Session("getViewButton") = "TRUE" Then
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        strOrderby = " Order By A.student_Name Asc"

        tmpSQL = "  Select distinct D.II_ID, A.student_Name, A.student_ID, C.class_Name, D.II_InvNo, D.II_Year, D.II_FullAmount, D.II_Published, D.II_Published as Status from student_info A
                    Left Join course B on A.std_ID = B.std_ID
                    Left Join class_info C on B.class_ID = C.class_ID
                    Left Join invoice_info D on A.std_ID = D.std_ID"

        strWhere = " WHERE (A.student_Status = 'Access' or A.student_Status = 'Graduate') and A.student_ID like '%M%' and A.student_Campus = 'PGPN'"
        strWhere += " and C.class_type = 'Compulsory'"
        strWhere += " and B.year = '" & ddlYear_InvoiceLock.SelectedValue & "' and C.class_year = '" & ddlYear_InvoiceLock.SelectedValue & "' and D.II_Year = '" & ddlYear_InvoiceLock.SelectedValue & "'"

        If txtstudent_data.Text.Length > 0 Then
            strWhere += " and A.student_Name like '%" & txtstudent_data.Text & "%'"
        End If

        If ddlLevel_InvoiceLock.SelectedIndex > 0 Then
            strWhere += " and C.class_Level = '" & ddlLevel_InvoiceLock.SelectedValue & "'"
        End If

        If ddlClass_InvoiceLock.SelectedIndex > 0 Then
            strWhere += " and C.class_ID = '" & ddlClass_InvoiceLock.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub BtnPublish_ServerClick(sender As Object, e As EventArgs) Handles BtnPublish.ServerClick

        Dim errorCount As Integer = 0
        Dim i As Integer = 0

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("CheckAll"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString

                If chkUpdate.Checked = True Then

                    strSQL = "UPDATE invoice_info set II_Published ='Yes' WHERE II_ID ='" & strKey & "' and II_Year = '" & ddlYear_InvoiceLock.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            End If
        Next

        If strRet = "0" Then
            ShowMessage("Successful Lock Invoice", MessageType.Success)
        Else
            ShowMessage("Unsuccessful Lock Invoice", MessageType.Error)
        End If

        strRet = BindData(datRespondent)
    End Sub

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        For row = 0 To datRespondent.Rows.Count - 1 Step row + 1
            lblDay = datRespondent.Rows(row).Cells(8).FindControl("Status")
            If lblDay.Text <> "Yes" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Red
                lblDay.ForeColor = Drawing.Color.Red
                lblDay.CssClass = "lblAbsent"

            End If

            If lblDay.Text = "Yes" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Green
                lblDay.ForeColor = Drawing.Color.Green
                lblDay.CssClass = "lblAttend"

            End If
        Next
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Session("getII_ID") = strKeyID
            Response.Redirect("admin_transaksi_yuran_view.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnPrint_ServerClick(sender As Object, e As EventArgs) Handles BtnPrint.ServerClick

        Dim errorCount As Integer = 0
        Dim i As Integer = 0

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                Dim chkRefNo As Label = DirectCast(datRespondent.Rows(i).FindControl("II_InvNo"), Label)

                If chkUpdate.Checked = True Then

                    ''Print 



                End If
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