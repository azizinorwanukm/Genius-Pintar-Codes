Imports System.Data.SqlClient

Public Class question1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("log") = "log" Then
            Session.Clear()
            Response.Redirect("login.aspx?note=error")
        End If

        Dim strSQL As String = "SELECT COUNT(*) A FROM STUDENTANSWERS WHERE choose = 0 AND ukm3id = " & Session("ukm3id")
        Dim notAnswered As Integer = CType(CommonMethod.getSingleCellValue(strSQL), Integer)

        If notAnswered > 0 Then

            strSQL = "SELECT TOP 1 quest_no FROM STUDENTANSWERS WHERE choose = 0 AND ukm3id = " & Session("ukm3id") & " ORDER BY quest_no"
            Dim currentquestion As String = CommonMethod.getSingleCellValue(strSQL)

            If currentquestion <> Request.QueryString("question") Then
                Response.Redirect("question.aspx?question=" & currentquestion)
            End If

        End If

    End Sub

End Class