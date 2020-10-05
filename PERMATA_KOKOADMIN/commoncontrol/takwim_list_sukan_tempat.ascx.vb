Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class takwim_list_sukan_tempat
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
                strRet = BindDataSQL(datSukan, "SUKAN")
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

    Private Function BindDataSQL(ByVal gvTable As GridView, ByVal strJenis As String) As Boolean
        Dim myDataSet As New DataSet

        Dim tmpSQL As String = "SELECT * FROM koko_kolejpermata"
        Dim strWhere As String = " WHERE Jenis='SUKAN' AND Tahun='" & ddlTahun.Text & "'"
        Dim strOrderby As String = " ORDER BY Nama"

        Dim strQuery As String = tmpSQL & strWhere & strOrderby
        lblTahun.Text = ddlTahun.Text
        '--debug
        'Response.Write(strQuery)

        Dim myDataAdapter As New SqlDataAdapter(strQuery, strConn)
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

    Private Sub datSukan_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datSukan.PageIndexChanging
        datSukan.PageIndex = e.NewPageIndex
        strRet = BindDataSQL(datSukan, "SUKAN")

    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        '--default
        strRet = BindDataSQL(datSukan, "SUKAN")

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