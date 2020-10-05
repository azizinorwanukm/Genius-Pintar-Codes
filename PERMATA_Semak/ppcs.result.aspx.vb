Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ppcs_result
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnTerima.Attributes.Add("onclick", "return confirm('Pasti ingin MENERIMA tawaran ini?');")
        btnTolak.Attributes.Add("onclick", "return confirm('Pasti ingin MENOLAK tawaran ini? Masukkan sebab kenapa tawaran ini ditolak.');")

        Try
            If Not IsPostBack Then
                lblPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")
                lblTarikhTutup.Text = oCommon.getAppsettings("PPCSTarikhTutup")
                '--lock value
                lblMYKAD.Text = Request.QueryString("mykad")

                Screen_load()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Screen_load()
        '--check if allow publish or not
        If oCommon.getAppsettings("PPCSResult") = "N" Then
            lblMsg.Text = "Keputusan PPCS belum dimuktamadkan lagi atau telah ditutup!"
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If


        Dim strQuery As String = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & lblMYKAD.Text & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strQuery)

        strSQL = "SELECT TOP 1 PPCSID FROM PPCS WHERE StudentID='" & strStudentID & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
        lblPPCSID.Text = oCommon.getFieldValue(strSQL)

        '--StudentID
        'strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "'"
        lblStudentID.Text = strStudentID

        '--StudentFullname
        strSQL = "SELECT TOP 1 StudentFullname FROM StudentProfile WHERE MYKAD='" & lblMYKAD.Text & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        '--if student change URL
        strSQL = "SELECT StudentID FROM PPCS WHERE StudentID='" & lblStudentID.Text & "' AND PPCSID='" & lblPPCSID.Text & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Rekod anda tidak ditemui!"
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        '--PPCSDate
        strSQL = "SELECT TOP 1 PPCSDate FROM PPCS WHERE StudentID='" & lblStudentID.Text & "' AND PPCSID='" & lblPPCSID.Text & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
        lblPPCSDate.Text = oCommon.getFieldValue(strSQL)

        '--PPCSStatus
        strSQL = "SELECT TOP 1 PPCSStatus FROM PPCS WHERE StudentID='" & lblStudentID.Text & "' AND PPCSID='" & lblPPCSID.Text & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
        lblPPCSStatus.Text = oCommon.getFieldValue(strSQL)

        btnPrint.Visible = False
        pnlDisplay.Visible = False
        pnlExpired.Visible = False
        pnlDisplay21.Visible = False

        '--jika ada cubaan menukar URL
        If lblPPCSDate.Text.Length > 0 Then
            divMsg.Attributes("class") = "info"
            Select Case lblPPCSStatus.Text
                Case "LAYAK"
                    lblMsg.Text = "TAHNIAH! Anda LAYAK ke Sessi PPCS " & lblPPCSDate.Text
                    btnPrint.Visible = True
                    pnlDisplay.Visible = True
                    pnlExpired.Visible = True
                    pnlDisplay21.Visible = True
                Case "SIMPANAN"
                    lblMsg.Text = "TAHNIAH! Anda dicalonkan sebagai calon SIMPANAN ke Sessi PPCS " & lblPPCSDate.Text
                Case Else
                    lblMsg.Text = "Status anda belum diputuskan lagi oleh Pihak Pengurusan PPCS."
            End Select
        Else
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "MAAF! Rekod anda tidak terdapat di dalam senarai PPCS. Hubungi Pihak Pengurusan PERMATApintar."
        End If
        lblMsgTop.Text = lblMsg.Text

        '--getAppsettings
        lblPPCSTarikhProgram.Text = oCommon.getAppsettings("PPCSTarikhProgram")
        lblPPCSMasa.Text = oCommon.getAppsettings("PPCSMasa")
        lblPPCSTempat.Text = oCommon.getAppsettings("PPCSTempat")
        lblPPCSYuran.Text = oCommon.getAppsettings("PPCSYuran")

        '--PPCS
        PPCS_view()

        '--SchoolProfile
        SchoolProfile_view()

    End Sub

    Private Sub PPCS_view()


        Dim strQuery As String = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & lblMYKAD.Text & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strQuery)

        strSQL = "SELECT TOP 1 PPCSID FROM PPCS WHERE StudentID='" & strStudentID & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
        lblPPCSID.Text = oCommon.getFieldValue(strSQL)


        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT PPCS_Course.CourseCode,PPCS_Course.CourseNameBM,PPCS.StatusDate FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        strWhere = " WHERE PPCSID='" & lblPPCSID.Text & "'"
        strSQL = tmpSQL & strWhere & strOrder

        '--debug
        'lblSQLDebug.Text = strSQL

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseCode")) Then
                    lblCourseCode.Text = ds.Tables(0).Rows(0).Item("CourseCode")
                Else
                    lblCourseCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseNameBM")) Then
                    lblCourseNameBM.Text = ds.Tables(0).Rows(0).Item("CourseNameBM")
                Else
                    lblCourseNameBM.Text = ""
                End If

                '--StatusDate
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StatusDate")) Then
                    lblStatusDate.Text = ds.Tables(0).Rows(0).Item("StatusDate")
                Else
                    lblStatusDate.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub SchoolProfile_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT * FROM StudentSchool a,SchoolProfile b"
        strWhere = " WHERE a.StudentID='" & oCommon.FixSingleQuotes(lblStudentID.Text) & "' AND a.SchoolID=b.SchoolID"
        strSQL = tmpSQL + strWhere
        '--debug
        'Response.Write(strSQL)

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolName")) Then
                    lblSchoolName.Text = MyTable.Rows(nRows).Item("SchoolName").ToString
                Else
                    lblSchoolName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolAddress")) Then
                    lblSchoolAddress.Text = MyTable.Rows(nRows).Item("SchoolAddress").ToString
                Else
                    lblSchoolAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolPostcode")) Then
                    lblSchoolPostcode.Text = MyTable.Rows(nRows).Item("SchoolPostcode").ToString
                Else
                    lblSchoolPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCity")) Then
                    lblSchoolCity.Text = MyTable.Rows(nRows).Item("SchoolCity").ToString
                Else
                    lblSchoolCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    lblSchoolState.Text = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    lblSchoolState.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Clear_screen()
        lblStudentFullname.Text = ""
        lblStudentID.Text = ""
        lblCourseCode.Text = ""
        lblCourseNameBM.Text = ""

        lblSchoolName.Text = ""
        lblSchoolAddress.Text = ""
        lblSchoolPostcode.Text = ""
        lblSchoolCity.Text = ""
        lblSchoolState.Text = ""

        pnlDisplay.Visible = False
        hyPDF.Visible = False

        lblMsgTop.Text = ""
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim msFileName As String = ""
        Dim msFilePath As String = ""

        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = "surat-tawaran-" & lblMYKAD.Text & ".pdf"
            msFilePath = Server.MapPath(".") & "\cert_pdf\" & msFileName
            hyPDF.NavigateUrl = "cert_pdf/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("Jamain Johari (ARAKEN SDN BHD)")
            myDocument.AddSubject("TAHNIAH! Anda LAYAK ke PPCS bagi sessi " & lblPPCSDate.Text)

            'Step 4: Now add some contents to the document

            'add image
            Dim strTop As String = Server.MapPath(".") & "\pic\sijil-top-pdf.jpg"
            Dim imgTop As Image = Image.GetInstance(strTop)
            imgTop.Alignment = Image.LEFT_ALIGN
            myDocument.Add(imgTop)

            ''--create horizontalline
            'Dim g As New Graphic
            'g.SetHorizontalLine(5.0F, 100.0F)
            'myDocument.Add(g)

            'To end the section with a dotted line
            Dim sepLINE As New iTextSharp.text.pdf.draw.LineSeparator
            sepLINE.LineWidth = 1
            Dim chnkLINE As New iTextSharp.text.Chunk(sepLINE)

            Dim myPara As New Paragraph("Rujukan : UKM1.41/344/2", FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara)

            Dim myPara2 As New Paragraph("Tarikh :" & oCommon.FormatDateDMY(Now), FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara2)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara8 As New Paragraph("Tuan/ Puan,", FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara8.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara8)

            Dim myPara9 As New Paragraph("TAWARAN MENGIKUTI PROGRAM PERKHEMAHAN CUTI SEKOLAH (PPCS) SESI DISEMBER 2015.", FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara9.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara9)

            myDocument.Add(Chunk.NEWLINE)
            Dim strBody As String = "Sukacita dimaklumkan bahawa anak tuan/ puan, berjaya untuk menyertai PPCS yang merupakan anjuran UKM dengan kerjasama Johns Hopkins University, Center for Talented Youth (JHU – CTY). Perincian pendaftaran adalah seperti berikut:- "
            Dim myPara10 As New Paragraph(strBody, FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara10.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara10)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara11a As New Paragraph("Nama                      : " & lblStudentFullname.Text, FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara11a.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11a)

            Dim myPara11b As New Paragraph("Kad Pengenalan    : " & lblMYKAD.Text, FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara11b.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11b)

            Dim myPara11c As New Paragraph("Kursus                    : " & lblCourseCode.Text & " > " & lblCourseNameBM.Text, FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara11c.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11c)

            Dim myPara12 As New Paragraph("Tarikh Program      : 29 Nov 2015 – 18 Dis 2015", FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara12.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara12)

            Dim myPara13 As New Paragraph("Lokasi Program     : Pusat PERMATApintar™ Negara, Universiti Kebangsaan Malaysia", FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara13.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara13)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara15 As New Paragraph("Pendaftaran", FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara15.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara15)

            Dim myPara16 As New Paragraph("Tarikh & Masa         : 29 Nov 2015 (Ahad) & 9.00 pagi – 12.00 tengahari (murid Sabah dan Sarawak / menaiki bas mengikut zon", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara16.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara16)

            Dim myPara31 As New Paragraph("                                 perlu daftar pada 28 Nov 2015)", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara31.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara31)

            Dim myPara17 As New Paragraph("                                Nota", FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara17.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara17)

            Dim myPara18 As New Paragraph("                                Kenderaan disediakan kepada murid yang memerlukan sahaja ", FontFactory.GetFont("Arial", 9, Font.BOLD))
            myPara18.Alignment = Element.ALIGN_JUSTIFIED
            myDocument.Add(myPara18)

            Dim myPara19 As New Paragraph("Tempat                    : Dewan Serbaguna, Pusat PERMATApintar™ Negara, UKM", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara19.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara19)

            Dim strPPCSYuran As String = oCommon.getAppsettings("PPCSYuran")
            Dim myPara20 As New Paragraph("Yuran                       :         RM 255.00 (Baharu)	                        RM 205.00 (Lama)", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara20.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara20)
            ''''
            Dim myTable As New PdfPTable(6)
            myTable.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTable.HorizontalAlignment = 2 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidth() As Integer = {10, 12, 10, 12, 10, 12}
            myTable.SetWidths(intTblWidth)

            Dim Cell1Hdr As New PdfPCell(New Phrase("Pendaftaran ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell1Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell1Hdr)

            Dim Cell2Hdr As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell2Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell2Hdr)

            Dim Cell3Hdr As New PdfPCell(New Phrase("180.00", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell3Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell3Hdr)

            Dim Cell4Hdr As New PdfPCell(New Phrase("Pendaftaran", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell4Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell4Hdr)

            Dim Cell5Hdr As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell5Hdr)

            Dim Cell5_1Hdr As New PdfPCell(New Phrase("180.00", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5_1Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell5_1Hdr)
            myDocument.Add(myTable)

            Dim myTable1 As New PdfPTable(6)
            myTable1.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTable1.HorizontalAlignment = 2 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidth1() As Integer = {10, 12, 10, 12, 10, 12}
            myTable1.SetWidths(intTblWidth1)

            Dim Cell1Hdr1 As New PdfPCell(New Phrase("Dobi ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell1Hdr1.Border = Rectangle.NO_BORDER
            myTable1.AddCell(Cell1Hdr1)

            Dim Cell2Hdr1 As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell2Hdr1.Border = Rectangle.NO_BORDER
            myTable1.AddCell(Cell2Hdr1)

            Dim Cell3Hdr1 As New PdfPCell(New Phrase("20.00", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell3Hdr1.Border = Rectangle.NO_BORDER
            myTable1.AddCell(Cell3Hdr1)

            Dim Cell4Hdr1 As New PdfPCell(New Phrase("Dobi", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell4Hdr1.Border = Rectangle.NO_BORDER
            myTable1.AddCell(Cell4Hdr1)

            Dim Cell5Hdr1 As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5Hdr1.Border = Rectangle.NO_BORDER
            myTable1.AddCell(Cell5Hdr1)

            Dim Cell5_1Hdr1 As New PdfPCell(New Phrase("20.00", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5_1Hdr1.Border = Rectangle.NO_BORDER
            myTable1.AddCell(Cell5_1Hdr1)
            myDocument.Add(myTable1)

            Dim myTable2 As New PdfPTable(6)
            myTable2.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTable2.HorizontalAlignment = 2 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidth2() As Integer = {10, 12, 10, 12, 10, 12}
            myTable2.SetWidths(intTblWidth2)

            Dim Cell1Hdr2 As New PdfPCell(New Phrase("Kunci Bilik ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell1Hdr2.Border = Rectangle.NO_BORDER
            myTable2.AddCell(Cell1Hdr2)

            Dim Cell2Hdr2 As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell2Hdr2.Border = Rectangle.NO_BORDER
            myTable2.AddCell(Cell2Hdr2)

            Dim Cell3Hdr2 As New PdfPCell(New Phrase("5.00", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell3Hdr2.Border = Rectangle.NO_BORDER
            myTable2.AddCell(Cell3Hdr2)

            Dim Cell4Hdr2 As New PdfPCell(New Phrase("Kunci Bilik", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell4Hdr2.Border = Rectangle.NO_BORDER
            myTable2.AddCell(Cell4Hdr2)

            Dim Cell5Hdr2 As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5Hdr2.Border = Rectangle.NO_BORDER
            myTable2.AddCell(Cell5Hdr2)

            Dim Cell5_1Hdr2 As New PdfPCell(New Phrase("5.00", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5_1Hdr2.Border = Rectangle.NO_BORDER
            myTable2.AddCell(Cell5_1Hdr2)
            myDocument.Add(myTable2)

            Dim myTable3 As New PdfPTable(6)
            myTable3.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTable2.HorizontalAlignment = 2 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidth3() As Integer = {10, 12, 10, 12, 10, 12}
            myTable3.SetWidths(intTblWidth3)

            Dim Cell1Hdr3 As New PdfPCell(New Phrase("Alumni ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell1Hdr3.Border = Rectangle.NO_BORDER
            myTable3.AddCell(Cell1Hdr3)

            Dim Cell2Hdr3 As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell2Hdr3.Border = Rectangle.NO_BORDER
            myTable3.AddCell(Cell2Hdr3)

            Dim Cell3Hdr3 As New PdfPCell(New Phrase("50.00", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell3Hdr3.Border = Rectangle.NO_BORDER
            myTable3.AddCell(Cell3Hdr3)

            Dim Cell4Hdr3 As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell4Hdr3.Border = Rectangle.NO_BORDER
            myTable3.AddCell(Cell4Hdr3)

            Dim Cell5Hdr3 As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5Hdr3.Border = Rectangle.NO_BORDER
            myTable3.AddCell(Cell5Hdr3)

            Dim Cell5_1Hdr3 As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5_1Hdr3.Border = Rectangle.NO_BORDER
            myTable3.AddCell(Cell5_1Hdr3)
            myDocument.Add(myTable3)
            myDocument.Add(Chunk.NEWLINE)

            Dim strTarikhTutup As String = oCommon.getAppsettings("PPCSTarikhTutup")

            Dim myPara21 As New Paragraph("2. Sekiranya anak tuan/ puan bersetuju untuk menyertai PPCS, polisi dan borang A - D boleh dimuat turun daripada laman sesawang http://semak.permatapintar.edu.my. Mohon tuan/ puan agar memberi keputusan penerimaan/ penolakkan tawaran sebelum " & strTarikhTutup & ". Kegagalan berbuat demikian, pihak pusat beranggapan bahawa pihak tuan/ puan telah menolak tawaran ini.", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara21.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara21)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara22 As New Paragraph("3. Sebarang maklumat atau pertanyaan lanjut boleh menghubungi Unit PPCS di talian 03-89217521/ 7532/ 7533. Polisi dan borang yang telah dilengkapkan boleh dihantar ke Unit PPCS atau diemel ke pcspermata@gmail.com.", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara22.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara22)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara23 As New Paragraph("4. Segala kerjasama dan keprihatinan tuan/ puan amat dihargai dan didahului dengan ucapan terima kasih.", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara23.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara23)
            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara24 As New Paragraph("Yang benar, ", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara24.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara24)
            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara25 As New Paragraph("PROF. DATUK DR. NORIAH MOHD ISHAK", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara25.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara25)

            Dim myPara26 As New Paragraph("Pengarah", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara26.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara26)

            Dim myPara27 As New Paragraph("Pusat Permata Pintar ™ Negara", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara27.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara27)

            Dim myPara28 As New Paragraph("Universiti Kebangsaan Malaysia", FontFactory.GetFont("Arial", 9, Font.NORMAL))
            myPara28.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara28)


            'Dim strBottom As String = Server.MapPath(".") & "\pic\sijil-bottom.png"
            'Dim imgBottom As Image = Image.GetInstance(strBottom)
            'imgBottom.Alignment = Image.MIDDLE_ALIGN
            'myDocument.Add(imgBottom)

            'Response.Write("Succesfully create the PDF")
            hyPDF.Visible = True
            lblMsg.Text = "PDF fail siap dijana."
            hyPDF.Text = "Klik disini untuk buka surat tawaran (1_SURAT PELAJAR.PDF)."
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

    Protected Sub btnTerima_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTerima.Click
        lblMsgTop.Text = ""

        strSQL = "UPDATE PPCS SET StatusTawaran='TERIMA',StatusDate='" & oCommon.getNow & "' WHERE PPCSStatus='LAYAK' AND StudentID='" & oCommon.FixSingleQuotes(lblStudentID.Text) & "' AND PPCSDate='" & lblPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini status tawaran anda kepada TERIMA."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System error: " & strRet
        End If

    End Sub

    Private Sub btnTolak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTolak.Click
        lblMsgTop.Text = ""

        If txtStatusReason.Text.Length = 0 Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Masukkan kenapa anda menolak tawaran ini."
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        '--auto set pelajar yg tolak, ppcsstatus=REJECT. Dr Siti 20131108
        strSQL = "UPDATE PPCS SET PPCSStatus='REJECT',StatusTawaran='TOLAK',StatusReason='" & oCommon.FixSingleQuotes(txtStatusReason.Text) & "',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & oCommon.FixSingleQuotes(lblStudentID.Text) & "' AND PPCSDate='" & lblPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini status tawaran anda kepada TOLAK."
            lblMsgTop.Text = lblMsg.Text
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System error: " & strRet
        End If
    End Sub

End Class