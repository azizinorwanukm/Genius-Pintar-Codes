Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Public Class ppcs
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
                lblPPCSDate.Text = ConfigurationManager.AppSettings("PPCSDate")
                lblTarikhTutup.Text = ConfigurationManager.AppSettings("TarikhTutup")
            End If

        Catch ex As Exception

        End Try
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
            PPCS_view()
            SchoolProfile_view()

            '--Dr Siti 20131017. Benarkan pelajar tukar status terima/tolak sebelum 1 Nov
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "TAHNIAH! Anda LAYAK ke PPCS bagi sessi " & lblPPCSDate.Text & "."
            lblMsgTop.Text = lblMsg.Text
            btnPrint.Visible = True

            '--tidak benarkan tolak/terima selepas 1 nov 2013.
            If isPPCSEND() = True Then
                pnlDisplay.Visible = False
            Else
                pnlDisplay.Visible = True
            End If

            '--Dr Siti 20131017. Benarkan pelajar tukar status terima/tolak sebelum tarikh tutup
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Anda tidak berjaya ke PPCS sessi " & lblPPCSDate.Text & ". Jangan putus asa, cuba lagi dan tingkatkan prestasi anda tahun hadapan."
            lblMsgTop.Text = lblMsg.Text
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
            strSQL = "SELECT configString FROM master_Config WHERE configCode='PPCSEND'"
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

    Private Sub PPCS_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT PPCS_Course.CourseCode,PPCS_Course.CourseNameBM,PPCS.StatusDate FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        strWhere = " WHERE PPCS.StudentID='" & lblStudentID.Text & "' AND PPCS.PPCSDate ='" & lblPPCSDate.Text & "'"
        strSQL = tmpSQL & strWhere & strOrder

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
        strWhere = " WHERE a.StudentID='" & lblStudentID.Text & "' AND a.SchoolID=b.SchoolID"
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

    Private Function IsLayak() As Boolean
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)
        lblStudentID.Text = strStudentID

        '--display fullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT StudentID FROM PPCS WHERE PPCSStatus='LAYAK' AND StudentID='" & strStudentID & "' AND PPCSDate='" & lblPPCSDate.Text & "'"
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
            msFileName = "surat-tawaran-" & txtMYKAD.Text & ".pdf"
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
            myDocument.Add(chnkLINE)

            Dim myPara As New Paragraph("Rujukan : UKM1.41/344/2", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara)

            Dim myPara2 As New Paragraph("Tarikh :" & oCommon.FormatDateDMY(Now), FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara2)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara3 As New Paragraph(lblStudentFullname.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara3)

            Dim myPara4 As New Paragraph("MYKAD#:" & txtMYKAD.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara4.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara4)

            Dim myPara5 As New Paragraph(lblSchoolName.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara5)

            Dim myPara6 As New Paragraph(lblSchoolAddress.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara6.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara6)

            Dim myPara7 As New Paragraph(lblSchoolPostcode.Text & ", " & lblSchoolCity.Text & ", " & lblSchoolState.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara7.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara7)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara8 As New Paragraph("Tuan/ Puan,", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara8.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara8)

            Dim myPara9 As New Paragraph("TAWARAN MENGIKUTI PROGRAM PERKHEMAHAN CUTI SEKOLAH " & lblPPCSDate.Text & ".", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara9.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara9)


            myDocument.Add(Chunk.NEWLINE)
            Dim strBody As String = "Sukacita dimaklumkan bahawa anak tuan/ puan, berjaya untuk menyertai PPCS yang merupakan anjuran UKM dengan kerjasama Johns Hopkins University, Center for Talented Youth (JHU – CTY). Perincian pendaftaran adalah seperti berikut:- "
            Dim myPara10 As New Paragraph(strBody, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara10.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara10)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara11a As New Paragraph("Nama: " & lblStudentFullname.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara11a.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11a)

            Dim myPara11b As New Paragraph("Kad Pengenalan: " & txtMYKAD.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara11b.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11b)

            Dim myPara11c As New Paragraph("Kursus: " & lblCourseCode.Text & " > " & lblCourseNameBM.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara11c.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11c)

            Dim strTarikhProgram As String = ConfigurationManager.AppSettings("TarikhProgram")
            Dim myPara12 As New Paragraph("Tarikh Program: " & strTarikhProgram, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara12.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara12)

            Dim myPara13 As New Paragraph("Lokasi Program: Pusat PERMATApintar™ Negara, Universiti Kebangsaan Malaysia.", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara13.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara13)

            myDocument.Add(Chunk.NEWLINE)
            Dim strTarikhTutup As String = ConfigurationManager.AppSettings("TarikhTutup")
            Dim myPara14 As New Paragraph("Sila baca dan fahami  polisi PPCS dan lain-lain dokumen yang boleh dimuat turun dari laman sesawang ini.  Anda di kehendaki mengemukakan  surat tawaran ini kepada pihak sekolah dan ibu bapa anda.   Pelajar dikehendaki menyatakan persetujuan menerima atau menolak secara dalam talian  melalui sistem semakan ini sebelum " & strTarikhTutup & ". Borang-borang yang perlu diisi hendaklah dihantar kepada Pusat PERMATApintar sebelum " & strTarikhTutup & ".", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara14.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara14)

            'myDocument.Add(chnkLINE)
            'Dim myPara15 As New Paragraph("Sesi PAGI : 8:00 pagi - 11:00 pagi", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            'myPara15.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myPara15)

            'Dim myPara16 As New Paragraph("Sesi TENGAHARI : 11:00 pagi - 2:00 petang", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            'myPara16.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myPara16)

            'Dim myPara17 As New Paragraph("Sesi PETANG : 2:00 petang - 5:00 petang", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            'myPara17.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myPara17)
            'myDocument.Add(chnkLINE)

            'Dim myPara18 As New Paragraph("Sila kembalikan kesemua surat atau borang sepertimana yang terdapat dalam ruangan muat turun.", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            'myPara18.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myPara18)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara19 As New Paragraph("Sekian, terima kasih", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara19.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara19)

            myDocument.Add(chnkLINE)
            Dim myPara20 As New Paragraph("(Surat tawaran ini janaan komputer dan tidak perlu tandatangan) ", FontFactory.GetFont("Arial", 10, Font.ITALIC))
            myPara20.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara20)

            Dim strBottom As String = Server.MapPath(".") & "\pic\sijil-bottom.png"
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
        lblMsgTop.Text = ""

        strSQL = "UPDATE PPCS SET StatusTawaran='TERIMA',StatusDate='" & oCommon.getNow & "' WHERE PPCSStatus='LAYAK' AND StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & lblPPCSDate.Text & "'"
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
        strSQL = "UPDATE PPCS SET PPCSStatus='REJECT',StatusTawaran='TOLAK',StatusReason='" & oCommon.FixSingleQuotes(txtStatusReason.Text) & "',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & lblPPCSDate.Text & "'"
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