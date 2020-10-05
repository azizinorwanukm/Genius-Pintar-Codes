Imports System
Imports System.Threading
Imports System.Web.UI
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Partial Public Class addPembantuPelajar
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                ClearScreen()

                Dim cmd As SqlCommand
                Dim dr As SqlDataReader
                Dim strcourseCode As String

                Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
                Dim con As SqlConnection = New SqlConnection(strConn)

                'GET COURSE NAME
                cmd = New SqlCommand("select courseName from ukm2_course", con)
                con.Open()

                dr = cmd.ExecuteReader()
                While dr.Read()

                    If dr(0).ToString().Length > 0 Then

                        txtcourse.Items.Add(dr(0).ToString())
                        ' Response.Write(dr(0).ToString())
                    End If
                End While

                con.Close()

                'GET COURSE CODE
                strSQL = "SELECT courseCode From ukm2_course where courseName='" & txtcourse.Text & "'"
                strcourseCode = oCommon.getFieldValue(strSQL)

                txtcoursecode.Text = strcourseCode
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

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtfullname.Text = ""
        txtaddress.Text = ""
        txtcontactno.Text = ""
        txtemail.Text = ""
        txtIC.Text = ""
        txtpwd.Text = ""

        lblMsg.Text = ""

        lblTotal.Text = ""

        strSQL = "SELECT * from ukm2_login WHERE usertype='PEMBANTU PELAJAR' ORDER BY userid desc"
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Record not found!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        ClearScreen()

        nPageno = e.NewPageIndex + 1


    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        '--Response.Write(strKeyID)
        Response.Redirect("viewPembantuPelajarDetails.aspx?loginid=" & strKeyID)

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            Dim strdatecreated As String = Now.ToString("yyyyMMdd HH:mm:ss.fff")
            Dim strusername As String
            Dim strtxnref As String = oCommon.gettxnref
            strusername = Session("username")

            '--upload scanned image document
            Dim imageInfo As FileInfo = New FileInfo(txtFileupload.Value.Trim)
            Dim strImageFilename As String = strtxnref & imageInfo.Extension

            Select Case (imageInfo.Extension.ToUpper())
                Case ".JPG"
                    If UploadImg(strtxnref, imageInfo.Extension) = False Then
                        Exit Sub
                    End If
                Case ".GIF"
                    If UploadImg(strtxnref, imageInfo.Extension) = False Then
                        Exit Sub
                    End If
                Case ".BMP"
                    If UploadImg(strtxnref, imageInfo.Extension) = False Then
                        Exit Sub
                    End If
                Case Else
                    lblScan.Text = "Invalid file type. Only JPG, GIF and BMP"
                    Exit Sub
            End Select

            'insert in to user, not active
            strSQL = "INSERT INTO ukm2_login(loginid,pwd,fullname,ICnumber,contactno,courseName,courseCode,usertype,address,className,ImageFilename) VALUES ('" & txtemail.Text & "','" & txtpwd.Text & "','" & txtfullname.Text & "','" & txtIC.Text & "','" & txtcontactno.Text & "','" & txtcourse.Text & "','" & txtcoursecode.Text & "','PENGAJAR','" & txtaddress.Text & "','" & txtclass.Text & "','" & strImageFilename & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "error:" & strRet

            Else
                lblMsg.Text = "Berjaya."
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            'oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    Private Sub DeleteFile(ByVal strFileName As String)

        If strFileName.Trim().Length > 0 Then
            Dim fi As New FileInfo(strFileName)
            If (fi.Exists) Then    'if file exists, delete it
                fi.Delete()
            End If
        End If

    End Sub

    Private Function UploadImg(ByVal strSaveFilename As String, ByVal strFileExt As String) As Boolean
        Dim sFileDir As String = Server.MapPath("..") & "\resume_img\"
        Dim lMaxFileSize As Long = 500000     'Bytes or 500KB
        Dim strSaveMe As String = sFileDir + strSaveFilename + strFileExt

        If (Not txtFileupload.PostedFile Is Nothing) _
           And (txtFileupload.PostedFile.ContentLength > 0) Then
            'Determine file name
            Dim sFileName As String = _
               System.IO.Path.GetFileName(txtFileupload.PostedFile.FileName)
            Try
                If txtFileupload.PostedFile.ContentLength <= lMaxFileSize Then
                    'save file on disk
                    txtFileupload.PostedFile.SaveAs(strSaveMe)
                    Return True
                Else
                    'reject file
                    lblScan.Text = "File size:" & txtFileupload.PostedFile.ContentLength.ToString & ". File Size if Over the Limit of " & lMaxFileSize.ToString
                    Return False
                End If
            Catch exc As Exception    'in case of an error
                lblScan.Text = "An Error Occured." & exc.Message & ". Please Try Again!"

                '--write to file
                Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
                Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & exc.Message
                Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
                oCommon.WriteLogFile(strPath, strMsg)
                'DeleteFile(strSaveMe)
                Return False
            End Try
        Else
            lblScan.Text = "Nothing to upload. Please Try Again!"
            Return False
        End If

    End Function

    Private Sub UpLoadImageFile(ByVal info As String, ByVal strtxnref As String)
        Dim objSQLConn As SqlConnection
        Dim objSQLcmd As SqlCommand

        objSQLConn = New SqlConnection(strConn)
        objSQLcmd = New SqlCommand("insert into ukm2_resume_img(resumeing)values(@resumeimg)", objSQLConn)

        Try
            Dim imagestream As FileStream = New FileStream(info, FileMode.Open)
            Dim data() As Byte
            ReDim data(imagestream.Length - 1)
            imagestream.Read(data, 0, imagestream.Length)
            imagestream.Close()

            ''--ref no
            'Dim txnrefParameter As SqlParameter = New SqlParameter("@txnref", SqlDbType.NVarChar)
            'txnrefParameter.Value = strtxnref
            'objSQLcmd.Parameters.Add(txnrefParameter)

            '--image
            Dim pictureParameter As SqlParameter = New SqlParameter("@resumeimg", SqlDbType.Image)
            pictureParameter.Value = data
            objSQLcmd.Parameters.Add(pictureParameter)

            objSQLConn.Open()
            objSQLcmd.ExecuteNonQuery()
            objSQLConn.Close()

            lblScan.Text += "success"
        Catch ex As Exception
            '--Throw New Exception(ex.Message)
            lblScan.Text += ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objSQLConn.Dispose()
        End Try

    End Sub

    Private Sub ShowImage()
        Dim objConn As SqlConnection
        Try
            objConn = New SqlConnection(strConn)
            Dim objCom As SqlCommand = New SqlCommand("SELECT * FROM ukm2_resume_img ORDER BY resumeID DESC", objConn)
            objConn.Open()
            Dim MyReader As SqlDataReader = objCom.ExecuteReader(CommandBehavior.CloseConnection)
            If MyReader.HasRows = True Then
                MyReader.Read()
                Me.Response.ContentType = "text/HTML"
                'Dim Msg() As Byte
                'Msg = System.Text.Encoding.Default.GetBytes(MyReader("Picture").ToString())
                Me.Response.BinaryWrite(MyReader.Item("Picture"))
            Else
                '--RegisterClientScriptBlock("alertMsg", "<script>alert('No Image.');</script>")
            End If
            MyReader.Close()
            objConn.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtemail.Text.Length = 0 Then
            lblMsg.Text = "Email perlu di isi."
            txtemail.Focus()
            Return False
            Exit Function
        End If

        If txtpwd.Text.Length = 0 Then
            lblMsg.Text = "Masukkan password."
            txtpwd.Focus()
            Return False
            Exit Function
        End If

        If txtfullname.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nama."
            txtfullname.Focus()
            Return False
            Exit Function
        End If

        If txtaddress.Text.Length = 0 Then
            lblMsg.Text = "Designation cannot be blank."
            txtaddress.Focus()
            Return False
            Exit Function
        End If

        If txtcontactno.Text.Length = 0 Then
            lblMsg.Text = "Designation cannot be blank."
            txtcontactno.Focus()
            Return False
            Exit Function
        End If

        If txtIC.Text.Length = 0 Then
            lblMsg.Text = "Staffid cannot be blank."
            txtIC.Focus()
            Return False
            Exit Function
        End If

        If txtcourse.Text.Length = 0 Then
            lblMsg.Text = "Kursus cannot be blank."
            txtcourse.Focus()
            Return False
            Exit Function
        End If

        Return True
    End Function

    Protected Sub btnCode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCode.Click
        Dim strCode As String
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As SqlConnection = New SqlConnection(strConn)


        'GET COURSE CODE
        strSQL = "SELECT courseCode from ukm2_course WHERE courseName='" & txtcourse.Text & "'"
        strCode = oCommon.getFieldValue(strSQL)

        txtcoursecode.Text = strCode

        'GET CLASS
        cmd = New SqlCommand("select className from ukm2_class WHERE courseCode='" & txtcoursecode.Text & "'", con)
        con.Open()

        dr = cmd.ExecuteReader()
        While dr.Read()

            If dr(0).ToString().Length > 0 Then

                txtclass.Items.Add(dr(0).ToString())

            End If
        End While

        con.Close()
    End Sub
End Class