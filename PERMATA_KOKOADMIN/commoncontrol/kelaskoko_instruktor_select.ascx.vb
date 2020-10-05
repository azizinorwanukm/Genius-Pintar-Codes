Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class kelaskoko_instruktor_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                getKOKODetail()

                '--default list
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    '--getKOKODetail
    Private Sub getKOKODetail()
        strSQL = "SELECT KokoID FROM koko_kelaskoko WHERE kelaskokoid=" & Request.QueryString("kelaskokoid")
        lblKokoID.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Nama FROM koko_kolejpermata WHERE KokoID=" & lblKokoID.Text
        lblKOKOName.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Tahun FROM koko_kolejpermata WHERE KokoID=" & lblKokoID.Text
        lblTahun.Text = oCommon.getFieldValue(strSQL)
    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
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
        Dim strOrder As String = " ORDER BY koko_instruktor.Fullname"

        '--PROFILE INSTRUKTOR
        tmpSQL = "SELECT koko_instruktor.kokoinstruktorid,koko_instruktor.InstruktorID,koko_instruktor.UniformID,koko_instruktor.PersatuanID,koko_instruktor.SukanID,koko_instruktor.RumahSukanID,koko_instruktor.Fullname,koko_instruktor.MYKAD,koko_instruktor.ContactNo,koko_instruktor.Email,koko_instruktor.Tahun,koko_instruktor.BankName,koko_instruktor.AcctNo,koko_kelas.Kelas,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.UniformID=koko_kolejpermata.KokoID) as Uniform,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.SukanID=koko_kolejpermata.KokoID) as Sukan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan"
        tmpSQL += " FROM koko_instruktor"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_instruktor.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_instruktor.Tahun='" & lblTahun.Text & "'"

        If Not lblTahun.Text = "ALL" Then
            strWhere += " AND koko_instruktor.Tahun='" & lblTahun.Text & "'"
        End If

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND koko_instruktor.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If
        If Not txtFullname.Text.Length = 0 Then
            strWhere += " AND koko_instruktor.Fullname LIKE '%" & oCommon.FixSingleQuotes(txtFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
                Case "ADMIN"
                    Select Case Request.QueryString("set")
                        Case "koko"
                            Response.Redirect("admin.koko.instruktor.assign.aspx?instruktorid=" & strKeyID & "&tahun=" & lblTahun.Text & "&admin_ID=" & Request.QueryString("admin_ID"))
                        Case "kehadiran"
                            Response.Redirect("admin.event.list.aspx?instruktorid=" & strKeyID & "&tahun=" & lblTahun.Text & "&admin_ID=" & Request.QueryString("admin_ID"))
                        Case "markah"
                            Response.Redirect("admin.instruktor.mark.aspx?set=markah&instruktorid=" & strKeyID & "&tahun=" & lblTahun.Text & "&admin_ID=" & Request.QueryString("admin_ID"))
                        Case Else
                            Response.Redirect("admin.instruktor.view.aspx?instruktorid=" & strKeyID & "&tahun=" & lblTahun.Text & "&admin_ID=" & Request.QueryString("admin_ID"))
                    End Select
                Case "INSTRUKTOR"

                Case "PENSYARAH"
                Case "PENGARAH"
                Case Else
                    lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
            End Select

        Catch ex As Exception
            lblMsg.Text = "System Error: " & ex.Message
        End Try

    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.kelaskoko.list.aspx?set=instruktor?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

    Protected Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    strSQL = "UPDATE koko_kelaskoko SET kokoinstruktorid=" & strKey & " WHERE KelasKokoID=" & Request.QueryString("kelaskokoid")
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini rekod."
        End If

        lblMsgTop.Text = lblMsg.Text
    End Sub
End Class