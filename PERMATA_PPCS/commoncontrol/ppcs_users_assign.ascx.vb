Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ppcs_users_assign
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnRemove.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)

                ppcsdate_list()
                ddlPPCSDateAssign.Text = oCommon.getAppsettings("DefaultPPCSDate")

                usertype_list()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try
    End Sub

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        strSQL = oCommon.PPCSDate_Query(Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value))

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--destination
            ddlPPCSDateAssign.DataSource = ds
            ddlPPCSDateAssign.DataTextField = "PPCSDate"
            ddlPPCSDateAssign.DataValueField = "PPCSDate"
            ddlPPCSDateAssign.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub usertype_list()
        strSQL = "SELECT usertype FROM master_PPCS_UserType WHERE UserType<>'ADMIN' ORDER BY UserType"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUserTypeAssign.DataSource = ds
            ddlUserTypeAssign.DataTextField = "usertype"
            ddlUserTypeAssign.DataValueField = "usertype"
            ddlUserTypeAssign.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Record not found!"
            Else
                lblMsg.Text = "Total users #:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY Fullname ASC"

        tmpSQL = "SELECT myGUID,Fullname,LoginID,Pwd FROM PPCS_Users"
        strWhere = " WITH (NOLOCK) WHERE LoginID<>'permatapintar'"

        If Not txtFullname.Text.Length = 0 Then
            strWhere += " AND Fullname LIKE '%" & oCommon.FixSingleQuotes(txtFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPPCSDate As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString  'myGUID

            lblPPCSDate = e.Row.FindControl("lblPPCSDate")
            lblPPCSDate.Text = getPPCSDate(strKeyID)
        End If

    End Sub

    Private Function getPPCSDate(ByVal strKeyID As String) As String
        Dim strValue As String = ""
        strSQL = "SELECT PPCSDate FROM PPCS_Users_Year WHERE myGUID='" & strKeyID & "'"
        strValue = oCommon.getRowValue(strSQL)

        Return strValue
    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
            Case "ADMIN"
                Response.Redirect("ppcs.users.update.aspx?myguid=" & strKeyID)
            Case "SUBADMIN"
            Case Else
                lblMsg.Text = "Invalid user type! "
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & Request.Cookies("ppcs_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString

                    '--create new record if not exist
                    PPCS_Users_Year_Copy(strID)
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            strRet = BindData(datRespondent)
            lblMsg.Text = "Berjaya assign PPCS Users. Hanya satu User Type pada satu Sessi PPCS."
        End If

    End Sub

    Private Sub PPCS_Users_Year_Copy(ByVal strmyGUID As String)
        '--dah ada jangan insert lagi
        strSQL = "SELECT myGUID FROM PPCS_Users_Year WHERE myGUID='" & strmyGUID & "' AND PPCSDate='" & ddlPPCSDateAssign.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            Exit Sub
        End If

        '--insert for new record
        strSQL = "INSERT INTO PPCS_Users_Year(myGUID,Usertype,PPCSDate) VALUES('" & strmyGUID & "','" & ddlUserTypeAssign.Text & "','" & ddlPPCSDateAssign.Text & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "NOK:" & strRet
        End If

    End Sub


    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString

                    '--DELETE 
                    strSQL = "DELETE FROM PPCS_Users_Year WHERE myGUID='" & strID & "' AND PPCSDate='" & ddlPPCSDateAssign.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID
                    End If
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            strRet = BindData(datRespondent)
            lblMsg.Text = "Berjaya Remove PPCS Users."
        End If

    End Sub

End Class