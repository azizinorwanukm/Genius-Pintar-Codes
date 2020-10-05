Imports System.Data.SqlClient

Public Class admin_view_disiplin
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

                    If result = 10 Then
                        ShowMessage("Save/Update success", MessageType.Success)
                    ElseIf result = 1 Then
                        ShowMessage("Save failed", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Please select the staff reported.", MessageType.Error)
                    ElseIf result = 4 Then
                        ShowMessage("Please check the input of the student mykad", MessageType.Error)
                    ElseIf result = 5 Then
                        ShowMessage("Please check the input of the student name", MessageType.Error)
                    ElseIf result = 6 Then
                        ShowMessage("Please check the input of the date format", MessageType.Error)
                    ElseIf result = 7 Then
                        ShowMessage("No student with current id", MessageType.Error)
                    ElseIf result = 8 Then
                        ShowMessage("No student with current ic", MessageType.Error)
                    ElseIf result = 9 Then
                        ShowMessage("No staff that match the input data", MessageType.Error)
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