Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_schoolprofile_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Anda pasti untuk DELETE sekolah tersebut? Sekolah akan di-MARK isDeleted=Y');")

        Try
            If Not IsPostBack Then
                '--disable button
                setAccessRight()

                SchoolState_list()
                schoolprofile_PPD_list()
                schoolprofile_city_list()

                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub setAccessRight()

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                btnPindah.Visible = False
                btnDelete.Visible = True
                btnUpdate.Visible = True

            Case "ADMINOP"
                btnPindah.Visible = False
                btnDelete.Visible = False
                btnUpdate.Visible = False

            Case "SUBADMIN"
                btnPindah.Visible = False
                btnDelete.Visible = False
                btnUpdate.Visible = False
            Case Else
                btnPindah.Visible = False
                btnDelete.Visible = False
                btnUpdate.Visible = False
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

            'ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub SchoolState_list()
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) ORDER BY SchoolStateID"
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

            ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))

            '--default state
            strRet = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If
            ddlSchoolState.SelectedValue = getUserProfile_State()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":SchoolState_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub schoolprofile_PPD_list()
        '-user can select ALL for state
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY SchoolPPD"

        strSQL = "SELECT DISTINCT SchoolPPD FROM SchoolProfile WHERE SchoolPPD<>'' AND IsDeleted<>'Y'"
        strWhere = " AND SchoolState='" & ddlSchoolState.Text & "'"
        strSQL = strSQL & strWhere & strOrderby

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

    Private Sub schoolprofile_city_list()
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY SchoolCity"

        strSQL = "SELECT DISTINCT SchoolCity FROM schoolprofile WHERE SchoolState='" & ddlSchoolState.Text & "' AND IsDeleted='N'"
        If Not ddlSchoolPPD.Text = "ALL" Then
            strWhere = " AND SchoolPPD='" & ddlSchoolPPD.Text & "'"
        End If
        strSQL = strSQL & strWhere & strOrderby

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolCity.DataSource = ds
            ddlSchoolCity.DataTextField = "schoolcity"
            ddlSchoolCity.DataValueField = "schoolcity"
            ddlSchoolCity.DataBind()

            ddlSchoolCity.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolCity.SelectedValue = "ALL"

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai"
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strWhereIn As String = ""
        Dim strOrder As String = " ORDER BY Jumlah DESC"

        Try
            ''avoid non-integer year
            Dim strYear As String = ""
            If ddlExamYear.Text.Length > 4 Then
                strYear = ddlExamYear.Text.Substring(0, 4)
            Else
                strYear = ddlExamYear.Text
            End If

            If chkRuleAge2.Checked = True Then
                strWhereIn = " AND UKM1.DOB_Year BETWEEN " & oCommon.getValidYear(strYear, 12) & " AND " & oCommon.getValidYear(strYear, 8)
            End If

            '--chkRuleAge
            If chkRuleAge.Checked = True Then
                strWhereIn = " AND UKM1.IsCount=1"
            End If

            tmpSQL = "SELECT SchoolProfile.SchoolID,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,SchoolProfile.SchoolLokasi,(SELECT COUNT(*) FROM UKM1 WHERE SchoolProfile.SchoolID=UKM1.SchoolID AND UKM1.Examyear='" & ddlExamYear.Text & "'" & strWhereIn & ") as Jumlah FROM SchoolProfile"
            strWhere += " WHERE SchoolProfile.IsDeleted='" & selIsDeleted.Value & "'"

            ''usertype. for MRSM only
            If getUserProfile_UserType() = "MRSM" Then
                strWhere += " AND SchoolProfile.SchoolType='MRSM'"
            End If

            '--SchoolState
            If Not ddlSchoolState.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolState='" & ddlSchoolState.Text & "'"
            End If

            '--SchoolPPD
            If Not ddlSchoolPPD.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolPPD='" & ddlSchoolPPD.Text & "'"
            End If

            '--SchoolCity
            If Not ddlSchoolCity.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolCity='" & ddlSchoolCity.Text & "'"
            End If

            '--SchoolCode
            If Not txtSchoolCode.Text.Length = 0 Then
                strWhere += " AND SchoolProfile.SchoolCode='" & oCommon.FixSingleQuotes(txtSchoolCode.Text) & "'"
            End If

            '--SchoolName
            If Not txtSchoolname.Text.Length = 0 Then
                strWhere += " AND SchoolProfile.SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolname.Text) & "%'"
            End If

            '--SchoolCode XXX
            If chkXXX.Checked = True Then
                strWhere += " AND SchoolProfile.SchoolCode LIKE 'XXX%'"
            Else
                strWhere += " AND SchoolProfile.SchoolCode NOT LIKE 'XXX%'"
            End If

            getSQL = tmpSQL & strWhere & strOrder

            ''--debug
            'Response.Write(getSQL)
            Return getSQL

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("kpm.ukm1.schoolprofile.student.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID & "&iscount=" & chkRuleAge.Checked)
                Case "ADMINOP"
                    Response.Redirect("ukm1.schoolprofile.student.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID & "&iscount=" & chkRuleAge.Checked)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.ukm1.schoolprofile.student.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID & "&iscount=" & chkRuleAge.Checked)
                Case "KPT"
                Case "ASASI"
                Case Else
                    lblMsg.Text = "Invalid User Type:" & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlSchoolState_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_PPD_list()
        schoolprofile_city_list()

    End Sub

    Private Sub ddlSchoolPPD_TextChanged(sender As Object, e As EventArgs) Handles ddlSchoolPPD.TextChanged
        schoolprofile_city_list()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE SchoolProfile WITH (UPDLOCK) SET IsDeleted='Y' WHERE SchoolID='" & strID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
        Next

        '--refresh screen
        strRet = BindData(datRespondent)
        lblMsgTop.Text = "Berjaya DELETE sekolah yang telah dipilih!"

    End Sub

    '--need to check code. 
    Private Sub btnPindah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPindah.Click
        Dim strPindahID As String = oCommon.getSessionID
        lblMsg.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    strSQL = "INSERT INTO SchoolPindah(PindahID,SchoolID) VALUES('" & strPindahID & "','" & strID & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "Error " & i.ToString & ". SchoolID:" & strID & "<br />"
                    End If
                End If
            End If
        Next

        '--something wrong
        If Not lblMsg.Text = "" Then
            Exit Sub
        End If

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.ukm1.schoolprofile.list.pindah.aspx?pindahid=" & strPindahID)
            Case "SUBADMIN"
                Response.Redirect("admin.ukm1.schoolprofile.list.pindah.aspx?pindahid=" & strPindahID)
            Case "KPT"
            Case "ASASI"
            Case Else
                lblMsg.Text = "Invalid User Type:" & getUserProfile_UserType()
        End Select

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
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
                    Response.Redirect("kpm.schoolprofile.update.aspx?schoolid=" & strID)
                End If
            End If
        Next
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
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