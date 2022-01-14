Imports System.Data.SqlClient
Imports System.IO

Public Class lecturer_List_Table
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim id As String = ""
                id = Request.QueryString("admin_ID")

                txtstaff_data.Text = ""
                year_list()

                ''get a user access
                Dim userAccess As String = ""
                userAccess = "select staff_Position from staff_Info where stf_ID = '" & id & "'"
                Dim access As String = getFieldValue(userAccess, strConn)
                hiddenAccess.Value = access

                load_page()
                strRet = BindData(datRespondent)
                ''Generate_Table()
            End If

        Catch ex As Exception
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

    Private Sub load_page()
        strSQL = "SELECT staff_Year from staff_Info where staff_Year ='" & Now.Year & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Year")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub btnRegNewStaff_ServerClick(sender As Object, e As EventArgs) Handles btnRegNewStaff.ServerClick
        Try
            Response.Redirect("admin_daftar_pengajar_baru.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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
            Dlt_ClassData.SelectCommand.CommandText = "delete staff_Info where stf_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

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

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY staff_Name ASC"

        tmpSQL = "Select * From staff_Info"
        strWhere += " WHERE stf_ID IS NOT NULL"
        strWhere += " AND staff_Year = '" & ddlYear.SelectedValue & "'"

        If Not txtstaff_data.Text.Length = 0 Then
            strWhere += " AND staff_Name LIKE '%" & txtstaff_data.Text & "%'"
        End If

        If Not txtstaff_data.Text.Length = 0 Then
            strWhere += " OR staff_Mykad = '" & txtstaff_data.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_pengajar_data.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + strKeyID)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
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
End Class