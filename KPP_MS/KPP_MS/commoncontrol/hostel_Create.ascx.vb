Imports System.Data.SqlClient
Imports System.Drawing

Public Class hostel_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction
    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                block_name_list()
                block_level_list()
                year_list()
                sem_list()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub block_name_list()
        strSQL = "SELECT Parameter, Value from setting where Type = 'Block_Name' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlock_Name.DataSource = ds
            ddlBlock_Name.DataTextField = "Parameter"
            ddlBlock_Name.DataValueField = "Value"
            ddlBlock_Name.DataBind()
            ddlBlock_Name.Items.Insert(0, New ListItem("Select Block", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_level_list()
        strSQL = "SELECT Parameter,Value from setting where Type = 'Block_Level' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlock_Level.DataSource = ds
            ddlBlock_Level.DataTextField = "Parameter"
            ddlBlock_Level.DataValueField = "Value"
            ddlBlock_Level.DataBind()
            ddlBlock_Level.Items.Insert(0, New ListItem("Select Floor Level", String.Empty))
            ''ddlYear.SelectedIndex = 0

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

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Value"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub sem_list()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSem.DataSource = ds
            ddlSem.DataTextField = "Parameter"
            ddlSem.DataValueField = "Value"
            ddlSem.DataBind()
            ddlSem.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount = 0
        Dim id As String = Request.QueryString("admin_ID")
        Dim queryStfID As String = String.Format("SELECT stf_ID FROM security_ID WHERE loginID_Number='{0}'", id)
        Dim getStfID = oCommon.getFieldValue(queryStfID)

        If ddlYear.SelectedIndex > 0 Then
            If ddlSem.SelectedIndex > 0 Then
                If ddlBlock_Name.SelectedIndex > 0 Then
                    If ddlBlock_Level.SelectedIndex > 0 Then
                        If txtRoomQuantity.Text.Length > 0 Then
                            Debug.WriteLine("txtRoomQuantity: " & txtRoomQuantity.Text)
                            If IsNumeric(txtRoomQuantity.Text) Then
                                If hostelRegistered() = "" Then
                                    Dim newHostelID = createHostel(getStfID)
                                    If newHostelID.Length > 0 Then
                                        Dim saveSuccess = createRoom(newHostelID, getStfID)
                                        If saveSuccess = True Then
                                            ShowMessage("Save success!", MessageType.Success)
                                            ddlYear.SelectedIndex = 0
                                            ddlSem.SelectedIndex = 0
                                            ddlBlock_Name.SelectedIndex = 0
                                            ddlBlock_Level.SelectedIndex = 0
                                            txtRoomQuantity.Text = ""
                                        End If
                                    Else
                                        'save hostel failed
                                        ShowMessage("Save hostel failed!", MessageType.Error)
                                    End If
                                Else
                                    'hostel registered
                                    ShowMessage("Hostel already reagistered!", MessageType.Error)
                                    Debug.WriteLine("Hostel ID: " & hostelRegistered())
                                    loadRegisteredHostel(hostelRegistered())
                                End If
                            Else
                                'room quantity not in number
                                ShowMessage("Please insert the correct number for the room quantiry!", MessageType.Error)
                            End If
                        Else
                            'room quantity empty
                            ShowMessage("Room quantity is required!", MessageType.Error)
                        End If
                    Else
                        'block level not selected
                        ShowMessage("Please select the room level!", MessageType.Error)
                    End If
                Else
                    'block name not selected
                    ShowMessage("Please select the block name!", MessageType.Error)
                End If
            Else
                'sem not selected
                ShowMessage("Please select semesters!", MessageType.Error)
            End If
        Else
            'year not selected
            ShowMessage("Please select year!", MessageType.Error)
        End If
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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

    Protected Function hostelRegistered() As String
        Dim query As String = "SELECT hostel_ID FROM hostel_info WHERE hostel_BlockNames='" & ddlBlock_Name.SelectedValue & "' AND hostel_BlockLevels='" & ddlBlock_Level.SelectedValue & "' AND year='" & ddlYear.SelectedValue & "' AND hostel_Sem='" & ddlSem.SelectedValue & "'"
        Dim getHostelId = oCommon.getFieldValue(query)
        Debug.WriteLine("registered hostel id:>" & getHostelId & "<")
        Return getHostelId
    End Function

    Protected Function createHostel(stfID As String) As String
        Dim hostelID = ""
        Using cmd As New SqlCommand("INSERT INTO hostel_info(hostel_BlockNames,hostel_BlockLevels,hostel_RoomNumbers,year,hostel_Sem,stf_ID,created_date) VALUES (@blockName,@blockLevel,@roomNo,@year,@sem,@stfID,@date);SELECT SCOPE_IDENTITY();", objConn)
            cmd.Parameters.AddWithValue("@blockName", ddlBlock_Name.SelectedValue)
            cmd.Parameters.AddWithValue("@blockLevel", ddlBlock_Level.SelectedValue)
            cmd.Parameters.AddWithValue("@roomNo", txtRoomQuantity.Text)
            cmd.Parameters.AddWithValue("@year", ddlYear.SelectedValue)
            cmd.Parameters.AddWithValue("@sem", ddlSem.SelectedValue)
            cmd.Parameters.AddWithValue("@stfID", stfID)
            cmd.Parameters.AddWithValue("@date", Date.Now)
            objConn.Open()
            hostelID = cmd.ExecuteScalar()
            objConn.Close()
        End Using
        Return hostelID
    End Function

    Protected Function createRoom(hostelID As String, stfID As String) As Boolean
        Dim roomName As String = "{0}-{1}-{2}"
        For i As Integer = 0 To Integer.Parse(txtRoomQuantity.Text.Trim) - 1
            Using cmd As New SqlCommand("INSERT INTO room_info(hostel_ID,room_Name,room_Capacity,year,room_Sem,stf_ID,created_date) VALUES(@hostelID,@roomName,@roomCap,@year,@roomSem,@stfID,@date)", objConn)
                cmd.Parameters.AddWithValue("@hostelID", hostelID)
                cmd.Parameters.AddWithValue("@roomName", String.Format(roomName, ddlBlock_Name.SelectedValue, ddlBlock_Level.SelectedValue, (i + 1)))
                cmd.Parameters.AddWithValue("@roomCap", "2")
                cmd.Parameters.AddWithValue("@year", ddlYear.SelectedValue)
                cmd.Parameters.AddWithValue("@roomSem", ddlSem.SelectedValue)
                cmd.Parameters.AddWithValue("@stfID", stfID)
                cmd.Parameters.AddWithValue("@date", Date.Now)
                objConn.Open()
                cmd.ExecuteNonQuery()
                objConn.Close()
            End Using
        Next
        Return True
    End Function

    Protected Sub loadRegisteredHostel(hostelID As String)
        Dim query = "SELECT 
	                    hostel_info.hostel_ID,
	                    hostel_info.hostel_RoomNumbers,
	                    a.Parameter AS HostelName,
	                    b.Parameter AS HostelLevel,
	                    c.Parameter AS HostelSem,
	                    hostel_info.year AS HostelYear,
	                    hostel_info.created_date AS HostelDateCreated,
	                    room_info.room_ID,
	                    room_info.room_Name,
	                    room_info.room_Capacity AS RoomCapacity,
	                    d.Parameter AS RoomSem,
	                    room_info.year,
	                    room_info.created_date AS RoomDateCreated,
	                    staff_info.staff_Name
                    FROM 
	                    hostel_info 
	                    JOIN room_info ON room_info.hostel_ID=hostel_info.hostel_ID
	                    JOIN setting AS a ON a.Value = hostel_info.hostel_BlockNames
	                    JOIN setting AS b ON b.Value = hostel_info.hostel_BlockLevels
	                    JOIN setting AS c ON c.Value = hostel_info.hostel_Sem
	                    JOIN setting AS d ON d.Value = room_info.room_Sem
	                    JOIN staff_Info ON staff_Info.stf_ID = hostel_info.stf_ID
                    WHERE hostel_info.hostel_ID='" & hostelID & "'"
        Try

            Dim sqlAdapter As New SqlDataAdapter(query, objConn)
            Dim ds As New DataSet
            sqlAdapter.Fill(ds, "AnyTable")
            lblHostelName.Text = ds.Tables(0).Rows(0).Item("HostelName")
            lblHostelLevel.Text = ds.Tables(0).Rows(0).Item("HostelLevel")
            lblHostelYear.Text = ds.Tables(0).Rows(0).Item("HostelYear")
            lblHostelSem.Text = ds.Tables(0).Rows(0).Item("HostelSem")
            lblHostelRoom.Text = ds.Tables(0).Rows(0).Item("hostel_RoomNumbers")
            lblCreatedDate.Text = ds.Tables(0).Rows(0).Item("HostelDateCreated")
            lblCreatedBy.Text = ds.Tables(0).Rows(0).Item("staff_Name")
            datRespondent.DataSource = ds
            datRespondent.DataBind()
        Catch ex As Exception

        End Try
        roomDiv.Visible = True
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class