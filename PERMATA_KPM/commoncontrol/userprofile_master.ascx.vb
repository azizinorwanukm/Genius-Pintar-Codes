Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class userprofile_master
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            UserProfile_load()
        End If
    End Sub

    Private Sub UserProfile_load()
        Try
            ''Response.Cookies("kpmadmin_loginid").Value
            ''Response.Write("loginid:" & Server.HtmlEncode(CType(Session.Item("kpmadmin_loginid"), String)))

            strSQL = "SELECT Fullname FROM UserProfile WHERE loginid='" & Server.HtmlEncode(CType(Session.Item("kpmadmin_loginid"), String)) & "'"
            lblFullname.Text = oCommon.getFieldValue(strSQL)
            lblLoginID.Text = CType(Session.Item("kpmadmin_loginid"), String)
        Catch ex As Exception
            Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again.")

        End Try

    End Sub

    

End Class