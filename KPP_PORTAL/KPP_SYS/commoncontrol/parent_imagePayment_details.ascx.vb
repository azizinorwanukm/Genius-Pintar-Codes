Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class parent_imagePayment_details
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

                Reload_datRespondant()
                strRet = BindData(datRespondent)

                hyperlinkInvoiceInformation.NavigateUrl = "~/penjaga_bayaran.aspx"

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Reload_datRespondant()

        strSQL = "  Select UPPER(A.Student_Name) as Student_Name, B.II_InvNo, B.II_FullAmount, B.II_Outstanding from student_info A
                    Left Join invoice_info B on A.std_ID = B.Std_ID
                    where B.II_ID = '" & Session("II_ID") & "'"

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
        strWhere = "    where A.II_ID = '" & Session("II_ID") & "'"

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

    Private Sub BtnUpdateInvoice_ServerClick(sender As Object, e As EventArgs) Handles BtnUpdateInvoice.ServerClick

        ''Checking if the invoice had been lock/publish
        strSQL = "Select II_Published from invoice_info where II_ID = '" & Session("II_ID") & "'"
        Dim find_InvoiceLock As String = oCommon.getFieldValue(strSQL)

        If find_InvoiceLock <> "Yes" Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim txtQuantity As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtIT_Quantity"), TextBox)

                If txtQuantity.Text.Length > 0 And (txtQuantity.Text > 0 And txtQuantity.Text <= 10) And IsNumeric(txtQuantity.Text) Then

                    ''Get PK
                    Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

                    ''Get Unit Price
                    Dim find_UnitPrice As String = " Select A.FIM_Price from fee_item_master A left join invoice_item B on A.FIM_ID = B.FIM_ID where IT_ID = '" & strKeyID & "' and II_ID = '" & Session("II_ID") & "'"
                    Dim get_UnitPrice As String = oCommon.getFieldValue(find_UnitPrice)

                    ''Get Unit Quantity
                    Dim find_UnitQuantity As String = " Select A.FIM_Quantity from fee_item_master A left join invoice_item B on A.FIM_ID = B.FIM_ID where IT_ID = '" & strKeyID & "' and II_ID = '" & Session("II_ID") & "'"
                    Dim get_UnitQuantity As String = oCommon.getFieldValue(find_UnitQuantity)

                    ''Calculate New Price
                    Dim new_UnitPrice As Double = 0.0
                    If txtQuantity.Text > get_UnitQuantity Then
                        new_UnitPrice = get_UnitPrice * txtQuantity.Text
                    ElseIf txtQuantity.Text < get_UnitQuantity Then
                        new_UnitPrice = get_UnitPrice / txtQuantity.Text
                    ElseIf txtQuantity.Text = get_UnitQuantity Then
                        new_UnitPrice = get_UnitPrice
                    End If

                    ''Update New Price into Invoice Item
                    strSQL = "Update invoice_item set IT_Price = '" & new_UnitPrice & "',IT_Quantity = '" & txtQuantity.Text & "'  where IT_ID = '" & strKeyID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            Next

            If strRet = 0 Then
                ''Calculate New FullAmount Price
                strSQL = "  Select Sum(B.IT_Price) from invoice_info A Left Join invoice_item B on A.II_ID = B.II_ID Left Join fee_item_master C on B.FIM_ID = C.FIM_ID
                            where A.II_ID = '" & Session("II_ID") & "'"
                Dim SumFullAmount_Pending As String = oCommon.getFieldValue(strSQL)

                ''Calculate New Outstanding Price
                strSQL = "  Select Sum(B.IT_Price) from invoice_info A Left Join invoice_item B on A.II_ID = B.II_ID Left Join fee_item_master C on B.FIM_ID = C.FIM_ID
                            where A.II_ID = '" & Session("II_ID") & "' and B.IT_Status = 'Pending'"
                Dim SumOutstanding_Pending As String = oCommon.getFieldValue(strSQL)

                If SumOutstanding_Pending.Length = 0 Then
                    SumOutstanding_Pending = "0.00"
                End If

                strSQL = "Update invoice_info set II_Outstanding = '" & SumOutstanding_Pending & "', II_FullAmount = '" & SumFullAmount_Pending & "' where II_ID = '" & Session("II_ID") & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage("Successful update items status ", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Student Result", MessageType.Error)
            End If

        Else
            ShowMessage(" Unable to update. Invoice had been locked", MessageType.Error)
        End If

        Reload_datRespondant()
        strRet = BindData(datRespondent)
    End Sub

    Private Sub BtnBayar_ServerClick(sender As Object, e As EventArgs) Handles BtnBayar.ServerClick

        Dim Test As New StringBuilder()

        Dim error_count As Integer
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim total_Price As Decimal = 0.0
        Dim concat_ID As String = ""
        Dim find_IIInvNo As String = ""
        Dim get_IIInvNo As String = ""

        If txt_desc.Text.Length > 0 Then

            Dim find_stdid As String = "select std_id from student_SecurityID where loginID_Number = '" & Session("Parent_ID") & "'"
            Dim get_stdid As String = oCommon.getFieldValue(find_stdid)

            Dim find_parentName As String = "select parent_Name from parent_info where parent_ID = '" & get_stdid & "'"
            Dim get_parentName As String = oCommon.getFieldValue(find_parentName)

            Dim find_parentEmail As String = "select parent_Email from parent_info where parent_ID = '" & get_stdid & "'"
            Dim get_parentEmail As String = oCommon.getFieldValue(find_parentEmail)

            Dim epay As String = "BiTARaPinTAR"
            Dim desc As String = txt_desc.Text

            Dim find_itemStatus As String = ""
            Dim get_itemStatus As String = ""

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)

                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    Dim IT_Price_txt As Label = DirectCast(datRespondent.Rows(i).FindControl("IT_Price"), Label)

                    If chkUpdate.Checked = True Then

                        find_itemStatus = "Select IT_Status from invoice_item where IT_ID = '" & strKey & "'"
                        get_itemStatus = oCommon.getFieldValue(find_itemStatus)

                        If get_itemStatus = "Pending" Then
                            total_Price = total_Price + IT_Price_txt.Text

                            If j = 0 Then
                                ''checking II_RefNo existed or not
                                Dim find_IIID As String = "select II_ID from invoice_item where IT_ID = '" & strKey & "'"
                                Dim get_IIID As String = oCommon.getFieldValue(find_IIID)

                                get_IIInvNo = get_IIID

                                Dim checking_ITRefNo As String = "select distinct MAX(IT_RefNo) from invoice_item where II_ID = '" & get_IIID & "'"
                                Dim get_ITRefNo As String = oCommon.getFieldValue(checking_ITRefNo)

                                If get_ITRefNo.Length = 0 Then
                                    ''create IT_RefNo from invoice info
                                    find_IIInvNo = "  select invoice_info.II_InvNo from invoice_info
                                                      left join invoice_item on invoice_info.II_ID = invoice_item.II_ID
                                                      where IT_ID = '" & strKey & "'"
                                    get_IIInvNo = oCommon.getFieldValue(find_IIInvNo) & "-" & (i + 1)

                                Else
                                    ''create IT_RefNo from existing reference no
                                    Dim get_lastNo As String = get_ITRefNo.Substring(get_ITRefNo.Length - 1)
                                    Dim new_lastNo As String = get_lastNo + 1

                                    Dim remove_lastNo As String = get_ITRefNo.Remove(get_ITRefNo.Length - 1)
                                    get_IIInvNo = remove_lastNo & new_lastNo

                                End If

                                ''Insert into invoice_item db
                                strSQL = "update invoice_item set IT_RefNo = '" & get_IIInvNo & "', IT_Desc = '" & txt_desc.Text & "' where IT_ID = '" & strKey & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            Else
                                ''Insert into invoice_item db
                                strSQL = "update invoice_item set IT_RefNo = '" & get_IIInvNo & "', IT_Desc = '" & txt_desc.Text & "' where IT_ID = '" & strKey & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            End If

                            j = j + 1

                            error_count = 0 'success'

                        Else
                            error_count = 1 'error item has been paid'
                            Exit For

                        End If

                    End If
                End If
            Next

            If error_count = 0 Then
                Test.AppendLine("<script type='text/javascript'> window.open('http://smkphp.ukm.my/epayment/pay/get_url?amount=" & total_Price & "&id_trans=" & get_IIInvNo & "&kod_epay=" & epay & "&bill_name=" & get_parentName & "&bill_email=" & get_parentEmail & "&bill_desc=" & txt_desc.Text & "') </script>")
                Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Test.ToString())

            ElseIf error_count = 1 Then

                ShowMessage(" The Selected Item Had Been Paid", MessageType.Error)
                strRet = BindData(datRespondent)

            End If

        Else
            ShowMessage(" Please Fill In Description Form ", MessageType.Error)
        End If
    End Sub

    Private Sub UpdatePaymentDB(ByVal kodepay As String)

        strSQL = "Select II_InvNo from invoice_info where II_ID = '" & Session("II_ID") & "'"
        Dim RefNo As String = oCommon.getFieldValue(strSQL)

        'We this to make an HTTP web request
        Dim req As Net.HttpWebRequest = Net.WebRequest.Create("https://epayment.ukm.my/ws_epay/infotrans?kod_epay=" & kodepay & "&format=json&id_trans=" & RefNo)

        'Make the web request and get the response
        Dim response As Net.WebResponse = req.GetResponse

        Dim stream As System.IO.Stream = response.GetResponseStream

        'Prepare buffer for reading from stream
        Dim buffer As Byte() = New Byte(1000) {}

        'Data read from stream is gathered here
        Dim data As New List(Of Byte)

        'Start reading stream
        Dim bytesRead = stream.Read(buffer, 0, buffer.Length)

        Do Until bytesRead = 0
            For i = 0 To bytesRead - 1
                data.Add(buffer(i))
            Next

            bytesRead = stream.Read(buffer, 0, buffer.Length)
        Loop

        'Gets the JSON data
        Dim dataJSON As String = System.Text.Encoding.UTF8.GetString(data.ToArray)
        Dim json As JObject = JObject.Parse(dataJSON)

        Dim get_data As String = json.Item("nmsts")

        If IsNothing(get_data) Then
            ShowMessage("The selected item has been paid", MessageType.Error)

        ElseIf get_data <> "00" Then
            ShowMessage("Your payment are in process. Please wait a moment ", MessageType.Error)

        Else
            Dim paydate As String = json.Item("paydate")

            strSQL = "update invoice_item set IT_STatus = 'Paid', IT_Date = '" & paydate & "' where II_ID = '" & Session("II_ID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If

        response.Close()
        stream.Close()

        strRet = BindData(datRespondent)
    End Sub

    Private Sub BtnRefresh_ServerClick(sender As Object, e As EventArgs) Handles BtnRefresh.ServerClick
        Dim epay As String = "BiTARaPinTAR"
        UpdatePaymentDB(epay)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

End Class
