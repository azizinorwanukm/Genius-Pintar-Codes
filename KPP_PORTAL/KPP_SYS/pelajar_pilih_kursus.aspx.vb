Imports System.Data.SqlClient

Public Class pelajar_pilih_kursus
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

                Dim id As String = Request.QueryString("std_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then
                    result = Request.QueryString("result")

                    '' student detail error
                    If result = 1 Then
                        ShowMessage(" Register Course", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("You Alreaday Register Another Third Language Course", MessageType.Error)
                    ElseIf result = 3 Then
                        ShowMessage("Course Already Full", MessageType.Error)
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