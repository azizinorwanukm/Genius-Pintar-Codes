﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm2_state_sort
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            examyear_list()
            ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")
        End If

    End Sub

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
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

            ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Senarai negeri tiada."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah pelajar bagi setiap negeri yang menduduki Ujian UKM2. Hanya negeri yang mempunyai pelajar dipaparkan."
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " GROUP BY SchoolState ORDER BY TotalStudent DESC"

        tmpSQL = "SELECT SchoolState,COUNT(SchoolState) as TotalStudent FROM UKM2"
        strWhere = " WITH (NOLOCK) WHERE ExamYear='" & ddlExamYear.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.ukm2.state.student.list.aspx?schoolstate=" & strKeyID & "&examyear=" & ddlExamYear.Text)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.ukm2.state.student.list.aspx?schoolstate=" & strKeyID & "&examyear=" & ddlExamYear.Text)
                Case Else

            End Select
        Catch ex As Exception

        End Try

    End Sub
End Class