Imports System.Data.SqlClient

Public Class course_List_Table
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
                filterYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                filterYear.SelectedValue = ""
            End If
        End If
    End Sub

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

    Private Sub year_load()
        Try

            Dim strLevelSql As String = "Select * from setting where Type = 'Year'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterYear.DataSource = levds
            filterYear.DataValueField = "Parameter"
            filterYear.DataTextField = "Parameter"
            filterYear.DataBind()
            filterYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            filterYear.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_year_load()
        Try

            Dim strLevelSql As String = "Select * from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterFoundationLvl.DataSource = levds
            filterFoundationLvl.DataValueField = "Parameter"
            filterFoundationLvl.DataTextField = "Parameter"
            filterFoundationLvl.DataBind()
            filterFoundationLvl.Items.Insert(0, New ListItem("Select Student Level", String.Empty))
            filterFoundationLvl.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub student_sem_load()
        Try

            Dim strLevelSql As String = "Select * from setting where Type = 'Sem'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterSems.DataSource = levds
            filterSems.DataValueField = "Value"
            filterSems.DataTextField = "Parameter"
            filterSems.DataBind()
            filterSems.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            filterSems.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subject_type_load()
        Try

            Dim strLevelSql As String = "select * from setting where Type = 'Subject Type'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            filterType.DataSource = levds
            filterType.DataValueField = "Parameter"
            filterType.DataTextField = "Parameter"
            filterType.DataBind()
            filterType.Items.Insert(0, New ListItem("Select Course Type", String.Empty))
            filterType.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete subject_info where subject_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_StudentYear ASC"

        tmpSQL = "Select * From subject_info"
        strWhere += " WHERE subject_ID IS NOT NULL AND subject_Name is not null"


        ''--debug
        If filterSems.SelectedIndex > 0 Then
            strWhere += " AND subject_sem = '" & filterSems.SelectedValue & "'"
        End If

        If filterFoundationLvl.SelectedIndex > 0 Then
            strWhere += " AND subject_StudentYear = '" & filterFoundationLvl.SelectedValue & "'"
        End If

        If filterType.SelectedIndex > 0 Then
            strWhere += " AND subject_type = '" & filterType.SelectedValue & "'"
        End If

        If filterYear.SelectedIndex > 0 Then
            strWhere += " AND subject_year = '" & filterYear.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyCode As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_kursus_data.aspx?subject_ID=" + strKeyCode + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub filterSems_Changed(sender As Object, e As EventArgs) Handles filterSems.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterFoundationLvl_Changed(sender As Object, e As EventArgs) Handles filterFoundationLvl.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterType_Changed(sender As Object, e As EventArgs) Handles filterType.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub filterYear_Changed(sender As Object, e As EventArgs) Handles filterYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnCourseSearch_ServerClick(sender As Object, e As EventArgs) Handles btnCourseSearch.ServerClick
        Dim query As String = ""
        Dim search As String = searchTextBox.Text

        Dim dt As New DataTable
        Dim dv As New DataView

        If Not String.IsNullOrEmpty(search) Then

            query = "SELECT * FROM subject_info "
            query += " WHERE subject_Name like '%" & search & "%'"
            query += " OR subject_code = '" & search & "'"

            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(query, strConn)
            myDataAdapter.SelectCommand.CommandTimeout = 120

            Try
                myDataAdapter.Fill(myDataSet, "myaccount")

                datRespondent.DataSource = myDataSet
                datRespondent.DataBind()
                objConn.Close()

            Catch ex As Exception
            End Try

        Else
            strRet = BindData(datRespondent)
        End If
    End Sub
End Class