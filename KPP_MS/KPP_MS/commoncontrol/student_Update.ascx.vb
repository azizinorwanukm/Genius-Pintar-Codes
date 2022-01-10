Imports System.Data.SqlClient

Public Class student_Update
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                student_sem_list()
                student_level_list()
                year_list()
                student_campus_list()
                student_program_list()

                ddlclass.Enabled = False

                load_page()

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnTransferClass.Visible = False

        Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & Request.QueryString("admin_ID") & "'"
        Dim stf_ID_Data As String = oCommon.getFieldValue(accessID)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''Get Login ID from Staff_Login
        strSQL = "Select login_ID from staff_Login where stf_ID = '" & stf_ID_Data & "' and staff_Access = '" & str_user_position & "'"
        Dim find_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Get Count from Menu_master_User
        strSQL = "select count(*) Count_No from menu_master_user where stf_ID = '" & stf_ID_Data & "' and login_ID = '" & find_LoginID & "'"
        Dim find_CountNo_LoginID As String = oCommon.getFieldValue(strSQL)

        ''Loop The Count_No
        For num As Integer = 0 To find_CountNo_LoginID - 1 Step 1

            ''Get Main Menu Data
            strSQL = "  Select A.Menu From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_Menu_Data As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Transfer Data
            strSQL = "  Select B.F1_Transfer From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Transfer As String = oCommon.getFieldValue(strSQL)

            If find_Data_Menu_Data = "Student Management" And find_Data_Menu_Data.Length > 0 Then

                If find_Data_F1Transfer.Length > 0 And find_Data_F1Transfer = "TRUE" Then
                    btnTransferClass.Visible = True
                End If
            End If

            If find_Data_Menu_Data = "All" Then
                btnTransferClass.Visible = True
            End If

        Next

    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter from setting where Type = 'Year' and Parameter = '" & Now.Year & "'"

        '--debug
        ''Response.Write(strSQLstd)

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddl_Year.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_Year.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub year_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT Parameter FROM setting WHERE Type = 'Year' and Parameter is not null "
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlyear.SelectedIndex = 0

            ddl_Year.DataSource = ds
            ddl_Year.DataTextField = "Parameter"
            ddl_Year.DataValueField = "Parameter"
            ddl_Year.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & ddlyear.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlstudentLevel.SelectedValue & "' and course_Program = '" & ddlstudentprogram.SelectedValue & "' and class_Campus = '" & ddl_Campus.SelectedValue & "' order by class_Name ASC"

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlclass.DataSource = ds
            ddlclass.DataTextField = "class_Name"
            ddlclass.DataValueField = "class_ID"
            ddlclass.DataBind()
            ddlclass.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlclass.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_name_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & ddl_Year.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddl_Level.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "' and class_Campus = '" & ddl_Campus.SelectedValue & "' order by class_Name ASC"

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Class.DataSource = ds
            ddl_Class.DataTextField = "class_Name"
            ddl_Class.DataValueField = "class_ID"
            ddl_Class.DataBind()
            ddl_Class.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddl_Class.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_sem_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT * FROM setting WHERE Type = 'Sem'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstudentSem.DataSource = ds
            ddlstudentSem.DataTextField = "Parameter"
            ddlstudentSem.DataValueField = "Value"
            ddlstudentSem.DataBind()
            ddlstudentSem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlstudentSem.SelectedIndex = 0

            ddl_Sem.DataSource = ds
            ddl_Sem.DataTextField = "Parameter"
            ddl_Sem.DataValueField = "Value"
            ddl_Sem.DataBind()
            ddl_Sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddl_Sem.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_level_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT Parameter FROM setting WHERE Type = 'Level' "
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstudentLevel.DataSource = ds
            ddlstudentLevel.DataTextField = "Parameter"
            ddlstudentLevel.DataValueField = "Parameter"
            ddlstudentLevel.DataBind()
            ddlstudentLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlstudentLevel.SelectedIndex = 0

            ddl_Level.DataSource = ds
            ddl_Level.DataTextField = "Parameter"
            ddl_Level.DataValueField = "Parameter"
            ddl_Level.DataBind()
            ddl_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_Level.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_campus_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
        End If

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Campus.DataSource = ds
            ddl_Campus.DataTextField = "Parameter"
            ddl_Campus.DataValueField = "Value"
            ddl_Campus.DataBind()
            ddl_Campus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
            ddl_Campus.SelectedIndex = 0
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_program_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        If ddl_Campus.SelectedValue = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
        End If

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Program.DataSource = ds
            ddl_Program.DataTextField = "Parameter"
            ddl_Program.DataValueField = "Value"
            ddl_Program.DataBind()
            ddl_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddl_Program.SelectedIndex = 0

            ddlstudentprogram.DataSource = ds
            ddlstudentprogram.DataTextField = "Parameter"
            ddlstudentprogram.DataValueField = "Value"
            ddlstudentprogram.DataBind()
            ddlstudentprogram.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddlstudentprogram.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlSem_Changed(sender As Object, e As EventArgs) Handles ddl_Sem.SelectedIndexChanged
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select distinct student_info.std_ID, student_Name, student_info.student_ID, student_Campus, student_Level.student_Level, student_Level.student_Sem, class_info.class_Name, student_Level.year
                  FROM student_info 
                  left join student_Level on student_info.std_ID=student_Level.std_ID
                  left join course on student_info.std_ID = course.std_ID
                  left join class_info on course.class_Id = class_info.class_ID"
        strWhere += " WHERE student_info.std_ID IS NOT NULL and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')"
        strWhere += " and student_Level.year = '" & ddl_Year.SelectedValue & "' and course.year = '" & ddl_Year.SelectedValue & "' and class_info.class_type = 'Compulsory' and student_Stream = '" & ddl_Program.SelectedValue & "'"

        If Session("SchoolCampus") = "APP" Then
            strWhere += " and student_info.student_Campus = '" & Session("SchoolCampus") & "'"
        End If

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " and student_Level.student_Level = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_Sem.SelectedIndex > 0 Then
            strWhere += " and student_Level.student_Sem = '" & ddl_Sem.SelectedValue & "'"
        End If

        If ddl_Class.SelectedIndex > 0 Then
            strWhere += " and class_info.class_ID = '" & ddl_Class.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Private Sub btnTransferClass_ServerClick(sender As Object, e As EventArgs) Handles btnTransferClass.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer

        Dim find_religion As String = ""
        Dim get_religion As String = ""

        If graduatedStudent.Checked = True Then
            If Checking_TransferStudent_CB() = False Then
                Exit Sub
            End If
        ElseIf graduatedStudent.Checked = False Then
            If Checking_TransferStudent() = False Then
                Exit Sub
            End If
        End If

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then
                    If graduatedStudent.Checked = False Then

                        If ddlstudentSem.SelectedValue = "Sem 1" Then

                            If ddl_Campus.SelectedValue <> "APP" Then

                                ''register name for hostel and room
                                Using PJGDATA As New SqlCommand("INSERT into student_room(std_ID,sem,year) 
                                                                values ('" & strKey & "','" & ddlstudentSem.SelectedValue & "','" & ddlyear.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                    If j <> 0 Then
                                        errorCount = 1
                                    Else
                                        errorCount = 2
                                    End If
                                End Using

                            End If

                            Using PJGDATA As New SqlCommand("INSERT into student_Level(std_ID,student_Sem,student_Level,year,month,day,Registered) 
                                                             values ('" & strKey & "','" & ddlstudentSem.SelectedValue & "','" & ddlstudentLevel.SelectedValue & "','" & ddlyear.SelectedValue & "','" & Now.Month & "','" & Now.Day & "', 'No')", objConn)
                                objConn.Open()
                                Dim j = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                                If j <> 0 Then
                                    errorCount = 1
                                Else
                                    errorCount = 2
                                End If
                            End Using

                            ''register all subject including religion subject (PENDIDIKAN ISLAM / PENDIDIKAN MORAL)
                            find_religion = "select student_religion from student_info where std_ID = '" & strKey & "'"
                            get_religion = oCommon.getFieldValue(find_religion)

                            Dim strsubj As String = "select subject_id from subject_info where"

                            If get_religion = "ISLAM" Then
                                ''select subject compulsory only for semester 1
                                strsubj += " subject_type = 'Compulsory'"
                                strsubj += " And subject_year = '" & ddlyear.SelectedValue & "'"
                                strsubj += " And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'"
                                strsubj += " And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                                strsubj += " And subject_religions <> 'OTHERS'"
                                strsubj += " And course_Program = '" & ddlstudentprogram.SelectedValue & "'"
                                strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                            Else
                                ''select subject compulsory only for semester 1
                                strsubj += " subject_type = 'Compulsory'"
                                strsubj += " And subject_year = '" & ddlyear.SelectedValue & "'"
                                strsubj += " And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'"
                                strsubj += " And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                                strsubj += " And subject_religions <> 'ISLAM'"
                                strsubj += " And course_Program = '" & ddlstudentprogram.SelectedValue & "'"
                                strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                            End If

                            Dim strsubjDA As New SqlDataAdapter(strsubj, objConn)

                            Dim subjds As DataSet = New DataSet
                            strsubjDA.Fill(subjds, "SubjTable")

                            For idx As Integer = 0 To subjds.Tables(0).Rows.Count - 1
                                Dim subj As String = subjds.Tables(0).Rows(idx).Item("subject_ID")
                                Dim strAdd As String = "INSERT INTO course(std_ID,class_ID,subject_ID,year) VALUES('" & strKey & "','" & ddlclass.SelectedValue & "' , '" & subj & "', '" & ddlyear.SelectedValue & "' )"

                                Using STDDATA As New SqlCommand(strAdd, objConn)
                                    objConn.Open()
                                    Dim run = STDDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using
                            Next

                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                            Dim level As String = ""

                            If ddlstudentLevel.SelectedValue = "Foundation 1" Or ddlstudentLevel.SelectedValue = "Foundation 2" Or ddlstudentLevel.SelectedValue = "Foundation 3" Then
                                level = "ASAS 1"
                            ElseIf ddlstudentLevel.SelectedValue = "Level 1" Or ddlstudentLevel.SelectedValue = "Level 2" Then
                                level = "TAHAP 1"
                            End If

                            Dim find_Mykad As String = "select student_Mykad from student_info where std_ID = '" & strKey & "'"
                            Dim get_Mykad As String = oCommon.getFieldValue(find_Mykad)

                            Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & get_Mykad & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                            Dim find_PPCSDate As String = "select PPCSDate from PPCS where StudentID = '" & get_StudentID & "'"
                            Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                            Dim find_kolejKelas As String = "select class_Name from class_info where class_ID = '" & ddlclass.SelectedValue & "'"
                            Dim get_kolejKelas As String = oCommon.getFieldValue(find_kolejKelas)

                            Dim find_kokoKelas As String = "select KelasID from koko_kelas where Kelas = '" & get_kolejKelas & "' and Tahun = '" & ddlyear.SelectedValue & "'"
                            Dim get_kokoKelas As String = oCommon.getFieldValue_Permata(find_kokoKelas)

                            strSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan, KelasID) 
                                  values('" & get_StudentID & "','" & get_PPCSDate & "','" & ddlyear.SelectedValue & "','" & level & "','N','" & get_kokoKelas & "')"
                            strRet = oCommon.ExecuteSQLPermata(strSQL)

                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        ElseIf ddlstudentSem.SelectedValue = "Sem 2" Then

                            If ddl_Campus.SelectedValue <> "APP" Then

                                ''register name for hostel and room
                                Using PJGDATA As New SqlCommand("   INSERT into student_room(std_ID,sem,year) 
                                                                    values ('" & strKey & "','" & ddlstudentSem.SelectedValue & "','" & ddlyear.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                    If j <> 0 Then
                                        errorCount = 1
                                    Else
                                        errorCount = 2
                                    End If
                                End Using

                            End If

                            Using PJGDATA As New SqlCommand("   INSERT into student_Level(std_ID,student_Sem,student_Level,year,month,day,Registered) 
                                                                values ('" & strKey & "','" & ddlstudentSem.SelectedValue & "','" & ddlstudentLevel.SelectedValue & "','" & ddlyear.SelectedValue & "','" & Now.Month & "','" & Now.Day & "','Yes')", objConn)
                                objConn.Open()
                                Dim j = PJGDATA.ExecuteNonQuery()
                                objConn.Close()
                                If j <> 0 Then
                                    errorCount = 1
                                Else
                                    errorCount = 2
                                End If
                            End Using

                            find_religion = "select student_religion from student_info where std_ID = '" & strKey & "'"
                            get_religion = oCommon.getFieldValue(find_religion)

                            Dim strsubj As String = "select subject_id from subject_info where"

                            If get_religion = "Islam" Then
                                ''select subject compulsory only for semester 2
                                strsubj += " subject_type = 'Compulsory'"
                                strsubj += " And subject_year = '" & ddlyear.SelectedValue & "'"
                                strsubj += " And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'"
                                strsubj += " And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                                strsubj += " And subject_religions <> 'OTHERS'"
                                strsubj += " And course_Program = '" & ddlstudentprogram.SelectedValue & "'"
                                strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                            Else
                                ''select subject compulsory only for semester 2
                                strsubj += " subject_type = 'Compulsory'"
                                strsubj += " And subject_year = '" & ddlyear.SelectedValue & "'"
                                strsubj += " And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'"
                                strsubj += " And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                                strsubj += " And subject_religions <> 'ISLAM'"
                                strsubj += " And course_Program = '" & ddlstudentprogram.SelectedValue & "'"
                                strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                            End If

                            Dim strsubjDA As New SqlDataAdapter(strsubj, objConn)

                            Dim subjds As DataSet = New DataSet
                            strsubjDA.Fill(subjds, "SubjTable")

                            ''get class id for class compulsory
                            Dim get_ClassCompulsory As String = "   select distinct course.class_ID from course left join class_info on course.class_ID = class_info.class_ID
                                                                    where course.year =  '" & ddl_Year.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddl_Level.SelectedValue & "' and class_Campus = '" & ddl_Campus.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "' and course.std_ID = '" & strKey & "'"
                            Dim data_ClassCompulsory As String = oCommon.getFieldValue(get_ClassCompulsory)

                            For idx As Integer = 0 To subjds.Tables(0).Rows.Count - 1
                                Dim subj As String = subjds.Tables(0).Rows(idx).Item("subject_ID")

                                ''insert into course table for subject compulsory only
                                Using STDDATA As New SqlCommand("INSERT INTO course(std_ID,class_ID,subject_ID,year) VALUES('" & strKey & "','" & data_ClassCompulsory & "' , '" & subj & "', '" & ddlyear.SelectedValue & "' )", objConn)
                                    objConn.Open()
                                    Dim run = STDDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using
                            Next

                            ''select subject electives for semester 1 that student taken
                            Dim strsubH As String = "select subject_info.course_Name from subject_info left join course on subject_info.subject_ID = course.subject_ID where"
                            strsubH += " subject_type = 'Electives'"
                            strsubH += " And subject_year = '" & ddl_Year.SelectedValue & "'"
                            strsubH += " And subject_StudentYear = '" & ddl_Level.SelectedValue & "'"
                            strsubH += " And subject_sem = '" & ddl_Sem.SelectedValue & "'"
                            strsubH += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                            strsubH += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                            strsubH += " And std_ID = '" & strKey & "'"
                            Dim strsubjDB As New SqlDataAdapter(strsubH, objConn)

                            Dim subjdr As DataSet = New DataSet
                            strsubjDB.Fill(subjdr, "SubjTable")

                            ''loop for each subject id to get the same subject Name but for semester 2
                            For idx As Integer = 0 To subjdr.Tables(0).Rows.Count - 1
                                Dim subj As String = subjdr.Tables(0).Rows(idx).Item("course_Name")

                                ''get the subject id for semester 2
                                Dim get_NonCompulsory_Sem2 As String = "Select subject_ID from subject_info where course_Name = '" & subj & "' And subject_year = '" & ddlyear.SelectedValue & "' And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'  And subject_sem = '" & ddlstudentSem.SelectedValue & "' and subject_Campus = '" & ddl_Campus.SelectedValue & "' And course_Program = '" & ddlstudentprogram.SelectedValue & "'"
                                Dim data_NonCompulsory_Sem2 As String = oCommon.getFieldValue(get_NonCompulsory_Sem2)

                                ''get the class id for semseter 2 for that new subject id
                                Dim get_ClassID_Sem2 As String = "Select class_ID from class_Info where subject_ID = '" & data_NonCompulsory_Sem2 & "' And class_year = '" & ddlyear.SelectedValue & "' And class_sem = '" & ddlstudentSem.SelectedValue & "' and class_Campus = '" & ddl_Campus.SelectedValue & "' And course_Program = '" & ddlstudentprogram.SelectedValue & "'"
                                Dim data_ClassD_Sem2 As String = oCommon.getFieldValue(get_ClassID_Sem2)

                                ''insert into course table for subject electives only
                                Dim strAdd As String = "INSERT INTO course(std_ID,class_ID,subject_ID,year) VALUES('" & strKey & "','" & data_ClassD_Sem2 & "' , '" & data_NonCompulsory_Sem2 & "', '" & ddlyear.SelectedValue & "' )"

                                Using STDDATA As New SqlCommand(strAdd, objConn)
                                    objConn.Open()
                                    Dim run = STDDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using
                            Next

                            ''select subject choose for semester 1 that student taken
                            Dim pastChoose As String = "select subject_Name from subject_info left join course on subject_info.subject_ID = course.subject_ID
                                                        where subject_type = 'Choose' and course_Name = 'Bahasa Antarabangsa' and subject_year = '" & ddl_Year.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "'
                                                        And subject_StudentYear = '" & ddl_Level.SelectedValue & "' And subject_sem = '" & ddl_Sem.SelectedValue & "' and subject_Campus = '" & ddl_Campus.SelectedValue & "' And std_ID = '" & strKey & "'"
                            Dim getPastChoose As String = oCommon.getFieldValue(pastChoose)

                            ''get the subject id for semester 2
                            Dim get_choose_Sem2 As String = "Select subject_ID from subject_info where subject_Name = '" & getPastChoose & "' And subject_year = '" & ddlyear.SelectedValue & "' and subject_Campus = '" & ddl_Campus.SelectedValue & "' And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'  And subject_sem = '" & ddlstudentSem.SelectedValue & "' and subject_type = 'Choose' And course_Program = '" & ddlstudentprogram.SelectedValue & "'"
                            Dim data_choose_Sem2 As String = oCommon.getFieldValue(get_choose_Sem2)

                            ''get the class id for semseter 2 for that new subject id
                            Dim get_ClassIDChoose_Sem2 As String = "Select class_ID from class_Info where subject_ID = '" & data_choose_Sem2 & "' and class_Campus = '" & ddl_Campus.SelectedValue & "' And class_year = '" & ddlyear.SelectedValue & "' And class_sem = '" & ddlstudentSem.SelectedValue & "' And course_Program = '" & ddlstudentprogram.SelectedValue & "'"
                            Dim data_ClassDChoose_Sem2 As String = oCommon.getFieldValue(get_ClassIDChoose_Sem2)

                            ''insert into course table for subject choose only
                            Dim strAddChoose As String = "INSERT INTO course(std_ID,class_ID,subject_ID,year) VALUES('" & strKey & "','" & data_ClassDChoose_Sem2 & "' , '" & data_choose_Sem2 & "', '" & ddlyear.SelectedValue & "' )"

                            Using STDDATA As New SqlCommand(strAddChoose, objConn)
                                objConn.Open()
                                Dim run = STDDATA.ExecuteNonQuery()
                                objConn.Close()
                            End Using
                        End If

                    Else
                        errorCount = 2
                    End If

                ElseIf graduatedStudent.Checked = True Then
                    Dim x As Integer = 0
                    Dim value As String = ""

                    For x = 0 To datRespondent.Rows.Count - 1 Step x + 1
                        Dim chkUpdatecheck As CheckBox = CType(datRespondent.Rows(x).Cells(0).FindControl("chkSelect"), CheckBox)
                        If Not chkUpdatecheck Is Nothing Then
                            Dim strID As String = datRespondent.DataKeys(x).Value.ToString
                            If chkUpdatecheck.Checked = True Then

                                Using PJGDATA As New SqlCommand("UPDATE student_info SET student_Status='Graduate', student_Password = student_Mykad student_ where std_ID = '" & strID & "'", objConn)
                                    objConn.Open()
                                    Dim j = PJGDATA.ExecuteNonQuery()
                                    objConn.Close()
                                    If j <> 0 Then
                                        errorCount = 1
                                    Else
                                        errorCount = 2
                                    End If
                                End Using
                            End If
                        End If
                    Next
                End If
            End If
            '--execute SQL
        Next

        If errorCount = 2 Then
            ShowMessage(" Transfer Student", MessageType.Error)
        ElseIf errorCount = 1 Then
            ShowMessage(" Transfer Student", MessageType.Success)
        End If

        strRet = BindData(datRespondent)
    End Sub

    Private Function Checking_TransferStudent() As Boolean

        If ddl_Year.SelectedIndex = 0 Then
            ShowMessage("Please Select Year", MessageType.Error)
            Return False
        End If

        If ddl_Campus.SelectedIndex = 0 Then
            ShowMessage("Please Select Institutions", MessageType.Error)
            Return False
        End If

        If ddl_Program.SelectedIndex = 0 Then
            ShowMessage("Please Select Program", MessageType.Error)
            Return False
        End If

        If ddl_Level.SelectedIndex = 0 Then
            ShowMessage("Please Select Level", MessageType.Error)
            Return False
        End If

        If ddl_Sem.SelectedIndex = 0 Then
            ShowMessage("Please Select Semester", MessageType.Error)
            Return False
        End If

        If ddl_Class.SelectedIndex = 0 Then
            ShowMessage("Please Select Cass", MessageType.Error)
            Return False
        End If

        If ddlyear.SelectedIndex = 0 Then
            ShowMessage("Please Select Transfer Year", MessageType.Error)
            Return False
        End If

        If ddlstudentprogram.SelectedIndex = 0 Then
            ShowMessage("Please Select Transfer Program", MessageType.Error)
            Return False
        End If

        If ddlstudentLevel.SelectedIndex = 0 Then
            ShowMessage("Please Select Transfer Level", MessageType.Error)
            Return False
        End If

        If ddlstudentSem.SelectedIndex = 0 Then
            ShowMessage("Please Select Transfer Semester", MessageType.Error)
            Return False
        End If

        If ddlclass.SelectedIndex = 0 Then
            ShowMessage("Please Select Transfer Class", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Function Checking_TransferStudent_CB() As Boolean

        If ddl_Year.SelectedIndex = 0 Then
            ShowMessage("Please Select Year", MessageType.Error)
            Return False
        End If

        If ddl_Campus.SelectedIndex = 0 Then
            ShowMessage("Please Select Institutions", MessageType.Error)
            Return False
        End If

        If ddl_Program.SelectedIndex = 0 Then
            ShowMessage("Please Select Program", MessageType.Error)
            Return False
        End If

        If ddl_Level.SelectedIndex = 0 Then
            ShowMessage("Please Select Level", MessageType.Error)
            Return False
        End If

        If ddl_Sem.SelectedIndex = 0 Then
            ShowMessage("Please Select Semester", MessageType.Error)
            Return False
        End If

        If ddl_Class.SelectedIndex = 0 Then
            ShowMessage("Please Select Cass", MessageType.Error)
            Return False
        End If

        If ddlyear.SelectedIndex = 0 Then
            ShowMessage("Please Select Transfer Year", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Protected Sub ddlstudentSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlstudentSem.SelectedIndexChanged
        Try

            If ddlstudentSem.SelectedValue = "Sem 1" Then
                ddlclass.Enabled = True
                class_list()
            Else
                ddlclass.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlstudentprogram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlstudentprogram.SelectedIndexChanged
        Try
            class_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Class.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            class_name_list()

            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Program.SelectedIndexChanged
        Try
            class_name_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Campus.SelectedIndexChanged
        Try
            student_program_list()
            class_name_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub graduatedStudent_CheckedChanged(sender As Object, e As EventArgs) Handles graduatedStudent.CheckedChanged

        If graduatedStudent.Checked = True Then
            ddlstudentprogram.Enabled = False
            ddlstudentLevel.Enabled = False
            ddlstudentSem.Enabled = False
            ddlclass.Enabled = False

        ElseIf graduatedStudent.Checked = False Then
            ddlstudentprogram.Enabled = True
            ddlstudentLevel.Enabled = True
            ddlstudentSem.Enabled = True
            ddlclass.Enabled = True
        End If

    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class