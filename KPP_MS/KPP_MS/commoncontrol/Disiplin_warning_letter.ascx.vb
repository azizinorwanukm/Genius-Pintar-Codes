Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Web.UI.Control

Public Class Disiplin_warning_letter
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim dateFormat As String = "dd MMM yyyy"

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Request.QueryString("v").Equals("0") Then
                LoadWarningLetterList()
            ElseIf Request.QueryString("v").Equals("1") And Request.QueryString("ltrID").Equals("") = False Then
                LoadWarningLetterContent(Request.QueryString("ltrID"))
            End If
        End If
        WarningLetterMultiView.ActiveViewIndex = Request.QueryString("v")
    End Sub

    Protected Sub LoadWarningLetterList()
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub LoadWarningLetterContent(strKeyId As String)
        If strKeyId.Length = 0 Then
            Response.Redirect("admin_config_warning_letter.aspx?v=0&admin_ID=" + Request.QueryString("admin_ID"))
        Else
            Dim getDetail As String = "SELECT * FROM warning_letters_table WHERE id='" + strKeyId + "'"

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(getDetail, objConn)

            Try
                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "AnyTable")

                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("title")) Then
                        txtLetterTitle.Text = ds.Tables(0).Rows(0).Item("title")
                    Else
                        txtLetterTitle.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("letter_content")) Then
                        txtLetterContent.Content = Server.HtmlDecode(ds.Tables(0).Rows(0).Item("letter_content")).ToString
                    Else
                        txtLetterContent.Content = Server.HtmlDecode("<b>Write content here</b>")
                    End If

                End If
                objConn.Close()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean

        Dim mydataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(mydataSet, "myaccount")
            gvTable.DataSource = mydataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim mainSQLQuery As String
        Dim whereSQLQuery As String = ""
        Dim orderBySQLQuery As String = " ORDER BY year ASC"

        mainSQLQuery = "SELECT * FROM warning_letters_table"

        If txtSearchContent.Text.Length > 0 Then
            whereSQLQuery = " WHERE title like '%" & txtSearchContent.Text & "%'"
        End If

        getSQL = mainSQLQuery & whereSQLQuery & orderBySQLQuery
        Return getSQL
    End Function

    Protected Sub createNewLetter_Click(sender As Object, e As EventArgs) Handles createNewLetter.Click

        strSQL = "select MAX(id) from warning_letters_table"
        strRet = oCommon.getFieldValue(strSQL)

        Dim MAXint As String = strRet + 1

        Response.Redirect("admin_config_warning_letter.aspx?v=1&admin_ID=" + Request.QueryString("admin_ID") + "&ltrID=" + MAXint)
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim ltrID As String = Request.QueryString("ltrID")
        Dim saveQuery As String = ""

        strSQL = "select id from warning_letters_table where id = '" & ltrID & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length > 0 Then
            saveQuery = "UPDATE warning_letters_table SET title='" & txtLetterTitle.Text & "', letter_content='" & Server.HtmlEncode(txtLetterContent.Content) & "' WHERE id='" & Request.QueryString("ltrID") & "'"
            strRet = oCommon.ExecuteSQL(saveQuery)

            If strRet = 0 Then
                Response.Redirect("admin_config_warning_letter.aspx?v=0&admin_ID=" + Request.QueryString("admin_ID"))
            End If
        Else

            strSQL = "INSERT INTO warning_letters_table(title,letter_content,year) VALUES ('" & txtLetterTitle.Text & "','" & Server.HtmlEncode(txtLetterContent.Content) & "','" & Now.Year & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = 0 Then
                Response.Redirect("admin_config_warning_letter.aspx?v=0&admin_ID=" + Request.QueryString("admin_ID"))
            End If

        End If

    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_config_warning_letter.aspx?v=0&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Response.Redirect("admin_config_warning_letter.aspx?ltrID=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID") + "&v=1")
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strKey As String = datRespondent.DataKeys(e.RowIndex).Value.ToString
        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "DELETE warning_letters_table WHERE id = '" & strKey & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Search_ServerClick(sender As Object, e As EventArgs) Handles Search.ServerClick
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub
End Class