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


                    filterType.DataSource = Me.fillDDL("subject_type")
                    filterType.DataTextField = "subject_type"
                    filterType.DataValueField = "subject_type"
                    filterType.DataBind()
                    filterType.Items.Insert(0, New ListItem("Select Course Type", String.Empty))

                    filterFoundationLvl.DataSource = Me.fillDDL("subject_StudentYear")
                    filterFoundationLvl.DataTextField = "subject_StudentYear"
                    filterFoundationLvl.DataValueField = "subject_StudentYear"
                    filterFoundationLvl.DataBind()
                    filterFoundationLvl.Items.Insert(0, New ListItem("Select Student Year", String.Empty))

                    filterSems.DataSource = Me.fillDDL("subject_sem")
                    filterSems.DataTextField = "subject_sem"
                    filterSems.DataValueField = "subject_sem"
                    filterSems.DataBind()
                    filterSems.Items.Insert(0, New ListItem("Select Sem", String.Empty))

                    filterYear.DataSource = Me.fillDDL("subject_year")
                    filterYear.DataTextField = "subject_year"
                    filterYear.DataValueField = "subject_year"
                    filterYear.DataBind()
                    filterYear.Items.Insert(0, New ListItem("Select Years", String.Empty))

                    strRet = BindData(datRespondent)

                End If
            End If
        Catch ex As Exception

        End Try
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
        Dim sem As String = filterSems.SelectedItem.Value
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Dim dt As New DataTable
        Dim dv As New DataView
        Try
            myDataAdapter.Fill(dt)
            dv = dt.DefaultView
            If Not String.IsNullOrEmpty(sem) Then
                dv.RowFilter = "subject_sem = '" & sem & "'"
            End If
            datRespondent.DataSource() = dv
            datRespondent.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub filterFoundationLvl_Changed(sender As Object, e As EventArgs) Handles filterFoundationLvl.SelectedIndexChanged
        Dim stdnYear As String = filterFoundationLvl.SelectedItem.Value
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Dim dt As New DataTable
        Dim dv As New DataView
        Try
            myDataAdapter.Fill(dt)
            dv = dt.DefaultView
            If Not String.IsNullOrEmpty(stdnYear) Then
                dv.RowFilter = "subject_StudentYear = '" & stdnYear & "'"
            End If
            datRespondent.DataSource() = dv
            datRespondent.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub filterType_Changed(sender As Object, e As EventArgs) Handles filterType.SelectedIndexChanged
        Dim type As String = filterType.SelectedItem.Value
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Dim dt As New DataTable
        Dim dv As New DataView
        Try
            myDataAdapter.Fill(dt)
            dv = dt.DefaultView
            If Not String.IsNullOrEmpty(type) Then
                dv.RowFilter = "subject_type = '" & type & "'"
            End If
            datRespondent.DataSource() = dv
            datRespondent.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub filterYear_Changed(sender As Object, e As EventArgs) Handles filterYear.SelectedIndexChanged
        Dim year As String = filterYear.SelectedItem.Value
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Dim dt As New DataTable
        Dim dv As New DataView
        Try
            myDataAdapter.Fill(dt)
            dv = dt.DefaultView
            If Not String.IsNullOrEmpty(year) Then
                dv.RowFilter = "subject_year = '" & year & "'"
            End If
            datRespondent.DataSource() = dv
            datRespondent.DataBind()
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

    Protected Sub searchBtn_Click(sender As Object, e As EventArgs)

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