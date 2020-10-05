﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class tempahan_search
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnLulus.Attributes.Add("onclick", "return confirm('Pasti ingin MELULUSKAN semua tempahan tersebut?');")

        Try
            If Not IsPostBack Then
                koko_tahun_list()
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

                '--default value
                selBulan.Value = DateTime.Now.ToString("MM")
                'Response.Write(DateTime.Now.ToString("MM"))

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
        Dim strOrder As String = " ORDER BY koko_tempahandetail.KemudahanID,koko_tempahandetail.BookingDate DESC"

        tmpSQL = "SELECT * FROM koko_tempahandetail"
        tmpSQL += " LEFT OUTER JOIN koko_kemudahan ON koko_tempahandetail.KemudahanID=koko_kemudahan.KemudahanID"
        strWhere = " WHERE BookingDate LIKE '%" & ddlTahun.Text & "%'"

        If Not selBulan.Value = "ALL" Then
            strWhere += " AND koko_tempahandetail.BookingDate LIKE '%" & ddlTahun.Text & "-" & selBulan.Value & "%'"
        End If

        If Not txtKemudahan.Text.Length = 0 Then
            strWhere += " AND koko_kemudahan.Kemudahan LIKE '%" & oCommon.FixSingleQuotes(txtKemudahan.Text) & "%'"
        End If

        If Not selStatusTempahan.Value = "ALL" Then
            strWhere += " AND koko_tempahandetail.StatusTempahan='" & selStatusTempahan.Value & "'"
        End If

        If Not txtPemohon.Text.Length = 0 Then
            strWhere += " AND koko_tempahandetail.Pemohon LIKE '%" & oCommon.FixSingleQuotes(txtPemohon.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
                Case "ADMIN"
                    Response.Redirect("admin.tempahan.view.aspx?tempahanid=" & strKeyID & "&admin_ID=" & Request.QueryString("admin_ID"))
                Case "INSTRUKTOR"
                Case "PENSYARAH"
                Case "PENGARAH"
                    Response.Redirect("pengarah.tempahan.view.aspx?tempahanid=" & strKeyID & "&pengarah_ID=" & Request.QueryString("pengarah_ID"))
                Case Else

            End Select
            lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."

        Catch ex As Exception
            lblMsg.Text = "System Error: " & ex.Message
        End Try

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=KOKO_File.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnLulus_Click(sender As Object, e As EventArgs) Handles btnLulus.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then
                    strSQL = "UPDATE koko_tempahandetail SET StatusTempahan='LULUS' WHERE TempahanID=" & strKey
                End If
                '--execute SQL
                strRet = oCommon.ExecuteSQL(strSQL)
                displayDebug(strSQL)
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini status tempahan."
        End If

    End Sub

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text += "Debug:" & strMsg
        End If

    End Sub

End Class