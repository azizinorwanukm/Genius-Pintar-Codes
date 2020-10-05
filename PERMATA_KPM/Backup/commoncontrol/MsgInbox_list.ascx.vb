Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization


Partial Public Class MsgInbox_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            MsgInbox_MsgFrom_List()
            MsgInbox_MsgTo_List()

            ''initial load
            strRet = BindData(datRespondent)
        End If

    End Sub

    Private Sub MsgInbox_MsgFrom_List()
        strSQL = "SELECT DISTINCT MsgFrom FROM MsgInbox ORDER BY MsgFrom"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMsgFrom.DataSource = ds
            ddlMsgFrom.DataTextField = "MsgFrom"
            ddlMsgFrom.DataValueField = "MsgFrom"
            ddlMsgFrom.DataBind()

            ddlMsgFrom.Items.Add(New ListItem("ALL", "ALL"))
            ddlMsgFrom.SelectedValue = "ALL"
            ''debug
            'Response.Write(getUserProfile_State())

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":MsgInbox_MsgFrom_List:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub MsgInbox_MsgTo_List()
        strSQL = "SELECT DISTINCT LoginID,Fullname FROM UserProfile ORDER BY LoginID"
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

            ''filter here
            ddlMsgTo.Items.Add(New ListItem("SEMUA PENGGUNA", "SEMUA PENGGUNA"))
            ddlMsgTo.Items.Add(New ListItem("ALL", "ALL"))

            ''only admin could view others
            If Not Request.Cookies("ukmkpm_loginid").Value = "kpmadmin" Then
                ddlMsgTo.SelectedValue = Request.Cookies("ukmkpm_loginid").Value
                ddlMsgTo.Enabled = False
            Else
                ddlMsgTo.SelectedValue = "ALL"
                ddlMsgTo.Enabled = True
            End If
            ''debug
            'Response.Write(getUserProfile_State())


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


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Mesej yang dicari tiada."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Senarai mesej berdasarkan pilihan anda. Jumlah mesej#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strSQL = getSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " Order By MsgDate DESC"

        tmpSQL = "SELECT * FROM MsgInbox"
        strWhere = " WITH (NOLOCK) WHERE isDeleted='N'"

        ''MsgFrom
        If Not ddlMsgFrom.Text = "ALL" Then
            strWhere += " AND MsgFrom='" & ddlMsgFrom.Text & "'"
        End If

        ''MsgTo
        If Not ddlMsgTo.Text = "ALL" Then
            strWhere += " AND (MsgTo='" & ddlMsgTo.Text & "' OR MsgTo='SEMUA PENGGUNA')"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Select Case getUserProfile_UserType()
                Case "KPM"
                    Response.Redirect("kpm.msginbox.view.aspx?msgcode=" & strKeyID)
                Case "JPN"
                    Response.Redirect("jpn.msginbox.view.aspx?msgcode=" & strKeyID)
                Case Else
                    Response.Redirect("system.error.aspx?usertype=" & getUserProfile_UserType())
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCompose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompose.Click
        Try
            Select Case getUserProfile_UserType()
                Case "KPM"
                    Response.Redirect("kpm.msginbox.create.aspx")
                Case "JPN"
                    Response.Redirect("jpn.msginbox.create.aspx")
                Case Else
                    lblMsg.Text = "You do not have the access right!"
            End Select

        Catch ex As Exception

        End Try

    End Sub
End Class