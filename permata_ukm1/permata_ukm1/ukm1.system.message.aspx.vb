Imports System.Globalization
Imports System.Threading
Imports System.Resources


Partial Public Class ukm1_system_message
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strHour As String = DateTime.Now.ToString("HH") 'return 24 hours format.
        lblMsg.Text = "Current server date time: " & Now.ToString("yyyyMMdd HH:mm:ss.fff") & " Hours:" & strHour

        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

        Catch ex As Exception
            ''--lblMsg.Text = "system error:" & ex.Message
            Thread.CurrentThread.CurrentCulture = New CultureInfo("ms-MY")
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblPerhatian.Text = rm.GetString("lblPerhatian", ci)
        lblMsg01.Text = rm.GetString("lblMsg01", ci)

    End Sub

End Class