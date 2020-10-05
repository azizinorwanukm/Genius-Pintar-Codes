Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class default_org
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
        Dim strUKM1END As String = oCommon.getAppsettings("UKM1END")
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        If CInt(strToday) > CInt(strUKM1END) Then
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

    Private Sub btnNextPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNextPage.Click
        Response.Redirect("default.01.aspx")

    End Sub

End Class