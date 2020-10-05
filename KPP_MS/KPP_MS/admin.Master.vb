﻿Imports System.Data.SqlClient

Public Class admin
    Inherits System.Web.UI.MasterPage

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then

            End If

            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                hiddenData.Value = id

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    Dim accessID As String = "select MAX(stf_ID) from security_ID where loginID_Number = '" & id & "'"
                    Dim accessData As String = getFieldValue(accessID, strConn)

                    loading_access(accessData)
                    loading_Page()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub loading_access(ByVal stf_ID As String)

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        ''data table menu
        Dim sql_data_Menu As String = "select Menu from menu_master left join menu_activity_access on menu_master.Admin_Menu_ID = menu_activity_access.Admin_Menu_ID 
                                       left join staff_Login on menu_activity_access.Login_ID = staff_Login.login_ID where staff_Login.stf_ID= '" & stf_ID & "' and staff_Login.staff_Access = '" & str_user_position & "'"

        ''data table sub menu
        Dim sql_data_Sub_Menu As String = "select Sub_Menu from menu_master left join menu_activity_access on menu_master.Admin_Menu_ID = menu_activity_access.Admin_Menu_ID 
                                           left join staff_Login on menu_activity_access.Login_ID = staff_Login.login_ID where staff_Login.stf_ID = '" & stf_ID & "' and staff_Login.staff_Access = '" & str_user_position & "'"

        FIND_DATA_MENU(sql_data_Menu)
        FIND_DATA_SUB_MENU(sql_data_Sub_Menu)

    End Sub

    Private Sub FIND_DATA_MENU(ByVal data As String)

        Dim find_general_management As Boolean = False
        Dim find_student As Boolean = False
        Dim find_staff As Boolean = False
        Dim find_coordinator As Boolean = False
        Dim find_discipline As Boolean = False
        Dim find_counselor As Boolean = False
        Dim find_examination As Boolean = False
        Dim find_payment As Boolean = False
        Dim find_hostel As Boolean = False
        Dim find_cocurricular As Boolean = False
        Dim find_report As Boolean = False
        Dim find_setting As Boolean = False
        Dim find_research As Boolean = False
        Dim find_all As Boolean = False
        Dim find_alumni As Boolean = False

        Dim DS_Menu As New DataTable
        Dim DATA_MENU As New SqlDataAdapter(data, strConn)
        Try
            DATA_MENU.Fill(DS_Menu)
        Catch ex As Exception
        End Try

        ''display menu based on admin access
        For Each row As DataRow In DS_Menu.Rows
            For Each column As DataColumn In DS_Menu.Columns
                Dim print_Menu As String = row(column.ColumnName)

                If print_Menu = "All" And print_Menu = "All" Then
                    find_general_management = True
                    find_student = True
                    find_staff = True
                    find_examination = True
                    find_payment = True
                    find_hostel = True
                    find_cocurricular = True
                    find_report = True
                    find_setting = True
                    find_discipline = True
                    find_counselor = True
                    find_coordinator = True
                    find_research = True
                    'find_alumni = True


                Else
                    If print_Menu = "General Management" Then
                        find_general_management = True
                    End If
                    If print_Menu = "Student" Then
                        find_student = True
                    End If
                    If print_Menu = "Staff" Then
                        find_staff = True
                    End If
                    If print_Menu = "Examination" Then
                        find_examination = True
                    End If
                    If print_Menu = "Payment" Then
                        find_payment = True
                    End If
                    If print_Menu = "Hostel" Then
                        find_hostel = True
                    End If
                    If print_Menu = "Report" Then
                        find_report = True
                    End If
                    If print_Menu = "Setting" Then
                        find_setting = True
                    End If
                    If print_Menu = "Discipline" Then
                        find_discipline = True
                    End If
                    If print_Menu = "Counselor" Then
                        find_counselor = True
                    End If
                    If print_Menu = "Coordinator" Then
                        find_coordinator = True
                    End If
                    If print_Menu = "Co-Curricular" Then
                        find_cocurricular = True
                    End If
                    If print_Menu = "Research" Then
                        find_research = True
                    End If
                    'If print_Menu = "Alumni" Then
                    '    find_alumni = True
                    'End If

                End If

            Next
        Next

        If find_general_management = True Then
            one.Style.Add("display", "block") ''general management
        End If
        If find_student = True Then
            two.Style.Add("display", "block") ''student
        End If
        If find_staff = True Then
            three.Style.Add("display", "block") ''staff
        End If
        If find_examination = True Then
            four.Style.Add("display", "block") ''examination
        End If
        If find_payment = True Then
            five.Style.Add("display", "block") ''payment
        End If
        If find_hostel = True Then
            six.Style.Add("display", "block") ''hostel
        End If
        If find_cocurricular = True Then
            nine.Style.Add("display", "block") ''hostel
        End If
        If find_report = True Then
            seven.Style.Add("display", "block") ''report
        End If
        If find_setting = True Then
            eight.Style.Add("display", "block") ''setting
        End If
        If find_discipline = True Then
            eleven.Style.Add("display", "block") ''discipline
        End If
        If find_counselor = True Then
            twentyone.Style.Add("display", "block") ''counselor
        End If
        If find_coordinator = True Then
            twelve.Style.Add("display", "block") ''coordinator
        End If
        If find_research = True Then
            twentytwo.Style.Add("display", "block") ''research
        End If
        'If find_alumni = True Then
        '    twentythree.Style.Add("display", "block") ''alumni
        'End If
    End Sub

    Private Sub FIND_DATA_SUB_MENU(ByVal data As String)

        ''sub menu all
        Dim find_all As Boolean = False

        ''sub menu general management
        Dim find_adminPeperiksaanPengurusanPeperiksaan As Boolean = False
        Dim find_adminPeperiksaanPengurusanGred As Boolean = False
        Dim find_adminPengurusanHostel As Boolean = False
        Dim find_adminPengurusanDisiplin As Boolean = False
        Dim find_adminPengurusanPembayaran As Boolean = False
        Dim find_adminPenilaian As Boolean = False

        ''sub menu student
        Dim find_adminPengurusanKursus As Boolean = False
        Dim find_adminPengurusanKelas As Boolean = False
        Dim find_adminCarianPelajar As Boolean = False
        Dim find_adminDaftarPelajarBaru As Boolean = False
        Dim find_adminImportPelajar As Boolean = False
        Dim find_adminPelajarPenempatanKelas As Boolean = False
        Dim find_adminPengurusanPelajar As Boolean = False
        Dim find_adminPelajarKehadiran As Boolean = False
        Dim find_adminPelajarKepastianKursus As Boolean = False
        Dim find_adminPelajarKepastianKokurikulum As Boolean = False
        Dim find_adminPelajarKepastianKelas As Boolean = False
        Dim find_adminPelajarKepastianHostel As Boolean = False
        Dim find_adminPelajarAgama As Boolean = False

        ''sub menu staff
        Dim find_adminCarianPengajar As Boolean = False
        Dim find_adminDaftarPengajarBaru As Boolean = False
        Dim find_adminImportPengajar As Boolean = False
        Dim find_adminPengajarPenempatanKelas As Boolean = False
        Dim find_adminPengajarKepastianKursus As Boolean = False

        ''sub menu coordinator
        Dim find_adminDaftarKoordinator As Boolean = False
        Dim find_adminViewKoordinator As Boolean = False

        ''sub menu discipline
        Dim find_admineditdisiplin As Boolean = False
        Dim find_adminviewdisiplin As Boolean = False

        ''sub menu counselor
        Dim find_adminSenaraiKaunselorPelajar As Boolean = False
        Dim find_adminKaunselorReview As Boolean = False
        Dim find_adminPerkembangankendiri As Boolean = False
        Dim find_adminJatiDiri As Boolean = False
        Dim find_adminPortfolio As Boolean = False
        Dim find_adminAktivitiKounselor As Boolean = False
        Dim find_adminPengurusanBiasiswa As Boolean = False
        Dim find_adminBiasiswa As Boolean = False
        Dim find_adminImportEksport As Boolean = False

        ''sub menu alumni
        Dim find_adminAlumni As Boolean = False
        Dim find_adminAlumniHistory As Boolean = False
        Dim find_adminAlumniMaklumatPembelajaran As Boolean = False
        Dim find_adminAlumniMaklumatPekerjaan As Boolean = False

        ''sub menu examination
        Dim find_adminKemaskiniMarkah As Boolean = False
        Dim find_adminKemaskiniMarkahKoko As Boolean = False
        Dim find_adminPeperiksaanTranskrip As Boolean = False
        Dim find_adminTranskripRasmi As Boolean = False
        Dim find_adminImportExport As Boolean = False
        Dim find_admin_ExamImport As Boolean = False

        ''sub menu payment
        Dim find_adminSemakYuran As Boolean = False
        Dim find_adminTransaksiYuran As Boolean = False

        ''sub menu hostel
        Dim find_adminDaftarAsramaBaru As Boolean = False
        Dim find_adminPenempatanPelajarAsrama As Boolean = False

        ''sub menu kokurikulum
        Dim find_adminPengurusanKokurikulum As Boolean = False

        ''sub menu report
        Dim find_adminLaporanPeperiksaan As Boolean = False
        Dim find_adminLaporanPeperiksaanKelas As Boolean = False
        Dim find_adminLaporanPeperiksaanKursus As Boolean = False
        Dim find_adminCartaKedudukanPelajar As Boolean = False
        Dim find_adminLaporanKehadiran As Boolean = False
        Dim find_adminLaporanBayaran As Boolean = False
        Dim find_adminLaporanUKM1 As Boolean = False
        Dim find_adminLaporanUKM2 As Boolean = False
        Dim find_adminLaporanUKM3 As Boolean = False
        Dim find_adminLaporanPPCS As Boolean = False
        Dim find_adminLaporanPCIS As Boolean = False

        ''sub menu setting
        Dim find_adminMastrerKonfigurasi As Boolean = False
        Dim find_adminKonfigurasi As Boolean = False
        Dim find_adminUserAkses As Boolean = False
        Dim find_adminUserLogin As Boolean = False
        Dim find_adminTutorial As Boolean = False

        ''sub menu research
        Dim find_adminDaftarKajianPelajar As Boolean = False
        Dim find_adminDaftarKajianProjek As Boolean = False
        Dim find_adminDaftarKajianMentor As Boolean = False
        Dim find_adminViewKajian As Boolean = False

        Dim DS_Sub_Menu As New DataTable
        Dim DATA_SUB_MENU As New SqlDataAdapter(data, strConn)
        Try
            DATA_SUB_MENU.Fill(DS_Sub_Menu)
        Catch ex As Exception
        End Try

        ''display menu based on admin access
        For Each row As DataRow In DS_Sub_Menu.Rows
            For Each column As DataColumn In DS_Sub_Menu.Columns
                Dim print_Menu As String = row(column.ColumnName)

                If print_Menu = "All" And print_Menu = "All" Then
                    find_all = True

                Else
                    If print_Menu = "Examination Management" Then
                        find_adminPeperiksaanPengurusanPeperiksaan = True
                    End If
                    If print_Menu = "Grade Management" Then
                        find_adminPeperiksaanPengurusanGred = True
                    End If
                    If print_Menu = "Hostel Management" Then
                        find_adminPengurusanHostel = True
                    End If
                    If print_Menu = "Discipline Management" Then
                        find_adminPengurusanDisiplin = True
                    End If
                    If print_Menu = "Payment Management" Then
                        find_adminPengurusanPembayaran = True
                    End If
                    If print_Menu = "Assessment Management" Then
                        find_adminPenilaian = True
                    End If


                    If print_Menu = "Courses Management" Then
                        find_adminPengurusanKursus = True
                    End If
                    If print_Menu = "Class Management" Then
                        find_adminPengurusanKelas = True
                    End If
                    If print_Menu = "Search Student" Then
                        find_adminCarianPelajar = True
                    End If
                    If print_Menu = "Register Student" Then
                        find_adminDaftarPelajarBaru = True
                    End If
                    If print_Menu = "Import Student" Then
                        find_adminImportPelajar = True
                    End If
                    If print_Menu = "Class & Course Placement" Then
                        find_adminPelajarPenempatanKelas = True
                    End If
                    If print_Menu = "Attendance" Then
                        find_adminPelajarKehadiran = True
                    End If
                    If print_Menu = "Student Management" Then
                        find_adminPengurusanPelajar = True
                    End If
                    If print_Menu = "View Courses" Then
                        find_adminPelajarKepastianKursus = True
                    End If
                    If print_Menu = "View Cocurricular" Then
                        find_adminPelajarKepastianKokurikulum = True
                    End If
                    If print_Menu = "View Class" Then
                        find_adminPelajarKepastianKelas = True
                    End If
                    If print_Menu = "View Hostel" Then
                        find_adminPelajarKepastianHostel = True
                    End If
                    If print_Menu = "View Religion" Then
                        find_adminPelajarAgama = True
                    End If


                    If print_Menu = "Search Staff" Then
                        find_adminCarianPengajar = True
                    End If
                    If print_Menu = "Register Staff" Then
                        find_adminDaftarPengajarBaru = True
                    End If
                    If print_Menu = "Import Staff" Then
                        find_adminImportPengajar = True
                    End If
                    If print_Menu = "Class & Courses Placement" Then
                        find_adminPengajarPenempatanKelas = True
                    End If
                    If print_Menu = "View Course" Then
                        find_adminPengajarKepastianKursus = True
                    End If


                    If print_Menu = "Register Coordinator" Then
                        find_adminDaftarKoordinator = True
                    End If
                    If print_Menu = "View Coordinator" Then
                        find_adminViewKoordinator = True
                    End If


                    If print_Menu = "Register Case" Then
                        find_admineditdisiplin = True
                    End If
                    If print_Menu = "View Case" Then
                        find_adminviewdisiplin = True
                    End If


                    If print_Menu = "Student List" Then
                        find_adminSenaraiKaunselorPelajar = True
                    End If
                    If print_Menu = "Summary Review" Then
                        find_adminKaunselorReview = True
                    End If
                    If print_Menu = "Self Development" Then
                        find_adminPerkembangankendiri = True
                    End If
                    If print_Menu = "Personality Development" Then
                        find_adminJatiDiri = True
                    End If
                    If print_Menu = "Portfolio" Then
                        find_adminPortfolio = True
                    End If
                    If print_Menu = "Counselling Activity" Then
                        find_adminAktivitiKounselor = True
                    End If
                    If print_Menu = "Scholarship Management" Then
                        find_adminPengurusanBiasiswa = True
                    End If
                    If print_Menu = "Scholarship" Then
                        find_adminBiasiswa = True
                    End If
                    If print_Menu = "Import/Export Result" Then
                        find_adminImportEksport = True
                    End If


                    If print_Menu = "Update Academic Result" Then
                        find_adminKemaskiniMarkah = True
                    End If
                    If print_Menu = "Update Co-curriculum Result" Then
                        find_adminKemaskiniMarkahKoko = True
                    End If
                    If print_Menu = "Exam Transcript" Then
                        find_adminPeperiksaanTranskrip = True
                    End If
                    If print_Menu = "Official Transcript" Then
                        find_adminTranskripRasmi = True
                    End If
                    If print_Menu = "Import/Export Result" Then
                        find_adminImportExport = True
                    End If
                    If print_Menu = "Import GPA & CGPA" Then
                        find_admin_ExamImport = True
                    End If


                    If print_Menu = "Payment Information" Then
                        find_adminSemakYuran = True
                    End If
                    If print_Menu = "Transaction Information" Then
                        find_adminTransaksiYuran = True
                    End If


                    If print_Menu = "Register Hostel" Then
                        find_adminDaftarAsramaBaru = True
                    End If
                    If print_Menu = "Student Placement" Then
                        find_adminPenempatanPelajarAsrama = True
                    End If

                    If print_Menu = "Configuration" Then
                        find_adminPengurusanKokurikulum = True
                    End If
                    If print_Menu = "Co-Curricular Management" Then
                        find_adminPengurusanKokurikulum = True
                    End If
                    If print_Menu = "Configuration" Then
                        find_adminPengurusanKokurikulum = True
                    End If


                    If print_Menu = "ThenExamination Report" Then
                        find_adminLaporanPeperiksaan = True
                    End If
                    If print_Menu = "Examination Report" Then
                        find_adminKemaskiniMarkah = True
                    End If
                    If print_Menu = "Examination Report" Then
                        find_adminKemaskiniMarkah = True
                    End If
                    If print_Menu = "Class Examination Report" Then
                        find_adminLaporanPeperiksaanKelas = True
                    End If
                    If print_Menu = "Courses Examination Report" Then
                        find_adminLaporanPeperiksaanKursus = True
                    End If
                    If print_Menu = "Student Ranking List" Then
                        find_adminCartaKedudukanPelajar = True
                    End If
                    If print_Menu = "Attendance Report" Then
                        find_adminLaporanKehadiran = True
                    End If
                    If print_Menu = "Financial Report" Then
                        find_adminLaporanBayaran = True
                    End If
                    If print_Menu = "UKM1 History" Then
                        find_adminLaporanUKM1 = True
                    End If
                    If print_Menu = "UKM2 History" Then
                        find_adminLaporanUKM2 = True
                    End If
                    If print_Menu = "UKM3 History" Then
                        find_adminLaporanUKM3 = True
                    End If
                    If print_Menu = "PPCS History" Then
                        find_adminLaporanPPCS = True
                    End If
                    If print_Menu = "PCIS History" Then
                        find_adminLaporanPCIS = True
                    End If

                    If print_Menu = "Alumni" Then
                        find_adminAlumni = True
                    End If
                    If print_Menu = "History" Then
                        find_adminAlumniHistory = True
                    End If
                    If print_Menu = "Study Information" Then
                        find_adminAlumniMaklumatPembelajaran = True
                    End If
                    If print_Menu = "Occupation Information" Then
                        find_adminAlumniMaklumatPekerjaan = True
                    End If

                    If print_Menu = "Admin Configuration" Then
                        find_adminMastrerKonfigurasi = True
                    End If
                    If print_Menu = "System Configuration" Then
                        find_adminKonfigurasi = True
                    End If
                    If print_Menu = "User Access" Then
                        find_adminUserAkses = True
                    End If
                    If print_Menu = "User Login Trail" Then
                        find_adminUserLogin = True
                    End If
                    If print_Menu = "Tutorial" Then
                        find_adminTutorial = True
                    End If

                    If print_Menu = "Register Student/Supervisor" Then
                        find_adminDaftarKajianPelajar = True
                    End If
                    If print_Menu = "Research Project/Field" Then
                        find_adminDaftarKajianProjek = True
                    End If
                    If print_Menu = "Register Mentor" Then
                        find_adminDaftarKajianMentor = True
                    End If
                    If print_Menu = "View List" Then
                        find_adminViewKajian = True
                    End If

                End If
            Next
        Next

        ''general management
        If find_adminPeperiksaanPengurusanPeperiksaan = True Then
            adminPeperiksaanPengurusanPeperiksaan.Style.Add("display", "block") ''Examination Management
        End If
        If find_adminPeperiksaanPengurusanGred = True Then
            adminPeperiksaanPengurusanGred.Style.Add("display", "block") ''Grade Management
        End If
        If find_adminPenilaian = True Then
            adminPenilaian.Style.Add("display", "block") ''Assessment Management
        End If

        'student
        If find_adminPengurusanKursus = True Then
            adminPengurusanKursus.Style.Add("display", "block") ''Courses Management
        End If
        If find_adminPengurusanKelas = True Then
            adminPengurusanKelas.Style.Add("display", "block") ''Class Management
        End If
        If find_adminCarianPelajar = True Then
            adminCarianPelajar.Style.Add("display", "block") ''Search Student
        End If
        If find_adminDaftarPelajarBaru = True Then
            adminDaftarPelajarBaru.Style.Add("display", "block") ''Register Student
        End If
        If find_adminImportPelajar = True Then
            adminImportPelajar.Style.Add("display", "block") ''Import Student
        End If
        If find_adminPelajarPenempatanKelas = True Then
            adminPelajarPenempatanKelas.Style.Add("display", "block") ''Class & Course Placement
        End If
        If find_adminPelajarKehadiran = True Then
            adminPelajarKehadiran.Style.Add("display", "block") ''Attendance
        End If
        If find_adminPengurusanPelajar = True Then
            adminPengurusanPelajar.Style.Add("display", "block") ''Student Management
        End If
        If find_adminPelajarKepastianKursus = True Then
            adminPelajarKepastianKursus.Style.Add("display", "block") ''View Courses
        End If
        If find_adminPelajarKepastianKokurikulum = True Then
            adminPelajarKepastianKokurikulum.Style.Add("display", "block") ''View Cocuricular
        End If
        If find_adminPelajarKepastianKelas = True Then
            adminPelajarKepastianKelas.Style.Add("display", "block") ''View Class
        End If
        If find_adminPelajarKepastianHostel = True Then
            adminPelajarKepastianHostel.Style.Add("display", "block") ''View Hostel
        End If
        If find_adminPelajarAgama = True Then
            adminPelajarAgama.Style.Add("display", "block")
        End If

        'staff
        If find_adminCarianPengajar = True Then
            adminCarianPengajar.Style.Add("display", "block") ''Search Staff
        End If
        If find_adminDaftarPengajarBaru = True Then
            adminDaftarPengajarBaru.Style.Add("display", "block") ''Register Staff
        End If
        If find_adminImportPengajar = True Then
            adminImportPengajar.Style.Add("display", "block") ''Import Staff
        End If
        If find_adminPengajarPenempatanKelas = True Then
            adminPengajarPenempatanKelas.Style.Add("display", "block") ''Class & Courses Placement
        End If
        If find_adminPengajarKepastianKursus = True Then
            adminPengajarKepastianKursus.Style.Add("display", "block") ''View Course
        End If

        'coordinator
        If find_adminDaftarKoordinator = True Then
            adminDaftarKoordinator.Style.Add("display", "block") ''Register Coordinator
        End If
        If find_adminViewKoordinator = True Then
            adminViewKoordinator.Style.Add("display", "block") ''View Coordinator
        End If

        'discipline
        If find_adminPengurusanDisiplin = True Then
            adminPengurusanDisiplin.Style.Add("display", "block") ''Discipline Management
        End If
        If find_admineditdisiplin = True Then
            admineditdisiplin.Style.Add("display", "block") ''Register Case    
        End If
        If find_adminviewdisiplin = True Then
            adminviewdisiplin.Style.Add("display", "block") ''View Case
        End If

        'counselor
        If find_adminSenaraiKaunselorPelajar = True Then
            adminSenaraiKaunselorPelajar.Style.Add("display", "block") ''Discipline Management
        End If
        If find_adminKaunselorReview = True Then
            'adminKaunselorReview.Style.Add("display", "block") ''Register Case    
        End If
        If find_adminPerkembangankendiri = True Then
            adminPerkembanganKendiri.Style.Add("display", "block") ''Self Development
        End If
        If find_adminJatiDiri = True Then
            adminJatiDiri.Style.Add("display", "block") ''Personality Development
        End If
        'alumni
        If find_adminAlumni = True Then
            adminAlumni.Style.Add("display", "block") ''Alumni
        End If
        If find_adminPortfolio = True Then
            adminPortfolio.Style.Add("display", "block") ''Portfolio
        End If
        If find_adminAktivitiKounselor = True Then
            adminAktivitiKounselor.Style.Add("display", "block") ''Counselling Activity
        End If
        If find_adminPengurusanBiasiswa = True Then
            adminPengurusanBiasiswa.Style.Add("display", "block") ''Scholarship Management
        End If
        If find_adminBiasiswa = True Then
            adminBiasiswa.Style.Add("display", "block") ''Scholarship
        End If
        If find_adminImportEksport = True Then
            adminImportEksport.Style.Add("display", "block") ''Import/Export Result
        End If

        'examination
        If find_adminKemaskiniMarkah = True Then
            adminKemaskiniMarkah.Style.Add("display", "block") ''Update Academic Result
        End If
        If find_adminKemaskiniMarkahKoko = True Then
            adminKemaskiniMarkahKoko.Style.Add("display", "block") ''Update Co-curriculum Result
        End If
        If find_adminPeperiksaanTranskrip = True Then
            adminPeperiksaanTranskrip.Style.Add("display", "block") ''Exam Transcript
        End If
        If find_adminTranskripRasmi = True Then
            adminTranskripRasmi.Style.Add("display", "block") ''Official Transcript
        End If
        If find_adminImportExport = True Then
            adminImportExport.Style.Add("display", "block") ''Import / Export Result
        End If
        If find_admin_ExamImport = True Then
            admin_ExamImport.Style.Add("display", "block") ''Import GPA & CGPA
        End If

        'payment
        If find_adminPengurusanPembayaran = True Then
            adminPengurusanPembayaran.Style.Add("display", "block") ''Payment Management
        End If
        If find_adminSemakYuran = True Then
            adminSemakYuran.Style.Add("display", "block") ''Payent Information    
        End If
        If find_adminTransaksiYuran = True Then
            adminTransaksiYuran.Style.Add("display", "block") ''Transaction Information
        End If

        'hostel
        If find_adminPengurusanHostel = True Then
            adminPengurusanHostel.Style.Add("display", "block") ''Hostel Management
        End If
        If find_adminDaftarAsramaBaru = True Then
            adminDaftarAsramaBaru.Style.Add("display", "block") ''Register Hostel    
        End If
        If find_adminPenempatanPelajarAsrama = True Then
            adminPenempatanPelajarAsrama.Style.Add("display", "block") ''Student Placement
        End If

        'kokurikulum
        If find_adminPengurusanKokurikulum = True Then
            adminPengurusanKokurikulum.Style.Add("display", "block") ''Co-Curricular
        End If

        'report
        If find_adminLaporanPeperiksaan = True Then
            adminLaporanPeperiksaan.Style.Add("display", "block") ''Examination Report
        End If
        If find_adminLaporanPeperiksaanKelas = True Then
            adminLaporanPeperiksaanKelas.Style.Add("display", "block") ''Class Examination Report
        End If
        If find_adminLaporanPeperiksaanKursus = True Then
            adminLaporanPeperiksaanKursus.Style.Add("display", "block") ''Courses Examination Report
        End If
        If find_adminCartaKedudukanPelajar = True Then
            adminCartaKedudukanPelajar.Style.Add("display", "block") ''Student Ranking List
        End If
        If find_adminLaporanKehadiran = True Then
            adminLaporanKehadiran.Style.Add("display", "block") ''Attendance Report
        End If
        If find_adminLaporanBayaran = True Then
            adminLaporanBayaran.Style.Add("display", "block") ''Financial Report
        End If
        If find_adminLaporanUKM1 = True Then
            adminLaporanUKM1.Style.Add("display", "block") ''UKM1 History
        End If
        If find_adminLaporanUKM2 = True Then
            adminLaporanUKM2.Style.Add("display", "block") ''UKM2 History
        End If
        If find_adminLaporanUKM3 = True Then
            adminLaporanUKM3.Style.Add("display", "block") ''UKM3 History
        End If
        If find_adminLaporanPPCS = True Then
            adminLaporanPPCS.Style.Add("display", "block") ''PPCS History
        End If
        If find_adminLaporanPCIS = True Then
            adminLaporanPCIS.Style.Add("display", "block") ''PCIS History
        End If

        'setting
        If find_adminMastrerKonfigurasi = True Then
            adminMastrerKonfigurasi.Style.Add("display", "block") ''Admin Configuration
        End If
        If find_adminKonfigurasi = True Then
            adminKonfigurasi.Style.Add("display", "block") ''System Configuration
        End If
        If find_adminUserAkses = True Then
            adminUserAkses.Style.Add("display", "block") ''User Access
        End If
        If find_adminUserLogin = True Then
            adminUserLogin.Style.Add("display", "block") ''User Login Trail
        End If
        If find_adminTutorial = True Then
            adminTutorial.Style.Add("display", "block") ''Tutorial
        End If

        ''Research
        If find_adminDaftarKajianPelajar = True Then
            adminDaftarKajianPelajar.Style.Add("display", "block") ''Register Student/Supervisor
        End If
        If find_adminDaftarKajianProjek = True Then
            adminDaftarKajianProjek.Style.Add("display", "block") ''Research Project/Field
        End If
        If find_adminDaftarKajianMentor = True Then
            adminDaftarKajianMentor.Style.Add("display", "block") ''Register Mentor
        End If
        If find_adminViewKajian = True Then
            adminViewKajian.Style.Add("display", "block") ''View List
        End If

        'all menu
        If find_all = True Then
            adminPeperiksaanPengurusanPeperiksaan.Style.Add("display", "block") ''Examination Management
            adminPeperiksaanPengurusanGred.Style.Add("display", "block") ''Grade Management
            adminPengurusanHostel.Style.Add("display", "block") ''Hostel Management
            adminPengurusanDisiplin.Style.Add("display", "block") ''Discipline Management
            adminConfigWarningLetter.Style.Add("display", "block") ''Warning Letter Setting
            adminPengurusanPembayaran.Style.Add("display", "block") ''Payment Management
            adminPenilaian.Style.Add("display", "block") ''Assessment Management

            adminPengurusanKursus.Style.Add("display", "block") ''Courses Management
            adminPengurusanKelas.Style.Add("display", "block") ''Class Management
            adminCarianPelajar.Style.Add("display", "block") ''Search Student
            adminDaftarPelajarBaru.Style.Add("display", "block") ''Register Student
            adminImportPelajar.Style.Add("display", "block") ''Import Student
            adminPelajarPenempatanKelas.Style.Add("display", "block") ''Class & Course Placement
            adminPelajarKehadiran.Style.Add("display", "block") ''Attendance
            adminPengurusanPelajar.Style.Add("display", "block") ''Student Management
            adminPelajarKepastianKursus.Style.Add("display", "block") ''View Courses
            adminPelajarKepastianKokurikulum.Style.Add("display", "block") ''View Cocurricular
            adminPelajarKepastianKelas.Style.Add("display", "block") ''View Class
            adminPelajarKepastianHostel.Style.Add("display", "block") ''View Hostel
            adminPelajarAgama.Style.Add("display", "block") ''View Religion

            adminCarianPengajar.Style.Add("display", "block") ''Search Staff
            adminDaftarPengajarBaru.Style.Add("display", "block") ''Register Staff
            adminImportPengajar.Style.Add("display", "block") ''Import Staff
            adminPengajarPenempatanKelas.Style.Add("display", "block") ''Class & Courses Placement
            adminPengajarKepastianKursus.Style.Add("display", "block") ''View Course

            adminDaftarKoordinator.Style.Add("display", "block") ''Register Coordinator
            adminViewKoordinator.Style.Add("display", "block") ''View Coordinator

            admineditdisiplin.Style.Add("display", "block") ''Register Case 
            adminviewdisiplin.Style.Add("display", "block") ''View 

            adminSenaraiKaunselorPelajar.Style.Add("display", "block") ''Search Student Case 
            'adminKaunselorReview.Style.Add("display", "block") ''Smmary Review
            adminPerkembanganKendiri.Style.Add("display", "block") ''Self Development
            adminJatiDiri.Style.Add("display", "block") ''Personality Development
            adminPortfolio.Style.Add("display", "block") ''Portfolio
            adminAktivitiKounselor.Style.Add("display", "block") ''Counselling Activity
            adminPengurusanBiasiswa.Style.Add("display", "block") ''Scholarship Management
            adminBiasiswa.Style.Add("display", "block") ''Scholarship
            adminImportEksport.Style.Add("display", "block") ''Import/Export Result

            adminKemaskiniMarkah.Style.Add("display", "block") ''Update Academic Result
            adminKemaskiniMarkahKoko.Style.Add("display", "block") ''Update Co-curriculum Result
            adminPeperiksaanTranskrip.Style.Add("display", "block") ''Exam Transcript
            adminTranskripRasmi.Style.Add("display", "block") ''Official Transcript
            adminImportExport.Style.Add("display", "block") ''Import / Export Result
            admin_ExamImport.Style.Add("display", "block") ''Import GPA & CGPA

            adminSemakYuran.Style.Add("display", "block") ''Payment Information  
            adminTransaksiYuran.Style.Add("display", "block") ''Transaction Information

            adminDaftarAsramaBaru.Style.Add("display", "block") ''Register Hostel 
            adminPenempatanPelajarAsrama.Style.Add("display", "block") ''View Case

            adminPengurusanKokurikulum.Style.Add("display", "block") ''Co-Curricular

            adminLaporanPeperiksaan.Style.Add("display", "block") ''Examination Report
            adminLaporanPeperiksaanKelas.Style.Add("display", "block") ''Class Examination Report
            adminLaporanPeperiksaanKursus.Style.Add("display", "block") ''Courses Examination Report
            adminCartaKedudukanPelajar.Style.Add("display", "block") ''Student Ranking List
            adminLaporanKehadiran.Style.Add("display", "block") ''Attendance Report
            adminLaporanBayaran.Style.Add("display", "block") ''Financial Report
            adminLaporanUKM1.Style.Add("display", "block") ''UKM1 History
            adminLaporanUKM2.Style.Add("display", "block") ''UKM2 History
            adminLaporanUKM3.Style.Add("display", "block") ''UKM3 History
            adminLaporanPPCS.Style.Add("display", "block") ''PPCS History
            adminLaporanPCIS.Style.Add("display", "block") ''PCIS History

            adminAlumni.Style.Add("display", "block") 'alumni

            adminMastrerKonfigurasi.Style.Add("display", "block") ''Admin Configuration
            adminKonfigurasi.Style.Add("display", "block") ''System Configuration
            adminUserAkses.Style.Add("display", "block") ''User Access
            adminUserLogin.Style.Add("display", "block") ''User Login Trail
            adminTutorial.Style.Add("display", "block") ''Tutorial

            adminDaftarKajianPelajar.Style.Add("display", "block") ''Register Student/Supervisor
            adminDaftarKajianProjek.Style.Add("display", "block") ''Research Project/Field
            adminDaftarKajianMentor.Style.Add("display", "block") ''Register Mentor
            adminViewKajian.Style.Add("display", "block") ''View List
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

        Dim running_ID As String = Request.QueryString("admin_ID")
        Dim accessID As String = "select stf_ID from security_LoginID where security_ID = '" & running_ID & "'"
        Dim accessData As String = getFieldValue(accessID, strConn)

        Home.NavigateUrl = String.Format("admin_login_berjaya.aspx?admin_ID=" + running_ID)
        adminPeperiksaanPengurusanPeperiksaan.NavigateUrl = String.Format("admin_peperiksaan_pengurusan_peperiksaan.aspx?admin_ID=" + running_ID)
        adminPeperiksaanPengurusanGred.NavigateUrl = String.Format("admin_peperiksaan_pengurusan_gred.aspx?admin_ID=" + running_ID)
        adminPengurusanHostel.NavigateUrl = String.Format("admin_pengurusan_am_hostel.aspx?admin_ID=" + running_ID)
        adminPengurusanDisiplin.NavigateUrl = String.Format("admin_config_disiplin.aspx?admin_ID=" + running_ID)
        adminConfigWarningLetter.NavigateUrl = String.Format("admin_config_warning_letter.aspx?admin_ID=" + running_ID + "&v=0")
        adminPengurusanPembayaran.NavigateUrl = String.Format("admin_daftar_yuran_baru.aspx?admin_ID=" + running_ID + "&value=0")
        adminPenilaian.NavigateUrl = String.Format("admin_penilaian_konfig.aspx?admin_ID=" + running_ID)

        adminPengurusanKursus.NavigateUrl = String.Format("admin_pengurusan_am_kursus.aspx?admin_ID=" + running_ID)
        adminPengurusanKelas.NavigateUrl = String.Format("admin_pengurusan_am_kelas.aspx?admin_ID=" + running_ID)
        adminCarianPelajar.NavigateUrl = String.Format("admin_carian_pelajar.aspx?admin_ID=" + running_ID)
        adminDaftarPelajarBaru.NavigateUrl = String.Format("admin_daftar_pelajar_baru.aspx?admin_ID=" + running_ID)
        adminImportPelajar.NavigateUrl = String.Format("admin_import_pelajar.aspx?admin_ID=" + running_ID)
        adminPelajarPenempatanKelas.NavigateUrl = String.Format("admin_pelajar_penempatan_kelas.aspx?admin_ID=" + running_ID)
        adminPengurusanPelajar.NavigateUrl = String.Format("adminPengurusanPelajar.aspx?admin_ID=" + running_ID)
        adminPelajarKepastianKursus.NavigateUrl = String.Format("admin_pelajar_kepastian_kursus.aspx?admin_ID=" + running_ID)
        adminPelajarKepastianKokurikulum.NavigateUrl = String.Format("admin_pelajar_kepastian_kokurikulum.aspx?admin_ID=" + running_ID)
        adminPelajarKepastianKelas.NavigateUrl = String.Format("admin_pelajar_kepastian_kelas.aspx?admin_ID=" + running_ID)
        adminPelajarKepastianHostel.NavigateUrl = String.Format("admin_pelajar_kepastian_hostel.aspx?admin_ID=" + running_ID)
        adminPelajarKehadiran.NavigateUrl = String.Format("admin_pelajar_kehadiran.aspx?admin_ID=" + running_ID)
        adminPelajarAgama.NavigateUrl = String.Format("admin_pelajar_kepastian_agama.aspx?admin_ID=" + running_ID)

        adminCarianPengajar.NavigateUrl = String.Format("admin_carian_pengajar.aspx?admin_ID=" + running_ID)
        adminDaftarPengajarBaru.NavigateUrl = String.Format("admin_daftar_pengajar_baru.aspx?admin_ID=" + running_ID)
        adminImportPengajar.NavigateUrl = String.Format("admin_import_pengajar.aspx?admin_ID=" + running_ID)
        adminPengajarPenempatanKelas.NavigateUrl = String.Format("admin_pengajar_penempatan_kelas.aspx?admin_ID=" + running_ID)
        adminPengajarKepastianKursus.NavigateUrl = String.Format("admin_pengajar_kepastian_kursus.aspx?admin_ID=" + running_ID)

        adminSemakYuran.NavigateUrl = String.Format("admin_carian_yuran.aspx?admin_ID=" + running_ID)
        adminTransaksiYuran.NavigateUrl = String.Format("admin_transaksi_yuran.aspx?admin_ID=" + running_ID)

        adminDaftarKoordinator.NavigateUrl = String.Format("admin_daftar_koordinator.aspx?admin_ID=" + running_ID)
        adminViewKoordinator.NavigateUrl = String.Format("admin_view_koordinator.aspx?admin_ID=" + running_ID)

        adminKemaskiniMarkah.NavigateUrl = String.Format("admin_peperiksaan_kemaskini_markah.aspx?admin_ID=" + running_ID)
        adminKemaskiniMarkahKoko.NavigateUrl = String.Format("admin_kemaskini_markah_koko.aspx?admin_ID=" + running_ID)
        adminPeperiksaanTranskrip.NavigateUrl = String.Format("admin_peperiksaan_transkrip.aspx?admin_ID=" + running_ID)
        adminTranskripRasmi.NavigateUrl = String.Format("admin_transkrip_rasmi.aspx?admin_ID=" + running_ID)
        adminImportExport.NavigateUrl = String.Format("admin_importexport_peperiksaan_markah.aspx?admin_ID=" + running_ID)

        adminPengurusanKokurikulum.NavigateUrl = String.Format("http://kokoadmin.permatapintar.edu.my/admin/admin.login.success.aspx?admin_ID=" + Request.QueryString("admin_ID"))

        adminDaftarAsramaBaru.NavigateUrl = String.Format("admin_daftar_asrama_baru.aspx?admin_ID=" + running_ID)
        adminPenempatanPelajarAsrama.NavigateUrl = String.Format("admin_asrama_penempatan_pelajar.aspx?admin_ID=" + running_ID)

        adminLaporanPeperiksaan.NavigateUrl = String.Format("admin_laporanPelajar_peperiksaan.aspx?admin_ID=" + running_ID)

        adminLaporanPeperiksaanKelas.NavigateUrl = String.Format("admin_laporanPeperiksaan_peperiksaan_kelas.aspx?admin_ID=" + running_ID)
        adminLaporanPeperiksaanKursus.NavigateUrl = String.Format("admin_laporanPeperiksaan_peperiksaan_kursus.aspx?admin_ID=" + running_ID)
        adminCartaKedudukanPelajar.NavigateUrl = String.Format("admin_laporanPeperiksaan_kedudukan_pelajar.aspx?admin_ID=" + running_ID)
        adminLaporanKehadiran.NavigateUrl = String.Format("admin_laporanPeperiksaan_laporan_kehadiran.aspx?admin_ID=" + running_ID)
        adminLaporanUKM1.NavigateUrl = String.Format("admin_laporanUKM1.aspx?admin_ID=" + running_ID)
        adminLaporanUKM2.NavigateUrl = String.Format("admin_laporanUKM2.aspx?admin_ID=" + running_ID)
        adminLaporanUKM3.NavigateUrl = String.Format("admin_laporanUKM3.aspx?admin_ID=" + running_ID)
        adminLaporanPPCS.NavigateUrl = String.Format("admin_laporanPPCS.aspx?admin_ID=" + running_ID)
        adminLaporanPCIS.NavigateUrl = String.Format("admin_laporanPCIS.aspx?admin_ID=" + running_ID)

        ''Add new line
        adminAlumni.NavigateUrl = String.Format("admin_alumni.aspx?admin_ID=" + running_ID)

        admineditdisiplin.NavigateUrl = String.Format("admin_edit_disiplin.aspx?admin_ID=" + running_ID)
        adminviewdisiplin.NavigateUrl = String.Format("admin_view_disiplin.aspx?admin_ID=" + running_ID)

        adminSenaraiKaunselorPelajar.NavigateUrl = String.Format("admin_carian_pelajar_kaunselor.aspx?admin_ID=" + running_ID)
        adminPerkembanganKendiri.NavigateUrl = String.Format("admin_kaunselor_perkembangankendiri.aspx?admin_ID=" + running_ID)
        adminJatiDiri.NavigateUrl = String.Format("admin_kaunselor_jatidiri.aspx?admin_ID=" + running_ID)
        adminPortfolio.NavigateUrl = String.Format("admin_kaunselor_portfolio.aspx?admin_ID=" + running_ID)
        adminAktivitiKounselor.NavigateUrl = String.Format("admin_kaunselor_aktivitikaunselor.aspx?admin_ID=" + running_ID)
        adminPengurusanBiasiswa.NavigateUrl = String.Format("admin_kaunselor_pengurusanbiasiswa.aspx?admin_ID=" + running_ID)
        adminBiasiswa.NavigateUrl = String.Format("admin_kaunselor_biasiswa.aspx?admin_ID=" + running_ID)
        adminImportEksport.NavigateUrl = String.Format("admin_kaunselor_importeksport.aspx?admin_ID=" + running_ID)

        adminMastrerKonfigurasi.NavigateUrl = String.Format("admin_Master_Konfigurasi.aspx?admin_ID=" + running_ID)
        adminKonfigurasi.NavigateUrl = String.Format("admin_konfigurasi.aspx?admin_ID=" + running_ID)
        adminUserAkses.NavigateUrl = String.Format("admin_userAkses.aspx?admin_ID=" + running_ID)
        adminUserLogin.NavigateUrl = String.Format("admin_userLogin.aspx?admin_ID=" + running_ID)
        adminTutorial.NavigateUrl = String.Format("admin_tutorial.aspx?admin_ID=" + running_ID)

        adminViewKajian.NavigateUrl = String.Format("admin_penyelidikanPelajar.aspx?admin_ID=" + running_ID)
        adminDaftarKajianPelajar.NavigateUrl = String.Format("admin_daftarPenyelidikanPelajar.aspx?admin_ID=" + running_ID)
        adminDaftarKajianProjek.NavigateUrl = String.Format("admin_daftarPenyelidikanProjek.aspx?admin_ID=" + running_ID)
        adminDaftarKajianMentor.NavigateUrl = String.Format("admin_daftarPenyelidikanMentor.aspx?admin_ID=" + running_ID)

        adminImport.NavigateUrl = String.Format("Import_Data.aspx?admin_ID=" + running_ID)
        'adminImportStudent.NavigateUrl = String.Format("Import_Student_Data_Temporary.aspx?admin_ID=" + running_ID)
        admin_ExamImport.NavigateUrl = String.Format("Import_Exam_Data.aspx?admin_ID=" + running_ID)
        adminTukarKataLaluan.NavigateUrl = String.Format("Tukar_KataLaluan.aspx?admin_ID=" + running_ID)
        adminLogout.NavigateUrl = String.Format("default.aspx?result=88&admin_ID=" + running_ID)

    End Sub
End Class