Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_studentrace_summary
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
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
                lblMsg.Text = "Senarai bangsa tiada"
            Else
                lblMsg.Text = "Jumlah pelajar bagi setiap bangsa yang menduduki Ujian UKM1."
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
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strWhereIn As String = ""
        Dim strOrder As String = " ORDER BY Jumlah DESC"

        '--chkRuleAge
        If chkRuleAge.Checked = True Then
            strWhereIn = " AND B.IsCount=1"
        End If

        Dim examyear_id As Integer = Common.getDefaultExamYearID()

        tmpSQL = "SELECT A.StudentRace,(SELECT COUNT(*) FROM UKM1 B,StudentProfile C WHERE B.StudentID=C.StudentID AND C.StudentRace=A.StudentRace AND B.examyear_id= " & examyear_id & " " & strWhereIn & ") as Jumlah FROM master_StudentRace A "
        strWhere += ""

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    
End Class