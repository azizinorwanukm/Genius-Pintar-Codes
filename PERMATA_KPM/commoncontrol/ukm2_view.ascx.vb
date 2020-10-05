Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Public Class ukm2_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnExecute.Attributes.Add("onclick", "return confirm('Pasti ingin meneruskan fungsi tersebut?');")

        Try
            If Not IsPostBack Then
                '--load UKM2 menu base on usertype
                master_menu_list()
                ddlMenudesc.SelectedValue = "01"

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub master_menu_list()
        '--visibility
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                selLang.Visible = True
            Case "UKM"
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

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
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
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT UKM2.UKM2ID,UKM2.StudentID,StudentProfile.MYKAD,StudentProfile.StudentFullname,StudentProfile.DOB_Year,SchoolProfile.SchoolID,SchoolProfile.SchoolCode,SchoolProfile.Schoolname,SchoolProfile.SchoolPPD,SchoolProfile.Schoolcity,UKM2.ExamYear,UKM2.ExamStart,UKM2.ExamEnd,UKM2.Status,UKM2.LastPage,UKM2.TarikhUjian,UKM2.SessiUKM2,UKM2.IsHadir,UKM2.IsLogin,UKM2.SelectedLang,ParentProfile.FamilyContactNo,ParentProfile.FatherFullname,PusatUjian.PusatState,PusatUjian.PusatCity,PusatUjian.PusatCode,PusatUjian.PusatName FROM UKM2"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM2.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM2.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN PusatUjian ON UKM2.PusatCode=PusatUjian.PusatCode"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM2.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE UKM2.StudentID='" & Request.QueryString("studentid") & "'"
        strWhere += " AND UKM2.ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Protected Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        lblMsg.Text = ddlMenudesc.SelectedValue

        '--execute base on ukm2id
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

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "NOK:" & strRet
        End If

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)
    End Sub

    '--Tidak Hadir
    Private Sub Execute_02()
        lblMsg.Text = ""


        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET IsHadir='N' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "NOK:" & strRet
        End If

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

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

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SelectedLang='" & selLang.Value & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "NOK:" & strRet
        End If

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini BAHASA PILIHAN pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    '--Reset UKM2
    Private Sub Execute_04()
        Dim strPusatCode As String = ""
        lblMsg.Text = ""

        ''--get pusatcode
        strSQL = "SELECT PusatCode FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strPusatCode = oCommon.getFieldValue(strSQL)

        '--TarikhUjian
        strSQL = "SELECT TarikhUjian FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        Dim strTarikhUjian As String = oCommon.getFieldValue(strSQL)

        '--SessiUKM2
        strSQL = "SELECT SessiUKM2 FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        Dim strSessiUKM2 As String = oCommon.getFieldValue(strSQL)

        '--ExamYear
        strSQL = "SELECT ExamYear FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        Dim strExamYear As String = oCommon.getFieldValue(strSQL)

        '--get schoolid
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strSchoolID = oCommon.getFieldValue(strSQL)

        ''DELETE UKM2
        strSQL = "DELETE UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "DELETE NOK:" & strRet
        End If

        ''INSERT BACK
        strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2,Status) VALUES ('" & Request.QueryString("studentid") & "','" & strExamYear & "','Y','" & strPusatCode & "','" & strSchoolID & "','" & strTarikhUjian & "','" & strSessiUKM2 & "','NEW')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "INSERT NOK:" & strRet
        End If

        '--school information
        UKM2_Update(Request.QueryString("studentid"))

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya RESET Ujian UKM2 pelajar tersebut."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    '--Reset ExamStart
    Private Sub Execute_05()
        lblMsg.Text = ""

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET ExamStart=NULL,ExamEnd=NULL,Status='NEW',IsHadir='Y' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "NOK:" & strRet
        End If

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini ExamStart pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    '--Logout UKM2
    Private Sub Execute_06()
        lblMsg.Text = ""

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET isLogin='N' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "NOK:" & strRet
        End If

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya Logout pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    '--Set DONE
    Private Sub Execute_07()
        lblMsg.Text = ""
        '--ExamYear
        strSQL = "SELECT ExamYear FROM UKM2 WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        Dim strExamYear As String = oCommon.getFieldValue(strSQL)

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='DONE' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "NOK:" & strRet
        End If

        '--SUM MOD
        oCommon.UKM2DONE_Mod(Request.QueryString("studentid"), strExamYear)
        '--SUM Index
        oCommon.UKM2DONE_Index(Request.QueryString("studentid"), strExamYear)
        '--Mental_Age_Year and Student_IQ
        strRet = oCommon.UKM2DONE_CountIQ(Request.QueryString("studentid"), strExamYear)

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya set DONE pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    '--Set NEW
    Private Sub Execute_08()
        lblMsg.Text = ""

        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Status='NEW' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "NOK:" & strRet
        End If

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya set NEW pelajar."
        End If

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
            strWhere += " WHERE a.Studentid='" & strStudentID & "' AND a.StudentID=b.StudentID AND b.SchoolID=c.SchoolID AND a.ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
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

                strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & strSchoolID & "',SchoolState='" & strSchoolState & "',SchoolCity='" & strSchoolCity & "',SchoolType='" & strSchoolType & "',SchoolPPD='" & strSchoolPPD & "',SchoolLokasi='" & strSchoolLokasi & "' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
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