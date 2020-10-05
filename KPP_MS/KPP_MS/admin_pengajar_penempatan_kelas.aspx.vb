Imports System.Data.SqlClient

Public Class admin_pengajar_penempatan_kelas
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim Result As Integer = 0

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

                    Result = Request.QueryString("result")

                    If Result = 1 Then
                        ShowMessage("Register class and course", MessageType.Success)
                    ElseIf Result = 2 Then
                        ShowMessage("Unable to register class and course that is nore higher or lower level ", MessageType.Error)
                    ElseIf Result = -1 Then
                        ShowMessage("Error Register ", MessageType.Error)
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