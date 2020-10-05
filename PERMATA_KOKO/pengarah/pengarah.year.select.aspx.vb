Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class pengarah_year_select
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_tahun_list()
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

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

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Dim strPensyarahID As String = ""
        strSQL = "SELECT PengarahID FROM koko_pengarah WHERE LoginID='" & CType(Session.Item("koko_loginid"), String) & "'"
        strPensyarahID = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT PengarahID FROM koko_pengarah WHERE LoginID='" & CType(Session.Item("koko_loginid"), String) & "' AND Tahun='" & ddlTahun.Text & "'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Profil anda tiada pada tahun :" & ddlTahun.Text
            Exit Sub
        End If

        Select Case Request.QueryString("set")
            Case "approval"
                Response.Redirect("pengarah.tempahan.view.aspx?tempahanid=" & strPensyarahID & "&tahun=" & ddlTahun.Text & "&pengarah_ID=" & Request.QueryString("pengarah_ID"))
            Case "profil"
                Response.Redirect("pengarah.profil.update.aspx?pengarahid=" & strPensyarahID & "&tahun=" & ddlTahun.Text & "&pengarah_ID=" & Request.QueryString("pengarah_ID"))
            Case "pwd"
                Response.Redirect("pengarah.password.update.aspx?pengarahid=" & strPensyarahID & "&tahun=" & ddlTahun.Text & "&pengarah_ID=" & Request.QueryString("pengarah_ID"))
            Case Else
                lblMsg.Text = "Maaf. Fungsi tidak ditemui."
        End Select
    End Sub

End Class