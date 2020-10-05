Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_pelajar_mark_uniform_p1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                getKOKOName()
                '--default list
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    '--get kokoname
    Private Sub getKOKOName()
        Dim strKokoID As String = Request.QueryString("value")

        strSQL = "SELECT Nama FROM koko_kolejpermata WHERE KokoID=" & strKokoID
        lblKOKOName.Text = oCommon.getFieldValue(strSQL)
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        Select Case Request.QueryString("peperiksaan")
            Case "1"
            Case "2"
            Case "3"
            Case "4"
            Case Else
        End Select

        tmpSQL = "SELECT koko_pelajar.kokopelajarid,koko_pelajar.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion,koko_pelajar.Tahun,koko_pelajar.StatusTawaran,koko_pelajar.Uniform_PencapaianP1,koko_pelajar.Uniform_PenglibatanP1,koko_pelajar.Uniform_JawatanP1,koko_pelajar.Uniform_KehadiranP1,koko_kelas.Kelas,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan"
        tmpSQL += " FROM koko_pelajar"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_pelajar.Tahun='" & Request.QueryString("tahun") & "'"
        strWhere += " AND koko_pelajar." & Request.QueryString("field") & "=" & Request.QueryString("value")

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
                    Response.Redirect("admin.pelajar.view.aspx?studentid=" & strKeyID)
                Case "INSTRUKTOR"
                    Response.Redirect("instruktor.pelajar.view.aspx?studentid=" & strKeyID)
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
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        If ValidateForm() = False Then
            lblMsg.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim txtPencapaian As TextBox = DirectCast(row.FindControl("txtPencapaian"), TextBox)
            Dim txtPenglibatan As TextBox = DirectCast(row.FindControl("txtPenglibatan"), TextBox)
            Dim txtJawatan As TextBox = DirectCast(row.FindControl("txtJawatan"), TextBox)
            Dim txtKehadiran As TextBox = DirectCast(row.FindControl("txtKehadiran"), TextBox)

            strSQL = "UPDATE koko_pelajar SET Uniform_PencapaianP1=" & txtPencapaian.Text & ",Uniform_PenglibatanP1=" & txtPenglibatan.Text & ",Uniform_JawatanP1=" & txtJawatan.Text & ",Uniform_KehadiranP1=" & txtKehadiran.Text & " WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND Tahun='" & Request.QueryString("tahun") & "'"
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

        '--calculate P1
        strRet = oCommon.set_pelajar_Gred(Request.QueryString("tahun"), "P1")
        '--debug
        'Response.Write(strRet)

        strRet = oCommon.set_pelajar_Jumlah(Request.QueryString("tahun"), "P1")
        '--debug
        'Response.Write(strRet)

    End Sub

    Protected Sub btnTemplate_Click(sender As Object, e As EventArgs) Handles btnTemplate.Click
        Dim strQueryTemplate As String = ""

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion,koko_pelajar.Tahun,koko_kelas.Kelas,koko_uniform.Uniform,koko_pelajar.Uniform_PencapaianP1 as Pencapaian,koko_pelajar.Uniform_PenglibatanP1 as Penglibatan,koko_pelajar.Uniform_JawatanP1 as Jawatan,koko_pelajar.Uniform_KehadiranP1 as Kehadiran FROM koko_pelajar"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID"
        tmpSQL += " LEFT OUTER JOIN koko_uniform ON koko_pelajar.UniformID=koko_uniform.UniformID"
        tmpSQL += " LEFT OUTER JOIN koko_persatuan ON koko_pelajar.PersatuanID=koko_persatuan.PersatuanID"
        tmpSQL += " LEFT OUTER JOIN koko_sukan ON koko_pelajar.SukanID=koko_sukan.SukanID"
        tmpSQL += " LEFT OUTER JOIN koko_rumahsukan ON koko_pelajar.RumahsukanID=koko_rumahsukan.RumahsukanID"
        strWhere = " WHERE koko_pelajar.Tahun='" & Request.QueryString("tahun") & "'"
        strWhere += " AND koko_pelajar." & Request.QueryString("field") & "=" & Request.QueryString("value")

        strQueryTemplate = tmpSQL & strWhere & strOrder

        Try
            ExportToCSV(strQueryTemplate)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    '--CHECK form validation.
    'Kehadiran 0.5	Jawatan 0.1	Penglibatan 0.2	Pencapaian 0.2
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim txtPencapaian As TextBox = DirectCast(row.FindControl("txtPencapaian"), TextBox)
            Dim txtPenglibatan As TextBox = DirectCast(row.FindControl("txtPenglibatan"), TextBox)
            Dim txtJawatan As TextBox = DirectCast(row.FindControl("txtJawatan"), TextBox)
            Dim txtKehadiran As TextBox = DirectCast(row.FindControl("txtKehadiran"), TextBox)

            '--validate NUMBER and less than 100
            '--txtKehadiran
            If Not txtKehadiran.Text.Length = 0 Then
                If oCommon.IsCurrency(txtKehadiran.Text) = False Then
                    Return False
                End If
                If CInt(txtKehadiran.Text) > 100 Then
                    Return False
                End If
            Else
                txtKehadiran.Text = "0"
            End If

            '--txtJawatan
            If Not txtJawatan.Text.Length = 0 Then
                If oCommon.IsCurrency(txtJawatan.Text) = False Then
                    Return False
                End If
                If CInt(txtJawatan.Text) > 100 Then
                    Return False
                End If
            Else
                txtJawatan.Text = "0"
            End If

            '--txtPenglibatan
            If Not txtPenglibatan.Text.Length = 0 Then
                If oCommon.IsCurrency(txtPenglibatan.Text) = False Then
                    Return False
                End If
                If CInt(txtPenglibatan.Text) > 100 Then
                    Return False
                End If
            Else
                txtPenglibatan.Text = "0"
            End If

            '--txtPencapaian
            If Not txtPencapaian.Text.Length = 0 Then
                If oCommon.IsCurrency(txtPencapaian.Text) = False Then
                    Return False
                End If
                If CInt(txtPencapaian.Text) > 100 Then
                    Return False
                End If
            Else
                txtPencapaian.Text = "0"
            End If
        Next

        Return True
    End Function

End Class