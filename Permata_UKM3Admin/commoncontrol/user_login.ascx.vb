Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources

Partial Public Class user_login
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

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
        ''If isBlockText(strSQL) = True Then
        ''    Return False
        ''End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim strConn1 As String = ConfigurationManager.AppSettings ("ConnectionString")
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
        Dim strSQL1 As String = ""
        Dim strSQL2 As String = ""


        strSQL = "select top 1 A.staff_position from ukm3.dbo.staff_info A WITH (NOLOCK) 
                    left join  kolejadmin.dbo.staff_Info B on  B.stf_id = A.staff_id    
                    left join kolejadmin.dbo.staff_Login C on C.stf_ID = B.stf_ID 
                where A.staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' and (A.staff_Password = '" & txtPwd.Text & "' or( C.staff_Password = '" & txtPwd.Text & "' AND c.staff_Access='INSTRUKTOR KPP')) and A.staff_session='Ukm 3 Examination-2018'"
        strSQL2 = "select  A.staff_position from ukm3.dbo.staff_info A 
                    left join permatapintar.dbo.PPCS_Users B on B.ppcsuserid=A.staff_id 
                where A.staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' and B.Pwd = '" & oDes.EncryptData(txtPwd.Text) & "' and A.staff_session = 'Ukm 3 Examination-2018'"
        Debug.WriteLine(strSQL)

        'isExistL(strSQL)
        'Exit Sub
        displayDebug(strSQL)
        If oCommon.isExist(strSQL) = True Then
            ''keep loginid of current user
            'Response.Cookies("permata_admin").Value = txtLoginID.Text
            'Response.Cookies("permata_admin").Expires = DateTime.Now.AddDays(30)

            Session("permata_admin") = txtLoginID.Text

            '--set default language BM
            Response.Cookies("ppcs_culture").Value = "ms-MY"

            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("admin.default.aspx")
                Case "Instruktor Ra PPCS"
                    Response.Redirect("admin.default.aspx")
                Case "Ra PPCS"
                    Response.Redirect("admin.default.aspx")
                Case "Instruktor PPCS"
                    Response.Redirect("admin.default.aspx")
                Case "Instruktor KPP"
                    Response.Redirect("admin.default.aspx")
                Case Else
                    lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
            End Select

            'ElseIf oCommon.isExist(strSQL1) = True Then
            '    Session("permata_ppcs") = txtLoginID.Text
            '    Response.Cookies("ppcs_culture").Value = "ms-MY"
            '    oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

            '    Select Case getUserProfile_UserType()
            '        Case "ADMIN"
            '            Response.Redirect("admin.default.aspx")
            '        Case "UKM3 - INSTRUKTOR Ra PPCS"
            '            Response.Redirect("admin.default.aspx")
            '        Case "Ra PPCS"
            '            Response.Redirect("admin.default.aspx")
            '        Case "UKM3 - INSTRUKTOR PPCS"
            '            Response.Redirect("admin.default.aspx")
            '        Case "Instruktor KPP"
            '            Response.Redirect("admin.default.aspx")
            '        Case Else
            '            lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
            '    End Select
        ElseIf oCommon.isExist(strSQL2) = True Then

            Session("permata_adminE") = txtLoginID.Text

            '--set default language BM
            Response.Cookies("ppcs_culture").Value = "ms-MY"

            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN", "NA")

            ''get usertype
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("admin.default.aspx")
                Case "Instruktor Ra PPCS"
                    Response.Redirect("admin.default.aspx")

                Case "Ra PPCS"
                    Response.Redirect("admin.default.aspx")
                Case "Instruktor PPCS"
                    Response.Redirect("admin.default.aspx")
                Case "Instruktor KPP"
                    Response.Redirect("admin.default.aspx")
                Case Else
                    lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
            End Select
        Else
            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtLoginID.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "LOGIN-FAILED", "NA")
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Invalid Login ID or Password! Please re-try."
        End If

    End Sub

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub

    Private Function getUserProfile_UserType() As String
        'Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        'strRet = oCommon.getFieldValue(tmpSQL)
        Dim usrType As String
        Dim strRet1 As String
        Dim strRet2 As String
        Dim tmpSQL As String = "select top 1 staff_position from staff_info where staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' "
        Dim querySQl As String = "SELECT A.UserType FROM permatapintar.dbo.UserProfile A WHERE A.LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
        Dim querySQL1 As String = "select top 1 staff_position from staff_info where staff_login = '" & oCommon.FixSingleQuotes(txtLoginID.Text) & "' "


        If oCommon.isExist(strSQL) = True Then
            strRet = oCommon.getFieldValue(tmpSQL)
            ''Debug.WriteLine(strRet)
            usrType = strRet
        ElseIf oCommon.isExist(querySQl) Then
            strRet1 = oCommon.getFieldValue(querySQl)
            ''Debug.WriteLine(strRet1)
            usrType = strRet1
        ElseIf oCommon.isExist(querySQl1) Then
            strRet2 = oCommon.getFieldValue(querySQL1)
            Debug.WriteLine(strRet2)
            usrType = strRet2
        End If

        'strRet = oCommon.getFieldValue(tmpSQL)
        'Dim stRet1 As String = oCommon.getFieldValue(querySQl)
        'Debug.WriteLine(strRet)
        'Debug.WriteLine(strRet1)
        'If strRet IsNot "" Then
        '    usrType = strRet
        'ElseIf strRet1 IsNot "" Then
        '    usrType = stRet1
        'End If
#Disable Warning BC42104 ' Variable is used before it has been assigned a value
        Return usrType
#Enable Warning BC42104 ' Variable is used before it has been assigned a value

        ''Debug.WriteLine(usrType)

    End Function

End Class