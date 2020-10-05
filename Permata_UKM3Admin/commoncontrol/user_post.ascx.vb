Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class user_post
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            BindData(gvTable)

        Catch ex As Exception

        End Try
    End Sub

    Private Function getSQL() As String
        Dim strTmpSQL As String = ""

        Dim strTemp As String = "SELECT * FROM user_post"
        Dim strWhere As String = ""
        Dim strOrderBy As String = " ORDER BY user_postid DESC"

        strTmpSQL = strTemp & strWhere & strOrderBy

        Return strTmpSQL

    End Function

    Private Sub BindData(ByVal gvTable As GridView)
        ''debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myTable")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub


    Private Sub gvTable_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTable.PageIndexChanging
        gvTable.PageIndex = e.NewPageIndex
        BindData(gvTable)

    End Sub

    Private Sub gvTable_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvTable.SelectedIndexChanging
        Dim strKeyID As String = gvTable.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.comment.list.aspx?postid=" & strKeyID)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.comment.list.aspx?postid=" & strKeyID)
                Case Else

            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        strSQL = "INSERT INTO user_post (Message,PostBy) VALUES ('" & oCommon.FixSingleQuotes(txtMessage.Text) & "','" & CType(Session.Item("permata_admin"), String) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Successfully post it."
            BindData(gvTable)
            txtMessage.Text = ""
            txtMessage.Focus()
        Else
            lblMsg.Text = "Error:" & strRet
        End If

    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
        BindData(gvTable)

    End Sub
End Class