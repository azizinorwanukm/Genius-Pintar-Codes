Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class pelajar_year_select
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
        '--student use MYKAD to login
        Dim strStudentID As String = ""
        strSQL = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & CType(Session.Item("koko_loginid"), String) & "'"
        strStudentID = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT StudentID FROM koko_pelajar WHERE StudentID='" & strStudentID & "' AND Tahun='" & ddlTahun.Text & "'"
        '--debug
        'Response.Write(strSQL)
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Profil anda tiada pada tahun :" & ddlTahun.Text
            Exit Sub
        End If

        'admin.koko.update.aspx?studentid=2c3cf181-d078-4366-b0eb-806981bd92b6&kokopelajarid=33
        Select Case Request.QueryString("set")
            Case "koko"
                Response.Redirect("pelajar.koko.update.aspx?studentid=" & strStudentID & "&tahun=" & ddlTahun.Text & "&std_ID=" & Request.QueryString("std_ID"))
            Case "pencapaian"
                Response.Redirect("pelajar.koko.pencapaian.update.aspx?studentid=" & strStudentID & "&tahun=" & ddlTahun.Text & "&std_ID=" & Request.QueryString("std_ID"))
            Case "profil"
                Response.Redirect("pelajar.profil.update.aspx?studentid=" & strStudentID & "&tahun=" & ddlTahun.Text & "&std_ID=" & Request.QueryString("std_ID"))
            Case "pwd"
                Response.Redirect("pelajar.password.update.aspx?studentid=" & strStudentID & "&tahun=" & ddlTahun.Text & "&std_ID=" & Request.QueryString("std_ID"))
            Case Else
                lblMsg.Text = "Maaf. Fungsi tidak ditemui."
        End Select
    End Sub

End Class