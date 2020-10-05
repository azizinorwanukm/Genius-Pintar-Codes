Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Public Class saringan
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                pnlDisplay.Visible = False
                lblExamYear.Text = ConfigurationManager.AppSettings("MYBRAINExamYear")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSemak_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSemak.Click
        '--check if allow publish or not
        If ConfigurationManager.AppSettings("SARINGANResult") = "N" Then
            lblMsg.Text = "Keputusan SARINGAN belum dimuktamadkan lagi!"
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
            UKM2_view()
            PusatUjian_view()

            '--Dr Siti 20131017. Benarkan pelajar tukar status terima/tolak sebelum 1 Nov
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "TAHNIAH! Anda LAYAK ke Ujian Saringan Biasiswa MyBrainSc dan ASASIpintar bagi Tahun " & ConfigurationManager.AppSettings("MYBRAINExamYear") & "."
            lblMsgTop.Text = lblMsg.Text
            btnPrint.Visible = True
            pnlDisplay.Visible = True

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Anda tidak berjaya ke Ujian Saringan Biasiswa MyBrainSc dan ASASIpintar Tahun " & ConfigurationManager.AppSettings("MYBRAINExamYear") & "."
            lblMsgTop.Text = lblMsg.Text
            btnPrint.Visible = False
            pnlDisplay.Visible = False
        End If

    End Sub

    Private Sub UKM2_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT PusatCode,TarikhUjian,SessiUKM2 FROM UKM2"
        strWhere = " WHERE StudentID='" & lblStudentID.Text & "' AND ExamYear='" & ConfigurationManager.AppSettings("MYBRAINExamYear") & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PusatCode")) Then
                    lblPusatCode.Text = ds.Tables(0).Rows(0).Item("PusatCode")
                Else
                    lblPusatCode.Text = ""
                End If

                '--TarikhUjian
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("TarikhUjian")) Then
                    lblTarikhUjian.Text = ds.Tables(0).Rows(0).Item("TarikhUjian")
                Else
                    lblTarikhUjian.Text = ""
                End If

                '--reformat to DD-MM-YYYY
                If Not lblTarikhUjian.Text.Length = 0 Then
                    lblTarikhUjian.Text = oCommon.FormatDateDDMMYYYY(lblTarikhUjian.Text)
                End If

                '--SessiUKM2
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SessiUKM2")) Then
                    lblSessiUKM2.Text = ds.Tables(0).Rows(0).Item("SessiUKM2")
                Else
                    lblSessiUKM2.Text = ""
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

        '--belum ada pusat ujian
        If lblPusatCode.Text.Length = 0 Then
            lblPusatName.Text = "Pusat Ujian belum lagi ditetapkan.<br/>Sila hubungi Pusat PEMATApintar Negara."
            Exit Sub
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT * FROM PusatUjian"
        strWhere = " WHERE PusatCode='" & lblPusatCode.Text & "'"
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
                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatName")) Then
                    lblPusatName.Text = MyTable.Rows(nRows).Item("PusatName").ToString
                Else
                    lblPusatName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatAddress")) Then
                    lblPusatAddress.Text = MyTable.Rows(nRows).Item("PusatAddress").ToString
                Else
                    lblPusatAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatPostcode")) Then
                    lblPusatPostcode.Text = MyTable.Rows(nRows).Item("PusatPostcode").ToString
                Else
                    lblPusatPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatCity")) Then
                    lblPusatCity.Text = MyTable.Rows(nRows).Item("PusatCity").ToString
                Else
                    lblPusatCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatState")) Then
                    lblPusatState.Text = MyTable.Rows(nRows).Item("PusatState").ToString
                Else
                    lblPusatState.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatNoTel")) Then
                    lblPusatNoTel.Text = MyTable.Rows(nRows).Item("PusatNoTel").ToString
                Else
                    lblPusatNoTel.Text = ""
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

        lblPusatCode.Text = ""
        lblPusatName.Text = ""
        lblPusatAddress.Text = ""
        lblPusatPostcode.Text = ""
        lblPusatCity.Text = ""
        lblPusatState.Text = ""
        lblPusatNoTel.Text = ""

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

        strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ConfigurationManager.AppSettings("MYBRAINExamYear") & "'"
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
            myDocument.AddSubject("TAHNIAH! Anda LAYAK ke Ujian Saringan Biasiswa MyBrainSc dan ASASIpintar Tahun " & ConfigurationManager.AppSettings("MYBRAINExamYear"))

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

            Dim myPara As New Paragraph("Rujukan : UKM1.41/281/4", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara)

            Dim myPara1 As New Paragraph("Tarikh :" & oCommon.FormatDateDMY(Now), FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara1.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara1)
            myDocument.Add(Chunk.NEWLINE)

            Dim myPara2 As New Paragraph("UJIAN SARINGAN BIASISWA MYBRAINSC DAN ASASIpintar." & vbCrLf & "TAHUN UJIAN: " & lblExamYear.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara2)

            myDocument.Add(chnkLINE)
            Dim myPara3 As New Paragraph("NAMA:" & lblStudentFullname.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara3)

            Dim myPara4 As New Paragraph("MYKAD#:" & txtMYKAD.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara4.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara4)
            myDocument.Add(Chunk.NEWLINE)

            '--pusat ujian
            Dim myPara5 As New Paragraph("PUSAT UJIAN", FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara5.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara5)

            Dim myPara6 As New Paragraph(lblPusatName.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara6.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara6)

            Dim myPara7 As New Paragraph(lblPusatAddress.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara7.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara7)

            Dim myPara8 As New Paragraph(lblPusatPostcode.Text & ", " & lblPusatCity.Text & ", " & lblPusatState.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara8.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara8)

            Dim myPara9 As New Paragraph("Tel#:" & lblPusatNoTel.Text, FontFactory.GetFont("Arial", 12, Font.NORMAL))
            myPara9.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara9)

            '-----bila ujian
            myDocument.Add(Chunk.NEWLINE)
            Dim myPara11 As New Paragraph("Tarikh Ujian: " & lblTarikhUjian.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara11.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara11)

            Dim myPara12 As New Paragraph("Sessi: " & lblSessiUKM2.Text, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara12.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara12)

            myDocument.Add(chnkLINE)
            Dim myPara15 As New Paragraph("Sesi PAGI : 8:00 pagi - 11:00 pagi", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara15.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara15)

            Dim myPara16 As New Paragraph("Sesi TENGAHARI : 11:00 pagi - 2:00 petang", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara16.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara16)

            Dim myPara17 As New Paragraph("Sesi PETANG : 2:00 petang - 5:00 petang. Jumaat: 2.30 petang - 5.30 petang", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara17.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara17)
            myDocument.Add(chnkLINE)

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
            hyPDF.Text = "Klik disini untuk buka PDF."

        Catch ex As DocumentException
            myDocument.Close()
            '--display on screen
            lblMsg.Text = "System Error:" & ex.Message

        Catch ioe As IOException
            myDocument.Close()
            '--display on screen
            lblMsg.Text = "System Error:" & ioe.Message

        Finally
            'Step 5: Remember to close the documnet
            myDocument.Close()

        End Try

    End Sub


End Class