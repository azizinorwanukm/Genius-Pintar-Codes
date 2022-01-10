Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class report_UKM1_PPCS
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
    Dim strConnUKM3 As String = ConfigurationManager.AppSettings("ConnectionUKM3")
    Dim objConnUKM3 As SqlConnection = New SqlConnection(strConnUKM3)

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "UKM 1R" Then ''UKM1 Report
                    txtbreadcrum1.Text = "UKM1 Report"

                    ViewUKM1Report.Visible = True
                    ViewUKM2Report.Visible = False
                    ViewPPCSReport.Visible = False
                    ViewUKM3Report.Visible = False

                    btnViewStudent_UKM1.Attributes("class") = "btn btn-info"
                    btnViewStudent_UKM2.Attributes("class") = "btn btn-default font"
                    btnViewStudent_PPCS.Attributes("class") = "btn btn-default font"
                    btnViewStudent_UKM3.Attributes("class") = "btn btn-default font"

                    YearListUKM1()
                    LevelListUKM1()
                    SemesterListUKM1()

                ElseIf Session("getStatus") = "UKM 2R" Then ''UKM2 Report
                    txtbreadcrum1.Text = "UKM2 Report"

                    ViewUKM1Report.Visible = False
                    ViewUKM2Report.Visible = True
                    ViewPPCSReport.Visible = False
                    ViewUKM3Report.Visible = False

                    btnViewStudent_UKM1.Attributes("class") = "btn btn-default font"
                    btnViewStudent_UKM2.Attributes("class") = "btn btn-info"
                    btnViewStudent_PPCS.Attributes("class") = "btn btn-default font"
                    btnViewStudent_UKM3.Attributes("class") = "btn btn-default font"

                    YearListUKM2()
                    LevelListUKM2()
                    SemesterListUKM2()

                ElseIf Session("getStatus") = "PPCS R" Then ''PPCS Report
                    txtbreadcrum1.Text = "PPCS Report"

                    ViewUKM1Report.Visible = False
                    ViewUKM2Report.Visible = False
                    ViewPPCSReport.Visible = True
                    ViewUKM3Report.Visible = False

                    btnViewStudent_UKM1.Attributes("class") = "btn btn-default font"
                    btnViewStudent_UKM2.Attributes("class") = "btn btn-default font"
                    btnViewStudent_PPCS.Attributes("class") = "btn btn-info"
                    btnViewStudent_UKM3.Attributes("class") = "btn btn-default font"

                    YearListPPCS()
                    LevelListPPCS()
                    SemesterListPPCS()

                ElseIf Session("getStatus") = "UKM 3R" Then ''UKM3 Report
                    txtbreadcrum1.Text = "UKM3 Report"

                    ViewUKM1Report.Visible = False
                    ViewUKM2Report.Visible = False
                    ViewPPCSReport.Visible = False
                    ViewUKM3Report.Visible = True

                    btnViewStudent_UKM1.Attributes("class") = "btn btn-default font"
                    btnViewStudent_UKM2.Attributes("class") = "btn btn-default font"
                    btnViewStudent_PPCS.Attributes("class") = "btn btn-default font"
                    btnViewStudent_UKM3.Attributes("class") = "btn btn-info"

                    YearListUKM3()
                    LevelListUKM3()
                    SemesterListUKM3()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnViewStudent_UKM1.Visible = False
        btnViewStudent_UKM2.Visible = False
        btnViewStudent_PPCS.Visible = False
        btnViewStudent_PPCS.Visible = False

        ViewUKM1Report.Visible = False
        ViewUKM2Report.Visible = False
        ViewPPCSReport.Visible = False
        ViewUKM3Report.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        Dim Get_ViewUKM1 As String = ""
        Dim Get_ViewUKM2 As String = ""
        Dim Get_ViewPPCS As String = ""
        Dim Get_ViewUKM3 As String = ""

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

            If find_Data_SubMenu2 = "UKM 1 Report" And find_Data_SubMenu2.Length > 0 Then
                btnViewStudent_UKM1.Visible = True
                ViewUKM1Report.Visible = True

                Get_ViewUKM1 = "TRUE"
            End If

            If find_Data_SubMenu2 = "UKM 2 Report" And find_Data_SubMenu2.Length > 0 Then
                btnViewStudent_UKM2.Visible = True
                ViewUKM2Report.Visible = True

                Get_ViewUKM2 = "TRUE"
            End If

            If find_Data_SubMenu2 = "PPCS Report" And find_Data_SubMenu2.Length > 0 Then
                btnViewStudent_PPCS.Visible = True
                ViewPPCSReport.Visible = True

                Get_ViewPPCS = "TRUE"
            End If

            If find_Data_SubMenu2 = "UKM 3 Report" And find_Data_SubMenu2.Length > 0 Then
                btnViewStudent_UKM3.Visible = True
                ViewUKM3Report.Visible = True

                Get_ViewUKM3 = "TRUE"
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnViewStudent_UKM1.Visible = True
                btnViewStudent_UKM2.Visible = True
                btnViewStudent_PPCS.Visible = True
                btnViewStudent_UKM3.Visible = True

                Get_ViewUKM1 = "TRUE"
            End If
        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "UKM 1R" Or Session("getStatus") = "UKM 2R" Or Session("getStatus") = "PPCS R" Or Session("getStatus") = "UKM 3R" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "UKM 1R" And Session("getStatus") <> "UKM 2R" And Session("getStatus") <> "PPCS R" And Session("getStatus") <> "UKM 3R" Then
            If Get_ViewUKM1 = "TRUE" Then
                Data_If_Not_Group_Status = "UKM 1R"
            ElseIf Get_ViewUKM2 = "TRUE" Then
                Data_If_Not_Group_Status = "UKM 2R"
            ElseIf Get_ViewPPCS = "TRUE" Then
                Data_If_Not_Group_Status = "PPCS R"
            ElseIf Get_ViewUKM3 = "TRUE" Then
                Data_If_Not_Group_Status = "UKM 3R"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_ViewUKM1 = "TRUE" And Data_If_Not_Group_Status = "UKM 1R" Then
                Session("getStatus") = "UKM 1R"
            ElseIf Get_ViewUKM2 = "TRUE" And Data_If_Not_Group_Status = "UKM 2R" Then
                Session("getStatus") = "UKM 2R"
            ElseIf Get_ViewPPCS = "TRUE" And Data_If_Not_Group_Status = "PPCS R" Then
                Session("getStatus") = "PPCS R"
            ElseIf Get_ViewUKM3 = "TRUE" And Data_If_Not_Group_Status = "UKM 3R" Then
                Session("getStatus") = "UKM 3R"
            End If
        End If
    End Sub

    Private Sub btnViewStudent_UKM1_ServerClick(sender As Object, e As EventArgs) Handles btnViewStudent_UKM1.ServerClick
        Session("getStatus") = "UKM 1R"
        Response.Redirect("admin_laporanPeperiksaan_UKM1_PPCS.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewStudent_UKM2_ServerClick(sender As Object, e As EventArgs) Handles btnViewStudent_UKM2.ServerClick
        Session("getStatus") = "UKM 2R"
        Response.Redirect("admin_laporanPeperiksaan_UKM1_PPCS.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewStudent_PPCS_ServerClick(sender As Object, e As EventArgs) Handles btnViewStudent_PPCS.ServerClick
        Session("getStatus") = "PPCS R"
        Response.Redirect("admin_laporanPeperiksaan_UKM1_PPCS.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnViewStudent_UKM3_ServerClick(sender As Object, e As EventArgs) Handles btnViewStudent_UKM3.ServerClick
        Session("getStatus") = "UKM 3R"
        Response.Redirect("admin_laporanPeperiksaan_UKM1_PPCS.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub YearListUKM1()

        strSQL = "Select Parameter From Setting Where Type = 'Year'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear_UKM1.DataSource = ds
            ddlYear_UKM1.DataTextField = "Parameter"
            ddlYear_UKM1.DataValueField = "Parameter"
            ddlYear_UKM1.DataBind()
            ddlYear_UKM1.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub YearListUKM2()

        strSQL = "Select Parameter From Setting Where Type = 'Year'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear_UKM2.DataSource = ds
            ddlYear_UKM2.DataTextField = "Parameter"
            ddlYear_UKM2.DataValueField = "Parameter"
            ddlYear_UKM2.DataBind()
            ddlYear_UKM2.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub YearListPPCS()

        strSQL = "Select Parameter From Setting Where Type = 'Year'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear_PPCS.DataSource = ds
            ddlYear_PPCS.DataTextField = "Parameter"
            ddlYear_PPCS.DataValueField = "Parameter"
            ddlYear_PPCS.DataBind()
            ddlYear_PPCS.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub YearListUKM3()

        strSQL = "Select Parameter From Setting Where Type = 'Year'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear_UKM3.DataSource = ds
            ddlYear_UKM3.DataTextField = "Parameter"
            ddlYear_UKM3.DataValueField = "Parameter"
            ddlYear_UKM3.DataBind()
            ddlYear_UKM3.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LevelListUKM1()

        strSQL = "Select Parameter From Setting Where Type = 'Level'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel_UKM1.DataSource = ds
            ddlLevel_UKM1.DataTextField = "Parameter"
            ddlLevel_UKM1.DataValueField = "Parameter"
            ddlLevel_UKM1.DataBind()
            ddlLevel_UKM1.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LevelListUKM2()

        strSQL = "Select Parameter From Setting Where Type = 'Level'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel_UKM2.DataSource = ds
            ddlLevel_UKM2.DataTextField = "Parameter"
            ddlLevel_UKM2.DataValueField = "Parameter"
            ddlLevel_UKM2.DataBind()
            ddlLevel_UKM2.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LevelListPPCS()

        strSQL = "Select Parameter From Setting Where Type = 'Level'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel_PPCS.DataSource = ds
            ddlLevel_PPCS.DataTextField = "Parameter"
            ddlLevel_PPCS.DataValueField = "Parameter"
            ddlLevel_PPCS.DataBind()
            ddlLevel_PPCS.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LevelListUKM3()

        strSQL = "Select Parameter From Setting Where Type = 'Level'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel_UKM3.DataSource = ds
            ddlLevel_UKM3.DataTextField = "Parameter"
            ddlLevel_UKM3.DataValueField = "Parameter"
            ddlLevel_UKM3.DataBind()
            ddlLevel_UKM3.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SemesterListUKM1()

        strSQL = "Select Parameter, Value From Setting Where Type = 'Sem'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester_UKM1.DataSource = ds
            ddlSemester_UKM1.DataTextField = "Parameter"
            ddlSemester_UKM1.DataValueField = "Value"
            ddlSemester_UKM1.DataBind()
            ddlSemester_UKM1.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SemesterListUKM2()

        strSQL = "Select Parameter, Value From Setting Where Type = 'Sem'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester_UKM2.DataSource = ds
            ddlSemester_UKM2.DataTextField = "Parameter"
            ddlSemester_UKM2.DataValueField = "Value"
            ddlSemester_UKM2.DataBind()
            ddlSemester_UKM2.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SemesterListPPCS()

        strSQL = "Select Parameter, Value From Setting Where Type = 'Sem'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester_PPCS.DataSource = ds
            ddlSemester_PPCS.DataTextField = "Parameter"
            ddlSemester_PPCS.DataValueField = "Value"
            ddlSemester_PPCS.DataBind()
            ddlSemester_PPCS.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub SemesterListUKM3()

        strSQL = "Select Parameter, Value From Setting Where Type = 'Sem'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester_UKM3.DataSource = ds
            ddlSemester_UKM3.DataTextField = "Parameter"
            ddlSemester_UKM3.DataValueField = "Value"
            ddlSemester_UKM3.DataBind()
            ddlSemester_UKM3.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlYear_UKM1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_UKM1.SelectedIndexChanged
        Try
            Dim getData As String = ddlYear_UKM1.SelectedValue
            strRet = BindDataUKM1(datRespondentUKM1)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_UKM2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_UKM2.SelectedIndexChanged
        Try
            strRet = BindDataUKM2(datRespondentUKM2)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_PPCS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_PPCS.SelectedIndexChanged
        Try
            strRet = BindDataPPCS(datRespondentPPCS)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_UKM3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_UKM3.SelectedIndexChanged
        Try
            strRet = BindDataUKM3(datRespondentUKM3)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_UKM1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel_UKM1.SelectedIndexChanged
        Try
            strRet = BindDataUKM1(datRespondentUKM1)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_UKM2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel_UKM2.SelectedIndexChanged
        Try
            strRet = BindDataUKM2(datRespondentUKM2)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_PPCS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel_PPCS.SelectedIndexChanged
        Try
            strRet = BindDataPPCS(datRespondentPPCS)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_UKM3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel_UKM3.SelectedIndexChanged
        Try
            strRet = BindDataUKM3(datRespondentUKM3)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSemester_UKM1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester_UKM1.SelectedIndexChanged
        Try
            strRet = BindDataUKM1(datRespondentUKM1)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSemester_UKM2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester_UKM2.SelectedIndexChanged
        Try
            strRet = BindDataUKM2(datRespondentUKM2)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSemester_PPCS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester_PPCS.SelectedIndexChanged
        Try
            strRet = BindDataPPCS(datRespondentPPCS)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSemester_UKM3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester_UKM3.SelectedIndexChanged
        Try
            strRet = BindDataUKM3(datRespondentUKM3)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataUKM1(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL_UKM1, strConnPermata)
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

    Private Function getSQL_UKM1() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "  select C.std_ID, C.student_Name, C.student_ID, B.ExamYear, B.ModA, B.ModB, B.ModC, B.TotalScore, B.TotalPercentage 
                    from kolejadmin.dbo.student_info C
                    left join StudentProfile A on C.student_Mykad = A.MYKAD
                    left join UKM1 B on A.StudentID = B.StudentID
                    LEFT JOIn kolejadmin.dbo.student_Level D on C.std_ID = D.std_ID"
        strWhere = "    where D.year = '" & ddlYear_UKM1.SelectedValue & "' and D.student_Level = '" & ddlLevel_UKM1.SelectedValue & "' and D.student_Sem = '" & ddlSemester_UKM1.SelectedValue & "'
                        and (C.student_Status = 'Access' or C.student_Status = 'Graduate')"
        strOrder = "    ORDER BY B.TotalPercentage DESC, C.student_Name ASC"

        getSQL_UKM1 = tmpSQL & strWhere & strOrder

        Return getSQL_UKM1
    End Function

    Private Function BindDataUKM2(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL_UKM2, strConnPermata)
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

    Private Function getSQL_UKM2() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "  SELECT distinct C.std_ID, C.student_Name, C.student_ID, A.ExamYear, A.VCI, A.PRI, A.WMI, A.PSI, A.TotalScore, ROUND(A.TotalPercentage,2) as TotalPercentage, ROUND(A.Mental_Age_Year,2) as Mental_Age_Year, Round(A.Student_IQ,2) as Student_IQ FROM UKM2 A
                    LEFT JOIN StudentProfile B on A.StudentID = B.StudentID
                    LEFT JOIN kolejadmin.dbo.student_info C on B.MYKAD = C.student_Mykad
                    LEFT JOIn kolejadmin.dbo.student_Level D on C.std_ID = D.std_ID"
        strWhere = "    where D.year = '" & ddlYear_UKM2.SelectedValue & "' and D.student_Level = '" & ddlLevel_UKM2.SelectedValue & "' and D.student_Sem = '" & ddlSemester_UKM2.SelectedValue & "'
                        and (C.student_Status = 'Access' or C.student_Status = 'Graduate')"
        strOrder = "    ORDER BY A.TotalScore DESC, Student_IQ DESC, C.student_Name ASC"

        getSQL_UKM2 = tmpSQL & strWhere & strOrder

        Return getSQL_UKM2
    End Function

    Private Function BindDataPPCS(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL_UKMPPCS, strConnPermata)
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

    Private Function getSQL_UKMPPCS() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "  SELECT E.std_ID, E.student_Name, E.student_ID,A.PPCSDate,A.PPCSStatus,A.StatusTawaran,A.StatusReason,B.CourseCode,C.ClassCode FROM PPCS A
                    LEFT JOIN PPCS_Course B ON A.CourseID = B.CourseID
                    LEFT JOIN PPCS_Class C ON A.ClassID = C.ClassID
                    LEFT JOIN StudentProfile D on A.StudentID = D.StudentID
                    LEFT JOIN kolejadmin.dbo.student_info E on D.MYKAD = E.student_Mykad
                    LEFT JOIn kolejadmin.dbo.student_Level F on E.std_ID = F.std_ID"
        strWhere = "    where F.year = '" & ddlYear_PPCS.SelectedValue & "' and F.student_Level = '" & ddlLevel_PPCS.SelectedValue & "' and F.student_Sem = '" & ddlSemester_PPCS.SelectedValue & "' 
                        and (E.student_Status = 'Access' or E.student_Status = 'Graduate')"
        strOrder = "    ORDER BY E.student_Name ASC, A.PPCSDate ASC"

        getSQL_UKMPPCS = tmpSQL & strWhere & strOrder

        Return getSQL_UKMPPCS
    End Function

    Private Function BindDataUKM3(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL_UKM3, strConnUKM3)
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

    Private Function getSQL_UKM3() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY F.student_Name ASC, G.ukm3Year ASC "

        tmpSQL = "  select distinct F.std_ID, F.student_Name, F.student_ID, G.ukm3Year, A.marks_100, A.markPretest, A.markPosttest, A.insPPCS100mark, A.insRAPPCS100mark, A.insKPP100mark, A.compo_Mark from ukm3 A
                    left join instruktorExam_result B on A.id = B.ukm3id
                    left join instruktorExam_result_raPcs C on A.id = C.ukm3id
                    left join instruktorExam_result_kpp D on A.id = D.ukm3id
                    left join student_info E on A.student_id = E.std_ID
                    left join kolejadmin.dbo.student_info F on E.student_Mykad = F.student_Mykad
                    left join UKM3Session G on A.session_id = G.id
                    LEFT JOIn kolejadmin.dbo.student_Level H on F.std_ID = H.std_ID"

        strWhere = "    where H.year = '" & ddlYear_UKM3.SelectedValue & "' and H.student_Level = '" & ddlLevel_UKM3.SelectedValue & "' and H.student_Sem = '" & ddlSemester_UKM3.SelectedValue & "' 
                        and (F.student_Status = 'Access' or F.student_Status = 'Graduate')"

        getSQL_UKM3 = tmpSQL & strWhere & strOrder

        Return getSQL_UKM3
    End Function

End Class