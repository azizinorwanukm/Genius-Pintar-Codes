Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_schoollokasi_summary
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            lblDatetime.Text = Now.ToLongDateString & "  " & Now.ToShortTimeString


        End If

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Senarai lokasi tiada"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah pelajar bagi setiap lokasi sekolah yang menduduki Ujian UKM1."
            End If

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

        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        Dim tmpSQL As String
        Dim strWhere As String = " WHERE ExamYear='" & selExamYear.Value & "'"
        Dim strGroupby As String = " GROUP BY SchoolLokasi"
        Dim strOrder As String = " ORDER BY nTotal DESC"

        tmpSQL = "SELECT SchoolLokasi, count(*) as nTotal FROM UKM1"
        getSQL = tmpSQL & strWhere & strGroupby & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub
End Class