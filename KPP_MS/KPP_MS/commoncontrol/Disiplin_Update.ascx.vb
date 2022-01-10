Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing

Public Class Disiplin_Update
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim staffSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim getStatus As String = Request.QueryString("status")

                If getStatus = "VC" Then ''View Case
                    txtbreadcrum1.Text = "View Case"
                    ViewCase.Visible = True
                    RegisterCase.Visible = False

                    btnViewCase.Attributes("class") = "btn btn-info"
                    btnRegisterCase.Attributes("class") = "btn btn-default font"

                    View_CaseYear_List()
                    View_CaseMonth_List()
                    View_CaseLevel_List()

                ElseIf getStatus = "RC" Then ''Register Case
                    txtbreadcrum1.Text = "Register Case"
                    ViewCase.Visible = False
                    RegisterCase.Visible = True

                    btnViewCase.Attributes("class") = "btn btn-default font"
                    btnRegisterCase.Attributes("class") = "btn btn-info"

                    txtDate.Text = Date.Now.ToString("dd MMMM yyyy")

                    txtStudent_Name.Enabled = False
                    txtClass_Name.Enabled = False
                    txtStudent_Level.Enabled = False

                    caseCategory_List()
                    caseName_List()

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnViewCase_ServerClick(sender As Object, e As EventArgs) Handles btnViewCase.ServerClick
        Response.Redirect("admin_edit_disiplin.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=VC")
    End Sub

    Private Sub btnRegisterCase_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterCase.ServerClick
        Response.Redirect("admin_edit_disiplin.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=RC")
    End Sub

    Private Sub caseCategory_List()
        strSQL = "SELECT * from setting where Type = 'Discipline' order by ID DESC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCase_Category.DataSource = ds
            ddlCase_Category.DataTextField = "Parameter"
            ddlCase_Category.DataValueField = "Value"
            ddlCase_Category.DataBind()
            ddlCase_Category.Items.Insert(0, New ListItem("Select Case Category", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub caseName_List()
        strSQL = "SELECT * from case_info where case_Category = '" & ddlCase_Category.SelectedValue & "' order by case_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCase_Name.DataSource = ds
            ddlCase_Name.DataTextField = "case_Name"
            ddlCase_Name.DataValueField = "case_ID"
            ddlCase_Name.DataBind()
            ddlCase_Name.Items.Insert(0, New ListItem("Select Case Name", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlCase_Category_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCase_Category.SelectedIndexChanged
        Try
            caseName_List()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSearch_Student_ServerClick(sender As Object, e As EventArgs) Handles btnSearch_Student.ServerClick

        Dim get_date As String = txtDate.Text
        Dim LastFour_Char As String = get_date.Substring(get_date.Length - 4, 4)

        Dim get_StudentName As String = "   select student_Name from student_info where student_Mykad = '" & txtStudentInformation.Text & "' and student_Status = 'Access' and student_ID is not null and student_ID <> '' and student_ID like '%M%'"
        Dim get_StudentLevel As String = "  select distinct MAX(A.student_Level) as student_Level from student_Level A left join student_info B on A.std_ID = B.std_ID
                                            where B.student_Mykad = '" & txtStudentInformation.Text & "' and B.student_Status = 'Access' and B.student_ID is not null and B.student_ID <> '' and B.student_ID like '%M%'  and A.year = '" & LastFour_Char & "'"
        Dim get_StudentClass As String = "  select distinct A.class_Name from class_info A left join course B on A.class_ID = B.class_ID left join student_info C on B.std_ID = C.std_ID
                                            where C.student_Mykad = '" & txtStudentInformation.Text & "' and C.student_Status = 'Access' and C.student_ID is not null and C.student_ID <> '' and C.student_ID like '%M%'
                                            and B.year = '" & LastFour_Char & "' and A.class_year = '" & LastFour_Char & "' and A.class_type = 'Compulsory'"

        txtStudent_Name.Text = oCommon.getFieldValue(get_StudentName)
        txtStudent_Level.Text = oCommon.getFieldValue(get_StudentLevel)
        txtClass_Name.Text = oCommon.getFieldValue(get_StudentClass)

    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick

        If checkData() = True Then

            Dim get_date As String = txtDate.Text
            Dim LastFour_Char As String = get_date.Substring(get_date.Length - 4, 4)

            ''Get Std_ID
            Dim Find_StdID As String = "Select std_ID from student_info where student_Mykad = '" & txtStudentInformation.Text & "' and student_Status = 'Access' and student_ID is not null and student_ID <> '' and student_ID like '%M%'"
            Dim Get_StdID As String = oCommon.getFieldValue(Find_StdID)

            ''Get Class_ID
            Dim Find_ClassID As String = "Select class_ID from class_info where class_year = '" & LastFour_Char & "' and class_Name = '" & txtClass_Name.Text & "'"
            Dim Get_ClassID As String = oCommon.getFieldValue(Find_ClassID)

            ''Get Demerit Point
            Dim Find_DemeritPoint As String = "Select case_MeritDemerit_Point from case_info where case_ID = '" & ddlCase_Name.SelectedValue & "'"
            Dim Get_DemeritPoint As String = oCommon.getFieldValue(Find_DemeritPoint)

            Dim getCheck_Counseling As String = ""

            If Check_NeedCounseling.Checked = True Then
                getCheck_Counseling = "True"
            Else
                getCheck_Counseling = "False"
            End If

            Dim Get_Date_SS As String = txtDate.Text.Substring(0, 2)
            Dim Get_Year_SS As String = txtDate.Text.Substring(txtDate.Text.Length - 4, 4)
            Dim Get_Month_SS As String = txtDate.Text.Remove(0, 3)
            Get_Month_SS = Get_Month_SS.Remove(Get_Month_SS.Length - 5, 5)

            If Get_Month_SS = "January" Then
                Get_Month_SS = "01"
            ElseIf Get_Month_SS = "February" Then
                Get_Month_SS = "02"
            ElseIf Get_Month_SS = "March" Then
                Get_Month_SS = "03"
            ElseIf Get_Month_SS = "April" Then
                Get_Month_SS = "04"
            ElseIf Get_Month_SS = "May" Then
                Get_Month_SS = "05"
            ElseIf Get_Month_SS = "June" Then
                Get_Month_SS = "06"
            ElseIf Get_Month_SS = "July" Then
                Get_Month_SS = "07"
            ElseIf Get_Month_SS = "August" Then
                Get_Month_SS = "08"
            ElseIf Get_Month_SS = "September" Then
                Get_Month_SS = "09"
            ElseIf Get_Month_SS = "October" Then
                Get_Month_SS = "10"
            ElseIf Get_Month_SS = "November" Then
                Get_Month_SS = "11"
            ElseIf Get_Month_SS = "December" Then
                Get_Month_SS = "12"
            End If

            Dim Final_Date_Data As String = Get_Date_SS & "/" & Get_Month_SS & "/" & Get_Year_SS

            Using STDDATA As New SqlCommand("INSERT INTO dicipline_info(std_ID, class_ID, case_ID, Detail_Case, Dicipline_Date, meritdemerit_point, need_Counseling ) values ('" & Get_StdID & "','" & Get_ClassID & "','" & ddlCase_Name.SelectedValue & "','" & txtCase_Detail.Text & "','" & Final_Date_Data & "','" & Get_DemeritPoint & "','" & getCheck_Counseling & "')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()

                If i <> 0 Then
                    ShowMessage(" Register Student Disciplinary Case ", MessageType.Success)
                Else
                    ShowMessage(" Unsuccessful Register Student Disciplinary Case ", MessageType.Error)
                End If
            End Using
        End If
    End Sub

    Public Function checkData()

        If txtStudentInformation.Text.Length = 0 Then
            ShowMessage(" Please Fill In Student Information ", MessageType.Error)
            Return False
        End If

        If Not Regex.IsMatch(txtStudentInformation.Text, "^[A-Z0-9]+$") Then
            ShowMessage(" Please Fill In Student Information With The Correct Format ", MessageType.Error)
            Return False
        End If

        If ddlCase_Category.SelectedIndex = 0 Then
            ShowMessage(" Please Select Case Category ", MessageType.Error)
            Return False
        End If

        If ddlCase_Name.SelectedIndex = 0 Then
            ShowMessage(" Please Select Case Name ", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Private Sub View_CaseYear_List()
        strSQL = "select distinct RIGHT(Dicipline_Date,4) as date from dicipline_info order by date asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlView_CaseYear.DataSource = ds
            ddlView_CaseYear.DataTextField = "date"
            ddlView_CaseYear.DataValueField = "date"
            ddlView_CaseYear.DataBind()
            ddlView_CaseYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub View_CaseMonth_List()
        strSQL = "select Parameter, Value from setting where type = 'month' order by ID ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlView_CaseMonth.DataSource = ds
            ddlView_CaseMonth.DataTextField = "Parameter"
            ddlView_CaseMonth.DataValueField = "Value"
            ddlView_CaseMonth.DataBind()
            ddlView_CaseMonth.Items.Insert(0, New ListItem("Select Month", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub View_CaseLevel_List()
        strSQL = "select * from setting where type = 'level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlVIew_CaseLevel.DataSource = ds
            ddlVIew_CaseLevel.DataTextField = "Parameter"
            ddlVIew_CaseLevel.DataValueField = "Value"
            ddlVIew_CaseLevel.DataBind()
            ddlVIew_CaseLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub View_CaseClass_List()
        strSQL = "Select class_ID, class_Name from class_info where class_year = '" & ddlView_CaseYear.SelectedValue & "' and class_Level = '" & ddlVIew_CaseLevel.SelectedValue & "' and class_type = 'Compulsory' and class_Campus = 'PGPN' order by class_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlView_CaseClass.DataSource = ds
            ddlView_CaseClass.DataTextField = "class_Name"
            ddlView_CaseClass.DataValueField = "class_ID"
            ddlView_CaseClass.DataBind()
            ddlView_CaseClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlView_CaseYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView_CaseYear.SelectedIndexChanged
        Try
            View_CaseClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlView_CaseMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView_CaseMonth.SelectedIndexChanged
        Try
            View_CaseClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlVIew_CaseLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVIew_CaseLevel.SelectedIndexChanged
        Try
            View_CaseClass_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlView_CaseClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView_CaseClass.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
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

    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER by B.Dicipline_Date ASC, C.class_Name ASC, D.student_Name ASC "

        tmpSQL = "  select B.disiplin_id, D.student_Name, C.class_Name, A.case_Category, B.meritdemerit_point, B.Dicipline_Date from case_info A
                    left join dicipline_info B on A.case_ID = B.case_ID
                    left join class_info C on B.class_ID = C.class_ID
                    left join student_info D on B.std_ID = D.std_ID
                    where D.student_Status = 'Access' and D.student_ID is not null and D.student_ID <> '' and D.student_Id like '%M%'
                    and B.Dicipline_Date like '%" & ddlView_CaseYear.SelectedValue & "%'"

        If ddlView_CaseMonth.SelectedIndex > 0 Then
            strWhere += "   and B.Dicipline_Date like '%/" & ddlView_CaseMonth.SelectedValue & "/%'"
        End If

        If ddlVIew_CaseLevel.SelectedIndex > 0 Then
            strWhere += "   and C.class_Level = '" & ddlVIew_CaseLevel.SelectedValue & "'"
        End If

        If ddlView_CaseClass.SelectedIndex > 0 Then
            strWhere += "   and C.class_ID = '" & ddlView_CaseClass.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Return getSQL
    End Function

    Protected Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
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

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyCode As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_disiplin_kemaskini.aspx?disiplin_id=" + strKeyCode + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class