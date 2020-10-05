Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            SchoolState_list()
            schoolprofile_PPD_list()
            schoolprofile_city_list()

            examyear_list()
            ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")
        End If

    End Sub

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "ADMINOP"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case "UKM"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & oCommon.getAppsettings("DefaultExamYear") & "%'  ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
        End Select

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

            '--ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub SchoolState_list()
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
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub schoolprofile_PPD_list()
        '-user can select ALL for state
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY SchoolPPD"

        strSQL = "SELECT DISTINCT SchoolPPD FROM SchoolProfile WHERE SchoolPPD<>'' AND IsDeleted<>'Y'"
        strWhere = " AND SchoolState='" & ddlSchoolState.Text & "'"
        strSQL = strSQL & strWhere & strOrderby

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolPPD.DataSource = ds
            ddlSchoolPPD.DataTextField = "SchoolPPD"
            ddlSchoolPPD.DataValueField = "SchoolPPD"
            ddlSchoolPPD.DataBind()

            ddlSchoolPPD.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolPPD.SelectedValue = "ALL"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub schoolprofile_city_list()
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY SchoolCity"

        strSQL = "SELECT DISTINCT SchoolCity FROM schoolprofile WHERE SchoolState='" & ddlSchoolState.Text & "' AND IsDeleted='N'"
        If Not ddlSchoolPPD.Text = "ALL" Then
            strWhere = " AND SchoolPPD='" & ddlSchoolPPD.Text & "'"
        End If
        strSQL = strSQL & strWhere & strOrderby

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolCity.DataSource = ds
            ddlSchoolCity.DataTextField = "schoolcity"
            ddlSchoolCity.DataValueField = "schoolcity"
            ddlSchoolCity.DataBind()

            ddlSchoolCity.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolCity.SelectedValue = "ALL"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai."
            Else
                lblMsg.Text = "Jumlah Rekod #:" & myDataSet.Tables(0).Rows.Count
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
        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("kpm.schoolprofile.students.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID & "&iscount=" & chkRuleAge.Checked)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.schoolprofile.students.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID & "&iscount=" & chkRuleAge.Checked)
                Case "JPN"
                    Response.Redirect("jpn.schoolprofile.students.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID & "&iscount=" & chkRuleAge.Checked)
                Case "KPM"
                    Response.Redirect("kpm.schoolprofile.students.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID & "&iscount=" & chkRuleAge.Checked)
                Case "KPT"
                Case "MRSM"
                Case "ASASI"
                Case Else
                    lblMsg.Text = "Invalid User Type: " & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strWhereIn As String = ""
        Dim strOrder As String = " ORDER BY Jumlah DESC"

        Try
            '--chkRuleAge
            If chkRuleAge.Checked = True Then
                strWhereIn = " AND UKM1.IsCount=1"
            End If

            If ddlExamYear.Text = getUKM1Year Then
                tmpSQL = "SELECT SchoolProfile.SchoolID,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,SchoolProfile.SchoolLokasi,(SELECT COUNT(*) FROM UKM1_" & getUKM1Year & " WHERE SchoolProfile.SchoolID=UKM1_" & getUKM1Year & ".SchoolID AND UKM1_" & getUKM1Year & ".Examyear='" & ddlExamYear.Text & "'" & strWhereIn & ") as Jumlah FROM SchoolProfile"
            Else
                tmpSQL = "SELECT SchoolProfile.SchoolID,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,SchoolProfile.SchoolLokasi,(SELECT COUNT(*) FROM UKM1 WHERE SchoolProfile.SchoolID=UKM1.SchoolID AND UKM1.Examyear='" & ddlExamYear.Text & "'" & strWhereIn & ") as Jumlah FROM SchoolProfile"
            End If

            strWhere += " WHERE SchoolProfile.IsDeleted='" & selIsDeleted.Value & "'"

            ''usertype. for MRSM only
            If getUserProfile_UserType() = "MRSM" Then
                strWhere += " AND SchoolProfile.SchoolType='MRSM'"
            End If

            '--SchoolState
            If Not ddlSchoolState.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolState='" & ddlSchoolState.Text & "'"
            End If

            '--SchoolPPD
            If Not ddlSchoolPPD.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolPPD='" & ddlSchoolPPD.Text & "'"
            End If

            '--SchoolCity
            If Not ddlSchoolCity.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolCity='" & ddlSchoolCity.Text & "'"
            End If

            '--SchoolCode
            If Not txtSchoolCode.Text.Length = 0 Then
                strWhere += " AND SchoolProfile.SchoolCode='" & oCommon.FixSingleQuotes(txtSchoolCode.Text) & "'"
            End If

            '--SchoolName
            If Not txtSchoolname.Text.Length = 0 Then
                strWhere += " AND SchoolProfile.SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolname.Text) & "%'"
            End If

            '--SchoolCode XXX
            If chkXXX.Checked = True Then
                strWhere += " AND SchoolProfile.SchoolCode LIKE 'XXX%'"
            Else
                strWhere += " AND SchoolProfile.SchoolCode NOT LIKE 'XXX%'"
            End If

            getSQL = tmpSQL & strWhere & strOrder

            ''--debug
            'Response.Write(getSQL)
            Return getSQL

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Sub ddlSchoolState_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_PPD_list()
        schoolprofile_city_list()

    End Sub

    Private Sub ddlSchoolPPD_TextChanged(sender As Object, e As EventArgs) Handles ddlSchoolPPD.TextChanged
        schoolprofile_city_list()

    End Sub
End Class