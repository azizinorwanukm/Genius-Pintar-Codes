Imports System.Data.SqlClient

Public Class class_Create
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

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then
                    staff_info_list()

                    course_Name.Enabled = False

                    class_Level.DataSource = getDT("class_Level")
                    class_Level.DataTextField = "class_Level"
                    class_Level.DataValueField = "class_Level"
                    class_Level.DataBind()
                    class_Level.Items.Insert(0, New ListItem("Select Class Level", String.Empty))

                    class_type_load()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub class_type_load()
        strSQL = "SELECT * from class_info where class_ID is NULL"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            class_Type.DataSource = ds
            class_Type.DataTextField = "class_Type"
            class_Type.DataValueField = "class_Type"
            class_Type.DataBind()
            class_Type.Items.Insert(0, New ListItem("Select Class Type", String.Empty))
            class_Type.Items.Insert(1, New ListItem("Compulsory", "Compulsory"))
            class_Type.Items.Insert(2, New ListItem("Electives", "Electives"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staff_info_list()
        strSQL = "SELECT stf_ID,staff_Name FROM staff_Info Where staff_Year = '" & Now.Year & "' order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_ID.DataSource = ds
            staff_ID.DataTextField = "staff_Name"
            staff_ID.DataValueField = "stf_ID"
            staff_ID.DataBind()
            staff_ID.Items.Insert(0, New ListItem("Select Staff", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnClassCreate_ServerClick(sender As Object, e As EventArgs) Handles btnClassCreate.ServerClick
        Dim errorCount As Integer = 0

        If class_Name.Text <> "" And Regex.IsMatch(class_Name.Text, "^[A-Za-z0-9]+$") Then

            If class_year.Text <> "" And Regex.IsMatch(class_year.Text, "^[0-9]+$") And IsNumeric(class_year.Text) Then

                If class_Level.SelectedValue <> "" And class_Level.SelectedValue = "Foundation 1" Or class_Level.SelectedValue = "Foundation 2" Or class_Level.SelectedValue = "Foundation 3" Or class_Level.SelectedValue = "Level 1" Or class_Level.Text = "Level 2" Then

                    If staff_ID.SelectedValue <> "" And Regex.IsMatch(class_Name.Text, "^[A-Za-z0-9]+$") Then

                        If class_Type.SelectedValue <> "" Then

                            If course_Name.SelectedValue <> "" Then

                                'Insert
                                Using CLASSDATA As New SqlCommand("INSERT class_info(class_Name,class_year,class_Level,stf_ID,class_Type,subject_ID) values ('" & class_Name.Text & "','" & class_year.Text & "','" & class_Level.Text & "','" & staff_ID.SelectedValue & "','" & class_Type.SelectedValue & "','" & course_Name.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim i = CLASSDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        errorCount = 0
                                    Else
                                        errorCount = 1
                                    End If
                                End Using

                            End If

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
            Response.Redirect("admin_pengurusan_am_kelas.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_pengurusan_am_kelas.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_pengurusan_am_kelas.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_pengurusan_am_kelas.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_pengurusan_am_kelas.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_pengurusan_am_kelas.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Protected Sub class_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles class_Type.SelectedIndexChanged
        Try

            If class_Type.SelectedValue = "Electives" Then
                course_Name.Enabled = True

                courseName_Load()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub courseName_Load()
        strSQL = "SELECT subject_Name ,subject_ID FROM subject_info Where subject_year = '" & Now.Year & "' and subject_type != 'Compulsory' and subject_StudentYear = '" & class_Level.SelectedValue & "' order by subject_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            course_Name.DataSource = ds
            course_Name.DataTextField = "subject_Name"
            course_Name.DataValueField = "subject_ID"
            course_Name.DataBind()
            course_Name.Items.Insert(0, New ListItem("Select Course", "NULL"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function getDT(columnName As String) As DataTable
        Dim dt As New DataTable
        Dim query As String = ""
        query += "SELECT DISTINCT " & columnName & ""
        query += " FROM class_info "
        query += " WHERE class_ID IS NOT NULL"
        Dim sqlAdapter As New SqlDataAdapter(query, strConn)
        sqlAdapter.Fill(dt)
        Return dt
    End Function
End Class