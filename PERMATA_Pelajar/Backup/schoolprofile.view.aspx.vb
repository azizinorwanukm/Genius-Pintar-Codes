﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class schoolprofile_view1
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strSchoolID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''btnPrint.Attributes.Add("onClick", "javascript:window.print(); return false;")

    End Sub

    

End Class