Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing

Public Class student_counselling_detail
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Session("getStatus") = "VCR_CGPA" Then
                    txtbreadcrum1.Text = "View Counselling (Underachiever)"
                    Exam_Show.Visible = True
                    CGPA_Show.Visible = True
                    Case_Show.Visible = False
                    Demerit_Schow.Visible = False
                    SE_Status_One.Visible = True
                    SE_Status_Two.Visible = True
                    SE_Status_Three.Visible = True

                    Load_Attendance()
                    Load_NewCounselor()
                    VCR_CGPA_Load()

                    'ElseIf Session("getStatus") = "VCR_DI" Then
                    '    txtbreadcrum1.Text = "View Coiunselor Report (Discipline Issues)"
                    '    Exam_Show.Visible = False
                    '    CGPA_Show.Visible = False
                    '    Case_Show.Visible = True
                    '    Demerit_Schow.Visible = True
                    '    SE_Status_One.Visible = True
                    '    SE_Status_Two.Visible = True
                    '    SE_Status_Three.Visible = True

                    '    Load_Attendance()
                    '    Load_NewCounselor()
                    '    VCR_DI_Load()

                    'ElseIf Session("getStatus") = "VCR_SE" Then
                    '    txtbreadcrum1.Text = "View Coiunselor Report (Social Emoitional)"
                    '    Exam_Show.Visible = False
                    '    CGPA_Show.Visible = False
                    '    Case_Show.Visible = False
                    '    Demerit_Schow.Visible = False
                    '    SE_Status_One.Visible = False
                    '    SE_Status_Two.Visible = False
                    '    SE_Status_Three.Visible = False

                    '    Load_Attendance()
                    '    Load_NewCounselor()
                    '    VCR_DI_Load()
                End If

                previousPage.NavigateUrl = String.Format("~/pelajar_kaunseling.aspx?std_ID=" + Request.QueryString("std_ID"))

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Load_Attendance()
        strSQL = "select * from setting where idx = 'attendance counselling'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlattendance_VCRCGPA.DataSource = ds
            ddlattendance_VCRCGPA.DataTextField = "case_Name"
            ddlattendance_VCRCGPA.DataValueField = "case_ID"
            ddlattendance_VCRCGPA.DataBind()
            ddlattendance_VCRCGPA.Items.Insert(0, New ListItem("Select Student Attendance", String.Empty))
            ddlattendance_VCRCGPA.Items.Insert(1, New ListItem("Volunteer", "Volunteer"))
            ddlattendance_VCRCGPA.Items.Insert(2, New ListItem("Consulted", "Consulted"))
            ddlattendance_VCRCGPA.Items.Insert(3, New ListItem("Summoned", "Summoned"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Load_NewCounselor()
        strSQL = "  select A.stf_ID, A.staff_Name from staff_info A left join staff_Login B on A.stf_ID = B.stf_ID
                    where A.staff_Status = 'Access' and (B.staff_Access = 'KSLR' or B.staff_Access = 'KKSLR') and staff_Campus = '" & Session("SchoolCampus") & "'
                    order by staff_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_NewCounselor.DataSource = ds
            ddl_NewCounselor.DataTextField = "staff_Name"
            ddl_NewCounselor.DataValueField = "stf_ID"
            ddl_NewCounselor.DataBind()
            ddl_NewCounselor.Items.Insert(0, New ListItem("Select Counselor Name", String.Empty))
            ddl_NewCounselor.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub VCR_CGPA_Load()

        ''Get Year
        strSQL = "Select CI_Year from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        ''Get All The Information
        strSQL = "  Select B.student_Name, F.exam_StartDate, E.class_Name, D.exam_Name, D.pngs, A.CI_Date, A.CI_StartTime, A.CI_EndTime, A.CI_Session,
                    A.CI_Attendance, A.CI_ClienClassification, A.CI_Type, A.CI_StudentCondition, A.CI_StudentBackground, A.CI_CounselingRemark, A.CI_Action,
                    A.CI_DT_MB, A.CI_DT_MT, A.CI_DT_MDMM, A.CI_DT_MDMA, A.CI_DT_MPPM, A.CI_DT_MS from counselor_info A
                    left join student_info B on A.std_ID = B.std_ID
                    left join staff_info C on A.stf_ID = C.stf_ID
                    left join student_Png D on A.std_Png_ID = D.ID
                    left join class_info E on A.class_ID = E.class_ID
                    left join exam_info F on D.exam_Name = F.exam_Name
                    where A.CI_ID = '" & Session("get_CIID") & "' and F.exam_Year = '" & strRet & "'"

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
                txtstudentName_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                txtstudentName_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_StartDate")) Then
                txtexamDate_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("exam_StartDate")
            Else
                txtexamDate_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Name")) Then
                txtclassName_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("class_Name")
            Else
                txtclassName_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_Name")) Then
                txtexamName_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("exam_Name")
            Else
                txtexamName_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("pngs")) Then
                txtcgpa_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("pngs")
            Else
                txtcgpa_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_Date")) Then
                txtcounselingDate_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_Date")
            Else
                txtcounselingDate_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_StartTime")) Then
                txtstart_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_StartTime")
            Else
                txtstart_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_EndTime")) Then
                txtend_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_EndTime")
            Else
                txtend_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_Session")) Then
                txtsession_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_Session")
            Else
                txtsession_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_Attendance")) Then
                ddlattendance_VCRCGPA.SelectedValue = ds.Tables(0).Rows(0).Item("CI_Attendance")
            Else
                ddlattendance_VCRCGPA.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_ClienClassification")) Then
                txtclientClassification_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_ClienClassification")
            Else
                txtclientClassification_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_Type")) Then
                txtissuesType_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_Type")
            Else
                txtissuesType_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_StudentCondition")) Then
                txtstudentCondition_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_StudentCondition")
            Else
                txtstudentCondition_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_StudentBackground")) Then
                txtstudentBackground_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_StudentBackground")
            Else
                txtstudentBackground_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_CounselingRemark")) Then
                txtremark_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_CounselingRemark")
            Else
                txtremark_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_Action")) Then
                txtaction_VCRCGPA.Text = ds.Tables(0).Rows(0).Item("CI_Action")
            Else
                txtaction_VCRCGPA.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_DT_MB")) Then
                CB_VCRCGPA_MB.Checked = ds.Tables(0).Rows(0).Item("CI_DT_MB")
            Else
                CB_VCRCGPA_MB.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_DT_MT")) Then
                CB_VCRCGPA_MT.Checked = ds.Tables(0).Rows(0).Item("CI_DT_MT")
            Else
                CB_VCRCGPA_MT.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_DT_MDMM")) Then
                CB_VCRCGPA_MDMM.Checked = ds.Tables(0).Rows(0).Item("CI_DT_MDMM")
            Else
                CB_VCRCGPA_MDMM.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_DT_MDMA")) Then
                CB_VCRCGPA_MDMA.Checked = ds.Tables(0).Rows(0).Item("CI_DT_MDMA")
            Else
                CB_VCRCGPA_MDMA.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_DT_MPPM")) Then
                CB_VCRCGPA_MPPM.Checked = ds.Tables(0).Rows(0).Item("CI_DT_MPPM")
            Else
                CB_VCRCGPA_MPPM.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CI_DT_MS")) Then
                CB_VCRCGPA_MS.Checked = ds.Tables(0).Rows(0).Item("CI_DT_MS")
            Else
                CB_VCRCGPA_MS.Checked = "False"
            End If

        End If

        ''Counselling Date Chang Format
        Dim Get_Date_SS As String = txtcounselingDate_VCRCGPA.Text.Substring(0, 2)
        Dim Get_Year_SS As String = txtcounselingDate_VCRCGPA.Text.Substring(txtcounselingDate_VCRCGPA.Text.Length - 4, 4)
        Dim Get_Month_SS As String = txtcounselingDate_VCRCGPA.Text.Remove(0, 3)
        Get_Month_SS = Get_Month_SS.Remove(Get_Month_SS.Length - 5, 5)

        If Get_Month_SS = "01" Then
            Get_Month_SS = "January"
        ElseIf Get_Month_SS = "02" Then
            Get_Month_SS = "February"
        ElseIf Get_Month_SS = "03" Then
            Get_Month_SS = "March"
        ElseIf Get_Month_SS = "04" Then
            Get_Month_SS = "April"
        ElseIf Get_Month_SS = "05" Then
            Get_Month_SS = "May"
        ElseIf Get_Month_SS = "06" Then
            Get_Month_SS = "June"
        ElseIf Get_Month_SS = "07" Then
            Get_Month_SS = "July"
        ElseIf Get_Month_SS = "08" Then
            Get_Month_SS = "August"
        ElseIf Get_Month_SS = "09" Then
            Get_Month_SS = "September"
        ElseIf Get_Month_SS = "10" Then
            Get_Month_SS = "October"
        ElseIf Get_Month_SS = "11" Then
            Get_Month_SS = "November"
        ElseIf Get_Month_SS = "12" Then
            Get_Month_SS = "December"
        End If

        txtcounselingDate_VCRCGPA.Text = Get_Date_SS & " " & Get_Month_SS & " " & Get_Year_SS

        ''Examination Date Chang Format
        Dim Get_Date_SS_Exam As String = txtexamDate_VCRCGPA.Text.Substring(0, 2)
        Dim Get_Year_SS_Exam As String = txtexamDate_VCRCGPA.Text.Substring(txtexamDate_VCRCGPA.Text.Length - 4, 4)
        Dim Get_Month_SS_Exam As String = txtexamDate_VCRCGPA.Text.Remove(0, 3)
        Get_Month_SS_Exam = Get_Month_SS_Exam.Remove(Get_Month_SS_Exam.Length - 5, 5)

        If Get_Month_SS_Exam = "01" Then
            Get_Month_SS_Exam = "January"
        ElseIf Get_Month_SS_Exam = "02" Then
            Get_Month_SS_Exam = "February"
        ElseIf Get_Month_SS_Exam = "03" Then
            Get_Month_SS_Exam = "March"
        ElseIf Get_Month_SS_Exam = "04" Then
            Get_Month_SS_Exam = "April"
        ElseIf Get_Month_SS_Exam = "05" Then
            Get_Month_SS_Exam = "May"
        ElseIf Get_Month_SS_Exam = "06" Then
            Get_Month_SS_Exam = "June"
        ElseIf Get_Month_SS_Exam = "07" Then
            Get_Month_SS_Exam = "July"
        ElseIf Get_Month_SS_Exam = "08" Then
            Get_Month_SS_Exam = "August"
        ElseIf Get_Month_SS_Exam = "09" Then
            Get_Month_SS_Exam = "September"
        ElseIf Get_Month_SS_Exam = "10" Then
            Get_Month_SS_Exam = "October"
        ElseIf Get_Month_SS_Exam = "11" Then
            Get_Month_SS_Exam = "November"
        ElseIf Get_Month_SS_Exam = "12" Then
            Get_Month_SS_Exam = "December"
        End If

        txtexamDate_VCRCGPA.Text = Get_Date_SS_Exam & " " & Get_Month_SS_Exam & " " & Get_Year_SS_Exam

    End Sub

End Class