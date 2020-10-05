Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Data.Common


Public Class studentprofile_import
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strBil As String = ""
    Dim strMykad As String = ""
    Dim strStudentFullName As String = ""
    Dim strDOB_Day As String = ""
    Dim strDOB_Month As String = ""
    Dim strDOB_Year As String = ""
    Dim strStudentGender As String = ""
    Dim strStudentRace As String = ""
    Dim strStudentReligion As String = ""
    Dim strStudentEmail As String = ""
    Dim strStudentForm As String = ""
    Dim strStudentAddress1 As String = ""
    Dim strStudentAddress2 As String = ""
    Dim strStudentPostcode As String = ""
    Dim strStudentCity As String = ""
    Dim strStudentState As String = ""
    Dim strStudentContactNo As String = ""
    Dim strFamilyContactNo As String = ""
    Dim strFatherMyKadNo As String = ""
    Dim strFatherFullName As String = ""
    Dim strFatherJob As String = ""
    Dim strMotherMyKadNo As String = ""
    Dim strMotherFullName As String = ""
    Dim strMotherJob As String = ""
    Dim strSchoolCode As String = ""
    Dim strSchoolName As String = ""
    Dim strStudentCountry As String = "MALAYSIA"
    Dim strUpdate As String = "Y"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            SchoolState_list()
            schoolprofile_PPD_list()
            schoolprofile_city_list()
        End If
    End Sub

    Private Sub SchoolState_list()
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) ORDER BY SchoolStateID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolState.DataSource = ds
            ddlSchoolState.DataTextField = "schoolstate"
            ddlSchoolState.DataValueField = "schoolstate"
            ddlSchoolState.DataBind()

            ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If

        Catch ex As Exception
            ''lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":SchoolState_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub schoolprofile_PPD_list()
        strSQL = "SELECT DISTINCT SchoolPPD FROM SchoolProfile WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolPPD<>'' AND IsDeleted<>'Y' ORDER BY SchoolPPD"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolPPD.DataSource = ds
            ddlSchoolPPD.DataTextField = "SchoolPPD"
            ddlSchoolPPD.DataValueField = "SchoolPPD"
            ddlSchoolPPD.DataBind()

            ddlSchoolPPD.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolPPD.SelectedValue = "ALL"

        Catch ex As Exception
            'lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub schoolprofile_city_list()
        strSQL = "SELECT DISTINCT SchoolCity FROM schoolprofile WHERE SchoolState='" & ddlSchoolState.Text & "' AND IsDeleted='N' ORDER BY SchoolCity"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolCity.DataSource = ds
            ddlSchoolCity.DataTextField = "schoolcity"
            ddlSchoolCity.DataValueField = "schoolcity"
            ddlSchoolCity.DataBind()

            ddlSchoolCity.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolCity.SelectedValue = "ALL"

        Catch ex As Exception
            '`lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
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

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/StudentData/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [StudentProfile$]", connection)
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
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")
                refreshVar()
                strMsg = ""

                'bil
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("BIL")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("BIL")
                End If

                'Student Full name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentFullName")) Then
                    strStudentFullName = SiteData.Tables(0).Rows(i).Item("StudentFullName")
                Else
                    strMsg += " Sila isi Nama|"
                End If

                'tarikh lahir
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("DOB_Day")) Then
                    strDOB_Day = SiteData.Tables(0).Rows(i).Item("DOB_Day")
                Else
                    strMsg += " Sila isi tarikh Lahir|"
                End If

                'bulan lahir
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("DOB_Month")) Then
                    strDOB_Month = SiteData.Tables(0).Rows(i).Item("DOB_Month")
                Else
                    strMsg += " Sila isi bulan Lahir|"
                End If

                'tahun lahir
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("DOB_Year")) Then
                    strDOB_Year = SiteData.Tables(0).Rows(i).Item("DOB_Year")
                Else
                    strMsg += " Sila isi tahun Lahir|"
                End If

                'jantina
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentGender")) Then
                    strStudentGender = SiteData.Tables(0).Rows(i).Item("StudentGender")
                Else
                    strMsg += " Sila isi jantina|"
                End If

                'bangsa
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentRace")) Then
                    strStudentRace = SiteData.Tables(0).Rows(i).Item("StudentRace")
                Else
                    strMsg += " Sila isi bangsa|"
                End If

                'Jantina
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentReligion")) Then
                    strStudentReligion = SiteData.Tables(0).Rows(i).Item("StudentReligion")
                Else
                    strMsg += " Sila isi Agama|"
                End If


                'darjah/tingkatan
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentForm")) Then
                    strStudentForm = SiteData.Tables(0).Rows(i).Item("StudentForm")
                Else
                    strMsg += " Sila isi Darjah/Tingkatan |"
                End If

                'Student Address
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentAddress1")) Then
                    strStudentAddress1 = SiteData.Tables(0).Rows(i).Item("StudentAddress1")
                Else
                    strMsg += " Sila isi Alamat|"
                End If

                'Kad pengenalan required
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MYKAD")) Then
                    strMykad = SiteData.Tables(0).Rows(i).Item("MYKAD")
                Else
                    strMsg += " Sila isi Mykad|"
                End If

                'strSQL = "SELECT MYKAD FROM StudentProfile Where Mykad='" & strMykad & "'"
                'If oCommon.isExist(strSQL) = True Then
                '    strMsg += "Mykad:" & strMykad & ":" & strStudentFullName & ". Mykad ini telah wujud."
                'End If 

                If strMsg.Length = 0 Then
                    'strMsg = "Record#:" & i.ToString & "OK"
                    'strMsg += "<br/>"
                Else
                    strMsg = "BIL#:" & strBil & ": Mykad:" & strMykad & ":" & strStudentFullName & strMsg
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)
                'disp bil

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        Dim display As String = ""

        'Dim str As String
        Try

            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")

                Dim StrStudentID As String = oCommon.getGUID
                Dim StrParentID As String = oCommon.getGUID

                strMykad = SiteData.Tables(0).Rows(i).Item("MYKAD")
                strStudentFullName = SiteData.Tables(0).Rows(i).Item("StudentFullName")
                strDOB_Day = SiteData.Tables(0).Rows(i).Item("DOB_Day")
                strDOB_Month = SiteData.Tables(0).Rows(i).Item("DOB_Month")
                strDOB_Year = SiteData.Tables(0).Rows(i).Item("DOB_Year")
                strStudentGender = SiteData.Tables(0).Rows(i).Item("StudentGender")
                strStudentRace = SiteData.Tables(0).Rows(i).Item("StudentRace")
                strStudentReligion = SiteData.Tables(0).Rows(i).Item("StudentReligion")
                strStudentForm = SiteData.Tables(0).Rows(i).Item("StudentForm")
                strStudentAddress1 = SiteData.Tables(0).Rows(i).Item("StudentAddress1")

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("BIL")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("BIL")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentEmail")) Then
                    strStudentEmail = SiteData.Tables(0).Rows(i).Item("StudentEmail")
                Else
                    strStudentEmail = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentAddress2")) Then
                    strStudentAddress2 = SiteData.Tables(0).Rows(i).Item("StudentAddress2")
                Else
                    strStudentAddress2 = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentPostcode")) Then
                    strStudentPostcode = SiteData.Tables(0).Rows(i).Item("StudentPostcode")
                Else
                    strStudentPostcode = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentCity")) Then
                    strStudentCity = SiteData.Tables(0).Rows(i).Item("StudentCity")
                Else
                    strStudentCity = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentState")) Then
                    strStudentState = SiteData.Tables(0).Rows(i).Item("StudentState")
                Else
                    strStudentState = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("StudentContactNo")) Then
                    strStudentContactNo = SiteData.Tables(0).Rows(i).Item("StudentContactNo")
                Else
                    strStudentContactNo = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("FamilyContactNo")) Then
                    strFamilyContactNo = SiteData.Tables(0).Rows(i).Item("FamilyContactNo")
                Else
                    strFamilyContactNo = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("FatherMyKadNo")) Then
                    strFatherMyKadNo = SiteData.Tables(0).Rows(i).Item("FatherMyKadNo")
                Else
                    strFatherMyKadNo = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("FatherFullName")) Then
                    strFatherFullName = SiteData.Tables(0).Rows(i).Item("FatherFullName")
                Else
                    strFatherFullName = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("FatherJob")) Then
                    strFatherJob = SiteData.Tables(0).Rows(i).Item("FatherJob")
                Else
                    strFatherJob = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MotherMyKadNo")) Then
                    strMotherMyKadNo = SiteData.Tables(0).Rows(i).Item("MotherMyKadNo")
                Else
                    strMotherMyKadNo = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MotherFullName")) Then
                    strMotherFullName = SiteData.Tables(0).Rows(i).Item("MotherFullName")
                Else
                    strMotherFullName = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MotherJob")) Then
                    strMotherJob = SiteData.Tables(0).Rows(i).Item("MotherJob")
                Else
                    strMotherJob = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("SchoolCode")) Then
                    strSchoolCode = SiteData.Tables(0).Rows(i).Item("SchoolCode")
                Else
                    strSchoolCode = ""
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("SchoolName")) Then
                    strSchoolName = SiteData.Tables(0).Rows(i).Item("SchoolName")
                Else
                    strSchoolName = ""
                End If

                strSQL = "SELECT MYKAD FROM StudentProfile Where Mykad='" & strMykad & "'"
                If oCommon.isExist(strSQL) = True Then
                    'update Data student

                    strSQL = "SELECT StudentID from StudentProfile where Mykad='" & strMykad & "'"
                    Dim stdid As String = oCommon.getFieldValue(strSQL)
                    ''update student profile if MYKAD exist
                    strSQL = "UPDATE StudentProfile SET MYKAD ='" & strMykad & "',StudentFullname ='" & oCommon.FixSingleQuotes(strStudentFullName) & "',DOB_Day='" & strDOB_Day & "',
                              DOB_Month ='" & strDOB_Month & "',DOB_Year ='" & strDOB_Year & "',StudentGender ='" & strStudentGender & "' ,StudentRace ='" & strStudentRace & "',StudentReligion ='" & strStudentReligion & "',
                              StudentEmail ='" & strStudentEmail & "', StudentForm ='" & strStudentForm & "',StudentAddress1 ='" & oCommon.FixSingleQuotes(strStudentAddress1) & "',
                              StudentAddress2 ='" & oCommon.FixSingleQuotes(strStudentAddress2) & "',StudentPostcode ='" & strStudentPostcode & "',StudentCity ='" & strStudentCity & "',
                              StudentState ='" & strStudentState & "',StudentContactNo ='" & strStudentContactNo & "' ,StudentCountry ='" & strStudentCountry & "',IsUpdated ='" & strUpdate & "'"
                    strSQL += " WHERE StudentID = '" & stdid & "' "
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If strRet = "0" Then
                        ''update parent info
                        strSQL = "UPDATE ParentProfile SET FamilyContactNo='" & strFamilyContactNo & "',FatherMYKADNo='" & strFatherMyKadNo & "',FatherFullname='" & oCommon.FixSingleQuotes(strFatherFullName) & "',
                                  FatherJob='" & strFatherJob & "',MotherMYKADNo='" & strMotherMyKadNo & "',MotherFullname='" & oCommon.FixSingleQuotes(strMotherFullName) & "',MotherJob='" & strMotherJob & "'"
                        strSQL += " WHERE StudentID = '" & stdid & "' "
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If strRet = "0" Then
                            ''update school info

                            ''get current school id
                            strSQL = "select SchoolID from StudentSchool "
                            strSQL += " WHERE StudentID = '" & stdid & "' AND IsLatest='Y' "
                            Dim strSchoolIDSS As String = oCommon.getFieldValue(strSQL)

                            ''check and get new schoolID
                            strSQL = "select SchoolID from SchoolProfile Where SchoolCode ='" & strSchoolCode & "'"
                            Dim strSchoolIDSP As String = oCommon.getFieldValue(strSQL)

                            If (strSchoolIDSS = strSchoolIDSP) Then
                                ''if sama x pe
                            Else
                                ''update latest school to N
                                strSQL = " UPDATE StudentSchool
                                           SET IsLatest ='N'"
                                strSQL += " WHERE StudentID = '" & stdid & "' AND IsLatest ='Y'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                                ''instert new school into 
                                strSQL = " INSERT INTO StudentSchool (StudentID,SchoolID,IsLatest)"
                                strSQL += " VALUES ('" & stdid & "','" & strSchoolIDSP & "','" & strUpdate & "')"
                                strRet = oCommon.ExecuteSQL(strSQL)

                                '''check data in ukm1 exist or not
                                'strSQL = "select SchoolID from ukm1 WHERE StudentID = '" & stdid & "' AND ExamYear ='2018'"
                                'strRet = oCommon.ExecuteSQL(strSQL)
                                'If strRet = "0" Then

                                '    ''check and get new schoolID
                                '    strSQL = "select SchoolID from ukm1 WHERE StudentID = '" & stdid & "' AND ExamYear ='2018'"
                                '    Dim strUKM1 As String = oCommon.getFieldValue(strSQL)

                                '    If (strUKM1 = strSchoolIDSP) Then
                                '        ''if sama x pe
                                '    Else
                                '        ''update ukm1 = new school id+state+city+type+PPD+lokasi

                                '        ''get schoolstate
                                '        strSQL = "select SchoolState from SchoolProfile Where SchoolCode ='" & strSchoolCode & "'"
                                '        Dim state As String = oCommon.getFieldValue(strSQL)

                                '        ''get schoolcity
                                '        strSQL = "select SchoolCity from SchoolProfile Where SchoolCode ='" & strSchoolCode & "'"
                                '        Dim city As String = oCommon.getFieldValue(strSQL)

                                '        ''get schooltype
                                '        strSQL = "select SchoolType from SchoolProfile Where SchoolCode ='" & strSchoolCode & "'"
                                '        Dim type As String = oCommon.getFieldValue(strSQL)

                                '        ''get schoolPPD
                                '        strSQL = "select SchoolPPD from SchoolProfile Where SchoolCode ='" & strSchoolCode & "'"
                                '        Dim PPD As String = oCommon.getFieldValue(strSQL)

                                '        ''get schoolLokasi
                                '        strSQL = "select SchoolLokasi from SchoolProfile Where SchoolCode ='" & strSchoolCode & "'"
                                '        Dim Lokasi As String = oCommon.getFieldValue(strSQL)

                                '        strSQL = " UPDATE ukm1
                                '           SET SchoolID='" & strSchoolIDSP & "',
                                '               SchoolState='" & state & "',
                                '               SchoolCity='" & city & "'
                                '               SchoolType='" & type & "',
                                '               SchoolPPD='" & PPD & "'
                                '               SchoolLokasi='" & Lokasi & "' "
                                '        strSQL += " WHERE WHERE StudentID = '" & stdid & "' AND ExamYear ='2018'"
                                '        strRet = oCommon.ExecuteSQL(strSQL)

                                '    End If

                                'End If

                            End If

                            display += "BIL#:" & strBil & ": Mykad:" & strMykad & ":" & strStudentFullName & ". Data pelajar telah dikemaskini. "
                            display += " <br/> "
                            lblMsg.Text = display
                        End If
                    End If

                Else
                    'insert new Data student
                    strSQL = "INSERT INTO StudentProfile (StudentID,MYKAD,StudentFullname,DOB_Day,DOB_Month,DOB_Year,StudentGender,StudentRace,StudentReligion,StudentEmail,
                          StudentForm,StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState,StudentContactNo,StudentCountry,IsUpdated)"
                    strSQL += " VALUES ('" & StrStudentID & "','" & strMykad & "','" & oCommon.FixSingleQuotes(strStudentFullName) & "','" & strDOB_Day & "','" & strDOB_Month & "','" & strDOB_Year & "',
                            '" & strStudentGender & "','" & strStudentRace & "','" & strStudentReligion & "','" & strStudentEmail & "','" & strStudentForm & "','" & oCommon.FixSingleQuotes(strStudentAddress1) & "','" & oCommon.FixSingleQuotes(strStudentAddress2) & "','" & strStudentPostcode & "',
                            '" & strStudentCity & "','" & strStudentState & "','" & strStudentContactNo & "','" & strStudentCountry & "','" & strUpdate & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If strRet = "0" Then

                        strSQL = "INSERT INTO ParentProfile (StudentID,ParentID,FamilyContactNo,FatherMYKADNo,FatherFullname,FatherJob,MotherMYKADNo,MotherFullname,MotherJob,IsUpdated)"
                        strSQL += " VALUES ('" & StrStudentID & "','" & StrParentID & "','" & strFamilyContactNo & "','" & strFatherMyKadNo & "','" & oCommon.FixSingleQuotes(strFatherFullName) & "','" & strFatherJob & "','" & strMotherMyKadNo & "',
                            '" & oCommon.FixSingleQuotes(strMotherFullName) & "','" & strMotherJob & "','" & strUpdate & "') "
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If strRet = "0" Then

                            strSQL = "select SchoolID from SchoolProfile Where SchoolCode ='" & strSchoolCode & "'"
                            Dim strSchoolID As String = oCommon.getFieldValue(strSQL)

                            strSQL = " INSERT INTO StudentSchool (StudentID,SchoolID,IsLatest)"
                            strSQL += " VALUES ('" & StrStudentID & "','" & strSchoolID & "','" & strUpdate & "')"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            'divMsg.Attributes("class") = "info"
                            'lblMsg.Text = "Calon berjaya didaftarkan"
                            display += "BIL#:" & strBil & ": Mykad:" & strMykad & ":" & strStudentFullName & ". Calon berjaya didaftarkan. "
                            display += " <br/> "
                            lblMsg.Text = display
                        End If

                    End If
                End If

            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Calon tidak berjaya didaftarkan"
            Return False
        End Try
        'lblMsg.Text = display
        Return True

    End Function

    Private Sub refreshVar()
        strBil = ""
        strMykad = ""
        strStudentFullName = ""
        strDOB_Day = ""
        strDOB_Month = ""
        strDOB_Year = ""
        strStudentGender = ""
        strStudentRace = ""
        strStudentReligion = ""
        strStudentEmail = ""
        strStudentForm = ""
        strStudentAddress1 = ""
        strStudentAddress2 = ""
        strStudentPostcode = ""
        strStudentCity = ""
        strStudentState = ""
        strStudentContactNo = ""
        strFamilyContactNo = ""
        strFatherMyKadNo = ""
        strFatherFullName = ""
        strFatherJob = ""
        strMotherMyKadNo = ""
        strMotherFullName = ""
        strMotherJob = ""
        strSchoolCode = ""
        strSchoolName = ""

    End Sub

End Class