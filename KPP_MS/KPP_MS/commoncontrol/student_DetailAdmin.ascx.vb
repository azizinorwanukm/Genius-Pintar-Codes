Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography

Public Class student_DetailAdmin
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim stfID As String = ""
    Dim adminID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                txtbreadcrum2.Text = "Student &nbsp; / &nbsp; Search Student "
                previousPage.NavigateUrl = String.Format("~/admin_carian_pelajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))
                txtstudentID.Enabled = True

                Session("getEditButton_CI") = "FALSE"
                Session("getDeleteButton_CI") = "FALSE"
                Session("getDeleteButton_RI") = "FALSE"
                Session("getViewButton_RI") = "FALSE"
                Session("getDownloadButton_RI") = "FALSE"

                If Session("SchoolCampus") = "PGPN" Then
                    btnHostelInfo.Visible = True
                    btnDiscInfo.Visible = True
                    btnRefInfo.Visible = True
                    btnExamHistoryInfo.Visible = True
                ElseIf Session("SchoolCampus") = "APP" Then
                    btnHostelInfo.Visible = False
                    btnDiscInfo.Visible = False
                    btnRefInfo.Visible = False
                    btnExamHistoryInfo.Visible = False
                End If

                Checking_MenuAccess_Load()

                If Session("getStatus") = "SI" Then ''student information
                    txtbreadcrum1.Text = "Student Information"

                    StudentInformation.Visible = True
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False
                    ExaminationHistoryInformation.Visible = False
                    ReferenceInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-info"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-default font"
                    btnRefInfo.Attributes("class") = "btn btn-default font"

                    Race_List()
                    Religion_List()
                    State_list()
                    Country_list()
                    Stream_List()
                    Campus_List()

                    LoadPageStudent()

                ElseIf Session("getStatus") = "FI" Then ''Family information
                    txtbreadcrum1.Text = "Family  Information"

                    StudentInformation.Visible = False
                    ParentInformation.Visible = True
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False
                    ExaminationHistoryInformation.Visible = False
                    ReferenceInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-info"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-default font"
                    btnRefInfo.Attributes("class") = "btn btn-default font"

                    LoadPageParent1()
                    LoadPageParent2()

                ElseIf Session("getStatus") = "CI" Then ''Course information
                    txtbreadcrum1.Text = "Course Information"

                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = True
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False
                    ExaminationHistoryInformation.Visible = False
                    ReferenceInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-info"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-default font"
                    btnRefInfo.Attributes("class") = "btn btn-default font"

                    Year_List()
                    Semester_List()
                    LoadCurrentYear()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "COI" Then ''Cocurricular information
                    txtbreadcrum1.Text = "Cocurricular Information"

                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = True
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False
                    ExaminationHistoryInformation.Visible = False
                    ReferenceInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-info"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-default font"
                    btnRefInfo.Attributes("class") = "btn btn-default font"

                    strRet = BindDataKOKO(datRespondentKOKO)

                ElseIf Session("getStatus") = "EI" Then ''Examination information
                    txtbreadcrum1.Text = "Examination Information"

                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = True
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False
                    ExaminationHistoryInformation.Visible = False
                    ReferenceInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-info"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-default font"
                    btnRefInfo.Attributes("class") = "btn btn-default font"

                    Year_Examination()
                    Exam_Examination()

                    strRet = BindDataExam(datRespondentExam)

                ElseIf Session("getStatus") = "HI" Then ''Hostel information
                    txtbreadcrum1.Text = "Hostel Information"

                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = True
                    DisciplineInformation.Visible = False
                    ExaminationHistoryInformation.Visible = False
                    ReferenceInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-info"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-default font"
                    btnRefInfo.Attributes("class") = "btn btn-default font"

                    strRet = BindDataHostel(datRespondentHostel)

                ElseIf Session("getStatus") = "DI" Then ''Discipline information
                    txtbreadcrum1.Text = "Discipline Information"

                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = True
                    ExaminationHistoryInformation.Visible = False
                    ReferenceInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-info"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-default font"
                    btnRefInfo.Attributes("class") = "btn btn-default font"

                    strRet = BindDataDisicpline(datRespondent_Discipline)

                ElseIf Session("getStatus") = "EHI" Then ''Examination History Information
                    txtbreadcrum1.Text = "Examination History Information"

                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False
                    ExaminationHistoryInformation.Visible = True
                    ReferenceInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-info"
                    btnRefInfo.Attributes("class") = "btn btn-default font"

                    strRet = BindDataUKM1(datRespondentUKM1)
                    strRet = BindDataUKM2(datRespondentUKM2)
                    strRet = BindDataUKM3(datRespondentUKM3)
                    strRet = BindDataPPCS(datRespondentPPCS)

                ElseIf Session("getStatus") = "RI" Then ''Reference Information

                    txtbreadcrum1.Text = "Reference Information"
                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False
                    ExaminationHistoryInformation.Visible = False
                    ReferenceInformation.Visible = True

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"
                    btnExamHistoryInfo.Attributes("class") = "btn btn-default font"
                    btnRefInfo.Attributes("class") = "btn btn-info"

                    Year_Info_list()
                    strRet = BindDataReference(datRespondentReference)
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnStudentInfo.Visible = False
        btnParentInfo.Visible = False
        btnCourseInfo.Visible = False
        btnCocurInfo.Visible = False
        btnExamInfo.Visible = False
        btnHostelInfo.Visible = False
        btnDiscInfo.Visible = False
        btnExamHistoryInfo.Visible = False
        btnRefInfo.Visible = False

        StudentInformation.Visible = False
        ParentInformation.Visible = False
        CourseInformation.Visible = False
        CocurricularInformation.Visible = False
        ExaminationInformation.Visible = False
        HostelInformation.Visible = False
        DisciplineInformation.Visible = False
        ExaminationHistoryInformation.Visible = False
        ReferenceInformation.Visible = False

        btnUpdateStudentInfo.Visible = False
        btnUpdateParentInfo.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_StudentInformation As String = ""
        Dim Get_ParentInformation As String = ""
        Dim Get_CourseInformation As String = ""
        Dim Get_CocurricularInformation As String = ""
        Dim Get_ExaminationInformation As String = ""
        Dim Get_HostelInformation As String = ""
        Dim Get_DisciplineInformation As String = ""
        Dim Get_ExaminationHistoryInformation As String = ""
        Dim Get_ReferenceInformation As String = ""

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

            ''Get Function Button 2 Edit Data 
            strSQL = "  Select B.F2_Edit From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Edit As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Delete Data 
            strSQL = "  Select B.F2_Delete From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Delete As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Update Data 
            strSQL = "  Select B.F2_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Update As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Download Data 
            strSQL = "  Select B.F2_Download From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Download As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 View Data 
            strSQL = "  Select B.F2_View From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2View As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu3 = "Student Information" And find_Data_SubMenu3.Length > 0 Then
                btnStudentInfo.Visible = True
                StudentInformation.Visible = True

                Get_StudentInformation = "TRUE"
                btnUpdateStudentInfo.Visible = True
            End If

            If find_Data_SubMenu3 = "Family Information" And find_Data_SubMenu3.Length > 0 Then
                btnParentInfo.Visible = True
                ParentInformation.Visible = True

                Get_ParentInformation = "TRUE"
                btnUpdateParentInfo.Visible = True
            End If

            If find_Data_SubMenu3 = "Course Information" And find_Data_SubMenu3.Length > 0 Then
                btnCourseInfo.Visible = True
                CourseInformation.Visible = True

                Get_CourseInformation = "TRUE"

                If find_Data_F2Edit.Length > 0 And find_Data_F2Edit = "TRUE" Then
                    Session("getEditButton_CI") = "TRUE"
                End If

                If find_Data_F2Delete.Length > 0 And find_Data_F2Delete = "TRUE" Then
                    Session("getDeleteButton_CI") = "TRUE"
                End If
            End If

            If find_Data_SubMenu3 = "Cocurricular Information" And find_Data_SubMenu3.Length > 0 Then
                btnCocurInfo.Visible = True
                CocurricularInformation.Visible = True

                Get_CocurricularInformation = "TRUE"
            End If

            If find_Data_SubMenu3 = "Examination Information" And find_Data_SubMenu3.Length > 0 Then
                btnExamInfo.Visible = True
                ExaminationInformation.Visible = True

                Get_ExaminationInformation = "TRUE"
            End If

            If find_Data_SubMenu3 = "Hostel Information" And find_Data_SubMenu3.Length > 0 Then
                btnHostelInfo.Visible = True
                HostelInformation.Visible = True

                Get_HostelInformation = "TRUE"
            End If

            If find_Data_SubMenu3 = "Discipline Information" And find_Data_SubMenu3.Length > 0 Then
                btnDiscInfo.Visible = True
                DisciplineInformation.Visible = True

                Get_DisciplineInformation = "TRUE"
            End If

            If find_Data_SubMenu3 = "UKM1 - PPCS" And find_Data_SubMenu3.Length > 0 Then
                btnExamHistoryInfo.Visible = True
                ExaminationHistoryInformation.Visible = True

                Get_ExaminationHistoryInformation = "TRUE"
            End If

            If find_Data_SubMenu3 = "Reference Information" And find_Data_SubMenu3.Length > 0 Then
                btnRefInfo.Visible = True
                ReferenceInformation.Visible = True

                Get_ReferenceInformation = "TRUE"

                If find_Data_F2View.Length > 0 And find_Data_F2View = "TRUE" Then
                    Session("getViewButton_RI") = "TRUE"
                End If

                If find_Data_F2Delete.Length > 0 And find_Data_F2Delete = "TRUE" Then
                    Session("getDeleteButton_RI") = "TRUE"
                End If

                If find_Data_F2Download.Length > 0 And find_Data_F2Download = "TRUE" Then
                    Session("getDownloadButton_RI") = "TRUE"
                End If
            End If

            If find_Data_SubMenu3.Length = 0 And find_Data_Menu_Data = "All" Then
                btnStudentInfo.Visible = True
                btnParentInfo.Visible = True
                btnCourseInfo.Visible = True
                btnCocurInfo.Visible = True
                btnExamInfo.Visible = True
                btnHostelInfo.Visible = True
                btnDiscInfo.Visible = True
                btnExamHistoryInfo.Visible = True
                btnRefInfo.Visible = True

                StudentInformation.Visible = True
                ParentInformation.Visible = True
                CourseInformation.Visible = True
                CocurricularInformation.Visible = True
                ExaminationInformation.Visible = True
                HostelInformation.Visible = True
                DisciplineInformation.Visible = True
                ExaminationHistoryInformation.Visible = True
                ReferenceInformation.Visible = True

                btnUpdateStudentInfo.Visible = True
                btnUpdateParentInfo.Visible = True

                Get_StudentInformation = "TRUE"
                Session("getEditButton_CI") = "TRUE"
                Session("getDeleteButton_CI") = "TRUE"
                Session("getDeleteButton_RI") = "TRUE"
                Session("getViewButton_RI") = "TRUE"
                Session("getDownloadButton_RI") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "SI" Or Session("getStatus") = "FI" Or Session("getStatus") = "CI" Or Session("getStatus") = "COI" Or Session("getStatus") = "EI" Or Session("getStatus") = "HI" Or Session("getStatus") = "DI" Or Session("getStatus") = "EHI" Or Session("getStatus") = "RI" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "SI" And Session("getStatus") <> "FI" And Session("getStatus") <> "CI" And Session("getStatus") <> "COI" And Session("getStatus") <> "EI" And Session("getStatus") <> "HI" And Session("getStatus") <> "DI" And Session("getStatus") <> "EHI" And Session("getStatus") <> "RI" Then
            If Get_StudentInformation = "TRUE" Then
                Data_If_Not_Group_Status = "SI"
            ElseIf Get_ParentInformation = "TRUE" Then
                Data_If_Not_Group_Status = "FI"
            ElseIf Get_CourseInformation = "TRUE" Then
                Data_If_Not_Group_Status = "CI"
            ElseIf Get_CocurricularInformation = "TRUE" Then
                Data_If_Not_Group_Status = "COI"
            ElseIf Get_ExaminationInformation = "TRUE" Then
                Data_If_Not_Group_Status = "EI"
            ElseIf Get_HostelInformation = "TRUE" Then
                Data_If_Not_Group_Status = "HI"
            ElseIf Get_DisciplineInformation = "TRUE" Then
                Data_If_Not_Group_Status = "DI"
            ElseIf Get_ExaminationHistoryInformation = "TRUE" Then
                Data_If_Not_Group_Status = "EHI"
            ElseIf Get_ReferenceInformation = "TRUE" Then
                Data_If_Not_Group_Status = "RI"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_StudentInformation = "TRUE" And Data_If_Not_Group_Status <> "SI" Then
                Data_If_Not_Group_Status = "SI"
            ElseIf Get_ParentInformation = "TRUE" And Data_If_Not_Group_Status <> "FI" Then
                Data_If_Not_Group_Status = "FI"
            ElseIf Get_CourseInformation = "TRUE" And Data_If_Not_Group_Status <> "CI" Then
                Data_If_Not_Group_Status = "CI"
            ElseIf Get_CocurricularInformation = "TRUE" And Data_If_Not_Group_Status <> "COI" Then
                Data_If_Not_Group_Status = "COI"
            ElseIf Get_ExaminationInformation = "TRUE" And Data_If_Not_Group_Status <> "EI" Then
                Data_If_Not_Group_Status = "EI"
            ElseIf Get_HostelInformation = "TRUE" And Data_If_Not_Group_Status <> "HI" Then
                Data_If_Not_Group_Status = "HI"
            ElseIf Get_DisciplineInformation = "TRUE" And Data_If_Not_Group_Status <> "DI" Then
                Data_If_Not_Group_Status = "DI"
            ElseIf Get_ExaminationHistoryInformation = "TRUE" And Data_If_Not_Group_Status <> "EHI" Then
                Data_If_Not_Group_Status = "EHI"
            ElseIf Get_ReferenceInformation = "TRUE" And Data_If_Not_Group_Status <> "RI" Then
                Data_If_Not_Group_Status = "RI"
            End If
        End If

    End Sub

    Private Sub btnStudentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnStudentInfo.ServerClick
        Session("getStatus") = "SI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnParentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnParentInfo.ServerClick
        Session("getStatus") = "FI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnCourseInfo_ServerClick(sender As Object, e As EventArgs) Handles btnCourseInfo.ServerClick
        Session("getStatus") = "CI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnCocurInfo_ServerClick(sender As Object, e As EventArgs) Handles btnCocurInfo.ServerClick
        Session("getStatus") = "COI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnExamInfo_ServerClick(sender As Object, e As EventArgs) Handles btnExamInfo.ServerClick
        Session("getStatus") = "EI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnHostelInfo_ServerClick(sender As Object, e As EventArgs) Handles btnHostelInfo.ServerClick
        Session("getStatus") = "HI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnDiscInfo_ServerClick(sender As Object, e As EventArgs) Handles btnDiscInfo.ServerClick
        Session("getStatus") = "DI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnExamHistoryInfo_ServerClick(sender As Object, e As EventArgs) Handles btnExamHistoryInfo.ServerClick
        Session("getStatus") = "EHI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnRefInfo_ServerClick(sender As Object, e As EventArgs) Handles btnRefInfo.ServerClick
        Session("getStatus") = "RI"
        Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub Race_List()
        strSQL = "SELECT UPPER(Parameter) Parameter, Value from setting where Type = 'Race' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRace.DataSource = ds
            ddlRace.DataTextField = "Parameter"
            ddlRace.DataValueField = "Value"
            ddlRace.DataBind()
            ddlRace.Items.Insert(0, New ListItem("Select Race", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Religion_List()
        strSQL = "SELECT UPPER(Parameter) Parameter, Value from setting where Type = 'Religion' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligion.DataSource = ds
            ddlReligion.DataTextField = "Parameter"
            ddlReligion.DataValueField = "Value"
            ddlReligion.DataBind()
            ddlReligion.Items.Insert(0, New ListItem("Select Religion", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub State_list()
        strSQL = "SELECT UPPER(Parameter) Parameter, Value FROM setting WHERE Type='State' order by Parameter ASC "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "Parameter"
            ddlState.DataValueField = "Value"
            ddlState.DataBind()
            ddlState.Items.Insert(0, New ListItem("Select State", String.Empty))

            ddlStateOfBirth.DataSource = ds
            ddlStateOfBirth.DataTextField = "Parameter"
            ddlStateOfBirth.DataValueField = "Value"
            ddlStateOfBirth.DataBind()
            ddlStateOfBirth.Items.Insert(0, New ListItem("Select State", String.Empty))
            ddlStateOfBirth.Items.Insert(1, New ListItem("OTHERS", "OTHERS"))


        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Country_list()

        strSQL = ""

        If ddlStateOfBirth.SelectedValue = "OTHERS" Then
            strSQL = "SELECT UPPER(Parameter) Parameter, Value FROM setting WHERE Type='Country' and idx = 'Address'  order by Parameter asc"
        Else
            strSQL = "SELECT UPPER(Parameter) Parameter, Value FROM setting WHERE Type='Country' and idx = 'Address' and Value = 'Malaysia' order by Parameter asc "
        End If


        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCountryOfBirth.DataSource = ds
            ddlCountryOfBirth.DataTextField = "Parameter"
            ddlCountryOfBirth.DataValueField = "Value"
            ddlCountryOfBirth.DataBind()
            ddlCountryOfBirth.Items.Insert(0, New ListItem("Select Country", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Stream_List()
        strSQL = "SELECT UPPER(Parameter) Parameter, Value FROM setting WHERE Type='Stream' "
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

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Campus_List()
        strSQL = "select UPPER(Parameter) Parameter, Value from setting where Type = 'Pusat Campus' order by Parameter ASC"
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
            ddlCampus.Items.Insert(0, New ListItem("Select Campus", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlStateOfBirth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStateOfBirth.SelectedIndexChanged
        Try

            If ddlStateOfBirth.SelectedValue = "Others" Then
                Country_list()
            Else
                ddlCountryOfBirth.SelectedValue = "Malaysia"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadPageStudent()

        Dim data_ID As String = Request.QueryString("std_ID")

        ''student_info
        strSQL = "  SELECT UPPER(student_Name) student_Name, UPPER(Student_Email) student_Email, UPPER(student_Sex) student_Sex, UPPER(student_Address) student_Address, UPPER(student_City) student_City, UPPER(student_Level) student_Level, UPPER(student_Sem) student_Sem,
                    student_info.student_ID, student_Mykad, student_FonNo, student_Level.year, student_PostalCode, student_Photo, student_Race, student_State, student_StateOfBirth, student_Stream, student_Campus, student_Religion, student_CountryOfBirth FROM student_info 
                    LEFT JOIN student_Level ON student_info.std_ID=student_Level.std_ID 
                    WHERE student_info.std_ID ='" & data_ID & "'
                    AND student_Level.ID = (SELECT MAX(ID) FROM student_Level C where C.std_ID ='" & data_ID & "')"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                txtstudentName.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                txtstudentName.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                txtstudentID.Text = ds.Tables(0).Rows(0).Item("student_ID")
            Else
                txtstudentID.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Mykad")) Then
                txtstudentMykad.Text = ds.Tables(0).Rows(0).Item("student_Mykad")
            Else
                txtstudentMykad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Email")) Then
                txtstudentEmail.Text = ds.Tables(0).Rows(0).Item("student_Email")
            Else
                txtstudentEmail.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sex")) Then
                txtstudentGender.Text = ds.Tables(0).Rows(0).Item("student_Sex")
            Else
                txtstudentGender.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_FonNo")) Then
                txtstudentPhone.Text = ds.Tables(0).Rows(0).Item("student_FonNo")
            Else
                txtstudentPhone.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Address")) Then
                txtstudentAddress.Text = ds.Tables(0).Rows(0).Item("student_Address")
            Else
                txtstudentAddress.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_City")) Then
                txtstudentCity.Text = ds.Tables(0).Rows(0).Item("student_City")
            Else
                txtstudentCity.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_State")) Then
                ddlState.SelectedValue = ds.Tables(0).Rows(0).Item("student_State")
            Else
                ddlState.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_StateOfBirth")) Then
                ddlStateOfBirth.SelectedValue = ds.Tables(0).Rows(0).Item("student_StateOfBirth")
            Else
                ddlStateOfBirth.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_CountryOfBirth")) Then
                ddlCountryOfBirth.SelectedValue = ds.Tables(0).Rows(0).Item("student_CountryOfBirth")
            Else

                If ddlStateOfBirth.SelectedValue <> "OTHERS" Then
                    ddlCountryOfBirth.SelectedValue = "Malaysia"
                Else
                    ddlCountryOfBirth.SelectedValue = ""
                End If

            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_PostalCode")) Then
                txtstudentPostcode.Text = ds.Tables(0).Rows(0).Item("student_PostalCode")
            Else
                txtstudentPostcode.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Level")) Then
                txtstudentLevel.Text = ds.Tables(0).Rows(0).Item("student_Level")
            Else
                txtstudentLevel.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                txtstudentYear.Text = ds.Tables(0).Rows(0).Item("year")
            Else
                txtstudentYear.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sem")) Then
                txtstudentSem.Text = ds.Tables(0).Rows(0).Item("student_Sem")
            Else
                txtstudentSem.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Race")) Then
                ddlRace.SelectedValue = ds.Tables(0).Rows(0).Item("student_Race")
            Else
                ddlRace.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Religion")) Then
                ddlReligion.SelectedValue = ds.Tables(0).Rows(0).Item("student_Religion")
            Else
                ddlReligion.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Stream")) Then
                ddlStream.SelectedValue = ds.Tables(0).Rows(0).Item("student_Stream")
            Else
                ddlStream.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Campus")) Then
                ddlCampus.SelectedValue = ds.Tables(0).Rows(0).Item("student_Campus")
            Else
                ddlCampus.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Photo")) Then
                Dim redirectText As String = ds.Tables(0).Rows(0).Item("student_Photo")
                student_Photo.ImageUrl = "~/subdomains/kppsys/httpdocs/" & redirectText.Substring(2)
            Else
                student_Photo.ImageUrl = "~/subdomains/kppsys/httpdocs/student_Image/user.png"
            End If

        End If
    End Sub

    Private Sub btnUpdateStudentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStudentInfo.ServerClick
        Dim errorCount As Integer = 0
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objNewConn As SqlConnection = New SqlConnection(strConn)

        Dim access As String = Request.QueryString("std_ID")

        If IsNumeric(txtstudentMykad.Text) And txtstudentMykad.Text <> "" And txtstudentMykad.Text.Length < 14 Then

            If txtstudentPostcode.Text = "" Or IsNumeric(txtstudentPostcode.Text) Then

                If txtstudentName.Text <> "" And Not IsNothing(txtstudentName.Text) Then

                    If txtstudentPhone.Text = "" Or txtstudentPhone.Text.Length > 0 Then

                        If txtstudentCity.Text = "" Or txtstudentCity.Text.Length > 0 Then

                            If txtstudentGender.Text <> "" And Regex.IsMatch(txtstudentGender.Text, "^[A-Za-z]+$") Then

                                If txtstudentEmail.Text <> "" Or txtstudentEmail.Text = "" Then

                                    Dim imgPath As String = "~/student_Image/user.png"

                                    If uploadPhoto.PostedFile.FileName <> "" Then

                                        Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                        ''sets the image path
                                        imgPath = "~/student_Image/" + filename

                                        ''then save it to the Folder
                                        uploadPhoto.SaveAs(Server.MapPath(imgPath))
                                    End If

                                    'UPDATE STUDENT DATA
                                    strSQL = "  UPDATE student_info set student_Mykad='" & txtstudentMykad.Text & "',
                                                student_Sex='" & txtstudentGender.Text & "',student_Name='" & oCommon.FixSingleQuotes(txtstudentName.Text) & "',student_Email='" & txtstudentEmail.Text & "',
                                                student_FonNo='" & txtstudentPhone.Text & "',student_Address='" & txtstudentAddress.Text & "',student_ID = '" & txtstudentID.Text & "',
                                                student_City='" & txtstudentCity.Text & "',student_State='" & ddlState.SelectedValue & "',student_StateOfBirth='" & ddlStateOfBirth.SelectedValue & "',student_Race='" & ddlRace.SelectedValue & "',student_Religion='" & ddlReligion.SelectedValue & "', student_CountryOfBirth = '" & ddlCountryOfBirth.SelectedValue & "'
                                                student_PostalCode='" & txtstudentPostcode.Text & "',student_Stream='" & ddlStream.SelectedValue & "', student_Campus = '" & ddlCampus.SelectedValue & "', student_Photo = '" & imgPath & "'  where std_ID = '" & access & "' "
                                    strRet = oCommon.ExecuteSQL(strSQL)

                                    If strRet = "0" Then

                                        ShowMessage(" Update Student Data ", MessageType.Success)

                                        Dim host As String = Net.Dns.GetHostName()

                                        Dim std_Name As String = "Select student_Name from student_info where std_ID = '" & access & "'"
                                        Dim data_stdName As String = oCommon.getFieldValue(std_Name)

                                        'Insert activity trail image into ActivityTrail_BtmLvl DB
                                        Using PJGDATA As New SqlCommand("INSERT into ActivityTrail_BtmLvl(Log_Date,Activity,Login_ID,User_HostAddress,Page,Name_Matters) 
                                                     values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','Update Student Data','" & access & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "','pelajar_update_profile.aspx','" & oCommon.FixSingleQuotes(data_stdName) & "')", objConn)
                                            objConn.Open()
                                            Dim k = PJGDATA.ExecuteNonQuery()
                                            objConn.Close()
                                        End Using

                                    Else
                                        ShowMessage(" Update Student Data", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage(" Invalid Email Format", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Invalid Gender Information", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Invalid City Information", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Invalid Student PHone", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Invalid Student Name", MessageType.Error)
                End If
            Else
                ShowMessage(" Invalid Postcode", MessageType.Error)
            End If
        Else
            ShowMessage(" Invalid Student MYKAD", MessageType.Error)
        End If
    End Sub

    Private Sub LoadPageParent2()

        strSQL = "  SELECT UPPER(parent_Name) parent_Name, parent_IC, parent_MobileNo, UPPER(parent_Status) parent_Status, UPPER(parent_work) parent_work, parent_Salary from parent_Info 
                    Left Join student_info ON parent_Info.parent_ID = student_info.parent_fatherID 
                    WHERE student_info.std_ID ='" & Request.QueryString("std_ID") & "' and parent_info.parent_No = '1'"

        Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

        Dim dset As DataSet = New DataSet
        sqlDB.Fill(dset, "AnyTable")

        Dim Rows As Integer = 0
        Dim Count As Integer = 1
        Dim Table As DataTable = New DataTable
        Table = dset.Tables(0)
        If Table.Rows.Count > 0 Then
            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Name")) Then
                lblGuardian1Name.Text = dset.Tables(0).Rows(0).Item("parent_Name")
            Else
                lblGuardian1Name.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_IC")) Then
                lblGuardian1Mykad.Text = dset.Tables(0).Rows(0).Item("parent_IC")
            Else
                lblGuardian1Mykad.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Status")) Then
                lblGuardian1Relationship.Text = dset.Tables(0).Rows(0).Item("parent_Status")
            Else
                lblGuardian1Relationship.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                lblGuardian1Phone.Text = dset.Tables(0).Rows(0).Item("parent_MobileNo")
            Else
                lblGuardian1Phone.Text = ""
            End If


            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_work")) Then
                lblGuardian1Job.Text = dset.Tables(0).Rows(0).Item("parent_work")
            Else
                lblGuardian1Job.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Salary")) Then
                lblGuardian1Salary.Text = dset.Tables(0).Rows(0).Item("parent_Salary")
            Else
                lblGuardian1Salary.Text = ""
            End If
        End If
    End Sub

    Private Sub LoadPageParent1()

        strSQL = "  SELECT UPPER(parent_Name) parent_Name, parent_IC, parent_MobileNo, UPPER(parent_Status) parent_Status, UPPER(parent_work) parent_work, parent_Salary from parent_Info 
                    Left Join student_info ON parent_Info.parent_ID = student_info.parent_motherID 
                    WHERE student_info.std_ID ='" & Request.QueryString("std_ID") & "' and parent_info.parent_No = '2'"

        Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

        Dim dset As DataSet = New DataSet
        sqlDB.Fill(dset, "AnyTable")

        Dim Rows As Integer = 0
        Dim Count As Integer = 1
        Dim Table As DataTable = New DataTable
        Table = dset.Tables(0)
        If Table.Rows.Count > 0 Then
            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Name")) Then
                lblGuardian2Name.Text = dset.Tables(0).Rows(0).Item("parent_Name")
            Else
                lblGuardian2Name.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_IC")) Then
                lblGuardian2Mykad.Text = dset.Tables(0).Rows(0).Item("parent_IC")
            Else
                lblGuardian2Mykad.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Status")) Then
                lblGuardian2Relationship.Text = dset.Tables(0).Rows(0).Item("parent_Status")
            Else
                lblGuardian2Relationship.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                lblGuardian2Phone.Text = dset.Tables(0).Rows(0).Item("parent_MobileNo")
            Else
                lblGuardian2Phone.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_work")) Then
                lblGuardian2Job.Text = dset.Tables(0).Rows(0).Item("parent_work")
            Else
                lblGuardian2Job.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Salary")) Then
                lblGuardian2Salary.Text = dset.Tables(0).Rows(0).Item("parent_Salary")
            Else
                lblGuardian2Salary.Text = ""
            End If
        End If
    End Sub

    Private Sub btnUpdateParentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateParentInfo.ServerClick

        Dim GetParentID1 As String = "Select parent_fatherID from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
        Dim CollectParentID1 As String = oCommon.getFieldValue(GetParentID1)

        If CollectParentID1.Length = 0 Then

            strSQL = "  Insert into parent_info(parent_IC,parent_Name,parent_MobileNo,parent_Status,parent_Work,parent_Salary,parent_No, parent_Password)
                        Values('" & lblGuardian1Mykad.Text & "','" & oCommon.FixSingleQuotes(lblGuardian1Name.Text) & "','" & lblGuardian1Phone.Text & "','" & lblGuardian1Relationship.Text & "','" & lblGuardian1Job.Text & "','" & lblGuardian1Salary.Text & "','1','" & lblGuardian1Mykad.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

            strSQL = "Select parent_ID from parent_info where parent_IC = '" & lblGuardian1Mykad.Text & "' and parent_Name = '" & lblGuardian1Name.Text & "' and parent_No = '1'"
            strRet = oCommon.getFieldValue(strSQL)

            strSQL = "Update student_info set parent_fatherID = '" & strRet & "' where std_ID = '" & Request.QueryString("std_ID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Update Guardian 1 Data ", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Guardian 1 Data ", MessageType.Error)
            End If
        Else
            'UPDATE PARENT DATA 1
            strSQL = "  UPDATE parent_info set parent_IC ='" & lblGuardian1Mykad.Text & "',
                        parent_Name='" & oCommon.FixSingleQuotes(lblGuardian1Name.Text) & "',
                        parent_MobileNo='" & lblGuardian1Phone.Text & "',
                        parent_Status='" & lblGuardian1Relationship.Text & "',
                        parent_Work='" & lblGuardian1Job.Text & "',
                        parent_Salary='" & lblGuardian1Salary.Text & "'
                        where parent_ID = '" & CollectParentID1 & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Update Guardian 1 Data ", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Guardian 1 Data ", MessageType.Error)
            End If
        End If

        Dim GetParentID2 As String = "Select parent_motherID from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
        Dim CollectParentID2 As String = oCommon.getFieldValue(GetParentID2)

        If CollectParentID1.Length = 0 Then

            strSQL = "  Insert into parent_info(parent_IC,parent_Name,parent_MobileNo,parent_Status,parent_Work,parent_Salary,parent_No,parent_Password)
                        Values('" & lblGuardian2Mykad.Text & "','" & oCommon.FixSingleQuotes(lblGuardian2Name.Text) & "','" & lblGuardian2Phone.Text & "','" & lblGuardian2Relationship.Text & "','" & lblGuardian2Job.Text & "','" & lblGuardian2Salary.Text & "','2','" & lblGuardian2Mykad.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

            strSQL = "Select parent_ID from parent_info where parent_IC = '" & lblGuardian2Mykad.Text & "' and parent_Name = '" & lblGuardian2Name.Text & "' and parent_No = '2'"
            strRet = oCommon.getFieldValue(strSQL)

            strSQL = "Update student_info set parent_motherID = '" & strRet & "' where std_ID = '" & Request.QueryString("std_ID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Update Guardian 2 Data ", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Guardian 2 Data ", MessageType.Error)
            End If
        Else
            'UPDATE PARENT DATA 2
            strSQL = "  UPDATE parent_info set parent_IC ='" & lblGuardian2Mykad.Text & "',
                        parent_Name='" & oCommon.FixSingleQuotes(lblGuardian2Name.Text) & "',
                        parent_MobileNo='" & lblGuardian2Phone.Text & "',
                        parent_Status='" & lblGuardian2Relationship.Text & "',
                        parent_Work='" & lblGuardian2Job.Text & "',
                        parent_Salary='" & lblGuardian2Salary.Text & "'
                        where parent_ID = '" & CollectParentID2 & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Update Guardian 2 Data ", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Guardian 2 Data ", MessageType.Error)
            End If
        End If

    End Sub

    Private Sub Year_List()
        strSQL = "select distinct year from student_level where std_ID = '" & Request.QueryString("std_ID") & "' order by year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseYear.DataSource = ds
            ddlCourseYear.DataTextField = "year"
            ddlCourseYear.DataValueField = "year"
            ddlCourseYear.DataBind()

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Semester_List()
        strSQL = "select * from setting where type = 'sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseSemester.DataSource = ds
            ddlCourseSemester.DataTextField = "Parameter"
            ddlCourseSemester.DataValueField = "Value"
            ddlCourseSemester.DataBind()

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LoadCurrentYear()
        strSQL = "select Max(Parameter) as year from setting where type = 'year'"

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
                ddlCourseYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
                ddlCourseSemester.SelectedIndex = 0
            Else
                ddlCourseYear.SelectedValue = 0
                ddlCourseSemester.SelectedIndex = 0
            End If
        End If
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

            If Session("getEditButton_CI") = "TRUE" Then
                gvTable.Columns(7).Visible = True
            Else
                gvTable.Columns(7).Visible = False
            End If

            If Session("getDeleteButton_CI") = "TRUE" Then
                gvTable.Columns(8).Visible = True
            Else
                gvTable.Columns(8).Visible = False
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQL() As String

        Dim data_ID As String = Request.QueryString("std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_Name ASC"

        tmpSQL = "Select * From course left join subject_info on course.subject_ID=subject_info.subject_ID left join class_info on course.class_ID=class_info.class_ID left join student_info on course.std_ID=student_info.std_ID"
        strWhere = " WHERE course.std_ID = '" & data_ID & "'"
        strWhere += " AND course.year = '" & ddlCourseYear.SelectedValue & "'"
        strWhere += " AND subject_info.subject_Sem = '" & ddlCourseSemester.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQL
    End Function

    Private Sub ddlCourseYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCourseSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseSemester.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
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

        Dim txtSubjectCode As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtsubject_code"), TextBox)
        Dim txtClassName As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtclass_Name"), TextBox)

        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        If ddlCourseYear.SelectedValue <> Now.Year Then
            ShowMessage("Unable to edit previous course data", MessageType.Error)
        Else
            ''get subject_ID
            Dim subjectID As String = "select subject_ID from subject_info where subject_code = '" + txtSubjectCode.Text + "' and subject_year = '" & ddlCourseYear.SelectedValue & "' and subject_sem = '" & ddlCourseSemester.SelectedValue & "'"
            Dim datasubjectID As String = oCommon.getFieldValue(subjectID)

            If datasubjectID.Length > 0 Then

                ''get subject type
                Dim subjectType As String = "select subject_StudentYear from subject_info where subject_ID = '" & datasubjectID & "'"
                Dim datasubjectType As String = oCommon.getFieldValue(subjectType)

                ''get class type
                Dim classType As String = "select distinct class_type from class_info where class_Name = '" & txtClassName.Text & "' and class_year = '" & ddlCourseYear.SelectedValue & "'"
                Dim dataclassType As String = oCommon.getFieldValue(classType)

                If datasubjectType <> "Compulsory" And dataclassType = "Electives" Then

                    ''get class ID 
                    Dim classID As String = "select class_ID from class_info where class_Name = '" & txtClassName.Text & "' and class_year = '" & ddlCourseYear.SelectedValue & "' and class_sem = '" & ddlCourseSemester.SelectedValue & "'"
                    Dim dataclassID As String = oCommon.getFieldValue(subjectID)

                    ''update the data in table
                    strSQL = "UPDATE course SET subject_ID='" & datasubjectID & "', class_ID='" & dataclassID & "' WHERE course_ID ='" & strKeyID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf datasubjectType = "Compulsory" And dataclassType = "Compulsory" Then

                    Dim classID As String = "select class_ID from class_info where class_Name = '" & txtClassName.Text & "' and class_year = '" & ddlCourseYear.SelectedValue & "'"
                    Dim dataclassID As String = oCommon.getFieldValue(subjectID)

                    ''update the data in table
                    strSQL = "UPDATE course SET subject_ID='" & datasubjectID & "', class_ID='" & dataclassID & "' WHERE course_ID ='" & strKeyID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If

                If strRet = "0" Then
                    ShowMessage("Update Course Data", MessageType.Success)
                End If

            End If
        End If

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete course where course_ID = '" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Function BindDataKOKO(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLKOKO, strConnPermata)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConnPermata.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQLKOKO() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT distinct A.std_ID, koko_pelajar.Tahun, koko_kelas.Kelas,
                 (SELECT UPPER(NamaBI) FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,
                 (SELECT UPPER(NamaBI) FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,
                 (SELECT UPPER(NamaBI) FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,
                 (SELECT UPPER(NamaBI) FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan
                 FROM koko_pelajar
                 LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                 LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                 LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                 LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"
        strWhere = " WHERE A.student_Status = 'Access' AND A.std_ID = '" & Request.QueryString("std_ID") & "'"
        strOrder = " ORDER BY koko_pelajar.Tahun ASC"

        getSQLKOKO = tmpSQL & strWhere & strOrder

        Return getSQLKOKO

    End Function

    Private Sub Year_Examination()

        Dim data_ID As String = Request.QueryString("std_ID")

        strSQL = "select distinct student_Level.year from student_Level
                  where student_Level.std_ID = '" + data_ID + "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "year"
            ddlExamYear.DataValueField = "year"
            ddlExamYear.DataBind()
            ddlExamYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Exam_Examination()

        Dim get_Check As String = ""

        If Session("SchoolCampus") = "PGPN" Then
            get_Check = "select Value from setting where Type  = 'Exam Result PGPN'"
        ElseIf Session("SchoolCampus") = "APP" Then
            get_Check = "select Value from setting where Type  = 'Exam Result APP'"
        End If

        Dim data_Check As String = oCommon.getFieldValue(get_Check)

        Dim data_Exam As String = ""

        If data_Check = "off" Or data_Check = "Off" Or data_Check = "OFF" Then

            ''show all student exam result except the current exam
            data_Exam = "   select max(exam_result.exam_ID) from exam_result
                            left join exam_info on exam_result.exam_ID = exam_Info.exam_ID
                            left join course on exam_result.course_ID = course.course_ID
                            where course.year = '" & ddlExamYear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            strRet = oCommon.getFieldValue(data_Exam)

            strSQL = "  select distinct exam_info.exam_Name from exam_result left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                        left join course on exam_result.course_ID = course.course_ID left join student_info on course.std_ID = student_info.std_ID
                        where student_info.student_Status = 'Access' and course.year = '" & ddlExamYear.SelectedValue & "' and exam_info.exam_Year = '" & ddlExamYear.SelectedValue & "' and exam_info.exam_ID < '" & strRet & "'
                        and student_info.std_ID = '" & Request.QueryString("std_ID") & "'"

        ElseIf data_Check = "On" Or data_Check = "on" Or data_Check = "ON" Then

            ''show all student exam result
            strSQL = "  select distinct exam_info.exam_Name from exam_result left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                        left join course on exam_result.course_ID = course.course_ID left join student_info on course.std_ID = student_info.std_ID
                        where student_info.student_Status = 'Access' and course.year = '" & ddlExamYear.SelectedValue & "' and exam_info.exam_Year = '" & ddlExamYear.SelectedValue & "'
                        and student_info.std_ID = '" & Request.QueryString("std_ID") & "'"
        End If



        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllExamName.DataSource = ds
            ddllExamName.DataTextField = "exam_Name"
            ddllExamName.DataValueField = "exam_Name"
            ddllExamName.DataBind()
            ddllExamName.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlExamYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamYear.SelectedIndexChanged
        Try
            Exam_Examination()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddllExamName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddllExamName.SelectedIndexChanged
        Try
            strRet = BindDataExam(datRespondentExam)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataExam(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLExamination, strConn)
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

    Private Function getSQLExamination() As String

        Dim data_ID As String = Request.QueryString("std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_Name ASC"

        tmpSQL = "      select exam_result.ID,exam_info.exam_Year,exam_info.exam_Name,subject_Name,subject_code,marks,grade,gpa from course
                        left join subject_info on course.subject_ID=subject_info.subject_ID
                        left join exam_result on course.course_ID=exam_result.course_ID
                        left join exam_Info on exam_result.exam_Id=exam_Info.exam_ID
                        left join grade_info on exam_result.grade=grade_info.grade_Name"
        strWhere = "    where course.std_ID = '" + data_ID + "'"
        strWhere += "   AND exam_Info.exam_Year = '" & ddlExamYear.SelectedValue & "'"
        strWhere += "   AND exam_Info.exam_Name = '" & ddllExamName.SelectedValue & "'"

        getSQLExamination = tmpSQL & strWhere & strOrderby

        Return getSQLExamination
    End Function

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
        Dim data_ID As String = Request.QueryString("std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrderby As String = " ORDER BY year ASC, hostel_Sem ASC"

        tmpSQL = "  select std_ID, hostel_CampusNames, hostel_BlockNames, hostel_BlockLevels, hostel_Sem, room_Name, student_room.year
                    from hostel_info
                    left join room_info on hostel_info.hostel_ID = room_info.hostel_ID
                    left join student_room on student_room.room_ID = room_info.room_ID "

        strWhere = " where student_room.std_ID = '" & data_ID & "'"

        getSQLHostel = tmpSQL & strWhere & strOrderby

        Return getSQLHostel
    End Function

    Private Function BindDataDisicpline(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLDiscipline, strConn)
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

    Private Function getSQLDiscipline() As String

        Dim data_ID As String = Request.QueryString("std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " order by Dicipline_Date DESC"

        tmpSQL = " select * from dicipline_info "
        strWhere = " where std_ID = '" & data_ID & "'"

        getSQLDiscipline = tmpSQL & strWhere & strOrder

        Return getSQLDiscipline
    End Function

    Private Function BindDataUKM1(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLUKM1, strConnPermata)
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

    Private Function getSQLUKM1() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "select B.UKM1ID,B.ExamYear,B.ExamStart,B.ExamEnd,B.Status,B.LastPage,B.ModA,B.ModB,B.ModC,B.TotalScore,B.TotalPercentage from kolejadmin.dbo.student_info C
                  left join StudentProfile A on C.student_Mykad = A.MYKAD
                  left join UKM1 B on A.StudentID = B.StudentID"
        strWhere = " where C.std_ID = '" & Request.QueryString("std_ID") & "'"
        strOrder = " ORDER BY B.ExamYear"

        getSQLUKM1 = tmpSQL & strWhere & strOrder

        Return getSQLUKM1
    End Function

    Private Function BindDataUKM2(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLUKM2, strConnPermata)
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

    Private Function getSQLUKM2() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT A.UKM2ID, A.ExamYear, A.Status, A.ExamStart, A.ExamEnd, A.LastPage, A.VCI, A.PRI, A.WMI, A.PSI, A.TotalScore, A.TotalPercentage, A.Mental_Age_Year, A.Student_IQ FROM UKM2 A
                  LEFT JOIN StudentProfile B on A.StudentID = B.StudentID
                  LEFT JOIN kolejadmin.dbo.student_info C on B.MYKAD = C.student_Mykad"
        strWhere = " where C.std_ID = '" & Request.QueryString("std_ID") & "'"
        strOrder = " ORDER BY A.ExamYear"

        getSQLUKM2 = tmpSQL & strWhere & strOrder

        Return getSQLUKM2
    End Function

    Private Function BindDataUKM3(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLUKM3, strConnPermata)
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

    Private Function getSQLUKM3() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "select A.id, G.ukm3Year, A.marks_100, A.markPretest, A.markPosttest, A.insPPCS100mark, B.instruktorExam_Komen, 
                  A.insRAPPCS100mark, C.instruktorExam_Komen, A.insKPP100mark, D.instruktorExam_Komen_kpp, A.compo_Mark from ukm3 A
                  left join instruktorExam_result B on A.id = B.ukm3id
                  left join instruktorExam_result_raPcs C on A.id = C.ukm3id
                  left join instruktorExam_result_kpp D on A.id = D.ukm3id
                  left join student_info E on A.student_id = E.std_ID
                  left join kolejadmin.dbo.student_info F on E.student_Mykad = F.student_Mykad
                  left join UKM3Session G on A.session_id = G.id"

        strWhere = " where F.student_status = 'Access' and F.std_ID = '" & Request.QueryString("std_ID") & "'"

        getSQLUKM3 = tmpSQL & strWhere & strOrder

        Return getSQLUKM3
    End Function

    Private Function BindDataPPCS(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLUKMPPCS, strConnPermata)
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

    Private Function getSQLUKMPPCS() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT A.PPCSID,A.PPCSDate,A.PPCSStatus,A.StatusTawaran,A.StatusReason,A.NamaAsrama,A.NoBilik,B.CourseCode,C.ClassCode FROM PPCS A
                  LEFT JOIN PPCS_Course B ON A.CourseID = B.CourseID
                  LEFT JOIN PPCS_Class C ON A.ClassID = C.ClassID
                  LEFT JOIN StudentProfile D on A.StudentID = D.StudentID
                  LEFT JOIN kolejadmin.dbo.student_info E on D.MYKAD = E.student_Mykad"
        strWhere = " WHERE E.std_ID ='" & Request.QueryString("std_ID") & "'"
        strOrder = " ORDER BY A.PPCSDate"

        getSQLUKMPPCS = tmpSQL & strWhere & strOrder

        Return getSQLUKMPPCS
    End Function

    Private Sub Year_Info_list()
        strSQL = "select distinct FDStudent_Year from fee_documentList_Student where std_ID = '" & Request.QueryString("std_ID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlView_Year.DataSource = ds
            ddlView_Year.DataTextField = "FDStudent_Year"
            ddlView_Year.DataValueField = "FDStudent_Year"
            ddlView_Year.DataBind()
            ddlView_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlView_Year.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataReference(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLReference, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

            If Session("getViewButton_RI") = "TRUE" Then
                gvTable.Columns(4).Visible = True
            Else
                gvTable.Columns(4).Visible = False
            End If

            If Session("getDownloadButton_RI") = "TRUE" Then
                gvTable.Columns(5).Visible = True
            Else
                gvTable.Columns(5).Visible = False
            End If

            If Session("getDeleteButton_RI") = "TRUE" Then
                gvTable.Columns(6).Visible = True
            Else
                gvTable.Columns(6).Visible = False
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLReference() As String

        Dim data_ID As String = Request.QueryString("std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY FDStudent_Year DESC, FDStudent_DateTime DESC, FDStudent_Name ASC"

        tmpSQL = "Select * from fee_documentList_Student"
        strWhere = " WHERE std_ID = '" & data_ID & "'"
        strWhere += " AND FDStudent_Year = '" & ddlView_Year.SelectedValue & "'"

        getSQLReference = tmpSQL & strWhere & strOrderby

        ''--debug
        Return getSQLReference
    End Function

    Protected Sub ddlView_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView_Year.SelectedIndexChanged
        Try
            strRet = BindDataReference(datRespondentReference)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondentReference_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondentReference.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondentReference.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete fee_documentList_Student where FDStudent_id = '" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindDataReference(datRespondentReference)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)

        Dim filePath As String = TryCast(sender, LinkButton).CommandArgument

        Dim find_stdMykad As String = "Select student_Mykad from student_info where std_ID = '" & Request.QueryString("std_ID") & "' and student_Status = 'Access'"
        Dim get_stdMykad As String = oCommon.getFieldValue(find_stdMykad)

        Dim find_datetime As String = "select FDStudent_Datetime from fee_documentList_Student where std_ID = '" & Request.QueryString("std_ID") & "' and FDStudent_Name = '" & filePath & "'"
        Dim get_datetime As String = oCommon.getFieldValue(find_datetime)

        Dim substring_get_day As String = get_datetime.Substring(0, 2)
        Dim substring_get_month As String = get_datetime.Substring(3, 2)
        Dim substring_get_year As String = get_datetime.Substring(6, 4)

        Dim convert_datetime As String = substring_get_year & substring_get_month & substring_get_day

        Dim link_name As String = get_stdMykad & "_" & convert_datetime & "_" & filePath

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & filePath)
        'Response.TransmitFile("D:\Genius Pintar Personal Version 2.0\KPP_SYS\KPP_SYS\reference download\LAMPIRAN A AKUAN PEMBAYARAN YURAN ASAS 2.pdf")
        Response.TransmitFile("C:\Inetpub\vhosts\permatapintar.edu.my\subdomains\kppsys\httpdocs\reference_upload\" & link_name)
        Response.End()
    End Sub

    Protected Sub ViewFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = TryCast(sender, LinkButton).CommandArgument

        Dim find_stdMykad As String = "Select student_Mykad from student_info where std_ID = '" & Request.QueryString("std_ID") & "' and student_Status = 'Access'"
        Dim get_stdMykad As String = oCommon.getFieldValue(find_stdMykad)

        Dim find_datetime As String = "select FDStudent_Datetime from fee_documentList_Student where std_ID = '" & Request.QueryString("std_ID") & "' and FDStudent_Name = '" & filePath & "'"
        Dim get_datetime As String = oCommon.getFieldValue(find_datetime)

        Dim substring_get_day As String = get_datetime.Substring(0, 2)
        Dim substring_get_month As String = get_datetime.Substring(3, 2)
        Dim substring_get_year As String = get_datetime.Substring(6, 4)

        Dim convert_datetime As String = substring_get_year & substring_get_month & substring_get_day

        Dim link_name As String = get_stdMykad & "_" & convert_datetime & "_" & filePath

        Dim pathshow As String = "C:\Inetpub\vhosts\permatapintar.edu.my\subdomains\kppsys\httpdocs\reference_upload\" & link_name


        'Dim pathshow As String = "D:\Genius Pintar Personal Version 2.0\KPP_SYS\KPP_SYS\reference download\LAMPIRAN A AKUAN PEMBAYARAN YURAN ASAS 2.pdf"

        Session("Path") = pathshow

        Response.Write("<script>")
        Response.Write("window.open('pelajar_view_reference.aspx','_blank')")
        Response.Write("</script>")

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class