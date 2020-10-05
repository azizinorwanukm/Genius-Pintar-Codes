Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_update
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
                schooltype_list()

                ''--load schoolprofile
                lblSchoolID.Text = Request.QueryString("schoolid")
                SchoolProfile_Load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub SchoolProfile_Load()
        strSQL = "Select * FROM SchoolProfile Where SchoolID='" & Request.QueryString("schoolid") & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolCode")) Then
                    txtSchoolCode.Text = ds.Tables(0).Rows(0).Item("SchoolCode")
                    lblSchoolCode.Text = ds.Tables(0).Rows(0).Item("SchoolCode")
                Else
                    txtSchoolCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolName")) Then
                    txtSchoolName.Text = ds.Tables(0).Rows(0).Item("SchoolName")
                Else
                    txtSchoolName.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolAddress")) Then
                    txtSchoolAddress.Text = ds.Tables(0).Rows(0).Item("SchoolAddress")
                Else
                    txtSchoolAddress.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolPostcode")) Then
                    txtSchoolPostcode.Text = ds.Tables(0).Rows(0).Item("SchoolPostcode")
                Else
                    txtSchoolPostcode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolCity")) Then
                    txtSchoolCity.Text = ds.Tables(0).Rows(0).Item("SchoolCity")
                Else
                    txtSchoolCity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolPPD")) Then
                    txtSchoolPPD.Text = ds.Tables(0).Rows(0).Item("SchoolPPD")
                Else
                    txtSchoolPPD.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolState")) Then
                    selSchoolState.Value = ds.Tables(0).Rows(0).Item("SchoolState")
                Else
                    selSchoolState.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolType")) Then
                    ddlSchoolType.Text = ds.Tables(0).Rows(0).Item("SchoolType")
                Else
                    ddlSchoolType.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolNoTel")) Then
                    txtSchoolNoTel.Text = ds.Tables(0).Rows(0).Item("SchoolNoTel")
                Else
                    txtSchoolNoTel.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolNoFax")) Then
                    txtSchoolNoFax.Text = ds.Tables(0).Rows(0).Item("SchoolNoFax")
                Else
                    txtSchoolNoFax.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolEmail")) Then
                    txtSchoolEmail.Text = ds.Tables(0).Rows(0).Item("SchoolEmail")
                Else
                    txtSchoolEmail.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SchoolLokasi")) Then
                    selSchoolLokasi.Value = ds.Tables(0).Rows(0).Item("SchoolLokasi")
                Else
                    selSchoolLokasi.Value = ""
                End If

            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try
    End Sub

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

    Private Function ValidatePage() As Boolean
        If txtSchoolCode.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Kod Sekolah"
            txtSchoolCode.Focus()
            Return False
        End If

        If Not txtSchoolCode.Text = lblSchoolCode.Text Then
            strSQL = "SELECT SchoolCode FROM SchoolProfile WHERE SchoolCode='" & txtSchoolCode.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "!Kod Sekolah sudah ujud"
                txtSchoolCode.Focus()
                Return False
            End If
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

        If selSchoolState.Value = "" Then
            lblMsg.Text = "!Sila pilih Negeri."
            selSchoolState.Focus()
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

    Private Function schoolprofile_update() As Boolean
        strSQL = "UPDATE SchoolProfile SET SchoolCode='" & oCommon.FixSingleQuotes(txtSchoolCode.Text.ToUpper) & "',SchoolName='" & oCommon.FixSingleQuotes(txtSchoolName.Text.ToUpper) & "',SchoolAddress='" & oCommon.FixSingleQuotes(txtSchoolAddress.Text.ToUpper) & "',SchoolPostcode='" & oCommon.FixSingleQuotes(txtSchoolPostcode.Text) & "',SchoolCity='" & oCommon.FixSingleQuotes(txtSchoolCity.Text.ToUpper) & "',SchoolState='" & selSchoolState.Value & "',SchoolType='" & ddlSchoolType.Text & "',SchoolPPD='" & oCommon.FixSingleQuotes(txtSchoolPPD.Text.ToUpper) & "',SchoolNoTel='" & oCommon.FixSingleQuotes(txtSchoolNoTel.Text) & "',SchoolNoFax='" & oCommon.FixSingleQuotes(txtSchoolNoFax.Text) & "',SchoolEmail='" & oCommon.FixSingleQuotes(txtSchoolEmail.Text) & "',SchoolLokasi='" & selSchoolLokasi.Value & "' WHERE SchoolID='" & lblSchoolID.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            Return True
        Else
            divMsg.Attributes("class") = "error"
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

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        ''--schoolprofile
        If schoolprofile_update() = True Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya kemaskini maklumat sekolah. schoolprofile_update"
        End If

    End Sub
End Class