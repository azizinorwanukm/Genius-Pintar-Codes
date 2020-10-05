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

                koko_tahun_list()
                ddlTahun.Text = Request.QueryString("tahun")

                koko_list()
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

    Private Sub kumpulan_list()

        Dim find_ismandatory As String = "select IsMandatory from koko_kolejpermata where KokoID = '" & ddlKokoID.SelectedValue & "'"
        Dim get_ismandatory As String = oCommon.getFieldValue(find_ismandatory)

        If get_ismandatory = "N" Then
            ddlKumpulanID.Visible = False
        End If

        Dim find_instruktorid As String = "select kokoinstruktorid from koko_instruktor where InstruktorID = '" & Request.QueryString("instruktorid") & "' and Tahun = '" & ddlTahun.SelectedValue & "'"
        Dim get_instruktorid As String = oCommon.getFieldValue(find_instruktorid)

        strSQL = "SELECT Kelas, KelasKokoID FROM koko_kelaskoko WHERE kokoinstruktorid='" & get_instruktorid & "' AND KokoID = '" & ddlKokoID.SelectedValue & "'ORDER BY Kelas ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKumpulanID.DataSource = ds
            ddlKumpulanID.DataTextField = "Kelas"
            ddlKumpulanID.DataValueField = "KelasKokoID"
            ddlKumpulanID.DataBind()

            ddlKumpulanID.Items.Add(New ListItem("--Pilih--", ""))

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

    Private Sub koko_list()

        Dim find_instruktorid As String = "select kokoinstruktorid from koko_instruktor where InstruktorID = '" & Request.QueryString("instruktorid") & "' and Tahun = '" & ddlTahun.SelectedValue & "'"
        Dim get_instruktorid As String = oCommon.getFieldValue(find_instruktorid)

        strSQL = "  select koko_kolejpermata.KokoID, koko_kolejpermata.Nama from koko_kolejpermata
                    left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.SukanID
                    where koko_kolejpermata.Tahun = '" & ddlTahun.SelectedValue & "'
                    and KetuaSukan = 'True'
                    and koko_instruktor.kokoinstruktorid = '" & get_instruktorid & "'
                    Union
                    select koko_kolejpermata.KokoID, koko_kolejpermata.Nama from koko_kolejpermata
                    left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.PersatuanID
                    where koko_kolejpermata.Tahun = '" & ddlTahun.SelectedValue & "'
                    and KetuaPersatuan = 'True'
                    and koko_instruktor.kokoinstruktorid = '" & get_instruktorid & "'
                    Union
                    select koko_kolejpermata.KokoID, koko_kolejpermata.Nama from koko_kolejpermata
                    left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.KetuaUniform
                    where koko_kolejpermata.Tahun = '" & ddlTahun.SelectedValue & "'
                    and KetuaUniform = 'True'
                    and koko_instruktor.kokoinstruktorid = '" & get_instruktorid & "'
                    Union
                    select koko_kolejpermata.KokoID, koko_kolejpermata.Nama from koko_kolejpermata
                    left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.RumahsukanID
                    where koko_kolejpermata.Tahun = '" & ddlTahun.SelectedValue & "'
                    and KetuaRumahsukan = 'True'
                    and koko_instruktor.kokoinstruktorid = '" & get_instruktorid & "'
                    order by koko_kolejpermata.Nama asc"

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
            strSQL = "INSERT INTO koko_event(Tahun,InstruktorID,EventDate,Title,KokoID,Agenda) VALUES ('" & Request.QueryString("tahun") & "','" & Request.QueryString("instruktorid") & "','" & lblEventDate.Text & "','" & oCommon.FixSingleQuotes(txtTitle.Text.ToUpper) & "','" & ddlKokoID.SelectedValue & "','" & oCommon.FixSingleQuotes(txtAgenda.Text) & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Penambahan berjaya!"

                'Dim strNewEventID = getNewEventID()

                'For i = 0 To ddlKumpulan.Items.Count - 1 Step i + 1
                '    If ddlKumpulan.Items(i).Selected = True Then
                '        strSQL = "INSERT INTO koko_kumpulan(KelasKokoID, EventID) VALUES ('" & ddlKumpulan.Items(i).Value.ToString & "', '" & strNewEventID & "')"
                '        strRet = oCommon.ExecuteSQL(strSQL)
                '    End If
                'Next

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
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.event.list.aspx?instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun"))
            Case "INSTRUKTOR"
                Response.Redirect("instruktor.event.list.aspx?instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun"))
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