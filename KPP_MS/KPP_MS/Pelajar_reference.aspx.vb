Imports System
Imports System.Net

Public Class Pelajar_reference
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Redirect(Request.QueryString("path"))

        Response.ContentType = "application/pdf"
        Response.WriteFile(Request.QueryString("path"))

    End Sub

End Class