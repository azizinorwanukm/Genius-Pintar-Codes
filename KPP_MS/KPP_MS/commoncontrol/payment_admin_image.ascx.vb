Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class payment_admin_image
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

                Invoice_Detail.Visible = False

                loadpage()
                year_list()

                strRet = CallingDataPayment(GridView1)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub loadpage()
        ''student_info
        strSQL = "SELECT * from student_info WHERE std_ID ='" & Request.QueryString("std_ID") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                student_Name.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                student_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                student_ID.Text = ds.Tables(0).Rows(0).Item("student_ID")
            Else
                student_ID.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Mykad")) Then
                student_Mykad.Text = ds.Tables(0).Rows(0).Item("student_Mykad")
            Else
                student_Mykad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sex")) Then
                student_Sex.Text = ds.Tables(0).Rows(0).Item("student_Sex")
            Else
                student_Sex.Text = ""
            End If
        End If
    End Sub

    Private Sub loadpage_invoice_detail(ByVal key As String)
        ''student_info
        strSQL = "SELECT II_PaymentType from invoice_info WHERE II_ID ='" & key & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("II_PaymentType")) Then
                ddlInvoice_Type.SelectedValue = ds.Tables(0).Rows(0).Item("II_PaymentType")
            End If
        End If
    End Sub

    Private Sub year_list()
        strSQL = "SELECT II_Year FROM invoice_info WHERE std_ID = '" & Request.QueryString("std_ID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "II_Year"
            ddlYear.DataValueField = "II_Year"
            ddlYear.DataBind()

            ddlYear.SelectedValue = Now.Year
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Invoice_Type_List()
        strSQL = "SELECT Parameter, Value FROM setting WHERE idx = 'Payment' and Type = 'Invoice Status'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlInvoice_Type.DataSource = ds
            ddlInvoice_Type.DataTextField = "Parameter"
            ddlInvoice_Type.DataValueField = "Value"
            ddlInvoice_Type.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnkemaskini_ServerClick(sender As Object, e As EventArgs) Handles Btnkemaskini.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer

        Dim get_transaction_id As String = ""
        Dim find_transaction_id As String = ""

        Dim find_totalamount As String = ""
        Dim get_totalamount As Decimal = 0.00

        Dim find_outstanding As String = ""
        Dim get_outstanding As Decimal = 0.00

        Dim Total_InvPrice As Decimal = 0.0


        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim strKey As String = datRespondent.DataKeys(i).Value.ToString

            get_transaction_id = "select II_ID from invoice_item where IT_ID = '" & strKey & "'"
            find_transaction_id = oCommon.getFieldValue(get_transaction_id)

            If ddlInvoice_Type.SelectedValue = "Scholarship Payment" Then

                Dim quantity As TextBox = DirectCast(datRespondent.Rows(i).FindControl("IT_Quantity"), TextBox)
                Dim get_quantity As Decimal = Decimal.Parse(quantity.Text)

                ''get invoice item price from db
                Dim find_price As String = "select FIM_Price from fee_item_master 
                                            left join invoice_item on fee_item_master.FIM_ID = invoice_item.FIM_ID 
                                            where invoice_item.IT_ID = '" & strKey & "' and invoice_item.IT_Year = '" & ddlYear.SelectedValue & "' and invoice_item.IT_Item = fee_item_master.FIM_Item"
                Dim get_price As Decimal = Decimal.Parse(oCommon.getFieldValue(find_price))

                get_price = get_price * get_quantity

                ''update invoice info DB
                strSQL = "UPDATE invoice_info set II_PaymentType ='" & ddlInvoice_Type.SelectedValue & "', II_Status = 'Paid', II_Outstanding = '0.00' WHERE II_ID ='" & find_transaction_id & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ''update invoice item DB
                strSQL = "UPDATE invoice_item set IT_Status = 'Paid', IT_Quantity = '" & get_quantity & "', IT_Price = '" & get_price & "' WHERE II_ID ='" & find_transaction_id & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ttl_InvPrice.Text = 0.00

                If strRet = "0" Then
                    errorCount = 0
                Else
                    errorCount = 1
                End If

            ElseIf ddlInvoice_Type.SelectedValue = "Installment Payment" Then

                Dim quantity As TextBox = DirectCast(datRespondent.Rows(i).FindControl("IT_Quantity"), TextBox)
                Dim get_quantity As Decimal = Decimal.Parse(quantity.Text)

                ''get invoice item price from db
                Dim find_price As String = "select FIM_Price from fee_item_master 
                                            left join invoice_item on fee_item_master.FIM_ID = invoice_item.FIM_ID 
                                            where invoice_item.IT_ID = '" & strKey & "' and invoice_item.IT_Year = '" & ddlYear.SelectedValue & "' and invoice_item.IT_Item = fee_item_master.FIM_Item"
                Dim get_price As Decimal = Decimal.Parse(oCommon.getFieldValue(find_price))

                get_price = get_price * get_quantity

                strSQL = "UPDATE invoice_item set IT_Quantity ='" & quantity.Text & "', IT_Price = '" & get_price & "'
                          WHERE IT_ID ='" & strKey & "' and IT_Year = '" & ddlYear.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ''calculate new outstanding and total amount
                find_totalamount = "select SUM(IT_Price) from invoice_item where II_ID = '" & find_transaction_id & "' and IT_Year = '" & ddlYear.SelectedValue & "'"
                get_totalamount = Decimal.Parse(oCommon.getFieldValue(find_totalamount))
                find_outstanding = "select SUM(IT_Price) from invoice_item where II_ID = '" & find_transaction_id & "' and IT_Year = '" & ddlYear.SelectedValue & "' and IT_Status = 'Pending'"
                get_outstanding = Decimal.Parse(oCommon.getFieldValue(find_outstanding))

                ttl_InvPrice.Text = get_outstanding

                ''update invoice info DB
                strSQL = "UPDATE invoice_info set II_PaymentType = '" & ddlInvoice_Type.SelectedValue & "', II_FullAmount = '" & get_totalamount & "', II_Outstanding = '" & get_outstanding & "' WHERE II_ID = '" & find_transaction_id & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    errorCount = 0
                Else
                    errorCount = 1
                End If

            ElseIf ddlInvoice_Type.SelectedValue = "Full Payment" Then

                Dim quantity As TextBox = DirectCast(datRespondent.Rows(i).FindControl("IT_Quantity"), TextBox)
                Dim get_quantity As Decimal = Decimal.Parse(quantity.Text)

                ''get invoice item price from db
                Dim find_price As String = "select FIM_Price from fee_item_master 
                                            left join invoice_item on fee_item_master.FIM_ID = invoice_item.FIM_ID 
                                            where invoice_item.IT_ID = '" & strKey & "' and invoice_item.IT_Year = '" & ddlYear.SelectedValue & "' and invoice_item.IT_Item = fee_item_master.FIM_Item"
                Dim get_price As Decimal = Decimal.Parse(oCommon.getFieldValue(find_price))

                get_price = get_price * get_quantity

                strSQL = "UPDATE invoice_item set IT_Quantity ='" & quantity.Text & "', IT_Price = '" & get_price & "'
                          WHERE IT_ID ='" & strKey & "' and IT_Year = '" & ddlYear.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ''calculate new outstanding and total amount
                find_totalamount = "select SUM(IT_Price) from invoice_item where II_ID = '" & find_transaction_id & "' and IT_Year = '" & ddlYear.SelectedValue & "'"
                get_totalamount = Decimal.Parse(oCommon.getFieldValue(find_totalamount))
                find_outstanding = "select SUM(IT_Price) from invoice_item where II_ID = '" & find_transaction_id & "' and IT_Year = '" & ddlYear.SelectedValue & "' and IT_Status = 'Pending'"
                get_outstanding = Decimal.Parse(oCommon.getFieldValue(find_outstanding))

                ttl_InvPrice.Text = get_outstanding

                ''update invoice info DB
                strSQL = "UPDATE invoice_info set II_PaymentType = '" & ddlInvoice_Type.SelectedValue & "', II_FullAmount = '" & get_totalamount & "', II_Outstanding = '" & get_outstanding & "' WHERE II_ID = '" & find_transaction_id & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    errorCount = 0
                Else
                    errorCount = 1
                End If

            End If

        Next

        If errorCount > 0 Then
            ShowMessage("Update Invoice Detail", MessageType.Error)
        ElseIf errorCount = 0 Then
            ShowMessage("Update Invoice Detail", MessageType.Success)
        End If

        strRet = BindData(datRespondent, find_transaction_id)

    End Sub

    Private Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim strKeyName As String = GridView1.DataKeys(e.RowIndex).Value.ToString

        Try
            Invoice_Detail.Visible = True

            Invoice_Type_List()
            loadpage_invoice_detail(strKeyName)

            Dim find_outstanding As String = "select II_Outstanding from invoice_info where II_ID = '" & strKeyName & "'"
            Dim get_outstanding As Decimal = Decimal.Parse(oCommon.getFieldValue(find_outstanding))

            ttl_InvPrice.Text = get_outstanding

            strRet = BindData(datRespondent, strKeyName)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
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

            Dim i As Integer = 0
            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkStatus As Label = DirectCast(datRespondent.Rows(i).FindControl("IT_Status"), Label)
                Dim chkQuantity As TextBox = DirectCast(datRespondent.Rows(i).FindControl("IT_Quantity"), TextBox)

                If chkStatus.Text = "Paid" Then
                    chkQuantity.Enabled = False
                End If

            Next

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

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by fee_item_master.FIM_Item ASC"

        tmpSQL = "  select invoice_item.IT_ID, fee_item_master.FIM_Item, invoice_item.IT_Quantity, invoice_item.IT_Price, invoice_item.IT_Status from invoice_item
                    left join invoice_info on invoice_item.II_ID = invoice_info.II_ID
                    left join fee_item_master on invoice_item.FIM_ID = fee_item_master.FIM_ID"

        strWhere = " where invoice_info.II_Year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and invoice_info.Std_ID = '" & Request.QueryString("std_ID") & "'"
        strWhere += " and invoice_info.II_ID = '" & key & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Function getDataSQLPayment() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by II_ID ASC"

        tmpSQL = "select * from invoice_info"
        strWhere = " WHERE II_ID IS NOT NULL"
        strWhere += " and II_Year = '" & ddlYear.SelectedValue & "' and std_ID = '" & Request.QueryString("std_ID") & "'"

        getDataSQLPayment = tmpSQL & strWhere & strOrderby

        Return getDataSQLPayment
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub BtnBack_ServerClick(sender As Object, e As EventArgs) Handles BtnBack.ServerClick
        Try
            Dim back As String = Request.QueryString("back")

            If back = "0" Then
                Response.Redirect("admin_daftar_invois.aspx?admin_ID=" + Request.QueryString("admin_ID"))
            ElseIf back = "1" Then
                Response.Redirect("admin_transaksi_yuran.aspx?admin_ID=" + Request.QueryString("admin_ID"))
            End If


        Catch ex As Exception
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class