Imports System.Data.SqlClient

Public Class instruktor_update_koko
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
                koko_state_list()
                ddlState.SelectedValue = "0"

                koko_kelas_list()
                ddlKelas.SelectedValue = "0"

                koko_uniform_list()
                ddlUniform.SelectedValue = "0"

                koko_persatuan_list()
                ddlPersatuan.SelectedValue = "0"

                koko_sukan_list()
                ddlSukan.SelectedValue = "0"

                koko_rumahsukan_list()
                ddlRumahsukan.SelectedValue = "0"

                koko_instruktor_load()
            End If

        Catch ex As Exception

        End Try

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

            ddlState.Items.Add(New ListItem("PILIH", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub koko_kelas_list()
        strSQL = "SELECT * FROM koko_kelas WHERE Tahun='" & Request.QueryString("tahun") & "' ORDER BY Kelas ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "Kelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            ddlKelas.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_uniform_list()
        strSQL = "SELECT * FROM koko_uniform WHERE Tahun='" & Request.QueryString("tahun") & "' ORDER BY Uniform ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniform.DataSource = ds
            ddlUniform.DataTextField = "Uniform"
            ddlUniform.DataValueField = "UniformID"
            ddlUniform.DataBind()

            ddlUniform.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_persatuan_list()
        strSQL = "SELECT * FROM koko_persatuan WHERE Tahun='" & Request.QueryString("tahun") & "' ORDER BY Persatuan ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPersatuan.DataSource = ds
            ddlPersatuan.DataTextField = "Persatuan"
            ddlPersatuan.DataValueField = "PersatuanID"
            ddlPersatuan.DataBind()

            ddlPersatuan.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_sukan_list()
        strSQL = "SELECT * FROM koko_sukan WHERE Tahun='" & Request.QueryString("tahun") & "' ORDER BY Sukan ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSukan.DataSource = ds
            ddlSukan.DataTextField = "Sukan"
            ddlSukan.DataValueField = "SukanID"
            ddlSukan.DataBind()

            ddlSukan.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_rumahsukan_list()
        strSQL = "SELECT * FROM koko_rumahsukan WHERE Tahun='" & Request.QueryString("tahun") & "' ORDER BY Rumahsukan ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRumahsukan.DataSource = ds
            ddlRumahsukan.DataTextField = "Rumahsukan"
            ddlRumahsukan.DataValueField = "RumahsukanID"
            ddlRumahsukan.DataBind()

            ddlRumahsukan.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_instruktor_load()
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        '--INSTRUKTOR INFO
        tmpSQL = "SELECT koko_instruktor.InstruktorID,koko_instruktor.Fullname,koko_instruktor.MYKAD,koko_instruktor.ContactNo,koko_instruktor.Email,koko_instruktor.Address1,koko_instruktor.Address2,koko_instruktor.Postcode,koko_instruktor.City,koko_instruktor.State,koko_instruktor.LoginID,koko_instruktor.Pwd,koko_instruktor.BankName,koko_instruktor.AcctNo,koko_instruktor.Tahun,koko_instruktor.BankName,koko_instruktor.AcctNo,koko_instruktor.KelasID,koko_kelas.Kelas,koko_instruktor.UniformID,koko_uniform.Uniform,koko_instruktor.PersatuanID,koko_persatuan.Persatuan,koko_instruktor.SukanID,koko_sukan.Sukan,koko_instruktor.RumahsukanID,koko_rumahsukan.Rumahsukan,koko_instruktor.KetuaUniform,koko_instruktor.KetuaPersatuan,koko_instruktor.KetuaSukan,koko_instruktor.KetuaRumahsukan FROM koko_instruktor"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_instruktor.KelasID=koko_kelas.KelasID"
        tmpSQL += " LEFT OUTER JOIN koko_uniform ON koko_instruktor.UniformID=koko_uniform.UniformID"
        tmpSQL += " LEFT OUTER JOIN koko_persatuan ON koko_instruktor.PersatuanID=koko_persatuan.PersatuanID"
        tmpSQL += " LEFT OUTER JOIN koko_sukan ON koko_instruktor.SukanID=koko_sukan.SukanID"
        tmpSQL += " LEFT OUTER JOIN koko_rumahsukan ON koko_instruktor.RumahsukanID=koko_rumahsukan.RumahsukanID"
        strWhere = " WHERE koko_instruktor.Tahun='" & Request.QueryString("tahun") & "'"
        strWhere += " AND koko_instruktor.InstruktorID='" & Request.QueryString("instruktorid") & "'"

        strSQL = tmpSQL & strWhere & strOrder
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
                '--unique MYKAD required
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

                '--KOKURIKULUM
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KelasID")) Then
                    ddlKelas.SelectedValue = ds.Tables(0).Rows(0).Item("KelasID")
                Else
                    ddlKelas.SelectedValue = "0"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UniformID")) Then
                    ddlUniform.SelectedValue = ds.Tables(0).Rows(0).Item("UniformID")
                Else
                    ddlUniform.SelectedValue = "0"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PersatuanID")) Then
                    ddlPersatuan.SelectedValue = ds.Tables(0).Rows(0).Item("PersatuanID")
                Else
                    ddlPersatuan.SelectedValue = "0"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SukanID")) Then
                    ddlSukan.SelectedValue = ds.Tables(0).Rows(0).Item("SukanID")
                Else
                    ddlSukan.SelectedValue = "0"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("RumahsukanID")) Then
                    ddlRumahsukan.SelectedValue = ds.Tables(0).Rows(0).Item("RumahsukanID")
                Else
                    ddlRumahsukan.SelectedValue = "0"
                End If

                '--KETUA
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KetuaUniform")) Then
                    chkKetuaUniform.Checked = ds.Tables(0).Rows(0).Item("KetuaUniform")
                Else
                    chkKetuaUniform.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KetuaPersatuan")) Then
                    chkKetuaPersatuan.Checked = ds.Tables(0).Rows(0).Item("KetuaPersatuan")
                Else
                    chkKetuaPersatuan.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KetuaSukan")) Then
                    chkKetuaSukan.Checked = ds.Tables(0).Rows(0).Item("KetuaSukan")
                Else
                    chkKetuaSukan.Checked = False
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KetuaRumahsukan")) Then
                    chkKetuaRumahsukan.Checked = ds.Tables(0).Rows(0).Item("KetuaRumahsukan")
                Else
                    chkKetuaRumahsukan.Checked = False
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
        strSQL = "UPDATE koko_instruktor SET UniformID=" & ddlUniform.SelectedValue & ",PersatuanID=" & ddlPersatuan.SelectedValue & ",SukanID=" & ddlSukan.SelectedValue & ",RumahsukanID=" & ddlRumahsukan.SelectedValue & ",KetuaUniform='" & chkKetuaUniform.Checked & "',KetuaPersatuan='" & chkKetuaPersatuan.Checked & "',KetuaSukan='" & chkKetuaSukan.Checked & "',KetuaRumahsukan='" & chkKetuaRumahsukan.Checked & "' WHERE InstruktorID='" & Request.QueryString("instruktorid") & "' AND Tahun='" & Request.QueryString("tahun") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Kemaskini berjaya!"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

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

        '--change made
        If Not lblMYKADOrg.Text = txtMYKAD.Text Then
            ''--check if already exist
            strSQL = "SELECT MYKAD FROM koko_instruktor WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Telah digunakan."
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.instruktor.list.aspx?set=koko")

    End Sub

End Class