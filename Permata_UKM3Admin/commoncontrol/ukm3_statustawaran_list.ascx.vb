Imports System.Data.SqlClient

Public Class ukm3_statustawaran_list
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
        Try
            If Not IsPostBack Then
                ppcsdate_list()

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT PPCSDate FROM PPCS GROUP BY PPCSDate"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

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

        Dim currentPpcs As String = Commonfunction.getSingleCellValue("SELECT configString FROM master_Config WHERE configCode = 'DefaultPPCSDate'")
        ddlPPCSDate.SelectedValue = currentPpcs
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
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada pelajar seperti kriteria dipilih."
            Else
                divMsg.Attributes("class") = "info"
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
        Dim strOrder As String = " ORDER BY b.StudentFullname"

        tmpSQL = " SELECT a.StudentID,b.StudentFullname,b.MYKAD,b.AlumniID,b.DOB_Year,b.StudentGender,a.PPMT,a.Program,a.StatusTawaran,a.StatusDate,a.StatusReason "
        tmpSQL += " FROM UKM3 a WITH (NOLOCK) LEFT JOIN StudentProfile b ON a.StudentID=b.StudentID "
        tmpSQL += " LEFT JOIN PPCS c ON a.StudentID=c.StudentID AND c.PPCSDate ='" & ddlPPCSDate.Text & "' AND c.PPCSStatus='LAYAK' AND c.StatusTawaran='TERIMA' "

        strWhere = " WHERE  a.PPCSDate ='" & ddlPPCSDate.Text & "' "

        'If Not ddlPPCSDate.Text = "ALL" Then
        '    strWhere += " AND a.PPCSDate ='" & ddlPPCSDate.Text & "'"
        'End If

        If Not selProgram.Value = "ALL" Then
            strWhere += " AND a.Program ='" & selProgram.Value & "'"
        End If

        If Not selStudentGender.Value = "ALL" Then
            strWhere += " AND b.StudentGender ='" & selStudentGender.Value & "'"
        End If

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND b.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        If Not selLayakPPMT.Value = "ALL" Then
            strWhere += " AND a.PPMT ='" & selLayakPPMT.Value & "'"
        End If
        If Not selStatusTawaran.Value = "ALL" Then
            strWhere += " AND a.StatusTawaran ='" & selStatusTawaran.Value & "'"
        End If

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND b.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND b.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        'getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        '--Response.Write(getSQL)

        Return tmpSQL & strWhere & strOrder

    End Function

    Private Function getSQLExport() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY b.StudentFullname"

        tmpSQL = " SELECT a.StudentID,b.StudentFullname,b.MYKAD,b.AlumniID,b.DOB_Year,b.StudentGender,e.ClassCode,a.PPMT,a.Program,a.StatusTawaran "
        tmpSQL += " ,a.StatusDate,a.StatusReason, f.FamilyContactNo, f.FamilyContactNoIbu, f.FatherJob, f.MotherJob, h.SchoolState ,h.SchoolCode ,h.SchoolName"
        tmpSQL += " FROM UKM3 a WITH (NOLOCK) LEFT JOIN StudentProfile b ON a.StudentID=b.StudentID "
        tmpSQL += " LEFT JOIN PPCS c ON a.StudentID=c.StudentID AND c.StatusTawaran='TERIMA' and c.PPCSStatus='LAYAK' AND  c.PPCSDate ='" & ddlPPCSDate.Text & "' "
        tmpSQL += " LEFT JOIN PPCS_Class e ON c.ClassID=e.ClassID "
        tmpSQL += " LEFT JOIN ParentProfile f ON f.StudentID = a.StudentID "
        tmpSQL += " LEFT JOIN StudentSchool g ON g.StudentID = a.StudentID AND G.IsLatest = 'Y' "
        tmpSQL += " LEFT JOIN SchoolProfile h ON h.SchoolID = g.SchoolID "

        strWhere = " WHERE a.PPCSDate ='" & ddlPPCSDate.Text & "' "

        'If Not ddlPPCSDate.Text = "ALL" Then
        '    strWhere += " AND a.PPCSDate ='" & ddlPPCSDate.Text & "'"
        'End If

        If Not selProgram.Value = "ALL" Then
            strWhere += " AND a.Program ='" & selProgram.Value & "'"
        End If

        If Not selStudentGender.Value = "ALL" Then
            strWhere += " AND b.StudentGender ='" & selStudentGender.Value & "'"
        End If

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND b.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        If Not selLayakPPMT.Value = "ALL" Then
            strWhere += " AND a.PPMT ='" & selLayakPPMT.Value & "'"
        End If
        If Not selStatusTawaran.Value = "ALL" Then
            strWhere += " AND a.StatusTawaran ='" & selStatusTawaran.Value & "'"
        End If

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND b.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND b.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        ''--debug
        '--Response.Write(getSQL)

        'Debug.WriteLine(tmpSQL & strWhere & strOrder)

        Return tmpSQL & strWhere & strOrder

    End Function


    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ExportToCSV()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV()
        'Get the data from database into datatable 
        Dim strQuery As String = getSQLExport()
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

    Protected Sub btnUpdateStatusTawaran_Click(sender As Object, e As EventArgs) Handles btnUpdateStatusTawaran.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            Debug.WriteLine(chkUpdate)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    '--UPDATE IF EXIST
                    strSQL = "UPDATE permatapintar.dbo.UKM3 SET permatapintar.dbo.UKM3.StatusDate='" & oCommon.getNow & "',
                                permatapintar.dbo.UKM3.StatusTawaran='" & selStatusTawaranUpdate.Value & "',
                                permatapintar.dbo.UKM3.StatusReason='" & oCommon.FixSingleQuotes(txtStatusReason.Text) & "'
                                WHERE permatapintar.dbo.UKM3.StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND 
                                permatapintar.dbo.UKM3.PPCSDate='" & ddlPPCSDate.Text & "'"

                    strRet = oCommon.ExecuteSQL(strSQL)
                    Debug.WriteLine(strRet)
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