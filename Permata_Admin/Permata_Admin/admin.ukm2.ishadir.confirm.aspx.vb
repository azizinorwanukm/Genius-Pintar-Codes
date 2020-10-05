Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class admin_ukm2_ishadir_confirm
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnResetExamStart.Attributes.Add("onclick", "return confirm('Markah tidak akan dikosongkan. Pasti ingin Reset ExamStart Ujian UKM2 pelajar tersebut tersebut? ');")
        btnResetAll.Attributes.Add("onclick", "return confirm('Markah UKM2 akan dikosongkan. Pasti ingin Reset Ujian UKM2 pelajar tersebut?');")

        Try
            If Not IsPostBack Then
                examyear_list()
                ddlExamYear.Text = Request.QueryString("examyear")

                setAccessRight()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub setAccessRight()

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                btnResetExamStart.Visible = True
            Case "SUBADMIN"
                btnResetExamStart.Visible = False
            Case Else
                btnResetExamStart.Visible = False
                lblMsg.Text = "Invalid usertype!"
        End Select

    End Sub

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "ADMINOP"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case "UKM"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & oCommon.getAppsettings("DefaultExamYear") & "%'  ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
        End Select

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "ExamYear"
            ddlExamYear.DataBind()

            ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnHadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHadir.Click
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    Private Sub btnTidakHadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakHadir.Click
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='N' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    Private Sub btnResetExamStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetExamStart.Click
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET ExamStart=NULL,ExamEnd=NULL,Status='NEW',IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    Protected Sub btnResetAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetAll.Click
        Dim strStudentID As String = Request.QueryString("studentid")
        lblMsg.Text = ""

        ''get pusatcode
        strSQL = "SELECT PusatCode FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strPusatCode As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT TarikhUjian FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strTarikhUjian As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SessiUKM2 FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strSessiUKM2 As String = oCommon.getFieldValue(strSQL)

        '--get schoolid
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
        strSchoolID = oCommon.getFieldValue(strSQL)

        ''DELETE UKM2
        strSQL = "DELETE UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "DELETE NOK:" & strStudentID & strRet & vbCrLf
        End If

        '--set default value AdditionalMinute
        Dim strAdditionalMinute As String = oCommon.getAppsettings("SaringanDuration")

        ''INSERT
        strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2,Status,AdditionalMinute) VALUES ('" & strStudentID & "','" & ddlExamYear.Text & "','Y','" & strPusatCode & "','" & strSchoolID & "','" & strTarikhUjian & "','" & strSessiUKM2 & "','NEW'," & strAdditionalMinute & ")"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "INSERT NOK:" & strStudentID & strRet & vbCrLf
        End If

        '--update UKM2 report information
        oCommon.UKM2_StudentprofileUpdate(strStudentID, ddlExamYear.Text)
        oCommon.UKM2_SchoolprofileUpdate(strStudentID, ddlExamYear.Text)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya Reset Ujian UKM2 pelajar tersebut."
        End If

    End Sub

End Class