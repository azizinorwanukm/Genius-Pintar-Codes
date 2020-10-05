Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class studentprofile_header_ppcs
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
        lblStudentID.Text = Request.QueryString("studentid")
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
        ''--fullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WHERE StudentID='" & lblStudentID.Text & "'"
        lblRespFullname.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT MYKAD FROM StudentProfile WHERE StudentID='" & lblStudentID.Text & "'"
        lblMYKAD.Text = oCommon.getFieldValue(strSQL)

        ''school info
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & lblStudentID.Text & "'"
        Dim strSchoolID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SchoolName FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        lblSchoolName.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SchoolState FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        lblSchoolState.Text = oCommon.getFieldValue(strSQL)

        ' ''ppcs course and class
        'strSQL = "SELECT PPCSCourse FROM PPCS WHERE StudentID='" & lblStudentID.Text & "' AND ExamYear='" & Request.QueryString("year") & "'"
        'lblPPCSCourse.Text = oCommon.getFieldValue(strSQL)

        'strSQL = "SELECT PPCSClass FROM PPCS WHERE StudentID='" & lblStudentID.Text & "' AND ExamYear='" & Request.QueryString("year") & "'"
        'lblPPCSClass.Text = oCommon.getFieldValue(strSQL)

        ''--load initial photo here
        imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")

    End Sub

End Class