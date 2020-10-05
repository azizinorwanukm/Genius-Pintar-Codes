Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Data.OleDb
'Imports System.IO
'Imports System.Globalization

Partial Public Class studentprofile_create
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
                Cenderung_list()
                master_Country_list()
                ddlCountry.Text = "MALAYSIA"
                master_dobyear_list()
                Race_list()
                Religion_list()

                lblPCInfo.Text = "IP Address: " & Request.UserHostAddress & " Hostname: " & Request.UserHostName & " Browser: " & Request.Browser.Browser & " Datetime:" & Now.ToString("yyyyMMdd HH:mm:ss.fff")

                ''--set unique ID
                Dim guid = System.Guid.NewGuid.ToString
                lblStudentID.Text = guid
                Response.Cookies("studentid").Value = guid

                txtMYKAD.Text = Request.QueryString("mykad")
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            divMsg.Attributes("class") = "error"
        End Try

    End Sub

    Private Sub master_Country_list()
        strSQL = "SELECT Country FROM master_Country ORDER BY Country"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCountry.DataSource = ds
            ddlCountry.DataTextField = "Country"
            ddlCountry.DataValueField = "Country"
            ddlCountry.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub master_dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear WHERE display = 1 ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")



            ddlDobYear.DataSource = ds
            ddlDobYear.DataTextField = "DOB_Year"
            ddlDobYear.DataValueField = "DOB_Year"
            ddlDobYear.DataBind()

            ddlDobYear.Items.Add(New ListItem("Tahun", "0"))
            ddlDobYear.SelectedValue = "0"

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

            If StudentProfile_Insert() = "0" Then
                ''Response.Redirect("schoolprofile.search.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & lblStudentID.Text, False)
                Response.Redirect("default.main.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & lblStudentID.Text, False)
                ''--lblMsg.Text = "Tahniah. Rekod pendaftaran pelajar anda berjaya disimpan. MYKAD#: " & txtMYKADNo.Text
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = strRet
            End If

            '--insert into security_login_trail
            oCommon.LogTrail(lblStudentID.Text, oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "CREATE-PROFILE", oCommon.FixSingleQuotes(lblPCInfo.Text))

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean

        ''--MYKAD is unique.
        strSQL = "SELECT StudentFullname FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        strRet = oCommon.getFieldValue(strSQL)
        If strRet.Length > 0 Then
            lblMsg.Text = "MYKAD# sudah digunakan oleh " & strRet & ". Jika ini adalah MYKAD anda, sila masuk semula menggunakan MYKAD ini."
            txtMYKAD.Focus()
            Return False
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. MYKAD#!"
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
            lblMsg.Text = "Please fill-in this field. Nama Penuh!"
            txtStudentFullname.Focus()
            Return False
        End If

        '--badword
        If oCommon.isBadWord(txtStudentFullname.Text) = True Then
            '--insert into security_login_trail
            oCommon.LogTrail(lblStudentID.Text, oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "BAD-WORD", oCommon.FixSingleQuotes(txtStudentFullname.Text))

            lblMsg.Text = "Bad word detected! You PC IP Address is logged for security reason."
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

        If ddlDobYear.SelectedValue = "0" Then
            lblMsg.Text = "Please select this field. Tarikh Lahir. Tahun!"
            ddlDobYear.Focus()
            Return False

        End If

        If selStudentForm.Value = "" Then
            lblMsg.Text = "Please select this field. Darjah/Tingkatan!"
            selStudentForm.Focus()
            Return False

        End If

        If ddlRace.SelectedValue = "" Then
            lblMsg.Text = "Please select this field. Bangsa!"
            ddlRace.Focus()
            Return False

        End If

        If txtStudentAddress1.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Alamat Rumah!"
            txtStudentAddress1.Focus()
            Return False
        End If
        ''If txtStudentAddress2.Text.Length = 0 Then
        ''    lblMsg.Text = "Please fill-in this field. Alamat Rumah!"
        ''    txtStudentAddress2.Focus()
        ''    Return False
        ''End If

        If txtStudentPostcode.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Poskod!"
            txtStudentPostcode.Focus()
            Return False

        End If

        '--invalid postcode
        If oCommon.isNumeric(txtStudentPostcode.Text) = False Then
            lblMsg.Text = "Invalid field format. Numeric only!"
            txtStudentPostcode.Focus()
            Return False
        End If

        If txtStudentCity.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Bandar!"
            txtStudentCity.Focus()
            Return False

        End If

        If selStudentState.Value = "" Then
            lblMsg.Text = "Please select this field. Negeri!"
            selStudentState.Focus()
            Return False

        End If

        If txtStudentEmail.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Email!"
            txtStudentEmail.Focus()
            Return False
        End If

        '--invalid email
        '--previously using isEmail. more rigid
        If oCommon.IsEmailEx(txtStudentEmail.Text) = False Then
            lblMsg.Text = "Invalid field format. Email address only!"
            txtStudentEmail.Focus()
            Return False
        End If

        If txtFatherFullname.Text.Length = 0 Then
            lblMsg.Text = "Please complete parent/guardian information!"
            Return False
        End If

        If txtFatherFullname.Text.Length = 0 Then
            lblMsg.Text = "Please complete parent/guardian information!"
            Return False
        End If

        If txtFatherJob.Text.Length = 0 Then
            lblMsg.Text = "Please complete parent/guardian information!"
            Return False
        End If

        Return True
    End Function

    Private Function StudentProfile_Insert() As String
        Dim strStudentAge As String = Now.Year - CInt(ddlDobYear.SelectedValue)

        ''-create new studentprofile record
        strSQL = "INSERT INTO StudentProfile (StudentID,MYKAD,Pwd,StudentFullname,DOB_Day,DOB_Month,DOB_Year,StudentGender,StudentRace,StudentReligion,StudentEmail,StudentForm,StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState,StudentCountry,TalkBM,TalkBI,TalkMan,TalkTamil,TalkArab,WriteBM,WriteBI,WriteMan,WriteTamil,WriteArab,StudentContactNo,IsUpdated,Kecenderungan)" &
        " VALUES ('" & lblStudentID.Text & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "','NA','" & oCommon.FixSingleQuotes(txtStudentFullname.Text.ToUpper) & "','" & selStudentDOB_day.Value & "','" & selStudentDOB_month.Value & "','" & ddlDobYear.SelectedValue & "','" & selStudentGender.Value & "','" & ddlRace.SelectedValue & "','" & ddlReligion.SelectedValue & "','" & oCommon.FixSingleQuotes(txtStudentEmail.Text) & "','" & selStudentForm.Value & "','" & oCommon.FixSingleQuotes(txtStudentAddress1.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentAddress2.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtStudentCity.Text.ToUpper) & "','" & selStudentState.Value & "','" & ddlCountry.Text & "','" & chkTalkBM.Checked & "','" & chkTalkBI.Checked & "','" & chkTalkMan.Checked & "','" & chkTalkTamil.Checked & "','" & chkTalkArab.Checked & "','" & chkWriteBM.Checked & "','" & chkWriteBI.Checked & "','" & chkWriteMan.Checked & "','" & chkWriteTamil.Checked & "','" & chkWriteArab.Checked & "','" & oCommon.FixSingleQuotes(txtStudentContactNo.Text) & "','Y','" & ddlCenderung.SelectedValue & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''insert parent info
        ParentProfile_create()

        ''-create new ParentProfile record

        ''log
        oCommon.TransactionLog("StudentProfile_Insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

        Return strRet
    End Function

    Private Sub ParentProfile_create()
        Dim strParentID As String
        strParentID = System.Guid.NewGuid.ToString

        strSQL = "INSERT INTO ParentProfile (StudentID,ParentID,FatherMYKADNo,FatherFullname,FatherJob,FatherEducation,MotherMYKADNo,MotherFullname,MotherJob,MotherEducation,FamilyIncome,FamilyContactNo,IsUpdated) " &
        "VALUES ('" & lblStudentID.Text & "','" & strParentID & "','" & oCommon.FixSingleQuotes(txtFatherMYKADNo.Text) & "','" & oCommon.FixSingleQuotes(txtFatherFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtFatherJob.Text.ToUpper) & "','NA','" & oCommon.FixSingleQuotes(txtMotherMYKADNo.Text) & "','" & oCommon.FixSingleQuotes(txtMotherFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtMotherJob.Text.ToUpper) & "','NA','NA','" & oCommon.FixSingleQuotes(txtFamilyContactNo.Text) & "','Y')"

        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("ParentProfile_create", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

    End Sub

    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click

        Dim stringQuery As String = "INSERT INTO StudentProfile (StudentID, MYKAD) VALUES ('" & lblStudentID.Text & "','" & txtMYKAD.Text & "')"

        Debug.WriteLine(stringQuery)

        Try
            oCommon.ExecuteSQL(stringQuery)
        Catch ex As Exception

        End Try

        Response.Redirect("schoolprofile.state.select.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & lblStudentID.Text)

    End Sub

    Private Sub Race_list()
        strSQL = "SELECT StudentRace FROM master_StudentRace ORDER BY idx"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRace.DataSource = ds
            ddlRace.DataTextField = "StudentRace"
            ddlRace.DataValueField = "StudentRace"
            ddlRace.DataBind()

            ddlRace.Items.Add(New ListItem("-- Pilih Bangsa -- ", ""))
            ddlRace.SelectedValue = ""

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub Religion_list()
        strSQL = "SELECT StudentReligion FROM master_StudentReligion ORDER BY idx"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligion.DataSource = ds
            ddlReligion.DataTextField = "StudentReligion"
            ddlReligion.DataValueField = "StudentReligion"
            ddlReligion.DataBind()

            ddlReligion.Items.Add(New ListItem("-- Pilih Agama -- ", ""))
            ddlReligion.SelectedValue = ""

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub Cenderung_list()
        strSQL = "SELECT id, kecenderungan FROM master_cenderung WHERE dropdown = 1 ORDER BY idx"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCenderung.DataSource = ds
            ddlCenderung.DataTextField = "kecenderungan"
            ddlCenderung.DataValueField = "id"
            ddlCenderung.DataBind()

            ddlCenderung.Items.Add(New ListItem("-- Kecenderungan -- ", ""))
            ddlCenderung.SelectedValue = ""

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

End Class