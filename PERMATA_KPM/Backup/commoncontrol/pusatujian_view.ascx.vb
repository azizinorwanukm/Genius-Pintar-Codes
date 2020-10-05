Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class pusatujian_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                pusatujian_view()
                setAccessRight()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub setAccessRight()

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                lnkEdit.Visible = True
            Case "SUBADMIN"
                lnkEdit.Visible = True
            Case "UKM"
                lnkEdit.Visible = True
            Case Else
                lnkEdit.Visible = False
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & Request.Cookies("ukmkpm_loginid").Value & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub pusatujian_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''--display PusatUjian profile
        strSQL = "SELECT * FROM PusatUjian WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatName")) Then
                    lblPusatName.Text = MyTable.Rows(nRows).Item("PusatName").ToString
                Else
                    lblPusatName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatAddress")) Then
                    lblPusatAddress.Text = MyTable.Rows(nRows).Item("PusatAddress").ToString
                Else
                    lblPusatAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatPostcode")) Then
                    lblPusatPostcode.Text = MyTable.Rows(nRows).Item("PusatPostcode").ToString
                Else
                    lblPusatPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatCity")) Then
                    lblPusatCity.Text = MyTable.Rows(nRows).Item("PusatCity").ToString
                Else
                    lblPusatCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatState")) Then
                    lblPusatState.Text = MyTable.Rows(nRows).Item("PusatState").ToString
                Else
                    lblPusatState.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatType")) Then
                    lblPusatType.Text = MyTable.Rows(nRows).Item("PusatType").ToString
                Else
                    lblPusatType.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatNoTel")) Then
                    lblPusatNoTel.Text = MyTable.Rows(nRows).Item("PusatNoTel").ToString
                Else
                    lblPusatNoTel.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatNoFax")) Then
                    lblPusatNoFax.Text = MyTable.Rows(nRows).Item("PusatNoFax").ToString
                Else
                    lblPusatNoFax.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatEmail")) Then
                    lblPusatEmail.Text = MyTable.Rows(nRows).Item("PusatEmail").ToString
                Else
                    lblPusatEmail.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatPPD")) Then
                    lblPusatPPD.Text = MyTable.Rows(nRows).Item("PusatPPD").ToString
                Else
                    lblPusatPPD.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatTahun")) Then
                    lblPusatTahun.Text = MyTable.Rows(nRows).Item("PusatTahun").ToString
                Else
                    lblPusatTahun.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatJumlahLab")) Then
                    lblPusatJumlahLab.Text = MyTable.Rows(nRows).Item("PusatJumlahLab").ToString
                Else
                    lblPusatJumlahLab.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatJumlahKomp")) Then
                    lblPusatJumlahKomp.Text = MyTable.Rows(nRows).Item("PusatJumlahKomp").ToString
                Else
                    lblPusatJumlahKomp.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Komen")) Then
                    lblKomen.Text = MyTable.Rows(nRows).Item("Komen").ToString
                Else
                    lblKomen.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkEdit.Click

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.update.aspx?pusatcode=" & Request.QueryString("pusatcode"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.update.aspx?pusatcode=" & Request.QueryString("pusatcode"))
            Case "UKM"
                Response.Redirect("ukm.pusatujian.update.aspx?pusatcode=" & Request.QueryString("pusatcode"))
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub
End Class