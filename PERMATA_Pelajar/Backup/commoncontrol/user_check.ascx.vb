Imports System.Data
Imports System.Data.OleDb
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
            Session("culture") = "ms-MY"
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ''--validate page
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        ''--existing user and change password done
        strSQL = "SELECT MYKAD FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        If oCommon.isExist(strSQL) = True Then
            Response.Redirect("default.main.aspx?studentid=" & getStudentID(), True)
        Else
            Response.Redirect("studentprofile.create.aspx?mykad=" & oCommon.FixSingleQuotes(txtMYKAD.Text), True)
        End If

    End Sub

    Private Function getStudentID() As String
        ''--get studentID
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        ''set initial cookies value
        Response.Cookies("studentid").Value = strRet
        Return strRet

    End Function

    Private Function ValidatePage() As Boolean
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. MYKAD/MYKID#"
            txtMYKAD.Focus()
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