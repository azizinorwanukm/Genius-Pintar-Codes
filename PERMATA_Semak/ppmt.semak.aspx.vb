Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ppmt_semak
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
                If oCommon.getAppsettings("SEMAK_PPMT") = "N" Then
                    Response.Redirect("default.aspx")
                End If

                lblPPMTTitle.Text = oCommon.getAppsettings("PPMTTitle")
                lblPPMTSessi.Text = oCommon.getAppsettings("PPMTSessi")
                lblTarikhTutup.Text = oCommon.getAppsettings("PPMTTarikhTutup")

                imgPDF.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub


    '-get from master_config related to PPMT
    Private Sub getPPMTInfo()

        lblPPMTTarikhProgram.Text = oCommon.getAppsettings("PPMTTarikhProgram")
        lblPPMTLokasiProgram.Text = oCommon.getAppsettings("PPMTLokasiProgram")
        lblPPMTTarikhDaftar.Text = oCommon.getAppsettings("PPMTTarikhDaftar")
        lblPPMTMasa.Text = oCommon.getAppsettings("PPMTMasa")
        lblPPMTTempatDaftar.Text = oCommon.getAppsettings("PPMTTempatDaftar")
        'lblPPMTYuran.Text = oCommon.getAppsettings("PPMTYuran")

    End Sub

    Protected Sub btnSemak_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSemak.Click
        '--check if allow publish or not
        If oCommon.getAppsettings("PPMTResult") = "N" Then
            lblMsg.Text = "Keputusan PPMT belum dimuktamadkan lagi!"
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        '--remove text on board
        RefreshScreen()
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "warning"
            Exit Sub
        End If

        '--get studentprofile and PPMTstatus and statustawaran
        PPMT_loadinfo()
        Select Case lblPPMTStatus.Text
            Case "LAYAK"
                setLAYAK()
            Case "TOLAK"
                setTOLAK()
                imgPDF.Visible = False
            Case "SIMPANAN"
                setSIMPANAN()
                imgPDF.Visible = False
            Case Else
                setTIDAKLAYAK()
                imgPDF.Visible = False
        End Select

    End Sub

    Private Sub setTOLAK()
        'getPPMTInfo()
        'SchoolProfile_view()

        ''--Dr Siti 20131017. Benarkan pelajar tukar status terima/tolak sebelum TUTUP
        'divMsg.Attributes("class") = "info"
        'lblMsg.Text = "TAHNIAH! Anda BERJAYA ke " & lblPPMTSessi.Text & "."
        'lblIsPosMsg.Text = "*Anda masih boleh MENERIMA tawaran ini dengan klik [Terima] pada Status Tawaran dibawah."

        'lblMsgTop.Text = lblMsg.Text
        'btnPrint.Visible = True

        ''--tidak benarkan tolak/terima selepas 1 nov 2013.
        'If isPPMTEnd() = True Then
        '    pnlDisplay.Visible = False
        'Else
        '    pnlDisplay.Visible = True
        'End If

        '--Dr Siti tak benarkan TERIMA semula selepas TOLAK

        '--display studentfullname
        getStudentProfile()

        divMsg.Attributes("class") = "warning"
        lblMsg.Text = "Anda telah MENOLAK tawaran ini. Tawaran telah dibuka kepada pelajar lain. Sebarang perubahan sila hubungi admin PERMATApintar."
        lblMsgTop.Text = lblMsg.Text
        btnPrint.Visible = False
        pnlDisplay.Visible = False

    End Sub

    Private Sub setTIDAKLAYAK()
        '--clear all
        RefreshScreen()

        '--display studentfullname
        getStudentProfile()

        divMsg.Attributes("class") = "error"
        lblMsg.Text = "Anda tidak berjaya ke " & lblPPMTSessi.Text & ". Jangan putus asa, cuba lagi dan tingkatkan prestasi anda tahun hadapan."
        lblMsgTop.Text = lblMsg.Text
        btnPrint.Visible = False
        pnlDisplay.Visible = False

    End Sub

    Private Sub setSIMPANAN()
        '--clear all
        RefreshScreen()

        '--display studentfullname
        getStudentProfile()

        divMsg.Attributes("class") = "warning"
        lblMsg.Text = "SIMPANAN: Melepasi skor kelayakan minimum. Pelajar akan ditawarkan jika ada calon LAYAK menolak. Pelajar akan dimaklumkan melalui emel. Sila pastikan emel dan nombor telefon yang betul di <a href='http://pelajar.permatapintar.edu.my' target='_blank'>Laman Rasmi Pelajar PERMATApintar</a>"
        lblMsgTop.Text = lblMsg.Text
        btnPrint.Visible = False
        pnlDisplay.Visible = False

    End Sub

    Private Sub setLAYAK()
        getPPMTInfo()
        SchoolProfile_view()

        '--Dr Siti 20131017. Benarkan pelajar tukar status terima/tolak sebelum TUTUP
        divMsg.Attributes("class") = "info"
        lblMsg.Text = "TAHNIAH! Anda BERJAYA ke " & lblPPMTSessi.Text & "."

        If lblIsPos.Text = "Y" Or lblIsScan.Text = "Y" Then
            lblIsPosMsg.Text = "*Pihak pengurusan PERMATApintar TELAH menerima borang penerimaan anda."
        Else
            lblIsPosMsg.Text = "*Pihak pengurusan PERMATApintar BELUM menerima borang penerimaan anda."
        End If

        lblMsgTop.Text = lblMsg.Text
        btnPrint.Visible = True

        '--tidak benarkan tolak/terima selepas 1 nov 2013.
        If isPPMTEnd() = True Then
            pnlDisplay.Visible = False
        Else
            pnlDisplay.Visible = True
        End If

        '--Dr Siti 20131017. Benarkan pelajar tukar status terima/tolak sebelum tarikh tutup
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

    Private Function isPPMTEnd() As Boolean
        Try
            strSQL = "SELECT configString FROM master_Config WHERE configCode='PPMTEnd'"
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

    Private Sub PPMT_loadinfo()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT Studentprofile.StudentID,Studentprofile.StudentFullname,Studentprofile.MYKAD,PPCS_Course.CourseCode,PPCS_Course.CourseNameBM,PPCS.PPCSStatus,PPCS.StatusTawaran,PPCS.StatusDate,PPCS.IsPos,PPCS.IsScan FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        strWhere += " WHERE PPCS.StudentID=Studentprofile.StudentID AND PPCS.PPCSDate ='" & lblPPMTSessi.Text & "'"
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentID")) Then
                    lblStudentID.Text = ds.Tables(0).Rows(0).Item("StudentID")
                Else
                    lblStudentID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentFullname")) Then
                    lblStudentFullname.Text = ds.Tables(0).Rows(0).Item("StudentFullname")
                Else
                    lblStudentFullname.Text = ""
                End If

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSStatus")) Then
                    lblPPMTStatus.Text = ds.Tables(0).Rows(0).Item("PPCSStatus")
                Else
                    lblPPMTStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StatusTawaran")) Then
                    lblStatusTawaran.Text = ds.Tables(0).Rows(0).Item("StatusTawaran")
                Else
                    lblStatusTawaran.Text = ""
                End If

                '--StatusDate
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StatusDate")) Then
                    lblStatusDate.Text = ds.Tables(0).Rows(0).Item("StatusDate")
                Else
                    lblStatusDate.Text = ""
                End If

                '--IsPos
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("IsPos")) Then
                    lblIsPos.Text = ds.Tables(0).Rows(0).Item("IsPos")
                Else
                    lblIsPos.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("IsScan")) Then
                    lblIsScan.Text = ds.Tables(0).Rows(0).Item("IsScan")
                Else
                    lblIsScan.Text = ""
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

    Private Sub RefreshScreen()
        lblStudentID.Text = ""
        lblStudentFullname.Text = ""
        lblCourseCode.Text = ""
        lblCourseNameBM.Text = ""

        lblPPMTStatus.Text = ""
        lblStatusTawaran.Text = ""
        lblStatusDate.Text = ""

        lblSchoolName.Text = ""
        lblSchoolAddress.Text = ""
        lblSchoolPostcode.Text = ""
        lblSchoolCity.Text = ""
        lblSchoolState.Text = ""

        pnlDisplay.Visible = False
        hyPDF.Visible = False

        lblMsgTop.Text = ""
        lblMsg.Text = "Mesej sistem..."
        lblIsPos.Text = ""
        lblIsPosMsg.Text = ""
        lblIsScan.Text = ""

    End Sub

    Private Sub getStudentProfile()
        '--get StudentID
        'strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        'lblStudentID.Text = oCommon.getFieldValue(strSQL)

        '--display fullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

    End Sub

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
            myDocument.AddSubject("TAHNIAH! Anda BERJAYA ke " & lblPPMTSessi.Text)

            'Step 4: Now add some contents to the document

            'add image
            Dim strTop As String = Server.MapPath(".") & "\pic\sijil-top-pdf.png"
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

            Dim myPara As New Paragraph("Rujukan : UKM1.41/344/2", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara)

            Dim myPara2 As New Paragraph("Tarikh :" & oCommon.getDateTime(), FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara2)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara8 As New Paragraph("Saudara/ Saudari,", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara8.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara8)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara9 As New Paragraph("TAWARAN MENGIKUTI PROGRAM PERKHEMAHAN CUTI SEKOLAH " & lblPPMTSessi.Text & ".", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara9.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara9)
            myDocument.Add(Chunk.NEWLINE)

            Dim strBody As String = "Sukacita dimaklumkan bahawa saudara/saudari, berjaya untuk menyertai PPMT yang merupakan anjuran UKM. Perincian pendaftaran adalah seperti berikut:-"
            Dim myPara10 As New Paragraph(strBody, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara10.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara10)
            myDocument.Add(Chunk.NEWLINE)

            '-----
            Dim myPara11a As New Paragraph("Nama: " & lblStudentFullname.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara11a.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11a)

            Dim myPara11b As New Paragraph("Kad Pengenalan: " & txtMYKAD.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara11b.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11b)

            Dim myPara11c As New Paragraph("Kursus: " & lblCourseCode.Text & " > " & lblCourseNameBM.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara11c.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11c)

            Dim myPara12 As New Paragraph("Tarikh Program: " & lblPPMTTarikhProgram.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara12.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara12)

            Dim myPara13 As New Paragraph("Lokasi Program: Pusat PERMATApintar™ Negara, Universiti Kebangsaan Malaysia.", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara13.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara13)
            myDocument.Add(Chunk.NEWLINE)
            '-----
            Dim myPendaftaran As New Paragraph("Pendaftaran", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPendaftaran.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPendaftaran)

            Dim myPendaftaran01 As New Paragraph("Tarikh & Masa: " & lblPPMTTarikhDaftar.Text & "   jam " & lblPPMTMasa.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPendaftaran01.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPendaftaran01)

            Dim myPendaftaran02 As New Paragraph("Tempat: " & lblPPMTTempatDaftar.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPendaftaran02.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPendaftaran02)

            'Dim myPendaftaran03 As New Paragraph("Yuran: " & lblPPMTYuran.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            'myPendaftaran03.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myPendaftaran03)
            'myDocument.Add(Chunk.NEWLINE)

            Dim strYuran As String = Server.MapPath(".") & "\pic\yuran-2016.png"
            Dim imgYuran As Image = Image.GetInstance(strYuran)
            imgYuran.Alignment = Image.LEFT_ALIGN
            myDocument.Add(imgYuran)

            '-----
            Dim myPara14 As New Paragraph("2. Sekiranya saudara/saudari bersetuju untuk menyertai PPMT, polisi dan borang A hingga C boleh dimuat turun daripada laman sesawang http://semak.permatapintar.edu.my. Mohon saudara/saudari agar memberi keputusan penerimaan/ penolakan tawaran sebelum " & lblTarikhTutup.Text & ". Kegagalan berbuat demikian, pihak pusat beranggapan bahawa pihak saudara/saudari telah menolak tawaran ini.", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara14.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara14)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara15 As New Paragraph("3. Sebarang maklumat atau pertanyaan lanjut boleh menghubungi Unit PPMT di talian 03-89217521/ 7532/ 7533. Polisi dan borang yang telah dilengkapkan boleh dihantar ke Unit PPMT atau diemel ke pcspermata@gmail.com.", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara15.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara15)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara16 As New Paragraph("4. Segala kerjasama dan keprihatinan saudara/saudari amat dihargai dan didahului dengan ucapan ribuan terima kasih.", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara16.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara16)
            myDocument.Add(Chunk.NEWLINE)

            '----
            Dim myPara19 As New Paragraph("Sekian, terima kasih", FontFactory.GetFont("Arial", 10, Font.NORMAL))
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
            hyPDF.Text = "Klik disini untuk buka SURAT TAWARAN."
            imgPDF.Visible = True

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

        '--back to normal status
        strSQL = "UPDATE PPCS SET PPCSStatus='LAYAK',StatusTawaran='TERIMA',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & lblPPMTSessi.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        '--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini status tawaran anda kepada TERIMA."
            lblMsgTop.Text = lblMsg.Text
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

        '--auto set pelajar yg tolak, ppcsstatus=TOLAK. Dr Siti 20131108
        strSQL = "UPDATE PPCS SET PPCSStatus='TOLAK',StatusTawaran='TOLAK',StatusReason='" & oCommon.FixSingleQuotes(txtStatusReason.Text) & "',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & lblPPMTSessi.Text & "'"
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