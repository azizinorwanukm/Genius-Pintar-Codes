Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class master_PPCSStatus_update1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")
        Try
            If Not IsPostBack Then
                master_Config_view()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub master_Config_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM master_PPCSStatus WHERE ppcsstatusid=" & Request.QueryString("ppcsstatusid")
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                ''--parent info
                If Not IsDBNull(MyTable.Rows(nRows).Item("PPCSStatus")) Then
                    PPCSStatus.Text = MyTable.Rows(nRows).Item("PPCSStatus").ToString
                Else
                    PPCSStatus.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & Request.Cookies("ppcs_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        strSQL = "UPDATE master_PPCSStatus SET PPCSStatus='" & oCommon.FixSingleQuotes(PPCSStatus.Text.ToUpper) & "' WHERE ppcsstatusid=" & Request.QueryString("ppcsstatusid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini PPCS Status."
        Else
            lblMsg.Text = "GAGAL mengemaskini PPCS Status." & strRet
        End If

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        strSQL = "DELETE master_PPCSStatus WHERE ppcsstatusid=" & Request.QueryString("ppcsstatusid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya menghapuskan PPCS Status."
        Else
            lblMsg.Text = "GAGAL menghapuskan PPCS Status." & strRet
        End If
    End Sub

    Protected Sub lnkBrowse_Click(sender As Object, e As EventArgs) Handles lnkBrowse.Click
        Response.Redirect("ppcs.ppcsstatus.list.aspx")

    End Sub
End Class