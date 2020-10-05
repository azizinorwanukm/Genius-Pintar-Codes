Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm1_semak
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            examyear_list()
            ddlExamYear.Text = Now.Year

        End If

    End Sub

    Private Sub examyear_list()
        strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE LEN(ExamYear)=4 ORDER BY ExamYear"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "ExamYear"
            ddlExamYear.DataBind()

            '--ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strSQL = "SELECT StudentID FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "' AND Status='DONE'"
        If oCommon.isExist(strSQL) = False Then
            lblMsg.Text = "Rekod Ujian UKM1 anda tidak dijumpai bagi tahun " & ddlExamYear.Text
            Exit Sub
        End If

        '--over 15minutes only
        getDuration()

        If CInt(lblTotalMin.Text) < 16 Then
            lblMsg.Text = "Sijil hanya akan dijana untuk pelajar yang menghabiskan ujian melebihi 15 minit sahaja. Harap Maklum."
            Exit Sub
        End If

        Response.Redirect("ukm1.permata.end.aspx?studentid=" & Request.QueryString("studentid") & "&examyear=" & ddlExamYear.Text)
    End Sub

    Private Sub getDuration()
        Dim nDuration As Integer = 0  ''duration in seconds
        Dim tsDuration As TimeSpan
        Dim strTotalSeconds As String = "0"
        Dim strDurationString As String = "0"

        '--init
        lblExamStart.Text = ""
        lblExamEnd.Text = ""

        ''exam end and exam year
        strSQL = "SELECT ExamStart FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strExamStart = oCommon.getFieldValue(strSQL)
        If Len(strExamStart) > 0 Then
            lblExamStart.Text = Mid(strExamStart, 1, 4) & "/" & Mid(strExamStart, 5, 2) & "/" & Mid(strExamStart, 7, 2) & " " & Mid(strExamStart, 10, 8)
        End If

        strSQL = "SELECT ExamEnd FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & ddlExamYear.Text & "'"
        Dim strExamEnd As String = oCommon.getFieldValue(strSQL)
        If Len(strExamEnd) > 0 Then
            lblExamEnd.Text = Mid(strExamEnd, 1, 4) & "/" & Mid(strExamEnd, 5, 2) & "/" & Mid(strExamEnd, 7, 2) & " " & Mid(strExamEnd, 10, 8)
        End If

        If Len(lblExamStart.Text) > 0 And Len(lblExamEnd.Text) > 0 Then
            tsDuration = CDate(lblExamEnd.Text) - CDate(lblExamStart.Text)
            lblDuration.Text = "Days: " & tsDuration.Days & " Hours: " & tsDuration.Hours & " Min: " & tsDuration.Minutes & " Sec:" & tsDuration.Seconds
            lblTotalMin.Text = tsDuration.TotalMinutes
        Else
            strDurationString = "0"
            strTotalSeconds = "0"
        End If


    End Sub

End Class