Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Partial Public Class ppcs_activity_update
    Inherits System.Web.UI.Page

    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim strSaparator As String = ","

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim tableColumn As DataColumnCollection
            Dim tableRows As DataRowCollection

            strSQL = "SELECT distinct createdby FROM ppcs_activity"

            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)
            myDataAdapter.Fill(myDataSet, "ppcs_activity")
            myDataAdapter.SelectCommand.CommandTimeout = 80000
            objConn.Close()

            '--transfer to an object
            tableColumn = myDataSet.Tables(0).Columns
            tableRows = myDataSet.Tables(0).Rows

            lblMsg.Text = ppcs_activity_update(tableColumn, tableRows)

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error." & ex.Message

            '--write to file
            Dim strMsg As String = Now.ToString & ":" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        End Try

    End Sub

    Private Function ppcs_activity_update(ByVal tableColumns As DataColumnCollection, ByVal tableRows As DataRowCollection) As String

        Dim strReturn As String = ""
        Dim rowscreated As Integer = 0
        Dim strTemp As String = ""

        Dim strFullname As String = ""
        lblStatus.Text = ""
        Try
            Dim row As DataRow
            For Each row In tableRows
                Dim rowItems() As Object = row.ItemArray

                strTemp = rowItems(0).ToString()
                strSQL = "SELECT Fullname FROM ppcs_users WHERE myGUID='" & strTemp & "'"
                strFullname = oCommon.getFieldValue(strSQL)
                lblStatus.Text += strFullname & "<br />"
                If strFullname.Length > 0 Then
                    strSQL = "UPDATE ppcs_activity SET createdby='" & strFullname & "' WHERE createdby='" & strTemp & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblStatus.Text += strRet & "<br />"
                    End If
                End If

                'lblStatus.Text += strTemp & "<br />"
                rowscreated = rowscreated + 1
            Next
            strReturn = "Successfully update ppcs_activity"

        Catch ae As SqlException
            '--display on screen
            lblMsg.Text = "System Error." & ae.Message

            '--write to file
            Dim strMsg As String = Now.ToString & ":" & Request.UserHostAddress & ":" & ae.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally

        End Try

        Return strReturn
    End Function



End Class