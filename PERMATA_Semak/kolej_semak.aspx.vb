Imports System.Data.SqlClient

Public Class kolej_semak
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If oCommon.getAppsettings("PPMTEnable") = "N" Then
                    Response.Redirect("default.aspx")
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSemak_Click(sender As Object, e As EventArgs) Handles btnSemak.Click

        If ValidatePage() = False Then
            divMsg.Attributes("class") = "warning"
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim run As String = ""

        ''Check Student Program
        tmpSQL = "Select UKM3.Program from StudentProfile"
        tmpSQL += " Left Join UKM3 on StudentProfile.StudentID = UKM3.StudentID"
        strWhere += " where StudentProfile.MYKAD ='" & txtMYKAD.Text & "' "
        strSQL = tmpSQL & strWhere

        run = oCommon.getFieldValue(strSQL)

        Response.Redirect("kolej.result.aspx?stdmykad=" + txtMYKAD.Text)

    End Sub

    Private Function ValidatePage() As Boolean
        If txtMYKAD.Text.Length = 0 Then
            txtMYKAD.Focus()
            lblMsg.Text = "Sila masukkan MYKAD/MYKID#!"
            Return False
        End If

        Return True
    End Function


End Class