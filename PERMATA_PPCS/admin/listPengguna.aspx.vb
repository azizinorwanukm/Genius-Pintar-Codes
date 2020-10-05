Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class listPengguna
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblUserType.Text = Request.QueryString("usertype")

        ''--debug
        ''--Response.Write("strClassID:" & strClassID)
        Try
            ''--get ClassNameBM
            strSQL = "SELECT ClassNameBM FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
            lblClassNameBM.Text = oCommon.getFieldValue(strSQL)

            ''--get PPCSDate
            strSQL = "SELECT PPCSDate FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
            lblPPCSDate.Text = oCommon.getFieldValue(strSQL)

            If Not IsPostBack Then
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Record not found!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
            Return False
        End Try

        Return True
    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Fullname ASC"

        tmpSQL = "SELECT * FROM PPCS_Users a,PPCS_Users_Year b"
        strWhere = " WITH (NOLOCK) WHERE a.myGUID=b.myGUID AND b.Usertype='" & lblUserType.Text & "' AND PPCSDate='" & lblPPCSDate.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL
    End Function


    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        '--Response.Write(strKeyID)
        Response.Redirect("user.update.classcode.aspx?usertype=" & lblUserType.Text.Trim & "&myguid=" & strKeyID & "&classid=" & Request.QueryString("classid"))

    End Sub

End Class