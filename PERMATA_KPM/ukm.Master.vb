Public Partial Class ukm
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--debug
                Dim strkpmadmin_loginid As String = CType(Session.Item("kpmadmin_loginid"), String)
                If strkpmadmin_loginid = "" Then
                    Response.Redirect("system.error.aspx?msg=You have logout from other browser or window. Please login again.")
                End If

            End If

        Catch ex As Exception
            lblFooterMsg.Text = "System error. Please contact admin. Err:" & ex.Message

        End Try

    End Sub

End Class