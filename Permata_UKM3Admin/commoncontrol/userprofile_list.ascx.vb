Imports System.Data.SqlClient

Partial Public Class userprofile_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                setAccessRight()

                master_UserType_list()
                ddlUserType.Text = "ALL"

                '--display default
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try
    End Sub

    Private Sub setAccessRight()
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                btnCreate.Visible = True
            Case Else
                btnCreate.Visible = False
                lblMsg.Text = "Invalid usertype!"
        End Select

    End Sub
    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub master_UserType_list()
        strSQL = "SELECT UserType FROM master_UserType WHERE UserType<>'ADMIN'  ORDER BY UserType"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUserType.DataSource = ds
            ddlUserType.DataTextField = "UserType"
            ddlUserType.DataValueField = "UserType"
            ddlUserType.DataBind()

            ddlUserType.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

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
        Dim strOrder As String = " ORDER BY UserType,Fullname"

        tmpSQL = "SELECT * FROM UserProfile"
        strWhere += " WHERE UserType<>'ADMIN'"

        If Not txtFullname.Text.Length = 0 Then
            strWhere += " AND Fullname LIKE '%" & oCommon.FixSingleQuotes(txtFullname.Text) & "%'"
        End If

        If Not ddlUserType.Text = "ALL" Then
            strWhere += " AND UserType='" & ddlUserType.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        displayDebug(getSQL)

        Return getSQL

    End Function

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("admin.userprofile.update.aspx?userprofilecode=" & strKeyID)

    End Sub

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Response.Redirect("admin.userprofile.create.aspx")

    End Sub
End Class