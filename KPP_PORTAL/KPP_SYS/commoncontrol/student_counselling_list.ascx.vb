Imports System.Data.SqlClient

Public Class student_counselling_list
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

                Dim getStatus As String = Request.QueryString("status")

                If getStatus = "VCO" Then ''View Counseling
                    txtbreadcrum1.Text = "View Counselling"

                    ViewCounselling.Visible = True
                    INeedCounselling.Visible = False

                    btnViewCounselor.Attributes("class") = "btn btn-info"
                    btnRegisterCounselor.Attributes("class") = "btn btn-default font"

                    SC_Year()
                    SC_Type()
                    SC_Session()

                    Table_VCR_CGPA.Visible = True
                    Table_VCR_DI.Visible = False
                    Table_VCR_SE.Visible = False
                    Table_VCR_INC.Visible = False

                ElseIf getStatus = "RCO" Then ''Register I Need Counseling
                    txtbreadcrum1.Text = "I Need Counselling"

                    ViewCounselling.Visible = False
                    INeedCounselling.Visible = True

                    txt_CBOther.Enabled = False

                    INC_Counsellor()

                    btnViewCounselor.Attributes("class") = "btn btn-default font"
                    btnRegisterCounselor.Attributes("class") = "btn btn-info"
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnViewCounselor_ServerClick(sender As Object, e As EventArgs) Handles btnViewCounselor.ServerClick
        Response.Redirect("pelajar_kaunseling.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=VCO")
    End Sub

    Private Sub btnRegisterCounselor_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterCounselor.ServerClick
        Response.Redirect("pelajar_kaunseling.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=RCO")
    End Sub

    Private Sub SC_Year()
        strSQL = "Select distinct Year from student_Level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' order by year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Year_VC.DataSource = ds
            ddl_Year_VC.DataTextField = "Year"
            ddl_Year_VC.DataValueField = "Year"
            ddl_Year_VC.DataBind()
            ddl_Year_VC.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_Year_VC.SelectedIndex = 0

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

            ddl_CounselorType_VC.DataSource = ds
            ddl_CounselorType_VC.DataTextField = "Parameter"
            ddl_CounselorType_VC.DataValueField = "Parameter"
            ddl_CounselorType_VC.DataBind()
            ddl_CounselorType_VC.Items.Insert(0, New ListItem("Select Counselor Type", String.Empty))
            ddl_CounselorType_VC.Items.Insert(1, New ListItem("Underachiever", "Underachiever"))
            ddl_CounselorType_VC.Items.Insert(2, New ListItem("Discipline Issues", "Discipline Issues"))
            ddl_CounselorType_VC.Items.Insert(3, New ListItem("Social Emotional", "Social Emotional"))
            ddl_CounselorType_VC.Items.Insert(3, New ListItem("I Need Counselling", "I Need Counselling"))
            ddl_CounselorType_VC.SelectedIndex = 0

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

            ddl_session_VC.DataSource = ds
            ddl_session_VC.DataTextField = "Parameter"
            ddl_session_VC.DataValueField = "Value"
            ddl_session_VC.DataBind()
            ddl_session_VC.Items.Insert(0, New ListItem("Select Session", String.Empty))
            ddl_session_VC.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddl_Year_VC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year_VC.SelectedIndexChanged
        Try
            If ddl_CounselorType_VC.SelectedValue = "Underachiever" Then
                Table_VCR_CGPA.Visible = True
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "Discipline Issues" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = True
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_DI(VCRDI_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "Social Emotional" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = True
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_SE(VCRSE_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "I Need Counselling" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = True
                strRet = BindDataVCR_INC(VCRINC_Respondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_CounselorType_VC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_CounselorType_VC.SelectedIndexChanged
        Try
            If ddl_CounselorType_VC.SelectedValue = "Underachiever" Then
                Table_VCR_CGPA.Visible = True
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "Discipline Issues" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = True
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_DI(VCRDI_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "Social Emotional" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = True
                Table_VCR_INC.Visible = False
                strRet = BindDataVCR_SE(VCRSE_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "I Need Counselling" Then
                Table_VCR_CGPA.Visible = False
                Table_VCR_DI.Visible = False
                Table_VCR_SE.Visible = False
                Table_VCR_INC.Visible = True
                strRet = BindDataVCR_INC(VCRINC_Respondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_session_VC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_session_VC.SelectedIndexChanged
        Try
            If ddl_CounselorType_VC.SelectedValue = "Underachiever" Then
                strRet = BindDataVCR_CGPA(VCRCGPA_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "Discipline Issues" Then
                strRet = BindDataVCR_DI(VCRDI_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "Social Emotional" Then
                strRet = BindDataVCR_SE(VCRSE_Respondent)

            ElseIf ddl_CounselorType_VC.SelectedValue = "I Need Counselling" Then
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

            objConn.Close()

            run_color()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLVCR_CGPA() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  select distinct A.CI_ID, A.CI_Session, E.staff_Name, C.class_Name, D.exam_Name, D.png, D.pngs, A.CI_Date, A.CI_StartTime, A.CI_EndTime, A.CI_Status, A.CI_Status as CI_Status_Color, A.CI_DateReorder from counselor_info A
                    left join student_info B on A.std_ID = B.std_ID
                    left join class_info C on A.class_ID = C.class_ID
                    left join student_png D on A.std_Png_ID = D.ID
                    left join staff_Info E on A.stf_ID = E.stf_ID"

        strWhere = "    where A.CI_Type = 'Underachiever'
                        and B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate') and A.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"

        If ddl_Year_VC.SelectedIndex > 0 Then
            strWhere += "   and  A.CI_Year = '" & ddl_Year_VC.SelectedValue & "' and C.class_year = '" & ddl_Year_VC.SelectedValue & "' and D.year = '" & ddl_Year_VC.SelectedValue & "'"
        End If

        If ddl_session_VC.SelectedIndex > 0 Then
            strWhere += "   and A.CI_Session = '" & ddl_session_VC.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CI_DateReorder DESC, A.CI_StartTime ASC"

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

            objConn.Close()

            run_color()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLVCR_DI() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  select distinct A.CI_ID, F.staff_Name, A.CI_Session, C.class_Name, E.case_Category, E.case_MeritDemerit_Point, A.CI_Date, A.CI_StartTime, A.CI_EndTime, A.CI_Status, A.CI_Status as CI_Status_Color, A.CI_DateReorder from counselor_info A
                    left join student_info B on A.std_ID = B.std_ID
                    left join class_info C on A.class_ID = C.class_ID
                    left join dicipline_info D on A.disiplin_ID = D.disiplin_id
                    left join case_info E on D.case_ID = E.case_ID
                    left join staff_Info F on A.stf_ID = F.stf_ID"

        strWhere = "    where A.CI_Type = 'Discipline Issues'
                        and B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate') and A.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"

        If ddl_Year_VC.SelectedIndex > 0 Then
            strWhere += "   and  A.CI_Year = '" & ddl_Year_VC.SelectedValue & "' and C.class_year = '" & ddl_Year_VC.SelectedValue & "' and D.year = '" & ddl_Year_VC.SelectedValue & "'"
        End If

        If ddl_session_VC.SelectedIndex > 0 Then
            strWhere += "   and A.CI_Session = '" & ddl_session_VC.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CI_DateReorder DESC, A.CI_StartTime ASC"

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

            objConn.Close()

            run_color()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLVCR_SE() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  Select A.CI_ID, D.staff_Name, A.CI_Session, C.class_Name, A.CI_Date, A.CI_StartTime, A.CI_EndTime, A.CI_Status, A.CI_Status as CI_Status_Color, A.CI_DateReorder from counselor_info A
                    Left join student_info B on A.std_ID = B.std_ID
                    Left join class_info C on A.class_ID = C.class_ID
                    left join staff_Info D on A.stf_ID = D.stf_ID"

        strWhere = "    where  A.CI_Type = 'Social Emotional'
                        and B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate') and A.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"

        If ddl_Year_VC.SelectedIndex > 0 Then
            strWhere += "   and  A.CI_Year = '" & ddl_Year_VC.SelectedValue & "' and C.class_year = '" & ddl_Year_VC.SelectedValue & "' and D.year = '" & ddl_Year_VC.SelectedValue & "'"
        End If

        If ddl_session_VC.SelectedIndex > 0 Then
            strWhere += "   and A.CI_Session = '" & ddl_session_VC.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CI_DateReorder DESC, A.CI_StartTime ASC"

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

            objConn.Close()

            run_color()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLVCR_INC() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        tmpSQL = "  Select A.CINC_ID, D.staff_Name, A.CINC_Session, C.class_Name, A.CINC_Date, A.CINC_StartTime, A.CINC_EndTime, A.CINC_Status, A.CINC_Status as CINC_Status_Color, A.CINC_SD_Reorder from counseling_inc A
                    Left join student_info B on A.std_ID = B.std_ID
                    Left join class_info C on A.class_ID = C.class_ID
                    left join staff_Info D on A.stf_ID = D.stf_ID"

        strWhere = "    where B.student_ID like '%M%' and (B.student_Status = 'Access' or B.student_Status = 'Graduate') and A.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"

        If ddl_Year_VC.SelectedIndex > 0 Then
            strWhere += "   and  A.CINC_Year = '" & ddl_Year_VC.SelectedValue & "' and C.class_year = '" & ddl_Year_VC.SelectedValue & "' and D.year = '" & ddl_Year_VC.SelectedValue & "'"
        End If

        If ddl_session_VC.SelectedIndex > 0 Then
            strWhere += "   and A.CINC_Session = '" & ddl_session_VC.SelectedValue & "'"
        End If

        strOrderby = "  order by A.CINC_SD_Reorder DESC, A.CINC_StartTime ASC"

        getSQLVCR_INC = tmpSQL & strWhere & strOrderby

        Return getSQLVCR_INC
    End Function

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        If ddl_CounselorType_VC.SelectedValue = "Underachiever" Then

            For row = 0 To VCRCGPA_Respondent.Rows.Count - 1 Step row + 1
                lblDay = VCRCGPA_Respondent.Rows(row).Cells(10).FindControl("CI_Status_Color")
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

        ElseIf ddl_CounselorType_VC.SelectedValue = "Discipline Issues" Then

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

        ElseIf ddl_CounselorType_VC.SelectedValue = "Social Emotional" Then

            For row = 0 To VCRSE_Respondent.Rows.Count - 1 Step row + 1
                lblDay = VCRSE_Respondent.Rows(row).Cells(7).FindControl("CI_Status_Color")
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

        ElseIf ddl_CounselorType_VC.SelectedValue = "I Need Counselling" Then

            For row = 0 To VCRINC_Respondent.Rows.Count - 1 Step row + 1
                lblDay = VCRINC_Respondent.Rows(row).Cells(7).FindControl("CINC_Status_Color")
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

    Private Sub INC_Counsellor()

        strSQL = "  select A.stf_ID, A.staff_Name from staff_info A left join staff_Login B on A.stf_ID = B.stf_ID
                    where A.staff_Status = 'Access' and (B.staff_Access = 'KSLR' or B.staff_Access = 'KKSLR') and staff_Campus = '" & Session("Student_Campus") & "'
                    order by staff_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_INC_CN.DataSource = ds
            ddl_INC_CN.DataTextField = "staff_Name"
            ddl_INC_CN.DataValueField = "stf_ID"
            ddl_INC_CN.DataBind()
            ddl_INC_CN.Items.Insert(0, New ListItem("Select Counsellor Name", String.Empty))
            ddl_INC_CN.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub CB_Other_CheckedChanged(sender As Object, e As EventArgs) Handles CB_Other.CheckedChanged

        If CB_Other.Checked = True Then
            txt_CBOther.Enabled = True
        ElseIf CB_Other.Checked = False Then
            txt_CBOther.Enabled = False
        End If

    End Sub

    Private Sub btn_register_INC_ServerClick(sender As Object, e As EventArgs) Handles btn_register_INC.ServerClick

        If checkData() = True Then

            strSQL = "  Select distinct A.class_ID from class_info A left join course B on A.class_ID = B.class_ID
                        Where B.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and A.class_campus = '" & Session("Student_Campus") & "' and A.class_type = 'Compulsory' and B.year = '" & Now.Year & "'"
            strRet = oCommon.getFieldValue(strSQL)

            strSQL = "  Insert Into counseling_inc(std_ID,stf_ID,class_ID,CINC_Status,CINC_MT,CINC_PL,CINC_CA,CINC_IA,CINC_AP,CINC_TR,CINC_TM,CINC_CG,CINC_VL,CINC_ACWL,CINC_AD,CINC_AT,CINC_DA,CINC_CAL,CINC_IS,CINC_BY,CINC_AY,CINC_SS,CINC_SH,CINC_RWF,CINC_SL,CINC_HS,CINC_OC,
                        CINC_HA,CINC_EP,CINC_WD,CINC_AX,CINC_RG,CINC_DP,CINC_FGW,CINC_SD,CINC_RL,CINC_PN,CINC_PH,CINC_SCD,CINC_MF,CINC_PNM,CINC_VA,CINC_IM,CINC_ADSAI,CINC_BC,CINC_DAS,CINC_SHA,CINC_Other,CINC_Other_Details,CINC_Year)
                        Values('" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "','" & ddl_INC_CN.SelectedValue & "','" & strRet & "','Requested','" & CB_MT.Checked & "','" & CB_PL.Checked & "','" & CB_CA.Checked & "','" & CB_IA.Checked & "','" & CB_AP.Checked & "','" & CB_TR.Checked & "',
                        '" & CB_TM.Checked & "','" & CB_CG.Checked & "','" & CB_VL.Checked & "','" & CB_ACWL.Checked & "','" & CB_AD.Checked & "','" & CB_AT.Checked & "','" & CB_DA.Checked & "','" & CB_CL.Checked & "','" & CB_IS.Checked & "','" & CB_BY.Checked & "',
                        '" & CB_AY.Checked & "','" & CB_SS.Checked & "','" & CB_SH.Checked & "','" & CB_RWF.Checked & "','" & CB_SL.Checked & "','" & CB_HS.Checked & "','" & CB_OC.Checked & "','" & CB_HA.Checked & "','" & CB_EP.Checked & "','" & CB_WD.Checked & "',
                        '" & CB_AX.Checked & "','" & CB_RG.Checked & "','" & CB_DP.Checked & "','" & CB_FGW.Checked & "','" & CB_SD.Checked & "','" & CB_RL.Checked & "','" & CB_PN.Checked & "','" & CB_PH.Checked & "','" & CB_SCD.Checked & "','" & CB_MF.Checked & "',
                        '" & CB_PNM.Checked & "','" & CB_VA.Checked & "','" & CB_IM.Checked & "','" & CB_ADSAI.Checked & "','" & CB_BC.Checked & "','" & CB_DAS.Checked & "','" & CB_SHA.Checked & "','" & CB_Other.Checked & "','" & txt_CBOther.Text & "','" & Now.Year & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Apply For Counselling", MessageType.Success)
            End If
        Else
            ShowMessage(" Please Select The Checkbox To Describe Your Issues", MessageType.Error)
        End If
    End Sub

    Public Function checkData()

        If CB_MT.Checked <> True And CB_PL.Checked <> True And CB_CA.Checked <> True And CB_IA.Checked <> True And CB_AP.Checked <> True And CB_TR.Checked <> True And CB_TM.Checked <> True And CB_CG.Checked <> True And CB_VL.Checked <> True And CB_ACWL.Checked <> True And CB_AD.Checked <> True And CB_AT.Checked <> True And CB_DA.Checked <> True And CB_CL.Checked <> True And CB_IS.Checked <> True And CB_BY.Checked <> True And CB_AY.Checked <> True And CB_SS.Checked <> True And CB_SH.Checked <> True And CB_RWF.Checked <> True And CB_SL.Checked <> True And CB_HS.Checked <> True And CB_OC.Checked <> True And CB_HA.Checked <> True And CB_EP.Checked <> True And CB_WD.Checked <> True And CB_AX.Checked <> True And CB_RG.Checked <> True And CB_DP.Checked <> True And CB_FGW.Checked <> True And CB_SD.Checked <> True And CB_RL.Checked <> True And CB_PN.Checked <> True And CB_PH.Checked <> True And CB_SCD.Checked <> True And CB_MF.Checked <> True And CB_PNM.Checked <> True And CB_VA.Checked <> True And CB_IM.Checked <> True And CB_ADSAI.Checked <> True And CB_BC.Checked <> True And CB_DAS.Checked <> True And CB_SHA.Checked <> True And CB_Other.Checked <> True Then
            Return False
        End If

        If CB_Other.Checked = True And txt_CBOther.Text.Length = 0 Then
            ShowMessage(" Please Fill In The Others Detail", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class