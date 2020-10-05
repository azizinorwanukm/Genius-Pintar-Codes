Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class pengarah_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_pengarah_load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_pengarah_load()
        strSQL = "SELECT * FROM koko_pengarah WHERE PengarahID='" & Request.QueryString("pengarahid") & "'"
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    lblTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    lblTahun.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    lblFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    lblFullname.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    lblMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    lblMYKAD.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContactNo")) Then
                    lblContactNo.Text = ds.Tables(0).Rows(0).Item("ContactNo")
                Else
                    lblContactNo.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Email")) Then
                    lblEmail.Text = ds.Tables(0).Rows(0).Item("Email")
                Else
                    lblEmail.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Address1")) Then
                    lblAddress1.Text = ds.Tables(0).Rows(0).Item("Address1")
                Else
                    lblAddress1.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Address2")) Then
                    lblAddress2.Text = ds.Tables(0).Rows(0).Item("Address2")
                Else
                    lblAddress2.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Postcode")) Then
                    lblPostcode.Text = ds.Tables(0).Rows(0).Item("Postcode")
                Else
                    lblPostcode.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("City")) Then
                    lblCity.Text = ds.Tables(0).Rows(0).Item("City")
                Else
                    lblCity.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("State")) Then
                    lblState.Text = ds.Tables(0).Rows(0).Item("State")
                Else
                    lblState.Text = ""
                End If

                '--LOGIN
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    lblLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    lblLoginID.Text = ""
                End If

                Dim strPwd As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    strPwd = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    strPwd = ""
                End If
                If Not strPwd.Length = 0 Then
                    lblPwd.Text = oDes.DecryptData(strPwd)
                End If

                '--KEWANGAN
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("BankName")) Then
                    lblBankName.Text = ds.Tables(0).Rows(0).Item("BankName")
                Else
                    lblBankName.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AcctNo")) Then
                    lblAcctNo.Text = ds.Tables(0).Rows(0).Item("AcctNo")
                Else
                    lblAcctNo.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class