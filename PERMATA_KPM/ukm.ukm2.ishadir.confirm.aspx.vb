Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm_ukm2_ishadir_confirm1
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnResetExamStart.Attributes.Add("onclick", "return confirm('Markah tidak akan dikosongkan. Pasti hendak Reset ExamStart Ujian UKM2 pelajar tersebut tersebut? ');")
        btnResetAll.Attributes.Add("onclick", "return confirm('Markah UKM2 akan dikosongkan. Pasti hendak Reset Ujian UKM2 pelajar tersebut?');")

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

            '--

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnHadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHadir.Click
        strSQL = "UPDATE UKM2 SET IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    Private Sub btnTidakHadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakHadir.Click
        strSQL = "UPDATE UKM2 SET IsHadir='N' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    Private Sub btnResetExamStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetExamStart.Click
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET ExamStart=NULL,ExamEnd=NULL,Status='NEW',IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Gagal mengemaskini kehadiran pelajar." & strRet
        End If

    End Sub

    Protected Sub btnResetAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetAll.Click
        lblMsg.Text = ""

        ''get pusatcode
        strSQL = "SELECT PusatCode FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strPusatCode As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT TarikhUjian FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strTarikhUjian As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SessiUKM2 FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strSessiUKM2 As String = oCommon.getFieldValue(strSQL)

        '--get schoolid
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strSchoolID = oCommon.getFieldValue(strSQL)

        ''DELETE UKM2
        strSQL = "DELETE UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "DELETE NOK:" & Request.QueryString("studentid") & strRet & vbCrLf
        End If

        ''INSERT BACK
        strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2) VALUES ('" & Request.QueryString("studentid") & "','" & ddlExamYear.Text & "','Y','" & strPusatCode & "','" & strSchoolID & "','" & strTarikhUjian & "','" & strSessiUKM2 & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "INSERT NOK:" & Request.QueryString("studentid") & strRet & vbCrLf
        End If

        '--school information
        UKM2_Update(Request.QueryString("studentid"))

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya Reset Ujian UKM2 pelajar tersebut."
        End If

    End Sub

    Private Sub UKM2_Update(ByVal strStudentID As String)
        Dim strWhere As String = ""
        Dim strOrderBy As String = ""
        Dim tmpSQL As String = ""

        Try
            ''--setup all records to generate completed records only
            Dim strSchoolID As String = ""
            Dim strSchoolState As String = ""
            Dim strSchoolCity As String = ""
            Dim strSchoolType As String = ""
            Dim strSchoolPPD As String = ""
            Dim strSchoolLokasi As String = ""

            strWhere = ""
            strOrderBy = ""
            tmpSQL = ""

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            tmpSQL = "SELECT a.StudentID,c.SchoolID,c.SchoolState,c.SchoolCity,c.SchoolType,c.SchoolPPD,c.SchoolLokasi FROM UKM2 a, StudentSchool b, SchoolProfile c"
            strWhere += " WHERE a.Studentid='" & strStudentID & "' AND a.StudentID=b.StudentID AND b.SchoolID=c.SchoolID AND a.ExamYear='" & ddlExamYear.Text & "'"
            strSQL = tmpSQL + strWhere

            Dim mySourceDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)
            myDataAdapter.Fill(mySourceDataSet, "myaccount")
            objConn.Close()

            For i As Integer = 0 To mySourceDataSet.Tables("myaccount").Rows.Count - 1
                strSchoolID = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolID")

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolState")) Then
                    strSchoolState = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolState")
                Else
                    strSchoolState = ""
                End If

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolCity")) Then
                    strSchoolCity = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolCity")
                Else
                    strSchoolCity = ""
                End If

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolType")) Then
                    strSchoolType = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolType")
                Else
                    strSchoolType = ""
                End If

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolPPD")) Then
                    strSchoolPPD = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolPPD")
                Else
                    strSchoolPPD = ""
                End If

                If Not IsDBNull(mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolLokasi")) Then
                    strSchoolLokasi = mySourceDataSet.Tables("myaccount").Rows(i).Item("SchoolLokasi")
                Else
                    strSchoolLokasi = ""
                End If

                strSQL = "UPDATE UKM2 SET SchoolID='" & strSchoolID & "',SchoolState='" & strSchoolState & "',SchoolCity='" & strSchoolCity & "',SchoolType='" & strSchoolType & "',SchoolPPD='" & strSchoolPPD & "',SchoolLokasi='" & strSchoolLokasi & "' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If Not strRet = "0" Then
                    lblMsg.Text = strRet
                End If
            Next
        Catch ex As Exception
            lblMsg.Text = "UKM2_Update:" & ex.Message
        End Try

    End Sub

End Class