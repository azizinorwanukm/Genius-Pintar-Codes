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
                SchoolState_list()

            End If
        Catch ex As Exception

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

            ddlState.DataSource = ds
            ddlState.DataTextField = "schoolstate"
            ddlState.DataValueField = "schoolstate"
            ddlState.DataBind()

            ddlState.Items.Add(New ListItem("ALL", "ALL"))
            strRet = getUserProfile_State()
            ddlState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlState.Enabled = False
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

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
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
            lblMsg.Text = "Berjaya tambah Petugas Pusat Ujian UKM2."
        Else
            lblMsg.Text = "Gagal tambah Petugas. " & strRet
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
            lblMsg.Text = "Sila masukkan maklumat medan ini.."
            txtMYKAD.Focus()
            Return False
        End If

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan maklumat medan ini.."
            txtFullname.Focus()
            Return False

        End If

        If txtContactNo.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan maklumat medan ini.."
            txtContactNo.Focus()
            Return False

        End If

        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan maklumat medan ini.."
            txtEmail.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub lnkList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkList.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.list.all.aspx")
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.list.all.aspx")
            Case "UKM"
                Response.Redirect("ukm.pusatujian.list.all.aspx")
            Case "JPN"
                Response.Redirect("jpn.pusatujian.list.all.aspx")
            Case "KPM"
                Response.Redirect("kpm.pusatujian.list.all.aspx")
            Case "KPT"
                Response.Redirect("kpt.pusatujian.list.all.aspx")
            Case "MRSM"
                Response.Redirect("mara.pusatujian.list.all.aspx")
            Case "ASASI"
                Response.Redirect("asasi.pusatujian.list.all.aspx")
            Case Else
                lblMsg.Text = "Invalid usertype!"

        End Select

    End Sub

End Class