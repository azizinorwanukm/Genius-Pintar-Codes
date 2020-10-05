
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Public Class upsi_certificate

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If Not IsNothing(Session("UserICNo")) Then

                If Session("UserICNo").ToString <> "" Then
                    lblPCISYear.Text = Now.Year
                    LoadCertificateInfo(Session("ExamId").ToString)

                    'ScriptManager.RegisterStartupScript(Me, GetType(Label), "print", "$(document).ready(function () {window.print();window.close();});", True)

                End If

            End If

        End If
    End Sub

    Sub LoadCertificateInfo(ExamId As String)


        Try

            Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

                Con.Open()

                Using Com As SqlCommand = New SqlCommand("spExam", Con)

                    Com.CommandType = CommandType.StoredProcedure
                    Com.Parameters.AddWithValue("operation", "examtime")
                    Com.Parameters.AddWithValue("id", ExamId)

                    Using ds As SqlDataReader = Com.ExecuteReader

                        If (ds.Read()) Then

                            lblStart.Text = "Tarikh dan Masa Mula : " + ds("StartDate").ToString
                            lblEnd.Text = "Tarikh dan Masa Tamat : " + ds("EndDate").ToString

                        End If

                        ds.Close()

                    End Using

                End Using

                Con.Close()

            End Using

        Catch ex As Exception

        End Try

        lblName.Text = Session("UserName").ToString
        lblMyKid.Text = Session("UserICNo").ToString
        lblTaska.Text = Session("LearnCentreName").ToString
        lblState.Text = Session("LearnCentreState").ToString

    End Sub

End Class