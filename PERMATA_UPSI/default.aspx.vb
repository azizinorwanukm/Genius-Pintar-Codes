
Imports System.Web.Configuration
Imports System.Data.SqlClient

Public Class _default
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim age_min As String = "select configString from pcis_config where configCode = 'PCISAgeMin'"
            Dim data_age_min As String = getFieldValue(age_min)
            agemin.Text = data_age_min

            Dim age_max As String = "select configString from pcis_config where configCode = 'PCISAgeMax'"
            Dim data_age_max As String = getFieldValue(age_max)
            agemax.Text = data_age_max

            Dim get_startDate As String = "select TOP 1 (start_date) from pcis_exam_year order by id desc"
            Dim dis_startData As String = getFieldValue(get_startDate)

            Dim get_endDate As String = "select TOP 1 (end_date) from pcis_exam_year order by id desc"
            Dim dis_endData As String = getFieldValue(get_endDate)

            Dim datetime_startdate As DateTime
            Dim datetime_enddate As DateTime

            If dis_startData = "" Then
                txtstart_date.Text = " "
            Else
                datetime_startdate = Convert.ToDateTime(dis_startData)
                Dim start_date As String = datetime_startdate.ToString("dd MMM yyyy")
                txtstart_date.Text = start_date
            End If

            If dis_endData = "" Then
                txtend_date.Text = " "
            Else
                datetime_enddate = Convert.ToDateTime(dis_endData)
                Dim end_date As String = datetime_enddate.ToString("dd MMM yyyy")
                txtend_date.Text = end_date
            End If

            Dim Emaildate As String = "select configString from pcis_config where configCode = 'PCISEmailDate'"
            Dim get_data As String = getFieldValue(Emaildate)

            txtEmail.Text = get_data

            Session.Abandon()

            Try

                Dim IsUnderMaintenance As String = WebConfigurationManager.AppSettings("IsUnderMaintenance").ToString()

                If IsUnderMaintenance = "1" Then Response.Redirect("upsi.maintenance.aspx")

            Catch ex As Exception

            End Try

        End If

    End Sub

    Public Function getFieldValue(ByVal strSQL As String) As String
        If strSQL.Length = 0 Then
            Return ""
        End If
        'If isBlockText(strSQL) = True Then
        '    getFieldValue = "*Security alert (Contact system admin): IP address and SQL command logged."
        '    Exit Function
        'End If

        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
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
            Else
                Return ""
            End If

        Catch ex As Exception
            Return "*System error (Contact system admin): " & ex.Message
        Finally
            objConn.Dispose()
        End Try

        Return strFieldValue
    End Function

    Private Sub btnLogin_Command(sender As Object, e As CommandEventArgs) Handles btnLogin.Command
        Dim warning As String = ""


        Dim obj As Object
        Dim dtime As DateTime = DateTime.MaxValue
        Dim s As StringBuilder = New StringBuilder

        Using Con As SqlConnection = New SqlConnection(modFunction.conStr)

            Con.Open()

            Using Comm As SqlCommand = New SqlCommand("SELECT dbo.fnGetDateEnd()", Con)

                obj = Comm.ExecuteScalar()

                If Not IsDBNull(obj) Then dtime = Convert.ToDateTime(obj)

            End Using

            Con.Close()

        End Using


        Dim get_startDate As String = "select TOP 1 (start_date) from pcis_exam_year order by id desc"
        Dim dis_startData As String = getFieldValue(get_startDate)

        Dim get_endDate As String = "select TOP 1 (end_date) from pcis_exam_year order by id desc"
        Dim dis_endData As String = getFieldValue(get_endDate)

        Dim datetime_startdate As DateTime
        Dim datetime_enddate As DateTime
        Dim start_date As String = ""
        Dim end_date As String = ""

        If dis_startData = "" Then

            s.Append("$('#lblModalTitle').html('Perhatian');")
            s.Append("$('#lblModalInstruction').html('Sila masukkan tarikh mula peperiksaan');")
            ScriptManager.RegisterStartupScript(btnLogin, GetType(String), "x", s.ToString() & "$('#myModal').modal('show');", True)
            Return
        Else
            datetime_startdate = Convert.ToDateTime(dis_startData)
            start_date = datetime_startdate.ToString("yyyyMMdd")
        End If

        If dis_endData = "" Then

            s.Append("$('#lblModalTitle').html('Perhatian');")
            s.Append("$('#lblModalInstruction').html('Sila masukkan tarikh tamat peperiksaan');")
            ScriptManager.RegisterStartupScript(btnLogin, GetType(String), "x", s.ToString() & "$('#myModal').modal('show');", True)
            Return
        Else
            datetime_enddate = Convert.ToDateTime(dis_endData)
            end_date = datetime_enddate.ToString("yyyyMMdd")
        End If

        Dim get_date As String = DateTime.Now.ToString("yyyyMMdd")

        If get_date >= start_date And get_date <= end_date Then

            If Now > dtime Then

                If dlstLanguage.SelectedValue = "BM" Then
                    s.Append("$('#lblModalTitle').html('Perhatian');")
                    s.Append("$('#lblModalInstruction').html('Ujian ini telah DITAMATKAN.');")
                Else
                    s.Append("$('#lblModalTitle').html('Attention');")
                    s.Append("$('#lblModalInstruction').html('This test was ENDED.');")
                End If

                ScriptManager.RegisterStartupScript(btnLogin, GetType(String), "x", s.ToString() & "$('#myModal').modal('show');", True)
                Return

            End If

        Else

            s.Append("$('#lblModalTitle').html('Perhatian');")
            s.Append("$('#lblModalInstruction').html('Maaf, Tarikh peperiksaan masih belum bermula');")
            ScriptManager.RegisterStartupScript(btnLogin, GetType(String), "x", s.ToString() & "$('#myModal').modal('show');", True)
            Return

        End If

        If txtmykad.Value = "" Then
            If dlstLanguage.SelectedValue = "BM" Then
                warning = "Sila masukkan No. MyKid."
            ElseIf dlstLanguage.SelectedValue = "BI" Then
                warning = "Please enter MyKid No."
            End If

            ScriptManager.RegisterStartupScript(btnLogin, GetType(String), "alert", "$('#lblalert').html('" & warning & "').show();", True)

            Return

        End If

        If txtmykad.Value.Length <> 12 Then

            warning = "Sila masukkan No. MyKid dengan betul."

            ScriptManager.RegisterStartupScript(btnLogin, GetType(String), "alert", "$('#lblalert').html('" & warning & "').show();", True)
            Return
        Else
            Dim substring_mykad As String = txtmykad.Value.Substring(0, 2)

            Dim year As String = Now.Year
            year = year.Substring(2, 2)

            If substring_mykad > year Then
                warning = "Ujian ini hanya untuk kanak kanak yang berumur 2 hingga 6 tahun sahaja."

                ScriptManager.RegisterStartupScript(btnLogin, GetType(String), "alert", "$('#lblalert').html('" & warning & "').show();", True)
                Return
            Else

                Dim total As String = year - substring_mykad

                If total < 2 And total > 6 Then
                    warning = "Ujian ini hanya untuk kanak kanak yang berumur 2 hingga 6 tahun sahaja."

                    ScriptManager.RegisterStartupScript(btnLogin, GetType(String), "alert", "$('#lblalert').html('" & warning & "').show();", True)
                    Return
                End If
            End If
        End If

        'init session state
        Session("UserId") = ""
        Session("UserName") = ""
        Session("Language") = dlstLanguage.SelectedValue
        Session("UserICNo") = txtmykad.Value
        Session("RoleId") = "0"
        Session("ZeroMark") = 0
        Session("UserPage") = ""
        Session("ExamId") = ""

        strSQL = "  select B.icno from pcis_exam A
                    left join pcis_user B on A.userid = B.id
                    where A.test_end is null
                    and A.test_start like '%" & Now.Year & "%' 
                    and B.icno = '" & Session("UserICNo") & "'"
        strRet = getFieldValue(strSQL)

        If strRet.Length > 0 Then
            Response.Redirect("upsi.detail.aspx")
        End If

        Response.Redirect("upsi.intro.aspx")

    End Sub



    Protected Sub dlstLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dlstLanguage.SelectedIndexChanged

        UpdateLabel("default.aspx", dlstLanguage.SelectedValue)

    End Sub

    Private Sub UpdateLabel(PageName As String, LanguageId As String)

        Dim dt As DataTable = modFunction.GetLabel(PageName, LanguageId)
        Dim lbl As Label
        Dim btn As Button


        For Each drow As DataRow In dt.Rows

            Select Case drow("ControlType").ToString

                Case "Label"
                    lbl = DirectCast(Me.FindControl(drow("ControlName").ToString), Label)
                    If Not IsNothing(lbl) Then
                        lbl.Text = drow("Value").ToString
                    End If

                Case "Button"
                    btn = DirectCast(Me.FindControl(drow("ControlName").ToString), Button)
                    If Not IsNothing(btn) Then
                        btn.Text = drow("Value").ToString
                    End If

            End Select

        Next

    End Sub


End Class