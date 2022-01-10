Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class lecturer_view_course
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

                Dim id As String = ""
                id = Request.QueryString("admin_ID")

                ddlCourse.Enabled = False

                year_list()
                course_level_list()
                course_sem_list()

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_level_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseLevel.DataSource = ds
            ddlCourseLevel.DataTextField = "Parameter"
            ddlCourseLevel.DataValueField = "Parameter"
            ddlCourseLevel.DataBind()
            ddlCourseLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlCourseLevel.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_sem_list()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseSem.DataSource = ds
            ddlCourseSem.DataTextField = "Parameter"
            ddlCourseSem.DataValueField = "Value"
            ddlCourseSem.DataBind()
            ddlCourseSem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlCourseSem.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_name_list()

        Dim checkSubjectType As String = "Select subject_type from subject_info where subject_ID = '" & ddlCourse.SelectedValue & "'"
        Dim findSubjectType As String = oCommon.getFieldValue(checkSubjectType)

        If findSubjectType = "Compulsory" Then
            strSQL = "SELECT class_Name,class_ID FROM class_info WHERE class_year = '" & ddlYear.SelectedValue & "' and class_level = '" & ddlCourseLevel.SelectedValue & "' and class_type = 'Compulsory'"
        Else
            strSQL = "SELECT class_Name,class_ID FROM class_info WHERE class_year = '" & ddlYear.SelectedValue & "' and class_level = '" & ddlCourseLevel.SelectedValue & "' and class_sem = '" & ddlCourseSem.SelectedValue & "' and class_type <> 'Compulsory'"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClass.DataSource = ds
            ddlClass.DataTextField = "class_Name"
            ddlClass.DataValueField = "class_ID"
            ddlClass.DataBind()
            ddlClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlClass.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_name_list()

        strSQL = "SELECT subject_Name,subject_ID FROM subject_info WHERE subject_year ='" & ddlYear.SelectedValue & "' and subject_info.subject_StudentYear = '" & ddlCourseLevel.SelectedValue & "' and subject_info.subject_sem = '" & ddlCourseSem.SelectedValue & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            ddlCourse.Enabled = True

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourse.DataSource = ds
            ddlCourse.DataTextField = "subject_Name"
            ddlCourse.DataValueField = "subject_ID"
            ddlCourse.DataBind()
            ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlCourse.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

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

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY staff_Info.staff_Name ASC"

        tmpSQL = "select lecturer.ID,staff_Info.staff_Name,subject_info.subject_StudentYear,subject_info.subject_Name,subject_info.subject_sem,class_info.class_Name from lecturer
                  left join staff_Info on lecturer.stf_ID=staff_Info.stf_ID
                  left join subject_info on lecturer.subject_ID=subject_info.subject_ID
                  left join class_info on lecturer.class_ID=class_info.class_ID"
        strWhere = " WHERE staff_Info.stf_ID IS NOT NULL"
        strWhere += " and lecturer.lecturer_year = '" & ddlYear.SelectedValue & "'"

        If ddlCourse.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_ID = '" & ddlCourse.SelectedValue & "'"
        End If

        If ddlClass.SelectedIndex > 0 Then
            strWhere += " AND class_info.class_ID = '" & ddlClass.SelectedValue & "'"
        End If

        If ddlCourseSem.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_sem = '" & ddlCourseSem.SelectedValue & "'"
        End If

        If ddlCourseSem.SelectedIndex > 0 Then
            strWhere += " AND subject_info.subject_StudentYear = '" & ddlCourseLevel.SelectedValue & "'"
        End If


        getSQL = tmpSQL & strWhere & strOrderby
        Debug.WriteLine(getSQL)
        ''--debug
        Return getSQL
    End Function

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourseSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseSem.SelectedIndexChanged
        Try
            course_name_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            course_name_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        Try
            class_name_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCourseLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseLevel.SelectedIndexChanged
        Try
            course_name_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub
End Class