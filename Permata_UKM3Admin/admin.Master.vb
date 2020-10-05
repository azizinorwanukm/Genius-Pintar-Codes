Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Partial Public Class admin
    Inherits System.Web.UI.MasterPage

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                '--debug
                Dim strpermata_admin As String = CType(Session.Item("permata_admin"), String)
                If strpermata_admin = "" Then
                    Session("permata_admin") = "uppb"
                    'Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again.")
                End If

                UserProfile_load()

                If lblLoginID.Text.Length = 0 Then
                    Session("permata_admin") = "uppb"
                    Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again.")
                End If

                getuser_Login()
            End If

        Catch ex As Exception
            lblFooterMsg.Text = "System error. Please contact admin. Err:" & ex.Message

        End Try
    End Sub


    Private Sub UserProfile_load()
        '--jj strSQL = "SELECT LoginID,Fullname,UserType FROM UserProfile WHERE loginid='" & CType(Session.Item("permata_admin"), String) & "'"
        'strSQL = "SELECT LoginID,Fullname,UserType FROM UserProfile WHERE loginid='" & CType(Session.Item("permata_admin"), String) & "'"

        strSQL = "select staff_name,staff_login,staff_position from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        Dim strSQL1 As String = "select staff_name,staff_login,staff_position from staff_info where staff_login = '" & CType(Session.Item("permata_ppcs"), String) & "'"
        Dim strSQL2 As String = "select staff_name,staff_login,staff_position from staff_info where staff_login = '" & CType(Session.Item("permata_adminE"), String) & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim sqlDA1 As New SqlDataAdapter(strSQL1, objConn)
        Dim sqlDA2 As New SqlDataAdapter(strSQL2, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            sqlDA1.Fill(ds, "AnyTable")
            sqlDA2.Fill(ds, "AnyTable")


            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_name")) Then
                    lblFullname.Text = ds.Tables(0).Rows(0).Item("staff_name")
                Else
                    lblFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_login")) Then
                    lblLoginID.Text = ds.Tables(0).Rows(0).Item("staff_login")
                Else
                    lblLoginID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_position")) Then
                    lblUserType.Text = ds.Tables(0).Rows(0).Item("staff_position")
                Else
                    lblUserType.Text = ""
                End If

            End If
        Catch ex As Exception
            '--
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub getuser_Login()


        strSQL = "select top 1 staff_position from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        Dim strSQL1 As String = "SELECT top 1 staff_position FROM staff_info A WHERE staff_login='" & CType(Session.Item("permata_ppcs"), String) & "'"
        Dim strSQL2 As String = "select top 1 staff_position from staff_info where staff_login = '" & CType(Session.Item("permata_adminE"), String) & "'"
        Debug.WriteLine(strSQL)
        Debug.WriteLine(strSQL1)
        If oCommon.isExist(strSQL) = True Then

            strRet = oCommon.getFieldValue(strSQL)
            ''Debug.WriteLine(strRet)
            If (strRet = "Admin" Or strRet = "ADMIN") Then
                ''SA
                admin_petugas.Visible = True
                ädmin_pelajar.Visible = True
                admin_markah.Visible = True
                admin_config.Visible = True
                admin_tawaran.Visible = True
                kelayakan_kolej.Visible = True
                'admin_laporan.Visible = True

                ''RA PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor Ra PPCS" OrElse strRet = "UKM3 - INSTRUKTOR Ra PPCS" OrElse strRet = "Ra PPCS") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                'admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = True

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor KPP" OrElse strRet = "UKM3 - INSTRUKTOR KPP") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                'admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = True

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor PPCS" OrElse strRet = "UKM3 - INSTRUKTOR PPCS") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                'admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = True

            Else
                Response.Redirect("default.aspx")

            End If

        ElseIf oCommon.isExist(strSQL1) = True Then

            strRet = oCommon.getFieldValue(strSQL1)
            ''Debug.WriteLine(strRet)

            If (strRet = "Admin" OrElse strRet = "ADMIN") Then
                ''SA
                admin_petugas.Visible = True
                ädmin_pelajar.Visible = True

                admin_markah.Visible = True
                admin_config.Visible = True
                admin_tawaran.Visible = True
                kelayakan_kolej.Visible = True
                'admin_laporan.Visible = True


                ''RA PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor Ra PPCS" OrElse strRet = "UKM3 - INSTRUKTOR Ra PPCS" OrElse strRet = "Ra PPCS") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                'admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = True

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor KPP" OrElse strRet = "UKM3 - INSTRUKTOR KPP") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                'admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = True

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor PPCS" OrElse strRet = "UKM3 - INSTRUKTOR PPCS") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                'admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = True

            Else

                Response.Redirect("default.aspx")

            End If

        ElseIf oCommon.isExist(strSQL2) = True Then

            strRet = oCommon.getFieldValue(strSQL2)
            Debug.WriteLine(strRet)
            If (strRet = "Admin" OrElse strRet = "ADMIN") Then
                ''SA
                admin_petugas.Visible = True
                ädmin_pelajar.Visible = True

                admin_markah.Visible = True
                admin_config.Visible = True
                admin_tawaran.Visible = True
                kelayakan_kolej.Visible = True
                ' admin_laporan.Visible = True

                ''RA PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor Ra PPCS" OrElse strRet = "UKM3 - INSTRUKTOR Ra PPCS" OrElse strRet = "Ra PPCS") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                'admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = True

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor KPP" OrElse strRet = "UKM3 - INSTRUKTOR KPP") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                'admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = True

                ''PPCS
                Ppcs_masukmarkah.Visible = False

            ElseIf (strRet = "Instruktor PPCS" OrElse strRet = "UKM3 - INSTRUKTOR PPCS") Then

                ''SA
                admin_petugas.Visible = False
                ädmin_pelajar.Visible = False
                admin_markah.Visible = False
                admin_config.Visible = False
                admin_tawaran.Visible = False
                kelayakan_kolej.Visible = False
                ' admin_laporan.Visible = False

                ''Ra PPCS
                RaPCs_masukmarkah.Visible = False

                ''KPP
                Kpp_masukmarkah.Visible = False

                ''PPCS
                Ppcs_masukmarkah.Visible = True

            Else
                Response.Redirect("default.aspx")

            End If

        Else
            Response.Redirect("default.aspx")

        End If

    End Sub

End Class