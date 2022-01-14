﻿Imports System.Data.SqlClient
Imports System.IO

Public Class penjaga_update_profile
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

                Dim id As String = oCommon.securityLogin(Request.QueryString("parent_ID"))

                If id = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf id = "TRUE" Then

                    Check_parent_view()

                    result = Request.QueryString("result")

                    '' student detail error
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
                    Else

                    End If

                    '' guardian detail error
                    If result = 20 Then
                        ShowMessage("Update guardian details", MessageType.Success)
                    ElseIf result = 21 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 22 Then
                        ShowMessage("Please enter a valid guardian name", MessageType.Error)
                    ElseIf result = 23 Then
                        ShowMessage("Please enter a valid guardian identification card", MessageType.Error)
                    ElseIf result = 24 Then
                        ShowMessage("Please enter a valid guardian gender", MessageType.Error)
                    ElseIf result = 25 Then
                        ShowMessage("Please enter a valid guardian job", MessageType.Error)
                    ElseIf result = 26 Then
                        ShowMessage("Please enter a valid guardian home email address", MessageType.Error)
                    ElseIf result = 27 Then
                        ShowMessage("Please enter a valid guardian home mobile number", MessageType.Error)
                    ElseIf result = 28 Then
                        ShowMessage("Please enter a valid guardian home city", MessageType.Error)
                    ElseIf result = 29 Then
                        ShowMessage("Please enter a valid guardian home state", MessageType.Error)
                    ElseIf result = 30 Then
                        ShowMessage("Please enter a valid guardian home postcode", MessageType.Error)
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

    Private Sub Check_parent_view()

        Dim data_ID = oCommon.Student_securityLogin(Request.QueryString("parent_ID"))

        Dim get_Check As String = "select Parent_No from parent_Info where parent_ID = '" & data_ID & "'"
        Dim data_Check As String = oCommon.getFieldValue(get_Check)

        If data_Check = "1" Then
            Hidden_Parent.Value = "P1"
        Else
            Hidden_Parent.Value = "P2"
        End If

    End Sub
End Class