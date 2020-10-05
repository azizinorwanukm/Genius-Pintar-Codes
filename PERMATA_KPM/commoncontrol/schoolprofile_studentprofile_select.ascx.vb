Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_studentprofile_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            SchoolState_list()

            schoolprofile_PPD_list()
            ddlSchoolPPD.SelectedValue = "ALL"

            strRet = BindData(datRespondent)
        End If

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
            ddlSchoolState.SelectedValue = strRet
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub schoolprofile_PPD_list()
        strSQL = "SELECT DISTINCT SchoolPPD FROM SchoolProfile WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolPPD<>'' AND IsDeleted<>'Y' ORDER BY SchoolPPD"
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


    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


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

    Private Function Schoolprofile_Count(ByVal strValue As String)
        'COUNT nuber of registered students
        strSQL = "SELECT COUNT(*) FROM StudentSchool WHERE SchoolID='" & strValue & "'"
        strRet = oCommon.getFieldValue(strSQL)
        ''--debug
        'Response.Write(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.schoolprofile.studentprofile.list.aspx?schoolid=" & strKeyID & "&examyear=" & oCommon.getAppsettings("DefaultExamYear"))
                Case "SUBADMIN"
                    Response.Redirect("subadmin.schoolprofile.studentprofile.list.aspx?schoolid=" & strKeyID & "&examyear=" & oCommon.getAppsettings("DefaultExamYear"))
                Case Else
                    lblMsg.Text = "Invalid User Type: " & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function


    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strWhereIn As String = ""
        Dim strOrder As String = " ORDER BY Jumlah DESC"
        Dim strGroupby As String = ""

        tmpSQL = "SELECT SchoolID, SchoolState, SchoolPPD, SchoolCity, SchoolCode, SchoolName, SchoolLokasi, (SELECT COUNT(*) FROM StudentSchool WHERE StudentSchool.SchoolID=SchoolProfile.SchoolID) AS Jumlah FROM SchoolProfile"
        strWhere = " WITH (NOLOCK) WHERE IsDeleted='N'"

        '--ddlSchoolState
        strRet = getUserProfile_State()
        If Not ddlSchoolState.Text = "ALL" Then
            strWhere += " AND SchoolState='" & ddlSchoolState.Text & "'"
        End If

        '--ddlSchoolPPD
        If Not ddlSchoolPPD.Text = "ALL" Then
            strWhere += " AND SchoolPPD='" & ddlSchoolPPD.Text & "'"
        End If

        '--usertype. for MRSM only
        If getUserProfile_UserType() = "MRSM" Then
            strWhere += " AND SchoolType='MRSM'"
        End If

        '--SchoolCode
        If Not txtSchoolCode.Text.Length = 0 Then
            strWhere += " AND SchoolCode='" & oCommon.FixSingleQuotes(txtSchoolCode.Text) & "'"
        End If

        '--Schoolname
        If Not txtSchoolname.Text.Length = 0 Then
            strWhere += " AND Schoolname LIKE '%" & oCommon.FixSingleQuotes(txtSchoolname.Text) & "%'"
        End If

        '--SchoolCode
        If cbXXX.Checked Then
            strWhere += " AND SchoolCode LIKE '%XXX%'"
        End If

        getSQL = tmpSQL & strWhere & strGroupby & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub ddlSchoolState_TextChanged(sender As Object, e As EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_PPD_list()
        ddlSchoolPPD.Text = "ALL"

    End Sub

End Class