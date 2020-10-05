Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_main
    Inherits System.Web.UI.MasterPage

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '--tarikh tamat ujian pada tahun semasa 
            If isExamEnd() = True Then
                Response.Redirect("default.end.aspx")
            End If

            ''--sqlinjection
            Dim strStudentid As String = oCommon.FixSingleQuotes(Request.QueryString("studentid"))
            If oCommon.CheckSqlInjection(strStudentid) = True Then
                Response.Redirect("ukm1.invalid.url.aspx?lang=" & Request.QueryString("lang"), False)
            End If

            '--debug
            If checkStudentID(strStudentid) = False Then
                Response.Redirect("ukm1.invalid.url.aspx?lang=" & Request.QueryString("lang"), False)
            End If

            If ConfigurationManager.AppSettings("CheckTime") = "Y" Then
                ''time more than 12am and less than 7am
                If isAlllow() = False Then
                    Response.Redirect("ukm1.system.message.aspx?lang=" & Request.QueryString("lang"), False)
                End If
            End If

            ExamYear.Text = oCommon.getAppsettings("UKM1ExamYear")
            lblRespFullname.Text = getStudentFullname(strStudentid)

        Catch ex As Exception
            Response.Redirect("ukm1.session.end.aspx", False)
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

    Private Function getStudentFullname(ByVal strstudentid As String) As String
        strSQL = "SELECT MYKAD,StudentFullname FROM StudentProfile WHERE StudentID='" & strstudentid & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        Return strRet
    End Function

    Private Function checkStudentID(ByVal strstudentid As String) As Boolean

        strSQL = "SELECT StudentID FROM StudentProfile WHERE StudentID='" & strstudentid & "'"
        If oCommon.isExist(strSQL) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function isAlllow() As Boolean
        ''Dim nHour As Integer = DatePart("h", Now)
        Dim strHour As String = DateTime.Now.ToString("HH") 'return 24 hours format.
        ''-- 24 hours. before 7 oclock x boleh guna
        If CInt(strHour) < 7 Then
            Return False
        End If

        ''--exam END
        Dim strUKM1END As String = oCommon.getAppsettings("UKM1END")
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)
        If CInt(strToday) > CInt(strUKM1END) Then
            Return False
        End If

        Return True
    End Function

    Private Sub lnkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHome.Click
        Dim strHome As String = "default.main.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid")
        Response.Redirect(strHome)

    End Sub
End Class