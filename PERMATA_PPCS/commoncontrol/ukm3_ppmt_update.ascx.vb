Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData

Partial Public Class ukm3_ppmt_update
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strTestID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnLayak.Attributes.Add("onclick", "return confirm('Pasti ingin MELAYAKKAN pelajar ke Kolej PERMATApintar?');")
        btnTidakLayak.Attributes.Add("onclick", "return confirm('Pasti ingin TIDAK MELAYAKKAN pelajar ke Kolej PERMATApintar?');")

        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub examyear_list()
        strSQL = "SELECT ExamYear FROM master_examyear ORDER BY ExamYear ASC"

        '--debug
        'Response.Write("examyear_list:" & strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "ExamYear"
            ddlExamYear.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub master_dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDOB_Year.DataSource = ds
            ddlDOB_Year.DataTextField = "DOB_Year"
            ddlDOB_Year.DataValueField = "DOB_Year"
            ddlDOB_Year.DataBind()

            ddlDOB_Year.Items.Add(New ListItem("ALL", "ALL"))

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

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

            '--ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada pelajar seperti kriteria dipilih."
            Else
                lblMsg.Text = "Jumlah Pelajar#:" & myDataSet.Tables(0).Rows.Count
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

        tmpSQL = "SELECT DISTINCT
                    PPCS.StudentID,
                    StudentProfile.StudentFullname,
                    StudentProfile.MYKAD,
                    StudentProfile.AlumniID,
                    StudentProfile.DOB_Year,
                    StudentProfile.StudentGender,
                    PPCS_Course.CourseCode,
                    PPCS_Class.ClassCode,
                    UKM3.PPMT,
                    UKM3.DisplayStatus,
                    UKM3.Program,
                    UKM3.TotalPercentage 
                    FROM PPCS"
        tmpSQL += " LEFT JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT JOIN UKM3 ON UKM3.StudentID=PPCS.StudentID AND UKM3.PPCSDate='" & ddlPPCSDate.Text & "'"
        tmpSQL += " LEFT JOIN PPCS_Course ON PPCS_Course.CourseID=PPCS.CourseID"
        tmpSQL += " LEFT JOIN PPCS_Class ON PPCS_Class.ClassID=PPCS.ClassID"
        tmpSQL += " WHERE PPCS.PPCSDate='" & ddlPPCSDate.Text & "'"
        strWhere = " AND PPCS.PPCSStatus='LAYAK' "

        If Not selProgram.Value = "ALL" Then
            strWhere += " AND UKM3.Program ='" & selProgram.Value & "'"
        End If
        If Not selStudentGender.Value = "ALL" Then
            strWhere += " AND StudentProfile.StudentGender ='" & selStudentGender.Value & "'"
        End If
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND StudentProfile.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        If Not selLayakPPMT.Value = "ALL" Then
            strWhere += " AND UKM3.PPMT ='" & selLayakPPMT.Value & "'"
        End If
        If Not selStatusTawaran.Value = "ALL" Then
            strWhere += " AND UKM3.StatusTawaran ='" & selStatusTawaran.Value & "'"
        End If

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        If selOrderBy.Value = "0" Then
            strOrder = " ORDER BY StudentProfile.StudentFullname"
        Else
            strOrder = " ORDER BY UKM3.TotalPercentage DESC"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        displayDebug(getSQL)

        Return getSQL

    End Function

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub


    'Private Function getSQLExport() As String
    '    Dim tmpSQL As String
    '    Dim strWhere As String = ""
    '    Dim strOrder As String = " ORDER BY a.TotalPercentage DESC"

    '    tmpSQL = "SELECT a.StudentID,b.StudentFullname,b.MYKAD,b.AlumniID,b.DOB_Year,b.StudentGender,c.PPCSCourse,c.PPCSClass,a.PPMT,a.Program,b.StudentAddress1,b.StudentAddress2,b.StudentPostcode,b.StudentCity,b.StudentState,d.FamilyContactNo,d.FatherFullname,d.FamilyContactNoIbu FROM UKM3 a, StudentProfile b, PPCS c, ParentProfile d"
    '    strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.StudentID=c.StudentID AND a.StudentID=d.StudentID AND c.PPCSDate ='" & ddlPPCSDate.Text & "' AND c.PPCSStatus='LAYAK'"

    '    If Not ddlPPCSDate.Text = "ALL" Then
    '        strWhere += " AND a.PPCSDate ='" & ddlPPCSDate.Text & "'"
    '    End If

    '    If Not ddlDOB_Year.Text = "ALL" Then
    '        strWhere += " AND b.DOB_Year ='" & ddlDOB_Year.Text & "'"
    '    End If

    '    If Not selStudentGender.Value = "ALL" Then
    '        strWhere += " AND b.StudentGender ='" & selStudentGender.Value & "'"
    '    End If

    '    If Not selLayakPPMT.Value = "ALL" Then
    '        strWhere += " AND a.PPMT ='" & selLayakPPMT.Value & "'"
    '    End If

    '    If Not selProgram.Value = "ALL" Then
    '        strWhere += " AND a.Program ='" & selProgram.Value & "'"
    '    End If

    '    If Not txtMYKAD.Text.Length = 0 Then
    '        strWhere += " AND b.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
    '    End If

    '    If Not txtStudentFullname.Text.Length = 0 Then
    '        strWhere += " AND b.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
    '    End If

    '    getSQLExport = tmpSQL & strWhere & strOrder
    '    ''--debug
    '    '--Response.Write(getSQLExport)

    '    Return getSQLExport

    'End Function


    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ExportToCSV()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV()
        'Get the data from database into datatable 
        Dim strQuery As String = getSQL()
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileExport.csv")
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

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    ' The ItemValue is in bound column #0...
        '    Dim itemValue As String = e.Row.Cells(0).Text

        '    ' The ItemText is in bound column #1...
        '    Dim itemText As String = e.Row.Cells(1).Text

        '    ' TextBox with an ID of "gridTextBox" is in column #2 of the Grid...
        '    Dim gridTextBox As TextBox = DirectCast(e.Row.Cells(2).FindControl("txtUKM3"), TextBox)

        '    If gridTextBox IsNot Nothing Then
        '        'gridTextBox.Attributes.Add("onchange", "alert('client-side onchange fired');");
        '        gridTextBox.Text = itemValue & " : " & itemText
        '    End If
        'End If
    End Sub


    Private Sub btnLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLayak.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    strSQL = "SELECT StudentID FROM UKM3 WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                    If oCommon.isExist(strSQL) = True Then
                        '--UPDATE IF EXIST
                        strSQL = "UPDATE UKM3 SET PPMT='Y',DisplayStatus='" & selDisplayStatusUKM3.Value & "',Program='" & selProgramUpdate.Value & "',PPYear='" & ddlExamYear.Text & "' WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                        End If
                    Else
                        '--INSERT
                        strSQL = "INSERT INTO UKM3 (StudentID,PPCSDate,Status,IsLayak,PPMT,Program,PPYear,DisplayStatus) VALUES('" & datRespondent.DataKeys(i).Value.ToString & "','" & ddlPPCSDate.Text & "','NEW','Y','Y','" & selProgramUpdate.Value & "','" & ddlExamYear.Text & "','" & selDisplayStatusUKM3.Value & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                        End If
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini status kelayakan Kolej PERMATApintar."
        End If
        strRet = BindData(datRespondent)
    End Sub

    Private Sub btnTidakLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakLayak.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)
                    strSQL = "UPDATE UKM3 SET PPMT='N',DisplayStatus='" & selDisplayStatusUKM3.Value & "',Program='' WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini status kelayakan Kolej PERMATApintar."
        End If
        strRet = BindData(datRespondent)

    End Sub
End Class