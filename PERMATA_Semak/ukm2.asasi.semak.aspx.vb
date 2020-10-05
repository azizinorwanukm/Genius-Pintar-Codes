Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ukm2_asasi_semak
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'btnTerima.Attributes.Add("onclick", "return confirm('Pasti ingin MENERIMA tawaran ini?');")
        'btnTolak.Attributes.Add("onclick", "return confirm('Pasti ingin MENOLAK tawaran ini? Masukkan sebab kenapa tawaran ini ditolak.');")

        Try
            If Not IsPostBack Then
                If oCommon.getAppsettings("SEMAK_ASASI") = "N" Then
                    Response.Redirect("default.aspx")
                End If

                lblUKM2ExamYear.Text = oCommon.getAppsettings("UKM2ExamYear")
                lblUKM2End.Text = oCommon.getAppsettings("UKM2End")
                lblUKM2Title.Text = oCommon.getAppsettings("UKM2Title")
                pnlDisplay.Visible = False

                ClearScreen()
            End If

        Catch ex As Exception

        End Try

    End Sub



    Private Sub ClearScreen()
        lblStudentFullname.Text = ""
        lblStudentID.Text = ""

        lblPusatCode.Text = ""
        lblPusatName.Text = ""
        lblPusatAddress.Text = ""
        lblPusatPostcode.Text = ""
        lblPusatCity.Text = ""
        lblPusatState.Text = ""
        lblPusatNoTel.Text = ""

        lblStudentAddress1.Text = ""
        lblStudentAddress2.Text = ""
        lblStudentPostcode.Text = ""
        lblStudentCity.Text = ""
        lblStudentState.Text = ""

        pnlDisplay.Visible = False
        hyPDF.Visible = False

        lblMsgTop.Text = ""

    End Sub

    Protected Sub btnSemak_Click(sender As Object, e As EventArgs) Handles btnSemak.Click
        '--check if allow publish or not
        If oCommon.getAppsettings("UKM2Result") = "N" Then
            lblMsg.Text = "Keputusan " & lblUKM2Title.Text & " belum dimuktamadkan lagi!"
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        '--remove text on board
        ClearScreen()

        If ValidatePage() = False Then
            divMsg.Attributes("class") = "warning"
            Exit Sub
        End If

        If IsLayak() = True Then
            Studentprofile_view()
            UKM2_view()
            PusatUjian_view()

            '--Dr Siti 20131017. Benarkan pelajar tukar status terima/tolak sebelum 1 Nov
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "TAHNIAH! Anda BERJAYA ke " & oCommon.getAppsettings("UKM2Title")
            lblMsgTop.Text = lblMsg.Text
            btnPrint.Visible = True
            pnlDisplay.Visible = True

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Anda tidak berjaya ke " & oCommon.getAppsettings("UKM2Title")
            lblMsgTop.Text = lblMsg.Text
            btnPrint.Visible = False
            pnlDisplay.Visible = False
        End If

    End Sub

    Private Sub Studentprofile_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT StudentAddress1,StudentAddress2,StudentPostcode,StudentCity,StudentState FROM StudentProfile"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentAddress1")) Then
                    lblStudentAddress1.Text = ds.Tables(0).Rows(0).Item("StudentAddress1")
                Else
                    lblStudentAddress1.Text = ""
                End If

                '--TarikhUjian
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentAddress2")) Then
                    lblStudentAddress2.Text = ds.Tables(0).Rows(0).Item("StudentAddress2")
                Else
                    lblStudentAddress2.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentPostcode")) Then
                    lblStudentPostcode.Text = ds.Tables(0).Rows(0).Item("StudentPostcode")
                Else
                    lblStudentPostcode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentCity")) Then
                    lblStudentCity.Text = ds.Tables(0).Rows(0).Item("StudentCity")
                Else
                    lblStudentCity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentState")) Then
                    lblStudentState.Text = ds.Tables(0).Rows(0).Item("StudentState")
                Else
                    lblStudentState.Text = ""
                End If

            End If
        Catch ex As Exception
            lblMsg.Text = "Studentprofile_view Err:" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub UKM2_view()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        tmpSQL = "SELECT PusatCode,TarikhUjian,SessiUKM2 FROM UKM2"
        strWhere = " WHERE StudentID='" & lblStudentID.Text & "' AND ExamYear='" & oCommon.getAppsettings("UKM2ExamYear") & "'"
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
            lblPusatName.Text = "Pusat Ujian belum ditetapkan lagi. Sila hubungi Pusat PEMATApintar Negara."
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

    Private Function IsLayak() As Boolean
        strSQL = "SELECT StudentID FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strSQL)
        lblStudentID.Text = strStudentID

        '--display fullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & oCommon.getAppsettings("UKM2ExamYear") & "'"
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
            msFileName = "2016-ASASI-" & txtMYKAD.Text & ".pdf"
            msFilePath = Server.MapPath(".") & "\cert_pdf\" & msFileName
            hyPDF.NavigateUrl = "cert_pdf/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("Jamain Johari (ARAKEN SDN BHD)")
            myDocument.AddSubject("TAHNIAH! Anda BERJAYA ke " & lblUKM2Title.Text)

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

            'Dim myPara1 As New Paragraph("Rujukan : UKM1.41/344/2", FontFactory.GetFont("Arial", 10, Font.BOLD))
            'myPara1.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myPara1)
            'myDocument.Add(Chunk.NEWLINE)

            Dim myPara2 As New Paragraph(lblStudentFullname.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara2)

            Dim myAddress As New Paragraph(lblStudentAddress1.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myAddress.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myAddress)

            Dim myAddress1 As New Paragraph(lblStudentAddress2.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myAddress1.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myAddress1)

            Dim myAddress3 As New Paragraph(lblStudentPostcode.Text & " " & lblStudentCity.Text & ", " & lblStudentState.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myAddress3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myAddress3)
            myDocument.Add(Chunk.NEWLINE)

            'Dim myAddress4 As New Paragraph(lblStudentState.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            'myAddress4.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myAddress4)
            'myDocument.Add(Chunk.NEWLINE)

            Dim myPara8 As New Paragraph("Saudara/ Saudari,", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myPara8.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myPara8)
            myDocument.Add(Chunk.NEWLINE)

            Dim myTitle As New Paragraph(lblUKM2Title.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myTitle.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myTitle)
            myDocument.Add(Chunk.NEWLINE)


            '--body
            Dim strBody As String = "Adalah dengan sukacitanya dimaklumkan bahawa saudara/i dikehendaki untuk hadir mengikuti ujian UKM2 UKM-ASASIpintar bagi kemasukan ke Program ASASIpintar UKM sesi akademik 2016 – 2017. Ujian UKM2 UKM-ASASIpintar ini adalah ujian atas talian dan wajib bagi pelajar yang ingin meneruskan pengajian ke ASASIpintar UKM. Saudara/i dikehendaki untuk membuat pembayaran sebanyak RM 20.00 sebagai yuran pemprosesan ujian ini.  Sila rujuk Lampiran 1 untuk Tatacara Pembayaran Yuran."
            Dim myBody1 As New Paragraph(strBody, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myBody1.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myBody1)
            myDocument.Add(Chunk.NEWLINE)

            strBody = "Tarikh pembayaran adalah bermula dari 17 April 2016 sehingga 27 April 2016  atau SEHARI SEBELUM tarikh menduduki ujian UKM2 UKM-ASASIpintar. Sila bawa bukti resit slip pembayaran asal beserta dengan Lampiran 2 iaitu Akaun Pembayaran Yuran semasa pendaftaran ujian UKM2 UKM-ASASIpintar tersebut. Saudara/i TIDAK AKAN DIBENARKAN untuk menduduki ujian UKM2 UKM-ASASIpintar ini jika saudara/i tidak membawa bukti slip pembayaran beserta dengan Lampiran 2."
            Dim myBody2 As New Paragraph(strBody, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myBody2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myBody2)
            myDocument.Add(Chunk.NEWLINE)

            strBody = "Saudara/i juga dikehendaki berpakaian kemas dan membawa kad pengenalan diri semasa menghadiri ujian UKM2 UKM-ASASIpintar ini. Sebarang pertanyaan lanjut boleh berhubung terus dengan Puan Siti Hawa bt Abu Samah di atas talian 03-89217513 / 03-89217512 atau melalui email ewa@ukm.edu.my"
            Dim myBody3 As New Paragraph(strBody, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myBody3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myBody3)
            myDocument.Add(Chunk.NEWLINE)

            '--ujian
            Dim myUjian1 As New Paragraph("Tarikh Ujian: " & lblTarikhUjian.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myUjian1.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myUjian1)

            Dim myUjian2 As New Paragraph("Sesi: " & lblSessiUKM2.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myUjian2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myUjian2)
            myDocument.Add(Chunk.NEWLINE)

            Dim myUjian3 As New Paragraph("Nama Pusat Ujian: " & lblPusatName.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myUjian3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myUjian3)

            'Dim myUjian4 As New Paragraph("Alamat: ", FontFactory.GetFont("Arial", 10, Font.NORMAL))
            'myUjian4.Alignment = Element.ALIGN_LEFT
            'myDocument.Add(myUjian4)

            Dim myUjian5 As New Paragraph("Alamat:" & lblPusatAddress.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myUjian5.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myUjian5)

            Dim myUjian6 As New Paragraph(lblPusatPostcode.Text & " " & lblPusatCity.Text & ", " & lblPusatState.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myUjian6.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myUjian6)

            Dim myUjian7 As New Paragraph("Tel#:" & lblPusatNoTel.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myUjian7.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myUjian7)
            myDocument.Add(Chunk.NEWLINE)

            '----
            strBody = "Sekian. Kehadiran saudara/i dalam sesi ujian UKM2 tersebut didahului dengan ucapan ribuan terima kasih."
            Dim myBody4 As New Paragraph(strBody, FontFactory.GetFont("Arial", 10, Font.NORMAL))
            myBody4.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myBody4)
            myDocument.Add(Chunk.NEWLINE)


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