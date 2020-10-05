Imports System.Data.SqlClient

Public Class pengarah_examination_slip
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    '' connection to PERMATApintar® databasse
    Dim permataConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objPermataConn As SqlConnection = New SqlConnection(permataConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                exam_info()
                student_year_info()
                year_list()
                load_page()
                strRet = BindData(datRespondent)

                rbtn_English.Checked = False
                rbtn_Malay.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub exam_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Exam' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlexam_Name.DataSource = ds
            ddlexam_Name.DataTextField = "Parameter"
            ddlexam_Name.DataValueField = "Parameter"
            ddlexam_Name.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT distinct year from student_Level where year ='" & Now.Year & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlyear.SelectedValue = ""
            End If
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        strSQL = "SELECT exam_Name from exam_Info where exam_Name ='Exam 1'"

        Dim strConnExam As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConnExam As SqlConnection = New SqlConnection(strConnExam)
        Dim sqlDAExam As New SqlDataAdapter(strSQL, objConnExam)

        Dim dsExam As DataSet = New DataSet
        sqlDAExam.Fill(dsExam, "AnyTable")

        Dim nRowsExam As Integer = 0
        Dim nCountExam As Integer = 1
        Dim MyTableExam As DataTable = New DataTable
        MyTableExam = dsExam.Tables(0)
        If MyTableExam.Rows.Count > 0 Then
            If Not IsDBNull(dsExam.Tables(0).Rows(0).Item("exam_Name")) Then
                ddlexam_Name.SelectedValue = dsExam.Tables(0).Rows(0).Item("exam_Name")
            Else
                ddlexam_Name.SelectedValue = ""
            End If
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        strSQL = "SELECT student_Level from student_level where student_Level ='Foundation 1'"

        Dim strConnLevel As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConnLevel As SqlConnection = New SqlConnection(strConnLevel)
        Dim sqlDALevel As New SqlDataAdapter(strSQL, objConnLevel)

        Dim dsLevel As DataSet = New DataSet
        sqlDALevel.Fill(dsLevel, "AnyTable")

        Dim nRowsLevel As Integer = 0
        Dim nCountLevel As Integer = 1
        Dim MyTableLevel As DataTable = New DataTable
        MyTableLevel = dsLevel.Tables(0)
        If MyTableLevel.Rows.Count > 0 Then
            If Not IsDBNull(dsLevel.Tables(0).Rows(0).Item("student_Level")) Then
                ddlstudent_Year.SelectedValue = dsLevel.Tables(0).Rows(0).Item("student_Level")
            Else
                ddlstudent_Year.SelectedValue = ""
            End If
        End If

    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_year_info()
        strSQL = "select Parameter from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstudent_Year.DataSource = ds
            ddlstudent_Year.DataTextField = "Parameter"
            ddlstudent_Year.DataValueField = "Parameter"
            ddlstudent_Year.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

            rbtn_English.Checked = False
            rbtn_Malay.Checked = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnprint_ServerClick(sender As Object, e As EventArgs) Handles Btnprint.ServerClick

        Dim tmpSQL As String = ""
        Dim errorCount As Integer = 0
        Dim i As Integer = 0
        Dim Test As New StringBuilder()

        'get englih literture on / off
        Dim check_Eng_Literature As String = "select Value from setting where Type = 'English Literature'"
        Dim Confirm_Eng_Literature As String = oCommon.getFieldValue(check_Eng_Literature)

        'get Portfolio percentage on / off
        Dim check_portfolio_percen As String = "select Value from setting where Type = 'Portfolio_Percentage'"
        Dim Confirm_Portfolio As String = oCommon.getFieldValue(check_portfolio_percen)

        ''get cocuricullum percentage on / off
        Dim check_cocuricullum_percen As String = "select Value from setting where Type = 'Cocuricullum_Percentage'"
        Dim Confirm_Cocuricullum As String = oCommon.getFieldValue(check_cocuricullum_percen)

        ''get research percentage on / off
        Dim check_research_percen As String = "select Value from setting where Type = 'Research_Percentage'"
        Dim Confirm_Research As String = oCommon.getFieldValue(check_research_percen)

        ''get self development percentage on / off
        Dim check_self_percen As String = "select Value from setting where Type = 'Self_Development_Percentage'"
        Dim Confirm_Self As String = oCommon.getFieldValue(check_self_percen)

        ''check print transcript language''
        If rbtn_Malay.Checked = True Then
            rbtn_English.Checked = False

            Test.AppendLine("<div id='data' style='display:none'>")
            Test.AppendLine("<div id='dataTESTBM'> ")

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''print subject name and grade
                        tmpSQL = "SELECT subject_NameBM, grade FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                        Dim SQA As New SqlDataAdapter(tmpSQL, strConn)

                        Dim DS As New DataTable
                        Dim DSEnglish_literature As New DataTable
                        Dim DSPortfolio As New DataTable
                        Dim DSResearch As New DataTable
                        Dim DSSelfdevelopment As New DataTable
                        Dim DSCocuricullum As New DataTable

                        ''print english literature
                        If Confirm_Eng_Literature = "on" Then
                            tmpSQL = "SELECT grade FROM [ExamSlip_English_Literature] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQEnglish_Literature As New SqlDataAdapter(tmpSQL, strConn)

                            Try
                                SQEnglish_Literature.Fill(DSEnglish_literature)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print Portfolio
                        If Confirm_Portfolio = "on" Then
                            tmpSQL = "SELECT grade FROM [ExamSlip_Portfolio] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQPortfolio As New SqlDataAdapter(tmpSQL, strConn)

                            Try
                                SQPortfolio.Fill(DSPortfolio)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print research 
                        If Confirm_Research = "on" Then
                            tmpSQL = "SELECT grade FROM [ExamSlip_Research] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQResearch As New SqlDataAdapter(tmpSQL, strConn)

                            Try
                                SQResearch.Fill(DSResearch)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print self development
                        If Confirm_Self = "on" Then
                            Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' "
                            Dim getLevel As String = oCommon.getFieldValue(level)

                            If getLevel <> "Level 1" Or getLevel <> "Level 2" Then
                                tmpSQL = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                tmpSQL = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            End If

                            Dim SQSelfdevelopment As New SqlDataAdapter(tmpSQL, strConn)

                            Try
                                SQSelfdevelopment.Fill(DSSelfdevelopment)
                            Catch ex As Exception

                            End Try
                        End If

                        '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                        If Confirm_Cocuricullum = "on" Then

                            Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim getStudent As String = oCommon.getFieldValue(studentData)

                            If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 5" Or ddlexam_Name.SelectedValue = "Exam 9" Then

                                tmpSQL = "select koko_pelajar.GredP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 10" Then

                                tmpSQL = "select koko_pelajar.GredP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 11" Then

                                tmpSQL = "select koko_pelajar.GredP3 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 12" Then

                                tmpSQL = "select koko_pelajar.GredP4 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try
                            End If
                        End If

                        Try
                            SQA.Fill(DS)
                        Catch ex As Exception
                        End Try

                        ''print student name
                        Dim stdName As String = "select student_Name from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdName As String = oCommon.getFieldValue(stdName)

                        ''print student id
                        Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdID As String = oCommon.getFieldValue(stdID)

                        ''print student mykad
                        Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

                        ''print exam Name
                        Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlexam_Name.SelectedValue & "'"
                        Dim dataExmName As String = oCommon.getFieldValue(exmName)

                        If dataExmName = "Exam 1" Then
                            dataExmName = "PEPERIKSAAN 1 ,"
                        ElseIf dataExmName = "Exam 2" Then
                            dataExmName = "PEPERIKSAAN 2 ,"
                        ElseIf dataExmName = "Exam 3" Then
                            dataExmName = "PEPERIKSAAN 3 ,"
                        ElseIf dataExmName = "Exam 4" Then
                            dataExmName = "PEPERIKSAAN 4 ,"
                        ElseIf dataExmName = "Exam 5" Then
                            dataExmName = "PEPERIKSAAN 5 ,"
                        ElseIf dataExmName = "Exam 6" Then
                            dataExmName = "PEPERIKSAAN 6 ,"
                        ElseIf dataExmName = "Exam 7" Then
                            dataExmName = "PEPERIKSAAN 7 ,"
                        ElseIf dataExmName = "Exam 8" Then
                            dataExmName = "PEPERIKSAAN 8 ,"
                        ElseIf dataExmName = "Exam 9" Then
                            dataExmName = "PEPERIKSAAN 9 ,"
                        ElseIf dataExmName = "Exam 10" Then
                            dataExmName = "PEPERIKSAAN 10 ,"
                        ElseIf dataExmName = "Exam 11" Then
                            dataExmName = "PEPERIKSAAN 11 ,"
                        ElseIf dataExmName = "Exam 12" Then
                            dataExmName = "PEPERIKSAAN 12 ,"
                        End If

                        ''get month
                        Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
                        Dim dataMonth As String = oCommon.getFieldValue(month)

                        Dim dataStdMonth As String = ""

                        If dataMonth = "1" Then
                            dataStdMonth = "Januari"
                        ElseIf dataMonth = "2" Then
                            dataStdMonth = "Februari"
                        ElseIf dataMonth = "3" Then
                            dataStdMonth = "Mac"
                        ElseIf dataMonth = "4" Then
                            dataStdMonth = "April"
                        ElseIf dataMonth = "5" Then
                            dataStdMonth = "Mei"
                        ElseIf dataMonth = "6" Then
                            dataStdMonth = "Jun"
                        ElseIf dataMonth = "7" Then
                            dataStdMonth = "Julai"
                        ElseIf dataMonth = "8" Then
                            dataStdMonth = "Ogos"
                        ElseIf dataMonth = "9" Then
                            dataStdMonth = "September"
                        ElseIf dataMonth = "10" Then
                            dataStdMonth = "Oktober"
                        ElseIf dataMonth = "11" Then
                            dataStdMonth = "November"
                        ElseIf dataMonth = "12" Then
                            dataStdMonth = "Disember"
                        End If

                        ''get PNG & PNGK 
                        Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

                        Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

                        Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
                        Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

                        ''round to 2 decimal places
                        Dim gpa As Decimal = png_dec.ToString("F2")
                        Dim cgpa As Decimal = pngs_dec.ToString("F2")

                        ''first column
                        Test.Append("<div style='margin:0;page-break-after: always;'>
                                    <div style ='background-image: url(img/exam_transkrip_v13_2.jpg);height:90%;background-position:center;background-repeat: no-repeat;background-size: cover;'> 
                                        <table style='width:100%'>
                                            <tr> 
                                                <td style='width: 33.33%'>
                                                    <table style='width: 100%; margin-top:20px;margin-bottom:0px'> 
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                   <tr>
                                                                        <td>
                                                                            <img src='img/permata_logo.png' height='56' width='120'>
                                                                        </td>
                                                                        <td>
                                                                            <img src='img/ukm.jpg'  height='56' width='120'>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        <p> &nbsp;&nbsp; </p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style='width: 100%'> 
                                                                        <td colspan='2'>SLIP KEPUTUSAN</td>
                                                                    </tr>
                                                                    <tr style='width :100%'>
                                                                        <td colspan='2'>" & dataExmName & "</td>  
                                                                    </tr>
                                                                    <tr style='width: 100%'>
                                                                        <td colspan='2'>TAHUN AKADEMIK " & ddlyear.SelectedValue & "</td>
                                                                    </tr>
                                                                    <tr style='width :100%'>
                                                                        <td colspan='2'>KOLEJ PERMATApintar®</td>
                                                                    </tr>
                                                                        <br>
                                                                    <tr style='width :100%'>
                                                                        <td colspan='2'>
                                                                        <br> Nama :</td>
                                                                    </tr>
                                                                    <tr style='width :100%'>
                                                                        <td colspan='2'>" & dataStdName & "</td>
                                                                    </tr>
                                                                        <br>
                                                                    <tr style='width: 100%'>
                                                                        <td style='width:100px'>
                                                                        <br> ID No : </td>
                                                                        <td>
                                                                        <br> MYKAD No :</td>
                                                                    </tr>
                                                                    <tr style='width :100%'>
                                                                        <td>" & dataStdID & "</td>
                                                                        <td>" & dataStdMykad & "</td>
                                                                    </tr>
                                                                        <br>
                                                                    <tr style='width ':100%' >
                                                                        <td colspan='2'>
                                                                        <br>Status :</td>
                                                                    </tr>
                                                                    <tr style='width:100%'>
                                                                        <td colspan='2'>Lulus Dan Layak Meneruskan Pengajian</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style ='width:33.33%'>
                                                    <table style='width:100%;margin-top :80px'>
                                                        <tr>
                                                            <td style='text-align:left; width: 325px'><b>Kursus</b></td>
                                                            <td style='text-align:left'><b>Gred</b></td>
                                                        </tr>")
                        ''second column (academic)
                        For Each row As DataRow In DS.Rows
                            Test.Append("<tr>")
                            For Each column As DataColumn In DS.Columns
                                Test.Append("<td style='text-align:left'>")
                                Test.Append(row(column.ColumnName))
                                Test.Append("</td>")
                            Next
                            Test.Append("</tr>")
                        Next

                        ''second column (cocuricullum)
                        If Confirm_Cocuricullum = "on" Then
                            For Each row As DataRow In DSCocuricullum.Rows
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("Kokurikulum")
                                Test.Append("</td>")
                                For Each column As DataColumn In DSCocuricullum.Columns
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                                Test.Append("</tr>")
                            Next

                        ElseIf Confirm_Cocuricullum = "off" Then
                            Test.Append("<tr>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("Kokurikulum")
                            Test.Append("</td>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("SM")
                            Test.Append("</td>")
                            Test.Append("</tr>")
                        End If

                        ''second column (research)
                        If Confirm_Research = "on" Then
                            For Each row As DataRow In DSResearch.Rows
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("Penyelidikan")
                                Test.Append("</td>")
                                For Each column As DataColumn In DSResearch.Columns
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                                Test.Append("</tr>")
                            Next

                        ElseIf Confirm_Research = "off" Then
                            Test.Append("<tr>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("Penyelidikan")
                            Test.Append("</td>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("SM")
                            Test.Append("</td>")
                            Test.Append("</tr>")
                        End If

                        ''second column (Portfolio)
                        If Confirm_Portfolio = "on" Then
                            For Each row As DataRow In DSPortfolio.Rows
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("Portfolio")
                                Test.Append("</td>")
                                For Each column As DataColumn In DSPortfolio.Columns
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                                Test.Append("</tr>")
                            Next

                        ElseIf Confirm_Portfolio = "off" Then
                            Test.Append("<tr>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("Portfolio")
                            Test.Append("</td>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("SM")
                            Test.Append("</td>")
                            Test.Append("</tr>")
                        End If

                        ''second column (self development)
                        If Confirm_Self = "on" Then
                            For Each row As DataRow In DSSelfdevelopment.Rows
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("Pembangunan Kendiri")
                                Test.Append("</td>")
                                For Each column As DataColumn In DSSelfdevelopment.Columns
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                                Test.Append("</tr>")
                            Next

                        ElseIf Confirm_Self = "off" Then
                            Test.Append("<tr>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("Pembangunan Kendiri")
                            Test.Append("</td>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("SM")
                            Test.Append("</td>")
                            Test.Append("</tr>")
                        End If

                        Dim get_ENG As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name = 'AP English Language and Composition' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE As String = oCommon.getFieldValue(get_ENG)

                        If data_ENGLITERATURE.Length > 0 Then

                            ''second column (english literature)
                            If Confirm_Eng_Literature = "on" Then
                                For Each row As DataRow In DSEnglish_literature.Rows
                                    Test.Append("<tr>")
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append("English Literature And Composition")
                                    Test.Append("</td>")
                                    For Each column As DataColumn In DSEnglish_literature.Columns
                                        Test.Append("<td style='text-align:left'>")
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("</td>")
                                    Next
                                    Test.Append("</tr>")
                                Next

                            ElseIf Confirm_Eng_Literature = "off" Then
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("English Literature And Composition")
                                Test.Append("</td>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("SM")
                                Test.Append("</td>")
                                Test.Append("</tr>")
                            End If

                        End If

                        ''gpa & cgpa display and third column
                        Test.Append("               </table>
                                                    <div>_______________________________________________</div>
                                                    <table style='width:100%'>
                                                        <tr>
                                                            <td style='text-align: left; width: 325px'><b>PNG </b></td>
                                                            <td style='text-align :left'><b>" & gpa & "</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td style='text-align: Left'><b> PNGK </b></td>
                                                            <td style='text-align: left'><b>" & cgpa & "</b></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style ='width:33.33%'>
                                                    <table style ='width: 100%; margin-top :260px; margin-left: 100px'>
                                                        <tr style='width :100%'>
                                                            <td colspan='2'>______________________________</td>
                                                        </tr>
                                                        <tr style='width: 100%'>
                                                            <td colspan ='2'>Tandatangan Pengarah</td>
                                                        </tr>
                                                        <tr style='width:100%'>
                                                            <td colspan ='2'>" & Now.Day & " " & dataStdMonth & " " & Now.Year & " </td>
                                                        </tr>
                                                        <tr style='width :100%'>
                                                            <td colspan ='2'>
                                                            Pusat PERMATApintar® Negara,</td>
                                                        </tr>
                                                        <tr style ='width :100%'>
                                                            <td colspan='2'>Universiti Kebangsaan Malaysia,</td>
                                                        </tr>
                                                        <tr style ='width :100%'>
                                                            <td colspan='2'>43600 UKM Bangi,</td>
                                                        </tr>
                                                        <tr style ='width :100%'>
                                                            <td colspan='2'>Selangor Darul Ehsan.</td>
                                                        </tr>
                                                        <tr style ='width :50%'>
                                                            <td> Tel : </td>
                                                            <td>03-89217529/7528/7508</td>
                                                        </tr>
                                                        <tr style='width :50%'>
                                                            <td> Faks :  </td>
                                                            <td>03-89217525</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                              </div>             
                                </div>")

                    End If
                End If
            Next

            Test.AppendLine(" </div> </div>")
            Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataTESTBM'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

            ''print
            Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

        ElseIf rbtn_English.Checked = True Then
            rbtn_Malay.Checked = False

            Test.AppendLine("<div id='data' style='display:none'>")
            Test.AppendLine("<div id='dataTESTBI'>")

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''print academic
                        tmpSQL = "SELECT subject_Name, grade FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                        Dim SQA As New SqlDataAdapter(tmpSQL, strConn)
                        Dim DS As New DataTable
                        Dim DSEnglish_literature As New DataTable
                        Dim DSPortfolio As New DataTable
                        Dim DSResearch As New DataTable
                        Dim DSSelfdevelopment As New DataTable
                        Dim DSCocuricullum As New DataTable

                        ''print english literature
                        If Confirm_Eng_Literature = "on" Then
                            tmpSQL = "SELECT grade FROM [ExamSlip_English_Literature] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQEnglish_Literature As New SqlDataAdapter(tmpSQL, strConn)
                            SQEnglish_Literature.SelectCommand.CommandTimeout = 120

                            Try
                                SQEnglish_Literature.Fill(DSEnglish_literature)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print Portfolio
                        If Confirm_Portfolio = "on" Then
                            tmpSQL = "SELECT grade FROM [ExamSlip_Portfolio] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQPortfolio As New SqlDataAdapter(tmpSQL, strConn)
                            SQPortfolio.SelectCommand.CommandTimeout = 120

                            Try
                                SQPortfolio.Fill(DSPortfolio)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print research 
                        If Confirm_Research = "on" Then
                            tmpSQL = "SELECT grade FROM [ExamSlip_Research] 
                                      where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            Dim SQResearch As New SqlDataAdapter(tmpSQL, strConn)
                            SQResearch.SelectCommand.CommandTimeout = 120

                            Try
                                SQResearch.Fill(DSResearch)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print self development
                        If Confirm_Self = "on" Then
                            Dim level As String = "select student_Level from student_level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' "
                            Dim getLevel As String = getFieldValue(level, strConn)

                            If getLevel <> "Level 1" Or getLevel <> "Level 2" Then
                                tmpSQL = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                          where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                tmpSQL = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                         where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                            End If

                            Dim SQSelfdevelopment As New SqlDataAdapter(tmpSQL, strConn)
                            SQSelfdevelopment.SelectCommand.CommandTimeout = 120

                            Try
                                SQSelfdevelopment.Fill(DSSelfdevelopment)
                            Catch ex As Exception

                            End Try
                        End If

                        '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
                        If Confirm_Cocuricullum = "on" Then

                            Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                            Dim getStudent As String = getFieldValue(studentData, strConn)

                            If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 5" Or ddlexam_Name.SelectedValue = "Exam 9" Then

                                tmpSQL = "select koko_pelajar.GredP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 6" Or ddlexam_Name.SelectedValue = "Exam 10" Then

                                tmpSQL = "select koko_pelajar.GredP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 7" Or ddlexam_Name.SelectedValue = "Exam 11" Then

                                tmpSQL = "select koko_pelajar.GredP3 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 4" Or ddlexam_Name.SelectedValue = "Exam 8" Or ddlexam_Name.SelectedValue = "Exam 12" Then

                                tmpSQL = "select koko_pelajar.GredP4 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try
                            End If
                        End If

                        Try
                            SQA.Fill(DS)
                        Catch ex As Exception
                        End Try

                        ''print student name
                        Dim stdName As String = "Select student_Name from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdName As String = getFieldValue(stdName, strConn)

                        ''print student id
                        Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdID As String = getFieldValue(stdID, strConn)

                        ''print student mykad
                        Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                        Dim dataStdMykad As String = getFieldValue(stdMykad, strConn)

                        ''print exam Name
                        Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlexam_Name.SelectedValue & "'"
                        Dim dataExmName As String = getFieldValue(exmName, strConn)

                        If dataExmName = "Exam 1" Then
                            dataExmName = "EXAMINATION 1 ,"
                        ElseIf dataExmName = "Exam 2" Then
                            dataExmName = "EXAMINATION 2 ,"
                        ElseIf dataExmName = "Exam 3" Then
                            dataExmName = "EXAMINATION 3 ,"
                        ElseIf dataExmName = "Exam 4" Then
                            dataExmName = "EXAMINATION 4 ,"
                        ElseIf dataExmName = "Exam 5" Then
                            dataExmName = "EXAMINATION 5 ,"
                        ElseIf dataExmName = "Exam 6" Then
                            dataExmName = "EXAMINATION 6 ,"
                        ElseIf dataExmName = "Exam 7" Then
                            dataExmName = "EXAMINATION 7 ,"
                        ElseIf dataExmName = "Exam 8" Then
                            dataExmName = "EXAMINATION 8 ,"
                        ElseIf dataExmName = "Exam 9" Then
                            dataExmName = "EXAMINATION 9 ,"
                        ElseIf dataExmName = "Exam 10" Then
                            dataExmName = "EXAMINATION 10 ,"
                        ElseIf dataExmName = "Exam 11" Then
                            dataExmName = "EXAMINATION 11 ,"
                        ElseIf dataExmName = "Exam 12" Then
                            dataExmName = "EXAMINATION 12 ,"
                        End If

                        ''get month
                        Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
                        Dim dataMonth As String = getFieldValue(month, strConn)

                        Dim dataStdMonth As String = ""

                        If dataMonth = "1" Then
                            dataStdMonth = "January"
                        ElseIf dataMonth = "2" Then
                            dataStdMonth = "February"
                        ElseIf dataMonth = "3" Then
                            dataStdMonth = "March"
                        ElseIf dataMonth = "4" Then
                            dataStdMonth = "April"
                        ElseIf dataMonth = "5" Then
                            dataStdMonth = "May"
                        ElseIf dataMonth = "6" Then
                            dataStdMonth = "June"
                        ElseIf dataMonth = "7" Then
                            dataStdMonth = "July"
                        ElseIf dataMonth = "8" Then
                            dataStdMonth = "August"
                        ElseIf dataMonth = "9" Then
                            dataStdMonth = "September"
                        ElseIf dataMonth = "10" Then
                            dataStdMonth = "October"
                        ElseIf dataMonth = "11" Then
                            dataStdMonth = "November"
                        ElseIf dataMonth = "12" Then
                            dataStdMonth = "December"
                        End If

                        ''get PNG & PNGK 
                        Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim exist_png_data As String = getFieldValue(check_png_exist_data, strConn)

                        Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim exist_pngs_data As String = getFieldValue(check_pngs_exist_data, strConn)

                        Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
                        Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

                        ''round to 2 decimal places
                        Dim gpa As Decimal = png_dec.ToString("F2")
                        Dim cgpa As Decimal = pngs_dec.ToString("F2")

                        ''first column
                        Test.Append("<div style='margin:0;page-break-after: always;'>
                                    <div style ='background-image: url(img/exam_transkrip_v13_2.jpg);height:90%;background-position:center;background-repeat: no-repeat;background-size: cover;'> 
                                                  <table style='width:100%'>
                                                      <tr>
                                                          <td style='width: 33.33%'>
                                                              <table style='width: 100%; margin-top :20px'>
                                                                  <tr>
                                                                      <td>
                                                                          <table>
                                                                              <tr>
                                                                                <td>
                                                                                    <img src='img/permata_logo.png' height='56' width='120'>
                                                                                </td>
                                                                                <td>
                                                                                    <img src='img/ukm.jpg'  height='56' width='120'>
                                                                                </td>
                                                                              </tr>
                                                                            <tr>
                                                                                <td>
                                                                                <p> &nbsp;&nbsp; </p>
                                                                                </td>
                                                                            </tr>
                                                                              <tr style='width: 100%'>
                                                                                  <td colspan='2'>EXAMINATION SLIP</td>
                                                                              </tr>
                                                                              <tr style='width :100%'>
                                                                                  <td colspan='2'>" & dataExmName & "</td>
                                                                              </tr>
                                                                              <tr style='width: 100%'>
                                                                                  <td colspan='2'>ACADEMIC YEAR " & ddlyear.SelectedValue & "</td>
                                                                              </tr>
                                                                              <tr style='width :100%'>
                                                                                  <td colspan='2'>PERMATApintar® COLLEGE</td>
                                                                              </tr>
                                                                              <br>
                                                                                  <tr style='width :100%'>
                                                                                      <td colspan='2'>
                                                                                          <br> Name :</td>
                                                                                  </tr>
                                                                                  <tr style='width :100%'>
                                                                                      <td colspan='2'>" & dataStdName & "</td>
                                                                                  </tr>
                                                                                  <br>
                                                                                      <tr style='width: 100%'>
                                                                                          <td style='width:100px'>
                                                                                              <br> ID No : </td>
                                                                                          <td>
                                                                                              <br> MYKAD No :</td>
                                                                                      </tr>
                                                                                      <tr style='width :100%'>
                                                                                          <td>" & dataStdID & "</td>
                                                                                          <td>" & dataStdMykad & "</td>
                                                                                      </tr>
                                                                                      <br>
                                                                                          <tr style='width ':100%' >
                                                                                              <td colspan='2'>
                                                                                                  <br>Status :</td>
                                                                                          </tr>
                                                                                          <tr style='width:100%'>
                                                                                              <td colspan='2'>Pass</td>
                                                                                          </tr>
                                                                </table>
                                                                      </td>
                                                                  </tr>
                                                              </table>
                                                          </td>
                                                          <td style='width:33.33%'>
                                                              <table style='width:100%;margin-top :80px'>
                                                                  <tr>
                                                                      <td style='text-align:left; width: 325px'><b>Courses</b></td>
                                                                      <td style='text-align:left'><b>Grade</b></td>
                                                                  </tr>")
                        ''second column (academic)
                        For Each row As DataRow In DS.Rows
                            Test.Append("<tr>")
                            For Each column As DataColumn In DS.Columns
                                Test.Append("<td style='text-align:left'>")
                                Test.Append(row(column.ColumnName))
                                Test.Append("</td>")
                            Next
                            Test.Append("</tr>")
                        Next

                        ''second column (cocuricullum)
                        If Confirm_Cocuricullum = "on" Then
                            For Each row As DataRow In DSCocuricullum.Rows
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("Cocurriculum")
                                Test.Append("</td>")
                                For Each column As DataColumn In DSCocuricullum.Columns
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                                Test.Append("</tr>")
                            Next
                        ElseIf Confirm_Cocuricullum = "off" Then
                            Test.Append("<tr>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("Cocurriculum")
                            Test.Append("</td>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("SM")
                            Test.Append("</td>")
                            Test.Append("</tr>")
                        End If

                        ''second column (research)
                        If Confirm_Research = "on" Then
                            For Each row As DataRow In DSResearch.Rows
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("Research Skill")
                                Test.Append("</td>")
                                For Each column As DataColumn In DSResearch.Columns
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                                Test.Append("</tr>")
                            Next
                        ElseIf Confirm_Research = "off" Then
                            Test.Append("<tr>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("Research")
                            Test.Append("</td>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("SM")
                            Test.Append("</td>")
                            Test.Append("</tr>")
                        End If

                        ''second column (Portfolio)
                        If Confirm_Portfolio = "on" Then
                            For Each row As DataRow In DSPortfolio.Rows
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("Portfolio")
                                Test.Append("</td>")
                                For Each column As DataColumn In DSPortfolio.Columns
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                                Test.Append("</tr>")
                            Next
                        ElseIf Confirm_Portfolio = "off" Then
                            Test.Append("<tr>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("Portfolio")
                            Test.Append("</td>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("SM")
                            Test.Append("</td>")
                            Test.Append("</tr>")
                        End If

                        ''second column (self development)
                        If Confirm_Self = "on" Then
                            For Each row As DataRow In DSSelfdevelopment.Rows
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("Self Development")
                                Test.Append("</td>")
                                For Each column As DataColumn In DSSelfdevelopment.Columns
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append(row(column.ColumnName))
                                    Test.Append("</td>")
                                Next
                                Test.Append("</tr>")
                            Next
                        ElseIf Confirm_Self = "off" Then
                            Test.Append("<tr>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("Self Develoment")
                            Test.Append("</td>")
                            Test.Append("<td style='text-align:left'>")
                            Test.Append("SM")
                            Test.Append("</td>")
                            Test.Append("</tr>")
                        End If

                        Dim get_ENG As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name = 'AP English Language and Composition' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                        Dim data_ENGLITERATURE As String = oCommon.getFieldValue(get_ENG)

                        If data_ENGLITERATURE.Length > 0 Then

                            ''second column (english literature)
                            If Confirm_Eng_Literature = "on" Then
                                For Each row As DataRow In DSEnglish_literature.Rows
                                    Test.Append("<tr>")
                                    Test.Append("<td style='text-align:left'>")
                                    Test.Append("English Literature And Composition")
                                    Test.Append("</td>")
                                    For Each column As DataColumn In DSEnglish_literature.Columns
                                        Test.Append("<td style='text-align:left'>")
                                        Test.Append(row(column.ColumnName))
                                        Test.Append("</td>")
                                    Next
                                    Test.Append("</tr>")
                                Next

                            ElseIf Confirm_Eng_Literature = "off" Then
                                Test.Append("<tr>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("English Literature And Composition")
                                Test.Append("</td>")
                                Test.Append("<td style='text-align:left'>")
                                Test.Append("SM")
                                Test.Append("</td>")
                                Test.Append("</tr>")
                            End If

                        End If

                        ''gpa & cgpa display and third column
                        Test.Append("               </table>
                                                              <div>_______________________________________________</div>
                                                              <table style='width:100%'>
                                                                  <tr>
                                                                      <td style='text-align: left; width: 325px'><b>GPA </b></td>
                                                                      <td style='text-align :left'><b>" & gpa & "</b></td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td style='text-align: Left'><b>CGPA </b></td>
                                                                      <td style='text-align: left'><b>" & cgpa & "</b></td>
                                                                  </tr>
                                                              </table>
                                                          </td>
                                                          <td style='width:33.33%'>
                                                              <table style='width: 100%; margin-top :260px; margin-left: 100px'>
                                                                  <tr style='width :100%'>
                                                                      <td colspan='2'>______________________________</td>
                                                                  </tr>
                                                                  <tr style='width: 100%'>
                                                                      <td colspan='2'>Signature of Director</td>
                                                                  </tr>
                                                                  <tr style='width:100%'>
                                                                      <td colspan='2'>" & Now.Day & " " & dataStdMonth & " " & Now.Year & "</td>
                                                                  </tr>
                                                                  <tr style='width :100%'>
                                                                      <td colspan='2'>
                                                            Pusat PERMATApintar® Negara,</td>
                                                                  </tr>
                                                                  <tr style='width :100%'>
                                                                      <td colspan='2'>Universiti Kebangsaan Malaysia,</td>
                                                                  </tr>
                                                                  <tr style='width :100%'>
                                                                      <td colspan='2'>43600 UKM Bangi,</td>
                                                                  </tr>
                                                                  <tr style='width :100%'>
                                                                      <td colspan='2'>Selangor Darul Ehsan.</td>
                                                                  </tr>
                                                                  <tr style='width :50%'>
                                                                      <td> Tel : </td>
                                                                      <td>03-89217529/7528/7508</td>
                                                                  </tr>
                                                                  <tr style='width :50%'>
                                                                      <td> Faks :  </td>
                                                                      <td>03-89217525</td>
                                                                  </tr>
                                                              </table>
                                                          </td>
                                                      </tr>
                                                  </table>
                                                                      </div>
                                              </div>")

                    End If
                End If
            Next

            Test.AppendLine("</div></div>")
            Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataTESTBI'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

            ''print
            Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

        End If
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select distinct student_info.std_ID, student_info.student_Name, student_info.student_Mykad, student_info.student_ID, student_level.student_Level,
                  student_Png.png, student_Png.pngs
                  from student_Png
                  left join student_info on student_Png.std_ID = student_info.std_ID
                  left join student_Level on student_info.std_ID = student_level.std_ID
                  left join course on student_info.std_ID = course.std_ID
                  left join class_info on course.class_ID = class_info.class_ID"
        strWhere = " where student_Png.year = '" & ddlyear.SelectedValue & "'"
        strWhere += " and class_info.class_Level = '" & ddlstudent_Year.SelectedValue & "'"
        strWhere += " and student_Png.exam_Name = '" & ddlexam_Name.SelectedValue & "'"

        If Not txtstudent.Text.Length = 0 Then
            strWhere += "and (student_info.student_ID LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " or student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " or student_info.student_Mykad LIKE '%" & txtstudent.Text & "%')"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    ''calculate png academic
    Public Function Cal_PNG(ByVal strKey As String)

        Dim check_png_exist As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
        Dim exist_png As String = getFieldValue(check_png_exist, strConn)

        Dim data_Student As String = ""


        Dim get_ENG As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                     where subject_info.subject_Name = 'AP English Language and Composition' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & strKey & "'"
        Dim data_ENGLITERATURE As String = oCommon.getFieldValue(get_ENG)

        If data_ENGLITERATURE.Length > 0 Then

            data_Student = oCommon.PNG_ENG(strKey, ddlexam_Name.SelectedValue, ddlyear.SelectedValue)

        ElseIf data_ENGLITERATURE.Length = 0 Then

            data_Student = oCommon.PNG(strKey, ddlexam_Name.SelectedValue, ddlyear.SelectedValue)

        End If

        Return data_Student

    End Function

    Public Function Cal_PNGK(ByVal strKey As String)

        Dim check_pngs_exist As String = "select pngs from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
        Dim exist_pngs As String = getFieldValue(check_pngs_exist, strConn)

        If exist_pngs = "0.00" Or exist_pngs = "0" Then

            ''check studdent_type
            Dim find_std_type As String = "select student_type from student_Png where std_ID = '" & strKey & "'"
            Dim data_std_type As String = oCommon.getFieldValue(find_std_type)


            '' count number of exam
            Dim CountExam As String = "Select count(exam_Name) from student_Png where std_ID = '" & strKey & "' and student_type = '" & data_std_type & "'"
            Dim dataCountExam As String = getFieldValue(CountExam, strConn)

            Dim dataCountExam_INT As Integer = Integer.Parse(dataCountExam)

            '' sum all png
            Dim SumPng As String = "select sum(png) from student_Png where std_ID = '" & strKey & "' and student_type = '" & data_std_type & "'"
            Dim dataSumPng As String = getFieldValue(SumPng, strConn)

            Dim dataSumPng_DEC As Decimal = Decimal.Parse(dataSumPng)

            Dim answer As Decimal = dataSumPng_DEC / dataCountExam_INT

            Dim pngk As Decimal = answer.ToString("F2")

            ''step to insert PNGK value into db
            strSQL = "UPDATE student_Png SET pngs ='" & pngk & "' WHERE std_ID ='" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Return pngk
        Else
            Return exist_pngs
        End If

    End Function

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Sub BtnExport_ServerClick(sender As Object, e As EventArgs) Handles BtnExport.ServerClick
        ExportToCSV(getSQL)
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

    'export 
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Exam_Transcript.csv")
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
End Class