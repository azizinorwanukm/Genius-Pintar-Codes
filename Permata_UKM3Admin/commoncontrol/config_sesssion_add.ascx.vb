Imports System.Data.SqlClient

Public Class config_sesssion_add
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_add_session(sender As Object, e As EventArgs) Handles btn_tambahsession.Click

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "INSERT INTO UKM3Session (sessionName,ukm3Year) VALUES (@sessionName,@ukm3year)"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mCmd.Parameters.Add(New SqlParameter("@sessionName", txt_namaSession.Text))
                mCmd.Parameters.Add(New SqlParameter("@ukm3year", txtYear.Text))
                mConn.Open()
                mCmd.ExecuteNonQuery()
            End Using
        End Using

        Response.Redirect("admin.session_config.aspx")

    End Sub

End Class