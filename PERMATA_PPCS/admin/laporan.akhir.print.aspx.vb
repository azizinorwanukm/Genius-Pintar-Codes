Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData


Partial Public Class laporan_akhir_print
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ClearScreen()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub


    Private Sub ClearScreen()
        lblMsg.Text = ""

        lblTotal.Text = ""

        strSQL = "SELECT * from ppcs_eval_end ORDER BY ClassCode ASC"
        strRet = BindData(datRespondent)

        '--display coursename
        lblCourse.Text = Session("CourseNameBM")

        If lblCourse.Text = "ALL" Then
            strSQL = "SELECT ppcsevalendid,Tokenid,ClassCode,Q001Remarks from ppcs_eval_end ORDER BY ClassCode ASC"
            strRet = BindData(datRespondent)
        Else
            strSQL = "SELECT ppcsevalendid,Tokenid,ClassCode,Q001Remarks from ppcs_eval_end WHERE ClassCode LIKE '%" & Session("Course2010") & "%' ORDER BY ClassCode"
            strRet = BindData(datRespondent)
        End If

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
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        ClearScreen()

        nPageno = e.NewPageIndex + 1


    End Sub

End Class