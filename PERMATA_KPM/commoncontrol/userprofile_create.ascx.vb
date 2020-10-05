Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class userprofile_create
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
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub usertype_list()
        strSQL = "SELECT usertype FROM master_UserType ORDER BY UserType"
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
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) WHERE SchoolState<>'UKM2-KPT' AND SchoolState <>'UKM2-ASASI'  ORDER BY SchoolStateID"
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

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        '--insert into UserProfile
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        ''--UPDATE UKM1 also
        strSQL = "INSERT INTO UserProfile (Fullname,LoginID,Pwd,UserType,State) VALUES ('" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & oDes.EncryptData(txtPwd.Text) & "','" & oCommon.FixSingleQuotes(ddlUserType.Text.ToUpper) & "','" & ddlSchoolState.Text & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = " Succesfully INSERT userprofile!"
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text += " INSERT SchoolProfile fail:" & strRet
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Nama Pelajar"
            txtFullname.Focus()
            Return False
        End If

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Login ID"
            txtLoginID.Focus()
            Return False
        End If

        strSQL = "SELECT * FROM UserProfile WHERE LoginID='" & txtLoginID.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Login ID sudah digunakan. Sila gunakan Login ID yang lain."
            Return False
        End If

        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Password"
            txtPwd.Focus()
            Return False
        End If

        If ddlUserType.Text.Length = 0 Then
            lblMsg.Text = "!Sila pilih UserType."
            ddlUserType.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnList.Click
        Response.Redirect("admin.userprofile.list.aspx")

    End Sub
End Class