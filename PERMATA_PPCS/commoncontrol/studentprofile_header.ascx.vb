Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
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
        ''--StudentFullname
        strSQL = "SELECT StudentFullname FROM StudentProfile WHERE StudentID='" & lblStudentID.Text & "'"
        lblRespFullname.Text = oCommon.getFieldValue(strSQL)

        '--PPCSDate
        strSQL = "SELECT PPCSDate FROM PPCS WHERE StudentID='" & lblStudentID.Text & "' AND ClassID=" & Request.QueryString("classid")
        lblPPCSDate.Text = oCommon.getFieldValue(strSQL)

        '--MYKAD
        strSQL = "SELECT MYKAD FROM StudentProfile WHERE StudentID='" & lblStudentID.Text & "'"
        lblMYKAD.Text = oCommon.getFieldValue(strSQL)

        ''--SchoolID
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & lblStudentID.Text & "'"
        Dim strSchoolID As String = oCommon.getFieldValue(strSQL)

        '--SchoolName
        strSQL = "SELECT SchoolName FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        lblSchoolName.Text = oCommon.getFieldValue(strSQL)

        '--SchoolState
        strSQL = "SELECT SchoolState FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        lblSchoolState.Text = oCommon.getFieldValue(strSQL)

        ''--CourseCode
        strSQL = "SELECT CourseCode FROM PPCS_Course WHERE CourseID=" & Request.QueryString("courseid")
        lblPPCSCourse.Text = oCommon.getFieldValue(strSQL)

        '--NamaKetuaModul,NamaKetuaModulBI
        strSQL = "SELECT NamaKetuaModul,NamaKetuaModulBI FROM PPCS_Course WHERE CourseID=" & Request.QueryString("courseid")
        lblNamaKetuaModul.Text = oCommon.getFieldValueEx(strSQL)

        '--ClassCode
        strSQL = "SELECT ClassCode FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
        lblPPCSClass.Text = oCommon.getFieldValue(strSQL)

        '--NamaPengajar
        strSQL = "SELECT NamaPengajar FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
        lblNamaPengajar.Text = oCommon.getFieldValue(strSQL)

        '--NamaPembantuPengajar
        strSQL = "SELECT NamaPembantuPengajar FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
        lblNamaPembantuPengajar.Text = oCommon.getFieldValue(strSQL)

        '--NamaPembantuPelajar
        strSQL = "SELECT NamaPembantuPelajar FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
        lblNamaPembantuPelajar.Text = oCommon.getFieldValue(strSQL)

        ''--load initial photo here
        imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")

    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        ''nothing to upload
        If imgUpload.FileName.Length = 0 Then
            lblMsgTop.Text = "Please select file to upload!"
            Exit Sub
        End If

        ''Studentphoto set unique studentid, photoid
        strSQL = "SELECT StudentID FROM StudentPhoto WHERE StudentID='" & Request.QueryString("studentid") & "'"
        If oCommon.isExist(strSQL) = True Then
            Studentphoto_Update()
        Else
            Studentphoto_Insert()
        End If
    End Sub

    Private Sub Studentphoto_Insert()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        ''--get unique id for photo
        Dim strPhotoID As String = oCommon.getGUID

        Try
            Dim img As FileUpload = CType(imgUpload, FileUpload)
            Dim imgByte As Byte() = Nothing
            If img.HasFile AndAlso Not img.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim File As HttpPostedFile = imgUpload.PostedFile
                'Create byte Array with file len
                imgByte = New Byte(File.ContentLength - 1) {}
                'force the control to load data in array
                File.InputStream.Read(imgByte, 0, File.ContentLength)
            End If

            ' Insert the employee name and image into db
            objConn = New SqlConnection(strConn)

            objConn.Open()
            strSQL = "INSERT INTO StudentPhoto(PhotoID,StudentID,SmallPhoto,DateCreated) VALUES(@PhotoID, @StudentID, @SmallPhoto,@DateCreated) SELECT @@IDENTITY"

            Dim cmd As SqlCommand = New SqlCommand(strSQL, objConn)
            cmd.Parameters.AddWithValue("@PhotoID", strPhotoID)
            cmd.Parameters.AddWithValue("@StudentID", Request.QueryString("studentid"))
            cmd.Parameters.AddWithValue("@SmallPhoto", imgByte)
            cmd.Parameters.AddWithValue("@DateCreated", oCommon.getNow)

            Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            lblMsgTop.Text = "Photo upload success." & String.Format("Photo ID is {0}", id)
            imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")
        Catch ex As Exception
            lblMsgTop.Text = "System Error. Contact Admin." & ex.Message
        Finally
            objConn.Close()
        End Try

    End Sub

    Private Sub Studentphoto_Update()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        ''--get unique id for photo
        Dim strPhotoID As String = oCommon.getGUID

        Try
            Dim img As FileUpload = CType(imgUpload, FileUpload)
            Dim imgByte As Byte() = Nothing
            If img.HasFile AndAlso Not img.PostedFile Is Nothing Then
                'To create a PostedFile
                Dim File As HttpPostedFile = imgUpload.PostedFile
                'Create byte Array with file len
                imgByte = New Byte(File.ContentLength - 1) {}
                'force the control to load data in array
                File.InputStream.Read(imgByte, 0, File.ContentLength)
            End If

            ' Insert the employee name and image into db
            objConn = New SqlConnection(strConn)

            objConn.Open()
            strSQL = "UPDATE StudentPhoto SET SmallPhoto=@SmallPhoto WHERE StudentID='" & Request.QueryString("studentid") & "'"

            Dim cmd As SqlCommand = New SqlCommand(strSQL, objConn)
            cmd.Parameters.AddWithValue("@SmallPhoto", imgByte)

            Dim id As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            lblMsgTop.Text = "Photo upload success. " & String.Format("Photo ID is {0}", id)
            imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")
        Catch ex As Exception
            lblMsgTop.Text = "System Error. Contact Admin. " & ex.Message
        Finally
            objConn.Close()
        End Try
    End Sub

End Class