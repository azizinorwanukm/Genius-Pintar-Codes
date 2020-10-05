Imports System.Data.SqlClient

Public Class scholarship_create
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

                    scholarship_type_list()
                    scholarship_status_list()

                    strRet = BindData(datRespondent)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub scholarship_type_list()
        strSQL = "SELECT * from setting where idx = 'Scholarship'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            scholarship_type.DataSource = ds
            scholarship_type.DataTextField = "Parameter"
            scholarship_type.DataValueField = "Parameter"
            scholarship_type.DataBind()
            scholarship_type.Items.Insert(0, New ListItem("Select Type", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub scholarship_status_list()
        Try
            ddlStatus.Items.Insert(0, New ListItem("All", String.Empty))
            ddlStatus.Items.Insert(1, New ListItem("Active", "Active"))
            ddlStatus.Items.Insert(2, New ListItem("Inactive", "Inactive"))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btn_create_ServerClick(sender As Object, e As EventArgs) Handles btn_create.ServerClick
        Try
            If scholarship_name.Text.Length > 0 Then
                If scholarship_sponsor.Text.Length > 0 Then
                    If scholarship_type.SelectedIndex > 0 Then

                        strSQL = "insert into scholarship(scholarship_name,scholarship_sponsar,scholarship_type,scholarship_status) values('" & scholarship_name.Text & "','" & scholarship_sponsor.Text & "','" & scholarship_type.SelectedValue & "','Active')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet = "0" Then
                            ShowMessage("Register scholarship", MessageType.Success)
                        Else
                            ShowMessage("Register scholarship", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please select scholarship type", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please fill in scholarship sponsor", MessageType.Error)
                End If
            Else
                ShowMessage("Please fill in scholarship name", MessageType.Error)
            End If

            strRet = BindData(datRespondent)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btn_back_ServerClick(sender As Object, e As EventArgs) Handles btn_back.ServerClick
        Try
            Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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
        Dim strOrderby As String = " ORDER BY scholarship_name ASC"

        tmpSQL = "Select * From scholarship"
        strWhere += " WHERE scholarship_id IS NOT NULL"

        If ddlStatus.SelectedIndex > 0 Then
            strWhere += " AND scholarship_status = '" & ddlStatus.SelectedValue & "'"
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
            Dlt_ClassData.SelectCommand.CommandText = "delete scholarship where scholarship_id ='" & strKeyName & "'"
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
            Response.Redirect("admin_edit_biasiswa.aspx?scholarship_id=" & strKeyName & "&admin_ID=" & adminID)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class