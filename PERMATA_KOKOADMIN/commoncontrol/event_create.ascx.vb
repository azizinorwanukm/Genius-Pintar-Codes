Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class event_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--refresh
                ClearScreen()
                lblTahun.Text = Request.QueryString("tahun")

                koko_list("ALL")
                kumpulan_list()
                ddlKokoID.SelectedValue = ""

                '--set default date
                calEventDate.SelectedDate = Now.Date
                txtEventDate.Text = oCommon.DateDisplay(calEventDate.SelectedDate)
                lblEventDate.Text = oCommon.DateSaved(calEventDate.SelectedDate)

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtEventDate.Text = ""
        txtTitle.Text = ""
        txtAgenda.Text = ""


    End Sub

    Private Sub kumpulan_list()

        Dim strKokoInstruktorID As String = getKokoInstruktorID(Request.QueryString("instruktorid"))

        strSQL = "SELECT Kelas, KelasKokoID FROM koko_kelaskoko WHERE kokoinstruktorid='" & strKokoInstruktorID & "' AND KokoID = '" & ddlKokoID.SelectedValue & "'ORDER BY Kelas ASC"

        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKumpulan.DataSource = ds
            ddlKumpulan.DataTextField = "Kelas"
            ddlKumpulan.DataValueField = "KelasKokoID"
            ddlKumpulan.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function getKokoInstruktorID(ByVal instruktorid As String) As String
        strSQL = "SELECT kokoinstruktorid FROM koko_instruktor WHERE instruktorid = '" & instruktorid & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

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

            ddlKokoID.DataSource = ds
            ddlKokoID.DataTextField = "Nama"
            ddlKokoID.DataValueField = "KokoID"
            ddlKokoID.DataBind()

            ddlKokoID.Items.Add(New ListItem("--Pilih--", ""))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click

        Dim i As Integer

        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "INSERT INTO koko_event(Tahun,InstruktorID,EventDate,Title,KokoID,Agenda) VALUES ('" & lblTahun.Text & "','" & Request.QueryString("instruktorid") & "','" & lblEventDate.Text & "','" & oCommon.FixSingleQuotes(txtTitle.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(ddlKokoID.Text) & "','" & oCommon.FixSingleQuotes(txtAgenda.Text) & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Penambahan berjaya!"

                Dim strNewEventID = getNewEventID()

                For i = 0 To ddlKumpulan.Items.Count - 1 Step i + 1
                    If ddlKumpulan.Items(i).Selected = True Then
                        strSQL = "INSERT INTO koko_kumpulan(KelasKokoID, EventID) VALUES ('" & ddlKumpulan.Items(i).Value.ToString & "', '" & strNewEventID & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                Next

            Else
                lblMsg.Text = "Gagal. " & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message
        End Try

    End Sub

    '--get newest eventid
    Private Function getNewEventID() As String
        strSQL = "SELECT MAX(EventID) FROM koko_event"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean
        If txtEventDate.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtEventDate.Focus()
            Return False
        End If

        If ddlKokoID.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih kokurikulum."
            ddlKokoID.Focus()
            Return False
        End If

        If txtTitle.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtTitle.Focus()
            Return False
        End If


        Return True
    End Function

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

    Private Sub calEventDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEventDate.SelectionChanged
        txtEventDate.Text = oCommon.DateDisplay(calEventDate.SelectedDate)
        lblEventDate.Text = oCommon.DateSaved(calEventDate.SelectedDate)

        calEventDate.Visible = False
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

    Protected Sub ddlKokoID_SelectedIndexChanged(sender As Object, e As EventArgs)
        kumpulan_list()
    End Sub

End Class