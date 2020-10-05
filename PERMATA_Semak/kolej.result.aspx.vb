Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class kolej_result
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
                lblPPYear.Text = oCommon.getAppsettings("KOLEJTahunKemasukan")
                lblTarikhTutup.Text = oCommon.getAppsettings("KOLEJTarikhTutup")
                '--lock value
                lblMYKAD.Text= Request.QueryString("stdmykad")
                Check_student()
                UKM3_view()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Check_student()
        If IsLayak() = True Then
            If ISTerima() = "" Then
                UKM3_view()
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "TAHNIAH! Anda LAYAK ke Kolej PERMATApintar bagi tahun kemasukkan " & lblPPYear.Text & "."
                lblMsgTop.Text = lblMsg.Text
                PnlStatus.Visible = True
                pnlDisplay.Visible = True
                btnPrint.Visible = True
                pnltolak.Visible = False
                pnlreason.Visible = False
            ElseIf ISTerima() = "TERIMA" Then
                UKM3_view()
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "TAHNIAH! Anda LAYAK ke Kolej PERMATApintar bagi tahun kemasukkan " & lblPPYear.Text & "."
                lblMsgTop.Text = lblMsg.Text
                PnlStatus.Visible = True
                pnlDisplay.Visible = True
                btnPrint.Visible = True
                pnltolak.Visible = False
                pnlreason.Visible = False
            ElseIf ISTerima() = "TOLAK" Then
                UKM3_view()
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Anda telah MENOLAK tawaran ke Kolej PERMATApintar bagi tahun kemasukkan " & lblPPYear.Text & "."
                lblMsgTop.Text = lblMsg.Text
                pnltolak.Visible = True
                PnlStatus.Visible = False
                pnlDisplay.Visible = False
                btnPrint.Visible = False
                pnlreason.Visible = True
            End If
        Else
            UKM3_view()
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Anda tidak mendapat tempat ke Kolej PERMATApintar bagi tahun kemasukkan  " & lblPPYear.Text & "."
            lblMsgTop.Text = lblMsg.Text
            pnltolak.Visible = False
            btnPrint.Visible = False
            pnlDisplay.Visible = False
            PnlStatus.Visible = False
            pnlreason.Visible = False
        End If

    End Sub

    Private Sub UKM3_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT PPYear,Program,PPMT,StatusTawaran,StatusReason,MYKAD,StudentFullname,StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState FROM UKM3 a,StudentProfile b"
        strWhere = " WHERE a.StudentID=b.StudentID"
        strWhere += " AND b.MYKAD='" & lblMYKAD.Text & "'"
        strWhere += " AND a.PPYear='" & lblPPYear.Text & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("MYKAD")) Then
                    lblMYKAD.Text = MyTable.Rows(nRows).Item("MYKAD").ToString
                Else
                    lblMYKAD.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentFullname")) Then
                    lblStudentFullname.Text = MyTable.Rows(nRows).Item("StudentFullname").ToString
                Else
                    lblStudentFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PPMT")) Then
                    Dim PPMT As String = MyTable.Rows(nRows).Item("PPMT").ToString

                    If PPMT = "Y" Then
                        lblPPMT.Text = "LAYAK"
                    ElseIf PPMT = "N" Then
                        lblPPMT.Text = "TIDAK LAYAK"
                    End If
                Else
                    lblPPMT.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("StatusTawaran")) Then
                    lblStatusTawaran.Text = MyTable.Rows(nRows).Item("StatusTawaran").ToString
                Else
                    lblStatusTawaran.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("StatusReason")) Then
                    lblStatusReason.Text = MyTable.Rows(nRows).Item("StatusReason").ToString
                Else
                    lblStatusReason.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Program")) Then
                    lblProgram.Text = MyTable.Rows(nRows).Item("Program").ToString
                Else
                    lblProgram.Text = ""
                End If

                '--studentprofile address StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState
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
        lblProgram.Text = ""

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
            msFileName = "Surat_Tawaran_Kemasukan_2018_" & lblMYKAD.Text & ".pdf"
            msFilePath = Server.MapPath(".") & "\cert_pdf\" & msFileName
            hyPDF.NavigateUrl = "cert_pdf/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("(ARAKEN SDN BHD)")
            myDocument.AddSubject("TAHNIAH! Anda LAYAK ke Kolej PERMATApintar bagi sessi " & lblPPYear.Text)

            'Step 4: Now add some contents to the document

            ''add background image
            Dim strTop As String = Server.MapPath(".") & "\pic\Background_kolej.png"
            Dim imgTop As Image = Image.GetInstance(strTop)
            imgTop.ScaleToFit(3330, 830)
            imgTop.Alignment = Image.UNDERLYING
            myDocument.Add(imgTop)

            ''--create horizontalline
            'Dim g As New Graphic
            'g.SetHorizontalLine(5.0F, 100.0F)
            'myDocument.Add(g)

            'To end the section with a dotted line
            Dim sepLINE As New iTextSharp.text.pdf.draw.LineSeparator
            sepLINE.LineWidth = 1
            Dim chnkLINE As New iTextSharp.text.Chunk(sepLINE)

            Dim test As String = " " & Environment.NewLine & Environment.NewLine & Environment.NewLine & Environment.NewLine & Environment.NewLine & Environment.NewLine
            Dim myParam As New Paragraph(test)
            myDocument.Add(myParam)

            Dim myPara As New Paragraph("Rujukan: UKM PMT/238/2", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara.Alignment = Element.ALIGN_RIGHT
            myDocument.Add(myPara)

            Dim myPara2 As New Paragraph("Tarikh: 23 Disember 2017 ", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_RIGHT
            myDocument.Add(myPara2)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara3 As New Paragraph(lblStudentFullname.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara3)

            Dim myPara4 As New Paragraph(lblStudentAddress1.Text & "," & lblStudentAddress2.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara4)

            Dim myPara5 As New Paragraph(lblStudentPostcode.Text & " " & lblStudentCity.Text & "," & lblStudentState.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara5)

            myDocument.Add(Chunk.NEWLINE)

            Dim myPara8 As New Paragraph("Saudara/saudari,", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara8.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara8)

            myDocument.Add(Chunk.NEWLINE)

            Dim myPara9 As New Paragraph("TAWARAN MENGIKUTI PROGRAM KOLEJ PERMATApintar® UKM BAGI SESI AKADEMIK " & lblPPYear.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara9.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara9)
            myDocument.Add(Chunk.NEWLINE)

            Dim strBody As String = "Setinggi-tinggi tahniah diucapkan kepada saudara/saudari kerana telah berjaya melepasi semua ujian UKM1, UKM2 dan UKM3. Dengan sukacitanya, saudara/saudari ditawarkan untuk mengikuti program pengajian di Kolej PERMATApintar® UKM, Bangi."
            Dim myPara10 As New Paragraph(strBody, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara10.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara10)

            myDocument.Add(Chunk.NEWLINE)
            Dim strBody2 As String = "Maklumat tawaran adalah seperti berikut:"
            Dim myPara10a As New Paragraph(strBody2, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara10.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara10a)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara11a As New Paragraph("Program Pengajian      : " & lblProgram.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara11a.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11a)

            Dim strKOLEJDaftarTarikh As String = oCommon.getAppsettings("KOLEJDaftarTarikh")
            Dim myPara11b As New Paragraph("Tarikh Pendaftaran      : " & strKOLEJDaftarTarikh, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara11b.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11b)

            Dim strKOLEJDaftarMasa As String = oCommon.getAppsettings("KOLEJDaftarMasa")
            Dim myPara11c As New Paragraph("Masa Pendaftaran       : " & strKOLEJDaftarMasa, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara11c.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11c)

            Dim strKOLEJDaftarTempat As String = oCommon.getAppsettings("KOLEJDaftarTempat")
            Dim myPara12 As New Paragraph("Tempat Pendaftaran    : " & strKOLEJDaftarTempat, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara12.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara12)

            myDocument.Add(Chunk.NEWLINE)
            Dim c1 As New Chunk("Sila muat turun semua dokumen di laman semakan dan pastikan ", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            Dim c2 As New Chunk("LAMPIRAN A – BORANG JAWAPAN PENERIMAAN ATAU PENOLAKAN ", FontFactory.GetFont("Arial", 10, Font.BOLD))
            Dim c3 As New Chunk("dihantar semula ke Pusat PERMATApintar® Negara sebelum atau pada ", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            Dim c4 As New Chunk("5 Januari 2018 (Jumaat) ", FontFactory.GetFont("Arial", 10, Font.BOLD))
            Dim c5 As New Chunk("melalui e-mel kepada kolejppn@gmail.com. Lain-lain borang yang telah dilengkapkan perlu dibawa dan diserahkan semasa hari pendaftaran. ", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            ''combine all text in 1 line
            Dim myPara20 As New Paragraph()
            myPara20.Add(c1)
            myPara20.Add(c2)
            myPara20.Add(c3)
            myPara20.Add(c4)
            myPara20.Add(c5)
            myPara20.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara20)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara20a As New Paragraph("Sebarang pertanyaan boleh dikemukakan kepada Puan Afifah binti Mohamad Radzi/ Pn Siti Aishah binti Hassan/ Pn Nurul Suzaina Joli (03 89217505/7528/7556), atau En. Jailani Ismail (03 89217529).", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara20a.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara20a)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara20b As New Paragraph("Sekian, terima kasih.", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara20b.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara20b)

            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara20c As New Paragraph("Yang benar,", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara20c.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara20c)

            myDocument.Add(Chunk.NEWLINE)

            Dim myPara25 As New Paragraph("PROF. DATUK DR. NORIAH MOHD ISHAK", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara25.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara25)

            Dim myPara26 As New Paragraph("Pengarah", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara26.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara26)

            Dim myPara27 As New Paragraph("Pusat PERMATApintar® Negara", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara27.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara27)

            Dim myPara27b As New Paragraph("(Surat tawaran ini janaan komputer, tandatangan tidak diperlukan)", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara27b.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara27b)

            'Response.Write("Succesfully create the PDF")
            hyPDF.Visible = True
            lblMsg.Text = "PDF fail siap dijana."
            lblMsgTop.Text = lblMsg.Text
            hyPDF.Text = "Klik disini untuk buka surat tawaran (Surat Tawaran Kemasukan 2018)."
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
        Dim strStudentMYKAD As String = Request.QueryString("stdmykad")

        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)
        lblStudentID.Text = strStudentID

        strSQL = "UPDATE UKM3 SET StatusTawaran='TERIMA',StatusReason='',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & lblStudentID.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini status tawaran anda kepada TERIMA."
            lblMsgTop.Text = lblMsg.Text
            Response.Redirect("kolej.result.aspx?stdmykad=" + strStudentMYKAD)
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

        Dim strStudentMYKAD As String = Request.QueryString("stdmykad")

        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)
        lblStudentID.Text = strStudentID

        '--auto set pelajar yg tolak, ppcsstatus=TOLAK. Dr Siti 20131108
        strSQL = "UPDATE UKM3 SET StatusTawaran='TOLAK',StatusReason='" & oCommon.FixSingleQuotes(txtStatusReason.Text) & "',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & lblStudentID.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini status tawaran anda kepada TOLAK."
            lblMsgTop.Text = lblMsg.Text
            Response.Redirect("kolej.result.aspx?stdmykad=" + strStudentMYKAD)
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System error: " & strRet
        End If
    End Sub

    Private Function IsLayak() As Boolean
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)
        lblStudentID.Text = strStudentID

        '--display fullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        '--PPMT=Kolej PERMATApintar
        strSQL = "SELECT StudentID FROM UKM3 WHERE PPMT='Y' AND StudentID='" & strStudentID & "' AND PPYear='" & lblPPYear.Text & "'"
        If oCommon.isExist(strSQL) = False Then
            Return False
        End If

        Return True
    End Function

    Private Function ISTerima() As String
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & lblMYKAD.Text & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)
        ''lblStudentID.Text = strStudentID

        '--display fullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & lblMYKAD.Text & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        '--PPMT=Kolej PERMATApintar
        strSQL = "SELECT StatusTawaran FROM UKM3 WHERE PPMT='Y' AND StudentID='" & lblStudentID.Text & "' AND PPYear='" & lblPPYear.Text & "'"
        Dim value As String = oCommon.getFieldValue(strSQL)

        If value = "" Then
            Return ""
        ElseIf value = "TOLAK" Then
            Return "TOLAK"
        Else
            Return "TERIMA"
        End If
    End Function

End Class