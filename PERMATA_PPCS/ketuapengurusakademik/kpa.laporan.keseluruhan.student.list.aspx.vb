Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData

Partial Public Class kpa_laporan_keseluruhan_student_list
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")
    Dim strClassID As String
    Dim strTestID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                ''do nothing
            End If

        Catch ex As Exception
            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkTest As CheckBox = CType(sender, CheckBox)
        Dim grdRow As GridViewRow = CType(chkTest.NamingContainer, GridViewRow)

        If (chkTest.Checked) Then
        Else
        End If

    End Sub


End Class