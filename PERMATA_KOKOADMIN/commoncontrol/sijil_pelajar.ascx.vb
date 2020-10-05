Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class sijil_pelajar
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsgTop.Text = ""

                koko_tahun_list()
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

                koko_kelas_list()
                ddlKelas.Text = "ALL"

                '--default list
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Private Sub koko_tahun_list()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

            ' ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub koko_kelas_list()
        strSQL = "SELECT * FROM koko_kelas WHERE Tahun='" & ddlTahun.SelectedValue & "' ORDER BY Kelas ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "Kelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            'ddlKelas.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        '--MARKAH PELAJAR
        tmpSQL = "SELECT koko_pelajar.kokopelajarid,koko_pelajar.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,"
        tmpSQL += " koko_pelajar.Tahun, koko_kelas.Kelas,koko_pelajar.Gred" & selPeperiksaan.Value & " as Gred,koko_pelajar.KOKO" & selPeperiksaan.Value & " as KOKO"
        tmpSQL += " FROM koko_pelajar LEFT OUTER Join StudentProfile On koko_pelajar.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID"

        If Not ddlTahun.Text = "ALL" Then
            strWhere = " WHERE koko_pelajar.Tahun='" & ddlTahun.Text & "'"
        End If
        ' strWhere += " AND koko_pelajar." & Request.QueryString("field") & "=" & Request.QueryString("value")
        If Not ddlKelas.Text = "ALL" Then
            strWhere += " AND koko_pelajar.KelasID='" & ddlKelas.SelectedValue & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        strSQL = "SELECT MYKAD FROM StudentProfile WHERE StudentID='" & strKeyID & "'"
        Dim strMYKAD As String = oCommon.getFieldValue(strSQL)
        Dim strFilename As String = "slip/KOKO-" & ddlTahun.SelectedItem.Text & "-" & strMYKAD & ".pdf"

        'Response.Redirect(strFilename)

        Dim myurl As String = strFilename
        Dim script As String = (Convert.ToString("(window.open('") & myurl) + "',null,'location=0,toolbar=0,status=0,scrollbars=1,resizable=1,width=800,Height=600'));"

        ScriptManager.RegisterStartupScript(Me, [GetType](), "PopUp", script, True)
    End Sub

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
    Protected Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        '--default list
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnGenSijil_Click(sender As Object, e As EventArgs) Handles btnGenSijil.Click
        Dim strStudentID As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQL)
        Dim dt As DataTable = GetData(cmd)

        For i As Integer = 0 To dt.Rows.Count - 1
            strStudentID = dt.Rows(i)("StudentID").ToString()
            getStudentProfile(strStudentID)
            GenerateSijil()
        Next


    End Sub

    Private Sub getStudentProfile(ByVal strKey As String)
        lblStudentID.Text = strKey

        strSQL = "SELECT StudentFullname FROM StudentProfile WHERE StudentID='" & strKey & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT MYKAD FROM StudentProfile WHERE StudentID='" & strKey & "'"
        lblMYKAD.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub GenerateSijil()
        Dim msFileName As String
        Dim msFilePath As String

        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)

        Try
            ''msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = "KOKO-" & ddlTahun.SelectedItem.Text & "-" & lblMYKAD.Text & ".pdf"
            msFilePath = Server.MapPath(".") & "\slip\" & msFileName
            hyPDF.NavigateUrl = "../admin/slip/" & msFileName

            'Step 2: Now create a writer that listens to this doucment and writes the document to desired Stream.
            PdfWriter.GetInstance(myDocument, New FileStream(msFilePath, FileMode.Create))

            'Step 3: Open the document now using
            myDocument.Open()
            myDocument.AddTitle("PERMATApintar")
            myDocument.AddAuthor("(ARAKEN SDN BHD)")
            myDocument.AddSubject("Menamatkan Ujian Dalam Talian UKM2 " & Request.QueryString("examyear"))

            'Step 4: Now add some contents to the document

            '-----HEADER START add image
            Dim imageFile As String = Server.MapPath(".") & "\img\logokpm.jpg"
            Dim jpeg As Image = Image.GetInstance(imageFile)
            jpeg.Alignment = Image.LEFT_ALIGN  'left
            myDocument.Add(jpeg)

            ''-----HORIZONTAL LINE
            ''To end the section with a dotted line
            'Dim sepLINE As New iTextSharp.text.pdf.draw.LineSeparator
            'sepLINE.LineWidth = 1
            'Dim chnkLINE As New iTextSharp.text.Chunk(sepLINE)
            'myDocument.Add(chnkLINE)

            '-----CONTENT HEADER START
            Dim myHeader1 As New Paragraph("Name                    : " & lblStudentFullname.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myHeader1.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myHeader1)

            Dim myHeader2 As New Paragraph("NRIC                     : " & lblMYKAD.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myHeader2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myHeader2)

            'To end the section with a dotted line
            Dim sepLINE1 As New iTextSharp.text.pdf.draw.LineSeparator
            sepLINE1.LineWidth = 1
            Dim chnkLINE1 As New iTextSharp.text.Chunk(sepLINE1)
            myDocument.Add(chnkLINE1)

            '-----SIJIL START
            '--Year
            Dim myContent1 As New Paragraph("Year                      : " & ddlTahun.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myContent1.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myContent1)

            '--Class
            Dim myContent2 As New Paragraph("Class                    : " & ddlKelas.SelectedItem.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myContent2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myContent2)

            ''--get Koko Mark
            Dim strKoko As String = ""
            strSQL = "SELECT koko_pelajar.KOKO" & selPeperiksaan.Value & " AS KOKO FROM koko_pelajar WHERE StudentID='" & lblStudentID.Text & "' And koko_pelajar.Tahun ='" & ddlTahun.Text & "'"
            strKoko = oCommon.getFieldValue(strSQL)
            strKoko = oCommon.DoConvertN(strKoko, 1)

            '--Curricular Mark. satu decimal sahaja
            Dim myContent3 As New Paragraph("Curricular Mark   : " & strKoko, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myContent3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myContent3)

            ''--get GRADE
            Dim strGred As String = ""
            strSQL = "SELECT koko_pelajar.Gred" & selPeperiksaan.Value & " as Gred FROM koko_pelajar WHERE StudentID='" & lblStudentID.Text & "' And koko_pelajar.Tahun ='" & ddlTahun.Text & "'"
            strGred = oCommon.getFieldValue(strSQL)

            '--Grade
            Dim myContent4 As New Paragraph("Grade                   : " & strGred, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myContent4.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myContent4)


            ''To end the section with a dotted line
            'Dim sepLINE2 As New iTextSharp.text.pdf.draw.LineSeparator
            'sepLINE2.LineWidth = 1
            'Dim chnkLINE2 As New iTextSharp.text.Chunk(sepLINE2)
            'myDocument.Add(chnkLINE2)

            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)

            '-----FOOTER START
            myDocument.Add(Chunk.NEWLINE)
            Dim myFooter1 As New Paragraph("Certified by:", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myFooter1.Alignment = 1
            myDocument.Add(myFooter1)
            myDocument.Add(Chunk.NEWLINE)
            myDocument.Add(Chunk.NEWLINE)

            Dim myFooter2 As New Paragraph("..........................", FontFactory.GetFont("Arial", 18, Font.BOLD))
            myFooter2.Alignment = 1
            myDocument.Add(myFooter2)

            Dim myFooter3 As New Paragraph("Prof. Datuk. Dr. Noriah Mohd Ishak", FontFactory.GetFont("Arial", 14, Font.BOLD))
            myFooter3.Alignment = 1
            myDocument.Add(myFooter3)

            Dim myFooter4 As New Paragraph("Director", FontFactory.GetFont("Arial", 14, Font.BOLD))
            myFooter4.Alignment = 1
            myDocument.Add(myFooter4)

            myDocument.Add(Chunk.NEWLINE)
            Dim myFooter5 As New Paragraph("Pusat PERMATApintar Negara", FontFactory.GetFont("Arial", 14, Font.BOLD))
            myFooter5.Alignment = 1
            myDocument.Add(myFooter5)

            Dim myFooter6 As New Paragraph("Universiti Kebangsaan Malaysia", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myFooter6.Alignment = 1
            myDocument.Add(myFooter6)

            Dim myFooter7 As New Paragraph("43600 Bangi, Selangor, Malaysia", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myFooter7.Alignment = 1
            myDocument.Add(myFooter7)

            Dim myFooter8 As New Paragraph("Tel: +603-8921 7503  Fax: +603-8921 7525", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myFooter8.Alignment = 1
            myDocument.Add(myFooter8)

            Dim myFooter9 As New Paragraph("E-Mail: permatapintar@ukm.edu.my", FontFactory.GetFont("Arial", 14, Font.NORMAL))
            myFooter9.Alignment = 1
            myDocument.Add(myFooter9)

            'myDocument.Add(chnkLINE)
            'Dim myFooter10 As New Paragraph("(Sijil ini janaan komputer dan tidak perlu tandatangan)", FontFactory.GetFont("Arial", 10, Font.ITALIC))
            'myFooter10.Alignment = 1
            'myDocument.Add(myFooter10)

            '----next page
            myDocument.NewPage()

            '-----HEADER START add image
            Dim imageFileP2 As String = Server.MapPath(".") & "\img\logokpm.jpg"
            Dim jpegP2 As Image = Image.GetInstance(imageFileP2)
            jpegP2.Alignment = Image.LEFT_ALIGN  'left
            myDocument.Add(jpegP2)


            '--Header Page2
            '-----CONTENT HEADER START
            Dim myHeaderP2_1 As New Paragraph("Name                    : " & lblStudentFullname.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myHeaderP2_1.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myHeaderP2_1)

            Dim myHeaderP2_2 As New Paragraph("NRIC                     : " & lblMYKAD.Text, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myHeaderP2_2.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myHeaderP2_2)

            'To end the section with a dotted line
            Dim sepLINEP2_1 As New iTextSharp.text.pdf.draw.LineSeparator
            sepLINEP2_1.LineWidth = 1
            Dim chnkLINEP2_1 As New iTextSharp.text.Chunk(sepLINEP2_1)
            myDocument.Add(chnkLINEP2_1)

            Dim myHeaderP2_3 As New Paragraph("PENCAPAIAN", FontFactory.GetFont("Arial", 14, Font.BOLD))
            myHeaderP2_3.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myHeaderP2_3)

            ''--get Pencapaian
            Dim strPencapaian As String = ""
            strSQL = "SELECT koko_pelajar.Pencapaian FROM koko_pelajar WHERE StudentID='" & lblStudentID.Text & "' And koko_pelajar.Tahun ='" & ddlTahun.Text & "'"
            strPencapaian = oCommon.getFieldValue(strSQL)

            Dim myContentP2_1 As New Paragraph(strPencapaian, FontFactory.GetFont("Arial", 14, Font.BOLD))
            myContentP2_1.Alignment = Element.ALIGN_LEFT
            myDocument.Add(myContentP2_1)

            'Response.Write("Succesfully create the PDF")
            lblMsg.Text = "PDF fail siap dijana."
            'hyPDF.Visible = True
            'hyPDF.Text = "Klik disini untuk buka."
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

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        koko_kelas_list()

    End Sub

End Class