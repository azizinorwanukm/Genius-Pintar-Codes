Imports System.Data.SqlClient
Imports System.Globalization

Public Class counselor_Activity
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

                If Session("getStatus") = "SC" Then ''Student Counselor
                    txtbreadcrum1.Text = "Student Counselor"

                    StudentCounselor.Visible = True
                    ViewCounselorReport.Visible = False

                    btnStudentCounselor.Attributes("class") = "btn btn-info"
                    btnViewCounselorReport.Attributes("class") = "btn btn-default font"

                    SC_Year()
                    SC_Level()
                    SC_Type()
                    SC_Session()
                    SC_Counselor()

                    txtDate.Text = Date.Now.ToString("dd MMMM yyyy")
                    txtDate_SCDI.Text = Date.Now.ToString("dd MMMM yyyy")
                    txtDate_SCSE.Text = Date.Now.ToString("dd MMMM yyyy")
                    txtDate_SCINC.Text = Date.Now.ToString("dd MMMM yyyy")

                    Table_SC.Visible = True
                    Table_SCDI.Visible = False
                    Table_SCSE.Visible = False
                    Table_SCINC.Visible = False

                    strRet = BindDataSC(SCRespondent)

                ElseIf Session("getStatus") = "VCR" Then ''View Counselor Report
                    txtbreadcrum1.Text = "View Counselor Report"

                    StudentCounselor.Visible = False
                    ViewCounselorReport.Visible = True

                    btnStudentCounselor.Attributes("class") = "btn btn-default font"
                    btnViewCounselorReport.Attributes("class") = "btn btn-info"

                    SC_Year()
                    SC_Level()
                    SC_Type()
                    SC_Session()
                    VCR_Month()

                    Table_VCR_CGPA.Visible = True
                    Table_VCR_DI.Visible = False
                    Table_VCR_SE.Visible = False
                    Table_VCR_INC.Visible = False

                    strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewCounselorReport.Visible = False
        btnStudentCounselor.Visible = False

        btn_RegisterCounselorCGPA.Visible = False
        btn_RegisterCounselorDiscipline.Visible = False
        btn_RegisterCounselorSocialEmotional.Visible = False
        btn_RegisterCounselorINeedCounselling.Visible = False

        assign_StatusCGPA.Visible = False
        assign_StatusDI.Visible = False
        assign_StatusSE.Visible = False
        assign_StatusINC.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = Session("user_position")

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_StudentCounseling As String = ""
        Dim Get_ViewCounselorReport As String = ""

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

            If find_Data_SubMenu2 = "Student Counselor" And find_Data_SubMenu2.Length > 0 Then
                btnStudentCounselor.Visible = True
                StudentCounselor.Visible = True

                Get_StudentCounseling = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btn_RegisterCounselorCGPA.Visible = True
                    btn_RegisterCounselorDiscipline.Visible = True
                    btn_RegisterCounselorSocialEmotional.Visible = True
                    btn_RegisterCounselorINeedCounselling.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "View Counselor Report" And find_Data_SubMenu2.Length > 0 Then
                btnViewCounselorReport.Visible = True
                ViewCounselorReport.Visible = True

                Get_ViewCounselorReport = "TRUE"

                If find_Data_F1View.Length > 0 And find_Data_F1View = "TRUE" Then
                    Session("getViewButton") = "TRUE"
                End If
            End If

            Dim find_LevelAccess As String = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = 'KKSLR' "
            Dim get_LevelAccess As String = oCommon.getFieldValue(find_LevelAccess)

            If (find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All") Or get_LevelAccess.Length > 0 Then
                btnStudentCounselor.Visible = True
                btnViewCounselorReport.Visible = True
                StudentCounselor.Visible = True

                btn_RegisterCounselorCGPA.Visible = True
                btn_RegisterCounselorDiscipline.Visible = True
                btn_RegisterCounselorSocialEmotional.Visible = True
                btn_RegisterCounselorINeedCounselling.Visible = True

                assign_StatusCGPA.Visible = True
                assign_StatusDI.Visible = True
                assign_StatusSE.Visible = True
                assign_StatusINC.Visible = True

                Get_StudentCounseling = "TRUE"
                Session("getViewButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "SC" Or Session("getStatus") = "VCR" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "SC" And Session("getStatus") <> "VCR" Then
            If Get_StudentCounseling = "TRUE" Then
                Data_If_Not_Group_Status = "SC"
            ElseIf Get_ViewCounselorReport = "TRUE" Then
                Data_If_Not_Group_Status = "VCR"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_StudentCounseling = "TRUE" And Data_If_Not_Group_Status = "SC" Then
                Session("getStatus") = "SC"
            ElseIf Get_ViewCounselorReport = "TRUE" And Data_If_Not_Group_Status = "VCR" Then
                Session("getStatus") = "VCR"
            End If
        End If
    End Sub

    Private Sub btnStudentCounselor_ServerClick(sender As Object, e As EventArgs) Handles btnStudentCounselor.ServerClick
        Session("getStatus") = "SC"
        Response.Redirect("admin_kaunselor_aktivitikaunselor.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnStudentScholarship_ServerClick(sender As Object, e As EventArgs) Handles btnViewCounselorReport.ServerClick
        Session("getStatus") = "VCR"
        Response.Redirect("admin_kaunselor_aktivitikaunselor.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub SC_Year()
        strSQL = "select Parameter from setting where Type = 'Year' "
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

            ddl_Year_VR.DataSource = ds
            ddl_Year_VR.DataTextField = "Parameter"
            ddl_Year_VR.DataValueField = "Parameter"
            ddl_Year_VR.DataBind()
            ddl_Year_VR.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_Year_VR.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SC_Level()
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

            ddl_Level_VR.DataSource = ds
            ddl_Level_VR.DataTextField = "Parameter"
            ddl_Level_VR.DataValueField = "Parameter"
            ddl_Level_VR.DataBind()
            ddl_Level_VR.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_Level_VR.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SC_Type()
        strSQL = "select Parameter from setting where Type = 'Year' and Parameter = '99999999999999'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_type.DataSource = ds
            ddl_type.DataTextField = "Parameter"
            ddl_type.DataValueField = "Parameter"
            ddl_type.DataBind()
            ddl_type.Items.Insert(0, New ListItem("Select Counselor Type", String.Empty))
            ddl_type.Items.Insert(1, New ListItem("Underachiever", "Underachiever"))
            ddl_type.Items.Insert(2, New ListItem("Discipline Issues", "Discipline Issues"))
            ddl_type.Items.Insert(3, New ListItem("Social Emotional", "Social Emotional"))
            ddl_type.Items.Insert(4, New ListItem("I Need Counselling", "I Need Counselling"))
            ddl_type.SelectedIndex = 0

            ddl_CounselorType_VR.DataSource = ds
            ddl_CounselorType_VR.DataTextField = "Parameter"
            ddl_CounselorType_VR.DataValueField = "Parameter"
            ddl_CounselorType_VR.DataBind()
            ddl_CounselorType_VR.Items.Insert(0, New ListItem("Select Counselor Type", String.Empty))
            ddl_CounselorType_VR.Items.Insert(1, New ListItem("Underachiever", "Underachiever"))
            ddl_CounselorType_VR.Items.Insert(2, New ListItem("Discipline Issues", "Discipline Issues"))
            ddl_CounselorType_VR.Items.Insert(3, New ListItem("Social Emotional", "Social Emotional"))
            ddl_CounselorType_VR.Items.Insert(4, New ListItem("I Need Counselling", "I Need Counselling"))
            ddl_CounselorType_VR.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SC_Session()
        strSQL = "select Parameter,Value from setting where Type = 'Counselor Session' and idx = 'Counselor'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_CounselorSessionCGPA.DataSource = ds
            ddl_CounselorSessionCGPA.DataTextField = "Parameter"
            ddl_CounselorSessionCGPA.DataValueField = "Value"
            ddl_CounselorSessionCGPA.DataBind()
            ddl_CounselorSessionCGPA.Items.Insert(0, New ListItem("Select Session", String.Empty))
            ddl_CounselorSessionCGPA.SelectedIndex = 0

            ddl_CounselorSessionDI.DataSource = ds
            ddl_CounselorSessionDI.DataTextField = "Parameter"
            ddl_CounselorSessionDI.DataValueField = "Value"
            ddl_CounselorSessionDI.DataBind()
            ddl_CounselorSessionDI.Items.Insert(0, New ListItem("Select Session", String.Empty))
            ddl_CounselorSessionDI.SelectedIndex = 0

            ddl_CounselorSessionSE.DataSource = ds
            ddl_CounselorSessionSE.DataTextField = "Parameter"
            ddl_CounselorSessionSE.DataValueField = "Value"
            ddl_CounselorSessionSE.DataBind()
            ddl_CounselorSessionSE.Items.Insert(0, New ListItem("Select Session", String.Empty))
            ddl_CounselorSessionSE.SelectedIndex = 0

            ddl_CounselorSessionINC.DataSource = ds
            ddl_CounselorSessionINC.DataTextField = "Parameter"
            ddl_CounselorSessionINC.DataValueField = "Value"
            ddl_CounselorSessionINC.DataBind()
            ddl_CounselorSessionINC.Items.Insert(0, New ListItem("Select Session", String.Empty))

            ddl_session_VR.DataSource = ds
            ddl_session_VR.DataTextField = "Parameter"
            ddl_session_VR.DataValueField = "Value"
            ddl_session_VR.DataBind()
            ddl_session_VR.Items.Insert(0, New ListItem("Select Session", String.Empty))
            ddl_session_VR.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub VCR_Month()
        strSQL = "Select Parameter, Value from setting where type = 'Month' and idx = 'Date'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Month_VR.DataSource = ds
            ddl_Month_VR.DataTextField = "Parameter"
            ddl_Month_VR.DataValueField = "Value"
            ddl_Month_VR.DataBind()
            ddl_Month_VR.Items.Insert(0, New ListItem("Select Month", String.Empty))
            ddl_Month_VR.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SC_Counselor()
        strSQL = "  select A.stf_ID, A.staff_Name from staff_info A left join staff_Login B on A.stf_ID = B.stf_ID
                    where A.staff_Status = 'Access' and (B.staff_Access = 'KSLR' or B.staff_Access = 'KKSLR') and staff_Campus = '" & Session("SchoolCampus") & "'
                    order by staff_Name asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_CounselorName.DataSource = ds
            ddl_CounselorName.DataTextField = "staff_Name"
            ddl_CounselorName.DataValueField = "stf_ID"
            ddl_CounselorName.DataBind()
            ddl_CounselorName.Items.Insert(0, New ListItem("Select Counselor Name", String.Empty))
            ddl_CounselorName.SelectedIndex = 0

            ddl_CounselorName_SCDI.DataSource = ds
            ddl_CounselorName_SCDI.DataTextField = "staff_Name"
            ddl_CounselorName_SCDI.DataValueField = "stf_ID"
            ddl_CounselorName_SCDI.DataBind()
            ddl_CounselorName_SCDI.Items.Insert(0, New ListItem("Select Counselor Name", String.Empty))
            ddl_CounselorName_SCDI.SelectedIndex = 0

            ddl_CounselorName_SCSE.DataSource = ds
            ddl_CounselorName_SCSE.DataTextField = "staff_Name"
            ddl_CounselorName_SCSE.DataValueField = "stf_ID"
            ddl_CounselorName_SCSE.DataBind()
            ddl_CounselorName_SCSE.Items.Insert(0, New ListItem("Select Counselor Name", String.Empty))
            ddl_CounselorName_SCSE.SelectedIndex = 0

            ddl_CounselorName_SCINC.DataSource = ds
            ddl_CounselorName_SCINC.DataTextField = "staff_Name"
            ddl_CounselorName_SCINC.DataValueField = "stf_ID"
            ddl_CounselorName_SCINC.DataBind()
            ddl_CounselorName_SCINC.Items.Insert(0, New ListItem("Select Counselor Name", String.Empty))
            ddl_CounselorName_SCINC.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            If ddl_type.SelectedValue = "Underachiever" Then
                strRet = BindDataSC(SCRespondent)
                Table_SC.Visible = True
                Table_SCDI.Visible = False
                Table_SCSE.Visible = False
                Table_SCINC.Visible = False

            ElseIf ddl_type.SelectedValue = "Discipline Issues" Then
                strRet = BindDataSCDI(SCDIRespondent)
                Table_SC.Visible = False
                Table_SCDI.Visible = True
                Table_SCSE.Visible = False
                Table_SCINC.Visible = False

            ElseIf ddl_type.SelectedValue = "Social Emotional" Then
                strRet = BindDataSCSE(SCSERespondent)
                Table_SC.Visible = False
                Table_SCDI.Visible = False
                Table_SCSE.Visible = True
                Table_SCINC.Visible = False

            ElseIf ddl_type.SelectedValue = "I Need Counselling" Then
                strRet = BindDataSCINC(SCINCRespondent)
                Table_SC.Visible = False
                Table_SCDI.Visible = False
                Table_SCSE.Visible = False
                Table_SCINC.Visible = True

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_type.SelectedIndexChanged
        Try
            If ddl_type.SelectedValue = "Underachiever" Then
                strRet = BindDataSC(SCRespondent)
                Table_SC.Visible = True
                Table_SCDI.Visible = False
                Table_SCSE.Visible = False
                Table_SCINC.Visible = False

            ElseIf ddl_type.SelectedValue = "Discipline Issues" Then
                strRet = BindDataSCDI(SCDIRespondent)
                Table_SC.Visible = False
                Table_SCDI.Visible = True
                Table_SCSE.Visible = False
                Table_SCINC.Visible = False

            ElseIf ddl_type.SelectedValue = "Social Emotional" Then
                strRet = BindDataSCSE(SCSERespondent)
                Table_SC.Visible = False
                Table_SCDI.Visible = False
                Table_SCSE.Visible = True
                Table_SCINC.Visible = False

            ElseIf ddl_type.SelectedValue = "I Need Counselling" Then
                strRet = BindDataSCINC(SCINCRespondent)
                Table_SC.Visible = False
                Table_SCDI.Visible = False
                Table_SCSE.Visible = False
                Table_SCINC.Visible = True

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            If ddl_type.SelectedValue = "Underachiever" Then
                strRet = BindDataSC(SCRespondent)
            ElseIf ddl_type.SelectedValue = "Discipline Issues" Then
                strRet = BindDataSCDI(SCDIRespondent)
            ElseIf ddl_type.SelectedValue = "Social Emotional" Then
                strRet = BindDataSCSE(SCSERespondent)
            ElseIf ddl_type.SelectedValue = "I Need Counselling" Then
                strRet = BindDataSCINC(SCINCRespondent)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataSC(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLSC, strConn)
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

    Private Function getSQLSC() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  Select distinct C.ID, A.student_Name, B.student_Level, E.class_Name, C.ID, C.exam_Name, A.student_Stream, C.png, C.pngs from student_info A
                    left join student_level B on A.std_ID = B.std_ID
                    left join student_Png C on A.std_ID = C.std_ID
                    left join course D on A.std_ID = D.std_ID
                    left join class_info E on D.class_ID = E.class_ID"

        strWhere = "    Where (A.student_Status = 'Access' or A.student_Status = 'Graduate') and A.student_ID like '%M%' and A.student_Campus = '" & Session("SchoolCampus") & "'
                        and B.year = '" & ddl_Year.SelectedValue & "' and B.Registered = 'Yes' 
                        and C.year = '" & ddl_Year.SelectedValue & "' and C.pngs < '3.0'
                        and D.year = '" & ddl_Year.SelectedValue & "'
                        and E.class_year = '" & ddl_Year.SelectedValue & "' and E.class_Type = 'Compulsory' and E.class_Campus = '" & Session("SchoolCampus") & "'"

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += "   and B.student_Level = '" & ddl_Level.SelectedValue & "'"
        End If

        strOrderby = "  order by B.student_Level, A.student_Name, C.ID asc"

        getSQLSC = tmpSQL & strWhere & strOrderby

        Return getSQLSC
    End Function

    Private Sub btn_RegisterCounselorCGPA_ServerClick(sender As Object, e As EventArgs) Handles btn_RegisterCounselorCGPA.ServerClick

        If checkData() = True Then
            Dim i As Integer

            strSQL = ""
            strRet = ""

            ''Get Date
            Dim Get_Date_SS As String = txtDate.Text.Substring(0, 2)
            Dim Get_Year_SS As String = txtDate.Text.Substring(txtDate.Text.Length - 4, 4)
            Dim Get_Month_SS As String = txtDate.Text.Remove(0, 3)
            Get_Month_SS = Get_Month_SS.Remove(Get_Month_SS.Length - 5, 5)

            If Get_Month_SS = "January" Then
                Get_Month_SS = "01"
            ElseIf Get_Month_SS = "February" Then
                Get_Month_SS = "02"
            ElseIf Get_Month_SS = "March" Then
                Get_Month_SS = "03"
            ElseIf Get_Month_SS = "April" Then
                Get_Month_SS = "04"
            ElseIf Get_Month_SS = "May" Then
                Get_Month_SS = "05"
            ElseIf Get_Month_SS = "June" Then
                Get_Month_SS = "06"
            ElseIf Get_Month_SS = "July" Then
                Get_Month_SS = "07"
            ElseIf Get_Month_SS = "August" Then
                Get_Month_SS = "08"
            ElseIf Get_Month_SS = "September" Then
                Get_Month_SS = "09"
            ElseIf Get_Month_SS = "October" Then
                Get_Month_SS = "10"
            ElseIf Get_Month_SS = "November" Then
                Get_Month_SS = "11"
            ElseIf Get_Month_SS = "December" Then
                Get_Month_SS = "12"
            End If

            Dim Final_Date_Data As String = Get_Date_SS & "/" & Get_Month_SS & "/" & Get_Year_SS
            Dim Final_Date_Data_Reorder As String = Get_Year_SS & Get_Month_SS & Get_Date_SS


            For i = 0 To SCRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(SCRespondent.Rows(i).Cells(5).FindControl("chkSelectSC"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = SCRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''Get Student ID
                        Dim Find_StdID As String = "Select distinct std_ID from student_Png where ID  = '" & strKey & "'"
                        Dim Get_StdID As String = oCommon.getFieldValue(Find_StdID)

                        ''Get Student Class ID
                        Dim Find_ClassID As String = "  Select distinct A.class_ID, A.class_Name from class_info A left join course B on A.class_ID = B.class_ID left join student_info c on B.std_ID = C.std_ID
                                                        where C.student_Campus = '" & Session("SchoolCampus") & "' and (C.student_Status = 'Access' or C.student_Status = 'Graduate') and C.student_ID like '%M%' and B.year = '" & ddl_Year.SelectedValue & "' and A.class_year = '" & ddl_Year.SelectedValue & "' 
                                                        and A.class_type = 'Compulsory' and A.class_Campus = '" & Session("SchoolCampus") & "' and C.std_ID = '" & Get_StdID & "'"
                        Dim Get_ClassID As String = oCommon.getFieldValue(Find_ClassID)

                        ''Insert Into Counserlor Info Database
                        strSQL = "  Insert Into counselor_info(std_ID,stf_ID,class_ID,CI_Year,CI_Type,CI_Date,CI_CounselingDetail,CI_DateReorder,std_Png_ID,CI_Status,CI_Session)
                                    Values('" & Get_StdID & "','" & ddl_CounselorName.SelectedValue & "','" & Get_ClassID & "','" & ddl_Year.SelectedValue & "','Underachiever','" & Final_Date_Data & "','Underachiever','" & Final_Date_Data_Reorder & "','" & strKey & "','In Progress','" & ddl_CounselorSessionCGPA.SelectedValue & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet <> "0" Then

                            strSQL = "Select UPPER(Student_Name) from student_info where std_ID = '" & Get_StdID & "'"
                            strRet = oCommon.getFieldValue(strSQL)

                            ShowMessage(" Fail To Set Counseling Date For Student -> " & strRet, MessageType.Error)
                            Exit For
                        End If

                    End If
                End If
            Next

            If strRet = "0" Then
                ShowMessage(" Transfer Student", MessageType.Success)
            End If

        End If
    End Sub

    Public Function checkData()

        If ddl_CounselorName.SelectedIndex = 0 Then
            ShowMessage(" Please Select Counselor Name ", MessageType.Error)
            Return False
        End If

        If txtDate.Text.Length = 0 Then
            ShowMessage(" Please Select Date ", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Function BindDataSCDI(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLSCDI, strConn)
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

    Private Function getSQLSCDI() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  select B.disiplin_id, A.student_Name, D.class_Level, D.class_Name, C.case_MeritDemerit_Point, C.case_Category, B.Dicipline_Date, B.Dicipline_DateReorder from student_info A
                    left join dicipline_info B on A.std_ID = B.std_ID
                    left join case_info C on B.case_ID = C.case_ID
                    left join class_info D on B.class_ID = D.class_ID"

        strWhere = "    where A.student_ID like '%M%' and (A.student_Status = 'Access' or A.student_Status = 'Graduate') and A.student_Campus = '" & Session("SchoolCampus") & "'
                        and D.class_year = '" & ddl_Year.SelectedValue & "' and D.class_type = 'Compulsory'"

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += "   and D.class_Level = '" & ddl_Level.SelectedValue & "'"
        End If

        strOrderby = "  order by B.Dicipline_DateReorder, A.student_Name asc"

        getSQLSCDI = tmpSQL & strWhere & strOrderby

        Return getSQLSCDI
    End Function

    Private Sub btn_RegisterCounselorDiscipline_ServerClick(sender As Object, e As EventArgs) Handles btn_RegisterCounselorDiscipline.ServerClick

        If checkDataDI() = True Then
            Dim i As Integer

            strSQL = ""
            strRet = ""

            ''Get Date
            Dim Get_Date_SS As String = txtDate_SCDI.Text.Substring(0, 2)
            Dim Get_Year_SS As String = txtDate_SCDI.Text.Substring(txtDate_SCDI.Text.Length - 4, 4)
            Dim Get_Month_SS As String = txtDate_SCDI.Text.Remove(0, 3)
            Get_Month_SS = Get_Month_SS.Remove(Get_Month_SS.Length - 5, 5)

            If Get_Month_SS = "January" Then
                Get_Month_SS = "01"
            ElseIf Get_Month_SS = "February" Then
                Get_Month_SS = "02"
            ElseIf Get_Month_SS = "March" Then
                Get_Month_SS = "03"
            ElseIf Get_Month_SS = "April" Then
                Get_Month_SS = "04"
            ElseIf Get_Month_SS = "May" Then
                Get_Month_SS = "05"
            ElseIf Get_Month_SS = "June" Then
                Get_Month_SS = "06"
            ElseIf Get_Month_SS = "July" Then
                Get_Month_SS = "07"
            ElseIf Get_Month_SS = "August" Then
                Get_Month_SS = "08"
            ElseIf Get_Month_SS = "September" Then
                Get_Month_SS = "09"
            ElseIf Get_Month_SS = "October" Then
                Get_Month_SS = "10"
            ElseIf Get_Month_SS = "November" Then
                Get_Month_SS = "11"
            ElseIf Get_Month_SS = "December" Then
                Get_Month_SS = "12"
            End If

            Dim Final_Date_Data As String = Get_Date_SS & "/" & Get_Month_SS & "/" & Get_Year_SS
            Dim Final_Date_Data_Reorder As String = Get_Year_SS & Get_Month_SS & Get_Date_SS

            For i = 0 To SCDIRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(SCDIRespondent.Rows(i).Cells(5).FindControl("chkSelectSCDI"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = SCDIRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''Get Student ID
                        Dim Find_StdID As String = "Select distinct std_ID from dicipline_info where disiplin_id  = '" & strKey & "'"
                        Dim Get_StdID As String = oCommon.getFieldValue(Find_StdID)

                        ''Get Student Class ID
                        Dim Find_ClassID As String = "  Select distinct A.class_ID, A.class_Name from class_info A left join course B on A.class_ID = B.class_ID left join student_info c on B.std_ID = C.std_ID
                                                        where C.student_Campus = '" & Session("SchoolCampus") & "' and (C.student_Status = 'Access' or C.student_Status = 'Graduate') and C.student_ID like '%M%' and B.year = '" & ddl_Year.SelectedValue & "' and A.class_year = '" & ddl_Year.SelectedValue & "' 
                                                        and A.class_type = 'Compulsory' and A.class_Campus = '" & Session("SchoolCampus") & "' and C.std_ID = '" & Get_StdID & "'"
                        Dim Get_ClassID As String = oCommon.getFieldValue(Find_ClassID)

                        ''Insert Into Counserlor Info Database
                        strSQL = "  Insert Into counselor_info(std_ID,stf_ID,class_ID,CI_Year,CI_Type,CI_Date,CI_CounselingDetail,CI_DateReorder,disiplin_ID,CI_Status,CI_Session)
                                    Values('" & Get_StdID & "','" & ddl_CounselorName_SCDI.SelectedValue & "','" & Get_ClassID & "','" & ddl_Year.SelectedValue & "','Discipline Issues','" & Final_Date_Data & "','Discipline Issues','" & Final_Date_Data_Reorder & "','" & strKey & "','In Progress','" & ddl_CounselorSessionDI.SelectedValue & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet <> "0" Then

                            strSQL = "Select UPPER(Student_Name) from student_info where std_ID = '" & Get_StdID & "'"
                            strRet = oCommon.getFieldValue(strSQL)

                            ShowMessage(" Fail To Set Counseling Date For Student -> " & strRet, MessageType.Error)
                            Exit For
                        End If

                    End If
                End If
            Next

            If strRet = "0" Then
                ShowMessage(" Transfer Student", MessageType.Success)
            End If

        End If
    End Sub

    Public Function checkDataDI()

        If ddl_CounselorName_SCDI.SelectedIndex = 0 Then
            ShowMessage(" Please Select Counselor Name ", MessageType.Error)
            Return False
        End If

        If txtDate_SCDI.Text.Length = 0 Then
            ShowMessage(" Please Select Date ", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Function BindDataSCSE(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLSCSE, strConn)
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

    Private Function getSQLSCSE() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  Select distinct A.std_ID, A.student_Name, A.student_ID, A.student_Mykad, B.student_Level, D.class_Name from Student_info A
                    Left join student_Level B on A.std_ID = B.std_ID
                    Left join course C on A.std_ID = C.std_ID
                    Left join class_info D on C.class_ID = D.class_ID"

        strWhere = "    Where A.student_ID like '%M%' and A.student_Status = 'Access' and B.year = '" & ddl_Year.SelectedValue & "' and B.Registered = 'Yes' and A.student_Campus = '" & Session("SchoolCampus") & "'
                        and C.year = '" & ddl_Year.SelectedValue & "' and D.class_year = '" & ddl_Year.SelectedValue & "' and D.class_type = 'Compulsory'"

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += "   and B.student_Level = '" & ddl_Level.SelectedValue & "'"
        End If

        strOrderby = "  order by A.student_Name asc"

        getSQLSCSE = tmpSQL & strWhere & strOrderby

        Return getSQLSCSE
    End Function

    Private Sub btn_RegisterCounselorSocialEmotional_ServerClick(sender As Object, e As EventArgs) Handles btn_RegisterCounselorSocialEmotional.ServerClick

        If checkDataSE() = True Then
            Dim i As Integer

            strSQL = ""
            strRet = ""

            ''Get Date
            Dim Get_Date_SS As String = txtDate_SCSE.Text.Substring(0, 2)
            Dim Get_Year_SS As String = txtDate_SCSE.Text.Substring(txtDate_SCSE.Text.Length - 4, 4)
            Dim Get_Month_SS As String = txtDate_SCSE.Text.Remove(0, 3)
            Get_Month_SS = Get_Month_SS.Remove(Get_Month_SS.Length - 5, 5)

            If Get_Month_SS = "January" Then
                Get_Month_SS = "01"
            ElseIf Get_Month_SS = "February" Then
                Get_Month_SS = "02"
            ElseIf Get_Month_SS = "March" Then
                Get_Month_SS = "03"
            ElseIf Get_Month_SS = "April" Then
                Get_Month_SS = "04"
            ElseIf Get_Month_SS = "May" Then
                Get_Month_SS = "05"
            ElseIf Get_Month_SS = "June" Then
                Get_Month_SS = "06"
            ElseIf Get_Month_SS = "July" Then
                Get_Month_SS = "07"
            ElseIf Get_Month_SS = "August" Then
                Get_Month_SS = "08"
            ElseIf Get_Month_SS = "September" Then
                Get_Month_SS = "09"
            ElseIf Get_Month_SS = "October" Then
                Get_Month_SS = "10"
            ElseIf Get_Month_SS = "November" Then
                Get_Month_SS = "11"
            ElseIf Get_Month_SS = "December" Then
                Get_Month_SS = "12"
            End If

            Dim Final_Date_Data As String = Get_Date_SS & "/" & Get_Month_SS & "/" & Get_Year_SS
            Dim Final_Date_Data_Reorder As String = Get_Year_SS & Get_Month_SS & Get_Date_SS

            For i = 0 To SCSERespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(SCSERespondent.Rows(i).Cells(5).FindControl("chkSelectSCSE"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = SCSERespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''Get Student Class ID
                        Dim Find_ClassID As String = "  Select distinct A.class_ID from class_info A Left join course B on A.class_ID = B.class_ID
                                                        Where A.class_Year = '" & ddl_Year.SelectedValue & "' and B.year = '" & ddl_Year.SelectedValue & "' and A.class_Type = 'Compulsory' and B.std_ID = '" & strKey & "' and A.class_Campus = '" & Session("SchoolCampus") & "'"
                        Dim Get_ClassID As String = oCommon.getFieldValue(Find_ClassID)

                        ''Insert Into Counserlor Info Database
                        strSQL = "  Insert Into counselor_info(std_ID,stf_ID,class_ID,CI_Year,CI_Type,CI_Date,CI_CounselingDetail,CI_DateReorder,CI_Status,CI_Session)
                                    Values('" & strKey & "','" & ddl_CounselorName_SCSE.SelectedValue & "','" & Get_ClassID & "','" & ddl_Year.SelectedValue & "','Social Emotional','" & Final_Date_Data & "','Social Emotional','" & Final_Date_Data_Reorder & "','In Progress','" & ddl_CounselorSessionSE.SelectedValue & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet <> "0" Then

                            strSQL = "Select UPPER(Student_Name) from student_info where std_ID = '" & strKey & "'"
                            strRet = oCommon.getFieldValue(strSQL)

                            ShowMessage(" Fail To Set Counseling Date For Student -> " & strRet, MessageType.Error)
                            Exit For
                        End If

                    End If
                End If
            Next

            If strRet = "0" Then
                ShowMessage(" Set Counselling Session", MessageType.Success)
            End If

        End If
    End Sub

    Public Function checkDataSE()

        If ddl_CounselorName_SCSE.SelectedIndex = 0 Then
            ShowMessage(" Please Select Counselor Name ", MessageType.Error)
            Return False
        End If

        If txtDate_SCSE.Text.Length = 0 Then
            ShowMessage(" Please Select Date ", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Function BindDataSCINC(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLSCINC, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
            run_color()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLSCINC() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  Select A.CINC_ID, B.student_Name, B.student_ID, C.class_Name, D.staff_Name, A.CINC_Status, A.CINC_Status as Status_Color from counseling_inc A
                    Left join student_info B on A.std_ID = B.std_ID
                    Left join class_info C on A.class_ID = C.class_ID
                    left join staff_Info D on A.stf_ID = D.stf_ID"

        strWhere = "    where B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate') and B.student_Campus = '" & Session("SchoolCampus") & "'
                        and A.CINC_Year = '" & ddl_Year.SelectedValue & "' and C.class_year = '" & ddl_Year.SelectedValue & "'"

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += "   and C.class_Level = '" & ddl_Level.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CINC_SD_Reorder DESC, A.CINC_StartTime ASC"

        getSQLSCINC = tmpSQL & strWhere & strOrderby

        Return getSQLSCINC
    End Function

    Private Sub btn_RegisterCounselorINeedCounselling_ServerClick(sender As Object, e As EventArgs) Handles btn_RegisterCounselorINeedCounselling.ServerClick

        If checkDataINC() = True Then
            Dim i As Integer

            strSQL = ""
            strRet = ""

            ''Get Date
            Dim Get_Date_SS As String = txtDate_SCINC.Text.Substring(0, 2)
            Dim Get_Year_SS As String = txtDate_SCINC.Text.Substring(txtDate_SCINC.Text.Length - 4, 4)
            Dim Get_Month_SS As String = txtDate_SCINC.Text.Remove(0, 3)
            Get_Month_SS = Get_Month_SS.Remove(Get_Month_SS.Length - 5, 5)

            If Get_Month_SS = "January" Then
                Get_Month_SS = "01"
            ElseIf Get_Month_SS = "February" Then
                Get_Month_SS = "02"
            ElseIf Get_Month_SS = "March" Then
                Get_Month_SS = "03"
            ElseIf Get_Month_SS = "April" Then
                Get_Month_SS = "04"
            ElseIf Get_Month_SS = "May" Then
                Get_Month_SS = "05"
            ElseIf Get_Month_SS = "June" Then
                Get_Month_SS = "06"
            ElseIf Get_Month_SS = "July" Then
                Get_Month_SS = "07"
            ElseIf Get_Month_SS = "August" Then
                Get_Month_SS = "08"
            ElseIf Get_Month_SS = "September" Then
                Get_Month_SS = "09"
            ElseIf Get_Month_SS = "October" Then
                Get_Month_SS = "10"
            ElseIf Get_Month_SS = "November" Then
                Get_Month_SS = "11"
            ElseIf Get_Month_SS = "December" Then
                Get_Month_SS = "12"
            End If

            Dim Final_Date_Data As String = Get_Date_SS & "/" & Get_Month_SS & "/" & Get_Year_SS
            Dim Final_Date_Data_Reorder As String = Get_Year_SS & Get_Month_SS & Get_Date_SS

            For i = 0 To SCINCRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(SCINCRespondent.Rows(i).Cells(5).FindControl("chkSelectSCINC"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = SCINCRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''Update Into Counserlor INC Database
                        strSQL = "  Update counseling_inc set stf_ID = '" & ddl_CounselorName_SCINC.SelectedValue & "', CINC_Date = '" & Final_Date_Data & "', CINC_DateReorder = '" & Final_Date_Data_Reorder & "', CINC_Status = 'Approved', 
                                    CINC_Session = '" & ddl_CounselorSessionINC.SelectedValue & "' where CINC_ID = '" & strKey & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet <> "0" Then

                            strSQL = "Select UPPER(Student_Name) from student_info where std_ID = '" & strKey & "'"
                            strRet = oCommon.getFieldValue(strSQL)

                            ShowMessage(" Fail To Set Counseling Date For Student -> " & strRet, MessageType.Error)
                            Exit For
                        End If

                    End If
                End If
            Next

            If strRet = "0" Then
                ShowMessage(" Set Counselling Session", MessageType.Success)
                strRet = BindDataSCINC(SCINCRespondent)
            End If

        End If
    End Sub

    Public Function checkDataINC()

        If ddl_CounselorName_SCINC.SelectedIndex = 0 Then
            ShowMessage(" Please Select Counselor Name ", MessageType.Error)
            Return False
        End If

        If txtDate_SCINC.Text.Length = 0 Then
            ShowMessage(" Please Select Date ", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Sub SCINCRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles SCINCRespondent.RowEditing
        Dim strKeyID As String = SCINCRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Session("getStatus") = "SC_INC"
            Session("get_ID") = strKeyID
            Response.Redirect("admin_kaunselor_aktivitikaunselor_view.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SCINCRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles SCINCRespondent.RowDeleting
        Try
            Dim strKeyCode As String = SCINCRespondent.DataKeys(e.RowIndex).Value.ToString
            strSQL = "Delete counseling_inc where CINC_ID = '" & strKeyCode & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Delete Information", MessageType.Success)
                strRet = BindDataSCINC(SCINCRespondent)
            Else
                ShowMessage(" Unsuccessful Delete Information", MessageType.Error)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Year_VR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year_VR.SelectedIndexChanged
        Try
            If ddl_CounselorType_VR.SelectedValue = "Underachiever" Then
                Table_VCR_CGPA.Visible = True
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Discipline Issues" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = True
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_DI(VCRDI_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Social Emotional" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = True
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_SE(VCRSE_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "I Need Counselling" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = True
                strRet = BindDataVCR_INC(VCRINC_Respondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_CounselorType_VR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_CounselorType_VR.SelectedIndexChanged
        Try
            If ddl_CounselorType_VR.SelectedValue = "Underachiever" Then
                Table_VCR_CGPA.Visible = True
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Discipline Issues" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = True
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_DI(VCRDI_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Social Emotional" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = True
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_SE(VCRSE_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "I Need Counselling" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = True
                strRet = BindDataVCR_INC(VCRINC_Respondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_VR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level_VR.SelectedIndexChanged
        Try
            If ddl_CounselorType_VR.SelectedValue = "Underachiever" Then
                Table_VCR_CGPA.Visible = True
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Discipline Issues" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = True
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_DI(VCRDI_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Social Emotional" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = True
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_SE(VCRSE_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "I Need Counselling" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = True
                strRet = BindDataVCR_INC(VCRINC_Respondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Month_VR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Month_VR.SelectedIndexChanged
        Try
            If ddl_CounselorType_VR.SelectedValue = "Underachiever" Then
                Table_VCR_CGPA.Visible = True
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Discipline Issues" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = True
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_DI(VCRDI_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Social Emotional" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = True
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_SE(VCRSE_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "I Need Counselling" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = True
                strRet = BindDataVCR_INC(VCRINC_Respondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_session_VR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_session_VR.SelectedIndexChanged
        Try
            If ddl_CounselorType_VR.SelectedValue = "Underachiever" Then
                Table_VCR_CGPA.Visible = True
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Discipline Issues" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = True
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_DI(VCRDI_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "Social Emotional" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = True
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_SE(VCRSE_Respondent)

            ElseIf ddl_CounselorType_VR.SelectedValue = "I Need Counselling" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = True
                strRet = BindDataVCR_INC(VCRINC_Respondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataVCR_CGPA(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLVCR_CGPA, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            If Session("getViewButton") = "TRUE" Then
                gvTable.Columns(11).Visible = True
            Else
                gvTable.Columns(11).Visible = False
            End If

            objConn.Close()

            run_color()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLVCR_CGPA() As String

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim find_LevelAccess As String = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and (staff_Access = 'KSLR' or staff_Access = 'KKSLR')"
        Dim get_LevelAccess As String = oCommon.getFieldValue(find_LevelAccess)

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  select distinct A.CI_ID, B.student_Name, C.class_Level, C.class_Name, D.exam_Name, D.pngs, A.CI_Date, A.CI_StartTime, A.CI_EndTime, A.CI_Status, A.CI_Status as CI_Status_Color, A.CI_DateReorder from counselor_info A
                    left join student_info B on A.std_ID = B.std_ID
                    left join class_info C on A.class_ID = C.class_ID
                    left join student_png D on A.std_Png_ID = D.ID"

        strWhere = "    where A.CI_Year = '" & ddl_Year_VR.SelectedValue & "' and C.class_year = '" & ddl_Year_VR.SelectedValue & "' and D.year = '" & ddl_Year_VR.SelectedValue & "'
                        and A.CI_Type = 'Underachiever'
                        and B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate')"

        If get_LevelAccess.Length > 0 Then
            strWhere += "   and A.stf_ID = '" & stf_ID_Data & "'"
        End If

        If ddl_Level_VR.SelectedIndex > 0 Then
            strWhere += "   and C.class_Level = '" & ddl_Level_VR.SelectedValue & "'"
        End If

        If ddl_Month_VR.SelectedIndex > 0 Then
            Dim get_Date As String = ddl_Year_VR.SelectedValue & ddl_Month_VR.SelectedValue

            strWhere += "   and A.CI_DateReorder like '%" & get_Date & "%'"
        End If

        If ddl_session_VR.SelectedIndex > 0 Then
            strWhere += "   and A.CI_Session = '" & ddl_session_VR.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CI_DateReorder DESC, A.CI_StartTime ASC, B.student_Name ASC"

        getSQLVCR_CGPA = tmpSQL & strWhere & strOrderby

        Return getSQLVCR_CGPA
    End Function

    Private Function BindDataVCR_DI(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLVCR_DI, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            If Session("getViewButton") = "TRUE" Then
                gvTable.Columns(11).Visible = True
            Else
                gvTable.Columns(11).Visible = False
            End If

            objConn.Close()

            run_color()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLVCR_DI() As String

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim find_LevelAccess As String = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and (staff_Access = 'KSLR' or staff_Access = 'KKSLR')"
        Dim get_LevelAccess As String = oCommon.getFieldValue(find_LevelAccess)

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  select distinct A.CI_ID, B.student_Name, C.class_Level, C.class_Name, E.case_Category, E.case_MeritDemerit_Point, A.CI_Date, A.CI_StartTime, A.CI_EndTime, A.CI_Status, A.CI_Status as CI_Status_Color, A.CI_DateReorder from counselor_info A
                    left join student_info B on A.std_ID = B.std_ID
                    left join class_info C on A.class_ID = C.class_ID
                    left join dicipline_info D on A.disiplin_ID = D.disiplin_id
                    left join case_info E on D.case_ID = E.case_ID"

        strWhere = "    where A.CI_Year = '" & ddl_Year_VR.SelectedValue & "' and C.class_year = '" & ddl_Year_VR.SelectedValue & "'
                        and A.CI_Type = 'Discipline Issues'
                        and B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate')"

        If get_LevelAccess.Length > 0 Then
            strWhere += "   and A.stf_ID = '" & stf_ID_Data & "'"
        End If

        If ddl_Level_VR.SelectedIndex > 0 Then
            strWhere += "   and C.class_Level = '" & ddl_Level_VR.SelectedValue & "'"
        End If

        If ddl_Month_VR.SelectedIndex > 0 Then
            Dim get_Date As String = ddl_Year_VR.SelectedValue & ddl_Month_VR.SelectedValue

            strWhere += "   and A.CI_DateReorder like '%" & get_Date & "%'"
        End If

        If ddl_session_VR.SelectedIndex > 0 Then
            strWhere += "   and A.CI_Session = '" & ddl_session_VR.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CI_DateReorder DESC, A.CI_StartTime ASC, B.student_Name ASC"

        getSQLVCR_DI = tmpSQL & strWhere & strOrderby

        Return getSQLVCR_DI
    End Function

    Private Function BindDataVCR_SE(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLVCR_SE, strConn)
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

    Private Function getSQLVCR_SE() As String

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim find_LevelAccess As String = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and (staff_Access = 'KSLR' or staff_Access = 'KKSLR')"
        Dim get_LevelAccess As String = oCommon.getFieldValue(find_LevelAccess)

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  Select A.CI_ID, B.student_Name, C.class_Level, C.class_Name, A.CI_Date, A.CI_StartTime, A.CI_EndTime, A.CI_Status, A.CI_Status, A.CI_Status as CI_Status_Color, A.CI_DateReorder from counselor_info A
                    Left join student_info B on A.std_ID = B.std_ID
                    Left join class_info C on A.class_ID = C.class_ID"

        strWhere = "    where A.CI_Year = '" & ddl_Year_VR.SelectedValue & "' and C.class_year = '" & ddl_Year_VR.SelectedValue & "'
                        and A.CI_Type = 'Social Emotional'
                        and B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate')"

        If get_LevelAccess.Length > 0 Then
            strWhere += "   and A.stf_ID = '" & stf_ID_Data & "'"
        End If

        If ddl_Level_VR.SelectedIndex > 0 Then
            strWhere += "   and C.class_Level = '" & ddl_Level_VR.SelectedValue & "'"
        End If

        If ddl_Month_VR.SelectedIndex > 0 Then
            Dim get_Date As String = ddl_Year_VR.SelectedValue & ddl_Month_VR.SelectedValue

            strWhere += "   and A.CI_DateReorder like '%" & get_Date & "%'"
        End If

        If ddl_session_VR.SelectedIndex > 0 Then
            strWhere += "   and A.CI_Session = '" & ddl_session_VR.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CI_DateReorder DESC, A.CI_StartTime ASC, B.student_Name ASC"

        getSQLVCR_SE = tmpSQL & strWhere & strOrderby

        Return getSQLVCR_SE
    End Function

    Private Function BindDataVCR_INC(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLVCR_INC, strConn)
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

    Private Function getSQLVCR_INC() As String

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim find_LevelAccess As String = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and (staff_Access = 'KSLR' or staff_Access = 'KKSLR')"
        Dim get_LevelAccess As String = oCommon.getFieldValue(find_LevelAccess)

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  Select A.CINC_ID, B.student_Name, B.student_ID, D.staff_Name, A.CINC_Session, C.class_Name, A.CINC_Date, A.CINC_StartTime, A.CINC_EndTime, A.CINC_Status, A.CINC_Status as CINC_Status_Color, A.CINC_SD_Reorder from counseling_inc A
                    Left join student_info B on A.std_ID = B.std_ID
                    Left join class_info C on A.class_ID = C.class_ID
                    left join staff_Info D on A.stf_ID = D.stf_ID"

        strWhere = "    where A.CINC_Year = '" & ddl_Year_VR.SelectedValue & "' and C.class_year = '" & ddl_Year_VR.SelectedValue & "'
                        and B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate')"

        If get_LevelAccess.Length > 0 Then
            strWhere += "   and D.stf_ID = '" & stf_ID_Data & "'"
        End If

        If ddl_Level_VR.SelectedIndex > 0 Then
            strWhere += "   and C.class_Level = '" & ddl_Level_VR.SelectedValue & "'"
        End If

        If ddl_Month_VR.SelectedIndex > 0 Then
            Dim get_Date As String = ddl_Year_VR.SelectedValue & ddl_Month_VR.SelectedValue

            strWhere += "   and A.CINC_DateReorder like '%" & get_Date & "%'"
        End If

        If ddl_session_VR.SelectedIndex > 0 Then
            strWhere += "   and A.CINC_Session = '" & ddl_session_VR.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CINC_DateReorder DESC, A.CINC_StartTime ASC, B.student_Name ASC"

        getSQLVCR_INC = tmpSQL & strWhere & strOrderby

        Return getSQLVCR_INC
    End Function

    Private Sub VCRCGPA_Respondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles VCRCGPA_Respondent.RowEditing
        Dim strKeyID As String = VCRCGPA_Respondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Session("getStatus") = "VCR_CGPA"
            Session("get_CIID") = strKeyID
            Response.Redirect("admin_kaunselor_aktivitikaunselor_detail.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub VCRDI_Respondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles VCRDI_Respondent.RowEditing
        Dim strKeyID As String = VCRDI_Respondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Session("getStatus") = "VCR_DI"
            Session("get_CIID") = strKeyID
            Response.Redirect("admin_kaunselor_aktivitikaunselor_detail.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VCRSE_Respondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles VCRSE_Respondent.RowEditing
        Dim strKeyID As String = VCRSE_Respondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Session("getStatus") = "VCR_SE"
            Session("get_CIID") = strKeyID
            Response.Redirect("admin_kaunselor_aktivitikaunselor_detail.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VCRINC_Respondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles VCRINC_Respondent.RowEditing
        Dim strKeyID As String = VCRINC_Respondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Session("getStatus") = "VCR_INC"
            Session("get_CIID") = strKeyID
            Response.Redirect("admin_kaunselor_aktivitikaunselor_detail.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        If ddl_CounselorType_VR.SelectedValue = "Underachiever" Then

            For row = 0 To VCRCGPA_Respondent.Rows.Count - 1 Step row + 1
                lblDay = VCRCGPA_Respondent.Rows(row).Cells(9).FindControl("CI_Status_Color")
                If lblDay.Text = "In Progress" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Orange
                    lblDay.ForeColor = Drawing.Color.Orange
                    lblDay.CssClass = "lblAbsent"

                End If

                If lblDay.Text = "Done" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Green
                    lblDay.ForeColor = Drawing.Color.Green
                    lblDay.CssClass = "lblAttend"

                End If
            Next

        ElseIf ddl_CounselorType_VR.SelectedValue = "Discipline Issues" Then

            For row = 0 To VCRDI_Respondent.Rows.Count - 1 Step row + 1
                lblDay = VCRDI_Respondent.Rows(row).Cells(9).FindControl("CI_Status_Color")
                If lblDay.Text = "In Progress" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Orange
                    lblDay.ForeColor = Drawing.Color.Orange
                    lblDay.CssClass = "lblAbsent"

                End If

                If lblDay.Text = "Done" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Green
                    lblDay.ForeColor = Drawing.Color.Green
                    lblDay.CssClass = "lblAttend"

                End If
            Next

        ElseIf ddl_CounselorType_VR.SelectedValue = "Social Emotional" Then

            For row = 0 To VCRSE_Respondent.Rows.Count - 1 Step row + 1
                lblDay = VCRSE_Respondent.Rows(row).Cells(8).FindControl("CI_Status_Color")
                If lblDay.Text = "In Progress" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Orange
                    lblDay.ForeColor = Drawing.Color.Orange
                    lblDay.CssClass = "lblAbsent"

                End If

                If lblDay.Text = "Done" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Green
                    lblDay.ForeColor = Drawing.Color.Green
                    lblDay.CssClass = "lblAttend"

                End If
            Next

        ElseIf ddl_CounselorType_VR.SelectedValue = "I Need Counselling" Then

            For row = 0 To VCRINC_Respondent.Rows.Count - 1 Step row + 1
                lblDay = VCRINC_Respondent.Rows(row).Cells(8).FindControl("CINC_Status_Color")
                If lblDay.Text = "Approved" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.DarkOrchid
                    lblDay.ForeColor = Drawing.Color.DarkOrchid
                    lblDay.CssClass = "lblAbsent"

                End If

                If lblDay.Text = "Done" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Green
                    lblDay.ForeColor = Drawing.Color.Green
                    lblDay.CssClass = "lblAttend"

                End If

                If lblDay.Text = "Requested" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.DodgerBlue
                    lblDay.ForeColor = Drawing.Color.DodgerBlue
                    lblDay.CssClass = "lblAttend"

                End If
            Next

        End If

        If ddl_type.SelectedValue = "I Need Counselling" Then

            For row = 0 To SCINCRespondent.Rows.Count - 1 Step row + 1
                lblDay = SCINCRespondent.Rows(row).Cells(5).FindControl("Status_Color")
                If lblDay.Text = "Approved" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.DarkOrchid
                    lblDay.ForeColor = Drawing.Color.DarkOrchid
                    lblDay.CssClass = "lblAbsent"

                End If

                If lblDay.Text = "Done" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.Green
                    lblDay.ForeColor = Drawing.Color.Green
                    lblDay.CssClass = "lblAttend"

                End If

                If lblDay.Text = "Requested" Then

                    lblDay.Text = "OO"
                    lblDay.BackColor = Drawing.Color.DodgerBlue
                    lblDay.ForeColor = Drawing.Color.DodgerBlue
                    lblDay.CssClass = "lblAttend"

                End If
            Next
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class