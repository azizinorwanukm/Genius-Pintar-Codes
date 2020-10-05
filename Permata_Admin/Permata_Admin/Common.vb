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

        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT examyearid FROM master_examyear WHERE ExamYear = @examYear "

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@examYear", examYear))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Return CType(attachmentsTable.Rows(0).Item(0).ToString, Integer)

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

    Public Shared Sub deleteUKM1(studentID As String)

        Dim attachmentsTable = New DataTable

        Try
            Dim mAdapter As New SqlDataAdapter

            Dim query As String = "SELECT UKM1Refer FROM master_Examyear WHERE UKM1Refer != 'UKM1'"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using

            Dim count As Integer = attachmentsTable.Rows.Count
            Dim queryStr As String = ""

            For i = 0 To count - 1
                queryStr += " DELETE " & attachmentsTable.Rows(i).Item(0).ToString & " WHERE StudentID = '" & studentID & "' "
            Next

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryStr, mConn)
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub updateUKM1_studentID(newStudentID As String, studentID As String)

        Dim attachmentsTable = New DataTable

        Try
            Dim mAdapter As New SqlDataAdapter

            Dim query As String = "SELECT UKM1Refer FROM master_Examyear WHERE UKM1Refer != 'UKM1'"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using

            Dim count As Integer = attachmentsTable.Rows.Count
            Dim queryStr As String = ""

            For i = 0 To count - 1
                queryStr += " UPDATE " & attachmentsTable.Rows(i).Item(0).ToString & " WITH (UPDLOCK) SET StudentID='" & newStudentID & "' WHERE StudentID='" & studentID & "' "
            Next

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryStr, mConn)
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception

        End Try

    End Sub

    'Public Shared Sub updateUKM1_SchoolCity(newCity As String, ukm1 As UKM1)

    '    Dim attachmentsTable = New DataTable

    '    Try
    '        Dim mAdapter As New SqlDataAdapter

    '        Dim query As String = "SELECT UKM1Refer FROM master_Examyear WHERE UKM1Refer != 'UKM1'"

    '        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

    '        Using mConn As New SqlConnection(strconn)
    '            Using mCmd As New SqlCommand(query, mConn)
    '                mConn.Open()
    '                mAdapter.SelectCommand = mCmd
    '                mAdapter.Fill(attachmentsTable)
    '            End Using
    '        End Using

    '        Dim count As Integer = attachmentsTable.Rows.Count
    '        Dim queryStr As String = ""

    '        For i = 0 To count - 1
    '            queryStr += " UPDATE " & attachmentsTable.Rows(i).Item(0).ToString & " WITH (UPDLOCK) SET SchoolCity='" & newCity & "' WHERE SchoolState='" & ukm1.SchoolState & "' AND SchoolCity='" & ukm1.SchoolCity & "' "
    '        Next

    '        Using mConn As New SqlConnection(strconn)
    '            Using mCmd As New SqlCommand(queryStr, mConn)
    '                mConn.Open()
    '                mCmd.ExecuteNonQuery()
    '            End Using
    '        End Using

    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Public Shared Sub updateUKM1_School(ukm1 As UKM1)

    '    Dim attachmentsTable = New DataTable

    '    Try
    '        Dim mAdapter As New SqlDataAdapter

    '        Dim query As String = "SELECT UKM1Refer FROM master_Examyear WHERE UKM1Refer != 'UKM1'"

    '        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

    '        Using mConn As New SqlConnection(strconn)
    '            Using mCmd As New SqlCommand(query, mConn)
    '                mConn.Open()
    '                mAdapter.SelectCommand = mCmd
    '                mAdapter.Fill(attachmentsTable)
    '            End Using
    '        End Using

    '        Dim count As Integer = attachmentsTable.Rows.Count
    '        Dim queryStr As String = ""

    '        For i = 0 To count - 1
    '            queryStr += " UPDATE " & attachmentsTable.Rows(i).Item(0).ToString & " WITH (UPDLOCK) SET SchoolID='" & ukm1.SchoolID & "',SchoolState='" & ukm1.SchoolState & "',SchoolCity='" & ukm1.SchoolCity & "',SchoolType='" & ukm1.SchoolType & "',SchoolPPD='" & ukm1.SchoolPPD & "' WHERE StudentID='" & ukm1.studentID & "' "
    '        Next

    '        Using mConn As New SqlConnection(strconn)
    '            Using mCmd As New SqlCommand(queryStr, mConn)
    '                mConn.Open()
    '                mCmd.ExecuteNonQuery()
    '            End Using
    '        End Using

    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Public Shared Sub updateUKM1_SchoolByID(ukm1 As UKM1)
    '    Dim attachmentsTable = New DataTable

    '    Try
    '        Dim mAdapter As New SqlDataAdapter

    '        Dim query As String = "SELECT UKM1Refer FROM master_Examyear WHERE UKM1Refer != 'UKM1'"

    '        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

    '        Using mConn As New SqlConnection(strconn)
    '            Using mCmd As New SqlCommand(query, mConn)
    '                mConn.Open()
    '                mAdapter.SelectCommand = mCmd
    '                mAdapter.Fill(attachmentsTable)
    '            End Using
    '        End Using

    '        Dim count As Integer = attachmentsTable.Rows.Count
    '        Dim queryStr As String = ""

    '        For i = 0 To count - 1
    '            queryStr += " UPDATE " & attachmentsTable.Rows(i).Item(0).ToString & " WITH (UPDLOCK) SET SchoolCity='" & ukm1.SchoolCity & "',SchoolState='" & ukm1.SchoolState & "',SchoolType='" & ukm1.SchoolType & "',SchoolPPD='" & ukm1.SchoolPPD & "',SchoolLokasi='" & ukm1.SchoolLokasi & "' WHERE SchoolID='" & ukm1.SchoolID & "' "
    '        Next

    '        Using mConn As New SqlConnection(strconn)
    '            Using mCmd As New SqlCommand(queryStr, mConn)
    '                mConn.Open()
    '                mCmd.ExecuteNonQuery()
    '            End Using
    '        End Using

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Shared Sub updateUKM1_SchoolPPD(newPPD As String, ukm1 As UKM1)

    '    Dim attachmentsTable = New DataTable

    '    Try
    '        Dim mAdapter As New SqlDataAdapter

    '        Dim query As String = "SELECT UKM1Refer FROM master_Examyear WHERE UKM1Refer != 'UKM1'"

    '        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

    '        Using mConn As New SqlConnection(strconn)
    '            Using mCmd As New SqlCommand(query, mConn)
    '                mConn.Open()
    '                mAdapter.SelectCommand = mCmd
    '                mAdapter.Fill(attachmentsTable)
    '            End Using
    '        End Using

    '        Dim count As Integer = attachmentsTable.Rows.Count
    '        Dim queryStr As String = ""

    '        For i = 0 To count - 1
    '            queryStr += " UPDATE " & attachmentsTable.Rows(i).Item(0).ToString & " WITH (UPDLOCK) SET SchoolPPD='" & newPPD & "' WHERE SchoolState='" & ukm1.SchoolState & "' AND SchoolPPD='" & ukm1.SchoolPPD & "' "
    '        Next

    '        Using mConn As New SqlConnection(strconn)
    '            Using mCmd As New SqlCommand(queryStr, mConn)
    '                mConn.Open()
    '                mCmd.ExecuteNonQuery()
    '            End Using
    '        End Using

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Public Shared Function getUKM1Fullmark() As Integer

        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT TOP 1 configString FROM master_Config WHERE configCode = 'ukm1fullmark'"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Return CType(attachmentsTable.Rows(0).Item(0).ToString, Integer)

    End Function

    Public Shared Function getUKM1tableByYear(examyear_id As String) As String

        Try
            Dim attachmentsTable = New DataTable

            Dim mAdapter As New SqlDataAdapter

            Dim query As String = "SELECT UKM1Refer FROM master_Examyear WHERE examyear_id = @examyear_id "

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@examyear_id", examyear_id))
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
