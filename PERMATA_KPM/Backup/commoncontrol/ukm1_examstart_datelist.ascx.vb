Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_examstart_datelist
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblDatetime.Text = Now.ToLongDateString & "  " & Now.ToShortTimeString

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
                lblMsg.Text = "Senarai tarikh tiada"
            Else
                divMsg.Attributes("class") = "info"
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


            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString

            ''usertype. for MRSM only
            Dim strWhere As String = ""
            If getUserProfile_UserType() = "MRSM" Then
                strWhere = " AND SchoolType='MRSM'"
                lblUserType.Text = " [UserType:MRSM]"
            End If

            ''NEW
            lblStudentSUM_NEW = e.Row.FindControl("StudentSUM_NEW")
            strSQL = "SELECT COUNT(*) FROM UKM1 WHERE ExamStart LIKE '%" & strKeyID & "%' AND ExamYear='" & Now.Year & "' " & strWhere
            lblStudentSUM_NEW.Text = oCommon.getFieldValue(strSQL)

            ''DONE
            lblStudentSUM_DONE = e.Row.FindControl("StudentSUM_DONE")
            strSQL = "SELECT COUNT(*) FROM UKM1 WHERE ExamEnd LIKE '%" & strKeyID & "%' AND Status='DONE' AND ExamYear='" & Now.Year & "' " & strWhere
            lblStudentSUM_DONE.Text = oCommon.getFieldValue(strSQL)

        End If
    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Private Function getSQL() As String
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " Order By ExamEndID"

        tmpSQL = "SELECT ExamEnd FROM UKM1_ExamEnd"
        strWhere = " WITH (NOLOCK) WHERE ExamEnd <= " & strToday
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

End Class