Imports System.Data.SqlClient

Partial Public Class ukm2_kelayakan_umur_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                master_dobyear_list()
                If Request.QueryString("dob_year") = "" Then
                    ddlDOB_Year.Text = "ALL"
                Else
                    ddlDOB_Year.Text = Request.QueryString("dob_year")
                End If

                '-list of exam avail
                examyear_list()
                ddlExamYear.Text = Request.QueryString("examyear")

                '--list all religion
                StudentReligion_list()
                ddlStudentReligion.Text = "ALL"

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub master_dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDOB_Year.DataSource = ds
            ddlDOB_Year.DataTextField = "DOB_Year"
            ddlDOB_Year.DataValueField = "DOB_Year"
            ddlDOB_Year.DataBind()

            ddlDOB_Year.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

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

            '--ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub StudentReligion_list()
        strSQL = "SELECT StudentReligion FROM master_StudentReligion ORDER BY StudentReligion"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStudentReligion.DataSource = ds
            ddlStudentReligion.DataTextField = "StudentReligion"
            ddlStudentReligion.DataValueField = "StudentReligion"
            ddlStudentReligion.DataBind()

            ddlStudentReligion.Items.Add(New ListItem("ALL", "ALL"))

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai."
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

    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strLayak As String = "(SELECT IsLayak FROM UKM2 WHERE UKM2.StudentID=UKM1.StudentID AND ExamYear='" & ddlExamYear.Text & "') AS isLayak"
        Dim strOrder As String = ""

        'tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentType,UKM1.Status,UKM1.TotalScore,UKM1.ExamStart,UKM1.ExamEnd,UKM1.QuestionYear,UKM1.ModA,UKM1.ModB,UKM1.ModC,UKM1.TotalPercentage,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,SchoolProfile.SchoolAddress,SchoolProfile.SchoolPostcode,SchoolProfile.SchoolCity,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolType,SchoolProfile.SchoolNoTel,SchoolProfile.SchoolNoFax,SchoolProfile.SchoolLokasi," & strLayak & " FROM UKM1"
        'tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM1.StudentID=StudentProfile.StudentID"
        'tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM1.StudentID=ParentProfile.StudentID"
        'tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM1.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        'tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        'strWhere = " WHERE UKM1.ExamYear='" & ddlExamYear.Text & "'"

        tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentType,UKM1.Status,UKM1.TotalScore,UKM1.ExamStart,UKM1.ExamEnd,UKM1.QuestionYear,UKM1.ModA,UKM1.ModB,UKM1.ModC,UKM1.TotalPercentage,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,SchoolProfile.SchoolAddress,SchoolProfile.SchoolPostcode,SchoolProfile.SchoolCity,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolType,SchoolProfile.SchoolNoTel,SchoolProfile.SchoolNoFax,SchoolProfile.SchoolLokasi,UKM2.IsLayak,UKM1.flag FROM UKM1 WITH(NOLOCK)"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM1.StudentID=StudentProfile.StudentID "
        tmpSQL += " LEFT OUTER JOIN UKM2 ON UKM2.StudentID=UKM1.StudentID AND UKM2.ExamYear='" & ddlExamYear.Text & "'"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM1.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM1.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        tmpSQL += " WHERE UKM1.ExamYear='" & ddlExamYear.Text & "'"

        '--iscount=True
        If Request.QueryString("iscount") = "True" Then
            strWhere += " AND UKM1.IsCount=1"
        End If

        '--DOB_Year. UKM1 to make it tally
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND UKM1.DOB_Year='" & ddlDOB_Year.Text & "'"
        End If

        '--StudentReligion
        If Not ddlStudentReligion.Text = "ALL" Then
            strWhere += " AND StudentProfile.StudentReligion='" & ddlStudentReligion.Text & "'"
        End If

        '--AlumniID
        If chkAlumni.Checked = True Then
            strWhere += " AND StudentProfile.AlumniID IS NOT NULL"
        End If

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        '--flag
        If Not flag.Value = "ALL" Then
            strWhere += " AND UKM1.flag='" & flag.Value & "'"
        End If

        strOrder = " ORDER BY TotalScore DESC"
        getSQL = tmpSQL & strWhere & strOrder

        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim lblLayak As Label

        '    Dim i As Integer = e.Row.RowIndex + 1
        '    Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

        '    lblLayak = e.Row.FindControl("lblLayak")
        '    lblLayak.Text = isLayak(strKeyID)

        'End If

    End Sub

    Private Function isLayak(ByVal strStudentID As String) As String
        strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            Return "Y"
        Else
            Return "N"
        End If

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLayak.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString

                    '--check duplicate
                    strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & ddlExamYear.Text & "'"
                    If oCommon.isExist(strSQL) = False Then
                        If setLayak(strID) = True Then
                            lblMsg.Text = "Berjaya melayakkan semua pelajar yang dipilih."
                        Else
                            lblMsg.Text += " NOK:" & strID & vbCrLf
                        End If
                    End If
                End If
            End If
        Next
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnTidakLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakLayak.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    If setTidakLayak(strID) = True Then
                        lblMsg.Text = "Berjaya reset kelayakan pelajar kepada TIDAK LAYAK."     ''& strID
                    Else
                        lblMsg.Text += " NOK:" & strID & vbCrLf
                    End If
                End If
            End If
        Next
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    Private Function setLayak(ByVal strStudentID As String) As Boolean
        '--get SchoolID
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strStudentID & "' and IsLatest = 'Y'"
        strSchoolID = oCommon.getFieldValue(strSQL)

        ''--insert UKM2
        strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,SchoolID,Status) VALUES('" & strStudentID & "','" & ddlExamYear.Text & "','" & strSchoolID & "','NEW')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        ''--insert UKM2_Answer
        strSQL = "INSERT INTO UKM2_Answer (StudentID,ExamYear) VALUES('" & strStudentID & "','" & ddlExamYear.Text & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        '--update UKM2 report information
        oCommon.UKM2_StudentprofileUpdate(strStudentID, ddlExamYear.Text)
        oCommon.UKM2_SchoolprofileUpdate(strStudentID, ddlExamYear.Text)

        ''--update UKM1

        If ddlExamYear.Text = getUKM1Year Then
            strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET isLayak='Y' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET isLayak='Y' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function

    Private Function setTidakLayak(ByVal strStudentID As String) As Boolean
        ''--semak sama ada telah menamatkan ujian 
        strSQL = "SELECT StudentID FROM UKM2 WHERE Status='DONE' AND StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Pelajar telah menamatkan Ujian UKM2 bagi tahun " & ddlExamYear.Text
            Return False
        End If

        ''--DELETE UKM2
        strSQL = "DELETE UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        ''--DELETE UKM2_Answer
        strSQL = "DELETE UKM2_Answer WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        If ddlExamYear.Text = getUKM1Year Then
            strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET isLayak='N' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET isLayak='N' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True
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


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        strRet = BindData(datRespondent)

    End Sub

End Class