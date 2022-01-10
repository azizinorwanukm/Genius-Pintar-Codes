Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class student_view_course
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "VCourse" Then ''View Course
                    txtbreadcrum1.Text = "View Course"

                    ViewCourse.Visible = True
                    ViewClass.Visible = False
                    ViewCocurriculum.Visible = False
                    ViewHostel.Visible = False
                    ViewReligion.Visible = False
                    ViewStudentID_Program.Visible = False

                    btnViewCourse.Attributes("class") = "btn btn-info"
                    btnViewClass.Attributes("class") = "btn btn-default font"
                    btnViewCocuriculum.Attributes("class") = "btn btn-default font"
                    btnViewHostel.Attributes("class") = "btn btn-default font"
                    btnViewReligion.Attributes("class") = "btn btn-default font"
                    btnViewStudentID.Attributes("class") = "btn btn-default font"

                    year_list()
                    student_Level()
                    student_Sem()

                    subject_list()
                    student_Program_list()
                    student_Campus_list()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "VClass" Then ''View Class
                    txtbreadcrum1.Text = "View Class"

                    ViewCourse.Visible = False
                    ViewClass.Visible = True
                    ViewCocurriculum.Visible = False
                    ViewHostel.Visible = False
                    ViewReligion.Visible = False
                    ViewStudentID_Program.Visible = False

                    btnViewCourse.Attributes("class") = "btn btn-default font"
                    btnViewClass.Attributes("class") = "btn btn-info"
                    btnViewCocuriculum.Attributes("class") = "btn btn-default font"
                    btnViewHostel.Attributes("class") = "btn btn-default font"
                    btnViewReligion.Attributes("class") = "btn btn-default font"
                    btnViewStudentID.Attributes("class") = "btn btn-default font"

                    year_list_data()
                    level_list_data()
                    student_Program_list()
                    student_Campus_list()

                    strRet = BindDataClass(datRespondentClass)

                ElseIf Session("getStatus") = "VCocurriculum" Then ''View Cocurriculum
                    txtbreadcrum1.Text = "View Cocurriculum"

                    ViewCourse.Visible = False
                    ViewClass.Visible = False
                    ViewCocurriculum.Visible = True
                    ViewHostel.Visible = False
                    ViewReligion.Visible = False
                    ViewStudentID_Program.Visible = False

                    btnViewCourse.Attributes("class") = "btn btn-default font"
                    btnViewClass.Attributes("class") = "btn btn-default font"
                    btnViewCocuriculum.Attributes("class") = "btn btn-info"
                    btnViewHostel.Attributes("class") = "btn btn-default font"
                    btnViewReligion.Attributes("class") = "btn btn-default font"
                    btnViewStudentID.Attributes("class") = "btn btn-default font"

                    year_list_info()
                    koko_type_info()
                    koko_list_info()
                    student_Program_list()
                    student_Campus_list()

                    strRet = BindDataCocurriculum(datRespondentCocurriculum)

                ElseIf Session("getStatus") = "VHostel" Then ''View Hostel
                    txtbreadcrum1.Text = "View Hostel"

                    ViewCourse.Visible = False
                    ViewClass.Visible = False
                    ViewCocurriculum.Visible = False
                    ViewHostel.Visible = True
                    ViewReligion.Visible = False
                    ViewStudentID_Program.Visible = False

                    btnViewCourse.Attributes("class") = "btn btn-default font"
                    btnViewClass.Attributes("class") = "btn btn-default font"
                    btnViewCocuriculum.Attributes("class") = "btn btn-default font"
                    btnViewHostel.Attributes("class") = "btn btn-info"
                    btnViewReligion.Attributes("class") = "btn btn-default font"
                    btnViewStudentID.Attributes("class") = "btn btn-default font"

                    block_name_list()
                    block_level_list()
                    hostel_sem_list()
                    hostel_year_list()

                    strRet = BindDataHostel(datRespondentHostel)

                ElseIf Session("getStatus") = "VReligion" Then ''View Religion
                    txtbreadcrum1.Text = "View Religion"

                    ViewCourse.Visible = False
                    ViewClass.Visible = False
                    ViewCocurriculum.Visible = False
                    ViewHostel.Visible = False
                    ViewReligion.Visible = True
                    ViewStudentID_Program.Visible = False

                    btnViewCourse.Attributes("class") = "btn btn-default font"
                    btnViewClass.Attributes("class") = "btn btn-default font"
                    btnViewCocuriculum.Attributes("class") = "btn btn-default font"
                    btnViewHostel.Attributes("class") = "btn btn-default font"
                    btnViewReligion.Attributes("class") = "btn btn-info"
                    btnViewStudentID.Attributes("class") = "btn btn-default font"

                    religion_year_list()
                    religion_student_Level()
                    religion_student_Religions()
                    student_Program_list()
                    student_Campus_list()

                    strRet = BindDataReligion(datRespondentReligion)

                ElseIf Session("getStatus") = "VStudentID" Then ''View Student ID / Program
                    txtbreadcrum1.Text = "View Student ID & Program"

                    ViewCourse.Visible = False
                    ViewClass.Visible = False
                    ViewCocurriculum.Visible = False
                    ViewHostel.Visible = False
                    ViewReligion.Visible = False
                    ViewStudentID_Program.Visible = True

                    btnViewCourse.Attributes("class") = "btn btn-default font"
                    btnViewClass.Attributes("class") = "btn btn-default font"
                    btnViewCocuriculum.Attributes("class") = "btn btn-default font"
                    btnViewHostel.Attributes("class") = "btn btn-default font"
                    btnViewReligion.Attributes("class") = "btn btn-default font"
                    btnViewStudentID.Attributes("class") = "btn btn-info"

                    SIP_YearList()
                    SIP_CampusList()
                    SIP_ProgramList()
                    SIP_LevelList()
                    SIP_SemList()
                    SIP_ClassList()
                    SIP_RegisteredList()

                    strRet = BindDataSIP(datRespondent_SIP)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewCourse.Visible = False
        btnViewClass.Visible = False
        btnViewCocuriculum.Visible = False
        btnViewHostel.Visible = False
        btnViewReligion.Visible = False
        btnViewStudentID.Visible = False

        ViewCourse.Visible = False
        ViewClass.Visible = False
        ViewCocurriculum.Visible = False
        ViewHostel.Visible = False
        ViewReligion.Visible = False
        ViewStudentID_Program.Visible = False

        BtnAdd.Visible = False
        btnAddClass.Visible = False
        btnUpdateReligion.Visible = False
        btnUpdateStudentIDProgram.Visible = False

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
        Dim Get_ViewClass As String = ""
        Dim Get_ViewCocurriculum As String = ""
        Dim Get_ViewHostel As String = ""
        Dim Get_ViewReligion As String = ""
        Dim Get_ViewStudentID_Program As String = ""

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

            ''Get Function Button 1 Add A Droupout Student Data 
            strSQL = "  Select B.F1_AddADropoutStudent From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1AddADropoutStudent As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Update Data 
            strSQL = "  Select B.F1_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Update As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "View Course" And find_Data_SubMenu2.Length > 0 Then
                btnViewCourse.Visible = True
                ViewCourse.Visible = True

                Get_ViewCourse = "TRUE"

                If find_Data_F1AddADropoutStudent.Length > 0 And find_Data_F1AddADropoutStudent = "TRUE" Then
                    BtnAdd.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "View Class" And find_Data_SubMenu2.Length > 0 Then
                btnViewClass.Visible = True
                ViewClass.Visible = True

                Get_ViewClass = "TRUE"

                If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                    btnAddClass.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "View Cocurriculum" And find_Data_SubMenu2.Length > 0 Then
                btnViewCocuriculum.Visible = True
                ViewCocurriculum.Visible = True

                Get_ViewCocurriculum = "TRUE"
            End If

            If find_Data_SubMenu2 = "View Hostel" And find_Data_SubMenu2.Length > 0 Then
                btnViewHostel.Visible = True
                ViewHostel.Visible = True

                Get_ViewHostel = "TRUE"
            End If

            If find_Data_SubMenu2 = "View Religion" And find_Data_SubMenu2.Length > 0 Then
                btnViewReligion.Visible = True
                ViewReligion.Visible = True

                Get_ViewReligion = "TRUE"

                If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                    btnUpdateReligion.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "View Student ID & Program" And find_Data_SubMenu2.Length > 0 Then
                btnViewStudentID.Visible = True
                ViewStudentID_Program.Visible = True

                Get_ViewStudentID_Program = "TRUE"

                If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                    btnUpdateStudentIDProgram.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewCourse.Visible = True
                btnViewClass.Visible = True
                btnViewCocuriculum.Visible = True
                btnViewHostel.Visible = True
                btnViewReligion.Visible = True
                btnViewStudentID.Visible = True
                ViewCourse.Visible = True
                ViewClass.Visible = True
                ViewCocurriculum.Visible = True
                ViewHostel.Visible = True
                ViewReligion.Visible = True
                ViewStudentID_Program.Visible = True

                BtnAdd.Visible = True
                btnAddClass.Visible = True
                btnUpdateReligion.Visible = True
                btnUpdateStudentIDProgram.Visible = True

                Get_ViewCourse = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "VCourse" Or Session("getStatus") = "VClass" Or Session("getStatus") = "VCocurriculum" Or Session("getStatus") = "VHostel" Or Session("getStatus") = "VReligion" Or Session("getStatus") = "VStudentID" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "VCourse" And Session("getStatus") <> "VClass" And Session("getStatus") <> "VCocurriculum" And Session("getStatus") <> "VHostel" And Session("getStatus") <> "VReligion" And Session("getStatus") <> "VStudentID" Then
            If Get_ViewCourse = "TRUE" Then
                Data_If_Not_Group_Status = "VCourse"
            ElseIf Get_ViewClass = "TRUE" Then
                Data_If_Not_Group_Status = "VClass"
            ElseIf Get_ViewCocurriculum = "TRUE" Then
                Data_If_Not_Group_Status = "VCocurriculum"
            ElseIf Get_ViewHostel = "TRUE" Then
                Data_If_Not_Group_Status = "VHostel"
            ElseIf Get_ViewReligion = "TRUE" Then
                Data_If_Not_Group_Status = "VReligion"
            ElseIf Get_ViewStudentID_Program = "TRUE" Then
                Data_If_Not_Group_Status = "VStudentID"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewCourse = "TRUE" And Data_If_Not_Group_Status = "VCourse" Then
                Session("getStatus") = "VCourse"
            ElseIf Get_ViewClass = "TRUE" And Data_If_Not_Group_Status = "VClass" Then
                Session("getStatus") = "VClass"
            ElseIf Get_ViewCocurriculum = "TRUE" And Data_If_Not_Group_Status = "VCocurriculum" Then
                Session("getStatus") = "VCocurriculum"
            ElseIf Get_ViewHostel = "TRUE" And Data_If_Not_Group_Status = "VHostel" Then
                Session("getStatus") = "VHostel"
            ElseIf Get_ViewReligion = "TRUE" And Data_If_Not_Group_Status = "VReligion" Then
                Session("getStatus") = "VReligion"
            ElseIf Get_ViewStudentID_Program = "TRUE" And Data_If_Not_Group_Status = "VStudentID" Then
                Session("getStatus") = "VStudentID"
            End If
        End If

    End Sub

    Private Sub btnViewCourse_ServerClick(sender As Object, e As EventArgs) Handles btnViewCourse.ServerClick
        Session("getStatus") = "VCourse"
        Response.Redirect("admin_pelajar_kepastian_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewClass_ServerClick(sender As Object, e As EventArgs) Handles btnViewClass.ServerClick
        Session("getStatus") = "VClass"
        Response.Redirect("admin_pelajar_kepastian_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewCocuriculum_ServerClick(sender As Object, e As EventArgs) Handles btnViewCocuriculum.ServerClick
        Session("getStatus") = "VCocurriculum"
        Response.Redirect("admin_pelajar_kepastian_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewHostel_ServerClick(sender As Object, e As EventArgs) Handles btnViewHostel.ServerClick
        Session("getStatus") = "VHostel"
        Response.Redirect("admin_pelajar_kepastian_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewReligion_ServerClick(sender As Object, e As EventArgs) Handles btnViewReligion.ServerClick
        Session("getStatus") = "VReligion"
        Response.Redirect("admin_pelajar_kepastian_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewStudentID_ServerClick(sender As Object, e As EventArgs) Handles btnViewStudentID.ServerClick
        Session("getStatus") = "VStudentID"
        Response.Redirect("admin_pelajar_kepastian_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub student_Program_list()

        If ddlCampus.SelectedValue = "APP" Or ddlClassCampus.SelectedValue = "APP" Or ddlKokoCampus.SelectedValue = "APP" Or ddlReligionCampus.SelectedValue = "APP" Then
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

            ddlProgram.DataSource = ds
            ddlProgram.DataTextField = "Parameter"
            ddlProgram.DataValueField = "Value"
            ddlProgram.DataBind()
            ddlProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

            ddlClassProgram.DataSource = ds
            ddlClassProgram.DataTextField = "Parameter"
            ddlClassProgram.DataValueField = "Value"
            ddlClassProgram.DataBind()
            ddlClassProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

            ddlKokoProgram.DataSource = ds
            ddlKokoProgram.DataTextField = "Parameter"
            ddlKokoProgram.DataValueField = "Value"
            ddlKokoProgram.DataBind()
            ddlKokoProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

            ddlReligionProgram.DataSource = ds
            ddlReligionProgram.DataTextField = "Parameter"
            ddlReligionProgram.DataValueField = "Value"
            ddlReligionProgram.DataBind()
            ddlReligionProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Campus_list()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCampus.DataSource = ds
            ddlCampus.DataTextField = "Parameter"
            ddlCampus.DataValueField = "Value"
            ddlCampus.DataBind()
            ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

            ddlClassCampus.DataSource = ds
            ddlClassCampus.DataTextField = "Parameter"
            ddlClassCampus.DataValueField = "Value"
            ddlClassCampus.DataBind()
            ddlClassCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

            ddlKokoCampus.DataSource = ds
            ddlKokoCampus.DataTextField = "Parameter"
            ddlKokoCampus.DataValueField = "Value"
            ddlKokoCampus.DataBind()
            ddlKokoCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

            ddlReligionCampus.DataSource = ds
            ddlReligionCampus.DataTextField = "Parameter"
            ddlReligionCampus.DataValueField = "Value"
            ddlReligionCampus.DataBind()
            ddlReligionCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

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

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Sem()
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

    Private Sub subject_list()

        strSQL = " select * from subject_info where subject_Sem ='" & ddlSem.SelectedValue & "'"
        strSQL += " And subject_year = '" & ddlYear.SelectedValue & "'"
        strSQL += " And subject_StudentYear = '" & ddlLevel.SelectedValue & "'"
        strSQL += " And course_Program = '" & ddlProgram.SelectedValue & "' and subject_info.subject_Campus = '" & ddlCampus.SelectedValue & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourse.DataSource = ds
            ddlCourse.DataTextField = "subject_Name"
            ddlCourse.DataValueField = "subject_ID"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
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
        Dim strOrderby As String = " order by student_Level.student_Level, class_info.class_Name, subject_info.subject_Name, student_Name asc"

        tmpSQL = "  Select distinct course.course_ID,
                    UPPER(student_info.student_Name) student_Name,
                    student_Level.student_Level,
                    subject_info.subject_sem,
                    class_info.class_Name,
                    subject_info.subject_Name,
                    student_info.student_ID,
                    student_info.student_Campus
                    From student_info 
                    left join student_Level on student_info.std_ID=student_Level.std_ID
                    left join course on student_info.std_ID= course.std_ID
                    left join subject_info on course.subject_ID=subject_info.subject_ID
                    left join class_info on course.class_ID= class_info.class_ID"
        strWhere = " WHERE student_info.std_ID IS NOT NULL and student_info.student_status = 'Access' and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"
        strWhere += " and course.year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and student_level.year = '" & ddlYear.SelectedValue & "'"

        strWhere += " and student_info.student_Campus = '" & ddlCampus.SelectedValue & "' and class_info.class_Campus = '" & ddlCampus.SelectedValue & "' and subject_info.subject_Campus = '" & ddlCampus.SelectedValue & "'"

        If ddlCourse.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_ID = '" & ddlCourse.SelectedValue & "'"
        End If

        If ddlLevel.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "'"
            strWhere += " AND student_level.student_Sem = '" & ddlSem.SelectedValue & "'"
        End If

        If ddlProgram.SelectedIndex > 0 Then
            strWhere += " AND student_info.student_Stream = '" & ddlProgram.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Protected Sub ddlProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgram.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampus.SelectedIndexChanged
        Try
            student_Program_list()
            subject_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        If ddlLevel.SelectedIndex > 0 Then
            subject_list()
            strRet = BindData(datRespondent)
        End If
    End Sub


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try

            If ddlYear.SelectedValue <> Now.Year Then
                ShowMessage("Unable delete previous data", MessageType.Error)
            Else
                Dim userAccess As String = "select course_ID from course where course_ID = '" & strKeyName & "'"
                Dim access As String = oCommon.getFieldValue(userAccess)

                Dim MyConnection As SqlConnection = New SqlConnection(strConn)
                Dim Dlt_ClassData As New SqlDataAdapter()

                Dim dlt_Class As String

                Dlt_ClassData.SelectCommand = New SqlCommand()
                Dlt_ClassData.SelectCommand.Connection = MyConnection
                Dlt_ClassData.SelectCommand.CommandText = "delete course where course_ID ='" & access & "'"
                MyConnection.Open()
                dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
                MyConnection.Close()

                ShowMessage(" Delete data", MessageType.Success)

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnAdd_ServerClick(sender As Object, e As EventArgs) Handles BtnAdd.ServerClick
        Response.Redirect("admin_edit_pelajar_kursus_data.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnExport_CourseInformation_ServerClick(sender As Object, e As EventArgs) Handles btnExport_CourseInformation.ServerClick

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_Level.student_Level asc, student_info.student_Name asc, subject_info.subject_Name asc, class_info.class_Name ASC"

        tmpSQL = "  Select distinct
                    student_info.student_Name,
                    student_info.student_ID,
                    student_Level.student_Level,
                    student_Level.student_Sem,
                    class_info.class_Name,
                    subject_info.subject_Name,
                    subject_info.subject_code
                    From student_info 
                    left join student_Level on student_info.std_ID=student_Level.std_ID
                    left join course on student_info.std_ID= course.std_ID
                    left join subject_info on course.subject_ID=subject_info.subject_ID
                    left join class_info on course.class_ID= class_info.class_ID"
        strWhere = " WHERE student_info.std_ID IS NOT NULL and student_info.student_status = 'Access' and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"
        strWhere += " and course.year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and student_level.year = '" & ddlYear.SelectedValue & "' and student_info.student_Campus = '" & ddlCampus.SelectedValue & "' and class_info.class_Campus = '" & ddlCampus.SelectedValue & "' and subject_info.subject_Campus = '" & ddlCampus.SelectedValue & "'"

        If ddlCourse.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_ID = '" & ddlCourse.SelectedValue & "'"
        End If

        If ddlLevel.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "'"
            strWhere += " AND student_level.student_Sem = '" & ddlSem.SelectedValue & "'"
        End If

        If ddlProgram.SelectedIndex > 0 Then
            strWhere += " AND student_info.student_Stream = '" & ddlProgram.SelectedValue & "'"
        End If

        Dim data_getSQL As String = tmpSQL & strWhere & strOrderby

        ExportToCSV(data_getSQL)
    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    'export 
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & ddlYear.SelectedValue & "_StudentCourseInformation_" & ddlLevel.SelectedValue & ".csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

    Private Sub ddlSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSem.SelectedIndexChanged

        If ddlLevel.SelectedIndex > 0 Then
            subject_list()
            strRet = BindData(datRespondent)
        End If
    End Sub

    Private Sub year_list_data()
        strSQL = "SELECT Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassYear.DataSource = ds
            ddlClassYear.DataTextField = "Parameter"
            ddlClassYear.DataValueField = "Parameter"
            ddlClassYear.DataBind()
            ddlClassYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub level_list_data()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassLevel.DataSource = ds
            ddlClassLevel.DataTextField = "Parameter"
            ddlClassLevel.DataValueField = "Parameter"
            ddlClassLevel.DataBind()
            ddlClassLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlClassProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassProgram.SelectedIndexChanged
        Try
            class_list_data()
            strRet = BindDataClass(datRespondentClass)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlClassCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassCampus.SelectedIndexChanged
        Try
            student_Program_list()
            class_list_data()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlClassLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassLevel.SelectedIndexChanged
        Try
            class_list_data()
            strRet = BindDataClass(datRespondentClass)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlClassNameSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassNameSelect.SelectedIndexChanged
        Try
            BindDataClass(datRespondentClass)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataClass(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLClass, strConn)
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

    Private Function getSQLClass() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by class_info.class_Name, student_info.student_Name ASC"

        tmpSQL = "select distinct student_info.std_ID,student_info.student_Name, student_info.student_Mykad, student_info.student_ID, student_info.student_Campus,
                  class_info.class_level, class_info.class_year, class_info.class_Name from course
                  left join class_info on course.class_ID = class_info.class_ID
                  left join student_info on course.std_ID = student_info.std_ID"
        strWhere = " where class_info.class_year = '" & ddlClassYear.SelectedValue & "' and student_info.student_status = 'Access' and student_info.student_ID is not null and student_info.student_ID <> '' and ((student_info.student_ID like '%M%' or student_info.student_ID like '%P%') or (student_info.student_ID like '%M%' or student_info.student_ID like '%P%'))"
        strWhere += " and class_info.class_level = '" & ddlClassLevel.SelectedValue & "'"

        strWhere += " and student_info.student_Campus = '" & ddlClassCampus.SelectedValue & "'"

        If ddlClassNameSelect.SelectedIndex > 0 Then
            strWhere += " and class_info.class_ID = '" & ddlClassNameSelect.SelectedValue & "'"
        End If

        strWhere += " and class_info.class_type = 'Compulsory'"

        If ddlClassProgram.SelectedIndex > 0 Then
            strWhere += " AND student_info.student_Stream = '" & ddlClassProgram.SelectedValue & "'"
        End If

        getSQLClass = tmpSQL & strWhere & strOrderby

        Return getSQLClass
    End Function

    Private Sub datRespondentClass_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondentClass.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondentClass.DataKeys(e.RowIndex).Value.ToString

        Try
            If ddlClassNameSelect.SelectedIndex = 0 Then
                ShowMessage(" Please Select Class Name On Top Before Delete The Data ", MessageType.Error)
            Else
                Dim MyConnection As SqlConnection = New SqlConnection(strConn)
                Dim Dlt_ClassData As New SqlDataAdapter()

                Dim dlt_Class As String

                Dlt_ClassData.SelectCommand = New SqlCommand()
                Dlt_ClassData.SelectCommand.Connection = MyConnection
                Dlt_ClassData.SelectCommand.CommandText = "delete course where std_ID ='" & strKeyName & "' and year = '" & ddlClassYear.SelectedValue & "' and class_ID = '" & ddlClassNameSelect.SelectedValue & "'"
                MyConnection.Open()
                dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
                MyConnection.Close()
            End If

            strRet = BindDataClass(datRespondentClass)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub class_list_data()

        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & ddlClassYear.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlClassLevel.SelectedValue & "' and course_Program = '" & ddlClassProgram.SelectedValue & "' and class_Campus = '" & ddlClassCampus.SelectedValue & "' order by class_Name ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassName.DataSource = ds
            ddlClassName.DataTextField = "class_Name"
            ddlClassName.DataValueField = "class_ID"
            ddlClassName.DataBind()
            ddlClassName.Items.Insert(0, New ListItem("Select Class", String.Empty))

            ddlClassNameSelect.DataSource = ds
            ddlClassNameSelect.DataTextField = "class_Name"
            ddlClassNameSelect.DataValueField = "class_ID"
            ddlClassNameSelect.DataBind()
            ddlClassNameSelect.Items.Insert(0, New ListItem("Select Class", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnAddClass_ServerClick(sender As Object, e As EventArgs) Handles btnAddClass.ServerClick
        Dim i As Integer

        For i = 0 To datRespondentClass.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondentClass.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondentClass.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim find_old_class As String = "Select distinct course.class_ID from course
                                                    Left Join class_info on course.class_ID = class_info.class_ID Left Join student_info on course.std_ID = student_info.std_ID
                                                    where class_info.class_year = '" & ddlClassYear.SelectedValue & "' And class_info.class_type = 'Compulsory' And course.std_ID = '" & strKey & "'"
                    Dim data_old_class As String = oCommon.getFieldValue(find_old_class)

                    'UPDATE
                    strSQL = "UPDATE course set class_ID ='" & ddlClassName.SelectedValue & "' WHERE std_ID ='" & strKey & "' and year = '" & ddlClassYear.SelectedValue & "' and class_ID = '" & data_old_class & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            End If
        Next

        If strRet = "0" Then
            ShowMessage(" Update Student Class", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Update Student Class", MessageType.Error)
        End If
    End Sub

    Private Sub year_list_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKokoYear.DataSource = ds
            ddlKokoYear.DataTextField = "Parameter"
            ddlKokoYear.DataValueField = "Parameter"
            ddlKokoYear.DataBind()
            ddlKokoYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_type_info()
        strSQL = "Select Distinct Jenis from koko_kolejpermata where Tahun = '" & ddlKokoYear.SelectedValue & "'"
        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKokoType.DataSource = ds
            ddlKokoType.DataTextField = "Jenis"
            ddlKokoType.DataValueField = "Jenis"
            ddlKokoType.DataBind()
            ddlKokoType.Items.Insert(0, New ListItem("Select Type", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_list_info()
        strSQL = "SELECT Distinct NamaBI from koko_kolejpermata where Jenis = '" & ddlKokoType.SelectedValue & "'and Tahun = '" & ddlKokoYear.SelectedValue & "' "
        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKokoName.DataSource = ds
            ddlKokoName.DataTextField = "NamaBI"
            ddlKokoName.DataValueField = "NamaBI"
            ddlKokoName.DataBind()
            ddlKokoName.Items.Insert(0, New ListItem("Select Name", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataCocurriculum(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLCocurriculum, strConnPermata)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConnPermata.Close()

        Catch ex As Exception

            Return False
        End Try
        Return True
    End Function

    Private Function getSQLCocurriculum() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " order by A.student_Name ASC"

        tmpSQL = "SELECT distinct A.std_ID , A.student_Name, A.student_Mykad, StudentProfile.AlumniID, koko_kelas.Kelas,
                  (SELECT NamaBI FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,
                  (SELECT NamaBI FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,
                  (SELECT NamaBI FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,
                  (SELECT NamaBI FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan
                  FROM koko_pelajar
                  LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                  LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                  LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                  LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"

        strWhere = " WHERE koko_pelajar.Tahun ='" & ddlKokoYear.SelectedValue & "' AND A.student_Status = 'Access' and A.student_ID is not null and A.student_ID <> '' and A.student_ID like '%M%'"

        strWhere += " and A.student_Campus = '" & ddlKokoCampus.SelectedValue & "'"
        If ddlKokoType.SelectedIndex > 0 Then
            If ddlKokoName.SelectedIndex > 0 Then
                strWhere += " AND koko_kolejpermata.Nama = '" & ddlKokoName.SelectedValue & "' "
            End If
        End If

        If ddlKokoProgram.SelectedIndex > 0 Then
            strWhere += " AND A.student_Stream = '" & ddlKokoProgram.SelectedValue & "'"
        End If

        getSQLCocurriculum = tmpSQL & strWhere & strOrder

        Return getSQLCocurriculum
    End Function

    Protected Sub ddlKokoProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKokoProgram.SelectedIndexChanged
        Try
            strRet = BindDataCocurriculum(datRespondentCocurriculum)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKokoCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKokoCampus.SelectedIndexChanged
        Try
            student_Program_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKokoYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKokoYear.SelectedIndexChanged
        Try
            koko_type_info()
            strRet = BindDataCocurriculum(datRespondentCocurriculum)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKokoType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKokoType.SelectedIndexChanged
        Try
            koko_list_info()
            strRet = BindDataCocurriculum(datRespondentCocurriculum)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKokoName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKokoName.SelectedIndexChanged
        Try
            strRet = BindDataCocurriculum(datRespondentCocurriculum)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub block_name_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Block_Name' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostelBlock.DataSource = ds
            ddlHostelBlock.DataTextField = "Parameter"
            ddlHostelBlock.DataValueField = "Value"
            ddlHostelBlock.DataBind()
            ddlHostelBlock.Items.Insert(0, New ListItem("Select Block", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_level_list()
        strSQL = "SELECT Parameter , Value FROM setting WHERE Type='Block_Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostelFloor.DataSource = ds
            ddlHostelFloor.DataTextField = "Parameter"
            ddlHostelFloor.DataValueField = "Value"
            ddlHostelFloor.DataBind()
            ddlHostelFloor.Items.Insert(0, New ListItem("Select Floor", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub hostel_sem_list()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostelSemester.DataSource = ds
            ddlHostelSemester.DataTextField = "Parameter"
            ddlHostelSemester.DataValueField = "Value"
            ddlHostelSemester.DataBind()
            ddlHostelSemester.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub hostel_year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostelYear.DataSource = ds
            ddlHostelYear.DataTextField = "Parameter"
            ddlHostelYear.DataValueField = "Parameter"
            ddlHostelYear.DataBind()
            ddlHostelYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataHostel(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLHostel, strConn)
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

    Private Function getSQLHostel() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY hostel_info.hostel_BlockNames asc, hostel_info.hostel_BlockLevels asc, room_info.room_Name asc, student_Name asc "

        tmpSQL = "  select distinct UPPER(student_info.student_Name) student_Name, student_info.student_ID, student_Level.student_Level, student_info.student_Campus, hostel_info.hostel_BlockNames,hostel_info.hostel_BlockLevels,room_info.room_Name,room_info.year,student_info.std_ID, student_room.sem
                    from student_info
                    left join student_room on student_info.std_ID = student_room.std_ID
                    left join student_Level on student_info.std_ID = student_Level.std_ID
                    left join room_info on student_room.room_ID = room_info.room_ID
                    left join hostel_info on room_info.hostel_ID = hostel_info.hostel_ID"

        strWhere = " where hostel_info.year = '" & ddlHostelYear.SelectedValue & "' and student_Level.year = '" & ddlHostelYear.SelectedValue & "' and student_info.student_status = 'Access' and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"

        strWhere += " and student_info.student_Campus <> 'APP'"

        If ddlHostelBlock.SelectedIndex > 0 Then
            strWhere += " and hostel_info.hostel_BlockNames = '" & ddlHostelBlock.SelectedValue & "'"
        End If

        If ddlHostelFloor.SelectedIndex > 0 Then
            strWhere += " and hostel_info.hostel_BlockLevels = '" & ddlHostelFloor.SelectedValue & "'"
        End If

        If ddlHostelSemester.SelectedIndex > 0 Then
            strWhere += " and student_room.sem = '" & ddlHostelSemester.SelectedValue & "'"
        End If

        getSQLHostel = tmpSQL & strWhere & strOrderby

        Return getSQLHostel
    End Function

    Protected Sub ddlHostelYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelYear.SelectedIndexChanged
        Try
            strRet = BindDataHostel(datRespondentHostel)
            block_name_list()
            block_level_list()
            hostel_sem_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlHostelBlock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelBlock.SelectedIndexChanged
        Try
            strRet = BindDataHostel(datRespondentHostel)
            block_level_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlHostelFloor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelFloor.SelectedIndexChanged
        Try
            strRet = BindDataHostel(datRespondentHostel)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlHostelSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelSemester.SelectedIndexChanged
        Try
            strRet = BindDataHostel(datRespondentHostel)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub religion_year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligionYear.DataSource = ds
            ddlReligionYear.DataTextField = "Parameter"
            ddlReligionYear.DataValueField = "Parameter"
            ddlReligionYear.DataBind()
            ddlReligionYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub religion_student_Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligionLevel.DataSource = ds
            ddlReligionLevel.DataTextField = "Parameter"
            ddlReligionLevel.DataValueField = "Parameter"
            ddlReligionLevel.DataBind()
            ddlReligionLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub religion_student_Religions()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Religion' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligionType.DataSource = ds
            ddlReligionType.DataTextField = "Parameter"
            ddlReligionType.DataValueField = "Parameter"
            ddlReligionType.DataBind()
            ddlReligionType.Items.Insert(0, New ListItem("Select Religion", String.Empty))

            ddlStudentReligion.DataSource = ds
            ddlStudentReligion.DataTextField = "Parameter"
            ddlStudentReligion.DataValueField = "Parameter"
            ddlStudentReligion.DataBind()
            ddlStudentReligion.Items.Insert(0, New ListItem("Select Religion", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub religion_student_class()

        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & ddlReligionYear.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlReligionLevel.SelectedValue & "' and course_Program = '" & ddlReligionProgram.SelectedValue & "' and class_Campus = '" & ddlReligionCampus.SelectedValue & "' order by class_Name ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            'ddlReligionClass.DataSource = ds
            'ddlReligionClass.DataTextField = "class_Name"
            'ddlReligionClass.DataValueField = "class_ID"
            'ddlReligionClass.DataBind()
            'ddlReligionClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlReligionYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReligionYear.SelectedIndexChanged
        Try
            religion_student_class()
            strRet = BindDataReligion(datRespondentReligion)
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ddlReligionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReligionType.SelectedIndexChanged
        Try
            strRet = BindDataReligion(datRespondentReligion)
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ddlReligionLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReligionLevel.SelectedIndexChanged
        Try
            religion_student_class()
            strRet = BindDataReligion(datRespondentReligion)
        Catch ex As Exception
        End Try
    End Sub

    'Protected Sub ddlReligionClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReligionClass.SelectedIndexChanged
    '    Try
    '        strRet = BindDataReligion(datRespondentReligion)
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Protected Sub ddlReligionProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReligionProgram.SelectedIndexChanged
        Try
            strRet = BindDataReligion(datRespondentReligion)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlReligionCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReligionCampus.SelectedIndexChanged
        Try
            student_Program_list()
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataReligion(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLReligion, strConn)
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

    Private Function getSQLReligion() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "  select distinct student_info.std_ID, student_info.student_Name, student_info.student_Mykad, student_info.student_ID, student_level.student_Level, student_Level.year, student_info.student_Religion, student_info.student_Campus, class_info.class_Name from student_info
                    left join student_level on student_info.std_ID = student_level.std_ID
                    left join course on course.std_ID = student_info.std_ID
                    left join class_info on class_info.class_ID = course.class_ID"

        strWhere = " WHERE student_info.student_Status = 'Access' and class_info.class_type = 'Compulsory' and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"

        strWhere += " AND student_level.year = '" & ddlReligionYear.SelectedValue & "' and course.year = '" & ddlReligionYear.SelectedValue & "' and class_info.class_year = '" & ddlReligionYear.SelectedValue & "'"

        strWhere += " and student_info.student_Campus = '" & ddlReligionCampus.SelectedValue & "' and class_info.class_Campus = '" & ddlReligionCampus.SelectedValue & "'"

        If ddlReligionLevel.SelectedIndex > 0 Then
            strWhere += " AND student_level.student_Level = '" & ddlReligionLevel.SelectedValue & "' and class_info.class_level = '" & ddlReligionLevel.SelectedValue & "'"
        End If

        If ddlReligionType.SelectedIndex > 0 Then
            strWhere += " AND student_info.student_Religion = '" & ddlReligionType.SelectedValue & "'"
        End If

        'If ddlReligionClass.SelectedIndex > 0 Then
        '    strWhere += " AND class_info.class_ID = '" & ddlReligionClass.SelectedValue & "'"
        'End If

        If ddlReligionProgram.SelectedIndex > 0 Then
            strWhere += " AND student_info.student_Stream = '" & ddlReligionProgram.SelectedValue & "'"
        End If

        getSQLReligion = tmpSQL & strWhere & strOrderby

        Return getSQLReligion

    End Function

    Private Sub btnUpdateReligion_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateReligion.ServerClick

        For i As Integer = 0 To datRespondentReligion.Rows.Count - 1

            Dim chkUpdate As CheckBox = CType(datRespondentReligion.Rows(i).Cells(5).FindControl("chkSelectReligion"), CheckBox)
            If Not chkUpdate Is Nothing Then

                Dim strKey As String = datRespondentReligion.DataKeys(i).Value.ToString

                If chkUpdate.Checked = True Then

                    strSQL = "UPDATE student_info SET student_Religion='" & ddlStudentReligion.SelectedValue & "' WHERE std_ID ='" & strKey & "' and student_info.student_status = 'Access'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            End If

        Next

        If strRet = "0" Then
            ShowMessage(" Update Student Religion ", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Update Student Religion ", MessageType.Error)
        End If

        strRet = BindDataReligion(datRespondentReligion)

    End Sub

    Private Sub SIP_CampusList()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSIP_Campus.DataSource = ds
            ddlSIP_Campus.DataTextField = "Parameter"
            ddlSIP_Campus.DataValueField = "Value"
            ddlSIP_Campus.DataBind()
            ddlSIP_Campus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SIP_ProgramList()

        If ddlSIP_Campus.SelectedValue = "APP" Then
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

            ddlSIP_Program.DataSource = ds
            ddlSIP_Program.DataTextField = "Parameter"
            ddlSIP_Program.DataValueField = "Value"
            ddlSIP_Program.DataBind()
            ddlSIP_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SIP_ClassList()

        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & ddlSIP_Year.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlSIP_Level.SelectedValue & "' and course_Program = '" & ddlSIP_Program.SelectedValue & "' and class_Campus = '" & ddlSIP_Campus.SelectedValue & "' order by class_Name ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSIP_Class.DataSource = ds
            ddlSIP_Class.DataTextField = "class_Name"
            ddlSIP_Class.DataValueField = "class_ID"
            ddlSIP_Class.DataBind()
            ddlSIP_Class.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SIP_LevelList()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSIP_Level.DataSource = ds
            ddlSIP_Level.DataTextField = "Parameter"
            ddlSIP_Level.DataValueField = "Parameter"
            ddlSIP_Level.DataBind()
            ddlSIP_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SIP_SemList()
        strSQL = "SELECT * FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSIP_Semester.DataSource = ds
            ddlSIP_Semester.DataTextField = "Parameter"
            ddlSIP_Semester.DataValueField = "Value"
            ddlSIP_Semester.DataBind()
            ddlSIP_Semester.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SIP_YearList()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSIP_Year.DataSource = ds
            ddlSIP_Year.DataTextField = "Parameter"
            ddlSIP_Year.DataValueField = "Parameter"
            ddlSIP_Year.DataBind()
            ddlSIP_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SIP_RegisteredList()
        strSQL = "SELECT Parameter FROM setting WHERE Type='99999999' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSip_Registered.DataSource = ds
            ddlSip_Registered.DataTextField = "Parameter"
            ddlSip_Registered.DataValueField = "Parameter"
            ddlSip_Registered.DataBind()
            ddlSip_Registered.Items.Insert(0, New ListItem("Select Registered", String.Empty))
            ddlSip_Registered.Items.Insert(1, New ListItem("Yes", "Yes"))
            ddlSip_Registered.Items.Insert(2, New ListItem("No", "No"))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataSIP(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL_SIP, strConn)
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

    Private Function getSQL_SIP() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " Order by A.student_Name ASC, A.student_ID ASC, C.class_Level ASC, C.Class_Name ASC"

        tmpSQL = "  Select distinct A.std_ID, A.student_Name, A.student_ID, A.student_Stream, C.class_Level, C.Class_Name, D.Registered, A.student_Campus from student_info A
                    Left Join course B on A.std_ID = B.std_ID
                    Left Join class_info C on B.class_ID = C.class_ID
                    Left Join Student_Level D on A.std_ID = D.std_ID"


        If ddlSIP_Program.SelectedValue = "Temp" Then

            strWhere += "   Where A.student_Status = 'Access' And A.student_Stream = 'Temp' and C.class_type = 'Compulsory'
                            And B.year = '" & ddlSIP_Year.SelectedValue & "' and C.class_year = '" & ddlSIP_Year.SelectedValue & "' and D.year = '" & ddlSIP_Year.SelectedValue & "'"

            strWhere += " and A.student_Campus = '" & ddlSIP_Campus.SelectedValue & "' and C.class_Campus = '" & ddlSIP_Campus.SelectedValue & "'"

            If ddlSIP_Level.SelectedIndex > 0 Then
                strWhere += " And C.class_Level = '" & ddlSIP_Level.SelectedValue & "'"
            End If

            If ddlSIP_Class.SelectedIndex > 0 Then
                strWhere += " And C.class_ID = '" & ddlSIP_Class.SelectedValue & "'"
            End If

            If ddlSip_Registered.SelectedIndex > 0 Then
                strWhere += " And D.Registered = '" & ddlSip_Registered.SelectedValue & "'"
            End If

        Else
            strWhere += "   Where A.student_Status = 'Access' And A.student_Stream = '" & ddlSIP_Program.SelectedValue & "'  And A.student_ID is not null And A.student_ID <> '' And A.student_ID like '%M%' 
                            And B.year = '" & ddlSIP_Year.SelectedValue & "' and C.class_year = '" & ddlSIP_Year.SelectedValue & "' and D.year = '" & ddlSIP_Year.SelectedValue & "' and C.class_type = 'Compulsory'"

            strWhere += " and A.student_Campus = '" & ddlSIP_Campus.SelectedValue & "' and C.class_Campus = '" & ddlSIP_Campus.SelectedValue & "'"

            If ddlSIP_Level.SelectedIndex > 0 Then
                strWhere += " And C.class_Level = '" & ddlSIP_Level.SelectedValue & "'"
            End If

            If ddlSIP_Class.SelectedIndex > 0 Then
                strWhere += " And C.class_ID = '" & ddlSIP_Class.SelectedValue & "'"
            End If

            If ddlSip_Registered.SelectedIndex > 0 Then
                strWhere += " And D.Registered = '" & ddlSip_Registered.SelectedValue & "'"
            End If

        End If

        getSQL_SIP = tmpSQL & strWhere & strOrderby

        Return getSQL_SIP
    End Function

    Protected Sub ddlSIP_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSIP_Year.SelectedIndexChanged
        Try
            SIP_ClassList()
            strRet = BindDataSIP(datRespondent_SIP)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSIP_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSIP_Campus.SelectedIndexChanged
        Try
            SIP_ProgramList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSIP_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSIP_Program.SelectedIndexChanged
        Try
            SIP_ClassList()
            strRet = BindDataSIP(datRespondent_SIP)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSIP_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSIP_Level.SelectedIndexChanged
        Try
            SIP_ClassList()
            strRet = BindDataSIP(datRespondent_SIP)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSIP_Semester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSIP_Semester.SelectedIndexChanged
        Try
            strRet = BindDataSIP(datRespondent_SIP)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSIP_Class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSIP_Class.SelectedIndexChanged
        Try
            strRet = BindDataSIP(datRespondent_SIP)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSip_Registered_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSip_Registered.SelectedIndexChanged
        Try
            strRet = BindDataSIP(datRespondent_SIP)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdateStudentIDProgram_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStudentIDProgram.ServerClick

        Dim i As Integer
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strRet = "1"

        If ddlSIP_Year.SelectedValue = Now.Year Then
            For i = 0 To datRespondent_SIP.Rows.Count - 1 Step i + 1

                Dim chkUpdate As CheckBox = CType(datRespondent_SIP.Rows(i).Cells(5).FindControl("chkSelect_SIP"), CheckBox)
                If Not chkUpdate Is Nothing Then

                    ' Get the values of textboxes using findControl
                    Dim txtStudentID As TextBox = datRespondent_SIP.Rows(i).FindControl("student_ID")
                    Dim txtClassName As TextBox = datRespondent_SIP.Rows(i).FindControl("class_Name")
                    Dim ddlSIP_SIP_Update As DropDownList = datRespondent_SIP.Rows(i).FindControl("ddlSIP_Update_Program")
                    Dim strKey As String = datRespondent_SIP.DataKeys(i).Value.ToString

                    If chkUpdate.Checked = True Then
                        If ddlSIP_Year.SelectedIndex > 0 Then
                            If ddlSIP_Level.SelectedIndex > 0 Then
                                If ddlSIP_Semester.SelectedIndex > 0 Then

                                    ''Update Table Student Info
                                    strSQL = "Update student_info set student_Stream = '" & ddlSIP_SIP_Update.SelectedValue & "', student_ID = '" & txtStudentID.Text & "' where std_ID = '" & strKey & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)

                                    ''Select Class ID From Table Class Info
                                    strSQL = "Select class_ID from class_info where class_year = '" & ddlSIP_Year.SelectedValue & "' and class_level = '" & ddlSIP_Level.SelectedValue & "' and class_Name = '" & txtClassName.Text & "' and class_type = 'Compulsory' and course_Program = '" & ddlSIP_SIP_Update.SelectedValue & "' and class_Campus = '" & Session("SchoolCampus") & "' "
                                    Dim get_ClassID As String = oCommon.getFieldValue(strSQL)

                                    If get_ClassID.Length = 0 Then
                                        ShowMessage(" Please Enter Valid Class Name ", MessageType.Error)
                                        Exit For
                                    End If

                                    ''Select Current Subject ID from Table Course
                                    Dim get_SubjectID As String = " select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID 
                                                                    where course.year = '" & ddlSIP_Year.SelectedValue & "' and subject_info.subject_year = '" & ddlSIP_Year.SelectedValue & "' and subject_info.subject_StudentYear = '" & ddlSIP_Level.SelectedValue & "'
                                                                    and subject_info.subject_sem = '" & ddlSIP_Semester.SelectedValue & "' and subject_info.course_Program = '" & ddlSIP_Program.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                                    strRet = oCommon.getFieldValue(get_SubjectID)

                                    If strRet.Length > 0 Then
                                        ''Delete Existing Data From Table Course
                                        strSQL = "  Delete from course where course_ID IN (
                                                Select course.course_ID from course left join class_info on course.class_ID = class_info.class_ID left join subject_info on course.subject_ID = subject_info.subject_ID
                                                where course.year = '" & ddlSIP_Year.SelectedValue & "' and class_info.class_year = '" & ddlSIP_Year.SelectedValue & "' and class_info.class_level = '" & ddlSIP_Level.SelectedValue & "' and class_info.course_Program = '" & ddlSIP_SIP_Update.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "' 
                                                and subject_info.subject_year = '" & ddlSIP_Year.SelectedValue & "' and subject_info.subject_StudentYear = '" & ddlSIP_Level.SelectedValue & "' and subject_info.subject_sem = '" & ddlSIP_Semester.SelectedValue & "' and subject_info.course_Program = '" & ddlSIP_SIP_Update.SelectedValue & "' and course.std_ID = '" & strKey & "')"
                                        strRet = oCommon.ExecuteSQL(strSQL)
                                    Else
                                        ''Delete Existing Data From Table Course
                                        strSQL = "  Delete from course where course_ID IN (
                                                Select course.course_ID from course left join class_info on course.class_ID = class_info.class_ID
                                                where course.year = '" & ddlSIP_Year.SelectedValue & "' and class_info.class_year = '" & ddlSIP_Year.SelectedValue & "' and class_info.class_level = '" & ddlSIP_Level.SelectedValue & "' and class_info.course_Program = '" & ddlSIP_SIP_Update.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "' and course.std_ID = '" & strKey & "')"
                                        strRet = oCommon.ExecuteSQL(strSQL)
                                    End If

                                    ''Insert Class And Subject To Table Course
                                    strSQL = "  Insert into course(std_ID,class_ID,subject_ID,year)
                                            Select '" & strKey & "','" & get_ClassID & "',subject_info.subject_ID,'" & ddlSIP_Year.SelectedValue & "' from subject_info
                                            where subject_info.subject_year = '" & ddlSIP_Year.SelectedValue & "' and subject_info.subject_StudentYear = '" & ddlSIP_Level.SelectedValue & "' and subject_info.subject_sem = '" & ddlSIP_Semester.SelectedValue & "' and subbjject_info.subject_Campus = '" & ddlSIP_Campus.SelectedValue & "'
                                            and subject_info.subject_type = 'Compulsory' and subject_info.course_Program = '" & ddlSIP_SIP_Update.SelectedValue & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)


                                    '''''''''''''''''''''''''''''''''' KOKURIKULUM DATABASE '''''''''''''''''''''''''''''''''''''

                                    ''Get Student Mykad From Table Student Info
                                    strSQL = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                    Dim get_StdMYKAD As String = oCommon.getFieldValue(strSQL)

                                    ''Get Student ID From Table Student Profile In Permatapintar Database
                                    strSQL = "Select StudentID from StudentProfile where MYKAD = '" & get_StdMYKAD & "'"
                                    Dim get_StdID As String = oCommon.getFieldValue_Permata(strSQL)

                                    ''Check If Student Data Is Existed In Koko Pelajar Database
                                    strSQL = "Select kokopelajarid from koko_pelajar where StudentID = '" & get_StdID & "' and Tahun = '" & ddlSIP_Year.SelectedValue & "'"
                                    strRet = oCommon.getFieldValue_Permata(strSQL)

                                    ''Get Student PPCS Date
                                    Dim find_PPCSDate As String = "select MAX(PPCSDate) from PPCS where StudentID = '" & get_StdID & "'"
                                    Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                                    ''Get Kelas Name From Koko Database
                                    Dim find_kokoKelas As String = "select KelasID from koko_kelas where Kelas = '" & txtClassName.Text & "' and Tahun = '" & ddlSIP_Year.SelectedValue & "'"
                                    Dim get_kokoKelas As String = oCommon.getFieldValue_Permata(find_kokoKelas)

                                    Dim level As String = ""
                                    If ddlSIP_Level.SelectedValue = "Foundation 1" Or ddlSIP_Level.SelectedValue = "Foundation 2" Or ddlSIP_Level.SelectedValue = "Foundation 3" Then
                                        level = "ASAS 1"
                                    ElseIf ddlSIP_Level.SelectedValue = "Level 1" Or ddlSIP_Level.SelectedValue = "Level 2" Then
                                        level = "TAHAP 1"
                                    End If

                                    If strRet <> "" Then
                                        ''Update Student Data In Koko Pelajar Database
                                        strSQL = "Update koko_pelajar set PPCSDate = '" & get_PPCSDate & "', Tahun = '" & ddlSIP_Year.SelectedValue & "', Program = '" & level & "', Disahkan = 'N', KelasID = '" & get_kokoKelas & "' where StudentID = '" & get_StdID & "'"
                                        strRet = oCommon.ExecuteSQLPermata(strSQL)
                                    Else
                                        ''Insert Student Data In Koko Pelajar Database
                                        strSQL = "Insert into koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan, KelasID) values('" & get_StdID & "','" & get_PPCSDate & "','" & ddlSIP_Year.SelectedValue & "','" & level & "','N','" & get_kokoKelas & "')"
                                        strRet = oCommon.ExecuteSQLPermata(strSQL)
                                    End If

                                Else
                                    ShowMessage(" Please Select Semester ", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Please Select Level ", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Please Select Year ", MessageType.Error)
                        End If
                    End If
                End If
            Next
        End If

        If strRet = "0" Then
            ShowMessage(" Update Student ID And Program ", MessageType.Success)
            strRet = BindDataSIP(datRespondent_SIP)
        ElseIf strRet = "1" Then
            ShowMessage(" Unable To Edit Or Delete Previous Year Data ", MessageType.Error)
            strRet = BindDataSIP(datRespondent_SIP)
        Else
            ShowMessage(" Unsuccessful Update Student ID And Program ", MessageType.Error)
            strRet = BindDataSIP(datRespondent_SIP)
        End If

    End Sub

    Private Sub datRespondent_SIP_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent_SIP.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try

            If ddlYear.SelectedValue <> Now.Year Then
                ShowMessage("Unable delete previous data", MessageType.Error)
            Else

                strSQL = "Update student_info set student_Status = 'Block' where std_ID = '" & strKeyName & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    ShowMessage(" Delete data", MessageType.Success)
                Else
                    ShowMessage(" Unsuccessful Delete data", MessageType.Error)
                End If

                strRet = BindData(datRespondent)
            End If
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