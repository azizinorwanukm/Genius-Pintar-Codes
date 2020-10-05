Public Class default_011
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strTestID As String = Now.Year.ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If isExamEnd() = True Then
                Response.Redirect("default.end.aspx")
            End If

            Dim getExamYear As String = "Select configString from master_Config where configCode = 'UKM1ExamYear'"
            Dim ExamYear As String = oCommon.getFieldValue(getExamYear)

            Response.Cookies("studentid").Value = "NA"
            If Not IsPostBack Then
                Lbl04.Text = ExamYear
                lblPCInfo.Text = "IP Address: " & Request.UserHostAddress & " Hostname: " & Request.UserHostName & " Browser: " & Request.Browser.Browser & " Datetime:" & Now.ToString("yyyyMMdd HH:mm:ss.fff")
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function isExamEnd() As Boolean
        ''--exam END
        Dim strUKM1END As String = oCommon.getAppsettings("UKM1END")
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        If CInt(strToday) > CInt(strUKM1END) Then
            Return True
        End If

        Return False
    End Function

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim strCulture As String = "ms-MY"
            ''--language selected
            If BM.Checked = True Then
                strCulture = "ms-MY"
            Else
                strCulture = "en-US"
            End If

            ''--validate page
            If ValidatePage() = False Then
                Exit Sub
            End If

            ''--sqlinjection
            Dim strStudentid As String = oCommon.FixSingleQuotes(txtMYKAD.Text)
            If oCommon.CheckSqlInjection(strStudentid) = True Then
                Response.Redirect("ukm1.invalid.url.aspx?lang=" & Request.QueryString("lang"), False)
            End If

            ''--existing user and change password done
            strSQL = "SELECT MYKAD FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                Response.Redirect("default.main.aspx?lang=" & strCulture & "&studentid=" & getStudentID(), True)
            Else
                ''strip all spaces and special chars
                Dim strMYKAD As String = oCommon.StringStrip(txtMYKAD.Text)
                txtMYKAD.Text = strMYKAD.ToUpper

                Response.Redirect("studentprofile.create.aspx?lang=" & strCulture & "&mykad=" & oCommon.FixSingleQuotes(txtMYKAD.Text), True)
            End If

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. "

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":btnNext_click:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        ''--exam END
        Dim strUKM1END As String = oCommon.getAppsettings("UKM1END")
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        If CInt(strToday) > CInt(strUKM1END) Then
            lblMsg.Text = "Ujian UKM1 telah ditamatkan. Hari ini:" & strToday
            Return False
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. MYKAD/MYKID#"
            txtMYKAD.Focus()
            Return False
        End If

        '--Dr Siti request. MYKAD digit only. 20150409
        If oCommon.isNumeric(txtMYKAD.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja! [0 - 9]"
            txtMYKAD.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function getStudentID() As String
        ''--get studentID
        strSQL = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        ''set initial cookies value
        Response.Cookies("studentid").Value = strRet
        Return strRet

    End Function

    Private Function isValidYear() As Boolean
        Dim stryear = Mid(txtMYKAD.Text, 1, 2) & "|"
        Dim strValidYear As String = "96|97|98|99|00|01|02|03|04|05|06|07|08|09|10|11|12"

        If strValidYear.Contains(stryear) Then
            Return True
        Else
            Return False
        End If
    End Function


    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNext.Click
        Try
            Dim strCulture As String = "ms-MY"
            ''--language selected
            If BM.Checked = True Then
                strCulture = "ms-MY"
            Else
                strCulture = "en-US"
            End If

            ''--validate page
            If ValidatePage() = False Then
                Exit Sub
            End If

            ''--sqlinjection
            Dim strStudentid As String = oCommon.FixSingleQuotes(txtMYKAD.Text)
            If oCommon.CheckSqlInjection(strStudentid) = True Then
                Response.Redirect("ukm1.invalid.url.aspx?lang=" & Request.QueryString("lang"), False)
            End If

            ''--existing user and change password done
            strSQL = "SELECT MYKAD FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                Response.Redirect("default.main.aspx?lang=" & strCulture & "&studentid=" & getStudentID(), True)
            Else
                ''strip all spaces and special chars
                Dim strMYKAD As String = oCommon.StringStrip(txtMYKAD.Text)
                txtMYKAD.Text = strMYKAD.ToUpper

                Response.Redirect("studentprofile.create.aspx?lang=" & strCulture & "&mykad=" & oCommon.FixSingleQuotes(txtMYKAD.Text), True)
            End If

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. "

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":btnNext_click:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub

End Class