Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class takwim_list_uniform
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_tahun_list()
                ddlTahun.SelectedValue = oCommon.getAppsettings("DefaultKOKOYear")

                '--default
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub koko_tahun_list()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--ddlTahun
            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod ditemui."
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Kategori,Tarikh ASC"
        tmpSQL = "SELECT * FROM koko_takwim"
        strWhere = " WHERE koko_takwim.Tahun='" & ddlTahun.Text & "'"
        strWhere += " AND Kategori='" & lblKategori.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        lblTahun.Text = ddlTahun.Text
        Return getSQL

    End Function

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        '--default
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub lnkUniform_Click(sender As Object, e As EventArgs) Handles lnkUniform.Click
        Response.Redirect("admin.takwim.uniform.aspx?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

    Protected Sub lnkSukan_Click(sender As Object, e As EventArgs) Handles lnkSukan.Click
        Response.Redirect("admin.takwim.sukan.aspx?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

    Protected Sub lnkTempatSukan_Click(sender As Object, e As EventArgs) Handles lnkTempatSukan.Click
        Response.Redirect("admin.takwim.sukan.tempat.aspx?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

    Protected Sub lnkRenang_Click(sender As Object, e As EventArgs) Handles lnkRenang.Click
        Response.Redirect("admin.takwim.renang.aspx?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub
End Class