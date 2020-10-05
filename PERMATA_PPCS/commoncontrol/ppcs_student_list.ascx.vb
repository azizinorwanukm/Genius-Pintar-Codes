Imports System
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData

Imports iTextSharp.text
Imports iTextSharp.text.pdf

Partial Public Class ppcs_student_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")
    Dim strClassID As String
    Dim strTestID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                hyPDF.Visible = False
                '--terus load base on classID
                BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY b.StudentFullname"

        tmpSQL = "SELECT a.StudentID,b.StudentFullname,b.MYKAD,b.AlumniID,b.DOB_Year,a.PPCSStatus,a.PPCSCourse,a.PPCSClass,d.SchoolName,d.SchoolCity,d.SchoolState FROM PPCS a, StudentProfile b, StudentSchool c, SchoolProfile d"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.StudentID=c.StudentID and c.SchoolID=d.SchoolID AND a.PPCSStatus='LAYAK' AND c.IsLatest='Y' AND a.ClassID=" & Request.QueryString("classid")

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub


    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ExportToCSV()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV()
        'Get the data from database into datatable 
        Dim strQuery As String = getSQL()
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileExport.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

   
    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            '--Response.Write(strKeyID)
            Dim strMod As String = Request.QueryString("mod")
            Select Case strMod
                Case "01"
                    Response.Redirect("laporan.keseluruhan.view.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate") & "&courseid=" & Request.QueryString("courseid") & "&classid=" & Request.QueryString("classid"))
                Case "02"
                    Response.Redirect("laporan.keseluruhan.view.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate") & "&courseid=" & Request.QueryString("courseid") & "&classid=" & Request.QueryString("classid"))
                Case "03"
                    Response.Redirect("laporan.keseluruhan.view.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate") & "&courseid=" & Request.QueryString("courseid") & "&classid=" & Request.QueryString("classid"))
                Case "04"
                    Response.Redirect("laporan.keseluruhan.view.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate") & "&courseid=" & Request.QueryString("courseid") & "&classid=" & Request.QueryString("classid"))
                Case "10"
                    Response.Redirect("laporan.keseluruhan.view.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate") & "&courseid=" & Request.QueryString("courseid") & "&classid=" & Request.QueryString("classid"))
                Case Else
                    lblMsg.Text = "Invalid page module! " & strMod
            End Select

        Catch ex As Exception

        End Try

    End Sub


    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            Dim tableColumn As DataColumnCollection
            Dim tableRows As DataRowCollection

            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
            myDataAdapter.Fill(myDataSet, "PPCS_Eval_End")
            myDataAdapter.SelectCommand.CommandTimeout = 80000
            objConn.Close()

            '--transfer to an object
            tableColumn = myDataSet.Tables(0).Columns
            tableRows = myDataSet.Tables(0).Rows

            CreatePDF(tableColumn, tableRows)

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error." & ex.Message

            '--write to file
            Dim strMsg As String = Now.ToString & ":" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        End Try

    End Sub

    Private Sub CreatePDF(ByVal tableColumns As DataColumnCollection, ByVal tableRows As DataRowCollection)
        Dim msFileName As String
        Dim msFilePath As String

        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''--msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = "Laporan_Akhir_" & oCommon.getRandom & ".pdf"
            msFilePath = Server.MapPath("..") & "\cert_pdf\" & msFileName
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
            Dim imageHeader As String = Server.MapPath("..") & "\img\cert_header-new.png"
            Dim imgHeader As Image = Image.GetInstance(imageHeader)
            imgHeader.Alignment = Image.LEFT_ALIGN  'left
            imgHeader.Border = 1

            ''--loop thru records here
            Dim strTokenid As String = ""
            Dim strClassCode As String = ""
            Dim strClassID As String = ""
            Dim strQ001Remarks As String = ""
            Dim strStudentFullname As String = ""
            Dim strCourseCode As String = ""
            Dim strCourseID As String = ""
            Dim strCourseNameBM As String = ""
            Dim strNamaPengajar As String = ""
            Dim strNamaPembantuPengajar As String = ""

            ''--photo
            ''Dim imgPhoto As Image

            strSQL = "SELECT CourseCode FROM PPCS_Course WHERE CourseID=" & Request.QueryString("courseid")
            strCourseCode = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT ClassCode FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
            strClassCode = oCommon.getFieldValue(strSQL)

            Dim row As DataRow
            For Each row In tableRows
                Dim rowItems() As Object = row.ItemArray

                Dim strStudentID As String = ""

                ''--Tokenid,ClassCode,Q001Remarks
                strSQL = "SELECT StudentFullname FROM StudentProfile WHERE StudentID='" & rowItems(0).ToString() & "'"
                strStudentFullname = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT CourseNameBM FROM ppcs_course WHERE CourseID=" & Request.QueryString("courseid")
                strCourseNameBM = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT NamaPengajar FROM ppcs_class WHERE ClassID=" & Request.QueryString("classid")
                strNamaPengajar = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT NamaPembantuPengajar FROM ppcs_class WHERE ClassID=" & Request.QueryString("classid")
                strNamaPembantuPengajar = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Q001Remarks FROM PPCS_Eval_End WHERE StudentID='" & rowItems(0).ToString() & "' AND ClassID=" & Request.QueryString("classid")
                strQ001Remarks = oCommon.getFieldValue(strSQL)

                ''--start here
                myDocument.Add(imgHeader)
                myDocument.Add(imgLine)

                '' ''--if photo exist
                ''strSQL = "SELECT TokenID FROM ukm1_respondent_photo WHERE Tokenid='" & rowItems(0).ToString() & "'"
                ''If oCommon.isExist(strSQL) = True Then
                ''    imgPhoto = Image.GetInstance(LoadPhoto(rowItems(0).ToString()))
                ''    imgPhoto.Alignment = Image.LEFT_ALIGN  'left
                ''    imgPhoto.ScaleAbsolute(60, 80)
                ''    imgPhoto.Border = 0
                ''    myDocument.Add(imgPhoto)
                ''End If

                ''paragraph.setAlignment( Element.ALIGN_CENTER );


                Dim myPara01 As New Paragraph("PENILAIAN AKHIR", FontFactory.GetFont("Arial", 10, Font.BOLD))
                myPara01.Alignment = Element.ALIGN_CENTER
                myDocument.Add(myPara01)

                Dim myPara02 As New Paragraph("Program Perkhemahan Cuti Sekolah PERMATApintar Negara UKM-JHU-CTY " & Request.QueryString("year"), FontFactory.GetFont("Arial", 10, Font.BOLD))
                myPara02.Alignment = Element.ALIGN_CENTER
                myDocument.Add(myPara02)

                myDocument.Add(imgSpacing)
                ''
                myDocument.Add(New Paragraph("NAMA PELAJAR: " & strStudentFullname, FontFactory.GetFont("Arial", 9, Font.BOLD)))
                myDocument.Add(New Paragraph("KURSUS: " & strCourseNameBM & "  " & strClassCode, FontFactory.GetFont("Arial", 9, Font.BOLD)))
                myDocument.Add(New Paragraph("TENAGA PENGAJAR: " & strNamaPengajar, FontFactory.GetFont("Arial", 9, Font.BOLD)))
                myDocument.Add(New Paragraph("PEMBANTU PENGAJAR: " & strNamaPembantuPengajar, FontFactory.GetFont("Arial", 9, Font.BOLD)))
                ''myDocument.Add(New Paragraph("LAPORAN AKHIR", FontFactory.GetFont("Arial", 9, Font.BOLD)))
                myDocument.Add(imgLine)
                myDocument.Add(New Paragraph(strQ001Remarks, FontFactory.GetFont("Arial", 9, Font.NORMAL)))

                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(New Paragraph("TANDATANGAN PENGAJAR   .............................................................    TARIKH: ......................... ", FontFactory.GetFont("Arial", 10, Font.BOLD)))

                ''myDocument.Add(imgSpacing)
                myDocument.Add(imgLine)
                ''myDocument.Add(imgSpacing)

                ''myDocument.Add(imgSpacing)
                myDocument.Add(New Paragraph("Pusat PERMATApintar™ Negara", FontFactory.GetFont("Arial", 8, Font.NORMAL)))
                myDocument.Add(New Paragraph("Universiti Kebangsaan Malaysia, 43600 UKM Bangi, Selangor Darul Ehsan", FontFactory.GetFont("Arial", 8, Font.NORMAL)))
                myDocument.Add(New Paragraph("Tel: +603-8921 7503 / 7521 / 7532/ 7533  Faksimili: +603-8921 7525   E-mel: permatapintar@ukm.edu.my", FontFactory.GetFont("Arial", 8, Font.NORMAL)))
                myDocument.Add(New Paragraph("Laman Web: http://www.ukm.my/permatapintar", FontFactory.GetFont("Arial", 8, Font.NORMAL)))

                myDocument.NewPage()
                ''--content end
            Next

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


End Class