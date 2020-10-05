Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Data.OleDb
'Imports System.IO
'Imports System.Globalization

Partial Public Class ukm1_dobyear_summary
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                strRet = BindData(dat_Age, getSQL)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView, ByVal strQuery As String) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strQuery, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
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


    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strWhereIn As String = ""
        Dim strOrder As String = " ORDER BY A.DOB_Year"

        '--chkRuleAge
        If chkRuleAge.Checked = True Then
            strWhereIn = " AND B.IsCount=1"
        End If

        Dim examyear_id As String = Common.getDefaultExamYearID()

        tmpSQL = "SELECT A.DOB_Year,(SELECT COUNT(*) FROM UKM1 B WHERE B.DOB_Year=A.DOB_Year AND B.examyear_id= " & examyear_id & " " & strWhereIn & ") as Jumlah FROM master_dobyear A "
        If chkRuleAge.Checked = True Then
            strWhere += " WHERE A.DOB_Year >" & oCommon.getMaxYear(oCommon.getAppsettings("UKM1ExamYear"), 15)
            strWhere += " AND A.DOB_Year <" & oCommon.getMinYear(oCommon.getAppsettings("UKM1ExamYear"), 8)
        End If
        getSQL = tmpSQL & strWhere & strOrder
        If oCommon.isDebug = True Then

        End If

        Return getSQL

    End Function

    Private Sub dat_Age_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles dat_Age.PageIndexChanging
        dat_Age.PageIndex = e.NewPageIndex
        strRet = BindData(dat_Age, getSQL)

    End Sub

End Class