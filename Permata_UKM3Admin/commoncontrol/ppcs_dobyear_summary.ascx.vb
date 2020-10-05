Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ppcs_dobyear_summary
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                studentprofile_studentreligion_list()
                studentprofile_race_list()
                ddlStudentRace.Text = "ALL"

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                PPCSStatus_list()
                ddlPPCSStatus.Text = "LAYAK"
                ddlPPCSStatusBangsa.Text = "LAYAK"

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PPCSStatus_list()
        strSQL = "SELECT PPCSStatus FROM master_PPCSStatus ORDER BY PPCSStatus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--ddlPPCSStatus
            ddlPPCSStatus.DataSource = ds
            ddlPPCSStatus.DataTextField = "PPCSStatus"
            ddlPPCSStatus.DataValueField = "PPCSStatus"
            ddlPPCSStatus.DataBind()

            ddlPPCSStatus.Items.Add(New ListItem("ALL", "ALL"))

            '--ddlPPCSStatusBangsa
            ddlPPCSStatusBangsa.DataSource = ds
            ddlPPCSStatusBangsa.DataTextField = "PPCSStatus"
            ddlPPCSStatusBangsa.DataValueField = "PPCSStatus"
            ddlPPCSStatusBangsa.DataBind()

            ddlPPCSStatusBangsa.Items.Add(New ListItem("ALL", "ALL"))

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

    Private Sub examyear_list(ByVal ddlExamyear As DropDownList)
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

            ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub studentprofile_studentreligion_list()
        strSQL = "SELECT DISTINCT studentreligion FROM StudentProfile ORDER BY studentreligion"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentReligion.DataSource = ds
            ddlStudentReligion.DataTextField = "studentreligion"
            ddlStudentReligion.DataValueField = "studentreligion"
            ddlStudentReligion.DataBind()

            ddlStudentReligion.Items.Add(New ListItem("ALL", "ALL"))

            ''default state
            strRet = getUserProfile_State()
            ddlStudentReligion.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlStudentReligion.Enabled = False
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
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function BindData(ByVal gvTable As GridView, ByVal strExecSQL As String) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strExecSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod ditemui."
            Else
                lblMsg.Text = ""
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


    Private Sub dat_Age_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles dat_Age.SelectedIndexChanging
        Dim strKeyID As String = dat_Age.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.ppcs.dobyear.list.aspx?ppcsdate=" & ddlPPCSDate.Text & "&studentrace=" & ddlStudentRace.Text & "&dob_year=" & Server.UrlEncode(strKeyID) & "&ppcsstatus=" & ddlPPCSStatusBangsa.Text)
                Case "ADMINOP"
                    Response.Redirect("ppcs.dobyear.list.aspx?ppcsdate=" & ddlPPCSDate.Text & "&studentrace=" & ddlStudentRace.Text & "&dob_year=" & Server.UrlEncode(strKeyID) & "&ppcsstatus=" & ddlPPCSStatusBangsa.Text)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.ppcs.dobyear.list.aspx?ppcsdate=" & ddlPPCSDate.Text & "&studentrace=" & ddlStudentRace.Text & "&dob_year=" & Server.UrlEncode(strKeyID) & "&ppcsstatus=" & ddlPPCSStatusBangsa.Text)
                Case Else
            End Select
        Catch ex As Exception
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub studentprofile_race_list()
        strSQL = "SELECT DISTINCT(StudentRace) FROM StudentProfile ORDER BY StudentRace"
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


    Private Sub btnSearch01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch01.Click
        strRet = BindData(dat_Age, getSQL01)

    End Sub

    Private Function getSQL01() As String
        Dim strWhere As String = ""

        Dim strGroupby As String = " GROUP BY StudentProfile.DOB_Year ORDER BY StudentProfile.DOB_Year ASC"
        Dim tmpSQL As String = "SELECT DISTINCT StudentProfile.DOB_Year,YEAR(getdate()) - StudentProfile.DOB_Year as StudentAge,COUNT(*) AS Jumlah FROM PPCS, StudentProfile"
        strWhere += " WHERE PPCS.StudentID=StudentProfile.StudentID"

        '--PPCSDate
        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSDate='" & ddlPPCSDate.Text & "'"
        End If

        '--PPCSStatus
        If Not ddlPPCSStatusBangsa.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSStatus='" & ddlPPCSStatusBangsa.Text & "'"
        End If

        ''StudentRace
        If Not ddlStudentRace.Text = "ALL" Then
            strWhere += " AND StudentProfile.StudentRace='" & ddlStudentRace.Text & "'"
        End If

        getSQL01 = tmpSQL & strWhere & strGroupby
        ''debug
        'Response.Write(getSQL01)

        Return getSQL01
    End Function

    Private Sub btnSearch02_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch02.Click
        strRet = BindData(dat_Age_State, getSQL02)

    End Sub

    Private Function getSQL02() As String
        Dim strWhere As String = ""

        Dim strGroupby As String = " GROUP BY StudentProfile.DOB_Year ORDER BY StudentProfile.DOB_Year ASC"
        Dim tmpSQL As String = "SELECT DISTINCT StudentProfile.DOB_Year,YEAR(getdate()) - StudentProfile.DOB_Year as StudentAge,COUNT(*) AS Jumlah FROM PPCS, StudentProfile"
        strWhere += " WHERE PPCS.StudentID=StudentProfile.StudentID"

        '--PPCSDate
        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSDate='" & ddlPPCSDate.Text & "'"
        End If

        '--PPCSStatus
        If Not ddlPPCSStatus.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSStatus='" & ddlPPCSStatus.Text & "'"
        End If

        ''selDOB_Year
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND StudentProfile.DOB_Year='" & ddlDOB_Year.Text & "'"
        End If

        If Not ddlStudentReligion.Text = "ALL" Then
            strWhere += " AND StudentProfile.StudentReligion='" & ddlStudentReligion.Text & "'"
        End If

        getSQL02 = tmpSQL & strWhere & strGroupby
        ''debug
        'Response.Write(getSQL02)

        Return getSQL02
    End Function

End Class