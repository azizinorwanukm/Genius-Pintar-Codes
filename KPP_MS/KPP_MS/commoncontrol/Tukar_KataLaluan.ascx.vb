Imports System.Data.SqlClient

Public Class Tukar_KataLaluan
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim Check As String = "select login_ID from staff_Login where staff_Password = '" & txtloginPassword.Text & "' and staff_Login = '" & txtloginUsername.Text & "'"
        Dim userID As String = getFieldValue(Check, strConn)

        If userID.Length > 0 Then
            strSQL = "UPDATE staff_Login SET staff_Password ='" & txtnewPassword.Text & "' WHERE login_ID ='" & userID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                Response.Redirect("Tukar_KataLaluan.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
            Else
                Response.Redirect("Tukar_KataLaluan.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
            End If
        Else
            Response.Redirect("Tukar_KataLaluan.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
        End If


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
End Class