Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class parent_student_paymentInformation
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
                Year_List()
                Year_Load()
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Year_List()
        Try
            strSQL = "SELECT year from student_Level where std_ID = '" & Request.QueryString("std_ID") & "'"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            ddlyear.DataSource = ds
            ddlyear.DataTextField = "year"
            ddlyear.DataValueField = "year"
            ddlyear.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Year_Load()
        strSQL = "SELECT distinct year from student_Level where year ='" & Now.Year & "' and std_ID = '" & Request.QueryString("std_ID") & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlyear.SelectedValue = ""
            End If
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

            Dim id As String = Request.QueryString("std_ID")
            Dim gender As String = "Select student_Sex from student_info where std_ID = '" & id & "'"
            Dim get_gender As String = oCommon.getFieldValue(gender)

            If get_gender = "LELAKI" Or get_gender = "Lelaki" Or get_gender = "lelaki" Or get_gender = "Male" Or get_gender = "MALE" Or get_gender = "male" Then
                gvTable.Columns(3).Visible = False
            ElseIf get_gender = "PEREMPUAN" Or get_gender = " ThenPerempuan" Or get_gender = "perempuan" Or get_gender = "Female" Or get_gender = "FEMALE" Or get_gender = "female" Then
                gvTable.Columns(2).Visible = False
            End If

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
        Dim strOrderby As String = ""

        Dim id As String = Request.QueryString("std_ID")
        Dim gender As String = "Select student_Sex from student_info where std_ID = '" & id & "'"
        Dim get_gender As String = oCommon.getFieldValue(gender)

        If get_gender = "LELAKI" Or get_gender = "Lelaki" Or get_gender = "lelaki" Or get_gender = "Male" Or get_gender = "MALE" Or get_gender = "male" Then

            strOrderby = "order by payment_info.Std_Male ASC"

        ElseIf get_gender = "PEREMPUAN" Or get_gender = "Perempuan" Or get_gender = "perempuan" Or get_gender = "Female" Or get_gender = "FEMALE" Or get_gender = "female" Then

            strOrderby = "order by payment_info.Std_Female ASC"
        End If

        tmpSQL = "select payment_transaction.Transaction_ID, payment_info.Description, payment_info.Std_Male, payment_info.Std_Female, payment_transaction.Status from payment_transaction
                  left join payment_info on payment_transaction.Payment_ID = payment_info.Payment_ID"
        strWhere = " WHERE payment_transaction.std_ID = '" & id & "'"

        If ddlyear.SelectedValue > 0 Then
            strWhere = " AND payment_transaction.Year = '" & ddlyear.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

End Class