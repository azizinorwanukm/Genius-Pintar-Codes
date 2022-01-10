Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class admin_alumni1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'Checking_MenuAccess_Load()

                If Session("getStatus") = "AI" Then ''Alumni Information
                    txtbreadcrum1.Text = "Alumni Information"

                    AlumniInfo.Visible = True
                    EducationalInfo.Visible = False
                    ProfessionalInfo.Visible = False

                    btnAlumniInformation.Attributes("class") = "btn btn-info"
                    btnAlumniEducation.Attributes("class") = "btn btn-default"
                    btnAlumniCompany.Attributes("class") = "btn btn-default"

                    Alumni_Year()
                    Alumni_Campus()
                    Alumni_Program()
                    Alumni_Batch()

                    strRet = BindDataAI(datRespondentAI)
                    load_DateSetting()

                ElseIf Session("getStatus") = "EI" Then ''Educational Information
                    txtbreadcrum1.Text = "Educational Information"

                    AlumniInfo.Visible = False
                    EducationalInfo.Visible = True
                    ProfessionalInfo.Visible = False

                    btnAlumniInformation.Attributes("class") = "btn btn-default"
                    btnAlumniEducation.Attributes("class") = "btn btn-info"
                    btnAlumniCompany.Attributes("class") = "btn btn-default"

                    'strRet = BindDataOfficial(TranscriptRespondent)

                ElseIf Session("getStatus") = "PI" Then ''Professional Information
                    txtbreadcrum1.Text = "Professional Information"

                    AlumniInfo.Visible = False
                    EducationalInfo.Visible = False
                    ProfessionalInfo.Visible = True

                    btnAlumniInformation.Attributes("class") = "btn btn-default"
                    btnAlumniEducation.Attributes("class") = "btn btn-default"
                    btnAlumniCompany.Attributes("class") = "btn btn-info"


                    'strRet = BindDataOfficial(TranscriptRespondent)

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub Checking_MenuAccess_Load()

    '    btnAlumniInformation.Visible = False
    '    btnAlumniEducation.Visible = False
    '    btnAlumniCompany.Visible = False
    '    AlumniInfo.Visible = False
    '    EducationalInfo.Visible = False
    '    ProfessionalInfo.Visible = False

    '    Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
    '    Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

    '    Dim str_user_position As String = CType(Session.Item("user_position"), String)

    '    ''Get Login ID from Staff_Login
    '    strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
    '    Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

    '    ''Get Count from Menu_master_User
    '    strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
    '    Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

    '    Dim Get_ExaminationTranscript As String = ""
    '    Dim Get_OfficialTranscript As String = ""

    '    ''Loop The Count_No
    '    For num As Integer = 0 To find_CountNo_LoginID - 1 Step 1

    '        ''Get Main Menu Data
    '        strSQL = "  Select A.Menu From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
    '                    Order By A.MenuID Asc
    '                    Offset " & num & " Rows Fetch Next 1 Rows Only"
    '        Dim find_Data_Menu_Data As String = oCommon.getFieldValue(strSQL)

    '        ''Get Sub Menu 2 Data
    '        strSQL = "  Select A.Menu_Sub2 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
    '                    Order By A.MenuID Asc
    '                    Offset " & num & " Rows Fetch Next 1 Rows Only"
    '        Dim find_Data_SubMenu2 As String = oCommon.getFieldValue(strSQL)

    '        ''Get Function Button 1 Print In BI Data 
    '        strSQL = "  Select B.F1_PrintInBI From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
    '                    Order By A.MenuID Asc
    '                    Offset " & num & " Rows Fetch Next 1 Rows Only"
    '        Dim find_Data_F1PrintInBI As String = oCommon.getFieldValue(strSQL)

    '        ''Get Function Button 1 Print In BM Data 
    '        strSQL = "  Select B.F1_PrintInBM From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
    '                    Order By A.MenuID Asc
    '                    Offset " & num & " Rows Fetch Next 1 Rows Only"
    '        Dim find_Data_F1PrintInBM As String = oCommon.getFieldValue(strSQL)

    '        ''Get Function Button 1 Generate GPA & CGPA Data 
    '        strSQL = "  Select B.F1_GenerateGPACGPA From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
    '                    Order By A.MenuID Asc
    '                    Offset " & num & " Rows Fetch Next 1 Rows Only"
    '        Dim find_Data_F1GenerateGPACGPA As String = oCommon.getFieldValue(strSQL)


    '        If find_Data_SubMenu2 = "Current Examination Transcript" And find_Data_SubMenu2.Length > 0 Then
    '            btnExaminationTranscript.Visible = True
    '            ExaminationTranscript.Visible = True

    '            Get_ExaminationTranscript = "TRUE"

    '            If find_Data_F1GenerateGPACGPA.Length > 0 And find_Data_F1GenerateGPACGPA = "TRUE" Then
    '                BtnGenerate.Visible = True
    '            End If

    '            If find_Data_F1PrintInBI.Length > 0 And find_Data_F1PrintInBI = "TRUE" Then
    '                BtnPrintkoko.Visible = True
    '            End If

    '            If find_Data_F1PrintInBM.Length > 0 And find_Data_F1PrintInBM = "TRUE" Then
    '                BtnPrintkokoBM.Visible = True
    '            End If
    '        End If

    '        If find_Data_SubMenu2 = "Official Transcript" And find_Data_SubMenu2.Length > 0 Then
    '            btnOfficialTranscript.Visible = True
    '            OfficialTranscript.Visible = True

    '            Get_OfficialTranscript = "TRUE"

    '            If find_Data_F1PrintInBI.Length > 0 And find_Data_F1PrintInBI = "TRUE" Then
    '                btnOfficialBI.Visible = True
    '            End If

    '            If find_Data_F1PrintInBM.Length > 0 And find_Data_F1PrintInBM = "TRUE" Then
    '                btnOfficialBM.Visible = True
    '            End If
    '        End If

    '        If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
    '            btnExaminationTranscript.Visible = True
    '            btnOfficialTranscript.Visible = True
    '            ExaminationTranscript.Visible = True
    '            OfficialTranscript.Visible = True

    '            BtnGenerate.Visible = True
    '            BtnPrintkoko.Visible = True
    '            BtnPrintkokoBM.Visible = True
    '            btnOfficialBI.Visible = True
    '            btnOfficialBI.Visible = True

    '            Get_ExaminationTranscript = "TRUE"
    '        End If

    '    Next

    '    Dim Data_If_Not_Group_Status As String = ""
    '    Session("getStatus_Temporary") = ""

    '    If Session("getStatus") = "CT" Or Session("getStatus") = "OT" Then
    '        Data_If_Not_Group_Status = Session("getStatus")
    '    End If

    '    If Session("getStatus") <> "CT" And Session("getStatus") <> "OT" Then
    '        If Get_ExaminationTranscript = "TRUE" Then
    '            Data_If_Not_Group_Status = "CT"
    '        ElseIf Get_OfficialTranscript = "TRUE" Then
    '            Data_If_Not_Group_Status = "OT"
    '        End If
    '    End If

    '    If Session("getStatus_Temporary") IsNot Nothing Then
    '        If Get_ExaminationTranscript = "TRUE" And Data_If_Not_Group_Status = "CT" Then
    '            Session("getStatus") = "CT"
    '        ElseIf Get_OfficialTranscript = "TRUE" And Data_If_Not_Group_Status = "OT" Then
    '            Session("getStatus") = "OT"
    '        End If
    '    End If

    'End Sub

    Private Sub btnAlumniInformation_ServerClick(sender As Object, e As EventArgs) Handles btnAlumniInformation.ServerClick
        Session("getStatus") = "AI"
        Response.Redirect("admin_alumni.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnAlumniEducation_ServerClick(sender As Object, e As EventArgs) Handles btnAlumniEducation.ServerClick
        Session("getStatus") = "EI"
        Response.Redirect("admin_alumni.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnAlumniCompany_ServerClick(sender As Object, e As EventArgs) Handles btnAlumniCompany.ServerClick
        Session("getStatus") = "PI"
        Response.Redirect("admin_alumni.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub load_DateSetting()

        Dim find_SD As String = "Select Value from Setting where idx = 'alumni' and type = 'Date Setting' and Parameter = 'Start Date'"
        Dim get_SD As String = oCommon.getFieldValue(find_SD)

        Dim find_ED As String = "Select Value from Setting where idx = 'alumni' and type = 'Date Setting' and Parameter = 'End Date'"
        Dim get_ED As String = oCommon.getFieldValue(find_ED)

        txtDateStart.Text = get_SD
        txtDateEnd.Text = get_ED

    End Sub

    Private Sub Alumni_Year()

        strSQL = "  select distinct B.year from student_info A left join student_Level B on A.std_ID = B.std_ID
                    where A.student_ID like '%M%' and A.student_Status = 'Graduate' and B.Registered = 'Yes' order by B.year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAI_Year.DataSource = ds
            ddlAI_Year.DataTextField = "year"
            ddlAI_Year.DataValueField = "year"
            ddlAI_Year.DataBind()
            ddlAI_Year.Items.Insert(0, New ListItem("Select Graduate Year", String.Empty))

            ddlEI_Year.DataSource = ds
            ddlAI_Year.DataTextField = "year"
            ddlAI_Year.DataValueField = "year"
            ddlAI_Year.DataBind()
            ddlAI_Year.Items.Insert(0, New ListItem("Select Graduate Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Alumni_Campus()

        strSQL = "select * from setting where type = 'pusat campus' order by parameter asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAI_Campus.DataSource = ds
            ddlAI_Campus.DataTextField = "Parameter"
            ddlAI_Campus.DataValueField = "Value"
            ddlAI_Campus.DataBind()
            ddlAI_Campus.Items.Insert(0, New ListItem("Select Campus", String.Empty))

            ddlEI_Campus.DataSource = ds
            ddlEI_Campus.DataTextField = "Parameter"
            ddlEI_Campus.DataValueField = "Value"
            ddlEI_Campus.DataBind()
            ddlEI_Campus.Items.Insert(0, New ListItem("Select Campus", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Alumni_Program()

        strSQL = "select * from setting where type = 'stream' order by parameter asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAI_Program.DataSource = ds
            ddlAI_Program.DataTextField = "Parameter"
            ddlAI_Program.DataValueField = "Value"
            ddlAI_Program.DataBind()
            ddlAI_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))

            ddlEI_Program.DataSource = ds
            ddlEI_Program.DataTextField = "Parameter"
            ddlEI_Program.DataValueField = "Value"
            ddlEI_Program.DataBind()
            ddlEI_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Alumni_Batch()

        strSQL = "Select distinct student_Batch from student_info where student_ID like '%M%' and student_Status <> 'Block' order by student_batch asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAI_Batch.DataSource = ds
            ddlAI_Batch.DataTextField = "student_Batch"
            ddlAI_Batch.DataValueField = "student_Batch"
            ddlAI_Batch.DataBind()
            ddlAI_Batch.Items.Insert(0, New ListItem("Select Alumni Batch", String.Empty))

            ddlEI_Batch.DataSource = ds
            ddlEI_Batch.DataTextField = "student_Batch"
            ddlEI_Batch.DataValueField = "student_Batch"
            ddlEI_Batch.DataBind()
            ddlEI_Batch.Items.Insert(0, New ListItem("Select Alumni Batch", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataAI(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLAI, strConn)
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

    Private Function getSQLAI() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = "    group by A.std_ID, A.student_Name, A.student_ID, A.student_Batch, student_Stream 
                                        order by Graduate_Year, A.student_Name"

        tmpSQL = "  Select A.std_ID, A.student_Name, A.student_ID, A.student_Batch, A.student_Stream,
                    (Select MIN(student_Level.year) from student_Level where student_Level.std_ID = A.std_ID) as Start_Year, 
                    (Select MAX(student_Level.year) from student_Level where student_Level.std_ID = A.std_ID) as Graduate_Year
                    from student_info A
                    left join student_level B on A.std_Id = B.std_ID"
        strWhere = " where A.student_ID like '%M%' and A.student_Status = 'Graduate'"

        If txtAlumniName.Text.Length > 0 Then
            strWhere += " and A.student_Name like '%" & txtAlumniName.Text & "%'"
        End If

        If ddlAI_Campus.SelectedIndex > 0 Then
            strWhere += " and A.student_Campus = '" & ddlAI_Campus.SelectedValue & "'"
        End If

        If ddlAI_Year.SelectedIndex > 0 Then
            strWhere += " and B.year = '" & ddlAI_Year.SelectedValue & "'"
        End If

        If ddlAI_Program.SelectedIndex > 0 Then
            strWhere += " and A.student_Stream = '" & ddlAI_Program.SelectedValue & "'"
        End If

        If ddlAI_Batch.SelectedIndex > 0 Then
            strWhere += " and A.student_Batch = '" & ddlAI_Batch.SelectedValue & "'"
        End If

        getSQLAI = tmpSQL & strWhere & strOrderby

        Return getSQLAI
    End Function

    Protected Sub ddlAI_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAI_Year.SelectedIndexChanged
        Try
            strRet = BindDataAI(datRespondentAI)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAI_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAI_Campus.SelectedIndexChanged
        Try
            strRet = BindDataAI(datRespondentAI)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAI_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAI_Program.SelectedIndexChanged
        Try
            strRet = BindDataAI(datRespondentAI)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlAI_Batch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAI_Batch.SelectedIndexChanged
        Try
            strRet = BindDataAI(datRespondentAI)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        strRet = BindDataAI(datRespondentAI)
    End Sub

    Private Sub btnDateSetting_Alumni_ServerClick(sender As Object, e As EventArgs) Handles btnDateSetting_Alumni.ServerClick

        If txtDateStart.Text.Length = 0 Then
            ShowMessage(" Please Fill In Start Date", MessageType.Error)
            Exit Sub
        End If

        If txtDateEnd.Text.Length = 0 Then
            ShowMessage(" Please Fill In End Date", MessageType.Error)
            Exit Sub
        End If

        strSQL = "Update setting set Value = '" & txtDateStart.Text & "' where idx = 'alumni' and type = 'Date Setting' and Parameter = 'Start Date'"
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "Update setting set Value = '" & txtDateEnd.Text & "' where idx = 'alumni' and type = 'Date Setting' and Parameter = 'End Date'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage(" Successful Update Alumni Date Setting", MessageType.Success)
        Else
            ShowMessage(" Uncessful Update Alumni Date Setting", MessageType.Error)
        End If
    End Sub

    Private Function BindDataEI(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLEI, strConn)
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

    Private Function getSQLEI() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = "    group by A.std_ID, A.student_Name, A.student_ID, A.student_Batch, student_Stream 
                                        order by Graduate_Year, A.student_Name"

        tmpSQL = "  Select A.std_ID, A.student_Name, A.student_ID, A.student_Batch, A.student_Stream,
                    (Select MIN(student_Level.year) from student_Level where student_Level.std_ID = A.std_ID) as Start_Year, 
                    (Select MAX(student_Level.year) from student_Level where student_Level.std_ID = A.std_ID) as Graduate_Year
                    from student_info A
                    left join student_level B on A.std_Id = B.std_ID"
        strWhere = " where A.student_ID like '%M%' and A.student_Status = 'Graduate'"

        If txtAlumniName.Text.Length > 0 Then
            strWhere += " and A.student_Name like '%" & txtAlumniName.Text & "%'"
        End If

        If ddlAI_Campus.SelectedIndex > 0 Then
            strWhere += " and A.student_Campus = '" & ddlAI_Campus.SelectedValue & "'"
        End If

        If ddlAI_Year.SelectedIndex > 0 Then
            strWhere += " and B.year = '" & ddlAI_Year.SelectedValue & "'"
        End If

        If ddlAI_Program.SelectedIndex > 0 Then
            strWhere += " and A.student_Stream = '" & ddlAI_Program.SelectedValue & "'"
        End If

        If ddlAI_Batch.SelectedIndex > 0 Then
            strWhere += " and A.student_Batch = '" & ddlAI_Batch.SelectedValue & "'"
        End If

        getSQLEI = tmpSQL & strWhere & strOrderby

        Return getSQLEI
    End Function

    Protected Sub ddlEI_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEI_Year.SelectedIndexChanged
        Try
            strRet = BindDataEI(datRespondentEI)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlEI_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEI_Campus.SelectedIndexChanged
        Try
            strRet = BindDataEI(datRespondentEI)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlEI_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEI_Program.SelectedIndexChanged
        Try
            strRet = BindDataEI(datRespondentEI)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlEI_Batch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEI_Batch.SelectedIndexChanged
        Try
            strRet = BindDataEI(datRespondentEI)
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