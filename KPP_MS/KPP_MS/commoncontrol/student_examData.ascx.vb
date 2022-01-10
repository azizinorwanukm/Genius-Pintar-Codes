﻿Imports System.Data.SqlClient

Public Class student_examData
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    '' connection to kolejadmin databasse
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    '' connection to PERMATApintar® databasse
    Dim permataConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objPermataConn As SqlConnection = New SqlConnection(permataConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Year()
                load_page()
                Exam()
                strRet = BindData(datRespondent)
                ''Generate_Table()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "select distinct year from student_level where year = '" & Now.Year & "'"

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
    End Sub

    Private Sub Year()
        strSQL = "select distinct student_Level.year from student_Level
                  left join student_info on student_Level.std_ID=student_info.std_ID
                  where student_info.std_ID = '" + Request.QueryString("std_ID") + "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "year"
            ddlyear.DataValueField = "year"
            ddlyear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Exam()
        strSQL = "select distinct exam_info.exam_Name from exam_result left join exam_info on exam_result.exam_ID = exam_info.exam_ID
                  left join course on exam_result.course_ID = course.course_ID left join student_info on course.std_ID = student_info.std_ID
                  where student_info.student_Status = 'Access' and course.year = '" & ddlyear.SelectedValue & "' and exam_info.exam_Year = '" & ddlyear.SelectedValue & "' 
                  and student_info.std_ID = '" & Request.QueryString("std_ID") & "'
                  intersect
                  select Parameter from setting 
                  where idx = 'Examination' and Type = 'Exam Result' and Value = 'on'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlexam.DataSource = ds
            ddlexam.DataTextField = "exam_Name"
            ddlexam.DataValueField = "exam_Name"
            ddlexam.DataBind()
            ddlexam.Items.Insert(0, New ListItem("Select Exam", String.Empty))
            ddlexam.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlexam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlexam.SelectedIndexChanged
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
        Dim strOrderby As String = " ORDER BY subject_info.subject_ID ASC"

        tmpSQL = "select exam_result.ID,subject_Name,subject_code,marks,grade,gpa from course
                      left join subject_info on course.subject_ID=subject_info.subject_ID
                      left join student_info on course.std_ID=student_info.std_ID
                      left join exam_result on course.course_ID=exam_result.course_ID
                      left join exam_Info on exam_result.exam_Id=exam_Info.exam_ID
                      left join grade_info on exam_result.grade=grade_info.grade_Name"

        strWhere = " where student_info.std_ID = '" + Request.QueryString("std_ID") + "'"
        strWhere += " AND exam_Info.exam_Year = '" & ddlyear.SelectedValue & "'"
        strWhere += " AND exam_Info.exam_Name = '" & ddlexam.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby
        '--debug
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

        Dim tmpSQL As String = ""
        Dim tmpSQL_Nama As String = ""
        Dim tmpSQL_Kod As String = ""
        Dim tmpSQL_Gred As String = ""
        Dim tmpSQL_PNG As String = ""
        Dim tmpSQL_Hour As String = ""
        Dim tmpSQL_Total As String = ""

        Dim tmpSQL_SD_GRED As String = ""
        Dim tmpsql_SD_PNG As String = ""
        Dim tmpsql_SD_KOD As String = ""

        Dim tmpsql_Portfolio_GRED As String = ""
        Dim tmpsql_Portfolio_PNG As String = ""
        Dim tmpsql_Portfolio_KOD As String = ""

        Dim tmpsql_Penyelidikan_Gred As String = ""
        Dim tmpsql_Penyelidikan_PNG As String = ""
        Dim tmpsql_Penyelidikan_KOD As String = ""

        Dim tmpsql_EL_GRED As String = ""
        Dim tmpsql_EL_PNG As String = ""
        Dim tmpsql_EL_KOD As String = ""
        Dim tmpsql_EL_HOUR As String = ""
        Dim tmpsql_EL_TOTAL As String = ""

        Dim tmpsql_KOKO_KOD_SUKAN As String = ""
        Dim tmpsql_KOKO_KOD_UNIFORM As String = ""
        Dim tmpsql_KOKO_KOD_KELAB As String = ""
        Dim tmpsql_KOKO_NAMA_SUKAN As String = ""
        Dim tmpsql_KOKO_NAMA_KELAB As String = ""
        Dim tmpsql_KOKO_NAMA_UNIFORM As String = ""
        Dim tmpsql_KOKO_GRED As String = ""
        Dim tmpsql_KOKO_PNG As String = ""

        Dim errorCount As Integer = 0
        Dim Test As New StringBuilder()

        ''check print transcript language''
        If rbtn_Malay.Checked = True Then
            rbtn_English.Checked = False

            Test.AppendLine("<div id='data' style='display:none'>")
            Test.AppendLine("<div id='dataTESTBM'> ")

            ''get student level
            Dim FindStudentLevel As String = "select distinct student_Level from student_Level where std_ID = '" & Request.QueryString("std_ID") & "' and year = '" & ddlyear.SelectedValue & "'"
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

            'get englih literture on / off
            Dim check_Eng_Literature As String = "select Value from setting where Type = 'English Literature'"
            Dim Confirm_Eng_Literature As String = oCommon.getFieldValue(check_Eng_Literature)

            ''print subject name 
            tmpSQL_Nama = "SELECT subject_NameBM FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

            ''print subject code
            tmpSQL_Kod = "SELECT subject_code FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

            ''print subject grade
            tmpSQL_Gred = "SELECT grade FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

            ''print subject png
            tmpSQL_PNG = "SELECT gpa FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

            ''print subject credit hour
            tmpSQL_Hour = "SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

            ''print subject credit hour
            tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)



            tmpSQL = "select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
            Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select SUM(total) FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
            Dim total_Total As String = oCommon.getFieldValue(tmpSQL)



            Dim DS_Nama As New DataTable
            Dim DS_Kod As New DataTable
            Dim DS_Gred As New DataTable
            Dim DS_PNG As New DataTable
            Dim DS_Hour As New DataTable
            Dim DS_Total As New DataTable

            Dim DSSelfdevelopment_GRED As New DataTable
            Dim DSSelfdevelopment_PNG As New DataTable
            Dim DSSelfdevelopment_KOD As New DataTable

            Dim DSEnglish_literature_GRED As New DataTable
            Dim DSEnglish_literature_PNG As New DataTable
            Dim DSEnglish_literature_KOD As New DataTable
            Dim DSEnglish_literature_HOUR As New DataTable
            Dim DSEnglish_literature_TOTAL As New DataTable

            Dim DSResearch_GRED As New DataTable
            Dim DSResearch_PNG As New DataTable
            Dim DSResearch_KOD As New DataTable

            Dim DSPortfolio_GRED As New DataTable
            Dim DSPortfolio_PNG As New DataTable
            Dim DSPortfolio_KOD As New DataTable

            Dim DSCocuricullum_KOD_SUKAN As New DataTable
            Dim DSCocuricullum_KOD_UNIFORM As New DataTable
            Dim DSCocuricullum_KOD_KELAB As New DataTable
            Dim DSCocuricullum_NAMA_SUKAN As New DataTable
            Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
            Dim DSCocuricullum_NAMA_KELAB As New DataTable
            Dim DSCocuricullum_GRED As New DataTable
            Dim DSCocuricullum_PNG As New DataTable

            Dim total_Credit_EL As String = "0"
            Dim total_Total_EL As String = "0"

            ''print english literature
            If Confirm_Eng_Literature = "on" Then
                tmpsql_EL_GRED = "SELECT grade FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_EL_PNG = "SELECT gpa FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_EL_KOD = "SELECT subject_code FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_EL_HOUR = "SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_EL_TOTAL = "SELECT total FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "


                tmpSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                total_Credit_EL = oCommon.getFieldValue(tmpSQL)

                If total_Credit_EL.Length = 0 Then
                    total_Credit_EL = "0"
                End If


                tmpSQL = "select total FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                total_Total_EL = oCommon.getFieldValue(tmpSQL)

                If total_Total_EL.Length = 0 Then
                    total_Total_EL = "0"
                End If

                Dim SQEnglish_Literature_GRED As New SqlDataAdapter(tmpsql_EL_GRED, strConn)
                Dim SQEnglish_Literature_PNG As New SqlDataAdapter(tmpsql_EL_PNG, strConn)
                Dim SQEnglish_Literature_KOD As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
                Dim SQEnglish_Literature_HOUR As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
                Dim SQEnglish_Literature_TOTAL As New SqlDataAdapter(tmpsql_EL_KOD, strConn)

                Try
                    SQEnglish_Literature_GRED.Fill(DSEnglish_literature_GRED)
                    SQEnglish_Literature_KOD.Fill(DSEnglish_literature_KOD)
                    SQEnglish_Literature_PNG.Fill(DSEnglish_literature_PNG)
                    SQEnglish_Literature_HOUR.Fill(DSEnglish_literature_HOUR)
                    SQEnglish_Literature_TOTAL.Fill(DSEnglish_literature_TOTAL)
                Catch ex As Exception

                End Try
            End If

            ''print Portfolio
            If Confirm_Portfolio = "on" Then
                tmpsql_Portfolio_GRED = "SELECT grade FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                Dim SQPortfolio_GRED As New SqlDataAdapter(tmpsql_Portfolio_GRED, strConn)
                Dim SQPortfolio_PNG As New SqlDataAdapter(tmpsql_Portfolio_PNG, strConn)
                Dim SQPortfolio_KOD As New SqlDataAdapter(tmpsql_Portfolio_KOD, strConn)

                Try
                    SQPortfolio_GRED.Fill(DSPortfolio_GRED)
                    SQPortfolio_PNG.Fill(DSPortfolio_PNG)
                    SQPortfolio_KOD.Fill(DSPortfolio_KOD)
                Catch ex As Exception

                End Try
            End If

            ''print research 
            If Confirm_Research = "on" Then
                tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_Penyelidikan_PNG = "SELECT gpa FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_Penyelidikan_KOD = "SELECT subject_code FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                Dim SQResearch_GRED As New SqlDataAdapter(tmpsql_Penyelidikan_Gred, strConn)
                Dim SQResearch_PNG As New SqlDataAdapter(tmpsql_Penyelidikan_PNG, strConn)
                Dim SQResearch_KOD As New SqlDataAdapter(tmpsql_Penyelidikan_KOD, strConn)

                Try
                    SQResearch_GRED.Fill(DSResearch_GRED)
                    SQResearch_PNG.Fill(DSResearch_PNG)
                    SQResearch_KOD.Fill(DSResearch_KOD)
                Catch ex As Exception

                End Try
            End If

            ''print self development
            If Confirm_Self = "on" Then
                Dim level As String = "select student_Level from student_level where std_ID = '" & Request.QueryString("std_ID") & "' and year = '" & ddlyear.SelectedValue & "' "
                Dim getLevel As String = oCommon.getFieldValue(level)

                If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                    tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                    tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                    tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                    tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                    tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                    tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                End If

                Dim SQSelfdevelopment_GRED As New SqlDataAdapter(tmpSQL_SD_GRED, strConn)
                Dim SQSelfdevelopment_PNG As New SqlDataAdapter(tmpsql_SD_PNG, strConn)
                Dim SQSelfdevelopment_KOD As New SqlDataAdapter(tmpsql_SD_KOD, strConn)

                Try
                    SQSelfdevelopment_GRED.Fill(DSSelfdevelopment_GRED)
                    SQSelfdevelopment_PNG.Fill(DSSelfdevelopment_PNG)
                    SQSelfdevelopment_KOD.Fill(DSSelfdevelopment_KOD)
                Catch ex As Exception

                End Try
            End If

            '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
            If Confirm_Cocuricullum = "on" Then

                Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
                Dim getStudent As String = oCommon.getFieldValue(studentData)

                If ddlexam.SelectedValue = "Exam 2" Or ddlexam.SelectedValue = "Exam 6" Or ddlexam.SelectedValue = "Exam 10" Then

                    tmpsql_KOKO_PNG = "select koko_pelajar.PNGP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, permataConn)
                    Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, permataConn)
                    Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, permataConn)
                    Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, permataConn)
                    Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, permataConn)
                    Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, permataConn)
                    Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, permataConn)
                    Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, permataConn)

                    Try
                        SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                        SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                        SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                        SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                        SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                        SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                        SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                        SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                    Catch ex As Exception

                    End Try

                ElseIf ddlexam.SelectedValue = "Exam 4" Or ddlexam.SelectedValue = "Exam 7" Or ddlexam.SelectedValue = "Exam 8" Or ddlexam.SelectedValue = "Exam 12" Then

                    tmpsql_KOKO_PNG = "select koko_pelajar.PNGP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, permataConn)
                    Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, permataConn)
                    Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, permataConn)
                    Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, permataConn)
                    Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, permataConn)
                    Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, permataConn)
                    Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, permataConn)
                    Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, permataConn)

                    Try
                        SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                        SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                        SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                        SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                        SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                        SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                        SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                        SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                    Catch ex As Exception

                    End Try

                End If
            End If

            Try
                SQA.Fill(DS_Nama)
                SQACODE.Fill(DS_Kod)
                SQAPNG.Fill(DS_PNG)
                SQAGRADE.Fill(DS_Gred)
                SQAHOUR.Fill(DS_Hour)
                SQATOTAL.Fill(DS_Total)
            Catch ex As Exception
            End Try

            ''print student name
            Dim stdName As String = "select student_Name from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim dataStdName As String = oCommon.getFieldValue(stdName)

            ''print student id
            Dim stdID As String = "select student_ID from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim dataStdID As String = oCommon.getFieldValue(stdID)

            ''print student mykad
            Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

            ''print exam Name
            Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlexam.SelectedValue & "'"
            Dim dataExmName As String = oCommon.getFieldValue(exmName)

            If dataExmName = "Exam 1" Then
                dataExmName = "1 "
            ElseIf dataExmName = "Exam 2" Then
                dataExmName = "2 "
            ElseIf dataExmName = "Exam 3" Then
                dataExmName = "3 "
            ElseIf dataExmName = "Exam 4" Then
                dataExmName = "4 "
            ElseIf dataExmName = "Exam 5" Then
                dataExmName = "5  "
            ElseIf dataExmName = "Exam 6" Then
                dataExmName = "6  "
            ElseIf dataExmName = "Exam 7" Then
                dataExmName = "7 "
            ElseIf dataExmName = "Exam 8" Then
                dataExmName = "8 "
            ElseIf dataExmName = "Exam 9" Then
                dataExmName = "9  "
            ElseIf dataExmName = "Exam 10" Then
                dataExmName = "10 "
            ElseIf dataExmName = "Exam 11" Then
                dataExmName = "11 "
            ElseIf dataExmName = "Exam 12" Then
                dataExmName = "12  "
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
            Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
            Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

            Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
            Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

            Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
            Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

            ''round to 2 decimal places
            Dim gpa As Decimal = png_dec.ToString("F2")
            Dim cgpa As Decimal = pngs_dec.ToString("F2")

            ''get the academic percentage
            strSQL = "Select Parameter from setting where Type = 'Academic " & GetStudentLevel & " Percentage'"
            Dim academic_value As String = oCommon.getFieldValue(strSQL)

            ''get the cocurricular percentage
            strSQL = "Select Parameter from setting where Type = 'Cocurricular " & GetStudentLevel & " Percentage'"
            Dim cocuricullum_value As String = oCommon.getFieldValue(strSQL)

            ''get the portfolio percentage
            strSQL = "Select Parameter from setting where Type = 'Portfolio " & GetStudentLevel & " Percentage'"
            Dim portfolio_value As String = oCommon.getFieldValue(strSQL)

            ''get the research percentage
            strSQL = "Select Parameter from setting where Type = 'Research " & GetStudentLevel & " Percentage'"
            Dim research_value As String = oCommon.getFieldValue(strSQL)

            ''get the self development percentage
            strSQL = "Select Parameter from setting where Type = 'Self Development " & GetStudentLevel & " Percentage'"
            Dim sd_value As String = oCommon.getFieldValue(strSQL)

            ''first column
            Test.Append("<div style='margin:0;page-break-after: always;'>
                            <table style='width:100%'>
                                <tr style='width:100%'> 
                                    <table>
                                        <tr style='width:100%'>
                                            <td style='width:20%'>
                                                <img src='img/permata_logo.png' height='56' width='120'>
                                            </td>
                                            <td style='width:20%'>
                                                <img src='img/ukm.jpg'  height='56' width='120'>
                                            </td>
                                            <td style='width:60%'>
                                            </td>
                                        </tr>
                                    </table>
                                </tr>
                                <tr>    
                                    <td>
                                        <p></p>
                                    </td>
                                    <table>
                                        <tr style='width:100%'>
                                            <td style='width:30%'> Nama </td>
                                            <td style='width:40%'>: " & dataStdName & "</td>
                                            <td style='width:30%'></td>
                                        </tr>     
                                        <tr style='width:100%'>
                                            <td style='width:30%'> No Kad Pengenalan </td>
                                            <td style='width:40%'>: " & dataStdMykad & "</td>
                                            <td style='width:30%'></td>
                                        </tr>     
                                        <tr style='width:100%'>
                                            <td style='width:30%'> No Kad Matrik </td>
                                            <td style='width:40%'>: " & dataStdID & "</td>
                                            <td style='width:30%'></td>
                                        </tr>  
                                        <tr style='width:100%'>
                                            <td style='width:30%'> Peperiksaan </td>
                                            <td style='width:40%'>: " & dataExmName & "</td>
                                            <td style='width:30%'></td>
                                        </tr>
                                    </table>                                                                                    
                                </tr>   
                                <tr>
                                    <td>
                                        <p></p>
                                    </td>
                                    <table style='border: 1px solid black;border-collapse: collapse;'>
                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                            <td style='width:20%;border: 1px solid black;'><b> Komponen </b></td>
                                            <td style='width:7%;border: 1px solid black;'><b> Peratusan </b></td>
                                            <td style='width:8%;border: 1px solid black;'><b> Kod Kursus </b></td>
                                            <td style='width:30%;border: 1px solid black;'><b> Kursus </b></td>
                                            <td style='width:5%;border: 1px solid black;'><b> Gred </b></td>
                                            <td style='width:5%;border: 1px solid black;'><b> PNG </b></td>
                                            <td style='width:10%;border: 1px solid black;'><b> Jam Kredit </b></td>
                                            <td style='width:15%;border: 1px solid black;'><b> PNG x Jam Kredit </b></td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black;text-align:center '>
                                            <td rowspan='3' style='width:20%;border: 1px solid black;'><b> Akademik </b></td>
                                            <td rowspan='3'style='width:7%;border: 1px solid black;'> " & academic_value & "</td>
                                            <td style='width:8%;border: 1px solid black'>")

            ''(column course code / kod kursus)
            For Each row As DataRow In DS_Kod.Rows
                For Each column As DataColumn In DS_Kod.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_KOD As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_KOD As String = oCommon.getFieldValue(get_ENG_KOD)

            If data_ENGLITERATURE_KOD.Length > 0 Then

                ''english literature kod
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_KOD.Rows
                        For Each column As DataColumn In DSEnglish_literature_KOD.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" SM <br>")
                End If

            End If

            Test.Append("                   </td>
                                            <td style='width:30%;border: 1px solid black;text-align:left'>")

            ''(column course / kursus)
            For Each row As DataRow In DS_Nama.Rows
                For Each column As DataColumn In DS_Nama.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_NAMA As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_NAMA As String = oCommon.getFieldValue(get_ENG_NAMA)

            If data_ENGLITERATURE_NAMA.Length > 0 Then

                ''english literature NAMA
                If Confirm_Eng_Literature = "on" Then
                    Test.Append(" Kesusasteraan Inggeris <br>")
                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" Kesusasteraan Inggeris <br>")
                End If

            End If

            Test.Append("                   </td>
                                            <td style='width:5%;border: 1px solid black;'> ")

            ''(column grade / gred)
            For Each row As DataRow In DS_Gred.Rows
                For Each column As DataColumn In DS_Gred.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_Grade As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_Grade As String = oCommon.getFieldValue(get_ENG_Grade)

            If data_ENGLITERATURE_Grade.Length > 0 Then

                ''english literature Name
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_GRED.Rows
                        For Each column As DataColumn In DSEnglish_literature_GRED.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" SM <br>")
                End If

            End If

            Test.Append("                   </td>
                                            <td style='width:5%;border: 1px solid black;'> ")

            ''(column gpa / png)
            For Each row As DataRow In DS_PNG.Rows
                For Each column As DataColumn In DS_PNG.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_Png As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_Png As String = oCommon.getFieldValue(get_ENG_Png)

            If data_ENGLITERATURE_Png.Length > 0 Then

                ''english literature Name
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_PNG.Rows
                        For Each column As DataColumn In DSEnglish_literature_PNG.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" SM <br>")
                End If

            End If

            Test.Append("                   </td>
                                            <td style='width:10%;border: 1px solid black;'>")

            ''(column credit hour / jam kredit)
            For Each row As DataRow In DS_Hour.Rows
                For Each column As DataColumn In DS_Hour.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_HOUR As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_HOUR As String = oCommon.getFieldValue(get_ENG_HOUR)

            If data_ENGLITERATURE_HOUR.Length > 0 Then

                ''english literature credit hour
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_HOUR.Rows
                        For Each column As DataColumn In DSEnglish_literature_HOUR.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" SM <br>")
                End If

            End If

            Test.Append("                   </td>
                                            <td style='width:15%;border: 1px solid black;'> ")

            ''(column total / jumalh)
            For Each row As DataRow In DS_Total.Rows
                For Each column As DataColumn In DS_Total.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_TOTAL As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_TOTAL As String = oCommon.getFieldValue(get_ENG_TOTAL)

            If data_ENGLITERATURE_TOTAL.Length > 0 Then

                ''english literature total / jumlah
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_TOTAL.Rows
                        For Each column As DataColumn In DSEnglish_literature_TOTAL.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Debug.WriteLine("Error 1")
                    Test.Append(" SM <br>")
                End If

            End If

            Dim Number1 As Double = Double.Parse(total_Credit)
            Dim Number2 As Double = Double.Parse(total_Credit_EL)
            Dim Number3 As Double = Double.Parse(total_Total)
            Dim Number4 As Double = Double.Parse(total_Total_EL)

            Dim total_Hour As Double = Number1 + Number2
            Dim final_Total As Double = Number3 + Number4

            Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

            Test.Append("                   </td>
                                            </tr>
                                            <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                <td colspan='4'style='width:8%;border: 1px solid black;text-align:left'><b> Jumlah </b></td>
                                                <td style='width:10%;border: 1px solid black;'> " & total_Hour & " </td>
                                                <td style='width:15%;border: 1px solid black;'> " & final_Total & " </td>
                                            </tr>
                                            <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                <td colspan='4'style='width:8%;border: 1px solid black;text-align:left'><b> PNG Akademik </b></td>
                                                <td style='width:10%;border: 1px solid black;'> </td>
                                                <td style='width:15%;border: 1px solid black;'> " & PNG_Akademik & " </td>
                                            </tr>
                                            <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                <td style='width:20%;border: 1px solid black;'><b> Kokurikulum </b></td>
                                                <td style='width:7%;border: 1px solid black;'>" & cocuricullum_value & "</td>
                                                <td style='width:8%;border: 1px solid black;'>")

            ''kokorikulum kod sukan
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            ''kokorikulum kod kelab
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            ''kokorikulum kod uniform
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:30%;border: 1px solid black;text-align:left'>")

            ''kokorikulum nama skan
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            ''kokorikulum nama kelab
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            ''kokorikulum nama uniform
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:5%;border: 1px solid black;'>")

            ''kokorikulum gred 
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_GRED.Rows
                    For Each column As DataColumn In DSCocuricullum_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append(" SM <br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:5%;border: 1px solid black;'>")

            ''kokorikulum png 
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_PNG.Rows
                    For Each column As DataColumn In DSCocuricullum_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("SM <br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:10%;border: 1px solid black;'></td>
                                            <td style='width:15%;border: 1px solid black;'></td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                            <td style='width:20%;border: 1px solid black;'><b> Portfolio </b></td>
                                            <td style='width:7%;border: 1px solid black;'> " & portfolio_value & " </td>
                                            <td style='width:8%;border: 1px solid black;'>")

            ''Portfolio KOD
            If Confirm_Portfolio = "on" Then
                For Each row As DataRow In DSPortfolio_KOD.Rows
                    For Each column As DataColumn In DSPortfolio_KOD.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Portfolio = "off" Then
                Test.Append("<br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:30%;border: 1px solid black;'></td>
                                            <td style='width:5%;border: 1px solid black;'>")

            ''Portfolio Gred
            If Confirm_Portfolio = "on" Then
                For Each row As DataRow In DSPortfolio_GRED.Rows
                    For Each column As DataColumn In DSPortfolio_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Portfolio = "off" Then
                Test.Append("SM <br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:5%;border: 1px solid black;'>")

            ''Portfolio PNG
            If Confirm_Portfolio = "on" Then
                For Each row As DataRow In DSPortfolio_PNG.Rows
                    For Each column As DataColumn In DSPortfolio_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Portfolio = "off" Then
                Test.Append("SM <br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:10%;border: 1px solid black;'></td>
                                            <td style='width:15%;border: 1px solid black;'></td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                            <td style='width:20%;border: 1px solid black;'><b> Penyelidikan </b></td>
                                            <td style='width:7%;border: 1px solid black;'> " & research_value & " </td>
                                            <td style='width:8%;border: 1px solid black;'>")

            ''research KOD
            If Confirm_Research = "on" Then
                For Each row As DataRow In DSResearch_KOD.Rows
                    For Each column As DataColumn In DSResearch_KOD.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "off" Then
                Test.Append("<br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:30%;border: 1px solid black;'></td>
                                            <td style='width:5%;border: 1px solid black;'>")

            ''research GRED
            If Confirm_Research = "on" Then
                For Each row As DataRow In DSResearch_GRED.Rows
                    For Each column As DataColumn In DSResearch_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "off" Then
                Test.Append(" SM <br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:5%;border: 1px solid black;'>")

            ''research PNG
            If Confirm_Research = "on" Then
                For Each row As DataRow In DSResearch_PNG.Rows
                    For Each column As DataColumn In DSResearch_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "off" Then
                Test.Append(" SM <br>")
            End If

            Test.Append("                   </td>
                                            <td style='width:10%;border: 1px solid black;'></td>
                                            <td style='width:15%;border: 1px solid black;'></td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                            <td style='width:20%;border: 1px solid black;'><b> Pembangunan Kendiri </b></td>
                                            <td style='width:7%;border: 1px solid black;'> " & sd_value & " </td>
                                            <td style='width:8%;border: 1px solid black;'>")

            ''(column self development codde / pembangunan kendiri kod)
            For Each row As DataRow In DSSelfdevelopment_KOD.Rows
                For Each column As DataColumn In DSSelfdevelopment_KOD.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Test.Append("                   </td>
                                            <td style='width:30%;border: 1px solid black;'></td>
                                            <td style='width:5%;border: 1px solid black;'>")

            ''(column self development grade / pembangunan kendiri gred)
            For Each row As DataRow In DSSelfdevelopment_GRED.Rows
                For Each column As DataColumn In DSSelfdevelopment_GRED.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Test.Append("                   </td>
                                            <td style='width:5%;border: 1px solid black;'>")

            ''(column self development gpa / pembangunan kendiri png)
            For Each row As DataRow In DSSelfdevelopment_PNG.Rows
                For Each column As DataColumn In DSSelfdevelopment_PNG.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Test.Append("                   </td>
                                            <td style='width:10%;border: 1px solid black;'></td>
                                            <td style='width:15%;border: 1px solid black;'></td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                            <td style='width:20%;border: 1px solid black;'><b> PNG </b></td>
                                            <td style='width:7%;border: 1px solid black;'><b> " & gpa & " </b></td>
                                            <td style='width:8%;border: 1px solid black;'></td>
                                            <td style='width:30%;border: 1px solid black;'></td>
                                            <td style='width:5%;border: 1px solid black;'></td>
                                            <td style='width:5%;border: 1px solid black;'></td>
                                            <td style='width:10%;border: 1px solid black;'></td>
                                            <td style='width:15%;border: 1px solid black;'></td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black;text-align:center'>
                                            <td style='width:20%;border: 1px solid black;'><b> PNGK </b></td>
                                            <td style='width:7%;border: 1px solid black;'><b> " & cgpa & "</b></td>
                                            <td style='width:8%;border: 1px solid black;'></td>
                                            <td style='width:30%;border: 1px solid black;'></td>
                                            <td style='width:5%;border: 1px solid black;'></td>
                                            <td style='width:5%;border: 1px solid black;'></td>
                                            <td style='width:10%;border: 1px solid black;'></td>
                                            <td style='width:15%;border: 1px solid black;'></td>
                                        </tr>
                                    </table>
                                </tr>
                                </table>    
                            </div>")

            Test.AppendLine(" </div> </div>")
            Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataTESTBM'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

            ''print
            Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

        ElseIf rbtn_English.Checked = True Then
            rbtn_Malay.Checked = False

            Test.AppendLine("<div id='data' style='display:none'>")
            Test.AppendLine("<div id='dataTESTBI'> ")

            ''get student level
            Dim FindStudentLevel As String = "select distinct student_Level from student_Level where std_ID = '" & Request.QueryString("std_ID") & "' and year = '" & ddlyear.SelectedValue & "'"
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

            'get englih literture on / off
            Dim check_Eng_Literature As String = "select Value from setting where Type = 'English Literature'"
            Dim Confirm_Eng_Literature As String = oCommon.getFieldValue(check_Eng_Literature)

            ''print subject name 
            tmpSQL_Nama = "SELECT subject_Name FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQA As New SqlDataAdapter(tmpSQL_Nama, strConn)

            ''print subject code
            tmpSQL_Kod = "SELECT subject_code FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQACODE As New SqlDataAdapter(tmpSQL_Kod, strConn)

            ''print subject grade
            tmpSQL_Gred = "SELECT grade FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQAGRADE As New SqlDataAdapter(tmpSQL_Gred, strConn)

            ''print subject png
            tmpSQL_PNG = "SELECT gpa FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQAPNG As New SqlDataAdapter(tmpSQL_PNG, strConn)

            ''print subject credit hour
            tmpSQL_Hour = "SELECT subject_CreditHour FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQAHOUR As New SqlDataAdapter(tmpSQL_Hour, strConn)

            ''print subject credit hour
            tmpSQL_Total = "SELECT total FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
            Dim SQATOTAL As New SqlDataAdapter(tmpSQL_Total, strConn)



            tmpSQL = "select SUM(subject_CreditHour) FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
            Dim total_Credit As String = oCommon.getFieldValue(tmpSQL)

            tmpSQL = "select SUM(total) FROM [ExamSlip_SubjectName] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
            Dim total_Total As String = oCommon.getFieldValue(tmpSQL)



            Dim DS_Nama As New DataTable
            Dim DS_Kod As New DataTable
            Dim DS_Gred As New DataTable
            Dim DS_PNG As New DataTable
            Dim DS_Hour As New DataTable
            Dim DS_Total As New DataTable

            Dim DSSelfdevelopment_GRED As New DataTable
            Dim DSSelfdevelopment_PNG As New DataTable
            Dim DSSelfdevelopment_KOD As New DataTable

            Dim DSEnglish_literature_GRED As New DataTable
            Dim DSEnglish_literature_PNG As New DataTable
            Dim DSEnglish_literature_KOD As New DataTable
            Dim DSEnglish_literature_HOUR As New DataTable
            Dim DSEnglish_literature_TOTAL As New DataTable

            Dim DSResearch_GRED As New DataTable
            Dim DSResearch_PNG As New DataTable
            Dim DSResearch_KOD As New DataTable

            Dim DSPortfolio_GRED As New DataTable
            Dim DSPortfolio_PNG As New DataTable
            Dim DSPortfolio_KOD As New DataTable

            Dim DSCocuricullum_KOD_SUKAN As New DataTable
            Dim DSCocuricullum_KOD_UNIFORM As New DataTable
            Dim DSCocuricullum_KOD_KELAB As New DataTable
            Dim DSCocuricullum_NAMA_SUKAN As New DataTable
            Dim DSCocuricullum_NAMA_UNIFORM As New DataTable
            Dim DSCocuricullum_NAMA_KELAB As New DataTable
            Dim DSCocuricullum_GRED As New DataTable
            Dim DSCocuricullum_PNG As New DataTable

            Dim total_Credit_EL As String = "0"
            Dim total_Total_EL As String = "0"

            ''print english literature
            If Confirm_Eng_Literature = "on" Then
                tmpsql_EL_GRED = "SELECT grade FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_EL_PNG = "SELECT gpa FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_EL_KOD = "SELECT subject_code FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_EL_HOUR = "SELECT subject_CreditHour FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_EL_TOTAL = "SELECT total FROM [ExamSlip_English_Literature] 
                                              where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "


                tmpSQL = "select subject_CreditHour FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                total_Credit_EL = oCommon.getFieldValue(tmpSQL)

                If total_Credit_EL.Length = 0 Then
                    total_Credit_EL = "0"
                End If


                tmpSQL = "select total FROM [ExamSlip_English_Literature] 
                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "
                total_Total_EL = oCommon.getFieldValue(tmpSQL)

                If total_Total_EL.Length = 0 Then
                    total_Total_EL = "0"
                End If

                Dim SQEnglish_Literature_GRED As New SqlDataAdapter(tmpsql_EL_GRED, strConn)
                Dim SQEnglish_Literature_PNG As New SqlDataAdapter(tmpsql_EL_PNG, strConn)
                Dim SQEnglish_Literature_KOD As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
                Dim SQEnglish_Literature_HOUR As New SqlDataAdapter(tmpsql_EL_KOD, strConn)
                Dim SQEnglish_Literature_TOTAL As New SqlDataAdapter(tmpsql_EL_KOD, strConn)

                Try
                    SQEnglish_Literature_GRED.Fill(DSEnglish_literature_GRED)
                    SQEnglish_Literature_KOD.Fill(DSEnglish_literature_KOD)
                    SQEnglish_Literature_PNG.Fill(DSEnglish_literature_PNG)
                    SQEnglish_Literature_HOUR.Fill(DSEnglish_literature_HOUR)
                    SQEnglish_Literature_TOTAL.Fill(DSEnglish_literature_TOTAL)
                Catch ex As Exception

                End Try
            End If

            ''print Portfolio
            If Confirm_Portfolio = "on" Then
                tmpsql_Portfolio_GRED = "SELECT grade FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_Portfolio_PNG = "SELECT gpa FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_Portfolio_KOD = "SELECT subject_code FROM [ExamSlip_Portfolio] 
                                                     where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                Dim SQPortfolio_GRED As New SqlDataAdapter(tmpsql_Portfolio_GRED, strConn)
                Dim SQPortfolio_PNG As New SqlDataAdapter(tmpsql_Portfolio_PNG, strConn)
                Dim SQPortfolio_KOD As New SqlDataAdapter(tmpsql_Portfolio_KOD, strConn)

                Try
                    SQPortfolio_GRED.Fill(DSPortfolio_GRED)
                    SQPortfolio_PNG.Fill(DSPortfolio_PNG)
                    SQPortfolio_KOD.Fill(DSPortfolio_KOD)
                Catch ex As Exception

                End Try
            End If

            ''print research 
            If Confirm_Research = "on" Then
                tmpsql_Penyelidikan_Gred = "SELECT grade FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_Penyelidikan_PNG = "SELECT gpa FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                tmpsql_Penyelidikan_KOD = "SELECT subject_code FROM [ExamSlip_Research] 
                                                        where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                Dim SQResearch_GRED As New SqlDataAdapter(tmpsql_Penyelidikan_Gred, strConn)
                Dim SQResearch_PNG As New SqlDataAdapter(tmpsql_Penyelidikan_PNG, strConn)
                Dim SQResearch_KOD As New SqlDataAdapter(tmpsql_Penyelidikan_KOD, strConn)

                Try
                    SQResearch_GRED.Fill(DSResearch_GRED)
                    SQResearch_PNG.Fill(DSResearch_PNG)
                    SQResearch_KOD.Fill(DSResearch_KOD)
                Catch ex As Exception

                End Try
            End If

            ''print self development
            If Confirm_Self = "on" Then
                Dim level As String = "select student_Level from student_level where std_ID = '" & Request.QueryString("std_ID") & "' and year = '" & ddlyear.SelectedValue & "' "
                Dim getLevel As String = oCommon.getFieldValue(level)

                If getLevel <> "Level 1" And getLevel <> "Level 2" Then
                    tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                    tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                    tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_ASAS] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                ElseIf getLevel = "Level 1" Or getLevel = "Level 2" Then
                    tmpSQL_SD_GRED = "SELECT grade FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                    tmpsql_SD_PNG = "SELECT gpa FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                    tmpsql_SD_KOD = "SELECT subject_code FROM [ExamSlip_SelfDevelopment_TAHAP] 
                                                  where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and exam_Year = '" & ddlyear.SelectedValue & "' "

                End If

                Dim SQSelfdevelopment_GRED As New SqlDataAdapter(tmpSQL_SD_GRED, strConn)
                Dim SQSelfdevelopment_PNG As New SqlDataAdapter(tmpsql_SD_PNG, strConn)
                Dim SQSelfdevelopment_KOD As New SqlDataAdapter(tmpsql_SD_KOD, strConn)

                Try
                    SQSelfdevelopment_GRED.Fill(DSSelfdevelopment_GRED)
                    SQSelfdevelopment_PNG.Fill(DSSelfdevelopment_PNG)
                    SQSelfdevelopment_KOD.Fill(DSSelfdevelopment_KOD)
                Catch ex As Exception

                End Try
            End If

            '' print cocuricullum (for temporary purpose.. until kolejadmin db combine with permata db)
            If Confirm_Cocuricullum = "on" Then

                Dim studentData As String = "Select student_Mykad from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
                Dim getStudent As String = oCommon.getFieldValue(studentData)

                If ddlexam.SelectedValue = "Exam 2" Or ddlexam.SelectedValue = "Exam 6" Or ddlexam.SelectedValue = "Exam 10" Then

                    tmpsql_KOKO_PNG = "select koko_pelajar.PNGP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_GRED = "select koko_pelajar.GredP1 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, permataConn)
                    Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, permataConn)
                    Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, permataConn)
                    Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, permataConn)
                    Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, permataConn)
                    Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, permataConn)
                    Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, permataConn)
                    Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, permataConn)

                    Try
                        SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                        SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                        SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                        SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                        SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                        SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                        SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                        SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                    Catch ex As Exception

                    End Try

                ElseIf ddlexam.SelectedValue = "Exam 4" Or ddlexam.SelectedValue = "Exam 7" Or ddlexam.SelectedValue = "Exam 8" Or ddlexam.SelectedValue = "Exam 12" Then

                    tmpsql_KOKO_PNG = "select koko_pelajar.PNGP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_GRED = "select koko_pelajar.GredP2 from koko_pelajar
                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                          where Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "'"

                    tmpsql_KOKO_NAMA_SUKAN = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_NAMA_KELAB = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_NAMA_UNIFORM = "select koko_kolejpermata.NAMA from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    tmpsql_KOKO_KOD_SUKAN = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.SukanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'SUKAN'"

                    tmpsql_KOKO_KOD_KELAB = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.PersatuanID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'PERSATUAN'"

                    tmpsql_KOKO_KOD_UNIFORM = "select koko_kolejpermata.Kod from koko_pelajar
                                                          left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                                                          left join koko_kolejpermata on koko_pelajar.UniformID = koko_kolejpermata.KokoID
                                                          where koko_pelajar.Tahun = '" & ddlyear.SelectedValue & "' and StudentProfile.MYKAD = '" & getStudent & "' and koko_kolejpermata.Tahun = '" & ddlyear.SelectedValue & "' and koko_kolejpermata.Jenis = 'UNIFORM'"

                    Dim SQCocuricullum_KOD_SUKAN As New SqlDataAdapter(tmpsql_KOKO_KOD_SUKAN, permataConn)
                    Dim SQCocuricullum_KOD_KELAB As New SqlDataAdapter(tmpsql_KOKO_KOD_KELAB, permataConn)
                    Dim SQCocuricullum_KOD_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_KOD_UNIFORM, permataConn)
                    Dim SQCocuricullum_NAMA_SUKAN As New SqlDataAdapter(tmpsql_KOKO_NAMA_SUKAN, permataConn)
                    Dim SQCocuricullum_NAMA_KELAB As New SqlDataAdapter(tmpsql_KOKO_NAMA_KELAB, permataConn)
                    Dim SQCocuricullum_NAMA_UNIFORM As New SqlDataAdapter(tmpsql_KOKO_NAMA_UNIFORM, permataConn)
                    Dim SQCocuricullum_GRED As New SqlDataAdapter(tmpsql_KOKO_GRED, permataConn)
                    Dim SQCocuricullum_PNG As New SqlDataAdapter(tmpsql_KOKO_PNG, permataConn)

                    Try
                        SQCocuricullum_KOD_SUKAN.Fill(DSCocuricullum_KOD_SUKAN)
                        SQCocuricullum_KOD_KELAB.Fill(DSCocuricullum_KOD_KELAB)
                        SQCocuricullum_KOD_UNIFORM.Fill(DSCocuricullum_KOD_UNIFORM)
                        SQCocuricullum_NAMA_SUKAN.Fill(DSCocuricullum_NAMA_SUKAN)
                        SQCocuricullum_NAMA_KELAB.Fill(DSCocuricullum_NAMA_KELAB)
                        SQCocuricullum_NAMA_UNIFORM.Fill(DSCocuricullum_NAMA_UNIFORM)
                        SQCocuricullum_GRED.Fill(DSCocuricullum_GRED)
                        SQCocuricullum_PNG.Fill(DSCocuricullum_PNG)
                    Catch ex As Exception

                    End Try

                End If
            End If

            Try
                SQA.Fill(DS_Nama)
                SQACODE.Fill(DS_Kod)
                SQAPNG.Fill(DS_PNG)
                SQAGRADE.Fill(DS_Gred)
                SQAHOUR.Fill(DS_Hour)
                SQATOTAL.Fill(DS_Total)
            Catch ex As Exception
            End Try

            ''print student name
            Dim stdName As String = "select student_Name from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim dataStdName As String = oCommon.getFieldValue(stdName)

            ''print student id
            Dim stdID As String = "select student_ID from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim dataStdID As String = oCommon.getFieldValue(stdID)

            ''print student mykad
            Dim stdMykad As String = "select student_Mykad from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim dataStdMykad As String = oCommon.getFieldValue(stdMykad)

            ''print exam Name
            Dim exmName As String = "select exam_Name from exam_Info where exam_Name = '" & ddlexam.SelectedValue & "'"
            Dim dataExmName As String = oCommon.getFieldValue(exmName)

            If dataExmName = "Exam 1" Then
                dataExmName = "1 "
            ElseIf dataExmName = "Exam 2" Then
                dataExmName = "2 "
            ElseIf dataExmName = "Exam 3" Then
                dataExmName = "3 "
            ElseIf dataExmName = "Exam 4" Then
                dataExmName = "4 "
            ElseIf dataExmName = "Exam 5" Then
                dataExmName = "5  "
            ElseIf dataExmName = "Exam 6" Then
                dataExmName = "6  "
            ElseIf dataExmName = "Exam 7" Then
                dataExmName = "7 "
            ElseIf dataExmName = "Exam 8" Then
                dataExmName = "8 "
            ElseIf dataExmName = "Exam 9" Then
                dataExmName = "9  "
            ElseIf dataExmName = "Exam 10" Then
                dataExmName = "10 "
            ElseIf dataExmName = "Exam 11" Then
                dataExmName = "11 "
            ElseIf dataExmName = "Exam 12" Then
                dataExmName = "12  "
            End If

            ''get month
            Dim month As String = "select Value from setting where Value = '" & Now.Month & "' and Type = 'month'"
            Dim dataMonth As String = oCommon.getFieldValue(month)

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
                dataStdMonth = "Jun"
            ElseIf dataMonth = "7" Then
                dataStdMonth = "July"
            ElseIf dataMonth = "8" Then
                dataStdMonth = "August"
            ElseIf dataMonth = "9" Then
                dataStdMonth = "September"
            ElseIf dataMonth = "10" Then
                dataStdMonth = "Ocotber"
            ElseIf dataMonth = "11" Then
                dataStdMonth = "November"
            ElseIf dataMonth = "12" Then
                dataStdMonth = "December"
            End If

            ''get PNG & PNGK 
            Dim check_png_exist_data As String = "select png from student_Png where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
            Dim exist_png_data As String = oCommon.getFieldValue(check_png_exist_data)

            Dim check_pngs_exist_data As String = "select pngs from student_Png where std_ID = '" & Request.QueryString("std_ID") & "' and exam_Name = '" & ddlexam.SelectedValue & "' and year = '" & ddlyear.SelectedValue & "'"
            Dim exist_pngs_data As String = oCommon.getFieldValue(check_pngs_exist_data)

            Dim png_dec As Decimal = Decimal.Parse(exist_png_data)
            Dim pngs_dec As Decimal = Decimal.Parse(exist_pngs_data)

            ''round to 2 decimal places
            Dim gpa As Decimal = png_dec.ToString("F2")
            Dim cgpa As Decimal = pngs_dec.ToString("F2")

            ''get the academic percentage
            strSQL = "Select Parameter from setting where Type = 'Academic " & GetStudentLevel & " Percentage'"
            Dim academic_value As String = oCommon.getFieldValue(strSQL)

            ''get the cocurricular percentage
            strSQL = "Select Parameter from setting where Type = 'Cocurricular " & GetStudentLevel & " Percentage'"
            Dim cocuricullum_value As String = oCommon.getFieldValue(strSQL)

            ''get the portfolio percentage
            strSQL = "Select Parameter from setting where Type = 'Portfolio " & GetStudentLevel & " Percentage'"
            Dim portfolio_value As String = oCommon.getFieldValue(strSQL)

            ''get the research percentage
            strSQL = "Select Parameter from setting where Type = 'Research " & GetStudentLevel & " Percentage'"
            Dim research_value As String = oCommon.getFieldValue(strSQL)

            ''get the self development percentage
            strSQL = "Select Parameter from setting where Type = 'Self Development " & GetStudentLevel & " Percentage'"
            Dim sd_value As String = oCommon.getFieldValue(strSQL)

            ''first column
            Test.Append("<div style='margin:0;page-break-after: always;'>
                                        <table style='width:100%'>
                                            <tr style='width:100%'> 
                                                <table>
                                                    <tr style='width:100%'>
                                                        <td style='width:20%'>
                                                            <img src='img/permata_logo.png' height='56' width='120'>
                                                        </td>
                                                        <td style='width:20%'>
                                                            <img src='img/ukm.jpg'  height='56' width='120'>
                                                        </td>
                                                        <td style='width:60%'>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </tr>
                                            <tr>    
                                                <td>
                                                    <p></p>
                                                </td>
                                                <table>
                                                    <tr style='width:100%'>
                                                        <td style='width:30%'> Name</td>
                                                        <td style='width:40%'>: " & dataStdName & "</td>
                                                        <td style='width:30%'></td>
                                                    </tr>     
                                                    <tr style='width:100%'>
                                                        <td style='width:30%'> IC Number </td>
                                                        <td style='width:40%'>: " & dataStdMykad & "</td>
                                                        <td style='width:30%'></td>
                                                    </tr>     
                                                    <tr style='width:100%'>
                                                        <td style='width:30%'> ID Number </td>
                                                        <td style='width:40%'>: " & dataStdID & "</td>
                                                        <td style='width:30%'></td>
                                                    </tr>  
                                                    <tr style='width:100%'>
                                                        <td style='width:30%'> Examination </td>
                                                        <td style='width:40%'>: " & dataExmName & "</td>
                                                        <td style='width:30%'></td>
                                                    </tr>
                                                </table>                                                                                    
                                            </tr>   
                                            <tr>
                                                <td>
                                                    <p></p>
                                                </td>
                                                <table style='border: 1px solid black;border-collapse: collapse;'>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;'><b> Component </b></td>
                                                        <td style='width:7%;border: 1px solid black;'><b> Percentage </b></td>
                                                        <td style='width:8%;border: 1px solid black;'><b> Course Code </b></td>
                                                        <td style='width:30%;border: 1px solid black;'><b> Course </b></td>
                                                        <td style='width:5%;border: 1px solid black;'><b> Grade </b></td>
                                                        <td style='width:5%;border: 1px solid black;'><b> PNG </b></td>
                                                        <td style='width:10%;border: 1px solid black;'><b> Credit Hour </b></td>
                                                        <td style='width:15%;border: 1px solid black;'><b> PNG x Credit Hour </b></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center '>
                                                        <td rowspan='3' style='width:20%;border: 1px solid black;'><b> Academic </b></td>
                                                        <td rowspan='3'style='width:7%;border: 1px solid black;'> " & academic_value & "</td>
                                                        <td style='width:8%;border: 1px solid black'>")

            ''(column course code / kod kursus)
            For Each row As DataRow In DS_Kod.Rows
                For Each column As DataColumn In DS_Kod.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_KOD As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_KOD As String = oCommon.getFieldValue(get_ENG_KOD)

            If data_ENGLITERATURE_KOD.Length > 0 Then

                ''english literature kod
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_KOD.Rows
                        For Each column As DataColumn In DSEnglish_literature_KOD.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" SM <br>")
                End If

            End If

            Test.Append("                    </td>
                                                        <td style='width:30%;border: 1px solid black;text-align:left'>")

            ''(column course / kursus)
            For Each row As DataRow In DS_Nama.Rows
                For Each column As DataColumn In DS_Nama.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_NAMA As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_NAMA As String = oCommon.getFieldValue(get_ENG_NAMA)

            If data_ENGLITERATURE_NAMA.Length > 0 Then

                ''english literature NAMA
                If Confirm_Eng_Literature = "on" Then
                    Test.Append(" English Literature <br>")
                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" English Literature <br>")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;'> ")

            ''(column grade / gred)
            For Each row As DataRow In DS_Gred.Rows
                For Each column As DataColumn In DS_Gred.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_Grade As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_Grade As String = oCommon.getFieldValue(get_ENG_Grade)

            If data_ENGLITERATURE_Grade.Length > 0 Then

                ''english literature Name
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_GRED.Rows
                        For Each column As DataColumn In DSEnglish_literature_GRED.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" SM <br>")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;'> ")

            ''(column gpa / png)
            For Each row As DataRow In DS_PNG.Rows
                For Each column As DataColumn In DS_PNG.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_Png As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                      where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_Png As String = oCommon.getFieldValue(get_ENG_Png)

            If data_ENGLITERATURE_Png.Length > 0 Then

                ''english literature Name
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_PNG.Rows
                        For Each column As DataColumn In DSEnglish_literature_PNG.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" SM <br>")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;'>")

            ''(column credit hour / jam kredit)
            For Each row As DataRow In DS_Hour.Rows
                For Each column As DataColumn In DS_Hour.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_HOUR As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_HOUR As String = oCommon.getFieldValue(get_ENG_HOUR)

            If data_ENGLITERATURE_HOUR.Length > 0 Then

                ''english literature credit hour
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_HOUR.Rows
                        For Each column As DataColumn In DSEnglish_literature_HOUR.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Test.Append(" SM <br>")
                End If

            End If

            Test.Append("                   </td>
                                                        <td style='width:15%;border: 1px solid black;'> ")

            ''(column total / jumalh)
            For Each row As DataRow In DS_Total.Rows
                For Each column As DataColumn In DS_Total.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Dim get_ENG_TOTAL As String = "select course.subject_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                  where subject_info.subject_Name like '%AP English%' and subject_info.subject_year = '" & ddlyear.SelectedValue & "' and course.std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_ENGLITERATURE_TOTAL As String = oCommon.getFieldValue(get_ENG_TOTAL)

            If data_ENGLITERATURE_TOTAL.Length > 0 Then

                ''english literature total / jumlah
                If Confirm_Eng_Literature = "on" Then
                    For Each row As DataRow In DSEnglish_literature_TOTAL.Rows
                        For Each column As DataColumn In DSEnglish_literature_TOTAL.Columns
                            Test.Append(row(column.ColumnName))
                            Test.Append("<br>")
                        Next
                    Next

                ElseIf Confirm_Eng_Literature = "off" Then
                    Debug.WriteLine("Error 1")
                    Test.Append(" SM <br>")
                End If

            End If

            Dim Number1 As Double = Double.Parse(total_Credit)
            Dim Number2 As Double = Double.Parse(total_Credit_EL)
            Dim Number3 As Double = Double.Parse(total_Total)
            Dim Number4 As Double = Double.Parse(total_Total_EL)

            Dim total_Hour As Double = Number1 + Number2
            Dim final_Total As Double = Number3 + Number4

            Dim PNG_Akademik As Double = Math.Round(final_Total / total_Hour, 2)

            Test.Append("                   </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td colspan='4'style='width:8%;border: 1px solid black;text-align:left'><b> Total </b></td>
                                                        <td style='width:10%;border: 1px solid black;'> " & total_Hour & " </td>
                                                        <td style='width:15%;border: 1px solid black;'> " & final_Total & " </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td colspan='4'style='width:8%;border: 1px solid black;text-align:left'><b> Academic PNG </b></td>
                                                        <td style='width:10%;border: 1px solid black;'> </td>
                                                        <td style='width:15%;border: 1px solid black;'> " & PNG_Akademik & " </td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;'><b> Cocurriculum </b></td>
                                                        <td style='width:7%;border: 1px solid black;'>" & cocuricullum_value & "</td>
                                                        <td style='width:8%;border: 1px solid black;'>")

            ''kokorikulum kod sukan
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_KOD_SUKAN.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_SUKAN.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            ''kokorikulum kod kelab
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_KOD_KELAB.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_KELAB.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            ''kokorikulum kod uniform
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_KOD_UNIFORM.Rows
                    For Each column As DataColumn In DSCocuricullum_KOD_UNIFORM.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            Test.Append("                   </td>
                                                        <td style='width:30%;border: 1px solid black;text-align:left'>")

            ''kokorikulum nama skan
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_NAMA_SUKAN.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_SUKAN.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            ''kokorikulum nama kelab
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_NAMA_KELAB.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_KELAB.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            ''kokorikulum nama uniform
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_NAMA_UNIFORM.Rows
                    For Each column As DataColumn In DSCocuricullum_NAMA_UNIFORM.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("<br>")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;'>")

            ''kokorikulum gred 
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_GRED.Rows
                    For Each column As DataColumn In DSCocuricullum_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append("SM <br>")
            End If

            Test.Append("</td>
                                                        <td style='width:5%;border: 1px solid black;'>")

            ''kokorikulum png 
            If Confirm_Cocuricullum = "on" Then
                For Each row As DataRow In DSCocuricullum_PNG.Rows
                    For Each column As DataColumn In DSCocuricullum_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Cocuricullum = "off" Then
                Test.Append(" SM <br>")
            End If

            Test.Append("</td>
                                                        <td style='width:10%;border: 1px solid black;'></td>
                                                        <td style='width:15%;border: 1px solid black;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;'><b> Portfolio </b></td>
                                                        <td style='width:7%;border: 1px solid black;'> " & portfolio_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;'>")

            ''Portfolio KOD
            If Confirm_Portfolio = "on" Then
                For Each row As DataRow In DSPortfolio_KOD.Rows
                    For Each column As DataColumn In DSPortfolio_KOD.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Portfolio = "off" Then
                Test.Append("<br>")
            End If

            Test.Append("                   </td>
                                                        <td style='width:30%;border: 1px solid black;'></td>
                                                        <td style='width:5%;border: 1px solid black;'>")

            ''Portfolio Gred
            If Confirm_Portfolio = "on" Then
                For Each row As DataRow In DSPortfolio_GRED.Rows
                    For Each column As DataColumn In DSPortfolio_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Portfolio = "off" Then
                Test.Append(" SM <br>")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;'>")

            ''Portfolio PNG
            If Confirm_Portfolio = "on" Then
                For Each row As DataRow In DSPortfolio_PNG.Rows
                    For Each column As DataColumn In DSPortfolio_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("<br>")
                    Next
                Next
            ElseIf Confirm_Portfolio = "off" Then
                Test.Append(" SM <br>")
            End If

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;'></td>
                                                        <td style='width:15%;border: 1px solid black;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;'><b> Research </b></td>
                                                        <td style='width:7%;border: 1px solid black;'> " & research_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;'>")

            ''research KOD
            If Confirm_Research = "on" Then
                For Each row As DataRow In DSResearch_KOD.Rows
                    For Each column As DataColumn In DSResearch_KOD.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "off" Then
                Test.Append("<br>")
            End If

            Test.Append("</td>
                                                        <td style='width:30%;border: 1px solid black;'></td>
                                                        <td style='width:5%;border: 1px solid black;'>")

            ''research GRED
            If Confirm_Research = "on" Then
                For Each row As DataRow In DSResearch_GRED.Rows
                    For Each column As DataColumn In DSResearch_GRED.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "off" Then
                Test.Append(" SM <br>")
            End If

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;'>")

            ''research PNG
            If Confirm_Research = "on" Then
                For Each row As DataRow In DSResearch_PNG.Rows
                    For Each column As DataColumn In DSResearch_PNG.Columns
                        Test.Append(row(column.ColumnName))
                        Test.Append("</td>")
                    Next
                Next
            ElseIf Confirm_Research = "off" Then
                Test.Append(" SM <br>")
            End If

            Test.Append("</td>
                                                        <td style='width:10%;border: 1px solid black;'></td>
                                                        <td style='width:15%;border: 1px solid black;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;'><b> Self Development </b></td>
                                                        <td style='width:7%;border: 1px solid black;'> " & sd_value & " </td>
                                                        <td style='width:8%;border: 1px solid black;'>")

            ''(column self development codde / pembangunan kendiri kod)
            For Each row As DataRow In DSSelfdevelopment_KOD.Rows
                For Each column As DataColumn In DSSelfdevelopment_KOD.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Test.Append("                    </td>
                                                        <td style='width:30%;border: 1px solid black;'></td>
                                                        <td style='width:5%;border: 1px solid black;'>")

            ''(column self development grade / pembangunan kendiri gred)
            For Each row As DataRow In DSSelfdevelopment_GRED.Rows
                For Each column As DataColumn In DSSelfdevelopment_GRED.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append("<br>")
                Next
            Next

            Test.Append("                   </td>
                                                        <td style='width:5%;border: 1px solid black;'>")

            ''(column self development gpa / pembangunan kendiri png)
            For Each row As DataRow In DSSelfdevelopment_PNG.Rows
                For Each column As DataColumn In DSSelfdevelopment_PNG.Columns
                    Test.Append(row(column.ColumnName))
                    Test.Append(" <br>")
                Next
            Next

            Test.Append("                   </td>
                                                        <td style='width:10%;border: 1px solid black;'></td>
                                                        <td style='width:15%;border: 1px solid black;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;'><b> PNG </b></td>
                                                        <td style='width:7%;border: 1px solid black;'><b> " & gpa & " </b></td>
                                                        <td style='width:8%;border: 1px solid black;'></td>
                                                        <td style='width:30%;border: 1px solid black;'></td>
                                                        <td style='width:5%;border: 1px solid black;'></td>
                                                        <td style='width:5%;border: 1px solid black;'></td>
                                                        <td style='width:10%;border: 1px solid black;'></td>
                                                        <td style='width:15%;border: 1px solid black;'></td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black;text-align:center'>
                                                        <td style='width:20%;border: 1px solid black;'><b> PNGK </b></td>
                                                        <td style='width:7%;border: 1px solid black;'><b> " & cgpa & "</b></td>
                                                        <td style='width:8%;border: 1px solid black;'></td>
                                                        <td style='width:30%;border: 1px solid black;'></td>
                                                        <td style='width:5%;border: 1px solid black;'></td>
                                                        <td style='width:5%;border: 1px solid black;'></td>
                                                        <td style='width:10%;border: 1px solid black;'></td>
                                                        <td style='width:15%;border: 1px solid black;'></td>
                                                    </tr>
                                                </table>
                                            </tr>
                                         </table>    
                                     </div>")


            Test.AppendLine(" </div> </div>")
            Test.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataTESTBI'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

            ''print
            Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())
        End If
    End Sub

End Class