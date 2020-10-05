Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class user_comment
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                user_post_load()

                BindData(gvTable)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub user_post_load()
        strSQL = "SELECT * FROM user_post WHERE user_postid=" & Request.QueryString("postid")
        ''debug
        'Response.Write(strSQL)

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PostBy")) Then
                    lblPostBy.Text = ds.Tables(0).Rows(0).Item("PostBy")
                Else
                    lblPostBy.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PostDate")) Then
                    lblPostDate.Text = Convert.ToDateTime(ds.Tables(0).Rows(0)("PostDate").ToString()).ToString("dd-MM-yyyy hh:mm tt")
                    'Convert.ToDateTime(ds.Tables[0].Rows[0]["EventDate"].ToString()).ToString("MM/dd/yyyy");
                    'ds.Tables[0].Rows[0]["EventDate"].ToString("MM/dd/yyyy");
                    'txt_EventDate.Text = ds.Tables[0].Rows[0]["EventDate"].ToString("d");
                Else
                    lblPostDate.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Message")) Then
                    lblMessage.Text = ds.Tables(0).Rows(0).Item("Message")
                Else
                    lblMessage.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMessage.Text = "err:" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

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

    Private Function getSQL() As String
        Dim strTmpSQL As String = ""

        Dim strTemp As String = "SELECT * FROM user_comment"
        Dim strWhere As String = " WHERE user_postid=" & Request.QueryString("postid")
        Dim strOrderBy As String = " ORDER BY user_commentid ASC"

        strTmpSQL = strTemp & strWhere & strOrderBy

        Return strTmpSQL

    End Function


    Private Sub gvTable_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTable.PageIndexChanging
        gvTable.PageIndex = e.NewPageIndex
        BindData(gvTable)

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        strSQL = "INSERT INTO user_comment (user_postid,Message,PostBy) VALUES (" & oCommon.FixSingleQuotes(Request.QueryString("postid")) & ",'" & oCommon.FixSingleQuotes(txtMessage.Text) & "','" & CType(Session.Item("kpmadmin_loginid"), String) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Successfully post your comment."
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