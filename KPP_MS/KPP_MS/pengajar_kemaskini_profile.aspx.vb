Imports System.Data.SqlClient

Public Class pengajar_kemaskini_profile
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

                Dim id As String = Request.QueryString("stf_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then
                    result = Request.QueryString("result")

                    If result = 1 Then
                        ShowMessage("Update Staff Profile", MessageType.Success)

                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)

                    ElseIf result = 2 Then
                        ShowMessage("Please enter a valid staff name", MessageType.Error)

                    ElseIf result = 3 Then
                        ShowMessage("Please enter a valid Mykad", MessageType.Error)

                    ElseIf result = 4 Then
                        ShowMessage("Please enter a valid staff Id", MessageType.Error)

                    ElseIf result = 5 Then
                        ShowMessage("Please enter a valid staff email", MessageType.Error)

                    ElseIf result = 6 Then
                        ShowMessage("Please enter staff sex", MessageType.Error)

                    ElseIf result = 7 Then
                        ShowMessage("Please enter a valid mobile number", MessageType.Error)

                    ElseIf result = 8 Then
                        ShowMessage("Please enter a city", MessageType.Error)


                    ElseIf result = 9 Then
                        ShowMessage("Please enter a state", MessageType.Error)

                    ElseIf result = 10 Then
                        ShowMessage("Please enter a valid posscode", MessageType.Error)

                    ElseIf result = 11 Then
                        ShowMessage("Please enter a position", MessageType.Error)

                    ElseIf result = 12 Then
                        ShowMessage("Cannot have same position", MessageType.Error)
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