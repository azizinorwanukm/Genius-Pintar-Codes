Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class _default1
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cookies("islogin").Value = "N"
            Response.Cookies("islogin").Expires = DateTime.Now.AddDays(1)

            If isClosed() = True Then
                Response.Redirect("esurvey.closed.aspx", False)
            End If

            ''Dim ci As CultureInfo
            ''Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("culture"))
            ' ''get the culture info to set the language
            ''rm = New ResourceManager("Resources.stresstest-2014", System.Reflection.Assembly.Load("App_GlobalResources"))
            ''ci = Thread.CurrentThread.CurrentCulture
            ''LoadStrings(ci)

        Catch ex As Exception
            ''lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function isClosed() As Boolean
        Try
            Dim strEndDate As String = oCommon.getAppsettings("EQTest_EndDate")
            strRet = oCommon.getToday()
            If CInt(strRet) > CInt(strEndDate) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function


    Private Sub LoadStrings(ByVal ci As CultureInfo)
        ''lbldefault_01.Text = rm.GetString("lbldefault_01", ci)

    End Sub

End Class