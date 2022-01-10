Imports System.Data.SqlClient

Public Class class_transfer
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

                Dim data As String = oCommon.securityLogin(Request.QueryString("admin_ID"))

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
        strSQL = "SELECT MAX(Parameter) FROM setting WHERE Type = 'Year' "

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

            If ddl_type.SelectedValue = "Compulsory" Then
                datRespondent.Columns(5).Visible = False
            Else
                datRespondent.Columns(5).Visible = True
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function


    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY class_Name ASC"

        tmpSQL = "Select * From class_info"
        strWhere += " WHERE class_ID IS NOT NULL "

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " AND class_level = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_type.SelectedIndex > 0 Then
            strWhere += " AND class_type = '" & ddl_type.SelectedValue & "'"

            If ddl_type.SelectedValue = "Electives" And ddl_Sem.SelectedIndex > 0 Then
                strWhere += " AND class_sem = '" & ddl_Sem.SelectedValue & "'"
            End If
        End If

        If ddl_Year.SelectedIndex > 0 Then
            strWhere += " AND class_year = '" & ddl_Year.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Private Sub year_load()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Year'"
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
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Level'"
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
            Dim strLevelSql As String = "select Parameter from setting where Type = 'Class Type'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_type.DataSource = levds
            ddl_type.DataValueField = "Parameter"
            ddl_type.DataTextField = "Parameter"
            ddl_type.DataBind()
            ddl_type.Items.Insert(0, New ListItem("Select Class Type", String.Empty))
            ddl_type.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Sem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_type.SelectedIndexChanged
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

                ''get class_ID
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim find_old_subjectCode As String = ""
                    Dim get_old_subjectCode As String = ""
                    Dim find_new_subjectID As String = ""
                    Dim get_new_subjectID As String = ""
                    Dim find_old_className As String = ""
                    Dim get_old_className As String = ""
                    Dim find_new_className As String = ""
                    Dim get_new_className As String = ""

                    Dim set_classLevel As String = ""
                    Dim set_classSem As String = ""
                    Dim set_className As String = ""

                    Dim answer As Char
                    Dim answerInt As Integer = 0

                    If ddl_type.SelectedValue = "Electives" Then

                        find_old_subjectCode = "select subject_code from subject_info left join class_info on subject_info.subject_ID = class_info.subject_ID
                                                where class_info.class_year = '" & ddl_Year.SelectedValue & "' and class_info.class_type = '" & ddl_type.SelectedValue & "' and class_info.class_ID = '" & strKey & "'"
                        get_old_subjectCode = oCommon.getFieldValue(find_old_subjectCode)

                        ''get subject ID
                        find_new_subjectID = "select subject_ID from subject_info where subject_year = '" & ddlyear_Transfer.SelectedValue & "' and subject_code = '" & get_old_subjectCode & "'"
                        get_new_subjectID = oCommon.getFieldValue(find_new_subjectID)

                        ''set a class sem
                        set_classSem = ddl_Sem.SelectedValue

                        find_old_className = "select class_Name from class_info where class_ID = '" & strKey & "'"
                        get_old_className = oCommon.getFieldValue(find_old_className)

                    Else

                        find_old_className = "select class_Name from class_info where class_ID = '" & strKey & "'"
                        get_old_className = oCommon.getFieldValue(find_old_className)

                    End If

                    ''get first character of string & convert to integer format
                    answer = get_old_className.Chars(0)
                    answerInt = Integer.Parse(answer)

                    ''convert the answer to = answerInt + 1
                    If answerInt = 1 Then
                        answerInt += 1
                    ElseIf answerInt = 2 Then
                        answerInt += 1
                    ElseIf answerInt = 3 Then
                        answerInt += 1
                    ElseIf answerInt = 4 Then
                        answerInt += 1
                    End If

                    ''combine character
                    get_new_className = answerInt & get_old_className.Remove(0, 1)

                    ''change old class level to new class level
                    If ddl_Level.SelectedValue = "Foundation 1" Then
                        set_classLevel = "Foundation 2"
                    ElseIf ddl_Level.SelectedValue = "Foundation 2" Then
                        set_classLevel = "Foundation 3"
                    ElseIf ddl_Level.SelectedValue = "Foundation 3" Then
                        set_classLevel = "Level 1"
                    ElseIf ddl_Level.SelectedValue = "Level 1" Then
                        set_classLevel = "Level 2"
                    End If

                    ''check if the new class name is exist in database
                    Dim go_checking As String = "select class_ID from class_info where class_year = '" & ddlyear_Transfer.SelectedValue & "' and class_Name = '" & get_new_className & "'"
                    Dim go_confirm As String = oCommon.getFieldValue(go_checking)

                    If go_confirm.Length = 0 Then

                        Using PJGDATA As New SqlCommand("INSERT INTO class_info(class_Name, class_Level, class_sem, class_Year, class_type, subject_ID) 
                                                         VALUES('" & get_new_className & "','" & set_classLevel & "','" & set_classSem & "','" & ddlyear_Transfer.SelectedValue & "','" & ddl_type.SelectedValue & "','" & get_new_subjectID & "')", objConn)
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

                    If ddl_type.SelectedValue = "Compulsory" Then

                        Dim go_checking_permata As String = "select Kelas from koko_kelas where Tahun = '" & ddlyear_Transfer.SelectedValue & "' and Kelas = '" & get_new_className & "'"
                        Dim go_confirm_permata As String = oCommon.getFieldValue_Permata(go_checking_permata)

                        If go_confirm.Length = 0 Then
                            ''Insert into koko_kelas in permatapintar database
                            strSQL = "insert into koko_kelas(Kelas,Tahun) values('" & get_new_className & "','" & ddlyear_Transfer.SelectedValue & "')"
                            strRet = oCommon.ExecuteSQLPermata(strSQL)
                        End If

                    End If
                End If
            End If
        Next

        If errorCount = 1 Then
            Response.Redirect("admin_daftar_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&result=" & errorCount)
        Else
            Response.Redirect("admin_daftar_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&result=" & errorCount)
        End If

    End Sub
End Class