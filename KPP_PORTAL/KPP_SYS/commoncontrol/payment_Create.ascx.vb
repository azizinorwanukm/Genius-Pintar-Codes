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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim level As String = Request.QueryString("value")

                Year()
                Level_list()
                Payment_Type()

                load_page(level)
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page(ByVal data As String)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        '' get year
        strSQL = "select Parameter from setting where Parameter = '" & Now.Year + 1 & "' and Type = 'Year'"
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

        If data = "0" Then
            data = ""
        ElseIf data = "1" Then
            data = "Foundation 1"
        ElseIf data = "2" Then
            data = "Foundation 2"
        ElseIf data = "3" Then
            data = "Foundation 3"
        ElseIf data = "4" Then
            data = "Level 1"
        ElseIf data = "5" Then
            data = "Level 2"
        End If

        If data = "Foundation 1" Or data = "Foundation 2" Or data = "Foundation 3" Or data = "Level 1" Or data = "Level 2" Then

            '' get student level
            strSQL = "select Parameter from setting where Parameter = '" & data & "' and Type = 'Level'"
            Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

            Dim dt As DataSet = New DataSet
            sqlDB.Fill(dt, "AnyTable")

            Dim Rowing As Integer = 0
            Dim Counting As Integer = 1
            Dim Tabling As DataTable = New DataTable
            Tabling = dt.Tables(0)
            If Tabling.Rows.Count > 0 Then
                If Not IsDBNull(dt.Tables(0).Rows(0).Item("Parameter")) Then
                    ddlLevel.SelectedValue = dt.Tables(0).Rows(0).Item("Parameter")
                    ddlLevel_List.SelectedValue = dt.Tables(0).Rows(0).Item("Parameter")
                Else
                    ddlLevel.SelectedValue = ""
                    ddlLevel_List.SelectedValue = ""
                End If
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

    Private Sub Level_list()
        strSQL = "select Parameter from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Student Level", String.Empty))
            ddlLevel.SelectedIndex = 0

            ddlLevel_List.DataSource = ds
            ddlLevel_List.DataTextField = "Parameter"
            ddlLevel_List.DataValueField = "Parameter"
            ddlLevel_List.DataBind()
            ddlLevel_List.Items.Insert(0, New ListItem("Select Student Level", String.Empty))
            ddlLevel_List.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Payment_Type()
        strSQL = "select Parameter from setting where Type = 'Payment_Type'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlType.DataSource = ds
            ddlType.DataTextField = "Parameter"
            ddlType.DataValueField = "Parameter"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Select Type", String.Empty))
            ddlType.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Type_Of_Payment()
        strSQL = "select Parameter from setting where Type = 'Type_Of_Payment'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPaymentType.DataSource = ds
            ddlPaymentType.DataTextField = "Parameter"
            ddlPaymentType.DataValueField = "Parameter"
            ddlPaymentType.DataBind()
            ddlPaymentType.Items.Insert(0, New ListItem("Select Payment Type", String.Empty))
            ddlPaymentType.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlLevel_List_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel_List.SelectedIndexChanged
        Try
            Dim data As String = ddlLevel_List.SelectedValue

            If data = "Foundation 1" Then
                data = "1"
            ElseIf data = "Foundation 2" Then
                data = "2"
            ElseIf data = "Foundation 3" Then
                data = "3"
            ElseIf data = "Level 1" Then
                data = "4"
            ElseIf data = "Level 2" Then
                data = "5"
            End If

            Response.Redirect("admin_daftar_yuran_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&value=" + data)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete payment_info where Payment_ID='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

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
        Dim strOrderby As String = " ORDER BY Payment_ID ASC"

        tmpSQL = "select * from payment_info"
        strWhere = " where Payment_ID is not null "

        If ddlYear_List.SelectedIndex > 0 Then
            strWhere += " AND Year = '" & ddlYear_List.SelectedValue & "'"
        End If

        If ddlLevel_List.SelectedIndex > 0 Then
            strWhere += " AND student_Level = '" & ddlLevel_List.SelectedValue & "'"
        End If

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

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")

        Try
            If ddlType.SelectedValue = "Student Fee" Then
                hiddenAccess.Value = "1"
            End If

            Type_Of_Payment()

        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim errorCount As String = ""

        If Description_Payment.Text <> "" And Not IsNothing(Description_Payment.Text) And Regex.IsMatch(oCommon.FixSingleQuotes(Description_Payment.Text), "^[A-Za-z0-9 ]+$") Then

            If Std_Female.Text <> "" Or IsNumeric(Std_Female.Text) Then

                If Std_Male.Text <> "" Or IsNumeric(Std_Male.Text) Then

                    If ddlYear.SelectedIndex > 0 Then

                        If ddlLevel.SelectedIndex > 0 Then

                            If ddlType.SelectedIndex > 0 Then

                                Dim get_total_male As String = "select distinct Total_Male from payment_info where Year = '" & ddlYear_List.SelectedValue & "' and student_Level = '" & ddlLevel_List.SelectedValue & "' and Payment_Type = '" & ddlType.SelectedValue & "'"
                                Dim data_ttl_male As String = getFieldValue(get_total_male, strConn)

                                Dim get_total_female As String = "select distinct Total_Female from payment_info where Year = '" & ddlYear_List.SelectedValue & "' and student_Level = '" & ddlLevel_List.SelectedValue & "' and Payment_Type = '" & ddlType.SelectedValue & "'"
                                Dim data_ttl_female As String = getFieldValue(get_total_female, strConn)

                                If data_ttl_male = "" And data_ttl_female = "" Then

                                    Dim data_male As Decimal = 0.0
                                    Dim data_female As Decimal = 0.0

                                    data_male = data_male + Std_Male.Text
                                    data_female = data_female + Std_Female.Text

                                    Using STDDATA As New SqlCommand("INSERT INTO payment_info (Description,Std_Male,Std_Female,Total_Male,Total_Female,Payment_Type,Year,student_Level,Note) values 
                                                               ('" & Description_Payment.Text & "','" & Std_Male.Text & "','" & Std_Female.Text & "','" & data_male & "','" & data_female & "','" & ddlType.SelectedValue & "',
                                                                '" & ddlYear.SelectedValue & "','" & ddlLevel.SelectedValue & "','" & Note.Text & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()
                                        If i <> 0 Then
                                            errorCount = "1"
                                        Else
                                            errorCount = "-1"
                                        End If
                                    End Using

                                Else

                                    Dim sum_ttl_male As String = "select MAX(Total_Male) from payment_info where Year = '" & ddlYear_List.SelectedValue & "' and student_Level = '" & ddlLevel_List.SelectedValue & "' and Payment_Type = '" & ddlType.SelectedValue & "'"
                                    Dim data_sum_male As String = getFieldValue(sum_ttl_male, strConn)

                                    Dim sum_ttl_female As String = "select MAX(Total_Female) from payment_info where Year = '" & ddlYear_List.SelectedValue & "' and student_Level = '" & ddlLevel_List.SelectedValue & "' and Payment_Type = '" & ddlType.SelectedValue & "'"
                                    Dim data_sum_female As String = getFieldValue(sum_ttl_female, strConn)

                                    Dim data_male As Decimal = 0.0
                                    Dim data_female As Decimal = 0.0

                                    data_male = Decimal.Parse(data_sum_male) + Std_Male.Text
                                    data_female = Decimal.Parse(data_sum_female) + Std_Female.Text

                                    Using STDDATA As New SqlCommand("INSERT INTO payment_info (Description,Std_Male,Std_Female,Total_Male,Total_Female,Payment_Type,Year,student_Level,Note) values 
                                                               ('" & Description_Payment.Text & "','" & Std_Male.Text & "','" & Std_Female.Text & "','" & data_male & "','" & data_female & "','" & ddlType.SelectedValue & "',
                                                                '" & ddlYear.SelectedValue & "','" & ddlLevel.SelectedValue & "','" & Note.Text & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()
                                        If i <> 0 Then
                                            errorCount = "1"
                                        Else
                                            errorCount = "-1"
                                        End If
                                    End Using

                                End If

                            Else

                            End If
                        Else
                            errorCount = "2"
                        End If
                    Else
                        errorCount = "3"
                    End If
                Else
                    errorCount = "4"
                End If
            Else
                errorCount = "5"
            End If
        Else
            errorCount = "6"
        End If

        Response.Redirect("admin_daftar_yuran_baru.aspx?result=" + errorCount + "&admin_ID=" + Request.QueryString("admin_ID") + "&value=" + ddlLevel.SelectedValue)
    End Sub


End Class