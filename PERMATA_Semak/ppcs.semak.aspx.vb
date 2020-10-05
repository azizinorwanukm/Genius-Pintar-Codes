Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ppcs_semak
    Inherits System.Web.UI.Page
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                If oCommon.getAppsettings("SEMAK_PPCS") = "N" Then
                    Response.Redirect("default.aspx")
                End If

                lblPPCSTitle.Text = oCommon.getAppsettings("PPCSTitle")
                lblTarikhTutup.Text = oCommon.getAppsettings("PPCSTarikhTutup")

                lblPPSCDateUKM.Text = "PPCS DIS 2017"
                lblPPSCDateUSIM.Text = "PPCS DIS 2017 (USIM)"
                lblPPCSStatus.Text = "LAYAK"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSemak_Click(sender As Object, e As EventArgs) Handles btnSemak.Click

        '--validate
        If ValidateForm() = False Then
            lblMsgTop.Text = lblMsg.Text
            Exit Sub
        End If

        'get studentid
        strSQL = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        lblStudentID.Text = oCommon.getFieldValue(strSQL)

        '--generate SQL
        strSQL = getSQL()
        '--lblDebug.Text = strSQL

        '--get ppcsdate
        Dim strPPCSDate As String = oCommon.getFieldValue(strSQL)
        Select Case strPPCSDate
            Case lblPPSCDateUSIM.Text
                Response.Redirect("ppcs.usim.aspx?studentid=" & lblStudentID.Text)
            Case lblPPSCDateUKM.Text
                Response.Redirect("ppcs.ukm.aspx?studentid=" & lblStudentID.Text)
            Case Else
                lblMsg.Text = "Anda tidak berjaya ke " & lblPPCSTitle.Text & ". Jangan putus asa, cuba lagi dan tingkatkan prestasi anda tahun hadapan."
        End Select

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        'tmpSQL = "SELECT PPCS.PPCSDate FROM PPCS,StudentProfile"
        'strWhere = " WHERE PPCS.StudentID=StudentProfile.StudentID"
        'strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        'strWhere += " AND PPCS.PPCSStatus='" & lblPPCSStatus.Text & "'"
        'strWhere += " AND (PPCS.PPCSDate='" & lblPPSCDateUKM.Text & "' OR PPCS.PPCSDate='" & lblPPSCDateUSIM.Text & "')"

        tmpSQL = "SELECT PPCSDate FROM PPCS"
        strWhere += " WHERE StudentID='" & lblStudentID.Text & "'"
        strWhere += " AND PPCSStatus='" & lblPPCSStatus.Text & "'"
        strWhere += " AND (PPCS.PPCSDate='" & lblPPSCDateUKM.Text & "' OR PPCS.PPCSDate='" & lblPPSCDateUSIM.Text & "')"

        getSQL = tmpSQL & strWhere & strOrder
        Return getSQL

    End Function

    Private Function ValidateForm() As Boolean
        If txtMYKAD.Text.Length = 0 Then
            txtMYKAD.Focus()
            lblMsg.Text = "Masukkan MYKAD/MYKID anda."
            Return False
        End If

        Return True
    End Function

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub
End Class