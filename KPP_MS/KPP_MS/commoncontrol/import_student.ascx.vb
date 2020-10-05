Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class import_student
    Inherits System.Web.UI.UserControl

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strBil As String = ""

    Dim strStudentName As String = ""
    Dim strStudentMykad As String = ""
    Dim strStudentGender As String = ""
    Dim strStudentRace As String = ""
    Dim strStudentReligion As String = ""
    Dim strStudentEmail As String = ""
    Dim strStudentPhone As String = ""
    Dim strStudentAddress As String = ""
    Dim strStudentPostcode As String = ""
    Dim strStudentCity As String = ""
    Dim strStudentState As String = ""
    Dim strStudentYear As String = ""

    Dim strStudentLevel As String = ""
    Dim strStudentSem As String = ""

    Dim strFatherMykad As String = ""
    Dim strFatherName As String = ""
    Dim strFatherEmail As String = ""
    Dim strFatherPhone As String = ""
    Dim strFatherJob As String = ""
    Dim strFatherSalary As String = ""

    Dim strMotherMykad As String = ""
    Dim strMotherName As String = ""
    Dim strMotherEmail As String = ""
    Dim strMotherPhone As String = ""
    Dim strMotherJob As String = ""
    Dim strMotherSalary As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/student_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [studentinfo$]", connection)
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
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Calon:<br />" & validationMessage
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

                'BIL
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Bil")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("Bil")
                End If

                'STUDENT MYKAD
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Mykad")) Then
                    strStudentMykad = SiteData.Tables(0).Rows(i).Item("Student_Mykad")
                Else
                    strMsg += " Please Enter Student Mykad |"
                End If

                'STUDENT NAME
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Name")) Then
                    strStudentName = SiteData.Tables(0).Rows(i).Item("Student_Name")
                Else
                    strMsg += " Please Enter Student Name |"
                End If

                'STUDENT GENDER
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Gender")) Then
                    strStudentGender = SiteData.Tables(0).Rows(i).Item("Student_Gender")
                Else
                    strMsg += " Please Enter Student Gender |"
                End If

                'STUDENT RACE
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Race")) Then
                    strStudentRace = SiteData.Tables(0).Rows(i).Item("Student_Race")
                Else
                    strMsg += " Please Enter Student Race |"
                End If

                'STUDENT RELIGION
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Religion")) Then
                    strStudentReligion = SiteData.Tables(0).Rows(i).Item("Student_Religion")
                Else
                    strMsg += " Please Enter Student Religion |"
                End If

                'ADDRESS
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Address")) Then
                    strStudentAddress = SiteData.Tables(0).Rows(i).Item("Student_Address")
                Else
                    strMsg += " Please Enter Address |"
                End If

                'POSTALCODE
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Postal_Code")) Then
                    strStudentPostcode = SiteData.Tables(0).Rows(i).Item("Student_Postal_Code")
                Else
                    strMsg += " Please Enter Postal Code |"
                End If

                'CITY
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_City")) Then
                    strStudentCity = SiteData.Tables(0).Rows(i).Item("Student_City")
                Else
                    strMsg += " Please Enter City |"
                End If

                'STATE
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_State")) Then
                    strStudentState = SiteData.Tables(0).Rows(i).Item("Student_State")
                Else
                    strMsg += " Please Enter State |"
                End If

                'FATHER MYKAD
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Mykad")) Then
                    strFatherMykad = SiteData.Tables(0).Rows(i).Item("Father_Mykad")
                Else
                    strMsg += " Please Enter Father Mykad |"
                End If

                'FATHER NAME
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Name")) Then
                    strFatherName = SiteData.Tables(0).Rows(i).Item("Father_Name")
                Else
                    strMsg += " Please Enter Father Name |"
                End If

                'MOTHER MYKAD
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Mykad")) Then
                    strMotherMykad = SiteData.Tables(0).Rows(i).Item("Mother_Mykad")
                Else
                    strMsg += " Please Enter Mother Mykad |"
                End If

                'MOTHER NAME
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Name")) Then
                    strMotherName = SiteData.Tables(0).Rows(i).Item("Mother_Name")
                Else
                    strMsg += " Please Enter Mother Name |"
                End If

                'Student Level
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Level")) Then
                    strStudentLevel = SiteData.Tables(0).Rows(i).Item("Student_Level")
                Else
                    strMsg += " Please Enter Student Level |"
                End If

                'Student Sem
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Sem")) Then
                    strStudentSem = SiteData.Tables(0).Rows(i).Item("Student_Sem")
                Else
                    strMsg += " Please Enter Student Sem |"
                End If

                'Student Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Year")) Then
                    strStudentYear = SiteData.Tables(0).Rows(i).Item("Year")
                Else
                    strMsg += " Please Enter Year |"
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg = "BIL# :" & strBil & " Mykad " & strStudentMykad & ":" & strStudentName & strMsg
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

        Dim countUpdateParents As Integer = 0
        Dim countUpdateStudent As Integer = 0
        Dim countInsertParents As Integer = 0
        Dim countInsertStudent As Integer = 0


        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")

            ''STUDENT
            strStudentMykad = SiteData.Tables(0).Rows(i).Item("Student_Mykad")
            strStudentName = SiteData.Tables(0).Rows(i).Item("Student_Name")
            strStudentGender = SiteData.Tables(0).Rows(i).Item("Student_Gender")
            strStudentRace = SiteData.Tables(0).Rows(i).Item("Student_Race")
            strStudentReligion = SiteData.Tables(0).Rows(i).Item("Student_Religion")
            strStudentAddress = SiteData.Tables(0).Rows(i).Item("Student_Address")
            strStudentPostcode = SiteData.Tables(0).Rows(i).Item("Student_Postal_Code")
            strStudentCity = SiteData.Tables(0).Rows(i).Item("Student_City")
            strStudentState = SiteData.Tables(0).Rows(i).Item("Student_State")

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Email")) Then
                strStudentEmail = SiteData.Tables(0).Rows(i).Item("Student_Email")
            Else
                strStudentEmail = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Phone")) Then
                strStudentPhone = SiteData.Tables(0).Rows(i).Item("Student_Phone")
            Else
                strStudentPhone = ""
            End If

            strStudentLevel = SiteData.Tables(0).Rows(i).Item("Student_Level")
            strStudentSem = SiteData.Tables(0).Rows(i).Item("Student_Sem")
            strStudentYear = SiteData.Tables(0).Rows(i).Item("Year")

            ''FATHER
            strFatherMykad = SiteData.Tables(0).Rows(i).Item("Father_Mykad")
            strFatherName = SiteData.Tables(0).Rows(i).Item("Father_Name")

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Email")) Then
                strFatherEmail = SiteData.Tables(0).Rows(i).Item("Father_Email")
            Else
                strFatherEmail = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Phone")) Then
                strFatherPhone = SiteData.Tables(0).Rows(i).Item("Father_Phone")
            Else
                strFatherPhone = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Job")) Then
                strFatherJob = SiteData.Tables(0).Rows(i).Item("Father_Job")
            Else
                strFatherJob = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Father_Salary")) Then
                strFatherSalary = SiteData.Tables(0).Rows(i).Item("Father_Salary")
            Else
                strFatherSalary = ""
            End If

            ''MOTHER
            strMotherMykad = SiteData.Tables(0).Rows(i).Item("Mother_Mykad")
            strMotherName = SiteData.Tables(0).Rows(i).Item("Mother_Name")

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Email")) Then
                strMotherEmail = SiteData.Tables(0).Rows(i).Item("Mother_Email")
            Else
                strMotherEmail = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Phone")) Then
                strMotherPhone = SiteData.Tables(0).Rows(i).Item("Mother_Phone")
            Else
                strMotherPhone = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Job")) Then
                strMotherJob = SiteData.Tables(0).Rows(i).Item("Mother_Job")
            Else
                strMotherJob = ""
            End If

            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mother_Salary")) Then
                strMotherSalary = SiteData.Tables(0).Rows(i).Item("Mother_Salary")
            Else
                strMotherSalary = ""
            End If

            ''UPDATE
            ''IF PARENT EXISTS
            strSQL = "SELECT parent_ID FROM parent_Info WHERE parent_IC = '" & strFatherMykad & "' OR parent_IC = '" & strMotherMykad & "'"
            If oCommon.isExist(strSQL) = True Then

                strSQL = "SELECT parent_ID FROM parent_Info WHERE parent_IC = '" & strFatherMykad & "'"
                Dim parentfatherID As String = oCommon.getFieldValue(strSQL)
                strSQL = "SELECT parent_ID FROM parent_Info WHERE parent_IC = '" & strMotherMykad & "'"
                Dim parentmotherID As String = oCommon.getFieldValue(strSQL)

                ''UPDATE FATHER INFO
                strSQL = "  UPDATE parent_Info SET 
                            parent_IC = '" & strFatherMykad & "', 
                            parent_Name = UPPER('" & oCommon.FixSingleQuotes(strFatherName) & "'),
                            parent_Email = '" & oCommon.FixSingleQuotes(strFatherEmail) & "',
                            parent_MobileNo = '" & oCommon.FixSingleQuotes(strFatherPhone) & "',
                            parent_Work = UPPER('" & oCommon.FixSingleQuotes(strFatherJob) & "'),
                            parent_Salary = '" & oCommon.FixSingleQuotes(strFatherSalary) & "'"

                strSQL += " WHERE parent_ID = '" & parentfatherID & "'"

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    countUpdateParents = countUpdateParents + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

                ''UPDATE MOTHER INFO
                strSQL = "  UPDATE parent_Info SET 
                            parent_IC = '" & strMotherMykad & "', 
                            parent_Name = UPPER('" & oCommon.FixSingleQuotes(strMotherName) & "'),
                            parent_Email = '" & oCommon.FixSingleQuotes(strMotherEmail) & "',
                            parent_MobileNo = '" & oCommon.FixSingleQuotes(strMotherPhone) & "',
                            parent_Work = UPPER('" & oCommon.FixSingleQuotes(strMotherJob) & "'),
                            parent_Salary = '" & oCommon.FixSingleQuotes(strMotherSalary) & "'"

                strSQL += " WHERE parent_ID = '" & parentmotherID & "'"

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    countUpdateParents = countUpdateParents + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

                ''IF STUDENT EXIST
                strSQL = "SELECT std_ID FROM student_info WHERE parent_fatherID = '" & parentfatherID & "' OR parent_motherID = '" & parentmotherID & "'"
                If oCommon.isExist(strSQL) = True Then

                    ''UPDATE STUDENT INFO
                    strSQL = "SELECT std_ID FROM student_info WHERE student_Mykad = '" & strStudentMykad & "'"
                    Dim stdID As String = oCommon.getFieldValue(strSQL)

                    strSQL = "  UPDATE student_info SET 
                                student_Mykad = '" & strStudentMykad & "', 
                                student_Name = UPPER('" & oCommon.FixSingleQuotes(strStudentName) & "'), 
                                student_Sex = UPPER('" & strStudentGender & "'), 
                                student_Race = UPPER('" & strStudentRace & "'), 
                                student_Religion = UPPER('" & strStudentReligion & "'),
                                student_Email = '" & oCommon.FixSingleQuotes(strStudentEmail) & "',
                                student_FonNo = '" & oCommon.FixSingleQuotes(strStudentPhone) & "',
                                student_Address = UPPER('" & oCommon.FixSingleQuotes(strStudentAddress) & "'), 
                                student_PostalCode ='" & strStudentPostcode & "', 
                                student_City = UPPER('" & strStudentCity & "'), 
                                student_State = UPPER('" & strStudentState & "'),
                                student_Year = '" & oCommon.FixSingleQuotes(strStudentYear) & "',
                                parent_FatherID = '" & parentfatherID & "',
                                parent_MotherID = '" & parentmotherID & "'"

                    strSQL += " WHERE std_ID = '" & stdID & "'"

                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = 0 Then
                        countUpdateStudent = countUpdateStudent + 1
                        errorData = 0
                    Else
                        errorData = 1
                    End If

                Else

                    ''IF STUDENT NOT EXIST
                    ''ADD NEW STUDENT

                    strSQL = "  INSERT INTO student_info 
                                (student_Mykad, 
                                student_Name, 
                                student_Sex, 
                                student_Race, 
                                student_Religion, 
                                student_Email, 
                                student_FonNo, 
                                student_Address, 
                                student_PostalCode, 
                                student_City, 
                                student_State, 
                                student_Year, 
                                parent_fatherID, 
                                parent_motherID, 
                                student_Photo, 
                                student_Status, 
                                student_LoginAttempt)"

                    strSQL += " VALUES 
                                ('" & strStudentMykad & "', 
                                UPPER('" & oCommon.FixSingleQuotes(strStudentName) & "'), 
                                UPPER('" & strStudentGender & "'), 
                                UPPER('" & strStudentRace & "'), 
                                UPPER('" & strStudentReligion & "'), 
                                '" & oCommon.FixSingleQuotes(strStudentEmail) & "',
                                '" & oCommon.FixSingleQuotes(strStudentPhone) & "',
                                UPPER('" & oCommon.FixSingleQuotes(strStudentAddress) & "'), 
                                '" & strStudentPostcode & "', 
                                UPPER('" & strStudentCity & "'), 
                                UPPER('" & strStudentState & "'),
                                '" & strStudentYear & "',
                                '" & parentfatherID & "', 
                                '" & parentmotherID & "', 
                                '~/student_Image/user.png', 
                                'Access', 
                                '0')"

                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = 0 Then

                        Dim getStd_ID As String = "select std_ID from student_info where student_Mykad = '" & strStudentMykad & "' and student_Status = 'Access'"
                        Dim datastd_ID As String = oCommon.getFieldValue(getStd_ID)

                        Dim getStudent_Level_ID As String = "select ID from student_level where std_ID = '" & datastd_ID & "' and student_Level = '" & strStudentLevel & "' and student_Sem = '" & strStudentSem & "' and year = '" & strStudentYear & "'"
                        Dim dataStudent_Level_ID As String = oCommon.getFieldValue(getStudent_Level_ID)

                        If dataStudent_Level_ID = "" Then
                            Using PJGDATA As New SqlCommand("INSERT INTO student_level(std_ID,student_Level,student_Sem,year,day) values 
                                                            ('" & datastd_ID & "' ,'" & strStudentLevel & "','" & strStudentSem & "','" & strStudentYear & "',
                                                            '" & Now.Month & "','" & Now.Day & "')", objConn)
                                objConn.Open()
                                Dim k = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                            End Using
                        End If

                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                        Dim level As String = ""

                        If strStudentLevel = "Foundation 1" Or strStudentLevel = "Foundation 2" Or strStudentLevel = "Foundation 3" Then
                            level = "ASAS 1"
                        ElseIf strStudentLevel = "Level 1" Or strStudentLevel = "Level 2" Then
                            level = "TAHAP 1"
                        End If

                        Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & strStudentMykad & "'"
                        Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                        Dim find_PPCSDate As String = "select PPCSDate from PPCS where StudentID = '" & get_StudentID & "'"
                        Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                        strSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan) 
                                  values('" & get_StudentID & "','" & get_PPCSDate & "','" & strStudentYear & "','" & level & "','N',)"
                        strRet = oCommon.ExecuteSQLPermata(strSQL)

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        Dim getcourse_ID As String = "select course_ID from course where std_ID = '" & datastd_ID & "' "
                        Dim datacourse_ID As String = oCommon.getFieldValue(getcourse_ID)

                        If datacourse_ID = "" Then
                            Using PJGDATA As New SqlCommand("INSERT INTO course(std_ID,year) values 
                                                            ('" & datastd_ID & "' ,'" & strStudentYear & "')", objConn)
                                objConn.Open()
                                Dim k = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                            End Using
                        End If

                        countInsertStudent = countInsertStudent + 1
                        errorData = 0
                    Else
                        errorData = 1
                    End If

                End If

            Else

                ''INSERT NEW PARENT DATA
                strSQL = "  INSERT INTO parent_Info
                            (
                            parent_IC,
                            parent_Name,
                            parent_Email,
                            parent_MobileNo,
                            parent_Work,
                            parent_Salary,
                            parent_Sex,
                            parent_No
                            )"

                strSQL += " VALUES 
                                ('" & strFatherMykad & "',
                                UPPER('" & oCommon.FixSingleQuotes(strFatherName) & "'),
                                '" & oCommon.FixSingleQuotes(strFatherEmail) & "',
                                '" & oCommon.FixSingleQuotes(strFatherPhone) & "',
                                UPPER('" & oCommon.FixSingleQuotes(strFatherJob) & "'),
                                '" & oCommon.FixSingleQuotes(strFatherSalary) & "',
                                'MALE',
                                '1') "

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    countInsertParents = countInsertParents + 1
                    errorData = 0
                Else
                    errorData = 1
                End If

                strSQL = "  INSERT INTO parent_Info
                            (
                            parent_IC,
                            parent_Name,
                            parent_Email,
                            parent_MobileNo,
                            parent_Work,
                            parent_Salary,
                            parent_Sex,
                            parent_No
                            )"

                strSQL += " VALUES 
                            ('" & strMotherMykad & "',
                            UPPER('" & oCommon.FixSingleQuotes(strMotherName) & "'),
                            '" & oCommon.FixSingleQuotes(strMotherEmail) & "',
                            '" & oCommon.FixSingleQuotes(strMotherPhone) & "',
                            UPPER('" & oCommon.FixSingleQuotes(strMotherJob) & "'),
                            '" & oCommon.FixSingleQuotes(strMotherSalary) & "',
                            'FEMALE',
                            '2')"

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then

                    Dim getStd_ID As String = "select std_ID from student_info where student_Mykad = '" & strStudentMykad & "' and student_Status = 'Access'"
                    Dim datastd_ID As String = oCommon.getFieldValue(getStd_ID)

                    Dim getStudent_Level_ID As String = "select ID from student_level where std_ID = '" & datastd_ID & "' and student_Level = '" & strStudentLevel & "' and student_Sem = '" & strStudentSem & "' and year = '" & strStudentYear & "'"
                    Dim dataStudent_Level_ID As String = oCommon.getFieldValue(getStudent_Level_ID)

                    If dataStudent_Level_ID = "" Then
                        Using PJGDATA As New SqlCommand("INSERT INTO student_level(std_ID,student_Level,student_Sem,year,day) values 
                                                            ('" & datastd_ID & "' ,'" & strStudentLevel & "','" & strStudentSem & "','" & strStudentYear & "',
                                                            '" & Now.Month & "','" & Now.Day & "')", objConn)
                            objConn.Open()
                            Dim k = PJGDATA.ExecuteNonQuery()
                            objConn.Close()
                        End Using
                    End If

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                    Dim level As String = ""

                    If strStudentLevel = "Foundation 1" Or strStudentLevel = "Foundation 2" Or strStudentLevel = "Foundation 3" Then
                        level = "ASAS 1"
                    ElseIf strStudentLevel = "Level 1" Or strStudentLevel = "Level 2" Then
                        level = "TAHAP 1"
                    End If

                    Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & strStudentMykad & "'"
                    Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                    Dim find_PPCSDate As String = "select PPCSDate from PPCS where StudentID = '" & get_StudentID & "'"
                    Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                    strSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan) 
                                  values('" & get_StudentID & "','" & get_PPCSDate & "','" & strStudentYear & "','" & level & "','N',)"
                    strRet = oCommon.ExecuteSQLPermata(strSQL)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    Dim getcourse_ID As String = "select course_ID from course where std_ID = '" & datastd_ID & "' "
                    Dim datacourse_ID As String = oCommon.getFieldValue(getcourse_ID)

                    If datacourse_ID = "" Then
                        Using PJGDATA As New SqlCommand("INSERT INTO course(std_ID,year) values 
                                                            ('" & datastd_ID & "' ,'" & strStudentYear & "')", objConn)
                            objConn.Open()
                            Dim k = PJGDATA.ExecuteNonQuery()
                            objConn.Close()
                        End Using
                    End If

                    countInsertStudent = countInsertStudent + 1
                    errorData = 0
                Else
                    errorData = 1
                End If
            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsertStudent & " new student's records. " & countInsertParents & " new parent's records. " & countUpdateStudent & " student records updated and " & countUpdateParents & " parent records updated.", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        Dim strBil As String = ""

        Dim strStudentName As String = ""
        Dim strStudentMykad As String = ""
        Dim strStudentGender As String = ""
        Dim strStudentRace As String = ""
        Dim strStudentReligion As String = ""
        Dim strStudentEmail As String = ""
        Dim strStudentPhone As String = ""
        Dim strStudentAddress As String = ""
        Dim strStudentPostcode As String = ""
        Dim strStudentCity As String = ""
        Dim strStudentState As String = ""
        Dim strStudentYear As String = ""

        Dim strStudentLevel As String = ""
        Dim strStudentSem As String = ""

        Dim strFatherMykad As String = ""
        Dim strFatherName As String = ""
        Dim strFatherEmail As String = ""
        Dim strFatherPhone As String = ""
        Dim strFatherJob As String = ""
        Dim strFatherSalary As String = ""

        Dim strMotherMykad As String = ""
        Dim strMotherName As String = ""
        Dim strMotherEmail As String = ""
        Dim strMotherPhone As String = ""
        Dim strMotherJob As String = ""
        Dim strMotherSalary As String = ""

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

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/student_info.xlsx")
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class