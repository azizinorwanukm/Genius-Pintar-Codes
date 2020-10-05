Imports System.Data.SqlClient

Public Class course_Transfer
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

                    year_load()
                    student_year_load()
                    student_sem_load()
                    subject_type_load()

                    Page_Load()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Page_Load()
        ''student_info
        strSQL = "select * from setting where Type = 'Year' and Value = '" & Now.Year & "'"

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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_StudentYear ASC"

        tmpSQL = "Select * From subject_info"
        strWhere += " WHERE subject_ID IS NOT NULL AND subject_Name is not null"

        If ddl_Sem.SelectedIndex > 0 Then
            strWhere += " AND subject_sem = '" & ddl_Sem.SelectedValue & "'"
        End If

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " AND subject_StudentYear = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_type.SelectedIndex > 0 Then
            strWhere += " AND subject_type = '" & ddl_type.SelectedValue & "'"
        End If

        If ddl_Year.SelectedIndex > 0 Then
            strWhere += " AND subject_year = '" & ddl_Year.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Private Sub year_load()
        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Year'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Year.DataSource = levds
            ddl_Year.DataValueField = "Parameter"
            ddl_Year.DataTextField = "Parameter"
            ddl_Year.DataBind()
            ddl_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_Year.SelectedIndex = 0

            ddlyear_Transfer.DataSource = levds
            ddlyear_Transfer.DataValueField = "Parameter"
            ddlyear_Transfer.DataTextField = "Parameter"
            ddlyear_Transfer.DataBind()
            ddlyear_Transfer.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlyear_Transfer.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_year_load()
        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Level.DataSource = levds
            ddl_Level.DataValueField = "Parameter"
            ddl_Level.DataTextField = "Parameter"
            ddl_Level.DataBind()
            ddl_Level.Items.Insert(0, New ListItem("Select Student Level", String.Empty))
            ddl_Level.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_sem_load()
        Try
            Dim strLevelSql As String = "Select * from setting where Type = 'Sem'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_Sem.DataSource = levds
            ddl_Sem.DataValueField = "Value"
            ddl_Sem.DataTextField = "Parameter"
            ddl_Sem.DataBind()
            ddl_Sem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddl_Sem.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subject_type_load()
        Try
            Dim strLevelSql As String = "select * from setting where Type = 'Subject Type'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_type.DataSource = levds
            ddl_type.DataValueField = "Parameter"
            ddl_type.DataTextField = "Parameter"
            ddl_type.DataBind()
            ddl_type.Items.Insert(0, New ListItem("Select Course Type", String.Empty))
            ddl_type.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim i As Integer
        Dim errorCount As Integer = 0

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim get_code As String = "select subject_code from subject_info where subject_ID = '" & strKey & "'"
                    Dim check_code As String = oCommon.getFieldValue(get_code)

                    Dim find_subjectID As String = "select subject_ID from subject_info where subject_code = '" & check_code & "' and subject_year = '" & ddlyear_Transfer.SelectedValue & "'"
                    Dim check_subjectID As String = oCommon.getFieldValue(find_subjectID)

                    Dim find_subjectName As String = "select subject_Name from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectName As String = oCommon.getFieldValue(find_subjectName)
                    Dim find_subjectNameBM As String = "select subject_NameBM from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectNameBM As String = oCommon.getFieldValue(find_subjectNameBM)

                    Dim find_subjectReligions As String = "select subject_religions from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectReligions As String = oCommon.getFieldValue(find_subjectReligions)
                    Dim find_subjectLevel As String = "select subject_StudentYear from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectLevel As String = oCommon.getFieldValue(find_subjectLevel)
                    Dim find_subjectHour As String = "select subject_CreditHour from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectHour As String = oCommon.getFieldValue(find_subjectHour)
                    Dim find_subjectSem As String = "select subject_sem from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectSem As String = oCommon.getFieldValue(find_subjectSem)
                    Dim find_subjectCourseName As String = "select course_Name from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectCourseName As String = oCommon.getFieldValue(find_subjectCourseName)
                    Dim find_subjectType As String = "select subject_type from subject_info where subject_ID = '" & strKey & "'"
                    Dim get_subjectType As String = oCommon.getFieldValue(find_subjectType)

                    If check_subjectID.Length = 0 Then

                        Using PJGDATA As New SqlCommand("INSERT INTO subject_info(subject_Name, subject_NameBM, subject_code, subject_year, subject_type, subject_religions, subject_StudentYear, subject_CreditHour, subject_sem, course_Name)
                                                         Values('" & get_subjectName & "','" & get_subjectNameBM & "','" & check_code & "','" & ddlyear_Transfer.SelectedValue & "','" & get_subjectType & "','" & get_subjectReligions & "',
                                                         '" & get_subjectLevel & "','" & Integer.Parse(get_subjectHour) & "','" & get_subjectSem & "','" & get_subjectCourseName & "')", objConn)
                            objConn.Open()
                            Dim j = PJGDATA.ExecuteNonQuery()
                            objConn.Close()
                            If j <> 0 Then
                                errorCount = 1
                            Else
                                errorCount = 2
                            End If
                        End Using

                    Else

                    End If


                End If
            End If
        Next

        If errorCount = 1 Then
            Response.Redirect("admin_daftar_kursus_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&result=" & errorCount)
        Else
            Response.Redirect("admin_daftar_kursus_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&result=" & errorCount)
        End If

    End Sub
End Class