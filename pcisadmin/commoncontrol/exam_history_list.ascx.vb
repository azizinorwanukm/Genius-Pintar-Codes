Imports System.Data.SqlClient

Public Class exam_history_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            'BindData(datRespondent)

            getpcis_user_icno()
            BindDataSP(datRespondent, "spReportSummary3")

        Catch ex As Exception
        End Try

    End Sub

    Private Sub getpcis_user_icno()
        Dim strid As String = Request.QueryString("id")
        strSQL = "SELECT icno FROM pcis_user WHERE id='" & strid & "'"
        strRet = oCommon.getFieldValue(strSQL)

        txticno.Text = strRet
    End Sub

    Private Sub BindDataSP(ByVal gvTable As GridView, ByVal strCommandText As String)
        Dim command As New SqlCommand()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        Dim i As Integer = 0
        Dim sql As String = Nothing
        Dim connectionString As String = strConn
        Dim connection As New SqlConnection(connectionString)

        connection.Open()
        command.Connection = connection
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = strCommandText    '--sp name
        command.Parameters.AddWithValue("@icno", txticno.Text)    '--parameters

        adapter = New SqlDataAdapter(command)
        adapter.Fill(ds)
        lblMsg.Text = "Jumlah Rekod#:" & ds.Tables(0).Rows.Count

        connection.Close()
        gvTable.DataSource = ds.Tables(0)
        gvTable.DataBind()

    End Sub

    'Private Sub BindDataSP(ByVal gvTable As GridView, ByVal strCommandText As String)
    '    Dim command As New SqlCommand()
    '    Dim adapter As New SqlDataAdapter()
    '    Dim ds As New DataSet()
    '    Dim i As Integer = 0
    '    Dim sql As String = Nothing
    '    Dim connectionString As String = strConn
    '    Dim connection As New SqlConnection(connectionString)

    '    connection.Open()
    '    command.Connection = connection
    '    command.CommandType = CommandType.StoredProcedure
    '    command.CommandText = strCommandText    '--sp name
    '    'command.Parameters.AddWithValue("@fullname", "") '--parameters
    '    'command.Parameters.AddWithValue("@examyearid", "")    '--parameters. SEARCH ALL
    '    command.Parameters.AddWithValue("@icno", txticno.Text)    '--parameters
    '    'command.Parameters.AddWithValue("@done", "")    '--1-done,0-not done,null- both
    '    command.Parameters.AddWithValue("@sortby", "test_start")    '--parameters
    '    command.Parameters.AddWithValue("@sortdir", "ASC")    '--parameters

    '    adapter = New SqlDataAdapter(command)
    '    adapter.Fill(ds)
    '    lblMsg.Text = "Jumlah Rekod#:" & ds.Tables(0).Rows.Count

    '    connection.Close()
    '    gvTable.DataSource = ds.Tables(0)
    '    gvTable.DataBind()

    'End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY examyearid DESC"

        Try
            tmpSQL = "SELECT pcis_exam.id,pcis_exam.test_start,pcis_exam.test_end,pcis_exam.lastpage,pcis_exam_year.description,pcis_user.learningcentrename FROM pcis_exam,pcis_exam_year,pcis_user"
            strWhere += " WHERE pcis_exam.examyearid=pcis_exam_year.id"
            strWhere += " AND pcis_exam.userid='" & Request.QueryString("id") & "'"
            strWhere += " AND pcis_user.id='" & Request.QueryString("id") & "'"

            getSQL = tmpSQL & strWhere & strOrder

            ''--debug
            'Response.Write(getSQL)
            Return getSQL

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function



End Class