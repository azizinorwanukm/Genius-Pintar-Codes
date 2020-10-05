Imports System.Data.SqlClient

Public Class course_Update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim result As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    subject_sem_Load()
                    subject_type_Load()
                    subject_StudentYear_Load()
                    year_Load()
                    subject_religions_Load()
                    subject_group_Load()

                    Subject_info_Load(Request.QueryString("subject_ID"))

                End If
            End If

        Catch ex As Exception
        End Try
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

        strSQL = "select distinct subject_year from subject_info"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        ddlsubject_year.DataSource = ds
        ddlsubject_year.DataTextField = "subject_year"
        ddlsubject_year.DataValueField = "subject_year"
        ddlsubject_year.DataBind()

    End Sub

    Private Sub Subject_info_Load(ByVal strcourse_ID As String)
        strSQL = "SELECT * from subject_info WHERE subject_ID ='" & strcourse_ID & "'"
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_Name")) Then
                    subject_Name.Text = ds.Tables(0).Rows(0).Item("subject_Name")
                Else
                    subject_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_NameBM")) Then
                    subject_NameBM.Text = ds.Tables(0).Rows(0).Item("subject_NameBM")
                Else
                    subject_NameBM.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("std_number")) Then
                    txtStdNumber.Text = ds.Tables(0).Rows(0).Item("std_number")
                Else
                    txtStdNumber.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_year")) Then
                    ddlsubject_year.SelectedValue = ds.Tables(0).Rows(0).Item("subject_year")
                Else
                    ddlsubject_year.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_StudentYear")) Then
                    subject_StudentYear.SelectedValue = ds.Tables(0).Rows(0).Item("subject_StudentYear")
                Else
                    subject_StudentYear.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_code")) Then
                    subject_Code.Text = ds.Tables(0).Rows(0).Item("subject_code")
                Else
                    subject_Code.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_type")) Then
                    subject_type.SelectedValue = ds.Tables(0).Rows(0).Item("subject_type")
                Else
                    subject_type.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_sem")) Then
                    subject_sem.SelectedValue = ds.Tables(0).Rows(0).Item("subject_sem")
                Else
                    subject_sem.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_CreditHour")) Then
                    subject_credithour.Text = ds.Tables(0).Rows(0).Item("subject_CreditHour")
                Else
                    subject_credithour.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("course_Name")) Then
                    ddlCourse_group.SelectedValue = ds.Tables(0).Rows(0).Item("course_Name")
                Else
                    ddlCourse_group.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_religions")) Then
                    ddl_subjectreligions.SelectedValue = ds.Tables(0).Rows(0).Item("subject_religions")
                Else
                    ddl_subjectreligions.SelectedValue = ""
                End If

            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        'UPDATE
        strSQL = "UPDATE subject_info SET subject_Name='" & subject_Name.Text & "',
                    subject_NameBM='" & subject_NameBM.Text & "',subject_code='" & subject_code.Text & "',
                    subject_year='" & ddlsubject_year.SelectedValue & "',subject_type='" & subject_type.SelectedValue & "',
                    subject_StudentYear='" & subject_StudentYear.SelectedValue & "',subject_sem='" & subject_sem.SelectedValue & "',
                    subject_CreditHour='" & subject_credithour.Text & "',std_number='" & txtStdNumber.Text & "',
                    course_Name = '" & ddlCourse_group.SelectedValue & "' WHERE subject_ID ='" & Request.QueryString("subject_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            errorCount = 0
        Else
            errorCount = 1
        End If

        If errorCount = 1 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_pengurusan_am_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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
        subject_sem.DataValueField = "Value"
        subject_sem.DataBind()
        subject_sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        subject_sem.SelectedIndex = 0

    End Sub

End Class