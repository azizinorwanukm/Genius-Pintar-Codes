Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ppcs_schooltype_list1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblDatetime.Text = Now.ToLongDateString & "  " & Now.ToShortTimeString
            lblSchoolType.Text = Request.QueryString("schooltype")

            strRet = BindData(datRespondent)
            ''--debug
            ''lblMsg.Text = Server.UrlDecode(Request.QueryString("schooltype"))
        End If

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Senarai jenis sekolah tiada"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah pelajar bagi setiap negeri berdasarkan jenis sekolah yang LAYAK ke PPCS."
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

        strSQL = getSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY nschool DESC"
        Dim strGroupby As String = " GROUP BY b.SchoolID,c.SchoolState,c.SchoolCity,c.SchoolPPD,c.SchoolName,c.SchoolCode"

        tmpSQL = "SELECT b.SchoolID,count(b.SchoolID) as nschool,c.schoolname,c.SchoolCode,c.SchoolState,c.SchoolPPD,c.SchoolCity FROM PPCS a, StudentSchool b, SchoolProfile c"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND b.SchoolID=c.SchoolID AND a.PPCSStatus='LAYAK' AND a.PPCSDate='" & Request.QueryString("ppcsdate") & "'"

        strWhere += " AND c.SchoolType='" & oCommon.FixSingleQuotes(Request.QueryString("schooltype")) & "'"
        getSQL = tmpSQL & strWhere & strGroupby & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

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
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ppcs.schoolprofile.student.list.aspx?schoolid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate"))
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

End Class