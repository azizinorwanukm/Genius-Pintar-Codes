Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Data.OleDb
'Imports System.IO
'Imports System.Globalization

Partial Public Class schoolprofile_state_select
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim studentID As String = Request.QueryString("studentid")

        If Common.isUKM1Done(studentID) Then
            Response.Redirect("default.main.aspx?lang=ms-MY&studentid=" & studentID)
        End If

        Try
            If Not IsPostBack Then
                SchoolState_list()
                ddlSchoolState.Text = "--Pilih Negeri--"

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SchoolState_list()
        ''strSQL = "SELECT DISTINCT State FROM schoolprofile ORDER BY SchoolState"
        strSQL = "SELECT State FROM master_State ORDER BY stateid"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolState.DataSource = ds
            ddlSchoolState.DataTextField = "State"
            ddlSchoolState.DataValueField = "State"
            ddlSchoolState.DataBind()

            '--add blank
            ddlSchoolState.Items.Add(New ListItem("--Pilih Negeri--", "--Pilih Negeri--"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
        If ddlSchoolState.Text = "--Pilih Negeri--" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila pilih negeri di mana sekolah anda berada."
            Exit Sub
        End If

        Response.Redirect("schoolprofile.search.select.aspx?schoolstate=" & ddlSchoolState.Text & "&lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))

    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click
        Response.Redirect("default.main.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))

    End Sub
End Class