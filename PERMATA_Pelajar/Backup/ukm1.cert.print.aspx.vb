Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Public Class ukm1_cert_print
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
                ExamYear.Text = Request.QueryString("examyear")

                StudentProfile_load()
                StudentSchool_load()

                ''exam end and exam year
                'strSQL = "SELECT ExamEnd FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                'ExamEnd.Text = oCommon.getFieldValue(strSQL)
                'If ExamEnd.Text.Length = 0 Then
                '    ExamEnd.Text = "[Tiada rekod. Tidak mengambil ujian.]"
                'End If
                ExamYear.Text = Request.QueryString("examyear")
            End If
        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        End Try

    End Sub

    Private Sub StudentProfile_load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT MYKAD,StudentFullname FROM StudentProfile WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "'"
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
        strSQL = "SELECT SchoolID FROM StudentSchool WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' ORDER BY StudentSchoolID DESC"
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

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = Request.QueryString("studentid") & "-" & ExamYear.Text & ".pdf"
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
            Dim imageFile As String = Server.MapPath(".") & "\pic\logosijilv2.png"
            Dim jpeg As Image = Image.GetInstance(imageFile)
            jpeg.Alignment = Image.LEFT_ALIGN  'left
            jpeg.Border = 1
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

            myDocument.Add(New Paragraph("Pusat PERMATApintar Negara", FontFactory.GetFont("Arial", 12, Font.BOLD)))
            myDocument.Add(New Paragraph("Universiti Kebangsaan Malaysia"))
            myDocument.Add(New Paragraph("43600 Bangi, Selangor, Malaysia"))
            myDocument.Add(New Paragraph("Tel: +603-8921 7503"))
            myDocument.Add(New Paragraph("Fax: +603-8921 7525"))
            myDocument.Add(New Paragraph("E-Mail: permatapintar@ukm.my"))

            myDocument.Add(chnkLINE)
            'myDocument.Add(New Paragraph("----------------------------------------------------------------------------------------------------------------------------------"))
            myDocument.Add(New Paragraph(40, "Dengan ini mengesahkan bahawa"))
            myDocument.Add(New Paragraph(20, "NAMA PELAJAR: " & StudentFullname.Text, FontFactory.GetFont("Arial", 12, Font.BOLD)))
            myDocument.Add(New Paragraph("MYKAD/MYKID#: " & MYKAD.Text, FontFactory.GetFont("Arial", 12, Font.BOLD)))
            myDocument.Add(New Paragraph("NAMA SEKOLAH: " & SchoolName.Text, FontFactory.GetFont("Arial", 12, Font.BOLD)))
            myDocument.Add(New Paragraph("NEGERI: " & SchoolState.Text, FontFactory.GetFont("Arial", 12, Font.BOLD)))
            myDocument.Add(New Paragraph(20, "telah menamatkan Ujian Dalam Talian UKM1" & ExamEnd.Text))
            myDocument.Add(New Paragraph(""))
            'myDocument.Add(New Paragraph(40, "----------------------------------------------------------------------------------------------------------------------------------"))
            myDocument.Add(chnkLINE)
            myDocument.Add(New Paragraph("Ujian Dalam Talian UKM1 TAHUN " & ExamYear.Text, FontFactory.GetFont("Arial", 14, Font.BOLD)))
            myDocument.Add(New Paragraph("FAIL RUJUKAN: " & msFileName, FontFactory.GetFont("Arial", 8, Font.NORMAL)))

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

End Class