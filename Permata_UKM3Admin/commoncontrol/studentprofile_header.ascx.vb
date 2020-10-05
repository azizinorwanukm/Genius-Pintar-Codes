Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class studentprofile_header
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim straction As String = ""
        Dim nPageno As Integer = 0
        Dim strDateCreated As String
        Dim strcourseCode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("studentid").Length <= 4 Then
            Dim stdID As String = Request.QueryString("studentid")
            lblStudentID.Text = oCommon.getFieldValue("SELECT std_guid FROM ukm3.dbo.UKM3 where id = '" & stdID & "'")
        Else
            lblStudentID.Text = Request.QueryString("studentid")
        End If
        strDateCreated = oCommon.getToday

        Try
            If Not IsPostBack Then
                '--get student profile
                LoadProfile(lblStudentID.Text)
            End If

        Catch ex As Exception
            ''lblMsgTop.Text = ex.Message
        End Try

    End Sub

    Private Sub LoadProfile(ByVal strValue As String)

        Dim strSchoolID As String = oCommon.getFieldValue("SELECT SchoolID FROM permatapintar.dbo.StudentSchool WHERE StudentID='" & lblStudentID.Text & "'")



        ''--StudentFullname
        strSQL = "SELECT StudentFullname FROM permatapintar.dbo.StudentProfile WHERE StudentID='" & lblStudentID.Text & "'"
        lblRespFullname.Text = oCommon.getFieldValue(strSQL)

        '--PPCSDate
        strSQL = "SELECT PPCSDate FROM permatapintar.dbo.PPCS WHERE StudentID='" & lblStudentID.Text & "' AND ClassID=" & Request.QueryString("classid")
        lblPPCSDate.Text = oCommon.getFieldValue(strSQL)

        '--MYKAD
        strSQL = "SELECT MYKAD FROM permatapintar.dbo.StudentProfile WHERE StudentID='" & lblStudentID.Text & "'"
        lblMYKAD.Text = oCommon.getFieldValue(strSQL)

        '--SchoolName
        strSQL = "SELECT SchoolName FROM permatapintar.dbo.SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        lblSchoolName.Text = oCommon.getFieldValue(strSQL)

        '--SchoolState
        strSQL = "SELECT SchoolState FROM permatapintar.dbo.SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        lblSchoolState.Text = oCommon.getFieldValue(strSQL)

        ''--CourseCode
        strSQL = "SELECT CourseCode FROM permatapintar.dbo.PPCS_Course WHERE CourseID=" & Request.QueryString("courseid")
        lblPPCSCourse.Text = oCommon.getFieldValue(strSQL)

        '--NamaKetuaModul,NamaKetuaModulBI
        strSQL = "SELECT NamaKetuaModul,NamaKetuaModulBI FROM PPCS_Course WHERE CourseID=" & Request.QueryString("courseid")
        lblNamaKetuaModul.Text = oCommon.getFieldValueEx(strSQL)

        '--ClassCode
        strSQL = "SELECT ClassCode FROM permatapintar.dbo.PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
        lblPPCSClass.Text = oCommon.getFieldValue(strSQL)

        '--NamaPengajar
        strSQL = "SELECT NamaPengajar FROM permatapintar.dbo.PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
        lblNamaPengajar.Text = oCommon.getFieldValue(strSQL)

        '--NamaPembantuPengajar
        strSQL = "SELECT NamaPembantuPengajar FROM permatapintar.dbo.PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
        lblNamaPembantuPengajar.Text = oCommon.getFieldValue(strSQL)

        '--NamaPembantuPelajar
        strSQL = "SELECT NamaPembantuPelajar FROM permatapintar.dbo.PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
        lblNamaPembantuPelajar.Text = oCommon.getFieldValue(strSQL)

        ''--load initial photo here
        imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")

    End Sub


End Class