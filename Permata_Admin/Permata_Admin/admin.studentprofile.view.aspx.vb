Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class admin_studentprofile_view
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("pageid") = "admin.studentprofile.view.aspx"
            btnExecute.Attributes.Add("onclick", "return confirm('Pasti ingin meneruskan fungsi tersebut?');")
            btnPPCS.Attributes.Add("onclick", "return confirm('Pasti ingin meneruskan LAYAK/TIDAK LAYAK PPCS?');")
            If Not IsPostBack Then
                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

                '--load UKM2 menu base on usertype
                master_menu_list()

                '--PPCSStatus
                PPCSStatus_list()
                ddlPPCSStatus.SelectedValue = ""

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                '--load flag status

                loadFlagStatus()
                flagstatus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub StatusFunction()

        Try
            Dim data_active As String = ""


            strSQL = "SELECT flag FROM UKM2 WHERE StudentID ='" & Request.QueryString("studentid") & "'"
            data_active = oCommon.getFieldValue(strSQL)

            If data_active = "Y" Then
                flaging.SelectedIndex = 1
            ElseIf data_active = "N" Then
                flaging.SelectedIndex = 0
            End If


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub PPCSStatus_list()
        strSQL = "SELECT PPCSStatus FROM master_PPCSStatus ORDER BY PPCSStatus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSStatus.DataSource = ds
            ddlPPCSStatus.DataTextField = "PPCSStatus"
            ddlPPCSStatus.DataValueField = "PPCSStatus"
            ddlPPCSStatus.DataBind()

            ddlPPCSStatus.Items.Add(New ListItem("--SILA PILIH--", 0))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub loadFlagStatus()
        strSQL = "select * from UKM2 where UKM2ID is null"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            flaging.DataSource = ds
            flaging.DataTextField = "flag"
            flaging.DataValueField = "flag"
            flaging.DataBind()
            flaging.Items.Insert(0, New ListItem("--SILA PILIH--", String.Empty))
            flaging.Items.Insert(1, New ListItem("PROFILE SUDAH DITELITI", String.Empty))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcsdate_list()
        strSQL = "SELECT PPCSDate FROM master_PPCSDate ORDER BY ppcsid ASC"
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

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub master_menu_list()
        strSQL = "SELECT menudesc,menucode FROM master_menu WHERE menucategory='StudentProfile' AND usertype='" & getUserProfile_UserType() & "' ORDER BY menucode"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMenudesc.DataSource = ds
            ddlMenudesc.DataTextField = "menudesc"
            ddlMenudesc.DataValueField = "menucode"
            ddlMenudesc.DataBind()
            ddlMenudesc.Items.Insert(0, New ListItem("--Select--", "00"))
            ddlMenudesc.SelectedIndex = 0


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "ADMINOP"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case "UKM"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & oCommon.getAppsettings("DefaultExamYear") & "%'  ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
        End Select

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

            ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    '--- get flag status if exist
    Private Sub flagstatus()
        Dim data_active As String = ""

        Try

            strSQL = "SELECT flag FROM UKM2 WHERE StudentID ='" & Request.QueryString("studentid") & "'"
            data_active = oCommon.getFieldValue(strSQL)

            If data_active = "Y" Then
                flaging.SelectedIndex = 1
            ElseIf data_active = "N" Then
                flaging.SelectedIndex = 0
            End If

            StatusFunction()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub



    Private Function getUserProfile_UserType() As String
        strSQL = "Select UserType FROM UserProfile With (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function UKM2Layak(ByVal strStudentID As String) As Boolean
        '--get SchoolID
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
        strSchoolID = oCommon.getFieldValue(strSQL)

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
                '--set default value AdditionalMinute
                Dim strAdditionalMinute As String = oCommon.getAppsettings("SaringanDuration")

                '--initial status=SYSTEM

                strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,SchoolID,Status,AdditionalMinute) VALUES('" & strStudentID & "','" & ddlExamYear.Text & "','" & strSchoolID & "','NEW'," & strAdditionalMinute & ")"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                strSQL = "INSERT INTO UKM2_Answer (StudentID,ExamYear) VALUES('" & strStudentID & "','" & ddlExamYear.Text & "')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                If (ddlExamYear.Text = getUKM1Year) Then
                    strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET isLayak='Y' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET isLayak='Y' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                '--update UKM2 report information
                oCommon.UKM2_StudentprofileUpdate(strStudentID, ddlExamYear.Text)
                oCommon.UKM2_SchoolprofileUpdate(strStudentID, ddlExamYear.Text)

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

    Private Function UKM2TidakLayak(ByVal strStudentID As String) As Boolean
        ''--semak sama ada telah menamatkan ujian bagi tahun sebagaimana dlm web.config
        'strSQL = "SELECT StudentID FROM UKM2 WHERE Status='DONE' AND StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        'If oCommon.isExist(strSQL) = True Then
        '    lblMsg.Text = "Pelajar telah menamatkan Ujian UKM2 bagi tahun " & ddlExamYear.Text
        '    Return False
        'End If

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
                strSQL = "SELECT * FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                strRet = oCommon.Bulk_Transfer(strSQL, "UKM2_History")

                '--delete UKM2

                strSQL = "DELETE UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--UKM2_Answer
                strSQL = "DELETE UKM2_Answer WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--reset kelayakan
                If (ddlExamYear.Text = getUKM1Year) Then
                    strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET isLayak='N' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    command.CommandText = strSQL
                End If

                strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET isLayak='N' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
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

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT * FROM StudentProfile a, UKM1 b"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.StudentID='" & Request.QueryString("studentid") & "' AND b.ExamYear='" & ddlExamYear.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
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


    Protected Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecute.Click
        Dim data As String = ddlMenudesc.SelectedValue

        Select Case data
            Case "00"
                lblMsg.Text = "Please select functions to execute!"

            Case "01"   'UKM1 Reset ExamStart
                Execute_01()

            Case "02"   'UKM1 Delete
                Execute_02()

            Case "03"   'UKM1 Export
                Execute_03()

            Case "04"   'UKM2 Reset ExamStart
                Execute_04()

            Case "05"   'UKM2 Delete
                Execute_05()

            Case "06"   'UKM2 Layak
                Execute_06()

            Case "07"   'UKM2 Tidak Layak
                Execute_07()

            Case "08"   'UKM2 Hadir
                Execute_08()

            Case "09"   'UKM2 Tidak Hadir
                Execute_09()

            Case "10"   'UKM2 Logout
                Execute_10()

            Case "15"   'Delete Profile
                Execute_15()

            Case Else
                lblMsg.Text = "Please select functions to execute!"
        End Select

        Response.Redirect("admin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub

    '--UKM1 Reset ExamStart
    Private Sub Execute_01()
        lblMsg.Text = ""
        'LastPage=NULL,SelectedLang=NULL
        If (ddlExamYear.Text = getUKM1Year) Then
            strSQL = "UPDATE UKM1_" & getUKM1Year & " With (UPDLOCK) Set ExamStart= NULL,ExamEnd=NULL,Status='NEW',LastPage=NULL WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        strSQL = "UPDATE UKM1 With (UPDLOCK) Set ExamStart= NULL,ExamEnd=NULL,Status='NEW',LastPage=NULL WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        
        If strRet = "0" Then
            lblMsg.Text = "Berjaya reset Ujian UKM1 pelajar tersebut."
        Else
            lblMsg.Text = "Gagal reset Ujian UKM1 pelajar tersebut" & strRet
        End If

    End Sub

    'UKM1 Delete
    Private Sub Execute_02()
        lblMsg.Text = ""
        '--backup before delete
        strSQL = "SELECT * FROM UKM1 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_History")
        lblMsg.Text += "UKM1_History:" & strRet & vbCrLf

        '--DELETE
        If (ddlExamYear.Text = getUKM1Year) Then
            strSQL = "DELETE UKM1_" & getUKM1Year & " WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        strSQL = "DELETE UKM1 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text += "Berjaya DELETE Ujian UKM1 pelajar tersebut."
        Else
            lblMsg.Text += "Gagal DELETE Ujian UKM1 pelajar tersebut" & strRet
        End If

    End Sub

    'UKM1 Export
    Private Sub Execute_03()
        Try
            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    'UKM2 Reset ExamStart
    Private Sub Execute_04()
        lblMsg.Text = ""

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET ExamStart=NULL,ExamEnd=NULL,Status='NEW',LastPage=NULL,IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini ExamStart pelajar."
        Else
            lblMsg.Text = "Gagal mengemaskini ExamStart pelajar." & strRet
        End If

    End Sub

    'UKM2 Delete
    Private Sub Execute_05()
        Dim strStudentID As String = Request.QueryString("studentid")
        lblMsg.Text = ""
        ''get pusatcode
        strSQL = "SELECT PusatCode FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strPusatCode As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT TarikhUjian FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strTarikhUjian As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SessiUKM2 FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strSessiUKM2 As String = oCommon.getFieldValue(strSQL)

        '--get schoolid
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
        strSchoolID = oCommon.getFieldValue(strSQL)

        '--backup before delete
        strSQL = "SELECT * FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM2_History")
        lblMsg.Text += "UKM2_History:" & strRet & vbCrLf

        ''DELETE UKM2

        strSQL = "DELETE UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "DELETE NOK:" & strStudentID & strRet & vbCrLf
        End If

        '--set default value AdditionalMinute
        Dim strAdditionalMinute As String = oCommon.getAppsettings("SaringanDuration")

        ''INSERT BACK
        strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2,Status,AdditionalMinute) VALUES ('" & strStudentID & "','" & ddlExamYear.Text & "','Y','" & strPusatCode & "','" & strSchoolID & "','" & strTarikhUjian & "','" & strSessiUKM2 & "','NEW'," & strAdditionalMinute & ")"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "INSERT NOK:" & strStudentID & strRet & vbCrLf
        End If

        '--update UKM2 report information
        oCommon.UKM2_StudentprofileUpdate(strStudentID, ddlExamYear.Text)
        oCommon.UKM2_SchoolprofileUpdate(strStudentID, ddlExamYear.Text)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya Reset Ujian UKM2 pelajar tersebut."
        End If
    End Sub

    'UKM2 Layak
    Private Sub Execute_06()
        If UKM2Layak(Request.QueryString("studentid")) = True Then
            lblMsg.Text = "Pelajar LAYAK untuk menduduki Ujian UKM2."
        End If

    End Sub

    'UKM2 Tidak Layak
    Private Sub Execute_07()
        If UKM2TidakLayak(Request.QueryString("studentid")) = True Then
            lblMsg.Text = "Pelajar TIDAK LAYAK untuk menduduki Ujian UKM2."
        End If

    End Sub

    'UKM2 Hadir
    Private Sub Execute_08()

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    'UKM2 Tidak Hadir
    Private Sub Execute_09()

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='N' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    'UKM2 Logout
    Private Sub Execute_10()

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='Y',IsLogin='N' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini UKM2 pelajar. IsHadir='Y',IsLogin='N'"
        Else
            lblMsg.Text = "Gagal mengemaskini UKM2 pelajar." & strRet
        End If

    End Sub

    'Delete Profile
    Private Sub Execute_15()
        lblMsg.Text = ""
        ''--backup 
        strSQL = "SELECT * FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentProfile_History")
        lblMsg.Text = "Backup StudentProfile_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM StudentPhoto WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentPhoto_History")
        lblMsg.Text += "Backup StudentPhoto_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM ParentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "ParentProfile_History")
        lblMsg.Text += "Backup ParentProfile_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentSchool_History")
        lblMsg.Text += "Backup StudentSchool_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM UKM1 WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_History")
        lblMsg.Text += "Backup UKM1_History:" & strRet & "<br />"

        ''--backup 
        strSQL = "SELECT * FROM UKM1_Answer WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM1_Answer_History")
        lblMsg.Text += "Backup UKM1_Answer_History:" & strRet & "<br />"

        ''--backup UKM2
        strSQL = "SELECT * FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM2_History")
        lblMsg.Text += "Backup UKM2_History:" & strRet & "<br />"

        ''--backup UKM3
        strSQL = "SELECT * FROM UKM3 WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "UKM3_History")
        lblMsg.Text += "Backup UKM3_History:" & strRet & "<br />"

        '--backup PPCS
        strSQL = "SELECT * FROM PPCS WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "PPCS_History")
        lblMsg.Text += "Backup PPCS_History:" & strRet & "<br />"

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
                strSQL = "DELETE FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete studentphoto
                strSQL = "DELETE FROM StudentPhoto WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ParentProfile
                strSQL = "DELETE FROM ParentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete studentschool
                strSQL = "DELETE FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ukm1

                strSQL = "DELETE FROM UKM1_" & getUKM1Year & " WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                strSQL = "DELETE FROM UKM1 WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete ukm1_answer
                strSQL = "DELETE FROM UKM1_Answer WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete UKM2

                strSQL = "DELETE FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete UKM2_Answer
                strSQL = "DELETE FROM UKM2_Answer WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete UKM3
                strSQL = "DELETE FROM UKM3 WHERE StudentID='" & Request.QueryString("studentid") & "'"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''delete PPCS
                strSQL = "DELETE FROM PPCS WHERE StudentID='" & Request.QueryString("studentid") & "'"
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

    Protected Sub btnPPCS_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPPCS.Click
        'Select Case ddlPPCSStatus.Text
        '    Case "LAYAK"
        '        If PPCSLayak(Request.QueryString("studentid")) = True Then
        '            lblMsg.Text = "Berjaya MELAYAKKAN pelajar ke PPCS."
        '        End If

        '    Case "TIDAK LAYAK"
        '        If PPCSTidakLayak(Request.QueryString("studentid")) = True Then
        '            lblMsg.Text = "Berjaya mengeluarkan KELAYAKAN pelajar daripada PPCS."
        '        End If

        '    Case Else
        '        lblMsg.Text = "Invalid function!"
        'End Select

        PPCS_Update()
        ''reload page
        Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)


    End Sub

    Private Sub PPCS_Update()
        Dim i As Integer = 0
        lblMsg.Text = ""

        '-validate
        If ddlPPCSStatus.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih Status PPCS untuk diKEMASKINI."
            Exit Sub
        End If

        Select Case ddlPPCSStatus.Text
            Case "TIDAK LAYAK"
                Dim strID As String = Request.QueryString("studentid")
                If setTidakLayak(strID) = True Then
                    lblMsg.Text = "Berjaya reset kelayakan pelajar kepada TIDAK LAYAK."
                Else
                    lblMsg.Text += "NOK:" & strID & vbCrLf
                End If

            Case Else
                Dim strID As String = Request.QueryString("studentid")
                If setPPCSStatus(strID, ddlPPCSStatus.Text) = True Then
                    lblMsg.Text = "Berjaya mengemaskini status pelajar."
                Else
                    lblMsg.Text += "NOK:" & strID & vbCrLf
                End If

        End Select

    End Sub

    Private Function setPPCSStatus(ByVal strStudentID As String, ByVal strPPCSStatus As String) As Boolean
        ''--check duplicate entry. no duplicate studentid and examyear
        strSQL = "SELECT StudentID FROM PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            ''--UPDATE
            '--DR Siti request not to update statustawaran. Only students can update it. Default blank/null
            strSQL = "UPDATE PPCS WITH (UPDLOCK) SET PPCSStatus='" & strPPCSStatus & "' WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        Else
            ''--INSERT
            strSQL = "INSERT INTO PPCS (StudentID,PPCSDate,PPCSStatus,StatusTawaran) VALUES('" & strStudentID & "','" & ddlPPCSDate.Text & "','" & strPPCSStatus & "',NULL)"
        End If
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True
    End Function


    Private Function setTidakLayak(ByVal strStudentID As String) As Boolean

        ''--insert UKM2
        strSQL = "DELETE PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function


    Private Function PPCSLayak(ByVal strStudentID As String) As Boolean
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
            command.CommandTimeout = 120

            Try
                '--1 PPCS insert if not exist
                strSQL = "SELECT StudentID FROM PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                If oCommon.isExist(strSQL) = False Then
                    '--INSERT
                    strSQL = "INSERT INTO PPCS (StudentID,PPCSDate,PPCSStatus,StatusTawaran) VALUES('" & strStudentID & "','" & ddlPPCSDate.Text & "','LAYAK',NULL)"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                ''--get DOB_Year
                Dim strDOB_Year As String = ""
                strSQL = "SELECT DOB_Year FROM StudentProfile WHERE StudentID='" & strStudentID & "'"
                strDOB_Year = oCommon.getFieldValue(strSQL)

                '--2 UKM1 
                strSQL = "SELECT StudentID FROM UKM1 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                If oCommon.isExist(strSQL) = False Then
                    '--INSERT

                    Dim examyear_id As String = oCommon.getFieldValue("select examyearid from master_examyear where ExamYear = '" & ddlExamYear.Text & "'")

                    If (ddlExamYear.Text = getUKM1Year) Then
                        strSQL = "INSERT INTO UKM1_" & getUKM1Year & " (StudentID,ExamYear,QuestionYear,HostAddress,HostName,Browser,SelectedLang,Status,DOB_Year,examyear_id) VALUES('" & strStudentID & "','" & ddlExamYear.Text & "','" & ddlExamYear.Text & "','" & Request.UserHostAddress & "','" & Request.UserHostName & "','" & Request.UserAgent & "','ms-MY','NEW','" & strDOB_Year & "','" & examyear_id & "')"
                        command.CommandText = strSQL
                        command.ExecuteNonQuery()
                    End If

                    strSQL = "INSERT INTO UKM1 (StudentID,ExamYear,QuestionYear,HostAddress,HostName,Browser,SelectedLang,Status,DOB_Year,examyear_id) VALUES('" & strStudentID & "','" & ddlExamYear.Text & "','" & ddlExamYear.Text & "','" & Request.UserHostAddress & "','" & Request.UserHostName & "','" & Request.UserAgent & "','ms-MY','NEW','" & strDOB_Year & "','" & examyear_id & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()

                    strSQL = "INSERT INTO UKM1_Answer (StudentID,ExamYear) VALUES('" & strStudentID & "','" & ddlExamYear.Text & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                '--3 UKM2
                strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                If oCommon.isExist(strSQL) = False Then
                    '--set default value AdditionalMinute
                    Dim strAdditionalMinute As String = oCommon.getAppsettings("SaringanDuration")

                    ''INSERT UKM2

                    strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2,Status,AdditionalMinute) VALUES ('" & strStudentID & "','" & ddlExamYear.Text & "','Y','NA','NA','NA','NA','NEW'," & strAdditionalMinute & ")"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()

                    strSQL = "INSERT INTO UKM2_Answer (StudentID,ExamYear) VALUES('" & strStudentID & "','" & ddlExamYear.Text & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                Return True
            Catch ex As Exception
                'Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
                'Console.WriteLine("  Message: {0}", ex.Message)
                lblMsg.Text = "Error:" & ex.Message

                ' Attempt to roll back the transaction. 
                Try
                    transaction.Rollback()

                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    'Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    'Console.WriteLine("  Message: {0}", ex2.Message)

                    lblMsg.Text = "Rollback:" & ex2.Message
                    Return False
                End Try
            End Try
        End Using

    End Function

    Private Function PPCSTidakLayak(ByVal strStudentID As String) As Boolean

        ''--delete PPCS sahaja. UKM1 dan UKM2 maintain
        strSQL = "DELETE PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function

    Protected Sub Simpanflag_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Simpanflag.Click

        If (ddlExamYear.Text = getUKM1Year) Then
            strSQL = "UPDATE UKM1_" & getUKM1Year & " SET flag='Y' WHERE StudentID ='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "' "
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        strSQL = "UPDATE UKM2 SET flag='Y' WHERE StudentID ='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "' "
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "UPDATE UKM1 SET flag='Y' WHERE StudentID ='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "' "
        strRet = oCommon.ExecuteSQL(strSQL)

        Response.Redirect("admin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub


End Class