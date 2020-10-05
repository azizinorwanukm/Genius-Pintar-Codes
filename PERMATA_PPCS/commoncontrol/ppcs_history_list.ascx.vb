Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class ppcs_history_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT * FROM PPCS"
        strWhere = " WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strOrder = " ORDER BY PPCSDate"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblCourseClass As Label

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString  'PPCSID

            lblCourseClass = e.Row.FindControl("lblCourseClass")
            lblCourseClass.Text = getCourseClass(strKeyID)
        End If

    End Sub

    Private Function getCourseClass(ByVal strKeyID As String) As String
        Dim strStudentID As String = ""
        Dim strPPCSDate As String = ""

        Dim strValue As String = ""
        Dim strCourseCode As String = ""
        Dim strClassCode As String = ""

        strSQL = "SELECT StudentID FROM PPCS WHERE PPCSID=" & strKeyID
        strStudentID = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT PPCSDate FROM PPCS WHERE PPCSID=" & strKeyID
        strPPCSDate = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT CourseCode FROM PPCS a,PPCS_Course b WHERE a.CourseID=b.CourseID AND a.PPCSDate='" & strPPCSDate & "' AND a.StudentID='" & strStudentID & "'"
        strCourseCode = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT ClassCode FROM PPCS a,PPCS_Class b WHERE a.ClassID=b.ClassID AND a.PPCSDate='" & strPPCSDate & "' AND a.StudentID='" & strStudentID & "'"
        strClassCode = oCommon.getFieldValue(strSQL)

        strValue = strCourseCode & "|" & strClassCode

        Return strValue
    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("ppcs.update.aspx?PPCSID=" & strKeyID & "&studentid=" & Request.QueryString("studentid"))

    End Sub
End Class