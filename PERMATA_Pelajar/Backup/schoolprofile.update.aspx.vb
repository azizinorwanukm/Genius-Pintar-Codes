Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class schoolprofile_update1
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strSchoolID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ''--get parentid
        Dim strParentID As String
        strSQL = "SELECT ParentID FROM StudentProfile WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strParentID = oCommon.getFieldValue(strSQL)

        ''--get mothermykad
        Dim strMotherMYKADNo As String = ""
        strSQL = "SELECT MotherMYKADNo FROM ParentProfile WHERE ParentID='" & strParentID & "'"
        strMotherMYKADNo = oCommon.getFieldValue(strSQL)

        If strMotherMYKADNo.Length = 0 Then
            Response.Redirect("parentprofile.create.aspx?studentid=" & Request.QueryString("studentid") & "&mothermykadno=" & strMotherMYKADNo)
        Else
            Response.Redirect("parentprofile.update.aspx?studentid=" & Request.QueryString("studentid") & "&mothermykadno=" & strMotherMYKADNo)
        End If

    End Sub

    Private Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Response.Redirect("schoolprofile.search.change.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub
End Class