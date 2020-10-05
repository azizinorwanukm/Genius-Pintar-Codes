Public Class upsi_outro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            modFunction.UpdateEndTime(Session("UserId").ToString, Session("ExamId").ToString)

        Catch ex As Exception

        End Try


    End Sub

End Class