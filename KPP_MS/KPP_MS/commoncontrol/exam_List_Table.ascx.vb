Imports System.Data.SqlClient

Public Class exam_List_Table
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "RE" Then ''Register Examination
                    txtbreadcrum1.Text = "Register Examination"

                    RegisterExamination.Visible = True
                    ViewExamination.Visible = False

                    btnRegisterExamination.Attributes("class") = "btn btn-info"
                    btnViewExamination.Attributes("class") = "btn btn-default font"

                    examName_List()
                    examYear_List()
                    InstitutionsList()

                ElseIf Session("getStatus") = "VE" Then ''View Examination
                    txtbreadcrum1.Text = "View Examination"

                    RegisterExamination.Visible = False
                    ViewExamination.Visible = True

                    btnRegisterExamination.Attributes("class") = "btn btn-default font"
                    btnViewExamination.Attributes("class") = "btn btn-info"

                    YearList()
                    ExaminationList()
                    InstitutionsList()

                    strRet = BindData(datRespondent)

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewExamination.Visible = False
        btnRegisterExamination.Visible = False
        ViewExamination.Visible = False
        RegisterExamination.Visible = False
        btnUpdate.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewExamination As String = ""
        Dim Get_RegisterExamination As String = ""

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


            If find_Data_SubMenu2 = "View Examination" And find_Data_SubMenu2.Length > 0 Then
                btnViewExamination.Visible = True
                ViewExamination.Visible = True

                Get_ViewExamination = "TRUE"

                If find_Data_F1Edit.Length > 0 And find_Data_F1Edit = "TRUE" Then
                    Session("getEditButton") = "TRUE"
                End If

                If find_Data_F1Delete.Length > 0 And find_Data_F1Delete = "TRUE" Then
                    Session("getDeleteButton") = "TRUE"
                End If
            End If

            If find_Data_SubMenu2 = "Register Examination" And find_Data_SubMenu2.Length > 0 Then
                btnRegisterExamination.Visible = True
                RegisterExamination.Visible = True

                Get_RegisterExamination = "TRUE"

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    btnUpdate.Visible = True
                End If
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewExamination.Visible = True
                ViewExamination.Visible = True
                btnRegisterExamination.Visible = True
                RegisterExamination.Visible = True
                btnUpdate.Visible = True

                Get_ViewExamination = "TRUE"
                Session("getEditButton") = "TRUE"
                Session("getDeleteButton") = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "RE" Or Session("getStatus") = "VE" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "RE" And Session("getStatus") <> "VE" Then
            If Get_ViewExamination = "TRUE" Then
                Data_If_Not_Group_Status = "VE"
            ElseIf Get_RegisterExamination = "TRUE" Then
                Data_If_Not_Group_Status = "RE"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewExamination = "TRUE" And Data_If_Not_Group_Status = "VE" Then
                Session("getStatus") = "VE"
            ElseIf Get_RegisterExamination = "TRUE" And Data_If_Not_Group_Status = "RE" Then
                Session("getStatus") = "RE"
            End If
        End If

    End Sub

    Private Sub btnRegisterExamination_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterExamination.ServerClick
        Session("getStatus") = "RE"
        Response.Redirect("admin_peperiksaan_pengurusan_peperiksaan.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewExamination_ServerClick(sender As Object, e As EventArgs) Handles btnViewExamination.ServerClick
        Session("getStatus") = "VE"
        Response.Redirect("admin_peperiksaan_pengurusan_peperiksaan.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub YearList()
        Try
            Dim stryear As String = "select distinct exam_year from exam_info order by exam_Year asc "
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddlYear.DataSource = yrds
            ddlYear.DataValueField = "exam_year"
            ddlYear.DataTextField = "exam_year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ExaminationList()
        Try
            Dim stryear As String = "select exam_Name from exam_info where exam_year = '" & ddlYear.SelectedValue & "' order by exam_Name asc"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddlExam.DataSource = yrds
            ddlExam.DataValueField = "exam_Name"
            ddlExam.DataTextField = "exam_Name"
            ddlExam.DataBind()
            ddlExam.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub InstitutionsList()
        Try
            Dim stryear As String = "select Parameter, Value from setting where Type = 'Pusat Campus' order by parameter asc"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddlInstitutions.DataSource = yrds
            ddlInstitutions.DataValueField = "Value"
            ddlInstitutions.DataTextField = "Parameter"
            ddlInstitutions.DataBind()
            ddlInstitutions.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

            ddlExamInstitutions.DataSource = yrds
            ddlExamInstitutions.DataValueField = "Value"
            ddlExamInstitutions.DataTextField = "Parameter"
            ddlExamInstitutions.DataBind()
            ddlExamInstitutions.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY exam_Year, exam_Name, exam_institutions ASC"

        tmpSQL = "Select * From exam_Info"
        strWhere += " WHERE exam_ID IS NOT NULL and exam_year = '" & ddlYear.SelectedValue & "'"

        If ddlExam.SelectedIndex > 0 Then
            strWhere += " AND exam_Name = '" & ddlExam.SelectedValue & "'"
        End If

        If ddlInstitutions.SelectedIndex > 0 Then
            strWhere += " AND (exam_Institutions = '" & ddlInstitutions.SelectedValue & "' or exam_Institutions = 'ALL')"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyCode As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)

            ''delete exam info
            Dim Dlt_NewCourse As New SqlDataAdapter()
            Dim dlt_Course As String
            Dlt_NewCourse.SelectCommand = New SqlCommand()
            Dlt_NewCourse.SelectCommand.Connection = MyConnection
            Dlt_NewCourse.SelectCommand.CommandText = "delete exam_Info where exam_ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Course = Dlt_NewCourse.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            '' delete exam result related to that exam info id 
            Dim Dlt_NewData As New SqlDataAdapter()
            Dim dlt_Data As String
            Dlt_NewData.SelectCommand = New SqlCommand()
            Dlt_NewData.SelectCommand.Connection = MyConnection
            Dlt_NewData.SelectCommand.CommandText = "delete exam_result where exam_ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Data = Dlt_NewData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyCode As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_exam_data.aspx?exam_ID=" + strKeyCode + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            ExaminationList()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlExam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExam.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlInstitutions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInstitutions.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub examName_List()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Exam' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamName.DataSource = ds
            ddlExamName.DataTextField = "Parameter"
            ddlExamName.DataValueField = "Parameter"
            ddlExamName.DataBind()
            ddlExamName.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub examYear_List()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "Parameter"
            ddlExamYear.DataValueField = "Parameter"
            ddlExamYear.DataBind()
            ddlExamYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick

        Dim check_Existed As Integer = 0
        Dim get_Level_Query As String = ""

        If ddlExamName.SelectedIndex > 0 Then

            If txtExamCode.Text.Length > 0 Then

                If ddlExamYear.SelectedIndex > 0 Then

                    If txtStartDate.Text <> "" And Regex.IsMatch(txtStartDate.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then

                        If txtEndDate.Text <> "" And Regex.IsMatch(txtEndDate.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then

                            If ddlExamInstitutions.SelectedIndex > 0 Then

                                ''checking if the data is exist
                                strSQL = "Select exam_ID from exam_info where exam_Year = '" & ddlExamYear.SelectedValue & "' and exam_Name = '" & ddlExamName.SelectedValue & "' and (exam_Institutions = '" & ddlExamInstitutions.SelectedValue & "' or exam_Institutions = 'ALL')"
                                strRet = oCommon.getFieldValue(strSQL)

                                If strRet.Length = 0 Then

                                    'Insert new exam in exam_Info
                                    Using PJGDATA As New SqlCommand("INSERT into exam_Info(exam_Name,exam_Code,exam_Year,exam_StartDate,exam_EndDate,exam_Institutions) values ('" & ddlExamName.SelectedValue & "','" & txtExamCode.Text & "','" & ddlExamYear.SelectedValue & "','" & txtStartDate.Text & "','" & txtEndDate.Text & "','" & ddlExamInstitutions.SelectedValue & "')", objConn)
                                        objConn.Open()
                                        Dim i = PJGDATA.ExecuteNonQuery()
                                        objConn.Close()
                                        If i <> 0 Then
                                            ShowMessage("Successfull register new exam", MessageType.Success)
                                        Else
                                            ShowMessage("Unsuccessfull register new exam", MessageType.Error)
                                        End If
                                    End Using

                                    check_Existed = 1
                                Else
                                    check_Existed = 0
                                    ShowMessage(ddlExamName.SelectedValue & " for year " & ddlExamYear.SelectedValue & " had been registered", MessageType.Error)
                                End If

                                If check_Existed = 1 Then

                                    ''get query for subject level and subject sem
                                    If ddlExamName.SelectedValue = "Exam 1" Or ddlExamName.SelectedValue = "Exam 2" Then

                                        get_Level_Query = " (subject_info.subject_StudentYear = 'Foundation 1' or subject_info.subject_StudentYear = 'Level 1') and subject_info.subject_sem = 'Sem 1' "

                                    ElseIf ddlExamName.SelectedValue = "Exam 3" Or ddlExamName.SelectedValue = "Exam 4" Then

                                        get_Level_Query = " (subject_info.subject_StudentYear = 'Foundation 1' or subject_info.subject_StudentYear = 'Level 1') and subject_info.subject_sem = 'Sem 2' "

                                    ElseIf ddlExamName.SelectedValue = "Exam 5" Or ddlExamName.SelectedValue = "Exam 6" Then

                                        get_Level_Query = " (subject_info.subject_StudentYear = 'Foundation 2' or subject_info.subject_StudentYear = 'Level 2') and subject_info.subject_sem = 'Sem 1' "

                                    ElseIf ddlExamName.SelectedValue = "Exam 7" Then

                                        get_Level_Query = " subject_info.subject_StudentYear = 'Foundation 2' and subject_info.subject_sem = 'Sem 2' "

                                    ElseIf ddlExamName.SelectedValue = "Exam 8" Then

                                        get_Level_Query = " subject_info.subject_StudentYear = 'Foundation 2' and subject_info.subject_sem = 'Sem 2' "

                                    ElseIf ddlExamName.SelectedValue = "Exam 9" Or ddlExamName.SelectedValue = "Exam 10" Then

                                        get_Level_Query = " subject_info.subject_StudentYear = 'Foundation 3' and subject_info.subject_sem = 'Sem 1' "

                                    ElseIf ddlExamName.SelectedValue = "Exam 11" Or ddlExamName.SelectedValue = "Exam 12" Then

                                        get_Level_Query = " subject_info.subject_StudentYear = 'Foundation 3' and subject_info.subject_sem = 'Sem 2' "

                                    End If

                                    'Insert student taking exam at exam_result
                                    Using PJGDATA As New SqlCommand("   INSERT into exam_result(exam_ID,course_ID) 
                                                                        select '" & strRet & "',course_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                                        where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and " & get_Level_Query & "
                                                                        and subject_info.subject_year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "'  
                                                                        order by course_ID ASC ", objConn)
                                        objConn.Open()
                                        Dim j = PJGDATA.ExecuteNonQuery()
                                        objConn.Close()
                                    End Using

                                    ''get exam result id for subject self development (F1,F2,F3)
                                    Dim examID_selfdevelopment As String = " select A.ID from exam_result A
                                                                             left join course B on A.course_ID = B.course_ID
                                                                             left join subject_info C on B.subject_Id = C.subject_id
                                                                             left join exam_info D on A.exam_ID = D.exam_ID
                                                                             where B.year = '" & ddlExamYear.SelectedValue & "' and C.subject_year = '" & ddlExamYear.SelectedValue & "'
                                                                             and D.exam_Year = '" & ddlExamYear.SelectedValue & "' and D.exam_Name = '" & ddlExamName.SelectedValue & "'
                                                                             and D.exam_Institutions = '" & ddlExamInstitutions.SelectedValue & "' and C.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "'
                                                                             and C.course_Name = 'Pembangunan Kendiri'"
                                    Dim ID_selfdevelpment As String = oCommon.getFieldValue(examID_selfdevelopment)

                                    If ID_selfdevelpment.Length > 0 Then
                                        strSQL = "  Insert into self_development_mark(examID,courseID,year)
                                                    select D.exam_ID, B.course_ID, '" & ddlExamYear.SelectedValue & "' from exam_result A
                                                    left join course B on A.course_ID = B.course_ID
                                                    left join subject_info C on B.subject_Id = C.subject_id
                                                    left join exam_info D on A.exam_ID = D.exam_ID
                                                    where B.year = '" & ddlExamYear.SelectedValue & "' and C.subject_year = '" & ddlExamYear.SelectedValue & "' and D.exam_Year = '" & ddlExamYear.SelectedValue & "' and D.exam_Institutions = '" & ddlExamInstitutions.SelectedValue & "'
                                                    and D.exam_Name = '" & ddlExamName.SelectedValue & "' and C.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "'  and C.course_Name = 'Pembangunan Kendiri'"
                                        strRet = oCommon.ExecuteSQL(strSQL)
                                    End If

                                    ''get exam result id for subject personality development (L1,L2)
                                    Dim examID_personalitydevelopment As String = " select A.ID from exam_result A
                                                                                    left join course B on A.course_ID = B.course_ID
                                                                                    left join subject_info C on B.subject_Id = C.subject_id
                                                                                    left join exam_info D on A.exam_ID = D.exam_ID
                                                                                    where B.year = '" & ddlExamYear.SelectedValue & "' and C.subject_year = '" & ddlExamYear.SelectedValue & "'
                                                                                    and D.exam_Year = '" & ddlExamYear.SelectedValue & "' and D.exam_Name = '" & ddlExamName.SelectedValue & "'
                                                                                    and D.exam_Institutions = '" & ddlExamInstitutions.SelectedValue & "' and C.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "'
                                                                                    and C.course_Name = 'Jati Diri'"
                                    Dim ID_personalitydevelpment As String = oCommon.getFieldValue(examID_selfdevelopment)

                                    If ID_personalitydevelpment.Length > 0 Then
                                        strSQL = "  Insert into personality_development_mark(examID,courseID,year)
                                                    select D.exam_ID, B.course_ID, '" & ddlExamYear.SelectedValue & "' from exam_result A
                                                    left join course B on A.course_ID = B.course_ID
                                                    left join subject_info C on B.subject_Id = C.subject_id
                                                    left join exam_info D on A.exam_ID = D.exam_ID
                                                    where B.year = '" & ddlExamYear.SelectedValue & "' and C.subject_year = '" & ddlExamYear.SelectedValue & "' and D.exam_Year = '" & ddlExamYear.SelectedValue & "' and D.exam_Institutions = '" & ddlExamInstitutions.SelectedValue & "'
                                                    and D.exam_Name = '" & ddlExamName.SelectedValue & "' and C.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' and C.course_Name = 'Jati Diri'"
                                        strRet = oCommon.ExecuteSQL(strSQL)
                                    End If

                                    ''get exam result id for subject portfolio (F1,F2,F3,L1,L2)
                                    Dim examID_portfolio As String = "  select A.ID from exam_result A
                                                                        left join course B on A.course_ID = B.course_ID
                                                                        left join subject_info C on B.subject_Id = C.subject_id
                                                                        left join exam_info D on A.exam_ID = D.exam_ID
                                                                        where B.year = '" & ddlExamYear.SelectedValue & "' and C.subject_year = '" & ddlExamYear.SelectedValue & "'
                                                                        and D.exam_Year = '" & ddlExamYear.SelectedValue & "' and D.exam_Name = '" & ddlExamName.SelectedValue & "'
                                                                        and D.exam_Institutions = '" & ddlExamInstitutions.SelectedValue & "' and C.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "'
                                                                        and C.course_Name = 'Portfolio'"
                                    Dim ID_portfolio As String = getFieldValue(examID_portfolio, strConn)

                                    If ID_portfolio.Length > 0 Then
                                        strSQL = "  Insert into portfolio_mark(examresult_id,year)
                                                    select A.ID, '" & ddlExamYear.SelectedValue & "' from exam_result A
                                                    left join course B on A.course_ID = B.course_ID
                                                    left join subject_info C on B.subject_Id = C.subject_id
                                                    left join exam_info D on A.exam_ID = D.exam_ID
                                                    where B.year = '" & ddlExamYear.SelectedValue & "' and C.subject_year = '" & ddlExamYear.SelectedValue & "' and D.exam_Year = '" & ddlExamYear.SelectedValue & "'and D.exam_Year = '" & ddlExamYear.SelectedValue & "' and D.exam_Institutions = '" & ddlExamInstitutions.SelectedValue & "'
                                                    and D.exam_Name = '" & ddlExamName.SelectedValue & "' and C.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' and C.course_Name = 'Portfolio'"
                                        strRet = oCommon.ExecuteSQL(strSQL)
                                    End If

                                    ''Insert into exam create 
                                    Insert_studentPng_Data()

                                    txtExamCode.Text = ""
                                    txtStartDate.Text = ""
                                    txtEndDate.Text = ""
                                End If
                            Else
                                ShowMessage("Please select institutions", MessageType.Error)
                            End If
                        Else
                            ShowMessage("Please enter a valid exam end date", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please enter a valid exam start date", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please select exam year", MessageType.Error)
                End If
            Else
                ShowMessage("Please enter a valid exam code", MessageType.Error)
            End If
        Else
            ShowMessage("Please select exam name", MessageType.Error)
        End If
    End Sub


    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Sub Insert_studentPng_Data()

        If ddlExamName.SelectedValue = "Exam 1" Or ddlExamName.SelectedValue = "Exam 2" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'ASAS' from course left join subject_info on course.subject_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 1' and subject_info.subject_sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

            Using STDDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'TAHAP' from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Level 1' and subject_info.subject_sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim K = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 3" Or ddlExamName.SelectedValue = "Exam 4" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                            select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'ASAS' from course left join subject_info on course.subject_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 1' and subject_info.subject_sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

            Using STDDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                            select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'TAHAP' from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Level 1' and subject_info.subject_sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim K = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 5" Or ddlExamName.SelectedValue = "Exam 6" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0','ASAS' from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 2' and subject_info.subject_sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

            Using STDDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                            select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'TAHAP' from course left join subject_info on course.subject_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Level 2' and subject_info.subject_sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim K = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 7" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0','ASAS' from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 2' and subject_info.subject_sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 8" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0','ASAS' from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 2' and subject_info.subject_sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 9" Or ddlExamName.SelectedValue = "Exam 10" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                              select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'ASAS' from course left join subject_info on course.subject_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 3' and subject_info.subject_sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 11" Or ddlExamName.SelectedValue = "Exam 12" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'ASAS' from course left join subject_info on course.subject_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 3' and subject_info.subject_sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_Campus = '" & ddlExamInstitutions.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        End If
    End Sub

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

End Class
