Imports System.Data.SqlClient
Imports System.Globalization

Public Class Student_marks_koko
    Inherits System.Web.UI.UserControl

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
                ddl_class.Enabled = False
                ddl_coccuriculum.Enabled = False
                ddl_list.Enabled = False

                ddlYear()
                ddlExam()
                ddlLevel()
                ddlCocu()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean

        Dim myDataset As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, masterObjConn)
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

    Protected Sub datRespondent_PageIndexChanging(sender As Object, e As EventArgs) Handles datRespondentUni1.PageIndexChanging

        Try
            strRet = BindData(datRespondentUni1)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlExam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_exam.SelectedIndexChanged
        Try
            strRet = BindData(datRespondentUni1)

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_class.SelectedIndexChanged

        Try
            strRet = BindData(datRespondentUni1)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_list.SelectedIndexChanged

        Try

            strRet = BindData(datRespondentUni1)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged

        If Not ddl_level.SelectedValue = "-1" Then

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objconn As SqlConnection = New SqlConnection(strconn)

            strSQL = "select class_ID,class_Name from class_info where class_level = '" & ddl_level.SelectedValue & "' and class_year = '" & ddl_year.SelectedValue & "' and class_type = 'Compulsory'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "ClassTable")

            ddl_class.DataSource = ds
            ddl_class.DataTextField = "class_Name"
            ddl_class.DataValueField = "class_ID"
            ddl_class.DataBind()
            ddl_class.Items.Insert(0, New ListItem("-Select Class-", "0"))
            ddl_class.Items.Insert(1, New ListItem("ALL", "ALL"))
            ddl_class.Enabled = True

        Else
            ddl_class.Items.Clear()
            ddl_class.Enabled = False

        End If

    End Sub

    Protected Sub ddlCoccuriculum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_coccuriculum.SelectedIndexChanged

        If Not ddl_coccuriculum.SelectedValue = "-1" Then

            If ddl_coccuriculum.SelectedValue = "1" Then

                Dim strconn As String = ConfigurationManager.AppSettings("ConnectionPermata")
                Dim objconn As SqlConnection = New SqlConnection(strconn)

                strSQL = "select KokoID, Nama from koko_kolejpermata where jenis = 'SUKAN' and Tahun = '" & ddl_year.SelectedValue & "'"
                Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

                Dim sukanDS As DataSet = New DataSet
                sqlDA.Fill(sukanDS, "sukanTable")


                ddl_list.DataSource = sukanDS
                ddl_list.DataTextField = "Nama"
                ddl_list.DataValueField = "KokoID"
                ddl_list.DataBind()
                ddl_list.Items.Insert(0, New ListItem("-select Sport-", "0"))
                ddl_list.Enabled = True

            ElseIf ddl_coccuriculum.SelectedValue = "2" Then

                Dim strconn As String = ConfigurationManager.AppSettings("ConnectionPermata")
                Dim objconn As SqlConnection = New SqlConnection(strconn)


                strSQL = "select KokoID, Nama from koko_kolejpermata where jenis = 'PERSATUAN' and Tahun = '" & ddl_year.SelectedValue & "'"
                Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

                Dim CLubDS As DataSet = New DataSet
                sqlDA.Fill(CLubDS, "ClubTable")

                ddl_list.DataSource = CLubDS
                ddl_list.DataTextField = "Nama"
                ddl_list.DataValueField = "KokoID"
                ddl_list.DataBind()
                ddl_list.Items.Insert(0, New ListItem("-select Clubs-", "0"))
                ddl_list.Enabled = True

            ElseIf ddl_coccuriculum.SelectedValue = "3" Then

                Dim strconn As String = ConfigurationManager.AppSettings("ConnectionPermata")
                Dim objconn As SqlConnection = New SqlConnection(strconn)


                strSQL = "select KokoID,Nama from koko_kolejpermata where jenis = 'UNIFORM' and Tahun = '" & ddl_year.SelectedValue & "'"
                Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

                Dim UniformDS As DataSet = New DataSet
                sqlDA.Fill(UniformDS, "UniformTable")

                ddl_list.DataSource = UniformDS
                ddl_list.DataTextField = "Nama"
                ddl_list.DataValueField = "KokoID"
                ddl_list.DataBind()
                ddl_list.Items.Insert(0, New ListItem("-select Uniform Unit-", "0"))
                ddl_list.Enabled = True

            Else
                ddl_list.Items.Clear()
                ddl_list.Enabled = False
            End If

        End If

    End Sub

    Protected Sub ddlYear()

        Try
            Dim strYear As String = "select distinct exam_year from exam_info"
            Dim sqlYearDa As New SqlDataAdapter(strYear, objConn)

            Dim yearDS As DataSet = New DataSet
            sqlYearDa.Fill(yearDS, "YearTable")

            ddl_year.DataSource = yearDS
            ddl_year.DataValueField = "exam_Year"
            ddl_year.DataTextField = "exam_Year"
            ddl_year.DataBind()
            ddl_year.Items.Insert(0, New ListItem(Year(Now), Year(Now)))
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlCocu()

        Try
            Dim PermataConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
            Dim permataObjConn As SqlConnection = New SqlConnection(PermataConn)

            Dim List_SQL As String = "select KategoriID,Kategori from koko_kategori where kategoriID = 9"
            Dim sqlListDA As New SqlDataAdapter(List_SQL, permataObjConn)

            Dim listDS As DataSet = New DataSet
            sqlListDA.Fill(listDS, "ListTable")

            ddl_coccuriculum.DataSource = listDS
            ddl_coccuriculum.DataValueField = "KategoriID"
            ddl_coccuriculum.DataTextField = "Kategori"
            ddl_coccuriculum.DataBind()
            ddl_coccuriculum.Items.Insert(0, New ListItem("-Select Co-Curiculum Type-", "0"))
            ddl_coccuriculum.Items.Insert(1, New ListItem("-Sports-", "1"))
            ddl_coccuriculum.Items.Insert(2, New ListItem("-Club and Association-", "2"))
            ddl_coccuriculum.Items.Insert(3, New ListItem("-Uniform Unit-", "3"))
            ddl_coccuriculum.Enabled = True

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlExam()
        Try
            Dim strExam As String = "select parameter from setting where type = 'exam'"
            Dim sqlExamDA As New SqlDataAdapter(strExam, objConn)

            Dim examDS As DataSet = New DataSet
            sqlExamDA.Fill(examDS, "ExamTable")

            ddl_exam.DataSource = examDS
            ddl_exam.DataValueField = "Parameter"
            ddl_exam.DataTextField = "Parameter"
            ddl_exam.DataBind()
            ddl_exam.Items.Insert(0, New ListItem("-Exam-", "0"))

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
            ddl_level.Items.Insert(0, New ListItem("-Select Foundation/Level-", ""))
        Catch ex As Exception

        End Try

    End Sub

    Private Function getSQL() As String

        Dim tempSQL As String = ""
        Dim strwhere As String = ""
        Dim strorderby As String = " order by kolejadmin.dbo.student_info.student_Name ASC"
        Dim passSQL As String = ""

        If ddl_exam.SelectedValue = "Exam 2" Or ddl_exam.SelectedValue = "Exam 6" Or ddl_exam.SelectedValue = "Exam 10" Then

            tempSQL = " select distinct kolejadmin.dbo.student_info.std_ID,kolejadmin.dbo.student_info.student_Name,kolejadmin.dbo.student_info.student_ID,kolejadmin.dbo.class_info.class_Name,
                        kolejadmin.dbo.exam_info.exam_name,permatapintar.dbo.koko_kolejpermata.Nama,"

            If ddl_coccuriculum.SelectedValue = "1" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Sukan_JumlahP1 as Jumlah,permatapintar.dbo.koko_pelajar.Sukan_GredP1 as Gred"
            ElseIf ddl_coccuriculum.SelectedValue = "2" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Persatuan_JumlahP1 as Jumlah,permatapintar.dbo.koko_pelajar.Persatuan_GredP1 as Gred"
            ElseIf ddl_coccuriculum.SelectedValue = "3" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Uniform_JumlahP1 as Jumlah,permatapintar.dbo.koko_pelajar.Uniform_GredP1 as Gred"
            End If

            tempSQL += " from permatapintar.dbo.koko_pelajar
                        left join permatapintar.dbo.StudentProfile on permatapintar.dbo.koko_pelajar.StudentID = permatapintar.dbo.StudentProfile.StudentID"

            If ddl_coccuriculum.SelectedValue = "1" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.SukanID = permatapintar.dbo.koko_kolejpermata.KokoID"
            ElseIf ddl_coccuriculum.SelectedValue = "2" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.PersatuanID = permatapintar.dbo.koko_kolejpermata.KokoID"
            ElseIf ddl_coccuriculum.SelectedValue = "3" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.UniformID = permatapintar.dbo.koko_kolejpermata.KokoID"
            End If

            tempSQL += " left join kolejadmin.dbo.student_info on permatapintar.dbo.StudentProfile.MYKAD = kolejadmin.dbo.student_info.student_Mykad
                        left join kolejadmin.dbo.student_level on kolejadmin.dbo.student_info.std_ID = kolejadmin.dbo.student_level.std_ID
                        left join kolejadmin.dbo.course on kolejadmin.dbo.student_info.std_ID = kolejadmin.dbo.course.std_ID
                        left join kolejadmin.dbo.class_info on kolejadmin.dbo.course.class_ID = kolejadmin.dbo.class_info.class_ID
                        left join kolejadmin.dbo.exam_result on kolejadmin.dbo.course.course_ID = kolejadmin.dbo.exam_result.course_ID
                        left join kolejadmin.dbo.exam_Info on kolejadmin.dbo.exam_result.exam_ID = kolejadmin.dbo.exam_info.exam_ID"

            strwhere = " where permatapintar.dbo.koko_pelajar.Tahun = '" & ddl_year.SelectedValue & "' 
                         and permatapintar.dbo.koko_kolejpermata.Tahun = '" & ddl_year.SelectedValue & "' 
                         and kolejadmin.dbo.student_level.year = '" & ddl_year.SelectedValue & "' 
                         and kolejadmin.dbo.course.year = '" & ddl_year.SelectedValue & "' 
                         and kolejadmin.dbo.class_info.class_year = '" & ddl_year.SelectedValue & "' 
                         and kolejadmin.dbo.exam_info.exam_Year = '" & ddl_year.SelectedValue & "'"

            strwhere += " and kolejadmin.dbo.exam_Info.exam_name ='" & ddl_exam.SelectedValue & "'"

            If ddl_class.SelectedValue <> "ALL" Then
                strwhere += " and kolejadmin.dbo.class_info.class_ID = '" & ddl_class.SelectedValue & "'"
            End If

            strwhere += " and kolejadmin.dbo.class_info.class_type = 'Compulsory'"

            strwhere += " and kolejadmin.dbo.student_level.student_Level = '" & ddl_level.SelectedValue & "'"

            strwhere += " and permatapintar.dbo.koko_kolejpermata.KokoID = '" & ddl_list.SelectedValue & "'"

            passSQL = tempSQL & strwhere & strorderby


        ElseIf ddl_exam.SelectedValue = "Exam 4" Or ddl_exam.SelectedValue = "Exam 7" Or ddl_exam.SelectedValue = "Exam 8" Or ddl_exam.SelectedValue = "Exam 12" Then

            tempSQL = " select distinct kolejadmin.dbo.student_info.std_ID,kolejadmin.dbo.student_info.student_Name,kolejadmin.dbo.student_info.student_ID,kolejadmin.dbo.class_info.class_Name,
                        kolejadmin.dbo.exam_info.exam_name,permatapintar.dbo.koko_kolejpermata.Nama,"

            If ddl_coccuriculum.SelectedValue = "1" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Sukan_JumlahP2 as Jumlah,permatapintar.dbo.koko_pelajar.Sukan_GredP2 as Gred"
            ElseIf ddl_coccuriculum.SelectedValue = "2" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Persatuan_JumlahP2 as Jumlah,permatapintar.dbo.koko_pelajar.Persatuan_GredP2 as Gred"
            ElseIf ddl_coccuriculum.SelectedValue = "3" Then
                tempSQL += "permatapintar.dbo.koko_pelajar.Uniform_JumlahP2 as Jumlah,permatapintar.dbo.koko_pelajar.Uniform_GredP2 as Gred"
            End If

            tempSQL += " from permatapintar.dbo.koko_pelajar
                        left join permatapintar.dbo.StudentProfile on permatapintar.dbo.koko_pelajar.StudentID = permatapintar.dbo.StudentProfile.StudentID"

            If ddl_coccuriculum.SelectedValue = "1" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.SukanID = permatapintar.dbo.koko_kolejpermata.KokoID"
            ElseIf ddl_coccuriculum.SelectedValue = "2" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.PersatuanID = permatapintar.dbo.koko_kolejpermata.KokoID"
            ElseIf ddl_coccuriculum.SelectedValue = "3" Then
                tempSQL += " Left Join permatapintar.dbo.koko_kolejpermata on permatapintar.dbo.koko_pelajar.UniformID = permatapintar.dbo.koko_kolejpermata.KokoID"
            End If

            tempSQL += " left join kolejadmin.dbo.student_info on permatapintar.dbo.StudentProfile.MYKAD = kolejadmin.dbo.student_info.student_Mykad
                        left join kolejadmin.dbo.student_level on kolejadmin.dbo.student_info.std_ID = kolejadmin.dbo.student_level.std_ID
                        left join kolejadmin.dbo.course on kolejadmin.dbo.student_info.std_ID = kolejadmin.dbo.course.std_ID
                        left join kolejadmin.dbo.class_info on kolejadmin.dbo.course.class_ID = kolejadmin.dbo.class_info.class_ID
                        left join kolejadmin.dbo.exam_result on kolejadmin.dbo.course.course_ID = kolejadmin.dbo.exam_result.course_ID
                        left join kolejadmin.dbo.exam_Info on kolejadmin.dbo.exam_result.exam_ID = kolejadmin.dbo.exam_info.exam_ID"

            strwhere = " where permatapintar.dbo.koko_pelajar.Tahun = '" & ddl_year.SelectedValue & "' 
                         and permatapintar.dbo.koko_kolejpermata.Tahun = '" & ddl_year.SelectedValue & "' 
                         and kolejadmin.dbo.student_level.year = '" & ddl_year.SelectedValue & "' 
                         and kolejadmin.dbo.course.year = '" & ddl_year.SelectedValue & "' 
                         and kolejadmin.dbo.class_info.class_year = '" & ddl_year.SelectedValue & "' 
                         and kolejadmin.dbo.exam_info.exam_Year = '" & ddl_year.SelectedValue & "'"

            strwhere += " and kolejadmin.dbo.exam_Info.exam_name ='" & ddl_exam.SelectedValue & "'"

            If ddl_class.SelectedValue <> "ALL" Then
                strwhere += " and kolejadmin.dbo.class_info.class_ID = '" & ddl_class.SelectedValue & "'"
            End If

            strwhere += " and kolejadmin.dbo.class_info.class_type = 'Compulsory'"

            strwhere += " and kolejadmin.dbo.student_level.student_Level = '" & ddl_level.SelectedValue & "'"

            strwhere += " and permatapintar.dbo.koko_kolejpermata.KokoID = '" & ddl_list.SelectedValue & "'"

            passSQL = tempSQL & strwhere & strorderby

        End If

        getSQL = passSQL

        Return getSQL


    End Function

End Class