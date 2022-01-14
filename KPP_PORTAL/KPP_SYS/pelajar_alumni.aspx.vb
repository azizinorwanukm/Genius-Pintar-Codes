Imports System.Data.SqlClient
Imports System.IO

Public Class pelajar_alumni
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim data As String = oCommon.securityLogin(Session("Std_ID"))

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then
                    loading_Page()

                    If Session("Std_Status") = "AI" Then ''Alumni Information
                        AlumniInformation.Visible = True
                        EducationalInformation.Visible = False
                        ProfessionalInformation.Visible = False

                        btnAlumniInfo.Attributes("class") = "btn btn-info font"
                        btnEducationInfo.Attributes("class") = "btn btn-default font"
                        btnProfessionalInfo.Attributes("class") = "btn btn-default font"

                        alumni_race()
                        alumni_religion()
                        alumni_state()
                        alumni_country()

                        loading_alumniInfo()

                    ElseIf Session("Std_Status") = "EI" Then ''Educational Information
                        AlumniInformation.Visible = False
                        EducationalInformation.Visible = True
                        ProfessionalInformation.Visible = False

                        btnAlumniInfo.Attributes("class") = "btn btn-default font"
                        btnEducationInfo.Attributes("class") = "btn btn-info font"
                        btnProfessionalInfo.Attributes("class") = "btn btn-default font"

                        alumni_school_type()
                        alumni_school_country()

                        strRet = BindDataAlumniEducation(datRespondent_EB)

                    ElseIf Session("Std_Status") = "PI" Then ''Professional Information
                        AlumniInformation.Visible = False
                        EducationalInformation.Visible = False
                        ProfessionalInformation.Visible = True

                        btnAlumniInfo.Attributes("class") = "btn btn-default font"
                        btnEducationInfo.Attributes("class") = "btn btn-default font"
                        btnProfessionalInfo.Attributes("class") = "btn btn-info font"

                        alumni_company_month()
                        alumni_company_country()

                        strRet = BindDataAlumniCompany(datRespondent_CB)

                    End If

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAlumniInfo_ServerClick(sender As Object, e As EventArgs) Handles btnAlumniInfo.ServerClick
        Session("Std_Status") = "AI"
        Response.Redirect("pelajar_alumni.aspx")
    End Sub

    Private Sub btnEducationInfo_ServerClick(sender As Object, e As EventArgs) Handles btnEducationInfo.ServerClick
        Session("Std_Status") = "EI"
        Response.Redirect("pelajar_alumni.aspx")
    End Sub

    Private Sub btnProfessionalInfo_ServerClick(sender As Object, e As EventArgs) Handles btnProfessionalInfo.ServerClick
        Session("Std_Status") = "PI"
        Response.Redirect("pelajar_alumni.aspx")
    End Sub

    Private Sub loading_Page()

        strSQL = "  Select UPPER(student_Name) From student_info
                    Where student_Status = 'Graduate'
                    And std_ID = '" & oCommon.Student_securityLogin(Session("Std_ID")) & "'"

        If Session("Student_Campus") = "PGPN" Then
            txtAlumniName.Text = " [&nbsp;&nbsp; WELCOME , &nbsp;&nbsp; " & oCommon.getFieldValue(strSQL) & " &nbsp;&nbsp; - &nbsp;&nbsp; ALUMNI PUSAT GENIUS@PINTAR NEGARA &nbsp;&nbsp; ] "
        ElseIf Session("Student_Campus") = "APP" Then
            txtAlumniName.Text = " [&nbsp;&nbsp; WELCOME , &nbsp;&nbsp; " & oCommon.getFieldValue(strSQL) & " &nbsp;&nbsp; - &nbsp;&nbsp; ALUMNI AKADEMIK PINTAR PENDANG &nbsp;&nbsp; ] "
        End If

        txtcurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub btnLogout_ServerClick(sender As Object, e As EventArgs) Handles btnLogout.ServerClick
        Response.Redirect("pelajar_CloseLogout.aspx?std_ID=" + Request.QueryString("std_ID"))
    End Sub

    Private Sub alumni_race()
        strSQL = "SELECT UPPER(Parameter) Parameter from setting where Type = 'Race'"
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

    Private Sub alumni_religion()
        strSQL = "SELECT UPPER(Parameter) Parameter from setting where Type = 'Religion' and Parameter is not null "
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

    Private Sub alumni_state()
        strSQL = "SELECT UPPER(Parameter) Parameter FROM setting WHERE Type='State' "
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
            ddlState.Items.Insert(1, New ListItem("OTHERS", "OTHERS"))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub alumni_country()

        If ddlState.SelectedValue <> "OTHERS" Then
            strSQL = "SELECT UPPER(Parameter) Parameter FROM setting WHERE Type='Country' and Value = 'Malaysia' "
        Else
            strSQL = "SELECT UPPER(Parameter) Parameter FROM setting WHERE Type='Country' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCountry.DataSource = ds
            ddlCountry.DataTextField = "Parameter"
            ddlCountry.DataValueField = "Parameter"
            ddlCountry.DataBind()
            ddlCountry.Items.Insert(0, New ListItem("Select Country", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlState.SelectedIndexChanged
        Try
            alumni_country()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub loading_alumniInfo()

        strSQL = "  select UPPER(student_Name) student_Name, student_ID, student_Mykad,  UPPER(student_Sex) student_Sex, UPPER(student_Email) student_Email, UPPER(student_Race) student_Race, UPPER(student_Religion) student_Religion, 
                    student_FonNo, UPPER(student_Address) student_Address, student_PostalCode, UPPER(student_State) student_State, student_Photo, UPPER(student_Batch) student_Batch,
                    UPPER(student_City) student_City, UPPER(student_CountryOfBirth) student_CountryOfBirth 
                    from student_info where std_ID = '" & oCommon.Student_securityLogin(Session("Std_ID")) & "'"

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

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Batch")) Then
                txtstudentBatch.Text = ds.Tables(0).Rows(0).Item("student_Batch")
            Else
                txtstudentBatch.Text = ""
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

                If staff_gender = "MALE" Or staff_gender = "LELAKI" Then
                    rbtn_Male.Checked = True
                ElseIf staff_gender = "FEMALE" Or staff_gender = "PEREMPUAN" Then
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

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_CountryOfBirth")) Then
                ddlCountry.SelectedValue = ds.Tables(0).Rows(0).Item("student_CountryOfBirth")
            Else

                If ddlState.SelectedValue <> "OTHERS" Then
                    ddlCountry.SelectedValue = "MALAYSIA"
                Else
                    ddlCountry.SelectedValue = ""
                End If

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

        End If
    End Sub

    Private Sub btnUpdateAlumniInfo_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateAlumniInfo.ServerClick
        Dim data_ID As String = oCommon.Student_securityLogin(Session("Std_ID"))

        If alumniInfoChecking() = False Then
            Exit Sub
        End If

        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "MALE"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "FEMALE"
        End If

        Dim imgPath As String = student_Photo.ImageUrl

        If uploadPhoto.PostedFile.FileName <> "" Then
            Dim filename As String = txtstudentMykad.Text & "_" & Path.GetFileName(uploadPhoto.PostedFile.FileName)
            imgPath = "~/student_Image/" + filename
            uploadPhoto.SaveAs(Server.MapPath(imgPath))
        End If

        'UPDATE ALUMNI DATA
        strSQL = "  UPDATE student_info SET student_Mykad='" & txtstudentMykad.Text & "',
                    student_Sex='" & strgender & "', student_Name='" & oCommon.FixSingleQuotes(txtstudentName.Text.ToUpper) & "', student_Email='" & txtstudentEmail.Text & "',
                    student_FonNo='" & txtstudentPhone.Text & "',student_Address='" & txtstudentAddress.Text.ToUpper & "',
                    student_City='" & txtstudentCity.Text & "', student_State='" & ddlState.SelectedValue & "', student_Race='" & ddlRace.SelectedValue & "', student_Religion='" & ddlReligion.SelectedValue & "',
                    student_PostalCode='" & txtstudentPostcode.Text & "', student_Photo = '" & imgPath & "' WHERE std_ID = '" & data_ID & "' "
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "  INSERT INTO alumni_studentData(std_id,spouseName,spouseAge,noChild) values('" & data_ID & "','" & txtSpouseName.Text.ToUpper & "','" & txtSpouseAge.Text & "','" & txtChilderNo.Text & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage(" Update Alumni Information", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Update Alumni Information", MessageType.Error)
        End If

    End Sub

    Private Function alumniInfoChecking()

        If txtstudentName.Text.Length = 0 Then
            ShowMessage(" Please Fill In Student Name", MessageType.Error)
            Return False
        End If

        If ddlRace.SelectedIndex = 0 Then
            ShowMessage(" Please Select Race", MessageType.Error)
            Return False
        End If

        If ddlReligion.SelectedIndex = 0 Then
            ShowMessage(" Please Select Religion", MessageType.Error)
            Return False
        End If

        If rbtn_Male.Checked = False And rbtn_Female.Checked = False Then
            ShowMessage(" Please Select Gender", MessageType.Error)
            Return False
        End If

        If txtstudentEmail.Text.Length = 0 Then
            ShowMessage(" Please Fill In Email Address", MessageType.Error)
            Return False
        End If

        If txtstudentPhone.Text.Length = 0 Or Regex.IsMatch(txtstudentPhone.Text, "^[A-Za-z]+$") Then
            ShowMessage(" Please Fill In Phone No [0-9]", MessageType.Error)
            Return False
        End If

        If txtstudentAddress.Text.Length = 0 Then
            ShowMessage(" Please Fill In Home Address", MessageType.Error)
            Return False
        End If

        If txtstudentCity.Text.Length = 0 Then
            ShowMessage(" Please Fill In City", MessageType.Error)
            Return False
        End If

        If txtstudentPostcode.Text.Length = 0 Or Regex.IsMatch(txtstudentPostcode.Text, "^[A-Za-z]+$") Then
            ShowMessage(" Please Fill In Zip Code", MessageType.Error)
            Return False
        End If

        If ddlState.SelectedIndex = 0 Then
            ShowMessage(" Please Select State", MessageType.Error)
            Return False
        End If

        If ddlCountry.SelectedIndex = 0 Then
            ShowMessage(" Please Select Country", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Sub alumni_school_country()
        strSQL = "select UPPER(value) value from setting where type = 'country'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniCountry.DataSource = ds
            ddlUniCountry.DataTextField = "value"
            ddlUniCountry.DataValueField = "value"
            ddlUniCountry.DataBind()
            ddlUniCountry.Items.Insert(0, New ListItem("Select Location", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub alumni_school_type()
        strSQL = "select UPPER(value) value from setting where type = 'institution type' and idx = 'alumni'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniType.DataSource = ds
            ddlUniType.DataTextField = "value"
            ddlUniType.DataValueField = "value"
            ddlUniType.DataBind()
            ddlUniType.Items.Insert(0, New ListItem("Select Institution Type", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataAlumniEducation(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLAlumniEducation, strConn)
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

    Private Function getSQLAlumniEducation() As String

        Dim data_ID As String = oCommon.Student_securityLogin(Session("Std_ID"))

        Dim tmpSQL As String
        Dim strOrderby As String = " order by AEB_SchoolStartsYear asc"

        tmpSQL = " select * from alumni_educationBackground where AEB_StdID = '" & data_ID & "'"

        getSQLAlumniEducation = tmpSQL & strOrderby

        Return getSQLAlumniEducation
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent_EB.RowDeleting
        Try
            Dim strKeyName As String = datRespondent_EB.DataKeys(e.RowIndex).Value.ToString

            strSQL = "delete alumni_educationBackground where AEB_ID = '" & strKeyName & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Delete Alumni Education Information", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Delete Alumni Education Information", MessageType.Error)
            End If

            strRet = BindDataAlumniEducation(datRespondent_EB)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdateEducation_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateEducation.ServerClick
        Dim data_ID As String = oCommon.Student_securityLogin(Session("Std_ID"))

        If alumniEducationChecking() = False Then
            Exit Sub
        End If

        strSQL = "  INSERT INTO alumni_educationBackground(AEB_SchoolName,AEB_SchoolType,AEB_SchoolStartsYear,AEB_SchoolEndsYear,AEB_SchoolCountry,AEB_StdID,AEB_SchoolCourseName,AEB_SchoolSponsorship) 
                    values('" & txtUniName.Text.ToUpper & "','" & ddlUniType.SelectedValue & "','" & txtUniStart.Text & "','" & txtUniEnd.Text & "','" & ddlUniCountry.SelectedValue & "','" & data_ID & "','" & txtUniCourse.Text.ToUpper & "','" & txtUniSponsorship.Text.ToUpper & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage(" Update Alumni Education Information", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Update Alumni Education Information", MessageType.Error)
        End If

        strRet = BindDataAlumniEducation(datRespondent_EB)

    End Sub

    Private Function alumniEducationChecking()

        If txtUniName.Text.Length = 0 Then
            ShowMessage(" Please Fill In Institution Name", MessageType.Error)
            Return False
        End If

        If ddlUniType.SelectedIndex = 0 Then
            ShowMessage(" Please Select Institution Type", MessageType.Error)
            Return False
        End If

        If ddlUniCountry.SelectedIndex = 0 Then
            ShowMessage(" Please Select Location", MessageType.Error)
            Return False
        End If

        If txtUniStart.Text.Length = 0 Then
            ShowMessage(" Please Fill In Start Year", MessageType.Error)
            Return False
        End If

        If txtUniEnd.Text.Length = 0 Then
            ShowMessage(" Please Fill In End Year", MessageType.Error)
            Return False
        End If

        If txtUniSponsorship.Text.Length = 0 Then
            ShowMessage(" Please Fill In Sponsorship", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Sub alumni_company_month()
        strSQL = "select UPPER(Parameter) Parameter from setting where type = 'month' and idx = 'Date'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCompanyStartMonth.DataSource = ds
            ddlCompanyStartMonth.DataTextField = "Parameter"
            ddlCompanyStartMonth.DataValueField = "Parameter"
            ddlCompanyStartMonth.DataBind()
            ddlCompanyStartMonth.Items.Insert(0, New ListItem("Select Month", String.Empty))

            ddlCompanyEndMonth.DataSource = ds
            ddlCompanyEndMonth.DataTextField = "Parameter"
            ddlCompanyEndMonth.DataValueField = "Parameter"
            ddlCompanyEndMonth.DataBind()
            ddlCompanyEndMonth.Items.Insert(0, New ListItem("Select Month", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub alumni_company_country()

        strSQL = "SELECT UPPER(Parameter) Parameter FROM setting WHERE Type='Country' "

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCompanyLocation.DataSource = ds
            ddlCompanyLocation.DataTextField = "Parameter"
            ddlCompanyLocation.DataValueField = "Parameter"
            ddlCompanyLocation.DataBind()
            ddlCompanyLocation.Items.Insert(0, New ListItem("Select Location", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub checkWorkHere_CheckedChanged(sender As Object, e As EventArgs) Handles checkWorkHere.CheckedChanged
        Try
            If checkWorkHere.Checked = True Then
                ddlCompanyEndMonth.Enabled = False
                txtCompanyEndYear.Enabled = False

            ElseIf checkWorkHere.Checked = False Then
                ddlCompanyEndMonth.Enabled = True
                txtCompanyEndYear.Enabled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataAlumniCompany(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLAlumniCompany, strConn)
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

    Private Function getSQLAlumniCompany() As String

        Dim data_ID As String = oCommon.Student_securityLogin(Session("Std_ID"))

        Dim tmpSQL As String
        Dim strOrderby As String = " Order By ACB_CompanyMonthStart Asc"

        tmpSQL = "  Select ACB_ID, UPPER(ACB_CompanyName) ACB_CompanyName, UPPER(ACB_CompanyPosition) ACB_CompanyPosition, UPPER(ACB_CompanyLocation) ACB_CompanyLocation, 
                    CONCAT(UPPER(ACB_CompanyMonthStart),' ',ACB_CompanyYearStart) As ACB_CompanyMonthStart, CONCAT(UPPER(ACB_CompanyMonthEnd),' ',ACB_CompanyYearEnd) As ACB_CompanyMonthEnd 
                    from alumni_companyBackground where ACB_StdID = '" & data_ID & "'"

        getSQLAlumniCompany = tmpSQL & strOrderby

        Return getSQLAlumniCompany
    End Function

    Private Sub btnUpdateCompany_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateCompany.ServerClick
        Dim data_ID As String = oCommon.Student_securityLogin(Session("Std_ID"))

        If alumniCompanyChecking() = False Then
            Exit Sub
        End If

        If checkWorkHere.Checked = True Then

            strSQL = "  Insert Into alumni_companyBackground(ACB_CompanyName,ACB_CompanyPosition,ACB_CompanyLocation,ACB_CompanyMonthStart,ACB_CompanyYearStart,ACB_CompanyCB,ACB_StdID)
                        values('" & txtCompanyName.Text.ToUpper & "','" & txtCompanyPosition.Text.ToUpper & "','" & ddlCompanyLocation.SelectedValue & "','" & ddlCompanyStartMonth.SelectedValue & "','" & txtCompanyStartYear.Text & "','" & checkWorkHere.Checked & "','" & data_ID & "')"

        ElseIf checkWorkHere.Checked = False Then

            strSQL = "  Insert Into alumni_companyBackground(ACB_CompanyName,ACB_CompanyPosition,ACB_CompanyLocation,ACB_CompanyMonthStart,ACB_CompanyYearStart,ACB_CompanyMonthEnd,ACB_CompanyYearEnd,ACB_CompanyCB,ACB_StdID)
                        values('" & txtCompanyName.Text.ToUpper & "','" & txtCompanyPosition.Text.ToUpper & "','" & ddlCompanyLocation.SelectedValue & "','" & ddlCompanyStartMonth.SelectedValue & "','" & txtCompanyStartYear.Text & "','" & ddlCompanyEndMonth.SelectedValue & "','" & txtCompanyEndYear.Text & "','" & checkWorkHere.Checked & "','" & data_ID & "')"

        End If

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage(" Update Alumni Company Information", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Update Alumni Company Information", MessageType.Error)
        End If

        strRet = BindDataAlumniCompany(datRespondent_CB)

    End Sub

    Private Function alumniCompanyChecking()

        If txtCompanyName.Text.Length = 0 Then
            ShowMessage(" Please Fill In Company Name", MessageType.Error)
            Return False
        End If

        If txtCompanyPosition.Text.Length = 0 Then
            ShowMessage(" Please Fill In Position", MessageType.Error)
            Return False
        End If

        If ddlCompanyLocation.SelectedIndex = 0 Then
            ShowMessage(" Please Select Location", MessageType.Error)
            Return False
        End If

        If ddlCompanyStartMonth.SelectedIndex = 0 Then
            ShowMessage(" Please Select Start Date", MessageType.Error)
            Return False
        End If

        If txtCompanyStartYear.Text.Length = 0 Then
            ShowMessage(" Please Fill In Start Date", MessageType.Error)
            Return False
        End If

        If ddlCompanyEndMonth.SelectedIndex = 0 And checkWorkHere.Checked = False Then
            ShowMessage(" Please Select End Date", MessageType.Error)
            Return False
        End If

        If txtCompanyEndYear.Text.Length = 0 And checkWorkHere.Checked = False Then
            ShowMessage(" Please Fill In End Date", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Sub datRespondent_CB_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent_CB.RowDeleting
        Try
            Dim strKeyName As String = datRespondent_CB.DataKeys(e.RowIndex).Value.ToString

            strSQL = "delete alumni_companyBackground where ACB_ID = '" & strKeyName & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Delete Alumni Profesional Information", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Delete Alumni Profesional Information", MessageType.Error)
            End If

            strRet = BindDataAlumniCompany(datRespondent_CB)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

End Class