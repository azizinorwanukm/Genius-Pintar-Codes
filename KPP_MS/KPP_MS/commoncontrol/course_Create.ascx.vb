Imports System.Data.SqlClient

Public Class course_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    year_Load()
                    subject_type_Load()
                    subject_sem_Load()
                    subject_StudentYear_Load()
                    subject_religions_Load()
                    subject_group_Load()

                    Load_Page()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCourseCreate_ServerClick(sender As Object, e As EventArgs) Handles btnCourseCreate.ServerClick
        Dim errorCount As Integer = 0

        If subject_Name.Text <> "" And Regex.IsMatch(subject_Name.Text, "^[A-Za-z0-9 ]+$") Then

            If subject_code.Text <> "" And Regex.IsMatch(subject_code.Text, "^[A-Za-z0-9]+$") Then

                If ddlsubject_year.SelectedValue <> "" Then

                    If subject_type.SelectedValue <> "" And subject_type.SelectedValue = "Compulsory" Or subject_type.SelectedValue = "Electives" Or subject_type.SelectedValue = "Choose" Then

                        If subject_StudentYear.SelectedValue <> "" And subject_StudentYear.SelectedValue = "Foundation 1" Or subject_StudentYear.SelectedValue = "Foundation 2" Or subject_StudentYear.SelectedValue = "Foundation 3" Or subject_StudentYear.SelectedValue = "Level 1" Or subject_StudentYear.SelectedValue = "Level 2" Then

                            If subject_sem.SelectedValue <> "" Then

                                If ddl_subjectreligions.SelectedValue <> "" Then

                                    If ddlCourse_group.SelectedValue <> "" Then

                                        Using STDDATA As New SqlCommand("INSERT INTO subject_info(subject_Name,subject_NameBM,subject_code,subject_year,subject_type,subject_StudentYear,subject_sem,subject_religions,course_Name,subject_CreditHour) 
                                                                        values ('" & subject_Name.Text & "','" & subject_NameBM.Text & "','" & subject_code.Text & "','" & ddlsubject_year.SelectedValue & "','" & subject_type.SelectedValue & "',
                                                                        '" & subject_StudentYear.SelectedValue & "','" & subject_sem.SelectedValue & "','" & ddl_subjectreligions.SelectedValue & "','" & ddlCourse_group.SelectedValue & "','" & subject_CreditHour.Text & "')", objConn)
                                            objConn.Open()
                                            Dim i = STDDATA.ExecuteNonQuery()
                                            objConn.Close()

                                            If i <> 0 Then
                                                errorCount = 0
                                            Else
                                                errorCount = 1
                                            End If
                                        End Using

                                    Else
                                        ''Error Course Group (please select course group)
                                        errorCount = 9
                                    End If

                                Else
                                    ''Error Religions (please select religions)
                                    errorCount = 8
                                End If

                            Else
                                ''Error Semester (please select semester)
                                errorCount = 7
                            End If
                        Else
                            errorCount = 6
                        End If
                    Else
                        errorCount = 5
                    End If
                Else
                    errorCount = 4
                End If
            Else
                errorCount = 3
            End If
        Else
            errorCount = 2
        End If

        If errorCount = 1 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 6 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 7 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 8 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 9 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))
        End If

    End Sub

    Private Sub Load_Page()
        ''student_info
        strSQL = "select * from setting where Type = 'Year' and Value = '" & Now.Year & "'"

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
                ddlsubject_year.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlsubject_year.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub subject_religions_Load()
        Try

            Dim strLevelSql As String = "Select subject_id from subject_info where subject_id is null"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_subjectreligions.DataSource = levds
            ddl_subjectreligions.DataValueField = "subject_id"
            ddl_subjectreligions.DataTextField = "subject_id"
            ddl_subjectreligions.DataBind()
            ddl_subjectreligions.Items.Insert(0, New ListItem("Select Course Religion", String.Empty))
            ddl_subjectreligions.Items.Insert(1, New ListItem("ALL", "ALL"))
            ddl_subjectreligions.Items.Insert(2, New ListItem("ISLAM", "ISLAM"))
            ddl_subjectreligions.Items.Insert(3, New ListItem("OTHERS", "OTHERS"))
            ddl_subjectreligions.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subject_type_Load()
        strSQL = "select * from setting where Type = 'Subject Type'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        subject_type.DataSource = ds
        subject_type.DataTextField = "Parameter"
        subject_type.DataValueField = "Parameter"
        subject_type.DataBind()
        subject_type.Items.Insert(0, New ListItem("Select Course Type", String.Empty))
        subject_type.SelectedIndex = 0
    End Sub

    Private Sub subject_StudentYear_Load()
        strSQL = "select * from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        subject_StudentYear.DataSource = ds
        subject_StudentYear.DataTextField = "Parameter"
        subject_StudentYear.DataValueField = "Parameter"
        subject_StudentYear.DataBind()
        subject_StudentYear.Items.Insert(0, New ListItem("Select Student Year", String.Empty))
        subject_StudentYear.SelectedIndex = 0

    End Sub

    Private Sub subject_sem_Load()
        strSQL = "select * from setting where Type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        subject_sem.DataSource = ds
        subject_sem.DataTextField = "Parameter"
        subject_sem.DataValueField = "Parameter"
        subject_sem.DataBind()
        subject_sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        subject_sem.SelectedIndex = 0

    End Sub

    Private Sub subject_group_Load()

        strSQL = "select * from setting where idx = 'Courses Group'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        ddlCourse_group.DataSource = ds
        ddlCourse_group.DataTextField = "Parameter"
        ddlCourse_group.DataValueField = "Parameter"
        ddlCourse_group.DataBind()
        ddlCourse_group.Items.Insert(0, New ListItem("Select Course Group", "NULL"))
        ddlCourse_group.SelectedIndex = 0

    End Sub

    Private Sub year_Load()

        strSQL = "select * from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        ddlsubject_year.DataSource = ds
        ddlsubject_year.DataTextField = "Parameter"
        ddlsubject_year.DataValueField = "Parameter"
        ddlsubject_year.DataBind()
        ddlsubject_year.Items.Insert(0, New ListItem("Select Year", "NULL"))
        ddlsubject_year.SelectedIndex = 0

    End Sub
End Class