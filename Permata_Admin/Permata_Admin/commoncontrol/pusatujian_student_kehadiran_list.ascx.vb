Imports System.Data.SqlClient

Partial Public Class pusatujian_student_kehadiran_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strUserType As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnExecute.Attributes.Add("onclick", "return confirm('Pasti ingin meneruskan fungsi tersebut?');")
        strUserType = getUserProfile_UserType()
        Try
            If Not IsPostBack Then
                ''--set default
                txtTarikhUjian.Text = oCommon.getTodayFormated

                '--load UKM2 menu base on usertype
                master_menu_list()
                ddlMenudesc.SelectedValue = "01"

                '--load sessiukm2
                master_sessiukm2_list()
                ddlSessiUKM2.Text = "ALL"

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub master_sessiukm2_list()
        strSQL = "SELECT * FROM master_sessiukm2 ORDER BY sessiukm2id"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSessiUKM2.DataSource = ds
            ddlSessiUKM2.DataTextField = "SessiUKM2"
            ddlSessiUKM2.DataValueField = "SessiUKM2"
            ddlSessiUKM2.DataBind()

            ddlSessiUKM2.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub master_menu_list()
        '--visibility
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                selLang.Visible = True
            Case "ADMINOP"
                selLang.Visible = True
            Case Else
                selLang.Visible = False
        End Select

        strSQL = "SELECT menudesc,menucode FROM master_menu WHERE menucategory='UKM2' AND usertype='" & getUserProfile_UserType() & "' ORDER BY menucode"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMenudesc.DataSource = ds
            ddlMenudesc.DataTextField = "menudesc"
            ddlMenudesc.DataValueField = "menucode"
            ddlMenudesc.DataBind()

            ddlMenudesc.Items.Add(New ListItem("--Select--", "00"))

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
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY UKM2.TarikhUjian,UKM2.SessiUKM2,SchoolProfile.SchoolName,SchoolProfile.SchoolCity,StudentProfile.StudentFullname"

        tmpSQL = "SELECT UKM2.StudentID,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.StudentFullname,StudentProfile.DOB_Year,StudentProfile.StudentCity,SchoolProfile.SchoolID,SchoolProfile.SchoolCode,SchoolProfile.Schoolname,SchoolProfile.Schoolcity,SchoolProfile.SchoolPPD,UKM2.SelectedLang,UKM2.IsHadir,UKM2.IsLogin,UKM2.ExamYear,UKM2.TarikhUjian,UKM2.SessiUKM2,UKM2.AdditionalMinute,UKM2.Status,ParentProfile.FamilyContactNo,ParentProfile.FatherFullname,PusatUjian.PusatCode,PusatUjian.PusatName FROM UKM2"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM2.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM2.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN PusatUjian ON UKM2.PusatCode=PusatUjian.PusatCode"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM2.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE UKM2.ExamYear='" & Request.QueryString("examyear") & "'"
        strWhere += " AND UKM2.PusatCode='" & Request.QueryString("pusatcode") & "'"

        '--TarikhUjian
        If Not txtTarikhUjian.Text.Length = 0 Then
            strWhere += " AND UKM2.TarikhUjian='" & oCommon.FixSingleQuotes(txtTarikhUjian.Text) & "'"
        End If

        '--SessiUKM2
        If Not ddlSessiUKM2.Text = "ALL" Then
            strWhere += " AND UKM2.SessiUKM2='" & ddlSessiUKM2.Text & "'"
        End If

        '--isHadir
        If Not selisHadir.Value = "ALL" Then
            strWhere += " AND UKM2.isHadir='" & selisHadir.Value & "'"
        End If

        '--MYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & txtMYKAD.Text & "'"
        End If

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & txtStudentFullname.Text & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getSQLMark() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY UKM2.TarikhUjian,UKM2.SessiUKM2,SchoolProfile.SchoolName,SchoolProfile.SchoolCity,StudentProfile.StudentFullname"

        tmpSQL = "SELECT UKM2.StudentID,StudentProfile.MYKAD,StudentProfile.StudentFullname,StudentProfile.DOB_Year,SchoolProfile.SchoolID,SchoolProfile.SchoolCode,SchoolProfile.Schoolname,SchoolProfile.Schoolcity,SchoolProfile.SchoolPPD,UKM2.IsHadir,UKM2.IsLogin,UKM2.ExamYear,UKM2.SessiUKM2,UKM2.TarikhUjian,UKM2.SelectedLang,ParentProfile.FamilyContactNo,ParentProfile.FatherFullname,PusatUjian.PusatCode,PusatUjian.PusatName,UKM2.Mod01,UKM2.Mod02,UKM2.Mod03,UKM2.Mod04,UKM2.Mod05,UKM2.Mod06,UKM2.Mod07,UKM2.Mod08,UKM2.Mod09,UKM2.Mod10,UKM2.Mod11,UKM2.Mod12,UKM2.Mod13,UKM2.Mod14,UKM2.Mod15,UKM2.TotalScore,UKM2.TotalPercentage,UKM2.VCI,UKM2.PRI,UKM2.WMI,UKM2.PSI FROM UKM2"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM2.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM2.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN PusatUjian ON UKM2.PusatCode=PusatUjian.PusatCode"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM2.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE UKM2.ExamYear='" & Request.QueryString("examyear") & "'"
        strWhere = " WHERE UKM2.PusatCode='" & Request.QueryString("pusatcode") & "'"

        '--SessiUKM2
        If Not ddlSessiUKM2.Text = "ALL" Then
            strWhere += " AND UKM2.SessiUKM2='" & ddlSessiUKM2.Text & "'"
        End If

        '--isHadir
        If Not selisHadir.Value = "ALL" Then
            strWhere += " AND UKM2.isHadir='" & selisHadir.Value & "'"
        End If

        '--MYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & txtMYKAD.Text & "'"
        End If

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & txtStudentFullname.Text & "%'"
        End If

        getSQLMark = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQLMark)

        Return getSQLMark

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "ADMINOP"
                Response.Redirect("studentprofile.view.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case Else
        End Select


    End Sub

    Private Sub UncheckAll()
        Dim row As GridViewRow
        For Each row In datRespondent.Rows
            Dim chkUncheck As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            chkUncheck.Checked = False
        Next

    End Sub

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
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

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecute.Click
        lblMsg.Text = ddlMenudesc.SelectedValue

        Select Case ddlMenudesc.SelectedValue
            Case "00"
                lblMsg.Text = "Please select functions to execute!"

            Case "01"   'Hadir
                Execute_01()

            Case "02"   'Tidak Hadir
                Execute_02()

            Case "03"   'Tukar Bahasa
                Execute_03()

            Case "04"   'Reset UKM2
                Execute_04()

            Case "05"   'Reset ExamStart
                Execute_05()

            Case "06"   'Logout UKM2
                Execute_06()

            Case "07"   'Set DONE
                Execute_07()

            Case "08"   'Set NEW
                Execute_08()

            Case "09"   'Export List
                Execute_09()

            Case "10"   'Export Mark
                Execute_10()
            Case "11" '--displaystatus=Y
                Execute_11()

            Case "12"   '--displaystatus=N
                Execute_12()

            Case "13"   'UPDATE ExamStopTime
                Execute_13()

            Case "14"   'UPDATE AdditionalMinute
                Execute_14()

            Case Else
                lblMsg.Text = "Please select functions to execute!"
        End Select

    End Sub



    '--Hadir
    Private Sub Execute_01()
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='Y' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        ''--UncheckAll()
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)
    End Sub

    '--Tidak Hadir
    Private Sub Execute_02()
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='N' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        ''--UncheckAll()
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    '--Tukar Bahasa
    Private Sub Execute_03()
        lblMsg.Text = ""
        If selLang.Value = "" Then
            lblMsg.Text = "Sila Pilih Bahasa terlebih dahulu."
            Exit Sub
        End If

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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SelectedLang='" & selLang.Value & "' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini BAHASA PILIHAN pelajar."
        End If

        ''--UncheckAll()
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)


    End Sub

    '--Reset UKM2
    Private Sub Execute_04()
        Dim strExamYear As String = Request.QueryString("examyear")
        Dim strPusatCode As String = ""
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
                    ''Dim strStudentID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strStudentID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strStudentID)

                    ''get pusatcode
                    strSQL = "SELECT PusatCode FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                    strPusatCode = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TarikhUjian FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                    Dim strTarikhUjian As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT SessiUKM2 FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                    Dim strSessiUKM2 As String = oCommon.getFieldValue(strSQL)

                    '--get schoolid
                    Dim strSchoolID As String = ""
                    strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
                    strSchoolID = oCommon.getFieldValue(strSQL)

                    ''DELETE UKM2
                    strSQL = "DELETE UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & strExamYear & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "DELETE NOK:" & strStudentID & strRet & vbCrLf
                    End If

                    '--set default value AdditionalMinute
                    Dim strAdditionalMinute As String = oCommon.getAppsettings("SaringanDuration")

                    ''INSERT BACK
                    strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2,Status,AdditionalMinute) VALUES ('" & strStudentID & "','" & strExamYear & "','Y','" & strPusatCode & "','" & strSchoolID & "','" & strTarikhUjian & "','" & strSessiUKM2 & "','NEW'," & strAdditionalMinute & ")"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "INSERT NOK:" & strStudentID & strRet & vbCrLf
                    End If

                    '--update UKM2 report information
                    oCommon.UKM2_StudentprofileUpdate(strStudentID, strExamYear)
                    oCommon.UKM2_SchoolprofileUpdate(strStudentID, strExamYear)

                    ''debug
                    ''Response.Write("INSERT:" & strSQL)
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini RESET UKM2 pelajar."
        End If

        ''--UncheckAll()
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)


    End Sub

    '--Reset ExamStart
    Private Sub Execute_05()
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET ExamStart=NULL,ExamEnd=NULL,Status='NEW',IsHadir='Y' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini RESET EXAMSTART pelajar."
        End If

        ''--UncheckAll()
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)


    End Sub

    '--Logout UKM2
    Private Sub Execute_06()
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET isLogin='N' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini LOGOUT pelajar."
        End If

        ''--UncheckAll()
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)


    End Sub

    '--Set DONE
    Private Sub Execute_07()
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='DONE' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                    '--SUM MOD
                    oCommon.UKM2DONE_Mod(strID, Request.QueryString("examyear"))
                    '--SUM Index
                    oCommon.UKM2DONE_Index(strID, Request.QueryString("examyear"))
                    '--Mental_Age_Year and Student_IQ
                    strRet = oCommon.UKM2DONE_CountIQ(strID, Request.QueryString("examyear"))
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini KEHADIRAN pelajar - DONE."
        End If

        ''--UncheckAll()
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)


    End Sub

    '--Set NEW
    Private Sub Execute_08()
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='NEW' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini KEHADIRAN pelajar - NEW."
        End If

        ''--UncheckAll()
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)


    End Sub

    '--Export List
    Private Sub Execute_09()
        Try
            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    '--Export Mark
    Private Sub Execute_10()
        Try
            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
    End Sub

    '--DisplayStatus='Y'
    Private Sub Execute_11()
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET DisplayStatus='Y' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini DisplayStatus UKM2."
        End If

    End Sub

    '--DisplayStatus='N'
    Private Sub Execute_12()
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET DisplayStatus='N' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini DisplayStatus UKM2."
        End If

    End Sub

    Private Sub Execute_13()
        'UPDATE UKM2 WITH (UPDLOCK) SET ExamStopTime=DATEADD(MINUTE, 5, ExamStopTime) WHERE StudentID='5d1230f0-582d-4aee-ba70-a485e377b0c9' AND ExamYear='2016-ASASI'
        lblMsg.Text = ""

        Try
            'Loop through gridview rows to find checkbox 
            'and check whether it is checked or not 
            Dim i As Integer
            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(17).FindControl("chkSelect"), CheckBox)
                ''--debug
                'Response.Write(chkUpdate)
                If Not chkUpdate Is Nothing Then
                    If chkUpdate.Checked = True Then
                        ' Get the values of textboxes using findControl
                        ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                        Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                        ''--debug
                        ''Response.Write(strID)

                        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET ExamStopTime=DATEADD(MINUTE, " & oCommon.FixSingleQuotes(txtAdditionalMinute.Text) & ", ExamStopTime) WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                        End If

                    End If
                End If
            Next

            strRet = BindData(datRespondent)

            If lblMsg.Text.Length = 0 Then
                lblMsg.Text = "Berjaya mengemaskini ExamStopTime pelajar."
            End If
        Catch ex As Exception
            lblMsgTop.Text = "Execute_13 Error: " & ex.Message
        End Try

    End Sub

    Private Sub Execute_14()
        lblMsg.Text = ""

        Try
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

                        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET AdditionalMinute=" & oCommon.FixSingleQuotes(txtAdditionalMinute.Text) & " WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                        End If

                    End If
                End If
            Next

            strRet = BindData(datRespondent)

            If lblMsg.Text.Length = 0 Then
                lblMsg.Text = "Berjaya mengemaskini AdditionalMinute pelajar."
            End If

        Catch ex As Exception
            lblMsgTop.Text = "Execute_14 Error: " & ex.Message
        End Try

    End Sub


End Class