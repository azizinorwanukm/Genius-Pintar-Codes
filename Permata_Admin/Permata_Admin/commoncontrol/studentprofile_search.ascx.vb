Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class studentprofile_search
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod pelajar tersebut?');")

        Try
            If Not IsPostBack Then
                lblMsg.Text = ""
                lblMsgTop.Text = ""

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"
            End If
        Catch ex As Exception

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


    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentGender,StudentProfile.StudentRace,StudentProfile.StudentReligion,SchoolProfile.SchoolCode FROM StudentProfile WITH (NOLOCK)"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON StudentProfile.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere += " WHERE StudentProfile.StudentID IS NOT NULL"

        ''txtMYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        ''txtStudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
                Case "ADMINOP"
                    Response.Redirect("studentprofile.view.aspx?studentid=" & strKeyID)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID)
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        '--make do somthing
        If ValidateSearch() = False Then
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        strRet = BindData(datRespondent)

    End Sub

    Private Function ValidateSearch()
        '--salah satu kena isi
        If txtMYKAD.Text.Length = 0 And txtStudentFullname.Text.Length = 0 Then
            lblMsg.Text = "Sila isikan sama ada Nama Pelajar atau MYKAD#"
            txtStudentFullname.Focus()
            Return False
        End If

        Return True
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

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(8).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    StudentProfile_delete(strID)
                    lblDebug.Text += lblMsg.Text
                End If
            End If
        Next

        ''--refresh screen
        strRet = BindData(datRespondent)

    End Sub

    'Delete Profile
    Private Sub StudentProfile_delete(ByVal strStudentID As String)
        lblMsg.Text = "StudentID:" & strStudentID & "<br />"
        ''--backup 
        strSQL = "SELECT * FROM StudentProfile WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentProfile_History")
        lblMsg.Text += "Backup StudentProfile_History:" & strRet & "|"

        ''--backup 
        strSQL = "SELECT * FROM StudentPhoto WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentPhoto_History")
        lblMsg.Text += "Backup StudentPhoto_History:" & strRet & "|"

        ''--backup 
        strSQL = "SELECT * FROM ParentProfile WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "ParentProfile_History")
        lblMsg.Text += "Backup ParentProfile_History:" & strRet & "|"

        ''--backup 
        strSQL = "SELECT * FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentSchool_History")
        lblMsg.Text += "Backup StudentSchool_History:" & strRet & "|"

        ''--backup 
        strSQL = "SELECT * FROM UKM1 WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_History")
        lblMsg.Text += "Backup UKM1_History:" & strRet & "|"

        ''--backup 
        strSQL = "SELECT * FROM UKM1_Answer WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_Answer_History")
        lblMsg.Text += "Backup UKM1_Answer_History:" & strRet & "|"

        ''--backup UKM2
        strSQL = "SELECT * FROM UKM2 WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM2_History")
        lblMsg.Text += "Backup UKM2_History:" & strRet & "<br />"

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
                ''delete studentprofile
                strSQL = "DELETE FROM StudentProfile WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete studentphoto
                strSQL = "DELETE FROM StudentPhoto WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ParentProfile
                strSQL = "DELETE FROM ParentProfile WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete studentschool
                strSQL = "DELETE FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ukm1
                strSQL = "DELETE FROM UKM1 WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                strSQL = "DELETE FROM UKM1_" & getUKM1Year & " WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ukm1_answer
                strSQL = "DELETE FROM UKM1_Answer WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete UKM2
                strSQL = "DELETE FROM UKM2 WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete UKM2_Answer
                strSQL = "DELETE FROM UKM2_Answer WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                lblMsg.Text += "DELETE Maklumat Pelajar completed!" & strRet & "<br />"

            Catch ex As Exception
                'Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
                'Console.WriteLine("  Message: {0}", ex.Message)
                strRet += ex.Message
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
                lblMsg.Text += strRet
            End Try
        End Using

    End Sub

End Class