Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Public Class laporan_keseluruhan_view
    Inherits System.Web.UI.UserControl

    Private rm As ResourceManager

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 0
    Dim strDateCreated As String = ""
    Dim strcourseCode As String = ""
    Dim strStudentID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strStudentID = Request.QueryString("studentid")
        lblPPCSDate.Text = Request.QueryString("ppcsdate")

        ''--todays date
        strDateCreated = oCommon.getToday
        Try
            If Not IsPostBack Then
                lnkEditAkhir.Visible = False
                lnkEditMingguan.Visible = False
                lnkEditHarian.Visible = False


                ClearScreen()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub


    Private Sub ClearScreen()
        lblMsg.Text = ""

        strSQL = "SELECT * FROM permatapintar.dbo.PPCS_Eval_Daily WHERE ClassID=" & Request.QueryString("classid") & " AND StudentID='" & strStudentID & "' ORDER BY DateCreated"
        strRet = BindData(datRespondent)

        strSQL = "SELECT * FROM permatapintar.dbo.PPCS_Eval_Weekly WHERE ClassID=" & Request.QueryString("classid") & " AND StudentID='" & strStudentID & "' ORDER BY DateCreated"
        strRet = BindData(datMingguan)

        ''in literal format
        strSQL = "SELECT Q001Remarks FROM permatapintar.dbo.PPCS_Eval_End WHERE ClassID=" & Request.QueryString("classid") & " AND StudentID='" & strStudentID & "' ORDER BY DateCreated"
        txtQ001Remarks.Text = oCommon.getFieldValue(strSQL)

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
        strSQL = "SELECT * FROM permatapintar.dbo.PPCS_Eval_Daily WHERE ClassID=" & Request.QueryString("classid") & " AND StudentID='" & strStudentID & "' ORDER BY DateCreated"
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datMingguan_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datMingguan.PageIndexChanging
        datMingguan.PageIndex = e.NewPageIndex
        strSQL = "SELECT * FROM permatapintar.dbo.PPCS_Eval_Weekly WHERE ClassID=" & Request.QueryString("classid") & " AND StudentID='" & strStudentID & "' ORDER BY DateCreated"
        strRet = BindData(datMingguan)

    End Sub

End Class