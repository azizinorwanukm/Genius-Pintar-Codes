Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_examend_summary
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
                lblMsg.Text = "Senarai tarikh tiada"
            Else
                lblMsg.Text = "Jumlah pelajar bagi setiap tarikh yang menduduki Ujian UKM1."
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

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblStudentSUM_NEW As Label
            Dim lblStudentSUM_DONE As Label
            Dim lblStudentSUM_ALL As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

            lblStudentSUM_NEW = e.Row.FindControl("StudentSUM_NEW")
            lblStudentSUM_NEW.Text = Schoolprofile_Count(strKeyID, "NEW")

            lblStudentSUM_DONE = e.Row.FindControl("StudentSUM_DONE")
            lblStudentSUM_DONE.Text = Schoolprofile_Count(strKeyID, "DONE")

            lblStudentSUM_ALL = e.Row.FindControl("StudentSUM_ALL")
            lblStudentSUM_ALL.Text = Schoolprofile_Count(strKeyID, "ALL")

        End If
    End Sub

    Private Function Schoolprofile_Count(ByVal strExamEnd As String, ByVal strStatus As String)
        Dim strWhere As String = ""
        Dim strOrderby As String = ""
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) ORDER BY SchoolStateID"
        strWhere = " WITH (NOLOCK) WHERE ExamEnd LIKE '%" & strExamEnd & "%'"

        If Not strStatus = "ALL" Then
            strWhere += " AND Status='" & strStatus & "'"
        End If

        strSQL = strSQL & strWhere & strOrderby
        strRet = oCommon.getFieldValue(strSQL)
        ''--debug
        ''Response.Write(strSQL)

        Return strRet
    End Function

    Private Function getSQL() As String
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY ExamEndID"

        tmpSQL = "SELECT ExamEnd FROM UKM1_ExamEnd"
        strWhere = " WITH (NOLOCK) WHERE ExamEnd <= " & strToday
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

End Class