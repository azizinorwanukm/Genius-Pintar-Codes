Imports System.Data.SqlClient
Imports System.IO

Public Class alumni_student_EditEduback1
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

                Dim admin_Id As String = Request.QueryString("admin_ID")
                Dim staff_Id As String = Request.QueryString("stf_ID")
                Dim id As String = ""

                If admin_Id = "" & staff_Id <> "" Then
                    id = Request.QueryString("stf_ID")
                ElseIf admin_Id <> "" & staff_Id = "" Then
                    id = Request.QueryString("admin_ID")
                Else

                End If

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

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