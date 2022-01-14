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

                    subject_type_Load()
                    subject_sem_Load()
                    subject_StudentYear_Load()
                    subject_religions_Load()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCourseCreate_ServerClick(sender As Object, e As EventArgs) Handles btnCourseCreate.ServerClick
        Dim errorCount As Integer = 0

        If subject_Name.Text <> "" And Regex.IsMatch(subject_Name.Text, "^[A-Za-z ]+$") Then

            If subject_code.Text <> "" And Regex.IsMatch(subject_code.Text, "^[A-Za-z0-9]+$") Then

                If subject_year.Text <> "" And IsNumeric(subject_year.Text) And Regex.IsMatch(subject_year.Text, "^[0-9]+$") Then

                    If subject_type.SelectedValue <> "" And subject_type.SelectedValue = "Compulsory" Or subject_type.SelectedValue = "Electives" Or subject_type.SelectedValue = "Choose" Then

                        If subject_StudentYear.SelectedValue <> "" And subject_StudentYear.SelectedValue = "Foundation 1" Or subject_StudentYear.SelectedValue = "Foundation 2" Or subject_StudentYear.SelectedValue = "Foundation 3" Or subject_StudentYear.SelectedValue = "Level 1" Or subject_StudentYear.SelectedValue = "Level 2" Then

                            Using STDDATA As New SqlCommand("INSERT INTO subject_info(subject_Name,subject_NameBM,subject_code,subject_year,subject_type,subject_StudentYear,subject_sem,subject_religions) values ('" & subject_Name.Text & "','" & subject_NameBM.Text & "','" & subject_code.Text & "','" & subject_year.Text & "','" & subject_type.SelectedValue & "','" & subject_StudentYear.SelectedValue & "','" & subject_sem.SelectedValue & "','" & ddl_subjectreligions.SelectedValue & "')", objConn)
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
            ddl_subjectreligions.Items.Insert(0, New ListItem("ALL", "ALL"))
            ddl_subjectreligions.Items.Insert(0, New ListItem("ISLAM", "ISLAM"))
            ddl_subjectreligions.Items.Insert(0, New ListItem("OTHERS", "OTHERS"))
            ddl_subjectreligions.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Function fillDDL(columnName As String) As DataTable

        Dim query As String = ""
        Dim dt As New DataTable
        query += "SELECT DISTINCT " & columnName & " FROM subject_info WHERE subject_ID IS NOT NULL AND subject_Name is not null"
        Dim sqlAdapter As New SqlDataAdapter(query, strConn)
        sqlAdapter.Fill(dt)

        Return dt
    End Function

    Private Sub subject_type_Load()
        subject_type.DataSource = Me.fillDDL("subject_type")
        subject_type.DataTextField = "subject_type"
        subject_type.DataValueField = "subject_type"
        subject_type.DataBind()
        subject_type.Items.Insert(0, New ListItem("Student Year", String.Empty))
    End Sub

    Private Sub subject_StudentYear_Load()
        subject_StudentYear.DataSource = Me.fillDDL("subject_StudentYear")
        subject_StudentYear.DataTextField = "subject_StudentYear"
        subject_StudentYear.DataValueField = "subject_StudentYear"
        subject_StudentYear.DataBind()
        subject_StudentYear.Items.Insert(0, New ListItem("Student Year", String.Empty))
    End Sub

    Private Sub subject_sem_Load()
        subject_sem.DataSource = Me.fillDDL("subject_sem")
        subject_sem.DataTextField = "subject_sem"
        subject_sem.DataValueField = "subject_sem"
        subject_sem.DataBind()
        subject_sem.Items.Insert(0, New ListItem("Student Year", String.Empty))
    End Sub
End Class