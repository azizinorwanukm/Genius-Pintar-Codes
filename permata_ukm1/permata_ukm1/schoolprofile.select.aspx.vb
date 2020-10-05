Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class schoolprofile_select
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            txtSchoolName.Text = Request.QueryString("schoolname").ToString()

        End If
    End Sub


    Private Sub RefreshScreen()
        lblMsg.Text = ""
        txtSchoolName.Text = ""

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

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

        strSQL = CreateSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("schoolprofile.select.aspx?schoolname=" & strKeyID)

    End Sub

    Private Function CreateSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " Order By SchoolName"
        tmpSQL = "Select SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType From SchoolProfile"

        If Not txtSchoolName.Text.Length = 0 Then
            strWhere = " Where SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text.Trim) & "%'"
        End If

        CreateSQL = tmpSQL & strWhere & strOrder
        Return CreateSQL

    End Function

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If txtSchoolName.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan sama ada Nama Sekolah. Contoh. Masukkan 'kajang' untuk SM TEKNIK KAJANG"
            Exit Sub
        End If

        strSQL = CreateSQL()
        strRet = BindData(datRespondent)
    End Sub

End Class