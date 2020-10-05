Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class pelajar_summary_status
    Inherits System.Web.UI.Page

    Private rm As ResourceManager

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 0
    Dim strDateCreated As String
    Dim strcourseCode As String
    Dim strTokenid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTokenid = Request.QueryString("tokenid")

        ''--todays date
        strDateCreated = oCommon.getToday

        Try

            If Not IsPostBack Then
                ClearScreen()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub ClearScreen()
        lblMsg.Text = ""

        strSQL = "SELECT * from ppcs_eval WHERE Tokenid='" & strTokenid & "' ORDER BY DateCreated"
        strRet = BindData(datRespondent)

        strSQL = "SELECT * FROM ppcs_eval_weekly WHERE Tokenid='" & strTokenid & "' ORDER BY DateCreated"
        strRet = BindData(datMingguan)

        strSQL = "SELECT * FROM ppcs_eval_end WHERE Tokenid='" & strTokenid & "' ORDER BY DateCreated"
        strRet = BindData(datEnd)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Record not found!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        ClearScreen()

        nPageno = e.NewPageIndex + 1

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        '--Response.Write(strKeyID)
        Response.Redirect("admin.pelajar.eval.daily.view.aspx?ppcsevalid=" & strKeyID & "&tokenid=" & strTokenid)

    End Sub

    Private Sub datMingguan_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datMingguan.PageIndexChanging
        datMingguan.PageIndex = e.NewPageIndex
        ClearScreen()

        nPageno = e.NewPageIndex + 1
    End Sub

    Private Sub datMingguan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datMingguan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lableIndex As Label
            Dim i As Integer = e.Row.RowIndex + 1

            lableIndex = e.Row.FindControl("lableIndexMingguan")
            lableIndex.Text = i.ToString()
        End If
    End Sub

    Private Sub datEnd_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datEnd.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lableIndex As Label
            Dim i As Integer = e.Row.RowIndex + 1

            lableIndex = e.Row.FindControl("lableIndexEnd")
            lableIndex.Text = i.ToString()
        End If
    End Sub

End Class