''Imports System.Data
''Imports System.Data.OleDb
Imports System.Data.SqlClient
''Imports System.IO
''Imports System.Globalization

Partial Public Class ppcs_kelayakan_select
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
                lblMsgTop.Text = ""
                txtExamStart.Text = oCommon.getToday

                '-list of exam avail
                examyear_list()
                ''ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")
                ddlExamYear.Text = oCommon.getFieldValue("select master_examyear.examyearid from master_examyear join master_Config on master_Examyear.ExamYear = master_Config.configString where master_Config.configCode='DefaultExamYear'")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                '--list all religion
                StudentReligion_list()
                ddlStudentReligion.Text = "ALL"

                '--SchoolState
                SchoolState_list()
                ddlSchoolState.Text = "ALL"

                '--SchoolPPD
                schoolprofile_PPD_list()

                '--schooltype
                schooltype_list()

                '--ddlPPCSDate
                ppcsdatesearch_list()
                ddlPPCSDateSearch.Text = oCommon.getAppsettings("DefaultPPCSDate")

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                '--PPCSStatus
                PPCSStatusSearch_list()
                ddlPPCSStatusSearch.Text = "PEMILIHAN"

                PPCSStatus_list()
                ddlPPCSStatus.SelectedValue = ""
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub PPCSStatusSearch_list()
        strSQL = "SELECT PPCSStatus FROM master_PPCSStatus WHERE IsKelayakan='Y' ORDER BY PPCSStatus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSStatusSearch.DataSource = ds
            ddlPPCSStatusSearch.DataTextField = "PPCSStatus"
            ddlPPCSStatusSearch.DataValueField = "PPCSStatus"
            ddlPPCSStatusSearch.DataBind()

            ddlPPCSStatusSearch.Items.Add(New ListItem("PEMILIHAN", "PEMILIHAN"))
            ddlPPCSStatusSearch.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub PPCSStatus_list()
        strSQL = "SELECT PPCSStatus FROM master_PPCSStatus WHERE IsKelayakan='Y' ORDER BY PPCSStatus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSStatus.DataSource = ds
            ddlPPCSStatus.DataTextField = "PPCSStatus"
            ddlPPCSStatus.DataValueField = "PPCSStatus"
            ddlPPCSStatus.DataBind()

            ddlPPCSStatus.Items.Add(New ListItem("--SILA PILIH--", ""))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub schooltype_list()
        strSQL = "SELECT schooltype FROM schooltype ORDER BY schooltypeid"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolType.DataSource = ds
            ddlSchoolType.DataTextField = "schooltype"
            ddlSchoolType.DataValueField = "schooltype"
            ddlSchoolType.DataBind()

            ddlSchoolType.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolType.SelectedValue = "ALL"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

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
            lblMsg.Text = "Database error!" & ex.Message

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
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub StudentReligion_list()
        strSQL = "SELECT StudentReligion FROM master_StudentReligion ORDER BY StudentReligion"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentReligion.DataSource = ds
            ddlStudentReligion.DataTextField = "StudentReligion"
            ddlStudentReligion.DataValueField = "StudentReligion"
            ddlStudentReligion.DataBind()

            ddlStudentReligion.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT examyearid, ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "ADMINOP"
                strSQL = "SELECT examyearid, ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT examyearid, ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT examyearid, ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT examyearid, ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case "UKM"
                strSQL = "SELECT examyearid, ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & oCommon.getAppsettings("DefaultExamYear") & "%'  ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT examyearid, ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
        End Select

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "examyearid"
            ddlExamYear.DataBind()


            '--ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub master_dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDOB_Year.DataSource = ds
            ddlDOB_Year.DataTextField = "DOB_Year"
            ddlDOB_Year.DataValueField = "DOB_Year"
            ddlDOB_Year.DataBind()

            ddlDOB_Year.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcsdatesearch_list()
        strSQL = "SELECT PPCSDate FROM master_PPCSDate ORDER BY ppcsid ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDateSearch.DataSource = ds
            ddlPPCSDateSearch.DataTextField = "PPCSDate"
            ddlPPCSDateSearch.DataValueField = "PPCSDate"
            ddlPPCSDateSearch.DataBind()

            'ddlPPCSDateSearch.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ppcsdate_list()
        strSQL = "SELECT PPCSDate FROM master_PPCSDate ORDER BY ppcsid ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL(0), strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai"
            Else
                lblMsg.Text = "Jumlah pelajar#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL(export As Integer) As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        '--senarai UKM2
        ''tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentType,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentGender,StudentProfile.StudentAddress1,StudentProfile.StudentAddress2,StudentProfile.StudentPostcode,StudentProfile.StudentCity,StudentProfile.StudentState,StudentProfile.StudentCountry,StudentProfile.StudentContactNo,ParentProfile.FatherFullname,ParentProfile.FatherJob,ParentProfile.FamilyContactNo,ParentProfile.MotherFullname,ParentProfile.FamilyContactNoIbu,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,SchoolProfile.SchoolType,SchoolProfile.SchoolLokasi,UKM1.Status as UKM1Status,UKM1.ExamStart as UKM1ExamStart,UKM1.ExamEnd as UKM1ExamEnd,UKM1.QuestionYear as UKM1QuestionYear,UKM1.TotalPercentage as UKM1TotalPercentage,UKM2.ExamStart as UKM2ExamStart,UKM2.ExamEnd as UKM2ExamEnd,UKM2.TotalPercentage as UKM2TotalPercentage,UKM2.Mental_Age_Year,UKM2.Student_IQ,UKM2.WMI,UKM2.SelectedLang as UKM2SelectedLang,UKM2.TimeTaken FROM UKM2"
        If export = 1 Then
            tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentType,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentReligion,StudentProfile.StudentGender,StudentProfile.StudentRace,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,UKM1.ModA,UKM1.ModB,UKM1.ModC,UKM1.TotalScore as ukm1TotalScore, UKM1.TotalPercentage as UKM1TotalPercentage ,UKM2.VCI,UKM2.PRI,UKM2.WMI,UKM2.PSI,UKM2.TotalScore as ukm2TotalScore,UKM2.TotalPercentage as UKM2TotalPercentage,UKM2.Mental_Age_Year,UKM2.Student_IQ,UKM2.SelectedLang as UKM2SelectedLang,PusatUjian.PusatName FROM UKM2"
            tmpSQL += " LEFT OUTER JOIN PusatUjian ON UKM2.PusatCode = PusatUjian.PusatCode"
        Else
            tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentType,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentReligion,StudentProfile.StudentGender,SchoolProfile.SchoolCode,UKM1.TotalPercentage as UKM1TotalPercentage,UKM2.TotalPercentage as UKM2TotalPercentage,UKM2.Mental_Age_Year,UKM2.Student_IQ,UKM2.WMI,UKM2.SelectedLang as UKM2SelectedLang FROM UKM2"
        End If

        tmpSQL += " LEFT OUTER JOIN UKM1 ON UKM2.StudentID=UKM1.StudentID AND UKM1.examyear_id='" & ddlExamYear.SelectedValue & "'"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM2.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM2.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM2.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE UKM2.ExamYear='" & ddlExamYear.SelectedItem.Text & "'"

        '--SchoolState
        If Not ddlSchoolState.Text = "ALL" Then
            strWhere += " AND SchoolProfile.SchoolState='" & ddlSchoolState.Text & "'"
        End If

        '--SchoolPPD
        If Not ddlSchoolPPD.Text = "ALL" Then
            strWhere += " AND SchoolProfile.SchoolPPD='" & ddlSchoolPPD.Text & "'"
        End If

        '--DOB_Year
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND StudentProfile.DOB_Year='" & ddlDOB_Year.Text & "'"
        End If

        '--StudentReligion
        If Not ddlStudentReligion.Text = "ALL" Then
            strWhere += " AND StudentProfile.StudentReligion='" & ddlStudentReligion.Text & "'"
        End If

        '--StudentGender
        If Not selStudentGender.Value = "ALL" Then
            strWhere += " AND StudentProfile.StudentGender='" & selStudentGender.Value & "'"
        End If

        '--MYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--ExamEnd
        If Not txtExamStart.Text.Length = 0 Then
            strWhere += " AND UKM2.ExamStart LIKE '" & oCommon.FixSingleQuotes(txtExamStart.Text) & "%'"
        End If

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        '--txtSchoolName
        If Not txtSchoolName.Text.Length = 0 Then
            strWhere += " AND  SchoolProfile.SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text) & "%'"
        End If

        '--ddlSchoolType
        If Not ddlSchoolType.Text = "ALL" Then
            strWhere += " AND SchoolProfile.SchoolType='" & ddlSchoolType.Text & "'"
        End If

        '--AlumniID
        If Not txtAlumniID.Text.Length = 0 Then
            strWhere += " AND StudentProfile.AlumniID LIKE '" & oCommon.FixSingleQuotes(txtAlumniID.Text) & "%'"
        End If

        '--order by
        Select Case selSort.Value
            Case "0"
                strOrder = " ORDER BY UKM2.TotalScore DESC"
            Case "1"
                strOrder = " ORDER BY UKM2.TotalScore DESC,UKM2.WMI DESC"
            Case Else
                strOrder = " ORDER BY UKM2.TotalScore DESC"
        End Select

        '--AlumniID
        If chkAlumni.Checked = True Then
            strWhere += " AND StudentProfile.AlumniID IS NOT NULL"
        End If

        '--not exisit inside PPCS
        'If chkNotExist.Checked = True Then
        '    strWhere += " AND NOT EXISTS (SELECT StudentID FROM PPCS WHERE UKM2.StudentID=PPCS.StudentID AND PPCS.PPCSDate='" & ddlPPCSDateSearch.Text & "')"
        'Else
        '    strWhere += " AND EXISTS (SELECT StudentID FROM PPCS WHERE UKM2.StudentID=PPCS.StudentID AND PPCS.PPCSDate='" & ddlPPCSDateSearch.Text & "')"
        'End If

        Select Case ddlPPCSStatusSearch.Text
            Case "ALL"
                '--do nothing.
            Case "PEMILIHAN"
                strWhere += " AND NOT EXISTS (SELECT StudentID FROM PPCS WHERE UKM2.StudentID=PPCS.StudentID AND PPCS.PPCSDate='" & ddlPPCSDateSearch.Text & "')"
            Case Else
                strWhere += " AND EXISTS (SELECT StudentID FROM PPCS WHERE UKM2.StudentID=PPCS.StudentID AND PPCS.PPCSDate='" & ddlPPCSDateSearch.Text & "' AND PPCS.PPCSStatus='" & ddlPPCSStatusSearch.Text & "')"
        End Select

        strSQL = tmpSQL & strWhere & strOrder

        Return strSQL

    End Function

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPPCSDate As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

            lblPPCSDate = e.Row.FindControl("lblPPCSDate")
            lblPPCSDate.Text = getPPCSDate(strKeyID)

        End If
    End Sub

    Private Function getPPCSDate(ByVal strStudentID As String) As String
        ''--get the date
        strSQL = "SELECT PPCSDate,PPCSStatus FROM PPCS WHERE StudentID='" & strStudentID & "' ORDER BY PPCSID ASC"
        strRet = oCommon.getRowValue(strSQL)
        Return strRet

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ppcs.alumni.studentprofile.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

    Private Sub UncheckAll()
        Dim row As GridViewRow
        For Each row In datRespondent.Rows
            Dim chkUncheck As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            chkUncheck.Checked = False
        Next

    End Sub

    Private Function setLayak(ByVal strStudentID As String) As Boolean
        ''--check duplicate entry. no duplicate studentid and examyear
        strSQL = "SELECT StudentID FROM PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            ''--UPDATE
            '--DR Siti request not to update statustawaran. Only students can update it. Default blank/null
            strSQL = "UPDATE PPCS WITH (UPDLOCK) SET PPCSStatus='LAYAK'" & "' WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        Else
            ''--INSERT
            strSQL = "INSERT INTO PPCS (StudentID,PPCSDate,PPCSStatus,StatusTawaran) VALUES('" & strStudentID & "','" & ddlPPCSDate.Text & "','LAYAK',NULL)"
        End If
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function

    Private Function setPPCSStatus(ByVal strStudentID As String, ByVal strPPCSStatus As String) As Boolean
        ''--check duplicate entry. no duplicate studentid and examyear
        strSQL = "SELECT StudentID FROM PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            ''--UPDATE
            '--DR Siti request not to update statustawaran. Only students can update it. Default blank/null
            strSQL = "UPDATE PPCS WITH (UPDLOCK) SET PPCSStatus='" & strPPCSStatus & "' WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        Else
            ''--INSERT
            strSQL = "INSERT INTO PPCS (StudentID,PPCSDate,PPCSStatus,StatusTawaran) VALUES('" & strStudentID & "','" & ddlPPCSDate.Text & "','" & strPPCSStatus & "',NULL)"
        End If
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True
    End Function

    Private Function setTidakLayak(ByVal strStudentID As String) As Boolean

        ''--insert UKM2
        strSQL = "DELETE PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function

    Private Function getSQLMark() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY a.TotalScore DESC"

        tmpSQL = "SELECT b.StudentID,b.StudentFullname,b.MYKAD,b.DOB_Year,b.StudentRace,b.StudentGender,b.AlumniID,a.TotalScore as ukm2TotalScore,a.TotalPercentage as ukm2TotalPercentage,a.ExamYear as UKM2ExamYear,c.TotalScore as ukm1TotalScore,c.TotalPercentage as ukm1TotalPercentage,c.ExamYear as UKM1ExamYear,(SELECT PPCSStatus FROM PPCS WHERE a.StudentID=StudentID AND PPCSDate='" & ddlPPCSDate.Text & "') as PPCSStatus FROM UKM2 a,StudentProfile b,UKM1 c"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.StudentID=c.StudentID AND a.ExamYear='" & ddlExamYear.Text & "'"

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND b.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        getSQLMark = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQLMark

    End Function


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlSchoolState_TextChanged(sender As Object, e As EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_PPD_list()

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV()
        'Get the data from database into datatable 
        Dim strQuery As String = getSQL(1)
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileExport.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim i As Integer = 0
        lblMsgTop.Text = ""
        lblMsg.Text = ""

        '-validate
        If ddlPPCSStatus.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih Status PPCS untuk diKEMASKINI."
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        Select Case ddlPPCSStatus.Text
            Case "TIDAK LAYAK"
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
                    ''--debug
                    'Response.Write(chkUpdate)
                    If Not chkUpdate Is Nothing Then
                        If chkUpdate.Checked = True Then
                            ' Get the values of textboxes using findControl
                            ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                            Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                            ''--debug
                            'Response.Write(strID)& "|"
                            If setTidakLayak(strID) = True Then
                                lblMsg.Text = "Berjaya reset kelayakan pelajar kepada TIDAK LAYAK."     ''& strID
                            Else
                                lblMsg.Text += "NOK:" & strID & vbCrLf
                            End If
                        End If
                    End If
                Next

            Case Else
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
                    ''--debug
                    'Response.Write(chkUpdate)
                    If Not chkUpdate Is Nothing Then
                        If chkUpdate.Checked = True Then
                            ' Get the values of textboxes using findControl
                            ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                            Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                            ''--debug
                            ''Response.Write(strID)
                            If setPPCSStatus(strID, ddlPPCSStatus.Text) = True Then
                                lblMsg.Text = "Berjaya melayakkan semua pelajar yang dipilih."
                            Else
                                lblMsg.Text += "NOK:" & strID & vbCrLf
                            End If

                        End If
                    End If
                Next

        End Select

        'UncheckAll()
        strRet = BindData(datRespondent)

    End Sub

End Class