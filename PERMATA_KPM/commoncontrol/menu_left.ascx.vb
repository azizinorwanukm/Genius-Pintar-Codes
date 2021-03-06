Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Public Class menu_left
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Private rm As ResourceManager
    Dim ci As CultureInfo

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

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
        strSQL = "SELECT LoginID,Fullname,UserType FROM UserProfile WHERE loginid='" & Request.Cookies("ukmadmin_loginid").Value & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    lblFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    lblFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    lblLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    lblLoginID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UserType")) Then
                    lblUserType.Text = ds.Tables(0).Rows(0).Item("UserType")
                Else
                    lblUserType.Text = ""
                End If

            End If
        Catch ex As Exception
            '--
        Finally
            objConn.Dispose()
        End Try

    End Sub

End Class