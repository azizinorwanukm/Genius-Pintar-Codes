Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_school_studentprofile_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            master_dobyear_list()
            ddlDOB_Year.Text = "ALL"

            strRet = BindData(datRespondent)
            setAccessRight()
        End If

    End Sub

    Private Sub setAccessRight()

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                btnStudentSchoolUpdate.Visible = True
            Case "ADMINOP"
                btnStudentSchoolUpdate.Visible = False
            Case "SUBADMIN"
                btnStudentSchoolUpdate.Visible = False
            Case Else
                btnStudentSchoolUpdate.Visible = False
        End Select

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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        Dim strYear = Request.QueryString("examyear")

        '--get from UKM1
        tmpSQL = "SELECT StudentProfile.StudentProfileID,StudentProfile.StudentID,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.StudentFullname,StudentProfile.StudentGender,StudentProfile.StudentRace,StudentProfile.StudentReligion,UKM1.DOB_Year,UKM1.Status,UKM1.TotalPercentage as UKM1Percentage FROM UKM1"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM1.StudentID=StudentProfile.StudentID"
        strWhere = " WHERE UKM1.SchoolID='" & Request.QueryString("schoolid") & "' AND UKM1.ExamYear='" & Request.QueryString("examyear") & "'"

        '--IsCount
        If Request.QueryString("iscount") = "True" Then
            strWhere += " AND UKM1.IsCount=1"
        Else
            strWhere += " AND UKM1.DOB_Year BETWEEN " & oCommon.getValidYear(strYear, 12) & " AND " & oCommon.getValidYear(strYear, 8)
        End If

        '--DOB_Year
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND UKM1.DOB_Year='" & ddlDOB_Year.Text & "'"
        End If

        '--Status
        If Not selStatus.Value = "ALL" Then
            strWhere += " AND UKM1.Status='" & selStatus.Value & "'"
        End If

        '--TotalPercentage
        If Not txtTotalPercentage.Text.Length = 0 Or txtTotalPercentage.Text = "0" Then
            strWhere += " AND UKM1.TotalPercentage >=" & txtTotalPercentage.Text
        End If

        Select Case selOrderBy.Value
            Case "0"
                strOrder = " ORDER BY StudentProfile.Studentfullname"
            Case "1"
                strOrder = " ORDER BY UKM1.DOB_Year"
            Case "2"
                strOrder = " ORDER BY UKM1.TotalPercentage DESC"
            Case Else
                strOrder = " ORDER BY StudentProfile.Studentfullname"
        End Select

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
            Case "ADMINOP"
                Response.Redirect("studentprofile.view.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "KPT"
            Case "ASASI"
            Case Else
                lblMsg.Text = "Invalid User Type: " & getUserProfile_UserType()
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
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

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnStudentSchoolUpdate_Click(sender As Object, e As EventArgs) Handles btnStudentSchoolUpdate.Click

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("kpm.studentschool.schoolprofile.select.aspx?oldschoolid=" & Request.QueryString("schoolid") & "&examyear=" & Request.QueryString("examyear"))
            Case "ADMINOP"
                Response.Redirect("studentschool.schoolprofile.select.aspx?oldschoolid=" & Request.QueryString("schoolid") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                lblMsg.Text = "Tiada kebenaran!"
            Case Else
                lblMsg.Text = "Tiada kebenaran!"
        End Select

    End Sub

    Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                '--security
                Select Case getUserProfile_UserType()
                    Case "ADMIN"
                        '--dont hide
                    Case "ADMINOP"
                        '--dont hide
                    Case Else
                        hideColumn("7")
                End Select

            End If

        Catch ex As Exception
            lblDebug.Text = "datRespondent_RowDataBound:" & ex.Message
        End Try

    End Sub


    Private Sub hideColumn(ByVal strIndex As String)
        '--admin dont send anything to hide
        If strIndex.Length = 0 Then
            Exit Sub
        End If

        ' Split string based on spaces.
        Dim arColumn As String() = strIndex.Split(",")

        ' Use For Each loop over words and display them.
        Dim nColumn As String
        For Each nColumn In arColumn
            datRespondent.Columns(nColumn).Visible = False
        Next

        ' datRespondent.Columns(6).Visible = False
    End Sub


End Class