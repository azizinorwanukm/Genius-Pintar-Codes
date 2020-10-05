Imports System.Data.SqlClient

Public Class config_sessiont_edit
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            populateData()
        End If

    End Sub

    Private Sub populateData()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT sessionName,ukm3year,isCurrent, ISNULL(exam_id,0) exam_id, ISNULL(ppcsdate,0) ppcsdate FROM UKM3Session WHERE id=@id"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@id", Request.QueryString("session")))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        txt_namaSession.Text = attachmentsTable.Rows(0).Item(0).ToString
        lblYear.Text = attachmentsTable.Rows(0).Item(1).ToString

        populateDdlExam(attachmentsTable.Rows(0).Item(3).ToString, attachmentsTable.Rows(0).Item(1).ToString)
        setDdlPpcs(attachmentsTable.Rows(0).Item(4).ToString)

        sesiTerkini.SelectedValue = attachmentsTable.Rows(0).Item(2).ToString

    End Sub

    Private Sub populateDdlExam(id As String, examYear As String)
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id,exam_name FROM Exams WHERE examyear=@examyear"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@examyear", examYear))
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim rows As Integer = attachmentsTable.Rows.Count

        If rows = 0 Then
            ddlExams.Items.Add(New ListItem("Tiada ujian untuk tahun " & examYear, 0))
        Else
            ddlExams.Items.Add(New ListItem("-- Tetapan Ujian -- ", 0))
        End If

        For k = 0 To rows - 1
            ddlExams.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        If Not id = 0 Then
            ddlExams.SelectedValue = id
        End If

    End Sub

    Private Sub updateSession_Click(sender As Object, e As EventArgs) Handles btn_updatesession.Click

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "UPDATE UKM3Session SET sessionName = @sessionName "

        Dim sessionID As String = Request.QueryString("session")
        Dim sessionName As String = txt_namaSession.Text
        Dim exam_id As String = "0"
        Dim ppcsdate As String = "0"

        If Not ddlExams.SelectedValue = "0" Then
            exam_id = ddlExams.SelectedValue
            query += ", exam_id = @exam_id "
        Else
            query += ", exam_id = NULL "
        End If

        If Not ddlPpcs.SelectedValue = "0" Then
            ppcsdate = ddlPpcs.SelectedValue
            query += ", ppcsdate = @ppcsdate "
        Else
            query += ", ppcsdate = NULL "
        End If

        query += "WHERE id = @sessionID"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@sessionName", sessionName))
                mCmd.Parameters.Add(New SqlParameter("@sessionID", sessionID))

                If Not exam_id = "0" Then
                    mCmd.Parameters.Add(New SqlParameter("@exam_id", exam_id))
                End If

                If Not ppcsdate = "0" Then
                    mCmd.Parameters.Add(New SqlParameter("@ppcsdate", ppcsdate))
                End If

                mConn.Open()
                mCmd.ExecuteNonQuery()
            End Using
        End Using

        Dim strRet As String = ""
        If sesiTerkini.SelectedValue = "1" Then
            query = "UPDATE UKM3Session SET isCurrent = 0 UPDATE UKM3Session SET isCurrent = 1 WHERE id = " & sessionID
        Else
            query = "UPDATE UKM3Session SET isCurrent = 0 WHERE id = " & sessionID
        End If

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mCmd.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("admin.session_config.aspx")
    End Sub

    Private Sub setDdlPpcs(ppcsdate As String)
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT PPCSDate FROM permatapintar.dbo.master_PPCSDate"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim rows As Integer = attachmentsTable.Rows.Count

        ddlPpcs.Items.Add(New ListItem("-- Tetapan PPCSDate -- ", 0))

        For k = 0 To rows - 1
            ddlPpcs.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(0).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        If Not ppcsdate = "0" Then
            ddlPpcs.SelectedValue = ppcsdate
        End If

    End Sub

    Private Sub back_Click(sender As Object, e As EventArgs) Handles btn_back.Click

        Response.Redirect("admin.session_config.aspx")

    End Sub

End Class