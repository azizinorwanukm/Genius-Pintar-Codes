Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class jpn_ukm2_ishadir_confirm
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblExamYear.Text = ConfigurationManager.AppSettings("ExamYear")

    End Sub

    Private Sub btnHadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHadir.Click
        strSQL = "UPDATE UKM2 SET IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & lblExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    Private Sub btnTidakHadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakHadir.Click
        strSQL = "UPDATE UKM2 SET IsHadir='N' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & lblExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

End Class