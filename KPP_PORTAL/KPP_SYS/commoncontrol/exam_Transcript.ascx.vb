Imports System.Data.SqlClient

Public Class exam_Transcript
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

        Dim tmpSQL As String
        Dim errorCount As Integer = 0
        Dim i As Integer
        Dim Test As New StringBuilder()

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

                        ''get student level
                        Dim FindStudentLevel As String = "select distinct student_Level from student_Level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim GetStudentLevel As String = oCommon.getFieldValue(FindStudentLevel)

                        'get Portfolio percentage on / off
                        Dim check_portfolio_percen As String = "select Value from setting where Type = 'Portfolio " & GetStudentLevel & " Percentage'"
                        Dim Confirm_Portfolio As String = getFieldValue(check_portfolio_percen, strConn)

                        ''get cocuricullum percentage on / off
                        Dim check_cocuricullum_percen As String = "select Value from setting where Type = 'Cocurricular " & GetStudentLevel & " Percentage'"
                        Dim Confirm_Cocuricullum As String = getFieldValue(check_cocuricullum_percen, strConn)

                        ''get research percentage on / off
                        Dim check_research_percen As String = "select Value from setting where Type = 'Research " & GetStudentLevel & " Percentage'"
                        Dim Confirm_Research As String = getFieldValue(check_research_percen, strConn)

                        ''get self development percentage on / off
                        Dim check_self_percen As String = "select Value from setting where Type = 'Self Development " & GetStudentLevel & " Percentage'"
                        Dim Confirm_Self As String = getFieldValue(check_self_percen, strConn)

                        ''print subject name and grade
                        tmpSQL = "Select subject_info.subject_NameBM,exam_result.grade from exam_result  
                                  left join course on exam_result.course_ID=course.course_ID 
                                  left join student_info on course.std_ID=student_info.std_ID 
                                  left join subject_info on course.subject_ID=subject_info.subject_ID 
                                  left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                  where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                  And subject_info.subject_Name != 'Self Development' and  subject_info.subject_NameBM != 'Pembangunan Diri'
                                  and subject_info.subject_Name not like '%Personality Development%' and  subject_info.subject_NameBM not like '%Jati Diri%'
                                  And subject_info.subject_Name  Not Like '%Research Skill%' and  subject_info.subject_NameBM not like '%Kemahiran Penyelidikan%'
                                  And subject_info.subject_Name  Not Like '%Portfolio%' and  subject_info.subject_NameBM not like '%Portfolio%'"
                        Dim SQA As New SqlDataAdapter(tmpSQL, strConn)
                        Dim DS As New DataTable
                        Dim DSPortfolio As New DataTable
                        Dim DSResearch As New DataTable
                        Dim DSSelfdevelopment As New DataTable
                        Dim DSCocuricullum As New DataTable
                        SQA.SelectCommand.CommandTimeout = 120

                        ''print Portfolio
                        If Confirm_Portfolio = "on" Then
                            tmpSQL = "Select exam_result.grade from exam_result 
                                      left join course on exam_result.course_ID=course.course_ID 
                                      left join student_info on course.std_ID=student_info.std_ID 
                                      left join subject_info on course.subject_ID=subject_info.subject_ID 
                                      left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                      where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                      And (subject_info.subject_Name = 'Portfolio' or  subject_info.subject_NameBM = 'Portfolio')"
                            Dim SQPortfolio As New SqlDataAdapter(tmpSQL, strConn)
                            SQPortfolio.SelectCommand.CommandTimeout = 120

                            Try
                                SQPortfolio.Fill(DSPortfolio)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print research 
                        If Confirm_Research = "on" Then
                            tmpSQL = "Select exam_result.grade from exam_result 
                                      left join course on exam_result.course_ID=course.course_ID 
                                      left join student_info on course.std_ID=student_info.std_ID 
                                      left join subject_info on course.subject_ID=subject_info.subject_ID 
                                      left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                      where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                      And subject_info.subject_Name like '%Research Skill%' and  subject_info.subject_NameBM like '%Kemahiran Penyelidikan"
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
                                tmpSQL = "Select exam_result.grade from exam_result 
                                      left join course on exam_result.course_ID=course.course_ID 
                                      left join student_info on course.std_ID=student_info.std_ID 
                                      left join subject_info on course.subject_ID=subject_info.subject_ID 
                                      left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                      where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                      and (subject_info.subject_Name = 'Self Development' or  subject_info.subject_NameBM = 'Pembangunan Diri'
                                      or subject_info.subject_Name like '%Personality Development%' or  subject_info.subject_NameBM like '%Jati Diri%')
                                      and exam_result.grade is not null"

                            ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                tmpSQL = "Select exam_result.grade from exam_result 
                                      left join course on exam_result.course_ID=course.course_ID 
                                      left join student_info on course.std_ID=student_info.std_ID 
                                      left join subject_info on course.subject_ID=subject_info.subject_ID 
                                      left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                      where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                      and (subject_info.subject_Name = 'Self Development' or  subject_info.subject_NameBM = 'Pembangunan Diri'
                                      or subject_info.subject_Name like '%Personality Development%' or  subject_info.subject_NameBM like '%Jati Diri%')
                                      and exam_result.grade is not null"

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
                            If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 5" Then

                                Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                                Dim getStudent As String = getFieldValue(studentData, strConn)

                                tmpSQL = "select koko_pelajar.GredP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 6" Then

                                Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                                Dim getStudent As String = getFieldValue(studentData, strConn)

                                tmpSQL = "select koko_pelajar.GredP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 7" Then

                                Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                                Dim getStudent As String = getFieldValue(studentData, strConn)

                                tmpSQL = "select koko_pelajar.GredP3 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 4" Then

                                Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                                Dim getStudent As String = getFieldValue(studentData, strConn)

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
                        Dim stdName As String = "select student_Name from student_info where std_ID = '" & strKey & "'"
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
                        End If

                        ''get month
                        Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
                        Dim dataMonth As String = getFieldValue(month, strConn)

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
                                                                        <td colspan='2'>PROGRAM PENDIDIKAN</td>
                                                                    </tr>
                                                                    <tr style='width :100%'>
                                                                        <td colspan='2'>PERMATApintar®</td>
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
                                Test.Append("kokurikullum")
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
                            Test.Append("kokurikullum")
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
            ''          
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

                        ''get student level
                        Dim FindStudentLevel As String = "select distinct student_Level from student_Level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "'"
                        Dim GetStudentLevel As String = oCommon.getFieldValue(FindStudentLevel)

                        'get Portfolio percentage on / off
                        Dim check_portfolio_percen As String = "select Value from setting where Type = 'Portfolio " & GetStudentLevel & " Percentage'"
                        Dim Confirm_Portfolio As String = getFieldValue(check_portfolio_percen, strConn)

                        ''get cocuricullum percentage on / off
                        Dim check_cocuricullum_percen As String = "select Value from setting where Type = 'Cocurricular " & GetStudentLevel & " Percentage'"
                        Dim Confirm_Cocuricullum As String = getFieldValue(check_cocuricullum_percen, strConn)

                        ''get research percentage on / off
                        Dim check_research_percen As String = "select Value from setting where Type = 'Research " & GetStudentLevel & " Percentage'"
                        Dim Confirm_Research As String = getFieldValue(check_research_percen, strConn)

                        ''get self development percentage on / off
                        Dim check_self_percen As String = "select Value from setting where Type = 'Self Development " & GetStudentLevel & " Percentage'"
                        Dim Confirm_Self As String = getFieldValue(check_self_percen, strConn)

                        ''print academic
                        tmpSQL = "Select subject_info.subject_Name,exam_result.grade from exam_result 
                                  left join course on exam_result.course_ID=course.course_ID 
                                  left join student_info on course.std_ID=student_info.std_ID 
                                  left join subject_info on course.subject_ID=subject_info.subject_ID 
                                  left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                  where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                  And subject_info.subject_Name != 'Self Development' and  subject_info.subject_NameBM != 'Pembangunan Diri'
                                  and subject_info.subject_Name not like '%Personality Development%' and  subject_info.subject_NameBM not like '%Jati Diri%'
                                  And subject_info.subject_Name  Not Like '%Research Skill%' and  subject_info.subject_NameBM not like '%Kemahiran Penyelidikan%'
                                  And subject_info.subject_Name  Not Like '%Portfolio%' and  subject_info.subject_NameBM not like '%Portfolio%'"
                        Dim SQA As New SqlDataAdapter(tmpSQL, strConn)
                        Dim DS As New DataTable
                        Dim DSPortfolio As New DataTable
                        Dim DSResearch As New DataTable
                        Dim DSSelfdevelopment As New DataTable
                        Dim DSCocuricullum As New DataTable
                        SQA.SelectCommand.CommandTimeout = 120

                        ''print Portfolio
                        If Confirm_Portfolio = "on" Then
                            tmpSQL = "Select exam_result.grade from exam_result 
                                      left join course on exam_result.course_ID=course.course_ID 
                                      left join student_info on course.std_ID=student_info.std_ID 
                                      left join subject_info on course.subject_ID=subject_info.subject_ID 
                                      left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                      where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                      And subject_info.subject_Name = 'Portfolio' and  subject_info.subject_NameBM = 'Portfolio'"
                            Dim SQPortfolio As New SqlDataAdapter(tmpSQL, strConn)
                            SQPortfolio.SelectCommand.CommandTimeout = 120

                            Try
                                SQPortfolio.Fill(DSPortfolio)
                            Catch ex As Exception

                            End Try
                        End If

                        ''print research 
                        If Confirm_Research = "on" Then
                            tmpSQL = "Select exam_result.grade from exam_result 
                                      left join course on exam_result.course_ID=course.course_ID 
                                      left join student_info on course.std_ID=student_info.std_ID 
                                      left join subject_info on course.subject_ID=subject_info.subject_ID 
                                      left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                      where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                      And (subject_info.subject_Name like '%Research Skill%' or  subject_info.subject_NameBM like '%Kemahiran Penyelidikan%')"
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
                                tmpSQL = "Select exam_result.grade from exam_result 
                                      left join course on exam_result.course_ID=course.course_ID 
                                      left join student_info on course.std_ID=student_info.std_ID 
                                      left join subject_info on course.subject_ID=subject_info.subject_ID 
                                      left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                      where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                      and (subject_info.subject_Name = 'Self Development' or  subject_info.subject_NameBM = 'Pembangunan Diri'
                                      or subject_info.subject_Name like '%Personality Development%' or  subject_info.subject_NameBM like '%Jati Diri%')
                                      and exam_result.grade is not null"

                            ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                                tmpSQL = "Select exam_result.grade from exam_result 
                                      left join course on exam_result.course_ID=course.course_ID 
                                      left join student_info on course.std_ID=student_info.std_ID 
                                      left join subject_info on course.subject_ID=subject_info.subject_ID 
                                      left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                      where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                      and (subject_info.subject_Name = 'Self Development' or  subject_info.subject_NameBM = 'Pembangunan Diri'
                                      or subject_info.subject_Name like '%Personality Development%' or  subject_info.subject_NameBM like '%Jati Diri%')
                                      and exam_result.grade is not null"

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
                            If ddlexam_Name.SelectedValue = "Exam 1" Or ddlexam_Name.SelectedValue = "Exam 5" Then

                                Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                                Dim getStudent As String = getFieldValue(studentData, strConn)

                                tmpSQL = "select koko_pelajar.GredP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 2" Or ddlexam_Name.SelectedValue = "Exam 6" Then

                                Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                                Dim getStudent As String = getFieldValue(studentData, strConn)

                                tmpSQL = "select koko_pelajar.GredP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 3" Or ddlexam_Name.SelectedValue = "Exam 7" Then

                                Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                                Dim getStudent As String = getFieldValue(studentData, strConn)

                                tmpSQL = "select koko_pelajar.GredP3 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlexam_Name.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                                Dim SQCocuricullum As New SqlDataAdapter(tmpSQL, permataConn)
                                SQCocuricullum.SelectCommand.CommandTimeout = 120

                                Try
                                    SQCocuricullum.Fill(DSCocuricullum)
                                Catch ex As Exception

                                End Try

                            ElseIf ddlexam_Name.SelectedValue = "Exam 4" Then

                                Dim studentData As String = "Select student_Mykad from student_info where std.ID = '" & strKey & "'"
                                Dim getStudent As String = getFieldValue(studentData, strConn)

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
                                                                                  <td colspan='2'>EDUCATIONAL PROGRAMS</td>
                                                                              </tr>
                                                                              <tr style='width :100%'>
                                                                                  <td colspan='2'>PERMATApintar®</td>
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
                                Test.Append("Co-Curicullum")
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
                            Test.Append("Cocuricullum")
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

        tmpSQL = "select distinct student_info.std_ID, student_info.student_Name, student_info.student_Mykad, student_level.student_Level,
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
            strWhere += " or student_info.student_ID LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " or student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " or student_info.student_Mykad LIKE '%" & txtstudent.Text & "%'"
        End If


        'tmpSQL = "select distinct student_info.std_ID,
        '          student_info.student_Name,
        '          student_info.student_Mykad,
        '          student_level.student_Level
        '          from exam_result 
        '          left join course on exam_result.course_ID = course.course_ID
        '          left join exam_info on exam_result.exam_ID = exam_Info.exam_ID
        '          left join student_info on course.std_ID = student_info.std_ID
        '          left join student_level on course.ID = student_level.ID"
        'strWhere = " WHERE exam_info.exam_Year = '" & ddlyear.SelectedValue & "'"

        'If ddlstudent_Year.SelectedValue <> "" Then
        '    strWhere += " AND student_level.student_Level ='" & ddlstudent_Year.SelectedValue & "'"
        'End If

        'If ddlexam_Name.SelectedValue <> "" Then
        '    strWhere += " AND exam_Info.exam_Name ='" & ddlexam_Name.SelectedValue & "'"
        'End If

        'If Not txtstudent.Text.Length = 0 Then
        '    strWhere += " OR student_info.student_ID LIKE '%" & txtstudent.Text & "%'"
        'End If

        'If Not txtstudent.Text.Length = 0 Then
        '    strWhere += " OR student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
        'End If

        'If Not txtstudent.Text.Length = 0 Then
        '    strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent.Text & "%'"
        'End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    ''calculate png academic
    Public Function Cal_PNG(ByVal strKey As String)

        Dim check_png_exist As String = "select png from student_Png where std_ID = '" & strKey & "' and exam_Name = '" & ddlexam_Name.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
        Dim exist_png As String = getFieldValue(check_png_exist, strConn)

        If exist_png = "0.00" Or exist_png = "0" Then
            ''calculate (grade value*credit hour) * count subject without 'Pembangunan Diri and Penyelidikan and kokurikullum and Portfolio'
            Dim Calsumgrade As String = ""
            Calsumgrade = "select sum(gpa * subject_info.subject_CreditHour) 
                            from grade_info left join exam_result on grade_info.grade_Name=exam_result.grade    
                            left join course on exam_result.course_ID=course.course_ID 
                            left join student_info on course.std_ID=student_info.std_ID 
                            left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                            left join subject_info on course.subject_ID=subject_info.subject_ID 
                            where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                            and subject_info.subject_Name != 'Self Development' and  subject_info.subject_NameBM != 'Pembangunan Diri'
                            and subject_info.subject_Name not like '%Personality Development%' and  subject_info.subject_NameBM not like '%Jati Diri%'
                            and subject_info.subject_Name  not like '%Research Skill%' and  subject_info.subject_NameBM not like '%Kemahiran Penyelidikan%'
                            and subject_info.subject_Name  not like '%Portfolio%' and  subject_info.subject_NameBM not like '%Portfolio%'"
            Dim valueA As Decimal = Decimal.Parse(getFieldValue(Calsumgrade, strConn))

            ''calculate (credit hour * count subject) without 'Pembangunan Diri and Penyelidikan and kokurikullum and Portfolio'
            Dim Calsumcredithour As String = ""
            Calsumcredithour = "Select sum(subject_info.subject_CreditHour) 
                                from grade_info left join exam_result on grade_info.grade_Name=exam_result.grade 
                                left join course on exam_result.course_ID=course.course_ID 
                                left join student_info on course.std_ID=student_info.std_ID 
                                left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                left join subject_info on course.subject_ID=subject_info.subject_ID 
                                where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                and subject_info.subject_Name != 'Self Development' and  subject_info.subject_NameBM != 'Pembangunan Diri'
                                and subject_info.subject_Name not like '%Personality Development%' and  subject_info.subject_NameBM not like '%Jati Diri%'
                                and subject_info.subject_Name  not like '%Research Skill%' and  subject_info.subject_NameBM not like '%Kemahiran Penyelidikan%'
                                and subject_info.subject_Name  not like '%Portfolio%' and  subject_info.subject_NameBM not like '%Portfolio%'"
            Dim valueB As Decimal = Decimal.Parse(getFieldValue(Calsumcredithour, strConn))

            Dim png As Decimal
            Dim answer As Decimal

            If valueA <> 0.0 And valueB <> 0.0 Then
                answer = valueA / valueB
            End If

            If answer <> 0.00 Then
                ''round to two decimal places and display
                png = answer.ToString("F2")
            Else
                png = 0.00
            End If

            ''get student level
            Dim FindStudentLevel As String = "select distinct student_Level from student_Level where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "'"
            Dim GetStudentLevel As String = oCommon.getFieldValue(FindStudentLevel)

            Dim check_academic_percen As String = ""
            Dim Confirm_Academic As String = ""
            Dim check_portfolio_percen As String = ""
            Dim Confirm_Portfolio As String = ""
            Dim check_cocuricullum_percen As String = ""
            Dim Confirm_Cocuricullum As String = ""
            Dim check_research_percen As String = ""
            Dim Confirm_Research As String = ""
            Dim check_self_percen As String = ""
            Dim Confirm_Self As String = ""

            ''get Portfolio percentage on / off
            check_academic_percen = "select Value from setting where Type = 'Academic " & GetStudentLevel & " Percentage'"
            Confirm_Academic = oCommon.getFieldValue(check_portfolio_percen)

            ''get Portfolio percentage on / off
            check_portfolio_percen = "select Value from setting where Type = 'Portfolio " & GetStudentLevel & " Percentage'"
            Confirm_Portfolio = oCommon.getFieldValue(check_portfolio_percen)

            ''get cocuricullum percentage on / off
            check_cocuricullum_percen = "select Value from setting where Type = 'Cocurricular " & GetStudentLevel & " Percentage'"
            Confirm_Cocuricullum = oCommon.getFieldValue(check_cocuricullum_percen)

            ''get research percentage on / off
            check_research_percen = "select Value from setting where Type = 'Research " & GetStudentLevel & " Percentage'"
            Confirm_Research = oCommon.getFieldValue(check_research_percen)

            ''get self development percentage on / off
            check_self_percen = "select Value from setting where Type = 'Self Development " & GetStudentLevel & " Percentage'"
            Confirm_Self = oCommon.getFieldValue(check_self_percen)

            Dim PNG_Academic As Decimal
            Dim PNG_Cocuricullum As Decimal
            Dim PNG_PortFolio As Decimal
            Dim PNG_Research As Decimal
            Dim PNG_SelfDevelopment As Decimal

            Dim percen_selfdevelopment As Decimal
            Dim percen_cocuricullum As Decimal
            Dim percen_academic As Decimal
            Dim percen_research As Decimal
            Dim percen_portfolio As Decimal

            '' Confirm Academic
            If Confirm_Academic = "on" Then
                Dim data_academic_percen As String = "select Parameter from setting where Type = 'Academic " & GetStudentLevel & " Percentage'"
                percen_academic = Decimal.Parse(getFieldValue(data_academic_percen, strConn))

                PNG_Academic = png
            Else
                percen_academic = 0.0
                PNG_Academic = 0.0
            End If

            '' Confirm Portfolio
            If Confirm_Portfolio = "on" Then
                Dim data_portfolio_percen As String = "select Parameter from setting where Type = 'Portfolio " & GetStudentLevel & " Percentage'"
                percen_portfolio = Decimal.Parse(getFieldValue(data_portfolio_percen, strConn))

                Dim get_portfolio As String = "Select gpa
                                              from grade_info left join exam_result on grade_info.grade_Name=exam_result.grade 
                                              left join course on exam_result.course_ID=course.course_ID 
                                              left join student_info on course.std_ID=student_info.std_ID 
                                              left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                              left join subject_info on course.subject_ID=subject_info.subject_ID 
                                              where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & " and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                              and subject_info.subject_Name like '%Portfolio%' and  subject_info.subject_NameBM like '%Portfolio%'"
                PNG_PortFolio = Decimal.Parse(getFieldValue(get_portfolio, strConn))

                PNG_PortFolio = 0.0
            Else
                percen_portfolio = 0.0
                PNG_PortFolio = 0.0
            End If

            '' Confirm Research
            If Confirm_Research = "on" Then
                Dim data_research_percen As String = "select Parameter from setting where Type = 'Research " & GetStudentLevel & " Percentage'"
                percen_research = Decimal.Parse(getFieldValue(data_research_percen, strConn))

                Dim get_research As String = "Select gpa
                                              from grade_info left join exam_result on grade_info.grade_Name=exam_result.grade 
                                              left join course on exam_result.course_ID=course.course_ID 
                                              left join student_info on course.std_ID=student_info.std_ID 
                                              left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                              left join subject_info on course.subject_ID=subject_info.subject_ID 
                                              where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & " and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                              and subject_info.subject_Name like '%Research Skill%' and  subject_info.subject_NameBM like '%Kemahiran Penyelidikan%'"
                PNG_Research = Decimal.Parse(getFieldValue(get_research, strConn))

                PNG_Research = 0.0

            Else
                percen_research = 0.0
                PNG_Research = 0.0
            End If

            '' Confirm Cocuricullum
            If Confirm_Cocuricullum = "on" Then
                Dim data_cocuricullum_percen As String = "select Parameter from setting where Type = 'Cocurricular " & GetStudentLevel & " Percentage'"
                percen_cocuricullum = Decimal.Parse(getFieldValue(data_cocuricullum_percen, strConn))

                PNG_Cocuricullum = 3.55 ''this is dummy data for test 
            Else
                percen_cocuricullum = 0.0
                PNG_Cocuricullum = 0.0
            End If

            '' Confirm Self Developmemnt
            If Confirm_Self = "on" Then
                Dim data_self_percen As String = "select Parameter from setting where Type = 'Self Development " & GetStudentLevel & " Percentage'"
                percen_selfdevelopment = Decimal.Parse(getFieldValue(data_self_percen, strConn))

                Dim get_selfdevelopment As String = "Select gpa
                                                    from grade_info left join exam_result on grade_info.grade_Name=exam_result.grade 
                                                    left join course on exam_result.course_ID=course.course_ID 
                                                    left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                                    left join subject_info on course.subject_ID=subject_info.subject_ID 
                                                    where course.std_ID = '" & strKey & "' and exam_Info.exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "'
                                                    and (subject_info.subject_Name = 'Self Development' or  subject_info.subject_NameBM = 'Pembangunan Diri'
                                                    or subject_info.subject_Name like '%Personality Development%' or  subject_info.subject_NameBM like '%Jati Diri%')"
                PNG_SelfDevelopment = Decimal.Parse(getFieldValue(get_selfdevelopment, strConn))

            Else
                percen_selfdevelopment = 0.0
                PNG_SelfDevelopment = 0.0
            End If

            '' Calculate PNGS - current semester png
            Dim PNGS_academic As Decimal = (PNG_Academic / 4) * percen_academic
            Dim PNGS_cocuricullum As Decimal = (PNG_Cocuricullum / 4) * percen_cocuricullum
            Dim PNGS_portfolio As Decimal = (PNG_PortFolio / 4) * percen_portfolio
            Dim PNGS_research As Decimal = (PNG_Research / 4) * percen_research
            Dim PNGS_selfdevelopment As Decimal = (PNG_SelfDevelopment / 4) * percen_selfdevelopment


            Dim total_percentage As Decimal = percen_cocuricullum + percen_academic + percen_portfolio + percen_research + percen_selfdevelopment

            Dim sun_pngk As Decimal = (PNGS_academic + PNGS_cocuricullum + PNGS_portfolio + PNGS_research + PNGS_selfdevelopment)
            Dim PNGS As Decimal = (sun_pngk * 4) / total_percentage
            PNGS = PNGS.ToString("F2")


            ''step to insert PNG value into db
            ''select exam code
            Dim exmCode As String = ""
            Dim dataExmCode As String = ""

            exmCode = "select exam_Name from exam_Info where exam_Name = '" & ddlexam_Name.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "'"
            dataExmCode = getFieldValue(exmCode, strConn)

            ''check student data if already exist
            Dim exist As String = ""
            Dim dataExist As String = ""

            exist = "select exam_Name from student_Png where std_ID = '" & strKey & "' and year = '" & ddlyear.SelectedValue & "' and exam_Name = '" & dataExmCode & "'"
            dataExist = getFieldValue(exist, strConn)

            If dataExist <> dataExmCode Then
                '' save in db
                Using PNGDATA As New SqlCommand("INSERT into student_Png(std_ID,exam_Name,year,png) values ('" & strKey & "','" & dataExmCode & "','" & ddlyear.SelectedValue & "','" & PNGS & "')", objConn)
                    objConn.Open()
                    Dim j = PNGDATA.ExecuteNonQuery()
                    objConn.Close()
                End Using

            ElseIf dataExist = dataExmCode Then

                ''update grades and gpa
                strSQL = "UPDATE student_Png SET png ='" & PNGS & "' WHERE std_ID ='" & strKey & "' and exam_Name = '" & dataExmCode & "' and year = '" & ddlyear.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

            Return PNGS
        Else
            Return exist_png
        End If

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

    Private Sub BtnGenerate_ServerClick(sender As Object, e As EventArgs) Handles BtnGenerate.ServerClick

        Dim i As Integer

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim gpa As Decimal = Cal_PNG(strKey)
                    Dim cgpa As Decimal = Cal_PNGK(strKey)

                End If
            End If

        Next

        strRet = BindData(datRespondent)

    End Sub
End Class