Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System
Imports System.Net

Public Class pelajar_view_reference
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Response.ContentType = "application/pdf"
            Response.WriteFile(Session("Path"))

        Catch ex As Exception
        End Try
    End Sub

End Class