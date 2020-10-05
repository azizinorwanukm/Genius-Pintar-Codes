Imports System.Data.SqlClient

Public Class class_List_Table
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

                    year_list()
                    load_page()
                    lect_list()

                    strRet = BindData(datRespondent)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lect_list()
        strSQL = "SELECT class_info.stf_ID, staff_Name FROM class_info left join staff_info on staff_info.stf_ID = class_info.stf_ID 
                  WHERE staff_Status ='Access' order by staff_Info.staff_Name ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            filterLect.DataSource = ds
            filterLect.DataTextField = "staff_Name"
            filterLect.DataValueField = "stf_ID"
            filterLect.DataBind()
            filterLect.Items.Insert(0, New ListItem("Select Staff", String.Empty))
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

            filterYear.DataSource = ds
            filterYear.DataTextField = "Parameter"
            filterYear.DataValueField = "Parameter"
            filterYear.DataBind()
            filterYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT class_year from class_info where class_year ='" & Now.Year & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_year")) Then
                filterYear.SelectedValue = ds.Tables(0).Rows(0).Item("class_year")
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY class_type,class_Name ASC"

        tmpSQL = "Select  * From class_info"
        tmpSQL += " Left Join staff_Info on class_info.stf_ID=staff_Info.stf_ID"
        strWhere += " WHERE class_ID IS NOT NULL"
        strWhere += " and class_info.class_year = '" & filterYear.SelectedValue & "'"

        ''--debug

        If filterLect.SelectedIndex > 0 Then
            strWhere += " AND staff_Info.stf_ID = '" & filterLect.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
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
            Dlt_ClassData.SelectCommand.CommandText = "delete class_info where class_id ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyName As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Dim adminID As String = Request.QueryString("admin_ID")
        Try
            Response.Redirect("admin_edit_kelas_data.aspx?class_ID=" + strKeyName + "&admin_ID=" + adminID)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub btnRegNewClass_ServerClick(sender As Object, e As EventArgs) Handles btnRegNewClass.ServerClick
        Response.Redirect("admin_daftar_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Protected Sub filterLect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles filterLect.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub filterYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles filterYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub
End Class