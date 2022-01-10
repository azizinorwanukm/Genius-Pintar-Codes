Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf


Public Class homeroom_assessment_remark
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ddlyear_info()
                ddlLevel_info()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlyear_info()

        strSQL = "select class_year from class_info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim years_DS As DataSet = New DataSet
            sqlDA.Fill(years_DS, "AnyTable")

            ddlyear.DataSource = years_DS
            ddlyear.DataTextField = "class_year"
            ddlyear.DataValueField = "class_year"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlLevel_info()
        Try
            strSQL = "Select class_Level from class_info where class_year = '" & ddlyear.SelectedValue & "' and stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim exam_DS As DataSet = New DataSet
            sqlDA.Fill(exam_DS, "ExamTable")

            ddlLevel.DataSource = exam_DS
            ddlLevel.DataValueField = "class_Level"
            ddlLevel.DataTextField = "class_Level"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlclass_info()
        Try
            Dim exam As String = "select * from class_info
                                  where class_info.class_year = '" & ddlyear.SelectedValue & "'
                                  and class_info.class_Level = '" & ddlLevel.SelectedValue & "'
                                  and class_info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
            Dim examDA As New SqlDataAdapter(exam, objConn)

            Dim exam_DS As DataSet = New DataSet
            examDA.Fill(exam_DS, "ExamTable")

            ddlClass.DataSource = exam_DS
            ddlClass.DataValueField = "class_ID"
            ddlClass.DataTextField = "Class_Name"
            ddlClass.DataBind()
            ddlClass.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlexam_info()
        Try
            Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

            Dim find_exam As String = "select class_Level from class_info where class_year = '" & ddlyear.SelectedValue & "' and stf_ID = '" & DATA_STAFFID & "'"
            Dim get_exam As String = oCommon.getFieldValue(find_exam)

            Dim exam As String = "select distinct exam_info.exam_ID, exam_info.exam_Name from exam_info
                                  left join exam_result on exam_info.exam_ID = exam_result.exam_ID
                                  left join course on exam_result.course_Id = course.course_ID
                                  left join class_info on course.class_ID = class_info.class_ID
                                  where class_info.class_year = '" & ddlyear.SelectedValue & "'
                                  and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                  and course.year = '" & ddlyear.SelectedValue & "'
                                  and class_info.class_level = '" & get_exam & "'
                                  order by exam_Name ASC"
            Dim examDA As New SqlDataAdapter(exam, objConn)

            Dim exam_DS As DataSet = New DataSet
            examDA.Fill(exam_DS, "ExamTable")

            ddlexam_Name.DataSource = exam_DS
            ddlexam_Name.DataValueField = "exam_Name"
            ddlexam_Name.DataTextField = "exam_Name"
            ddlexam_Name.DataBind()
            ddlexam_Name.Items.Insert(0, New ListItem("Select Exam", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            ddlLevel_info()
            ddlexam_info()
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim get_exam As String = "select exam_ID from exam_info where exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_year = '" & ddlyear.SelectedValue & "'"
        Dim data_exam As String = oCommon.getFieldValue(get_exam)

        Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by class_info.class_Name, student_info.student_Name ASC"

        tmpSQL = "select distinct student_info.std_ID, student_info.student_Name, student_info.student_ID, student_info.student_Mykad, 
                  class_info.class_Name, exam_info.exam_Name from student_info
                  left join course on student_info.std_ID = course.std_ID
                  left join exam_result on course.course_ID = exam_result.course_ID
                  left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                  left join class_info on course.class_ID = class_info.class_ID"

        strWhere = " where course.year = '" & ddlyear.SelectedValue & "'"
        strWhere += " And exam_info.exam_Year = '" & ddlyear.SelectedValue & "'"
        strWhere += " And exam_info.exam_ID = '" & data_exam & "'"
        strWhere += " And class_info.class_year = '" & ddlyear.SelectedValue & "'"
        strWhere += " And class_info.class_Level = '" & ddlLevel.SelectedValue & "'"
        strWhere += " And class_info.class_type = 'Compulsory'"
        strWhere += " And class_info.class_ID = '" & ddlClass.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL

    End Function

    Protected Sub ddlexam_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlexam_Name.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        Try
            ddlclass_info()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Answer_Test.Style.Add("display", "block")

        Try

            Dim get_student_Class As String = "select class_info.class_Name from class_info left join course on class_info.class_ID = course.class_ID
                                               where course.year = '" & ddlyear.SelectedValue & "' and class_info.class_year = '" & ddlyear.SelectedValue & "'
                                               and course.std_ID = '" & strKeyName & "' and class_info.class_type = 'Compulsory'"
            Dim data_student_Class As String = oCommon.getFieldValue(get_student_Class)

            Dim get_student_Name As String = "select student_Name from student_info where std_ID = '" & strKeyName & "' "
            Dim data_student_Name As String = oCommon.getFieldValue(get_student_Name)

            Dim get_student_Mykad As String = "select student_Mykad from student_info where std_ID = '" & strKeyName & "' "
            Dim data_student_Mykad As String = oCommon.getFieldValue(get_student_Mykad)


            'std_Name.Text = data_student_Name
            'std_Mykad.Text = data_student_Mykad
            'std_Class.Text = data_student_Class
            'std_Exam.Text = ddlexam_Name.SelectedValue
            'std_Year.Text = ddlyear.SelectedValue

            strRet = RemarkData(GridView1)

            remark_load_page()

        Catch ex As Exception
        End Try
    End Sub

    Private Function RemarkData(ByVal gvTable As GridView) As Boolean

        Dim myTable As DataTable = New DataTable
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getREMARK_SQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Dim i As Integer

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            For i = 0 To GridView1.Rows.Count - 1 Step i + 1
                myTable = myDataSet.Tables(0)

                Dim rbtlist_remark As RadioButtonList = CType(GridView1.Rows(i).Cells(2).FindControl("rbtn_One"), RadioButtonList)
                If Not IsDBNull(myTable.Rows(i).Item("asrenark")) Then

                    Dim value_data As Integer

                    If value_data = 1 Then
                        rbtlist_remark.SelectedIndex = 0
                    ElseIf value_data = 2 Then
                        rbtlist_remark.SelectedIndex = 1
                    ElseIf value_data = 3 Then
                        rbtlist_remark.SelectedIndex = 2
                    ElseIf value_data = 4 Then
                        rbtlist_remark.SelectedIndex = 3
                    ElseIf value_data = 5 Then
                        rbtlist_remark.SelectedIndex = 4
                    End If

                End If
            Next

            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getREMARK_SQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim get_stdData As String = "select std_ID from student_info where student_Mykad = '" & std_Mykad.Text & "' and student_Status = 'Access' "
        Dim collect_stdData As String = oCommon.getFieldValue(get_stdData)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by assessment_config.asconfig_ID ASC"

        tmpSQL = "select assessment_config.asconfig_ID, assessment_remark.std_ID, assessment_config.description, asremark from assessment_remark
                  left join assessment_config on assessment_remark.asconfig_ID = assessment_config.asconfig_ID
                  left join exam_info on assessment_remark.exam_ID = exam_info.exam_ID"

        strWhere = " where assessment_remark.std_ID = '" & collect_stdData & "'"
        strWhere += " and assessment_remark.class_ID = '" & ddlClass.SelectedValue & "'"
        strWhere += " And exam_info.exam_Name = '" & ddlexam_Name.SelectedValue & "'"
        strWhere += " and exam_info.exam_year = '" & ddlyear.SelectedValue & "'"
        strWhere += " and assessment_remark.year = '" & ddlyear.SelectedValue & "'"

        getREMARK_SQL = tmpSQL & strWhere & strOrderby

        Return getREMARK_SQL

    End Function

    Private Sub remark_load_page()
        Try

            Dim get_std_ID As String = "select std_ID from student_info where student_Mykad = '" & std_Mykad.Text & "' and student_Status = 'Access'"
            Dim data_std_ID As String = oCommon.getFieldValue(get_std_ID)

            strSQL = "SELECT asremark_essay from assessment_remark_essay 
                      left join exam_info on assessment_remark_essay.exam_ID = exam_info.exam_ID
                      where assessment_remark_essay.std_ID ='" & data_std_ID & "' 
                      and assessment_remark_essay.year = '" & std_Year.Text & "' and exam_info.exam_Name = '" & ddlexam_Name.SelectedValue & "'"

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim dmonth As DataSet = New DataSet
            sqlDA.Fill(dmonth, "AnyTable")

            Dim nRowsMonth As Integer = 0
            Dim nCountMonth As Integer = 1
            Dim MyTableMonth As DataTable = New DataTable
            MyTableMonth = dmonth.Tables(0)
            If MyTableMonth.Rows.Count > 0 Then
                If Not IsDBNull(dmonth.Tables(0).Rows(0).Item("asremark_essay")) Then
                    txtMessage.Value = dmonth.Tables(0).Rows(0).Item("asremark_essay")
                Else
                    txtMessage.Value = ""
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        ''update remark radio button
        For i As Integer = 0 To GridView1.Rows.Count - 1

            Dim radioList As RadioButtonList = DirectCast(GridView1.Rows(i).FindControl("rbtn_One"), RadioButtonList)
            Dim strKeyID As String = GridView1.DataKeys(i).Value.ToString

            ''update marks
            strSQL = "UPDATE assessment_remark SET asremark ='" & radioList.SelectedItem.Value & "' WHERE std_ID ='" & strKeyID & "' and year = '" & std_Year.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        Next

        ''get student_ID
        Dim get_ID As String = "select std_ID from student_info where student_Mykad = '" & std_Mykad.Text & "' and student_Status = 'Access'"
        Dim data_ID As String = oCommon.getFieldValue(get_ID)

        ''update marks
        strSQL = "UPDATE assessment_remark_essay SET asremark_essay ='" & txtMessage.Value & "' WHERE std_ID ='" & data_ID & "' and year = '" & std_Year.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        strRet = RemarkData(GridView1)

    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub BtnPrint_ServerClick(sender As Object, e As EventArgs) Handles BtnPrint.ServerClick

    '    Dim myDocument As New Document(PageSize.A4)

    '    Dim i As Integer = 0

    '    HttpContext.Current.Response.ContentType = "application/pdf"
    '    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=UlasanPelajarPeramtaPintar.pdf")
    '    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

    '    PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

    '    myDocument.Open()


    '    For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
    '        Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
    '        If Not chkUpdate Is Nothing Then

    '            Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
    '            If chkUpdate.Checked = True Then

    '                ''GET EXAM ID
    '                Dim get_exam_ID As String = "select exam_ID from exam_info where exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_year = '" & ddlyear.SelectedValue & "'"
    '                Dim data_exam_ID As String = oCommon.getFieldValue(get_exam_ID)

    '                ''GET STUDENT NAME
    '                Dim get_student_name As String = "select student_Name from student_info where std_ID ='" & strKey & "' and student_Status = 'Access'"
    '                Dim data_student_name As String = oCommon.getFieldValue(get_student_name)

    '                ''GET STUDENT CLASS
    '                Dim get_student_class As String = "select class_Name from class_info where class_ID = '" & ddlClass.SelectedValue & "'"
    '                Dim data_student_class As String = oCommon.getFieldValue(get_student_class)

    '                ''GET STUDENT MATIK
    '                Dim get_student_ID As String = "select student_ID from student_info where std_ID ='" & strKey & "' and student_Status = 'Access'"
    '                Dim data_student_ID As String = oCommon.getFieldValue(get_student_ID)

    '                ''GET YEAR
    '                Dim data_student_year As String = ddlyear.SelectedValue

    '                ''GET REMARK DESCRIPTION
    '                Dim tmpsql_remark_description As String = ""
    '                Dim get_description As New DataTable

    '                tmpsql_remark_description = "select assessment_config.description from assessment_remark
    '                                             left join assessment_config on assessment_remark.asconfig_ID = assessment_config.asconfig_ID
    '                                             left join exam_info on assessment_remark.exam_ID = exam_info.exam_ID
    '                                             where assessment_remark.std_ID = '" & strKey & "' and assessment_remark.class_ID = '" & ddlClass.SelectedValue & "'
    '                                             and exam_info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_year = '" & ddlyear.SelectedValue & "' and assessment_remark.year = '" & ddlyear.SelectedValue & "'
    '                                             order by assessment_config.asconfig_ID ASC"
    '                Dim sql_remark_description As New SqlDataAdapter(tmpsql_remark_description, strConn)

    '                ''GET REMARK POINT
    '                Dim tmpsql_remark_point As String = ""
    '                Dim get_point As New DataTable

    '                tmpsql_remark_point = "select asremark from assessment_remark
    '                                       left join assessment_config on assessment_remark.asconfig_ID = assessment_config.asconfig_ID
    '                                       left join exam_info on assessment_remark.exam_ID = exam_info.exam_ID
    '                                       where assessment_remark.std_ID = '" & strKey & "' and assessment_remark.class_ID = '" & ddlClass.SelectedValue & "'
    '                                       and exam_info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_year = '" & ddlyear.SelectedValue & "' and assessment_remark.year = '" & ddlyear.SelectedValue & "'
    '                                       order by assessment_config.asconfig_ID ASC"
    '                Dim sql_remark_point As New SqlDataAdapter(tmpsql_remark_point, strConn)

    '                ''GET REMARK ESSAY
    '                Dim tmpsql_remark_essay As String = ""
    '                Dim get_essay As New DataTable

    '                tmpsql_remark_essay = "SELECT asremark_essay from assessment_remark_essay 
    '                                       left join exam_info on asremark_essay.exam_ID = exam_info.exam_ID
    '                                       where asremark_essay.std_ID ='" & strKey & "' and asremark_essay.year = '" & std_Year.Text & "' and exam_info.exam_Name = '" & ddlexam_Name.SelectedValue & "'"
    '                Dim sql_remark_essay As New SqlDataAdapter(tmpsql_remark_essay, strConn)

    '                Try
    '                    sql_remark_description.Fill(get_description)
    '                    sql_remark_point.Fill(get_point)
    '                    sql_remark_essay.Fill(get_essay)
    '                Catch ex As Exception
    '                End Try

    '                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '                '' draw spacing
    '                Dim imgdrawSpacing As String = Server.MapPath("~/img/empty_space.png")
    '                Dim imgSpacing As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgdrawSpacing)
    '                imgSpacing.Alignment = iTextSharp.text.Image.LEFT_ALIGN  'left
    '                imgSpacing.Border = 0

    '                ' drawa permata pintar image
    '                Dim get_imgPP As String = Server.MapPath("~/img/permata_logo.png")
    '                Dim data_imgPP As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(get_imgPP)
    '                data_imgPP.ScalePercent(30)
    '                data_imgPP.SetAbsolutePosition(212, 770)
    '                myDocument.Add(data_imgPP)

    '                '' drawa permata pintar image
    '                Dim get_imgUKM As String = Server.MapPath("~/img/ukm.jpg")
    '                Dim data_imgUKM As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(get_imgUKM)
    '                data_imgUKM.ScalePercent(12.5)
    '                data_imgUKM.SetAbsolutePosition(312, 770)
    '                myDocument.Add(data_imgUKM)

    '                '' spacing
    '                myDocument.Add(imgSpacing)
    '                myDocument.Add(imgSpacing)
    '                myDocument.Add(imgSpacing)
    '                myDocument.Add(imgSpacing)

    '                '' BORANG PENILAIN PELAJAR Text
    '                Dim myPara001 As New Paragraph("BORANG PENILAIAN PELAJAR", FontFactory.GetFont("Arial", 10, Font.BOLD))
    '                myPara001.Alignment = Element.ALIGN_CENTER
    '                myDocument.Add(myPara001)

    '                '' KOLEJ PERMATApintar Text
    '                Dim myPara002 As New Paragraph("KOLEJ PERMATApintar®", FontFactory.GetFont("Arial", 10, Font.BOLD))
    '                myPara002.Alignment = Element.ALIGN_CENTER
    '                myDocument.Add(myPara002)

    '                '' spacing
    '                myDocument.Add(imgSpacing)


    '                '' create a table
    '                Dim table As New PdfPTable(4)
    '                table.WidthPercentage = 100
    '                table.SetWidths({5, 22, 68, 5})

    '                Dim cetak = Environment.NewLine & ""
    '                Dim cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" NAMA PELAJAR", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" " & data_student_name, FontFactory.GetFont("Arial", 9)))
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" KELAS", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" " & data_student_class, FontFactory.GetFont("Arial", 9)))
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table.AddCell(cell)
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" NO MATRIK", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" " & data_student_ID, FontFactory.GetFont("Arial", 9)))
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table.AddCell(cell)
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" TAHUN PENILAIAN", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" " & data_student_year, FontFactory.GetFont("Arial", 9)))
    '                table.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table.AddCell(cell)

    '                myDocument.Add(table)

    '                '' spacing
    '                myDocument.Add(imgSpacing)

    '                '' create a table
    '                Dim table1 As New PdfPTable(2)
    '                table1.WidthPercentage = 100
    '                table1.SetWidths({5, 95})

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table1.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph("Sila rujuk jadual pemarkahan", FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table1.AddCell(cell)

    '                myDocument.Add(table1)

    '                '' spacing
    '                myDocument.Add(imgSpacing)

    '                '' create a table
    '                Dim table2 As New PdfPTable(4)
    '                table2.WidthPercentage = 100
    '                table2.SetWidths({15, 5, 30, 50})

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" 5", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Sangat Baik", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" 4", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Baik", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" 3", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Memuaskan", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" 2", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Kurang Memuaskan", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" 1", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Tidak Memuaskan", FontFactory.GetFont("Arial", 9)))
    '                table2.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table2.AddCell(cell)

    '                myDocument.Add(table2)

    '                '' spacing
    '                myDocument.Add(imgSpacing)

    '                '' create a table
    '                Dim table3 As New PdfPTable(2)
    '                table3.WidthPercentage = 100
    '                table3.SetWidths({5, 95})

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table3.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph("BAHAGIAN A : KEHADIRAN DAN PRESTASI PELAJAR ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                cell.Border = 0
    '                table3.AddCell(cell)

    '                myDocument.Add(table3)

    '                '' spacing
    '                myDocument.Add(imgSpacing)

    '                '' create a table
    '                Dim table4 As New PdfPTable(5)
    '                table4.WidthPercentage = 100
    '                table4.SetWidths({5, 5, 60, 12, 18})

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table4.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Bil ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                table4.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Perkara ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                table4.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Pemarkahan ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                table4.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table4.AddCell(cell)

    '                '' get count remark content for 
    '                Dim get_count_remark As String = "select count(*) from assessment_config"
    '                Dim data_count_remark As String = oCommon.getFieldValue(get_count_remark)

    '                For value As Integer = 1 To data_count_remark

    '                    Dim get_content_remark As String = "select description from assessment_config where asconfig_ID = '" & value & "'"
    '                    Dim data_content_remark As String = oCommon.getFieldValue(get_content_remark)

    '                    Dim get_remark_result As String = "select asremark from assessment_remark where asconfig_ID = '" & value & "' and std_ID = '" & strKey & "' and class_ID = '" & ddlClass.SelectedValue & "' and exam_ID = '" & data_exam_ID & "'"
    '                    Dim data_remark_result As String = oCommon.getFieldValue(get_remark_result)

    '                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                    cell.Border = 0
    '                    table4.AddCell(cell)

    '                    cell = New PdfPCell(New Paragraph(" " & value, FontFactory.GetFont("Arial", 9)))
    '                    table4.AddCell(cell)

    '                    cell = New PdfPCell(New Paragraph(" " & data_content_remark, FontFactory.GetFont("Arial", 9)))
    '                    table4.AddCell(cell)

    '                    Dim myPara005 As New Paragraph(" " & data_remark_result, FontFactory.GetFont("Arial", 9))
    '                    myPara005.Alignment = Element.ALIGN_CENTER

    '                    cell.VerticalAlignment = Element.ALIGN_MIDDLE
    '                    cell.AddElement(myPara005)
    '                    table4.AddCell(cell)

    '                    cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                    cell.Border = 0
    '                    table4.AddCell(cell)

    '                Next

    '                myDocument.Add(table4)

    '                '' spacing
    '                myDocument.Add(imgSpacing)

    '                '' create a table
    '                Dim table5 As New PdfPTable(2)
    '                table5.WidthPercentage = 100
    '                table5.SetWidths({5, 95})

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table5.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph("BAHAGIAN B : PRESTASI DAN HARAPAN KOLEJ PERMATAPINTAR KEPADA PELAJAR ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                cell.Border = 0
    '                table5.AddCell(cell)

    '                myDocument.Add(table5)

    '                '' spacing
    '                myDocument.Add(imgSpacing)

    '                Dim get_remark_essay As String = "select asremark_essay from assessment_remark_essay where year = '" & ddlyear.SelectedValue & "' and std_ID = '" & strKey & "' and class_ID = '" & ddlClass.SelectedValue & "' and exam_ID = '" & data_exam_ID & "'"
    '                Dim data_remark_essay As String = oCommon.getFieldValue(get_remark_essay)

    '                '' create a table
    '                Dim table6 As New PdfPTable(3)
    '                table6.WidthPercentage = 100
    '                table6.SetWidths({5, 90, 5})

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                Dim cell_rowspan = New PdfPCell(New Paragraph(" " & data_remark_essay, FontFactory.GetFont("Arial", 9)))
    '                cell_rowspan.Rowspan = 5
    '                table6.AddCell(cell_rowspan)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table6.AddCell(cell)

    '                myDocument.Add(table6)

    '                '' spacing
    '                myDocument.Add(imgSpacing)
    '                myDocument.Add(imgSpacing)
    '                myDocument.Add(imgSpacing)
    '                myDocument.Add(imgSpacing)


    '                '' create a table
    '                Dim table7 As New PdfPTable(3)
    '                table7.WidthPercentage = 100
    '                table7.SetWidths({25, 15, 60})

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table7.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph("(Tandatangan)", FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table7.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table7.AddCell(cell)

    '                myDocument.Add(table7)

    '                '' spacing
    '                myDocument.Add(imgSpacing)

    '                '' create a table
    '                Dim table8 As New PdfPTable(4)
    '                table8.WidthPercentage = 100
    '                table8.SetWidths({5, 60, 20, 5})

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Nama : ", FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                Dim cell_rowspan_cop = New PdfPCell(New Paragraph("         Cop Rasmi Kolej ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
    '                cell_rowspan_cop.Rowspan = 4
    '                table8.AddCell(cell_rowspan_cop)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(" Tarikh : ", FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                cell = New PdfPCell(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
    '                cell.Border = 0
    '                table8.AddCell(cell)

    '                myDocument.Add(table8)

    '                '' spacing
    '                myDocument.NewPage()

    '            End If
    '        End If
    '    Next

    '    myDocument.Close()

    '    HttpContext.Current.Response.Write(myDocument)
    '    HttpContext.Current.Response.End()

    'End Sub

End Class