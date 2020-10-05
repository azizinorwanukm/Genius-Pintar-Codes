Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_list_rumahsukan
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
                strRet = BindDataSQL(datRumahsukan)
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

            ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Function BindDataSQL(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet

        Dim tmpSQL As String = "SELECT *,(SELECT COUNT(*) FROM koko_pelajar WHERE koko_pelajar.Tahun='" & ddlTahun.Text & "' AND koko_pelajar.RumahsukanID=koko_kolejpermata.KokoID) as JumlahPelajar FROM koko_kolejpermata"
        Dim strWhere As String = " WHERE Jenis='RUMAHSUKAN' AND Tahun='" & ddlTahun.Text & "'"
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

    Private Sub datRumahsukan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRumahsukan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRumahsukan.DataKeys(e.Row.RowIndex).Value.ToString  'KelasID

            '--instruktor list
            Dim lblInstruktor As Label
            lblInstruktor = e.Row.FindControl("lblInstruktor")
            strSQL = "SELECT Fullname FROM koko_instruktor WHERE RumahsukanID=" & strKeyID & " AND Tahun='" & ddlTahun.Text & "' ORDER BY Fullname"
            lblInstruktor.Text = oCommon.getRowValue(strSQL)

            '-ketua instruktor
            Dim lblKetuaInstruktor As Label
            lblKetuaInstruktor = e.Row.FindControl("lblKetuaInstruktor")
            strSQL = "SELECT Fullname FROM koko_instruktor WHERE RumahsukanID=" & strKeyID & " AND KetuaRumahsukan='True' AND Tahun='" & ddlTahun.Text & "' ORDER BY Fullname"
            lblKetuaInstruktor.Text = oCommon.getFieldValue(strSQL)

            '-lblKetuaInstruktorTelefon
            Dim lblKetuaInstruktorTelefon As Label
            lblKetuaInstruktorTelefon = e.Row.FindControl("lblKetuaInstruktorTelefon")
            strSQL = "SELECT ContactNo FROM koko_instruktor WHERE RumahsukanID=" & strKeyID & " AND KetuaRumahsukan='True' AND Tahun='" & ddlTahun.Text & "' ORDER BY Fullname"
            lblKetuaInstruktorTelefon.Text = oCommon.getFieldValue(strSQL)

        End If

    End Sub

    Private Sub datSukan_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRumahsukan.SelectedIndexChanging
        Dim strKeyID As String = datRumahsukan.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
                Case "ADMIN"
                    Response.Redirect("admin.koko.pelajar.list.aspx?kokoid=" & strKeyID & "&std_ID=" & Request.QueryString("std_ID"))
                Case "INSTRUKTOR"
                    Response.Redirect("instruktor.koko.pelajar.list.aspx?kokoid=" & strKeyID & "&std_ID=" & Request.QueryString("std_ID"))
                Case "PENSYARAH"
                Case "PENGARAH"
                Case Else
                    lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
            End Select

        Catch ex As Exception
            lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub lnkSukan_Click(sender As Object, e As EventArgs) Handles lnkSukan.Click
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.koko.list.sukan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.koko.list.sukan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case "PELAJAR"
                Response.Redirect("pelajar.koko.list.sukan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Protected Sub lnkUniform_Click(sender As Object, e As EventArgs) Handles lnkUniform.Click
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.koko.list.uniform.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.koko.list.uniform.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case "PELAJAR"
                Response.Redirect("pelajar.koko.list.uniform.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Protected Sub lnkRumahsukan_Click(sender As Object, e As EventArgs) Handles lnkRumahsukan.Click
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.koko.list.rumahsukan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.koko.list.rumahsukan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case "PELAJAR"
                Response.Redirect("pelajar.koko.list.rumahsukan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Protected Sub lnkPersatuan_Click(sender As Object, e As EventArgs) Handles lnkPersatuan.Click
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.koko.list.persatuan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.koko.list.persatuan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case "PELAJAR"
                Response.Redirect("pelajar.koko.list.persatuan.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select
    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        '--default
        strRet = BindDataSQL(datRumahsukan)

    End Sub
End Class