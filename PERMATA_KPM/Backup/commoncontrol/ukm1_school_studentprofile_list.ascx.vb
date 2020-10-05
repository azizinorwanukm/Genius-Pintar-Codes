Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_school_studentprofile_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            strRet = BindData(datRespondent)
        End If

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            myDataAdapter.SelectCommand.CommandTimeout = 80000

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada pelajar yang menduduki Ujian UKM1 dari sekolah ini."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Senarai pelajar yang menduduki Ujian UKM1. Jumlah pelajar#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " Order By b.Studentfullname"

        tmpSQL = "SELECT b.StudentID,b.StudentFullname,b.MYKAD,b.DOB_Year,b.StudentRace,a.Status FROM UKM1 a,StudentProfile b"
        strWhere = " WITH (NOLOCK) WHERE a.SchoolID='" & Request.QueryString("schoolid") & "' AND a.StudentID=b.StudentID AND a.ExamYear='" & Request.QueryString("examyear") & "'"

        If Not selStatus.Value = "ALL" Then
            strWhere += " AND a.Status='" & selStatus.Value & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL


    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strSQL = getSQL()
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            Response.Redirect("studentprofile.view.aspx?examyear=" & Request.QueryString("examyear") & "&schoolid=" & Request.QueryString("schoolid") & "&studentid=" & strKeyID)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        strRet = BindData(datRespondent)

    End Sub
End Class