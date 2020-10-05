Imports System.Data.SqlClient

Public Class pcis_jsc_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                examyear_list()
                jscsession()
                ddlExamYear.SelectedValue = oCommon.getpcis_setting("exam_year")

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub jscsession()
        strSQL = "select description from pcis_jsc_config order by year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSessiJsc.DataSource = ds
            ddlSessiJsc.DataTextField = "description"
            ddlSessiJsc.DataValueField = "description"
            ddlSessiJsc.DataBind()

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub examyear_list()

        '--Limit examyear access
        Select Case get_pcis_admin_UserType()
            Case "ADMIN"
                strSQL = "SELECT * FROM pcis_exam_year WITH (NOLOCK) ORDER BY description"
            Case "SUBADMIN"
                strSQL = "SELECT * FROM pcis_exam_year WITH (NOLOCK) ORDER BY description"
            Case Else
                strSQL = "SELECT * FROM pcis_exam_year WITH (NOLOCK) WHERE description='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY description"
        End Select
        '--debug
        'Response.Write(strSQL)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "description"
            ddlExamYear.DataValueField = "id"
            ddlExamYear.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function get_pcis_admin_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM pcis_admin WITH (NOLOCK) WHERE LoginID='" & Request.Cookies("pcis_admin").Value & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        'strRet = BindData(datRespondent)

        If Not selStatus.Value = "All" Then
            BindDataSP(datRespondent, "spJSC")
        Else
            BindDataSP2(datRespondent, "spJSC2")
        End If
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
        command.Parameters.AddWithValue("@fullname", txtfullname.Text) '--parameters
        command.Parameters.AddWithValue("@examyearid", ddlExamYear.SelectedValue)    '--parameters
        command.Parameters.AddWithValue("@icno", txticno.Text)    '--parameters
        command.Parameters.AddWithValue("@done", selStatus.Value)
        command.Parameters.AddWithValue("@sortby", selSort.Value)    '--parameters

        adapter = New SqlDataAdapter(command)
        adapter.Fill(ds)
        lblMsg.Text = "Jumlah Rekod#:" & ds.Tables(0).Rows.Count

        connection.Close()
        gvTable.DataSource = ds.Tables(0)
        gvTable.DataBind()

    End Sub

    Private Sub BindDataSP2(ByVal gvTable As GridView, ByVal strCommandText As String)
        Dim command As New SqlCommand()
        Dim adapter As New SqlDataAdapter()
        Dim dt As New DataSet()
        Dim i As Integer = 0
        Dim sql As String = Nothing
        Dim connectionString As String = strConn
        Dim connection As New SqlConnection(connectionString)

        connection.Open()
        command.Connection = connection
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = strCommandText    '--sp name
        command.Parameters.AddWithValue("@fullname", txtfullname.Text) '--parameters
        command.Parameters.AddWithValue("@examyearid", ddlExamYear.SelectedValue)    '--parameters
        command.Parameters.AddWithValue("@icno", txticno.Text)    '--parameters
        command.Parameters.AddWithValue("@done", selStatus.Value)
        command.Parameters.AddWithValue("@sortby", selSort.Value)    '--parameters

        adapter = New SqlDataAdapter(command)
        adapter.Fill(dt)
        lblMsg.Text = "Jumlah Rekod#:" & dt.Tables(0).Rows.Count

        connection.Close()
        gvTable.DataSource = dt.Tables(0)
        gvTable.DataBind()

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Select Case get_pcis_admin_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.mark.view.aspx?id=" & strKeyID)
            Case "SUBADMIN"
            Case "KPT"
            Case "ASASI"
            Case Else
        End Select

    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        If Not selStatus.Value = "All" Then
            BindDataSP(datRespondent, "spJSC")
        Else
            BindDataSP2(datRespondent, "spJSC2")
        End If


    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim strFilename As String = "ExportSummary-" & oCommon.getRandom & ".csv"

            If Not selStatus.Value = "All" Then
                ExportToCSVSP("spJSC", strFilename)
            Else
                ExportToCSVSP("spJSC2", strFilename)
            End If
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
    End Sub

    Private Sub ExportToCSVSP(ByVal strCommandText As String, ByVal strFilename As String)
        Dim command As New SqlCommand()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet("mytable")
        Dim sql As String = Nothing
        Dim connectionString As String = strConn
        Dim connection As New SqlConnection(connectionString)

        connection.Open()
        command.Connection = connection
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = strCommandText    '--sp name
        command.Parameters.AddWithValue("@fullname", txtfullname.Text) '--parameters
        command.Parameters.AddWithValue("@examyearid", ddlExamYear.SelectedValue)    '--parameters
        command.Parameters.AddWithValue("@icno", txticno.Text)    '--parameters
        command.Parameters.AddWithValue("@done", selStatus.Value)    '--1-done,0-not done,null- both
        command.Parameters.AddWithValue("@sortby", selSort.Value)    '--parameters

        '--conver to datatable
        Dim dz As DataTable = GetDataSP(command)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & strFilename)
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dz.Columns.Count - 1
            'add separator 
            sb.Append(dz.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dz.Rows.Count - 1
            For k As Integer = 0 To dz.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dz.Rows(i)(k).ToString()
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

    Private Function GetDataSP(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.StoredProcedure
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

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click

        Dim i As Integer = 0

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    strSQL = "select description from pcis_exam_year where id = '" & ddlExamYear.SelectedValue & "'"
                    strRet = oCommon.getFieldValue(strSQL)

                    If strRet.Length > 0 Then
                        strSQL = "select jsc_id from pcis_jsc where user_id ='" & strKey & "' and status_jsc like '%" & strRet & "%' "
                        strRet = oCommon.getFieldValue(strSQL)

                        strSQL = "update pcis_jsc set status_jsc = '" & ddlSessiJsc.SelectedValue & " - " & ddlStatusJsc.Value & "' where jsc_id = '" & strRet & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        strSQL = "insert into pcis_jsc(user_id,status_jsc) values ('" & strKey & "','" & ddlSessiJsc.SelectedValue & " - " & ddlStatusJsc.Value & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                End If
            End If
        Next

        If Not selStatus.Value = "All" Then
            BindDataSP(datRespondent, "spJSC")
        Else
            BindDataSP2(datRespondent, "spJSC2")
        End If
    End Sub
End Class