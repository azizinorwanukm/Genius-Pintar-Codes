Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class hostel_list_managae
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

                Dim id As String = ""
                id = Request.QueryString("admin_ID")

                block_name_list()
                block_level_list()
                year_list()
                sem_list()
                strRet = BindData(datRespondent)
            End If
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strRet = BindData(datRespondent)
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

    Protected Sub ddlBlockName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockName.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlBlockLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockLevel.SelectedIndexChanged
        ''Dim class As String = ""
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlYearList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYearList.SelectedIndexChanged
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
        Dim strOrderby As String = " ORDER BY year ASC"

        tmpSQL = "SELECT 
	                hostel_info.hostel_ID,
	                A.Parameter AS HostelName,
	                B.Parameter AS HostelLevel,
	                hostel_info.hostel_RoomNumbers,
	                C.Parameter AS HostelSem,
	                hostel_info.year
                FROM 
	                hostel_info
	                JOIN setting AS A ON A.Value = hostel_info.hostel_BlockNames
	                JOIN setting AS B ON B.Value = hostel_info.hostel_BlockLevels
	                JOIN setting AS C ON C.Value = hostel_info.hostel_Sem"

        strWhere = " WHERE hostel_info.year = '" & ddlYearList.SelectedValue & "'"

        If ddlBlockName.SelectedIndex > 0 Then
            strWhere += " AND hostel_info.hostel_BlockNames = '" & ddlBlockName.SelectedValue & "'"
        End If

        If ddlBlockLevel.SelectedIndex > 0 Then
            strWhere += " AND hostel_info.hostel_BlockLevels = '" & ddlBlockLevel.SelectedValue & "'"
        End If

        If ddlSemList.SelectedIndex > 0 Then
            strWhere += " AND hostel_info.hostel_Sem = '" & ddlSemList.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Debug.WriteLine(getSQL)
        ''--debug
        Return getSQL
    End Function

    Private Sub block_name_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Block_Name' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlockName.DataSource = ds
            ddlBlockName.DataTextField = "Parameter"
            ddlBlockName.DataValueField = "Value"
            ddlBlockName.DataBind()
            ddlBlockName.Items.Insert(0, New ListItem("Select Block", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_level_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Block_Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlockLevel.DataSource = ds
            ddlBlockLevel.DataTextField = "Parameter"
            ddlBlockLevel.DataValueField = "Value"
            ddlBlockLevel.DataBind()
            ddlBlockLevel.Items.Insert(0, New ListItem("Select Block Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter,Value FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYearList.DataSource = ds
            ddlYearList.DataTextField = "Parameter"
            ddlYearList.DataValueField = "Value"
            ddlYearList.DataBind()
            ddlYearList.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYearList.SelectedValue = Date.Today.Year
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub sem_list()
        Using cmd As New SqlCommand("SELECT Parameter,Value FROM setting WHERE Type='Sem'", objConn)
            objConn.Open()
            ddlSemList.DataSource = cmd.ExecuteReader
            ddlSemList.DataTextField = "Parameter"
            ddlSemList.DataValueField = "Value"
            ddlSemList.DataBind()
            ddlSemList.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlSemList.SelectedIndex = 0
            objConn.Close()
        End Using
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_asrama_data.aspx?hostelID=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
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
            Dlt_ClassData.SelectCommand.CommandText = "DELETE FROM hostel_info WHERE hostel_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub ddlSemList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemList.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub
End Class