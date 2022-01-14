Imports System.Data.SqlClient
Imports System.IO

Public Class pelajar_daftar_baru
    Inherits System.Web.UI.Page

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

                Dim getStatus As String = Session("NewStudent_SignUp")

                If getStatus = "SI" Then ''student information
                    StudentInformation.Visible = True
                    FamilyInformation.Visible = False
                    StudentTutorial.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-info"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnTutorial.Attributes("class") = "btn btn-default font"

                    Race_List()
                    Religion_List()
                    State_list()
                    LoadPageStudent()

                ElseIf getStatus = "FI" Then ''family information
                    StudentInformation.Visible = False
                    FamilyInformation.Visible = True
                    StudentTutorial.Visible = False

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-info"
                    btnTutorial.Attributes("class") = "btn btn-default font"

                    Salary_list()
                    Guardian1_Load()
                    Guardian2_Load()

                ElseIf getStatus = "SRT" Then ''student registration tutorial
                    StudentInformation.Visible = False
                    FamilyInformation.Visible = False
                    StudentTutorial.Visible = True

                    btnStudentInfo.Attributes("class") = "btn btn-default font"
                    btnParentInfo.Attributes("class") = "btn btn-default font"
                    btnTutorial.Attributes("class") = "btn btn-info"

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnStudentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnStudentInfo.ServerClick
        Session("NewStudent_SignUp") = "SI"
        Response.Redirect("pelajar_daftar_baru.aspx?StudentID=" + Request.QueryString("StudentID"))
    End Sub

    Private Sub btnParentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnParentInfo.ServerClick
        Session("NewStudent_SignUp") = "FI"
        Response.Redirect("pelajar_daftar_baru.aspx?StudentID=" + Request.QueryString("StudentID"))
    End Sub

    Private Sub btnTutorial_ServerClick(sender As Object, e As EventArgs) Handles btnTutorial.ServerClick
        Session("NewStudent_SignUp") = "SRT"
        Response.Redirect("pelajar_daftar_baru.aspx?StudentID=" + Request.QueryString("StudentID"))
    End Sub

    Private Sub btnLogout_ServerClick(sender As Object, e As EventArgs) Handles btnLogout.ServerClick
        Session("NewStudent_SignUp") = ""
        Response.Redirect("default.aspx")
    End Sub

    Private Sub Race_List()
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
            ddlRace.Items.Insert(0, New ListItem("Select Race", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Religion_List()
        strSQL = "SELECT Parameter from setting where Type = 'Religion' and Parameter is not null "
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
            ddlReligion.Items.Insert(0, New ListItem("Select Religion", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub State_list()
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

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub    

    Private Sub LoadPageStudent()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

        Dim find_KPP_STDID As String = "Select std_ID from student_info where student_Mykad = '" & Session("std_Mykad") & "' and student_Status = 'Access' and student_Stream = 'Temp' "
        Dim get_KPP_STDID As String = oCommon.getFieldValue(find_KPP_STDID)

        If get_KPP_STDID.Length <> 0 Then

            strSQL = "  Select * from StudentProfile where StudentID = '" & Request.QueryString("StudentID") & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentFullName")) Then
                    txtstudentName.Text = ds.Tables(0).Rows(0).Item("StudentFullName")
                Else
                    txtstudentName.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    txtstudentMykad.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    txtstudentMykad.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentEmail")) Then
                    txtstudentEmail.Text = ds.Tables(0).Rows(0).Item("StudentEmail")
                Else
                    txtstudentEmail.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentGender")) Then
                    Dim staff_gender As String = ds.Tables(0).Rows(0).Item("StudentGender")

                    If staff_gender = "Male" Or staff_gender = "MALE" Or staff_gender = "Lelaki" Or staff_gender = "LELAKI" Then
                        rbtn_Male.Checked = True
                    ElseIf staff_gender = "Female" Or staff_gender = "FEMALE" Or staff_gender = "Perempuan" Or staff_gender = "PEREMPUAN" Then
                        rbtn_Female.Checked = True
                    End If
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentContactNo")) Then
                    txtstudentPhone.Text = ds.Tables(0).Rows(0).Item("StudentContactNo")
                Else
                    txtstudentPhone.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentAddress1")) Then
                    txtstudentAddress.Text = ds.Tables(0).Rows(0).Item("StudentAddress1")
                Else
                    txtstudentAddress.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentCity")) Then
                    txtstudentCity.Text = ds.Tables(0).Rows(0).Item("StudentCity")
                Else
                    txtstudentCity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentState")) Then
                    ddlState.SelectedValue = ds.Tables(0).Rows(0).Item("StudentState")
                Else
                    ddlState.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentPostcode")) Then
                    txtstudentPostcode.Text = ds.Tables(0).Rows(0).Item("StudentPostcode")
                Else
                    txtstudentPostcode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentRace")) Then
                    ddlRace.SelectedValue = ds.Tables(0).Rows(0).Item("StudentRace")
                Else
                    ddlRace.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentReligion")) Then
                    ddlReligion.SelectedValue = ds.Tables(0).Rows(0).Item("StudentReligion")
                Else
                    ddlReligion.SelectedValue = ""
                End If

                txtstudentLevel.Text = "Foundation 1"
                txtstudentYear.Text = Now.Year
                txtstudentSem.Text = "Sem 1"
                student_Photo.ImageUrl = "student_Image/user.png"

            End If

        Else
            strSQL = "  Select * from student_info where student_Mykad = '" & Session("std_Mykad") & "' and student_Status = 'Access' and student_Stream = 'Temp'"

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_PostalCode")) Then
                    txtstudentPostcode.Text = ds.Tables(0).Rows(0).Item("student_PostalCode")
                Else
                    txtstudentPostcode.Text = ""
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

                txtstudentLevel.Text = "Foundation 1"
                txtstudentYear.Text = Now.Year
                txtstudentSem.Text = "Sem 1"
            End If
        End If
    End Sub

    Private Sub btnUpdateStudentInfo_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStudentInfo.ServerClick

        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        If txtstudentMykad.Text <> "" And txtstudentMykad.Text.Length < 13 And IsNumeric(txtstudentMykad.Text) And Regex.IsMatch(txtstudentMykad.Text, "^[0-9]+$") Then

            If txtstudentPostcode.Text <> "" Or IsNumeric(txtstudentPostcode.Text) And Regex.IsMatch(txtstudentPostcode.Text, "^[0-9]+$") Then

                If txtstudentName.Text <> "" And Not IsNothing(txtstudentName.Text) Then

                    If txtstudentPhone.Text <> "" Then

                        If txtstudentCity.Text <> "" Or txtstudentCity.Text.Length > 0 Then

                            If strgender <> "" Then

                                If ddlState.SelectedIndex > 0 Then

                                    If ddlReligion.SelectedIndex > 0 Then

                                        If ddlRace.SelectedIndex > 0 Then

                                            If txtstudentEmail.Text = "" Or Regex.IsMatch(txtstudentEmail.Text, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$") Then

                                                Dim find_STDMYKAD As String = " Select std_ID from student_info where student_Mykad = '" & txtstudentMykad.Text & "' and student_Status = 'Access'"
                                                Dim get_STDMYKAD As String = oCommon.getFieldValue(find_STDMYKAD)

                                                If get_STDMYKAD.Length > 0 Then

                                                    Dim imgPath As String = "~/student_Image/user.png"

                                                    If uploadPhoto.PostedFile.FileName <> "" Then

                                                        Dim filename As String = txtstudentMykad.Text & "_" & Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                                        imgPath = "~/student_Image/" + filename
                                                        uploadPhoto.SaveAs(Server.MapPath(imgPath))
                                                    End If

                                                    strSQL = "  Update student_info set student_Mykad = '" & txtstudentMykad.Text & "', student_Sex = '" & strgender & "', student_Name = '" & oCommon.FixSingleQuotes(txtstudentName.Text.ToUpper) & "', 
                                                                student_Email = '" & txtstudentEmail.Text & "', student_FonNo = '" & txtstudentPhone.Text & "', student_Address = '" & txtstudentAddress.Text & "', student_City = '" & txtstudentCity.Text & "',
                                                                student_State = '" & ddlState.SelectedValue & "', student_Race = '" & ddlRace.SelectedValue & "', student_Religion = '" & ddlReligion.SelectedValue & "', 
                                                                student_PostalCode = '" & txtstudentPostcode.Text & "', student_Status = 'Access', student_Password = '" & txtstudentMykad.Text & "', student_Photo = '" & imgPath & "', student_Stream = 'Temp'
                                                                where std_ID = '" & get_STDMYKAD & "'"
                                                    strRet = oCommon.ExecuteSQL(strSQL)

                                                    If strRet = "0" Then

                                                        strSQL = "Select std_ID from student_info where student_Mykad = '" & txtstudentMykad.Text & "' and student_Stream = 'Temp' "
                                                        strRet = oCommon.getFieldValue(strSQL)

                                                        strSQL = "Update student_Level set Registered = 'Yes' where std_ID = '" & strRet & "'"
                                                        strRet = oCommon.ExecuteSQL(strSQL)

                                                        If strRet = "0" Then

                                                            ShowMessage(" Register Student Information ", MessageType.Success)
                                                        Else
                                                            ShowMessage(" Unable To Register Student Information ", MessageType.Error)
                                                        End If
                                                    Else
                                                        ShowMessage(" Unable To Register Student Information ", MessageType.Error)
                                                    End If

                                                    'If strRet = "0" Then

                                                    '    strSQL = "  Insert into student_level(std_ID,student_Sem,student_Level,year,month,day)
                                                    '                values('" & get_STDMYKAD & "','" & txtstudentSem.Text & "','" & txtstudentLevel.Text & "','" & txtstudentYear.Text & "','" & Now.Month & "','" & Now.Day & "')"
                                                    '    strRet = oCommon.ExecuteSQL(strSQL)

                                                    '    strSQL = "Insert into course(std_ID,year) values('" & get_STDMYKAD & "','" & txtstudentYear.Text & "')"
                                                    '    strRet = oCommon.ExecuteSQL(strSQL)

                                                    '    If strRet = "0" Then
                                                    '        ShowMessage(" Register Student Information ", MessageType.Success)
                                                    '    Else
                                                    '        ShowMessage(" Unable To Register Student Information ", MessageType.Error)
                                                    '    End If
                                                    'Else
                                                    '    ShowMessage(" Unable To Register Student Information ", MessageType.Error)
                                                    'End If
                                                Else
                                                    ShowMessage(" Student Information Had Been Registerd In Pusat Genius@PINTAR Negara", MessageType.Error)
                                                End If
                                            Else
                                                ShowMessage(" Invalid Email Format", MessageType.Error)
                                            End If
                                        Else
                                            ShowMessage(" Please Select Race ", MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage(" Please Select Religion ", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage(" Please Select State ", MessageType.Error)
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

    Private Sub Salary_list()
        strSQL = "SELECT Parameter from setting where Type = 'Salary' and Parameter is not null "
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
            ddlsalaryP1.Items.Insert(0, New ListItem("Select Salary", String.Empty))

            ddlsalaryP2.DataSource = ds
            ddlsalaryP2.DataTextField = "Parameter"
            ddlsalaryP2.DataValueField = "Parameter"
            ddlsalaryP2.DataBind()
            ddlsalaryP2.Items.Insert(0, New ListItem("Select Salary", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Guardian1_Load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

        Dim find_KPP_FatherID As String = "Select parent_fatherID from student_info where student_Mykad = '" & Session("std_Mykad") & "' and student_Status = 'Access' and student_Stream = 'Temp' "
        Dim get_KPP_FatherID As String = oCommon.getFieldValue(find_KPP_FatherID)

        If get_KPP_FatherID.Length = 0 Then

            strSQL = " select * from ParentProfile where StudentID = '" & Request.QueryString("StudentID") & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherFullname")) Then
                    Parent1_Name.Text = ds.Tables(0).Rows(0).Item("FatherFullname")
                Else
                    Parent1_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherMYKADNo")) Then
                    Parent1_IC.Text = ds.Tables(0).Rows(0).Item("FatherMYKADNo")
                Else
                    Parent1_IC.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FamilyContactNo")) Then
                    Parent1_MobileNo.Text = ds.Tables(0).Rows(0).Item("FamilyContactNo")
                Else
                    Parent1_MobileNo.Text = ""
                End If

                Parent1_Email.Text = ""

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherJob")) Then
                    Parent1_Work.Text = ds.Tables(0).Rows(0).Item("FatherJob")
                Else
                    Parent1_Work.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherSalary")) Then
                    ddlsalaryP1.Text = ds.Tables(0).Rows(0).Item("FatherSalary")
                Else
                    ddlsalaryP1.Text = ""
                End If
            End If
        Else

            strSQL = "  Select * from parent_Info 
                        Left Join student_info ON parent_Info.parent_ID = student_info.parent_fatherID 
                        WHERE student_info.student_Mykad = '" & Session("std_Mykad") & "' and parent_info.parent_No = '1'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Name")) Then
                    Parent1_Name.Text = ds.Tables(0).Rows(0).Item("parent_Name")
                Else
                    Parent1_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_IC")) Then
                    Parent1_IC.Text = ds.Tables(0).Rows(0).Item("parent_IC")
                Else
                    Parent1_IC.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                    Parent1_MobileNo.Text = ds.Tables(0).Rows(0).Item("parent_MobileNo")
                Else
                    Parent1_MobileNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Email")) Then
                    Parent1_Email.Text = ds.Tables(0).Rows(0).Item("parent_Email")
                Else
                    Parent1_Email.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Work")) Then
                    Parent1_Work.Text = ds.Tables(0).Rows(0).Item("parent_Work")
                Else
                    Parent1_Work.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Salary")) Then
                    ddlsalaryP1.Text = ds.Tables(0).Rows(0).Item("parent_Salary")
                Else
                    ddlsalaryP1.Text = ""
                End If
            End If

        End If
    End Sub

    Private Sub Guardian2_Load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

        Dim find_KPP_FatherID As String = "Select parent_fatherID from student_info where student_Mykad = '" & Session("std_Mykad") & "' and student_Status = 'Access' and student_Stream = 'Temp' "
        Dim get_KPP_FatherID As String = oCommon.getFieldValue(find_KPP_FatherID)

        If get_KPP_FatherID.Length = 0 Then

            strSQL = " select * from ParentProfile where StudentID = '" & Request.QueryString("StudentID") & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MotherFullname")) Then
                    Parent2_Name.Text = ds.Tables(0).Rows(0).Item("MotherFullname")
                Else
                    Parent2_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MotherMYKADNo")) Then
                    Parent2_IC.Text = ds.Tables(0).Rows(0).Item("MotherMYKADNo")
                Else
                    Parent2_IC.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FamilyContactNo")) Then
                    Parent2_MobileNo.Text = ds.Tables(0).Rows(0).Item("FamilyContactNo")
                Else
                    Parent2_MobileNo.Text = ""
                End If

                Parent1_Email.Text = ""

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MotherJob")) Then
                    Parent2_Work.Text = ds.Tables(0).Rows(0).Item("MotherJob")
                Else
                    Parent2_Work.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MotherSalary")) Then
                    ddlsalaryP2.Text = ds.Tables(0).Rows(0).Item("MotherSalary")
                Else
                    ddlsalaryP2.Text = ""
                End If
            End If
        Else

            strSQL = "  Select * from parent_Info 
                        Left Join student_info ON parent_Info.parent_ID = student_info.parent_motherID 
                        WHERE student_info.student_Mykad = '" & Session("std_Mykad") & "' and parent_info.parent_No = '2'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Name")) Then
                    Parent2_Name.Text = ds.Tables(0).Rows(0).Item("parent_Name")
                Else
                    Parent2_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_IC")) Then
                    Parent2_IC.Text = ds.Tables(0).Rows(0).Item("parent_IC")
                Else
                    Parent2_IC.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                    Parent2_MobileNo.Text = ds.Tables(0).Rows(0).Item("parent_MobileNo")
                Else
                    Parent2_MobileNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Email")) Then
                    Parent2_Email.Text = ds.Tables(0).Rows(0).Item("parent_Email")
                Else
                    Parent2_Email.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Work")) Then
                    Parent2_Work.Text = ds.Tables(0).Rows(0).Item("parent_Work")
                Else
                    Parent2_Work.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("parent_Salary")) Then
                    ddlsalaryP2.Text = ds.Tables(0).Rows(0).Item("parent_Salary")
                Else
                    ddlsalaryP2.Text = ""
                End If
            End If

        End If
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim data_P1 As String = "False"
        Dim data_P2 As String = "False"

        If checkData_Parent1() = True Then
            If checkData_Parent2() = True Then

                data_P1 = "True"

                Dim exist_fatherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent1_IC.Text & "'"
                Dim data_fatherID As String = oCommon.getFieldValue(exist_fatherID)

                If data_fatherID.Length = 0 Then

                    Dim P1_RStatus As String = ""

                    If Parent1_RadioStatusAlive.Checked = True Then
                        P1_RStatus = "Alive"
                    ElseIf Parent1_RadioStatusPassAway.Checked = True Then
                        P1_RStatus = "Passed Away"
                    End If

                    strSQL = "  INSERT INTO parent_Info(parent_No,parent_Name,parent_IC,parent_MobileNo,parent_Email,parent_Work,parent_Salary,parent_Password,parent_Status,parent_Condition) 
                                values ('1' ,'" & oCommon.FixSingleQuotes(Parent1_Name.Text.ToUpper) & "','" & Parent1_IC.Text & "','" & Parent1_MobileNo.Text & "','" & Parent1_Email.Text & "',
                                '" & Parent1_Work.Text.ToUpper & "','" & ddlsalaryP1.SelectedValue & "','" & Parent1_IC.Text & "','" & Parent1_Connection.Text & "','" & P1_RStatus & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then
                        ShowMessage(" Register Guardian 1 Information ", MessageType.Success)
                    Else
                        ShowMessage(" Unable To Register Guardian 1 Information ", MessageType.Error)
                    End If
                End If

                data_P2 = "True"

                Dim exist_motherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent2_IC.Text & "'"
                Dim data_motherID As String = oCommon.getFieldValue(exist_motherID)

                If data_motherID.Length = 0 Then

                    Dim P2_RStatus As String = ""

                    If Parent2_RadioStatusAlive.Checked = True Then
                        P2_RStatus = "Alive"
                    ElseIf Parent2_RadioStatusPassAway.Checked = True Then
                        P2_RStatus = "Passed Away"
                    End If

                    strSQL = "  INSERT INTO parent_Info(parent_No,parent_Name,parent_IC,parent_MobileNo,parent_Email,parent_Work,parent_Salary,parent_Password,parent_Status,parent_Condition) 
                                values ('2' ,'" & oCommon.FixSingleQuotes(Parent2_Name.Text.ToUpper) & "','" & Parent2_IC.Text & "','" & Parent2_MobileNo.Text & "','" & Parent2_Email.Text & "',
                                '" & Parent2_Work.Text.ToUpper & "','" & ddlsalaryP2.SelectedValue & "','" & Parent2_IC.Text & "','" & Parent2_Connection.Text & "','" & P2_RStatus & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then
                        ShowMessage(" Register Guardian 2 Information ", MessageType.Success)
                    Else
                        ShowMessage(" Unable To Register Guardian 2 Information ", MessageType.Error)
                    End If
                End If
            End If
        End If

        If data_P1 = "True" And data_P2 = "True" Then
            Dim fatherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent1_IC.Text & "'"
            Dim ExistFatherID As String = oCommon.getFieldValue(fatherID)

            Dim motherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent2_IC.Text & "'"
            Dim ExistMotherID As String = oCommon.getFieldValue(motherID)

            strSQL = "UPDATE student_info set parent_fatherID ='" & ExistFatherID & "',parent_motherID='" & ExistMotherID & "' where student_Mykad = '" & Session("std_Mykad") & "' and student_Status = 'Access' and student_Stream = 'Temp'"
            strRet = oCommon.ExecuteSQL(strSQL)

            strSQL = "Select std_ID from student_info where student_Mykad = '" & Session("std_Mykad") & "'"
            strRet = oCommon.getFieldValue(strSQL)

            strSQL = "Update student_Level set Registered = 'Yes' where std_ID = '" & strRet & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

    End Sub

    Private Function checkData_Parent1()
        If Parent1_Name.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 1 Name Information ", MessageType.Error)
            Return False
        End If

        If Parent1_IC.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 1 Mykad / Passport No Information ", MessageType.Error)
            Return False
        End If

        If Parent1_MobileNo.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 1 Phone No Information ", MessageType.Error)
            Return False
        End If

        If Parent1_Connection.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 1 Connection Information ", MessageType.Error)
            Return False
        End If

        If Parent1_Work.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 1 Occupation Information ", MessageType.Error)
            Return False
        End If

        If ddlsalaryP1.SelectedIndex = 0 Then
            ShowMessage(" Please Fill In Guardian 1 Salary (RM) Information ", MessageType.Error)
            Return False
        End If

        If Parent1_RadioStatusAlive.Checked = False And Parent1_RadioStatusPassAway.Checked = False Then
            ShowMessage(" Please Fill In Guardian 1 Status Information ", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Function checkData_Parent2()
        If Parent2_Name.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 2 Name Information ", MessageType.Error)
            Return False
        End If

        If Parent2_IC.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 2 Mykad / Passport No Information ", MessageType.Error)
            Return False
        End If

        If Parent2_MobileNo.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 2 Phone No Information ", MessageType.Error)
            Return False
        End If

        If Parent2_Connection.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 2 Connection Information ", MessageType.Error)
            Return False
        End If

        If Parent2_Work.Text.Length = 0 Then
            ShowMessage(" Please Fill In Guardian 2 Occupation Information ", MessageType.Error)
            Return False
        End If

        If ddlsalaryP2.SelectedIndex = 0 Then
            ShowMessage(" Please Fill In Guardian 2 Salary (RM) Information ", MessageType.Error)
            Return False
        End If

        If Parent2_RadioStatusAlive.Checked = False And Parent2_RadioStatusPassAway.Checked = False Then
            ShowMessage(" Please Fill In Guardian 2 Status Information ", MessageType.Error)
            Return False
        End If

        Return True
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