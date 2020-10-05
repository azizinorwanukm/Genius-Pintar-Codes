Imports System.Data.SqlClient

Public Class login1
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblMsg.Text = ""

        checkOnline()

        If Request.QueryString("note") = "done" Then
            lblMsg.Text = "Anda sudah selesai menjawab ujian."
        ElseIf Request.QueryString("note") = "logoff" Then
            lblMsg.Text = "Anda sudah log keluar."
        ElseIf Request.QueryString("note") = "error" Then
            lblMsg.Text = "Sila log masuk dahulu."
        ElseIf Request.QueryString("note") = "closed" Then
            lblMsg.Text = "Anda tidak berdaftar untuk ujian ini."
        End If
    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        Session.Clear()

        Dim logged As Integer = studentLogin(txtLoginID.Text, txtPwd.Text)

        If logged = 1 Then
            Session("User") = txtLoginID.Text
            Session("log") = "log"
            Response.Redirect("logged.aspx")
        ElseIf logged = 2 Then
            Response.Redirect("login.aspx?note=closed")
        Else
            lblMsg.Text = "Wrong Password"
        End If
    End Sub

    Private Function studentLogin(ByVal mykad As String, ByVal password As String) As Integer
        If mykad.Length = 0 Then
            Return 0
        End If

        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT TOP 1 A.id, B.std_ID, B.student_Name, B.student_Mykad, ISNULL(C.exam_id, 0) "
        query += "FROM UKM3 A LEFT JOIN student_info B ON A.student_id = B.std_ID "
        query += "LEFT JOIN UKM3Session C ON A.session_id = C.id "
        query += "WHERE A.active = 1 AND B.student_Mykad = @mykad AND B.password = @password"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@mykad", mykad))
                mCmd.Parameters.Add(New SqlParameter("@password", password))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        If attachmentsTable.Rows.Count = 0 Then
            Return 0
        End If

        If attachmentsTable.Rows(0).Item(4).ToString = "0" Then
            Return 2
        End If

        Session("ukm3id") = attachmentsTable.Rows(0).Item(0).ToString
        Session("StudentId") = attachmentsTable.Rows(0).Item(1).ToString
        Session("StudentName") = attachmentsTable.Rows(0).Item(2).ToString
        Session("mykad") = attachmentsTable.Rows(0).Item(3).ToString
        Session("examId") = attachmentsTable.Rows(0).Item(4).ToString

        Dim ukm3id As String = attachmentsTable.Rows(0).Item(0).ToString
        Dim gotRows As String = CommonMethod.getSingleCellValue("SELECT COUNT(*) FROM StudentAnswers WHERE ukm3id = " & ukm3id)

        If gotRows = "0" Then

            Dim totalQ As Integer = CType(CommonMethod.getSingleCellValue("SELECT TOP 1 quantity FROM Exams WHERE ID = " & attachmentsTable.Rows(0).Item(4).ToString), Integer)

            query = "INSERT INTO StudentAnswers (quest_no,ukm3id) VALUES (1," & ukm3id & ")"

            For k = 2 To totalQ
                query += ",(" & k & "," & ukm3id & ")"
            Next

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using

        End If

        Return 1
    End Function

    Private Sub checkOnline()
        Dim status As String = CommonMethod.getSingleCellValue("SELECT parameter FROM general_config WHERE config = 'Stem Test open'")

        If Not status = "1" Then
            lblMsg.Text = "Status Ujian STEM: Offline"
            btnLogin.Enabled = False
        End If

    End Sub

End Class