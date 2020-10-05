Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class ukm2_kelayakan_sekolah_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblExamYear.Text = Request.QueryString("examyear")

            master_dobyear_list()
            ddlDOB_Year.Text = "ALL"

            strRet = BindData(datRespondent)
        End If

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai."
            Else
                lblMsg.Text = "Jumlah Rekod #:" & myDataSet.Tables(0).Rows.Count
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
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strLayak As String = "(SELECT IsLayak FROM UKM2 WHERE UKM2.StudentID=UKM1.StudentID AND ExamYear='" & Request.QueryString("examyear") & "') as isLayak"
        Dim strOrder As String = ""

        tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentReligion,UKM1.Status,UKM1.TotalScore,UKM1.ExamStart,UKM1.ExamEnd,UKM1.QuestionYear,UKM1.ModA,UKM1.ModB,UKM1.ModC,UKM1.TotalPercentage,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,SchoolProfile.SchoolAddress,SchoolProfile.SchoolPostcode,SchoolProfile.SchoolCity,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolType,SchoolProfile.SchoolNoTel,SchoolProfile.SchoolNoFax,SchoolProfile.SchoolLokasi," & strLayak & " FROM UKM1"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM1.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON UKM1.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere += " WHERE UKM1.ExamYear='" & Request.QueryString("examyear") & "'"
        strWhere += " AND UKM1.SchoolID='" & Request.QueryString("schoolid") & "'"

        '--iscount=True
        If Request.QueryString("iscount") = "True" Then
            strWhere += " AND UKM1.IsCount=1"
        End If

        '--DOB_Year
        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND UKM1.DOB_Year='" & ddlDOB_Year.Text & "'"
        End If

        strOrder = " ORDER BY TotalScore DESC"
        getSQL = tmpSQL & strWhere & strOrder

        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function isLayak(ByVal strStudentID As String) As String
        strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        If oCommon.isExist(strSQL) = True Then
            Return "Y"
        Else
            Return "N"
        End If

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLayak.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

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
                    If setLayak(strID) = True Then
                        lblMsg.Text = "Berjaya melayakkan semua pelajar yang dipilih."
                    Else
                        lblMsg.Text += "NOK:" & strID & vbCrLf
                    End If

                End If
            End If
        Next

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnTidakLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakLayak.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

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
                    'Response.Write(strID)& "|"
                    If setTidakLayak(strID) = True Then
                        lblMsg.Text = "Berjaya reset kelayakan pelajar kepada TIDAK LAYAK."     ''& strID
                    Else
                        lblMsg.Text += "NOK:" & strID & vbCrLf
                    End If
                End If
            End If
        Next

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    Private Sub UncheckAll()
        Dim row As GridViewRow
        For Each row In datRespondent.Rows
            Dim chkUncheck As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            chkUncheck.Checked = False
        Next

    End Sub


    Private Function setLayak(ByVal strStudentID As String) As Boolean
        '--get SchoolID
        Dim strSchoolID As String = ""
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & strStudentID & "'"
        strSchoolID = oCommon.getFieldValue(strSQL)

        ''--insert UKM2
        strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,SchoolID,Status) VALUES('" & strStudentID & "','" & Request.QueryString("examyear") & "','" & strSchoolID & "','NEW')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        ''--insert UKM2_Answer
        strSQL = "INSERT INTO UKM2_Answer (StudentID,ExamYear) VALUES('" & strStudentID & "','" & Request.QueryString("examyear") & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        '--school information
        UKM2_Update(strStudentID)

        '--update UKM1
        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET isLayak='Y' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True
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


    Private Function setTidakLayak(ByVal strStudentID As String) As Boolean
        ''--semak sama ada telah menamatkan ujian bagi tahun sebagaimana dlm web.config
        strSQL = "SELECT StudentID FROM UKM2 WHERE Status='DONE' AND StudentID='" & strStudentID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Pelajar telah menamatkan Ujian UKM2 bagi tahun " & Request.QueryString("examyear")
            Return False
        End If

        ''--insert UKM2
        strSQL = "DELETE UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        ''--insert UKM2_Answer
        strSQL = "DELETE UKM2_Answer WHERE StudentID='" & strStudentID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        '--Export Detail
        '--Dim strQ As String = "a.Q001,a.Q002,a.Q003,a.Q004,a.Q005,a.Q006,a.Q007,a.Q008,a.Q009,a.Q010,a.Q011,a.Q012,a.Q013,a.Q014,a.Q015,a.Q016,a.Q017,a.Q018,a.Q019,a.Q020,a.Q021,a.Q022,a.Q023,a.Q024,a.Q025,a.Q026,a.Q027,a.Q028,a.Q029,a.Q030,a.Q031,a.Q032,a.Q033,a.Q034,a.Q035,a.Q036,a.Q037,a.Q038,a.Q039,a.Q040,a.Q041,a.Q042,a.Q043,a.Q044,a.Q045,a.Q046,a.Q047,a.Q048,a.Q049,a.Q050,a.Q051,a.Q052,a.Q053,a.Q054,a.Q055,a.Q056,a.Q057,a.Q058,a.Q059,a.Q060,a.Q061,a.Q062,a.Q063,a.Q064,a.Q065,a.Q066,a.Q067,a.Q068,a.Q069,a.Q070,a.Q071,a.Q072,a.Q073,a.Q074,a.Q075,a.Q076,a.Q077,a.Q078,a.Q079,a.Q080,a.Q081,a.Q082,a.Q083,a.Q084,a.Q085,a.Q086,a.Q087,a.Q088,a.Q089,a.Q090,a.Q091,a.Q092,a.Q093,a.Q094,a.Q095,a.Q096,a.Q097,a.Q098,a.Q099,a.Q100,a.Q101,a.Q102,a.Q103,a.Q104,a.Q105,a.Q106,a.Q107,a.Q108,a.Q109,a.Q110,a.Q111,a.Q112,a.Q113,a.Q114,a.Q115,a.Q116,a.Q117,a.Q118,a.Q119,a.Q120,a.Q121,a.Q122,a.Q123,a.Q124,a.Q125,a.Q126,a.Q127,a.Q128,a.Q129,a.Q130,a.Q131,a.Q132"

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