Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData

Partial Public Class ukm3_mark_update
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
        btnTransfer.Attributes.Add("onclick", "return confirm('Pasti ingin transfer ke UKM3?');")

        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                '--check current record
                lblIsTransfered.Text = isTransfered().ToString
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Function isTransfered() As Boolean
        '--transfer if only UKM3 record not exist
        strSQL = "SELECT UKM3ID FROM UKM3 WHERE PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            Return True
        Else
            Return False
        End If

    End Function

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
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT UKM3.UKM3ID,UKM3.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentGender,StudentProfile.StudentReligion,ParentProfile.FamilyContactNo,ParentProfile.FatherMYKADNo,ParentProfile.FatherFullname,ParentProfile.FatherJob,ParentProfile.MotherMYKADNo,ParentProfile.MotherFullname,UKM3.PPMT,UKM3.Program,UKM3.StatusTawaran,UKM3.TotalPercentage,SchoolProfile.SchoolName,SchoolProfile.SchoolCity,SchoolProfile.SchoolState,PPCS_Course.CourseCode,PPCS_Class.ClassCode FROM UKM3"
        tmpSQL += " LEFT OUTER JOIN PPCS ON UKM3.StudentID=PPCS.StudentID AND PPCS.PPCSDate='" & ddlPPCSDate.Text & "'"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM3.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM3.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM3.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        strWhere = " WHERE UKM3.PPCSDate='" & ddlPPCSDate.Text & "'"

        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND UKM3.PPCSDate ='" & ddlPPCSDate.Text & "'"
        End If
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

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

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

    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        '--transfer if only UKM3 record not exist
        If isTransfered() = False Then
            UKM3_insert()
        Else
            lblMsg.Text = "Rekod UKM3 sudah ditransfer sebelum ini."
        End If

    End Sub

    Private Sub UKM3_insert()
        ' Create source connection
        Dim source As New SqlConnection(strConn)
        ' Create destination connection
        Dim destination As New SqlConnection(strConn)
        ' Clean up destination table. Your destination database must have the 
        ' table with schema which you are copying data to. 
        ' Before executing this code, you must create a table BulkDataTable 
        ' in your database where you are trying to copy data to.
        Dim cmd As New SqlCommand("DELETE FROM UKM3 WHERE PPCSDate='" & ddlPPCSDate.Text & "'", destination)    ''dont have to cleanup
        ''dont have to cleanup
        ' Open source and destination connections.
        Try
            source.Open()
            destination.Open()
            cmd.ExecuteNonQuery()

            ' Select data from Products table
            cmd = New SqlCommand(SQLTransfer, source)    '--sama dengan search
            ' Execute reader
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            ' Create SqlBulkCopy
            Dim bulkData As New SqlBulkCopy(destination)
            ' Set destination table name
            bulkData.DestinationTableName = "UKM3"
            ' Write data
            bulkData.WriteToServer(reader)
            ' Close objects
            bulkData.Close()
            destination.Close()
            source.Close()

            lblMsg.Text = "Transfer completed!"
        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Function SQLTransfer() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentID"

        tmpSQL = "SELECT PPCSID,StudentID,ExamYear,PPCSDate FROM PPCS"
        strWhere = " WITH (NOLOCK) WHERE PPCSStatus='LAYAK' AND StatusTawaran='TERIMA'"

        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND PPCSDate ='" & ddlPPCSDate.Text & "'"
        End If

        SQLTransfer = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(SQLTransfer)

        Return SQLTransfer

    End Function

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim gridTextBox As TextBox = DirectCast(row.FindControl("txtTotalPercentage"), TextBox)

            strSQL = "UPDATE UKM3 SET TotalPercentage='" & gridTextBox.Text & "' WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString
            End If
        Next

        ''refresh screen
        strRet = BindData(datRespondent)
        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya kemaskini markah UKM3."
        End If

    End Sub
End Class