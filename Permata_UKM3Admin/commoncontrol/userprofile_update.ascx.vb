Imports System.Data.SqlClient

Partial Public Class userprofile_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                usertype_list()
                SchoolState_list()

                userprofile_load()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub usertype_list()
        strSQL = "SELECT usertype FROM master_UserType WHERE UserType<>'ADMIN' ORDER BY UserType"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUserType.DataSource = ds
            ddlUserType.DataTextField = "usertype"
            ddlUserType.DataValueField = "usertype"
            ddlUserType.DataBind()

            '--ddlUserType.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub SchoolState_list()
        ''strSQL = "SELECT DISTINCT schoolstate FROM schoolprofile ORDER BY SchoolState"
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) ORDER BY SchoolStateID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolState.DataSource = ds
            ddlSchoolState.DataTextField = "schoolstate"
            ddlSchoolState.DataValueField = "schoolstate"
            ddlSchoolState.DataBind()

            ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub userprofile_load()
        strSQL = "SELECT * FROM UserProfile Where UserProfileCode='" & Request.QueryString("userprofilecode") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    txtFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    txtFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    txtLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                    lblLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    txtLoginID.Text = ""
                End If

                '--password encrypted
                Dim strPwd As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    strPwd = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    strPwd = ""
                End If
                '--display actual password
                If strPwd.Length > 0 Then
                    txtPwd.Text = oDes.DecryptData(strPwd)
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UserType")) Then
                    ddlUserType.Text = ds.Tables(0).Rows(0).Item("UserType")
                Else
                    ddlUserType.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("State")) Then
                    ddlSchoolState.Text = ds.Tables(0).Rows(0).Item("State")
                Else
                    ddlSchoolState.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":userprofile_load:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''--UPDATE UKM1 also
        strSQL = "UPDATE UserProfile SET Fullname='" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "',LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "',Pwd='" & oDes.EncryptData(txtPwd.Text) & "',UserType='" & oCommon.FixSingleQuotes(ddlUserType.Text.ToUpper) & "',State='" & ddlSchoolState.Text & "' WHERE UserProfileCode='" & Request.QueryString("userprofilecode") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = " Succesfully update userprofile!"
        Else
            lblMsg.Text += " Update SchoolProfile fail:" & strRet
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Nama Pengguna"
            txtFullname.Focus()
            Return False
        End If

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Login ID"
            txtLoginID.Focus()
            Return False
        End If

        ''changes made on the login id. its unique
        If Not txtLoginID.Text = lblLoginID.Text Then
            strSQL = "SELECT * FROM UserProfile WHERE LoginID='" & txtLoginID.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Login ID sudah digunakan. Sila gunakan Login ID yang lain."
                Return False
            End If
        End If

        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Password"
            txtPwd.Focus()
            Return False
        End If

        If ddlUserType.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan UserType."
            ddlUserType.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.userprofile.list.aspx")
    End Sub
End Class