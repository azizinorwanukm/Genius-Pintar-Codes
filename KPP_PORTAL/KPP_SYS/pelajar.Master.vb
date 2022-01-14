Imports System.Data.SqlClient

Public Class pelajar
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

                Dim id As String = Request.QueryString("std_ID")

                hiddenData.Value = id

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then
                    loading_Page()

                End If

                If Session("Student_Campus") = "PGPN" Then
                    Main_Logo_PGPN.Visible = True
                    Main_Logo_APP.Visible = False

                ElseIf Session("Student_Campus") = "APP" Then
                    Main_Logo_PGPN.Visible = False
                    Main_Logo_APP.Visible = True
                End If

                Check_STDStatus()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Check_STDStatus()

        Dim check_Stream As String = "Select student_Stream from student_info where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and student_Status = 'Access'"
        Dim get_Stream As String = oCommon.getFieldValue(check_Stream)

        Dim check_Campus As String = "Select student_Campus from student_info where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and student_Status = 'Access'"
        Dim get_Campus As String = oCommon.getFieldValue(check_Campus)

        If (get_Stream = "PS" Or get_Stream = "DIP") And get_Campus = "PGPN" Then
            MenuClass.Style.Add("display", "block")
            MenuKokurikulum.Style.Add("display", "block")
            MenuAttendance.Style.Add("display", "block")
            MenuReference.Style.Add("display", "block")
            MenuCounseling.Style.Add("display", "block")
            'MenuTutorial.Style.Add("display", "block")

        ElseIf (get_Stream = "PS" Or get_Stream = "DIP") And get_Campus = "APP" Then
            MenuClass.Style.Add("display", "block")
            MenuKokurikulum.Style.Add("display", "block")
            'MenuTutorial.Style.Add("display", "block")

        ElseIf get_Stream = "Temp" Then
            MenuReference.Style.Add("display", "block")
            'MenuTutorial.Style.Add("display", "block")
        End If
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

        Home.NavigateUrl = String.Format("pelajar_login_berjaya.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=SI")
        pelajarLK.NavigateUrl = String.Format("pelajar_laporan_kehadiran.aspx?std_ID=" + Request.QueryString("std_ID"))
        pelajarPK.NavigateUrl = String.Format("pelajar_pilih_kursus.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=VCC")
        pelajarKOKO.NavigateUrl = String.Format("pelajar_pilih_koko.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=VKOKO")
        pelajarSY.NavigateUrl = String.Format("pelajar_muat_turun_dokumen.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=DR")
        pelajarKA.NavigateUrl = String.Format("pelajar_kaunseling.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=VCO")
        'pelajarTuto.NavigateUrl = String.Format("pelajar_Tutorial.aspx?std_ID=" + Request.QueryString("std_ID"))

        strSQL = "  select student_Name from student_info
                    where student_Status = 'Access'
                    and std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        txtstudentName.Text = " [ WELCOME , &nbsp;&nbsp; " & oCommon.getFieldValue(strSQL) & " ] "

        txtcurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy")

    End Sub

    Private Sub btnLogout_ServerClick(sender As Object, e As EventArgs) Handles btnLogout.ServerClick

        Response.Redirect("pelajar_CloseLogout.aspx?std_ID=" + Request.QueryString("std_ID"))

    End Sub
End Class