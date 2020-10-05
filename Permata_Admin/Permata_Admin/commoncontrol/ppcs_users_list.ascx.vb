Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ppcs_users_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnRemove.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                ''access right
                btnExport.Visible = False
                setAccessRight()

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                master_PPCS_UserType_list()
                ddlUserType.Text = "ALL"

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
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

            '--source
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


    Private Sub master_PPCS_UserType_list()
        strSQL = "SELECT UserType FROM master_PPCS_UserType WHERE UserType<>'ADMIN' ORDER BY UserType"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUserType.DataSource = ds
            ddlUserType.DataTextField = "UserType"
            ddlUserType.DataValueField = "UserType"
            ddlUserType.DataBind()

            ddlUserType.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub setAccessRight()
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                btnExport.Visible = True
            Case "ADMINOP"
                btnExport.Visible = True

            Case "KETUA PENGURUS AKADEMIK"

            Case "PENGURUS AKADEMIK"

            Case "KETUA MODUL"

            Case "PENGAJAR"

            Case "PEMBANTU PENGAJAR"

            Case "PENGURUS PELAJAR"

            Case "PEMBANTU PELAJAR"

            Case "PENGURUS PEJABAT"

            Case Else
                btnExport.Visible = False

        End Select

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
                lblMsg.Text = "Record not found!"
            Else
                lblMsg.Text = "Total Record#:" & myDataSet.Tables(0).Rows.Count
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY a.Fullname ASC"

        tmpSQL = "SELECT a.myGUID,a.Fullname,a.LoginID,a.Pwd,a.IsAllow,b.UserType,b.PPCSDate FROM PPCS_Users a,PPCS_Users_Year b"
        strWhere = " WITH (NOLOCK) WHERE a.myGUID=b.myGUID AND b.UserType<>'ADMIN' AND b.PPCSDate='" & ddlPPCSDate.Text & "'"

        If Not ddlUserType.Text = "ALL" Then
            strWhere += " AND b.usertype='" & ddlUserType.Text & "'"
        End If

        If Not txtFullname.Text.Length = 0 Then
            strWhere += " AND a.Fullname LIKE '%" & oCommon.FixSingleQuotes(txtFullname.Text) & "%'"
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

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblCourse As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString  'myGUID

            lblCourse = e.Row.FindControl("lblCourse")
            lblCourse.Text = getClassCode(strKeyID)
        End If

    End Sub

    Private Function getClassCode(ByVal strmyGUID As String) As String
        Dim strValue As String = ""

        strSQL = "SELECT Usertype FROM PPCS_Users_Year WHERE myGUID='" & strmyGUID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        Dim strUserType As String = oCommon.getFieldValue(strSQL)

        Select Case strUserType
            Case "ADMIN"
            Case "ADMINOP"

            Case "KETUA PENGURUS AKADEMIK"

            Case "PENGURUS AKADEMIK"

            Case "KETUA MODUL"
                strSQL = "SELECT CourseCode FROM PPCS_Course WHERE (PPCSDate = '" & ddlPPCSDate.Text & "') AND (KetuaModul = '" & strmyGUID & "') OR (PPCSDate = '" & ddlPPCSDate.Text & "') AND (KetuaModulBI = '" & strmyGUID & "') ORDER BY CourseCode"
                strValue = oCommon.getRowValue(strSQL)

            Case "PENGAJAR"
                strSQL = "SELECT ClassCode FROM PPCS_Class WHERE PPCSDate='" & ddlPPCSDate.Text & "' AND Pengajar='" & strmyGUID & "' ORDER BY ClassCode"
                strValue = oCommon.getRowValue(strSQL)

            Case "PEMBANTU PENGAJAR"
                strSQL = "SELECT ClassCode FROM PPCS_Class WHERE PPCSDate='" & ddlPPCSDate.Text & "' AND PembantuPengajar='" & strmyGUID & "' ORDER BY ClassCode"
                strValue = oCommon.getRowValue(strSQL)

            Case "PENGURUS PELAJAR"
                strSQL = "SELECT ClassCode FROM PPCS_Class WHERE PPCSDate='" & ddlPPCSDate.Text & "' AND PengurusPelajar='" & strmyGUID & "' ORDER BY ClassCode"
                strValue = oCommon.getRowValue(strSQL)

            Case "PEMBANTU PELAJAR"
                strSQL = "SELECT ClassCode FROM PPCS_Class WHERE PPCSDate='" & ddlPPCSDate.Text & "' AND PembantuPelajar='" & strmyGUID & "' ORDER BY ClassCode"
                strValue = oCommon.getRowValue(strSQL)

            Case "PENGURUS PEJABAT"

            Case Else
                strValue = ""
                lblMsg.Text = "Invalid user type. Please contact system admin. " & strRet & ":" & strUserType
        End Select

        Return strValue
    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.ppcs.users.update.aspx?myguid=" & strKeyID)
            Case "ADMINOP"
                Response.Redirect("ppcs.users.update.aspx?myguid=" & strKeyID)
            Case "SUBADMIN"
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select



    End Sub


    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub


    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
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

                    '--DELETE 
                    strSQL = "DELETE FROM PPCS_Users_Year WHERE myGUID='" & strID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID
                    End If
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            strRet = BindData(datRespondent)
            lblMsg.Text = "Berjaya Remove PPCS Users."
        End If

    End Sub

    Private Sub btnIsAllowY_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIsAllowY.Click
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
                    strSQL = "UPDATE PPCS_Users WITH (UPDLOCK) SET isAllow='Y' WHERE myGUID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini isAllow."
        End If
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnIsAllowN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIsAllowN.Click
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
                    strSQL = "UPDATE PPCS_Users WITH (UPDLOCK) SET isAllow='N' WHERE myGUID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                    End If

                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini isAllow."
        End If
        strRet = BindData(datRespondent)
    End Sub
   

End Class