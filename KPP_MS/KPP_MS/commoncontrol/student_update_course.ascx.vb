Imports System.Data.SqlClient

Public Class student_update_course
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

                ddlYear()
                ddlLevel()
                load_page()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT year from student_Level where year ='" & Now.Year & "'"

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

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
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
                  student_Level.student_Sem
                  From student_info 
                  left join student_Level on student_info.std_ID=student_Level.std_ID
                  left join course on student_info.std_ID= course.std_ID "
        strWhere = " WHERE student_info.std_ID Is Not NULL"
        strWhere += " And course.year = '" & ddl_year.SelectedValue & "'"
        strWhere += " And student_level.year = '" & ddl_year.SelectedValue & "'"
        strWhere += " And student_Level.student_Level = '" & ddl_level.SelectedValue & "'"
        strWhere += " And student_Level.student_Sem = '" & ddl_sem.SelectedValue & "'"

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " And (student_info.student_ID Like '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent.Text & "%' )"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL

    End Function

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

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            subject_list()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlSem()
        Try
            strSQL = "select distinct subject_sem from subject_info where subject_StudentYear ='" & ddl_level.SelectedValue & "' and subject_year = '" & ddl_year.SelectedValue & "'"
            ''strSQL += " And subject_Year = '" & ddl_year.SelectedValue & "'"

            Debug.WriteLine(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_sem.DataSource = ds
            ddl_sem.DataTextField = "subject_sem"
            ddl_sem.DataValueField = "subject_sem"
            ddl_sem.DataBind()
            ddl_sem.Items.Insert(0, New ListItem("Select Student Sem", String.Empty))
            ddl_sem.SelectedIndex = 0
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

    Protected Sub subject_list()
        Try
            Dim stryear As String = "Select subject_ID, subject_Name from subject_info where subject_StudentYear = '" & ddl_level.SelectedValue & "' and subject_sem = '" & ddl_sem.SelectedValue & "' and subject_year = '" & ddl_year.SelectedValue & "'"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddlCourseChoose.DataSource = yrds
            ddlCourseChoose.DataValueField = "subject_ID"
            ddlCourseChoose.DataTextField = "subject_Name"
            ddlCourseChoose.DataBind()
            ddlCourseChoose.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourseChoose.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub class_list_data()
        Try
            Dim check As String = "select subject_ID from class_info where subject_ID = '" & ddlCourseChoose.SelectedValue & "'"
            Dim data_check As String = oCommon.getFieldValue(check)

            If data_check = "" Then

                Dim stryear As String = "Select class_ID, class_Name from class_info where class_level = '" & ddl_level.SelectedValue & "' and class_type = 'Compulsory' and class_year = '" & ddl_year.SelectedValue & "'"
                Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

                Dim yrds As DataSet = New DataSet
                sqlYearDA.Fill(yrds, "YrTable")

                ddlClassChose.DataSource = yrds
                ddlClassChose.DataValueField = "class_ID"
                ddlClassChose.DataTextField = "class_Name"
                ddlClassChose.DataBind()
                ddlClassChose.Items.Insert(0, New ListItem("Select Class", String.Empty))
                ddlClassChose.SelectedIndex = 0

            Else

                Dim stryear As String = "Select class_ID, class_Name from class_info where class_level = '" & ddl_level.SelectedValue & "' and subject_ID = '" & ddlCourseChoose.SelectedValue & "'  and class_year = '" & ddl_year.SelectedValue & "'"
                Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

                Dim yrds As DataSet = New DataSet
                sqlYearDA.Fill(yrds, "YrTable")

                ddlClassChose.DataSource = yrds
                ddlClassChose.DataValueField = "class_ID"
                ddlClassChose.DataTextField = "class_Name"
                ddlClassChose.DataBind()
                ddlClassChose.Items.Insert(0, New ListItem("Select Class", String.Empty))
                ddlClassChose.SelectedIndex = 0

            End If

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
            ddl_level.Items.Insert(0, New ListItem("Select Student Level", String.Empty))
            ddl_level.SelectedIndex = 0

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub ddl_level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged
        Try
            ddlSem()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlCourseChoose_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseChoose.SelectedIndexChanged
        Try
            class_list_data()
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

                    'Insert
                    Using CLASSDATA As New SqlCommand("INSERT into course(std_ID,class_ID,subject_ID,year) 
                                                       values ('" & strKey & "','" & ddlClassChose.SelectedValue & "','" & ddlCourseChoose.SelectedValue & "','" & ddl_year.SelectedValue & "')", objConn)
                        objConn.Open()
                        Dim j = CLASSDATA.ExecuteNonQuery()
                        objConn.Close()

                        If j <> 0 Then
                            errorCount = 0
                        Else
                            errorCount = 1
                        End If
                    End Using

                End If
            End If
        Next


        If errorCount = 1 Then
            ShowMessage("Register course", MessageType.Success)
        ElseIf errorCount = 0 Then
            ShowMessage("Register course", MessageType.Error)
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class