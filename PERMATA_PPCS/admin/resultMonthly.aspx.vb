Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class resultMonthly
    Inherits System.Web.UI.Page
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strICnumber As String
    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strName As String
        strICnumber = Request.QueryString("ICnumber")
        year.Value = Now.Year

        strSQL = "SELECT fullname from ukm2_login WHERE ICnumber='" & strICnumber & "'"
        strName = oCommon.getFieldValue(strSQL)
        lblFullname.Text = strName

    End Sub

    Private Sub ClearScreen(ByVal strICnumber As String)
        lblMsg.Text = ""

        lblTotal.Text = ""

        strSQL = "SELECT * from ukm2_respondent_eval WHERE ICnumber='" & strICnumber & "' AND DateUpdate like '" & year.Value & month.Value & "%'"
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
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
        ClearScreen(strICnumber)

        nPageno = e.NewPageIndex + 1


    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strCourseCode As String
        Dim strDateUpdate As String = datRespondent.DataKeys(e.NewSelectedIndex).Values("DateUpdate")
        strICnumber = Request.QueryString("ICnumber")

        'GET COURSE NAME
        strSQL = "SELECT courseCode from ukm2_login WHERE ICnumber='" & strICnumber & "'"
        strCourseCode = oCommon.getFieldValue(strSQL)

        'Response.Redirect("viewResult.aspx?ICnumber=" & strKeyID & " & DateUpdate=" & strDateUpdate & "")
        Response.Redirect("viewResult.aspx?DateUpdate=" & strDateUpdate & " & ICnumber=" & strICnumber & " &page=1")

    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenerate.Click
        strICnumber = Request.QueryString("ICnumber")

        '--GET ALL DATA
        strSQL = "SELECT * FROM ukm2_respondent_eval WHERE ICnumber='" & strICnumber & "' AND  DateUpdate like '" & year.Value & month.Value & "%'"
        strRet = oCommon.getFieldValue(strSQL)
        'strCourseName = strRet
        '--get all user account info
        ClearScreen(strICnumber)
    End Sub
End Class