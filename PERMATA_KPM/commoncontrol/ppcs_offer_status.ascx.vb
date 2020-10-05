Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class ppcs_offer_status
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
                ppcsdate_list()
                ddlPPCSDate.Text = ConfigurationManager.AppSettings("DefaultPPCSDate")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                PPCSStatus_list()
                ddlPPCSStatus.Text = "ALL"

                '--load PPCS menu base on usertype
                master_menu_list()
                ddlMenudesc.SelectedValue = "00"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub master_menu_list()
        '--visibility
        Select Case getUserProfile_UserType()
            Case "ADMIN"
            Case "UKM"
            Case Else
        End Select

        strSQL = "SELECT menudesc,menucode FROM master_menu WHERE menucategory='PPCS' AND usertype='" & getUserProfile_UserType() & "' ORDER BY menucode"

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

    Private Sub PPCSStatus_list()
        strSQL = "SELECT PPCSStatus FROM master_PPCSStatus ORDER BY PPCSStatus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSStatus.DataSource = ds
            ddlPPCSStatus.DataTextField = "PPCSStatus"
            ddlPPCSStatus.DataValueField = "PPCSStatus"
            ddlPPCSStatus.DataBind()

            ddlPPCSStatus.Items.Add(New ListItem("ALL", "ALL"))

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

            'ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub setAccessRight()

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


    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
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
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod dijumpai."
            Else
                divMsg.Attributes("class") = "info"
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        If selSort.Value = "0" Then
            strOrder = " ORDER BY PPCS.StatusDate DESC"
        Else
            strOrder = " ORDER BY StudentProfile.StudentFullname"
        End If

        tmpSQL = "SELECT PPCS.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentReligion,StudentProfile.StudentRace,StudentProfile.StudentCity,StudentProfile.StudentState,SchoolProfile.SchoolName,SchoolProfile.SchoolCity,SchoolProfile.SchoolState,PPCS.PPCSStatus,PPCS.StatusTawaran,PPCS.StatusDate,PPCS.StatusReason,PPCS_Course.CourseCode,PPCS_Class.ClassCode FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON PPCS.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        strWhere = " WHERE PPCS.PPCSDate='" & ddlPPCSDate.Text & "'"

        If Not ddlPPCSStatus.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSStatus='" & ddlPPCSStatus.Text & "'"
        End If

        If Not selStatusTawaran.Value = "ALL" Then
            strWhere += " AND PPCS.StatusTawaran ='" & selStatusTawaran.Value & "'"
        End If

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND StudentProfile.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        If Not selStudentGender.Value = "ALL" Then
            strWhere += " AND StudentProfile.StudentGender ='" & selStudentGender.Value & "'"
        End If

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

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
        strRet = BindData(datRespondent)

    End Sub


    Protected Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        lblMsg.Text = ddlMenudesc.SelectedValue

        Select Case ddlMenudesc.SelectedValue
            Case "00"
                lblMsg.Text = "Please select functions to execute!"

            Case "01"   'Terima
                Execute_01()

            Case "02"   'Tolak
                Execute_02()

            Case "03"   'Export
                Execute_03()

            Case "04"   'DisplayStatus Y
                Execute_04()

            Case "05"   'DisplayStatus N
                Execute_05()

            Case Else
                lblMsg.Text = "Please select functions to execute!"
        End Select

    End Sub

    '--Terima
    Private Sub Execute_01()
        divMsg.Attributes("class") = "info"
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
                    ''--UPDATE
                    strSQL = "UPDATE PPCS WITH (UPDLOCK) SET PPCSStatus='LAYAK',StatusTawaran='TERIMA',StatusReason='-',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & strID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text = "NOK: " & strRet
                    End If
                End If
            End If
        Next

        txtStatusReason.Text = ""
        strRet = BindData(datRespondent)

        lblMsg.Text = "Berjaya mengemaskini Status PPCSStatus=LAYAK dan StatusTawaran=TERIMA"

    End Sub

    '--Tolak
    Private Sub Execute_02()
        divMsg.Attributes("class") = "info"
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        If txtStatusReason.Text.Length = 0 Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila masukkan sebab TOLAK."
            lblMsgTop.Text = lblMsg.Text
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
                    ''--UPDATE
                    strSQL = "UPDATE PPCS WITH (UPDLOCK) SET PPCSStatus='REJECT',StatusTawaran='TOLAK',StatusReason='" & oCommon.FixSingleQuotes(txtStatusReason.Text) & "',StatusDate='" & oCommon.getNow & "' WHERE StudentID='" & strID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text = "NOK: " & strRet
                    End If
                End If
            End If
        Next

        txtStatusReason.Text = ""
        strRet = BindData(datRespondent)
        lblMsg.Text = "Berjaya mengemaskini Status PPCSStatus=REJECT dan StatusTawaran=TOLAK"

    End Sub

    '--Export
    Private Sub Execute_03()
        Try
            ExportToCSV()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    '--DisplayStatus Y
    Private Sub Execute_04()
        Try
            strSQL = "UPDATE PPCS WITH (UPDLOCK) SET DisplayStatus='Y' WHERE PPCSDate='" & ddlPPCSDate.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text += "NOK:" & strRet & vbCrLf
            Else
                lblMsg.Text = "Berjaya kemaskini DisplayStatus=Y bagi Sessi PPCS pilihan."
            End If

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    '--DisplayStatus N
    Private Sub Execute_05()
        Try
            strSQL = "UPDATE PPCS WITH (UPDLOCK) SET DisplayStatus='N' WHERE PPCSDate='" & ddlPPCSDate.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text += "NOK:" & strRet & vbCrLf
            Else
                lblMsg.Text = "Berjaya kemaskini DisplayStatus=N bagi Sessi PPCS pilihan."
            End If

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub


End Class