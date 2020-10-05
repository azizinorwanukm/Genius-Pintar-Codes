Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_confirm
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;")

    End Sub

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If ValidatePage() = False Then
                Exit Sub
            End If

            ''--keep inserting school. student could delete it and insert new if wrongly select.
            If StudentSchool_insert() = "0" Then
                Response.Redirect("parentprofile.search.aspx?studentid=" & Request.QueryString("studentid") & "&schoolid=" & Request.QueryString("schoolid"), False)
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = strRet
            End If

        Catch ex As Exception
            lblMsg.Text = "system error:" & ex.Message
        End Try
    End Sub

    Private Function ValidatePage() As Boolean

        If selStartDate_day.Value = "" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
            Return False
        End If
        If selStartDate_month.Value = "" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
            Return False
        End If
        If selStartDate_year.Value = "" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
            Return False
        End If

        Return True
    End Function

    Private Function StudentSchool_insert() As String
        Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & selStartDate_year.Value
        Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & selEndDate_year.Value

        strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,StartDate,EndDate,CreatedDate) VALUES ('" & Request.QueryString("studentid") & "','" & Request.QueryString("schoolid") & "','" & strStartDate & "','" & strEndDate & "','" & oCommon.getNow & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("StudentSchool_insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

        Return strRet
    End Function

End Class