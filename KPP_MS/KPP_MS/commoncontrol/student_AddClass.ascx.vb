Imports System.Data.SqlClient

Public Class student_AddClass

    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                ddlYear()
                ddlLevel()
                ddlProgram()
                ddlCampus()
                ddlclassChoose.Enabled = False

                load_page()
                strRet = BindData(datRespondent)
                ''Generate_Table()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        Btnsimpan.Visible = False

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

            ''Get Function Button 1 Register Data 
            strSQL = "  Select B.F1_Register From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Register As String = oCommon.getFieldValue(strSQL)

            If find_Data_Menu_Data = "Class & Course Placement" And find_Data_Menu_Data.Length > 0 Then

                If find_Data_F1Register.Length > 0 And find_Data_F1Register = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_Menu_Data = "All" Then
                Btnsimpan.Visible = True
            End If

        Next

    End Sub

    Private Sub load_page()
        strSQL = "SELECT MAX(Parameter) as year from setting where type = 'year'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddl_year.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddl_year.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete student_info where std_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
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

            If gvTable.Rows.Count = 0 Then
                ddlclassChoose.Visible = False
                Btnsimpan.Visible = False
            Else
                Btnsimpan.Visible = True
                ddlclassChoose.Visible = True
            End If

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_Name ASC"

        tmpSQL = "Select distinct student_info.student_ID, student_info.std_ID,
                  student_info.student_Mykad,
                  student_info.student_Name,
                  student_Level.student_Level,
                  student_Level.student_Sem,
                  class_info.class_Name,
                  student_info.student_Campus
                  From student_info 
                  left join student_Level on student_info.std_ID=student_Level.std_ID
                  left join course on student_info.std_ID= course.std_ID 
                  left join class_info on course.class_ID = class_info.class_ID"
        strWhere = " WHERE student_info.std_ID Is Not NULL and (student_info.student_Status = 'Access' or student_info.student_Status = 'Graduate') and student_info.student_ID is not null and student_info.student_ID <> '' and ((student_info.student_ID like '%M%' or student_info.student_ID like '%P%') or student_info.student_ID like '%P%')"
        strWhere += " And course.year = '" & ddl_year.SelectedValue & "' and student_Stream = '" & ddl_Program.SelectedValue & "' and student_Level.Registered = 'Yes' "
        strWhere += " And student_level.year = '" & ddl_year.SelectedValue & "' AND ( class_info.class_type = 'Compulsory' or course.class_ID is null) and student_info.student_Campus = '" & ddl_Campus.SelectedValue & "'"

        If ddl_level.SelectedIndex > 0 Then
            strWhere += " And student_Level.student_Level = '" & ddl_level.SelectedValue & "'"
        End If

        If ddl_sem.SelectedIndex > 0 Then
            strWhere += " And student_Level.student_Sem = '" & ddl_sem.SelectedValue & "'"
        End If

        If ddl_class.SelectedIndex > 0 Then
            strWhere += " And class_info.class_ID = '" & ddl_class.SelectedValue & "' AND class_info.class_year = '" & ddl_year.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL

    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString

                If chkUpdate.Checked = True Then

                    Dim studentLevel As String = ""
                    Dim studentID As String = ""
                    Dim classLevel As String = ""
                    Dim religion As String = ""
                    Dim id As String = ""

                    studentID = "select std_ID from student_info where std_ID ='" & strKey & "' "
                    Dim datastudentID As String = getFieldValue(studentID, strConn)

                    studentLevel = "select student_Level from student_Level where std_ID ='" & datastudentID & "' And year = '" & ddl_year.SelectedValue & "'"
                    Dim datastudent As String = getFieldValue(studentLevel, strConn)

                    id = "select ID from student_Level where std_ID ='" & datastudentID & "' And year = '" & ddl_year.SelectedValue & "'"
                    Dim dataID As String = getFieldValue(id, strConn)

                    classLevel = "Select class_Level from class_info where class_ID='" & ddlclassChoose.SelectedValue & "' And class_year = '" & ddl_year.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "' and class_Campus = '" & ddl_Campus.SelectedValue & "'"
                    Dim dataclass As String = getFieldValue(classLevel, strConn)

                    religion = "select student_Religion from student_info where std_ID  ='" & datastudentID & "'"
                    Dim dataReligion = getFieldValue(religion, strConn)

                    If datastudent = dataclass Then

                        Try
                            Dim strsubj As String = ""

                            If dataReligion = "ISLAM" Or dataReligion = "islam" Or dataReligion = "Islam" Then

                                If ddl_level.SelectedValue = "Foundation 3" And ddl_sem.SelectedValue = "Sem 2" Then

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ISLAM'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"

                                ElseIf ddl_level.SelectedValue = "Level 2" And ddl_sem.SelectedValue = "Sem 2" Then

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ISLAM'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"

                                Else

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " And subject_Name <> 'Portfolio'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ISLAM'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"

                                End If

                            Else
                                If ddl_level.SelectedValue = "Foundation 3" And ddl_sem.SelectedValue = "Sem 2" Then

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'OTHERS'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"

                                ElseIf ddl_level.SelectedValue = "Level 2" And ddl_sem.SelectedValue = "Sem 2" Then

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'OTHERS'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"

                                Else

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " And subject_Name <> 'Portfolio'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'OTHERS'"
                                    strsubj += " And course_Program = '" & ddl_Program.SelectedValue & "'"
                                    strsubj += " And subject_Campus = '" & ddl_Campus.SelectedValue & "'"

                                End If

                            End If

                            Dim strsubjDA As New SqlDataAdapter(strsubj, objConn)
                            Dim subjds As DataSet = New DataSet
                            strsubjDA.Fill(subjds, "SubjTable")

                            Try
                                Dim MyConnection As SqlConnection = New SqlConnection(strConn)
                                Dim Dlt_ClassData As New SqlDataAdapter()

                                Dim dlt_Class As String

                                Dlt_ClassData.SelectCommand = New SqlCommand()
                                Dlt_ClassData.SelectCommand.Connection = MyConnection
                                Dlt_ClassData.SelectCommand.CommandText = "delete course where std_ID ='" & datastudentID & "' And year = '" & ddl_year.SelectedValue & "'"
                                MyConnection.Open()
                                dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
                                MyConnection.Close()

                            Catch ex As Exception

                            End Try

                            For idx As Integer = 0 To subjds.Tables(0).Rows.Count - 1
                                Dim subj As String = subjds.Tables(0).Rows(idx).Item("subject_ID")
                                Dim strAdd As String = "INSERT INTO course(std_ID,class_ID,subject_ID,ID,year) VALUES('" & datastudentID & "','" & ddlclassChoose.SelectedValue & "' , '" & subj & "', '" & dataID & "', '" & ddl_year.SelectedValue & "' )"

                                Using STDDATA As New SqlCommand(strAdd, objConn)
                                    objConn.Open()
                                    Dim run = STDDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using
                            Next

                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                            Dim find_Mykad As String = "select student_Mykad from student_info where std_ID = '" & datastudentID & "'"
                            Dim get_Mykad As String = oCommon.getFieldValue(find_Mykad)

                            Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & get_Mykad & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                            Dim find_kolejKelas As String = "select class_Name from class_info where class_ID = '" & ddlclassChoose.SelectedValue & "'"
                            Dim get_kolejKelas As String = oCommon.getFieldValue(find_kolejKelas)

                            Dim find_kokoKelas As String = "select KelasID from koko_kelas where Kelas = '" & get_kolejKelas & "' and Tahun = '" & ddl_year.SelectedValue & "' and Kampus = '" & ddl_Campus.SelectedValue & "'"
                            Dim get_kokoKelas As String = oCommon.getFieldValue_Permata(find_kokoKelas)

                            strSQL = "UPDATE koko_pelajar SET KelasID = '" & get_kokoKelas & "' where StudentID = '" & get_StudentID & "' and Tahun = '" & ddl_year.SelectedValue & "'"
                            strRet = oCommon.ExecuteSQLPermata(strSQL)

                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        Catch ex As Exception

                        End Try


                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End If
                '--execute SQL
            End If
        Next

        If errorCount > 0 Then
            ShowMessage(" Unsuccessful Add Student Class & Course ", MessageType.Success)
        Else
            ShowMessage(" Add Student Class & Course ", MessageType.Success)
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

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged

        ddlClass()
        ddlSem()

        If ddl_level.SelectedIndex > 0 Then
            ddlclassChoose.Enabled = True
        Else
            ddlclassChoose.Enabled = False
        End If

        BindData(datRespondent)

    End Sub

    Protected Sub ddl_sem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddl_Campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Campus.SelectedIndexChanged
        Try
            ddlProgram()
            ddlClass()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSem()
        Try
            strSQL = "Select * from setting where Type = 'Sem'"

            Debug.WriteLine(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_sem.DataSource = ds
            ddl_sem.DataTextField = "Parameter"
            ddl_sem.DataValueField = "Value"
            ddl_sem.DataBind()
            ddl_sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddl_sem.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlClass()
        Try
            strSQL = "SELECT class_Name, class_ID from class_info where class_type = 'Compulsory' and class_year = '" & ddl_year.SelectedValue & "' and class_Level = '" & ddl_level.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "' and class_Campus = '" & ddl_Campus.SelectedValue & "' ORDER BY class_Name ASC"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_class.DataSource = ds
            ddl_class.DataTextField = "class_Name"
            ddl_class.DataValueField = "class_ID"
            ddl_class.DataBind()
            ddl_class.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddl_class.SelectedIndex = 0

            ddlclassChoose.DataSource = ds
            ddlclassChoose.DataTextField = "class_Name"
            ddlclassChoose.DataValueField = "class_ID"
            ddlclassChoose.DataBind()
            ddlclassChoose.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlclassChoose.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear()
        Try
            Dim stryear As String = "Select Parameter from setting where Type = 'Year'"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddl_year.DataSource = yrds
            ddl_year.DataValueField = "Parameter"
            ddl_year.DataTextField = "Parameter"
            ddl_year.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevel()

        Try

            Dim strLevelSql As String = "Select Parameter,idx from setting where Type = 'Level' order by idx ASC"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_level.DataSource = levds
            ddl_level.DataValueField = "Parameter"
            ddl_level.DataTextField = "Parameter"
            ddl_level.DataBind()
            ddl_level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_level.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlProgram()

        Try

            If ddl_Campus.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Program.DataSource = levds
            ddl_Program.DataValueField = "Value"
            ddl_Program.DataTextField = "Parameter"
            ddl_Program.DataBind()
            ddl_Program.Items.Insert(0, New ListItem("Select Course Program", String.Empty))
            ddl_Program.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCampus()

        Try
            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Campus.DataSource = levds
            ddl_Campus.DataValueField = "Value"
            ddl_Campus.DataTextField = "Parameter"
            ddl_Campus.DataBind()
            ddl_Campus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
            ddl_Campus.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_class.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Program.SelectedIndexChanged
        Try
            ddlClass()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class