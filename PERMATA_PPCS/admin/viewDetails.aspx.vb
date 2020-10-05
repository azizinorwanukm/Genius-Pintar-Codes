Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class viewDetails
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

    Dim oDes As New Simple3Des("p@ssw0rd1")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btndelete.Attributes.Add("onclick", "return confirm('Pasti hendak menghapuskan Akaun tersebut?');")

        Try
            strppcsuserid = Request.QueryString("ppcsuserid")
            If Not IsPostBack Then
                PPCS_UserType_list()

                examyear_list()
                ddlUsersYear.Text = oCommon.getAppsettings("DefaultExamYear")

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                Load_Details()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            ' oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub

    Private Sub PPCS_UserType_list()
        strSQL = "SELECT UserType FROM master_PPCS_UserType ORDER BY UserType ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUserType.DataSource = ds
            ddlUserType.DataTextField = "UserType"
            ddlUserType.DataValueField = "UserType"
            ddlUserType.DataBind()

            'ddlUserType.Items.Add(New ListItem("ALL", "ALL"))
        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub examyear_list()
        strSQL = "SELECT ExamYear FROM master_examyear ORDER BY ExamYear ASC"

        '--debug
        'Response.Write("examyear_list:" & strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUsersYear.DataSource = ds
            ddlUsersYear.DataTextField = "ExamYear"
            ddlUsersYear.DataValueField = "ExamYear"
            ddlUsersYear.DataBind()

            'ddlUsersYear.Items.Add(New ListItem("ALL", "ALL"))
        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        strSQL = oCommon.PPCSDate_Query(Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value))

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

            'ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Load_Details()
        strSQL = "SELECT * FROM PPCS_Users a,PPCS_Users_Year b WHERE a.myGUID=b.myGUID AND a.ppcsuserid=" & strppcsuserid

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    txtLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    txtLoginID.Text = ""
                End If
                ''--to compare changes
                lblLoginID.Text = txtLoginID.Text

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    txtPwd.Text = ds.Tables(0).Rows(0).Item("Pwd")
                    txtPwdVerify.Text = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    txtPwd.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UserType")) Then
                    ddlUserType.Text = ds.Tables(0).Rows(0).Item("UserType")
                Else
                    ddlUserType.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    txtFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    txtFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Address")) Then
                    txtAddress.Text = ds.Tables(0).Rows(0).Item("Address")
                Else
                    txtAddress.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContactNo")) Then
                    txtContactNo.Text = ds.Tables(0).Rows(0).Item("ContactNo")
                Else
                    txtContactNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ICNo")) Then
                    txtICNo.Text = ds.Tables(0).Rows(0).Item("ICNo")
                Else
                    txtICNo.Text = ""
                End If

                '--year and ppcsdate
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UsersYear")) Then
                    ddlUsersYear.Text = ds.Tables(0).Rows(0).Item("UsersYear")
                Else
                    ddlUsersYear.Text = ""
                End If
                lblUsersYear.Text = ddlUsersYear.Text

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSDate")) Then
                    ddlPPCSDate.Text = ds.Tables(0).Rows(0).Item("PPCSDate")
                Else
                    ddlPPCSDate.Text = ""
                End If
                lblPPCSDate.Text = ddlPPCSDate.Text


            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btndelete.Click
        Dim strmyGUID As String = ""
        strSQL = "SELECT myGUID FROM PPCS_Users WHERE ppcsuserid=" & strppcsuserid
        strmyGUID = oCommon.getFieldValue(strSQL)

        strRet = "0"
        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using connection As New SqlConnection(strconn)
            connection.Open()

            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction("TxnStart")

            ' Must assign both transaction object and connection 
            ' to Command object for a pending local transaction.
            command.Connection = connection
            command.Transaction = transaction

            Try
                strSQL = "DELETE FROM ppcs_users WHERE ppcsuserid=" & strppcsuserid
                command.CommandText = strSQL
                command.ExecuteNonQuery()
                '--debug
                'Response.Write(strSQL)

                strSQL = "DELETE FROM PPCS_Users_Year WHERE myGUID=" & strmyGUID
                command.CommandText = strSQL
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                '--Console.WriteLine("Both records are written to database.")

                lblMsg.Text = "Berjaya MENGHAPUSKAN Pengguna PPCS."
            Catch ex As Exception
                'Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
                'Console.WriteLine("  Message: {0}", ex.Message)
                strRet = ex.Message

                ' Attempt to roll back the transaction. 
                Try
                    transaction.Rollback()

                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    'Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    'Console.WriteLine("  Message: {0}", ex2.Message)

                    strRet = "Rollback:" & ex2.Message

                End Try
            End Try
        End Using

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            '--change made on loginid
            If Not lblLoginID.Text = txtLoginID.Text Then
                strSQL = "SELECT LoginID FROM ppcs_users WHERE LoginID='" & txtLoginID.Text & "'"
                If oCommon.isExist(strSQL) = True Then
                    lblMsg.Text = "Login ID/Email already exist. Please use different Login ID."
                    Exit Sub
                End If
            End If

            '--update existing profile
            strSQL = "UPDATE PPCS_Users WITH (UPDLOCK) SET LoginID='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "',Email='" & oCommon.FixSingleQuotes(txtLoginID.Text) & "',Pwd='" & oDes.EncryptData(oCommon.FixSingleQuotes(txtPwd.Text)) & "',Fullname='" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "',ICNo='" & oCommon.FixSingleQuotes(txtICNo.Text) & "',ContactNo='" & oCommon.FixSingleQuotes(txtContactNo.Text) & "',Address='" & oCommon.FixSingleQuotes(txtAddress.Text.ToUpper) & "',UserType='" & ddlUserType.Text & "' WHERE ppcsuserid=" & strppcsuserid
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "error:" & strRet
            Else
                lblMsg.Text = "Berjaya dikemaskini."
            End If

            '--changes made on year
            If Not lblUsersYear.Text = ddlUsersYear.Text Then

            End If

            '--changes made on ppcsdate
            If Not lblPPCSDate.Text = ddlPPCSDate.Text Then

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

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean
        ''strSQL = "SELECT LoginID FROM ppcs_users WHERE LoginID='" & txtLoginID.Text & "'"
        ''If oCommon.isExist(strSQL) = True Then
        ''    lblMsg.Text = "Login ID sudah ujud. Masukkan Login ID yang lain."
        ''    txtLoginID.Focus()
        ''    Return False
        ''End If

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nama."
            txtFullname.Focus()
            Return False
        End If

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "Email perlu di isi."
            txtLoginID.Focus()
            Return False
        End If

        If txtICNo.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor IC."
            txtICNo.Focus()
            Return False
        End If


        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "Masukkan password."
            txtPwd.Focus()
            Return False
        End If

        If Not txtPwd.Text = txtPwdVerify.Text Then
            lblMsg.Text = "Password dimasukkan tidak sama."
            txtPwd.Focus()
            Return False

        End If

        If txtContactNo.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor telefon."
            txtContactNo.Focus()
            Return False
        End If

        If txtAddress.Text.Length = 0 Then
            lblMsg.Text = "Masukkan alamat terkini."
            txtAddress.Focus()
            Return False
        End If

        Return True
    End Function

End Class