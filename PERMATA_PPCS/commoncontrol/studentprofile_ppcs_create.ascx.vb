Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class studentprofile_ppcs_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ''--set unique ID
                lblStudentID.Text = System.Guid.NewGuid.ToString
                Response.Cookies("studentid").Value = lblStudentID.Text

                '--master_dobyear_list
                master_dobyear_list()
                ddlDOB_Year.Text = Now.Year

                '--studentrace_list
                studentrace_list()
                ddlStudentRace.Text = "LAIN-LAIN"

                ''studentreligion
                studentreligion_list()
                ddlStudentReligion.Text = "LAIN-LAIN"

                '--SchoolState_list
                SchoolState_list()
                ddlStudentState.Text = "LAIN-LAIN"

                '--ppcsdate_list
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                '--Temporary school for new student for UKM2
                'getTempSchool()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            divMsg.Attributes("class") = "error"
        End Try

    End Sub

    Private Sub studentreligion_list()
        strSQL = "SELECT StudentReligion FROM master_StudentReligion ORDER BY StudentReligion"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentReligion.DataSource = ds
            ddlStudentReligion.DataTextField = "StudentReligion"
            ddlStudentReligion.DataValueField = "StudentReligion"
            ddlStudentReligion.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub studentrace_list()
        strSQL = "SELECT StudentRace FROM master_StudentRace ORDER BY StudentRace"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentRace.DataSource = ds
            ddlStudentRace.DataTextField = "StudentRace"
            ddlStudentRace.DataValueField = "StudentRace"
            ddlStudentRace.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub SchoolState_list()
        strSQL = "SELECT SchoolState FROM SchoolState ORDER BY SchoolStateID"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentState.DataSource = ds
            ddlStudentState.DataTextField = "SchoolState"
            ddlStudentState.DataValueField = "SchoolState"
            ddlStudentState.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
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

            '--ddlDOB_Year.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        strSQL = oCommon.PPCSDate_Query(Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value))

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

            '--ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            strRet = studentprofile_create()
            If strRet = "0" Then
                Response.Redirect("studentprofile.view.aspx?studentid=" & lblStudentID.Text)
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "studentprofile_ppcs_create error:" & strRet
            End If

            'strRet = StudentProfile_Insert()
            'If strRet = "0" Then
            '    strRet = PPCS_insert()
            '    If strRet = "0" Then
            '        Response.Redirect("studentprofile.view.aspx?studentid=" & lblStudentID.Text)
            '        'Dim strMSg As String = "Rekod pendaftaran pelajar anda berjaya disimpan. ID PELAJAR:" & txtMYKAD.Text
            '        'Response.Redirect("result.page.aspx?msg=" & strMSg & "&msgtype=info")
            '    Else
            '        divMsg.Attributes("class") = "error"
            '        lblMsg.Text = "PPCS_insert error:" & strRet
            '    End If
            'Else
            '    divMsg.Attributes("class") = "error"
            '    lblMsg.Text = "StudentProfile_Insert error:" & strRet
            'End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function PPCS_insert() As String
        ''--mandatory record for each students
        ''--Temp school for new PPCS Student
        strSQL = "SELECT configString FROM master_Config WHERE configCode='SC01'"
        Dim strSchoolID As String = oCommon.getFieldValue(strSQL)
        If strSchoolID.Length = 0 Then
            Return "99:master_config school not exist! Please update student school." '--record not exist
        End If

        ''--dummy school inserted
        strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,CreatedDate) VALUES ('" & lblStudentID.Text & "','" & strSchoolID & "','" & oCommon.getNow & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''--insert into PPCS and LAYAK,TERIMA
        strSQL = "INSERT INTO PPCS (StudentID,PPCSDate,PPCSStatus,StatusTawaran) VALUES ('" & lblStudentID.Text & "','" & ddlPPCSDate.Text & "','LAYAK','TERIMA')"
        strRet = oCommon.ExecuteSQL(strSQL)
        Return strRet

    End Function

    Private Function StudentProfile_Insert() As String
        strSQL = "INSERT INTO StudentProfile (StudentID,MYKAD,Pwd,StudentFullname,DOB_Day,DOB_Month,DOB_Year,StudentGender,StudentRace,StudentReligion,StudentEmail,StudentForm,StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState,StudentCountry,TalkBM,TalkBI,TalkMan,TalkTamil,TalkArab,WriteBM,WriteBI,WriteMan,WriteTamil,WriteArab,IsUpdated,AlumniID)" & _
        " VALUES ('" & lblStudentID.Text & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "','NA','" & oCommon.FixSingleQuotes(txtStudentFullname.Text.ToUpper) & "','" & selStudentDOB_day.Value & "','" & selStudentDOB_month.Value & "','" & ddlDOB_Year.Text & "','" & selStudentGender.Value & "','" & ddlStudentRace.Text & "','" & ddlStudentReligion.Text & "','" & oCommon.FixSingleQuotes(txtStudentEmail.Text) & "','NA','" & oCommon.FixSingleQuotes(txtStudentAddress1.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentAddress2.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtStudentCity.Text.ToUpper) & "','" & ddlStudentState.Text & "','" & selStudentCountry.Value.ToUpper & "',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Y','" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "')"

        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("StudentProfile_Insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

        Return strRet
    End Function

    Private Function studentprofile_create() As String
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

            Try
                '--1 INSERT INTO StudentProfile 
                strSQL = "INSERT INTO StudentProfile (StudentID,MYKAD,Pwd,StudentFullname,DOB_Day,DOB_Month,DOB_Year,StudentGender,StudentRace,StudentReligion,StudentEmail,StudentForm,StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState,StudentCountry,TalkBM,TalkBI,TalkMan,TalkTamil,TalkArab,WriteBM,WriteBI,WriteMan,WriteTamil,WriteArab,IsUpdated,AlumniID,NoPelajar)" & _
                " VALUES ('" & lblStudentID.Text & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "','NA','" & oCommon.FixSingleQuotes(txtStudentFullname.Text.ToUpper) & "','" & selStudentDOB_day.Value & "','" & selStudentDOB_month.Value & "','" & ddlDOB_Year.Text & "','" & selStudentGender.Value & "','" & ddlStudentRace.Text & "','" & ddlStudentReligion.Text & "','" & oCommon.FixSingleQuotes(txtStudentEmail.Text) & "','" & selStudentForm.Value & "','" & oCommon.FixSingleQuotes(txtStudentAddress1.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentAddress2.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtStudentCity.Text.ToUpper) & "','" & ddlStudentState.Text & "','" & selStudentCountry.Value.ToUpper & "','" & chkTalkBM.Checked & "','" & chkTalkBI.Checked & "','" & chkTalkMan.Checked & "','" & chkTalkTamil.Checked & "','" & chkTalkArab.Checked & "','" & chkWriteBM.Checked & "','" & chkWriteBI.Checked & "','" & chkWriteMan.Checked & "','" & chkWriteTamil.Checked & "','" & chkWriteArab.Checked & "','Y','" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "','" & oCommon.FixSingleQuotes(txtNoPelajar.Text) & "')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()
                '--debug
                'Response.Write(strSQL)

                '--2 INSERT INTO PPCS
                strSQL = "INSERT INTO PPCS (StudentID,PPCSDate,PPCSStatus) VALUES ('" & lblStudentID.Text & "','" & ddlPPCSDate.Text & "','LAYAK')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--3 INSERT INTO ParentProfile
                Dim lblParentID As String = System.Guid.NewGuid.ToString
                strSQL = "INSERT INTO ParentProfile (StudentID,ParentID,IsUpdated) VALUES ('" & lblStudentID.Text & "','" & lblParentID & "','Y')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ''--4 mandatory record for each students. Get Temp SchoolID
                strSQL = "SELECT configString FROM master_Config WHERE configCode='SC01'"
                Dim strSchoolID As String = oCommon.getFieldValue(strSQL)
                If strSchoolID.Length = 0 Then
                    strSchoolID = "NA"
                End If

                ''--dummy school inserted
                strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,CreatedDate) VALUES ('" & lblStudentID.Text & "','" & strSchoolID & "','" & oCommon.getNow & "')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

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


    Private Function ValidatePage() As Boolean
        '--dr siti request remove mandatory for admin. 20131114

        ''--MYKAD is unique.
        strSQL = "SELECT MYKAD,StudentFullname FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
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

        If txtStudentFullname.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Nama Penuh!"
            txtStudentFullname.Focus()
            Return False

        End If

        '--not blank
        If Not txtAlumniID.Text.Length = 0 Then
            strSQL = "SELECT MYKAD,StudentFullname FROM StudentProfile WHERE AlumniID='" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            If strRet.Length > 0 Then
                lblMsg.Text = "Alumni ID sudah digunakan oleh: " & strRet
                txtMYKAD.Focus()
                Return False
            End If
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


        'If selStudentGender.Value = "" Then
        '    lblMsg.Text = "Please select this field. Jantina!"
        '    selStudentGender.Focus()
        '    Return False

        'End If

        'If selStudentDOB_day.Value = "" Then
        '    lblMsg.Text = "Please select this field. Tarikh Lahir. Hari!"
        '    selStudentDOB_day.Focus()
        '    Return False

        'End If

        'If selStudentDOB_month.Value = "" Then
        '    lblMsg.Text = "Please select this field. Tarikh Lahir. Bulan!"
        '    selStudentDOB_month.Focus()
        '    Return False

        'End If

        'If ddlDOB_Year.Text = "" Then
        '    lblMsg.Text = "Please select this field. Tarikh Lahir. Tahun!"
        '    ddlDOB_Year.Focus()
        '    Return False

        'End If

        'If selStudentForm.Value = "" Then
        '    lblMsg.Text = "Please select this field. Darjah/Tingkatan!"
        '    selStudentForm.Focus()
        '    Return False

        'End If

        'If ddlStudentRace.Text = "" Then
        '    lblMsg.Text = "Please select this field. Bangsa!"
        '    ddlStudentRace.Focus()
        '    Return False

        'End If

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