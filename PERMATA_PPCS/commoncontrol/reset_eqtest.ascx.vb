Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData


Public Class reset_eqtest1
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
                btnDelete.Visible = False

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                setAccessRight()

                master_surveyid_list()
                ddlSurveyID.Text = "ALL"

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

    Private Sub master_surveyid_list()
        '--base on usertype. admin only allow all

        strSQL = "SELECT SurveyID FROM master_surveyid ORDER BY master_surveyid ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
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

            ddlSurveyID.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        strSQL = oCommon.PPCSDate_Query(Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value))

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--source
            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub setAccessRight()
        Dim strUserType As String = Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)

        Select Case strUserType
            Case "ADMIN"
                btnExport.Visible = True
                btnDelete.Visible = True

            Case "KETUA PENGURUS AKADEMIK"

            Case "PENGURUS AKADEMIK"

            Case "KETUA MODUL"

            Case "PENGAJAR"

            Case "PEMBANTU PENGAJAR"

            Case "PENGURUS PELAJAR"

            Case "PEMBANTU PELAJAR"

            Case "PENGURUS PEJABAT"

            Case Else
                btnExport.Visible = False
                btnDelete.Visible = False

        End Select

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

        tmpSQL = "SELECT EQTest.EQTestID,EQTest.LastUpdate,EQTest.Fullname,EQTest.EmailAdd,StudentProfile.StudentFullname,StudentProfile.StudentEmail,StudentProfile.AlumniID,EQTest.SurveyID,EQTest.LastPage,EQTest.IsCompleted,EQTest.ScorePercentage FROM EQTest"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON EQTest.StudentID=StudentProfile.StudentID"
        strWhere = " WHERE IsDeleted='N'"


        '--PPCSDate
        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND EQTest.PPCSDate='" & ddlPPCSDate.Text & "'"
        End If

        '--IsCompleted
        If Not selIsCompleted.Value.Length = 0 Then
            strWhere += " AND EQTest.IsCompleted='" & selIsCompleted.Value & "'"
        End If

        '--AlumniID
        If Not txtAlumniID.Text.Length = 0 Then
            strWhere += " AND StudentProfile.AlumniID='" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "'"
        End If

        '--EmailAdd
        If Not txtEmailAdd.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentEmail LIKE '%" & oCommon.FixSingleQuotes(txtEmailAdd.Text) & "%'"
        End If

        '--Fullname
        If Not txtFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtFullname.Text) & "%'"
        End If

        '--SurveyID
        If Not ddlSurveyID.Text = "ALL" Then
            strWhere += " AND EQTest.SurveyID='" & ddlSurveyID.Text & "'"
        End If

        If selOrderBy.Value = "1" Then
            strOrder = " ORDER BY EQTest.LastUpdate DESC"
        Else
            strOrder = " ORDER BY EQTest.ScorePercentage DESC"
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

End Class