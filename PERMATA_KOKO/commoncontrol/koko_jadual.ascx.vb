Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_jadual
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
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

                '--default
                strRet = BindData(datUniform, "koko_uniform", "Uniform")
                strRet = BindData(datPersatuan, "koko_persatuan", "Persatuan")
                strRet = BindData(datSukan, "koko_sukan", "Sukan")
                strRet = BindData(datRumahsukan, "koko_rumahsukan", "RumahSukan")

                lblMsg.Text = "Masa cetakan:" & oCommon.formatDateDay(Now)
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

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

            'ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        '--default
        strRet = BindData(datUniform, "koko_uniform", "Uniform")
        strRet = BindData(datPersatuan, "koko_persatuan", "Persatuan")
        strRet = BindData(datSukan, "koko_sukan", "Sukan")
        strRet = BindData(datRumahsukan, "koko_rumahsukan", "RumahSukan")

        lblMsg.Text = "Masa cetakan:" & oCommon.formatDateDay(Now)
    End Sub

    Private Function BindData(ByVal gvTable As GridView, ByVal strTablename As String, ByVal strFieldname As String) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL(strTablename, strFieldname), strConn)
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

    Private Function getSQL(ByVal strTablename As String, ByVal strFieldname As String) As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY " & strFieldname

        tmpSQL = "SELECT * FROM " & strTablename

        If Not ddlTahun.Text = "ALL" Then
            strWhere = " WHERE Tahun='" & ddlTahun.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

End Class