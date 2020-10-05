Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class pusatujian_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtPusatJumlahLab.Text = "1"

            SchoolState_list()
            examyear_list()
            ddlExamYear.Text = Now.Year
        End If

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

            ''ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))
            ''default state
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If
            ''debug
            'Response.Write(getUserProfile_State())

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":SchoolState_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case "UKM"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & ConfigurationManager.AppSettings("ExamYear") & "%'  ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & ConfigurationManager.AppSettings("ExamYear") & "' ORDER BY ExamYear"
        End Select
        '--debug
        'Response.Write("examyear_list:" & strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "ExamYear"
            ddlExamYear.DataBind()

            '-ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

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

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim strPusatcode As String = oCommon.getGUID

        ''--validate screen
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        strSQL = "INSERT INTO PusatUjian (PusatCode,PusatName,PusatAddress,PusatPostcode,PusatCity,PusatState,PusatType,PusatPPD,PusatNoTel,PusatNoFax,PusatEmail,PusatTahun,PusatJumlahLab,PusatJumlahKomp) VALUES('" & strPusatcode & "','" & oCommon.FixSingleQuotes(txtPusatName.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPusatAddress.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPusatPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtPusatCity.Text.ToUpper) & "','" & ddlSchoolState.Text & "','" & oCommon.FixSingleQuotes(txtPusatType.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPusatPPD.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPusatNoTel.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPusatNoFax.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPusatEmail.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(ddlExamYear.Text) & "','" & oCommon.FixSingleQuotes(txtPusatJumlahLab.Text) & "','" & oCommon.FixSingleQuotes(txtPusatJumlahKomp.Text) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya memasukkan rekod Pusat Ujian UKM2 yang baru."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = strRet
        End If


    End Sub


    Private Function ValidatePage() As Boolean
        If txtPusatName.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatName.Focus()
            Return False
        End If

        If txtPusatAddress.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatAddress.Focus()
            Return False
        End If

        If txtPusatPostcode.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatPostcode.Focus()
            Return False
        End If

        If IsNumeric(txtPusatPostcode.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja."
            txtPusatPostcode.Focus()
            Return False
        End If

        If txtPusatCity.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatCity.Focus()
            Return False
        End If

        If txtPusatJumlahLab.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatJumlahLab.Focus()
            Return False
        End If
        If IsNumeric(txtPusatJumlahLab.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja."
            txtPusatJumlahLab.Focus()
            Return False
        End If

        If txtPusatJumlahKomp.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatJumlahKomp.Focus()
            Return False
        End If
        If IsNumeric(txtPusatJumlahKomp.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja."
            txtPusatJumlahKomp.Focus()
            Return False
        End If


        Return True
    End Function

    Private Sub ClearScreen()
        txtPusatName.Text = ""
        txtPusatAddress.Text = ""
        txtPusatPostcode.Text = ""
        txtPusatCity.Text = ""
        ''selPusatState.Value = ""
        txtPusatType.Text = ""
        txtPusatNoTel.Text = ""
        txtPusatNoFax.Text = ""
        txtPusatJumlahLab.Text = 0
        txtPusatJumlahKomp.Text = ""

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        ClearScreen()

    End Sub
End Class