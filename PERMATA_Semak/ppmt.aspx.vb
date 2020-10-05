Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Public Class ppmt
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '--not yet open
        Response.Redirect("contactus.aspx")
        Exit Sub

        btnTerima.Attributes.Add("onclick", "return confirm('Pasti ingin MENERIMA tawaran ini?');")
        btnTolak.Attributes.Add("onclick", "return confirm('Pasti ingin MENOLAK tawaran ini? Masukkan sebab kenapa tawaran ini ditolak.');")

        If Not IsPostBack Then
            lblPPCSDate.Text = ConfigurationManager.AppSettings("PPCSDate")
        End If

    End Sub

    Protected Sub btnSemak_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSemak.Click
        '--check if allow publish or not
        If ConfigurationManager.AppSettings("PPCSResult") = "N" Then
            lblMsg.Text = "Keputusan PPCS belum dimuktamadkan lagi!"
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        '--remove text on board
        Clear_screen()

        If ValidatePage() = False Then
            divMsg.Attributes("class") = "warning"
            Exit Sub
        End If

        If IsLayak() = True Then
            PPMT_view()
            StudentProfile_view()

            '--Dr Siti 20131017. Benarkan pelajar tukar status terima/tolak sebelum 1 Nov
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "TAHNIAH! Anda LAYAK ke PROGRAM PENDIDIKAN PERMATApintar™ bagi tahun " & ConfigurationManager.AppSettings("PPMTExamYear") & "."
            btnPrint.Visible = True

            '--tidak benarkan tolak/terima selepas 1 nov 2013.
            If isPPCSEND() = True Then
                pnlDisplay.Visible = False
            Else
                pnlDisplay.Visible = True
            End If

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Anda tidak berjaya ke PROGRAM PENDIDIKAN PERMATApintar™ bagi tahun " & ConfigurationManager.AppSettings("PPMTExamYear") & ". Jangan putus asa, cuba lagi dan tingkatkan prestasi anda tahun hadapan."
            btnPrint.Visible = False
            pnlDisplay.Visible = False
        End If

    End Sub

    Private Function isTolak() As Boolean
        strSQL = "SELECT StatusDate FROM PPCS WHERE StatusTawaran='TOLAK' AND StudentID='" & lblStudentID.Text & "' AND ExamYear='" & ConfigurationManager.AppSettings("ExamYear") & "'"
        '--debug
        'Response.Write(strSQL)
        If oCommon.isExist(strSQL) = True Then
            Return True
        End If

        Return False
    End Function

    Private Function isPPCSEND() As Boolean
        Try
            strSQL = "SELECT configString FROM master_Config WHERE configCode='PPMTEND'"
            strRet = oCommon.getFieldValue(strSQL)  'yyyymmdd

            'yyyymmdd
            Dim strToday As String = oCommon.getToday
            If CInt(strToday) >= CInt(strRet) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub PPMT_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT Program FROM UKM3"
        strWhere = " WHERE StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & ConfigurationManager.AppSettings("PPCSDate") & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Program")) Then
                    lblProgram.Text = ds.Tables(0).Rows(0).Item("Program")
                Else
                    lblProgram.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub StudentProfile_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT * FROM StudentProfile"
        strWhere = " WHERE StudentID='" & lblStudentID.Text & "'"
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
                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentAddress1")) Then
                    lblStudentAddress1.Text = MyTable.Rows(nRows).Item("StudentAddress1").ToString
                Else
                    lblStudentAddress1.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentAddress2")) Then
                    lblStudentAddress2.Text = MyTable.Rows(nRows).Item("StudentAddress2").ToString
                Else
                    lblStudentAddress2.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentPostcode")) Then
                    lblStudentPostcode.Text = MyTable.Rows(nRows).Item("StudentPostcode").ToString
                Else
                    lblStudentPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentCity")) Then
                    lblStudentCity.Text = MyTable.Rows(nRows).Item("StudentCity").ToString
                Else
                    lblStudentCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentState")) Then
                    lblStudentState.Text = MyTable.Rows(nRows).Item("StudentState").ToString
                Else
                    lblStudentState.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("NoPelajar")) Then
                    lblNoPelajar.Text = MyTable.Rows(nRows).Item("NoPelajar").ToString
                Else
                    lblNoPelajar.Text = ""
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
        lblProgram.Text = ""

        lblStudentAddress1.Text = ""
        lblStudentAddress2.Text = ""
        lblStudentPostcode.Text = ""
        lblStudentCity.Text = ""
        lblStudentState.Text = ""

        pnlDisplay.Visible = False
        hyPDF.Visible = False
    End Sub

    Private Function IsLayak() As Boolean
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)
        lblStudentID.Text = strStudentID

        '--display fullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT StudentID FROM UKM3 WHERE PPMT='Y' AND StudentID='" & strStudentID & "' AND PPCSDate='" & ConfigurationManager.AppSettings("PPCSDate") & "'"
        '--debug
        'Response.Write(strSQL)
        If oCommon.isExist(strSQL) = False Then
            Return False
        End If

        Return True
    End Function

    Private Function ValidatePage() As Boolean
        If txtMYKAD.Text.Length = 0 Then
            txtMYKAD.Focus()
            lblMsg.Text = "Sila masukkan MYKAD/MYKID#!"
            Return False
        End If

        Return True
    End Function


    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim msFileName As String = ""
        Dim msFilePath As String = ""

        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = "surat-tawaran-ppmt-" & txtMYKAD.Text & ".pdf"
            msFilePath = Server.MapPath(".") & "\cert_pdf\" & msFileName
            hyPDF.NavigateUrl = "cert_pdf/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("Jamain Johari (ARAKEN SDN BHD)")
            myDocument.AddSubject("PPMT " & ConfigurationManager.AppSettings("PPMTExamYear"))

            'Step 4: Now add some contents to the document

            'add image
            Dim strTop As String = Server.MapPath(".") & "\pic\4p-top-pdf.png"
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
            myDocument.Add(chnkLINE)

            Dim myPara As New Paragraph("Rujukan   : UKM1.41/281/4 ", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara)

            Dim myPara2 As New Paragraph("Tarikh :" & oCommon.FormatDateDMY(Now), FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara2)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara3 As New Paragraph(lblStudentFullname.Text & " MYKAD#:" & txtMYKAD.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara3)

            'Dim myPara4 As New Paragraph("MYKAD#:" & txtMYKAD.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            'myPara4.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myPara4)

            Dim myPara5 As New Paragraph(lblStudentAddress1.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara5)

            Dim myPara6 As New Paragraph(lblStudentAddress2.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara6.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara6)

            Dim myPara7 As New Paragraph(lblStudentPostcode.Text & ", " & lblStudentCity.Text & ", " & lblStudentState.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara7.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara7)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara8 As New Paragraph("Saudara/i", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara8.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara8)

            Dim myPara9 As New Paragraph("TAWARAN KEMASUKAN KE PROGRAM PENDIDIKAN PERMATApintar™, PUSAT PERMATApintar™ NEGARA, UNIVERSITI KEBANGSAAN MALAYSIA", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara9.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara9)


            myDocument.Add(Chunk.NEWLINE)
            Dim strBody As String = "Tahniah diucapkan kepada saudara kerana ditawarkan tempat bagi mengikuti program di Pusat PERMATApintar™ Negara, Universiti Kebangsaan Malaysia seperti butiran berikut:"
            Dim myPara10 As New Paragraph(strBody, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara10.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara10)

            Dim myPara15 As New Paragraph("No Pelajar: " & lblNoPelajar.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara15.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara15)

            Dim myPara11 As New Paragraph("Program: PROGRAM PENDIDIKAN PERMATApintar™ NEGARA", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara11.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11)

            Dim myPara12 As New Paragraph("Aras Pengajian: " & lblProgram.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara12.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara12)

            Dim myPara13 As New Paragraph("Tempat Pengajian: PUSAT PERMATApintar™ NEGARA", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara13.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara13)

            Dim myPara14 As New Paragraph("Kolej Kediaman: KOLEJ KEDIAMAN PERMATApintar™ ", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara14.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara14)

            Dim myPara16 As New Paragraph("Saudara diminta hadir untuk mendaftar pada tarikh, tempat dan masa seperti berikut:", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara16.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara16)

            Dim myPara17 As New Paragraph("Tarikh: 20 JANUARI 2014 (ISNIN)", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara17.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara17)

            Dim myPara18 As New Paragraph("Tempat: DEWAN SERBAGUNA PUSAT", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara18.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara18)

            Dim myPara19 As New Paragraph("Waktu Pendaftaran: 8:30 PAGI - 11.00 PAGI", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara19.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara19)

            Dim myPara20 As New Paragraph("Taklimat Ibu Bapa: 11:00 PAGI- 1:00 TENGAHARI (DEWAN MAKAN)", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara20.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara20)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara21 As New Paragraph("Saudara diminta menjawab surat penerimaan tawaran ini dengan kadar segera pada atau sebelum  16 Januari 2014 (Khamis) 	melalui fax kepada 03-89217525. Bersama-sama ini, dilampirkan borang- borang yang perlu dilengkapkan oleh saudara dan hendaklah diserahkan semasa sesi pendaftaran. Sebarang  pertanyaan  dan  rujukan  bolehlah  menghubungi:", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara21.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara21)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara22 As New Paragraph("1. Encik  Jalani  bin  Ismail/Puan Siti Rahmah Ismail  di  talian  03- 89217529.", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara22.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara22)

            Dim myPara23 As New Paragraph("2. Encik Saparuddin Zainuddin di talian 0389217506", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara23.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara23)

            Dim myPara24 As New Paragraph("3. Puan Noorsyakina Simin di talian 0389217528", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara24.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara24)

            Dim myPara25 As New Paragraph("4. Dr. Rorlinda Yusof di talian 0389216255", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara25.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara25)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara26 As New Paragraph("Sekian, terima kasih.", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara26.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara26)


            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)
            Dim myPara27 As New Paragraph("Yang benar,", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara27.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara27)

            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)
            Dim myPara28 As New Paragraph("PROF. DR. NORIAH MOHD ISHAK ", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara28.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara28)

            Dim myPara29 As New Paragraph("Pengarah ", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara29.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara29)

            Dim myPara30 As New Paragraph("Pusat PERMATApintar™ Negara", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara30.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara30)


            myDocument.Add(chnkLINE)
            Dim myPara31 As New Paragraph("(Surat tawaran ini janaan komputer dan tidak perlu tandatangan) ", FontFactory.GetFont("Arial", 10, Font.ITALIC))
            myPara31.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara31)

            Dim strBottom As String = Server.MapPath(".") & "\pic\4p-sijil-bottom.png"
            Dim imgBottom As Image = Image.GetInstance(strBottom)
            imgBottom.Alignment = Image.MIDDLE_ALIGN
            myDocument.Add(imgBottom)

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
        strSQL = "UPDATE UKM3 SET StatusTawaran='TERIMA',StatusDate='" & oCommon.getNow & "' WHERE PPMT='Y' AND StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & ConfigurationManager.AppSettings("PPCSDate") & "'"
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
        If txtStatusReason.Text.Length = 0 Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Masukkan kenapa anda menolak tawaran ini."
            Exit Sub
        End If

        '--auto set pelajar yg tolak, ppcsstatus=REJECT. Dr Siti 20131108
        '--PPMT status maintain walaupun pelajar TOLAK. Dr Siti Email 20140108
        'strSQL = "UPDATE UKM3 SET PPMT='N',StatusTawaran='TOLAK',StatusReason='" & oCommon.FixSingleQuotes(txtStatusReason.Text) & "',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & ConfigurationManager.AppSettings("PPCSDate") & "'"
        strSQL = "UPDATE UKM3 SET StatusTawaran='TOLAK',StatusReason='" & oCommon.FixSingleQuotes(txtStatusReason.Text) & "',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & ConfigurationManager.AppSettings("PPCSDate") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini status tawaran anda kepada TOLAK."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System error: " & strRet
        End If
    End Sub

End Class