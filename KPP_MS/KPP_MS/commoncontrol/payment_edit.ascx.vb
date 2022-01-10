Imports System.Data.SqlClient
Imports System.IO

Public Class payment_edit
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                previousPage.NavigateUrl = String.Format("~/admin_daftar_yuran_baru.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                Year()
                Fee_Type()

                LoadPage()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Year()
        strSQL = "select Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear.SelectedIndex = 0
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Fee_Type()
        strSQL = "select * from setting where Type = 'Payment_Type' and idx = 'Payment'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlType.DataSource = ds
            ddlType.DataTextField = "Parameter"
            ddlType.DataValueField = "Value"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Select Fee Type", String.Empty))
            ddlType.SelectedIndex = 0
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LoadPage()

        strSQL = "  Select * from fee_item_master where FIM_ID = '" & Request.QueryString("FIM_ID") & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("FIM_Year")
            Else
                ddlYear.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Level")) Then
                Dim find_Level As String = ds.Tables(0).Rows(0).Item("FIM_Level")

                If find_Level = "Foundation 1" Then
                    CB_F1.Checked = True
                ElseIf find_Level = "Foundation 2" Then
                    CB_F2.Checked = True
                ElseIf find_Level = "Foundation 3" Then
                    CB_F3.Checked = True
                ElseIf find_Level = "Level 1" Then
                    CB_L1.Checked = True
                ElseIf find_Level = "Level 2" Then
                    CB_L2.Checked = True
                End If
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Type")) Then
                ddlType.SelectedValue = ds.Tables(0).Rows(0).Item("FIM_Type")
            Else
                ddlType.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Item")) Then
                Inv_Name.Text = ds.Tables(0).Rows(0).Item("FIM_Item")
            Else
                Inv_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Quantity")) Then
                Inv_Quantity.Text = ds.Tables(0).Rows(0).Item("FIM_Quantity")
            Else
                Inv_Quantity.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Gender")) Then
                Dim txtgender As String = ds.Tables(0).Rows(0).Item("FIM_Gender")

                If txtgender = "All" Then
                    rbtn_Both.Checked = True
                ElseIf txtgender = "Male" Then
                    rbtn_Male.Checked = True
                ElseIf txtgender = "Female" Then
                    rbtn_Female.Checked = True
                End If
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Religion")) Then
                Dim txtreligion As String = ds.Tables(0).Rows(0).Item("FIM_Religion")

                If txtreligion = "All" Then
                    rbtn_All.Checked = True
                ElseIf txtreligion = "Muslim" Then
                    rbtn_Muslim.Checked = True
                ElseIf txtreligion = "Non_Muslim" Then
                    rbtn_NonMuslim.Checked = True
                End If
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Price")) Then
                Std_Price.Text = ds.Tables(0).Rows(0).Item("FIM_Price")
            Else
                Std_Price.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("FIM_Remark")) Then
                Inv_Remark.Text = ds.Tables(0).Rows(0).Item("FIM_Remark")
            Else
                Inv_Remark.Text = ""
            End If
        End If
    End Sub

    Private Sub btnAddPayment_ServerClick(sender As Object, e As EventArgs) Handles btnAddPayment.ServerClick

        Session("Invoice_Level") = ""
        Session("Invoice_Gender") = ""
        Session("Invoice_Religion") = ""

        If rbtn_Male.Checked = True Then
            Session("Invoice_Gender") = "Male"
        End If
        If rbtn_Female.Checked = True Then
            Session("Invoice_Gender") = "Female"
        End If
        If rbtn_Both.Checked = True Then
            Session("Invoice_Gender") = "All"
        End If

        If rbtn_Muslim.Checked = True Then
            Session("Invoice_Religion") = "Muslim"
        End If
        If rbtn_NonMuslim.Checked = True Then
            Session("Invoice_Religion") = "Non-Muslim"
        End If
        If rbtn_All.Checked = True Then
            Session("Invoice_Religion") = "All"
        End If

        If ddlYear.SelectedIndex > 0 Then

            If Inv_Name.Text.Length > 0 Then

                If CB_F1.Checked = True Or CB_F2.Checked = True Or CB_F3.Checked = True Or CB_L1.Checked = True Or CB_L2.Checked = True Then

                    If Std_Price.Text.Length > 0 Then

                        If Session("Invoice_Gender").Length > 0 Then

                            If Session("Invoice_Religion").Length > 0 Then

                                If ddlType.SelectedIndex > 0 Then

                                    If ddlYear.SelectedValue = Now.Year Then

                                        If CB_F1.Checked = True Then
                                            Session("Invoice_Level") = "Foundation 1"
                                            UpdateInvoice()
                                        End If

                                        If CB_F2.Checked = True Then
                                            Session("Invoice_Level") = "Foundation 2"
                                            UpdateInvoice()
                                        End If

                                        If CB_F3.Checked = True Then
                                            Session("Invoice_Level") = "Foundation 3"
                                            UpdateInvoice()
                                        End If

                                        If CB_L1.Checked = True Then
                                            Session("Invoice_Level") = "Level 1"
                                            UpdateInvoice()
                                        End If

                                        If CB_L2.Checked = True Then
                                            Session("Invoice_Level") = "Level 2"
                                            UpdateInvoice()
                                        End If

                                        If strRet = 0 Then
                                            ShowMessage("Update Payment Items", MessageType.Success)
                                        Else
                                            ShowMessage("Unsuccessful Update Payment Items", MessageType.Error)
                                        End If

                                    Else
                                        ShowMessage("Unable to change previous year data", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage("Please select type of fee", MessageType.Error)
                                End If
                            Else
                                ShowMessage("Please select religion", MessageType.Error)
                            End If
                        Else
                            ShowMessage("Please select gender", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please enter price", MessageType.Error)
                    End If

                Else
                    ShowMessage("Please select level", MessageType.Error)
                End If
            Else
                ShowMessage("Please enter a valid invoice name", MessageType.Error)
            End If
        Else
            ShowMessage("Please select year ", MessageType.Error)
        End If
    End Sub

    Private Sub UpdateInvoice()

        strSQL = "  Update fee_item_master set FIM_Year = '" & ddlYear.SelectedValue & "', FIM_Item = '" & Inv_Name.Text & "', FIM_Quantity = '" & Inv_Quantity.Text & "', FIM_Price = '" & Std_Price.Text & "', FIM_Gender = '" & Session("Invoice_Gender") & "', FIM_Type = '" & ddlType.SelectedValue & "',
                    FIM_Remark = '" & Inv_Remark.Text & "', FIM_Religion = '" & Session("Invoice_Religion") & "', FIM_Level = '" & Session("Invoice_Level").SelectedValue & "' where FIM_ID = '" & Request.QueryString("FIM_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class