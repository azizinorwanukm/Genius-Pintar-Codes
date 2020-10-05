Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class instruktor_child_page
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--default
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod ditemui."
            Else
                divMsg.Attributes("class") = "info"
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
        tmpSQL = "SELECT KelasID, Kelas FROM koko_kelas"
        strWhere = " WHERE koko_kelas.Tahun='2015'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'assuming that the required value column is the second column in gridview
            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString  '--KelasID
            Dim strKelas As String = getKelas(strKeyID)

            Dim btnSelect As Button
            btnSelect = e.Row.FindControl("btnSelect")
            btnSelect.Attributes.Add("onclick", "javascript:GetRowValue('" & "ID:" & strKeyID & "|" & strKelas & "')")

        End If

    End Sub

    Private Function getKelas(ByVal strKelasID As String) As String
        strSQL = "SELECT Kelas FROM koko_kelas WHERE KelasID=" & strKelasID
        Return oCommon.getFieldValue(strSQL)

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
                Case "ADMIN"
                    Select Case Request.QueryString("set")
                        Case "pelajar"
                            Response.Redirect("admin.kelas.pelajar.assign.aspx?kelasid=" & strKeyID)
                        Case "instruktor"
                            Response.Redirect("admin.kelas.instruktor.assign.aspx?kelasid=" & strKeyID)
                        Case "pensyarah"
                            Response.Redirect("admin.kelas.pensyarah.assign.aspx?kelasid=" & strKeyID)
                        Case Else
                            Response.Redirect("admin.kelas.view.aspx?kelasid=" & strKeyID)
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

End Class