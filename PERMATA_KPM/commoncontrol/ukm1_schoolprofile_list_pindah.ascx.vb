Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_schoolprofile_list_pindah
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnPindah.Attributes.Add("onclick", "return confirm('Anda pasti untuk PINDAH semua pelajar tersebut?');")

        Try
            If Not IsPostBack Then
                SchoolState_list()
                schoolprofile_PPD_list()
                schoolprofile_city_list()

                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

                DisplaySourceSchool()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub DisplaySourceSchool()
        Dim strSource As String = Request.QueryString("source")
        '--Response.Write("strSource:" & strSource)

        Dim arSchoolID As Array = strSource.Split("|")

        For i = 0 To UBound(arSchoolID)
            If Not arSchoolID(i).ToString.Length = 0 Then
                lblSchoolName.Text += getSchoolName(arSchoolID(i).ToString) & " | "
            End If
        Next

    End Sub

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case "UKM"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & oCommon.getAppsettings("DefaultExamYear") & "%'  ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
        End Select

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

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub SchoolState_list()
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) WHERE SchoolState<>'UKM2-KPT' AND SchoolState <>'UKM2-ASASI'  ORDER BY SchoolStateID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolState.DataSource = ds
            ddlSchoolState.DataTextField = "schoolstate"
            ddlSchoolState.DataValueField = "schoolstate"
            ddlSchoolState.DataBind()

            ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = strRet
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub schoolprofile_PPD_list()
        strSQL = "SELECT DISTINCT SchoolPPD FROM SchoolProfile WHERE SchoolState='" & ddlSchoolState.Text & "' AND SchoolPPD<>'' AND IsDeleted<>'Y' ORDER BY SchoolPPD"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolPPD.DataSource = ds
            ddlSchoolPPD.DataTextField = "SchoolPPD"
            ddlSchoolPPD.DataValueField = "SchoolPPD"
            ddlSchoolPPD.DataBind()

            ddlSchoolPPD.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolPPD.SelectedValue = "ALL"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub schoolprofile_city_list()
        strSQL = "SELECT DISTINCT SchoolCity FROM schoolprofile WHERE SchoolState='" & ddlSchoolState.Text & "' ORDER BY SchoolCity"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolCity.DataSource = ds
            ddlSchoolCity.DataTextField = "schoolcity"
            ddlSchoolCity.DataValueField = "schoolcity"
            ddlSchoolCity.DataBind()

            ddlSchoolCity.Items.Add(New ListItem("ALL", "ALL"))
            ddlSchoolCity.SelectedValue = "ALL"

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":schoolprofile_city_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
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
                lblMsg.Text = "Rekod tidak dijumpai"
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
        Dim strWhereIn As String = ""
        Dim strOrder As String = " ORDER BY Jumlah DESC"

        Try
            '--chkRuleAge
            If chkRuleAge.Checked = True Then
                strWhereIn = " AND UKM1.IsCount=1"
            End If

            tmpSQL = "SELECT SchoolProfile.SchoolID,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,SchoolProfile.SchoolLokasi,(SELECT COUNT(*) FROM UKM1 WHERE SchoolProfile.SchoolID=UKM1.SchoolID AND UKM1.Examyear='" & ddlExamYear.Text & "'" & strWhereIn & ") as Jumlah FROM SchoolProfile"
            strWhere += " WHERE SchoolProfile.IsDeleted='N'"

            ''usertype. for MRSM only
            If getUserProfile_UserType() = "MRSM" Then
                strWhere += " AND SchoolProfile.SchoolType='MRSM'"
            End If

            '--SchoolState
            If Not ddlSchoolState.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolState='" & ddlSchoolState.Text & "'"
            End If

            '--SchoolPPD
            If Not ddlSchoolPPD.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolPPD='" & ddlSchoolPPD.Text & "'"
            End If

            '--SchoolCity
            If Not ddlSchoolCity.Text = "ALL" Then
                strWhere += " AND SchoolProfile.SchoolCity='" & ddlSchoolCity.Text & "'"
            End If

            '--SchoolCode
            If Not txtSchoolCode.Text.Length = 0 Then
                strWhere += " AND SchoolProfile.SchoolCode='" & oCommon.FixSingleQuotes(txtSchoolCode.Text) & "'"
            End If

            '--SchoolName
            If Not txtSchoolname.Text.Length = 0 Then
                strWhere += " AND SchoolProfile.SchoolName LIKE '%" & oCommon.FixSingleQuotes(txtSchoolname.Text) & "%'"
            End If

            '--SchoolName XXX
            If chkXXX.Checked = True Then
                strWhere += " AND SchoolProfile.SchoolCode LIKE '%XXX%'"
            End If

            getSQL = tmpSQL & strWhere & strOrder

            ''--debug
            'Response.Write(getSQL)
            Return getSQL

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("kpm.ukm1.schoolprofile.student.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.ukm1.schoolprofile.student.list.aspx?examyear=" & ddlExamYear.Text & "&schoolid=" & strKeyID)
                Case "KPT"
                Case "ASASI"
                Case Else
                    lblMsg.Text = "Invalid User Type:" & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlSchoolState_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSchoolState.TextChanged
        schoolprofile_PPD_list()
        schoolprofile_city_list()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnPindah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPindah.Click
        lblMsg.Text = ""
        lblRet.Text = ""
        Dim strTarget As String = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    strTarget = strID
                End If
            End If
        Next

        '--LOOP thru SOURCE and update with TARGET schoolid
        Dim strSource As String = Request.QueryString("source")
        Dim arSchoolID As Array = strSource.Split("|")

        For i = 0 To UBound(arSchoolID)
            If Not arSchoolID(i).ToString.Length = 0 Then
                If PindahSekolah(strTarget, arSchoolID(i).ToString) = False Then
                    lblMsg.Text += "FAIL:" & arSchoolID(i).ToString & vbCrLf
                End If
            End If
        Next
        If lblMsg.Text.Length = 0 Then
            lblRet.Text = " BERJAYA dipindahkan ke > " & getSchoolName(strTarget)
        Else
            lblRet.Text = lblMsg.Text
        End If

        '--refresh screen
        strRet = BindData(datRespondent)
    End Sub

    Private Function PindahSekolah(ByVal strSchoolIDNew As String, ByVal strSchoolIDOld As String) As Boolean
        Try
            '--update studentschool
            strSQL = "UPDATE StudentSchool WITH (UPDLOCK) SET SchoolID='" & strSchoolIDNew & "' WHERE SchoolID='" & strSchoolIDOld & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            '--update UKM1 to new schoolid profile
            If ukm1_schoolprofile_update(strSchoolIDNew, strSchoolIDOld) = False Then
                Return False
            End If

            '--update UKM2 to new schoolid profile
            If ukm2_schoolprofile_update(strSchoolIDNew, strSchoolIDOld) = False Then
                Return False
            End If

            '--set DELETED for OLD School
            If chkDeleteSchool.Checked = True Then
                strSQL = "UPDATE SchoolProfile WITH (UPDLOCK) SET IsDeleted='Y' WHERE SchoolID='" & strSchoolIDOld & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            Return True

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Function


    Private Function ukm1_schoolprofile_update(ByVal strSchoolIDNew As String, ByVal strSchoolIDOld As String) As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strSchoolIDNew & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        ''update UKM1 to new schoolid profile
        strSQL = "UPDATE UKM1 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE schoolid='" & strSchoolIDOld & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "UKM1 system error:" & strRet
            Return False
        End If

        Return True

    End Function

    Private Function ukm2_schoolprofile_update(ByVal strSchoolIDNew As String, ByVal strSchoolIDOld As String) As Boolean
        ''--get schoolprofile
        strSQL = "SELECT SchoolID,SchoolState,SchoolCity,SchoolType,SchoolPPD,SchoolLokasi FROM SchoolProfile WHERE SchoolID='" & strSchoolIDNew & "'"
        strRet = oCommon.getFieldValueEx(strSQL)
        Dim arSchoolProfile As Array = strRet.Split("|")
        If Not UBound(arSchoolProfile) = 6 Then
            lblMsg.Text = "SchoolProfile error:" & strRet & ":" & UBound(arSchoolProfile).ToString
            Return False
        End If

        ''update UKM2 to new schoolid profile
        strSQL = "UPDATE UKM2 WITH (UPDLOCK) SET SchoolID='" & oCommon.FixSingleQuotes(arSchoolProfile(0).ToString) & "',SchoolState='" & oCommon.FixSingleQuotes(arSchoolProfile(1).ToString) & "',SchoolCity='" & oCommon.FixSingleQuotes(arSchoolProfile(2).ToString) & "',SchoolType='" & oCommon.FixSingleQuotes(arSchoolProfile(3).ToString) & "',SchoolPPD='" & oCommon.FixSingleQuotes(arSchoolProfile(4).ToString) & "',SchoolLokasi='" & oCommon.FixSingleQuotes(arSchoolProfile(5).ToString) & "' WHERE schoolid='" & strSchoolIDOld & "' AND ExamYear='" & Request.QueryString("examyear") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = "0" Then
            lblMsg.Text = "UKM2 system error:" & strRet
            Return False
        End If

        Return True

    End Function

    Private Function getSchoolName(ByVal strID As String) As String
        strSQL = "SELECT SchoolName FROM SchoolProfile WHERE SchoolID='" & strID & "'"
        Return oCommon.getFieldValue(strSQL)

    End Function

End Class