Imports System.Data.SqlClient

Public Class admin_daftar_pelajar_baru
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
                        ShowMessage("Update Student Data", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Please enter a valid student level", MessageType.Error)
                    ElseIf result = 3 Then
                        ShowMessage("Please enter a valid student identification card", MessageType.Error)
                    ElseIf result = 4 Then
                        ShowMessage("Please enter a valid postcode", MessageType.Error)
                    ElseIf result = 5 Then
                        ShowMessage("Please enter a valid student name", MessageType.Error)
                    ElseIf result = 6 Then
                        ShowMessage("Please enter a valid phone number", MessageType.Error)
                    ElseIf result = 7 Then
                        ShowMessage("Please enter a valid city", MessageType.Error)
                    ElseIf result = 8 Then
                        ShowMessage("Please enter a valid state", MessageType.Error)
                    ElseIf result = 9 Then
                        ShowMessage("Please enter a valid gender", MessageType.Error)
                    ElseIf result = 10 Then
                        ShowMessage("Please enter a valid email address", MessageType.Error)
                    ElseIf result = 11 Then
                        ShowMessage("Please enter a valid student id", MessageType.Error)
                    ElseIf result = 12 Then
                        ShowMessage("Please enter a valid student year", MessageType.Error)
                    ElseIf result = 20 Then
                        ShowMessage("Please enter a valid guardian 1 name", MessageType.Error)
                    ElseIf result = 21 Then
                        ShowMessage("Please enter a valid guardian 1 identification card", MessageType.Error)
                    ElseIf result = 22 Then
                        ShowMessage("Please enter a valid guardian 1 email", MessageType.Error)
                    ElseIf result = 23 Then
                        ShowMessage("Please enter a valid guardian 1 phone number", MessageType.Error)
                    ElseIf result = 24 Then
                        ShowMessage("Please enter a valid guardian 1 status", MessageType.Error)
                    ElseIf result = 25 Then
                        ShowMessage("Please enter a valid guardian 1 city", MessageType.Error)
                    ElseIf result = 26 Then
                        ShowMessage("Please enter a valid guardian 1 state", MessageType.Error)
                    ElseIf result = 27 Then
                        ShowMessage("Please enter a valid guardian 1 postcode", MessageType.Error)
                    ElseIf result = 28 Then
                        ShowMessage("Please enter a valid guardian 1 work", MessageType.Error)
                    ElseIf result = 29 Then
                        ShowMessage("Please enter a valid guardian 1 office number", MessageType.Error)
                    ElseIf result = 40 Then
                        ShowMessage("Please enter a valid guardian 2 name", MessageType.Error)
                    ElseIf result = 41 Then
                        ShowMessage("Please enter a valid guardian 2 identification card", MessageType.Error)
                    ElseIf result = 42 Then
                        ShowMessage("Please enter a valid guardian 2 email", MessageType.Error)
                    ElseIf result = 43 Then
                        ShowMessage("Please enter a valid guardian 2 phone number", MessageType.Error)
                    ElseIf result = 44 Then
                        ShowMessage("Please enter a valid guardian 2 status", MessageType.Error)
                    ElseIf result = 45 Then
                        ShowMessage("Please enter a valid guardian 2 city", MessageType.Error)
                    ElseIf result = 46 Then
                        ShowMessage("Please enter a valid guardian 2 state", MessageType.Error)
                    ElseIf result = 47 Then
                        ShowMessage("Please enter a valid guardian 2 postcode", MessageType.Error)
                    ElseIf result = 48 Then
                        ShowMessage("Please enter a valid guardian 2 work", MessageType.Error)
                    ElseIf result = 49 Then
                        ShowMessage("Please enter a valid guardian 2 office number", MessageType.Error)
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