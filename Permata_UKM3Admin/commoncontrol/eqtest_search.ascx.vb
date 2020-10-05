Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class eqtest_search
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti hendak menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                ''access right
                btnExport.Visible = False
                ppcsdate_list()

                master_surveyid_list()
                ''ddlSurveyID.Text = "ALL"

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

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT DISTINCT ppcsdate FROM UKM3Session WHERE ppcsdate IS NOT NULL"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        For k = 0 To quantity - 1
            ddlPPCSDate.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(0).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        Dim currentPpcs As String = oCommon.getFieldValue("SELECT B.ppcsdate FROM general_config A JOIN UKM3Session B ON A.parameter = B.id WHERE A.config = 'currentSession'")
        ddlPPCSDate.SelectedValue = currentPpcs
    End Sub

    Private Sub master_surveyid_list()
        '--base on usertype. admin only allow all

        strSQL = "SELECT DISTINCT surveyID FROM UKM3Session WHERE surveyID IS NOT NULL"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--source
            ddlSurveyID.DataSource = ds
            ddlSurveyID.DataTextField = "SurveyID"
            ddlSurveyID.DataValueField = "SurveyID"
            ddlSurveyID.DataBind()

            ''ddlSurveyID.Items.Add(New ListItem("ALL", "ALL"))

            Dim defaultSurvey As String = oCommon.getFieldValue("SELECT B.surveyID FROM general_config A JOIN UKM3Session B ON A.parameter = B.id WHERE A.config = 'currentSession'")

            ddlSurveyID.SelectedValue = defaultSurvey

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT A.EQTestID,A.LastUpdate,A.Fullname,A.EmailAdd,B.StudentFullname,B.StudentEmail,B.AlumniID,A.SurveyID,A.LastPage,A.IsCompleted,A.ScorePercentage "
        tmpSQL += " FROM EQTest A LEFT JOIN StudentProfile B ON A.StudentID=B.StudentID "
        tmpSQL += " LEFT JOIN ukm3.dbo.student_info C ON C.guid = B.StudentID "
        tmpSQL += " INNER JOIN ukm3.dbo.UKM3 D ON D.student_id = C.std_ID AND D.active = 1 AND D.session_id = " & Commonfunction.getUkm3SessionID(ddlPPCSDate.SelectedValue)
        strWhere = " WHERE IsDeleted='N'"

        '--PPCSDate
        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND A.PPCSDate='" & ddlPPCSDate.Text & "'"
        End If

        '--IsCompleted
        If Not selIsCompleted.Value.Length = 0 Then
            strWhere += " AND A.IsCompleted='" & selIsCompleted.Value & "'"
        End If

        '--AlumniID
        If Not txtAlumniID.Text.Length = 0 Then
            strWhere += " AND B.AlumniID='" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "'"
        End If

        '--EmailAdd
        If Not txtEmailAdd.Text.Length = 0 Then
            strWhere += " AND B.StudentEmail LIKE '%" & oCommon.FixSingleQuotes(txtEmailAdd.Text) & "%'"
        End If

        '--Fullname
        If Not txtFullname.Text.Length = 0 Then
            strWhere += " AND B.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtFullname.Text) & "%'"
        End If

        '--SurveyID
        If Not ddlSurveyID.Text = "ALL" Then
            strWhere += " And A.SurveyID ='" & ddlSurveyID.Text & "'"
        End If

        If selOrderBy.Value = "1" Then
            strOrder = " ORDER BY A.LastUpdate DESC"
        Else
            strOrder = " ORDER BY A.ScorePercentage DESC"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
    End Sub

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileExportUKM3.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

            If isChecked Then
                strSQL = "UPDATE EQTest SET IsDeleted='Y' WHERE EQTestID=" & datRespondent.DataKeys(i).Value.ToString
                oCommon.ExecuteSQL(strSQL)
            Else
                '--do nothing
            End If
        Next

        ''refresh screen
        strRet = BindData(datRespondent)
        lblMsg.Text = "Berjaya kemaskini database."

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString  'StressTestID

        strSQL = "SELECT LoginID FROM EQTest WHERE EQTestID=" & strKeyID
        Dim strLoginID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SurveyID FROM EQTest WHERE EQTestID=" & strKeyID
        Dim strSurveyID As String = oCommon.getFieldValue(strSQL)

        Dim strLink As String = oCommon.getAppsettings("DomainEQTest") & "home.candidate.aspx?culture=en-US&surveyid=" & strSurveyID & "&loginid=" & strLoginID
        Response.Redirect(strLink, False)


    End Sub

    Protected Sub btnIsComplete_Click(sender As Object, e As EventArgs) Handles btnIsComplete.Click
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

            If isChecked Then
                strSQL = "UPDATE EQTest SET IsCompleted='" & selSetIsCompleted.Value & "' WHERE EQTestID=" & datRespondent.DataKeys(i).Value.ToString
                oCommon.ExecuteSQL(strSQL)
            Else
                '--do nothing
            End If
        Next

        ''refresh screen
        strRet = BindData(datRespondent)
        lblMsg.Text = "Berjaya kemaskini database."

    End Sub

    Private Sub setDdlClass()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT M.ClassID, G.ClassCode FROM( "
        query += " SELECT A.ClassID FROM permatapintar.dbo.PPCS_Class A "
        query += " JOIN permatapintar.dbo.PPCS B ON B.ClassID = A.ClassID AND B.PPCSDate = '" & Commonfunction.getPpcsDate(ddlPPCSDate.SelectedValue) & "' "
        query += " JOIN student_info C ON C.guid = B.StudentID "
        query += " JOIN UKM3 D ON D.student_id = C.std_ID "
        query += " WHERE D.active = 1 AND D.session_id = " & ddlPPCSDate.SelectedValue & " GROUP BY A.ClassID) M  "
        query += " LEFT JOIN permatapintar.dbo.PPCS_Class G ON G.ClassID = M.ClassID "

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        ddlClass.Items.Clear()

        ddlClass.Items.Add(New ListItem("-- Pilih Kelas --", 0))

        For k = 0 To quantity - 1
            ddlClass.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next
    End Sub
End Class