Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class TransactionLog_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                txtDateCreated.Text = oCommon.getToday
                lblDateCreated.Text = oCommon.getToday
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        '--truncate column and tooptip
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY TransactionID DESC"

        tmpSQL = "SELECT * FROM TransactionLog"
        strWhere = " WHERE TransactionID IS NOT NULL"

        '--LoginID
        If Not txtLoginID.Text.Length = 0 Then
            strWhere += " AND LoginID LIKE '%" & oCommon.FixSingleQuotes(txtLoginID.Text) & "%'"
        End If

        '--SQLAction
        If Not txtSQLAction.Text.Length = 0 Then
            strWhere += " AND SQLAction LIKE '%" & oCommon.FixSingleQuotes(txtSQLAction.Text) & "%'"
        End If

        '--DateCreated
        If Not txtDateCreated.Text.Length = 0 Then
            strWhere += " AND DateCreated LIKE '%" & oCommon.FixSingleQuotes(txtDateCreated.Text) & "%'"        'MDY
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.TransactionLog.view.aspx?transactionID=" & strKeyID)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        If txtDateCreated.Text.Length = 0 Then
            lblMsg.Text = "Date Created cannot be BLANK due to big transaction records."
            Exit Sub
        End If

        strRet = BindData(datRespondent)

    End Sub

End Class