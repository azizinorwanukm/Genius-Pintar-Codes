Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_instruktor_transfer
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnUpdate.Attributes.Add("onclick", "return confirm('Pasti ingin memindahkan instruktor ke tahun tersebut?');")

        Try
            If Not IsPostBack Then
                lblMsgTop.Text = ""

                koko_tahun_list()
                ddlTahun.SelectedValue = oCommon.getAppsettings("DefaultKOKOYear")
                ddlTahunUpdate.SelectedValue = oCommon.getAppsettings("DefaultKOKOYear")

                '--default value
                lblTahunLama.Text = ddlTahun.SelectedItem.Text
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

            '--ddlTahunUpdate
            ddlTahunUpdate.DataSource = ds
            ddlTahunUpdate.DataTextField = "Tahun"
            ddlTahunUpdate.DataValueField = "Tahun"
            ddlTahunUpdate.DataBind()

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

                lblMsg.Text = "Tiada rekod instruktor."
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
        tmpSQL = "SELECT koko_instruktor.InstruktorID,koko_instruktor.UniformID,koko_instruktor.PersatuanID,koko_instruktor.SukanID,koko_instruktor.RumahSukanID,koko_instruktor.Fullname,koko_instruktor.MYKAD,koko_instruktor.ContactNo,koko_instruktor.Email,koko_instruktor.Tahun,koko_instruktor.BankName,koko_instruktor.AcctNo,koko_kelas.Kelas,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.UniformID=koko_kolejpermata.KokoID) as Uniform,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.SukanID=koko_kolejpermata.KokoID) as Sukan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan"
        tmpSQL += " FROM koko_instruktor"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_instruktor.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_instruktor.Tahun='" & ddlTahun.Text & "'"

        If Not ddlTahun.Text = "ALL" Then
            strWhere += " AND koko_instruktor.Tahun='" & ddlTahun.SelectedValue & "'"
        End If

        'If Not txtMYKAD.Text.Length = 0 Then
        '    strWhere += " AND koko_instruktor.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        'End If
        'If Not txtFullname.Text.Length = 0 Then
        '    strWhere += " AND koko_instruktor.Fullname LIKE '%" & oCommon.FixSingleQuotes(txtFullname.Text) & "%'"
        'End If

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
                    Response.Redirect("admin.instruktor.view.aspx?instruktorid=" & strKeyID & "&tahun=" & ddlTahun.Text & "&admin_ID=" & Request.QueryString("admin_ID"))
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

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If chkInstruktorALL.Checked = True Then
            TransferALL()
        Else
            TransferSelected()
        End If

    End Sub

    Private Sub TransferSelected()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

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

                    tmpSQL = "INSERT INTO koko_instruktor(InstruktorID,Tahun,Fullname,ContactNo,Email,MYKAD,BankName,AcctNo,Address1,Address2,PostCode,City,State,LoginID,Pwd)"
                    tmpSQL += " SELECT NEWID(),'" & ddlTahunUpdate.SelectedValue & "',Fullname,ContactNo,Email,MYKAD,BankName,AcctNo,Address1,Address2,PostCode,City,State,LoginID,Pwd FROM koko_instruktor"
                    strWhere = " WHERE Tahun='" & ddlTahun.SelectedValue & "'"
                    strWhere += " AND InstruktorID='" & strKey & "'"

                    strSQL = tmpSQL + strWhere
                    If oCommon.getAppsettings("isDebug") = "Y" Then
                        lblDebug.Text = "Debug strSQL:" & strSQL
                    End If
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += strKey & ":" & strRet & "|"
                    End If
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya pindah instruktor ke Tahun: " & ddlTahunUpdate.SelectedItem.Text
        End If


    End Sub

    Private Sub TransferALL()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        tmpSQL = "INSERT INTO koko_instruktor(InstruktorID,Tahun,Fullname,ContactNo,Email,MYKAD,BankName,AcctNo,Address1,Address2,PostCode,City,State,LoginID,Pwd)"
        tmpSQL += "SELECT NEWID(),'" & ddlTahunUpdate.SelectedValue & "',Fullname,ContactNo,Email,MYKAD,BankName,AcctNo,Address1,Address2,PostCode,City,State,LoginID,Pwd FROM koko_instruktor"
        strWhere = " WHERE Tahun='" & ddlTahun.SelectedValue & "'"

        strSQL = tmpSQL + strWhere
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya memindahkan kesemua instruktor ke Tahun: " & ddlTahunUpdate.SelectedItem.Text
        Else
            lblMsg.Text = strRet
        End If
        lblMsgTop.Text = lblMsg.Text

    End Sub

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        lblMsgTop.Text = ""

        '--default list
        strRet = BindData(datRespondent)
        lblTahunLama.Text = ddlTahun.SelectedItem.Text

    End Sub

End Class