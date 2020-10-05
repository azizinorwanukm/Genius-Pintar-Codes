Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

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
            ''--set unique ID
            lblStudentID.Text = System.Guid.NewGuid.ToString
            Response.Cookies("studentid").Value = lblStudentID.Text

            txtMYKAD.Text = Request.QueryString("mykad")
        Catch ex As Exception
            lblMsg.Text = ex.Message
            divMsg.Attributes("class") = "error"
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

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean

        ''--MYKAD is unique.
        strSQL = "SELECT StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        strRet = oCommon.getFieldValue(strSQL)
        If strRet.Length > 0 Then
            lblMsg.Text = "MYKAD/MYKID# sudah digunakan oleh " & strRet & ". Jika ini adalah MYKAD anda, sila masuk semula menggunakan MYKAD ini."
            txtMYKAD.Focus()
            Return False
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. MYKAD/MYKID#!"
            txtMYKAD.Focus()
            Return False
        End If

        ''Alow for birthcert without - or space
        'If oCommon.isNumeric(txtMYKAD.Text) = False Then
        '    lblMsg.Text = "Invalid MYKAD format. Fill in numbers only! [0 - 9]"
        '    txtMYKAD.Focus()
        '    Return False
        'End If

        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Kata Laluan#!"
            txtPwd.Focus()
            Return False
        End If

        If Not txtPwd.Text = txtPwdVerify.Text Then
            lblMsg.Text = "Kata laluan dan Ulang kata luan tidak sama."
            txtPwd.Focus()
            Return False
        End If

        If txtStudentFullname.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Nama Penuh!"
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

        If selStudentDOB_year.Value = "" Then
            lblMsg.Text = "Please select this field. Tarikh Lahir. Tahun!"
            selStudentDOB_year.Focus()
            Return False

        End If

        If selStudentForm.Value = "" Then
            lblMsg.Text = "Please select this field. Darjah/Tingkatan!"
            selStudentForm.Focus()
            Return False

        End If

        If selStudentRace.Value = "" Then
            lblMsg.Text = "Please select this field. Bangsa!"
            selStudentRace.Focus()
            Return False

        End If

        If txtStudentAddress1.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Alamat Rumah!"
            txtStudentAddress1.Focus()
            Return False
        End If
        ''If txtStudentAddress2.Text.Length = 0 Then
        ''    lblMsg.Text = "Sila masukkan medan ini. Alamat Rumah!"
        ''    txtStudentAddress2.Focus()
        ''    Return False
        ''End If

        If txtStudentPostcode.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Poskod!"
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
            lblMsg.Text = "Sila masukkan medan ini. Bandar!"
            txtStudentCity.Focus()
            Return False

        End If

        If selStudentState.Value = "" Then
            lblMsg.Text = "Please select this field. Negeri!"
            selStudentState.Focus()
            Return False

        End If

        If txtStudentEmail.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Email!"
            txtStudentEmail.Focus()
            Return False

        End If

        '--invalid email
        If oCommon.isEmail(txtStudentEmail.Text) = False Then
            lblMsg.Text = "Invalid field format. Email address only!"
            txtStudentEmail.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function StudentProfile_Insert() As String
        Dim strStudentAge As String = Now.Year - CInt(selStudentDOB_year.Value)

        strSQL = "INSERT INTO StudentProfile (StudentID,MYKAD,Pwd,StudentFullname,DOB_Day,DOB_Month,DOB_Year,StudentGender,StudentRace,StudentReligion,StudentEmail,StudentForm,StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState,StudentCountry,TalkBM,TalkBI,TalkMan,TalkTamil,TalkArab,WriteBM,WriteBI,WriteMan,WriteTamil,WriteArab,IsUpdated)" & _
        " VALUES ('" & lblStudentID.Text & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "','" & oCommon.FixSingleQuotes(txtPwd.Text) & "','" & oCommon.FixSingleQuotes(txtStudentFullname.Text.ToUpper) & "','" & selStudentDOB_day.Value & "','" & selStudentDOB_month.Value & "','" & selStudentDOB_year.Value & "','" & selStudentGender.Value & "','" & selStudentRace.Value & "','" & selStudentReligion.Value & "','" & oCommon.FixSingleQuotes(txtStudentEmail.Text) & "','" & selStudentForm.Value & "','" & oCommon.FixSingleQuotes(txtStudentAddress1.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentAddress2.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtStudentPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtStudentCity.Text.ToUpper) & "','" & selStudentState.Value & "','MALAYSIA','" & chkTalkBM.Checked & "','" & chkTalkBI.Checked & "','" & chkTalkMan.Checked & "','" & chkTalkTamil.Checked & "','" & chkTalkArab.Checked & "','" & chkWriteBM.Checked & "','" & chkWriteBI.Checked & "','" & chkWriteMan.Checked & "','" & chkWriteTamil.Checked & "','" & chkWriteArab.Checked & "','Y')"

        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("StudentProfile_Insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

        Return strRet
    End Function

End Class