Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing

Public Class Disiplin_config
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
                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnsimpan_serverClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim bill As Integer

        If Not IsNothing(Compound.Text) And Compound.Text = "" Then
            bill = 0
        Else
            bill = 1

        End If

        If case_Name.Text <> "" And Not IsNumeric(case_Name.Text) And Not IsNothing(case_Name.Text) And Regex.IsMatch(case_Name.Text, "^[A-Za-z ]+$") Then
            If IsNumeric(Merit.Text) Then
                If IsNumeric(Compound.Text) Then

                    Using STDDATA As New SqlCommand("INSERT INTO case_info(case_name,merit,compound,bill) values 
                                ('" & case_Name.Text & "','" & Merit.Text & "', '" & Compound.Text & "', '" & bill & "')", objConn)
                        objConn.Open()
                        Dim i = STDDATA.ExecuteNonQuery()
                        objConn.Close()
                        If i <> 0 Then
                            errorCount = 0 '' Success
                        Else
                            errorCount = 1 ''

                        End If
                    End Using
                Else
                    errorCount = 2 ''compound
                End If
            Else
                errorCount = 3 ''merit 
            End If
        Else
            errorCount = 4 '' case name
        End If

        If errorCount = 0 Then
            Response.Redirect("admin_config_disiplin.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 1 Then
            Response.Redirect("admin_config_disiplin.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_config_disiplin.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_config_disiplin.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_config_disiplin.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))


        End If
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim mydataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(mydataSet, "myaccount")

            gvTable.DataSource = mydataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False

        End Try

        Return True


    End Function

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

    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = "ORDER by case_info.case_id ASC"

        tmpSQL = "select distinct case_id, case_name, merit, compound, bill from case_info "

        getSQL = tmpSQL & strOrderby
        Return getSQL
    End Function

    Protected Sub datrespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Try
            datRespondent.EditIndex = e.NewEditIndex
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub datrespondent_rowcancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles datRespondent.RowCancelingEdit
        datRespondent.EditIndex = -1
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value

        Try
            Dim Myconnection As SqlConnection = New SqlConnection(strConn)
            Dim dlt_ClassData As New SqlDataAdapter()

            Dim dlt_class As String

            dlt_ClassData.SelectCommand = New SqlCommand()
            dlt_ClassData.SelectCommand.Connection = Myconnection
            dlt_ClassData.SelectCommand.CommandText = "delete case_info where case_id = '" & strKeyName & "'"

            Myconnection.Open()
            dlt_class = dlt_ClassData.SelectCommand.ExecuteScalar()
            Response.Redirect("admin_config_disiplin.aspx?result=7&admin_ID=" + Request.QueryString("admin_ID"))
            Myconnection.Close()

            strRet = BindData(datRespondent)


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub datRespondent_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles datRespondent.RowUpdating
        Dim strkeyID As String = datRespondent.DataKeys(e.RowIndex).Value

        Dim case_box As TextBox = datRespondent.Rows(e.RowIndex).FindControl("case_box")
        Dim merit_box As TextBox = datRespondent.Rows(e.RowIndex).FindControl("merit_box")
        Dim compound_box As TextBox = datRespondent.Rows(e.RowIndex).FindControl("compound_box")
        Dim bill As TextBox = datRespondent.Rows(e.RowIndex).FindControl("bill_box")

        Try
            strSQL = "update case_info set case_name ='" + case_box.Text + "', merit = '" + merit_box.Text + "', compound = '" + compound_box.Text + "', bill = '" + bill.Text + "' where case_ID = '" & strkeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                Response.Redirect("admin_config_disiplin.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
            Else
                Response.Redirect("admin_config_disiplin.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))
            End If
        Catch ex As Exception

        End Try
        datRespondent.EditIndex = -1
        strRet = BindData(datRespondent)
    End Sub

End Class