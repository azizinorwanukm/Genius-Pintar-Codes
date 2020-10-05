Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm_pusatujian_view1
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnExecute.Attributes.Add("onclick", "return confirm('Pasti ingin meneruskan fungsi tersebut?');")

        Try
            If Not IsPostBack Then
                '--load UKM2 menu base on usertype
                master_menu_list()
                ddlMenudesc.SelectedValue = "03"
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub master_menu_list()
        strSQL = "SELECT menudesc,menucode FROM master_menu WHERE menucategory='PusatUjian01' AND usertype='" & getUserProfile_UserType() & "' ORDER BY menucode"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMenudesc.DataSource = ds
            ddlMenudesc.DataTextField = "menudesc"
            ddlMenudesc.DataValueField = "menucode"
            ddlMenudesc.DataBind()

            '--ddlMenudesc.Items.Add(New ListItem("Select", "Select"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecute.Click
        Select Case ddlMenudesc.Text
            Case "00"
                lblMsg.Text = "Please select functions to execute!"

            Case "01"   'Batal Pusat Ujian
                Execute_01()

            Case "02"   'Assign Pelajar
                Execute_02()

            Case "03"   'Senarai Pelajar
                Execute_03()

            Case "04"   'Assign Petugas
                Execute_04()

            Case "05"   'Senarai Petugas
                Execute_05()

            Case Else
                lblMsg.Text = "Please select functions to execute!"
        End Select

    End Sub

    '--Batal Pusat Ujian
    Private Sub Execute_01()
        strSQL = "SELECT PusatCode FROM UKM2 WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Perhatian: Terdapat pelajar layak ke UKM2 ditempatkan di Pusat Ujian tersebut. Pilih [Senarai Pelajar] dan [Un-assign Pusat] dahulu."
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        strSQL = "DELETE PusatUjian WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya membatalkan sekolah tersebut sebagai Pusat Ujian."
            divMsg.Attributes("class") = "info"
        Else
            lblMsg.Text = "Error:" & strRet
            divMsg.Attributes("class") = "error"
        End If

    End Sub

    '--Assign Pelajar
    Private Sub Execute_02()
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.student.select.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.student.select.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "UKM"
                Response.Redirect("ukm.pusatujian.student.select.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))

            Case Else
                lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

    '--Senarai Pelajar
    Private Sub Execute_03()
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.student.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.student.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "UKM"
                Response.Redirect("ukm.pusatujian.student.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))

            Case Else
                lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

    '--Assign Petugas
    Private Sub Execute_04()
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.petugas.select.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.petugas.select.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "UKM"
                Response.Redirect("ukm.pusatujian.petugas.select.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))

            Case Else
                lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

    '--Senarai Petugas
    Private Sub Execute_05()
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.petugas.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.petugas.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "UKM"
                Response.Redirect("ukm.pusatujian.petugas.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))

            Case Else
                lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

End Class