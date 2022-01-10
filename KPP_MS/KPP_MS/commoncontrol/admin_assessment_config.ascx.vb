Imports System.Data.SqlClient

Public Class admin_assessment_config
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
                format_info()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub format_info()
        strSQL = "select staff_Name, stf_ID from staff_Info where staff_contract = '1999'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlformat.DataSource = ds
            ddlformat.DataTextField = "staff_Name"
            ddlformat.DataValueField = "stf_ID"
            ddlformat.DataBind()
            ddlformat.Items.Insert(0, New ListItem("Select Format", String.Empty))
            ddlformat.Items.Insert(1, New ListItem("Homeroom", "Homeroom"))
            ddlformat.Items.Insert(2, New ListItem("Coordinator", "Homeroom"))
            ddlformat.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlformat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlformat.SelectedIndexChanged
        Try
            If ddlformat.SelectedValue = "Homeroom" Then
                strRet = BindData(datRespondent)
            End If

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

            'gvTable.Columns(1).ControlStyle.Width = 170
            'gvTable.Columns(2).ControlStyle.Width = 180
            'gvTable.Columns(3).ControlStyle.Width = 180

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
        Dim strOrderby As String = " order by asconfig_ID ASC"

        tmpSQL = "select * from assessment_config "

        strWhere = " where asconfig_ID is not null"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing

        datRespondent.EditIndex = e.NewEditIndex
        Me.BindData(datRespondent)

    End Sub

    Protected Sub AssessmentCancelEditing(sender As Object, e As EventArgs)
        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub AssessmentUpdate(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)

        Dim txtdescription As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtdescription"), TextBox)

        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''update the data in table
        strSQL = "Update assessment_config set description = '" & txtdescription.Text & "' where asconfig_ID = '" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Private Sub btnAdd_ServerClick(sender As Object, e As EventArgs) Handles btnAdd.ServerClick

        'Insert
        Using ASSDATA As New SqlCommand("INSERT into assessment_config(description) values ('Please edit this data')", objConn)
            objConn.Open()
            Dim i = ASSDATA.ExecuteNonQuery()
            objConn.Close()
        End Using

        BindData(datRespondent)
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
            Dlt_ClassData.SelectCommand.CommandText = "delete assessment_config where asconfig_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub
End Class