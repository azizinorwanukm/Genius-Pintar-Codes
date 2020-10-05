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
        End If

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod."
            Else
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strSQL = getSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY nschool DESC"
        Dim strGroupby As String = " GROUP BY StudentSchool.SchoolID,SchoolProfile.SchoolState,SchoolProfile.SchoolCity,SchoolProfile.SchoolPPD,SchoolProfile.SchoolName,SchoolProfile.SchoolCode"

        tmpSQL = "SELECT StudentSchool.SchoolID,count(StudentSchool.SchoolID) as nschool,SchoolProfile.schoolname,SchoolProfile.SchoolCode,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON PPCS.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE PPCS.PPCSDate='" & Request.QueryString("ppcsdate") & "'"

        '--PPCSStatus
        If Not Request.QueryString("ppcsstatus") = "ALL" Then
            strWhere += " AND PPCS.PPCSStatus='" & Request.QueryString("ppcsstatus") & "'"
        End If

        strWhere += " AND SchoolProfile.SchoolType='" & oCommon.FixSingleQuotes(Request.QueryString("schooltype")) & "'"

        getSQL = tmpSQL & strWhere & strGroupby & strOrder

        Return getSQL

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ppcs.schoolprofile.student.list.aspx?schoolid=" & strKeyID & "&ppcsstatus=" & Request.QueryString("ppcsstatus") & "&ppcsdate=" & Request.QueryString("ppcsdate"))
            Case "ADMINOP"
                Response.Redirect("ppcs.schoolprofile.student.list.aspx?schoolid=" & strKeyID & "&ppcsstatus=" & Request.QueryString("ppcsstatus") & "&ppcsdate=" & Request.QueryString("ppcsdate"))
            Case "SUBADMIN"
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub

End Class