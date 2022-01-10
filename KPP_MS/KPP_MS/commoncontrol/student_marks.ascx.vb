Imports System.Data.SqlClient
Imports System.Globalization

Public Class student_marks1
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim permataConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim masterConn As String = ConfigurationManager.AppSettings("ConnectionMaster")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim PermataObjCon As SqlConnection = New SqlConnection(permataConn)
    Dim masterObjConn As SqlConnection = New SqlConnection(masterConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Checking_MenuAccess_Load()

                If Session("getStatus") = "AR" Then ''Academic Result
                    txtbreadcrum1.Text = "Academic Result"

                    AcademicResult.Visible = True
                    CocurricularResult.Visible = False

                    btnAcademicResult.Attributes("class") = "btn btn-info"
                    btnCocurricularResult.Attributes("class") = "btn btn-default font"

                    ddlYear()
                    ddlcampus()
                    ddlprogram()
                    ddlLevel()
                    ddlExam()
                    ddlCourseList()
                    ddlClassList()

                    load_page()

                    strRet = BindData(datRespondent)

                ElseIf Session("getStatus") = "CR" Then ''Cocurricular Result
                    txtbreadcrum1.Text = "Cocurricular Result"

                    AcademicResult.Visible = False
                    CocurricularResult.Visible = True

                    btnAcademicResult.Attributes("class") = "btn btn-default font"
                    btnCocurricularResult.Attributes("class") = "btn btn-info"

                    ddlCocu_Year()
                    ddlCocu_Exam()
                    ddlCocu_Level()
                    ddlCocu_Type()
                    ddlCocu_Name()

                    load_page_cocu()

                    strRet = BindDataCocu(CocuRespondent)

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Checking_MenuAccess_Load()

        btnAcademicResult.Visible = False
        btnCocurricularResult.Visible = False
        AcademicResult.Visible = False
        CocurricularResult.Visible = False

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

        Dim Get_AcademicResult As String = ""
        Dim Get_CocurricularResult As String = ""

        ''Loop The Count_No
        For num As Integer = 0 To find_CountNo_LoginID - 1 Step 1

            ''Get Main Menu Data
            strSQL = "  Select A.Menu From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_Menu_Data As String = oCommon.getFieldValue(strSQL)

            ''Get Sub Menu 2 Data
            strSQL = "  Select A.Menu_Sub2 From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_SubMenu2 As String = oCommon.getFieldValue(strSQL)

            ''Get Function Button 1 Update Data 
            strSQL = "  Select B.F1_Update From menu_master_access A Left Join menu_master_user B On A.MenuID = B.MenuID Where stf_ID = '" & stf_ID_Data & "' And login_ID = '" & find_LoginID & "'
                        Order By A.MenuID Asc
                        Offset " & num & " Rows Fetch Next 1 Rows Only"
            Dim find_Data_F1Update As String = oCommon.getFieldValue(strSQL)

            If find_Data_SubMenu2 = "Academic Result" And find_Data_SubMenu2.Length > 0 Then
                btnAcademicResult.Visible = True
                AcademicResult.Visible = True

                Get_AcademicResult = "TRUE"

                If find_Data_F1Update.Length > 0 And find_Data_F1Update = "TRUE" Then
                    Btnsimpan.Visible = True
                End If
            End If

            If find_Data_SubMenu2 = "Cocurriculum Result" And find_Data_SubMenu2.Length > 0 Then
                btnCocurricularResult.Visible = True
                CocurricularResult.Visible = True

                Get_CocurricularResult = "TRUE"
            End If

            If find_Data_SubMenu2.Length = 0 And find_Data_Menu_Data = "All" Then
                btnAcademicResult.Visible = True
                btnCocurricularResult.Visible = True
                AcademicResult.Visible = True
                CocurricularResult.Visible = True

                Btnsimpan.Visible = True

                Get_AcademicResult = "TRUE"
            End If

        Next

        Dim Data_If_Not_Group_Status As String = ""
        Session("getStatus_Temporary") = ""

        If Session("getStatus") = "AR" Or Session("getStatus") = "CR" Then
            Data_If_Not_Group_Status = Session("getStatus")
        End If

        If Session("getStatus") <> "AR" And Session("getStatus") <> "CR" Then
            If Get_AcademicResult = "TRUE" Then
                Data_If_Not_Group_Status = "AR"
            ElseIf Get_CocurricularResult = "TRUE" Then
                Data_If_Not_Group_Status = "CR"
            End If
        End If

        If Session("getStatus_Temporary") IsNot Nothing Then
            If Get_AcademicResult = "TRUE" And Data_If_Not_Group_Status = "AR" Then
                Session("getStatus") = "AR"
            ElseIf Get_CocurricularResult = "TRUE" And Data_If_Not_Group_Status = "CR" Then
                Session("getStatus") = "CR"
            End If
        End If

    End Sub

    Private Sub btnAcademicResult_ServerClick(sender As Object, e As EventArgs) Handles btnAcademicResult.ServerClick
        Session("getStatus") = "AR"
        Response.Redirect("admin_peperiksaan_kemaskini_markah.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnCocurricularResult_ServerClick(sender As Object, e As EventArgs) Handles btnCocurricularResult.ServerClick
        Session("getStatus") = "CR"
        Response.Redirect("admin_peperiksaan_kemaskini_markah.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter from setting where Type ='Year'  and Parameter = '" & Now.Year & "'"

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
                ddl_year.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_year.SelectedIndex = 0
            End If
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by student_info.student_Name ASC"

        tmpSQL = "select distinct exam_result.course_ID, exam_result.ID, student_info.student_ID, student_info.student_Name, class_info.class_Name, exam_Info.exam_Name, subject_info.subject_Name, exam_result.marks, exam_result.grade
                  From exam_result Join course On exam_result.course_ID = course.course_ID
                  Left Join exam_info On exam_result.exam_ID = exam_Info.exam_ID
                  Left Join student_info On course.std_ID = student_info.std_ID
                  Left Join class_info On course.class_ID = class_info.class_ID
                  Left Join subject_info On course.subject_ID = subject_info.subject_ID left Join student_Png On student_info.std_ID=student_Png.std_ID
                  Where exam_result.ID Is Not null and (student_info.student_status = 'Access' or student_info.student_Status = 'Graduate') and student_info.student_ID is not null and student_info.student_ID <> '' and (student_info.student_ID like '%M%' or student_info.student_ID like '%P%')
                  And subject_info.subject_Campus = '" & ddl_campus.SelectedValue & "' and subject_info.course_Program = '" & ddl_program.SelectedValue & "' "
        strWhere += " And exam_Info.exam_Year = '" & ddl_year.SelectedValue & "'"
        strWhere += " And exam_Info.exam_Name = '" & ddl_exam.SelectedValue & "' and (exam_Info.exam_Institutions = '" & ddl_campus.SelectedValue & "' or exam_Info.exam_Institutions = 'ALL')"
        strWhere += " And subject_info.subject_StudentYear = '" & ddl_level.SelectedValue & "'"

        If ddl_class.SelectedIndex > 0 Then
            strWhere += " And course.class_ID = '" & ddl_class.SelectedValue & "' and class_info.class_Campus = '" & ddl_campus.SelectedValue & "' and class_info.course_Program = '" & ddl_program.SelectedValue & "'"
        End If

        If ddl_subject.SelectedIndex > 0 Then
            strWhere += " And course.subject_ID = '" & ddl_subject.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Private Sub ddlYear()
        Try
            Dim stryear As String = "Select distinct exam_Year from exam_Info"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddl_year.DataSource = yrds
            ddl_year.DataValueField = "exam_Year"
            ddl_year.DataTextField = "exam_Year"
            ddl_year.DataBind()
            ddl_year.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlcampus()
        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_campus.DataSource = ds
            ddl_campus.DataTextField = "Parameter"
            ddl_campus.DataValueField = "Value"
            ddl_campus.DataBind()
            ddl_campus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlprogram()
        If ddl_campus.SelectedValue = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_program.DataSource = ds
            ddl_program.DataTextField = "Parameter"
            ddl_program.DataValueField = "Value"
            ddl_program.DataBind()
            ddl_program.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlLevel()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Level' order by Parameter ASC"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_level.DataSource = levds
            ddl_level.DataValueField = "Parameter"
            ddl_level.DataTextField = "Parameter"
            ddl_level.DataBind()
            ddl_level.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlExam()
        Try
            Dim strLevelSql As String = ""

            If ddl_level.SelectedValue = "Foundation 1" Or ddl_level.SelectedValue = "Level 1" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 1' or Parameter = 'Exam 2' or Parameter = 'Exam 3' or Parameter = 'Exam 4')"

            ElseIf ddl_level.SelectedValue = "Foundation 2" Or ddl_level.SelectedValue = "Level 2" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 5' or Parameter = 'Exam 6' or Parameter = 'Exam 7' or Parameter = 'Exam 8')"

            ElseIf ddl_level.SelectedValue = "Foundation 3" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 9' or Parameter = 'Exam 10' or Parameter = 'Exam 11' or Parameter = 'Exam 12')"

            End If

            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "ExamTable")

            ddl_exam.DataSource = levds
            ddl_exam.DataValueField = "Parameter"
            ddl_exam.DataTextField = "Parameter"
            ddl_exam.DataBind()
            ddl_exam.Items.Insert(0, New ListItem("Select Exam", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCourseList()
        Try
            Dim findSemester As String = ""

            If ddl_exam.SelectedValue = "Exam 1" Or ddl_exam.SelectedValue = "Exam 2" Or ddl_exam.SelectedValue = "Exam 5" Or ddl_exam.SelectedValue = "Exam 6" Or ddl_exam.SelectedValue = "Exam 9" Or ddl_exam.SelectedValue = "Exam 10" Then
                findSemester = "Sem 1"

            ElseIf ddl_exam.SelectedValue = "Exam 3" Or ddl_exam.SelectedValue = "Exam 4" Or ddl_exam.SelectedValue = "Exam 7" Or ddl_exam.SelectedValue = "Exam 8" Or ddl_exam.SelectedValue = "Exam 11" Or ddl_exam.SelectedValue = "Exam 12" Then
                findSemester = "Sem 2"

            End If

            Dim strLevelSql As String = "Select subject_ID, subject_Name from subject_info where subject_year = '" & ddl_year.SelectedValue & "' and subject_StudentYear = '" & ddl_level.SelectedValue & "' and (subject_sem is null or subject_sem = '" & findSemester & "') and subject_Campus = '" & ddl_campus.SelectedValue & "' and course_Program = '" & ddl_program.SelectedValue & "' order by subject_Name asc"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_subject.DataSource = levds
            ddl_subject.DataValueField = "subject_ID"
            ddl_subject.DataTextField = "subject_Name"
            ddl_subject.DataBind()
            ddl_subject.Items.Insert(0, New ListItem("Select Course", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlClassList()
        Try
            Dim findClassType As String = "Select subject_type from subject_info where subject_id = '" & ddl_subject.SelectedValue & "'"
            Dim getclassType As String = oCommon.getFieldValue(findClassType)

            Dim strclassName As String = ""

            If getclassType <> "Compulsory" Then
                strclassName = "Select class_id, class_Name from class_info where class_year = '" & ddl_year.SelectedValue & "' and class_level= '" & ddl_level.SelectedValue & "' and class_type <> 'Compulsory' and subject_id = '" & ddl_subject.SelectedValue & "' and class_Campus = '" & ddl_campus.SelectedValue & "' and course_Program = '" & ddl_program.SelectedValue & "' order by class_Name asc"

            ElseIf getclassType = "Compulsory" Then
                strclassName = "Select class_id, class_Name from class_info where class_year = '" & ddl_year.SelectedValue & "' and class_level= '" & ddl_level.SelectedValue & "' and class_type = 'Compulsory' and class_Campus = '" & ddl_campus.SelectedValue & "' and course_Program = '" & ddl_program.SelectedValue & "'  order by class_Name asc"

            End If

            Dim sqlLevelDA As New SqlDataAdapter(strclassName, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_class.DataSource = levds
            ddl_class.DataValueField = "class_id"
            ddl_class.DataTextField = "class_Name"
            ddl_class.DataBind()
            ddl_class.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            ddlLevel()
            ddlExam()
            ddlCourseList()
            ddlClassList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            ddlExam()
            ddlCourseList()
            ddlClassList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlExam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_exam.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            ddlCourseList()
            ddlClassList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_campus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_campus.SelectedIndexChanged
        Try
            ddlprogram()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_program.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            ddlCourseList()
            ddlClassList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_class.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_subject.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            ddlClassList()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim marks As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtmarks"), TextBox)

            If marks.Text.Length > 0 Then

                Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

                Dim ResultGrades As String = "select grade_Name from grade_info where grade_min_range <= '" & marks.Text & "' and grade_max_range >= '" & marks.Text & "'"

                Dim grades As String = oCommon.getFieldValue(ResultGrades)

                ''update marks and grade
                strSQL = "UPDATE exam_result SET marks='" & marks.Text & "', grade='" & grades & "' WHERE ID ='" & strKeyID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
        Next

        If strRet = 0 Then
            ShowMessage(" Update Student Result", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Update Student Result", MessageType.Error)
        End If

        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlCocu_Year()
        Try
            Dim stryear As String = "Select distinct exam_Year from exam_Info"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddl_cocuYear.DataSource = yrds
            ddl_cocuYear.DataValueField = "exam_Year"
            ddl_cocuYear.DataTextField = "exam_Year"
            ddl_cocuYear.DataBind()
            ddl_cocuYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCocu_Level()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Level' order by idx ASC"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_cocuLevel.DataSource = levds
            ddl_cocuLevel.DataValueField = "Parameter"
            ddl_cocuLevel.DataTextField = "Parameter"
            ddl_cocuLevel.DataBind()
            ddl_cocuLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCocu_Exam()
        Try
            Dim strLevelSql As String = ""

            If ddl_cocuLevel.SelectedValue = "Foundation 1" Or ddl_cocuLevel.SelectedValue = "Level 1" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 1' or Parameter = 'Exam 2' or Parameter = 'Exam 3' or Parameter = 'Exam 4')"

            ElseIf ddl_cocuLevel.SelectedValue = "Foundation 2" Or ddl_cocuLevel.SelectedValue = "Level 2" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 5' or Parameter = 'Exam 6' or Parameter = 'Exam 7' or Parameter = 'Exam 8')"

            ElseIf ddl_cocuLevel.SelectedValue = "Foundation 3" Then
                strLevelSql = "Select Parameter from setting where Type = 'Exam' and (Parameter = 'Exam 9' or Parameter = 'Exam 10' or Parameter = 'Exam 11' or Parameter = 'Exam 12')"

            End If

            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "ExamTable")

            ddl_cocuExam.DataSource = levds
            ddl_cocuExam.DataValueField = "Parameter"
            ddl_cocuExam.DataTextField = "Parameter"
            ddl_cocuExam.DataBind()
            ddl_cocuExam.Items.Insert(0, New ListItem("Select Exam", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCocu_Type()
        Try
            Dim PermataConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
            Dim permataObjConn As SqlConnection = New SqlConnection(PermataConn)

            Dim List_SQL As String = "select KategoriID,Kategori from koko_kategori where kategoriID = 9"
            Dim sqlListDA As New SqlDataAdapter(List_SQL, permataObjConn)

            Dim listDS As DataSet = New DataSet
            sqlListDA.Fill(listDS, "ListTable")

            ddl_cocuType.DataSource = listDS
            ddl_cocuType.DataValueField = "KategoriID"
            ddl_cocuType.DataTextField = "Kategori"
            ddl_cocuType.DataBind()
            ddl_cocuType.Items.Insert(0, New ListItem("Select Type", String.Empty))
            ddl_cocuType.Items.Insert(1, New ListItem("Clubs", "1"))
            ddl_cocuType.Items.Insert(2, New ListItem("Sports", "2"))
            ddl_cocuType.Items.Insert(3, New ListItem("Uniforms", "3"))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlCocu_Name()
        Try
            Dim strSQLCocu As String = ""

            If ddl_cocuType.SelectedValue = "3" Then
                strSQLCocu = "select KokoID,Nama from koko_kolejpermata where jenis = 'UNIFORM' and Tahun = '" & ddl_cocuYear.SelectedValue & "'"

            ElseIf ddl_cocuType.SelectedValue = "2" Then
                strSQLCocu = "select KokoID, Nama from koko_kolejpermata where jenis = 'SUKAN' and Tahun = '" & ddl_cocuYear.SelectedValue & "'"

            ElseIf ddl_cocuType.SelectedValue = "1" Then
                strSQLCocu = "select KokoID, Nama from koko_kolejpermata where jenis = 'PERSATUAN' and Tahun = '" & ddl_cocuYear.SelectedValue & "'"

            End If

            Dim PermataConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
            Dim permataObjConn As SqlConnection = New SqlConnection(PermataConn)

            Dim sqlListDA As New SqlDataAdapter(strSQLCocu, permataObjConn)

            Dim listDS As DataSet = New DataSet
            sqlListDA.Fill(listDS, "ListTable")

            ddl_cocuName.DataSource = listDS
            ddl_cocuName.DataTextField = "Nama"
            ddl_cocuName.DataValueField = "KokoID"
            ddl_cocuName.DataBind()
            ddl_cocuName.Items.Insert(0, New ListItem("Select Cocurricular", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub load_page_cocu()
        strSQL = "SELECT Parameter from setting where Type ='Year'  and Parameter = '" & Now.Year & "'"

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
                ddl_cocuYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_cocuYear.SelectedIndex = 0
            End If
        End If
    End Sub

    Protected Sub ddl_cocuYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_cocuYear.SelectedIndexChanged
        Try
            strRet = BindDataCocu(CocuRespondent)
            ddlCocu_Exam()
            ddlCocu_Level()
            ddlCocu_Type()
            ddlCocu_Name()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_cocuLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_cocuLevel.SelectedIndexChanged
        Try
            strRet = BindDataCocu(CocuRespondent)
            ddlCocu_Exam()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_cocuExam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_cocuExam.SelectedIndexChanged
        Try
            strRet = BindDataCocu(CocuRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_cocuType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_cocuType.SelectedIndexChanged
        Try
            strRet = BindDataCocu(CocuRespondent)
            ddlCocu_Name()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_cocuName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_cocuName.SelectedIndexChanged
        Try
            strRet = BindDataCocu(CocuRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataCocu(ByVal gvTable As GridView) As Boolean

        Dim myDataset As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLCocu, masterObjConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataset, "myaccount")

            gvTable.DataSource = myDataset
            gvTable.DataBind()

            masterObjConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQLCocu() As String

        Dim tempSQL As String = ""
        Dim strwhere As String = ""
        Dim strorderby As String = " order by kolejadmin.dbo.student_info.student_Name ASC"
        Dim passSQL As String = ""

        If ddl_cocuExam.SelectedValue = "Exam 2" Or ddl_cocuExam.SelectedValue = "Exam 6" Or ddl_cocuExam.SelectedValue = "Exam 10" Then

            tempSQL = " select distinct kolejadmin.dbo.student_info.std_ID,kolejadmin.dbo.student_info.student_Name,kolejadmin.dbo.student_info.student_ID,kolejadmin.dbo.class_info.class_Name,
                        kolejadmin.dbo.exam_info.exam_name,permatapintar.dbo.koko_kolejpermata.Nama,"

            If ddl_cocuType.SelectedValue = "2" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Sukan_JumlahP1 as Jumlah,permatapintar.dbo.koko_pelajar.Sukan_GredP1 as Gred"

            ElseIf ddl_cocuType.SelectedValue = "1" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Persatuan_JumlahP1 as Jumlah,permatapintar.dbo.koko_pelajar.Persatuan_GredP1 as Gred"

            ElseIf ddl_cocuType.SelectedValue = "3" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Uniform_JumlahP1 as Jumlah,permatapintar.dbo.koko_pelajar.Uniform_GredP1 as Gred"

            End If

            tempSQL += " from permatapintar.dbo.koko_pelajar
                        left join permatapintar.dbo.StudentProfile on permatapintar.dbo.koko_pelajar.StudentID = permatapintar.dbo.StudentProfile.StudentID"

            If ddl_cocuType.SelectedValue = "2" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.SukanID = permatapintar.dbo.koko_kolejpermata.KokoID"

            ElseIf ddl_cocuType.SelectedValue = "1" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.PersatuanID = permatapintar.dbo.koko_kolejpermata.KokoID"

            ElseIf ddl_cocuType.SelectedValue = "3" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.UniformID = permatapintar.dbo.koko_kolejpermata.KokoID"

            End If

            tempSQL += " left join kolejadmin.dbo.student_info on permatapintar.dbo.StudentProfile.MYKAD = kolejadmin.dbo.student_info.student_Mykad
                        left join kolejadmin.dbo.student_level on kolejadmin.dbo.student_info.std_ID = kolejadmin.dbo.student_level.std_ID
                        left join kolejadmin.dbo.course on kolejadmin.dbo.student_info.std_ID = kolejadmin.dbo.course.std_ID
                        left join kolejadmin.dbo.class_info on kolejadmin.dbo.course.class_ID = kolejadmin.dbo.class_info.class_ID
                        left join kolejadmin.dbo.exam_result on kolejadmin.dbo.course.course_ID = kolejadmin.dbo.exam_result.course_ID
                        left join kolejadmin.dbo.exam_Info on kolejadmin.dbo.exam_result.exam_ID = kolejadmin.dbo.exam_info.exam_ID"

            strwhere = " where permatapintar.dbo.koko_pelajar.Tahun = '" & ddl_cocuYear.SelectedValue & "' and ( kolejadmin.dbo.student_info.student_status = 'Access' or kolejadmin.dbo.student_info.student_Status = 'Graduate' ) and  kolejadmin.dbo.student_info.student_ID is not null and  kolejadmin.dbo.student_info.student_ID <> '' and  (kolejadmin.dbo.student_info.student_ID like '%M%' or student_info.student_ID like '%P%')
                         and permatapintar.dbo.koko_kolejpermata.Tahun = '" & ddl_cocuYear.SelectedValue & "' 
                         and kolejadmin.dbo.student_level.year = '" & ddl_cocuYear.SelectedValue & "' 
                         and kolejadmin.dbo.course.year = '" & ddl_cocuYear.SelectedValue & "' 
                         and kolejadmin.dbo.class_info.class_year = '" & ddl_cocuYear.SelectedValue & "' 
                         and kolejadmin.dbo.exam_info.exam_Year = '" & ddl_cocuYear.SelectedValue & "'"

            strwhere += " and kolejadmin.dbo.exam_Info.exam_name ='" & ddl_cocuExam.SelectedValue & "'"

            strwhere += " and kolejadmin.dbo.class_info.class_type = 'Compulsory'"

            strwhere += " and kolejadmin.dbo.student_level.student_Level = '" & ddl_cocuLevel.SelectedValue & "'"

            strwhere += " and permatapintar.dbo.koko_kolejpermata.KokoID = '" & ddl_cocuName.SelectedValue & "'"

            passSQL = tempSQL & strwhere & strorderby


        ElseIf ddl_cocuExam.SelectedValue = "Exam 4" Or ddl_cocuExam.SelectedValue = "Exam 7" Or ddl_cocuExam.SelectedValue = "Exam 8" Or ddl_cocuExam.SelectedValue = "Exam 12" Then

            tempSQL = " select distinct kolejadmin.dbo.student_info.std_ID,kolejadmin.dbo.student_info.student_Name,kolejadmin.dbo.student_info.student_ID,kolejadmin.dbo.class_info.class_Name,
                        kolejadmin.dbo.exam_info.exam_name,permatapintar.dbo.koko_kolejpermata.Nama,"

            If ddl_cocuType.SelectedValue = "2" Then
                tempSQL += " permatapintar.dbo.koko_pelajar.Sukan_JumlahP2 as Jumlah,permatapintar.dbo.koko_pelajar.Sukan_GredP2 as Gred"

            ElseIf ddl_cocuType.SelectedValue = "1" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Persatuan_JumlahP2 as Jumlah,permatapintar.dbo.koko_pelajar.Persatuan_GredP2 as Gred"

            ElseIf ddl_cocuType.SelectedValue = "3" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Uniform_JumlahP2 as Jumlah,permatapintar.dbo.koko_pelajar.Uniform_GredP2 as Gred"

            End If

            tempSQL += " from permatapintar.dbo.koko_pelajar
                        left join permatapintar.dbo.StudentProfile on permatapintar.dbo.koko_pelajar.StudentID = permatapintar.dbo.StudentProfile.StudentID"

            If ddl_cocuType.SelectedValue = "2" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.SukanID = permatapintar.dbo.koko_kolejpermata.KokoID"

            ElseIf ddl_cocuType.SelectedValue = "1" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.PersatuanID = permatapintar.dbo.koko_kolejpermata.KokoID"

            ElseIf ddl_cocuType.SelectedValue = "3" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.UniformID = permatapintar.dbo.koko_kolejpermata.KokoID"

            End If

            tempSQL += " left join kolejadmin.dbo.student_info on permatapintar.dbo.StudentProfile.MYKAD = kolejadmin.dbo.student_info.student_Mykad
                        left join kolejadmin.dbo.student_level on kolejadmin.dbo.student_info.std_ID = kolejadmin.dbo.student_level.std_ID
                        left join kolejadmin.dbo.course on kolejadmin.dbo.student_info.std_ID = kolejadmin.dbo.course.std_ID
                        left join kolejadmin.dbo.class_info on kolejadmin.dbo.course.class_ID = kolejadmin.dbo.class_info.class_ID
                        left join kolejadmin.dbo.exam_result on kolejadmin.dbo.course.course_ID = kolejadmin.dbo.exam_result.course_ID
                        left join kolejadmin.dbo.exam_Info on kolejadmin.dbo.exam_result.exam_ID = kolejadmin.dbo.exam_info.exam_ID"

            strwhere = " where permatapintar.dbo.koko_pelajar.Tahun = '" & ddl_cocuYear.SelectedValue & "' and ( kolejadmin.dbo.student_info.student_status = 'Access' or kolejadmin.dbo.student_info.student_Status = 'Graduate' ) and  kolejadmin.dbo.student_info.student_ID is not null and  kolejadmin.dbo.student_info.student_ID <> '' and (kolejadmin.dbo.student_info.student_ID like '%M%' or student_info.student_ID like '%P%')
                         and permatapintar.dbo.koko_kolejpermata.Tahun = '" & ddl_cocuYear.SelectedValue & "' 
                         and kolejadmin.dbo.student_level.year = '" & ddl_cocuYear.SelectedValue & "' 
                         and kolejadmin.dbo.course.year = '" & ddl_cocuYear.SelectedValue & "' 
                         and kolejadmin.dbo.class_info.class_year = '" & ddl_cocuYear.SelectedValue & "' 
                         and kolejadmin.dbo.exam_info.exam_Year = '" & ddl_cocuYear.SelectedValue & "'"

            strwhere += " and kolejadmin.dbo.exam_Info.exam_name ='" & ddl_cocuExam.SelectedValue & "'"

            strwhere += " and kolejadmin.dbo.class_info.class_type = 'Compulsory'"

            strwhere += " and kolejadmin.dbo.student_level.student_Level = '" & ddl_cocuLevel.SelectedValue & "'"

            strwhere += " and permatapintar.dbo.koko_kolejpermata.KokoID = '" & ddl_cocuName.SelectedValue & "'"

            passSQL = tempSQL & strwhere & strorderby

        End If

        getSQLCocu = passSQL

        Return getSQLCocu

    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class