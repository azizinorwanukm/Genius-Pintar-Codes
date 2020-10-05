Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_pusatujian_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            schoolprofile_state_list()
            schoolprofile_PPD_list()
            schoolprofile_city_list()
        End If

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

            ''default state
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If
            ''debug
            'Response.Write(getUserProfile_State())

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_state_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub schoolprofile_PPD_list()
        strSQL = "SELECT DISTINCT SchoolPPD FROM SchoolProfile WITH (NOLOCK) WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolPPD<>'' AND IsDeleted<>'Y' ORDER BY SchoolPPD"
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
        strSQL = "SELECT DISTINCT SchoolCity FROM schoolprofile WHERE SchoolState='" & ddlSchoolState.Text & "' ORDER BY SchoolCity"
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

        strSQL = getSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Function Schoolprofile_Count(ByVal strValue As String)
        strSQL = "SELECT COUNT(*) FROM StudentSchool WITH (NOLOCK) WHERE SchoolID='" & strValue & "'"
        strRet = oCommon.getFieldValue(strSQL)
        ''--debug
        'Response.Write(strSQL)

        Return strRet
    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " Order By SchoolName"

        tmpSQL = "SELECT SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType,SchoolNoTel,SchoolPPD FROM SchoolProfile"
        strWhere = " WITH (NOLOCK) WHERE IsDeleted<>'Y'"

        ''state
        If Not ddlSchoolState.Text = "ALL" Then
            strWhere += " AND SchoolState='" & ddlSchoolState.Text & "'"
        End If

        ''PPD
        If Not ddlSchoolPPD.Text = "ALL" Then
            strWhere += " AND SchoolPPD='" & ddlSchoolPPD.Text & "'"
        End If

        ''city
        If Not ddlSchoolCity.Text = "ALL" Then
            strWhere += " AND SchoolCity='" & ddlSchoolCity.Text & "'"
        End If

        ''usertype. for MRSM only
        If getUserProfile_UserType() = "MRSM" Then
            strWhere += " AND SchoolType='MRSM'"
        End If

        ''schooname
        If Not txtSchoolname.Text.Length = 0 Then
            strWhere += " AND Schoolname LIKE '%" & oCommon.FixSingleQuotes(txtSchoolname.Text) & "%'"
        End If


        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub ddlSchoolState_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_PPD_list()
        schoolprofile_city_list()

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            ''jumlah pelajar berdaftar
            'Response.Redirect("ukm1.school.students.aspx?schoolid=" & strKeyID)
            Select Case getUserProfile_UserType()
                Case "JPN"
                    Response.Redirect("jpn.schoolprofile.pusatujian.confirm.aspx?schoolid=" & strKeyID)
                Case Else
                    lblMsg.Text = "Anda tidak layak menggunakan fungsi ini." & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            ''jumlah pelajar berdaftar
            'Response.Redirect("ukm1.school.students.aspx?schoolid=" & strKeyID)
            Select Case getUserProfile_UserType()
                Case "JPN"
                    Response.Redirect("jpn.pusatujian.create.aspx")
                Case Else
                    lblMsg.Text = "Anda tidak layak menggunakan fungsi ini." & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try


    End Sub
End Class