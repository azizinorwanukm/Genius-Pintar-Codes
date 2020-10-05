Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class student_profile_photo_upload
    Inherits System.Web.UI.UserControl

    '--This is for year 2010 intake. Change this for next year deployment
    Dim strTestID As String = "2010"

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strLang As String
    Dim strTokenID As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 1


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strTokenID = Request.QueryString("tokenid")

            If Not IsPostBack Then
                ''--set tokenid
                tokenid.Text = strTokenID

                LoadProfile_Student(strTokenID)
                LoadProfile_School(strTokenID)
                LoadProfile_Parent(strTokenID)
                ''--LoadProfile_Exam(strTokenID)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function LoadProfile_Student(ByVal strKeyID As String) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT respondentid,Tokenid,TestID,SelectedLang,QuestionSet,RespFullname,DateCreated,RespEmail,RespDOB,RespAge,RespGender,RespRace,RespReligion,RespForm,RespArabLanguage,RespBM,RespBI,RespMan,RespTamil,RespArab,RespBMW,RespBIW,RespManW,RespTamilW,RespArabW,RespAddress,RespPostcode,RespState,RespCity,RespCountry,RespContact FROM ukm1_respondent_mark2011 WHERE Tokenid='" & strKeyID & "' AND TestID='2010'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("respondentid")) Then
                    txtRespondentid.Text = MyTable.Rows(nRows).Item("respondentid").ToString
                Else
                    txtRespondentid.Text = ""
                End If
                ''--set respondentid session
                Session("respondentid") = txtRespondentid.Text


                If Not IsDBNull(MyTable.Rows(nRows).Item("RespFullname")) Then
                    RespFullname.Text = MyTable.Rows(nRows).Item("RespFullname").ToString
                Else
                    RespFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("RespEmail")) Then
                    RespEmail.Text = MyTable.Rows(nRows).Item("RespEmail").ToString
                Else
                    RespEmail.Text = ""
                End If

                Dim strRespDOB As String
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespDOB")) Then
                    strRespDOB = MyTable.Rows(nRows).Item("RespDOB").ToString
                Else
                    strRespDOB = "00-00-0000"
                End If
                Dim arRespDOB
                arRespDOB = Split(strRespDOB, "-")
                If UBound(arRespDOB) > 1 Then
                    RespDOBday.Value = arRespDOB(0)
                    RespDOBmonth.Value = arRespDOB(1)
                    RespDOBYear.Value = arRespDOB(2)
                End If

                Dim strGender As String
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespGender")) Then
                    strGender = MyTable.Rows(nRows).Item("RespGender").ToString
                Else
                    strGender = ""
                End If
                If strGender = "LELAKI" Then
                    RespGender1.Checked = True
                End If
                If strGender = "PEREMPUAN" Then
                    RespGender2.Checked = True
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("RespRace")) Then
                    RespRace.Value = MyTable.Rows(nRows).Item("RespRace").ToString
                Else
                    RespRace.Value = "00"
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("RespReligion")) Then
                    RespReligion.Value = MyTable.Rows(nRows).Item("RespReligion").ToString
                Else
                    RespReligion.Value = "00"
                End If

                'Dim strRespArabLanguage As String
                'If Not IsDBNull(MyTable.Rows(nRows).Item("RespArabLanguage")) Then
                '    strRespArabLanguage = MyTable.Rows(nRows).Item("RespArabLanguage").ToString
                'Else
                '    strRespArabLanguage = ""
                'End If
                'If strRespArabLanguage = "TERHAD" Then
                '    Arab1.Checked = True
                'End If
                'If strRespArabLanguage = "SEDERHANA" Then
                '    Arab2.Checked = True
                'End If
                'If strRespArabLanguage = "BAIK" Then
                '    Arab3.Checked = True
                'End If
                'If strRespArabLanguage = "AMAT BAIK" Then
                '    Arab4.Checked = True
                'End If

                '--language tutur
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespBM")) Then
                    If MyTable.Rows(nRows).Item("RespBM").ToString = "Y" Then
                        chkLang1.Checked = True
                    End If
                Else
                    chkLang1.Checked = False
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespBI")) Then
                    If MyTable.Rows(nRows).Item("RespBI").ToString = "Y" Then
                        chkLang2.Checked = True
                    End If
                Else
                    chkLang2.Checked = False
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespMan")) Then
                    If MyTable.Rows(nRows).Item("RespMan").ToString = "Y" Then
                        chkLang3.Checked = True
                    End If
                Else
                    chkLang3.Checked = False
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespTamil")) Then
                    If MyTable.Rows(nRows).Item("RespTamil").ToString = "Y" Then
                        chkLang4.Checked = True
                    End If
                Else
                    chkLang4.Checked = False
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespArab")) Then
                    If MyTable.Rows(nRows).Item("RespArab").ToString = "Y" Then
                        chkLang5.Checked = True
                    End If
                Else
                    chkLang5.Checked = False
                End If

                '--language tulis
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespBMW")) Then
                    If MyTable.Rows(nRows).Item("RespBMW").ToString = "Y" Then
                        chkLangWrite1.Checked = True
                    End If
                Else
                    chkLangWrite1.Checked = False
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespBIW")) Then
                    If MyTable.Rows(nRows).Item("RespBIW").ToString = "Y" Then
                        chkLangWrite2.Checked = True
                    End If
                Else
                    chkLangWrite2.Checked = False
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespManW")) Then
                    If MyTable.Rows(nRows).Item("RespManW").ToString = "Y" Then
                        chkLangWrite3.Checked = True
                    End If
                Else
                    chkLangWrite3.Checked = False
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespTamilW")) Then
                    If MyTable.Rows(nRows).Item("RespTamilW").ToString = "Y" Then
                        chkLangWrite4.Checked = True
                    End If
                Else
                    chkLangWrite4.Checked = False
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespArabW")) Then
                    If MyTable.Rows(nRows).Item("RespArabW").ToString = "Y" Then
                        chkLangWrite5.Checked = True
                    End If
                Else
                    chkLangWrite5.Checked = False
                End If

                '--done
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespForm")) Then
                    RespForm.Value = MyTable.Rows(nRows).Item("RespForm").ToString
                Else
                    RespForm.Value = "00"
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("RespAddress")) Then
                    RespAddress.Text = MyTable.Rows(nRows).Item("RespAddress").ToString
                Else
                    RespAddress.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespPostcode")) Then
                    RespPostcode.Text = MyTable.Rows(nRows).Item("RespPostcode").ToString
                Else
                    RespPostcode.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespState")) Then
                    RespState.Value = MyTable.Rows(nRows).Item("RespState").ToString
                Else
                    RespState.Value = "00"
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("RespCity")) Then
                    RespCity.Text = MyTable.Rows(nRows).Item("RespCity").ToString
                Else
                    RespCity.Text = ""
                End If

                '--keep tokenid and question set
                ''If Not IsDBNull(MyTable.Rows(nRows).Item("QuestionSet")) Then
                ''    lblQuestionSet.Text = MyTable.Rows(nRows).Item("QuestionSet").ToString
                ''Else
                ''    lblQuestionSet.Text = "NA"
                ''End If

                ''--load initial photo here
                imgStudent.ImageUrl = "~/ShowImage.ashx?tokenid=" & tokenid.Text.Trim

            Else
                lblMsg.Text = "Profil baru."
            End If

            Return True
        Catch ex As Exception
            lblMsg.Text = ex.Message
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Private Function LoadProfile_School(ByVal strKeyID As String) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT SchoolName,SchoolCode,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolType FROM ukm1_respondent_mark2011 WHERE Tokenid='" & strKeyID & "' AND TestID='2010'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        '-debug
        '--Response.Write(strSQL)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolName")) Then
                    SchoolName.Text = MyTable.Rows(nRows).Item("SchoolName").ToString
                Else
                    SchoolName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCode")) Then
                    SchoolCode.Text = MyTable.Rows(nRows).Item("SchoolCode").ToString
                Else
                    SchoolCode.Text = ""
                End If
                ''--initial schoolcode session value
                Session("schoolcode") = SchoolCode.Text

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolAddress")) Then
                    SchoolAddress.Text = MyTable.Rows(nRows).Item("SchoolAddress").ToString
                Else
                    SchoolAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolPostcode")) Then
                    SchoolPostcode.Text = MyTable.Rows(nRows).Item("SchoolPostcode").ToString
                Else
                    SchoolPostcode.Text = "00"
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCity")) Then
                    SchoolCity.Text = MyTable.Rows(nRows).Item("SchoolCity").ToString
                Else
                    SchoolCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    SchoolState.Value = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    SchoolState.Value = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolType")) Then
                    SchoolType.Value = MyTable.Rows(nRows).Item("SchoolType").ToString
                Else
                    SchoolType.Value = ""
                End If
            Else
                lblMsg.Text = "Gagal Load profile sekolah"
            End If
            Return True

        Catch ex As Exception
            lblMsg.Text = ex.Message
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Private Function LoadSchool_info(ByVal strKeyID As String) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM PP_Schools WHERE SchoolCode='" & strKeyID & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        '-debug
        '--Response.Write(strSQL)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolName")) Then
                    SchoolName.Text = MyTable.Rows(nRows).Item("SchoolName").ToString
                Else
                    SchoolName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCode")) Then
                    SchoolCode.Text = MyTable.Rows(nRows).Item("SchoolCode").ToString
                Else
                    SchoolCode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolAddress")) Then
                    SchoolAddress.Text = MyTable.Rows(nRows).Item("SchoolAddress").ToString
                Else
                    SchoolAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolPostcode")) Then
                    SchoolPostcode.Text = MyTable.Rows(nRows).Item("SchoolPostcode").ToString
                Else
                    SchoolPostcode.Text = "00"
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCity")) Then
                    SchoolCity.Text = MyTable.Rows(nRows).Item("SchoolCity").ToString
                Else
                    SchoolCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    SchoolState.Value = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    SchoolState.Value = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolType")) Then
                    SchoolType.Value = MyTable.Rows(nRows).Item("SchoolType").ToString
                Else
                    SchoolType.Value = "00"
                End If
            Else
                lblMsg.Text = "Gagal Load profile sekolah"
            End If
            Return True

        Catch ex As Exception
            lblMsg.Text = ex.Message
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Private Function LoadProfile_Parent(ByVal strKeyID As String) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT Tokenid,ParentFullname,ParentContactNo,ParentJob,ParentEdu,ParentSalary FROM ukm1_respondent_mark2011 WHERE Tokenid='" & strKeyID & "' AND TestID='2010'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        '-debug
        'Response.Write(strSQL)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("ParentFullname")) Then
                    ParentFullname.Text = MyTable.Rows(nRows).Item("ParentFullname").ToString
                Else
                    ParentFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("ParentContactNo")) Then
                    ParentContactNo.Text = MyTable.Rows(nRows).Item("ParentContactNo").ToString
                Else
                    ParentContactNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("ParentJob")) Then
                    ParentJob.Text = MyTable.Rows(nRows).Item("ParentJob").ToString
                Else
                    ParentJob.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("ParentEdu")) Then
                    ParentEdu.Value = MyTable.Rows(nRows).Item("ParentEdu").ToString
                Else
                    ParentEdu.Value = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("ParentSalary")) Then
                    ParentSalary.Value = MyTable.Rows(nRows).Item("ParentSalary").ToString
                Else
                    ParentSalary.Value = ""
                End If
            Else
                lblMsg.Text = "Gagal Load profile ibubapa/penjaga"
            End If

            Return True
        Catch ex As Exception
            lblMsg.Text = ex.Message
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

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
            strSQL = "SELECT TokenID FROM ukm1_respondent_photo WHERE TokenID='" & tokenid.Text & "'"
            If oCommon.isExist(strSQL) = True Then
                strSQL = "UPDATE ukm1_respondent_photo SET RespPhoto=@RespPhoto WHERE TokenID='" & tokenid.Text & "'"
            Else
                strSQL = "INSERT INTO ukm1_respondent_photo(TokenID,RespPhoto) VALUES(@TokenID, @RespPhoto) SELECT @@IDENTITY"
            End If

            Dim cmd As SqlCommand = New SqlCommand(strSQL, objConn)
            cmd.Parameters.AddWithValue("@TokenID", tokenid.Text.Trim())
            cmd.Parameters.AddWithValue("@RespPhoto", imgByte)

            Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            lblResult.Text = String.Format("Photo ID is {0}", id)

            lblMsg.Text = "Photo upload success."
            imgStudent.ImageUrl = "~/ShowImage.ashx?tokenid=" & tokenid.Text.Trim
        Catch
            lblMsg.Text = "There was an error"
        Finally
            objConn.Close()
        End Try
    End Sub
End Class