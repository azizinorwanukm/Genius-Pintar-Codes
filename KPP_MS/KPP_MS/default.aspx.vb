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
    Dim atempt As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                result = Request.QueryString("result")
                loginid = Request.QueryString("loginid")
                atempt = Request.QueryString("attempt")

                If result = 1 Then
                    ShowMessage("Invalid Username", MessageType.Error)
                ElseIf result = 2 Then
                    ShowMessage("Invalid Password", MessageType.Error)
                ElseIf result = 3 Then
                    ShowMessage("Username And Password Doesn't Match.. Login Attempt " & atempt & " Of 5 For Login ID =" & loginid & "", MessageType.Error)
                ElseIf result = 4 Then
                    ShowMessage(" Your Login ID Has Been Blocked For Multiple Fail Login Attempt.. Please Contact PERMATA PINTAR Person In Charge", MessageType.Error)
                ElseIf result = 5 Then
                    ShowMessage("Pengunaan Sistem Pelajar Dan Ibu Bapa ditangguhkan sehingga tarikh keputusan dikeluarkan.. ", MessageType.Error)
                ElseIf result = 88 Then
                    logout_activity(Request.QueryString("admin_ID"))
                ElseIf result = 89 Then
                    logout_activity(Request.QueryString("stf_ID"))
                ElseIf result = 90 Then
                    logout_activity(Request.QueryString("pengarah_ID"))
                Else

                End If


            End If
        Catch ex As Exception

        End Try
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

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        ''get ipv4 address
        Dim host As String = Net.Dns.GetHostName()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim MyConnection As SqlConnection = New SqlConnection(strConn)
        Dim errorCount As Integer = 0

        MyConnection.Open()

        ''admin login / staff login
        Dim staffID As String = "select staff_Login from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Status = 'Access'"
        Dim dataStaffID As String = oCommon.getFieldValue(staffID)
        Dim staffPSWD As String = "select staff_Password from staff_Login where staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim dataStaffPSWD As String = oCommon.getFieldValue(staffPSWD)

        ''get user position either position 1 or position 2
        Dim find_P1 As String = "select staff_Access from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim get_P1 As String = oCommon.getFieldValue(find_P1)

        If txtloginUsername.Text = "" Or dataStaffID = "" Then
            Response.Redirect("default.aspx?result=1")

        ElseIf txtloginPassword.Text = "" Or dataStaffPSWD = "" Then
            Response.Redirect("default.aspx?result=2")

        End If

        If dataStaffID = txtloginUsername.Text And dataStaffPSWD = txtloginPassword.Text And dataStaffID <> "" And dataStaffPSWD <> "" Then

            If get_P1 = "PENGARAH" Or get_P1 = "TIMBALAN PENGARAH" Then ''login to pengarah portal

                Dim stfID As String = "select staff_info.stf_ID from staff_info left join staff_Login on staff_info.stf_ID = staff_Login.stf_ID
                                           where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "'
                                           and staff_Login.staff_Password = '" & txtloginPassword.Text & "'"
                Dim dataStfID As String = getFieldValue(stfID, strConn)

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

                Response.Redirect("pengarah_login_berjaya.aspx?pengarah_ID=" + login_ID)

            ElseIf get_P1 = "ADMIN" Or get_P1 = "PPE" Or get_P1 = "HEA" Or get_P1 = "HEP" Or get_P1 = "KSLR" Or get_P1 = "SUP" Or get_P1 = "SUD" Or get_P1 = "ADMIN KOKURIKULUM" Then ''login to admin portal

                Dim stfID As String = "select staff_info.stf_ID from staff_info left join staff_Login on staff_info.stf_ID = staff_Login.stf_ID
                                           where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                           and staff_Login.staff_Password = '" & txtloginPassword.Text & "'"
                Dim dataStfID As String = getFieldValue(stfID, strConn)

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

                Session("user_position") = get_staff_postion

                Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + login_ID)

            ElseIf get_P1 = "PENSYARAH" Or get_P1 = "INSTRUKTOR KPP" Or get_P1 = "INSTRUKTOR KPP - SEMENTARA" Then

                ''login to lectuer / homeroom / coordinator portal
                Dim STAFF_ID As String = "select staff_info.stf_ID from staff_info left join staff_Login on staff_info.stf_ID = staff_Login.stf_ID
                                               where staff_Login.staff_Status = 'Access' and staff_Login.staff_Login = '" & txtloginUsername.Text & "' 
                                               and staff_Login.staff_Password = '" & txtloginPassword.Text & "' "
                Dim DATA_STAFF_ID As String = getFieldValue(STAFF_ID, strConn)

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

                Response.Redirect("pengajar_login_berjaya.aspx?stf_ID=" + login_ID)

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

    Private Sub attemptLogin()

        Dim check_Staff_Login As String = "select stf_ID from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
        Dim get_Staff_Login As String = oCommon.getFieldValue(check_Staff_Login)

        If get_Staff_Login = "" Then

            StdattemptLogin()

        Else

            Dim attempt As Integer = 0
            Dim countAttempt As String = "select staff_LoginAttempt from staff_Info where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "'"
            Dim data_Attempt As String = oCommon.getFieldValue(countAttempt)

            If data_Attempt = "3" Then
                strSQL = "Update staff_Info set staff_Status = 'Block' where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Response.Redirect("default.aspx?result=4")
            Else
                attempt = Integer.Parse(data_Attempt)
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