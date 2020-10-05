Imports System.Data.SqlClient

Public Class admin_config_disiplin1
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objconn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                result = Request.QueryString("result")
                If result = 1 Then
                    ShowMessage("Succesfully insert new data", MessageType.Success)
                ElseIf result = -1 Then
                    ShowMessage("Unable to insert new data", MessageType.Error)
                ElseIf result = 2 Then
                    ShowMessage("plesae check the value of the compound that u had enter", MessageType.Error)
                ElseIf result = 3 Then
                    ShowMessage("please check the value of the merit that u had enter", MessageType.Error)
                ElseIf result = 4 Then
                    ShowMessage("please check the name of the case that u had enter", MessageType.Error)
                ElseIf result = 5 Then
                    ShowMessage("Update is Successfull", MessageType.Success)
                ElseIf result = 6 Then
                    ShowMessage("Update Fail", MessageType.Error)
                ElseIf result = 7 Then
                    ShowMessage("Delete Success", MessageType.Success)
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