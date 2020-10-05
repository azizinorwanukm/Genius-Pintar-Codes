Imports System.Data.SqlClient

Public Class exam_Official_Transcript
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                student_info_list()

                load_page()
                class_info_list()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT year from student_Level where year ='" & Now.Year & "'"

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
                ddlstudentYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlstudentYear.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub class_info_list()
        strSQL = "select idx from setting where Parameter = 'student_Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlclassID.DataSource = ds
            ddlclassID.DataTextField = "idx"
            ddlclassID.DataValueField = "idx"
            ddlclassID.DataBind()
            ddlclassID.Items.Insert(0, New ListItem("Select Transcript", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_info_list()
        strSQL = "select Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstudentYear.DataSource = ds
            ddlstudentYear.DataTextField = "Parameter"
            ddlstudentYear.DataValueField = "Parameter"
            ddlstudentYear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select distinct student_png.std_ID, student_info.student_Name, student_info.student_ID, student_info.student_Mykad, student_info.student_Email from student_Png
                  left join student_info on student_Png.std_ID = student_info.std_ID
                  left join student_level on student_info.std_ID = student_level.std_ID"
        strWhere = " WHERE student_Png.year = '" & ddlstudentYear.SelectedValue & "'"

        If ddlclassID.SelectedValue = "High School" Then
            strWhere += " and student_Png.student_type = 'TAHAP'
                         and student_level.year = '" & ddlstudentYear.SelectedValue & "'
                         and student_level.student_level = 'Level 2'"

        ElseIf ddlclassID.SelectedValue = "Junior School" Then
            strWhere += " and student_Png.student_type = 'ASAS'
                         and student_level.year = '" & ddlstudentYear.SelectedValue & "'
                         and student_level.student_level = 'Foundation 3'"
        End If

        If Not txtStdName.Text.Length = 0 Then
            strWhere += " AND student_info.student_Name like '%" & txtStdName.Text & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
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

    Private Sub Btnprint_ServerClick(sender As Object, e As EventArgs) Handles Btnprint.ServerClick

        Dim tmpSQL As String
        Dim errorCount As Integer = 0
        Dim i As Integer
        Dim count_i As Integer = 0
        Dim Test As New StringBuilder()

        Dim find_director As String = "select Parameter from setting where idx = 'Examination' and Type = 'Director Permata Pintar'"
        Dim get_director As String = oCommon.getFieldValue(find_director)

        Dim find_job As String = "select Value from setting where idx = 'Examination' and Type = 'Director Permata Pintar'"
        Dim get_job As String = oCommon.getFieldValue(find_job)

        'For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
        '    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
        '    If Not chkUpdate Is Nothing Then

        '        If chkUpdate.Checked = True Then
        '            count_i += 1
        '        End If

        '    End If
        'Next

        ''check print transcript language
        If rbtn_Malay.Checked = True Then
            rbtn_English.Checked = False

            Test.AppendLine("<div id='data' style='display:none'>")
            Test.AppendLine("<div id='dataOfficialBM'>")

            If ddlclassID.SelectedValue = "Junior School" Then
                ''print junior school format transcript
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''get student  date
                            Dim stdYear As String = "select Min(year) from student_level where std_ID = '" & strKey & "'"
                            Dim dataTest As String = getFieldValue(stdYear, strConn)
                            Dim dataStdYear As Integer = Integer.Parse(dataTest)

                            ''print student name
                            Dim stdName As String = "select student_Name from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdName As String = getFieldValue(stdName, strConn)

                            ''print student id
                            Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                            ''print student mykad
                            Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdMykad As String = getFieldValue(stdMykad, strConn)

                            ''get student entry date / term ending from config function
                            Dim find_Junior_Entry_Date As String = "select Value from setting where idx = 'Examination' and Type = 'Junior Entry Date'"
                            Dim get_Junior_Entry_Date As String = oCommon.getFieldValue(find_Junior_Entry_Date)

                            Dim find_Junior_Term_Ending As String = "select Value from setting where idx = 'Examination' and Type = 'Junior Term Ending'"
                            Dim get_Junior_Term_Ending As String = oCommon.getFieldValue(find_Junior_Term_Ending)

                            ''get student end year (sudah tamat)
                            Dim juniorEndYear As Integer = dataStdYear + 2

                            ''looping year to get exam year
                            For year As Integer = dataStdYear To (dataStdYear + 2)

                                ''year 1
                                If year = dataStdYear Then

                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 1
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result 
                                              left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID 
                                              left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                              WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAONE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMONE As New DataTable
                                    SQAONE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAONE.Fill(EXAMONE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 1
                                    Dim gpaExam1 As String = ""
                                    Dim datagpaExam1 As String = ""
                                    gpaExam1 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name 
                                                where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and student_Png.year = '" & year & "'"
                                    datagpaExam1 = getFieldValue(gpaExam1, strConn)

                                    Dim cgpaExam1 As String = ""
                                    Dim datacgpaExam1 As String = ""
                                    cgpaExam1 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name 
                                                 where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and student_Png.year = '" & year & "'"
                                    datacgpaExam1 = getFieldValue(cgpaExam1, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 2
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result 
                                              left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID 
                                              left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                              WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATWO As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTWO As New DataTable
                                    SQATWO.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATWO.Fill(EXAMTWO)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 2
                                    Dim gpaExam2 As String = ""
                                    Dim datagpaExam2 As String = ""
                                    gpaExam2 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name 
                                                where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and student_Png.year = '" & year & "'"
                                    datagpaExam2 = getFieldValue(gpaExam2, strConn)

                                    Dim cgpaExam2 As String = ""
                                    Dim datacgpaExam2 As String = ""
                                    cgpaExam2 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID 
                                                 left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name 
                                                 where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and student_Png.year = '" & year & "'"
                                    datacgpaExam2 = getFieldValue(cgpaExam2, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOONE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOONE As New DataTable
                                    SQAKOKOONE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOONE.Fill(EXAMKOKOONE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 3
                                    tmpSQL = "Select subject_info.subject_Code, subject_info.subject_NameBM, grade, subject_info.subject_CreditHour from exam_result 
                                              Left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID 
                                              left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                              WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATHREE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTHREE As New DataTable
                                    SQATHREE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATHREE.Fill(EXAMTHREE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 3
                                    Dim gpaExam3 As String = ""
                                    Dim datagpaExam3 As String = ""
                                    gpaExam3 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name 
                                                where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and student_Png.year = '" & year & "'"
                                    datagpaExam3 = getFieldValue(gpaExam3, strConn)

                                    Dim cgpaExam3 As String = ""
                                    Dim datacgpaExam3 As String = ""
                                    cgpaExam3 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID 
                                                 left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name 
                                                 where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and student_Png.year = '" & year & "'"
                                    datacgpaExam3 = getFieldValue(cgpaExam3, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 4
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result 
                                              left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID 
                                              left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID 
                                              WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAFOUR As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMFOUR As New DataTable
                                    SQAFOUR.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAFOUR.Fill(EXAMFOUR)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 4
                                    Dim gpaExam4 As String = ""
                                    Dim datagpaExam4 As String = ""
                                    gpaExam4 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name 
                                                where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and student_Png.year = '" & year & "'"
                                    datagpaExam4 = getFieldValue(gpaExam4, strConn)

                                    Dim cgpaExam4 As String = ""
                                    Dim datacgpaExam4 As String = ""
                                    cgpaExam4 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID 
                                                 left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name 
                                                  where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and student_Png.year = '" & year & "'"
                                    datacgpaExam4 = getFieldValue(cgpaExam4, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOTWO As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOTWO As New DataTable
                                    SQAKOKOTWO.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOTWO.Fill(EXAMKOKOTWO)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                        <table style='width:100%'>
                                            <tr>
                                                <td style='width :26%'>
                                                    <table style='width:100%;margin-top:70px'>
                                                        <tr>
                                                            <td>
                                                                <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>TRANSKRIP RASMI</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Nama : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Pendaftaran : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Kad Pengenalan : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2's>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Junior_Entry_Date & " </font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Junior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td class ='printRow' style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 1 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 1
                                    For Each row As DataRow In EXAMONE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMONE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam1 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam1 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 2 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")

                                    'data for exam 2
                                    For Each row As DataRow In EXAMTWO.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTWO.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'koko data for exam 2
                                    For Each row As DataRow In EXAMKOKOONE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOONE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam2 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam2 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width: 33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 3 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                    </tr>")

                                    'data for exam 3
                                    For Each row As DataRow In EXAMTHREE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTHREE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam3 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam3 & "</b></font></td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table style ='margin-top:0px;width:100%'>
                                                <tr>
                                                    <td style='background-color:#FC747C'>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 4 </b></font></p>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style ='width: 100%'>
                                                        <div class='table-responsive'>
                                                            <p></p>
                                                            <table>
                                                                <tr>
                                                                    <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                    <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                </tr>")

                                    'data for exam 4
                                    For Each row As DataRow In EXAMFOUR.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMFOUR.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'koko data for exam 4
                                    For Each row As DataRow In EXAMKOKOTWO.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOTWO.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                                <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam4 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam4 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                    ''year 2
                                ElseIf year = (dataStdYear + 1) Then

                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 5
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAFIVE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMFIVE As New DataTable
                                    SQAFIVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAFIVE.Fill(EXAMFIVE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 5
                                    Dim gpaExam5 As String = ""
                                    Dim datagpaExam5 As String = ""
                                    gpaExam5 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and student_Png.year = '" & year & "'"
                                    datagpaExam5 = getFieldValue(gpaExam5, strConn)

                                    Dim cgpaExam5 As String = ""
                                    Dim datacgpaExam5 As String = ""
                                    cgpaExam5 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and student_Png.year = '" & year & "'"
                                    datacgpaExam5 = getFieldValue(cgpaExam5, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 6
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQASIX As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMSIX As New DataTable
                                    SQASIX.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQASIX.Fill(EXAMSIX)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 6
                                    Dim gpaExam6 As String = ""
                                    Dim datagpaExam6 As String = ""
                                    gpaExam6 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and student_Png.year = '" & year & "'"
                                    datagpaExam6 = getFieldValue(gpaExam6, strConn)

                                    Dim cgpaExam6 As String = ""
                                    Dim datacgpaExam6 As String = ""
                                    cgpaExam6 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and student_Png.year = '" & year & "'"
                                    datacgpaExam6 = getFieldValue(cgpaExam6, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOTHREE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOTHREE As New DataTable
                                    SQAKOKOTHREE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOTHREE.Fill(EXAMKOKOTHREE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 7
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQASEVEN As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMSEVEN As New DataTable
                                    SQASEVEN.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQASEVEN.Fill(EXAMSEVEN)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 7
                                    Dim gpaExam7 As String = ""
                                    Dim datagpaExam7 As String = ""
                                    gpaExam7 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and student_Png.year = '" & year & "'"
                                    datagpaExam7 = getFieldValue(gpaExam7, strConn)

                                    Dim cgpaExam7 As String = ""
                                    Dim datacgpaExam7 As String = ""
                                    cgpaExam7 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and student_Png.year = '" & year & "'"
                                    datacgpaExam7 = getFieldValue(cgpaExam7, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 8
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 8' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAEIGHT As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMEIGHT As New DataTable
                                    SQAEIGHT.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAEIGHT.Fill(EXAMEIGHT)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 8
                                    Dim gpaExam8 As String = ""
                                    Dim datagpaExam8 As String = ""
                                    gpaExam8 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 8' and student_Png.year = '" & year & "'"
                                    datagpaExam8 = getFieldValue(gpaExam8, strConn)

                                    Dim cgpaExam8 As String = ""
                                    Dim datacgpaExam8 As String = ""
                                    cgpaExam8 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 8' and student_Png.year = '" & year & "'"
                                    datacgpaExam8 = getFieldValue(cgpaExam8, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOFOUR As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOFOUR As New DataTable
                                    SQAKOKOFOUR.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOFOUR.Fill(EXAMKOKOFOUR)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                        <table style='width:100%'>
                                            <tr>
                                                <td style='width :26%'>
                                                    <table style='width:100%;margin-top:70px'>
                                                        <tr>
                                                            <td>
                                                                <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>TRANSKRIP RASMI</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Nama : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Pendaftaran : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Kad Pengenalan : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2's>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                       <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Junior_Entry_Date & " </font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Junior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td class ='printRow' style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 5 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 5
                                    For Each row As DataRow In EXAMFIVE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMFIVE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam5 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam5 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 6 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")

                                    'data for exam 6
                                    For Each row As DataRow In EXAMSIX.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMSIX.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'koko data for exam 6
                                    For Each row As DataRow In EXAMKOKOTHREE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOTHREE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam6 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam6 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width: 33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 7 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                    </tr>")

                                    'data for exam 7
                                    For Each row As DataRow In EXAMSEVEN.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMSEVEN.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam7 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam7 & "</b></font></td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table style ='margin-top:0px;width:100%'>
                                                <tr>
                                                    <td style='background-color:#FC747C'>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 8 </b></font></p>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style ='width: 100%'>
                                                        <div class='table-responsive'>
                                                            <p></p>
                                                            <table>
                                                                <tr>
                                                                    <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                    <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                </tr>")

                                    'data for exam 8
                                    For Each row As DataRow In EXAMEIGHT.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMEIGHT.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'koko data for exam 8
                                    For Each row As DataRow In EXAMKOKOFOUR.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOFOUR.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                                <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam8 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam8 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")


                                    ''year 3
                                ElseIf year = (dataStdYear + 2) Then

                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 9
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 9' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQANINE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMNINE As New DataTable
                                    SQANINE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQANINE.Fill(EXAMNINE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 9
                                    Dim gpaExam9 As String = ""
                                    Dim datagpaExam9 As String = ""
                                    gpaExam9 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 9' and student_Png.year = '" & year & "'"
                                    datagpaExam9 = getFieldValue(gpaExam9, strConn)

                                    Dim cgpaExam9 As String = ""
                                    Dim datacgpaExam9 As String = ""
                                    cgpaExam9 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 9' and student_Png.year = '" & year & "'"
                                    datacgpaExam9 = getFieldValue(cgpaExam9, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 10
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 10' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATEN As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTEN As New DataTable
                                    SQATEN.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATEN.Fill(EXAMTEN)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 10
                                    Dim gpaExam10 As String = ""
                                    Dim datagpaExam10 As String = ""
                                    gpaExam10 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 10' and student_Png.year = '" & year & "'"
                                    datagpaExam10 = getFieldValue(gpaExam10, strConn)

                                    Dim cgpaExam10 As String = ""
                                    Dim datacgpaExam10 As String = ""
                                    cgpaExam10 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 10' and student_Png.year = '" & year & "'"
                                    datacgpaExam10 = getFieldValue(cgpaExam10, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOFIVE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOFIVE As New DataTable
                                    SQAKOKOFIVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOFIVE.Fill(EXAMKOKOFIVE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 11
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 11' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAELEVEN As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMELEVEN As New DataTable
                                    SQAELEVEN.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAELEVEN.Fill(EXAMELEVEN)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 11
                                    Dim gpaExam11 As String = ""
                                    Dim datagpaExam11 As String = ""
                                    gpaExam11 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 11' and student_Png.year = '" & year & "'"
                                    datagpaExam11 = getFieldValue(gpaExam11, strConn)

                                    Dim cgpaExam11 As String = ""
                                    Dim datacgpaExam11 As String = ""
                                    cgpaExam11 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 11' and student_Png.year = '" & year & "'"
                                    datacgpaExam11 = getFieldValue(cgpaExam11, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 12
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 12' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATWELVE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTWELVE As New DataTable
                                    SQATWELVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATWELVE.Fill(EXAMTWELVE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 12
                                    Dim gpaExam12 As String = ""
                                    Dim datagpaExam12 As String = ""
                                    gpaExam12 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 12' and student_Png.year = '" & year & "'"
                                    datagpaExam12 = getFieldValue(gpaExam12, strConn)

                                    Dim cgpaExam12 As String = ""
                                    Dim datacgpaExam12 As String = ""
                                    cgpaExam12 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 12' and student_Png.year = '" & year & "'"
                                    datacgpaExam12 = getFieldValue(cgpaExam12, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOSIX As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOSIX As New DataTable
                                    SQAKOKOSIX.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOSIX.Fill(EXAMKOKOSIX)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                        <table style='width:100%'>
                                            <tr>
                                                <td style='width :26%'>
                                                    <table style='width:100%;margin-top:70px'>
                                                        <tr>
                                                            <td>
                                                                <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>TRANSKRIP RASMI</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Nama : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Pendaftaran : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Kad Pengenalan : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2's>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Junior_Entry_Date & " </font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Junior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td class ='printRow' style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 9 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 9
                                    For Each row As DataRow In EXAMNINE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMNINE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam9 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam9 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 10 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")

                                    'data for exam 10
                                    For Each row As DataRow In EXAMTEN.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTEN.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    For Each row As DataRow In EXAMKOKOFIVE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOFIVE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam10 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam10 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width: 33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 11 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                    </tr>")

                                    'data for exam 11
                                    For Each row As DataRow In EXAMELEVEN.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMELEVEN.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam11 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam11 & "</b></font></td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table style ='margin-top:0px;width:100%'>
                                                <tr>
                                                    <td style='background-color:#FC747C'>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 12 </b></font></p>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style ='width: 100%'>
                                                        <div class='table-responsive'>
                                                            <p></p>
                                                            <table>
                                                                <tr>
                                                                    <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                    <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                </tr>")

                                    'data for exam 12
                                    For Each row As DataRow In EXAMTWELVE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTWELVE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    For Each row As DataRow In EXAMKOKOSIX.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOSIX.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                                <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam12 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam12 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                End If

                            Next

                            ''printing format
                            Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                        <table style='width:100%'>
                                            <tr>
                                                <td style='width :26%'>
                                                    <table style='width:100%; margin-top:70px'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>TRANSKRIP RASMI</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Nama : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Pendaftaran : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Kad Pengenalan : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                       <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Junior_Entry_Date & " </font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Junior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :74%;margin-left:20px'>
                                                    <p style='margin-bottom:0px;margin-top: 0px'>____________________</p>
                                                    <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>" & get_director & "</b></font</p>
                                                    <p style='margin-bottom:0px;margin-top: 0px'>" & get_job & " ,</p>
                                                    <p style='margin-bottom:0px;margin-top: 0px'>Pusat PERMATApintar® Negara</p>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                        End If
                    End If
                Next

                Test.AppendLine("</div> </div>")
                Test.AppendLine("<script language=javascript> var divToPrint=document.getElementById('dataOfficialBM'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close();</script>")

                ''print
                Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

            ElseIf ddlclassID.SelectedValue = "High School" Then
                ''print high school format transcript
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''get student register date
                            Dim stdYear As String = "select Min(year) from student_level where std_ID = '" & strKey & "' "
                            Dim dataTest As String = getFieldValue(stdYear, strConn)
                            Dim dataStdYear As Integer = Integer.Parse(dataTest)

                            ''print student name
                            Dim stdName As String = "select student_Name from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdName As String = getFieldValue(stdName, strConn)

                            ''print student id
                            Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                            ''print student mykad
                            Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdMykad As String = getFieldValue(stdMykad, strConn)

                            ''get student entry date / term ending from config function
                            Dim find_Senior_Entry_Date As String = "select Value from setting where idx = 'Examination' and Type = 'Senior Entry Date'"
                            Dim get_Senior_Entry_Date As String = oCommon.getFieldValue(find_Senior_Entry_Date)

                            Dim find_Senior_Term_Ending As String = "select Value from setting where idx = 'Examination' and Type = 'Senior Term Ending'"
                            Dim get_Senior_Term_Ending As String = oCommon.getFieldValue(find_Senior_Term_Ending)

                            ''get student end year (sudah tamat)
                            Dim highEndYear As Integer = dataStdYear + 1

                            ''looping year to get exam year
                            For year As Integer = dataStdYear To (dataStdYear + 1)

                                If year = dataStdYear Then
                                    ''print exam 1 to 4
                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 1
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAONE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMONE As New DataTable
                                    SQAONE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAONE.Fill(EXAMONE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 1
                                    Dim gpaExam1 As String = ""
                                    Dim datagpaExam1 As String = ""
                                    gpaExam1 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and student_Png.year = '" & year & "'"
                                    datagpaExam1 = getFieldValue(gpaExam1, strConn)

                                    Dim cgpaExam1 As String = ""
                                    Dim datacgpaExam1 As String = ""
                                    cgpaExam1 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and student_Png.year = '" & year & "'"
                                    datacgpaExam1 = getFieldValue(cgpaExam1, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 2
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATWO As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTWO As New DataTable
                                    SQATWO.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATWO.Fill(EXAMTWO)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 2
                                    Dim gpaExam2 As String = ""
                                    Dim datagpaExam2 As String = ""
                                    gpaExam2 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and student_Png.year = '" & year & "'"
                                    datagpaExam2 = getFieldValue(gpaExam2, strConn)

                                    Dim cgpaExam2 As String = ""
                                    Dim datacgpaExam2 As String = ""
                                    cgpaExam2 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and student_Png.year = '" & year & "'"
                                    datacgpaExam2 = getFieldValue(cgpaExam2, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOONE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOONE As New DataTable
                                    SQAKOKOONE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOONE.Fill(EXAMKOKOONE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 3
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATHREE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTHREE As New DataTable
                                    SQATHREE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATHREE.Fill(EXAMTHREE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 3
                                    Dim gpaExam3 As String = ""
                                    Dim datagpaExam3 As String = ""
                                    gpaExam3 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and student_Png.year = '" & year & "'"
                                    datagpaExam3 = getFieldValue(gpaExam3, strConn)

                                    Dim cgpaExam3 As String = ""
                                    Dim datacgpaExam3 As String = ""
                                    cgpaExam3 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and student_Png.year = '" & year & "'"
                                    datacgpaExam3 = getFieldValue(cgpaExam3, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 4
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAFOUR As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMFOUR As New DataTable
                                    SQAFOUR.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAFOUR.Fill(EXAMFOUR)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 4
                                    Dim gpaExam4 As String = ""
                                    Dim datagpaExam4 As String = ""
                                    gpaExam4 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and student_Png.year = '" & year & "'"
                                    datagpaExam4 = getFieldValue(gpaExam4, strConn)

                                    Dim cgpaExam4 As String = ""
                                    Dim datacgpaExam4 As String = ""
                                    cgpaExam4 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and student_Png.year = '" & year & "'"
                                    datacgpaExam4 = getFieldValue(cgpaExam4, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOTWO As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOTWO As New DataTable
                                    SQAKOKOTWO.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOTWO.Fill(EXAMKOKOTWO)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                          <table style='width:100%'>
                                            <tr>
                                                <td style='width:26%;margin-top:70px'>
                                                    <table style='width:100%'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>TRANSKRIP RASMI</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Nama : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Pendaftaran : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Kad Pengenalan : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Senior_Entry_Date & "</font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Senior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td class ='printRow' style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 1 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 1
                                    For Each row As DataRow In EXAMONE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMONE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam1 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam1 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 2 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")

                                    'data for exam 2
                                    For Each row As DataRow In EXAMTWO.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTWO.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 2
                                    For Each row As DataRow In EXAMKOKOONE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOONE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam2 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam2 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width: 33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 3 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                    </tr>")

                                    'data for exam 3
                                    For Each row As DataRow In EXAMTHREE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTHREE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam3 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam3 & "</b></font></td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table style ='margin-top:0px;width:100%'>
                                                <tr>
                                                    <td style='background-color:#9b9fa5'>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 4 </b></font></p>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style ='width: 100%'>
                                                        <div class='table-responsive'>
                                                            <p></p>
                                                            <table>
                                                                <tr>
                                                                    <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                    <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                </tr>")

                                    'data for exam 4
                                    For Each row As DataRow In EXAMFOUR.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMFOUR.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 4
                                    For Each row As DataRow In EXAMKOKOTWO.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOTWO.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                                <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam4 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam4 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                ElseIf year = (dataStdYear + 1) Then
                                    ''print exam 5 to 7
                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 5
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAFIVE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMFIVE As New DataTable
                                    SQAFIVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAFIVE.Fill(EXAMFIVE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 5
                                    Dim gpaExam5 As String = ""
                                    Dim datagpaExam5 As String = ""
                                    gpaExam5 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and student_Png.year = '" & year & "'"
                                    datagpaExam5 = getFieldValue(gpaExam5, strConn)

                                    Dim cgpaExam5 As String = ""
                                    Dim datacgpaExam5 As String = ""
                                    cgpaExam5 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and student_Png.year = '" & year & "'"
                                    datacgpaExam5 = getFieldValue(cgpaExam5, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 6
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQASIX As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMSIX As New DataTable
                                    SQASIX.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQASIX.Fill(EXAMSIX)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 6
                                    Dim gpaExam6 As String = ""
                                    Dim datagpaExam6 As String = ""
                                    gpaExam6 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and student_Png.year = '" & year & "'"
                                    datagpaExam6 = getFieldValue(gpaExam6, strConn)

                                    Dim cgpaExam6 As String = ""
                                    Dim datacgpaExam6 As String = ""
                                    cgpaExam6 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and student_Png.year = '" & year & "'"
                                    datacgpaExam6 = getFieldValue(cgpaExam6, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOTHREE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOTHREE As New DataTable
                                    SQAKOKOTHREE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOTHREE.Fill(EXAMKOKOTHREE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 7
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_NameBM,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQASEVEN As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMSEVEN As New DataTable
                                    SQAFIVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQASEVEN.Fill(EXAMSEVEN)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 7
                                    Dim gpaExam7 As String = ""
                                    Dim datagpaExam7 As String = ""
                                    gpaExam7 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and student_Png.year = '" & year & "'"
                                    datagpaExam7 = getFieldValue(gpaExam7, strConn)

                                    Dim cgpaExam7 As String = ""
                                    Dim datacgpaExam7 As String = ""
                                    cgpaExam7 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and student_Png.year = '" & year & "'"
                                    datacgpaExam7 = getFieldValue(cgpaExam7, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMA AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMA As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOFOUR As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOFOUR As New DataTable
                                    SQAKOKOFOUR.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOFOUR.Fill(EXAMKOKOFOUR)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0;margin-top:10px;height:95%;page-break-after: always;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                        <table style='width:100%'>
                                            <tr>
                                                <td style='width :26%'>
                                                    <table style='width:100%;margin-top:70px'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>TRANSKRIP RASMI</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Nama : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Pendaftaran : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> No. Kad Pengenalan : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Senior_Entry_Date & "</font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Senior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td class ='printRow' style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 5 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 5
                                    For Each row As DataRow In EXAMFIVE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMFIVE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam5 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam5 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 6 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                        </tr>")

                                    'data for exam 6
                                    For Each row As DataRow In EXAMSIX.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMSIX.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 6
                                    For Each row As DataRow In EXAMKOKOTHREE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOTHREE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam6 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam6 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width: 33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>PEPERIKSAAN 7 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>TAHUN AKADEMIK " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Kod</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Kursus</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Gred</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Kredit</b></font></td>
                                                                    </tr>")

                                    'data for exam 7
                                    For Each row As DataRow In EXAMSEVEN.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMSEVEN.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 7
                                    For Each row As DataRow In EXAMKOKOFOUR.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOFOUR.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                       <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGS </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam7 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>PNGK </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam7 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style ='margin-top:70px;margin-left:50px;width:100%'>
                                                        <tr>
                                                            <td>
                                                                <p style='margin-bottom:0px;margin-top: 0px'>____________________</p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>" & get_director & "</b></font</p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'>" & get_job & " ,</p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'>Pusat PERMATApintar® Negara</p>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                End If

                            Next

                        End If
                    End If
                Next

                Test.AppendLine("</div> </div>")
                Test.AppendLine("<script language=javascript> var divToPrint=document.getElementById('dataOfficialBM'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close();</script>")

                ''print
                Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

            End If


        ElseIf rbtn_English.Checked = True Then

            rbtn_Malay.Checked = False

            Test.AppendLine("<div id='data' style='display:none'>")
            Test.AppendLine("<div id='dataOfficialBI'>")

            If ddlclassID.SelectedValue = "Junior School" Then
                ''print junior school format transcript
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''get student register date
                            Dim stdYear As String = "select Min(year) from student_level where std_ID = '" & strKey & "'"
                            Dim dataTest As String = getFieldValue(stdYear, strConn)
                            Dim dataStdYear As Integer = Integer.Parse(dataTest)

                            ''print student name
                            Dim stdName As String = "select student_Name from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdName As String = getFieldValue(stdName, strConn)

                            ''print student id
                            Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                            ''print student mykad
                            Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdMykad As String = getFieldValue(stdMykad, strConn)

                            ''get student entry date / term ending from config function
                            Dim find_Junior_Entry_Date As String = "select Value from setting where idx = 'Examination' and Type = 'Junior Entry Date'"
                            Dim get_Junior_Entry_Date As String = oCommon.getFieldValue(find_Junior_Entry_Date)

                            Dim find_Junior_Term_Ending As String = "select Value from setting where idx = 'Examination' and Type = 'Junior Term Ending'"
                            Dim get_Junior_Term_Ending As String = oCommon.getFieldValue(find_Junior_Term_Ending)

                            ''get student end year (sudah tamat)
                            Dim juniorEndYear As Integer = dataStdYear + 2

                            ''looping year to get exam year
                            For year As Integer = dataStdYear To (dataStdYear + 2)

                                ''year 1
                                If year = dataStdYear Then

                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 1
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAONE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMONE As New DataTable
                                    SQAONE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAONE.Fill(EXAMONE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 1
                                    Dim gpaExam1 As String = ""
                                    Dim datagpaExam1 As String = ""
                                    gpaExam1 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and student_Png.year = '" & year & "'"
                                    datagpaExam1 = getFieldValue(gpaExam1, strConn)

                                    Dim cgpaExam1 As String = ""
                                    Dim datacgpaExam1 As String = ""
                                    cgpaExam1 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and student_Png.year = '" & year & "'"
                                    datacgpaExam1 = getFieldValue(cgpaExam1, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 2
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATWO As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTWO As New DataTable
                                    SQATWO.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATWO.Fill(EXAMTWO)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 2
                                    Dim gpaExam2 As String = ""
                                    Dim datagpaExam2 As String = ""
                                    gpaExam2 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and student_Png.year = '" & year & "'"
                                    datagpaExam2 = getFieldValue(gpaExam2, strConn)

                                    Dim cgpaExam2 As String = ""
                                    Dim datacgpaExam2 As String = ""
                                    cgpaExam2 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and student_Png.year = '" & year & "'"
                                    datacgpaExam2 = getFieldValue(cgpaExam2, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOONE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOONE As New DataTable
                                    SQAKOKOONE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOONE.Fill(EXAMKOKOONE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 3
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATHREE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTHREE As New DataTable
                                    SQATHREE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATHREE.Fill(EXAMTHREE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 3
                                    Dim gpaExam3 As String = ""
                                    Dim datagpaExam3 As String = ""
                                    gpaExam3 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and student_Png.year = '" & year & "'"
                                    datagpaExam3 = getFieldValue(gpaExam3, strConn)

                                    Dim cgpaExam3 As String = ""
                                    Dim datacgpaExam3 As String = ""
                                    cgpaExam3 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and student_Png.year = '" & year & "'"
                                    datacgpaExam3 = getFieldValue(cgpaExam3, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 4
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAFOUR As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMFOUR As New DataTable
                                    SQAFOUR.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAFOUR.Fill(EXAMFOUR)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 4
                                    Dim gpaExam4 As String = ""
                                    Dim datagpaExam4 As String = ""
                                    gpaExam4 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and student_Png.year = '" & year & "'"
                                    datagpaExam4 = getFieldValue(gpaExam4, strConn)

                                    Dim cgpaExam4 As String = ""
                                    Dim datacgpaExam4 As String = ""
                                    cgpaExam4 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and student_Png.year = '" & year & "'"
                                    datacgpaExam4 = getFieldValue(cgpaExam4, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOTWO As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOTWO As New DataTable
                                    SQAKOKOTWO.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOTWO.Fill(EXAMKOKOTWO)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                          <table style='width:100%'>
                                            <tr>
                                                <td style='width:26%;margin-top:70px'>
                                                    <table style='width:100%'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>OFFICIAL TANSCRIPT</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Name : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> Registration No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> I/C No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Junior_Entry_Date & "</font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Junior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td class ='printRow' style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 1 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 1
                                    For Each row As DataRow In EXAMONE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMONE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam1 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam1 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 2 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")

                                    'data for exam 2
                                    For Each row As DataRow In EXAMTWO.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTWO.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 2
                                    For Each row As DataRow In EXAMKOKOONE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOONE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam2 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam2 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width: 33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 3 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                    </tr>")

                                    'data for exam 3
                                    For Each row As DataRow In EXAMTHREE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTHREE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam3 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam3 & "</b></font></td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table style ='margin-top:0px;width:100%'>
                                                <tr>
                                                    <td style='background-color:#FC747C'>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 4 </b></font></p>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style ='width: 100%'>
                                                        <div class='table-responsive'>
                                                            <p></p>
                                                            <table>
                                                                <tr>
                                                                    <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                    <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                </tr>")

                                    'data for exam 4
                                    For Each row As DataRow In EXAMFOUR.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMFOUR.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 4
                                    For Each row As DataRow In EXAMKOKOTWO.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOTWO.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                                <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam4 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam4 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                    ''year 2
                                ElseIf year = (dataStdYear + 1) Then

                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 5
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAFIVE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMFIVE As New DataTable
                                    SQAFIVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAFIVE.Fill(EXAMFIVE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 5
                                    Dim gpaExam5 As String = ""
                                    Dim datagpaExam5 As String = ""
                                    gpaExam5 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and student_Png.year = '" & year & "'"
                                    datagpaExam5 = getFieldValue(gpaExam5, strConn)

                                    Dim cgpaExam5 As String = ""
                                    Dim datacgpaExam5 As String = ""
                                    cgpaExam5 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and student_Png.year = '" & year & "'"
                                    datacgpaExam5 = getFieldValue(cgpaExam5, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 6
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQASIX As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMSIX As New DataTable
                                    SQASIX.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQASIX.Fill(EXAMSIX)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 6
                                    Dim gpaExam6 As String = ""
                                    Dim datagpaExam6 As String = ""
                                    gpaExam6 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and student_Png.year = '" & year & "'"
                                    datagpaExam6 = getFieldValue(gpaExam6, strConn)

                                    Dim cgpaExam6 As String = ""
                                    Dim datacgpaExam6 As String = ""
                                    cgpaExam6 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and student_Png.year = '" & year & "'"
                                    datacgpaExam6 = getFieldValue(cgpaExam6, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOTHREE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOTHREE As New DataTable
                                    SQAKOKOTHREE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOTHREE.Fill(EXAMKOKOTHREE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 7
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQASEVEN As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMSEVEN As New DataTable
                                    SQASEVEN.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQASEVEN.Fill(EXAMSEVEN)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 7
                                    Dim gpaExam7 As String = ""
                                    Dim datagpaExam7 As String = ""
                                    gpaExam7 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and student_Png.year = '" & year & "'"
                                    datagpaExam7 = getFieldValue(gpaExam7, strConn)

                                    Dim cgpaExam7 As String = ""
                                    Dim datacgpaExam7 As String = ""
                                    cgpaExam7 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and student_Png.year = '" & year & "'"
                                    datacgpaExam7 = getFieldValue(cgpaExam7, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 8
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 8' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAEIGHT As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMEIGHT As New DataTable
                                    SQAEIGHT.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAEIGHT.Fill(EXAMEIGHT)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 8
                                    Dim gpaExam8 As String = ""
                                    Dim datagpaExam8 As String = ""
                                    gpaExam8 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 8' and student_Png.year = '" & year & "'"
                                    datagpaExam8 = getFieldValue(gpaExam8, strConn)

                                    Dim cgpaExam8 As String = ""
                                    Dim datacgpaExam8 As String = ""
                                    cgpaExam8 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 8' and student_Png.year = '" & year & "'"
                                    datacgpaExam8 = getFieldValue(cgpaExam8, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOFOUR As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOFOUR As New DataTable
                                    SQAKOKOFOUR.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOFOUR.Fill(EXAMKOKOFOUR)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                          <table style='width:100%'>
                                            <tr>
                                                <td style='width:26%;margin-top:70px'>
                                                    <table style='width:100%'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>OFFICIAL TANSCRIPT</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Name : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> Registration No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> I/C No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                       <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Junior_Entry_Date & "</font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Junior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td class ='printRow' style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 5 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 5
                                    For Each row As DataRow In EXAMFIVE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMFIVE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam5 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam5 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 6 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")

                                    'data for exam 6
                                    For Each row As DataRow In EXAMSIX.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMSIX.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 6
                                    For Each row As DataRow In EXAMKOKOTHREE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOTHREE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam6 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam6 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width: 33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 7 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                    </tr>")

                                    'data for exam 7
                                    For Each row As DataRow In EXAMSEVEN.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMSEVEN.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam7 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam7 & "</b></font></td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table style ='margin-top:0px;width:100%'>
                                                <tr>
                                                    <td style='background-color:#FC747C'>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 8 </b></font></p>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style ='width: 100%'>
                                                        <div class='table-responsive'>
                                                            <p></p>
                                                            <table>
                                                                <tr>
                                                                    <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                    <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                </tr>")

                                    'data for exam 8
                                    For Each row As DataRow In EXAMEIGHT.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMEIGHT.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 8
                                    For Each row As DataRow In EXAMKOKOFOUR.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOFOUR.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                                <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam8 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam8 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                    ''year 3
                                ElseIf year = (dataStdYear + 2) Then

                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 9
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 9' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQANINE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMNINE As New DataTable
                                    SQANINE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQANINE.Fill(EXAMNINE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 9
                                    Dim gpaExam9 As String = ""
                                    Dim datagpaExam9 As String = ""
                                    gpaExam9 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 9' and student_Png.year = '" & year & "'"
                                    datagpaExam9 = getFieldValue(gpaExam9, strConn)

                                    Dim cgpaExam9 As String = ""
                                    Dim datacgpaExam9 As String = ""
                                    cgpaExam9 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 9' and student_Png.year = '" & year & "'"
                                    datacgpaExam9 = getFieldValue(cgpaExam9, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 10
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 10' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATEN As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTEN As New DataTable
                                    SQATEN.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATEN.Fill(EXAMTEN)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 10
                                    Dim gpaExam10 As String = ""
                                    Dim datagpaExam10 As String = ""
                                    gpaExam10 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 10' and student_Png.year = '" & year & "'"
                                    datagpaExam10 = getFieldValue(gpaExam10, strConn)

                                    Dim cgpaExam10 As String = ""
                                    Dim datacgpaExam10 As String = ""
                                    cgpaExam10 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 10' and student_Png.year = '" & year & "'"
                                    datacgpaExam10 = getFieldValue(cgpaExam10, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOFIVE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOFIVE As New DataTable
                                    SQAKOKOFIVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOFIVE.Fill(EXAMKOKOFIVE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 11
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 11' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAELEVEN As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMELEVEN As New DataTable
                                    SQAELEVEN.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAELEVEN.Fill(EXAMELEVEN)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 11
                                    Dim gpaExam11 As String = ""
                                    Dim datagpaExam11 As String = ""
                                    gpaExam11 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 11' and student_Png.year = '" & year & "'"
                                    datagpaExam11 = getFieldValue(gpaExam11, strConn)

                                    Dim cgpaExam11 As String = ""
                                    Dim datacgpaExam11 As String = ""
                                    cgpaExam11 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 11' and student_Png.year = '" & year & "'"
                                    datacgpaExam11 = getFieldValue(cgpaExam11, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 12
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 12' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATWELVE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTWELVE As New DataTable
                                    SQATWELVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATWELVE.Fill(EXAMTWELVE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 12
                                    Dim gpaExam12 As String = ""
                                    Dim datagpaExam12 As String = ""
                                    gpaExam12 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 12' and student_Png.year = '" & year & "'"
                                    datagpaExam12 = getFieldValue(gpaExam12, strConn)

                                    Dim cgpaExam12 As String = ""
                                    Dim datacgpaExam12 As String = ""
                                    cgpaExam12 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 12' and student_Png.year = '" & year & "'"
                                    datacgpaExam12 = getFieldValue(cgpaExam12, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOSIX As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOSIX As New DataTable
                                    SQAKOKOSIX.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOSIX.Fill(EXAMKOKOSIX)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                          <table style='width:100%'>
                                            <tr>
                                                <td style='width:26%;margin-top:70px'>
                                                    <table style='width:100%'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>OFFICIAL TANSCRIPT</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Name : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> Registration No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> I/C No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Junior_Entry_Date & "</font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Junior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td class ='printRow' style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 9 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 9
                                    For Each row As DataRow In EXAMNINE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMNINE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam9 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam9 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 10 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")

                                    'data for exam 10
                                    For Each row As DataRow In EXAMTEN.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTEN.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 10
                                    For Each row As DataRow In EXAMKOKOFIVE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOFIVE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam10 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam10 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width: 33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#FC747C'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 11 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                    </tr>")

                                    'data for exam 11
                                    For Each row As DataRow In EXAMELEVEN.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMELEVEN.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam11 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam11 & "</b></font></td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table style ='margin-top:0px;width:100%'>
                                                <tr>
                                                    <td style='background-color:#FC747C'>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 12 </b></font></p>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style ='width: 100%'>
                                                        <div class='table-responsive'>
                                                            <p></p>
                                                            <table>
                                                                <tr>
                                                                    <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                    <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                    <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                </tr>")

                                    'data for exam 12
                                    For Each row As DataRow In EXAMTWELVE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTWELVE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 12
                                    For Each row As DataRow In EXAMKOKOSIX.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOSIX.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                                <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam12 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam12 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                End If

                            Next

                            If i <= count_i - 1 Then
                                ''printing format
                                Test.Append("<div style='margin: 0; page-break-after: always; height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>")
                            Else
                                ''printing format
                                Test.Append("<div style='margin: 0; height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>")
                            End If

                            'Test.Append("<div style='margin: 0; height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>")
                            Test.Append("  <table style='width:100%'>
                                            <tr>
                                                <td style='width :26%'>
                                                    <table style='width:100%;margin-top:70px'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>OFFICIAL TRANSCRIPT</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Name : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> Registration No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> I/C No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                       <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Junior_Entry_Date & "</font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Junior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :74%;margin-left:50px;margin-top:50px'>
                                                    <p style='margin-bottom:0px;margin-top: 0px'>____________________</p>
                                                    <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>" & get_director & "</b></font</p>
                                                    <p style='margin-bottom:0px;margin-top: 0px'>" & get_job & " ,</p>
                                                    <p style='margin-bottom:0px;margin-top: 0px'>Pusat PERMATApintar® Negara</p>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")


                        End If
                    End If
                Next

                Test.AppendLine(" </div> </div>")
                Test.AppendLine("<script language=javascript>javascript: var divToPrint=document.getElementById('dataOfficialBI'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close();</script>")

                ''print
                Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

            ElseIf ddlclassID.SelectedValue = "High School" Then
                ''print high school format transcript
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''get student register date year 
                            Dim stdYear As String = "select Min(year) from student_level where std_ID = '" & strKey & "' "
                            Dim dataTestYear As String = getFieldValue(stdYear, strConn)
                            Dim dataStdYear As Integer = Integer.Parse(dataTestYear)

                            ''get student entry date / term ending from config function
                            Dim find_Senior_Entry_Date As String = "select Value from setting where idx = 'Examination' and Type = 'Senior Entry Date'"
                            Dim get_Senior_Entry_Date As String = oCommon.getFieldValue(find_Senior_Entry_Date)

                            Dim find_Senior_Term_Ending As String = "select Value from setting where idx = 'Examination' and Type = 'Senior Term Ending'"
                            Dim get_Senior_Term_Ending As String = oCommon.getFieldValue(find_Senior_Term_Ending)

                            ''print student name
                            Dim stdName As String = "select student_Name from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdName As String = getFieldValue(stdName, strConn)

                            ''print student id
                            Dim stdID As String = "select student_ID from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                            ''print student mykad
                            Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim dataStdMykad As String = getFieldValue(stdMykad, strConn)

                            ''get student end year (sudah tamat)
                            Dim highEndYear As Integer = dataStdYear + 1


                            ''looping year to get exam year
                            For year As Integer = dataStdYear To (dataStdYear + 1)

                                If year = dataStdYear Then
                                    ''print exam 1 to 4
                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 1
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAONE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMONE As New DataTable
                                    SQAONE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAONE.Fill(EXAMONE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 1
                                    Dim gpaExam1 As String = ""
                                    Dim datagpaExam1 As String = ""
                                    gpaExam1 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and student_Png.year = '" & year & "'"
                                    datagpaExam1 = getFieldValue(gpaExam1, strConn)

                                    Dim cgpaExam1 As String = ""
                                    Dim datacgpaExam1 As String = ""
                                    cgpaExam1 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 1' and student_Png.year = '" & year & "'"
                                    datacgpaExam1 = getFieldValue(cgpaExam1, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 2
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATWO As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTWO As New DataTable
                                    SQATWO.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATWO.Fill(EXAMTWO)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 2
                                    Dim gpaExam2 As String = ""
                                    Dim datagpaExam2 As String = ""
                                    gpaExam2 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and student_Png.year = '" & year & "'"
                                    datagpaExam2 = getFieldValue(gpaExam2, strConn)

                                    Dim cgpaExam2 As String = ""
                                    Dim datacgpaExam2 As String = ""
                                    cgpaExam2 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 2' and student_Png.year = '" & year & "'"
                                    datacgpaExam2 = getFieldValue(cgpaExam2, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOONE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOONE As New DataTable
                                    SQAKOKOONE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOONE.Fill(EXAMKOKOONE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 3
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQATHREE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMTHREE As New DataTable
                                    SQATHREE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQATHREE.Fill(EXAMTHREE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 3
                                    Dim gpaExam3 As String = ""
                                    Dim datagpaExam3 As String = ""
                                    gpaExam3 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and student_Png.year = '" & year & "'"
                                    datagpaExam3 = getFieldValue(gpaExam3, strConn)

                                    Dim cgpaExam3 As String = ""
                                    Dim datacgpaExam3 As String = ""
                                    cgpaExam3 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 3' and student_Png.year = '" & year & "'"
                                    datacgpaExam3 = getFieldValue(cgpaExam3, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 4
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAFOUR As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMFOUR As New DataTable
                                    SQAFOUR.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAFOUR.Fill(EXAMFOUR)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 4
                                    Dim gpaExam4 As String = ""
                                    Dim datagpaExam4 As String = ""
                                    gpaExam4 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and student_Png.year = '" & year & "'"
                                    datagpaExam4 = getFieldValue(gpaExam4, strConn)

                                    Dim cgpaExam4 As String = ""
                                    Dim datacgpaExam4 As String = ""
                                    cgpaExam4 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 4' and student_Png.year = '" & year & "'"
                                    datacgpaExam4 = getFieldValue(cgpaExam4, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOTWO As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOTWO As New DataTable
                                    SQAKOKOTWO.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOTWO.Fill(EXAMKOKOTWO)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0; page-break-after: always;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                          <table style='width:100%'>
                                            <tr>
                                                <td style='width:26%;margin-top:70px'>
                                                    <table style='width:100%'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>OFFICIAL TRANSCRIPT</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Name : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> Registration No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> I/C No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Senior_Entry_Date & "</font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Senior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 1 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 1
                                    For Each row As DataRow In EXAMONE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMONE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                               <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam1 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam1 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 2 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")

                                    'data for exam 2
                                    For Each row As DataRow In EXAMTWO.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTWO.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 2
                                    For Each row As DataRow In EXAMKOKOONE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOONE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam2 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam2 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width:33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 3 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                    </tr>")

                                    'data for exam 3
                                    For Each row As DataRow In EXAMTHREE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMTHREE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                       <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam3 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam3 & "</b></font></td>
                                                                        </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table style ='margin-top:0px;width:100%'>
                                                <tr>
                                                    <td style='background-color:#9b9fa5'>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 4 </b></font></p>
                                                        <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style ='width: 100%'>
                                                        <div class='table-responsive'>
                                                            <p></p>
                                                            <table>
                                                                <tr>
                                                                    <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                </tr>")

                                    'data for exam 4
                                    For Each row As DataRow In EXAMFOUR.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMFOUR.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 24
                                    For Each row As DataRow In EXAMKOKOTWO.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOTWO.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                                <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam4 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam4 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                ElseIf year = (dataStdYear + 1) Then
                                    ''print exam 5 to 7
                                    ''get student exam result based on year
                                    ''get subject code, subject name, grade, subject credit hour for exam 5
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQAFIVE As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMFIVE As New DataTable
                                    SQAFIVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAFIVE.Fill(EXAMFIVE)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 5
                                    Dim gpaExam5 As String = ""
                                    Dim datagpaExam5 As String = ""
                                    gpaExam5 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and student_Png.year = '" & year & "'"
                                    datagpaExam5 = getFieldValue(gpaExam5, strConn)

                                    Dim cgpaExam5 As String = ""
                                    Dim datacgpaExam5 As String = ""
                                    cgpaExam5 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 5' and student_Png.year = '" & year & "'"
                                    datacgpaExam5 = getFieldValue(cgpaExam5, strConn)

                                    ''get subject code, subject name, grade, subject credit hour for exam 6
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQASIX As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMSIX As New DataTable
                                    SQASIX.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQASIX.Fill(EXAMSIX)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 6
                                    Dim gpaExam6 As String = ""
                                    Dim datagpaExam6 As String = ""
                                    gpaExam6 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and student_Png.year = '" & year & "'"
                                    datagpaExam6 = getFieldValue(gpaExam6, strConn)

                                    Dim cgpaExam6 As String = ""
                                    Dim datacgpaExam6 As String = ""
                                    cgpaExam6 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 6' and student_Png.year = '" & year & "'"
                                    datacgpaExam6 = getFieldValue(cgpaExam6, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP1 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOTHREE As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOTHREE As New DataTable
                                    SQAKOKOTHREE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOTHREE.Fill(EXAMKOKOTHREE)
                                    Catch ex As Exception
                                    End Try

                                    ''get subject code, subject name, grade, subject credit hour for exam 7
                                    tmpSQL = "Select subject_info.subject_Code,subject_info.subject_Name,grade,subject_info.subject_CreditHour from exam_result left join course on exam_result.course_ID=course.course_ID left join student_info on course.std_ID=student_info.std_ID left join subject_info on course.subject_ID=subject_info.subject_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID WHERE student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and exam_Info.exam_Year= '" & year & "'"
                                    Dim SQASEVEN As New SqlDataAdapter(tmpSQL, strConn)
                                    Dim EXAMSEVEN As New DataTable
                                    SQAFIVE.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQASEVEN.Fill(EXAMSEVEN)
                                    Catch ex As Exception
                                    End Try

                                    ''get gpa and cgpa for exam 7
                                    Dim gpaExam7 As String = ""
                                    Dim datagpaExam7 As String = ""
                                    gpaExam7 = "select student_Png.png from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and student_Png.year = '" & year & "'"
                                    datagpaExam7 = getFieldValue(gpaExam7, strConn)

                                    Dim cgpaExam7 As String = ""
                                    Dim datacgpaExam7 As String = ""
                                    cgpaExam7 = "select student_Png.pngs from student_Png left join student_info on student_Png.std_ID=student_Info.std_ID left join exam_Info on student_Png.exam_Name=exam_Info.exam_Name where student_info.std_ID = '" & strKey & "' and exam_Info.exam_Name = 'Exam 7' and student_Png.year = '" & year & "'"
                                    datacgpaExam7 = getFieldValue(cgpaExam7, strConn)

                                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''(KOKO DATA FROM PERMATAPINTAR)
                                    tmpSQL = "select koko_kolejpermata.Kod AS KOD, koko_kolejpermata.NAMABI AS KURSUS, koko_pelajar.GredP2 AS GRED, '1' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'SUKAN'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'UNIFORM'
                                              UNION
                                              Select koko_kolejpermata.Kod As KOD, koko_kolejpermata.NAMABI As KURSUS, '' AS GRED, '' AS KREDIT from koko_pelajar
                                              Left Join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                              Left Join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                              where koko_pelajar.Tahun = '" & year & "' And StudentProfile.MYKAD = '" & dataStdMykad & "' 
                                              And koko_kolejpermata.Tahun = '" & year & "' And koko_kolejpermata.Jenis = 'PERSATUAN'
                                              order by koko_kolejpermata.Kod ASC"
                                    Dim SQAKOKOFOUR As New SqlDataAdapter(tmpSQL, strConnPermata)
                                    Dim EXAMKOKOFOUR As New DataTable
                                    SQAKOKOFOUR.SelectCommand.CommandTimeout = 120

                                    Try
                                        SQAKOKOFOUR.Fill(EXAMKOKOFOUR)
                                    Catch ex As Exception
                                    End Try

                                    ''printing format
                                    Test.Append("<div style='margin: 0;height:100%;background-position:center;background-repeat: no-repeat;background-size: cover;'>
                                        <table style='width:100%'>
                                            <tr>
                                                <td style='width :26%'>
                                                    <table style='width:100%;margin-top:70px'>
                                                        <tr>
                                                            <td>
                                                            <p> &nbsp;&nbsp; </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b>OFFICIAL TRANSCRIPT</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='3'><b> Kolej PERMATApintar®</b></font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b> Universiti Kebangsaan Malaysia</b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'><b>Pusat PERMATApintar® Negara,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Universiti Kebangsaan Malaysia,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>43600 UKM Bangi,</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Selangor Darul Ehsan</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Tel : </b>(+603)-8921 7529/7528/7508</font></p>
                                                                <p style='margin-top: 0px'><font size='2'><b>Fax : </b>(+603)-8921 7525</font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Name : </font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdName & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:95px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> Registration No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdID & " </b></font></p>
                                                                        </td>
                                                                        <td>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'> I/C No. : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top:0px'><font size='2'><b> " & dataStdMykad & " </b></font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan='2'>
                                                                <table style='width:100%'>
                                                                    <tr>
                                                                        <td style='width:100px'>
                                                                            <p style='margin-bottom:0px;margin-top:15px'><font size='2'>Entry Date : </font></p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>" & get_Senior_Entry_Date & "</font></p>
                                                                        </td>
                                                                        <td><p style='margin-bottom:0px;margin-top:15px'><font size='2'>Term Ending : </font</p>
                                                                            <p style='margin-bottom:0px;margin-top: 0px'><font size='2'> " & get_Senior_Term_Ending & "</font></p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'>Status : </font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>Graduated</b></font></p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width :33%;margin-left:0px'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 5 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                            <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                            <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")


                                    ''data for exam 5
                                    For Each row As DataRow In EXAMFIVE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMFIVE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                       <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam5 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam5 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style='margin-top:0px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>EXAMINATION 6 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>
                                                                    <div class='table-responsive'>                                                                 
                                                                    <p></p>
                                                                    <table>
                                                                        <tr>
                                                                            <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                        </tr>")

                                    'data for exam 6
                                    For Each row As DataRow In EXAMSIX.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMSIX.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 6
                                    For Each row As DataRow In EXAMKOKOTHREE.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOTHREE.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    Test.Append("                              <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam6 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam6 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style='width:33%;'>
                                                    <table style='margin-top:10px;width:100%'>
                                                        <tr>
                                                            <td style='background-color:#9b9fa5'>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>Examination 7 </b></font></p>
                                                                <p style='margin-bottom:0px;margin-top:0px'><font size='1'><b>ACADEMIC YEAR " & year & " </b></font></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='width: 100%'>
                                                                <div class='table-responsive'>                                                              
                                                                <p></p>
                                                                <table>
                                                                    <tr>
                                                                        <td style='text-align:left; width: 100px'><font size='1'><b>Code</b></font></td>
                                                                        <td style='text-align:left; width: 2000px'><font size='1'><b>Course</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Grade</b></font></td>
                                                                        <td style='text-align:left; width: 50px'><font size='1'><b>Credit</b></font></td>
                                                                    </tr>")

                                    'data for exam 7
                                    For Each row As DataRow In EXAMSEVEN.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMSEVEN.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next

                                    'data for exam 7
                                    For Each row As DataRow In EXAMKOKOFOUR.Rows
                                        Test.Append("<tr>")
                                        For Each column As DataColumn In EXAMKOKOFOUR.Columns
                                            Test.Append("<td style='text-align:left'><p style='margin-bottom:0px;margin-top:0px'><font size='1'>")
                                            Test.Append(row(column.ColumnName))
                                            Test.Append("</font></p></td>")
                                        Next
                                        Test.Append("</tr>")
                                    Next


                                    Test.Append("                       <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>GPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datagpaExam7 & "</b></font></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></p></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b>CGPA </b></font></td>
                                                                            <td><p style='text-align:left;margin-bottom:0px;margin-top:0px'></td>
                                                                            <td style='text-align:left;margin-bottom:0px;margin-top:0px'><font size='1'><b> " & datacgpaExam7 & "</b></font></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <table style ='margin-top:30px;margin-left:50px;width:100%'>
                                                        <tr>
                                                            <td>
                                                                <p style='margin-bottom:0px;margin-top: 0px'>____________________</p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'><font size='2'><b>" & get_director & "</b></font></p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'>" & get_job & " ,</p>
                                                                <p style='margin-bottom:0px;margin-top: 0px'>Pusat PERMATApintar® Negara</p>
                                                            </td>
                                                        </tr>
                                                     </table>
                                                </td>
                                                <td style='width: 11%;'>
                                                    <table style='width:100%'>
                                                        <tr>
	                                                        <td><p>&nbsp; &nbsp;</p>&nbsp; &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>")

                                End If

                            Next

                        End If
                    End If
                Next

                Test.AppendLine("</div> </div>")
                Test.AppendLine("<script language=javascript> var divToPrint=document.getElementById('dataOfficialBI'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close();</script>")

                ''print
                Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

            End If


        End If
    End Sub
End Class