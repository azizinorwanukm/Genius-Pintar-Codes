Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class student_view_course
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = ""
                id = Request.QueryString("admin_ID")
                year_list()
                student_Level()
                student_Sem()



                ''get a user access
                Dim userAccess As String = ""
                userAccess = "select staff_Position from staff_Info where stf_ID = '" & id & "'"
                Dim access As String = getFieldValue(userAccess, strConn)
                hiddenAccess.Value = access

                load_page()
                student_Class()
                strRet = BindData(datRespondent)
                ''Generate_Table()
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
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub student_Class()
        strSQL = "SELECT class_ID, class_Name from class_info where class_year = '" & ddlYear.SelectedValue & "' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClass.DataSource = ds
            ddlClass.DataTextField = "class_Name"
            ddlClass.DataValueField = "class_ID"
            ddlClass.DataBind()
            ddlClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Sem()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSem.DataSource = ds
            ddlSem.DataTextField = "Parameter"
            ddlSem.DataValueField = "Parameter"
            ddlSem.DataBind()
            ddlSem.Items.Insert(0, New ListItem("Select Sem", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String
        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
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

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

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
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "Select distinct course.course_ID,
                  student_info.student_Name,
                  student_Level.student_Level,
                  student_Level.student_Sem,
                  class_info.class_Name,
                  subject_info.subject_Name
                  From student_info 
                  left join student_Level on student_info.std_ID=student_Level.std_ID
                  left join course on student_info.std_ID= course.std_ID
                  left join subject_info on course.subject_ID=subject_info.subject_ID
                  left join class_info on course.class_ID= class_info.class_ID"
        strWhere = " WHERE student_info.std_ID IS NOT NULL"
        strWhere += " and course.year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and student_level.year = '" & ddlYear.SelectedValue & "'"

        If ddlCourse.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_ID = '" & ddlCourse.SelectedValue & "'"
        End If

        If ddlSem.SelectedIndex > 0 Then
            strWhere += " AND student_level.student_Sem = '" & ddlSem.SelectedValue & "'"
        End If

        If ddlLevel.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_StudentYear = '" & ddlLevel.SelectedValue & "'"
        End If

        If ddlClass.SelectedIndex > 0 Then
            strWhere += " AND class_info.class_ID = '" & ddlClass.SelectedValue & "'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " And ( student_info.student_Name like '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_ID like '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad like '%" & txtstudent.Text & "%' )"
        End If
        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            student_Class()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSem.SelectedIndexChanged
        If ddlSem.SelectedValue <> "0" Then

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim STDLEVEL As New SqlDataAdapter()

            strSQL = "select * from subject_info where subject_sem ='" & ddlSem.SelectedValue & "'"
            strSQL += " And subject_year = '" & ddlYear.SelectedValue & "'"

            Debug.WriteLine(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourse.DataSource = ds
            ddlCourse.DataTextField = "subject_Name"
            ddlCourse.DataValueField = "subject_ID"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourse.SelectedIndex = 0

        End If

        BindData(datRespondent)

    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        If ddlLevel.SelectedValue <> "" Then

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim STDLEVEL As New SqlDataAdapter()

            strSQL = "select * from subject_info where subject_Sem ='" & ddlSem.SelectedValue & "'"
            strSQL += " And subject_year = '" & ddlYear.SelectedValue & "'"
            strSQL += " And subject_StudentYear= '" & ddlLevel.SelectedValue & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourse.DataSource = ds
            ddlCourse.DataTextField = "subject_Name"
            ddlCourse.DataValueField = "subject_ID"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourse.SelectedIndex = 0


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            strSQL = "SELECT class_ID, class_Name from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_Level = '" & ddlLevel.SelectedValue & "' "

            Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

            Dim dsA As DataSet = New DataSet
            sqlDB.Fill(dsA, "AnyTable")

            ddlClass.DataSource = dsA
            ddlClass.DataTextField = "class_Name"
            ddlClass.DataValueField = "class_ID"
            ddlClass.DataBind()
            ddlClass.Items.Insert(0, New ListItem("Select Class", String.Empty))

        End If

        BindData(datRespondent)

    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try

            Dim userAccess As String = "select course_ID from course 
                                        where course_ID = '" & strKeyName & "'"
            Dim access As String = getFieldValue(userAccess, strConn)

            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete course where course_ID ='" & access & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_pelajar_data.aspx?std_ID=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        strRet = BindData(datRespondent)
    End Sub
End Class