Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing

'Public Class Disiplin_Update
'    Inherits System.Web.UI.UserControl

'    Dim strSQL As String = ""
'    Dim strRet As String = ""
'    Dim result As Integer = 0

'    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
'    Dim objConn As SqlConnection = New SqlConnection(strConn)
'    Dim oCommon As New Commonfunction
'    Dim oDes As New Simple3Des("p@ssw0rd1")

'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Try
'            If Not IsPostBack Then
'                result = Request.QueryString("result")

'                Dim id As String = ""
'                id = Request.QueryString("admin_ID")
'                dicipline()
'                CurrentDate.Text = Date.Now.ToString("dd MMMM yyyy")

'            End If
'        Catch ex As Exception
'        End Try
'    End Sub

'    Protected Sub dicipline()

'        strSQL = "select case_name from case_info "

'        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
'        Dim objConn As SqlConnection = New SqlConnection(strConn)
'        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

'        Try
'            Dim ds As DataSet = New DataSet
'            sqlDA.Fill(ds, "AnyTable")


'            ddlDiciplinetype.DataSource = ds
'            ddlDiciplinetype.DataTextField = "case_name"
'            ddlDiciplinetype.DataValueField = "case_name"
'            ddlDiciplinetype.DataBind()
'            ddlDiciplinetype.Items.Insert(0, New ListItem("-- Pick A Case --", String.Empty))
'            ddlDiciplinetype.SelectedIndex = 0

'        Catch ex As Exception

'        End Try


'    End Sub

'    Protected Sub disiplin_onselectedindexchanged(sender As Object, e As EventArgs) Handles ddlDiciplinetype.SelectedIndexChanged

'        Dim display As String = "block"
'        HiddenField2.Value = display

'    End Sub

'    Private Sub searchComplainant_serverclick(sender As Object, e As EventArgs) Handles searchComplainant.ServerClick
'        Dim display As String = "block"
'        HiddenField3.Value = display

'        strSQL = "select staff_name,staff_id from staff_info where staff_id = '" & Pelapor_id.Text & "' "
'        Dim strconn As String = ConfigurationManager.AppSettings("connectionstring")
'        Dim objconn As SqlConnection = New SqlConnection(strconn)
'        Dim sqlDa As New SqlDataAdapter(strSQL, objconn)

'        Try
'            Dim ds As DataSet = New DataSet
'            sqlDa.Fill(ds)

'            If ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
'                Person_charge.Text = ""

'            ElseIf Pelapor_id.Text = ds.Tables(0).Rows(0)("staff_id").ToString() Then
'                Person_charge.Text = ds.Tables(0).Rows(0)("staff_name").ToString()


'            End If

'        Catch ex As Exception

'        End Try

'    End Sub



'    Private Sub btn_search_serverclick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
'        Dim display As String = "block"
'        HiddenField1.Value = display

'        strSQL = "select class_info.class_Name, student_info.student_Name, student_info.student_Mykad, student_info.student_ID from student_info
'            left join course on course.std_id = student_info.std_id
'            left join class_info on class_info.class_id = course.class_id
'            where student_info.student_id = '" & student_Mykad.Text & "' or student_info.student_mykad = '" & student_Mykad.Text & "' and class_info.class_type = 'Compulsory'"
'        Dim strConn As String = ConfigurationManager.AppSettings("connectionString")
'        Dim objConn As SqlConnection = New SqlConnection(strConn)
'        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

'        Try
'            Dim ds As DataSet = New DataSet
'            sqlDA.Fill(ds)

'            If ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
'                Response.Redirect("admin_edit_disiplin.aspx?result=8&admin_ID=" + Request.QueryString("admin_ID"))
'                Student_name.Text = ""
'                Student_class.Text = ""
'                student_Mykad.Text = ""


'            ElseIf student_Mykad.Text = ds.Tables(0).Rows(0)("student_id").ToString() OrElse student_Mykad.Text = ds.Tables(0).Rows(0)("student_mykad") Then
'                Hidden_IC.Text = ds.Tables(0).Rows(0)("student_mykad").ToString()
'                student_id.Text = ds.Tables(0).Rows(0)("student_id").ToString()
'                Student_name.Text = ds.Tables(0).Rows(0)("student_name").ToString()
'                Student_class.Text = ds.Tables(0).Rows(0)("class_name").ToString()


'            End If
'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub Btnsimpan_serverClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
'        Dim errorCount As Integer = 0S

'        ''get student ID
'        Dim get_StdID As String = "select student_info.std_ID from student_info left join student_level on student_info.std_ID = student_level.std_ID
'                                    where student_level.year = '" & Now.Year & "' and (student_info.student_ID = '" & student_Mykad.Text & "' or student_info.student_Mykad = '" & student_Mykad.Text & "')"
'        Dim data_StdID As String = oCommon.getFieldValue(get_StdID)

'        ''get class ID
'        Dim get_ClassID As String = "select class_ID from class_info where class_Name = '" & Student_class.Text & "' and class_year = '" & Now.Year & "'"
'        Dim data_ClassID As String = oCommon.getFieldValue(get_ClassID)

'        ''get staff ID
'        Dim get_StfID As String = "select stf_ID from staff_Info where staff_ID = '" & Pelapor_id.Text & "' and staff_year = '" & Now.Year & "'"
'        Dim data_StfID As String = oCommon.getFieldValue(get_StfID)

'        ''get case ID
'        Dim get_CaseID As String = "Select case_ID from case_info where case_Name = '" & ddlDiciplinetype.SelectedValue & "'"
'        Dim data_CaseID As String = oCommon.getFieldValue(get_CaseID)

'        If ddlDiciplinetype.SelectedIndex > 0 Then
'            If Student_name.Text <> "" And Not IsNumeric(Student_name.Text) Then
'                If student_id.Text <> "" And Regex.IsMatch(student_id.Text, "^[A-Za-z0-9]+$") Then
'                    If Person_charge.Text <> "" And Not IsNumeric(Person_charge.Text) And Not IsNothing(Person_charge.Text) And Regex.IsMatch(Person_charge.Text, "^[A-Za-z ]+$") Then
'                        If Pelapor_id.Text <> "" Then

'                            Using STDDATA As New SqlCommand("INSERT INTO dicipline_info(std_ID,class_ID,case_ID,stf_ID,Detail_Case,Dicipline_Action,Dicipline_Date) values
'                                ('" & data_StdID & "','" & data_ClassID & "','" & data_CaseID & "','" & data_StfID & "','" & Detail_case.Text & "','" & Action_box.Text & "','" & CurrentDate.Text & "')", objConn)
'                                objConn.Open()
'                                Dim i = STDDATA.ExecuteNonQuery()
'                                objConn.Close()
'                                If i <> 0 Then
'                                    errorCount = 0 ''success
'                                Else
'                                    errorCount = 1 ''

'                                End If
'                            End Using
'                        Else
'                            errorCount = 2 ''id pelapor
'                        End If
'                    Else
'                        errorCount = 3 ''nama person in charge
'                    End If
'                Else
'                    errorCount = 4 ''student id
'                End If
'            Else
'                errorCount = 5 ''student name
'            End If
'        Else
'            errorCount = 6 ''student mykad
'        End If


'        If errorCount = 0 Then
'            Response.Redirect("admin_edit_disiplin.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
'        ElseIf errorCount = 1 Then
'            Response.Redirect("admin_edit_disiplin.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
'        ElseIf errorCount = 2 Then
'            Response.Redirect("admin_edit_disiplin.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
'        ElseIf errorCount = 3 Then
'            Response.Redirect("admin_edit_disiplin.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
'        ElseIf errorCount = 4 Then
'            Response.Redirect("admin_edit_disiplin.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
'        ElseIf errorCount = 5 Then
'            Response.Redirect("admin_edit_disiplin.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
'        ElseIf errorCount = 6 Then
'            Response.Redirect("admin_edit_disiplin.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))


'        End If

'    End Sub

'    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
'        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
'    End Sub


'End Class

Public Class Disiplin_Update
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim staffSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim data_StdID As String
    Dim data_StdName As String
    Dim data_className As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                result = Request.QueryString("result")

                Dim id As String = ""
                id = Request.QueryString("admin_ID")
                dicipline()
                action_info()

                'StaffDDL()
                CurrentDate.Text = Date.Now.ToString("dd MMMM yyyy")
                'setReporterDDLToCurrUser()

                CurrentDate.Text = Date.Now.ToString("dd MMMM yyyy")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dicipline()

        strSQL = "select case_name,case_ID from case_info "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")


            ddlDiciplinetype.DataSource = ds
            ddlDiciplinetype.DataTextField = "case_name"
            ddlDiciplinetype.DataValueField = "case_ID"
            ddlDiciplinetype.DataBind()
            ddlDiciplinetype.Items.Insert(0, New ListItem("-- Pick A Case --", String.Empty))
            ddlDiciplinetype.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CounselingStaffDDl()
        Dim query As String = "SELECT distinct staff_Info.staff_Name, staff_Info.stf_ID 
                               FROM staff_Info 
                               LEFT JOIN staff_Login ON staff_Info.stf_ID = staff_Login.stf_ID 
                               WHERE staff_Login.staff_Status = 'Access' ORDER BY staff_Name ASC"
        Dim sqlDA As New SqlDataAdapter(query, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCounselingStaff.DataSource = ds
            ddlCounselingStaff.DataTextField = "staff_Name"
            ddlCounselingStaff.DataValueField = "stf_ID"
            ddlCounselingStaff.DataBind()
            ddlCounselingStaff.Items.Insert(0, New ListItem("-- Select Staff In Charge --", String.Empty))
            ddlCounselingStaff.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub action_info()
        Dim query As String = "select * from warning_letters_table"
        Dim sqlDA As New SqlDataAdapter(query, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlActionType.DataSource = ds
            ddlActionType.DataTextField = "title"
            ddlActionType.DataValueField = "id"
            ddlActionType.DataBind()
            ddlActionType.Items.Insert(0, New ListItem("-- Select= Action --", String.Empty))
            ddlActionType.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_search_serverclick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick

        If student_Mykad.Text.Length > 0 Then

            Try
                'Get student name
                Dim query_stdName As String = "select student_info.student_Name from student_info
                                                 left join course on course.std_id = student_info.std_id
                                                 left join class_info on class_info.class_id = course.class_id
                                                 where (student_info.student_id = '" & student_Mykad.Text & "'
                                                 or student_info.student_mykad = '" & student_Mykad.Text & "')
                                                 and class_info.class_type = 'Compulsory'"
                data_StdName = oCommon.getFieldValue(query_stdName)

                'Get class name
                Dim query_stdClass As String = "select class_info.class_name from student_info
                                                     left join course on course.std_id = student_info.std_id
                                                     left join class_info on class_info.class_id = course.class_id
                                                     where (student_info.student_id = '" & student_Mykad.Text & "'
                                                     or student_info.student_mykad = '" & student_Mykad.Text & "')
                                                     and class_info.class_type = 'Compulsory'"
                data_className = oCommon.getFieldValue(query_stdClass)

                strSQL = "select Student_ID from student_info  where (student_info.student_id = '" & student_Mykad.Text & "' or student_info.student_mykad = '" & student_Mykad.Text & "') and student_status = 'Access' "
                strRet = oCommon.getFieldValue(strSQL)

                If data_StdName.Length > 0 And data_className.Length > 0 Then
                    Dim display As String = "block"
                    HiddenField1.Value = display
                    StudentNameLbl.Text = data_StdName
                    StudentClassLbl.Text = data_className
                    StudentIDLbl.Text = strRet
                Else
                    Response.Redirect("admin_edit_disiplin.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
                End If

            Catch ex As Exception

            End Try
        Else
            Response.Redirect("admin_edit_disiplin.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
        End If

    End Sub

    Private Sub Btnsimpan_serverClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        'get student ID
        Dim get_StdID As String = " SELECT std_ID 
                                    FROM student_info 
                                    WHERE (student_ID = '" & student_Mykad.Text & "' OR student_Mykad = '" & student_Mykad.Text & "') AND student_Status = 'Access'"
        Dim data_StdID As String = oCommon.getFieldValue(get_StdID)

        'get class ID
        Dim get_ClassID As String = "select class_ID from class_info where class_Name = '" & StudentClassLbl.Text & "' and class_year = '" & Now.Year & "'"
        Dim data_ClassID As String = oCommon.getFieldValue(get_ClassID)

        'get case ID
        Dim get_CaseID As String = "Select case_ID from case_info where case_ID = '" & ddlDiciplinetype.SelectedValue & "'"
        Dim data_CaseID As String = oCommon.getFieldValue(get_CaseID)

        Dim get_StdName As String = "select student_Name from student_info where std_ID='" + data_StdID + "' and student_Status = 'Access'"
        Dim data_StdName As String = oCommon.getFieldValue(get_StdName)

        Dim get_point As String = "select case_MeritDemerit_Point from case_info where case_ID = '" & ddlDiciplinetype.SelectedValue & "'"
        Dim data_point As String = oCommon.getFieldValue(get_point)

        Dim newDispQuery As String = "INSERT INTO dicipline_info(std_ID, class_ID, case_ID, stf_ID, Detail_Case, warning_id, Dicipline_Date,meritdemerit_point ) VALUES ('" & data_StdID & "','" & data_ClassID & "','" & ddlDiciplinetype.SelectedValue & "','" & ddlCounselingStaff.SelectedValue & "','" & Detail_case.Text & "','" & ddlActionType.SelectedValue & "','" & CurrentDate.Text & "','" & data_point & "')"

        If ddlDiciplinetype.SelectedIndex > 0 Then
            If data_StdID.Length > 0 Then
                If CurrentDate.Text <> "" And IsDate(CurrentDate.Text) Then
                    If ddlActionType.SelectedIndex > 0 Then
                        If Detail_case.Text <> "" And Regex.IsMatch(Detail_case.Text, "^[0-9A-Za-z ]+$") Then
                            If needCounseling.Checked = True Then

                                Using STDDATA As New SqlCommand(newDispQuery, objConn)
                                    objConn.Open()
                                    Dim dispID = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If dispID <> 0 Then
                                        If ddlCounselingStaff.SelectedIndex > 0 Then
                                            If data_StdID <> "" Then
                                                If CounselingDate.Text <> "" And IsDate(CounselingDate.Text) Then

                                                    Dim captureDate As Date = CounselingDate.Text

                                                    If txtstart_time.Text.Length > 0 And txtend_time.Text.Length > 0 Then
                                                        If txtcode_session.Text.Length > 0 Then
                                                            If txtclient_classification.Text.Length > 0 Then
                                                                If txttype_interview.Text.Length > 0 Then

                                                                    Using KSLRDATA As New SqlCommand("INSERT INTO counseling_info (stf_ID,std_ID,disiplin_id,kslr_year,kslr_date,kslr_day,kslr_startTime,kslr_endTime,kslr_status,kslr_codesession,kslr_classificationClass,kslr_typeIV) 
                                                                                                      VALUES ('" & ddlCounselingStaff.SelectedValue & "','" & data_StdID & "','" & dispID & "','" & Now.Year & "','" & CounselingDate.Text & "','" & captureDate.DayOfWeek.ToString().ToUpper & "','" & txtstart_time.Text & "','" & txtend_time.Text & "','Uncompleted','" & txtcode_session.Text & "','" & txtclient_classification.Text & "','" & txttype_interview.Text & "')", objConn)
                                                                        objConn.Open()
                                                                        Dim i = KSLRDATA.ExecuteNonQuery()
                                                                        objConn.Close()

                                                                        If i <> 0 Then
                                                                            errorCount = 0
                                                                        Else
                                                                            errorCount = 13 'error save counseling
                                                                        End If
                                                                    End Using

                                                                Else
                                                                    errorCount = 17 ''error txt type of interview
                                                                End If
                                                            Else
                                                                errorCount = 16 ''error txt client classification
                                                            End If
                                                        Else
                                                            errorCount = 15 ''error txt code session
                                                        End If
                                                    Else
                                                        errorCount = 14 ''error start and end time
                                                    End If
                                                Else
                                                    errorCount = 11 'counseling date error
                                                End If
                                            Else
                                                errorCount = 5 'not student found
                                            End If
                                        Else
                                            errorCount = 12 'counselor incharge not selected
                                        End If
                                    Else
                                        errorCount = 10 'error saving disiplin
                                    End If
                                End Using

                            Else

                                Using STDDATA As New SqlCommand(newDispQuery, objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()
                                    If i <> 0 Then
                                        errorCount = 0 'no error
                                    Else
                                        errorCount = 10 'cannot save disiplin
                                    End If
                                End Using
                            End If
                        Else
                            errorCount = 9 'Detail_case error
                        End If
                    Else
                        errorCount = 8 'Action_box error
                    End If
                Else
                    errorCount = 7 'CurrDate error
                End If
            Else
                errorCount = 5 'id student not exist
            End If
        Else
            errorCount = 4 'case type not selected
        End If

        Response.Redirect(String.Format("admin_edit_disiplin.aspx?result={0}&admin_ID={1}", errorCount, Request.QueryString("admin_ID")))
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub needCounseling_CheckedChanged(sender As Object, e As EventArgs) Handles needCounseling.CheckedChanged
        If needCounseling.Checked = True And showCounselingDiv.Visible = False Then
            showCounselingDiv.Visible = True
        ElseIf needCounseling.Checked = False And showCounselingDiv.Visible = True Then
            showCounselingDiv.Visible = False
        End If
        CounselingStaffDDl()
        CounselingDate.Text = Date.Today.ToString("dd MMMM yyyy")

    End Sub

    Protected Sub ddlActionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlActionType.SelectedIndexChanged
        Dim getDetail As String = "SELECT * FROM warning_letters_table WHERE id='" + ddlActionType.SelectedValue + "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(getDetail, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("letter_content")) Then
                    txtLetterContent.Content = Server.HtmlDecode(ds.Tables(0).Rows(0).Item("letter_content")).ToString
                Else
                    txtLetterContent.Content = Server.HtmlDecode("<b>Write content here</b>")
                End If

            End If
            objConn.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class