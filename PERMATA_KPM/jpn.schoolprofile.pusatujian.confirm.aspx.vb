Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class jpn_schoolprofile_pusatujian_confirm
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            examyear_list()
            ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
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

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Dim myAray As Array
        strRet = "||||||||||||"
        myAray = strRet.Split("|")

        ''--validate
        If ValidatePage() = False Then
            Exit Sub
        End If

        strSQL = "SELECT SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType,SchoolNoTel,SchoolNoFax,SchoolLokasi,SchoolEmail,SchoolPPD FROM SchoolProfile WHERE SchoolID='" & Request.QueryString("schoolid") & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        If Not strRet.Length = 0 Then
            myAray = strRet.Split("|")
        End If

        ''--new code
        Dim strPusatCode As String = System.Guid.NewGuid.ToString

        strSQL = "INSERT INTO PusatUjian (PusatCode,PusatName,PusatAddress,PusatPostcode,PusatCity,PusatState,PusatType,PusatNoTel,PusatNoFax,PusatLokasi,PusatEmail,PusatPPD,PusatTahun,PusatJumlahLab,PusatJumlahKomp) VALUES('" & strPusatCode & "','" & oCommon.FixSingleQuotes(myAray(0)) & "','" & oCommon.FixSingleQuotes(myAray(1)) & "','" & oCommon.FixSingleQuotes(myAray(2)) & "','" & oCommon.FixSingleQuotes(myAray(3)) & "','" & oCommon.FixSingleQuotes(myAray(4)) & "','" & oCommon.FixSingleQuotes(myAray(5)) & "','" & oCommon.FixSingleQuotes(myAray(6)) & "','" & oCommon.FixSingleQuotes(myAray(7)) & "','" & oCommon.FixSingleQuotes(myAray(8)) & "','" & oCommon.FixSingleQuotes(myAray(9)) & "','" & oCommon.FixSingleQuotes(myAray(10)) & "','" & ddlExamYear.Text & "'," & oCommon.FixSingleQuotes(txtJumLab.Text) & "," & oCommon.FixSingleQuotes(txtJumKomp.Text) & ")"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya memasukkan sekolah tersebut sebagai Pusat Ujian bagi tahun " & ddlExamYear.Text
        Else
            lblMsg.Text = "error:" & strRet
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        strSQL = "SELECT SchoolID FROM PusatUjian WHERE SchoolID='" & Request.QueryString("schoolid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Sekolah tersebut sudah dimasukkan sebagai Pusat Ujian bagi tahun " & ddlExamYear.Text
            Return False
        End If

        ''lab
        If txtJumLab.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan nombor sekurang-kurangnya 1."
            txtJumLab.Focus()
            Return False
        End If
        If oCommon.isNumeric(txtJumLab.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja."
            txtJumLab.Focus()
            Return False
        End If

        ''komp
        If txtJumKomp.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan nombor sekurang-kurangnya 1."
            txtJumKomp.Focus()
            Return False
        End If
        If oCommon.isNumeric(txtJumKomp.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja."
            txtJumKomp.Focus()
            Return False
        End If

        Return True
    End Function


    Protected Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.schoolprofile.pusatujian.select.aspx")
            Case "SUBADMIN"
                Response.Redirect("subadmin.schoolprofile.pusatujian.select.aspx")
            Case "UKM"
                Response.Redirect("ukm.schoolprofile.pusatujian.select.aspx")
            Case "JPN"
                Response.Redirect("jpn.schoolprofile.pusatujian.select.aspx")
            Case "KPM"
                Response.Redirect("kpm.schoolprofile.pusatujian.select.aspx")
            Case "KPT"
                Response.Redirect("kpt.schoolprofile.pusatujian.select.aspx")
            Case "MRSM"
                Response.Redirect("mara.schoolprofile.pusatujian.select.aspx")
            Case "ASASI"
                Response.Redirect("asasi.schoolprofile.pusatujian.select.aspx")
            Case Else
                lblMsg.Text = "Invalid usertype!"
        End Select

    End Sub
End Class