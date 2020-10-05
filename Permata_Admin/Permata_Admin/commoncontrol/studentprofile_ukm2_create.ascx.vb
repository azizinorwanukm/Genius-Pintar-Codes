Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class studentprofile_ukm2_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '-list of exam avail
                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

                '--DOB_Year
                master_dobyear_list()

                ''--set unique ID
                lblStudentID.Text = System.Guid.NewGuid.ToString
                lblParentID.Text = System.Guid.NewGuid.ToString

                Response.Cookies("ukmadmin_studentid").Value = lblStudentID.Text
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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

            'ddlDOB_Year.Items.Add(New ListItem("ALL", "ALL"))

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

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            If ValidatePage() = False Then
                Exit Sub
            End If

            '--start insert transaction
            strRet = UKM2_Insert_Transaction()
            If strRet = "0" Then
                'Dim strMSg As String = "Rekod pendaftaran pelajar anda berjaya disimpan(IsHadir=N). ID PELAJAR:" & txtMYKAD.Text
                'Response.Redirect("result.page.aspx?msg=" & strMSg & "&msgtype=info")
                lblMsg.Text = "Rekod pendaftaran pelajar anda berjaya disimpan(IsHadir=N)."
            Else
                lblMsg.Text = "system error:" & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub setStudentSchool_N(ByVal strStudentID As String)
        strSQL = "UPDATE StudentSchool SET IsLatest='N' WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub

    Private Function UKM2_Insert_Transaction() As String
        strRet = "0"
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
                '--1StudentProfile
                Dim strStudentAge As String = Now.Year - CInt(ddlDOB_Year.Text)
                strSQL = "INSERT INTO StudentProfile (StudentID,MYKAD,Pwd,StudentFullname,DOB_Day,DOB_Month,DOB_Year,StudentGender,StudentRace,StudentReligion,StudentEmail,StudentForm,StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState,StudentCountry,TalkBM,TalkBI,TalkMan,TalkTamil,TalkArab,WriteBM,WriteBI,WriteMan,WriteTamil,WriteArab,IsUpdated)" &
                " VALUES ('" & lblStudentID.Text & "','" & oDes.EncryptData(txtMYKAD.Text) & "','NA','" & oCommon.FixSingleQuotes(txtStudentFullname.Text.ToUpper) & "','" & selStudentDOB_day.Value & "','" & selStudentDOB_month.Value & "','" & ddlDOB_Year.Text & "','" & selStudentGender.Value & "','" & selStudentRace.Value & "','" & selStudentReligion.Value & "','" & oCommon.FixSingleQuotes(txtStudentEmail.Text) & "','NA','" & oCommon.FixSingleQuotes(txtStudentAddress1.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentAddress2.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtStudentCity.Text.ToUpper) & "','" & selStudentState.Value & "','" & selStudentCountry.Value & "',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Y')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--reset N before set Y for new
                setStudentSchool_N(lblStudentID.Text)

                '--2 StudentSchool
                Dim strSchoolID As String = Request.QueryString("schoolid")
                strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,CreatedDate,IsLatest) VALUES ('" & lblStudentID.Text & "','" & strSchoolID & "','" & oCommon.getNow & "','Y')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--3 ParentProfile
                strSQL = "INSERT INTO ParentProfile (StudentID,ParentID,IsUpdated) VALUES ('" & lblStudentID.Text & "','" & lblParentID.Text & "','Y')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--4 UKM1 
                strSQL = "SELECT StudentID FROM UKM1 WHERE StudentID='" & lblStudentID.Text & "' AND ExamYear='" & ddlExamYear.Text & "'"
                If oCommon.isExist(strSQL) = False Then
                    Dim examyear_id As String = oCommon.getFieldValue("select examyearid from master_examyear where ExamYear = '" & ddlExamYear.Text & "'")

                    If ddlExamYear.Text = getUKM1Year Then
                        strSQL = "INSERT INTO UKM1_" & getUKM1Year & " (StudentID,ExamYear,QuestionYear,HostAddress,HostName,Browser,SelectedLang,Status,DOB_Year,examyear_id) VALUES('" & lblStudentID.Text & "','" & ddlExamYear.Text & "','" & ddlExamYear.Text & "','" & Request.UserHostAddress & "','" & Request.UserHostName & "','" & Request.UserAgent & "','ms-MY','NEW','" & ddlDOB_Year.Text & "','" & examyear_id & "')"
                        command.CommandText = strSQL
                        command.ExecuteNonQuery()
                    End If

                    strSQL = "INSERT INTO UKM1 (StudentID,ExamYear,QuestionYear,HostAddress,HostName,Browser,SelectedLang,Status,DOB_Year,examyear_id) VALUES('" & lblStudentID.Text & "','" & ddlExamYear.Text & "','" & ddlExamYear.Text & "','" & Request.UserHostAddress & "','" & Request.UserHostName & "','" & Request.UserAgent & "','ms-MY','NEW','" & ddlDOB_Year.Text & "','" & examyear_id & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()

                    strSQL = "INSERT INTO UKM1_Answer (StudentID,ExamYear) VALUES('" & lblStudentID.Text & "','" & ddlExamYear.Text & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If


                '--5 UKM2
                strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & lblStudentID.Text & "' AND ExamYear='" & ddlExamYear.Text & "'"
                If oCommon.isExist(strSQL) = False Then
                    strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,SchoolID,Status) VALUES ('" & lblStudentID.Text & "','" & ddlExamYear.Text & "','N','" & strSchoolID & "','NEW')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()

                    strSQL = "INSERT INTO UKM2_Answer (StudentID,ExamYear) VALUES ('" & lblStudentID.Text & "','" & ddlExamYear.Text & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

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

                    strRet = "Rollback:" & ex2.Message

                End Try
            End Try
        End Using

        '--0 means success
        Return strRet

    End Function


    Private Function StudentProfile_Insert() As String
        Dim strStudentAge As String = Now.Year - CInt(ddlDOB_Year.Text)

        strSQL = "INSERT INTO StudentProfile (StudentID,MYKAD,Pwd,StudentFullname,DOB_Day,DOB_Month,DOB_Year,StudentGender,StudentRace,StudentReligion,StudentEmail,StudentForm,StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState,StudentCountry,TalkBM,TalkBI,TalkMan,TalkTamil,TalkArab,WriteBM,WriteBI,WriteMan,WriteTamil,WriteArab,IsUpdated)" &
        " VALUES ('" & lblStudentID.Text & "','" & oDes.EncryptData(txtMYKAD.Text) & "','NA','" & oCommon.FixSingleQuotes(txtStudentFullname.Text.ToUpper) & "','" & selStudentDOB_day.Value & "','" & selStudentDOB_month.Value & "','" & ddlDOB_Year.Text & "','" & selStudentGender.Value & "','" & selStudentRace.Value & "','" & selStudentReligion.Value & "','" & oCommon.FixSingleQuotes(txtStudentEmail.Text) & "','NA','" & oCommon.FixSingleQuotes(txtStudentAddress1.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentAddress2.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtStudentCity.Text.ToUpper) & "','" & selStudentState.Value & "','" & selStudentCountry.Value & "',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Y')"

        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("studentprofile_ukm2_create", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress, CType(Session.Item("permata_admin"), String))

        Return strRet
    End Function

    Private Function ValidatePage() As Boolean

        ''--MYKAD is unique.
        strSQL = "SELECT StudentFullname FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        strRet = oCommon.getFieldValue(strSQL)
        If strRet.Length > 0 Then
            lblMsg.Text = "ID PELAJAR# sudah digunakan oleh " & strRet
            txtMYKAD.Focus()
            Return False
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. ID PELAJAR#!"
            txtMYKAD.Focus()
            Return False
        End If

        ''Alow for birthcert without - or space
        'If oCommon.isNumeric(txtMYKAD.Text) = False Then
        '    lblMsg.Text = "Invalid MYKAD format. Fill in numbers only! [0 - 9]"
        '    txtMYKAD.Focus()
        '    Return False
        'End If

        'If txtPwd.Text.Length = 0 Then
        '    lblMsg.Text = "Please fill-in this field. Kata Laluan#!"
        '    txtPwd.Focus()
        '    Return False
        'End If

        'If Not txtPwd.Text = txtPwdVerify.Text Then
        '    lblMsg.Text = "Kata laluan dan Ulang kata luan tidak sama."
        '    txtPwd.Focus()
        '    Return False
        'End If

        If txtStudentFullname.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Nama Pelajar!"
            txtStudentFullname.Focus()
            Return False

        End If

        If selStudentGender.Value = "" Then
            lblMsg.Text = "Please select this field. Jantina!"
            selStudentGender.Focus()
            Return False

        End If

        If selStudentDOB_day.Value = "" Then
            lblMsg.Text = "Please select this field. Tarikh Lahir. Hari!"
            selStudentDOB_day.Focus()
            Return False

        End If

        If selStudentDOB_month.Value = "" Then
            lblMsg.Text = "Please select this field. Tarikh Lahir. Bulan!"
            selStudentDOB_month.Focus()
            Return False

        End If

        If ddlDOB_Year.Text = "" Then
            lblMsg.Text = "Please select this field. Tarikh Lahir. Tahun!"
            Return False
        End If

        'If selStudentForm.Value = "" Then
        '    lblMsg.Text = "Please select this field. Darjah/Tingkatan!"
        '    selStudentForm.Focus()
        '    Return False

        'End If

        If selStudentRace.Value = "" Then
            lblMsg.Text = "Please select this field. Bangsa!"
            selStudentRace.Focus()
            Return False

        End If

        'If txtStudentAddress1.Text.Length = 0 Then
        '    lblMsg.Text = "Please fill-in this field. Alamat Rumah!"
        '    txtStudentAddress1.Focus()
        '    Return False
        'End If

        ''If txtStudentAddress2.Text.Length = 0 Then
        ''    lblMsg.Text = "Please fill-in this field. Alamat Rumah!"
        ''    txtStudentAddress2.Focus()
        ''    Return False
        ''End If

        'If txtStudentPostcode.Text.Length = 0 Then
        '    lblMsg.Text = "Please fill-in this field. Poskod!"
        '    txtStudentPostcode.Focus()
        '    Return False

        'End If

        ''--invalid postcode
        'If oCommon.isNumeric(txtStudentPostcode.Text) = False Then
        '    lblMsg.Text = "Invalid field format. Numeric only!"
        '    txtStudentPostcode.Focus()
        '    Return False

        'End If

        'If txtStudentCity.Text.Length = 0 Then
        '    lblMsg.Text = "Please fill-in this field. Bandar!"
        '    txtStudentCity.Focus()
        '    Return False

        'End If

        'If selStudentState.Value = "" Then
        '    lblMsg.Text = "Please select this field. Negeri!"
        '    selStudentState.Focus()
        '    Return False

        'End If

        'If txtStudentEmail.Text.Length = 0 Then
        '    lblMsg.Text = "Please fill-in this field. Email!"
        '    txtStudentEmail.Focus()
        '    Return False

        'End If

        ''--invalid email
        'If oCommon.isEmail(txtStudentEmail.Text) = False Then
        '    lblMsg.Text = "Invalid field format. Email address only!"
        '    txtStudentEmail.Focus()
        '    Return False
        'End If

        Return True
    End Function

End Class