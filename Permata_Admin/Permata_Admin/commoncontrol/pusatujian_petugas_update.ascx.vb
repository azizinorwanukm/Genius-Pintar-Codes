Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class pusatujian_petugas_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                State_list()
                ''--load PusatUjian_Petugas
                PusatUjian_Petugas_Load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub State_list()
        ''strSQL = "SELECT DISTINCT schoolstate FROM schoolprofile ORDER BY SchoolState"
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) ORDER BY SchoolStateID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "schoolstate"
            ddlState.DataValueField = "schoolstate"
            ddlState.DataBind()

            ''ddlState.Items.Add(New ListItem("ALL", "ALL"))

            ''default state
            strRet = getUserProfile_State()
            ddlState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlState.Enabled = False
            End If
            ''debug
            'Response.Write(getUserProfile_State())

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub PusatUjian_Petugas_Load()
        strSQL = "Select * FROM PusatUjian_Petugas Where PetugasID='" & Request.QueryString("petugasid") & "'"
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
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UserType")) Then
                    selUserType.Value = ds.Tables(0).Rows(0).Item("UserType")
                Else
                    selUserType.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    txtMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    txtMYKAD.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    txtFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    txtFullname.Text = ""
                End If

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PostCode")) Then
                    txtPostCode.Text = ds.Tables(0).Rows(0).Item("PostCode")
                Else
                    txtPostCode.Text = ""
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("BankName")) Then
                    txtBankName.Text = ds.Tables(0).Rows(0).Item("BankName")
                Else
                    txtBankName.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AccountNo")) Then
                    txtAccountNo.Text = ds.Tables(0).Rows(0).Item("AccountNo")
                Else
                    txtAccountNo.Text = ""
                End If
            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ''--validate screen
        If ValidatePage() = False Then
            Exit Sub
        End If

        strSQL = "UPDATE PusatUjian_Petugas SET UserType='" & selUserType.Value & "',Fullname='" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "',ContactNo='" & oCommon.FixSingleQuotes(txtContactNo.Text) & "',Email='" & oCommon.FixSingleQuotes(txtEmail.Text) & "',MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "',BankName='" & oCommon.FixSingleQuotes(txtBankName.Text.ToUpper) & "',AccountNo='" & oCommon.FixSingleQuotes(txtAccountNo.Text) & "',Address1='" & oCommon.FixSingleQuotes(txtAddress1.Text.ToUpper) & "',Address2='" & oCommon.FixSingleQuotes(txtAddress2.Text.ToUpper) & "',Postcode='" & oCommon.FixSingleQuotes(txtPostCode.Text) & "',City='" & oCommon.FixSingleQuotes(txtCity.Text.ToUpper) & "',State='" & ddlState.Text & "' WHERE PetugasID=" & Request.QueryString("petugasid")
        '--debug
        'Response.Write(strSQL)
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            '--ClearScreen()
            lblMsg.Text = "Successfully UPDATE Petugas Pusat Ujian UKM2."
        Else
            lblMsg.Text = "Error UPDATE Petugas. " & strRet
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Please fill in mandatory field."
            txtMYKAD.Focus()
            Return False
        End If

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Please fill in mandatory field."
            txtFullname.Focus()
            Return False

        End If

        If txtContactNo.Text.Length = 0 Then
            lblMsg.Text = "Please fill in mandatory field."
            txtContactNo.Focus()
            Return False

        End If

        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Please fill in mandatory field."
            txtEmail.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strRet = PusatUjian_Petugas_delete()
        If strRet = "0" Then
            ClearScreen()
            lblMsg.Text = "Berjaya menghapuskan rekod petugas tersebut."
        Else
            lblMsg.Text = "Error:" & strRet
        End If

    End Sub

    Private Sub ClearScreen()
        txtMYKAD.Text = ""
        txtFullname.Text = ""
        txtContactNo.Text = ""
        txtEmail.Text = ""
        txtBankName.Text = ""
        txtAccountNo.Text = ""
        selUserType.SelectedIndex = 0

        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtPostCode.Text = ""
        txtCity.Text = ""
        ddlState.SelectedIndex = 0

    End Sub

    Private Function PusatUjian_Petugas_delete() As String
        strRet = "0"
        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using connection As New SqlConnection(strconn)
            connection.Open()

            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction("TxnStart")

            ' Must assign both transaction object and connection 
            ' to Command object for a pending local transaction.
            command.Connection = connection
            command.Transaction = transaction
            command.CommandTimeout = 300    '5minit. timeout in second

            Try
                '--1
                strSQL = "DELETE FROM PusatUjian_Petugas WHERE PetugasID=" & Request.QueryString("petugasid")
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                '--2 PusatUjian_Petugas_List
                strSQL = "DELETE FROM PusatUjian_Petugas_List WHERE PetugasID=" & Request.QueryString("petugasid")
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

            Catch ex As Exception
                'Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
                'Console.WriteLine("  Message: {0}", ex.Message)
                strRet = ex.Message

                ' Attempt to roll back the transaction. 
                Try
                    transaction.Rollback()

                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    'Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    'Console.WriteLine("  Message: {0}", ex2.Message)

                    strRet = "Rollback:" & ex2.Message

                End Try
            End Try
        End Using

        '--0 means success
        Return strRet

    End Function


    Protected Sub lnkSenaraiPetugas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSenaraiPetugas.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.list.all.aspx")
            Case "ADMINOP"
                Response.Redirect("pusatujian.list.all.aspx")
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.list.all.aspx")
            Case Else
                lblMsg.Text = "Invalid usertype!"
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class