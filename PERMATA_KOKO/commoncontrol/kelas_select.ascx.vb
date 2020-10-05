Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class kelas_select
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
        Dim strOrder As String = " ORDER BY Kelas"
        tmpSQL = "SELECT *,(SELECT COUNT(*) FROM koko_pelajar WHERE koko_kelas.KelasID=koko_pelajar.KelasID AND koko_pelajar.Tahun='" & ddlTahun.Text & "') as JumlahPelajar FROM koko_kelas"
        strWhere = " WHERE koko_kelas.Tahun='" & ddlTahun.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblInstruktor As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString  'KelasID

            lblInstruktor = e.Row.FindControl("lblInstruktor")
            strSQL = "SELECT Fullname FROM koko_instruktor WHERE KelasID=" & strKeyID & " AND Tahun='" & ddlTahun.Text & "'"
            lblInstruktor.Text = oCommon.getRowValue(strSQL)
        End If

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
                Case "ADMIN"
                    Select Case Request.QueryString("set")
                        Case "pelajar"
                            Response.Redirect("admin.kelas.pelajar.assign.aspx?kelasid=" & strKeyID & "&tahun=" & ddlTahun.Text)
                        Case "senaraipelajar"
                            Response.Redirect("admin.kelas.pelajar.list.aspx?kelasid=" & strKeyID & "&tahun=" & ddlTahun.Text)
                        Case "instruktor"
                            Response.Redirect("admin.kelas.instruktor.assign.aspx?kelasid=" & strKeyID & "&tahun=" & ddlTahun.Text)
                        Case "pensyarah"
                            Response.Redirect("admin.kelas.pensyarah.assign.aspx?kelasid=" & strKeyID & "&tahun=" & ddlTahun.Text)
                        Case Else
                            Response.Redirect("admin.kelas.view.aspx?kelasid=" & strKeyID & "&tahun=" & ddlTahun.Text)
                    End Select
                Case "INSTRUKTOR"
                    Response.Redirect("instruktor.kelas.pelajar.list.aspx?kelasid=" & strKeyID & "&tahun=" & ddlTahun.Text)
                Case "PENSYARAH"
                Case "PENGARAH"
                Case Else
                    lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
            End Select

        Catch ex As Exception
            lblMsg.Text = "System Error: " & ex.Message
        End Try

    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

    End Sub

End Class