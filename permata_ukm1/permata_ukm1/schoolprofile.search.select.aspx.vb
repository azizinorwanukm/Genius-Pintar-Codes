Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_search_select
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnCreate.Attributes.Add("onclick", "return confirm('CARIAN sudah dilakukan? Sekolah anda TIADA?');")
        lblSchoolState.Text = Request.QueryString("schoolstate")

        Try
            If Not IsPostBack Then
                btnCreate.Visible = False
                lblNewSchool.Visible = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblNoRecord.Text = ""

        If txtSchoolCode.Text.Length = 0 Then
            If txtSchoolName.Text.Length = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Sila masukkan kata kunci nama sekolah anda. Contoh: Masukkan 'ORANG KAYA' untuk carian SK ORANG KAYA MOHAMMAD"
                txtSchoolName.Focus()
                Exit Sub
            End If
        End If

        divMsg.Attributes("class") = "info"
        strRet = BindData(datRespondent)
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 1200

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Sekolah yang anda cari tidak terdapat di dalam senarai Kementerian Pelajaran Malaysia bagi negeri " & lblSchoolState.Text & "."
                lblNoRecord.Text = lblMsg.Text
                '--Dr Siti request. No new school registration
                'lblNewSchool.Visible = True
                'btnCreate.Visible = True
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Tekan [PILIH] untuk sekolah anda."
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
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("schoolprofile.confirm.change.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid") & "&schoolid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""

        '--hide schoolcode XXX. Dr Siti 20150331

        Dim strOrder As String = " ORDER BY SchoolCode,SchoolName"
        tmpSQL = "SELECT SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType FROM SchoolProfile"
        strWhere += " WHERE IsDeleted='N'"
        strWhere += " AND SchoolCode NOT LIKE 'XXX%'"
        strWhere += " AND SchoolState='" & Request.QueryString("schoolstate") & "'"

        If Not txtSchoolCode.Text.Length = 0 Then
            strWhere += " AND SchoolCode='" & txtSchoolCode.Text & "'"
        End If

        If Not txtSchoolName.Text.Length = 0 Then
            strWhere += " AND SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text.Trim) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        Return getSQL

    End Function

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Response.Redirect("student.schoolprofile.create.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))

    End Sub

    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChange.Click
        Response.Redirect("schoolprofile.state.select.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))


    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click
        Response.Redirect("default.main.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))

    End Sub

End Class