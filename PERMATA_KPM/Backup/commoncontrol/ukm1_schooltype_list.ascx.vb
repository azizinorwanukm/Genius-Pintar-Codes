Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_schooltype_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
        lblTotal.Text = ""

        If Not IsPostBack Then
            lblDatetime.Text = Now.ToLongDateString & "  " & Now.ToShortTimeString
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
        Dim strOrder As String = " ORDER BY nschool DESC"
        Dim strGroupby As String = " GROUP BY a.SchoolID,a.SchoolState,a.SchoolCity,a.SchoolPPD,b.SchoolName,b.SchoolCode"

        tmpSQL = "SELECT a.SchoolID,count(a.SchoolID) as nschool,b.schoolname,b.SchoolCode,a.SchoolState,a.SchoolPPD,a.SchoolCity FROM UKM1 a,SchoolProfile b"
        strWhere = " WITH (NOLOCK) WHERE a.schoolid=b.SchoolID AND a.ExamYear='" & Request.QueryString("examyear") & "'"
        strWhere += " AND a.SchoolType='" & oCommon.FixSingleQuotes(Request.QueryString("schooltype")) & "'"
        getSQL = tmpSQL & strWhere & strGroupby & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "JPN"
                Response.Redirect("jpn.ukm1.schoolprofile.student.list.aspx?examyear=" & Request.QueryString("examyear") & "&schoolid=" & strKeyID)
            Case "KPM"
                Response.Redirect("kpm.ukm1.schoolprofile.student.list.aspx?examyear=" & Request.QueryString("examyear") & "&schoolid=" & strKeyID)
            Case "KPT"
            Case "MRSM"
            Case "ASASI"
            Case Else
                lblMsg.Text = "Anda tidak layak menggunakan fungsi ini." & getUserProfile_UserType()
        End Select

    End Sub

End Class