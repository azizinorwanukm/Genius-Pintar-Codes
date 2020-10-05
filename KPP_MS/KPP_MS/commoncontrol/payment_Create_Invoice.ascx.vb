Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class payment_Create_Invoice
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Year()
                student_Level()
                load_page()

                strRet = BindData(datRespondent)

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
                ddlYear_List.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
                ddl_Year.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlYear_List.SelectedValue = ""
                ddl_Year.SelectedValue = ""
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

            ddlYear_List.DataSource = ds
            ddlYear_List.DataTextField = "Parameter"
            ddlYear_List.DataValueField = "Parameter"
            ddlYear_List.DataBind()

            ddl_Year.DataSource = ds
            ddl_Year.DataTextField = "Parameter"
            ddl_Year.DataValueField = "Parameter"
            ddl_Year.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Level.DataSource = ds
            ddl_Level.DataTextField = "Parameter"
            ddl_Level.DataValueField = "Parameter"
            ddl_Level.DataBind()
            ddl_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_Level.Items.Insert(1, New ListItem("All", "All"))
            ddl_Level.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_info()
        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & ddl_Year.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddl_Level.SelectedValue & "' order by class_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Class.DataSource = ds
            ddl_Class.DataTextField = "class_Name"
            ddl_Class.DataValueField = "class_ID"
            ddl_Class.DataBind()
            ddl_Class.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddl_Class.Items.Insert(1, New ListItem("All", "All"))
            ddl_Class.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlYear_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_List.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            If ddl_Level.SelectedValue = "All" Then
                strRet = FillData(gridRespondent)
            Else
                class_info()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Class.SelectedIndexChanged
        Try
            strRet = FillData(gridRespondent)
        Catch ex As Exception
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

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY FIM_ID ASC"

        tmpSQL = "select * from fee_item_master"
        strWhere = " where FIM_ID is not null "
        strWhere += " AND FIM_Year = '" & ddlYear_List.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Function FillData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(findSQL, strConn)
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

    Private Function findSQL() As String

        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by class_info.class_Level, class_info.class_Name, student_info.student_Name ASC"

        tmpSQL = "select distinct student_info.std_ID, course.year, student_info.student_Name, student_info.student_ID, student_info.student_Mykad, class_info.class_Name, class_info.class_Level from student_info
                  left join course on student_info.std_ID = course.std_ID left join class_info on course.class_ID = class_info.class_ID"

        strWhere = " where course.std_ID is not null
                     and course.year = '" & ddl_Year.SelectedValue & "'
                     and class_info.class_year = '" & ddl_Year.SelectedValue & "'
                     and class_info.class_type = 'Compulsory'
                     and student_info.student_Status = 'Access'"

        If ddl_Level.SelectedValue <> "All" Then

            strWhere += " AND class_info.class_Level = '" & ddl_Level.SelectedValue & "'"

            If ddl_Class.SelectedValue <> "All" Then

                strWhere += " AND class_info.class_ID = '" & ddl_Class.SelectedValue & "'"

            End If
        End If

        findSQL = tmpSQL & strWhere & strOrderby

        Return findSQL
    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        ''student table gridview
        For i As Integer = 0 To gridRespondent.Rows.Count - 1

            Dim chkUpdate_Std As CheckBox = CType(gridRespondent.Rows(i).Cells(5).FindControl("chkSelect_Std"), CheckBox)

            If Not chkUpdate_Std Is Nothing Then
                ''get std_ID from gridview student list data
                Dim strStdID As String = gridRespondent.DataKeys(i).Value.ToString

                If chkUpdate_Std.Checked = True Then

                    'count invoice information data
                    Dim find_count As String = "select count(II_ID) from invoice_info where II_Year = '" & ddl_Year.SelectedValue & "'"
                    Dim get_count As String = oCommon.getFieldValue(find_count)

                    'generate reference number / order number
                    'determine the patern of numbering xxxxx
                    get_count = get_count + 1
                    Dim reference_number As String = String.Concat(ddl_Year.SelectedValue, get_count.PadLeft(5, "0"))

                    ''get current date
                    Dim current_date As String = Date.Now().ToString("dd MMM yyyy")

                    ''Insert into invoice info DB
                    Using STDDATA As New SqlCommand("Insert into invoice_info (II_Date, II_Year, II_InvNo, II_Status, Std_ID, II_PaymentType, II_Published) values 
                                                    ('" & current_date & "', '" & ddl_Year.SelectedValue & "', '" & reference_number & "', 'Pending', '" & strStdID & "', 'Full Payment', 'No')", objConn)
                        objConn.Open()
                        Dim confirm = STDDATA.ExecuteNonQuery()
                        objConn.Close()
                    End Using

                    ''get invoice info id from invoice_info DB
                    Dim find_II_ID As String = "select II_ID from invoice_info where II_InvNo = '" & reference_number & "' and II_Year = '" & ddl_Year.SelectedValue & "' and Std_ID = '" & strStdID & "'"
                    Dim get_II_ID As String = oCommon.getFieldValue(find_II_ID)

                    ''invoice item table gridview
                    For j As Integer = 0 To datRespondent.Rows.Count - 1

                        Dim chkUpdate As CheckBox = CType(datRespondent.Rows(j).Cells(5).FindControl("chkSelect"), CheckBox)
                        If Not chkUpdate Is Nothing Then
                            ''get fee item master id from gridview invoice list data
                            Dim strKeyID As String = datRespondent.DataKeys(j).Value.ToString

                            If chkUpdate.Checked = True Then

                                ''get invoice quantity from Fee item master DB
                                Dim find_IT_Quantity As String = "select FIM_Quantity from fee_item_master where FIM_ID = '" & strKeyID & "'"
                                Dim get_IT_Quantity As String = oCommon.getFieldValue(find_IT_Quantity)

                                ''get invoice price from Fee item master DB
                                Dim find_IT_Price As String = "select FIM_Price from fee_item_master where FIM_ID = '" & strKeyID & "'"
                                Dim get_IT_Price As String = oCommon.getFieldValue(find_IT_Price)

                                ''get invoice price from Fee item master DB
                                Dim find_IT_Item As String = "select FIM_Item from fee_item_master where FIM_ID = '" & strKeyID & "'"
                                Dim get_IT_Item As String = oCommon.getFieldValue(find_IT_Item)

                                ''Insert into invoice info DB
                                Using STDDATA As New SqlCommand("Insert into invoice_item (II_ID, FIM_ID, IT_Year, IT_Status, IT_Quantity, IT_Price, IT_Item) values 
                                                                ('" & get_II_ID & "','" & strKeyID & "','" & ddlYear_List.SelectedValue & "','Pending','" & get_IT_Quantity & "','" & get_IT_Price & "','" & get_IT_Item & "')", objConn)
                                    objConn.Open()
                                    Dim confirm = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        errorCount = 1
                                    Else
                                        errorCount = 2
                                    End If

                                End Using

                            End If
                        End If
                    Next

                    ''Calculate fullamount and outstanding value on invoice info DB
                    Dim find_totalamount As String = "select SUM(IT_Price) from invoice_item where II_ID = '" & get_II_ID & "'"
                    Dim get_totalamount As Decimal = Decimal.Parse(oCommon.getFieldValue(find_totalamount))

                    Dim find_outstanding As String = "select SUM(IT_Price) from invoice_item where II_ID = '" & get_II_ID & "' and IT_Status = 'Pending'"
                    Dim get_outstanding As Decimal = Decimal.Parse(oCommon.getFieldValue(find_outstanding))

                    strSQL = "UPDATE invoice_info set II_FullAmount = '" & get_totalamount & "', II_Outstanding = '" & get_outstanding & "' where II_ID = '" & get_II_ID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            End If

        Next

        If strRet = "0" Then
            ShowMessage("Generating Invoice", MessageType.Success)
        Else
            ShowMessage("Generating Invoice", MessageType.Error)
        End If

    End Sub

    Private Sub BtnBack_ServerClick(sender As Object, e As EventArgs) Handles BtnBack.ServerClick
        Try
            Response.Redirect("admin_daftar_yuran_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&value=0")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub gridRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gridRespondent.RowEditing
        Dim strKeyID As String = gridRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_transaksi_yuran_gambar.aspx?std_ID=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID") + "&back=0")
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class