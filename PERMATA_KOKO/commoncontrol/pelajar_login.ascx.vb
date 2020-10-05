Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Resources

Public Class pelajar_login1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim strUserType As String
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_tahun_list()
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub koko_tahun_list()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

            'ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        '--validate page form
        If ValidatePage() = False Then
            Exit Sub
        End If

        If isValidPwd() = False Then
            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtMYKAD.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "KOKO_LOGIN_FAILED", "NA")
            lblMsg.Text = "Kesilapan Login ID atau Kata Laluan. Sila cuba lagi..."
            Exit Sub
        End If

        Session("koko_loginid") = txtMYKAD.Text
        Session("koko_year") = ddlTahun.Text
        Response.Cookies("koko_usertype").Value = "PELAJAR"
        Response.Cookies("koko_culture").Value = "ms-MY"

        '--insert into security audit trail table
        oCommon.LoginTrail(oCommon.FixSingleQuotes(txtMYKAD.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "KOKO_LOGIN", "NA")

        ''get usertype
        Response.Redirect("pelajar/pelajar.login.succcess.aspx")

    End Sub

    Private Function isValidPwd() As Boolean
        ''Dr Siti request to add father or mother MYKAD# as password
        Select Case selMYKAD.Value
            Case "1"    'ibu
                strSQL = "SELECT StudentProfile.MYKAD,ParentProfile.FatherMYKADNo,ParentProfile.MotherMYKADNo FROM StudentProfile,ParentProfile WHERE StudentProfile.StudentID=ParentProfile.StudentID AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "' AND ParentProfile.MotherMYKADNo='" & oCommon.FixSingleQuotes(txtMYKADSearch.Text) & "'"
            Case "2"    'bapa
                strSQL = "SELECT StudentProfile.MYKAD,ParentProfile.FatherMYKADNo,ParentProfile.MotherMYKADNo FROM StudentProfile,ParentProfile WHERE StudentProfile.StudentID=ParentProfile.StudentID AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "' AND ParentProfile.FatherMYKADNo='" & oCommon.FixSingleQuotes(txtMYKADSearch.Text) & "'"
            Case Else
                strSQL = ""
                lblMsg.Text = "Sila pilih jenis MYKAD untuk dijadikan kata laluan."
                Return False
        End Select

        If oCommon.isExist(strSQL) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD/MYKID#!"
            txtMYKAD.Focus()
            Return False
        End If

        If selMYKAD.Value = "0" Then
            lblMsg.Text = "Sila pilih jenis MYKAD untuk dijadikan kata laluan."
            selMYKAD.Focus()
            Return False
        End If

        If txtMYKADSearch.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD Ibu atau Bapa."
            txtMYKADSearch.Focus()
            Return False
        End If

        '--check if exist as pelajar koko for the defaultyear
        Dim strStudentID As String = ""
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        strStudentID = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT StudentID FROM koko_pelajar WHERE StudentID='" & strStudentID & "' AND Tahun='" & ddlTahun.Text & "' AND StatusTawaran='TERIMA'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Rekod anda tidak terdapat di dalam senarai KOKO Tahun:" & ddlTahun.Text & " atau anda MENOLAK tawaran ini."
            Return False
        End If

        Return True
    End Function


End Class