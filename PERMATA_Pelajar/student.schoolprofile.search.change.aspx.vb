Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class student_schoolprofile_search_change
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnCreate.Attributes.Add("onclick", "return confirm('CARIAN sudah dilakukan? Sekolah anda TIADA?');")

        If Not IsPostBack Then
            btnCreate.Visible = False
            lblNewSchool.Visible = False

            SchoolState_list()
            ddlSchoolState.SelectedValue = ""

            schoolprofile_PPD_list()
            ddlSchoolPPD.Text = "ALL"
        End If

    End Sub

    Private Sub SchoolState_list()
        '--remove invalid state
        strSQL = "SELECT SchoolState FROM SchoolState WHERE SchoolState<>'UKM2-KPT' AND SchoolState<>'UKM2-ASASI' ORDER BY SchoolStateID"
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

            ddlSchoolState.Items.Add(New ListItem("--Select--", ""))

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

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


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If ValidatePage() = False Then
            Exit Sub
        End If

        strRet = BindData(datRespondent)

    End Sub

    Private Function ValidatePage() As Boolean
        If ddlSchoolState.Text = "" Then
            lblMsg.Text = "Sila PILIH NEGERI sekolah anda."
            ddlSchoolState.Focus()
            Return False
        End If

        'If txtSchoolName.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan kata kunci sekolah anda. Contoh: Masukkan 'ORANG KAYA' untuk carian SK ORANG KAYA MOHAMMAD"
        '    txtSchoolName.Focus()
        '    Return False
        'End If

        Return True
    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

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
            Response.Redirect("student.schoolprofile.confirm.change.aspx?schoolid=" & strKeyID)
        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        End Try

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = " Order By SchoolState,SchoolPPD,SchoolCity,SchoolName ASC"
        tmpSQL = "SELECT SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolPPD,SchoolState,SchoolType FROM SchoolProfile"
        strWhere = " WITH (NOLOCK) WHERE IsDeleted<>'Y' AND SchoolCode NOT LIKE 'XXX%'"

        If Not ddlSchoolState.SelectedValue = "" Then
            strWhere += " AND SchoolState='" & ddlSchoolState.Text & "'"
        End If

        If Not ddlSchoolPPD.Text = "ALL" Then
            strWhere += " AND SchoolPPD='" & ddlSchoolPPD.Text & "'"
        End If

        If Not txtSchoolCode.Text.Length = 0 Then
            strWhere += " AND SchoolCode='" & oCommon.FixSingleQuotes(txtSchoolCode.Text) & "'"
        End If

        If Not txtSchoolName.Text.Length = 0 Then
            strWhere += " AND SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text.Trim) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        Return getSQL

    End Function

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Response.Redirect("student.schoolprofile.create.aspx?studentid=" & CType(Session.Item("permata_studentid"), String))

    End Sub

    Private Sub ddlSchoolState_TextChanged(sender As Object, e As EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_PPD_list()
        ddlSchoolPPD.Text = "ALL"

    End Sub

End Class