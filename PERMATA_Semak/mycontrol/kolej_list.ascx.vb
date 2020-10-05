Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class kolej_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = oCommon.getAppsettings("KOLEJGagal")
            Else
                '--lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
                lblMsg.Text = oCommon.getAppsettings("KOLEJBerjaya")
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        '--get StudentID
        Dim strQuery As String = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(Request.QueryString("mykad")) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strQuery)

        tmpSQL = "SELECT * FROM UKM3"
        strWhere = " WHERE StudentID='" & strStudentID & "' AND PPMT='Y' AND DisplayStatus='Y'"
        strOrder = " ORDER BY UKM3ID DESC"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Response.Redirect("kolej.result.aspx?UKM3ID=" & strKeyID)
    End Sub

End Class