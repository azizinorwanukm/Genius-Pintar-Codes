Imports System.Data.SqlClient

Public Class pengajar_kehadiran_pelajar
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
        Dim strRet As String = ""

        Dim result As Integer = 0
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                If Not IsPostBack Then
                    result = Request.QueryString("result")

                    If result = 1 Then
                        ShowMessage("Added Student Attendance", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Data already added", MessageType.Error)
                    ElseIf result = 3 Then
                        ShowMessage("Data has been updated", MessageType.Success)
                    ElseIf result = 4 Then
                        ShowMessage("Data already exist", MessageType.Error)
                    ElseIf result = 6 Then
                        ShowMessage("Please select semester", MessageType.Error)
                    ElseIf result = 7 Then
                        ShowMessage("Please select class name ", MessageType.Error)
                    ElseIf result = 8 Then
                        ShowMessage("Please select subject name", MessageType.Error)
                    ElseIf result = 9 Then
                        ShowMessage("Please select date", MessageType.Error)

                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub

        Protected Sub ShowMessage(Message As String, type As MessageType)
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
        End Sub

        Public Enum MessageType
            Success
            [Error]
        End Enum
    End Class