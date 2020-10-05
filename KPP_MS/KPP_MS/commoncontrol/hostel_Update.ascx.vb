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

                'make sure the data read is integer(as for id from db).
                If Not Regex.IsMatch(Request.QueryString("hostelID"), "^[0-9 ]+$") Then
                    Response.Redirect("admin_pengurusan_am_hostel.aspx?admin_ID=" + Request.QueryString("admin_ID"))
                Else
                    'validated.
                    block_name_list()
                    block_level_list()
                    year_list()
                    sem_list()

                    LoadHostelInfo(Request.QueryString("hostelID"))
                    ListRoom()
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub block_name_list()
        strSQL = "SELECT Parameter,Value from setting where Type = 'Block_Name' and Parameter is not null "
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
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub sem_list()
        Using cmd As New SqlCommand("SELECT Parameter,Value FROM setting WHERE Type='Sem'", objConn)
            objConn.Open()
            ddlSem.DataSource = cmd.ExecuteReader
            ddlSem.DataTextField = "Parameter"
            ddlSem.DataValueField = "Value"
            ddlSem.DataBind()
            ddlSem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlSem.SelectedIndex = 0
            objConn.Close()
        End Using
    End Sub

    Private Sub LoadHostelInfo(ByVal hostelID As String)
        strSQL = "SELECT * FROM hostel_info WHERE hostel_ID = '" & hostelID & "'"

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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_BlockNames")) Then
                    ddlBlock_Name.SelectedValue = ds.Tables(0).Rows(0).Item("hostel_BlockNames")
                Else
                    ddlBlock_Name.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_BlockLevels")) Then
                    ddlBlock_Level.SelectedValue = ds.Tables(0).Rows(0).Item("hostel_BlockLevels")
                Else
                    ddlBlock_Level.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                    ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
                Else
                    ddlYear.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_Sem")) Then
                    ddlSem.SelectedValue = ds.Tables(0).Rows(0).Item("hostel_Sem")
                Else
                    ddlSem.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("hostel_RoomNumbers")) Then
                    roomNumbers.Text = ds.Tables(0).Rows(0).Item("hostel_RoomNumbers")
                Else
                    roomNumbers.Text = ""
                End If
                ddlBlock_Level.Enabled = False
                ddlBlock_Name.Enabled = False
                ddlYear.Enabled = False
                ddlSem.Enabled = False
                roomNumbers.Enabled = False
            Else
                Debug.WriteLine("Table hostel_info return no data...")
            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    'Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
    '    ''UPDATE room_info
    '    'strSQL = "UPDATE room_info SET room_Name ='" & room_Name.Text & "',roomNumbers='" & roomNumbers.Text & "',year = '" & ddlYear.SelectedValue & "' WHERE room_ID ='" & Request.QueryString("room_ID") & "'"
    '    'strRet = oCommon.ExecuteSQL(strSQL)

    '    ''get hostel id from that room id
    '    'Dim hostelExist As String = "select hostel_ID from room_info where room_ID = '" & Request.QueryString("room_ID") & "'"
    '    'Dim dataHostelExist As String = getFieldValue(hostelExist, strConn)

    '    ''UPDATE hostel_info
    '    'strSQL = "UPDATE hostel_info SET hostel_Name ='" & ddlHostelName.SelectedValue & "', block_Name='" & ddlBlock_Name.SelectedValue & "', block_Level='" & ddlBlock_Level.SelectedValue & "',year = '" & ddlYear.SelectedValue & "' WHERE hostel_ID ='" & dataHostelExist & "'"
    '    'strRet = oCommon.ExecuteSQL(strSQL)
    '    'Update hostel info

    '    If strRet = "0" Then
    '        Response.Redirect("admin_pengurusan_am_hostel.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID") + "")
    '    Else
    '        Response.Redirect("admin_pengurusan_am_hostel.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID") + "")
    '    End If
    'End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_pengurusan_am_hostel.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub ListRoom()
        Dim hostelID As String = Request.QueryString("hostelID")
        Using cmd As New SqlCommand("SELECT 
	                                    room_info.room_ID,
	                                    room_info.room_Name,
	                                    room_info.room_Capacity,
	                                    d.Parameter AS RoomSem,
	                                    room_info.year,
	                                    room_info.created_date
                                    FROM 
	                                    room_info
	                                    JOIN hostel_info ON hostel_info.hostel_ID = room_info.hostel_ID
	                                    JOIN setting AS d ON d.Value = room_info.room_Sem
                                    WHERE hostel_info.hostel_ID = @hostelID", objConn)
            cmd.Parameters.AddWithValue("@hostelID", hostelID)
            objConn.Open()
            floorInfo.DataSource = cmd.ExecuteReader()
            floorInfo.DataBind()
            objConn.Close()
        End Using
    End Sub

    Protected Sub UpdateRoom(sender As Object, e As EventArgs)
        Dim item As RepeaterItem = TryCast(TryCast(sender, Button).Parent, RepeaterItem)
        Dim roomID As String = TryCast(item.FindControl("roomID"), HiddenField).Value
        Dim roomName As String = TryCast(item.FindControl("txtRoomName"), TextBox).Text.Trim()
        Dim roomCapacity As String = TryCast(item.FindControl("txtRoomCapacity"), TextBox).Text.Trim()

        If roomID <> "" And Regex.IsMatch(roomID, "^[0-9 ]+$") Then
            If roomName <> "" Then
                If roomCapacity <> "" And Regex.IsMatch(roomCapacity, "^[0-9 ]+$") Then
                    Dim query = String.Format("UPDATE room_info SET room_Name='{1}', room_Capacity='{2}' WHERE room_ID='{0}'", roomID, roomName, roomCapacity)
                    strRet = oCommon.ExecuteSQL(query)
                    If strRet <> "" Then
                        ListRoom()
                        ShowMessage("Succesfully insert new data", MessageType.Success)
                    Else
                        ShowMessage("Error saving update", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please enter room capacity correctly", MessageType.Error)
                End If
            Else
                ShowMessage("Please enter room name correctly", MessageType.Error)
            End If
        Else
            ShowMessage("Room not find", MessageType.Error)
        End If
    End Sub

    Protected Sub DeleteRoom(sender As Object, e As EventArgs)
        Dim item As RepeaterItem = TryCast(TryCast(sender, Button).Parent, RepeaterItem) 'get refered repeateditem.
        Dim roomID As String = TryCast(item.FindControl("roomID"), HiddenField).Value 'get room id.
        'get totalroom saved fro the hostel floor.
        Dim totalRoom As Integer = Integer.Parse(oCommon.getFieldValue("SELECT COUNT(room_ID) FROM room_info WHERE hostel_ID='" & Request.QueryString("hostelID") & "'"))

        If roomID <> "" And Regex.IsMatch(roomID, "^[0-9 ]+$") Then
            'delete room.
            Dim dltSuccess = oCommon.ExecuteSQL("DELETE FROM room_info WHERE room_ID = '" & roomID & "'")
            If dltSuccess <> "" Then
                totalRoom -= 1
                'make sure value is positive.
                If totalRoom < 0 Then
                    totalRoom = 0
                End If
                'update room floor.
                Dim updtRoom = oCommon.ExecuteSQL("UPDATE hostel_info SET hostel_RoomNumbers='" & totalRoom.ToString & "' WHERE hostel_ID='" & Request.QueryString("hostelID") & "'")
                If updtRoom <> "" Then
                    'reload data.
                    LoadHostelInfo(Request.QueryString("hostelID"))
                    ListRoom()
                Else
                    ShowMessage("Update hostel failed", MessageType.Error)
                End If
            End If
        Else
            ShowMessage("Room not found", MessageType.Error)
        End If

    End Sub

    Protected Sub AddNewRoom(sender As Object, e As EventArgs)
        Dim hostelID = Request.QueryString("hostelID")
        Dim totalRoom As Integer = getTotalRoom()
        Dim hostelYear As String = oCommon.getFieldValue(String.Format("SELECT year FROM hostel_info WHERE hostel_ID='{0}'", hostelID))
        Dim hostelSem As String = oCommon.getFieldValue(String.Format("SELECT hostel_Sem FROM hostel_info WHERE hostel_ID='{0}'", hostelID))
        Dim stfID As String = oCommon.getFieldValue(String.Format("SELECT stf_ID from security_ID WHERE loginID_Number='{0}'", Request.QueryString("admin_ID")))
        Dim roomName = txtRoomName.Text
        Dim roomCapacity = txtRoomCapacity.Text
        Dim query As String = ""
        If hostelID <> "" Then
            If roomName <> "" Then
                If roomCapacity <> "" Then
                    query = String.Format("INSERT INTO room_info (hostel_ID,room_Name,room_Capacity,year,room_Sem,stf_ID,created_date) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", hostelID, roomName, roomCapacity, hostelYear, hostelSem, stfID, DateTime.Now)
                    Dim insrtSuccess = oCommon.ExecuteSQL(query)
                    If insrtSuccess = "0" Then
                        ShowMessage("Create success.", MessageType.Success)
                        ListRoom()
                        totalRoom += 1
                        query = String.Format("UPDATE hostel_info SET hostel_RoomNumbers='{0}' WHERE hostel_ID='{1}'", totalRoom, hostelID)
                        Dim updtSucccess = oCommon.ExecuteSQL(query)
                        If updtSucccess = "0" Then
                            txtRoomName.Text = ""
                            txtRoomCapacity.Text = ""
                            LoadHostelInfo(hostelID)
                        End If
                    Else
                        ShowMessage("Create failed!", MessageType.Error)
                    End If
                Else

                End If
            Else

            End If
        Else

        End If
    End Sub

    Protected Function getTotalRoom() As Integer
        Dim totalRoom As Integer = 0
        Using cmd As New SqlCommand("SELECT hostel_RoomNumbers FROM hostel_info WHERE hostel_ID=@hostelID", objConn)
            Try
                objConn.Open()
                cmd.Parameters.AddWithValue("@hostelID", Request.QueryString("hostelID"))
                Using objReader As SqlDataReader = cmd.ExecuteReader
                    While objReader.Read
                        totalRoom = Integer.Parse(objReader("hostel_RoomNumbers"))
                    End While
                End Using
                objConn.Close()
            Catch ex As Exception
                Debug.WriteLine("Loct: getTotalRoom, Error: " & ex.Message)
            End Try
        End Using
        Return totalRoom
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class