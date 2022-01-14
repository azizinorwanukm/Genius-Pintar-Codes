Imports System.Data.SqlClient
Imports System.Globalization

Public Class WebForm1
    Inherits System.Web.UI.Page
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim result As Integer = 0
    Dim loginid As String = ""
    Dim atempt As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Request.IsSecureConnection Then
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"))
            End If

            If Not IsPostBack Then
                result = Request.QueryString("result")
                loginid = Request.QueryString("loginid")
                atempt = Request.QueryString("attempt")

                If result = 1 Then
                    ShowMessage(" Invalid Username, Please Enter The Correct Username", MessageType.Error)
                ElseIf result = 2 Then
                    ShowMessage(" Invalid Password, Please Enter The Correct Password", MessageType.Error)
                ElseIf result = 3 Then
                    ShowMessage(" Username And Password Does Not Match. Login Attempt " & atempt & " Of 3 For Username =" & loginid & "", MessageType.Error)
                ElseIf result = 4 Then
                    ShowMessage(" Your Login ID Has Been Blocked For Multiple Fail Login Attempt. Please Contact Pusat Genius@Pintar Negara Person In Charge", MessageType.Error)
                ElseIf result = 5 Then
                    ShowMessage(" Pengunaan Sistem Pelajar Dan Ibu Bapa ditangguhkan sehingga tarikh keputusan dikeluarkan. ", MessageType.Warning)
                ElseIf result = 89 Then
                    logout_activity(Request.QueryString("std_ID"))
                ElseIf result = 90 Then
                    logout_activityParent(Request.QueryString("parent_ID"))
                Else

                End If

                strRet = BindData(datRespondent)

                strSQL = " SELECT MAX(PengumumanID) FROM koko_pengumuman "
                strRet = oCommon.getFieldValuePermata(strSQL)

                koko_pengumuman_load(strRet)

                If Session("loginType_Info") = "" Then
                    login_inf_btn.Visible = True
                    signup_inf_btn.Visible = False
                    login_inf_btn_APP.Visible = True
                    signup_inf_btn_APP.Visible = False

                    BtnLoginTop.Visible = True
                    BtnSignupTop.Visible = True
                    txtloginUsername.Visible = True
                    txtloginPassword.Visible = True
                    BtnLoginTop_APP.Visible = True
                    BtnSignupTop_APP.Visible = True
                    txtloginUsername_APP.Visible = True
                    txtloginPassword_APP.Visible = True

                    BtnLoginTop.Attributes("class") = "btn btn-success"
                    BtnSignupTop.Attributes("class") = "btn btn-default font"
                    BtnLoginTop_APP.Attributes("class") = "btn btn-success"
                    BtnSignupTop_APP.Attributes("class") = "btn btn-default font"

                ElseIf Session("loginType_Info") = "L_info" Then
                    login_inf_btn.Visible = True
                    signup_inf_btn.Visible = False
                    login_inf_btn_APP.Visible = True
                    signup_inf_btn_APP.Visible = False

                    BtnLoginTop.Visible = True
                    BtnSignupTop.Visible = True
                    txtloginUsername.Visible = True
                    txtloginPassword.Visible = True
                    BtnLoginTop_APP.Visible = True
                    BtnSignupTop_APP.Visible = True
                    txtloginUsername_APP.Visible = True
                    txtloginPassword_APP.Visible = True

                    BtnLoginTop.Attributes("class") = "btn btn-success"
                    BtnSignupTop.Attributes("class") = "btn btn-default font"
                    BtnLoginTop_APP.Attributes("class") = "btn btn-success"
                    BtnSignupTop_APP.Attributes("class") = "btn btn-default font"

                ElseIf Session("loginType_Info") = "S_info" Then
                    login_inf_btn.Visible = False
                    signup_inf_btn.Visible = True
                    login_inf_btn_APP.Visible = True
                    signup_inf_btn_APP.Visible = False

                    BtnLoginTop.Visible = True
                    BtnSignupTop.Visible = True
                    txtloginUsername.Visible = True
                    txtloginPassword.Visible = False
                    BtnLoginTop_APP.Visible = True
                    BtnSignupTop_APP.Visible = True
                    txtloginUsername_APP.Visible = True
                    txtloginPassword_APP.Visible = True

                    BtnLoginTop.Attributes("class") = "btn btn-default font"
                    BtnSignupTop.Attributes("class") = "btn btn-success"
                    BtnLoginTop_APP.Attributes("class") = "btn btn-success"
                    BtnSignupTop_APP.Attributes("class") = "btn btn-default font"

                ElseIf Session("loginType_Info") = "L_info_APP" Then
                    login_inf_btn.Visible = True
                    signup_inf_btn.Visible = False
                    login_inf_btn_APP.Visible = True
                    signup_inf_btn_APP.Visible = False

                    BtnLoginTop.Visible = True
                    BtnSignupTop.Visible = True
                    txtloginUsername.Visible = True
                    txtloginPassword.Visible = True
                    BtnLoginTop_APP.Visible = True
                    BtnSignupTop_APP.Visible = True
                    txtloginUsername_APP.Visible = True
                    txtloginPassword_APP.Visible = True

                    BtnLoginTop.Attributes("class") = "btn btn-success"
                    BtnSignupTop.Attributes("class") = "btn btn-default font"
                    BtnLoginTop_APP.Attributes("class") = "btn btn-success"
                    BtnSignupTop_APP.Attributes("class") = "btn btn-default font"

                ElseIf Session("loginType_Info") = "S_info_APP" Then
                    login_inf_btn_APP.Visible = False
                    signup_inf_btn_APP.Visible = True
                    login_inf_btn.Visible = True
                    signup_inf_btn.Visible = False

                    BtnLoginTop_APP.Visible = True
                    BtnSignupTop_APP.Visible = True
                    txtloginUsername_APP.Visible = True
                    txtloginPassword_APP.Visible = False
                    BtnLoginTop.Visible = True
                    BtnSignupTop.Visible = True
                    txtloginUsername.Visible = True
                    txtloginPassword.Visible = True

                    BtnLoginTop_APP.Attributes("class") = "btn btn-default font"
                    BtnSignupTop_APP.Attributes("class") = "btn btn-success"
                    BtnLoginTop.Attributes("class") = "btn btn-success"
                    BtnSignupTop.Attributes("class") = "btn btn-default font"

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnLoginTop_ServerClick(sender As Object, e As EventArgs) Handles BtnLoginTop.ServerClick
        Session("loginType_Info") = "L_info"
        Response.Redirect("default.aspx")
    End Sub

    Private Sub BtnSignupTop_ServerClick(sender As Object, e As EventArgs) Handles BtnSignupTop.ServerClick
        Session("loginType_Info") = "S_info"
        Response.Redirect("default.aspx")
    End Sub

    Private Sub BtnLoginTop_APP_ServerClick(sender As Object, e As EventArgs) Handles BtnLoginTop_APP.ServerClick
        Session("loginType_Info") = "L_info_APP"
        Response.Redirect("default.aspx")
    End Sub

    Private Sub BtnSignupTop_APP_ServerClick(sender As Object, e As EventArgs) Handles BtnSignupTop_APP.ServerClick
        Session("loginType_Info") = "S_info_APP"
        Response.Redirect("default.aspx")
    End Sub

    Private Sub logout_activity(ByVal text As String)

        Dim accessID As String = "select MAX(security_ID) from student_SecurityID where loginID_Number = '" & text & "'"
        Dim accessData As String = oCommon.getFieldValue(accessID)

        Dim get_userID As String = "select std_ID from student_SecurityID where security_ID = '" & accessData & "'"
        Dim data_userID As String = oCommon.getFieldValue(get_userID)

        Dim search_Data As String = "select student_Mykad from student_info where std_ID = '" & data_userID & "'"
        Dim find_getData As String = oCommon.getFieldValue(search_Data)


        Session.Abandon()
        Session.RemoveAll()
        Session.Clear()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1))
        Response.Cache.SetNoStore()
        HttpContext.Current.Session.Abandon()

        ''get ipv4 address
        Dim host As String = Net.Dns.GetHostName()

        Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
                                           values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','LOGOUT','" & find_getData & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "')", objConn)
            objConn.Open()
            Dim i = LoginData.ExecuteNonQuery()
            objConn.Close()
        End Using

        Response.Redirect("default.aspx")

    End Sub

    Private Sub logout_activityParent(ByVal text As String)

        Dim accessID As String = "select MAX(security_ID) from student_SecurityID where loginID_Number = '" & text & "'"
        Dim accessData As String = oCommon.getFieldValue(accessID)

        Dim get_userID As String = "select std_ID from student_SecurityID where security_ID = '" & accessData & "'"
        Dim data_userID As String = oCommon.getFieldValue(get_userID)

        Dim search_Data As String = "select parent_IC from parent_Info where parent_ID = '" & data_userID & "'"
        Dim find_getData As String = oCommon.getFieldValue(search_Data)


        Session.Abandon()
        Session.RemoveAll()
        Session.Clear()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1))
        Response.Cache.SetNoStore()
        HttpContext.Current.Session.Abandon()

        ''get ipv4 address
        Dim host As String = Net.Dns.GetHostName()

        Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
                                           values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','LOGOUT','" & find_getData & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "')", objConn)
            objConn.Open()
            Dim i = LoginData.ExecuteNonQuery()
            objConn.Close()
        End Using

        Response.Redirect("default.aspx")

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        Dim get_Login As String = "select Value from setting where Type = 'User Login'"
        Dim data_Login As String = oCommon.getFieldValue(get_Login)

        If data_Login = "on" Or data_Login = "ON" Or data_Login = "On" Or data_Login = "oN" Then

            ''get ipv4 address
            Dim host As String = Net.Dns.GetHostName()

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim errorCount As Integer = 0

            MyConnection.Open()

            ''student login
            Dim studentParentIC As String = "select student_Mykad from student_info where student_Mykad = '" & txtloginUsername.Text & "'  and (student_Status = 'Access' or student_Status = 'Graduate') and student_Campus = 'PGPN' and student_ID like '%M%'"
            Dim dataStudentParentIC As String = getFieldValue(studentParentIC, strConn)
            Dim studentIC As String = "select student_Password from student_info where student_Password = '" & txtloginPassword.Text & "'   and (student_Status = 'Access' or student_Status = 'Graduate') and student_Campus = 'PGPN' and student_ID like '%M%'"
            Dim dataStudentIC As String = getFieldValue(studentIC, strConn)

            ''parent login
            Dim parentIC As String = "select student_Mykad from student_info where student_Mykad ='" & txtloginUsername.Text & "' and student_Status = 'Access' and student_Campus = 'PGPN' and student_ID like '%M%'"
            Dim dataParentIC As String = getFieldValue(parentIC, strConn)
            Dim find_parentPassword As String = "select parent_Password from parent_Info where parent_Password = '" & txtloginPassword.Text & "'"
            Dim data_findparentPassword As String = getFieldValue(find_parentPassword, strConn)

            ''get student status
            Dim find_SS As String = "Select student_Status from student_info where student_Mykad = '" & txtloginUsername.Text & "' and student_Campus = 'PGPN' and student_ID like '%M%'"
            Dim get_SS As String = oCommon.getFieldValue(find_SS)

            If txtloginUsername.Text = "" Then
                ShowMessage(" Please Fill In Mykad ", MessageType.Error)

            ElseIf txtloginPassword.Text = "" Then
                ShowMessage(" Please Fill In Password ", MessageType.Error)

            ElseIf get_SS = "Graduate" And txtloginUsername.Text = dataStudentParentIC And txtloginPassword.Text = dataStudentIC Then

                'Collect Registered Data 
                Dim get_StudentID_Data As String = "Select std_ID from student_info where student_Mykad = '" & txtloginUsername.Text & "' and student_Password = '" & txtloginPassword.Text & "' and student_Status = 'Graduate' and student_Campus = 'PGPN' and student_ID like '%M%'"
                Dim collectData_StudentID As String = oCommon.getFieldValue(get_StudentID_Data)

                Dim login_ID As String = oCommon.random()

                Using LoginData As New SqlCommand(" INSERT into student_SecurityID(datetime,std_ID,loginID_Number) 
                                                    values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & collectData_StudentID & "','" & login_ID & "')", objConn)
                    objConn.Open()
                    Dim i = LoginData.ExecuteNonQuery()
                    objConn.Close()

                    If i <> 0 Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End Using

                Session("Std_ID") = login_ID
                Session("Student_Campus") = "PGPN"
                Session("Std_Status") = "AI"
                Response.Redirect("pelajar_alumni.aspx")

            ElseIf get_SS = "Access" Then

                'Collect Registered Data 
                Dim get_StudentID_Data As String = "Select std_ID from student_info where student_Mykad = '" & txtloginUsername.Text & "' and student_Status = 'Access' and student_Campus = 'PGPN' and student_ID like '%M%'"
                Dim collectData_StudentID As String = oCommon.getFieldValue(get_StudentID_Data)

                Dim find_Data_year As String = "Select distinct MAX(year) from student_level where std_ID = '" & collectData_StudentID & "'"
                Dim get_Data_year As String = oCommon.getFieldValue(find_Data_year)

                If collectData_StudentID.Length > 0 Then
                    Dim find_Data_Registered As String = "Select distinct Registered from student_Level where year = '" & get_Data_year & "' and std_ID = '" & collectData_StudentID & "'"
                    Dim get_Data_Registered As String = oCommon.getFieldValue(find_Data_Registered)

                    If get_Data_Registered = "Yes" Then

                        If dataStudentParentIC = txtloginUsername.Text And dataStudentIC = txtloginPassword.Text And dataStudentIC <> "" And dataStudentParentIC <> "" Then

                            ''student
                            Dim stdID As String = "select std_ID from student_info where student_Password = '" & txtloginPassword.Text & "' and student_Mykad = '" & txtloginUsername.Text & "' and student_Status = 'Access' and student_Campus = 'PGPN' and student_ID like '%M%'"
                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                            Using LoginData As New SqlCommand(" INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
                                                                values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','LOGIN','" & txtloginUsername.Text & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "')", objConn)
                                objConn.Open()
                                Dim i = LoginData.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                            Dim login_ID As String = oCommon.random()

                            Using LoginData As New SqlCommand(" INSERT into student_SecurityID(datetime,std_ID,loginID_Number) 
                                                                values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & dataStdID & "','" & login_ID & "')", objConn)
                                objConn.Open()
                                Dim i = LoginData.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                            strSQL = "Update student_info set student_LoginAttempt = '0' where student_Mykad = '" & txtloginUsername.Text & "' and student_Campus = 'PGPN'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            Session("Student_Campus") = "PGPN"

                            Response.Redirect("pelajar_login_berjaya.aspx?std_ID=" + login_ID + "&status=SI")

                        ElseIf dataParentIC = txtloginUsername.Text And data_findparentPassword = txtloginPassword.Text And dataParentIC <> "" And data_findparentPassword <> "" Then

                            ''parent
                            Dim prntID As String = "select Max(parent_ID) from parent_Info where parent_Password = '" & txtloginPassword.Text & "' "
                            Dim dataPrntID As String = getFieldValue(prntID, strConn)

                            Dim prntName As String = "select parent_Name from parent_Info where parent_Password = '" & txtloginPassword.Text & "' "
                            Dim dataPrntName As String = getFieldValue(prntName, strConn)

                            Dim stdID As String = "select std_ID from student_info where student_Mykad = '" & dataParentIC & "' and (parent_fatherID = '" & dataPrntID & "' or parent_motherID = '" & dataPrntID & "') and student_Campus = 'PGPN' and student_ID like '%M%'"
                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                            Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
                                                       values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','LOGIN','" & dataPrntName & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "')", objConn)
                                objConn.Open()
                                Dim i = LoginData.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                            Dim login_ID As String = oCommon.random()

                            Using LoginData As New SqlCommand("INSERT into student_SecurityID(datetime,std_ID,loginID_Number) 
                                                       values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & dataPrntID & "','" & login_ID & "')", objConn)
                                objConn.Open()
                                Dim i = LoginData.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                            Session("Student_Campus") = "PGPN"
                            Session("Std_ID") = dataStdID
                            Session("Parent_ID") = login_ID
                            Session("ParentPortal_Status") = "SI"

                            Response.Redirect("penjaga_login_berjaya.aspx")

                        Else
                            StdattemptLogin()
                        End If
                    Else
                        ShowMessage(" MYKAD " & txtloginUsername.Text & ", Have Not Sign Up For Year " & Now.Year & ". Please Sign Up.", MessageType.Error)
                    End If
                Else
                    ShowMessage(" MYKAD " & txtloginUsername.Text & ", Have Been Blocked. Please Contact Administrative.", MessageType.Error)
                End If


            End If
        Else
            Response.Redirect("default.aspx?result=5")
        End If
    End Sub

    Private Sub btnLogin_APP_Click(sender As Object, e As EventArgs) Handles btnLogin_APP.Click

        Dim get_Login As String = "select Value from setting where Type = 'User Login'"
        Dim data_Login As String = oCommon.getFieldValue(get_Login)

        If data_Login = "on" Or data_Login = "ON" Or data_Login = "On" Or data_Login = "oN" Then

            ''get ipv4 address
            Dim host As String = Net.Dns.GetHostName()

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim errorCount As Integer = 0

            MyConnection.Open()

            ''student login
            Dim studentParentIC As String = "select student_Mykad from student_info where student_Mykad = '" & txtloginUsername_APP.Text & "'  and student_Status = 'Access' and student_Campus = 'APP'"
            Dim dataStudentParentIC As String = getFieldValue(studentParentIC, strConn)
            Dim studentIC As String = "select student_Password from student_info where student_Password = '" & txtloginPassword_APP.Text & "'  and student_Status = 'Access' and student_Campus = 'APP'"
            Dim dataStudentIC As String = getFieldValue(studentIC, strConn)

            ''parent login
            Dim parentIC As String = "select student_Mykad from student_info where student_Mykad ='" & txtloginUsername_APP.Text & "' and student_Status = 'Access' and student_Campus = 'APP'"
            Dim dataParentIC As String = getFieldValue(parentIC, strConn)
            Dim find_parentPassword As String = "select parent_Password from parent_Info where parent_Password = '" & txtloginPassword_APP.Text & "'"
            Dim data_findparentPassword As String = getFieldValue(find_parentPassword, strConn)

            ''get student status
            Dim find_SS As String = "Select student_Status from student_info where student_Mykad = '" & txtloginUsername_APP.Text & "' and student_Campus = 'APP' and student_ID like '%P%'"
            Dim get_SS As String = oCommon.getFieldValue(find_SS)

            If txtloginUsername_APP.Text = "" Then
                ShowMessage(" Invalid Username, Please Enter The Correct Username", MessageType.Error)

            ElseIf txtloginPassword_APP.Text = "" Then
                ShowMessage(" Invalid Password, Please Enter The Correct Password", MessageType.Error)

            ElseIf get_SS = "Graduate" Then

                'Collect Registered Data 
                Dim get_StudentID_Data As String = "Select std_ID from student_info where student_Mykad = '" & txtloginUsername_APP.Text & "' and student_Password = '" & txtloginPassword_APP.Text & "' and student_Status = 'Graduate' and student_Campus = 'APP' and student_ID like '%P%'"
                Dim collectData_StudentID As String = oCommon.getFieldValue(get_StudentID_Data)

                Dim login_ID As String = oCommon.random()

                Using LoginData As New SqlCommand(" INSERT into student_SecurityID(datetime,std_ID,loginID_Number) 
                                                    values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & collectData_StudentID & "','" & login_ID & "')", objConn)
                    objConn.Open()
                    Dim i = LoginData.ExecuteNonQuery()
                    objConn.Close()

                    If i <> 0 Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End Using

                Session("Std_ID") = login_ID
                Session("Student_Campus") = "APP"
                Response.Redirect("pelajar_alumni.aspx")

            ElseIf get_SS = "Access" Then

                'Collect Registered Data 
                Dim get_StudentID_Data As String = "Select std_ID from student_info where student_Mykad = '" & txtloginUsername_APP.Text & "' and student_Status = 'Access' and student_Campus = 'APP'"
                Dim collectData_StudentID As String = oCommon.getFieldValue(get_StudentID_Data)

                Dim find_Data_year As String = "Select distinct MAX(year) from student_level where std_ID = '" & collectData_StudentID & "'"
                Dim get_Data_year As String = oCommon.getFieldValue(find_Data_year)

                If collectData_StudentID.Length > 0 Then
                    Dim find_Data_Registered As String = "Select distinct Registered from student_Level where year = '" & get_Data_year & "' and std_ID = '" & collectData_StudentID & "'"
                    Dim get_Data_Registered As String = oCommon.getFieldValue(find_Data_Registered)

                    If get_Data_Registered = "Yes" Then

                        If dataStudentParentIC = txtloginUsername_APP.Text And dataStudentIC = txtloginPassword_APP.Text And dataStudentIC <> "" And dataStudentParentIC <> "" Then

                            ''student
                            Dim stdID As String = "select std_ID from student_info where student_Password = '" & txtloginPassword_APP.Text & "' and student_Mykad = '" & txtloginUsername_APP.Text & "' and student_Status = 'Access' and student_Campus = 'APP'"
                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                            Using LoginData As New SqlCommand(" INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
                                                                values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','LOGIN','" & txtloginPassword_APP.Text & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "')", objConn)
                                objConn.Open()
                                Dim i = LoginData.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                            Dim login_ID As String = oCommon.random()

                            Using LoginData As New SqlCommand(" INSERT into student_SecurityID(datetime,std_ID,loginID_Number) 
                                                                values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & dataStdID & "','" & login_ID & "')", objConn)
                                objConn.Open()
                                Dim i = LoginData.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                            strSQL = "Update student_info set student_LoginAttempt = '0' where student_Mykad = '" & txtloginUsername_APP.Text & "' and student_Campus = 'APP'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            Session("Student_Campus") = "APP"

                            Response.Redirect("pelajar_login_berjaya.aspx?std_ID=" + login_ID + "&status=SI")

                        ElseIf dataParentIC = txtloginUsername_APP.Text And data_findparentPassword = txtloginPassword_APP.Text And dataParentIC <> "" And data_findparentPassword <> "" Then

                            ''parent
                            Dim prntID As String = "select Max(parent_ID) from parent_Info where parent_Password = '" & txtloginPassword_APP.Text & "' "
                            Dim dataPrntID As String = getFieldValue(prntID, strConn)

                            Dim prntName As String = "select parent_Name from parent_Info where parent_Password = '" & txtloginPassword_APP.Text & "' "
                            Dim dataPrntName As String = getFieldValue(prntName, strConn)

                            Dim stdID As String = "select std_ID from student_info where student_Mykad = '" & dataParentIC & "' and (parent_fatherID = '" & dataPrntID & "' or parent_motherID = '" & dataPrntID & "') and student_Campus = 'APP'"
                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                            Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
                                                       values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','LOGIN','" & dataPrntName & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "')", objConn)
                                objConn.Open()
                                Dim i = LoginData.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                            Dim login_ID As String = oCommon.random()

                            Using LoginData As New SqlCommand("INSERT into student_SecurityID(datetime,std_ID,loginID_Number) 
                                                       values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & dataPrntID & "','" & login_ID & "')", objConn)
                                objConn.Open()
                                Dim i = LoginData.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                            Session("Student_Campus") = "APP"
                            Session("Std_ID") = dataStdID
                            Session("Parent_ID") = login_ID
                            Session("ParentPortal_Status") = "SI"

                            Response.Redirect("penjaga_login_berjaya.aspx")

                        Else
                            StdattemptLogin()
                        End If
                    Else
                        ShowMessage(" MYKAD " & txtloginUsername_APP.Text & ", Have Not Sign Up For Year " & Now.Year & ". Please Sign Up.", MessageType.Error)
                    End If
                Else
                    ShowMessage(" MYKAD " & txtloginUsername_APP.Text & ", Have Been Blocked. Please Contact Administrative.", MessageType.Error)
                End If

            End If
        Else
            ShowMessage(" Pengunaan Sistem Pelajar Dan Ibu Bapa ditangguhkan sehingga tarikh keputusan dikeluarkan. ", MessageType.Warning)
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

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

    Private Sub StdattemptLogin()
        Dim attempt As Integer = 0
        Dim countAttempt As String = "select student_LoginAttempt from student_info where student_Mykad = '" & txtloginUsername_APP.Text & "' "
        Dim data_Attempt As String = oCommon.getFieldValue(countAttempt)

        If data_Attempt = "3" Then
            strSQL = "Update student_info set student_Status = 'Block' where student_Mykad = '" & txtloginUsername_APP.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Response.Redirect("default.aspx?result=4")
        Else
            attempt = Integer.Parse(data_Attempt)
            attempt = attempt + 1

            strSQL = "Update student_info set student_LoginAttempt = '" & attempt & "' where student_Mykad = '" & txtloginUsername_APP.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Response.Redirect("default.aspx?result=3&attempt=" + attempt.ToString() + "&loginid=" + txtloginUsername_APP.Text)
        End If
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConnPermata)
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
        Dim strOrder As String = " ORDER BY PengumumanID DESC"

        tmpSQL = "      SELECT * FROM koko_pengumuman"
        strWhere = "    WHERE IsDisplay='Y' AND Tahun='" & oCommon.getAppsettings("DefaultKOKOYear") & "'"

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

    Protected Sub lnkRead_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = DirectCast(sender, LinkButton)
        Dim row As GridViewRow = DirectCast(btn.NamingContainer, GridViewRow)

        Dim strMsg As String = datRespondent.DataKeys(row.RowIndex).Value.ToString()

        koko_pengumuman_load(strMsg)

    End Sub

    Private Sub koko_pengumuman_load(ByVal data As String)
        Dim strDateCreated As String = ""

        strSQL = "SELECT * FROM koko_pengumuman WHERE PengumumanID='" & data & "' AND Tahun='" & oCommon.getAppsettings("DefaultKOKOYear") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Title")) Then
                    lblTitle.Text = ds.Tables(0).Rows(0).Item("Title")
                Else
                    lblTitle.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Body")) Then
                    ltBody.Text = ds.Tables(0).Rows(0).Item("Body").ToString.Replace(Environment.NewLine, "<br />")
                Else
                    ltBody.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DateCreated")) Then
                    strDateCreated = ds.Tables(0).Rows(0).Item("DateCreated")
                    lblDateCreated.Text = oCommon.formatDateDay(strDateCreated)
                Else
                    lblDateCreated.Text = ""
                End If

            End If

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnSignup_Click(sender As Object, e As EventArgs) Handles btnSignup.Click

        ''Get Start Date Registration
        Dim StartDate_Checking As String = "Select Value from setting where Type = 'Student Registration Start Date' and idx = 'Student Registration Date'"
        Dim Collect_StartDate As String = oCommon.getFieldValue(StartDate_Checking)

        Dim convertToDate_StartDate As DateTime = DateTime.ParseExact(Collect_StartDate, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
        Dim formatted_StartDate As String = convertToDate_StartDate.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo)

        ''Get End Date Registration
        Dim EndDate_Checking As String = "Select Value from setting where Type = 'Student Registration End Date' and idx = 'Student Registration Date'"
        Dim Collect_EndDate As String = oCommon.getFieldValue(EndDate_Checking)

        Dim convertToDate_EndDate As DateTime = DateTime.ParseExact(Collect_EndDate, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
        Dim formatted_EndDate As String = convertToDate_EndDate.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo)

        ''Get Current Date
        Dim Get_CurrentDate As String = DateTime.Now.ToString("yyyyMMdd")

        If Get_CurrentDate >= formatted_StartDate Then

            If Get_CurrentDate <= formatted_EndDate Then

                ''Collect Data From Database PERMATAPINTAR
                Dim find_Data_StudentID As String = "Select StudentID from StudentProfile where MYKAD = '" & txtloginUsername.Text & "'"
                Dim get_Data_StudentID As String = oCommon.getFieldValuePermata(find_Data_StudentID)

                If get_Data_StudentID.Length > 0 Then

                    ''Collect Data From Database PERMATAPINTAR
                    Dim find_Data_isLayak As String = "Select UKM3ID from UKM3 where StudentID = '" & get_Data_StudentID & "' and isLayak = 'Y' and PPMT = 'Y' and StatusTawaran = 'TERIMA' and PPYear = '" & Now.Year & "'"
                    Dim get_data_isLayak As String = oCommon.getFieldValuePermata(find_Data_isLayak)

                    If get_data_isLayak.Length > 0 Then

                        'Collect Registered Data 
                        Dim get_StudentID_Data As String = "Select std_ID from student_info where student_Mykad = '" & txtloginUsername.Text & "' and student_Status = 'Access' and student_Campus = 'PGPN'"
                        Dim collectData_StudentID As String = oCommon.getFieldValue(get_StudentID_Data)

                        Dim find_Data_Registered As String = "Select distinct Registered from student_Level where year = '" & Now.Year & "' and std_ID = '" & collectData_StudentID & "'"
                        Dim get_Data_Registered As String = oCommon.getFieldValue(find_Data_Registered)

                        If get_Data_Registered = "No" Then
                            Session("NewStudent_SignUp") = "SRT"
                            Session("std_Mykad") = txtloginUsername.Text
                            Response.Redirect("pelajar_daftar_baru.aspx?StudentID=" + get_Data_StudentID)

                            Session("Student_Campus") = "PGPN"
                        Else
                            ShowMessage(" MYKAD " & txtloginUsername.Text & ", Have Been Sign Up For Year " & Now.Year & ". Please Login.", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" MYKAD " & txtloginUsername.Text & ", Are Not Accepted In Pusat Genius@PINTAR Negara", MessageType.Error)
                    End If
                Else
                    ShowMessage(" MYKAD " & txtloginUsername.Text & ", Are Not Found In Pusat Genius@PINTAR Negara Database", MessageType.Error)
                End If
            Else
                ShowMessage(" Sign Up Registration Are Closed After " & Collect_EndDate, MessageType.Error)
            End If
        Else
            ShowMessage(" Sign Up Registration Are Not Open Until " & Collect_StartDate, MessageType.Error)
        End If

    End Sub

    Private Sub btnSignup_APP_Click(sender As Object, e As EventArgs) Handles btnSignup_APP.Click

        ''Get Start Date Registration
        Dim StartDate_Checking As String = "Select Value from setting where Type = 'Student Registration Start Date' and idx = 'Student Registration Date'"
        Dim Collect_StartDate As String = oCommon.getFieldValue(StartDate_Checking)

        Dim convertToDate_StartDate As DateTime = DateTime.ParseExact(Collect_StartDate, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
        Dim formatted_StartDate As String = convertToDate_StartDate.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo)

        ''Get End Date Registration
        Dim EndDate_Checking As String = "Select Value from setting where Type = 'Student Registration End Date' and idx = 'Student Registration Date'"
        Dim Collect_EndDate As String = oCommon.getFieldValue(EndDate_Checking)

        Dim convertToDate_EndDate As DateTime = DateTime.ParseExact(Collect_EndDate, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
        Dim formatted_EndDate As String = convertToDate_EndDate.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo)

        ''Get Current Date
        Dim Get_CurrentDate As String = DateTime.Now.ToString("yyyyMMdd")

        If Get_CurrentDate >= formatted_StartDate Then

            If Get_CurrentDate <= formatted_EndDate Then

                ''Collect Data From Database PERMATAPINTAR
                Dim find_Data_StudentID As String = "Select StudentID from StudentProfile where MYKAD = '" & txtloginUsername_APP.Text & "'"
                Dim get_Data_StudentID As String = oCommon.getFieldValuePermata(find_Data_StudentID)

                If get_Data_StudentID.Length > 0 Then

                    ''Collect Data From Database PERMATAPINTAR
                    Dim find_Data_isLayak As String = "Select UKM3ID from UKM3 where StudentID = '" & get_Data_StudentID & "' and isLayak = 'Y' and PPMT = 'Y' and StatusTawaran = 'TERIMA' and PPYear = '" & Now.Year & "'"
                    Dim get_data_isLayak As String = oCommon.getFieldValuePermata(find_Data_isLayak)

                    If get_data_isLayak.Length > 0 Then

                        'Collect Registered Data 
                        Dim get_StudentID_Data As String = "Select std_ID from student_info where student_Mykad = '" & txtloginUsername_APP.Text & "' and student_Status = 'Access' and student_Campus = 'APP'"
                        Dim collectData_StudentID As String = oCommon.getFieldValue(get_StudentID_Data)

                        Dim find_Data_Registered As String = "Select distinct Registered from student_Level where year = '" & Now.Year & "' and std_ID = '" & collectData_StudentID & "'"
                        Dim get_Data_Registered As String = oCommon.getFieldValue(find_Data_Registered)

                        If get_Data_Registered = "No" Then
                            Session("NewStudent_SignUp") = "SRT"
                            Session("std_Mykad") = txtloginUsername_APP.Text
                            Response.Redirect("pelajar_daftar_baru.aspx?StudentID=" + get_Data_StudentID)

                            Session("Student_Campus") = "APP"
                        Else
                            ShowMessage(" MYKAD " & txtloginUsername_APP.Text & ", Have Been Sign Up For Year " & Now.Year & ". Please Login.", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" MYKAD " & txtloginUsername_APP.Text & ", Are Not Accepted In Akademik Pintar Pendang", MessageType.Error)
                    End If
                Else
                    ShowMessage(" MYKAD " & txtloginUsername_APP.Text & ", Are Not Found In Akademik Pintar Pendang Database", MessageType.Error)
                End If
            Else
                ShowMessage(" Sign Up Registration Are Closed After " & Collect_EndDate, MessageType.Error)
            End If
        Else
            ShowMessage(" Sign Up Registration Are Not Open Until " & Collect_StartDate, MessageType.Error)
        End If

    End Sub

End Class