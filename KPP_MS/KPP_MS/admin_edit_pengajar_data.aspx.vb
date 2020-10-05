
Imports System.Data.SqlClient

Public Class admin_edit_pengajar_data
    Inherits System.Web.UI.Page
    Dim strSQL As String = ""
    Dim strRet As String = ""
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

                    '' student detail error
                    If result = 1 Then
                        ShowMessage("Update Staff Data", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Please enter a valid staff name", MessageType.Error)
                    ElseIf result = 3 Then
                        ShowMessage("Please enter a valid staff identification card", MessageType.Error)
                    ElseIf result = 4 Then
                        ShowMessage("Please enter a valid staff id", MessageType.Error)
                    ElseIf result = 5 Then
                        ShowMessage("Please enter a valid staff email", MessageType.Error)
                    ElseIf result = 6 Then
                        ShowMessage("Please enter a valid staff gender", MessageType.Error)
                    ElseIf result = 7 Then
                        ShowMessage("Please enter a valid phone number", MessageType.Error)
                    ElseIf result = 8 Then
                        ShowMessage("Please enter a valid city", MessageType.Error)
                    ElseIf result = 9 Then
                        ShowMessage("Please enter a valid state", MessageType.Error)
                    ElseIf result = 10 Then
                        ShowMessage("Please enter a valid postal code", MessageType.Error)
                    ElseIf result = 11 Then
                        ShowMessage("Please enter a valid staff position", MessageType.Error)
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