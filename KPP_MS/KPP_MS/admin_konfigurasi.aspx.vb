Imports System.Data.SqlClient

Public Class admin_konfigurasi
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

                    If result = 1 Then
                        ShowMessage("Register New Setting", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Please enter a valid school id", MessageType.Error)
                    ElseIf result = 3 Then
                        ShowMessage("Please enter a valid parameter ", MessageType.Error)
                    ElseIf result = 4 Then
                        ShowMessage("Please enter a valid value", MessageType.Error)
                    ElseIf result = 5 Then
                        ShowMessage("Please enter a valid code", MessageType.Error)
                    ElseIf result = 6 Then
                        ShowMessage("Please enter a valid type", MessageType.Error)
                    ElseIf result = 7 Then
                        ShowMessage("Please enter a valid description", MessageType.Error)
                    ElseIf result = 8 Then
                        ShowMessage("Please enter a valid idx", MessageType.Error)
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