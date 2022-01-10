Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class student_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Dim strBil As String = ""

    Dim strStudentName As String = ""
    Dim strStudentID As String = ""
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
                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    Dim getStatus As String = Request.QueryString("status")

                    If getStatus = "RS" Then ''register student
                        txtbreadcrum1.Text = "Register Student Information"

                        RegisterStudent.Visible = True
                        ImportStudent.Visible = False
                        btnRegisterStudent.Attributes("class") = "btn btn-info"
                        btnImportStudent.Attributes("class") = "btn btn-default font"

                        student_year()
                        State()
                        Race_List()
                        Religion_List()
                        Level()
                        Salary()
                        Semester()

                    ElseIf getStatus = "IS" Then ''import student
                        txtbreadcrum1.Text = "Import Student Information"

                        RegisterStudent.Visible = False
                        ImportStudent.Visible = True
                        btnRegisterStudent.Attributes("class") = "btn btn-default font"
                        btnImportStudent.Attributes("class") = "btn btn-info"

                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRegisterStudent_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterStudent.ServerClick

        Response.Redirect("admin_daftar_pelajar_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=RS")

    End Sub

    Private Sub btnImportStudent_ServerClick(sender As Object, e As EventArgs) Handles btnImportStudent.ServerClick

        Response.Redirect("admin_daftar_pelajar_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=IS")

    End Sub

    Private Sub Salary()
        strSQL = "SELECT Parameter from setting where Type = 'Race'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRace.DataSource = ds
            ddlRace.DataTextField = "Parameter"
            ddlRace.DataValueField = "Parameter"
            ddlRace.DataBind()
            ddlRace.Items.Insert(0, New ListItem("Select Race", ""))
            ddlRace.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Race_List()
        strSQL = "SELECT Parameter from setting where Type = 'Salary'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsalaryP1.DataSource = ds
            ddlsalaryP1.DataTextField = "Parameter"
            ddlsalaryP1.DataValueField = "Parameter"
            ddlsalaryP1.DataBind()
            ddlsalaryP1.Items.Insert(0, New ListItem("Select Salary", ""))
            ddlsalaryP1.SelectedIndex = 0

            ddlsalaryP2.DataSource = ds
            ddlsalaryP2.DataTextField = "Parameter"
            ddlsalaryP2.DataValueField = "Parameter"
            ddlsalaryP2.DataBind()
            ddlsalaryP2.Items.Insert(0, New ListItem("Select Salary", ""))
            ddlsalaryP2.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Religion_List()
        strSQL = "SELECT Parameter from setting where Type = 'Religion'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligion.DataSource = ds
            ddlReligion.DataTextField = "Parameter"
            ddlReligion.DataValueField = "Parameter"
            ddlReligion.DataBind()
            ddlReligion.Items.Insert(0, New ListItem("Select Religion", ""))
            ddlReligion.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_year()
        strSQL = "SELECT Parameter from setting where Type = 'Year' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub State()
        strSQL = "SELECT Parameter FROM setting WHERE Type='State' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "Parameter"
            ddlState.DataValueField = "Parameter"
            ddlState.DataBind()
            ddlState.Items.Insert(0, New ListItem("Select State", String.Empty))
            ddlState.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlLevel.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Semester()
        strSQL = "SELECT * FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSem.DataSource = ds
            ddlSem.DataTextField = "Parameter"
            ddlSem.DataValueField = "Value"
            ddlSem.DataBind()
            ddlSem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlSem.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        If ddlLevel.SelectedIndex > 0 Then

            If IsNumeric(student_Mykad.Text) And student_Mykad.Text <> "" And student_Mykad.Text.Length < 14 Then

                If student_PostalCode.Text = "" Or IsNumeric(student_PostalCode.Text) Then

                    If student_Name.Text.Length > 0 And Not IsNothing(student_Name.Text) Then

                        If IsNumeric(student_FonNo.Text) Or student_FonNo.Text = "" Then

                            If std_txtCty.Text = "" Or std_txtCty.Text.Length > 0 Then

                                If ddlState.SelectedIndex > 0 Then

                                    If strgender.Length > 0 And Regex.IsMatch(strgender, "^[A-Za-z]+$") Then

                                        If student_Email.Text.Length > 0 Then

                                            If student_ID.Text.Length > 0 And Regex.IsMatch(student_ID.Text, "^[A-Za-z0-9]+$") Then

                                                If ddlYear.SelectedIndex > 0 Then

                                                    Dim imgPath As String = "~/student_Image/user.png"

                                                    If ddlRace.SelectedIndex > 0 Then

                                                        If ddlReligion.SelectedIndex > 0 Then

                                                            ''parent 1 validation
                                                            If validateParent1() = 0 Then

                                                                ''parent 2 validation
                                                                If validateParent2() = 0 Then

                                                                    Using STDDATA As New SqlCommand("INSERT student_info(student_ID,student_Mykad,student_Name,student_Email,student_FonNo,student_Address,
                                                                                                     student_Password,student_PostalCode,student_State,student_City,student_Photo,student_Sex,student_Race,student_Religion, student_Status) values 
                                                                                                     ('" & student_ID.Text & "','" & student_Mykad.Text & "','" & oCommon.FixSingleQuotes(student_Name.Text.ToUpper) & "',
                                                                                                     '" & student_Email.Text & "','" & student_FonNo.Text & "','" & student_Address.Text.ToUpper & "',
                                                                                                     '" & student_Mykad.Text & "','" & student_PostalCode.Text & "','" & ddlState.SelectedValue & "','" & std_txtCty.Text.ToUpper & "',
                                                                                                     '" & imgPath & "','" & strgender.ToUpper & "','" & ddlRace.SelectedValue & "','" & ddlReligion.SelectedValue & "', 'Access')", objConn)
                                                                        objConn.Open()
                                                                        Dim i = STDDATA.ExecuteNonQuery()
                                                                        objConn.Close()
                                                                        If i <> 0 Then
                                                                            ShowMessage(" Add Student Information ", MessageType.Success)

                                                                            Dim stdID As String = "select std_ID from student_info where student_Mykad = '" & student_Mykad.Text & "'"
                                                                            Dim dataStdID As String = oCommon.getFieldValue(stdID)

                                                                            Using STDLEVEL As New SqlCommand("INSERT INTO student_level(std_ID,student_Sem,student_Level,year,month,day) values 
                                                                                                    ('" & dataStdID & "','Sem 1','" & ddlLevel.SelectedValue & "','" & ddlYear.SelectedValue & "','" & Now.Month & "','" & Now.Day & "')", objConn)
                                                                                objConn.Open()
                                                                                Dim j = STDLEVEL.ExecuteNonQuery()
                                                                                objConn.Close()
                                                                                If j <> 0 Then
                                                                                    errorCount = 0
                                                                                Else
                                                                                    errorCount = 1
                                                                                End If
                                                                            End Using

                                                                            strSQL = "INSERT INTO course(std_ID,Year) values('" & dataStdID & "','" & Now.Year & "')"
                                                                            strRet = oCommon.ExecuteSQL(strSQL)

                                                                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                                                                            Dim level As String = ""

                                                                            If ddlLevel.SelectedValue = "Foundation 1" Or ddlLevel.SelectedValue = "Foundation 2" Or ddlLevel.SelectedValue = "Foundation 3" Then
                                                                                level = "ASAS 1"
                                                                            ElseIf ddlLevel.SelectedValue = "Level 1" Or ddlLevel.SelectedValue = "Level 2" Then
                                                                                level = "TAHAP 1"
                                                                            End If

                                                                            Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & student_Mykad.Text & "'"
                                                                            Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                                                                            Dim find_PPCSDate As String = "select MAX(PPCSDate) from PPCS where StudentID = '" & get_StudentID & "'"
                                                                            Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                                                                            strSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan) 
                                                                                      values('" & get_StudentID & "','" & get_PPCSDate & "','" & ddlYear.SelectedValue & "','" & level & "','N',)"
                                                                            strRet = oCommon.ExecuteSQLPermata(strSQL)

                                                                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                                                                            Dim exist_fatherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent1_IC.Text & "'"
                                                                            Dim data_fatherID As String = oCommon.getFieldValue(exist_fatherID)

                                                                            If data_fatherID = "" Or data_fatherID = "NULL" Or data_fatherID = "0" Then

                                                                                'Validate parent1 input
                                                                                Using PJGDATA As New SqlCommand("INSERT INTO parent_Info(parent_No,parent_Name,parent_IC,parent_MobileNo,parent_Email,
                                                                                                                 parent_Work,parent_Salary) values 
                                                                                                                ('1' ,'" & oCommon.FixSingleQuotes(Parent1_Name.Text.ToUpper) & "','" & Parent1_IC.Text & "',
                                                                                                                '" & Parent1_MobileNo.Text & "','" & Parent1_Email.Text & "','" & Parent1_Work.Text.ToUpper & "',
                                                                                                                '" & ddlsalaryP1.SelectedValue & "')", objConn)
                                                                                    objConn.Open()
                                                                                    Dim k = PJGDATA.ExecuteNonQuery()
                                                                                    objConn.Close()
                                                                                    If k <> 0 Then
                                                                                        errorCount = 0
                                                                                    Else
                                                                                        errorCount = 1
                                                                                    End If
                                                                                End Using

                                                                            End If


                                                                            Dim exist_motherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent2_IC.Text & "'"
                                                                            Dim data_motherID As String = oCommon.getFieldValue(exist_motherID)

                                                                            If data_motherID = "" Or data_motherID = "NULL" Or data_motherID = "0" Then

                                                                                Using PJGDATA As New SqlCommand("INSERT INTO parent_Info(parent_No,parent_Name,parent_IC,parent_MobileNo,parent_Email,
                                                                                                                 parent_Work,parent_Salary) values 
                                                                                                                ('2','" & oCommon.FixSingleQuotes(Parent2_Name.Text.ToUpper) & "','" & Parent2_IC.Text & "',
                                                                                                                '" & Parent2_MobileNo.Text & "','" & Parent2_Email.Text & "','" & Parent2_Work.Text.ToUpper & "',
                                                                                                                '" & ddlsalaryP2.SelectedValue & "')", objConn)
                                                                                    objConn.Open()
                                                                                    Dim l = PJGDATA.ExecuteNonQuery()
                                                                                    objConn.Close()
                                                                                    If l <> 0 Then
                                                                                        errorCount = 0
                                                                                    Else
                                                                                        errorCount = 1
                                                                                    End If
                                                                                End Using

                                                                            End If

                                                                            Dim fatherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent1_IC.Text & "'"
                                                                            Dim ExistFatherID As String = oCommon.getFieldValue(fatherID)

                                                                            Dim motherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent2_IC.Text & "'"
                                                                            Dim ExistMotherID As String = oCommon.getFieldValue(motherID)

                                                                            strSQL = "UPDATE student_info set parent_fatherID ='" & ExistFatherID & "',parent_motherID='" & ExistMotherID & "' WHERE std_ID ='" & dataStdID & "'"
                                                                            strRet = oCommon.ExecuteSQL(strSQL)

                                                                        Else
                                                                            ShowMessage(" Unsuccessful Add Student Information ", MessageType.Error)
                                                                        End If
                                                                    End Using

                                                                Else
                                                                    errorCount = validateParent2()
                                                                End If
                                                            Else
                                                                errorCount = validateParent1()
                                                            End If

                                                        Else
                                                            ShowMessage(" Please Select Religion ", MessageType.Error)
                                                        End If

                                                    Else
                                                        ShowMessage(" Please Select Race ", MessageType.Error)
                                                    End If

                                                Else
                                                    ShowMessage(" Please Select Student Year ", MessageType.Error)
                                                End If
                                            Else
                                                ShowMessage(" Please Fill In Student ID ", MessageType.Error)
                                            End If
                                        Else
                                            ShowMessage(" Please Fill In Student Email ", MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage(" Please Select Gender ", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage(" Please Select State ", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Please Fill In City ", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Please Fill In Student Phone No ", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Fill In Student Name ", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Fill In Zip Code ", MessageType.Error)
                End If
            Else
                ShowMessage(" Please Fill In Student NRIC / MYKAD ", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Select Student Level ", MessageType.Error)
        End If

    End Sub

    Public Function getValue(ByVal sqlA_plus As String, ByVal MyConnection As String) As String
        If sqlA_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlA_plus, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Function validateParent1() As Integer

        If Parent1_Name.Text.Length > 0 Then

            If Parent1_IC.Text.Length > 0 And Regex.IsMatch(Parent1_IC.Text, "^[0-9]+$") And Parent1_IC.Text.Length < 14 Then

                If IsNumeric(Parent1_MobileNo.Text) Or Parent1_MobileNo.Text = "" Then

                    If Parent1_Email.Text <> "" Then

                        If Parent1_Work.Text = "" Or Regex.IsMatch(Parent1_Work.Text, "^[A-Za-z ]+$") Then
                            Return 0

                        Else
                            Return 28
                        End If

                    Else
                        Return 24
                    End If

                Else
                    Return 23
                End If

            Else
                Return 21
            End If

        Else
            Return 20
        End If

    End Function

    Private Function validateParent2() As Integer

        If Parent2_Name.Text.Length > 0 Then

            If Parent2_IC.Text.Length > 0 And Regex.IsMatch(Parent2_IC.Text, "^[0-9]+$") And Parent2_IC.Text.Length < 14 Then

                If IsNumeric(Parent2_MobileNo.Text) Or Parent2_MobileNo.Text = "" Then

                    If Parent2_Email.Text <> "" Then

                        If Parent2_Work.Text = "" Or Regex.IsMatch(Parent2_Work.Text, "^[A-Za-z ]+$") Then
                            Return 0

                        Else
                            Return 48
                        End If

                    Else
                        Return 44
                    End If

                Else
                    Return 43
                End If

            Else
                Return 41
            End If

        Else
            Return 40
        End If

    End Function

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/student_info.xlsx")
    End Sub

    Private Sub BtnUploadedStudentOnly_ServerClick(sender As Object, e As EventArgs) Handles BtnUploadedStudentOnly.ServerClick

        Try
            '--upload excel
            If ImportExcel() = True Then

            Else
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

                    If rbtnStudentOnly.Checked = True Then
                        SaveSiteDataStudentOnly(ds)

                    ElseIf rbtnStudentAndCourse.Checked = True Then
                        SaveSiteData(ds)
                    End If

                Else
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
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

                'STUDENT NAME
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_ID")) Then
                    strStudentID = SiteData.Tables(0).Rows(i).Item("Student_ID")
                Else
                    strMsg += " Please Enter Student ID |"
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
            strStudentID = SiteData.Tables(0).Rows(i).Item("Student_ID")
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
                                student_Status = 'Access',
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

    Private Function SaveSiteDataStudentOnly(ByVal SiteData As DataSet) As String

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
            strStudentID = SiteData.Tables(0).Rows(i).Item("Student_ID")
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
                                student_Status = 'Access',
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
        Dim strStudentID As String = ""
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
        Dim strMotherEMail As String = ""
        Dim strMotherPhone As String = ""
        Dim strMotherJob As String = ""
        Dim strMotherSalary As String = ""

    End Sub


    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class