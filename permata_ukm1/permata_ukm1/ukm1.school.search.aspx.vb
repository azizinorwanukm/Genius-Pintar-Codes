Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class ukm1_school_search
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            RefreshScreen()
        End If

    End Sub

    Private Sub RefreshScreen()
        lblMsg.Text = ""
        lblPageNo.Text = ""
        lblTotal.Text = "0"

        'txtKodSekolah.Text = ""
        txtNamaSekolah.Text = ""
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
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

        nPageno = e.NewPageIndex + 1
        lblPageNo.Text = "Mukasurat: " & nPageno.ToString
    End Sub

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        '--running no
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lableIndex As Label
            Dim i As Integer = e.Row.RowIndex + 1
            lableIndex = e.Row.FindControl("lableIndex")
            lableIndex.Text = i.ToString()
        End If

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        '--Response.Write("Approve:" & strKeyID)

        Response.Redirect("ukm1.update.school.schoolid.aspx?SchoolID=" & strKeyID)
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Response.Redirect("ukm1.member.profiles.page3.aspx?SchoolID=0")

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'If txtNamaSekolah.Text.Length = 0 And txtKodSekolah.Text.Length = 0 Then
        '    lblFormMsg.Text = "Sila masukkan sama ada Nama Sekolah ATAU Kod Sekolah."
        '    Exit Sub
        'End If

        Try
            If txtNamaSekolah.Text.Length = 0 Then
                lblFormMsg.Text = "Sila masukkan Nama Sekolah. Lihat contoh diatas."
                Exit Sub
            End If

            strSQL = CreateSQL()
            strRet = BindData(datRespondent)
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
        '--debug
        '-lblMsg.Text = strSQL
    End Sub

    Private Function CreateSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " Order By SchoolName"
        tmpSQL = "SELECT [SchoolID],[SchoolCode],[SchoolName],[SchoolAddress],[SchoolPostcode],[SchoolCity],[SchoolState],[SchoolType],[SchoolNoTel],[SchoolNoFax],[SchoolLokasi] From PP_Schools"

        If Not txtNamaSekolah.Text.Length = 0 Then
            strWhere = " Where SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtNamaSekolah.Text.Trim) & "%'"
        End If

        'If Not txtKodSekolah.Text.Length = 0 Then
        '    strWhere = " Where SchoolCode='" & oCommon.FixSingleQuotes(txtKodSekolah.Text) & "'"
        'End If

        CreateSQL = tmpSQL & strWhere & strOrder
        Return CreateSQL

    End Function

End Class