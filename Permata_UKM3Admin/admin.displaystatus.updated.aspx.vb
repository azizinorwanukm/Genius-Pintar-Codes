Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class admin_displaystatus_updated
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--load examyear
                examyear_list(ddlExamYearUKM2)
                ddlExamYearUKM2.Text = oCommon.getAppsettings("DefaultExamYear")

                examyear_list(ddlExamYearUKM3)
                ddlExamYearUKM3.Text = oCommon.getAppsettings("DefaultExamYear")

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub examyear_list(ByVal ddlExamYear As DropDownList)
        strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"

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

            'ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcsdate_list()
        strSQL = "SELECT PPCSDate FROM master_PPCSDate ORDER BY ppcsid ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '--update if only selected
        If Not selDisplayStatusUKM2.Value = "" Then
            strSQL = "UPDATE UKM2 SET DisplayStatus='" & selDisplayStatusUKM2.Value & "' WHERE ExamYear='" & ddlExamYearUKM2.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "BERJAYA mengemaskini DisplayStatus UKM2.<br/>"
            End If
        End If

        If Not selDisplayStatusPPCS.Value = "" Then
            strSQL = "UPDATE PPCS SET DisplayStatus='" & selDisplayStatusPPCS.Value & "' WHERE PPCSDate='" & ddlPPCSDate.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text += "BERJAYA mengemaskini DisplayStatus PPCS."
            End If
        End If

        If Not selDisplayStatusUKM3.Value = "" Then
            strSQL = "UPDATE UKM3 SET DisplayStatus='" & selDisplayStatusUKM3.Value & "' WHERE PPYear='" & ddlExamYearUKM3.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "BERJAYA mengemaskini DisplayStatus UKM3.<br/>"
            End If
        End If

    End Sub

End Class