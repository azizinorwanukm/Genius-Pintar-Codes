Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Data.OleDb
'Imports System.IO
'Imports System.Globalization

Partial Public Class schoolprofile_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                ''--dropdown
                schooltype_list()

                ''--display current school if any
                If schoolprofile_view() = True Then
                    lblMsg.Text = "Kemaskini maklumat sekolah."
                End If

                txtSchoolCode.Text = getSchoolCode()
                lblSchoolID.Text = oCommon.getGUID
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub schooltype_list()
        strSQL = "SELECT schooltype FROM schooltype ORDER BY schooltypeid"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolType.DataSource = ds
            ddlSchoolType.DataTextField = "schooltype"
            ddlSchoolType.DataValueField = "schooltype"
            ddlSchoolType.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getSchoolCode() As String
        Dim nCount As Integer = 0

        '--always create new schoolid
        strRet = oCommon.getRandom
        Return "XXX" & strRet

    End Function


    Private Function schoolprofile_view() As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strSchoolID = oCommon.getFieldValue(strSQL)
        If strSchoolID.Length = 0 Then
            Return False
        End If

        lblSchoolID.Text = strSchoolID

        strSQL = "SELECT * FROM SchoolProfile WHERE SchoolID='" & strSchoolID & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolName")) Then
                    txtSchoolName.Text = MyTable.Rows(nRows).Item("SchoolName").ToString
                Else
                    txtSchoolName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCode")) Then
                    txtSchoolCode.Text = MyTable.Rows(nRows).Item("SchoolCode").ToString
                Else
                    txtSchoolCode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolAddress")) Then
                    txtSchoolAddress.Text = MyTable.Rows(nRows).Item("SchoolAddress").ToString
                Else
                    txtSchoolAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolPostcode")) Then
                    txtSchoolPostcode.Text = MyTable.Rows(nRows).Item("SchoolPostcode").ToString
                Else
                    txtSchoolPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCity")) Then
                    txtSchoolCity.Text = MyTable.Rows(nRows).Item("SchoolCity").ToString
                Else
                    txtSchoolCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    selSchoolState.Value = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    selSchoolState.Value = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolNoTel")) Then
                    txtSchoolNoTel.Text = MyTable.Rows(nRows).Item("SchoolNoTel").ToString
                Else
                    txtSchoolNoTel.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolNoFax")) Then
                    txtSchoolNoFax.Text = MyTable.Rows(nRows).Item("SchoolNoFax").ToString
                Else
                    txtSchoolNoFax.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolEmail")) Then
                    txtSchoolEmail.Text = MyTable.Rows(nRows).Item("SchoolEmail").ToString
                Else
                    txtSchoolEmail.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolType")) Then
                    ddlSchoolType.Text = MyTable.Rows(nRows).Item("SchoolType").ToString
                Else
                    ddlSchoolType.Text = ""
                End If
            End If
            Return True

        Catch ex As Exception
            Return False
        Finally
            objConn.Dispose()
        End Try

    End Function

    Private Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''--always create new school
        If SchoolProfile_create() = False Then
            ''gagal memasukkan sekolah baru
            Exit Sub
        End If

        ''--create or update student schoolid
        strSQL = "SELECT SchoolID from StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
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
            lblMsg.Text = "Confirm error:" & strRet
        End If

        ''--always UKM1 schoolprofile
        ukm1_schoolprofile_update()

        ''--add into student history table
        StudentSchool_history_insert()

    End Sub

    Private Function ValidatePage() As Boolean
        If Not Mid(txtSchoolCode.Text, 1, 3) = "XXX" Then
            lblMsg.Text = "Anda tidak dibenarkan menukar maklumat sekolah yang sudah didaftarkan."
            Return False
        End If

        If txtSchoolName.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Nama Sekolah"
            txtSchoolName.Focus()
            Return False
        End If

        If txtSchoolAddress.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Alamat Sekolah"
            txtSchoolAddress.Focus()
            Return False
        End If

        If txtSchoolPostcode.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Poskod."
            txtSchoolPostcode.Focus()
            Return False
        End If

        If oCommon.isNumeric(txtSchoolPostcode.Text) = False Then
            txtSchoolPostcode.Focus()
            lblMsg.Text = "!Nombor sahaja. 0 - 9"
            Return False
        End If

        If txtSchoolCity.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Bandar."
            txtSchoolCity.Focus()
            Return False
        End If

        If selSchoolState.Value = "" Then
            lblMsg.Text = "!Sila pilih Negeri."
            selSchoolState.Focus()
            Return False
        End If

        'If ddlSchoolType.Text = "" Then
        '    lblMsg.Text = "!Sila pilih Jenis Sekolah."
        '    ddlSchoolType.Focus()
        '    Return False
        'End If

        ' ''Mengesahkan Maklumat Sekolah Baru
        'If selStartDate_day.Value = "" Then
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If
        'If selStartDate_month.Value = "" Then
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If
        'If selStartDate_year.Value = "" Then
        '    lblMsg.Text = "Sila masukkan tarikh mula sekolah baru."
        '    Return False
        'End If

        Return True
    End Function

    Private Function SchoolProfile_create() As Boolean
        ''refresh again to get latest only
        txtSchoolCode.Text = getSchoolCode()

        strSQL = "INSERT INTO SchoolProfile (SchoolID,SchoolCode,SchoolName,SchoolAddress,SchoolPostcode,SchoolCity,SchoolState,SchoolNoTel,SchoolNoFax,SchoolEmail,SchoolType) VALUES('" & lblSchoolID.Text & "','" & txtSchoolCode.Text & "','" & oCommon.FixSingleQuotes(txtSchoolName.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtSchoolAddress.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtSchoolPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtSchoolCity.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(selSchoolState.Value) & "','" & oCommon.FixSingleQuotes(txtSchoolNoTel.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtSchoolNoFax.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtSchoolEmail.Text) & "','" & oCommon.FixSingleQuotes(ddlSchoolType.Text) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "SchoolProfile_create Error:" & strRet
            Return False
        End If

    End Function

    Private Function SchoolProfile_update() As Boolean
        strSQL = "UPDATE SchoolProfile SET SchoolName='" & oCommon.FixSingleQuotes(txtSchoolName.Text) & "',SchoolAddress='" & oCommon.FixSingleQuotes(txtSchoolAddress.Text) & "',SchoolPostcode='" & oCommon.FixSingleQuotes(txtSchoolPostcode.Text) & "',SchoolCity='" & oCommon.FixSingleQuotes(txtSchoolCity.Text) & "',SchoolState='" & oCommon.FixSingleQuotes(selSchoolState.Value) & "',SchoolType='" & oCommon.FixSingleQuotes(ddlSchoolType.Text) & "',SchoolNoTel='" & oCommon.FixSingleQuotes(txtSchoolNoTel.Text) & "',SchoolNoFax='" & oCommon.FixSingleQuotes(txtSchoolNoFax.Text) & "',SchoolEmail='" & oCommon.FixSingleQuotes(txtSchoolEmail.Text) & "' WHERE SchoolID='" & lblSchoolID.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "Error:" & strRet
            Return False
        End If

    End Function

    Private Function StudentSchool_update() As String
        Dim strStartDate As String = "NA"
        Dim strEndDate As String = "NA"

        strSQL = "UPDATE StudentSchool SET SchoolID='" & lblSchoolID.Text & "',StartDate='" & strStartDate & "',EndDate='" & strEndDate & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function

    Private Function ukm1_schoolprofile_update() As Boolean
        ''--get schoolID base on studentid
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "' ORDER BY StudentSchoolID DESC"
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

        Dim ukm1Table As String = Common.getUKM1Table(Now.Year)

        strSQL = ""
        ''update UKM1
        If Not ukm1Table = "UKM1" Then
            strSQL = " UPDATE " & ukm1Table & " SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & Now.Year & "' "
        End If

        strSQL += " UPDATE UKM1 SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & Now.Year & "'"
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

        Dim strStartDate As String = "NA"
        Dim strEndDate As String = "NA"

        strSQL = "INSERT INTO StudentSchool (StudentID,SchoolID,StartDate,EndDate,CreatedDate) VALUES ('" & Request.QueryString("studentid") & "','" & lblSchoolID.Text & "','" & strStartDate & "','" & strEndDate & "','" & oCommon.getNow & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("StudentSchool_insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

        Return strRet
    End Function

    Private Function StudentSchool_history_insert() As String
        'Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & selStartDate_year.Value
        'Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & selEndDate_year.Value

        Dim strStartDate As String = "NA"
        Dim strEndDate As String = "NA"

        strSQL = "INSERT INTO StudentSchool_History (StudentID,SchoolID,StartDate,EndDate,CreatedDate) VALUES ('" & Request.QueryString("studentid") & "','" & lblSchoolID.Text & "','" & strStartDate & "','" & strEndDate & "','" & oCommon.getNow & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("StudentSchool_history_insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress)

        Return strRet
    End Function

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click
        Response.Redirect("default.main.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))

    End Sub
End Class