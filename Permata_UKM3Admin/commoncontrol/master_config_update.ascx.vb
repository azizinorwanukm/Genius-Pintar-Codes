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
            If Request.QueryString("type") = "Update" Then
                btnUpdate.Text = "Kemaskini"
                btnDelete.Visible = True
            ElseIf Request.QueryString("type") = "Insert" Then
                btnUpdate.Text = "Kemasukan"
                btnDelete.Visible = False
            End If
            If Not IsPostBack Then
                master_Config_view()
                insertconfig_view()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub master_Config_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        If Request.QueryString("type") = "Update" Then

            strSQL = "SELECT * FROM master WHERE data_id=" & Request.QueryString("configID")
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Try
                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "AnyTable")

                Dim nRows As Integer = 0
                Dim MyTable As DataTable = New DataTable
                MyTable = ds.Tables(0)
                If MyTable.Rows.Count > 0 Then

                    If Not IsDBNull(MyTable.Rows(nRows).Item("description")) Then
                        txtdescription.Text = MyTable.Rows(nRows).Item("description").ToString
                    Else
                        txtdescription.Text = ""
                    End If

                    If Not IsDBNull(MyTable.Rows(nRows).Item("type")) Then
                        txttype.Text = MyTable.Rows(nRows).Item("type").ToString
                    Else
                        txttype.Text = ""
                    End If

                End If
            Catch ex As Exception

            Finally
                objConn.Dispose()
            End Try



        End If

    End Sub
    Private Sub insertconfig_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        If Request.QueryString("type") = "Insert" Then

            strSQL = "select type from master where type = '" & Request.QueryString("datatype") & "'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Try
                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "AnyTable")

                Dim nRows As Integer = 0
                Dim MyTable As DataTable = New DataTable
                MyTable = ds.Tables(0)
                If MyTable.Rows.Count > 0 Then

                    If Not IsDBNull(MyTable.Rows(nRows).Item("type")) Then
                        txttype.Text = MyTable.Rows(nRows).Item("type").ToString
                    Else
                        txttype.Text = ""
                    End If

                End If
            Catch ex As Exception

            Finally
                objConn.Dispose()

            End Try
        End If

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT staff_position FROM staff_info WHERE staf_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If Request.QueryString("type") = "Update" Then
            strSQL = "UPDATE master SET description='" & oCommon.FixSingleQuotes(txtdescription.Text) & "',type='" & oCommon.FixSingleQuotes(txttype.Text) & "' WHERE data_id=" & Request.QueryString("configID")
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Berjaya mengemaskini Sistem Konfigurasi."
            Else
                lblMsg.Text = "GAGAL mengemaskini Sistem Konfigurasi." & strRet
            End If

        ElseIf Request.QueryString("type") = "Insert" Then
            If Not txtdescription.Text = "" Then

                If Not txttype.Text = "" Then

                    strSQL = "Insert into master(description,type) values ('" & txtdescription.Text & "','" & txttype.Text & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If strRet = "0" Then
                        lblMsg.Text = "Berjaya Memasukkan '" & txtdescription.Text & "' ke dalam Sistem Konfigurasi."
                    Else
                        lblMsg.Text = "GAGAL memasukkan data ke dalam Sistem Konfigurasi." & strRet

                    End If

                Else
                    lblMsg.Text = "sila isi data jenis"
                End If
            Else
                lblMsg.Text = "sila is data penerangan"
            End If
            End If



    End Sub

    Private Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("ukm3.config_system.aspx")

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        strSQL = "DELETE FROM master WHERE data_id=" & Request.QueryString("configID")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA menghapuskan reokd tersebut."
        End If

    End Sub
End Class