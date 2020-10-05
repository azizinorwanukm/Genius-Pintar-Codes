Imports System.Data.SqlClient

Public Class coordinator_Update
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

                yearList()

                year_page()
                courseList()
                staffList()
                studentlevel()

                load_page()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub year_page()
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
            Else
                ddlyear.SelectedValue = ""
            End If
        End If

    End Sub

    Private Sub load_page()
        strSQL = "SELECT * from coordinator where coordinator_ID = '" & Request.QueryString("coordinator_ID") & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlyear.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("stf_ID")) Then
                ddlstaff.SelectedValue = ds.Tables(0).Rows(0).Item("stf_ID")
            Else
                ddlstaff.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("course_Name")) Then
                ddlcourse.SelectedValue = ds.Tables(0).Rows(0).Item("course_Name")

                subjectList()
            Else
                ddlcourse.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_Name")) Then
                ddlsubject.SelectedValue = ds.Tables(0).Rows(0).Item("subject_Name")
            Else
                ddlsubject.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("coordinator_Level")) Then
                ddllevel.SelectedValue = ds.Tables(0).Rows(0).Item("coordinator_Level")
            Else
                ddllevel.SelectedValue = ""
            End If

        End If

    End Sub

    Private Sub studentlevel()
        strSQL = "SELECT Parameter from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllevel.DataSource = ds
            ddllevel.DataTextField = "Parameter"
            ddllevel.DataValueField = "Parameter"
            ddllevel.DataBind()
            ddllevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddllevel.Items.Insert(1, New ListItem("ALL", "ALL"))
            ddllevel.Items.Insert(2, New ListItem("Foundation (1-3)", "F1"))
            ddllevel.Items.Insert(3, New ListItem("Level (1-2)", "L1"))
            ddllevel.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
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

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlcourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcourse.SelectedIndexChanged

        If ddlcourse.SelectedValue = "Bahasa Antarabangsa" Or ddlcourse.SelectedValue = "AP Courses" Then
            subjectList()
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
        Dim strOrderby As String = " ORDER BY coordinator_ID ASC"

        tmpSQL = "select coordinator_ID,staff_Name, course_Name, subject_Name, year from coordinator left join staff_Info on coordinator.stf_ID = staff_Info.stf_ID"
        strWhere = " where year = '" & ddlyear.SelectedValue & "'"
        strWhere += " and coordinator.stf_ID = '" & ddlstaff.SelectedValue & "'"

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

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        staffList()
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        If ddlcourse.SelectedValue <> "" And ddlcourse.SelectedValue <> "Bahasa Antarabangsa" And ddlcourse.SelectedValue <> "AP Courses" Then

            'UPDATE
            strSQL = "UPDATE coordinator SET stf_ID='" & ddlstaff.SelectedValue & "',course_Name ='" & ddlcourse.SelectedValue & "',subject_Name ='" & ddlcourse.SelectedValue & "',year ='" & ddlyear.SelectedValue & "',coordinator_Level ='" & ddllevel.SelectedValue & "' WHERE coordinator_ID ='" & Request.QueryString("coordinator_ID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        ElseIf ddlcourse.SelectedValue = "Bahasa Antarabangsa" Or ddlcourse.SelectedValue > "AP Courses" Then

            'UPDATE
            strSQL = "UPDATE coordinator SET stf_ID='" & ddlstaff.SelectedValue & "',course_Name ='" & ddlcourse.SelectedValue & "',subject_Name ='" & ddlsubject.SelectedValue & "',year ='" & ddlyear.SelectedValue & "',coordinator_Level ='" & ddllevel.SelectedValue & "' WHERE coordinator_ID ='" & Request.QueryString("coordinator_ID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If


        If strRet = "0" Then
            Response.Redirect("admin_view_koordinator.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        Else
            Response.Redirect("admin_view_koordinator.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_view_koordinator.aspx?&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

End Class