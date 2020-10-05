Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class tempahan_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                lblTempahanID.Text = Request.QueryString("tempahanid")
                lblPrintDate.Text = oCommon.formatDateDay(Now)

                '--person info
                koko_tempahandetail_load()

                refreshScreen()
                check_tempahandetail()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_tempahandetail_load()
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderBy As String = ""

        tmpSQL = "SELECT * FROM koko_tempahandetail a, koko_kemudahan b WHERE a.KemudahanID=b.KemudahanID"
        strWhere = " AND a.TempahanID=" & Request.QueryString("tempahanid")
        strSQL = tmpSQL & strWhere & strOrderBy
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
                '--koko_kemudahan
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

                '--koko_tempahan
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("BookingDate")) Then
                    lblBookingDate.Text = ds.Tables(0).Rows(0).Item("BookingDate")
                Else
                    lblBookingDate.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pemohon")) Then
                    lblPemohon.Text = ds.Tables(0).Rows(0).Item("Pemohon")
                Else
                    lblPemohon.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContactNo")) Then
                    lblContactNo.Text = ds.Tables(0).Rows(0).Item("ContactNo")
                Else
                    lblContactNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Catatan")) Then
                    lblCatatan.Text = ds.Tables(0).Rows(0).Item("Catatan")
                Else
                    lblCatatan.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StatusTempahan")) Then
                    lblStatusTempahan.Text = ds.Tables(0).Rows(0).Item("StatusTempahan")
                Else
                    lblStatusTempahan.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CatatanPengarah")) Then
                    lblCatatanPengarah.Text = ds.Tables(0).Rows(0).Item("CatatanPengarah")
                Else
                    lblCatatanPengarah.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub refreshScreen()
        Time07.Enabled = False
        Time08.Enabled = False
        Time09.Enabled = False
        Time10.Enabled = False
        Time11.Enabled = False
        Time12.Enabled = False
        Time13.Enabled = False
        Time14.Enabled = False
        Time15.Enabled = False
        Time16.Enabled = False
        Time17.Enabled = False
        Time18.Enabled = False
        Time19.Enabled = False
        Time20.Enabled = False
        Time21.Enabled = False
        Time22.Enabled = False
        Time23.Enabled = False


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

    Private Sub check_tempahandetail()
        Dim strQuery As String = "SELECT * FROM koko_tempahandetail WHERE TempahanID=" & lblTempahanID.Text

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
                    setCheckBox(columnName, columnStringValue)
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