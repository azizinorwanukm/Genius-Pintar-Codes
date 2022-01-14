Imports System.Data.SqlClient

Public Class hostel_Update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("room_ID")
                block_name_list()
                block_level_list()
                hostel_name_list()
                year_list()

                room_info_Load(id)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub block_name_list()
        strSQL = "SELECT Parameter from setting where Type = 'Block_Name' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlock_Name.DataSource = ds
            ddlBlock_Name.DataTextField = "Parameter"
            ddlBlock_Name.DataValueField = "Parameter"
            ddlBlock_Name.DataBind()
            ddlBlock_Name.Items.Insert(0, New ListItem("Select Block", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub hostel_name_list()
        strSQL = "SELECT Parameter from setting where Type = 'Hostel_Name' and Parameter is not null "
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
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_level_list()
        strSQL = "SELECT Parameter from setting where Type = 'Block_Level' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlock_Level.DataSource = ds
            ddlBlock_Level.DataTextField = "Parameter"
            ddlBlock_Level.DataValueField = "Parameter"
            ddlBlock_Level.DataBind()
            ddlBlock_Level.Items.Insert(0, New ListItem("Select Floor Level", String.Empty))
            ''ddlYear.SelectedIndex = 0

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

    Private Sub room_info_Load(ByVal strroom_ID As String)
        strSQL = "SELECT * FROM room_info 
                  left join hostel_info on room_info.hostel_ID=hostel_info.hostel_ID 
                  WHERE room_ID ='" & strroom_ID & "'"
        '--debug
        ''Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_Name")) Then
                    ddlHostelName.SelectedValue = ds.Tables(0).Rows(0).Item("hostel_Name")
                Else
                    ddlHostelName.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("block_Name")) Then
                    ddlBlock_Name.SelectedValue = ds.Tables(0).Rows(0).Item("block_Name")
                Else
                    ddlBlock_Name.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("block_Level")) Then
                    ddlBlock_Level.SelectedValue = ds.Tables(0).Rows(0).Item("block_Level")
                Else
                    ddlBlock_Level.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                    ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
                Else
                    ddlYear.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("room_Capacity")) Then
                    room_Capacity.Text = ds.Tables(0).Rows(0).Item("room_Capacity")
                Else
                    room_Capacity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("room_Name")) Then
                    room_Name.Text = ds.Tables(0).Rows(0).Item("room_Name")
                Else
                    room_Name.Text = ""
                End If

            Else
                'Response.Write("Table count < 0")
            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        'UPDATE room_info
        strSQL = "UPDATE room_info SET room_Name ='" & room_Name.Text & "',room_Capacity='" & room_Capacity.Text & "',year = '" & ddlYear.SelectedValue & "' WHERE room_ID ='" & Request.QueryString("room_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''get hostel id from that room id
        Dim hostelExist As String = "select hostel_ID from room_info where room_ID = '" & Request.QueryString("room_ID") & "'"
        Dim dataHostelExist As String = getFieldValue(hostelExist, strConn)

        ''UPDATE hostel_info
        strSQL = "UPDATE hostel_info SET hostel_Name ='" & ddlHostelName.SelectedValue & "', block_Name='" & ddlBlock_Name.SelectedValue & "', block_Level='" & ddlBlock_Level.SelectedValue & "',year = '" & ddlYear.SelectedValue & "' WHERE hostel_ID ='" & dataHostelExist & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            Response.Redirect("admin_pengurusan_am_hostel.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID") + "")
        Else
            Response.Redirect("admin_pengurusan_am_hostel.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID") + "")
        End If
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_pengurusan_am_hostel.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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

End Class