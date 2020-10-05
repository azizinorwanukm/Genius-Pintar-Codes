Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Public Class ukm2
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '--not yet open
        'Response.Redirect("contactus.aspx")
        'Exit Sub

        '--btnPrint.Attributes.Add("onClick", "javascript:window.print(); return false;")
        Try
            If Not IsPostBack Then
                lblMsgTop.Text = ""
                lblExamYear.Text = ConfigurationManager.AppSettings("UKM2ExamYear")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSemak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSemak.Click
        '--remove text on board
        Clear_screen()

        If ValidatePage() = False Then
            divMsg.Attributes("class") = "warning"
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        If IsLayak() = True Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "TAHNIAH! Anda LAYAK ke Ujian UKM2 bagi tahun " & ConfigurationManager.AppSettings("UKM2ExamYear") & "."

            PusatUjian_view()
            SchoolProfile_view()

            '--check sama ada negeri sudah sahkan atau belum
            If lblPusatState.Text.Length = 0 Then
                lblMsg.Text = "Pusat Ujian, Tarikh dan Waktu Ujian belum ditetapkan lagi. Sila semak dari masa ke semasa."
            End If

            '--PusatUjianStatus
            strSQL = "SELECT PusatUjianStatus FROM master_State WHERE State='" & lblPusatState.Text & "'"
            strRet = oCommon.getFieldValue(strSQL)
            If strRet = "Y" Then
                pnlDisplay.Visible = True
                btnPrint.Enabled = True
            Else
                ClearScreen()
                lblMsg.Text = "*Negeri belum mengesahkan Pusat Ujian, Tarikh dan Waktu Ujian. Sila semak kelayakkan dari masa ke semasa."
            End If
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Anda tidak berjaya ke ujian UKM2 bagi tahun " & ConfigurationManager.AppSettings("UKM2ExamYear") & ". Jangan putus asa, cuba lagi dan tingkatkan prestasi anda tahun hadapan."
            btnPrint.Enabled = False
        End If
        lblMsgTop.Text = lblMsg.Text

    End Sub

    Private Sub ClearScreen()
        lblPusatUjian.Text = ""
        lblTarikhUjian.Text = ""
        lblSessiUKM2.Text = ""

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

    Private Sub PusatUjian_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim tmpPusatUjian As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT UKM2.ExamYear,UKM2.TarikhUjian,UKM2.SessiUKM2,PusatUjian.PusatCode,PusatUjian.PusatName,PusatUjian.PusatAddress,PusatUjian.PusatPostcode,PusatUjian.PusatCity,PusatUjian.PusatState FROM UKM2"
        tmpSQL += " LEFT OUTER JOIN PusatUjian ON UKM2.PusatCode=PusatUjian.PusatCode"
        strWhere += " WHERE UKM2.StudentID='" & lblStudentID.Text & "' AND ExamYear='" & ConfigurationManager.AppSettings("ExamYear") & "'"

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PusatName")) Then
                    tmpPusatUjian += ds.Tables(0).Rows(0).Item("PusatName") & "<br/>"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PusatAddress")) Then
                    tmpPusatUjian += ds.Tables(0).Rows(0).Item("PusatAddress") & "<br/>"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PusatPostcode")) Then
                    tmpPusatUjian += ds.Tables(0).Rows(0).Item("PusatPostcode") & "<br/>"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PusatCity")) Then
                    tmpPusatUjian += ds.Tables(0).Rows(0).Item("PusatCity") & "<br/>"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PusatState")) Then
                    tmpPusatUjian += ds.Tables(0).Rows(0).Item("PusatState") & "<br/>"
                    lblPusatState.Text = ds.Tables(0).Rows(0).Item("PusatState")
                End If

                lblPusatUjian.Text = tmpPusatUjian

                Dim strTarikhUjian As String = ""
                If Not IsDBNull(MyTable.Rows(nRows).Item("TarikhUjian")) Then
                    strTarikhUjian = MyTable.Rows(nRows).Item("TarikhUjian").ToString
                Else
                    strTarikhUjian = ""
                End If
                lblTarikhUjian.Text = oCommon.FormatDateDDMMYYYY(strTarikhUjian)

                If Not IsDBNull(MyTable.Rows(nRows).Item("SessiUKM2")) Then
                    lblSessiUKM2.Text = MyTable.Rows(nRows).Item("SessiUKM2").ToString
                Else
                    lblSessiUKM2.Text = ""
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
        lblPusatUjian.Text = ""
        lblSessiUKM2.Text = ""
        lblTarikhUjian.Text = ""

        lblSchoolName.Text = ""
        lblSchoolAddress.Text = ""
        lblSchoolPostcode.Text = ""
        lblSchoolCity.Text = ""
        lblSchoolState.Text = ""

        pnlDisplay.Visible = False
        hyPDF.Visible = False
        btnPrint.Enabled = False
    End Sub

    Private Function IsLayak() As Boolean
        '--get StudentID
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)
        lblStudentID.Text = strStudentID

        '--display fullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        '--if exist LAYAK
        strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ConfigurationManager.AppSettings("UKM2ExamYear") & "'"
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

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrint.Click
        Dim msFileName As String = ""
        Dim msFilePath As String = ""

        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = "surat-tawaran-" & oCommon.getToday & "-" & txtMYKAD.Text & ".pdf"
            msFilePath = Server.MapPath(".") & "\cert_pdf\" & msFileName
            hyPDF.NavigateUrl = "cert_pdf/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("Jamain Johari (ARAKEN SDN BHD)")
            myDocument.AddSubject("Menamatkan Ujian Dalam Talian UKM1 " & ConfigurationManager.AppSettings("UKM2ExamYear"))

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

            Dim myPara As New Paragraph("Rujukan : UKM1.41/281/4", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara)

            Dim myPara2 As New Paragraph("Tarikh :" & oCommon.FormatDateDMY(Now), FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara2)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara3 As New Paragraph(lblStudentFullname.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara3)

            Dim myPara4 As New Paragraph(txtMYKAD.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara4.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara4)

            Dim myPara5 As New Paragraph(lblSchoolName.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara5)

            Dim myPara6 As New Paragraph(lblSchoolAddress.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara6.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara6)

            Dim myPara7 As New Paragraph(lblSchoolPostcode.Text & ", " & lblSchoolCity.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara7.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara7)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara8 As New Paragraph(lblStudentFullname.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara8.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara8)

            Dim myPara9 As New Paragraph("TAWARAN MENDUDUKI UJIAN SARINGAN KEDUA PENCARIAN BAKAT UKM2", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara9.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara9)

            Dim strBody As String = "Tahniah diucapkan kepada anda kerana ditawarkan untuk menduduki  Ujian UKM2.   Sila kemukakan surat tawaran ini kepada pihak sekolah  dan ibu bapa anda.  Sila pastikan, surat tawaran ini dan pengenalan diri yang sah seperti  mykad/mykid dan sijil kelahiran perlu dibawa dan dikemukakan kepada Pengawas  di pusat ujian. Kegagalan anda untuk membawa dokumen-dokumen tersebut mengakibatkan anda tidak dibenarkan menduduki ujian tersebut."
            Dim myPara10 As New Paragraph(strBody, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara10.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara10)

            myDocument.Add(Chunk.NEWLINE)
            Dim myPara11 As New Paragraph("Pusat ujian, tarikh dan masa adalah seperti berikut:", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara11.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11)

            Dim strPusatUjian As String = lblPusatUjian.Text.Replace("<br/>", ", ")
            Dim myPara12 As New Paragraph("Pusat Ujian :" & strPusatUjian, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara12.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara12)

            Dim myPara13 As New Paragraph("Tarikh Ujian :" & lblTarikhUjian.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara13.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara13)

            Dim myPara14 As New Paragraph("Waktu Ujian :" & lblSessiUKM2.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
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

            myDocument.Add(chnkLINE)
            Dim myPara18 As New Paragraph("Anda perlu tiba di pusat ujian, 30 minit sebelum sesi ujian dijalankan.", FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara18.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara18)

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
End Class