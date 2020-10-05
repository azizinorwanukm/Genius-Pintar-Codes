Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class parentprofile_search
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ''--validate page
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        ''create parent info
        Response.Redirect("parentprofile.create.aspx?studentid=" & Request.QueryString("studentid") & "&mothermykadno=" & txtMotherMYKADNo.Text, False)
    End Sub

    Private Function ValidatePage() As Boolean
        If txtMotherMYKADNo.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. MYKAD Ibubapa/Penjaga!"
            txtMotherMYKADNo.Focus()
            Return False
        End If

        If oCommon.isNumeric(txtMotherMYKADNo.Text) = False Then
            lblMsg.Text = "Invalid MYKAD format. Fill in numbers only! [0 - 9]"
            txtMotherMYKADNo.Focus()
            Return False
        End If

        Return True
    End Function

End Class