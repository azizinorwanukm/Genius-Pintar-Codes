Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm2_semak
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            examyear_list()
            ddlExamYear.Text = Now.Year

        End If
    End Sub

    Private Sub examyear_list()
        strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE LEN(ExamYear)=4 ORDER BY ExamYear"

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

            '--ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "' AND Status='DONE'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Rekod Ujian UKM2 anda tidak dijumpai atau anda belum menamatkan ujian tersebut bagi tahun " & ddlExamYear.Text
            Exit Sub
        End If

        Response.Redirect("ukm2.permata.end.aspx?studentid=" & Request.QueryString("studentid") & "&examyear=" & ddlExamYear.Text)
    End Sub

End Class