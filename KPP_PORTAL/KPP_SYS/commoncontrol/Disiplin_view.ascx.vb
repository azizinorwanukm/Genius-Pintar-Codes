Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Web.UI.Control

Public Class Disiplin_view
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim pattern As String = "dd MMMM yyyy"

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = ""

                id = Request.QueryString("admin_ID")
                txtstudent.Text = ""
                case_list()
                yeardropdown()
                '' cal.Visible = False

                ''get a user access
                Dim userAccess As String = ""
                userAccess = "select staff_Position from staff_Info where stf_ID = '" & id & "'"
                Dim access As String = getFieldValue(userAccess, strConn)
                hiddenaccess.Value = access
                strRet = BindData(datRespondent)
                '' load_page()
                dicipline_dropdownvalue()
                ''Generate_Table()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnRegDicipline_ServerClick(sender As Object, e As EventArgs) Handles btnRegDicipline.ServerClick
        Try
            Response.Redirect("admin_edit_disiplin.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlLevelNaming_selectedindexchange(sender As Object, e As EventArgs) Handles ddlLevelNaming.SelectedIndexChanged
        Try
            class_info_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub ddlClassnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassnaming.SelectedIndexChanged

        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlCasenaming_SelectedIndexCHanged(sender As Object, e As EventArgs) Handles ddlCasenaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlYear_selectedindexchange(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            classLevel_Dropdown()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim mydataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(mydataSet, "myaccount")

            gvTable.DataSource = mydataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False

        End Try

        Return True


    End Function


    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String

        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue


    End Function


    Protected Sub class_info_list()
        strSQL = "SELECT distinct class_Name from class_info where class_level = '" & ddlLevelNaming.SelectedValue & "' and class_type = 'Compulsory' and class_year = '" & ddlYear.SelectedValue & "' order by Class_Name ASC"
        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassnaming.DataSource = ds
            ddlClassnaming.DataTextField = "class_Name"
            ddlClassnaming.DataValueField = "class_Name"
            ddlClassnaming.DataBind()
            ddlClassnaming.Items.Insert(0, New ListItem("Select Class", String.Empty))


        Catch ex As Exception
            objconn.Dispose()
        End Try
    End Sub
    Protected Sub case_list()
        strSQL = "SELECT case_Name,case_ID from case_info order by case_ID "
        Dim strconn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCasenaming.DataSource = ds
            ddlCasenaming.DataTextField = "case_Name"
            ddlCasenaming.DataValueField = "case_Name"
            ddlCasenaming.DataBind()
            ddlCasenaming.Items.Insert(0, New ListItem("Select Case", String.Empty))

        Catch ex As Exception
            objconn.Dispose()
        End Try

    End Sub
    Protected Sub classLevel_Dropdown()
        strSQL = "select distinct class_level from class_info where class_year = '" & ddlYear.SelectedValue & "'"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionstring")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlda As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlda.Fill(ds, "anytable")

            ddlLevelNaming.DataSource = ds
            ddlLevelNaming.DataTextField = "class_level"
            ddlLevelNaming.DataValueField = "class_level"
            ddlLevelNaming.DataBind()
            ddlLevelNaming.Items.Insert(0, New ListItem("Select Class Level", String.Empty))
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub yeardropdown()
        strSQL = "select distinct class_year from class_info  "

        Dim strconn As String = ConfigurationManager.AppSettings("connectionstring")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlda As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlda.Fill(ds, "anytable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "class_year"
            ddlYear.DataValueField = "class_year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select year", String.Empty))

        Catch ex As Exception

        End Try
    End Sub

    Private Function getSQL() As String

        Dim dates As String = Request.Form("datepicker")
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = "ORDER BY dicipline_info.disiplin_id ASC"

        tmpSQL = "Select distinct
                   dicipline_info.disiplin_id,student_info.student_ID,student_info.student_Mykad,
                   dicipline_info.Dicipline_Date,
                   student_info.student_Name,
                   student_info.student_Photo,
                   dicipline_info.class_Name,
                   dicipline_info.case_Name
                   From dicipline_info
                   left join student_info on dicipline_info.student_ID = student_info.student_ID
                    Left Join course on dicipline_info.student_ID = course.std_ID
                     Left Join class_info on class_info.class_ID = course.class_ID
				  "


        strWhere = " WHERE dicipline_info.disiplin_id IS NOT NULL "
        ''strWhere += " OR dicipline_info.case_Name = '" & ddlCasenaming.SelectedValue & "'"
        '' strWhere += " OR class_info.class_Name = '" & ddlClassnaming.SelectedValue & "'"


        If Not txtstudent.Text.Length = 0 Then
            strWhere += " AND dicipline_info.student_Name LIKE '%" & txtstudent.Text & " %'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR dicipline_info.student_ID LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not StartDate.Text.Length = 0 And Not EndDate.Text.Length = 0 Then

            strWhere += " AND convert(date,dicipline_info.Dicipline_Date) between '" & StartDate.Text & "' and '" & EndDate.Text & "'"

        End If
        If ddlClassnaming.SelectedIndex > 0 Then
            strWhere += " And dicipline_info.class_Name = '" & ddlClassnaming.SelectedValue & "'"
        End If

        If ddlCasenaming.SelectedIndex > 0 Then
            strWhere += " and dicipline_info.case_Name = '" & ddlCasenaming.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Protected Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        ''Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try

            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete dicipline_info where disiplin_id = '" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub datrespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing

        Dim strkey As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Dim value As String

        strSQL = "select * from dicipline_info left join 
                    student_info on dicipline_info.student_ID = student_info.student_id
                   where dicipline_info.disiplin_id = '" + strkey + "'"

        '' Dim strDIc As String = "select case_name from case_info"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDADicipline As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDADicipline.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                hiddenaccess.Value = strkey
                dic_info.Attributes.Add("style", "display:block")
                dic_table.Attributes.Add("style", "display:none")

                student_Mykad.Text = ds.Tables(0).Rows(0)("student_mykad").ToString()
                Student_name.Text = ds.Tables(0).Rows(0)("student_name").ToString()
                student_id.Text = ds.Tables(0).Rows(0)("student_id").ToString()
                Student_class.Text = ds.Tables(0).Rows(0)("class_name").ToString()
                ddlDiciplinetype.SelectedValue = ds.Tables(0).Rows(0)("case_name").ToString()
                Merit.Text = ds.Tables(0).Rows(0)("dicipline_merit").ToString()
                student_compound.Text = ds.Tables(0).Rows(0)("dicipline_compound").ToString()
                value = ds.Tables(0).Rows(0)("dicipline_bill").ToString()
                SaveDate.Text = ds.Tables(0).Rows(0)("Dicipline_Date").ToString()

                If value = "1" Then
                    Checkbox2.Checked = True
                ElseIf value = "0" Then
                    Checkbox2.Checked = False
                End If

                pelapor_id.Text = ds.Tables(0).Rows(0)("staff_id").ToString()
                Person_charge.Text = ds.Tables(0).Rows(0)("staff_name").ToString()
                Detail_case.Text = ds.Tables(0).Rows(0)("detail_case").ToString()
                Action_box.Text = ds.Tables(0).Rows(0)("dicipline_action").ToString()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnback_serverclik(Sender As Object, e As EventArgs) Handles btnback.ServerClick
        dic_info.Attributes.Add("style", "display:none")
        dic_table.Attributes.Add("style", "display:block")
    End Sub

    Protected Sub btnsimpan_serverclick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorcount As Integer = 0
        Dim value As Integer


        If Checkbox2.Checked = True Then
            value = 1
        Else
            value = 0
        End If

        Try
            If SaveDate.Text <> "" And Not IsNothing(SaveDate.Text) Then
                If Student_name.Text <> "" And Not IsNumeric(Student_name.Text) And Not IsNothing(Student_name.Text) And Regex.IsMatch(Student_name.Text, "^[A-Za-z ]+$") Then
                    If student_Mykad.Text <> "" And IsNumeric(student_Mykad.Text) And student_Mykad.Text.Length < 14 Then
                        If Person_charge.Text <> "" And Not IsNumeric(Person_charge.Text) And Not IsNothing(Person_charge.Text) And Regex.IsMatch(Person_charge.Text, "^[A-Za-z ]+$") Then
                            If pelapor_id.Text <> "" Then

                                strSQL = "update dicipline_info set student_id = '" & student_id.Text & "',student_name = '" & Student_name.Text & "',class_name = '" & Student_class.Text & "', case_name = '" & ddlDiciplinetype.SelectedValue & "',dicipline_merit = '" & Merit.Text & "', dicipline_compound = '" & student_compound.Text & "',dicipline_bill = '" & value & "', staff_id = '" & pelapor_id.Text & "',staff_name = '" & Person_charge.Text & "'
                                         ,Dicipline_Date = '" & SaveDate.Text & "' ,detail_case = '" & Detail_case.Text & "',dicipline_action = '" & Action_box.Text & "' where disiplin_id = '" & hiddenaccess.Value & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                                If strRet = 0 Then
                                    errorcount = 0 '' success

                                Else
                                    errorcount = 1 'error
                                End If
                            Else
                                errorcount = 2 ''staff_id
                            End If
                        Else
                            errorcount = 3 ''staff_name

                        End If
                    Else
                        errorcount = 4 ''student mykad
                    End If
                Else
                    errorcount = 5 ''student name
                End If
            Else errorcount = 6 ''calendar format
            End If

            If errorcount = 0 Then
                Response.Redirect("admin_view_disiplin.aspx?result=10&admin_ID=" + Request.QueryString("admin_ID"))
            ElseIf errorcount = 1 Then
                Response.Redirect("admin_view_disiplin.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
            ElseIf errorcount = 2 Then
                Response.Redirect("admin_view_disiplin.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
            ElseIf errorcount = 3 Then
                Response.Redirect("admin_view_disiplin.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
            ElseIf errorcount = 4 Then
                Response.Redirect("admin_view_disiplin.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
            ElseIf errorcount = 5 Then
                Response.Redirect("admin_view_disiplin.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
            ElseIf errorcount = 6 Then
                Response.Redirect("admin_view_disiplin.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))
            End If
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub dicipline_dropdownvalue()
        strSQL = "select case_name from case_info"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionstring")
        Dim objconn As SqlConnection = New SqlConnection(strconn)
        Dim sqlda As New SqlDataAdapter(strSQL, objconn)

        Try
            Dim ds As DataSet = New DataSet
            sqlda.Fill(ds, "anytable")

            ddlDiciplinetype.DataSource = ds
            ddlDiciplinetype.DataTextField = "case_name"
            ddlDiciplinetype.DataValueField = "case_name"
            ddlDiciplinetype.DataBind()


        Catch ex As Exception

        End Try

    End Sub


    Protected Sub disiplin_onselectedindexchanged(sender As Object, e As EventArgs) Handles ddlDiciplinetype.SelectedIndexChanged

        strSQL = "select merit,compound,Bill from case_info where case_name = '" & ddlDiciplinetype.Text & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim results As String = "1"
        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                Merit.Text = ds.Tables(0).Rows(0)("merit").ToString()
                student_compound.Text = ds.Tables(0).Rows(0)("compound").ToString()

                If ds.Tables(0).Rows(0)("Bill").ToString() = results Then
                    Checkbox2.Checked = True
                Else
                    Checkbox2.Checked = False
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub studentID()
        strSQL = "SELECT student_id from student_info where student_Mykad = '" & student_Mykad.Text & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                student_id.Text = ds.Tables(0).Rows(0)("student_id").ToString()

            End If
            studentclass()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub studentclass()

        strSQL = "SELECT DISTINCT class_info.class_name from class_info 
                left join course on class_info.class_id = course.class_id
                left join student_info on course.std_ID = student_info.std_ID
                WHERE class_info.class.class_type = 'Compulsory' and student_info.student_ID = '" & student_id.Text & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                Student_class.Text = ds.Tables(0).Rows(0)("class_name").ToString()

            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub studentName()

        strSQL = "SELECT student_Name from student_info where student_Mykad = '" & student_Mykad.Text & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                Student_name.Text = ds.Tables(0).Rows(0)("student_name").ToString()

            End If


        Catch ex As Exception

        End Try

    End Sub
    Protected Sub studentMyKad()
        strSQL = "select student_mykad from student_info where student_id = '" & student_id.Text & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                student_Mykad.Text = ds.Tables(0).Rows(0)("student_mykad").ToString()

            End If
            studentName()

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub pelapor_name()
        strSQL = "select staff_Name from staff_info where staff_id = '" & pelapor_id.Text & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                Person_charge.Text = ds.Tables(0).Rows(0)("staff_Name").ToString()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class