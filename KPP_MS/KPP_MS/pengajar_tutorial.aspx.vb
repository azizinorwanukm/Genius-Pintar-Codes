Imports System.Data.SqlClient

Public Class pengajar_tutorial
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim MyConnection As SqlConnection = New SqlConnection(strConn)
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim data As String = oCommon.securityLogin(Request.QueryString("stf_ID"))

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

End Class