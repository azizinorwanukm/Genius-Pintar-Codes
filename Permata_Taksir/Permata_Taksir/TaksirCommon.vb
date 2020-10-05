Imports System.Data.SqlClient

Public Class TaksirCommon

    Public Shared Function getPpcsFromUKM3(ukm3Session As String) As String

        Dim ppcsDate As String = ""
        Dim attachmentsTable = New DataTable
        Dim mAdapter As New SqlDataAdapter

        Dim queryString As String = "SELECT ppcsdate FROM UKM3Session WHERE id = @sessionID"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Try
            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryString, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@sessionID", ukm3Session))
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using
        Catch ex As Exception
            Return ""
        End Try

        ppcsDate = attachmentsTable.Rows(0).Item(0).ToString

        Return ppcsDate
    End Function

    Public Shared Function insertMarklist(Ques_id As String, ukm3_id As String, marks As String, stf_id As String) As Boolean

        Dim attachmentsTable = New DataTable
        Dim mAdapter As New SqlDataAdapter

        Dim queryString As String = "SELECT ukm3id FROM instruktor_marklist WHERE ukm3id= @ukm3id AND Ques_id = @Ques_id"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Try
            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryString, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@ukm3id", ukm3_id))
                    mCmd.Parameters.Add(New SqlParameter("@Ques_id", Ques_id))
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using

            If attachmentsTable.Rows.Count = 0 Then

                queryString = "INSERT INTO instruktor_marklist (Ques_id, ukm3id, marks, stf_id) VALUES (@Ques_id,@ukm3id,@marks,@stf_id)"

            Else
                queryString = "UPDATE instruktor_marklist SET marks=@marks,stf_id=@stf_id WHERE Ques_id=@Ques_id AND ukm3id=@ukm3id"

            End If

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryString, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@Ques_id", Ques_id))
                    mCmd.Parameters.Add(New SqlParameter("@ukm3id", ukm3_id))
                    mCmd.Parameters.Add(New SqlParameter("@marks", marks))
                    mCmd.Parameters.Add(New SqlParameter("@stf_id", stf_id))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function



End Class
