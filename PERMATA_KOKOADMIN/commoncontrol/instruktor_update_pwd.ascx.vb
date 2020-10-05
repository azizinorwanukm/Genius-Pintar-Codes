Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class instruktor_update_pwd
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
                koko_instruktor_load()

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub koko_instruktor_load()
        strSQL = "SELECT * FROM koko_instruktor WHERE InstruktorID='" & Request.QueryString("instruktorid") & "'"
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
                    lblPwdOrg.Text = oDes.DecryptData(strPwd)
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub


    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If ValidateForm() = False Then
            Exit Sub
        End If

        If isValid() = False Then
            lblMsg.Text = "Salah kata laluan yang asal."
            Exit Sub
        End If

        strSQL = "UPDATE koko_instruktor SET Pwd='" & oDes.EncryptData(txtPwdNew.Text) & "' WHERE InstruktorID='" & Request.QueryString("instruktorid") & "' AND Tahun='" & Request.QueryString("tahun") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini Kata Laluan anda."
        Else
            lblMsg.Text = "GAGAL mengemaskini Kata Laluan anda."
        End If

    End Sub

    Private Function isValid() As Boolean
        strSQL = "SELECT LoginID FROM koko_instruktor WHERE InstruktorID='" & Request.QueryString("instruktorid") & "' AND Tahun='" & Request.QueryString("tahun") & "' AND Pwd='" & oDes.EncryptData(txtPwd.Text) & "'"
        If oCommon.isExist(strSQL) = True Then
            Return True
        End If

        Return False
    End Function

    Private Function ValidateForm() As Boolean
        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtPwd.Focus()
            Return False
        End If

        If txtPwdNew.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtPwdNew.Focus()
            Return False
        End If

        If txtPwdVerify.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtPwdVerify.Focus()
            Return False
        End If

        If Not txtPwdNew.Text = txtPwdVerify.Text Then
            lblMsg.Text = "Kata Laluan Baru dan Pastikan Kata Laluan Baru tidak sama."
            txtPwd.Focus()
            Return False
        End If

        Return True

    End Function

End Class