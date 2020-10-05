Public Class logged1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("log") = "log" Then
            Session.Clear()
            Response.Redirect("login.aspx?note=error")
        End If

        Dim strSQL As String = "SELECT COUNT(*) A FROM STUDENTANSWERS WHERE choose = 0 AND ukm3id = " & Session("ukm3id")
        Dim notAnswered As Integer = CType(CommonMethod.getSingleCellValue(strSQL), Integer)

        If notAnswered = 0 Then
            Session.Clear()
            Response.Redirect("login.aspx?note=done")
        End If

    End Sub

End Class