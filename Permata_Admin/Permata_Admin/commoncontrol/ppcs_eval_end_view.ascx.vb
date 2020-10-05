Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.IO

Imports RKLib.ExportData

Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ppcs_eval_end_view1
    Inherits System.Web.UI.UserControl

    Private rm As ResourceManager

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 0
    Dim strcourseCode As String
    Dim strDateCreated As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''--todays date
        strDateCreated = oCommon.getToday

        Try
            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.eval2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                '--load answers
                ppcs_eval_end_load()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub resetall_radiobutton()
        ''RESET
        Q1.SelectedValue = "0"

    End Sub

    Private Sub ppcs_eval_end_load()
        ''RESET
        resetall_radiobutton()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM PPCS_Eval_End WHERE StudentID='" & Request.QueryString("studentid") & "' AND classid=" & Request.QueryString("classid")
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q001")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q001").ToString
                Else
                    Q1.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q001Remarks")) Then
                    Q001Remarks.Text = MyTable.Rows(nRows).Item("Q001Remarks").ToString
                Else
                    Q001Remarks.Text = ""
                End If
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        '--debug
        lblSoalan.Text = rm.GetString("lblSoalan", ci)
        lblJawapan.Text = rm.GetString("lblJawapan", ci)

        lblQ001.Text = rm.GetString("ENDQ001", ci)

        '--answer string
        Q1.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q1.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q1.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q1.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q1.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q1.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q1.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q1.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q1.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q1.Items(9).Text = rm.GetString("AgreeLevel10", ci)

    End Sub


    Private Function ValidatePage() As Boolean
        If Q1.Text.Length = 0 Then
            lblMsgTop.Text = "Pilih markah antara 1 hingga 10."
            lblMsg.Text = "Pilih markah antara 1 hingga 10."
            Q1.Focus()
            Return False
        End If

        Return True
    End Function


    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try

            CreatePDF()

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error." & ex.Message

            '--write to file
            Dim strMsg As String = Now.ToString & ":" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub

    Private Sub CreatePDF()
        Dim msFileName As String
        Dim msFilePath As String

        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''--msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = Request.QueryString("studentid") & oCommon.getRandom & ".pdf"
            msFilePath = Server.MapPath("..") & "\cert_pdf\" & msFileName
            'hyPDF.NavigateUrl = "cert_pdf/" & msFileName
            hyPDF.NavigateUrl = "../cert_pdf/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("Jamain Johari (ARAKEN SDN BHD)")
            myDocument.AddSubject("Laporan Akhir PPCS")

            ''--drawline
            Dim imgdrawLine As String = Server.MapPath("..") & "\img\drawline.png"
            Dim imgLine As Image = Image.GetInstance(imgdrawLine)
            imgLine.Alignment = Image.LEFT_ALIGN  'left
            imgLine.Border = 0

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("..") & "\img\spacing.png"
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            ''--page header
            Dim imageHeader As String = Server.MapPath("..") & "\img\cert_header.png"
            Dim imgHeader As Image = Image.GetInstance(imageHeader)
            imgHeader.Alignment = Image.LEFT_ALIGN  'left
            imgHeader.Border = 1

            ''--loop thru records here
            ''--Dim Request.QueryString("studentid") As String = ""
            Dim strPPCSDate As String = ""
            Dim strClassCode As String = ""
            Dim strQ001Remarks As String = ""
            Dim strRespFullname As String = ""
            Dim strCourseCode As String = ""
            Dim strCourseNameBM As String = ""
            Dim strNamaPengajar As String = ""
            Dim strNamaPembantuPengajar As String = ""
            ''--photo
            Dim imgPhoto As Image


            ''--StudentID,ClassCode,Q001Remarks
            strSQL = "SELECT StudentFullname FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
            strRespFullname = oCommon.getFieldValue(strSQL)

            '--ClassCode
            strSQL = "SELECT ClassCode FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
            strClassCode = oCommon.getFieldValue(strSQL)

            '--CourseID
            strSQL = "SELECT CourseID FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
            Dim strCourseID As String = oCommon.getFieldValue(strSQL)

            '--CourseCode
            strSQL = "SELECT CourseCode FROM PPCS_Course WHERE CourseID=" & strCourseID
            strCourseCode = oCommon.getFieldValue(strSQL)

            '--CourseNameBM
            strSQL = "SELECT CourseNameBM FROM PPCS_Course WHERE CourseID=" & strCourseID
            strCourseNameBM = oCommon.getFieldValue(strSQL)

            '--NamaPengajar dlm kelas
            strSQL = "SELECT NamaPengajar FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
            strNamaPengajar = oCommon.getFieldValue(strSQL)

            '--NamaPembantuPengajar
            strSQL = "SELECT NamaPembantuPengajar FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
            strNamaPembantuPengajar = oCommon.getFieldValue(strSQL)

            ''--laporan akhir item
            strSQL = "SELECT Q001Remarks FROM PPCS_Eval_End WHERE StudentID='" & Request.QueryString("studentid") & "' AND classid=" & Request.QueryString("classid")
            strQ001Remarks = oCommon.getFieldValue(strSQL)

            ''--start here
            myDocument.Add(imgHeader)
            myDocument.Add(imgLine)

            ''--if photo exist
            strSQL = "SELECT StudentID FROM ukm1_respondent_photo WHERE StudentID='" & Request.QueryString("studentid") & "'"
            If oCommon.isExist(strSQL) = True Then
                imgPhoto = Image.GetInstance(LoadPhoto(Request.QueryString("studentid")))
                imgPhoto.Alignment = Image.LEFT_ALIGN  'left
                imgPhoto.ScaleAbsolute(60, 80)
                imgPhoto.Border = 0
                myDocument.Add(imgPhoto)
            End If

            myDocument.Add(New Paragraph("NAMA PELAJAR: " & strRespFullname, FontFactory.GetFont("Arial", 9, Font.BOLD)))
            myDocument.Add(New Paragraph("KURSUS: " & strCourseCode & " KELAS:" & strClassCode & " SESSI PPCS:" & strPPCSDate, FontFactory.GetFont("Arial", 9, Font.BOLD)))
            myDocument.Add(New Paragraph("TENAGA PENGAJAR: " & strNamaPengajar, FontFactory.GetFont("Arial", 9, Font.BOLD)))
            myDocument.Add(New Paragraph("PEMBANTU PENGAJAR: " & strNamaPembantuPengajar, FontFactory.GetFont("Arial", 9, Font.BOLD)))
            myDocument.Add(New Paragraph("LAPORAN AKHIR DICETAK PADA: " & Now.ToString("dddd dd/MM/yyyy"), FontFactory.GetFont("Arial", 9, Font.BOLD)))
            ''myDocument.Add(New Paragraph("LAPORAN AKHIR", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            myDocument.Add(imgLine)
            myDocument.Add(New Paragraph(strQ001Remarks, FontFactory.GetFont("Arial", 9, Font.NORMAL)))

            myDocument.Add(imgSpacing)
            myDocument.Add(imgLine)
            ''myDocument.Add(imgSpacing)

            ''myDocument.Add(New Paragraph("TANDATANGAN PENGAJAR: ", FontFactory.GetFont("Arial", 10, Font.BOLD)))

            ''myDocument.Add(imgSpacing)
            myDocument.Add(New Paragraph("Pusat PERMATApintar Negara", FontFactory.GetFont("Arial", 8, Font.NORMAL)))
            myDocument.Add(New Paragraph("Universiti Kebangsaan Malaysia, 43600 UKM Bangi, Selangor Darul Ehsan", FontFactory.GetFont("Arial", 8, Font.NORMAL)))
            myDocument.Add(New Paragraph("Telefon: +603-8921 7152/7153/7154  Faksimili: +603-8921 6642   E-mel: permatapintar@ukm.my", FontFactory.GetFont("Arial", 8, Font.NORMAL)))
            myDocument.Add(New Paragraph("Laman Web: http://www.permatapintar.edu.my", FontFactory.GetFont("Arial", 8, Font.NORMAL)))


            lblMsg.Text = "PDF siap dijana."
            hyPDF.Visible = True
            hyPDF.Text = "Klik disini untuk buka."
        Catch ex As DocumentException
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. " & ex.Message

        Catch ioe As IOException
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. " & ioe.Message.ToString

        Finally
            'Step 5: Remember to close the documnet
            myDocument.Close()

        End Try

    End Sub

    Private Function LoadPhoto(ByVal strValue As String) As Stream
        Dim conn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connection As SqlConnection = New SqlConnection(conn)

        Dim sql As String = "SELECT RespPhoto FROM ukm1_respondent_photo WHERE StudentID = @StudentID"
        Dim cmd As SqlCommand = New SqlCommand(sql, connection)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@StudentID", strValue)

        connection.Open()
        Dim img As Object = cmd.ExecuteScalar()
        Try
            Return New MemoryStream(CType(img, Byte()))
        Catch
            Return Nothing
        Finally
            connection.Close()
        End Try

    End Function

    Protected Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub

End Class