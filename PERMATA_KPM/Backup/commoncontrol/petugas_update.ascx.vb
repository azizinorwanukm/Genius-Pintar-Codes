Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class petugas_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PusatUjian_Petugas_view()
        End If

    End Sub

    Private Sub PusatUjian_Petugas_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''--display PusatUjian profile
        strSQL = "SELECT * FROM PusatUjian_Petugas WHERE PetugasID=" & Request.QueryString("petugasid")
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("UserType")) Then
                    selUserType.Value = MyTable.Rows(nRows).Item("UserType").ToString
                Else
                    selUserType.Value = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("MYKAD")) Then
                    txtMYKAD.Text = MyTable.Rows(nRows).Item("MYKAD").ToString
                Else
                    txtMYKAD.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Fullname")) Then
                    txtFullname.Text = MyTable.Rows(nRows).Item("Fullname").ToString
                Else
                    txtFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("ContactNo")) Then
                    txtContactNo.Text = MyTable.Rows(nRows).Item("ContactNo").ToString
                Else
                    txtContactNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Email")) Then
                    txtEmail.Text = MyTable.Rows(nRows).Item("Email").ToString
                Else
                    txtEmail.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("BankName")) Then
                    txtBankName.Text = MyTable.Rows(nRows).Item("BankName").ToString
                Else
                    txtBankName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("AccountNo")) Then
                    txtAccountNo.Text = MyTable.Rows(nRows).Item("AccountNo").ToString
                Else
                    txtAccountNo.Text = ""
                End If

            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        strSQL = "UPDATE PusatUjian_Petugas SET UserType='" & selUserType.Value & "',Fullname='" & oCommon.FixSingleQuotes(txtFullname.Text) & "',ContactNo='" & oCommon.FixSingleQuotes(txtContactNo.Text) & "',Email='" & oCommon.FixSingleQuotes(txtEmail.Text) & "',MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "',BankName='" & oCommon.FixSingleQuotes(txtBankName.Text) & "',AccountNo='" & oCommon.FixSingleQuotes(txtAccountNo.Text) & "' WHERE PetugasID=" & Request.QueryString("petugasid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini maklumat Petugas."
        Else
            lblMsg.Text = "Gagal mengemaskini maklumat Petugas."
        End If

    End Sub
End Class