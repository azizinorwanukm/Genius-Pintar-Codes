Imports System.Data.SqlClient
Imports System.Globalization

Public Class counselor_Self_Development_Mark
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Dim get_PercenATM As Decimal = 0.00
    Dim get_PercenREM As Decimal = 0.00
    Dim get_PercenASM As Decimal = 0.00
    Dim get_PercenLEM As Decimal = 0.00
    Dim get_PercenCSM As Decimal = 0.00
    Dim get_PercenSDM As Decimal = 0.00
    Dim get_PercenRTM As Decimal = 0.00
    Dim get_PercenAPM As Decimal = 0.00

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    Year_List_Info()
                    Program_List_Info()
                    Exam_List_Info()
                    Level_List_Info()

                    strRet = BindData(datRespondent)

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Year_List_Info()
        strSQL = "SELECT distinct year from personality_development_mark order by year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "year"
            ddlYear.DataValueField = "year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Program_List_Info()
        strSQL = "SELECT * from setting where Type = 'Stream'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlProgram.DataSource = ds
            ddlProgram.DataTextField = "Parameter"
            ddlProgram.DataValueField = "Value"
            ddlProgram.DataBind()
            ddlProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Exam_List_Info()

        If ddlLevelnaming.SelectedValue = "Foundation 1" Then
            strSQL = "Select exam_ID, exam_Name from exam_info where exam_year = '" & ddlYear.SelectedValue & "' and (exam_Name = 'Exam 1' or exam_Name = 'Exam 2' or exam_Name = 'Exam 3' or exam_Name = 'Exam 4') order by exam_name asc"
        ElseIf ddlLevelnaming.SelectedValue = "Foundation 2" Then
            strSQL = "Select exam_ID, exam_Name from exam_info where exam_year = '" & ddlYear.SelectedValue & "' and (exam_Name = 'Exam 5' or exam_Name = 'Exam 6' or exam_Name = 'Exam 7' or exam_Name = 'Exam 8') order by exam_name asc"
        ElseIf ddlLevelnaming.SelectedValue = "Foundation 3" Then
            strSQL = "Select exam_ID, exam_Name from exam_info where exam_year = '" & ddlYear.SelectedValue & "' and (exam_Name = 'Exam 9' or exam_Name = 'Exam 10' or exam_Name = 'Exam 11' or exam_Name = 'Exam 12') order by exam_name asc"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamnaming.DataSource = ds
            ddlExamnaming.DataTextField = "exam_Name"
            ddlExamnaming.DataValueField = "exam_ID"
            ddlExamnaming.DataBind()
            ddlExamnaming.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Level_List_Info()
        strSQL = "SELECT * from setting where Type = 'Level' and Parameter <> 'Level 1' and Parameter <> 'Level 2'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevelnaming.DataSource = ds
            ddlLevelnaming.DataTextField = "Parameter"
            ddlLevelnaming.DataValueField = "Parameter"
            ddlLevelnaming.DataBind()
            ddlLevelnaming.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Class_List_Info()
        strSQL = "SELECT * from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_Level = '" & ddlLevelnaming.SelectedValue & "' and class_Type = 'Compulsory' and class_Campus = 'PGPN' order by class_name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassnaming.DataSource = ds
            ddlClassnaming.DataTextField = "class_Name"
            ddlClassnaming.DataValueField = "class_ID"
            ddlClassnaming.DataBind()
            ddlClassnaming.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
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

            If ddlLevelnaming.SelectedValue = "Foundation 3" Then
                gvTable.Columns(5).Visible = False
            Else
                gvTable.Columns(5).Visible = True
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY F.class_Name, E.student_Name ASC"

        tmpSQL = "  SELECT A.sd_id, E.student_Name, E.student_ID, F.class_Name, A.attendance_mark, A.appearance_mark, A.reflection_mark, A.assignment_mark, A.leadership_mark, A.communityservice_mark,
                    A.selfdevelopment_mark, A.roomtidiness_mark, A.appearance_mark, A.merit_mark, A.sd_total, A.sd_grade from self_development_mark A
                    LEFT JOIN course C on A.courseID = C.course_id
                    LEFT JOIN exam_info D on A.examID = D.exam_id
                    LEFT JOIN student_info E ON C.std_ID = E.std_ID
                    LEFT JOIN class_info F ON C.class_ID = F.class_ID
                    LEFT JOIN subject_info G ON C.subject_ID = G.subject_ID

                    WHERE C.year = '" & ddlYear.SelectedValue & "' AND F.class_year = '" & ddlYear.SelectedValue & "' AND G.subject_year = '" & ddlYear.SelectedValue & "'
                    AND F.class_level = '" & ddlLevelnaming.SelectedValue & "' AND G.subject_StudentYear = '" & ddlLevelnaming.SelectedValue & "'

                    AND G.subject_Type = 'Compulsory' AND F.class_Type = 'Compulsory' AND (E.student_status = 'Access' or E.student_status = 'Graduate') AND E.student_Stream = '" & ddlProgram.SelectedValue & "'
                    AND D.exam_ID = '" & ddlExamnaming.SelectedValue & "' AND E.student_Campus = 'PGPN' 
                    And (G.subject_Name = 'Self Development' OR G.subject_NameBM = 'Pembangunan Diri')"

        If ddlClassnaming.SelectedIndex > 0 Then
            strWhere += " AND F.class_ID = '" & ddlClassnaming.SelectedValue & "' "
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            Class_List_Info()
            Exam_List_Info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgram.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevelnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelnaming.SelectedIndexChanged
        Try
            Class_List_Info()
            Exam_List_Info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlClassnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlExamnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        Dim i As Integer = 0
        Dim value As String = ""

        Dim sql_LastDateExam As String = "Select exam_EndDate from exam_info where exam_ID = '" & ddlExamnaming.SelectedValue & "'"
        Dim get_LastDateExam As String = oCommon.getFieldValue(sql_LastDateExam)

        Dim convertToDate_examEndDate As DateTime = DateTime.ParseExact(get_LastDateExam, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo)
        Dim formatted_examEndDate As String = convertToDate_examEndDate.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo)

        Dim get_currentDate As String = DateTime.Now.ToString("yyyyMMdd")

        If get_currentDate < formatted_examEndDate Then

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then

                        ''get student id
                        Dim find_stdid As String = "select distinct std_ID from course
                                                    left join self_development_mark on course.course_ID = self_development_mark.courseID where sd_id = '" & strKey & "'"
                        Dim get_stdid As String = oCommon.getFieldValue(find_stdid)

                        Dim txt_attendance As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtattendance_mark"), TextBox)
                        Dim txt_reflection As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtreflection_mark"), TextBox)
                        Dim txt_assignment As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtassignment_mark"), TextBox)
                        Dim txt_leadership As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtleadership_mrk"), TextBox)
                        Dim txt_communityservice As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtcommunityservice_mark"), TextBox)
                        Dim txt_selfdevelopment As TextBox = DirectCast(datRespondent.Rows(i).FindControl("selfdevelopment_mark"), TextBox)
                        Dim txt_roomtidiness As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtroomtidiness_mark"), TextBox)
                        Dim txt_appearance As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtappearance_mark"), TextBox)
                        Dim txt_merit As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtmerit_mark"), TextBox)

                        Dim Data_ATM As String = txt_attendance.Text
                        Dim Data_REM As String = txt_reflection.Text
                        Dim Data_ASM As String = txt_assignment.Text
                        Dim Data_LEM As String = txt_leadership.Text
                        Dim Data_CSM As String = txt_communityservice.Text
                        Dim Data_PDM As String = txt_selfdevelopment.Text
                        Dim Data_RTM As String = txt_roomtidiness.Text
                        Dim Data_APM As String = txt_appearance.Text
                        Dim Data_MM As String = txt_merit.Text

                        If checking_Data(Data_ATM, Data_REM, Data_ASM, Data_LEM, Data_CSM, Data_PDM, Data_RTM, Data_APM, Data_MM) = True Then

                            Dim find_sumpoint As String = ""
                            Dim get_sumponint As Decimal = 0.00

                            If ddlExamnaming.SelectedValue = "Exam 1" Or ddlExamnaming.SelectedValue = "Exam 5" Or ddlExamnaming.SelectedValue = "Exam 9" Then
                                ''get the demerit point and sum the demerit point  
                                strSQL = "select sum(meritdemerit_point) from dicipline_info where Dicipline_Date like '%" & ddlYear.SelectedValue & "%' and std_ID = '" & get_stdid & "'"
                            Else
                                Dim examName As String = ddlExamnaming.SelectedValue
                                Dim lastChar As Integer = Integer.Parse(examName.Substring(5)) - 1 ''Get The Previeous Exam No

                                ''Get Exam End Date === Previous Exam End Date
                                strSQL = "Select exam_EndDate from exam_info where exam_Name = 'Exam " & lastChar & "' and exam_Year = '" & ddlYear.SelectedValue & "'"
                                Dim Previous_EndDate As String = oCommon.getFieldValue(strSQL)

                                ''Get Exam End Date === Current Exam End Date
                                strSQL = "Select exam_EndDate from exam_info where exam_Name = '" & ddlExamnaming.SelectedValue & "' and exam_Year = '" & ddlYear.SelectedValue & "'"
                                Dim Current_EndDate As String = oCommon.getFieldValue(strSQL)

                                ''Get the demerit point and sum the demerit point  
                                strSQL = "select sum(meritdemerit_point) from dicipline_info where Dicipline_Date > '" & Previous_EndDate & "' and Dicipline_Date <= '" & Current_EndDate & "' and std_ID = '" & get_stdid & "'"
                                get_sumponint = Decimal.Parse(oCommon.getFieldValue(find_sumpoint))
                            End If

                            ''Get Total Mark
                            Dim total_PDM As Decimal = txt_selfdevelopment.Text - get_sumponint
                            Dim total_mark As Decimal = 0.00

                            If ddlLevelnaming.SelectedValue <> "Foundation 3" Then
                                total_mark = txt_attendance.Text + txt_leadership.Text + txt_communityservice.Text + txt_reflection.Text + txt_assignment.Text + txt_appearance.Text + txt_roomtidiness.Text + total_PDM + txt_merit.Text
                            Else
                                total_mark = txt_leadership.Text + txt_communityservice.Text + txt_reflection.Text + txt_assignment.Text + txt_appearance.Text + txt_roomtidiness.Text + total_PDM + txt_merit.Text
                            End If

                            ''Get Grade
                            strSQL = "Select grade_Name from grade_info where grade_min_range >= '" & total_mark & "' and grade_max_range = '" & total_mark & "'"
                            Dim get_GradeData As String = oCommon.getFieldValue(strSQL)

                            ''update to database
                            strSQL = "UPDATE self_development_mark
                                      SET attendance_percen = '" & get_PercenATM & "', attendance_mark = '" & txt_attendance.Text & "',
                                      reflection_percen = '" & get_PercenREM & "', reflection_mark = '" & txt_reflection.Text & "',
                                      assignment_percen = '" & get_PercenASM & "', assignment_mark = '" & txt_assignment.Text & "',
                                      leadership_percen = '" & get_PercenLEM & "', leadership_mark = '" & txt_leadership.Text & "',
                                      communityservice_percen = '" & get_PercenCSM & "', communityservice_mark = '" & txt_communityservice.Text & "',
                                      selfdevelopment_percen = '" & get_PercenSDM & "', selfdevelopment_mark = '" & total_PDM & "',
                                      appearance_percen = '" & get_PercenAPM & "', appearance_mark = '" & txt_appearance.Text & "',
                                      roomtidiness_percen = '" & get_PercenRTM & "', roomtidiness_mark = '" & txt_roomtidiness.Text & "',
                                      merit_mark = '" & txt_merit.Text & "', sd_grade ='" & get_GradeData & "',
                                      sd_total = '" & total_mark & "'
                                      Where sd_id = '" & strKey & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                Dim find_examID As String = "Select examID from self_development_mark where sd_id = '" & strKey & "'"
                                Dim get_examID As String = oCommon.getFieldValue(find_examID)

                                Dim find_courseID As String = "Select courseID from self_development_mark where sd_id = '" & strKey & "'"
                                Dim get_courseID As String = oCommon.getFieldValue(find_courseID)

                                Dim select_examresultid As String = "select ID from exam_result where exam_ID = '" & get_examID & "' and course_ID = '" & get_courseID & "'"
                                Dim get_examresultid As String = oCommon.getFieldValue(select_examresultid)

                                strSQL = "UPDATE exam_result SET
                                          marks = '" & total_mark & "', grade = '" & get_GradeData & "'
                                          WHERE ID = '" & get_examresultid & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                ShowMessage(" Unablt To Update Student Result ", MessageType.Error)
                                Exit For
                            End If
                        Else
                            Exit For
                        End If

                    End If
                End If
            Next

            If strRet = "0" Then
                ShowMessage(" Update Student Result ", MessageType.Success)
            End If

        Else
            ShowMessage(" Unable To Update Previous Result. Closed After " & get_LastDateExam, MessageType.Error)
        End If

    End Sub

    Private Function checking_Data(ATM_Data As String, REM_Data As String, ASM_Data As String, LEM_Data As String, CSM_Data As String, SDM_Data As String, RTM_Data As String, APM_Data As String, MM_Data As String)

        If ddlLevelnaming.SelectedValue <> "Foundation 3" Then
            Dim find_PercenAttendance As String = "select CM_Percentage from counselor_management_rubrick where CM_Year = '" & ddlYear.SelectedValue & "' and CM_Program = '" & ddlProgram.SelectedValue & "' and CM_Level = '" & ddlLevelnaming.SelectedValue & "' and CM_Name = 'Attendance'"
            get_PercenATM = Decimal.Parse(oCommon.getFieldValue(find_PercenAttendance))
        End If

        Dim find_PercenReflection As String = "select CM_Percentage from counselor_management_rubrick where CM_Year = '" & ddlYear.SelectedValue & "' and CM_Program = '" & ddlProgram.SelectedValue & "' and CM_Level = '" & ddlLevelnaming.SelectedValue & "' and CM_Name = 'Reflection'"
        get_PercenREM = Decimal.Parse(oCommon.getFieldValue(find_PercenReflection))

        Dim find_PercenAssignment As String = "select CM_Percentage from counselor_management_rubrick where CM_Year = '" & ddlYear.SelectedValue & "' and CM_Program = '" & ddlProgram.SelectedValue & "' and CM_Level = '" & ddlLevelnaming.SelectedValue & "' and CM_Name = 'Assignment'"
        get_PercenASM = Decimal.Parse(oCommon.getFieldValue(find_PercenAssignment))

        Dim find_PercenLeadership As String = "select CM_Percentage from counselor_management_rubrick where CM_Year = '" & ddlYear.SelectedValue & "' and CM_Program = '" & ddlProgram.SelectedValue & "' and CM_Level = '" & ddlLevelnaming.SelectedValue & "' and CM_Name = 'Leadership'"
        get_PercenLEM = Decimal.Parse(oCommon.getFieldValue(find_PercenLeadership))

        Dim find_PercenCommunityServices As String = "select CM_Percentage from counselor_management_rubrick where CM_Year = '" & ddlYear.SelectedValue & "' and CM_Program = '" & ddlProgram.SelectedValue & "' and CM_Level = '" & ddlLevelnaming.SelectedValue & "' and CM_Name = 'Community Services'"
        get_PercenCSM = Decimal.Parse(oCommon.getFieldValue(find_PercenCommunityServices))

        Dim find_PercenSelfDevelopmentAssessment As String = "select CM_Percentage from counselor_management_rubrick where CM_Year = '" & ddlYear.SelectedValue & "' and CM_Program = '" & ddlProgram.SelectedValue & "' and CM_Level = '" & ddlLevelnaming.SelectedValue & "' and CM_Name = 'Self Development Assessment'"
        get_PercenSDM = Decimal.Parse(oCommon.getFieldValue(find_PercenSelfDevelopmentAssessment))

        Dim find_PercenRoomTidiness As String = "select CM_Percentage from counselor_management_rubrick where CM_Year = '" & ddlYear.SelectedValue & "' and CM_Program = '" & ddlProgram.SelectedValue & "' and CM_Level = '" & ddlLevelnaming.SelectedValue & "' and CM_Name = 'Room Tidiness'"
        get_PercenRTM = Decimal.Parse(oCommon.getFieldValue(find_PercenRoomTidiness))

        Dim find_PercenAppearance As String = "select CM_Percentage from counselor_management_rubrick where CM_Year = '" & ddlYear.SelectedValue & "' and CM_Program = '" & ddlProgram.SelectedValue & "' and CM_Level = '" & ddlLevelnaming.SelectedValue & "' and CM_Name = 'Appearance'"
        get_PercenAPM = Decimal.Parse(oCommon.getFieldValue(find_PercenAppearance))

        If ddlLevelnaming.SelectedValue <> "Foundation 3" Then
            If ATM_Data.Length = 0 Then
                ShowMessage(" Please Fill In Attendance Mark ", MessageType.Error)
                Return False
            End If
        End If

        If REM_Data.Length = 0 Then
            ShowMessage(" Please Fill In Reflection Mark ", MessageType.Error)
            Return False
        End If

        If Decimal.Parse(REM_Data) > get_PercenREM Then
            ShowMessage(" Please Fill In Reflection Mark Less Than " & get_PercenREM, MessageType.Error)
            Return False
        End If

        If ASM_Data.Length = 0 Then
            ShowMessage(" Please Fill In Assignment Mark ", MessageType.Error)
            Return False
        End If

        If Decimal.Parse(ASM_Data) > get_PercenASM Then
            ShowMessage(" Please Fill In Assignment Mark Less Than " & get_PercenASM, MessageType.Error)
            Return False
        End If

        If LEM_Data.Length = 0 Then
            ShowMessage(" Please Fill In Leadership Mark ", MessageType.Error)
            Return False
        End If

        If Decimal.Parse(LEM_Data) > get_PercenLEM Then
            ShowMessage(" Please Fill In Leadership Mark Less Than " & get_PercenLEM, MessageType.Error)
            Return False
        End If

        If CSM_Data.Length = 0 Then
            ShowMessage(" Please Fill In Community Services Mark ", MessageType.Error)
            Return False
        End If

        If Decimal.Parse(CSM_Data) > get_PercenCSM Then
            ShowMessage(" Please Fill In Community Services Mark Less Than " & get_PercenCSM, MessageType.Error)
            Return False
        End If

        If SDM_Data.Length = 0 Then
            ShowMessage(" Please Fill In Self Development Mark ", MessageType.Error)
            Return False
        End If

        If Decimal.Parse(SDM_Data) > get_PercenSDM Then
            ShowMessage(" Please Fill In Self Development Mark Less Than " & get_PercenSDM, MessageType.Error)
            Return False
        End If

        If RTM_Data.Length = 0 Then
            ShowMessage(" Please Fill In Room Tidiness Mark ", MessageType.Error)
            Return False
        End If

        If RTM_Data > get_PercenRTM Then
            ShowMessage(" Please Fill In Room Tidiness Mark Less Than " & get_PercenRTM, MessageType.Error)
            Return False
        End If

        If APM_Data.Length = 0 Then
            ShowMessage(" Please Fill In Appearance Mark ", MessageType.Error)
            Return False
        End If

        If APM_Data > get_PercenAPM Then
            ShowMessage(" Please Fill In Appearance Mark Less Than " & get_PercenAPM, MessageType.Error)
            Return False
        End If

        If MM_Data.Length = 0 Then
            ShowMessage(" Please Fill In Merit Mark ", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class