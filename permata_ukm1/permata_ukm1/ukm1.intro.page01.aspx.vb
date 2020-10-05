Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data.SqlClient

Partial Public Class ukm1_intro_page01
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim ci As CultureInfo

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
                Dim strBasename As String = "Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid"))
                ''--debug
                'Response.Write("strBasename:" & strBasename)

                rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
                ci = Thread.CurrentThread.CurrentCulture
                LoadStrings(ci)

                If oCommon.isDebug = True Then
                    getIsCount()
                End If
            End If

        Catch ex As Exception
            lblMsg.Text = "system error:" & ex.Message
        End Try
    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblintro000.Text = rm.GetString("lblintro000", ci)
        lblintro001.Text = rm.GetString("lblintro001", ci)
        lblintro002.Text = rm.GetString("lblintro002", ci)
        lblintro003.Text = rm.GetString("lblintro003", ci)
        lblintro004.Text = rm.GetString("lblintro004", ci)

        lblSijil.Text = "<p>**Sijil hanya akan dijana untuk pelajar yang mendapat markah melebihi had minimum yang telah ditetapkan untuk mendapat sijil Penyertaan Ujian UKM1.</p>Harap Maklum"

        btnNo.Text = rm.GetString("btnNo", ci)
        btnYes.Text = rm.GetString("btnYes", ci)

    End Sub

    Private Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Dim strNext As String = ""
        Dim strLast As String = ""
        Dim strQuestionYear As String = ""

        ''test DONE
        strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        If oCommon.getFieldValue(strSQL) = "DONE" Then
            Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
        End If

        strSQL = "SELECT StudentID FROM UKM1 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        If oCommon.isExist(strSQL) = False Then
            ''--new student taking ukm1

            ''--set examyear
            strQuestionYear = oCommon.getRandomQuestionYear()

            If UKM1_Insert(strQuestionYear) = True Then
                strNext = "ukm1.modA.page01.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid")
            Else
                '--do nothing. please report to admin. error message
                strNext = "ukm1.invalid.url.aspx"
                Exit Sub
            End If
        Else
            '--lastpage
            strSQL = "SELECT LastPage FROM UKM1 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            strLast = oCommon.getFieldValue(strSQL)
            If strLast.Length = 0 Then
                strLast = "ukm1.modA.page01.aspx?"
            End If

            '--QuestionYear
            strSQL = "SELECT QuestionYear FROM UKM1 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            strQuestionYear = oCommon.getFieldValue(strSQL)
            If strQuestionYear.Length = 0 Then
                strQuestionYear = Now.Year
            End If

            strNext = strLast & "&lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid")
        End If

        ''--debug
        'Response.Write(strNext)
        Response.Redirect(strNext)

    End Sub

    Private Function isDONE() As Boolean
        strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & oCommon.FixSingleQuotes(Request.QueryString("studentid")) & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        If strRet = "DONE" Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Response.Redirect("default.aspx")

    End Sub

    Private Function UKM1_Insert(ByVal strQuestionYear As String) As Boolean
        ''--get DOB_Year
        Dim strDOB_Year As String = ""
        strSQL = "SELECT DOB_Year FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strDOB_Year = oCommon.getFieldValue(strSQL)

        '--strStudentReligion
        Dim strStudentReligion As String = ""
        strSQL = "SELECT StudentReligion FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strStudentReligion = oCommon.getFieldValue(strSQL)

        ''got studentschool
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "' and IsLatest='Y'"
        strSchoolID = oCommon.getFieldValue(strSQL)

        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

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
                '--valid user?
                Dim nIsCount As Integer = getIsCount()

                ''INSERT UKM1
                Dim examYear As String = oCommon.getAppsettings("UKM1ExamYear")
                Dim examyear_id As Integer = Common.getExamYearID(examYear)

                strSQL = "INSERT INTO UKM1 (StudentID,ExamYear,QuestionYear,HostAddress,HostName,Browser,SelectedLang,Status,SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi,DOB_Year,StudentReligion,IsCount,examyear_id) OUTPUT INSERTED.ukm1id VALUES('" & Request.QueryString("studentid") & "','" & examYear & "','" & strQuestionYear & "','" & Request.UserHostAddress & "','" & Request.UserHostName & "','" & Request.UserAgent & "','" & Request.QueryString("lang") & "','NEW','" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "','" & strDOB_Year & "','" & strStudentReligion & "'," & nIsCount & ",'" & examyear_id & "')"
                Dim ukm1id As String = oCommon.getFieldValue(strSQL)
                'command.CommandText = strSQL
                'command.ExecuteNonQuery()

                Dim ukm1Table As String = Common.getUKM1Table(examYear)

                If Not ukm1Table = "UKM1" Then
                    strSQL = "INSERT INTO " & ukm1Table & " (UKM1ID, StudentID,ExamYear,QuestionYear,HostAddress,HostName,Browser,SelectedLang,Status,SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi,DOB_Year,StudentReligion,IsCount,examyear_id) VALUES('" & ukm1id & "','" & Request.QueryString("studentid") & "','" & oCommon.getAppsettings("UKM1ExamYear") & "','" & strQuestionYear & "','" & Request.UserHostAddress & "','" & Request.UserHostName & "','" & Request.UserAgent & "','" & Request.QueryString("lang") & "','NEW','" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "','" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "','" & strDOB_Year & "','" & strStudentReligion & "'," & nIsCount & ",'" & examyear_id & "')"
                    oCommon.ExecuteSQL(strSQL)
                    'command.CommandText = strSQL
                    'command.ExecuteNonQuery()
                End If

                ''INSERT UKM1_Answer
                strSQL = "INSERT INTO UKM1_Answer (StudentID,ExamYear) VALUES('" & Request.QueryString("studentid") & "','" & oCommon.getAppsettings("UKM1ExamYear") & "')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                lblMsg.Text += "INSERT completed! Ready to take UKM1."

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

        Return True
    End Function

    Private Function getIsCount() As Integer
        Dim strYear As String = ""
        '--DOB_YEAR. 8-15 tahun?
        strSQL = "SELECT DOB_Year FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        Dim nDOB_Year As Integer = oCommon.getFieldValueInt(strSQL)

        '--current year
        Dim strUKM1ExamYear As String = oCommon.getAppsettings("UKM1ExamYear")
        If strUKM1ExamYear.Length > 4 Then
            strYear = strUKM1ExamYear.Substring(1, 4)
        Else
            strYear = strUKM1ExamYear
        End If
        If oCommon.isDebug = True Then
            lbldebug.Text = "Year:" & strYear
        End If

        '--lebih 15 tahun
        If CInt(strYear) - nDOB_Year > 15 Then
            Return 0
        End If

        '--bawah dari 8 tahun
        If CInt(strYear) - nDOB_Year < 8 Then
            Return 0
        End If

        '--get StudentReligion
        strSQL = "SELECT StudentReligion FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        Dim strStudentReligion As String = oCommon.getFieldValue(strSQL)

        '-8 tahun dan tak ISLAM
        If CInt(strYear) - nDOB_Year = 8 And strStudentReligion <> "ISLAM" Then
            Return 0
        End If

        Return 1
    End Function

End Class