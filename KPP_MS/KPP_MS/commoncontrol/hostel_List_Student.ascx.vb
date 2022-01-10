Imports System.Data.SqlClient

Public Class hostel_List_Student
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

                HostelYear_List()
                HostelSem_List()
                HostelCampus_List()
                HostelBlock_List()
                HostelFloor_List()

                strRet = BindData(datRespondent)
                ''Generate_Table()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

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

            If find_Data_SubMenu2 = "View Information" And find_Data_SubMenu2.Length > 0 Then

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                Session("getDeleteButton") = "TRUE"
            End If

        Next
    End Sub

    Private Sub HostelYear_List()
        strSQL = "select distinct year from hostel_info order by year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostel_Year.DataSource = ds
            ddlHostel_Year.DataTextField = "year"
            ddlHostel_Year.DataValueField = "year"
            ddlHostel_Year.DataBind()
            ddlHostel_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub HostelSem_List()
        strSQL = "select Parameter, Value from setting where type = 'Sem' "
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

    Private Sub HostelCampus_List()
        strSQL = "select Parameter, Value from setting where type = 'hostel_Name' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostel_Campus.DataSource = ds
            ddlHostel_Campus.DataTextField = "Parameter"
            ddlHostel_Campus.DataValueField = "Value"
            ddlHostel_Campus.DataBind()
            ddlHostel_Campus.Items.Insert(0, New ListItem("Select Campus", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub HostelBlock_List()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Block_Name' "
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
            ddlHostel_Block.Items.Insert(0, New ListItem("Select Floor", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub HostelFloor_List()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Block_Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostel_Floor.DataSource = ds
            ddlHostel_Floor.DataTextField = "Parameter"
            ddlHostel_Floor.DataValueField = "Value"
            ddlHostel_Floor.DataBind()
            ddlHostel_Floor.Items.Insert(0, New ListItem("Select Floor", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlHostel_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostel_Year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlHostel_Semester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostel_Semester.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlHostel_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostel_Campus.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlHostel_Block_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostel_Block.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlHostel_Floor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostel_Floor.SelectedIndexChanged
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
        Dim strOrderby As String = " order by A.hostel_CampusNames ASC, A.hostel_BlockNames ASC, A.hostel_BlockLevels ASC, B.room_Name ASC, D.student_Name ASC"

        tmpSQL = "  select C.id,A.year,C.sem,A.hostel_CampusNames, A.hostel_BlockNames, A.hostel_BlockLevels, B.room_Name, D.student_Name, D.student_ID from hostel_info A
                    left join room_info B on A.hostel_ID = B.hostel_ID
                    left join student_room C on B.room_ID = C.room_ID
                    left join student_info D on C.std_ID = D.std_ID
                    left join student_level E on D.std_ID = E.std_ID"

        strWhere = "    where A.year = '" & ddlHostel_Year.SelectedValue & "' and B.year = '" & ddlHostel_Year.SelectedValue & "' and E.year = '" & ddlHostel_Year.SelectedValue & "'
                        and D.student_Status = 'Access' and D.student_ID is not null and D.student_ID like '%M%'"

        strWhere += " and C.sem = '" & ddlHostel_Semester.SelectedValue & "' and E.student_Sem = '" & ddlHostel_Semester.SelectedValue & "'"


        If ddlHostel_Campus.SelectedIndex > 0 Then
            strWhere += " and A.hostel_CampusNames = '" & ddlHostel_Campus.SelectedValue & "'"
        End If

        If ddlHostel_Block.SelectedIndex > 0 Then
            strWhere += " AND A.hostel_BlockNames = '" & ddlHostel_Block.SelectedValue & "'"
        End If

        If ddlHostel_Floor.SelectedIndex > 0 Then
            strWhere += " and A.hostel_BlockLevels = '" & ddlHostel_Floor.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String
            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "DELETE FROM student_room WHERE id ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
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