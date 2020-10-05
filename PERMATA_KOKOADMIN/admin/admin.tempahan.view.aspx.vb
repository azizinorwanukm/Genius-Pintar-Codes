Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class admin_tempahan_view
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")
        Try

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.kemudahan.list.aspx?admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        strSQL = "DELETE koko_tempahandetail WHERE TempahanID=" & Request.QueryString("tempahanid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Rekod berjaya DIHAPUSKAN."
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

    Private Sub koko_tempahan_update(ByVal strQuery As String)
        Dim strKemudahanID As String = ""
        Dim strBookingDate As String = ""

        strSQL = "SELECT KemudahanID FROM koko_tempahandetail WHERE TempahanID=" & Request.QueryString("tempahanid")
        strKemudahanID = oCommon.getFieldValue(strSQL)
        strSQL = "SELECT BookingDate FROM koko_tempahandetail WHERE TempahanID=" & Request.QueryString("tempahanid")
        strBookingDate = oCommon.getFieldValue(strSQL)


        Dim strcolumnName As String = ""
        Dim strcolumnStringValue As String = ""

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1

                strcolumnName = dt.Columns(k).ColumnName.ToString
                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                Else
                    strcolumnStringValue = columnValue.ToString()
                End If

                '--debug
                'Response.Write("columnName:" & strcolumnName & ":columnStringValue:" & strcolumnStringValue)
                If strcolumnStringValue = "True" Then
                    strSQL = "UPDATE koko_tempahan SET " & strcolumnName & "='false' WHERE KemudahanID=" & strKemudahanID & " AND BookingDate='" & strBookingDate & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            Next
        Next

    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

End Class