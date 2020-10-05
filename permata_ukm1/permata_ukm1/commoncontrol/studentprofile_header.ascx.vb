Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class studentprofile_header
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        StudentProfile_header()

    End Sub

    Private Sub StudentProfile_header()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM StudentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
        ''--debug
        ''Response.Write(strSQL)

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("MYKAD")) Then
                    lblMYKAD.Text = MyTable.Rows(nRows).Item("MYKAD").ToString
                Else
                    lblMYKAD.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("StudentFullname")) Then
                    lblStudentFullname.Text = MyTable.Rows(nRows).Item("StudentFullname").ToString
                Else
                    lblStudentFullname.Text = ""
                End If

            End If
        Catch ex As Exception
            Response.Write("system error:" & ex.Message)
        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class