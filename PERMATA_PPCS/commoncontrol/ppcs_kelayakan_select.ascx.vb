Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class ppcs_kelayakan_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim strUserType As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                strUserType = Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
                lblUserType.Text = strUserType

                master_examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

            End If

        Catch ex As Exception
            lblMsg.Text = "Page_Load:" & ex.Message
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

            '--ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub master_examyear_list()
        strSQL = "SELECT ExamYear FROM master_examyear ORDER BY ExamYear ASC"

        '--debug
        'Response.Write("examyear_list:" & strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "ExamYear"
            ddlExamYear.DataBind()

            ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

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


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada pelajar yang menduduki Ujian UKM2 bagi tahun lahir ini."
            Else
                lblMsg.Text = "Senarai pelajar yang menduduki Ujian UKM2 dan DONE. Jumlah pelajar#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY UKM2.TotalPercentage DESC"

        tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentRace,UKM2.TotalScore as UKM2TotalScore,UKM2.TotalPercentage as UKM2TotalPercentage,UKM1.TotalScore as UKM1TotalScore,UKM1.TotalPercentage as UKM1TotalPercentage FROM UKM2"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM2.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN UKM1 ON UKM2.StudentID=UKM1.StudentID AND UKM1.ExamYear='" & ddlExamYear.Text & "'"
        tmpSQL += " WHERE UKM2.ExamYear='" & ddlExamYear.Text & "'"

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND StudentProfile.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblLayak As Label

                Dim i As Integer = e.Row.RowIndex + 1
                Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

                lblLayak = e.Row.FindControl("lblLayak")
                lblLayak.Text = isLayak(strKeyID)

                '--security
                If Not lblUserType.Text = "ADMIN" Then
                    hideColumn()
                End If

            End If

        Catch ex As Exception
            lblDebug.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub hideColumn()
        Dim nCol As Integer = datRespondent.Rows.Count
        datRespondent.Columns(5).Visible = False
        datRespondent.Columns(6).Visible = False

    End Sub


    Private Function isLayak(ByVal strStudentID As String) As String
        Dim strReturn As String = ""
        strSQL = "SELECT StudentID FROM PPCS WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            strReturn = ""
        Else
            strReturn = "N"
        End If

        ''--get the date
        strSQL = "SELECT PPCSDate FROM PPCS WHERE StudentID='" & strStudentID & "' AND ExamYear='" & ddlExamYear.Text & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strReturn & " " & strRet

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("studentprofile.view.aspx?studentid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkTest As CheckBox = CType(sender, CheckBox)
        Dim grdRow As GridViewRow = CType(chkTest.NamingContainer, GridViewRow)

        If (chkTest.Checked) Then
        Else
        End If

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
                        lblMsg.Text = "Berjaya melayakkan semua pelajar yang dipilih."
                    Else
                        lblMsg.Text += "NOK:" & strID & vbCrLf
                    End If

                End If
            End If
        Next

        'UncheckAll()
        strRet = BindData(datRespondent)

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
                        lblMsg.Text = "Berjaya reset kelayakan pelajar kepada TIDAK LAYAK."     ''& strID
                    Else
                        lblMsg.Text += "NOK:" & strID & vbCrLf
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
        ''--check duplicate entry. no duplicate studentid and examyear
        strSQL = "SELECT StudentID FROM PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = False Then
            strSQL = "INSERT INTO PPCS (StudentID,PPCSDate) VALUES('" & strStudentID & "','" & ddlPPCSDate.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "Error: " & strRet & strSQL
                Return False
            End If
        End If

        Return True

    End Function

    Private Function setTidakLayak(ByVal strStudentID As String) As Boolean

        ''--insert UKM2
        strSQL = "DELETE PPCS WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "Error: " & strRet & strSQL
            Return False
        End If

        Return True

    End Function

    Private Function getSQLMark() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY a.TotalScore DESC"

        tmpSQL = "SELECT b.StudentID,b.StudentFullname,b.MYKAD,b.AlumniID,b.DOB_Year,b.StudentRace,a.TotalScore as ukm2TotalScore,a.TotalPercentage as ukm2TotalPercentage,c.TotalScore as ukm1TotalScore,c.TotalPercentage as ukm1TotalPercentage FROM UKM2 a,StudentProfile b,UKM1 c"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.StudentID=c.StudentID AND a.Status='DONE' AND a.ExamYear='" & ddlExamYear.Text & "'"

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND b.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        getSQLMark = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQLMark

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