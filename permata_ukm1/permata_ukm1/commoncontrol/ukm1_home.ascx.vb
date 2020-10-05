Public Class ukm1_home
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Dim getExamYear As String = "Select configString from master_Config where configCode = 'UKM1ExamYear'"
                Dim ExamYear As String = oCommon.getFieldValue(getExamYear)

                lblYear01.Text = ExamYear
                lblYear02.Text = ExamYear
                lblYear03.Text = ExamYear
                lblYear04.Text = ExamYear
                lblYear05.Text = ExamYear
                lblYear06.Text = ExamYear
                lblYear07.Text = ExamYear

            End If

        Catch ex As Exception
            Response.Write("Err:" & ex.Message)

        End Try
    End Sub

    Private Sub btnNextPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNextPage.Click
        Response.Redirect("default.01.aspx")

    End Sub

End Class