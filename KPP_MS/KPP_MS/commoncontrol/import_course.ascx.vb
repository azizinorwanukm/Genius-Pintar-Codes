Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class import_course
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strBil As String = ""
    Dim strSubjectName As String = ""
    Dim strSubjectNameBM As String = ""
    Dim strSubjectCode As String = ""
    Dim strSubjectYear As String = ""
    Dim strSubjectType As String = ""
    Dim strSubjectReligion As String = ""
    Dim strSubjectStudentYear As String = ""
    Dim strSubjectCreditHour As String = ""
    Dim strSubjectSem As String = ""
    Dim strCourseName As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/course_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [course$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Kursus:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVar()
                strMsg = ""

                'Bil
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Bil")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("Bil")
                End If

                'Subject_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Name")) Then
                    strSubjectName = SiteData.Tables(0).Rows(i).Item("Subject_Name")
                Else
                    strMsg += " Please Enter Subject_Name |"
                End If

                'Subject_Code
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_NameBM")) Then
                    strSubjectNameBM = SiteData.Tables(0).Rows(i).Item("Subject_NameBM")
                Else
                    strMsg += " Please Enter Subject_Code |"
                End If

                'Subject_Code
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Code")) Then
                    strSubjectCode = SiteData.Tables(0).Rows(i).Item("Subject_Code")
                Else
                    strMsg += " Please Enter Subject_Code |"
                End If

                'Subject_Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Year")) Then
                    strSubjectYear = SiteData.Tables(0).Rows(i).Item("Subject_Year")
                Else
                    strMsg += " Please Enter Subject_Year |"
                End If

                'Subject_Type
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Type")) Then
                    strSubjectType = SiteData.Tables(0).Rows(i).Item("Subject_Type")
                Else
                    strMsg += " Please Enter Subject_Type |"
                End If

                'Subject_Religion
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Religion")) Then
                    strSubjectReligion = SiteData.Tables(0).Rows(i).Item("Subject_Religion")
                Else
                    strMsg += " Please Enter Subject_Religion |"
                End If

                'Subject_Student_Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Student_Year")) Then
                    strSubjectStudentYear = SiteData.Tables(0).Rows(i).Item("Subject_Student_Year")
                Else
                    strMsg += " Please Enter Subject_Student_Year |"
                End If

                'Subject_Credit_Hour
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Credit_Hour")) Then
                    strSubjectCreditHour = SiteData.Tables(0).Rows(i).Item("Subject_Credit_Hour")
                Else
                    strMsg += " Please Enter Subject_Credit_Hour |"
                End If

                'Subject_Sem
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Sem")) Then
                    strSubjectSem = SiteData.Tables(0).Rows(i).Item("Subject_Sem")
                Else
                    strMsg += " Please Enter Subject_Sem |"
                End If

                'course_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Course_Name")) Then
                    strCourseName = SiteData.Tables(0).Rows(i).Item("Course_Name")
                Else
                    strMsg += " Please Enter Course_Name |"
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg = "BIL# :" & strBil & " Subject Name " & strSubjectName & ":" & strMsg
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0

        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")
            strSubjectName = SiteData.Tables(0).Rows(i).Item("Subject_Name")
            strSubjectNameBM = SiteData.Tables(0).Rows(i).Item("Subject_NameBM")
            strSubjectCode = SiteData.Tables(0).Rows(i).Item("Subject_Code")
            strSubjectYear = SiteData.Tables(0).Rows(i).Item("Subject_Year")
            strSubjectType = SiteData.Tables(0).Rows(i).Item("Subject_Type")
            strSubjectReligion = SiteData.Tables(0).Rows(i).Item("Subject_Religion")
            strSubjectStudentYear = SiteData.Tables(0).Rows(i).Item("Subject_Student_Year")
            strSubjectCreditHour = SiteData.Tables(0).Rows(i).Item("Subject_Credit_Hour")
            strSubjectSem = SiteData.Tables(0).Rows(i).Item("Subject_Sem")
            strCourseName = SiteData.Tables(0).Rows(i).Item("course_Name")

            strSQL = "SELECT subject_ID FROM subject_info WHERE subject_Name = '" & oCommon.FixSingleQuotes(strSubjectName) & "' AND subject_NameBM = '" & oCommon.FixSingleQuotes(strSubjectNameBM) & "' AND subject_code = '" & strSubjectCode & "' AND subject_year = '" & strSubjectYear & "'"
            Dim subjectID As String = oCommon.getFieldValue(strSQL)

            If oCommon.isExist(strSQL) = True Then
                'UPDATE SUBJECT INFO
                strSQL = "  UPDATE subject_info SET
                                subject_Name = '" & oCommon.FixSingleQuotes(strSubjectName) & "',
                                subject_NameBM = '" & oCommon.FixSingleQuotes(strSubjectNameBM) & "',
                                subject_code = '" & strSubjectCode & "',
                                subject_year = '" & strSubjectYear & "',
                                subject_type = '" & strSubjectType & "',
                                subject_religions = '" & strSubjectReligion & "',
                                subject_StudentYear = '" & strSubjectStudentYear & "',
                                subject_CreditHour = '" & strSubjectCreditHour & "',
                                subject_sem = '" & strSubjectSem & "',
                                course_Name = '" & strCourseName & "'                                
                                WHERE subject_ID = '" & subjectID & "'"
                If strRet = 0 Then
                    countUpdate = countUpdate + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

            Else

                'INSERT NEW SUBJECT

                strSQL = "  INSERT INTO subject_info
                                (subject_Name,
                                subject_NameBM,
                                subject_code,
                                subject_year,
                                subject_type,
                                subject_religions,
                                subject_StudentYear,
                                subject_CreditHour,
                                subject_sem,
                                course_Name
                                )"

                strSQL += " VALUES 
                                ('" & oCommon.FixSingleQuotes(strSubjectName) & "', 
                                '" & oCommon.FixSingleQuotes(strSubjectNameBM) & "', 
                                '" & strSubjectCode & "', 
                                '" & strSubjectYear & "',
                                '" & strSubjectType & "',
                                '" & strSubjectReligion & "', 
                                '" & strSubjectStudentYear & "', 
                                '" & strSubjectCreditHour & "', 
                                '" & strSubjectSem & "', 
                                '" & strCourseName & "')"

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    countInsert = countInsert + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        Dim strBil As String = ""
        Dim strSubjectName As String = ""
        Dim strSubjectNameBM As String = ""
        Dim strSubjectCode As String = ""
        Dim strSubjectYear As String = ""
        Dim strSubjectType As String = ""
        Dim strSubjectReligion As String = ""
        Dim strSubjectStudentYear As String = ""
        Dim strSubjectCreditHour As String = ""
        Dim strSubjectSem As String = ""
        Dim strCourseName As String = ""

    End Sub

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick

        Response.Redirect("download/CourseProfileBatch.xlsx")

    End Sub

    Private Sub BtnUploaded_ServerClick(sender As Object, e As EventArgs) Handles BtnUploaded.ServerClick
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"
            Else
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]

    End Enum
End Class