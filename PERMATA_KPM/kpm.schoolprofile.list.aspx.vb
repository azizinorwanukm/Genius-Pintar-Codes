Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class kpm_schoolprofile_list
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
          
        End If

    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Response.Redirect("kpm.schoolprofile.create.aspx")

    End Sub

    Private Sub btnUpdate_Schoolcity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate_Schoolcity.Click
        Response.Redirect("kpm.schoolprofile.schoolcity.update.aspx")

    End Sub
End Class