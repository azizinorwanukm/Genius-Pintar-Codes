Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization


Partial Public Class MsgInbox_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            txtMsgFrom.Text = Request.Cookies("ukmkpm_loginid").Value
            MsgInbox_MsgTo_List()
        End If

    End Sub

    Private Sub MsgInbox_MsgTo_List()
        strSQL = "SELECT DISTINCT LoginID,Fullname FROM UserProfile ORDER BY Fullname"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMsgTo.DataSource = ds
            ddlMsgTo.DataTextField = "Fullname"
            ddlMsgTo.DataValueField = "LoginID"
            ddlMsgTo.DataBind()

            ddlMsgTo.Items.Add(New ListItem("SEMUA PENGGUNA", "SEMUA PENGGUNA"))
            ddlMsgTo.SelectedValue = "SEMUA PENGGUNA"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''new code
        Dim strMsgCode As String = System.Guid.NewGuid.ToString

        strSQL = "INSERT INTO MsgInbox (MsgCode,MsgFrom,MsgTo,MsgSubject,MsgBody) VALUES ('" & strMsgCode & "','" & oCommon.FixSingleQuotes(txtMsgFrom.Text) & "','" & oCommon.FixSingleQuotes(ddlMsgTo.SelectedValue.ToString) & "','" & oCommon.FixSingleQuotes(txtMsgSubject.Text) & "','" & oCommon.FixSingleQuotes(txtMsgBody.Text) & "')"
        ''debug
        'Response.Write(strSQL)
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Mesej berjaya dihantar!"
            divMsg.Attributes("class") = "info"
        Else
            lblMsg.Text = "Mesej GAGAL dihantar!"
            divMsg.Attributes("class") = "error"
        End If

    End Sub


    Private Function ValidatePage() As Boolean
        If txtMsgSubject.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan subjek!"
            divMsg.Attributes("class") = "error"
            txtMsgSubject.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        txtMsgBody.Text = ""
        txtMsgSubject.Text = ""

    End Sub
End Class