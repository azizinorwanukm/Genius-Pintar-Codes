Imports System.Data.SqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim result As Integer = 0
    Dim loginid As String = ""
    Dim atemptCount As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Not Request.IsSecureConnection Then
        '    Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"))
        'End If

        Try
            If Not IsPostBack Then

                result = Request.QueryString("result")
                loginid = Request.QueryString("loginid")
                atemptCount = Request.QueryString("attempt")

                If result = 1 Then
                    ShowMessage(" Invalid Username", MessageType.Error)
                ElseIf result = 2 Then
                    ShowMessage(" Invalid Password", MessageType.Error)
                ElseIf result = 3 Then
                    ShowMessage(" Username And Password Does not Match.. Login Attempt " & atemptCount & " Of 3 For Login ID =" & loginid & "", MessageType.Error)
                ElseIf result = 4 Then
                    ShowMessage(" Your Login ID Has Been Blocked For Multiple Fail Login Attempt.. Please Contact PERMATA PINTAR Person In Charge", MessageType.Error)
                ElseIf result = 5 Then
                    ShowMessage(" Pengunaan Sistem Pelajar Dan Ibu Bapa ditangguhkan sehingga tarikh keputusan dikeluarkan.. ", MessageType.Error)
                ElseIf result = 6 Then
                    ShowMessage(" Username And Password Does Not Exist. Please Enter The Correct Username And Password ", MessageType.Error)
                ElseIf result = 7 Then
                    ShowMessage(" You Does Not Have Access To Login Genius Pintar Websites ", MessageType.Error)
                ElseIf result = 8 Then
                    ShowMessage(" You Does Not Have Access To Login Akademik Pintar Websites ", MessageType.Error)
                ElseIf result = 88 Then
                    logout_activity(Request.QueryString("admin_ID"))
                ElseIf result = 89 Then
                    logout_activity(Request.QueryString("stf_ID"))
                ElseIf result = 90 Then
                    logout_activity(Request.QueryString("pengarah_ID"))
                Else

                End If

                If Session("loginType_Info") = "" Or Session("loginType_Info") = "PGPN_info" Then
                    login_inf_btn_PGPN.Visible = True
                    login_inf_btn_APP.Visible = False

                    BtnLoginPGPN.Visible = True
                    BtnLoginAPP.Visible = True
                    txtloginUsername.Visible = True
                    txtloginPassword.Visible = True
                    logo_pgpn.Visible = True
                    logo_app.Visible = False

                    BtnLoginPGPN.Attributes("class") = "btn btn-success"
                    BtnLoginAPP.Attributes("class") = "btn btn-default font"

                    logo_pgpn.Src = "img/logo genius pintar.png"

                ElseIf Session("loginType_Info") = "APP_info" Then
                    login_inf_btn_PGPN.Visible = False
                    login_inf_btn_APP.Visible = True

                    BtnLoginPGPN.Visible = True
                    BtnLoginAPP.Visible = True
                    txtloginUsername.Visible = True
                    txtloginPassword.Visible = True
                    logo_pgpn.Visible = False
                    logo_app.Visible = True

                    BtnLoginPGPN.Attributes("class") = "btn btn-default font"
                    BtnLoginAPP.Attributes("class") = "btn btn-success"

                    logo_app.Src = "img/logo kpm.png"
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnLoginPGPN_ServerClick(sender As Object, e As EventArgs) Handles BtnLoginPGPN.ServerClick
        Session("loginType_Info") = "PGPN_info"
        Response.Redirect("default.aspx")
    End Sub

    Private Sub BtnLoginAPP_ServerClick(sender As Object, e As EventArgs) Handles BtnLoginAPP.ServerClick
        Session("loginType_Info") = "APP_info"
        Response.Redirect("default.aspx")
    End Sub

    Private Sub logout_activity(ByVal text As String)

        Dim accessID As String = "select MAX(security_ID) from security_ID where loginID_Number = '" & text & "'"
        Dim accessData As String = oCommon.getFieldValue(accessID)

        Dim get_userID As String = "select stf_ID from security_ID where security_ID = '" & accessData & "'"
        Dim data_userID As String = oCommon.getFieldValue(get_userID)

        Dim search_Data As String = "select staff_Login from staff_Login where stf_ID = '" & data_userID & "'"
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

    Private Sub btnLogin_PGPN_Click(sender As Object, e As EventArgs) Handles btnLogin_PGPN.Click

        ''get ipv4 address
        Dim host As String = Net.Dns.GetHostName()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim MyConnection As SqlConnection = New SqlConnection(strConn)
        Dim errorCount As Integer = 0

        MyConnection.Open()

        ''admin login / staff login
        Dim staffID As String = "select distinct staff_Login from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Status = 'Access'"
        Dim dataStaffID As String = oCommon.getFieldValue(staffID)
        Dim staffPSWD As String = "select distinct staff_Password from staff_Login where staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim dataStaffPSWD As String = oCommon.getFieldValue(staffPSWD)

        ''get user position either position 1 or position 2
        Dim find_P1 As String = "select staff_Access from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim get_P1 As String = oCommon.getFieldValue(find_P1)

        ''get stf_ID (Exist or Not)
        Dim find_stfID As String = "Select stf_ID from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim get_stfID As String = oCommon.getFieldValue(find_stfID)

        If txtloginUsername.Text = "" Or dataStaffID = "" Then
            Response.Redirect("default.aspx?result=1")
        ElseIf txtloginPassword.Text = "" Or dataStaffPSWD = "" Then
            Response.Redirect("default.aspx?result=2")
        ElseIf get_stfID = "" Or get_stfID.Length = 0 Then
            Response.Redirect("default.aspx?result=6")
        End If

        ''get user campus (PGPN OR APP)
        Dim find_stfCampus As String = "Select staff_Campus from staff_info where stf_ID = '" & get_stfID & "'"
        Dim get_stfCampus As String = oCommon.getFieldValue(find_stfCampus)

        If get_stfCampus <> "PGPN" Or get_stfCampus = "" Then
            Response.Redirect("default.aspx?result=7")
        End If

        If dataStaffID = txtloginUsername.Text And dataStaffPSWD = txtloginPassword.Text And dataStaffID <> "" And dataStaffPSWD <> "" Then

            Session("SchoolCampus") = get_stfCampus

            If get_P1 = "ADMIN" Or get_P1 = "PPE" Or get_P1 = "HEA" Or get_P1 = "HEP" Or get_P1 = "KSLR" Or get_P1 = "SUP" Or get_P1 = "SUD" Or get_P1 = "ADMIN KOKURIKULUM" Or get_P1 = "PA" Or get_P1 = "PENGARAH" Or get_P1 = "TIMBALAN PENGARAH" Or get_P1 = "KKSLR" Or get_P1 = "KU" Then ''login to admin portal

                Dim stfID As String = "select staff_info.stf_ID from staff_info left join staff_Login on staff_info.stf_ID = staff_Login.stf_ID
                                           where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                           and staff_Login.staff_Password = '" & txtloginPassword.Text & "'"
                Dim dataStfID As String = oCommon.getFieldValue(stfID)

                Dim find_staff_position As String = "select staff_Access from staff_Login where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                                         and staff_Login.staff_Password = '" & txtloginPassword.Text & "' "
                Dim get_staff_postion As String = oCommon.getFieldValue(find_staff_position)

                Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
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

                Using LoginData As New SqlCommand("INSERT into security_ID(datetime,stf_ID,loginID_Number) 
                                                       values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & dataStfID & "','" & login_ID & "')", objConn)
                    objConn.Open()
                    Dim i = LoginData.ExecuteNonQuery()
                    objConn.Close()
                    If i <> 0 Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End Using

                strSQL = "Update staff_Login set staff_LoginAttempt = '0' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Session("user_position") = get_staff_postion

                Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + login_ID)

            ElseIf get_P1 = "PENSYARAH" Or get_P1 = "INSTRUKTOR KPP" Or get_P1 = "INSTRUKTOR KPP - SEMENTARA" Then

                ''login to lectuer / homeroom / coordinator portal
                Dim STAFF_ID As String = "select staff_info.stf_ID from staff_info left join staff_Login on staff_info.stf_ID = staff_Login.stf_ID
                                               where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                               and staff_Login.staff_Password = '" & txtloginPassword.Text & "' "
                Dim DATA_STAFF_ID As String = oCommon.getFieldValue(STAFF_ID)

                Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
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

                Using LoginData As New SqlCommand("INSERT into security_ID(datetime,stf_ID,loginID_Number) 
                                                       values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & DATA_STAFF_ID & "','" & login_ID & "')", objConn)
                    objConn.Open()
                    Dim i = LoginData.ExecuteNonQuery()
                    objConn.Close()
                    If i <> 0 Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End Using

                Dim find_staff_position As String = "select staff_Access from staff_Login where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                                         and staff_Login.staff_Password = '" & txtloginPassword.Text & "' "
                Dim get_staff_postion As String = oCommon.getFieldValue(find_staff_position)

                Session("lecturer_position") = get_staff_postion

                strSQL = "Update staff_Login set staff_LoginAttempt = '0' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Response.Redirect("pengajar_login_berjaya.aspx?stf_ID=" + login_ID + "&status=SI")

            End If
        Else
            attemptLogin()
            Response.Redirect("default.aspx")

        End If

    End Sub

    Private Sub btnLogin_APP_Click(sender As Object, e As EventArgs) Handles btnLogin_APP.Click

        ''get ipv4 address
        Dim host As String = Net.Dns.GetHostName()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim MyConnection As SqlConnection = New SqlConnection(strConn)
        Dim errorCount As Integer = 0

        MyConnection.Open()

        ''admin login / staff login
        Dim staffID As String = "select distinct staff_Login from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Status = 'Access'"
        Dim dataStaffID As String = oCommon.getFieldValue(staffID)
        Dim staffPSWD As String = "select distinct staff_Password from staff_Login where staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim dataStaffPSWD As String = oCommon.getFieldValue(staffPSWD)

        ''get user position either position 1 or position 2
        Dim find_P1 As String = "select staff_Access from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim get_P1 As String = oCommon.getFieldValue(find_P1)

        ''get stf_ID (Exist or Not)
        Dim find_stfID As String = "Select stf_ID from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim get_stfID As String = oCommon.getFieldValue(find_stfID)

        If txtloginUsername.Text = "" Or dataStaffID = "" Then
            Response.Redirect("default.aspx?result=1")
        ElseIf txtloginPassword.Text = "" Or dataStaffPSWD = "" Then
            Response.Redirect("default.aspx?result=2")
        ElseIf get_stfID = "" Or get_stfID.Length = 0 Then
            Response.Redirect("default.aspx?result=6")
        End If

        ''get user campus (PGPN OR APP)
        Dim find_stfCampus As String = "Select staff_Campus from staff_info where stf_ID = '" & get_stfID & "'"
        Dim get_stfCampus As String = oCommon.getFieldValue(find_stfCampus)

        If get_stfCampus <> "APP" Or get_stfCampus = "" Then
            Response.Redirect("default.aspx?result=8")
        End If

        If dataStaffID = txtloginUsername.Text And dataStaffPSWD = txtloginPassword.Text And dataStaffID <> "" And dataStaffPSWD <> "" Then

            Session("SchoolCampus") = get_stfCampus

            If get_P1 = "ADMIN" Or get_P1 = "PPE" Or get_P1 = "HEA" Or get_P1 = "HEP" Or get_P1 = "KSLR" Or get_P1 = "SUP" Or get_P1 = "SUD" Or get_P1 = "ADMIN KOKURIKULUM" Or get_P1 = "PA" Or get_P1 = "KU" Then ''login to admin portal

                Dim stfID As String = "select staff_info.stf_ID from staff_info left join staff_Login on staff_info.stf_ID = staff_Login.stf_ID
                                           where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                           and staff_Login.staff_Password = '" & txtloginPassword.Text & "'"
                Dim dataStfID As String = oCommon.getFieldValue(stfID)

                Dim find_staff_position As String = "select staff_Access from staff_Login where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                                         and staff_Login.staff_Password = '" & txtloginPassword.Text & "' "
                Dim get_staff_postion As String = oCommon.getFieldValue(find_staff_position)

                Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
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

                Using LoginData As New SqlCommand("INSERT into security_ID(datetime,stf_ID,loginID_Number) 
                                                       values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & dataStfID & "','" & login_ID & "')", objConn)
                    objConn.Open()
                    Dim i = LoginData.ExecuteNonQuery()
                    objConn.Close()
                    If i <> 0 Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End Using

                strSQL = "Update staff_Login set staff_LoginAttempt = '0' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Session("user_position") = get_staff_postion

                Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + login_ID)

            ElseIf get_P1 = "PENSYARAH" Or get_P1 = "INSTRUKTOR KPP" Or get_P1 = "INSTRUKTOR KPP - SEMENTARA" Or get_P1 = "GURU AKADEMIK PINTAR PENDANG" Then

                ''login to lectuer / homeroom / coordinator portal
                Dim STAFF_ID As String = "select staff_info.stf_ID from staff_info left join staff_Login on staff_info.stf_ID = staff_Login.stf_ID
                                               where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                               and staff_Login.staff_Password = '" & txtloginPassword.Text & "' "
                Dim DATA_STAFF_ID As String = oCommon.getFieldValue(STAFF_ID)

                Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
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

                Using LoginData As New SqlCommand("INSERT into security_ID(datetime,stf_ID,loginID_Number) 
                                                       values ('" & DateTime.Now.ToString("yyyyMMdd") & "','" & DATA_STAFF_ID & "','" & login_ID & "')", objConn)
                    objConn.Open()
                    Dim i = LoginData.ExecuteNonQuery()
                    objConn.Close()
                    If i <> 0 Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End Using

                Dim find_staff_position As String = "select staff_Access from staff_Login where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                                         and staff_Login.staff_Password = '" & txtloginPassword.Text & "' "
                Dim get_staff_postion As String = oCommon.getFieldValue(find_staff_position)

                Session("lecturer_position") = get_staff_postion

                strSQL = "Update staff_Login set staff_LoginAttempt = '0' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Response.Redirect("pengajar_login_berjaya.aspx?stf_ID=" + login_ID + "&status=SI")

            End If
        Else
            attemptLogin()
            Response.Redirect("default.aspx")

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

    Private Sub attemptLogin()

        Dim check_Staff_Login As String = "select stf_ID from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim get_Staff_Login As String = oCommon.getFieldValue(check_Staff_Login)

        If get_Staff_Login = "" Then

            StdattemptLogin()

        Else

            Dim attempt As Double = 0
            Dim countAttempt As String = "select staff_LoginAttempt from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "'"
            Dim data_Attempt As String = oCommon.getFieldValue(countAttempt)

            If data_Attempt = "3" Then
                strSQL = "Update staff_Login set staff_Status = 'Block' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Response.Redirect("default.aspx?result=4")
            Else
                attempt = Double.Parse(data_Attempt)
                attempt = attempt + 1

                strSQL = "Update staff_Login set staff_LoginAttempt = '" & attempt & "' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Response.Redirect("default.aspx?result=3&attempt=" + attempt + "&loginid=" + txtloginUsername.Text)
            End If

        End If
    End Sub

    Private Sub StdattemptLogin()
        Dim attempt As Integer = 0
        Dim countAttempt As String = "select staff_LoginAttempt from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "'"
        Dim data_Attempt As String = oCommon.getFieldValue(countAttempt)

        If data_Attempt = "3" Then
            strSQL = "Update staff_Login set staff_Status = 'Block' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Response.Redirect("default.aspx?result=4")
        Else
            attempt = Integer.Parse(data_Attempt)
            attempt = attempt + 1

            strSQL = "Update staff_Login set staff_LoginAttempt = '" & attempt & "' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Response.Redirect("default.aspx?result=3&attempt=" + attempt + "&loginid=" + txtloginUsername.Text)
        End If
    End Sub

End Class