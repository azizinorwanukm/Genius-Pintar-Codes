Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Data.OleDb
'Imports System.IO
'Imports System.Globalization

Public Class ukm2_kodsekolah_summary
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        LblNo.Text = 1
        Dim year As String = Now.Year

        ''get schoolname
        strSQL = "select schoolName from SchoolProfile where SchoolCode='" & txtKodSekolah.Text & "'"
        Dim School As String = oCommon.getFieldValue(strSQL)
        LblSchoolName.Text = School

        ''get schoolstate
        strSQL = "select SchoolState from SchoolProfile where SchoolCode='" & txtKodSekolah.Text & "'"
        Dim State As String = oCommon.getFieldValue(strSQL)
        LblNegeri.Text = State

        ''get schoolstate
        strSQL = "select
        count(*)
        from ukm1 a
        left join SchoolProfile b on a. SchoolID=b. SchoolID
        where schoolcode='" & txtKodSekolah.Text & "'
        and examyear_id = " & Common.getExamYearID(year)

        If chkRuleAge.Checked Then
            strSQL += " and IsCount='1' "
        End If

        Dim total As String = oCommon.getFieldValue(strSQL)
        Lbljumlah.Text = total

        'checkTerimaSijil()

        BindData(datRespondent)



    End Sub

    'Private Sub checkTerimaSijil()

    '    Dim ukm1Table As String = Common.getUKM1Table(oCommon.getAppsettings("UKM1ExamYear"))

    '    'get StudentID
    '    strSQL = "  SELECT StudentID 
    '                FROM " & ukm1Table & ""
    '    '            LEFT JOIN StudentProfile ON StudentProfile.StudentID = " & ukm1Table & ".StudentID 
    '    '            LEFT JOIN SchoolProfile ON SchoolProfile.SchoolID = " & ukm1Table & ".SchoolID
    '    '            WHERE SchoolProfile.SchoolCode = '" & txtKodSekolah.Text & "'
    '    '            AND " & ukm1Table & ".examyear_id = '" & Common.getExamYearID(Now.Year) & "'"

    '    'If chkRuleAge.Checked Then
    '    '    strSQL += " AND " & ukm1Table & ".IsCount='1' "
    '    'End If

    '    'If Not txtFilterNamaPelajar.Text = "" Then
    '    '    strSQL += " AND StudentProfile.StudentFullname LIKE '%" & txtFilterNamaPelajar.Text & "%'"
    '    'End If

    '    'strSQL += " ORDER BY StudentProfile.StudentFullname"

    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Dim ds As DataSet = New DataSet
    '    sqlDA.Fill(ds, "AnyTable")

    '    Dim nCount As Integer = 1
    '    Dim MyTable As DataTable = New DataTable
    '    MyTable = ds.Tables(0)
    '    Dim numrows As Integer

    '    numrows = MyTable.Rows.Count

    '    If numrows > 0 Then

    '        For i = 0 To numrows - 1

    '            Dim strStudentID As String = ds.Tables(0).Rows(i).Item("StudentID")

    '            strSQL = "SELECT DOB_Year FROM StudentProfile WHERE StudentID = '" & strStudentID & "'"
    '            Dim StudentDOB_Year As String = oCommon.getFieldValue(strSQL)

    '            strSQL = "SELECT ExamYear FROM master_Examyear WHERE examyearid = '" & Common.getExamYearID(Now.Year) & "'"
    '            Dim strExamYear As String = oCommon.getFieldValue(strSQL)

    '            Dim StudentAge As Integer = Integer.Parse(strExamYear) - Integer.Parse(StudentDOB_Year)

    '            strSQL = "SELECT UKM1ID FROM " & ukm1Table & " WHERE StudentID = '" & strStudentID & "' AND examyear_id = '" & Common.getExamYearID(Now.Year) & "'"
    '            Dim strUKM1ID As String = oCommon.getFieldValue(strSQL)

    '            strSQL = "SELECT isLayakSijil FROM " & ukm1Table & " WHERE UKM1ID = '" & strUKM1ID & "'"
    '            Dim strIsLayakSijil As String = oCommon.getFieldValue(strSQL)

    '            If StudentAge >= 8 And StudentAge <= 15 Then

    '                strSQL = "SELECT TotalPercentage FROM " & ukm1Table & " WHERE UKM1ID = '" & strUKM1ID & "'"
    '                Dim StudentTotalPercentage As Double = oCommon.getFieldValue(strSQL)

    '                strSQL = "SELECT configString FROM master_Config WHERE configCode = 'MinMarkAge" & StudentAge & "'"
    '                Dim strMinMark As Double = oCommon.getFieldValue(strSQL)

    '                If StudentTotalPercentage >= strMinMark Then

    '                    If Not strIsLayakSijil = "Y" Then

    '                        strSQL = "UPDATE " & ukm1Table & " SET isLayakSijil = 'Y' WHERE UKM1ID = '" & strUKM1ID & "'"
    '                        strRet = oCommon.ExecuteSQL(strSQL)

    '                    End If

    '                Else

    '                    If Not strIsLayakSijil = "N" Then

    '                        strSQL = "UPDATE " & ukm1Table & " SET isLayakSijil = 'N' WHERE UKM1ID = '" & strUKM1ID & "'"
    '                        strRet = oCommon.ExecuteSQL(strSQL)

    '                    End If

    '                End If

    '            Else

    '                If Not strIsLayakSijil = "N" Then

    '                    strSQL = "UPDATE " & ukm1Table & " SET isLayakSijil = 'N' WHERE UKM1ID = '" & strUKM1ID & "'"
    '                    strRet = oCommon.ExecuteSQL(strSQL)

    '                End If

    '            End If

    '        Next

    '    End If

    'End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try
        Return True

    End Function


    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function getSQL() As String

        'Dim ukm1Table As String = Common.getUKM1Table(oCommon.getAppsettings("UKM1ExamYear"))

        Dim tmpSQL As String

        Try

            tmpSQL = "  SELECT 
                        StudentProfile.StudentID, 
                        StudentProfile.StudentFullname,
                        StudentProfile.DOB_Year,
                        SchoolProfile.SchoolCode, 
                        CASE WHEN UKM1.Status = 'NEW' THEN 'BELUM TAMAT' ELSE 'TAMAT' END AS 'Status',
                        CASE WHEN UKM1.isLayakSijil = 'Y' THEN 'YA' ELSE 'TIDAK' END AS 'isLayakSijil'
                        FROM UKM1 
                        LEFT JOIN StudentProfile ON StudentProfile.StudentID = UKM1.StudentID 
                        LEFT JOIN SchoolProfile ON SchoolProfile.SchoolID = UKM1.SchoolID
                        WHERE SchoolProfile.SchoolCode = '" & txtKodSekolah.Text & "'
                        AND UKM1.examyear_id = '" & Common.getExamYearID(Now.Year) & "'"

            If chkRuleAge.Checked Then
                tmpSQL += " AND UKM1.IsCount='1' "
            End If

            If Not txtFilterNamaPelajar.Text = "" Then
                tmpSQL += " AND StudentProfile.StudentFullname LIKE '%" & txtFilterNamaPelajar.Text & "%'"
            End If

            tmpSQL += " ORDER BY StudentProfile.StudentFullname"

            getSQL = tmpSQL

            ''--debug
            'Response.Write(getSQL)
            Debug.WriteLine(getSQL)
            Return getSQL

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ExportToCSV()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV()
        'Get the data from database into datatable 
        Dim strQuery As String = getSQL()
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileExport.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

End Class