Imports System.Data.SqlClient

Public Class admin_userLogin
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0
    Dim pattern As String = "dd MMMM yyyy"

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                data_user_list()
                data_akses_list()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub data_user_list()
        strSQL = "select Parameter from setting where Type = ''"

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
        strSQL = "select Parameter from setting where Type = ''"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLogin.DataSource = ds
            ddlLogin.DataTextField = "Parameter"
            ddlLogin.DataValueField = "Parameter"
            ddlLogin.DataBind()
            ddlLogin.Items.Insert(0, New ListItem("Select Activity", String.Empty))
            ddlLogin.Items.Insert(1, New ListItem("Login", "LOGIN"))
            ddlLogin.Items.Insert(2, New ListItem("Logout", "LOGOUT"))

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
        Dim strOrderby As String = " ORDER BY Log_Date DESC"

        Dim get_Start_DATE As Date = StartDate.Text
        Dim get_End_DATE As Date = EndDate.Text

        Dim start_DATE As String = get_Start_DATE.ToString("yyyy/MM/dd")
        Dim end_DATE As String = get_End_DATE.ToString("yyyy/MM/dd")

        tmpSQL = "select security_LoginTrail.Trail_ID, security_LoginTrail.Login_ID, staff_Info.staff_Name, security_LoginTrail.Log_Date, security_LoginTrail.Activity 
                  from security_LoginTrail
                  left join staff_Info on security_LoginTrail.Login_ID = staff_Info.staff_Login"
        strWhere = " WHERE Activity = '" & ddlLogin.SelectedValue & "'
                     and Log_Date >= '" & start_DATE & " 00:00:00'
                     and Log_Date <= '" & end_DATE & " 23:59:59'
                     and security_LoginTrail.Login_ID = staff_Info.staff_Login"

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
        Dim strOrderby As String = " ORDER BY Log_Date DESC"

        Dim get_Start_DATE As Date = StartDate.Text
        Dim get_End_DATE As Date = EndDate.Text

        Dim start_DATE As String = get_Start_DATE.ToString("yyyy/MM/dd")
        Dim end_DATE As String = get_End_DATE.ToString("yyyy/MM/dd")

        tmpSQL = "select security_LoginTrail.Trail_ID, security_LoginTrail.Login_ID, student_info.student_Name, security_LoginTrail.Log_Date, security_LoginTrail.Activity 
                  from security_LoginTrail
                  left join student_info on security_LoginTrail.Login_ID = student_info.student_Mykad"
        strWhere = " WHERE Activity = '" & ddlLogin.SelectedValue & "'
                     and Log_Date >= '" & start_DATE & " 00:00:00'
                     and Log_Date <= '" & end_DATE & " 23:59:59'
                     and security_LoginTrail.Login_ID = student_info.student_Mykad"

        getSQLStudent = tmpSQL & strWhere & strOrderby

        Return getSQLStudent
    End Function

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick

        If ddlUser.SelectedIndex = 1 Then

            UserType_HF.Value = "Staff"
            strRet = BindDataStaff(datRespondentStaff)

        ElseIf ddlUser.SelectedIndex = 2 Then

            UserType_HF.Value = "Student"
            strRet = BindDataStudent(datRespondentStudent)

        End If
    End Sub

End Class