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
        Try
            If Not IsPostBack Then
                UserProfile_load()
                If lblLoginID.Text.Length = 0 Then
                    Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again.")
                End If
            End If

        Catch ex As Exception
            Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again. err:" & ex.Message)

        End Try
    End Sub

    Private Sub UserProfile_load()
        strSQL = "SELECT Fullname FROM UserProfile WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        lblFullname.Text = oCommon.getFieldValue(strSQL)
        lblLoginID.Text = oCommon.FixSingleQuotes(Request.Cookies("ukmkpm_loginid").Value)

    End Sub

    

End Class