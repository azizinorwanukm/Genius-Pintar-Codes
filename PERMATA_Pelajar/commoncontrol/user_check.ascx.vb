Imports System.Data.SqlClient

Partial Public Class user_check
    Inherits System.Web.UI.UserControl

    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("culture") = "ms-MY"
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ''--validate page
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''Dr Siti request to add father or mother MYKAD# as password
        'strSQL = "SELECT MYKAD FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        Select Case selMYKAD.Value
            Case "1"    'ibu
                strSQL = "SELECT StudentProfile.MYKAD,ParentProfile.FatherMYKADNo,ParentProfile.MotherMYKADNo FROM StudentProfile,ParentProfile WHERE StudentProfile.StudentID=ParentProfile.StudentID AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "' AND ParentProfile.MotherMYKADNo='" & oCommon.FixSingleQuotes(txtMYKADSearch.Text) & "'"
            Case "2"    'bapa
                strSQL = "SELECT StudentProfile.MYKAD,ParentProfile.FatherMYKADNo,ParentProfile.MotherMYKADNo FROM StudentProfile,ParentProfile WHERE StudentProfile.StudentID=ParentProfile.StudentID AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "' AND ParentProfile.FatherMYKADNo='" & oCommon.FixSingleQuotes(txtMYKADSearch.Text) & "'"
            Case Else
                strSQL = ""
                lblMsg.Text = "Sila pilih jenis MYKAD untuk dijadikan kata laluan."
                Exit Sub
        End Select

        '--debug
        'Response.Write(strSQL)
        If oCommon.isExist(strSQL) = True Then
            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtMYKAD.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "PELAJAR_LOGIN", "NA")
            ''set initial cookies value
            Session("permata_studentid") = getStudentID()
            Session("permata_mykad") = txtMYKAD.Text

            Response.Redirect("default.main.aspx")
        Else
            '--insert into security audit trail table
            oCommon.LoginTrail(oCommon.FixSingleQuotes(txtMYKAD.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "PELAJAR_LOGIN_FAILED", "NA")
            '--reset cookies
            Session("permata_studentid") = ""
            Session("permata_mykad") = ""
            lblMsg.Text = "MYKAD/MYKID# Pelajar dan MYKAD# Ibu atau Bapa tidak ditemui di dalam pengkalan data."
        End If

    End Sub

    Private Function getStudentID() As String
        ''--get studentID
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)

        Return strStudentID
    End Function

    Private Function ValidatePage() As Boolean
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD/MYKID#"
            txtMYKAD.Focus()
            Return False
        End If

        If selMYKAD.Value = "0" Then
            lblMsg.Text = "Sila pilih jenis MYKAD untuk dijadikan kata laluan."
            selMYKAD.Focus()
            Return False
        End If


        'If oCommon.isNumeric(txtMYKAD.Text) = False Then
        '    lblMsg.Text = "Invalid MYKAD format. Fill in numbers only! [0 - 9]"
        '    txtMYKAD.Focus()
        '    Return False
        'End If

        Return True
    End Function
End Class