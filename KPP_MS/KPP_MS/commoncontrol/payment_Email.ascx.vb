Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail

Public Class payment_Email
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = ""
                id = Request.QueryString("admin_ID")

                student_Level()
                year_list()

                ''get a user access
                Dim userAccess As String = ""
                userAccess = "select staff_Position from staff_Info where stf_ID = '" & id & "'"
                Dim access As String = oCommon.getFieldValue(userAccess)
                hiddenAccess.Value = access

                load_page()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT year from student_Level where year ='" & Now.Year & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub student_Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevelnaming.DataSource = ds
            ddlLevelnaming.DataTextField = "Parameter"
            ddlLevelnaming.DataValueField = "Parameter"
            ddlLevelnaming.DataBind()
            ddlLevelnaming.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_info_list()
        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & ddlYear.SelectedValue & "' and class_type = 'Compulsory' and class_Level = '" & ddlLevelnaming.SelectedValue & "' order by class_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassnaming.DataSource = ds
            ddlClassnaming.DataTextField = "class_Name"
            ddlClassnaming.DataValueField = "class_ID"
            ddlClassnaming.DataBind()
            ddlClassnaming.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlClassnaming.Items.Insert(1, New ListItem("All", "All"))
            ddlClassnaming.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            If ddlLevelnaming.SelectedIndex > 0 Then
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevelnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
            class_info_list()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlClassnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrderby As String = ""

        strOrderby = " order by class_info.class_Name, student_info.student_Name ASC"

        tmpSQL = "select distinct student_info.std_ID, student_info.student_Name, student_info.student_Mykad, student_info.student_ID,  class_info.class_Level ,class_info.class_Name from student_info
                  left join course on student_info.std_ID = course.std_ID
                  left join class_info on course.class_ID = class_info.class_ID
                  where student_info.student_Status = 'Access'
                  and class_info.class_type = 'Compulsory'"

        strWhere = " and course.year = '" & ddlYear.SelectedValue & "' and class_info.class_year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and class_info.class_Level = '" & ddlLevelnaming.SelectedValue & "'"

        If ddlClassnaming.SelectedIndex > 1 Then
            strWhere += " and class_info.class_ID = '" & ddlClassnaming.SelectedValue & "'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            Dim student_ID As String = "Select student_ID from student_info where student_ID = '" & txtstudent.Text & "'"
            Dim get_student_ID As String = oCommon.getFieldValue(student_ID)

            Dim student_Name As String = "Select student_Name from student_info where student_Name = '" & txtstudent.Text & "'"
            Dim get_student_Name As String = oCommon.getFieldValue(student_Name)

            Dim student_Mykad As String = "Select student_Mykad from student_info where student_Mykad = '" & txtstudent.Text & "'"
            Dim get_student_Mykad As String = oCommon.getFieldValue(student_Mykad)

            If get_student_ID <> txtstudent.Text Then

                If get_student_Name <> txtstudent.Text Then

                    If get_student_Mykad <> txtstudent.Text Then
                        strWhere += " and student_info.student_Mykad = '" & txtstudent.Text & "'"
                    ElseIf get_student_Mykad = txtstudent.Text Then
                        strWhere += " and student_info.student_Mykad = '" & txtstudent.Text & "'"

                    End If
                ElseIf get_student_Name = txtstudent.Text Then
                    strWhere += " and student_info.student_Name like '%" & txtstudent.Text & "%'"

                End If
            ElseIf get_student_ID = txtstudent.Text Then
                strWhere += " and student_info.student_ID = '" & txtstudent.Text & "'"

            End If
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL

    End Function

    Private Sub BtnSendEmail_ServerClick(sender As Object, e As EventArgs) Handles BtnSendEmail.ServerClick

        Dim checking_error As Integer = 0
        Dim i As Integer = 0
        Dim errorCount As Integer = 0

        Dim get_email_subject As String = ""

        Dim filename As String = ""
        Dim imgPath As String = ""

        Dim get_date As String = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")

        Dim strDescription As String = Server.HtmlEncode(txtDescription.Content)

        If emailUpload.PostedFile.FileName <> "" Then
            filename = Path.GetFileName(emailUpload.PostedFile.FileName)

            ''sets the image path
            imgPath = "~/payment_Image/" + filename

            ''then save it to the Folder
            emailUpload.SaveAs(Server.MapPath(imgPath))
        End If

        ''save email conntent on database
        Using PJGDATA As New SqlCommand("insert into invoice_email(IM_Subject, IM_Content, IM_Attachment, IM_Date) values 
                                        ('" & txtsubject.Text & "','" & strDescription & "','" & imgPath & "','" & get_date & "')", objConn)
            objConn.Open()
            Dim k = PJGDATA.ExecuteNonQuery()
            objConn.Close()

            If k <> 0 Then
                errorCount = 0
            Else
                errorCount = 1
            End If
        End Using

        Dim SmtpServer As New SmtpClient("smtp.gmail.com", 587)
        Dim mail As New MailMessage()
        Dim attachment As System.Net.Mail.Attachment

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then

                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString

                If chkUpdate.Checked = True Then

                    SmtpServer.UseDefaultCredentials = False
                    SmtpServer.Credentials = New System.Net.NetworkCredential("azizinorwan@gmail.com", "mohdazizi93")
                    SmtpServer.EnableSsl = True

                    mail = New MailMessage
                    mail.From = New MailAddress("azizinorwan@gmail.com")
                    mail.To.Add("mohdazizinorwan@yahoo.com")
                    mail.Subject = txtsubject.Text
                    mail.Body = strDescription
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

                    attachment = New System.Net.Mail.Attachment(filename)
                    mail.Attachments.Add(attachment)

                    SmtpServer.Send(mail)

                End If
            End If
        Next

    End Sub
End Class