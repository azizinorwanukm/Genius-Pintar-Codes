Imports System.Data.SqlClient

Public Class pengarah_update
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
                '--refresh
                ClearScreen()
                setAccess()

                koko_state_list()
                ddlState.SelectedValue = ""

                '--selected year
                txtTahun.Text = Request.QueryString("tahun")

                koko_pengarah_load()
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

    Private Sub setAccess()
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                txtLoginID.Enabled = True
                lnkList.Visible = True

            Case Else
                txtLoginID.Enabled = False
                lnkList.Visible = False

        End Select

    End Sub

    Private Sub koko_state_list()
        strSQL = "SELECT State FROM koko_state ORDER BY State"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "State"
            ddlState.DataValueField = "State"
            ddlState.DataBind()

            ddlState.Items.Add(New ListItem("PILIH", ""))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtFullname.Text = ""
        txtMYKAD.Text = ""
        txtContactNo.Text = ""
        txtEmail.Text = ""

        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtPostcode.Text = ""
        txtCity.Text = ""

        txtLoginID.Text = ""
        txtPwd.Text = ""

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
                    txtTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    txtTahun.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    txtFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    txtFullname.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    txtMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    txtMYKAD.Text = ""
                End If
                lblMYKADOrg.Text = txtMYKAD.Text

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContactNo")) Then
                    txtContactNo.Text = ds.Tables(0).Rows(0).Item("ContactNo")
                Else
                    txtContactNo.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Email")) Then
                    txtEmail.Text = ds.Tables(0).Rows(0).Item("Email")
                Else
                    txtEmail.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Address1")) Then
                    txtAddress1.Text = ds.Tables(0).Rows(0).Item("Address1")
                Else
                    txtAddress1.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Address2")) Then
                    txtAddress2.Text = ds.Tables(0).Rows(0).Item("Address2")
                Else
                    txtAddress2.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Postcode")) Then
                    txtPostcode.Text = ds.Tables(0).Rows(0).Item("Postcode")
                Else
                    txtPostcode.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("City")) Then
                    txtCity.Text = ds.Tables(0).Rows(0).Item("City")
                Else
                    txtCity.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("State")) Then
                    ddlState.Text = ds.Tables(0).Rows(0).Item("State")
                Else
                    ddlState.Text = ""
                End If

                '--LOGIN
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    txtLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    txtLoginID.Text = ""
                End If
                Dim strPwd As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    strPwd = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    strPwd = ""
                End If
                If Not strPwd.Length = 0 Then
                    txtPwd.Text = oDes.DecryptData(strPwd)
                End If

                '--KEWANGAN
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("BankName")) Then
                    txtBankName.Text = ds.Tables(0).Rows(0).Item("BankName")
                Else
                    txtBankName.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AcctNo")) Then
                    txtAcctNo.Text = ds.Tables(0).Rows(0).Item("AcctNo")
                Else
                    txtAcctNo.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtFullname.Focus()
            Return False
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtMYKAD.Focus()
            Return False
        End If

        If Not lblMYKADOrg.Text = txtMYKAD.Text Then
            ''--check if already exist
            strSQL = "SELECT MYKAD FROM koko_pengarah WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "MYKAD Telah digunakan."
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.pengarah.list.aspx?pengarah_ID=" & Request.QueryString("pengarah_ID"))

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'check form validation. if failed exit
        If ValidateForm() = False Then
            Exit Sub
        End If

        'UPDATE
        strSQL = "UPDATE koko_pengarah SET Fullname='" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "',ContactNo='" & oCommon.FixSingleQuotes(txtContactNo.Text.ToUpper) & "',Email='" & oCommon.FixSingleQuotes(txtEmail.Text) & "',MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text.ToUpper) & "',Address1='" & oCommon.FixSingleQuotes(txtAddress1.Text.ToUpper) & "',Address2='" & oCommon.FixSingleQuotes(txtAddress2.Text.ToUpper) & "',PostCode='" & oCommon.FixSingleQuotes(txtPostcode.Text.ToUpper) & "',City='" & oCommon.FixSingleQuotes(txtCity.Text.ToUpper) & "',State='" & ddlState.Text & "',BankName='" & oCommon.FixSingleQuotes(txtBankName.Text.ToUpper) & "',AcctNo='" & oCommon.FixSingleQuotes(txtAcctNo.Text.ToUpper) & "',LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "',Pwd='" & oDes.EncryptData(txtPwd.Text) & "' WHERE PengarahID='" & Request.QueryString("pengarahid") & "' AND Tahun='" & txtTahun.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Kemaskini berjaya!"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

End Class