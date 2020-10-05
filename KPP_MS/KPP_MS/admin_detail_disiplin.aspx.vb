Imports System.Data.SqlClient

Public Class admin_detail_disiplin
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim result As Integer
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Dim v As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                v = Request.QueryString("v")
                If v = "1" Then
                    disiplinMultiView.ActiveViewIndex = 1
                ElseIf v = "2" Then
                    disiplinMultiView.ActiveViewIndex = 2
                Else
                    disiplinMultiView.ActiveViewIndex = 0
                End If

                Dim id As String = Request.QueryString("admin_ID")
                Dim data As String = oCommon.securityLogin(id)
                If data = "False" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then
                    result = Int32.Parse(Request.QueryString("result"))

                    If IsNumeric(result) Then
                        If result = 0 Then
                            ShowMessage("Save Data Successfull", MessageType.Success)
                        ElseIf result = 1 Then
                            ShowMessage("Person reported are required", MessageType.Error)
                        ElseIf result = 2 Then
                            ShowMessage("Case detail are required", MessageType.Error)
                        ElseIf result = 3 Then
                            ShowMessage("Case type are required", MessageType.Error)
                        ElseIf result = 4 Then
                            ShowMessage("Case action are required", MessageType.Error)
                        ElseIf result = 5 Then
                            ShowMessage("Demerit mark cannot be empty", MessageType.Error)
                        ElseIf result = 6 Then
                            ShowMessage("Case date are not in correct format", MessageType.Error)
                        ElseIf result = 7 Then
                            ShowMessage("Request error", MessageType.Error)
                        ElseIf result = 8 Then
                            ShowMessage("Failed to delete letter.", MessageType.Error)
                        ElseIf result = 9 Then
                            ShowMessage("Delete success", MessageType.Success)
                        ElseIf result = 12 Then
                            ShowMessage("Failed to save change", MessageType.Error)
                        ElseIf result = 13 Then
                            ShowMessage("Warning letter not exist OR counseling session not completed", MessageType.Error)
                        Else
                            ShowMessage("Unknown error", MessageType.Error)
                        End If
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