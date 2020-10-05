Imports System.Data.SqlClient

Public Class koko_user_update
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
                koko_user_load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_user_load()
        strSQL = "SELECT * FROM koko_user WHERE LoginID='" & CType(Session.Item("koko_loginid"), String) & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kokouserid")) Then
                    lblkokouserid.Text = ds.Tables(0).Rows(0).Item("kokouserid")
                Else
                    lblkokouserid.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    txtLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    txtLoginID.Text = ""
                End If
                lblLoginIDOrg.Text = txtLoginID.Text

                Dim strPwd As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    strPwd = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    strPwd = ""
                End If
                If Not strPwd.Length = 0 Then
                    txtPwd.Text = oDes.DecryptData(strPwd)
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    txtFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    txtFullname.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'check form validation. if failed exit
        If ValidateForm() = False Then
            Exit Sub
        End If

        'UPDATE
        strSQL = "UPDATE koko_user SET LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "',Pwd='" & oDes.EncryptData(txtPwd.Text) & "',Fullname='" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "' WHERE kokouserid=" & lblkokouserid.Text
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Kemaskini berjaya!"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtLoginID.Focus()
            Return False
        End If

        '--change made. check duplicate
        If Not lblLoginIDOrg.Text = txtLoginID.Text Then
            strSQL = "SELECT LoginID FROM koko_user WHERE LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "LoginID sudah digunakan."
                txtLoginID.Focus()
                Return False
            End If
        End If

        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtPwd.Focus()
            Return False
        End If

        Return True
    End Function
   
End Class