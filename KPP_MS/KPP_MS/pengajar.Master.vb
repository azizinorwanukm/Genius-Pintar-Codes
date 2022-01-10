Imports System.Data.SqlClient

Public Class pengajar
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

            If Not IsPostBack Or IsPostBack Then

                Dim id As String = Request.QueryString("stf_ID")

                hiddenData.Value = id

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then
                    Dim staffID_Data As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

                    If staffID_Data = "NULL" Then
                        Response.Redirect("default.aspx")

                    Else

                        ''Hide Certain Menu When User APP Login
                        If Session("SchoolCampus") = "APP" Then
                            pengajarPelajarKehadiran.Visible = False
                            homeroomSemakKehadiran.Visible = False
                            ninenine.Visible = False
                        End If

                        hiddenAccess.Value = "ACCESS"
                        hiddenKoordinator.Value = "ACCESS"
                        hiddenKaunselor.Value = "ACCESS"

                        loading_Page(id)

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

    Private Sub loading_Page(ByVal id As String)

        Home.NavigateUrl = String.Format("pengajar_login_berjaya.aspx?stf_ID=" + id + "&status=SI")
        pengajarDaftarKelas.NavigateUrl = String.Format(("pengajar_daftar_kelas.aspx?stf_ID=" + id))
        pengajarPelajarKehadiran.NavigateUrl = String.Format("pengajar_kehadiran_pelajar.aspx?stf_ID=" + id)
        pengajarKemaskiniMarkah.NavigateUrl = String.Format("pengajar_kemasukan_markah.aspx?stf_ID=" + id)
        pengajarKemasukanPelajar.NavigateUrl = String.Format("penagajr_kemasukan_pelajar.aspx?stf_ID=" + id + "&status=SR")
        'pengajarSenaraiKaunselorPelajar.NavigateUrl = String.Format("pengajar_carian_pelajar_kaunselor.aspx?stf_ID=" + id)

        pengajarCarianPelajar.NavigateUrl = String.Format("pengajar_senarai_pelajar.aspx?stf_ID=" + id)
        pengajarLaporanPeperiksaan.NavigateUrl = String.Format("pengajar_laporan_pentaksiran.aspx?stf_ID=" + id)
        homeroomSemakKehadiran.NavigateUrl = String.Format("homeroom_semak_kehadiran.aspx?stf_ID=" + id)
        'homeroomPenilaianPelajar.NavigateUrl = String.Format("pengajar_penilaian_pelajar.aspx?stf_ID=" + id)
        koordinatorKemaskiniMarkah.NavigateUrl = String.Format("pegajar_koordinator_kemaskini_markah.aspx?stf_ID=" + id)
        'koordinatorPenilaianPelajar.NavigateUrl = String.Format("pengajar_koordinator_penilaian_pelajar.aspx?stf_ID=" + id)

        kokocarianpelajar.NavigateUrl = String.Format("pengajar_koko_carianPelajar.aspx?stf_ID=" + id)
        kokopangkatpelajar.NavigateUrl = String.Format("pengajar_koko_pangkatPelajar.aspx?stf_ID=" + id)
        kokokehadiranpelajar.NavigateUrl = String.Format("pengajar_koko_kehadiranPelajar.aspx?stf_ID=" + id)
        kokomarkahpelajar.NavigateUrl = String.Format("pengajar_koko_markahPelajar.aspx?stf_ID=" + id)
        kokojadualpelajar.NavigateUrl = String.Format("pengajar_koko_jadualPelajar.aspx?stf_ID=" + id)

        LinkTutorial.NavigateUrl = String.Format("pengajar_tutorial.aspx?stf_ID=" + id)

        Dim staffID_Data As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        If staffID_Data = "1654" Then
            pengajarKemasukanPelajar.Visible = True
        Else
            pengajarKemasukanPelajar.Visible = False
        End If


        strSQL = "  select staff_Name from staff_Info
                    where staff_Status = 'Access'
                    and stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"


        Dim strSQLPosition As String = "Select Parameter from setting where Value = '" & Session("lecturer_position") & "'"

        txtstaffName.Text = " [ WELCOME ,  &nbsp;&nbsp; " & oCommon.getFieldValue(strSQL) & " &nbsp;&nbsp; - &nbsp;&nbsp; " & oCommon.getFieldValue(strSQLPosition).ToUpper & " ] "

        txtcurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy")

    End Sub

    Private Sub btnLogout_ServerClick(sender As Object, e As EventArgs) Handles btnLogout.ServerClick
        Response.Redirect("default.aspx?result=89&stf_ID=" + oCommon.Staff_securityLogin(Request.QueryString("stf_ID")))
    End Sub


End Class