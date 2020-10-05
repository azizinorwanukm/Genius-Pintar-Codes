Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class subadmin_studentschool_schoolprofile_confirm
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnStudentSchoolUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStudentSchoolUpdate.Click

        Try
            ''refresh message color
            divMsg.Attributes("class") = "info"

            ''update studentschool
            strSQL = "UPDATE StudentSchool WITH (UPDLOCK) SET SchoolID='" & Request.QueryString("schoolid") & "' WHERE SchoolID='" & Request.QueryString("oldschoolid") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            ''debug
            'Response.Write(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Berjaya memindahkan semua pelajar ke sekolah baru."
            Else
                lblMsg.Text = "Gagal memindahkan semua pelajar ke sekolah baru. " & strRet
            End If

            ''update isdeleted only for XXX schoolcode
            strSQL = "SELECT SchoolCode FROM SchoolProfile WHERE SchoolID='" & Request.QueryString("oldschoolid") & "'"
            strRet = oCommon.getFieldValue(strSQL)
            If strRet.Length > 3 Then
                Dim strTemp As String = strRet.Substring(0, 3)
                If strTemp = "XXX" Then
                    ''update SchoolProfile isDeleted='Y'
                    strSQL = "UPDATE SchoolProfile WITH (UPDLOCK) SET IsDeleted='Y' WHERE SchoolID='" & Request.QueryString("oldschoolid") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
                ''debug
                'Response.Write("SchoolCode:" & strTemp)
            End If

            ''audit trail
            UKM1_History_insert()
            '--update UKM1 to new schoolid profile
            ukm1_schoolprofile_update()


            ''audit trail
            UKM2_History_insert()
            '--update UKM2 to new schoolid profile
            ukm2_schoolprofile_update()

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = ex.Message
        End Try

    End Sub

    '--UKM1
    Private Sub UKM1_History_insert()
        ' Create source connection
        Dim source As New SqlConnection(strConn)
        ' Create destination connection
        Dim destination As New SqlConnection(strConn)
        ' Clean up destination table. Your destination database must have the 
        ' table with schema which you are copying data to. 
        ' Before executing this code, you must create a table BulkDataTable 
        ' in your database where you are trying to copy data to.
        Dim cmd As New SqlCommand("DELETE FROM UKM1_History WHERE SchoolID=''", destination)    ''dont have to cleanup
        ' Open source and destination connections.
        source.Open()
        destination.Open()
        cmd.ExecuteNonQuery()

        ' Select data from Products table
        cmd = New SqlCommand("SELECT * FROM UKM1 WHERE Schoolid='" & Request.QueryString("oldschoolid") & "' AND Examyear='" & Request.QueryString("examyear") & "'", source)
        ' Execute reader
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        ' Create SqlBulkCopy
        Dim bulkData As New SqlBulkCopy(destination)
        ' Set destination table name
        bulkData.DestinationTableName = "UKM1_History"
        ' Write data
        bulkData.WriteToServer(reader)
        ' Close objects
        bulkData.Close()
        destination.Close()
        source.Close()

    End Sub

    '--UKM2
    Private Sub UKM2_History_insert()
        ' Create source connection
        Dim source As New SqlConnection(strConn)
        ' Create destination connection
        Dim destination As New SqlConnection(strConn)
        ' Clean up destination table. Your destination database must have the 
        ' table with schema which you are copying data to. 
        ' Before executing this code, you must create a table BulkDataTable 
        ' in your database where you are trying to copy data to.
        Dim cmd As New SqlCommand("DELETE FROM UKM2_History WHERE SchoolID=''", destination)    ''dont have to cleanup
        ' Open source and destination connections.
        source.Open()
        destination.Open()
        cmd.ExecuteNonQuery()

        ' Select data from Products table
        cmd = New SqlCommand("SELECT * FROM UKM2 WHERE Schoolid='" & Request.QueryString("oldschoolid") & "' AND Examyear='" & Request.QueryString("examyear") & "'", source)
        ' Execute reader
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        ' Create SqlBulkCopy
        Dim bulkData As New SqlBulkCopy(destination)
        ' Set destination table name
        bulkData.DestinationTableName = "UKM2_History"
        ' Write data
        bulkData.WriteToServer(reader)
        ' Close objects
        bulkData.Close()
        destination.Close()
        source.Close()

    End Sub

    '--UKM1
    Private Function ukm1_schoolprofile_update() As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & Request.QueryString("schoolid") & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        ''update UKM1 to new schoolid profile

        If Request.QueryString("examyear") = getUKM1Year Then
            strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE schoolid='" & Request.QueryString("oldschoolid") & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE schoolid='" & Request.QueryString("oldschoolid") & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "system error:" & strRet
            Return False
        End If

    End Function

    '--UKM2
    Private Function ukm2_schoolprofile_update() As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & Request.QueryString("schoolid") & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        ''update UKM2 to new schoolid profile
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE schoolid='" & Request.QueryString("oldschoolid") & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "system error:" & strRet
            Return False
        End If

    End Function

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        ''reload page
        Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub

End Class