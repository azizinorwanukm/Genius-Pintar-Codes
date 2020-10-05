Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_search_change
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

            schoolprofile_state_list()
            'schoolprofile_city_list()
        End If

    End Sub

    Private Sub schoolprofile_state_list()
        strSQL = "SELECT DISTINCT schoolstate FROM schoolprofile ORDER BY SchoolState"
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


            ''make it blank first
            ddlSchoolState.Items.Add(New ListItem("", ""))
            ddlSchoolState.SelectedValue = ""

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If ddlSchoolState.Text = "" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila PILIH NEGERI sekolah anda."
            ddlSchoolState.Focus()
            Exit Sub
        End If

        If txtSchoolName.Text.Length = 0 Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila masukkan kata kunci sekolah anda. Contoh: Masukkan 'ORANG KAYA' untuk carian SK ORANG KAYA MOHAMMAD"
            txtSchoolName.Focus()
            Exit Sub
        End If

        divMsg.Attributes("class") = "info"
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
                lblMsg.Text = "Tiada rekod pelajar."
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
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("schoolprofile.confirm.change.aspx?studentid=" & Request.QueryString("studentid") & "&schoolid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = " ORDER BY SchoolCode,SchoolName"
        tmpSQL = "SELECT SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType FROM SchoolProfile"
        ''--display all even manually key-in school
        strWhere = " WITH (NOLOCK) WHERE IsDeleted<>'Y' AND SchoolState='" & ddlSchoolState.Text & "'"

        If Not txtSchoolName.Text.Length = 0 Then
            strWhere += " AND SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text.Trim) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        Return getSQL

    End Function

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Response.Redirect("student.schoolprofile.create.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub

End Class