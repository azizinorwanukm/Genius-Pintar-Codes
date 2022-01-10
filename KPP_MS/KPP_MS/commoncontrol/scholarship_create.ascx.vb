Imports System.Data.SqlClient
Imports System.Globalization

Public Class scholarship_create
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

                If Session("getStatus") = "LS" Then ''List Scholarship
                    txtbreadcrum1.Text = "List Scholarship"

                    ListScholarship.Visible = True
                    StudentScholarship.Visible = False

                    btnListScholarship.Attributes("class") = "btn btn-info"
                    btnStudentScholarship.Attributes("class") = "btn btn-default font"

                    LS_Type()

                    strRet = BindData(ScholarshipListRespondent)

                ElseIf Session("getStatus") = "SS" Then ''Student Scholarship
                    txtbreadcrum1.Text = "Student Scholarship"

                    ListScholarship.Visible = False
                    StudentScholarship.Visible = True

                    btnListScholarship.Attributes("class") = "btn btn-default font"
                    btnStudentScholarship.Attributes("class") = "btn btn-info"

                    SS_Year()
                    SS_Level()
                    SS_Type()
                    SS_Scholarship()

                    strRet = BindDataSS(SSRespondent)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnListScholarship.Visible = False
        btnStudentScholarship.Visible = False
        btnStudentScholarship.Visible = False
        StudentScholarship.Visible = False

        btnUpdate.Visible = False
        btnRegister.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ListScholarship As String = ""
        Dim Get_StudentScholarship As String = ""

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

            If find_Data_SubMenu2 = "List Scholarship" And find_Data_SubMenu2.Length > 0 Then
                btnListScholarship.Visible = True
                ListScholarship.Visible = True

                Get_ListScholarship = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton_LS") = "TRUE"
                End If

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnUpdate.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Student Scholarship" And find_Data_SubMenu2.Length > 0 Then
                btnStudentScholarship.Visible = True
                StudentScholarship.Visible = True

                Get_StudentScholarship = "TRUE"

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton_SS") = "TRUE"
                End If

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnRegister.Visible = True
                End If

            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnListScholarship.Visible = True
                btnStudentScholarship.Visible = True
                ListScholarship.Visible = True

                btnUpdate.Visible = True
                btnRegister.Visible = True

                Get_ListScholarship = "TRUE"
                Session("getDeleteButton_LS") = "TRUE"
                Session("getDeleteButton_SS") = "TRUE"
                Session("getEditButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "LS" Or Session("getStatus") = "SS" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "LS" And Session("getStatus") <> "SS" Then
            If Get_ListScholarship = "TRUE" Then
                Data_If_Not_Group_Status = "LS"
            ElseIf Get_StudentScholarship = "TRUE" Then
                Data_If_Not_Group_Status = "SS"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ListScholarship = "TRUE" And Data_If_Not_Group_Status = "LS" Then
                Session("getStatus") = "LS"
            ElseIf Get_StudentScholarship = "TRUE" And Data_If_Not_Group_Status = "SS" Then
                Session("getStatus") = "SS"
            End If
        End If
    End Sub

    Private Sub btnListScholarship_ServerClick(sender As Object, e As EventArgs) Handles btnListScholarship.ServerClick
        Session("getStatus") = "LS"
        Response.Redirect("admin_kaunselor_pengurusanbiasiswa.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnStudentScholarship_ServerClick(sender As Object, e As EventArgs) Handles btnStudentScholarship.ServerClick
        Session("getStatus") = "SS"
        Response.Redirect("admin_kaunselor_pengurusanbiasiswa.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub LS_Type()
        strSQL = "select * from setting where Type = 'Scholarship'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_scholarshipType.DataSource = ds
            ddl_scholarshipType.DataTextField = "Parameter"
            ddl_scholarshipType.DataValueField = "Value"
            ddl_scholarshipType.DataBind()
            ddl_scholarshipType.Items.Insert(0, New ListItem("Select Type Of Scholarship", String.Empty))
            ddl_scholarshipType.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Try
            If txt_scholarshipName.Text.Length > 0 Then

                ''Check if the data is existed
                strSQL = "Select scholarship_id from scholarship where scholarship_name like '%" & txt_scholarshipName.Text & "%'"
                strRet = oCommon.getFieldValue(strSQL)

                If ddl_scholarshipType.SelectedIndex > 0 Then

                    If strRet.Length = 0 Then

                        ''Register New Scholarship
                        strSQL = "insert into scholarship(scholarship_name,scholarship_type,scholarship_status,scholarship_view) values('" & txt_scholarshipName.Text & "','" & ddl_scholarshipType.SelectedValue & "','Active','Active')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet = "0" Then
                            ShowMessage(" Register scholarship", MessageType.Success)
                        Else
                            ShowMessage(" Unsuccessful Register scholarship", MessageType.Error)
                        End If

                    Else
                        ShowMessage(" The data already exists in database", MessageType.Error)
                    End If

                Else
                    ShowMessage("Please select type of scholarship", MessageType.Error)
                End If
            Else
                ShowMessage("Please fill in scholarship name", MessageType.Error)
            End If

            strRet = BindData(ScholarshipListRespondent)

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
                gvTable.Columns(5).Visible = True
            Else
                gvTable.Columns(5).Visible = False
            End If

            If Session("getDeleteButton_LS") = "TRUE" Then
                gvTable.Columns(6).Visible = True
            Else
                gvTable.Columns(6).Visible = False
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

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY scholarship_status, scholarship_name ASC"

        tmpSQL = "  Select scholarship_ID, UPPER(scholarship_name) scholarship_name, Parameter, scholarship_status From scholarship
                    Left join setting on scholarship.scholarship_type = setting.Value"
        strWhere += " WHERE scholarship_view = 'Active' and setting.idx = 'Scholarship'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub ScholarshipListRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles ScholarshipListRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = ScholarshipListRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            strSQL = "Update scholarship set scholarship_view = 'Non Active', scholarship_status = 'Non Active' wherer scholarship_id = '" & strKeyName & "'"
            strRet = oCommon.getFieldValue(strSQL)

            strRet = BindData(ScholarshipListRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub ScholarshipListRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles ScholarshipListRespondent.RowEditing

        ScholarshipListRespondent.EditIndex = e.NewEditIndex
        Me.BindData(ScholarshipListRespondent)

    End Sub

    Protected Sub OnRowCancelingEdit(sender As Object, e As EventArgs)
        ScholarshipListRespondent.EditIndex = -1
        Me.BindData(ScholarshipListRespondent)
    End Sub

    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim txtupdate_SName As TextBox = DirectCast(ScholarshipListRespondent.Rows(e.RowIndex).FindControl("txtupdate_scholarshipName"), TextBox)
        Dim txtupdate_SType As TextBox = DirectCast(ScholarshipListRespondent.Rows(e.RowIndex).FindControl("txtupdate_scholarshipType"), TextBox)
        Dim txtupdate_SStatus As TextBox = DirectCast(ScholarshipListRespondent.Rows(e.RowIndex).FindControl("txtupdate_scholarshipStatus"), TextBox)

        Dim strKeyID As String = ScholarshipListRespondent.DataKeys(e.RowIndex).Value.ToString

        If txtupdate_SType.Text = "Financial Aids" Then
            txtupdate_SType.Text = "01"

            ''update marks
            strSQL = "UPDATE scholarship SET scholarship_name='" & txtupdate_SName.Text & "',scholarship_type ='" & txtupdate_SType.Text & "',scholarship_status ='" & txtupdate_SStatus.Text & "' WHERE scholarship_ID ='" & strKeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Update scholarship information", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful update scholarship information", MessageType.Error)
            End If

            ScholarshipListRespondent.EditIndex = -1
            Me.BindData(ScholarshipListRespondent)

        ElseIf txtupdate_SType.Text = "Sponsorship" Then
            txtupdate_SType.Text = "02"

            ''update marks
            strSQL = "UPDATE scholarship SET scholarship_name='" & txtupdate_SName.Text & "',scholarship_type ='" & txtupdate_SType.Text & "',scholarship_status ='" & txtupdate_SStatus.Text & "' WHERE scholarship_ID ='" & strKeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Update scholarship information", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful update scholarship information", MessageType.Error)
            End If

            ScholarshipListRespondent.EditIndex = -1
            Me.BindData(ScholarshipListRespondent)
        Else
            ShowMessage("Please fill in scholarship type correctly", MessageType.Error)
        End If


    End Sub

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        For row = 0 To ScholarshipListRespondent.Rows.Count - 1 Step row + 1
            lblDay = ScholarshipListRespondent.Rows(row).Cells(4).FindControl("scholarship_Color")
            If lblDay.Text = "Non Active" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Red
                lblDay.ForeColor = Drawing.Color.Red
                lblDay.CssClass = "lblAbsent"

            End If

            If lblDay.Text = "Active" Then

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

    Private Sub SS_Year()
        strSQL = "select Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Year.DataSource = ds
            ddl_Year.DataTextField = "Parameter"
            ddl_Year.DataValueField = "Parameter"
            ddl_Year.DataBind()
            ddl_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_Year.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SS_Level()
        strSQL = "Select Parameter from setting where Type = 'Level' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Level.DataSource = ds
            ddl_Level.DataTextField = "Parameter"
            ddl_Level.DataValueField = "Parameter"
            ddl_Level.DataBind()
            ddl_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_Level.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SS_Type()
        strSQL = "select * from setting where Type = 'Scholarship'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_typeScholarship.DataSource = ds
            ddl_typeScholarship.DataTextField = "Parameter"
            ddl_typeScholarship.DataValueField = "Value"
            ddl_typeScholarship.DataBind()
            ddl_typeScholarship.Items.Insert(0, New ListItem("Select Type Of Scholarship", String.Empty))
            ddl_typeScholarship.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub
    Private Sub SS_Scholarship()
        strSQL = "Select UPPER(scholarship_name) scholarship_name, scholarship_id from scholarship where scholarship_view = 'Active' and scholarship_status = 'Active' and scholarship_type = '" & ddl_typeScholarship.SelectedValue & "' order by scholarship_name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_scholarship.DataSource = ds
            ddl_scholarship.DataTextField = "scholarship_name"
            ddl_scholarship.DataValueField = "scholarship_id"
            ddl_scholarship.DataBind()
            ddl_scholarship.Items.Insert(0, New ListItem("Select Scholarship", String.Empty))
            ddl_scholarship.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindDataSS(SSRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            strRet = BindDataSS(SSRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_typeScholarship_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_typeScholarship.SelectedIndexChanged
        Try
            SS_Scholarship()
            strRet = BindDataSS(SSRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_scholarship_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_scholarship.SelectedIndexChanged
        Try
            strRet = BindDataSS(SSRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataSS(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLSS, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            If Session("getDeleteButton_SS") = "TRUE" Then
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

    Private Function getSQLSS() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY D.student_Level, student_Name, scholarship_name ASC"

        tmpSQL = "  select distinct SS_id, UPPER(A.student_Name) student_Name, UPPER(A.student_ID) student_ID, D.student_Level, UPPER(C.scholarship_name) scholarship_name, B.year from student_info A
                    left join scholarship_student B on A.std_ID = B.std_ID
                    left join scholarship C on B.scholarship_id = C.scholarship_id
                    left join student_Level D on A.std_ID = D.std_ID"

        strWhere += " where A.student_Campus = 'PGPN' and D.Registered = 'Yes' and A.student_ID like '%M%' and (A.student_status = 'Access' or A.student_status = 'Graduate')"

        strWhere += " and D.year = '" & ddl_Year.SelectedValue & "' and B.year = '" & ddl_Year.SelectedValue & "'"

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " and D.student_Level = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_scholarship.SelectedIndex > 0 Then
            strWhere += " and C.scholarship_id = '" & ddl_scholarship.SelectedValue & "'"
        End If

        If ddl_typeScholarship.SelectedIndex > 0 Then
            strWhere += " and C.scholarship_type = '" & ddl_typeScholarship.SelectedValue & "'"
        End If

        getSQLSS = tmpSQL & strWhere & strOrderby

        Return getSQLSS
    End Function

    Private Sub btnRegister_ServerClick(sender As Object, e As EventArgs) Handles btnRegister.ServerClick
        Try
            Response.Redirect("admin_edit_biasiswa.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SSRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles SSRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = SSRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete scholarship_student where SS_id ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindDataSS(SSRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

End Class