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

                '''get a user access
                'Dim userAccess As String = ""
                'userAccess = "select staff_Position from staff_Info where stf_ID = '" & id & "'"
                'Dim access As String = getFieldValue(userAccess, strConn)
                'hiddenAccess.Value = access

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

    Private Sub btnRegNewStaff_ServerClick(sender As Object, e As EventArgs) Handles btnRegNewStaff.ServerClick
        Try
            Response.Redirect("admin_daftar_pengajar_baru.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''data is not delete.. instead just block from view.. incase permata want to recover the data
        strSQL = "Update staff_Info set satff_Status = 'Block' where stf_ID = '" & strKeyName & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY staff_Name ASC"

        tmpSQL = "Select * From staff_Info"
        strWhere += " WHERE stf_ID IS NOT NULL"
        strWhere += " AND staff_Status = 'Access' "

        If Not txtstaff_data.Text.Length = 0 Then
            strWhere += " AND (staff_Name LIKE '%" & txtstaff_data.Text & "%'"
        End If

        If Not txtstaff_data.Text.Length = 0 Then
            strWhere += " OR staff_Mykad = '" & txtstaff_data.Text & "' OR staff_ID = '" & txtstaff_data.Text & "')"
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

End Class