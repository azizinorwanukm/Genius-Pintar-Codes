Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class event_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                koko_tahun_list()
                koko_list("ALL")

                koko_event_load()
            End If

        Catch ex As Exception

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

    Private Sub koko_list(ByVal strJenis As String)
        If Not strJenis = "ALL" Then
            strSQL = "SELECT KokoID,Nama FROM koko_kolejpermata WHERE Tahun='" & Request.QueryString("tahun") & "' AND Jenis='" & strJenis & "' ORDER BY Nama ASC"
        Else
            strSQL = "SELECT KokoID,Nama FROM koko_kolejpermata WHERE Tahun='" & Request.QueryString("tahun") & "' AND Jenis<>'RUMAHSUKAN' ORDER BY Nama ASC"
        End If
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKokurikulum.DataSource = ds
            ddlKokurikulum.DataTextField = "Nama"
            ddlKokurikulum.DataValueField = "KokoID"
            ddlKokurikulum.DataBind()

            ddlKokurikulum.Items.Add(New ListItem("--Pilih--", ""))
        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_event_load()
        strSQL = "SELECT koko_event.Tahun,koko_event.EventDate,koko_event.KokoID,koko_event.Title,koko_event.Agenda FROM koko_event LEFT OUTER JOIN koko_kolejpermata ON koko_event.KokoID=koko_kolejpermata.KokoID WHERE EventID=" & Request.QueryString("eventid")
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
                    ddlTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlTahun.Text = ""
                End If

                Dim strEventDate As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EventDate")) Then
                    strEventDate = ds.Tables(0).Rows(0).Item("EventDate")
                    calEventDate.SelectedDate = CDate(strEventDate) '--set selected date
                    calEventDate.VisibleDate = CDate(strEventDate) '--display selected date

                    txtEventDate.Text = oCommon.DateDisplay(strEventDate)
                    lblEventDate.Text = oCommon.DateSaved(strEventDate)
                Else
                    strEventDate = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KokoID")) Then
                    ddlKokurikulum.SelectedValue = ds.Tables(0).Rows(0).Item("KokoID")
                Else
                    ddlKokurikulum.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Title")) Then
                    txtTitle.Text = ds.Tables(0).Rows(0).Item("Title")
                Else
                    txtTitle.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Agenda")) Then
                    txtAgenda.Text = ds.Tables(0).Rows(0).Item("Agenda")
                Else
                    txtAgenda.Text = ""
                End If

            End If

        Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub


    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'check form validation. if failed exit
        If ValidateForm() = False Then
            Exit Sub
        End If

        'UPDATE
        strSQL = "UPDATE koko_event SET Tahun='" & ddlTahun.Text & "',EventDate='" & oCommon.FixSingleQuotes(lblEventDate.Text) & "',Title='" & oCommon.FixSingleQuotes(txtTitle.Text.ToUpper) & "',KokoID='" & oCommon.FixSingleQuotes(ddlKokurikulum.Text) & "',Agenda='" & oCommon.FixSingleQuotes(txtAgenda.Text) & "' WHERE EventID=" & Request.QueryString("eventid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Kemaskini berjaya!"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean
        If txtEventDate.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtEventDate.Focus()
            Return False
        End If

        If txtTitle.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtTitle.Focus()
            Return False
        End If


        Return True
    End Function

    Private Sub calEventDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEventDate.SelectionChanged
        txtEventDate.Text = oCommon.DateDisplay(calEventDate.SelectedDate)
        lblEventDate.Text = oCommon.DateSaved(calEventDate.SelectedDate)

        calEventDate.Visible = False

    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.event.list.aspx?instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&admin_ID=" & Request.QueryString("admin_ID"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.event.list.aspx?instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&admin_ID=" & Request.QueryString("admin_ID"))
            Case "PENSYARAH"
            Case "PENGARAH"
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Private Sub btnDate_Click(sender As Object, e As ImageClickEventArgs) Handles btnDate.Click
        Dim [date] As New DateTime()
        'Flip the visibility attribute
        calEventDate.Visible = Not (calEventDate.Visible)
        'If the calendar is visible try assigning the date from the textbox
        If calEventDate.Visible Then
            'If the Conversion was successfull assign the textbox's date
            If DateTime.TryParse(txtEventDate.Text, [date]) Then
                calEventDate.SelectedDate = [date]
            End If
            calEventDate.Attributes.Add("style", "POSITION: absolute")
        End If

    End Sub

End Class