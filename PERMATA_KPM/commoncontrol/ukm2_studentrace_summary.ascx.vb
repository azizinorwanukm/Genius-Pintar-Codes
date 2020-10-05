Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm2_studentrace_summary1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                master_StudentRace_list()

                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub master_dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear ORDER BY DOB_Year"
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

    Private Sub master_StudentRace_list()
        strSQL = "SELECT StudentRace FROM master_StudentRace ORDER BY StudentRace"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentRace.DataSource = ds
            ddlStudentRace.DataTextField = "studentrace"
            ddlStudentRace.DataValueField = "studentrace"
            ddlStudentRace.DataBind()

            ddlStudentRace.Items.Add(New ListItem("ALL", "ALL"))

            ''default state
            strRet = getUserProfile_State()
            ddlStudentRace.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlStudentRace.Enabled = False
            End If
            ''debug
            'Response.Write(getUserProfile_State())

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":SchoolState_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function BindData(ByVal gvTable As GridView, ByVal strmySQL As String) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strmySQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Senarai bangsa tiada"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah pelajar bagi setiap bangsa yang menduduki Ujian UKM2."
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

        strRet = BindData(datRespondent, getSQL)
    End Sub

    Private Function getSQL() As String
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strGroupby As String = " GROUP BY b.StudentRace"
        Dim strOrder As String = " ORDER BY nTotal DESC"

        tmpSQL = "SELECT b.StudentRace, count(*) as nTotal FROM UKM2 a,StudentProfile b"
        strWhere += " WHERE a.StudentID=b.StudentID"

        If Not ddlExamYear.Text = "ALL" Then
            strWhere += " AND ExamYear='" & ddlExamYear.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strGroupby & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL
    End Function

    Private Sub btnSearchRace_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchRace.Click
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strGroupBy As String = " GROUP BY a.SchoolState"
        Dim strOrder As String = " ORDER BY TotalStudent DESC"

        tmpSQL = "SELECT a.SchoolState,COUNT(a.SchoolState) as TotalStudent FROM UKM2 a,StudentProfile b"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.ExamYear='" & ddlExamYear.Text & "'"

        ''StudentReligion
        If Not ddlStudentRace.Text = "ALL" Then
            strWhere += " AND b.StudentRace='" & ddlStudentRace.Text & "'"
        End If

        ''Age
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND b.DOB_Year='" & ddlDOB_Year.Text & "'"
        End If

        strSQL = tmpSQL & strWhere & strGroupBy & strOrder
        ''debug
        'Response.Write(strSQL)
        strRet = BindData(dat_Race_State, strSQL)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ''default screen on top

        strRet = BindData(datRespondent, getSQL)

    End Sub
End Class