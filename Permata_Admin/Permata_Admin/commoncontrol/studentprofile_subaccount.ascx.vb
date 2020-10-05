Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class studentprofile_subaccount
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lnkDelete.Attributes.Add("onclick", "return confirm('Pasti ingin DELETE SUB ACCOUNT tersebut?');")

        Try
            StudentProfile_header()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub StudentProfile_header()
        strSQL = "SELECT * FROM StudentProfile WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Rekod SUB ACCOUNT tidak dijumpai atau sudah di DELETE!"
            Exit Sub
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM StudentProfile WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("MYKAD")) Then
                    lblMYKAD.Text = MyTable.Rows(nRows).Item("MYKAD").ToString
                Else
                    lblMYKAD.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentFullname")) Then
                    lblStudentFullname.Text = MyTable.Rows(nRows).Item("StudentFullname").ToString
                Else
                    lblStudentFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("AlumniID")) Then
                    lblAlumniID.Text = MyTable.Rows(nRows).Item("AlumniID").ToString
                Else
                    lblAlumniID.Text = ""
                End If

            End If
        Catch ex As Exception
            Response.Write("system error:" & ex.Message)
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkDelete.Click
        StudentProfile_Delete()

    End Sub

    Private Sub StudentProfile_Delete()
        '--DELETE SUB ACCOUNT

        lblMsg.Text = ""
        If Not getUserProfile_UserType() = "ADMIN" Then
            lblMsg.Text = "Anda tidak dibenarkan menggunakan fungsi ini."
            Exit Sub
        End If

        strSQL = "SELECT * FROM StudentProfile WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Rekod SUB ACCOUNT tidak dijumpai!"
            Exit Sub
        End If

        ''--backup 
        strSQL = "SELECT * FROM StudentProfile WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentProfile_History")
        lblMsg.Text = "Backup StudentProfile_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM StudentPhoto WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentPhoto_History")
        lblMsg.Text += "Backup StudentPhoto_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM ParentProfile WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "ParentProfile_History")
        lblMsg.Text += "Backup ParentProfile_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM StudentSchool WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentSchool_History")
        lblMsg.Text += "Backup StudentSchool_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM UKM1 WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_History")
        lblMsg.Text += "Backup UKM1_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM UKM1_Answer WHERE StudentID='" & Request.QueryString("substudentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_Answer_History")
        lblMsg.Text += "Backup UKM1_Answer_History:" & strRet & "<br />"

        ''--backup UKM2
        strSQL = "SELECT * FROM UKM2 WHERE StudentID='" & Request.QueryString("substudentid") & "'"
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
                strSQL = "DELETE FROM StudentProfile WHERE StudentID='" & Request.QueryString("substudentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete studentphoto
                strSQL = "DELETE FROM StudentPhoto WHERE StudentID='" & Request.QueryString("substudentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ParentProfile
                strSQL = "DELETE FROM ParentProfile WHERE StudentID='" & Request.QueryString("substudentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete studentschool
                strSQL = "DELETE FROM StudentSchool WHERE StudentID='" & Request.QueryString("substudentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ukm1
                strSQL = "DELETE FROM UKM1 WHERE StudentID='" & Request.QueryString("substudentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                strSQL = "DELETE FROM UKM1_" & getUKM1Year & " WHERE StudentID='" & Request.QueryString("substudentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ukm1_answer
                strSQL = "DELETE FROM UKM1_Answer WHERE StudentID='" & Request.QueryString("substudentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete UKM2
                strSQL = "DELETE FROM UKM2 WHERE StudentID='" & Request.QueryString("substudentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                lblMsg.Text += "DELETE SUB-ACCOUNT Maklumat Pelajar berjaya!" & strRet & "<br />"

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

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class