Imports System.Data.SqlClient

Public Class Student_RegisterResearch_List
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                year_list_info()
                level_list_info()
                staff_list_info()

                load_page()

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub year_list_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_year.DataSource = ds
            ddl_year.DataTextField = "Parameter"
            ddl_year.DataValueField = "Parameter"
            ddl_year.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub level_list_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_level.DataSource = ds
            ddl_level.DataTextField = "Parameter"
            ddl_level.DataValueField = "Parameter"
            ddl_level.DataBind()
            ddl_level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_level.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_list_info()
        strSQL = "select * from class_info where class_year = '" & ddl_year.SelectedValue & "' and class_Level = '" & ddl_level.SelectedValue & "' and class_type = 'Compulsory' order by class_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_class.DataSource = ds
            ddl_class.DataTextField = "class_Name"
            ddl_class.DataValueField = "class_ID"
            ddl_class.DataBind()
            ddl_class.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddl_class.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staff_list_info()
        strSQL = "select * from staff_info where staff_Status = 'Access' and staff_ID <> 'Araken2019' order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_staff.DataSource = ds
            ddl_staff.DataTextField = "staff_Name"
            ddl_staff.DataValueField = "stf_ID"
            ddl_staff.DataBind()
            ddl_staff.Items.Insert(0, New ListItem("Select Staff", String.Empty))
            ddl_staff.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT * from setting where Type = 'Year' and Parameter ='" & Now.Year & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddl_year.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddl_year.SelectedValue = Now.Year
            End If
        End If
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by student_info.student_Name asc"

        tmpSQL = "  select distinct student_info.std_ID, student_info.student_Name, student_info.student_Mykad, class_info.class_level, class_info.class_Name from student_info
                    left join course on student_info.std_ID = course.std_Id
                    left join class_info on course.class_Id = class_info.class_ID"
        strWhere = " where student_info.student_Status = 'Access' and class_info.class_type = 'Compulsory'"
        strWhere += " and course.year = '" & ddl_year.SelectedValue & "' and class_info.class_year = '" & ddl_year.SelectedValue & "'"

        If ddl_class.SelectedIndex > 0 Then
            strWhere += " and class_info.class_ID = '" & ddl_class.SelectedValue & "'"
        End If

        If ddl_level.SelectedIndex > 0 Then
            strWhere += " and class_info.class_Level = '" & ddl_level.SelectedValue & "'"
        End If

        If txtStudent.Text.Length > 0 Then
            strWhere += " And (student_info.student_ID Like '%" & txtStudent.Text & "%'"
            strWhere += " or student_info.student_Name LIKE '%" & txtStudent.Text & "%'"
            strWhere += " or student_info.student_Mykad LIKE '%" & txtStudent.Text & "%')"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function


    Protected Sub ddl_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            class_list_info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged
        Try
            class_list_info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_class.SelectedIndexChanged
        Try
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

    Private Sub btnBack_ServerClick(sender As Object, e As EventArgs) Handles btnBack.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnAdd_ServerClick(sender As Object, e As EventArgs) Handles btnAdd.ServerClick
        Dim i As Integer = 0
        Dim value As String = ""

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    strSQL = "insert into research_info(std_id,ri_year,ri_groupname,stf_id) values('" & strKey & "','" & ddl_year.SelectedValue & "','" & txt_group.Text & "','" & ddl_staff.SelectedValue & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            End If
        Next

        If strRet = "0" Then
            ShowMessage("Register student group", MessageType.Success)
        Else
            ShowMessage("Register student group", MessageType.Error)
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class