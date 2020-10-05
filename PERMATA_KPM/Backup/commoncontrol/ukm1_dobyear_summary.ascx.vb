Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_dobyear_summary
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

                studentprofile_studentreligion_list()
                ddlStudentReligion.Text = "ALL"

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"
            End If
        Catch ex As Exception

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

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub master_dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear WITH (NOLOCK) ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDOB_Year.DataSource = ds
            ddlDOB_Year.DataTextField = "DOB_Year"
            ddlDOB_Year.DataValueField = "DOB_Year"
            ddlDOB_Year.DataBind()

            ddlDOB_Year.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub studentprofile_studentreligion_list()
        strSQL = "SELECT StudentReligion FROM master_StudentReligion ORDER BY StudentReligion"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentReligion.DataSource = ds
            ddlStudentReligion.DataTextField = "StudentReligion"
            ddlStudentReligion.DataValueField = "StudentReligion"
            ddlStudentReligion.DataBind()

            ddlStudentReligion.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function BindData(ByVal gvTable As GridView, ByVal strQuery As String) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strQuery, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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

    Private Function getSQL() As String
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strGroupby As String = " GROUP BY b.DOB_Year"
        Dim strOrder As String = " ORDER BY b.DOB_Year"

        tmpSQL = "SELECT b.DOB_year, COUNT(*) as nTotal FROM UKM1 a,StudentProfile b"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID"
        getSQL = tmpSQL & strWhere & strGroupby & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL
    End Function


    Private Sub btnSearchState_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchState.Click
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strGroupBy As String = " GROUP BY SchoolState"
        Dim strOrder As String = " ORDER BY TotalStudent DESC"

        tmpSQL = "SELECT SchoolState,COUNT(SchoolState) as TotalStudent FROM UKM1 a,StudentProfile b"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND ExamYear='" & ddlExamYear.Text & "'"

        ''StudentReligion
        If Not ddlStudentReligion.Text = "ALL" Then
            strWhere += " AND b.StudentReligion='" & ddlStudentReligion.Text & "'"
        End If

        ''Age
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND b.DOB_Year='" & ddlDOB_Year.Text & "'"
        End If

        strSQL = tmpSQL & strWhere & strGroupBy & strOrder
        ''debug
        'Response.Write(strSQL)

        strRet = BindData(dat_Age_State, strSQL)

    End Sub

    Private Sub dat_Age_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles dat_Age.SelectedIndexChanging
        Dim strKeyID As String = dat_Age.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
            Case "SUBADMIN"
            Case "JPN"
            Case "KPM"
                Response.Redirect("kpm.ukm1.dobyear.list.aspx?examyear=" & ddlExamYear.Text & "&dob_year=" + Server.UrlEncode(strKeyID))
            Case Else
                lblMsg.Text = "Invalid user type."
        End Select

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strSQL = "SELECT DISTINCT b.DOB_Year,YEAR(getdate()) - b.DOB_Year as StudentAge,COUNT(*) AS StudentTotal FROM UKM1 a, StudentProfile b WHERE a.ExamYear='" & ddlExamYear.Text & "' AND (a.StudentID=b.StudentID) GROUP BY b.DOB_Year ORDER BY b.DOB_Year ASC"
        strRet = BindData(dat_Age, strSQL)

    End Sub
End Class