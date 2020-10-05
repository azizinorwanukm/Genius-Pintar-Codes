Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_pelajar_mark_sukanmandatory_p2
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

                list_kumpulan()
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

        Dim kokoinstruktorid As String = getkokoinstruktorid(Request.QueryString("instruktorid"))
        Dim tahun As String = Request.QueryString("tahun")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strGroup As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT
                        StudentProfile.StudentID,
                        StudentProfile.StudentFullname,
                        StudentProfile.MYKAD,
                        StudentProfile.NoPelajar,
                        StudentProfile.StudentRace,
                        StudentProfile.StudentGender,
                        koko_kelas.Kelas,
                        koko_kolejpermata.Tahun,
                        (SELECT koko_pelajarmandatory_markah.Markah FROM koko_pelajarmandatory_markah WHERE koko_pelajarmandatory_markah.StudentID = StudentProfile.StudentID AND koko_pelajarmandatory_markah.Peperiksaan = 'Peperiksaan 2') AS 'Markah'
                        FROM
                        StudentProfile
                        LEFT JOIN koko_pelajar ON StudentProfile.StudentID = koko_pelajar.StudentID
                        LEFT JOIN koko_kelas ON koko_pelajar.KelasID = koko_kelas.KelasID
                        LEFT JOIN koko_kelaskokopelajar ON koko_pelajar.kokopelajarid = koko_kelaskokopelajar.kokopelajarid
                        LEFT JOIN koko_kelaskoko ON koko_kelaskokopelajar.KelasKokoID = koko_kelaskoko.KelasKokoID
                        LEFT JOIN koko_kolejpermata ON koko_kelaskoko.KokoID = koko_kolejpermata.KokoID"
        strWhere = " WHERE
                        koko_kelaskoko.KelasKokoID = '" & ddlKumpulan2.SelectedValue & "'
                        AND koko_kelaskoko.kokoinstruktorid = '" & kokoinstruktorid & "'
                        AND koko_kolejpermata.Tahun = '" & tahun & "'"
        strGroup = " GROUP BY
                        StudentProfile.StudentID,
                        StudentProfile.StudentFullname,
                        StudentProfile.MYKAD,
                        StudentProfile.NoPelajar,
                        StudentProfile.StudentRace,
                        StudentProfile.StudentGender,
                        koko_kelas.Kelas,
                        koko_kolejpermata.Tahun"

        getSQL = tmpSQL & strWhere & strGroup & strOrder
        ''--debug
        'Response.Write(getSQL)


        Return getSQL

    End Function

    Private Function getkokoinstruktorid(ByVal instruktorid As String) As String
        strSQL = "SELECT kokoinstruktorid FROM koko_instruktor WHERE InstruktorID = '" & instruktorid & "' "
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Function getKokoID(ByVal kelaskokoid As String) As String
        strSQL = "SELECT KokoID FROM koko_kelaskoko WHERE KelasKokoID = '" & kelaskokoid & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
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
            Dim txtMarkah As TextBox = CType(datRespondent.Rows(i).FindControl("Markah"), TextBox)

            Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString
            '--get ppcsdate
            Dim strPPCSdate As String = getPPCSdate(strKeyID)
            Dim strProgram As String = getProgram(strKeyID)
            Dim strKelasID As String = getKelasID(strKeyID)

            strSQL = "SELECT Markah FROM koko_pelajarmandatory_markah WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "' AND Peperiksaan = 'Peperiksaan 2'"

            If Not oCommon.isExist(strSQL) Then
                strSQL = "INSERT INTO koko_pelajarmandatory_markah (StudentID, PPCSDate, Semester, Tahun, Date, Program, KelasID, SukanID, Markah, Peperiksaan) VALUES('" & strKeyID & "', '" & strPPCSdate & "', 'Semester 2', '" & Request.QueryString("tahun") & "', '" & Now.Date.ToString("yyyy-MM-dd") & "', '" & strProgram & "', '" & strKelasID & "', '" & Request.QueryString("value") & "', '" & txtMarkah.Text & "', 'Peperiksaan 2')"
            Else
                strSQL = "UPDATE koko_pelajarmandatory_markah SET Markah ='" & txtMarkah.Text & "' WHERE StudentID='" & strKeyID & "' AND Tahun='" & Request.QueryString("tahun") & "' AND Peperiksaan = 'Peperiksaan 2'"
            End If

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

    Private Function getPPCSdate(ByVal strKeyID As String) As String
        strSQL = "SELECT PPCSDate FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Function getProgram(ByVal strKeyID As String) As String
        strSQL = "SELECT Program FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Function getKelasID(ByVal strKeyID As String) As String
        strSQL = "SELECT KelasID FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Sub list_kumpulan()

        Dim kokoinstruktorid As String = getkokoinstruktorid(Request.QueryString("instruktorid"))

        strSQL = "SELECT Kelas, KelasKokoID FROM koko_kelaskoko WHERE kokoinstruktorid = '" & kokoinstruktorid & "' ORDER BY Kelas"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKumpulan2.DataSource = ds
            ddlKumpulan2.DataTextField = "Kelas"
            ddlKumpulan2.DataValueField = "KelasKokoID"
            ddlKumpulan2.DataBind()

            'ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    '--CHECK form validation.
    'Kehadiran 0.5	Jawatan 0.1	Penglibatan 0.2	Pencapaian 0.2
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim txtMarkah As TextBox = DirectCast(row.FindControl("Markah"), TextBox)

            '--validate NUMBER and less than 100
            '--txtMarkah
            If Not txtMarkah.Text.Length = 0 Then
                If oCommon.IsCurrency(txtMarkah.Text) = False Then
                    Return False
                End If
                If CInt(txtMarkah.Text) > 100 Then
                    Return False
                End If
            Else
                txtMarkah.Text = "0"
            End If
        Next

        Return True
    End Function

    Protected Sub ddlKumpulan2_SelectedIndexChanged(sender As Object, e As EventArgs)
        strRet = BindData(datRespondent)
    End Sub
End Class