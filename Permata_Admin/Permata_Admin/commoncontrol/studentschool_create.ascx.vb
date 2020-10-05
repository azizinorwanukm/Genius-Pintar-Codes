Imports System.Data.SqlClient

Public Class studentschool_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                master_Dobyear_list()
                ddlStartDate_year.Text = Now.Year
                ddlEndDate_year.Text = Now.Year

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub master_Dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear WITH (NOLOCK) ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--ddlStartDate_day
            ddlStartDate_year.DataSource = ds
            ddlStartDate_year.DataTextField = "DOB_Year"
            ddlStartDate_year.DataValueField = "DOB_Year"
            ddlStartDate_year.DataBind()

            '--ddlEndDate_year
            ddlEndDate_year.DataSource = ds
            ddlEndDate_year.DataTextField = "DOB_Year"
            ddlEndDate_year.DataValueField = "DOB_Year"
            ddlEndDate_year.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        If ValidatePage() = False Then
            Exit Sub
        End If

        strRet = StudentSchool_insert()


        If strRet = "0" Then
            lblMsg.Text = "BERJAYA menambah sekolah pelajar."

            If chkIsLatest.Checked = True Then
                ''--update UKM1 schoolprofile
                If ukm1_schoolprofile_update() = True Then
                    lblMsg.Text += "<br/>BERJAYA kemaskini maklumat sekolah UKM1 pelajar."
                End If
            End If
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

    Private Function ukm1_schoolprofile_update() As Boolean
        ''--get schoolID base on studentid
        strSQL = "SELECT TOP 1 SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "' AND IsLatest='Y' ORDER BY StudentSchoolID DESC"
        Dim strSchoolID As String = oCommon.getFieldValue(strSQL)

        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        ''--debug
        'Response.Write("ukm1_schoolprofile_update:" & strRet & ":" & strSQL)

        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text += "<br/>SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        ''update UKM1

        strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (ROWLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & Now.Year & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "UPDATE UKM1 WITH (ROWLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & Now.Year & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "<br/>UPDATE UKM1 error:" & strRet
            Return False
        End If

        Return True

    End Function


    Private Function ValidatePage() As Boolean

        ''Dr Siti request 20120229 to remove this

        'If selStartDate_day.Value = "" Then
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If
        'If selStartDate_month.Value = "" Then
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If
        'If ddlStartDate_year.Text = "" Then
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If

        Return True
    End Function


    Private Function StudentSchool_insert() As String
        Dim strIsLatest As String = "N"

        Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & ddlStartDate_year.Text
        Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & ddlEndDate_year.Text

        If chkIsLatest.Checked = True Then
            strIsLatest = "Y"
            setStudentSchool_N(Request.QueryString("studentid"))

            '--INSERT StudentSchool
            strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,StartDate,EndDate,CreatedDate,IsLatest) VALUES ('" & Request.QueryString("studentid") & "','" & Request.QueryString("schoolid") & "','" & strStartDate & "','" & strEndDate & "','" & oCommon.getNow & "','" & strIsLatest & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

            ''Update UKM1
            ''get Student ID data
            Dim get_StudentID As String = "select StudentSchool.StudentID from StudentSchool
                                           where studentschoolid='" & Request.QueryString("studentschoolid") & "'"
            Dim data_StudentID As String = oCommon.getFieldValue(get_StudentID)

            ''get School ID data 
            Dim get_SchoolID As String = "select SchoolProfile.SchoolID from SchoolProfile
                                          left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                          left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                          where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolID As String = oCommon.getFieldValue(get_SchoolID)

            ''get School State data
            Dim get_SchoolState As String = "select SchoolProfile.SchoolState from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolState As String = oCommon.getFieldValue(get_SchoolState)

            ''get School City data
            Dim get_SchoolCity As String = "select SchoolProfile.SchoolCity from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolCity As String = oCommon.getFieldValue(get_SchoolCity)

            ''get School Type data
            Dim get_SchoolType As String = "select SchoolProfile.SchoolType from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolType As String = oCommon.getFieldValue(get_SchoolType)

            ''get School PPD data
            Dim get_SchoolPPD As String = "select SchoolProfile.SchoolPPD from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolPPD As String = oCommon.getFieldValue(get_SchoolPPD)

            ''get School Lokasi data
            Dim get_SchoolLokasi As String = "select SchoolProfile.SchoolType from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolLokasi As String = oCommon.getFieldValue(get_SchoolLokasi)

            Dim examYear As String = oCommon.getFieldValue("select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "'")

            If examYear = getUKM1Year Then
                strSQL = "UPDATE UKM1_" & getUKM1Year & " SET SchoolID='" & data_SchoolID & "',SchoolState='" & data_SchoolState & "',SchoolCity='" & data_SchoolCity & "', SchoolType='" & data_SchoolType & "',SchoolPPD='" & data_SchoolPPD & "',SchoolLokasi='" & data_SchoolLokasi & "' 
                        WHERE StudentID=" & data_StudentID & "' And ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            strSQL = "UPDATE UKM1 SET SchoolID='" & data_SchoolID & "',SchoolState='" & data_SchoolState & "',SchoolCity='" & data_SchoolCity & "', SchoolType='" & data_SchoolType & "',SchoolPPD='" & data_SchoolPPD & "',SchoolLokasi='" & data_SchoolLokasi & "' 
                        WHERE StudentID=" & data_StudentID & "' And ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')'"
            strRet = oCommon.ExecuteSQL(strSQL)


            ''get Student ID data
            Dim get_StudentID_UKM2 As String = "select StudentSchool.StudentID from StudentSchool
                                           where studentschoolid='" & Request.QueryString("studentschoolid") & "'"
            Dim data_StudentID_UKM2 As String = oCommon.getFieldValue(get_StudentID_UKM2)

            ''get School ID data 
            Dim get_SchoolID_UKM2 As String = "select SchoolProfile.SchoolID from SchoolProfile
                                          left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                          left join UKM2 on UKM1.StudentID = StudentSchool.StudentID
                                          where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
            Dim data_SchoolID_UKM2 As String = oCommon.getFieldValue(get_SchoolID_UKM2)

            ''get School State data
            Dim get_SchoolState_UKM2 As String = "select SchoolProfile.SchoolState from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
            Dim data_SchoolState_UKM2 As String = oCommon.getFieldValue(get_SchoolState_UKM2)

            ''get School City data
            Dim get_SchoolCity_UKM2 As String = "select SchoolProfile.SchoolCity from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
            Dim data_SchoolCity_UKM2 As String = oCommon.getFieldValue(get_SchoolCity_UKM2)

            ''get School Type data
            Dim get_SchoolType_UKM2 As String = "select SchoolProfile.SchoolType from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
            Dim data_SchoolType_UKM2 As String = oCommon.getFieldValue(get_SchoolType_UKM2)

            ''get School PPD data
            Dim get_SchoolPPD_UKM2 As String = "select SchoolProfile.SchoolPPD from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
            Dim data_SchoolPPD_UKM2 As String = oCommon.getFieldValue(get_SchoolPPD_UKM2)

            ''get School Lokasi data
            Dim get_SchoolLokasi_UKM2 As String = "select SchoolProfile.SchoolType from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
            Dim data_SchoolLokasi_UKM2 As String = oCommon.getFieldValue(get_SchoolLokasi_UKM2)

            strSQL = "UPDATE UKM2 SET SchoolID='" & data_SchoolID_UKM2 & "',SchoolState='" & data_SchoolState_UKM2 & "',SchoolCity='" & data_SchoolCity_UKM2 & "', SchoolType='" & data_SchoolType_UKM2 & "',SchoolPPD='" & data_SchoolPPD_UKM2 & "',SchoolLokasi='" & data_SchoolLokasi_UKM2 & "' 
                        WHERE StudentID=" & data_StudentID_UKM2 & "' And ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID_UKM2 & "')'"
            strRet = oCommon.ExecuteSQL(strSQL)

        ElseIf chkIsLatest.Checked = False Then

            '--INSERT StudentSchool
            strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,StartDate,EndDate,CreatedDate,IsLatest) VALUES ('" & Request.QueryString("studentid") & "','" & Request.QueryString("schoolid") & "','" & strStartDate & "','" & strEndDate & "','" & oCommon.getNow & "','" & strIsLatest & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If

        Return strRet

    End Function

    Private Sub setStudentSchool_N(ByVal strStudentID As String)
        strSQL = "UPDATE StudentSchool SET IsLatest='N' WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub


    Protected Sub lnkStudentProfileView_Click(sender As Object, e As EventArgs) Handles lnkStudentProfileView.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect(CType(Session.Item("pageid"), String) & "?studentid=" & Request.QueryString("studentid"))
            Case "ADMINOP"
                Response.Redirect("studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class