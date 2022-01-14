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
                loadpage()
                strRet = BindData(datRespondent)
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

    Private Sub Btnkemaskini_ServerClick(sender As Object, e As EventArgs) Handles Btnkemaskini.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    strSQL = "UPDATE payment_transaction set Status ='Paid'
                              WHERE Transaction_ID ='" & strKey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If

                End If
                '--execute SQL
            End If
        Next

        If errorCount > 0 Then
            Response.Redirect("admin_transaksi_yuran_gambar.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID") + "&std_ID=" + Request.QueryString("std_ID") + "&year=" + Request.QueryString("year"))
        Else
            Response.Redirect("admin_transaksi_yuran_gambar.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID") + "&std_ID=" + Request.QueryString("std_ID") + "&year=" + Request.QueryString("year"))
        End If
    End Sub

    Private Sub Btnpending_ServerClick(sender As Object, e As EventArgs) Handles Btnpending.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    strSQL = "UPDATE payment_transaction set Status ='Pending'
                              WHERE Transaction_ID ='" & strKey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If

                End If
                '--execute SQL
            End If
        Next

        If errorCount > 0 Then
            Response.Redirect("admin_transaksi_yuran_gambar.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID") + "&std_ID=" + Request.QueryString("std_ID") + "&year=" + Request.QueryString("year"))
        Else
            Response.Redirect("admin_transaksi_yuran_gambar.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID") + "&std_ID=" + Request.QueryString("std_ID") + "&year=" + Request.QueryString("year"))
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
                gvTable.Columns(4).Visible = False
            ElseIf get_gender = "PEREMPUAN" Or get_gender = " ThenPerempuan" Or get_gender = "perempuan" Or get_gender = "Female" Or get_gender = "FEMALE" Or get_gender = "female" Then
                gvTable.Columns(3).Visible = False
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
        strWhere = " WHERE payment_transaction.std_ID = '" & id & "' and payment_transaction.Year = '" & Request.QueryString("year") & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

End Class