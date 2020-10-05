Public Class logged2
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        checkOnline()

        Try
            If Not IsPostBack Then
                lblUser.Text = Session("StudentName")
                lblMykad.Text = Session("mykad")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub startExam() Handles btnStartExam.Click

        Response.Redirect("question.aspx?question=1")

    End Sub

    Private Sub checkOnline()
        Dim status As String = CommonMethod.getSingleCellValue("SELECT parameter FROM general_config WHERE config = 'Stem Test open'")

        If Not status = "1" Then
            Session.Clear()
            Response.Redirect("login.aspx")
        End If

    End Sub

End Class