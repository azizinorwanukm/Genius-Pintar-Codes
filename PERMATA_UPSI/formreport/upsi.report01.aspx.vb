Imports System.Data.SqlClient


Public Class upsi_report01
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        RefreshResult()
    End Sub


    Protected Sub ddlmodule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlmodule.SelectedIndexChanged

        If ddlmodule.SelectedValue.ToString <> "" Then
            Label2.Text = ddlmodule.SelectedItem.Text & " Result"
        Else
            Label2.Text = ""

            lblQuestion.InnerText = "N/A"
            lblAnswered.InnerText = "N/A"
            lblmark.InnerText = "N/A"

        End If

        RefreshResult()

    End Sub

    Private Sub upsi_report01_Load(sender As Object, e As EventArgs) Handles Me.Load

        lblname.InnerText = UCase(Session("UserName"))
        lblmykid.InnerText = Session("UserICNo")
        lblExam.Text = Session("examid")
        lblassistant.InnerText = UCase(Session("AssistantName"))
        lblcontact.InnerText = Session("AssistantPhoneNo")

        lblQuestion.InnerText = "N/A"
        lblAnswered.InnerText = "N/A"
        lblmark.InnerText = "N/A"


        If Not Page.IsPostBack Then RefreshResult()

    End Sub

    Private Sub RefreshResult()

        Dim c As String = ddlmodule.Text.ToString

        Dim a As String = Nothing
        Dim mqno As String = Nothing
        Dim mdate As String = Nothing
        Dim manswered As String = Nothing
        Dim mmark As String = Nothing

        Dim row As HtmlTableRow
        Dim col As HtmlTableCell

        Dim totalQ As Integer = 0
        Dim totalanswered As Integer = 0
        Dim totalmark As Integer = 0
        Dim totalpercentage As Integer = 0
        Dim moduleid As String = 0
        Dim i As Integer = 0

        Select Case ddlmodule.SelectedValue

            Case 1
                totalQ = 18
            Case 2
                totalQ = 27
            Case 3
                totalQ = 29
            Case 4
                totalQ = 25
            Case 5
                totalQ = 30
            Case 6
                totalQ = 10
            Case 7
                totalQ = 34
            Case 8
                totalQ = 4
            Case 9
                totalQ = 22
            Case 10
                totalQ = 10

        End Select


        'Acces table examlog
        Using Con As SqlConnection = New SqlConnection(modFunction.conStr)
            Con.Open()
            'exam
            Using Comm As SqlCommand = New SqlCommand("spReport", Con)
                Comm.CommandType = CommandType.StoredProcedure

                Comm.Parameters.AddWithValue("reporttype", "individual")
                Comm.Parameters.AddWithValue("examid", lblExam.Text)
                Comm.Parameters.AddWithValue("moduleno", ddlmodule.SelectedValue)

                Using ds As SqlDataReader = Comm.ExecuteReader


                    Do While ds.Read


                        mqno = ds("no").ToString
                        mdate = ds("logs").ToString
                        mdate = Format(CDate(mdate), "dd/MM/yyyy HH:mm:ss")
                        manswered = ds("answers").ToString
                        mmark = ds("mark").ToString

                        totalanswered = totalanswered + 1
                        totalmark = totalmark + mmark

                        i = i + 1
                        row = New HtmlTableRow

                        col = New HtmlTableCell

                        col.InnerHtml = mqno
                        row.Cells.Add(col)

                        col = New HtmlTableCell
                        col.InnerHtml = mdate
                        row.Cells.Add(col)

                        col = New HtmlTableCell
                        col.InnerHtml = manswered
                        row.Cells.Add(col)


                        col = New HtmlTableCell
                        col.InnerHtml = mmark
                        row.Cells.Add(col)

                        tbllaporan4.Rows.Add(row)

                    Loop

                    ds.Close()

                End Using

            End Using
            Con.Close()
        End Using

        lblmark.InnerText = totalmark.ToString
        lblAnswered.InnerText = totalanswered.ToString
        lblQuestion.InnerText = totalQ


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        Try

            Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

                Con.Open()

                Using Comm As SqlCommand = New SqlCommand("spExamLog", Con)

                    Comm.CommandType = CommandType.StoredProcedure

                    Comm.Parameters.AddWithValue("operation", "clear")
                    Comm.Parameters.AddWithValue("examid", lblExam.Text)
                    Comm.Parameters.AddWithValue("moduleno", ddlmodule.SelectedValue)

                    Comm.ExecuteNonQuery()

                End Using

                Con.Close()

            End Using


        Catch ex As Exception

        End Try

        RefreshResult()

    End Sub

End Class