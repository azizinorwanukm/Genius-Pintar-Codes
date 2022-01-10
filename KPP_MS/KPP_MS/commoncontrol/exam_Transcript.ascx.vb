Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
'Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class exam_Transcript
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    '' connection to PERMATApintar® databasse
    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objstrConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                BtnGenerate.Visible = False
                BtnPrintkoko.Visible = False
                BtnPrintkokoBM.Visible = False
                btnOfficialBI.Visible = False
                btnOfficialBI.Visible = False

                Checking_MenuAccess_Load()

                If Session("getStatus") = "CT" Then ''Current Examination Transcript
                    txtbreadcrum1.Text = "Current Examination Transcript"

                    ExaminationTranscript.Visible = True
                    OfficialTranscript.Visible = False

                    btnExaminationTranscript.Attributes("class") = "btn btn-info"
                    btnOfficialTranscript.Attributes("class") = "btn btn-default font"

                    exam_info()
                    student_year_info()
                    year_list()
                    ddlcampus_info()
                    ddlprogram_info()
                    class_info()
                    load_page()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "OT" Then ''Official Transcript
                    txtbreadcrum1.Text = "Official Transcript"

                    ExaminationTranscript.Visible = False
                    OfficialTranscript.Visible = True

                    btnExaminationTranscript.Attributes("class") = "btn btn-default font"
                    btnOfficialTranscript.Attributes("class") = "btn btn-info"

                    official_year_list()
                    official_type_list()
                    official_campus_list()
                    official_program_list()
                    official_load_page()

                    strRet = BindDataOfficial(TranscriptRespondent)

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnExaminationTranscript.Visible = False
        btnOfficialTranscript.Visible = False
        ExaminationTranscript.Visible = False
        OfficialTranscript.Visible = False

        BtnGenerate.Visible = False
        BtnPrintkoko.Visible = False
        BtnPrintkokoBM.Visible = False
        btnOfficialBI.Visible = False
        btnOfficialBI.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ExaminationTranscript As String = ""
        Dim Get_OfficialTranscript As String = ""

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

            ''Get Function Button 1 Print In BI Data 
            strSQL = "  Select B.F1_PrintInBI From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1PrintInBI As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Print In BM Data 
            strSQL = "  Select B.F1_PrintInBM From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1PrintInBM As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Generate GPA & CGPA Data 
            strSQL = "  Select B.F1_GenerateGPACGPA From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1GenerateGPACGPA As String = oCommon.getFieldValue(strSQL)


            If find_Data_SubMenu2 = "Current Examination Transcript" And find_Data_SubMenu2.Length > 0 Then
                btnExaminationTranscript.Visible = True
                ExaminationTranscript.Visible = True

                Get_ExaminationTranscript = "TRUE"

                If find_Data_F1GenerateGPACGPA.Length > 0 And find_Data_F1GenerateGPACGPA = "TRUE" Then
                    BtnGenerate.Visible = True
                End If

                If find_Data_F1PrintInBI.Length > 0 And find_Data_F1PrintInBI = "TRUE" Then
                    BtnPrintkoko.Visible = True
                End If

                If find_Data_F1PrintInBM.Length > 0 And find_Data_F1PrintInBM = "TRUE" Then
                    BtnPrintkokoBM.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Official Transcript" And find_Data_SubMenu2.Length > 0 Then
                btnOfficialTranscript.Visible = True
                OfficialTranscript.Visible = True

                Get_OfficialTranscript = "TRUE"

                If find_Data_F1PrintInBI.Length > 0 And find_Data_F1PrintInBI = "TRUE" Then
                    btnOfficialBI.Visible = True
                End If

                If find_Data_F1PrintInBM.Length > 0 And find_Data_F1PrintInBM = "TRUE" Then
                    btnOfficialBM.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnExaminationTranscript.Visible = True
                btnOfficialTranscript.Visible = True
                ExaminationTranscript.Visible = True
                OfficialTranscript.Visible = True

                BtnGenerate.Visible = True
                BtnPrintkoko.Visible = True
                BtnPrintkokoBM.Visible = True
                btnOfficialBI.Visible = True
                btnOfficialBI.Visible = True

                Get_ExaminationTranscript = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "CT" Or Session("getStatus") = "OT" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "CT" And Session("getStatus") <> "OT" Then
            If Get_ExaminationTranscript = "TRUE" Then
                Data_If_Not_Group_Status = "CT"
            ElseIf Get_OfficialTranscript = "TRUE" Then
                Data_If_Not_Group_Status = "OT"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ExaminationTranscript = "TRUE" And Data_If_Not_Group_Status = "CT" Then
                Session("getStatus") = "CT"
            ElseIf Get_OfficialTranscript = "TRUE" And Data_If_Not_Group_Status = "OT" Then
                Session("getStatus") = "OT"
            End If
        End If

    End Sub

    Private Sub btnExaminationTranscript_ServerClick(sender As Object, e As EventArgs) Handles btnExaminationTranscript.ServerClick
        Session("getStatus") = "CT"
        Response.Redirect("admin_peperiksaan_transkrip.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnOfficialTranscript_ServerClick(sender As Object, e As EventArgs) Handles btnOfficialTranscript.ServerClick
        Session("getStatus") = "OT"
        Response.Redirect("admin_peperiksaan_transkrip.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub exam_info()
        Try
            Dim strLevelSql As String = ""

            If ddlstudent_Year.SelectedValue = "Foundation 1" Or ddlstudent_Year.SelectedValue = "Level 1" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 1' or Parameter = 'Exam 2' or Parameter = 'Exam 3' or Parameter = 'Exam 4')"

            ElseIf ddlstudent_Year.SelectedValue = "Foundation 2" Or ddlstudent_Year.SelectedValue = "Level 2" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 5' or Parameter = 'Exam 6' or Parameter = 'Exam 7' or Parameter = 'Exam 8')"

            ElseIf ddlstudent_Year.SelectedValue = "Foundation 3" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 9' or Parameter = 'Exam 10' or Parameter = 'Exam 11' or Parameter = 'Exam 12')"

            End If

            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "ExamTable")

            ddlexam_Name.DataSource = levds
            ddlexam_Name.DataValueField = "Parameter"
            ddlexam_Name.DataTextField = "Parameter"
            ddlexam_Name.DataBind()
            ddlexam_Name.Items.Insert(0, New ListItem("Select Exam", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlexam_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlexam_Name.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub class_info()

        strSQL = "select * from class_info where class_year = '" & ddlyear.SelectedValue & "' and class_level = '" & ddlstudent_Year.SelectedValue & "' and class_type = 'Compulsory' and course_Program = '" & ddlprogram.SelectedValue & "' and class_Campus = '" & ddlcampus.SelectedValue & "' order by class_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlclass.DataSource = ds
            ddlclass.DataTextField = "class_Name"
            ddlclass.DataValueField = "class_ID"
            ddlclass.DataBind()
            ddlclass.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlclass.Items.Insert(1, New ListItem("All", "All"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlclass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlclass.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT distinct year from student_Level where year ='" & Now.Year & "'"

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
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlyear.SelectedValue = ""
            End If
        End If

    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlcampus_info()
        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"

        ElseIf Session("SchoolCampus") = "PGPN" And Session("user_position") <> "KSLR" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "

        ElseIf Session("SchoolCampus") = "PGPN" And Session("user_position") = "KSLR" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'PGPN'"

        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcampus.DataSource = ds
            ddlcampus.DataTextField = "Parameter"
            ddlcampus.DataValueField = "Value"
            ddlcampus.DataBind()
            ddlcampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlprogram_info()
        If ddlcampus.SelectedValue = "APP" Then
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

            ddlprogram.DataSource = ds
            ddlprogram.DataTextField = "Parameter"
            ddlprogram.DataValueField = "Value"
            ddlprogram.DataBind()
            ddlprogram.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            exam_info()
            student_year_info()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlcampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcampus.SelectedIndexChanged
        Try
            class_info()
            ddlprogram_info()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlprogram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlprogram.SelectedIndexChanged
        Try
            class_info()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub student_year_info()
        strSQL = "select Parameter from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstudent_Year.DataSource = ds
            ddlstudent_Year.DataTextField = "Parameter"
            ddlstudent_Year.DataValueField = "Parameter"
            ddlstudent_Year.DataBind()
            ddlstudent_Year.Items.Insert(0, New ListItem("Select Level", String.Empty))

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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_Png.pngs DESC"

        tmpSQL = "select distinct student_info.std_ID, student_info.student_Name, student_info.student_ID, student_info.student_ID, class_info.class_Level,
                  student_Png.png, student_Png.pngs,class_info.class_Name, student_Png.exam_Name 
                  from student_Png
                  left join student_info on student_Png.std_ID = student_info.std_ID             
                  left join course on student_info.std_ID = course.std_ID
                  left join class_info on course.class_ID = class_info.class_ID"
        strWhere = " where student_Png.year = '" & ddlyear.SelectedValue & "' and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%') and class_info.course_Program = '" & ddlprogram.SelectedValue & "' and class_info.class_Campus = '" & ddlcampus.SelectedValue & "' and student_info.student_Campus = '" & ddlcampus.SelectedValue & "' and student_info.student_Stream = '" & ddlprogram.SelectedValue & "'"
        strWhere += " and class_info.class_Level = '" & ddlstudent_Year.SelectedValue & "'"
        strWhere += " and student_Png.exam_Name = '" & ddlexam_Name.SelectedValue & "'"

        If ddlclass.SelectedValue <> "All" Then
            strWhere += " and class_info.class_ID = '" & ddlclass.SelectedValue & "'"
            strWhere += " and class_info.class_year = '" & ddlyear.SelectedValue & "'"

        ElseIf ddlclass.SelectedValue = "All" Then
            strWhere += " and class_info.class_type = 'Compulsory'"
            strWhere += " and class_info.class_year = '" & ddlyear.SelectedValue & "'"

        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    ''calculate png academic
    Private Sub Cal_PNG(ByVal strKey As String)

        Dim data_Student As String = ""

        Dim get_ENG As String = "   select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                    where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
        Dim data_ENGLITERATURE As String = oCommon.getFieldValue(get_ENG)

        If data_ENGLITERATURE.Length > 0 Then

            data_Student = oCommon.PNG_ENG(strKey, ddlexam_Name.SelectedValue, ddlyear.SelectedValue)

        ElseIf data_ENGLITERATURE.Length = 0 Then

            data_Student = oCommon.PNG(strKey, ddlexam_Name.SelectedValue, ddlyear.SelectedValue)

        End If

    End Sub

    Private Sub Cal_PNGK(ByVal strKey As String)

        ''get student level
        Dim FindStudentLevel As String = "select distinct student_Level from student_Level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "'"
        Dim GetStudentLevel As String = oCommon.getFieldValue(FindStudentLevel)

        Dim findCampus As String = "Select student_Campus from student_info where std_ID = '" & strKey & "'"
        Dim getCampus As String = oCommon.getFieldValue(findCampus)

        Dim check_academic_percen As String = ""
        Dim Confirm_Academic As String = ""
        Dim check_portfolio_percen As String = ""
        Dim Confirm_Portfolio As String = ""
        Dim check_cocuricullum_percen As String = ""
        Dim Confirm_Cocuricullum As String = ""
        Dim check_research_percen As String = ""
        Dim Confirm_Research As String = ""
        Dim check_self_percen As String = ""
        Dim Confirm_Self As String = ""

        'get academic percentage on / off
        check_academic_percen = "select Value from setting where Type = 'Academic " & GetStudentLevel & " Percentage'"
        Confirm_Academic = oCommon.getFieldValue(check_academic_percen)

        ''get Portfolio percentage on / off
        check_portfolio_percen = "select Value from setting where Type = 'Portfolio " & GetStudentLevel & " Percentage'"
        Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

        ''get cocuricullum percentage on / off
        check_cocuricullum_percen = "select Value from setting where Type = 'Cocurricular " & GetStudentLevel & " Percentage'"
        Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

        ''get research percentage on / off
        check_research_percen = "select Value from setting where Type = 'Research " & GetStudentLevel & " Percentage'"
        Confirm_Research = oCommon.getFieldValue(check_research_percen)

        ''get self development percentage on / off
        check_self_percen = "select Value from setting where Type = 'Self Development " & GetStudentLevel & " Percentage'"
        Confirm_Self = oCommon.getFieldValue(check_self_percen)

        Dim percen_selfdevelopment As Decimal = 0.0
        Dim percen_cocuricullum As Decimal = 0.0
        Dim percen_academic As Decimal = 0.0
        Dim percen_research As Decimal = 0.0
        Dim percen_portfolio As Decimal = 0.0

        Dim check_pngs_exist As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
        Dim exist_pngs As String = oCommon.getFieldValue(check_pngs_exist)

        ''check studdent_type
        Dim find_std_type As String = "select student_type from student_Png where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "'"
        Dim data_std_type As String = oCommon.getFieldValue(find_std_type)

        '' Confirm Academic
        If Confirm_Academic = "On" Or Confirm_Academic = "on" Then
            Dim data_academic_percen As String = "select Parameter from setting where Type = 'Academic " & GetStudentLevel & " Percentage'"
            percen_academic = Decimal.Parse(oCommon.getFieldValue(data_academic_percen))
        End If

        '' Confirm Portfolio
        If Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
            Dim data_portfolio_percen As String = "select Parameter from setting where Type = 'Portfolio " & GetStudentLevel & " Percentage'"
            percen_portfolio = Decimal.Parse(oCommon.getFieldValue(data_portfolio_percen))
        End If

        '' Confirm Research
        If Confirm_Research = "On" Or Confirm_Research = "on" Then
            Dim data_research_percen As String = "select Parameter from setting where Type = 'Research " & GetStudentLevel & " Percentage'"
            percen_research = Decimal.Parse(oCommon.getFieldValue(data_research_percen))
        End If

        '' Confirm Cocuricullum
        If Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
            Dim data_cocuricullum_percen As String = "select Parameter from setting where Type = 'Cocurricular " & GetStudentLevel & " Percentage'"
            percen_cocuricullum = Decimal.Parse(oCommon.getFieldValue(data_cocuricullum_percen))
        End If

        '' Confirm Self Developmemnt
        If Confirm_Self = "On" Or Confirm_Self = "on" Then
            Dim data_self_percen As String = "select Parameter from setting where Type = 'Self Development " & GetStudentLevel & " Percentage'"
            percen_selfdevelopment = Decimal.Parse(oCommon.getFieldValue(data_self_percen))
        End If

        ''calculate pngk based on examination
        ''sum all png
        Dim SumPng As String = ""
        Dim count_loop As Integer = 0
        Dim dataSumPng As Decimal = 0.00
        Dim answer As Decimal = 0.00
        Dim pngk As Decimal = 0.00
        Dim string_i As String = ""
        Dim string_j As String = ""

        Dim myChars() As Char = ddlexam_Name.SelectedValue.ToCharArray()
        For Each ch As Char In myChars
            If Char.IsDigit(ch) Then
                If string_i = "" Then
                    string_i = ch
                Else
                    string_j = ch
                End If
            End If
        Next

        Dim data As String = string_i + string_j
        Dim i As Integer = Integer.Parse(data)

        For value As Integer = 1 To i

            Dim select_yearPNG As String = "Select year from student_Png where std_ID = '" & strKey & "' and student_type = '" & data_std_type & "' and exam_Name = 'Exam " & value & "'"
            Dim get_yearPNG As String = oCommon.getFieldValue(select_yearPNG)

            If get_yearPNG = "2020" Then

                If value = 2 Or value = 4 Or value = 6 Or value = 8 Or value = 10 Or value = 12 Then

                    SumPng = "select png from student_Png where std_ID = '" & strKey & "' and student_type = '" & data_std_type & "' and exam_Name = 'Exam " & value & "'"

                    Dim convert_string As String = oCommon.getFieldValue(SumPng)

                    If convert_string = "" Then
                        convert_string = "0"
                    Else
                        count_loop = count_loop + 1
                    End If

                    dataSumPng = Decimal.Parse(convert_string)

                    answer = answer + dataSumPng

                End If
            Else

                SumPng = "select png from student_Png where std_ID = '" & strKey & "' and student_type = '" & data_std_type & "' and exam_Name = 'Exam " & value & "'"

                Dim convert_string As String = oCommon.getFieldValue(SumPng)

                If convert_string = "" Then
                    convert_string = "0"
                Else
                    count_loop = count_loop + 1
                End If

                dataSumPng = Decimal.Parse(convert_string)

                answer = answer + dataSumPng

            End If
        Next

        If count_loop <> i Then
            answer = answer / count_loop
        Else
            answer = answer / i
        End If

        pngk = Math.Round(answer, 2, MidpointRounding.AwayFromZero)

        strSQL = "select exam_EndDate from exam_Info where exam_year = '" & ddlyear.SelectedValue & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "'and exam_Institutions = '" & getCampus & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim get_date As String = DateTime.Now.ToString("dd/MM/yyyy")

        If strRet >= get_date Then

            ''step to insert PNGK value into db
            strSQL = "UPDATE student_Png SET pngs ='" & pngk & "' , komp_akademik = '" & percen_academic & "', komp_kokurikulum = '" & percen_cocuricullum & "', komp_portfolio = '" & percen_portfolio & "',
                      komp_penyelidikan = '" & percen_research & "', komp_kendiri = '" & percen_selfdevelopment & "', stat_akademik = '" & Confirm_Academic & "', stat_kokurikulum = '" & Confirm_Cocuricullum & "',
                      stat_portfolio = '" & Confirm_Portfolio & "', stat_penyelidikan = '" & Confirm_Research & "', stat_kendiri = '" & Confirm_Self & "'
                      WHERE std_ID ='" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"

            strRet = oCommon.ExecuteSQL(strSQL)

            ShowMessage("Creating Student CGPA", MessageType.Success)
        Else
            ShowMessage("Unable To Generate GPA/CGPA Due To Exceeding Time Limit", MessageType.Success)
        End If

    End Sub

    Private Sub BtnGenerate_ServerClick(sender As Object, e As EventArgs) Handles BtnGenerate.ServerClick

        Dim i As Integer = 0
        Dim value As String = ""

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Cal_PNG(strKey)
                    Cal_PNGK(strKey)

                End If
            End If
        Next

        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlstudent_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlstudent_Year.SelectedIndexChanged
        exam_info()
        class_info()
    End Sub

    Private Sub BtnPrintkoko_ServerClick(sender As Object, e As EventArgs) Handles BtnPrintkoko.ServerClick
        Try
            Print_Transcript("BI")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnPrintkokoBM_ServerClick(sender As Object, e As EventArgs) Handles BtnPrintkokoBM.ServerClick
        Try
            Print_Transcript("BM")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Print_Transcript(Lang As String)

        Dim tmpSQL As String = ""
        Dim tmpSQL_Nama As String = ""
        Dim tmpSQL_Kod As String = ""
        Dim tmpSQL_Gred As String = ""
        Dim tmpSQL_PNG As String = ""
        Dim tmpSQL_Hour As String = ""
        Dim tmpSQL_Total As String = ""

        Dim tmpSQL_SD_GRED As String = ""
        Dim tmpsql_SD_PNG As String = ""
        Dim tmpsql_SD_KOD As String = ""

        Dim tmpsql_Portfolio_GRED As String = ""
        Dim tmpsql_Portfolio_PNG As String = ""
        Dim tmpsql_Portfolio_KOD As String = ""

        Dim tmpsql_Penyelidikan_Gred As String = ""
        Dim tmpsql_Penyelidikan_PNG As String = ""
        Dim tmpsql_Penyelidikan_KOD As String = ""

        Dim tmpsql_EL_Subject As String = ""
        Dim tmpsql_EL_GRED As String = ""
        Dim tmpsql_EL_PNG As String = ""
        Dim tmpsql_EL_KOD As String = ""
        Dim tmpsql_EL_HOUR As String = ""
        Dim tmpsql_EL_TOTAL As String = ""

        Dim tmpsql_KOKO_KOD_SUKAN As String = ""
        Dim tmpsql_KOKO_KOD_UNIFORM As String = ""
        Dim tmpsql_KOKO_KOD_KELAB As String = ""
        Dim tmpsql_KOKO_NAMA_SUKAN As String = ""
        Dim tmpsql_KOKO_NAMA_KELAB As String = ""
        Dim tmpsql_KOKO_NAMA_UNIFORM As String = ""
        Dim tmpsql_KOKO_GRED As String = ""
        Dim tmpsql_KOKO_PNG As String = ""

        Dim errorCount As Integer = 0
        Dim i As Integer = 0
        Dim Test As New StringBuilder()

        'get englih literture on / off
        Dim check_Eng_Literature As String = "select Value from setting where Type = 'English Literature'"
        Dim Confirm_Eng_Literature As String = oCommon.getFieldValue(check_Eng_Literature)

        Test.AppendLine("<div id='data' style='display:none'>")
        Test.AppendLine("<div id='dataPrint'> ")

        If Lang = "BM" Then

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''''''''''''''''''''''''''''''checking student 
                        'get Portfolio percentage on / off
                        Dim check_portfolio_percen As String = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim Confirm_Portfolio As String = oCommon.getFieldValue(check_portfolio_percen)

                        ''get cocuricullum percentage on / off
                        Dim check_cocuricullum_percen As String = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim Confirm_Cocuricullum As String = oCommon.getFieldValue(check_cocuricullum_percen)

                        ''get research percentage on / off
                        Dim check_research_percen As String = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim Confirm_Research As String = oCommon.getFieldValue(check_research_percen)

                        ''get self development percentage on / off
                        Dim check_self_percen As String = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim Confirm_Self As String = oCommon.getFieldValue(check_self_percen)

                        ''print subject name 
                        tmpSQL_Nama = "SELECT subject_NameBM FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

                        ''print subject code
                        tmpSQL_Kod = "SELECT subject_code FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

                        ''print subject grade
                        tmpSQL_Gred = "SELECT grade FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

                        ''print subject png
                        tmpSQL_PNG = "SELECT gpa FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

                        ''print subject credit hour
                        tmpSQL_Hour = "SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

                        ''print subject credit hour
                        tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)



                        tmpSQL = "select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                        Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select SUM(total) FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                        Dim total_Total As String = oCommon.getFieldValue(tmpSQL)


                        Dim DS_Nama As New DataTable
                        Dim DS_Kod As New DataTable
                        Dim DS_Gred As New DataTable
                        Dim DS_PNG As New DataTable
                        Dim DS_Hour As New DataTable
                        Dim DS_Total As New DataTable

                        Dim DSSelfdevelopment_GRED As New DataTable
                        Dim DSSelfdevelopment_PNG As New DataTable
                        Dim DSSelfdevelopment_KOD As New DataTable

                        Dim DSEnglish_literature_SUBJECT As New DataTable
                        Dim DSEnglish_literature_GRED As New DataTable
                        Dim DSEnglish_literature_PNG As New DataTable
                        Dim DSEnglish_literature_KOD As New DataTable
                        Dim DSEnglish_literature_HOUR As New DataTable
                        Dim DSEnglish_literature_TOTAL As New DataTable

                        Dim DSResearch_GRED As New DataTable
                        Dim DSResearch_PNG As New DataTable
                        Dim DSResearch_KOD As New DataTable

                        Dim DSPortfolio_GRED As New DataTable
                        Dim DSPortfolio_PNG As New DataTable
                        Dim DSPortfolio_KOD As New DataTable

                        Dim DSCocuricullum_KOD_SUKAN As New DataTable
                        Dim DSCocuricullum_KOD_UNIFORM As New DataTable
                        Dim DSCocuricullum_KOD_KELAB As New DataTable
                        Dim DSCocuricullum_NAMA_SUKAN As New DataTable
                        Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
                        Dim DSCocuricullum_NAMA_KELAB As New DataTable
                        Dim DSCocuricullum_GRED As New DataTable
                        Dim DSCocuricullum_PNG As New DataTable

                        Dim total_Credit_EL As String = "0"
                        Dim total_Total_EL As String = "0"

                        ''print english literature
                        If Confirm_Eng_Literature = "On" Then
                            tmpsql_EL_Subject = "SELECT subject_NameBM FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_GRED = "SELECT grade FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_PNG = "SELECT gpa FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_KOD = "SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_HOUR = "SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_TOTAL = "SELECT total FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "


                            tmpSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                            total_Credit_EL = oCommon.getFieldValue(tmpSQL)

                            If total_Credit_EL.Length = 0 Then
                                total_Credit_EL = "0"
                            End If

                            tmpSQL = "select total FROM [ExamSlip_English_Literature] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                            total_Total_EL = oCommon.getFieldValue(tmpSQL)

                            If total_Total_EL.Length = 0 Then
                                total_Total_EL = "0"
                            End If

                            Dim SQEnglish_Literature_SUBJECT As New SqlDataAdapter(tmpsql_EL_Subject, strConn)
                            Dim SQEnglish_Literature_GRED As New SqlDataAdapter(tmpsql_EL_GRED, strConn)
                            Dim SQEnglish_Literature_PNG As New SqlDataAdapter(tmpsql_EL_PNG, strConn)
                            Dim SQEnglish_Literature_KOD As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
                            Dim SQEnglish_Literature_HOUR As New SqlDataAdapter(tmpsql_EL_HOUR, strConn)
                            Dim SQEnglish_Literature_TOTAL As New SqlDataAdapter(tmpsql_EL_TOTAL, strConn)

                            Try
                                SQEnglish_Literature_SUBJECT.Fill(DSEnglish_literature_SUBJECT)
                                SQEnglish_Literature_GRED.Fill(DSEnglish_literature_GRED)
                                SQEnglish_Literature_KOD.Fill(DSEnglish_literature_KOD)
                                SQEnglish_Literature_PNG.Fill(DSEnglish_literature_PNG)
                                SQEnglish_Literature_HOUR.Fill(DSEnglish_literature_HOUR)
                                SQEnglish_Literature_TOTAL.Fill(DSEnglish_literature_TOTAL)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print Portfolio
                        If Confirm_Portfolio = "On" Then
                            tmpsql_Portfolio_GRED = "SELECT grade FROM [ExamSlip_Portfolio] 
                                                         where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                         where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                         where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQPortfolio_GRED As New SqlDataAdapter(tmpsql_Portfolio_GRED, strConn)
                            Dim SQPortfolio_PNG As New SqlDataAdapter(tmpsql_Portfolio_PNG, strConn)
                            Dim SQPortfolio_KOD As New SqlDataAdapter(tmpsql_Portfolio_KOD, strConn)

                            Try
                                SQPortfolio_GRED.Fill(DSPortfolio_GRED)
                                SQPortfolio_PNG.Fill(DSPortfolio_PNG)
                                SQPortfolio_KOD.Fill(DSPortfolio_KOD)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print research 
                        If Confirm_Research = "On" Then
                            tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                            where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_Penyelidikan_PNG = "SELECT gpa FROM [ExamSlip_Research] 
                                                            where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_Penyelidikan_KOD = "SELECT subject_code FROM [ExamSlip_Research] 
                                                            where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQResearch_GRED As New SqlDataAdapter(tmpsql_Penyelidikan_Gred, strConn)
                            Dim SQResearch_PNG As New SqlDataAdapter(tmpsql_Penyelidikan_PNG, strConn)
                            Dim SQResearch_KOD As New SqlDataAdapter(tmpsql_Penyelidikan_KOD, strConn)

                            Try
                                SQResearch_GRED.Fill(DSResearch_GRED)
                                SQResearch_PNG.Fill(DSResearch_PNG)
                                SQResearch_KOD.Fill(DSResearch_KOD)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print self development
                        If Confirm_Self = "On" Then
                            Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' "
                            Dim getLevel As String = oCommon.getFieldValue(level)

                            If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                                tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                                tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                                tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                                tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            End If

                            Dim SQSelfdevelopment_GRED As New SqlDataAdapter(tmpSQL_SD_GRED, strConn)
                            Dim SQSelfdevelopment_PNG As New SqlDataAdapter(tmpsql_SD_PNG, strConn)
                            Dim SQSelfdevelopment_KOD As New SqlDataAdapter(tmpsql_SD_KOD, strConn)

                            Try
                                SQSelfdevelopment_GRED.Fill(DSSelfdevelopment_GRED)
                                SQSelfdevelopment_PNG.Fill(DSSelfdevelopment_PNG)
                                SQSelfdevelopment_KOD.Fill(DSSelfdevelopment_KOD)
                            Catch ex As Exception

                            End Try
                        End If

                        '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                        If Confirm_Cocuricullum = "On" Then

                            Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim getStudent As String = oCommon.getFieldValue(studentData)

                            If ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 10" Then

                                tmpsql_KOKO_PNG = "select koko_pelajar.PNGP1 from koko_pelajar
                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
                                Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
                                Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

                                Try
                                    SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                    SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                    SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                    SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                    SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                    SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                    SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                                    SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 12" Then

                                tmpsql_KOKO_PNG = "select koko_pelajar.PNGP2 from koko_pelajar
                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
                                Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
                                Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

                                Try
                                    SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                    SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                    SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                    SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                    SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                    SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                    SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                                    SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                                Catch ex As Exception

                                End Try

                            End If
                        End If

                        Try
                            SQA.Fill(DS_Nama)
                            SQACODE.Fill(DS_Kod)
                            SQAPNG.Fill(DS_PNG)
                            SQAGRADE.Fill(DS_Gred)
                            SQAHOUR.Fill(DS_Hour)
                            SQATOTAL.Fill(DS_Total)
                        Catch ex As Exception
                        End Try

                        ''print student name
                        Dim stdName As String = "select UPPER(student_Name) from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdName As String = oCommon.getFieldValue(stdName)

                        ''print student id
                        Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdID As String = oCommon.getFieldValue(stdID)

                        ''print student mykad
                        Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

                        ''print exam Name
                        Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlexam_Name.SelectedValue & "'"
                        Dim dataExmName As String = oCommon.getFieldValue(exmName)

                        If dataExmName = "Exam 1" Then
                            dataExmName = "Pentaksiran 1 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 2" Then
                            dataExmName = "Pentaksiran 2 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 3" Then
                            dataExmName = "Pentaksiran 1 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 4" Then
                            dataExmName = "Pentaksiran 2 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 5" Then
                            dataExmName = "Pentaksiran 1 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 6" Then
                            dataExmName = "Pentaksiran 2 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 7" Then
                            dataExmName = "Pentaksiran 1 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 8" Then
                            dataExmName = "Pentaksiran 2 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 9" Then
                            dataExmName = "Pentaksiran 1 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 10" Then
                            dataExmName = "Pentaksiran 2 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 11" Then
                            dataExmName = "Pentaksiran 1 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 12" Then
                            dataExmName = "Pentaksiran 2 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
                        End If

                        ''get month
                        Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
                        Dim dataMonth As String = oCommon.getFieldValue(month)

                        Dim dataStdMonth As String = ""

                        If dataMonth = "1" Then
                            dataStdMonth = "Januari"
                        ElseIf dataMonth = "2" Then
                            dataStdMonth = "Februari"
                        ElseIf dataMonth = "3" Then
                            dataStdMonth = "Mac"
                        ElseIf dataMonth = "4" Then
                            dataStdMonth = "April"
                        ElseIf dataMonth = "5" Then
                            dataStdMonth = "Mei"
                        ElseIf dataMonth = "6" Then
                            dataStdMonth = "Jun"
                        ElseIf dataMonth = "7" Then
                            dataStdMonth = "Julai"
                        ElseIf dataMonth = "8" Then
                            dataStdMonth = "Ogos"
                        ElseIf dataMonth = "9" Then
                            dataStdMonth = "September"
                        ElseIf dataMonth = "10" Then
                            dataStdMonth = "Oktober"
                        ElseIf dataMonth = "11" Then
                            dataStdMonth = "November"
                        ElseIf dataMonth = "12" Then
                            dataStdMonth = "Disember"
                        End If

                        ''get PNG & PNGK 
                        Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

                        Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

                        Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
                        Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

                        ''round to 2 decimal places
                        Dim gpa As Decimal = png_dec.ToString("F2")
                        Dim cgpa As Decimal = pngs_dec.ToString("F2")


                        tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim research_value As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim sd_value As String = oCommon.getFieldValue(tmpSQL)

                        ''first column
                        Test.Append("<div style='margin:0;page-break-after: always;'>
                                            <table style='width:100%;'>
                                                <tr style='width:100%'>
                                                    <td style='width:100%'>
                                                        <table tyle='width:100%'>
                                                            <tr style='width:100%'>
                                                                <td>
                                                                    <img src='img/ukm.jpg'  height='56' width='120'>
                                                                    &nbsp;
                                                                    <img src='img/logo genius pintar.png' height='62' width='120'>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr style='width:100%'>    
                                                    <td style='width:100%'>
                                                        <table style='width:100%'>
                                                            <tr style='width:100%'>
                                                                <td style='width:10%; font-size:0.8125em !important;'> Nama</td>
                                                                <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdName & "</td>
                                                            </tr>     
                                                            <tr style='width:100%'>
                                                                <td style='width:10%; font-size:0.8125em !important;'> MYKAD </td>
                                                                <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdMykad & "</td>
                                                            </tr>     
                                                            <tr style='width:100%'>
                                                                <td style='width:10%; font-size:0.8125em !important;'> ID Pelajar </td>
                                                                <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdID & "</td>
                                                            </tr>  
                                                            <tr style='width:100%'>
                                                                <td style='width:10%; font-size:0.8125em !important;'> Pentaksiran </td>
                                                                <td style='width:90%; font-size:0.8125em !important;'>: " & dataExmName & "</td>
                                                            </tr>
                                                        </table>    
                                                    </td>
                                                </tr>
                                            </table>

                                            <table style='width:100%; padding-top:5px'>
                                                <tr>
                                                    <td>
                                                        <p></p>
                                                    </td>
                                                    <table style='border: 1px solid black;border-collapse: collapse;'>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Komponen </b></td>
                                                            <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'><b> Peratusan </b></td>
                                                            <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'><b> Kod Kursus </b></td>
                                                            <td style='width:30%;border: 1px solid black; font-size:0.8125em !important;'><b> Kursus </b></td>
                                                            <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'><b> Gred </b></td>
                                                            <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'><b> PNG </b></td>
                                                            <td style='width:10%;border: 1px solid black; font-size:0.8125em !important;'><b> Jam Kredit </b></td>
                                                            <td style='width:15%;border: 1px solid black; font-size:0.8125em !important;'><b> PNG x Jam Kredit </b></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center '>
                                                            <td rowspan='3' style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Akademik </b></td>
                                                            <td rowspan='3'style='width:7%;border: 1px solid black; font-size:0.8125em !important;'> " & academic_value & "</td>
                                                            <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;text-align:left'>")

                        ''(column course code / kod kursus)
                        For Each row As DataRow In DS_Kod.Rows
                            For Each column As DataColumn In DS_Kod.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_KOD As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_KOD As String = oCommon.getFieldValue(get_ENG_KOD)

                        If data_ENGLITERATURE_KOD.Length > 0 Then

                            ''english literature kod
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_KOD.Rows
                                    For Each column As DataColumn In DSEnglish_literature_KOD.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Test.Append("                    </td>
                                                            <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'>")

                        ''(column course / kursus)
                        For Each row As DataRow In DS_Nama.Rows
                            For Each column As DataColumn In DS_Nama.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_NAMA As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_NAMA As String = oCommon.getFieldValue(get_ENG_NAMA)

                        If data_ENGLITERATURE_NAMA.Length > 0 Then

                            ''english literature NAMA
                            For Each row As DataRow In DSEnglish_literature_SUBJECT.Rows
                                For Each column As DataColumn In DSEnglish_literature_SUBJECT.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next

                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'> ")

                        ''(column grade / gred)
                        For Each row As DataRow In DS_Gred.Rows
                            For Each column As DataColumn In DS_Gred.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_Grade As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                          where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_Grade As String = oCommon.getFieldValue(get_ENG_Grade)

                        If data_ENGLITERATURE_Grade.Length > 0 Then

                            ''english literature Name
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_GRED.Rows
                                    For Each column As DataColumn In DSEnglish_literature_GRED.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'> ")

                        ''(column gpa / png)
                        For Each row As DataRow In DS_PNG.Rows
                            For Each column As DataColumn In DS_PNG.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_Png As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                          where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_Png As String = oCommon.getFieldValue(get_ENG_Png)

                        If data_ENGLITERATURE_Png.Length > 0 Then

                            ''english literature Name
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_PNG.Rows
                                    For Each column As DataColumn In DSEnglish_literature_PNG.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Test.Append("                   </td>
                                                            <td style='width:10%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''(column credit hour / jam kredit)
                        For Each row As DataRow In DS_Hour.Rows
                            For Each column As DataColumn In DS_Hour.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_HOUR As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_HOUR As String = oCommon.getFieldValue(get_ENG_HOUR)

                        If data_ENGLITERATURE_HOUR.Length > 0 Then

                            ''english literature credit hour
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_HOUR.Rows
                                    For Each column As DataColumn In DSEnglish_literature_HOUR.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Test.Append("                   </td>
                                                            <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> ")

                        ''(column total / jumalh)
                        For Each row As DataRow In DS_Total.Rows
                            For Each column As DataColumn In DS_Total.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_TOTAL As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_TOTAL As String = oCommon.getFieldValue(get_ENG_TOTAL)

                        If data_ENGLITERATURE_TOTAL.Length > 0 Then

                            ''english literature total / jumlah
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_TOTAL.Rows
                                    For Each column As DataColumn In DSEnglish_literature_TOTAL.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Debug.WriteLine("Error 1")
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Dim Number1 As Double = Double.Parse(total_Credit)
                        Dim Number2 As Double = Double.Parse(total_Credit_EL)
                        Dim Number3 As Double = Double.Parse(total_Total)
                        Dim Number4 As Double = Double.Parse(total_Total_EL)

                        Dim total_Hour As Double = Number1 + Number2
                        Dim final_Total As Double = Number3 + Number4

                        Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

                        Test.Append("                   </td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'><b> Jumlah </b></td>
                                                            <td style='width:10%;border: 1px solid black;font-size:0.8125em !important;'> " & total_Hour & " </td>
                                                            <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> " & final_Total & " </td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'><b> PNG Akademik </b></td>
                                                            <td style='width:10%;border: 1px solid black;'> </td>
                                                            <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> " & PNG_Akademik & " </td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Kokurikulum </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'>" & cocuricullum_value & "</td>
                                                            <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''kokorikulum kod sukan
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                                For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        ''kokorikulum kod kelab
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                                For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        ''kokorikulum kod uniform
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                                For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:30%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''kokorikulum nama skan
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                                For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        ''kokorikulum nama kelab
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                                For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        ''kokorikulum nama uniform
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                                For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''kokorikulum gred 
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_GRED.Rows
                                For Each column As DataColumn In DSCocuricullum_GRED.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append(" SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''kokorikulum png 
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_PNG.Rows
                                For Each column As DataColumn In DSCocuricullum_PNG.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Portfolio </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & portfolio_value & " </td>
                                                            <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''Portfolio KOD
                        If Confirm_Portfolio = "On" Then
                            For Each row As DataRow In DSPortfolio_KOD.Rows
                                For Each column As DataColumn In DSPortfolio_KOD.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Portfolio = "Off" Then
                            Test.Append("<br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''Portfolio Gred
                        If Confirm_Portfolio = "On" Then
                            For Each row As DataRow In DSPortfolio_GRED.Rows
                                For Each column As DataColumn In DSPortfolio_GRED.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Portfolio = "Off" Then
                            Test.Append("SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''Portfolio PNG
                        If Confirm_Portfolio = "On" Then
                            For Each row As DataRow In DSPortfolio_PNG.Rows
                                For Each column As DataColumn In DSPortfolio_PNG.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Portfolio = "Off" Then
                            Test.Append("SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Penyelidikan </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & research_value & " </td>
                                                            <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''research KOD
                        If Confirm_Research = "On" Then
                            For Each row As DataRow In DSResearch_KOD.Rows
                                For Each column As DataColumn In DSResearch_KOD.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                            Next
                        ElseIf Confirm_Research = "Off" Then
                            Test.Append("<br />")
                        End If

                        Test.Append("</td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''research GRED
                        If Confirm_Research = "On" Then
                            For Each row As DataRow In DSResearch_GRED.Rows
                                For Each column As DataColumn In DSResearch_GRED.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                            Next
                        ElseIf Confirm_Research = "Off" Then
                            Test.Append(" SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''research PNG
                        If Confirm_Research = "On" Then
                            For Each row As DataRow In DSResearch_PNG.Rows
                                For Each column As DataColumn In DSResearch_PNG.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                            Next
                        ElseIf Confirm_Research = "Off" Then
                            Test.Append(" SM <br />")
                        End If

                        Test.Append("</td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Pembangunan Kendiri </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & sd_value & " </td>
                                                            <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''(column self development codde / pembangunan kendiri kod)
                        For Each row As DataRow In DSSelfdevelopment_KOD.Rows
                            For Each column As DataColumn In DSSelfdevelopment_KOD.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Test.Append("                    </td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''(column self development grade / pembangunan kendiri gred)
                        For Each row As DataRow In DSSelfdevelopment_GRED.Rows
                            For Each column As DataColumn In DSSelfdevelopment_GRED.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''(column self development gpa / pembangunan kendiri png)
                        For Each row As DataRow In DSSelfdevelopment_PNG.Rows
                            For Each column As DataColumn In DSSelfdevelopment_PNG.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Test.Append("                   </td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> PNG </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'><b> " & gpa & " </b></td>
                                                            <td style='width:8%;border: 1px solid black;'></td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;'></td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> PNGK </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'><b> " & cgpa & "</b></td>
                                                            <td style='width:8%;border: 1px solid black;'></td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;'></td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                    </table>
                                                </tr>
                                             </table>    
                                         </div>")

                    End If
                End If
            Next

        ElseIf Lang = "BI" Then

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''''''''''''''''''''''''''''''checking student 
                        'get Portfolio percentage on / off
                        Dim check_portfolio_percen As String = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim Confirm_Portfolio As String = oCommon.getFieldValue(check_portfolio_percen)

                        ''get cocuricullum percentage on / off
                        Dim check_cocuricullum_percen As String = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim Confirm_Cocuricullum As String = oCommon.getFieldValue(check_cocuricullum_percen)

                        ''get research percentage on / off
                        Dim check_research_percen As String = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim Confirm_Research As String = oCommon.getFieldValue(check_research_percen)

                        ''get self development percentage on / off
                        Dim check_self_percen As String = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim Confirm_Self As String = oCommon.getFieldValue(check_self_percen)

                        ''print subject name 
                        tmpSQL_Nama = "SELECT subject_Name FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

                        ''print subject code
                        tmpSQL_Kod = "SELECT subject_code FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

                        ''print subject grade
                        tmpSQL_Gred = "SELECT grade FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

                        ''print subject png
                        tmpSQL_PNG = "SELECT gpa FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

                        ''print subject credit hour
                        tmpSQL_Hour = "SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

                        ''print subject credit hour
                        tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
                        Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)



                        tmpSQL = "select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                        Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select SUM(total) FROM [ExamSlip_SubjectName] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                        Dim total_Total As String = oCommon.getFieldValue(tmpSQL)


                        Dim DS_Nama As New DataTable
                        Dim DS_Kod As New DataTable
                        Dim DS_Gred As New DataTable
                        Dim DS_PNG As New DataTable
                        Dim DS_Hour As New DataTable
                        Dim DS_Total As New DataTable

                        Dim DSSelfdevelopment_GRED As New DataTable
                        Dim DSSelfdevelopment_PNG As New DataTable
                        Dim DSSelfdevelopment_KOD As New DataTable

                        Dim DSEnglish_literature_SUBJECT As New DataTable
                        Dim DSEnglish_literature_GRED As New DataTable
                        Dim DSEnglish_literature_PNG As New DataTable
                        Dim DSEnglish_literature_KOD As New DataTable
                        Dim DSEnglish_literature_HOUR As New DataTable
                        Dim DSEnglish_literature_TOTAL As New DataTable

                        Dim DSResearch_GRED As New DataTable
                        Dim DSResearch_PNG As New DataTable
                        Dim DSResearch_KOD As New DataTable

                        Dim DSPortfolio_GRED As New DataTable
                        Dim DSPortfolio_PNG As New DataTable
                        Dim DSPortfolio_KOD As New DataTable

                        Dim DSCocuricullum_KOD_SUKAN As New DataTable
                        Dim DSCocuricullum_KOD_UNIFORM As New DataTable
                        Dim DSCocuricullum_KOD_KELAB As New DataTable
                        Dim DSCocuricullum_NAMA_SUKAN As New DataTable
                        Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
                        Dim DSCocuricullum_NAMA_KELAB As New DataTable
                        Dim DSCocuricullum_GRED As New DataTable
                        Dim DSCocuricullum_PNG As New DataTable

                        Dim total_Credit_EL As String = "0"
                        Dim total_Total_EL As String = "0"

                        ''print english literature
                        If Confirm_Eng_Literature = "On" Then
                            tmpsql_EL_Subject = "SELECT subject_Name FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_GRED = "SELECT grade FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_PNG = "SELECT gpa FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_KOD = "SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_HOUR = "SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_EL_TOTAL = "SELECT total FROM [ExamSlip_English_Literature] 
                                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "


                            tmpSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                            total_Credit_EL = oCommon.getFieldValue(tmpSQL)

                            If total_Credit_EL.Length = 0 Then
                                total_Credit_EL = "0"
                            End If

                            tmpSQL = "select total FROM [ExamSlip_English_Literature] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                            total_Total_EL = oCommon.getFieldValue(tmpSQL)

                            If total_Total_EL.Length = 0 Then
                                total_Total_EL = "0"
                            End If

                            Dim SQEnglish_Literature_SUBJECT As New SqlDataAdapter(tmpsql_EL_Subject, strConn)
                            Dim SQEnglish_Literature_GRED As New SqlDataAdapter(tmpsql_EL_GRED, strConn)
                            Dim SQEnglish_Literature_PNG As New SqlDataAdapter(tmpsql_EL_PNG, strConn)
                            Dim SQEnglish_Literature_KOD As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
                            Dim SQEnglish_Literature_HOUR As New SqlDataAdapter(tmpsql_EL_HOUR, strConn)
                            Dim SQEnglish_Literature_TOTAL As New SqlDataAdapter(tmpsql_EL_TOTAL, strConn)

                            Try
                                SQEnglish_Literature_SUBJECT.Fill(DSEnglish_literature_SUBJECT)
                                SQEnglish_Literature_GRED.Fill(DSEnglish_literature_GRED)
                                SQEnglish_Literature_KOD.Fill(DSEnglish_literature_KOD)
                                SQEnglish_Literature_PNG.Fill(DSEnglish_literature_PNG)
                                SQEnglish_Literature_HOUR.Fill(DSEnglish_literature_HOUR)
                                SQEnglish_Literature_TOTAL.Fill(DSEnglish_literature_TOTAL)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print Portfolio
                        If Confirm_Portfolio = "On" Then
                            tmpsql_Portfolio_GRED = "SELECT grade FROM [ExamSlip_Portfolio] 
                                                         where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                         where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                         where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQPortfolio_GRED As New SqlDataAdapter(tmpsql_Portfolio_GRED, strConn)
                            Dim SQPortfolio_PNG As New SqlDataAdapter(tmpsql_Portfolio_PNG, strConn)
                            Dim SQPortfolio_KOD As New SqlDataAdapter(tmpsql_Portfolio_KOD, strConn)

                            Try
                                SQPortfolio_GRED.Fill(DSPortfolio_GRED)
                                SQPortfolio_PNG.Fill(DSPortfolio_PNG)
                                SQPortfolio_KOD.Fill(DSPortfolio_KOD)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print research 
                        If Confirm_Research = "On" Then
                            tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                            where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_Penyelidikan_PNG = "SELECT gpa FROM [ExamSlip_Research] 
                                                            where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            tmpsql_Penyelidikan_KOD = "SELECT subject_code FROM [ExamSlip_Research] 
                                                            where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQResearch_GRED As New SqlDataAdapter(tmpsql_Penyelidikan_Gred, strConn)
                            Dim SQResearch_PNG As New SqlDataAdapter(tmpsql_Penyelidikan_PNG, strConn)
                            Dim SQResearch_KOD As New SqlDataAdapter(tmpsql_Penyelidikan_KOD, strConn)

                            Try
                                SQResearch_GRED.Fill(DSResearch_GRED)
                                SQResearch_PNG.Fill(DSResearch_PNG)
                                SQResearch_KOD.Fill(DSResearch_KOD)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print self development
                        If Confirm_Self = "On" Then
                            Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' "
                            Dim getLevel As String = oCommon.getFieldValue(level)

                            If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                                tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                                tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                                tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                                tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            End If

                            Dim SQSelfdevelopment_GRED As New SqlDataAdapter(tmpSQL_SD_GRED, strConn)
                            Dim SQSelfdevelopment_PNG As New SqlDataAdapter(tmpsql_SD_PNG, strConn)
                            Dim SQSelfdevelopment_KOD As New SqlDataAdapter(tmpsql_SD_KOD, strConn)

                            Try
                                SQSelfdevelopment_GRED.Fill(DSSelfdevelopment_GRED)
                                SQSelfdevelopment_PNG.Fill(DSSelfdevelopment_PNG)
                                SQSelfdevelopment_KOD.Fill(DSSelfdevelopment_KOD)
                            Catch ex As Exception

                            End Try
                        End If

                        '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                        If Confirm_Cocuricullum = "On" Then

                            Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim getStudent As String = oCommon.getFieldValue(studentData)

                            If ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 10" Then

                                tmpsql_KOKO_PNG = "select koko_pelajar.PNGP1 from koko_pelajar
                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
                                Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
                                Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

                                Try
                                    SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                    SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                    SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                    SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                    SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                    SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                    SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                                    SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 12" Then

                                tmpsql_KOKO_PNG = "select koko_pelajar.PNGP2 from koko_pelajar
                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                              left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                              left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                              where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
                                Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
                                Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

                                Try
                                    SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                    SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                    SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                    SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                    SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                    SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                    SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                                    SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                                Catch ex As Exception

                                End Try

                            End If
                        End If

                        Try
                            SQA.Fill(DS_Nama)
                            SQACODE.Fill(DS_Kod)
                            SQAPNG.Fill(DS_PNG)
                            SQAGRADE.Fill(DS_Gred)
                            SQAHOUR.Fill(DS_Hour)
                            SQATOTAL.Fill(DS_Total)
                        Catch ex As Exception
                        End Try

                        ''print student name
                        Dim stdName As String = "select UPPER(student_Name) from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdName As String = oCommon.getFieldValue(stdName)

                        ''print student id
                        Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdID As String = oCommon.getFieldValue(stdID)

                        ''print student mykad
                        Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

                        ''print exam Name
                        Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlexam_Name.SelectedValue & "'"
                        Dim dataExmName As String = oCommon.getFieldValue(exmName)

                        If dataExmName = "Exam 1" Then
                            dataExmName = "Assessment 1 Semester 1, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 2" Then
                            dataExmName = "Assessment 2 Semester 1, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 3" Then
                            dataExmName = "Assessment 1 Semester 2, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 4" Then
                            dataExmName = "Assessment 2 Semester 2, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 5" Then
                            dataExmName = "Assessment 1 Semester 1, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 6" Then
                            dataExmName = "Assessment 2 Semester 1, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 7" Then
                            dataExmName = "Assessment 1 Semester 2, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 8" Then
                            dataExmName = "Assessment 2 Semester 2, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 9" Then
                            dataExmName = "Assessment 1 Semester 1, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 10" Then
                            dataExmName = "Assessment 2 Semester 1, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 11" Then
                            dataExmName = "Assessment 1 Semester 2, Academic Year " & ddlyear.SelectedValue
                        ElseIf dataExmName = "Exam 12" Then
                            dataExmName = "Assessment 2 Semester 2, Academic Year " & ddlyear.SelectedValue
                        End If

                        ''get month
                        Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
                        Dim dataMonth As String = oCommon.getFieldValue(month)

                        Dim dataStdMonth As String = ""

                        If dataMonth = "1" Then
                            dataStdMonth = "January"
                        ElseIf dataMonth = "2" Then
                            dataStdMonth = "February"
                        ElseIf dataMonth = "3" Then
                            dataStdMonth = "March"
                        ElseIf dataMonth = "4" Then
                            dataStdMonth = "April"
                        ElseIf dataMonth = "5" Then
                            dataStdMonth = "May"
                        ElseIf dataMonth = "6" Then
                            dataStdMonth = "June"
                        ElseIf dataMonth = "7" Then
                            dataStdMonth = "July"
                        ElseIf dataMonth = "8" Then
                            dataStdMonth = "August"
                        ElseIf dataMonth = "9" Then
                            dataStdMonth = "September"
                        ElseIf dataMonth = "10" Then
                            dataStdMonth = "October"
                        ElseIf dataMonth = "11" Then
                            dataStdMonth = "November"
                        ElseIf dataMonth = "12" Then
                            dataStdMonth = "December"
                        End If

                        ''get PNG & PNGK 
                        Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

                        Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

                        Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
                        Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

                        ''round to 2 decimal places
                        Dim gpa As Decimal = png_dec.ToString("F2")
                        Dim cgpa As Decimal = pngs_dec.ToString("F2")


                        tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim research_value As String = oCommon.getFieldValue(tmpSQL)

                        tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim sd_value As String = oCommon.getFieldValue(tmpSQL)

                        ''first column
                        Test.Append("<div style='margin:0;page-break-after: always;'>
                                            <table style='width:100%;'>
                                                <tr style='width:100%'>
                                                    <td style='width:100%'>
                                                        <table tyle='width:100%'>
                                                            <tr style='width:100%'>
                                                                <td>
                                                                    <img src='img/ukm.jpg'  height='56' width='120'>
                                                                    &nbsp;
                                                                    <img src='img/logo genius pintar.png' height='62' width='120'>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr style='width:100%'>    
                                                    <td style='width:100%'>
                                                        <table style='width:100%'>
                                                            <tr style='width:100%'>
                                                                <td style='width:10%; font-size:0.8125em !important;'> Name </td>
                                                                <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdName & "</td>
                                                            </tr>     
                                                            <tr style='width:100%'>
                                                                <td style='width:10%; font-size:0.8125em !important;'> MYKAD </td>
                                                                <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdMykad & "</td>
                                                            </tr>     
                                                            <tr style='width:100%'>
                                                                <td style='width:10%; font-size:0.8125em !important;'> Student ID </td>
                                                                <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdID & "</td>
                                                            </tr>  
                                                            <tr style='width:100%'>
                                                                <td style='width:10%; font-size:0.8125em !important;'> Assessment </td>
                                                                <td style='width:90%; font-size:0.8125em !important;'>: " & dataExmName & "</td>
                                                            </tr>
                                                        </table>    
                                                    </td>
                                                </tr>
                                            </table>

                                            <table style='width:100%; padding-top:5px'>
                                                <tr>
                                                    <td>
                                                        <p></p>
                                                    </td>
                                                    <table style='border: 1px solid black;border-collapse: collapse;'>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Component </b></td>
                                                            <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'><b> Percentage </b></td>
                                                            <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'><b> Course Code </b></td>
                                                            <td style='width:30%;border: 1px solid black; font-size:0.8125em !important;'><b> Course </b></td>
                                                            <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'><b> Grade </b></td>
                                                            <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'><b> GPA </b></td>
                                                            <td style='width:10%;border: 1px solid black; font-size:0.8125em !important;'><b> Credit Hour </b></td>
                                                            <td style='width:15%;border: 1px solid black; font-size:0.8125em !important;'><b> GPA x Credit Hour </b></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center '>
                                                            <td rowspan='3' style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Academic </b></td>
                                                            <td rowspan='3'style='width:7%;border: 1px solid black; font-size:0.8125em !important;'> " & academic_value & "</td>
                                                            <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;text-align:left'>")

                        ''(column course code / kod kursus)
                        For Each row As DataRow In DS_Kod.Rows
                            For Each column As DataColumn In DS_Kod.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_KOD As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_KOD As String = oCommon.getFieldValue(get_ENG_KOD)

                        If data_ENGLITERATURE_KOD.Length > 0 Then

                            ''english literature kod
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_KOD.Rows
                                    For Each column As DataColumn In DSEnglish_literature_KOD.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Test.Append("                    </td>
                                                            <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'>")

                        ''(column course / kursus)
                        For Each row As DataRow In DS_Nama.Rows
                            For Each column As DataColumn In DS_Nama.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_NAMA As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_NAMA As String = oCommon.getFieldValue(get_ENG_NAMA)

                        If data_ENGLITERATURE_NAMA.Length > 0 Then

                            ''english literature NAMA
                            For Each row As DataRow In DSEnglish_literature_SUBJECT.Rows
                                For Each column As DataColumn In DSEnglish_literature_SUBJECT.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next

                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'> ")

                        ''(column grade / gred)
                        For Each row As DataRow In DS_Gred.Rows
                            For Each column As DataColumn In DS_Gred.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_Grade As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                          where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_Grade As String = oCommon.getFieldValue(get_ENG_Grade)

                        If data_ENGLITERATURE_Grade.Length > 0 Then

                            ''english literature Name
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_GRED.Rows
                                    For Each column As DataColumn In DSEnglish_literature_GRED.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'> ")

                        ''(column gpa / png)
                        For Each row As DataRow In DS_PNG.Rows
                            For Each column As DataColumn In DS_PNG.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_Png As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                          where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_Png As String = oCommon.getFieldValue(get_ENG_Png)

                        If data_ENGLITERATURE_Png.Length > 0 Then

                            ''english literature Name
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_PNG.Rows
                                    For Each column As DataColumn In DSEnglish_literature_PNG.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Test.Append("                   </td>
                                                            <td style='width:10%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''(column credit hour / jam kredit)
                        For Each row As DataRow In DS_Hour.Rows
                            For Each column As DataColumn In DS_Hour.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_HOUR As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_HOUR As String = oCommon.getFieldValue(get_ENG_HOUR)

                        If data_ENGLITERATURE_HOUR.Length > 0 Then

                            ''english literature credit hour
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_HOUR.Rows
                                    For Each column As DataColumn In DSEnglish_literature_HOUR.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Test.Append("                   </td>
                                                            <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> ")

                        ''(column total / jumalh)
                        For Each row As DataRow In DS_Total.Rows
                            For Each column As DataColumn In DS_Total.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Dim get_ENG_TOTAL As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE_TOTAL As String = oCommon.getFieldValue(get_ENG_TOTAL)

                        If data_ENGLITERATURE_TOTAL.Length > 0 Then

                            ''english literature total / jumlah
                            If Confirm_Eng_Literature = "On" Then
                                For Each row As DataRow In DSEnglish_literature_TOTAL.Rows
                                    For Each column As DataColumn In DSEnglish_literature_TOTAL.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                            ElseIf Confirm_Eng_Literature = "Off" Then
                                Debug.WriteLine("Error 1")
                                Test.Append(" SM <br />")
                            End If

                        End If

                        Dim Number1 As Double = Double.Parse(total_Credit)
                        Dim Number2 As Double = Double.Parse(total_Credit_EL)
                        Dim Number3 As Double = Double.Parse(total_Total)
                        Dim Number4 As Double = Double.Parse(total_Total_EL)

                        Dim total_Hour As Double = Number1 + Number2
                        Dim final_Total As Double = Number3 + Number4

                        Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

                        Test.Append("                   </td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'><b> Total </b></td>
                                                            <td style='width:10%;border: 1px solid black;font-size:0.8125em !important;'> " & total_Hour & " </td>
                                                            <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> " & final_Total & " </td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'><b> Academic GPA </b></td>
                                                            <td style='width:10%;border: 1px solid black;'> </td>
                                                            <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> " & PNG_Akademik & " </td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Co-Curricular </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'>" & cocuricullum_value & "</td>
                                                            <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''kokorikulum kod sukan
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                                For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        ''kokorikulum kod kelab
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                                For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        ''kokorikulum kod uniform
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                                For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'>")

                        ''kokorikulum nama skan
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                                For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        ''kokorikulum nama kelab
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                                For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        ''kokorikulum nama uniform
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                                For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("<br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''kokorikulum gred 
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_GRED.Rows
                                For Each column As DataColumn In DSCocuricullum_GRED.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append(" SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''kokorikulum png 
                        If Confirm_Cocuricullum = "On" Then
                            For Each row As DataRow In DSCocuricullum_PNG.Rows
                                For Each column As DataColumn In DSCocuricullum_PNG.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Cocuricullum = "Off" Then
                            Test.Append("SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Portfolio </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & portfolio_value & " </td>
                                                            <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''Portfolio KOD
                        If Confirm_Portfolio = "On" Then
                            For Each row As DataRow In DSPortfolio_KOD.Rows
                                For Each column As DataColumn In DSPortfolio_KOD.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Portfolio = "Off" Then
                            Test.Append("<br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''Portfolio Gred
                        If Confirm_Portfolio = "On" Then
                            For Each row As DataRow In DSPortfolio_GRED.Rows
                                For Each column As DataColumn In DSPortfolio_GRED.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Portfolio = "Off" Then
                            Test.Append("SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''Portfolio PNG
                        If Confirm_Portfolio = "On" Then
                            For Each row As DataRow In DSPortfolio_PNG.Rows
                                For Each column As DataColumn In DSPortfolio_PNG.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("<br />")
                                Next
                            Next
                        ElseIf Confirm_Portfolio = "Off" Then
                            Test.Append("SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Research </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & research_value & " </td>
                                                            <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''research KOD
                        If Confirm_Research = "On" Then
                            For Each row As DataRow In DSResearch_KOD.Rows
                                For Each column As DataColumn In DSResearch_KOD.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                            Next
                        ElseIf Confirm_Research = "Off" Then
                            Test.Append("<br />")
                        End If

                        Test.Append("</td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''research GRED
                        If Confirm_Research = "On" Then
                            For Each row As DataRow In DSResearch_GRED.Rows
                                For Each column As DataColumn In DSResearch_GRED.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                            Next
                        ElseIf Confirm_Research = "Off" Then
                            Test.Append(" SM <br />")
                        End If

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''research PNG
                        If Confirm_Research = "On" Then
                            For Each row As DataRow In DSResearch_PNG.Rows
                                For Each column As DataColumn In DSResearch_PNG.Columns
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                            Next
                        ElseIf Confirm_Research = "Off" Then
                            Test.Append(" SM <br />")
                        End If

                        Test.Append("</td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Self Development </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & sd_value & " </td>
                                                            <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;text-align:left'>")

                        ''(column self development codde / pembangunan kendiri kod)
                        For Each row As DataRow In DSSelfdevelopment_KOD.Rows
                            For Each column As DataColumn In DSSelfdevelopment_KOD.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Test.Append("                    </td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''(column self development grade / pembangunan kendiri gred)
                        For Each row As DataRow In DSSelfdevelopment_GRED.Rows
                            For Each column As DataColumn In DSSelfdevelopment_GRED.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Test.Append("                   </td>
                                                            <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

                        ''(column self development gpa / pembangunan kendiri png)
                        For Each row As DataRow In DSSelfdevelopment_PNG.Rows
                            For Each column As DataColumn In DSSelfdevelopment_PNG.Columns
                                Test.Append(row(column.ColumnName))
                                Test.Append("<br />")
                            Next
                        Next

                        Test.Append("                   </td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> GPA </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'><b> " & gpa & " </b></td>
                                                            <td style='width:8%;border: 1px solid black;'></td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;'></td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                            <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> CGPA </b></td>
                                                            <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'><b> " & cgpa & "</b></td>
                                                            <td style='width:8%;border: 1px solid black;'></td>
                                                            <td style='width:30%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;'></td>
                                                            <td style='width:5%;border: 1px solid black;'></td>
                                                            <td style='width:10%;border: 1px solid black;'></td>
                                                            <td style='width:15%;border: 1px solid black;'></td>
                                                        </tr>
                                                    </table>
                                                </tr>
                                             </table>    
                                         </div>")

                    End If
                End If
            Next

        End If


        Test.AppendLine(" </div> </div>")
        Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataPrint'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

        'print
        Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())


    End Sub

    'Private Sub Print_Transcript(Lang As String)

    '    Dim tmpSQL As String = ""
    '    Dim tmpSQL_Nama As String = ""
    '    Dim tmpSQL_Kod As String = ""
    '    Dim tmpSQL_Gred As String = ""
    '    Dim tmpSQL_PNG As String = ""
    '    Dim tmpSQL_Hour As String = ""
    '    Dim tmpSQL_Total As String = ""

    '    Dim tmpSQL_SD_GRED As String = ""
    '    Dim tmpsql_SD_PNG As String = ""
    '    Dim tmpsql_SD_KOD As String = ""

    '    Dim tmpsql_Portfolio_GRED As String = ""
    '    Dim tmpsql_Portfolio_PNG As String = ""
    '    Dim tmpsql_Portfolio_KOD As String = ""

    '    Dim tmpsql_Penyelidikan_Gred As String = ""
    '    Dim tmpsql_Penyelidikan_PNG As String = ""
    '    Dim tmpsql_Penyelidikan_KOD As String = ""

    '    Dim tmpsql_EL_Subject As String = ""
    '    Dim tmpsql_EL_GRED As String = ""
    '    Dim tmpsql_EL_PNG As String = ""
    '    Dim tmpsql_EL_KOD As String = ""
    '    Dim tmpsql_EL_HOUR As String = ""
    '    Dim tmpsql_EL_TOTAL As String = ""

    '    Dim tmpsql_KOKO_KOD_SUKAN As String = ""
    '    Dim tmpsql_KOKO_KOD_UNIFORM As String = ""
    '    Dim tmpsql_KOKO_KOD_KELAB As String = ""
    '    Dim tmpsql_KOKO_NAMA_SUKAN As String = ""
    '    Dim tmpsql_KOKO_NAMA_KELAB As String = ""
    '    Dim tmpsql_KOKO_NAMA_UNIFORM As String = ""
    '    Dim tmpsql_KOKO_GRED As String = ""
    '    Dim tmpsql_KOKO_PNG As String = ""

    '    Dim errorCount As Integer = 0
    '    Dim i As Integer = 0
    '    Dim Test As New StringBuilder()

    '    'get englih literture on / off
    '    Dim check_Eng_Literature As String = "select Value from setting where Type = 'English Literature'"
    '    Dim Confirm_Eng_Literature As String = oCommon.getFieldValue(check_Eng_Literature)

    '    ''check print transcript language''
    '    If Lang = "BM" Then

    '        Test.AppendLine("<div id='data' style='display:none'>")
    '        Test.AppendLine("<div id='dataTESTBM'> ")

    '        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
    '            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
    '            If Not chkUpdate Is Nothing Then
    '                ' Get the values of textboxes using findControl
    '                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
    '                If chkUpdate.Checked = True Then

    '                    ''''''''''''''''''''''''''''''checking student 
    '                    'get Portfolio percentage on / off
    '                    Dim check_portfolio_percen As String = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim Confirm_Portfolio As String = oCommon.getFieldValue(check_portfolio_percen)

    '                    ''get cocuricullum percentage on / off
    '                    Dim check_cocuricullum_percen As String = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim Confirm_Cocuricullum As String = oCommon.getFieldValue(check_cocuricullum_percen)

    '                    ''get research percentage on / off
    '                    Dim check_research_percen As String = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim Confirm_Research As String = oCommon.getFieldValue(check_research_percen)

    '                    ''get self development percentage on / off
    '                    Dim check_self_percen As String = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim Confirm_Self As String = oCommon.getFieldValue(check_self_percen)

    '                    ''print subject name 
    '                    tmpSQL_Nama = "SELECT subject_NameBM FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
    '                    Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

    '                    ''print subject code
    '                    tmpSQL_Kod = "SELECT subject_code FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
    '                    Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

    '                    ''print subject grade
    '                    tmpSQL_Gred = "SELECT grade FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
    '                    Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

    '                    ''print subject png
    '                    tmpSQL_PNG = "SELECT gpa FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
    '                    Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

    '                    ''print subject credit hour
    '                    tmpSQL_Hour = "SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
    '                    Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

    '                    ''print subject credit hour
    '                    tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_NameBM ASC"
    '                    Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)



    '                    tmpSQL = "select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
    '                    Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select SUM(total) FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
    '                    Dim total_Total As String = oCommon.getFieldValue(tmpSQL)


    '                    Dim DS_Nama As New DataTable
    '                    Dim DS_Kod As New DataTable
    '                    Dim DS_Gred As New DataTable
    '                    Dim DS_PNG As New DataTable
    '                    Dim DS_Hour As New DataTable
    '                    Dim DS_Total As New DataTable

    '                    Dim DSSelfdevelopment_GRED As New DataTable
    '                    Dim DSSelfdevelopment_PNG As New DataTable
    '                    Dim DSSelfdevelopment_KOD As New DataTable

    '                    Dim DSEnglish_literature_SUBJECT As New DataTable
    '                    Dim DSEnglish_literature_GRED As New DataTable
    '                    Dim DSEnglish_literature_PNG As New DataTable
    '                    Dim DSEnglish_literature_KOD As New DataTable
    '                    Dim DSEnglish_literature_HOUR As New DataTable
    '                    Dim DSEnglish_literature_TOTAL As New DataTable

    '                    Dim DSResearch_GRED As New DataTable
    '                    Dim DSResearch_PNG As New DataTable
    '                    Dim DSResearch_KOD As New DataTable

    '                    Dim DSPortfolio_GRED As New DataTable
    '                    Dim DSPortfolio_PNG As New DataTable
    '                    Dim DSPortfolio_KOD As New DataTable

    '                    Dim DSCocuricullum_KOD_SUKAN As New DataTable
    '                    Dim DSCocuricullum_KOD_UNIFORM As New DataTable
    '                    Dim DSCocuricullum_KOD_KELAB As New DataTable
    '                    Dim DSCocuricullum_NAMA_SUKAN As New DataTable
    '                    Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
    '                    Dim DSCocuricullum_NAMA_KELAB As New DataTable
    '                    Dim DSCocuricullum_GRED As New DataTable
    '                    Dim DSCocuricullum_PNG As New DataTable

    '                    Dim total_Credit_EL As String = "0"
    '                    Dim total_Total_EL As String = "0"

    '                    ''print english literature
    '                    If Confirm_Eng_Literature = "On" Then
    '                        tmpsql_EL_Subject = "SELECT subject_NameBM FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_GRED = "SELECT grade FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_PNG = "SELECT gpa FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_KOD = "SELECT subject_code FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_HOUR = "SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_TOTAL = "SELECT total FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "


    '                        tmpSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
    '                        total_Credit_EL = oCommon.getFieldValue(tmpSQL)

    '                        If total_Credit_EL.Length = 0 Then
    '                            total_Credit_EL = "0"
    '                        End If

    '                        tmpSQL = "select total FROM [ExamSlip_English_Literature] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
    '                        total_Total_EL = oCommon.getFieldValue(tmpSQL)

    '                        If total_Total_EL.Length = 0 Then
    '                            total_Total_EL = "0"
    '                        End If

    '                        Dim SQEnglish_Literature_SUBJECT As New SqlDataAdapter(tmpsql_EL_Subject, strConn)
    '                        Dim SQEnglish_Literature_GRED As New SqlDataAdapter(tmpsql_EL_GRED, strConn)
    '                        Dim SQEnglish_Literature_PNG As New SqlDataAdapter(tmpsql_EL_PNG, strConn)
    '                        Dim SQEnglish_Literature_KOD As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
    '                        Dim SQEnglish_Literature_HOUR As New SqlDataAdapter(tmpsql_EL_HOUR, strConn)
    '                        Dim SQEnglish_Literature_TOTAL As New SqlDataAdapter(tmpsql_EL_TOTAL, strConn)

    '                        Try
    '                            SQEnglish_Literature_SUBJECT.Fill(DSEnglish_literature_SUBJECT)
    '                            SQEnglish_Literature_GRED.Fill(DSEnglish_literature_GRED)
    '                            SQEnglish_Literature_KOD.Fill(DSEnglish_literature_KOD)
    '                            SQEnglish_Literature_PNG.Fill(DSEnglish_literature_PNG)
    '                            SQEnglish_Literature_HOUR.Fill(DSEnglish_literature_HOUR)
    '                            SQEnglish_Literature_TOTAL.Fill(DSEnglish_literature_TOTAL)
    '                        Catch ex As Exception

    '                        End Try
    '                    End If

    '                    ''print Portfolio
    '                    If Confirm_Portfolio = "On" Then
    '                        tmpsql_Portfolio_GRED = "SELECT grade FROM [ExamSlip_Portfolio] 
    '                                                 where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
    '                                                 where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
    '                                                 where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        Dim SQPortfolio_GRED As New SqlDataAdapter(tmpsql_Portfolio_GRED, strConn)
    '                        Dim SQPortfolio_PNG As New SqlDataAdapter(tmpsql_Portfolio_PNG, strConn)
    '                        Dim SQPortfolio_KOD As New SqlDataAdapter(tmpsql_Portfolio_KOD, strConn)

    '                        Try
    '                            SQPortfolio_GRED.Fill(DSPortfolio_GRED)
    '                            SQPortfolio_PNG.Fill(DSPortfolio_PNG)
    '                            SQPortfolio_KOD.Fill(DSPortfolio_KOD)
    '                        Catch ex As Exception

    '                        End Try
    '                    End If

    '                    ''print research 
    '                    If Confirm_Research = "On" Then
    '                        tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
    '                                                    where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_Penyelidikan_PNG = "SELECT gpa FROM [ExamSlip_Research] 
    '                                                    where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_Penyelidikan_KOD = "SELECT subject_code FROM [ExamSlip_Research] 
    '                                                    where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        Dim SQResearch_GRED As New SqlDataAdapter(tmpsql_Penyelidikan_Gred, strConn)
    '                        Dim SQResearch_PNG As New SqlDataAdapter(tmpsql_Penyelidikan_PNG, strConn)
    '                        Dim SQResearch_KOD As New SqlDataAdapter(tmpsql_Penyelidikan_KOD, strConn)

    '                        Try
    '                            SQResearch_GRED.Fill(DSResearch_GRED)
    '                            SQResearch_PNG.Fill(DSResearch_PNG)
    '                            SQResearch_KOD.Fill(DSResearch_KOD)
    '                        Catch ex As Exception

    '                        End Try
    '                    End If

    '                    ''print self development
    '                    If Confirm_Self = "On" Then
    '                        Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' "
    '                        Dim getLevel As String = oCommon.getFieldValue(level)

    '                        If getLevel <> "Level 1" And getLevel <> "Level 2" Then
    '                            tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                            tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                            tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
    '                            tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                            tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                            tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        End If

    '                        Dim SQSelfdevelopment_GRED As New SqlDataAdapter(tmpSQL_SD_GRED, strConn)
    '                        Dim SQSelfdevelopment_PNG As New SqlDataAdapter(tmpsql_SD_PNG, strConn)
    '                        Dim SQSelfdevelopment_KOD As New SqlDataAdapter(tmpsql_SD_KOD, strConn)

    '                        Try
    '                            SQSelfdevelopment_GRED.Fill(DSSelfdevelopment_GRED)
    '                            SQSelfdevelopment_PNG.Fill(DSSelfdevelopment_PNG)
    '                            SQSelfdevelopment_KOD.Fill(DSSelfdevelopment_KOD)
    '                        Catch ex As Exception

    '                        End Try
    '                    End If

    '                    '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
    '                    If Confirm_Cocuricullum = "On" Then

    '                        Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
    '                        Dim getStudent As String = oCommon.getFieldValue(studentData)

    '                        If ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 10" Then

    '                            tmpsql_KOKO_PNG = "select koko_pelajar.PNGP1 from koko_pelajar
    '                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                      where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

    '                            tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
    '                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                      where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

    '                            tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

    '                            tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

    '                            tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

    '                            tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

    '                            tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

    '                            tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

    '                            Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
    '                            Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
    '                            Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
    '                            Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
    '                            Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

    '                            Try
    '                                SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
    '                                SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
    '                                SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
    '                                SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
    '                                SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
    '                                SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
    '                                SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
    '                                SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
    '                            Catch ex As Exception

    '                            End Try

    '                        ElseIf ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 12" Then

    '                            tmpsql_KOKO_PNG = "select koko_pelajar.PNGP2 from koko_pelajar
    '                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                      where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

    '                            tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
    '                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                      where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

    '                            tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

    '                            tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

    '                            tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

    '                            tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

    '                            tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

    '                            tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

    '                            Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
    '                            Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
    '                            Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
    '                            Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
    '                            Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

    '                            Try
    '                                SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
    '                                SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
    '                                SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
    '                                SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
    '                                SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
    '                                SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
    '                                SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
    '                                SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
    '                            Catch ex As Exception

    '                            End Try

    '                        End If
    '                    End If

    '                    Try
    '                        SQA.Fill(DS_Nama)
    '                        SQACODE.Fill(DS_Kod)
    '                        SQAPNG.Fill(DS_PNG)
    '                        SQAGRADE.Fill(DS_Gred)
    '                        SQAHOUR.Fill(DS_Hour)
    '                        SQATOTAL.Fill(DS_Total)
    '                    Catch ex As Exception
    '                    End Try

    '                    ''print student name
    '                    Dim stdName As String = "select UPPER(student_Name) from student_info where std_ID = '" & strKey & "'"
    '                    Dim dataStdName As String = oCommon.getFieldValue(stdName)

    '                    ''print student id
    '                    Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
    '                    Dim dataStdID As String = oCommon.getFieldValue(stdID)

    '                    ''print student mykad
    '                    Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
    '                    Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

    '                    ''print exam Name
    '                    Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlexam_Name.SelectedValue & "'"
    '                    Dim dataExmName As String = oCommon.getFieldValue(exmName)

    '                    If dataExmName = "Exam 1" Then
    '                        dataExmName = "Pentaksiran 1 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 2" Then
    '                        dataExmName = "Pentaksiran 2 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 3" Then
    '                        dataExmName = "Pentaksiran 1 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 4" Then
    '                        dataExmName = "Pentaksiran 2 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 5" Then
    '                        dataExmName = "Pentaksiran 1 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 6" Then
    '                        dataExmName = "Pentaksiran 2 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 7" Then
    '                        dataExmName = "Pentaksiran 1 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 8" Then
    '                        dataExmName = "Pentaksiran 2 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 9" Then
    '                        dataExmName = "Pentaksiran 1 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 10" Then
    '                        dataExmName = "Pentaksiran 2 Semester 1, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 11" Then
    '                        dataExmName = "Pentaksiran 1 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 12" Then
    '                        dataExmName = "Pentaksiran 2 Semester 2, Tahun Akademik " & ddlyear.SelectedValue
    '                    End If

    '                    ''get month
    '                    Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
    '                    Dim dataMonth As String = oCommon.getFieldValue(month)

    '                    Dim dataStdMonth As String = ""

    '                    If dataMonth = "1" Then
    '                        dataStdMonth = "Januari"
    '                    ElseIf dataMonth = "2" Then
    '                        dataStdMonth = "Februari"
    '                    ElseIf dataMonth = "3" Then
    '                        dataStdMonth = "Mac"
    '                    ElseIf dataMonth = "4" Then
    '                        dataStdMonth = "April"
    '                    ElseIf dataMonth = "5" Then
    '                        dataStdMonth = "Mei"
    '                    ElseIf dataMonth = "6" Then
    '                        dataStdMonth = "Jun"
    '                    ElseIf dataMonth = "7" Then
    '                        dataStdMonth = "Julai"
    '                    ElseIf dataMonth = "8" Then
    '                        dataStdMonth = "Ogos"
    '                    ElseIf dataMonth = "9" Then
    '                        dataStdMonth = "September"
    '                    ElseIf dataMonth = "10" Then
    '                        dataStdMonth = "Oktober"
    '                    ElseIf dataMonth = "11" Then
    '                        dataStdMonth = "November"
    '                    ElseIf dataMonth = "12" Then
    '                        dataStdMonth = "Disember"
    '                    End If

    '                    ''get PNG & PNGK 
    '                    Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

    '                    Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

    '                    Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
    '                    Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

    '                    ''round to 2 decimal places
    '                    Dim gpa As Decimal = png_dec.ToString("F2")
    '                    Dim cgpa As Decimal = pngs_dec.ToString("F2")


    '                    tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim research_value As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim sd_value As String = oCommon.getFieldValue(tmpSQL)

    '                    ''first column
    '                    Test.Append("<div style='margin:0;page-break-after: always;'>
    '                                    <table style='width:100%;'>
    '                                        <tr style='width:100%'>
    '                                            <td style='width:100%'>
    '                                                <table tyle='width:100%'>
    '                                                    <tr style='width:100%'>
    '                                                        <td>
    '                                                            <img src='img/ukm.jpg'  height='56' width='120'>
    '                                                            &nbsp;
    '                                                            <img src='img/logo genius pintar.png' height='62' width='120'>
    '                                                        </td>
    '                                                    </tr>
    '                                                </table>
    '                                            </td>
    '                                        </tr>
    '                                        <tr style='width:100%'>    
    '                                            <td style='width:100%'>
    '                                                <table style='width:100%'>
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> Nama</td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdName & "</td>
    '                                                    </tr>     
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> MYKAD </td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdMykad & "</td>
    '                                                    </tr>     
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> ID Pelajar </td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdID & "</td>
    '                                                    </tr>  
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> Pentaksiran </td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & dataExmName & "</td>
    '                                                    </tr>
    '                                                </table>    
    '                                            </td>
    '                                        </tr>
    '                                    </table>

    '                                    <table style='width:100%; padding-top:5px'>
    '                                        <tr>
    '                                            <td>
    '                                                <p></p>
    '                                            </td>
    '                                            <table style='border: 1px solid black;border-collapse: collapse;'>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Komponen </b></td>
    '                                                    <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'><b> Peratusan </b></td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'><b> Kod Kursus </b></td>
    '                                                    <td style='width:30%;border: 1px solid black; font-size:0.8125em !important;'><b> Kursus </b></td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'><b> Gred </b></td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'><b> PNG </b></td>
    '                                                    <td style='width:10%;border: 1px solid black; font-size:0.8125em !important;'><b> Jam Kredit </b></td>
    '                                                    <td style='width:15%;border: 1px solid black; font-size:0.8125em !important;'><b> PNG x Jam Kredit </b></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center '>
    '                                                    <td rowspan='3' style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Akademik </b></td>
    '                                                    <td rowspan='3'style='width:7%;border: 1px solid black; font-size:0.8125em !important;'> " & academic_value & "</td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''(column course code / kod kursus)
    '                    For Each row As DataRow In DS_Kod.Rows
    '                        For Each column As DataColumn In DS_Kod.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_KOD As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                              where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_KOD As String = oCommon.getFieldValue(get_ENG_KOD)

    '                    If data_ENGLITERATURE_KOD.Length > 0 Then

    '                        ''english literature kod
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_KOD.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_KOD.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Test.Append("                    </td>
    '                                                    <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'>")

    '                    ''(column course / kursus)
    '                    For Each row As DataRow In DS_Nama.Rows
    '                        For Each column As DataColumn In DS_Nama.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_NAMA As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                              where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_NAMA As String = oCommon.getFieldValue(get_ENG_NAMA)

    '                    If data_ENGLITERATURE_NAMA.Length > 0 Then

    '                        ''english literature NAMA
    '                        For Each row As DataRow In DSEnglish_literature_SUBJECT.Rows
    '                            For Each column As DataColumn In DSEnglish_literature_SUBJECT.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next

    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'> ")

    '                    ''(column grade / gred)
    '                    For Each row As DataRow In DS_Gred.Rows
    '                        For Each column As DataColumn In DS_Gred.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_Grade As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_Grade As String = oCommon.getFieldValue(get_ENG_Grade)

    '                    If data_ENGLITERATURE_Grade.Length > 0 Then

    '                        ''english literature Name
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_GRED.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_GRED.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'> ")

    '                    ''(column gpa / png)
    '                    For Each row As DataRow In DS_PNG.Rows
    '                        For Each column As DataColumn In DS_PNG.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_Png As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_Png As String = oCommon.getFieldValue(get_ENG_Png)

    '                    If data_ENGLITERATURE_Png.Length > 0 Then

    '                        ''english literature Name
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_PNG.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_PNG.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:10%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''(column credit hour / jam kredit)
    '                    For Each row As DataRow In DS_Hour.Rows
    '                        For Each column As DataColumn In DS_Hour.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_HOUR As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                              where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_HOUR As String = oCommon.getFieldValue(get_ENG_HOUR)

    '                    If data_ENGLITERATURE_HOUR.Length > 0 Then

    '                        ''english literature credit hour
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_HOUR.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_HOUR.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> ")

    '                    ''(column total / jumalh)
    '                    For Each row As DataRow In DS_Total.Rows
    '                        For Each column As DataColumn In DS_Total.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_TOTAL As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                              where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_TOTAL As String = oCommon.getFieldValue(get_ENG_TOTAL)

    '                    If data_ENGLITERATURE_TOTAL.Length > 0 Then

    '                        ''english literature total / jumlah
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_TOTAL.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_TOTAL.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Debug.WriteLine("Error 1")
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Dim Number1 As Double = Double.Parse(total_Credit)
    '                    Dim Number2 As Double = Double.Parse(total_Credit_EL)
    '                    Dim Number3 As Double = Double.Parse(total_Total)
    '                    Dim Number4 As Double = Double.Parse(total_Total_EL)

    '                    Dim total_Hour As Double = Number1 + Number2
    '                    Dim final_Total As Double = Number3 + Number4

    '                    Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

    '                    Test.Append("                   </td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'><b> Jumlah </b></td>
    '                                                    <td style='width:10%;border: 1px solid black;font-size:0.8125em !important;'> " & total_Hour & " </td>
    '                                                    <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> " & final_Total & " </td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'><b> PNG Akademik </b></td>
    '                                                    <td style='width:10%;border: 1px solid black;'> </td>
    '                                                    <td style='width:15%;border: 1px solid black;font-size:0.8125em !important;'> " & PNG_Akademik & " </td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Kokurikulum </b></td>
    '                                                    <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'>" & cocuricullum_value & "</td>
    '                                                    <td style='width:8%;border: 1px solid black;'>")

    '                    ''kokorikulum kod sukan
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
    '                            For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    ''kokorikulum kod kelab
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
    '                            For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    ''kokorikulum kod uniform
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
    '                            For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em !important;'>")

    '                    ''kokorikulum nama skan
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
    '                            For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    ''kokorikulum nama kelab
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
    '                            For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    ''kokorikulum nama uniform
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
    '                            For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''kokorikulum gred 
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_GRED.Rows
    '                            For Each column As DataColumn In DSCocuricullum_GRED.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append(" SM <br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''kokorikulum png 
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_PNG.Rows
    '                            For Each column As DataColumn In DSCocuricullum_PNG.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("SM <br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Portfolio </b></td>
    '                                                    <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & portfolio_value & " </td>
    '                                                    <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''Portfolio KOD
    '                    If Confirm_Portfolio = "On" Then
    '                        For Each row As DataRow In DSPortfolio_KOD.Rows
    '                            For Each column As DataColumn In DSPortfolio_KOD.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Portfolio = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''Portfolio Gred
    '                    If Confirm_Portfolio = "On" Then
    '                        For Each row As DataRow In DSPortfolio_GRED.Rows
    '                            For Each column As DataColumn In DSPortfolio_GRED.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Portfolio = "Off" Then
    '                        Test.Append("SM <br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''Portfolio PNG
    '                    If Confirm_Portfolio = "On" Then
    '                        For Each row As DataRow In DSPortfolio_PNG.Rows
    '                            For Each column As DataColumn In DSPortfolio_PNG.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Portfolio = "Off" Then
    '                        Test.Append("SM <br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Penyelidikan </b></td>
    '                                                    <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & research_value & " </td>
    '                                                    <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''research KOD
    '                    If Confirm_Research = "On" Then
    '                        For Each row As DataRow In DSResearch_KOD.Rows
    '                            For Each column As DataColumn In DSResearch_KOD.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("</td>")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Research = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    Test.Append("</td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''research GRED
    '                    If Confirm_Research = "On" Then
    '                        For Each row As DataRow In DSResearch_GRED.Rows
    '                            For Each column As DataColumn In DSResearch_GRED.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("</td>")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Research = "Off" Then
    '                        Test.Append(" SM <br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''research PNG
    '                    If Confirm_Research = "On" Then
    '                        For Each row As DataRow In DSResearch_PNG.Rows
    '                            For Each column As DataColumn In DSResearch_PNG.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("</td>")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Research = "Off" Then
    '                        Test.Append(" SM <br />")
    '                    End If

    '                    Test.Append("</td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> Pembangunan Kendiri </b></td>
    '                                                    <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'> " & sd_value & " </td>
    '                                                    <td style='width:8%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''(column self development codde / pembangunan kendiri kod)
    '                    For Each row As DataRow In DSSelfdevelopment_KOD.Rows
    '                        For Each column As DataColumn In DSSelfdevelopment_KOD.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Test.Append("                    </td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''(column self development grade / pembangunan kendiri gred)
    '                    For Each row As DataRow In DSSelfdevelopment_GRED.Rows
    '                        For Each column As DataColumn In DSSelfdevelopment_GRED.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black;font-size:0.8125em !important;'>")

    '                    ''(column self development gpa / pembangunan kendiri png)
    '                    For Each row As DataRow In DSSelfdevelopment_PNG.Rows
    '                        For Each column As DataColumn In DSSelfdevelopment_PNG.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Test.Append("                   </td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> PNG </b></td>
    '                                                    <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'><b> " & gpa & " </b></td>
    '                                                    <td style='width:8%;border: 1px solid black;'></td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;'></td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black;font-size:0.8125em !important;'><b> PNGK </b></td>
    '                                                    <td style='width:7%;border: 1px solid black;font-size:0.8125em !important;'><b> " & cgpa & "</b></td>
    '                                                    <td style='width:8%;border: 1px solid black;'></td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;'></td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                            </table>
    '                                        </tr>
    '                                     </table>    
    '                                 </div>")

    '                End If
    '            End If
    '        Next

    '        Test.AppendLine(" </div> </div>")
    '        Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataTESTBM'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

    '        'print
    '        Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

    '    ElseIf Lang = "BI" Then

    '        Test.AppendLine("<div id='data' style='display:none'>")
    '        Test.AppendLine("<div id='dataTESTBI'> ")

    '        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
    '            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
    '            If Not chkUpdate Is Nothing Then
    '                ' Get the values of textboxes using findControl
    '                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
    '                If chkUpdate.Checked = True Then

    '                    ''print subject name 
    '                    tmpSQL_Nama = "SELECT subject_Name FROM [ExamSlip_SubjectName] 
    '                                where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_Name ASC"
    '                    Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

    '                    ''print subject code
    '                    tmpSQL_Kod = "SELECT subject_code FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_Name ASC"
    '                    Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

    '                    ''print subject grade
    '                    tmpSQL_Gred = "SELECT grade FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_Name ASC"
    '                    Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

    '                    ''print subject png
    '                    tmpSQL_PNG = "SELECT gpa FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_Name ASC"
    '                    Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

    '                    ''print subject credit hour
    '                    tmpSQL_Hour = "SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_Name ASC"
    '                    Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

    '                    ''print subject credit hour
    '                    tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name, subject_Name ASC"
    '                    Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)


    '                    ''''''''''''''''''''''''''''''checking student status on/off

    '                    'get Portfolio percentage on / off
    '                    Dim check_portfolio_percen As String = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim Confirm_Portfolio As String = oCommon.getFieldValue(check_portfolio_percen)

    '                    ''get cocuricullum percentage on / off
    '                    Dim check_cocuricullum_percen As String = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim Confirm_Cocuricullum As String = oCommon.getFieldValue(check_cocuricullum_percen)

    '                    ''get research percentage on / off
    '                    Dim check_research_percen As String = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim Confirm_Research As String = oCommon.getFieldValue(check_research_percen)

    '                    ''get self development percentage on / off
    '                    Dim check_self_percen As String = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim Confirm_Self As String = oCommon.getFieldValue(check_self_percen)


    '                    tmpSQL = "select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
    '                    Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select SUM(total) FROM [ExamSlip_SubjectName] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
    '                    Dim total_Total As String = oCommon.getFieldValue(tmpSQL)

    '                    Dim DS_Nama As New DataTable
    '                    Dim DS_Kod As New DataTable
    '                    Dim DS_Gred As New DataTable
    '                    Dim DS_PNG As New DataTable
    '                    Dim DS_Hour As New DataTable
    '                    Dim DS_Total As New DataTable

    '                    Dim DSSelfdevelopment_GRED As New DataTable
    '                    Dim DSSelfdevelopment_PNG As New DataTable
    '                    Dim DSSelfdevelopment_KOD As New DataTable

    '                    Dim DSEnglish_literature_SUBJECT As New DataTable
    '                    Dim DSEnglish_literature_GRED As New DataTable
    '                    Dim DSEnglish_literature_PNG As New DataTable
    '                    Dim DSEnglish_literature_KOD As New DataTable
    '                    Dim DSEnglish_literature_HOUR As New DataTable
    '                    Dim DSEnglish_literature_TOTAL As New DataTable

    '                    Dim DSResearch_GRED As New DataTable
    '                    Dim DSResearch_PNG As New DataTable
    '                    Dim DSResearch_KOD As New DataTable

    '                    Dim DSPortfolio_GRED As New DataTable
    '                    Dim DSPortfolio_PNG As New DataTable
    '                    Dim DSPortfolio_KOD As New DataTable

    '                    Dim DSCocuricullum_KOD_SUKAN As New DataTable
    '                    Dim DSCocuricullum_KOD_UNIFORM As New DataTable
    '                    Dim DSCocuricullum_KOD_KELAB As New DataTable
    '                    Dim DSCocuricullum_NAMA_SUKAN As New DataTable
    '                    Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
    '                    Dim DSCocuricullum_NAMA_KELAB As New DataTable
    '                    Dim DSCocuricullum_GRED As New DataTable
    '                    Dim DSCocuricullum_PNG As New DataTable

    '                    Dim total_Credit_EL As String = "0"
    '                    Dim total_Total_EL As String = "0"

    '                    ''print english literature
    '                    If Confirm_Eng_Literature = "On" Then
    '                        tmpsql_EL_Subject = "SELECT subject_Name FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_GRED = "SELECT grade FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_PNG = "SELECT gpa FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_KOD = "SELECT subject_code FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_HOUR = "SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_EL_TOTAL = "SELECT total FROM [ExamSlip_English_Literature] 
    '                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "


    '                        tmpSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
    '                        total_Credit_EL = oCommon.getFieldValue(tmpSQL)

    '                        If total_Credit_EL.Length = 0 Then
    '                            total_Credit_EL = "0"
    '                        End If


    '                        tmpSQL = "select total FROM [ExamSlip_English_Literature] 
    '                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
    '                        total_Total_EL = oCommon.getFieldValue(tmpSQL)

    '                        If total_Total_EL.Length = 0 Then
    '                            total_Total_EL = "0"
    '                        End If

    '                        Dim SQEnglish_Literature_SUBJECT As New SqlDataAdapter(tmpsql_EL_Subject, strConn)
    '                        Dim SQEnglish_Literature_GRED As New SqlDataAdapter(tmpsql_EL_GRED, strConn)
    '                        Dim SQEnglish_Literature_PNG As New SqlDataAdapter(tmpsql_EL_PNG, strConn)
    '                        Dim SQEnglish_Literature_KOD As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
    '                        Dim SQEnglish_Literature_HOUR As New SqlDataAdapter(tmpsql_EL_HOUR, strConn)
    '                        Dim SQEnglish_Literature_TOTAL As New SqlDataAdapter(tmpsql_EL_TOTAL, strConn)

    '                        Try
    '                            SQEnglish_Literature_SUBJECT.Fill(DSEnglish_literature_SUBJECT)
    '                            SQEnglish_Literature_GRED.Fill(DSEnglish_literature_GRED)
    '                            SQEnglish_Literature_KOD.Fill(DSEnglish_literature_KOD)
    '                            SQEnglish_Literature_PNG.Fill(DSEnglish_literature_PNG)
    '                            SQEnglish_Literature_HOUR.Fill(DSEnglish_literature_HOUR)
    '                            SQEnglish_Literature_TOTAL.Fill(DSEnglish_literature_TOTAL)
    '                        Catch ex As Exception

    '                        End Try
    '                    End If

    '                    ''print Portfolio
    '                    If Confirm_Portfolio = "On" Then
    '                        tmpsql_Portfolio_GRED = "SELECT grade FROM [ExamSlip_Portfolio] 
    '                                                 where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
    '                                                 where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
    '                                                 where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        Dim SQPortfolio_GRED As New SqlDataAdapter(tmpsql_Portfolio_GRED, strConn)
    '                        Dim SQPortfolio_PNG As New SqlDataAdapter(tmpsql_Portfolio_PNG, strConn)
    '                        Dim SQPortfolio_KOD As New SqlDataAdapter(tmpsql_Portfolio_KOD, strConn)

    '                        Try
    '                            SQPortfolio_GRED.Fill(DSPortfolio_GRED)
    '                            SQPortfolio_PNG.Fill(DSPortfolio_PNG)
    '                            SQPortfolio_KOD.Fill(DSPortfolio_KOD)
    '                        Catch ex As Exception

    '                        End Try
    '                    End If

    '                    ''print research 
    '                    If Confirm_Research = "On" Then
    '                        tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
    '                                                    where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_Penyelidikan_PNG = "SELECT gpa FROM [ExamSlip_Research] 
    '                                                    where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        tmpsql_Penyelidikan_KOD = "SELECT subject_code FROM [ExamSlip_Research] 
    '                                                    where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        Dim SQResearch_GRED As New SqlDataAdapter(tmpsql_Penyelidikan_Gred, strConn)
    '                        Dim SQResearch_PNG As New SqlDataAdapter(tmpsql_Penyelidikan_PNG, strConn)
    '                        Dim SQResearch_KOD As New SqlDataAdapter(tmpsql_Penyelidikan_KOD, strConn)

    '                        Try
    '                            SQResearch_GRED.Fill(DSResearch_GRED)
    '                            SQResearch_PNG.Fill(DSResearch_PNG)
    '                            SQResearch_KOD.Fill(DSResearch_KOD)
    '                        Catch ex As Exception

    '                        End Try
    '                    End If

    '                    ''print self development
    '                    If Confirm_Self = "On" Then
    '                        Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' "
    '                        Dim getLevel As String = oCommon.getFieldValue(level)

    '                        If getLevel <> "Level 1" And getLevel <> "Level 2" Then
    '                            tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                            tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                            tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
    '                            tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                            tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                            tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
    '                                              where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

    '                        End If

    '                        Dim SQSelfdevelopment_GRED As New SqlDataAdapter(tmpSQL_SD_GRED, strConn)
    '                        Dim SQSelfdevelopment_PNG As New SqlDataAdapter(tmpsql_SD_PNG, strConn)
    '                        Dim SQSelfdevelopment_KOD As New SqlDataAdapter(tmpsql_SD_KOD, strConn)

    '                        Try
    '                            SQSelfdevelopment_GRED.Fill(DSSelfdevelopment_GRED)
    '                            SQSelfdevelopment_PNG.Fill(DSSelfdevelopment_PNG)
    '                            SQSelfdevelopment_KOD.Fill(DSSelfdevelopment_KOD)
    '                        Catch ex As Exception

    '                        End Try
    '                    End If

    '                    '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
    '                    If Confirm_Cocuricullum = "On" Then

    '                        Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
    '                        Dim getStudent As String = oCommon.getFieldValue(studentData)

    '                        If ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 10" Then

    '                            tmpsql_KOKO_PNG = "select koko_pelajar.PNGP1 from koko_pelajar
    '                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                      where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

    '                            tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
    '                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                      where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

    '                            tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

    '                            tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

    '                            tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMABI from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

    '                            tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

    '                            tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

    '                            tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

    '                            Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
    '                            Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
    '                            Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
    '                            Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
    '                            Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

    '                            Try
    '                                SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
    '                                SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
    '                                SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
    '                                SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
    '                                SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
    '                                SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
    '                                SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
    '                                SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
    '                            Catch ex As Exception

    '                            End Try

    '                        ElseIf ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 12" Then

    '                            tmpsql_KOKO_PNG = "select koko_pelajar.PNGP2 from koko_pelajar
    '                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                      where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

    '                            tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
    '                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                      where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

    '                            tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

    '                            tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

    '                            tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMABI from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

    '                            tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

    '                            tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

    '                            tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
    '                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
    '                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
    '                                                      where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

    '                            Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
    '                            Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
    '                            Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
    '                            Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
    '                            Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
    '                            Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

    '                            Try
    '                                SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
    '                                SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
    '                                SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
    '                                SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
    '                                SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
    '                                SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
    '                                SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
    '                                SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
    '                            Catch ex As Exception

    '                            End Try

    '                        End If
    '                    End If

    '                    Try
    '                        SQA.Fill(DS_Nama)
    '                        SQACODE.Fill(DS_Kod)
    '                        SQAPNG.Fill(DS_PNG)
    '                        SQAGRADE.Fill(DS_Gred)
    '                        SQAHOUR.Fill(DS_Hour)
    '                        SQATOTAL.Fill(DS_Total)
    '                    Catch ex As Exception
    '                    End Try

    '                    ''print student name
    '                    Dim stdName As String = "select UPPER(student_Name) from student_info where std_ID = '" & strKey & "'"
    '                    Dim dataStdName As String = oCommon.getFieldValue(stdName)

    '                    ''print student id
    '                    Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
    '                    Dim dataStdID As String = oCommon.getFieldValue(stdID)

    '                    ''print student mykad
    '                    Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
    '                    Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

    '                    ''print exam Name
    '                    Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlexam_Name.SelectedValue & "'"
    '                    Dim dataExmName As String = oCommon.getFieldValue(exmName)

    '                    If dataExmName = "Exam 1" Then
    '                        dataExmName = "Assessment 1 Semester 1, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 2" Then
    '                        dataExmName = "Assessment 2 Semester 1, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 3" Then
    '                        dataExmName = "Assessment 1 Semester 2, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 4" Then
    '                        dataExmName = "Assessment 2 Semester 2, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 5" Then
    '                        dataExmName = "Assessment 1 Semester 1, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 6" Then
    '                        dataExmName = "Assessment 2 Semester 1, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 7" Then
    '                        dataExmName = "Assessment 1 Semester 2, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 8" Then
    '                        dataExmName = "Assessment 2 Semester 2, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 9" Then
    '                        dataExmName = "Assessment 1 Semester 1, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 10" Then
    '                        dataExmName = "Assessment 2 Semester 1, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 11" Then
    '                        dataExmName = "Assessment 1 Semester 2, Academic Year " & ddlyear.SelectedValue
    '                    ElseIf dataExmName = "Exam 12" Then
    '                        dataExmName = "Assessment 2 Semester 2, Academic Year " & ddlyear.SelectedValue
    '                    End If

    '                    ''get month
    '                    Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
    '                    Dim dataMonth As String = oCommon.getFieldValue(month)

    '                    Dim dataStdMonth As String = ""

    '                    If dataMonth = "1" Then
    '                        dataStdMonth = "January"
    '                    ElseIf dataMonth = "2" Then
    '                        dataStdMonth = "February"
    '                    ElseIf dataMonth = "3" Then
    '                        dataStdMonth = "March"
    '                    ElseIf dataMonth = "4" Then
    '                        dataStdMonth = "April"
    '                    ElseIf dataMonth = "5" Then
    '                        dataStdMonth = "May"
    '                    ElseIf dataMonth = "6" Then
    '                        dataStdMonth = "Jun"
    '                    ElseIf dataMonth = "7" Then
    '                        dataStdMonth = "July"
    '                    ElseIf dataMonth = "8" Then
    '                        dataStdMonth = "August"
    '                    ElseIf dataMonth = "9" Then
    '                        dataStdMonth = "September"
    '                    ElseIf dataMonth = "10" Then
    '                        dataStdMonth = "Ocotber"
    '                    ElseIf dataMonth = "11" Then
    '                        dataStdMonth = "November"
    '                    ElseIf dataMonth = "12" Then
    '                        dataStdMonth = "December"
    '                    End If

    '                    ''get PNG & PNGK 
    '                    Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

    '                    Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

    '                    Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
    '                    Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

    '                    ''round to 2 decimal places
    '                    Dim gpa As Decimal = png_dec.ToString("F2")
    '                    Dim cgpa As Decimal = pngs_dec.ToString("F2")


    '                    tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim research_value As String = oCommon.getFieldValue(tmpSQL)

    '                    tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
    '                    Dim sd_value As String = oCommon.getFieldValue(tmpSQL)

    '                    ''first column
    '                    Test.Append("<div style='margin:0;page-break-after: always;'>
    '                                    <table style='width:100%'>
    '                                        <tr style='width:100%'>
    '                                            <td style='width:100%'>
    '                                                <table tyle='width:100%'>
    '                                                    <tr style='width:100%'>
    '                                                        <td>
    '                                                            <img src='img/ukm.jpg'  height='56' width='120'>
    '                                                            &nbsp;
    '                                                            <img src='img/logo genius pintar.png' height='62' width='120'>
    '                                                        </td>
    '                                                    </tr>
    '                                                </table>
    '                                            </td>
    '                                        </tr>
    '                                        <tr style='width:100%'>    
    '                                            <td style='width:100%'>
    '                                                <table style='width:100%'>
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> Name </td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdName & "</td>
    '                                                    </tr>     
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> NRIC </td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdMykad & "</td>
    '                                                    </tr>     
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> Student ID </td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & dataStdID & "</td>
    '                                                    </tr>  
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> Assessment </td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & dataExmName & "</td>
    '                                                    </tr>
    '                                                    <tr style='width:100%'>
    '                                                        <td style='width:10%; font-size:0.8125em !important;'> Assessment Year</td>
    '                                                        <td style='width:90%; font-size:0.8125em !important;'>: " & ddlyear.SelectedValue & "</td>
    '                                                    </tr>
    '                                                </table>    
    '                                            </td>
    '                                        </tr>
    '                                    </table>

    '                                    <table style='width:100%; padding-top:5px'>
    '                                        <tr>
    '                                            <td>
    '                                                <p></p>
    '                                            </td>
    '                                            <table style='border: 1px solid black;border-collapse: collapse;'>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Component </b></td>
    '                                                    <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'><b> Percentage </b></td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'><b> Course Code </b></td>
    '                                                    <td style='width:30%;border: 1px solid black; font-size:0.8125em !important;'><b> Course </b></td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'><b> Grade </b></td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'><b> PNG </b></td>
    '                                                    <td style='width:10%;border: 1px solid black; font-size:0.8125em !important;'><b> Credit Hour </b></td>
    '                                                    <td style='width:15%;border: 1px solid black; font-size:0.8125em !important;'><b> PNG x Credit Hour </b></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center '>
    '                                                    <td rowspan='3' style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Academic </b></td>
    '                                                    <td rowspan='3'style='width:7%;border: 1px solid black; font-size:0.8125em !important;'> " & academic_value & "</td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''(column course code / kod kursus)
    '                    For Each row As DataRow In DS_Kod.Rows
    '                        For Each column As DataColumn In DS_Kod.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_KOD As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                              where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_KOD As String = oCommon.getFieldValue(get_ENG_KOD)

    '                    If data_ENGLITERATURE_KOD.Length > 0 Then

    '                        ''english literature kod
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_KOD.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_KOD.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Test.Append("                    </td>
    '                                                    <td style='width:30%;border: 1px solid black;text-align:left; font-size:0.8125em !important;'>")

    '                    ''(column course / kursus)
    '                    For Each row As DataRow In DS_Nama.Rows
    '                        For Each column As DataColumn In DS_Nama.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_NAMA As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                              where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_NAMA As String = oCommon.getFieldValue(get_ENG_NAMA)

    '                    If data_ENGLITERATURE_NAMA.Length > 0 Then

    '                        ''english literature NAMA
    '                        For Each row As DataRow In DSEnglish_literature_SUBJECT.Rows
    '                            For Each column As DataColumn In DSEnglish_literature_SUBJECT.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next

    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'> ")

    '                    ''(column grade / gred)
    '                    For Each row As DataRow In DS_Gred.Rows
    '                        For Each column As DataColumn In DS_Gred.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_Grade As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_Grade As String = oCommon.getFieldValue(get_ENG_Grade)

    '                    If data_ENGLITERATURE_Grade.Length > 0 Then

    '                        ''english literature Name
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_GRED.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_GRED.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'> ")

    '                    ''(column gpa / png)
    '                    For Each row As DataRow In DS_PNG.Rows
    '                        For Each column As DataColumn In DS_PNG.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_Png As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_Png As String = oCommon.getFieldValue(get_ENG_Png)

    '                    If data_ENGLITERATURE_Png.Length > 0 Then

    '                        ''english literature Name
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_PNG.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_PNG.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:10%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''(column credit hour / jam kredit)
    '                    For Each row As DataRow In DS_Hour.Rows
    '                        For Each column As DataColumn In DS_Hour.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_HOUR As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                              where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_HOUR As String = oCommon.getFieldValue(get_ENG_HOUR)

    '                    If data_ENGLITERATURE_HOUR.Length > 0 Then

    '                        ''english literature credit hour
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_HOUR.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_HOUR.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:15%;border: 1px solid black; font-size:0.8125em !important;'> ")

    '                    ''(column total / jumalh)
    '                    For Each row As DataRow In DS_Total.Rows
    '                        For Each column As DataColumn In DS_Total.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Dim get_ENG_TOTAL As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
    '                                              where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
    '                    Dim data_ENGLITERATURE_TOTAL As String = oCommon.getFieldValue(get_ENG_TOTAL)

    '                    If data_ENGLITERATURE_TOTAL.Length > 0 Then

    '                        ''english literature total / jumlah
    '                        If Confirm_Eng_Literature = "On" Then
    '                            For Each row As DataRow In DSEnglish_literature_TOTAL.Rows
    '                                For Each column As DataColumn In DSEnglish_literature_TOTAL.Columns
    '                                    Test.Append(row(column.ColumnName))
    '                                    Test.Append("<br />")
    '                                Next
    '                            Next

    '                        ElseIf Confirm_Eng_Literature = "Off" Then
    '                            Debug.WriteLine("Error 1")
    '                            Test.Append(" SM <br />")
    '                        End If

    '                    End If

    '                    Dim Number1 As Double = Double.Parse(total_Credit)
    '                    Dim Number2 As Double = Double.Parse(total_Credit_EL)
    '                    Dim Number3 As Double = Double.Parse(total_Total)
    '                    Dim Number4 As Double = Double.Parse(total_Total_EL)

    '                    Dim total_Hour As Double = Number1 + Number2
    '                    Dim final_Total As Double = Number3 + Number4

    '                    Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

    '                    Test.Append("                   </td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td colspan='4'style='width:8%;border: 1px solid black;text-align:left; font-size:0.8125em !important;'><b> Total </b></td>
    '                                                    <td style='width:10%;border: 1px solid black; font-size:0.8125em !important;'> " & total_Hour & " </td>
    '                                                    <td style='width:15%;border: 1px solid black; font-size:0.8125em !important;'> " & final_Total & " </td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td colspan='4'style='width:8%;border: 1px solid black;text-align:left; font-size:0.8125em !important;'><b> Academic PNG </b></td>
    '                                                    <td style='width:10%;border: 1px solid black;'> </td>
    '                                                    <td style='width:15%;border: 1px solid black; font-size:0.8125em !important;'> " & PNG_Akademik & " </td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Cocurriculum </b></td>
    '                                                    <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'>" & cocuricullum_value & "</td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''kokorikulum kod sukan
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
    '                            For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    ''kokorikulum kod kelab
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
    '                            For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    ''kokorikulum kod uniform
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
    '                            For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:30%;border: 1px solid black;text-align:left; font-size:0.8125em !important;'>")

    '                    ''kokorikulum nama skan
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
    '                            For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    ''kokorikulum nama kelab
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
    '                            For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    ''kokorikulum nama uniform
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
    '                            For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''kokorikulum gred 
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_GRED.Rows
    '                            For Each column As DataColumn In DSCocuricullum_GRED.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append("SM <br />")
    '                    End If

    '                    Test.Append("</td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''kokorikulum png 
    '                    If Confirm_Cocuricullum = "On" Then
    '                        For Each row As DataRow In DSCocuricullum_PNG.Rows
    '                            For Each column As DataColumn In DSCocuricullum_PNG.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Cocuricullum = "Off" Then
    '                        Test.Append(" SM <br />")
    '                    End If

    '                    Test.Append("</td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Portfolio </b></td>
    '                                                    <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'> " & portfolio_value & " </td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''Portfolio KOD
    '                    If Confirm_Portfolio = "On" Then
    '                        For Each row As DataRow In DSPortfolio_KOD.Rows
    '                            For Each column As DataColumn In DSPortfolio_KOD.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Portfolio = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''Portfolio Gred
    '                    If Confirm_Portfolio = "On" Then
    '                        For Each row As DataRow In DSPortfolio_GRED.Rows
    '                            For Each column As DataColumn In DSPortfolio_GRED.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Portfolio = "Off" Then
    '                        Test.Append(" SM <br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''Portfolio PNG
    '                    If Confirm_Portfolio = "On" Then
    '                        For Each row As DataRow In DSPortfolio_PNG.Rows
    '                            For Each column As DataColumn In DSPortfolio_PNG.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("<br />")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Portfolio = "Off" Then
    '                        Test.Append(" SM <br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Research </b></td>
    '                                                    <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'> " & research_value & " </td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''research KOD
    '                    If Confirm_Research = "On" Then
    '                        For Each row As DataRow In DSResearch_KOD.Rows
    '                            For Each column As DataColumn In DSResearch_KOD.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("</td>")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Research = "Off" Then
    '                        Test.Append("<br />")
    '                    End If

    '                    Test.Append("</td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''research GRED
    '                    If Confirm_Research = "On" Then
    '                        For Each row As DataRow In DSResearch_GRED.Rows
    '                            For Each column As DataColumn In DSResearch_GRED.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("</td>")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Research = "Off" Then
    '                        Test.Append(" SM <br />")
    '                    End If

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''research PNG
    '                    If Confirm_Research = "On" Then
    '                        For Each row As DataRow In DSResearch_PNG.Rows
    '                            For Each column As DataColumn In DSResearch_PNG.Columns
    '                                Test.Append(row(column.ColumnName))
    '                                Test.Append("</td>")
    '                            Next
    '                        Next
    '                    ElseIf Confirm_Research = "Off" Then
    '                        Test.Append(" SM <br />")
    '                    End If

    '                    Test.Append("</td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> Self Development </b></td>
    '                                                    <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'> " & sd_value & " </td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''(column self development codde / pembangunan kendiri kod)
    '                    For Each row As DataRow In DSSelfdevelopment_KOD.Rows
    '                        For Each column As DataColumn In DSSelfdevelopment_KOD.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Test.Append("                    </td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''(column self development grade / pembangunan kendiri gred)
    '                    For Each row As DataRow In DSSelfdevelopment_GRED.Rows
    '                        For Each column As DataColumn In DSSelfdevelopment_GRED.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append("<br />")
    '                        Next
    '                    Next

    '                    Test.Append("                   </td>
    '                                                    <td style='width:5%;border: 1px solid black; font-size:0.8125em !important;'>")

    '                    ''(column self development gpa / pembangunan kendiri png)
    '                    For Each row As DataRow In DSSelfdevelopment_PNG.Rows
    '                        For Each column As DataColumn In DSSelfdevelopment_PNG.Columns
    '                            Test.Append(row(column.ColumnName))
    '                            Test.Append(" <br />")
    '                        Next
    '                    Next

    '                    Test.Append("                   </td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> GPA </b></td>
    '                                                    <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'><b> " & gpa & " </b></td>
    '                                                    <td style='width:8%;border: 1px solid black; font-size:0.8125em !important;'></td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;'></td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                                <tr style='width:100%;border: 1px solid black;text-align:center'>
    '                                                    <td style='width:20%;border: 1px solid black; font-size:0.8125em !important;'><b> CGPA </b></td>
    '                                                    <td style='width:7%;border: 1px solid black; font-size:0.8125em !important;'><b> " & cgpa & "</b></td>
    '                                                    <td style='width:8%;border: 1px solid black;'></td>
    '                                                    <td style='width:30%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;'></td>
    '                                                    <td style='width:5%;border: 1px solid black;'></td>
    '                                                    <td style='width:10%;border: 1px solid black;'></td>
    '                                                    <td style='width:15%;border: 1px solid black;'></td>
    '                                                </tr>
    '                                            </table>
    '                                        </tr>
    '                                     </table>    
    '                                 </div>")

    '                End If
    '            End If
    '        Next

    '        Test.AppendLine(" </div> </div>")
    '        Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataTESTBI'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

    '        ''print
    '        Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

    '    End If

    'End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Dim get_ExamID As String = "select exam_ID from exam_info where exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_year='" & ddlyear.SelectedValue & "' "
        Dim find_ExamID As String = oCommon.getFieldValue(get_ExamID)

        Dim url As String = "Admin_Peperiksaan_Popup_Pelajar.aspx?admin_ID=" & Request.QueryString("admin_ID") & "&std_ID=" & strKeyName & "&exam_ID=" & find_ExamID & "&year=" & ddlyear.SelectedValue

        Dim s As String = "window.open('" & url & "', 'popup_window', 'width=850,height=450,left=300,top=300','resizable=no');"

        Page.ClientScript.RegisterStartupScript([GetType](), "script", s, True)

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Sub official_year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_TrasncriptYear.DataSource = ds
            ddl_TrasncriptYear.DataTextField = "Parameter"
            ddl_TrasncriptYear.DataValueField = "Parameter"
            ddl_TrasncriptYear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub official_type_list()

        strSQL = "select idx from setting where Parameter = 'student_Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_TranscriptType.DataSource = ds
            ddl_TranscriptType.DataTextField = "idx"
            ddl_TranscriptType.DataValueField = "idx"
            ddl_TranscriptType.DataBind()
            ddl_TranscriptType.Items.Insert(0, New ListItem("Select Type", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub official_campus_list()
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

            ddl_TranscriptCampus.DataSource = ds
            ddl_TranscriptCampus.DataTextField = "Parameter"
            ddl_TranscriptCampus.DataValueField = "Value"
            ddl_TranscriptCampus.DataBind()
            ddl_TranscriptCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub official_program_list()
        If ddl_TranscriptCampus.SelectedValue = "APP" Then
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

            ddl_TranscriptProgram.DataSource = ds
            ddl_TranscriptProgram.DataTextField = "Parameter"
            ddl_TranscriptProgram.DataValueField = "Value"
            ddl_TranscriptProgram.DataBind()
            ddl_TranscriptProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddl_TrasncriptYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_TrasncriptYear.SelectedIndexChanged
        Try
            strRet = BindDataOfficial(TranscriptRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_TranscriptType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_TranscriptType.SelectedIndexChanged
        Try
            strRet = BindDataOfficial(TranscriptRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_TranscriptCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_TranscriptCampus.SelectedIndexChanged
        Try
            official_program_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_TranscriptProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_TranscriptProgram.SelectedIndexChanged
        Try
            strRet = BindDataOfficial(TranscriptRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub official_load_page()
        strSQL = "SELECT Parameter from setting where type = 'Year' and Parameter = '" & Now.Year & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddl_TrasncriptYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_TrasncriptYear.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Function BindDataOfficial(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLOfficial, strConn)
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

    Private Function getSQLOfficial() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select distinct student_png.std_ID, student_info.student_Name, student_info.student_ID, student_info.student_Mykad, student_info.student_Email from student_Png
                  left join student_info on student_Png.std_ID = student_info.std_ID
                  left join student_level on student_info.std_ID = student_level.std_ID"
        strWhere = " WHERE student_Png.year = '" & ddl_TrasncriptYear.SelectedValue & "' and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and student_info.student_ID is not null and student_info.student_Campus = '" & ddl_TranscriptCampus.SelectedValue & "' and student_info.student_Stream = '" & ddl_TranscriptProgram.SelectedValue & "'"

        If ddl_TranscriptType.SelectedValue = "High School" Then
            strWhere += " and student_Png.student_type = 'TAHAP'
                         and student_level.year = '" & ddl_TrasncriptYear.SelectedValue & "'
                         and student_level.student_level = 'Level 2'"

        ElseIf ddl_TranscriptType.SelectedValue = "Junior School" Then
            strWhere += " and student_Png.student_type = 'ASAS'
                         and student_level.year = '" & ddl_TrasncriptYear.SelectedValue & "'
                         and student_level.student_level = 'Foundation 3'"
        End If

        getSQLOfficial = tmpSQL & strWhere & strOrderby

        Return getSQLOfficial
    End Function

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Sub btnOfficialBI_ServerClick(sender As Object, e As EventArgs) Handles btnOfficialBI.ServerClick
        PrintOfficalTranscript("BI")
    End Sub

    Private Sub btnOfficialBM_ServerClick(sender As Object, e As EventArgs) Handles btnOfficialBM.ServerClick
        PrintOfficalTranscript("BM")
    End Sub

    Private Sub PrintOfficalTranscript(Lang As String)

        Dim tmpSQL As String = ""
        Dim tmpSQL_Nama As String = ""
        Dim tmpSQL_Kod As String = ""
        Dim tmpSQL_Gred As String = ""
        Dim tmpSQL_PNG As String = ""
        Dim tmpSQL_Hour As String = ""
        Dim tmpSQL_Total As String = ""

        Dim tmpSQL_SD_GRED As String = ""
        Dim tmpsql_SD_PNG As String = ""
        Dim tmpsql_SD_KOD As String = ""

        Dim tmpsql_Portfolio_GRED As String = ""
        Dim tmpsql_Portfolio_PNG As String = ""
        Dim tmpsql_Portfolio_KOD As String = ""

        Dim tmpsql_Penyelidikan_Gred As String = ""
        Dim tmpsql_Penyelidikan_PNG As String = ""
        Dim tmpsql_Penyelidikan_KOD As String = ""

        Dim tmpsql_EL_GRED As String = ""
        Dim tmpsql_EL_PNG As String = ""
        Dim tmpsql_EL_KOD As String = ""
        Dim tmpsql_EL_HOUR As String = ""
        Dim tmpsql_EL_TOTAL As String = ""

        Dim tmpsql_KOKO_KOD_SUKAN As String = ""
        Dim tmpsql_KOKO_KOD_UNIFORM As String = ""
        Dim tmpsql_KOKO_KOD_KELAB As String = ""
        Dim tmpsql_KOKO_KOD_RENANG As String = ""
        Dim tmpsql_KOKO_NAMA_SUKAN As String = ""
        Dim tmpsql_KOKO_NAMA_KELAB As String = ""
        Dim tmpsql_KOKO_NAMA_UNIFORM As String = ""
        Dim tmpsql_KOKO_NAMA_RENANG As String = ""
        Dim tmpsql_KOKO_GRED As String = ""
        Dim tmpsql_KOKO_PNG As String = ""

        Dim errorCount As Integer = 0
        Dim i As Integer = 0
        Dim Test As New StringBuilder()

        Dim check_portfolio_percen As String = ""
        Dim Confirm_Portfolio As String = ""
        Dim check_cocuricullum_percen As String = ""
        Dim Confirm_Cocuricullum As String = ""
        Dim check_research_percen As String = ""
        Dim Confirm_Research As String = ""
        Dim check_self_percen As String = ""
        Dim Confirm_Self As String = ""
        Dim check_Eng_Literature As String = ""
        Dim Confirm_Eng_Literature As String = ""

        ''check print transcript language
        If Lang = "BM" Then

            Test.AppendLine("<div id='data' style='display:none; padding:0px; margin:0px;'>")
            Test.AppendLine("<div id='dataOfficialTranscriptBM' style='padding-top:0px; margin-top:0px; height: 100%; margin: 0px;'> ")

            If ddl_TranscriptType.SelectedValue = "Junior School" Then

                For i = 0 To TranscriptRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(TranscriptRespondent.Rows(i).Cells(5).FindControl("chkSelectTranscript"), CheckBox)

                    Dim j_examName As Integer = 0
                    Dim countPage As Integer = 0

                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = TranscriptRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''Get Student Name
                            Dim select_StudentName As String = "select UPPER(student_Name) from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentName As String = oCommon.getFieldValue(select_StudentName)

                            ''Get Student ID
                            Dim select_StudentID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue(select_StudentID)

                            ''Get Student MYKAD
                            Dim select_StudentMYKAD As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentMYKAD As String = oCommon.getFieldValue(select_StudentMYKAD)

                            '''Get Student Start Date Year
                            'Dim select_StudentSDY As String = "select year from student_level where std_ID = '" & strKey & "' and student_Level = 'Foundation 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDY As String = oCommon.getFieldValue(select_StudentSDY)

                            '''Get Student Start Date Month
                            'Dim select_StudentSDM As String = "select month from student_level where std_ID = '" & strKey & "' and student_Level = 'Foundation 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDM As String = oCommon.getFieldValue(select_StudentSDM)

                            '''Get Student Start Date Day
                            'Dim select_StudentSDD As String = "select day from student_level where std_ID = '" & strKey & "' and student_Level = 'Foundation 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDD As String = oCommon.getFieldValue(select_StudentSDD)

                            ''Get Student First Date Year
                            Dim find_StudentFDM As String = "Select Value from Setting where Idx = 'Examination' and Type = 'Junior Entry Date'"
                            Dim get_StudentFDM As String = oCommon.getFieldValue(find_StudentFDM)

                            ''Get Student Last Date Year
                            Dim find_StudentLDM As String = "Select Value from Setting where Idx = 'Examination' and Type = 'Junior Term Ending'"
                            Dim get_StudentLDM As String = oCommon.getFieldValue(find_StudentLDM)

                            Dim EndMonth As String = System.DateTime.Now.Month

                            If EndMonth = "1" Then
                                EndMonth = "Januari"
                            ElseIf EndMonth = "2" Then
                                EndMonth = "Februari"
                            ElseIf EndMonth = "3" Then
                                EndMonth = "Mac"
                            ElseIf EndMonth = "4" Then
                                EndMonth = "April"
                            ElseIf EndMonth = "5" Then
                                EndMonth = "Mei"
                            ElseIf EndMonth = "6" Then
                                EndMonth = "Jun"
                            ElseIf EndMonth = "7" Then
                                EndMonth = "Julai"
                            ElseIf EndMonth = "8" Then
                                EndMonth = "Ogos"
                            ElseIf EndMonth = "9" Then
                                EndMonth = "September"
                            ElseIf EndMonth = "10" Then
                                EndMonth = "Oktober"
                            ElseIf EndMonth = "11" Then
                                EndMonth = "November"
                            ElseIf EndMonth = "12" Then
                                EndMonth = "Disember"
                            End If

                            countPage = 1

                            For j_examName = 1 To 12 Step j_examName + 2

                                ''getYear Exam 1,3,5,7,9,11
                                Dim select_ExamYearSem1 As String = "Select year from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and student_type = 'ASAS'"
                                Dim get_ExamYearSem1 As String = oCommon.getFieldValue(select_ExamYearSem1)

                                ''getYear Exam 2,4,6,8,10,12
                                Dim select_ExamYearSem2 As String = "Select year from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and student_type = 'ASAS'"
                                Dim get_ExamYearSem2 As String = oCommon.getFieldValue(select_ExamYearSem2)

                                If get_ExamYearSem1 = "" Then
                                    get_ExamYearSem1 = get_ExamYearSem2
                                End If

                                If get_ExamYearSem2 = "" Then
                                    get_ExamYearSem2 = get_ExamYearSem1
                                End If

                                Dim get_ExamRename As String = ""
                                Dim get_SemesterName As Integer = 0

                                If j_examName = 1 Then
                                    get_ExamRename = "1 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 3 Then
                                    get_ExamRename = "2 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 5 Then
                                    get_ExamRename = "3 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 7 Then
                                    get_ExamRename = "4 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 9 Then
                                    get_ExamRename = "5 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 11 Then
                                    get_ExamRename = "6 SEMESTER "
                                    get_SemesterName = 1
                                End If

                                Test.AppendLine("<div style='margin: 0; page-break-after:always; padding-top:0px; margin-top:0px;height: 100%; display: block; position: relative;'>")

                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic; padding-top:0px; margin-top:0px; border-collapse: collapse;'>
                                                        <tr style='font-size:14px;text-align:center; width:100%'>
                                                            <td colspan='5'><b> TRANSKRIP RASMI KOLEJ GENIUS@Pintar </b></td>
                                                        </tr>
                                                        <tr style='font-size:14px;text-align:center; width:100%;'>
                                                            <td colspan='5'><b> UNIVERSITI KEBANGSAAN MALAYSIA </b></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr style='text-align:left'>
                                                            <td rowspan='8' style='with:40%; padding:0px; margin:0px;height:60px; width:300px;'></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr>
    	                                                    <td colspan='2' style='with:40%; font-size:9px;'>Name :</td>
                                                            <td style='with:20%; font-size:9px; text-align:right'>Tarikh Kemasukan :</td>
                                                        </tr>
                                                        <tr>
    	                                                    <td colspan='2' style='with:40%; font-size:12px;'><b>" & get_StudentName.ToUpper & "</b></td>
                                                            <td style='with:20%; font-size:10px; ; text-align:right'><b>" & get_StudentFDM & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr >
    	                                                    <td style='with:25%; font-size:9px; '>No. Kad Matrik :</td>
                                                            <td style='with:25%; font-size:9px; '>No. Kad Pengenalan :</td>
                                                            <td style='with:10%; font-size:9px; text-align:right'>Tarikh Tamat Pengajian :</td>
                                                        </tr>
                                                        <tr>
    	                                                    <td style='with:25%; font-size:10px; '><b>" & get_StudentID & "</b></td>
                                                            <td style='with:25%; font-size:10px; '><b>" & get_StudentMYKAD & "</b></td>
                                                            <td style='with:10%; font-size:10px; text-align:right'><b>" & get_StudentLDM & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                            
                                                    <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:15px'>
                                                        <tr>
                                                            <td colspan='5' style='border-top:0.5px solid black; border-bottom:0.5px solid black; background-color:lightgray; font-size:9.5px; text-align:center'><b> PENTAKSIRAN " & get_ExamRename & get_SemesterName & ", TAHUN AKADEMIK " & get_ExamYearSem1 & " </b></td>
                                                        </tr>
                                                    </table>")

                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''' SEMESTER 1 - SEMESTER 1 - SEMESTER 1 - SEMESTER 1 - SEMESTER 1 '''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                'get Portfolio percentage on / off
                                check_portfolio_percen = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

                                ''get cocuricullum percentage on / off
                                check_cocuricullum_percen = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

                                ''get research percentage on / off
                                check_research_percen = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Research = oCommon.getFieldValue(check_research_percen)

                                ''get self development percentage on / off
                                check_self_percen = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Self = oCommon.getFieldValue(check_self_percen)

                                'get englih literture on / off
                                check_Eng_Literature = "select Value from setting where Type = 'English Literature'"
                                Confirm_Eng_Literature = oCommon.getFieldValue(check_Eng_Literature)

                                ''print subject name 
                                tmpSQL_Nama = " SELECT subject_NameBM FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                ''print subject code
                                tmpSQL_Kod = "  SELECT subject_code FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                ''print subject grade
                                tmpSQL_Gred = " SELECT grade FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

                                ''print subject png
                                tmpSQL_PNG = "  SELECT gpa FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

                                ''print subject credit hour
                                tmpSQL_Hour = " SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

                                ''print subject credit hour
                                tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)

                                ''print total credit hour for subject taken
                                tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

                                ''print total PNG x credit hour for subject taken
                                tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Total As String = oCommon.getFieldValue(tmpSQL)

                                ''print academic percentage
                                tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

                                Dim DS_Nama As New DataTable
                                Dim DS_Kod As New DataTable
                                Dim DS_Gred As New DataTable
                                Dim DS_PNG As New DataTable
                                Dim DS_Hour As New DataTable
                                Dim DS_Total As New DataTable

                                Try
                                    SQA.Fill(DS_Nama)
                                    SQACODE.Fill(DS_Kod)
                                    SQAPNG.Fill(DS_PNG)
                                    SQAGRADE.Fill(DS_Gred)
                                    SQAHOUR.Fill(DS_Hour)
                                    SQATOTAL.Fill(DS_Total)
                                Catch ex As Exception
                                End Try

                                Dim DSSelfdevelopment_GRED As String = ""
                                Dim DSSelfdevelopment_PNG As String = ""
                                Dim DSSelfdevelopment_KOD As String = ""

                                Dim DSEnglish_literature_GRED As New DataTable
                                Dim DSEnglish_literature_PNG As New DataTable
                                Dim DSEnglish_literature_KOD As New DataTable
                                Dim DSEnglish_literature_HOUR As New DataTable
                                Dim DSEnglish_literature_TOTAL As New DataTable

                                Dim DSResearch_GRED As String = ""
                                Dim DSResearch_PNG As String = ""
                                Dim DSResearch_KOD As String = ""

                                Dim DSPortfolio_GRED As String = ""
                                Dim DSPortfolio_PNG As String = ""
                                Dim DSPortfolio_KOD As String = ""

                                Dim DSCocuricullum_KOD_SUKAN As New DataTable
                                Dim DSCocuricullum_KOD_UNIFORM As New DataTable
                                Dim DSCocuricullum_KOD_KELAB As New DataTable
                                Dim DSCocuricullum_NAMA_SUKAN As New DataTable
                                Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
                                Dim DSCocuricullum_NAMA_KELAB As New DataTable
                                Dim DSCocurricullum_Gred As String = ""
                                Dim DSCocurricullum_PNG As String = ""

                                Dim total_Credit_EL As String = "0"
                                Dim total_Total_EL As String = "0"

                                tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim research_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim sd_value As String = oCommon.getFieldValue(tmpSQL)


                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic;border-collapse: collapse; margin-top:5px'>
                                                        <tr style='font-size:9px;background-color:lightgray; border-top:0.5px solid black; border-bottom:0.5px solid black;'>
                                                            <td style='width:20%;'><b> Komponent </b></td>
                                                            <td style='width:10%;'><b> Kod Kursus </b></td>
                                                            <td style='width:35%;'><b> Kursus </b></td>
                                                            <td style='width:5%; text-align:center'><b> Gred </b></td>
                                                            <td style='width:5%; text-align:center'><b> PNG </b></td>
                                                            <td style='width:10%; text-align:center'><b> Jam Kredit </b></td>
                                                            <td style='width:15%; text-align:center'><b> PNG x Jam Kredit </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td rowspan='2'><b> Akademik (" & academic_value & "%) </b></td>
                                                            <td ><b>")

                                If get_ExamYearSem1 = "2020" Then
                                    Dim get_Semester As String = ""
                                    Dim get_Level As String = ""

                                    If j_examName = "1" Or j_examName = "5" Or j_examName = "9" Then
                                        get_Semester = "Sem 1"
                                    ElseIf j_examName = "3" Or j_examName = "7" Or j_examName = "11" Then
                                        get_Semester = "Sem 2"
                                    End If

                                    If j_examName = "1" Or j_examName = "3" Then
                                        get_Level = "Foundation 1"
                                    ElseIf j_examName = "5" Or j_examName = "7" Then
                                        get_Level = "Foundation 2"
                                    ElseIf j_examName = "9" Or j_examName = "11" Then
                                        get_Level = "Foundation 3"
                                    End If

                                    ''print subject name for Exam 1, 3, 5
                                    tmpSQL_Nama = " Select subject_NameBM from subject_info left join course on subject_info.subject_ID = course.subject_ID 
                                                    where course.year = '" & get_ExamYearSem1 & "' and subject_info.subject_year = '" & get_ExamYearSem1 & "' and subject_info.course_Name <> 'Portfolio' and subject_info.course_Name <> 'Penyelidikan' and subject_info.course_Name <> 'Pembangunan Kendiri'
                                                    and subject_info.subject_StudentYear = '" & get_Level & "' and subject_info.subject_sem = '" & get_Semester & "' and course.std_ID = '" & strKey & "'
                                                    order by course_Name, subject_NameBM ASC"
                                    Dim SQA_2020 As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                    ''print subject code for Exam 1, 3, 5
                                    tmpSQL_Kod = "  Select subject_code from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                                    where course.year = '" & get_ExamYearSem1 & "' and subject_info.subject_year = '" & get_ExamYearSem1 & "' and subject_info.course_Name <> 'Portfolio' and subject_info.course_Name <> 'Penyelidikan' and subject_info.course_Name <> 'Pembangunan Kendiri'
                                                    and subject_info.subject_StudentYear = '" & get_Level & "' and subject_info.subject_sem = '" & get_Semester & "' and course.std_ID = '" & strKey & "'
                                                    order by course_Name, subject_NameBM ASC"
                                    Dim SQACODE_2020 As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                    Dim DS_Nama_2020 As New DataTable
                                    Dim DS_Kod_2020 As New DataTable

                                    Try
                                        SQA_2020.Fill(DS_Nama_2020)
                                        SQACODE_2020.Fill(DS_Kod_2020)
                                    Catch ex As Exception
                                    End Try

                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod_2020.Rows
                                        For Each column As DataColumn In DS_Kod_2020.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td>")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama_2020.Rows
                                        For Each column As DataColumn In DS_Nama_2020.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next


                                    Test.Append("           </td>
                                                            <td ></td>
                                                            <td ></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td ></td>
                                                        </tr>")
                                Else

                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod.Rows
                                        For Each column As DataColumn In DS_Kod.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td >")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama.Rows
                                        For Each column As DataColumn In DS_Nama.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center'><b>")

                                    ''(column course grade)
                                    For Each row As DataRow In DS_Gred.Rows
                                        For Each column As DataColumn In DS_Gred.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td style='text-align:center;'>")

                                    ''(column course gpa )
                                    For Each row As DataRow In DS_PNG.Rows
                                        For Each column As DataColumn In DS_PNG.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course credit hour)
                                    For Each row As DataRow In DS_Hour.Rows
                                        For Each column As DataColumn In DS_Hour.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course total)
                                    For Each row As DataRow In DS_Total.Rows
                                        For Each column As DataColumn In DS_Total.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.AppendLine("           </td>
                                                        </tr>")
                                End If

                                ''print english literature
                                If Confirm_Eng_Literature = "On" Then

                                    Dim SQEnglish_Literature_GRED As String = ""
                                    Dim SQEnglish_Literature_PNG As String = ""
                                    Dim SQEnglish_Literature_KOD As String = ""
                                    Dim SQEnglish_Literature_HOUR As String = ""
                                    Dim SQEnglish_Literature_TOTAL As String = ""

                                    tmpsql_EL_GRED = "  SELECT grade FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_GRED = oCommon.getFieldValue(tmpsql_EL_GRED)

                                    tmpsql_EL_PNG = "   SELECT gpa FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_PNG = oCommon.getFieldValue(tmpsql_EL_PNG)

                                    tmpsql_EL_KOD = "   SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_KOD = oCommon.getFieldValue(tmpsql_EL_KOD)

                                    tmpsql_EL_HOUR = "  SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_HOUR = oCommon.getFieldValue(tmpsql_EL_HOUR)

                                    tmpsql_EL_TOTAL = " SELECT total FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_TOTAL = oCommon.getFieldValue(tmpsql_EL_TOTAL)

                                    If get_ExamYearSem1 = "2020" Then

                                        Test.Append("   <tr style='font-size:9px;'> 
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    Else

                                        If SQEnglish_Literature_GRED.Length > 0 Then
                                            Test.AppendLine("<tr style='font-size:9px;'>
                                                                <td style='border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_KOD & " </b></td>
                                                                <td style='border-bottom:0.5px solid black'> AP English Literature and Composition </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_GRED & " </b></td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_PNG & " </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_HOUR & " </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_TOTAL & " </td>
                                                            </tr>")

                                            If SQEnglish_Literature_HOUR = "" Then
                                                total_Credit_EL = "0"
                                            End If

                                            If SQEnglish_Literature_TOTAL = "" Then
                                                total_Total_EL = "0"
                                            End If
                                        Else
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                                    <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                                </tr>")
                                        End If
                                    End If

                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                        </tr>")
                                End If

                                If total_Credit = "" Then
                                    total_Credit = "0"
                                End If

                                If total_Total = "" Then
                                    total_Total = "0"
                                End If

                                ''Calculatr Total Credit Hour , PNG x Credit Hour & PNG Academic
                                Dim Number1 As Double = Double.Parse(total_Credit)
                                Dim Number2 As Double = Double.Parse(total_Credit_EL)
                                Dim Number3 As Double = Double.Parse(total_Total)
                                Dim Number4 As Double = Double.Parse(total_Total_EL)

                                Dim total_Hour As Double = Number1 + Number2
                                Dim final_Total As Double = Number3 + Number4

                                Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

                                If total_Hour = 0 And final_Total = 0 Then
                                    PNG_Akademik = 0
                                End If

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right'><b> Jumlah </b></td>
                                                            <td style='text-align:center'><b> " & total_Hour & " </b></td>
                                                            <td style='text-align:center'><b> " & final_Total & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right; border-bottom:0.5px solid black'><b> PNG Akademik </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & PNG_Akademik & " </b></td>
                                                        </tr> ")

                                '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                                If Confirm_Cocuricullum = "oN" Or Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then

                                    Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                    Dim getStudent As String = oCommon.getFieldValue(studentData)

                                    If j_examName = 2 Or j_examName = 6 Or j_examName = 10 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_PNG = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_Gred = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                            SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                            SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                            SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                            SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                            SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                        Catch ex As Exception

                                        End Try

                                    ElseIf j_examName = 4 Or j_examName = 7 Or j_examName = 8 Or j_examName = 12 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "    select koko_kolejpermata.NAMA from koko_pelajar
                                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                      where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                            SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                            SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                            SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                            SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                            SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                        Catch ex As Exception

                                        End Try

                                    End If
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("   <tr style='font-size:9px;'> 
                                                            <td><b> Kokurikulum (" & cocuricullum_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td></td>
                                                        </tr>")
                                Else

                                    Test.AppendLine("        <tr style='font-size:9px;'>
                                                            <td><b> Kokurikulum (" & cocuricullum_value & "%) </b></td>
                                                            <td><b>")


                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum kod sukan
                                        For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod kelab
                                        For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod uniform
                                        For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("PKS317 <br />")

                                    ElseIf Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Or Confirm_Cocuricullum = "OFF" Then
                                        Test.Append("<br />")
                                    End If

                                    Test.AppendLine("           </b></td>
                                                            <td>")

                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum nama skan
                                        For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama kelab
                                        For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama uniform
                                        For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("Renang <br />")

                                    ElseIf Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Then
                                        Test.Append("<br />")
                                    End If

                                    If DSCocurricullum_PNG.Length > 0 Then
                                        Test.AppendLine("       </td>
                                                            <td style='text-align:center'><b> " & DSCocurricullum_Gred & " </b></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG) & " </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("       </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print Portfolio
                                If Confirm_Portfolio = "ON" Or Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
                                    tmpsql_Portfolio_GRED = "   SELECT grade FROM [ExamSlip_Portfolio] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_GRED = oCommon.getFieldValue(tmpsql_Portfolio_GRED)

                                    tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_PNG = oCommon.getFieldValue(tmpsql_Portfolio_PNG)

                                    tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_KOD = oCommon.getFieldValue(tmpsql_Portfolio_KOD)

                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("   <tr style='font-size:9px;'> 
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                Else

                                    If DSPortfolio_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSPortfolio_GRED & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print research 
                                If Confirm_Research = "ON" Or Confirm_Research = "On" Or Confirm_Research = "on" Then
                                    tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_GRED = oCommon.getFieldValue(tmpsql_Penyelidikan_Gred)

                                    tmpsql_Penyelidikan_PNG = " SELECT gpa FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_PNG = oCommon.getFieldValue(tmpsql_Penyelidikan_PNG)

                                    tmpsql_Penyelidikan_KOD = " SELECT subject_code FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_KOD = oCommon.getFieldValue(tmpsql_Penyelidikan_KOD)
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("   <tr style='font-size:9px;'> 
                                                            <td><b> Penyelidikan (" & research_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    If DSResearch_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSResearch_GRED & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print self development
                                If Confirm_Self = "ON" Or Confirm_Self = "On" Or Confirm_Self = "on" Then
                                    Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & get_ExamYearSem1 & "' "
                                    Dim getLevel As String = oCommon.getFieldValue(level)

                                    If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD = oCommon.getFieldValue(tmpsql_SD_KOD)

                                    ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD = oCommon.getFieldValue(tmpsql_SD_KOD)
                                    End If
                                End If


                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("   <tr style='font-size:9px;'> 
                                                            <td><b> Pembangunan Kendiri (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  Sedang Maju  </td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                Else
                                    If DSSelfdevelopment_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_GRED & " </b></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG) & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  Sedang Maju  </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''Print PNG & PNGK 
                                Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

                                Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

                                If exist_png_data = "" Then
                                    exist_png_data = "0"
                                End If

                                If exist_pngs_data = "" Then
                                    exist_pngs_data = "0"
                                End If

                                Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
                                Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> PNG </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & png_dec & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> PNGK </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & pngs_dec & " </b></td>
                                                        </tr>")
                                Test.AppendLine("   </table>")


                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''' SEMESTER 2 - SEMESTER 2 - SEMESTER 2 - SEMESTER 2 - SEMESTER 2 '''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                'get Portfolio percentage on / off
                                check_portfolio_percen = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

                                ''get cocuricullum percentage on / off
                                check_cocuricullum_percen = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

                                ''get research percentage on / off
                                check_research_percen = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Research = oCommon.getFieldValue(check_research_percen)

                                ''get self development percentage on / off
                                check_self_percen = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Self = oCommon.getFieldValue(check_self_percen)


                                ''print subject name 
                                tmpSQL_Nama = " SELECT subject_NameBM FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQA_Sem2 As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                ''print subject code
                                tmpSQL_Kod = "  SELECT subject_code FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQACODE_Sem2 As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                ''print subject grade
                                tmpSQL_Gred = " SELECT grade FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAGRADE_Sem2 As New SqlDataAdapter(tmpSQL_Gred, strConn)

                                ''print subject png
                                tmpSQL_PNG = "  SELECT gpa FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAPNG_Sem2 As New SqlDataAdapter(tmpSQL_PNG, strConn)

                                ''print subject credit hour
                                tmpSQL_Hour = " SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAHOUR_Sem2 As New SqlDataAdapter(tmpSQL_Hour, strConn)

                                ''print subject credit hour
                                tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQATOTAL_Sem2 As New SqlDataAdapter(tmpSQL_Total, strConn)

                                ''print total credit hour for subject taken
                                tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Credit_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                ''print total PNG x credit hour for subject taken
                                tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Total_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                ''print academic percentage
                                tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim academic_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                Dim DS_Nama_Sem2 As New DataTable
                                Dim DS_Kod_Sem2 As New DataTable
                                Dim DS_Gred_Sem2 As New DataTable
                                Dim DS_PNG_Sem2 As New DataTable
                                Dim DS_Hour_Sem2 As New DataTable
                                Dim DS_Total_Sem2 As New DataTable

                                Try
                                    SQA_Sem2.Fill(DS_Nama_Sem2)
                                    SQACODE_Sem2.Fill(DS_Kod_Sem2)
                                    SQAPNG_Sem2.Fill(DS_PNG_Sem2)
                                    SQAGRADE_Sem2.Fill(DS_Gred_Sem2)
                                    SQAHOUR_Sem2.Fill(DS_Hour_Sem2)
                                    SQATOTAL_Sem2.Fill(DS_Total_Sem2)
                                Catch ex As Exception
                                End Try

                                Dim DSSelfdevelopment_GRED_Sem2 As String = ""
                                Dim DSSelfdevelopment_PNG_Sem2 As String = ""
                                Dim DSSelfdevelopment_KOD_Sem2 As String = ""

                                Dim DSEnglish_literature_GRED_Sem2 As New DataTable
                                Dim DSEnglish_literature_PNG_Sem2 As New DataTable
                                Dim DSEnglish_literature_KOD_Sem2 As New DataTable
                                Dim DSEnglish_literature_HOUR_Sem2 As New DataTable
                                Dim DSEnglish_literature_TOTAL_Sem2 As New DataTable

                                Dim DSResearch_GRED_Sem2 As String = ""
                                Dim DSResearch_PNG_Sem2 As String = ""
                                Dim DSResearch_KOD_Sem2 As String = ""

                                Dim DSPortfolio_GRED_Sem2 As String = ""
                                Dim DSPortfolio_PNG_Sem2 As String = ""
                                Dim DSPortfolio_KOD_Sem2 As String = ""

                                Dim DSCocuricullum_KOD_SUKAN_Sem2 As New DataTable
                                Dim DSCocuricullum_KOD_UNIFORM_Sem2 As New DataTable
                                Dim DSCocuricullum_KOD_KELAB_Sem2 As New DataTable
                                Dim DSCocuricullum_NAMA_SUKAN_Sem2 As New DataTable
                                Dim DSCocuricullum_NAMA_UNIFORM_Sem2 As New DataTable
                                Dim DSCocuricullum_NAMA_KELAB_Sem2 As New DataTable
                                Dim DSCocurricullum_Gred_Sem2 As String = ""
                                Dim DSCocurricullum_PNG_Sem2 As String = ""

                                Dim total_Credit_EL_Sem2 As String = "0"
                                Dim total_Total_EL_Sem2 As String = "0"

                                tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim cocuricullum_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim portfolio_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim research_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim sd_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)


                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:15px'>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='5' style='border-top:0.5px solid black; border-bottom:0.5px solid black; background-color:lightgray; font-size:9.5px; text-align:center'><b> PENTAKSIRAN " & get_ExamRename & get_SemesterName + 1 & ", TAHUN AKADEMIK " & get_ExamYearSem1 & " </b></td>
                                                        </tr>
                                                    </table>

                                                    <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:5px'>
                                                        <tr style='font-size:9px;background-color:lightgray'>
                                                            <td style='width:20%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Komponent </b></td>
                                                            <td style='width:10%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Kod Kursus </b></td>
                                                            <td style='width:35%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Kursus </b></td>
                                                            <td style='width:5%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> Gred </b></td>
                                                            <td style='width:5%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> PNG </b></td>
                                                            <td style='width:10%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> Jam Kredit </b></td>
                                                            <td style='width:15%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> PNG x Jam Kredit </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td rowspan='2'><b> Akademik (" & academic_value_Sem2 & "%) </b></td>
                                                            <td ><b>")

                                ''(column course code)
                                For Each row As DataRow In DS_Kod_Sem2.Rows
                                    For Each column As DataColumn In DS_Kod_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </b></td>
                                                            <td >")

                                ''(column course name)
                                For Each row As DataRow In DS_Nama_Sem2.Rows
                                    For Each column As DataColumn In DS_Nama_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </td>
                                                            <td style='text-align:center;'><b>")

                                ''(column course grade)
                                For Each row As DataRow In DS_Gred_Sem2.Rows
                                    For Each column As DataColumn In DS_Gred_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </b></td>
                                                            <td style='text-align:center; '>")

                                ''(column course gpa )
                                For Each row As DataRow In DS_PNG_Sem2.Rows
                                    For Each column As DataColumn In DS_PNG_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                ''(column course credit hour)
                                For Each row As DataRow In DS_Hour_Sem2.Rows
                                    For Each column As DataColumn In DS_Hour_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                ''(column course total)
                                For Each row As DataRow In DS_Total_Sem2.Rows
                                    For Each column As DataColumn In DS_Total_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.AppendLine("           </td>
                                                        </tr>")

                                ''print english literature
                                If Confirm_Eng_Literature = "On" Then

                                    Dim SQEnglish_Literature_GRED As String = ""
                                    Dim SQEnglish_Literature_PNG As String = ""
                                    Dim SQEnglish_Literature_KOD As String = ""
                                    Dim SQEnglish_Literature_HOUR As String = ""
                                    Dim SQEnglish_Literature_TOTAL As String = ""

                                    tmpsql_EL_GRED = "  SELECT grade FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_GRED = oCommon.getFieldValue(tmpsql_EL_GRED)

                                    tmpsql_EL_PNG = "   SELECT gpa FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_PNG = oCommon.getFieldValue(tmpsql_EL_PNG)

                                    tmpsql_EL_KOD = "   SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_KOD = oCommon.getFieldValue(tmpsql_EL_KOD)

                                    tmpsql_EL_HOUR = "  SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_HOUR = oCommon.getFieldValue(tmpsql_EL_HOUR)

                                    tmpsql_EL_TOTAL = " SELECT total FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_TOTAL = oCommon.getFieldValue(tmpsql_EL_TOTAL)

                                    If SQEnglish_Literature_GRED.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td style='border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'> AP English Literature and Composition </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_GRED & " </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_PNG & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_HOUR & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_TOTAL & " </td>
                                                        </tr>")

                                        total_Credit_EL_Sem2 = SQEnglish_Literature_HOUR
                                        total_Total_EL_Sem2 = SQEnglish_Literature_TOTAL
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                                    <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                            </tr>")
                                    End If

                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                        </tr>")
                                End If

                                If total_Credit_Sem2 = "" Then
                                    total_Credit_Sem2 = "0"
                                End If

                                If total_Credit_EL_Sem2 = "" Then
                                    total_Credit_EL_Sem2 = "0"
                                End If

                                If total_Total_Sem2 = "" Then
                                    total_Total_Sem2 = "0"
                                End If

                                If total_Total_EL_Sem2 = "" Then
                                    total_Total_EL_Sem2 = "0"
                                End If

                                ''Calculatr Total Credit Hour , PNG x Credit Hour & PNG Academic
                                Dim Number1_Sem2 As Double = Double.Parse(total_Credit_Sem2)
                                Dim Number2_Sem2 As Double = Double.Parse(total_Credit_EL_Sem2)
                                Dim Number3_Sem2 As Double = Double.Parse(total_Total_Sem2)
                                Dim Number4_Sem2 As Double = Double.Parse(total_Total_EL_Sem2)

                                Dim total_Hour_Sem2 As Double = Number1_Sem2 + Number2_Sem2
                                Dim final_Total_Sem2 As Double = Number3_Sem2 + Number4_Sem2

                                Dim PNG_Akademik_Sem2 As Double = Math.Round(final_Total_Sem2 / total_Hour_Sem2, 2)

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right'><b> Jumlah </b></td>
                                                            <td style='text-align:center'><b> " & total_Hour_Sem2 & " </b></td>
                                                            <td style='text-align:center'><b> " & final_Total_Sem2 & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right; border-bottom:0.5px solid black'><b> PNG Akademik </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & PNG_Akademik_Sem2 & " </b></td>
                                                        </tr> ")

                                '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                                If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then

                                    Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                    Dim getStudent As String = oCommon.getFieldValue(studentData)

                                    If j_examName + 1 = 2 Or j_examName + 1 = 6 Or j_examName + 1 = 10 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_PNG_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_Gred_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN_Sem2.Fill(DSCocuricullum_KOD_SUKAN_Sem2)
                                            SQCocuricullum_KOD_KELAB_Sem2.Fill(DSCocuricullum_KOD_KELAB_Sem2)
                                            SQCocuricullum_KOD_UNIFORM_Sem2.Fill(DSCocuricullum_KOD_UNIFORM_Sem2)
                                            SQCocuricullum_NAMA_SUKAN_Sem2.Fill(DSCocuricullum_NAMA_SUKAN_Sem2)
                                            SQCocuricullum_NAMA_KELAB_Sem2.Fill(DSCocuricullum_NAMA_KELAB_Sem2)
                                            SQCocuricullum_NAMA_UNIFORM_Sem2.Fill(DSCocuricullum_NAMA_UNIFORM_Sem2)
                                        Catch ex As Exception

                                        End Try

                                    ElseIf j_examName + 1 = 4 Or j_examName + 1 = 8 Or j_examName + 1 = 12 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_PNG_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_Gred_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "    select koko_kolejpermata.NAMA from koko_pelajar
                                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                      where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN_Sem2.Fill(DSCocuricullum_KOD_SUKAN_Sem2)
                                            SQCocuricullum_KOD_KELAB_Sem2.Fill(DSCocuricullum_KOD_KELAB_Sem2)
                                            SQCocuricullum_KOD_UNIFORM_Sem2.Fill(DSCocuricullum_KOD_UNIFORM_Sem2)
                                            SQCocuricullum_NAMA_SUKAN_Sem2.Fill(DSCocuricullum_NAMA_SUKAN_Sem2)
                                            SQCocuricullum_NAMA_KELAB_Sem2.Fill(DSCocuricullum_NAMA_KELAB_Sem2)
                                            SQCocuricullum_NAMA_UNIFORM_Sem2.Fill(DSCocuricullum_NAMA_UNIFORM_Sem2)
                                        Catch ex As Exception

                                        End Try

                                    End If
                                End If

                                Test.AppendLine("        <tr style='font-size:9px;'>
                                                            <td><b> Kokurikulum (" & cocuricullum_value_Sem2 & "%) </b></td>
                                                            <td><b>")


                                If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                    ''kokorikulum kod sukan
                                    For Each row As DataRow In DSCocuricullum_KOD_SUKAN_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_KOD_SUKAN_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next
                                    ''kokorikulum kod kelab
                                    For Each row As DataRow In DSCocuricullum_KOD_KELAB_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_KOD_KELAB_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next
                                    ''kokorikulum kod uniform
                                    For Each row As DataRow In DSCocuricullum_KOD_UNIFORM_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("PKS317 <br />")

                                ElseIf Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "off" Then
                                    Test.Append("<br />")
                                End If

                                Test.AppendLine("           </b></td>
                                                            <td>")

                                If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                    ''kokorikulum nama skan
                                    For Each row As DataRow In DSCocuricullum_NAMA_SUKAN_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next
                                    ''kokorikulum nama kelab
                                    For Each row As DataRow In DSCocuricullum_NAMA_KELAB_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_NAMA_KELAB_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next
                                    ''kokorikulum nama uniform
                                    For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("Renang <br />")

                                ElseIf Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Then
                                    Test.Append("<br />")
                                End If

                                If DSCocurricullum_PNG_Sem2.Length > 0 Then
                                    Test.AppendLine("       </td>
                                                            <td style='text-align:center'><b> " & DSCocurricullum_Gred_Sem2 & " </b></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG_Sem2) & " </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG_Sem2) & " </td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("       </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                End If

                                ''print Portfolio
                                If Confirm_Portfolio = "ON" Or Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
                                    tmpsql_Portfolio_GRED = "   SELECT grade FROM [ExamSlip_Portfolio] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_GRED_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_GRED)

                                    tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_PNG_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_PNG)

                                    tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_KOD_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_KOD)

                                End If

                                If DSPortfolio_PNG_Sem2.Length > 0 Then
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSPortfolio_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG_Sem2) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG_Sem2) & " </td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                End If

                                ''print research 
                                If Confirm_Research = "ON" Or Confirm_Research = "On" Or Confirm_Research = "on" Then
                                    tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_GRED_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_Gred)

                                    tmpsql_Penyelidikan_PNG = " SELECT gpa FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_PNG_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_PNG)

                                    tmpsql_Penyelidikan_KOD = " SELECT subject_code FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_KOD_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_KOD)
                                End If

                                If DSResearch_PNG_Sem2.Length > 0 Then
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSResearch_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG_Sem2) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG_Sem2) & " </td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                End If

                                ''print self development
                                If Confirm_Self = "ON" Or Confirm_Self = "On" Or Confirm_Self = "on" Then
                                    Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & get_ExamYearSem1 & "' "
                                    Dim getLevel As String = oCommon.getFieldValue(level)

                                    If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED_Sem2 = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG_Sem2 = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD_Sem2 = oCommon.getFieldValue(tmpsql_SD_KOD)

                                    ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED_Sem2 = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG_Sem2 = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD_Sem2 = oCommon.getFieldValue(tmpsql_SD_KOD)
                                    End If
                                End If

                                If DSSelfdevelopment_PNG_Sem2.Length > 0 Then
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value_Sem2 & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD_Sem2 & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG_Sem2) & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG_Sem2) & " </td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value_Sem2 & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD_Sem2 & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  Sedang Maju  </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                End If

                                ''Print PNG & PNGK 
                                Dim check_png_exist_data_Sem2 As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_png_data_Sem2 As String = oCommon.getFieldValue(check_png_exist_data_Sem2)

                                Dim check_pngs_exist_data_Sem2 As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_pngs_data_Sem2 As String = oCommon.getFieldValue(check_pngs_exist_data_Sem2)

                                If exist_png_data_Sem2 = "" Then
                                    exist_png_data_Sem2 = "0"
                                End If

                                If exist_pngs_data_Sem2 = "" Then
                                    exist_pngs_data_Sem2 = "0"
                                End If

                                Dim png_dec_Sem2 As Decimal = Decimal.Parse(exist_png_data_Sem2)
                                Dim pngs_dec_Sem2 As Decimal = Decimal.Parse(exist_pngs_data_Sem2)

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> PNG </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & png_dec_Sem2 & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> PNGK </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & pngs_dec_Sem2 & " </b></td>
                                                        </tr>")
                                Test.AppendLine("   </table>")

                                Dim find_printStatus As String = "Select Value from setting where idx = 'Examination' and Type = 'Print Date Status'"
                                Dim get_printStatus As String = oCommon.getFieldValue(find_printStatus)

                                Dim find_PrintDateJSC As String = "Select Value from setting where idx = 'Examination' and Type = 'Printing Date'"
                                Dim get_PrintDateJSC As String = oCommon.getFieldValue(find_PrintDateJSC)

                                If get_printStatus = "On" And get_printStatus = "ON" And get_printStatus = "on" Then

                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:50px; left:0px; font-family:Century Gothic;'>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:45%'></td>
                                                            <td style='width:35%;></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:45%'></td>
                                                            <td style='width:35%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%; text-align:left; color:red; font-size:9px'> No Rujukan : </td>    
                                                            <td style='width:45%; text-align:left; font-size:9px'> Tarikh Cetakan : </td>
                                                            <td style='width:35%'></td>
                                                        </tr>
                                                        <tr >
                                                            <td style='width:20%; text-align:left; color:red; font-size:10px'><b> " & get_StudentID & " </b></td>    
                                                            <td style='width:45%; text-align:left; font-size:9px'><b> " & get_PrintDateJSC & " <b></td>
                                                            <td style='width:35%'></td>
                                                        </tr>                                                        
                                                    </table>")

                                Else

                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:20px; left:0px; font-family:Century Gothic;'>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%; text-align:left; color:red; font-size:9px'> No Rujukan : </td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr >
                                                            <td style='width:20%; text-align:left; color:red; font-size:10px'><b> " & get_StudentID & " </b></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>                                                        
                                                    </table>")

                                End If

                                If j_examName + 1 = 12 Then
                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:20px; left:0px; font-family:Century Gothic;'>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%;  border-top:1px dotted black'></td>
                                                                <td style='width:5%'></td>
                                                            </tr> 
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Prof. Madya Dr Rorlinda binti Yusof </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Pengarah, </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Pusat GENIUS@Pintar Negara </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Universiti Kebangsaan Malaysia </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                        </table>")
                                End If

                                Test.AppendLine("<div style='width:98%; position:absolute; bottom:0px; left:0px; font-family:Century Gothic;'> <p style='text-align:right; font-size:9px'>  " & countPage & " of 6 </p></div>")

                                Test.AppendLine("</div>")

                                countPage = countPage + 1
                            Next


                        End If
                    End If
                Next

            ElseIf ddl_TranscriptType.SelectedValue = "High School" Then

                For i = 0 To TranscriptRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(TranscriptRespondent.Rows(i).Cells(5).FindControl("chkSelectTranscript"), CheckBox)

                    Dim j_examName As Integer = 0
                    Dim countPage As Integer = 0

                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = TranscriptRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''Get Student Name
                            Dim select_StudentName As String = "select UPPER(student_Name) from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentName As String = oCommon.getFieldValue(select_StudentName)

                            ''Get Student ID
                            Dim select_StudentID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue(select_StudentID)

                            ''Get Student MYKAD
                            Dim select_StudentMYKAD As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentMYKAD As String = oCommon.getFieldValue(select_StudentMYKAD)

                            '''Get Student Start Date Year
                            'Dim select_StudentSDY As String = "select year from student_level where std_ID = '" & strKey & "' and student_Level = 'Level 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDY As String = oCommon.getFieldValue(select_StudentSDY)

                            '''Get Student Start Date Month
                            'Dim select_StudentSDM As String = "select month from student_level where std_ID = '" & strKey & "' and student_Level = 'Level 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDM As String = oCommon.getFieldValue(select_StudentSDM)

                            '''Get Student Start Date Day
                            'Dim select_StudentSDD As String = "select day from student_level where std_ID = '" & strKey & "' and student_Level = 'Level 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDD As String = oCommon.getFieldValue(select_StudentSDD)

                            ''Get Student First Date Year
                            Dim find_StudentFDM As String = "Select Value from Setting where Idx = 'Examination' and Type = 'Senior Entry Date'"
                            Dim get_StudentFDM As String = oCommon.getFieldValue(find_StudentFDM)

                            ''Get Student Last Date Year
                            Dim find_StudentLDM As String = "Select Value from Setting where Idx = 'Examination' and Type = 'Senior Term Ending'"
                            Dim get_StudentLDM As String = oCommon.getFieldValue(find_StudentLDM)

                            Dim EndMonth As String = System.DateTime.Now.Month

                            If EndMonth = "1" Then
                                EndMonth = "Januari"
                            ElseIf EndMonth = "2" Then
                                EndMonth = "Februari"
                            ElseIf EndMonth = "3" Then
                                EndMonth = "Mac"
                            ElseIf EndMonth = "4" Then
                                EndMonth = "April"
                            ElseIf EndMonth = "5" Then
                                EndMonth = "Mei"
                            ElseIf EndMonth = "6" Then
                                EndMonth = "Jun"
                            ElseIf EndMonth = "7" Then
                                EndMonth = "Julai"
                            ElseIf EndMonth = "8" Then
                                EndMonth = "Ogos"
                            ElseIf EndMonth = "9" Then
                                EndMonth = "September"
                            ElseIf EndMonth = "10" Then
                                EndMonth = "Oktober"
                            ElseIf EndMonth = "11" Then
                                EndMonth = "November"
                            ElseIf EndMonth = "12" Then
                                EndMonth = "Disember"
                            End If

                            countPage = 1

                            For j_examName = 1 To 6 Step j_examName + 2

                                ''getYear Exam 1,3,5
                                Dim select_ExamYearSem1 As String = "Select year from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and student_type = 'TAHAP'"
                                Dim get_ExamYearSem1 As String = oCommon.getFieldValue(select_ExamYearSem1)

                                ''getYear Exam 2,4,6
                                Dim select_ExamYearSem2 As String = "Select year from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and student_type = 'TAHAP'"
                                Dim get_ExamYearSem2 As String = oCommon.getFieldValue(select_ExamYearSem2)

                                If get_ExamYearSem1 = "" Then
                                    get_ExamYearSem1 = get_ExamYearSem2
                                End If

                                If get_ExamYearSem2 = "" Then
                                    get_ExamYearSem2 = get_ExamYearSem1
                                End If

                                Dim get_ExamRename As String = ""
                                Dim get_SemesterName As Integer = 0

                                If j_examName = 1 Then
                                    get_ExamRename = "1 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 3 Then
                                    get_ExamRename = "2 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 5 Then
                                    get_ExamRename = "3 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 7 Then
                                    get_ExamRename = "4 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 9 Then
                                    get_ExamRename = "5 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 11 Then
                                    get_ExamRename = "6 SEMESTER "
                                    get_SemesterName = 1
                                End If

                                Test.AppendLine("<div style='margin: 0; page-break-after:always; padding-top:0px; margin-top:0px;height: 100%; display: block; position: relative;'>")

                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic; padding-top:0px; margin-top:0px; border-collapse: collapse;'>
                                                        <tr style='font-size:14px;text-align:center; width:100%'>
                                                            <td colspan='5'><b> TRANSKRIP RASMI KOLEJ GENIUS@Pintar </b></td>
                                                        </tr>
                                                        <tr style='font-size:14px;text-align:center; width:100%;'>
                                                            <td colspan='5'><b> UNIVERSITI KEBANGSAAN MALAYSIA </b></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr style='text-align:left'>
                                                            <td rowspan='8' style='with:40%; padding:0px; margin:0px;height:60px; width:300px;'></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr>
    	                                                    <td colspan='2' style='with:40%; font-size:9px;'>Name :</td>
                                                            <td style='with:20%; font-size:9px; text-align:right'>Tarikh Kemasukan :</td>
                                                        </tr>
                                                        <tr>
    	                                                    <td colspan='2' style='with:40%; font-size:12px;'><b>" & get_StudentName.ToUpper & "</b></td>
                                                            <td style='with:20%; font-size:10px; ; text-align:right'><b>" & get_StudentFDM & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr >
    	                                                    <td style='with:25%; font-size:9px; '>No. Kad Matrik :</td>
                                                            <td style='with:25%; font-size:9px; '>No. Kad Pengenalan :</td>
                                                            <td style='with:10%; font-size:9px; text-align:right'>Tarikh Tamat Pengajian :</td>
                                                        </tr>
                                                        <tr>
    	                                                    <td style='with:25%; font-size:10px; '><b>" & get_StudentID & "</b></td>
                                                            <td style='with:25%; font-size:10px; '><b>" & get_StudentMYKAD & "</b></td>
                                                            <td style='with:10%; font-size:10px; text-align:right'><b>" & get_StudentLDM & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                            
                                                    <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:15px'>
                                                        <tr>
                                                            <td colspan='5' style='border-top:0.5px solid black; border-bottom:0.5px solid black; background-color:lightgray; font-size:9.5px; text-align:center'><b> PENTAKSIRAN " & get_ExamRename & get_SemesterName & ", TAHUN AKADEMIK " & get_ExamYearSem1 & " </b></td>
                                                        </tr>
                                                    </table>")

                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''' SEMESTER 1 - SEMESTER 1 - SEMESTER 1 - SEMESTER 1 - SEMESTER 1 '''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                'get Portfolio percentage on / off
                                check_portfolio_percen = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

                                ''get cocuricullum percentage on / off
                                check_cocuricullum_percen = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

                                ''get research percentage on / off
                                check_research_percen = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Research = oCommon.getFieldValue(check_research_percen)

                                ''get self development percentage on / off
                                check_self_percen = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Self = oCommon.getFieldValue(check_self_percen)

                                'get englih literture on / off
                                check_Eng_Literature = "select Value from setting where Type = 'English Literature'"
                                Confirm_Eng_Literature = oCommon.getFieldValue(check_Eng_Literature)


                                ''print subject name 
                                tmpSQL_Nama = " SELECT subject_NameBM FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                ''print subject code
                                tmpSQL_Kod = "  SELECT subject_code FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                ''print subject grade
                                tmpSQL_Gred = " SELECT grade FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

                                ''print subject png
                                tmpSQL_PNG = "  SELECT gpa FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

                                ''print subject credit hour
                                tmpSQL_Hour = " SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

                                ''print subject credit hour
                                tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)

                                ''print total credit hour for subject taken
                                tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

                                ''print total PNG x credit hour for subject taken
                                tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Total As String = oCommon.getFieldValue(tmpSQL)

                                ''print academic percentage
                                tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

                                Dim DS_Nama As New DataTable
                                Dim DS_Kod As New DataTable
                                Dim DS_Gred As New DataTable
                                Dim DS_PNG As New DataTable
                                Dim DS_Hour As New DataTable
                                Dim DS_Total As New DataTable

                                Try
                                    SQA.Fill(DS_Nama)
                                    SQACODE.Fill(DS_Kod)
                                    SQAPNG.Fill(DS_PNG)
                                    SQAGRADE.Fill(DS_Gred)
                                    SQAHOUR.Fill(DS_Hour)
                                    SQATOTAL.Fill(DS_Total)
                                Catch ex As Exception
                                End Try

                                Dim DSSelfdevelopment_GRED As String = ""
                                Dim DSSelfdevelopment_PNG As String = ""
                                Dim DSSelfdevelopment_KOD As String = ""

                                Dim DSEnglish_literature_GRED As New DataTable
                                Dim DSEnglish_literature_PNG As New DataTable
                                Dim DSEnglish_literature_KOD As New DataTable
                                Dim DSEnglish_literature_HOUR As New DataTable
                                Dim DSEnglish_literature_TOTAL As New DataTable

                                Dim DSResearch_GRED As String = ""
                                Dim DSResearch_PNG As String = ""
                                Dim DSResearch_KOD As String = ""

                                Dim DSPortfolio_GRED As String = ""
                                Dim DSPortfolio_PNG As String = ""
                                Dim DSPortfolio_KOD As String = ""

                                Dim DSCocuricullum_KOD_SUKAN As New DataTable
                                Dim DSCocuricullum_KOD_UNIFORM As New DataTable
                                Dim DSCocuricullum_KOD_KELAB As New DataTable
                                Dim DSCocuricullum_NAMA_SUKAN As New DataTable
                                Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
                                Dim DSCocuricullum_NAMA_KELAB As New DataTable
                                Dim DSCocurricullum_Gred As String = ""
                                Dim DSCocurricullum_PNG As String = ""

                                Dim total_Credit_EL As String = "0"
                                Dim total_Total_EL As String = "0"

                                tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim research_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim sd_value As String = oCommon.getFieldValue(tmpSQL)


                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic;border-collapse: collapse; margin-top:5px'>
                                                        <tr style='font-size:9px;background-color:lightgray; border-top:0.5px solid black; border-bottom:0.5px solid black;'>
                                                            <td style='width:20%;'><b> Komponent </b></td>
                                                            <td style='width:10%;'><b> Kod Kursus </b></td>
                                                            <td style='width:35%;'><b> Kursus </b></td>
                                                            <td style='width:5%; text-align:center'><b> Gred </b></td>
                                                            <td style='width:5%; text-align:center'><b> PNG </b></td>
                                                            <td style='width:10%; text-align:center'><b> Jam Kredit </b></td>
                                                            <td style='width:15%; text-align:center'><b> PNG x Jam Kredit </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td rowspan='2'><b> Akademik (" & academic_value & "%) </b></td>
                                                            <td ><b>")

                                If get_ExamYearSem1 = "2020" Then
                                    Dim get_Semester As String = ""
                                    Dim get_Level As String = ""

                                    If j_examName = "1" Or j_examName = "5" Then
                                        get_Semester = "Sem 1"
                                    ElseIf j_examName = "3" Then
                                        get_Semester = "Sem 2"
                                    End If

                                    If j_examName = "1" Or j_examName = "3" Then
                                        get_Level = "Level 1"
                                    ElseIf j_examName = "5" Then
                                        get_Level = "Level 2"
                                    End If

                                    ''print subject name for Exam 1, 3, 5
                                    tmpSQL_Nama = " Select subject_NameBM from subject_info left join course on subject_info.subject_ID = course.subject_ID 
                                                    where course.year = '" & get_ExamYearSem1 & "' and subject_info.subject_year = '" & get_ExamYearSem1 & "' and subject_info.course_Name <> 'Portfolio' and subject_info.course_Name <> 'Penyelidikan' and subject_info.course_Name <> 'Jati Diri'
                                                    and subject_info.subject_StudentYear = '" & get_Level & "' and subject_info.subject_sem = '" & get_Semester & "' and course.std_ID = '" & strKey & "'
                                                    order by course_Name, subject_NameBM ASC"
                                    Dim SQA_2020 As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                    ''print subject code for Exam 1, 3, 5
                                    tmpSQL_Kod = "  Select subject_code from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                                    where course.year = '" & get_ExamYearSem1 & "' and subject_info.subject_year = '" & get_ExamYearSem1 & "' and subject_info.course_Name <> 'Portfolio' and subject_info.course_Name <> 'Penyelidikan' and subject_info.course_Name <> 'Jati Diri'
                                                    and subject_info.subject_StudentYear = '" & get_Level & "' and subject_info.subject_sem = '" & get_Semester & "' and course.std_ID = '" & strKey & "'
                                                    order by course_Name, subject_NameBM ASC"
                                    Dim SQACODE_2020 As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                    Dim DS_Nama_2020 As New DataTable
                                    Dim DS_Kod_2020 As New DataTable

                                    Try
                                        SQA_2020.Fill(DS_Nama_2020)
                                        SQACODE_2020.Fill(DS_Kod_2020)
                                    Catch ex As Exception
                                    End Try

                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod_2020.Rows
                                        For Each column As DataColumn In DS_Kod_2020.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td >")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama_2020.Rows
                                        For Each column As DataColumn In DS_Nama_2020.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next


                                    Test.Append("           </td>
                                                            <td ></td>
                                                            <td ></td>
                                                            <td style=' text-align:center'> Sedang Maju </td>
                                                            <td ></td>
                                                        </tr>")
                                Else
                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod.Rows
                                        For Each column As DataColumn In DS_Kod.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td >")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama.Rows
                                        For Each column As DataColumn In DS_Nama.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'><b>")

                                    ''(column course grade)
                                    For Each row As DataRow In DS_Gred.Rows
                                        For Each column As DataColumn In DS_Gred.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td style='text-align:center;'>")

                                    ''(column course gpa )
                                    For Each row As DataRow In DS_PNG.Rows
                                        For Each column As DataColumn In DS_PNG.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course credit hour)
                                    For Each row As DataRow In DS_Hour.Rows
                                        For Each column As DataColumn In DS_Hour.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course total)
                                    For Each row As DataRow In DS_Total.Rows
                                        For Each column As DataColumn In DS_Total.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.AppendLine("           </td>
                                                        </tr>")
                                End If

                                ''print english literature
                                If Confirm_Eng_Literature = "On" Then

                                    Dim SQEnglish_Literature_GRED As String = ""
                                    Dim SQEnglish_Literature_PNG As String = ""
                                    Dim SQEnglish_Literature_KOD As String = ""
                                    Dim SQEnglish_Literature_HOUR As String = ""
                                    Dim SQEnglish_Literature_TOTAL As String = ""

                                    tmpsql_EL_GRED = "  SELECT grade FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_GRED = oCommon.getFieldValue(tmpsql_EL_GRED)

                                    tmpsql_EL_PNG = "   SELECT gpa FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_PNG = oCommon.getFieldValue(tmpsql_EL_PNG)

                                    tmpsql_EL_KOD = "   SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_KOD = oCommon.getFieldValue(tmpsql_EL_KOD)

                                    tmpsql_EL_HOUR = "  SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_HOUR = oCommon.getFieldValue(tmpsql_EL_HOUR)

                                    tmpsql_EL_TOTAL = " SELECT total FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_TOTAL = oCommon.getFieldValue(tmpsql_EL_TOTAL)

                                    If get_ExamYearSem1 = "2020" Then

                                        Test.Append("   <tr style='font-size:9px;'>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    Else

                                        If SQEnglish_Literature_GRED.Length > 0 Then
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                                <td style='border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_KOD & " </b></td>
                                                                <td style='border-bottom:0.5px solid black'> AP English Literature and Composition </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_GRED & " </b></td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_PNG & " </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_HOUR & " </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_TOTAL & " </td>
                                                            </tr>")

                                            total_Credit_EL = SQEnglish_Literature_HOUR
                                            total_Total_EL = SQEnglish_Literature_TOTAL
                                        Else
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                                    <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                                </tr>")
                                        End If

                                    End If

                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                        </tr>")
                                End If

                                If total_Credit = "" Then
                                    total_Credit = "0"
                                End If

                                If total_Credit_EL = "" Then
                                    total_Credit_EL = "0"
                                End If

                                If total_Total = "" Then
                                    total_Total = "0"
                                End If

                                If total_Total_EL = "" Then
                                    total_Total_EL = "0"
                                End If

                                ''Calculatr Total Credit Hour , PNG x Credit Hour & PNG Academic
                                Dim Number1 As Double = Double.Parse(total_Credit)
                                Dim Number2 As Double = Double.Parse(total_Credit_EL)
                                Dim Number3 As Double = Double.Parse(total_Total)
                                Dim Number4 As Double = Double.Parse(total_Total_EL)

                                Dim total_Hour As Double = Number1 + Number2
                                Dim final_Total As Double = Number3 + Number4

                                Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

                                If total_Hour = 0 And final_Total = 0 Then
                                    PNG_Akademik = 0
                                End If

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right'><b> Jumlah </b></td>
                                                            <td style='text-align:center'><b> " & total_Hour & " </b></td>
                                                            <td style='text-align:center'><b> " & final_Total & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right; border-bottom:0.5px solid black'><b> PNG Akademik </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & PNG_Akademik & " </b></td>
                                                        </tr> ")

                                '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                                If Confirm_Cocuricullum = "oN" Or Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then

                                    Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                    Dim getStudent As String = oCommon.getFieldValue(studentData)

                                    If j_examName = 2 Or j_examName = 6 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_PNG = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_Gred = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                            SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                            SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                            SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                            SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                            SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                        Catch ex As Exception

                                        End Try

                                    ElseIf j_examName = 4 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "    select koko_kolejpermata.NAMA from koko_pelajar
                                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                      where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                            SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                            SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                            SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                            SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                            SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                        Catch ex As Exception

                                        End Try

                                    End If
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("       <tr style='font-size:9px;'>
                                                             <td><b> Kokurikulum (" & cocuricullum_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td'></td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Kokurikulum (" & cocuricullum_value & "%) </b></td>
                                                            <td><b>")


                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum kod sukan
                                        For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod kelab
                                        For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod uniform
                                        For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("PKS317 <br />")

                                    ElseIf Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Or Confirm_Cocuricullum = "OFF" Then
                                        Test.Append("<br />")
                                    End If

                                    Test.AppendLine("           </b></td>
                                                            <td>")

                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum nama skan
                                        For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama kelab
                                        For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama uniform
                                        For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("Renang <br />")

                                    ElseIf Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Then
                                        Test.Append("<br />")
                                    End If

                                    If DSCocurricullum_PNG.Length > 0 Then
                                        Test.AppendLine("       </td>
                                                                <td style='text-align:center'><b> " & DSCocurricullum_Gred & " </b></td>
                                                                <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG) & " </td>
                                                                <td style='text-align:center'></td>
                                                                <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG) & " </td>
                                                            </tr>")
                                    Else
                                        Test.AppendLine("       </td>
                                                                <td style='text-align:center'></td>
                                                                <td style='text-align:center'></td>
                                                                <td style='text-align:center'> Sedang Maju </td>
                                                                <td style='text-align:center'></td>
                                                            </tr>")
                                    End If
                                End If

                                ''print Portfolio
                                If Confirm_Portfolio = "ON" Or Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
                                    tmpsql_Portfolio_GRED = "   SELECT grade FROM [ExamSlip_Portfolio] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_GRED = oCommon.getFieldValue(tmpsql_Portfolio_GRED)

                                    tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_PNG = oCommon.getFieldValue(tmpsql_Portfolio_PNG)

                                    tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_KOD = oCommon.getFieldValue(tmpsql_Portfolio_KOD)

                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("       <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    If DSPortfolio_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSPortfolio_GRED & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print research 
                                If Confirm_Research = "ON" Or Confirm_Research = "On" Or Confirm_Research = "on" Then
                                    tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_GRED = oCommon.getFieldValue(tmpsql_Penyelidikan_Gred)

                                    tmpsql_Penyelidikan_PNG = " SELECT gpa FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_PNG = oCommon.getFieldValue(tmpsql_Penyelidikan_PNG)

                                    tmpsql_Penyelidikan_KOD = " SELECT subject_code FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_KOD = oCommon.getFieldValue(tmpsql_Penyelidikan_KOD)
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("       <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    If DSResearch_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSResearch_GRED & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print self development
                                If Confirm_Self = "ON" Or Confirm_Self = "On" Or Confirm_Self = "on" Then
                                    Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & get_ExamYearSem1 & "' "
                                    Dim getLevel As String = oCommon.getFieldValue(level)

                                    If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD = oCommon.getFieldValue(tmpsql_SD_KOD)

                                    ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD = oCommon.getFieldValue(tmpsql_SD_KOD)
                                    End If
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("       <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  Sedang Maju  </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                Else
                                    If DSSelfdevelopment_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_GRED & " </b></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG) & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  Sedang Maju  </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''Print PNG & PNGK 
                                Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

                                Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

                                If exist_png_data = "" Then
                                    exist_png_data = "0"
                                End If

                                If exist_pngs_data = "" Then
                                    exist_pngs_data = "0"
                                End If

                                Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
                                Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> PNG </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & png_dec & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> PNGK </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & pngs_dec & " </b></td>
                                                        </tr>")
                                Test.AppendLine("   </table>")


                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''' SEMESTER 2 - SEMESTER 2 - SEMESTER 2 - SEMESTER 2 - SEMESTER 2 '''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                If j_examName + 1 <= 6 Then



                                    'get Portfolio percentage on / off
                                    check_portfolio_percen = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

                                    ''get cocuricullum percentage on / off
                                    check_cocuricullum_percen = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

                                    ''get research percentage on / off
                                    check_research_percen = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Confirm_Research = oCommon.getFieldValue(check_research_percen)

                                    ''get self development percentage on / off
                                    check_self_percen = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Confirm_Self = oCommon.getFieldValue(check_self_percen)


                                    ''print subject name 
                                    tmpSQL_Nama = " SELECT subject_NameBM FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQA_Sem2 As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                    ''print subject code
                                    tmpSQL_Kod = "  SELECT subject_code FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQACODE_Sem2 As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                    ''print subject grade
                                    tmpSQL_Gred = " SELECT grade FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQAGRADE_Sem2 As New SqlDataAdapter(tmpSQL_Gred, strConn)

                                    ''print subject png
                                    tmpSQL_PNG = "  SELECT gpa FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQAPNG_Sem2 As New SqlDataAdapter(tmpSQL_PNG, strConn)

                                    ''print subject credit hour
                                    tmpSQL_Hour = " SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQAHOUR_Sem2 As New SqlDataAdapter(tmpSQL_Hour, strConn)

                                    ''print subject credit hour
                                    tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQATOTAL_Sem2 As New SqlDataAdapter(tmpSQL_Total, strConn)

                                    ''print total credit hour for subject taken
                                    tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    Dim total_Credit_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    ''print total PNG x credit hour for subject taken
                                    tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    Dim total_Total_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    ''print academic percentage
                                    tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim academic_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    Dim DS_Nama_Sem2 As New DataTable
                                    Dim DS_Kod_Sem2 As New DataTable
                                    Dim DS_Gred_Sem2 As New DataTable
                                    Dim DS_PNG_Sem2 As New DataTable
                                    Dim DS_Hour_Sem2 As New DataTable
                                    Dim DS_Total_Sem2 As New DataTable

                                    Try
                                        SQA_Sem2.Fill(DS_Nama_Sem2)
                                        SQACODE_Sem2.Fill(DS_Kod_Sem2)
                                        SQAPNG_Sem2.Fill(DS_PNG_Sem2)
                                        SQAGRADE_Sem2.Fill(DS_Gred_Sem2)
                                        SQAHOUR_Sem2.Fill(DS_Hour_Sem2)
                                        SQATOTAL_Sem2.Fill(DS_Total_Sem2)
                                    Catch ex As Exception
                                    End Try

                                    Dim DSSelfdevelopment_GRED_Sem2 As String = ""
                                    Dim DSSelfdevelopment_PNG_Sem2 As String = ""
                                    Dim DSSelfdevelopment_KOD_Sem2 As String = ""

                                    Dim DSEnglish_literature_GRED_Sem2 As New DataTable
                                    Dim DSEnglish_literature_PNG_Sem2 As New DataTable
                                    Dim DSEnglish_literature_KOD_Sem2 As New DataTable
                                    Dim DSEnglish_literature_HOUR_Sem2 As New DataTable
                                    Dim DSEnglish_literature_TOTAL_Sem2 As New DataTable

                                    Dim DSResearch_GRED_Sem2 As String = ""
                                    Dim DSResearch_PNG_Sem2 As String = ""
                                    Dim DSResearch_KOD_Sem2 As String = ""

                                    Dim DSPortfolio_GRED_Sem2 As String = ""
                                    Dim DSPortfolio_PNG_Sem2 As String = ""
                                    Dim DSPortfolio_KOD_Sem2 As String = ""

                                    Dim DSCocuricullum_KOD_SUKAN_Sem2 As New DataTable
                                    Dim DSCocuricullum_KOD_UNIFORM_Sem2 As New DataTable
                                    Dim DSCocuricullum_KOD_KELAB_Sem2 As New DataTable
                                    Dim DSCocuricullum_NAMA_SUKAN_Sem2 As New DataTable
                                    Dim DSCocuricullum_NAMA_UNIFORM_Sem2 As New DataTable
                                    Dim DSCocuricullum_NAMA_KELAB_Sem2 As New DataTable
                                    Dim DSCocurricullum_Gred_Sem2 As String = ""
                                    Dim DSCocurricullum_PNG_Sem2 As String = ""

                                    Dim total_Credit_EL_Sem2 As String = "0"
                                    Dim total_Total_EL_Sem2 As String = "0"

                                    tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim cocuricullum_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim portfolio_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim research_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim sd_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)


                                    Test.AppendLine("   <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:15px'>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='5' style='border-top:0.5px solid black; border-bottom:0.5px solid black; background-color:lightgray; font-size:9.5px; text-align:center'><b> PENTAKSIRAN " & get_ExamRename & get_SemesterName + 1 & ", TAHUN AKADEMIK " & get_ExamYearSem1 & " </b></td>
                                                        </tr>
                                                    </table>

                                                    <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:5px'>
                                                        <tr style='font-size:9px;background-color:lightgray'>
                                                            <td style='width:20%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Komponent </b></td>
                                                            <td style='width:10%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Kod Kursus </b></td>
                                                            <td style='width:35%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Kursus </b></td>
                                                            <td style='width:5%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> Gred </b></td>
                                                            <td style='width:5%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> PNG </b></td>
                                                            <td style='width:10%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> Jam Kredit </b></td>
                                                            <td style='width:15%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> PNG x Jam Kredit </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td rowspan='2'><b> Akademik (" & academic_value_Sem2 & "%) </b></td>
                                                            <td ><b>")

                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod_Sem2.Rows
                                        For Each column As DataColumn In DS_Kod_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td >")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama_Sem2.Rows
                                        For Each column As DataColumn In DS_Nama_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center; '><b>")

                                    ''(column course grade)
                                    For Each row As DataRow In DS_Gred_Sem2.Rows
                                        For Each column As DataColumn In DS_Gred_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td style='text-align:center;'>")

                                    ''(column course gpa )
                                    For Each row As DataRow In DS_PNG_Sem2.Rows
                                        For Each column As DataColumn In DS_PNG_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course credit hour)
                                    For Each row As DataRow In DS_Hour_Sem2.Rows
                                        For Each column As DataColumn In DS_Hour_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course total)
                                    For Each row As DataRow In DS_Total_Sem2.Rows
                                        For Each column As DataColumn In DS_Total_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.AppendLine("           </td>
                                                        </tr>")

                                    ''print english literature
                                    If Confirm_Eng_Literature = "On" Then

                                        Dim SQEnglish_Literature_GRED As String = ""
                                        Dim SQEnglish_Literature_PNG As String = ""
                                        Dim SQEnglish_Literature_KOD As String = ""
                                        Dim SQEnglish_Literature_HOUR As String = ""
                                        Dim SQEnglish_Literature_TOTAL As String = ""

                                        tmpsql_EL_GRED = "  SELECT grade FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_GRED = oCommon.getFieldValue(tmpsql_EL_GRED)

                                        tmpsql_EL_PNG = "   SELECT gpa FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_PNG = oCommon.getFieldValue(tmpsql_EL_PNG)

                                        tmpsql_EL_KOD = "   SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_KOD = oCommon.getFieldValue(tmpsql_EL_KOD)

                                        tmpsql_EL_HOUR = "  SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_HOUR = oCommon.getFieldValue(tmpsql_EL_HOUR)

                                        tmpsql_EL_TOTAL = " SELECT total FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_TOTAL = oCommon.getFieldValue(tmpsql_EL_TOTAL)

                                        If SQEnglish_Literature_GRED.Length > 0 Then
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td style='border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'> AP English Literature and Composition </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_GRED & " </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_PNG & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_HOUR & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_TOTAL & " </td>
                                                        </tr>")

                                            total_Credit_EL_Sem2 = SQEnglish_Literature_HOUR
                                            total_Total_EL_Sem2 = SQEnglish_Literature_TOTAL
                                        Else
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                                    <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                                </tr>")
                                        End If

                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                        </tr>")
                                    End If

                                    If total_Credit_Sem2 = "" Then
                                        total_Credit_Sem2 = "0"
                                    End If

                                    If total_Credit_EL_Sem2 = "" Then
                                        total_Credit_EL_Sem2 = "0"
                                    End If

                                    If total_Total_Sem2 = "" Then
                                        total_Total_Sem2 = "0"
                                    End If

                                    If total_Total_EL_Sem2 = "" Then
                                        total_Total_EL_Sem2 = "0"
                                    End If

                                    ''Calculatr Total Credit Hour , PNG x Credit Hour & PNG Academic
                                    Dim Number1_Sem2 As Double = Double.Parse(total_Credit_Sem2)
                                    Dim Number2_Sem2 As Double = Double.Parse(total_Credit_EL_Sem2)
                                    Dim Number3_Sem2 As Double = Double.Parse(total_Total_Sem2)
                                    Dim Number4_Sem2 As Double = Double.Parse(total_Total_EL_Sem2)

                                    Dim total_Hour_Sem2 As Double = Number1_Sem2 + Number2_Sem2
                                    Dim final_Total_Sem2 As Double = Number3_Sem2 + Number4_Sem2

                                    Dim PNG_Akademik_Sem2 As Double = Math.Round(final_Total_Sem2 / total_Hour_Sem2, 2)

                                    Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right'><b> Jumlah </b></td>
                                                            <td style='text-align:center'><b> " & total_Hour_Sem2 & " </b></td>
                                                            <td style='text-align:center'><b> " & final_Total_Sem2 & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right; border-bottom:0.5px solid black'><b> PNG Akademik </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & PNG_Akademik_Sem2 & " </b></td>
                                                        </tr> ")

                                    '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then

                                        Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                        Dim getStudent As String = oCommon.getFieldValue(studentData)

                                        If j_examName + 1 = 2 Or j_examName + 1 = 6 Then

                                            tmpsql_KOKO_PNG = " select koko_pelajar.PNGP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                            DSCocurricullum_PNG_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                            tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                            DSCocurricullum_Gred_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                            tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                            tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                            tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                            tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                            tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                            tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                            Dim SQCocuricullum_KOD_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                            Dim SQCocuricullum_KOD_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                            Dim SQCocuricullum_KOD_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                            Dim SQCocuricullum_NAMA_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                            Dim SQCocuricullum_NAMA_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                            Dim SQCocuricullum_NAMA_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                            Try
                                                SQCocuricullum_KOD_SUKAN_Sem2.Fill(DSCocuricullum_KOD_SUKAN_Sem2)
                                                SQCocuricullum_KOD_KELAB_Sem2.Fill(DSCocuricullum_KOD_KELAB_Sem2)
                                                SQCocuricullum_KOD_UNIFORM_Sem2.Fill(DSCocuricullum_KOD_UNIFORM_Sem2)
                                                SQCocuricullum_NAMA_SUKAN_Sem2.Fill(DSCocuricullum_NAMA_SUKAN_Sem2)
                                                SQCocuricullum_NAMA_KELAB_Sem2.Fill(DSCocuricullum_NAMA_KELAB_Sem2)
                                                SQCocuricullum_NAMA_UNIFORM_Sem2.Fill(DSCocuricullum_NAMA_UNIFORM_Sem2)
                                            Catch ex As Exception

                                            End Try

                                        ElseIf j_examName + 1 = 4 Then

                                            tmpsql_KOKO_PNG = " select koko_pelajar.PNGP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                            DSCocurricullum_PNG_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                            tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                            DSCocurricullum_Gred_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                            tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                            tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                            tmpsql_KOKO_NAMA_UNIFORM = "    select koko_kolejpermata.NAMA from koko_pelajar
                                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                      where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                            tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                            tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                            tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                            Dim SQCocuricullum_KOD_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                            Dim SQCocuricullum_KOD_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                            Dim SQCocuricullum_KOD_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                            Dim SQCocuricullum_NAMA_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                            Dim SQCocuricullum_NAMA_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                            Dim SQCocuricullum_NAMA_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                            Try
                                                SQCocuricullum_KOD_SUKAN_Sem2.Fill(DSCocuricullum_KOD_SUKAN_Sem2)
                                                SQCocuricullum_KOD_KELAB_Sem2.Fill(DSCocuricullum_KOD_KELAB_Sem2)
                                                SQCocuricullum_KOD_UNIFORM_Sem2.Fill(DSCocuricullum_KOD_UNIFORM_Sem2)
                                                SQCocuricullum_NAMA_SUKAN_Sem2.Fill(DSCocuricullum_NAMA_SUKAN_Sem2)
                                                SQCocuricullum_NAMA_KELAB_Sem2.Fill(DSCocuricullum_NAMA_KELAB_Sem2)
                                                SQCocuricullum_NAMA_UNIFORM_Sem2.Fill(DSCocuricullum_NAMA_UNIFORM_Sem2)
                                            Catch ex As Exception

                                            End Try

                                        End If
                                    End If

                                    Test.AppendLine("        <tr style='font-size:9px;'>
                                                            <td><b> Kokurikulum (" & cocuricullum_value_Sem2 & "%) </b></td>
                                                            <td><b>")


                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum kod sukan
                                        For Each row As DataRow In DSCocuricullum_KOD_SUKAN_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_SUKAN_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod kelab
                                        For Each row As DataRow In DSCocuricullum_KOD_KELAB_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_KELAB_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod uniform
                                        For Each row As DataRow In DSCocuricullum_KOD_UNIFORM_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("PKS317 <br />")

                                    ElseIf Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "off" Then
                                        Test.Append("<br />")
                                    End If

                                    Test.AppendLine("           </b></td>
                                                            <td>")

                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum nama skan
                                        For Each row As DataRow In DSCocuricullum_NAMA_SUKAN_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama kelab
                                        For Each row As DataRow In DSCocuricullum_NAMA_KELAB_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_KELAB_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama uniform
                                        For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("Renang <br />")

                                    ElseIf Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Then
                                        Test.Append("<br />")
                                    End If

                                    If DSCocurricullum_PNG_Sem2.Length > 0 Then
                                        Test.AppendLine("       </td>
                                                            <td style='text-align:center'><b> " & DSCocurricullum_Gred_Sem2 & " </b></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG_Sem2) & " </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG_Sem2) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("       </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If

                                    ''print Portfolio
                                    If Confirm_Portfolio = "ON" Or Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
                                        tmpsql_Portfolio_GRED = "   SELECT grade FROM [ExamSlip_Portfolio] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSPortfolio_GRED_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_GRED)

                                        tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSPortfolio_PNG_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_PNG)

                                        tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSPortfolio_KOD_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_KOD)

                                    End If

                                    If DSPortfolio_PNG_Sem2.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSPortfolio_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG_Sem2) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG_Sem2) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If

                                    ''print research 
                                    If Confirm_Research = "ON" Or Confirm_Research = "On" Or Confirm_Research = "on" Then
                                        tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSResearch_GRED_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_Gred)

                                        tmpsql_Penyelidikan_PNG = " SELECT gpa FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSResearch_PNG_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_PNG)

                                        tmpsql_Penyelidikan_KOD = " SELECT subject_code FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSResearch_KOD_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_KOD)
                                    End If

                                    If DSResearch_PNG_Sem2.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSResearch_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG_Sem2) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG_Sem2) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Penyelidikan (" & research_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> Sedang Maju </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If

                                    ''print self development
                                    If Confirm_Self = "ON" Or Confirm_Self = "On" Or Confirm_Self = "on" Then
                                        Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & get_ExamYearSem1 & "' "
                                        Dim getLevel As String = oCommon.getFieldValue(level)

                                        If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                            tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_GRED_Sem2 = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                            tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_PNG_Sem2 = oCommon.getFieldValue(tmpsql_SD_PNG)

                                            tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_KOD_Sem2 = oCommon.getFieldValue(tmpsql_SD_KOD)

                                        ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                            tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_GRED_Sem2 = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                            tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_PNG_Sem2 = oCommon.getFieldValue(tmpsql_SD_PNG)

                                            tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_KOD_Sem2 = oCommon.getFieldValue(tmpsql_SD_KOD)
                                        End If
                                    End If

                                    If DSSelfdevelopment_PNG_Sem2.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value_Sem2 & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD_Sem2 & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG_Sem2) & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG_Sem2) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Pembangunan Kendiri (" & sd_value_Sem2 & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD_Sem2 & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  Sedang Maju  </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    End If

                                    ''Print PNG & PNGK 
                                    Dim check_png_exist_data_Sem2 As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim exist_png_data_Sem2 As String = oCommon.getFieldValue(check_png_exist_data_Sem2)

                                    Dim check_pngs_exist_data_Sem2 As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim exist_pngs_data_Sem2 As String = oCommon.getFieldValue(check_pngs_exist_data_Sem2)

                                    If exist_png_data_Sem2 = "" Then
                                        exist_png_data_Sem2 = "0"
                                    End If

                                    If exist_pngs_data_Sem2 = "" Then
                                        exist_pngs_data_Sem2 = "0"
                                    End If

                                    Dim png_dec_Sem2 As Decimal = Decimal.Parse(exist_png_data_Sem2)
                                    Dim pngs_dec_Sem2 As Decimal = Decimal.Parse(exist_pngs_data_Sem2)

                                    Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> PNG </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & png_dec_Sem2 & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> PNGK </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & pngs_dec_Sem2 & " </b></td>
                                                        </tr>")
                                    Test.AppendLine("   </table>")

                                End If

                                Dim find_printStatus As String = "Select Value from setting where idx = 'Examination' and Type = 'Print Date Status'"
                                Dim get_printStatus As String = oCommon.getFieldValue(find_printStatus)

                                Dim find_PrintDateJSC As String = "Select Value from setting where idx = 'Examination' and Type = 'Printing Date'"
                                Dim get_PrintDateJSC As String = oCommon.getFieldValue(find_PrintDateJSC)

                                If get_printStatus = "On" And get_printStatus = "ON" And get_printStatus = "on" Then

                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:50px; left:0px; font-family:Century Gothic;'>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:45%'></td>
                                                            <td style='width:35%;></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:45%'></td>
                                                            <td style='width:35%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%; text-align:left; color:red; font-size:9px'> No Rujukan : </td>    
                                                            <td style='width:45%; text-align:left; font-size:9px'> Tarikh Cetakan : </td>
                                                            <td style='width:35%'></td>
                                                        </tr>
                                                        <tr >
                                                            <td style='width:20%; text-align:left; color:red; font-size:10px'><b> " & get_StudentID & " </b></td>    
                                                            <td style='width:45%; text-align:left; font-size:9px'><b> " & get_PrintDateJSC & " <b></td>
                                                            <td style='width:35%'></td>
                                                        </tr>                                                        
                                                    </table>")

                                Else

                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:20px; left:0px; font-family:Century Gothic;'>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%; text-align:left; color:red; font-size:9px'> No Rujukan : </td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr >
                                                            <td style='width:20%; text-align:left; color:red; font-size:10px'><b> " & get_StudentID & " </b></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>                                                        
                                                    </table>")

                                End If

                                If j_examName = 5 Then
                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:20px; left:0px; font-family:Century Gothic;'>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%;  border-top:1px dotted black'></td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                             <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Prof. Madya Dr Rorlinda binti Yusof </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Pengarah, </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Pusat GENIUS@Pintar Negara </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Universiti Kebangsaan Malaysia </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                        </table>")
                                End If

                                Test.AppendLine("<div style='width:98%; position:absolute; bottom:0px; left:0px; font-family:Century Gothic;'> <p style='text-align:right; font-size:9px'>  " & countPage & " of 3 </p></div>")

                                Test.AppendLine("</div>")

                                countPage = countPage + 1
                            Next


                        End If
                    End If
                Next

            End If

            Test.AppendLine("</div> </div>")
            Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataOfficialTranscriptBM'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

            'print
            Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())


        ElseIf Lang = "BI" Then

            Test.AppendLine("<div id='data' style='display:none; padding:0px; margin:0px;'>")
            Test.AppendLine("<div id='dataOfficialTranscriptBI' style='padding-top:0px; margin-top:0px; height: 100%; margin: 0px;'> ")

            If ddl_TranscriptType.SelectedValue = "Junior School" Then

                For i = 0 To TranscriptRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(TranscriptRespondent.Rows(i).Cells(5).FindControl("chkSelectTranscript"), CheckBox)

                    Dim j_examName As Integer = 0
                    Dim countPage As Integer = 0

                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = TranscriptRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''Get Student Name
                            Dim select_StudentName As String = "select  UPPER(student_Name) from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentName As String = oCommon.getFieldValue(select_StudentName)

                            ''Get Student ID
                            Dim select_StudentID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue(select_StudentID)

                            ''Get Student MYKAD
                            Dim select_StudentMYKAD As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentMYKAD As String = oCommon.getFieldValue(select_StudentMYKAD)

                            '''Get Student Start Date Year
                            'Dim select_StudentSDY As String = "select year from student_level where std_ID = '" & strKey & "' and student_Level = 'Foundation 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDY As String = oCommon.getFieldValue(select_StudentSDY)

                            '''Get Student Start Date Month
                            'Dim select_StudentSDM As String = "select month from student_level where std_ID = '" & strKey & "' and student_Level = 'Foundation 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDM As String = oCommon.getFieldValue(select_StudentSDM)

                            '''Get Student Start Date Day
                            'Dim select_StudentSDD As String = "select day from student_level where std_ID = '" & strKey & "' and student_Level = 'Foundation 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDD As String = oCommon.getFieldValue(select_StudentSDD)

                            ''Get Student First Date Year
                            Dim find_StudentFDM As String = "Select Value from Setting where Idx = 'Examination' and Type = 'Junior Entry Date'"
                            Dim get_StudentFDM As String = oCommon.getFieldValue(find_StudentFDM)

                            ''Get Student Last Date Year
                            Dim find_StudentLDM As String = "Select Value from Setting where Idx = 'Examination' and Type = 'Junior Term Ending'"
                            Dim get_StudentLDM As String = oCommon.getFieldValue(find_StudentLDM)

                            Dim EndMonth As String = System.DateTime.Now.Month

                            If EndMonth = "1" Then
                                EndMonth = "January"
                            ElseIf EndMonth = "2" Then
                                EndMonth = "February"
                            ElseIf EndMonth = "3" Then
                                EndMonth = "March"
                            ElseIf EndMonth = "4" Then
                                EndMonth = "April"
                            ElseIf EndMonth = "5" Then
                                EndMonth = "May"
                            ElseIf EndMonth = "6" Then
                                EndMonth = "June"
                            ElseIf EndMonth = "7" Then
                                EndMonth = "July"
                            ElseIf EndMonth = "8" Then
                                EndMonth = "August"
                            ElseIf EndMonth = "9" Then
                                EndMonth = "September"
                            ElseIf EndMonth = "10" Then
                                EndMonth = "October"
                            ElseIf EndMonth = "11" Then
                                EndMonth = "November"
                            ElseIf EndMonth = "12" Then
                                EndMonth = "December"
                            End If

                            countPage = 1

                            For j_examName = 1 To 12 Step j_examName + 2

                                ''getYear Exam 1,3,5,7,9,11
                                Dim select_ExamYearSem1 As String = "Select year from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and student_type = 'ASAS'"
                                Dim get_ExamYearSem1 As String = oCommon.getFieldValue(select_ExamYearSem1)

                                ''getYear Exam 2,4,6,8,10,12
                                Dim select_ExamYearSem2 As String = "Select year from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and student_type = 'ASAS'"
                                Dim get_ExamYearSem2 As String = oCommon.getFieldValue(select_ExamYearSem2)

                                If get_ExamYearSem1 = "" Then
                                    get_ExamYearSem1 = get_ExamYearSem2
                                End If

                                If get_ExamYearSem2 = "" Then
                                    get_ExamYearSem2 = get_ExamYearSem1
                                End If

                                Dim get_ExamRename As String = ""
                                Dim get_SemesterName As Integer = 0

                                If j_examName = 1 Then
                                    get_ExamRename = "1 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 3 Then
                                    get_ExamRename = "2 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 5 Then
                                    get_ExamRename = "3 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 7 Then
                                    get_ExamRename = "4 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 9 Then
                                    get_ExamRename = "5 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 11 Then
                                    get_ExamRename = "6 SEMESTER "
                                    get_SemesterName = 1
                                End If

                                Test.AppendLine("<div style='margin: 0; page-break-after:always; padding-top:0px; margin-top:0px;height: 100%; display: block; position: relative;'>")

                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic; padding-top:0px; margin-top:0px; border-collapse: collapse;'>
                                                        <tr style='font-size:14px;text-align:center; width:100%'>
                                                            <td colspan='5'><b> OFFICIAL TRANSCRIPT KOLEJ GENIUS@Pintar </b></td>
                                                        </tr>
                                                        <tr style='font-size:14px;text-align:center; width:100%;'>
                                                            <td colspan='5'><b> UNIVERSITI KEBANGSAAN MALAYSIA </b></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr style='text-align:left'>
                                                            <td rowspan='8' style='with:40%; padding:0px; margin:0px;height:60px; width:300px;'></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr>
    	                                                    <td colspan='2' style='with:40%; font-size:9px;'> Name :</td>
                                                            <td style='with:20%; font-size:9px; text-align:right'> Admission Date :</td>
                                                        </tr>
                                                        <tr>
    	                                                    <td colspan='2' style='with:40%; font-size:12px;'><b>" & get_StudentName.ToUpper & "</b></td>
                                                            <td style='with:20%; font-size:10px; ; text-align:right'><b>" & get_StudentFDM & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr >
    	                                                    <td style='with:25%; font-size:9px; '> Matrix Card No :</td>
                                                            <td style='with:25%; font-size:9px; '> Identification Card No :</td>
                                                            <td style='with:10%; font-size:9px; text-align:right'> Graduation Date :</td>
                                                        </tr>
                                                        <tr>
    	                                                    <td style='with:25%; font-size:10px; '><b>" & get_StudentID & "</b></td>
                                                            <td style='with:25%; font-size:10px; '><b>" & get_StudentMYKAD & "</b></td>
                                                            <td style='with:10%; font-size:10px; text-align:right'><b>" & get_StudentLDM & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                            
                                                    <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:15px'>
                                                        <tr>
                                                            <td colspan='5' style='border-top:0.5px solid black; border-bottom:0.5px solid black; background-color:lightgray; font-size:9.5px; text-align:center'><b> ASSESSMENT " & get_ExamRename & get_SemesterName & ", ACADEMIC YEAR " & get_ExamYearSem1 & " </b></td>
                                                        </tr>
                                                    </table>")

                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''' SEMESTER 1 - SEMESTER 1 - SEMESTER 1 - SEMESTER 1 - SEMESTER 1 '''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                'get Portfolio percentage on / off
                                check_portfolio_percen = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

                                ''get cocuricullum percentage on / off
                                check_cocuricullum_percen = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

                                ''get research percentage on / off
                                check_research_percen = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Research = oCommon.getFieldValue(check_research_percen)

                                ''get self development percentage on / off
                                check_self_percen = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Self = oCommon.getFieldValue(check_self_percen)

                                'get englih literture on / off
                                check_Eng_Literature = "select Value from setting where Type = 'English Literature'"
                                Confirm_Eng_Literature = oCommon.getFieldValue(check_Eng_Literature)

                                ''print subject name 
                                tmpSQL_Nama = " SELECT subject_Name FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                ''print subject code
                                tmpSQL_Kod = "  SELECT subject_code FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                ''print subject grade
                                tmpSQL_Gred = " SELECT grade FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

                                ''print subject png
                                tmpSQL_PNG = "  SELECT gpa FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

                                ''print subject credit hour
                                tmpSQL_Hour = " SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

                                ''print subject credit hour
                                tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)

                                ''print total credit hour for subject taken
                                tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

                                ''print total PNG x credit hour for subject taken
                                tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Total As String = oCommon.getFieldValue(tmpSQL)

                                ''print academic percentage
                                tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

                                Dim DS_Nama As New DataTable
                                Dim DS_Kod As New DataTable
                                Dim DS_Gred As New DataTable
                                Dim DS_PNG As New DataTable
                                Dim DS_Hour As New DataTable
                                Dim DS_Total As New DataTable

                                Try
                                    SQA.Fill(DS_Nama)
                                    SQACODE.Fill(DS_Kod)
                                    SQAPNG.Fill(DS_PNG)
                                    SQAGRADE.Fill(DS_Gred)
                                    SQAHOUR.Fill(DS_Hour)
                                    SQATOTAL.Fill(DS_Total)
                                Catch ex As Exception
                                End Try

                                Dim DSSelfdevelopment_GRED As String = ""
                                Dim DSSelfdevelopment_PNG As String = ""
                                Dim DSSelfdevelopment_KOD As String = ""

                                Dim DSEnglish_literature_GRED As New DataTable
                                Dim DSEnglish_literature_PNG As New DataTable
                                Dim DSEnglish_literature_KOD As New DataTable
                                Dim DSEnglish_literature_HOUR As New DataTable
                                Dim DSEnglish_literature_TOTAL As New DataTable

                                Dim DSResearch_GRED As String = ""
                                Dim DSResearch_PNG As String = ""
                                Dim DSResearch_KOD As String = ""

                                Dim DSPortfolio_GRED As String = ""
                                Dim DSPortfolio_PNG As String = ""
                                Dim DSPortfolio_KOD As String = ""

                                Dim DSCocuricullum_KOD_SUKAN As New DataTable
                                Dim DSCocuricullum_KOD_UNIFORM As New DataTable
                                Dim DSCocuricullum_KOD_KELAB As New DataTable
                                Dim DSCocuricullum_NAMA_SUKAN As New DataTable
                                Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
                                Dim DSCocuricullum_NAMA_KELAB As New DataTable
                                Dim DSCocurricullum_Gred As String = ""
                                Dim DSCocurricullum_PNG As String = ""

                                Dim total_Credit_EL As String = "0"
                                Dim total_Total_EL As String = "0"

                                tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim research_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim sd_value As String = oCommon.getFieldValue(tmpSQL)


                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic;border-collapse: collapse; margin-top:5px'>
                                                        <tr style='font-size:9px;background-color:lightgray; border-top:0.5px solid black; border-bottom:0.5px solid black;'>
                                                            <td style='width:20%;'><b> Component </b></td>
                                                            <td style='width:10%;'><b> Course Code </b></td>
                                                            <td style='width:35%;'><b> Course </b></td>
                                                            <td style='width:5%; text-align:center'><b> Grade </b></td>
                                                            <td style='width:5%; text-align:center'><b> GPA </b></td>
                                                            <td style='width:10%; text-align:center'><b> Credit Hour </b></td>
                                                            <td style='width:15%; text-align:center'><b> GPA x Credit Hour </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td rowspan='2'><b> Academic (" & academic_value & "%) </b></td>
                                                            <td ><b>")

                                If get_ExamYearSem1 = "2020" Then
                                    Dim get_Semester As String = ""
                                    Dim get_Level As String = ""

                                    If j_examName = "1" Or j_examName = "5" Or j_examName = "9" Then
                                        get_Semester = "Sem 1"
                                    ElseIf j_examName = "3" Or j_examName = "7" Or j_examName = "11" Then
                                        get_Semester = "Sem 2"
                                    End If

                                    If j_examName = "1" Or j_examName = "3" Then
                                        get_Level = "Foundation 1"
                                    ElseIf j_examName = "5" Or j_examName = "7" Then
                                        get_Level = "Foundation 2"
                                    ElseIf j_examName = "9" Or j_examName = "11" Then
                                        get_Level = "Foundation 3"
                                    End If

                                    ''print subject name for Exam 1, 3, 5
                                    tmpSQL_Nama = " Select subject_Name from subject_info left join course on subject_info.subject_ID = course.subject_ID 
                                                    where course.year = '" & get_ExamYearSem1 & "' and subject_info.subject_year = '" & get_ExamYearSem1 & "' and subject_info.course_Name <> 'Portfolio' and subject_info.course_Name <> 'Penyelidikan' and subject_info.course_Name <> 'Pembangunan Kendiri'
                                                    and subject_info.subject_StudentYear = '" & get_Level & "' and subject_info.subject_sem = '" & get_Semester & "' and course.std_ID = '" & strKey & "'
                                                    order by course_Name, subject_NameBM ASC"
                                    Dim SQA_2020 As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                    ''print subject code for Exam 1, 3, 5
                                    tmpSQL_Kod = "  Select subject_code from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                                    where course.year = '" & get_ExamYearSem1 & "' and subject_info.subject_year = '" & get_ExamYearSem1 & "' and subject_info.course_Name <> 'Portfolio' and subject_info.course_Name <> 'Penyelidikan' and subject_info.course_Name <> 'Pembangunan Kendiri'
                                                    and subject_info.subject_StudentYear = '" & get_Level & "' and subject_info.subject_sem = '" & get_Semester & "' and course.std_ID = '" & strKey & "'
                                                    order by course_Name, subject_NameBM ASC"
                                    Dim SQACODE_2020 As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                    Dim DS_Nama_2020 As New DataTable
                                    Dim DS_Kod_2020 As New DataTable

                                    Try
                                        SQA_2020.Fill(DS_Nama_2020)
                                        SQACODE_2020.Fill(DS_Kod_2020)
                                    Catch ex As Exception
                                    End Try

                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod_2020.Rows
                                        For Each column As DataColumn In DS_Kod_2020.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td >")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama_2020.Rows
                                        For Each column As DataColumn In DS_Nama_2020.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next


                                    Test.Append("           </td>
                                                            <td ></td>
                                                            <td ></td>
                                                            <td style=' text-align:center'> In Progress </td>
                                                            <td ></td>
                                                        </tr>")
                                Else

                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod.Rows
                                        For Each column As DataColumn In DS_Kod.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td>")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama.Rows
                                        For Each column As DataColumn In DS_Nama.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'><b>")

                                    ''(column course grade)
                                    For Each row As DataRow In DS_Gred.Rows
                                        For Each column As DataColumn In DS_Gred.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td style='text-align:center;'>")

                                    ''(column course gpa )
                                    For Each row As DataRow In DS_PNG.Rows
                                        For Each column As DataColumn In DS_PNG.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course credit hour)
                                    For Each row As DataRow In DS_Hour.Rows
                                        For Each column As DataColumn In DS_Hour.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course total)
                                    For Each row As DataRow In DS_Total.Rows
                                        For Each column As DataColumn In DS_Total.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.AppendLine("           </td>
                                                        </tr>")

                                End If

                                ''print english literature
                                If Confirm_Eng_Literature = "On" Then

                                    Dim SQEnglish_Literature_GRED As String = ""
                                    Dim SQEnglish_Literature_PNG As String = ""
                                    Dim SQEnglish_Literature_KOD As String = ""
                                    Dim SQEnglish_Literature_HOUR As String = ""
                                    Dim SQEnglish_Literature_TOTAL As String = ""

                                    tmpsql_EL_GRED = "  SELECT grade FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_GRED = oCommon.getFieldValue(tmpsql_EL_GRED)

                                    tmpsql_EL_PNG = "   SELECT gpa FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_PNG = oCommon.getFieldValue(tmpsql_EL_PNG)

                                    tmpsql_EL_KOD = "   SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_KOD = oCommon.getFieldValue(tmpsql_EL_KOD)

                                    tmpsql_EL_HOUR = "  SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_HOUR = oCommon.getFieldValue(tmpsql_EL_HOUR)

                                    tmpsql_EL_TOTAL = " SELECT total FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_TOTAL = oCommon.getFieldValue(tmpsql_EL_TOTAL)

                                    If get_ExamYearSem1 = "2020" Then

                                        Test.Append("   <tr style='font-size:9px;'>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")

                                    Else
                                        If SQEnglish_Literature_GRED.Length > 0 Then
                                            Test.AppendLine("<tr style='font-size:9px;'>
                                                                <td style='border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_KOD & " </b></td>
                                                                <td style='border-bottom:0.5px solid black'> AP English Literature and Composition </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_GRED & " </b></td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_PNG & " </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_HOUR & " </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_TOTAL & " </td>
                                                            </tr>")

                                            If SQEnglish_Literature_HOUR = "" Then
                                                total_Credit_EL = "0"
                                            End If

                                            If SQEnglish_Literature_TOTAL = "" Then
                                                total_Total_EL = "0"
                                            End If
                                        Else
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                                    <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                                </tr>")
                                        End If
                                    End If
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                        </tr>")
                                End If

                                If total_Credit = "" Then
                                    total_Credit = "0"
                                End If

                                If total_Total = "" Then
                                    total_Total = "0"
                                End If

                                ''Calculatr Total Credit Hour , PNG x Credit Hour & PNG Academic
                                Dim Number1 As Double = Double.Parse(total_Credit)
                                Dim Number2 As Double = Double.Parse(total_Credit_EL)
                                Dim Number3 As Double = Double.Parse(total_Total)
                                Dim Number4 As Double = Double.Parse(total_Total_EL)

                                Dim total_Hour As Double = Number1 + Number2
                                Dim final_Total As Double = Number3 + Number4

                                Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

                                If total_Hour = 0 And final_Total = 0 Then
                                    PNG_Akademik = 0
                                End If

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right'><b> Total </b></td>
                                                            <td style='text-align:center'><b> " & total_Hour & " </b></td>
                                                            <td style='text-align:center'><b> " & final_Total & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right; border-bottom:0.5px solid black'><b> Academic GPA </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & PNG_Akademik & " </b></td>
                                                        </tr> ")

                                '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                                If Confirm_Cocuricullum = "oN" Or Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then

                                    Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                    Dim getStudent As String = oCommon.getFieldValue(studentData)

                                    If j_examName = 2 Or j_examName = 6 Or j_examName = 10 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_PNG = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_Gred = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                            SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                            SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                            SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                            SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                            SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                        Catch ex As Exception

                                        End Try

                                    ElseIf j_examName = 4 Or j_examName = 7 Or j_examName = 8 Or j_examName = 12 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "    select koko_kolejpermata.NAMABI from koko_pelajar
                                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                      where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                            SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                            SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                            SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                            SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                            SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                        Catch ex As Exception

                                        End Try

                                    End If
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("   <tr style='font-size:9px;'>
                                                            <td><b> Co-curriculum (" & cocuricullum_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td style='text-align:center;'> In Progress </td>
                                                            <td></td>
                                                        </tr>")

                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Cocurriculum (" & cocuricullum_value & "%) </b></td>
                                                            <td><b>")

                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum kod sukan
                                        For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod kelab
                                        For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod uniform
                                        For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("PKS317 <br />")

                                    ElseIf Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Or Confirm_Cocuricullum = "OFF" Then
                                        Test.Append("<br />")
                                    End If

                                    Test.AppendLine("           </b></td>
                                                            <td>")

                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum nama skan
                                        For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama kelab
                                        For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama uniform
                                        For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("Swimming <br />")

                                    ElseIf Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Then
                                        Test.Append("<br />")
                                    End If

                                    If DSCocurricullum_PNG.Length > 0 Then
                                        Test.AppendLine("       </td>
                                                            <td style='text-align:center'><b> " & DSCocurricullum_Gred & " </b></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG) & " </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("       </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print Portfolio
                                If Confirm_Portfolio = "ON" Or Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
                                    tmpsql_Portfolio_GRED = "   SELECT grade FROM [ExamSlip_Portfolio] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_GRED = oCommon.getFieldValue(tmpsql_Portfolio_GRED)

                                    tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_PNG = oCommon.getFieldValue(tmpsql_Portfolio_PNG)

                                    tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_KOD = oCommon.getFieldValue(tmpsql_Portfolio_KOD)

                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    If DSPortfolio_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSPortfolio_GRED & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print research 
                                If Confirm_Research = "ON" Or Confirm_Research = "On" Or Confirm_Research = "on" Then
                                    tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_GRED = oCommon.getFieldValue(tmpsql_Penyelidikan_Gred)

                                    tmpsql_Penyelidikan_PNG = " SELECT gpa FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_PNG = oCommon.getFieldValue(tmpsql_Penyelidikan_PNG)

                                    tmpsql_Penyelidikan_KOD = " SELECT subject_code FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_KOD = oCommon.getFieldValue(tmpsql_Penyelidikan_KOD)
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    If DSResearch_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSResearch_GRED & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print self development
                                If Confirm_Self = "ON" Or Confirm_Self = "On" Or Confirm_Self = "on" Then
                                    Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & get_ExamYearSem1 & "' "
                                    Dim getLevel As String = oCommon.getFieldValue(level)

                                    If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD = oCommon.getFieldValue(tmpsql_SD_KOD)

                                    ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD = oCommon.getFieldValue(tmpsql_SD_KOD)
                                    End If
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  In Progress </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                Else
                                    If DSSelfdevelopment_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_GRED & " </b></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG) & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  In Progress </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''Print PNG & PNGK 
                                Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

                                Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

                                If exist_png_data = "" Then
                                    exist_png_data = "0"
                                End If

                                If exist_pngs_data = "" Then
                                    exist_pngs_data = "0"
                                End If

                                Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
                                Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> GPA </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & png_dec & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> CGPA </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & pngs_dec & " </b></td>
                                                        </tr>")
                                Test.AppendLine("   </table>")


                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''' SEMESTER 2 - SEMESTER 2 - SEMESTER 2 - SEMESTER 2 - SEMESTER 2 '''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                'get Portfolio percentage on / off
                                check_portfolio_percen = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

                                ''get cocuricullum percentage on / off
                                check_cocuricullum_percen = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

                                ''get research percentage on / off
                                check_research_percen = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Research = oCommon.getFieldValue(check_research_percen)

                                ''get self development percentage on / off
                                check_self_percen = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Self = oCommon.getFieldValue(check_self_percen)


                                ''print subject name 
                                tmpSQL_Nama = " SELECT subject_Name FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQA_Sem2 As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                ''print subject code
                                tmpSQL_Kod = "  SELECT subject_code FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQACODE_Sem2 As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                ''print subject grade
                                tmpSQL_Gred = " SELECT grade FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAGRADE_Sem2 As New SqlDataAdapter(tmpSQL_Gred, strConn)

                                ''print subject png
                                tmpSQL_PNG = "  SELECT gpa FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAPNG_Sem2 As New SqlDataAdapter(tmpSQL_PNG, strConn)

                                ''print subject credit hour
                                tmpSQL_Hour = " SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAHOUR_Sem2 As New SqlDataAdapter(tmpSQL_Hour, strConn)

                                ''print subject credit hour
                                tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQATOTAL_Sem2 As New SqlDataAdapter(tmpSQL_Total, strConn)

                                ''print total credit hour for subject taken
                                tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Credit_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                ''print total PNG x credit hour for subject taken
                                tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Total_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                ''print academic percentage
                                tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim academic_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                Dim DS_Nama_Sem2 As New DataTable
                                Dim DS_Kod_Sem2 As New DataTable
                                Dim DS_Gred_Sem2 As New DataTable
                                Dim DS_PNG_Sem2 As New DataTable
                                Dim DS_Hour_Sem2 As New DataTable
                                Dim DS_Total_Sem2 As New DataTable

                                Try
                                    SQA_Sem2.Fill(DS_Nama_Sem2)
                                    SQACODE_Sem2.Fill(DS_Kod_Sem2)
                                    SQAPNG_Sem2.Fill(DS_PNG_Sem2)
                                    SQAGRADE_Sem2.Fill(DS_Gred_Sem2)
                                    SQAHOUR_Sem2.Fill(DS_Hour_Sem2)
                                    SQATOTAL_Sem2.Fill(DS_Total_Sem2)
                                Catch ex As Exception
                                End Try

                                Dim DSSelfdevelopment_GRED_Sem2 As String = ""
                                Dim DSSelfdevelopment_PNG_Sem2 As String = ""
                                Dim DSSelfdevelopment_KOD_Sem2 As String = ""

                                Dim DSEnglish_literature_GRED_Sem2 As New DataTable
                                Dim DSEnglish_literature_PNG_Sem2 As New DataTable
                                Dim DSEnglish_literature_KOD_Sem2 As New DataTable
                                Dim DSEnglish_literature_HOUR_Sem2 As New DataTable
                                Dim DSEnglish_literature_TOTAL_Sem2 As New DataTable

                                Dim DSResearch_GRED_Sem2 As String = ""
                                Dim DSResearch_PNG_Sem2 As String = ""
                                Dim DSResearch_KOD_Sem2 As String = ""

                                Dim DSPortfolio_GRED_Sem2 As String = ""
                                Dim DSPortfolio_PNG_Sem2 As String = ""
                                Dim DSPortfolio_KOD_Sem2 As String = ""

                                Dim DSCocuricullum_KOD_SUKAN_Sem2 As New DataTable
                                Dim DSCocuricullum_KOD_UNIFORM_Sem2 As New DataTable
                                Dim DSCocuricullum_KOD_KELAB_Sem2 As New DataTable
                                Dim DSCocuricullum_NAMA_SUKAN_Sem2 As New DataTable
                                Dim DSCocuricullum_NAMA_UNIFORM_Sem2 As New DataTable
                                Dim DSCocuricullum_NAMA_KELAB_Sem2 As New DataTable
                                Dim DSCocurricullum_Gred_Sem2 As String = ""
                                Dim DSCocurricullum_PNG_Sem2 As String = ""

                                Dim total_Credit_EL_Sem2 As String = "0"
                                Dim total_Total_EL_Sem2 As String = "0"

                                tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim cocuricullum_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim portfolio_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim research_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim sd_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)


                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:15px'>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='5' style='border-top:0.5px solid black; border-bottom:0.5px solid black; background-color:lightgray; font-size:9.5px; text-align:center'><b> ASSESSMENT " & get_ExamRename & get_SemesterName + 1 & ", ACADEMIC YEAR " & get_ExamYearSem1 & " </b></td>
                                                        </tr>
                                                    </table>

                                                    <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:5px'>
                                                        <tr style='font-size:9px;background-color:lightgray'>
                                                            <td style='width:20%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Component </b></td>
                                                            <td style='width:10%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Course Code </b></td>
                                                            <td style='width:35%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Course </b></td>
                                                            <td style='width:5%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> Grade </b></td>
                                                            <td style='width:5%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> GPA </b></td>
                                                            <td style='width:10%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> Credit Hour </b></td>
                                                            <td style='width:15%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> GPA x Credit Hour </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td rowspan='2'><b> Akademic (" & academic_value_Sem2 & "%) </b></td>
                                                            <td ><b>")

                                ''(column course code)
                                For Each row As DataRow In DS_Kod_Sem2.Rows
                                    For Each column As DataColumn In DS_Kod_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </b></td>
                                                            <td >")

                                ''(column course name)
                                For Each row As DataRow In DS_Nama_Sem2.Rows
                                    For Each column As DataColumn In DS_Nama_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </td>
                                                            <td style='text-align:center;'><b>")

                                ''(column course grade)
                                For Each row As DataRow In DS_Gred_Sem2.Rows
                                    For Each column As DataColumn In DS_Gred_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </b></td>
                                                            <td style='text-align:center;'>")

                                ''(column course gpa )
                                For Each row As DataRow In DS_PNG_Sem2.Rows
                                    For Each column As DataColumn In DS_PNG_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                ''(column course credit hour)
                                For Each row As DataRow In DS_Hour_Sem2.Rows
                                    For Each column As DataColumn In DS_Hour_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                ''(column course total)
                                For Each row As DataRow In DS_Total_Sem2.Rows
                                    For Each column As DataColumn In DS_Total_Sem2.Columns
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("<br />")
                                    Next
                                Next

                                Test.AppendLine("           </td>
                                                        </tr>")

                                ''print english literature
                                If Confirm_Eng_Literature = "On" Then

                                    Dim SQEnglish_Literature_GRED As String = ""
                                    Dim SQEnglish_Literature_PNG As String = ""
                                    Dim SQEnglish_Literature_KOD As String = ""
                                    Dim SQEnglish_Literature_HOUR As String = ""
                                    Dim SQEnglish_Literature_TOTAL As String = ""

                                    tmpsql_EL_GRED = "  SELECT grade FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_GRED = oCommon.getFieldValue(tmpsql_EL_GRED)

                                    tmpsql_EL_PNG = "   SELECT gpa FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_PNG = oCommon.getFieldValue(tmpsql_EL_PNG)

                                    tmpsql_EL_KOD = "   SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_KOD = oCommon.getFieldValue(tmpsql_EL_KOD)

                                    tmpsql_EL_HOUR = "  SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_HOUR = oCommon.getFieldValue(tmpsql_EL_HOUR)

                                    tmpsql_EL_TOTAL = " SELECT total FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_TOTAL = oCommon.getFieldValue(tmpsql_EL_TOTAL)

                                    If SQEnglish_Literature_GRED.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td style='border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'> AP English Literature and Composition </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_GRED & " </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_PNG & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_HOUR & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_TOTAL & " </td>
                                                        </tr>")

                                        total_Credit_EL_Sem2 = SQEnglish_Literature_HOUR
                                        total_Total_EL_Sem2 = SQEnglish_Literature_TOTAL
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                                    <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                            </tr>")
                                    End If

                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td colspan='6'><b></b></td>
                                                        </tr>")
                                End If

                                If total_Credit_Sem2 = "" Then
                                    total_Credit_Sem2 = "0"
                                End If

                                If total_Total_Sem2 = "" Then
                                    total_Total_Sem2 = "0"
                                End If

                                ''Calculatr Total Credit Hour , PNG x Credit Hour & PNG Academic
                                Dim Number1_Sem2 As Double = Double.Parse(total_Credit_Sem2)
                                Dim Number2_Sem2 As Double = Double.Parse(total_Credit_EL_Sem2)
                                Dim Number3_Sem2 As Double = Double.Parse(total_Total_Sem2)
                                Dim Number4_Sem2 As Double = Double.Parse(total_Total_EL_Sem2)

                                Dim total_Hour_Sem2 As Double = Number1_Sem2 + Number2_Sem2
                                Dim final_Total_Sem2 As Double = Number3_Sem2 + Number4_Sem2

                                Dim PNG_Akademik_Sem2 As Double = Math.Round(final_Total_Sem2 / total_Hour_Sem2, 2)

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right'><b> Total </b></td>
                                                            <td style='text-align:center'><b> " & total_Hour_Sem2 & " </b></td>
                                                            <td style='text-align:center'><b> " & final_Total_Sem2 & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right; border-bottom:0.5px solid black'><b> Academic GPA </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & PNG_Akademik_Sem2 & " </b></td>
                                                        </tr> ")

                                '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                                If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then

                                    Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                    Dim getStudent As String = oCommon.getFieldValue(studentData)

                                    If j_examName + 1 = 2 Or j_examName + 1 = 6 Or j_examName + 1 = 10 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_PNG_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_Gred_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN_Sem2.Fill(DSCocuricullum_KOD_SUKAN_Sem2)
                                            SQCocuricullum_KOD_KELAB_Sem2.Fill(DSCocuricullum_KOD_KELAB_Sem2)
                                            SQCocuricullum_KOD_UNIFORM_Sem2.Fill(DSCocuricullum_KOD_UNIFORM_Sem2)
                                            SQCocuricullum_NAMA_SUKAN_Sem2.Fill(DSCocuricullum_NAMA_SUKAN_Sem2)
                                            SQCocuricullum_NAMA_KELAB_Sem2.Fill(DSCocuricullum_NAMA_KELAB_Sem2)
                                            SQCocuricullum_NAMA_UNIFORM_Sem2.Fill(DSCocuricullum_NAMA_UNIFORM_Sem2)
                                        Catch ex As Exception

                                        End Try

                                    ElseIf j_examName + 1 = 4 Or j_examName + 1 = 8 Or j_examName + 1 = 12 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_PNG_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_Gred_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "    select koko_kolejpermata.NAMABI from koko_pelajar
                                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                      where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN_Sem2.Fill(DSCocuricullum_KOD_SUKAN_Sem2)
                                            SQCocuricullum_KOD_KELAB_Sem2.Fill(DSCocuricullum_KOD_KELAB_Sem2)
                                            SQCocuricullum_KOD_UNIFORM_Sem2.Fill(DSCocuricullum_KOD_UNIFORM_Sem2)
                                            SQCocuricullum_NAMA_SUKAN_Sem2.Fill(DSCocuricullum_NAMA_SUKAN_Sem2)
                                            SQCocuricullum_NAMA_KELAB_Sem2.Fill(DSCocuricullum_NAMA_KELAB_Sem2)
                                            SQCocuricullum_NAMA_UNIFORM_Sem2.Fill(DSCocuricullum_NAMA_UNIFORM_Sem2)
                                        Catch ex As Exception

                                        End Try

                                    End If
                                End If

                                Test.AppendLine("        <tr style='font-size:9px;'>
                                                            <td><b> Co-curriculum (" & cocuricullum_value_Sem2 & "%) </b></td>
                                                            <td><b>")


                                If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                    ''kokorikulum kod sukan
                                    For Each row As DataRow In DSCocuricullum_KOD_SUKAN_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_KOD_SUKAN_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next
                                    ''kokorikulum kod kelab
                                    For Each row As DataRow In DSCocuricullum_KOD_KELAB_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_KOD_KELAB_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next
                                    ''kokorikulum kod uniform
                                    For Each row As DataRow In DSCocuricullum_KOD_UNIFORM_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("PKS317 <br />")

                                ElseIf Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "off" Then
                                    Test.Append("<br />")
                                End If

                                Test.AppendLine("           </b></td>
                                                            <td>")

                                If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                    ''kokorikulum nama skan
                                    For Each row As DataRow In DSCocuricullum_NAMA_SUKAN_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next
                                    ''kokorikulum nama kelab
                                    For Each row As DataRow In DSCocuricullum_NAMA_KELAB_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_NAMA_KELAB_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next
                                    ''kokorikulum nama uniform
                                    For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM_Sem2.Rows
                                        For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("Swimming <br />")

                                ElseIf Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Then
                                    Test.Append("<br />")
                                End If

                                If DSCocurricullum_PNG_Sem2.Length > 0 Then
                                    Test.AppendLine("       </td>
                                                            <td style='text-align:center'><b> " & DSCocurricullum_Gred_Sem2 & " </b></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG_Sem2) & " </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG_Sem2) & " </td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("       </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                End If

                                ''print Portfolio
                                If Confirm_Portfolio = "ON" Or Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
                                    tmpsql_Portfolio_GRED = "   SELECT grade FROM [ExamSlip_Portfolio] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_GRED_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_GRED)

                                    tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_PNG_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_PNG)

                                    tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_KOD_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_KOD)

                                End If

                                If DSPortfolio_PNG_Sem2.Length > 0 Then
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSPortfolio_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG_Sem2) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG_Sem2) & " </td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                End If

                                ''print research 
                                If Confirm_Research = "ON" Or Confirm_Research = "On" Or Confirm_Research = "on" Then
                                    tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_GRED_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_Gred)

                                    tmpsql_Penyelidikan_PNG = " SELECT gpa FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_PNG_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_PNG)

                                    tmpsql_Penyelidikan_KOD = " SELECT subject_code FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_KOD_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_KOD)
                                End If

                                If DSResearch_PNG_Sem2.Length > 0 Then
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSResearch_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG_Sem2) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG_Sem2) & " </td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                End If

                                ''print self development
                                If Confirm_Self = "ON" Or Confirm_Self = "On" Or Confirm_Self = "on" Then
                                    Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & get_ExamYearSem1 & "' "
                                    Dim getLevel As String = oCommon.getFieldValue(level)

                                    If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED_Sem2 = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG_Sem2 = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD_Sem2 = oCommon.getFieldValue(tmpsql_SD_KOD)

                                    ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED_Sem2 = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG_Sem2 = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD_Sem2 = oCommon.getFieldValue(tmpsql_SD_KOD)
                                    End If
                                End If

                                If DSSelfdevelopment_PNG_Sem2.Length > 0 Then
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value_Sem2 & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD_Sem2 & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG_Sem2) & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG_Sem2) & " </td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value_Sem2 & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD_Sem2 & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  In Progress </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                End If

                                ''Print PNG & PNGK 
                                Dim check_png_exist_data_Sem2 As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_png_data_Sem2 As String = oCommon.getFieldValue(check_png_exist_data_Sem2)

                                Dim check_pngs_exist_data_Sem2 As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_pngs_data_Sem2 As String = oCommon.getFieldValue(check_pngs_exist_data_Sem2)

                                If exist_png_data_Sem2 = "" Then
                                    exist_png_data_Sem2 = "0"
                                End If

                                If exist_pngs_data_Sem2 = "" Then
                                    exist_pngs_data_Sem2 = "0"
                                End If

                                Dim png_dec_Sem2 As Decimal = Decimal.Parse(exist_png_data_Sem2)
                                Dim pngs_dec_Sem2 As Decimal = Decimal.Parse(exist_pngs_data_Sem2)

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> GPA </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & png_dec_Sem2 & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> CGPA </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & pngs_dec_Sem2 & " </b></td>
                                                        </tr>")
                                Test.AppendLine("   </table>")

                                Dim find_printStatus As String = "Select Value from setting where idx = 'Examination' and Type = 'Print Date Status'"
                                Dim get_printStatus As String = oCommon.getFieldValue(find_printStatus)

                                Dim find_PrintDateJSC As String = "Select Value from setting where idx = 'Examination' and Type = 'Printing Date'"
                                Dim get_PrintDateJSC As String = oCommon.getFieldValue(find_PrintDateJSC)

                                If get_printStatus = "On" And get_printStatus = "ON" And get_printStatus = "on" Then

                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:50px; left:0px; font-family:Century Gothic;'>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:45%'></td>
                                                            <td style='width:35%;></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:45%'></td>
                                                            <td style='width:35%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%; text-align:left; color:red; font-size:9px'> Reference No : </td>    
                                                            <td style='width:45%; text-align:left; font-size:9px'> Print Date : </td>
                                                            <td style='width:35%'></td>
                                                        </tr>
                                                        <tr >
                                                            <td style='width:20%; text-align:left; color:red; font-size:10px'><b> " & get_StudentID & " </b></td>    
                                                            <td style='width:45%; text-align:left; font-size:9px'><b> " & get_PrintDateJSC & " <b></td>
                                                            <td style='width:35%'></td>
                                                        </tr>                                                        
                                                    </table>")

                                Else

                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:20px; left:0px; font-family:Century Gothic;'>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%; text-align:left; color:red; font-size:9px'> Reference No : </td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr >
                                                            <td style='width:20%; text-align:left; color:red; font-size:10px'><b> " & get_StudentID & " </b></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>                                                        
                                                    </table>")

                                End If

                                If j_examName + 1 = 12 Then
                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:20px; left:0px; font-family:Century Gothic;'>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%;  border-top:1px dotted black'></td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Prof. Madya Dr Rorlinda binti Yusof </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Director, </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Pusat GENIUS@Pintar Negara </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Universiti Kebangsaan Malaysia </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                        </table>")
                                End If

                                Test.AppendLine("<div style='width:98%; position:absolute; bottom:0px; left:0px; font-family:Century Gothic;'> <p style='text-align:right; font-size:9px'>  " & countPage & " of 6 </p></div>")

                                Test.AppendLine("</div>")

                                countPage = countPage + 1
                            Next


                        End If
                    End If
                Next

            ElseIf ddl_TranscriptType.SelectedValue = "High School" Then

                For i = 0 To TranscriptRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(TranscriptRespondent.Rows(i).Cells(5).FindControl("chkSelectTranscript"), CheckBox)

                    Dim j_examName As Integer = 0
                    Dim countPage As Integer = 0

                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = TranscriptRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''Get Student Name
                            Dim select_StudentName As String = "select  UPPER(student_Name) from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentName As String = oCommon.getFieldValue(select_StudentName)

                            ''Get Student ID
                            Dim select_StudentID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue(select_StudentID)

                            ''Get Student MYKAD
                            Dim select_StudentMYKAD As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim get_StudentMYKAD As String = oCommon.getFieldValue(select_StudentMYKAD)

                            '''Get Student Start Date Year
                            'Dim select_StudentSDY As String = "select year from student_level where std_ID = '" & strKey & "' and student_Level = 'Level 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDY As String = oCommon.getFieldValue(select_StudentSDY)

                            '''Get Student Start Date Month
                            'Dim select_StudentSDM As String = "select month from student_level where std_ID = '" & strKey & "' and student_Level = 'Level 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDM As String = oCommon.getFieldValue(select_StudentSDM)

                            '''Get Student Start Date Day
                            'Dim select_StudentSDD As String = "select day from student_level where std_ID = '" & strKey & "' and student_Level = 'Level 1' and student_Sem = 'Sem 1'"
                            'Dim get_StudentSDD As String = oCommon.getFieldValue(select_StudentSDD)

                            ''Get Student First Date Year
                            Dim find_StudentFDM As String = "Select Value from Setting where Idx = 'Examination' and Type = 'Senior Entry Date'"
                            Dim get_StudentFDM As String = oCommon.getFieldValue(find_StudentFDM)

                            ''Get Student Last Date Year
                            Dim find_StudentLDM As String = "Select Value from Setting where Idx = 'Examination' and Type = 'Senior Term Ending'"
                            Dim get_StudentLDM As String = oCommon.getFieldValue(find_StudentLDM)

                            Dim EndMonth As String = System.DateTime.Now.Month

                            If EndMonth = "1" Then
                                EndMonth = "January"
                            ElseIf EndMonth = "2" Then
                                EndMonth = "February"
                            ElseIf EndMonth = "3" Then
                                EndMonth = "March"
                            ElseIf EndMonth = "4" Then
                                EndMonth = "April"
                            ElseIf EndMonth = "5" Then
                                EndMonth = "May"
                            ElseIf EndMonth = "6" Then
                                EndMonth = "June"
                            ElseIf EndMonth = "7" Then
                                EndMonth = "July"
                            ElseIf EndMonth = "8" Then
                                EndMonth = "August"
                            ElseIf EndMonth = "9" Then
                                EndMonth = "September"
                            ElseIf EndMonth = "10" Then
                                EndMonth = "October"
                            ElseIf EndMonth = "11" Then
                                EndMonth = "November"
                            ElseIf EndMonth = "12" Then
                                EndMonth = "December"
                            End If

                            countPage = 1

                            For j_examName = 1 To 6 Step j_examName + 2

                                ''getYear Exam 1,3,5
                                Dim select_ExamYearSem1 As String = "Select year from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and student_type = 'TAHAP'"
                                Dim get_ExamYearSem1 As String = oCommon.getFieldValue(select_ExamYearSem1)

                                ''getYear Exam 2,4,6
                                Dim select_ExamYearSem2 As String = "Select year from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and student_type = 'TAHAP'"
                                Dim get_ExamYearSem2 As String = oCommon.getFieldValue(select_ExamYearSem2)

                                If get_ExamYearSem1 = "" Then
                                    get_ExamYearSem1 = get_ExamYearSem2
                                End If

                                If get_ExamYearSem2 = "" Then
                                    get_ExamYearSem2 = get_ExamYearSem1
                                End If

                                Dim get_ExamRename As String = ""
                                Dim get_SemesterName As Integer = 0

                                If j_examName = 1 Then
                                    get_ExamRename = "1 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 3 Then
                                    get_ExamRename = "2 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 5 Then
                                    get_ExamRename = "3 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 7 Then
                                    get_ExamRename = "4 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 9 Then
                                    get_ExamRename = "5 SEMESTER "
                                    get_SemesterName = 1
                                ElseIf j_examName = 11 Then
                                    get_ExamRename = "6 SEMESTER "
                                    get_SemesterName = 1
                                End If

                                Test.AppendLine("<div style='margin: 0; page-break-after:always; padding-top:0px; margin-top:0px;height: 100%; display: block; position: relative;'>")

                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic; padding-top:0px; margin-top:0px; border-collapse: collapse;'>
                                                        <tr style='font-size:14px;text-align:center; width:100%'>
                                                            <td colspan='5'><b> OFFICIAL TRANSCRIPT KOLEJ GENIUS@Pintar </b></td>
                                                        </tr>
                                                        <tr style='font-size:14px;text-align:center; width:100%;'>
                                                            <td colspan='5'><b> UNIVERSITI KEBANGSAAN MALAYSIA </b></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr style='text-align:left'>
                                                            <td rowspan='8' style='with:40%; padding:0px; margin:0px;height:60px; width:300px;'></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr>
    	                                                    <td colspan='2' style='with:40%; font-size:9px;'>Name :</td>
                                                            <td style='with:20%; font-size:9px; text-align:right'>Admission Date :</td>
                                                        </tr>
                                                        <tr>
    	                                                    <td colspan='2' style='with:40%; font-size:12px;'><b>" & get_StudentName.ToUpper & "</b></td>
                                                            <td style='with:20%; font-size:10px; ; text-align:right'><b>" & get_StudentFDM & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr >
    	                                                    <td style='with:25%; font-size:9px; '>Matrix Card No :</td>
                                                            <td style='with:25%; font-size:9px; '>Identification Card No :</td>
                                                            <td style='with:10%; font-size:9px; text-align:right'>Graduation Date :</td>
                                                        </tr>
                                                        <tr>
    	                                                    <td style='with:25%; font-size:10px; '><b>" & get_StudentID & "</b></td>
                                                            <td style='with:25%; font-size:10px; '><b>" & get_StudentMYKAD & "</b></td>
                                                            <td style='with:10%; font-size:10px; text-align:right'><b>" & get_StudentLDM & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='3' style='with:60%; border-top:0.5px solid black'></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                            
                                                    <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:15px'>
                                                        <tr>
                                                            <td colspan='5' style='border-top:0.5px solid black; border-bottom:0.5px solid black; background-color:lightgray; font-size:9.5px; text-align:center'><b> ASSESSMENT " & get_ExamRename & get_SemesterName & ", ACADEMIC YEAR " & get_ExamYearSem1 & " </b></td>
                                                        </tr>
                                                    </table>")

                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''' SEMESTER 1 - SEMESTER 1 - SEMESTER 1 - SEMESTER 1 - SEMESTER 1 '''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                'get Portfolio percentage on / off
                                check_portfolio_percen = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

                                ''get cocuricullum percentage on / off
                                check_cocuricullum_percen = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

                                ''get research percentage on / off
                                check_research_percen = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Research = oCommon.getFieldValue(check_research_percen)

                                ''get self development percentage on / off
                                check_self_percen = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Confirm_Self = oCommon.getFieldValue(check_self_percen)

                                'get englih literture on / off
                                check_Eng_Literature = "select Value from setting where Type = 'English Literature'"
                                Confirm_Eng_Literature = oCommon.getFieldValue(check_Eng_Literature)

                                ''print subject name 
                                tmpSQL_Nama = " SELECT subject_Name FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                ''print subject code
                                tmpSQL_Kod = "  SELECT subject_code FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                ''print subject grade
                                tmpSQL_Gred = " SELECT grade FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

                                ''print subject png
                                tmpSQL_PNG = "  SELECT gpa FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

                                ''print subject credit hour
                                tmpSQL_Hour = " SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

                                ''print subject credit hour
                                tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)

                                ''print total credit hour for subject taken
                                tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

                                ''print total PNG x credit hour for subject taken
                                tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                Dim total_Total As String = oCommon.getFieldValue(tmpSQL)

                                ''print academic percentage
                                tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

                                Dim DS_Nama As New DataTable
                                Dim DS_Kod As New DataTable
                                Dim DS_Gred As New DataTable
                                Dim DS_PNG As New DataTable
                                Dim DS_Hour As New DataTable
                                Dim DS_Total As New DataTable

                                Try
                                    SQA.Fill(DS_Nama)
                                    SQACODE.Fill(DS_Kod)
                                    SQAPNG.Fill(DS_PNG)
                                    SQAGRADE.Fill(DS_Gred)
                                    SQAHOUR.Fill(DS_Hour)
                                    SQATOTAL.Fill(DS_Total)
                                Catch ex As Exception
                                End Try

                                Dim DSSelfdevelopment_GRED As String = ""
                                Dim DSSelfdevelopment_PNG As String = ""
                                Dim DSSelfdevelopment_KOD As String = ""

                                Dim DSEnglish_literature_GRED As New DataTable
                                Dim DSEnglish_literature_PNG As New DataTable
                                Dim DSEnglish_literature_KOD As New DataTable
                                Dim DSEnglish_literature_HOUR As New DataTable
                                Dim DSEnglish_literature_TOTAL As New DataTable

                                Dim DSResearch_GRED As String = ""
                                Dim DSResearch_PNG As String = ""
                                Dim DSResearch_KOD As String = ""

                                Dim DSPortfolio_GRED As String = ""
                                Dim DSPortfolio_PNG As String = ""
                                Dim DSPortfolio_KOD As String = ""

                                Dim DSCocuricullum_KOD_SUKAN As New DataTable
                                Dim DSCocuricullum_KOD_UNIFORM As New DataTable
                                Dim DSCocuricullum_KOD_KELAB As New DataTable
                                Dim DSCocuricullum_NAMA_SUKAN As New DataTable
                                Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
                                Dim DSCocuricullum_NAMA_KELAB As New DataTable
                                Dim DSCocurricullum_Gred As String = ""
                                Dim DSCocurricullum_PNG As String = ""

                                Dim total_Credit_EL As String = "0"
                                Dim total_Total_EL As String = "0"

                                tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim research_value As String = oCommon.getFieldValue(tmpSQL)

                                tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim sd_value As String = oCommon.getFieldValue(tmpSQL)


                                Test.AppendLine("   <table style='width:100%; font-family:Century Gothic;border-collapse: collapse; margin-top:5px'>
                                                        <tr style='font-size:9px;background-color:lightgray; border-top:0.5px solid black; border-bottom:0.5px solid black;'>
                                                            <td style='width:20%;'><b> Component </b></td>
                                                            <td style='width:10%;'><b> Course Code </b></td>
                                                            <td style='width:35%;'><b> Course </b></td>
                                                            <td style='width:5%; text-align:center'><b> Grade </b></td>
                                                            <td style='width:5%; text-align:center'><b> GPA </b></td>
                                                            <td style='width:10%; text-align:center'><b> Credit Hour </b></td>
                                                            <td style='width:15%; text-align:center'><b> GPA x Credit Hour </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td rowspan='2'><b> Academic (" & academic_value & "%) </b></td>
                                                            <td ><b>")

                                If get_ExamYearSem1 = "2020" Then

                                    Dim get_Semester As String = ""
                                    Dim get_Level As String = ""

                                    If j_examName = "1" Or j_examName = "5" Then
                                        get_Semester = "Sem 1"
                                    ElseIf j_examName = "3" Then
                                        get_Semester = "Sem 2"
                                    End If

                                    If j_examName = "1" Or j_examName = "3" Then
                                        get_Level = "Level 1"
                                    ElseIf j_examName = "5" Then
                                        get_Level = "Level 2"
                                    End If

                                    ''print subject name for Exam 1, 3, 5
                                    tmpSQL_Nama = " Select subject_Name from subject_info left join course on subject_info.subject_ID = course.subject_ID 
                                                    where course.year = '" & get_ExamYearSem1 & "' and subject_info.subject_year = '" & get_ExamYearSem1 & "' and subject_info.course_Name <> 'Portfolio' and subject_info.course_Name <> 'Penyelidikan' and subject_info.course_Name <> 'Jati Diri'
                                                    and subject_info.subject_StudentYear = '" & get_Level & "' and subject_info.subject_sem = '" & get_Semester & "' and course.std_ID = '" & strKey & "'
                                                    order by course_Name, subject_NameBM ASC"
                                    Dim SQA_2020 As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                    ''print subject code for Exam 1, 3, 5
                                    tmpSQL_Kod = "  Select subject_code from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                                    where course.year = '" & get_ExamYearSem1 & "' and subject_info.subject_year = '" & get_ExamYearSem1 & "' and subject_info.course_Name <> 'Portfolio' and subject_info.course_Name <> 'Penyelidikan' and subject_info.course_Name <> 'Jati Diri'
                                                    and subject_info.subject_StudentYear = '" & get_Level & "' and subject_info.subject_sem = '" & get_Semester & "' and course.std_ID = '" & strKey & "'
                                                    order by course_Name, subject_NameBM ASC"
                                    Dim SQACODE_2020 As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                    Dim DS_Nama_2020 As New DataTable
                                    Dim DS_Kod_2020 As New DataTable

                                    Try
                                        SQA_2020.Fill(DS_Nama_2020)
                                        SQACODE_2020.Fill(DS_Kod_2020)
                                    Catch ex As Exception
                                    End Try

                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod_2020.Rows
                                        For Each column As DataColumn In DS_Kod_2020.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td >")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama_2020.Rows
                                        For Each column As DataColumn In DS_Nama_2020.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next


                                    Test.Append("           </td>
                                                            <td ></td>
                                                            <td ></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td ></td>
                                                        </tr>")
                                Else
                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod.Rows
                                        For Each column As DataColumn In DS_Kod.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td >")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama.Rows
                                        For Each column As DataColumn In DS_Nama.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center; '><b>")

                                    ''(column course grade)
                                    For Each row As DataRow In DS_Gred.Rows
                                        For Each column As DataColumn In DS_Gred.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td style='text-align:center;'>")

                                    ''(column course gpa )
                                    For Each row As DataRow In DS_PNG.Rows
                                        For Each column As DataColumn In DS_PNG.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course credit hour)
                                    For Each row As DataRow In DS_Hour.Rows
                                        For Each column As DataColumn In DS_Hour.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course total)
                                    For Each row As DataRow In DS_Total.Rows
                                        For Each column As DataColumn In DS_Total.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.AppendLine("           </td>
                                                        </tr>")
                                End If

                                ''print english literature
                                If Confirm_Eng_Literature = "On" Then

                                    Dim SQEnglish_Literature_GRED As String = ""
                                    Dim SQEnglish_Literature_PNG As String = ""
                                    Dim SQEnglish_Literature_KOD As String = ""
                                    Dim SQEnglish_Literature_HOUR As String = ""
                                    Dim SQEnglish_Literature_TOTAL As String = ""

                                    tmpsql_EL_GRED = "  SELECT grade FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_GRED = oCommon.getFieldValue(tmpsql_EL_GRED)

                                    tmpsql_EL_PNG = "   SELECT gpa FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_PNG = oCommon.getFieldValue(tmpsql_EL_PNG)

                                    tmpsql_EL_KOD = "   SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_KOD = oCommon.getFieldValue(tmpsql_EL_KOD)

                                    tmpsql_EL_HOUR = "  SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_HOUR = oCommon.getFieldValue(tmpsql_EL_HOUR)

                                    tmpsql_EL_TOTAL = " SELECT total FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    SQEnglish_Literature_TOTAL = oCommon.getFieldValue(tmpsql_EL_TOTAL)

                                    If get_ExamYearSem1 = "2020" Then

                                        Test.Append("   <tr style='font-size:9px;'>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    Else

                                        If SQEnglish_Literature_GRED.Length > 0 Then
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td style='border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'> AP English Literature and Composition </td>                                                            
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_GRED & " </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_PNG & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_HOUR & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_TOTAL & " </td>
                                                        </tr>")

                                            total_Credit_EL = SQEnglish_Literature_HOUR
                                            total_Total_EL = SQEnglish_Literature_TOTAL
                                        Else
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                                    <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                                </tr>")
                                        End If

                                    End If

                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                        </tr>")
                                End If

                                If total_Credit = "" Then
                                    total_Credit = "0"
                                End If

                                If total_Credit_EL = "" Then
                                    total_Credit_EL = "0"
                                End If

                                If total_Total = "" Then
                                    total_Total = "0"
                                End If

                                If total_Total_EL = "" Then
                                    total_Total_EL = "0"
                                End If

                                ''Calculatr Total Credit Hour , PNG x Credit Hour & PNG Academic
                                Dim Number1 As Double = Double.Parse(total_Credit)
                                Dim Number2 As Double = Double.Parse(total_Credit_EL)
                                Dim Number3 As Double = Double.Parse(total_Total)
                                Dim Number4 As Double = Double.Parse(total_Total_EL)

                                Dim total_Hour As Double = Number1 + Number2
                                Dim final_Total As Double = Number3 + Number4

                                Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

                                If total_Hour = 0 And final_Total = 0 Then
                                    PNG_Akademik = 0
                                End If

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right'><b> Total </b></td>
                                                            <td style='text-align:center'><b> " & total_Hour & " </b></td>
                                                            <td style='text-align:center'><b> " & final_Total & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right; border-bottom:0.5px solid black'><b> Academic GPA </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & PNG_Akademik & " </b></td>
                                                        </tr> ")

                                '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                                If Confirm_Cocuricullum = "oN" Or Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then

                                    Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                    Dim getStudent As String = oCommon.getFieldValue(studentData)

                                    If j_examName = 2 Or j_examName = 6 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_PNG = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                        DSCocurricullum_Gred = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                            SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                            SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                            SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                            SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                            SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                        Catch ex As Exception

                                        End Try

                                    ElseIf j_examName = 4 Then

                                        tmpsql_KOKO_PNG = " select koko_pelajar.PNGP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                        tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                        tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_NAMA_UNIFORM = "    select koko_kolejpermata.NAMABI from koko_pelajar
                                                                        left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                        left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                        where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                        tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                        tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                        Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                        Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                        Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                        Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                        Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                        Try
                                            SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                                            SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                                            SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                                            SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                                            SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                                            SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                                        Catch ex As Exception

                                        End Try

                                    End If
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("       <tr style='font-size:9px;'>
                                                            <td><b> Co-curriculum (" & cocuricullum_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Cocurriculum (" & cocuricullum_value & "%) </b></td>
                                                            <td><b>")


                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum kod sukan
                                        For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod kelab
                                        For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod uniform
                                        For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("PKS317 <br />")

                                    ElseIf Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Or Confirm_Cocuricullum = "OFF" Then
                                        Test.Append("<br />")
                                    End If

                                    Test.AppendLine("           </b></td>
                                                            <td>")

                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum nama skan
                                        For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama kelab
                                        For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama uniform
                                        For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("Swimming <br />")

                                    ElseIf Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Then
                                        Test.Append("<br />")
                                    End If

                                    If DSCocurricullum_PNG.Length > 0 Then
                                        Test.AppendLine("       </td>
                                                                <td style='text-align:center'><b> " & DSCocurricullum_Gred & " </b></td>
                                                                <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG) & " </td>
                                                                <td style='text-align:center'></td>
                                                                <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG) & " </td>
                                                            </tr>")
                                    Else
                                        Test.AppendLine("       </td>
                                                                <td style='text-align:center'></td>
                                                                <td style='text-align:center'></td>
                                                                <td style='text-align:center'> In Progress </td>
                                                                <td style='text-align:center'></td>
                                                            </tr>")
                                    End If
                                End If

                                ''print Portfolio
                                If Confirm_Portfolio = "ON" Or Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
                                    tmpsql_Portfolio_GRED = "   SELECT grade FROM [ExamSlip_Portfolio] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_GRED = oCommon.getFieldValue(tmpsql_Portfolio_GRED)

                                    tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_PNG = oCommon.getFieldValue(tmpsql_Portfolio_PNG)

                                    tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSPortfolio_KOD = oCommon.getFieldValue(tmpsql_Portfolio_KOD)

                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("       <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    If DSPortfolio_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSPortfolio_GRED & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print research 
                                If Confirm_Research = "ON" Or Confirm_Research = "On" Or Confirm_Research = "on" Then
                                    tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_GRED = oCommon.getFieldValue(tmpsql_Penyelidikan_Gred)

                                    tmpsql_Penyelidikan_PNG = " SELECT gpa FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_PNG = oCommon.getFieldValue(tmpsql_Penyelidikan_PNG)

                                    tmpsql_Penyelidikan_KOD = " SELECT subject_code FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    DSResearch_KOD = oCommon.getFieldValue(tmpsql_Penyelidikan_KOD)
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("       <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    If DSResearch_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSResearch_GRED & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''print self development
                                If Confirm_Self = "ON" Or Confirm_Self = "On" Or Confirm_Self = "on" Then
                                    Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & get_ExamYearSem1 & "' "
                                    Dim getLevel As String = oCommon.getFieldValue(level)

                                    If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD = oCommon.getFieldValue(tmpsql_SD_KOD)

                                    ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                        tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_GRED = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                        tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_PNG = oCommon.getFieldValue(tmpsql_SD_PNG)

                                        tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSSelfdevelopment_KOD = oCommon.getFieldValue(tmpsql_SD_KOD)
                                    End If
                                End If

                                If get_ExamYearSem1 = "2020" Then

                                    Test.Append("       <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value & "%) </b></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td></td>
                                                        </tr>")
                                Else
                                    If DSSelfdevelopment_PNG.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_GRED & " </b></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG) & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  In Progress  </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    End If
                                End If

                                ''Print PNG & PNGK 
                                Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

                                Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName & "' and year = '" & get_ExamYearSem1 & "'"
                                Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

                                If exist_png_data = "" Then
                                    exist_png_data = "0"
                                End If

                                If exist_pngs_data = "" Then
                                    exist_pngs_data = "0"
                                End If

                                Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
                                Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

                                Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> GPA </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & png_dec & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> CGPA </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & pngs_dec & " </b></td>
                                                        </tr>")
                                Test.AppendLine("   </table>")


                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''' SEMESTER 2 - SEMESTER 2 - SEMESTER 2 - SEMESTER 2 - SEMESTER 2 '''''''''''''''''''''''''''''''''''''''''''
                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                If j_examName + 1 <= 6 Then

                                    'get Portfolio percentage on / off
                                    check_portfolio_percen = "select stat_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

                                    ''get cocuricullum percentage on / off
                                    check_cocuricullum_percen = "select stat_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

                                    ''get research percentage on / off
                                    check_research_percen = "select stat_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Confirm_Research = oCommon.getFieldValue(check_research_percen)

                                    ''get self development percentage on / off
                                    check_self_percen = "select stat_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Confirm_Self = oCommon.getFieldValue(check_self_percen)


                                    ''print subject name 
                                    tmpSQL_Nama = " SELECT subject_Name FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQA_Sem2 As New SqlDataAdapter(tmpSQL_Nama, strConn)

                                    ''print subject code
                                    tmpSQL_Kod = "  SELECT subject_code FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQACODE_Sem2 As New SqlDataAdapter(tmpSQL_Kod, strConn)

                                    ''print subject grade
                                    tmpSQL_Gred = " SELECT grade FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQAGRADE_Sem2 As New SqlDataAdapter(tmpSQL_Gred, strConn)

                                    ''print subject png
                                    tmpSQL_PNG = "  SELECT gpa FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQAPNG_Sem2 As New SqlDataAdapter(tmpSQL_PNG, strConn)

                                    ''print subject credit hour
                                    tmpSQL_Hour = " SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQAHOUR_Sem2 As New SqlDataAdapter(tmpSQL_Hour, strConn)

                                    ''print subject credit hour
                                    tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' order by course_Name, subject_NameBM ASC"
                                    Dim SQATOTAL_Sem2 As New SqlDataAdapter(tmpSQL_Total, strConn)

                                    ''print total credit hour for subject taken
                                    tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    Dim total_Credit_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    ''print total PNG x credit hour for subject taken
                                    tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                    Dim total_Total_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    ''print academic percentage
                                    tmpSQL = "select komp_akademik from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim academic_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    Dim DS_Nama_Sem2 As New DataTable
                                    Dim DS_Kod_Sem2 As New DataTable
                                    Dim DS_Gred_Sem2 As New DataTable
                                    Dim DS_PNG_Sem2 As New DataTable
                                    Dim DS_Hour_Sem2 As New DataTable
                                    Dim DS_Total_Sem2 As New DataTable

                                    Try
                                        SQA_Sem2.Fill(DS_Nama_Sem2)
                                        SQACODE_Sem2.Fill(DS_Kod_Sem2)
                                        SQAPNG_Sem2.Fill(DS_PNG_Sem2)
                                        SQAGRADE_Sem2.Fill(DS_Gred_Sem2)
                                        SQAHOUR_Sem2.Fill(DS_Hour_Sem2)
                                        SQATOTAL_Sem2.Fill(DS_Total_Sem2)
                                    Catch ex As Exception
                                    End Try

                                    Dim DSSelfdevelopment_GRED_Sem2 As String = ""
                                    Dim DSSelfdevelopment_PNG_Sem2 As String = ""
                                    Dim DSSelfdevelopment_KOD_Sem2 As String = ""

                                    Dim DSEnglish_literature_GRED_Sem2 As New DataTable
                                    Dim DSEnglish_literature_PNG_Sem2 As New DataTable
                                    Dim DSEnglish_literature_KOD_Sem2 As New DataTable
                                    Dim DSEnglish_literature_HOUR_Sem2 As New DataTable
                                    Dim DSEnglish_literature_TOTAL_Sem2 As New DataTable

                                    Dim DSResearch_GRED_Sem2 As String = ""
                                    Dim DSResearch_PNG_Sem2 As String = ""
                                    Dim DSResearch_KOD_Sem2 As String = ""

                                    Dim DSPortfolio_GRED_Sem2 As String = ""
                                    Dim DSPortfolio_PNG_Sem2 As String = ""
                                    Dim DSPortfolio_KOD_Sem2 As String = ""

                                    Dim DSCocuricullum_KOD_SUKAN_Sem2 As New DataTable
                                    Dim DSCocuricullum_KOD_UNIFORM_Sem2 As New DataTable
                                    Dim DSCocuricullum_KOD_KELAB_Sem2 As New DataTable
                                    Dim DSCocuricullum_NAMA_SUKAN_Sem2 As New DataTable
                                    Dim DSCocuricullum_NAMA_UNIFORM_Sem2 As New DataTable
                                    Dim DSCocuricullum_NAMA_KELAB_Sem2 As New DataTable
                                    Dim DSCocurricullum_Gred_Sem2 As String = ""
                                    Dim DSCocurricullum_PNG_Sem2 As String = ""

                                    Dim total_Credit_EL_Sem2 As String = "0"
                                    Dim total_Total_EL_Sem2 As String = "0"

                                    tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim cocuricullum_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim portfolio_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim research_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)

                                    tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim sd_value_Sem2 As String = oCommon.getFieldValue(tmpSQL)


                                    Test.AppendLine("   <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:15px'>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='5' style='border-top:0.5px solid black; border-bottom:0.5px solid black; background-color:lightgray; font-size:9.5px; text-align:center'><b> ASSESSMENT " & get_ExamRename & get_SemesterName + 1 & ", ACADEMIC YEAR " & get_ExamYearSem1 & " </b></td>
                                                        </tr>
                                                    </table>

                                                    <table style='width:100%; font-family:Century Gothic; border-collapse: collapse; margin-top:5px'>
                                                        <tr style='font-size:9px;background-color:lightgray'>
                                                            <td style='width:20%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Component </b></td>
                                                            <td style='width:10%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Course Code </b></td>
                                                            <td style='width:35%;border-top:0.5px solid black; border-bottom:0.5px solid black;'><b> Course </b></td>
                                                            <td style='width:5%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> Grade </b></td>
                                                            <td style='width:5%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> GPA </b></td>
                                                            <td style='width:10%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> Credit Hour </b></td>
                                                            <td style='width:15%;border-top:0.5px solid black; border-bottom:0.5px solid black; text-align:center'><b> GPA x Credit Hour </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td rowspan='2'><b> Academic (" & academic_value_Sem2 & "%) </b></td>
                                                            <td ><b>")

                                    ''(column course code)
                                    For Each row As DataRow In DS_Kod_Sem2.Rows
                                        For Each column As DataColumn In DS_Kod_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td >")

                                    ''(column course name)
                                    For Each row As DataRow In DS_Nama_Sem2.Rows
                                        For Each column As DataColumn In DS_Nama_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'><b>")

                                    ''(column course grade)
                                    For Each row As DataRow In DS_Gred_Sem2.Rows
                                        For Each column As DataColumn In DS_Gred_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </b></td>
                                                            <td style='text-align:center;'>")

                                    ''(column course gpa )
                                    For Each row As DataRow In DS_PNG_Sem2.Rows
                                        For Each column As DataColumn In DS_PNG_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course credit hour)
                                    For Each row As DataRow In DS_Hour_Sem2.Rows
                                        For Each column As DataColumn In DS_Hour_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.Append("               </td>
                                                            <td style='text-align:center;'>")

                                    ''(column course total)
                                    For Each row As DataRow In DS_Total_Sem2.Rows
                                        For Each column As DataColumn In DS_Total_Sem2.Columns
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("<br />")
                                        Next
                                    Next

                                    Test.AppendLine("           </td>
                                                        </tr>")

                                    ''print english literature
                                    If Confirm_Eng_Literature = "On" Then

                                        Dim SQEnglish_Literature_GRED As String = ""
                                        Dim SQEnglish_Literature_PNG As String = ""
                                        Dim SQEnglish_Literature_KOD As String = ""
                                        Dim SQEnglish_Literature_HOUR As String = ""
                                        Dim SQEnglish_Literature_TOTAL As String = ""

                                        tmpsql_EL_GRED = "  SELECT grade FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_GRED = oCommon.getFieldValue(tmpsql_EL_GRED)

                                        tmpsql_EL_PNG = "   SELECT gpa FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_PNG = oCommon.getFieldValue(tmpsql_EL_PNG)

                                        tmpsql_EL_KOD = "   SELECT subject_code FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_KOD = oCommon.getFieldValue(tmpsql_EL_KOD)

                                        tmpsql_EL_HOUR = "  SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_HOUR = oCommon.getFieldValue(tmpsql_EL_HOUR)

                                        tmpsql_EL_TOTAL = " SELECT total FROM [ExamSlip_English_Literature] 
                                                        where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        SQEnglish_Literature_TOTAL = oCommon.getFieldValue(tmpsql_EL_TOTAL)

                                        If SQEnglish_Literature_GRED.Length > 0 Then
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                                <td style='border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_KOD & " </b></td>
                                                                <td style='border-bottom:0.5px solid black'> AP English Literature and Composition </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'><b> " & SQEnglish_Literature_GRED & " </b></td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_PNG & " </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_HOUR & " </td>
                                                                <td style='text-align:center; border-bottom:0.5px solid black'> " & SQEnglish_Literature_TOTAL & " </td>
                                                            </tr>")

                                            total_Credit_EL_Sem2 = SQEnglish_Literature_HOUR
                                            total_Total_EL_Sem2 = SQEnglish_Literature_TOTAL
                                        Else
                                            Test.AppendLine("   <tr style='font-size:9px;'>
                                                                    <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                                </tr>")
                                        End If

                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td colspan='6' style='border-bottom:0.5px solid black'><b></b></td>
                                                        </tr>")
                                    End If

                                    If total_Credit_Sem2 = "" Then
                                        total_Credit_Sem2 = "0"
                                    End If

                                    If total_Credit_EL_Sem2 = "" Then
                                        total_Credit_EL_Sem2 = "0"
                                    End If

                                    If total_Total_Sem2 = "" Then
                                        total_Total_Sem2 = "0"
                                    End If

                                    If total_Total_EL_Sem2 = "" Then
                                        total_Total_EL_Sem2 = "0"
                                    End If

                                    ''Calculatr Total Credit Hour , PNG x Credit Hour & PNG Academic
                                    Dim Number1_Sem2 As Double = Double.Parse(total_Credit_Sem2)
                                    Dim Number2_Sem2 As Double = Double.Parse(total_Credit_EL_Sem2)
                                    Dim Number3_Sem2 As Double = Double.Parse(total_Total_Sem2)
                                    Dim Number4_Sem2 As Double = Double.Parse(total_Total_EL_Sem2)

                                    Dim total_Hour_Sem2 As Double = Number1_Sem2 + Number2_Sem2
                                    Dim final_Total_Sem2 As Double = Number3_Sem2 + Number4_Sem2

                                    Dim PNG_Akademik_Sem2 As Double = Math.Round(final_Total_Sem2 / total_Hour_Sem2, 2)

                                    Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right'><b> Total </b></td>
                                                            <td style='text-align:center'><b> " & total_Hour_Sem2 & " </b></td>
                                                            <td style='text-align:center'><b> " & final_Total_Sem2 & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='5' style='text-align:right; border-bottom:0.5px solid black'><b> Academic GPA </b></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & PNG_Akademik_Sem2 & " </b></td>
                                                        </tr> ")

                                    '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then

                                        Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                                        Dim getStudent As String = oCommon.getFieldValue(studentData)

                                        If j_examName + 1 = 2 Or j_examName + 1 = 6 Then

                                            tmpsql_KOKO_PNG = " select koko_pelajar.PNGP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                            DSCocurricullum_PNG_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                            tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                            DSCocurricullum_Gred_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                            tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                            tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                            tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                            tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                            tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                            tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                            Dim SQCocuricullum_KOD_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                            Dim SQCocuricullum_KOD_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                            Dim SQCocuricullum_KOD_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                            Dim SQCocuricullum_NAMA_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                            Dim SQCocuricullum_NAMA_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                            Dim SQCocuricullum_NAMA_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                            Try
                                                SQCocuricullum_KOD_SUKAN_Sem2.Fill(DSCocuricullum_KOD_SUKAN_Sem2)
                                                SQCocuricullum_KOD_KELAB_Sem2.Fill(DSCocuricullum_KOD_KELAB_Sem2)
                                                SQCocuricullum_KOD_UNIFORM_Sem2.Fill(DSCocuricullum_KOD_UNIFORM_Sem2)
                                                SQCocuricullum_NAMA_SUKAN_Sem2.Fill(DSCocuricullum_NAMA_SUKAN_Sem2)
                                                SQCocuricullum_NAMA_KELAB_Sem2.Fill(DSCocuricullum_NAMA_KELAB_Sem2)
                                                SQCocuricullum_NAMA_UNIFORM_Sem2.Fill(DSCocuricullum_NAMA_UNIFORM_Sem2)
                                            Catch ex As Exception

                                            End Try

                                        ElseIf j_examName + 1 = 4 Then

                                            tmpsql_KOKO_PNG = " select koko_pelajar.PNGP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                            DSCocurricullum_PNG_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_PNG)

                                            tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                                            left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                            where Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                                            DSCocurricullum_Gred_Sem2 = oCommon.getFieldValue_Permata(tmpsql_KOKO_GRED)

                                            tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                            tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                                  left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                  left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                  where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                            tmpsql_KOKO_NAMA_UNIFORM = "    select koko_kolejpermata.NAMABI from koko_pelajar
                                                                      left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                      left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                      where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                            tmpsql_KOKO_KOD_SUKAN = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                                            tmpsql_KOKO_KOD_KELAB = "   select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                                            tmpsql_KOKO_KOD_UNIFORM = " select koko_kolejpermata.Kod from koko_pelajar
                                                                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                                    left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                                    where koko_pelajar.Tahun = '" & get_ExamYearSem1 & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & get_ExamYearSem1 & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                                            Dim SQCocuricullum_KOD_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                                            Dim SQCocuricullum_KOD_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                                            Dim SQCocuricullum_KOD_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                                            Dim SQCocuricullum_NAMA_SUKAN_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                                            Dim SQCocuricullum_NAMA_KELAB_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                                            Dim SQCocuricullum_NAMA_UNIFORM_Sem2 As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)

                                            Try
                                                SQCocuricullum_KOD_SUKAN_Sem2.Fill(DSCocuricullum_KOD_SUKAN_Sem2)
                                                SQCocuricullum_KOD_KELAB_Sem2.Fill(DSCocuricullum_KOD_KELAB_Sem2)
                                                SQCocuricullum_KOD_UNIFORM_Sem2.Fill(DSCocuricullum_KOD_UNIFORM_Sem2)
                                                SQCocuricullum_NAMA_SUKAN_Sem2.Fill(DSCocuricullum_NAMA_SUKAN_Sem2)
                                                SQCocuricullum_NAMA_KELAB_Sem2.Fill(DSCocuricullum_NAMA_KELAB_Sem2)
                                                SQCocuricullum_NAMA_UNIFORM_Sem2.Fill(DSCocuricullum_NAMA_UNIFORM_Sem2)
                                            Catch ex As Exception

                                            End Try

                                        End If
                                    End If

                                    Test.AppendLine("        <tr style='font-size:9px;'>
                                                            <td><b> Co-curriculum (" & cocuricullum_value_Sem2 & "%) </b></td>
                                                            <td><b>")


                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum kod sukan
                                        For Each row As DataRow In DSCocuricullum_KOD_SUKAN_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_SUKAN_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod kelab
                                        For Each row As DataRow In DSCocuricullum_KOD_KELAB_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_KELAB_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum kod uniform
                                        For Each row As DataRow In DSCocuricullum_KOD_UNIFORM_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("PKS317 <br />")

                                    ElseIf Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "off" Then
                                        Test.Append("<br />")
                                    End If

                                    Test.AppendLine("           </b></td>
                                                            <td>")

                                    If Confirm_Cocuricullum = "ON" Or Confirm_Cocuricullum = "On" Or Confirm_Cocuricullum = "on" Then
                                        ''kokorikulum nama skan
                                        For Each row As DataRow In DSCocuricullum_NAMA_SUKAN_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama kelab
                                        For Each row As DataRow In DSCocuricullum_NAMA_KELAB_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_KELAB_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next
                                        ''kokorikulum nama uniform
                                        For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM_Sem2.Rows
                                            For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM_Sem2.Columns
                                                Test.Append(row(column.ColumnName))
                                                Test.Append("<br />")
                                            Next
                                        Next

                                        Test.Append("Swimming <br />")

                                    ElseIf Confirm_Cocuricullum = "OFF" Or Confirm_Cocuricullum = "Off" Or Confirm_Cocuricullum = "off" Then
                                        Test.Append("<br />")
                                    End If

                                    If DSCocurricullum_PNG_Sem2.Length > 0 Then
                                        Test.AppendLine("       </td>
                                                            <td style='text-align:center'><b> " & DSCocurricullum_Gred_Sem2 & " </b></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG_Sem2) & " </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSCocurricullum_PNG_Sem2) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("       </td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If

                                    ''print Portfolio
                                    If Confirm_Portfolio = "ON" Or Confirm_Portfolio = "On" Or Confirm_Portfolio = "on" Then
                                        tmpsql_Portfolio_GRED = "   SELECT grade FROM [ExamSlip_Portfolio] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSPortfolio_GRED_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_GRED)

                                        tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSPortfolio_PNG_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_PNG)

                                        tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSPortfolio_KOD_Sem2 = oCommon.getFieldValue(tmpsql_Portfolio_KOD)

                                    End If

                                    If DSPortfolio_PNG_Sem2.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSPortfolio_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG_Sem2) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSPortfolio_PNG_Sem2) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Portfolio (" & portfolio_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSPortfolio_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'> </td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If

                                    ''print research 
                                    If Confirm_Research = "ON" Or Confirm_Research = "On" Or Confirm_Research = "on" Then
                                        tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSResearch_GRED_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_Gred)

                                        tmpsql_Penyelidikan_PNG = " SELECT gpa FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSResearch_PNG_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_PNG)

                                        tmpsql_Penyelidikan_KOD = " SELECT subject_code FROM [ExamSlip_Research] 
                                                                where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                        DSResearch_KOD_Sem2 = oCommon.getFieldValue(tmpsql_Penyelidikan_KOD)
                                    End If

                                    If DSResearch_PNG_Sem2.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'><b> " & DSResearch_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG_Sem2) & " </td>
                                                            <td></td>
                                                            <td style='text-align:center'> " & Double.Parse(DSResearch_PNG_Sem2) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Research (" & research_value_Sem2 & "%) </b></td>
                                                            <td><b> " & DSResearch_KOD_Sem2 & " </b></td>
                                                            <td></td>
                                                            <td style='text-align:center'></td>    
                                                            <td style='text-align:center'></td>
                                                            <td style='text-align:center'> In Progress </td>
                                                            <td style='text-align:center'></td>
                                                        </tr>")
                                    End If

                                    ''print self development
                                    If Confirm_Self = "ON" Or Confirm_Self = "On" Or Confirm_Self = "on" Then
                                        Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & get_ExamYearSem1 & "' "
                                        Dim getLevel As String = oCommon.getFieldValue(level)

                                        If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                                            tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_GRED_Sem2 = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                            tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_PNG_Sem2 = oCommon.getFieldValue(tmpsql_SD_PNG)

                                            tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_KOD_Sem2 = oCommon.getFieldValue(tmpsql_SD_KOD)

                                        ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                            tmpSQL_SD_GRED = "  SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_GRED_Sem2 = oCommon.getFieldValue(tmpSQL_SD_GRED)

                                            tmpsql_SD_PNG = "   SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_PNG_Sem2 = oCommon.getFieldValue(tmpsql_SD_PNG)

                                            tmpsql_SD_KOD = "   SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                            where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and exam_Year = '" & get_ExamYearSem1 & "' "
                                            DSSelfdevelopment_KOD_Sem2 = oCommon.getFieldValue(tmpsql_SD_KOD)
                                        End If
                                    End If

                                    If DSSelfdevelopment_PNG_Sem2.Length > 0 Then
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value_Sem2 & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD_Sem2 & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_GRED_Sem2 & " </b></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG_Sem2) & " </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'> " & Double.Parse(DSSelfdevelopment_PNG_Sem2) & " </td>
                                                        </tr>")
                                    Else
                                        Test.AppendLine("   <tr style='font-size:9px;'>
                                                            <td><b> Self Development (" & sd_value_Sem2 & "%) </b></td>
                                                            <td style='border-bottom:0.5px solid black'><b> " & DSSelfdevelopment_KOD_Sem2 & " </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'>  In Progress </td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'></td>
                                                        </tr>")
                                    End If

                                    ''Print PNG & PNGK 
                                    Dim check_png_exist_data_Sem2 As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim exist_png_data_Sem2 As String = oCommon.getFieldValue(check_png_exist_data_Sem2)

                                    Dim check_pngs_exist_data_Sem2 As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = 'Exam " & j_examName + 1 & "' and year = '" & get_ExamYearSem1 & "'"
                                    Dim exist_pngs_data_Sem2 As String = oCommon.getFieldValue(check_pngs_exist_data_Sem2)

                                    If exist_png_data_Sem2 = "" Then
                                        exist_png_data_Sem2 = "0"
                                    End If

                                    If exist_pngs_data_Sem2 = "" Then
                                        exist_pngs_data_Sem2 = "0"
                                    End If

                                    Dim png_dec_Sem2 As Decimal = Decimal.Parse(exist_png_data_Sem2)
                                    Dim pngs_dec_Sem2 As Decimal = Decimal.Parse(exist_pngs_data_Sem2)

                                    Test.AppendLine("       <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> GPA </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & png_dec_Sem2 & " </b></td>
                                                        </tr>
                                                        <tr style='font-size:9px;'>
                                                            <td colspan='4' style='text-align:center; border-bottom:0.5px solid black'></td>    
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> CGPA </b></td>
                                                            <td style='border-bottom:0.5px solid black'></td>
                                                            <td style='text-align:center; border-bottom:0.5px solid black'><b> " & pngs_dec_Sem2 & " </b></td>
                                                        </tr>")
                                    Test.AppendLine("   </table>")

                                End If

                                Dim find_printStatus As String = "Select Value from setting where idx = 'Examination' and Type = 'Print Date Status'"
                                Dim get_printStatus As String = oCommon.getFieldValue(find_printStatus)

                                Dim find_PrintDateJSC As String = "Select Value from setting where idx = 'Examination' and Type = 'Printing Date'"
                                Dim get_PrintDateJSC As String = oCommon.getFieldValue(find_PrintDateJSC)

                                If get_printStatus = "On" And get_printStatus = "ON" And get_printStatus = "on" Then

                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:50px; left:0px; font-family:Century Gothic;'>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:45%'></td>
                                                            <td style='width:35%;></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:45%'></td>
                                                            <td style='width:35%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%; text-align:left; color:red; font-size:9px'> Reference No : </td>    
                                                            <td style='width:45%; text-align:left; font-size:9px'> Print Date : </td>
                                                            <td style='width:35%'></td>
                                                        </tr>
                                                        <tr >
                                                            <td style='width:20%; text-align:left; color:red; font-size:10px'><b> " & get_StudentID & " </b></td>    
                                                            <td style='width:45%; text-align:left; font-size:9px'><b> " & get_PrintDateJSC & " <b></td>
                                                            <td style='width:35%'></td>
                                                        </tr>                                                        
                                                    </table>")

                                Else

                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:20px; left:0px; font-family:Century Gothic;'>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%'></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width:20%; text-align:left; color:red; font-size:9px'> Reference No : </td>    
                                                            <td style='width:80%'></td>
                                                        </tr>
                                                        <tr >
                                                            <td style='width:20%; text-align:left; color:red; font-size:10px'><b> " & get_StudentID & " </b></td>    
                                                            <td style='width:80%'></td>
                                                        </tr>                                                        
                                                    </table>")

                                End If

                                If j_examName = 5 Then
                                    Test.AppendLine("   <table style='width:100%; position:absolute; bottom:20px; left:0px; font-family:Century Gothic;'>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%;  border-top:1px dotted black'></td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Prof. Madya Dr Rorlinda binti Yusof </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Director, </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Pusat GENIUS@Pintar Negara </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                            <tr>
                                                                <td style='width:65%'></td>    
                                                                <td style='width:30%; text-align:center; font-size:9px'> Universiti Kebangsaan Malaysia </td>
                                                                <td style='width:5%'></td>
                                                            </tr>
                                                        </table>")
                                End If

                                Test.AppendLine("<div style='width:98%; position:absolute; bottom:0px; left:0px; font-family:Century Gothic;'> <p style='text-align:right; font-size:9px'>  " & countPage & " of 3 </p></div>")

                                Test.AppendLine("</div>")

                                countPage = countPage + 1
                            Next


                        End If
                    End If
                Next

            End If

            Test.AppendLine("</div> </div>")
            Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataOfficialTranscriptBI'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

            'print
            Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

        End If
    End Sub
End Class