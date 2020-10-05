Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ppcs_studentrace_summary1
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

                ppcsdate_list()
                ddlPPCSDate.Text = ConfigurationManager.AppSettings("DefaultPPCSDate")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"
            End If

        Catch ex As Exception

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

            'ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
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
            ddlStudentRace.DataTextField = "StudentRace"
            ddlStudentRace.DataValueField = "StudentRace"
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
                lblMsg.Text = "Rekod tidak dijumpai."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY Jumlah DESC"
        Dim strGroupby As String = " GROUP BY StudentProfile.StudentRace"

        'tmpSQL = "SELECT b.StudentRace, count(*) as nTotal FROM PPCS a,StudentProfile b"
        'strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.PPCSStatus='LAYAK'"

        tmpSQL = "SELECT StudentProfile.StudentRace, COUNT(*) as Jumlah FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        strWhere = " WHERE PPCS.PPCSStatus='LAYAK' AND PPCS.PPCSDate ='" & ddlPPCSDate.Text & "'"

        getSQL = tmpSQL & strWhere & strGroupby & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL
    End Function

    Private Sub btnSearchRace_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchRace.Click
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strGroupBy As String = " GROUP BY c.SchoolState"
        Dim strOrder As String = " ORDER BY Jumlah DESC"

        tmpSQL = "SELECT c.SchoolState,COUNT(c.SchoolState) as Jumlah FROM PPCS a,StudentSchool b,SchoolProfile c,StudentProfile d"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.StudentID=d.StudentID AND b.SchoolID=c.SchoolID AND a.PPCSStatus='LAYAK'"

        '--PPCSDate
        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND a.PPCSDate='" & ddlPPCSDate.Text & "'"
        End If

        ''StudentRace
        If Not ddlStudentRace.Text = "ALL" Then
            strWhere += " AND d.StudentRace='" & ddlStudentRace.Text & "'"
        End If

        ''Age
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND d.DOB_Year='" & ddlDOB_Year.Text & "'"
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