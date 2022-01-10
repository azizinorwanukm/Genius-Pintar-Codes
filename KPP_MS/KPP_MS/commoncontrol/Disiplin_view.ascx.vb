Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Web.UI.Control


Public Class Disiplin_view
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim pattern As String = "dd MMMM yyyy"

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                yeardropdown()
                class_level()

                load_info()

                Counseling_Status()
                case_list()
                class_info_list()
                action_list()

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub load_info()
        ''student_info
        strSQL = "Select Parameter from setting where type = 'Year' and Parameter = '" & Now.Year & "'"

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
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlYear.SelectedValue = Now.Year
            End If
        End If
    End Sub

    Protected Sub ddlLevelNaming_selectedindexchange(sender As Object, e As EventArgs) Handles ddlLevelNaming.SelectedIndexChanged
        Try
            class_info_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlClassnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassnaming.SelectedIndexChanged

        Try
            'class_info_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlCasenaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCasenaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlYear_selectedindexchange(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            Counseling_Status()
            case_list()
            class_info_list()
            action_list()

            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlAction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAction.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

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

    Protected Sub ddlCounselingStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCounselingStatus.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean

        Dim mydataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 200

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


    Protected Sub class_info_list()

        If ddlLevelNaming.SelectedValue.Length = 0 And ddlYear.SelectedValue.Length = 0 Then
            ddlClassnaming.SelectedIndex = 0
            ddlClassnaming.Enabled = False
        Else
            ddlClassnaming.Enabled = True
        End If

        strSQL = "SELECT distinct class_Name,class_ID from class_info where class_level = '" & ddlLevelNaming.SelectedValue & "' and class_type = 'Compulsory' and class_Campus = 'PGPN'
                  and class_year = '" & ddlYear.SelectedValue & "' order by Class_Name ASC"
        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassnaming.DataSource = ds
            ddlClassnaming.DataTextField = "class_Name"
            ddlClassnaming.DataValueField = "class_ID"
            ddlClassnaming.DataBind()
            ddlClassnaming.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception
            objconn.Dispose()
        End Try
    End Sub

    Protected Sub case_list()
        strSQL = "SELECT case_Name,case_ID from case_info order by case_Name ASC "
        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCasenaming.DataSource = ds
            ddlCasenaming.DataTextField = "case_Name"
            ddlCasenaming.DataValueField = "case_ID"
            ddlCasenaming.DataBind()
            ddlCasenaming.Items.Insert(0, New ListItem("Select Case", String.Empty))

        Catch ex As Exception
            objconn.Dispose()
        End Try

    End Sub

    Private Sub action_list()
        strSQL = "select * from warning_letters_table order by id asc"
        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAction.DataSource = ds
            ddlAction.DataTextField = "title"
            ddlAction.DataValueField = "title"
            ddlAction.DataBind()
            ddlAction.Items.Insert(0, New ListItem("Select Action", String.Empty))

        Catch ex As Exception
            objconn.Dispose()
        End Try
    End Sub

    Protected Sub class_level()

        strSQL = "select * from setting where idx = 'Courses' and Type = 'Level'"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionstring")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlda As New SqlDataAdapter(strSQL, objconn)

        Try

            Dim ds As DataSet = New DataSet
            sqlda.Fill(ds, "anytable")

            ddlLevelNaming.DataSource = ds
            ddlLevelNaming.DataTextField = "Parameter"
            ddlLevelNaming.DataValueField = "Value"
            ddlLevelNaming.DataBind()
            ddlLevelNaming.Items.Insert(0, New ListItem("Select Class Level", String.Empty))

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub yeardropdown()
        strSQL = "Select * from setting where Type = 'Year'"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionstring")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlda As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlda.Fill(ds, "anytable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Counseling_Status()
        Dim strSQL As String = "SELECT value,parameter FROM setting WHERE idx='Counselor'"
        Dim strconn As String = ConfigurationManager.AppSettings("connectionstring")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlda As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlda.Fill(ds, "anytable")
            ddlCounselingStatus.DataSource = ds
            ddlCounselingStatus.DataTextField = "parameter"
            ddlCounselingStatus.DataValueField = "parameter"
            ddlCounselingStatus.DataBind()
            ddlCounselingStatus.Items.Insert(0, New ListItem("Counseling Status", String.Empty))
        Catch ex As Exception

        End Try

    End Sub

    Private Function getSQL() As String

        Dim dates As String = Request.Form("datepicker")
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY CONVERT(date, dicipline_info.Dicipline_Date) DESC"

        tmpSQL = "SELECT 
                    dicipline_info.disiplin_id,
	                dicipline_info.Dicipline_Date,
	                student_info.student_Name,
	                class_info.class_Name,
	                case_info.case_Name,
	                case_info.case_Category,
                    dicipline_info.std_ID,
                    warning_letters_table.title,
                    staff_info.staff_name,
					counseling_info.kslr_status
                FROM dicipline_info
	                LEFT JOIN student_info ON student_info.std_ID = dicipline_info.std_ID
	                LEFT JOIN class_info ON class_info.class_ID = dicipline_info.class_ID
	                LEFT JOIN case_info ON case_info.case_ID = dicipline_info.case_ID
					LEFT JOIN counseling_info ON counseling_info.disiplin_id = dicipline_info.disiplin_id
                    LEFT JOIN staff_info ON dicipline_info.stf_ID = staff_info.stf_ID
                    LEFT JOIN warning_letters_table ON dicipline_info.warning_id = warning_letters_table.id"

        strWhere = " WHERE class_info.class_type = 'Compulsory'"

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " AND (
                    student_info.student_Name like '%" & txtstudent.Text & "%' OR 
                    student_info.student_ID = '" & txtstudent.Text & "' OR 
                    student_info.student_Mykad = '" & txtstudent.Text & "')"
        End If

        If StartDate.Text.Length > 0 And EndDate.Text.Length = 0 Then
            strWhere += " AND CONVERT(date, dicipline_info.Dicipline_Date) >= '" & StartDate.Text & "'"
        End If

        If EndDate.Text.Length > 0 And StartDate.Text.Length = 0 Then
            strWhere += " AND CONVERT(date, dicipline_info.Dicipline_Date) <= '" & EndDate.Text & "'"
        End If

        If StartDate.Text.Length > 0 And EndDate.Text.Length > 0 Then
            strWhere += " AND CONVERT(date,dicipline_info.Dicipline_Date) BETWEEN '" & StartDate.Text & "' AND '" & EndDate.Text & "'"
        End If

        If Not ddlClassnaming.SelectedIndex = 0 And Not ddlClassnaming.SelectedValue.Length = 0 Then
            strWhere += " AND dicipline_info.class_ID = '" & ddlClassnaming.SelectedValue & "'"
        End If

        If Not ddlCasenaming.SelectedIndex = 0 And Not ddlCasenaming.SelectedValue.Length = 0 Then
            strWhere += " AND dicipline_info.case_ID = '" + ddlCasenaming.SelectedValue + "'"
        End If

        If Not ddlCounselingStatus.SelectedIndex = 0 And Not ddlCounselingStatus.SelectedValue.Length = 0 Then
            strWhere += " AND counseling_info.kslr_status = '" + ddlCounselingStatus.SelectedValue + "'"
        End If

        If ddlAction.SelectedIndex > 0 Then
            strWhere += " AND dicipline_info.Dicipline_Action = '" + ddlAction.SelectedValue + "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Protected Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        ''Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try

            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "DELETE dicipline_info WHERE disiplin_id = '" & strKeyName & "'; DELETE counseling_info WHERE disiplin_id='" & strKeyName & "';"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strkeyId As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_detail_disiplin.aspx?dispID=" + strkeyId + "&v=0" + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            Debug.WriteLine("Error" & ex.Message)
        End Try
    End Sub

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        If e.CommandName = "1" Then
            Response.Redirect("admin_detail_disiplin.aspx?stdID=" + e.CommandArgument + "&v=1" + "&admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Private Sub btnFind_ServerClick(sender As Object, e As EventArgs) Handles btnFind.ServerClick
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub
End Class