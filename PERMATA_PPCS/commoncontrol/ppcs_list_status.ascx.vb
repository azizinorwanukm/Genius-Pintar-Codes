Imports System.Data.SqlClient

Public Class ppcs_list_status1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strTestID As String

    '--truncate string and popup
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnUpdate.Attributes.Add("onclick", "return confirm('Pasti hendak mengemaskini rekod tersebut?');")

        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                '--top ppcs status
                PPCSStatus_list()
                ddlPPCSStatusSearch.Text = "ALL"

                'strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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

            ddlPPCSStatusSearch.DataSource = ds
            ddlPPCSStatusSearch.DataTextField = "PPCSStatus"
            ddlPPCSStatusSearch.DataValueField = "PPCSStatus"
            ddlPPCSStatusSearch.DataBind()

            ddlPPCSStatusSearch.Items.Add(New ListItem("ALL", "ALL"))
        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
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

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        strSQL = oCommon.PPCSDate_Query(Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value))

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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod ditemui!"
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT PPCS.PPCSID,PPCS.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.StudentGender,StudentProfile.DOB_Year,StudentProfile.StudentReligion,ParentProfile.FamilyContactNo,ParentProfile.FamilyContactNoIbu,SchoolProfile.SchoolName,SchoolProfile.SchoolCity,SchoolProfile.SchoolState,PPCS_Course.CourseCode,PPCS_Class.ClassCode,PPCS.NamaAsrama,PPCS.NoBilik,PPCS.isHadir,PPCS.IsScan,PPCS.IsPos,PPCS.StatusTawaran,PPCS.PPCSStatus,PPCS.Catatan FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON PPCS.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON StudentProfile.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        strWhere = " WHERE PPCS.PPCSDate ='" & ddlPPCSDate.Text & "'"

        If Not ddlPPCSStatusSearch.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSStatus ='" & ddlPPCSStatusSearch.Text & "'"
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

        '--SCAN
        If Not selScanSearch.Value = "ALL" Then
            strWhere += " AND PPCS.IsScan ='" & selScanSearch.Value & "'"
        End If

        '--POS
        If Not selPosSearch.Value = "ALL" Then
            strWhere += " AND PPCS.IsPos ='" & selPosSearch.Value & "'"
        End If

        '--selStatusTawaran
        If Not selStatusTawaran.Value = "ALL" Then
            strWhere += " AND PPCS.StatusTawaran ='" & selStatusTawaran.Value & "'"
        End If

        '--sel Hadir PPCS
        If Not selHadirPPCS.Value = "ALL" Then
            strWhere += " AND PPCS.IsHadir ='" & selHadirPPCS.Value & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        '--Response.Write(getSQL)

        Return getSQL

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

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
            Case "ADMIN"
                Response.Redirect("ppcs.update.aspx?studentid=" & strKeyID & "&ppcsdate=" & ddlPPCSDate.Text)
            Case "PENGURUS PEJABAT"
                Response.Redirect("ppcs.update.aspx?studentid=" & strKeyID & "&ppcsdate=" & ddlPPCSDate.Text)
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub

    Protected Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

            If isChecked Then
                strSQL = "UPDATE PPCS SET isScan='" & selScan.Value & "' WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                oCommon.ExecuteSQL(strSQL)
            Else
                '--do nothing
            End If
        Next
        ''refresh screen
        strRet = BindData(datRespondent)

        lblMsg.Text = "Berjaya kemaskini Status SCAN pelajar."

    End Sub

    Protected Sub btnPos_Click(sender As Object, e As EventArgs) Handles btnPos.Click
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

            If isChecked Then
                strSQL = "UPDATE PPCS SET isPos='" & selPos.Value & "' WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                oCommon.ExecuteSQL(strSQL)
            Else
                '--do nothing
            End If
        Next
        ''refresh screen
        strRet = BindData(datRespondent)

        lblMsg.Text = "Berjaya kemaskini Status POS pelajar."
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim tmpSQL As String = ""

        Try
            '--get what to update
            Select Case selStatus.Value
                Case "ScanY"
                    tmpSQL = "UPDATE PPCS SET IsScan='Y'"
                Case "ScanN"
                    tmpSQL = "UPDATE PPCS SET IsScan='N'"
                Case "PosY"
                    tmpSQL = "UPDATE PPCS SET IsPos='Y'"
                Case "PosN"
                    tmpSQL = "UPDATE PPCS SET IsPos='N'"
                Case Else
                    lblMsg.Text = "Please select status to update!"
                    Exit Sub
            End Select

            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(i)
                Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

                '--update it
                If isChecked Then
                    strSQL = tmpSQL & " WHERE StudentID='" & datRespondent.DataKeys(i).Value.ToString & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    '--do nothing
                End If
            Next
            ''refresh screen
            strRet = BindData(datRespondent)
            lblMsg.Text = "Berjaya kemaskini status scan/pos pelajar."

        Catch ex As Exception

        End Try

    End Sub

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand

        '--ok Response.Write("CommandName:" & e.CommandName)

        If e.CommandName = "ShowPopup" Then
            Dim btndetails As LinkButton = DirectCast(e.CommandSource, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

            lblStudentID.Text = datRespondent.DataKeys(gvrow.RowIndex).Value.ToString()
            getStudentProfileinfo()

            'txtCatatan.Text = HttpUtility.HtmlDecode(gvrow.Cells(3).Text)
            Popup(True)

            '--ok Response.Write("datRespondent_RowCommand01")
        End If

    End Sub

    Private Sub getStudentProfileinfo()

        strSQL = "SELECT StudentFullname FROM StudentProfile WHERE StudentID='" & lblStudentID.Text & "'"
        lblStudentFullname.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT MYKAD FROM StudentProfile WHERE StudentID='" & lblStudentID.Text & "'"
        lblMYKAD.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Catatan FROM PPCS WHERE StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        txtCatatan.Text = oCommon.getFieldValue(strSQL)

    End Sub

    'To show message after performing operations
    Private Sub Popup(isDisplay As Boolean)
        Dim builder As New StringBuilder()

        If isDisplay Then
            builder.Append("<script language=JavaScript> ShowPopup(); </script>" & vbLf)
            Page.ClientScript.RegisterStartupScript(Me.[GetType](), "ShowPopup", builder.ToString())
        Else
            builder.Append("<script language=JavaScript> HidePopup(); </script>" & vbLf)
            Page.ClientScript.RegisterStartupScript(Me.[GetType](), "HidePopup", builder.ToString())
        End If

    End Sub

    Private Sub btnPPCSUpdate_Click(sender As Object, e As EventArgs) Handles btnPPCSUpdate.Click
        strSQL = "UPDATE PPCS SET Catatan='" & oCommon.FixSingleQuotes(txtCatatan.Text) & "' WHERE StudentID='" & lblStudentID.Text & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Popup(False)
        End If

    End Sub

End Class