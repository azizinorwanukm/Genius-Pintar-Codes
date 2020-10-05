Imports System.Data.SqlClient

Public Class coordinator_View
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction
    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ddlsubject.Enabled = False

                yearList()
                courseList()

                load_page()
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter from setting where Value ='" & Now.Year & "' and Type = 'Year'"

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
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
                staffList()
            Else
                ddlyear.SelectedValue = ""
            End If
        End If

    End Sub

    Private Sub yearList()
        strSQL = "SELECT Parameter from setting where Type = 'Year'"
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

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staffList()
        strSQL = "SELECT staff_Name,stf_ID from staff_Info where staff_Status = 'Access' order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaff.DataSource = ds
            ddlstaff.DataTextField = "staff_Name"
            ddlstaff.DataValueField = "stf_ID"
            ddlstaff.DataBind()
            ddlstaff.Items.Insert(0, New ListItem("Select Staff", String.Empty))
            ddlstaff.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub courseList()
        strSQL = "SELECT distinct course_Name from subject_info where subject_year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcourse.DataSource = ds
            ddlcourse.DataTextField = "course_Name"
            ddlcourse.DataValueField = "course_Name"
            ddlcourse.DataBind()
            ddlcourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlcourse.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub subjectList()
        strSQL = "SELECT distinct subject_Name from subject_info where course_Name = '" & ddlcourse.SelectedValue & "' and subject_year = '" & ddlyear.SelectedValue & "' order by subject_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsubject.DataSource = ds
            ddlsubject.DataTextField = "subject_Name"
            ddlsubject.DataValueField = "subject_Name"
            ddlsubject.DataBind()
            ddlsubject.Items.Insert(0, New ListItem("Select Subject", String.Empty))
            ddlsubject.Items.Insert(1, New ListItem("ALL", "ALL"))
            ddlsubject.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlcourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcourse.SelectedIndexChanged

        If ddlcourse.SelectedValue = "Bahasa Antarabangsa" Or ddlcourse.SelectedValue = "AP Courses" Then
            ddlsubject.Enabled = True
            subjectList()

        Else
            strRet = BindData(datRespondent)
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
        Dim strOrderby As String = " ORDER BY staff_Info.staff_Name ASC"

        tmpSQL = "select coordinator_ID,staff_Name, coordinator_Level, subject_Name, year from coordinator left join staff_Info on coordinator.stf_ID = staff_Info.stf_ID"
        strWhere = " where year = '" & ddlyear.SelectedValue & "'"

        If ddlstaff.SelectedValue <> "" Then
            strWhere += " and coordinator.stf_ID = '" & ddlstaff.SelectedValue & "'"
        End If

        If ddlcourse.SelectedValue <> "" Then
            strWhere += " and course_Name = '" & ddlcourse.SelectedValue & "'"
        End If

        If ddlsubject.SelectedValue <> "" Then
            strWhere += " and subject_Name = '" & ddlsubject.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete coordinator where coordinator_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyName As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Dim adminID As String = Request.QueryString("admin_ID")
        Try
            Response.Redirect("admin_edit_coordinator.aspx?coordinator_ID=" + strKeyName + "&admin_ID=" + adminID)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        staffList()
    End Sub

    Protected Sub ddlstaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlstaff.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ddlsubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsubject.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

End Class