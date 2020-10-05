
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports System.Text.RegularExpressions

Module modFunction

    Public conStr As String = WebConfigurationManager.AppSettings("ConnectionString").ToString
    Public sessionTimeout As String = WebConfigurationManager.AppSettings("SessionTimeOut").ToString

    Sub LogError(Method As String, ErrDesc As String)


        Using Con As SqlConnection = New SqlConnection(conStr)

            Con.Open()

            Using Com As SqlCommand = New SqlCommand("spErrorLog", Con)

                Com.CommandType = CommandType.StoredProcedure
                Com.Parameters.AddWithValue("operation", "add")
                Com.Parameters.AddWithValue("errordesc", ErrDesc)
                Com.Parameters.AddWithValue("method", Method)
                Com.ExecuteNonQuery()

            End Using

            Con.Close()

        End Using

    End Sub

    Sub AddAnswer(Userid As String, ExamId As String, AssistantName As String, AssistantPhoneNo As String, ModuleNo As String, QuestionNo As String, Answer As String, Score As String)

        Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

            Con.Open()

            Using Com As SqlCommand = New SqlCommand("spExamLog", Con)

                Com.CommandType = CommandType.StoredProcedure
                Com.Parameters.AddWithValue("operation", "update")
                Com.Parameters.AddWithValue("examid", ExamId)
                Com.Parameters.AddWithValue("moduleno", ModuleNo)
                Com.Parameters.AddWithValue("questionno", QuestionNo)
                Com.Parameters.AddWithValue("answer", Answer)
                Com.Parameters.AddWithValue("mark", Score)
                Com.ExecuteNonQuery()

            End Using

            Con.Close()

        End Using


    End Sub

    Sub UpdateLastState(UserId As String, ExamId As String, PageName As String, TimeLeft As Integer, ZeroMarkCount As Integer)

        Try
            Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

                Con.Open()

                Using Com As SqlCommand = New SqlCommand("spExam", Con)

                    Com.CommandType = CommandType.StoredProcedure
                    Com.Parameters.AddWithValue("operation", "update")
                    Com.Parameters.AddWithValue("id", ExamId)
                    Com.Parameters.AddWithValue("userid", UserId)
                    Com.Parameters.AddWithValue("lastpage", PageName)
                    Com.Parameters.AddWithValue("timeleft", TimeLeft)
                    Com.Parameters.AddWithValue("zeromarkcount", ZeroMarkCount)
                    Com.ExecuteNonQuery()

                End Using

                Con.Close()

            End Using

        Catch ex As Exception
            LogError("modfunction.vb UpdateLastState:", ex.Message + " " + ex.StackTrace)

        End Try


    End Sub

    Sub UpdateEndTime(UserId As String, ExamId As String)

        Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

            Con.Open()

            Using Com As SqlCommand = New SqlCommand("spExam", Con)

                Com.CommandType = CommandType.StoredProcedure
                Com.Parameters.AddWithValue("operation", "end")
                Com.Parameters.AddWithValue("id", ExamId)
                Com.Parameters.AddWithValue("userid", UserId)

                Com.ExecuteNonQuery()

            End Using

            Con.Close()

        End Using


    End Sub

    Function IsCurrentPage(CurrentPage As String, UserPage As String, RoleId As String)

        If RoleId <> "1" And UserPage <> "" Then

            If Not CurrentPage.Contains(UserPage.Remove(0, 1)) Then Return False

        End If

        Return True

    End Function

    Function GetLabel(PageName As String, LanguageId As String) As DataTable

        Dim dt As DataTable = New DataTable

        Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

            Con.Open()

            Using Com As SqlCommand = New SqlCommand("spLanguage", Con)

                Com.CommandType = CommandType.StoredProcedure
                Com.Parameters.AddWithValue("operation", "select")
                Com.Parameters.AddWithValue("pagename", PageName)
                Com.Parameters.AddWithValue("languageid", LanguageId)

                Using ds As SqlDataReader = Com.ExecuteReader
                    dt.Load(ds)
                    ds.Close()
                End Using

            End Using

            Con.Close()

        End Using

        Return dt

    End Function

    Function mod01GetMark(Answer As String(,), UserAnswer As String, NoOfBlokUsed As Integer) As Single

        Dim AnswerArray As String() = UserAnswer.Split(",")
        Dim AnswerArray2D(2, 4) As String
        Dim count As Integer = 0
        Dim NonEmptyBlok As Integer = 0
        Dim Mark As Single = 1

        For i As Integer = 0 To 2
            For j As Integer = 0 To 4
                AnswerArray2D(i, j) = AnswerArray(count)
                If AnswerArray(count) <> "" Then NonEmptyBlok = NonEmptyBlok + 1
                count = count + 1
            Next
        Next

        'get no of pattern use in answer. is not same fail
        If NonEmptyBlok <> NoOfBlokUsed Then Return 0

        For i As Integer = 0 To 2
            For j As Integer = 0 To 4
                If AnswerArray2D(i, j) = Answer(0, 0) Then
                    If ComparePattern(Answer, AnswerArray2D, i, j) Then Return Mark
                End If
            Next
        Next

        Return 0

    End Function

    Private Function ComparePattern(AnswerPattern As String(,), UserAnswer As String(,), UserAnswerX As Integer, UserAnswerY As Integer)

        For i As Integer = 0 To AnswerPattern.GetLength(0) - 1
            For j As Integer = 0 To AnswerPattern.GetLength(1) - 1

                If UserAnswerX + i > UserAnswer.GetLength(0) - 1 Or UserAnswerY + j > UserAnswer.GetLength(1) - 1 Then Return False

                If AnswerPattern(i, j) <> UserAnswer(UserAnswerX + i, UserAnswerY + j) Then Return False
            Next
        Next

        Return True

    End Function

    Function mod02GetMark(Answer As String, UserAnswer As String) As Single

        Dim Mark As Single = 1

        If UserAnswer = Answer Then
            Return Mark
        End If

        Return 0

    End Function

    Function mod03GetMark(Answer As String, UserAnswer As String) As Single

        Dim Mark As Single = 1

        If UserAnswer = Answer Then
            Return Mark
        End If

        Return 0

    End Function

    Function mod04GetMark(Answer As String(), UserAnswer As String) As Single

        Dim UserAnswerArray As String() = UserAnswer.Split(",")

        Dim UserMark As Single = 0

        If UserAnswer.Length >= 1 AndAlso Answer(0) = UserAnswerArray(0) Then
            UserMark = UserMark + 1
        End If

        If UserAnswer.Length >= 2 AndAlso Answer(1) = UserAnswerArray(1) Then
            UserMark = UserMark + 1
        End If

        If UserAnswer.Length >= 3 AndAlso Answer(2) = UserAnswerArray(2) Then
            UserMark = UserMark + 1
        End If

        Return UserMark

    End Function

    Function mod05GetMark(Answer As String, UserAnswer As String) As Single

        Dim Mark As Single = 1

        Dim UserAnswerArray As String() = UserAnswer.Split(",")

        For Each UserAnswer In UserAnswerArray

            If Not Answer.Contains(UserAnswer) Then Return 0

        Next

        Return Mark

    End Function

    Function mod06GetMark(QuestionNo As Integer, LanguageId As String, UserAnswer As String) As Single

        Dim Answer As List(Of mod6AnswerMark) = New List(Of mod6AnswerMark)
        Dim match As Match

        Answer = mod06GetAnswer(QuestionNo, LanguageId)

        For Each x As mod6AnswerMark In Answer

            match = Regex.Match(UserAnswer, x.Answer, RegexOptions.IgnoreCase)

            If match.Success Then
                Return x.Mark
            End If

        Next

        Return 0

    End Function

    Function mod06GetAnswer(QuestionNo As Integer, LanguageId As String) As List(Of mod6AnswerMark)

        Dim Answer As List(Of mod6AnswerMark) = New List(Of mod6AnswerMark)

        Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

            Con.Open()

            Using Com As SqlCommand = New SqlCommand("spMod6Answer", Con)

                Com.CommandType = CommandType.StoredProcedure
                Com.Parameters.AddWithValue("questionno", QuestionNo)
                Com.Parameters.AddWithValue("languageid", LanguageId)

                Using ds As SqlDataReader = Com.ExecuteReader

                    While ds.Read

                        Answer.Add(New mod6AnswerMark(ds("Value").ToString, Convert.ToSingle(ds("Mark"))))

                    End While

                    ds.Close()

                End Using

            End Using

            Con.Close()

        End Using

        Return Answer

    End Function

    Function mod07GetMark(Answer As String(), UserAnswer As String) As Single

        Dim Mark As Single = 1

        Dim UserAnswerArray As String() = UserAnswer.Split(",")

        For i As Integer = 0 To Answer.Length - 1

            If UserAnswerArray(i) <> Answer(i) Then Return 0

        Next

        Return Mark

    End Function

    Function mod08GetMark(Answer As String, UserAnswer As String) As Single

        Dim UserAnswerArray As String() = UserAnswer.Split(",")
        Dim Mark As Single = 0

        For Each UserAnswer In UserAnswerArray

            If Answer.Contains(UserAnswer) Then
                Mark = Mark + 1
            Else
                Mark = Mark - 1
            End If

        Next

        If Mark < 0 Then Mark = 0

        Return Mark

    End Function

    Function mod09GetMark(Answer As String, UserAnswer As String) As Single

        Dim UserAnswerArray As String() = UserAnswer.Split(",")
        Dim AnswerArray As String() = Answer.Split(",")
        Dim Mark As Single = 1

        For i As Integer = 0 To AnswerArray.Length - 1

            If Not AnswerArray(i) = UserAnswerArray(i) Then Return 0

        Next

        Return Mark

    End Function

    Function mod10GetMark(HorizontalPair As List(Of CorrectPair), VerticalPair As List(Of CorrectPair), UserAnswer As String, Row As Integer, Col As Integer, PairMark As Single) As Single

        Dim Mark As Single = 0
        Dim AnswerArray As String() = UserAnswer.Split(",")
        Dim AnswerArray2D(Row - 1, Col - 1) As String
        Dim count As Integer = 0

        For i As Integer = 0 To Row - 1
            For j As Integer = 0 To Col - 1
                AnswerArray2D(i, j) = AnswerArray(count)
                count = count + 1
            Next
        Next

        For Each x As CorrectPair In HorizontalPair
            If x.LocateHorizontally(AnswerArray2D) Then Mark = Mark + PairMark
        Next

        For Each y As CorrectPair In VerticalPair
            If y.LocateVertically(AnswerArray2D) Then Mark = Mark + PairMark
        Next

        Return Mark

    End Function

End Module

Class mod6AnswerMark

    Public Answer As String
    Public Mark As Single

    Public Sub New()

    End Sub

    Public Sub New(answer As String, mark As Single)
        Me.Answer = answer
        Me.Mark = mark
    End Sub

End Class

Class CorrectPair

    Public FirstValue As String
    Public SecondValue As String

    Public Sub New()

    End Sub

    Public Sub New(firstval As String, secval As String)
        FirstValue = firstval
        SecondValue = secval
    End Sub

    Public Function LocateHorizontally(Pattern As String(,)) As Boolean

        For i As Integer = 0 To Pattern.GetLength(0) - 1

            For j As Integer = 0 To Pattern.GetLength(1) - 1

                If Pattern(i, j) = FirstValue Then

                    If j + 1 > Pattern.GetLength(1) - 1 Then Return False

                    If Pattern(i, j + 1) = SecondValue Then Return True

                End If

            Next

        Next

        Return False

    End Function

    Public Function LocateVertically(Pattern As String(,)) As Boolean

        For i As Integer = 0 To Pattern.GetLength(0) - 1

            For j As Integer = 0 To Pattern.GetLength(1) - 1

                If Pattern(i, j) = FirstValue Then

                    If i + 1 > Pattern.GetLength(0) - 1 Then Return False

                    If Pattern(i + 1, j) = SecondValue Then Return True

                End If

            Next

        Next

        Return False

    End Function

End Class

Class RegexUtilities
    Dim invalid As Boolean = False

    Public Function IsValidEmail(strIn As String) As Boolean
        invalid = False
        If String.IsNullOrEmpty(strIn) Then Return False

        ' Use IdnMapping class to convert Unicode domain names.
        Try
            strIn = Regex.Replace(strIn, "(@)(.+)$", AddressOf Me.DomainMapper, RegexOptions.None)
        Catch e As Exception
            Return False
        End Try

        If invalid Then Return False

        ' Return true if strIn is in valid e-mail format.
        Try
            Return Regex.IsMatch(strIn,
                 "^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 "(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                 RegexOptions.IgnoreCase)
        Catch e As Exception
            Return False
        End Try
    End Function

    Private Function DomainMapper(match As Match) As String
        ' IdnMapping class with default property values.
        Dim idn As New IdnMapping()

        Dim domainName As String = match.Groups(2).Value
        Try
            domainName = idn.GetAscii(domainName)
        Catch e As ArgumentException
            invalid = True
        End Try
        Return match.Groups(1).Value + domainName
    End Function
End Class

