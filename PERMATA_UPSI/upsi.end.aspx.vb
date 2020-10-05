
Imports System.Web.Configuration
Imports System.Data.SqlClient

Public Class upsi_end
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Session.Abandon()

            Try

                Dim IsUnderMaintenance As String = WebConfigurationManager.AppSettings("IsUnderMaintenance").ToString()

                If IsUnderMaintenance = "1" Then Response.Redirect("upsi.maintenance.aspx")

                Dim obj As Object
                Dim dtime As DateTime = DateTime.MaxValue

                Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

                    Con.Open()

                    Using Comm As SqlCommand = New SqlCommand("SELECT dbo.fnGetDateEnd()", Con)

                        obj = Comm.ExecuteScalar()

                        If Not IsDBNull(obj) Then dtime = Convert.ToDateTime(obj)

                    End Using

                    Con.Close()

                End Using

                If Now < dtime Then Response.Redirect("default.aspx")

            Catch ex As Exception

            End Try

        End If

    End Sub

End Class