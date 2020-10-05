Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class tempahan_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                lblKodTempahan.Text = oCommon.getRandom

                '--get kemudahan year and name
                koko_kemudahan_load()

                '--set default date
                calStartDate.SelectedDate = Now.Date
                txtBookingDate.Text = calStartDate.SelectedDate.ToString("yyyy-MM-dd dddd")

                refreshScreen()
                check_tempahandetail()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_kemudahan_load()
        strSQL = "SELECT * FROM koko_kemudahan WHERE KemudahanID=" & Request.QueryString("kemudahanid")
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    lblTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    lblTahun.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kemudahan")) Then
                    lblKemudahan.Text = ds.Tables(0).Rows(0).Item("Kemudahan")
                Else
                    lblKemudahan.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.kemudahan.list.aspx?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

    Private Sub calStartDate_SelectionChanged(sender As Object, e As EventArgs) Handles calStartDate.SelectionChanged
        txtBookingDate.Text = calStartDate.SelectedDate.ToString("yyyy-MM-dd dddd")
        calStartDate.Visible = False

        refreshScreen()
        check_tempahandetail()
    End Sub

    Private Sub refreshScreen()
        Time07.Enabled = True
        Time08.Enabled = True
        Time09.Enabled = True
        Time10.Enabled = True
        Time11.Enabled = True
        Time12.Enabled = True
        Time13.Enabled = True
        Time14.Enabled = True
        Time15.Enabled = True
        Time16.Enabled = True
        Time17.Enabled = True
        Time18.Enabled = True
        Time19.Enabled = True
        Time20.Enabled = True
        Time21.Enabled = True
        Time22.Enabled = True
        Time23.Enabled = True


        Time07.Checked = False
        Time08.Checked = False
        Time09.Checked = False
        Time10.Checked = False
        Time11.Checked = False
        Time12.Checked = False
        Time13.Checked = False
        Time14.Checked = False
        Time15.Checked = False
        Time16.Checked = False
        Time17.Checked = False
        Time18.Checked = False
        Time19.Checked = False
        Time20.Checked = False
        Time21.Checked = False
        Time22.Checked = False
        Time23.Checked = False

    End Sub

    Protected Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click

        Try
            If ValidateForm() = False Then
                Exit Sub
            End If

            If koko_tempahandetail_insert() = False Then
                Exit Sub
            End If

            '--refresh page
            Response.Redirect(Request.RawUrl)

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

    Private Function koko_tempahandetail_insert() As Boolean
        strRet = ""
        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using connection As New SqlConnection(strconn)
            connection.Open()

            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction("TxnStart")

            ' Must assign both transaction object and connection 
            ' to Command object for a pending local transaction.
            command.Connection = connection
            command.Transaction = transaction
            command.CommandTimeout = 300    '5minit. timeout in second

            Try
                '--maklumat penempah
                strSQL = "INSERT INTO koko_tempahandetail(KodTempahan,KemudahanID,Pemohon,ContactNo,Catatan,BookingDate,Time07,Time08,Time09,Time10,Time11,Time12,Time13,Time14,Time15,Time16,Time17,Time18,Time19,Time20,Time21,Time22,Time23) VALUES ('" & lblKodTempahan.Text & "'," & Request.QueryString("kemudahanid") & ",'" & oCommon.FixSingleQuotes(txtPemohon.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtContactNo.Text) & "','" & oCommon.FixSingleQuotes(txtCatatan.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtBookingDate.Text) & "','" & CheckBox07.Checked & "','" & CheckBox08.Checked & "','" & CheckBox09.Checked & "','" & CheckBox10.Checked & "','" & CheckBox11.Checked & "','" & CheckBox12.Checked & "','" & CheckBox13.Checked & "','" & CheckBox14.Checked & "','" & CheckBox15.Checked & "','" & CheckBox16.Checked & "','" & CheckBox17.Checked & "','" & CheckBox18.Checked & "','" & CheckBox19.Checked & "','" & CheckBox20.Checked & "','" & CheckBox21.Checked & "','" & CheckBox22.Checked & "','" & CheckBox23.Checked & "')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Return True
            Catch ex As Exception
                strRet = ex.Message
                ' Attempt to roll back the transaction. 
                Try
                    transaction.Rollback()
                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    'Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    'Console.WriteLine("  Message: {0}", ex2.Message)
                    strRet += "Rollback:" & ex2.Message
                End Try
                lblMsg.Text = strRet
                Return False
            End Try
        End Using

    End Function

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtPemohon.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtPemohon.Focus()
            Return False
        End If

        If txtContactNo.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtContactNo.Focus()
            Return False
        End If

        If txtCatatan.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtCatatan.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub Time07_CheckedChanged(sender As Object, e As EventArgs) Handles Time07.CheckedChanged
        CheckBox07.Checked = Time07.Checked

    End Sub

    Private Sub Time08_CheckedChanged(sender As Object, e As EventArgs) Handles Time08.CheckedChanged
        CheckBox08.Checked = Time08.Checked

    End Sub

    Private Sub Time09_CheckedChanged(sender As Object, e As EventArgs) Handles Time09.CheckedChanged
        CheckBox09.Checked = Time09.Checked
    End Sub

    Private Sub Time10_CheckedChanged(sender As Object, e As EventArgs) Handles Time10.CheckedChanged
        CheckBox10.Checked = Time10.Checked
    End Sub

    Private Sub Time11_CheckedChanged(sender As Object, e As EventArgs) Handles Time11.CheckedChanged
        CheckBox11.Checked = Time11.Checked
    End Sub

    Private Sub Time12_CheckedChanged(sender As Object, e As EventArgs) Handles Time12.CheckedChanged
        CheckBox12.Checked = Time12.Checked
    End Sub

    Private Sub Time13_CheckedChanged(sender As Object, e As EventArgs) Handles Time13.CheckedChanged
        CheckBox13.Checked = Time13.Checked
    End Sub

    Private Sub Time14_CheckedChanged(sender As Object, e As EventArgs) Handles Time14.CheckedChanged
        CheckBox14.Checked = Time14.Checked
    End Sub

    Private Sub Time15_CheckedChanged(sender As Object, e As EventArgs) Handles Time15.CheckedChanged
        CheckBox15.Checked = Time15.Checked

    End Sub

    Private Sub Time16_CheckedChanged(sender As Object, e As EventArgs) Handles Time16.CheckedChanged
        CheckBox16.Checked = Time16.Checked

    End Sub

    Private Sub Time17_CheckedChanged(sender As Object, e As EventArgs) Handles Time17.CheckedChanged
        CheckBox17.Checked = Time17.Checked
    End Sub

    Private Sub Time18_CheckedChanged(sender As Object, e As EventArgs) Handles Time18.CheckedChanged
        CheckBox18.Checked = Time18.Checked
    End Sub

    Private Sub Time19_CheckedChanged(sender As Object, e As EventArgs) Handles Time19.CheckedChanged
        CheckBox19.Checked = Time19.Checked
    End Sub

    Private Sub Time20_CheckedChanged(sender As Object, e As EventArgs) Handles Time20.CheckedChanged
        CheckBox20.Checked = Time20.Checked
    End Sub

    Private Sub Time21_CheckedChanged(sender As Object, e As EventArgs) Handles Time21.CheckedChanged
        CheckBox21.Checked = Time21.Checked
    End Sub

    Private Sub Time22_CheckedChanged(sender As Object, e As EventArgs) Handles Time22.CheckedChanged
        CheckBox22.Checked = Time22.Checked
    End Sub

    Private Sub Time23_CheckedChanged(sender As Object, e As EventArgs) Handles Time23.CheckedChanged
        CheckBox23.Checked = Time23.Checked
    End Sub

    Private Sub btnDate_Click(sender As Object, e As ImageClickEventArgs) Handles btnDate.Click
        Dim [date] As New DateTime()
        'Flip the visibility attribute
        calStartDate.Visible = Not (calStartDate.Visible)
        'If the calendar is visible try assigning the date from the textbox
        If calStartDate.Visible Then
            'If the Conversion was successfull assign the textbox's date
            If DateTime.TryParse(txtBookingDate.Text, [date]) Then
                calStartDate.SelectedDate = [date]
            End If
            calStartDate.Attributes.Add("style", "POSITION: absolute")
        End If

    End Sub


    Private Sub check_tempahandetail()
        '--TIDAK LULUS iqnore
        Dim strQuery As String = "SELECT * FROM koko_tempahandetail WHERE KemudahanID=" & Request.QueryString("kemudahanid") & " AND BookingDate='" & oCommon.FixSingleQuotes(txtBookingDate.Text) & "' AND (StatusTempahan='POHON' OR StatusTempahan='LULUS')"

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                Dim columnName As String = dt.Columns(k).ColumnName
                Dim columnValue As Object = dt.Rows(i)(k).ToString()

                If columnValue Is Nothing Then
                    '--do nothing
                Else
                    Dim columnStringValue As String = columnValue.ToString()
                    setCheckbox(columnName, columnStringValue)
                    '--debug
                    'Response.Write(columnName & columnStringValue)
                End If
            Next
        Next
    End Sub

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

    Private Sub setCheckbox(ByVal strColumnName As String, ByVal strValue As String)
        Select Case strColumnName
            Case "Time07"
                If strValue = "True" Then
                    Time07.Checked = True
                    Time07.Enabled = False
                End If
            Case "Time08"
                If strValue = "True" Then
                    Time08.Checked = True
                    Time08.Enabled = False
                End If
            Case "Time09"
                If strValue = "True" Then
                    Time09.Checked = True
                    Time09.Enabled = False
                End If
            Case "Time10"
                If strValue = "True" Then
                    Time10.Checked = True
                    Time10.Enabled = False
                End If
            Case "Time11"
                If strValue = "True" Then
                    Time11.Checked = True
                    Time11.Enabled = False
                End If
            Case "Time12"
                If strValue = "True" Then
                    Time12.Checked = True
                    Time12.Enabled = False
                End If
            Case "Time13"
                If strValue = "True" Then
                    Time13.Checked = True
                    Time13.Enabled = False
                End If
            Case "Time14"
                If strValue = "True" Then
                    Time14.Checked = True
                    Time14.Enabled = False
                End If
            Case "Time15"
                If strValue = "True" Then
                    Time15.Checked = True
                    Time15.Enabled = False
                End If
            Case "Time16"
                If strValue = "True" Then
                    Time16.Checked = True
                    Time16.Enabled = False
                End If
            Case "Time17"
                If strValue = "True" Then
                    Time17.Checked = True
                    Time17.Enabled = False
                End If
            Case "Time18"
                If strValue = "True" Then
                    Time18.Checked = True
                    Time18.Enabled = False
                End If
            Case "Time19"
                If strValue = "True" Then
                    Time19.Checked = True
                    Time19.Enabled = False
                End If
            Case "Time20"
                If strValue = "True" Then
                    Time20.Checked = True
                    Time20.Enabled = False
                End If
            Case "Time21"
                If strValue = "True" Then
                    Time21.Checked = True
                    Time21.Enabled = False
                End If
            Case "Time22"
                If strValue = "True" Then
                    Time22.Checked = True
                    Time22.Enabled = False
                End If
            Case "Time23"
                If strValue = "True" Then
                    Time23.Checked = True
                    Time23.Enabled = False
                End If

        End Select

    End Sub

    

End Class