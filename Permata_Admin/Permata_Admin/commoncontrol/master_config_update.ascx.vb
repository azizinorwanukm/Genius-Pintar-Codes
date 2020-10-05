Imports System.Data.SqlClient

Public Class master_config_update
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

        strSQL = "SELECT * FROM master_Config WHERE configID=" & Request.QueryString("configID")
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                ''--parent info
                If Not IsDBNull(MyTable.Rows(nRows).Item("configCode")) Then
                    txtconfigCode.Text = MyTable.Rows(nRows).Item("configCode").ToString
                Else
                    txtconfigCode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("configString")) Then
                    txtconfigString.Text = MyTable.Rows(nRows).Item("configString").ToString
                Else
                    txtconfigString.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("configDesc")) Then
                    txtconfigDesc.Text = MyTable.Rows(nRows).Item("configDesc").ToString
                Else
                    txtconfigDesc.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        strSQL = "UPDATE master_Config SET configCode='" & oCommon.FixSingleQuotes(txtconfigCode.Text) & "',configString='" & oCommon.FixSingleQuotes(txtconfigString.Text) & "',configDesc='" & oCommon.FixSingleQuotes(txtconfigDesc.Text) & "' WHERE configID=" & Request.QueryString("configID")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini Sistem Konfigurasi."
        Else
            lblMsg.Text = "GAGAL mengemaskini Sistem Konfigurasi." & strRet
        End If

    End Sub

    Private Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.master.config.list.aspx")

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        strSQL = "DELETE FROM master_Config WHERE configID=" & Request.QueryString("configID")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA menghapuskan reokd tersebut."
        End If

    End Sub
End Class