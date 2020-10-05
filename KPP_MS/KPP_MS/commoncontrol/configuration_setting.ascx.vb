Imports System.Data.SqlClient

Public Class configuration_setting
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

                settingType()

            End If

            '' strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        If Parameter.Text <> "" Then

            If Value.Text <> "" Then

                If Type.Text = "" Or Regex.IsMatch(Type.Text, "^[A-Za-z0-9 ]+$") Then

                    If idx.Text = "" Or Regex.IsMatch(idx.Text, "^[A-Za-z0-9 ]+$") Then
                        'Insert
                        Using SETTINGDATA As New SqlCommand("INSERT setting(Parameter,Value,Type,idx) values ('" & Parameter.Text & "','" & Value.Text & "','" & Type.Text & "','" & idx.Text & "')", objConn)
                            objConn.Open()
                            Dim i = SETTINGDATA.ExecuteNonQuery()
                            objConn.Close()

                            If i <> 0 Then
                                errorCount = 0
                            Else
                                errorCount = 1
                            End If
                        End Using
                    Else
                        errorCount = 8
                    End If
                Else
                    errorCount = 6
                End If
            Else
                errorCount = 4
            End If
        Else
            errorCount = 3
        End If

        If errorCount = 1 Then
            Response.Redirect("admin_konfigurasi.aspx?result=-1&admin_ID=" & Request.QueryString("admin_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_konfigurasi.aspx?result=1&admin_ID=" & Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_konfigurasi.aspx?result=2&admin_ID=" & Request.QueryString("admin_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_konfigurasi.aspx?result=3&admin_ID=" & Request.QueryString("admin_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_konfigurasi.aspx?result=4&admin_ID=" & Request.QueryString("admin_ID"))
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_konfigurasi.aspx?result=5&admin_ID=" & Request.QueryString("admin_ID"))
        ElseIf errorCount = 6 Then
            Response.Redirect("admin_konfigurasi.aspx?result=6&admin_ID=" & Request.QueryString("admin_ID"))
        ElseIf errorCount = 7 Then
            Response.Redirect("admin_konfigurasi.aspx?result=7&admin_ID=" & Request.QueryString("admin_ID"))
        ElseIf errorCount = 8 Then
            Response.Redirect("admin_konfigurasi.aspx?result=8&admin_ID=" & Request.QueryString("admin_ID"))
        End If
    End Sub

    Private Sub settingType()
        strSQL = "SELECT distinct idx from setting where Type is not null and idx is not null"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlType.DataSource = ds
            ddlType.DataTextField = "idx"
            ddlType.DataValueField = "idx"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Select Setting Type", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
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
        Dim strOrderby As String = " ORDER BY Type ASC"

        tmpSQL = "select * from setting "
        strWhere = " where ID Is Not null"

        If ddlType.SelectedIndex > 0 Then
            strWhere += " and idx = '" & ddlType.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing

        datRespondent.EditIndex = e.NewEditIndex
        Me.BindData(datRespondent)

    End Sub

    Protected Sub OnRowCancelingEdit(sender As Object, e As EventArgs)
        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim parametertxtbox As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtParameter"), TextBox)
        Dim valuetxtbox As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtValue"), TextBox)
        Dim typetxtbox As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtType"), TextBox)
        Dim idxtxtbox As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtidx"), TextBox)

        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''update marks
        strSQL = "UPDATE setting SET Parameter='" & parametertxtbox.Text & "',Value ='" & valuetxtbox.Text & "',Type='" & typetxtbox.Text & "',idx='" & idxtxtbox.Text & "' WHERE ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete setting where ID='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub
End Class