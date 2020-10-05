Imports System.Data.SqlClient

Public Class studentpindah_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnPindah.Attributes.Add("onclick", "return confirm('Pasti ingin memindahkan semua pelajar tersebut?');")

        Try
            If Not IsPostBack Then
                lblExamYear.Text = Request.QueryString("examyear")
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
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
        Dim strOrder As String = ""

        tmpSQL = "SELECT * FROM StudentPindah"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON StudentPindah.StudentID=StudentProfile.StudentID"
        strWhere += " WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        strOrder = " ORDER BY StudentProfile.StudentFullname"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
                Case "ADMINOP"
                    Response.Redirect("studentprofile.view.aspx?studentid=" & strKeyID)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID)
                Case Else

            End Select
        Catch ex As Exception

        End Try

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

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        lblMsgTop.Text = ""
        strSQL = "DELETE StudentPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error:" & strRet
        Else
            lblMsg.Text = "Berjaya membatalkan perpindahan pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnPindah_Click(sender As Object, e As EventArgs) Handles btnPindah.Click
        lblMsgTop.Text = ""
        lblMsg.Text = ""

        '--update StudentSchool
        StudentSchool_update()

        '--update UKM1 only
        If chkUKM1.Checked = True Then
            UKM1_UpdateSchoolID()
        End If

        '--update UKM2 only
        If chkUKM2.Checked = True Then
            UKM2_UpdateSchoolID()
        End If

        '--delete temp records
        StudentPindah_delete()
    End Sub

    Private Sub UKM1_UpdateSchoolID()
        Dim bSuccess As Boolean = True
        Dim strStudentID As String = ""
        Dim strOldSchoolID As String = ""
        Dim strNewSchoolID As String = Request.QueryString("schoolid")

        If strNewSchoolID.Length = 0 Then
            lblMsgTop.Text += "Tiada rekod sekolah baru. Sila pilih sekolah terlebih dahulu! <br/>"
            Exit Sub
        End If

        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        strSQL = "SELECT * FROM StudentPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strRowValue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--loop thru dataset
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds.Tables(0).Rows(i).Item("StudentID").ToString) Then
                        strStudentID = ds.Tables(0).Rows(i).Item("StudentID").ToString

                        '--update UKM1 Schoolprofile
                        If ukm1_schoolprofile_update(strNewSchoolID, strStudentID) = False Then
                            lblMsg.Text += "GAGAL kemaskini maklumat sekolah UKM1. " & strStudentID & "<br/>"
                        Else
                            lblMsg.Text += "BERJAYA kemaskini maklumat sekolah UKM1. " & strStudentID & "<br/>"
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub UKM2_UpdateSchoolID()
        Dim bSuccess As Boolean = True
        Dim strStudentID As String = ""
        Dim strOldSchoolID As String = ""
        Dim strNewSchoolID As String = Request.QueryString("schoolid")

        If strNewSchoolID.Length = 0 Then
            lblMsgTop.Text += "Tiada rekod sekolah baru. Sila pilih sekolah terlebih dahulu! <br/>"
            Exit Sub
        End If

        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        strSQL = "SELECT * FROM StudentPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strRowValue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--loop thru dataset
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds.Tables(0).Rows(i).Item("StudentID").ToString) Then
                        strStudentID = ds.Tables(0).Rows(i).Item("StudentID").ToString

                        '--update UKM1 Schoolprofile
                        If ukm2_schoolprofile_update(strNewSchoolID, strStudentID) = False Then
                            lblMsg.Text += "GAGAL kemaskini maklumat sekolah UKM2. " & strStudentID & "<br/>"
                        Else
                            lblMsg.Text += "BERJAYA kemaskini maklumat sekolah UKM2. " & strStudentID & "<br/>"
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub StudentSchool_update()
        Dim bSuccess As Boolean = True
        Dim strStudentID As String = ""
        Dim strSchoolID As String = Request.QueryString("schoolid")
        If strSchoolID.Length = 0 Then
            lblMsgTop.Text = "Tiada rekod sekolah. Sila pilih sekolah terlebih dahulu!"
            Exit Sub
        End If

        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        strSQL = "SELECT * FROM StudentPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strRowValue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--loop thru dataset
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds.Tables(0).Rows(i).Item("StudentID").ToString) Then
                        strStudentID = ds.Tables(0).Rows(i).Item("StudentID").ToString
                        strSQL = "UPDATE StudentSchool SET SchoolID='" & strSchoolID & "' WHERE StudentID='" & strStudentID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += "Err StudentSchool_update:" & strRet & "<br/>"
                        Else
                            lblMsg.Text += "BERJAYA kemaskini maklumat sekolah pelajar. " & strStudentID & "<br/>"
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub UKM1_UKM2_update()
        Dim bSuccess As Boolean = True
        Dim strStudentID As String = ""
        Dim strNewSchoolID As String = Request.QueryString("schoolid")
        If strNewSchoolID.Length = 0 Then
            lblMsgTop.Text += "Tiada rekod sekolah. Sila pilih sekolah terlebih dahulu! <br/>"
            Exit Sub
        End If

        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(getSQL, objConn)
        Dim strRowValue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--loop thru dataset
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds.Tables(0).Rows(i).Item("StudentID").ToString) Then
                        strStudentID = ds.Tables(0).Rows(i).Item("StudentID").ToString

                        '--update UKM1 Schoolprofile
                        If ukm1_schoolprofile_update(strNewSchoolID, strStudentID) = False Then
                            lblMsg.Text += "Err. SchoolID:" & strNewSchoolID
                            bSuccess = False
                        End If

                        '--update UKM2 Schoolprofile
                        If ukm2_schoolprofile_update(strNewSchoolID, strStudentID) = False Then
                            lblMsg.Text += "Err. SchoolID:" & strNewSchoolID
                            bSuccess = False
                        End If

                    End If
                Next
            End If

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try

    End Sub

    '--update ukm1 schoolprofile
    Private Function ukm1_schoolprofile_update(ByVal strNewSchoolID As String, ByVal strStudentID As String) As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strNewSchoolID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text += "Err. ukm1_schoolprofile_update" & strRet & "<br/>"
            Return False
        End If

        ''update UKM1 to new schoolid profile
        If lblExamYear.Text = getUKM1Year Then
            strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & lblExamYear.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & lblExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "Err. ukm1_schoolprofile_update:" & strRet & "<br/>"
            Return False
        End If

        Return True

    End Function

    '--update ukm2 schoolprofile
    Private Function ukm2_schoolprofile_update(ByVal strNewSchoolID As String, ByVal strStudentID As String) As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strNewSchoolID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text += "Err ukm2_schoolprofile_update:" & strRet & "<br/>"
            Return False
        End If

        ''update UKM2 to new schoolid profile
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE StudentID='" & strStudentID & "' AND ExamYear='" & lblExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Err ukm2_schoolprofile_update:" & strRet & "<br/>"
            Return False
        End If

        Return True

    End Function

    Private Sub StudentPindah_delete()

        '--DELETE StudentPindah
        strSQL = "DELETE StudentPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsgTop.Text += "Error. StudentPindah_delete:" & strRet
        End If

    End Sub


End Class