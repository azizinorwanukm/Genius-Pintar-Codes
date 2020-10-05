Imports System.Data.SqlClient

Partial Public Class studentprofile_search
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim i As Integer
        Try
            If Not IsPostBack Then
                setDdlPpcs()
                setDdlClass()
                setDdlExams()
                lblMsg.Text = ""
                lblMsgTop.Text = ""
                strRet = BindData(datRespondent)
                ''Tukar value of Jantina
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim jantina As Label = CType(datRespondent.Rows(i).Cells(4).FindControl("student_sex"), Label)
                    If jantina.Text = "1" Then
                        jantina.Text = "Lelaki"
                    ElseIf jantina.Text = "0" Then
                        jantina.Text = "Perempuan"
                    End If
                Next

            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet

        Dim strConn2 As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn2 As SqlConnection = New SqlConnection(strConn2)
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn2)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn2.Close()
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
        Dim strSQL As String
        Dim strWhere As String = ""
        strSQL = "SELECT A.ppcsid,B.studentfullname, B.mykad, B.StudentGender,B.AlumniID, K.ClassCode,floor(DATEDIFF(year,B.DOB_year,GETDATE())) AS age "
        strSQL += " FROM PPCS A LEFT JOIN StudentProfile B ON B.StudentID = A.StudentID "
        strSQL += " LEFT JOIN PPCS_Class K on A.ClassID = K.ClassID "

        strWhere += " WHERE A.PPCSStatus = 'LAYAK' AND A.StatusTawaran = 'TERIMA' and A.PPCSDate ='" & ddlppcsDate.SelectedValue & "' "

        ''search
        If Not txtsearchAge.Text.Length = 0 Then
            strWhere += " and B.DOB_year = (YEAR(GetDate()) - " & txtsearchAge.Text & ")  "
        End If

        If Not txtsearch.Text.Length = 0 Then
            strWhere += "and  B.MYKAD LIKE '%" & txtsearch.Text & "%' "
        End If

        If Not ddlKelas.SelectedValue = "0" Then
            strWhere += "and A.ClassID = '" & ddlKelas.SelectedValue & "' "
        End If

        getSQL = strSQL + strWhere
        ''--debug
        'Response.Write(getSQL)

        ''Debug.WriteLine(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim encyptid As String = oDes.EncryptData(strKeyID)
        ''Debug.WriteLine (encyptid)
        Try
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("ukm3_admin.studentprofileview.aspx?studentid=" & encyptid)
                Case "Instruktor Ra PPCS"
                    Response.Redirect("ukm3_Rapcs.masukmarkah.aspx?studentid=" & encyptid)
                Case "Instruktor KPP"
                    Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & encyptid)
                Case "Instruktor PPCS"
                    Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & encyptid)
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)

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

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click

        Dim message As String = ""

        For i = 0 To datRespondent.Rows.Count - 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(4).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then

                Try
                    If chkUpdate.Checked = True Then
                        ' Get the values of textboxes using findControl
                        Dim ppcsid As String = datRespondent.DataKeys(i).Value.ToString

                        Dim attachmentsTable = New DataTable

                        Dim mAdapter As New SqlDataAdapter

                        Dim query As String = "SELECT TOP 1 B.studentfullname, B.mykad, CASE WHEN B.studentgender = 'LELAKI' THEN 1 ELSE 0 END StudentGender, B.DOB_Day,B.DOB_Month,B.DOB_Year,B.StudentID,B.AlumniID, B.StudentEmail "
                        query += "FROM PPCS A join StudentProfile B ON B.StudentID = A.StudentID WHERE A.PPCSID = @ppcsid"

                        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

                        Using mConn As New SqlConnection(strconn)
                            Using mCmd As New SqlCommand(query, mConn)
                                mCmd.Parameters.Add(New SqlParameter("@ppcsid", ppcsid))
                                mConn.Open()
                                mAdapter.SelectCommand = mCmd
                                mAdapter.Fill(attachmentsTable)
                            End Using
                        End Using

                        Dim fullName As String = Trim(attachmentsTable.Rows(0).Item(0).ToString)
                        Dim mykad As String = Trim(attachmentsTable.Rows(0).Item(1).ToString)
                        Dim gender As String = Trim(attachmentsTable.Rows(0).Item(2).ToString)
                        Dim dobday As String = Trim(attachmentsTable.Rows(0).Item(3).ToString)
                        Dim dobmonth As String = Trim(attachmentsTable.Rows(0).Item(4).ToString)
                        Dim dobyear As String = Trim(attachmentsTable.Rows(0).Item(5).ToString)
                        Dim guid As String = attachmentsTable.Rows(0).Item(6).ToString
                        Dim alumniID As String = Trim(attachmentsTable.Rows(0).Item(7).ToString)
                        Dim StudentEmail As String = Trim(attachmentsTable.Rows(0).Item(8).ToString)

                        Dim isExist As Integer = CType(oCommon.getFieldValue("select count(*) from student_info where student_Mykad = '" & mykad & "'"), Integer)

                        Dim studentId As String = ""

                        If isExist = 0 Then
                            query = "INSERT INTO ukm3.dbo.student_info (student_Name,student_Mykad,student_sex,guid,password,alumniID) OUTPUT INSERTED.std_ID "
                            query += " VALUES (@fullName,@mykad,@gender,@guid,@mykad,@alumniID) "

                            attachmentsTable = New DataTable

                            Using mConn As New SqlConnection(strconn)
                                Using mCmd As New SqlCommand(query, mConn)
                                    mCmd.Parameters.Add(New SqlParameter("@fullName", fullName))
                                    mCmd.Parameters.Add(New SqlParameter("@mykad", mykad))
                                    mCmd.Parameters.Add(New SqlParameter("@gender", gender))
                                    mCmd.Parameters.Add(New SqlParameter("@guid", guid))
                                    mCmd.Parameters.Add(New SqlParameter("@alumniID", alumniID))
                                    mConn.Open()
                                    mAdapter.SelectCommand = mCmd
                                    mAdapter.Fill(attachmentsTable)
                                End Using
                            End Using

                            studentId = attachmentsTable.Rows(0).Item(0).ToString

                        Else
                            studentId = oCommon.getFieldValue("SELECT std_ID FROM student_info WHERE student_Mykad = '" & mykad & "'")
                        End If

                        query = "SELECT id FROM UKM3 WHERE student_id = " & studentId & " AND session_id = " & ddlSession.SelectedValue

                        If oCommon.isExist(query) Then
                            query = "UPDATE UKM3 SET active=1 WHERE student_id = " & studentId & " AND session_id = " & ddlSession.SelectedValue
                            oCommon.ExecuteSQL(query)
                        Else
                            query = "INSERT INTO UKM3 (active,student_id,session_id,std_guid) VALUES (1," & studentId & ", " & ddlSession.SelectedValue & ",'" & guid & "')"

                            oCommon.ExecuteSQL(query)
                        End If

                        ''Insert into EQTest
                        strSQL = "SELECT EQTestID FROM permatapintar.dbo.EQTest WHERE LoginID = '" & guid & "' AND SurveyID = (SELECT configString FROM permatapintar.dbo.master_Config WHERE configCode='EQTest_SurveyID') "

                        If oCommon.isExist(strSQL) = False Then
                            strSQL = "INSERT INTO EQTest (LoginID,StudentID,PPCSDate,Fullname,AlumniID,EmailAdd,SurveyID) "
                            strSQL += " VALUES (@strStudentID,@strStudentID,@PpcsDate,@studentName,@AlumniID,@StudentEmail,@SurveyID)"

                            Using mConn As New SqlConnection(strconn)
                                Using mCmd As New SqlCommand(strSQL, mConn)
                                    mCmd.Parameters.Add(New SqlParameter("@strStudentID", guid))
                                    mCmd.Parameters.Add(New SqlParameter("@PpcsDate", ddlppcsDate.Text))
                                    mCmd.Parameters.Add(New SqlParameter("@studentName", fullName))
                                    mCmd.Parameters.Add(New SqlParameter("@AlumniID", alumniID))
                                    mCmd.Parameters.Add(New SqlParameter("@StudentEmail", StudentEmail))
                                    mCmd.Parameters.Add(New SqlParameter("@SurveyID", oCommon.getAppsettings("EQTest_SurveyID")))
                                    mConn.Open()
                                    mCmd.ExecuteNonQuery()
                                End Using
                            End Using
                        End If
                        message = message & "<br/>" & fullName & " registered successfully"

                        ''lblDebug.Text += lblMsg.Text
                    End If
                Catch ex As Exception

                End Try
            End If
        Next

        lblMsg.Text = message

    End Sub

    Private Sub setDdlExams()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id, sessionName FROM UKM3Session ORDER BY isCurrent DESC"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        For k = 0 To quantity - 1
            ddlSession.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

    End Sub

    Private Sub setDdlPpcs()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT PPCSDate FROM PPCS GROUP BY PPCSDate"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        For k = 0 To quantity - 1
            ddlPPCSDate.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(0).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        Dim currentPpcs As String = Commonfunction.getSingleCellValue("SELECT configString FROM master_Config WHERE configCode = 'DefaultPPCSDate'")
        ddlppcsDate.SelectedValue = currentPpcs

    End Sub

    Private Sub ddlPPCS_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPPCSDate.TextChanged
        setDdlClass()
    End Sub

    Private Sub setDdlClass()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT A.ClassID, B.ClassCode FROM (SELECT ClassID FROM PPCS WHERE PPCSDate = '" & ddlppcsDate.SelectedValue & "' GROUP BY ClassID) A "
        query += " LEFT JOIN PPCS_Class B ON A.ClassID = B.ClassID WHERE A.ClassID IS NOT NULL"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        ddlKelas.Items.Clear()

        ddlKelas.Items.Add(New ListItem("-- Pilih Kelas --", 0))

        For k = 0 To quantity - 1
            ddlKelas.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next
    End Sub

End Class