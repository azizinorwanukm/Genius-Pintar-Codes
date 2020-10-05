Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class pelajar_class_assign_01
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                
            End If

        Catch ex As Exception
        End Try
    End Sub

End Class