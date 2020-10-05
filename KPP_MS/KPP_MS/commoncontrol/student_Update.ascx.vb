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
                'student_status()
                student_sem_list()
                student_level_list()
                year_list()

                ddlclass.Enabled = False

                load_page()

            End If
        Catch ex As Exception

        End Try
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
            '' ddl_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ''ddl_Year.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT * FROM class_info WHERE class_year = '" & ddlyear.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlstudentLevel.SelectedValue & "'"
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

    'Private Sub student_status()
    '    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    '    Dim objConn As SqlConnection = New SqlConnection(strConn)

    '    strSQL = "SELECT * FROM setting WHERE Type = 'Status'"
    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Try
    '        Dim ds As DataSet = New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        ddlStatus.DataSource = ds
    '        ddlStatus.DataTextField = "Parameter"
    '        ddlStatus.DataValueField = "Value"
    '        ddlStatus.DataBind()
    '        ddlStatus.Items.Insert(0, New ListItem("Select Status", String.Empty))
    '        ddlStatus.SelectedIndex = 0


    '    Catch ex As Exception

    '    Finally
    '        objConn.Dispose()
    '    End Try
    'End Sub

    Private Sub class_name_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT * FROM class_info WHERE class_year = '" & ddl_Year.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddl_Level.SelectedValue & "'"
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
            ddlstudentSem.Items.Insert(0, New ListItem("Select Sem", String.Empty))
            ddlstudentSem.SelectedIndex = 0

            ddl_Sem.DataSource = ds
            ddl_Sem.DataTextField = "Parameter"
            ddl_Sem.DataValueField = "Value"
            ddl_Sem.DataBind()
            ddl_Sem.Items.Insert(0, New ListItem("Select Sem", String.Empty))
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

    Protected Sub ddlSem_Changed(sender As Object, e As EventArgs) Handles ddl_Sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
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

        tmpSQL = "select distinct student_info.std_ID, student_Name, student_info.student_ID, student_Mykad, student_Level.student_Level, student_Level.student_Sem, class_info.class_Name, student_Level.year
                  FROM student_info 
                  left join student_Level on student_info.std_ID=student_Level.std_ID
                  left join course on student_info.std_ID = course.std_ID
                  left join class_info on course.class_Id = class_info.class_ID"
        strWhere += " WHERE student_info.std_ID IS NOT NULL and student_info.student_Status != 'Block'"
        strWhere += " and student_Level.year = '" & ddl_Year.SelectedValue & "' and class_info.class_type = 'Compulsory'"

        If Not txtstudent_data.Text.Length = 0 Then
            strWhere += " AND (student_info.student_ID LIKE '%" & txtstudent_data.Text & "%'"
        End If

        If Not txtstudent_data.Text.Length = 0 Then
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent_data.Text & "%'"
        End If

        If Not txtstudent_data.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent_data.Text & "%')"
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

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            strRet = "delete course where std_ID = '" & strKeyName & "' and year = '" & ddl_Year.SelectedValue & "'"
            strSQL = oCommon.ExecuteSQL(strRet)

            strRet = "delete student_level where std_ID = '" & strKeyName & "' and year = '" & ddl_Year.SelectedValue & "' and student_Level = '" & ddl_Level.SelectedValue & "' and student_Sem = '" & ddl_Sem.SelectedValue & "'"
            strSQL = oCommon.ExecuteSQL(strRet)

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer

        Dim get_exist As String = ""
        Dim data_exist As String = ""
        Dim find_religion As String = ""
        Dim get_religion As String = ""

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then
                    If graduatedStudent.Checked = False Then

                        If ddlstudentSem.SelectedValue = "Sem 1" Then

                            get_exist = "select std_ID from student_Level where std_ID = '" & strKey & "' and student_sem = '" & ddlstudentSem.SelectedValue & "'"

                            Using PJGDATA As New SqlCommand("INSERT into student_Level(std_ID,student_Sem,student_Level,year,month,day) 
                                                         values ('" & strKey & "','" & ddlstudentSem.SelectedValue & "','" & ddlstudentLevel.SelectedValue & "','" & ddlyear.SelectedValue & "','" & Now.Month & "','" & Now.Day & "')", objConn)
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
                                ''select subject compulsory only for semester 1
                                strsubj += " subject_type = 'Compulsory'"
                                strsubj += " And subject_year = '" & ddlyear.SelectedValue & "'"
                                strsubj += " And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'"
                                strsubj += " And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                                strsubj += " And subject_religions <> 'OTHERS'"
                            Else
                                ''select subject compulsory only for semester 1
                                strsubj += " subject_type = 'Compulsory'"
                                strsubj += " And subject_year = '" & ddlyear.SelectedValue & "'"
                                strsubj += " And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'"
                                strsubj += " And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                                strsubj += " And subject_religions <> 'ISLAM'"
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

                            Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & find_Mykad & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                            Dim find_PPCSDate As String = "select PPCSDate from PPCS where StudentID = '" & get_StudentID & "'"
                            Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                            Dim find_kolejKelas As String = "select class_Name from class_info where class_ID = '" & ddlclass.SelectedValue & "'"
                            Dim get_kolejKelas As String = oCommon.getFieldValue(find_kolejKelas)

                            Dim find_kokoKelas As String = "select KelasID from koko_kelas where Kelas = '" & get_kolejKelas & "'"
                            Dim get_kokoKelas As String = oCommon.getFieldValue_Permata(find_kokoKelas)

                            strSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan, KelasID) 
                                  values('" & get_StudentID & "','" & get_PPCSDate & "','" & ddlyear.SelectedValue & "','" & level & "','N','" & get_kokoKelas & "')"
                            strRet = oCommon.ExecuteSQLPermata(strSQL)

                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        ElseIf ddlstudentSem.SelectedValue = "Sem 2" Then

                            Using PJGDATA As New SqlCommand("INSERT into student_Level(std_ID,student_Sem,student_Level,year,month,day) 
                                                         values ('" & strKey & "','" & ddlstudentSem.SelectedValue & "','" & ddlstudentLevel.SelectedValue & "','" & ddlyear.SelectedValue & "','" & Now.Month & "','" & Now.Day & "')", objConn)
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
                            Else
                                ''select subject compulsory only for semester 2
                                strsubj += " subject_type = 'Compulsory'"
                                strsubj += " And subject_year = '" & ddlyear.SelectedValue & "'"
                                strsubj += " And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'"
                                strsubj += " And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                                strsubj += " And subject_religions <> 'ISLAM'"
                            End If

                            Dim strsubjDA As New SqlDataAdapter(strsubj, objConn)

                            Dim subjds As DataSet = New DataSet
                            strsubjDA.Fill(subjds, "SubjTable")

                            ''get class id for class compulsory
                            Dim get_ClassCompulsory As String = "select distinct course.class_ID from course left join class_info on course.class_ID = class_info.class_ID
                                                             where course.year =  '" & ddl_Year.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddl_Level.SelectedValue & "' and course.std_ID = '" & strKey & "'"
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

                            ''select subject electives and choose for semester 1 that student taken
                            Dim strsubH As String = "select subject_info.course_Name from subject_info left join course on subject_info.subject_ID = course.subject_ID where"
                            strsubH += " subject_type != 'Compulsory'"
                            strsubH += " And subject_year = '" & ddl_Year.SelectedValue & "'"
                            strsubH += " And subject_StudentYear = '" & ddl_Level.SelectedValue & "'"
                            strsubH += " And subject_sem = '" & ddl_Sem.SelectedValue & "'"
                            strsubH += " And std_ID = '" & strKey & "'"
                            Dim strsubjDB As New SqlDataAdapter(strsubH, objConn)

                            Dim subjdr As DataSet = New DataSet
                            strsubjDB.Fill(subjdr, "SubjTable")

                            ''loop for each subject id to get the same subject Name but for semester 2
                            For idx As Integer = 0 To subjdr.Tables(0).Rows.Count - 1
                                Dim subj As String = subjdr.Tables(0).Rows(idx).Item("subject_NameBM")

                                ''get the subject id for semester 2
                                Dim get_NonCompulsory_Sem2 As String = "Select subject_ID from subject_info where cousre_Name = '" & subj & "' And subject_year = '" & ddlyear.SelectedValue & "' And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'  And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                                Dim data_NonCompulsory_Sem2 As String = oCommon.getFieldValue(get_NonCompulsory_Sem2)

                                ''get the class id for semseter 2 for that new subject id
                                Dim get_ClassID_Sem2 As String = "Select class_ID from class_Info where subject_ID = '" & data_NonCompulsory_Sem2 & "' And class_year = '" & ddlyear.SelectedValue & "' And class_sem = '" & ddlstudentSem.SelectedValue & "'"
                                Dim data_ClassD_Sem2 As String = oCommon.getFieldValue(get_ClassID_Sem2)

                                ''insert into course table for subject non compulsory only
                                Dim strAdd As String = "INSERT INTO course(std_ID,class_ID,subject_ID,year) VALUES('" & strKey & "','" & data_ClassD_Sem2 & "' , '" & data_NonCompulsory_Sem2 & "', '" & ddlyear.SelectedValue & "' )"

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

                            Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & find_Mykad & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                            Dim find_PPCSDate As String = "select PPCSDate from PPCS where StudentID = '" & get_StudentID & "'"
                            Dim get_PPCSDate As String = oCommon.getFieldValue_Permata(find_PPCSDate)

                            Dim find_kolejKelas As String = "select class_Name from class_info where class_ID = '" & ddlclass.SelectedValue & "'"
                            Dim get_kolejKelas As String = oCommon.getFieldValue(find_kolejKelas)

                            Dim find_kokoKelas As String = "select KelasID from koko_kelas where Kelas = '" & get_kolejKelas & "'"
                            Dim get_kokoKelas As String = oCommon.getFieldValue_Permata(find_kokoKelas)

                            strSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, Disahkan, KelasID) 
                                  values('" & get_StudentID & "','" & get_PPCSDate & "','" & ddlyear.SelectedValue & "','" & level & "','N','" & get_kokoKelas & "')"
                            strRet = oCommon.ExecuteSQLPermata(strSQL)

                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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
                                get_exist = "select std_ID from student_Level where std_ID = '" & strID & "' and student_sem = '" & ddlstudentSem.SelectedValue & "'"

                                Using PJGDATA As New SqlCommand("UPDATE student_info SET student_Status='Graduate' where std_ID = '" & strID & "'", objConn)
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

        If errorCount > 0 Then
            ShowMessage(" Transfer Student", MessageType.Error)
        Else
            ShowMessage(" Transfer Student", MessageType.Success)
        End If

        strRet = BindData(datRespondent)
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

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
    End Sub

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

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub graduatedStudent_CheckedChanged(sender As Object, e As EventArgs) Handles graduatedStudent.CheckedChanged

        If graduatedStudent.Checked = True Then
            ddlstudentLevel.Enabled = False
            ddlstudentSem.Enabled = False
            ddlclass.Enabled = False

        ElseIf graduatedStudent.Checked = False Then
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