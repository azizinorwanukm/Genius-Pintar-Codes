Imports System.Data.SqlClient

Public Class upsi_intro
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsNothing(Session("UserICNo")) Then Response.Redirect("~/default.aspx")

        'timebomb
        'Dim dt As Date = Date.Parse("2015-11-30")

        'If Now.Date > dt Then Response.Redirect("~/default.aspx")

        If Not Page.IsPostBack Then

            LoadState()

            If Session("UserICNo").ToString <> "" Then

                Session.Timeout = Convert.ToInt16(modFunction.sessionTimeout)

                txtMyKidNo.Text = Session("UserICNo").ToString
                GetUserInfo(Session("UserICNo").ToString)
            End If

            strSQL = "  select B.icno from pcis_exam A
                        left join pcis_user B on A.userid = B.id
                        where A.test_end is not null
                        and A.test_end like '%" & Now.Year & "%' 
                        and B.icno = '" & Session("UserICNo") & "'"
            strRet = getFieldValue(strSQL)

            If strRet.Length > 0 Then
                Response.Redirect("~/upsi.certificate.aspx")
            End If

        End If

    End Sub

    Public Function getFieldValue(ByVal strSQL As String) As String
        If strSQL.Length = 0 Then
            Return ""
        End If

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim strFieldValue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strFieldValue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return ""
                End If
            End If

        Catch ex As Exception
            Return "*System error (Contact system admin): " & ex.Message
        Finally
            objConn.Dispose()
        End Try

        Return strFieldValue
    End Function

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        If UpdateUser() Then

            Session("AssistantName") = txtAssistantName.Text
            Session("AssistantPhoneNo") = txtAssistantPhoneNo.Text
            Session("UserName") = txtName.Text
            Session("LearnCentreName") = txtLearningCentreName.Text
            Session("LearnCentreState") = dlstLearningCentreState.SelectedItem.Text

            Response.Redirect(Session("UserPage").ToString)

        End If


    End Sub

    Private Sub LoadState()

        Dim dt As DataTable = New DataTable

        Try

            Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

                Con.Open()

                Using Comm As SqlCommand = New SqlCommand("spState", Con)

                    Comm.CommandType = CommandType.StoredProcedure
                    Comm.Parameters.AddWithValue("operation", "select")

                    Using ds As SqlDataReader = Comm.ExecuteReader()

                        dt.Load(ds)
                        ds.Close()

                    End Using

                    dlstLearningCentreState.DataSource = dt
                    dlstLearningCentreState.DataBind()

                End Using

                Con.Close()

                '--add default value
                If Session("Language") = "BM" Then
                    dlstLearningCentreState.Items.Insert(0, New ListItem("-- Pilih Negeri --", "NA"))
                Else
                    dlstLearningCentreState.Items.Insert(0, New ListItem("-- Select State --", "NA"))
                End If

            End Using

        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub GetUserInfo(UserICNo As String)

        Try

            Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

                Con.Open()

                'user
                Using Comm As SqlCommand = New SqlCommand("spUser", Con)
                    Comm.CommandType = CommandType.StoredProcedure

                    Comm.Parameters.AddWithValue("operation", "select")
                    Comm.Parameters.AddWithValue("icno", UserICNo)

                    Using ds As SqlDataReader = Comm.ExecuteReader

                        If ds.Read Then

                            Session("UserId") = ds("Id").ToString
                            txtName.Text = ds("fullname").ToString
                            'Session("DOB") = ds("tarikhlahir").ToString
                            txtAddress.Text = ds("address").ToString
                            txtPhone.Text = ds("phoneno").ToString
                            txtEmail.Text = ds("email").ToString
                            txtMotherName.Text = ds("mothername").ToString
                            txtFatherName.Text = ds("fathername").ToString
                            txtMotherOccupation.Text = ds("motheroccupation").ToString
                            txtFatherOccupation.Text = ds("fatheroccupation").ToString
                            txtLearningCentreName.Text = ds("learningcentrename").ToString
                            txtLearningCentreAddress.Text = ds("learningcentreaddress").ToString
                            dlstLearningCentreState.SelectedValue = ds("learningcentrestateid").ToString
                            txtLearningCentrePhoneNo.Text = ds("learningcentrephoneno").ToString
                            'txtAssistantName.Text = ds("assistantname").ToString
                            'txtAssistantPhoneNo.Text = ds("assistantphoneno").ToString

                            Session("RoleId") = ds("accesslevel").ToString

                            Session("UserICNo") = txtMyKidNo.Text
                            Session("UserName") = txtName.Text
                            Session("LearnCentreName") = txtLearningCentreName.Text
                            Session("LearnCentreState") = dlstLearningCentreState.SelectedItem.Text

                            'Session("AssistantName") = txtAssistantName.Text
                            'Session("AssistantPhoneNo") = txtAssistantPhoneNo.Text


                        End If

                        ds.Close()

                    End Using

                End Using


                'exam
                If (Session("UserId").ToString()) <> "" Then

                    Using Comm As SqlCommand = New SqlCommand("spExam", Con)
                        Comm.CommandType = CommandType.StoredProcedure

                        Comm.Parameters.AddWithValue("operation", "select")
                        Comm.Parameters.AddWithValue("userid", Session("UserId").ToString)

                        Using ds As SqlDataReader = Comm.ExecuteReader

                            If ds.Read Then

                                Session("ExamId") = ds("Id").ToString
                                Session("UserPage") = ds("CurrentPage").ToString
                                Session("TimeLeft") = CInt(ds("TimeLeft"))
                                Session("ZeroMark") = CInt(ds("ZeroMark"))

                                txtAssistantName.Text = ds("assistantname").ToString
                                txtAssistantPhoneNo.Text = ds("assistantphoneno").ToString

                                Session("AssistantName") = txtAssistantName.Text
                                Session("AssistantPhoneNo") = txtAssistantPhoneNo.Text

                            End If

                        End Using

                    End Using

                End If

                Con.Close()

            End Using

        Catch ex As Exception
            LogError("upsi.intro.aspx - GetUserInfo", ex.Message + " " + ex.StackTrace)
        End Try
        
    End Sub

    Private Function Validation()

        If txtMyKidNo.Text = "" And txtName.Text = "" And txtAddress.Text = "" And txtPhone.Text = "" And txtEmail.Text = "" And txtLearningCentreName.Text = "" And txtLearningCentreAddress.Text = "" And txtLearningCentrePhoneNo.Text = "" And txtAssistantName.Text = "" And txtAssistantPhoneNo.Text = "" And txtFatherName.Text = "" And txtFatherOccupation.Text = "" And txtMotherName.Text = "" And txtMotherOccupation.Text = "" Then

            If Session("Language") = "BM" Then
                ScriptManager.RegisterStartupScript(btnSubmit, GetType(String), "alert", "$('#lblalert').html('Sila isikan semua rekod yang bertanda *').show();", True)
            ElseIf Session("Language") = "BI" Then
                ScriptManager.RegisterStartupScript(btnSubmit, GetType(String), "alert", "$('#lblalert').html('Please fill in all record marked with *').show();", True)
            End If
            Return False
        End If

        If dlstLearningCentreState.SelectedValue = "NA" Then
            If Session("Language") = "BM" Then
                ScriptManager.RegisterStartupScript(btnSubmit, GetType(String), "alert", "$('#lblalert').html('Sila isikan semua rekod yang bertanda *').show();", True)
            ElseIf Session("Language") = "BI" Then
                ScriptManager.RegisterStartupScript(btnSubmit, GetType(String), "alert", "$('#lblalert').html('Please fill in all record marked with *').show();", True)
            End If
            Return False
        End If

        Dim oRegex As New RegexUtilities
        If oRegex.IsValidEmail(txtEmail.Text) = False Then
            If Session("Language") = "BM" Then
                ScriptManager.RegisterStartupScript(btnSubmit, GetType(String), "alert", "$('#lblalert').html('Bukan email format!').show();", True)
            ElseIf Session("Language") = "BI" Then
                ScriptManager.RegisterStartupScript(btnSubmit, GetType(String), "alert", "$('#lblalert').html('Invalid email format!').show();", True)
            End If
            Return False
        End If

        If chkSetuju.Checked = False Then

            If Session("Language") = "BM" Then
                ScriptManager.RegisterStartupScript(btnSubmit, GetType(String), "alert", "$('#lblalert').html('Sila klik kotak semak di atas untuk meneruskan ujian ini.').show();", True)
            ElseIf Session("Language") = "BI" Then
                ScriptManager.RegisterStartupScript(btnSubmit, GetType(String), "alert", "$('#lblalert').html('Please click on the checkbox above to proceed.').show();", True)
            End If
            Return False
        End If



        Return True

    End Function


    Private Function UpdateUser() As Boolean

        Dim operation As String = "add"

        If Session("UserId") <> "" Then operation = "edit"

        Try

            If Validation() Then

                Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

                    Con.Open()

                    'user
                    Using Comm As SqlCommand = New SqlCommand("spUser", Con)

                        Comm.CommandType = CommandType.StoredProcedure

                        Comm.Parameters.AddWithValue("operation", operation)
                        If operation = "edit" Then Comm.Parameters.AddWithValue("id", Session("UserId").ToString)
                        Comm.Parameters.AddWithValue("icno", txtMyKidNo.Text)
                        Comm.Parameters.AddWithValue("fullname", txtName.Text)
                        Comm.Parameters.AddWithValue("address", txtAddress.Text)
                        Comm.Parameters.AddWithValue("phoneno", txtPhone.Text)
                        Comm.Parameters.AddWithValue("email", txtEmail.Text)
                        Comm.Parameters.AddWithValue("mothername", txtMotherName.Text)
                        Comm.Parameters.AddWithValue("fathername", txtFatherName.Text)
                        Comm.Parameters.AddWithValue("motheroccupation", txtMotherOccupation.Text)
                        Comm.Parameters.AddWithValue("fatheroccupation", txtFatherOccupation.Text)
                        Comm.Parameters.AddWithValue("learningcentrename", txtLearningCentreName.Text)
                        Comm.Parameters.AddWithValue("learningcentreaddress", txtLearningCentreAddress.Text)
                        Comm.Parameters.AddWithValue("learningcentrestateid", dlstLearningCentreState.SelectedValue)
                        Comm.Parameters.AddWithValue("learningcentrephoneno", txtLearningCentrePhoneNo.Text)

                        Using ds As SqlDataReader = Comm.ExecuteReader

                            If ds.Read Then
                                Session("UserId") = ds("Id").ToString
                            End If

                            ds.Close()

                        End Using

                    End Using

                    operation = "add"

                    If Session("ExamId") <> "" Then operation = "edit"

                    'exam
                    Using Comm As SqlCommand = New SqlCommand("spExam", Con)

                        Comm.CommandType = CommandType.StoredProcedure
                        Comm.Parameters.AddWithValue("operation", operation)
                        Comm.Parameters.AddWithValue("userid", Session("UserId").ToString)
                        Comm.Parameters.AddWithValue("assistantname", txtAssistantName.Text)
                        Comm.Parameters.AddWithValue("assistantphoneno", txtAssistantPhoneNo.Text)

                        Using ds As SqlDataReader = Comm.ExecuteReader

                            If ds.Read Then

                                Session("ExamId") = ds("Id").ToString
                                Session("UserPage") = ds("CurrentPage").ToString
                                Session("TimeLeft") = CInt(ds("TimeLeft"))
                                Session("ZeroMark") = CInt(ds("ZeroMark"))

                            End If

                            ds.Close()

                        End Using

                    End Using

                    Con.Close()

                End Using

                Return True

            Else

                Return False

            End If

        Catch ex As Exception
            LogError("upsi.intro.aspx - UpdateUser", ex.Message + " " + ex.StackTrace)
            Return False
        End Try

    End Function

End Class