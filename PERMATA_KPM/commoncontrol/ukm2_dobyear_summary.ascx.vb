Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm2_dobyear_summary
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                examyear_list(ddlExamYear)
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                studentprofile_race_list()
                ddlStudentRace.Text = "ALL"

                studentprofile_studentreligion_list()
                ddlStudentReligion.Text = "ALL"

                '--SchoolState
                SchoolState_list()
                ddlSchoolState.Text = "ALL"

            End If
        Catch ex As Exception

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

            'ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

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
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub SchoolState_list()
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) WHERE SchoolState<>'UKM2-KPT' AND SchoolState <>'UKM2-ASASI'  ORDER BY SchoolStateID"
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

            ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If

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



    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub studentprofile_race_list()
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

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datDOB)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod ditemui."
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count.ToString
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
        Dim strWhere As String = ""

        Dim strGroupby As String = " GROUP BY b.DOB_Year ORDER BY b.DOB_Year ASC"
        Dim tmpSQL As String = "SELECT DISTINCT b.DOB_Year,YEAR(getdate()) - b.DOB_Year as StudentAge,COUNT(*) AS StudentTotal FROM UKM2 a, StudentProfile b, StudentSchool c, SchoolProfile d"
        strWhere += " WHERE a.StudentID=b.StudentID AND a.StudentID=c.StudentID AND c.SchoolID=d.SchoolID"

        '--ExamYear
        If Not ddlExamYear.Text = "ALL" Then
            strWhere += " AND a.ExamYear='" & ddlExamYear.Text & "'"
        End If

        ''--StudentRace
        If Not ddlStudentRace.Text = "ALL" Then
            strWhere += " AND b.StudentRace='" & ddlStudentRace.Text & "'"
        End If

        ''--StudentReligion
        If Not ddlStudentReligion.Text = "ALL" Then
            strWhere += " AND b.StudentReligion='" & ddlStudentReligion.Text & "'"
        End If

        ''--SchoolState
        If Not ddlSchoolState.Text = "ALL" Then
            strWhere += " AND d.SchoolState='" & ddlSchoolState.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strGroupby
        ''debug
        'Response.Write(getSQL)

        Return getSQL
    End Function

    Private Sub datDOB_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datDOB.SelectedIndexChanging
        Dim strKeyID As String = datDOB.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.ukm2.dobyear.list.aspx?examyear=" & ddlExamYear.Text & "&studentrace=" & ddlStudentRace.Text & "&dob_year=" & Server.UrlEncode(strKeyID))
                Case "SUBADMIN"
                    Response.Redirect("subadmin.ukm2.dobyear.list.aspx?examyear=" & ddlExamYear.Text & "&studentrace=" & ddlStudentRace.Text & "&dob_year=" & Server.UrlEncode(strKeyID))
                Case Else
            End Select
        Catch ex As Exception
        End Try

    End Sub
End Class