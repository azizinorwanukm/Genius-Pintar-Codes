Imports System.Data.SqlClient

Public Class student_AddHostel
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ddlRoomNameChoose.Enabled = False

                ddlYear()
                ddlLevel()
                ddlSem()
                ddlHostelNameList()
                ddlBlockNameList()
                ddlBlockLevelList()

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
                ddl_year.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddl_year.SelectedValue = ""
            End If
        End If
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

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete student_info where std_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
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

            If gvTable.Rows.Count = 0 Then
                Btnsimpan.Visible = False
                nodatamessage.Visible = True
            Else
                nodatamessage.Visible = False
                Btnsimpan.Visible = True
            End If

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

        tmpSQL = "select student_info.std_ID,student_info.student_Name,hostel_info.hostel_Name,hostel_info.block_Name,hostel_info.block_Level,room_info.room_Name from student_info
                  left join student_level on student_info.std_ID=student_level.std_ID
                  left join room_info on student_info.std_ID=room_info.std_ID
                  left join hostel_info on room_info.hostel_id=hostel_info.hostel_ID"
        strWhere = " where student_level.year = '" & ddl_year.SelectedValue & "'"
        strWhere += " and room_info.std_ID is null"

        If ddl_level.SelectedIndex > 0 Then
            strWhere += " And student_level.student_Level = '" & ddl_level.SelectedValue & "'"
        End If

        If ddl_sem.SelectedIndex > 0 Then
            strWhere += " And student_level.student_Sem = '" & ddl_sem.SelectedValue & "'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " And student_info.student_ID Like '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent.Text & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL

    End Function

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
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

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSem()
        Try
            strSQL = "Select Parameter from setting where Type = 'Sem'"
            ''strSQL += " And subject_Year = '" & ddl_year.SelectedValue & "'"

            Debug.WriteLine(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_sem.DataSource = ds
            ddl_sem.DataTextField = "Parameter"
            ddl_sem.DataValueField = "Parameter"
            ddl_sem.DataBind()
            ddl_sem.Items.Insert(0, New ListItem("Select Student Sem", String.Empty))
            ddl_sem.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlYear()
        Try
            Dim stryear As String = "Select Parameter from setting where Type = 'Year'"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddl_year.DataSource = yrds
            ddl_year.DataValueField = "Parameter"
            ddl_year.DataTextField = "Parameter"
            ddl_year.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevel()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_level.DataSource = levds
            ddl_level.DataValueField = "Parameter"
            ddl_level.DataTextField = "Parameter"
            ddl_level.DataBind()
            ddl_level.Items.Insert(0, New ListItem("Select Student Level", String.Empty))
            ddl_level.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlHostelNameList()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Hostel_Name'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlHostelNameChoose.DataSource = levds
            ddlHostelNameChoose.DataValueField = "Parameter"
            ddlHostelNameChoose.DataTextField = "Parameter"
            ddlHostelNameChoose.DataBind()
            ddlHostelNameChoose.Items.Insert(0, New ListItem("Select Hostel", String.Empty))
            ddlHostelNameChoose.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlBlockNameList()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Block_Name'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlBlockNameChoose.DataSource = levds
            ddlBlockNameChoose.DataValueField = "Parameter"
            ddlBlockNameChoose.DataTextField = "Parameter"
            ddlBlockNameChoose.DataBind()
            ddlBlockNameChoose.Items.Insert(0, New ListItem("Select Block", String.Empty))
            ddlBlockNameChoose.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlBlockLevelList()
        Try
            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Block_Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlBlockLevelChoose.DataSource = levds
            ddlBlockLevelChoose.DataValueField = "Parameter"
            ddlBlockLevelChoose.DataTextField = "Parameter"
            ddlBlockLevelChoose.DataBind()
            ddlBlockLevelChoose.Items.Insert(0, New ListItem("Select Floor", String.Empty))
            ddlBlockLevelChoose.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlBlockLevelChoose_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockLevelChoose.SelectedIndexChanged
        Try

            ddlRoomNameChoose.Enabled = True

            ''get a room name that is still have empty space
            Dim roomEmpty As String = "select distinct room_Name from room_info where year = '" & Now.Year & "'"
            Dim sqlLevelDA As New SqlDataAdapter(roomEmpty, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")
            ddlRoomNameChoose.DataSource = levds
            ddlRoomNameChoose.DataValueField = "room_Name"
            ddlRoomNameChoose.DataTextField = "room_Name"
            ddlRoomNameChoose.DataBind()
            ddlRoomNameChoose.Items.Insert(0, New ListItem("Select Room", String.Empty))
            ddlRoomNameChoose.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlRoomNameChoose_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoomNameChoose.SelectedIndexChanged
        Try

            ''count a selected room name
            Dim cRoomName As String = "select count(room_Name) from room_info where room_Name = '" & ddlRoomNameChoose.SelectedValue & "' and std_ID is not null and year = '" & Now.Year & "'"
            Dim DatacRoomName As String = getFieldValue(cRoomName, strConn)

            ''get a max room capacirty
            Dim maxRoomName As String = "select room_Capacity from room_info where room_Name = '" & ddlRoomNameChoose.SelectedValue & "' and year = '" & Now.Year & "'"
            Dim DatamaxRoomName As String = getFieldValue(maxRoomName, strConn)

            Dim answer As String = DatacRoomName & "/" & DatamaxRoomName
            count_student.Text = answer

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim errorCount As Integer = 0

        ''get hostel id
        Dim hostelID As String = "select hostel_ID from hostel_info where hostel_Name = '" & ddlHostelNameChoose.SelectedValue & "' 
                                  and block_Name = '" & ddlBlockNameChoose.SelectedValue & "' and block_Level = '" & ddlBlockLevelChoose.SelectedValue & "' and year = '" & Now.Year & "'"
        Dim DatahostelID As String = getFieldValue(hostelID, strConn)

        ''get room capacity
        Dim roomMax As String = "select distinct room_Capacity from room_info where hostel_ID = '" & DatahostelID & "' and year = '" & Now.Year & "'"
        Dim DataroomMax As String = getFieldValue(roomMax, strConn)
        Dim max As Integer = 0

        If DataroomMax = "2" Then
            max = 2
        ElseIf DataroomMax = "3" Then
            max = 3
        ElseIf DataroomMax = "4" Then
            max = 4
        End If

        ''get a balance free space ín the selected room
        Dim emptyRoomName As String = "select count(room_Name) from room_info where room_Name = '" & ddlRoomNameChoose.SelectedValue & "' and std_ID is not null and year = '" & Now.Year & "'"
        Dim DataEmptyRoomName As String = getFieldValue(emptyRoomName, strConn)
        Dim empty As Integer = 0

        If DataEmptyRoomName = "0" Then
            empty = 0
        ElseIf DataEmptyRoomName = "1" Then
            empty = 1
        ElseIf DataEmptyRoomName = "2" Then
            empty = 2
        ElseIf DataEmptyRoomName = "3" Then
            empty = 3
        ElseIf DataEmptyRoomName = "4" Then
            empty = 4
        End If

        Dim answer As Integer = 0
        answer = max - empty

        ''check number of student want to register in the room
        Dim i As Integer
        Dim countSpace As Integer = 0
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdates As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdates Is Nothing Then
                If chkUpdates.Checked = True Then
                    countSpace += 1
                End If
            End If
        Next

        If countSpace >= max Then
            Response.Redirect("admin_asrama_penempatan_pelajar.aspx?result=90&admin_ID=" + Request.QueryString("admin_ID"))
        Else
            ''check for the free room 
            If countSpace >= answer Then
                Response.Redirect("admin_asrama_penempatan_pelajar.aspx?result=91&admin_ID=" + Request.QueryString("admin_ID"))
            Else
                ''insert
                Dim j As Integer
                For j = 0 To datRespondent.Rows.Count - 1 Step j + 1
                    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(j).Cells(5).FindControl("chkSelect"), CheckBox)
                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strData As String = datRespondent.DataKeys(j).Value.ToString
                        If chkUpdate.Checked = True Then

                            ''get room id
                            Dim topRoomID As String = "select top(1) room_ID from room_info where std_ID is null and year = '" & Now.Year & "' and room_Name = '" & ddlRoomNameChoose.SelectedValue & "'"
                            Dim DatatopRoomID As String = getFieldValue(topRoomID, strConn)

                            'get student id
                            'Dim stdID As String = "select student_info.std_ID from student_info left join student_level on student_info.std_ID = student_level.std_ID where student_level.year = '" & Now.Year & "' and std_ID= '" & strData & "'"
                            'Dim DatastdID As String = getFieldValue(stdID, strConn)

                            strSQL = "update room_info set std_ID='" & strData & "' where room_ID = '" & DatatopRoomID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                errorCount = 0
                            Else
                                errorCount = 1
                            End If

                        End If
                    End If
                Next
            End If
        End If


        If errorCount = 0 Then
            Response.Redirect("admin_asrama_penempatan_pelajar.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 1 Then
            Response.Redirect("admin_asrama_penempatan_pelajar.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))

        End If
    End Sub
End Class