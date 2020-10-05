Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class studentprofile_update_mykad
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strSchoolID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPage()
        End If

    End Sub

    Private Sub LoadPage()
        strSQL = "Select * FROM StudentProfile Where StudentID='" & Request.QueryString("studentid") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    txtMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                    lblMYKAD_ori.Text = txtMYKAD.Text
                Else
                    txtMYKAD.Text = ""
                    lblMYKAD_ori.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentFullname")) Then
                    txtStudentFullname.Text = ds.Tables(0).Rows(0).Item("StudentFullname")
                Else
                    txtStudentFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentGender")) Then
                    selStudentGender.Value = ds.Tables(0).Rows(0).Item("StudentGender")
                Else
                    selStudentGender.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DOB_Day")) Then
                    selStudentDOB_day.Value = ds.Tables(0).Rows(0).Item("DOB_Day")
                Else
                    selStudentDOB_day.Value = "Hari"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DOB_Month")) Then
                    selStudentDOB_month.Value = ds.Tables(0).Rows(0).Item("DOB_Month")
                Else
                    selStudentDOB_month.Value = "Bulan"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DOB_Year")) Then
                    selStudentDOB_year.Value = ds.Tables(0).Rows(0).Item("DOB_Year")
                Else
                    selStudentDOB_year.Value = "Tahun"
                End If


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentForm")) Then
                    selStudentForm.Value = ds.Tables(0).Rows(0).Item("StudentForm")
                Else
                    selStudentForm.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentRace")) Then
                    selStudentRace.Value = ds.Tables(0).Rows(0).Item("StudentRace")
                Else
                    selStudentRace.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentReligion")) Then
                    selStudentReligion.Value = ds.Tables(0).Rows(0).Item("StudentReligion")
                Else
                    selStudentReligion.Value = ""
                End If

                ''--talk language
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkBM")) Then
                    chkTalkBM.Checked = MyTable.Rows(nRows).Item("TalkBM").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkBI")) Then
                    chkTalkBI.Checked = MyTable.Rows(nRows).Item("TalkBI").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkMan")) Then
                    chkTalkMan.Checked = MyTable.Rows(nRows).Item("TalkMan").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkTamil")) Then
                    chkTalkTamil.Checked = MyTable.Rows(nRows).Item("TalkTamil").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkArab")) Then
                    chkTalkArab.Checked = MyTable.Rows(nRows).Item("TalkArab").ToString
                End If

                ''--write language
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteBM")) Then
                    chkWriteBM.Checked = MyTable.Rows(nRows).Item("WriteBM").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteBI")) Then
                    chkWriteBI.Checked = MyTable.Rows(nRows).Item("WriteBI").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteMan")) Then
                    chkWriteMan.Checked = MyTable.Rows(nRows).Item("WriteMan").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteTamil")) Then
                    chkWriteTamil.Checked = MyTable.Rows(nRows).Item("WriteTamil").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteArab")) Then
                    chkWriteArab.Checked = MyTable.Rows(nRows).Item("WriteArab").ToString
                End If

                ''--continue
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentAddress1")) Then
                    txtStudentAddress1.Text = ds.Tables(0).Rows(0).Item("StudentAddress1")
                Else
                    txtStudentAddress1.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentAddress2")) Then
                    txtStudentAddress2.Text = ds.Tables(0).Rows(0).Item("StudentAddress2")
                Else
                    txtStudentAddress2.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentPostcode")) Then
                    txtStudentPostcode.Text = ds.Tables(0).Rows(0).Item("StudentPostcode")
                Else
                    txtStudentPostcode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentCity")) Then
                    txtStudentCity.Text = ds.Tables(0).Rows(0).Item("StudentCity")
                Else
                    txtStudentCity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentState")) Then
                    selStudentState.Value = ds.Tables(0).Rows(0).Item("StudentState")
                Else
                    selStudentState.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentEmail")) Then
                    txtStudentEmail.Text = ds.Tables(0).Rows(0).Item("StudentEmail")
                Else
                    txtStudentEmail.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    txtPwd.Text = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    txtPwd.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AlumniID")) Then
                    txtAlumniID.Text = ds.Tables(0).Rows(0).Item("AlumniID")
                    lblAlumniID_ori.Text = txtAlumniID.Text
                Else
                    txtAlumniID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NoPelajar")) Then
                    txtNoPelajar.Text = ds.Tables(0).Rows(0).Item("NoPelajar")
                    lblNoPelajar_ori.Text = txtNoPelajar.Text
                Else
                    txtNoPelajar.Text = ""
                    lblNoPelajar_ori.Text = ""
                End If

                ''--load initial photo here
                imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")

            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function ValidatePage() As Boolean

        ''--mykadno changed. MYKAD is unique.
        If Not txtMYKAD.Text = lblMYKAD_ori.Text Then
            strSQL = "SELECT StudentFullname FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
            strRet = oCommon.getFieldValue(strSQL)
            If strRet.Length > 0 Then
                lblMsg.Text = "MYKAD# sudah digunakan oleh " & strRet & ". Jika ini adalah MYKAD anda, sila masuk semula menggunakan MYKAD ini."
                txtMYKAD.Focus()
                Return False
            End If
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

        If txtStudentFullname.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Nama Penuh!"
            txtStudentFullname.Focus()
            Return False

        End If

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

        'If selStudentDOB_year.Value = "" Then
        '    lblMsg.Text = "Please select this field. Tarikh Lahir. Tahun!"
        '    selStudentDOB_year.Focus()
        '    Return False

        'End If

        'If selStudentForm.Value = "" Then
        '    lblMsg.Text = "Please select this field. Darjah/Tingkatan!"
        '    selStudentForm.Focus()
        '    Return False

        'End If

        'If selStudentRace.Value = "" Then
        '    lblMsg.Text = "Please select this field. Bangsa!"
        '    selStudentRace.Focus()
        '    Return False

        'End If

        'If txtStudentAddress1.Text.Length = 0 Then
        '    lblMsg.Text = "Please fill-in this field. Alamat Rumah!"
        '    txtStudentAddress1.Focus()
        '    Return False
        'End If

        ' ''If txtStudentAddress2.Text.Length = 0 Then
        ' ''    lblMsg.Text = "Please fill-in this field. Alamat Rumah!"
        ' ''    txtStudentAddress2.Focus()
        ' ''    Return False
        ' ''End If

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

        'If Not txtAlumniID.Text = lblAlumniID_ori.Text Then
        '    strSQL = "SELECT AlumniID FROM StudentProfile WHERE AlumniID='" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "'"
        '    If oCommon.isExist(strSQL) = True Then
        '        lblMsg.Text = "PPCS Alumni ID telah digunakan!"
        '        txtAlumniID.Focus()
        '        Return False
        '    End If
        'End If

        'If Not txtNoPelajar.Text.Length = 0 Then
        '    If Not txtNoPelajar.Text = lblNoPelajar_ori.Text Then
        '        strSQL = "SELECT MYKAD,StudentFullname FROM StudentProfile WHERE NoPelajar='" & oCommon.FixSingleQuotes(txtNoPelajar.Text) & "'"
        '        strRet = oCommon.getFieldValueEx(strSQL)
        '        If strRet.Length > 0 Then
        '            lblMsg.Text = "No Pelajar sudah digunakan oleh: " & strRet
        '            txtMYKAD.Focus()
        '            Return False
        '        End If
        '    End If
        'End If

        Return True
    End Function

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ''strip all spaces and special chars

        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        ''trim before update
        Dim strMYKAD As String = oCommon.StringStrip(txtMYKAD.Text)
        txtMYKAD.Text = strMYKAD

        strRet = StudentProfile_update()
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini maklumat pelajar!<a href='default.main.aspx?lang=ms-MY&studentid=0a9ee8bb-cb63-479f-87ef-49adc808cfdb'"
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

    Private Function StudentProfile_update() As String
        Try
            strSQL = "UPDATE StudentProfile SET IsUpdated='Y',MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "',StudentFullname='" & oCommon.FixSingleQuotes(txtStudentFullname.Text.ToUpper) & "',DOB_day='" & selStudentDOB_day.Value & "',DOB_month='" & selStudentDOB_month.Value & "',DOB_year='" & selStudentDOB_year.Value & "',StudentGender='" & selStudentGender.Value & "',StudentRace='" & selStudentRace.Value & "',StudentReligion='" & selStudentReligion.Value & "',StudentEmail='" & oCommon.FixSingleQuotes(txtStudentEmail.Text) & "',StudentForm='" & selStudentForm.Value & "',StudentAddress1='" & oCommon.FixSingleQuotes(txtStudentAddress1.Text.ToUpper) & "',StudentAddress2='" & oCommon.FixSingleQuotes(txtStudentAddress2.Text.ToUpper) & "',StudentPostcode='" & txtStudentPostcode.Text & "',StudentCity='" & oCommon.FixSingleQuotes(txtStudentCity.Text.ToUpper) & "',StudentState='" & selStudentState.Value & "',TalkBM='" & chkTalkBM.Checked & "',TalkBI='" & chkTalkBI.Checked & "',TalkMan='" & chkTalkMan.Checked & "',TalkTamil='" & chkTalkTamil.Checked & "',TalkArab='" & chkTalkArab.Checked & "',WriteBM='" & chkWriteBM.Checked & "',WriteBI='" & chkWriteBI.Checked & "',WriteMan='" & chkWriteMan.Checked & "',WriteTamil='" & chkWriteTamil.Checked & "',WriteArab='" & chkWriteArab.Checked & "',Pwd='" & oCommon.FixSingleQuotes(txtPwd.Text) & "',AlumniID='" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
            ''debug
            ''Response.Write(strSQL)
            strRet = oCommon.ExecuteSQL(strSQL)

            ''log
            oCommon.TransactionLog("StudentProfile_update", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

            Return strRet
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        ''nothing to upload
        If imgUpload.FileName.Length = 0 Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Please select file to upload!"
            Exit Sub
        End If

        ''Studentphoto set unique studentid, photoid
        strSQL = "SELECT StudentID FROM StudentPhoto WHERE StudentID='" & Request.QueryString("studentid") & "'"
        If oCommon.isExist(strSQL) = True Then
            Studentphoto_Update()
        Else
            Studentphoto_Insert()
        End If

    End Sub

    Private Sub Studentphoto_Insert()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        ''--get unique id for photo
        Dim strPhotoID As String = oCommon.getGUID

        Try
            Dim img As FileUpload = CType(imgUpload, FileUpload)
            Dim imgByte As Byte() = Nothing
            If img.HasFile AndAlso Not img.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim File As HttpPostedFile = imgUpload.PostedFile
                'Create byte Array with file len
                imgByte = New Byte(File.ContentLength - 1) {}
                'force the control to load data in array
                File.InputStream.Read(imgByte, 0, File.ContentLength)
            End If

            ' Insert the employee name and image into db
            objConn = New SqlConnection(strConn)

            objConn.Open()
            strSQL = "INSERT INTO StudentPhoto(PhotoID,StudentID,SmallPhoto,DateCreated) VALUES(@PhotoID, @StudentID, @SmallPhoto,@DateCreated) SELECT @@IDENTITY"

            Dim cmd As SqlCommand = New SqlCommand(strSQL, objConn)
            cmd.Parameters.AddWithValue("@PhotoID", strPhotoID)
            cmd.Parameters.AddWithValue("@StudentID", Request.QueryString("studentid"))
            cmd.Parameters.AddWithValue("@SmallPhoto", imgByte)
            cmd.Parameters.AddWithValue("@DateCreated", oCommon.getNow)

            Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            lblMsg.Text = "Photo upload success." & String.Format("Photo ID is {0}", id)
            imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")
        Catch ex As Exception
            lblMsg.Text = "System Error. Contact Admin." & ex.Message
        Finally
            objConn.Close()
        End Try

    End Sub

    Private Sub Studentphoto_Update()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        ''--get unique id for photo
        Dim strPhotoID As String = oCommon.getGUID

        Try
            Dim img As FileUpload = CType(imgUpload, FileUpload)
            Dim imgByte As Byte() = Nothing
            If img.HasFile AndAlso Not img.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim File As HttpPostedFile = imgUpload.PostedFile
                'Create byte Array with file len
                imgByte = New Byte(File.ContentLength - 1) {}
                'force the control to load data in array
                File.InputStream.Read(imgByte, 0, File.ContentLength)
            End If

            ' Insert the employee name and image into db
            objConn = New SqlConnection(strConn)

            objConn.Open()
            strSQL = "UPDATE StudentPhoto SET SmallPhoto=@SmallPhoto WHERE StudentID='" & Request.QueryString("studentid") & "'"

            Dim cmd As SqlCommand = New SqlCommand(strSQL, objConn)
            cmd.Parameters.AddWithValue("@SmallPhoto", imgByte)

            Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            lblMsg.Text = "Photo upload success. " & String.Format("Photo ID is {0}", id)
            imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")
        Catch ex As Exception
            lblMsg.Text = "System Error. Contact Admin. " & ex.Message
        Finally
            objConn.Close()
        End Try
    End Sub

    Protected Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub
End Class