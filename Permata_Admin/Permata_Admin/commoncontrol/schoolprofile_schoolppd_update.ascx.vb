Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class schoolprofile_schoolppd_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            SchoolState_list()
            schoolprofile_ppd_list()
        End If

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


    Private Sub schoolprofile_ppd_list()
        strSQL = "SELECT DISTINCT SchoolPPD FROM schoolprofile WHERE SchoolState='" & ddlSchoolState.Text & "' ORDER BY SchoolPPD"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolPPD.DataSource = ds
            ddlSchoolPPD.DataTextField = "SchoolPPD"
            ddlSchoolPPD.DataValueField = "SchoolPPD"
            ddlSchoolPPD.DataBind()

            'ddlSchoolPPD.Items.Add(New ListItem("ALL", "ALL"))
            'ddlSchoolPPD.SelectedValue = "ALL"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlSchoolState_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_ppd_list()
        txtSchoolPPD.Text = ""

    End Sub

    Private Function ValidatePage() As Boolean
        '--validate
        If ddlSchoolPPD.Text = "ALL" Then
            lblMsg.Text = "Sila pilih PPD yang ingin dikemaskini."
            ddlSchoolPPD.Focus()
            Return False
        End If

        If txtSchoolPPD.Text.Length = 0 Then
            txtSchoolPPD.Focus()
            lblMsg.Text = "Medan ini tidak dibenarkan kosong."
            Return False
        End If

        Return True
    End Function

    Protected Sub btnSchoolPPD_update_Click(sender As Object, e As EventArgs) Handles btnSchoolPPD_update.Click
        '--validate page
        If ValidatePage() = False Then
            Exit Sub
        End If

        '--kemaskini SchoolProfile
        strSQL = "UPDATE SchoolProfile WITH (UPDLOCK) SET SchoolPPD='" & oCommon.FixSingleQuotes(txtSchoolPPD.Text.ToUpper) & "' WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolPPD='" & ddlSchoolPPD.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini Bandar:[" & ddlSchoolPPD.Text & "] Nama Baru:[" & txtSchoolPPD.Text & "]"
        Else
            lblMsg.Text = "error:" & strRet
        End If

        '--kemaskini UKM1 juga
        strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET SchoolPPD='" & oCommon.FixSingleQuotes(txtSchoolPPD.Text.ToUpper) & "' WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolPPD='" & ddlSchoolPPD.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolPPD='" & oCommon.FixSingleQuotes(txtSchoolPPD.Text.ToUpper) & "' WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolPPD='" & ddlSchoolPPD.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini Bandar:[" & ddlSchoolPPD.Text & "] Nama Baru:[" & txtSchoolPPD.Text & "]"
        Else
            lblMsg.Text += "error:" & strRet
        End If
    End Sub

    Private Sub ddlSchoolPPD_TextChanged(sender As Object, e As EventArgs) Handles ddlSchoolPPD.TextChanged
        txtSchoolPPD.Text = ddlSchoolPPD.Text

    End Sub
End Class