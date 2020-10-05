Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData

Partial Public Class ppcs_stat_list
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strTestID As String

    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                examyear_list()
                ddlExamYear.Text = Now.Year

                schooltype_list()
                ddlSchoolType.Text = "ALL"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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

            ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub examyear_list()
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

    Private Sub schooltype_list()
        strSQL = "SELECT schooltype FROM schooltype ORDER BY schooltypeid"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolType.DataSource = ds
            ddlSchoolType.DataTextField = "schooltype"
            ddlSchoolType.DataValueField = "schooltype"
            ddlSchoolType.DataBind()

            ddlSchoolType.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada pelajar layak."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Pelajar#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY SchoolName,StudentFullname"

        tmpSQL = "SELECT a.PPCSID, a.StudentID, a.ExamYear, a.PPCSDate, a.PPCSCourse, a.PPCSClass, a.PPCSStatus, a.NamaAsrama, a.NoBilik, a.SaizBaju, b.StudentSchoolID, b.StudentID AS Expr1,"
        tmpSQL += "b.SchoolID, b.StartDate, b.EndDate, b.CreatedDate, c.SchoolProfileID, c.SchoolID AS Expr2, c.SchoolCode, c.SchoolName, c.SchoolAddress, c.SchoolPostcode, c.SchoolCity, c.SchoolState,"
        tmpSQL += "c.SchoolType, c.SchoolNoTel, c.SchoolNoFax, c.SchoolLokasi, c.SchoolEmail, c.SchoolPPD, c.CreateBy, c.CreateDate, c.IsDeleted, d.StudentProfileID, d.StudentID AS Expr3, d.MYKAD, d.Pwd, d.AlumniID,"
        tmpSQL += "d.StudentFullname, d.DOB_Day, d.DOB_Month, d.DOB_Year, d.StudentGender, d.StudentRace, d.StudentReligion, d.StudentEmail, d.StudentForm, d.StudentAddress1, d.StudentAddress2,d.StudentPostcode, d.StudentCity, d.StudentState, d.StudentCountry, d.StudentContactNo"
        tmpSQL += " FROM PPCS AS a INNER JOIN StudentSchool AS b ON a.StudentID = b.StudentID INNER JOIN SchoolProfile AS c ON b.SchoolID = c.SchoolID INNER JOIN StudentProfile AS d ON a.StudentID = d.StudentID"
        strWhere = " WITH (NOLOCK) WHERE (a.PPCSStatus='LAYAK') AND (a.ExamYear = '" & ddlExamYear.Text & "')"

        If Not ddlPPCSDate.Text = "ALL" Then
            strWhere += " AND (a.PPCSDate ='" & ddlPPCSDate.Text & "')"
        End If

        If Not ddlSchoolType.Text = "ALL" Then
            strWhere += " AND (c.SchoolType ='" & ddlSchoolType.Text & "')"
        End If

        If Not selSchoolLokasi.Value = "ALL" Then
            strWhere += " AND (c.SchoolLokasi ='" & selSchoolLokasi.Value & "')"
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

   
End Class