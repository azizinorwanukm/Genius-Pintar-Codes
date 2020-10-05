Imports System.Data.SqlClient

Public Class CommonMethod

    Public Shared Function getSingleCellValue(ByVal strSQL As String) As String

        Dim mAdapter As New SqlDataAdapter

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim attachmentsTable As New DataTable

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(strSQL, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Return attachmentsTable.Rows(0).Item(0).ToString

    End Function

    Public Shared Function gotRow(ByVal strSQL As String) As Boolean

        Dim attachmentsTable As New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(strSQL, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        If attachmentsTable.Rows.Count > 0 Then
            Return True
        End If

        Return False

    End Function


End Class
