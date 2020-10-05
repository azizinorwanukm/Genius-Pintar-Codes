Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Data.OleDb
'Imports System.IO
'Imports System.Globalization

Partial Public Class default_main
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"

        If oCommon.getFieldValue(strSQL) = "DONE" Then
            Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
        End If

        Try
            If isExamEnd() = True Then
                Response.Redirect("default.end.aspx")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function isExamEnd() As Boolean
        ''--exam END
        Dim strUKM1END As String = oCommon.getAppsettings("UKM1END")
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        If CInt(strToday) > CInt(strUKM1END) Then
            Return True
        End If

        Return False
    End Function

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        lblMsgTop.Text = ""

        If isUpdated() = False Then
            divMsg.Attributes("class") = "error"
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        ''test DONE
        '--request by Dr Siti. Check Email 19-05-2013
        strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        If oCommon.getFieldValue(strSQL) = "DONE" Then
            Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
        Else
            '--goto to next page
            Dim strNext As String = "ukm1.intro.page01.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid")
            Response.Redirect(strNext)
        End If

    End Sub

    Private Function isDONE() As Boolean
        Dim strNext As String = ""
        Dim strLast As String = ""

        ''test DONE
        strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
        If oCommon.getFieldValue(strSQL) = "DONE" Then
            Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
        End If

        ''--debug
        'Response.Write(strNext)
        Response.Redirect(strNext)
    End Function

    Private Function isUpdated() As Boolean
        strSQL = "SELECT IsUpdated FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        If Not strRet = "Y" Then
            lblMsg.Text = "Sila kemaskini Maklumat Pelajar!" & strRet
            Return False
        End If

        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Sila kemaskini Maklumat Sekolah !"
            Return False
        End If

        ''temporarily disable it here. enable it inside pelajar site
        strSQL = "SELECT IsUpdated FROM ParentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        If Not strRet = "Y" Then
            lblMsg.Text = "Sila kemaskini Maklumat Bapa/Penjaga!"
            Return False
        End If

        Return True

    End Function
End Class