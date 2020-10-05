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
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

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
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
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

            ''ALL users
            ddlMsgTo.Items.Add(New ListItem("ALL", "ALL"))
            ddlMsgTo.SelectedValue = "ALL"
            ddlMsgTo.Enabled = True
            ''debug

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
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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

        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY MsgDate DESC"

        tmpSQL = "SELECT * FROM MsgInbox"
        strWhere = " WITH (NOLOCK) WHERE isDeleted='N'"

        ''MsgFrom
        If Not ddlMsgFrom.Text = "ALL" Then
            strWhere += " AND MsgFrom='" & ddlMsgFrom.Text & "'"
        End If

        ''MsgTo
        If Not ddlMsgTo.Text = "ALL" Then
            strWhere += " AND MsgTo='" & ddlMsgTo.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles datRespondent.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = datRespondent.SelectedRow

        ' Display the company name from the selected row.
        ' In this example, the third column (index 2) contains
        ' the company name.
        lblMsg.Text = "You selected " & row.Cells(2).Text & "."
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.msginbox.view.aspx?msgcode=" & strKeyID)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

    Private Sub btnCompose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompose.Click
        Response.Redirect("admin.msginbox.create.aspx")

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(3).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)
                    strSQL = "DELETE MsgInbox WHERE MsgCode='" & strID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Rekod BERJAYA dipadam!"
            lblMsgTop.Text = lblMsg.Text
        End If

        ''--refresh screen
        strRet = BindData(datRespondent)

    End Sub

End Class