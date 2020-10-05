Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class pusatujian_petugas_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                State_list()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub State_list()
        ''strSQL = "SELECT DISTINCT schoolstate FROM schoolprofile ORDER BY SchoolState"
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) ORDER BY SchoolStateID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "schoolstate"
            ddlState.DataValueField = "schoolstate"
            ddlState.DataBind()

            ''ddlState.Items.Add(New ListItem("ALL", "ALL"))

            ''default state
            strRet = getUserProfile_State()
            ddlState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlState.Enabled = False
            End If
            ''debug
            'Response.Write(getUserProfile_State())

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        ''--validate screen
        If ValidatePage() = False Then
            Exit Sub
        End If

        strSQL = "INSERT INTO PusatUjian_Petugas (Fullname,ContactNo,Email,MYKAD,BankName,AccountNo,UserType,Address1,Address2,Postcode,City,State) VALUES('" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper.ToUpper) & "','" & oCommon.FixSingleQuotes(txtContactNo.Text) & "','" & oCommon.FixSingleQuotes(txtEmail.Text) & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "','" & oCommon.FixSingleQuotes(txtBankName.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtAccountNo.Text) & "','" & selUserType.Value & "','" & oCommon.FixSingleQuotes(txtAddress1.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtAddress2.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPostCode.Text) & "','" & oCommon.FixSingleQuotes(txtCity.Text.ToUpper) & "','" & ddlState.Text & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            '--ClearScreen()
            lblMsg.Text = "Successfully create Petugas Pusat Ujian UKM2."
        Else
            lblMsg.Text = "Error create Petugas. " & strRet
        End If

    End Sub

    Private Sub ClearScreen()
        txtMYKAD.Text = ""
        txtFullname.Text = ""
        txtContactNo.Text = ""
        txtEmail.Text = ""
        txtBankName.Text = ""
        txtAccountNo.Text = ""
        selUserType.SelectedIndex = 0

        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtPostCode.Text = ""
        txtCity.Text = ""
        ddlState.SelectedIndex = 0

    End Sub


    Private Function ValidatePage() As Boolean
        strSQL = "SELECT MYKAD FROM PusatUjian_Petugas WHERE MYKAD='" & txtMYKAD.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "MYKAD already exist. Petugas sudah didaftarkan."
            txtMYKAD.Focus()
            Return False
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Please fill in mandatory field."
            txtMYKAD.Focus()
            Return False
        End If

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Please fill in mandatory field."
            txtFullname.Focus()
            Return False

        End If

        If txtContactNo.Text.Length = 0 Then
            lblMsg.Text = "Please fill in mandatory field."
            txtContactNo.Focus()
            Return False

        End If

        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Please fill in mandatory field."
            txtEmail.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub lnkList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkList.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
            Case "SUBADMIN"
            Case "KPM"
            Case "JPN"
            Case "UKM"
                Response.Redirect("ukm.pusatujian.list.all.aspx")
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub
End Class