Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class ppcs_alumni_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnLayak.Attributes.Add("onclick", "return confirm('Pilih Tahun Ujian dan Sessi PPCS. Pasti ingin MELAYAKKAN pelajar-pelajar tersebut?');")
        btnTidakLayak.Attributes.Add("onclick", "return confirm('Pilih Tahun Ujian dan Sessi PPCS. Pasti TIDAK MELAYAKKAN pelajar-pelajar tersebut? Maklumat PPCS jika ada akan terhapus.');")

        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = ConfigurationManager.AppSettings("DefaultPPCSDate")

                '--must select age
                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub


    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentFullname ASC"

        '-AlumniID ada sahaja
        tmpSQL = "SELECT StudentID,StudentFullname,MYKAD,DOB_Year,StudentRace,StudentGender,AlumniID FROM StudentProfile"
        strWhere = " WITH (NOLOCK) WHERE AlumniID IS NOT NULL"

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
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

    Private Function getPPCSDate(ByVal strStudentID As String) As String
        ''--get the date
        strSQL = "SELECT PPCSDate FROM PPCS WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet

    End Function

    Private Function getPPCSDateValues(ByVal strStudentID As String) As String
        Dim strValues As String = ""
        Dim i As Integer = 0

        strSQL = "SELECT PPCSDate FROM PPCS WHERE StudentID='" & strStudentID & "'"

        Try
            Dim comm As New SqlCommand(strSQL, objConn)
            objConn.Open()

            Dim reader As SqlDataReader = comm.ExecuteReader()
            While reader.Read()
                strValues += reader.GetValue(0).ToString() & ","
            End While
            reader.Close()

            If strValues.Length > 0 Then
                strValues = strValues.Substring(0, strValues.Length - 1)
            End If
            Return strValues
        Catch ex As Exception
            Return ex.Message
        Finally
            objConn.Close()
        End Try

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ppcs.alumni.studentprofile.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

    Private Sub btnLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLayak.Click
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
                    If setLayak(strID) = True Then
                        lblMsg.Text = "Berjaya. Klik [Cari] untuk REFERESH."
                    Else
                        lblMsg.Text = "NOK:" & strID
                        Exit For
                    End If

                End If
            End If
        Next

        'UncheckAll()
        'strRet = BindData(datRespondent)

    End Sub

    Private Sub btnTidakLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakLayak.Click
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
                    'Response.Write(strID)& "|"
                    If setTidakLayak(strID) = True Then
                        lblMsg.Text = "Berjaya. Klik [Cari] untuk REFERESH."
                    Else
                        lblMsg.Text = "NOK:" & strID & vbCrLf
                        Exit For
                    End If
                End If
            End If
        Next

        'UncheckAll()
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
        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using connection As New SqlConnection(strconn)
            connection.Open()

            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction("TxnStart")

            ' Must assign both transaction object and connection 
            ' to Command object for a pending local transaction.
            command.Connection = connection
            command.Transaction = transaction
            command.CommandTimeout = 120

            Try
                '--1 PPCS insert if not exist
                strSQL = "SELECT StudentID FROM PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
                If oCommon.isExist(strSQL) = False Then
                    '--INSERT
                    strSQL = "INSERT INTO PPCS (StudentID,PPCSDate,PPCSStatus,StatusTawaran) VALUES('" & strStudentID & "','" & ddlPPCSDate.Text & "','LAYAK',NULL)"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                ''--get DOB_Year
                Dim strDOB_Year As String = ""
                strSQL = "SELECT DOB_Year FROM StudentProfile WHERE StudentID='" & strStudentID & "'"
                strDOB_Year = oCommon.getFieldValue(strSQL)

                '--2 UKM1 
                strSQL = "SELECT StudentID FROM UKM1 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
                If oCommon.isExist(strSQL) = False Then
                    '--INSERT
                    strSQL = "INSERT INTO UKM1 (StudentID,ExamYear,QuestionYear,HostAddress,HostName,Browser,SelectedLang,Status,DOB_Year) VALUES('" & strStudentID & "','" & oCommon.getAppsettings("DefaultExamYear") & "','" & oCommon.getAppsettings("DefaultExamYear") & "','" & Request.UserHostAddress & "','" & Request.UserHostName & "','" & Request.UserAgent & "','ms-MY','NEW','" & strDOB_Year & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()

                    strSQL = "INSERT INTO UKM1_Answer (StudentID,ExamYear) VALUES('" & strStudentID & "','" & oCommon.getAppsettings("DefaultExamYear") & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                '--3 UKM2
                strSQL = "SELECT StudentID FROM UKM2 WHERE StudentID='" & strStudentID & "' AND ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "'"
                If oCommon.isExist(strSQL) = False Then
                    '--INSERT
                    strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,IsHadir,PusatCode,SchoolID,TarikhUjian,SessiUKM2,Status) VALUES ('" & strStudentID & "','" & oCommon.getAppsettings("DefaultExamYear") & "','Y','NA','NA','NA','NA','NEW')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()

                    strSQL = "INSERT INTO UKM2_Answer (StudentID,ExamYear) VALUES('" & strStudentID & "','" & oCommon.getAppsettings("DefaultExamYear") & "')"
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                End If

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                Return True
            Catch ex As Exception
                'Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
                'Console.WriteLine("  Message: {0}", ex.Message)
                lblMsg.Text = "Error:" & ex.Message

                ' Attempt to roll back the transaction. 
                Try
                    transaction.Rollback()

                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    'Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    'Console.WriteLine("  Message: {0}", ex2.Message)

                    lblMsg.Text = "Rollback:" & ex2.Message
                    Return False
                End Try
            End Try
        End Using

    End Function

    Private Function setTidakLayak(ByVal strStudentID As String) As Boolean

        ''--delete PPCS sahaja. UKM1 dan UKM2 maintain
        strSQL = "DELETE PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function


    Private Function ExportData(ByVal dsTable As DataSet, ByVal strTitle As String) As String
        ''-Dim strFilename As String = Server.MapPath(".") & "log\" & "Export." & oCommon.getRandom & ".txt"
        Dim strFilename As String = strTitle & oCommon.getRandom & ".txt"

        Try
            ' Export all the details to xls
            Dim objExport As New RKLib.ExportData.Export("Web")
            Dim dtRespondent As DataTable = dsTable.Tables("mytable").Copy()
            objExport.ExportDetails(dtRespondent, Export.ExportFormat.CSV, strFilename)

            Return strFilename
        Catch Ex As Exception
            Return Ex.Message
        End Try

    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

End Class