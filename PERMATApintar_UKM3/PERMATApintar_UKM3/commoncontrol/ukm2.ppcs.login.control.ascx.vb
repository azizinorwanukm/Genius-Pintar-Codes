
Partial Public Class ukm2_ppcs_login_control
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strUsertype As String = ""
    Dim strmyGUID As String = ""
    Dim strClassID As String = ""
    Dim strClassCode As String = ""
    Dim strPPCSDate As String = ""

    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                setCookies()
                lblMsg.Text = "Masukkan Email dan Kata Laluan..."
            End If
        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Sub setCookies()
        Response.Cookies("ppcs_culture").Value = "ms-MY"
        Response.Cookies("ppcs_culture").Expires = DateTime.Now.AddDays(1)

        Response.Cookies("ppcs_loginid").Value = ""
        Response.Cookies("ppcs_loginid").Expires = DateTime.Now.AddDays(1)

        Response.Cookies("ppcs_myguid").Value = ""
        Response.Cookies("ppcs_myguid").Expires = DateTime.Now.AddDays(1)

        Response.Cookies("ppcs_usertype").Value = ""
        Response.Cookies("ppcs_usertype").Expires = DateTime.Now.AddDays(1)

        Response.Cookies("ppcs_classid").Value = ""
        Response.Cookies("ppcs_classid").Expires = DateTime.Now.AddDays(1)

        Response.Cookies("ppcs_classcode").Value = ""
        Response.Cookies("ppcs_classcode").Expires = DateTime.Now.AddDays(1)

        Response.Cookies("ppcs_ppcsdate").Value = ""
        Response.Cookies("ppcs_ppcsdate").Expires = DateTime.Now.AddDays(1)

        Response.Cookies("ppcs_classnamebm").Value = ""
        Response.Cookies("ppcs_classnamebm").Expires = DateTime.Now.AddDays(1)

    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            strSQL = "SELECT LoginID,Pwd FROM PPCS_Users WHERE isAllow='Y' AND LoginID='" & oCommon.FixSingleQuotes(txtemailid.Text) & "' AND Pwd='" & oDes.EncryptData(txtpwd.Text) & "'"
            ''--debug
            'Response.Write("strSQL:" & strSQL)
            If oCommon.isExist(strSQL) = True Then
                '--LoginID
                Response.Cookies("ppcs_loginid").Value = txtemailid.Text

                '--DefaultPPCSDate. get web.config
                Response.Cookies("ppcs_ppcsdate").Value = oCommon.getAppsettings("DefaultPPCSDate")
                strPPCSDate = oCommon.getAppsettings("DefaultPPCSDate")

                '--myGUID
                strSQL = "SELECT myGUID FROM PPCS_Users WHERE loginid='" & oCommon.FixSingleQuotes(txtemailid.Text) & "'"
                strmyGUID = oCommon.getFieldValue(strSQL)
                Response.Cookies("ppcs_myguid").Value = strmyGUID

                '--UserType
                strSQL = "SELECT b.UserType FROM PPCS_Users a,PPCS_Users_Year b WHERE a.myGUID=b.myGUID AND a.LoginID='" & txtemailid.Text & "' AND b.PPCSDate='" & strPPCSDate & "'"
                strUsertype = oCommon.getFieldValue(strSQL)
                Response.Cookies("ppcs_usertype").Value = strUsertype

                '--insert into security audit trail table
                oCommon.LogTrail(oCommon.FixSingleQuotes(txtemailid.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "PPCS_LOGIN", "NA")

                Select Case strUsertype
                    Case "ADMIN"
                        Response.Redirect("admin/default.aspx", False)

                    Case "KETUA PENGURUS AKADEMIK"
                        Response.Redirect("ketuapengurusakademik/default.aspx")

                    Case "PENGURUS AKADEMIK"
                        Response.Redirect("pengurusakademik/default.aspx")

                    Case "KETUA MODUL"
                        Response.Redirect("ketuamodul/default.aspx")

                    Case "PENGAJAR"
                        '--ClassID
                        strSQL = "SELECT ClassID FROM PPCS_Class WHERE Pengajar='" & strmyGUID & "' AND PPCSDate='" & strPPCSDate & "'"
                        strClassID = oCommon.getFieldValue(strSQL)
                        Response.Cookies("ppcs_classid").Value = strClassID
                        '--debug
                        'Response.Write("strClassID:" & strClassID)

                        '--ClassCode
                        strSQL = "SELECT ClassCode FROM PPCS_Class WHERE ClassID=" & strClassID
                        strClassCode = oCommon.getFieldValue(strSQL)
                        Response.Cookies("ppcs_classcode").Value = strClassCode

                        If strClassID = "" Then
                            lblMsg.Text = "KELAS belum ditempatkan untuk anda bagi sessi PPCS " & strPPCSDate & ". Sila hubungi System Admin."
                            Exit Sub
                        End If

                        Response.Redirect("pengajar/default.aspx")

                    Case "PEMBANTU PENGAJAR"
                        '--ClassID
                        strSQL = "SELECT ClassID FROM PPCS_Class WHERE PembantuPengajar='" & strmyGUID & "' AND PPCSDate='" & strPPCSDate & "'"
                        strClassID = oCommon.getFieldValue(strSQL)
                        Response.Cookies("ppcs_classid").Value = strClassID

                        '--ClassCode
                        strSQL = "SELECT ClassCode FROM PPCS_Class WHERE ClassID=" & strClassID
                        strClassCode = oCommon.getFieldValue(strSQL)
                        Response.Cookies("ppcs_classcode").Value = strClassCode

                        If strClassID = "" Then
                            lblMsg.Text = "KELAS belum ditempatkan untuk anda bagi sessi PPCS " & strPPCSDate & ". Sila hubungi System Admin."
                            Exit Sub
                        End If

                        ''move to default page
                        Response.Redirect("pembantupengajar/default.aspx")

                    Case "PENGURUS PELAJAR"
                        Response.Redirect("penguruspelajar/default.aspx")

                    Case "PEMBANTU PELAJAR"
                        '--ClassID
                        strSQL = "SELECT ClassID FROM PPCS_Class WHERE PembantuPelajar='" & strmyGUID & "' AND PPCSDate='" & strPPCSDate & "'"
                        strClassID = oCommon.getFieldValue(strSQL)
                        Response.Cookies("ppcs_classid").Value = strClassID

                        ''--view mingguan dan akhir sahaja
                        Response.Redirect("pembantupelajar/default.aspx")

                    Case "PENGURUS PEJABAT"
                        Response.Redirect("penguruspejabat/default.aspx")

                    Case Else
                        lblMsg.Text = "Invalid user type. Please contact system admin. " & strRet & ":" & strUsertype
                End Select
            Else
                '--insert into security audit trail table
                oCommon.LogTrail(oCommon.FixSingleQuotes(txtemailid.Text), oCommon.getNow, Request.UserHostAddress, Request.UserHostName, Request.Browser.Browser, "PPCS_LOGIN_FAILED", "NA")
                lblMsg.Text = "Tiada kebenaran, Email atau kata laluan salah. Sila masukkan semula."
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

End Class