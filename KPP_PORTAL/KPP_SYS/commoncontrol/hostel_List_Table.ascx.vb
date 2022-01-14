Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class hostel_List_Table
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

                hostel_name_list()
                block_name_list()
                block_level_list()
                student_Level()
                student_Sem()
                year_list()

                load_page()
                strRet = BindData(datRespondent)
                ''Generate_Table()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT year from student_Level where year ='" & Now.Year & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
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

    Protected Sub ddlHostelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelName.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

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

    Protected Sub ddlSemnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevelnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelnaming.SelectedIndexChanged
        ''Dim class As String = ""
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
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select student_info.student_Name,hostel_info.hostel_Name,hostel_info.block_Name,hostel_info.block_Level,room_info.room_Name,room_info.year,student_info.std_ID
                  from student_info
                  left join student_level on student_info.std_ID = student_level.std_ID
                  left join room_info on student_info.std_ID = room_info.std_ID
                  left join hostel_info on room_info.hostel_ID = hostel_info.hostel_ID"
        strWhere = " where hostel_info.year = '" & ddlYear.SelectedValue & "'"

        If ddlHostelName.SelectedIndex > 0 Then
            strWhere += " and hostel_info.hostel_Name = '" & ddlHostelName.SelectedValue & "'"
        End If

        If ddlBlockName.SelectedIndex > 0 Then
            strWhere += " and hostel_info.block_Name = '" & ddlBlockName.SelectedValue & "'"
        End If

        If ddlBlockLevel.SelectedIndex > 0 Then
            strWhere += " and hostel_info.block_Level = '" & ddlBlockLevel.SelectedValue & "'"
        End If

        If ddlLevelnaming.SelectedIndex > 0 Then
            strWhere += " and student_Level.student_Level = '" & ddlLevelnaming.SelectedValue & "'"
        End If

        If ddlSemnaming.SelectedIndex > 0 Then
            strWhere += " and student_Level.student_Sem = '" & ddlSemnaming.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Debug.WriteLine(getSQL)
        ''--debug
        Return getSQL
    End Function

    Private Sub block_name_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Block_Name' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlockName.DataSource = ds
            ddlBlockName.DataTextField = "Parameter"
            ddlBlockName.DataValueField = "Parameter"
            ddlBlockName.DataBind()
            ddlBlockName.Items.Insert(0, New ListItem("Select Block", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_level_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Block_Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlockLevel.DataSource = ds
            ddlBlockLevel.DataTextField = "Parameter"
            ddlBlockLevel.DataValueField = "Parameter"
            ddlBlockLevel.DataBind()
            ddlBlockLevel.Items.Insert(0, New ListItem("Select Block Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub hostel_name_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Hostel_Name' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostelName.DataSource = ds
            ddlHostelName.DataTextField = "Parameter"
            ddlHostelName.DataValueField = "Parameter"
            ddlHostelName.DataBind()
            ddlHostelName.Items.Insert(0, New ListItem("Select Hostel", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevelnaming.DataSource = ds
            ddlLevelnaming.DataTextField = "Parameter"
            ddlLevelnaming.DataValueField = "Parameter"
            ddlLevelnaming.DataBind()
            ddlLevelnaming.Items.Insert(0, New ListItem("Select Student Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Sem()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemnaming.DataSource = ds
            ddlSemnaming.DataTextField = "Parameter"
            ddlSemnaming.DataValueField = "Parameter"
            ddlSemnaming.DataBind()
            ddlSemnaming.Items.Insert(0, New ListItem("Select Student Sem", String.Empty))
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

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try

            Dim stdID As String = "select std_ID from student_info where std_ID = '" & strKeyName & "'"
            Dim dataStdID As String = getFieldValue(stdID, strConn)

            'UPDATE
            strSQL = "UPDATE room_info SET std_ID='' WHERE std_ID ='" & dataStdID & "' and year='" & Now.Year & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            strRet = BindData(datRespondent)

        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

End Class