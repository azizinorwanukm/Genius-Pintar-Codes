Imports System.Data.SqlClient

Public Class grade_List_Table
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

                strRet = BindData(datRespondent)
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


            If find_Data_Menu_Data = "Grade Management" And find_Data_Menu_Data.Length > 0 Then

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_Menu_Data = "All" Then
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY grade_min_range DESC"

        tmpSQL = "Select * From grade_info"
        strWhere += " WHERE grade_ID IS NOT NULL"

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

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
            Dlt_NewCourse.SelectCommand.CommandText = "delete grade_info where grade_ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Course = Dlt_NewCourse.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
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
        Dim grade_Name As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtgrade_Name"), TextBox)
        Dim grade_min_range As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtgrade_min_range"), TextBox)
        Dim grade_max_range As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtgrade_max_range"), TextBox)
        Dim gpa As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtgpa"), TextBox)
        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''update grades
        strSQL = "UPDATE grade_info SET grade_Name='" & grade_Name.Text & "',grade_min_range='" & grade_min_range.Text & "',grade_max_range='" & grade_max_range.Text & "',gpa='" & gpa.Text & "' WHERE grade_ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

End Class