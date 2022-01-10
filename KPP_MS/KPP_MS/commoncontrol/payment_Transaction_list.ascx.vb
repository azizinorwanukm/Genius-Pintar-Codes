Imports System.Data.SqlClient
Imports System.IO
Public Class payment_Transaction_list
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                previousPage1.NavigateUrl = String.Format("~/admin_transaksi_yuran.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                LoadPage()
                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadPage()

        strSQL = "  Select UPPER(A.Student_Name) as Student_Name, B.II_InvNo, B.II_FullAmount, B.II_Outstanding from student_info A
                    Left Join invoice_info B on A.std_ID = B.Std_ID
                    where B.II_ID = '" & Session("getII_ID") & "'"

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

            Inv_StudentName_LBL.Text = ds.Tables(0).Rows(0).Item("Student_Name")
            Inv_Number_LBL.Text = ds.Tables(0).Rows(0).Item("II_InvNo")
            Inv_Totalamount_LBL.Text = "RM " & ds.Tables(0).Rows(0).Item("II_FullAmount")
            Inv_Outstanding_LBL.Text = "RM " & ds.Tables(0).Rows(0).Item("II_Outstanding")

            Inv_StudentName_LBL.Font.Bold = True
            Inv_Number_LBL.Font.Bold = True
            Inv_Totalamount_LBL.Font.Bold = True
            Inv_Outstanding_LBL.Font.Bold = True

        End If
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
            run_color()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by C.FIM_Item asc"

        tmpSQL = "  Select B.IT_ID, C.FIM_Year, C.FIM_Item, B.IT_Quantity, C.FIM_Price, B.IT_Price ,B.IT_Status, B.IT_Status as Status_Color, B.IT_Date from invoice_info A
                    Left Join invoice_item B on A.II_ID = B.II_ID
                    Left Join fee_item_master C on B.FIM_ID = C.FIM_ID"
        strWhere = "    where A.II_ID = '" & Session("getII_ID") & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        For row = 0 To datRespondent.Rows.Count - 1 Step row + 1
            lblDay = datRespondent.Rows(row).Cells(6).FindControl("Status_Color")
            If lblDay.Text = "Pending" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Red
                lblDay.ForeColor = Drawing.Color.Red
                lblDay.CssClass = "lblAbsent"

            End If

            If lblDay.Text = "Paid" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Green
                lblDay.ForeColor = Drawing.Color.Green
                lblDay.CssClass = "lblAttend"

            End If
        Next
    End Sub

    Private Sub BtnUpdate_ServerClick(sender As Object, e As EventArgs) Handles BtnUpdate.ServerClick

        ''Checking if the invoice had been lock/publish
        strSQL = "Select II_Published from invoice_info where II_ID = '" & Session("getII_ID") & "'"
        Dim find_InvoiceLock As String = oCommon.getFieldValue(strSQL)

        If find_InvoiceLock <> "Yes" Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim txtQuantity As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtIT_Quantity"), TextBox)

                If txtQuantity.Text.Length > 0 And (txtQuantity.Text > 0 And txtQuantity.Text <= 10) And IsNumeric(txtQuantity.Text) Then

                    ''Get PK
                    Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

                    ''Get Unit Price
                    Dim find_UnitPrice As String = " Select A.FIM_Price from fee_item_master A left join invoice_item B on A.FIM_IF = B.FIM_IF where IT_ID = '" & strKeyID & "' and II_ID = '" & Session("getII_ID") & "'"
                    Dim get_UnitPrice As String = oCommon.getFieldValue(find_UnitPrice)

                    ''Calculate New Price
                    Dim new_UnitPrice As Double = get_UnitPrice * txtQuantity.Text

                    ''Update New Price into Invoice Item
                    strSQL = "Update invoice_item set IT_Price = '' where IT_ID = '" & strKeyID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            Next

            If strRet = 0 Then
                ''Calculate New FullAmount Price
                strSQL = "  Select Sum(B.IT_Price) from invoice_info A Left Join invoice_item B on A.II_ID = B.II_ID Left Join fee_item_master C on B.FIM_ID = C.FIM_ID
                            where A.II_ID = '" & Session("getII_ID") & "'"
                Dim SumFullAmount_Pending As String = oCommon.getFieldValue(strSQL)

                ''Calculate New Outstanding Price
                strSQL = "  Select Sum(B.IT_Price) from invoice_info A Left Join invoice_item B on A.II_ID = B.II_ID Left Join fee_item_master C on B.FIM_ID = C.FIM_ID
                            where A.II_ID = '" & Session("getII_ID") & "' and B.IT_Status = 'Pending'"
                Dim SumOutstanding_Pending As String = oCommon.getFieldValue(strSQL)

                If SumOutstanding_Pending.Length = 0 Then
                    SumOutstanding_Pending = "0.00"
                End If

                strSQL = "Update invoice_info set II_Outstanding = '" & SumOutstanding_Pending & "', II_FullAmount = '" & SumFullAmount_Pending & "' where II_ID = '" & Session("getII_ID") & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage("Successful update items status ", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Student Result", MessageType.Error)
            End If

        Else
            ShowMessage(" Unable to update. Invoice had been locked", MessageType.Error)
        End If

        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub
    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class