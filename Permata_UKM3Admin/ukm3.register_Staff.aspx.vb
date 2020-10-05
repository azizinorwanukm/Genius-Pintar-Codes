Imports System.Data.SqlClient
Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                result = Request.QueryString("result")

                If result = 1 Then
                    ShowMessage("Data Pengajar Berjaya Dimasukkan", MessageType.Success)

                ElseIf result = 2 Then
                    ShowMessage("Error", MessageType.Error)

                ElseIf result = 3 Then
                    ShowMessage("Sila semak semula nombor telefon yang dimasukkan", MessageType.Error)

                ElseIf result = 4 Then
                    ShowMessage("kata laluan yang dimasukkan tidak sama seperti di atas", MessageType.Error)

                ElseIf result = 5 Then
                    ShowMessage("sila semak kata laluan yang dimasukkan", MessageType.Error)

                ElseIf result = 6 Then
                    ShowMessage("sila pilih jawatan bagi pengajar", MessageType.Error)

                ElseIf result = 7 Then
                    ShowMessage("Sila semak semula pengisian bandar", MessageType.Error)

                ElseIf result = 8 Then
                    ShowMessage("Sila semak semula pengisian negeri", MessageType.Error)

                ElseIf result = 9 Then
                    ShowMessage("sila semak semula pengisian alamat", MessageType.Error)

                ElseIf result = 10 Then
                    ShowMessage("sila semak semula pengisian email", MessageType.Error)

                ElseIf result = 11 Then
                    ShowMessage("sila pilih jantina", MessageType.Error)

                ElseIf result = 12 Then
                    ShowMessage("sila semak semula Id pengajar", MessageType.Error)

                ElseIf result = 13 Then
                    ShowMessage("sila semak semula ic pengajar", MessageType.Error)


                ElseIf result = 14 Then
                    ShowMessage("sila semak semula nama pengajar", MessageType.Error)
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