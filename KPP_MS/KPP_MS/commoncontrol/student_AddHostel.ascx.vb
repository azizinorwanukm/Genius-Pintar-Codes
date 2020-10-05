Imports System.Data.SqlClient

Public Class student_AddHostel
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim roomCap As Integer
    Dim roomOccupied As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                load_page()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        ddlSetting(ddlHostelYear, "Year")
        ddlSetting(ddlBlockName, "Block_Name")
        ddlSetting(ddlBlockLevel, "Block_Level")
        ddlSetting(ddlHostelSem, "Sem")
        ddlRoomName.Enabled = False
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Sub ddlSetting(ddl As DropDownList, type As String)
        Using cmd As New SqlCommand("SELECT Parameter, Value FROM setting WHERE Type=@type", objConn)
            cmd.Parameters.AddWithValue("@type", type)
            Try
                objConn.Open()
                ddl.DataSource = cmd.ExecuteReader
                ddl.DataTextField = "Parameter"
                ddl.DataValueField = "Value"
                ddl.DataBind()
                objConn.Close()
                Select Case type
                    Case "Year"
                        ddl.Items.Insert(0, New ListItem("Year..."))
                        ddl.SelectedValue = Date.Now.Year
                    Case "Block_Name"
                        ddl.Items.Insert(0, New ListItem("Block Name..."))
                    Case "Block_Level"
                        ddl.Items.Insert(0, New ListItem("Block Level..."))
                    Case "Sem"
                        ddl.Items.Insert(0, New ListItem("Semesters..."))
                    Case "Level"
                        ddl.Items.Insert(0, New ListItem("Student Levels..."))
                    Case Else
                        Return
                End Select
            Catch ex As Exception
                Debug.WriteLine("(ddlSetting)Err:" & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub fillRoomName()
        Dim query As String = "SELECT 
                        room_info.room_ID, 
                        room_info.room_Name 
                    FROM 
                        room_info JOIN hostel_info ON room_info.hostel_ID = hostel_info.hostel_ID 
                    WHERE 
                        hostel_info.year = @year AND 
                        hostel_BlockNames=@blockName AND 
                        hostel_BlockLevels = @blockLvl AND 
                    hostel_info.hostel_Sem = @sem"
        Using cmd As New SqlCommand(query, objConn)
            Try
                cmd.Parameters.AddWithValue("@year", ddlHostelYear.SelectedValue)
                cmd.Parameters.AddWithValue("@blockName", ddlBlockName.SelectedValue)
                cmd.Parameters.AddWithValue("@blockLvl", ddlBlockLevel.SelectedValue)
                cmd.Parameters.AddWithValue("@sem", ddlHostelSem.SelectedValue)
                objConn.Open()
                ddlRoomName.DataSource = cmd.ExecuteReader
                ddlRoomName.DataTextField = "room_Name"
                ddlRoomName.DataValueField = "room_ID"
                ddlRoomName.DataBind()
                objConn.Close()
                ddlRoomName.Items.Insert(0, New ListItem("Room Name..."))

            Catch ex As Exception
                Debug.WriteLine("(fillRoomName)Err:" & ex.Message)
            End Try
        End Using
    End Sub

    Private Function getHostelID()
        Dim hostelID As String = ""
        If ddlHostelYear.SelectedIndex > 0 Then
            If ddlBlockName.SelectedIndex > 0 Then
                If ddlBlockLevel.SelectedIndex > 0 Then
                    If ddlHostelSem.SelectedIndex > 0 Then
                        Using cmd As New SqlCommand("SELECT hostel_ID FROM hostel_info WHERE hostel_BlockNames=@blockName AND hostel_BlockLevels=@blockLvl AND year=@year AND hostel_Sem=@sem", objConn)
                            cmd.Parameters.AddWithValue("@blockName", ddlBlockName.SelectedValue)
                            cmd.Parameters.AddWithValue("@blockLvl", ddlBlockLevel.SelectedValue)
                            cmd.Parameters.AddWithValue("@year", ddlHostelYear.SelectedValue)
                            cmd.Parameters.AddWithValue("@sem", ddlHostelSem.SelectedValue)
                            Try
                                objConn.Open()
                                Dim reader = cmd.ExecuteReader
                                If reader.HasRows Then
                                    While reader.Read
                                        hostelID = reader("hostel_ID")
                                    End While
                                End If
                                objConn.Close()
                            Catch ex As Exception
                                Debug.WriteLine("(getHostelID) Err:" & ex.Message)
                            End Try
                        End Using
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Else
            Return ""
        End If
        Return hostelID
    End Function

    Private Sub fillTable()
        Dim query = getSQL()
        Using cmd As New SqlCommand(query, objConn)
            Try
                If ddlHostelYear.SelectedIndex > 0 Then
                    cmd.Parameters.AddWithValue("@year", ddlHostelYear.SelectedValue)
                End If
                If ddlHostelSem.SelectedIndex > 0 Then
                    cmd.Parameters.AddWithValue("@sem", ddlHostelSem.SelectedValue)
                End If
                objConn.Open()
                datRespondent.DataSource = cmd.ExecuteReader
                datRespondent.DataBind()
                objConn.Close()
            Catch ex As Exception
                Debug.WriteLine("(fillTable) Err:" & ex.Message)
            End Try
        End Using
    End Sub

    Private Function getSQL()
        Dim query As String = ""
        Dim selectStr As String = "SELECT 
                                    student_room.id AS StudentRoomID,
                                    student_info.std_ID AS STDID,
                                    student_info.student_Name AS StudentName, 
                                    a.Parameter AS SettingBlockName, 
                                    b.Parameter AS SettingBlockLevel, 
                                    room_info.room_Name AS RoomName, 
                                    c.Parameter AS SettingSem, 
                                    student_level.year AS StudentLevelYear, 
                                    student_level.student_Level AS StudentLevelLevel 
                                  FROM 
                                    student_info 
                                    LEFT JOIN student_level ON student_info.std_ID=student_level.std_ID 
                                    LEFT JOIN student_room ON student_level.std_ID = student_room.std_ID 
                                        AND student_level.student_Sem=student_room.sem 
                                    LEFT JOIN room_info ON student_room.room_ID = room_info.room_ID 
                                    LEFT JOIN hostel_info ON room_info.hostel_id=hostel_info.hostel_ID 
                                    LEFT JOIN setting AS a ON a.Value = hostel_info.hostel_BlockNames 
                                    LEFT JOIN setting AS b ON b.Value = hostel_info.hostel_BlockLevels 
                                    LEFT JOIN setting AS c ON c.Value = student_level.student_Sem"
        Dim whereStr As String = " WHERE student_info.student_Name IS NOT NULL"

        If ddlHostelYear.SelectedIndex > 0 Then
            whereStr += " AND student_Level.year = @year"
        End If

        If ddlHostelSem.SelectedIndex > 0 Then
            whereStr += " AND student_Level.student_Sem = @sem"
        End If

        If dddlFilterTable.SelectedIndex > 0 Then
            If dddlFilterTable.SelectedValue = "1" Then
                whereStr += " AND room_info.room_Name IS NOT NULL"
            Else
                whereStr += " AND room_info.room_Name IS NULL"
            End If
        End If

        If txtstudent.Text.Length > 0 Then
            whereStr += " And student_info.student_ID LIKE '%" & txtstudent.Text & "%' OR student_info.student_Name LIKE '%" & txtstudent.Text & "%' OR student_info.student_Mykad LIKE '%" & txtstudent.Text & "%'"
        End If

        Dim orderStr As String = " Order By student_info.student_Name ASC"
        query = selectStr & whereStr & orderStr
        Return query
    End Function

    Private Function getChecklist() As ArrayList
        Dim stdIDs As New ArrayList
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim chckBox = TryCast(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            If chckBox.Checked = True Then
                stdIDs.Add(datRespondent.DataKeys(i).Value.ToString)
            End If
        Next
        Return stdIDs
    End Function

    Private Function getStfID() As String
        Debug.WriteLine("Funct. Called: getStfID...")
        Dim stfID = ""
        If Request.QueryString("admin_ID").Length > 0 Then
            Using cmd As New SqlCommand("SELECT stf_ID FROM security_ID WHERE loginID_Number=@logID", objConn)
                cmd.Parameters.AddWithValue("@logID", Request.QueryString("admin_ID"))
                Try
                    objConn.Open()
                    Dim reader = cmd.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            stfID = reader("stf_ID")
                        End While
                    End If
                Catch ex As Exception
                    Debug.WriteLine("(getStfID) Err: " & ex.Message)
                Finally
                    objConn.Close()
                End Try
            End Using
            Return stfID
        End If
        Return stfID
    End Function

    Private Function assignStudent(studID As String, hostelID As String) As Boolean
        Debug.WriteLine("Funct. Called: assignStudnet<stdID:" & studID & ">...")
        'Check if stdID and hostelID is not null and querystring available.
        If studID <> "" And hostelID <> "" And getStfID() <> "" Then
            Using cmd As New SqlCommand("INSERT INTO student_room(std_ID, room_ID,year,sem,stf_ID,date_assigned) VALUES(@stdID, @roomID, @year, @sem,@stfID,@date) ", objConn)
                cmd.Parameters.AddWithValue("@stdID", studID)
                cmd.Parameters.AddWithValue("@roomID", ddlRoomName.SelectedValue)
                cmd.Parameters.AddWithValue("@year", ddlHostelYear.SelectedValue)
                cmd.Parameters.AddWithValue("@sem", ddlHostelSem.SelectedValue)
                cmd.Parameters.AddWithValue("@stfID", getStfID())
                cmd.Parameters.AddWithValue("@date", Date.Now)
                Try
                    objConn.Open()
                    Dim status = cmd.ExecuteNonQuery()
                    objConn.Close()
                    If status <> 0 Then
                        Return True
                    Else
                        Return False
                    End If
                    Return True
                Catch ex As Exception
                    Debug.WriteLine("(assignStudent) Err: " & ex.Message)
                    Return False
                End Try
            End Using
        Else
            'return false
            Return False
        End If
    End Function

    Private Function checkReg(stdID As String) As Boolean
        Debug.WriteLine("Funct. Called: checkReg... ")
        Dim hasReg As Boolean = False
        If stdID <> "" Then
            If ddlRoomName.SelectedValue > 0 Then
                Using cmd As New SqlCommand("SELECT id FROM student_room WHERE std_ID=@stdID AND room_ID=@roomID", objConn)
                    cmd.Parameters.AddWithValue("@stdID", stdID)
                    cmd.Parameters.AddWithValue("@roomID", ddlRoomName.SelectedValue)
                    Try
                        objConn.Open()
                        Dim id As String = cmd.ExecuteScalar()
                        objConn.Close()
                        If id <> "" Then
                            hasReg = True
                        End If
                    Catch ex As Exception
                        Debug.WriteLine("(checkReg) Err: " & ex.Message)
                    End Try
                    Return hasReg
                End Using
            Else
                ShowMessage("Please select the hostel's room to be assigned for the student(s).", MessageType.Error)
                Debug.WriteLine("(checkReg) Err: Room not selected.")
                Return True
            End If
        Else
            Debug.WriteLine("(checkReg) Err: StdID null.")
            Return True
        End If
    End Function

    Private Function checkRoom() As Boolean
        Debug.WriteLine("Funct. Called: checkRoom...")
        Dim totalFilled = Integer.Parse(oCommon.getFieldValue(String.Format("SELECT COUNT(room_ID) FROM student_room WHERE room_ID = '{0}'", ddlRoomName.SelectedValue)))
        Dim roomMaxCap = Integer.Parse(oCommon.getFieldValue(String.Format("SELECT room_Capacity FROM room_info WHERE room_ID = '{0}'", ddlRoomName.SelectedValue)))
        roomOccupied = totalFilled
        roomCap = roomMaxCap
        If totalFilled >= roomMaxCap Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function roomLimit(count As Integer)
        Debug.WriteLine("Funct. Called: roomLimit...")
        Dim roomMaxCap = Integer.Parse(oCommon.getFieldValue(String.Format("SELECT room_Capacity FROM room_info WHERE room_ID = '{0}'", ddlRoomName.SelectedValue)))
        If roomMaxCap >= count Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function getName(stdID As String)
        Return oCommon.getFieldValue("SELECT student_Name from student_info WHERE std_ID='" & stdID.ToString & "'")
    End Function

    Private Sub roomInfo()
        Dim roomFilled As String = oCommon.getFieldValue("SELECT Count(id) FROM student_room WHERE room_ID='" & ddlRoomName.SelectedValue & "' AND year='" & ddlHostelYear.SelectedValue & "' AND sem='" & ddlHostelSem.SelectedValue & "'")
        Dim roomCap As String = oCommon.getFieldValue("SELECT room_Capacity FROM room_info WHERE room_ID='" & ddlRoomName.SelectedValue & "'")
        lblRoomName.Text = ddlRoomName.SelectedItem.Text
        lblRoomAvailability.Text = roomFilled & " / " & roomCap
        If roomFilled.Equals(roomCap) Then
            lblRoomAvailability.Text += " (Full)"
        End If
        If divRoomInfo.Visible = False Then
            divRoomInfo.Visible = True
        End If
    End Sub

    '--------------------------------------------------------------------------------------------------------------------------

    Private Sub ddlHostelYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelYear.SelectedIndexChanged
        fillRoomName()
        fillTable()
    End Sub

    Private Sub ddlBlockName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockName.SelectedIndexChanged
        fillRoomName()
    End Sub

    Private Sub ddlBlockLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBlockLevel.SelectedIndexChanged
        fillRoomName()
    End Sub

    Private Sub ddlHostelSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHostelSem.SelectedIndexChanged
        fillRoomName()
        ddlRoomName.Enabled = True
        fillTable()
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        If ddlHostelYear.SelectedIndex > 0 Then
            fillTable()
        End If
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim stdIDs As ArrayList = getChecklist()
        Dim hostelID As String = getHostelID()
        If hostelID <> "" And ddlRoomName.SelectedIndex > 0 Then
            If stdIDs.Count > 0 Then
                If checkRoom() Then
                    If roomLimit(stdIDs.Count) Then
                        For Each stdid In stdIDs
                            If checkReg(stdid) = False Then
                                If assignStudent(stdid, hostelID) Then
                                    ShowMessage("Success: " & getName(stdid), MessageType.Success)
                                Else
                                    ShowMessage("Failed: " & getName(stdid), MessageType.Error)
                                    Debug.WriteLine("| StdID:< " & stdid & " >|")
                                End If
                            Else
                                ShowMessage("Student " & getName(stdid) & " has registered to this room.", MessageType.Error)
                            End If
                        Next
                    Else
                        ShowMessage("Total student selected are more that the available space in that room.", MessageType.Error)
                    End If
                Else
                    ShowMessage("Room full, please select another room to assign.", MessageType.Error)
                End If
            Else
                ShowMessage("Please select a student to be assigned a room.", MessageType.Error)
            End If
        Else
            ShowMessage("Please select the hostel's room to be assigned for the student(s).", MessageType.Error)
        End If
        fillTable()
    End Sub

    Private Sub dddlFilterTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dddlFilterTable.SelectedIndexChanged
        fillTable()
    End Sub

    Private Sub ddlRoomName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoomName.SelectedIndexChanged
        roomInfo()
    End Sub

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        Dim stdIDs = getChecklist()
        If e.CommandName = "changeRoom" Then
            If ddlRoomName.SelectedIndex > 0 Then
                If checkRoom() Then
                    Using cmd As New SqlCommand("UPDATE student_room SET room_ID=@roomID WHERE id=@id", objConn)
                        cmd.Parameters.AddWithValue("@id", e.CommandArgument.ToString)
                        cmd.Parameters.AddWithValue("@roomID", ddlRoomName.SelectedValue)
                        Try
                            objConn.Open()
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            Debug.WriteLine("(dateRespondent_RowCommand) Err: " & ex.Message)
                        Finally
                            ShowMessage("Relocate successful.", MessageType.Success)
                            objConn.Close()
                        End Try
                    End Using
                Else
                    ShowMessage("Room full. Please choose different room to relocate.", MessageType.Error)
                End If
            Else
                ShowMessage("Please select a room for the student to be relocate to.", MessageType.Error)
            End If
        End If
        fillTable()
    End Sub
End Class