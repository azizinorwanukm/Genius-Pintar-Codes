Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Data.OleDb
'Imports System.IO
'Imports System.Globalization

Partial Public Class schoolprofile_confirm_change
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;")
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If ValidatePage() = False Then
                Exit Sub
            End If

            ''--new school base on todays date
            strRet = StudentSchool_insert()
            If strRet = "0" Then

                ''--update UKM1 schoolprofile
                ukm1_schoolprofile_update()
                lblMsg.Text = "Berjaya mengemaskini maklumat sekolah anda."
            Else
                lblMsg.Text = strRet
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Function ValidatePage() As Boolean

        'If selStartDate_day.Value = "" Then
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If
        'If selStartDate_month.Value = "" Then
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If
        'If selStartDate_year.Value = "" Then
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If

        Return True
    End Function

    'not used
    Private Function StudentSchool_update() As String
        'Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & selStartDate_year.Value
        'Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & selEndDate_year.Value

        Dim strStartDate As String = oCommon.getDisplayToday
        Dim strEndDate As String = "NA"

        ''update studentschool
        strSQL = "UPDATE StudentSchool SET SchoolID='" & Request.QueryString("schoolid") & "',StartDate='" & strStartDate & "',EndDate='" & strEndDate & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function

    Private Function ukm1_schoolprofile_update() As Boolean

        Dim studentID As String = Request.QueryString("studentid")
        Dim examYear As String = oCommon.getAppsettings("DefaultExamYear")
        Dim examYearID As String = Common.getDefaultExamYearID()

        Dim ukm1Table As String = Common.getUKM1Table(examYear)

        strSQL = ""

        'update UKM1
        If Not ukm1Table = "UKM1" Then
            strSQL = "UPDATE " & ukm1Table & " SET SchoolID = Origin.SchoolID, SchoolState = Origin.SchoolState, SchoolCity = Origin.SchoolCity, SchoolType = Origin.SchoolType, SchoolPPD = Origin.SchoolPPD, SchoolLokasi = Origin.SchoolLokasi "
            strSQL += " FROM (SELECT TOP 1 A.SchoolID, SchoolState, SchoolCity, SchoolType, SchoolPPD, SchoolLokasi FROM StudentSchool A LEFT JOIN SchoolProfile B ON A.SchoolID = B.SchoolID WHERE A.StudentID = '" & studentID & "' AND A.IsLatest = 'Y') as Origin "
            strSQL += " WHERE StudentID = '" & studentID & "' AND examyear_id = " & examYearID
            'oCommon.ExecuteSQL(strSQL)

        End If

        strSQL += " UPDATE UKM1 SET SchoolID = Origin.SchoolID, SchoolState = Origin.SchoolState, SchoolCity = Origin.SchoolCity, SchoolType = Origin.SchoolType, SchoolPPD = Origin.SchoolPPD, SchoolLokasi = Origin.SchoolLokasi "
        strSQL += " FROM (SELECT TOP 1 A.SchoolID, SchoolState, SchoolCity, SchoolType, SchoolPPD, SchoolLokasi FROM StudentSchool A LEFT JOIN SchoolProfile B ON A.SchoolID = B.SchoolID WHERE A.StudentID = '" & studentID & "' AND A.IsLatest = 'Y') as Origin "
        strSQL += " WHERE StudentID = '" & studentID & "' AND examyear_id = " & examYearID

        'Debug.WriteLine(strSQL)

        strRet = oCommon.ExecuteSQL(strSQL)

        If Not strRet = "0" Then
            lblMsg.Text = "UPDATE UKM1 error:" & strRet
            Return False
        End If

    End Function

    Private Function StudentSchool_insert() As String
        'Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & selStartDate_year.Value
        'Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & selEndDate_year.Value

        Dim strStartDate As String = oCommon.getDisplayToday
        Dim strEndDate As String = "NA"
        Dim studentID As String = Request.QueryString("studentid")
        Dim schoolID As String = Request.QueryString("schoolid")

        Dim isSchoolLatest As String = "SELECT StudentSchoolID FROM StudentSchool WHERE StudentID = '" & studentID & "' AND IsLatest = 'Y' AND SchoolID = '" & schoolID & "'"

        strSQL = "UPDATE StudentSchool SET IsLatest = 'N' WHERE StudentID = '" & studentID & "' "

        If oCommon.isExist(isSchoolLatest) Then
            Dim studentSchoolID As String = oCommon.getFieldValue(isSchoolLatest)
            strSQL += "UPDATE StudentSchool SET EndDate = 'NA', IsLatest = 'Y' WHERE StudentSchoolID = " & studentSchoolID
        Else
            strSQL += "INSERT INTO StudentSchool (StudentID,SchoolID,StartDate,EndDate,CreatedDate,IsLatest) VALUES ('" & studentID & "','" & schoolID & "','" & strStartDate & "','NA','" & oCommon.getNow & "','Y')"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)
        oCommon.TransactionLog("StudentSchool_insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

        Return strRet
    End Function

    Private Sub lnkViewProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkViewProfile.Click
        Response.Redirect("default.main.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"), False)

    End Sub

End Class