Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Configuration
Imports System.Data
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class student_imagePayment
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                student_year()
                Invoice_Detail.Visible = False

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub student_year()

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        strSQL = "select distinct II_Year from invoice_info where std_ID = '" & data_ID & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "II_Year"
            ddlyear.DataValueField = "II_Year"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", ""))
            ddlyear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            strRet = CallingDataPayment(GridView1)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView, ByVal keyName As String) As Boolean

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL(keyName), strConn)
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

    Private Function CallingDataPayment(ByVal PaymentTable As GridView) As Boolean
        Dim stdDataSet As New DataSet
        Dim stdDataAdapter As New SqlDataAdapter(getDataSQLPayment, strConn)
        stdDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            stdDataAdapter.Fill(stdDataSet, "myaccount")

            PaymentTable.DataSource = stdDataSet
            PaymentTable.DataBind()

            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL(ByVal key As String) As String
        'A left outer join will give all rows in A, plus any common rows in B.
        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by invoice_detail.ID_Item ASC"

        tmpSQL = "select invoice_transaction.IT_ID, invoice_detail.ID_Item, invoice_transaction.IT_Quantity, invoice_transaction.IT_Price, invoice_transaction.IT_Status, invoice_transaction.IT_Date from invoice_transaction
                  left join invoice_info on invoice_transaction.II_ID = invoice_info.II_ID
                  left join invoice_detail on invoice_transaction.ID_ID = invoice_detail.ID_ID"

        strWhere = " where invoice_info.II_Year = '" & ddlyear.SelectedValue & "' and invoice_detail.ID_Year = '" & ddlyear.SelectedValue & "' and invoice_transaction.IT_Year = '" & ddlyear.SelectedValue & "' "
        strWhere += " and invoice_info.Std_ID = '" & data_ID & "'"
        strWhere += " and invoice_info.II_ID = '" & key & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Function getDataSQLPayment() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by II_ID ASC"

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        tmpSQL = "select * from invoice_info"
        strWhere = " WHERE II_ID IS NOT NULL"
        strWhere += " and II_Year = '" & ddlyear.SelectedValue & "' and std_ID = '" & data_ID & "' and II_Published = 'Yes'"

        getDataSQLPayment = tmpSQL & strWhere & strOrderby

        Return getDataSQLPayment
    End Function

    Private Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim strKeyName As String = GridView1.DataKeys(e.RowIndex).Value.ToString

        Try
            Invoice_Detail.Visible = True

            Reload_datRespondant(strKeyName)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Reload_datRespondant(ByVal key As String)

        Dim Find_TotalInvPrice As String = "select sum(IT_Price) from invoice_transaction where IT_Year = '" & ddlyear.SelectedValue & "' and II_ID = '" & key & "' and IT_Status = 'Pending'"
        Dim Get_TotalInvPrice As String = oCommon.getFieldValue(Find_TotalInvPrice)

        ttl_InvPrice.Text = Get_TotalInvPrice

        strRet = BindData(datRespondent, key)

    End Sub

End Class
