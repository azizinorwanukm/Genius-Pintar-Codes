Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class kelaskoko_list_pelajar

    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                getEventID()

                '--default
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    '--get eventid
    Private Sub getEventID()
        Dim strField As String = ""
        Dim kokoid As String = Request.QueryString("kokoid")
        '--eventid
        strSQL = "SELECT EventID FROM koko_event WHERE KokoID=" & kokoid
        Dim eventid As String = oCommon.getFieldValue(strSQL)
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

        Dim kelaskokoid As String = Request.QueryString("kelaskokoid")
        Dim kokoinstruktorid As String = Request.QueryString("kokoinstruktorid")
        Dim tahun As String = Request.QueryString("tahun")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strGroup As String = ""
        Dim strOrder As String = " ORDER BY koko_kelas.Kelas ASC"
        tmpSQL = "SELECT
                        StudentProfile.StudentID,
                        StudentProfile.StudentFullname,
                        StudentProfile.MYKAD,
                        StudentProfile.NoPelajar,
                        koko_kelas.Kelas,
                        koko_event.EventID,
                        (SELECT koko_pelajarmandatory.Kehadiran FROM koko_pelajarmandatory WHERE koko_pelajarmandatory.StudentID = StudentProfile.StudentID AND koko_pelajarmandatory.EventID = koko_event.EventID) AS 'Kehadiran'
                        FROM
                        StudentProfile
                        LEFT JOIN koko_pelajar ON StudentProfile.StudentID = koko_pelajar.StudentID
                        LEFT JOIN koko_kelas ON koko_pelajar.KelasID = koko_kelas.KelasID
                        LEFT JOIN koko_kelaskokopelajar ON koko_pelajar.kokopelajarid = koko_kelaskokopelajar.kokopelajarid
                        LEFT JOIN koko_kelaskoko ON koko_kelaskokopelajar.KelasKokoID = koko_kelaskoko.KelasKokoID
                        LEFT JOIN koko_kolejpermata ON koko_kelaskoko.KokoID = koko_kolejpermata.KokoID
                        LEFT JOIN koko_kumpulan ON koko_kelaskokopelajar.KelasKokoID = koko_kumpulan.KelasKokoID
                        LEFT JOIN koko_event ON koko_kumpulan.EventID = koko_event.EventID
                        LEFT JOIN koko_pelajarmandatory ON koko_kumpulan.EventID = koko_pelajarmandatory.EventID"
        strWhere = " WHERE
                        koko_kelaskoko.KelasKokoID = '" & kelaskokoid & "'
                        AND koko_kelaskoko.kokoinstruktorid = '" & kokoinstruktorid & "'
                        AND koko_kolejpermata.Tahun = '" & tahun & "'
                        AND koko_event.EventID = '" & Request.QueryString("eventid") & "'"
        strGroup = " GROUP BY
                        StudentProfile.StudentID,
                        StudentProfile.StudentFullname,
                        StudentProfile.MYKAD,
                        StudentProfile.NoPelajar,
                        koko_kelas.Kelas,
                        koko_event.EventID"

        getSQL = tmpSQL & strWhere & strGroup & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Protected Sub btnAssign_Click(sender As Object, e As EventArgs) Handles btnAssign.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Dim strKeyID As String = ""
        Dim strEventID As String = ""
        Dim strPPCSdate As String = ""
        Dim strDate As String = ""
        Dim strProgram As String = ""
        Dim strKelasID As String = ""
        Dim strSukanID As String = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count.ToString - 1 Step i + 1

            '--get studentid
            strKeyID = datRespondent.DataKeys(i).Value.ToString
            '--get eventid
            strEventID = getEventID(strKeyID)
            '--get ppcsdate
            strPPCSdate = getPPCSdate(strKeyID)
            '--get semester

            '--get date
            strDate = getDate(strEventID)
            '--get program
            strProgram = getProgram(strKeyID)
            '--get kelasid
            strKelasID = getKelasID(strKeyID)
            '--get sukanid
            strSukanID = getSukanID(strKeyID)

            Dim chkKehadiran As Label = CType(datRespondent.Rows(i).FindControl("Status"), Label)
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                If chkKehadiran.Text = "" Then
                    'Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then
                        strSQL = "INSERT INTO koko_pelajarmandatory (StudentID, EventID, PPCSDate, Tahun, Date, Program, KelasID, SukanID, Kehadiran, KelasKokoID) VALUES('" & strKey & "', '" & Request.QueryString("eventid") & "', '" & strPPCSdate & "', '" & Request.QueryString("tahun") & "', '" & strDate & "', '" & strProgram & "', '" & strKelasID & "', '" & strSukanID & "', 'Hadir', '" & Request.QueryString("kelaskokoid") & "')"
                    ElseIf chkUpdate.Checked = False Then
                        strSQL = "INSERT INTO koko_pelajarmandatory (StudentID, EventID, PPCSDate, Tahun, Date, Program, KelasID, SukanID, Kehadiran, KelasKokoID) VALUES('" & strKey & "', '" & Request.QueryString("eventid") & "', '" & strPPCSdate & "', '" & Request.QueryString("tahun") & "', '" & strDate & "', '" & strProgram & "', '" & strKelasID & "', '" & strSukanID & "', 'Tidak Hadir', '" & Request.QueryString("kelaskokoid") & "')"
                    End If
                Else
                    If chkUpdate.Checked = True Then
                        strSQL = "UPDATE koko_pelajarmandatory SET Kehadiran = 'Hadir' WHERE StudentID = '" & strKeyID & "' AND EventID = '" & Request.QueryString("eventid") & "'"
                    ElseIf chkUpdate.Checked = False Then
                        strSQL = "UPDATE koko_pelajarmandatory SET Kehadiran = 'Tidak Hadir' WHERE StudentID = '" & strKeyID & "' AND EventID = '" & Request.QueryString("eventid") & "'"
                    End If
                End If
                '--execute SQL
                strRet = oCommon.ExecuteSQL(strSQL)
                If Not strRet = "0" Then
                    lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    Private Function getEventID(ByVal strKeyID As String) As String
        strSQL = "SELECT EventID FROM koko_event WHERE KokoID = '" & Request.QueryString("kokoid") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function
    Private Function getPPCSdate(ByVal strKeyID As String) As String
        strSQL = "SELECT PPCSDate FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function
    Private Function getDate(ByVal strEventID As String) As String
        strSQL = "SELECT EventDate FROM koko_event WHERE EventID = '" & strEventID & "'"
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
    Private Function getSukanID(ByVal strKeyID As String) As String
        strSQL = "SELECT SukanID FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

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