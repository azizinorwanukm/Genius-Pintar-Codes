Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class schoolprofile_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        schoolprofile_view()

    End Sub

    Private Sub schoolprofile_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)


        strSQL = "SELECT * FROM SchoolProfile WHERE SchoolID='" & Request.QueryString("schoolid") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolName")) Then
                    lblSchoolName.Text = MyTable.Rows(nRows).Item("SchoolName").ToString
                Else
                    lblSchoolName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCode")) Then
                    lblSchoolCode.Text = MyTable.Rows(nRows).Item("SchoolCode").ToString
                Else
                    lblSchoolCode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolAddress")) Then
                    lblSchoolAddress.Text = MyTable.Rows(nRows).Item("SchoolAddress").ToString
                Else
                    lblSchoolAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolPostcode")) Then
                    lblSchoolPostcode.Text = MyTable.Rows(nRows).Item("SchoolPostcode").ToString
                Else
                    lblSchoolPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCity")) Then
                    lblSchoolCity.Text = MyTable.Rows(nRows).Item("SchoolCity").ToString
                Else
                    lblSchoolCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    lblSchoolState.Text = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    lblSchoolState.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolType")) Then
                    lblSchoolType.Text = MyTable.Rows(nRows).Item("SchoolType").ToString
                Else
                    lblSchoolType.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolNoTel")) Then
                    lblSchoolNoTel.Text = MyTable.Rows(nRows).Item("SchoolNoTel").ToString
                Else
                    lblSchoolNoTel.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolNoFax")) Then
                    lblSchoolNoFax.Text = MyTable.Rows(nRows).Item("SchoolNoFax").ToString
                Else
                    lblSchoolNoFax.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolLokasi")) Then
                    lblSchoolLokasi.Text = MyTable.Rows(nRows).Item("SchoolLokasi").ToString
                Else
                    lblSchoolLokasi.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolEmail")) Then
                    lblSchoolEmail.Text = MyTable.Rows(nRows).Item("SchoolEmail").ToString
                Else
                    lblSchoolEmail.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolPPD")) Then
                    lblSchoolPPD.Text = MyTable.Rows(nRows).Item("SchoolPPD").ToString
                Else
                    lblSchoolPPD.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("IsDeleted")) Then
                    lblIsDeleted.Text = MyTable.Rows(nRows).Item("IsDeleted").ToString
                Else
                    lblIsDeleted.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

End Class