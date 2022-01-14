Imports System.Data.SqlClient

Public Class penjaga_login_berjaya
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

                Dim id As String = Request.QueryString("parent_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then
                    Check_student_exam()
                    Check_parent_view()

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Check_student_exam()

        Dim get_Check As String = ""

        If Session("Student_Campus") = "PGPN" Then
            get_Check = "select Value from setting where Type  = 'Exam Result PGPN'"
        ElseIf Session("Student_Campus") = "APP" Then
            get_Check = "select Value from setting where Type  = 'Exam Result APP'"
        End If

        Dim data_Check As String = oCommon.getFieldValue(get_Check)

        If data_Check = "off" Then
            Hidden_Data.Value = "OFF"
        Else
            Hidden_Data.Value = "ON"
        End If

    End Sub

    Private Sub Check_parent_view()

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("parent_ID"))

        Dim get_Check As String = "select Parent_No from parent_Info where parent_ID = '" & data_ID & "'"
        Dim data_Check As String = oCommon.getFieldValue(get_Check)

        If data_Check = "1" Then
            Hidden_Parent.Value = "P1"
        Else
            Hidden_Parent.Value = "P2"
        End If

    End Sub
End Class