Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization


Partial Public Class MsgInbox_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                txtMsgFrom.Text = Request.Cookies("ukmkpm_loginid").Value
                MsgInbox_view()
            End If
        Catch ex As Exception
            Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again.")
        End Try

    End Sub

    Private Sub MsgInbox_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM MsgInbox WHERE MsgCode='" & Request.QueryString("msgcode") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                ''--parent info
                If Not IsDBNull(MyTable.Rows(nRows).Item("MsgFrom")) Then
                    txtMsgFrom.Text = MyTable.Rows(nRows).Item("MsgFrom").ToString
                Else
                    txtMsgFrom.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("MsgTo")) Then
                    txtMsgTo.Text = MyTable.Rows(nRows).Item("MsgTo").ToString
                Else
                    txtMsgTo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("MsgSubject")) Then
                    txtMsgSubject.Text = MyTable.Rows(nRows).Item("MsgSubject").ToString
                Else
                    txtMsgSubject.Text = ""
                End If

                ''mother info
                If Not IsDBNull(MyTable.Rows(nRows).Item("MsgBody")) Then
                    txtMsgBody.Text = MyTable.Rows(nRows).Item("MsgBody").ToString
                Else
                    txtMsgBody.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function UserProfile_fullname(ByVal strLoginID As String) As String
        If strLoginID = "SEMUA PENGGUNA" Then
            Return "SEMUA PENGGUNA"
        End If

        Dim tmpSQL As String = "SELECT Fullname FROM UserProfile WHERE loginid='" & strLoginID & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Private Sub btnReply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReply.Click

        Select Case getUserProfile_UserType()
            Case "KPM"
                Response.Redirect("kpm.msginbox.reply.aspx?msgcode=" & Request.QueryString("msgcode"))
            Case "JPN"
                Response.Redirect("jpn.msginbox.reply.aspx?msgcode=" & Request.QueryString("msgcode"))
            Case Else
                Response.Redirect("system.error.aspx?usertype=" & getUserProfile_UserType())
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

End Class