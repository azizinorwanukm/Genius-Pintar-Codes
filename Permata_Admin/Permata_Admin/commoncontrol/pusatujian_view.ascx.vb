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

        pusatujian_view()
        setAccess()

    End Sub

    Private Sub setAccess()
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                lnkUpdate.Visible = True
            Case "ADMINOP"
                lnkUpdate.Visible = True
            Case "SUBADMIN"
                lnkUpdate.Visible = True
            Case Else
                lnkUpdate.Visible = False
        End Select

    End Sub

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
                    Dim strFieldTemp As String = MyTable.Rows(nRows).Item("Komen").ToString
                    Dim strFieldNew As String = strFieldTemp.Replace("\n", "<br />")
                    txtKomen.Text = strFieldTemp
                Else
                    txtKomen.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub lnkUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkUpdate.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.update.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "ADMINOP"
                Response.Redirect("pusatujian.update.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.update.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk fungsi ini. " & getUserProfile_UserType()
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class