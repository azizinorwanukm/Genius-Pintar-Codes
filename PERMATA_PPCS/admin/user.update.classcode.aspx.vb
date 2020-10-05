Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class user_update_classcode
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strppcsuserid As String = ""
    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ''--get CourseNameBM
                strSQL = "SELECT ClassNameBM FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
                lblClassNameBM.Text = oCommon.getFieldValue(strSQL)

                ''--get PPCSDate
                strSQL = "SELECT PPCSDate FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
                lblPPCSDate.Text = oCommon.getFieldValue(strSQL)

                ''--usertype
                lblUserType.Text = Request.QueryString("usertype")

            End If

        Catch ex As Exception
            'lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            ' oCommon.WriteLogFile(strPath, strMsg)

        End Try

    End Sub


    Private Sub Update_ppc_users()
        Try
            '--PPCS_Users Fullname
            strSQL = "SELECT Fullname FROM PPCS_Users WHERE myGUID='" & Request.QueryString("myguid") & "'"
            Dim strFullname As String = oCommon.getFieldValue(strSQL)

            Select Case lblUserType.Text.Trim
                Case "PENGAJAR"
                    ''--
                    strSQL = "UPDATE PPCS_Class SET Pengajar='" & Request.QueryString("myguid") & "',NamaPengajar='" & oCommon.FixSingleQuotes(strFullname) & "' WHERE ClassID=" & Request.QueryString("classid")
                    strRet = oCommon.ExecuteSQL(strSQL)

                Case "PEMBANTU PENGAJAR"
                    strSQL = "UPDATE PPCS_Class SET PembantuPengajar='" & Request.QueryString("myguid") & "',NamaPembantuPengajar='" & oCommon.FixSingleQuotes(strFullname) & "' WHERE ClassID=" & Request.QueryString("classid")
                    strRet = oCommon.ExecuteSQL(strSQL)

                Case "PENGURUS PELAJAR"
                    strSQL = "UPDATE PPCS_Class SET PengurusPelajar='" & Request.QueryString("myguid") & "',NamaPengurusPelajar='" & oCommon.FixSingleQuotes(strFullname) & "' WHERE ClassID=" & Request.QueryString("classid")
                    strRet = oCommon.ExecuteSQL(strSQL)

                Case "PEMBANTU PELAJAR"
                    strSQL = "UPDATE PPCS_Class SET PembantuPelajar='" & Request.QueryString("myguid") & "',NamaPembantuPelajar='" & oCommon.FixSingleQuotes(strFullname) & "' WHERE ClassID=" & Request.QueryString("classid")
                    strRet = oCommon.ExecuteSQL(strSQL)

                Case Else
                    strRet = "User Type not exist!"
            End Select

            If strRet = "0" Then
                lblMsg.Text = "Berjaya menentukan " & lblUserType.Text & " untuk kelas " & lblClassNameBM.Text
            Else
                lblMsg.Text = "System Error:" & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("class.list.aspx?usertype=" & lblUserType.Text)

        ''http://localhost/ppcs/admin/class.list.aspx?usertype=PENGAJAR

    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Update_ppc_users()

    End Sub
End Class