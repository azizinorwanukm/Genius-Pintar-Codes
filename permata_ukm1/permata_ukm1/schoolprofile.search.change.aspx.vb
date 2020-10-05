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

            SchoolState_list()
        End If

    End Sub

    Private Sub SchoolState_list()
        ''strSQL = "SELECT DISTINCT schoolstate FROM schoolprofile ORDER BY SchoolState"
        strSQL = "SELECT schoolstate FROM SchoolState ORDER BY SchoolStateID"
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

            ''ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
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

        'If txtSchoolName.Text.Length = 0 Then
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Sila masukkan kata kunci sekolah anda. Contoh: Masukkan 'ORANG KAYA' untuk carian SK ORANG KAYA MOHAMMAD"
        '    txtSchoolName.Focus()
        '    Exit Sub
        'End If

        divMsg.Attributes("class") = "info"
        strRet = BindData(datRespondent)
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(CreateSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Sekolah yang anda cari tidak terdapat di dalam senarai Kem. Pelajaran Malaysia. Sila klik [Sekolah Baru]. SILA BUAT CARIAN dan pastikan sekolah anda tiada."
                lblNewSchool.Visible = True
                btnCreate.Visible = True
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Tekan [PILIH] untuk sekolah anda. Pilih kod sekolah yang bukan XXX terlebih dahulu. Kod XXX adalah kod sekolah yang dimasukkan oleh pelajar untuk sekolah yang tiada dalam senarai."
                lblNewSchool.Visible = False
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
            Response.Redirect("schoolprofile.confirm.change.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid") & "&schoolid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Private Function CreateSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        Dim strOrder As String = " Order By SchoolCode,SchoolName"
        tmpSQL = "SELECT SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType FROM SchoolProfile"
        strWhere = " WHERE IsDeleted='N'"

        If Not ddlSchoolState.Text = "ALL" Then
            strWhere += " AND SchoolState='" & ddlSchoolState.Text & "'"
        End If

        If Not txtSchoolCode.Text.Length = 0 Then
            strWhere += " AND SchoolCode='" & txtSchoolCode.Text & "'"
        End If

        If Not txtSchoolName.Text.Length = 0 Then
            strWhere += " AND SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text.Trim) & "%'"
        End If


        CreateSQL = tmpSQL & strWhere & strOrder
        Return CreateSQL

    End Function

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Response.Redirect("student.schoolprofile.create.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))

    End Sub

    Private Sub ddlSchoolState_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSchoolState.TextChanged
        'schoolprofile_city_list()

    End Sub
End Class