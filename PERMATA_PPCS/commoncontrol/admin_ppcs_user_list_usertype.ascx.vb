Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports RKLib.ExportData

Partial Public Class admin_ppcs_user_list_usertype
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
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
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Record not found!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Total Record#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY a.Fullname ASC"

        tmpSQL = "SELECT a.myGUID,a.Fullname,a.LoginID,a.Pwd,a.IsAllow,b.UserType,b.PPCSDate FROM PPCS_Users a,PPCS_Users_Year b"
        strWhere = " WITH (NOLOCK) WHERE a.myGUID=b.myGUID AND b.UserType<>'ADMIN' AND b.Usertype='" & Request.QueryString("usertype") & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("ppcs.users.update.aspx?myguid=" & strKeyID)

    End Sub

    Private Function ExportData(ByVal dsTable As DataSet) As String
        ''-Dim strFilename As String = Server.MapPath(".") & "log\" & "Export." & oCommon.getRandom & ".txt"
        Dim strFilename As String = "PPCS_Userlist_" & oCommon.getRandom & ".txt"

        Try
            ' Export all the details to CSV
            Dim objExport As New RKLib.ExportData.Export("Web")
            Dim dtRespondent As DataTable = dsTable.Tables("myTable").Copy()
            ''--objExport.ExportDetails(dtRespondent, Export.ExportFormat.Excel, strFilename)
            objExport.ExportDetails(dtRespondent, Export.ExportFormat.CSV, strFilename)

            Return strFilename
        Catch Ex As Exception
            Return Ex.Message
        End Try

    End Function

End Class