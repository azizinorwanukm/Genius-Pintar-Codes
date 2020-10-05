Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Partial Public Class admin_pusatujian_remarks_update
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PusatUjian_load()

        End If

    End Sub

    Private Sub PusatUjian_load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''--display PusatUjian profile
        strSQL = "SELECT * FROM PusatUjian WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("Komen")) Then
                    txtRemarks.Text = MyTable.Rows(nRows).Item("Komen").ToString
                Else
                    txtRemarks.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        '--update komen table pusatujian
        strSQL = "UPDATE PusatUjian WITH (UPDLOCK) SET Komen='" & oCommon.FixSingleQuotes(txtRemarks.Text) & "' WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Successfully update Pusat Ujian Remarks."
        Else
            lblMsg.Text = "Error: " & strRet
        End If

    End Sub

End Class