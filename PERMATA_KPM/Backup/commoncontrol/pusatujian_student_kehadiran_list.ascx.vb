Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

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
                '--load UKM2 menu base on usertype
                master_menu_list()
                ddlMenudesc.SelectedValue = "01"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub master_menu_list()
        '--visibility
        Select Case getUserProfile_UserType()
            Case "ADMIN"
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

            ddlMenudesc.Items.Add(New ListItem("Select", "Select"))

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
                lblMsg.Text = "Tiada rekod ditemui."
            Else
                divMsg.Attributes("class") = "info"
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
        Dim strOrder As String = " ORDER BY a.TarikhUjian,a.SessiUKM2,d.SchoolName,d.SchoolCity,b.StudentFullname"

        tmpSQL = "SELECT a.StudentID,a.IsHadir,a.SelectedLang,a.IsLogin,a.ExamYear,a.TarikhUjian,a.SessiUKM2,a.Status,b.MYKAD,b.StudentFullname,b.DOB_Year,d.SchoolName,d.SchoolCity FROM UKM2 a, StudentProfile b, StudentSchool c, SchoolProfile d"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.StudentID=c.StudentID AND c.SchoolID=d.SchoolID"
        strWhere += " AND a.PusatCode='" & Request.QueryString("pusatcode") & "' AND a.ExamYear='" & Request.QueryString("examyear") & "'"

        '-DR Siti request. reason given migh hv problem later due to sempang or spaces.
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND b.MYKAD='" & txtMYKAD.Text & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND b.StudentFullname LIKE '%" & txtStudentFullname.Text & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getSQLMark() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY a.ExamStart DESC"

        tmpSQL = "SELECT a.StudentID,b.MYKAD,b.StudentFullname,b.DOB_Year,b.StudentRace,b.StudentReligion,b.StudentForm,d.FamilyContactNo,d.FatherFullname,f.SchoolName,f.SchoolCity,f.SchoolState,c.PusatState,c.PusatName,a.IsHadir,a.ExamStart,a.ExamEnd,a.Status,a.IsLogin,a.LastPage,a.Mod01,a.Mod02,a.Mod03,a.Mod04,a.Mod05,a.Mod06,a.Mod07,a.Mod08,a.Mod09,a.Mod10,a.Mod11,a.Mod12,a.Mod13,a.Mod14,a.Mod15,a.TotalScore,a.Fullmark,a.TotalPercentage,a.VCI,a.PRI,a.WMI,a.PSI FROM UKM2 a LEFT OUTER JOIN ParentProfile d ON a.StudentID = d.StudentID, StudentProfile b, PusatUjian c, StudentSchool e, SchoolProfile f"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.StudentID=e.StudentID AND e.SchoolID=f.SchoolID AND a.PusatCode=c.PusatCode AND IsHadir='Y'"

        '-DR Siti request. reason given migh hv problem later due to sempang or spaces.
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND b.MYKAD='" & txtMYKAD.Text & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND b.StudentFullname LIKE '%" & txtStudentFullname.Text & "%'"
        End If

        getSQLMark = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQLMark)

        Return getSQLMark

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID & "&examyear=" & Request.QueryString("examyear"))
            Case "UKM"
                Response.Redirect("ukm.studentprofile.view.aspx?studentid=" & strKeyID & "&examyear=" & Request.QueryString("examyear"))
            Case "KPM"
                Response.Redirect("kpm.studentprofile.view.aspx?studentid=" & strKeyID & "&examyear=" & Request.QueryString("examyear"))
            Case "JPN"
                Response.Redirect("jpn.studentprofile.view.aspx?studentid=" & strKeyID & "&examyear=" & Request.QueryString("examyear"))
            Case "ASASI"
                Response.Redirect("asasi.studentprofile.view.aspx?studentid=" & strKeyID & "&examyear=" & Request.QueryString("examyear"))

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

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

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
            strWhere += " WHERE a.Studentid='" & strStudentID & "' AND a.StudentID=b.StudentID AND b.SchoolID=c.SchoolID AND a.ExamYear='" & Request.QueryString("examyear") & "'"
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

                strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & strSchoolID & "',SchoolState='" & strSchoolState & "',SchoolCity='" & strSchoolCity & "',SchoolType='" & strSchoolType & "',SchoolPPD='" & strSchoolPPD & "',SchoolLokasi='" & strSchoolLokasi & "' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If Not strRet = "0" Then
                    lblMsg.Text = strRet
                End If
            Next
        Catch ex As Exception
            lblMsg.Text = "UKM2_Update:" & ex.Message
        End Try

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

        ''--UncheckAll()
        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini BAHASA PILIHAN pelajar."
        End If

    End Sub

    '--Reset UKM2
    Private Sub Execute_04()
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
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    ''get pusatcode
                    strSQL = "SELECT PusatCode FROM UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strPusatCode = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TarikhUjian FROM UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    Dim strTarikhUjian As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT SessiUKM2 FROM UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    Dim strSessiUKM2 As String = oCommon.getFieldValue(strSQL)

                    '--get schoolid
                    Dim strSchoolID As String = ""
                    strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strID & "'"
                    strSchoolID = oCommon.getFieldValue(strSQL)

                    ''DELETE UKM2
                    strSQL = "DELETE UKM2 WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "DELETE NOK:" & strID & strRet & vbCrLf
                    End If

                    ''INSERT BACK
                    strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2) VALUES ('" & strID & "','" & Request.QueryString("examyear") & "','Y','" & strPusatCode & "','" & strSchoolID & "','" & strTarikhUjian & "','" & strSessiUKM2 & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "INSERT NOK:" & strID & strRet & vbCrLf
                    End If

                    '--school information
                    UKM2_Update(strID)

                    ''debug
                    ''Response.Write("INSERT:" & strSQL)
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya RESET Ujian UKM2 pelajar tersebut. Click [CARI] untuk REFRESH!"
        End If

        ''--UncheckAll()
        ''strRet = BindData(datRespondent)

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
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        ''--UncheckAll()
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

        ''--UncheckAll()
        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya LOGOUT pelajar."
        End If

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
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='DONE' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    End If

                    '--SUM MOD
                    oCommon.UKM2DONE_Mod(strID, Request.QueryString("examyear"))
                    '--SUM Index
                    oCommon.UKM2DONE_Index(strID, Request.QueryString("examyear"))
                End If
            End If
        Next

        ''--UncheckAll()
        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar kepada DONE"
        End If

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

        ''--UncheckAll()
        strRet = BindData(datRespondent)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar kepada NEW"
        End If

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
            ExportToCSV(getSQLMark)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
    End Sub

End Class