Imports System.Data.SqlClient

Public Class upsi_detail
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            LoadState()

            If Session("UserICNo").ToString <> "" Then

                Session.Timeout = Convert.ToInt16(modFunction.sessionTimeout)

                txtMyKidNo.Text = Session("UserICNo").ToString
                GetUserInfo(Session("UserICNo").ToString)
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

                End Using

                Con.Close()

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
                            txtMotherName.Text = ds("mothername").ToString
                            txtFatherName.Text = ds("fathername").ToString

                            Session("RoleId") = ds("accesslevel").ToString

                            Session("UserICNo") = txtMyKidNo.Text
                            Session("UserName") = txtName.Text
                            Session("LearnCentreName") = ds("learningcentrename").ToString
                            Session("LearnCentreState") = ds("learningcentrestateid").ToString

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

                                Session("AssistantName") = ds("assistantname").ToString
                                Session("AssistantPhoneNo") = ds("assistantphoneno").ToString

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

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim strDOB As String
        strDOB = InputBox("Sila Masuk Katalaluan",
                 "PCIS",
                 "")

        Dim phoneno As String = "select phoneno from pcis_user where icno = '" & Session("UserICNo") & "' "
        Dim data_phoneno As String = getFieldValue(phoneno)

        If strDOB = data_phoneno Then
            Response.Redirect("~/upsi.intro.aspx")

        Else

            If strDOB.Length = 0 Then
                Response.Redirect("~/upsi.intro.aspx")
            Else
                strDOB = InputBox("Sila Masuk Katalaluan",
                 "PCIS",
                 "Sila Masukkan Katalaluan yang betul.")
            End If
        End If

    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Response.Redirect(Session("UserPage").ToString)
    End Sub
End Class