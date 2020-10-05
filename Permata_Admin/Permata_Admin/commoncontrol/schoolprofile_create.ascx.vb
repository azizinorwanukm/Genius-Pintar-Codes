Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ''--dropdown
                SchoolState_list()
                ddlSchoolState.Text = "LAIN-LAIN"

                schooltype_list()
                ddlSchoolType.Text = "LAIN-LAIN"

                lblSchoolID.Text = oCommon.getGUID
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub SchoolState_list()
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
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub schooltype_list()
        strSQL = "SELECT schooltype FROM schooltype ORDER BY schooltypeid"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolType.DataSource = ds
            ddlSchoolType.DataTextField = "schooltype"
            ddlSchoolType.DataValueField = "schooltype"
            ddlSchoolType.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''--schoolprofile
        If SchoolProfile_create() = True Then
            lblMsg.Text = "Berjaya memasukkan maklumat sekolah anda. SchoolProfile_create"
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        If txtSchoolCode.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Kod Sekolah"
            txtSchoolCode.Focus()
            Return False
        End If

        strSQL = "SELECT SchoolCode FROM SchoolProfile WHERE SchoolCode='" & txtSchoolCode.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "!Kod Sekolah sudah ujud"
            txtSchoolCode.Focus()
            Return False
        End If

        If txtSchoolName.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Nama Sekolah"
            txtSchoolName.Focus()
            Return False
        End If

        If txtSchoolAddress.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Alamat Sekolah"
            txtSchoolAddress.Focus()
            Return False
        End If

        If txtSchoolPostcode.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Poskod."
            txtSchoolPostcode.Focus()
            Return False
        End If

        If oCommon.isNumeric(txtSchoolPostcode.Text) = False Then
            txtSchoolPostcode.Focus()
            lblMsg.Text = "!Nombor sahaja. 0 - 9"
            Return False
        End If

        If txtSchoolCity.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Bandar."
            txtSchoolCity.Focus()
            Return False
        End If

        If txtSchoolPPD.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan PPD."
            txtSchoolPPD.Focus()
            Return False
        End If

        If ddlSchoolState.Text = "" Then
            lblMsg.Text = "!Sila pilih Negeri."
            ddlSchoolState.Focus()
            Return False
        End If

        If ddlSchoolType.Text = "" Then
            lblMsg.Text = "!Sila pilih Jenis Sekolah."
            ddlSchoolType.Focus()
            Return False
        End If

        If txtSchoolNoTel.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Tel.#"
            txtSchoolNoTel.Focus()
            Return False
        End If

        If txtSchoolNoFax.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Fax#"
            txtSchoolNoFax.Focus()
            Return False
        End If

        If Not txtSchoolEmail.Text.Length = 0 Then
            If oCommon.isEmail(txtSchoolEmail.Text) = False Then
                lblMsg.Text = "!Format Email tidak sah"
                txtSchoolEmail.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Function SchoolProfile_create() As Boolean
        strSQL = "INSERT INTO SchoolProfile (SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType,SchoolPPD,SchoolNoTel,SchoolNoFax,SchoolEmail,SchoolLokasi,CreateBy,CreateDate,IsDeleted,SchoolCountry) VALUES('" & lblSchoolID.Text & "','" & oCommon.FixSingleQuotes(txtSchoolCode.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtSchoolName.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtSchoolAddress.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtSchoolPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtSchoolCity.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(ddlSchoolState.Text) & "','" & oCommon.FixSingleQuotes(ddlSchoolType.Text) & "','" & oCommon.FixSingleQuotes(txtSchoolPPD.Text) & "','" & oCommon.FixSingleQuotes(txtSchoolNoTel.Text) & "','" & oCommon.FixSingleQuotes(txtSchoolNoFax.Text) & "','" & oCommon.FixSingleQuotes(txtSchoolEmail.Text) & "','" & selSchoolLokasi.Value & "','KPM','" & oCommon.getToday & "','N','" & selSchoolCountry.Value & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "Error:" & strRet
            Return False
        End If

    End Function


    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RefreshPage()

    End Sub

    Private Sub RefreshPage()
        txtSchoolAddress.Text = ""
        txtSchoolCity.Text = ""
        txtSchoolCode.Text = ""
        txtSchoolEmail.Text = ""
        txtSchoolName.Text = ""
        txtSchoolNoFax.Text = ""
        txtSchoolNoTel.Text = ""
        txtSchoolPostcode.Text = ""
        txtSchoolPPD.Text = ""
        lblSchoolID.Text = ""

        lblSchoolID.Text = oCommon.getGUID
        txtSchoolCode.Focus()
    End Sub
End Class