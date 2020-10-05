Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_search
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            schoolprofile_state_list()
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

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ''If txtSchoolName.Text.Length = 0 Then
        ''    divMsg.Attributes("class") = "error"
        ''    lblMsg.Text = "Sila masukkan sama ada Nama Sekolah. Contoh. Masukkan 'kajang' untuk carian SM TEKNIK KAJANG"
        ''    Exit Sub
        ''End If

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
                lblMsg.Text = "Sekolah yang anda cari tidak terdapat di dalam senarai Kem. Pelajaran Malaysia. Sila email ke permatapintar@ukm.my maklumat sekolah anda."
                btnCreate.Visible = True
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Tekan [PILIH] untuk sekolah anda."
                btnCreate.Visible = False
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

        strSQL = CreateSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("schoolprofile.confirm.aspx?studentid=" & Request.QueryString("studentid") & "&schoolid=" & strKeyID)
        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message
        End Try

    End Sub

    Private Function CreateSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " Order By SchoolName"
        tmpSQL = "SELECT SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType FROM SchoolProfile"
        strWhere = " WITH (NOLOCK) WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolCode <>'XXX0001'"

        If Not txtSchoolName.Text.Length = 0 Then
            strWhere += " AND SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text.Trim) & "%'"
        End If

        CreateSQL = tmpSQL & strWhere & strOrder
        Return CreateSQL

    End Function

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Response.Redirect("student.schoolprofile.create.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub
End Class