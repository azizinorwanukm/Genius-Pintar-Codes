Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class _default6
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LoadClass_info()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadClass_info()
        '--get classcode
        strSQL = "SELECT ClassCode FROM ppcs_class WHERE PembantuPengajar='" & Server.HtmlEncode(Request.Cookies("ppcs_myguid").Value) & "'"
        Response.Cookies("ppcs_classcode").Value = oCommon.getFieldValue(strSQL)

        '--get ClassNameBM
        strSQL = "SELECT ClassNameBM FROM ppcs_class WHERE PembantuPengajar='" & Server.HtmlEncode(Request.Cookies("ppcs_myguid").Value) & "'"
        Response.Cookies("ppcs_classnamebm").Value = oCommon.getFieldValue(strSQL)

    End Sub

End Class