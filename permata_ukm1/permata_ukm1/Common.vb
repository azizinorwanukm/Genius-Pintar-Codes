Imports System.Data.SqlClient

Public Class Common

    Public Shared Function getDefaultExamYearID() As Integer

        Dim strSQL As String = "SELECT A.examyearid FROM master_examyear A JOIN master_Config B ON A.ExamYear = B.configString WHERE B.configCode='DefaultExamYear'"

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

        Return CType(attachmentsTable.Rows(0).Item(0).ToString, Integer)

    End Function

    Public Shared Function getExamYearID(examYear As String) As Integer

        Dim strSQL As String = "SELECT examyearid FROM master_examyear WHERE ExamYear = '" & examYear & "' "

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

        Return CType(attachmentsTable.Rows(0).Item(0).ToString, Integer)

    End Function

    Public Shared Function isUKM1Done(studentID As String) As Boolean

        Dim strSQL As String = "SELECT UKM1ID FROM UKM1 WHERE StudentID = @studentID AND examyear_id = @examyear_id AND Status = 'DONE'"

        Dim mAdapter As New SqlDataAdapter

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim attachmentsTable As New DataTable

        Try
            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(strSQL, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@studentID", studentID))
                    mCmd.Parameters.Add(New SqlParameter("@examyear_id", getDefaultExamYearID()))
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using
        Catch ex As Exception
            Return False
        End Try

        If attachmentsTable.Rows.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Public Shared Function getUKM1Table(examYear As String) As String

        Try
            Dim attachmentsTable = New DataTable

            Dim mAdapter As New SqlDataAdapter

            Dim query As String = "SELECT UKM1Refer FROM master_Examyear WHERE ExamYear = @examYear "

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@examYear", examYear))
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using

            Return attachmentsTable.Rows(0).Item(0).ToString
        Catch ex As Exception
            Return "UKM1"
        End Try

    End Function

End Class
