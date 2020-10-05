Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class master_config_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                ''strRet = BindData(datRespondent)
                config_list()
            End If
            ''initial load

        Catch ex As Exception

        End Try
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

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY type"

        tmpSQL = "SELECT data_id,description,type FROM master"

        If Not ddlconfig.SelectedValue = "" Then
            strWhere = " where type = '" & ddlconfig.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function
    Protected Sub ddlconfig_selectedindexchanged(sender As Object, e As EventArgs) Handles ddlconfig.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try



    End Sub
    Private Sub config_list()

        strSQL = "select distinct type from master "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "Anytable")

            ddlconfig.DataSource = ds
            ddlconfig.DataTextField = "type"
            ddlconfig.DataValueField = "type"
            ddlconfig.DataBind()
            ddlconfig.Items.Insert(0, New ListItem("pilih parameter", String.Empty))
            ddlconfig.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim type As String = "Update"

        Response.Redirect("admin.master.config.updated.aspx?configID=" & strKeyID & "&type=" & type)
        ''"&data_id=" & Request.QueryString("myguid")
    End Sub

    Private Sub lnkCreate_Click(sender As Object, e As EventArgs) Handles lnkCreate.Click
        Dim type As String = "Insert"

        Dim datatype = ddlconfig.SelectedValue
        Response.Redirect("admin.master.config.updated.aspx?type=" & type & "&datatype=" & datatype)

    End Sub
End Class