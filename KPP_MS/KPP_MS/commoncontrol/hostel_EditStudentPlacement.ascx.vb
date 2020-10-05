Imports System.Data.SqlClient

Public Class hostel_EditStudentPlacement
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.load
        If Not IsPostBack Then
            loadPage()
        End If
    End Sub

    Protected Sub loadPage()
        Dim roomID = Request.QueryString("roomID")
        Dim adminSecID = Request.QueryString("admin_ID")
        ddlSetting(ddlHostelYear, "Year")
        ddlSetting(ddlBlockName, "Block_Name")
        ddlSetting(ddlBlockLevel, "Block_Level")
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
                        hostel_BlockLevels = @blockLvl"
        Using cmd As New SqlCommand(query, objConn)
            Try
                cmd.Parameters.AddWithValue("@year", ddlHostelYear.SelectedValue)
                cmd.Parameters.AddWithValue("@blockName", ddlBlockName.SelectedValue)
                cmd.Parameters.AddWithValue("@blockLvl", ddlBlockLevel.SelectedValue)
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
End Class