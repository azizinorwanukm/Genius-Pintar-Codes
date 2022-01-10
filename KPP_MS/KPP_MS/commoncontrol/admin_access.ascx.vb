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

                Checking_MenuAccess_Load()

                If Session("getStatus") = "VUA" Then ''View User Access
                    txtbreadcrum1.Text = "View User Access"

                    ViewUserAccess.Visible = True
                    RegisterUserAccess.Visible = False

                    btnViewUserAccess.Attributes("class") = "btn btn-info"
                    btnRegisterUserAccess.Attributes("class") = "btn btn-default font"

                    View_UserInfo_List()
                    View_UserPosition_List()

                    collapse_Menu_GeneralManagement.Visible = False
                    collapse_Menu_Student.Visible = False
                    collapse_Menu_Staff.Visible = False
                    collapse_Menu_Coordinator.Visible = False
                    collapse_Menu_Discipline.Visible = False
                    collapse_Menu_Counselor.Visible = False
                    collapse_Menu_Research.Visible = False
                    collapse_Menu_Examination.Visible = False
                    collapse_Menu_Hostel.Visible = False
                    collapse_Menu_Cocurricular.Visible = False
                    collapse_Menu_Report.Visible = False
                    collapse_Menu_Setting.Visible = False

                    ''LIST OF ALL MAIN MENU
                    MENU_GM.Visible = False
                    MENU_STD.Visible = False
                    MENU_STF.Visible = False
                    MENU_COO.Visible = False
                    MENU_DISC.Visible = False
                    MENU_COUN.Visible = False
                    MENU_RES.Visible = False
                    MENU_EXAM.Visible = False
                    MENU_HST.Visible = False
                    MENU_CC.Visible = False
                    MENU_RPT.Visible = False
                    MENU_SET.Visible = False

                    ''LIST OF ALL SUB MENU 1
                    MENU_GM_EM.Visible = False
                    MENU_GM_GM.Visible = False
                    MENU_GM_AM.Visible = False
                    MENU_STD_COURSEM.Visible = False
                    MENU_STD_CLASSM.Visible = False
                    MENU_STD_SM.Visible = False
                    MENU_STD_SS.Visible = False
                    MENU_STD_CCP.Visible = False
                    MENU_STD_ATT.Visible = False
                    MENU_STD_VI.Visible = False
                    MENU_STF_SS.Visible = False
                    MENU_STF_CP.Visible = False
                    MENU_COO_SC.Visible = False
                    MENU_DISC_DM.Visible = False
                    MENU_DISC_CM.Visible = False
                    MENU_COUN_CM.Visible = False
                    MENU_COUN_SDM.Visible = False
                    MENU_COUN_PDM.Visible = False
                    MENU_COUN_POR.Visible = False
                    MENU_COUN_CA.Visible = False
                    MENU_COUN_SM.visible = False
                    MENU_RES_RSS.Visible = False
                    MENU_RES_RPF.Visible = False
                    MENU_RES_RM.Visible = False
                    MENU_EXAM_ER.Visible = False
                    MENU_EXAM_ET.Visible = False
                    MENU_EXAM_IE.Visible = False
                    MENU_HST_HM.Visible = False
                    MENU_HST_SP.Visible = False
                    MENU_HST_VHI.Visible = False
                    MENU_CC_CCM.Visible = False
                    MENU_RPT_ER.Visible = False
                    MENU_RPT_CLASSER.Visible = False
                    MENU_RPT_COURSESER.Visible = False
                    MENU_RPT_SRL.Visible = False
                    MENU_RPT_AR.Visible = False
                    MENU_RPT_FR.Visible = False
                    MENU_SET_UC.Visible = False
                    MENU_SET_SC.Visible = False
                    MENU_SET_UA.Visible = False

                    ''LIST OF ALL SUB MENU 2
                    MENU_GM_EM_VE.Visible = False
                    MENU_GM_EM_RE.Visible = False
                    MENU_STD_COURSEM_VC.Visible = False
                    MENU_STD_COURSEM_RC.Visible = False
                    MENU_STD_COURSEM_TC.Visible = False
                    MENU_STD_CLASSM_VC.Visible = False
                    MENU_STD_CLASSM_RC.Visible = False
                    MENU_STD_CLASSM_TC.Visible = False
                    MENU_STD_SS_VS.Visible = False
                    MENU_STD_SS_RS.Visible = False
                    MENU_STD_SS_IS.Visible = False
                    MENU_STD_ATT_VA.Visible = False
                    MENU_STD_ATT_UA.Visible = False
                    MENU_STD_VI_VCOURSE.Visible = False
                    MENU_STD_VI_VCLASS.Visible = False
                    MENU_STD_VIVCOCURRICULUM.Visible = False
                    MENU_STD_VI_VH.Visible = False
                    MENU_STD_VI_VR.Visible = False
                    MENU_STF_SS_VS.Visible = False
                    MENU_STF_SS_RS.Visible = False
                    MENU_STF_SS_IS.Visible = False
                    MENU_STF_CP_VSC.Visible = False
                    MENU_STF_CP_RSC.Visible = False
                    MENU_COO_SC_VC.Visible = False
                    MENU_COO_SC_RC.Visible = False
                    MENU_DISC_DM_VD.Visible = False
                    MENU_DISC_DM_RD.Visible = False
                    MENU_DISC_CM_VC.Visible = False
                    MENU_DISC_CM_RC.Visible = False
                    MENU_COUN_CM_VM.Visible = False
                    MENU_COUN_CM_SDM.Visible = False
                    MENU_COUN_CM_PDM.Visible = False
                    MENU_COUN_CA_SC.Visible = False
                    MENU_COUN_CA_VCR.Visible = False
                    MENU_COUN_SM_LS.Visible = False
                    MENU_COUN_SM_SS.Visible = False
                    MENU_RES_RSS_VSGS.Visible = False
                    MENU_RES_RSS_RSGS.Visible = False
                    MENU_RES_RPF_VSP.Visible = False
                    MENU_RES_RPF_RSP.Visible = False
                    MENU_RES_RM_VSM.Visible = False
                    MENU_RES_RM_RSN.Visible = False
                    MENU_EXAM_ER_AR.Visible = False
                    MENU_EXAM_ER_CR.Visible = False
                    MENU_EXAM_ET_CET.Visible = False
                    MENU_EXAM_ET_OT.Visible = False
                    MENU_HST_HM_VH.Visible = False
                    MENU_HST_HM_RH.Visible = False
                    MENU_EXAM_IE_IER.Visible = False
                    MENU_EXAM_IE_IGC.Visible = False
                    MENU_SET_UC_VUA.Visible = False
                    MENU_SET_UC_RUA.Visible = False
                    MENU_SET_SC_VSC.Visible = False
                    MENU_SET_SC_RSC.Visible = False

                    ''LIST OF ALL SUB MENU 3
                    MENU_STD_SS_VS_VB_SI.Visible = False
                    MENU_STD_SS_VS_VB_FI.Visible = False
                    MENU_STD_SS_VS_VB_COURSEI.Visible = False
                    MENU_STD_SS_VS_VB_COCURRICULARI.Visible = False
                    MENU_STD_SS_VS_VB_EI.Visible = False
                    MENU_STD_SS_VS_VB_HI.Visible = False
                    MENU_STD_SS_VS_VB_DI.Visible = False
                    MENU_STD_SS_VS_VB_UP.Visible = False
                    MENU_STD_SS_VS_VB_RI.Visible = False
                    MENU_STF_SS_VS_EB_SI.Visible = False
                    MENU_STF_SS_VS_EB_CI.Visible = False
                    MENU_HST_HM_VH_EB_EHI.Visible = False
                    MENU_HST_HM_VH_EB_ERI.Visible = False

                    ''LIST OF ALL BUTTON FUNCTION 1
                    MENU_GM_EM_VE_EB.Visible = False
                    MENU_GM_EM_VE_DB.Visible = False
                    MENU_GM_EM_RE_RB.Visible = False
                    MENU_GM_GM_EB.Visible = False
                    MENU_GM_GM_DB.Visible = False
                    MENU_STD_COURSEM_VC_EB.Visible = False
                    MENU_STD_COURSEM_VC_DB.Visible = False
                    MENU_STD_COURSEM_RC_RB.Visible = False
                    MENU_STD_COURSEM_TC_TB.Visible = False
                    MENU_STD_CLASSM_VC_EB.Visible = False
                    MENU_STD_CLASSM_VC_DB.Visible = False
                    MENU_STD_CLASSM_RC_RB.Visible = False
                    MENU_STD_CLASSM_TC_TB.Visible = False
                    MENU_STD_SM_TB.Visible = False
                    MENU_STD_SS_VS_VB.Visible = False
                    MENU_STD_SS_VS_DB.Visible = False
                    MENU_STD_SS_RS_RB.Visible = False
                    MENU_STD_SS_IS_IB.Visible = False
                    MENU_STD_CCP_RB.Visible = False
                    MENU_STD_ATT_UA_UB.Visible = False
                    MENU_STD_VI_VCOURSE_AB.Visible = False
                    MENU_STD_VI_VCLASS_UB.Visible = False
                    MENU_STD_VI_VR_UB.Visible = False
                    MENU_STF_SS_VS_EB.Visible = False
                    MENU_STF_SS_VS_DB.Visible = False
                    MENU_STF_SS_RS_RB.Visible = False
                    MENU_STF_SS_IS_IB.Visible = False
                    MENU_STF_CP_RSC_RB.Visible = False
                    MENU_COO_SC_VC_EB.Visible = False
                    MENU_COO_SC_VC_DB.Visible = False
                    MENU_COO_SC_RC_RB.Visible = False
                    MENU_DISC_DM_VD_EB.Visible = False
                    MENU_DISC_DM_VD_DB.Visible = False
                    MENU_DISC_DM_RD_RB.Visible = False
                    MENU_DISC_CM_VC_EB.Visible = False
                    MENU_DISC_CM_VC_DB.Visible = False
                    MENU_DISC_CM_RC_RB.Visible = False
                    MENU_COUN_CM_VM_EB.Visible = False
                    MENU_COUN_CM_VM_DB.Visible = False
                    MENU_COUN_CM_SDM_RB.Visible = False
                    MENU_COUN_CM_PDM_RB.Visible = False
                    MENU_COUN_SDM_RB.Visible = False
                    MENU_COUN_PDM_RB.Visible = False
                    MENU_COUN_POR_RB.Visible = False
                    MENU_RES_RSS_VSGS_DB.Visible = False
                    MENU_RES_RSS_RSGS_RB.Visible = False
                    MENU_RES_RPF_VSP_DB.Visible = False
                    MENU_RES_RPF_RSP_RB.Visible = False
                    MENU_RES_RM_VSM_DB.Visible = False
                    MENU_RES_RM_RSN_RB.Visible = False
                    MENU_EXAM_ER_AR_UB.Visible = False
                    MENU_EXAM_ET_CET_GEN.Visible = False
                    MENU_EXAM_ET_CET_BI.Visible = False
                    MENU_EXAM_ET_CET_BM.Visible = False
                    MENU_EXAM_ET_OT_BI.Visible = False
                    MENU_EXAM_ET_OT_BM.Visible = False
                    MENU_EXAM_IE_IER_BI.Visible = False
                    MENU_EXAM_IE_IGC_BI.Visible = False
                    MENU_HST_HM_VH_EB.Visible = False
                    MENU_HST_HM_VH_DB.Visible = False
                    MENU_HST_HM_RH_RB.Visible = False
                    MENU_HST_SP_UB.Visible = False
                    MENU_HST_VHI_DB.Visible = False
                    MENU_SET_UC_RUA_RB.Visible = False
                    MENU_SET_SC_VSC_EB.Visible = False
                    MENU_SET_SC_VSC_DB.Visible = False
                    MENU_SET_SC_RSC_RB.Visible = False
                    MENU_SET_UA_UB.Visible = False

                    ''LIST OF ALL BUTTON FUNCTION 2
                    MENU_STD_SS_VS_VB_SI_UB.Visible = False
                    MENU_STD_SS_VS_VB_FI_UB.Visible = False
                    MENU_STD_SS_VS_VB_COURSEI_EB.Visible = False
                    MENU_STD_SS_VS_VB_COURSEI_DB.Visible = False
                    MENU_STF_SS_VS_EB_SI_UB.Visible = False
                    MENU_STF_SS_VS_EB_CI_DB.Visible = False
                    MENU_HST_HM_VH_EB_EHI_UB.Visible = False
                    MENU_HST_HM_VH_EB_ERI_EB.Visible = False
                    MENU_HST_HM_VH_EB_ERI_DB.Visible = False

                ElseIf Session("getStatus") = "RUA" Then ''Register User Access
                    txtbreadcrum1.Text = "Register User Access"

                    ViewUserAccess.Visible = False
                    RegisterUserAccess.Visible = True

                    btnViewUserAccess.Attributes("class") = "btn btn-default font"
                    btnRegisterUserAccess.Attributes("class") = "btn btn-info"

                    UserInfo_List()
                    UserPosition_List()
                    MenuInfo_List()

                    Sub1MenuInfo_List()
                    Sub2MenuInfo_List()
                    Sub3MenuInfo_List()

                    displayStatusSubMenu1.Visible = False
                    displayStatusSubMenu2.Visible = False
                    displayStatusSubMenu3.Visible = False

                    displayButtonFunction1.Visible = False
                    displayButtonFunction2.Visible = False

                    F1_R1.Visible = False
                    F1_R1_C1_P1.Visible = False
                    F1_R1_C1_P2.Visible = False
                    F1_R1_C1_P3.Visible = False
                    F1_R1_C1_P4.Visible = False

                    F1_R2.Visible = False
                    F1_R2_C2_P1.Visible = False
                    F1_R2_C2_P2.Visible = False
                    F1_R2_C2_P3.Visible = False
                    F1_R2_C2_P4.Visible = False

                    F1_R3.Visible = False
                    F1_R3_C3_P1.Visible = False
                    F1_R3_C3_P2.Visible = False
                    F1_R3_C3_P3.Visible = False

                    F2_R1.Visible = False
                    F2_R1_C1_P1.Visible = False
                    F2_R1_C1_P2.Visible = False
                    F2_R1_C1_P3.Visible = False
                    F2_R1_C1_P4.Visible = False
                    F2_R1_C1_P5.Visible = False

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewUserAccess.Visible = False
        btnRegisterUserAccess.Visible = False
        ViewUserAccess.Visible = False
        RegisterUserAccess.Visible = False
        btnSaveRegisterUserAccess.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewUserAccess As String = ""
        Dim Get_RegisterUserAccess As String = ""

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

            If find_Data_SubMenu2 = "View User Access" And find_Data_SubMenu2.Length > 0 Then
                btnViewUserAccess.Visible = True
                ViewUserAccess.Visible = True

                Get_ViewUserAccess = "TRUE"
            End If

            If find_Data_SubMenu2 = "Register User Access" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterUserAccess.Visible = True
                RegisterUserAccess.Visible = True

                Get_RegisterUserAccess = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnSaveRegisterUserAccess.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewUserAccess.Visible = True
                btnRegisterUserAccess.Visible = True
                ViewUserAccess.Visible = True
                RegisterUserAccess.Visible = True
                btnSaveRegisterUserAccess.Visible = True

                Get_ViewUserAccess = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RUA" Or Session("getStatus") = "VUA" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RUA" And Session("getStatus") <> "VUA" Then
            If Get_ViewUserAccess = "TRUE" Then
                Data_If_Not_Group_Status = "VUA"
            ElseIf Get_RegisterUserAccess = "TRUE" Then
                Data_If_Not_Group_Status = "RUA"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewUserAccess = "TRUE" And Data_If_Not_Group_Status = "VUA" Then
                Session("getStatus") = "VUA"
            ElseIf Get_RegisterUserAccess = "TRUE" And Data_If_Not_Group_Status = "RUA" Then
                Session("getStatus") = "RUA"
            End If
        End If

    End Sub

    Private Sub btnViewUserAccess_ServerClick(sender As Object, e As EventArgs) Handles btnViewUserAccess.ServerClick
        Session("getStatus") = "VUA"
        Response.Redirect("admin_Master_Konfigurasi.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnRegisterUserAccess_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterUserAccess.ServerClick
        Session("getStatus") = "RUA"
        Response.Redirect("admin_Master_Konfigurasi.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub UserInfo_List()
        strSQL = "Select stf_ID, staff_Name from staff_Info where staff_Status = 'Access' and staff_Name not like '%araken%' order by staff_Name asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStaffName.DataSource = ds
            ddlStaffName.DataTextField = "staff_Name"
            ddlStaffName.DataValueField = "stf_ID"
            ddlStaffName.DataBind()
            ddlStaffName.Items.Insert(0, New ListItem("Select User", String.Empty))
            ddlStaffName.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub UserPosition_List()
        strSQL = "Select Parameter, Value from setting left join staff_Login on setting.Value = staff_Login.staff_Access where stf_ID = '" & ddlStaffName.SelectedValue & "' and staff_Access is not null and staff_Access <> '' order by Parameter asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStaffPosition.DataSource = ds
            ddlStaffPosition.DataTextField = "Parameter"
            ddlStaffPosition.DataValueField = "Value"
            ddlStaffPosition.DataBind()
            ddlStaffPosition.Items.Insert(0, New ListItem("Select Position", String.Empty))
            ddlStaffPosition.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub View_UserInfo_List()
        strSQL = "Select stf_ID, staff_Name from staff_Info where staff_Status = 'Access' and staff_Name not like '%araken%' order by staff_Name asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_ViewStaffName.DataSource = ds
            ddl_ViewStaffName.DataTextField = "staff_Name"
            ddl_ViewStaffName.DataValueField = "stf_ID"
            ddl_ViewStaffName.DataBind()
            ddl_ViewStaffName.Items.Insert(0, New ListItem("Select User", String.Empty))
            ddl_ViewStaffName.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub View_UserPosition_List()
        strSQL = "Select Parameter, Value from setting left join staff_Login on setting.Value = staff_Login.staff_Access where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' and staff_Access is not null and staff_Access <> '' order by Parameter asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_ViewStaffPosition.DataSource = ds
            ddl_ViewStaffPosition.DataTextField = "Parameter"
            ddl_ViewStaffPosition.DataValueField = "Value"
            ddl_ViewStaffPosition.DataBind()
            ddl_ViewStaffPosition.Items.Insert(0, New ListItem("Select Position", String.Empty))
            ddl_ViewStaffPosition.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub MenuInfo_List()
        strSQL = "Select distinct Menu from menu_master_access order by Menu ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMainMenu.DataSource = ds
            ddlMainMenu.DataTextField = "Menu"
            ddlMainMenu.DataValueField = "Menu"
            ddlMainMenu.DataBind()
            ddlMainMenu.Items.Insert(0, New ListItem("Select Menu", "NULL"))
            ddlMainMenu.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Sub1MenuInfo_List()
        strSQL = "Select distinct Menu_Sub1 from menu_master_access where Menu = '" & ddlMainMenu.SelectedValue & "' order by Menu_Sub1 ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubMenu1.DataSource = ds
            ddlSubMenu1.DataTextField = "Menu_Sub1"
            ddlSubMenu1.DataValueField = "Menu_Sub1"
            ddlSubMenu1.DataBind()
            ddlSubMenu1.Items.Insert(0, New ListItem("Select Sub Menu 1", "NULL"))
            ddlSubMenu1.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Sub2MenuInfo_List()
        strSQL = "Select distinct Menu_Sub2 from menu_master_access where Menu = '" & ddlMainMenu.SelectedValue & "' and Menu_Sub1 = '" & ddlSubMenu1.SelectedValue & "' and Menu_Sub2 is not null and Menu_Sub2 <> '' order by Menu_Sub2 ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubMenu2.DataSource = ds
            ddlSubMenu2.DataTextField = "Menu_Sub2"
            ddlSubMenu2.DataValueField = "Menu_Sub2"
            ddlSubMenu2.DataBind()
            ddlSubMenu2.Items.Insert(0, New ListItem("Select Sub Menu 2", "NULL"))
            ddlSubMenu2.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Sub3MenuInfo_List()
        strSQL = "Select distinct Menu_Sub3 from menu_master_access where Menu = '" & ddlMainMenu.SelectedValue & "' and Menu_Sub1 = '" & ddlSubMenu1.SelectedValue & "' and Menu_Sub2 = '" & ddlSubMenu2.SelectedValue & "' and Menu_Sub3 is not null and Menu_Sub3 <> '' order by Menu_Sub3 ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSubMenu3.DataSource = ds
            ddlSubMenu3.DataTextField = "Menu_Sub3"
            ddlSubMenu3.DataValueField = "Menu_Sub3"
            ddlSubMenu3.DataBind()
            ddlSubMenu3.Items.Insert(0, New ListItem("Select Sub Menu 3", "NULL"))
            ddlSubMenu3.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlMainMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMainMenu.SelectedIndexChanged
        Try

            If ddlMainMenu.SelectedValue <> "All" Then
                Sub1MenuInfo_List()
                displayStatusSubMenu1.Visible = True
                displayStatusSubMenu2.Visible = False
                displayStatusSubMenu3.Visible = False
                displayButtonFunction1.Visible = False
                displayButtonFunction2.Visible = False

            Else
                displayStatusSubMenu1.Visible = False
                displayStatusSubMenu2.Visible = False
                displayStatusSubMenu3.Visible = False
                displayButtonFunction1.Visible = False
                displayButtonFunction2.Visible = False
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSubMenu1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubMenu1.SelectedIndexChanged
        Try

            displayStatusSubMenu2.Visible = False
            displayStatusSubMenu3.Visible = False
            displayButtonFunction1.Visible = False
            displayButtonFunction2.Visible = False

            F1_R1.Visible = False
            F1_R1_C1_P1.Visible = False
            F1_R1_C1_P2.Visible = False
            F1_R1_C1_P3.Visible = False
            F1_R1_C1_P4.Visible = False

            F1_R2.Visible = False
            F1_R2_C2_P1.Visible = False
            F1_R2_C2_P2.Visible = False
            F1_R2_C2_P3.Visible = False
            F1_R2_C2_P4.Visible = False

            F1_R3.Visible = False
            F1_R3_C3_P1.Visible = False
            F1_R3_C3_P2.Visible = False
            F1_R3_C3_P3.Visible = False

            F2_R1.Visible = False
            F2_R1_C1_P1.Visible = False
            F2_R1_C1_P2.Visible = False
            F2_R1_C1_P3.Visible = False

            If ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Student Management" Then
                displayButtonFunction1.Visible = True
                F1_R2.Visible = True
                F1_R2_C2_P3.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Class & Course Placement" Then
                displayButtonFunction1.Visible = True
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Hostel" And ddlSubMenu1.SelectedValue = "Student Placement" Then
                displayButtonFunction1.Visible = True
                F1_R1.Visible = True
                F1_R1_C1_P3.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Hostel" And ddlSubMenu1.SelectedValue = "View Hostel Information" Then
                displayButtonFunction1.Visible = True
                F1_R1.Visible = True
                F1_R1_C1_P4.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "General Management" And ddlSubMenu1.SelectedValue = "Grade Management" Then
                displayButtonFunction1.Visible = True
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Self Development" Then
                displayButtonFunction1.Visible = True
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Personality Development" Then
                displayButtonFunction1.Visible = True
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Portfolio" Then
                displayButtonFunction1.Visible = True
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Setting" And ddlSubMenu1.SelectedValue = "User Access" Then
                displayButtonFunction1.Visible = True
                F1_R1.Visible = True
                F1_R1_C1_P3.Visible = True

            Else
                strSQL = "Select distinct Menu_Sub2 from menu_master_access where Menu = '" & ddlMainMenu.SelectedValue & "' and Menu_Sub1 = '" & ddlSubMenu1.SelectedValue & "'  and Menu_Sub2 is not null and Menu_Sub2 <> '' order by Menu_Sub2 ASC"
                strRet = oCommon.getFieldValue(strSQL)

                If strRet.Length > 0 Then
                    Sub2MenuInfo_List()
                    displayStatusSubMenu2.Visible = True
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSubMenu2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubMenu2.SelectedIndexChanged
        Try

            displayStatusSubMenu3.Visible = False
            displayButtonFunction1.Visible = False
            displayButtonFunction2.Visible = False

            F1_R1.Visible = False
            F1_R1_C1_P1.Visible = False
            F1_R1_C1_P2.Visible = False
            F1_R1_C1_P3.Visible = False
            F1_R1_C1_P4.Visible = False

            F1_R2.Visible = False
            F1_R2_C2_P1.Visible = False
            F1_R2_C2_P2.Visible = False
            F1_R2_C2_P3.Visible = False
            F1_R2_C2_P4.Visible = False

            F1_R3.Visible = False
            F1_R3_C3_P1.Visible = False
            F1_R3_C3_P2.Visible = False
            F1_R3_C3_P3.Visible = False

            F2_R1.Visible = False
            F2_R1_C1_P1.Visible = False
            F2_R1_C1_P2.Visible = False
            F2_R1_C1_P3.Visible = False

            If ddlMainMenu.SelectedValue = "General Management" And ddlSubMenu1.SelectedValue = "Examination Management" And ddlSubMenu2.SelectedValue = "Register Examination" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "General Management" And ddlSubMenu1.SelectedValue = "Examination Management" And ddlSubMenu2.SelectedValue = "View Examination" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Course Management" And ddlSubMenu2.SelectedValue = "View Course" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Course Management" And ddlSubMenu2.SelectedValue = "Register Course" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Course Management" And ddlSubMenu2.SelectedValue = "Transfer Course" Then
                F1_R2.Visible = True
                F1_R2_C2_P3.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Class Management" And ddlSubMenu2.SelectedValue = "View Class" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Class Management" And ddlSubMenu2.SelectedValue = "Register Class" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Class Management" And ddlSubMenu2.SelectedValue = "Transfer Class" Then
                F1_R2.Visible = True
                F1_R2_C2_P3.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Search Student" And ddlSubMenu2.SelectedValue = "View Student" Then
                F1_R1.Visible = True
                F1_R1_C1_P1.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Search Student" And ddlSubMenu2.SelectedValue = "Register Student" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Search Student" And ddlSubMenu2.SelectedValue = "Import Student" Then
                F1_R2.Visible = True
                F1_R2_C2_P2.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Attendance" And ddlSubMenu2.SelectedValue = "Update Attendance" Then
                F1_R1.Visible = True
                F1_R1_C1_P3.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "View Information" And ddlSubMenu2.SelectedValue = "View Course" Then
                F1_R2.Visible = True
                F1_R2_C2_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "View Information" And ddlSubMenu2.SelectedValue = "View Class" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "View Information" And ddlSubMenu2.SelectedValue = "View Religion" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Staff" And ddlSubMenu1.SelectedValue = "Search Staff" And ddlSubMenu2.SelectedValue = "View Staff" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Staff" And ddlSubMenu1.SelectedValue = "Search Staff" And ddlSubMenu2.SelectedValue = "Register Staff" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Staff" And ddlSubMenu1.SelectedValue = "Search Staff" And ddlSubMenu2.SelectedValue = "Import Staff" Then
                F1_R2.Visible = True
                F1_R2_C2_P2.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Staff" And ddlSubMenu1.SelectedValue = "Course Placement" And ddlSubMenu2.SelectedValue = "Register Staff Course" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Coordinator" And ddlSubMenu1.SelectedValue = "Search Coordinator" And ddlSubMenu2.SelectedValue = "View Coordinator" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Coordinator" And ddlSubMenu1.SelectedValue = "Search Coordinator" And ddlSubMenu2.SelectedValue = "Register Coordinator" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Discipline" And ddlSubMenu1.SelectedValue = "Discipline Management" And ddlSubMenu2.SelectedValue = "Register Discipline" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Discipline" And ddlSubMenu1.SelectedValue = "Discipline Management" And ddlSubMenu2.SelectedValue = "View Discipline" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Discipline" And ddlSubMenu1.SelectedValue = "Case Management" And ddlSubMenu2.SelectedValue = "Register Case" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Discipline" And ddlSubMenu1.SelectedValue = "Case Management" And ddlSubMenu2.SelectedValue = "View Case" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Counselor Management" And ddlSubMenu2.SelectedValue = "View Management" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Counselor Management" And ddlSubMenu2.SelectedValue = "Self Development Management" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Counselor Management" And ddlSubMenu2.SelectedValue = "Personality Development Management" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Counselling Activity" And ddlSubMenu2.SelectedValue = "Student Counselor" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Counselling Activity" And ddlSubMenu2.SelectedValue = "View Counselor Report" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                F1_R1.Visible = True
                F1_R1_C1_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Scholarship Management" And ddlSubMenu2.SelectedValue = "List Scholarship" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Counselor" And ddlSubMenu1.SelectedValue = "Scholarship Management" And ddlSubMenu2.SelectedValue = "Student Scholarship" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                F1_R1.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Research" And ddlSubMenu1.SelectedValue = "Register Student/Supervisor" And ddlSubMenu2.SelectedValue = "View Student Group & Supervisor" Then
                F1_R1.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Research" And ddlSubMenu1.SelectedValue = "Register Student/Supervisor" And ddlSubMenu2.SelectedValue = "Register Student Group & Supervisor" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Research" And ddlSubMenu1.SelectedValue = "Register Project/Field" And ddlSubMenu2.SelectedValue = "View Student Project" Then
                F1_R1.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Research" And ddlSubMenu1.SelectedValue = "Register Project/Field" And ddlSubMenu2.SelectedValue = "Register Student Project" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Research" And ddlSubMenu1.SelectedValue = "Register Mentor" And ddlSubMenu2.SelectedValue = "View Student Mentor" Then
                F1_R1.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Research" And ddlSubMenu1.SelectedValue = "Register Mentor" And ddlSubMenu2.SelectedValue = "Register Student Mentor" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Examination" And ddlSubMenu1.SelectedValue = "Examination Result" And ddlSubMenu2.SelectedValue = "Academic Result" Then
                F1_R1.Visible = True
                F1_R1_C1_P3.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Examination" And ddlSubMenu1.SelectedValue = "Examination Transcript" And ddlSubMenu2.SelectedValue = "Current Examination Transcript" Then
                F1_R3.Visible = True
                F1_R3_C3_P1.Visible = True
                F1_R3_C3_P2.Visible = True
                F1_R3_C3_P3.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Examination" And ddlSubMenu1.SelectedValue = "Examination Transcript" And ddlSubMenu2.SelectedValue = "Official Transcript" Then
                F1_R3.Visible = True
                F1_R3_C3_P2.Visible = True
                F1_R3_C3_P3.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Examination" And ddlSubMenu1.SelectedValue = "Import Examination" And ddlSubMenu2.SelectedValue = "Import Examination Result" Then
                F1_R2.Visible = True
                F1_R2_C2_P2.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Examination" And ddlSubMenu1.SelectedValue = "Import Examination" And ddlSubMenu2.SelectedValue = "Import GPA & CGPA" Then
                F1_R2.Visible = True
                F1_R2_C2_P2.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Hostel" And ddlSubMenu1.SelectedValue = "Hostel Management" And ddlSubMenu2.SelectedValue = "Register Hostel" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Hostel" And ddlSubMenu1.SelectedValue = "Hostel Management" And ddlSubMenu2.SelectedValue = "View Hostel" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Setting" And ddlSubMenu1.SelectedValue = "User Configuration" And ddlSubMenu2.SelectedValue = "Register User Access" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Setting" And ddlSubMenu1.SelectedValue = "System Configuration" And ddlSubMenu2.SelectedValue = "View System Configuration" Then
                F1_R1.Visible = True
                F1_R1_C1_P2.Visible = True
                F1_R1_C1_P4.Visible = True
                displayButtonFunction1.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Setting" And ddlSubMenu1.SelectedValue = "System Configuration" And ddlSubMenu2.SelectedValue = "Register System Configuration" Then
                F1_R2.Visible = True
                F1_R2_C2_P1.Visible = True
                displayButtonFunction1.Visible = True

            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSubMenu3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubMenu3.SelectedIndexChanged
        Try

            displayButtonFunction2.Visible = False

            F2_R1.Visible = False
            F2_R1_C1_P1.Visible = False
            F2_R1_C1_P2.Visible = False
            F2_R1_C1_P3.Visible = False
            F2_R1_C1_P4.Visible = False

            If ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Search Student" And ddlSubMenu2.SelectedValue = "View Student" And ddlSubMenu3.SelectedValue = "Student Information" Then
                F2_R1.Visible = True
                F2_R1_C1_P3.Visible = True
                displayButtonFunction2.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Search Student" And ddlSubMenu2.SelectedValue = "View Student" And ddlSubMenu3.SelectedValue = "Family Information" Then
                F2_R1.Visible = True
                F2_R1_C1_P3.Visible = True
                displayButtonFunction2.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Search Student" And ddlSubMenu2.SelectedValue = "View Student" And ddlSubMenu3.SelectedValue = "Course Information" Then
                F2_R1.Visible = True
                F2_R1_C1_P1.Visible = True
                F2_R1_C1_P2.Visible = True
                displayButtonFunction2.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Search Student" And ddlSubMenu2.SelectedValue = "View Student" And ddlSubMenu3.SelectedValue = "Reference Information" Then
                F2_R1.Visible = True
                F2_R1_C1_P2.Visible = True
                F2_R1_C1_P4.Visible = True
                F2_R1_C1_P5.Visible = True
                displayButtonFunction2.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Staff" And ddlSubMenu1.SelectedValue = "Search Staff" And ddlSubMenu2.SelectedValue = "View Staff" And ddlSubMenu3.SelectedValue = "Staff Information" Then
                F2_R1.Visible = True
                F2_R1_C1_P3.Visible = True
                displayButtonFunction2.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Staff" And ddlSubMenu1.SelectedValue = "Search Staff" And ddlSubMenu2.SelectedValue = "View Staff" And ddlSubMenu3.SelectedValue = "Course Information" Then
                F2_R1.Visible = True
                F2_R1_C1_P2.Visible = True
                displayButtonFunction2.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Hostel" And ddlSubMenu1.SelectedValue = "Hostel Management" And ddlSubMenu2.SelectedValue = "View Hostel" And ddlSubMenu3.SelectedValue = "Edit Hostel Information" Then
                F2_R1.Visible = True
                F2_R1_C1_P3.Visible = True
                displayButtonFunction2.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Hostel" And ddlSubMenu1.SelectedValue = "Hostel Management" And ddlSubMenu2.SelectedValue = "View Hostel" And ddlSubMenu3.SelectedValue = "Edit Room Information" Then
                F2_R1.Visible = True
                F2_R1_C1_P1.Visible = True
                F2_R1_C1_P2.Visible = True
                displayButtonFunction2.Visible = True

            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub check_ViewButton_F1_CheckedChanged(sender As Object, e As EventArgs) Handles check_ViewButton_F1.CheckedChanged
        Try

            If ddlMainMenu.SelectedValue = "Student" And ddlSubMenu1.SelectedValue = "Search Student" And ddlSubMenu2.SelectedValue = "View Student" And check_ViewButton_F1.Checked = True Then
                Sub3MenuInfo_List()
                displayStatusSubMenu3.Visible = True
            Else
                displayStatusSubMenu3.Visible = False
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub check_EditButton_F1_CheckedChanged(sender As Object, e As EventArgs) Handles check_EditButton_F1.CheckedChanged
        Try

            If ddlMainMenu.SelectedValue = "Staff" And ddlSubMenu1.SelectedValue = "Search Staff" And ddlSubMenu2.SelectedValue = "View Staff" And check_EditButton_F1.Checked = True Then
                Sub3MenuInfo_List()
                displayStatusSubMenu3.Visible = True

            ElseIf ddlMainMenu.SelectedValue = "Hostel" And ddlSubMenu1.SelectedValue = "Hostel Management" And ddlSubMenu2.SelectedValue = "View Hostel" And check_EditButton_F1.Checked = True Then
                Sub3MenuInfo_List()
                displayStatusSubMenu3.Visible = True

            Else
                displayStatusSubMenu3.Visible = False

            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlStaffName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStaffName.SelectedIndexChanged
        Try
            UserPosition_List()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSaveRegisterUserAccess_ServerClick(sender As Object, e As EventArgs) Handles btnSaveRegisterUserAccess.ServerClick

        Dim userposition_list As String = ""
        Dim checkmenuaccess_list As String = ""

        Dim Data_F1View As String = "FALSE"
        Dim Data_F1Edit As String = "FALSE"
        Dim Data_F1Update As String = "FALSE"
        Dim Data_F1Delete As String = "FALSE"
        Dim Data_F1Register As String = "FALSE"
        Dim Data_F1Import As String = "FALSE"
        Dim Data_F1Transfer As String = "FALSE"
        Dim Data_F1AddADropoutStudent As String = "FALSE"
        Dim Data_F1GenerateGPACGPA As String = "FALSE"
        Dim Data_F1PrintInBi As String = "FALSE"
        Dim Data_F1PrintInBm As String = "FALSE"
        Dim Data_F2Edit As String = "FALSE"
        Dim Data_F2Delete As String = "FALSE"
        Dim Data_F2Update As String = "FALSE"
        Dim Data_F2Download As String = "FALSE"
        Dim Data_F2View As String = "FALSE"

        Dim ErrorData As Integer = 0

        If ddlStaffName.SelectedIndex > 0 Then

            If ddlStaffPosition.SelectedIndex > 0 Then

                If ddlMainMenu.SelectedIndex > 0 And ddlMainMenu.SelectedValue = "All" Then

                    ''Set A New Data STATUS
                    Data_F1View = "TRUE"
                    Data_F1Edit = "TRUE"
                    Data_F1Update = "TRUE"
                    Data_F1Delete = "TRUE"
                    Data_F1Register = "TRUE"
                    Data_F1Import = "TRUE"
                    Data_F1Transfer = "TRUE"
                    Data_F1AddADropoutStudent = "TRUE"
                    Data_F1GenerateGPACGPA = "TRUE"
                    Data_F1PrintInBi = "TRUE"
                    Data_F1PrintInBm = "TRUE"
                    Data_F2Edit = "TRUE"
                    Data_F2Delete = "TRUE"
                    Data_F2Update = "TRUE"
                    Data_F2Download = "TRUE"
                    Data_F2View = "TRUE"
                    ErrorData = 2

                ElseIf ddlMainMenu.SelectedIndex > 0 And ddlMainMenu.SelectedValue <> "All" Then

                    ''FIRST SUB MENU RULE
                    If ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Grade Management" And ddlMainMenu.SelectedValue = "General Management" Then

                        ErrorData = 2
                        If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Edit = "TRUE"
                        End If

                        If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Delete = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Student Management" And ddlMainMenu.SelectedValue = "Student" Then

                        ErrorData = 2
                        If check_TransferButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Transfer = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Class & Course Placement" And ddlMainMenu.SelectedValue = "Student" Then

                        ErrorData = 2
                        If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Register = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Import Examination" And ddlMainMenu.SelectedValue = "Examination" Then

                        ErrorData = 2
                        If check_ImportButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Import = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Self Development" And ddlMainMenu.SelectedValue = "Counselor" Then

                        ErrorData = 2
                        If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Register = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Personality Development" And ddlMainMenu.SelectedValue = "Counselor" Then

                        ErrorData = 2
                        If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Register = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Portfolio" And ddlMainMenu.SelectedValue = "Counselor" Then

                        ErrorData = 2
                        If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Register = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Student Placement" And ddlMainMenu.SelectedValue = "Hostel" Then

                        ErrorData = 2
                        If check_UpdateButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Update = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "View Hostel Information" And ddlMainMenu.SelectedValue = "Hostel" Then

                        ErrorData = 2
                        If check_UpdateButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Delete = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "Co-Curriculum Management" And ddlMainMenu.SelectedValue = "Co-Curriculum" Then

                        ErrorData = 2 ''Set A New Data STATUS

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlMainMenu.SelectedValue = "Report" Then

                        ErrorData = 2 ''Set A New Data STATUS

                    ElseIf ddlSubMenu1.SelectedIndex > 0 And ddlSubMenu1.SelectedValue = "User Access" And ddlMainMenu.SelectedValue = "Setting" Then

                        ErrorData = 2
                        If check_UpdateButton_F1.Checked = True Then ''Set A New Data STATUS
                            Data_F1Update = "TRUE"
                        End If

                    ElseIf ddlSubMenu1.SelectedIndex = 0 Then

                        ErrorData = 0
                        ShowMessage(" Please Select Sub Menu 1 ", MessageType.Error)

                    Else

                        ''SECOND SUB MENU RULE
                        If ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Examination" And ddlSubMenu1.SelectedValue = "Examination Management" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Examination" And ddlSubMenu1.SelectedValue = "Examination Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Course" And ddlSubMenu1.SelectedValue = "Course Management" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Course" And ddlSubMenu1.SelectedValue = "Course Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Transfer Course" And ddlSubMenu1.SelectedValue = "Course Management" Then

                            ErrorData = 2
                            If check_TransferButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Transfer = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Class" And ddlSubMenu1.SelectedValue = "Class Management" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Class" And ddlSubMenu1.SelectedValue = "Class Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Transfer Class" And ddlSubMenu1.SelectedValue = "Class Management" Then

                            ErrorData = 2
                            If check_TransferButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Transfer = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Student" And ddlSubMenu1.SelectedValue = "Search Student" Then

                            ErrorData = 2
                            If check_ViewButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1View = "TRUE"

                                ''THIRD SUB MENU RULE FOR MENU STUDENT > SEARCH STUDENT > VIEW STUDENT
                                If ddlSubMenu3.SelectedIndex > 0 And ddlSubMenu3.SelectedValue = "Student Information" Then

                                    ErrorData = 2
                                    If check_UpdateButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Update = "TRUE"
                                    End If

                                ElseIf ddlSubMenu3.SelectedIndex > 0 And ddlSubMenu3.SelectedValue = "Family Information" Then

                                    ErrorData = 2
                                    If check_UpdateButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Update = "TRUE"
                                    End If

                                ElseIf ddlSubMenu3.SelectedIndex > 0 And ddlSubMenu3.SelectedValue = "Course Information" Then

                                    ErrorData = 2
                                    If check_EditButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Edit = "TRUE"
                                    End If

                                    If check_DeleteButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Delete = "TRUE"
                                    End If

                                ElseIf ddlSubMenu3.SelectedIndex > 0 And ddlSubMenu3.SelectedValue = "Reference Information" Then

                                    ErrorData = 2
                                    If check_ViewButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2View = "TRUE"
                                    End If

                                    If check_DownloadButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Download = "TRUE"
                                    End If

                                    If check_DeleteButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Download = "TRUE"
                                    End If

                                ElseIf ddlSubMenu3.SelectedIndex = 0 Then
                                    ErrorData = 0
                                    ShowMessage(" Please Select Sub Menu 3 ", MessageType.Error)
                                End If

                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Student" And ddlSubMenu1.SelectedValue = "Search Student" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Import Student" And ddlSubMenu1.SelectedValue = "Search Student" Then

                            ErrorData = 2
                            If check_ImportButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Import = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Update Attendance" And ddlSubMenu1.SelectedValue = "Attendance" Then

                            ErrorData = 2
                            If check_UpdateButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Update = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Course" And ddlSubMenu1.SelectedValue = "View Information" Then

                            ErrorData = 2
                            If check_DropoutButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1AddADropoutStudent = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Class" And ddlSubMenu1.SelectedValue = "View Information" Then

                            ErrorData = 2
                            If check_UpdateButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Update = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Religion" And ddlSubMenu1.SelectedValue = "View Information" Then

                            ErrorData = 2
                            If check_UpdateButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Update = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Staff" And ddlSubMenu1.SelectedValue = "Search Staff" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"

                                ''THIRD SUB MENU RULE FOR MENU STAFF > SEARCH STAFF > VIEW STAFF
                                If ddlSubMenu3.SelectedIndex > 0 And ddlSubMenu3.SelectedValue = "Staff Information" Then

                                    ErrorData = 2
                                    If check_UpdateButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Update = "TRUE"
                                    End If

                                ElseIf ddlSubMenu3.SelectedIndex > 0 And ddlSubMenu3.SelectedValue = "Course Information" Then

                                    ErrorData = 2
                                    If check_DeleteButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Delete = "TRUE"
                                    End If

                                ElseIf ddlSubMenu3.SelectedIndex = 0 Then
                                    ErrorData = 0
                                    ShowMessage(" Please Select Sub Menu 3 ", MessageType.Error)
                                End If

                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Staff" And ddlSubMenu1.SelectedValue = "Search Staff" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Import Staff" And ddlSubMenu1.SelectedValue = "Search Staff" Then

                            ErrorData = 2
                            If check_ImportButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Import = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Staff Course" And ddlSubMenu1.SelectedValue = "Course Placement" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Staff Course" And ddlSubMenu1.SelectedValue = "Course Placement" Then

                            ErrorData = 2

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Coordinator" And ddlSubMenu1.SelectedValue = "Search Coordinator" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Coordinator" And ddlSubMenu1.SelectedValue = "Search Coordinator" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Discipline" And ddlSubMenu1.SelectedValue = "Discipline Management" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Discipline" And ddlSubMenu1.SelectedValue = "Discipline Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Case" And ddlSubMenu1.SelectedValue = "Case Management" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Case" And ddlSubMenu1.SelectedValue = "Case Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Management" And ddlSubMenu1.SelectedValue = "Counselor Management" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Self Development Management" And ddlSubMenu1.SelectedValue = "Counselor Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Personality Development Management" And ddlSubMenu1.SelectedValue = "Counselor Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Student Counselor" And ddlSubMenu1.SelectedValue = "Counselling Activity" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Counselor Report" And ddlSubMenu1.SelectedValue = "Counselling Activity" Then

                            ErrorData = 2
                            If check_ViewButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1View = "TRUE"
                            End If

                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "List Scholarship" And ddlSubMenu1.SelectedValue = "Scholarship Management" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Student Scholarship" And ddlSubMenu1.SelectedValue = "Scholarship Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Student Group & Supervisor" And ddlSubMenu1.SelectedValue = "Register Student/Supervisor" Then

                            ErrorData = 2
                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Student Group & Supervisor" And ddlSubMenu1.SelectedValue = "Register Student/Supervisor" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Student Project" And ddlSubMenu1.SelectedValue = "Register Project/Field" Then

                            ErrorData = 2
                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Student Project" And ddlSubMenu1.SelectedValue = "Register Project/Field" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Student Mentor" And ddlSubMenu1.SelectedValue = "Register Mentor" Then

                            ErrorData = 2
                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Student Mentor" And ddlSubMenu1.SelectedValue = "Register Mentor" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Academic Result" And ddlSubMenu1.SelectedValue = "Examination Result" Then

                            ErrorData = 2
                            If check_UpdateButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Update = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Cocurriculum Result" And ddlSubMenu1.SelectedValue = "Examination Result" Then
                            ErrorData = 2

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Current Examination Transcript" And ddlSubMenu1.SelectedValue = "Examination Transcript" Then

                            ErrorData = 2
                            If check_GenerateButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1GenerateGPACGPA = "TRUE"
                            End If

                            If check_PrintBIButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1PrintInBi = "TRUE"
                            End If

                            If check_PrintBMButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1PrintInBm = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Official Transcript" And ddlSubMenu1.SelectedValue = "Examination Transcript" Then

                            ErrorData = 2
                            If check_PrintBIButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1PrintInBi = "TRUE"
                            End If

                            If check_PrintBMButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1PrintInBm = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register Hostel" And ddlSubMenu1.SelectedValue = "Hostel Management" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View Hostel" And ddlSubMenu1.SelectedValue = "Hostel Management" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"

                                ''THIRD SUB MENU RULE FOR MENU STAFF > SEARCH STAFF > VIEW STAFF
                                If ddlSubMenu3.SelectedIndex > 0 And ddlSubMenu3.SelectedValue = "Edit Hostel Information" Then

                                    ErrorData = 2
                                    If check_UpdateButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Update = "TRUE"
                                    End If

                                ElseIf ddlSubMenu3.SelectedIndex > 0 And ddlSubMenu3.SelectedValue = "Edit Room Information" Then

                                    ErrorData = 2
                                    If check_EditButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Edit = "TRUE"
                                    End If

                                    If check_DeleteButton_F2.Checked = True Then ''Set A New Data STATUS
                                        Data_F2Delete = "TRUE"
                                    End If

                                ElseIf ddlSubMenu3.SelectedIndex = 0 Then
                                    ErrorData = 0
                                    ShowMessage(" Please Select Sub Menu 3 ", MessageType.Error)
                                End If

                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register User Access" And ddlSubMenu1.SelectedValue = "User Configuration" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Register = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "Register System Configuration" And ddlSubMenu1.SelectedValue = "System Configuration" Then

                            ErrorData = 2
                            If check_RegisterButton_F1.Checked = True Then ''Set A New Data STATUS
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex > 0 And ddlSubMenu2.SelectedValue = "View System Configuration" And ddlSubMenu1.SelectedValue = "System Configuration" Then

                            ErrorData = 2
                            If check_EditButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Edit = "TRUE"
                            End If

                            If check_DeleteButton_F1.Checked = True Then ''Set A New Data STATUS
                                Data_F1Delete = "TRUE"
                            End If

                        ElseIf ddlSubMenu2.SelectedIndex = 0 Then
                            ErrorData = 0
                            ShowMessage(" Please Select Sub Menu 2 ", MessageType.Error)
                        End If

                    End If
                Else
                    ErrorData = 0
                    ShowMessage(" Please Select Menu ", MessageType.Error)
                End If
            Else
                ErrorData = 0
                ShowMessage(" Please Select User Position ", MessageType.Error)
            End If
        Else
            ErrorData = 0
            ShowMessage(" Please Select User ", MessageType.Error)
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If ErrorData = 2 Then

            ''Get User Position 
            strSQL = "Select login_ID from staff_Login where stf_ID = '" & ddlStaffName.SelectedValue & "' and staff_Access = '" & ddlStaffPosition.SelectedValue & "' and staff_Status = 'Access'"
            userposition_list = oCommon.getFieldValue(strSQL)

            If ddlMainMenu.SelectedIndex > 0 Then
                strSQL = "Select MenuID from menu_master_access where Menu = '" & ddlMainMenu.SelectedValue & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            If ddlSubMenu1.SelectedIndex > 0 Then
                strSQL = "Select MenuID from menu_master_access where Menu = '" & ddlMainMenu.SelectedValue & "' and Menu_Sub1 = '" & ddlSubMenu1.SelectedValue & "' "
                strRet = oCommon.getFieldValue(strSQL)
            End If

            If ddlSubMenu2.SelectedIndex > 0 Then
                strSQL = "Select MenuID from menu_master_access where Menu = '" & ddlMainMenu.SelectedValue & "' and Menu_Sub1 = '" & ddlSubMenu1.SelectedValue & "' and Menu_Sub2 = '" & ddlSubMenu2.SelectedValue & "' "
                strRet = oCommon.getFieldValue(strSQL)
            End If

            If ddlSubMenu3.SelectedIndex > 0 Then
                strSQL = "Select MenuID from menu_master_access where Menu = '" & ddlMainMenu.SelectedValue & "' and Menu_Sub1 = '" & ddlSubMenu1.SelectedValue & "' and Menu_Sub2 = '" & ddlSubMenu2.SelectedValue & "' and Menu_Sub3 = '" & ddlSubMenu3.SelectedValue & "'"
                strRet = oCommon.getFieldValue(strSQL)
            End If

            ''Check User Data If Exist
            strSQL = "Select ID from menu_master_user where stf_ID = '" & ddlStaffName.SelectedValue & "' and login_ID = '" & userposition_list & "' and MenuID = '" & strRet & "'"
            checkmenuaccess_list = oCommon.getFieldValue(strSQL)

            If checkmenuaccess_list.Length > 0 Then

                ''Update Data
                strSQL = "  Update menu_master_user set
                            stf_ID = '" & ddlStaffName.SelectedValue & "', login_ID = '" & userposition_list & "', MenuID = '" & strRet & "', F1_View = '" & Data_F1View & "', F1_Edit = '" & Data_F1Edit & "', F1_Update = '" & Data_F1Update & "', F1_Delete = '" & Data_F1Delete & "',
                            F1_Register = '" & Data_F1Register & "', F1_Import = '" & Data_F1Import & "', F1_Transfer = '" & Data_F1Transfer & "', F1_AddADropoutStudent = '" & Data_F1AddADropoutStudent & "', F1_GenerateGPACGPA = '" & Data_F1GenerateGPACGPA & "', F1_PrintInBI = '" & Data_F1PrintInBi & "', F1_PrintInBM = '" & Data_F1PrintInBm & "', F2_Edit = '" & Data_F2Edit & "', F2_Delete = '" & Data_F2Delete & "', F2_Update = '" & Data_F2Update & "', F2_Download = '" & Data_F2Download & "', F2_View = '" & Data_F2View & "' 
                            where ID = '" & checkmenuaccess_list & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    ShowMessage(" Update User Access Menu ", MessageType.Success)
                Else
                    ShowMessage(" Unsuccessful Update User Access Menu ", MessageType.Error)
                End If
            Else

                ''Insert New Data
                strSQL = "  Insert into menu_master_user (stf_ID,login_ID,MenuID,F1_View,F1_Edit,F1_Update,F1_Delete,F1_Register,F1_Import,F1_Transfer,F1_AddADropoutStudent,F1_GenerateGPACGPA,F1_PrintInBI,F1_PrintInBM,F2_Edit,F2_Delete,F2_Update,F2_Download,F2_View)
                            Values ('" & ddlStaffName.SelectedValue & "','" & userposition_list & "','" & strRet & "','" & Data_F1View & "','" & Data_F1Edit & "','" & Data_F1Update & "','" & Data_F1Delete & "','" & Data_F1Register & "','" & Data_F1Import & "','" & Data_F1Transfer & "','" & Data_F1AddADropoutStudent & "','" & Data_F1GenerateGPACGPA & "','" & Data_F1PrintInBi & "','" & Data_F1PrintInBm & "','" & Data_F2Edit & "','" & Data_F2Delete & "','" & Data_F2Update & "','" & Data_F2Download & "','" & Data_F2View & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    ShowMessage(" Register User Access Menu ", MessageType.Success)
                Else
                    ShowMessage(" Unsuccessful Register User Access Menu ", MessageType.Error)
                End If
            End If

        End If

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Protected Sub ddl_ViewStaffName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_ViewStaffName.SelectedIndexChanged
        Try
            View_UserPosition_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_ViewStaffPosition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_ViewStaffPosition.SelectedIndexChanged
        Try
            collapse_Menu_GeneralManagement.Visible = False
            collapse_Menu_Student.Visible = False
            collapse_Menu_Staff.Visible = False
            collapse_Menu_Coordinator.Visible = False
            collapse_Menu_Discipline.Visible = False
            collapse_Menu_Counselor.Visible = False
            collapse_Menu_Research.Visible = False
            collapse_Menu_Examination.Visible = False
            collapse_Menu_Hostel.Visible = False
            collapse_Menu_Cocurricular.Visible = False
            collapse_Menu_Report.Visible = False
            collapse_Menu_Setting.Visible = False

            ''LIST OF ALL MAIN MENU
            MENU_GM.Visible = False
            MENU_STD.Visible = False
            MENU_STF.Visible = False
            MENU_COO.Visible = False
            MENU_DISC.Visible = False
            MENU_COUN.Visible = False
            MENU_RES.Visible = False
            MENU_EXAM.Visible = False
            MENU_HST.Visible = False
            MENU_CC.Visible = False
            MENU_RPT.Visible = False
            MENU_SET.Visible = False

            ''LIST OF ALL SUB MENU 1
            MENU_GM_EM.Visible = False
            MENU_GM_GM.Visible = False
            MENU_GM_AM.Visible = False
            MENU_STD_COURSEM.Visible = False
            MENU_STD_CLASSM.Visible = False
            MENU_STD_SM.Visible = False
            MENU_STD_SS.Visible = False
            MENU_STD_CCP.Visible = False
            MENU_STD_ATT.Visible = False
            MENU_STD_VI.Visible = False
            MENU_STF_SS.Visible = False
            MENU_STF_CP.Visible = False
            MENU_COO_SC.Visible = False
            MENU_DISC_DM.Visible = False
            MENU_DISC_CM.Visible = False
            MENU_COUN_CM.Visible = False
            MENU_COUN_SDM.Visible = False
            MENU_COUN_PDM.Visible = False
            MENU_COUN_POR.Visible = False
            MENU_COUN_CA.Visible = False
            MENU_COUN_SM.Visible = False
            MENU_RES_RSS.Visible = False
            MENU_RES_RPF.Visible = False
            MENU_RES_RM.Visible = False
            MENU_EXAM_ER.Visible = False
            MENU_EXAM_ET.Visible = False
            MENU_EXAM_IE.Visible = False
            MENU_HST_HM.Visible = False
            MENU_HST_SP.Visible = False
            MENU_HST_VHI.Visible = False
            MENU_CC_CCM.Visible = False
            MENU_RPT_ER.Visible = False
            MENU_RPT_CLASSER.Visible = False
            MENU_RPT_COURSESER.Visible = False
            MENU_RPT_SRL.Visible = False
            MENU_RPT_AR.Visible = False
            MENU_RPT_FR.Visible = False
            MENU_SET_UC.Visible = False
            MENU_SET_SC.Visible = False
            MENU_SET_UA.Visible = False

            ''LIST OF ALL SUB MENU 2
            MENU_GM_EM_VE.Visible = False
            MENU_GM_EM_RE.Visible = False
            MENU_STD_COURSEM_VC.Visible = False
            MENU_STD_COURSEM_RC.Visible = False
            MENU_STD_COURSEM_TC.Visible = False
            MENU_STD_CLASSM_VC.Visible = False
            MENU_STD_CLASSM_RC.Visible = False
            MENU_STD_CLASSM_TC.Visible = False
            MENU_STD_SS_VS.Visible = False
            MENU_STD_SS_RS.Visible = False
            MENU_STD_SS_IS.Visible = False
            MENU_STD_ATT_VA.Visible = False
            MENU_STD_ATT_UA.Visible = False
            MENU_STD_VI_VCOURSE.Visible = False
            MENU_STD_VI_VCLASS.Visible = False
            MENU_STD_VIVCOCURRICULUM.Visible = False
            MENU_STD_VI_VH.Visible = False
            MENU_STD_VI_VR.Visible = False
            MENU_STF_SS_VS.Visible = False
            MENU_STF_SS_RS.Visible = False
            MENU_STF_SS_IS.Visible = False
            MENU_STF_CP_VSC.Visible = False
            MENU_STF_CP_RSC.Visible = False
            MENU_COO_SC_VC.Visible = False
            MENU_COO_SC_RC.Visible = False
            MENU_DISC_DM_VD.Visible = False
            MENU_DISC_DM_RD.Visible = False
            MENU_DISC_CM_VC.Visible = False
            MENU_DISC_CM_RC.Visible = False
            MENU_COUN_CM_VM.Visible = False
            MENU_COUN_CM_SDM.Visible = False
            MENU_COUN_CM_PDM.Visible = False
            MENU_COUN_CA_SC.Visible = False
            MENU_COUN_CA_VCR.Visible = False
            MENU_COUN_SM_LS.Visible = False
            MENU_COUN_SM_SS.Visible = False
            MENU_RES_RSS_VSGS.Visible = False
            MENU_RES_RSS_RSGS.Visible = False
            MENU_RES_RPF_VSP.Visible = False
            MENU_RES_RPF_RSP.Visible = False
            MENU_RES_RM_VSM.Visible = False
            MENU_RES_RM_RSN.Visible = False
            MENU_EXAM_ER_AR.Visible = False
            MENU_EXAM_ER_CR.Visible = False
            MENU_EXAM_ET_CET.Visible = False
            MENU_EXAM_ET_OT.Visible = False
            MENU_HST_HM_VH.Visible = False
            MENU_HST_HM_RH.Visible = False
            MENU_EXAM_IE_IER.Visible = False
            MENU_EXAM_IE_IGC.Visible = False
            MENU_SET_UC_VUA.Visible = False
            MENU_SET_UC_RUA.Visible = False
            MENU_SET_SC_VSC.Visible = False
            MENU_SET_SC_RSC.Visible = False

            ''LIST OF ALL SUB MENU 3
            MENU_STD_SS_VS_VB_SI.Visible = False
            MENU_STD_SS_VS_VB_FI.Visible = False
            MENU_STD_SS_VS_VB_COURSEI.Visible = False
            MENU_STD_SS_VS_VB_COCURRICULARI.Visible = False
            MENU_STD_SS_VS_VB_EI.Visible = False
            MENU_STD_SS_VS_VB_HI.Visible = False
            MENU_STD_SS_VS_VB_DI.Visible = False
            MENU_STD_SS_VS_VB_UP.Visible = False
            MENU_STD_SS_VS_VB_RI.Visible = False
            MENU_STF_SS_VS_EB_SI.Visible = False
            MENU_STF_SS_VS_EB_CI.Visible = False
            MENU_HST_HM_VH_EB_EHI.Visible = False
            MENU_HST_HM_VH_EB_ERI.Visible = False

            ''LIST OF ALL BUTTON FUNCTION 1
            MENU_GM_EM_VE_EB.Visible = False
            MENU_GM_EM_VE_DB.Visible = False
            MENU_GM_EM_RE_RB.Visible = False
            MENU_GM_GM_EB.Visible = False
            MENU_GM_GM_DB.Visible = False
            MENU_STD_COURSEM_VC_EB.Visible = False
            MENU_STD_COURSEM_VC_DB.Visible = False
            MENU_STD_COURSEM_RC_RB.Visible = False
            MENU_STD_COURSEM_TC_TB.Visible = False
            MENU_STD_CLASSM_VC_EB.Visible = False
            MENU_STD_CLASSM_VC_DB.Visible = False
            MENU_STD_CLASSM_RC_RB.Visible = False
            MENU_STD_CLASSM_TC_TB.Visible = False
            MENU_STD_SM_TB.Visible = False
            MENU_STD_SS_VS_VB.Visible = False
            MENU_STD_SS_VS_DB.Visible = False
            MENU_STD_SS_RS_RB.Visible = False
            MENU_STD_SS_IS_IB.Visible = False
            MENU_STD_CCP_RB.Visible = False
            MENU_STD_ATT_UA_UB.Visible = False
            MENU_STD_VI_VCOURSE_AB.Visible = False
            MENU_STD_VI_VCLASS_UB.Visible = False
            MENU_STD_VI_VR_UB.Visible = False
            MENU_STF_SS_VS_EB.Visible = False
            MENU_STF_SS_VS_DB.Visible = False
            MENU_STF_SS_RS_RB.Visible = False
            MENU_STF_SS_IS_IB.Visible = False
            MENU_STF_CP_RSC_RB.Visible = False
            MENU_COO_SC_VC_EB.Visible = False
            MENU_COO_SC_VC_DB.Visible = False
            MENU_COO_SC_RC_RB.Visible = False
            MENU_DISC_DM_VD_EB.Visible = False
            MENU_DISC_DM_VD_DB.Visible = False
            MENU_DISC_DM_RD_RB.Visible = False
            MENU_DISC_CM_VC_EB.Visible = False
            MENU_DISC_CM_VC_DB.Visible = False
            MENU_DISC_CM_RC_RB.Visible = False
            MENU_COUN_CM_VM_EB.Visible = False
            MENU_COUN_CM_VM_DB.Visible = False
            MENU_COUN_CM_SDM_RB.Visible = False
            MENU_COUN_CM_PDM_RB.Visible = False
            MENU_COUN_SDM_RB.Visible = False
            MENU_COUN_PDM_RB.Visible = False
            MENU_COUN_POR_RB.Visible = False
            MENU_RES_RSS_VSGS_DB.Visible = False
            MENU_RES_RSS_RSGS_RB.Visible = False
            MENU_RES_RPF_VSP_DB.Visible = False
            MENU_RES_RPF_RSP_RB.Visible = False
            MENU_RES_RM_VSM_DB.Visible = False
            MENU_RES_RM_RSN_RB.Visible = False
            MENU_EXAM_ER_AR_UB.Visible = False
            MENU_EXAM_ET_CET_GEN.Visible = False
            MENU_EXAM_ET_CET_BI.Visible = False
            MENU_EXAM_ET_CET_BM.Visible = False
            MENU_EXAM_ET_OT_BI.Visible = False
            MENU_EXAM_ET_OT_BM.Visible = False
            MENU_EXAM_IE_IER_BI.Visible = False
            MENU_EXAM_IE_IGC_BI.Visible = False
            MENU_HST_HM_VH_EB.Visible = False
            MENU_HST_HM_VH_DB.Visible = False
            MENU_HST_HM_RH_RB.Visible = False
            MENU_HST_SP_UB.Visible = False
            MENU_HST_VHI_DB.Visible = False
            MENU_SET_UC_RUA_RB.Visible = False
            MENU_SET_SC_VSC_EB.Visible = False
            MENU_SET_SC_VSC_DB.Visible = False
            MENU_SET_SC_RSC_RB.Visible = False
            MENU_SET_UA_UB.Visible = False

            ''LIST OF ALL BUTTON FUNCTION 2
            MENU_STD_SS_VS_VB_SI_UB.Visible = False
            MENU_STD_SS_VS_VB_FI_UB.Visible = False
            MENU_STD_SS_VS_VB_COURSEI_EB.Visible = False
            MENU_STD_SS_VS_VB_COURSEI_DB.Visible = False
            MENU_STD_SS_VS_VB_RI_VB.Visible = False
            MENU_STD_SS_VS_VB_RI_DOWNLOADB.Visible = False
            MENU_STD_SS_VS_VB_RI_DB.Visible = False
            MENU_STF_SS_VS_EB_SI_UB.Visible = False
            MENU_STF_SS_VS_EB_CI_DB.Visible = False
            MENU_HST_HM_VH_EB_EHI_UB.Visible = False
            MENU_HST_HM_VH_EB_ERI_EB.Visible = False
            MENU_HST_HM_VH_EB_ERI_DB.Visible = False

            If ddl_ViewStaffPosition.SelectedIndex > 0 Then
                View_UserDataLoad()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub View_UserDataLoad()

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' and staff_Access = '" & ddl_ViewStaffPosition.SelectedValue & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Loop The Count_No
        For num As Integer = 0 To find_CountNo_LoginID - 1 Step 1

            ''Get Main Menu Data
            strSQL = "  Select A.Menu From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_Menu As String = oCommon.getFieldValue(strSQL)

            ''Get Sub Menu 1 Data
            strSQL = "  Select A.Menu_Sub1 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_SubMenu1 As String = oCommon.getFieldValue(strSQL)

            ''Get Sub Menu 2 Data
            strSQL = "  Select A.Menu_Sub2 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_SubMenu2 As String = oCommon.getFieldValue(strSQL)

            ''Get Sub Menu 3 Data
            strSQL = "  Select A.Menu_Sub3 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_SubMenu3 As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 View Data (1)
            strSQL = "  Select B.F1_View From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1View As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Edit Data (2)
            strSQL = "  Select B.F1_Edit From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Edit As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Update Data (3)
            strSQL = "  Select B.F1_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Update As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Delete Data (4)
            strSQL = "  Select B.F1_Delete From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Delete As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Register Data (5)
            strSQL = "  Select B.F1_Register From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Register As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Import Data (6)
            strSQL = "  Select B.F1_Import From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Import As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Transfer Data (7)
            strSQL = "  Select B.F1_Transfer From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Transfer As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 AddADropoutStudent Data (8)
            strSQL = "  Select B.F1_AddADropoutStudent From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1AddADropoutStudent As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Generate GPA CGPA Data (9)
            strSQL = "  Select B.F1_GenerateGPACGPA From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1GenerateGPACGPA As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Print In BI Data (10)
            strSQL = "  Select B.F1_PrintInBI From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1PrintInBI As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Print In BM Data (11)
            strSQL = "  Select B.F1_PrintInBM From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1PrintInBM As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Edit Data (12)
            strSQL = "  Select B.F2_Edit From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Edit As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Delete Data (13)
            strSQL = "  Select B.F2_Delete From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Delete As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Update Data (14)
            strSQL = "  Select B.F2_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Update As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Delete Data (15)
            strSQL = "  Select B.F2_Download From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2Download As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 2 Delete Data (16)
            strSQL = "  Select B.F2_View From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & ddl_ViewStaffName.SelectedValue & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F2View As String = oCommon.getFieldValue(strSQL)


            ''''' GENERAL MANAGEMENT MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "General Management" Then
                collapse_Menu_GeneralManagement.Visible = True
                MENU_GM.Visible = True

                ''''' GENERAL MANAGEMENT >> EXAMINATION MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Examination Management" Then
                    MENU_GM_EM.Visible = True

                    ''''' GENERAL MANAGEMENT >> EXAMINATION MANAGEMENT >> VIEW EXAMINATION '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Examination" Then
                        MENU_GM_EM_VE.Visible = True

                        ''''' GENERAL MANAGEMENT >> EXAMINATION MANAGEMENT >> VIEW EXAMINATION >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_GM_EM_VE_EB.Visible = True
                        End If

                        ''''' GENERAL MANAGEMENT >> EXAMINATION MANAGEMENT >> VIEW EXAMINATION >> Delete BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_GM_EM_VE_DB.Visible = True
                        End If
                    End If

                    ''''' GENERAL MANAGEMENT >> EXAMINATION MANAGEMENT >> REGISTER EXAMINATION '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Examination" Then
                        MENU_GM_EM_RE.Visible = True

                        ''''' GENERAL MANAGEMENT >> EXAMINATION MANAGEMENT >> REGISTER EXAMINATION >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_GM_EM_RE_RB.Visible = True
                        End If
                    End If
                End If

                ''''' GENERAL MANAGEMENT >> GRADE MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Grade Management" Then
                    MENU_GM_GM.Visible = True

                    ''''' GENERAL MANAGEMENT >> GRADE MANAGEMENT >> EDIT BUTTON '''''
                    If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                        MENU_GM_EM_VE_EB.Visible = True
                    End If

                    ''''' GENERAL MANAGEMENT >> GRADE MANAGEMENT >> Delete BUTTON '''''
                    If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                        MENU_GM_EM_VE_DB.Visible = True
                    End If
                End If

                ''''' GENERAL MANAGEMENT >> ASSESSMENT MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Assessment Management" Then
                    MENU_GM_AM.Visible = True
                End If
            End If

            ''''' STUDENT MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Student" Then
                collapse_Menu_Student.Visible = True
                MENU_STD.Visible = True

                ''''' STUDENT >> COURSE MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Course Management" Then
                    MENU_STD_COURSEM.Visible = True

                    ''''' STUDENT >> COURSE MANAGEMENT >> VIEW COURSE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Course" Then
                        MENU_STD_COURSEM_VC.Visible = True

                        ''''' STUDENT >> COURSE MANAGEMENT >> VIEW COURSE >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_STD_COURSEM_VC_EB.Visible = True
                        End If

                        ''''' STUDENT >> COURSE MANAGEMENT >> VIEW COURSE >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_STD_COURSEM_VC_DB.Visible = True
                        End If
                    End If

                    ''''' STUDENT >> COURSE MANAGEMENT >> REGISTER COURSE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Course" Then
                        MENU_STD_COURSEM_RC.Visible = True

                        ''''' STUDENT >> COURSE MANAGEMENT >> REGISTER COURSE >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_STD_COURSEM_RC_RB.Visible = True
                        End If
                    End If

                    ''''' STUDENT >> COURSE MANAGEMENT >> TRANSFER COURSE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Transfer Course" Then
                        MENU_STD_COURSEM_TC.Visible = True

                        ''''' STUDENT >> COURSE MANAGEMENT >> REGISTER COURSE >> TRANSFER BUTTON '''''
                        If find_Data_F1Transfer.Length > 0 And find_Data_F1Transfer = "TRUE" Then
                            MENU_STD_COURSEM_TC_TB.Visible = True
                        End If
                    End If
                End If

                ''''' STUDENT >> CLASS MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Class Management" Then
                    MENU_STD_CLASSM.Visible = True

                    ''''' STUDENT >> CLASS MANAGEMENT >> VIEW CLASS '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Class" Then
                        MENU_STD_CLASSM_VC.Visible = True

                        ''''' STUDENT >> CLASS MANAGEMENT >> VIEW CLASS >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_STD_CLASSM_VC_EB.Visible = True
                        End If

                        ''''' STUDENT >> CLASS MANAGEMENT >> VIEW CLASS >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_STD_CLASSM_VC_DB.Visible = True
                        End If
                    End If

                    ''''' STUDENT >> CLASS MANAGEMENT >> REGISTER CLASS '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Class" Then
                        MENU_STD_CLASSM_RC.Visible = True

                        ''''' STUDENT >> CLASS MANAGEMENT >> REGISTER CLASS >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_STD_CLASSM_RC_RB.Visible = True
                        End If
                    End If

                    ''''' STUDENT >> CLASS MANAGEMENT >> TRANSFER CLASS '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Transfer Class" Then
                        MENU_STD_CLASSM_TC.Visible = True

                        ''''' STUDENT >> CLASS MANAGEMENT >> REGISTER CLASS >> TRANSFER BUTTON '''''
                        If find_Data_F1Transfer.Length > 0 And find_Data_F1Transfer = "TRUE" Then
                            MENU_STD_CLASSM_TC_TB.Visible = True
                        End If
                    End If
                End If

                ''''' STUDENT >> STUDENT MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Student Management" Then
                    MENU_STD_SM.Visible = True

                    ''''' STUDENT >> STUDENT MANAGEMENT >> TRANSFER BUTTON '''''
                    If find_Data_F1Transfer.Length > 0 And find_Data_F1Transfer = "TRUE" Then
                        MENU_STD_SM_TB.Visible = True
                    End If
                End If

                ''''' STUDENT >> SEARCH STUDENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Search Student" Then
                    MENU_STD_SS.Visible = True

                    ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Student" Then
                        MENU_STD_SS_VS.Visible = True

                        ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON '''''
                        If find_Data_F1View.Length > 0 And find_Data_F1View = "TRUE" Then
                            MENU_STD_SS_VS_VB.Visible = True

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> STUDENT INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Student Information" Then
                                MENU_STD_SS_VS_VB_SI.Visible = True

                                ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> STUDENT INFORMATION >> UPDATE BUTTON '''''
                                If find_Data_F2Update.Length > 0 And find_Data_F2Update = "TRUE" Then
                                    MENU_STD_SS_VS_VB_SI_UB.Visible = True
                                End If
                            End If

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> FAMILY INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Family Information" Then
                                MENU_STD_SS_VS_VB_FI.Visible = True

                                ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> FAMILY INFORMATION >> UPDATE BUTTON '''''
                                If find_Data_F2Update.Length > 0 And find_Data_F2Update = "TRUE" Then
                                    MENU_STD_SS_VS_VB_FI_UB.Visible = True
                                End If
                            End If

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> COURSE INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Course Information" Then
                                MENU_STD_SS_VS_VB_COURSEI.Visible = True

                                ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> COURSE INFORMATION >> EDIT BUTTON '''''
                                If find_Data_F2Edit.Length > 0 And find_Data_F2Edit = "TRUE" Then
                                    MENU_STD_SS_VS_VB_COURSEI_EB.Visible = True
                                End If

                                ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> COURSE INFORMATION >> DELETE BUTTON '''''
                                If find_Data_F2Delete.Length > 0 And find_Data_F2Delete = "TRUE" Then
                                    MENU_STD_SS_VS_VB_COURSEI_DB.Visible = True
                                End If
                            End If

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> COCURRICULAR INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Cocurricular Information" Then
                                MENU_STD_SS_VS_VB_COCURRICULARI.Visible = True
                            End If

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> EXAMINATION INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Examination Information" Then
                                MENU_STD_SS_VS_VB_EI.Visible = True
                            End If

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> HOSTEL INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Hostel Information" Then
                                MENU_STD_SS_VS_VB_HI.Visible = True
                            End If

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> DISCPLINE INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Discipline Information" Then
                                MENU_STD_SS_VS_VB_DI.Visible = True
                            End If

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> UKM1 - PPCS '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "UKM1 - PPCS" Then
                                MENU_STD_SS_VS_VB_UP.Visible = True
                            End If

                            ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> REFERENCE INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Reference Information" Then
                                MENU_STD_SS_VS_VB_RI.Visible = True

                                ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> REFERENCE INFORMATION >> VIEW BUTTON '''''
                                If find_Data_F2View.Length > 0 And find_Data_F2View = "TRUE" Then
                                    MENU_STD_SS_VS_VB_RI_VB.Visible = True
                                End If

                                ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> REFERENCE INFORMATION >> DOWNLOAD BUTTON '''''
                                If find_Data_F2Download.Length > 0 And find_Data_F2Download = "TRUE" Then
                                    MENU_STD_SS_VS_VB_RI_DOWNLOADB.Visible = True
                                End If

                                ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> VIEW BUTTON >> REFERENCE INFORMATION >> DELETE BUTTON '''''
                                If find_Data_F2Delete.Length > 0 And find_Data_F2Delete = "TRUE" Then
                                    MENU_STD_SS_VS_VB_RI_DB.Visible = True
                                End If
                            End If
                        End If

                        ''''' STUDENT >> CLASS MANAGEMENT >> VIEW STUDENT >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_STD_SS_VS_DB.Visible = True
                        End If
                    End If

                    ''''' STUDENT >> SEARCH STUDENT >> REGISTER STDUENT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Student" Then
                        MENU_STD_SS_RS.Visible = True

                        ''''' STUDENT >> SEARCH STUDENT >> REGISTER STDUENT >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_STD_SS_RS_RB.Visible = True
                        End If
                    End If

                    ''''' STUDENT >> SEARCH STUDENT >> IMPORT STDUENT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Import Student" Then
                        MENU_STD_SS_IS.Visible = True

                        ''''' STUDENT >> SEARCH STUDENT >> IMPORT STDUENT >> IMPORT BUTTON '''''
                        If find_Data_F1Import.Length > 0 And find_Data_F1Import = "TRUE" Then
                            MENU_STD_SS_IS_IB.Visible = True
                        End If
                    End If
                End If

                ''''' STUDENT >> CLASS & COURSE PLACEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Class & Course Placement" Then
                    MENU_STD_CCP.Visible = True

                    ''''' STUDENT >> CLASS & COURSE PLACEMENT >> REGISTER BUTTON '''''
                    If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                        MENU_STD_CCP_RB.Visible = True
                    End If
                End If

                ''''' STUDENT >> ATTENDANCE '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Attendance" Then
                    MENU_STD_ATT.Visible = True

                    ''''' STUDENT >> ATTENDANCE >> VIEW ATTENDANCE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Attendance" Then
                        MENU_STD_ATT_VA.Visible = True
                    End If

                    ''''' STUDENT >> ATTENDANCE >> UPDATE ATTENDANCE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Update Attendance" Then
                        MENU_STD_ATT_UA.Visible = True

                        ''''' STUDENT >> ATTENDANCE >> UPDATE ATTENDANCE >> UPDATE BUTTON '''''
                        If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                            MENU_STD_ATT_UA_UB.Visible = True
                        End If
                    End If
                End If

                ''''' STUDENT >> VIEW INFORMATION '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "View Information" Then
                    MENU_STD_VI.Visible = True

                    ''''' STUDENT >> VIEW INFORMATION >> VIEW COURSE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Course" Then
                        MENU_STD_VI_VCOURSE.Visible = True

                        ''''' STUDENT >> VIEW INFORMATION >> VIEW CLASS >> ADD A DROPOUT STUDENT BUTTON '''''
                        If find_Data_F1AddADropoutStudent.Length > 0 And find_Data_F1AddADropoutStudent = "TRUE" Then
                            MENU_STD_VI_VCOURSE_AB.Visible = True
                        End If
                    End If

                    ''''' STUDENT >> VIEW INFORMATION >> VIEW CLASS '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Class" Then
                        MENU_STD_VI_VCLASS.Visible = True

                        ''''' STUDENT >> VIEW INFORMATION >> VIEW CLASS >> UPDATE BUTTON '''''
                        If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                            MENU_STD_VI_VCLASS_UB.Visible = True
                        End If
                    End If

                    ''''' STUDENT >> VIEW INFORMATION >> VIEW COCURRICULUM '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Cocurriculum" Then
                        MENU_STD_VIVCOCURRICULUM.Visible = True
                    End If

                    ''''' STUDENT >> VIEW INFORMATION >> VIEW HOSTEL '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Hostel" Then
                        MENU_STD_VI_VH.Visible = True
                    End If

                    ''''' STUDENT >> VIEW INFORMATION >> VIEW RELIGION '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Religion" Then
                        MENU_STD_VI_VR.Visible = True

                        ''''' STUDENT >> VIEW INFORMATION >> VIEW RELIGION >> UPDATE BUTTON '''''
                        If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                            MENU_STD_VI_VR_UB.Visible = True
                        End If
                    End If
                End If
            End If

            ''''' STAFF MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Staff" Then
                collapse_Menu_Staff.Visible = True
                MENU_STF.Visible = True


                ''''' STAFF >> SEARCH STAFF '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Search Staff" Then
                    MENU_STF_SS.Visible = True

                    ''''' STAFF >> SEARCH STAFF >> VIEW STAFF '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Staff" Then
                        MENU_STF_SS_VS.Visible = True

                        ''''' STAFF >> SEARCH STAFF >> VIEW STAFF >> EDIT BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_STF_SS_VS_EB.Visible = True

                            ''''' STAFF >> SEARCH STAFF >> VIEW STAFF >> EDIT BUTTON >> STAFF INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Staff Information" Then
                                MENU_STF_SS_VS_EB_SI.Visible = True

                                ''''' STAFF >> SEARCH STAFF >> VIEW STAFF >> EDIT BUTTON >> STAFF INFORMATION >> UPDATE BUTTON '''''
                                If find_Data_F2Update.Length > 0 And find_Data_F2Update = "TRUE" Then
                                    MENU_STF_SS_VS_EB_SI_UB.Visible = True
                                End If
                            End If

                            ''''' STAFF >> SEARCH STAFF >> VIEW STAFF >> EDIT BUTTON >> COURSE INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Course Information" Then
                                MENU_STF_SS_VS_EB_CI.Visible = True

                                ''''' STAFF >> SEARCH STAFF >> VIEW STAFF >> EDIT BUTTON >> STAFF INFORMATION >> UPDATE BUTTON '''''
                                If find_Data_F2Delete.Length > 0 And find_Data_F2Update = "TRUE" Then
                                    MENU_STF_SS_VS_EB_CI_DB.Visible = True
                                End If
                            End If
                        End If

                        ''''' STAFF >> SEARCH STAFF >> VIEW STAFF >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_STF_SS_VS_DB.Visible = True
                        End If
                    End If

                    ''''' STAFF >> SEARCH STAFF >> REGISTER STAFF '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Staff" Then
                        MENU_STF_SS_RS.Visible = True

                        ''''' STAFF >> SEARCH STAFF >> REGISTER STAFF >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_STF_SS_RS_RB.Visible = True
                        End If
                    End If

                    ''''' STAFF >> SEARCH STAFF >> IMPORT STAFF '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Import Staff" Then
                        MENU_STF_SS_IS.Visible = True

                        ''''' STAFF >> SEARCH STAFF >> IMPORT STAFF >> IMPORT BUTTON '''''
                        If find_Data_F1Import.Length > 0 And find_Data_F1Import = "TRUE" Then
                            MENU_STF_SS_IS_IB.Visible = True
                        End If
                    End If
                End If

                ''''' STAFF >> COURSE PLACEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Course Placement" Then
                    MENU_STF_CP.Visible = True

                    ''''' STAFF >> COURSE PLACEMENT >> VIEW STAFF COURSE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Staff Course" Then
                        MENU_STF_CP_VSC.Visible = True
                    End If

                    ''''' STAFF >> COURSE PLACEMENT >> REGISTER STAFF COURSE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Staff Course" Then
                        MENU_STF_SS_RS.Visible = True

                        '''''  STAFF >> COURSE PLACEMENT >> REGISTER STAFF COURSE >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_STF_SS_RS_RB.Visible = True
                        End If
                    End If
                End If
            End If

            ''''' COORDINATOR MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Coordinator" Then
                collapse_Menu_Coordinator.Visible = True
                MENU_COO.Visible = True

                ''''' COORDINATOR MENU >> SEARCH COORDINATOR '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Search Coordinator" Then
                    MENU_COO_SC.Visible = True

                    ''''' COORDINATOR MENU >> SEARCH COORDINATOR >> VIEW COORDINATOR '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Coordinator" Then
                        MENU_COO_SC_VC.Visible = True

                        '''''  COORDINATOR MENU >> SEARCH COORDINATOR >> VIEW COORDINATOR >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_COO_SC_VC_EB.Visible = True
                        End If

                        '''''  COORDINATOR MENU >> SEARCH COORDINATOR >> VIEW COORDINATOR >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_COO_SC_VC_DB.Visible = True
                        End If
                    End If

                    ''''' COORDINATOR MENU >> SEARCH COORDINATOR >> REGISTER COORDINATOR '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Coordinator" Then
                        MENU_COO_SC_RC.Visible = True

                        '''''  COORDINATOR MENU >> SEARCH COORDINATOR >> REGISTER COORDINATOR >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_COO_SC_RC_RB.Visible = True
                        End If
                    End If
                End If
            End If

            ''''' DISCIPLINE MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Discipline" Then
                collapse_Menu_Discipline.Visible = True
                MENU_DISC.Visible = True

                ''''' DISCIPLINE MENU >> DISCIPLINE MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Discipline Management" Then
                    MENU_DISC_DM.Visible = True

                    ''''' DISCIPLINE MENU >> DISCIPLINE MANAGEMENT >> VIEW DISCIPLINE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Discipline" Then
                        MENU_DISC_DM_VD.Visible = True

                        '''''  DISCIPLINE MENU >> DISCIPLINE MANAGEMENT >> VIEW DISCIPLINE >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_DISC_DM_VD_EB.Visible = True
                        End If

                        '''''  DISCIPLINE MENU >> DISCIPLINE MANAGEMENT >> VIEW DISCIPLINE >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_DISC_DM_VD_DB.Visible = True
                        End If
                    End If

                    ''''' DISCIPLINE MENU >> DISCIPLINE MANAGEMENT >> REGISTER DISCIPLINE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Discipline" Then
                        MENU_DISC_DM_RD.Visible = True

                        '''''  DISCIPLINE MENU >> DISCIPLINE MANAGEMENT >> REGISTER DISCIPLINE >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_DISC_DM_RD_RB.Visible = True
                        End If
                    End If
                End If

                ''''' DISCIPLINE MENU >> CASE MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Case Management" Then
                    MENU_DISC_CM.Visible = True

                    ''''' DISCIPLINE MENU >> CASE MANAGEMENT >> VIEW CASE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Case" Then
                        MENU_DISC_CM_VC.Visible = True

                        '''''  DISCIPLINE MENU >> CASE MANAGEMENT >> VIEW CASE >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_DISC_CM_VC_EB.Visible = True
                        End If

                        '''''  DISCIPLINE MENU >> CASE MANAGEMENT >> VIEW CASE >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_DISC_CM_VC_DB.Visible = True
                        End If
                    End If

                    ''''' CDISCIPLINE MENU >> CASE MANAGEMENT >> REGISTER CASE '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Case" Then
                        MENU_DISC_CM_RC.Visible = True

                        '''''  DISCIPLINE MENU >> CASE MANAGEMENT >> REGISTER CASE >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_DISC_CM_RC_RB.Visible = True
                        End If
                    End If
                End If
            End If

            ''''' COUNSELOR MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Counselor" Then
                collapse_Menu_Counselor.Visible = True
                MENU_COUN.Visible = True

                ''''' COUNSELOR MENU >> COUNSELOR MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Counselor Management" Then
                    MENU_COUN_CM.Visible = True

                    ''''' COUNSELOR MENU >> COUNSELOR MANAGEMENT >> VIEW MANAGEMENT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Management" Then
                        MENU_COUN_CM_VM.Visible = True

                        '''''  COUNSELOR MENU >> COUNSELOR MANAGEMENT >> VIEW MANAGEMENT >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_COUN_CM_VM_EB.Visible = True
                        End If

                        '''''  COUNSELOR MENU >> COUNSELOR MANAGEMENT >> VIEW MANAGEMENT >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_COUN_CM_VM_DB.Visible = True
                        End If
                    End If

                    ''''' COUNSELOR MENU >> COUNSELOR MANAGEMENT >> SELF DEVELOPMENT MANAGEMENT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Self Development Management" Then
                        MENU_COUN_CM_SDM.Visible = True

                        '''''  COUNSELOR MENU >> COUNSELOR MANAGEMENT >> SELF DEVELOPMENT MANAGEMENT >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_COUN_CM_SDM_RB.Visible = True
                        End If
                    End If

                    ''''' COUNSELOR MENU >> COUNSELOR MANAGEMENT >> PERSONALITY DEVELOPMENT MANAGEMENT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Personality Development Management" Then
                        MENU_COUN_CM_PDM.Visible = True

                        '''''  COUNSELOR MENU >> COUNSELOR MANAGEMENT >> PERSONALITY DEVELOPMENT MANAGEMENT >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_COUN_CM_PDM_RB.Visible = True
                        End If
                    End If
                End If

                ''''' COUNSELOR MENU >> SELF DEVELOPMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Self Development" Then
                    MENU_COUN_SDM.Visible = True

                    '''''  COUNSELOR MENU >> PORTFOLIO >> REGISTER BUTTON '''''
                    If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                        MENU_COUN_SDM_RB.Visible = True
                    End If
                End If

                ''''' COUNSELOR MENU >> PERSONALITY DEVELOPMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Personality Development" Then
                    MENU_COUN_PDM.Visible = True

                    '''''  COUNSELOR MENU >> PORTFOLIO >> REGISTER BUTTON '''''
                    If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                        MENU_COUN_PDM_RB.Visible = True
                    End If
                End If

                ''''' COUNSELOR MENU >> PORTFOLIO '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Portfolio" Then
                    MENU_COUN_POR.Visible = True

                    '''''  COUNSELOR MENU >> PORTFOLIO >> REGISTER BUTTON '''''
                    If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                        MENU_COUN_POR_RB.Visible = True
                    End If
                End If

                ''''' COUNSELOR MENU >> COUNSELLING ACTIVITY '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Counselling Activity" Then
                    MENU_COUN_CA.Visible = True

                    ''''' COUNSELOR MENU >> COUNSELLING ACTIVITY >> STUDENT COUNSELOR '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Student Counselor" Then
                        MENU_COUN_CA_SC.Visible = True

                        '''''   COUNSELOR MENU >> COUNSELLING ACTIVITY >> STUDENT COUNSELOR >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_COUN_CA_SC_RB.Visible = True
                        End If
                    End If

                    ''''' COUNSELOR MENU >> COUNSELLING ACTIVITY >> VIEW COUNSELOR REPORT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Counselor Report" Then
                        MENU_COUN_CA_VCR.Visible = True

                        '''''   COUNSELOR MENU >> COUNSELLING ACTIVITY >> VIEW COUNSELOR REPORT >> VIEW BUTTON '''''
                        If find_Data_F1View.Length > 0 And find_Data_F1View = "TRUE" Then
                            MENU_COUN_CA_VCR_VB.Visible = True
                        End If

                        '''''   COUNSELOR MENU >> COUNSELLING ACTIVITY >> VIEW COUNSELOR REPORT >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_COUN_CA_VCR_RB.Visible = True
                        End If
                    End If
                End If

                ''''' COUNSELOR MENU >> SCHOLARSHIP MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Scholarship Management" Then
                    MENU_COUN_SM.Visible = True

                    ''''' COUNSELOR MENU >> SCHOLARSHIP MANAGEMENT >> LIST SCHOLARSHIP '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "List Scholarship" Then
                        MENU_COUN_SM_LS.Visible = True

                        '''''   COUNSELOR MENU >> SCHOLARSHIP MANAGEMENT >> LIST SCHOLARSHIP >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_COUN_SM_LS_RB.Visible = True
                        End If

                        '''''   COUNSELOR MENU >> SCHOLARSHIP MANAGEMENT >> LIST SCHOLARSHIP >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_COUN_SM_LS_EB.Visible = True
                        End If

                        '''''   COUNSELOR MENU >> SCHOLARSHIP MANAGEMENT >> LIST SCHOLARSHIP >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_COUN_SM_LS_DB.Visible = True
                        End If
                    End If

                    ''''' COUNSELOR MENU >> SCHOLARSHIP MANAGEMENT >> STUDENT SCHOLARSHIP '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Student Scholarship" Then
                        MENU_COUN_SM_SS.Visible = True

                        '''''   COUNSELOR MENU >> SCHOLARSHIP MANAGEMENT >> STUDENT SCHOLARSHIP >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_COUN_SM_SS_DB.Visible = True
                        End If

                        '''''   COUNSELOR MENU >> SCHOLARSHIP MANAGEMENT >> STUDENT SCHOLARSHIP >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_COUN_SM_SS_RB.Visible = True
                        End If
                    End If
                End If
            End If

            ''''' RESEARCH MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Research" Then
                collapse_Menu_Research.Visible = True
                MENU_RES.Visible = True

                ''''' RESEARCH >> REGISTER STUDENT/SUPERVISOR '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Register Student/Supervisor" Then
                    MENU_RES_RSS.Visible = True

                    ''''' RESEARCH >> REGISTER STUDENT/SUPERVISOR >> VIEW STUDENT GROUP & SUPERVISOR '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Student Group & Supervisor" Then
                        MENU_RES_RSS_VSGS.Visible = True

                        '''''  RESEARCH >> REGISTER STUDENT/SUPERVISOR >> VIEW STUDENT GROUP & SUPERVISOR >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_RES_RSS_VSGS_DB.Visible = True
                        End If
                    End If

                    ''''' RESEARCH >> REGISTER STUDENT/SUPERVISOR >> REGISTER STUDENT GROUP & SUPERVISOR '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Student Group & Supervisor" Then
                        MENU_RES_RSS_RSGS.Visible = True

                        '''''  RESEARCH >> REGISTER STUDENT/SUPERVISOR >> REGISTER STUDENT GROUP & SUPERVISOR >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_RES_RSS_RSGS_RB.Visible = True
                        End If
                    End If
                End If

                ''''' RESEARCH >> REGISTER PROJECT/FIELD '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Register Project/Field" Then
                    MENU_RES_RPF.Visible = True

                    ''''' RESEARCH >> REGISTER PROJECT/FIELD >> VIEW STUDENT PROJECT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Student Project" Then
                        MENU_RES_RPF_VSP.Visible = True

                        '''''  RESEARCH >> REGISTER PROJECT/FIELD >> VIEW STUDENT PROJECT >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_RES_RPF_VSP_DB.Visible = True
                        End If
                    End If

                    ''''' RESEARCH >> REGISTER PROJECT/FIELD >> REGISTER STUDENT PROJECT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Student Project" Then
                        MENU_RES_RPF_RSP.Visible = True

                        '''''  RESEARCH >> REGISTER PROJECT/FIELD >> REGISTER STUDENT PROJECT >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_RES_RPF_RSP_RB.Visible = True
                        End If
                    End If
                End If

                ''''' RESEARCH >> REGISTER MENTOR '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Register Mentor" Then
                    MENU_RES_RM.Visible = True

                    ''''' RESEARCH >> REGISTER MENTOR >> VIEW STUDENT MENTOR '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Student Mentor" Then
                        MENU_RES_RM_VSM.Visible = True

                        '''''  RESEARCH >> REGISTER MENTOR >> VIEW STUDENT MENTOR >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_RES_RM_VSM_DB.Visible = True
                        End If
                    End If

                    ''''' RESEARCH >> REGISTER MENTOR >> REGISTER STUDENT MENTOR '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Student Mentor" Then
                        MENU_RES_RM_RSN.Visible = True

                        '''''  RESEARCH >> REGISTER MENTOR >> REGISTER STUDENT MENTOR >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_RES_RM_RSN_RB.Visible = True
                        End If
                    End If
                End If
            End If

            ''''' EXAMINATION MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Examination" Then
                collapse_Menu_Examination.Visible = True
                MENU_EXAM.Visible = True

                ''''' EXAMINATION >> EXAMINATION RESULT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Examination Result" Then
                    MENU_EXAM_ER.Visible = True

                    ''''' EXAMINATION >> EXAMINATION RESULT >> ACADEMIC RESULT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Academic Result" Then
                        MENU_EXAM_ER_AR.Visible = True

                        '''''  EXAMINATION >> EXAMINATION RESULT >> ACADEMIC RESULT >> UPDATE BUTTON '''''
                        If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                            MENU_EXAM_ER_AR_UB.Visible = True
                        End If
                    End If

                    ''''' EXAMINATION >> EXAMINATION RESULT >> COCURRICULAR RESULT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Cocurriculum Result" Then
                        MENU_EXAM_ER_CR.Visible = True
                    End If
                End If

                ''''' EXAMINATION >> EXAMINATION TRANSCRIPT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Examination Transcript" Then
                    MENU_EXAM_ET.Visible = True

                    ''''' EXAMINATION >> EXAMINATION TRANSCRIPT >> CURRENT EXAMINATION TRANSCRIPT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Current Examination Transcript" Then
                        MENU_EXAM_ET_CET.Visible = True

                        '''''  EXAMINATION >> EXAMINATION TRANSCRIPT >> CURRENT EXAMINATION TRANSCRIPT >> GENERATE GPA CGPA BUTTON '''''
                        If find_Data_F1GenerateGPACGPA.Length > 0 And find_Data_F1GenerateGPACGPA = "TRUE" Then
                            MENU_EXAM_ET_CET_GEN.Visible = True
                        End If

                        '''''  EXAMINATION >> EXAMINATION TRANSCRIPT >> CURRENT EXAMINATION TRANSCRIPT >> PRINT IN BI BUTTON '''''
                        If find_Data_F1PrintInBI.Length > 0 And find_Data_F1PrintInBI = "TRUE" Then
                            MENU_EXAM_ET_CET_BI.Visible = True
                        End If

                        '''''  EXAMINATION >> EXAMINATION TRANSCRIPT >> CURRENT EXAMINATION TRANSCRIPT >> PRINT IN BM BUTTON '''''
                        If find_Data_F1PrintInBM.Length > 0 And find_Data_F1PrintInBM = "TRUE" Then
                            MENU_EXAM_ET_CET_BM.Visible = True
                        End If
                    End If

                    ''''' EXAMINATION >> EXAMINATION TRANSCRIPT >> OFFICIAL TRANSCRIPT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Official Transcript" Then
                        MENU_EXAM_ET_OT.Visible = True

                        '''''  EXAMINATION >> EXAMINATION TRANSCRIPT >> OFFICIAL TRANSCRIPT >> PRINT IN BI BUTTON '''''
                        If find_Data_F1PrintInBI.Length > 0 And find_Data_F1PrintInBI = "TRUE" Then
                            MENU_EXAM_ET_OT_BI.Visible = True
                        End If

                        '''''  EXAMINATION >> EXAMINATION TRANSCRIPT >> OFFICIAL TRANSCRIPT >> PRINT IN BM BUTTON '''''
                        If find_Data_F1PrintInBM.Length > 0 And find_Data_F1PrintInBM = "TRUE" Then
                            MENU_EXAM_ET_OT_BM.Visible = True
                        End If
                    End If
                End If

                ''''' EXAMINATION >> IMPORT EXAMINATION '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Import Examination" Then
                    MENU_EXAM_IE.Visible = True

                    ''''' EXAMINATION >> IMPORT EXAMINATION >> IMPORT EXAMINATION RESULT '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Import Examination Result" Then
                        MENU_EXAM_IE_IER.Visible = True

                        '''''   EXAMINATION >> IMPORT EXAMINATION >> IMPORT EXAMINATION RESULT >> IMPORT BUTTON '''''
                        If find_Data_F1Import.Length > 0 And find_Data_F1Import = "TRUE" Then
                            MENU_EXAM_IE_IER_BI.Visible = True
                        End If
                    End If

                    ''''' EXAMINATION >> IMPORT EXAMINATION >> IMPORT GPA & CGPA '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Import GPA & CGPA" Then
                        MENU_EXAM_IE_IGC.Visible = True

                        '''''   EXAMINATION >> IMPORT EXAMINATION >> IMPORT EXAMINATION RESULT >> IMPORT BUTTON '''''
                        If find_Data_F1Import.Length > 0 And find_Data_F1Import = "TRUE" Then
                            MENU_EXAM_IE_IGC_BI.Visible = True
                        End If
                    End If
                End If
            End If

            ''''' HOSTEL MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Hostel" Then
                collapse_Menu_Hostel.Visible = True
                MENU_HST.Visible = True

                ''''' HOSTEL >> HOSTEL MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Hostel Management" Then
                    MENU_HST_HM.Visible = True

                    ''''' HOSTEL >> HOSTEL MANAGEMENT >> REGISTER HOSTEL '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register Hostel" Then
                        MENU_HST_HM_RH.Visible = True

                        '''''  HOSTEL >> HOSTEL MANAGEMENT >> REGISTER HOSTEL >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_HST_HM_RH_RB.Visible = True
                        End If
                    End If

                    ''''' HOSTEL >> HOSTEL MANAGEMENT >> VIEW HOSTEL '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View Hostel" Then
                        MENU_HST_HM_VH.Visible = True

                        '''''  HOSTEL >> HOSTEL MANAGEMENT >> VIEW HOSTEL >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_HST_HM_VH_EB.Visible = True

                            ''''' HOSTEL >> HOSTEL MANAGEMENT >> VIEW HOSTEL >> EDIT BUTTON >> EDIT HOSTEL INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Edit Hostel Information" Then
                                MENU_HST_HM_VH_EB_EHI.Visible = True

                                '''''  HOSTEL >> HOSTEL MANAGEMENT >> VIEW HOSTEL >> EDIT BUTTON >> EDIT HOSTEL INFORMATION >> UPDATE BUTTON '''''
                                If find_Data_F2Update.Length > 0 And find_Data_F2Update = "TRUE" Then
                                    MENU_HST_HM_VH_EB_EHI_UB.Visible = True
                                End If
                            End If

                            ''''' HOSTEL >> HOSTEL MANAGEMENT >> VIEW HOSTEL >> EDIT BUTTON >> EDIT ROOM INFORMATION '''''
                            If find_Data_SubMenu3.Length > 0 And find_Data_SubMenu3 = "Edit Room Information" Then
                                MENU_HST_HM_VH_EB_ERI.Visible = True

                                '''''  HOSTEL >> HOSTEL MANAGEMENT >> VIEW HOSTEL >> EDIT BUTTON >> EDIT ROOM INFORMATION >> EDIT BUTTON '''''
                                If find_Data_F2Edit.Length > 0 And find_Data_F2Edit = "TRUE" Then
                                    MENU_HST_HM_VH_EB_ERI_EB.Visible = True
                                End If

                                '''''  HOSTEL >> HOSTEL MANAGEMENT >> VIEW HOSTEL >> EDIT BUTTON >> EDIT ROOM INFORMATION >> DELETE BUTTON '''''
                                If find_Data_F2Delete.Length > 0 And find_Data_F2Delete = "TRUE" Then
                                    MENU_HST_HM_VH_EB_ERI_DB.Visible = True
                                End If
                            End If
                        End If

                        '''''  HOSTEL >> HOSTEL MANAGEMENT >> VIEW HOSTEL >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_HST_HM_VH_DB.Visible = True
                        End If
                    End If
                End If

                ''''' HOSTEL >> STUDENT PLACEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Student Placement" Then
                    MENU_HST_SP.Visible = True

                    '''''  HOSTEL >> STUDENT PLACEMENT >> UPDATE BUTTON '''''
                    If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                        MENU_HST_SP_UB.Visible = True
                    End If
                End If

                ''''' HOSTEL >> VIEW INFORMATION '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "View Information" Then
                    MENU_HST_VHI.Visible = True

                    '''''  HOSTEL >> VIEW INFORMATION >> DELETE BUTTON '''''
                    If find_Data_F1Delete.Length > 0 And find_Data_F1Delete.Length = "TRUE" Then
                        MENU_HST_VHI_DB.Visible = True
                    End If
                End If
            End If

            ''''' CO-CURRICULAR MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Co-Curriculum" Then
                collapse_Menu_Cocurricular.Visible = True
                MENU_CC.Visible = True

                ''''' CO-CURRICULAR >> CO-CURRICULAR MANAGEMENT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Co-Curriculum Management" Then
                    MENU_CC_CCM.Visible = True
                End If
            End If

            ''''' REPORT MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Report" Then
                collapse_Menu_Report.Visible = True
                MENU_RPT.Visible = True

                ''''' REPORT >> EXAMINATION REPORT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Examination Report" Then
                    MENU_RPT_ER.Visible = True
                End If

                ''''' REPORT >> CLASS EXAMINATION REPORT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Class Examination Report" Then
                    MENU_RPT_CLASSER.Visible = True
                End If

                ''''' REPORT >> COURSES EXAMINATION REPORT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Courses Examination Report" Then
                    MENU_RPT_COURSESER.Visible = True
                End If

                ''''' REPORT >> STUDENT RANKING LIST '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Student Ranking List" Then
                    MENU_RPT_SRL.Visible = True
                End If

                ''''' REPORT >> ATTENDANCE REPORT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Attendance Report" Then
                    MENU_RPT_AR.Visible = True
                End If

                ''''' REPORT >> FINANCIAL REPORT '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "Financial Report" Then
                    MENU_RPT_FR.Visible = True
                End If
            End If

            ''''' SETTING MENU '''''
            If find_Data_Menu.Length > 0 And find_Data_Menu = "Setting" Then
                collapse_Menu_Setting.Visible = True
                MENU_SET.Visible = True

                ''''' SETTING >> USER CONFIGURATION '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "User Configuration" Then
                    MENU_SET_UC.Visible = True

                    ''''' SETTING >> USER CONFIGURATION >> VIEW USER ACCESS '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View User Access" Then
                        MENU_SET_UC_VUA.Visible = True
                    End If

                    ''''' SETTING >> USER CONFIGURATION >> REGISTER USER ACCESS '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register User Access" Then
                        MENU_SET_UC_RUA.Visible = True

                        ''''' SETTING >> USER CONFIGURATION >> REGISTER USER ACCESS >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_SET_UC_RUA_RB.Visible = True
                        End If
                    End If
                End If

                ''''' SETTING >> SYSTEM CONFIGURATION '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "System Configuration" Then
                    MENU_SET_SC.Visible = True

                    ''''' SETTING >> SYSTEM CONFIGURATION >> REGISTER SYSTEM CONFIGURATION '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "Register System Configuration" Then
                        MENU_SET_SC_RSC.Visible = True

                        '''''  SETTING >> SYSTEM CONFIGURATION >> REGISTER SYSTEM CONFIGURATION >> REGISTER BUTTON '''''
                        If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                            MENU_SET_SC_RSC_RB.Visible = True
                        End If
                    End If

                    ''''' SETTING >> SYSTEM CONFIGURATION >> VIEW SYSTEM CONFIGURATION '''''
                    If find_Data_SubMenu2.Length > 0 And find_Data_SubMenu2 = "View System Configuration" Then
                        MENU_SET_SC_VSC.Visible = True

                        '''''  SETTING >> SYSTEM CONFIGURATION >> VIEW SYSTEM CONFIGURATION >> EDIT BUTTON '''''
                        If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                            MENU_SET_SC_VSC_EB.Visible = True
                        End If

                        '''''  SETTING >> SYSTEM CONFIGURATION >> VIEW SYSTEM CONFIGURATION >> DELETE BUTTON '''''
                        If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                            MENU_SET_SC_VSC_DB.Visible = True
                        End If
                    End If
                End If

                ''''' SETTING >> USER ACCESS '''''
                If find_Data_SubMenu1.Length > 0 And find_Data_SubMenu1 = "User Access" Then
                    MENU_SET_UA.Visible = True

                    '''''   SETTING >> USER ACCESS >> UPDATE BUTTON '''''
                    If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                        MENU_SET_UA_UB.Visible = True
                    End If
                End If

            End If

            If find_Data_Menu.Length > 0 And find_Data_Menu = "All" Then

                collapse_Menu_GeneralManagement.Visible = True
                collapse_Menu_Student.Visible = True
                collapse_Menu_Staff.Visible = True
                collapse_Menu_Coordinator.Visible = True
                collapse_Menu_Discipline.Visible = True
                collapse_Menu_Counselor.Visible = True
                collapse_Menu_Research.Visible = True
                collapse_Menu_Examination.Visible = True
                collapse_Menu_Hostel.Visible = True
                collapse_Menu_Cocurricular.Visible = True
                collapse_Menu_Report.Visible = True
                collapse_Menu_Setting.Visible = True

                ''LIST OF ALL MAIN MENU
                MENU_GM.Visible = True
                MENU_STD.Visible = True
                MENU_STF.Visible = True
                MENU_COO.Visible = True
                MENU_DISC.Visible = True
                MENU_COUN.Visible = True
                MENU_RES.Visible = True
                MENU_EXAM.Visible = True
                MENU_HST.Visible = True
                MENU_CC.Visible = True
                MENU_RPT.Visible = True
                MENU_SET.Visible = True

                ''LIST OF ALL SUB MENU 1
                MENU_GM_EM.Visible = True
                MENU_GM_GM.Visible = True
                MENU_GM_AM.Visible = True
                MENU_STD_COURSEM.Visible = True
                MENU_STD_CLASSM.Visible = True
                MENU_STD_SM.Visible = True
                MENU_STD_SS.Visible = True
                MENU_STD_CCP.Visible = True
                MENU_STD_ATT.Visible = True
                MENU_STD_VI.Visible = True
                MENU_STF_SS.Visible = True
                MENU_STF_CP.Visible = True
                MENU_COO_SC.Visible = True
                MENU_DISC_DM.Visible = True
                MENU_DISC_CM.Visible = True
                MENU_COUN_CM.Visible = True
                MENU_COUN_SDM.Visible = True
                MENU_COUN_PDM.Visible = True
                MENU_COUN_POR.Visible = True
                MENU_COUN_CA.Visible = True
                MENU_COUN_SM.Visible = True
                MENU_RES_RSS.Visible = True
                MENU_RES_RPF.Visible = True
                MENU_RES_RM.Visible = True
                MENU_EXAM_ER.Visible = True
                MENU_EXAM_ET.Visible = True
                MENU_EXAM_IE.Visible = True
                MENU_HST_HM.Visible = True
                MENU_HST_SP.Visible = True
                MENU_HST_VHI.Visible = True
                MENU_CC_CCM.Visible = True
                MENU_RPT_ER.Visible = True
                MENU_RPT_CLASSER.Visible = True
                MENU_RPT_COURSESER.Visible = True
                MENU_RPT_SRL.Visible = True
                MENU_RPT_AR.Visible = True
                MENU_RPT_FR.Visible = True
                MENU_SET_UC.Visible = True
                MENU_SET_SC.Visible = True
                MENU_SET_UA.Visible = True

                ''LIST OF ALL SUB MENU 2
                MENU_GM_EM_VE.Visible = True
                MENU_GM_EM_RE.Visible = True
                MENU_STD_COURSEM_VC.Visible = True
                MENU_STD_COURSEM_RC.Visible = True
                MENU_STD_COURSEM_TC.Visible = True
                MENU_STD_CLASSM_VC.Visible = True
                MENU_STD_CLASSM_RC.Visible = True
                MENU_STD_CLASSM_TC.Visible = True
                MENU_STD_SS_VS.Visible = True
                MENU_STD_SS_RS.Visible = True
                MENU_STD_SS_IS.Visible = True
                MENU_STD_ATT_VA.Visible = True
                MENU_STD_ATT_UA.Visible = True
                MENU_STD_VI_VCOURSE.Visible = True
                MENU_STD_VI_VCLASS.Visible = True
                MENU_STD_VIVCOCURRICULUM.Visible = True
                MENU_STD_VI_VH.Visible = True
                MENU_STD_VI_VR.Visible = True
                MENU_STF_SS_VS.Visible = True
                MENU_STF_SS_RS.Visible = True
                MENU_STF_SS_IS.Visible = True
                MENU_STF_CP_VSC.Visible = True
                MENU_STF_CP_RSC.Visible = True
                MENU_COO_SC_VC.Visible = True
                MENU_COO_SC_RC.Visible = True
                MENU_DISC_DM_VD.Visible = True
                MENU_DISC_DM_RD.Visible = True
                MENU_DISC_CM_VC.Visible = True
                MENU_DISC_CM_RC.Visible = True
                MENU_COUN_CM_VM.Visible = True
                MENU_COUN_CM_SDM.Visible = True
                MENU_COUN_CM_PDM.Visible = True
                MENU_COUN_CA_SC.Visible = True
                MENU_COUN_CA_VCR.Visible = True
                MENU_COUN_SM_LS.Visible = True
                MENU_COUN_SM_SS.Visible = True
                MENU_RES_RSS_VSGS.Visible = True
                MENU_RES_RSS_RSGS.Visible = True
                MENU_RES_RPF_VSP.Visible = True
                MENU_RES_RPF_RSP.Visible = True
                MENU_RES_RM_VSM.Visible = True
                MENU_RES_RM_RSN.Visible = True
                MENU_EXAM_ER_AR.Visible = True
                MENU_EXAM_ER_CR.Visible = True
                MENU_EXAM_ET_CET.Visible = True
                MENU_EXAM_ET_OT.Visible = True
                MENU_HST_HM_VH.Visible = True
                MENU_HST_HM_RH.Visible = True
                MENU_EXAM_IE_IER.Visible = True
                MENU_EXAM_IE_IGC.Visible = True
                MENU_SET_UC_VUA.Visible = True
                MENU_SET_UC_RUA.Visible = True
                MENU_SET_SC_VSC.Visible = True
                MENU_SET_SC_RSC.Visible = True

                ''LIST OF ALL SUB MENU 3
                MENU_STD_SS_VS_VB_SI.Visible = True
                MENU_STD_SS_VS_VB_FI.Visible = True
                MENU_STD_SS_VS_VB_COURSEI.Visible = True
                MENU_STD_SS_VS_VB_COCURRICULARI.Visible = True
                MENU_STD_SS_VS_VB_EI.Visible = True
                MENU_STD_SS_VS_VB_HI.Visible = True
                MENU_STD_SS_VS_VB_DI.Visible = True
                MENU_STD_SS_VS_VB_UP.Visible = True
                MENU_STD_SS_VS_VB_RI.Visible = True
                MENU_STF_SS_VS_EB_SI.Visible = True
                MENU_STF_SS_VS_EB_CI.Visible = True
                MENU_HST_HM_VH_EB_EHI.Visible = True
                MENU_HST_HM_VH_EB_ERI.Visible = True

                ''LIST OF ALL BUTTON FUNCTION 1
                MENU_GM_EM_VE_EB.Visible = True
                MENU_GM_EM_VE_DB.Visible = True
                MENU_GM_EM_RE_RB.Visible = True
                MENU_GM_GM_EB.Visible = True
                MENU_GM_GM_DB.Visible = True
                MENU_STD_COURSEM_VC_EB.Visible = True
                MENU_STD_COURSEM_VC_DB.Visible = True
                MENU_STD_COURSEM_RC_RB.Visible = True
                MENU_STD_COURSEM_TC_TB.Visible = True
                MENU_STD_CLASSM_VC_EB.Visible = True
                MENU_STD_CLASSM_VC_DB.Visible = True
                MENU_STD_CLASSM_RC_RB.Visible = True
                MENU_STD_CLASSM_TC_TB.Visible = True
                MENU_STD_SM_TB.Visible = True
                MENU_STD_SS_VS_VB.Visible = True
                MENU_STD_SS_VS_DB.Visible = True
                MENU_STD_SS_RS_RB.Visible = True
                MENU_STD_SS_IS_IB.Visible = True
                MENU_STD_CCP_RB.Visible = True
                MENU_STD_ATT_UA_UB.Visible = True
                MENU_STD_VI_VCOURSE_AB.Visible = True
                MENU_STD_VI_VCLASS_UB.Visible = True
                MENU_STD_VI_VR_UB.Visible = True
                MENU_STF_SS_VS_EB.Visible = True
                MENU_STF_SS_VS_DB.Visible = True
                MENU_STF_SS_RS_RB.Visible = True
                MENU_STF_SS_IS_IB.Visible = True
                MENU_STF_CP_RSC_RB.Visible = True
                MENU_COO_SC_VC_EB.Visible = True
                MENU_COO_SC_VC_DB.Visible = True
                MENU_COO_SC_RC_RB.Visible = True
                MENU_DISC_DM_VD_EB.Visible = True
                MENU_DISC_DM_VD_DB.Visible = True
                MENU_DISC_DM_RD_RB.Visible = True
                MENU_DISC_CM_VC_EB.Visible = True
                MENU_DISC_CM_VC_DB.Visible = True
                MENU_DISC_CM_RC_RB.Visible = True
                MENU_COUN_CM_VM_EB.Visible = True
                MENU_COUN_CM_VM_DB.Visible = True
                MENU_COUN_CM_SDM_RB.Visible = True
                MENU_COUN_CM_PDM_RB.Visible = True
                MENU_COUN_SDM_RB.Visible = True
                MENU_COUN_PDM_RB.Visible = True
                MENU_COUN_POR_RB.Visible = True
                MENU_RES_RSS_VSGS_DB.Visible = True
                MENU_RES_RSS_RSGS_RB.Visible = True
                MENU_RES_RPF_VSP_DB.Visible = True
                MENU_RES_RPF_RSP_RB.Visible = True
                MENU_RES_RM_VSM_DB.Visible = True
                MENU_RES_RM_RSN_RB.Visible = True
                MENU_EXAM_ER_AR_UB.Visible = True
                MENU_EXAM_ET_CET_GEN.Visible = True
                MENU_EXAM_ET_CET_BI.Visible = True
                MENU_EXAM_ET_CET_BM.Visible = True
                MENU_EXAM_ET_OT_BI.Visible = True
                MENU_EXAM_ET_OT_BM.Visible = True
                MENU_EXAM_IE_IER_BI.Visible = True
                MENU_EXAM_IE_IGC_BI.Visible = True
                MENU_HST_HM_VH_EB.Visible = True
                MENU_HST_HM_VH_DB.Visible = True
                MENU_HST_HM_RH_RB.Visible = True
                MENU_HST_SP_UB.Visible = True
                MENU_HST_VHI_DB.Visible = True
                MENU_SET_UC_RUA_RB.Visible = True
                MENU_SET_SC_VSC_EB.Visible = True
                MENU_SET_SC_VSC_DB.Visible = True
                MENU_SET_SC_RSC_RB.Visible = True
                MENU_SET_UA_UB.Visible = True

                ''LIST OF ALL BUTTON FUNCTION 2
                MENU_STD_SS_VS_VB_SI_UB.Visible = True
                MENU_STD_SS_VS_VB_FI_UB.Visible = True
                MENU_STD_SS_VS_VB_COURSEI_EB.Visible = True
                MENU_STD_SS_VS_VB_COURSEI_DB.Visible = True
                MENU_STD_SS_VS_VB_RI_VB.Visible = True
                MENU_STD_SS_VS_VB_RI_DOWNLOADB.Visible = True
                MENU_STD_SS_VS_VB_RI_DB.Visible = True
                MENU_STF_SS_VS_EB_SI_UB.Visible = True
                MENU_STF_SS_VS_EB_CI_DB.Visible = True
                MENU_HST_HM_VH_EB_EHI_UB.Visible = True
                MENU_HST_HM_VH_EB_ERI_EB.Visible = True
                MENU_HST_HM_VH_EB_ERI_DB.Visible = True

            End If

        Next

    End Sub

End Class