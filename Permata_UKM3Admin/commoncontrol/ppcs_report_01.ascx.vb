Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ppcs_report_01
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

                '-list of exam avail
                'examyear_list()
                'ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

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

                master_PPCSStatus_list()

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub master_PPCSStatus_list()
        strSQL = "SELECT PPCSStatus FROM master_PPCSStatus ORDER BY PPCSStatus ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSSstatus.DataSource = ds
            ddlPPCSSstatus.DataTextField = "PPCSStatus"
            ddlPPCSSstatus.DataValueField = "PPCSStatus"
            ddlPPCSSstatus.DataBind()

            ddlPPCSSstatus.Items.Add(New ListItem("ALL", "ALL"))
            ddlPPCSSstatus.SelectedValue = "ALL"

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

            ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))
            ddlPPCSDate.SelectedValue = "ALL"

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

    'Private Sub examyear_list()
    '    '--Limit examyear access
    '    Select Case getUserProfile_UserType()
    '        Case "ADMIN"
    '            strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
    'Case "ADMINOP"
    '            strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
    '        Case "SUBADMIN"
    '            strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
    '        Case "KPT"
    '            strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
    '        Case "ASASI"
    '            strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
    '        Case "UKM"
    '            strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & oCommon.getAppsettings("DefaultExamYear") & "%'  ORDER BY ExamYear"
    '        Case Else
    '            strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
    '    End Select

    '    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    '    Dim objConn As SqlConnection = New SqlConnection(strConn)
    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Try
    '        Dim ds As DataSet = New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        ddlExamYear.DataSource = ds
    '        ddlExamYear.DataTextField = "ExamYear"
    '        ddlExamYear.DataValueField = "ExamYear"
    '        ddlExamYear.DataBind()

    '        '--ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

    '    Catch ex As Exception
    '        lblMsg.Text = "Database error!" & ex.Message
    '    Finally
    '        objConn.Dispose()
    '    End Try

    'End Sub

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
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

    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        'tmpSQL = "SELECT StudentProfile.StudentID, StudentProfile.StudentFullname, StudentProfile.MYKAD, StudentProfile.AlumniID, StudentProfile.DOB_Year, StudentProfile.StudentRace, StudentProfile.StudentReligion, StudentProfile.StudentGender, StudentProfile.StudentAddress1, StudentProfile.StudentAddress2, StudentProfile.StudentPostcode, StudentProfile.StudentCity, StudentProfile.StudentState, StudentProfile.StudentCountry, StudentProfile.StudentContactNo, ParentProfile.FatherFullname, ParentProfile.FatherJob, ParentProfile.FamilyContactNo, ParentProfile.MotherFullname, ParentProfile.FamilyContactNoIbu, SchoolProfile.SchoolName, SchoolProfile.SchoolState, SchoolProfile.SchoolPPD, SchoolProfile.SchoolCity, SchoolProfile.SchoolType, SchoolProfile.SchoolLokasi, UKM2.ExamStart as UKM2ExamStart, UKM2.ExamEnd as UKM2ExamEnd, UKM2.TotalPercentage as UKM2TotalPercentage, UKM2.Mental_Age_Year, UKM2.Student_IQ, UKM2.WMI, UKM2.SelectedLang as UKM2SelectedLang, UKM2.TimeTaken, PPCS.PPCSDate, PPCS.PPCSStatus, PPCS_Course.CourseNameBM FROM UKM2"
        'tmpSQL += " LEFT OUTER JOIN PPCS ON UKM2.StudentID=PPCS.StudentID AND PPCS.PPCSDate IS NOT NULL"
        'tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        'tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM2.StudentID=StudentProfile.StudentID"
        'tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM2.StudentID=ParentProfile.StudentID"
        'tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM2.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        'tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        'strWhere = " WHERE UKM2.ExamYear='" & ddlExamYear.Text & "'"

        tmpSQL = "SELECT PPCS.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentEmail,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentGender,StudentProfile.StudentAddress1,StudentProfile.StudentAddress2,StudentProfile.StudentPostcode,StudentProfile.StudentCity,StudentProfile.StudentState,StudentProfile.StudentCountry,StudentProfile.StudentContactNo,ParentProfile.FatherFullname,ParentProfile.FatherJob,ParentProfile.FamilyContactNo,ParentProfile.MotherFullname,ParentProfile.MotherJob,ParentProfile.FamilyContactNoIbu,SchoolProfile.SchoolName,SchoolProfile.SchoolAddress,SchoolProfile.SchoolState,SchoolProfile.SchoolPostcode,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,PPCS.PPCSDate,PPCS.PPCSStatus,PPCS.StatusTawaran,PPCS_Course.CourseCode,PPCS_Course.CourseNameBM,PPCS_Class.ClassCode FROM PPCS"
        'tmpSQL += " LEFT OUTER JOIN UKM2 ON PPCS.StudentID=UKM2.StudentID AND UKM2.ExamYear='" & ddlExamYear.Text & "'"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON PPCS.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON PPCS.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        strWhere = " WHERE PPCS.PPCSDate='" & ddlPPCSDate.Text & "'"

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

        '--MYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
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

        '--AlumniID
        If chkAlumni.Checked = True Then
            strWhere += " AND StudentProfile.AlumniID IS NOT NULL"
        End If

        '--PPCSSstatus
        If Not ddlPPCSSstatus.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSStatus='" & ddlPPCSSstatus.Text & "'"
        End If

        '--PPCSDate
        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSDate='" & ddlPPCSDate.Text & "'"
        End If

        '--order by
        Select Case selSort.Value
            Case "0"
                strOrder = " ORDER BY StudentProfile.StudentFullname ASC, PPCS.PPCSID DESC"
            Case "1"
                strOrder = " ORDER BY PPCS.PPCSID DESC"
            Case Else
                strOrder = " ORDER BY StudentProfile.StudentFullname ASC, PPCS.PPCSID DESC"
        End Select

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)
        'lblSQLDebug.Text = getSQL

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function getPPCSDate(ByVal strStudentID As String) As String
        ''--get the date
        strSQL = "Select PPCSDate FROM PPCS WHERE StudentID='" & strStudentID & "' ORDER BY PPCSID ASC"
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
        Dim strQuery As String = getSQL()
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

End Class