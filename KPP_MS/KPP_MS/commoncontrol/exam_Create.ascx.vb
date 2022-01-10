Imports System.Data.SqlClient

Public Class exam_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                examName_List()
                examYear_List()

                previousPage.NavigateUrl = String.Format("~/admin_peperiksaan_pengurusan_peperiksaan.aspx?admin_ID=" + Request.QueryString("admin_ID"))
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub examName_List()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Exam' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamName.DataSource = ds
            ddlExamName.DataTextField = "Parameter"
            ddlExamName.DataValueField = "Parameter"
            ddlExamName.DataBind()
            ddlExamName.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub examYear_List()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "Parameter"
            ddlExamYear.DataValueField = "Parameter"
            ddlExamYear.DataBind()
            ddlExamYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick

        If ddlExamName.SelectedIndex > 0 Then

            If txtExamCode.Text.Length > 0 Then

                If ddlExamYear.SelectedIndex > 0 Then

                    If txtStartDate.Text <> "" And Regex.IsMatch(txtStartDate.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then

                        If txtEndDate.Text <> "" And Regex.IsMatch(txtEndDate.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then

                            'Insert new exam in exam_Info
                            Using PJGDATA As New SqlCommand("INSERT into exam_Info(exam_Name,exam_Code,exam_Year,exam_StartDate,exam_EndDate) values ('" & ddlExamName.SelectedValue & "','" & txtExamCode.Text & "','" & ddlExamYear.SelectedValue & "','" & txtStartDate.Text & "','" & txtEndDate.Text & "')", objConn)
                                objConn.Open()
                                Dim i = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                                If i <> 0 Then
                                    ShowMessage("Successfull register new exam", MessageType.Success)
                                Else
                                    ShowMessage("Unsuccessfull register new exam", MessageType.Error)
                                End If
                            End Using

                            ''print exam name
                            If ddlExamName.SelectedValue = "Exam 1" Or ddlExamName.SelectedValue = "Exam 2" Then

                                ''get exam id
                                Dim exmID As String = "select exam_ID from exam_Info where exam_Name = '" & ddlExamName.SelectedValue & "' and exam_Year = '" & ddlExamYear.SelectedValue & "'"
                                Dim dataexmID As String = getFieldValue(exmID, strConn)

                                'Insert student taking exam at exam_result
                                Using PJGDATA As New SqlCommand("INSERT into exam_result(exam_ID,course_ID) 
                                                                 select '" & dataexmID & "',course_ID from course left join student_level on course.std_ID = student_level.std_ID 
                                                                 where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and student_level.student_Level != 'Level 2' and student_level.student_Level != 'Foundation 2' and student_level.student_Level != 'Foundation 3' and student_level.student_Sem = 'Sem 1' and student_level.year = '" & ddlExamYear.SelectedValue & "' order by course_ID ASC ", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using

                            ElseIf ddlExamName.SelectedValue = "Exam 3" Or ddlExamName.SelectedValue = "Exam 4" Then

                                ''get exam id
                                Dim exmID As String = "select exam_ID from exam_Info where exam_Name = '" & ddlExamName.SelectedValue & "' and exam_Year = '" & ddlExamYear.SelectedValue & "'"
                                Dim dataexmID As String = getFieldValue(exmID, strConn)

                                'Insert student taking exam at exam_result
                                Using PJGDATA As New SqlCommand("INSERT into exam_result(exam_ID,course_ID) 
                                                                 select '" & dataexmID & "',course_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                                 where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear != 'Level 2' 
                                                                 and subject_info.subject_StudentYear != 'Foundation 2' and subject_info.subject_StudentYear != 'Foundation 3' 
                                                                 and subject_info.subject_sem = 'Sem 2' and subject_info.subject_year = '" & ddlExamYear.SelectedValue & "'  order by course_ID ASC ", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using

                            ElseIf ddlExamName.SelectedValue = "Exam 5" Or ddlExamName.SelectedValue = "Exam 6" Then

                                ''get exam id
                                Dim exmID As String = "select exam_ID from exam_Info where exam_Name = '" & ddlExamName.SelectedValue & "' and exam_Year = '" & ddlExamYear.SelectedValue & "'"
                                Dim dataexmID As String = getFieldValue(exmID, strConn)

                                'Insert student taking exam at exam_result
                                Using PJGDATA As New SqlCommand("INSERT into exam_result(exam_ID,course_ID) 
                                                                 select '" & dataexmID & "',course_ID from course left join student_level on course.std_ID = student_level.std_ID 
                                                                 where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and student_level.student_Level != 'Level 1' and student_level.year = '" & ddlExamYear.SelectedValue & "' and student_level.student_Level != 'Foundation 1' and student_level.student_Level != 'Foundation 3' and student_level.student_Sem = 'Sem 1' order by course_ID ASC ", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using

                            ElseIf ddlExamName.SelectedValue = "Exam 7" Then

                                ''get exam id
                                Dim exmID As String = "select exam_ID from exam_Info where exam_Name = '" & ddlExamName.SelectedValue & "' and exam_Year = '" & ddlExamYear.SelectedValue & "'"
                                Dim dataexmID As String = getFieldValue(exmID, strConn)

                                'Insert student taking exam at exam_result
                                Using PJGDATA As New SqlCommand("INSERT into exam_result(exam_ID,course_ID) 
                                                                 select '" & dataexmID & "',course_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                                 where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear != 'Level 1' 
                                                                 and subject_info.subject_StudentYear != 'Foundation 1' and subject_info.subject_StudentYear != 'Foundation 3' 
                                                                 and subject_info.subject_sem = 'Sem 2' and subject_info.subject_year = '" & ddlExamYear.SelectedValue & "'  order by course_ID ASC ", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using

                            ElseIf ddlExamName.SelectedValue = "Exam 8" Then

                                ''get exam id
                                Dim exmID As String = "select exam_ID from exam_Info where exam_Name = '" & ddlExamName.SelectedValue & "' and exam_Year = '" & ddlExamYear.SelectedValue & "'"
                                Dim dataexmID As String = getFieldValue(exmID, strConn)

                                'Insert student taking exam at exam_result
                                Using PJGDATA As New SqlCommand("INSERT into exam_result(exam_ID,course_ID) 
                                                                 select '" & dataexmID & "',course_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                                 where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear != 'Level 1' and subject_info.subject_StudentYear != 'Level 2' 
                                                                 and subject_info.subject_StudentYear != 'Foundation 1' and subject_info.subject_StudentYear != 'Foundation 3' 
                                                                 and subject_info.subject_sem = 'Sem 2' and subject_info.subject_year = '" & ddlExamYear.SelectedValue & "'  order by course_ID ASC ", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using

                            ElseIf ddlExamName.SelectedValue = "Exam 9" Or ddlExamName.SelectedValue = "Exam 10" Then

                                ''get exam id
                                Dim exmID As String = "select exam_ID from exam_Info where exam_Name = '" & ddlExamName.SelectedValue & "' and exam_Year = '" & ddlExamYear.SelectedValue & "'"
                                Dim dataexmID As String = getFieldValue(exmID, strConn)

                                'Insert student taking exam at exam_result
                                Using PJGDATA As New SqlCommand("INSERT into exam_result(exam_ID,course_ID) 
                                                                 select '" & dataexmID & "',course_ID from course left join student_level on course.ID = student_level.ID 
                                                                 where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and student_level.student_Level = 'Foundation 3' and student_level.year = '" & ddlExamYear.SelectedValue & "' and student_level.student_Sem = 'Sem 1' order by course_ID ASC ", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using

                            ElseIf ddlExamName.SelectedValue = "Exam 11" Or ddlExamName.SelectedValue = "Exam 12" Then

                                ''get exam id
                                Dim exmID As String = "select exam_ID from exam_Info where exam_Name = '" & ddlExamName.SelectedValue & "' and exam_Year = '" & ddlExamYear.SelectedValue & "'"
                                Dim dataexmID As String = getFieldValue(exmID, strConn)

                                'Insert student taking exam at exam_result
                                Using PJGDATA As New SqlCommand("INSERT into exam_result(exam_ID,course_ID) 
                                                                 select '" & dataexmID & "',course_ID from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                                                 where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 3' 
                                                                 and subject_info.subject_sem = 'Sem 2' and subject_info.subject_year = '" & ddlExamYear.SelectedValue & "'  order by course_ID ASC ", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using

                            End If

                            ''get exam result id for subject self development
                            Dim examID_selfdevelopment As String = " select A.ID from exam_result A
                                                                     left join course B on A.course_ID = B.course_ID
                                                                     left join subject_info C on B.subject_Id = C.subject_id
                                                                     left join exam_info D on A.exam_ID = D.exam_ID
                                                                     where B.year = '" & ddlExamYear.SelectedValue & "'
                                                                     and C.subject_year = '" & ddlExamYear.SelectedValue & "'
                                                                     and D.exam_Year = '" & ddlExamYear.SelectedValue & "'
                                                                     and D.exam_Name = '" & ddlExamName.SelectedValue & "'
                                                                     and C.subject_name = 'Self Development'"
                            Dim ID_selfdevelpment As String = getFieldValue(examID_selfdevelopment, strConn)

                            If ID_selfdevelpment.Length > 0 Then
                                'insert data into self_deveopment_mark table
                                Using PJGDATA As New SqlCommand("INSERT INTO self_development_mark(examresult_id,year) VALUES('" & ID_selfdevelpment & "','" & ddlExamYear.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using
                            End If

                            ''get exam result id for subject personality development
                            Dim examID_personalitydevelopment As String = " select A.ID from exam_result A
                                                                            left join course B on A.course_ID = B.course_ID
                                                                            left join subject_info C on B.subject_Id = C.subject_id
                                                                            left join exam_info D on A.exam_ID = D.exam_ID
                                                                            where B.year = '" & ddlExamYear.SelectedValue & "'
                                                                            and C.subject_year = '" & ddlExamYear.SelectedValue & "'
                                                                            and D.exam_Year = '" & ddlExamYear.SelectedValue & "'
                                                                            and D.exam_Name = '" & ddlExamName.SelectedValue & "'
                                                                            and C.subject_name = 'Personality Development'"
                            Dim ID_personalitydevelpment As String = getFieldValue(examID_selfdevelopment, strConn)

                            If ID_personalitydevelpment.Length > 0 Then
                                'insert data into self_deveopment_mark table
                                Using PJGDATA As New SqlCommand("INSERT INTO self_development_mark(examresult_id,year) VALUES('" & ID_selfdevelpment & "','" & ddlExamYear.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using
                            End If

                            ''get exam result id for subject portfolio
                            Dim examID_portfolio As String = " select A.ID from exam_result A
                                                               left join course B on A.course_ID = B.course_ID
                                                               left join subject_info C on B.subject_Id = C.subject_id
                                                               left join exam_info D on A.exam_ID = D.exam_ID
                                                               where B.year = '" & ddlExamYear.SelectedValue & "'
                                                               and C.subject_year = '" & ddlExamYear.SelectedValue & "'
                                                               and D.exam_Year = '" & ddlExamYear.SelectedValue & "'
                                                               and D.exam_Name = '" & ddlExamName.SelectedValue & "'
                                                               and C.subject_name = 'Portfolio'"
                            Dim ID_portfolio As String = getFieldValue(examID_portfolio, strConn)

                            If ID_portfolio.Length > 0 Then
                                'insert data into portfolio_mark table
                                Using PJGDATA As New SqlCommand("INSERT INTO portfolio_mark(examresult_id,year) VALUES('" & ID_selfdevelpment & "','" & ddlExamYear.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using
                            End If

                            ''Insert into exam create 
                            Insert_studentPng_Data()

                            txtExamCode.Text = ""
                            txtStartDate.Text = ""
                            txtEndDate.Text = ""
                        Else
                            ShowMessage("Please enter a valid exam end date", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please enter a valid exam start date", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please enter a valid exam year", MessageType.Error)
                End If
            Else
                ShowMessage("Please enter a valid exam code", MessageType.Error)
            End If
        Else
            ShowMessage("Please enter a valid exam name", MessageType.Error)
        End If
    End Sub


    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Sub Insert_studentPng_Data()

        If ddlExamName.SelectedValue = "Exam 1" Or ddlExamName.SelectedValue = "Exam 2" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'ASAS' from course left join bject_info on course.subject_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 1' and subject_info.subject_sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

            Using STDDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'TAHAP' from course left join subject_info on course.subject_ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Level 1' and subject_info.subject_sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim K = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 3" Or ddlExamName.SelectedValue = "Exam 4" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                            select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'ASAS' from course left join subject_info on course.subject_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 1' and subject_info.student_Sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

            Using STDDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                            select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'TAHAP' from course left join student_level on course.std_ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Level 1' and subject_info.student_Sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim K = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 5" Or ddlExamName.SelectedValue = "Exam 6" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0','ASAS' from course left join student_level on course.std_ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 2' and subject_info.student_Sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

            Using STDDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                            select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'TAHAP' from course left join student_level on course.std_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Level 2' and subject_info.student_Sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim K = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 7" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0','ASAS' from course left join student_level on course.ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 2' and subject_info.student_Sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

            Using STDDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type) 
                                            select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0', 'TAHAP' from course left join student_level on course.std_ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Level 2' and subject_info.student_Sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim K = STDDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 8" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0','ASAS' from course left join student_level on course.ID = subject_info.subject_ID
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 2' and subject_info.student_Sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 9" Or ddlExamName.SelectedValue = "Exam 10" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0','ASAS' from course left join student_level on course.ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 3' and subject_info.student_Sem = 'Sem 1' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        ElseIf ddlExamName.SelectedValue = "Exam 11" Or ddlExamName.SelectedValue = "Exam 12" Then

            'Insert student taking exam at exam_result
            Using PJGDATA As New SqlCommand("INSERT into student_Png(exam_Name,std_ID,year,png,pngs,student_type)  
                                             select distinct '" & ddlExamName.SelectedValue & "', course.std_ID, '" & ddlExamYear.SelectedValue & "', '0', '0','ASAS' from course left join student_level on course.ID = subject_info.subject_ID 
                                             where course_ID is not null and course.year = '" & ddlExamYear.SelectedValue & "' and subject_info.subject_StudentYear = 'Foundation 3' and subject_info.student_Sem = 'Sem 2' and course.year = '" & ddlExamYear.SelectedValue & "' order by course.std_ID ASC ", objConn)
                objConn.Open()
                Dim j = PJGDATA.ExecuteNonQuery()
                objConn.Close()
            End Using

        End If
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
End Class