Imports System.Data.SqlClient

Public Class admin_edit_disiplin
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim result As Integer
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

                    result = Int32.Parse(Request.QueryString("result"))

                    If IsNumeric(result) Then
                        If result = 0 Then
                            ShowMessage("Save Data Successfully", MessageType.Success)
                        ElseIf result = 4 Then
                            ShowMessage("Please Select Case Type", MessageType.Error)
                        ElseIf result = 5 Then
                            ShowMessage("Please Check If The Student ID/MyKad Is Correct", MessageType.Error)
                        ElseIf result = 6 Then
                            ShowMessage("Please select the staff that report the case.", MessageType.Error)
                        ElseIf result = 7 Then
                            ShowMessage("Please check the date if inserted correctly.", MessageType.Error)
                        ElseIf result = 8 Then
                            ShowMessage("Please fill in the action section.", MessageType.Error)
                        ElseIf result = 9 Then
                            ShowMessage("Please fill in the detail section", MessageType.Error)
                        ElseIf result = 11 Then
                            ShowMessage("Please cheack the date for the counseling session is correct.", MessageType.Error)
                        ElseIf result = 12 Then
                            ShowMessage("Please select the counselor in charge of the case.", MessageType.Error)
                        ElseIf result = 10 Then
                            ShowMessage("Failed to save diciplin data", MessageType.Error)
                        ElseIf result = 13 Then
                            ShowMessage("Failed to save counseling data", MessageType.Error)
                        ElseIf result = 14 Then
                            ShowMessage("Please fill in start time and end time section", MessageType.Error)
                        ElseIf result = 15 Then
                            ShowMessage("Please fill in code session section", MessageType.Error)
                        ElseIf result = 16 Then
                            ShowMessage("Please fill in client classification section", MessageType.Error)
                        ElseIf result = 17 Then
                            ShowMessage("Please fil in type of interview section", MessageType.Error)
                        Else
                            ShowMessage("Unknown Error", MessageType.Error)
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