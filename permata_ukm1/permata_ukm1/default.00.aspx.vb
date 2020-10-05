Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class default_01
    Inherits System.Web.UI.Page

   Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strTestID As String = Now.Year.ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If isExamEnd() = True Then
                Response.Redirect("default.end.aspx")
            End If

            If Not IsPostBack Then
                Lbl04.Text = Now.Year
                Lbl05.Text = Now.Year

                lblUKM1DisplayEnd.Text = oCommon.getAppsettings("UKM1DisplayEnd")

                '--display the counters
                Dim intTotalNumberOfUsers As Integer = Global_asax.TotalNumberOfUsers
                Dim intCurrentNumberOfUsers As Integer = Global_asax.CurrentNumberOfUsers

                lblCurrentNumberOfUsers.Text = intCurrentNumberOfUsers.ToString()
                lblTotalNumberOfUsers.Text = intTotalNumberOfUsers.ToString("N0", CultureInfo.InvariantCulture)
            End If

        Catch ex As Exception
            Response.Write("Err:" & ex.Message)

        End Try

    End Sub

    Private Function isExamEnd() As Boolean

        ''--exam END
        Dim strUKM1END As String = oCommon.getAppsettings("UKM1END")    'YYYYMMDD 20161130 - 20160914
        If strUKM1END.Length = 0 Then
            Return False
        End If

        Dim strYear As String = strUKM1END.Substring(0, 4)
        Dim strMonth As String = strUKM1END.Substring(4, 2)
        Dim strDay As String = strUKM1END.Substring(6, 2)
        If isDebug() = True Then
            lblDebug.Text = strYear & ":" & strMonth & ":" & strDay
        End If

        Dim dtUKM1END As New DateTime(strYear, strMonth, strDay)

        '--balance number of days
        'Dim nBalance As Integer = (dtUKM1END - DateTime.Now).TotalDays
        Dim nBalance As Integer = getDays(Date.Today, dtUKM1END)
        If nBalance = 0 Then
            lblBalance.Text = "Hari Terakhir!"
        Else
            lblBalance.Text = nBalance.ToString
        End If
        If nBalance < 0 Then
            Return True
        End If

        'Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)
        'If CInt(strToday) > CInt(strUKM1END) Then
        '    Return True
        'End If

        Return False
    End Function

    Private Function getDays(ByVal dtStart As Date, ByVal dtEnd As Date) As Integer
        'Dim offset = New Date(1, 1, 1)
        Dim diff As TimeSpan = dtEnd - dtStart

        'Dim years As Integer = (offset + diff).Year - 1
        'Dim months As Integer = (dtEnd.Month - dtStart.Month) + 12 * (dtEnd.Year - dtStart.Year)
        Dim days As Integer = diff.Days

        Return days
    End Function

    Private Function isDebug() As Boolean
        If oCommon.getAppsettings("isDebug") = "Y" Then
            Return True
        End If
        Return False
    End Function
    Private Function GetIPAddress() As String
        Dim context As System.Web.HttpContext = System.Web.HttpContext.Current()
        Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(sIPAddress) Then
            Return context.Request.ServerVariables("REMOTE_ADDR")
        Else
            Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
            Return ipArray(0)
        End If

    End Function

    'Private Sub btnNextPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNextPage.Click
    '    Response.Redirect("default.01.aspx")

    'End Sub

End Class