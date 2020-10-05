Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class security_login_trail_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                LoadPage()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadPage()
        strSQL = "SELECT * FROM security_login_trail WITH (NOLOCK) WHERE securityid='" & Request.QueryString("securityid") & "'"
        ''debug
        ''Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    lblLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    lblLoginID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LogTime")) Then
                    lblLogTime.Text = ds.Tables(0).Rows(0).Item("LogTime")
                Else
                    lblLogTime.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UserHostAddress")) Then
                    lblUserHostAddress.Text = ds.Tables(0).Rows(0).Item("UserHostAddress")
                Else
                    lblUserHostAddress.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UserHostName")) Then
                    lblUserHostName.Text = ds.Tables(0).Rows(0).Item("UserHostName")
                Else
                    lblUserHostName.Text = "--"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UserBrowser")) Then
                    lblUserBrowser.Text += "-" & ds.Tables(0).Rows(0).Item("UserBrowser")
                Else
                    lblUserBrowser.Text = "--"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Activity")) Then
                    lblActivity.Text += "-" & ds.Tables(0).Rows(0).Item("Activity")
                Else
                    lblActivity.Text = "--"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AuditDetail")) Then
                    lblAuditDetail.Text = ds.Tables(0).Rows(0).Item("AuditDetail")
                Else
                    lblAuditDetail.Text = ""
                End If

            End If

        Catch ex As Exception
            ''--display on screen
            'lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class