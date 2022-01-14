Imports System.Data.SqlClient

Public Class exam_List_Table
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY exam_Year DESC"

        tmpSQL = "Select * From exam_Info"
        strWhere += " WHERE exam_ID IS NOT NULL"

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyCode As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)

            ''delete exam info
            Dim Dlt_NewCourse As New SqlDataAdapter()
            Dim dlt_Course As String
            Dlt_NewCourse.SelectCommand = New SqlCommand()
            Dlt_NewCourse.SelectCommand.Connection = MyConnection
            Dlt_NewCourse.SelectCommand.CommandText = "delete exam_Info where exam_ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Course = Dlt_NewCourse.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            '' delete exam result related to that exam info id 
            Dim Dlt_NewData As New SqlDataAdapter()
            Dim dlt_Data As String
            Dlt_NewData.SelectCommand = New SqlCommand()
            Dlt_NewData.SelectCommand.Connection = MyConnection
            Dlt_NewData.SelectCommand.CommandText = "delete exam_result where exam_ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Data = Dlt_NewData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyCode As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_exam_data.aspx?exam_ID=" + strKeyCode + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub btnRegExam_ServerClick(sender As Object, e As EventArgs) Handles btnRegExam.ServerClick
        Try
            Response.Redirect("admin_peperiksaan_daftar_baru.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception

        End Try
    End Sub
End Class