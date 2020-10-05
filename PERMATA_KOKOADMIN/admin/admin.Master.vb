Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class admin
    Inherits System.Web.UI.MasterPage

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strUsername As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Session("koko_loginid") = "admin"

                Response.Cookies("kokoadmin_usertype").Value = "ADMIN"

                '--set default language BM
                Response.Cookies("koko_culture").Value = "ms-MY"

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                '--security check
                Dim strkoko_loginid As String = CType(Session.Item("koko_loginid"), String)
                If strkoko_loginid = "" Then
                    Response.Redirect("../default.timeout.aspx")
                End If

                strUsername = CType(Session.Item("koko_loginid"), String)
                lblUsername.Text = strUsername & "|" & getFullname()

                '--display label
                lblDate.Text = Now.Date.ToString("dddd dd-MM-yyyy")
                lblUserType.Text = Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
            End If

        Catch ex As Exception
            lblFooterMsg.Text = ex.Message
        End Try

    End Sub

    '--temporarily not use
    Private Function get_koko_user_usertype() As String
        Dim tmpSQL As String = "SELECT UserType FROM koko_user WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("koko_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function

    Private Function getFullname() As String
        Dim strFullname As String = ""

        strSQL = "SELECT Fullname FROM koko_user WHERE LoginID='" & CType(Session.Item("koko_loginid"), String) & "'"
        strFullname = oCommon.getFieldValue(strSQL)

        Return strFullname
    End Function


End Class