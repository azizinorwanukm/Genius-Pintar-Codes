Public Partial Class admin_schoolprofile_studentprofile_list
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnStudentSchoolUpdate_Click(sender As Object, e As EventArgs) Handles btnStudentSchoolUpdate.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("kpm.studentschool.schoolprofile.select.aspx?oldschoolid=" & Request.QueryString("schoolid") & "&examyear=" & Request.QueryString("examyear"))
            Case "ADMINOP"
                Response.Redirect("kpm.studentschool.schoolprofile.select.aspx?oldschoolid=" & Request.QueryString("schoolid") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                lblMsg.Text = "Tiada kebenaran!"
            Case Else
        End Select


    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class