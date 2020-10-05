Imports System.Data.SqlClient

Public Class admin_access
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

                ddlPosition.Enabled = False

                admin_info()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub admin_info()
        strSQL = "select distinct staff_Info.stf_ID, staff_Info.staff_Name from staff_info
                  left join staff_Login on staff_info.stf_ID = staff_Login.stf_ID
                  where staff_info.staff_Status = 'Access' and staff_Login.staff_Status = 'Access'
                  order by staff_info.staff_Name"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddladmin.DataSource = ds
            ddladmin.DataTextField = "staff_Name"
            ddladmin.DataValueField = "stf_ID"
            ddladmin.DataBind()
            ddladmin.Items.Insert(0, New ListItem("Select Admin", String.Empty))
            ddladmin.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub position_info()
        strSQL = "select C.Value, C.Parameter from staff_Login A
                  left join staff_Info B on A.stf_ID = B.stf_ID
                  left join setting C on A.staff_Access = C.Value
                  where A.staff_PositionNo <> 'Position 3'
                  and A.stf_ID = '" & ddladmin.SelectedValue & "'
                  and C.idx = 'Admin' and C.Type <> 'Position'
                  order by A.staff_PositionNo ASC"

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
            ddlPosition.Items.Insert(0, New ListItem("Select User Position", String.Empty))
            ddlPosition.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub menu_info()
        strSQL = "select distinct Menu, Menu_String_ID from menu_master"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMenu.DataSource = ds
            ddlMenu.DataTextField = "Menu"
            ddlMenu.DataValueField = "Menu_String_ID"
            ddlMenu.DataBind()
            ddlMenu.Items.Insert(0, New ListItem("Select Menu", String.Empty))
            ddlMenu.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub submenu_info()
        strSQL = "select distinct Sub_Menu, Admin_Menu_ID from menu_master where Menu_string_ID = '" & ddlMenu.SelectedValue & "' order by Admin_Menu_ID ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubMenu.DataSource = ds
            ddlSubMenu.DataTextField = "Sub_Menu"
            ddlSubMenu.DataValueField = "Admin_Menu_ID"
            ddlSubMenu.DataBind()
            ddlSubMenu.Items.Insert(0, New ListItem("Select Sub Menu", String.Empty))
            ddlSubMenu.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddladmin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddladmin.SelectedIndexChanged
        Try
            ddlPosition.Enabled = True

            position_info()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPosition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPosition.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

            Dim get_Admin As String = "select staff_Name from staff_Info where stf_ID = '" & ddladmin.SelectedValue & "'"
            Dim data_Admin = oCommon.getFieldValue(get_Admin)

            admin_Name.Text = data_Admin

            menu_info()
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

            gvTable.Columns(1).ControlStyle.Width = 170
            gvTable.Columns(2).ControlStyle.Width = 180
            gvTable.Columns(3).ControlStyle.Width = 180

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
        Dim strOrderby As String = " order by Menu, Sub_Menu ASC"

        tmpSQL = "select Menu_Act_ID, staff_info.staff_Name, Menu, Sub_Menu, Add_Function, Delete_Function, Edit_Function, Print_Function, Save_Function, Back_Function, Import_Export_Function, Generate_Cgpa_Function,
                  Upload_Image_Function from menu_activity_access
                  left join staff_Login on menu_activity_access.Login_ID = staff_Login.login_ID
                  left join staff_info on staff_Login.stf_ID = staff_info.stf_ID
                  left join menu_master on menu_activity_access.Admin_Menu_ID = menu_master.Admin_Menu_ID"

        strWhere = " where staff_Login.stf_ID = '" & ddladmin.SelectedValue & "' and staff_Access = '" & ddlPosition.SelectedValue & "'"


        getSQL = tmpSQL & strWhere
        ''--debug
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
            Dlt_ClassData.SelectCommand.CommandText = "delete menu_activity_access where Menu_Act_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick

        Dim errorCount As Integer = 0
        Dim i As Integer

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString

                Dim Add_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Add_Function"), TextBox)
                Dim Delete_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Delete_Function"), TextBox)
                Dim Edit_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Edit_Function"), TextBox)
                Dim Print_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Print_Function"), TextBox)
                Dim Save_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Save_Function"), TextBox)
                Dim Back_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Back_Function"), TextBox)
                Dim Import_Export_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Import_Export_Function"), TextBox)
                Dim Generate_Cgpa_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Generate_Cgpa_Function"), TextBox)
                Dim Upload_Image_Functiontxt As TextBox = DirectCast(datRespondent.Rows(i).FindControl("Upload_Image_Function"), TextBox)

                If chkUpdate.Checked = True Then

                    strSQL = "UPDATE menu_activity_access SET Add_Function = '" & Add_Functiontxt.Text & "', 
                              Delete_Function = '" & Delete_Functiontxt.Text & "', 
                              Edit_Function='" & Edit_Functiontxt.Text & "', 
                              Print_Function='" & Print_Functiontxt.Text & "', 
                              Save_Function='" & Save_Functiontxt.Text & "', 
                              Back_Function='" & Back_Functiontxt.Text & "', 
                              Import_Export_Function='" & Import_Export_Functiontxt.Text & "', 
                              Generate_Cgpa_Function='" & Generate_Cgpa_Functiontxt.Text & "',
                              Upload_Image_Function='" & Upload_Image_Functiontxt.Text & "' WHERE ID ='" & strKey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            End If
        Next

        strRet = BindData(datRespondent)

    End Sub

    Protected Sub ddlMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMenu.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

            submenu_info()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUpload_ServerClick(sender As Object, e As EventArgs) Handles btnUpload.ServerClick

        strSQL = ""
        strRet = ""

        Dim get_loginID As String = "select login_ID from staff_Login where staff_Login.stf_ID = '" & ddladmin.SelectedValue & "' and staff_Access = '" & ddlPosition.SelectedValue & "' "
        Dim find_loginID As String = oCommon.getFieldValue(get_loginID)

        Dim get_AddFunction As String = "No"
        Dim get_DeleteFunction As String = "No"
        Dim get_EditFunction As String = "No"
        Dim get_PrintFunction As String = "No"
        Dim get_SaveFunction As String = "No"
        Dim get_BackFunction As String = "No"
        Dim get_ImportExportFunction As String = "No"
        Dim get_CgpaFunction As String = "No"
        Dim get_ImageFunction As String = "No"

        If check_Add.Checked = True Then
            get_AddFunction = "Yes"
        End If
        If check_Delete.Checked = True Then
            get_DeleteFunction = "Yes"
        End If
        If check_Edit.Checked = True Then
            get_EditFunction = "Yes"
        End If
        If check_Print.Checked = True Then
            get_PrintFunction = "Yes"
        End If
        If check_Save.Checked = True Then
            get_SaveFunction = "Yes"
        End If
        If check_Back.Checked = True Then
            get_BackFunction = "Yes"
        End If
        If check_ImportExport.Checked = True Then
            get_ImportExportFunction = "Yes"
        End If
        If check_Cgpa.Checked = True Then
            get_CgpaFunction = "Yes"
        End If
        If check_Image.Checked = True Then
            get_ImageFunction = "Yes"
        End If

        strSQL = "INSERT INTO menu_activity_access(login_ID,Admin_Menu_ID,Menu_Accessibility,Add_Function,Delete_Function,Edit_Function,Print_Function,Save_Function,Back_Function,
                  Generate_Cgpa_Function,Import_Export_Function,Upload_Image_Function) values 
                  ('" & find_loginID & "','" & ddlSubMenu.SelectedValue & "','Yes','" & get_AddFunction & "','" & get_DeleteFunction & "','" & get_EditFunction & "',
                  '" & get_PrintFunction & "','" & get_SaveFunction & "','" & get_BackFunction & "','" & get_ImportExportFunction & "','" & get_CgpaFunction & "','" & get_ImageFunction & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        strRet = BindData(datRespondent)

    End Sub
End Class