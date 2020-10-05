Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class ukm1_invalid_url
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

        Catch ex As Exception
            lblMsg.Text = "system error:" & ex.Message
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblPerhatian.Text = rm.GetString("lblPerhatian", ci)
        lblInvalidURL.Text = rm.GetString("lblInvalidURL", ci)

    End Sub


End Class