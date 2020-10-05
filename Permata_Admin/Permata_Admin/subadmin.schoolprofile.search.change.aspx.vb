Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization


Public Class subadmin_schoolprofile_search_change
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            btnCreate.Visible = False
            lblNewSchool.Text = ""

            SchoolState_list()
            schoolprofile_city_list()
        End If

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

    Private Sub schoolprofile_city_list()
        strSQL = "SELECT DISTINCT SchoolCity FROM schoolprofile WHERE SchoolState='" & ddlSchoolState.Text & "' AND IsDeleted='N' ORDER BY SchoolCity"
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

        ''If ddlSchoolCity.Text = "PILIH" Then
        ''    divMsg.Attributes("class") = "error"
        ''    lblMsg.Text = "Sila PILIH bandar sekolah anda."
        ''    Exit Sub
        ''End If
        ''If txtSchoolName.Text.Length = 0 Then
        ''    lblMsg.Text = "Sila masukkan sama ada Nama Sekolah. Contoh. Masukkan 'kajang' untuk carian SM TEKNIK KAJANG"
        ''    Exit Sub
        ''End If

        strRet = BindData(datRespondent)
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(CreateSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")


            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Sekolah yang anda cari tidak terdapat di dalam senarai Kem. Pelajaran Malaysia. Sila klik [Sekolah Baru]."
                lblNewSchool.Text = "*Jika Pelajar / Guru / Ibu bapa tidak memilih pilih sekolah sedia ada dalam senarai database KPM dan MARA,  maka pelajar  tidak akan dipertimbangkan untuk ke ujian UKM2. Kod XXX hanya untuk sekolah bukan dalam pangkalan data  KPM & MARA sahaja."
                btnCreate.Visible = True
            Else
                lblMsg.Text = "Tekan [Pilih] untuk sekolah anda. Pilih kod sekolah yang bukan XXX terlebih dahulu. Kod XXX adalah kod sekolah yang dimasukkan oleh pelajar untuk sekolah yang tiada dalam senarai."
                lblNewSchool.Text = ""
                btnCreate.Visible = False
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()


        Catch ex As Exception
            ''debug
            Response.Write(strSQL)
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strSQL = CreateSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("subadmin.schoolprofile.confirm.change.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid") & "&schoolid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Private Function CreateSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = " ORDER BY SchoolCode,SchoolName"
        tmpSQL = "SELECT SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType FROM SchoolProfile"
        ''--display all even manually key-in school
        ''--strWhere = " WITH (NOLOCK) WHERE SchoolState='" & ddlSchoolState.Text & "' AND SUBSTRING(SchoolCode,1,3) <>'XXX'"
        strWhere = " WITH (NOLOCK) WHERE SchoolState='" & ddlSchoolState.Text & "'"

        ''city
        If Not ddlSchoolCity.Text = "ALL" Then
            strWhere += " AND SchoolCity='" & ddlSchoolCity.Text & "'"
        End If

        If Not txtSchoolName.Text.Length = 0 Then
            strWhere += " AND SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text.Trim) & "%'"
        End If

        CreateSQL = tmpSQL & strWhere & strOrder
        Return CreateSQL

    End Function

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Response.Redirect("subadmin.student.schoolprofile.create.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))

    End Sub

    Private Sub ddlSchoolState_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_city_list()

    End Sub

    Private Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub

End Class