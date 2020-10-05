
Partial Public Class _default1
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                setDisplay()
                getTitle()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub getTitle()
        lblUKM2Title.Text = oCommon.getAppsettings("UKM2Title")
        lblPPCSTitle.Text = oCommon.getAppsettings("PPCSTitle")
        lblASASITitle.Text = oCommon.getAppsettings("ASASITitle")
        lblPPMTTitle.Text = oCommon.getAppsettings("PPMTTitle")
        lblKOLEJTitle.Text = oCommon.getAppsettings("KOLEJTitle")
    End Sub

    Private Sub setDisplay()

        '--SEMAK_STOPALL
        If oCommon.getAppsettings("SEMAK_STOPALL") = "Y" Then
            lblSemak.Text = "LAMAN SEMAK INI DITUTUP BUAT SEMENTARA WAKTU."

            pnlUKM2.Visible = False
            pnlPPCS.Visible = False
            pnlASASI.Visible = False
            pnlPPMT.Visible = False
            pnlKolej.Visible = False
            Exit Sub
        Else
            lblSemak.Text = "SILA PILIH SEMAKAN YANG INGIN DIBUAT:"
        End If

        '--SEMAK_UKM2
        If oCommon.getAppsettings("SEMAK_UKM2") = "Y" Then
            pnlUKM2.Visible = True
        Else
            pnlUKM2.Visible = False
        End If

        '--SEMAK_PPCS
        If oCommon.getAppsettings("SEMAK_PPCS") = "Y" Then
            pnlPPCS.Visible = True
        Else
            pnlPPCS.Visible = False
        End If

        '--SEMAK_ASASI
        If oCommon.getAppsettings("SEMAK_ASASI") = "Y" Then
            pnlASASI.Visible = True
        Else
            pnlASASI.Visible = False
        End If

        '--PPMTResult
        If oCommon.getAppsettings("SEMAK_PPMT") = "Y" Then
            pnlPPMT.Visible = True
        Else
            pnlPPMT.Visible = False
        End If

        '--KOLEJResult
        If oCommon.getAppsettings("KOLEJResult") = "Y" Then
            pnlKolej.Visible = True
        Else
            pnlKolej.Visible = False
        End If

    End Sub

    Private Sub lnkPPCS_Click(sender As Object, e As EventArgs) Handles lnkPPCSTitle.Click
        Response.Redirect("ppcs.semak.aspx")

    End Sub

    Private Sub lnkPPMT_Click(sender As Object, e As EventArgs) Handles lnkPPMTTitle.Click
        Response.Redirect("ppmt.semak.aspx")

    End Sub

    Private Sub lnkUKM2_Click(sender As Object, e As EventArgs) Handles lnkUKM2Title.Click
        Response.Redirect("ukm2.semak.aspx")

    End Sub

    Private Sub lnkASASI_Click(sender As Object, e As EventArgs) Handles lnkASASITitle.Click
        Response.Redirect("asasi.semak.aspx")
    End Sub

    Private Sub lnkKOLEJ_Click(sender As Object, e As EventArgs) Handles lnkKOLEJitle.Click
        Response.Redirect("kolej_semak.aspx")
    End Sub

End Class