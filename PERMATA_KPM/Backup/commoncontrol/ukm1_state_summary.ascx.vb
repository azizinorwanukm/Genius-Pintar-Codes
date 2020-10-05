Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_state_summary
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblDatetime.Text = Now.ToLongDateString & "  " & Now.ToShortTimeString
            examyear_list()

        End If

    End Sub

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & ConfigurationManager.AppSettings("ExamYear") & "' ORDER BY ExamYear"
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
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
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
                lblTotal.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblTotal.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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


    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblStudentSUM_NEW As Label
            Dim lblStudentSUM_DONE As Label
            Dim lblStudentSUM_ALL As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

            lblStudentSUM_NEW = e.Row.FindControl("StudentSUM_NEW")
            lblStudentSUM_NEW.Text = Schoolprofile_Count(strKeyID, "NEW")

            lblStudentSUM_DONE = e.Row.FindControl("StudentSUM_DONE")
            lblStudentSUM_DONE.Text = Schoolprofile_Count(strKeyID, "DONE")

            lblStudentSUM_ALL = e.Row.FindControl("StudentSUM_ALL")
            lblStudentSUM_ALL.Text = Schoolprofile_Count(strKeyID, "ALL")

        End If

    End Sub

    Private Function Schoolprofile_Count(ByVal strSchoolState As String, ByVal strStatus As String)
        Dim strWhere As String = ""
        Dim strOrderby As String = ""
        strSQL = "SELECT COUNT(*) FROM UKM1"
        strWhere = " WITH (NOLOCK) WHERE ExamYear='" & ddlExamYear.Text & "'"

        If Not strSchoolState = "ALL" Then
            strWhere += " AND SchoolState='" & strSchoolState & "'"
        End If

        If Not strStatus = "ALL" Then
            strWhere += " AND Status='" & strStatus & "'"
        End If

        strSQL = strSQL & strWhere & strOrderby
        strRet = oCommon.getFieldValue(strSQL)
        ''--debug
        'Response.Write(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "JPN"
                Response.Redirect("jpn.ukm1.school.students.aspx?schoolid=" & strKeyID)
            Case "KPM"
                Response.Redirect("kpm.ukm1.school.students.aspx?schoolid=" & strKeyID)
            Case "KPT"
                Response.Redirect("kpt.ukm1.school.students.aspx?schoolid=" & strKeyID)
            Case "MRSM"
                Response.Redirect("mrsm.ukm1.school.students.aspx?schoolid=" & strKeyID)
            Case "ASASI"
            Case Else
                lblMsg.Text = "Anda tidak layak menggunakan fungsi ini." & getUserProfile_UserType()
        End Select

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY SchoolStateID"

        tmpSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK)"
        strWhere = ""
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        lblTotal.Text = ""

        strRet = BindData(datRespondent)
    End Sub

End Class