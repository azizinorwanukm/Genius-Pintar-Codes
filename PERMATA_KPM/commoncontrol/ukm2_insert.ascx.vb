Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ukm2_insert
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strSchoolID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                '--load examyear
                examyear_list()
                ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

            End If
        Catch ex As Exception

        End Try

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

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub lnkStudentProfileView_Click(sender As Object, e As EventArgs) Handles lnkStudentProfileView.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "UKM"
                Response.Redirect("ukm.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "JPN"
                Response.Redirect("jpn.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "KPM"
                Response.Redirect("kpm.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "KPT"
                Response.Redirect("kpt.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "MRSM"
                Response.Redirect("mara.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "ASASI"
                Response.Redirect("asasi.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case Else
                lblMsg.Text = "Invalid usertype!"
        End Select

    End Sub

    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        ''--validate
        If ValidatePage() = False Then
            Exit Sub
        End If

        strSQL = "INSERT INTO UKM2 (StudentID,ExamYear,ExamStart,ExamEnd,LastPage,Status,VCI,PRI,WMI,PSI,TotalScore,TotalPercentage)"
        strSQL += " VALUES('" & Request.QueryString("studentid") & "','" & oCommon.FixSingleQuotes(ddlExamYear.Text) & "','" & oCommon.FixSingleQuotes(txtExamStart.Text) & "','" & oCommon.FixSingleQuotes(txtExamEnd.Text) & "','" & oCommon.FixSingleQuotes(txtLastPage.Text) & "','" & oCommon.FixSingleQuotes(txtStatus.Text) & "','" & oCommon.FixSingleQuotes(txtVCI.Text) & "','" & oCommon.FixSingleQuotes(txtPRI.Text) & "','" & oCommon.FixSingleQuotes(txtWMI.Text) & "','" & oCommon.FixSingleQuotes(txtPSI.Text) & "','" & oCommon.FixSingleQuotes(txtTotalScore.Text) & "','" & oCommon.FixSingleQuotes(txtTotalPercentage.Text) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengembalikan data UKM2 bagi tahun: " & ddlExamYear.Text
            divMsg.Attributes("class") = "info"
        Else
            lblMsg.Text = strRet
            divMsg.Attributes("class") = "error"
        End If

    End Sub

    Private Function ValidatePage() As Boolean

        Return True
    End Function

End Class