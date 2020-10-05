Imports System.Data.SqlClient

Public Class koko_pelajar_list_mark
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
                lblMsgTop.Text = "A:Kehadiran. B:Jawatan. C:Penglibatan. D:Pencapaian."

                koko_tahun_list()
                ddlTahun.SelectedValue = lblTahun.Text

                koko_kelas_list()
                ddlKelas.Text = "ALL"

                '--default list
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Private Sub koko_kelas_list()
        strSQL = "SELECT * FROM koko_kelas WHERE Tahun='" & ddlTahun.Text & "' ORDER BY Kelas ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "Kelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            ddlKelas.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    '--getKOKODetail
    Private Sub getKOKODetail()
        strSQL = "SELECT Jenis FROM koko_kolejpermata WHERE KokoID=" & Request.QueryString("kokoid")
        lblJenis.Text = oCommon.getFieldValue(strSQL)
        Select Case lblJenis.Text
            Case "PERSATUAN"
                lblFieldname.Text = "PersatuanID"
            Case "RUMAHSUKAN"
                lblFieldname.Text = "RumahsukanID"
            Case "SUKAN"
                lblFieldname.Text = "SukanID"
            Case "UNIFORM"
                lblFieldname.Text = "UniformID"
        End Select

        strSQL = "SELECT Nama FROM koko_kolejpermata WHERE KokoID=" & Request.QueryString("kokoid")
        lblKOKOName.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Tahun FROM koko_kolejpermata WHERE KokoID=" & Request.QueryString("kokoid")
        lblTahun.Text = oCommon.getFieldValue(strSQL)

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

                lblMsg.Text = "Tiada rekod pelajar."
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
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        '--MARKAH PELAJAR
        tmpSQL = "SELECT koko_pelajar.kokopelajarid,koko_pelajar.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion,koko_pelajar.Tahun,koko_kelas.Kelas,koko_pelajar.StatusTawaran,"
        '                                                                        Uniform_KehadiranP1,                                                          Uniform_JawatanP1,                                                              Uniform_PenglibatanP1,                                                             Uniform_PencapaianP1,                                                         Uniform_JumlahP1,                                                       Uniform_GredP1"
        tmpSQL += "koko_pelajar.Uniform_Kehadiran" & selPeperiksaan.Value & " as Uniform_Kehadiran,koko_pelajar.Uniform_Jawatan" & selPeperiksaan.Value & " as Uniform_Jawatan,koko_pelajar.Uniform_Penglibatan" & selPeperiksaan.Value & " as Uniform_Penglibatan,koko_pelajar.Uniform_Pencapaian" & selPeperiksaan.Value & " as Uniform_Pencapaian,koko_pelajar.Uniform_Jumlah" & selPeperiksaan.Value & " as Uniform_Jumlah,koko_pelajar.Uniform_Gred" & selPeperiksaan.Value & " as Uniform_Gred,"
        '                                                                          Persatuan_KehadiranP1,                                                            Persatuan_JawatanP1,                                                                Persatuan_PenglibatanP1,                                                               Persatuan_PencapaianP1,                                                           Persatuan_JumlahP1,                                                         Persatuan_GredP1,
        tmpSQL += "koko_pelajar.Persatuan_Kehadiran" & selPeperiksaan.Value & " as Persatuan_Kehadiran,koko_pelajar.Persatuan_Jawatan" & selPeperiksaan.Value & " as Persatuan_Jawatan,koko_pelajar.Persatuan_Penglibatan" & selPeperiksaan.Value & " as Persatuan_Penglibatan,koko_pelajar.Persatuan_Pencapaian" & selPeperiksaan.Value & " as Persatuan_Pencapaian,koko_pelajar.Persatuan_Jumlah" & selPeperiksaan.Value & " as Persatuan_Jumlah,koko_pelajar.Persatuan_Gred" & selPeperiksaan.Value & " as Persatuan_Gred,"
        '                                                                      Sukan_KehadiranP1,                                                        Sukan_JawatanP1,                                                            Sukan_PenglibatanP1,                                                           Sukan_PencapaianP1,                                                       Sukan_JumlahP1,                                                     Sukan_GredP1
        tmpSQL += "koko_pelajar.Sukan_Kehadiran" & selPeperiksaan.Value & " as Sukan_Kehadiran,koko_pelajar.Sukan_Jawatan" & selPeperiksaan.Value & " as Sukan_Jawatan,koko_pelajar.Sukan_Penglibatan" & selPeperiksaan.Value & " as Sukan_Penglibatan,koko_pelajar.Sukan_Pencapaian" & selPeperiksaan.Value & " as Sukan_Pencapaian,koko_pelajar.Sukan_Jumlah" & selPeperiksaan.Value & " as Sukan_Jumlah,koko_pelajar.Sukan_Gred" & selPeperiksaan.Value & " as Sukan_Gred,"
        '                                                            BonusP1,                                                 JumlahP1,                                                 MarkahP1,                                               GredP1,                                              PNGP1,                                               KOKOP1,
        tmpSQL += "koko_pelajar.Bonus" & selPeperiksaan.Value & " as Bonus,koko_pelajar.Jumlah" & selPeperiksaan.Value & " as Jumlah,koko_pelajar.Markah" & selPeperiksaan.Value & " as Markah,koko_pelajar.Gred" & selPeperiksaan.Value & " as Gred,koko_pelajar.PNG" & selPeperiksaan.Value & " as PNG,koko_pelajar.KOKO" & selPeperiksaan.Value & " as KOKO,"

        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan"
        tmpSQL += " FROM koko_pelajar"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_pelajar.Tahun='" & lblTahun.Text & "'"
        strWhere += " AND koko_pelajar." & lblFieldname.Text & "=" & Request.QueryString("kokoid")

        If Not ddlKelas.Text = "ALL" Then
            strWhere += " AND koko_pelajar.KelasID='" & ddlKelas.SelectedValue & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
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
                    Response.Redirect("admin.pelajar.view.aspx?studentid=" & strKeyID & "&admin_ID=" & Request.QueryString("admin_ID"))
                Case "INSTRUKTOR"
                    Response.Redirect("instruktor.pelajar.view.aspx?studentid=" & strKeyID & "&admin_ID=" & Request.QueryString("admin_ID"))
                Case "PENSYARAH"
                Case "PENGARAH"
                Case Else
                    lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
            End Select

        Catch ex As Exception
            lblMsg.Text = "System Error: " & ex.Message
        End Try

    End Sub
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim txtBonus As TextBox = DirectCast(row.FindControl("txtBonus"), TextBox)


            '--validate NUMBER and less than 100
            '--txtKehadiran
            If Not txtBonus.Text.Length = 0 Then
                If oCommon.IsCurrency(txtBonus.Text) = False Then
                    Return False
                End If
                If CInt(txtBonus.Text) > 100 Then
                    Return False
                End If
            Else
                txtBonus.Text = "0"
            End If
        Next

        Return True
    End Function

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
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        If ValidateForm() = False Then
            lblMsg.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim txtBonus As TextBox = DirectCast(row.FindControl("txtBonus"), TextBox)

            strSQL = "UPDATE koko_pelajar SET Bonus" & selPeperiksaan.Value & "=" & txtBonus.Text & " WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND Tahun='" & lblTahun.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
            End If
        Next

        ''refresh screen
        'strRet = BindData(datRespondent)
        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya kemaskini markah."
        End If
        lblMsgTop.Text = lblMsg.Text

        Bonus()

        strRet = BindData(datRespondent)

    End Sub
    Public Sub Bonus()
        Dim strPeperiksaan As String = "P1"
        'JumlahP1=Uniform_JumlahP1+Persatuan_JumlahP1+Sukan_JumlahP1+BonusP1
        strSQL = "UPDATE koko_pelajar SET Jumlah" & strPeperiksaan & "=Uniform_Jumlah" & strPeperiksaan & "+Persatuan_Jumlah" & strPeperiksaan & "+Sukan_Jumlah" & strPeperiksaan & "+Bonus" & strPeperiksaan
        strSQL += " WHERE Tahun='" & ddlTahun.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then

        End If

        'MarkahP1=JumlahP1/3
        strSQL = "UPDATE koko_pelajar SET Markah" & strPeperiksaan & "=Jumlah" & strPeperiksaan & "/3"
        strSQL += " WHERE Tahun='" & ddlTahun.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then

        End If

        'KOKOP1=PNG/4*15
        strSQL = "UPDATE koko_pelajar SET KOKO" & strPeperiksaan & "=PNG" & strPeperiksaan & "/4*15"
        strSQL += " WHERE Tahun='" & ddlTahun.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then

        End If



    End Sub
    Protected Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        lblMsgTop.Text = ""

        '--set_pelajar_Jumlah
        strRet = oCommon.set_pelajar_Jumlah(lblTahun.Text, selPeperiksaan.Value)
        If Not strRet = "0" Then
            lblMsg.Text = "Err: " & strRet
            Exit Sub
        Else
            lblMsg.Text = "Berjaya mengemaskini Jumlah pelajar!" & vbCrLf
        End If

        '--set_pelajar_Gred
        strRet = oCommon.set_pelajar_Gred(lblTahun.Text, selPeperiksaan.Value)
        If Not strRet = "0" Then
            lblMsg.Text = "Err: " & strRet
            Exit Sub
        Else
            lblMsg.Text += "Berjaya mengemaskini Gred pelajar!"
        End If
        lblMsgTop.Text = lblMsg.Text

    End Sub

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        '--default list
        strRet = BindData(datRespondent)

    End Sub

    Private Sub lnkMarkahPenuh_Click(sender As Object, e As EventArgs) Handles lnkMarkahPenuh.Click
        Dim queryString As String = "admin.laporan.koko.pelajar.list.full.aspx?tahun=" & lblTahun.Text & "&field=" & Request.QueryString("field") & "&value=" & Request.QueryString("value") & "&admin_ID=" & Request.QueryString("admin_ID")
        Dim newWin As String = "window.open('" & queryString & "');"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Sistem Pengurusan Kokurikulum Pelajar", newWin, True)

    End Sub

    Private Sub datRespondent_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

            Dim HeaderCell As New TableCell()
            HeaderCell.Text = ""
            HeaderCell.BorderStyle = BorderStyle.Solid
            HeaderCell.BorderWidth = 1
            HeaderCell.ColumnSpan = 7
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "BADAN BERUNIFORM"
            HeaderCell.BorderStyle = BorderStyle.Solid
            HeaderCell.BorderWidth = 1
            HeaderCell.ColumnSpan = 5
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "KELAB & PERSATUAN"
            HeaderCell.BorderStyle = BorderStyle.Solid
            HeaderCell.BorderWidth = 1
            HeaderCell.ColumnSpan = 5
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "SUKAN & PERMAINAN"
            HeaderCell.BorderStyle = BorderStyle.Solid
            HeaderCell.BorderWidth = 1
            HeaderCell.ColumnSpan = 5
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = ""
            HeaderCell.BorderStyle = BorderStyle.Solid
            HeaderCell.BorderWidth = 1
            HeaderCell.ColumnSpan = 7
            HeaderGridRow.Cells.Add(HeaderCell)

            datRespondent.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If

    End Sub
End Class