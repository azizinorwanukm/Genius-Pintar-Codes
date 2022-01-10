Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class student_List_Table
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim strBil As String = ""

    Dim strStudentName As String = ""
    Dim strStudentID As String = ""
    Dim strStudentMykad As String = ""
    Dim strStudentGender As String = ""
    Dim strStudentRace As String = ""
    Dim strStudentReligion As String = ""
    Dim strStudentEmail As String = ""
    Dim strStudentPhone As String = ""
    Dim strStudentAddress As String = ""
    Dim strStudentPostcode As String = ""
    Dim strStudentCity As String = ""
    Dim strStudentState As String = ""
    Dim strStudentStateOfBirth As String = ""
    Dim strStudentStream As String = ""
    Dim strStudentCampus As String = ""
    Dim strStudentYear As String = ""

    Dim strStudentLevel As String = ""
    Dim strStudentSem As String = ""

    Dim strFatherMykad As String = ""
    Dim strFatherName As String = ""
    Dim strFatherEmail As String = ""
    Dim strFatherPhone As String = ""
    Dim strFatherJob As String = ""
    Dim strFatherSalary As String = ""

    Dim strMotherMykad As String = ""
    Dim strMotherName As String = ""
    Dim strMotherEmail As String = ""
    Dim strMotherPhone As String = ""
    Dim strMotherJob As String = ""
    Dim strMotherSalary As String = ""

    Dim newStudent_name As String = ""
    Dim newStudent_ic As String = ""
    Dim newStudent_classname As String = ""
    Dim newStudent_hostelroom As String = ""
    Dim newStudent_program As String = ""


    Dim strBil_Alumni As String = ""

    Dim strStudentName_Alumni As String = ""
    Dim strStudentID_Alumni As String = ""
    Dim strStudentMykad_Alumni As String = ""
    Dim strStudentMasok_Alumni As String = ""
    Dim strStudentKeluar_Alumni As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "RS" Then ''Register Student
                    txtbreadcrum1.Text = "Register Student"

                    RegisterStudent.Visible = True
                    ViewStudent.Visible = False
                    ImportStudent.Visible = False

                    btnRegisterStudent.Attributes("class") = "btn btn-info"
                    btnViewStudent.Attributes("class") = "btn btn-default font"
                    btnImportStudent.Attributes("class") = "btn btn-default font"

                    student_year()
                    State()
                    Race_List()
                    Religion_List()
                    Level()
                    Salary()
                    Semester()
                    Stream_List()
                    campus_List()

                ElseIf Session("getStatus") = "VS" Then ''View Student
                    txtbreadcrum1.Text = "View Student"

                    RegisterStudent.Visible = False
                    ViewStudent.Visible = True
                    ImportStudent.Visible = False

                    btnRegisterStudent.Attributes("class") = "btn btn-default font"
                    btnViewStudent.Attributes("class") = "btn btn-info"
                    btnImportStudent.Attributes("class") = "btn btn-default font"

                    campus_List()
                    year_list()
                    load_page()
                    student_Level()
                    Stream_List()
                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "IS" Then ''Import Student
                    txtbreadcrum1.Text = "Import Student"

                    RegisterStudent.Visible = False
                    ViewStudent.Visible = False
                    ImportStudent.Visible = True

                    btnRegisterStudent.Attributes("class") = "btn btn-default font"
                    btnViewStudent.Attributes("class") = "btn btn-default font"
                    btnImportStudent.Attributes("class") = "btn btn-info"

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewStudent.Visible = False
        btnRegisterStudent.Visible = False
        btnImportStudent.Visible = False
        ViewStudent.Visible = False
        RegisterStudent.Visible = False
        ImportStudent.Visible = False

        Btnsimpan.Visible = False
        BtnImport.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewStudent As String = ""
        Dim Get_RegisterStudent As String = ""
        Dim Get_ImportStudent As String = ""

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


            ''Get Function Button 1 Import Data 
            strSQL = "  Select B.F1_Import From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Import As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "View Student" And find_Data_SubMenu2.Length > 0 Then
                btnViewStudent.Visible = True
                ViewStudent.Visible = True

                Get_ViewStudent = "TRUE"

                If find_Data_F1View.Length > 0 And find_Data_F1View = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Student" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterStudent.Visible = True
                RegisterStudent.Visible = True

                Get_RegisterStudent = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Import Student" And find_Data_SubMenu2.Length > 0 Then
                btnImportStudent.Visible = True
                ImportStudent.Visible = True

                Get_ImportStudent = "TRUE"

                If find_Data_F1Import.Length > 0 And find_Data_F1Import = "TRUE" Then
                    BtnImport.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewStudent.Visible = True
                btnRegisterStudent.Visible = True
                btnImportStudent.Visible = True
                ViewStudent.Visible = True
                RegisterStudent.Visible = True
                ImportStudent.Visible = True

                Btnsimpan.Visible = True
                BtnImport.Visible = True

                Get_ViewStudent = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RS" Or Session("getStatus") = "VS" Or Session("getStatus") = "IS" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RS" And Session("getStatus") <> "VS" And Session("getStatus") <> "IS" Then
            If Get_ViewStudent = "TRUE" Then
                Data_If_Not_Group_Status = "VS"
            ElseIf Get_RegisterStudent = "TRUE" Then
                Data_If_Not_Group_Status = "RS"
            ElseIf Get_ImportStudent = "TRUE" Then
                Data_If_Not_Group_Status = "IS"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewStudent = "TRUE" And Data_If_Not_Group_Status = "VS" Then
                Session("getStatus") = "VS"
            ElseIf Get_RegisterStudent = "TRUE" And Data_If_Not_Group_Status = "RS" Then
                Session("getStatus") = "RS"
            ElseIf Get_ImportStudent = "TRUE" And Data_If_Not_Group_Status = "IS" Then
                Session("getStatus") = "IS"
            End If
        End If

    End Sub

    Private Sub btnRegisterStudent_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterStudent.ServerClick
        Session("getStatus") = "RS"
        Response.Redirect("admin_carian_pelajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewStudent_ServerClick(sender As Object, e As EventArgs) Handles btnViewStudent.ServerClick
        Session("getStatus") = "VS"
        Response.Redirect("admin_carian_pelajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnImportStudent_ServerClick(sender As Object, e As EventArgs) Handles btnImportStudent.ServerClick
        Session("getStatus") = "IS"
        Response.Redirect("admin_carian_pelajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub load_page()
        strSQL = "SELECT year from student_Level where year ='" & Now.Year & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
    End Sub

    Protected Sub ddlClassnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Campus.SelectedIndexChanged
        Try
            Stream_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            ddlLevelnaming.Enabled = True
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevelnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelnaming.SelectedIndexChanged
        Try
            ddlClassnaming.Enabled = True
            class_info_list()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlStreaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStreaming.SelectedIndexChanged
        Try
            class_info_list()
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
                gvTable.Columns(7).Visible = True
            Else
                gvTable.Columns(7).Visible = False
            End If

            If Session("getDeleteButton") = "TRUE" Then
                gvTable.Columns(8).Visible = True
            Else
                gvTable.Columns(8).Visible = False
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
        Dim strOrderby As String = " ORDER BY student_Name ASC"

        tmpSQL = "Select distinct UPPER(student_info.student_ID) student_ID, student_info.std_ID,
                  student_info.student_Mykad,
                  UPPER(student_info.student_Name) student_Name,
                  UPPER(student_Level.student_Level) student_Level,
                  UPPER(student_Level.student_Sem) student_Sem,
                  student_info.student_Photo,
                  UPPER(class_info.class_Name) class_Name 
                  From student_info 
                  left join student_Level on student_info.std_ID=student_level.std_ID
                  left join course on student_info.std_ID= course.std_ID 
                  left join class_info on course.class_ID= class_info.class_ID"
        strWhere = " WHERE student_info.std_ID IS NOT NULL and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and student_level.Registered = 'Yes' "

        strWhere += " and student_info.student_Campus = '" & ddl_Campus.SelectedValue & "' and class_info.class_Campus = '" & ddl_Campus.SelectedValue & "'"

        strWhere += " and course.year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and student_info.student_Stream = '" & ddlStreaming.SelectedValue & "'"
        strWhere += " and student_level.year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and (class_info.class_type = 'Compulsory' or course.class_ID is null)"
        strWhere += " and student_level.student_Level = '" & ddlLevelnaming.SelectedValue & "'"

        If ddlClassnaming.SelectedIndex > 0 Then
            strWhere += " and class_info.class_ID = '" & ddlClassnaming.SelectedValue & "'"
        End If

        If ddlStreaming.SelectedValue <> "Temp" Then
            strWhere += " and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Try
            Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

            strSQL = "Update student_info set student_Status = 'Block' where std_ID = '" & strKeyName & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            strRet = BindData(datRespondent)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Session("getStatus") = "SI"
            Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub class_info_list()

        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & ddlYear.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlLevelnaming.SelectedValue & "' and course_PRogram = '" & ddlStreaming.SelectedValue & "'  and class_Campus = '" & ddl_Campus.SelectedValue & "' order by class_Name ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassnaming.DataSource = ds
            ddlClassnaming.DataTextField = "class_Name"
            ddlClassnaming.DataValueField = "class_ID"
            ddlClassnaming.DataBind()
            ddlClassnaming.Items.Insert(0, New ListItem("Select Class", String.Empty))

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

            ddlLevelnaming.DataSource = ds
            ddlLevelnaming.DataTextField = "Parameter"
            ddlLevelnaming.DataValueField = "Parameter"
            ddlLevelnaming.DataBind()
            ddlLevelnaming.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
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
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Salary()
        strSQL = "SELECT Parameter from setting where Type = 'Race'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRace.DataSource = ds
            ddlRace.DataTextField = "Parameter"
            ddlRace.DataValueField = "Parameter"
            ddlRace.DataBind()
            ddlRace.Items.Insert(0, New ListItem("Select Race", ""))
            ddlRace.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Race_List()
        strSQL = "SELECT Parameter from setting where Type = 'Salary'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsalaryP1.DataSource = ds
            ddlsalaryP1.DataTextField = "Parameter"
            ddlsalaryP1.DataValueField = "Parameter"
            ddlsalaryP1.DataBind()
            ddlsalaryP1.Items.Insert(0, New ListItem("Select Salary", ""))
            ddlsalaryP1.SelectedIndex = 0

            ddlsalaryP2.DataSource = ds
            ddlsalaryP2.DataTextField = "Parameter"
            ddlsalaryP2.DataValueField = "Parameter"
            ddlsalaryP2.DataBind()
            ddlsalaryP2.Items.Insert(0, New ListItem("Select Salary", ""))
            ddlsalaryP2.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Religion_List()
        strSQL = "SELECT Parameter from setting where Type = 'Religion'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligion.DataSource = ds
            ddlReligion.DataTextField = "Parameter"
            ddlReligion.DataValueField = "Parameter"
            ddlReligion.DataBind()
            ddlReligion.Items.Insert(0, New ListItem("Select Religion", ""))
            ddlReligion.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_year()
        strSQL = "SELECT Parameter from setting where Type = 'Year' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYearNaming.DataSource = ds
            ddlYearNaming.DataTextField = "Parameter"
            ddlYearNaming.DataValueField = "Parameter"
            ddlYearNaming.DataBind()
            ddlYearNaming.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub State()
        strSQL = "SELECT Parameter FROM setting WHERE Type='State' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "Parameter"
            ddlState.DataValueField = "Parameter"
            ddlState.DataBind()
            ddlState.Items.Insert(0, New ListItem("Select State", String.Empty))
            ddlState.SelectedIndex = 0

            ddlStateOfBirth.DataSource = ds
            ddlStateOfBirth.DataTextField = "Parameter"
            ddlStateOfBirth.DataValueField = "Parameter"
            ddlStateOfBirth.DataBind()
            ddlStateOfBirth.Items.Insert(0, New ListItem("Select State", String.Empty))
            ddlStateOfBirth.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlLevel.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Semester()
        strSQL = "SELECT * FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSem.DataSource = ds
            ddlSem.DataTextField = "Parameter"
            ddlSem.DataValueField = "Value"
            ddlSem.DataBind()
            ddlSem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlSem.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Stream_List()

        If ddl_Campus.SelectedValue = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStream.DataSource = ds
            ddlStream.DataTextField = "Parameter"
            ddlStream.DataValueField = "Value"
            ddlStream.DataBind()
            ddlStream.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddlStream.SelectedIndex = 0

            ddlStreaming.DataSource = ds
            ddlStreaming.DataTextField = "Parameter"
            ddlStreaming.DataValueField = "Value"
            ddlStreaming.DataBind()
            ddlStreaming.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddlStreaming.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub campus_List()
        Try
            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCampus.DataSource = ds
            ddlCampus.DataTextField = "Parameter"
            ddlCampus.DataValueField = "Value"
            ddlCampus.DataBind()
            ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

            ddl_Campus.DataSource = ds
            ddl_Campus.DataTextField = "Parameter"
            ddl_Campus.DataValueField = "Value"
            ddl_Campus.DataBind()
            ddl_Campus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        If ddlLevel.SelectedIndex > 0 Then

            If student_Mykad.Text <> "" And student_Mykad.Text.Length < 13 Then

                If student_PostalCode.Text <> "" And IsNumeric(student_PostalCode.Text) Then

                    If student_Name.Text.Length > 0 And Not IsNothing(student_Name.Text) Then

                        If IsNumeric(student_FonNo.Text) Or student_FonNo.Text = "" Then

                            If std_txtCty.Text <> "" And std_txtCty.Text.Length > 0 Then

                                If ddlState.SelectedIndex > 0 Then

                                    If strgender.Length > 0 Then

                                        If student_Email.Text.Length > 0 Then

                                            If student_ID.Text.Length > 0 And Regex.IsMatch(student_ID.Text, "^[A-Za-z0-9]+$") Then

                                                If ddlYearNaming.SelectedIndex > 0 Then

                                                    Dim imgPath As String = "~/student_Image/user.png"

                                                    If ddlRace.SelectedIndex > 0 Then

                                                        If ddlReligion.SelectedIndex > 0 Then

                                                            If ddlStream.SelectedIndex > 0 Then

                                                                If ddlStateOfBirth.SelectedIndex > 0 Then

                                                                    If ddlCampus.SelectedIndex > 0 Then

                                                                        ''parent 1 validation
                                                                        If validateParent1() = 0 Then

                                                                            ''parent 2 validation
                                                                            If validateParent2() = 0 Then

                                                                                ''checking student data existed or not
                                                                                Dim find_checkData As String = "Select std_ID from student_info where student_Mykad = '" & student_Mykad.Text & "'"
                                                                                Dim get_checkData As String = oCommon.getFieldValue(find_checkData)

                                                                                If get_checkData.Length = 0 Then

                                                                                    Using STDDATA As New SqlCommand("INSERT student_info(student_ID,student_Mykad,student_Name,student_Email,student_FonNo,student_Address,student_StateOfBirth,student_Campus,
                                                                                                                 student_Password,student_PostalCode,student_State,student_City,student_Photo,student_Sex,student_Race,student_Religion, student_Status,student_Stream) values 
                                                                                                                 ('" & student_ID.Text & "','" & student_Mykad.Text & "','" & oCommon.FixSingleQuotes(student_Name.Text.ToUpper) & "',
                                                                                                                 '" & student_Email.Text & "','" & student_FonNo.Text & "','" & student_Address.Text.ToUpper & "','" & ddlStateOfBirth.SelectedValue & "','" & ddlCampus.SelectedValue & "',
                                                                                                                 '" & student_Mykad.Text & "','" & student_PostalCode.Text & "','" & ddlState.SelectedValue & "','" & std_txtCty.Text.ToUpper & "',
                                                                                                                 '" & imgPath & "','" & strgender.ToUpper & "','" & ddlRace.SelectedValue & "','" & ddlReligion.SelectedValue & "', 'Access', '" & ddlStream.SelectedValue & "')", objConn)
                                                                                        objConn.Open()
                                                                                        Dim i = STDDATA.ExecuteNonQuery()
                                                                                        objConn.Close()
                                                                                        If i <> 0 Then
                                                                                            ShowMessage(" Add Student Information ", MessageType.Success)

                                                                                            Dim stdID As String = "select std_ID from student_info where student_Mykad = '" & student_Mykad.Text & "'"
                                                                                            Dim dataStdID As String = oCommon.getFieldValue(stdID)

                                                                                            Using STDLEVEL As New SqlCommand("INSERT INTO student_level(std_ID,student_Sem,student_Level,year,month,day,Registered) values 
                                                                                                    ('" & dataStdID & "','Sem 1','" & ddlLevel.SelectedValue & "','" & ddlYearNaming.SelectedValue & "','" & Now.Month & "','" & Now.Day & "','No')", objConn)
                                                                                                objConn.Open()
                                                                                                Dim j = STDLEVEL.ExecuteNonQuery()
                                                                                                objConn.Close()
                                                                                                If j <> 0 Then
                                                                                                    errorCount = 0
                                                                                                Else
                                                                                                    errorCount = 1
                                                                                                End If
                                                                                            End Using

                                                                                            strSQL = "INSERT INTO course(std_ID,Year) values('" & dataStdID & "','" & Now.Year & "')"
                                                                                            strRet = oCommon.ExecuteSQL(strSQL)

                                                                                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                                                                                            Dim level As String = ""

                                                                                            If ddlLevel.SelectedValue = "Foundation 1" Or ddlLevel.SelectedValue = "Foundation 2" Or ddlLevel.SelectedValue = "Foundation 3" Then
                                                                                                level = "ASAS 1"
                                                                                            ElseIf ddlLevel.SelectedValue = "Level 1" Or ddlLevel.SelectedValue = "Level 2" Then
                                                                                                level = "TAHAP 1"
                                                                                            End If

                                                                                            Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & student_Mykad.Text & "'"
                                                                                            Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                                                                                            Dim find_PPCSDate As String = "select MAX(PPCSDate) from PPCS where StudentID = '" & get_StudentID & "'"
                                                                                            Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                                                                                            strSQL = "  INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan) 
                                                                                            values('" & get_StudentID & "','" & get_PPCSDate & "','" & ddlYearNaming.SelectedValue & "','" & level & "','N')"
                                                                                            strRet = oCommon.ExecuteSQLPermata(strSQL)

                                                                                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                                                                            Dim exist_fatherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent1_IC.Text & "'"
                                                                                            Dim data_fatherID As String = oCommon.getFieldValue(exist_fatherID)

                                                                                            If data_fatherID.Length = 0 Then

                                                                                                'Validate parent1 input
                                                                                                Using PJGDATA As New SqlCommand("   INSERT INTO parent_Info(parent_No,parent_Name,parent_IC,parent_MobileNo,parent_Email,
                                                                                                                                 parent_Work,parent_Salary,parent_Password) values 
                                                                                                                                ('1' ,'" & oCommon.FixSingleQuotes(Parent1_Name.Text.ToUpper) & "','" & Parent1_IC.Text & "',
                                                                                                                                '" & Parent1_MobileNo.Text & "','" & Parent1_Email.Text & "','" & Parent1_Work.Text.ToUpper & "',
                                                                                                                                '" & ddlsalaryP1.SelectedValue & "','" & Parent1_IC.Text & "')", objConn)
                                                                                                    objConn.Open()
                                                                                                    Dim k = PJGDATA.ExecuteNonQuery()
                                                                                                    objConn.Close()
                                                                                                    If k <> 0 Then
                                                                                                        errorCount = 0
                                                                                                    Else
                                                                                                        errorCount = 1
                                                                                                    End If
                                                                                                End Using

                                                                                            End If


                                                                                            Dim exist_motherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent2_IC.Text & "'"
                                                                                            Dim data_motherID As String = oCommon.getFieldValue(exist_motherID)

                                                                                            If data_motherID.Length = 0 Then

                                                                                                Using PJGDATA As New SqlCommand("   INSERT INTO parent_Info(parent_No,parent_Name,parent_IC,parent_MobileNo,parent_Email,
                                                                                                                                 parent_Work,parent_Salary,parent_Password) values 
                                                                                                                                ('2','" & oCommon.FixSingleQuotes(Parent2_Name.Text.ToUpper) & "','" & Parent2_IC.Text & "',
                                                                                                                                '" & Parent2_MobileNo.Text & "','" & Parent2_Email.Text & "','" & Parent2_Work.Text.ToUpper & "',
                                                                                                                                '" & ddlsalaryP2.SelectedValue & "','" & Parent2_IC.Text & "')", objConn)
                                                                                                    objConn.Open()
                                                                                                    Dim l = PJGDATA.ExecuteNonQuery()
                                                                                                    objConn.Close()
                                                                                                    If l <> 0 Then
                                                                                                        errorCount = 0
                                                                                                    Else
                                                                                                        errorCount = 1
                                                                                                    End If
                                                                                                End Using

                                                                                            End If

                                                                                            Dim fatherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent1_IC.Text & "'"
                                                                                            Dim ExistFatherID As String = oCommon.getFieldValue(fatherID)

                                                                                            Dim motherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent2_IC.Text & "'"
                                                                                            Dim ExistMotherID As String = oCommon.getFieldValue(motherID)

                                                                                            strSQL = "UPDATE student_info set parent_fatherID ='" & ExistFatherID & "',parent_motherID='" & ExistMotherID & "' WHERE std_ID ='" & dataStdID & "'"
                                                                                            strRet = oCommon.ExecuteSQL(strSQL)

                                                                                        Else
                                                                                            ShowMessage(" Unsuccessful Add Student Information ", MessageType.Error)
                                                                                        End If
                                                                                    End Using

                                                                                Else
                                                                                    ShowMessage(" Student Information Had Existed In Pusat GENIUS@Pintar System ", MessageType.Error)
                                                                                End If
                                                                            Else
                                                                                ShowMessage(" Please Fill Guardian 2 Information ", MessageType.Error)
                                                                            End If
                                                                        Else
                                                                            ShowMessage(" Please Fill Guardian 1 Information ", MessageType.Error)
                                                                        End If
                                                                    Else
                                                                        ShowMessage(" Please Select Instututions ", MessageType.Error)
                                                                    End If
                                                                Else
                                                                    ShowMessage(" Please Select State Of Birth ", MessageType.Error)
                                                                End If
                                                            Else
                                                                ShowMessage(" Please Select Stream ", MessageType.Error)
                                                            End If

                                                        Else
                                                            ShowMessage(" Please Select Religion ", MessageType.Error)
                                                        End If

                                                    Else
                                                        ShowMessage(" Please Select Race ", MessageType.Error)
                                                    End If

                                                Else
                                                    ShowMessage(" Please Select Student Year ", MessageType.Error)
                                                End If
                                            Else
                                                ShowMessage(" Please Fill In Student ID ", MessageType.Error)
                                            End If
                                        Else
                                            ShowMessage(" Please Fill In Student Email ", MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage(" Please Select Gender ", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage(" Please Select State ", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Please Fill In City ", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Please Fill In Student Phone No ", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Fill In Student Name ", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Fill In Zip Code ", MessageType.Error)
                End If
            Else
                ShowMessage(" Please Fill In Student NRIC / MYKAD ", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Select Student Level ", MessageType.Error)
        End If

    End Sub

    Private Function validateParent1() As Integer

        If Parent1_Name.Text.Length > 0 Then

            If Parent1_IC.Text.Length > 0 And Parent1_IC.Text.Length < 14 Then
                Return 0
            Else
                ShowMessage(" Please Fill Guardian 1 NRIC ", MessageType.Error)
                Return 21
            End If

        Else
            ShowMessage(" Please Fill Guardian 1 Name ", MessageType.Error)
            Return 20
        End If

    End Function

    Private Function validateParent2() As Integer

        If Parent2_Name.Text.Length > 0 Then

            If Parent2_IC.Text.Length > 0 And Parent2_IC.Text.Length < 14 Then
                Return 0
            Else
                ShowMessage(" Please Fill Guardian 2 NRIC ", MessageType.Error)
                Return 41
            End If
        Else
            ShowMessage(" Please Fill Guardian 2 Name ", MessageType.Error)
            Return 40
        End If

    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/student_info.xlsx")
    End Sub

    Private Sub BtnUploadedStudentOnly_ServerClick(sender As Object, e As EventArgs) Handles BtnUploadedStudentOnly.ServerClick

        Try
            '--upload excel
            If ImportExcel() = True Then

            Else
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/student_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [studentinfo$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then

                    SaveSiteData(ds)

                Else
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVar()
                strMsg = ""

                'BIL
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Bil")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("Bil")
                End If

                'STUDENT MYKAD
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Mykad")) Then
                    strStudentMykad = SiteData.Tables(0).Rows(i).Item("Student_Mykad")
                Else
                    strMsg += " Please Enter Student Mykad |"
                End If

                'STUDENT NAME
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Name")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Student_Name")
                Else
                    strMsg += " Please Enter Student Name |"
                End If

                'STUDENT NAME
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_ID")) Then
                    strStudentID = SiteData.Tables(0).Rows(i).Item("Student_ID")
                Else
                    strMsg += " Please Enter Student ID |"
                End If

                'STUDENT GENDER
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Gender")) Then
                    strStudentGender = SiteData.Tables(0).Rows(i).Item("Student_Gender")
                Else
                    strMsg += " Please Enter Student Gender |"
                End If

                'STUDENT RACE
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Race")) Then
                    strStudentRace = SiteData.Tables(0).Rows(i).Item("Student_Race")
                Else
                    strMsg += " Please Enter Student Race |"
                End If

                'STUDENT RELIGION
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Religion")) Then
                    strStudentReligion = SiteData.Tables(0).Rows(i).Item("Student_Religion")
                Else
                    strMsg += " Please Enter Student Religion |"
                End If

                'FATHER MYKAD
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Mykad")) Then
                    strFatherMykad = SiteData.Tables(0).Rows(i).Item("Father_Mykad")
                Else
                    strMsg += " Please Enter Father Mykad |"
                End If

                'FATHER NAME
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Name")) Then
                    strFatherName = SiteData.Tables(0).Rows(i).Item("Father_Name")
                Else
                    strMsg += " Please Enter Father Name |"
                End If

                'MOTHER MYKAD
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Mykad")) Then
                    strMotherMykad = SiteData.Tables(0).Rows(i).Item("Mother_Mykad")
                Else
                    strMsg += " Please Enter Mother Mykad |"
                End If

                'MOTHER NAME
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Name")) Then
                    strMotherName = SiteData.Tables(0).Rows(i).Item("Mother_Name")
                Else
                    strMsg += " Please Enter Mother Name |"
                End If

                'Student Level
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Level")) Then
                    strStudentLevel = SiteData.Tables(0).Rows(i).Item("Student_Level")
                Else
                    strMsg += " Please Enter Student Level |"
                End If

                'Student Sem
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Sem")) Then
                    strStudentSem = SiteData.Tables(0).Rows(i).Item("Student_Sem")
                Else
                    strMsg += " Please Enter Student Sem |"
                End If

                'Student Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Year")) Then
                    strStudentYear = SiteData.Tables(0).Rows(i).Item("Year")
                Else
                    strMsg += " Please Enter Year |"
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg = "BIL# :" & strBil & " Mykad " & strStudentMykad & ":" & strStudentName & strMsg
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countUpdateParents As Integer = 0
        Dim countUpdateStudent As Integer = 0
        Dim countInsertParents As Integer = 0
        Dim countInsertStudent As Integer = 0


        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")

            ''STUDENT
            strStudentMykad = SiteData.Tables(0).Rows(i).Item("Student_Mykad")
            strStudentName = SiteData.Tables(0).Rows(i).Item("Student_Name")
            strStudentID = SiteData.Tables(0).Rows(i).Item("Student_ID")
            strStudentGender = SiteData.Tables(0).Rows(i).Item("Student_Gender")
            strStudentRace = SiteData.Tables(0).Rows(i).Item("Student_Race")
            strStudentReligion = SiteData.Tables(0).Rows(i).Item("Student_Religion")
            strStudentAddress = SiteData.Tables(0).Rows(i).Item("Student_Address")
            strStudentPostcode = SiteData.Tables(0).Rows(i).Item("Student_Postal_Code")
            strStudentCity = SiteData.Tables(0).Rows(i).Item("Student_City")
            strStudentState = SiteData.Tables(0).Rows(i).Item("Student_State")
            strStudentState = SiteData.Tables(0).Rows(i).Item("Student_State")
            strStudentStateOfBirth = SiteData.Tables(0).Rows(i).Item("Student_StateOfBirth")
            strStudentStream = SiteData.Tables(0).Rows(i).Item("Student_Stream")
            strStudentCampus = SiteData.Tables(0).Rows(i).Item("student_Campus")

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Email")) Then
                strStudentEmail = SiteData.Tables(0).Rows(i).Item("Student_Email")
            Else
                strStudentEmail = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Phone")) Then
                strStudentPhone = SiteData.Tables(0).Rows(i).Item("Student_Phone")
            Else
                strStudentPhone = ""
            End If

            strStudentLevel = SiteData.Tables(0).Rows(i).Item("Student_Level")
            strStudentSem = SiteData.Tables(0).Rows(i).Item("Student_Sem")
            strStudentYear = SiteData.Tables(0).Rows(i).Item("Year")

            ''FATHER
            strFatherMykad = SiteData.Tables(0).Rows(i).Item("Father_Mykad")
            strFatherName = SiteData.Tables(0).Rows(i).Item("Father_Name")

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Email")) Then
                strFatherEmail = SiteData.Tables(0).Rows(i).Item("Father_Email")
            Else
                strFatherEmail = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Phone")) Then
                strFatherPhone = SiteData.Tables(0).Rows(i).Item("Father_Phone")
            Else
                strFatherPhone = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Job")) Then
                strFatherJob = SiteData.Tables(0).Rows(i).Item("Father_Job")
            Else
                strFatherJob = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Salary")) Then
                strFatherSalary = SiteData.Tables(0).Rows(i).Item("Father_Salary")
            Else
                strFatherSalary = ""
            End If

            ''MOTHER
            strMotherMykad = SiteData.Tables(0).Rows(i).Item("Mother_Mykad")
            strMotherName = SiteData.Tables(0).Rows(i).Item("Mother_Name")

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Email")) Then
                strMotherEmail = SiteData.Tables(0).Rows(i).Item("Mother_Email")
            Else
                strMotherEmail = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Phone")) Then
                strMotherPhone = SiteData.Tables(0).Rows(i).Item("Mother_Phone")
            Else
                strMotherPhone = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Job")) Then
                strMotherJob = SiteData.Tables(0).Rows(i).Item("Mother_Job")
            Else
                strMotherJob = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Salary")) Then
                strMotherSalary = SiteData.Tables(0).Rows(i).Item("Mother_Salary")
            Else
                strMotherSalary = ""
            End If

            ''UPDATE
            ''IF PARENT EXISTS
            strSQL = "SELECT parent_ID FROM parent_Info WHERE parent_IC = '" & strFatherMykad & "' OR parent_IC = '" & strMotherMykad & "'"
            If oCommon.isExist(strSQL) = True Then

                strSQL = "SELECT parent_ID FROM parent_Info WHERE parent_IC = '" & strFatherMykad & "'"
                Dim parentfatherID As String = oCommon.getFieldValue(strSQL)
                strSQL = "SELECT parent_ID FROM parent_Info WHERE parent_IC = '" & strMotherMykad & "'"
                Dim parentmotherID As String = oCommon.getFieldValue(strSQL)

                ''UPDATE FATHER INFO
                strSQL = "  UPDATE parent_Info SET 
                            parent_IC = '" & strFatherMykad & "', 
                            parent_Name = UPPER('" & oCommon.FixSingleQuotes(strFatherName) & "'),
                            parent_Email = '" & oCommon.FixSingleQuotes(strFatherEmail) & "',
                            parent_MobileNo = '" & oCommon.FixSingleQuotes(strFatherPhone) & "',
                            parent_Work = UPPER('" & oCommon.FixSingleQuotes(strFatherJob) & "'),
                            parent_Salary = '" & oCommon.FixSingleQuotes(strFatherSalary) & "'"

                strSQL += " WHERE parent_ID = '" & parentfatherID & "'"

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    countUpdateParents = countUpdateParents + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

                ''UPDATE MOTHER INFO
                strSQL = "  UPDATE parent_Info SET 
                            parent_IC = '" & strMotherMykad & "', 
                            parent_Name = UPPER('" & oCommon.FixSingleQuotes(strMotherName) & "'),
                            parent_Email = '" & oCommon.FixSingleQuotes(strMotherEmail) & "',
                            parent_MobileNo = '" & oCommon.FixSingleQuotes(strMotherPhone) & "',
                            parent_Work = UPPER('" & oCommon.FixSingleQuotes(strMotherJob) & "'),
                            parent_Salary = '" & oCommon.FixSingleQuotes(strMotherSalary) & "'"

                strSQL += " WHERE parent_ID = '" & parentmotherID & "'"

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    countUpdateParents = countUpdateParents + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

                ''IF STUDENT EXIST
                strSQL = "SELECT std_ID FROM student_info WHERE parent_fatherID = '" & parentfatherID & "' OR parent_motherID = '" & parentmotherID & "'"
                If oCommon.isExist(strSQL) = True Then

                    ''UPDATE STUDENT INFO
                    strSQL = "SELECT std_ID FROM student_info WHERE student_Mykad = '" & strStudentMykad & "'"
                    Dim stdID As String = oCommon.getFieldValue(strSQL)

                    strSQL = "  UPDATE student_info SET 
                                student_Mykad = '" & strStudentMykad & "', 
                                student_Name = UPPER('" & oCommon.FixSingleQuotes(strStudentName) & "'), 
                                student_Sex = UPPER('" & strStudentGender & "'), 
                                student_Race = UPPER('" & strStudentRace & "'), 
                                student_Religion = UPPER('" & strStudentReligion & "'),
                                student_Email = '" & oCommon.FixSingleQuotes(strStudentEmail) & "',
                                student_FonNo = '" & oCommon.FixSingleQuotes(strStudentPhone) & "',
                                student_Address = UPPER('" & oCommon.FixSingleQuotes(strStudentAddress) & "'), 
                                student_PostalCode ='" & strStudentPostcode & "', 
                                student_City = UPPER('" & strStudentCity & "'), 
                                student_State = UPPER('" & strStudentState & "'),
                                student_StateOfBirth = UPPER('" & strStudentStateOfBirth & "'),
                                student_Stream = UPPER('" & strStudentStream & "'),
                                student_Campus = UPPER('" & strStudentCampus & "'),
                                student_Status = 'Access',
                                student_Year = '" & oCommon.FixSingleQuotes(strStudentYear) & "',
                                parent_FatherID = '" & parentfatherID & "',
                                parent_MotherID = '" & parentmotherID & "'"

                    strSQL += " WHERE std_ID = '" & stdID & "'"

                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = 0 Then
                        countUpdateStudent = countUpdateStudent + 1
                        errorData = 0
                    Else
                        errorData = 1
                    End If

                Else

                    ''IF STUDENT NOT EXIST
                    ''ADD NEW STUDENT

                    strSQL = "  INSERT INTO student_info 
                                (student_Mykad, 
                                student_Name, 
                                student_Sex, 
                                student_Race, 
                                student_Religion, 
                                student_Email, 
                                student_FonNo, 
                                student_Address, 
                                student_PostalCode, 
                                student_City, 
                                student_State,
                                student_StateOfBirth,
                                student_Stream,
                                student_Campus,
                                student_Year, 
                                parent_fatherID, 
                                parent_motherID, 
                                student_Photo, 
                                student_Status, 
                                student_LoginAttempt)"

                    strSQL += " VALUES 
                                ('" & strStudentMykad & "', 
                                UPPER('" & oCommon.FixSingleQuotes(strStudentName) & "'), 
                                UPPER('" & strStudentGender & "'), 
                                UPPER('" & strStudentRace & "'), 
                                UPPER('" & strStudentReligion & "'), 
                                '" & oCommon.FixSingleQuotes(strStudentEmail) & "',
                                '" & oCommon.FixSingleQuotes(strStudentPhone) & "',
                                UPPER('" & oCommon.FixSingleQuotes(strStudentAddress) & "'), 
                                '" & strStudentPostcode & "', 
                                UPPER('" & strStudentCity & "'), 
                                UPPER('" & strStudentState & "'),
                                UPPER('" & strStudentStateOfBirth & "'),
                                UPPER('" & strStudentStream & "'),
                                UPPER('" & strStudentCampus & "'),
                                '" & strStudentYear & "',
                                '" & parentfatherID & "', 
                                '" & parentmotherID & "', 
                                '~/student_Image/user.png', 
                                'Access', 
                                '0')"

                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = 0 Then

                        Dim getStd_ID As String = "select std_ID from student_info where student_Mykad = '" & strStudentMykad & "' and student_Status = 'Access'"
                        Dim datastd_ID As String = oCommon.getFieldValue(getStd_ID)

                        Dim getStudent_Level_ID As String = "select ID from student_level where std_ID = '" & datastd_ID & "' and student_Level = '" & strStudentLevel & "' and student_Sem = '" & strStudentSem & "' and year = '" & strStudentYear & "'"
                        Dim dataStudent_Level_ID As String = oCommon.getFieldValue(getStudent_Level_ID)

                        If dataStudent_Level_ID = "" Then
                            Using PJGDATA As New SqlCommand("INSERT INTO student_level(std_ID,student_Level,student_Sem,year,day) values 
                                                            ('" & datastd_ID & "' ,'" & strStudentLevel & "','" & strStudentSem & "','" & strStudentYear & "',
                                                            '" & Now.Month & "','" & Now.Day & "')", objConn)
                                objConn.Open()
                                Dim k = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                            End Using
                        End If

                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                        Dim level As String = ""

                        If strStudentLevel = "Foundation 1" Or strStudentLevel = "Foundation 2" Or strStudentLevel = "Foundation 3" Then
                            level = "ASAS 1"
                        ElseIf strStudentLevel = "Level 1" Or strStudentLevel = "Level 2" Then
                            level = "TAHAP 1"
                        End If

                        Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & strStudentMykad & "'"
                        Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                        Dim find_PPCSDate As String = "select PPCSDate from PPCS where StudentID = '" & get_StudentID & "'"
                        Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                        strSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan) 
                                  values('" & get_StudentID & "','" & get_PPCSDate & "','" & strStudentYear & "','" & level & "','N',)"
                        strRet = oCommon.ExecuteSQLPermata(strSQL)

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        Dim getcourse_ID As String = "select course_ID from course where std_ID = '" & datastd_ID & "' "
                        Dim datacourse_ID As String = oCommon.getFieldValue(getcourse_ID)

                        If datacourse_ID = "" Then
                            Using PJGDATA As New SqlCommand("INSERT INTO course(std_ID,year) values 
                                                            ('" & datastd_ID & "' ,'" & strStudentYear & "')", objConn)
                                objConn.Open()
                                Dim k = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                            End Using
                        End If

                        countInsertStudent = countInsertStudent + 1
                        errorData = 0
                    Else
                        errorData = 1
                    End If

                End If

            Else

                ''INSERT NEW PARENT DATA
                strSQL = "  INSERT INTO parent_Info
                            (
                            parent_IC,
                            parent_Name,
                            parent_Email,
                            parent_MobileNo,
                            parent_Work,
                            parent_Salary,
                            parent_Sex,
                            parent_No
                            )"

                strSQL += " VALUES 
                                ('" & strFatherMykad & "',
                                UPPER('" & oCommon.FixSingleQuotes(strFatherName) & "'),
                                '" & oCommon.FixSingleQuotes(strFatherEmail) & "',
                                '" & oCommon.FixSingleQuotes(strFatherPhone) & "',
                                UPPER('" & oCommon.FixSingleQuotes(strFatherJob) & "'),
                                '" & oCommon.FixSingleQuotes(strFatherSalary) & "',
                                'MALE',
                                '1') "

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    countInsertParents = countInsertParents + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

                strSQL = "  INSERT INTO parent_Info
                            (
                            parent_IC,
                            parent_Name,
                            parent_Email,
                            parent_MobileNo,
                            parent_Work,
                            parent_Salary,
                            parent_Sex,
                            parent_No
                            )"

                strSQL += " VALUES 
                            ('" & strMotherMykad & "',
                            UPPER('" & oCommon.FixSingleQuotes(strMotherName) & "'),
                            '" & oCommon.FixSingleQuotes(strMotherEmail) & "',
                            '" & oCommon.FixSingleQuotes(strMotherPhone) & "',
                            UPPER('" & oCommon.FixSingleQuotes(strMotherJob) & "'),
                            '" & oCommon.FixSingleQuotes(strMotherSalary) & "',
                            'FEMALE',
                            '2')"

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then

                    Dim getStd_ID As String = "select std_ID from student_info where student_Mykad = '" & strStudentMykad & "' and student_Status = 'Access'"
                    Dim datastd_ID As String = oCommon.getFieldValue(getStd_ID)

                    Dim getStudent_Level_ID As String = "select ID from student_level where std_ID = '" & datastd_ID & "' and student_Level = '" & strStudentLevel & "' and student_Sem = '" & strStudentSem & "' and year = '" & strStudentYear & "'"
                    Dim dataStudent_Level_ID As String = oCommon.getFieldValue(getStudent_Level_ID)

                    If dataStudent_Level_ID = "" Then
                        Using PJGDATA As New SqlCommand("INSERT INTO student_level(std_ID,student_Level,student_Sem,year,day) values 
                                                            ('" & datastd_ID & "' ,'" & strStudentLevel & "','" & strStudentSem & "','" & strStudentYear & "',
                                                            '" & Now.Month & "','" & Now.Day & "')", objConn)
                            objConn.Open()
                            Dim k = PJGDATA.ExecuteNonQuery()
                            objConn.Close()
                        End Using
                    End If

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                    Dim level As String = ""

                    If strStudentLevel = "Foundation 1" Or strStudentLevel = "Foundation 2" Or strStudentLevel = "Foundation 3" Then
                        level = "ASAS 1"
                    ElseIf strStudentLevel = "Level 1" Or strStudentLevel = "Level 2" Then
                        level = "TAHAP 1"
                    End If

                    Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & strStudentMykad & "'"
                    Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                    Dim find_PPCSDate As String = "select PPCSDate from PPCS where StudentID = '" & get_StudentID & "'"
                    Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                    strSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan) 
                                  values('" & get_StudentID & "','" & get_PPCSDate & "','" & strStudentYear & "','" & level & "','N',)"
                    strRet = oCommon.ExecuteSQLPermata(strSQL)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    Dim getcourse_ID As String = "select course_ID from course where std_ID = '" & datastd_ID & "' "
                    Dim datacourse_ID As String = oCommon.getFieldValue(getcourse_ID)

                    If datacourse_ID = "" Then
                        Using PJGDATA As New SqlCommand("INSERT INTO course(std_ID,year) values 
                                                            ('" & datastd_ID & "' ,'" & strStudentYear & "')", objConn)
                            objConn.Open()
                            Dim k = PJGDATA.ExecuteNonQuery()
                            objConn.Close()
                        End Using
                    End If

                    countInsertStudent = countInsertStudent + 1
                    errorData = 0
                Else
                    errorData = 1
                End If
            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsertStudent & " new student's records. " & countInsertParents & " new parent's records. " & countUpdateStudent & " student records updated and " & countUpdateParents & " parent records updated.", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        Dim strBil As String = ""

        Dim strStudentName As String = ""
        Dim strStudentID As String = ""
        Dim strStudentMykad As String = ""
        Dim strStudentGender As String = ""
        Dim strStudentRace As String = ""
        Dim strStudentReligion As String = ""
        Dim strStudentEmail As String = ""
        Dim strStudentPhone As String = ""
        Dim strStudentAddress As String = ""
        Dim strStudentPostcode As String = ""
        Dim strStudentCity As String = ""
        Dim strStudentState As String = ""
        Dim strStudentStateOfBirth As String = ""
        Dim strStudentStream As String = ""
        Dim strStudentCampus As String = ""
        Dim strStudentYear As String = ""

        Dim strStudentLevel As String = ""
        Dim strStudentSem As String = ""

        Dim strFatherMykad As String = ""
        Dim strFatherName As String = ""
        Dim strFatherEmail As String = ""
        Dim strFatherPhone As String = ""
        Dim strFatherJob As String = ""
        Dim strFatherSalary As String = ""

        Dim strMotherMykad As String = ""
        Dim strMotherName As String = ""
        Dim strMotherEMail As String = ""
        Dim strMotherPhone As String = ""
        Dim strMotherJob As String = ""
        Dim strMotherSalary As String = ""

    End Sub



    'Private Sub BtnUploadedStudentOnly_Alumni_ServerClick(sender As Object, e As EventArgs) Handles BtnUploadedStudentOnly_Alumni.ServerClick

    '    Try
    '        '--upload excel
    '        If ImportExcel_Alumni() = True Then

    '        Else
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Function ImportExcel_Alumni() As Boolean
    '    Dim path As String = String.Concat(Server.MapPath("~/import/student_import/"))

    '    If FlUploadcsv_Alumni.HasFile Then
    '        Dim rand As Random = New Random()
    '        Dim randNum = rand.Next(1000)
    '        Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv_Alumni.FileName
    '        FlUploadcsv_Alumni.PostedFile.SaveAs(fullFileName)

    '        '--required ms access engine
    '        Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
    '        Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
    '        Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [studentinfo$]", connection)
    '        Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
    '        Dim ds As DataSet = New DataSet

    '        Try
    '            connection.Open()
    '            da.Fill(ds)
    '            Dim validationMessage As String = ValidateSiteData_Alumni(ds)
    '            If validationMessage = "" Then

    '                SaveSiteDataAlumni(ds)

    '            Else
    '                Return False
    '            End If

    '            da.Dispose()
    '            connection.Close()
    '            command.Dispose()

    '        Catch ex As Exception
    '            Return False
    '        Finally
    '            If connection.State = ConnectionState.Open Then
    '                connection.Close()
    '            End If
    '        End Try

    '    Else
    '        Return False
    '    End If

    '    Return True

    'End Function

    'Protected Function ValidateSiteData_Alumni(ByVal SiteData As DataSet) As String
    '    Try
    '        'Loop through DataSet and validate data
    '        'If data is bad, bail out, otherwise continue on with the bulk copy
    '        Dim strMsg As String = ""
    '        Dim sb As StringBuilder = New StringBuilder()
    '        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")
    '            refreshVar_Alumni()
    '            strMsg = ""

    '            'BIL
    '            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("BIL")) Then
    '                strBil_Alumni = SiteData.Tables(0).Rows(i).Item("BIL")
    '            End If

    '            'STUDENT NAMA
    '            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("NAMA")) Then
    '                strStudentName_Alumni = SiteData.Tables(0).Rows(i).Item("NAMA")
    '            Else
    '                strMsg += " Please Enter Student Mykad |"
    '            End If

    '            'STUDENT MYKAD
    '            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MYKAD")) Then
    '                strStudentMykad_Alumni = SiteData.Tables(0).Rows(i).Item("MYKAD")
    '            Else
    '                strMsg += " Please Enter Student Name |"
    '            End If

    '            'STUDENT ID
    '            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("STUDENTID")) Then
    '                strStudentID_Alumni = SiteData.Tables(0).Rows(i).Item("STUDENTID")
    '            Else
    '                strMsg += " Please Enter Student ID |"
    '            End If

    '            'YEAR IN
    '            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("YEARIN")) Then
    '                strStudentMasok_Alumni = SiteData.Tables(0).Rows(i).Item("YEARIN")
    '            Else
    '                strMsg += " Please Enter Student Gender |"
    '            End If

    '            'YEAR OUT
    '            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("YEAROUT")) Then
    '                strStudentKeluar_Alumni = SiteData.Tables(0).Rows(i).Item("YEAROUT")
    '            Else
    '                strMsg += " Please Enter Student Race |"
    '            End If

    '            If strMsg.Length = 0 Then

    '            Else
    '                strMsg = "BIL# :" & strBil & " Mykad " & strStudentMykad & ":" & strStudentName & strMsg
    '                strMsg += "<br/>"
    '            End If

    '            sb.Append(strMsg)

    '        Next
    '        Return sb.ToString()
    '    Catch ex As Exception
    '        Return ex.Message
    '    End Try

    'End Function

    'Private Function SaveSiteDataAlumni(ByVal SiteData As DataSet) As String

    '    Dim display As String = ""
    '    Dim errorData As Integer = 0

    '    Dim countUpdateParents As Integer = 0
    '    Dim countUpdateStudent As Integer = 0
    '    Dim countInsertParents As Integer = 0
    '    Dim countInsertStudent As Integer = 0


    '    Dim sb As StringBuilder = New StringBuilder()
    '    For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")

    '        strBil_Alumni = SiteData.Tables(0).Rows(i).Item("BIL")

    '        ''STUDENT
    '        strStudentName_Alumni = SiteData.Tables(0).Rows(i).Item("NAMA")
    '        strStudentMykad_Alumni = SiteData.Tables(0).Rows(i).Item("MYKAD")
    '        strStudentID_Alumni = SiteData.Tables(0).Rows(i).Item("STUDENTID")
    '        strStudentMasok_Alumni = SiteData.Tables(0).Rows(i).Item("YEARIN")
    '        strStudentKeluar_Alumni = SiteData.Tables(0).Rows(i).Item("YEAROUT")

    '        ''UPDATE
    '        ''IF PARENT EXISTS
    '        strSQL = "Select distinct std_ID from student_info where student_ID = '" & strStudentID_Alumni & "' and student_Mykad = '" & strStudentMykad_Alumni & "'"
    '        Dim getAlumni_STDID As String = oCommon.getFieldValue(strSQL)

    '        If getAlumni_STDID.Length > 0 Then

    '            strSQL = "update student_info set student_Password = '" & strStudentMykad_Alumni & "', student_Status = 'Graduate', student_Batch = 'Loistava' where std_ID = '" & getAlumni_STDID & "'"
    '            strRet = oCommon.ExecuteSQL(strSQL)

    '            strSQL = "insert into student_level(std_ID,student_Sem,student_Level,year,month,day,Registered) values('" & getAlumni_STDID & "','Sem 1','Level 1','" & strStudentMasok_Alumni & "','01','01','Yes')"
    '            strRet = oCommon.ExecuteSQL(strSQL)

    '            strSQL = "insert into student_level(std_ID,student_Sem,student_Level,year,month,day,Registered) values('" & getAlumni_STDID & "','Sem 2','Level 1','" & strStudentMasok_Alumni & "','06','01','Yes')"
    '            strRet = oCommon.ExecuteSQL(strSQL)

    '            strSQL = "insert into student_level(std_ID,student_Sem,student_Level,year,month,day,Registered) values('" & getAlumni_STDID & "','Sem 1','Level 2','" & strStudentKeluar_Alumni & "','01','01','Yes')"
    '            strRet = oCommon.ExecuteSQL(strSQL)

    '            strSQL = "insert into student_level(std_ID,student_Sem,student_Level,year,month,day,Registered) values('" & getAlumni_STDID & "','Sem 2','Level 2','" & strStudentKeluar_Alumni & "','06','01','Yes')"
    '            strRet = oCommon.ExecuteSQL(strSQL)

    '        End If

    '    Next

    '    Return True

    'End Function

    'Private Sub refreshVar_Alumni()

    '    Dim strBil_Alumni As String = ""

    '    Dim strStudentName_Alumni As String = ""
    '    Dim strStudentID_Alumni As String = ""
    '    Dim strStudentMykad_Alumni As String = ""
    '    Dim strStudentMasok_Alumni As String = ""
    '    Dim strStudentKeluar_Alumni As String = ""

    'End Sub



End Class