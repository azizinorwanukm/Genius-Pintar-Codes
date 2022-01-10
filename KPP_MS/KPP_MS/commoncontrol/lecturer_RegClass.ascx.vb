Imports System.Data.SqlClient

Public Class lecturer_RegClass
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

                If Session("getStatus") = "VC" Then ''View Staff Course
                    txtbreadcrum1.Text = "View Staff Course"

                    ViewCourse.Visible = True
                    RegisterCourse.Visible = False

                    btnViewCourse.Attributes("class") = "btn btn-info"
                    btnRegisterCourse.Attributes("class") = "btn btn-default font"

                    ddlCourse.Enabled = False
                    year_list()
                    course_level_list()
                    course_sem_list()
                    course_campus_list()
                    course_program_list()

                    strRet = BindDataView(ViewdatRespondent)

                ElseIf Session("getStatus") = "RC" Then ''Register Staff Course
                    txtbreadcrum1.Text = "Register Staff Course"

                    ViewCourse.Visible = False
                    RegisterCourse.Visible = True

                    btnViewCourse.Attributes("class") = "btn btn-default font"
                    btnRegisterCourse.Attributes("class") = "btn btn-info"

                    subject_year_list()
                    subject_sem_list()
                    subject_campus_list()
                    subject_program_list()
                    subject_level_list()
                    fillStaffDDL()

                    strRet = BindData(datRespondent)
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewCourse.Visible = False
        btnRegisterCourse.Visible = False
        ViewCourse.Visible = False
        RegisterCourse.Visible = False

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

        Dim Get_ViewCourse As String = ""
        Dim Get_RegisterCourse As String = ""

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

            ''Get Function Button 1 Register Data 
            strSQL = "  Select B.F1_Register From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Register As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "View Staff Course" And find_Data_SubMenu2.Length > 0 Then
                btnViewCourse.Visible = True
                ViewCourse.Visible = True

                Get_ViewCourse = "TRUE"
            End If

            If find_Data_SubMenu2 = "Register Staff Course" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterCourse.Visible = True
                RegisterCourse.Visible = True

                Get_RegisterCourse = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewCourse.Visible = True
                btnRegisterCourse.Visible = True
                RegisterCourse.Visible = True
                ViewCourse.Visible = True

                Btnsimpan.Visible = True

                Get_ViewCourse = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "VC" Or Session("getStatus") = "RC" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "VC" And Session("getStatus") <> "RC" Then
            If Get_ViewCourse = "TRUE" Then
                Data_If_Not_Group_Status = "VC"
            ElseIf Get_RegisterCourse = "TRUE" Then
                Data_If_Not_Group_Status = "RC"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewCourse = "TRUE" And Data_If_Not_Group_Status = "VC" Then
                Session("getStatus") = "VC"
            ElseIf Get_RegisterCourse = "TRUE" And Data_If_Not_Group_Status = "RC" Then
                Session("getStatus") = "RC"
            End If
        End If

    End Sub

    Private Sub btnViewCourse_ServerClick(sender As Object, e As EventArgs) Handles btnViewCourse.ServerClick
        Session("getStatus") = "VC"
        Response.Redirect("admin_pengajar_penempatan_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnRegisterCourse_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterCourse.ServerClick
        Session("getStatus") = "RC"
        Response.Redirect("admin_pengajar_penempatan_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub subject_sem_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            Dim strLevelSql As String = "Select Parameter, Value from setting where Type = 'Sem'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlFilterSems.DataSource = levds
            ddlFilterSems.DataValueField = "Value"
            ddlFilterSems.DataTextField = "Parameter"
            ddlFilterSems.DataBind()
            ddlFilterSems.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub subject_campus_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlFilterCampus.DataSource = levds
            ddlFilterCampus.DataValueField = "Value"
            ddlFilterCampus.DataTextField = "Parameter"
            ddlFilterCampus.DataBind()
            ddlFilterCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub subject_program_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If ddlFilterCampus.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlFilterProgram.DataSource = levds
            ddlFilterProgram.DataValueField = "Value"
            ddlFilterProgram.DataTextField = "Parameter"
            ddlFilterProgram.DataBind()
            ddlFilterProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub subject_level_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlFilterLevel.DataSource = levds
            ddlFilterLevel.DataValueField = "Parameter"
            ddlFilterLevel.DataTextField = "Parameter"
            ddlFilterLevel.DataBind()
            ddlFilterLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub subject_year_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Year'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlYear.DataSource = levds
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_StudentYear, subject_type, subject_Name, subject_sem ASC"

        tmpSQL = "select * from subject_info"
        strWhere = " where subject_year = '" & ddlYear.SelectedValue & "' and subject_Campus = '" & ddlFilterCampus.SelectedValue & "' and course_Program = '" & ddlFilterProgram.SelectedValue & "'"

        If ddlFilterSems.SelectedIndex > 0 Then
            strWhere += " And subject_sem = '" & ddlFilterSems.SelectedValue & "'"
        End If

        If ddlFilterLevel.SelectedIndex > 0 Then
            strWhere += " And subject_StudentYear = '" & ddlFilterLevel.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim subject_checking As String = "Select ID from lecturer where subject_ID = '" & strKey & "' and lecturer_year = '" & ddlYear.SelectedValue & "' and stf_ID = '" & ddlstaffChoose.SelectedValue & "'"
                    Dim dataSubject_Check As String = oCommon.getFieldValue(subject_checking)

                    If dataSubject_Check.Length = 0 Then

                        strSQL = "INSERT INTO lecturer(stf_ID, subject_ID, lecturer_year) VALUES('" & ddlstaffChoose.SelectedValue & "','" & strKey & "','" & ddlYear.SelectedValue & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet = "0" Then
                            ShowMessage(" Register Staff Course", MessageType.Success)
                        Else
                            ShowMessage(" Unsuccessful Add Staff Course", MessageType.Error)
                        End If

                    ElseIf dataSubject_Check.Length > 0 Then
                        ShowMessage(" Staff Course Had Been Registered", MessageType.Error)
                    End If

                End If
            End If
        Next

    End Sub

    Private Sub fillStaffDDL()
        Try

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            strSQL = "SELECT stf_ID,staff_Name FROM staff_Info where staff_Status = 'Access' and staff_Name not like '%araken%' and staff_Campus = '" & ddlFilterCampus.SelectedValue & "' order by staff_Name asc"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaffChoose.DataSource = ds
            ddlstaffChoose.DataTextField = "staff_Name"
            ddlstaffChoose.DataValueField = "stf_ID"
            ddlstaffChoose.DataBind()
            ddlstaffChoose.Items.Insert(0, New ListItem("Select Staff", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlFilterSems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterSems.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlFilterCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterCampus.SelectedIndexChanged
        Try
            fillStaffDDL()
            subject_program_list()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlFilterProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterProgram.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlFilterLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterLevel.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
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

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseYear.DataSource = ds
            ddlCourseYear.DataTextField = "Parameter"
            ddlCourseYear.DataValueField = "Parameter"
            ddlCourseYear.DataBind()
            ddlCourseYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_level_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseLevel.DataSource = ds
            ddlCourseLevel.DataTextField = "Parameter"
            ddlCourseLevel.DataValueField = "Parameter"
            ddlCourseLevel.DataBind()
            ddlCourseLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlCourseLevel.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_sem_list()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseSem.DataSource = ds
            ddlCourseSem.DataTextField = "Parameter"
            ddlCourseSem.DataValueField = "Value"
            ddlCourseSem.DataBind()
            ddlCourseSem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlCourseSem.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_campus_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlCourseCampus.DataSource = levds
            ddlCourseCampus.DataValueField = "Value"
            ddlCourseCampus.DataTextField = "Parameter"
            ddlCourseCampus.DataBind()
            ddlCourseCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub course_program_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If ddlCourseCampus.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlCourseProgram.DataSource = levds
            ddlCourseProgram.DataValueField = "Value"
            ddlCourseProgram.DataTextField = "Parameter"
            ddlCourseProgram.DataBind()
            ddlCourseProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub class_name_list()

        Dim checkSubjectType As String = "Select subject_type from subject_info where subject_ID = '" & ddlCourse.SelectedValue & "'"
        Dim findSubjectType As String = oCommon.getFieldValue(checkSubjectType)

        strSQL = "SELECT class_Name,class_ID FROM class_info WHERE class_year = '" & ddlCourseYear.SelectedValue & "' and class_level = '" & ddlCourseLevel.SelectedValue & "' and class_type = 'Compulsory' and class_Campus = '" & ddlCourseCampus.SelectedValue & "' and course_Program = '" & ddlCourseProgram.SelectedValue & "' order by class_Name asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClass.DataSource = ds
            ddlClass.DataTextField = "class_Name"
            ddlClass.DataValueField = "class_ID"
            ddlClass.DataBind()
            ddlClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlClass.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_name_list()

        strSQL = "SELECT subject_Name,subject_ID FROM subject_info WHERE subject_year ='" & ddlCourseYear.SelectedValue & "' and subject_info.subject_StudentYear = '" & ddlCourseLevel.SelectedValue & "' and subject_info.subject_sem = '" & ddlCourseSem.SelectedValue & "' and subject_info.subject_Campus = '" & ddlCourseCampus.SelectedValue & "' and subject_info.course_Program = '" & ddlCourseProgram.SelectedValue & "' order by subject_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            ddlCourse.Enabled = True

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourse.DataSource = ds
            ddlCourse.DataTextField = "subject_Name"
            ddlCourse.DataValueField = "subject_ID"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourse.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataView(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLView, strConn)
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

    Private Function getSQLView() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY staff_Info.staff_Name ASC"

        tmpSQL = "select lecturer.ID,staff_Info.staff_Name,subject_info.subject_StudentYear,subject_info.subject_Name,subject_info.subject_sem,class_info.class_Name from lecturer
                  left join staff_Info on lecturer.stf_ID=staff_Info.stf_ID
                  left join subject_info on lecturer.subject_ID=subject_info.subject_ID
                  left join class_info on lecturer.class_ID=class_info.class_ID"
        strWhere = "    WHERE staff_Info.stf_ID IS NOT NULL and subject_info.subject_Campus = '" & ddlCourseCampus.SelectedValue & "' and class_info.class_campus = '" & ddlCourseCampus.SelectedValue & "'
                        and subject_info.course_Program = '" & ddlCourseProgram.SelectedValue & "' and class_info.course_Program = '" & ddlCourseProgram.SelectedValue & "'"
        strWhere += " and lecturer.lecturer_year = '" & ddlCourseYear.SelectedValue & "'"

        If ddlCourse.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_ID = '" & ddlCourse.SelectedValue & "'"
        End If

        If ddlClass.SelectedIndex > 0 Then
            strWhere += " AND class_info.class_ID = '" & ddlClass.SelectedValue & "'"
        End If

        If ddlCourseSem.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_sem = '" & ddlCourseSem.SelectedValue & "'"
        End If

        If ddlCourseSem.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_StudentYear = '" & ddlCourseLevel.SelectedValue & "'"
        End If

        getSQLView = tmpSQL & strWhere & strOrderby

        Return getSQLView
    End Function

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass.SelectedIndexChanged
        Try
            strRet = BindDataView(ViewdatRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourseSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseSem.SelectedIndexChanged
        Try
            course_name_list()
            strRet = BindDataView(ViewdatRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourseYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseYear.SelectedIndexChanged
        Try
            course_name_list()
            strRet = BindDataView(ViewdatRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourseCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseCampus.SelectedIndexChanged
        Try
            course_program_list()
            class_name_list()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourseProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseProgram.SelectedIndexChanged
        Try
            class_name_list()
            strRet = BindDataView(ViewdatRespondent)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        Try
            class_name_list()
            strRet = BindDataView(ViewdatRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourseLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseLevel.SelectedIndexChanged
        Try
            course_name_list()
            class_name_list()
            strRet = BindDataView(ViewdatRespondent)
        Catch ex As Exception

        End Try
    End Sub

End Class