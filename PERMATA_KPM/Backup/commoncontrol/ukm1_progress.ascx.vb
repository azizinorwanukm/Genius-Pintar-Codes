Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_progress1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblDatetime.Text = Now.ToLongDateString & "  " & Now.ToShortTimeString
                examyear_list()
                schoolprofile_state_list()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub schoolprofile_state_list()
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) ORDER BY SchoolStateID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolState.DataSource = ds
            ddlSchoolState.DataTextField = "schoolstate"
            ddlSchoolState.DataValueField = "schoolstate"
            ddlSchoolState.DataBind()


            '--ddlSchoolState
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If
            ''debug
            'Response.Write(getUserProfile_State())

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

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

    Private Sub getTotal()
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT COUNT(*) FROM UKM1"
        strWhere = " WITH (NOLOCK) WHERE ExamYear='" & ddlExamYear.Text & "'"

        '--SchoolState
        If Not ddlSchoolState.Text = "ALL" Then
            strWhere += " AND SchoolState='" & ddlSchoolState.Text & "'"
        End If

        strSQL = tmpSQL & strWhere & strOrder
        lblTotal.Text = "<b>" & ddlExamYear.Text & ":" & oCommon.getFieldValue(strSQL) & " Negeri:" & ddlSchoolState.Text & "<b/>"

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        lblTotal.Text = ""

        strRet = BindData(datRespondent)

    End Sub

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

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "JPN"
                Response.Redirect("jpn.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "KPM"
                Response.Redirect("kpm.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "KPT"
                Response.Redirect("kpt.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "MRSM"
                Response.Redirect("mrsm.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "ASASI"
            Case Else
                lblMsg.Text = "Anda tidak layak menggunakan fungsi ini." & getUserProfile_UserType()
        End Select

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY a.ExamEnd DESC"

        Try
            tmpSQL = "SELECT b.Studentfullname,b.MYKAD,b.DOB_Year,a.SchoolState,a.StudentID,a.ExamStart,a.ExamEnd,a.Status,a.Lastpage,a.QuestionYear,c.SchoolCode,c.Schoolname FROM UKM1 a, StudentProfile b, SchoolProfile c"
            strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.SchoolID=c.SchoolID"

            '--ddlExamYear
            If Not ddlExamYear.Text = "ALL" Then
                strWhere += " AND a.ExamYear='" & ddlExamYear.Text & "'"
            End If

            ''status
            If Not selStatus.Value = "ALL" Then
                strWhere += " AND a.Status='" & selStatus.Value & "'"
            End If

            ''SchoolType. for MRSM only
            If getUserProfile_UserType() = "MRSM" Then
                strWhere += " AND a.SchoolType='MRSM'"
            End If

            '--SchoolState
            If Not ddlSchoolState.Text = "ALL" Then
                strWhere += " AND a.SchoolState='" & ddlSchoolState.Text & "'"
            End If

            ''student fullname
            If Not txtStudentFullname.Text.Length = 0 Then
                strWhere += " AND b.Studentfullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text.Trim) & "%'"
            End If

            '--MYKAD
            If Not txtMYKAD.Text.Length = 0 Then
                strWhere += " AND b.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text.Trim) & "'"
            End If

            getSQL = tmpSQL & strWhere & strOrder

            ''--debug
            'Response.Write(getSQL)
            Return getSQL

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class