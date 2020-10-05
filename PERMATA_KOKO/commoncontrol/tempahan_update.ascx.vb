Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class tempahan_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                koko_kemudahan_load()

                '--set default date
                calStartDate.SelectedDate = Now.Date
                txtBookingDate.Text = calStartDate.SelectedDate.ToString("yyyy-MM-dd dddd")
                setCheckBox()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_masa_list(ByVal ddlControl As DropDownList)
        strSQL = "SELECT Masa FROM koko_masa ORDER BY Masa ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlControl.DataSource = ds
            ddlControl.DataTextField = "Masa"
            ddlControl.DataValueField = "Masa"
            ddlControl.DataBind()

            ''ddlControl.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
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
        Response.Redirect("admin.kemudahan.list.aspx")

    End Sub

    Private Sub calStartDate_SelectionChanged(sender As Object, e As EventArgs) Handles calStartDate.SelectionChanged
        'txtBookingDate.Text = calStartDate.SelectedDate.ToString("yyyyMMdd")
        txtBookingDate.Text = calStartDate.SelectedDate.ToString("yyyy-MM-dd dddd")
        calStartDate.Visible = False

        setCheckBox()
        setChkEnable()
    End Sub

    Private Sub setCheckBox()
        strSQL = "SELECT * FROM koko_tempahan WHERE KemudahanID=" & Request.QueryString("kemudahanid") & " AND BookingDate='" & oCommon.FixSingleQuotes(txtBookingDate.Text) & "'"

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time07")) Then
                    Time07.Checked = ds.Tables(0).Rows(0).Item("Time07")
                Else
                    Time07.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time08")) Then
                    Time08.Checked = ds.Tables(0).Rows(0).Item("Time08")
                Else
                    Time08.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time09")) Then
                    Time09.Checked = ds.Tables(0).Rows(0).Item("Time09")
                Else
                    Time09.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time10")) Then
                    Time10.Checked = ds.Tables(0).Rows(0).Item("Time10")
                Else
                    Time10.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time11")) Then
                    Time11.Checked = ds.Tables(0).Rows(0).Item("Time11")
                Else
                    Time11.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time12")) Then
                    Time12.Checked = ds.Tables(0).Rows(0).Item("Time12")
                Else
                    Time12.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time13")) Then
                    Time13.Checked = ds.Tables(0).Rows(0).Item("Time13")
                Else
                    Time13.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time14")) Then
                    Time14.Checked = ds.Tables(0).Rows(0).Item("Time14")
                Else
                    Time14.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time15")) Then
                    Time15.Checked = ds.Tables(0).Rows(0).Item("Time15")
                Else
                    Time15.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time16")) Then
                    Time16.Checked = ds.Tables(0).Rows(0).Item("Time16")
                Else
                    Time16.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time17")) Then
                    Time17.Checked = ds.Tables(0).Rows(0).Item("Time17")
                Else
                    Time17.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time18")) Then
                    Time18.Checked = ds.Tables(0).Rows(0).Item("Time18")
                Else
                    Time18.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time19")) Then
                    Time19.Checked = ds.Tables(0).Rows(0).Item("Time19")
                Else
                    Time19.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time20")) Then
                    Time20.Checked = ds.Tables(0).Rows(0).Item("Time20")
                Else
                    Time20.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time21")) Then
                    Time21.Checked = ds.Tables(0).Rows(0).Item("Time21")
                Else
                    Time21.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time22")) Then
                    Time22.Checked = ds.Tables(0).Rows(0).Item("Time22")
                Else
                    Time22.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Time23")) Then
                    Time23.Checked = ds.Tables(0).Rows(0).Item("Time23")
                Else
                    Time23.Checked = False
                End If

                '--disabled is checked
                setChkEnable()
            Else
                '--clear checkbox
                refreshScreen()
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try


    End Sub

    Private Sub setChkEnable()
        If Time07.Checked = True Then
            Time07.Enabled = False
        Else
            Time07.Enabled = True
        End If
        If Time08.Checked = True Then
            Time08.Enabled = False
        Else
            Time08.Enabled = True
        End If
        If Time09.Checked = True Then
            Time09.Enabled = False
        Else
            Time09.Enabled = True
        End If
        If Time10.Checked = True Then
            Time10.Enabled = False
        Else
            Time10.Enabled = True
        End If
        If Time11.Checked = True Then
            Time11.Enabled = False
        Else
            Time11.Enabled = True
        End If
        If Time12.Checked = True Then
            Time12.Enabled = False
        Else
            Time12.Enabled = True
        End If
        If Time13.Checked = True Then
            Time13.Enabled = False
        Else
            Time13.Enabled = True
        End If
        If Time14.Checked = True Then
            Time14.Enabled = False
        Else
            Time14.Enabled = True
        End If
        If Time15.Checked = True Then
            Time15.Enabled = False
        Else
            Time15.Enabled = True
        End If
        If Time16.Checked = True Then
            Time16.Enabled = False
        Else
            Time16.Enabled = True
        End If
        If Time17.Checked = True Then
            Time17.Enabled = False
        Else
            Time17.Enabled = True
        End If
        If Time18.Checked = True Then
            Time18.Enabled = False
        Else
            Time18.Enabled = True
        End If
        If Time19.Checked = True Then
            Time19.Enabled = False
        Else
            Time19.Enabled = True
        End If
        If Time20.Checked = True Then
            Time20.Enabled = False
        Else
            Time20.Enabled = True
        End If
        If Time21.Checked = True Then
            Time21.Enabled = False
        Else
            Time21.Enabled = True
        End If
        If Time22.Checked = True Then
            Time22.Enabled = False
        Else
            Time22.Enabled = True
        End If
        If Time23.Checked = True Then
            Time23.Enabled = False
        Else
            Time23.Enabled = True
        End If


    End Sub

    Private Sub refreshScreen()
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

            If koko_tempahan_insert() = False Then
                Exit Sub
            End If

            '--refresh page
            Response.Redirect(Request.RawUrl)

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

    Private Function koko_tempahan_insert() As Boolean
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

                '-kemaskini.pastikan tiada tindanan tempahan
                strSQL = "SELECT TempahanID FROM koko_tempahan WHERE KemudahanID=" & Request.QueryString("kemudahanid") & " AND BookingDate='" & oCommon.FixSingleQuotes(txtBookingDate.Text) & "'"
                If oCommon.isExist(strSQL) Then
                    '--update koko_tempahan
                    strSQL = "UPDATE koko_tempahan SET Time07='" & Time07.Checked & "',Time08='" & Time08.Checked & "',Time09='" & Time09.Checked & "',Time10='" & Time10.Checked & "',Time11='" & Time11.Checked & "',Time12='" & Time12.Checked & "',Time13='" & Time13.Checked & "',Time14='" & Time14.Checked & "',Time15='" & Time15.Checked & "',Time16='" & Time16.Checked & "',Time17='" & Time17.Checked & "',Time18='" & Time18.Checked & "',Time19='" & Time19.Checked & "',Time20='" & Time20.Checked & "',Time21='" & Time21.Checked & "',Time22='" & Time22.Checked & "',Time23='" & Time23.Checked & "' WHERE KemudahanID=" & Request.QueryString("kemudahanid") & " AND BookingDate='" & oCommon.FixSingleQuotes(txtBookingDate.Text) & "'"
                Else
                    'insert into koko_tempahan
                    strSQL = "INSERT INTO koko_tempahan(KemudahanID,BookingDate,Time07,Time08,Time09,Time10,Time11,Time12,Time13,Time14,Time15,Time16,Time17,Time18,Time19,Time20,Time21,Time22,Time23) VALUES (" & Request.QueryString("kemudahanid") & ",'" & oCommon.FixSingleQuotes(txtBookingDate.Text) & "','" & Time07.Checked & "','" & Time08.Checked & "','" & Time09.Checked & "','" & Time10.Checked & "','" & Time11.Checked & "','" & Time12.Checked & "','" & Time13.Checked & "','" & Time14.Checked & "','" & Time15.Checked & "','" & Time16.Checked & "','" & Time17.Checked & "','" & Time18.Checked & "','" & Time19.Checked & "','" & Time20.Checked & "','" & Time21.Checked & "','" & Time22.Checked & "','" & Time23.Checked & "')"
                End If
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--maklumat penempah
                strSQL = "INSERT INTO koko_tempahandetail(KemudahanID,Pemohon,ContactNo,Catatan,BookingDate,Time07,Time08,Time09,Time10,Time11,Time12,Time13,Time14,Time15,Time16,Time17,Time18,Time19,Time20,Time21,Time22,Time23) VALUES (" & Request.QueryString("kemudahanid") & ",'" & oCommon.FixSingleQuotes(txtPemohon.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtContactNo.Text) & "','" & oCommon.FixSingleQuotes(txtCatatan.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtBookingDate.Text) & "','" & CheckBox07.Checked & "','" & CheckBox08.Checked & "','" & CheckBox09.Checked & "','" & CheckBox10.Checked & "','" & CheckBox11.Checked & "','" & CheckBox12.Checked & "','" & CheckBox13.Checked & "','" & CheckBox14.Checked & "','" & CheckBox15.Checked & "','" & CheckBox16.Checked & "','" & CheckBox17.Checked & "','" & CheckBox18.Checked & "','" & CheckBox19.Checked & "','" & CheckBox20.Checked & "','" & CheckBox21.Checked & "','" & CheckBox22.Checked & "','" & CheckBox23.Checked & "')"
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

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


End Class