Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData

Public Class ukm3_kelayakan_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnLayak.Attributes.Add("onclick", "return confirm('Pilih Sessi PPCS. Pasti ingin MELAYAKKAN pelajar-pelajar tersebut?');")
        btnTidakLayak.Attributes.Add("onclick", "return confirm('Pilih Sessi PPCS. Pasti TIDAK MELAYAKKAN pelajar-pelajar tersebut? Maklumat PPCS jika ada akan terhapus.');")

        Try
            If Not IsPostBack Then
                lblMsg.Text = ""
                lblMsgTop.Text = ""

                '--ddlPPCSDate
                ppcsdate_list()
                ddlPPCSDate.Text = ConfigurationManager.AppSettings("DefaultPPCSDate")

                '-list of exam avail
                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")


                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                '--list all religion
                StudentReligion_list()
                ddlStudentReligion.Text = "ALL"

                '--SchoolState
                SchoolState_list()
                ddlSchoolState.Text = "ALL"

                '--SchoolPPD
                schoolprofile_PPD_list()

                '--schooltype
                schooltype_list()

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
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

            '--ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
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

            ddlSchoolType.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolType.SelectedValue = "ALL"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub SchoolState_list()
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) WHERE SchoolState<>'UKM2-KPT' AND SchoolState <>'UKM2-ASASI'  ORDER BY SchoolStateID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolState.DataSource = ds
            ddlSchoolState.DataTextField = "schoolstate"
            ddlSchoolState.DataValueField = "schoolstate"
            ddlSchoolState.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub schoolprofile_PPD_list()
        strSQL = "SELECT DISTINCT SchoolPPD FROM SchoolProfile WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolPPD<>'' AND IsDeleted<>'Y' ORDER BY SchoolPPD"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolPPD.DataSource = ds
            ddlSchoolPPD.DataTextField = "SchoolPPD"
            ddlSchoolPPD.DataValueField = "SchoolPPD"
            ddlSchoolPPD.DataBind()

            ddlSchoolPPD.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolPPD.SelectedValue = "ALL"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
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
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

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

    Private Sub ppcsdate_list()
        strSQL = "SELECT PPCSDate FROM master_PPCSDate ORDER BY ppcsid ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak dijumpai"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah pelajar#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = ""

        tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentGender,StudentProfile.StudentAddress1,StudentProfile.StudentAddress2,StudentProfile.StudentPostcode,StudentProfile.StudentCity,StudentProfile.StudentState,StudentProfile.StudentCountry,StudentProfile.StudentContactNo,ParentProfile.FatherFullname,ParentProfile.FatherJob,ParentProfile.FamilyContactNo,ParentProfile.MotherFullname,ParentProfile.FamilyContactNoIbu,SchoolProfile.SchoolName,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,SchoolProfile.SchoolType,SchoolProfile.SchoolLokasi,UKM1.Status as UKM1Status,UKM1.ExamStart as UKM1ExamStart,UKM1.ExamEnd as UKM1ExamEnd,UKM1.QuestionYear as UKM1QuestionYear,UKM1.TotalPercentage as UKM1TotalPercentage,UKM2.ExamStart as UKM2ExamStart,UKM2.ExamEnd as UKM2ExamEnd,UKM2.TotalPercentage as UKM2TotalPercentage,UKM2.Mental_Age_Year,UKM2.Student_IQ,UKM2.SelectedLang as UKM2SelectedLang,PPCS.PPCSDate FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN UKM1 ON PPCS.StudentID=UKM1.StudentID AND UKM1.ExamYear='" & ddlExamYear.Text & "'"
        tmpSQL += " LEFT OUTER JOIN UKM2 ON PPCS.StudentID=UKM2.StudentID AND UKM2.ExamYear='" & ddlExamYear.Text & "'"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON PPCS.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON PPCS.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE PPCS.PPCSDate='" & ddlPPCSDate.Text & "' AND PPCSStatus='LAYAK' AND StatusTawaran='TERIMA'"

        '--SchoolState
        If Not ddlSchoolState.Text = "ALL" Then
            strWhere += " AND SchoolProfile.SchoolState='" & ddlSchoolState.Text & "'"
        End If

        '--SchoolPPD
        If Not ddlSchoolPPD.Text = "ALL" Then
            strWhere += " AND SchoolProfile.SchoolPPD='" & ddlSchoolPPD.Text & "'"
        End If


        '--DOB_Year
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND StudentProfile.DOB_Year='" & ddlDOB_Year.Text & "'"
        End If

        If Not ddlStudentReligion.Text = "ALL" Then
            strWhere += " AND StudentProfile.StudentReligion='" & ddlStudentReligion.Text & "'"
        End If

        If chkAlumni.Checked = True Then
            strWhere += " AND StudentProfile.AlumniID IS NOT NULL"
        End If

        '--MYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        '--txtSchoolName
        If Not txtSchoolName.Text.Length = 0 Then
            strWhere += " AND  SchoolProfile.SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolName.Text) & "%'"
        End If

        '--ddlSchoolType
        If Not ddlSchoolType.Text = "ALL" Then
            strWhere += " AND SchoolProfile.SchoolType='" & ddlSchoolType.Text & "'"
        End If

        If selSort.Value = "0" Then
            strOrder = " ORDER BY StudentProfile.StudentFullname ASC"
        Else
            strOrder = " ORDER BY UKM2.TotalScore DESC"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        '--Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblLayak As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

            lblLayak = e.Row.FindControl("lblLayak")
            lblLayak.Text = isLayakUKM3(strKeyID)

        End If
    End Sub

    Private Function isLayakUKM3(ByVal strStudentID As String) As String
        ''--get UKM3
        strSQL = "SELECT StudentID FROM UKM3 WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            Return "Y"
        Else
            Return "N"
        End If

    End Function

    Private Function getPPCSDate(ByVal strStudentID As String) As String
        ''--get the date
        strSQL = "SELECT PPCSDate FROM PPCS WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.getRowValue(strSQL)
        Return strRet

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ppcs.alumni.studentprofile.aspx?studentid=" & strKeyID & "&ppcsdate=" & ddlPPCSDate.Text)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

    Private Sub btnLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLayak.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)
                    If setLayak(strID) = True Then
                        lblMsg.Text = "Berjaya melayakkan semua pelajar yang dipilih."
                    Else
                        lblMsg.Text += "NOK:" & strID & vbCrLf
                    End If
                End If
            End If
        Next

        lblMsgTop.Text = lblMsg.Text
        'UncheckAll()
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnTidakLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakLayak.Click
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    'Response.Write(strID)& "|"
                    If setTidakLayak(strID) = True Then
                        lblMsg.Text = "Berjaya reset kelayakan pelajar kepada TIDAK LAYAK."     ''& strID
                    Else
                        lblMsg.Text += "NOK:" & strID & vbCrLf
                    End If
                End If
            End If
        Next

        lblMsgTop.Text = lblMsg.Text
        'UncheckAll()
        strRet = BindData(datRespondent)

    End Sub

    Private Sub UncheckAll()
        Dim row As GridViewRow
        For Each row In datRespondent.Rows
            Dim chkUncheck As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            chkUncheck.Checked = False
        Next

    End Sub

    Private Function setLayak(ByVal strStudentID As String) As Boolean
        ''--check duplicate entry. no duplicate studentid and examyear
        strSQL = "SELECT StudentID FROM UKM3 WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            ''--UPDATE
            '--DR Siti request not to update statustawaran. Only students can update it. Default blank/null
            strSQL = "UPDATE UKM3 WITH (UPDLOCK) SET Status='LAYAK'" & "' WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        Else
            ''--INSERT
            strSQL = "INSERT INTO UKM3 (StudentID,PPCSDate,Status,StatusTawaran) VALUES('" & strStudentID & "','" & ddlPPCSDate.Text & "','LAYAK',NULL)"
        End If
        ''-debug
        'Response.Write(strSQL)
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function

    Private Function setTidakLayak(ByVal strStudentID As String) As Boolean

        ''--insert UKM2
        strSQL = "DELETE UKM3 WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function

    Private Function ExportData(ByVal dsTable As DataSet, ByVal strTitle As String) As String
        ''-Dim strFilename As String = Server.MapPath(".") & "log\" & "Export." & oCommon.getRandom & ".txt"
        Dim strFilename As String = strTitle & oCommon.getRandom & ".txt"

        Try
            ' Export all the details to xls
            Dim objExport As New RKLib.ExportData.Export("Web")
            Dim dtRespondent As DataTable = dsTable.Tables("mytable").Copy()
            objExport.ExportDetails(dtRespondent, Export.ExportFormat.CSV, strFilename)

            Return strFilename
        Catch Ex As Exception
            Return Ex.Message
        End Try

    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlSchoolState_TextChanged(sender As Object, e As EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_PPD_list()

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV()
            lblMsgTop.Text = lblMsg.Text
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