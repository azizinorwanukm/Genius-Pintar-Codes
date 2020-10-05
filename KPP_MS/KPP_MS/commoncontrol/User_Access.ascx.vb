Imports System.Data.SqlClient

Public Class User_Access
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                data_year_list()
                data_user_list()
                data_akses_list()
                data_position_list()

                ddlPosition.Enabled = False

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub data_year_list()
        strSQL = "select Parameter from setting where Type = 'Year'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlyear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub data_position_list()
        strSQL = "select Parameter,Value from setting where (Type = 'Level Access' or Type = 'Position' or Type = 'Admin Access') and idx = 'Admin'  order by Parameter ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPosition.DataSource = ds
            ddlPosition.DataTextField = "Parameter"
            ddlPosition.DataValueField = "Value"
            ddlPosition.DataBind()
            ddlPosition.Items.Insert(0, New ListItem("Select Position", String.Empty))
            ddlPosition.Items.Insert(1, New ListItem("All", "All"))
            ddlPosition.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub data_user_list()
        strSQL = "select Parameter from setting where Type = 'TEST GOD'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUser.DataSource = ds
            ddlUser.DataTextField = "Parameter"
            ddlUser.DataValueField = "Parameter"
            ddlUser.DataBind()
            ddlUser.Items.Insert(0, New ListItem("Select User", String.Empty))
            ddlUser.Items.Insert(1, New ListItem("Staff", "Staff"))
            ddlUser.Items.Insert(2, New ListItem("Student", "Student"))


        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub data_akses_list()
        strSQL = "select Parameter from setting where Type = 'Status'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlAccess.DataSource = ds
            ddlAccess.DataTextField = "Parameter"
            ddlAccess.DataValueField = "Parameter"
            ddlAccess.DataBind()
            ddlAccess.Items.Insert(0, New ListItem("Select Access", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataStaff(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLStaff, strConn)
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

    Private Function getSQLStaff() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY staff_Name ASC"

        tmpSQL = "select staff_Login.login_ID, staff_Info.staff_Name, staff_Login.staff_Login, staff_Login.staff_Password, staff_Login.staff_Status from staff_Info
                  left join staff_Login on staff_Info.stf_ID = staff_Login.stf_ID"
        strWhere = " where staff_Login.staff_Status = 'Access'
                     and staff_Login.staff_Access <> ''"

        strWhere += " AND (staff_ID = '" & txt_User.Text & "' OR staff_Name like '%" & txt_User.Text & "%' OR staff_Mykad = '" & txt_User.Text & "') "

        If ddlPosition.SelectedValue <> "" and ddlPosition.SelectedValue <> "All" Then
            strWhere += " And staff_Login.staff_Access = '" & ddlPosition.SelectedValue & "'"
        End If

        getSQLStaff = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQLStaff
    End Function

    Private Function BindDataStudent(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLStudent, strConn)
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

    Private Function getSQLStudent() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select distinct student_info.std_ID, student_info.student_Name, student_info.student_Mykad, student_info.student_Password, student_info.student_Status from student_info
                  left join course on student_info.std_ID = course.std_ID"
        strWhere = " where course.year = '" & ddlyear.SelectedValue & "' and student_info.student_Status = '" & ddlAccess.SelectedValue & "' "

        strWhere += " AND (student_info.student_ID = '" & txt_User.Text & "' OR student_info.student_Name like '%" & txt_User.Text & "%' OR student_info.student_Mykad = '" & txt_User.Text & "') "

        getSQLStudent = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQLStudent
    End Function

    Private Sub datRespondentStaff_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondentStaff.RowEditing

        datRespondentStaff.EditIndex = e.NewEditIndex
        Me.BindDataStaff(datRespondentStaff)

    End Sub

    Protected Sub StaffCancelEdit(sender As Object, e As EventArgs)
        datRespondentStaff.EditIndex = -1
        Me.BindDataStaff(datRespondentStaff)
    End Sub

    Protected Sub StaffUpdate(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)

        Dim txtstaffLogin As TextBox = DirectCast(datRespondentStaff.Rows(e.RowIndex).FindControl("txtstaff_Login"), TextBox)
        Dim txtstaffPassword As TextBox = DirectCast(datRespondentStaff.Rows(e.RowIndex).FindControl("txtstaff_Password"), TextBox)
        Dim txtstaffStatus As TextBox = DirectCast(datRespondentStaff.Rows(e.RowIndex).FindControl("txtstaff_Status"), TextBox)

        Dim strKeyID As String = datRespondentStaff.DataKeys(e.RowIndex).Value.ToString

        ''update the data in table
        strSQL = "UPDATE staff_Login SET staff_Login ='" & txtstaffLogin.Text & "', staff_Status ='" & txtstaffStatus.Text & "', staff_Password ='" & txtstaffPassword.Text & "' 
                  WHERE login_ID ='" & strKeyID & "' "
        strRet = oCommon.ExecuteSQL(strSQL)

        If txtstaffStatus.Text = "Access" Then
            strSQL = "UPDATE staff_Info set staff_Status = 'Access' WHERE stf_ID ='" & strKeyID & "' "
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        datRespondentStaff.EditIndex = -1
        Me.BindDataStaff(datRespondentStaff)
    End Sub

    Private Sub datRespondentStudent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondentStudent.RowEditing

        datRespondentStudent.EditIndex = e.NewEditIndex
        Me.BindDataStudent(datRespondentStudent)

    End Sub

    Protected Sub StudentCancelEdit(sender As Object, e As EventArgs)
        datRespondentStudent.EditIndex = -1
        Me.BindDataStudent(datRespondentStudent)
    End Sub

    Protected Sub StudentUpdate(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)

        Dim txtstudentPassword As TextBox = DirectCast(datRespondentStudent.Rows(e.RowIndex).FindControl("txtstudent_Password"), TextBox)
        Dim txtstudentStatus As TextBox = DirectCast(datRespondentStudent.Rows(e.RowIndex).FindControl("txtstudent_Status"), TextBox)

        Dim strKeyID As String = datRespondentStudent.DataKeys(e.RowIndex).Value.ToString

        ''update the data in table
        strSQL = "UPDATE student_info SET student_Password ='" & txtstudentPassword.Text & "', student_Status ='" & txtstudentStatus.Text & "' WHERE std_ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        datRespondentStudent.EditIndex = -1
        Me.BindDataStudent(datRespondentStudent)
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick

        If ddlUser.SelectedIndex = 1 Then

            UserType_HF.Value = "Staff"
            strRet = BindDataStaff(datRespondentStaff)

        ElseIf ddlUser.SelectedIndex = 2 Then

            UserType_HF.Value = "Student"
            strRet = BindDataStudent(datRespondentStudent)

        End If
    End Sub

    Protected Sub ddlAccess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccess.SelectedIndexChanged

        If ddlUser.SelectedValue = "Staff" And ddlAccess.SelectedValue = "Access" Then
            ddlPosition.Enabled = True
        Else
            ddlPosition.Enabled = False
        End If
    End Sub
End Class