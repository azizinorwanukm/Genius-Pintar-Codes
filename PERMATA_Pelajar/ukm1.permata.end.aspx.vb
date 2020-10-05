Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Public Class ukm1_permata_end
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
            '--ujian ukm1 telah ditamatkan bagi sessi tahun semasa
            'If isExamEnd() = True Then
            '    Response.Redirect("default.end.aspx")
            'End If

            If Not IsPostBack Then
                ''--set status to DONE
                'setUKM1DONE()

                StudentProfile_load()
                StudentSchool_load()

                '--get examstart, examend and duration
                getDuration()

                lblExamYear.Text = Request.QueryString("examyear")
                UKM1Result.Text = oCommon.getAppsettings("UKM1Result")

                '--check kelayakan
                If isLayakSijil() = True Then
                    pnlLayak.Visible = True
                    pnlTidakLayak.Visible = False
                Else
                    pnlTidakLayak.Visible = True
                    pnlLayak.Visible = False
                End If

            End If
        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message
        End Try

    End Sub

    Private Function isLayakSijil() As Boolean
        '--LAYAK UKM2. Allow print sijil. Dr Siti 20150609
        strSQL = "SELECT StudentID FROM UKM2 WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        If oCommon.isExist(strSQL) = True Then
            Return True
        End If

        '--limit print sijil 15 mins
        Dim strMinDuration As String = oCommon.getAppsettings("MinDuration")
        If CInt(lblTotalMin.Text) < CInt(strMinDuration) Then
            Return False
        End If

        '--limit TotalPercentage <20%
        Dim strMinMark As String = oCommon.getAppsettings("MinMark")
        strSQL = "SELECT TotalPercentage FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        Dim strTotalPercentage As String = oCommon.getFieldValueInt(strSQL)
        If CInt(strTotalPercentage) < CInt(strMinMark) Then
            Return False
        End If

        Return True

    End Function

    Private Function isExamEnd() As Boolean
        ''--exam END
        Dim strUKM1END As String = ConfigurationManager.AppSettings("UKM1END")
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        If CInt(strToday) > CInt(strUKM1END) Then
            Return True
        End If

        Return False
    End Function

    Private Sub getDuration()
        Dim nDuration As Integer = 0  ''duration in seconds
        Dim tsDuration As TimeSpan
        Dim strTotalSeconds As String = "0"
        Dim strDurationString As String = "0"

        '--init
        lblExamStart.Text = ""
        lblExamEnd.Text = ""

        ''exam end and exam year
        strSQL = "SELECT ExamStart FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        displayDebug(strSQL)

        Dim strExamStart = oCommon.getFieldValue(strSQL)
        lblExamStartDB.Text = strExamStart
        If Len(strExamStart) > 0 Then
            lblExamStart.Text = Mid(strExamStart, 1, 4) & "/" & Mid(strExamStart, 5, 2) & "/" & Mid(strExamStart, 7, 2) & " " & Mid(strExamStart, 10, 8)
            lblExamStartDisp.Text = Mid(strExamStart, 7, 2) & "/" & Mid(strExamStart, 5, 2) & "/" & Mid(strExamStart, 1, 4) & " " & Mid(strExamStart, 10, 8)
        End If

        strSQL = "SELECT ExamEnd FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        Dim strExamEnd As String = oCommon.getFieldValue(strSQL)
        If Len(strExamEnd) > 0 Then
            lblExamEnd.Text = Mid(strExamEnd, 1, 4) & "/" & Mid(strExamEnd, 5, 2) & "/" & Mid(strExamEnd, 7, 2) & " " & Mid(strExamEnd, 10, 8)
            lblExamEndDisp.Text = Mid(strExamEnd, 7, 2) & "/" & Mid(strExamEnd, 5, 2) & "/" & Mid(strExamEnd, 1, 4) & " " & Mid(strExamEnd, 10, 8)
        End If

        If Len(lblExamStart.Text) > 0 And Len(lblExamEnd.Text) > 0 Then
            tsDuration = CDate(lblExamEnd.Text) - CDate(lblExamStart.Text)
            lblDuration.Text = "Days: " & tsDuration.Days & " Hours: " & tsDuration.Hours & " Min: " & tsDuration.Minutes & " Sec:" & tsDuration.Seconds
            lblTotalMin.Text = tsDuration.TotalMinutes
        Else
            strDurationString = "0"
            strTotalSeconds = "0"
        End If

    End Sub

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

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
        strSQL = "SELECT TotalScore FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length = 0 Then
            strRet = "0"
        End If

        Return strRet

    End Function

    Private Sub setUKM1DONE()
        ''--update once only
        strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        If oCommon.getFieldValue(strSQL) = "DONE" Then
            Exit Sub
        End If

        ''update UKM1
        strSQL = "UPDATE UKM1 WITH (ROWLOCK) SET Status='DONE',LastPage='ukm1.permata.end.aspx?',ExamEnd='" & oCommon.getNow & "' WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = strRet
        End If

        ''--SUM here ModA. TOTAL=60. Soalan 1-60
        strSQL = "UPDATE UKM1 WITH (ROWLOCK) SET ModA=(SELECT SUM(isnull(Q001,0)+isnull(Q002,0)+isnull(Q003,0)+isnull(Q004,0)+isnull(Q005,0)+isnull(Q006,0)+isnull(Q007,0)+isnull(Q008,0)+isnull(Q009,0)+isnull(Q010,0)+isnull(Q011,0)+isnull(Q012,0)+isnull(Q013,0)+isnull(Q014,0)+isnull(Q015,0)+isnull(Q016,0)+isnull(Q017,0)+isnull(Q018,0)+isnull(Q019,0)+isnull(Q020,0)+isnull(Q021,0)+isnull(Q022,0)+isnull(Q023,0)+isnull(Q024,0)+isnull(Q025,0)+isnull(Q026,0)+isnull(Q027,0)+isnull(Q028,0)+isnull(Q029,0)+isnull(Q030,0)+isnull(Q031,0)+isnull(Q032,0)+isnull(Q033,0)+isnull(Q034,0)+isnull(Q035,0)+isnull(Q036,0)+isnull(Q037,0)+isnull(Q038,0)+isnull(Q039,0)+isnull(Q040,0)+isnull(Q041,0)+isnull(Q042,0)+isnull(Q043,0)+isnull(Q044,0)+isnull(Q045,0)+isnull(Q046,0)+isnull(Q047,0)+isnull(Q048,0)+isnull(Q049,0)+isnull(Q050,0)+isnull(Q051,0)+isnull(Q052,0)+isnull(Q053,0)+isnull(Q054,0)+isnull(Q055,0)+isnull(Q056,0)+isnull(Q057,0)+isnull(Q058,0)+isnull(Q059,0)+isnull(Q060,0)) as ModA FROM UKM1 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "') WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        oCommon.ExecuteSQL(strSQL)
        ''-debug 
        'Response.Write("ModA:" & strSQL)

        ''--SUM here ModB. TOTAL=30. Soalan 61-90
        strSQL = "UPDATE UKM1 WITH (ROWLOCK) SET ModB=(SELECT SUM(isnull(Q061,0)+isnull(Q062,0)+isnull(Q063,0)+isnull(Q064,0)+isnull(Q065,0)+isnull(Q066,0)+isnull(Q067,0)+isnull(Q068,0)+isnull(Q069,0)+isnull(Q070,0)+isnull(Q071,0)+isnull(Q072,0)+isnull(Q073,0)+isnull(Q074,0)+isnull(Q075,0)+isnull(Q076,0)+isnull(Q077,0)+isnull(Q078,0)+isnull(Q079,0)+isnull(Q080,0)+isnull(Q081,0)+isnull(Q082,0)+isnull(Q083,0)+isnull(Q084,0)+isnull(Q085,0)+isnull(Q086,0)+isnull(Q087,0)+isnull(Q088,0)+isnull(Q089,0)+isnull(Q090,0)) as ModB FROM UKM1 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "') WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        oCommon.ExecuteSQL(strSQL)
        'Response.Write("ModB:" & strSQL)

        ''--SUM here ModC. TOTAL=30. Soalan 105-134
        strSQL = "UPDATE UKM1 WITH (ROWLOCK) SET ModC=(SELECT SUM(isnull(Q105,0)+isnull(Q106,0)+isnull(Q107,0)+isnull(Q108,0)+isnull(Q109,0)+isnull(Q110,0)+isnull(Q111,0)+isnull(Q112,0)+isnull(Q113,0)+isnull(Q114,0)+isnull(Q115,0)+isnull(Q116,0)+isnull(Q117,0)+isnull(Q118,0)+isnull(Q119,0)+isnull(Q120,0)+isnull(Q121,0)+isnull(Q122,0)+isnull(Q123,0)+isnull(Q124,0)+isnull(Q125,0)+isnull(Q126,0)+isnull(Q127,0)+isnull(Q128,0)+isnull(Q129,0)+isnull(Q130,0)+isnull(Q131,0)+isnull(Q132,0)+isnull(Q133,0)+isnull(Q134,0)) as ModC FROM UKM1 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "') WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        oCommon.ExecuteSQL(strSQL)
        'Response.Write("ModC:" & strSQL)

        ''--SUM TotalScore
        strSQL = "UPDATE UKM1 WITH (ROWLOCK) SET TotalScore=(SELECT sum(ModA+ModB+ModC) as TotalScore FROM UKM1 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "') WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        oCommon.ExecuteSQL(strSQL)
        'Response.Write("TotalScore:" & strSQL)

        ''--SUM TotalPercentage
        strSQL = "UPDATE UKM1 WITH (ROWLOCK) SET TotalPercentage=(SELECT CONVERT(DECIMAL(5,2),100.0 * (SELECT TotalScore FROM UKM1 WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "') / " & ConfigurationManager.AppSettings("FullMark") & ") FROM UKM1  WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "') WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        oCommon.ExecuteSQL(strSQL)
        'Response.Write("TotalPercentage:" & strSQL)

        '--insert into security_login_trail
        oCommon.TransactionLog("ukm1_setDONE", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress, CType(Session.Item("permata_mykad"), String))
    End Sub

    Private Sub StudentProfile_load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT MYKAD,StudentFullname,DOB_Year FROM StudentProfile WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "'"
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
            lblMsg.Text = "System Error: " & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub StudentSchool_load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''get schoolID
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' ORDER BY StudentSchoolID DESC"
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
            lblMsg.Text = "System Error. Contact system admin. "

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub UKM1_Load()
        ''--load UKM1 result
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''get schoolID
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' ORDER BY StudentSchoolID DESC"
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
            lblMsg.Text = "System Error. Contact system admin. "

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = CType(Session.Item("permata_studentid"), String) & "-" & lblExamYear.Text & ".pdf"
            msFilePath = Server.MapPath(".") & "\cert_pdf\" & msFileName
            hyPDF.NavigateUrl = "cert_pdf/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("Jamain Johari (ARAKEN SDN BHD)")
            myDocument.AddSubject("Menamatkan Ujian Dalam Talian UKM1 " & Request.QueryString("examyear"))

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

            Dim myPara9 As New Paragraph("Ujian Pencarian Bakat UKM1 " & lblExamYear.Text, FontFactory.GetFont("Arial", 18, Font.BOLD))
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

            Dim myPara18 As New Paragraph("E-Mail: permatapintar@ukm.my", FontFactory.GetFont("Arial", 14, Font.NORMAL))
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

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":btncreate_click.ex:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        Catch ioe As IOException
            '--display on screen
            lblMsg.Text = "System Error:" & ioe.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":btncreate_click.ioe:" & Request.UserHostAddress & ":" & ioe.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        Finally
            'Step 5: Remember to close the documnet
            myDocument.Close()

        End Try

    End Sub
End Class