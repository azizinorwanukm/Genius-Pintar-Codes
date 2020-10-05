Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Web

Public Class Commonfunction

    Public Function isConfigEnable(ByVal strKey As String) As Boolean
        If getAppsettings(strKey) = "Y" Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function getAppsettings(ByVal strconfigCode As String) As String
        Dim strRet As String = ""
        Dim strSQL As String = ""

        strSQL = "SELECT configString FROM master_Config WHERE configCode='" & strconfigCode & "'"
        strRet = getFieldValue(strSQL)

        Return strRet
    End Function

    '--security
    Public Sub LogTrail(ByVal strTokenid As String, ByVal strLogTime As String, ByVal strUserHostAddress As String, ByVal strUserHostName As String, ByVal strUserBrowser As String, ByVal strActivity As String, ByVal strAuditDetail As String)
        Dim strSQL As String
        Dim strRet As String

        Try
            strSQL = "INSERT INTO security_login_trail (StudentID,LogTime,UserHostAddress,UserHostName,UserBrowser,Activity,AuditDetail) VALUES ('" & strTokenid & "','" & strLogTime & "','" & strUserHostAddress & "','" & strUserHostName & "','" & strUserBrowser & "','" & strActivity & "','" & strAuditDetail & "')"
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

    Function CheckSqlInjection(ByVal userValue As String) As Boolean
        ' Throw an exception if a blacklisted word is detected.
        Dim blackList As [String]() = {"alter", "begin", "cast", "create", "cursor", "declare",
         "delete", "drop", "exec", "execute", "fetch", "insert",
         "kill", "open", "select", "sys", "sysobjects", "syscolumns",
         "table", "update", "<script", "</script", "--", "/*",
         "*/", "@@", "@"}
        For i As Integer = 0 To blackList.Length - 1
            If userValue.ToLower().IndexOf(blackList(i)) <> -1 Then
                Return True
            End If
        Next

        Return False
    End Function

    ''padleft
    Function DoPadZeroLeft(ByVal strValue As String, ByVal nCount As Integer) As String
        Return strValue.PadLeft(nCount, "0")

    End Function

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

    ''--strip special char and space
    Public Function StringStrip(ByVal strStrip As String)
        Dim strorigFileName As String
        Dim intCounter As Integer
        Dim arrSpecialChar() As String = {".", ",", "<", ">", ":", "?", """", "/", "{", "[", "}", "]", "`", "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "|", " ", "\"}
        strorigFileName = strStrip
        intCounter = 0
        Dim i As Integer
        For i = 0 To arrSpecialChar.Length - 1
            Do Until intCounter = 29
                strStrip = Replace(strorigFileName, arrSpecialChar(i), "")
                intCounter = intCounter + 1
                strorigFileName = strStrip
            Loop
            intCounter = 0
        Next
        Return strorigFileName

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

    '--masuk YYYY-MM-DD keluar DD-MM-YYYY
    Function FormatDateDDMMYYYY(ByVal strDate As String) As String
        '--invalid length. return same string
        If strDate.Length < 10 Then
            Return strDate
        End If

        Dim strFormatDate As String = ""

        Dim strYear As String = ""
        Dim strMonth As String = ""
        Dim strDay As String = ""

        strYear = Mid(strDate, 1, 4)
        strMonth = Mid(strDate, 6, 2)
        strDay = Mid(strDate, 9, 2)

        strFormatDate = strDay & "-" & strMonth & "-" & strYear
        Return strFormatDate
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

    Function getDateTime() As String
        Dim time As DateTime = DateTime.Now
        Dim strToday As String = time.ToString("dd.MM.yyyy hh:mm:ss t.\M.")

        Return strToday
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

    Function getTodayHHMM() As String
        Return Now.ToString("yyyyMMdd_HHmm")

    End Function

    Function getRandom() As String
        Dim strTemp As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second

        Return strTemp
    End Function

    Function getRandomQuestion() As String
        ''--comment this since Dr Siti said many error on the question set 2009
        ''--2011B havent completed 2 feb 2011
        'Dim aRand As New Random
        'Dim nRand As Integer = aRand.Next(1, 3)

        'Select Case nRand
        '    Case 1
        '        Return "2011A"
        '    Case 2
        '        Return "2011B"
        '    Case Else
        '        Return "2011A"
        'End Select
        Return "2011A"

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
            strReturn += "<a target=_blank href='../cert_pdf/" + strFilename + "'>" + strFilename + "</a>"
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
        Dim smtp As New SmtpClient("mail.onlineapp.com.my", 587)
        smtp.Credentials = New NetworkCredential("mykadpro@onlineapp.com.my", "p@ssw0rd1")
        smtp.Send(mail)

    End Sub

    Sub sendmailHTML(ByVal mailfrom As String, ByVal mailto As String, ByVal mailsubject As String, ByVal mailbody As String)
        Dim message As New MailMessage(mailfrom, mailto)
        Dim SmtpClient As New SmtpClient("mail.onlineapp.com.my", 587)
        message.Subject = mailsubject
        message.Body = mailbody
        message.IsBodyHtml = True
        SmtpClient.Credentials = New NetworkCredential("mykadpro@onlineapp.com.my", "p@ssw0rd1")
        SmtpClient.Send(message)

    End Sub

    '--sum columns 
    Public Function SumColumn(ByVal mySQL As String) As Integer
        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")
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

        'If isBlockText(strSQL) = True Then
        '    ExecuteSQL = "*Security alert (Contact system admin): IP address and SQL command logged."
        '    Exit Function
        'End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim cmdSQL As New SqlCommand(strSQL, objConn)

        Try
            cmdSQL.Connection.Open()
            cmdSQL.ExecuteNonQuery()
            cmdSQL.Connection.Close()
        Catch ex As SqlException
            ExecuteSQL = "*System error (Contact system admin):" & Err.Description   '-- & "." & strSQL
            'do not exposed it to end user. hacker might used the info
        Finally
            'detach the SqlParameters from the command object, so they can be used again
            cmdSQL.Parameters.Clear()
            objConn.Dispose()
        End Try

    End Function

    Public Function isBlockText(ByVal strValue As String) As Boolean
        Dim myArray As Array
        myArray = Split("xp_;drop;alter;create;rename;delete;replace", ";")

        Dim myValue As Array
        myValue = Split(strValue, " ")

        Dim i As Integer
        For i = LBound(myArray) To UBound(myArray)
            Dim n As Integer
            For n = LBound(myValue) To UBound(myValue)
                If String.Compare(myArray(i), myValue(n), True) = 0 Then
                    Return True
                End If
            Next
        Next

        Return False
    End Function


    Public Function isExist(ByVal strSQL As String) As Boolean
        If strSQL.Length = 0 Then
            Return False
        End If
        ''If isBlockText(strSQL) = True Then
        ''    Return False
        ''End If

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

    Public Function isAnswered(ByVal strQ As String, ByVal strKey As String, ByVal strExamYear As String) As Boolean
        Dim strSQL As String = "SELECT StudentID FROM UKM1 WHERE StudentID='" & strKey & "' AND ExamYear='" & strExamYear & "' AND NOT(" & strQ & " IS NULL)"

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
            Return ""
        End If
        'If isBlockText(strSQL) = True Then
        '    getFieldValue = "*Security alert (Contact system admin): IP address and SQL command logged."
        '    Exit Function
        'End If

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strFieldValue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strFieldValue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return ""
                End If
            End If

        Catch ex As Exception
            Return ex.Message
        Finally
            objConn.Dispose()
        End Try

        Return strFieldValue
    End Function

    Public Function getFieldValueInt(ByVal strSQL As String) As Integer
        If strSQL.Length = 0 Then
            Return 0
        End If
        'If isBlockText(strSQL) = True Then
        '    getFieldValue = "*Security alert (Contact system admin): IP address and SQL command logged."
        '    Exit Function
        'End If

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strFieldValue As String = "0"
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strFieldValue = ds.Tables(0).Rows(0).Item(0).ToString
                    If strFieldValue.Length = 0 Then
                        strFieldValue = "0"
                    End If
                Else
                    Return 0
                End If
            End If

        Catch ex As Exception
            Return 0 '-- + ex.Message
        Finally
            objConn.Dispose()
        End Try

        Return CInt(strFieldValue)
    End Function

    Public Function getFieldValueReader(ByVal strSQL As String) As String
        Dim connectionString As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim queryString As String = strSQL
        getFieldValueReader = ""

        Using connection As New SqlConnection(connectionString)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = queryString
            command.CommandTimeout = 60 * 10    'in seconds
            Try
                connection.Open()

                Dim oDataReader As SqlDataReader = command.ExecuteReader()
                Do While oDataReader.Read()
                    getFieldValueReader = oDataReader(0).ToString
                Loop

                oDataReader.Close()
            Catch ex As Exception
                getFieldValueReader = ex.Message
            End Try

        End Using

    End Function


    Public Function getFieldValueEx(ByVal strSQL As String) As String
        If strSQL.Length = 0 Then
            Return ""
        End If
        'If isBlockText(strSQL) = True Then
        '    getFieldValueEx = "*Security alert (Contact system admin): IP address and SQL command logged."
        '    Exit Function
        'End If

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")
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
                        Return ""
                    End If
                Next
            End If

        Catch ex As Exception
            Return "*System error: " & ex.Message
        Finally
            objConn.Dispose()
        End Try

        Return strFieldValue
    End Function

    Public Function TransactionLog(ByVal SQLAction As String, ByVal strSQLStatement As String, ByVal strIPAddress As String) As String
        Dim strSQL As String
        Dim strRet As String

        Try
            strSQL = "INSERT INTO TransactionLog (SQLAction,SQLStatement,IPAddress,DateCreated) VALUES ('" & SQLAction & "','" & strSQLStatement & "','" & strIPAddress & "','" & Now.ToString & "')"
            strRet = ExecuteSQL(strSQL)
            Return strRet
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Function getGUID() As String
        Return System.Guid.NewGuid.ToString()

    End Function


    ''--string functions
    Function isMatching(ByVal strFind As String, ByVal strFullword As String) As Boolean
        '--works for english only!
        If System.Text.RegularExpressions.Regex.IsMatch(strFind, strFullword, RegexOptions.IgnoreCase) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Function Compare(ByVal strFind As String, ByVal strFullword As String) As Boolean
        Return strFullword.ToUpper.Contains(strFind.ToUpper)

    End Function

    Function Searchword(ByVal strFind As String, ByVal strFullword As String) As Boolean
        Dim nPos As Integer
        nPos = strFullword.ToUpper.IndexOf(strFind.ToUpper)
        If nPos > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Function RegexInStr(ByVal Start As Integer, ByVal String1 As String, ByVal String2 As String) As Integer
        If String1.Length < Start Then Return 0

        Dim intCharsToRemove As Integer = Start - 1
        Dim strToTest As String = Strings.Right(String1, String1.Length - intCharsToRemove)
        Dim intIndex As Integer = System.Text.RegularExpressions.Regex.Match(strToTest, String2).Index + 1

        If intIndex = 1 And Strings.Left(strToTest, String2.Length) <> String2 Then

        End If

        Return 0
        Return intIndex + intCharsToRemove
    End Function

End Class
