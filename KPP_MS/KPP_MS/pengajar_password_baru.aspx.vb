Imports System.Data.SqlClient

Public Class pengajar_password_baru
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

                    '' student detail error
                    If result = 1 Then
                        ShowMessage(" Update Password", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Please enter a valid old password", MessageType.Error)
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

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick

        Response.Redirect("pengajar_login_berjaya.aspx?stf_ID=" + Request.QueryString("stf_ID"))
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        Dim userIDMYKAD As String = ""
        Dim userIDPSWD As String = ""

        If txtloginUsername.Text = "" Then
            ShowMessage("Please enter a valid username", MessageType.Error)
        Else
            If txtloginPassword.Text = "" Then
                ShowMessage("Please enter a valid old password", MessageType.Error)
            Else
                If txtnewPassword.Text = "" Then
                    ShowMessage("Please enter a valid new password", MessageType.Error)
                Else

                    Dim CheckMykad As String = ""
                    CheckMykad = "select login_ID from staff_Login where staff_Login = '" & txtloginUsername.Text & "' and staff_Password = '" & txtloginPassword.Text & "' and staff_Status = 'Access'"
                    userIDMYKAD = getFieldValue(CheckMykad, strConn)

                    If userIDMYKAD.Length = 0 Then
                        ShowMessage("Please enter a correct username & old password", MessageType.Error)
                    Else

                        strSQL = "UPDATE staff_Login SET staf_Password ='" & txtnewPassword.Text & "' WHERE login_ID ='" & userIDMYKAD & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet = "0" Then
                            Response.Redirect("pengajar_password_baru.aspx?stf_ID=" + Request.QueryString("stf_ID") + "&result=1")
                        Else
                            Response.Redirect("pengajar_password_baru.aspx?stf_ID=" + Request.QueryString("stf_ID") + "&result=-1")
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function
End Class