Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class pusatujian_student_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnExecute.Attributes.Add("onclick", "return confirm('Pasti ingin meneruskan fungsi tersebut?');")

        Try
            If Not IsPostBack Then
                ''--set default
                txtTarikhUjian.Text = oCommon.getTodayFormated

                '--load sessiukm2
                master_sessiukm2_list()

                '--load UKM2 menu base on usertype
                master_menu_list()
                ddlMenudesc.SelectedValue = "02"   'Kemaskini

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

            '--ddlSessiUKM2.Items.Add(New ListItem("--Select--", "00"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub master_menu_list()
        strSQL = "SELECT menudesc,menucode FROM master_menu WHERE menucategory='PusatUjian02' AND usertype='" & getUserProfile_UserType() & "' ORDER BY menucode"

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

        strSQL = getSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY UKM2.TarikhUjian,UKM2.SessiUKM2,SchoolProfile.Schoolname,StudentProfile.StudentFullname"

        tmpSQL = "SELECT UKM2.StudentID,StudentProfile.MYKAD,StudentProfile.StudentFullname,StudentProfile.DOB_Year,StudentProfile.StudentCity,SchoolProfile.SchoolID,SchoolProfile.SchoolCode,SchoolProfile.Schoolname,SchoolProfile.Schoolcity,SchoolProfile.SchoolPPD,UKM2.IsHadir,UKM2.IsLogin,UKM2.ExamYear,UKM2.SessiUKM2,UKM2.TarikhUjian,ParentProfile.FamilyContactNo,ParentProfile.FatherFullname,PusatUjian.PusatCode,PusatUjian.PusatName FROM UKM2"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM2.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM2.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN PusatUjian ON UKM2.PusatCode=PusatUjian.PusatCode"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM2.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE UKM2.ExamYear='" & Request.QueryString("examyear") & "'"
        strWhere = " WHERE UKM2.PusatCode='" & Request.QueryString("pusatcode") & "'"

        '--isHadir
        If Not selisHadir.Value = "ALL" Then
            strWhere += " AND UKM2.isHadir='" & selisHadir.Value & "'"
        End If

        '--MYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Function getPusatName() As String
        strSQL = "SELECT PusatName FROM PusatUjian WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "UKM"
                Response.Redirect("ukm.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "JPN"
                Response.Redirect("jpn.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "KPM"
                Response.Redirect("kpm.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "KPT"
                Response.Redirect("kpt.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "MRSM"
                Response.Redirect("mara.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "ASASI"
                Response.Redirect("asasi.studentprofile.view.aspx?studentid=" & strKeyID)
            Case Else
                lblMsg.Text = "Invalid usertype!"
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

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

    Private Sub CalUKM2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calUKM2.SelectionChanged
        txtTarikhUjian.Text = calUKM2.SelectedDate.ToString("yyyy-MM-dd")

    End Sub

    Protected Sub btnExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExecute.Click

        Select Case ddlMenudesc.Text
            Case "00"
                lblMsg.Text = "Please select functions to execute!"

            Case "01"   'Un-assign Pusat
                Execute_01()

            Case "02"   'Kemaskini
                Execute_02()

            Case "03"   'Export
                Execute_03()

            Case "04"   'Assign Pelajar
                Execute_04()

            Case Else
                lblMsg.Text = "Please select functions to execute!"
        End Select
    End Sub

    '--Un-assign Pusat
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Pusatcode=NULL,TarikhUjian=NULL,SessiUKM2=NULL WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    Else
                        lblMsg.Text += "OK"
                    End If

                End If
            End If
        Next

        lblMsg.Text += "Successfully un-assign Pusat Ujian for UKM2."
        strRet = BindData(datRespondent)

    End Sub

    'Kemaskini
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

                    strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET Pusatcode='" & Request.QueryString("pusatcode") & "',TarikhUjian='" & txtTarikhUjian.Text & "',SessiUKM2='" & ddlSessiUKM2.Text & "' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    Else
                        lblMsg.Text += "OK"
                    End If

                End If
            End If
        Next

        lblMsg.Text += "Successfully update students Date and Session."
        strRet = BindData(datRespondent)

    End Sub

    'Export
    Private Sub Execute_03()
        Try
            ExportToCSV()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    'Assign Pelajar
    Private Sub Execute_04()
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.student.select.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.student.select.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case Else
                lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

    Protected Sub btnLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

    End Sub

End Class