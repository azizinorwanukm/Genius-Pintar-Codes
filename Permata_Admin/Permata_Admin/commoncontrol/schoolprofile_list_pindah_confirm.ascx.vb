Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class schoolprofile_list_pindah_confirm
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnPindah.Attributes.Add("onclick", "return confirm('Pasti ingin memindahkan rekod sekolah tersebut?');")

        If Not IsPostBack Then
            lblMsgTop.Text = ""
            lblExamYear.Text = oCommon.getAppsettings("DefaultExamYear")
            strRet = BindData(datRespondent)

        End If

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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT SchoolPindah.SchoolID,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,SchoolProfile.SchoolName,SchoolProfile.SchoolCode,SchoolProfile.SchoolLokasi,(SELECT COUNT(*) FROM StudentSchool WHERE StudentSchool.SchoolID=SchoolProfile.SchoolID) AS Jumlah FROM SchoolPindah"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON SchoolPindah.SchoolID=SchoolProfile.SchoolID"
        strWhere += " WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        strOrder = " ORDER BY SchoolProfile.SchoolName"

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
        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("kpm.schoolprofile.students.list.aspx?examyear=" & lblExamYear.Text & "&schoolid=" & strKeyID)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.schoolprofile.students.list.aspx?examyear=" & lblExamYear.Text & "&schoolid=" & strKeyID)
                Case "JPN"
                    Response.Redirect("jpn.schoolprofile.students.list.aspx?examyear=" & lblExamYear.Text & "&schoolid=" & strKeyID)
                Case "KPM"
                    Response.Redirect("kpm.schoolprofile.students.list.aspx?examyear=" & lblExamYear.Text & "&schoolid=" & strKeyID)
                Case "KPT"
                Case "MRSM"
                Case "ASASI"
                Case Else
                    lblMsg.Text = "Invalid User Type: " & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
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
        strSQL = "DELETE SchoolPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error:" & strRet
        Else
            lblMsg.Text = "BERJAYA membatalkan perpindahan sekolah."
        End If

        lblMsgTop.Text = lblMsg.Text
        '--refresh list
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnPindah_Click(sender As Object, e As EventArgs) Handles btnPindah.Click
        lblMsgTop.Text = ""
        lblMsg.Text = ""

        '--Pindah pelajar ke sekolah baru
        StudentSchool_update()

        '--update UKM1 only
        If chkUKM1.Checked = True Then
            UKM1_UpdateSchoolID()
        End If

        '--update UKM2 only
        If chkUKM2.Checked = True Then
            UKM2_UpdateSchoolID()
        End If

        '--UPDATE SchoolProfile IsDeleted='Y'
        If chkDelete.Checked = True Then
            Schoolprofile_delete()
        End If

        '--delete temp records
        SchoolPindah_delete()

    End Sub

    Private Sub Schoolprofile_delete()
        strSQL = "UPDATE SchoolProfile SET IsDeleted='Y' WHERE EXISTS (SELECT SchoolID FROM SchoolPindah WHERE SchoolProfile.SchoolID=SchoolPindah.SchoolID AND SchoolPindah.PindahID='" & Request.QueryString("pindahid") & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "Err Schoolprofile_delete: " & strRet & "<br/>"
        End If

    End Sub

    Private Sub StudentSchool_update()
        Dim bSuccess As Boolean = True
        Dim strStudentID As String = ""
        Dim strOldSchoolID As String = ""
        Dim strNewSchoolID As String = Request.QueryString("schoolid")
        If strNewSchoolID.Length = 0 Then
            lblMsgTop.Text = "Tiada rekod sekolah. Sila pilih sekolah terlebih dahulu!"
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
                    If Not IsDBNull(ds.Tables(0).Rows(i).Item("SchoolID").ToString) Then
                        strOldSchoolID = ds.Tables(0).Rows(i).Item("SchoolID").ToString
                        strSQL = "UPDATE StudentSchool SET SchoolID='" & strNewSchoolID & "' WHERE SchoolID='" & strOldSchoolID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsgTop.Text += "Error:" & strRet & "<br/>"
                            bSuccess = False
                            Exit For
                        Else

                        End If
                    End If
                Next

                '--success
                If bSuccess = True Then
                    lblMsgTop.Text += "BERJAYA kemaskini maklumat sekolah pelajar.<br/>"
                End If
            End If

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try

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
        strSQL = "SELECT * FROM SchoolPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strRowValue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--loop thru dataset
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds.Tables(0).Rows(i).Item("SchoolID").ToString) Then
                        strOldSchoolID = ds.Tables(0).Rows(i).Item("SchoolID").ToString

                        '--update UKM1 Schoolprofile
                        If ukm1_schoolprofile_update(strNewSchoolID, strOldSchoolID) = False Then
                            lblMsg.Text += "GAGAL kemaskini maklumat sekolah UKM1. " & strOldSchoolID & "<br/>"
                        Else
                            lblMsg.Text += "BERJAYA kemaskini maklumat sekolah UKM1. " & strOldSchoolID & "<br/>"
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
        strSQL = "SELECT * FROM SchoolPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strRowValue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--loop thru dataset
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds.Tables(0).Rows(i).Item("SchoolID").ToString) Then
                        strOldSchoolID = ds.Tables(0).Rows(i).Item("SchoolID").ToString

                        '--update UKM1 Schoolprofile
                        If ukm2_schoolprofile_update(strNewSchoolID, strOldSchoolID) = False Then
                            lblMsg.Text += "GAGAL kemaskini maklumat sekolah UKM2. " & strOldSchoolID & "<br/>"
                        Else
                            lblMsg.Text += "BERJAYA kemaskini maklumat sekolah UKM2. " & strOldSchoolID & "<br/>"
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
        Dim strOldSchoolID As String = ""
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
                    If Not IsDBNull(ds.Tables(0).Rows(i).Item("SchoolID").ToString) Then
                        strOldSchoolID = ds.Tables(0).Rows(i).Item("SchoolID").ToString

                        '--update UKM1 Schoolprofile
                        If ukm1_schoolprofile_update(strNewSchoolID, strOldSchoolID) = False Then
                            lblMsg.Text += "Err. NewSchoolID:" & strNewSchoolID
                            bSuccess = False
                        End If

                        '--update UKM2 Schoolprofile
                        If ukm2_schoolprofile_update(strNewSchoolID, strOldSchoolID) = False Then
                            lblMsg.Text += "Err. NewSchoolID:" & strNewSchoolID
                            bSuccess = False
                        End If

                    End If
                Next

                '--success
                If bSuccess = True Then
                    lblMsgTop.Text += "BERJAYA kemaskini maklumat sekolah UKM1 pelajar.<br/>"
                End If

            End If

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try

    End Sub

    '--update ukm1 schoolprofile
    Private Function ukm1_schoolprofile_update(ByVal strNewSchoolID As String, ByVal strOldSchoolID As String) As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strNewSchoolID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text += "Err. ukm1_schoolprofile_update" & strRet & ":" & UBound(arSchoolProfile).ToString & "<br/>"
            Return False
        End If

        ''update UKM1 to new schoolid profile

        If lblExamYear.Text = getUKM1Year Then
            strSQL = "UPDATE UKM1_" & getUKM1Year & " WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE schoolid='" & strOldSchoolID & "' AND ExamYear='" & lblExamYear.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE schoolid='" & strOldSchoolID & "' AND ExamYear='" & lblExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "Err. ukm1_schoolprofile_update:" & strRet & "<br/>"
            Return False
        End If

        Return True

    End Function

    '--update ukm2 schoolprofile
    Private Function ukm2_schoolprofile_update(ByVal strSchoolIDNew As String, ByVal strSchoolIDOld As String) As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strSchoolIDNew & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text += "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString & "<br/>"
            Return False
        End If

        ''update UKM2 to new schoolid profile
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE schoolid='" & strSchoolIDOld & "' AND ExamYear='" & lblExamYear.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text += "UKM2 system error:" & strRet & "<br/>"
            Return False
        End If

        Return True

    End Function

    Private Sub SchoolPindah_delete()
        Dim bSuccess As Boolean = True

        '--DELETE StudentPindah
        strSQL = "DELETE SchoolPindah WHERE PindahID='" & Request.QueryString("pindahid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsgTop.Text += "Err. SchoolPindah_delete:" & strRet
        End If

    End Sub

End Class