Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class pusatujian_student_list
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
        lblTotal.Text = ""

        Try
            If Not IsPostBack Then
                ''--set default
                myCal.Visible = False
                txtTarikhUjian.Text = oCommon.getTodayFormated

                strRet = BindData(datRespondent)

                setAccessRight()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub setAccessRight()

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                pnlBottom.Visible = True
                btnExport.Visible = True
            Case "SUBADMIN"
                pnlBottom.Visible = True
                btnExport.Visible = False
            Case "KPM"
                pnlBottom.Visible = False
                btnExport.Visible = False
            Case "JPN"
                pnlBottom.Visible = False
                btnExport.Visible = False
            Case "UKM"
                pnlBottom.Visible = False
                btnExport.Visible = False
            Case Else
                pnlBottom.Visible = False
                btnExport.Visible = False
                lblMsg.Text = "Invalid user type!" & getUserProfile_UserType()
        End Select

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblTotal.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblTotal.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " Order By e.SchoolName,c.StudentFullname"

        tmpSQL = "SELECT a.StudentID,c.MYKAD,c.StudentFullname,c.DOB_Year,d.SchoolID,e.Schoolname,e.Schoolcity,e.SchoolPPD,IsHadir,ExamYear,SessiUKM2,TarikhUjian,FamilyContactNo,FatherFullname FROM UKM2 a LEFT OUTER JOIN ParentProfile b ON a.StudentID = b.StudentID, StudentProfile c, StudentSchool d, SchoolProfile e"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=c.StudentID AND a.StudentID=d.StudentID AND d.SchoolID=e.SchoolID"

        ''ExamYear
        strWhere += " AND a.ExamYear='" & Request.QueryString("examyear") & "'"
        strWhere += " AND a.PusatCode='" & Request.QueryString("pusatcode") & "'"

        '--MYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND c.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND c.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getSQLExport() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " Order By e.SchoolName,c.StudentFullname"

        tmpSQL = "SELECT e.Schoolname,c.StudentFullname,c.MYKAD,c.DOB_Year,e.Schoolcity,e.SchoolPPD,IsHadir,ExamYear,TarikhUjian,SessiUKM2,FamilyContactNo,FatherFullname FROM UKM2 a LEFT OUTER JOIN ParentProfile b ON a.StudentID = b.StudentID, StudentProfile c, StudentSchool d, SchoolProfile e"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=c.StudentID AND a.StudentID=d.StudentID AND d.SchoolID=e.SchoolID"

        ''ExamYear
        strWhere += " AND a.ExamYear='" & Request.QueryString("examyear") & "'"
        strWhere += " AND a.PusatCode='" & Request.QueryString("pusatcode") & "'"

        getSQLExport = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQLExport)

        Return getSQLExport

    End Function

    Private Function getPusatName() As String
        strSQL = "SELECT PusatName FROM PusatUjian WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.pusatujian.student.assign.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&studentid=" & strKeyID)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.pusatujian.student.assign.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&studentid=" & strKeyID)
                Case "UKM"
                    Response.Redirect("ukm.pusatujian.student.assign.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&studentid=" & strKeyID)
                Case "KPM"
                    Response.Redirect("kpm.studentprofile.view.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&studentid=" & strKeyID)
                Case "JPN"
                    Response.Redirect("jpn.studentprofile.view.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&studentid=" & strKeyID)

                Case Else
                    lblMsg.Text = "You do not have access right!" & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try



    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnUnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnAssign.Click
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

                    strSQL = "UPDATE UKM2 SET Pusatcode=NULL,TarikhUjian=NULL,SessiUKM2=NULL WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    Else
                        lblMsg.Text += "OK"
                    End If

                End If
            End If
        Next

        lblMsg.Text += "Successfully un-assign Pusat Ujian for UKM2."
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(getSQLExport, strConn)
            myDataAdapter.Fill(myDataSet, "mytable")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

            strRet = ExportData(myDataSet, "PusatUjian_Pelajar")

            objConn.Close()
        Catch ex As Exception
        End Try

    End Sub

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

    Private Sub btnCal_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCal.Click
        myCal.Visible = True

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
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

                    strSQL = "UPDATE UKM2 SET Pusatcode='" & Request.QueryString("pusatcode") & "',TarikhUjian='" & txtTarikhUjian.Text & "',SessiUKM2='" & selSessiUKM2.Value & "' WHERE StudentID='" & strID & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "NOK:" & strID & strRet & vbCrLf
                    Else
                        lblMsg.Text += "OK"
                    End If

                End If
            End If
        Next

        lblMsg.Text += "Successfully update students Date and Session."
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub myCal_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myCal.SelectionChanged
        txtTarikhUjian.Text = myCal.SelectedDate.ToString("yyyy-MM-dd")
        myCal.Visible = False

    End Sub

End Class