Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class parent_imagePayment
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

                student_year()

                strRet = CallingDataPayment(datRespondentPayment)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub student_year()
        strSQL = "select distinct II_Year from invoice_info where std_ID = '" & Session("Std_ID") & "' order by II_Year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "II_Year"
            ddlyear.DataValueField = "II_Year"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlyear.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            strRet = CallingDataPayment(datRespondentPayment)
        Catch ex As Exception
        End Try
    End Sub

    Private Function CallingDataPayment(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getDataSQLPayment, strConn)
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

    Private Function getDataSQLPayment() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        strOrderby = " Order By D.II_Year, D.II_InvNo Asc"

        tmpSQL = "  Select distinct D.II_ID, D.II_InvNo, D.II_Year, D.II_PaidSetting, D.II_FullAmount, D.II_Outstanding, D.II_Published, D.II_Published as Publish_Status, D.II_Status, D.II_Status as Status from student_info A
                    Left Join course B on A.std_ID = B.std_ID
                    Left Join class_info C on B.class_ID = C.class_ID
                    Left Join invoice_info D on A.std_ID = D.std_ID"

        strWhere = " WHERE (A.student_Status = 'Access' or A.student_Status = 'Graduate') and A.student_ID like '%M%' and A.student_Campus = 'PGPN'"
        strWhere += " and C.class_type = 'Compulsory' and A.std_ID = '" & Session("Std_ID") & "'"
        strWhere += " and B.year = '" & ddlyear.SelectedValue & "' and C.class_year = '" & ddlyear.SelectedValue & "' and D.II_Year = '" & ddlyear.SelectedValue & "'"

        getDataSQLPayment = tmpSQL & strWhere & strOrderby

        Return getDataSQLPayment
    End Function

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label
        Dim Status As Label

        For row = 0 To datRespondentPayment.Rows.Count - 1 Step row + 1
            lblDay = datRespondentPayment.Rows(row).Cells(7).FindControl("Publish_Status")
            Status = datRespondentPayment.Rows(row).Cells(9).FindControl("Status")

            If lblDay.Text <> "Yes" Then
                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Red
                lblDay.ForeColor = Drawing.Color.Red
                lblDay.CssClass = "lblAbsent"
            End If

            If lblDay.Text = "Yes" Then
                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Green
                lblDay.ForeColor = Drawing.Color.Green
                lblDay.CssClass = "lblAttend"
            End If

            If Status.Text <> "Paid" Then
                Status.Text = "OO"
                Status.BackColor = Drawing.Color.Red
                Status.ForeColor = Drawing.Color.Red
                Status.CssClass = "lblAbsent"
            End If

            If Status.Text = "Paid" Then
                Status.Text = "OO"
                Status.BackColor = Drawing.Color.Green
                Status.ForeColor = Drawing.Color.Green
                Status.CssClass = "lblAttend"
            End If
        Next
    End Sub

    Private Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondentPayment.RowDeleting
        Dim strKeyName As String = datRespondentPayment.DataKeys(e.RowIndex).Value.ToString

        Try
            Session("II_ID") = strKeyName
            Response.Redirect("penjaga_bayaran_detail.aspx")

        Catch ex As Exception
        End Try
    End Sub

End Class
