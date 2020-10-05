Imports System.Data.SqlClient

Public Class pengajar_pelajar_detail
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
                End If

                Check_student_exam()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Check_student_exam()

        Dim get_Check As String = "select Value from setting where Type  = 'Exam Result'"
        Dim data_Check As String = oCommon.getFieldValue(get_Check)

        If data_Check = "off" Then
            Hidden_Data.Value = "OFF"
        Else
            Hidden_Data.Value = "ON"
        End If

    End Sub

End Class