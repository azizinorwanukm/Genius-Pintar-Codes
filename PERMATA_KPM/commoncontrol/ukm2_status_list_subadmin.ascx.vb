Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData
Imports System.Threading
Imports System.Resources

Public Class ukm2_status_list_subadmin
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim strUserType As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnExecute.Attributes.Add("onclick", "return confirm('Pasti ingin meneruskan fungsi tersebut?');")

        Try
            lblMsgTop.Text = ""
            strUserType = getUserProfile_UserType()

            ''set US for calculation
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            ci = Thread.CurrentThread.CurrentCulture

            If Not IsPostBack Then
                txtExamStart.Text = oCommon.getToday
                calUKM2.SelectedDate = Now.Date

                SchoolState_list()

                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                '--load UKM2 menu base on usertype
                master_menu_list()
                ddlMenudesc.SelectedValue = "00"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub master_menu_list()
        '--visibility
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                selLang.Visible = True
            Case Else
                selLang.Visible = False
        End Select

        strSQL = "SELECT menudesc,menucode FROM master_menu WHERE menucategory='UKM2' AND usertype='" & getUserProfile_UserType() & "' ORDER BY menucode"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMenudesc.DataSource = ds
            ddlMenudesc.DataTextField = "menudesc"
            ddlMenudesc.DataValueField = "menucode"
            ddlMenudesc.DataBind()

            ddlMenudesc.Items.Add(New ListItem("--Select--", "00"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub SchoolState_list()
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) WHERE SchoolState<>'UKM2-KPT' AND SchoolState <>'UKM2-ASASI'  ORDER BY SchoolStateID"
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

            ''ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))
            ''default state
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If
            ''debug
            'Response.Write(getUserProfile_State())

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

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
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case "UKM"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & oCommon.getAppsettings("DefaultExamYear") & "%'  ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
        End Select

        '--debug
        'Response.Write("examyear_list:" & strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "ExamYear"
            ddlExamYear.DataBind()

            '--ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
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
        Dim myDataAdapter As New SqlDataAdapter(getSQL(0), strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak dijumpai."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL(ByVal nType As Integer) As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY UKM2.ExamStart DESC"


        If nType = 1 Then
            '--export with mark
            tmpSQL = "SELECT UKM2.StudentID,UKM2.HostAddress,StudentProfile.MYKAD,StudentProfile.StudentFullname,StudentProfile.DOB_Year,StudentProfile.StudentCity,SchoolProfile.SchoolID,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.Schoolcity,SchoolProfile.SchoolCode,SchoolProfile.Schoolname,UKM2.ExamStart,UKM2.ExamEnd,UKM2.SelectedLang,UKM2.IsHadir,UKM2.IsLogin,UKM2.ExamYear,UKM2.TarikhUjian,UKM2.SessiUKM2,UKM2.Status,UKM2.LastPage,ParentProfile.FamilyContactNo,ParentProfile.FatherFullname,PusatUjian.PusatCode,PusatUjian.PusatState,PusatUjian.PusatName,UKM2.Mod01,UKM2.Mod02,UKM2.Mod03,UKM2.Mod04,UKM2.Mod05,UKM2.Mod06,UKM2.Mod07,UKM2.Mod08,UKM2.Mod09,UKM2.Mod10,UKM2.Mod11,UKM2.Mod12,UKM2.Mod13,UKM2.Mod14,UKM2.Mod15,UKM2.TotalScore,UKM2.TotalPercentage as ukm2TotalPercentage,UKM2.VCI,UKM2.PRI,UKM2.WMI,UKM2.PSI FROM UKM2"
        Else
            '--export without mark
            tmpSQL = "SELECT UKM2.StudentID,UKM2.HostAddress,StudentProfile.MYKAD,StudentProfile.StudentFullname,StudentProfile.DOB_Year,StudentProfile.StudentCity,SchoolProfile.SchoolID,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.Schoolcity,SchoolProfile.SchoolCode,SchoolProfile.Schoolname,UKM2.ExamStart,UKM2.ExamEnd,UKM2.SelectedLang,UKM2.IsHadir,UKM2.IsLogin,UKM2.ExamYear,UKM2.TarikhUjian,UKM2.SessiUKM2,UKM2.Status,UKM2.LastPage,ParentProfile.FamilyContactNo,ParentProfile.FatherFullname,PusatUjian.PusatCode,PusatUjian.PusatState,PusatUjian.PusatName FROM UKM2"
        End If

        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM2.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM2.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN PusatUjian ON UKM2.PusatCode=PusatUjian.PusatCode"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM2.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE UKM2.ExamYear='" & ddlExamYear.Text & "'"

        '--TarikhUjian
        If Not txtExamStart.Text.Length = 0 Then
            strWhere += " AND UKM2.ExamStart LIKE '%" & oCommon.FixSingleQuotes(txtExamStart.Text) & "%'"
        End If

        '--isHadir
        If Not selIsHadir.Value = "ALL" Then
            strWhere += " AND UKM2.IsHadir='" & selIsHadir.Value & "'"
        End If

        ''status
        If Not selStatus.Value = "ALL" Then
            strWhere += " AND UKM2.Status='" & selStatus.Value & "'"
        End If

        '--txtMYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        ''DOB_Year
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND StudentProfile.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        ''ddlSchoolState
        If Not ddlSchoolState.Text = "ALL" Then
            strWhere += " AND SchoolProfile.SchoolState='" & oCommon.FixSingleQuotes(ddlSchoolState.Text) & "'"
        End If

        ''txtPusatName
        If Not txtPusatName.Text.Length = 0 Then
            strWhere += " AND PusatUjian.PusatName LIKE '%" & oCommon.FixSingleQuotes(txtPusatName.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
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

    Private Function ExportData(ByVal dsTable As DataSet, ByVal strTitle As String) As String
        ''-Dim strFilename As String = Server.MapPath(".") & "log\" & "Export." & oCommon.getRandom & ".txt"
        Dim strFilename As String = strTitle & oCommon.getRandom & ".txt"

        Try
            ' Export all the details to xls
            Dim objExport As New RKLib.ExportData.Export("Web")
            Dim dtRespondent As DataTable = dsTable.Tables("mytable").Copy()
            objExport.ExportDetails(dtRespondent, Export.ExportFormat.CSV, strFilename)

            Return strFilename
        Catch Ex As Exception
            Return Ex.Message
        End Try

    End Function

    '--generate MARKS and INDEX
    Private Sub ExamEnd_update(ByVal strStudentID As String)
        ''--student have answer the question. just exit
        strSQL = "SELECT ExamEnd FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "' AND ExamEnd IS NULL"
        If oCommon.isExist(strSQL) = True Then
            ''--update UKM2 Status, ExamEnd
            strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='DONE',IsLogin='N',ExamEnd='" & oCommon.getNow & "' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else
            ''--update UKM2 Status only
            strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='DONE',IsLogin='N' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        ''Sumary
        GenerateSum(strStudentID)
        GenerateIndex(strStudentID)

    End Sub

    Private Sub GenerateSum(ByVal strStudentID As String)
        Dim strMod1, strMod2, strMod3, strMod4, strMod5, strMod6, strMod7, strMod8, strMod9, strMod10, strMod11, strMod12, strMod13, strMod14, strMod15 As String

        ''-mod1
        strSQL = "SELECT SUM(isnull(Q001,0)+isnull(Q002,0)+isnull(Q003,0)+isnull(Q004,0)+isnull(Q005,0)+isnull(Q006,0)+isnull(Q007,0)+isnull(Q008,0)+isnull(Q009,0)+isnull(Q010,0)+isnull(Q011,0)+isnull(Q012,0)+isnull(Q013,0)+isnull(Q014,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod1 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q015,0)+isnull(Q016,0)+isnull(Q017,0)+isnull(Q018,0)+isnull(Q019,0)+isnull(Q020,0)+isnull(Q021,0)+isnull(Q022,0)+isnull(Q023,0)+isnull(Q024,0)+isnull(Q025,0)+isnull(Q026,0)+isnull(Q027,0)+isnull(Q028,0)+isnull(Q029,0)+isnull(Q030,0)+isnull(Q031,0)+isnull(Q032,0)+isnull(Q033,0)+isnull(Q034,0)+isnull(Q035,0)+isnull(Q036,0)+isnull(Q037,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod2 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q038,0)+isnull(Q039,0)+isnull(Q040,0)+isnull(Q041,0)+isnull(Q042,0)+isnull(Q043,0)+isnull(Q044,0)+isnull(Q045,0)+isnull(Q046,0)+isnull(Q047,0)+isnull(Q048,0)+isnull(Q049,0)+isnull(Q050,0)+isnull(Q051,0)+isnull(Q052,0)+isnull(Q053,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod3 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q054,0)+isnull(Q055,0)+isnull(Q056,0)+isnull(Q057,0)+isnull(Q058,0)+isnull(Q059,0)+isnull(Q060,0)+isnull(Q061,0)+isnull(Q062,0)+isnull(Q063,0)+isnull(Q064,0)+isnull(Q065,0)+isnull(Q066,0)+isnull(Q067,0)+isnull(Q068,0)+isnull(Q069,0)+isnull(Q070,0)+isnull(Q071,0)+isnull(Q072,0)+isnull(Q073,0)+isnull(Q074,0)+isnull(Q075,0)+isnull(Q076,0)+isnull(Q077,0)+isnull(Q078,0)+isnull(Q079,0)+isnull(Q080,0)+isnull(Q081,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod4 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q082,0)+isnull(Q083,0)+isnull(Q084,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod5 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q085,0)+isnull(Q086,0)+isnull(Q087,0)+isnull(Q088,0)+isnull(Q089,0)+isnull(Q090,0)+isnull(Q091,0)+isnull(Q092,0)+isnull(Q093,0)+isnull(Q094,0)+isnull(Q095,0)+isnull(Q096,0)+isnull(Q097,0)+isnull(Q098,0)+isnull(Q099,0)+isnull(Q100,0)+isnull(Q101,0)+isnull(Q102,0)+isnull(Q103,0)+isnull(Q104,0)+isnull(Q105,0)+isnull(Q106,0)+isnull(Q107,0)+isnull(Q108,0)+isnull(Q109,0)+isnull(Q110,0)+isnull(Q111,0)+isnull(Q112,0)+isnull(Q113,0)+isnull(Q114,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod6 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q115,0)+isnull(Q116,0)+isnull(Q117,0)+isnull(Q118,0)+isnull(Q119,0)+isnull(Q120,0)+isnull(Q121,0)+isnull(Q122,0)+isnull(Q123,0)+isnull(Q124,0)+isnull(Q125,0)+isnull(Q126,0)+isnull(Q127,0)+isnull(Q128,0)+isnull(Q129,0)+isnull(Q130,0)+isnull(Q131,0)+isnull(Q132,0)+isnull(Q133,0)+isnull(Q134,0)+isnull(Q135,0)+isnull(Q136,0)+isnull(Q137,0)+isnull(Q138,0)+isnull(Q139,0)+isnull(Q140,0)+isnull(Q141,0)+isnull(Q142,0)+isnull(Q143,0)+isnull(Q144,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod7 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q145,0)+isnull(Q146,0)+isnull(Q147,0)+isnull(Q148,0)+isnull(Q149,0)+isnull(Q150,0)+isnull(Q151,0)+isnull(Q152,0)+isnull(Q153,0)+isnull(Q154,0)+isnull(Q155,0)+isnull(Q156,0)+isnull(Q157,0)+isnull(Q158,0)+isnull(Q159,0)+isnull(Q160,0)+isnull(Q161,0)+isnull(Q162,0)+isnull(Q163,0)+isnull(Q164,0)+isnull(Q165,0)+isnull(Q166,0)+isnull(Q167,0)+isnull(Q168,0)+isnull(Q169,0)+isnull(Q170,0)+isnull(Q171,0)+isnull(Q172,0)+isnull(Q173,0)+isnull(Q174,0)+isnull(Q175,0)+isnull(Q176,0)+isnull(Q177,0)+isnull(Q178,0)+isnull(Q179,0)+isnull(Q180,0)+isnull(Q181,0)+isnull(Q182,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod8 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q183,0)+isnull(Q184,0)+isnull(Q185,0)+isnull(Q186,0)+isnull(Q187,0)+isnull(Q188,0)+isnull(Q189,0)+isnull(Q190,0)+isnull(Q191,0)+isnull(Q192,0)+isnull(Q193,0)+isnull(Q194,0)+isnull(Q195,0)+isnull(Q196,0)+isnull(Q197,0)+isnull(Q198,0)+isnull(Q199,0)+isnull(Q200,0)+isnull(Q201,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod9 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q202,0)+isnull(Q203,0)+isnull(Q204,0)+isnull(Q205,0)+isnull(Q206,0)+isnull(Q207,0)+isnull(Q208,0)+isnull(Q209,0)+isnull(Q210,0)+isnull(Q211,0)+isnull(Q212,0)+isnull(Q213,0)+isnull(Q214,0)+isnull(Q215,0)+isnull(Q216,0)+isnull(Q217,0)+isnull(Q218,0)+isnull(Q219,0)+isnull(Q220,0)+isnull(Q221,0)+isnull(Q222,0)+isnull(Q223,0)+isnull(Q224,0)+isnull(Q225,0)+isnull(Q226,0)+isnull(Q227,0)+isnull(Q228,0)+isnull(Q229,0)+isnull(Q230,0)+isnull(Q231,0)+isnull(Q232,0)+isnull(Q233,0)+isnull(Q234,0)+isnull(Q235,0)+isnull(Q236,0)+isnull(Q237,0)+isnull(Q238,0)+isnull(Q239,0)+isnull(Q240,0)+isnull(Q241,0)+isnull(Q242,0)+isnull(Q243,0)+isnull(Q244,0)+isnull(Q245,0)+isnull(Q246,0)+isnull(Q247,0)+isnull(Q248,0)+isnull(Q249,0)+isnull(Q250,0)+isnull(Q251,0)+isnull(Q252,0)+isnull(Q253,0)+isnull(Q254,0)+isnull(Q255,0)+isnull(Q256,0)+isnull(Q257,0)+isnull(Q258,0)+isnull(Q259,0)+isnull(Q260,0)+isnull(Q261,0)+isnull(Q262,0)+isnull(Q263,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod10 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q264,0)+isnull(Q265,0)+isnull(Q266,0)+isnull(Q267,0)+isnull(Q268,0)+isnull(Q269,0)+isnull(Q270,0)+isnull(Q271,0)+isnull(Q272,0)+isnull(Q273,0)+isnull(Q274,0)+isnull(Q275,0)+isnull(Q276,0)+isnull(Q277,0)+isnull(Q278,0)+isnull(Q279,0)+isnull(Q280,0)+isnull(Q281,0)+isnull(Q282,0)+isnull(Q283,0)+isnull(Q284,0)+isnull(Q285,0)+isnull(Q286,0)+isnull(Q287,0)+isnull(Q288,0)+isnull(Q289,0)+isnull(Q290,0)+isnull(Q291,0)+isnull(Q292,0)+isnull(Q293,0)+isnull(Q294,0)+isnull(Q295,0)+isnull(Q296,0)+isnull(Q297,0)+isnull(Q298,0)+isnull(Q299,0)+isnull(Q300,0)+isnull(Q301,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod11 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q302,0)+isnull(Q303,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod12 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q304,0)+isnull(Q305,0)+isnull(Q306,0)+isnull(Q307,0)+isnull(Q308,0)+isnull(Q309,0)+isnull(Q310,0)+isnull(Q311,0)+isnull(Q312,0)+isnull(Q313,0)+isnull(Q314,0)+isnull(Q315,0)+isnull(Q316,0)+isnull(Q317,0)+isnull(Q318,0)+isnull(Q319,0)+isnull(Q320,0)+isnull(Q321,0)+isnull(Q322,0)+isnull(Q323,0)+isnull(Q324,0)+isnull(Q325,0)+isnull(Q326,0)+isnull(Q327,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod13 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q328,0)+isnull(Q329,0)+isnull(Q330,0)+isnull(Q331,0)+isnull(Q332,0)+isnull(Q333,0)+isnull(Q334,0)+isnull(Q335,0)+isnull(Q336,0)+isnull(Q337,0)+isnull(Q338,0)+isnull(Q339,0)+isnull(Q340,0)+isnull(Q341,0)+isnull(Q342,0)+isnull(Q343,0)+isnull(Q344,0)+isnull(Q345,0)+isnull(Q346,0)+isnull(Q347,0)+isnull(Q348,0)+isnull(Q349,0)+isnull(Q350,0)+isnull(Q351,0)+isnull(Q352,0)+isnull(Q353,0)+isnull(Q354,0)+isnull(Q355,0)+isnull(Q356,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod14 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q357,0)+isnull(Q358,0)+isnull(Q359,0)+isnull(Q360,0)+isnull(Q361,0)+isnull(Q362,0)+isnull(Q363,0)+isnull(Q364,0)+isnull(Q365,0)+isnull(Q366,0)+isnull(Q367,0)+isnull(Q368,0)+isnull(Q369,0)+isnull(Q370,0)+isnull(Q371,0)+isnull(Q372,0)+isnull(Q373,0)+isnull(Q374,0)+isnull(Q375,0)+isnull(Q376,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strMod15 = oCommon.getFieldValue(strSQL)

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Mod01=" & strMod1 & ",Mod02=" & strMod2 & ",Mod03=" & strMod3 & ",Mod04=" & strMod4 & ",Mod05=" & strMod5 & ",Mod06=" & strMod6 & ",Mod07=" & strMod7 & ",Mod08=" & strMod8 & ",Mod09=" & strMod9 & ",Mod10=" & strMod10 & ",Mod11=" & strMod11 & ",Mod12=" & strMod12 & ",Mod13=" & strMod13 & ",Mod14=" & strMod14 & ",Mod15=" & strMod15 & " WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub

    Private Sub GenerateIndex(ByVal strStudentID As String)
        Dim strVCI, strPRI, strWMI, strPSI As String
        Dim strTotalScore As String

        'VCI (Verbal Completion Index)	2+6+9+13+15  (verbal)
        strSQL = "SELECT SUM(isnull(Mod02,0)+isnull(Mod06,0)+isnull(Mod09,0)+isnull(Mod13,0)+isnull(Mod15,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strVCI = oCommon.getFieldValue(strSQL)

        'PRI (Perseptual Reasoning Index)	1+4+8+12 (science+math)
        strSQL = "SELECT SUM(isnull(Mod01,0)+isnull(Mod04,0)+isnull(Mod08,0)+isnull(Mod12,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strPRI = oCommon.getFieldValue(strSQL)

        'WMI(Working Memory Index)	3+7+14 (sokongan VCI/PRI)
        strSQL = "SELECT SUM(isnull(Mod03,0)+isnull(Mod07,0)+isnull(Mod14,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strWMI = oCommon.getFieldValue(strSQL)

        'PSI(Processing Speed Index)	5+10+11 (sokongan VCI/PRI)
        strSQL = "SELECT SUM(isnull(Mod05,0)+isnull(Mod10,0)+isnull(Mod11,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strPSI = oCommon.getFieldValue(strSQL)

        ''OK
        strSQL = "UPDATE UKM2 set VCI=" & strVCI & ",PRI=" & strPRI & ",WMI=" & strWMI & ",PSI=" & strPSI & " WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        'get strTotalScore. NOK
        strSQL = "SELECT SUM(isnull(VCI,0)+isnull(PRI,0)+isnull(WMI,0)+isnull(PSI,0)) as SumA FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strTotalScore = oCommon.getFieldValue(strSQL)

        Dim dblTotalPercentage As Double
        dblTotalPercentage = (CInt(strTotalScore) / 692) * 100
        Dim strTotalPercentage As String = oCommon.DoConvertD(dblTotalPercentage, 4)

        'update TotalPercentage
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET FullMark=692,TotalPercentage=" & strTotalPercentage & ",TotalScore=" & strTotalScore & " WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            ''--debug
            'Response.Write("TotalPercentage:" & strRet)
        End If

    End Sub

    Private Sub UKM2_Update(ByVal strStudentID As String)
        Dim strWhere As String = ""
        Dim strOrderBy As String = ""
        Dim tmpSQL As String = ""

        Try
            ''--setup all records to generate completed records only
            Dim strSchoolID As String = ""
            Dim strSchoolState As String = ""
            Dim strSchoolCity As String = ""
            Dim strSchoolType As String = ""
            Dim strSchoolPPD As String = ""
            Dim strSchoolLokasi As String = ""

            strWhere = ""
            strOrderBy = ""
            tmpSQL = ""

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            tmpSQL = "SELECT a.StudentID,c.SchoolID,c.SchoolState,c.SchoolCity,c.SchoolType,c.SchoolPPD,c.SchoolLokasi FROM UKM2 a, StudentSchool b, SchoolProfile c"
            strWhere += " WHERE a.Studentid='" & strStudentID & "' AND a.StudentID=b.StudentID AND b.SchoolID=c.SchoolID AND a.ExamYear='" & ddlExamYear.Text & "'"
            strSQL = tmpSQL + strWhere

            Dim mySourceDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)
            myDataAdapter.Fill(mySourceDataSet, "myaccount")
            objConn.Close()

            For i As Integer = 0 To mySourceDataSet.Tables("myaccount").Rows.Count - 1
                strSchoolID = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolID")

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolState")) Then
                    strSchoolState = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolState")
                Else
                    strSchoolState = ""
                End If

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolCity")) Then
                    strSchoolCity = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolCity")
                Else
                    strSchoolCity = ""
                End If

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolType")) Then
                    strSchoolType = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolType")
                Else
                    strSchoolType = ""
                End If

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolPPD")) Then
                    strSchoolPPD = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolPPD")
                Else
                    strSchoolPPD = ""
                End If

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolLokasi")) Then
                    strSchoolLokasi = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolLokasi")
                Else
                    strSchoolLokasi = ""
                End If

                strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & strSchoolID & "',SchoolState='" & strSchoolState & "',SchoolCity='" & strSchoolCity & "',SchoolType='" & strSchoolType & "',SchoolPPD='" & strSchoolPPD & "',SchoolLokasi='" & strSchoolLokasi & "' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If Not strRet = "0" Then
                    lblMsg.Text = strRet
                End If
            Next
        Catch ex As Exception
            lblMsg.Text = "UKM2_Update:" & ex.Message
        End Try

    End Sub

    Protected Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecute.Click
        lblMsg.Text = ddlMenudesc.SelectedValue

        Select Case ddlMenudesc.SelectedValue
            Case "00"
                lblMsg.Text = "Please select functions to execute!"

            Case "01"   'Hadir
                Execute_01()

            Case "02"   'Tidak Hadir
                Execute_02()

            Case "03"   'Tukar Bahasa
                Execute_03()

            Case "04"   'Reset UKM2
                Execute_04()

            Case "05"   'Reset ExamStart
                Execute_05()

            Case "06"   'Logout UKM2
                Execute_06()

            Case "07"   'Set DONE
                Execute_07()

            Case "08"   'Set NEW
                Execute_08()

            Case "09"   'Export List
                Execute_09()

            Case "10"   'Export Mark
                Execute_10()


            Case Else
                lblMsg.Text = "Please select functions to execute!"
        End Select

    End Sub

    '--Hadir
    Private Sub Execute_01()
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='Y' WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        ''--UncheckAll()
        strRet = BindData(datRespondent)
    End Sub

    '--Tidak Hadir
    Private Sub Execute_02()
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='N' WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        ''--UncheckAll()
        strRet = BindData(datRespondent)

    End Sub

    '--Tukar Bahasa
    Private Sub Execute_03()
        lblMsg.Text = ""
        If selLang.Value = "" Then
            lblMsg.Text = "Sila Pilih Bahasa terlebih dahulu."
            Exit Sub
        End If

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SelectedLang='" & selLang.Value & "' WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        ''--UncheckAll()
        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini BAHASA PILIHAN pelajar."
        End If

    End Sub

    '--Reset UKM2
    Private Sub Execute_04()
        Dim strPusatCode As String = ""
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    ''get pusatcode
                    strSQL = "SELECT PusatCode FROM UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strPusatCode = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TarikhUjian FROM UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    Dim strTarikhUjian As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT SessiUKM2 FROM UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    Dim strSessiUKM2 As String = oCommon.getFieldValue(strSQL)

                    '--get schoolid
                    Dim strSchoolID As String = ""
                    strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strID & "'"
                    strSchoolID = oCommon.getFieldValue(strSQL)

                    ''DELETE UKM2
                    strSQL = "DELETE UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "DELETE NOK:" & strID & strRet & vbCrLf
                    End If

                    ''INSERT BACK
                    strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2,Status) VALUES ('" & strID & "','" & ddlExamYear.Text & "','Y','" & strPusatCode & "','" & strSchoolID & "','" & strTarikhUjian & "','" & strSessiUKM2 & "','NEW')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "INSERT NOK:" & strID & strRet & vbCrLf
                    End If

                    '--school information
                    UKM2_Update(strID)

                    ''debug
                    ''Response.Write("INSERT:" & strSQL)
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya RESET Ujian UKM2 pelajar tersebut. Click [CARI] untuk REFRESH!"
        End If

        ''--UncheckAll()
        ''strRet = BindData(datRespondent)

    End Sub

    '--Reset ExamStart
    Private Sub Execute_05()
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    '--LastPage=NULL,SelectedLang=NULL (Dr Siti request to let the student proceed with new duration)
                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET ExamStart=NULL,ExamEnd=NULL,Status='NEW',IsHadir='Y' WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        ''--UncheckAll()
        strRet = BindData(datRespondent)

    End Sub

    '--Logout UKM2
    Private Sub Execute_06()
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET isLogin='N' WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        ''--UncheckAll()
        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya LOGOUT pelajar."
        End If

    End Sub

    '--Set DONE
    Private Sub Execute_07()
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='DONE' WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                    '--SUM MOD
                    oCommon.UKM2DONE_Mod(strID, ddlExamYear.Text)
                    '--SUM Index
                    oCommon.UKM2DONE_Index(strID, ddlExamYear.Text)
                    '--Mental_Age_Year and Student_IQ
                    strRet = oCommon.UKM2DONE_CountIQ(strID, ddlExamYear.Text)

                End If
            End If
        Next

        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar kepada DONE"
        End If

    End Sub

    '--Set NEW
    Private Sub Execute_08()
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='NEW' WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar kepada DONE"
        End If

    End Sub

    '--Export List
    Private Sub Execute_09()
        Try
            ExportToCSV(getSQL(0))

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    '--Export Mark
    Private Sub Execute_10()
        Try
            ExportToCSV(getSQL(1))
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
    End Sub


    Private Sub calDate_SelectionChanged(sender As Object, e As EventArgs) Handles calUKM2.SelectionChanged
        txtExamStart.Text = calUKM2.SelectedDate.ToString("yyyyMMdd")

    End Sub

End Class