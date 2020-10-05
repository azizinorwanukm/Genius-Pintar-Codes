Imports System.Data.SqlClient

Partial Public Class schoolprofile_confirm_change
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''btnBack.Attributes.Add("onClick", "javascript:history.back(); return false;")
        lblLog.Text = ""

        Try
            If Not IsPostBack Then
                master_Dobyear_list()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub master_Dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear WITH (NOLOCK) ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--ddlStartDate_day
            ddlStartDate_year.DataSource = ds
            ddlStartDate_year.DataTextField = "DOB_Year"
            ddlStartDate_year.DataValueField = "DOB_Year"
            ddlStartDate_year.DataBind()

            '--ddlEndDate_year
            ddlEndDate_year.DataSource = ds
            ddlEndDate_year.DataTextField = "DOB_Year"
            ddlEndDate_year.DataValueField = "DOB_Year"
            ddlEndDate_year.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If ValidatePage() = False Then
                Exit Sub
            End If

            strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
            If oCommon.isExist(strSQL) = True Then
                ''--update existing school
                strRet = StudentSchool_update()
            Else
                ''--new school
                strRet = StudentSchool_insert()
            End If
            If strRet = "0" Then
                lblMsg.Text = "Berjaya mengemaskini maklumat sekolah anda."
            Else
                lblMsg.Text = strRet
            End If

            '--update UKM1
            ukm1_schoolprofile_update()

            '--update UKM2
            ukm2_schoolprofile_update()

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    '--UKM1
    Private Function ukm1_schoolprofile_update() As Boolean
        ''--get schoolID base on studentid
        strSQL = "SELECT SchoolID FROM StudentSchool WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' ORDER BY StudentSchoolID DESC"
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

        strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & Now.Year & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & Now.Year & "'"
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
        strSQL = "SELECT SchoolID FROM StudentSchool WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' ORDER BY StudentSchoolID DESC"
        Dim strSchoolID As String = oCommon.getFieldValue(strSQL)

        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        ''update UKM2
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & Now.Year & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "UPDATE UKM2 error:" & strRet
            Return False
        End If

        '--log
        lblLog.Text += "ukm2_schoolprofile_update:" & strRet & "<br />"

    End Function


    Private Function ValidatePage() As Boolean

        ''Dr Siti request 20120229 to remove this

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
        'If ddlStartDate_year.Text = "" Then
        '    divMsg.Attributes("class") = "error"
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If

        Return True
    End Function

    Private Function StudentSchool_update() As String
        Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & ddlStartDate_year.Text
        Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & ddlEndDate_year.Text

        ''--backup 
        strSQL = "SELECT * FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.Bulk_Transfer(strSQL, "StudentSchool_History")

        ''update studentschool
        'strSQL = "UPDATE StudentSchool WITH (UPDLOCK) SET SchoolID='" & Request.QueryString("schoolid") & "',StartDate='" & strStartDate & "',EndDate='" & strEndDate & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        'strRet = oCommon.ExecuteSQL(strSQL)

        '--reset N before set Y for new
        setStudentSchool_N(Request.QueryString("studentid"))

        '--INSERT StudentSchool
        strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,StartDate,EndDate,CreatedDate,IsLatest) VALUES ('" & Request.QueryString("studentid") & "','" & Request.QueryString("schoolid") & "','" & strStartDate & "','" & strEndDate & "','" & oCommon.getNow & "','Y')"
        strRet = oCommon.ExecuteSQL(strSQL)

        '--log
        lblLog.Text += "StudentSchool_update:" & strRet & "<br />"
        Return strRet
    End Function

    Private Function ukm1ukm2_schoolprofile_update() As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD FROM SchoolProfile WHERE SchoolID='" & Request.QueryString("schoolid") & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 5 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        '--update UKM1
        strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "system error:" & strRet
            Return False
        End If

        '--update UKM2
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "system error:" & strRet
            Return False
        End If

    End Function

    Private Sub setStudentSchool_N(ByVal strStudentID As String)
        strSQL = "UPDATE StudentSchool SET IsLatest='N' WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub


    Private Function StudentSchool_insert() As String
        Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & ddlStartDate_year.Text
        Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & ddlEndDate_year.Text

        '--reset N before set Y for new
        setStudentSchool_N(Request.QueryString("studentid"))

        '--INSERT StudentSchool
        strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,StartDate,EndDate,IsLatest,CreatedDate) VALUES ('" & Request.QueryString("studentid") & "','" & Request.QueryString("schoolid") & "','" & strStartDate & "','" & strEndDate & "','Y','" & oCommon.getNow & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''--get SchoolProfile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD FROM SchoolProfile WHERE SchoolID='" & Request.QueryString("schoolid") & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 5 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        '--update UKM1

        strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "system error:" & strRet
            Return False
        End If

        '--update UKM2
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "system error:" & strRet
            Return False
        End If

        '--log
        lblLog.Text += "StudentSchool_insert:" & strRet & "<br />"

        '--dblog
        oCommon.TransactionLog("schoolprofile.confirm.change", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress, CType(Session.Item("permata_admin"), String))

        Return strRet
    End Function

    Private Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("admin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub
End Class