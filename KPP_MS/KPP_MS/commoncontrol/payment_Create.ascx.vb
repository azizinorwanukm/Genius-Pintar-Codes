Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class payment_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Dim reload As String = "0"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim display_data As String = Request.QueryString("value")

                If display_data = "0" Then
                    payment_baru.Visible = False
                Else
                    payment_baru.Visible = True
                End If

                Year()
                Gender_List()
                Fee_Type()
                load_page()
                strRet = BindData(datRespondent)
            End If

            If IsPostBack Then
                Dim display_data As String = Request.QueryString("value")

                If display_data = "0" Then
                    payment_baru.Visible = False
                Else
                    payment_baru.Visible = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        '' get year
        strSQL = "select Parameter from setting where Parameter = '" & Now.Year & "' and Type = 'Year'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
                ddlYear_List.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlYear.SelectedValue = ""
                ddlYear_List.SelectedValue = ""
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

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear.SelectedIndex = 0

            ddlYear_List.DataSource = ds
            ddlYear_List.DataTextField = "Parameter"
            ddlYear_List.DataValueField = "Parameter"
            ddlYear_List.DataBind()
            ddlYear_List.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear_List.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Gender_List()
        strSQL = "select Parameter,Value from setting where Type = 'Student Gender'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlGender.DataSource = ds
            ddlGender.DataTextField = "Parameter"
            ddlGender.DataValueField = "Value"
            ddlGender.DataBind()
            ddlGender.Items.Insert(0, New ListItem("All", "All"))
            ddlGender.SelectedIndex = 0

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
            ddlType.Items.Insert(0, New ListItem("Select Fee    ", String.Empty))
            ddlType.SelectedIndex = 0

            ddlType_List.DataSource = ds
            ddlType_List.DataTextField = "Parameter"
            ddlType_List.DataValueField = "Value"
            ddlType_List.DataBind()
            ddlType_List.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try

            If ddlYear_List.SelectedValue <> Now.Year Then

                ShowMessage("Unable to delete data year " & ddlYear_List.SelectedValue, MessageType.Error)

            Else

                Dim MyConnection As SqlConnection = New SqlConnection(strConn)
                Dim Dlt_ClassData As New SqlDataAdapter()

                Dim dlt_Class As String

                Dlt_ClassData.SelectCommand = New SqlCommand()
                Dlt_ClassData.SelectCommand.Connection = MyConnection
                Dlt_ClassData.SelectCommand.CommandText = "delete invoice_detail  where ID_ID ='" & strKeyName & "'"
                MyConnection.Open()
                dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
                MyConnection.Close()

                ShowMessage("Remove data ", MessageType.Success)

            End If

            strRet = BindData(datRespondent)

        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
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

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY FIM_ID ASC"

        tmpSQL = "select * from fee_item_master"
        strWhere = " where FIM_ID is not null "
        strWhere += " AND FIM_Year = '" & ddlYear_List.SelectedValue & "' and FIM_Type = '" & ddlType_List.SelectedValue & "'"


        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String
        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
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

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        If ddlYear.SelectedValue = Now.Year Then

            If Inv_Name.Text.Length > 0 Then

                If Std_Price.Text.Length > 0 Then

                    If ddlType.SelectedIndex > 0 Then
                        strSQL = "  INSERT INTO fee_item_master(FIM_Year, FIM_Item, FIM_Quantity, FIM_Price, FIM_Gender, FIM_Type, FIM_Remark)
                                    VALUES ('" & ddlYear.SelectedValue & "', '" & Inv_Name.Text & "', '1', '" & Std_Price.Text & "', '" & ddlGender.SelectedValue & "', '" & ddlType.SelectedValue & "', '" & Inv_Remark.Text & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        ShowMessage("Please select type of fee", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please enter price", MessageType.Error)
                End If
            Else
                ShowMessage("Please enter a valid invoice name", MessageType.Error)
            End If
        Else
            ShowMessage("Unable to select year " & ddlYear.SelectedValue, MessageType.Error)
        End If

        strRet = BindData(datRespondent)

    End Sub

    Private Sub Add_Student_ServerClick(sender As Object, e As EventArgs) Handles Add_Student.ServerClick
        Try
            Response.Redirect("admin_daftar_invois.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub payment_information_ServerClick(sender As Object, e As EventArgs) Handles payment_information.ServerClick
        Dim display_data As String = Request.QueryString("value")

        If display_data = "0" Then
            Response.Redirect("admin_daftar_yuran_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&value=1")
        Else
            Response.Redirect("admin_daftar_yuran_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&value=0")
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Protected Sub ddlYear_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_List.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ddlType_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType_List.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class