Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class _default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Response.Write("Err:" & ex.Message)
        End Try


    End Sub

    
End Class