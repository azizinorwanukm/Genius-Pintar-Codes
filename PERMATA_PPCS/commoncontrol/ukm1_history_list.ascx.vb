Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class ukm1_history_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim strUserType As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strUserType = Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
            strRet = BindData(datRespondent)

        Catch ex As Exception
            lblDebug.Text = "Page_load:" & strUserType & ":" & ex.Message
        End Try

    End Sub


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getTotal() As Integer
        strSQL = "SELECT COUNT(DISTINCT StudentID) as nTotal FROM PPCS"
        getTotal = oCommon.getFieldValue(strSQL)

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT * FROM UKM1"
        strWhere = " WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strOrder = " ORDER BY ExamYear"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not strUserType = "ADMIN" Then
                    hideColumn()
                End If

            End If

        Catch ex As Exception
            lblDebug.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub hideColumn()
        Dim nCol As Integer = datRespondent.Rows.Count
        datRespondent.Columns(3).Visible = False
        datRespondent.Columns(4).Visible = False
        datRespondent.Columns(5).Visible = False
        datRespondent.Columns(6).Visible = False
        datRespondent.Columns(7).Visible = False

    End Sub


End Class