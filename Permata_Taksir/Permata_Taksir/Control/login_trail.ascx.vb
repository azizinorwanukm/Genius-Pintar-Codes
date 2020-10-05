Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources

Public Class login_trail
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionMaster")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Function isExistL(ByVal strSQL As String) As String
        If strSQL.Length = 0 Then
            Return False
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionMaster")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            If ds.Tables(0).Rows.Count > 0 Then
                lblDebug.Text = "OK:" & strConn
                Return True
            Else
                lblDebug.Text = "NOK:" & strConn
                Return False
            End If

        Catch ex As Exception
            lblDebug.Text = "Err:" & ex.Message
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim usertype As String = Request.QueryString("userType")

        Dim strSQL2 As String = ""

        strSQL = "select top 1 A.staff_position from ukm3.dbo.staff_info A 
                    left join kolejadmin.dbo.staff_Info B on B.stf_id = A.staff_id
                    left join kolejadmin.dbo.staff_Login C on C.stf_ID = B.stf_ID 
                where A.staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' and A.isAllow = 1"
        ''(A.staff_Password = '" & txtPwd.Text & "' or (c.staff_Access='INSTRUKTOR KPP'))
        ''or c.staff_Password = '" & txtPwd.Text & "' AND 
        ''and A.staff_session like '%" & Now.Year & "%'
        strSQL2 = "select top 1 A.staff_position from ukm3.dbo.staff_info A 
                    left join permatapintar.dbo.PPCS_Users B on A.staff_id = B.ppcsuserid
                where A.staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' and A.isAllow = 1"

        If usertype = "rappcs" Then
            If oCommon.isExist(strSQL) = True Then
                Session.Clear()

                Session("permata_admin") = txtLoginID.Text
                Session("userType") = "rappcs"

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case getUserProfile_UserType()
                    Case "Instruktor Ra PPCS"
                        Response.Redirect("admin.default.aspx")
                    Case "Ra PPCS"
                        Response.Redirect("admin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                End Select


            ElseIf oCommon.isExist(strSQL2) = True Then
#Disable Warning S1871 ' Two branches in a conditional structure should not have exactly the same implementation
                Session.Clear()
                ''Debug.WriteLine(oCommon.isExist(strSQL2))
                Session("permata_admin") = txtLoginID.Text
                Session("userType") = "rappcs"

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case getUserProfile_UserType()
                    Case "Instruktor Ra PPCS"
                        Response.Redirect("admin.default.aspx")
                    Case "Ra PPCS"
                        Response.Redirect("admin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                End Select
#Enable Warning S1871 ' Two branches in a conditional structure should not have exactly the same implementation
            Else
                '--insert into security audit trail table
                Session.Clear()
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Invalid Login ID or Password! Please re-try."
            End If
        ElseIf usertype = "instruktor_ppcs" Then
            If oCommon.isExist(strSQL) = True Then
                Session.Clear()

                Session("permata_admin") = txtLoginID.Text
                Session("userType") = "instruktor_ppcs"

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case getUserProfile_UserType()
                    Case "Instruktor PPCS"
                        Response.Redirect("admin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                End Select


            ElseIf oCommon.isExist(strSQL2) = True Then
#Disable Warning S1871 ' Two branches in a conditional structure should not have exactly the same implementation
                Session.Clear()
                'Debug.WriteLine(oCommon.isExist(strSQL2))
                Session("permata_admin") = txtLoginID.Text
                Session("userType") = "instruktor_ppcs"

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case getUserProfile_UserType()
                    Case "Instruktor PPCS"
                        Response.Redirect("admin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                End Select
#Enable Warning S1871 ' Two branches in a conditional structure should not have exactly the same implementation
            Else
                '--insert into security audit trail table
                Session.Clear()
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Invalid Login ID or Password! Please re-try."
            End If
        ElseIf usertype = "instruktor_kpp" Then
            If oCommon.isExist(strSQL) = True Then
                Session.Clear()

                Session("permata_admin") = txtLoginID.Text
                Session("userType") = "instruktor_kpp"

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case getUserProfile_UserType()
                    Case "Instruktor KPP"
                        Response.Redirect("admin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                End Select


            ElseIf oCommon.isExist(strSQL2) = True Then
                Session.Clear()
                ''Debug.WriteLine(oCommon.isExist(strSQL2))
                Session("permata_admin") = txtLoginID.Text
                Session("userType") = "instruktor_kpp"

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case getUserProfile_UserType()
                    Case "Instruktor KPP"
                        Response.Redirect("admin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                End Select
            Else
                '--insert into security audit trail table
                Session.Clear()
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Invalid Login ID or Password! Please re-try."
            End If
        ElseIf usertype = "tappcs" Then
            If oCommon.isExist(strSQL) = True Then
                Session.Clear()
                ''Debug.WriteLine(oCommon.isExist(strSQL))
                Session("permata_admin") = txtLoginID.Text
                Session("userType") = "tappcs"

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case getUserProfile_UserType()
                    Case "Ta PPCS"
                        Response.Redirect("admin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                End Select


            ElseIf oCommon.isExist(strSQL2) = True Then
                Session.Clear()
                ''Debug.WriteLine(oCommon.isExist(strSQL2))
                Session("permata_admin") = txtLoginID.Text
                Session("userType") = "tappcs"

                '--set default language BM
                Response.Cookies("ppcs_culture").Value = "ms-MY"

                '--insert into security audit trail table
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

                ''get usertype
                Select Case getUserProfile_UserType()
                    Case "Ta PPCS"
                        Response.Redirect("admin.default.aspx")
                    Case Else
                        lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
                End Select
            Else
                '--insert into security audit trail table
                Session.Clear()
                oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Invalid Login ID or Password! Please re-try."
            End If
        Else
            lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
            divMsg.Attributes("class") = "error"
        End If

    End Sub

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim usrType As String
        Dim strRet1 As String
        Dim tmpSQL As String = "select top 1 staff_position from staff_info where staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' "
        Dim querySQL1 As String = "select top 1 staff_position from staff_info where staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' "


        If oCommon.isExist(strSQL) = True Then
            strRet = oCommon.getFieldValue(tmpSQL)
            ''Debug.WriteLine(strRet)
            usrType = strRet
        ElseIf oCommon.isExist(querySQL1) Then
            strRet1 = oCommon.getFieldValue(querySQL1)
            ''Debug.WriteLine(strRet1)
            usrType = strRet1
        End If

#Disable Warning BC42104 ' Variable is used before it has been assigned a value
        Return usrType
#Enable Warning BC42104 ' Variable is used before it has been assigned a value

    End Function

End Class