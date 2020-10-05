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
            If Not IsPostBack Then

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
                        Dim userAccess As String = ""
                        userAccess = "select stf_ID from staff_Info where stf_ID = '" & staffID_Data & "' and staff_Status = 'Access'"
                        Dim access As String = getFieldValue(userAccess, strConn)

                        ''check whether lecturer is a homeroom teacher
                        Dim homeroom As String = "select distinct stf_ID from class_info where stf_ID = '" & staffID_Data & "' and class_year= '" & Now.Year & "'"
                        Dim accessHomeroom As String = oCommon.getFieldValue(homeroom)

                        ''check whether lecturer is a coordinator teacher
                        Dim coordinator As String = "select distinct stf_ID from coordinator where stf_ID = '" & staffID_Data & "' and year= '" & Now.Year & "'"
                        Dim accessCoordinator As String = oCommon.getFieldValue(coordinator)

                        ''check whether lecturer is a counselor
                        Dim counselor As String = "select distinct stf_ID from counseling_info where kslr_year = '" & Now.Year & "' and stf_ID = '" & staffID_Data & "'"
                        Dim accessCounselor As String = oCommon.getFieldValue(counselor)

                        If accessHomeroom = "" Then
                            hiddenAccess.Value = "NOT ACCESS"
                        Else
                            hiddenAccess.Value = "ACCESS"
                        End If

                        If accessCoordinator = "" Then
                            hiddenKoordinator.Value = "NOT ACCESS"
                        Else
                            hiddenKoordinator.Value = "ACCESS"
                        End If

                        If accessCounselor = "" Then
                            hiddenKaunselor.Value = "NOT ACCESS"
                        Else
                            hiddenKaunselor.Value = "ACCESS"
                        End If

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
        Home.NavigateUrl = String.Format("pengajar_login_berjaya.aspx?stf_ID=" + id)
        pengajarPelajarKehadiran.NavigateUrl = String.Format("pengajar_kehadiran_pelajar.aspx?stf_ID=" + id)
        pengajarKemaskiniMarkah.NavigateUrl = String.Format("pengajar_kemasukan_markah.aspx?stf_ID=" + id)
        pengajarSenaraiKaunselorPelajar.NavigateUrl = String.Format("pengajar_carian_pelajar_kaunselor.aspx?stf_ID=" + id)
        pengajarKemaskiniProfil.NavigateUrl = String.Format("pengajar_kemaskini_profile.aspx?stf_ID=" + id)
        pengajarTukarKataLaluan.NavigateUrl = String.Format("pengajar_password_baru.aspx?stf_ID=" + id)
        pengajarLogout.NavigateUrl = String.Format("default.aspx?result=89&stf_ID=" + id)
        pengajarCarianPelajar.NavigateUrl = String.Format("pengajar_senarai_pelajar.aspx?stf_ID=" + id)
        pengajarLaporanPeperiksaan.NavigateUrl = String.Format("pengajar_laporan_pentaksiran.aspx?stf_ID=" + id)
        homeroomSemakKehadiran.NavigateUrl = String.Format("homeroom_semak_kehadiran.aspx?stf_ID=" + id)
        homeroomPenilaianPelajar.NavigateUrl = String.Format("pengajar_penilaian_pelajar.aspx?stf_ID=" + id)
        koordinatorKemaskiniMarkah.NavigateUrl = String.Format("pegajar_koordinator_kemaskini_markah.aspx?stf_ID=" + id)
        koordinatorPenilaianPelajar.NavigateUrl = String.Format("pengajar_koordinator_penilaian_pelajar.aspx?stf_ID=" + id)
    End Sub

End Class