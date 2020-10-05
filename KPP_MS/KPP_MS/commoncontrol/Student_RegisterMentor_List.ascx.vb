Imports System.Data.SqlClient

Public Class Student_RegisterMentor_List
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim hide As Integer = 0

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                gridvew_list.Visible = False

                year_list_info()
                load_page()
                group_list_info()

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

    Private Sub group_list_info()
        strSQL = "SELECT distinct ri_groupname from research_info where ri_year = '" & ddl_year.SelectedValue & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_group.DataSource = ds
            ddl_group.DataTextField = "ri_groupname"
            ddl_group.DataValueField = "ri_groupname"
            ddl_group.DataBind()
            ddl_group.Items.Insert(0, New ListItem("Select Group", String.Empty))
            ddl_group.SelectedIndex = 0

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

    Protected Sub ddl_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            group_list_info()
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = "  order by ri_groupname, ri_mentor, ri_comentor ASC"

        tmpSQL = " select ri_id, ri_year, ri_groupname, ri_mentor, ri_mentorfaculty, ri_comentor, ri_comentorfaculty from research_info"

        strWhere = " where ri_year = '" & ddl_year.SelectedValue & "'"

        If ddl_group.SelectedIndex > 0 Then
            strWhere += " and ri_groupname = '" & ddl_group.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Private Sub btnBack_ServerClick(sender As Object, e As EventArgs) Handles btnBack.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub btnAdd_ServerClick(sender As Object, e As EventArgs) Handles btnAdd.ServerClick

        ''update grades and gpa
        strSQL = "UPDATE research_info set ri_mentor = '" & txtmentor.Text & "', ri_mentorfaculty = '" & txtmentor_faculty.Text & "', ri_comentor = '" & txtcomentor.Text & "', ri_comentorfaculty = '" & txtcomentor_faculty.Text & "' where ri_groupname = '" & ddl_group.SelectedValue & "' and ri_year = '" & ddl_year.SelectedValue & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage("Register student project", MessageType.Success)
        Else
            ShowMessage("Register student project", MessageType.Error)
        End If

        strRet = BindData(datRespondent)

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub list_mentor_ServerClick(sender As Object, e As EventArgs) Handles list_mentor.ServerClick
        Try
            If hide = 0 Then
                gridvew_list.Visible = True
            End If
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ddl_group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_group.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class