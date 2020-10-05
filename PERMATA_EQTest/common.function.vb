Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net
Imports System.Net.Mime
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.ComponentModel


Public Class Commonfunction

    Public Function getAppsettings(ByVal strconfigCode As String) As String
        Dim strRet As String = ""
        Dim strSQL As String = ""

        strSQL = "SELECT configString FROM master_Config WHERE configCode='PPCSEND'"
        strRet = getFieldValue(strSQL)

        Return strRet
    End Function

    '--disable all buttons on a page
    '--call SetControls(Page, False)
    Private Sub SetControls(ByVal parentControl As Control, Optional ByVal enable As Boolean = False)
        For Each c As Control In parentControl.Controls
            If TypeOf (c) Is CheckBox Then
                CType(c, CheckBox).Enabled = enable
            ElseIf TypeOf (c) Is Button Then
                CType(c, Button).Enabled = enable
            ElseIf TypeOf (c) Is RadioButtonList Then
                CType(c, RadioButtonList).Enabled = enable
            End If

            SetControls(c)
        Next

    End Sub

    Public Function RandomNumber(ByVal MaxNumber As Integer, Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        Dim r As New Random(System.DateTime.Now.Millisecond)

        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)

    End Function


    '--security
    Public Sub LogTrail(ByVal strTokenid As String, ByVal strLogTime As String, ByVal strUserHostAddress As String, ByVal strUserHostName As String, ByVal strUserBrowser As String, ByVal strActivity As String)
        Dim strSQL As String
        Dim strRet As String

        Try
            strSQL = "INSERT INTO security_login_trail (Tokenid,LogTime,UserHostAddress,UserHostName,UserBrowser,Activity) VALUES ('" & strTokenid & "','" & strLogTime & "','" & strUserHostAddress & "','" & strUserHostName & "','" & strUserBrowser & "','" & strActivity & "')"
            strRet = ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                'lblMsg.Text = strRet
            End If
        Catch ex As Exception

        End Try

    End Sub

    '--
    Sub WriteLogFile(ByVal strPath As String, ByVal strError As String)
        Dim File As System.IO.StreamWriter
        Dim strReturn As String = ""
        Dim rowscreated As Integer = 0
        Dim sqlinsert As String = ""

        Try
            '--open append
            File = New System.IO.StreamWriter(strPath, True)

            File.WriteLine(strError)

            File.Close()
            File = Nothing
        Catch ae As SqlException

        Finally

        End Try

    End Sub


    '---
    Function DoConvertC(ByVal Str As String, ByVal DecPlc As Integer) As String
        Return String.Format("{0:c" & DecPlc & "}", CDec(Str))

    End Function

    '--decimal places for number
    Function DoConvertN(ByVal Str As String, ByVal DecPlc As Integer) As String
        Return String.Format("{0:n" & DecPlc & "}", CDec(Str))

    End Function

    Function IsTextValidated(ByVal strTextEntry As String) As Boolean
        Dim objNotWholePattern As New Regex("[^0-9]")
        Return (Not objNotWholePattern.IsMatch(strTextEntry))

    End Function

    Function isNumeric(ByVal strTextEntry As String) As Boolean
        Dim objNotWholePattern As New Regex("[^0-9]")
        Return (Not objNotWholePattern.IsMatch(strTextEntry))

    End Function

    Function IsCurrency(ByVal value As String) As Boolean
        Dim dummy As Decimal
        Return ([Decimal].TryParse(value, NumberStyles.Currency, CultureInfo.CurrentCulture, dummy))

    End Function

    Function gettxnref()
        Return Now.ToString("yyyyMMdd") & Now.Minute & Now.Second & Now.Millisecond

    End Function

    Function isEmail(ByVal inputEmail As String) As Boolean

        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(inputEmail, pattern)
        If emailAddressMatch.Success Then
            isEmail = True
        Else
            isEmail = False
        End If

    End Function

    Function FormatDateDMY(ByVal strDate As Date) As String
        Dim ddate As Date
        ddate = strDate
        Dim dd, mm, yy As String
        dd = Day(ddate).ToString
        If dd.Length = 1 Then
            dd = "0" & dd
        End If
        mm = Month(ddate).ToString
        If mm.Length = 1 Then
            mm = "0" & mm
        End If
        yy = Year(ddate).ToString

        FormatDateDMY = dd & "/" & mm & "/" & yy
    End Function

    'yyyy'-'MM'-'dd'T'HH': 'mm': 'ss.fffffff'Z' —For UCT values
    'yyyy'-'MM'-'dd'T'HH': 'mm': 'ss.fffffff'zzz' —For local values
    'yyyy'-'MM'-'dd'T'HH': 'mm': 'ss.fffffff' —For abstract time values
    Function getNow() As String
        Return Now.ToString("yyyyMMdd HH:mm:ss.fff")

    End Function

    Function getToday() As String
        Return Now.ToString("yyyyMMdd")

    End Function

    Function getRandom() As String
        Dim strTemp As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second

        Return strTemp
    End Function

    Function getRandomQuestion() As String
        '--comment this since Dr Siti said many error on the question set 2009
        'Dim aRand As New Random
        'Dim nRand As Integer = aRand.Next(1, 3)

        'Select Case nRand
        '    Case 1
        '        Return "2009"
        '    Case 2
        '        Return "2010"
        '    Case Else
        '        Return "2010"
        'End Select
        Return "2010"

    End Function

    Function FixSingleQuotes(ByVal strValue As String) As String
        '--fix complete sql injection
        Dim intLevel As Integer = 2

        Try
            If Not IsDBNull(strValue) Then
                If intLevel > 0 Then
                    strValue = Replace(strValue, "'", "''") ' Most important one! This line alone can prevent most injection attacks
                    strValue = Replace(strValue, "--", "")
                    strValue = Replace(strValue, "[", "[[]")
                    strValue = Replace(strValue, "%", "[%]")
                End If

                If intLevel > 1 Then
                    Dim myArray As Array
                    myArray = Split("xp_ ;update ;insert ;select ;drop ;alter ;create ;rename ;delete ;replace ", ";")
                    Dim i, i2, intLenghtLeft As Integer
                    For i = LBound(myArray) To UBound(myArray)
                        Dim rx As New Regex(myArray(i), RegexOptions.Compiled Or RegexOptions.IgnoreCase)
                        Dim matches As MatchCollection = rx.Matches(strValue)
                        i2 = 0
                        For Each match As Match In matches
                            Dim groups As GroupCollection = match.Groups
                            intLenghtLeft = groups.Item(0).Index + Len(myArray(i)) + i2
                            strValue = Left(strValue, intLenghtLeft - 1) & "&nbsp;" & Right(strValue, Len(strValue) - intLenghtLeft)
                            i2 += 5
                        Next
                    Next
                End If

                'strValue = replace(strValue, ";", ";&nbsp;")
                'strValue = replace(strValue, "_", "[_]")

                Return strValue
            Else
                Return strValue
            End If
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Function CToString(ByVal strString As String)
        Dim strTemp As String
        strTemp = strString
        CToString = strTemp

    End Function

    Function ReplaceComa(ByVal strString As String)
        Dim intIndex
        Dim strTemp As String = ""
        intIndex = InStr(strString, ",")
        If intIndex > 0 Then
            strTemp = strString
            strTemp.Replace(",", ".")
        End If
        ReplaceComa = strTemp

    End Function

    Function FixComa(ByVal strString As String)
        Dim intIndex
        Dim strTemp As String
        intIndex = InStr(strString, ",")
        If intIndex > 0 Then
            strTemp = """" & strString & """"
        Else
            strTemp = strString
        End If
        FixComa = strTemp

    End Function


    Function ChkTime(ByVal strTokenID As String, ByVal strTestID As String) As Boolean
        '--comment for launching purpose only

        'strSQL = "SELECT * FROM RespondentAnswer WHERE TokenID='" & strTokenID & "' AND TestID='" & strTestID & "'"
        'Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        'Dim objConn As SqlConnection = New SqlConnection(strConn)
        'Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        'Dim Dur As String
        'Dim st, et As DateTime

        'Try
        '    Dim ds As DataSet = New DataSet
        '    sqlDA.Fill(ds, "AnyTable")

        '    Dim MyTable As DataTable = New DataTable
        '    MyTable = ds.Tables(0)
        '    If MyTable.Rows.Count > 0 Then
        '        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ExamStart")) Then
        '            st = ds.Tables(0).Rows(0).Item("ExamStart")
        '        Else
        '            st = Now.ToString
        '        End If
        '        If Not IsDBNull(ds.Tables(0).Rows(0).Item("ExamEnd")) Then
        '            et = ds.Tables(0).Rows(0).Item("ExamEnd")
        '        Else
        '            et = Now.ToString
        '        End If
        '        Dur = et.Subtract(st).Duration.TotalSeconds.ToString
        '        Dur = CInt(Dur)

        '        If Dur < 5400 Then
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Else
        '        Return True
        '    End If

        'Catch ex As Exception
        '    Return True
        'Finally
        '    objConn.Close()
        'End Try

        Return True

    End Function

    Function StartTimer(ByVal strSQL As String)
        Dim dtStartDate As Date = Now

        '--update into database starttime
        Return True

    End Function

    Function EndTimer(ByVal strSQL As String)
        Dim dtEndDate As Date = Now

        '--update into database endtime

        Return True
    End Function

    Function ComputeTime()

        Return True
    End Function

    Function LogEventDB(ByVal myEvent As String, ByVal FileID As String,
    ByVal FileName As String, ByVal FolderName As String, ByVal FolDir As String, ByVal History As String,
    ByVal UserID As String, ByVal LoginID As String) As String

        Dim strSQL As String
        strSQL = "INSERT INTO mLog (myEvent,FileID,FileName,FolderName,FolDir,History,UserID,LoginID) VALUES ('" & myEvent & "'," & FileID & ",'" & FileName.Replace("'", "") & "','" & FolderName.Replace("'", "") & "','" & FolDir.Replace("'", "") & "','" & History.Replace("'", "") & "'," & UserID & ",'" & LoginID & "')"
        LogEventDB = ExecuteSQL(strSQL)

    End Function


    Function WriteInExportedFile(ByVal strPath As String, ByVal tableColumns As DataColumnCollection, ByVal tableRows As DataRowCollection) As String
        Dim File As System.IO.StreamWriter

        Dim strReturn As String = ""
        Dim rowscreated As Integer = 0
        Dim sqlinsert As String = ""

        Try
            File = New System.IO.StreamWriter(strPath)

            'Loop through columns of table to generate first row of CSV file
            Dim ctrColumn As Integer = 0
            Dim dc As DataColumn
            For Each dc In tableColumns
                If (ctrColumn < tableColumns.Count - 1) Then
                    sqlinsert += dc.ColumnName.ToString() + ","
                Else
                    sqlinsert += dc.ColumnName.ToString()
                End If

                ctrColumn = ctrColumn + 1
            Next
            File.WriteLine(sqlinsert)

            Dim row As DataRow
            For Each row In tableRows
                sqlinsert = ""
                Dim sqlvalues As String = ""
                Dim rowItems() As Object = row.ItemArray

                ctrColumn = 0
                Dim dcol As DataColumn
                For Each dcol In tableColumns
                    If (ctrColumn < tableColumns.Count - 1) Then
                        sqlvalues += """" + rowItems(ctrColumn).ToString().Replace(" ''", "'") + """" + ","
                    Else
                        sqlvalues += """" + rowItems(ctrColumn).ToString().Replace(" ''", "'") + """"
                    End If

                    ctrColumn = ctrColumn + 1
                Next

                sqlinsert = sqlinsert + sqlvalues
                File.WriteLine(sqlinsert)

                rowscreated = rowscreated + 1
            Next
            strReturn = "Records Exported Successfully!<br>"
            strReturn += rowscreated.ToString()
            strReturn += " rows created in CSV file "

            Dim intFileNameLength = InStr(1, StrReverse(strPath), "\")
            Dim strFilename As String = Mid(strPath, (Len(strPath) - intFileNameLength) + 2)
            strReturn += "<a target=_blank href='result/" + strFilename + "'>" + strFilename + "</a>"
            File.Close()
            File = Nothing
        Catch ae As SqlException
            strReturn = "Error at Record Number: "
            strReturn += rowscreated.ToString()
            strReturn += "<br>Message: " + ae.Message.ToString() + "<br>"
            strReturn += "Error importing. Please try again"
        Finally

        End Try

        Return strReturn
    End Function

    Function strClean(ByVal strtoclean As String) As String
        '--special '
        strtoclean = strtoclean.Replace("'", "-")

        Dim outputStr As String
        Dim rgPattern = "[(?*"",\\<>&#~%{}+@:\/!;]+$^():~`"
        Dim objRegExp As New Regex(rgPattern)

        outputStr = objRegExp.Replace(strtoclean, "")

        Return outputStr
    End Function

    Function filterFilename(ByVal strFilename As String) As String
        '--Replace invalid file name characters \ /:*?"<>|
        strFilename = strFilename.Replace("'", "")
        strFilename = strFilename.Replace(":", "")
        strFilename = strFilename.Replace("*", "")
        strFilename = strFilename.Replace("?", "")
        strFilename = strFilename.Replace("<", "")
        strFilename = strFilename.Replace(">", "")
        strFilename = strFilename.Replace("|", "")
        strFilename = strFilename.Replace("/", "")
        strFilename = strFilename.Replace("\\", "")
        strFilename = strFilename.Replace("\", "")

        Return strFilename

    End Function

    Sub sendmail(ByVal mailfrom As String, ByVal mailto As String, ByVal mailsubject As String, ByVal mailbody As String)

        'create the mail message
        Dim mail As New MailMessage()
        '--Dim MyAttachment As Attachment = New Attachment(strFileAttach_mykad)

        'set the addresses
        mail.From = New MailAddress(mailfrom)
        mail.To.Add(mailto)

        'set the content
        mail.Subject = mailsubject
        '--mail.Attachments.Add(MyAttachment)
        mail.Body = mailbody
        'mail.IsBodyHtml = True

        'send the message
        Dim smtp As New SmtpClient("mail.arakenonline.biz", 587)
        smtp.Credentials = New NetworkCredential("support@arakenonline.biz", "p@ssw0rd1")
        smtp.Send(mail)

    End Sub

    Sub sendmailHTML(ByVal mailfrom As String, ByVal mailto As String, ByVal mailsubject As String, ByVal mailbody As String)
        Dim message As New MailMessage(mailfrom, mailto)
        Dim SmtpClient As New SmtpClient("mail.arakenonline.biz", 587)
        message.Subject = mailsubject
        message.Body = mailbody
        message.IsBodyHtml = True
        SmtpClient.Credentials = New NetworkCredential("support@arakenonline.biz", "p@ssw0rd1")
        SmtpClient.Send(message)

    End Sub

    '--sum columns 
    Public Function SumColumn(ByVal mySQL As String) As Integer
        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(mySQL, objConn)
        Dim ds As DataSet = New DataSet

        Dim nCol As Integer = 0
        Dim strTemp As String = ""
        Dim nTemp As Integer = 0

        Try
            sqlDA.Fill(ds, "AnyTable")
            If ds.Tables(0).Rows.Count > 0 Then
                While nCol < ds.Tables(0).Columns.Count
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item(nCol)) Then
                        strTemp = ds.Tables(0).Rows(0).Item(nCol)
                    Else
                        strTemp = "0"
                    End If

                    nTemp += CInt(strTemp)
                    nCol += 1
                End While
            End If
        Catch ex As Exception
            Return 0
        Finally
            ds.Dispose()
            sqlDA.Dispose()
            objConn.Dispose()
        End Try

        Return nTemp
    End Function

    Public Function ExecuteSQL(ByVal strSQL As String) As String
        ExecuteSQL = "0"

        If strSQL.Length = 0 Then
            ExecuteSQL = "*System error (Contact system admin): No query string pass."
            Exit Function
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim cmdSQL As New SqlCommand(strSQL, objConn)

        Try
            cmdSQL.Connection.Open()
            cmdSQL.ExecuteNonQuery()
            cmdSQL.Connection.Close()
        Catch ex As SqlException
            ExecuteSQL = "*System error (Contact system admin)" & Err.Description & "." & strSQL
            'do not exposed it to end user. hacker might used the info
        Finally
            'detach the SqlParameters from the command object, so they can be used again
            cmdSQL.Parameters.Clear()
            objConn.Dispose()
        End Try

    End Function

    Public Function isExist(ByVal strSQL As String) As Boolean
        If strSQL.Length = 0 Then
            Return False
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Public Function isAnswered(ByVal strQ As String, ByVal strKey As String) As Boolean
        Dim strSQL As String = "SELECT Tokenid FROM ukm1_respondent_mark WHERE TokenID='" & strKey & "' AND TestID='2010' AND NOT(" & strQ & " IS NULL)"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Public Function getCount(ByVal strSQL As String) As Integer
        If strSQL.Length = 0 Then
            Return 0
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            getCount = ds.Tables(0).Rows.Count
        Catch ex As Exception
            Return 0
        Finally
            objConn.Dispose()
        End Try

    End Function

    Public Function getFieldValue(ByVal strSQL As String) As String
        If strSQL.Length = 0 Then
            Return "0"
        End If

        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strFieldValue As String = "0"
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strFieldValue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If

            '--send number 0 back
            If strFieldValue = "" Then
                strFieldValue = "0"
            End If

        Catch ex As Exception
            Return "*System error (Contact system admin): " '-- + ex.Message
        Finally
            objConn.Dispose()
        End Try

        Return strFieldValue
    End Function


    Public Function getFieldValueEx(ByVal strSQL As String) As String
        If strSQL.Length = 0 Then
            Return "0|"
        End If

        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strFieldValue As String = ""
        Dim i As Integer
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Columns.Count - 1
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                        strFieldValue += ds.Tables(0).Rows(0).Item(i).ToString & "|"
                    Else
                        Return "0|"
                    End If
                Next
            End If

        Catch ex As Exception
            Return "*System error (Contact system admin): "  '--+ ex.Message
        Finally
            objConn.Dispose()
        End Try

        Return strFieldValue
    End Function

    Function getGUID() As String
        Return System.Guid.NewGuid.ToString()

    End Function

    ''--send mail ASYNC
    Public Sub SendAsync()
        'create the mail message
        Dim mail As New MailMessage()

        'set the addresses
        mail.From = New MailAddress("me@mycompany.com")
        mail.To.Add("you@yourcompany.com")

        'set the content
        mail.Subject = "This is an email"
        mail.Body = "this is the body content of the email."

        'send the message
        Dim smtp As New SmtpClient("127.0.0.1") 'specify the mail server address
        'the userstate can be any object. The object can be accessed in the callback method
        'in this example, we will just use the MailMessage object.
        Dim userState As Object = mail

        'wire up the event for when the Async send is completed
        AddHandler smtp.SendCompleted, AddressOf SmtpClient_OnCompleted

        smtp.SendAsync(mail, userState)
    End Sub 'SendAsync

    Public Sub SmtpClient_OnCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
        'Get the Original MailMessage object
        Dim mail As MailMessage = CType(e.UserState, MailMessage)

        'write out the subject
        Dim subject As String = mail.Subject

        If e.Cancelled Then
            Console.WriteLine("Send canceled for mail with subject [{0}].", subject)
        End If
        If Not (e.Error Is Nothing) Then
            Console.WriteLine("Error {1} occurred when sending mail [{0}] ", subject, e.Error.ToString())
        Else
            Console.WriteLine("Message [{0}] sent.", subject)
        End If
    End Sub 'SmtpClient_OnCompleted


End Class
