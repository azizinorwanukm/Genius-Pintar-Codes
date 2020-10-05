Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Threading
Imports System.Resources
Imports System.Reflection

Partial Public Class ukm2_permata_end
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strLang As String

    Dim msFileName As String
    Dim msFilePath As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hyPDF.Text = ""
        Try
            If Not IsPostBack Then
                '--set DONE updated by batch file run END-OF-DAY
                'setUKM2DONE

                StudentProfile_load()
                StudentSchool_load()

                '--examstart and examend
                getDuration()

                lblExamYear.Text = Request.QueryString("examyear")
                UKM2Result.Text = oCommon.getAppsettings("UKM2Result")

                lblLevel.Text = ""    '--not implement in 2013
            End If
        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message
        End Try

    End Sub

    Private Function getLevel() As String
        Dim strStundentAge As String = Now.Year - CInt(lblDOB_Year.Text)

        Dim strLevel As String = ""
        strSQL = "SELECT Status FROM UKM1_Level WHERE ExamYear='" & lblExamYear.Text & "' AND StudentAge = '" & strStundentAge & "' AND " & getTotalscore() & " BETWEEN nLow AND nHigh"
        strLevel = oCommon.getFieldValue(strSQL)

        ''debug
        'Response.Write(strSQL)

        Return strLevel
    End Function

    Private Function getTotalscore() As String
        strSQL = "SELECT TotalScore FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length = 0 Then
            strRet = "0"
        End If

        Return strRet

    End Function

    Private Sub setUKM2DONE()
        ''--update once only
        strSQL = "SELECT Status FROM UKM2 WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        If oCommon.getFieldValue(strSQL) = "DONE" Then
            Exit Sub
        End If

        ''Sumary
        GenerateSum()
        GenerateIndex()

    End Sub

    Private Sub GenerateSum()
        Dim strMod1, strMod2, strMod3, strMod4, strMod5, strMod6, strMod7, strMod8, strMod9, strMod10, strMod11, strMod12, strMod13, strMod14, strMod15 As String

        ''-mod1
        strSQL = "SELECT SUM(isnull(Q001,0)+isnull(Q002,0)+isnull(Q003,0)+isnull(Q004,0)+isnull(Q005,0)+isnull(Q006,0)+isnull(Q007,0)+isnull(Q008,0)+isnull(Q009,0)+isnull(Q010,0)+isnull(Q011,0)+isnull(Q012,0)+isnull(Q013,0)+isnull(Q014,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod1 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q015,0)+isnull(Q016,0)+isnull(Q017,0)+isnull(Q018,0)+isnull(Q019,0)+isnull(Q020,0)+isnull(Q021,0)+isnull(Q022,0)+isnull(Q023,0)+isnull(Q024,0)+isnull(Q025,0)+isnull(Q026,0)+isnull(Q027,0)+isnull(Q028,0)+isnull(Q029,0)+isnull(Q030,0)+isnull(Q031,0)+isnull(Q032,0)+isnull(Q033,0)+isnull(Q034,0)+isnull(Q035,0)+isnull(Q036,0)+isnull(Q037,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod2 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q038,0)+isnull(Q039,0)+isnull(Q040,0)+isnull(Q041,0)+isnull(Q042,0)+isnull(Q043,0)+isnull(Q044,0)+isnull(Q045,0)+isnull(Q046,0)+isnull(Q047,0)+isnull(Q048,0)+isnull(Q049,0)+isnull(Q050,0)+isnull(Q051,0)+isnull(Q052,0)+isnull(Q053,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod3 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q054,0)+isnull(Q055,0)+isnull(Q056,0)+isnull(Q057,0)+isnull(Q058,0)+isnull(Q059,0)+isnull(Q060,0)+isnull(Q061,0)+isnull(Q062,0)+isnull(Q063,0)+isnull(Q064,0)+isnull(Q065,0)+isnull(Q066,0)+isnull(Q067,0)+isnull(Q068,0)+isnull(Q069,0)+isnull(Q070,0)+isnull(Q071,0)+isnull(Q072,0)+isnull(Q073,0)+isnull(Q074,0)+isnull(Q075,0)+isnull(Q076,0)+isnull(Q077,0)+isnull(Q078,0)+isnull(Q079,0)+isnull(Q080,0)+isnull(Q081,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod4 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q082,0)+isnull(Q083,0)+isnull(Q084,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod5 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q085,0)+isnull(Q086,0)+isnull(Q087,0)+isnull(Q088,0)+isnull(Q089,0)+isnull(Q090,0)+isnull(Q091,0)+isnull(Q092,0)+isnull(Q093,0)+isnull(Q094,0)+isnull(Q095,0)+isnull(Q096,0)+isnull(Q097,0)+isnull(Q098,0)+isnull(Q099,0)+isnull(Q100,0)+isnull(Q101,0)+isnull(Q102,0)+isnull(Q103,0)+isnull(Q104,0)+isnull(Q105,0)+isnull(Q106,0)+isnull(Q107,0)+isnull(Q108,0)+isnull(Q109,0)+isnull(Q110,0)+isnull(Q111,0)+isnull(Q112,0)+isnull(Q113,0)+isnull(Q114,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod6 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q115,0)+isnull(Q116,0)+isnull(Q117,0)+isnull(Q118,0)+isnull(Q119,0)+isnull(Q120,0)+isnull(Q121,0)+isnull(Q122,0)+isnull(Q123,0)+isnull(Q124,0)+isnull(Q125,0)+isnull(Q126,0)+isnull(Q127,0)+isnull(Q128,0)+isnull(Q129,0)+isnull(Q130,0)+isnull(Q131,0)+isnull(Q132,0)+isnull(Q133,0)+isnull(Q134,0)+isnull(Q135,0)+isnull(Q136,0)+isnull(Q137,0)+isnull(Q138,0)+isnull(Q139,0)+isnull(Q140,0)+isnull(Q141,0)+isnull(Q142,0)+isnull(Q143,0)+isnull(Q144,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod7 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q145,0)+isnull(Q146,0)+isnull(Q147,0)+isnull(Q148,0)+isnull(Q149,0)+isnull(Q150,0)+isnull(Q151,0)+isnull(Q152,0)+isnull(Q153,0)+isnull(Q154,0)+isnull(Q155,0)+isnull(Q156,0)+isnull(Q157,0)+isnull(Q158,0)+isnull(Q159,0)+isnull(Q160,0)+isnull(Q161,0)+isnull(Q162,0)+isnull(Q163,0)+isnull(Q164,0)+isnull(Q165,0)+isnull(Q166,0)+isnull(Q167,0)+isnull(Q168,0)+isnull(Q169,0)+isnull(Q170,0)+isnull(Q171,0)+isnull(Q172,0)+isnull(Q173,0)+isnull(Q174,0)+isnull(Q175,0)+isnull(Q176,0)+isnull(Q177,0)+isnull(Q178,0)+isnull(Q179,0)+isnull(Q180,0)+isnull(Q181,0)+isnull(Q182,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod8 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q183,0)+isnull(Q184,0)+isnull(Q185,0)+isnull(Q186,0)+isnull(Q187,0)+isnull(Q188,0)+isnull(Q189,0)+isnull(Q190,0)+isnull(Q191,0)+isnull(Q192,0)+isnull(Q193,0)+isnull(Q194,0)+isnull(Q195,0)+isnull(Q196,0)+isnull(Q197,0)+isnull(Q198,0)+isnull(Q199,0)+isnull(Q200,0)+isnull(Q201,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod9 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q202,0)+isnull(Q203,0)+isnull(Q204,0)+isnull(Q205,0)+isnull(Q206,0)+isnull(Q207,0)+isnull(Q208,0)+isnull(Q209,0)+isnull(Q210,0)+isnull(Q211,0)+isnull(Q212,0)+isnull(Q213,0)+isnull(Q214,0)+isnull(Q215,0)+isnull(Q216,0)+isnull(Q217,0)+isnull(Q218,0)+isnull(Q219,0)+isnull(Q220,0)+isnull(Q221,0)+isnull(Q222,0)+isnull(Q223,0)+isnull(Q224,0)+isnull(Q225,0)+isnull(Q226,0)+isnull(Q227,0)+isnull(Q228,0)+isnull(Q229,0)+isnull(Q230,0)+isnull(Q231,0)+isnull(Q232,0)+isnull(Q233,0)+isnull(Q234,0)+isnull(Q235,0)+isnull(Q236,0)+isnull(Q237,0)+isnull(Q238,0)+isnull(Q239,0)+isnull(Q240,0)+isnull(Q241,0)+isnull(Q242,0)+isnull(Q243,0)+isnull(Q244,0)+isnull(Q245,0)+isnull(Q246,0)+isnull(Q247,0)+isnull(Q248,0)+isnull(Q249,0)+isnull(Q250,0)+isnull(Q251,0)+isnull(Q252,0)+isnull(Q253,0)+isnull(Q254,0)+isnull(Q255,0)+isnull(Q256,0)+isnull(Q257,0)+isnull(Q258,0)+isnull(Q259,0)+isnull(Q260,0)+isnull(Q261,0)+isnull(Q262,0)+isnull(Q263,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod10 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q264,0)+isnull(Q265,0)+isnull(Q266,0)+isnull(Q267,0)+isnull(Q268,0)+isnull(Q269,0)+isnull(Q270,0)+isnull(Q271,0)+isnull(Q272,0)+isnull(Q273,0)+isnull(Q274,0)+isnull(Q275,0)+isnull(Q276,0)+isnull(Q277,0)+isnull(Q278,0)+isnull(Q279,0)+isnull(Q280,0)+isnull(Q281,0)+isnull(Q282,0)+isnull(Q283,0)+isnull(Q284,0)+isnull(Q285,0)+isnull(Q286,0)+isnull(Q287,0)+isnull(Q288,0)+isnull(Q289,0)+isnull(Q290,0)+isnull(Q291,0)+isnull(Q292,0)+isnull(Q293,0)+isnull(Q294,0)+isnull(Q295,0)+isnull(Q296,0)+isnull(Q297,0)+isnull(Q298,0)+isnull(Q299,0)+isnull(Q300,0)+isnull(Q301,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod11 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q302,0)+isnull(Q303,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod12 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q304,0)+isnull(Q305,0)+isnull(Q306,0)+isnull(Q307,0)+isnull(Q308,0)+isnull(Q309,0)+isnull(Q310,0)+isnull(Q311,0)+isnull(Q312,0)+isnull(Q313,0)+isnull(Q314,0)+isnull(Q315,0)+isnull(Q316,0)+isnull(Q317,0)+isnull(Q318,0)+isnull(Q319,0)+isnull(Q320,0)+isnull(Q321,0)+isnull(Q322,0)+isnull(Q323,0)+isnull(Q324,0)+isnull(Q325,0)+isnull(Q326,0)+isnull(Q327,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod13 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q328,0)+isnull(Q329,0)+isnull(Q330,0)+isnull(Q331,0)+isnull(Q332,0)+isnull(Q333,0)+isnull(Q334,0)+isnull(Q335,0)+isnull(Q336,0)+isnull(Q337,0)+isnull(Q338,0)+isnull(Q339,0)+isnull(Q340,0)+isnull(Q341,0)+isnull(Q342,0)+isnull(Q343,0)+isnull(Q344,0)+isnull(Q345,0)+isnull(Q346,0)+isnull(Q347,0)+isnull(Q348,0)+isnull(Q349,0)+isnull(Q350,0)+isnull(Q351,0)+isnull(Q352,0)+isnull(Q353,0)+isnull(Q354,0)+isnull(Q355,0)+isnull(Q356,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod14 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SUM(isnull(Q357,0)+isnull(Q358,0)+isnull(Q359,0)+isnull(Q360,0)+isnull(Q361,0)+isnull(Q362,0)+isnull(Q363,0)+isnull(Q364,0)+isnull(Q365,0)+isnull(Q366,0)+isnull(Q367,0)+isnull(Q368,0)+isnull(Q369,0)+isnull(Q370,0)+isnull(Q371,0)+isnull(Q372,0)+isnull(Q373,0)+isnull(Q374,0)+isnull(Q375,0)+isnull(Q376,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strMod15 = oCommon.getFieldValue(strSQL)

        '--DONE
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='DONE',Mod01=" & strMod1 & ",Mod02=" & strMod2 & ",Mod03=" & strMod3 & ",Mod04=" & strMod4 & ",Mod05=" & strMod5 & ",Mod06=" & strMod6 & ",Mod07=" & strMod7 & ",Mod08=" & strMod8 & ",Mod09=" & strMod9 & ",Mod10=" & strMod10 & ",Mod11=" & strMod11 & ",Mod12=" & strMod12 & ",Mod13=" & strMod13 & ",Mod14=" & strMod14 & ",Mod15=" & strMod15 & " WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub

    Private Sub GenerateIndex()
        ''--set fix US format for double
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim strVCI, strPRI, strWMI, strPSI As String
        Dim strTotalScore As String

        'VCI (Verbal Completion Index)	2+6+9+13+15  (verbal)
        strSQL = "SELECT SUM(isnull(Mod02,0)+isnull(Mod06,0)+isnull(Mod09,0)+isnull(Mod13,0)+isnull(Mod15,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strVCI = oCommon.getFieldValue(strSQL)

        'PRI (Perseptual Reasoning Index)	1+4+8+12 (science+math)
        strSQL = "SELECT SUM(isnull(Mod01,0)+isnull(Mod04,0)+isnull(Mod08,0)+isnull(Mod12,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strPRI = oCommon.getFieldValue(strSQL)

        'WMI(Working Memory Index)	3+7+14 (sokongan VCI/PRI)
        strSQL = "SELECT SUM(isnull(Mod03,0)+isnull(Mod07,0)+isnull(Mod14,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strWMI = oCommon.getFieldValue(strSQL)

        'PSI(Processing Speed Index)	5+10+11 (sokongan VCI/PRI)
        strSQL = "SELECT SUM(isnull(Mod05,0)+isnull(Mod10,0)+isnull(Mod11,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strPSI = oCommon.getFieldValue(strSQL)

        ''OK
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET VCI=" & strVCI & ",PRI=" & strPRI & ",WMI=" & strWMI & ",PSI=" & strPSI & " WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        'get strTotalScore. NOK
        strSQL = "SELECT SUM(isnull(VCI,0)+isnull(PRI,0)+isnull(WMI,0)+isnull(PSI,0)) as SumA FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strTotalScore = oCommon.getFieldValue(strSQL)

        Dim dblTotalPercentage As Double
        dblTotalPercentage = (CInt(strTotalScore) / 692) * 100
        Dim strTotalPercentage As String = oCommon.DoConvertD(dblTotalPercentage, 4)

        'update TotalPercentage
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET FullMark=692,TotalPercentage=" & strTotalPercentage & ",TotalScore=" & strTotalScore & " WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            ''--debug
            'Response.Write("TotalPercentage:" & strRet)
        End If

    End Sub


    Private Sub StudentProfile_load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT MYKAD,StudentFullname,DOB_Year FROM StudentProfile WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentFullname")) Then
                    StudentFullname.Text = MyTable.Rows(nRows).Item("StudentFullname").ToString
                Else
                    StudentFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("MYKAD")) Then
                    MYKAD.Text = MyTable.Rows(nRows).Item("MYKAD").ToString
                Else
                    MYKAD.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("DOB_Year")) Then
                    lblDOB_Year.Text = MyTable.Rows(nRows).Item("DOB_Year").ToString
                Else
                    lblDOB_Year.Text = ""
                End If
            End If

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub StudentSchool_load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''get schoolID
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' ORDER BY StudentSchoolID DESC"
        strSchoolID = oCommon.getFieldValue(strSQL)

        '--get school profile
        strSQL = "SELECT * FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCode")) Then
                    lblSchoolCode.Text = MyTable.Rows(nRows).Item("SchoolCode").ToString
                Else
                    lblSchoolCode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolName")) Then
                    SchoolName.Text = MyTable.Rows(nRows).Item("SchoolName").ToString
                Else
                    SchoolName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCity")) Then
                    lblSchoolcity.Text = MyTable.Rows(nRows).Item("SchoolCity").ToString
                Else
                    lblSchoolcity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    SchoolState.Text = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    SchoolState.Text = ""
                End If
            End If

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub UKM2_Load()
        ''--load UKM2 result
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''get schoolID
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' ORDER BY StudentSchoolID DESC"
        strSchoolID = oCommon.getFieldValue(strSQL)

        '--get school profile
        strSQL = "SELECT * FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    SchoolState.Text = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    SchoolState.Text = ""
                End If
            End If

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub getDuration()
        '--init
        lblExamStart.Text = ""
        lblExamEnd.Text = ""

        ''exam end and exam year
        strSQL = "SELECT ExamStart FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        Dim strExamStart = oCommon.getFieldValue(strSQL)
        If Len(strExamStart) > 0 Then
            lblExamStart.Text = Mid(strExamStart, 1, 4) & "/" & Mid(strExamStart, 5, 2) & "/" & Mid(strExamStart, 7, 2) & " " & Mid(strExamStart, 10, 8)
            lblExamStartDisp.Text = Mid(strExamStart, 7, 2) & "/" & Mid(strExamStart, 5, 2) & "/" & Mid(strExamStart, 1, 4) & " " & Mid(strExamStart, 10, 8)
        End If

        strSQL = "SELECT ExamEnd FROM UKM2 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        Dim strExamEnd As String = oCommon.getFieldValue(strSQL)
        If Len(strExamEnd) > 0 Then
            lblExamEnd.Text = Mid(strExamEnd, 1, 4) & "/" & Mid(strExamEnd, 5, 2) & "/" & Mid(strExamEnd, 7, 2) & " " & Mid(strExamEnd, 10, 8)
            lblExamEndDisp.Text = Mid(strExamEnd, 7, 2) & "/" & Mid(strExamEnd, 5, 2) & "/" & Mid(strExamEnd, 1, 4) & " " & Mid(strExamEnd, 10, 8)
        End If

    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = CType(Session.Item("permata_studentid"), String) & "-UKM2-" & lblExamYear.Text & ".pdf"
            msFilePath = Server.MapPath(".") & "\cert_pdf\" & msFileName
            hyPDF.NavigateUrl = "cert_pdf/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("Jamain Johari (ARAKEN SDN BHD)")
            myDocument.AddSubject("Menamatkan Ujian Dalam Talian UKM2 " & Request.QueryString("examyear"))

            'Step 4: Now add some contents to the document

            'add image
            Dim imageFile As String = Server.MapPath(".") & "\pic\sijil-top-pdf.jpg"
            Dim jpeg As Image = Image.GetInstance(imageFile)
            jpeg.Alignment = Image.LEFT_ALIGN  'left
            myDocument.Add(jpeg)

            ''--create horizontalline
            'Dim g As New Graphic
            'g.SetHorizontalLine(5.0F, 100.0F)
            'myDocument.Add(g)

            'To end the section with a dotted line
            Dim sepLINE As New iTextSharp.text.pdf.draw.LineSeparator
            sepLINE.LineWidth = 1
            Dim chnkLINE As New iTextSharp.text.Chunk(sepLINE)
            myDocument.Add(chnkLINE)

            Dim myPara As New Paragraph("Sijil Penyertaan", FontFactory.GetFont("Arial", 32, Font.BOLD))
            myPara.Alignment = 1
            myDocument.Add(myPara)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara2 As New Paragraph("Dengan ini disahkan bahawa", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myPara2.Alignment = 1
            myDocument.Add(myPara2)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara3 As New Paragraph(StudentFullname.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myPara3.Alignment = 1
            myDocument.Add(myPara3)

            Dim myPara4 As New Paragraph(MYKAD.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myPara4.Alignment = 1
            myDocument.Add(myPara4)

            Dim myPara5 As New Paragraph(lblSchoolCode.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myPara5.Alignment = 1
            myDocument.Add(myPara5)

            Dim myPara6 As New Paragraph(SchoolName.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myPara6.Alignment = 1
            myDocument.Add(myPara6)

            Dim myPara7 As New Paragraph(lblSchoolcity.Text & ", " & SchoolState.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myPara7.Alignment = 1
            myDocument.Add(myPara7)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara8 As New Paragraph("telah menamatkan", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myPara8.Alignment = 1
            myDocument.Add(myPara8)

            Dim myPara9 As New Paragraph("Ujian Pencarian Bakat UKM2 " & lblExamYear.Text, FontFactory.GetFont("Arial", 18, Font.BOLD))
            myPara9.Alignment = 1
            myDocument.Add(myPara9)

            Dim myPara10 As New Paragraph("Peringkat Kebangsaan", FontFactory.GetFont("Arial", 18, Font.BOLD))
            myPara10.Alignment = 1
            myDocument.Add(myPara10)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara11 As New Paragraph("Tarikh dan Masa MULA: " & lblExamStartDisp.Text, FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myPara11.Alignment = 1
            myDocument.Add(myPara11)

            Dim myPara12 As New Paragraph("Tarikh dan Masa TAMAT: " & lblExamEndDisp.Text, FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myPara12.Alignment = 1
            myDocument.Add(myPara12)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara13 As New Paragraph("Tahniah dan Terima kasih", FontFactory.GetFont("Arial", 18, Font.BOLD))
            myPara13.Alignment = 1
            myDocument.Add(myPara13)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara14 As New Paragraph("Pusat PERMATApintar Negara", FontFactory.GetFont("Arial", 14, Font.BOLD))
            myPara14.Alignment = 1
            myDocument.Add(myPara14)

            Dim myPara15 As New Paragraph("Universiti Kebangsaan Malaysia", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myPara15.Alignment = 1
            myDocument.Add(myPara15)

            Dim myPara16 As New Paragraph("43600 Bangi, Selangor, Malaysia", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myPara16.Alignment = 1
            myDocument.Add(myPara16)

            Dim myPara17 As New Paragraph("Tel: +603-8921 7503  Fax: +603-8921 7525", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myPara17.Alignment = 1
            myDocument.Add(myPara17)

            Dim myPara18 As New Paragraph("E-Mail: permatapintar@ukm.edu.my", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myPara18.Alignment = 1
            myDocument.Add(myPara18)

            myDocument.Add(chnkLINE)
            Dim myPara19 As New Paragraph("(Sijil ini janaan komputer dan tidak perlu tandatangan)", FontFactory.GetFont("Arial", 10, Font.ITALIC))
            myPara19.Alignment = 1
            myDocument.Add(myPara19)

            'Response.Write("Succesfully create the PDF")
            lblMsg.Text = "PDF fail siap dijana."
            hyPDF.Text = "Klik disini untuk buka."
        Catch ex As DocumentException
            myDocument.Close()
            '--display on screen
            lblMsg.Text = "System Error:" & ex.Message
        Catch ioe As IOException
            '--display on screen
            lblMsg.Text = "System Error:" & ioe.Message

        Finally
            'Step 5: Remember to close the documnet
            myDocument.Close()

        End Try

    End Sub

    Private Sub btnCreate_Command(sender As Object, e As CommandEventArgs) Handles btnCreate.Command

    End Sub
End Class