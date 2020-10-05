Imports System.Data.SqlClient

Public Class admin_daftar_koordinator
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

                Dim data As String = oCommon.securityLogin(Request.QueryString("admin_ID"))

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then
                    result = Request.QueryString("result")

                    If result = 1 Then
                        ShowMessage("Add New Coordinator", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Please Select Year", MessageType.Error)
                    ElseIf result = 3 Then
                        ShowMessage("Please Select Staff", MessageType.Error)
                    ElseIf result = 4 Then
                        ShowMessage("Please Select Course", MessageType.Error)
                    ElseIf result = 5 Then
                        ShowMessage("Please Select Subject", MessageType.Error)
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