Imports System.Data.SqlClient

Public Class Site1
    Inherits System.Web.UI.MasterPage

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Request.IsSecureConnection Then
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"))
            End If

            If Not IsPostBack Then

                Dim id As String = Request.QueryString("pengarah_ID")

                hiddenData.Value = id

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then
                    Dim staffID_Data As String = oCommon.Staff_securityLogin(Request.QueryString("pengarah_ID"))

                    If staffID_Data = "NULL" Then
                        Response.Redirect("default.aspx")

                    Else

                        loading_Page()

                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String
        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Sub loading_Page()

        Dim running_ID As String = Request.QueryString("pengarah_ID")

        Home.NavigateUrl = String.Format("pengarah_login_berjaya.aspx?pengarah_ID=" + running_ID)

        pengarahPeperiksaanKelas.NavigateUrl = String.Format("pengarah_laporan_peperiksaan_kelas.aspx?pengarah_ID=" + running_ID)
        pengarahPeperiksaanKursus.NavigateUrl = String.Format("pengarah_laporan_kursus.aspx?pengarah_ID=" + running_ID)
        pengarahLaporanPeperiksaan.NavigateUrl = String.Format("pengarah_laporan_peperiksaan.aspx?pengarah_ID=" + running_ID)

        pengarahLaporanKehadiran.NavigateUrl = String.Format("pengarah_laporan_kehadiran.aspx?pengarah_ID=" + running_ID)
        pengarahSlipPeperiksaan.NavigateUrl = String.Format("pengarah_slip_peperiksaan.aspx?pengarah_ID=" + running_ID)

        pengarahPengurusanKokurikulum.NavigateUrl = String.Format("http://koko.permatapintar.edu.my/pengarah/pengarah.login.succcess.aspx?pengarah_ID=" + Request.QueryString("pengarah_ID"))
        'pengarahPengurusanKokurikulum.NavigateUrl = String.Format("http://localhost/permata_koko/pengarah/pengarah.login.succcess.aspx?pengarah_ID=" + Request.QueryString("pengarah_ID"))

        strSQL = "  select staff_Name from staff_info
                    where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("pengarah_ID")) & "'"

        Dim strSQLPosition As String = "Select Parameter from setting where Value = 'Pengarah'"

        txtstaffName.Text = " [ WELCOME ,  &nbsp;&nbsp; " & oCommon.getFieldValue(strSQL) & " &nbsp;&nbsp; - &nbsp;&nbsp; " & oCommon.getFieldValue(strSQLPosition).ToUpper & " ] "

        txtcurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub btnLogout_ServerClick(sender As Object, e As EventArgs) Handles btnLogout.ServerClick

        Response.Redirect("pengarah_CloseLogout.aspx?pengarah_ID=" + Request.QueryString("pengarah_ID"))

    End Sub

End Class