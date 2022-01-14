Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing

Public Class Disiplin_Update
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
                result = Request.QueryString("result")

                Dim id As String = ""
                id = Request.QueryString("admin_ID")
                dicipline()
                CurrentDate.Text = Date.Now.ToString("dd MMMM yyyy")

            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dicipline()

        strSQL = "select case_name from case_info "

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")


            ddlDiciplinetype.DataSource = ds
            ddlDiciplinetype.DataTextField = "case_name"
            ddlDiciplinetype.DataValueField = "case_name"
            ddlDiciplinetype.DataBind()
            ddlDiciplinetype.Items.Insert(0, New ListItem("-- Pick A Case --", String.Empty))
            ddlDiciplinetype.SelectedIndex = 0

        Catch ex As Exception

        End Try


    End Sub
    Protected Sub disiplin_onselectedindexchanged(sender As Object, e As EventArgs) Handles ddlDiciplinetype.SelectedIndexChanged
        Dim display As String = "block"
        HiddenField2.Value = display

        strSQL = "select merit,compound,Bill from case_info where case_name = '" & ddlDiciplinetype.Text & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim result As String = "1"
        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                Merit.Text = ds.Tables(0).Rows(0)("merit").ToString()
                student_compound.Text = ds.Tables(0).Rows(0)("compound").ToString()

                If ds.Tables(0).Rows(0)("Bill").ToString() = result Then
                    Checkbox2.Checked = True
                Else
                    Checkbox2.Checked = False
                End If

            Else
                Merit.Text = ""
                student_compound.Text = ""
                Checkbox2.Checked = False
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub searchComplainant_serverclick(sender As Object, e As EventArgs) Handles searchComplainant.ServerClick
        Dim display As String = "block"
        HiddenField3.Value = display

        strSQL = "select staff_name,staff_id from staff_info where staff_id = '" & Pelapor_id.Text & "' "
        Dim strconn As String = ConfigurationManager.AppSettings("connectionstring")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDa As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDa.Fill(ds)

            If ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                Person_charge.Text = ""

            ElseIf Pelapor_id.Text = ds.Tables(0).Rows(0)("staff_id").ToString() Then
                Person_charge.Text = ds.Tables(0).Rows(0)("staff_name").ToString()


            End If

        Catch ex As Exception

        End Try

    End Sub



    Private Sub btn_search_serverclick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Dim display As String = "block"
        HiddenField1.Value = display

        strSQL = "select class_info.class_Name, student_info.student_Name, student_info.student_Mykad, student_info.student_ID from student_info
            left join course on course.std_id = student_info.std_id
            left join class_info on class_info.class_id = course.class_id
            where student_info.student_id = '" & student_Mykad.Text & "' or student_info.student_mykad = '" & student_Mykad.Text & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            If ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                Response.Redirect("admin_edit_disiplin.aspx?result=8&admin_ID=" + Request.QueryString("admin_ID"))
                Student_name.Text = ""
                Student_class.Text = ""
                student_Mykad.Text = ""


            ElseIf student_Mykad.Text = ds.Tables(0).Rows(0)("student_id").ToString() OrElse student_Mykad.Text = ds.Tables(0).Rows(0)("student_mykad") Then
                Hidden_IC.Text = ds.Tables(0).Rows(0)("student_mykad").ToString()
                student_id.Text = ds.Tables(0).Rows(0)("student_id").ToString()
                Student_name.Text = ds.Tables(0).Rows(0)("student_name").ToString()
                Student_class.Text = ds.Tables(0).Rows(0)("class_name").ToString()


            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Btnsimpan_serverClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim value As Integer

        Select Case Checkbox2.Checked
            Case True
                value = 1
            Case Else
                value = 0
        End Select

        If ddlDiciplinetype.SelectedIndex > 0 Then
            If Student_name.Text <> "" And Not IsNumeric(Student_name.Text) And Not IsNothing(Student_name.Text) Then
                If student_id.Text <> "" And Regex.IsMatch(student_id.Text, "^[A-Za-z0-9]+$") Then
                    If Person_charge.Text <> "" And Not IsNumeric(Person_charge.Text) And Not IsNothing(Person_charge.Text) And Regex.IsMatch(Person_charge.Text, "^[A-Za-z ]+$") Then
                        If Pelapor_id.Text <> "" Then

                            Using STDDATA As New SqlCommand("INSERT INTO dicipline_info(student_ID,student_name,Dicipline_Date,Dicipline_Merit,Dicipline_Compound,Dicipline_Bill,Case_name,Detail_case,staff_ID,staff_name,Dicipline_Action,class_Name,student_Mykad) values
                                ('" & student_id.Text & "','" & Student_name.Text & "','" & CurrentDate.Text & "','" & Merit.Text & "', '" & student_compound.Text & "','" & value & "','" & ddlDiciplinetype.SelectedValue & "','" & Detail_case.Text & "','" & Pelapor_id.Text & "','" & Person_charge.Text & "','" & Action_box.Text & "','" & Student_class.Text & "', '" & Hidden_IC.Text & "')", objConn)
                                objConn.Open()
                                Dim i = STDDATA.ExecuteNonQuery()
                                objConn.Close()
                                If i <> 0 Then
                                    errorCount = 0 ''success
                                Else
                                    errorCount = 1 ''

                                End If
                            End Using
                        Else
                            errorCount = 2 ''id pelapor
                        End If
                    Else
                        errorCount = 3 ''nama person in charge
                    End If
                Else
                    errorCount = 4 ''student id
                End If
            Else
                errorCount = 5 ''student name
            End If
        Else
            errorCount = 6 ''student mykad
        End If


        If errorCount = 0 Then
            Response.Redirect("admin_edit_disiplin.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 1 Then
            Response.Redirect("admin_edit_disiplin.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_edit_disiplin.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_edit_disiplin.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_edit_disiplin.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_edit_disiplin.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 6 Then
            Response.Redirect("admin_edit_disiplin.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))


        End If

    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub


End Class