Imports System.Data.SqlClient
Imports System.IO

Public Class parent_studenthomepage
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim getStatus As String = Session("ParentPortal_Status")

                If getStatus = "SI" Then ''student information
                    txtbreadcrum1.Text = "Student Information"
                    StudentInformation.Visible = True
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-info"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"

                    Race_List()
                    Religion_List()
                    State_list()
                    Country_list()
                    LoadPageStudent()

                ElseIf getStatus = "FI" Then ''family information
                    txtbreadcrum1.Text = "Family Information"
                    StudentInformation.Visible = False
                    ParentInformation.Visible = True
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-info"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"

                    Salary_List()

                    LoadPageGuardian()

                ElseIf getStatus = "CI" Then ''course information
                    txtbreadcrum1.Text = "Course Information"
                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = True
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-info"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"

                    Year_List()
                    Semester_List()
                    LoadCurrentYear()

                    strRet = BindData(datRespondent)

                ElseIf getStatus = "COI" Then ''cocurricular information
                    txtbreadcrum1.Text = "Co-Curriculum Information"
                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = True
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-info"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"

                    strRet = BindDataCocurricular(datRespondent_Cocurricular)

                ElseIf getStatus = "EI" Then ''exam information
                    txtbreadcrum1.Text = "Examination Information"
                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = True
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-info"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"

                    Year_Examination_List()
                    LoadExaminationYear()
                    Examination_List()
                    collect_Data()

                    strRet = BindDataExamination(datRespondent_Examination)

                ElseIf getStatus = "HI" Then ''hostel information
                    txtbreadcrum1.Text = "Hostel Information"
                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = True
                    DisciplineInformation.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-info"
                    btnDiscInfo.Attributes("class") = "btn btn-default font"

                    strRet = BindDataHostel(datRespondent_Hostel)

                ElseIf getStatus = "DI" Then ''discipline information 
                    txtbreadcrum1.Text = "Discipline Information"
                    StudentInformation.Visible = False
                    ParentInformation.Visible = False
                    CourseInformation.Visible = False
                    CocurricularInformation.Visible = False
                    ExaminationInformation.Visible = False
                    HostelInformation.Visible = False
                    DisciplineInformation.Visible = True

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"
                    btnCocurInfo.Attributes("class") = "btn btn-default font"
                    btnExamInfo.Attributes("class") = "btn btn-default font"
                    btnHostelInfo.Attributes("class") = "btn btn-default font"
                    btnDiscInfo.Attributes("class") = "btn btn-info"

                    strRet = BindDataDisicpline(datRespondent_Discipline)

                End If

                Check_STDStatus()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Check_STDStatus()

        Dim check_Stream As String = "Select student_Stream from student_info where std_ID = '" & Session("Std_ID") & "' and student_Status = 'Access'"
        Dim get_Stream As String = oCommon.getFieldValue(check_Stream)

        Dim check_Campus As String = "Select student_Campus from student_info where std_ID = '" & Session("Std_ID") & "' and student_Status = 'Access'"
        Dim get_Campus As String = oCommon.getFieldValue(check_Campus)

        If get_Stream = "Temp" Then
            btnCourseInfo.Visible = False
            btnCocurInfo.Visible = False
            btnExamInfo.Visible = False
            btnDiscInfo.Visible = False

        ElseIf get_Stream = "PS" And get_Campus = "APP" Then

            btnHostelInfo.Visible = False
            btnDiscInfo.Visible = False
        End If

    End Sub

    Private Sub btnStudentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnStudentInfo.ServerClick
        Session("ParentPortal_Status") = "SI"
        Response.Redirect("penjaga_login_berjaya.aspx")
    End Sub

    Private Sub btnParentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnParentInfo.ServerClick
        Session("ParentPortal_Status") = "FI"
        Response.Redirect("penjaga_login_berjaya.aspx")
    End Sub

    Private Sub btnCourseInfo_ServerClick(sender As Object, e As EventArgs) Handles btnCourseInfo.ServerClick
        Session("ParentPortal_Status") = "CI"
        Response.Redirect("penjaga_login_berjaya.aspx")
    End Sub

    Private Sub btnCocurInfo_ServerClick(sender As Object, e As EventArgs) Handles btnCocurInfo.ServerClick
        Session("ParentPortal_Status") = "COI"
        Response.Redirect("penjaga_login_berjaya.aspx")
    End Sub

    Private Sub btnExamInfo_ServerClick(sender As Object, e As EventArgs) Handles btnExamInfo.ServerClick
        Session("ParentPortal_Status") = "EI"
        Response.Redirect("penjaga_login_berjaya.aspx")
    End Sub

    Private Sub btnHostelInfo_ServerClick(sender As Object, e As EventArgs) Handles btnHostelInfo.ServerClick
        Session("ParentPortal_Status") = "HI"
        Response.Redirect("penjaga_login_berjaya.aspx")
    End Sub

    Private Sub btnDiscInfo_ServerClick(sender As Object, e As EventArgs) Handles btnDiscInfo.ServerClick
        Session("ParentPortal_Status") = "DI"
        Response.Redirect("penjaga_login_berjaya.aspx")
    End Sub

    Private Sub Race_List()
        strSQL = "SELECT UPPER(Parameter) Parameter, Value from setting where Type = 'Race' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRace.DataSource = ds
            ddlRace.DataTextField = "Parameter"
            ddlRace.DataValueField = "Value"
            ddlRace.DataBind()
            ddlRace.Items.Insert(0, New ListItem("Select Race", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Religion_List()
        strSQL = "SELECT UPPER(Parameter) Parameter, Value from setting where Type = 'Religion' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligion.DataSource = ds
            ddlReligion.DataTextField = "Parameter"
            ddlReligion.DataValueField = "Value"
            ddlReligion.DataBind()
            ddlReligion.Items.Insert(0, New ListItem("Select Religion", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub State_list()
        strSQL = "SELECT UPPER(Parameter) Parameter, Value FROM setting WHERE Type='State' order by Parameter asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "Parameter"
            ddlState.DataValueField = "Value"
            ddlState.DataBind()
            ddlState.Items.Insert(0, New ListItem("Select State", String.Empty))

            ddlStateOfBirth.DataSource = ds
            ddlStateOfBirth.DataTextField = "Parameter"
            ddlStateOfBirth.DataValueField = "Value"
            ddlStateOfBirth.DataBind()
            ddlStateOfBirth.Items.Insert(0, New ListItem("Select State", String.Empty))
            ddlStateOfBirth.Items.Insert(1, New ListItem("OTHERS", "OTHERS"))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Country_list()

        strSQL = ""

        If ddlStateOfBirth.SelectedValue = "OTHERS" Then
            strSQL = "SELECT UPPER(Parameter) Parameter, Value FROM setting WHERE Type='Country' and idx = 'Address'  order by Parameter asc"
        Else
            strSQL = "SELECT UPPER(Parameter) Parameter, Value FROM setting WHERE Type='Country' and idx = 'Address' and Value = 'Malaysia' order by Parameter asc "
        End If


        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCountryOfBirth.DataSource = ds
            ddlCountryOfBirth.DataTextField = "Parameter"
            ddlCountryOfBirth.DataValueField = "Value"
            ddlCountryOfBirth.DataBind()
            ddlCountryOfBirth.Items.Insert(0, New ListItem("Select Country", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlStateOfBirth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStateOfBirth.SelectedIndexChanged
        Try

            If ddlStateOfBirth.SelectedValue = "OTHERS" Then
                Country_list()
            Else
                ddlCountryOfBirth.SelectedValue = "Malaysia"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadPageStudent()

        ''student_info
        strSQL = "  SELECT UPPER(student_Name) student_Name, UPPER(Student_Email) student_Email, UPPER(student_Sex) student_Sex, UPPER(student_Address) student_Address, UPPER(student_City) student_City, UPPER(student_Level) student_Level, UPPER(student_Sem) student_Sem, student_CountryOfBirth,
                    student_info.student_ID, student_Mykad, student_FonNo, student_Level.year, student_PostalCode, student_Photo, student_Race, student_State, student_StateOfBirth, UPPER(student_Stream) student_Stream, UPPER(student_Campus) student_Campus, student_Religion FROM student_info 
                    LEFT JOIN student_Level ON student_info.std_ID=student_Level.std_ID 
                    WHERE student_info.std_ID ='" & Session("Std_ID") & "'
                    AND student_Level.ID = (SELECT MAX(ID) FROM student_Level C where C.std_ID ='" & Session("Std_ID") & "')"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                txtstudentName.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                txtstudentName.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                txtstudentID.Text = ds.Tables(0).Rows(0).Item("student_ID")
            Else
                txtstudentID.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Mykad")) Then
                txtstudentMykad.Text = ds.Tables(0).Rows(0).Item("student_Mykad")
            Else
                txtstudentMykad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Email")) Then
                txtstudentEmail.Text = ds.Tables(0).Rows(0).Item("student_Email")
            Else
                txtstudentEmail.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sex")) Then
                Dim staff_gender As String = ds.Tables(0).Rows(0).Item("student_Sex")

                If staff_gender = "Male" Or staff_gender = "MALE" Or staff_gender = "Lelaki" Or staff_gender = "LELAKI" Then
                    rbtn_Male.Checked = True
                ElseIf staff_gender = "Female" Or staff_gender = "FEMALE" Or staff_gender = "Perempuan" Or staff_gender = "PEREMPUAN" Then
                    rbtn_Female.Checked = True
                End If
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_FonNo")) Then
                txtstudentPhone.Text = ds.Tables(0).Rows(0).Item("student_FonNo")
            Else
                txtstudentPhone.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Address")) Then
                txtstudentAddress.Text = ds.Tables(0).Rows(0).Item("student_Address")
            Else
                txtstudentAddress.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_City")) Then
                txtstudentCity.Text = ds.Tables(0).Rows(0).Item("student_City")
            Else
                txtstudentCity.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_State")) Then
                ddlState.SelectedValue = ds.Tables(0).Rows(0).Item("student_State")
            Else
                ddlState.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_StateOfBirth")) Then
                ddlStateOfBirth.SelectedValue = ds.Tables(0).Rows(0).Item("student_StateOfBirth")
            Else
                ddlStateOfBirth.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_CountryOfBirth")) Then
                ddlCountryOfBirth.SelectedValue = ds.Tables(0).Rows(0).Item("student_CountryOfBirth")
            Else

                If ddlStateOfBirth.SelectedValue <> "OTHERS" Then
                    ddlCountryOfBirth.SelectedValue = "Malaysia"
                Else
                    ddlCountryOfBirth.SelectedValue = ""
                End If

            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_PostalCode")) Then
                txtstudentPostcode.Text = ds.Tables(0).Rows(0).Item("student_PostalCode")
            Else
                txtstudentPostcode.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Level")) Then
                txtstudentLevel.Text = ds.Tables(0).Rows(0).Item("student_Level")
            Else
                txtstudentLevel.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                txtstudentYear.Text = ds.Tables(0).Rows(0).Item("year")
            Else
                txtstudentYear.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sem")) Then
                txtstudentSem.Text = ds.Tables(0).Rows(0).Item("student_Sem")
            Else
                txtstudentSem.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Race")) Then
                ddlRace.SelectedValue = ds.Tables(0).Rows(0).Item("student_Race")
            Else
                ddlRace.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Religion")) Then
                ddlReligion.SelectedValue = ds.Tables(0).Rows(0).Item("student_Religion")
            Else
                ddlReligion.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Photo")) Then
                student_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("student_Photo")
            Else
                student_Photo.ImageUrl = "~/student_Image/user.png"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Stream")) Then
                Dim dataProgram As String = ds.Tables(0).Rows(0).Item("student_Stream")

                If dataProgram = "PS" Then
                    txtStream.Text = "PURE SCIENCE"
                ElseIf dataProgram = "DIP" Then
                    txtStream.Text = "DIGITAL INNOVATORS PROGRAM"
                End If
            Else
                txtStream.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Campus")) Then
                Dim dataProgram As String = ds.Tables(0).Rows(0).Item("student_Campus")

                If dataProgram = "PGPN" Then
                    txtCampus.Text = "PUSAT GENIUS@PINTAR NEGARA, UKM"
                ElseIf dataProgram = "APP" Then
                    txtCampus.Text = "AKADEMIK PINTAR PENDANG"
                End If
            Else
                txtCampus.Text = ""
            End If

        End If
    End Sub

    Private Sub btnUpdateStudentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStudentInfo.ServerClick
        Dim data_ID As String = Session("Std_ID")

        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        If IsNumeric(txtstudentMykad.Text) And txtstudentMykad.Text <> "" And txtstudentMykad.Text.Length < 14 Then

            If txtstudentPostcode.Text = "" Or IsNumeric(txtstudentPostcode.Text) Then

                If txtstudentName.Text <> "" And Not IsNothing(txtstudentName.Text) Then

                    If txtstudentPhone.Text = "" Or txtstudentPhone.Text.Length > 0 Then

                        If txtstudentCity.Text = "" Or txtstudentCity.Text.Length > 0 Then

                            If strgender <> "" Then

                                If txtstudentEmail.Text = "" Or Regex.IsMatch(txtstudentEmail.Text, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$") Then

                                    Dim imgPath As String = student_Photo.ImageUrl

                                    If uploadPhoto.PostedFile.FileName <> "" Then

                                        Dim filename As String = txtstudentMykad.Text & "_" & Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                        imgPath = "~/student_Image/" + filename
                                        uploadPhoto.SaveAs(Server.MapPath(imgPath))
                                    End If

                                    'UPDATE STUDENT DATA
                                    strSQL = "  UPDATE student_info set student_Mykad='" & txtstudentMykad.Text & "',
                                                student_Sex='" & strgender & "',student_Name='" & oCommon.FixSingleQuotes(txtstudentName.Text.ToUpper) & "',student_Email='" & txtstudentEmail.Text & "',
                                                student_FonNo='" & txtstudentPhone.Text & "',student_Address='" & txtstudentAddress.Text.ToUpper & "', student_CountryOfBirth = '" & ddlCountryOfBirth.SelectedValue & "',
                                                student_City='" & txtstudentCity.Text & "',student_State='" & ddlState.SelectedValue & "',student_StateOfBirth='" & ddlStateOfBirth.SelectedValue & "',student_Race='" & ddlRace.SelectedValue & "',student_Religion='" & ddlReligion.SelectedValue & "',
                                                student_PostalCode='" & txtstudentPostcode.Text & "',student_Photo = '" & imgPath & "' where std_ID = '" & data_ID & "' "
                                    strRet = oCommon.ExecuteSQL(strSQL)

                                    If strRet = "0" Then

                                        ShowMessage(" Update Student Data ", MessageType.Success)

                                        Dim host As String = Net.Dns.GetHostName()

                                        Dim std_Name As String = "Select student_Name from student_info where std_ID = '" & data_ID & "'"
                                        Dim data_stdName As String = oCommon.getFieldValue(std_Name)

                                        'Insert activity trail image into ActivityTrail_BtmLvl DB
                                        Using PJGDATA As New SqlCommand("INSERT into ActivityTrail_BtmLvl(Log_Date,Activity,Login_ID,User_HostAddress,Page,Name_Matters) 
                                                     values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','Update Student Data','" & data_ID & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "','pelajar_update_profile.aspx','" & oCommon.FixSingleQuotes(data_stdName) & "')", objConn)
                                            objConn.Open()
                                            Dim k = PJGDATA.ExecuteNonQuery()
                                            objConn.Close()
                                        End Using

                                    Else
                                        ShowMessage(" Update Student Data", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage(" Invalid Email Format", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Invalid Gender Information", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Invalid City Information", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Invalid Student PHone", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Invalid Student Name", MessageType.Error)
                End If
            Else
                ShowMessage(" Invalid Postal Code", MessageType.Error)
            End If
        Else
            ShowMessage(" Invalid Student MYKAD", MessageType.Error)
        End If
    End Sub

    Private Sub Salary_List()
        strSQL = "SELECT * from setting where type = 'salary'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSalaryOne.DataSource = ds
            ddlSalaryOne.DataTextField = "Parameter"
            ddlSalaryOne.DataValueField = "Parameter"
            ddlSalaryOne.DataBind()
            ddlSalaryOne.Items.Insert(0, New ListItem("Select Salary", String.Empty))

            ddlSalaryTwo.DataSource = ds
            ddlSalaryTwo.DataTextField = "Parameter"
            ddlSalaryTwo.DataValueField = "Parameter"
            ddlSalaryTwo.DataBind()
            ddlSalaryTwo.Items.Insert(0, New ListItem("Select Salary", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LoadPageGuardian()

        strSQL = "select parent_No from parent_info where parent_ID = '" & oCommon.Student_securityLogin(Session("Parent_ID")) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim parentOne As String = ""
        Dim parentTwo As String = ""
        Dim leftParentOne As String = ""
        Dim leftParentTwo As String = ""

        If strRet = "1" Then
            parentOne = " and parent_info.parent_No = '1' "
            parentTwo = " and parent_info.parent_No = '2' "
            leftParentOne = " student_info.parent_fatherID "
            leftParentTwo = " student_info.parent_motherID "

        ElseIf strRet = "2" Then
            parentOne = " and parent_info.parent_No = '2' "
            parentTwo = " and parent_info.parent_No = '1' "
            leftParentOne = " student_info.parent_motherID "
            leftParentTwo = " student_info.parent_fatherID "

        End If

        Dim data_ID As String = Session("Std_ID")

        ''Guardian 1 Data
        strSQL = "  Select UPPER(parent_Name) parent_Name, parent_IC, parent_MobileNo, UPPER(parent_Email) parent_Email, UPPER(parent_Status) parent_Status,
                    UPPER(parent_Work) parent_Work, parent_Salary, parent_Condition from parent_Info 
                    Left Join student_info ON parent_Info.parent_ID = " & leftParentOne & "
                    WHERE student_info.std_ID ='" & data_ID & "' " & parentOne

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Name")) Then
                txtGuardianNameOne.Text = ds.Tables(0).Rows(0).Item("parent_Name")
            Else
                txtGuardianNameOne.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_IC")) Then
                txtMykadNumberOne.Text = ds.Tables(0).Rows(0).Item("parent_IC")
            Else
                txtMykadNumberOne.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                txtPhoneNoOne.Text = ds.Tables(0).Rows(0).Item("parent_MobileNo")
            Else
                txtPhoneNoOne.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Email")) Then
                Parent1_Email.Text = ds.Tables(0).Rows(0).Item("parent_Email")
            Else
                Parent1_Email.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Status")) Then
                txtRelationshipOne.Text = ds.Tables(0).Rows(0).Item("parent_Status")
            Else
                txtRelationshipOne.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Work")) Then
                txtJobOne.Text = ds.Tables(0).Rows(0).Item("parent_Work")
            Else
                txtJobOne.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Salary")) Then
                ddlSalaryOne.SelectedValue = ds.Tables(0).Rows(0).Item("parent_Salary")
            Else
                ddlSalaryOne.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Condition")) Then
                Dim P1_RStatus_Collect As String = ds.Tables(0).Rows(0).Item("parent_Condition")

                If P1_RStatus_Collect = "Alive" Then
                    Parent1_RadioStatusAlive.Checked = True
                ElseIf P1_RStatus_Collect = "PAssed Away" Then
                    Parent1_RadioStatusPassAway.Checked = True
                Else
                    Parent1_RadioStatusAlive.Checked = False
                    Parent1_RadioStatusPassAway.Checked = False
                End If
            Else
                Parent1_RadioStatusAlive.Checked = False
                Parent1_RadioStatusPassAway.Checked = False
            End If

        End If

        ''Guardian 2 Data
        strSQL = "  Select UPPER(parent_Name) parent_Name, parent_IC, parent_MobileNo, UPPER(parent_Email) parent_Email, UPPER(parent_Status) parent_Status,
                    UPPER(parent_Work) parent_Work, parent_Salary, parent_Condition from parent_Info 
                    Left Join student_info ON parent_Info.parent_ID = " & leftParentTwo & "
                    WHERE student_info.std_ID ='" & data_ID & "' " & parentTwo

        Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

        Dim dT As DataSet = New DataSet
        sqlDB.Fill(dT, "AnyTable")

        Dim nRowt As Integer = 0
        Dim MyTables As DataTable = New DataTable
        MyTables = dT.Tables(0)
        If MyTables.Rows.Count > 0 Then
            If Not IsDBNull(dT.Tables(0).Rows(0).Item("parent_Name")) Then
                txtGuardianNameTwo.Text = dT.Tables(0).Rows(0).Item("parent_Name")
            Else
                txtGuardianNameTwo.Text = ""
            End If

            If Not IsDBNull(dT.Tables(0).Rows(0).Item("parent_IC")) Then
                txtMykadNumberTwo.Text = dT.Tables(0).Rows(0).Item("parent_IC")
            Else
                txtMykadNumberTwo.Text = ""
            End If

            If Not IsDBNull(dT.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                txtPhoneNoTwo.Text = dT.Tables(0).Rows(0).Item("parent_MobileNo")
            Else
                txtPhoneNoTwo.Text = ""
            End If

            If Not IsDBNull(dT.Tables(0).Rows(0).Item("parent_Email")) Then
                Parent2_Email.Text = dT.Tables(0).Rows(0).Item("parent_Email")
            Else
                Parent2_Email.Text = ""
            End If

            If Not IsDBNull(dT.Tables(0).Rows(0).Item("parent_Status")) Then
                txtRelationshipTwo.Text = dT.Tables(0).Rows(0).Item("parent_Status")
            Else
                txtRelationshipTwo.Text = ""
            End If

            If Not IsDBNull(dT.Tables(0).Rows(0).Item("parent_Work")) Then
                txtJobTwo.Text = dT.Tables(0).Rows(0).Item("parent_Work")
            Else
                txtJobTwo.Text = ""
            End If

            If Not IsDBNull(dT.Tables(0).Rows(0).Item("parent_Salary")) Then
                ddlSalaryTwo.SelectedValue = dT.Tables(0).Rows(0).Item("parent_Salary")
            Else
                ddlSalaryTwo.SelectedIndex = 0
            End If

            If Not IsDBNull(dT.Tables(0).Rows(0).Item("parent_Condition")) Then
                Dim P2_RStatus_Collect As String = dT.Tables(0).Rows(0).Item("parent_Condition")

                If P2_RStatus_Collect = "Alive" Then
                    Parent2_RadioStatusAlive.Checked = True
                ElseIf P2_RStatus_Collect = "PAssed Away" Then
                    Parent2_RadioStatusPassAway.Checked = True
                Else
                    Parent2_RadioStatusAlive.Checked = False
                    Parent2_RadioStatusPassAway.Checked = False
                End If
            Else
                Parent2_RadioStatusAlive.Checked = False
                Parent2_RadioStatusPassAway.Checked = False
            End If

        End If
    End Sub

    Private Sub btnUpdateParentInformation_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateParentInformation.ServerClick

        Dim P1_RStatus As String = ""

        If Parent1_RadioStatusAlive.Checked = True Then
            P1_RStatus = "Alive"
        ElseIf Parent1_RadioStatusPassAway.Checked = True Then
            P1_RStatus = "Passed Away"
        End If

        'UPDATE STUDENT DATA
        strSQL = "  UPDATE parent_info set parent_IC ='" & txtMykadNumberOne.Text & "',
                    parent_Name ='" & oCommon.FixSingleQuotes(txtGuardianNameOne.Text) & "',
                    parent_MobileNo ='" & txtPhoneNoOne.Text & "',
                    parent_Status ='" & txtRelationshipOne.Text & "',
                    parent_Work ='" & txtJobOne.Text & "',
                    parent_Email = '" & Parent1_Email.Text & "',
                    parent_Condition ='" & P1_RStatus & "',
                    parent_Salary ='" & ddlSalaryOne.SelectedValue & "',
                    parent_Password ='" & txtMykadNumberOne.Text & "'
                    where parent_ID = '" & oCommon.Student_securityLogin(Session("Parent_ID")) & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage(" Update Guardian Information ", MessageType.Success)
        Else
            ShowMessage(" Unable Update Guardian Information ", MessageType.Error)
        End If

    End Sub

    Private Sub Year_List()
        strSQL = "select distinct year from student_level where std_ID = '" & Session("Std_ID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "year"
            ddlYear.DataValueField = "year"
            ddlYear.DataBind()

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Semester_List()
        strSQL = "select * from setting where type = 'sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemesster.DataSource = ds
            ddlSemesster.DataTextField = "Parameter"
            ddlSemesster.DataValueField = "Value"
            ddlSemesster.DataBind()

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LoadCurrentYear()
        strSQL = "select Max(Parameter) as year from setting where type = 'year'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
                ddlSemesster.SelectedIndex = 0
            Else
                ddlYear.SelectedValue = 0
                ddlSemesster.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQL() As String

        Dim data_ID As String = Session("Std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_Name ASC"

        tmpSQL = "Select * From course left join subject_info on course.subject_ID=subject_info.subject_ID left join class_info on course.class_ID=class_info.class_ID left join student_info on course.std_ID=student_info.std_ID"
        strWhere = " WHERE course.std_ID = '" & data_ID & "' And student_info.student_Status = 'Access' and student_info.student_ID is not null and student_info.student_ID <> '' and student_info.student_ID like '%M%'"
        strWhere += " AND course.year = '" & ddlYear.SelectedValue & "'"
        strWhere += " AND subject_info.subject_Sem = '" & ddlSemesster.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQL
    End Function

    Private Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlSemesster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemesster.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataCocurricular(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLCocurricular, strConnPermata)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLCocurricular() As String

        Dim data_ID As String = Session("Std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY koko_pelajar.Tahun ASC"

        tmpSQL = "SELECT distinct A.std_ID, koko_pelajar.Tahun, koko_kelas.Kelas,
                 (SELECT UPPER(Nama) Nama FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,
                 (SELECT UPPER(Nama) Nama FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,
                 (SELECT UPPER(Nama) Nama FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,
                 (SELECT UPPER(Nama) Nama FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan
                 FROM koko_pelajar
                 LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                 LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                 LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                 LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"
        strWhere = " WHERE A.student_Status = 'Access' and A.student_ID is not null and A.student_ID <> '' and A.student_ID like '%M%' AND A.std_ID = '" & data_ID & "'"

        getSQLCocurricular = tmpSQL & strWhere & strOrderby

        Return getSQLCocurricular
    End Function

    Private Sub Year_Examination_List()
        strSQL = "  select distinct student_Level.year from student_Level
                    left join student_info on student_Level.std_ID = student_info.std_ID
                    where student_info.std_ID = '" + Session("Std_ID") + "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYearExam.DataSource = ds
            ddlYearExam.DataTextField = "year"
            ddlYearExam.DataValueField = "year"
            ddlYearExam.DataBind()
            ddlYearExam.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Examination_List()

        Dim get_Check As String = ""

        If Session("Student_Campus") = "PGPN" Then
            get_Check = "select Value from setting where Type  = 'Exam Result PGPN'"
        ElseIf Session("Student_Campus") = "APP" Then
            get_Check = "select Value from setting where Type  = 'Exam Result APP'"
        End If

        Dim data_Check As String = oCommon.getFieldValue(get_Check)

        Dim data_Exam As String = ""

        If data_Check = "off" Or data_Check = "Off" Or data_Check = "OFF" Then

            ''show all student exam result except the current exam
            data_Exam = "   select max(exam_result.exam_ID) from exam_result
                            left join exam_info on exam_result.exam_ID = exam_Info.exam_ID
                            left join course on exam_result.course_ID = course.course_ID
                            where course.year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            strRet = oCommon.getFieldValue(data_Exam)

            If ddlYearExam.SelectedIndex > 0 And ddlYearExam.SelectedValue = Now.Year Then
                strSQL = "  select distinct exam_info.exam_Name from exam_result left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                            left join course on exam_result.course_ID = course.course_ID left join student_info on course.std_ID = student_info.std_ID
                            where student_info.student_Status = 'Access' and course.year = '" & ddlYearExam.SelectedValue & "' and exam_info.exam_Year = '" & ddlYearExam.SelectedValue & "' 
                            and exam_info.exam_ID < '" & strRet & "' and student_info.std_ID = '" & Session("Std_ID") & "'"
            Else
                strSQL = "  select distinct exam_info.exam_Name from exam_result left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                            left join course on exam_result.course_ID = course.course_ID left join student_info on course.std_ID = student_info.std_ID
                            where student_info.student_Status = 'Access' and course.year = '" & ddlYearExam.SelectedValue & "' and exam_info.exam_Year = '" & ddlYearExam.SelectedValue & "' 
                            and exam_info.exam_ID <= '" & strRet & "' and student_info.std_ID = '" & Session("Std_ID") & "'"
            End If

        ElseIf data_Check = "on" Or data_Check = "On" Or data_Check = "ON" Then

            ''show all student exam result
            strSQL = "  select distinct exam_info.exam_Name from exam_result left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                        left join course on exam_result.course_ID = course.course_ID left join student_info on course.std_ID = student_info.std_ID
                        where student_info.student_Status = 'Access' and course.year = '" & ddlYearExam.SelectedValue & "' and exam_info.exam_Year = '" & ddlYearExam.SelectedValue & "'
                        and student_info.std_ID = '" & Session("Std_ID") & "'"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamination.DataSource = ds
            ddlExamination.DataTextField = "exam_Name"
            ddlExamination.DataValueField = "exam_Name"
            ddlExamination.DataBind()
            ddlExamination.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LoadExaminationYear()
        strSQL = "select Max(Parameter) as year from setting where type = 'year'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlYearExam.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlYearExam.SelectedValue = Now.Year
            End If
        End If
    End Sub

    Private Function BindDataExamination(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLExamination, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLExamination() As String

        Dim get_Check As String = ""

        If Session("Student_Campus") = "PGPN" Then
            get_Check = "select Value from setting where Type  = 'Exam Result PGPN'"
        ElseIf Session("Student_Campus") = "APP" Then
            get_Check = "select Value from setting where Type  = 'Exam Result APP'"
        End If

        Dim data_Check As String = oCommon.getFieldValue(get_Check)

        getSQLExamination = ""

        If data_Check = "On" Or data_Check = "on" Or data_Check = "ON" Then

            Dim data_ID As String = Session("Std_ID")

            Dim tmpSQL As String
            Dim strWhere As String = ""
            Dim strOrderby As String = " ORDER BY subject_Name ASC"

            tmpSQL = "  select exam_result.ID,exam_info.exam_Year,exam_info.exam_Name,subject_Name,subject_code,grade,gpa from course
                    left join subject_info on course.subject_ID=subject_info.subject_ID
                    left join exam_result on course.course_ID=exam_result.course_ID
                    left join exam_Info on exam_result.exam_Id=exam_Info.exam_ID
                    left join grade_info on exam_result.grade=grade_info.grade_Name"

            strWhere = "    where course.std_ID = '" & data_ID & "' and course_Name <> 'Portfolio' and course_Name <> 'Penyelidikan' and course_Name <> 'Pembangunan Kendiri' and course_Name <> 'Jati Diri'"
            strWhere += "   AND exam_Info.exam_Year = '" & ddlYearExam.SelectedValue & "'"
            strWhere += "   AND exam_Info.exam_Name = '" & ddlExamination.SelectedValue & "'"

            getSQLExamination = tmpSQL & strWhere & strOrderby

        End If

        Return getSQLExamination
    End Function

    Private Sub ddlYearExam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYearExam.SelectedIndexChanged
        Try
            Examination_List()
            collect_Data()
            strRet = BindDataExamination(datRespondent_Examination)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlExamination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamination.SelectedIndexChanged
        Try
            collect_Data()
            strRet = BindDataExamination(datRespondent_Examination)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub collect_Data()

        Dim get_Check As String = ""

        If Session("Student_Campus") = "PGPN" Then
            get_Check = "select Value from setting where Type  = 'Exam Result PGPN'"
        ElseIf Session("Student_Campus") = "APP" Then
            get_Check = "select Value from setting where Type  = 'Exam Result APP'"
        End If

        Dim get_ExamStatus As String = oCommon.getFieldValue(get_Check)

        If get_ExamStatus = "On" Or get_ExamStatus = "on" Or get_ExamStatus = "ON" Then

            txt_academic_point.Text = ""
            txt_cocurricular_grade.Text = ""
            txt_cocurricular_point.Text = ""
            txt_portfolio_grade.Text = ""
            txt_portfolio_point.Text = ""
            txt_research_point.Text = ""
            txt_research_grade.Text = ""
            txt_sd_grade.Text = ""
            txt_sd_point.Text = ""
            txt_pd_grade.Text = ""
            txt_pd_point.Text = ""
            txt_pd_gpa.Text = ""
            txt_pd_cgpa.Text = ""

            txt_academic_point.Font.Bold = True
            txt_cocurricular_grade.Font.Bold = True
            txt_cocurricular_point.Font.Bold = True
            txt_portfolio_grade.Font.Bold = True
            txt_portfolio_point.Font.Bold = True
            txt_research_point.Font.Bold = True
            txt_research_grade.Font.Bold = True
            txt_sd_grade.Font.Bold = True
            txt_sd_point.Font.Bold = True
            txt_pd_grade.Font.Bold = True
            txt_pd_point.Font.Bold = True
            txt_pd_gpa.Font.Bold = True
            txt_pd_cgpa.Font.Bold = True

            ''get Student Level
            strSQL = "select distinct student_level from student_level where year = '" & ddlYearExam.SelectedValue & "' and std_ID = '" & Session("Std_ID") & "'"
            Dim get_StudentLevel As String = oCommon.getFieldValue(strSQL)

            If get_StudentLevel <> "Level 1" And get_StudentLevel <> "Level 2" Then
                row_pd.Visible = False
                row_sd.Visible = True
            ElseIf get_StudentLevel = "Level 1" Or get_StudentLevel = "Level 2" Then
                row_pd.Visible = True
                row_sd.Visible = False
            End If

            'get englih literture on / off
            Dim check_Eng_Literature As String = "select Value from setting where Type = 'English Literature'"
            Dim Confirm_Eng_Literature As String = oCommon.getFieldValue(check_Eng_Literature)

            'get Portfolio percentage on / off
            Dim check_portfolio_percen As String = "select stat_portfolio from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Portfolio As String = oCommon.getFieldValue(check_portfolio_percen)

            ''get cocuricullum percentage on / off
            Dim check_cocuricullum_percen As String = "select stat_kokurikulum from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Cocuricullum As String = oCommon.getFieldValue(check_cocuricullum_percen)

            ''get research percentage on / off
            Dim check_research_percen As String = "select stat_penyelidikan from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Research As String = oCommon.getFieldValue(check_research_percen)

            ''get self development percentage on / off
            Dim check_self_percen As String = "select stat_kendiri from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Self As String = oCommon.getFieldValue(check_self_percen)

            ''print Portfolio
            If Confirm_Portfolio = "On" Then
                strSQL = "SELECT grade FROM [ExamSlip_Portfolio] where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                txt_portfolio_grade.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT gpa FROM [ExamSlip_Portfolio] where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                txt_portfolio_point.Text = oCommon.getFieldValue(strSQL)
            Else
                txt_portfolio_grade.Text = "SM"
                txt_portfolio_point.Text = "SM"
            End If

            ''print research 
            If Confirm_Research = "On" Then
                strSQL = "SELECT grade FROM [ExamSlip_Research] where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                txt_research_grade.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT gpa FROM [ExamSlip_Research] where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                txt_research_point.Text = oCommon.getFieldValue(strSQL)
            Else
                txt_research_grade.Text = "SM"
                txt_research_point.Text = "SM"
            End If

            ''print self development
            If Confirm_Self = "On" Then

                If get_StudentLevel <> "Level 1" And get_StudentLevel <> "Level 2" Then
                    strSQL = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                    txt_sd_grade.Text = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                    txt_sd_point.Text = oCommon.getFieldValue(strSQL)

                    ''Get CGPA on SD Row
                    strSQL = "SELECT png from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "' and student_type = 'ASAS'"
                    txt_pd_gpa.Text = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT pngs from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "' and student_type = 'ASAS'"
                    txt_pd_cgpa.Text = oCommon.getFieldValue(strSQL)

                ElseIf get_StudentLevel = "Level 1" Or get_StudentLevel = "Level 2" Then
                    strSQL = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                    txt_pd_grade.Text = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                    txt_pd_point.Text = oCommon.getFieldValue(strSQL)

                    ''Get CGPA on PD Row
                    strSQL = "SELECT png from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "' and student_type = 'TAHAP'"
                    txt_pd_gpa.Text = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT pngs from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "' and student_type = 'TAHAP'"
                    txt_pd_cgpa.Text = oCommon.getFieldValue(strSQL)
                End If

            Else
                txt_sd_grade.Text = "SM"
                txt_sd_point.Text = "SM"
                txt_pd_grade.Text = "SM"
                txt_pd_point.Text = "SM"
            End If

            '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
            If Confirm_Cocuricullum = "On" Then

                Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & Session("Std_ID") & "'"
                Dim getStudent As String = oCommon.getFieldValue(studentData)

                If ddlExamination.SelectedValue = "Exam 2" Or ddlExamination.SelectedValue = "Exam 6" Or ddlExamination.SelectedValue = "Exam 10" Then

                    strSQL = "  select koko_pelajar.PNGP1 from koko_pelajar
                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                    txt_cocurricular_point.Text = oCommon.getFieldValuePermata(strSQL)

                    strSQL = "  select koko_pelajar.GredP1 from koko_pelajar
                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                    txt_cocurricular_grade.Text = oCommon.getFieldValuePermata(strSQL)

                ElseIf ddlExamination.SelectedValue = "Exam 4" Or ddlExamination.SelectedValue = "Exam 7" Or ddlExamination.SelectedValue = "Exam 8" Or ddlExamination.SelectedValue = "Exam 12" Then

                    strSQL = "  select koko_pelajar.PNGP2 from koko_pelajar
                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                    txt_cocurricular_point.Text = oCommon.getFieldValuePermata(strSQL)

                    strSQL = "  select koko_pelajar.GredP2 from koko_pelajar
                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"
                    txt_cocurricular_grade.Text = oCommon.getFieldValuePermata(strSQL)
                End If
            Else
                txt_cocurricular_point.Text = "SM"
                txt_cocurricular_grade.Text = "SM"
            End If

            ''print academic
            strSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
            Dim total_Credit As String = oCommon.getFieldValue(strSQL)

            strSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
            Dim total_Total As String = oCommon.getFieldValue(strSQL)

            Dim total_Credit_EL As String = "0"
            Dim total_Total_EL As String = "0"

            ''print academic english literature
            If Confirm_Eng_Literature = "On" Then

                strSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                total_Credit_EL = oCommon.getFieldValue(strSQL)

                If total_Credit_EL.Length = 0 Then
                    total_Credit_EL = "0"
                End If

                strSQL = "select total FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                total_Total_EL = oCommon.getFieldValue(strSQL)

                If total_Total_EL.Length = 0 Then
                    total_Total_EL = "0"
                End If
            End If

            Dim Number1 As Double = Double.Parse(total_Credit)
            Dim Number2 As Double = Double.Parse(total_Credit_EL)
            Dim Number3 As Double = Double.Parse(total_Total)
            Dim Number4 As Double = Double.Parse(total_Total_EL)

            Dim total_Hour As Double = Number1 + Number2
            Dim final_Total As Double = Number3 + Number4

            txt_academic_point.Text = Math.Round(final_Total / total_Hour, 2)

        Else

            row_pd.Visible = False

            txt_academic_point.Text = ""
            txt_cocurricular_grade.Text = ""
            txt_cocurricular_point.Text = ""
            txt_portfolio_grade.Text = ""
            txt_portfolio_point.Text = ""
            txt_research_point.Text = ""
            txt_research_grade.Text = ""
            txt_sd_grade.Text = ""
            txt_sd_point.Text = ""
            txt_pd_grade.Text = ""
            txt_pd_point.Text = ""
            txt_pd_gpa.Text = ""
            txt_pd_cgpa.Text = "The Examination Is Closed"

        End If

    End Sub

    Private Sub btnPrintMalay_ServerClick(sender As Object, e As EventArgs) Handles btnPrintMalay.ServerClick

        Dim get_Check As String = ""

        If Session("Student_Campus") = "PGPN" Then
            get_Check = "select Value from setting where Type  = 'Exam Result PGPN'"
        ElseIf Session("Student_Campus") = "APP" Then
            get_Check = "select Value from setting where Type  = 'Exam Result APP'"
        End If

        Dim get_ExamStatus As String = oCommon.getFieldValue(get_Check)

        If get_ExamStatus = "On" Or get_ExamStatus = "on" Or get_ExamStatus = "ON" Then

            Dim tmpSQL As String = ""
            Dim tmpSQL_Nama As String = ""
            Dim tmpSQL_Kod As String = ""
            Dim tmpSQL_Gred As String = ""
            Dim tmpSQL_PNG As String = ""
            Dim tmpSQL_Hour As String = ""
            Dim tmpSQL_Total As String = ""

            Dim tmpSQL_SD_GRED As String = ""
            Dim tmpsql_SD_PNG As String = ""
            Dim tmpsql_SD_KOD As String = ""

            Dim tmpsql_Portfolio_GRED As String = ""
            Dim tmpsql_Portfolio_PNG As String = ""
            Dim tmpsql_Portfolio_KOD As String = ""

            Dim tmpsql_Penyelidikan_Gred As String = ""
            Dim tmpsql_Penyelidikan_PNG As String = ""
            Dim tmpsql_Penyelidikan_KOD As String = ""

            Dim tmpsql_EL_Subject As String = ""
            Dim tmpsql_EL_GRED As String = ""
            Dim tmpsql_EL_PNG As String = ""
            Dim tmpsql_EL_KOD As String = ""
            Dim tmpsql_EL_HOUR As String = ""
            Dim tmpsql_EL_TOTAL As String = ""

            Dim tmpsql_KOKO_KOD_SUKAN As String = ""
            Dim tmpsql_KOKO_KOD_UNIFORM As String = ""
            Dim tmpsql_KOKO_KOD_KELAB As String = ""
            Dim tmpsql_KOKO_NAMA_SUKAN As String = ""
            Dim tmpsql_KOKO_NAMA_KELAB As String = ""
            Dim tmpsql_KOKO_NAMA_UNIFORM As String = ""
            Dim tmpsql_KOKO_GRED As String = ""
            Dim tmpsql_KOKO_PNG As String = ""

            Dim errorCount As Integer = 0
            Dim i As Integer = 0
            Dim Test As New StringBuilder()

            'get englih literture on / off
            Dim check_Eng_Literature As String = "select Value from setting where Type = 'English Literature'"
            Dim Confirm_Eng_Literature As String = oCommon.getFieldValue(check_Eng_Literature)

            Test.AppendLine("<div id='data' style='display:none'>")
            Test.AppendLine("<div id='dataTESTBM'> ")

            ''''''''''''''''''''''''''''''checking student 
            'get Portfolio percentage on / off
            Dim check_portfolio_percen As String = "select stat_portfolio from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Portfolio As String = oCommon.getFieldValue(check_portfolio_percen)

            ''get cocuricullum percentage on / off
            Dim check_cocuricullum_percen As String = "select stat_kokurikulum from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Cocuricullum As String = oCommon.getFieldValue(check_cocuricullum_percen)

            ''get research percentage on / off
            Dim check_research_percen As String = "select stat_penyelidikan from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Research As String = oCommon.getFieldValue(check_research_percen)

            ''get self development percentage on / off
            Dim check_self_percen As String = "select stat_kendiri from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Self As String = oCommon.getFieldValue(check_self_percen)

            ''print subject name 
            tmpSQL_Nama = "SELECT subject_NameBM FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_NameBM ASC"
            Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

            ''print subject code
            tmpSQL_Kod = "SELECT subject_code FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_NameBM ASC"
            Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

            ''print subject grade
            tmpSQL_Gred = "SELECT grade FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_NameBM ASC"
            Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

            ''print subject png
            tmpSQL_PNG = "SELECT gpa FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_NameBM ASC"
            Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

            ''print subject credit hour
            tmpSQL_Hour = "SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_NameBM ASC"
            Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

            ''print subject credit hour
            tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_NameBM ASC"
            Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)



            tmpSQL = "  select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
            Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "  select SUM(total) FROM [ExamSlip_SubjectName] 
                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
            Dim total_Total As String = oCommon.getFieldValue(tmpSQL)


            Dim DS_Nama As New DataTable
            Dim DS_Kod As New DataTable
            Dim DS_Gred As New DataTable
            Dim DS_PNG As New DataTable
            Dim DS_Hour As New DataTable
            Dim DS_Total As New DataTable

            Dim DSSelfdevelopment_GRED As New DataTable
            Dim DSSelfdevelopment_PNG As New DataTable
            Dim DSSelfdevelopment_KOD As New DataTable

            Dim DSEnglish_literature_SUBJECT As New DataTable
            Dim DSEnglish_literature_GRED As New DataTable
            Dim DSEnglish_literature_PNG As New DataTable
            Dim DSEnglish_literature_KOD As New DataTable
            Dim DSEnglish_literature_HOUR As New DataTable
            Dim DSEnglish_literature_TOTAL As New DataTable

            Dim DSResearch_GRED As New DataTable
            Dim DSResearch_PNG As New DataTable
            Dim DSResearch_KOD As New DataTable

            Dim DSPortfolio_GRED As New DataTable
            Dim DSPortfolio_PNG As New DataTable
            Dim DSPortfolio_KOD As New DataTable

            Dim DSCocuricullum_KOD_SUKAN As New DataTable
            Dim DSCocuricullum_KOD_UNIFORM As New DataTable
            Dim DSCocuricullum_KOD_KELAB As New DataTable
            Dim DSCocuricullum_NAMA_SUKAN As New DataTable
            Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
            Dim DSCocuricullum_NAMA_KELAB As New DataTable
            Dim DSCocuricullum_GRED As New DataTable
            Dim DSCocuricullum_PNG As New DataTable

            Dim total_Credit_EL As String = "0"
            Dim total_Total_EL As String = "0"

            ''print english literature
            If Confirm_Eng_Literature = "On" Then
                tmpsql_EL_Subject = "SELECT subject_NameBM FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_GRED = "SELECT grade FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_PNG = "SELECT gpa FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_KOD = "SELECT subject_code FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_HOUR = "SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_TOTAL = "SELECT total FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "


                tmpSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                total_Credit_EL = oCommon.getFieldValue(tmpSQL)

                If total_Credit_EL.Length = 0 Then
                    total_Credit_EL = "0"
                End If

                tmpSQL = "select total FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                total_Total_EL = oCommon.getFieldValue(tmpSQL)

                If total_Total_EL.Length = 0 Then
                    total_Total_EL = "0"
                End If

                Dim SQEnglish_Literature_SUBJECT As New SqlDataAdapter(tmpsql_EL_Subject, strConn)
                Dim SQEnglish_Literature_GRED As New SqlDataAdapter(tmpsql_EL_GRED, strConn)
                Dim SQEnglish_Literature_PNG As New SqlDataAdapter(tmpsql_EL_PNG, strConn)
                Dim SQEnglish_Literature_KOD As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
                Dim SQEnglish_Literature_HOUR As New SqlDataAdapter(tmpsql_EL_HOUR, strConn)
                Dim SQEnglish_Literature_TOTAL As New SqlDataAdapter(tmpsql_EL_TOTAL, strConn)

                Try
                    SQEnglish_Literature_SUBJECT.Fill(DSEnglish_literature_SUBJECT)
                    SQEnglish_Literature_GRED.Fill(DSEnglish_literature_GRED)
                    SQEnglish_Literature_KOD.Fill(DSEnglish_literature_KOD)
                    SQEnglish_Literature_PNG.Fill(DSEnglish_literature_PNG)
                    SQEnglish_Literature_HOUR.Fill(DSEnglish_literature_HOUR)
                    SQEnglish_Literature_TOTAL.Fill(DSEnglish_literature_TOTAL)
                Catch ex As Exception

                End Try
            End If

            ''print Portfolio
            If Confirm_Portfolio = "On" Then
                tmpsql_Portfolio_GRED = "SELECT grade FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                Dim SQPortfolio_GRED As New SqlDataAdapter(tmpsql_Portfolio_GRED, strConn)
                Dim SQPortfolio_PNG As New SqlDataAdapter(tmpsql_Portfolio_PNG, strConn)
                Dim SQPortfolio_KOD As New SqlDataAdapter(tmpsql_Portfolio_KOD, strConn)

                Try
                    SQPortfolio_GRED.Fill(DSPortfolio_GRED)
                    SQPortfolio_PNG.Fill(DSPortfolio_PNG)
                    SQPortfolio_KOD.Fill(DSPortfolio_KOD)
                Catch ex As Exception

                End Try
            End If

            ''print research 
            If Confirm_Research = "On" Then
                tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_Penyelidikan_PNG = "SELECT gpa FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_Penyelidikan_KOD = "SELECT subject_code FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                Dim SQResearch_GRED As New SqlDataAdapter(tmpsql_Penyelidikan_Gred, strConn)
                Dim SQResearch_PNG As New SqlDataAdapter(tmpsql_Penyelidikan_PNG, strConn)
                Dim SQResearch_KOD As New SqlDataAdapter(tmpsql_Penyelidikan_KOD, strConn)

                Try
                    SQResearch_GRED.Fill(DSResearch_GRED)
                    SQResearch_PNG.Fill(DSResearch_PNG)
                    SQResearch_KOD.Fill(DSResearch_KOD)
                Catch ex As Exception

                End Try
            End If

            ''print self development
            If Confirm_Self = "On" Then
                Dim level As String = "select student_Level from student_level where std_ID = '" & Session("Std_ID") & "' and year = '" & ddlYearExam.SelectedValue & "' "
                Dim getLevel As String = oCommon.getFieldValue(level)

                If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                    tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                    tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                    tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                    tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                    tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                    tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                End If

                Dim SQSelfdevelopment_GRED As New SqlDataAdapter(tmpSQL_SD_GRED, strConn)
                Dim SQSelfdevelopment_PNG As New SqlDataAdapter(tmpsql_SD_PNG, strConn)
                Dim SQSelfdevelopment_KOD As New SqlDataAdapter(tmpsql_SD_KOD, strConn)

                Try
                    SQSelfdevelopment_GRED.Fill(DSSelfdevelopment_GRED)
                    SQSelfdevelopment_PNG.Fill(DSSelfdevelopment_PNG)
                    SQSelfdevelopment_KOD.Fill(DSSelfdevelopment_KOD)
                Catch ex As Exception

                End Try
            End If

            '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
            If Confirm_Cocuricullum = "On" Then

                Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & Session("Std_ID") & "'"
                Dim getStudent As String = oCommon.getFieldValue(studentData)

                If ddlExamination.SelectedValue = "Exam 2" Or ddlExamination.SelectedValue = "Exam 6" Or ddlExamination.SelectedValue = "Exam 10" Then

                    tmpsql_KOKO_PNG = "select koko_pelajar.PNGP1 from koko_pelajar
                                        left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                        where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                        left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                        where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                    Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                    Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                    Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                    Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                    Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
                    Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
                    Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

                    Try
                        SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                        SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                        SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                        SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                        SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                        SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                        SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                        SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                    Catch ex As Exception

                    End Try

                ElseIf ddlExamination.SelectedValue = "Exam 4" Or ddlExamination.SelectedValue = "Exam 7" Or ddlExamination.SelectedValue = "Exam 8" Or ddlExamination.SelectedValue = "Exam 12" Then

                    tmpsql_KOKO_PNG = "select koko_pelajar.PNGP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                    Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                    Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                    Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                    Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                    Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
                    Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
                    Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

                    Try
                        SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                        SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                        SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                        SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                        SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                        SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                        SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                        SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                    Catch ex As Exception

                    End Try

                End If
            End If

            Try
                SQA.Fill(DS_Nama)
                SQACODE.Fill(DS_Kod)
                SQAPNG.Fill(DS_PNG)
                SQAGRADE.Fill(DS_Gred)
                SQAHOUR.Fill(DS_Hour)
                SQATOTAL.Fill(DS_Total)
            Catch ex As Exception
            End Try

            ''print student name
            Dim stdName As String = "select student_Name from student_info where std_ID = '" & Session("Std_ID") & "'"
            Dim dataStdName As String = oCommon.getFieldValue(stdName)

            ''print student id
            Dim stdID As String = "select student_ID from student_info where std_ID = '" & Session("Std_ID") & "'"
            Dim dataStdID As String = oCommon.getFieldValue(stdID)

            ''print student mykad
            Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & Session("Std_ID") & "'"
            Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

            ''print exam Name
            Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlExamination.SelectedValue & "'"
            Dim dataExmName As String = oCommon.getFieldValue(exmName)

            If dataExmName = "Exam 1" Then
                dataExmName = "Pentaksiran 1 Semester 1 "
            ElseIf dataExmName = "Exam 2" Then
                dataExmName = "Pentaksiran 2 Semester 1 "
            ElseIf dataExmName = "Exam 3" Then
                dataExmName = "Pentaksiran 1 Semester 2 "
            ElseIf dataExmName = "Exam 4" Then
                dataExmName = "Pentaksiran 2 Semester 2 "
            ElseIf dataExmName = "Exam 5" Then
                dataExmName = "Pentaksiran 1 Semester 1 "
            ElseIf dataExmName = "Exam 6" Then
                dataExmName = "Pentaksiran 2 Semester 1 "
            ElseIf dataExmName = "Exam 7" Then
                dataExmName = "Pentaksiran 1 Semester 2 "
            ElseIf dataExmName = "Exam 8" Then
                dataExmName = "Pentaksiran 2 Semester 2 "
            ElseIf dataExmName = "Exam 9" Then
                dataExmName = "Pentaksiran 1 Semester 1 "
            ElseIf dataExmName = "Exam 10" Then
                dataExmName = "Pentaksiran 2 Semester 1 "
            ElseIf dataExmName = "Exam 11" Then
                dataExmName = "Pentaksiran 1 Semester 2 "
            ElseIf dataExmName = "Exam 12" Then
                dataExmName = "Pentaksiran 2 Semester 2 "
            End If

            ''get month
            Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
            Dim dataMonth As String = oCommon.getFieldValue(month)

            Dim dataStdMonth As String = ""

            If dataMonth = "1" Then
                dataStdMonth = "Januari"
            ElseIf dataMonth = "2" Then
                dataStdMonth = "Februari"
            ElseIf dataMonth = "3" Then
                dataStdMonth = "Mac"
            ElseIf dataMonth = "4" Then
                dataStdMonth = "April"
            ElseIf dataMonth = "5" Then
                dataStdMonth = "Mei"
            ElseIf dataMonth = "6" Then
                dataStdMonth = "Jun"
            ElseIf dataMonth = "7" Then
                dataStdMonth = "Julai"
            ElseIf dataMonth = "8" Then
                dataStdMonth = "Ogos"
            ElseIf dataMonth = "9" Then
                dataStdMonth = "September"
            ElseIf dataMonth = "10" Then
                dataStdMonth = "Oktober"
            ElseIf dataMonth = "11" Then
                dataStdMonth = "November"
            ElseIf dataMonth = "12" Then
                dataStdMonth = "Disember"
            End If

            ''get PNG & PNGK 
            Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

            Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

            Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
            Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

            ''round to 2 decimal places
            Dim gpa As Decimal = png_dec.ToString("F2")
            Dim cgpa As Decimal = pngs_dec.ToString("F2")


            tmpSQL = "select komp_akademik from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim research_value As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim sd_value As String = oCommon.getFieldValue(tmpSQL)

            ''first column
            Test.Append("<div style='margin:0;page-break-after: always;'>
                                        <table style='width:100%'>
                                            <tr style='width:100%'>
                                                <td style='width:100%'>
                                                    <table tyle='width:100%'>
                                                        <tr style='width:100%'>
                                                            <td>
                                                                <img src='img/ukm.jpg'  height='56' width='120'>
                                                                &nbsp;
                                                                <img src='img/logo genius pintar.png' height='62' width='100'>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style='width:100%'>    
                                                <td style='width:100%'>
                                                    <table style='width:100%'>
                                                        <tr style='width:100%'>
                                                            <td style='width:10%;font-size:0.8125em;'> Nama</td>
                                                            <td style='width:90%;font-size:0.8125em;'>: " & dataStdName & "</td>
                                                        </tr>     
                                                        <tr style='width:100%'>
                                                            <td style='width:10%;font-size:0.8125em;'> MYKAD </td>
                                                            <td style='width:90%;font-size:0.8125em;'>: " & dataStdMykad & "</td>
                                                        </tr>     
                                                        <tr style='width:100%'>
                                                            <td style='width:10%;font-size:0.8125em;'> ID Pelajar </td>
                                                            <td style='width:90%;font-size:0.8125em;'>: " & dataStdID & "</td>
                                                        </tr>  
                                                        <tr style='width:100%'>
                                                            <td style='width:10%;font-size:0.8125em;'> Pentaksiran </td>
                                                            <td style='width:90%;font-size:0.8125em;'>: " & dataExmName & "</td>
                                                        </tr>
                                                    </table>    
                                                </td>
                                            </tr>
                                        </table>

                                        <table style='width:100%; padding-top:5px;>
                                            <tr>
                                                <td>
                                                    <p></p>
                                                </td>
                                                <table style='border: 1px solid black;border-collapse: collapse;'>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Komponen </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'><b> Peratusan </b></td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'><b> Kod Kursus </b></td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'><b> Kursus </b></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'><b> Gred </b></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'><b> PNG </b></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'><b> Jam Kredit </b></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'><b> PNG x Jam Kredit </b></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center '>
                                                        <td rowspan='3' style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Akademik </b></td>
                                                        <td rowspan='3'style='width:7%;border: 1px solid black;font-size:0.8125em;'> " & academic_value & "</td>
                                                        <td style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em;'>")

            ''(column course code / kod kursus)
            For Each row As DataRow In DS_Kod.Rows
                For Each column As DataColumn In DS_Kod.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_KOD As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_KOD As String = oCommon.getFieldValue(get_ENG_KOD)

            If data_ENGLITERATURE_KOD.Length > 0 Then

                ''english literature kod
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_KOD.Rows
                        For Each column As DataColumn In DSEnglish_literature_KOD.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Test.Append(" SM <br />")
                End If

            End If

            Test.Append("                   </td>
                                            <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em;'>")

            ''(column course / kursus)
            For Each row As DataRow In DS_Nama.Rows
                For Each column As DataColumn In DS_Nama.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_NAMA As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_NAMA As String = oCommon.getFieldValue(get_ENG_NAMA)

            If data_ENGLITERATURE_NAMA.Length > 0 Then

                ''english literature NAMA
                For Each row As DataRow In DSEnglish_literature_SUBJECT.Rows
                    For Each column As DataColumn In DSEnglish_literature_SUBJECT.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next

            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'> ")

            ''(column grade / gred)
            For Each row As DataRow In DS_Gred.Rows
                For Each column As DataColumn In DS_Gred.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_Grade As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_Grade As String = oCommon.getFieldValue(get_ENG_Grade)

            If data_ENGLITERATURE_Grade.Length > 0 Then

                ''english literature Name
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_GRED.Rows
                        For Each column As DataColumn In DSEnglish_literature_GRED.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Test.Append(" SM <br />")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'> ")

            ''(column gpa / png)
            For Each row As DataRow In DS_PNG.Rows
                For Each column As DataColumn In DS_PNG.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_Png As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_Png As String = oCommon.getFieldValue(get_ENG_Png)

            If data_ENGLITERATURE_Png.Length > 0 Then

                ''english literature Name
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_PNG.Rows
                        For Each column As DataColumn In DSEnglish_literature_PNG.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Test.Append(" SM <br />")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'>")

            ''(column credit hour / jam kredit)
            For Each row As DataRow In DS_Hour.Rows
                For Each column As DataColumn In DS_Hour.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_HOUR As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_HOUR As String = oCommon.getFieldValue(get_ENG_HOUR)

            If data_ENGLITERATURE_HOUR.Length > 0 Then

                ''english literature credit hour
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_HOUR.Rows
                        For Each column As DataColumn In DSEnglish_literature_HOUR.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Test.Append(" SM <br />")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'> ")

            ''(column total / jumalh)
            For Each row As DataRow In DS_Total.Rows
                For Each column As DataColumn In DS_Total.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_TOTAL As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_TOTAL As String = oCommon.getFieldValue(get_ENG_TOTAL)

            If data_ENGLITERATURE_TOTAL.Length > 0 Then

                ''english literature total / jumlah
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_TOTAL.Rows
                        For Each column As DataColumn In DSEnglish_literature_TOTAL.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Debug.WriteLine("Error 1")
                    Test.Append(" SM <br />")
                End If

            End If

            Dim Number1 As Double = Double.Parse(total_Credit)
            Dim Number2 As Double = Double.Parse(total_Credit_EL)
            Dim Number3 As Double = Double.Parse(total_Total)
            Dim Number4 As Double = Double.Parse(total_Total_EL)

            Dim total_Hour As Double = Number1 + Number2
            Dim final_Total As Double = Number3 + Number4

            Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

            Test.Append("                   </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em;'><b> Jumlah </b></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'> " & total_Hour & " </td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'> " & final_Total & " </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em;'><b> PNG Akademik </b></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'> </td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'> " & PNG_Akademik & " </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Kokurikulum </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'>" & cocuricullum_value & "</td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'>")

            ''kokorikulum kod sukan
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            ''kokorikulum kod kelab
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            ''kokorikulum kod uniform
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em;'>")

            ''kokorikulum nama skan
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            ''kokorikulum nama kelab
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            ''kokorikulum nama uniform
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''kokorikulum gred 
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_GRED.Rows
                    For Each column As DataColumn In DSCocuricullum_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append(" SM <br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''kokorikulum png 
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_PNG.Rows
                    For Each column As DataColumn In DSCocuricullum_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("SM <br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Portfolio </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'> " & portfolio_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'>")

            ''Portfolio KOD
            If Confirm_Portfolio = "On" Then
                For Each row As DataRow In DSPortfolio_KOD.Rows
                    For Each column As DataColumn In DSPortfolio_KOD.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Portfolio = "Off" Then
                Test.Append("<br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''Portfolio Gred
            If Confirm_Portfolio = "On" Then
                For Each row As DataRow In DSPortfolio_GRED.Rows
                    For Each column As DataColumn In DSPortfolio_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Portfolio = "Off" Then
                Test.Append("SM <br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''Portfolio PNG
            If Confirm_Portfolio = "On" Then
                For Each row As DataRow In DSPortfolio_PNG.Rows
                    For Each column As DataColumn In DSPortfolio_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Portfolio = "Off" Then
                Test.Append("SM <br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Penyelidikan </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'> " & research_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'>")

            ''research KOD
            If Confirm_Research = "On" Then
                For Each row As DataRow In DSResearch_KOD.Rows
                    For Each column As DataColumn In DSResearch_KOD.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "Off" Then
                Test.Append("<br />")
            End If

            Test.Append("</td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''research GRED
            If Confirm_Research = "On" Then
                For Each row As DataRow In DSResearch_GRED.Rows
                    For Each column As DataColumn In DSResearch_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "Off" Then
                Test.Append(" SM <br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''research PNG
            If Confirm_Research = "On" Then
                For Each row As DataRow In DSResearch_PNG.Rows
                    For Each column As DataColumn In DSResearch_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "Off" Then
                Test.Append(" SM <br />")
            End If

            Test.Append("</td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Pembangunan Kendiri </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'> " & sd_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'>")

            ''(column self development codde / pembangunan kendiri kod)
            For Each row As DataRow In DSSelfdevelopment_KOD.Rows
                For Each column As DataColumn In DSSelfdevelopment_KOD.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Test.Append("                    </td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''(column self development grade / pembangunan kendiri gred)
            For Each row As DataRow In DSSelfdevelopment_GRED.Rows
                For Each column As DataColumn In DSSelfdevelopment_GRED.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''(column self development gpa / pembangunan kendiri png)
            For Each row As DataRow In DSSelfdevelopment_PNG.Rows
                For Each column As DataColumn In DSSelfdevelopment_PNG.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> PNG </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'><b> " & gpa & " </b></td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> PNGK </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'><b> " & cgpa & "</b></td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                </table>
                                            </tr>
                                         </table>    
                                     </div>")

            Test.AppendLine(" </div> </div>")
            Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataTESTBM'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

            'print
            Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())
        Else
            ShowMessage(" The Examination Is Closed ", MessageType.Error)
        End If
    End Sub

    Private Sub btnPrintEnglish_ServerClick(sender As Object, e As EventArgs) Handles btnPrintEnglish.ServerClick

        Dim get_Check As String = ""

        If Session("Student_Campus") = "PGPN" Then
            get_Check = "select Value from setting where Type  = 'Exam Result PGPN'"
        ElseIf Session("Student_Campus") = "APP" Then
            get_Check = "select Value from setting where Type  = 'Exam Result APP'"
        End If

        Dim get_ExamStatus As String = oCommon.getFieldValue(get_Check)

        If get_ExamStatus = "On" Or get_ExamStatus = "on" Or get_ExamStatus = "ON" Then

            Dim tmpSQL As String = ""
            Dim tmpSQL_Nama As String = ""
            Dim tmpSQL_Kod As String = ""
            Dim tmpSQL_Gred As String = ""
            Dim tmpSQL_PNG As String = ""
            Dim tmpSQL_Hour As String = ""
            Dim tmpSQL_Total As String = ""

            Dim tmpSQL_SD_GRED As String = ""
            Dim tmpsql_SD_PNG As String = ""
            Dim tmpsql_SD_KOD As String = ""

            Dim tmpsql_Portfolio_GRED As String = ""
            Dim tmpsql_Portfolio_PNG As String = ""
            Dim tmpsql_Portfolio_KOD As String = ""

            Dim tmpsql_Penyelidikan_Gred As String = ""
            Dim tmpsql_Penyelidikan_PNG As String = ""
            Dim tmpsql_Penyelidikan_KOD As String = ""

            Dim tmpsql_EL_Subject As String = ""
            Dim tmpsql_EL_GRED As String = ""
            Dim tmpsql_EL_PNG As String = ""
            Dim tmpsql_EL_KOD As String = ""
            Dim tmpsql_EL_HOUR As String = ""
            Dim tmpsql_EL_TOTAL As String = ""

            Dim tmpsql_KOKO_KOD_SUKAN As String = ""
            Dim tmpsql_KOKO_KOD_UNIFORM As String = ""
            Dim tmpsql_KOKO_KOD_KELAB As String = ""
            Dim tmpsql_KOKO_NAMA_SUKAN As String = ""
            Dim tmpsql_KOKO_NAMA_KELAB As String = ""
            Dim tmpsql_KOKO_NAMA_UNIFORM As String = ""
            Dim tmpsql_KOKO_GRED As String = ""
            Dim tmpsql_KOKO_PNG As String = ""

            Dim errorCount As Integer = 0
            Dim i As Integer = 0
            Dim Test As New StringBuilder()

            'get englih literture on / off
            Dim check_Eng_Literature As String = "select Value from setting where Type = 'English Literature'"
            Dim Confirm_Eng_Literature As String = oCommon.getFieldValue(check_Eng_Literature)

            Test.AppendLine("<div id='data' style='display:none'>")
            Test.AppendLine("<div id='dataTESTBI'> ")

            ''print subject name 
            tmpSQL_Nama = "SELECT subject_Name FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_Name ASC"
            Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

            ''print subject code
            tmpSQL_Kod = "SELECT subject_code FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_Name ASC"
            Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

            ''print subject grade
            tmpSQL_Gred = "SELECT grade FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_Name ASC"
            Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

            ''print subject png
            tmpSQL_PNG = "SELECT gpa FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_Name ASC"
            Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

            ''print subject credit hour
            tmpSQL_Hour = "SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_Name ASC"
            Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

            ''print subject credit hour
            tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' order by course_Name, subject_Name ASC"
            Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)


            ''''''''''''''''''''''''''''''checking student status on/off

            'get Portfolio percentage on / off
            Dim check_portfolio_percen As String = "select stat_portfolio from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Portfolio As String = oCommon.getFieldValue(check_portfolio_percen)

            ''get cocuricullum percentage on / off
            Dim check_cocuricullum_percen As String = "select stat_kokurikulum from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Cocuricullum As String = oCommon.getFieldValue(check_cocuricullum_percen)

            ''get research percentage on / off
            Dim check_research_percen As String = "select stat_penyelidikan from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Research As String = oCommon.getFieldValue(check_research_percen)

            ''get self development percentage on / off
            Dim check_self_percen As String = "select stat_kendiri from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim Confirm_Self As String = oCommon.getFieldValue(check_self_percen)


            tmpSQL = "select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
            Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select SUM(total) FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
            Dim total_Total As String = oCommon.getFieldValue(tmpSQL)

            Dim DS_Nama As New DataTable
            Dim DS_Kod As New DataTable
            Dim DS_Gred As New DataTable
            Dim DS_PNG As New DataTable
            Dim DS_Hour As New DataTable
            Dim DS_Total As New DataTable

            Dim DSSelfdevelopment_GRED As New DataTable
            Dim DSSelfdevelopment_PNG As New DataTable
            Dim DSSelfdevelopment_KOD As New DataTable

            Dim DSEnglish_literature_SUBJECT As New DataTable
            Dim DSEnglish_literature_GRED As New DataTable
            Dim DSEnglish_literature_PNG As New DataTable
            Dim DSEnglish_literature_KOD As New DataTable
            Dim DSEnglish_literature_HOUR As New DataTable
            Dim DSEnglish_literature_TOTAL As New DataTable

            Dim DSResearch_GRED As New DataTable
            Dim DSResearch_PNG As New DataTable
            Dim DSResearch_KOD As New DataTable

            Dim DSPortfolio_GRED As New DataTable
            Dim DSPortfolio_PNG As New DataTable
            Dim DSPortfolio_KOD As New DataTable

            Dim DSCocuricullum_KOD_SUKAN As New DataTable
            Dim DSCocuricullum_KOD_UNIFORM As New DataTable
            Dim DSCocuricullum_KOD_KELAB As New DataTable
            Dim DSCocuricullum_NAMA_SUKAN As New DataTable
            Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
            Dim DSCocuricullum_NAMA_KELAB As New DataTable
            Dim DSCocuricullum_GRED As New DataTable
            Dim DSCocuricullum_PNG As New DataTable

            Dim total_Credit_EL As String = "0"
            Dim total_Total_EL As String = "0"

            ''print english literature
            If Confirm_Eng_Literature = "On" Then
                tmpsql_EL_Subject = "SELECT subject_Name FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_GRED = "SELECT grade FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_PNG = "SELECT gpa FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_KOD = "SELECT subject_code FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_HOUR = "SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_EL_TOTAL = "SELECT total FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "


                tmpSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                total_Credit_EL = oCommon.getFieldValue(tmpSQL)

                If total_Credit_EL.Length = 0 Then
                    total_Credit_EL = "0"
                End If


                tmpSQL = "select total FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "
                total_Total_EL = oCommon.getFieldValue(tmpSQL)

                If total_Total_EL.Length = 0 Then
                    total_Total_EL = "0"
                End If

                Dim SQEnglish_Literature_SUBJECT As New SqlDataAdapter(tmpsql_EL_Subject, strConn)
                Dim SQEnglish_Literature_GRED As New SqlDataAdapter(tmpsql_EL_GRED, strConn)
                Dim SQEnglish_Literature_PNG As New SqlDataAdapter(tmpsql_EL_PNG, strConn)
                Dim SQEnglish_Literature_KOD As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
                Dim SQEnglish_Literature_HOUR As New SqlDataAdapter(tmpsql_EL_HOUR, strConn)
                Dim SQEnglish_Literature_TOTAL As New SqlDataAdapter(tmpsql_EL_TOTAL, strConn)

                Try
                    SQEnglish_Literature_SUBJECT.Fill(DSEnglish_literature_SUBJECT)
                    SQEnglish_Literature_GRED.Fill(DSEnglish_literature_GRED)
                    SQEnglish_Literature_KOD.Fill(DSEnglish_literature_KOD)
                    SQEnglish_Literature_PNG.Fill(DSEnglish_literature_PNG)
                    SQEnglish_Literature_HOUR.Fill(DSEnglish_literature_HOUR)
                    SQEnglish_Literature_TOTAL.Fill(DSEnglish_literature_TOTAL)
                Catch ex As Exception

                End Try
            End If

            ''print Portfolio
            If Confirm_Portfolio = "On" Then
                tmpsql_Portfolio_GRED = "SELECT grade FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                Dim SQPortfolio_GRED As New SqlDataAdapter(tmpsql_Portfolio_GRED, strConn)
                Dim SQPortfolio_PNG As New SqlDataAdapter(tmpsql_Portfolio_PNG, strConn)
                Dim SQPortfolio_KOD As New SqlDataAdapter(tmpsql_Portfolio_KOD, strConn)

                Try
                    SQPortfolio_GRED.Fill(DSPortfolio_GRED)
                    SQPortfolio_PNG.Fill(DSPortfolio_PNG)
                    SQPortfolio_KOD.Fill(DSPortfolio_KOD)
                Catch ex As Exception

                End Try
            End If

            ''print research 
            If Confirm_Research = "On" Then
                tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_Penyelidikan_PNG = "SELECT gpa FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                tmpsql_Penyelidikan_KOD = "SELECT subject_code FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                Dim SQResearch_GRED As New SqlDataAdapter(tmpsql_Penyelidikan_Gred, strConn)
                Dim SQResearch_PNG As New SqlDataAdapter(tmpsql_Penyelidikan_PNG, strConn)
                Dim SQResearch_KOD As New SqlDataAdapter(tmpsql_Penyelidikan_KOD, strConn)

                Try
                    SQResearch_GRED.Fill(DSResearch_GRED)
                    SQResearch_PNG.Fill(DSResearch_PNG)
                    SQResearch_KOD.Fill(DSResearch_KOD)
                Catch ex As Exception

                End Try
            End If

            ''print self development
            If Confirm_Self = "On" Then
                Dim level As String = "select student_Level from student_level where std_ID = '" & Session("Std_ID") & "' and year = '" & ddlYearExam.SelectedValue & "' "
                Dim getLevel As String = oCommon.getFieldValue(level)

                If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                    tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                    tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                    tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                    tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                    tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                    tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and exam_Year = '" & ddlYearExam.SelectedValue & "' "

                End If

                Dim SQSelfdevelopment_GRED As New SqlDataAdapter(tmpSQL_SD_GRED, strConn)
                Dim SQSelfdevelopment_PNG As New SqlDataAdapter(tmpsql_SD_PNG, strConn)
                Dim SQSelfdevelopment_KOD As New SqlDataAdapter(tmpsql_SD_KOD, strConn)

                Try
                    SQSelfdevelopment_GRED.Fill(DSSelfdevelopment_GRED)
                    SQSelfdevelopment_PNG.Fill(DSSelfdevelopment_PNG)
                    SQSelfdevelopment_KOD.Fill(DSSelfdevelopment_KOD)
                Catch ex As Exception

                End Try
            End If

            '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
            If Confirm_Cocuricullum = "On" Then

                Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & Session("Std_ID") & "'"
                Dim getStudent As String = oCommon.getFieldValue(studentData)

                If ddlExamination.SelectedValue = "Exam 2" Or ddlExamination.SelectedValue = "Exam 6" Or ddlExamination.SelectedValue = "Exam 10" Then

                    tmpsql_KOKO_PNG = "select koko_pelajar.PNGP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                    Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                    Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                    Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                    Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                    Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
                    Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
                    Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

                    Try
                        SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                        SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                        SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                        SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                        SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                        SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                        SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                        SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                    Catch ex As Exception

                    End Try

                ElseIf ddlExamination.SelectedValue = "Exam 4" Or ddlExamination.SelectedValue = "Exam 7" Or ddlExamination.SelectedValue = "Exam 8" Or ddlExamination.SelectedValue = "Exam 12" Then

                    tmpsql_KOKO_PNG = "select koko_pelajar.PNGP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMABI from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlYearExam.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlYearExam.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, strConnPermata)
                    Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, strConnPermata)
                    Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, strConnPermata)
                    Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, strConnPermata)
                    Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, strConnPermata)
                    Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, strConnPermata)
                    Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, strConnPermata)
                    Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, strConnPermata)

                    Try
                        SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                        SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                        SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                        SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                        SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                        SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                        SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                        SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                    Catch ex As Exception

                    End Try

                End If
            End If

            Try
                SQA.Fill(DS_Nama)
                SQACODE.Fill(DS_Kod)
                SQAPNG.Fill(DS_PNG)
                SQAGRADE.Fill(DS_Gred)
                SQAHOUR.Fill(DS_Hour)
                SQATOTAL.Fill(DS_Total)
            Catch ex As Exception
            End Try

            ''print student name
            Dim stdName As String = "select student_Name from student_info where std_ID = '" & Session("Std_ID") & "'"
            Dim dataStdName As String = oCommon.getFieldValue(stdName)

            ''print student id
            Dim stdID As String = "select student_ID from student_info where std_ID = '" & Session("Std_ID") & "'"
            Dim dataStdID As String = oCommon.getFieldValue(stdID)

            ''print student mykad
            Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & Session("Std_ID") & "'"
            Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

            ''print exam Name
            Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlExamination.SelectedValue & "'"
            Dim dataExmName As String = oCommon.getFieldValue(exmName)

            If dataExmName = "Exam 1" Then
                dataExmName = "Assessment 1 Semester 1 "
            ElseIf dataExmName = "Exam 2" Then
                dataExmName = "Assessment 2 Semester 1 "
            ElseIf dataExmName = "Exam 3" Then
                dataExmName = "Assessment 1 Semester 2 "
            ElseIf dataExmName = "Exam 4" Then
                dataExmName = "Assessment 2 Semester 2 "
            ElseIf dataExmName = "Exam 5" Then
                dataExmName = "Assessment 1 Semester 1 "
            ElseIf dataExmName = "Exam 6" Then
                dataExmName = "Assessment 2 Semester 1 "
            ElseIf dataExmName = "Exam 7" Then
                dataExmName = "Assessment 1 Semester 2 "
            ElseIf dataExmName = "Exam 8" Then
                dataExmName = "Assessment 2 Semester 2 "
            ElseIf dataExmName = "Exam 9" Then
                dataExmName = "Assessment 1 Semester 1 "
            ElseIf dataExmName = "Exam 10" Then
                dataExmName = "Assessment 2 Semester 1 "
            ElseIf dataExmName = "Exam 11" Then
                dataExmName = "Assessment 1 Semester 2 "
            ElseIf dataExmName = "Exam 12" Then
                dataExmName = "Assessment 2 Semester 2 "
            End If

            ''get month
            Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
            Dim dataMonth As String = oCommon.getFieldValue(month)

            Dim dataStdMonth As String = ""

            If dataMonth = "1" Then
                dataStdMonth = "January"
            ElseIf dataMonth = "2" Then
                dataStdMonth = "February"
            ElseIf dataMonth = "3" Then
                dataStdMonth = "March"
            ElseIf dataMonth = "4" Then
                dataStdMonth = "April"
            ElseIf dataMonth = "5" Then
                dataStdMonth = "May"
            ElseIf dataMonth = "6" Then
                dataStdMonth = "Jun"
            ElseIf dataMonth = "7" Then
                dataStdMonth = "July"
            ElseIf dataMonth = "8" Then
                dataStdMonth = "August"
            ElseIf dataMonth = "9" Then
                dataStdMonth = "September"
            ElseIf dataMonth = "10" Then
                dataStdMonth = "Ocotber"
            ElseIf dataMonth = "11" Then
                dataStdMonth = "November"
            ElseIf dataMonth = "12" Then
                dataStdMonth = "December"
            End If

            ''get PNG & PNGK 
            Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

            Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

            Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
            Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

            ''round to 2 decimal places
            Dim gpa As Decimal = png_dec.ToString("F2")
            Dim cgpa As Decimal = pngs_dec.ToString("F2")


            tmpSQL = "select komp_akademik from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim academic_value As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select komp_kokurikulum from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim cocuricullum_value As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select komp_portfolio from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim portfolio_value As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select komp_penyelidikan from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim research_value As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select komp_kendiri from student_Png where std_ID = '" & Session("Std_ID") & "' and exam_Name = '" & ddlExamination.SelectedValue & "' and year = '" & ddlYearExam.SelectedValue & "'"
            Dim sd_value As String = oCommon.getFieldValue(tmpSQL)

            ''first column
            Test.Append("<div style='margin:0;page-break-after: always;'>
                                        <table style='width:100%'>
                                            <tr style='width:100%'>
                                                <td style='width:100%'>
                                                    <table tyle='width:100%'>
                                                        <tr style='width:100%'>
                                                            <td>
                                                                <img src='img/ukm.jpg'  height='56' width='120'>
                                                                &nbsp;
                                                                <img src='img/logo genius pintar.png' height='62' width='100'>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style='width:100%'>    
                                                <td style='width:100%'>
                                                    <table style='width:100%'>
                                                        <tr style='width:100%'>
                                                            <td style='width:10%;font-size:0.8125em;'> Name </td>
                                                            <td style='width:90%;font-size:0.8125em;'>: " & dataStdName & "</td>
                                                        </tr>     
                                                        <tr style='width:100%'>
                                                            <td style='width:10%;font-size:0.8125em;'> NRIC </td>
                                                            <td style='width:90%;font-size:0.8125em;'>: " & dataStdMykad & "</td>
                                                        </tr>     
                                                        <tr style='width:100%'>
                                                            <td style='width:10%;font-size:0.8125em;'> Student ID </td>
                                                            <td style='width:90%;font-size:0.8125em;'>: " & dataStdID & "</td>
                                                        </tr>  
                                                        <tr style='width:100%'>
                                                            <td style='width:10%;font-size:0.8125em;'> Assessment </td>
                                                            <td style='width:90%;font-size:0.8125em;'>: " & dataExmName & "</td>
                                                        </tr>
                                                    </table>    
                                                </td>
                                            </tr>
                                        </table>

                                        <table style='width:100%; padding-top:5px'>
                                            <tr>
                                                <td>
                                                    <p></p>
                                                </td>
                                                <table style='border: 1px solid black;border-collapse: collapse;'>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Component </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'><b> Percentage </b></td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'><b> Course Code </b></td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'><b> Course </b></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'><b> Grade </b></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'><b> PNG </b></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'><b> Credit Hour </b></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'><b> PNG x Credit Hour </b></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center '>
                                                        <td rowspan='3' style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Academic </b></td>
                                                        <td rowspan='3'style='width:7%;border: 1px solid black;font-size:0.8125em;'> " & academic_value & "</td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;text-align:left'>")

            ''(column course code / kod kursus)
            For Each row As DataRow In DS_Kod.Rows
                For Each column As DataColumn In DS_Kod.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_KOD As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_KOD As String = oCommon.getFieldValue(get_ENG_KOD)

            If data_ENGLITERATURE_KOD.Length > 0 Then

                ''english literature kod
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_KOD.Rows
                        For Each column As DataColumn In DSEnglish_literature_KOD.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Test.Append(" SM <br />")
                End If

            End If

            Test.Append("                    </td>
                                                        <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em;'>")

            ''(column course / kursus)
            For Each row As DataRow In DS_Nama.Rows
                For Each column As DataColumn In DS_Nama.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_NAMA As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_NAMA As String = oCommon.getFieldValue(get_ENG_NAMA)

            If data_ENGLITERATURE_NAMA.Length > 0 Then

                ''english literature NAMA
                For Each row As DataRow In DSEnglish_literature_SUBJECT.Rows
                    For Each column As DataColumn In DSEnglish_literature_SUBJECT.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next

            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'> ")

            ''(column grade / gred)
            For Each row As DataRow In DS_Gred.Rows
                For Each column As DataColumn In DS_Gred.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_Grade As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_Grade As String = oCommon.getFieldValue(get_ENG_Grade)

            If data_ENGLITERATURE_Grade.Length > 0 Then

                ''english literature Name
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_GRED.Rows
                        For Each column As DataColumn In DSEnglish_literature_GRED.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Test.Append(" SM <br />")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'> ")

            ''(column gpa / png)
            For Each row As DataRow In DS_PNG.Rows
                For Each column As DataColumn In DS_PNG.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_Png As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_Png As String = oCommon.getFieldValue(get_ENG_Png)

            If data_ENGLITERATURE_Png.Length > 0 Then

                ''english literature Name
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_PNG.Rows
                        For Each column As DataColumn In DSEnglish_literature_PNG.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Test.Append(" SM <br />")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'>")

            ''(column credit hour / jam kredit)
            For Each row As DataRow In DS_Hour.Rows
                For Each column As DataColumn In DS_Hour.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_HOUR As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_HOUR As String = oCommon.getFieldValue(get_ENG_HOUR)

            If data_ENGLITERATURE_HOUR.Length > 0 Then

                ''english literature credit hour
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_HOUR.Rows
                        For Each column As DataColumn In DSEnglish_literature_HOUR.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Test.Append(" SM <br />")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'> ")

            ''(column total / jumalh)
            For Each row As DataRow In DS_Total.Rows
                For Each column As DataColumn In DS_Total.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Dim get_ENG_TOTAL As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlYearExam.SelectedValue & "' and course.std_ID = '" & Session("Std_ID") & "'"
            Dim data_ENGLITERATURE_TOTAL As String = oCommon.getFieldValue(get_ENG_TOTAL)

            If data_ENGLITERATURE_TOTAL.Length > 0 Then

                ''english literature total / jumlah
                If Confirm_Eng_Literature = "On" Then
                    For Each row As DataRow In DSEnglish_literature_TOTAL.Rows
                        For Each column As DataColumn In DSEnglish_literature_TOTAL.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br />")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "Off" Then
                    Debug.WriteLine("Error 1")
                    Test.Append(" SM <br />")
                End If

            End If

            Dim Number1 As Double = Double.Parse(total_Credit)
            Dim Number2 As Double = Double.Parse(total_Credit_EL)
            Dim Number3 As Double = Double.Parse(total_Total)
            Dim Number4 As Double = Double.Parse(total_Total_EL)

            Dim total_Hour As Double = Number1 + Number2
            Dim final_Total As Double = Number3 + Number4

            Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

            Test.Append("                   </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em;'><b> Total </b></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'> " & total_Hour & " </td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'> " & final_Total & " </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td colspan='4'style='width:8%;border: 1px solid black;text-align:left;font-size:0.8125em;'><b> Academic PNG </b></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'> </td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'> " & PNG_Akademik & " </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Cocurriculum </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'>" & cocuricullum_value & "</td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'>")

            ''kokorikulum kod sukan
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            ''kokorikulum kod kelab
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            ''kokorikulum kod uniform
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:30%;border: 1px solid black;text-align:left;font-size:0.8125em;'>")

            ''kokorikulum nama skan
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            ''kokorikulum nama kelab
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            ''kokorikulum nama uniform
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("<br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''kokorikulum gred 
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_GRED.Rows
                    For Each column As DataColumn In DSCocuricullum_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append("SM <br />")
            End If

            Test.Append("</td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''kokorikulum png 
            If Confirm_Cocuricullum = "On" Then
                For Each row As DataRow In DSCocuricullum_PNG.Rows
                    For Each column As DataColumn In DSCocuricullum_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "Off" Then
                Test.Append(" SM <br />")
            End If

            Test.Append("</td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Portfolio </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'> " & portfolio_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'>")

            ''Portfolio KOD
            If Confirm_Portfolio = "On" Then
                For Each row As DataRow In DSPortfolio_KOD.Rows
                    For Each column As DataColumn In DSPortfolio_KOD.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Portfolio = "Off" Then
                Test.Append("<br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''Portfolio Gred
            If Confirm_Portfolio = "On" Then
                For Each row As DataRow In DSPortfolio_GRED.Rows
                    For Each column As DataColumn In DSPortfolio_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Portfolio = "Off" Then
                Test.Append(" SM <br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''Portfolio PNG
            If Confirm_Portfolio = "On" Then
                For Each row As DataRow In DSPortfolio_PNG.Rows
                    For Each column As DataColumn In DSPortfolio_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br />")
                    Next
                Next
            ElseIf Confirm_Portfolio = "Off" Then
                Test.Append(" SM <br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Research </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'> " & research_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'>")

            ''research KOD
            If Confirm_Research = "On" Then
                For Each row As DataRow In DSResearch_KOD.Rows
                    For Each column As DataColumn In DSResearch_KOD.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "Off" Then
                Test.Append("<br />")
            End If

            Test.Append("</td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''research GRED
            If Confirm_Research = "On" Then
                For Each row As DataRow In DSResearch_GRED.Rows
                    For Each column As DataColumn In DSResearch_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "Off" Then
                Test.Append(" SM <br />")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''research PNG
            If Confirm_Research = "On" Then
                For Each row As DataRow In DSResearch_PNG.Rows
                    For Each column As DataColumn In DSResearch_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "Off" Then
                Test.Append(" SM <br />")
            End If

            Test.Append("</td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> Self Development </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'> " & sd_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'>")

            ''(column self development codde / pembangunan kendiri kod)
            For Each row As DataRow In DSSelfdevelopment_KOD.Rows
                For Each column As DataColumn In DSSelfdevelopment_KOD.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Test.Append("                    </td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''(column self development grade / pembangunan kendiri gred)
            For Each row As DataRow In DSSelfdevelopment_GRED.Rows
                For Each column As DataColumn In DSSelfdevelopment_GRED.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br />")
                Next
            Next

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'>")

            ''(column self development gpa / pembangunan kendiri png)
            For Each row As DataRow In DSSelfdevelopment_PNG.Rows
                For Each column As DataColumn In DSSelfdevelopment_PNG.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append(" <br />")
                Next
            Next

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> PNG </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'><b> " & gpa & " </b></td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;font-size:0.8125em;'><b> PNGK </b></td>
                                                        <td style='width:7%;border: 1px solid black;font-size:0.8125em;'><b> " & cgpa & "</b></td>
                                                        <td style='width:8%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:30%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:5%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:10%;border: 1px solid black;font-size:0.8125em;'></td>
                                                        <td style='width:15%;border: 1px solid black;font-size:0.8125em;'></td>
                                                    </tr>
                                                </table>
                                            </tr>
                                         </table>    
                                     </div>")

            Test.AppendLine(" </div> </div>")
            Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataTESTBI'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

            ''print
            Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())
        Else
            ShowMessage(" The Examination Is Closed ", MessageType.Error)
        End If
    End Sub

    Private Function BindDataHostel(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLHostel, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLHostel() As String

        Dim data_ID As String = Session("Std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""

        tmpSQL = "  select hostel_info.hostel_CampusNames,hostel_info.hostel_BlockNames,hostel_info.hostel_BlockLevels,hostel_info.hostel_Sem,room_info.room_Name,room_info.year,student_room.std_ID 
                    from hostel_info
                    left join room_info on hostel_info.hostel_ID = room_info.hostel_ID
                    left join student_room on student_room.room_ID = room_info.room_ID "

        strWhere = " where student_room.std_ID = '" & data_ID & "'"

        getSQLHostel = tmpSQL & strWhere

        Return getSQLHostel
    End Function

    Private Function BindDataDisicpline(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLDiscipline, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQLDiscipline() As String

        Dim data_ID As String = Session("Std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " order by Dicipline_Date DESC"

        tmpSQL = " select * from dicipline_info "
        strWhere = " where std_ID = '" & data_ID & "'"

        getSQLDiscipline = tmpSQL & strWhere & strOrder

        Return getSQLDiscipline
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

End Class