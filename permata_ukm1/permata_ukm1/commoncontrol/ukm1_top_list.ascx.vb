Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Data.OleDb
'Imports System.IO
'Imports System.Globalization

Partial Public Class ukm1_top_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim time As DateTime = DateTime.Parse(oCommon.getFieldValue("SELECT configDate FROM master_config_date WHERE configString = 'UKM1SchoolRank'"))
            lblDate.Text = time.ToString("d MMMM yyyy hh:mm tt ")

            If Not IsPostBack Then
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai."
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            'lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Return "SELECT * FROM SchoolRank ORDER BY Jumlah DESC"

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strWhereIn As String = ""
        Dim strOrder As String = " ORDER BY Jumlah DESC"

        Try
            '--chkRuleAge
            If chkRuleAge.Checked = True Then
                strWhereIn = " AND B.IsCount=1"
            End If

            tmpSQL = "SELECT TOP 10 A.SchoolID,A.SchoolState,A.SchoolName,(SELECT COUNT(*) FROM UKM1 B WHERE A.SchoolID=B.SchoolID AND B.examyear_id = " & Common.getDefaultExamYearID() & " " & strWhereIn & ") as Jumlah FROM SchoolProfile A "
            strWhere += " WHERE A.IsDeleted='N'"

            getSQL = tmpSQL & strWhere & strOrder
            If oCommon.isDebug = True Then
                lblDebug.Text = getSQL
            End If

            Return getSQL

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

End Class