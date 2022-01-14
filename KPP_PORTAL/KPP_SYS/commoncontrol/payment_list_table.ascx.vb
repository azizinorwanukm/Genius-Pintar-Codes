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

    Protected Sub ddllevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddllevel.SelectedIndexChanged
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
        tmpSQL = "Select Description,Std_Male, Std_Female, Note from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Student Fee'"
        Dim SQA As New SqlDataAdapter(tmpSQL, strConn)
        Dim DS As New DataTable

        ''subsidi fee
        tmpSQL = "Select Description,Std_Male, Std_Female, Note from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Subsidi Fee'"
        Dim SQB As New SqlDataAdapter(tmpSQL, strConn)
        Dim DT As New DataTable

        Try
            SQA.Fill(DS)
            SQB.Fill(DT)
        Catch ex As Exception
        End Try

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

                   <br><p style='text-align:center;color:black;margin-top:10px;margin-bottom:10px;margin-left:20px;margin-right:20px'><b> SENARAI YURAN PENGAJIAN PELAJAR BARU </b></p>
                   

                    <p style='text-align:left;color:black;margin-top:10px;margin-bottom:10px;margin-left:20px;margin-right:20px'><u> Bayaran yuran bagu pelajar Tahap 1 (Tingkatan 4) dan Asas 1 (Tingkatan 1) bagi tempoh setahun pengajian </u></p>
                    
                    <table style='margin-left:20px;margin-right:20px;margin-top:10px;margin-bottom:10px;' border='1px solid black'>
                        <tr style='background-color:#bababc;text-align:center'>
                           <td rowspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Bil.</b></p></td>
                           <td rowspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Butiran</b></p></td>
                           <td colspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Jumlah (RM)</b></p></td>
                           <td rowspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Catatan</b></p></td>
                        </tr>
                        <tr style='background-color:#bababc;text-align:center'>
                           <td><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Lelaki </b></p></td>
                           <td><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Perempuan </b></p></td>
                        </tr>"

        Dim countA As Integer = 0
        For Each row As DataRow In DS.Rows
            countA = countA + 1
            Test += "<tr>"
            Test += "<td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>*" & countA & "</b></td>"
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

        Dim data_Male As String = "Select Max(Total_Male) from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Student Fee'"
        Dim get_Male As String = getFieldValue(data_Male, strConn)

        Dim data_Female As String = "Select Max(Total_Female) from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Student Fee'"
        Dim get_Female As String = getFieldValue(data_Female, strConn)

        Test += "<tr>
                    <td colspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px;text-align:center'><b> Jumlah Kesuluruhan </b></p></td>
                    <td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>" & get_Male & "</b></td>
                    <td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>" & get_Female & "<b></td>
                 </tr>"
        Test += "</table>

                <p style='text-align:left;color:black;margin-top:10px;margin-bottom:10px;margin-left:20px;margin-right:20px'><u> Maklumat Subsidi Yuran Kolej PERMATApintar®, UKM bagi tempoh setahun pengajian </u></p>

                <table style='margin-left:20px;margin-right:20px;margin-top:10px;margin-bottom:20px;' border='1px solid black'>
                    <tr style='background-color:#bababc;text-align:center'>
                        <td rowspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Bil.</b></p></td>
                        <td rowspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Butiran</b></p></td>
                        <td colspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Jumlah (RM)</b></p></td>
                        <td rowspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Catatan</b></p></td>
                    </tr>
                    <tr style='background-color:#bababc;text-align:center'>
                        <td><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Lelaki </b></p></td>
                        <td><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px'><b>Perempuan </b></p></td>
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

        Dim data_Male_Sub As String = "Select Max(Total_Male) from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Subsidi Fee'"
        Dim get_Male_Sub As String = getFieldValue(data_Male_Sub, strConn)

        Dim data_Female_Sub As String = "Select Max(Total_Female) from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Subsidi Fee'"
        Dim get_Female_Sub As String = getFieldValue(data_Female_Sub, strConn)

        Test += "<tr>
                    <td colspan='2'><p style='color:black;margin-top:0px;margin-bottom:0px;margin-left:10px;margin-right:10px;text-align:center'><b> Jumlah Kesuluruhan </b></p></td>
                    <td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>" & get_Male_Sub & "</b></td>
                    <td style='text-align:center;color:black;margin-left:10px;margin-right:10px'><b>" & get_Female_Sub & "</b></td>
                 </tr>"

        Dim data_Payment_Male As String = "Select Max(Total_Male) from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Student Fee' and Type_Of_Payment = 'Counter Payment'"
        Dim get_Payment_Male As String = getFieldValue(data_Payment_Male, strConn)

        Dim data_Payment_Female As String = "Select Max(Total_Female) from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Student Fee' and Type_Of_Payment = 'Counter Payment'"
        Dim get_Payment_Female As String = getFieldValue(data_Payment_Female, strConn)

        Dim data_Payment_BankIn As String = "Select Sum(Std_Female) from payment_info
                  where Year = '" & ddlyear.SelectedValue & "' and student_Level = '" & ddllevel.SelectedValue & "' and Payment_Type = 'Student Fee' and Type_Of_Payment = 'Bank-In Payment'"
        Dim get_Payment_BankIn As String = getFieldValue(data_Payment_BankIn, strConn)

        Test += "</table>
                
                <div  style='text-align:left;color:black;margin-top:30px;margin-bottom:20px;margin-left:20px;margin-right:20px;border: 1px solid black;width:700px' align='center'>
                    <p style='text-align:left;color:black;margin-top:10px;margin-bottom:10px;margin-left:40px;margin-right:40px'><b>*</b>Mohon Ibubapa/Penjaga membayar jumlah dari <b>1</b> hingga <b>16</b> sebanyak <b>RM" & get_Payment_Male & "</b> (pelajar lelaki) 
                                                                                                                                 atau <b>RM" & get_Payment_Female & "</b> (pelajar perempuan) di kaunter bank / atas talian / mesin deposit tunai
                                                                                                                                 dan membawa resit pembayaran semasa hari pendaftaran.</p>
                    <p style='text-align:left;color:black;margin-top:10px;margin-bottom:10px;margin-left:40px;margin-right:40px'><b>**</b>Mohon Ibubapa/Penjaga membayar jumlah <b>17</b> dan <b>18</b> sebanyak <b>RM" & get_Payment_BankIn & "</b> semasa hari pendaftaran.</p>
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

        Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Data_Print.ToString())
    End Sub
End Class