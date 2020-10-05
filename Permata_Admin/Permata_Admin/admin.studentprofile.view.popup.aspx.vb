Imports System.Data.SqlClient

Public Class admin_studentprofile_view_popup
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

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
            Response.Write("Please login again..")
        End Try
    End Sub

    Private Sub UserProfile_load()
        Try
            strSQL = "SELECT LoginID,Fullname,UserType FROM UserProfile WHERE loginid='" & CType(Session.Item("permata_admin"), String) & "'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

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