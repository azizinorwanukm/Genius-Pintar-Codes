Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ukm1_ppd_student_list
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            lblMsg.Text = ""
            strRet = BindData(datRespondent)
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)

    End Sub

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

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "KPT"
            Case "ASASI"
            Case Else
        End Select

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY UKM1.ExamEnd DESC"

        Try
            tmpSQL = "SELECT StudentProfile.Studentfullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,UKM1.StudentID,UKM1.ExamStart,UKM1.ExamEnd,UKM1.Status,UKM1.Lastpage,UKM1.QuestionYear,SchoolProfile.SchoolCode,SchoolProfile.Schoolname,UKM1.SchoolState,UKM1.SchoolPPD FROM UKM1"
            tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM1.StudentID=StudentProfile.StudentID"
            tmpSQL += " LEFT OUTER JOIN SchoolProfile ON UKM1.SchoolID=SchoolProfile.SchoolID"
            strWhere += " WHERE UKM1.ExamYear='" & Request.QueryString("examyear") & "'"   '--mandatory filter

            ''usertype. for MRSM only
            If getUserProfile_UserType() = "MRSM" Then
                strWhere += " AND UKM1.SchoolType='MRSM'"
            End If

            '--DOB_Year
            If Not Request.QueryString("dob_year") = "ALL" Then
                strWhere += " AND UKM1.DOB_Year='" & Request.QueryString("dob_year") & "'"
            End If

            '--SchoolPPD
            If Not Request.QueryString("schoolppd") = "ALL" Then
                strWhere += " AND UKM1.SchoolPPD='" & Request.QueryString("schoolppd") & "'"
            End If

            '--Status
            If Not selStatus.Value = "ALL" Then
                strWhere += " AND UKM1.Status='" & selStatus.Value & "'"
            End If

            '--Studentfullname
            If Not txtStudentFullname.Text.Length = 0 Then
                strWhere += " AND StudentProfile.Studentfullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text.Trim) & "%'"
            End If

            '--MYKAD
            If Not txtMYKAD.Text.Length = 0 Then
                strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text.Trim) & "'"
            End If

            '--chkRuleAge
            If Request.QueryString("iscount") = "True" Then
                strWhere += " AND UKM1.IsCount=1"
            End If

            getSQL = tmpSQL & strWhere & strOrder

            ''--debug
            'Response.Write(getSQL)
            Return getSQL

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class