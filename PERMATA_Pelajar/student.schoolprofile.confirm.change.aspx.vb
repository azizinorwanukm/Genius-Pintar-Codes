Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class student_schoolprofile_confirm_change
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;")
        lblLog.Text = ""

    End Sub

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If ValidatePage() = False Then
                Exit Sub
            End If

            ''--new school base on todays date
            strRet = StudentSchool_insert()
            If strRet = "0" Then
                '--make others non-active
                strSQL = "UPDATE StudentSchool SET IsLatest='N' WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND SchoolID <>'" & Request.QueryString("schoolid") & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ''--update UKM1 schoolprofile
                ukm1_schoolprofile_update()
                ''--update UKM2 schoolprofile
                ukm2_schoolprofile_update()
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

    Private Function StudentSchool_update() As String
        'Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & selStartDate_year.Value
        'Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & selEndDate_year.Value
        Dim strStartDate As String = "NA"
        Dim strEndDate As String = "NA"

        ''--backup 
        strSQL = "SELECT * FROM StudentSchool WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentSchool_History")

        ''update studentschool
        strSQL = "UPDATE StudentSchool WITH (UPDLOCK) SET SchoolID='" & Request.QueryString("schoolid") & "',StartDate='" & strStartDate & "',EndDate='" & strEndDate & "' WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND IsLatest='Y'"
        strRet = oCommon.ExecuteSQL(strSQL)

        '--log
        lblLog.Text += "StudentSchool_update:" & strRet & "<br />"

        Return strRet
    End Function

    '--UKM1
    Private Function ukm1_schoolprofile_update() As Boolean
        ''--get schoolID base on studentid
        strSQL = "SELECT SchoolID FROM StudentSchool WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' ORDER BY StudentSchoolID DESC"
        Dim strSchoolID As String = oCommon.getFieldValue(strSQL)

        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        ''--debug
        ''Response.Write("ukm1_schoolprofile_update:" & strRet)

        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        ''update UKM1
        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Now.Year & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "UPDATE UKM1 error:" & strRet
            Return False
        End If

        '--log
        lblLog.Text += "ukm1_schoolprofile_update:" & strRet & "<br />"

    End Function

    '--UKM2
    Private Function ukm2_schoolprofile_update() As Boolean
        ''--get schoolID base on studentid
        strSQL = "SELECT SchoolID FROM StudentSchool WITH (NOLOCK) WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' ORDER BY StudentSchoolID DESC"
        Dim strSchoolID As String = oCommon.getFieldValue(strSQL)

        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        ''--debug
        ''Response.Write("ukm1_schoolprofile_update:" & strRet)

        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        ''update UKM2
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' AND ExamYear='" & Now.Year & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "UPDATE UKM2 error:" & strRet
            Return False
        End If

        '--log
        lblLog.Text += "ukm2_schoolprofile_update:" & strRet & "<br />"

    End Function

    Private Function StudentSchool_insert() As String
        'Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & selStartDate_year.Value
        'Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & selEndDate_year.Value

        Dim strStartDate As String = oCommon.getDisplayToday
        Dim strEndDate As String = "NA"

        strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,StartDate,EndDate,CreatedDate,IsLatest) VALUES ('" & CType(Session.Item("permata_studentid"), String) & "','" & Request.QueryString("schoolid") & "','" & strStartDate & "','" & strEndDate & "','" & oCommon.getNow & "','Y')"
        strRet = oCommon.ExecuteSQL(strSQL)

        '--log
        lblLog.Text += "StudentSchool_insert:" & strRet & "<br />"

        ''log
        oCommon.TransactionLog("studentschool_insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress, CType(Session.Item("permata_mykad"), String))

        Return strRet
    End Function

    Protected Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("student.studentprofile.view.aspx")

    End Sub

End Class