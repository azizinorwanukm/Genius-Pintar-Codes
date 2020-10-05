Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_list_uniform
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnReset.Attributes.Add("onclick", "return confirm('Pasti ingin RESET koko pelajar tersebut?');")

        Try
            If Not IsPostBack Then
                koko_tahun_list()
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

                '--default
                strRet = BindDataSQL(datUniform)
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


    Private Function BindDataSQL(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet

        Dim tmpSQL As String = "SELECT *,(SELECT COUNT(*) FROM koko_pelajar WHERE koko_pelajar.Tahun='" & ddlTahun.Text & "' AND koko_pelajar.UniformID=koko_kolejpermata.KokoID) as JumlahPelajar FROM koko_kolejpermata"
        Dim strWhere As String = " WHERE Jenis='UNIFORM' AND Tahun='" & ddlTahun.Text & "'"
        Dim strOrderby As String = " ORDER BY Nama"

        Dim strQuery As String = tmpSQL & strWhere & strOrderby
        lblTahun.Text = ddlTahun.Text
        '--debug
        'Response.Write(strQuery)

        lblSQL.Text = strQuery
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

    Private Sub datUniform_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datUniform.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datUniform.DataKeys(e.Row.RowIndex).Value.ToString  'UniformID

            '--instruktor list
            Dim lblInstruktor As Label
            lblInstruktor = e.Row.FindControl("lblInstruktor")
            strSQL = "SELECT Fullname FROM koko_instruktor WHERE UniformID=" & strKeyID & " AND Tahun='" & ddlTahun.Text & "' ORDER BY Fullname"
            lblInstruktor.Text = oCommon.getRowValue(strSQL)

            '-ketua instruktor
            Dim lblKetuaInstruktor As Label
            lblKetuaInstruktor = e.Row.FindControl("lblKetuaInstruktor")
            strSQL = "SELECT Fullname FROM koko_instruktor WHERE UniformID=" & strKeyID & " AND KetuaUniform='True' AND Tahun='" & ddlTahun.Text & "' ORDER BY Fullname"
            lblKetuaInstruktor.Text = oCommon.getFieldValue(strSQL)

            '-lblKetuaInstruktorTelefon
            Dim lblKetuaInstruktorTelefon As Label
            lblKetuaInstruktorTelefon = e.Row.FindControl("lblKetuaInstruktorTelefon")
            strSQL = "SELECT ContactNo FROM koko_instruktor WHERE UniformID=" & strKeyID & " AND KetuaUniform='True' AND Tahun='" & ddlTahun.Text & "' ORDER BY Fullname"
            lblKetuaInstruktorTelefon.Text = oCommon.getFieldValue(strSQL)

        End If

    End Sub

    Private Sub datUniform_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datUniform.SelectedIndexChanging
        Dim strKeyID As String = datUniform.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
                Case "ADMIN"
                    Select Case Request.QueryString("set")
                        Case "laporan"
                            Response.Redirect("admin.laporan.koko.pelajar.list.aspx?kokoid=" & strKeyID & "&admin_ID=" & Request.QueryString("admin_ID"))
                        Case Else
                            Response.Redirect("admin.koko.pelajar.list.aspx?kokoid=" & strKeyID & "&admin_ID=" & Request.QueryString("admin_ID"))
                    End Select

                Case "INSTRUKTOR"
                    Response.Redirect("instruktor.koko.pelajar.list.aspx?kokoid=" & strKeyID & "&admin_ID=" & Request.QueryString("admin_ID"))
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
        Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.koko.list.sukan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.koko.list.sukan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case "PELAJAR"
                Response.Redirect("pelajar.koko.list.sukan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Protected Sub lnkUniform_Click(sender As Object, e As EventArgs) Handles lnkUniform.Click
        Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.koko.list.uniform.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.koko.list.uniform.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case "PELAJAR"
                Response.Redirect("pelajar.koko.list.uniform.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Protected Sub lnkRumahsukan_Click(sender As Object, e As EventArgs) Handles lnkRumahsukan.Click
        Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.koko.list.rumahsukan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.koko.list.rumahsukan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case "PELAJAR"
                Response.Redirect("pelajar.koko.list.rumahsukan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Protected Sub lnkPersatuan_Click(sender As Object, e As EventArgs) Handles lnkPersatuan.Click
        Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.koko.list.persatuan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.koko.list.persatuan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case "PELAJAR"
                Response.Redirect("pelajar.koko.list.persatuan.aspx?admin_ID=" & Request.QueryString("admin_ID"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        '--default
        strRet = BindDataSQL(datUniform)

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Try
            strSQL = "UPDATE koko_pelajar SET " & lblFieldname.Text & "=NULL WHERE Tahun='" & lblTahun.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "System error:" & strRet
            Else
                lblMsg.Text = "Berjaya RESET KOKO pelajar."
            End If

            '--default
            strRet = BindDataSQL(datUniform)
        Catch ex As Exception
            lblMsgTop.Text = "System error:" & ex.Message
        End Try

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        Try
            ExportToCSV(lblSQL.Text)

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

End Class