Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ukm1_view
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnReset.Attributes.Add("onclick", "return confirm('Pasti ingin RESET EXAMSTART rekod tersebut?');")
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin MENGHAPUSKAN rekod tersebut?');")
        If Not IsPostBack Then
            strRet = BindData(datRespondent)
        End If

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT * FROM UKM1"
        strWhere = " WITH (NOLOCK) WHERE UKM1ID=" & Request.QueryString("ukm1id")
        
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

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

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        '--reset examstart

        Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

        lblMsg.Text = ""

        Dim stdId As String = oCommon.getFieldValue("SELECT StudentID FROM UKM1 WHERE UKM1ID = " & Request.QueryString("ukm1id"))

        strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET ExamStart=NULL,ExamEnd=NULL,Status='NEW',LastPage=NULL WHERE StudentID=" & stdId
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET ExamStart=NULL,ExamEnd=NULL,Status='NEW',LastPage=NULL WHERE UKM1ID=" & Request.QueryString("ukm1id")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya reset Ujian UKM1 pelajar tersebut."
        Else
            lblMsg.Text = "Gagal reset Ujian UKM1 pelajar tersebut" & strRet
        End If

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblMsg.Text = ""
        '--backup before delete
        strSQL = "SELECT * FROM UKM1 WHERE UKM1ID=" & Request.QueryString("ukm1id")
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_History")
        lblMsg.Text += "UKM1_History:" & strRet & vbCrLf

        '--DELETE

        Dim stdId As String = oCommon.getFieldValue("SELECT StudentID FROM UKM1 WHERE UKM1ID = '" & Request.QueryString("ukm1id") & "'")

        strSQL = "DELETE UKM1_" & getUKM1Year & " WHERE StudentID='" & stdId & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "DELETE UKM1 WHERE UKM1ID=" & Request.QueryString("ukm1id")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text += "Berjaya DELETE Ujian UKM1 pelajar tersebut."
        Else
            lblMsg.Text += "Gagal DELETE Ujian UKM1 pelajar tersebut" & strRet
        End If

    End Sub

    Protected Sub lnkViewProfile_Click(sender As Object, e As EventArgs) Handles lnkViewProfile.Click
        Response.Redirect("admin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub

    Protected Sub btnLayak_Click(sender As Object, e As EventArgs) Handles btnLayak.Click
        Dim strStudentID As String = Request.QueryString("studentid")

        '--ExamYear
        Dim strExamYear As String = ""
        strSQL = "SELECT ExamYear FROM UKM1 WHERE UKM1ID=" & Request.QueryString("ukm1id")
        strExamYear = oCommon.getFieldValue(strSQL)

        If UKM2Layak(strStudentID, strExamYear) = True Then
            lblMsg.Text = "BERJAYA melayakkan pelajar ke Ujian UKM2."
        End If

    End Sub

    Private Function UKM2Layak(ByVal strStudentID As String, ByVal strExamYear As String) As Boolean
        '--get SchoolID
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
        strSchoolID = oCommon.getFieldValue(strSQL)

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")
        Using connection As New SqlConnection(strconn)
            connection.Open()

            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction("TxnStart")

            ' Must assign both transaction object and connection 
            ' to Command object for a pending local transaction.
            command.Connection = connection
            command.Transaction = transaction
            command.CommandTimeout = 300    '5minit. timeout in second

            Try
                '--initial status=SYSTEM
                strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,SchoolID,Status) VALUES('" & strStudentID & "','" & strExamYear & "','" & strSchoolID & "','NEW')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                strSQL = "INSERT INTO UKM2_Answer (StudentID,ExamYear) VALUES('" & strStudentID & "','" & strExamYear & "')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                If strExamYear = getUKM1Year Then
                    strSQL = "UPDATE UKM1_" & getUKM1Year & " SET isLayak='Y' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                strSQL = "UPDATE UKM1 SET isLayak='Y' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                '--update UKM2 report information
                oCommon.UKM2_StudentprofileUpdate(strStudentID, strExamYear)
                oCommon.UKM2_SchoolprofileUpdate(strStudentID, strExamYear)

                Return True

            Catch ex As Exception
                'Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
                'Console.WriteLine("  Message: {0}", ex.Message)
                strRet = "UKM2Layak:" & ex.Message
                ' Attempt to roll back the transaction. 
                Try
                    transaction.Rollback()
                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    'Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    'Console.WriteLine("  Message: {0}", ex2.Message)
                    strRet += "Rollback:" & ex2.Message
                End Try
                lblMsg.Text = strRet
                Return False
            End Try
        End Using

    End Function

    Private Sub btnTidakLayak_Click(sender As Object, e As EventArgs) Handles btnTidakLayak.Click
        Dim strStudentID As String = Request.QueryString("studentid")

        '--ExamYear
        Dim strExamYear As String = ""
        strSQL = "SELECT ExamYear FROM UKM1 WHERE UKM1ID=" & Request.QueryString("ukm1id")
        strExamYear = oCommon.getFieldValue(strSQL)

        If UKM2TidakLayak(strStudentID, strExamYear) = True Then
            lblMsg.Text = "Pelajar TIDAK LAYAK untuk menduduki Ujian UKM2."
        End If

    End Sub

    Private Function UKM2TidakLayak(ByVal strStudentID As String, ByVal strExamYear As String) As Boolean
        strRet = ""
        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using connection As New SqlConnection(strconn)
            connection.Open()

            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction("TxnStart")

            ' Must assign both transaction object and connection 
            ' to Command object for a pending local transaction.
            command.Connection = connection
            command.Transaction = transaction
            command.CommandTimeout = 300    '5minit. timeout in second

            Try
                '--backup before delete
                strSQL = "SELECT * FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                strRet = oCommon.Bulk_Transfer(strSQL, "UKM2_History")

                '--delete UKM2
                strSQL = "DELETE UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--UKM2_Answer
                strSQL = "DELETE UKM2_Answer WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--reset kelayakan

                Dim stdId As String = oCommon.getFieldValue("SELECT StudentID FROM UKM1 WHERE UKM1ID = " & Request.QueryString("ukm1id"))

                strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET isLayak='N' WHERE StudentID=" & stdId
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET isLayak='N' WHERE UKM1ID=" & Request.QueryString("ukm1id")
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                Return True

            Catch ex As Exception
                'Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
                'Console.WriteLine("  Message: {0}", ex.Message)
                strRet = ex.Message
                ' Attempt to roll back the transaction. 
                Try
                    transaction.Rollback()
                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    'Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    'Console.WriteLine("  Message: {0}", ex2.Message)
                    strRet += "Rollback:" & ex2.Message
                End Try
                lblMsg.Text = strRet
                Return False
            End Try
        End Using

    End Function

End Class