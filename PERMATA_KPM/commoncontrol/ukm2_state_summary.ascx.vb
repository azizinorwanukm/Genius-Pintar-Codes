Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm2_state_summary
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
                lblMsg.Text = "Senarai negeri tiada"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah pelajar bagi setiap negeri yang menduduki Ujian UKM2."
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

        strSQL = getSQL()
        strRet = BindData(datRespondent)
    End Sub


    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim isHadir_N As Label
            Dim isHadir_NB As Label
            Dim isHadir_NLB As Label

            Dim isHadir_Y As Label
            Dim isHadir_YB As Label
            Dim isHadir_YLB As Label

            Dim isHadir_ALL As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

            isHadir_N = e.Row.FindControl("isHadir_N")
            isHadir_N.Text = Schoolprofile_Count(strKeyID, "N", "ALL")

            isHadir_NB = e.Row.FindControl("isHadir_NB")
            isHadir_NB.Text = Schoolprofile_Count(strKeyID, "N", "B")

            isHadir_NLB = e.Row.FindControl("isHadir_NLB")
            isHadir_NLB.Text = Schoolprofile_Count(strKeyID, "N", "LB")

            isHadir_Y = e.Row.FindControl("isHadir_Y")
            isHadir_Y.Text = Schoolprofile_Count(strKeyID, "Y", "ALL")

            isHadir_YB = e.Row.FindControl("isHadir_YB")
            isHadir_YB.Text = Schoolprofile_Count(strKeyID, "Y", "B")

            isHadir_YLB = e.Row.FindControl("isHadir_YLB")
            isHadir_YLB.Text = Schoolprofile_Count(strKeyID, "Y", "LB")


            isHadir_ALL = e.Row.FindControl("isHadir_ALL")
            isHadir_ALL.Text = Schoolprofile_Count(strKeyID, "ALL", "ALL")

        End If

    End Sub

    Private Function Schoolprofile_Count(ByVal strSchoolState As String, ByVal strHadir As String, ByVal strSchoolLokasi As String)
        Dim strWhere As String = ""
        Dim strOrderby As String = ""
        strSQL = "SELECT COUNT(*) FROM UKM2 WHERE ExamYear='" & ddlExamYear.Text & "'"

        If Not strSchoolState = "ALL" Then
            strWhere += " AND SchoolState='" & strSchoolState & "'"
        End If

        If Not strHadir = "ALL" Then
            strWhere += " AND isHadir='" & strHadir & "'"
        End If

        If Not strSchoolLokasi = "ALL" Then
            strWhere += " AND SchoolLokasi='" & strSchoolLokasi & "'"
        End If

        strSQL = strSQL & strWhere & strOrderby
        strRet = oCommon.getFieldValue(strSQL)
        ''--debug
        'Response.Write(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("ukm1.school.students.aspx?schoolid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY SchoolStateID"

        tmpSQL = "SELECT SchoolState FROM SchoolState"
        strWhere = ""
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub datRespondent_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles datRespondent.SelectedIndexChanged

    End Sub
End Class