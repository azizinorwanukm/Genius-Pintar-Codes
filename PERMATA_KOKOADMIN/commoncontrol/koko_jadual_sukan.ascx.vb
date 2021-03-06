Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_jadual_sukan
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
                strRet = BindData(datRumahsukan, "SUKAN")
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message
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

    Private Function BindData(ByVal gvTable As GridView, ByVal strJenis As String) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL(strJenis), strConn)
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

    Private Function getSQL(ByVal strJenis As String) As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Nama"

        tmpSQL = "SELECT * FROM koko_kolejpermata"
        strWhere = " WHERE Jenis='" & strJenis & "' AND Tahun='" & ddlTahun.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        lblTahun.Text = ddlTahun.Text
        Return getSQL

    End Function

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged

        '--default
        strRet = BindData(datRumahsukan, "SUKAN")

    End Sub

    Protected Sub lnkPersatuan_Click(sender As Object, e As EventArgs) Handles lnkPersatuan.Click
        Response.Redirect("admin.koko.jadual.persatuan.aspx")

    End Sub

    Protected Sub lnkSukan_Click(sender As Object, e As EventArgs) Handles lnkSukan.Click
        Response.Redirect("admin.koko.jadual.sukan.aspx")

    End Sub

    Protected Sub lnkUniform_Click(sender As Object, e As EventArgs) Handles lnkUniform.Click
        Response.Redirect("admin.koko.jadual.uniform.aspx")

    End Sub

End Class