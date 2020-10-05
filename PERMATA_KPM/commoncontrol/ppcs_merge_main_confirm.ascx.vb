Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class ppcs_merge_main_confirm
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnMerge.Attributes.Add("onclick", "return confirm('Pasti ingin MERGE akaun pelajar tersebut?');")

        Try
            If Not IsPostBack Then
                '--must select age
                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub


    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod dijumpai."
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
        Dim strOrder As String = " ORDER BY StudentFullname ASC"

        '-kena ambil ukm1 dan ukm2 in the same year
        tmpSQL = "SELECT StudentID,StudentFullname,MYKAD,DOB_Year,StudentRace,StudentGender,AlumniID FROM StudentProfile"
        strWhere = " WITH (NOLOCK) WHERE StudentID<>'" & Request.QueryString("mainstudentid") & "'"

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
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

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPPCSDate As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

            lblPPCSDate = e.Row.FindControl("lblPPCSDate")
            lblPPCSDate.Text = getPPCSDateValues(strKeyID)

        End If
    End Sub

    Private Function getPPCSDateValues(ByVal strStudentID As String) As String
        Dim strValues As String = ""
        Dim i As Integer = 0

        strSQL = "SELECT PPCSDate FROM PPCS WHERE StudentID='" & strStudentID & "' ORDER BY PPCSDate"

        Try
            Dim comm As New SqlCommand(strSQL, objConn)
            objConn.Open()

            Dim reader As SqlDataReader = comm.ExecuteReader()
            While reader.Read()
                strValues += reader.GetValue(0).ToString() & ","
            End While
            reader.Close()

            If strValues.Length > 0 Then
                strValues = strValues.Substring(0, strValues.Length - 1)
            End If
            Return strValues
        Catch ex As Exception
            Return ex.Message
        Finally
            objConn.Close()
        End Try

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ppcs.alumni.studentprofile.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub


    Private Function ExportData(ByVal dsTable As DataSet, ByVal strTitle As String) As String
        ''-Dim strFilename As String = Server.MapPath(".") & "log\" & "Export." & oCommon.getRandom & ".txt"
        Dim strFilename As String = strTitle & oCommon.getRandom & ".txt"

        Try
            ' Export all the details to xls
            Dim objExport As New RKLib.ExportData.Export("Web")
            Dim dtRespondent As DataTable = dsTable.Tables("mytable").Copy()
            objExport.ExportDetails(dtRespondent, Export.ExportFormat.CSV, strFilename)

            Return strFilename
        Catch Ex As Exception
            Return Ex.Message
        End Try

    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""

        '--make do somthing
        If ValidatePage() = False Then
            Exit Sub
        End If

        strRet = BindData(datRespondent)

    End Sub

    Private Function ValidatePage() As Boolean
        If txtMYKAD.Text.Length = 0 Then
            If txtStudentFullname.Text.Length = 0 Then
                lblMsg.Text = "Sila isikan sama ada Nama Pelajar atau MYKAD#"
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub btnMerge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMerge.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        Dim i As Integer

        Try
            'Loop through gridview rows to find checkbox 
            'and check whether it is checked or not 
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
                        If MergeAccount(strID) = True Then
                            'lblMsg.Text = "Telah berjaya MERGE akaun pilihan kepada MAIN ACCOUNT pelajar."
                            Response.Redirect("admin.ppcs.merge.main.confirm.msg.aspx?msg=SUCCESS&studentid=" & Request.QueryString("mainstudentid") & "&substudentid=" & strID)
                        End If
                    Else
                        lblMsgTop.Text = "Sila pilih sekurang-kurangnya satu."
                    End If
                End If
            Next

        Catch ex As Exception
            lblMsg.Text += "Err: Contact admin. " & ex.Message
        End Try

    End Sub

    Private Function MergeAccount(ByVal strStudentID As String) As Boolean

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
                '--set priority to avoid deadlock
                command.CommandText = "SET DEADLOCK_PRIORITY NORMAL"
                command.ExecuteNonQuery()

                '--1 PPCS
                strSQL = "UPDATE PPCS WITH (UPDLOCK) SET StudentID='" & Request.QueryString("mainstudentid") & "' WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                'PPCS_Eval_Daily
                strSQL = "UPDATE PPCS_Eval_Daily WITH (UPDLOCK) SET StudentID='" & Request.QueryString("mainstudentid") & "' WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                'PPCS_Eval_End
                strSQL = "UPDATE PPCS_Eval_End WITH (UPDLOCK) SET StudentID='" & Request.QueryString("mainstudentid") & "' WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                'PPCS_Eval_Weekly
                strSQL = "UPDATE PPCS_Eval_Weekly WITH (UPDLOCK) SET StudentID='" & Request.QueryString("mainstudentid") & "' WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--2 UKM1
                '-check if mainstudentid taken the test or not for that year
                strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET StudentID='" & Request.QueryString("mainstudentid") & "' WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                strSQL = "UPDATE UKM1_Answer SET StudentID='" & Request.QueryString("mainstudentid") & "' WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--3 UKM2
                strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET StudentID='" & Request.QueryString("mainstudentid") & "' WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                strSQL = "UPDATE UKM2_Answer SET StudentID='" & Request.QueryString("mainstudentid") & "' WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                Return True

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
                Return False
            End Try
        End Using

    End Function

    Private Function StudentProfile_Delete(ByVal strStudentID As String) As Boolean
        lblMsg.Text = ""
        If Not getUserProfile_UserType() = "ADMIN" Then
            lblMsg.Text = "Anda tidak dibenarkan menggunakan fungsi ini."
            Return False
        End If

        ''--backup 
        strSQL = "SELECT * FROM StudentProfile WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentProfile_History")
        lblMsg.Text = "Backup StudentProfile_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM StudentPhoto WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentPhoto_History")
        lblMsg.Text += "Backup StudentPhoto_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM ParentProfile WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "ParentProfile_History")
        lblMsg.Text += "Backup ParentProfile_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentSchool_History")
        lblMsg.Text += "Backup StudentSchool_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM UKM1 WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_History")
        lblMsg.Text += "Backup UKM1_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM UKM1_Answer WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_Answer_History")
        lblMsg.Text += "Backup UKM1_Answer_History:" & strRet & "<br />"

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

                ''delete ukm1_answer
                strSQL = "DELETE FROM UKM1_Answer WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete UKM2
                strSQL = "DELETE FROM UKM2 WHERE StudentID='" & strStudentID & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                lblMsg.Text += "DELETE Student Profile completed!" & strRet & "<br />"
                Return True

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
                Return False
            End Try
        End Using

    End Function


End Class