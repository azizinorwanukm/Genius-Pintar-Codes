Imports System.Data.SqlClient

Public Class pengarah_laporan_peperiksaan_kursus_table
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim MyConnection As SqlConnection = New SqlConnection(strConn)
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then

                year_list()
                exam_list()
                course_list()
                bahasa_list()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub year_list()
        Try

            strSQL = "  SELECT * FROM setting WHERE Type = 'Year'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.SelectedValue = Now.Year

        Catch ex As Exception

        End Try
    End Sub

    Private Sub exam_list()
        Try

            strSQL = "SELECT * FROM exam_info WHERE exam_Year='" & ddlYear.SelectedValue & "' ORDER BY exam_Name ASC"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExam.DataSource = ds
            ddlExam.DataTextField = "exam_Name"
            ddlExam.DataValueField = "exam_ID"
            ddlExam.DataBind()
            ddlExam.Items.Insert(0, New ListItem("Select Exam", String.Empty))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub course_list()
        strSQL = "SELECT exam_Name FROM exam_info WHERE exam_ID = '" & ddlExam.SelectedValue & "'"
        Dim examName As String = oCommon.getFieldValue(strSQL)

        Dim strSem As String = ""
        Dim strLevel As String = ""

        If examName = "Exam 1" Or examName = "Exam 2" Or examName = "Exam 5" Or examName = "Exam 6" Then

            strSem = "Sem 1"

        Else

            strSem = "Sem 2"

        End If

        Try

            strSQL = "SELECT course_Name FROM subject_info WHERE subject_year = '" & ddlYear.SelectedValue & "' AND subject_sem = '" & strSem & "'"

            If examName = "Exam 1" Or examName = "Exam 2" Or examName = "Exam 3" Or examName = "Exam 4" Then

                strSQL += " AND subject_StudentYear <> 'Level 2'"

            Else

                strSQL += " AND subject_StudentYear = 'Level 2'"

            End If

            strSQL += "GROUP BY course_Name ORDER BY course_Name ASC"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourse.DataSource = ds
            ddlCourse.DataTextField = "course_Name"
            ddlCourse.DataValueField = "course_Name"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))

        Catch ex As Exception

        End Try



    End Sub

    Private Sub bahasa_list()
        Try

            strSQL = "SELECT subject_Name FROM subject_info WHERE subject_year = '" & ddlYear.SelectedValue & "' AND course_Name = 'Bahasa Antarabangsa' GROUP BY subject_Name"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBahasa.DataSource = ds
            ddlBahasa.DataTextField = "subject_Name"
            ddlBahasa.DataValueField = "subject_Name"
            ddlBahasa.DataBind()
            ddlBahasa.Items.Insert(0, New ListItem("Bahasa Antarabangsa", String.Empty))

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        exam_list()
        course_list()
        bahasa_list()
        If ddlCourse.SelectedValue = "Bahasa Antarabangsa" Then
            ddlBahasa.Enabled = True
        Else
            ddlBahasa.Enabled = False
        End If
        graphFunction()
    End Sub

    Protected Sub ddlExam_SelectedIndexChanged(sender As Object, e As EventArgs)
        course_list()
        bahasa_list()
        If ddlCourse.SelectedValue = "Bahasa Antarabangsa" Then
            ddlBahasa.Enabled = True
        Else
            ddlBahasa.Enabled = False
        End If
        graphFunction()
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs)
        bahasa_list()
        strRet = BindData(datRespondent)
        If ddlCourse.SelectedValue = "Bahasa Antarabangsa" Then
            ddlBahasa.Enabled = True
        Else
            ddlBahasa.Enabled = False
        End If
        graphFunction()
    End Sub

    Protected Sub ddlBahasa_SelectedIndexChanged(sender As Object, e As EventArgs)
        strRet = BindData(datRespondent)
        graphFunction()
    End Sub

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

        Dim strSelect As String = ""
        Dim strWhere As String = ""
        Dim strGroupby As String = ""
        Dim strOrderby As String = " ORDER BY subject_info.subject_StudentYear ASC"

        strSelect = "   SELECT 
			            subject_info.subject_ID,
			            subject_info.course_Name,
			            subject_info.subject_Name,
			            subject_info.subject_code,
                        subject_info.subject_StudentYear,
			            COUNT(student_info.std_ID) AS 'Jumlah Pelajar',
			            COUNT(CASE WHEN exam_result.grade = 'A+' THEN 1 ELSE NULL END) AS 'A+',
			            COUNT(CASE WHEN exam_result.grade = 'A' THEN 1 ELSE NULL END) AS 'A',
			            COUNT(CASE WHEN exam_result.grade = 'A-' THEN 1 ELSE NULL END) AS 'A-',
			            COUNT(CASE WHEN exam_result.grade = 'B+' THEN 1 ELSE NULL END) AS 'B+',
			            COUNT(CASE WHEN exam_result.grade = 'B' THEN 1 ELSE NULL END) AS 'B',
			            COUNT(CASE WHEN exam_result.grade = 'B-' THEN 1 ELSE NULL END) AS 'B-',
			            COUNT(CASE WHEN exam_result.grade = 'C+' THEN 1 ELSE NULL END) AS 'C+',
			            COUNT(CASE WHEN exam_result.grade = 'C' THEN 1 ELSE NULL END) AS 'C',
			            COUNT(CASE WHEN exam_result.grade = 'D' THEN 1 ELSE NULL END) AS 'D',
			            COUNT(CASE WHEN exam_result.grade = 'E' THEN 1 ELSE NULL END) AS 'E',
			            COUNT(CASE WHEN exam_result.grade = 'G' THEN 1 ELSE NULL END) AS 'G',

			            CONCAT((COUNT(CASE WHEN exam_result.grade = 'A+' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%A+',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'A' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%A',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'A-' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%A-',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'B+' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%B+',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'B' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%B',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'B-' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%B-',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'C+' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%C+',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'C' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%C',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'D' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%D',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'E' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%E',
		                CONCAT((COUNT(CASE WHEN exam_result.grade = 'G' THEN 1 ELSE NULL END))*100/COUNT(student_info.std_ID),'%') AS '%G'
			            FROM
			            subject_info
			            LEFT JOIN course ON subject_info.subject_ID = course.subject_ID
			            LEFT JOIN exam_result ON exam_result.course_ID = course.course_ID
			            LEFT JOIN exam_Info ON exam_Info.exam_ID = exam_result.exam_ID
			            LEFT JOIN student_info ON student_info.std_ID = course.std_ID"
        strWhere = "    WHERE
			            exam_Info.exam_Year = '" & ddlYear.SelectedValue & "'
			            AND exam_Info.exam_ID = '" & ddlExam.SelectedValue & "'
			            AND subject_info.course_Name = '" & ddlCourse.SelectedValue & "'"

        If ddlBahasa.SelectedIndex > 0 Then
            strWhere += "   AND subject_Name = '" & ddlBahasa.SelectedValue & "'"
        End If

        strGroupby = "  GROUP BY
		                subject_info.subject_ID,
			            subject_info.course_Name,
			            subject_info.subject_Name,
			            subject_info.subject_code,
                        subject_info.subject_StudentYear"

        getSQL = strSelect & strWhere & strGroupby & strOrderby

        Return getSQL

    End Function

    Private Sub graphFunction()

        graph1.Value = 0
        graph2.Value = 0
        graph3.Value = 0
        graph4.Value = 0
        graph5.Value = 0

        lblKursus1.Text = ""
        lblKursus2.Text = ""
        lblKursus3.Text = ""
        lblKursus4.Text = ""
        lblKursus5.Text = ""

        table1_countaplus.Value = 0
        table1_countaa.Value = 0
        table1_countaminus.Value = 0
        table1_countbplus.Value = 0
        table1_countbb.Value = 0
        table1_countbminus.Value = 0
        table1_countcplus.Value = 0
        table1_countcc.Value = 0
        table1_countdd.Value = 0
        table1_countee.Value = 0
        table1_countgg.Value = 0

        table2_countaplus.Value = 0
        table2_countaa.Value = 0
        table2_countaminus.Value = 0
        table2_countbplus.Value = 0
        table2_countbb.Value = 0
        table2_countbminus.Value = 0
        table2_countcplus.Value = 0
        table2_countcc.Value = 0
        table2_countdd.Value = 0
        table2_countee.Value = 0
        table2_countgg.Value = 0

        table3_countaplus.Value = 0
        table3_countaa.Value = 0
        table3_countaminus.Value = 0
        table3_countbplus.Value = 0
        table3_countbb.Value = 0
        table3_countbminus.Value = 0
        table3_countcplus.Value = 0
        table3_countcc.Value = 0
        table3_countdd.Value = 0
        table3_countee.Value = 0
        table3_countgg.Value = 0

        table4_countaplus.Value = 0
        table4_countaa.Value = 0
        table4_countaminus.Value = 0
        table4_countbplus.Value = 0
        table4_countbb.Value = 0
        table4_countbminus.Value = 0
        table4_countcplus.Value = 0
        table4_countcc.Value = 0
        table4_countdd.Value = 0
        table4_countee.Value = 0
        table4_countgg.Value = 0

        table5_countaplus.Value = 0
        table5_countaa.Value = 0
        table5_countaminus.Value = 0
        table5_countbplus.Value = 0
        table5_countbb.Value = 0
        table5_countbminus.Value = 0
        table5_countcplus.Value = 0
        table5_countcc.Value = 0
        table5_countdd.Value = 0
        table5_countee.Value = 0
        table5_countgg.Value = 0

        Dim i As Integer

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim gradeaplus As Label = datRespondent.Rows(i).FindControl("aplus")
            Dim gradeaa As Label = datRespondent.Rows(i).FindControl("aa")
            Dim gradeaminus As Label = datRespondent.Rows(i).FindControl("aminus")
            Dim gradebplus As Label = datRespondent.Rows(i).FindControl("bplus")
            Dim gradebb As Label = datRespondent.Rows(i).FindControl("bb")
            Dim gradebminus As Label = datRespondent.Rows(i).FindControl("bminus")
            Dim gradecplus As Label = datRespondent.Rows(i).FindControl("cplus")
            Dim gradecc As Label = datRespondent.Rows(i).FindControl("cc")
            Dim gradedd As Label = datRespondent.Rows(i).FindControl("dd")
            Dim gradeee As Label = datRespondent.Rows(i).FindControl("ee")
            Dim gradegg As Label = datRespondent.Rows(i).FindControl("gg")

            Dim lblSubjectName As Label = datRespondent.Rows(i).FindControl("subject_Name")
            Dim lblStudentYear As Label = datRespondent.Rows(i).FindControl("subject_StudentYear")

            If i = 0 Then

                graph1.Value = 1
                lblKursus1.Text = lblSubjectName.Text & " : " & lblStudentYear.Text

                table1_countaplus.Value = gradeaplus.Text
                table1_countaa.Value = gradeaa.Text
                table1_countaminus.Value = gradeaminus.Text
                table1_countbplus.Value = gradebplus.Text
                table1_countbb.Value = gradebb.Text
                table1_countbminus.Value = gradebminus.Text
                table1_countcplus.Value = gradecplus.Text
                table1_countcc.Value = gradecc.Text
                table1_countdd.Value = gradedd.Text
                table1_countee.Value = gradeee.Text
                table1_countgg.Value = gradegg.Text
            End If

            If i = 1 Then

                graph2.Value = 1
                lblKursus2.Text = lblSubjectName.Text & " : " & lblStudentYear.Text

                table2_countaplus.Value = gradeaplus.Text
                table2_countaa.Value = gradeaa.Text
                table2_countaminus.Value = gradeaminus.Text
                table2_countbplus.Value = gradebplus.Text
                table2_countbb.Value = gradebb.Text
                table2_countbminus.Value = gradebminus.Text
                table2_countcplus.Value = gradecplus.Text
                table2_countcc.Value = gradecc.Text
                table2_countdd.Value = gradedd.Text
                table2_countee.Value = gradeee.Text
                table2_countgg.Value = gradegg.Text
            End If

            If i = 2 Then

                graph3.Value = 1
                lblKursus3.Text = lblSubjectName.Text & " : " & lblStudentYear.Text

                table3_countaplus.Value = gradeaplus.Text
                table3_countaa.Value = gradeaa.Text
                table3_countaminus.Value = gradeaminus.Text
                table3_countbplus.Value = gradebplus.Text
                table3_countbb.Value = gradebb.Text
                table3_countbminus.Value = gradebminus.Text
                table3_countcplus.Value = gradecplus.Text
                table3_countcc.Value = gradecc.Text
                table3_countdd.Value = gradedd.Text
                table3_countee.Value = gradeee.Text
                table3_countgg.Value = gradegg.Text
            End If

            If i = 3 Then

                graph4.Value = 1
                lblKursus4.Text = lblSubjectName.Text & " : " & lblStudentYear.Text

                table4_countaplus.Value = gradeaplus.Text
                table4_countaa.Value = gradeaa.Text
                table4_countaminus.Value = gradeaminus.Text
                table4_countbplus.Value = gradebplus.Text
                table4_countbb.Value = gradebb.Text
                table4_countbminus.Value = gradebminus.Text
                table4_countcplus.Value = gradecplus.Text
                table4_countcc.Value = gradecc.Text
                table4_countdd.Value = gradedd.Text
                table4_countee.Value = gradeee.Text
                table4_countgg.Value = gradegg.Text
            End If

            If i = 4 Then

                graph5.Value = 1
                lblKursus5.Text = lblSubjectName.Text & " : " & lblStudentYear.Text

                table5_countaplus.Value = gradeaplus.Text
                table5_countaa.Value = gradeaa.Text
                table5_countaminus.Value = gradeaminus.Text
                table5_countbplus.Value = gradebplus.Text
                table5_countbb.Value = gradebb.Text
                table5_countbminus.Value = gradebminus.Text
                table5_countcplus.Value = gradecplus.Text
                table5_countcc.Value = gradecc.Text
                table5_countdd.Value = gradedd.Text
                table5_countee.Value = gradeee.Text
                table5_countgg.Value = gradegg.Text
            End If
        Next

    End Sub

End Class