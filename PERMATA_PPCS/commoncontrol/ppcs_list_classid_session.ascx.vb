Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData

Partial Public Class ppcs_list_classid_session
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")
    Dim strClassID As String
    Dim strTestID As String

    '--DefaultPPCSDate
    Dim strPPCSDate As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                '--load terus base on classid
                BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod pelajar."
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
        Dim strOrder As String = " ORDER BY StudentFullname"

        tmpSQL = "SELECT PPCS.StudentID,StudentFullname,MYKAD,AlumniID,DOB_Year,SchoolName,SchoolCity,SchoolState,StudentReligion,PPCS_Course.CourseCode,PPCS_Class.ClassCode FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON PPCS.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        strWhere = " WHERE PPCS.ClassID=" & Server.HtmlEncode(Request.Cookies("ppcs_classid").Value) & " AND PPCS.PPCSStatus='LAYAK'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

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

        '--get ClassID
        Dim strClassID As String = Server.HtmlEncode(Request.Cookies("ppcs_classid").Value)

        '--get class year
        Dim strPPCSDate As String = ""
        strSQL = "SELECT PPCSDate FROM PPCS_Class WHERE ClassID=" & strClassID
        strPPCSDate = oCommon.getFieldValue(strSQL)

        '--get CourseID
        Dim strCourseID As String = ""
        strSQL = "SELECT CourseID FROM PPCS_Class WHERE ClassID=" & strClassID
        strCourseID = oCommon.getFieldValue(strSQL)

        '--Response.Write(strKeyID)
        Dim strMod As String = Request.QueryString("mod")
        Select Case strMod
            Case "01"
                Response.Redirect("studentprofile.view.aspx?studentid=" & strKeyID)
            Case "02"
            Case "03"
            Case "04"
                Response.Redirect("laporan.keseluruhan.view.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & strPPCSDate & "&courseid=" & strCourseID & "&classid=" & strClassID)
            Case "05"
                Response.Redirect("laporan.harian.create.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & strPPCSDate & "&courseid=" & strCourseID & "&classid=" & strClassID)
            Case "06"
                Response.Redirect("laporan.mingguan.create.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & strPPCSDate & "&courseid=" & strCourseID & "&classid=" & strClassID)
            Case "07"
                Response.Redirect("laporan.akhir.create.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & strPPCSDate & "&courseid=" & strCourseID & "&classid=" & strClassID)
            Case "08"
                Response.Redirect("laporan.keseluruhan.view.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & strPPCSDate & "&courseid=" & strCourseID & "&classid=" & strClassID)
            Case "09"
                Response.Redirect("student.profile.view.aspx?mod=" & strMod & "&studentid=" & strKeyID & "&ppcsdate=" & strPPCSDate & "&courseid=" & strCourseID & "&classid=" & strClassID)

            Case Else
                lblMsg.Text = "Invalid page module! " & strMod
        End Select


    End Sub

End Class