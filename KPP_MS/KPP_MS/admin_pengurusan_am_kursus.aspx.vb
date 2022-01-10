Imports System.Data.SqlClient

Public Class admin_pengurusan_am_kursus
    Inherits System.Web.UI.Page

    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    result = Request.QueryString("result")

                    If result = 1 Then
                        ShowMessage("Add New Courses", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Please enter a valid course name", MessageType.Error)
                    ElseIf result = 3 Then
                        ShowMessage("Please enter a valid course code", MessageType.Error)
                    ElseIf result = 4 Then
                        ShowMessage("Please enter a valid course year", MessageType.Error)
                    ElseIf result = 5 Then
                        ShowMessage("Please enter a valid course type", MessageType.Error)
                    ElseIf result = 6 Then
                        ShowMessage("Please enter a valid course student year", MessageType.Error)
                    ElseIf result = 7 Then
                        ShowMessage("Please enter a valid course sem", MessageType.Error)
                    ElseIf result = 8 Then
                        ShowMessage("Please enter a valid course religion", MessageType.Error)
                    ElseIf result = 9 Then
                        ShowMessage("Please enter a valid course group", MessageType.Error)
                    Else

                    End If

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