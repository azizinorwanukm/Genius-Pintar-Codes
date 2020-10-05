Imports System.Data.SqlClient

Public Class payment_list_table
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0
    Dim Data_Print As String = ""

    '' connection to kolejadmin databasse
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then
                    Year()
                    Level()
                    Inv_Group()

                    load_page()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "select Parameter from setting where Parameter = '" & Now.Year & "' and Type = 'Year'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlyear.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub Year()
        strSQL = "select Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Level()
        strSQL = "select Parameter From setting where Type = 'Level'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllevel.DataSource = ds
            ddllevel.DataTextField = "Parameter"
            ddllevel.DataValueField = "Parameter"
            ddllevel.DataBind()
            ddllevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddllevel.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Inv_Group()
        strSQL = "select Parameter, Value from setting where Type = 'Invoice Type' and idx = 'Payment'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlInvGroup.DataSource = ds
            ddlInvGroup.DataTextField = "Parameter"
            ddlInvGroup.DataValueField = "Value"
            ddlInvGroup.DataBind()
            ddlInvGroup.Items.Insert(0, New ListItem("Select Invoice Type", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddllevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddllevel.SelectedIndexChanged
        Try
            If ddlInvGroup.SelectedValue <> "" Then
                Fee_List()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlInvGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInvGroup.SelectedIndexChanged
        Try
            Fee_List()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Fee_List()

        Dim std_level As String = ddllevel.SelectedValue
        Dim std_year As String = ddlyear.SelectedValue

        Dim Test As String = ""
        Dim tmpSQL As String

        ''student fee
        tmpSQL = "select distinct invoice_detail.ID_Item, invoice_detail.ID_Quantity, invoice_detail.ID_Price, invoice_detail.ID_Catatan from invoice_transaction
                  left join invoice_info on invoice_transaction.II_ID = invoice_info.II_ID
                  left join invoice_detail on invoice_transaction.ID_ID = invoice_detail.ID_ID
                  where invoice_info.II_Year = '" & ddlyear.SelectedValue & "' and invoice_transaction.IT_Year = '" & ddlyear.SelectedValue & "' and invoice_detail.ID_Year = '" & ddlyear.SelectedValue & "'
                  and invoice_info.II_Level = '" & ddllevel.SelectedValue & "' and invoice_detail.ID_FeeType = 'Student Fee' and invoice_detail.ID_Group = '" & ddlInvGroup.SelectedValue & "'
                  order by invoice_detail.ID_Item ASC "
        Dim SQA As New SqlDataAdapter(tmpSQL, strConn)
        Dim DS As New DataTable

        ''subsidi fee
        tmpSQL = "select distinct invoice_transaction.IT_Item, invoice_transaction.IT_Quantity, invoice_transaction.IT_Price, invoice_detail.ID_Catatan from invoice_transaction
                  left join invoice_info on invoice_transaction.II_ID = invoice_info.II_ID
                  left join invoice_detail on invoice_transaction.ID_ID = invoice_detail.ID_ID
                  where invoice_info.II_Year = '" & ddlyear.SelectedValue & "' and invoice_transaction.IT_Year = '" & ddlyear.SelectedValue & "' and invoice_detail.ID_Year = '" & ddlyear.SelectedValue & "'
                  and invoice_info.II_Level = '" & ddllevel.SelectedValue & "' and invoice_detail.ID_FeeType = 'Subsidi Fee' and invoice_detail.ID_Group = '" & ddlInvGroup.SelectedValue & "'
                  order by invoice_detail.ID_Item ASC "
        Dim SQB As New SqlDataAdapter(tmpSQL, strConn)
        Dim DT As New DataTable

        Try
            SQA.Fill(DS)
            SQB.Fill(DT)
        Catch ex As Exception
        End Try

        Dim levelData As String = ""
        Dim InvoiceData As String = ""

        If ddllevel.SelectedValue = "Foundation 1" Then
            levelData = "Asas 1 (Tingkatan 1)"
        ElseIf ddllevel.SelectedValue = "Foundation 2" Then
            levelData = "Asas 2 (Tingkatan 2)"
        ElseIf ddllevel.SelectedValue = "Foundation 3" Then
            levelData = "Asas 3 (Tingkatan 3)"
        ElseIf ddllevel.SelectedValue = "Level 1" Then
            levelData = "Tahap 1 (Tingkatan 4)"
        ElseIf ddllevel.SelectedValue = "Level 2" Then
            levelData = "Tahap 2 (Tingkatan 5)"
        End If

        If ddlInvGroup.SelectedValue = "Fees Payment" Then
            InvoiceData = "Yuran"
        ElseIf ddlInvGroup.SelectedValue = "Hostel Payment" Then
            InvoiceData = "Hostel"
        ElseIf ddlInvGroup.SelectedValue = "Discipline Payment" Then
            InvoiceData = "Disiplin"
        End If


        Test = "<div id='data' style='background-color:white;border: 3px solid black;' align='center' >
                    <table style='margin-left:20px;margin-top:10px;margin-bottom:10px;margin-right:20px'>
                        <tr>
                            <table>
                                 <tr>
                                    <td>
                                        <img src='img/permata_logo.png' height='56' width='120' margin-left='auto' margin-right='auto'>
                                    </td>
                                    <td>
                                        <img src='img/ukm.jpg'  height='56' width='120' margin-left='auto' margin-right='auto'>
                                    </td>
                                </tr>
                            </table>
                        </tr>
                    </table>

                   <br><p style='text-align:center;color:black;margin-top:10px;margin-bottom:10px;margin-left:20px;margin-right:20px'><b> SENARAI BAYARAN " & InvoiceData.ToUpper() & " </b></p>
                   

                    <p style='align:center;color:black;margin-top:10px;margin-bottom:10px;margin-left:20px;margin-right:20px'><u> Bayaran " & InvoiceData & " bagi pelajar " & levelData & " bagi tahun " & ddlyear.SelectedValue & " </u></p>
                    
                    <table style='margin-left:20px;margin-right:20px;margin-top:10px;margin-bottom:10px;' border='1px solid black'>
                        <tr style='background-color:#bababc;text-align:center'>
                           <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Bil.</b></p></td>
                           <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Butiran</b></p></td>
                           <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Kuantiti</b></p></td>
                           <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Jumlah (RM)</b></p></td>
                           <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Catatan</b></p></td>
                        </tr>"

        Dim countA As Integer = 0
        For Each row As DataRow In DS.Rows
            countA = countA + 1
            Test += "<tr>"
            Test += "<td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>   " & countA & "</b></td>"
            For Each column As DataColumn In DS.Columns

                If column.ColumnName = "Description" Then
                    Test += "<td style='text-align:left;color:black;margin-left:10px;margin-right:10px'>" & row(column.ColumnName) & "</td>"
                ElseIf column.ColumnName = "Note" Then
                    Test += "<td style='text-align:center;color:black;margin-left:10px;margin-right:10px;width:250px'>" & row(column.ColumnName) & "</td>"
                Else
                    Test += "<td style='text-align:center;color:black;margin-left:10px;margin-right:10px'>" & row(column.ColumnName) & "</td>"
                End If

            Next
            Test += "</tr>"
        Next

        Dim data_Male As String = "select sum(invoice_detail.ID_Price) from invoice_transaction
                                   left join invoice_info on invoice_transaction.II_ID = invoice_info.II_ID
                                   left join invoice_detail on invoice_transaction.ID_ID = invoice_detail.ID_ID
                                   where invoice_info.II_Year = '" & ddlyear.SelectedValue & "' and invoice_transaction.IT_Year = '" & ddlyear.SelectedValue & "' and invoice_detail.ID_Year = '" & ddlyear.SelectedValue & "'
                                   and invoice_info.II_Level = '" & ddllevel.SelectedValue & "' and invoice_detail.ID_FeeType = 'Student Fee' and invoice_detail.ID_Group = '" & ddlInvGroup.SelectedValue & "'"
        Dim get_Male As String = getFieldValue(data_Male, strConn)

        Test += "<tr>
                    <td colspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px;text-align:center'><b> Jumlah Kesuluruhan </b></p></td>
                    <td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>  </b></td>
                    <td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>" & get_Male & "<b></td>
                 </tr>"
        Test += "</table>

                <p style='align:cecnter;color:black;margin-top:10px;margin-bottom:10px;margin-left:20px;margin-right:20px'><u> Maklumat Subsidi " & InvoiceData & " Kolej PERMATApintar®, UKM bagi tahun " & ddlyear.SelectedValue & " </u></p>

                <table style='margin-left:20px;margin-right:20px;margin-top:10px;margin-bottom:20px;' border='1px solid black'>
                    <tr style='background-color:#bababc;text-align:center'>
                        <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Bil.</b></p></td>
                        <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Butiran</b></p></td>
                        <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Kuantiti</b></p></td>
                        <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Jumlah (RM)</b></p></td>
                        <td ><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Catatan</b></p></td>
                    </tr>"

        Dim countB As Integer = 0
        For Each row As DataRow In DT.Rows
            countB = countB + 1
            Test += "<tr>"
            Test += "<td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>" & countB & "</b></td>"
            For Each column As DataColumn In DT.Columns

                If column.ColumnName = "Description" Then
                    Test += "<td style='text-align:left;color:black;margin-left:10px;margin-right:10px'>" & row(column.ColumnName) & "</td>"
                ElseIf column.ColumnName = "Note" Then
                    Test += "<td style='text-align:center;color:black;margin-left:10px;margin-right:10px;width:250px'>" & row(column.ColumnName) & "</td>"
                Else
                    Test += "<td style='text-align:center;color:black;margin-left:10px;margin-right:10px'>" & row(column.ColumnName) & "</td>"
                End If

            Next
            Test += "</tr>"
        Next

        Dim data_Male_Sub As String = "select sum(invoice_detail.ID_Price) from invoice_transaction
                                       left join invoice_info on invoice_transaction.II_ID = invoice_info.II_ID
                                       left join invoice_detail on invoice_transaction.ID_ID = invoice_detail.ID_ID
                                       where invoice_info.II_Year = '" & ddlyear.SelectedValue & "' and invoice_transaction.IT_Year = '" & ddlyear.SelectedValue & "' and invoice_detail.ID_Year = '" & ddlyear.SelectedValue & "'
                                       and invoice_info.II_Level = '" & ddllevel.SelectedValue & "' and invoice_detail.ID_FeeType = 'Subsidi Fee' and invoice_detail.ID_Group = '" & ddlInvGroup.SelectedValue & ""
        Dim get_Male_Sub As String = getFieldValue(data_Male_Sub, strConn)

        Test += "<tr>
                    <td colspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px;text-align:center'><b> Jumlah Kesuluruhan </b></p></td>
                    <td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b> </b></td>
                    <td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>" & get_Male_Sub & "</b></td>
                 </tr>"

        Dim data_Payment_Male As String = "select sum(invoice_detail.ID_Price) from invoice_transaction
                                           left join invoice_info on invoice_transaction.II_ID = invoice_info.II_ID
                                           left join invoice_detail on invoice_transaction.ID_ID = invoice_detail.ID_ID
                                           where invoice_info.II_Year = '" & ddlyear.SelectedValue & "' and invoice_transaction.IT_Year = '" & ddlyear.SelectedValue & "' and invoice_detail.ID_Year = '" & ddlyear.SelectedValue & "'
                                           and invoice_info.II_Level = '" & ddllevel.SelectedValue & "' and invoice_detail.ID_FeeType = 'Student Fee' and invoice_detail.ID_Group = '" & ddlInvGroup.SelectedValue & "'
                                           and invoice_detail.ID_PaymentType = 'Counter Payment'"
        Dim get_Payment_Male As String = getFieldValue(data_Payment_Male, strConn)

        Dim data_Payment_BankIn As String = "select sum(invoice_detail.ID_Price) from invoice_transaction
                                            left join invoice_info on invoice_transaction.II_ID = invoice_info.II_ID
                                            left join invoice_detail on invoice_transaction.ID_ID = invoice_detail.ID_ID
                                            where invoice_info.II_Year = '" & ddlyear.SelectedValue & "' and invoice_transaction.IT_Year = '" & ddlyear.SelectedValue & "' and invoice_detail.ID_Year = '" & ddlyear.SelectedValue & "'
                                            and invoice_info.II_Level = '" & ddllevel.SelectedValue & "' and invoice_detail.ID_FeeType = 'Student Fee' and invoice_detail.ID_Group = '" & ddlInvGroup.SelectedValue & "'
                                            and invoice_detail.ID_PaymentType = 'Online Payment' "
        Dim get_Payment_BankIn As String = getFieldValue(data_Payment_BankIn, strConn)

        Test += "</table>
                
                <div  style='text-align:left;color:black;margin-top:30px;margin-bottom:20px;margin-left:20px;margin-right:20px;border: 1px solid black;width:650px' align='center'>
                    <p style='text-align:left;color:black;margin-top:10px;margin-bottom:10px;margin-left:30px;margin-right:40px'><b>*</b>Mohon Ibubapa/Penjaga membayar jumlah sebanyak <b>RM" & get_Payment_Male & "</b> (pelajar lelaki) 
                                                                                                                                 atau <b>RM" & get_Payment_Male & "</b> (pelajar perempuan) di kaunter bank / atas talian / mesin deposit tunai
                                                                                                                                 dan membawa resit pembayaran semasa hari pendaftaran.</p>
                    <p style='text-align:left;color:black;margin-top:10px;margin-bottom:10px;margin-left:30px;margin-right:40px'><b>**</b>Mohon Ibubapa/Penjaga membayar jumlah sebanyak <b>RM" & get_Payment_BankIn & "</b> semasa hari pendaftaran.</p>
                </div>
            </div>"

        FEE_Lists.Text = Test

        Data_Print = Test

    End Sub

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Sub Btnprint_ServerClick(sender As Object, e As EventArgs) Handles Btnprint.ServerClick

        Data_Print = "<script type='text/javascript'>  var divToPrint=document.getElementById('data'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>"

        'Data_Print = "<script type='text/javascript'>  
        '                    var a = document.body.appendChild(document.createElement('a'));
        '                    a.download = 'Email.html';
        '                    a.href = 'data:Text/ html,' + document.getElementById('data').outerHTML; 
        '                    a.click(); 
        '            </script>"

        'Data_Print = "<script type='text/javascript'>   
        '                var doc = new jsPDF();
        '                var specialElementHandlers = 
        '                    {
        '                        #editor': function (element, renderer) 
        '                            {  return true; }
        '                    };

        '                doc.fromHTML($('#data').html(), 15, 15, {'width': 170, 'elementHandlers': specialElementHandlers});
        '                doc.save('invoice.pdf');
        '             </script>"

        Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Data_Print.ToString())
    End Sub


End Class